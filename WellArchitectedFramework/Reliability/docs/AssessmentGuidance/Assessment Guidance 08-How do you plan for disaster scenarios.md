---
title: 08 How do you plan for disaster scenarios?
weight: 1
geekdocCollapseSection: true
#slug: Reliability
#geekdocHidden: true
#geekdocHiddenTocTree: true
geekdocAlign: "left"
---
# Reliability Assessment - Disaster Scenarios
![](./img/well-architected-hub.png)
## Question: How do you plan for disaster scenarios?

To meet internal service-level objectives (SLOs) or even a service-level agreement (SLA) that you have guaranteed for your customers, you must have a robust and reliable disaster recovery strategy. Failures and other major issues are expected. Your preparations to deal with these incidents determine how much your customers can trust your business to reliably deliver for them. A disaster recovery strategy is the backbone of preparation for major incidents.

### Comments
*This question focuses on the plans you have to recover from an outage. The FMA is critical to this process so if this has not been performed most answers will be left unchecked.*

*Also, this is a great time to work with the operations team in the organizations to ensure definitions for things like an outage are clear to all.*

## Question Responses

### [X] **We ensure that recovery and failover plans have clear criteria.**
You have clear decision criteria that you can use to decide if and when your disaster recovery plan must be initiated. You obtain these criteria from the results of your failure mode analysis and your availability and recovery targets. You also have clear criteria to decide when the failover is considered 'completed'.
#### Comments
*Can you answer the question, "What constitutes a disaster"? Disasters should be defined by the scale of the issue. Is it a regional outage for services like Entra ID?*

*As part of the FMA, identify what is and what isn't a disaster scenario and what the mitigation steps are.*

