app.factory('TextBlock', function (Ajax, ErrorHandler, TextBlockFile, $rootScope) {

    /**
    @class: Definition of Project
    */
    var TextBlock = function () {
        //--- these fields will be created from server response
        /*
        this.ID = -1;
        this.IDproject;
        this.text;
        */
        this.Files = null;

    };

    /** all Projects in app */
    TextBlock.all = Array();




    /** fills  Item.allItems array
   @param: pagenum {int} curent page number
   @param: countPerPage {int} numbetr of Items per page
   @param: functionOnFinished {Function} functionOnFinished that takes item as parameter
   */
    TextBlock.getById = function (id, OKf, ErrF) {

        Ajax.getData("/TextBlock/GetById/?id=" + id,
           function (response) {
               console.log('"/TextBlock/GetById?response = ', response);
               var item = createFromResponse(response);
               OKf(item);
           },
           function (response) {
               console.error(" TextBlock.getById ", response);
               ErrF(response);
           }
        );

    };



    TextBlock.save = function (textBlock, OKf, ErrF) {
        console.log("   TextBox.saves project = ", textBlock);
        Ajax.sendDataPOST("/TextBox/Save",
           textBlock,
           function (response) {
               console.log("  TextBox.save ", response);
               OKf(createFromResponse(response));
           },
           function (response) {
               console.error("  TextBox.save ERROR ", response);
               ErrF(response);
               //ErrorHandler.ShowError('Bad request or connection failed');
           }
       );
    };

    TextBlock.delete = function (textBlock, OKf, ErrF) {

        Ajax.sendDataPOST("/TextBox/Delete",
            textBlock,
           function (response) {
               console.log(" TextBox/Delete ", response);
               OKf(response);
           },
           function (response) {
               console.error(" TextBox/Delete", response);
               //ErrorHandler.ShowError('Bad request or connection failed');
               ErrF(response);
           }
       );
    };

    TextBlock.sendFileUploadedEmail = function (/*file,*/time, OKf, ErrF) {

      /*
        Ajax.sendDataPOST("/TextBox/SendFileUploadedEmail",
            file,
           function (response) {
               console.log("  TextBlock.sendFileUploadedEmail ", response);
               OKf(Item.ItemID);
           },
           function (response) {
               console.error("  TextBlock.sendFileUploadedEmail ", response);
               ErrF(Item.ItemID);
           }
       );
       */

       // Ajax.getData("/TextBox/SendFileUploadedEmail?fileName=" + file + "&time=" + time + "&language=" + $rootScope.rootScope_language,
       Ajax.getData("/TextBox/SendFileUploadedEmail?time=" + time + "&language=" + $rootScope.rootScope_language,
           function (response) {
               console.log("  TextBlock.sendFileUploadedEmail ", response);
               OKf(response);
           },
           function (response) {
               console.error("  TextBlock.sendFileUploadedEmail ", response);
               ErrF(response);
           }
        );
    };


    //---response object and this one must be the same, changing only necessary fields
    var createFromResponse = function (response) {
        var textBlock = response;
        if (response.Files) textBlock.Files = TextBlockFile.createFromResponseArray(response.Files);
        textBlock.time = Date_ParseFromServer(response.time);
        return textBlock;

    };

    /** Creates array of TextBlock from given array of TextBlock-s response from server    */
    TextBlock.createFromResponseArray = function (arrayOfResponses) {

        //    console.log("TextBlock.createFromResponseArray", arrayOfResponses);
        var retArray = Array();
        for (var i = 0; i < arrayOfResponses.length; i++) {
            retArray.push(createFromResponse(arrayOfResponses[i]));
        }
        return retArray;
    }

    return TextBlock;
});