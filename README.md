RythuMitraAI - Solution scaffold

This repository contains a Clean Architecture scaffold for an ASP.NET Core 8 Web API application.

Projects:
- RythuMitraAI.API - ASP.NET Core Web API project
- RythuMitraAI.Application - Application layer (interfaces, DTOs, mapping, validators)
- RythuMitraAI.Domain - Domain entities
- RythuMitraAI.Infrastructure - Infrastructure (EF Core, repositories, unit of work)
- RythuMitraAI.Tests - Unit test project (xUnit)

Notes:
- Replace JWT key in appsettings.json
- Update EF Core package versions to stable when available
- Add business logic to Application layer and entities to Domain layer
