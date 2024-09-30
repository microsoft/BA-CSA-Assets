# Security Assessment - Security Requirements
## Question: How do you secure the code, development environment, and development lifecycle?

At the core of a workload are the components that implement the business logic. These components can be a mix of low-code elements like canvas apps and flows, and code-first elements like plugins. All components that make up your workload must be free of security defects to ensure confidentiality, integrity, and availability.

### Comments


## Question Responses

### [X] **We identified all business and security requirements of the application.**
Protecting the data and integrity of your application should be a core requirement through every phase of the development lifecycle. Document the security requirements and the guardrails needed to ensure that the application complies with those requirements during this phase.
#### Comments



#### References


### [X] **We adopted threat modelling processes, ranked identified threats based on organizational impact, mapped them to mitigations, and communicated these to stakeholders**
Threat modeling is an engineering technique that you can use to help identify threats, attacks, vulnerabilities, and countermeasures that can affect an application. Threat modeling consists of defining security requirements, identifying threats, mitigating threats, and validating threat mitigation. Microsoft uses the STRIDE framework for threat modeling. Tools like the Microsoft Threat Modeling Tool can help.

### [X] **We maintain a manifest that enumerates all dependencies, frameworks, and libraries that the workload uses, and we update the manifest regularly.**
To ensure that the security requirements are being adhered to by all contributors to the software, maintain a list of approved and unapproved frameworks, libraries, vendors, and versions of each. When possible, place guardrails in the development pipelines to support this list.

### [X] **We ensure developers meet security training requirements before they gain access to environments and solutions.**
Many vulnerabilities are simply coding errors or oversights due to lack of training or adherence to policy. Ensuring that your developers meet a training benchmark as criteria for access to the source code helps prevent errors and oversights, and helps limit exposure.

### [X] **We safeguard development environments by controlling access to Power Platform environments, solutions, code, pipeline tools, and source control systems.**
Supply chain attacks and inconsistent configurations are two of many threats that occur when your development environment isn't isolated and well protected. Developers should use consistent and protected environments while accessing or authoring production code.

### [X] **We promptly remove unused and legacy assets from the development environment.**
Many supply chain attacks occur when the attacker simply "lives off the land," meaning they use tools or vulnerabilities introduced by the developer instead of downloading malware to meet their objectives. An important step in preventing these attacks is to clean and sanitize your environment regularly.

### [X] **We ensure data from production environments isn't available to development or test environments.**
Production data often contains personally identifiable data or other information that can be valuable to an attacker. Always use sanitized or fictionally simulated data in development or test environments.

### [X] **We maintain a catalog of all deployed assets and their versions.**
This information is useful during incident triage, when you mitigate issues, and when you get the system back to a working state. Versioned assets can also be compared against published Common Vulnerabilities and Exposures (CVE) notices. You should use automation to perform these comparisons.

### [X] **We make sure security posture doesn't decay over time.**
We continuously assess and improve the security of the software development process by taking into account code reviews, feedback, lessons learned, and evolving threats, as well as new features made available by Power Platform.
