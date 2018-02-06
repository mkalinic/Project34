controllers.UpdatePassword_controller = function ($scope, $location, $translate, $filter, ErrorHandler, Project, User, $routeParams, Ajax) {

    $scope.Message = '';
    $scope.init = function () {

        var token = $routeParams.token;


        Ajax.getData("/User/UpdatePassword?token=" + token,
           function (response) {
               console.log('/User/UpdatePassword/ ', response);
    
               $scope.user = response;
               console.log('USER = ', $scope.user);
           },
           function (response) {
               console.error(" Item.getById ", response);
               ErrF(response);
           }
        );

        $scope.save = function () {
            if ($scope.pass1 != $scope.pass2) {
                $scope.Message =  $filter('translate')('Paswords do not match');
                return;
            }

            $scope.user.pass = $scope.pass1;

            Ajax.sendDataPOST("/User/SaveNewPassword",
                 {user: $scope.user, token: token},
                function (response) {
                    console.log(" Item.SaveJson ", response);
                    $scope.Message =  $filter('translate')('New pasword saved');
                },
                function (response) {
                    console.error(" Item.SaveJson ", response);
                    $scope.Message =  $filter('translate')('Password could not be saved, please try later');
                }
             );
        }




    }

}