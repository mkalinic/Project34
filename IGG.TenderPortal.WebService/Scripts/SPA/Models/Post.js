app.factory('Post', function (Ajax, ErrorHandler) {

    /**
    @class: Definition of Project
    */
    var Post = function () {
        //--- these fields will be created from server response
        /*     this.ID = -1;
             this.IDproject;
             this.text;
             this.time;
             this.from;
             this.to;*/
    };


    Post.all = Array();

    /** fills  Item.allItems array
   @param: pagenum {int} curent page number
   @param: countPerPage {int} numbetr of Items per page
   @param: functionOnFinished {Function} functionOnFinished that takes item as parameter
   */
    Post.getById = function (id, OKf, ErrF) {

        Ajax.getData("/Post/GetById/?id=" + id,
           function (response) {
               console.log('"/Post/GetById?response = ', response);
               var item = Post.createFromResponse(response);
               OKf(item);
           },
           function (response) {
               console.error(" Post.getById ", response);
               ErrF(response);
           }
        );

    };



    Post.save = function (post, OKf, ErrF) {
        console.log("   Post.saves project = ", post);
        Ajax.sendDataPOST("/Post/Save",
           post,
           function (response) {
               console.log("  Post.save ", response);
               OKf(response);
           },
           function (response) {
               console.error("  Post.save ERROR ", response);
               ErrF(response);
 
           }
       );
    };

    Post.delete = function (post, OKf, ErrF) {
        Ajax.sendDataPOST("/Post/Delete",
            post,
           function (response) {
               console.log(" Post.saveAllItems ", response);
               OKf(Item.ItemID);
           },
           function (response) {
               console.error(" Post.saveAllItems ", response);
             
               ErrF(Item.ItemID);
           }
       );
    };


    Post.GetPostsTo = function (id, top) {
        Ajax.getData("/Post/GetPostsTo?id="+id+"&top="+top,
          function (response) {
              var postArray = [];
              for (var i = 0; i < response.length; i++) {
                  var p = Post.createFromResponse(response[i]);
                  postArray.push(p);
              }
              console.log('GetPostsTo, postArray = ', postArray);
          },
          function (response) {
              console.error(" Post.saveAllItems ", response);
              ErrF(Item.ItemID);
          }
      );
    }

    Post.GetPostsFrom = function (id, top) {
        Ajax.getData("/Post/GetPostsFrom?id=" + id + "&top=" + top,
          function (response) {
              var postArray = [];
              for (var i = 0; i < response.length; i++) {
                  var p = Post.createFromResponse(response[i]);
                  postArray.push(p);
              }
              console.log('GetPostsFrom, postArray = ', postArray);
          },
          function (response) {
              console.error(" Post.saveAllItems ", response);
              ErrF(Item.ItemID);
          }
      );
    }

    //---response object and this one must be the same, changing only necessary fields
    Post.createFromResponse = function (response) {
        var post = response;
        post.time = Date_ParseFromServer(response.time);
        return post;

    };

    /** Creates array of Post from given array of Post-s response from server    */
    Post.createFromResponseArray = function (arrayOfResponses) {
        if (!arrayOfResponses) return null;
        // console.log("Post.createFromResponseArray", arrayOfResponses);
        var retArray = Array();
        for (var i = 0; i < arrayOfResponses.length; i++) {
            retArray.push(Post.createFromResponse(arrayOfResponses[i]));
        }
        return retArray;
    }


    Post.ImageFolder = /*window.location.origin + */"/UPLOADED_FILES/posts/";

    Post.GetImageAddress = function (image) {
        //  console.log(' Project.GetImageAddress  image =', image);
        if (image == null || image == '') return /*window.location.origin +*/ "/UPLOADED_IMAGES/no_photo.png";
        // console.log(' Project.GetImageAddress  Project.ImageFolder =', Project.ImageFolder);
        return "/UPLOADED_FILES/posts/" + image;//Post.ImageFolder + image;
    }


    return Post;
});