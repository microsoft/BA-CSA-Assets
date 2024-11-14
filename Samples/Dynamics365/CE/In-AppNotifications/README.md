---
title: In-App Notifications Setup and Broadcast Use Cases
---
# In-App Notifications Setup and Broadcast Use Cases

## Summary

Model Driven and Dynamics 365 notifications provide a powerful and extremely flexible way to communicate with an organization's user base. These notifications allow managers, team leads, administrators to quickly send alerts to specific users. These alerts can be automated and depending on an organization's business needs finely tuned to provide actionable next steps.

In this article, we will explore use cases and how to setup a Model Driven App to use notifications using Power Automate Flow. We will look at ways to broadcast messages to specific users, teams and an entire user base using both no code and pro code methods.



## Use Cases for Individuals

The Dataverse allows organizations to build enterprise ready scalable cloud native applications for the Power Platform. Within this rich ecosystem organizations have solutions tailored to industry verticals including case management systems, business to business opportunity pipeline management, HR talent management, project operations, etc. Each of these solutions provide an opportunity to use in-app notifications to enhance the user experience and increase productivity.

### Case Management

In case management scenarios, alerts can be sent to customer service agents identifying service level agreements that are past due for a specific case. Actions can be added to quickly email the customer or review the case directly in Customer Service Hub.

<img src="https://raw.githubusercontent.com/aliyoussefi/Samples/main/Dynamics365/In-AppNotifications/_images/CustomerServiceHubExample.gif" style="zoom: 80%;" />

### Opportunity Pipeline Management

In sales opportunity scenarios, notifications can be sent out to sellers notifying them that an opportunity in their pipeline is nearing close. Sellers can quickly review details and access discount rates directly from SharePoint in hopes to win the opportunity.

<img src="https://raw.githubusercontent.com/aliyoussefi/Samples/main/Dynamics365/In-AppNotifications/_images/SalesHubExample.gif" style="zoom: 80%;" />

### Project Operations Management

In this scenario, one of the project's team members has submitted an expense that doesn't comply with the company's standards or project agreements. With in-app notifications, automation, such as Power Automate Flow, can quickly notify the team member that comments and receipts were not submitted.

<img src="https://raw.githubusercontent.com/aliyoussefi/Samples/main/Dynamics365/In-AppNotifications/_images/ProjectServiceExample.gif" style="zoom: 80%;" />

## Broadcast Use Case for Teams and All Users

Team managers need the ability to quickly notify members of new policy information, changing deadlines, newsletters, etc. Using teams within Dataverse, managers can notify each member one by one as detailed above. Depending on team size or the nature of the alert there may be a need to broadcast an alert to many users at once.

To do so, automation techniques such as the low code platform Power Automate to pro code platforms such as Azure Functions or Dataverse Custom Actions can be leveraged. The below samples detail various ways to achieve this functionality using Power Apps, Power Automate and Azure Data Factory.



## Setup and Samples