**NOTE - If the FMA has not be completed with disaster scenarios and mitigation steps do not check this box. Start by clearly defining what is a disaster and apply this definition to each FMA for business critical workloads.**
#### References
[Azure - Disaster Recovery](https://learn.microsoft.com/en-us/azure/well-architected/reliability/disaster-recovery)

[Power Platform - Maintain a disaster recovery plan](https://learn.microsoft.com/en-us/power-platform/well-architected/reliability/disaster-recovery#maintain-a-disaster-recovery-plan)
### [X] **We use the results of our failure mode analysis to create recovery plans.**
To identify failures that have a high impact, your failure mode analysis classifies respective failure modes as disasters. You use this classification to distinguish various disasters and design appropriate mitigation strategies.
#### Comments
*Review the example located [here](https://learn.microsoft.com/en-us/power-platform/well-architected/reliability/failure-mode-analysis#example). Outage classifications include Full, Potential for Partial, Potential for Full Outage, Low Risk, etc.*

*For each service identified and documented, a order of operations for testing availability should start to come together. Using the FMA is critical to defining the recovery plan.*

**NOTE - If each service in a workload has not been classified into how disruptive an outage will be, do not check this box.**
#### References
[Failure Mode Analysis Template](./docs/Failure%20Mode%20Analysis%20for%20Mission%20Critical%20Applications.docx)

### [X] **We define, document, and understand the roles and responsibilities during an incident response.**
There's clear organizational guidance about roles and responsibilities during an active incident. For example, you know who makes critical decisions like declaring and closing the disaster, or deciding how to go forward in unforeseen situations, testing, and validating. In addition, escalation paths and communication channels are well-defined.
#### Comments
*Part of the plan must define roles and responsibilities. Roles such as declaring a disaster and resolution, communications, and RCAs.*
Roles include:
- The party responsible for declaring a disaster
- The party responsible for declaring incident closure
- Operations roles
- Testing and validation roles
- Internal and external communications roles
- Retrospective and root-cause analysis (RCA) lead roles
  
**NOTE - If these roles have not been defined, do not check this box.**

#### References
[Azure - Maintain a disaster recovery plan](https://learn.microsoft.com/en-us/azure/well-architected/reliability/disaster-recovery#maintain-a-disaster-recovery-plan)

[Power Platform - Maintain a disaster recovery plan](https://learn.microsoft.com/en-us/power-platform/well-architected/reliability/disaster-recovery#maintain-a-disaster-recovery-plan)
### [X] **We have a disaster recovery plan with processes to recover the workload, components, and data.**
Your disaster recovery plan is sufficiently detailed to provide guidance for the recovery of various aspects of your workload. You have comprehensive documentation describing the recovery of the entire workload from scratch. You might prioritize the recovery procedure of the data estate.
#### Comments
*The Disaster Recovery plan should include recovery metrics such as RTO and RPO. For Dataverse, ensure backups are taken regularly and transactions that may have been terminated mid execution can be rolled back.*

*For Power Automate consider the workflows leveraging the Power Automate flow. Is there a plan in place to validate any data that may have been modified during the flow run? Flows offer a retry capability but at this time only for the entire flow run so consider the design of the flow for validating before execution.*

**NOTE - The DR plan needs to include how to properly recover from an incident both from service availability and data validation. If not, do not check this box.**

#### References
[Azure - Maintain a disaster recovery plan](https://learn.microsoft.com/en-us/azure/well-architected/reliability/disaster-recovery#maintain-a-disaster-recovery-plan)

[Power Platform - Maintain a disaster recovery plan](https://learn.microsoft.com/en-us/power-platform/well-architected/reliability/disaster-recovery#maintain-a-disaster-recovery-plan)
### [X] **We've deliberately automated disaster recovery processes wherever reasonable.**
Automation of your recovery processes improves the predictability of its execution and therefore increases the confidence in your processes.

#### Comments
*Dataverse and the Power Platform handle the infrastructure hosting the core services so the focus should be ensuring that the customizations (apps, flows, bots, etc) are in working order. Having a robust testing strategy to validate availability and quality of responses is key.*

*If Azure services are part of any workload then they need to be included in the test strategy. Also, the organization may need additional scripts and procedures confirming recovery. A well defined order of operations is expected with this question.*

**NOTE - If a order of operations is not established and all services within the business applications validated with automation, do not check this box.**
#### References
[Power Platform - Maintain a disaster recovery plan](https://learn.microsoft.com/en-us/power-platform/well-architected/reliability/disaster-recovery#maintain-a-disaster-recovery-plan)

[BAP - Business Continuity and disaster recovery](https://learn.microsoft.com/en-us/power-platform/admin/business-continuity-disaster-recovery)

[Azure - Maintain a disaster recovery plan](https://learn.microsoft.com/en-us/azure/well-architected/reliability/disaster-recovery#maintain-a-disaster-recovery-plan)

[DR for Azure Data Platform - Overview](https://learn.microsoft.com/en-us/azure/architecture/data-guide/disaster-recovery/dr-for-azure-data-platform-overview)
### [X] **We have defined an escalation path.**
You've have defined the escalation paths that the workload team must follow to ensure that recovery status is communicated to stakeholders.
#### Comments
*All business critical workloads should include an escalation path in the situation that an outage occurs. This includes communications to end users and validation of all services availability.*

**NOTE - Do not check this box if an escalation path has not been created.**

#### References
[Atlassian - Create an escalation path template](https://www.atlassian.com/incident-management/on-call/escalation-path-template#how-to-create-an-escalation-path-template)

### [X] **We have detailed instructions on the order in which components of the workload should be recovered.**
You have included the prescribed order in which components of the workload should be recovered to cause the least impact. For example, recover databases and restart cloud flows before you recover the application. You have detailed each component's recovery procedure as a step-by-step guide. You have defined your team's responsibilities versus your cloud hosting provider's responsibilities.
#### Comments
*Each task in the instructions must not leave any room for guessing or assumptions. "Check flows are on in an environment" isn't going to cut it. "Check XYZ flow in ABC solution in 123 environment is on" is more concrete.*

*Review the table below, found [here](https://learn.microsoft.com/en-us/power-platform/admin/business-continuity-disaster-recovery).*

![](./img/DrResponsibility.png)

*Anything listed in Customers responsibilities needs to have a plan in place with detailed instructions.*

**NOTE - If this plan is not available and/or is ambiguous to the steps needed to fully recover, do not check this box.**

#### References
[Power Platform Business Continuity and Disaster Recovery](https://learn.microsoft.com/en-us/power-platform/admin/business-continuity-disaster-recovery)
### [X] **We regularly test our recovery plans in a dry run.**
You perform regular dry runs to test, update, and improve the disaster recovery plan. You train all relevant staff to continuously improve the plan and team execution. You take the dry runs as an opportunity to update the disaster recovery plan's timeline.
#### Comments
*You should have a production like environment available for DR recovery drills. These drills should help uncover gaps and train everyone. These should be performed at least once a year. The ability to test your metrics such as RTO and define the minimum time necessary for recovery are key outcomes from these drills.*

**NOTE - Do not check this box if you do not have a production like environment that you have performed a DR dry run against.**

#### References
[Conduct disaster recovery drills](https://learn.microsoft.com/en-us/power-platform/well-architected/reliability/disaster-recovery#conduct-disaster-recovery-drills)
### [X] **We know the expected recovery time of each of our recovery plans.**
Your disaster recovery plan includes how long operations take to complete. You can assess compliance with your recovery time objective (RTO). The time indications take into account steps that you can't accelerate, for example Domain Name System (DNS) updates need a certain amount of time to propagate.
#### Comments
*It's recommended to understand how long each service in your workloads will take to recover. When using the Application Criticality Template, ensure that this is clearly defined.*

**NOTE - If the RTO for each service your workload connects to is not defined, do not check this box.**
#### References
[Application Criticality Template](./docs/Application%20Criticality%20Template.docx)