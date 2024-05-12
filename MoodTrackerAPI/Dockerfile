# Use the official Microsoft ASP.NET Core image.
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80

# Use SDK image to build the application.
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["MoodTrackerAPI.csproj", "./"]
RUN dotnet restore "MoodTrackerAPI.csproj"
COPY . .
WORKDIR "/src/"
RUN dotnet build "MoodTrackerAPI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "MoodTrackerAPI.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MoodTrackerAPI.dll"]
