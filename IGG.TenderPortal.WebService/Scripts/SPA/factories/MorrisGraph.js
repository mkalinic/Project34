/** factory for getting json data from server
    client implementation of "ajax simple protocol"
*/
app
.factory('MorrisGraph', ['$http', function ($http) {

    var rootDir = window.location.origin;
    var MorrisGraph = {};

    //----------------------------------------------------   GRAPH
    MorrisGraph.setupGraph = function () {

        console.log('::::: setupGraph ');

        var PointOnLine = function (date, color, text) {
            this.Date = date;
            this.Color = color;
            this.Text = text;
        }

        var allPointsOnline = [];
        /*
          allPointsOnline.push(new PointOnLine(new Date(01,01,2016).getTime(), 'red', 'neki text'));
          allPointsOnline.push(new PointOnLine(new Date(02,01,2016).getTime(), 'red', 'neki text'));
          allPointsOnline.push(new PointOnLine(new Date(03,01,2016).getTime(), 'red', 'neki text'));
          allPointsOnline.push(new PointOnLine(new Date(03,01,2016).getTime(), 'red', 'neki text'));
          allPointsOnline.push(new PointOnLine(new Date(05,01,2016).getTime(), 'red', 'neki text'));
          allPointsOnline.push(new PointOnLine(new Date(06,01,2016).getTime(), 'red', 'neki text'));
        */

        allPointsOnline.push(new PointOnLine("2016-07-24", 'red', 'neki text 1'));
        allPointsOnline.push(new PointOnLine("2016-08-25", 'green', 'neki text2'));
        //  allPointsOnline.push(new PointOnLine("2016-09-26", 'red', 'neki text3'));
        allPointsOnline.push(new PointOnLine(new Date().getTime(), 'NEVERMIND', 'NOW_LINE'));
        allPointsOnline.push(new PointOnLine("2016-10-27", 'blue', 'neki text 4'));
        allPointsOnline.push(new PointOnLine("2016-11-28", 'yellow', 'neki text 5'));

        allPointsOnline.push(new PointOnLine("2016-12-01", 'red', 'neki text 6'));
        allPointsOnline.push(new PointOnLine("2017-01-02", 'red', 'neki text 7'));
        allPointsOnline.push(new PointOnLine("2017-02-03", 'red', 'neki text 8'));
        //  allPointsOnline.push(new PointOnLine("2012-03-04", 'red', 'neki text'));
        allPointsOnline.push(new PointOnLine("2017-03-05", 'gray', 'neki text 9'));
        allPointsOnline.push(new PointOnLine("2017-04-06", 'red', 'neki text 10'));


        var week_data = [];

        for (i = 0; i < allPointsOnline.length; i++) week_data.push({ "period": allPointsOnline[i].Date, "licensed": 0 });

        // console.log('allPointsOnline = ', allPointsOnline);
        // console.log('week_data = ', week_data);

        Morris.Line({
            element: 'graph',
            data: week_data,
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

        var dots = document.getElementsByTagName("circle");
        for (i = 0; i < allPointsOnline.length; i++) {
            dots[i].style.fill = allPointsOnline[i].Color;
            dots[i].style.stroke = allPointsOnline[i].Color;
            dots[i].style.cy = 153; // polozaj kuglica u okviru grtafa

            var lineHeight = Math.round(Math.random() * 60) + 30; // visina linija 
            console.log(dots[i].cx.baseVal.value);

            if (allPointsOnline[i].Text == "NOW_LINE") {
                dots[i].style.fill = 'transparent';
                dots[i].style.stroke = 'transparent';
                document.getElementById('graph').innerHTML += '<div class="linijaDanas" style=" left:' + (dots[i].cx.baseVal.value + 14) + 'px; top:' + (dots[i].cy.baseVal.value - 150) + 'px; "></div>';
            }
            else
                if (i % 2 == 0) {
                    document.getElementById('graph').innerHTML += '<div class="baloncic" style="left:' + (dots[i].cx.baseVal.value) + 'px; top:' + (dots[i].cy.baseVal.value - lineHeight - 43) + 'px; margin-left:-15px;">' + allPointsOnline[i].Text + '</div>';
                    document.getElementById('graph').innerHTML += '<div class="linija" style="left:' + (dots[i].cx.baseVal.value + 14) + 'px; top:' + (dots[i].cy.baseVal.value - lineHeight - 14) + 'px; margin-left:-15px; height:' + lineHeight + 'px;""></div>';
                }
                else {
                    document.getElementById('graph').innerHTML += '<div class="baloncic" style="left:' + (dots[i].cx.baseVal.value) + 'px; top:' + (dots[i].cy.baseVal.value + lineHeight - 9) + 'px; margin-left:-15px;">' + allPointsOnline[i].Text + '</div>';
                    document.getElementById('graph').innerHTML += '<div class="linija" style="left:' + (dots[i].cx.baseVal.value + 14) + 'px; top:' + (dots[i].cy.baseVal.value) + 'px; margin-left:-15px; height:' + lineHeight + 'px;"></div>';
                }

        }
    }
    //----------------------------------------------------   / GRAPH

    return MorrisGraph;

}]);