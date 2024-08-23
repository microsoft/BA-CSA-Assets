# Reliability Assessment - Availability Testing Strategy
![](./img/well-architected-hub.png)
## Question: How do you test your resiliency and availability strategies?

Reliability testing focuses on the resiliency and availability of your workload, specifically the critical flows that you identify when you design your solution. You test the measures and mechanisms that are implemented to achieve the desired level of resiliency. You prove the workload can degrade gracefully if there's a failure.

### Comments
*Speed. Consistency. What does QA mean to you? We all hold responsibility to ensure our business critical applications are reliable. A major component of that is testing, ensuring our application has been thoroughly vetted and validated.*

*This question focuses on both availability testing and testing as a whole.*

## Question Responses

### [X] **We ensure that the development and operations teams are familiar with the concepts of fault injection and chaos engineering.**
Your technical engineering team is familiar with the concepts of fault injection and chaos engineering. They understand why these techniques are important and what they can learn from their results.
#### Comments
*Use the Failure Mode Analysis information to test for faults that have been prioritized. Ensure you have a mitigation for each test and do not assume that because a test passed that you have tested for fault.*

***Chaos engineering** refers to attempts to break the system to better understand where things may fail. Measuring a baseline, injecting a fault and monitoring the result.*

*Dynamics 365 offers tools that can assist with chaos engineering. Power Automate offers static and synthetic results.*

**NOTE - If fault injection and chaos engineering is not part of your plan, do not check this box.**

