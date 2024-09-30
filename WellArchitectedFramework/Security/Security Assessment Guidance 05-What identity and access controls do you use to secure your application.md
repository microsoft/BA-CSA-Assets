# Security Assessment - Zero Trust
## Question: What identity and access controls do you use to secure your application?

Ensure that every attempt to access the workload is properly identified and scrutinized against an agreed-upon zero-trust policy set. Evaluate each access attempt based on changing conditions that can occur from one instance to the next. Avoid using long-lived tokens unless there's a proper continuous evaluation process in place and a working method for revoking them that all applications honor.

### Comments


## Question Responses

### [X] **We identified all identity personas for this workload and avoid hard-coded credentials where possible.**
A comprehensive identity design is crucial to ensure that all identities, including diverse user personas, are properly authenticated when they access the workload. This design addresses outside-in access, where end users and operators require secure authentication, and inside-out access, where the workload itself needs to authenticate against other resources. It establishes a robust identity management framework, and it enhances security and access control for the entire system.

#### Comments



#### References


### [X] **We separate the control plane and the data access plane.**
Defining actions for authorization helps ensure each authenticated identity in the workload has clearly defined and authorized capabilities that span data plane and control plane access. Data plane access includes data transfer, data source interactions, and resource access. Control plane access governs the creation, modification, or deletion of Power Platform resources and their configuration. By specifying and authorizing actions in both planes, you establish precise access controls and enhance your organization's ability to safeguard resources, prevent unauthorized operations, and maintain compliance.

### [X] **We use role-based access control (RBAC) for this workload, and we've clearly defined roles and responsibilities.**
Implementing role-based authorization helps ensure each identity's access aligns with its responsibilities. This approach enhances security by tailoring permissions based on roles to reduce the risk of unauthorized actions. Understanding the extent of an identity's role and its actions helps you make informed authorization decisions and reduce potential security breaches. By limiting access to what is needed, you minimize the attack surface and maintain control over resource access. Role-based authorization also streamlines access management to enable efficient monitoring and auditing of user activities.

### [X] **We use attribute-based access controls to enforce just-in-time (JIT) and just-enough-access (JEA) principles.**
Implementing Conditional Access choices helps ensure identities are granted access based on specific criteria like time and privilege. By restricting access duration and privileges, you enhance security measures by reducing the likelihood of prolonged unauthorized control. Considering additional conditions like device, network, and access location make access policies more precise, which reinforces overall control. Strong access controls like those based on user identity, device health, risk, and data classification help detect and block unauthorized access attempts effectively.

### [X] **We isolated critical impact accounts and strictly enforce privilege separation and access reviews.**
In safeguarding critical impact accounts, it's imperative to recognize that administrative identities pose security risks, given their privileged access to essential systems and applications. Any compromise or misuse of these accounts can have severe consequences for an organization's information systems and overall business operations. As a result, the security of administrative access is a top priority.

### [X] **We implement identity lifecycle management and have a documented offboarding process.**
Effective identity lifecycle management is fundamental for revoking or deleting access promptly during team or software changes. This management spans various domains like source control, data, and control planes. Establish a comprehensive identity governance process for offboarding digital identities, including high-privileged and external users, with regular and automated access reviews to prevent lingering permissions.