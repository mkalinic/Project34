app.factory('Logbook', function (Ajax, ErrorHandler) {

    /**
    @class: Definition of Logbook
    */
    var Logbook = function () {
 
    };

 
 
    Logbook.geAll = function (userType, IDproject, StartDate, EndDate, OKf, ErrF, page, pagesize) {

        Ajax.sendDataPOST("/Logbook/GetAll/" + userType,
          { IDproject, StartDate, EndDate, page, pagesize },
           function (response) {
              // console.log("   ProjectFile.save ", response);
               OKf(createFromResponseArray(response));
           },
           function (response) {
             //  console.error("   ProjectFile.save ", response);
               ErrF(response);
               //ErrorHandler.ShowError('Bad request or connection failed');
           }
       );

    };

    Logbook.getCount = function (userType, IDproject, StartDate, EndDate, OKf, ErrF) {

        Ajax.sendDataPOST("/Logbook/GetCount/" + userType,
        { IDproject, StartDate, EndDate },
         function (response) {
            // console.log("   ProjectFile.save ", response);
             OKf(createFromResponseArray(response));
         },
         function (response) {
            // console.error("   ProjectFile.save ", response);
             ErrF(response);
             //ErrorHandler.ShowError('Bad request or connection failed');
         }
        );

    };


    Logbook.getCountForUser = function (userID, IDproject, StartDate, EndDate, OKf, ErrF) {
        Ajax.sendDataPOST("/Logbook/GetCountForUser/" + userID,
          { IDproject, StartDate, EndDate },
           function (response) {
             //  console.log("   ProjectFile.save ", response);
               OKf(createFromResponseArray(response));
           },
           function (response) {
            //   console.error("   ProjectFile.save ", response);
               ErrF(response);
               //ErrorHandler.ShowError('Bad request or connection failed');
           }
       );

    };



    Logbook.getAllForUser = function (userID, IDproject, StartDate, EndDate, OKf, ErrF, page, pagesize) {
      
        Ajax.sendDataPOST("/Logbook/GetAllForUser/" + userID ,
           { IDproject, StartDate ,EndDate , page , pagesize},
            function (response) {
              //  console.log("   ProjectFile.save ", response);
                OKf(createFromResponseArray(response));
            },
            function (response) {
              //  console.error("   ProjectFile.save ", response);
                ErrF(response);
                //ErrorHandler.ShowError('Bad request or connection failed');
            }
        );

    };

    var createFromResponse = function (response) {
        response.time = Date_ParseFromServer(response.time)
        return response;
    };


    var createFromResponseArray = function (response) {
        for (var i = 0; i < response.length; i++) {
            response[i].time = Date_ParseFromServer(response[i].time)
        }
        return response;
    };

    return Logbook;
});