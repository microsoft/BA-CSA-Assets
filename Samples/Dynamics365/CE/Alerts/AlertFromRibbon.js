/*================================================================================================================================
This Sample Code is provided for the purpose of illustration only and is not intended to be used in a production environment.
THIS SAMPLE CODE AND ANY RELATED INFORMATION ARE PROVIDED "AS IS" WITHOUT WARRANTY OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A PARTICULAR PURPOSE.
We grant You a nonexclusive, royalty-free right to use and modify the Sample Code and to reproduce and distribute the object code form of the Sample Code, provided that You agree: (i) to not use Our name, logo, or trademarks to market Your software product in which the Sample Code is embedded; (ii) to include a valid copyright notice on Your software product in which the Sample Code is embedded; and (iii) to indemnify, hold harmless, and defend Us and Our suppliers from and against any claims or lawsuits, including attorneys fees, that arise or result from the use or distribution of the Sample Code.
=================================================================================================================================*/
/****************************
 * AlertFromRibbon.js file for Ribbon Alerting examples
 * Author: Ali Youssefi (ali.youssefi@microsoft.com)
 * //References
 *****************************/
function D365OpenAlertFromRibbon(primaryControl){
    alert('test');

}

function D365OpenAlertDialogFromRibbon(primaryControl){
    var formContext = primaryControl;
    // Perform operations using the formContext object
    var alertStrings = { confirmButtonLabel: "Yes", text: "This is an alert.", title: "Sample title" };
    var alertOptions = { height: 120, width: 260 };
    Xrm.Navigation.openAlertDialog(alertStrings, alertOptions).then(
        function (success) {
            console.log("Alert dialog closed");
        },
        function (error) {
            console.log(error.message);
        }
    );
}