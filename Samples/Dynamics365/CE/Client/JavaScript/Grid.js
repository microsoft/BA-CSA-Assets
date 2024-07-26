function OpenEntityForm(executionContext){
    var entityFormOptions = {};
    entityFormOptions["entityName"] = "contact";

    // Set default values for the Contact form
    var formParameters = {};
    formParameters["firstname"] = "Sample";
    formParameters["lastname"] = "Contact";
    formParameters["fullname"] = "Sample Contact";
    formParameters["emailaddress1"] = "contact@adventure-works.com";
    formParameters["jobtitle"] = "Sr. Marketing Manager";
    formParameters["donotemail"] = "1";
    formParameters["description"] = "Default values for this record were set programmatically.";

    // Set lookup column
    formParameters["preferredsystemuserid"] = "3493e403-fc0c-eb11-a813-002248e258e0"; // ID of the user.
    formParameters["preferredsystemuseridname"] = "Admin user"; // Name of the user.
    formParameters["preferredsystemuseridtype"] = "systemuser"; // Table name. 
    // End of set lookup column

// Open the form.
Xrm.Navigation.openForm(entityFormOptions, formParameters).then(
    function (success) {
        console.log(success);
    },
    function (error) {
        console.log(error);
    });
}

function CreateReservationSidePane(executionContext){
    Xrm.App.sidePanes.createPane({
        title: "Reservations",
        imageSrc: "WebResources/mce_WebResources/sample_product_icon",
        paneId: "ReservationList",
        canClose: false
    }).then((pane) => {
        pane.navigate({
            pageType: "webresource",
            name: "msdyn_appmanagementcontrol" //WebResources/msdyn_appmanagementcontrol
        })
    });

    Xrm.App.sidePanes.createPane({
        title: "Accounts",
        imageSrc: "WebResources/mce_WebResources/sample_product_icon",
        paneId: "AccountList",
        canClose: false
    }).then((pane) => {
        pane.navigate({
            pageType: "entitylist",
            entityName: "account" //WebResources/msdyn_appmanagementcontrol
        })
    });
}

function ChangeTab(executionContext){
    var formContext = executionContext.getFormContext(); // get formContext
    var tabObj = formContext.ui.tabs.get(1);
    tabObj.setFocus();
}