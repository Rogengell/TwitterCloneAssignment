# **TwitterCloneAssignment** :tada:

# Assignment #1 :memo:

This project aims to develop a Twitter-like platform, structured into four key stages. We will begin with the creation of an architecture diagram, followed by the implementation of two interconnected microservices. This overview will outline each step briefly, providing a clear understanding of our process. For a comprehensive overview a [Figma Link](https://www.figma.com/board/yup3d434pA2f6q1tH27IRE/Tweed-Application?node-id=0-1&node-type=canvas)

## **The architectual diagram**

![architectureBoundary](https://github.com/Rogengell/TwitterCloneAssignment/blob/main/Diagrams/architectureBoundary.png)
![architectureDiagram](https://github.com/Rogengell/TwitterCloneAssignment/blob/main/Diagrams/architectureDiagram.png)

As you can clearly see, we have implemented three central microservices in our system.

1 **Account Management**: This service takes care of all functions related to the creation and management of user accounts. This includes registering new users, updating profile information and searching for users. In addition, the service also enables advanced search functions, such as finding users or content using tags.

2 **Messenger Manager**: The second service is responsible for creating and managing messages (tweets). Here, users can not only create new tweets, but also search for existing messages. This service also enables the creation and management of comments on the respective tweets, which promotes interactive communication between users.

3 **Notification Manager**: The third service ensures that users are informed about activities related to their tweets. This includes notifications about new comments and reactions to their posts. This keeps users up to date and allows them to respond to interactions in a timely manner.

These three microservices work together seamlessly to create a comprehensive and user-friendly platform that meets the needs of our users.

## Development Strategy :rocket:

for a quick understanding we made a C4 model up to level 2

**Level 1 C4 Model**

![lvl1](https://github.com/Rogengell/TwitterCloneAssignment/blob/main/Diagrams/level%201.png)

**Level 2 C4 Model**

![lvl2](https://github.com/Rogengell/TwitterCloneAssignment/blob/main/Diagrams/level%202.png)

We chose to implement an API gateway instead of a messaging system like RabbitMQ for several key reasons:

**Centralized Control:** The API gateway provides a single point of entry for clients, simplifying administration and communication within the system.

**Enhanced Security:** It allows for the integration of features such as authentication, authorization, rate limiting, and API key management, which are crucial for our decision.

**Improved Performance:** The gateway enables performance enhancements through caching and load balancing, facilitating scalability.

**Simplified Client Interactions:** Clients benefit from a unified API interface, reducing complexity in interactions.

While we prioritized security and simplified interactions with the API gateway, we recognize the advantages of RabbitMQ for other microservices that focus on communication and notifications. RabbitMQ is better suited for loose coupling and asynchronous communication, particularly in complex workflows or event-driven architectures.

Ultimately, a hybrid approach that combines both the API gateway and RabbitMQ could be beneficial. The API gateway can manage requests, while RabbitMQ handles asynchronous communication between services.

## Implementation :sparkles:

This is a brief overview of the technologies we utilized and the reasons we believe they will benefit our project.
**Docker:** We package each service into Docker containers to ensure consistent environments and facilitate easier deployment.

**Entity Framework (EF):** We use EF for database implementation and management, aiming for a uniform database with every deployment to maintain system reliability.

**.NET Core & REST:** REST is an effective protocol for enabling two-way communication through requests and responses.

**Dependency Injection:** This approach fosters loose coupling between components, enhancing code readability and testability. It allows us to inject services such as data access and business logic into our controllers.

**XUnit (Testing):** As we had no prior experience with testing in .NET, we utilized available resources for mocking and testing. This proved to be a valuable learning experience, and we plan to use these tools in future projects.

## Setup :bookmark:

To test the program in Swagger, follow these steps:

1. **Build the Project:**
   ```
   dotnet build
   ```
2. **Start the Docker Containers:**
   ```
   docker compose up -d
   ```
3. **Access Swagger UI:**
   Open your web browser and navigate to:
   ```
   localhost:8080/swagger
   ```

This will launch Swagger UI, where you can explore and test your API endpoints.

# Assignment #2 :memo:

## Week 44 implementing Gateway :see_no_evil:

We've already implemented a gateway in our system to facilitate communication and provide a single point of entry for users. This centralized approach enhances security by consolidating security measures. Additionally, the gateway improves scalability by efficiently handling increased traffic and optimizes performance through load distribution across backend services. It also simplifies API management by providing a centralized platform for managing and exposing APIs, as we will explore in more detail in future implementations.

## Week 45 implementing some reliability :ambulance:

we implemented a retry policy because it can significantly improve the reliability of our microservices architecture. By automatically retrying failed requests, we can handle temporary issues like network glitches or service overloads, but we have to keep in mind that we have to fail fast. Thats why it's good practis that we have to use a circuit breaker pattern to prevent excessive retries and protect against cascading failures. This helps us to temporarily halt retries when a service is consistently failing, allowing it to recover and preventing resource exhaustion.

## Week 46 Kubernetes :technologist:

This is need to have acces to the dashbord

```
kubectl apply -f https://raw.githubusercontent.com/kubernetes/dashboard/v2.7.0/aio/deploy/recommended.yaml
```

```
kubectl create sa webadmin -n kubernetes-dashboard
kubectl create clusterrolebinding webadmin --clusterrole=cluster-admin --serviceaccount=kubernetes-dashboard:webadmin
```

```
kubectl create token webadmin -n kubernetes-dashboard
```

```
kubectl proxy
```

```
http://localhost:8001/api/v1/namespaces/kubernetes-dashboard/services/https:kubernetes-dashboard:/proxy/

```

To deploy our services and jobs to Kubernetes, we built Docker images for each component. Here are the common commands used to create these images:

```
docker build -f LoginServiceApi/Dockerfile -t my-loginserviceapi .
```

```
docker build -f UserServiceApi/Dockerfile -t my-userserviceapi .
```

```
docker build -f ApiGateWay/Dockerfile -t my-apigateway .
```

```
docker build -f EFramework/Migrations.Dockerfile -t my-ef-migration .
```

After building the Docker images, we deployed them to our Kubernetes cluster using the following command:

```
kubectl apply -f user-db-volume.yml
```

```
kubectl apply -f user-db.yml
```

```
kubectl apply -f vault-data-pvc.yaml
```

```
kubectl apply -f vault-deployment.yaml
```

**REMEMBER LS AND NOT CRLF, IT RUNS ON LINUX NOW WINDOWS**

```
kubectl create configmap fluentd-config --from-file=fluentd.conf
```

```
kubectl apply -f fluentd-deployment.yaml
```

Now before we go further, we want to setup Vault.

```
http://localhost:8200
```

We will setup a user, names "user" and a password "1234".

We will also make a policy:

```
path "secret/*" {
	capabilities = ["read"]
}
```

We use the already existing Secrets kv engines.
We also make the secret token, with key **MICRO_SERVICE_TOKEN**, and a for now random token.

```
kubectl apply -f apigateway.yml
```

Now that the apiGateWay is running, we call

```
http://localhost:30001/Login/GEtAuthenticated
```

in something like postman. Then we enter the Vault again, and update the Token, with what is returned in the previous call.

```
kubectl rollout restart deployment/apigateway
```

```
kubectl apply -f loginserviceapi.yml
```

```
kubectl apply -f userserviceapi.yml
```

```
kubectl apply -f migration_service.yml
```

Once we had configured the pods, they were deployed into our Kubernetes cluster. Here are some screenshots of our system running within the Kubernetes environment.

![Kubernetes](https://github.com/Rogengell/TwitterCloneAssignment/blob/main/Screenshots/image%20kubernetes.png)

## Week 47 Security :lock:

We implemented an authentication mechanism to secure communication between the gateway and the microservices. This involves generating JWT tokens, which are stored securely on a vault server to prevent sensitive information from being exposed in our codebase. It also logs all secret access for auditing and compliance. By implementing Vault, we reduce the risk of leaks, improve security, and simplify secret management.

![Vault](https://github.com/Rogengell/TwitterCloneAssignment/blob/main/Screenshots/Screenshot%20vault.png)

## Week 48 microservice pattern (sidecar pattern) :passport_control:

In our Twitter clone, we implemented the sidecar pattern to enhance monitoring capabilities. This approach allows us to effectively trace and capture events ranging from warnings to critical errors (fatal). By doing so, we gain real-time visibility into system health and can promptly identify when something breaks or behaves unexpectedly.

![Fluentd](https://github.com/Rogengell/TwitterCloneAssignment/blob/main/Screenshots/Screenshot%20fluentd%20los%20example.png)

Implementing monitoring in our Twitter clone is essential for ensuring system reliability and detecting issues early. It enables rapid identification of errors, minimizes downtime, and improves user experience by maintaining fast and consistent performance.

to see the logs of our monitoring you can run thus commands in the terminal
with this you get a list of all pods

```
kubectl get pods -l app=fluentd
```

her we enter Linux terminal from the selected pod

```
kubectl exec -it <fluentd-pod-name> -- bash
```

her we get a list of all logs

```
ls /fluentd/log
```

with this we executer / open the log file in the terminal

```
cat /fluentd/log/<Name of the log file ex: apigateway.log.20241129.log>
```
