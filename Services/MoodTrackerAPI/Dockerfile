# Use the official image as a parent image
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

# Set the working directory
WORKDIR /src

# Copy the project file into the container at /src
COPY ["Services/MoodTrackerAPI/MoodTrackerAPI.csproj", "./"]

# Restore dependencies
RUN dotnet restore "MoodTrackerAPI.csproj"

# Copy the rest of your app's source code from your host to your image filesystem.
COPY Services/MoodTrackerAPI/. .

# Build the application
RUN dotnet build "MoodTrackerAPI.csproj" -c Release -o /app/build

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
COPY --from=build /app/build .

RUN export OTEL_SERVICE_NAME=geometrygeneratornonsampled && \
export OTEL_RESOURCE_ATTRIBUTES="deployment.environment=dev,service.namespace=default,service.version=1,service.instance.id=1" && \
export OTEL_EXPORTER_ENDPOINT="http://grafana-k8s-monitoring-grafana-agent.monitoring.svc.cluster.local:4318" && \
export OTEL_EXPORTER_OTLP_PROTOCOL="http/protobuf"

ENTRYPOINT ["dotnet", "MoodTrackerAPI.dll"]
