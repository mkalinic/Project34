controllers.ViewProject_controller = function ($scope, $rootScope, $location, $translate, $filter, $window, ErrorHandler, Project, User, UsersProject, Milestone, Phase, TextBlock, TextBlockFile, $routeParams, Upload, Post, $timeout, $http, Ajax, ProjectFile, Checklist, ChecklistChecked) {

    $scope.init = function () {

      

        $scope.now = new Date();
        $scope.Project = new Project();
        $scope.Project.ID = -1;


        if ($routeParams.id > 0) {
            var projectID = $routeParams.id;

            Project.getById(projectID,
                function (project) {
                    if (project == null) return;
                    $scope.Project = project;
                    setupGraph(project);
                    setUpContactPersons(project);
                    GetChecklistForProject($scope.Project.ID);
                },
                function (error) {
                    console.error(' NewProject_controller Project.getById', error);
                })

            User.GetMyAccount(function (user) {
                    console.log('----------- ', user);
                    $rootScope.rootScope_curentUser = user;
                    $rootScope.rootScope_username = user.name;
                    if (user) {
                        console.log('LOGGED USER = ', user);
                    }
                    else {
                    }
                 },
              function (err) {

              });

        }
    }

    $scope.ContactPersons = [];
    var setUpContactPersons = function (project) {
        if (!project.UsersProjects) { project.UsersProjects = []; return; }
        for (var i = 0; i < project.UsersProjects.length; i++) {
            if (project.UsersProjects[i].userType == "IGG") {
                $scope.ContactPersons.push(project.UsersProjects[i]);
                console.log('  $scope.ContactPersons = ', $scope.ContactPersons);
            }
        }
    }
    //----------------------------------------------------
    //-----------   GRAPH
    $scope.ShowTime = function (time) {
        return Date_WriteDate(time); //"pera";
    }

    var PointOnLine = function (date, color, text) {
        this.Date = date;
        this.Color = color;
        this.Text = text;
    }

    var setupGraph = function (project) {
        if (!project.Milestones) { project.Milestones = []; return; }
      //  console.log('::::: setupGraph project = ', project);
        var dots = document.getElementsByTagName("circle");  
        if (dots.length > 0) return;

        var allPointsOnline = [];
        var the_data = [];
        var minTime = new Date(01,01,3000);
        var maxTime =  new Date(01,01,2000);
        var nowLinePushed = false
        ;
  //------------ visibility
        var allowedMilestones = ["CANDIDATE"];
        var theUsersTypeOnProject = null; // a role user has on this project
        // if there are no users on this project return:
        if (!$scope.Project.UsersProjects) return; 

        //--- find a role that this user has on this project:
            for (var i = 0; i < $scope.Project.UsersProjects.length; i++) {
                if ($scope.Project.UsersProjects[i].User.ID == $rootScope.rootScope_curentUser.ID) {
                    theUsersTypeOnProject = $scope.Project.UsersProjects[i].userType;
                    break;
                }
            }
 


        //console.log(' theUsersTypeOnProject ', theUsersTypeOnProject);
     /*   if ($rootScope.rootScope_curentUser.userType != "CANDIDATE") {
            allowedMilestones = ["CANDIDATE"];
        }
        else*/
        if (theUsersTypeOnProject == "CLIENT") {
            allowedMilestones = ["CANDIDATE", "CLIENT"];
        }
        else if (theUsersTypeOnProject == "CONSULTANT") {
            allowedMilestones = ["CANDIDATE", "CLIENT", "CONSULTANT"];
        }
        else if (theUsersTypeOnProject == "TENDER-TEAM") {
            allowedMilestones = ["CANDIDATE", "CLIENT", "CONSULTANT", "TENDER-TEAM"];
        }
        else if (theUsersTypeOnProject == "IGG") {
            allowedMilestones = ["CANDIDATE", "CLIENT", "CONSULTANT", "TENDER-TEAM", "IGG"];
        }


        for (var i = 0; i < project.Milestones.length; i++) {
            var doIshowThis = false;
            for (var j = 0; j < allowedMilestones.length; j++) {
                if (project.Milestones[i].visibleFor == allowedMilestones[j]) {
                    doIshowThis = true;
                }
            }

            if (!doIshowThis) continue;

  //---------- / visibility

            var color = "#f25e4f"; //"orange";
            if (project.Milestones[i].visibleFor == "CANDIDATE") color = "#0B3B17"; //"green";
            else if (project.Milestones[i].visibleFor == "CONSULTANT") color = "#22376e"; //"blue";
            else if (project.Milestones[i].visibleFor == "TENDER-TEAM") color = "#2f79bd";//"light blue";
            
            allPointsOnline.push(new PointOnLine(project.Milestones[i].time.getTime(), color, project.Milestones[i].name));
            the_data.push({ "period": project.Milestones[i].time.getTime(), "licensed": 0 });
 
            //--- PUSHING THE now line on its place
            if (project.Milestones[i + 1] && !nowLinePushed)
                if (project.Milestones[i].time <= new Date() && new Date() <= project.Milestones[i+1].time) {
                allPointsOnline.push(new PointOnLine(new Date().getTime(), 'red', 'NOW_LINE'));
                the_data.push({ "period": new Date().getTime(), "licensed": 0 });
             //   console.log('++++++++++++++ PUSHED');
                nowLinePushed = true; 
            }
        }
 
       // console.log('allPointsOnline = ', allPointsOnline);
       // console.log('week_data = ', week_data);

        Morris.Line({
            element: 'graph',
            data: the_data,
            xkey: 'period',
            ykeys: ['licensed'],
            labels: ['Labela'],
            grid: false,
            yLabelFormat: function () { return ""; },
            pointSize: 7,
            lineColors: ['#000'],
            lineWidth: 1,
            smooth: true,
            fillOpacity: 1,
            hideHover: true
        });

 
        for (i = 0; i < allPointsOnline.length; i++) {
            dots[i].style.fill = allPointsOnline[i].Color;
            dots[i].style.stroke = allPointsOnline[i].Color;
            dots[i].style.cy = 153; // polozaj kuglica u okviru grafa

            var lineHeight = Math.floor(Math.random() * 100) + 30; // height of one line
        //    if (i % 2 == 0) lineHeight = Math.floor(Math.random() * 120) + 30;
             
      
           // console.log(dots[i].cx.baseVal.value);

            //---------   if point has text NOW_LINE then it is the now line
            if (allPointsOnline[i].Text == "NOW_LINE") {
                dots[i].style.fill = 'transparent';
                dots[i].style.stroke = 'transparent';
                document.getElementById('graph').innerHTML += '<div class="linijaDanas" style=" left:' + (dots[i].cx.baseVal.value + 14) + 'px; top:' + (dots[i].cy.baseVal.value   - 150) + 'px; "></div>';
            }
            else
            if (i % 2 == 0) {
                document.getElementById('graph').innerHTML += '<div class="baloncic" style="left:' + (dots[i].cx.baseVal.value) + 'px; top:' + (dots[i].cy.baseVal.value - lineHeight - 43) + 'px; margin-left:-15px;">' + allPointsOnline[i].Text + '<br />(' + Date_WriteDate(new Date(allPointsOnline[i].Date)) + ')</div>';
                document.getElementById('graph').innerHTML += '<div class="linija" style="left:' + (dots[i].cx.baseVal.value + 14) + 'px; top:' + (dots[i].cy.baseVal.value - lineHeight-14) + 'px; margin-left:-15px; height:' + lineHeight + 'px;""></div>';
            }
            else {
                document.getElementById('graph').innerHTML += '<div class="baloncic" style="left:' + (dots[i].cx.baseVal.value) + 'px; top:' + (dots[i].cy.baseVal.value + lineHeight - 9) + 'px; margin-left:-15px;">' + allPointsOnline[i].Text + ' <br />(' + Date_WriteDate(new Date(allPointsOnline[i].Date)) + ')</div>';
                document.getElementById('graph').innerHTML += '<div class="linija" style="left:' + (dots[i].cx.baseVal.value + 14) + 'px; top:' + (dots[i].cy.baseVal.value) + 'px; margin-left:-15px; height:' + lineHeight + 'px;"></div>';
            }

        }
    }
    //------------   / GRAPH
    //----------------------------------------------------


    /** Returns one of users attached to this project*/
    $scope.getUserForThisProjectById = function (id) {
        if (!id || id <= 0) return null;
        for (var i = 0; i < $scope.Project.UsersProjects.length; i++) {
            if ($scope.Project.UsersProjects[i].User.ID == id) return $scope.Project.UsersProjects[i].User;
        }
        return null;
    }

    /** Returns one of users attached to this project*/
    $scope.getUserForThisProjectByUsername = function (name) {
        if (!$scope.Project.UsersProjects) { alert($filter('translate')('there are no users on this project')); $scope.Project.UsersProjects = []; return null; }
        for (var i = 0; i < $scope.Project.UsersProjects.length; i++) {
            if ($scope.Project.UsersProjects[i].User.username == name) return $scope.Project.UsersProjects[i].User;
        }
        return null;
    }

    $scope.toMegabytes = function (bytes) {
        return Math.floor(bytes / 1000000);
    }



    $scope.NewPost = new Post();
    $scope.addPost = function () {
        $scope.Project.posts.push(new Post());
    };


    $scope.uploadPostImage = function (files, from) {
        //  $scope.uploadProgressShow = true;

        var URL = '/Post/UploadImage?postID=' + $scope.NewPost.ID + '&projectID=' + $scope.Project.ID;

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
                            console.log(' resp = ', resp);
                            // $scope.log = 'file: ' + resp.config.data.file.name + ', Response: ' + JSON.stringify(resp.data) + '\n' + $scope.log;
                            ///   console.log('$timeout(function, $scope.log = ', $scope.log);
                            $scope.NewPostImage = resp.data.Data.name;
                            $scope.NewPostImageSize = resp.data.Data.size;
                            console.log('$scope.NewPostImage = ', $scope.NewPostImage);
                            console.log('$scope.NewPost = ', $scope.NewPost);
                            $scope.uploadProgressShow = false;
                        });
                    }, null, function (evt) {
                        var progressPercentage = parseInt(100.0 * evt.loaded / evt.total);
                        $scope.uploadProgressPercentage = progressPercentage;
                        $scope.log = 'progress: ' + progressPercentage +'% ' + evt.config.data.file.name + '\n' + $scope.log;
                        console.log('$scope.uploadProgressPercentage = progressPercentage');
                    });
                }
            }
        }
    };


    $scope.SendPost = function (NewPostText, NewPostImage, NewDefinitionText) {
        if (!NewPostText) return;
    //    $scope.SendNotificationText = changeNotificationText('Sending in progress');
        var theNewPost = new Post();
        theNewPost.ID = -1;
        theNewPost.IDproject = $scope.Project.ID;
        theNewPost.text = NewPostText;
        // theNewPost.to = $scope.NewPost.from;
      //  if ($scope.getUserForThisProjectByUsername($rootScope.rootScope_username) == null) { alert($filter('translate')('there are no users on this project')); return; }

        if (!$scope.Project.UsersProjects || $scope.Project.UsersProjects.length < 1) { if (!confirm($filter('translate')('there are no users on this project, are you sure you want to save this post?'))) return;  }

        theNewPost.from = $rootScope.rootScope_curentUser.ID;
      //  else theNewPost.from = $scope.getUserForThisProjectByUsername($rootScope.rootScope_username).ID;
        theNewPost.definition = NewDefinitionText;
        theNewPost.image = NewPostImage;
        theNewPost.imageSize = $scope.NewPostImageSize;
        theNewPost.time = new Date;

        Project.SendPost($scope.Project.ID, theNewPost, $scope.NewPost,
            function (post) {
                console.log('Project.SendPost  post =', post)
                //  changeNotificationText('emails sent succesfully');
                if (!$scope.Project.Posts) $scope.Project.Posts = [];
                $scope.Project.Posts.push(post);
                theNewPost = new Post();
            },
            function (retval) {
              //  changeNotificationText('emails could not be sentr');
            });
        $scope.NewDefinitionText = "";
        $scope.NewPostText = "";
        $scope.NewPostImage = null;
        $scope.NewPostImageSize = 0;
    };

    //----------------------  text block file
    var minTFID = -1;
    $scope.NewProjectFile = new ProjectFile();
    $scope.NewProjectFile.ID = minTFID;
    $scope.ProjectFiles = [];
  
    $scope.uploadTextBlockFile = function (files, newProjectFile) {
        URL = '/Project/UploadFile?projID=';
        console.log(files);
        if (files && files.length) {
            $scope.uploadProgressShow = true;
            for (var i = 0; i < files.length; i++) {
                var file = files[i];
                file.userCanDelete = true;
                if (!file.$error) {

                    Upload.upload({
                        url: URL + $scope.Project.ID,
                        data: {
                            username: $scope.username,
                            file: file
                        }
                    }).then(function (resp) {
                        console.log('+++++ resp ', resp);

                        $timeout(function () {
                            $scope.log = 'file: ' +  resp.config.data.file.name + ', Response: ' + JSON.stringify(resp.data) +'\n' + $scope.log;
                            $scope.NewProjectFile.file = resp.data.Data.name;
                            $scope.NewProjectFile.size = resp.data.Data.size;
                            //   $scope.ProjectFiles.push($scope.NewProjectFile);
                            //if (!$scope.Project.ProjectFiles) $scope.Project.ProjectFiles = [];
                            //$scope.NewProjectFile.userCanDelete = true;
                            //$scope.Project.ProjectFiles.push($scope.NewProjectFile)
                            console.log('$scope.ProjectFiles = ', $scope.ProjectFiles);
                            $scope.NewProjectFile = new TextBlockFile();
                            minTFID--;
                            $scope.NewProjectFile.ID = minTFID;
                            $scope.uploadProgressShow = false;
                            console.log('SAVING ', newProjectFile);
                            newProjectFile.userCanDelete = true;
                            $scope.SaveProjectFile(newProjectFile);
                        });
                    }, null, function (evt) {
                        var progressPercentage = parseInt(100.0 * evt.loaded / evt.total);
                        $scope.uploadProgressPercentage = progressPercentage;
                        $scope.log = 'progress: ' + progressPercentage + '% ' + evt.config.data.file.name + '\n' + $scope.log;
                        console.log('$scope.uploadProgressPercentage = progressPercentage');
                    });
                }
            }
        }
    };


    $scope.deleteTextBlockFile = function (file) {
        URL = '/Project/DeleteFile'; 
    };


    /** Object must have ID */
    var removeObjectFromArray = function (arrayOfObjects, objectToRemove) {
        for(var i = 0; i < arrayOfObjects.length; i++) {
            if (objectToRemove.ID == arrayOfObjects[i].ID) {
                arrayOfObjects.splice(i, 1);
                return;
                //   console.log(' removeFromArray found on ',i);
            }
        }
    }

    //--- removes file from server
    $scope.removeFile = function (file) {
        console.log(' $scope.removeFile file = ', file);
              
        //------------  delete on server
       Ajax.sendDataPOST("/Project/DeleteProjectFile",
          file,
           function (response) {
               console.log("  Project.sendDataPOST response =", response);
               removeObjectFromArray($scope.Project.ProjectFiles, file);
               console.log('$scope.ProjectFiles = ', $scope.ProjectFiles);
           },
           function (response) {
               console.error("  Project.sendDataPOST ERROR ", response);
           }
       );
    };


    $scope.GetProjectImageAddress = function (image) {
        return Project.GetImageAddress(image);
    }

    $scope.GetuserImageAddress = function (image) {
        return User.GetImageAddress(image);
    }

    $scope.GetPostImageAddress = function (image) {
        return Post.GetImageAddress(image);
    }


    $scope.downloadAllForTextBlock = function (textblock) {
        Ajax.Download(textblock.ID, '/Project/ZipAllForTextBlock?textblockID=' + textblock.ID);
    }
    

    $scope.downloadPostFile = function (file) {
        //   Ajax.Download("UPLOADED_FILES/projects/" + file);
        Ajax.Download(file);
    }

    $scope.downloadFile = function (file) {
        console.log('++++++++++ downloadFile ', $scope.Project.ID);
        Ajax.Download("UPLOADED_FILES/projects/" + file, "/File/Download/" + $scope.Project.ID);
       // else Ajax.Download("UPLOADED_FILES/projects/" + file);
       
    }

    $scope.sendFileUploadedEmail = function (/*file*/) {
        console.log('----***** -- $scope.sendFileUploadedEmail ');
      //  file.IDproject = $scope.Project.ID;
      //  file.userCanDelete = false;

        for (var i = 0; i < $scope.Project.ProjectFiles.length; i++) {
            $scope.Project.ProjectFiles[i].userCanDelete = false;
            $scope.Project.ProjectFiles[i].IDproject = $scope.Project.ID;

            ProjectFile.save($scope.Project.ProjectFiles[i],
                function (response) {
                    console.log(i+" $scope.SaveProjectFile", response);
                    //  $scope.sendFileUploadedEmail(file.file);
                    //  $scope.Project.ProjectFiles[i] = response;
                },
                function (response) {

             });
        }

    //    console.log("UPLOADED_FILES/documents/projects/", file);

        var time = Date_WriteDateTime($scope.Project.timeCompleted);
        console.log("time", time);
        TextBlock.sendFileUploadedEmail(time,
            function (response) {

            },
            function (error) {

            });
    }


    $scope.SaveProjectFile = function(file){
        file.IDproject = $scope.Project.ID;
        file.userCanDelete = true;
        ProjectFile.save(file, 
            function(response){ 
                file = response;
                
                //  $scope.sendFileUploadedEmail(file.file);
                if (!$scope.Project.ProjectFiles) $scope.Project.ProjectFiles = [];
                response.userCanDelete = true;
                $scope.Project.ProjectFiles.push(response);
                console.log(" $scope.SaveProjectFile", $scope.Project.ProjectFiles);
            }, 
            function(response){ 
                console.err(" $scope.SaveProjectFile", response);
            });
    
    }

    $scope.Checklist = [];

    var GetChecklistForProject = function (projectID) {
        // console.log('GetChecklistForProject ', projectID);
        Checklist.GetForProject(projectID,
            function (response) {
                 console.log('GetChecklistForProject ', response);
                 $scope.Checklist = response;
                // for (var i = 0; i < $scope.Checklist.length; i++)  $scope.Checklist[i].checked = false;  
            },
            function (error) {
                console.error('GetChecklistForProject ', error);
            });
    };


   
    $scope.ChecklistChecked = [];

    $scope.CheckedChanged = function (index, elem) {

        for (var i = 0; i < $scope.Checklist.length; i++) {
            if ($scope.Checklist[i].ID == elem.e.ID) {
                $scope.Checklist[i].Checked = $scope.ChecklistChecked[index];

                if ($scope.ChecklistChecked[index]) {

                    ChecklistChecked.save($scope.Checklist[i], true, function () { }, function () { });

                    console.log(' SNIMANJE U TABELU CheckedChecklists DODJE OVDE ');
                }
                else {

                    ChecklistChecked.save($scope.Checklist[i], false, function () { }, function () { });

                }
            }
        }

        console.log(' $scope.Checklist ', $scope.Checklist);
    };

 
};