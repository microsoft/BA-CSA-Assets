# Reliability Assessment - Monitoring Health
![](./img/well-architected-hub.png)
## Question: How do you plan to monitor health?

Background jobs run automatically without the need for user interaction. Background jobs help minimize the load on the application UI, which improves availability and reduces interactive response time.

### Comments


## Question Responses

### **We use trigger conditions to reduce the number of unnecessary runs**
Trigger conditions set up multiple conditions that must be met before a workflow is triggered. Use trigger conditions to streamline your workflows and reduce the number of unnecessary runs.

### **We have well defined return results for background jobs.**
Background jobs run asynchronously in a separate process from the UI or the process that invoked the background job. If you require a background task to communicate with the calling task to indicate progress or completion, you must return a well defined and documented status indicator.

### **We have evaluated all steps of the background job and decomposed a task into multiple reusable steps.**
Background tasks can be complex and require multiple tasks to run. Evaluate if you can divide the task into smaller discrete steps or subtasks that multiple consumers can run.

### **We planned background jobs to provide a reliable service for the workload.**
Create resilient background tasks to provide reliable services for the application. Background tasks need to gracefully handle restarts without corrupting data or introducing inconsistency into the application.

### **We planned background jobs to offer sufficient performance for the workload.**
Background tasks must offer sufficient performance to ensure that they don't block the application or delay operation when the system is under load.