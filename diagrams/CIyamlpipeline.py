from diagrams import Diagram, Cluster, Edge
from diagrams.azure.devops import Repos, Pipelines
from diagrams.onprem.ci import Jenkins
from diagrams.custom import Custom

with Diagram("CI Pipeline", filename="ci_pipeline_diagram", direction="LR"):
    trigger = Repos("Push to Main Branch")

    with Cluster("Build and Push Images"):
        mood_tracker = Jenkins("MoodTrackerAPI")
        supportive_consumer = Jenkins("SupportiveMessageConsumer")
        supportive_producer = Jenkins("SupportiveMessageProducer")
        supportive_service = Jenkins("SupportiveMessageService")

    checkout = Pipelines("Checkout")
    login = Pipelines("Login to GHCR")
    build_push = Pipelines("Build and Push Image")

    services = [mood_tracker, supportive_consumer, supportive_producer, supportive_service]

    trigger >> checkout >> login >> build_push
    for service in services:
        service - Edge(color="darkgreen") - build_push
