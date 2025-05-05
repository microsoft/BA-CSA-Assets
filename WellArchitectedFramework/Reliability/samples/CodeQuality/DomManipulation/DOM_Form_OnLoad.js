function onload(formContext){
    //incorrect
    window.document.getElementById("name").value = "John Doe";
    Xrm.Page.getAttribute("name").setValue("John Doe");
    Mscrm.FormControl.getControl("name").setDisabled(true);
    Xrm.Page.getControl("name").setDisabled(true);
    var test = formContext.getAttribute("name").getValue();
    //correct


}