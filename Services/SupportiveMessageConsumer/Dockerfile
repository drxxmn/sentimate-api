# syntax=docker/dockerfile:1
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# Copy the csproj file and restore as distinct layers
COPY Services/SupportiveMessageConsumer/*.csproj ./SupportiveMessageConsumer/
RUN ls -la /app # Debugging: List files in /app after copying .csproj
RUN dotnet restore ./SupportiveMessageConsumer/SupportiveMessageConsumer.csproj

# Copy the entire project and build
COPY Services/SupportiveMessageConsumer/. ./SupportiveMessageConsumer/
RUN ls -la /app/SupportiveMessageConsumer # Debugging: List files in /app/SupportiveMessageConsumer after copying project files
WORKDIR /app/SupportiveMessageConsumer
RUN dotnet publish -c Release -o /app/publish
RUN ls -la /app/publish # Debugging: List files in /app/publish after publishing

FROM mcr.microsoft.com/dotnet/runtime:8.0 AS runtime
WORKDIR /app
COPY --from=build /app/publish .
RUN ls -la /app # Debugging: List files in /app after copying from build stage

ENTRYPOINT ["dotnet", "SupportiveMessageConsumer.dll"]
