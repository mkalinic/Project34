app.factory('Checklist', function (Ajax, ErrorHandler) {

    /**
    @class: Definition of Logbook
    */
    var Checklist = function () {

    };


    Checklist.GetForProject = function (projectID, OKf, ErrF) {

        console.log(' Checklist.GetForProject = ', projectID);

        Ajax.getData("/Checklist/GetForProject/" + projectID,
           function (response) {
               console.log('/Checklist/GetForProject/ = ', response);
               var item = createFromResponseArray(response);
               OKf(response);
           },
           function (response) {
               console.error(" Checklist.GetForProject  ", response);
               ErrF(response);
           }
        );

    };

    Checklist.removeItem = function (itemID, OKf, ErrF) {

        console.log(' Checklist.GetForProject = ', itemID);

        Ajax.getData("/Checklist/Remove/" + itemID,
           function (response) {
               console.log('/Checklist/Remove/ = ', response);
               OKf(itemID);
           },
           function (response) {
               console.error(" Checklist.Remove  ", response);
               ErrF(response);
           }
        );

    };

    Checklist.save = function (item, OKf, ErrF) {
        console.log("   Milestone.saves project = ", item);
        Ajax.sendDataPOST("/Checklist/Save",
           item,
           function (response) {
               console.log("  Checklist.save ", response);
               OKf(response);
           },
           function (response) {
               console.error("  Checklist.save ERROR ", response);
               ErrF(response);
               //ErrorHandler.ShowError('Bad request or connection failed');

           }
       );
    };


    var createFromResponse = function (response) {
      
        return response;
    }


    var createFromResponseArray = function (response) {

        return response;

        //for (var i = 0; i < response.length; i++) {
        //    response[i].time = Date_ParseFromServer(response[i].time)
        //}
        //return response;
    };


    return Checklist;
});