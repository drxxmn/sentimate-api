# syntax=docker/dockerfile:1
FROM --platform=$BUILDPLATFORM mcr.microsoft.com/dotnet/sdk:8.0-alpine AS build

WORKDIR /source

COPY ./Services/SupportiveMessageConsumer/*.csproj .
RUN dotnet restore

COPY ./Services/SupportiveMessageConsumer/. .
RUN dotnet publish -c Release -o /app

FROM mcr.microsoft.com/dotnet/runtime:8.0-alpine AS final
WORKDIR /app
COPY --from=build /app .
ENTRYPOINT ["dotnet", "SupportiveMessageConsumer.dll"]
