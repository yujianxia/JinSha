$(function() {
  //回到顶部
  showScroll();
  kuaixun();
  gonggao();

});
function showScroll() {
  $(window).scroll(function() {
    var scrollValue = $(window).scrollTop();
    scrollValue > 500 ? $(".toTop").fadeIn(300) : $(".toTop").fadeOut(200);
  });
  $(".toTop").click(function() {
    $("body").animate({ scrollTop: 0 }, 200);
  });
}
function kuaixun() {
    var parameter = {
        Index: 0,
        Size: 5,
        Name: '"InformationEnglishAll"',
       OrderBy: '"IsTop" desc',
        Condition: {
            Collection: [
                { F: '"ColumName"', O: "=", P: "@ColumName", V: "News" }
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
                    "<a target='_blank' href='News?id=" + item.Id + "'>" +
                    "<img src='/upload/Information/" + item.Id + "/" + item.Photo + "' class='img-responsive ' />" +
                    "</a>" +
                    "</div>" +
                    "<div class='showroom-info col-lg-8 col-md-8 col-sm-12 col-xs-12'>" +
                    "<a target='_blank' href='News?id=" + item.Id + "'>" +
                    "<h3>" + item.Title + "</h3>" +
                    "<p class='showroom-time'>Updated time: " + getCurrentDate(new Date(item.CreationDate)) + "</p>" +
                    "<p class='showroom-p'>" +
                    item.Intro +
                    "</p>" +
                    "</a>" +
                    "<div class='showroom-btn'>" +
                    "<a target='_blank' href='News?id=" + item.Id + "'>Learn more</a>" +
                    "</div>" +
                    "</div>" +
                    "</div>"
            });
           
            $("#NewsDivDocker").html(aaaa);
            /*$('.ccccc').each(function () {
                if (!this.complete || typeof this.naturalWidth == "undefined" || this.naturalWidth == 0) {
                    this.src = '/img/ErrorImg/ErrorImg.png';
                }
            });*/
            textLen();
            $('#pageToolbar').Paging({
                pagesize: 5, count: data.total, toolbar: true, callback: function (page, size, count) {
                     $("html, body").animate({scrollTop: 0}, 0);
                    var parameter = {
                        Index: page - 1,
                        Size: 5,
                        Name: '"InformationEnglishAll"',
                       OrderBy: '"IsTop" desc',
                        Condition: {
                            Collection: [
                                { F: '"ColumName"', O: "=", P: "@ColumName", V: "News" }
                            ]
                        }
                    };
                    $.ajax({
                        url: '/api/DataSearch',
                        type: 'Post',
                        dataType: "text",
                        contentType: "application/json",
                        data: JSON.stringify(parameter),
                        //window.location.href = '/NEWS/Index#showroom'
                        success: function (data) {
                            var aaaa = "";
                            var data = JSON.parse(data);
                            $.each(data.data, function (i, item) {
                                aaaa += "<div class='showroom-one row'>" +
                                    "<div class='showroom-img col-lg-4 col-md-4 col-sm-12 col-xs-12'>" +
                                    "<a target='_blank' href='News?id=" + item.Id + "'>" +
                                    "<img src='/upload/Information/" + item.Id + "/" + item.Photo + "' class='img-responsive ' />" +
                                    "</a>" +
                                    "</div>" +
                                    "<div class='showroom-info col-lg-8 col-md-8 col-sm-12 col-xs-12'>" +
                                    "<a target='_blank' href='News?id=" + item.Id + "'>" +
                                    "<h3>" + item.Title + "</h3>" +
                                    "<p class='showroom-time'>Updated time: " + getCurrentDate(new Date(item.CreationDate)) + "</p>" +
                                    "<p class='showroom-p'>" +
                                    item.Intro +
                                    "</p>" +
                                    "</a>" +
                                    "<div class='showroom-btn'>" +
                                    "<a target='_blank' href='News?id=" + item.Id + "'>Learn more</a>" +
                                    "</div>" +
                                    "</div>" +
                                    "</div>"
                            });
                            $("#NewsDivDocker").html(aaaa);

                            /*$('.ccccc').each(function () {
                                if (!this.complete || typeof this.naturalWidth == "undefined" || this.naturalWidth == 0) {
                                    this.src = '/img/ErrorImg/ErrorImg.png';
                                }
                            });*/
                            textLen();
                        }
                    });
                }
            });
        }
    });
}

function gonggao() {
    var parameter = {
        Index: 0,
        Size: 10,
        Name: '"InformationEnglishAll"',
       OrderBy: '"IsTop" desc',
        Condition: {
            Collection: [
                { F: '"ColumName"', O: "=", P: "@ColumName", V: "Announcements" }
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
                aaaa += "<div class='notice row'>" +
                    "<a target='_blank' class='notice-cell' href='Notice?id=" + item.Id + "'>" +
                    "<p class='col-sm-8 col-xs-8'><span class='notice-span'>" + item.Title + "</span></p>" +
                    "<div class='col-sm-4 col-xs-4'><span class='time-span'>" + getCurrentDate(new Date(item.CreationDate)) + "</span></div>" +
                    "</a>" +
                    "</div>"
            });
            $("#AnnouncementsDivDocker").html(aaaa);
            $('#pageToolbar2').Paging({
                pagesize: 10, count: data.total, toolbar: true, callback: function (page, size, count) {
                    var parameter = {
                        Index: page - 1,
                        Size: 10,
                        Name: '"InformationEnglishAll"',
                       OrderBy: '"IsTop" desc',
                        Condition: {
                            Collection: [
                                { F: '"ColumName"', O: "=", P: "@ColumName", V: "Announcements" }
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
                                aaaa += "<div class='notice row'>" +
                                    "<a target='_blank' class='notice-cell' href='Notice?id=" + item.Id + "'>" +
                                    "<p class='col-sm-8 col-xs-8'><span class='notice-span'>" + item.Title + "</span></p>" +
                                    "<div class='col-sm-4 col-xs-4'><span class='time-span'>" + getCurrentDate(new Date(item.CreationDate)) + "</span></div>" +
                                    "</a>" +
                                    "</div>"
                            });
                            $("#AnnouncementsDivDocker").html(aaaa);
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
function textLen() {
    //限制个数
    var limited =200;
    //处理后的字符串
    var afterText = "";
    var text = $(".showroom-p")
    text.map(function (index, value) {
        var _text = $.trim($(value).text());
        if (_text.length >= limited) {
            afterText = _text.slice(0, limited);
            afterText += "...";
        } else {
            afterText = _text;
        }
        $(value).text(afterText);
    })
}
