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
Each guidance provided is intended to help frame and answer the question. Each question will include comments as will each possible response to the question. The structure will look similar to this:

#### Question: What is your plan for XYZ?
- Comments
- Artifacts, Learnings and General References
#### Question Responses
- [X] Possible Question Response
  - Comments
  - Note detailing how to answer
  - References

### Reliability Assessment Guidance
[Guidance on Question 1: How do you keep the workload simple and efficient?](./Assessment%20Guidance%2001-How%20do%20you%20keep%20the%20workload%20simple%20and%20efficient.md)

[Guidance on Question 2: How do you identify and rate the workload's flows?](./Assessment%20Guidance%2002-How%20do%20you%20identify%20and%20rate%20the%20workload's%20flows.md)

[Guidance on Question 3: How do you perform failure mode analysis?](./Assessment%20Guidance%2003-How%20do%20you%20perform%20failure%20mode%20analysis.md)

[Guidance on Question 4: How do you define reliability targets?](./Assessment%20Guidance%2004-How%20do%20you%20define%20reliability%20targets.md)

[Guidance on Question 5: How do you strengthen the resiliency of your workload?](./Assessment%20Guidance%2005-How%20do%20you%20strengthen%20the%20resiliency%20of%20your%20workload.md)

[Guidance on Question 6: How do you implement background jobs?](./Assessment%20Guidance%2006-How%20do%20you%20implement%20background%20jobs.md)

[Guidance on Question 7: How do you test your resiliency and availability strategies?](./Assessment%20Guidance%2007-How%20do%20you%20test%20your%20resiliency%20and%20availability%20strategies.md)

[TBD - Guidance on Question 8: How do you plan for disaster scenarios?](./Assessment%20Guidance%2008-How%20do%20you%20plan%20for%20disaster%20scenarios.md)

[TBD - Guidance on Question 9: How do you plan to monitor health?](./Assessment%20Guidance%2009-How%20do%20you%20plan%20to%20monitor%20health.md)

### What to do post assessment
Store the CSV file and perform another assessment at an agreed upon date. Review the new assessment to the previous to determine trends.

Review the artifacts contained in this repository. Review the self paced trainings below.

## Checklist and Artifacts
[Checklist](https://learn.microsoft.com/en-us/power-platform/well-architected/reliability/checklist)

[Failure Mode Analysis for Mission Critical Applications Template](./docs/Failure%20Mode%20Analysis%20for%20Mission%20Critical%20Applications.docx)

[Business Criticality Template](./docs/Application%20Criticality%20Template.docx)