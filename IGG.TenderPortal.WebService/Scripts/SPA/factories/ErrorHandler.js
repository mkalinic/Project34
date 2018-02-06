
/** I extracted error here so I can change the way it will be shown at any time */
app.factory('ErrorHandler',function ($translate, $filter) {

    var ErrorHandler = {};

    ErrorHandler.ShowError = function (messsage) {

        alert($filter('translate')(messsage));
        console.error('!! ErrorHandler: ', messsage)

    };

    return ErrorHandler;
});