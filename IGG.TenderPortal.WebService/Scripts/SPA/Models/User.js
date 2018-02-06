app.factory('User', function (Ajax, ErrorHandler, $rootScope) {

    /**
    @class: Definition of Item
    */
    var User = function () {
        //--- all fields will be created from server response
    };
 

    User.GetAll = function (functionOnFinished, functionOnError) {
        Ajax.getData("/User/GetAll",
               function (response) {
                   console.log('User/GetUserById response = ', response);
                   var user = createFromResponseArray(response);
                   functionOnFinished(user);
               },
               function (response) {
                   functionOnError(response);
               }
            );
    }



    /** fills  Item.allItems array
    @param: pagenum {int} curent page number
    @param: countPerPage {int} numbetr of Items per page
    @param: functionOnFinished {Function} functionOnFinished that takes NumberOfAllPages as parameter
    */
    User.GetByIdWithPass = function (id, functionOnFinished, functionOnError) {
        Ajax.getData("/User/GetByIdWithPass?id=" + id,
           function (response) {
               console.log('User/GetUserById response = ', response);
               var user = createFromResponse(response);
               functionOnFinished(user);
           },
           function (response) {
               functionOnError(response);
           }
        );

    };

    /** fills  Item.allItems array
   @param: pagenum {int} curent page number
   @param: countPerPage {int} numbetr of Items per page
   @param: functionOnFinished {Function} functionOnFinished that takes item as parameter
   */
   /* User.getById = function (id, OKf, ErrF) {

        Ajax.getData("/User/GetById/?id=" + id,
           function (response) {
               console.log('"/User/GetById?id = ', id);
               var item = new Item(response.Id, response.Name, response.Value);
               OKf(item);
           },
           function (response) {
               console.error(" Item.getById ", response);
               ErrF(response);
           }
        );

    };
    */

    /** fills  Item.allItems array
   @param: pagenum {int} curent page number
   @param: countPerPage {int} numbetr of Items per page
   @param: functionOnFinished {Function} functionOnFinished that takes item as parameter
   */
    User.GetMyAccount = function (OKf, ErrF) {

        Ajax.getData("/User/GetMyAccount/" ,
           function (response) {
               console.log('/User/GetMyAccount/ ');
               var user = createFromResponse(response);
               OKf(user);
           },
           function (response) {
               console.error(" Item.getById ", response);
               ErrF(response);
           }
        );

    };
 

    User.delete = function (userID, OKf, ErrF) {

        Ajax.getData("/User/Delete?UserID=" + userID,
           function (response) {
               console.log("  User.delete ", response);
               OKf(response);
           },
           function (response) {
               console.error("  User.delete ERRRR ", response);
               //ErrorHandler.ShowError('Bad request or connection failed');
               ErrF(response);
           }
       );
    };


    User.save = function (user, OKf, ErrF) {
        Ajax.sendDataPOST("/User/Save",
                user,
               function (response) {
                   console.log(" Item.SaveJson ", response);
                   OKf(createFromResponse(response));
               },
               function (response) {
                   ErrF(response);
               }
            );
    };



    User.SaveMyAccount = function (user, OKf, ErrF) {
        Ajax.sendDataPOST("/User/SaveMyAccount",
                user,
               function (response) {
                   console.log(" User.SaveMyAccount ", response);
                   OKf(createFromResponse(response));
               },
               function (response) {
                   console.error(" User.SaveMyAccount ", response);
                   //ErrorHandler.ShowError('Bad request or connection failed');
                   ErrF(response);
               }
            );
    };

    User.SaveAll = function (usersArray, OKf, ErrF) {
        Ajax.sendDataPOST("/User/SaveAll",
                usersArray,
               function (response) {
                   console.log(" User.SaveJson ", response);
                   OKf(response);
               },
               function (response) {
                   console.error(" Item.SaveJson ", response);
                   //ErrorHandler.ShowError('Bad request or connection failed');
                   ErrF(response);
               }
            );
    };

    //---response object and this one must be the same, changing only necessary fields
    var createFromResponse = function (response) {
        var user = response;
        if(response)
            if (response.joined) user.joined = Date_ParseFromServer(response.joined);
        return user;

    };

    var createFromResponseArray = function (response) {
        for (var i = 0; i < response.length; i++) {
            response[i].joined = Date_ParseFromServer(response[i].joined)
        }
        return response;
    };


    User.SaveAll = function (usersArray, OKf, ErrF) {
        Ajax.sendDataPOST("/User/SaveAll",
                usersArray,
               function (response) {
                   console.log(" User.SaveJson ", response);
                   OKf(response);
               },
               function (response) {
                   console.error(" Item.SaveJson ", response);
                   //ErrorHandler.ShowError('Bad request or connection failed');
                   ErrF(response);
               }
            );
    };

    User.SendLinkForForgottenPassword = function (usernameOrEmail, OKf, ErrF) {
        Ajax.getData("/User/ForgottenPassword?usernameOrEmail=" + usernameOrEmail + "&language=" + $rootScope.rootScope_language,
           function (response) {
               console.log('/User/GetMyAccount/ ');
               var user = createFromResponse(response);
               OKf(user);
           },
           function (response) {
               console.error(" Item.getById ", response);
               ErrF(response);
           }
        );
    };


    User.GetUserTypes = function () {
        var types = ["CANDIDATE", "CLIENT", "CONSULTANT", "TENDER-TEAM", "IGG"];
        return types;
    }


    User.ImageFolder = "./../../UPLOADED_IMAGES/users/";

    User.GetImageAddress = function (image) {
        if (image == null || image == '') return "./../../UPLOADED_IMAGES/no_photo.png";
        return User.ImageFolder + image;
    }



    User.SendCredentials = function (user, OKf, ErrF) {
        Ajax.sendDataPOST("/User/SendCredentials",
         { user: user, language: $rootScope.rootScope_language },
        function (response) {
            console.log(" User/SendCredentials ", response);
            OKf(createFromResponse(response));
        },
        function (response) {
            console.error(" Item.SaveJson ", response);
            //ErrorHandler.ShowError('Bad request or connection failed');
            ErrF(response);
        }
     );


    }


    User.Get = function (page, pagesize, OKf, ErrF) {
        Ajax.getData("/User/Get?page=" + page+"&pagesize="+pagesize,
           function (response) {
               console.log('/User/GetMyAccount/ ');
               OKf(createFromResponseArray(response));
           },
           function (response) {
               console.error(" Item.getById ", response);
               ErrF(response);
           }
        );
    }

    User.GetSorted = function (page, pagesize, sortcolumn, desc,OKf, ErrF) {
        Ajax.getData("/User/GetSorted?page=" + page + "&pagesize=" + pagesize + "&column=" + sortcolumn + "&desc=" + desc,
           function (response) {
               console.log('/User/GetMyAccount/ ');
               OKf(createFromResponseArray(response));
           },
           function (response) {
               console.error(" Item.getById ", response);
               ErrF(response);
           }
        );
    }

    User.GetCount = function (OKf, ErrF) {
        Ajax.getData("/User/GetCount/",
           function (response) {
               console.log('/User/GetCount/ ');
               OKf(response);
           },
           function (response) {
               console.error(" /User/GetCount/  ", response);
               ErrF(response);
           }
        );
    }

    User.Search = function (keyword, OKf, ErrF) {
        Ajax.getData("/User/Search/?keyword=" + keyword,
           function (response) {
               console.log('/User/Search/ ');
               OKf(createFromResponseArray(response));
           },
           function (response) {
               console.error(" /User/Search/  ", response);
               ErrF(response);
           }
        );
    }

    return User;
});