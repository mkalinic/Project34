controllers.NewProject_controller = function ($scope, $location, $translate, $filter, ErrorHandler, Project, User, $routeParams) {

    $scope.IGGUsersProjects = [];
    $scope.UsersProjects = [];


    $scope.init = function () {
        console.log('NewProject_controller INITED');

        if ($routeParams.id) {
            var projectID = $routeParams.id;

            console.log('NewProject_controller  $scope.projectID ', projectID);

            Project.getById(projectID,
                function (project) {
                    console.log(' NewProject_controller Project project ', project);
                    $scope.Project = project;
                    arangeUsers();
                    $scope.TopImage = $scope.GetProjectImageAddress($scope.Project.photo);

                },
                function (error) {
                    console.error(' NewProject_controller Project.getById', error);
                })

        }
        else {
            console.log(' NewProject_controller PRAVI SE NOVI PROJEKT ');
        }

       
       


    };

    var arangeUsers = function () {
        for (var i = 0; i < $scope.Project.UsersProjects.length; i++) {
            if ($scope.Project.UsersProjects[i].User.userType == "IGG") $scope.IGGUsersProjects.push($scope.Project.UsersProjects[i]);
            else $scope.UsersProjects.push($scope.Project.UsersProjects[i]);

        }
    }


    /** Returns one of users attached to this project*/
    $scope.getUserForThisProjectById = function (id) {

        for (var i = 0; i < $scope.Project.UsersProjects.length; i++) {
            if ($scope.Project.UsersProjects[i].User.ID == id) return $scope.Project.UsersProjects[i].User;
        }
        return null;

    }


    $scope.addUser = function () {

        alert('now a modal gets opened to inset a new user');

    }

    


    $scope.GetProjectImageAddress = function (image) {
     //   alert(image);
        return Project.GetImageAddress(image);
    }

    $scope.GetuserImageAddress = function (image) {
        return User.GetImageAddress(image);
    }




    //--------------------------------------------------------
    //--------------------- datepicker:
   
    var DatePicker = function () {

       // this.dt is variable in which the date will be returned

        this.today = function () {
            this.dt = new Date();
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
            return (mode === 'day' && (date.getDay() === 0 || date.getDay() === 6));
        };

        this.toggleMin = function () {
            this.minDate = (this.minDate) ? null : new Date();
        };
        this.toggleMin();

        this.open = function ($event) {
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


    $scope.datePickerGeneral = new DatePicker();
    $scope.datePickerSchedule = new DatePicker();
    $scope.datePickerDownloads = new DatePicker();
    $scope.datePickerDownloads2 = new DatePicker();


    /*
            $scope.today = function () {
            $scope.dt = new Date();
        };
        $scope.today();

        $scope.showWeeks = true;
        $scope.toggleWeeks = function () {
            $scope.showWeeks = !$scope.showWeeks;
        };

        $scope.clear = function () {
            $scope.dt = null;
        };

        // Disable weekend selection
        $scope.disabled = function (date, mode) {
            return (mode === 'day' && (date.getDay() === 0 || date.getDay() === 6));
        };

        $scope.toggleMin = function () {
            $scope.minDate = ($scope.minDate) ? null : new Date();
        };
        $scope.toggleMin();

        $scope.open = function ($event) {
            $event.preventDefault();
            $event.stopPropagation();

            $scope.opened = true;
        };

        $scope.dateOptions = {
            'year-format': "'yy'",
            'starting-day': 1
        };

        $scope.formats = ['dd-MM-yyyy', 'yyyy/MM/dd', 'shortDate'];
        $scope.format = $scope.formats[0];
    */
   
 
    //--------------------- / datepicker
    //--------------------------------------------------------

}