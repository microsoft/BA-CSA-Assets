# Reliability Assessment - Strengthen Resiliency
![](./img/well-architected-hub.png)
## Question: How do you strengthen the resiliency of your workload?

Strengthen the resiliency of your workload by implementing error handling and transient fault handling. Build capabilities into the solution to handle component failures and transient errors.

### Comments
*Business applications tend to interact with multiple services. While we can do our best to ensure uptime, some services may introduce throttles or faults. TO ensure resiliency we must design our systems to determine the type of fault and how to handle.*

*This question will primarily focus on the [RE:05 Background Jobs recommendations](https://learn.microsoft.com/en-us/power-platform/well-architected/reliability/background-jobs).*


### References
[Azure Well-Architected Framework Handling Transient Faults](https://learn.microsoft.com/en-us/azure/well-architected/reliability/handle-transient-faults)

[Azure Well-Architected Framework Developing Background Jobs](https://learn.microsoft.com/en-us/azure/well-architected/reliability/background-jobs)
## Question Responses

### [X]  **We designed the system to follow the paradigm of loosely coupled services.**
Avoid building monolithic applications in your application design. Use loosely coupled services that communicate with each other via well-defined standards to reduce the risk of extensive problems when malfunctions occur in a single component.
#### Comments
*Most Power Platform connections to services are controlled by connectors. These represent a loose coupling. If the organization has custom connectors or is connecting to Azure services, while they are also decoupled, it is important to review any connections happening within. This guidance also applies to Dynamics 365 web resources and plug-ins that make calls to external services.*

![](https://learn.microsoft.com/en-us/connectors/custom-connectors/media/index/intro.png)

*Another way to look at this is the application itself. Is it designed to be modular? Could a particular feature be removed with minimal impact?*

**NOTE - If you are not comfortable with the design of the application or would like further review, do not check this box.**

#### References
[Partition a Canvas App](https://learn.microsoft.com/en-us/power-apps/maker/canvas-apps/working-with-large-apps#partition-the-app)

[Power Apps Canvas App Code Standards and Guidelines](https://www.microsoft.com/en-us/power-platform/blog/wp-content/uploads/2024/06/PowerApps-canvas-app-coding-standards-and-guidelines.pdf
)

[Custom Connector Overview](https://learn.microsoft.com/en-us/connectors/custom-connectors/)

[Saga pattern with Dataverse](https://learn.microsoft.com/en-us/dynamics365/guidance/reference-architectures/saga-pattern-dataverse)
### [X]  **We implement asynchronous communication rather than synchronous communication between components wherever appropriate.**
Asynchronous communication reduces tight coupling of components and prevents data loss, and can also help increase performance.
#### Comments
*This has been a long standing recommendation for Dynamics 365 workloads that do not require business logic to be accessed in transaction. Evaluating usage of the async process should consider the stress that the system can be put under. Considerations should be made to establish a publish subscribe pattern with Azure Service Bus and Azure Event Hubs.*

![](https://learn.microsoft.com/en-us/power-apps/developer/data-platform/scalable-customization-design/media/understanding-causes.png)

*For Power Platform, similar pub sub patterns will apply here.*

**NOTE - If you have performance concerns or have not reviewed your logic for async offload, do not check this box.**

#### References
[Dataverse - Asynchronous Service](https://learn.microsoft.com/en-us/power-apps/developer/data-platform/asynchronous-service?tabs=webapi)

[Dataverse - Asynchronous processing of cascading transactions](https://learn.microsoft.com/en-us/power-platform/admin/async-cascading)

[Dataverse - Optimize performance for bulk operations](https://learn.microsoft.com/en-us/power-apps/developer/data-platform/optimize-performance-create-update#business-logic)

[Dataverse - Scalable customization design](https://learn.microsoft.com/en-us/power-apps/developer/data-platform/scalable-customization-design/overview)

[Dataverse - Write a custom Azure-aware plug-in](https://learn.microsoft.com/en-us/power-apps/developer/data-platform/write-custom-azure-aware-plugin)

[Power Apps - Database transactions](https://learn.microsoft.com/en-us/power-apps/developer/data-platform/scalable-customization-design/database-transactions)
### [X]  **We enable the workload to degrade its service gracefully.**
Sometimes you can't work around a problem, but you can provide reduced functionality. You take the appropriate steps to handle interruptions, either by rejecting new requests or queueing them for later processing. You present the effect of service degradation to the service consumer, for example by showing an error page or by using error response codes. You also publish the component's updated health status in its own health check endpoint.
#### Comments
*External services can introduce throttles or can become unresponsive. When interacting with these services, ensure that a proper time to live is set for any call. If this time is not met, then the system should respond with a message or queue for later processing.*



*Dynamics 365 offers a pattern with HTTP calls to set a time out using a header. All calls from web resources and plug-ins should include this.*

*Power Apps and Automate offer error codes and logic to queue messages.*

**NOTE - If you have apps or flows that do not account for potential disruption of any connected service, do not check this box.**

#### References
[Dataverse - Set Timeout when making external calls in a plug-in](https://learn.microsoft.com/en-us/power-apps/developer/data-platform/best-practices/business-logic/set-timeout-for-external-calls-from-plug-ins)

[Dataverse - Use InvalidPluginExecutionException](https://learn.microsoft.com/en-us/power-apps/developer/data-platform/best-practices/business-logic/use-invalidpluginexecutionexception-plugin-workflow-activities)

### [X] **We mitigate intermittent communication failures and congestion.**
To handle transient faults within your application implementation, you implemented the appropriate self-preservation methods.
#### Comments
*Communication failures or increased load can cause applications to not work as intended. Building off patterns such as Saga, Retry, Rate Limiting or Queue-Based Load Leveling could help here.*

**NOTE - If you have not reviewed how your business critical apps and flows respond to throttling or faults, do not check this box.**

#### References
[Log and track transient and nontransient faults](https://learn.microsoft.com/en-us/power-platform/well-architected/reliability/handle-transient-faults#log-and-track-transient-and-nontransient-faults)


[Azure - Queue-Based Load Leveling pattern](https://learn.microsoft.com/en-us/azure/architecture/patterns/queue-based-load-leveling)

[Azure - Rate Limiting pattern](https://learn.microsoft.com/en-us/azure/architecture/patterns/rate-limiting-pattern)

### [X] **We have determined if there's a built-in retry mechanism for workload components.**
Some services you're connecting to might already provide a transient fault handling mechanism. The retry policy it uses is typically tailored to the nature and requirements of the target service. Alternatively, REST interfaces for services might return information that can help you determine whether a retry is appropriate and how long to wait before the next retry attempt. You should use the built-in retry mechanism when one is available, unless you have specific and well-understood requirements that make a different retry behavior more appropriate.
#### Comments
Dynamics 365 workflows offer a retry based on the error code sent back to the system. Other services in the Power Platform will need a built mechanism if the service connecting to doesn't retry on behalf.
![](https://learn.microsoft.com/en-us/azure/architecture/patterns/_images/retry-pattern.png)

**NOTE - If you have not built a retry policy for all workloads within your app or flow that connect to services, do not check this box.**

#### References
[Dataverse - How to retry](https://learn.microsoft.com/en-us/power-apps/developer/data-platform/api-limits?tabs=sdk#how-to-re-try)

[Power Automate - Building a retry scope - Community Post](https://sharepains.com/2019/09/27/retry-after-failures-power-automate)

[Handle Transient Faults](https://learn.microsoft.com/en-us/power-platform/well-architected/reliability/handle-transient-faults)

### [X] We ensure all components of the workload provide insights into their behavior by writing appropriate log statements for later analysis.
Component logs describe actions, decisions, and the reason behind decisions, for example "Stopping sending further requests to back-end-X for the next 5 minutes because X's health endpoint returned state is unhealthy".
#### Comments
*Each service a business critical workload interacts with should respond with information about health. If not, this should be added to the Failure Mode Analysis as a risk.*

*Dataverse sends headers back detailing the API throttling and rate limits. These should be monitored. Implementing a circuit breaker pattern can help with business operations.*
![](https://learn.microsoft.com/en-us/azure/architecture/patterns/_images/circuit-breaker-diagram.png)

**NOTE - If you are not tracking returned headers from Dataverse for chatty or intensive workloads, do not check this box. Also, if you are not logging and actively monitoring do not check this box.**

#### References

[Azure - Circuit Breaker pattern](https://learn.microsoft.com/en-us/azure/architecture/patterns/circuit-breaker)

[Dataverse - Service Protection API Limits Errors returned](https://learn.microsoft.com/en-us/power-apps/developer/data-platform/api-limits?tabs=sdk#service-protection-api-limit-errors-returned)

### [X] We are avoiding duplicate layers of retry code.
You avoid designs that include cascading retry mechanisms or that implement retry at every stage of an operation that involves a hierarchy of requests, unless you have specific requirements to do so. You don't perform endless retries, and you don't perform an immediate retry more than once.
#### Comments
Duplicate retries can cause catastrophic issues if not implemented correctly. Data can be malformed, applications can become crippled. For all retry logic, ensure that it is documented to retry only a minimal amount of time. Alerting should be setup if any retry occurs.

**NOTE - If you are not retrying, monitoring retries and documenting between teams who is responsible for retry and rollback, do not check this box.**

#### References
[Determine an appropriate retry count and interval](https://learn.microsoft.com/en-us/power-platform/well-architected/reliability/handle-transient-faults#determine-an-appropriate-retry-count-and-interval)