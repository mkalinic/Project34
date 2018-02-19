'use strict';

app.config(function (tmhDynamicLocaleProvider) {
    tmhDynamicLocaleProvider.localeLocationPattern('http://code.angularjs.org/1.0.8/i18n/angular-locale_{{locale}}.js');
});

//-----------------------   
app.run(['$rootScope', '$route', '$translate', '$filter', '$location', 'authService', 'tmhDynamicLocale', 'Ajax', 'localStorageService', function ($rootScope, $route, $translate, $filter, $location, authService, tmhDynamicLocale, Ajax, localStorageService) {
 
     $rootScope.title = "";
     $rootScope.text = "";

     $rootScope.rootScope_language = "nl";
     var savedLang = localStorageService.get('language');
     console.log('savedLang = ', savedLang);

     if (savedLang) {
         $translate.use(savedLang.language);
         if (savedLang.language == "nl") tmhDynamicLocale.set("nl-nl");
         $rootScope.rootScope_language = savedLang;
     }
     else {
         $translate.use("nl");
         tmhDynamicLocale.set("nl-nl");
     }

   

     $rootScope.rootScope_changeLanguage = function (lang) {

         localStorageService.set('language', { language: lang }); //<<<----------------------


        $translate.use(lang);
        if (lang == "nl") tmhDynamicLocale.set("nl-nl");
        else if (lang == "en") tmhDynamicLocale.set("en");
        else console.error('YOU ARE TRYING TO SEN LANGUAGE TO "' + lang + '" WHICH IS NOT SUPPORTED BY THIS APP!');
 
        $rootScope.rootScope_language = lang;

        Ajax.getData("/Home/GetFrontPageTitle?lang=" + lang,
              function (response) {
                  // console.log('/Home/GetFrontPageTitle response = ', response);
                  $rootScope.title = response.title;
                  $rootScope.text = response.text;
              },
              function (error) {
                  // console.log('/Home/GetFrontPageTitle ERROR = ', response);
              }
           );

    };

    $rootScope.rootScope_sexes = [
        { value: false, name: $filter('translate')('Female') },
        { value: true, name: $filter('translate')('Male') }
    ];

    $rootScope.rootScope_countries = [
            "Germany",
            "France",
            "Nederland",
            "United Kingdom"
    ];

    $rootScope.rootScope_titles = [
        "Mr",
        "Mrs",
        "Miss"
    ];

    $rootScope.rootScope_userTypes = [            
            "CANDIDATE",
            "CLIENT",
            "CONSULTANT",
            "TENDER-TEAM",
            "IGG"
    ];

    $rootScope.rootScope_defaultPhases = [
          { ID: 1, name: "selection stage" },
          { ID: 2, name: "award stage" },
          { ID: 3, name: "market consultation" },
          { ID: 4, name: "dialogue" }
    ];
 
    //--- currently loged in user
    $rootScope.rootScope_username = null;

    $rootScope.rootScope_curentUser = null;

    $rootScope.rootScope_logout = function () {
        authService.logOut();
        $rootScope.rootScope_username = null;
        $location.path('/');
        //window.location.reload();
    };

    $rootScope.rootScope_GoToFrontPage = function () {
        $location.path("/");
    };

    $rootScope.rootScope_ShowDate = function (d) {
        if (!d) return;
        return d.getDate() + "-" + (d.getMonth() + 1) + "-" + d.getFullYear();
    }

    $rootScope.rootScope_ShowDateTime = function (d) {
        if (!d) return;
       // return d.getDate() + "-" + (d.getMonth() + 1) + "-" + d.getFullYear() + " " + d.getHours() + ":" + d.getMinutes();
        return Date_WriteDateTime(d);
    }

    $rootScope.rootScope_FoundProjects = [];

}]);



