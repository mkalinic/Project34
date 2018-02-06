/** factory for getting json data from server
    client implementation of "ajax simple protocol"
*/
app
.factory('Ajax', function ($http,/* $location,*/ ErrorHandler, localStorageService) {

    var rootDir = window.location.origin;
    var Ajax = {};

    /** sends data using POST 
        @param: target {String} - target URL
        @param: data {Json String} - json data to be posted
        @param: responseFunction {Function} - this will fire on response returned
        @param: errorFunction {Function} - this will fire on error returned (http or userdefined error)
    */
    Ajax.sendDataPOST = function (target, data, responseFunction, errorFunction) {
            $http({
                url: rootDir + target,
                method: "POST",
                data:  data 
            })
           .then(function (response, hasErrors) {
               console.log(' ::::: Ajax.sendDataPOST, NO ERROR, response = ', response);
               if (response.data.Status == "ERROR") errorFunction(response.data.Data);
               else responseFunction(response.data.Data);
               //  else responseFunction(JSON.parse(response.data.Data));
           },
            function (response) {
                console.log(' ::::: Ajax.sendDataPOST,  !!!!! ERROR, response = ', response);
              //  if (response.statusText == "Forbidden") ErrorHandler.ShowError("Access to this feature forbiden to this user");
              //  else ErrorHandler.ShowError('Bad request or connection failed');
                console.error('Ajax:: FAILED response for request : ' + target, response);
                errorFunction(response);
            });
        };


    /** sends data using GET 
        @param: target {String} - target URL
        @param: data {Json String} - json data to be posted
        @param: responseFunction {Function} - this will fire on response returned
        @param: errorFunction {Function} - this will fire on error returned (http or userdefined error)
    */
    Ajax.sendDataGET = function (target, data, responseFunction, errorFunction) {
            $http({
                url: rootDir + target,
                method: "GET",
                data:   data 
               // data: { data }
            })
             .then(function (response, hasErrors) {
                 console.log(' ::::: Ajax.sendDataGET, NO ERROR, response = ', response);
                 if (response.data.Status == "ERROR") errorFunction(response.data.Data);
                 else responseFunction(response.data.Data);
               //  else responseFunction(JSON.parse(response.data.Data));
             },
            function (response) { 
                console.error('AjaxFactory:: FAILED response for request : ' + target, response);
                errorFunction(response);
            });
        };

    /** just recieves data, does'nt send anything 
       @param: target {String} - target URL
       @param: responseFunction {Function} - this will fire on response returned
       @param: errorFunction {Function} - this will fire on error returned (http or userdefined error)
   */
    Ajax.getData = function (target, responseFunction, errorFunction) {
            $http({
                url: rootDir + target,
                method: "GET"
            })
           .then(function (response, hasErrors) {
               console.log(' ::::: Ajax.getData, NO ERROR, response = ',response);
               if (response.data.Status == "ERROR") errorFunction(response.data.Data);
               else responseFunction( response.data.Data );
           },
            function (response) {
                console.error('AjaxFactory:: FAILED response for request : ' + target, response);
                errorFunction(response);
            });
    };


    Ajax.Download = function (file, action/*, responseFunction, errorFunction*/) {
        var authData = localStorageService.get('authorizationData');

        var theAction = "/File/Download/";
        if (action) theAction = action;
        var form = document.createElement("form");
        form.setAttribute("method", "post");
        form.setAttribute("action", rootDir + theAction);
        form.setAttribute("target", "_blank");
        params=[]
 
        var input = document.createElement('input');
        input.type = 'hidden';
        input.name = "path";
        input.value = file;
        form.appendChild(input);

        input = document.createElement('input');
        input.type = 'hidden';
        input.name = "tokenEncripted";
        input.value = authData.token;
        form.appendChild(input);

        input = document.createElement('input');
        input.type = 'hidden';
        input.name = "userName";
        input.value = authData.userName;
        form.appendChild(input);
 

        document.body.appendChild(form);
        form.submit();
        document.body.removeChild(form);
 
    };

    return Ajax;

});