FROM mcr.microsoft.com/dotnet/runtime:8.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["SupportiveMessageConsumer/SupportiveMessageConsumer.csproj", "SupportiveMessageConsumer/"]
RUN dotnet restore "SupportiveMessageConsumer/SupportiveMessageConsumer.csproj"
COPY . .
WORKDIR "/src/SupportiveMessageConsumer"
RUN dotnet build "SupportiveMessageConsumer.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "SupportiveMessageConsumer.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SupportiveMessageConsumer.dll"]
