# Reliability Assessment - Assess Workflows
## Question: How do you identify and rate the workload's flows?

Not all user and system flows within your workload are equally important. Before designing for reliability, it's important to assess and rank flows to reduce the risk of overengineering or, conversely, not focusing enough on the most important flows.

### Comments
*This question focuses on Power Platform but should consider Dynamics 365 workflows as well. I would also consider any business logic into this. Meaning, thinking from a pure transactional stand point, what logic is invoked. For instance, on create of an opportunity in your sales data, could you identify all dependent services and actions? Are they designed to roll back any previous changes if an error occurs? Think about the full transaction as you respond to the below responses.*

## Question Responses

### [X] **We identify all user and system flows in the workload.**
The output of identifying all user and system flows is a catalog of all the flows in your workload. This identification process requires you to map out every user interaction and process within a system from beginning to end. This mapping is a prerequisite for identifying critical flows.

#### Comments
*This question is focused on Power Platform and Power Automate flows. Flows can be created and owned by users and flows.*

*That said, consider Dynamics 365 workflows. These can be designed to run under the executing user.*

*Overall, document each trigger to understand the scope.*

**NOTE - If you have the Center of Excellence and do not use Dataverse plug-ins or workflows, check this box. If you do not have the CoE and/or can't identify workflows and the scope they run under, do not check the box.**

### [X] **We know the business process or processes that each flow supports.**
Business processes are a series of tasks to achieve an output, such as expense reporting, annual leave management, order fulfillment, or inventory control. The identification of business processes for each flow involves mapping flows to one or more business processes. This mapping helps you understand the importance of each flow to the business.

#### Comments
*Can you align all business logic to a specific business process. Is there a team who is responsible for this. Ensure all workflows in the application or environment serve a purpose. They should all be documented and tied to a team that will support them and a business group who will define requirements.*

### [X] **We know the business impact of each flow.**
The identification of the business impact of each flow is essential for understanding how each flow contributes to key business objectives. Business impact could include revenue generation, customer satisfaction, or operational efficiency. You need to understand both the positive and negative impact of each flow.

#### Comments
*Using tools like the Center of Excellence or other operational management tools, you should be able to apply a criticality to each workflow.* 

**NOTE - If you have not done this, do not check the box and begin the Business Criticality Analysis.**

### [X] **We know the process owner and stakeholders for each flow.**
The process owner for a flow is the individual who's responsible for the successful execution of a given process. They're responsible for that process and the flows that support it. You might have a responsibility assignment matrix (RAM) or Responsible, Accountable, Consulted, and Informed (RACI) matrix that identifies process owners and stakeholders. Typically, process owners are responsible or accountable for a process, and you consult or inform stakeholders.

#### Comments
*Have you completed a RACI for each business process in your environment?* 

**NOTE - If you do not feel you have visibility into who is responsible if something breaks, do not check the box.**

### [X] **We know the escalation path for each flow.**
The identification of escalation paths is about determining channels for escalating issues related to a flow. Issues that need escalation could be urgent updates, security concerns, degradations, or technical incidents. The goal of identifying an escalation path is to ensure timely and effective resolution of issues.

#### Comments
*This one is straight forward. I would add that to feel confident you have an escalation plan in place to run drills and ensure each component in the workflow is notifying the proper escalation path when an issue comes up.* 

**NOTE - If an escalation path is not in place, do not check the box.**

### [X] **We assigned a criticality rating to each flow.**
A detailed evaluation of flow importance relative to the overall business impacts allows you to assign a criticality rating to each flow. You can use quantitative or qualitative criticality ratings. The purpose is to sort the flows by priority and assign a label that allows you to identify the critical flows. You can use the following criticality descriptions to assign your critical ratings: Critical (high criticality), Important (medium criticality), Productivity (low criticality).

#### Comments
*Using tools like the Center of Excellence, document each flow.* 

**NOTE - If you have not done this, do not check this box.**
