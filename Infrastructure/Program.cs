using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Application.DTOs.Requests;
using Application.DTOs.Responses;
using Application.Exceptions;
using Application.Interfaces.Repositories.Read;
using Application.Interfaces.Repositories.Write;
using Application.UseCases.Commands;
using Application.UseCases.Queries;
using Domain.Entities;
using Domain.ValueObjects;
using Infrastructure.Dal;
using Infrastructure.Dal.Repositories.Read;
using Infrastructure.Dal.Repositories.Write;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add database context with error handling
try
{
    builder.Services.AddDbContext<TaskedDbContext>(options =>
        options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));
}
catch (Exception ex)
{
    Console.WriteLine($"Warning: Database connection failed. Running without database. Error: {ex.Message}");
}

// Add repositories only if database is available
if (builder.Environment.IsDevelopment())
{
    builder.Services.AddScoped<IUserReadRepository, UserReadRepository>();
    builder.Services.AddScoped<IUserWriteRepository, UserWriteRepository>();
    builder.Services.AddScoped<IUserTaskReadRepository, UserTaskReadRepository>();
    builder.Services.AddScoped<IUserTaskWriteRepository, UserTaskWriteRepository>();
}

// Add MediatR
builder.Services.AddMediatR(cfg => {
    cfg.RegisterServicesFromAssembly(typeof(CreateUserCommand).Assembly);
});

// Configure JWT authentication
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["JwtSettings:Issuer"],
            ValidAudience = builder.Configuration["JwtSettings:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(builder.Configuration["JwtSettings:SecretKey"]!))
        };
    });

builder.Services.AddAuthorization();

// Configure Swagger with JWT support
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Tasked API", Version = "v1" });
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "JWT Authorization header using the Bearer scheme.",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Scheme = "bearer"
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
});

var app = builder.Build();

// Configure middleware
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

// Add error handling middleware
app.Use(async (context, next) =>
{
    try
    {
        await next();
    }
    catch (Exception ex)
    {
        var response = ex switch
        {
            TaskNotFoundException => Results.NotFound(new { message = ex.Message }),
            UserNotFoundException => Results.NotFound(new { message = ex.Message }),
            UnauthorizedTaskAccessException => Results.Unauthorized(),
            DuplicateEmailException => Results.Conflict(new { message = ex.Message }),
            TaskedException => Results.BadRequest(new { message = ex.Message }),
            _ => Results.StatusCode(500)
        };

        await response.ExecuteAsync(context);
    }
});

// Auth endpoints
app.MapPost("/api/auth/register", async (IMediator mediator, RegisterRequest request) =>
{
    var command = new CreateUserCommand(request.Username, request.Email, HashPassword(request.Password));
    var userId = await mediator.Send(command);
    return Results.Ok(new { Message = "Registration successful", UserId = userId });
})
.WithName("Register")
.WithOpenApi();

app.MapPost("/api/auth/login", async (IMediator mediator, IConfiguration config, LoginRequest request) =>
{
    var query = new GetUserByEmailQuery(request.Email);
    var user = await mediator.Send(query);
    
    if (user == null || !VerifyPassword(request.Password, user.PasswordHash))
        return Results.Unauthorized();

    var token = GenerateJwtToken(user, config);
    return Results.Ok(new { Token = token });
})
.WithName("Login")
.WithOpenApi();

// Task endpoints
app.MapGet("/api/tasks", async (IMediator mediator, ClaimsPrincipal user) =>
{
    var userId = Guid.Parse(user.FindFirst(ClaimTypes.NameIdentifier)?.Value!);
    var query = new GetUserTasksQuery(userId);
    var tasks = await mediator.Send(query);
    return Results.Ok(tasks.Select(t => new UserTaskResponse(t)));
})
.RequireAuthorization()
.WithName("GetTasks")
.WithOpenApi();

