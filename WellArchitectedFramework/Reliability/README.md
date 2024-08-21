# Summary
This repository focuses on business applications built by organizations upon the Power Platform. It encompasses best practices learned and crafted by experts in the field.

This overview will highlight the principles of Reliability as they pertain to Dynamics 365 and the Power Platform. We understand that each application has unique requirements and nuanced architecture that can span into other technologies and clouds. As such we will provide our best effort detailing workloads within the Dynamics 365 and Power Platform ecosphere.  

## Reliability Principles
Reliability is an assurance. It covers application layers and tiers. It involves multiple teams including but not limited to both Operational and IT groups. The assurance provided is intended to keep significant problems from essentially crippling organizations.

Some of the quality attributes attested to reliability include:
- Resiliency
- Recovery
- Operational observability
- Simplicity and efficiency
  
### Power Platform
Power Platform provides opportunities to build in observability and recovery into each pillar. Power Apps can trigger alerts based on tests or observed patterns. Power Automate has retry mechanisms. Copilot Studio sends telemetry and session information based on conversations.

Each workload that uses any of these pillars should include these capabilities. Each one that doesn't, espicially business critical workloads, could jeoparidze your ability to react quickly to any problems.

### Dynamics 365
As Power Platform provides opportunities so does Dynamics 365. Native redundancy for failover to a secondary location. 


## The Power Platform Well-Architected Framework Assessment and Readout

### Guidance on the Assessment
[Guidance on Question 1](./Assessment%20Guidance%2001.md)

[Guidance on Question 2](Assessment%20Guidance%2002.md)

[Guidance on Question 3](Assessment%20Guidance%2003.md)
### What to do post assessment
Store the CSV file and perform another assessment at an agreed upon date. Review the new assessment to the previous to determine trends.

Review the artifacts contained in this repository. Review the self paced trainings below.

## Checklist and Artifacts
[Checklist](https://learn.microsoft.com/en-us/power-platform/well-architected/reliability/checklist)

[Failure Mode Analysis for Mission Critical Applications Template](./docs/Failure%20Mode%20Analysis%20for%20Mission%20Critical%20Applications.docx)

[Business Criticality Template](./docs/Application%20Criticality%20Template.docx)