
//app.controller('Upload_controller', ['$scope', 'Upload', '$timeout', function ($scope, Upload, $timeout) {
controllers.Upload_controller = function ($scope, $location, ErrorHandler,  $translate, Upload, $timeout, $http) {
    $scope.$watch('files', function () {
        $scope.upload($scope.files);
    });
    $scope.$watch('file', function () {
        if ($scope.file != null) {
            $scope.files = [$scope.file];
        }
    });
    $scope.log = '';

    $scope.upload = function (files) {
        if (files && files.length) {
            for (var i = 0; i < files.length; i++) {
                var file = files[i];
                if (!file.$error) {
                    Upload.upload({
                        url: '/Home/UploadFile',
                        data: {
                            username: $scope.username,
                            file: file
                        }
                    }).then(function (resp) {
                        $timeout(function () {
                            $scope.log = 'file: ' +
                            resp.config.data.file.name +
                            ', Response: ' + JSON.stringify(resp.data) +
                            '\n' + $scope.log;
                        });
                    }, null, function (evt) {
                        var progressPercentage = parseInt(100.0 *
                                evt.loaded / evt.total);
                        $scope.log = 'progress: ' + progressPercentage +
                            '% ' + evt.config.data.file.name + '\n' +
                          $scope.log;
                    });
                }
            }
        }
    };


    //TODO: Download should be in AJAX
    $scope.downloadZip = function () {
 
        console.log(' scope.DownloadZip');
        var form = document.createElement("form");
        form.setAttribute("action", "Home/DownloadZip/");
        form.setAttribute("method", "get");
        form.setAttribute("target", "_blank");

        //   //--- element to post
        //var hiddenEle1 = document.createElement("input");
        //hiddenEle1.setAttribute("type", "hidden");
        //hiddenEle1.setAttribute("name", "some");
        //hiddenEle1.setAttribute("value", value);
        
 
        form.submit(); 

    };

 
    $scope.downloadZip2 = function () {
         $http({
            url: "/Home/DownloadZip",
            method: "GET"
        })
    };


};
 //]);