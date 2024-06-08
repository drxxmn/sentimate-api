# Use the .NET SDK image as a build environment
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy the project file and restore dependencies
COPY SupportiveMessageConsumer/SupportiveMessageConsumer.csproj SupportiveMessageConsumer/
RUN dotnet restore SupportiveMessageConsumer/SupportiveMessageConsumer.csproj

# Copy the rest of the source code and build the application
COPY SupportiveMessageConsumer/ SupportiveMessageConsumer/
WORKDIR /src/SupportiveMessageConsumer
RUN dotnet build SupportiveMessageConsumer.csproj -c Release -o /app/build

# Publish the application
RUN dotnet publish SupportiveMessageConsumer.csproj -c Release -o /app/publish

# Use the ASP.NET Core runtime image
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "SupportiveMessageConsumer.dll"]
