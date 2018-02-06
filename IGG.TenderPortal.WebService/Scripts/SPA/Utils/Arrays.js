 
Arrays_RemoveObject = function (objec, fromArray) {
    for (var i = 0; i < fromArray.length; i++) { 
        if (fromArray[i].ID == objec.ID) arrayOfObjects.splice(i, 1);
    }
}

