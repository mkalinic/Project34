app.factory('Project', function (Ajax, ErrorHandler, TextBlock, Milestone, Phase, Post, UsersProject, Notification, ProjectFile, $rootScope) {

    /**
    @class: Definition of Project
    */
    var Project = function () {
        this.ID = -1;
        this.name = null;
        this.description = null;
        this.place = null;
        this.clientCreated = null;
        this.IGGperson = null;
        this.canUpload = 1;
        this.status = "OPEN";
        this.submisionDate = null;
        this.photo = null;
        this.onFrontPage = false;
        this.timeCompleted = null;
        this.clientName = null;
        this.photoThumbnail = null;
        this.timeOpenVault = null;
        this.select
        this.TextBlocks = [];
        this.Posts = [];
        this.Milestones = [];
        this.ProjectFiles = [];
    };

    Project.NewSceleton = null;
    /** all Projects */
    Project.allProjects = Array();

    Project.createNew = function (OKf, ErrF) {
        /*if (Project.NewSceleton == null) {
            Ajax.getData("/Project/GetEmptyProject",
                    function (response) {
                        console.log('/Project/GetEmptyProject/ response = ', response);
                        Project.NewSceleton = JSON.parse(JSON.stringify(response));
                        OKf(createFromResponse(Project.NewSceleton));
                    },
                    function (response) {
                        ErrF(response);
                    }
                    );
        }
        else {
            console.log('  Project.NewSceleton = ', Project.NewSceleton);
            OKf(new Project());
        }*/
        OKf(new Project());
    }

    Project.GetAll = function (functionOnFinished, functionOnError) {
        Ajax.getData("/Project/GetAll",
           function (response) {
               console.log('/Project/GetAll/ response = ', response);
               //Project.allProjects = response;
               var projects = [];
               for (var i = 0; i < response.length; i++) {
                   var p = createFromResponse(response[i]);
                   projects.push(p);
               }

               functionOnFinished(projects);
           },
           function (response) {
               functionOnError(response);
           }
        );

    };

    /** fills  Item.allItems array
    @param: n {int} howmany
    @param: countPerPage {int} numbetr of Items per page
    @param: functionOnFinished {Function} functionOnFinished that takes NumberOfAllPages as parameter
    */
    Project.GetTopNForFrontPage = function (n, functionOnFinished, functionOnError) {

       
        Ajax.getData("/Project/GetTopNForFrontPage?n=" + n,
           function (response) {
               console.log('/Project/GetTopNForFrontPage/ response = ', response);
               //Project.allProjects = response;
               var projects = [];
               for (var i = 0; i < response.length; i++) {
                   var p = createFromResponse(response[i]);
                   projects.push(p);
               }

               functionOnFinished(projects);
           },
           function (response) {
               functionOnError(response);
           }
        );

    };


    Project.GetTopNForUser = function (n, userID, OKf, ErrF) {

        Ajax.getData("/Project/GetTopNForUser?n=" + n + "&userID=" + userID,
        function (response) {
            console.log('/Project/GetTopNForUser/ response = ', response);
            //Project.allProjects = response;
            var projects = [];
            for (var i = 0; i < response.length; i++) {
                var p = createFromResponse(response[i]);
                projects.push(p);
            }

            OKf(projects);
        },
        function (response) {
            ErrF(response);
        }
     );

    };

 
    Project.GetTopN = function (n, functionOnFinished, functionOnError) {
        Ajax.getData("/Project/GetTopN?n=" + n,
           function (response) {
               console.log('/Project/GetTopNForFrontPage/ response = ', response);
               //Project.allProjects = response;
               var projects = [];
               for (var i = 0; i < response.length; i++) {
                   var p = createFromResponse(response[i]);
                   projects.push(p);
               }

               functionOnFinished(projects);
           },
           function (response) {
               functionOnError(response);
           }
        );

    };

    /** fills  Item.allItems array
@param: pagenum {int} curent page number
@param: countPerPage {int} numbetr of Items per page
@param: functionOnFinished {Function} functionOnFinished that takes NumberOfAllPages as parameter
*/
  /*  Project.GetTopN = function (pagenum, countPerPage, functionOnFinished, functionOnError) {

        if (pagenum == null) pagenum = 0;


        Ajax.getData("/Project/GetTopN?n=5",
           function (response) {
               console.log('/Project/GetTopN/ response = ', response);
               //Project.allProjects = response;
               for (var i = 0; i < response.length; i++) {
                   var p = createFromResponse(response[i]);
                   Project.allProjects.push(p);
               }

               functionOnFinished(response.NumberOfAllPages);
           },
           function (response) {
               functionOnError(response);
           }
        );

    };
    */
    /** fills  Item.allItems array
   @param: pagenum {int} curent page number
   @param: countPerPage {int} numbetr of Items per page
   @param: functionOnFinished {Function} functionOnFinished that takes item as parameter
   */
    Project.getById = function (id, OKf, ErrF) {
        console.log('  Project.getById');
        Ajax.getData("/Project/GetById/?id=" + id,
           function (response) {
               console.log('"/Project/GetById?response = ', response);
               var proj = createFromResponse(response);
               OKf(proj);
           },
           function (response) {
               console.error(" Item.getById ", response);
               ErrF(response);
           }
        );

    };



    Project.save = function (project, OKf, ErrF) {
        console.log("   Project.saves project = ", project);
        Ajax.sendDataPOST("/Project/Save",
           project,
           function (newproject) {
               console.log("  Project.save ", newproject);
               OKf(createFromResponse(newproject));
           },
           function (response) {
               console.error("  Project.save ERROR ", response);
               ErrF(response);
               //ErrorHandler.ShowError('Bad request or connection failed');
           }
       );
    };


    Project.close = function (project, OKf, ErrF) {
        console.log("   Project.saves project = ", project);
        Ajax.sendDataPOST("/Project/Close",
           project,
           function (response) {
               console.log("  Project.close ", response);
               OKf(response);
           },
           function (response) {
               console.error("  Project.close ERROR ", response);
               ErrF(response);
               //ErrorHandler.ShowError('Bad request or connection failed');
           }
       );
    };  


    Project.delete = function (project, OKf, ErrF) {
        console.log("   Project.saves project = ", project);
        Ajax.sendDataPOST("/Project/Delete",
           project,
           function (response) {
               console.log("  Project.delete ", response);
               OKf(response);
           },
           function (response) {
               console.error("  Project.delete ERROR ", response);
               ErrF(response);
               //ErrorHandler.ShowError('Bad request or connection failed');
           }
       );
    };

    /*
    Project.delete = function (project, OKf, ErrF) {

        Ajax.sendDataPOST("/Project/Delete",
            project,
           function (response) {
               console.log(" Item.saveAllItems ", response);
               OKf(Item.ItemID);
           },
           function (response) {
               console.error(" Item.saveAllItems ", response);
               ///ErrorHandler.ShowError('Bad request or connection failed');
               ErrF(Item.ItemID);
           }
       );
    };
    */
    /** searces projects for given keyword */
    Project.search = function (keywords, OKf, ErrF) {
        console.log("  Project.search keywords = ", keywords);
      //  Ajax.sendDataPOST("/Project/Search",
        //       keywords,
        Ajax.getData("/Project/Search?keywords="+keywords,
           function (response) {
               console.log("  Project.search response = ", response);
               var projects = [];
               for (var i = 0; i < response.length; i++) {
                   var project = createFromResponse(response[i]);
                   projects.push(project);
               }
               console.log("  Project.search projects = ", projects);
               OKf(projects);
           },
           function (response) {
               console.error("  Project.search ", response);
               //ErrorHandler.ShowError('Bad request or connection failed');
               ErrF(keywords);
           }
       );
    };

    //---response object and this one must be the same, changing only necessary fields
    var createFromResponse = function (response) {
        if (!response) return null;
        var proj = response;
        proj.submisionDate = Date_ParseFromServer(response.submisionDate);

        proj.timeCompleted = Date_ParseFromServer(response.timeCompleted);
        proj.timeCreated = Date_ParseFromServer(response.timeCreated);
        proj.timeOpenVault = Date_ParseFromServer(response.timeOpenVault);

        if (proj.TextBlocks) proj.TextBlocks = TextBlock.createFromResponseArray(response.TextBlocks);
        if (proj.Phases) proj.Phases = Phase.createFromResponseArray(response.Phases);
        if (proj.Posts) proj.Posts = Post.createFromResponseArray(response.Posts);
        if (proj.Milestones) proj.Milestones = Milestone.createFromResponseArray(response.Milestones);
        if (proj.UsersProjects) proj.UsersProjects = UsersProject.createFromResponseArray(response.UsersProjects);
        if (proj.Notifications) proj.Notifications = Notification.createFromResponseArray(response.Notifications);
        if (proj.ProjectFiles) proj.ProjectFiles = ProjectFile.createFromResponseArray(response.ProjectFiles);
        return proj;

    };

    Project.ImageFolder = window.location.origin + "/UPLOADED_IMAGES/projects/";
     
    Project.GetImageAddress = function (image) {
      //  console.log(' Project.GetImageAddress  image =', image);
        if (image == null || image == '') return window.location.origin + "/UPLOADED_IMAGES/no_photo.png";
      //  console.log(' Project.GetImageAddress returns =', Project.ImageFolder + image);
        return Project.ImageFolder + image;
    }


    Project.SendNoticesToPeopleOnProject = function (projectID, OKf, ErrF) {
        console.log($rootScope.rootScope_language);
        Ajax.getData("/Project/SendNoticesToPeopleOnProject?projectID=" + projectID + "&language=" + $rootScope.rootScope_language.language,
           function (message) {
               console.log("  Project.SendNoticesToPeopleOnProject response = ", message);
               OKf(message);
           },
           function (response) {
               console.error("  Project.SendNoticesToPeopleOnProject ", response);
               ErrF(response);
           }
        );
    }

    Project.SendPostResponse = function (ProjectID, newpost, postrepliedTo, OKf, ErrF) {
      //  console.log("   Project.saves notification = ", notification);
        Ajax.sendDataPOST("/Project/SendPostResponse",
           { newPost: newpost, postRepliedTo: postrepliedTo, ProjectID: ProjectID, language: $rootScope.rootScope_language },
           function (response) {
               console.log("  Project.SendPostResponse response =", response);
               OKf(newpost);
           },
           function (response) {
               console.error("  Project.delete ERROR ", response);
               ErrF(response);
               //ErrorHandler.ShowError('Bad request or connection failed');
           }
       );
    };

    Project.SendPost = function (ProjectID, newpost, postrepliedTo, OKf, ErrF) {
        //  console.log("   Project.saves notification = ", notification);
        Ajax.sendDataPOST("/Project/SendPost",
           { newPost: newpost, postRepliedTo: postrepliedTo, ProjectID: ProjectID, language: $rootScope.rootScope_language },
           function (response) {
               console.log("  Project.SendPostResponse response =", response);
               OKf(newpost);
           },
           function (response) {
               console.error("  Project.delete ERROR ", response);
               ErrF(response);
               //ErrorHandler.ShowError('Bad request or connection failed');
           }
       );
    };

    Project.SendNewUserInProjectMail = function (ProjectID, NewUserProject, OKf, ErrF) {
        console.log("   Project.SendNewUserInProjectMail = ", NewUserProject);
        Ajax.sendDataPOST("/Project/SendNewUserInProjectMail",
           { usersProject: NewUserProject, projectID: ProjectID, language: $rootScope.rootScope_language },
           function (response) {
               console.log("  Project.delete ", response);
               OKf(response);
           },
           function (response) {
               console.error("  Project.delete ERROR ", response);
               ErrF(response);
               //ErrorHandler.ShowError('Bad request or connection failed');
           }
       );


    };

    Project.OpenVault = function (projectID, OKf, ErrF) {

        Ajax.getData("/Project/OpenVault?projectID=" + projectID + "&language=" + $rootScope.rootScope_language,
           function (message) {
               console.log("  Project.SendNoticesToPeopleOnProject response = ", message);
               OKf(message);
           },
           function (response) {
               console.error("  Project.SendNoticesToPeopleOnProject ", response);
               ErrF(response);
           }
        );


    }

    return Project;
});