
var _executionContext = null;
function loadSideDialog(executionContext) {
        //Connect to record via API
        //poll record every 5 seconds
        //if record sentiment is 8 or 9 then open side dialog
    
       var allPanes = Xrm.App.sidePanes.getAllPanes()._getAll();
    _executionContext = executionContext;
        executionContext.getFormContext().getAttribute("msdyn_customersentimentlabel").addOnChange(function () {
            var recId = executionContext._formContext._data._entity.getId();
            if (result.msdyn_customersentimentlabel == 8 || result.msdyn_customersentimentlabel == 9) {
                Xrm.App.sidePanes.createPane({
                    title: "Quality Call Score Card",
                    paneId: "scorecard" + recId,
                    canClose: true,
                    width: 500
                }).then((pane) => {
                    pane.navigate({
                        pageType: "custom",
                        name: "demo_testsavestatus4_fad6b",
                        entityname: executionContext._formContext._data._entity.getEntityName(),
                        recordId: recId
                    })
                });
        }});


       for (var i = 0; i < allPanes.length; i++) {
           //debugger;
           console.log(allPanes[i].paneId);
           if (allPanes[i].paneId.includes("scorecard{")) {
               allPanes[i].close();
           }
       }




   }

function recursion(executionContext) {
    var recId = executionContext._formContext._data._entity.getId();
//'{C614AC9F-0C58-46DB-89F0-687C1E309680}'
    Xrm.WebApi.retrieveRecord("msdyn_ocliveworkitem", recId, "?$select=msdyn_customersentimentlabel").then(
     function success(result) {
         if (result.msdyn_customersentimentlabel == 8 || result.msdyn_customersentimentlabel == 9) {
             Xrm.App.sidePanes.createPane({
                 title: "Quality Call Score Card",
                 paneId: "scorecard" + recId,
                 canClose: true,
                 width: 500
             }).then((pane) => {
                 pane.navigate({
                    pageType: "webresource",
                    webresourceName: "kim2_/html/copilotchat.html",
                     entityname: executionContext._formContext._data._entity.getEntityName(),
                     recordId: recId
                 })
             });
        }
         // perform operations on record retrieval
     },
     function (error) {
         console.log(error.message);
         // handle error conditions
     }
 );
    }

    var sleep = duration => new Promise(resolve => setTimeout(resolve, duration))
    var poll = (promiseFn, duration) => promiseFn().then(
                 sleep(duration).then(() => poll(promiseFn, duration)))
    
    // Greet the World every second
    poll(() => new Promise(() => recursion(_executionContext)), 5000)