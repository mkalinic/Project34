controllers.Logbook_controller = function ($scope, $rootScope, $location, ErrorHandler, Project, $translate, $filter, Post, Notification, User, Upload, $timeout, $window, $templateCache, Logbook) {


    $scope.userTypes;

    $scope.logs;
    var perPage = 15;
    $scope.curPage = 1;
    $scope.pagesCount = 1;
    $scope.selectedUserType = "";


    $scope.init = function () {
        $scope.userTypes =  User.GetUserTypes();
       
        $scope.GetAllProjects();
        $scope.getForUsertype();

       // Logbook.getCount($scope.selectedUserType,
       //    function (response) {
       //        console.log('$scope.init Logbook.getCount = ', response);
       //        $scope.pagesCount = Math.ceil(response / perPage);
             
       //    },
       //    function (error) {
       //        console.error('$scope.init Logbook.getCount  errrrrrrr ', error);
       //    }
       //);
    };


    $scope.getForUsertype = function () {
        console.log($scope.selectedUserType);

        Logbook.getCount($scope.selectedUserType, $scope.selectedProjectID, $scope.StartDate, $scope.EndDate,
           function (response) {
               console.log('$scope.init Logbook.getCount = ', response);
               $scope.pagesCount = Math.ceil(response / perPage);
           },
           function (error) {
               console.error('$scope.init Logbook.getCount  errrrrrrr ', error);
           }
        );

        Logbook.geAll($scope.selectedUserType, $scope.selectedProjectID, $scope.StartDate, $scope.EndDate,
            function (response) {
                console.log('$scope.init Logbook.geAll = ', response);
                $scope.logs = response;
            },
            function (error) {
                console.error('$scope.init Logbook.geAll  errrrrrrr ', error);
            }
            , $scope.curPage, perPage
            );
    };



    $scope.curPage = 1;
    $scope.userCount = 0;
    $scope.pagesCount = 0

    $scope.nextPage = function () {
        if ($scope.curPage >= $scope.pagesCount) return;
        $scope.curPage++;
        $scope.getForUsertype();

    }

    $scope.prevPage = function () {
        if ($scope.curPage <= 1) return;
        if ($scope.curPage > 1) {
            $scope.curPage--;
            $scope.getForUsertype();
        }
    }

    $scope.GetAllProjects = function () {

        Project.GetAll(
           function (response) {
               console.log('+++++++++++++++++++++++++++++++ $scope.GetAllProjects = ', response);
               $scope.Projects = response;
           },
           function (error) {
               console.error('$scope.GetAllProjects  errrrrrrr ', error);
           }
        );

    };

    $scope.gotoURL = function (url) {
        console.log(url);
        window.location.href = url;
    }

    //--------------------------------------------------------
    //--------------------- datepicker:

    var DatePicker = function () {

        // this.dt is variable in which the date will be returned

        this.today = function () {
            this.dt = $rootScope.rootScope_ShowDate(new Date());
        };
        this.today();

        this.showWeeks = true;
        this.toggleWeeks = function () {
            this.showWeeks = !this.showWeeks;
        };

        this.clear = function () {
            this.dt = null;
        };

        // Disable weekend selection
        this.disabled = function (date, mode) {
            //  return (mode === 'day' && (date.getDay() === 0 || date.getDay() === 6));
        };

        this.toggleMin = function () {
            this.minDate = (this.minDate) ? null : new Date();
        };
        // this.toggleMin();

        this.open = function ($event) {
            console.log(this, "open", $event);
            $event.preventDefault();
            $event.stopPropagation();

            this.opened = true;
        };

        this.dateOptions = {
            'year-format': "'yy'",
            'starting-day': 1
        };

        this.formats = ['dd-MM-yyyy', 'yyyy/MM/dd', 'shortDate'];
        this.format = this.formats[0];

    }

    $scope.datePickerStart = new DatePicker();
    $scope.datePickerEnd = new DatePicker();

    $scope.StartDate;
    $scope.EndDate;

    $scope.StartChanged = function () {
        console.log('StartChanged ', $scope.StartDate);
        if ($scope.EndDate != null) $scope.getForUsertype();
    }

    $scope.EndChanged = function () {
        console.log('EndChanged ', $scope.EndDate);
        if ($scope.StartDate != null) $scope.getForUsertype();
    }

    //--------------------- / datepicker
    //--------------------------------------------------------




}