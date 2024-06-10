FROM --platform=$BUILDPLATFORM mcr.microsoft.com/dotnet/sdk:8.0-alpine AS build
COPY . /source
WORKDIR /source
ARG TARGETARCH

RUN --mount=type=cache,id=nuget,target=/root/.nuget/packages \
    dotnet publish -a ${TARGETARCH/amd64/x64} --use-current-runtime --self-contained false -o /out
FROM mcr.microsoft.com/dotnet/aspnet:8.0-alpine AS final
LABEL org.opencontainers.image.source=https://github.com/TsvetislavRangelov/mesher
LABEL org.opencontainers.image.description="Geometry Generator Service (Non-Sampled)"
WORKDIR /out

COPY --from=build /out .
USER $APP_UID

RUN export OTEL_SERVICE_NAME=geometrygeneratornonsampled && \
export OTEL_RESOURCE_ATTRIBUTES="deployment.environment=dev,service.namespace=default,service.version=1,service.instance.id=1" && \
export OTEL_EXPORTER_ENDPOINT="http://grafana-k8s-monitoring-grafana-agent.monitoring.svc.cluster.local:4318" && \
export OTEL_EXPORTER_OTLP_PROTOCOL="http/protobuf"

ENTRYPOINT ["dotnet", "MoodTrackerAPI.dll"]
