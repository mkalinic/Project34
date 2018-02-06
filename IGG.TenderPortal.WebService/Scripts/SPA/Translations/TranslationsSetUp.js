//------------- Loads translations from database: 
 /*
app
.factory('customTranslationLoader', function ($http, $q, Ajax) {
    return function (options) {
        console.log(options);
        var deferred = $q.defer();
        Ajax.getData("/Account/GetTranslations?lang_code=" + options.key,
           function (response) {
             //  console.log('customTranslationLoader response =', response);
               var translation = "";
               for (var i = 0; i < response.length; i++) {
                   translation += '"'+response[i].text + '":"' + response[i].translation+'"'
                   if (i < response.length - 1) translation += ",";
               }
             //  console.log("FINAL translation = ", translation);
               data = JSON.parse("{" + translation + "}");      
             //  console.log("data = ", data);
               return deferred.resolve(data);
     
           },
           function (response) {
               // reject with language key
               return deferred.reject(options.key);
           }
        );

        return deferred.promise;
    };
});

app
.config(['$translateProvider', function ($translateProvider) {
    $translateProvider.useLoader('customTranslationLoader');
    $translateProvider.preferredLanguage('en');
}]);
 
 */

//------------- Loads translations from static files named en.json, de.json ... : 
app
.config(['$translateProvider', function ($translateProvider) {
 
 
    $translateProvider.useStaticFilesLoader({
        prefix: 'Scripts/SPA/Translations/',
        suffix: '.json'
    });
    $translateProvider.useSanitizeValueStrategy(null);
    $translateProvider.preferredLanguage('nl');
   //  $translateProvider.preferredLanguage('en');
 
}]);
