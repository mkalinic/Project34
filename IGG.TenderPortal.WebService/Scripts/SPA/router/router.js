
// fix for IE
if (!window.location.origin) {
    window.location.origin = window.location.protocol + "//" + window.location.hostname + (window.location.port ? ':' + window.location.port : '');
}

var templates_root = window.location.origin + '/Scripts/spa/templates_and_controlers/';

app.config(function ($routeProvider, $locationProvider) {
   
    $routeProvider
     .when('/Login', {
         controller: 'Login_controller',
         templateUrl: templates_root + 'Login_template.html'
     })
    .when('/Index', {
        controller: 'Index_controller',
        templateUrl: templates_root + 'Index_template.html'
    })
    .when('/MyAccount', {
        controller: 'MyAccount_controller',
        templateUrl: templates_root + 'MyAccount_template.html'
    })
        
    .when('/ShowUsers', {
        controller: 'EditShowUser_controller',
        templateUrl: templates_root + 'ShowUser_template.html'
    })

     .when('/UpdatePassword/:token', {
         controller: 'UpdatePassword_controller',
         templateUrl: templates_root + 'UpdatePassword_template.html'
     })

    .when('/Project/:id', {
        controller: 'Project_controller',
        templateUrl: templates_root + 'Project_template.html'
    })

    .when('/ViewProject/:id', {
        controller: 'ViewProject_controller',
        templateUrl: templates_root + 'ViewProject_template.html'
    })

    .when('/User/:id', {
        controller: 'User_controller',
        templateUrl: templates_root + 'User_template.html'
    })

    .when('/Admin', {
        controller: 'Admin_controller',
        templateUrl: templates_root + 'Admin_template.html'
    })

    .when('/SearchResult', {
        controller: 'SearchResult_controller',
        templateUrl: templates_root + 'SearchResult_template.html'
    })

    .when('/Logbook', {
        controller: 'Logbook_controller',
        templateUrl: templates_root + 'Logbook_template.html'
    });

    $routeProvider.otherwise({ redirectTo: '/Index' });

    //---- no hash - add after all is done
    // if (!!(window.history && history.pushState)) $locationProvider.html5Mode(true);  // after all is done
}); 