# Sentimate API

Welcome to the **Sentimate API** project! This repository contains the backend API for the Sentimate application, a mood-tracking platform designed with security, scalability, and compliance in mind. The application uses ASP.NET Core, MongoDB, Kubernetes, and follows modern development practices including distributed data handling, GDPR compliance, and security by design.

## Table of Contents

- [Features](#features)
- [Tech Stack](#tech-stack)
- [Architecture](#architecture)
- [Getting Started](#getting-started)
- [Usage](#usage)
- [Scaling and Distributed Data](#scaling-and-distributed-data)
- [Security Measures](#security-measures)
- [Contributing](#contributing)
- [License](#license)

## Features

- **Mood Tracking**: Add, retrieve, update, and delete mood entries.
- **GDPR Compliance**: Allows users to delete all their personal data.
- **Authentication and Authorization**: Uses Auth0 for secure user authentication and JWT-based authorization.
- **Distributed Data**: Scalable database design with MongoDB and Kubernetes.
- **Horizontal Pod Autoscaling (HPA)**: Automatically scales based on traffic and resource usage.
- **Monitoring and Metrics**: Integrated with Prometheus and Grafana for performance monitoring.
- **Security by Design**: Incorporates OWASP best practices, data encryption, and secure session management.

## Tech Stack

- **Backend**: ASP.NET Core
- **Database**: MongoDB
- **Containerization**: Docker
- **Orchestration**: Kubernetes (MicroK8s)
- **Authentication**: Auth0
- **Monitoring**: Prometheus, Grafana
- **Load Testing**: k6

## Architecture

The application is designed with microservices principles:

- **Mood Service**: Handles mood entries (CRUD operations).
- **Authentication**: Secures the API using JWT tokens from Auth0.
- **Supportive Messages**: Separate service for user engagement.
- **Horizontal Scaling**: Kubernetes Horizontal Pod Autoscaler (HPA) scales services based on resource usage.

## Getting Started

### Prerequisites

- Docker
- Kubernetes (MicroK8s recommended)
- .NET SDK 8.0
- MongoDB
- Auth0 account
- Prometheus and Grafana (for monitoring)

### Installation

1. Clone the repository:
   ```bash
   git clone https://github.com/drxxmn/sentimate-api.git
   cd sentimate-api
   ```

2. Build the Docker image:
   ```bash
   docker build -t sentimate-api .
   ```

3. Push the Docker image to a container registry:
   ```bash
   docker tag sentimate-api:latest your-dockerhub-username/sentimate-api:latest
   docker push your-dockerhub-username/sentimate-api:latest
   ```

4. Apply Kubernetes configurations:
   ```bash
   kubectl apply -f k8s-files/moodtrackerapi-deployment.yaml
   kubectl apply -f k8s-files/moodtrackerapi-hpa.yaml
   ```

5. Enable monitoring:
   ```bash
   microk8s enable metrics-server
   ```

## Usage

### API Endpoints

#### Mood Management

- **GET /api/mood**: Retrieve all mood entries for the authenticated user.
- **POST /api/mood**: Add or update a mood entry for the current date.
- **DELETE /api/mood/{id}**: Delete a specific mood entry by ID.
- **DELETE /api/mood/deleteAll**: Delete all mood entries for the authenticated user (GDPR compliance).

### Testing

Use `k6` for load testing:

1. Install k6:
   ```bash
   sudo apt install k6
   ```

2. Run a load test:
   ```bash
   k6 run loadtest.js
   ```

## Scaling and Distributed Data

- **Horizontal Pod Autoscaler (HPA)**: Scales the number of pods based on CPU utilization.
- **Sharding and Partitioning**: MongoDB is configured to handle distributed datasets efficiently.
- **Redis Caching** (Planned): Improve response times by caching frequently accessed data.

## Security Measures

- **Authentication**: Secure authentication using Auth0 and JWT tokens.
- **Data Encryption**: AES-256 for data at rest, TLS/SSL for data in transit.
- **Input Validation**: Prevents common vulnerabilities like SQL injection and XSS.
- **Audit Logs**: Logs all access and modification activities.
- **GDPR Compliance**: Allows users to delete their data securely.

