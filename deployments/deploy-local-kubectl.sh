#!/usr/bin/env bash

kubectl apply --server-side=true -f https://github.com/envoyproxy/gateway/releases/download/latest/install.yaml


kubectl wait --timeout=5m -n envoy-gateway-system deployment/envoy-gateway --for=condition=Available

kubectl -n envoy-gateway-system port-forward service/envoy-gateway 8888:80 &

export ENVOY_SERVICE=$(kubectl get svc -n envoy-gateway-system --selector=gateway.envoyproxy.io/owning-gateway-namespace=default,gateway.envoyproxy.io/owning-gateway-name=eg -o jsonpath='{.items[0].metadata.name}')


kubectl apply -f .
