app.factory('Phase', function (Ajax, ErrorHandler) {

    /**
    @class: Definition of Project
    */
    var Phase = function () {
        //--- these fields will be created from server response
        /*  this.ID = -1;
          this.IDmilestone;
          this.date;*/
    };


    Phase.all = Array();




    /** fills  Item.allItems array
   @param: pagenum {int} curent page number
   @param: countPerPage {int} numbetr of Items per page
   @param: functionOnFinished {Function} functionOnFinished that takes item as parameter
   */
    Phase.getById = function (id, OKf, ErrF) {

        Ajax.getData("/Phase/GetById/?id=" + id,
           function (response) {
               console.log('"/Phase/GetById?response = ', response);
               var item = createFromResponse(response);
               OKf(item);
           },
           function (response) {
               console.error(" Phase.getById ", response);
               ErrF(response);
           }
        );

    };



    Phase.save = function (phase, OKf, ErrF) {
        console.log("   Phase.saves project = ", phase);
        Ajax.sendDataPOST("/Phase/Save",
           phase,
           function (response) {
               console.log("  Phase.save ", response);
               OKf(response);
           },
           function (response) {
               console.error("  Phase.save ERROR ", response);
               ErrF(response);
             //  ErrorHandler.ShowError('Bad request or connection failed');
           }
       );
    };

    Phase.delete = function (phase, OKf, ErrF) {

        Ajax.sendDataPOST("/Phase/Delete",
            phase,
           function (response) {
               console.log(" Phase.saveAllItems ", response);
               OKf(Item.ItemID);
           },
           function (response) {
               console.error(" Phase.saveAllItems ", response);
             //  ErrorHandler.ShowError('Bad request or connection failed');
               ErrF(Item.ItemID);
           }
       );
    };


    //---response object and this one must be the same, changing only necessary fields
    var createFromResponse = function (response) {
        var phase = response;
        phase.date = Date_ParseFromServer(response.date);
        return phase;

    };


    /** Creates array of Phase from given array of Phase-s response from server    */
    Phase.createFromResponseArray = function (arrayOfResponses) {
        if (!arrayOfResponses) return null;
        //  console.log("Phase.createFromResponseArray", arrayOfResponses);
        var retArray = Array();
        for (var i = 0; i < arrayOfResponses.length; i++) {
            retArray.push(createFromResponse(arrayOfResponses[i]));
        }
        return retArray;
    }


    return Phase;
});