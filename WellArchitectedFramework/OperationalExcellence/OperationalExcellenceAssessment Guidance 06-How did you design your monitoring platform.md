# Operational Excellence Assessment - Software Observability
## Question: How did you design your monitoring platform?

It's important to have a build process up and running the first day of your product development. High-performing teams use industry-proven tools and processes to minimize wasted effort and potential code errors. As code is developed, updated, or even removed, having an intuitive and safe method to integrate these changes into the main code branch enables developers to provide value.


### Comments


## Question Responses

### [X] **We collect telemetry or events like logs and metrics.**
You should configure all workload components to capture telemetry and/or events like logs and metrics. Logs are primarily useful for detecting and investigating anomalies. Metrics are primarily useful for building a health model and identifying trends in workload performance and reliability.
#### Comments


**NOTE - **


### [X] **We store monitoring data with just enough retention and in appropriate geographies.**
Monitoring data is retained according to its usage, such as long-term storage for legal and auditing reasons, mid-term for trending and planning, and short-term to drive your operational activities in everyday operations. Data residency and privacy requirements are also followed.
#### Comments


**NOTE - **

### [X] **We keep separate data between environments.**
You use distinct monitoring data sinks for each environment to keep the data from different environments separate.
#### Comments


**NOTE - **

### [X] - **We ensure our monitoring data provides perspective both holistically and at individual user and system flow levels.**
Rather than restricting your focus on metrics of individual components of your workload, you are putting the state and behavior of your user and system flows into the center of your consideration. This involves being able to correlate request and data flows throughout the workload.
#### Comments


**NOTE - **

### [X] **We prefer, where possible, platform services and capabilities for collecting metrics and log information over custom solutions.**
As much as possible, collect logs from your cloud platform. You might be able to collect activity logs for your subscription and diagnostic logs for the management plane.
#### Comments


**NOTE - **

### [X] **We visualize monitoring data and make it available to stakeholders.**
Your operations team, your developers, business stakeholders can use the monitoring platform to get insight about the workload. You visualize and expose monitoring data to meet their needs.
#### Comments


**NOTE - **

### [X] **We have created an alerting strategy that focuses on actionable alerts directed to appropriate parties.**
Alerts should notify the workload teams when the health of a given component or of the workload overall changes from one state to another. Alerts need to be actionable, meaning the party receiving the alert needs to perform some activity to investigate and begin remediation. Avoid creating alerts that can be treated as background noise, which leads to alert fatigue.
#### Comments


**NOTE - **


