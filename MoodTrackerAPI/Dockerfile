# Define the base image for the SDK to build the project
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy csproj file and restore any dependencies (via NuGet)
COPY MoodTrackerAPI.csproj .
RUN dotnet restore

# Copy the remaining source files from your project into the Docker image
COPY . .

# Publish the application to the directory for running the app
RUN dotnet publish -c Release -o /app/publish

# Define the base image for the runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "MoodTrackerAPI.dll"]
