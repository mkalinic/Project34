app.factory('ProjectFile', function (Ajax, ErrorHandler) {

    /**
    @class: Definition of Project
    */
    var ProjectFile = function () {
        //--- all fields will be created from server response

    };


    //---response object and this one must be the same, changing only necessary fields
    var createFromResponse = function (response) {
        var thisone = response;
        thisone.dateModified = Date_ParseFromServer(response.dateModified);
        thisone.dateUploaded = Date_ParseFromServer(response.dateUploaded);
        return thisone;
    };




    /** Creates array of TextBlockFiles from given array of TextBlockFile-s response from server    */
    ProjectFile.createFromResponseArray = function (arrayOfResponses) {
        var retArray = Array();
        for (var i = 0; i < arrayOfResponses.length; i++) {
            retArray.push(createFromResponse(arrayOfResponses[i]));
        }
        return retArray;
    }

/*
    ProjectFile.delete = function (projectFile, OKf, ErrF) {
        Ajax.sendDataPOST("/TextBox/DeleteTextBoxFile",
           textBoxFile,
            function (response) {
                console.log("   TextBlockFile.removeFile ", response);
                OKf(createFromResponse(response));
            },
            function (response) {
                console.error("   TextBlockFile.removeFile ", response);
                ErrF(response);
                //ErrorHandler.ShowError('Bad request or connection failed');
            }
        );
    }*/

    ProjectFile.save = function (projectFile, OKf, ErrF) {
        Ajax.sendDataPOST("/Project/SaveProjectFile",
           projectFile,
            function (response) {
                console.log("   ProjectFile.save ", response);
                OKf(createFromResponse(response));
            },
            function (response) {
                console.error("   ProjectFile.save ", response);
                ErrF(response);
                //ErrorHandler.ShowError('Bad request or connection failed');
            }
        );
    }

    return ProjectFile;
});