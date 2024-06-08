# Use the .NET SDK to build the application
FROM mcr.microsoft.com/dotnet/sdk:8.0-alpine AS build
WORKDIR /source

# Copy the .csproj file and restore dependencies
COPY *.csproj ./
RUN dotnet restore

# Copy the remaining source code and build the project
COPY . ./
RUN dotnet publish -c Release -o /app

# Use the .NET runtime to run the application
FROM mcr.microsoft.com/dotnet/runtime:8.0-alpine AS runtime
WORKDIR /app
COPY --from=build /app ./
ENTRYPOINT ["dotnet", "SupportiveMessageConsumer.dll"]
