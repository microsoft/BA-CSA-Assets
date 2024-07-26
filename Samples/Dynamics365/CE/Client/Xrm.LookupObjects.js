/*================================================================================================================================
This Sample Code is provided for the purpose of illustration only and is not intended to be used in a production environment.
THIS SAMPLE CODE AND ANY RELATED INFORMATION ARE PROVIDED "AS IS" WITHOUT WARRANTY OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A PARTICULAR PURPOSE.
We grant You a nonexclusive, royalty-free right to use and modify the Sample Code and to reproduce and distribute the object code form of the Sample Code, provided that You agree: (i) to not use Our name, logo, or trademarks to market Your software product in which the Sample Code is embedded; (ii) to include a valid copyright notice on Your software product in which the Sample Code is embedded; and (iii) to indemnify, hold harmless, and defend Us and Our suppliers from and against any claims or lawsuits, including attorneys fees, that arise or result from the use or distribution of the Sample Code.
=================================================================================================================================*/
/****************************
 * XrmUtilityExamples.js file for XrmUtilityExamples code
 * Author: Ali Youssefi (ali.youssefi@microsoft.com)
 * //References
 * https://www.itaintboring.com/dynamics-crm/filtered-nn-lookup/
 * https://xrmdynamicscrm.wordpress.com/2020/06/07/dynamics-365-sub-grid-add-existing-look-up-view-nn/
 *****************************/
 XrmUtilityExamples = {};
 if (typeof (XrmUtilityExamples) === 'undefined') {
     XrmUtilityExamples = {};
 }
 XrmUtilityExamples.LookupAndGrid = {
     PFEfilterAddExistingObjects: function (selectedEntityTypeName, selectedControl, firstPrimaryItemId, primaryControl) {
         // https://docs.microsoft.com/en-us/powerapps/developer/model-driven-apps/clientapi/reference/xrm-utility/lookupobjects
         //define data for lookupOptions
 
         console.log('Inside of PFEfilterAddExistingObjects');
         if (primaryControl.getAttribute('aac_pcp_acct1').getValue() !== null){
             var accountID = primaryControl.getAttribute('aac_pcp_acct1').getValue()[0].id;
             console.log(accountID);
     
             var primaryId = "";
                     /*
         Property Name        Type        Required        Description
         allowMultiSelect        Boolean        No        Indicates whether the lookup allows more than one item to be selected.
         defaultEntityType        String        No        The default entity type to use.
         defaultViewId        String        No        The default view to use.
         disableMru        Boolean        No        Decides whether to display the most recently used(MRU) item.
         Available only for Unified Interface.
         entityTypes        Array        Yes        The entity types to display.
         filters        Array of objects        No        Used to filter the results. Each object in the array contains the following attributes:
             filterXml: String. The FetchXML filter element to apply.
             entityLogicalName: String. The entity type to which to apply this filter.
         searchText        String        No        Indicates the default search term for the lookup control. This is supported only on Unified Interface.
         showBarcodeScanner        Boolean        No        Indicates whether the lookup control should show the barcode scanner in mobile clients.
         viewIds        Array        No        The views to be available in the view picker. Only system views are supported.
         */
         var lookupOptions = 
         {
             //defaultEntityType: "account",
             entityTypes: ["opportunity"],
             allowMultiSelect: true,
             //defaultViewId:"0D5D377B-5E7C-47B5-BAB1-A5CB8B4AC10",
             //viewIds:["0D5D377B-5E7C-47B5-BAB1-A5CB8B4AC10","00000000-0000-0000-00AA-000010001003"],
             //searchText:"Allison",
             disableMru: true,
             showNew: false,
             filters: [{filterXml: "<filter type='or'><condition attribute='parentaccountid' operator='eq' value='" + accountID + "' /></filter>",
                         entityLogicalName: "opportunity"}]
         };
 
         // Get account records based on the lookup Options
         Xrm.Utility.lookupObjects(lookupOptions).then(function (results) {
             XrmUtilityExamples.LookupAndGrid.PFEassociateAddExistingResults('aac_Opportunity_aac_OpporPCPMatching_aac_c_pcp', 'aac_c_pcps', 'opportunities', selectedEntityTypeName, firstPrimaryItemId.replace("{", "").replace("}", ""), selectedControl, results, 0);
         },
         function(error){console.log(error);});
         }
         else{
             console.log('ayw_account is not set, do not filter subgrid.');
         }
 
 
     },
     PFEassociateAddExistingResults: function (relationshipName, primaryEntitySetName, relatedEntitySetName, relatedEntity, parentRecordId, gridControl, results, index) {
         var globalContext = Xrm.Utility.getGlobalContext();
         if (index >= results.length) {
             if (gridControl) { gridControl.refresh(); }
 
             // Clear the final notification after 2 seconds
             setTimeout(function () {
                 //  Xrm.Page.ui.clearFormNotification("associate");
             }, 2000);
 
             return;
         }
         //Xrm.Page.ui.setFormNotification("Associating record " + (index + 1) + " of " + results.length, "INFO", "associate");
 
         var lookupId = results[index].id.replace("{", "").replace("}", "");
         var lookupEntity = results[index].entityType || results[index].typename;
 
         var primaryId = parentRecordId;
         var relatedId = lookupId;
         if (lookupEntity.toLowerCase() != relatedEntity.toLowerCase()) {
             primaryId = lookupId;
             relatedId = parentRecordId;
         }
 
         var association = { '@odata.id': globalContext.getClientUrl() + "/api/data/v9.0/" + relatedEntitySetName + "(" + relatedId + ")" };
 
         var req = new XMLHttpRequest();
         req.open("POST", globalContext.getClientUrl() + "/api/data/v9.0/" + primaryEntitySetName + "(" + primaryId + ")/" + relationshipName + "/$ref", true);
         req.setRequestHeader("Accept", "application/json");
         req.setRequestHeader("Content-Type", "application/json; charset=utf-8");
         req.setRequestHeader("OData-MaxVersion", "4.0");
         req.setRequestHeader("OData-Version", "4.0");
         req.onreadystatechange = function () {
             if (this.readyState === 4) {
                 req.onreadystatechange = null;
                 index++;
                 if (this.status === 204 || this.status === 1223) {
                     // Success
                     // Process the next item in the list
                     XrmUtilityExamples.LookupAndGrid.PFEassociateAddExistingResults(relationshipName, primaryEntitySetName, relatedEntitySetName, relatedEntity, parentRecordId, gridControl, results, index);
                 }
                 else {
                     // Error
                     var error = JSON.parse(this.response).error.message;
                     if (error == "A record with matching key values already exists.") {
                         // Process the next item in the list
                         XrmUtilityExamples.LookupAndGrid.associateAddExistingResults(relationshipName, primaryEntitySetName, relatedEntitySetName, relatedEntity, parentRecordId, gridControl, results, index);
                     }
                     else {
                         Xrm.Utility.alertDialog(error);
                         // Xrm.Page.ui.clearFormNotification("associate");
                         if (gridControl) { gridControl.refresh(); }
                     }
                 }
             }
         };
         req.send(JSON.stringify(association));
     },
     associateAddExistingResults: function (relationshipName, primaryEntitySetName, relatedEntitySetName, relatedEntity, parentRecordId, gridControl, results, index) {
         var globalContext = Xrm.Utility.getGlobalContext();
         if (index >= results.length) {
             if (gridControl) { gridControl.refresh(); }
 
             // Clear the final notification after 2 seconds
             setTimeout(function () {
                 //  Xrm.Page.ui.clearFormNotification("associate");
             }, 2000);
 
             return;
         }
         //Xrm.Page.ui.setFormNotification("Associating record " + (index + 1) + " of " + results.length, "INFO", "associate");
 
         var lookupId = results[index].id.replace("{", "").replace("}", "");
         var lookupEntity = results[index].entityType || results[index].typename;
 
         var primaryId = parentRecordId;
         var relatedId = lookupId;
         if (lookupEntity.toLowerCase() != relatedEntity.toLowerCase()) {
             primaryId = lookupId;
             relatedId = parentRecordId;
         }
 
         var association = { '@odata.id': globalContext.getClientUrl() + "/api/data/v9.0/" + relatedEntitySetName + "(" + relatedId + ")" };
 
         var req = new XMLHttpRequest();
         req.open("POST", globalContext.getClientUrl() + "/api/data/v9.0/" + primaryEntitySetName + "(" + primaryId + ")/" + relationshipName + "/$ref", true);
         req.setRequestHeader("Accept", "application/json");
         req.setRequestHeader("Content-Type", "application/json; charset=utf-8");
         req.setRequestHeader("OData-MaxVersion", "4.0");
         req.setRequestHeader("OData-Version", "4.0");
         req.onreadystatechange = function () {
             if (this.readyState === 4) {
                 req.onreadystatechange = null;
                 index++;
                 if (this.status === 204 || this.status === 1223) {
                     // Success
                     // Process the next item in the list
                     XrmUtilityExamples.LookupAndGrid.associateAddExistingResults(relationshipName, primaryEntitySetName, relatedEntitySetName, relatedEntity, parentRecordId, gridControl, results, index);
                 }
                 else {
                     // Error
                     var error = JSON.parse(this.response).error.message;
                     if (error == "A record with matching key values already exists.") {
                         // Process the next item in the list
                         XrmUtilityExamples.LookupAndGrid.associateAddExistingResults(relationshipName, primaryEntitySetName, relatedEntitySetName, relatedEntity, parentRecordId, gridControl, results, index);
                     }
                     else {
                         Xrm.Utility.alertDialog(error);
                         // Xrm.Page.ui.clearFormNotification("associate");
                         if (gridControl) { gridControl.refresh(); }
                     }
                 }
             }
         };
         req.send(JSON.stringify(association));
     }
 };
 