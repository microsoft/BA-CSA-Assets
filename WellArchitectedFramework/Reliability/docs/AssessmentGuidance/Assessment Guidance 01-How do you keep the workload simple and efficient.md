---
title: 01 How do you keep the workload simple and efficient?
weight: 1
geekdocCollapseSection: true
#slug: Reliability
#geekdocHidden: true
#geekdocHiddenTocTree: true
geekdocAlign: "left"
---
{{< alertList name="alertList" >}}
# Reliability Assessment - Concise and Simple Workflows
## Question: How do you keep the workload simple and efficient?

A key tenet of designing for reliability is to keep your workload simple and efficient. Focus your workload design on meeting business requirements to reduce the risk of unnecessary complexity or excess overhead. Various workloads might have different requirements for availability, scalability, data consistency, and disaster recovery.

### Comments


## Question Responses

### [X] **We have a deliberate goal to keep our workload simple.**
Striving for reliability and maintainability, you keep the workload's general implementation guidelines concise and simple.
#### Comments
*Application architecture and workloads should focus on minimizing complexity and reducing dependencies (shout out to Steve McConnell!). Look for workloads that include complex design spanning geos, clouds, hybrid connections.* 

#### Power Platform Comments
Power Apps and Power Automate continuously get new features to help streamline operations and make workloads more efficient. Are you reviewing all apps and flows for new functionality that can help make things more concise?

**NOTE - If a plan is not in place to reduce complexity, keep this unchecked.**


### [X] **We utilize platform functionality where appropriate.**
You have assessed platform-provided offerings and features, so you can offload administration and management tasks to Power Platform and other cloud providers.
#### Comments
*Review current administrative and change management processes. How many of these leverage Dynamics or Power Platform based tools? How much of this is still a manual effort?* 

#### Power Platform Comments

**NOTE - Check this box if you feel you have done an assessment and are confident in the latest tools for Power Platform management. For additional assistance, consider [a Microsoft led workshop](https://pfedynamics.wordpress.com/wp-content/uploads/2023/09/activatepowerplatformadminenglish.pdf) or this self paced training.**

### [X] **We offload cross-cutting services to separate services.**
Rather than duplicating code, you reuse existing services with well-defined interfaces that can easily be consumed by various components of your workload.
#### Comments
*Cross cutting concerns such as monitoring, exception handling, data access and distribution should be well defined and reusable.*

**NOTE - If you do not have a plan for this across Dynamics 365 apps and Power Platform apps and flows, do not check this.**

### [X] - We implement essential capabilities in our workload for all coding efforts.
You focus your coding efforts on functionalities that support or deliver primary business objectives for your workload.
#### Comments
*All code should focus on delivering value. Building unneeded code for the future can incur a maintenance cost. With ever changing requirements and technologies, focus on streamlining coding efforts.* 

**NOTE - If code is not being reviewed each sprint for maintenance and complexity, do not check this box.**

### [X] **We understand the functional and nonfunctional requirements of the workload.**
Functional requirements define the features and behavior of the system. They're specified by the user and captured in use cases. Nonfunctional requirements define the performance and quality attributes of the system. You understand nonfunctional requirements like availability, compliance, data retention/residency, performance, privacy, recovery time, security, and scalability.
#### Comments
*Review each component and the services or resources it relies on. Ensure each interaction is documented including contracts, interfaces and so on. Take time to review each service's SLA, RPO and RTO advantages. Document each business critical workload into the business criticality analysis artifact here.* 

**NOTE - If you cannot answer both functional or nonfunctional attributes, do not check this box.**
