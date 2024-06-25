from diagrams import Diagram, Cluster
from diagrams.azure.devops import Repos, Pipelines, TestPlans, Artifacts
from diagrams.onprem.ci import Jenkins

with Diagram("CI Pipeline (via GitHub Actions)", filename="pipeline_diagram", direction="LR"):
    with Cluster("Triggered on every commit to master", direction="LR"):
        login = Repos("Login to GHCR")
        setup = Pipelines("Setup Docker Buildx")
        build = Jenkins("Build images")
        test = TestPlans("Run tests")
        publish = Artifacts("Publish to GHCR")
        cloudflare_deploy = Jenkins("Deploy to CloudFlare")

        login >> setup >> build >> test >> publish >> cloudflare_deploy
