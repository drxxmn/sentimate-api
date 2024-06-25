from diagrams import Cluster, Diagram, Edge
from diagrams.azure.compute import AKS
from diagrams.azure.database import CosmosDb
from diagrams.azure.devops import Pipelines
from diagrams.onprem.client import Users
from diagrams.onprem.network import Internet
from diagrams.onprem.queue import Rabbitmq
from diagrams.programming.language import Nodejs
from diagrams.onprem.monitoring import Grafana
from diagrams.onprem.network import Nginx

with Diagram("Mood Tracker API Architecture", show=False):

    user = Users("User")

    with Cluster("Kubernetes Cluster"):
        mood_tracker_api = AKS("MoodTracker API")
        supportive_message_producer = AKS("Supportive Message Producer")
        supportive_message_consumer = AKS("Supportive Message Consumer")
        supportive_message_service = AKS("Supportive Message Service")
        mood_mongodb = CosmosDb("MongoDB (MoodTracker)")
        supportive_mongodb_shard = CosmosDb("MongoDB Shard (Supportive Messages)")
        rabbitmq = Rabbitmq("RabbitMQ")
        envoy = Nginx("Envoy Gateway")

        mood_tracker_api - Edge(label="Write Mood") >> mood_mongodb
        mood_tracker_api - Edge(label="Read Mood") << mood_mongodb

        supportive_message_producer - Edge(label="Publish Message") >> rabbitmq
        supportive_message_consumer - Edge(label="Consume Message") << rabbitmq
        supportive_message_consumer - Edge(label="Write Message") >> supportive_mongodb_shard
        supportive_message_service - Edge(label="Read Message") << supportive_mongodb_shard

    with Cluster("External Services"):
        auth0 = Nodejs("Auth0")
        user >> Edge(label="Auth") >> auth0
        auth0 >> Edge(label="Authenticated") >> envoy

    with Cluster("CI/CD Pipeline"):
        cicd = Pipelines("GitHub Actions")
        cicd >> Edge(label="Deploy") >> mood_tracker_api
        cicd >> Edge(label="Deploy") >> supportive_message_producer
        cicd >> Edge(label="Deploy") >> supportive_message_consumer
        cicd >> Edge(label="Deploy") >> supportive_message_service

    internet = Internet("Internet")
    user >> Edge(label="Use") >> internet
    internet >> Edge(label="Route") >> envoy
    envoy >> Edge(label="Route") >> mood_tracker_api
    envoy >> Edge(label="Route") >> supportive_message_producer
    envoy >> Edge(label="Route") >> supportive_message_service

    grafana_cloud = Grafana("Grafana Cloud")
    grafana_cloud >> Edge(label="Monitor") >> mood_tracker_api
    grafana_cloud >> Edge(label="Monitor") >> supportive_message_producer
    grafana_cloud >> Edge(label="Monitor") >> supportive_message_consumer
    grafana_cloud >> Edge(label="Monitor") >> supportive_message_service
