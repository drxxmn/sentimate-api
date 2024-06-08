# syntax=docker/dockerfile:1
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# Copy the csproj file and restore as distinct layers
COPY SupportiveMessageConsumer/*.csproj ./SupportiveMessageConsumer/
RUN dotnet restore ./SupportiveMessageConsumer/SupportiveMessageConsumer.csproj

# Copy the entire project and build
COPY SupportiveMessageConsumer/. ./SupportiveMessageConsumer/
WORKDIR /app/SupportiveMessageConsumer
RUN dotnet publish -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/runtime:8.0 AS runtime
WORKDIR /app
COPY --from=build /app/publish .

ENTRYPOINT ["dotnet", "SupportiveMessageConsumer.dll"]
