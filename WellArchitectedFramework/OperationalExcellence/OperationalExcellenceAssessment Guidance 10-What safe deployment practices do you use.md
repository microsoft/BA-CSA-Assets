# Operational Excellence Assessment - Deployment Quality
## Question: What safe deployment practices do you use?

Deployments introduce risk into the workload. It's important to ensure safe and consistent deployments. This involves considering the frequency, automation, monitoring, testing, and recovery protocols in place.


### Comments


## Question Responses

### [X] **We reduce risk by deploying small, incremental, and tested changes.**
Whether you're deploying an update to your application code, feature flag, or a configuration update, you're introducing risk to the workload. Every deployment must follow a standard pattern and should be automated to enforce consistency and minimize the risk of human error.
#### Comments


**NOTE - **


### [X] **We rely on health signals to provide go, no-go decisions during rollouts.**
As a workload is being progressively exposed, the health and functionality of the workload is observed and passes quality gates before continued exposure. During a rollout, if you receive an alert about a health change relating to an end user, the rollout should immediately halt and an investigation into the cause of the alert should be performed to help determine the next course of action.
#### Comments


**NOTE - **

### [X] **We have a defined hotfix strategy that safely addresses urgent releases.**
Urgent releases are still subject to safe deployment practices, but have a accelerated process to address the emergency presented.
#### Comments


**NOTE - **

### [X] - **We ensure the workload reliably handles data changes.**
Your safe deployment practices guidelines should provide clear instructions on how to deal with data changes according to the data estate design for your workload.
#### Comments


**NOTE - **

### [X] - **We ensure that any and all production changes follow the safe deployment practices.**
No production changes, including configuration changes, bypass the workload's safe deployment practices.
#### Comments


**NOTE - **
