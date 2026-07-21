dotnet clean UserService.sln

dotnet build UserService.sln

dotnet test UserService.sln


dotnet run --project src/UserService.API

cd src/UserService.Infrastructure

dotnet add package Microsoft.EntityFrameworkCore --version 8.0.7

dotnet add package Microsoft.EntityFrameworkCore.Design --version 8.0.7

dotnet add package Microsoft.EntityFrameworkCore.Relational --version 8.0.7

dotnet add package Npgsql.EntityFrameworkCore.PostgreSQL --version 8.0.4

dotnet list package

Microsoft.EntityFrameworkCore
Microsoft.EntityFrameworkCore.Design
Microsoft.EntityFrameworkCore.Relational
Npgsql.EntityFrameworkCore.PostgreSQL


mkdir ProductService
cd ProductService

dotnet new sln -n ProductService

mkdir src
mkdir tests

cd src

dotnet new webapi -n ProductService.API

dotnet new classlib -n ProductService.Domain

dotnet new classlib -n ProductService.Infrastructure

cd ..

cd tests

dotnet new xunit -n ProductService.UnitTests


docker run -d --name us -e ASPNETCORE_ENVIRONMENT=Development -p 3030:8080 userservice:v1


In a production environment, we would typically automate database creation using one of these approaches:

EF Core migrations executed by the application during startup.
An initialization SQL script mounted into the PostgreSQL container.
Infrastructure as Code (Terraform/CloudFormation) for managed databases like Amazon RDS.

We'll discuss those approaches later. For now, manually creating the databases keeps things simple and lets us focus on Docker networking.
