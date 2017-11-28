$(function () {
  artslist($("#listspanid").attr('tag'));
});
function artslist(id) {
    var parameter = {
        Index: 0,
        Size: 5,
        Name: '"InformationEnglishAll"',
       OrderBy: '"IsTop" desc',
        Condition: {
            Collection: [
                { F: '"ColumnId"', O: "=", P: "@ColumnId::uuid", V: id }
            ]
        }
    };
    $.ajax({
        url: '/api/DataSearch',
        type: 'Post',
        dataType: "text",
        contentType: "application/json",
        data: JSON.stringify(parameter),
        success: function (data) {
            var aaaa = "";
            var data = JSON.parse(data);
            //console.log(data);
            $.each(data.data, function (i, item) {
                aaaa += "<div class='showroom-one row'>" +
                    "<div class='showroom-img col-lg-4 col-md-4 col-sm-12 col-xs-12'>" +
                    "<a target='_blank' href='Intro?id=" + item.Id + "'>" +
                    "<img src='/upload/Information/" + item.Id + "/" + item.Photo + "' class='img-responsive' />" +
                    "</a>" +
                    "</div>" +
                    "<div class='showroom-info col-lg-8 col-md-8 col-sm-12 col-xs-12'>" +
                    "<a target='_blank' href='Intro?id=" + item.Id + "'>" +
                    "<h3>" + item.Title + "</h3>" +
                    "<p class='showroom-time'>Updated time: " + getCurrentDate(new Date(item.CreationDate)) + "</p>" +
                    "<p class='showroom-p'>" +
                    item.Intro +
                    "</p>" +
                    "</a>" +
                    "</div>" +
                    "<a target='_blank' class='view-href' href='Intro?id=" + item.Id + "'>" +
                    "<div class='showroom-btn'>Learn more</div>" +
                    "</a>" +
                    "</div>"
            });
            $("#NewsDivDocker").html(aaaa);
            $('#pageToolbar').Paging({
                pagesize: 5, count: data.total, toolbar: true, callback: function (page, size, count) {
                    var parameter = {
                        Index: page - 1,
                        Size: 5,
                        Name: '"InformationEnglishAll"',
                       OrderBy: '"IsTop" desc',
                        Condition: {
                            Collection: [
                                { F: '"ColumnId"', O: "=", P: "@ColumnId::uuid", V: id }
                            ]
                        }
                    };
                    $.ajax({
                        url: '/api/DataSearch',
                        type: 'Post',
                        dataType: "text",
                        contentType: "application/json",
                        data: JSON.stringify(parameter),
                        success: function (data) {
                            var aaaa = "";
                            var data = JSON.parse(data);
                            $.each(data.data, function (i, item) {
                                aaaa += "<div class='showroom-one row'>" +
                                    "<div class='showroom-img col-lg-4 col-md-4 col-sm-12 col-xs-12'>" +
                                    "<a target='_blank' href='Intro?id=" + item.Id + "'>" +
                                    "<img src='/upload/Information/" + item.Id + "/" + item.Photo + "' class='img-responsive' />" +
                                    "</a>" +
                                    "</div>" +
                                    "<div class='showroom-info col-lg-8 col-md-8 col-sm-12 col-xs-12'>" +
                                    "<a target='_blank' href='Intro?id=" + item.Id + "'>" +
                                    "<h3>" + item.Title + "</h3>" +
                                    "<p class='showroom-time'>Updated time: " + getCurrentDate(new Date(item.CreationDate)) + "</p>" +
                                    "<p class='showroom-p'>" +
                                    item.Intro +
                                    "</p>" +
                                    "</a>" +
                                    "</div>" +
                                    "<a target='_blank' class='view-href' href='Intro?id=" + item.Id + "'>" +
                                    "<div class='showroom-btn'>Learn more</div>" +
                                    "</a>" +
                                    "</div>"
                            });
                            $("#NewsDivDocker").html(aaaa);
                        }
                    });
                }
            });
        }
    });
}

function getCurrentDate(datetime) {
    var date = datetime;
    var monthArray = new Array
        ("January", "February", "March", "April", "May", "June", "July", "August",
        "September", "October", "November", "December");
    var weekArray = new Array("Sunday", "Monday", "Tuesday",
        "Wednesday", "Thursday", "Friday", "Saturday");
    month = date.getMonth();
    day = date.getDate();
    if (day.toString().length == 1) {
        day = "0" + day.toString();
    }
    return monthArray[month] + " " + day + ", " + date.getFullYear();
}


