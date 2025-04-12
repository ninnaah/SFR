
# 🚀 SFR Deployment Anleitung (Docker + Kubernetes/kind)

Diese Schritte bitte genau einhalten, um alle Services erfolgreich lokal zu starten:

---

## Schritt 1: Docker Compose (Kafka & Schema Registry starten)

**Wichtig:** Consumer & Postgres sind **nicht** mehr in Docker Compose, diese laufen im Kubernetes!

```bash
docker compose up -d
```

Warte, bis Kafka und Schema Registry vollständig gestartet sind.
1. Dann einmal Producer starten, damit das topic bei kafka erstellt wird
2. Dann Streamprocessor noch mal starten, damit das zweite topic erstellt wird

---

## Schritt 2: Kubernetes Cluster starten (kind)

Erstelle den Cluster lokal, falls nicht schon geschehen:

```bash
kind create cluster
```

---

## Schritt 3: Postgres in Kubernetes starten

```bash
kubectl apply -f sfr-k8s/sfr/postgres/postgres-deployment.yaml
```

Dadurch startet PostgreSQL direkt im Kubernetes Cluster.

---

## Schritt 4: Kafka und Schema Registry für Kubernetes verfügbar machen

Lege Kubernetes-Service an, der auf externes Docker (Kafka und Schema Registry) verweist:

```bash
kubectl apply -f sfr-k8s/sfr/kafka/kafka-service.yaml
```

---

## Schritt 5 & 6 : Docker-Image für Consumer bauen, laden und deployen

ich hab den Prozess so oft wiederholen müssen, deswegen hab ich ein .sh script erstellt. Auf root einfach die redeploy-consumer.sh ausführen.

```bash
./redeploy-consumer.sh     
```
---

## Schritt 7: Logs prüfen

Überprüfe abschließend die Logs des Consumers:

```bash
kubectl logs deploy/sfr-consumer -f
```

✅ **Wenn alles läuft**, siehst du erfolgreiche Kafka-Consumptions.

---

## 🔄 Redeployment bei Änderungen (Skript)

Nutze folgendes Skript zur schnellen Neuverteilung bei Code-Änderungen:

```bash
./redeploy-consumer.sh
```

---

## 🛠 Services prüfen (optional)

Übersicht aller Kubernetes Services:

```bash
kubectl get services
```

Übersicht aller laufenden Pods:

```bash
kubectl get pods
```

---
