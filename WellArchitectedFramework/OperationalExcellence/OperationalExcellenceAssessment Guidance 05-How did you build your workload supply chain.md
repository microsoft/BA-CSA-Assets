# Operational Excellence Assessment - Software Deployment and Continuos Integration
## Question: How did you build your workload supply chain?

Develop a supply chain to ensure that you have a predictable, standardized method of maintaining your workload. CI/CD pipelines are the manifestation of the supply chain, but you should have a single supply chain. And you might have several pipelines that follow the same processes and use the same tools.


### Comments


## Question Responses

### [X] **We ensure all deployments, to all environments, happen through deployment pipelines.**
You use pipelines to implement the required checks or get required approvals to follow the process you've defined within your workload to drive quality changes to release. Beyond your pipelines, there's no further out-of-band processes that are invisible to the pipeline and its history and logs. For production environments, avoid human interaction with the cloud platform's control plane.
#### Comments


**NOTE - **


### [X] **We use one set of code assets and artifacts across all environments and pipelines.**
A common pain point for organizations is when nonproduction environments are different from production environments. Building production and nonproduction environments manually can result in mismatched configurations between the environments. You ensure your test and QA environments are sufficiently comparable to provide insightful test results.
#### Comments


**NOTE - **

### [X] **We have a standard deployment method to the production environment.**
Talk to the product owner about the acceptable amount of production downtime for your workload. Depending on how much, if any, downtime is acceptable, you can choose the deployment method that's right for your requirements. Ideally, you should perform deployments during a maintenance window to reduce complexity and risk. If no downtime is acceptable, employ a blue-green deployment method.
#### Comments


**NOTE - **

### [X] - **We have planned for a robust testing strategy that aligns with a shift-left ethos.**
Your testing strategy should include all code, including application code, IaC code, and configuration scripts. Automate your testing to the extent that it's practical for your workload operations to ensure consistency and minimize human error. Your strategy should include unit, smoke, integration and chaos testing, all of which should be performed in non-production and production environments.
#### Comments


**NOTE - **

### [X] **We use quality gates to promote code to higher environments.**
Supply chains that span across all environments manage the roll-out of changes from the code repository across all pre-production environments into to your production environment. Quality gates need to be passed before you promote code to a higher environment, like development to testing.
#### Comments


**NOTE - **



