# Tasked

A clean architecture-based task management system built with .NET.

## Features

- Task management with status tracking (Todo, InProgress, Done)
- Priority-based task organization
- User-specific task assignments
- Domain-driven design implementation
- Clean architecture principles
- Event-driven architecture for status changes

## Project Structure

The solution follows clean architecture principles with three main layers:

### Domain Layer
- Contains core business logic
- Entities (UserTask, User)
- Value Objects (Status, Priority)
- Domain Events
- Repository Interfaces

### Application Layer
- Use Cases (CQRS pattern with MediatR)
- DTOs
- Interfaces for external services

### Infrastructure Layer
- Data Access Layer (DAL)
- API Controllers
- Database Context
- Repository Implementations

## Getting Started

### Prerequisites
- .NET 8.0 SDK
- SQL Server (or compatible database)

### Setup
1. Clone the repository
2. Update connection string in `appsettings.json`
3. Run database migrations
4. Start the application

## Architecture

The project follows clean architecture principles:
- Domain layer is independent of other layers
- Application layer depends only on Domain
- Infrastructure layer implements interfaces defined in Domain and Application layers
- Dependencies point inward

## Status Management

Tasks can have one of three statuses:
- Todo
- InProgress
- Done

Status changes are tracked through domain events, allowing for:
- Audit logging
- Notifications
- Integration with other systems

## Contributing

1. Fork the repository
2. Create a feature branch
3. Commit your changes
4. Push to the branch
5. Create a Pull Request 