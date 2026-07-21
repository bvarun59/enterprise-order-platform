# -----------------------------
# Stage 1 - Build
# -----------------------------
FROM mcr.microsoft.com/dotnet/sdk:8.0.423 AS build

WORKDIR /src

COPY . .

RUN dotnet restore src/OrderService.API/OrderService.API.csproj

RUN dotnet publish src/OrderService.API/OrderService.API.csproj \
    -c Release \
    -o /app/publish

# -----------------------------
# Stage 2 - Runtime
# -----------------------------
FROM mcr.microsoft.com/dotnet/aspnet:8.0.29

WORKDIR /app

COPY --from=build /app/publish .

ENV ASPNETCORE_URLS=http://+:8080

EXPOSE 8080

ENTRYPOINT ["dotnet","OrderService.API.dll"]