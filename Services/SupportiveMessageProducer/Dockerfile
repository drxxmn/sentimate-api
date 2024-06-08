# syntax=docker/dockerfile:1
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# Copy the csproj file and restore as distinct layers
COPY Services/SupportiveMessageProducer/*.csproj ./SupportiveMessageProducer/
RUN ls -la /app # Debugging: List files in /app after copying .csproj
RUN dotnet restore ./SupportiveMessageProducer/SupportiveMessageProducer.csproj

# Copy the entire project and build
COPY Services/SupportiveMessageProducer/. ./SupportiveMessageProducer/
RUN ls -la /app/SupportiveMessageProducer # Debugging: List files in /app/SupportiveMessageProducer after copying project files
WORKDIR /app/SupportiveMessageProducer
RUN dotnet publish -c Release -o /app/publish
RUN ls -la /app/publish # Debugging: List files in /app/publish after publishing

FROM mcr.microsoft.com/dotnet/runtime:8.0 AS runtime
WORKDIR /app
COPY --from=build /app/publish .
RUN ls -la /app # Debugging: List files in /app after copying from build stage

ENTRYPOINT ["dotnet", "SupportiveMessageProducer.dll"]
