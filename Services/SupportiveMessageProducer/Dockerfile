FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["SupportiveMessageProducer/SupportiveMessageProducer.csproj", "SupportiveMessageProducer/"]
RUN dotnet restore "SupportiveMessageProducer/SupportiveMessageProducer.csproj"
COPY . .
WORKDIR "/src/SupportiveMessageProducer"
RUN dotnet build "SupportiveMessageProducer.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "SupportiveMessageProducer.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SupportiveMessageProducer.dll"]
