app.factory('TextBlockFile', function (Ajax, ErrorHandler) {

    /**
    @class: Definition of Project
    */
    var TextBlockFile = function () {
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
    TextBlockFile.createFromResponseArray = function (arrayOfResponses) {
        //   console.log('TextBlockFile.createFromResponseArray ', arrayOfResponses);
        var retArray = Array();
        for (var i = 0; i < arrayOfResponses.length; i++) {
            retArray.push(createFromResponse(arrayOfResponses[i]));
        }
        return retArray;
    }


    TextBlockFile.delete = function (textBoxFile, OKf, ErrF) {
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

     /*   Ajax.getData("/File/Delete?path=UPLOADED_FILES/projects/" + filePath.file,
              function (response) {
                  console.log('/Project/GetAll/ response = ', response);
                  OKf(response);
              },
              function (response) {
                  ErrF(response);
              }
           );*/
    }

    return TextBlockFile;
});