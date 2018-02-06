controllers.Project_controller = function ($scope, $rootScope, $location, $translate, $filter, $window, ErrorHandler, Project, User, UsersProject, Milestone, Phase, TextBlock, TextBlockFile, $routeParams, Upload, Post, Checklist, $timeout, $http, Ajax) {

    $scope.IGGUsersProjects = [];
    $scope.UsersProjects = [];
    $scope.Project = null;
    $scope.uploadProgressShow = false;

    $scope.init = function () {

        $scope.uploadProgressShow = false;
        console.log('NewProject_controller INITED');

        $scope.Project = new Project();
        $scope.Project.ID = -1;
    

        if ($routeParams.id>0) {
            var projectID = $routeParams.id;
            //console.log('$rootScope.rootScope_curentUser.userType=', $rootScope.rootScope_curentUser.userType);
            
            GetChecklistForProject(projectID);

            var thisUser = null;
            User.GetMyAccount(
                function (user) {
                    console.log('GetMyAccount = ', user);
                    thisUser = user;
                    $rootScope.rootScope_curentUser = user;
                    $rootScope.rootScope_curentUser = user;
                    $rootScope.rootScope_username = user.name;
                    if (thisUser.userType != "IGG") {
                        $location.path("/ViewProject/" + projectID);
                        return;
                    }
                    else {
                        console.log('NewProject_controller  $scope.projectID ', projectID);

                        Project.getById(projectID,
                            function (project) {
                                console.log(' NewProject_controller Project project ', project);
                                if (project == null) return;
                                $scope.Project = project;
                                arangeUsers();
                                $scope.TopImage = $scope.Project.photo;
                                $scope.Image = $scope.TopImage;
                                console.log(' ------------ $scope.TopImage = ', $scope.TopImage);
                                $scope.Project.photo = $scope.Image;
                                $scope.Project.timeCompleted = $rootScope.rootScope_ShowDateTime($scope.Project.timeCompleted);
                                AddDatePickers();
                                if (new Date() > Date_Parse($scope.Project.timeCompleted)) {
                                    $scope.OpenVaultShow = true;
                                }

                            },
                            function (error) {
                                console.error(' NewProject_controller Project.getById', error);
                            })
                    }
                },
                function (error) {
                   // $location.path("/ViewProject/" + projectID);
                });

 
        

        }
        else {
            console.log(' NewProject_controller PRAVI SE NOVI PROJEKT ');
            $scope.ShowDetails = false;
            $scope.Project.Phases = [];

            var phase = new Phase();
            phase.ID = 1;
            phase.IDproject = -1;
            phase.date = new Date();
            phase.name = "selection stage";
            $scope.Project.Phases.push(phase);

            var phase = new Phase();
            phase.ID = 2;
            phase.IDproject = -1;
            phase.date = new Date();
            phase.name = "award stage";
            $scope.Project.Phases.push(phase);

            var phase = new Phase();
            phase.ID = 3;
            phase.IDproject = -1;
            phase.date = new Date();
            phase.name = "market consultation";
            $scope.Project.Phases.push(phase);

            var phase = new Phase();
            phase.ID = 4;
            phase.IDproject = -1;
            phase.date = new Date();
            phase.name = "dialogue";
            $scope.Project.Phases.push(phase);
        }
 
        $scope.AllUsers = [];
        User.GetAll(function (users) { $scope.AllUsers = users; console.log('-----  $scope.AllUsers ', $scope.AllUsers); }, function () { });
 
    };



    var arangeUsers = function () {
        if ($scope.Project.UsersProjects == null) $scope.Project.UsersProjects = [];

        for (var i = 0; i < $scope.Project.UsersProjects.length; i++) {
            if ($scope.Project.UsersProjects[i].userType == "IGG") $scope.IGGUsersProjects.push($scope.Project.UsersProjects[i]);
            else $scope.UsersProjects.push($scope.Project.UsersProjects[i]);
        }

        console.log('$scope.IGGUsersProjects ', $scope.IGGUsersProjects);
    }


    /** Returns one of users attached to this project*/
    $scope.getUserForThisProjectById = function (id) {
        if (!id || id <= 0) return null;
        if ($scope.Project.UsersProjects)
        for (var i = 0; i < $scope.Project.UsersProjects.length; i++) {
            if ($scope.Project.UsersProjects[i].User.ID == id) return $scope.Project.UsersProjects[i].User;
        }
        return null;

    }

    /** Returns one of users attached to this project*/
    $scope.getUserForThisProjectByUsername= function (name) {

        for (var i = 0; i < $scope.Project.UsersProjects.length; i++) {
            if ($scope.Project.UsersProjects[i].User.username == name) return $scope.Project.UsersProjects[i].User;
        }
        return null;

    }

 
    //----------------------------------------- ----------------------------------------------
    //-----------------------------------------   POSTS sending 
    $scope.SendNotificationText = $filter('translate')('Are you sure you want to send all notifications to all clients involved in this project');

    $scope.SendNoticesToPeopleOnProject = function () {
        $scope.SendNotificationText = changeNotificationText('Sending in progress');
        Project.SendNoticesToPeopleOnProject($scope.Project.ID,
            function (retval) {
                  changeNotificationText('emails sent succesfully');
            },
            function (retval) {
                changeNotificationText('emails could not be sentr');
            });
    };

    var changeNotificationText = function (newText) {
        $scope.SendNotificationText = $filter('translate')(newText);
    }

   // $scope.ShowResponseModal = false;
    $scope.sendingResponseProgressShow = false;
    $scope.SendPostResponse = function (NewPostText, NewPostImage, NewDefinitionText) {
        $scope.SendNotificationText = changeNotificationText('Sending in progress');
        var theNewPost = new Post();

        theNewPost.ID = -1;
        theNewPost.IDproject = $scope.Project.ID;
        theNewPost.text = NewPostText;
        theNewPost.to = $scope.NewPost.from;
        if (!$scope.Project.UsersProjects || $scope.Project.UsersProjects.length < 1) { if (!confirm($filter('translate')('there are no users on this project, are you sure you want to save this post?'))) return; }
     //   theNewPost.from = $scope.getUserForThisProjectByUsername($rootScope.rootScope_username).ID;
        theNewPost.from = $rootScope.rootScope_curentUser.ID;
        theNewPost.definition = NewDefinitionText;
        theNewPost.image = NewPostImage;
        theNewPost.imageSize = $scope.NewPostImageSize;
        theNewPost.time = new Date;

        $scope.sendingResponseProgressShow = true;

        Project.SendPostResponse($scope.Project.ID, theNewPost, $scope.NewPost,
            function (post) {
                console.log('Project.SendPostResponse  post =', post)
                changeNotificationText('emails sent succesfully');
                $scope.Project.Posts.push(post);
                theNewPost = new Post();

                alert($filter('translate')('sent succesfully'));
                hideAllModalBackgrounds();
             //   $scope.ShowResponseModal = false;
                $scope.sendingResponseProgressShow = false;

            },
            function (retval) {
                changeNotificationText('emails could not be sent');
            });

       
    };
//    $scope.NewPost = new Post();
    $scope.setNewPost = function (note) {
        console.log(' $scope.setNewPost note=', note);
        $scope.NewPost = note;
        $scope.ShowResponseModal = true;
        
    }

    function clone(obj) {
        if (null == obj || "object" != typeof obj) return obj;
        var copy = obj.constructor();
        for (var attr in obj) {
            if (obj.hasOwnProperty(attr)) copy[attr] = obj[attr];
        }
        return copy;
    }

    $scope.getPostImageAddress = function (image) {
        return Post.GetImageAddress(image);
    }
    //-----------------------------------------   / POSTS sending 
    //----------------------------------------- ----------------------------------------------

    $scope.GetProjectImageAddress = function (image) {
       // console.log('GetProjectImageAddress Project.GetImageAddress(image) =', Project.GetImageAddress(image));
        return Project.GetImageAddress(image);
    }

    $scope.GetUser = function (image) {
        return Project.GetImageAddress(image);
    }

    $scope.GetuserImageAddress = function (image) {
        return User.GetImageAddress(image);
    }


    $scope.Save = function () {
        $scope.Project.photo = $scope.Image;
        $scope.Project.photoThumbnail = $scope.Image;

        if ($scope.Project.ID <= 0) { // if creating new project

         //   if (!$scope.Project.UsersProjects)
            $scope.Project.UsersProjects = [];

            console.log(' $scope.Save $scope.IGGUsersProjects = ', $scope.IGGUsersProjects);
            console.log(' $scope.Save $scope.UsersProjects = ', $scope.UsersProjects);

            for (var i = 0; i < $scope.IGGUsersProjects.length; i++) {
                $scope.IGGUsersProjects[i].userType = "IGG";
                $scope.Project.UsersProjects.push($scope.IGGUsersProjects[i]);
            }

            for (var i = 0; i < $scope.UsersProjects.length; i++) {
                $scope.Project.UsersProjects.push($scope.UsersProjects[i]);
            }
            for (var i = 0; i < $scope.Project.TextBlocks.length; i++) {

                //--- remove textblocks with no files
                if ($scope.Project.TextBlocks[i].Files.length < 1) {
                    removeObjectFromArray($scope.Project.TextBlocks, $scope.Project.TextBlocks[i]);
                    console.log('------- REMOVED FROM ARRAY ', $scope.Project.TextBlocks[i]);
                }

                //--- remove extra fields from objects
                delete $scope.Project.TextBlocks[i].NewTextBlockFile;
            }

            

        }


        //---- fix for the picker
        if ($scope.Project.timeCompleted instanceof Date) {
            console.log('ALREady a date ');
        }
        else {
            console.log('$scope.Project.timeCompleted ', $scope.Project.timeCompleted);
           
                var dateCompleted = Date_Parse_FromPicker($scope.Project.timeCompleted);
                if (dateCompleted && dateCompleted.getFullYear() < 1970) {
                    dateCompleted = Date_Parse($scope.Project.timeCompleted);
                }
                $scope.Project.timeCompleted = dateCompleted;
        }
        //---- / fix for the picker

        $scope.Message = "";
        Project.save($scope.Project,
            function (project) {
                console.log('project save, project = ', project);
                if ($scope.Project.ID <= 0) $location.path("/Project/" + project.ID);
                $scope.Project = project;
                $scope.Project.timeCompleted = $rootScope.rootScope_ShowDateTime($scope.Project.timeCompleted);
                $scope.TopImage = $scope.Project.photo;
                $scope.Image = $scope.TopImage;
                $routeParams.id = $scope.Project.ID;
                alert($filter('translate')('saved succesfully'));
               // $scope.Message = $filter('translate')('saved succesfully');
                console.log($scope.Message);

            },
            function (error) {
               
                console.log('project NOT saved, error=', error);
        });
    }


    $scope.removePhase = function (phase) {
        removeObjectFromArray($scope.Project.Phases, phase);
        console.log('phase removed $scope.Project.Phases =', $scope.Project.Phases);
    }

    $scope.removeFile = function (file) {
        console.log('file remove ', file);
        console.log('$scope.Project ', $scope.Project);
        if (!$scope.Project.TextBlocks) return;

        TextBlockFile.delete(file,
            function (response) {
                for (var i = 0; i < $scope.Project.TextBlocks.length; i++) {
                    if ($scope.Project.TextBlocks[i].ID != file.IDTextBlock) continue;
                    removeObjectFromArray($scope.Project.TextBlocks[i].Files, file);
                    if ($scope.Project.TextBlocks[i].Files.length < 1) {
                        TextBlock.delete($scope.Project.TextBlocks[i],
                            function (response) {
                             //   removeObjectFromArray($scope.Project.TextBlocks, $scope.Project.TextBlocks[i]);
                            },
                            function (err) {
                            }
                         );
                     removeObjectFromArray($scope.Project.TextBlocks, $scope.Project.TextBlocks[i]);
                    }
                    
                }
            }, function (err) {

            });

        console.log('file removed $scope.Project.TextBlocks =', $scope.Project.TextBlocks);
    }


    $scope.removeTextBlock = function (theTextBlock) {

        console.log('$scope.removeTextBlock , block = ', theTextBlock);
        TextBlock.delete(theTextBlock,
                        function (response) {
                            //   removeObjectFromArray($scope.Project.TextBlocks, $scope.Project.TextBlocks[i]);
                        },
                        function (err) {
                        }
                     );
        removeObjectFromArray($scope.Project.TextBlocks, theTextBlock);

    }

    $scope.addTextBlockFile = function (textBlockFile, textBoxID) {
        console.log(textBlockFile);
        if (!textBlockFile) return;
        if (!textBlockFile.file) return;

        textBlockFile.dateUploaded = new Date();
        textBlockFile.dateModified = new Date();
        textBlockFile.IDTextBlock = textBoxID;
        textBlockFile.ID = -1;

        console.log('addFile textBoxID=' + textBoxID + " textBlockFile =", textBlockFile);
   
        for (var i = 0; i < $scope.Project.TextBlocks.length; i++) {
            if ($scope.Project.TextBlocks[i].ID == textBoxID) {
                if (!$scope.Project.TextBlocks[i].Files) $scope.Project.TextBlocks[i].Files = [];
                $scope.Project.TextBlocks[i].Files.push(textBlockFile);

                //------- save the textbox:
                TextBlock.save($scope.Project.TextBlocks[i],
                    function (response) {
                        console.log('TextBlock saved');
                        console.log(response);
                        $scope.Project.TextBlocks[i] = response;  
                      
                    },
                    function (error) {
                        console.error(error);
                    });

                $scope.Project.TextBlocks[i].NewTextBlockFile = new TextBlockFile();
                break;
            }
        }



        console.log("Project = ",$scope.Project);
    }
 

    var minTextBlockID = -1;
    $scope.addTextBlock = function () {
        var NewTextBlock = null;
        var NewTextBlockFile = null;
        NewTextBlock = new TextBlock();
        NewTextBlock.NewTextBlockFile = new TextBlockFile();
        NewTextBlock.IDproject = $scope.Project.ID;
        NewTextBlock.Files = [];
        NewTextBlock.text = '';
        NewTextBlock.ID = minTextBlockID;
        minTextBlockID--;
        NewTextBlock.time = new Date();
        $scope.datePickers.push(new DatePicker());
       // console.log(' $scope.datePickers =', $scope.datePickers);

        if (!$scope.Project.TextBlocks) $scope.Project.TextBlocks = [];
        $scope.Project.TextBlocks.push(NewTextBlock);
        console.log('  $scope.Project.TextBlocks =', $scope.Project.TextBlocks);
    }

    //-----------------------------------------  milestones  - DONE

    $scope.removeMilestone = function (milestone) {
        console.log('milestone remove ', milestone);
        if ($scope.Project.ID > 0) {  // if editing existing project 
            Milestone.delete(milestone, function (response) {
                removeObjectFromArray($scope.Project.Milestones, milestone);
                console.log('file removed $scope.Project.TextBlocks =', $scope.Project.Milestones);
            }, function (response) {
                console.error('MILESTONE CAN NOT BE REMOVED');
            });
        }
        else {  // if creating new project
            removeObjectFromArray($scope.Project.Milestones, milestone);
        }
       
    }

    var minMilestoneID = -1;
    $scope.addMilestone = function (milestone) {
        $scope.datePickerSchedule.opened = false;
        milestone.IDproject = $scope.Project.ID;
        milestone.ID = minMilestoneID;
        minMilestoneID--;
        console.log('$scope.addMilestone ', milestone);
        $scope.NewMilestone = new Milestone();

        if ($scope.Project.ID > 0) {  // if editing existing project 
            Milestone.save(milestone, function (response) {
                if (!$scope.Project.Milestones) $scope.Project.Milestones = [];
                $scope.Project.Milestones.push(milestone);
                $scope.datePickerSchedule.opened = false;
            }, function (response) {
                console.error(' $scope.addMilestone   Milestone.save');
            });
        }
        else {  // if creating new project
            $scope.Project.Milestones.push(milestone);
        }

        $scope.datePickerSchedule.opened = false;
    }

    //----------------------------------------- / milestones 

    $scope.closeProject = function () {
        if (confirm($filter('translate')('are you sure you want to close this project'))) {
            Project.close($scope.Project, function () {
                console.log('PROJECT CLOSED');
                $location.path("/Project/" + $scope.Project.ID);
            }, function () {
                console.error('PROJECT COULD NOT BE CLOSED');
            });
        }
    }


    $scope.removeProject = function () {
        if (confirm($filter('translate')('are you sure you want to delete this project'))) {
               Project.delete($scope.Project, function () {
                        console.log('PROJECT REMOVED');
                        $location.path("/Admin");
                    }, function () {
                        console.error('PROJECT COULD NOT BE CLOSED');
                    });
        }
    }


    $scope.viewProject = function () {
        $location.path("/ViewProject/" + $scope.Project.ID);
    }

    /** Object must have ID */
    var removeObjectFromArray = function (arrayOfObjects, objectToRemove) {
        if (!arrayOfObjects) return;
        for(var i = 0; i < arrayOfObjects.length; i++) {
            if (objectToRemove.ID == arrayOfObjects[i].ID) {
                arrayOfObjects.splice(i, 1);
               //   console.log(' removeFromArray found on ',i);
            }
        }
    }

    /** Object must have ID */
    var removeObjectFromArraybyID = function (arrayOfObjects, ID) {
        if (!arrayOfObjects) return;
        for (var i = 0; i < arrayOfObjects.length; i++) {
            if (ID == arrayOfObjects[i].ID) {
                arrayOfObjects.splice(i, 1);
            }
        }
    }

    //--------------------------------------------------------
    //--------------------- datepicker:
   
    var DatePicker = function () {

       // this.dt is variable in which the date will be returned

        this.today = function () {
            this.dt = $rootScope.rootScope_ShowDateTime(new Date());
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
            'starting-day': 1,
            'showButtonBar': false
        };

        this.formats = ['dd-MM-yyyy', 'yyyy/MM/dd', 'shortDate'];
        this.format = this.formats[0];

    }


    $scope.ShowTime = function (time) {
        return Date_WriteDate(time);  
    }

    $scope.ShowDateTime = function (time) {
        return Date_WriteDateTime(time);
    }


    $scope.datePickerGeneral = new DatePicker();
    $scope.datePickerGeneral.format =  'dd-MM-yyyy HH:mm';
    $scope.datePickerSchedule = new DatePicker();
    $scope.datePickerDownloads = new DatePicker();
    $scope.datePickerDownloads2 = new DatePicker();

    $scope.datePickerUserFrom = new DatePicker();
    $scope.datePickerUserFrom.minDate = new Date();
    $scope.datePickerUserFrom.maxDate = new Date();
    $scope.datePickerUserTo = new DatePicker();
    $scope.datePickerUserTo.minDate = new Date();
    $scope.datePickerUserTo.maxDate = new Date();

    $scope.datePickerEditedUserProjectFrom = new DatePicker();
    $scope.datePickerEditedUserProjectTo = new DatePicker();
    $scope.datePickerMilestoneForEdit = new DatePicker();
    $scope.datePickerMilestoneForEdit.format = 'dd-MM-yyyy';

    $scope.datePickerNotification = new DatePicker();
    $scope.datePickerNotification.format = 'dd-MM-yyyy';

    $scope.datePickerNotificationEditMilestone = new DatePicker();
    $scope.datePickerNotificationEditMilestone.format = 'dd-MM-yyyy';


    $scope.datePickers = [];
    var AddDatePickers = function () {
        if (!$scope.Project.TextBlocks) return;
        for (var i = 0; i < $scope.Project.TextBlocks.length; i++) {
            $scope.datePickers.push(new DatePicker());
        }

        console.log(" $scope.datePickers =", $scope.datePickers);
    }

    //--------------------- / datepicker
    //--------------------------------------------------------


    //--------------------------------------------------------
    //--------------------- upload
 
    $scope.log = '';

    $scope.uploadImage = function (files) {
        //  $scope.uploadProgressShow = true;

         var URL = '/Project/UploadImage?projID=';
 
        if (files && files.length) {
            $scope.uploadProgressShow = true;
            for (var i = 0; i < files.length; i++) {
                var file = files[i];
                if (!file.$error) {
                    
                    Upload.upload({
                        url: URL + $scope.Project.ID,
                        data: {
                            username: $scope.username,
                            file: file
                        }
                    }).then(function (resp) {
                        $timeout(function () {
                         /* $scope.log = 'file: ' + resp.config.data.file.name +  ', Response: ' + JSON.stringify(resp.data) + '\n' + $scope.log; */
                            console.log(' $$$$$$$$$$$$$$$$$$$$ $scope.uploadImage ', resp);
                            $scope.Project.photo = resp.data.Data.name;
                            $scope.Image = $scope.Project.photo;
                            $scope.TopImage = $scope.Image;
                            console.log('++++ $scope.Project.photo = ', $scope.Project.photo);
                            $scope.uploadProgressShow = false;
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


     $scope.uploadPostImage = function (files, from) {
        //  $scope.uploadProgressShow = true;

         var URL = '/Post/UploadImage?postID=' + $scope.NewPost.ID + '&projectID='+$scope.Project.ID;

        if (files && files.length) {
            $scope.uploadProgressShow = true;
            for (var i = 0; i < files.length; i++) {
                var file = files[i];
                if (!file.$error) {

                    Upload.upload({
                        url: URL + $scope.Project.ID,
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
                            console.log(' $$$$$$$$$$$$$$$$$$$$ $scope.uploadPostImage ', resp);
                            ///   console.log('$timeout(function, $scope.log = ', $scope.log);
                            $scope.NewPostImage = resp.data.Data.name;
                            $scope.NewPostImageSize = resp.data.Data.size;

                           
                            
                            console.log(' $scope.NewPost = ', $scope.NewPost);
                            $scope.uploadProgressShow = false;
                        });
                    }, null, function (evt) {
                        var progressPercentage = parseInt(100.0 *
                                evt.loaded / evt.total);
                        $scope.uploadProgressPercentage = progressPercentage;
                        $scope.log = 'progress: ' + progressPercentage +
                            '% ' + evt.config.data.file.name + '\n' +
                          $scope.log;
                        console.log('$scope.uploadProgressPercentage = progressPercentage');
                    });
                }
            }
        }
    };

    $scope.NewTextBlockFile = new TextBlockFile();

    $scope.uploadTextBlockFile = function (files, textBlock) {
       URL = '/Project/UploadFile?projID=';
       console.log('----- textBlock =',textBlock);
        if (files && files.length) {
            $scope.uploadProgressShow = true;
            for (var i = 0; i < files.length; i++) {
                var file = files[i];
                if (!file.$error) {
                    Upload.upload({
                        url: URL + $scope.Project.ID,
                        data: {
                            username: $scope.username,
                            file: file
                        }
                    }).then(function (resp) {
                        $timeout(function () {
                            // console.log(' $$$$$$$$$$$$$$$$$$$$ $scope.uploadTextBlockFile ', resp);

                            if (!textBlock.NewTextBlockFile) textBlock.NewTextBlockFile = new TextBlockFile();
                            textBlock.NewTextBlockFile.file = resp.data.Data.name;
                            textBlock.NewTextBlockFile.size = resp.data.Data.size;
                            $scope.uploadProgressShow = false;
                        });
                    }, null, function (evt) {
                        var progressPercentage = parseInt(100.0 * evt.loaded / evt.total);
                        $scope.uploadProgressPercentage = progressPercentage;
                        $scope.log = 'progress: ' + progressPercentage + '% ' + evt.config.data.file.name + '\n' + $scope.log;
                        // console.log('$scope.uploadProgressPercentage = progressPercentage');
                    });
                }
            }
        }
    };
    //--------------------- / upload
    //--------------------------------------------------------

    //--------------------------------------------------------
    //-----------------------------UsersProject s-------------
// UsersProject
    $scope.NewUserProject = new UsersProject();
    $scope.NewUserProject.beganWithProject = new Date();
    $scope.NewUserProject.endedWithProject = new Date();
    $scope.NewUserProject.endedWithProject.setMonth($scope.NewUserProject.beganWithProject.getMonth() + 1);

    $scope.addUser = function () {
        if ($scope.Project.ID > 0) {  // if editing existing project 
            $scope.NewUserProject.IDproject = $scope.Project.ID;
            $scope.NewUserProject.User = $scope.getUserForThisProjectById($scope.NewUserProject.IDuser);
            //   $scope.UsersProjects.push($scope.NewUserProject);
          //  console.log('  $scope.addUser   $scope.UsersProjects =', $scope.UsersProjects);
           
            for (var i = 0; i < $scope.UsersProjects.length; i++) {
                if ($scope.NewUserProject.IDuser == $scope.UsersProjects[i].IDuser) {
                    alert($filter('translate')('user already added'));
                    return;
                }
            }

            Project.SendNewUserInProjectMail($scope.Project.ID, $scope.NewUserProject,
              function (retval) {
                  $scope.UsersProjects.push(UsersProject.createFromResponse(retval));
                  changeNotificationText('emails sent succesfully');
                  $scope.EditUserProjectShow = false;
                  hideAllModalBackgrounds();
                  $('#Gebruiker').hide();
              },
              function (retval) {
                  changeNotificationText('emails could not be sent');
              });

        }
        else { // if creating new project
            for (var i = 0; i < $scope.UsersProjects.length; i++) {
                if ($scope.NewUserProject.IDuser == $scope.UsersProjects[i].IDuser) {
                    alert($filter('translate')('user already added'));
                    return;
                }
            }
            for (var i = 0; i < $scope.AllUsers.length; i++) {
                if ($scope.AllUsers[i].ID == $scope.NewUserProject.IDuser) {
                    var up = new UsersProject();
                    up.IDproject = -1;
                    up.IDuser = $scope.AllUsers[i].ID;
                    up.User = $scope.AllUsers[i];
                    $scope.UsersProjects.push(up);
                    break;
                }
            }
            $scope.EditUserProjectShow = false;
            hideAllModalBackgrounds();
        }
    };

    $scope.EditUserProjectShow = false;
    $scope.EditedUserProject = null;
    $scope.editUserProject = function (userProject) {
        $scope.EditUserProjectShow = true;
        console.log('    $scope.editUserProject userProject =', userProject);
        $scope.EditedUserProject = userProject;
    };

    $scope.deleteEditedUserProject = function () {
        //  console.log('    $scope.deleteUserProject userProject =', userProject);
        if ($scope.Project.ID > 0) {  // if editing existing project 
            UsersProject.delete($scope.EditedUserProject,
                function (response) {
                    removeObjectFromArray($scope.UsersProjects, $scope.EditedUserProject);
                    $scope.EditUserProjectShow = false;
                    hideAllModalBackgrounds();
                   // console.log('    $scope.deleteUserProject removed   =', $scope.EditedUserProject);
                }
               , function (response) {


               });
        }
        else {// if creating new project
            removeObjectFromArray($scope.UsersProjects, $scope.EditedUserProject);
            $scope.EditUserProjectShow = false;
            hideAllModalBackgrounds();
        }
    };

    $scope.saveEditedUserProject = function () {
        //console.log('    $scope.addUserProject userProject =', userProject);
        if ($scope.Project.ID > 0) {  // if editing existing project 
            UsersProject.save($scope.EditedUserProject,
                function (response) {
                   // console.log('$scope.saveEditedUserProject response = ', response);
                    $scope.EditUserProjectShow = false;
                    hideAllModalBackgrounds();
                }
               , function (response) {


               });
        }
        else {// if creating new project
            $scope.EditUserProjectShow = false;
            hideAllModalBackgrounds();

        }
    };


    /** Hides all modal backgrounds and windows**/
    var hideAllModalBackgrounds = function () {
      //  $('.modal-backdrop').modal('toggle');// ovo menja stanje
        $('.modal').modal('hide');
    }


    $scope.closeProgress = function () {
        $scope.uploadProgressShow = false;
    }


    $scope.removeIGGer = function (igger) {
        if ($scope.Project.ID > 0) {  // if editing existing project 
            UsersProject.delete(igger,
             function (response) {
               //  console.log('......................... removeIGGer   response =', igger);
               //  console.log('......................... removeIGGer   $scope.IGGUsersProjects =', $scope.IGGUsersProjects);
                 removeObjectFromArray($scope.IGGUsersProjects, igger);
                 // $scope.EditUserProjectShow = false;
                 // hideAllModalBackgrounds();
               //  console.log('    removeIGGer   =', igger);
             }
            , function (response) {


            });
        }
        else {// if creating new project
            removeObjectFromArray($scope.IGGUsersProjects, igger);
        }
    }

    //---------------------------------- iggers
    $scope.addIGGerShow = true;
    $scope.NewIGGerID = null;
    $scope.addIGGer = function (iggerID) {
        for (var i = 0; i < $scope.IGGUsersProjects.length; i++) {
            if ($scope.IGGUsersProjects[i].IDuser == iggerID) {
                alert($filter('translate')('The IGG user has already been added, please chose another IGG user'));
                return;
            }
        }

        var iggger = null;
        for (var i = 0; i < $scope.AllUsers.length; i++) {
            if ($scope.AllUsers[i].ID == iggerID) iggger = $scope.AllUsers[i];
        }
        //console.log(' add new iggger = ', iggger);


        if ($scope.Project.ID > 0) {  // if editing existing project 
            UsersProject.AddIggerToProject(iggerID, $scope.Project.ID,
                function (response) {
                    $scope.IGGUsersProjects.push(response);
                    hideAllModalBackgrounds();
                },
                function (response) {

                });
        }
        else {// if creating new project
            for (var i = 0; i < $scope.AllUsers.length; i++) {
                if ($scope.AllUsers[i].ID == iggerID) {
                    var up = new UsersProject();
                    up.IDproject = -1;
                    up.IDuser = $scope.AllUsers[i].ID;
                    up.User = $scope.AllUsers[i];
                    $scope.IGGUsersProjects.push(up);
                    break;
                }
            }
        }
    }




    //----------------------------- /UsersProject s ---------
    //--------------------------------------------------------
 
    $scope.toMegabytes = function (bytes) {
        return Math.floor(bytes / 1000000);
    };


    $scope.openedvault =[];
    $scope.OpenVault = function () {
        Project.OpenVault($scope.Project.ID,
            function (response) {
                // $scope.openedvault = response;
                //  console.log('$scope.openedvault ', $scope.openedvault);
                $scope.openedvault = [];
                $scope.openedvault.push({ User: response[0].User, files: [] });
                $scope.openedvault[0].files.push(response[0]);
                var uconuter = 0;
                for (var i = 1; i < response.length; i++) {
                    if (response[i].User != response[i - 1].User) {
                        $scope.openedvault.push({ User: response[i].User, files: [] });
                        uconuter++;
                    }
                    $scope.openedvault[uconuter].files.push(response[i]);
                    // $scope.openedvault.push();
                }

                //  console.log('$scope.openedvault ', $scope.openedvault);
            },
            function () {


            });
    };


    $scope.downloadAllFilesForVault = function () {
        Ajax.Download("", "/Project/DownloadAllFilesForVault?projectID=" + $scope.Project.ID + "&language=" + $rootScope.rootScope_language);
    };

    $scope.downloadSVaultFile = function (file) {
       // console.log('downloadSingleFile file = ', file);
        Ajax.Download("/UPLOADED_FILES/projects/" + file.File);
    };

    $scope.ShowDetails = true;
    $scope.digitalFilingChanged = function (event) {
        if (event.target.checked) {
            if (new Date() > Date_Parse($scope.Project.timeCompleted)) {
                $scope.OpenVaultShow = true;
            }
        }
        else {
            $scope.OpenVaultShow = false;
        } 
    };

    $scope.timeCompletedChanged = function (ovaj) {
        console.log(ovaj);
    };

    $scope.showPlanning = false;
    $scope.MilestoneForEdit = null;
    $scope.editMilestone = function (milestone) {
        $scope.showPlanning = true;
        $scope.MilestoneForEdit = milestone;
        console.error('editMilestone NOT IMPLEMENTED ', milestone);
    };

    $scope.saveEditedMilestone = function (milestone) {
            Milestone.save(milestone, function (response) {
                //console.log(' $scope.saveEditedMilestone   Milestone.save response =', response);
            }, function (response) {
               // console.error(' $scope.addMilestone   Milestone.save');
            });
    };


    $scope.downloadFile = function (file) {
        Ajax.Download(file);
    };


    $scope.getUserById = function (id) {
        for (var i = 0; i < $scope.AllUsers.length; i++) {
            if ($scope.AllUsers[i].ID == id) return $scope.AllUsers[i];
        }
    };


    $scope.upItem = function (ID, itemOrder) {
      //  console.log('$scope.upItem ', ID);
        for (var i = 0; i < $scope.Checklist.length; i++) {
            if ($scope.Checklist[i].ID == ID) {
                $scope.Checklist[i].itemOrder--;
                Checklist.save($scope.Checklist[i], function () { }, function () { });
            }
            else if ($scope.Checklist[i].itemOrder == itemOrder-1) {
                    $scope.Checklist[i].itemOrder++; 
                    Checklist.save($scope.Checklist[i], function () { GetChecklistForProject($scope.Project.ID) }, function () { });
            }  
        }
      //  console.log('@@@@@@ $scope.upItem ', $scope.Checklist);
    };



    $scope.downItem = function (ID, itemOrder) {
        // console.log('$scope.downItem ', ID);

        for (var i = 0; i < $scope.Checklist.length; i++) {
            if ($scope.Checklist[i].ID == ID) {
                $scope.Checklist[i].itemOrder++;
                Checklist.save($scope.Checklist[i], function () { }, function () { });
            }
            else if ($scope.Checklist[i].itemOrder == itemOrder + 1) {
                $scope.Checklist[i].itemOrder--;
                Checklist.save($scope.Checklist[i], function () { GetChecklistForProject($scope.Project.ID) }, function () { });
            }
        }
      //  console.log('##### $scope.downItem ', $scope.Checklist);
    };


    $scope.removeChecklistItem = function (ID) {
       // console.log('$scope.removeChecklistItem ', ID);
        Checklist.removeItem(ID, 
            function (response) {
               // console.log('$scope.removeChecklistItem, response = ', response);
                removeObjectFromArraybyID($scope.Checklist, response)
            },
            function(error) {
                console.error('$scope.removeChecklistItem, ERROR = ', response);
            })
    };


    var GetChecklistForProject = function (projectID) {
       // console.log('GetChecklistForProject ', projectID);
        Checklist.GetForProject(projectID,
            function (response) {
               // console.log('GetChecklistForProject ', response);
                $scope.Checklist = response;
            },
            function (error) {
                console.error('GetChecklistForProject ', error);
            });

    };
    $scope.showItemChange = [];
    $scope.addChecklistItem = function () {
        //console.log($scope.NewChecklistItem);
        var maxOrder = 0;
        for (var i = 0; i < $scope.Checklist.length; i++) {
            if ($scope.Checklist[i].itemOrder > maxOrder) {
                maxOrder = $scope.Checklist[i].itemOrder;
            }
            $scope.showItemChange[$scope.Checklist[i].ID] = false;
        }

        // console.log('maxOrder = ', maxOrder);

        var newOne = new Checklist();
        newOne.ID = -1;
        newOne.item = $scope.NewChecklistItem;
        newOne.itemOrder = maxOrder + 1;
        newOne.projectID = $scope.Project.ID;

        $scope.Checklist.push(newOne);
        Checklist.save(newOne, function () { GetChecklistForProject($scope.Project.ID); $scope.NewChecklistItem = ""; }, function () { });
        // console.log('$scope.Checklist = ', $scope.Checklist);

    };


    $scope.showChangeName = function (elem, ID) {
        //  console.log('showChangeName elem = ', elem);
        for (var i = 0; i < 1000; i++)
            if ($scope.showItemChange[i]) $scope.showItemChange[i] = false;

        $scope.showItemChange[ID] = true;
    };

    $scope.renameListItem = function (id) {
        // console.log('renameListItem id = ', id);
        for (var i = 0; i < $scope.Checklist.length; i++) {
            $scope.showItemChange[$scope.Checklist[i].ID] = false;
            if ($scope.Checklist[i].ID == id) {
                Checklist.save($scope.Checklist[i], function () { GetChecklistForProject($scope.Project.ID) }, function () { });
            }
        }
    };
}