var Date_ParseFromServer = function (dateFromServer) {

    if (!dateFromServer) return null;

    var milli = dateFromServer.replace(/\/Date\((-?\d+)\)\//, '$1');
    var d = new Date(parseInt(milli));
    return d;
}

var Date_Parse = function (dateFromServer) {
    if (!dateFromServer) return null;
   // console.log('Date_Parse ', dateFromServer);
    var parts1 = dateFromServer.split(' ');
    var parts = parts1[0].split('-');
  //  console.log('parts', parts);
    var partsH = parts1[1].split(':');
 //   console.log('partsH', partsH);
    return new Date(parts[2], parts[1] - 1, parts[0], partsH[0], partsH[1]); 
}

var Date_Parse_FromPicker = function (dateFromServer) {
    if (!dateFromServer) return null;
    console.log('Date_Parse_FromPicker ', dateFromServer);
    var parts1 = dateFromServer.split(' ');
    var parts = parts1[0].split('-');
    //  console.log('parts', parts);
    var partsH = parts1[1].split(':');
    //   console.log('partsH', partsH);
    return new Date(parts[0], parts[1] - 1, parts[2], partsH[0], partsH[1]);
}



var Date_WriteDate = function (date) {
    if (!date) return;
    var yyyy = date.getFullYear().toString();
    var mm = (date.getMonth() + 1).toString();     
    var dd = date.getDate().toString();
    return   (dd[1] ? dd : "0" + dd[0]) + "-"+ (mm[1] ? mm : "0" + mm[0]) + '-' + yyyy;
}

var Date_WriteDateTime = function (date) {
    if (!date) return;
    var yyyy = date.getFullYear().toString();
    var mm = (date.getMonth() + 1).toString();
    var dd = date.getDate().toString();
    var hh = date.getHours().toString();
    var min = date.getMinutes().toString();
    return (dd[1] ? dd : "0" + dd[0]) + "-" + (mm[1] ? mm : "0" + mm[0]) + '-' + yyyy + ' ' + (hh[1] ? hh : "0" + hh[0]) + ":" + (min[1] ? min : "0" + min[0]);
}

