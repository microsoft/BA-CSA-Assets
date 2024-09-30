# Security Assessment - Security Testing
## Question: How do you test your workload before and after it reaches production?

Implement a security testing regimen by combining the following testing approaches: prevent the introduction of security issues; validate implemented threat-prevention mechanisms; and validate threat-detection mechanisms.

### Comments


## Question Responses

### [X] **We use tools to monitor our workload resources.**
You should use the monitoring tools that are built into the platform that your application runs on. These tools can give you insights and a good view of security events.

#### Comments



#### References


### [X] **We collect logs from all workload user flows.**
You should design your workload to provide runtime visibility when events occur. Code should be instrumented via structured logging. Doing so enables easy and uniform querying and filtering of logs.

### [X] **We periodically review access to the control plane and data plane of the application.**
Record all resource access activities by capturing who does what and when they do it.

### [X] **We use a threat-detection tool to aggregate and analyze logs.**
Consider a central view of logs and data, when applicable. Storing all your logs in a single repository creates a single point of observability, reduces duplication, and aids in the use of machine learning to corelate incidents.

### [X] **We monitor configuration drift away from the baseline and agreed-upon standards.**
Drift in resource configuration must be monitored. Any modification to an existing resource needs to be documented. Also keep track of changes that can't finish as part of a rollout to a fleet of resources. Logs must capture the specifics of each change and the exact time that it occurred.

### [X] **We use modern threat-detection techniques to detect suspicious activities.**
Transforming aggregated data into actionable intelligence helps you react to incidents quickly and effectively. Monitoring helps with post-incident activities. The goal is to collect enough data to analyze and understand what happened. The process of monitoring captures information about past events that you can use to enhance reactive capabilities and potentially predict future incidents.

