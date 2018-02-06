app.factory('ChecklistChecked', function (Ajax, ErrorHandler) {



    /**
    @class: Definition of Logbook
    */
    var ChecklistChecked = function () {

    };



    ChecklistChecked.save = function (item, state, OKf, ErrF) {
        console.log("   ChecklistChecked.saves item= ", item);
        console.log("   ChecklistChecked.saves state= ", state);
        if (state) {
            Ajax.sendDataPOST("/Checklist/SaveChecked",
                  item,
                  function (response) {
                      console.log("  ChecklistChecked.save ", response);
                      OKf(response);
                  },
                  function (response) {
                      console.error("  ChecklistChecked.save ERROR ", response);
                      ErrF(response);
                      //ErrorHandler.ShowError('Bad request or connection failed');

                  }
              );
        }
        else {
            Ajax.sendDataPOST("/Checklist/RemoveChecked",
                  item,
                  function (response) {
                      console.log("  ChecklistChecked.save ", response);
                      OKf(response);
                  },
                  function (response) {
                      console.error("  ChecklistChecked.save ERROR ", response);
                      ErrF(response);
                      //ErrorHandler.ShowError('Bad request or connection failed');

                  }
              );
        }
       
    };


    return ChecklistChecked;

})