# **TwitterCloneAssignment**

This project aims to develop a Twitter-like platform, structured into four key stages. We will begin with the creation of an architecture diagram, followed by the implementation of two interconnected microservices. This overview will outline each step briefly, providing a clear understanding of our process. For a comprehensive overview a![Figma Link](https://www.figma.com/board/yup3d434pA2f6q1tH27IRE/Tweed-Application?node-id=0-1&node-type=canvas)  

## **The architectual diagram**

# Picture hier

As you can clearly see, we have implemented three central microservices in our system.

1 **Account Management**: This service takes care of all functions related to the creation and management of user accounts. This includes registering new users, updating profile information and searching for users. In addition, the service also enables advanced search functions, such as finding users or content using tags.

2 **Messenger Manager**: The second service is responsible for creating and managing messages (tweets). Here, users can not only create new tweets, but also search for existing messages. This service also enables the creation and management of comments on the respective tweets, which promotes interactive communication between users.

3 **Notification Manager**: The third service ensures that users are informed about activities related to their tweets. This includes notifications about new comments and reactions to their posts. This keeps users up to date and allows them to respond to interactions in a timely manner.

These three microservices work together seamlessly to create a comprehensive and user-friendly platform that meets the needs of our users.

## Development Strategy

for a quick understanding we made a C4 model up to level 2

**Level 1 C4 Model**

**Level 2 C4 Model**

We chose to implement an API gateway instead of a messaging system like RabbitMQ for several key reasons:

**Centralized Control:** The API gateway provides a single point of entry for clients, simplifying administration and communication within the system.

**Enhanced Security:** It allows for the integration of features such as authentication, authorization, rate limiting, and API key management, which are crucial for our decision.

**Improved Performance:** The gateway enables performance enhancements through caching and load balancing, facilitating scalability.

**Simplified Client Interactions:** Clients benefit from a unified API interface, reducing complexity in interactions.


While we prioritized security and simplified interactions with the API gateway, we recognize the advantages of RabbitMQ for other microservices that focus on communication and notifications. RabbitMQ is better suited for loose coupling and asynchronous communication, particularly in complex workflows or event-driven architectures.

Ultimately, a hybrid approach that combines both the API gateway and RabbitMQ could be beneficial. The API gateway can manage requests, while RabbitMQ handles asynchronous communication between services.

## Implementation
This is a brief overview of the technologies we utilized and the reasons we believe they will benefit our project.
**Docker:** We package each service into Docker containers to ensure consistent environments and facilitate easier deployment.

**Entity Framework (EF):** We use EF for database implementation and management, aiming for a uniform database with every deployment to maintain system reliability.

**.NET Core & REST:** REST is an effective protocol for enabling two-way communication through requests and responses.

**Dependency Injection:** This approach fosters loose coupling between components, enhancing code readability and testability. It allows us to inject services such as data access and business logic into our controllers.

**XUnit (Testing):** As we had no prior experience with testing in .NET, we utilized available resources for mocking and testing. This proved to be a valuable learning experience, and we plan to use these tools in future projects.
