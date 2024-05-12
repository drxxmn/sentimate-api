# Use the official image as a parent image
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

# Set the working directory
WORKDIR /src

# Copy the project file into the container at /src
COPY ["MoodTrackerAPI.csproj", "./"]

# Restore dependencies
RUN dotnet restore "MoodTrackerAPI.csproj"

# Copy the rest of your app's source code from your host to your image filesystem.
COPY . .

# Build the application
RUN dotnet build "MoodTrackerAPI.csproj" -c Release -o /app/build

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
COPY --from=build /app/build .
ENTRYPOINT ["dotnet", "MoodTrackerAPI.dll"]
