# Use the .NET SDK image for building the application
FROM --platform=$BUILDPLATFORM mcr.microsoft.com/dotnet/sdk:8.0-alpine AS build

# Set the working directory to /source
WORKDIR /source

# Copy the project files to the /source directory
COPY . .

# Build and publish the application
ARG TARGETARCH
RUN --mount=type=cache,id=nuget,target=/root/.nuget/packages \
    dotnet publish -c Release -a ${TARGETARCH/amd64/x64} --use-current-runtime --self-contained false -o /app

# Use the ASP.NET runtime image
FROM mcr.microsoft.com/dotnet/aspnet:8.0-alpine AS final

# Set the working directory to /app
WORKDIR /app

# Copy the published application from the build stage to the final stage
COPY --from=build /app .

# Run the application as a non-root user (ensure APP_UID is set appropriately in your environment)
USER $APP_UID

# Set the entry point to the application's DLL
ENTRYPOINT ["dotnet", "SupportiveMessageProducer.dll"]
