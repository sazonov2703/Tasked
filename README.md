# Tasked

Multi-user online task management service built on the principles of clean architecture on the .NET platform.

## Features

- Task management with status tracking (Todo, InProgress, Done)

- Organization of tasks by priority

- Assigning tasks to specific users

- Implementing Domain-Driven Design (DDD)

- Clean architecture

- Event-driven architecture for tracking status changes

## Project Structure

The solution is built on the principles of clean architecture and includes three main layers:

### Domain (Domain layer)
- Main business logic
- Entities (UserTask, User)
- Value objects (Status, Priority)
- Domain events
- Repository interfaces

### Application (Application layer)
- Use Cases (CQRS with MediatR is used)
- DTO (Data Transfer Objects)
- Interfaces for external services
  
### Infrastructure
- Data Access Layer (DAL)
- API Controllers
- Database Context
- Repository Implementations

## Getting Started
### Prerequisites
- .NET SDK 8.0
- SQL Server (or compatible database)

### Installation
- Clone the repository
- Update the connection string in appsettings.json
- Apply database migrations
- Run the application

## Architecture

The project follows the principles of clean architecture:
- The domain layer does not depend on others
- The application layer depends only on the domain
- The infrastructure implements interfaces defined in the domain and application
- All dependencies are directed inward

## Status management

Tasks can have one of three statuses:
- Todo
- InProgress
- Done

Status changes are tracked using domain events, which allows you to:
- Log
- Send notifications
- Integrate with external systems

# Contribute to the project
1. Fork the repository
2. Create a new branch with a feature
3. Commit the changes
4. Send the branch to the repository
5. Create a Pull Request
