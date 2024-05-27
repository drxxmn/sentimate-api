from diagrams import Diagram, Cluster, Edge
from diagrams.onprem.vcs import Github
from diagrams.onprem.ci import GithubActions
from diagrams.onprem.container import Docker
from diagrams.onprem.client import Users
from diagrams.k8s.compute import Pod
from diagrams.k8s.network import Service
from diagrams.k8s.group import Namespace
from diagrams.generic.compute import Rack
from diagrams.generic.network import VPN
from diagrams.generic.os import Ubuntu

with Diagram("CI/CD Pipelines", show=False):
    users = Users("Developers")

    with Cluster("Source Control"):
        github = Github("GitHub Repository")

    with Cluster("Pipeline 1: .NET CI/CD Pipeline"):
        github_actions_1 = GithubActions("GitHub Actions")
        docker_build = Docker("Docker Build")
        ghcr = Docker("GitHub Container Registry")

    with Cluster("Pipeline 2: Deploy to VM"):
        github_actions_2 = GithubActions("GitHub Actions")
        vpn = VPN("VPN")
        vm = Ubuntu("VM with Kubernetes")
        k8s_files = Pod("Kubernetes Files")

    with Cluster("Pipeline 3: Build and Deploy to Azure Web App"):
        github_actions_3 = GithubActions("GitHub Actions")
        dotnet_build = Docker("Build .NET")
        azure_webapp = Rack("Azure Web App")

    users >> github

    github >> github_actions_1
    github_actions_1 >> docker_build
    docker_build >> ghcr

    github >> github_actions_2
    github_actions_2 >> vpn
    vpn >> vm
    vm >> k8s_files
    k8s_files >> vm

    github >> github_actions_3
    github_actions_3 >> dotnet_build
    dotnet_build >> azure_webapp
