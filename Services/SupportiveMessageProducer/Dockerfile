# syntax=docker/dockerfile:1
FROM --platform=$BUILDPLATFORM mcr.microsoft.com/dotnet/sdk:8.0-alpine AS build

# Copy the entire source code into the container
COPY . /source

# Set the working directory
WORKDIR /source

# Argument for the target architecture
ARG TARGETARCH

# Restore dependencies and build the project
RUN --mount=type=cache,id=nuget,target=/root/.nuget/packages \
    dotnet publish -a ${TARGETARCH/amd64/x64} --use-current-runtime --self-contained false -o /app

# Final stage: use the ASP.NET runtime image
FROM mcr.microsoft.com/dotnet/aspnet:8.0-alpine AS final
WORKDIR /app

# Copy the built application from the previous stage
COPY --from=build /app .

# Optionally set a user (uncomment and set appropriate user if needed)
# USER $APP_UID

# Set the entry point for the application
ENTRYPOINT ["dotnet", "SupportiveMessagesProducer.dll"]
