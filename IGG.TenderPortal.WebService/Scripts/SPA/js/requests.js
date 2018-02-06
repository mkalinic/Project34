
//--------------------------------------------------------------
//          UNIVERSAL HTTP REQUESTS 
//--------------------------------------------------------------


/**
     * @description
     * universal GET funciton for ajax calls
     *
     * @param  {String} API route   
     * @return {Function} (data)
     * @public
     */
app.factory('getRequest', ['$http', function (http) {

    return function (tableRoute, result) {

        http.get(urlAdress + tableRoute).
                success(function (data, status, headers, config) {

                    result(data);
                }).
                error(function (data, status, headers, config) {
                    console.log("greska -");
                    console.log(data);
                    result(data);
                });
    }
}])

/**
     * @description
     * universal POST funciton for ajax calls
     *
     * @param  {String} API route  
     * @param  {Object} POST data object 
     * @return {Function} (data)
     * @public
     */
.factory('postRequest'['$http', function (http) {

    return function (tableRoute, postData, result) {

        http.post(urlAdress + tableRoute, postData).
            success(function (data, status, headers, config) {

                result(data);
            }).
            error(function (data, status, headers, config) {
                console.log("greska -");
                console.log(data);
                result(data);
            });
    }
}])

/**
     * @description
     * universal PUT funciton for ajax calls
     *
     * @param  {String} API route  
     * @param  {Object} PUT data object 
     * @return {Function} (data)
     * @public
     */
.factory('putRequest'['$http', function (http) {

    return function (tableRoute, putData, result) {

        http.put(urlAdress + tableRoute, putData).
            success(function (data, status, headers, config) {

                result(data);
            }).
            error(function (data, status, headers, config) {
                console.log("greska -");
                console.log(data);
                result(data);
            });
    }
}])

/**
     * @description
     * universal DELETE funciton for ajax calls
     *
     * @param  {String} API route   
     * @return {Function} (data)
     * @public
     */
.factory('deleteRequest', ['$http', function (http) {

    return function (tableRoute, result) {

        http.delete(urlAdress + tableRoute).
            success(function (data, status, headers, config) {

                result(true);
            }).
            error(function (data, status, headers, config) {
                console.log("greska -");
                console.log(data);
                result(false);
            });
    }
}])