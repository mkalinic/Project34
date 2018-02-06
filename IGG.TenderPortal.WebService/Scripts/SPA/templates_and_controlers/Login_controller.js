app.controller('Login_controller', ['$scope', '$rootScope', '$location', '$filter', '$translate', 'authService', 'User', function ($scope, $rootScope, $location,  $filter, $translate, authService, User) {

    $scope.loginData = {
        userName: "",
        password: ""
    };

    $scope.message = "";

    $scope.login = function () {
        authService.login($scope.loginData).then(function (response) {
           
            console.log('RELOADING ', response);
            if (response == 'invalid attempt') {
                authService.logOut();
                alert($filter('translate')('you have supplied wrong credentials'));
            }
            else {
                $rootScope.rootScope_showlogin = false;
                window.location.reload();
            }
           //  
        },
         function (err) {
             $scope.message = err.error_description;
             authService.logOut();
             alert($filter('translate')('you have supplied wrong credentials'));
         });
    };


    $scope.showForgottenPasswordDialog = function () {
        $rootScope.rootScope_showlogin_ALL = true;
        $rootScope.rootScope_showForgottenPassword = true;
        $rootScope.rootScope_showlogin = false;

    }

    $scope.hideForgottenPasswordDialog = function () {
        $rootScope.rootScope_showForgottenPassword = false;
        $rootScope.rootScope_showlogin = false;
        $rootScope.rootScope_showlogin_ALL = false;
      //  window.location.reload();

    }

    $scope.sendLinkForForgottenPassword = function (usernameOrEmail) {
        User.SendLinkForForgottenPassword(usernameOrEmail, function (response) {
            console.log(' $scope.sendLinkForForgottenPassword aresponse = ', response);
            // alert('link has been sent');
            if (response == "OK") $rootScope.rootScope_showForgottenPasswordMessage = $filter('translate')('Please check your email to update password');
            else $rootScope.rootScope_showForgottenPasswordMessage = $filter('translate')('Unknown error, please contact us for this');
        },
         function (err) {
             $rootScope.rootScope_showForgottenPasswordMessage = $filter('translate')('Unknown error, please try again later or contact us for this');
         });
    };

    $scope.init = function () {
     ///   $scope.showlogin = true;
    }

}]);