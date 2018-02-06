app.factory('Milestone', function (Ajax, ErrorHandler) {

    /**
    @class: Definition of Project
    */
    var Milestone = function () {
        //--- these fields will be created from server response
        /*   this.ID = -1;
           this.IDproject; 
           this.name;
           this.Phases = null;*/
    };


    /** fills  Item.allItems array
   @param: pagenum {int} curent page number
   @param: countPerPage {int} numbetr of Items per page
   @param: functionOnFinished {Function} functionOnFinished that takes item as parameter
   */
    Milestone.getById = function (id, OKf, ErrF) {

        Ajax.getData("/Milestone/GetById/?id=" + id,
           function (response) {
               console.log('"/Milestone/GetById?response = ', response);
               var item = createFromResponse(response);
               OKf(item);
           },
           function (response) {
               console.error(" Milestone.getById ", response);
               ErrF(response);
           }
        );

    };


    Milestone.save = function (milestone, OKf, ErrF) {
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

    Milestone.delete = function (milestone, OKf, ErrF) {

        Ajax.sendDataPOST("/Milestone/Delete",
            milestone,
           function (response) {
               console.log(" Milestone.saveAllItems ", response);
               OKf(createFromResponse(response));
           },
           function (response) {
               console.error(" Milestone.saveAllItems ", response);
               ErrorHandler.ShowError('Bad request or connection failed');
               ErrF(response);
           }
       );
    };


    //---response object and this one must be the same, changing only necessary fields
    var createFromResponse = function (response) {
        // I migth want to add some conversion here later
        var item = response;
        item.time = Date_ParseFromServer(response.time);
        item.notificationDate = Date_ParseFromServer(response.notificationDate);
        return item;
    };

    /** Creates array of Milestone from given array of Milestone-s response from server    */
    Milestone.createFromResponseArray = function (arrayOfResponses) {
        if (!arrayOfResponses) return null;
        //  console.log("Milestone.createFromResponseArray", arrayOfResponses);
        var retArray = Array();
        for (var i = 0; i < arrayOfResponses.length; i++) {
            retArray.push(createFromResponse(arrayOfResponses[i]));
        }
        return retArray;
    }


    return Milestone;
});