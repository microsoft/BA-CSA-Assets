# Security Assessment - Security Baseline
## Question: Do you have a documented security baseline?

A security baseline is a document that defines your organization's security standards and expectations for various areas. A good security baseline helps you to protect your data and systems, meet regulatory requirements, avoid risk of accidental errors, and lower the chances of breaches and their business impact. Security baselines should be widely shared in your organization so that all stakeholders know what you expect.

### Comments


## Question Responses

### [X] **We documented an initial baseline for our workload that's based on industry standards, compliance, and business requirements.**
Documenting an initial baseline for your workload is an important step in security hardening. It helps you to establish a clear and consistent set of security requirements and expectations for your workload, based on your business and regulatory compliance needs. It also helps you to identify and address any gaps or weaknesses in your current security posture, and to measure and track your progress and improvement over time. By documenting an initial baseline, you can ensure that your workload meets the minimum security standards and is protected from potential threats.
#### Comments
**Baseline** - The minimum level of security affordances that a workload must have to avoid being exploited.

Generally, if you have a plan for asset inventory, risk assessment, control access and monitoring you can check this box. If you have a potential plan or gaps in the items listed before, keep this unchecked.


#### References
Review the [security strategy in the Success by Design methodology](https://learn.microsoft.com/en-us/dynamics365/guidance/implementation-guide/security).

Review the guidance in the Power Platform Well-Architected Framework for establishing a [security baseline](https://learn.microsoft.com/en-us/power-platform/well-architected/security/establish-baseline).

[Microsoft Cloud Security Benchmark](https://learn.microsoft.com/en-us/security/benchmark/azure/overview)

### [X] **We continuously revisit the baseline as the workload gets updated and changes over time.**
Continuously revisiting the baseline for your workload is an essential part of security hardening. It helps you to keep your security requirements and expectations up to date with the changing needs and challenges of your workload. It also helps you to detect and respond to any new or emerging threats or vulnerabilities that can affect your workload, and to implement any necessary security improvements or adjustments. By continuously revisiting the baseline, you can ensure that your workload maintains a high level of security and resilience over time.

#### Comments
Microsoft releases major updates to the platform in release waves twice a year. The feature list is released a couple of months before feature roll outs generally. Be sure to schedule time to review any updates to security features.

### [X] **We have a baseline with recommendations for incident response, including communication and recovery.**
The baseline must establish standards for using threat detection capabilities and alerting on unusual activities that indicate real incidents. These standards must cover all layers of the workload, including all the endpoints that can be accessed from hostile networks. The baseline must also have recommendations for setting up incident response processes such as communication and a recovery plan. The baseline should also recommend which of those processes can be automated to speed up detection and analysis.

#### Comments
Using Microsoft Purview in conjunction with Azure Sentinel provides near real time incident alerts for Dataverse activities. Strategic focus should be put on what steps need to be performed when a security breach happens. Power Pages specifically is a window for outside attackers sp extra attention should be applied.

If you do not have a documented incident response plan, do not check this box.

### [X] **We provide role-based security training that enhances skills in maintaining baselines.**
It's important to develop and maintain a security training program to ensure the workload team is equipped with the appropriate skills to support the security goals and requirements. The team needs fundamental security training, but use what you can from your organization to support specialized roles. Having role-based security training compliance and drilling participation is part of your security baseline.

### [X] **We use the baseline to drive the use of preventative guardrails.**
Where possible, criteria in the baseline should have related guardrails that are established to enforce required security configurations, technologies, and operations. The guardrails should be based on internal factors, such as business requirements, risks, and asset evaluation, and external factors, such as benchmarks, regulatory standards, and your threat environment. This practice helps to minimize the risk of inadvertent oversight, and the risk of punitive fines from noncompliance.

### [X] **We have a clear understanding of all regulatory compliance requirements for our industry and location.**
Understanding regulatory compliance requirements helps ensure your workload operates within legal and industry-specific standards like Health Insurance Portability and Accountability Act (HIPAA) and Payment Card Industry Data Security Standard (PCI DSS). It helps mitigate legal risks, potential fines, and reputational damage from non-compliance. Aligning with geographical compliance requirements such as General Data Protection Regulation (GDPR), the California Consumer Privacy Act (CCPA) and "Lei geral de proteção de dados" (LGPD), ensures your operations meet local laws and regulations.
