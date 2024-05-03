# Use the official ASP.NET Core runtime image as a parent image
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

# Install curl
RUN apt-get update && apt-get install -y curl

# Use the .NET SDK image to build the app
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
# Copy csproj and restore any dependencies (via NuGet)
COPY ["MoodTrackerAPI.csproj", "./"]
RUN dotnet restore "MoodTrackerAPI.csproj"

# Copy the project files and build your release
COPY . .
RUN dotnet build "MoodTrackerAPI.csproj" -c Release -o /app/build

# Publish the application
FROM build AS publish
RUN dotnet publish "MoodTrackerAPI.csproj" -c Release -o /app/publish

# Final stage/image that creates the runtime image
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MoodTrackerAPI.dll"]


