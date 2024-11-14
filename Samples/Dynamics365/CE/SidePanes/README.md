---
title: Client API and Side Panes
---
# Client API and Side Panes

## Summary

Model Driven and Dynamics 365 API provide developers the ability to extend forms and views to the side pane. This feature can be extended to include many different panes showing views, forms, etc.

<img src="https://raw.githubusercontent.com/aliyoussefi/Samples/main/Dynamics365/SidePanes/SidePane.JPG" style="zoom: 80%;" />

## Setup and Samples

From the official documentation, to work with side panes we need to work with the Client API.

The documentation here shows how to work with the API but not how to implement.

To implement, I went through the following steps:

1. Create a **new solution**
2. Create **two new web resources**
   1. JavaScript file called **sidepanes.js**
   2. Png file called **sample_product_icon**
3. Created a **new command** in a model driven app.
4. **Referenced the function** within sidepanes.js from the button command.
5. **Published**.



Once published I have the following button:

<img src="https://raw.githubusercontent.com/aliyoussefi/Samples/main/Dynamics365/SidePanes/SidePaneButtonLauncher.JPG" style="zoom: 80%;" />

The button press opens the side pane as shown:

<img src="https://raw.githubusercontent.com/aliyoussefi/Samples/main/Dynamics365/SidePanes/SidePane.JPG" style="zoom: 80%;" />

## References

[Creating side panes using client API in model-driven apps - Power Apps | Microsoft Docs](https://docs.microsoft.com/en-us/powerapps/developer/model-driven-apps/clientapi/create-app-side-panes)

[createPane (Client API reference) in model-driven apps - Power Apps | Microsoft Docs](https://docs.microsoft.com/en-us/powerapps/developer/model-driven-apps/clientapi/reference/xrm-app/xrm-app-sidepanes/createpane)

[sidePanes (Client API reference) in model-driven apps - Power Apps | Microsoft Docs](https://docs.microsoft.com/en-us/powerapps/developer/model-driven-apps/clientapi/reference/xrm-app-sidepanes)

[Power Apps App Side Panes Feedback discussion thre... - Power Platform Community (microsoft.com)](https://powerusers.microsoft.com/t5/Building-Power-Apps/Power-Apps-App-Side-Panes-Feedback-discussion-thread/m-p/1261035#M331075)