From the official documentation, to turn on in-app notifications we need to use the "AllowNotificationsEarlyAccess" setting in a Model Driven application. The example located [here](https://docs.microsoft.com/en-us/powerapps/developer/model-driven-apps/clientapi/send-in-app-notifications) shows how to do this with the Developer Tools available for most modern browsers.

```
fetch(window.origin + "/api/data/v9.1/SaveSettingValue()",{ method: "POST",    headers: {'Content-Type': 'application/json'},   body: JSON.stringify({AppUniqueName: "Your app unique name", SettingName:"AllowNotificationsEarlyAccess", Value: "true"})   });
```

In the sample above the AppUniqueName is the uniquename column in the App Modules table. For instance, for the Model Driven app "Sales Hub" you can replace "Your app unique name" with "msdynce_saleshub". This can be found using the Dataverse API for appmodules or by using Power Automate Flow and the Dataverse connector action "List rows" on the "Model-driven Apps" table.

### The SaveSettingValue Power Automate Flow

Within the sample repo located here, I have a Power Automate flow within two Dataverse solutions that will take the friendly name of the app (e.g. SalesHub) and set the value for you.

<img src="https://raw.githubusercontent.com/aliyoussefi/Samples/main/Dynamics365/In-AppNotifications/_images/SetSettingValueListRows.JPG" style="zoom: 80%;" />

The Power Automate flow uses the Dataverse connector action "Perform an unbound action" and the "SaveSettingValue" to set the appropriate setting.

<img src="https://raw.githubusercontent.com/aliyoussefi/Samples/main/Dynamics365/In-AppNotifications/_images/UnboundImage.JPG" style="zoom: 80%;" />

To run the Power Automate flow, open the flow and click the run button. Set the App to the friendly name, the Setting to "AllowNotificationsEarlyAccess" and the Value to true.

<img src="https://raw.githubusercontent.com/aliyoussefi/Samples/main/Dynamics365/In-AppNotifications/_images/RunSaveSettingValueFlowInputs.JPG" style="zoom: 80%;" />

NOTE: This flow can be extended for other settings if needed in the future using the Setting field.



### Broadcast messages with Power Automate Flow

The broadcast in-app notifications to all users flow shows how to get all users and send a notification.

<img src="https://raw.githubusercontent.com/aliyoussefi/Samples/main/Dynamics365/In-AppNotifications/_images/Broadcast%20In-App%20Notification%20to%20all%20users.JPG" style="zoom: 80%;" />

This Power Automate Flow is triggered from the Power Apps connector. This allows for use within a Power App as described below but also as a Child Flow.

The inputs for the Power App are related in the table below.

| Input              | Description                                                  |
| ------------------ | ------------------------------------------------------------ |
| Title              | The title of the notification. This will show in bold at the top of the notification. |
| Body               | The body of the notification. This will be under the title.  |
| IconType           | The icon of the notification. Refer to the Icon Type table below. |
| IconUrl (optional) | A custom icon URL for the notification.                      |
| ToastType          | The toast appearance of the notification. Refer to the Toast Type table below. |
| Data (optional)    |                                                              |
| Owner (optional)   |                                                              |
| Team (optional)    |                                                              |
| AllUsers           |                                                              |
| Expiry             |                                                              |

Below are the tables describing the number input in the body.

#### Toast Type

| Toast Type | Behavior                                                     | Value     |
| :--------- | :----------------------------------------------------------- | :-------- |
| Timed      | Notification appears for a brief duration and then disappears. (default 4 seconds) | 200000000 |
| Hidden     | Notification appears only in the notification center and not as a toast. | 200000001 |

#### Icon Type

| Icon Type | Value     |
| :-------- | :-------- |
| Info      | 100000000 |
| Success   | 100000001 |
| Failure   | 100000002 |
| Warning   | 100000003 |
| Mention   | 100000004 |
| Custom    | 100000005 |

### Dataverse Solutions



### Updating All Apps using PowerShell

[This PowerShell script](https://github.com/aliyoussefi/Samples/blob/main/Dynamics365/In-AppNotifications/PowerShell/01-ConnectToDataverseApiAndSetSettingValue.ps1) will loop through all apps in an organization and set the in-app notification enablement. It uses an application user to achieve this which requires an app registration as detailed here. Set the clientId, clientSecret and tenantId from the application in Azure and the orgUrl from the Power Platform Admin Center.

```
$orgUrl = "<org>.crm.dynamics.com" #This can be found in the Power Platform Admin Center under environments.
$clientId = "client or object ID in Azure"
$clientSecret = "Generated secret in Azure"
$tenantId = "tenant ID in Azure"
$aadUrl = "https://login.microsoftonline.com/$($tenantId)/oauth2/token"
```

The full PowerShell script can be found [here](https://github.com/aliyoussefi/Samples/blob/main/Dynamics365/In-AppNotifications/PowerShell/01-ConnectToDataverseApiAndSetSettingValue.ps1).

### 

### Managing security for notifications

The in-app notification feature uses three tables, and a user needs to have the correct security roles to receive notifications and send notifications to themselves, or other users.

| Usage                                                        | Needed table privileges                                      |
| :----------------------------------------------------------- | :----------------------------------------------------------- |
| User has no in-app notification bell and receives no in-app notifications toasts | None: Read privilege on app notification table.              |
| User can receive in-app notifications                        | - Basic: Read privilege on app notification table. - Create and read privilege on model-driven app user setting. |
| User can send in-app notifications to self                   | - Basic: Create privilege on app notification table. - Write and append privilege on model-driven app user setting. - Append privilege on setting definition. |
| User can send in-app notifications to others                 | Read privilege with Local, Deep, or Global access level on app notification table based on the receiving user's business unit. |