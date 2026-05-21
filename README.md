# Task Management System Backend

Backend implementation using .NET 8 Clean Architecture, EF Core (SQLite), and JWT Authentication with HttpOnly cookie support.

## Architecture Overview

The project is divided into four layers:
1.  **Domain**: Core entities and business rules (POCOs, Enums).
2.  **Application**: Application business logic, Interfaces, DTOs, Mapping and Exceptions.
3.  **Infrastructure**: External concerns like Database (EF Core), JWT Implementation and Repositories.
4.  **Api**: Entry point, Controllers and Middleware.

## Features

- **Clean Architecture**: Separation of concerns for maintainability and testability.
- **JWT Security**: Secure authentication. Tokens are supported via both `Authorization` header and `HttpOnly` cookies.
- **Repository Pattern**: Abstracted data access with a generic repository and Unit of Work.
- **Ownership Enforcement**: Users can only access and modify their own tasks.
- **Global Error Handling**: Middleware to capture exceptions and return standard JSON responses.
- **AutoMapper**: Clean transformation between Domain Entities and DTOs.
- **SQLite Support**: Runs immediately without requiring external database servers.

## Prerequisites

- .NET 8 SDK (https://dotnet.microsoft.com/download/dotnet/8.0)
- Visual Studio 2022 or VS Code

## Setup & Installation

1.  Clone the repository
2.  Update connection string 
3.  Run the webapi
4.  Go to TaskMgmt.Client folder
5.  Open the folder in cmd and run:
      > npm install
	  > npm run start