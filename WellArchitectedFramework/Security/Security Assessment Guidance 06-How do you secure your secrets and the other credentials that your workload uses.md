# Security Assessment - Securing Secrets
## Question: How do you secure your secrets and the other credentials that your workload uses?

Protect application secrets, such as configuration settings, certificates, signing keys, and API keys, by hardening their storage and by restricting and auditing their access and manipulation.

### Comments


## Question Responses

### [X] **We don't share secrets between development, testing, and production environments.**
Using the same secrets and credentials in different development environments creates a security risk. Attackers can exploit a shared key to access higher-level data from lower-level environments.

#### Comments



#### References


### [X] **We store application secrets and credentials separately from the workload in a secret management system that uses strong encryption.**
If you need to store application secrets, keep them outside the source code and rotate them periodically. Use a secret management system, such as Azure Key Vault, to help ensure that any potential attackers who gain access to your source code don't also gain access to your secrets. Also store certificates in Key Vault or in the certificate store of the operating system. For example, we don't recommend storing an X.509 certificate in a Personal Information Exchange (PFX) file on disk.

### [X] **We have a secure process in place for distributing secrets to the locations where they're used.**
If you generate or manage secrets, you should distribute them securely. You can use tools that provide a way for you to share secrets safely with your organization or external partners. If you don’t have such tools, you should follow a secure process to hand over credentials to authorized recipients.

### [X] **We use automation to detect and remove hard-coded secrets from our source code and build artifacts.**
You should use tools that automatically and regularly check your application code and build artifacts for any exposed secrets. Scan for credentials with Git pre-commit hooks before you commit source code. Make sure no secrets are accidentally logged in your application logs, and clean them regularly. You can also use processes like peer reviews to reinforce detection.

### [X] **We rotate keys and expire old keys on a regular basis.**
You should have a process in place that maintains secret hygiene. The longevity of your secrets influences your operational procedures. The longer that a key remains valid, the more likely that it's discovered and used in a compromise. Always rotate keys as often as possible.

### [X] **We have a method in place to respond to secret rotation without disruption.**
You need to know your organization's secret rotation plan and policies so that you can update your secret without affecting users. You also need to handle possible downtime that occurs when you change secrets. You can use retry logic or concurrent access patterns in your code to handle this downtime.

### [X] **We employ role-based access control (RBAC) to manage access to keys.**
When your workloads access a key or credential from your secret management system, they should use a set of RBAC rules to perform application identity authentication.