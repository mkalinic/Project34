app.factory('Notification', function (Ajax, ErrorHandler) {

    /**
    @class: Definition of Project
    */
    var Notification = function () {
        //--- these fields will be created from server response
 
    };


    /** fills  Item.allItems array
   @param: pagenum {int} curent page number
   @param: countPerPage {int} numbetr of Items per page
   @param: functionOnFinished {Function} functionOnFinished that takes item as parameter
   */
    Notification.GetLatestNotificationForProject = function (IDproject, howmany, OKf, ErrF) {

        Ajax.getData("/Notification/GetLatestNotificationForProject/?IDproject=" + IDproject+"&howmany="+howmany,
           function (response) {
               var item = Notification.createFromResponseArray(response);
               OKf(item);
           },
           function (response) {
               console.error("/Notification/GetLatestNotificationForProject/?IDproject=" + IDproject + "&howmany=" + howmany, response);
               ErrF(response);
           }
        );

    };



    Notification.save = function (milestone, OKf, ErrF) {
        console.log("   Milestone.saves project = ", milestone);
        Ajax.sendDataPOST("/Milestone/Save",
           milestone,
           function (response) {
               console.log("  Milestone.save ", response);
               OKf(response);
           },
           function (response) {
               console.error("  Milestone.save ERROR ", response);
               ErrF(response);
               //ErrorHandler.ShowError('Bad request or connection failed');
           }
       );
    };

    Notification.delete = function (milestone, OKf, ErrF) {

        Ajax.sendDataPOST("/Milestone/Delete",
            milestone,
           function (response) {
               console.log(" Milestone.saveAllItems ", response);
               OKf(Item.ItemID);
           },
           function (response) {
               console.error(" Milestone.saveAllItems ", response);
              // ErrorHandler.ShowError('Bad request or connection failed');
               ErrF(Item.ItemID);
           }
       );
    };


    //---response object and this one must be the same, changing only necessary fields
    var createFromResponse = function (response) {
        // I migth want to add some conversion here later
       // console.log("Notification createFromResponse, response = ", response)
        var notification = response;
        notification.time = Date_ParseFromServer(response.time);
        return notification;
    };

    /** Creates array of Milestone from given array of Milestone-s response from server    */
    Notification.createFromResponseArray = function (arrayOfResponses) {
        if (!arrayOfResponses) return null;
      //  console.log("Notification.createFromResponseArray", arrayOfResponses);
        var retArray = Array();
        for (var i = 0; i < arrayOfResponses.length; i++) {
            retArray.push(createFromResponse(arrayOfResponses[i]));
        }
        return retArray;
    }


    return Notification;
});