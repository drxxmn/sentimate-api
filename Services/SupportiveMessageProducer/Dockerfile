# syntax=docker/dockerfile:1
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# Copy the csproj file and restore as distinct layers
COPY SupportiveMessageProducer/SupportiveMessageProducer.csproj ./SupportiveMessageProducer/
RUN dotnet restore ./SupportiveMessageProducer/SupportiveMessageProducer.csproj

# Copy the entire project and build
COPY SupportiveMessageProducer/ ./SupportiveMessageProducer/
WORKDIR /app/SupportiveMessageProducer
RUN dotnet publish -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/runtime:8.0 AS runtime
WORKDIR /app
COPY --from=build /app/publish .

ENTRYPOINT ["dotnet", "SupportiveMessageProducer.dll"]
