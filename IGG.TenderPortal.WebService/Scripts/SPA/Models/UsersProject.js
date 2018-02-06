app.factory('UsersProject', function (Ajax, ErrorHandler) {

    /**
    @class: Definition of
    */
    var UsersProject = function () {
        //--- these fields will be created from server response
    };

 

    //---response object and this one must be the same, changing only necessary fields
    UsersProject.createFromResponse = function (response) {
        var up = response;
        up.beganWithProject = Date_ParseFromServer(response.beganWithProject);
        up.endedWithProject = Date_ParseFromServer(response.endedWithProject);
        return up;
    };

    /** Creates array of Milestone from given array of Milestone-s response from server    */
    UsersProject.createFromResponseArray = function (arrayOfResponses) {
        if (!arrayOfResponses) return null;
        //  console.log("UsersProject.createFromResponseArray", arrayOfResponses);
        var retArray = Array();
        for (var i = 0; i < arrayOfResponses.length; i++) {
            retArray.push(UsersProject.createFromResponse(arrayOfResponses[i]));
        }
        return retArray;
    }

    UsersProject.delete = function(userProject, OKf, ErrF){
    
        console.log("   UsersProject.delete  userProject = ", userProject);
        Ajax.sendDataPOST("/User/DeleteUserProject",
           userProject,
           function (response) {
               console.log("  Project.delete ", response);
               OKf(UsersProject.createFromResponse(response));
           },
           function (response) {
               console.error("  Project.delete ERROR ", response);
               ErrF(response);
             
           }
       );
    
    }

    UsersProject.save = function (userProject, OKf, ErrF){
    
        console.log("   UsersProject.delete  userProject = ", userProject);
        Ajax.sendDataPOST("/User/SaveUserProject",
           userProject,
           function (response) {
               console.log("  Project.delete ", response);
               OKf(response);
           },
           function (response) {
               console.error("  Project.delete ERROR ", response);
               ErrF(response);
             
           }
       );
    
    }
    
 

    UsersProject.AddIggerToProject = function (iggerID, projectID, OKf, ErrF) {

        console.log("   UsersProject.delete  iggerID = " + iggerID + "; projectID=" + projectID);
        Ajax.sendDataPOST("/User/AddIggerToProject",
           { IggerID: iggerID, ProjectID: projectID },
           function (response) {
               console.log("  Project.AddIggerToProject ", response);
               OKf(UsersProject.createFromResponse(response));
           },
           function (response) {
               console.error("  Project.AddIggerToProject ERROR ", response);
               ErrF(response);

           }
       );

    }





    return UsersProject;
});