from diagrams import Diagram, Cluster, Edge
from diagrams.aws.database import Database
from diagrams.onprem.client import Users
from diagrams.onprem.network import Nginx
from diagrams.onprem.compute import Server
from diagrams.k8s.compute import Pod
from diagrams.k8s.network import Service
from diagrams.k8s.group import Namespace
from diagrams.onprem.container import Docker

with Diagram("MoodTrackerAPI Architecture", show=False):
    users = Users("Users")

    with Cluster("Kubernetes Cluster"):
        nginx = Nginx("Envoy Gateway")

        with Cluster("MoodTracker Namespace"):
            mood_pod = Pod("MoodService")
            user_pod = Pod("UserService")
            support_pod = Pod("SupportService")
            
            mood_svc = Service("MoodService")
            user_svc = Service("UserService")
            support_svc = Service("SupportService")

            mood_pod - Edge(label="Handles Mood Data") - mood_svc
            user_pod - Edge(label="Handles User Data") - user_svc
            support_pod - Edge(label="Handles Support Messages") - support_svc

        mongodb = Database("MongoDB")
        mood_svc - mongodb
        user_svc - mongodb
        support_svc - mongodb

        nginx >> mood_svc
        nginx >> user_svc
        nginx >> support_svc

    users >> nginx
