controllers.Admin_controller = function ($scope, $rootScope, $location, ErrorHandler, Project, $translate, $filter, Post, Notification, User, Upload, $timeout, $window, $templateCache) {

    $scope.projects = [];
    $scope.projectsInProgress = [];
    $scope.projectsClosed = [];
    $scope.users = [];

    $scope.user = null;

    $scope.curPage = 1;
    $scope.userCount = 0;
    $scope.pagesCount = 0


   // document.getElementById("banner-main").style.backgroundImage = "url('../../../UPLOADED_IMAGES/frontImage/IGGtenderportal.jpg?t="+(new Date())+"'";
   // $('#banner-main').css("background-image", "url('../../../UPLOADED_IMAGES/frontImage/IGGtenderportal.jpg')");

    $scope.init = function () {
        User.GetMyAccount(
                function (user) {
                    console.log('GetMyAccount = ', user);
                    thisUser = user;
                    $rootScope.rootScope_curentUser = user;
                    if (thisUser.userType != "IGG") {
                        $location.path("/MyAccount");
                    }
                },
                function (error) {
                    // $location.path("/ViewProject/" + projectID);
                });


        $scope.GetUsers($scope.curPage, 15);
        $scope.GetAllProjects();

        User.GetCount(function (response)
        {

            $scope.userCount = response;
            $scope.pagesCount = Math.ceil($scope.userCount / 15);

            console.log('$scope.userCount = ', $scope.userCount);
            console.log(' $scope.pagesCount = ', $scope.pagesCount);

        }, function () {


        })
    };




    $scope.sortUsers = function (byWhat) {


    }


    $scope.GetAllProjects = function () {
        Project.GetAll(
            function (projects) {
              //  console.log('GetTopNForUser, projects = ', projects);
                for (var i = 0; i < projects.length; i++) {
                    if (projects[i].status == "CLOSED") $scope.projectsClosed.push(projects[i]);
                    else $scope.projectsInProgress.push(projects[i]);
                }
                console.log('CLOSED: ', $scope.projectsClosed);
                console.log('IN PROGRESS: ', $scope.projectsInProgress);

            }, function (error) {
                console.error('GetTopNForUser, ERROR = ', error);
            });
    };


    $scope.GetUsers = function (page, pagesize) {
        console.log(' $scope.GetUsers( ' + page + ',' + pagesize + ') ');
        User.Get(page, pagesize,
            function (response) {
              //  console.log(' $scope.GetUsers( ' + page + ',' + pagesize + ') response = ', response);
                $scope.users = response;
                sorted = false;
            },
            function (response) {
                console.error(' $scope.GetUsers FAILED');
            })

    };

    var asc = true;
    var sorted = false;
    var sortedBy = null;
    $scope.GetSortedUsers = function (bywhat) {
        sortedBy = bywhat;
        sorted = true;
        console.log('  $scope.sortUsersby');
        User.GetSorted($scope.curPage, 15, bywhat, asc,
            function (response) {
                $scope.users = response;
                  asc = !asc;
            },
            function (response) {
                console.error(' $scope.GetUsers FAILED');
            })

    }

    $scope.nextPage = function () {
        if ($scope.curPage >= $scope.pagesCount) return;
        $scope.curPage++;
        if (sorted) { asc = !asc; $scope.GetSortedUsers(sortedBy); }
        else $scope.GetUsers($scope.curPage, 15);
    }

    $scope.prevPage = function () {
        if ($scope.curPage <= 1) return;
        if ($scope.curPage > 1) {
            $scope.curPage--;
            if (sorted) {asc = !asc; $scope.GetSortedUsers(sortedBy);  }
            else $scope.GetUsers($scope.curPage, 15);
        }
    }

/*
    $scope.selectProject = function (project) {
       // console.log('  $scope.selectProject ', project);
        $scope.selectedUserProjects = project.UsersProjects;
    };
    */
    $scope.ShowDate = function (date) {
        return $rootScope.rootScope_ShowDate(date);
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

    $scope.datePickerUserFrom = new DatePicker();
    $scope.datePickerUserTo = new DatePicker();


    $scope.datePickerEditedUserProjectFrom = new DatePicker();
    $scope.datePickerEditedUserProjectTo = new DatePicker();


    //--------------------- / datepicker
    //--------------------------------------------------------


    //-------------------------  user edit, save:

    $scope.UserInFocus = null;
    $scope.showDialog = false;
    $scope.setUserForEdit = function (us) {
        $scope.UserInFocus = us;
        console.log('$scope.setUserForEdit ', us);
      //  angular.element('#Gebruiker').trigger('click');
       // angular.element('#Gebruiker').fadeIn('fast');
      //  var element = angular.element(document.querySelector('#Gebruiker'))[0];
     //   element.modal('show')
      //  $scope.showDialog = true;
      //  console.log('element ===', element);
       // console.log(element.style);
        $window.open('./#/User/'+us.ID, '_self');
    }


    $scope.addNewUser= function (us) {
        $window.open('./#/User/0', '_self');
    }


    $scope.Save = function () {
        console.log("EditItems_controller saveAll page_num=");
        $scope.show_wait = true;
        User.save($scope.user,
            function (response) {
                $scope.show_wait = false;
               // alert($filter('translate')('succesfully saved'));
            },
        function (response) {
            alert('Not possible to save all items, see console for error');
        });
    };

    //----------------- -----------------------
    //----------------- UPLOAD ONE FILE
    $scope.log = '';

    $scope.upload = function (file, user) {
        console.log('upload ', file);
        if (!file) { return; }
        if (!file.$error) {
            Upload.upload({
                url: '/User/UploadFile',
                data: {
                    username: $scope.user.username,
                    file: file
                }
            }).then(function (resp) {
                $timeout(function () {
                    // console.log(resp);
                    $scope.user.photoFile = resp.data.Data;
                    $scope.user.photo = resp.data.Data;
                });
            }, null, function (evt) {
                var progressPercentage = parseInt(100.0 *
                        evt.loaded / evt.total);
                $scope.log = 'progress: ' + progressPercentage +
                    '% ' + evt.config.data.file.name + '\n' +
                  $scope.log;
            });
        }
    };

    $scope.GetImageAddress = function (image) {
        if (image == null || image == '') return "./../../UPLOADED_IMAGES/no_photo.png";
        return "./../../UPLOADED_FILES/" + image;
    }

    //----------------- / UPLOAD ONE FILE
    //----------------- -----------------------


    $scope.GetProjectImageAddress = function (image) {
        return Project.GetImageAddress(image);
    }

    $scope.gotoProject = function (id) {
        $location.path("/Project/" + id);
    }

    $scope.addNewProject = function () {
        $location.path("/Project/0" );
    }

    $scope.searchuserKeyword = "";
    $scope.searchUser = function () {
        if ($scope.searchuserKeyword == "" || !$scope.searchuserKeyword) {
            $scope.GetUsers($scope.curPage, 15);
            return;
        }

        User.Search($scope.searchuserKeyword,
            function (foundUsers) {
                $scope.users = foundUsers;
            }, function (response) {
                console.error('  User.Search failed');
            });


    }


    $scope.uploadFrontImage = function (files) {
        //  $scope.uploadProgressShow = true;

        var URL = '/File/UploadFrontImage';

        if (files && files.length) {
            $scope.uploadProgressShow = true;
            for (var i = 0; i < files.length; i++) {
                var file = files[i];
                if (!file.$error) {

                    Upload.upload({
                        url: URL,
                        data: {
                            username: $scope.username,
                            file: file
                        }
                    }).then(function (resp) {
                        $timeout(function () {
                            /* $scope.log = 'file: ' + resp.config.data.file.name +  ', Response: ' + JSON.stringify(resp.data) + '\n' + $scope.log; */
                            console.log(' $$$$$$$$$$$$$$$$$$$$ $scope.uploadImage ', resp);
                           /* $scope.Project.photo = resp.data.Data.name;
                            $scope.Image = $scope.Project.photo;
                            $scope.TopImage = $scope.Image;
                            console.log('++++ $scope.Project.photo = ', $scope.Project.photo);*/
                            $scope.uploadProgressShow = false;
                            $templateCache.removeAll();
                            $window.location.reload();

                        });
                    }, null, function (evt) {
                        var progressPercentage = parseInt(100.0 * evt.loaded / evt.total);
                        $scope.uploadProgressPercentage = progressPercentage;
                        $scope.log = 'progress: ' + progressPercentage + '% ' + evt.config.data.file.name + '\n' + $scope.log;
                        //  console.log('$scope.uploadProgressPercentage = progressPercentage');
                    });
                }
            }
        }
    };

}