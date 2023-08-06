Project README

Overview
--------
This document guides you through the setup and execution process of the project, including building the Docker image, adjusting the IP configuration, and running the necessary commands for successful deployment.

Prerequisites
-------------
Before running the project, you must create the Docker image by taking a Docker build.

Important Notice:
Make sure the IP in the appsettings.json file corresponds to the IP of the machine where the project will be run.

Build and Run Instructions
--------------------------
1. Build the Docker Compose:
   docker-compose -f docker-compose-build.yml build

2. Run RabbitMQ Container:
   docker run -d --restart unless-stopped --name rabbitmq -p 5672:5672 -p 15672:15672 rabbitmq:3.11.4-management

3. Start the Project using Docker Compose:
   docker-compose -f docker-compose-run.yml up -d

API Usage Examples
------------------
You can interact with the URL shortener service using the following Curl commands:

- Create Short URL without Custom Alias:
  curl --location 'http://localhost:5218/api/UrlShortener/Shorten' --header 'Content-Type: application/json' --data '{"LongUrl": "http://localhost:5218/swagger/index.html","CustomAlias": null}'

- Create Short URL with Custom Alias:
  curl --location 'http://localhost:5218/api/UrlShortener/Shorten' --header 'Content-Type: application/json' --data '{"LongUrl": "http://localhost:5218/swagger/index.html","CustomAlias": "MERCEDES"}'

- Redirect to URL Using Custom Alias:
  curl --location 'http://localhost:5218/api/UrlShortener/Redirect/MERCEDES'

Conclusion
----------
This README provides the necessary steps and examples to set up and interact with the project. Please ensure you follow the instructions closely and adapt the configuration as per your system requirements.
