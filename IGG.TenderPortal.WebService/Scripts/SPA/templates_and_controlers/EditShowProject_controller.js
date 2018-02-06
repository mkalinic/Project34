controllers.EditShowProject_controller = function ($scope, $location, ErrorHandler, Project, $translate, $filter, Post, Notification) {
 
    $scope.NumberOfAllPages = 0;
    $scope.show_wait = false;

    var pagenum = 0;
    var countPerPage = 15;

    


    /**
Gets all Items for given page 
@param: page_num - number of page where items are to be shown
*/
    $scope.getTopN = function (n) {

        $scope.show_wait = true;
        Project.GetTopN(n,
            function (projects) {
                if (projects == null) alert('No projects to display');
                //  $scope.NumberOfAllPages = NumberOfAllPages;
                $scope.Projects = projects;
                $scope.show_wait = false;
            },
        function (response) {
            alert('Not possible to get data from server');
        });

    };




    /**
Gets all Items for given page 
 @param: page_num - number of page where items are to be shown
*/
    $scope.GetTopNForFrontPage = function (n) {

        $scope.show_wait = true;
        Project.GetTopNForFrontPage(n,
            function (projects) {
          //  $scope.NumberOfAllPages = NumberOfAllPages;
            $scope.Projects = projects;
            $scope.show_wait = false;
        },
        function (response) {
            alert('Not possible to get items from server, see console for error');
        });

    };


    /** Saves given project to database 
     @param: project - project to be saved */
    $scope.save = function (project) {
        console.log('scope.save, proj = ', project);
        Project.save(project,
            function (response) {
                console.log('OK, project saved');

            },
             function (response) {
                 console.log('NOT OK, project NOT saved');

             });


    };

    /** Saves given project to database 
    @param: project - project to be saved */
    $scope.close = function (project) {
        if (confirm($filter('translate')('This project will be closed. Are you sure you want to do this?'))) {
            console.log('scope.close, proj = ', project);
            Project.close(project,
                function (objec) {
                    console.log('OK, project closed');
                    for (var i = 0; i < $scope.Projects.length; i++) if ($scope.Projects[i].ID == objec.ID) $scope.Projects.splice(i, 1);
                },
                 function (response) {
                     console.log('NOT OK, project NOT closed');

                 });
        }
    };

    $scope.delete = function (project) {
        if (confirm($filter('translate')('This project will be deleted without trace. Are you sure you want to do this?'))) {
            console.log('scope.delete, proj = ', project);
            Project.delete(project,
                function (objec) {
                    console.log('OK, project deleted');
                    for (var i = 0; i < $scope.Projects.length; i++) if ($scope.Projects[i].ID == objec.ID) $scope.Projects.splice(i, 1);
                },
                 function (response) {
                     console.log('NOT OK, project NOT deleted');

                 });
        }
    };




    /**
    Gets all Items for given page 
     @param: page_num - number of page where items are to be shown
    */
    $scope.getAll = function (page_num) {
      
        $scope.show_wait = true;
        Project.getAll(page_num, countPerPage, function (NumberOfAllPages) {
            $scope.NumberOfAllPages = NumberOfAllPages;
            $scope.Items = Item.allItems;
            $scope.show_wait = false;
        },
        function (response) {
            alert('Not possible to get items from server, see console for error');
        });

    };

 
    $scope.saveAll = function () 
    {
        console.log("EditItems_controller saveAll page_num=");
        $scope.show_wait = true;
        Item.saveAll(function(response){
        $scope.show_wait = false;
        },
        function (response) {
            alert('Not possible to save all items, see console for error');
        });
    };
 

 
    $scope.addNew = function () {
        console.log("EditItems_controller addNew ");
         Project.createNew(
          function (project) {
              console.log('adding project', project)
              $scope.Projects.push(project);
          },
          function (response) {

          });
       
    };


    /**
    Removes item from page AND SERVER
    @param: item -item to be removed
 */
    $scope.remove = function (item) {
        console.log("EditItems_controller remove item=", item);

        if (confirm('Are you sure you want to delete item "' + item.Name + '" with value '+item.Value)) {
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

    /**
   Redirects to list screen
   */
    $scope.goToList = function (item) {
        console.log('++++++++ goToList ');
        $location.path("/ShowItems");
    };

    /**
   Redirects to edit all screen
   */
    $scope.goToEditAll = function (item) {
        console.log('++++++++ goToEditAll ');
        $location.path("/EditItems");
    };

    /**
   Redirects to edit item with id
   */
    $scope.edit = function (item) {
        console.log('++++++++ edit item.Id = ', item.Id);
        $location.path("/EditItem/" + item.Id );
    };
 


    $scope.init = function () {

        $scope.getTopN(5, 1, function (response) { }, function (response) { });

        Post.GetPostsTo(5, 10, function (response) { }, function (response) { });

        Notification.GetLatestNotificationForProject(1, 3,
        function (notifications) {
            console.log('notifications = ', notifications);
        },
        function (response) {
            console.error('notifications = ', notifications);
        });
       
    };


    /** Changes the language */
    $scope.changeLanguage = function (langKey) {
        if (langKey === 'en') {
            $translate.fallbackLanguage('fr');
        } else if (langKey == 'de') {
            $translate.fallbackLanguage('en');
        }
        $translate.use(langKey);
    };


    /** searces projects for given keyword */
    $scope.search = function (keywords) {
        console.log("   $scope.search keywords = ", keywords);
        Project.search(keywords,
         function (response) {
             console.log(response);
             $scope.Projects = response;
         },
          function (response) {
              console.log('NOT OK, project NOT saved');

          });

    }

   
};