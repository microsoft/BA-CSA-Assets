# Microsoft Copilot Studio and Dynamics 365 
This sample shows how to launch a side pane in Dynamics 365 containing a Microsoft Copilot Studio Agent.
This sample will connect to the XRM namespace, pulling information from the form, user, environment, etc. form a JS file.
This agent will accept information from the JS file and set global variables through question nodes.

# Business Value
- Imagine having an agent that can monitor sentiment of a customer service interaction and launch when the sentiment is negative.
- Sales agents that can launch when an opportunity is nearing close and offer incentives based on knowledge of previous won opportunities.
- Sales agents helping to build account planning materials without human interaction, i.e. autonomous


# Steps
1. Create a Copilot Studio Agent
2. Create a web resource (HTML) using the CopilotChat.html file in this sample.
3. Create a web resource (JS) using CopilotDynamics365.js file in this sample.
4. Modify the JS file to point to your HTML web resource (publisher prefix, etc.)
5. Modify the HTML file to use the endpoint from the Direct Line Integration in the Agent channel.
6. Modify the HTML file to modify the payload sent to the agent to include relevant information.
7. Build a question node in the Conversation Start topic and map to the property name.