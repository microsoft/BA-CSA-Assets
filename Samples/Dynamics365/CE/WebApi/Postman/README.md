---
title: Web API and Postman
---
# IMPORTANT

This sample relies on pre work found here:

Online:

[Set up a Postman environment (Microsoft Dataverse for Apps) - Power Apps | Microsoft Docs](https://docs.microsoft.com/en-us/powerapps/developer/data-platform/webapi/setup-postman-environment#connect-with-your-common-data-service-environment)

On-Premise:

[Set up a Postman environment (Developer Guide for Dynamics 365 Customer Engagement) | Microsoft Docs](https://docs.microsoft.com/en-us/dynamics365/customerengagement/on-premises/developer/webapi/setup-postman-environment?view=op-9-1)

Here are steps copied as of 8/25/2021:

## Connect with your Dataverse environment

This environment uses a client ID for an application that is registered for all Dataverse environments.

You can use the `clientid` and `callback` values supplied in these instructions. However, when building your own application, you should register your own Azure Active Directory (Azure AD) application.

To register your own Azure AD application, see the steps described in [Walkthrough: Register a Dataverse app with Azure Active Directory](https://docs.microsoft.com/en-us/powerapps/developer/data-platform/walkthrough-register-app-azure-active-directory).

Use these steps to create a Postman environment that you can use to connect with your Dataverse instance:

1. Launch the Postman desktop application.
2. Select the **Environment Options** gear icon in the top-right corner.
3. In the **Manage Environments** dialog box, select the **Add** button to add a new environment.

![Click on Add button to add a new Postman environment.](https://docs.microsoft.com/en-us/powerapps/developer/data-platform/webapi/media/postman-manage-env.png)

1. In the dialog box that opens, type a name for the environment. Then add the following key-value pairs into the editing space.

   | Variable name | Value                                                        |
   | :------------ | :----------------------------------------------------------- |
   | `url`         | `https://<add your environment name, like 'myorg.crm'>.dynamics.com` |
   | `clientid`    | `51f81489-12ee-4a9e-aaae-a2591f45987d`                       |
   | `version`     | `9.0`                                                        |
   | `webapiurl`   | `{{url}}/api/data/v{{version}}/`                             |
   | `callback`    | `https://callbackurl`                                        |
   | `authurl`     | `https://login.microsoftonline.com/common/oauth2/authorize?resource={{url}}` |

    Note

   For [relevance search](https://docs.microsoft.com/en-us/powerapps/developer/data-platform/webapi/relevance-search), specify a version of 1.0 and a webapiurl of {{url}}/api/search/v{{version}}/.

   ![Create a new Postman environment to connect with Online instance.](https://docs.microsoft.com/en-us/powerapps/developer/data-platform/webapi/media/postman-add-online-env.png)

2. Replace the instance URL placeholder value with the URL of your Dataverse environment, and select **Add** to save the environment.

3. Close the **Manage environments** dialog box.

### Generate an access token to use with your environment

To connect using **OAuth 2.0**, you must have an access token. Use the following steps to get a new access token:

1. Make sure the new environment you created is selected.

2. Select the **Authorization** tab.

3. Set the **Type** to **OAuth 2.0**.

4. Verify that you have selected the environment that you created.

5. Select **Get New Access Token**

   ![In Authorization tab, set Type to OAuth 2.0.](https://docs.microsoft.com/en-us/powerapps/developer/data-platform/webapi/media/postman-set-type.png)

6. Set the following values in the dialog box. Select `Implicit` from the **Grant Type** drop-down menu. You can set the **Token Name** to whatever you like, and leave other keys set to default values.

   ![Get new Access Token.](https://docs.microsoft.com/en-us/powerapps/developer/data-platform/webapi/media/postman-access-token.png)

    Note

   If you are configuring environments in Postman for multiple Dataverse instances using different user credentials, you might need to delete the cookies cached by Postman. Select the **Cookies** link, which can be found under the **Send** button, and remove the saved cookies from the **Manage Cookies** dialog box.
   ![Remove Cookies.](https://docs.microsoft.com/en-us/powerapps/developer/data-platform/webapi/media/postman-cookies.png)
   Some of these cookies are very persistent. You can delete some of them in groups, but you might have to delete others individually. You might need to do this twice to ensure that no cookies remain.

7. Select **Request Token**. When you do this, an Azure Active Directory sign-in page appears. Enter your username and password.

8. After the token is generated, scroll to the bottom and select **Use Token**. This closes the **Manage Access Tokens** dialog box.

9. After you have added a token, you can select which token to apply to requests. On the **Available Tokens** drop-down list, select the token you have just created. The Authorization header gets added to the Web API request.

See [Test your connection](https://docs.microsoft.com/en-us/powerapps/developer/data-platform/webapi/setup-postman-environment#test-your-connection) for steps to verify your connection.

## Test your connection

Create a new Web API request to test the connection with your Dataverse instance. Use the [WhoAmI function](https://docs.microsoft.com/en-us/dynamics365/customer-engagement/web-api/whoami):

1. Select `GET` as the HTTP method and add `{{webapiurl}}WhoAmI` in the editing space. ![WhoAmI function request.](https://docs.microsoft.com/en-us/powerapps/developer/data-platform/webapi/media/postman-whoami-request.png)
2. Select **Send** to send this request.
3. If your request is successful, you see the data from the [WhoAmIResponse ComplexType](https://docs.microsoft.com/en-us/dynamics365/customer-engagement/web-api/whoamiresponse) that is returned by the [WhoAmI Function](https://docs.microsoft.com/en-us/dynamics365/customer-engagement/web-api/whoami).