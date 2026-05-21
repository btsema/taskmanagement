# ?? Build stage ???????????????????????????????????????????????????????????????
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy solution and project files first to leverage layer caching on restore
COPY TaskMgmt.sln ./
COPY TaskMgmt.Api/TaskMgmt.Api.csproj                         TaskMgmt.Api/
COPY TaskMgmt.Application/TaskMgmt.Application.csproj         TaskMgmt.Application/
COPY TaskMgmt.Infrastructure/TaskMgmt.Infrastructure.csproj   TaskMgmt.Infrastructure/
COPY TaskMgmt.Domain/TaskMgmt.Domain.csproj                   TaskMgmt.Domain/

RUN dotnet restore TaskMgmt.Api/TaskMgmt.Api.csproj

# Copy the rest of the source and publish
COPY . .
RUN dotnet publish TaskMgmt.Api/TaskMgmt.Api.csproj \
    -c Release \
    -o /app/publish \
    --no-restore

# ?? Runtime stage ?????????????????????????????????????????????????????????????
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app

# Create a volume-friendly directory for the SQLite database file
RUN mkdir -p /app/data

# Copy published output
COPY --from=build /app/publish .

# Environment overrides (can be set at runtime via docker run -e or compose)
ENV ASPNETCORE_URLS=http://+:8080
ENV ASPNETCORE_ENVIRONMENT=Production
ENV ConnectionStrings__DefaultConnection="Data Source=/app/data/TaskManagementDb"

EXPOSE 8080

ENTRYPOINT ["dotnet", "TaskMgmt.Api.dll"]
