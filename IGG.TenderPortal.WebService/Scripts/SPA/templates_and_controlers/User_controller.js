controllers.User_controller = function ($scope, $location, ErrorHandler, User, Upload, Logbook, Project, $timeout, $translate, $filter, $routeParams, $window, $rootScope) {
    $window.scrollTo(0, 0);

    $scope.NumberOfAllPages = 0;
    $scope.Items = Array();
    $scope.show_wait = false;
    $scope.Users;
    $scope.UserTypes = User.GetUserTypes();
    var pagenum = 0;
    var countPerPage = 15;
    $scope.user = null;

    $scope.init = function () {
        $scope.GetAllProjects();

        var userID = $routeParams.id;
       
        getUserByID(userID);
        $scope.UserTypes = User.GetUserTypes();


        $scope.userTypes = $scope.UserTypes  
    };



    /**
    Gets all Items for given page 
     @param: page_num - number of page where items are to be shown
    */
    $scope.getAll = function (page_num) {
        $scope.show_wait = true;
        User.GetAll(function (users) {
            $scope.user = users;
            $scope.show_wait = false;
            console.log(" $scope.getAll ", users);
        },
        function (response) {
           // alert('Not possible to get items from server, see console for error');
        });

    };

    /**
Gets all Items for given page 
 @param: page_num - number of page where items are to be shown
*/
    var getUserByID = function (id) {

        $scope.show_wait = true;
        User.GetByIdWithPass(id,
            function (users) {
                console.log(" $scope.getUserBy ", users);
                $scope.show_wait = false;
                $scope.user = users;
                $scope.getForUsertype();
        },
        function (response) {
            //alert('Not possible to get items from server, see console for error');
        });

    };



    /**
     Saves all Items for given page 
   */
    $scope.Save = function () {
        console.log("$scope.Save  $scope.user = ", $scope.user);
        $scope.show_wait = true;
        if (!$scope.user.email) alert($filter('translate')('you must enter email address'));
        if (!$scope.user.ID) {
            $scope.user.joined = new Date();
            console.log('LESSSS THAN 1$scope.user.joined =', $scope.user.joined);
        }
        User.save( $scope.user,
            function (response) {
                $scope.show_wait = false;
                $scope.user = response;
                console.log("SAVED response = ", response);
                alert($filter('translate')('succesfully saved'));
                $window.open("/#/User/" + response.ID, '_self');
        },
        function (response) {
           // alert('Not possible to save all items, see console for error');
        });
    };


    /**
     Adds new item to the page, does not send to server until Save pressed
    */
    $scope.addNew = function () {
        console.log("EditItems_controller addNew ");
        var c = new Item();
        Item.allItems.push(c);

    };


    /**
    Removes item from page AND SERVER
    @param: item -item to be removed
 */
    $scope.remove = function (item) {
        console.log("EditItems_controller remove item=", item);

        if (confirm('Are you sure you want to delete item "' + item.Name + '" with value ' + item.Value)) {
            $scope.show_wait = true;
            Item.delete(item, function (itemID) {
                for (var i = 0; i < Item.allItems.length; i++) {
                    if (Item.allItems[i].ItemID == itemID) {
                        console.log(' ---  ', itemID);
                        Item.allItems.splice(i, 1);
                       // alert($filter('translate')('removed'));
                        $location.path("/Admin");
                    }
                }
                $scope.show_wait = false;
            },
            function (itemID) {

                alert('Not possible to remove item ');
            });
        } else {
            // Do nothing!
        }

    };


    $scope.Delete = function () {

        User.delete($scope.user.ID,
            function (retval) {
               // $filter('translate')('user deleted');
                $location.path('/Admin');
            },
            function (retval) {

            });

    }

    /*


    $scope.changeLanguage = function (langKey) {
        if (langKey === 'en') {
            $translate.fallbackLanguage('fr');
        } else if (langKey == 'de') {
            $translate.fallbackLanguage('en');
        }
        $translate.use(langKey);
    };

    */

    $scope.GetMyAccount = function () {

        User.GetMyAccount(function (user) {
            console.log("I AM ", user);
            //$scope.Users = [];
            /// $scope.Users.push(user);

        },
        function (response) {


        });
    }

    $scope.SaveAll = function () {
        User.SaveAll($scope.Users,
            function (response) {
                console.log('ALL USERS SAVED');
                $filter('translate')('ALL USERS SAVED');
            },
            function (response) {
                console.log('ALL USERS NOT SAVED');
            });
    }

    $scope.AddNew = function () {
        $scope.Users.push(new User());
    }


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
                    console.log(resp);
                    $scope.user.photoFile = resp.data.Data;
                    $scope.user.photo = resp.data.Data.name;
                });
            }, null, function (evt) {
                var progressPercentage = parseInt(100.0 *evt.loaded / evt.total);
                $scope.log = 'progress: ' + progressPercentage +'% ' + evt.config.data.file.name + '\n' + $scope.log;
            });
        }
    };

    $scope.GetImageAddress = function (image) {
        if (image == null || image == '') return "./../../UPLOADED_IMAGES/no_photo.png";
        return "./../../UPLOADED_IMAGES/users/" + image;
    }

    //----------------- / UPLOAD ONE FILE
    //----------------- -----------------------


    $scope.SendCredentials = function () {
      //  console.log()
        User.SendCredentials($scope.user,
            function () {
               // console.log('$scope.SendCredentials SENT');
                alert($filter('translate')('Credentials sent'));
            },
            function () {
               // console.error('$scope.SendCredentials NOT SENT');
                alert($filter('translate')('Credentials could not be sent'));
            });
    }


    //-----------------------------------------------------------------------
    //-------------- logbook:

    $scope.userTypes;
    $scope.Projects;
    $scope.selectedProjectID;
    $scope.StartDate = null;
    $scope.EndDate = null;

    $scope.logs;
    var perPage = 15;
    $scope.curPage = 1;
    $scope.pagesCount = 1;
    $scope.selectedUserType = "";


    $scope.GetAllProjects = function () {

        Project.GetAll(
           function (response) {
             //  console.log('+++++++++++++++++++++++++++++++ $scope.GetAllProjects = ', response);
               $scope.Projects = response;
           },
           function (error) {
               console.error('$scope.GetAllProjects  errrrrrrr ', error);
           }
        );

    };


    $scope.getForUsertype = function () {
       // console.log($scope.user.ID);
       // console.log('$scope.selectedProject = ', $scope.selectedProjectID);

        Logbook.getCountForUser($scope.user.ID, $scope.selectedProjectID, $scope.StartDate, $scope.EndDate,
           function (response) {
              // console.log('$scope.init Logbook.getCountForUser = ', response);
               $scope.pagesCount = Math.ceil(response / perPage);
           },
           function (error) {
               console.error('$scope.init Logbook.getCountForUser  errrrrrrr ', error);
           }
        );

        Logbook.getAllForUser($scope.user.ID, $scope.selectedProjectID, $scope.StartDate, $scope.EndDate,
            function (response) {
              //  console.log('$scope.init Logbook.getAllForUser = ', response);
                $scope.logs = response;
            },
            function (error) {
                console.error('$scope.init Logbook.getAllForUser  errrrrrrr ', error);
            }
            , $scope.curPage
            , perPage
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


    $scope.gotoURL = function (url) {
        console.log(url);
        window.location.href = url;
    }

    //--------------------- / logbook
    //-----------------------------------------------------------------------


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
        if ($scope.StartDate!=null) $scope.getForUsertype();
    }

    //--------------------- / datepicker
    //--------------------------------------------------------
};