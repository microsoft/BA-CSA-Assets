# Reliability Assessment - Strengthen Resiliency
![](./img/well-architected-hub.png)
## Question: How do you strengthen the resiliency of your workload?

Strengthen the resiliency of your workload by implementing error handling and transient fault handling. Build capabilities into the solution to handle component failures and transient errors.

### Comments
*Power Platform handles most of the infrastructure needed to run its services. That said it is a shared responsibility based on the workloads and strain applied to the platform. Also, as the platform provides extensibility into Azure, the responsibility can shift more towards organizations.* 

*This question will primarily focus on the [RE:04 Target Metrics recommendations](https://learn.microsoft.com/en-us/power-platform/well-architected/reliability/metrics).*

*To learn more and get started with defining target metrics, review the official FMA collection below.*

### References
[Well-Architected Framework Target Metrics](https://learn.microsoft.com/en-us/azure/well-architected/reliability/metrics)

[Official Microsoft Well-Architected Framework Reliability Failure Mode Analysis Collection](https://learn.microsoft.com/en-us/collections/138f02j4p0ge0?&sharingId=37BA9080B82744F0)

[YouTube: Power Platform Well-Architected Framework - Defining a Monitoring Strategy](https://www.youtube.com/watch?v=AvoD66ItJv4)

## Question Responses

### We designed the system to follow the paradigm of loosely coupled services.
Avoid building monolithic applications in your application design. Use loosely coupled services that communicate with each other via well-defined standards to reduce the risk of extensive problems when malfunctions occur in a single component.
#### Comments


#### References

### We implement asynchronous communication rather than synchronous communication between components wherever appropriate.
Asynchronous communication reduces tight coupling of components and prevents data loss, and can also help increase performance.

### We enable the workload to degrade its service gracefully.
Sometimes you can't work around a problem, but you can provide reduced functionality. You take the appropriate steps to handle interruptions, either by rejecting new requests or queueing them for later processing. You present the effect of service degradation to the service consumer, for example by showing an error page or by using error response codes. You also publish the component's updated health status in its own health check endpoint.

### We mitigate intermittent communication failures and congestion.
To handle transient faults within your application implementation, you implemented the appropriate self-preservation methods.

### We have determined if there's a built-in retry mechanism for workload components.
Some services you're connecting to might already provide a transient fault handling mechanism. The retry policy it uses is typically tailored to the nature and requirements of the target service. Alternatively, REST interfaces for services might return information that can help you determine whether a retry is appropriate and how long to wait before the next retry attempt. You should use the built-in retry mechanism when one is available, unless you have specific and well-understood requirements that make a different retry behavior more appropriate.

### We ensure all components of the workload provide insights into their behavior by writing appropriate log statements for later analysis.
Component logs describe actions, decisions, and the reason behind decisions, for example "Stopping sending further requests to back-end-X for the next 5 minutes because X's health endpoint returned state is unhealthy".

### We are avoiding duplicate layers of retry code.
You avoid designs that include cascading retry mechanisms or that implement retry at every stage of an operation that involves a hierarchy of requests, unless you have specific requirements to do so. You don't perform endless retries, and you don't perform an immediate retry more than once.