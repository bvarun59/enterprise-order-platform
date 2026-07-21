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