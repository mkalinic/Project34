controllers.EditShowUser_controller = function ($scope, $location, ErrorHandler, User, Upload, $timeout, $translate) {

    $scope.NumberOfAllPages = 0;
    $scope.Items = Array();
    $scope.show_wait = false;
    $scope.Users;
    $scope.UserTypes = User.GetUserTypes();
    var pagenum = 0;
    var countPerPage = 15;

    /**
    Gets all Items for given page 
     @param: page_num - number of page where items are to be shown
    */
    $scope.getAll = function (page_num) {

        $scope.show_wait = true;
        User.GetAll(function (users) {
            $scope.Users = users;
            $scope.show_wait = false;
            console.log(" $scope.getAll ", users);
        },
        function (response) {
            alert('Not possible to get items from server, see console for error');
        });

    };

    /**
     Saves all Items for given page 
   */
    $scope.saveAll = function () {
        console.log("EditItems_controller saveAll page_num=");
        $scope.show_wait = true;
        Item.saveAll(function (response) {
            $scope.show_wait = false;
        },
        function (response) {
            alert('Not possible to save all items, see console for error');
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
                    }
                }
                $scope.show_wait = false;
            },
            function (itemID) {
                alert('Not possible to remove item with ');
            });
        } else {
            // Do nothing!
        }

    };

  
    $scope.init = function () {
        $scope.getAll();
        $scope.GetMyAccount();
        $scope.UserTypes = User.GetUserTypes();
    };



    $scope.changeLanguage = function (langKey) {
        if (langKey === 'en') {
            $translate.fallbackLanguage('fr');
        } else if (langKey == 'de') {
            $translate.fallbackLanguage('en');
        }
        $translate.use(langKey);
    };



    $scope.GetMyAccount = function () {

        User.GetMyAccount(function (user) {
            console.log("I AM ", user);
            //$scope.Users = [];
           /// $scope.Users.push(user);

        },
        function (response) {


        });
    }

    $scope.SaveAll = function(){
        User.SaveAll($scope.Users,
            function (response) {
                console.log('ALL USERS SAVED');
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
                            user.photoFile = file;
                            user.photo = file.name;
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




};