#### References
[Principles of Chaos](https://principlesofchaos.org/)

[Power Platform fault injection and chaos engineering guidance](https://learn.microsoft.com/en-us/power-platform/well-architected/reliability/testing-strategy#fault-injection-and-chaos-engineering-guidance)

[Azure Fault injection and chaos engineering guidance](https://learn.microsoft.com/en-us/azure/well-architected/reliability/testing-strategy#fault-injection-and-chaos-engineering-guidance)

[Monitoring the Power Platform - Tracked Properties and Error Handling](https://community.dynamics.com/blogs/post/?postid=4afe064d-5663-4298-b2eb-67083ab561c7)

[Test workflows with mock outputs in Azure Logic Apps](https://learn.microsoft.com/en-us/azure/logic-apps/test-logic-apps-mock-data-static-results?tabs=consumption)

[YouTube - Power Apps Availability Testing Overview, Strategy and Samples](https://www.youtube.com/watch?v=Qkp_DqAJMHc)



### [X] **We run tests in a production-like environment, and we run a targeted subset of tests in production.**
You perform tests in an environment that's as production-like as possible in all important aspects: environment configuration, geography and setup. Target specific tests that you run in production to ensure that you can control the effects of the testing while proving the reliability of the production environment to the extent practical.
#### Comments
*Testing provides consistency and a guarantee from developers to quality assurance. Test plans should test in a production like environment. This could be SIT or Pre-Prod. It should be able to simulate production responses as close as possible. Test tools such as EasyRepro, Playwright, Power Apps Test Engine and LeapWork all represent options to test in a production-like environment.*

**NOTE - If a testing plan is not in place or production like testing is not happening, do not check this box.**

#### References
[Azure General testing guidance](https://learn.microsoft.com/en-us/azure/well-architected/reliability/testing-strategy#general-testing-guidance)

[Power Platform General testing guidance](https://learn.microsoft.com/en-us/power-platform/well-architected/reliability/testing-strategy#general-testing-guidance)

### [X] **We use planned downtime to run tests on production.**
For each period of planned downtime, when your workload or a part of a workload are offline, you seize the opportunity to run tests in your production environment where possible.
#### Comments
*Dataverse offers administrators the ability set a planned maintenance time. Microsoft also plans maintenance during specific time windows. During this downtime, you can attempt to test and understand what an outage may look like.*
![](https://github.com/aliyoussefi/MonitoringPowerPlatform/blob/main/Artifacts/AppInsights/AvailabilityTests/AzureWorldMapDeployed.jpg?raw=true)
**NOTE - If downtime tests are not feasible or not planned, do not check this box.**

#### References
[Azure Planned maintenance testing](https://learn.microsoft.com/en-us/azure/well-architected/reliability/testing-strategy#planned-maintenance)

[Power Platform Planned maintenance testing](https://learn.microsoft.com/en-us/power-platform/well-architected/reliability/testing-strategy#take-advantage-of-planned-and-unplanned-outages)
### [X] **We learn from production incidents.**
You have a process in place to learn from unplanned service interruptions and degradations. You use interruptions and degradations as input to continuously improve and extend your portfolio of tests.
#### Comments
As part of the post-mortem review of service interruptions, take time to ensure testing are created against the now known unplanned interruption. Tag the test in a test plan dedicated for service uptime observability.

#### References
[What is Azure Test Plans?](https://learn.microsoft.com/en-us/azure/devops/test/overview?view=azure-devops)

### [X] **We perform testing early in the development process and throughout it.**
Adopting a shift-left approach to testing in the development cycle enables you to perform small, frequent deployments by having code that's ready to deploy. You don't have to wait on time-intensive testing processes that occur at the end of the cycle. Testing early and throughout the cycle makes the entire development process smoother and more efficient.
#### Comments
*Shift-left testing should be done early and often. Designing and executing tests early in the development process can identify bugs and regressions. Dataverse and the Power Platform offer first party and community tools for dev testing.*

![](./img/Shift%20Left%20and%20Right%20Testing.png)

**NOTE - Testing should be done in a repeatable and predictable manner. If tests are not contained in a test suite and plan with outputs for review, do not check this box.**

#### References
[Azure General testing guidance](https://learn.microsoft.com/en-us/azure/well-architected/reliability/testing-strategy#general-testing-guidance)

[Power Platform General testing guidance](https://learn.microsoft.com/en-us/power-platform/well-architected/reliability/testing-strategy#general-testing-guidance)

[YouTube - Model Driven Power Apps UI Testing Execution and Results Locally](https://www.youtube.com/watch?v=gqsdJGsF4hM)

[YouTube - Recording Tests with Power Apps Test Studio](https://www.youtube.com/watch?v=iGpWDDhCCdU)

### [X] **We use the results of a failure mode analysis to identify and define meaningful chaos experiments.**
You performed failure mode analysis on your workload to determine a set of meaningful chaos experiments. You ranked the failure modes according to impact and have covered the highest-ranking failure modes in your experiments.
#### Comments
*Review the [Failure Mode Analysis Template](./docs/Failure%20Mode%20Analysis%20for%20Mission%20Critical%20Applications.docx) for opportunities to perform chaos experiments. These experiments can be conducted with Azure Load Testing or through Microsoft led Benchmark Assessments.*

**NOTE - If a FMA has not been conducted with chaos experiments and tools needed defined, do not check this box.**

#### References
[FastTrack Implementation Assets Testing at Scale](https://github.com/microsoft/Dynamics-365-FastTrack-Implementation-Assets/tree/master/Customer%20Service/Testing/At%20Scale)

### [X] **We monitor total application health during chaos experiments.**
During chaos experiments, you observe the behavior of critical flows holistically and end to end with the purpose of revealing unforeseen effects and impact.
#### Comments
*Application Insights as part of the Azure Monitor suite can help monitor Dataverse environments and Power Platform apps, bots and flows. Each service included in your critical workflows needs to be observable and should correlate based on a workflow execution.*
![](./img/Mointoring%20Tools%20and%20Scenarios.png)
**NOTE - If you do not monitor end to end transactions and have complete visibility into critical workloads, update the [Application Criticality Template](./docs/Application%20Criticality%20Template.docx) and do not check this box.**

#### References
[Dataverse Operational Telemetry](https://learn.microsoft.com/en-us/power-platform/admin/set-up-export-application-insights)

[Power Automate Operational Telemetry](https://learn.microsoft.com/en-us/power-platform/admin/app-insights-cloud-flow)

[Activity Logs - Dynamics 365 Connector for Azure Sentinel](https://learn.microsoft.com/en-us/azure/sentinel/data-connectors/dynamics-365)

[YouTube - Monitoring the Power Platform Bootcamp S03: Extend Application Insights to Power Apps Canvas Apps](https://www.youtube.com/watch?v=ZajMP2eLnpw&t=1s)

[YouTube - Power Platform Wll-Architected Framework - Monitoring Tools and Scenairos](https://www.youtube.com/watch?v=bXs57Zk7vjY&t=189s)
### [X] **We routinely perform testing to validate existing thresholds, targets, and assumptions.**
We routinely perform testing to validate existing thresholds, targets, and assumptions. When a major change occurs in your workload, run regular testing. Perform most testing in testing and staging environments. It's also beneficial to run a subset of tests against the production system.
#### Comments
*Shift-right testing into nonfunctional testing can provide valuable insights. Baselines can be affirmed, trends can be analyzed and recommendations provided.*

*Testing Dynamics 365 apps and Power Apps using automated test plans is critical to understand thresholds and target metrics are being met. Tools such as Power Apps Test Engine, Test Studio, EasyRepro and Azure Test Plans can assist.*
![](./img/PowerApps%20Testing%20Strategy.png)

**NOTE - If nonfunctional testing is not being performed and business targets are not validated do not check this box. Refer to the [FasTrack testing templates for more information](https://github.com/microsoft/Dynamics-365-FastTrack-Implementation-Assets/tree/master/Customer%20Service/Testing/Strategy/Templates).**


#### References
[Test your Dynamics 365 solution before deployment](https://learn.microsoft.com/en-us/dynamics365/guidance/implementation-guide/testing-strategy)

[Monitor and optimize your Dynamics 365 environments](https://learn.microsoft.com/en-us/dynamics365/guidance/implementation-guide/service-solution-monitor-service-health)

[YouTube - Recording Tests with Power Apps Test Studio](https://www.youtube.com/watch?v=iGpWDDhCCdU)