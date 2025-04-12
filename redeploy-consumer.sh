#!/bin/bash

set -e

echo "🔨 Baue Docker-Image..."
docker build -t sfr-consumer:dev -f Consumer/Dockerfile .

echo "📦 Lade Image in kind..."
kind load docker-image sfr-consumer:dev

echo "🧱 Erstelle Deployment neu..."
kubectl apply -f sfr-k8s/sfr/consumer-deployment.yaml

echo "🚀 Restart Deployment..."
kubectl rollout restart deployment sfr-consumer

echo "Logs..."
kubectl logs deploy/sfr-consumer -f

echo "✅ Done!"