app.MapGet("/api/tasks/{id}", async (IMediator mediator, ClaimsPrincipal user, Guid id) =>
{
    var userId = Guid.Parse(user.FindFirst(ClaimTypes.NameIdentifier)?.Value!);
    var query = new GetUserTaskByIdQuery(id, userId);
    var task = await mediator.Send(query);
    return task == null ? Results.NotFound() : Results.Ok(new UserTaskResponse(task));
})
.RequireAuthorization()
.WithName("GetTask")
.WithOpenApi();

app.MapPost("/api/tasks", async (IMediator mediator, ClaimsPrincipal user, CreateTaskRequest request) =>
{
    var userId = Guid.Parse(user.FindFirst(ClaimTypes.NameIdentifier)?.Value!);
    var command = new CreateUserTaskCommand(
        request.Title,
        request.Description,
        Status.Todo,
        Priority.FromString(request.Priority),
        userId
    );
    
    var taskId = await mediator.Send(command);
    var query = new GetUserTaskByIdQuery(taskId, userId);
    var task = await mediator.Send(query);
    
    return Results.Created($"/api/tasks/{taskId}", new UserTaskResponse(task!));
})
.RequireAuthorization()
.WithName("CreateTask")
.WithOpenApi();

app.MapPut("/api/tasks/{id}", async (IMediator mediator, ClaimsPrincipal user, Guid id, UpdateTaskRequest request) =>
{
    var userId = Guid.Parse(user.FindFirst(ClaimTypes.NameIdentifier)?.Value!);
    var command = new UpdateUserTaskCommand(
        id,
        userId,
        request.Title,
        request.Description,
        request.Status,
        request.Priority
    );
    
    await mediator.Send(command);
    var query = new GetUserTaskByIdQuery(id, userId);
    var task = await mediator.Send(query);
    
    return Results.Ok(new UserTaskResponse(task!));
})
.RequireAuthorization()
.WithName("UpdateTask")
.WithOpenApi();

app.MapDelete("/api/tasks/{id}", async (IMediator mediator, ClaimsPrincipal user, Guid id) =>
{
    var userId = Guid.Parse(user.FindFirst(ClaimTypes.NameIdentifier)?.Value!);
    var command = new DeleteUserTaskCommand(id, userId);
    await mediator.Send(command);
    return Results.Ok();
})
.RequireAuthorization()
.WithName("DeleteTask")
.WithOpenApi();

app.MapPatch("/api/tasks/{id}/status", async (IMediator mediator, ClaimsPrincipal user, Guid id, ChangeTaskStatusRequest request) =>
{
    var userId = Guid.Parse(user.FindFirst(ClaimTypes.NameIdentifier)?.Value!);
    var command = new ChangeTaskStatusCommand(id, request.Status);
    var result = await mediator.Send(command);
    
    if (!result.IsSuccess)
        return Results.BadRequest(new { message = result.Error });
    
    var query = new GetUserTaskByIdQuery(id, userId);
    var task = await mediator.Send(query);
    
    return Results.Ok(new UserTaskResponse(task!));
})
.RequireAuthorization()
.WithName("ChangeTaskStatus")
.WithOpenApi();

app.Run();

// Helper methods
static string HashPassword(string password)
{
    using var sha256 = SHA256.Create();
    var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
    return Convert.ToBase64String(hashedBytes);
}

static bool VerifyPassword(string password, string hash)
{
    return HashPassword(password) == hash;
}

static string GenerateJwtToken(User user, IConfiguration config)
{
    var claims = new[]
    {
        new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
        new Claim(ClaimTypes.Email, user.Email),
        new Claim(ClaimTypes.Name, user.Username)
    };

    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["JwtSettings:SecretKey"]!));
    var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
    var expires = DateTime.Now.AddMinutes(Convert.ToDouble(config["JwtSettings:ExpirationInMinutes"]));

    var token = new JwtSecurityToken(
        config["JwtSettings:Issuer"],
        config["JwtSettings:Audience"],
        claims,
        expires: expires,
        signingCredentials: creds
    );

    return new JwtSecurityTokenHandler().WriteToken(token);
}
