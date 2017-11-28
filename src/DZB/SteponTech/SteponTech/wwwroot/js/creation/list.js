$(function () {
    $.ajaxSetup({ cache: false });
    artslist($("#listspanid").attr('tag'));
    function showScroll() {
        $(window).scroll(function () {
            var scrollValue = $(window).scrollTop();
            scrollValue > 500 ? $(".toTop").fadeIn(300) : $(".toTop").fadeOut(200);
        });
        $(".toTop").click(function () {
            $("body").animate({ scrollTop: 0 }, 200);
        });
    }
})
function artslist(id) {
    console.log(id);
    var parameter = {
        Index: 0,
        Size: 5,
        Name: '"InformationAll"',
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
            console.log(data);
            $.each(data.data, function (i, item) {
                aaaa += "<div class='showroom-one row'>" +
                    "<div class='showroom-img col-lg-3 col-md-4 col-sm-12 col-xs-12'>" +
                    "<a target='_blank' href='Intro?id=" + item.Id + "'>" +
                    "<img src='/upload/Information/" + item.Id + "/" + item.Photo + "' class='img-responsive' />" +
                    "</a>" +
                    "</div>" +
                    "<div class='showroom-info col-lg-9 col-md-8 col-sm-12 col-xs-12'>" +
                    "<a target='_blank' href='Intro?id=" + item.Id + "'>" +
                    "<h3>" + item.Title + "</h3>" +
                    "<p class='showroom-time'>更新时间: " + new Date(item.CreationDate).Format("yyyy年MM月dd日") + "</p>" +
                    "<p class='showroom-p'>" +
                    item.Intro +
                    "</p>" +
                    "</a>" +
                    "<a target='_blank' class='view-href' href='Intro?id=" + item.Id + "'>" +
                    "<div class='showroom-btn'>查看更多</div>" +
                    "</a>" +
                    "</div>" +
                    "</div>"
            });
            $("#NewsDivDocker").html(aaaa);
            $('#pageToolbar').Paging({
                pagesize: 5, count: data.total, toolbar: true, callback: function (page, size, count) {
                    var parameter = {
                        Index: page - 1,
                        Size: 5,
                        Name: '"InformationAll"',
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
                                    "<div class='showroom-img col-lg-3 col-md-4 col-sm-12 col-xs-12'>" +
                                    "<a target='_blank' href='Intro?id=" + item.Id + "'>" +
                                    "<img src='/upload/Information/" + item.Id + "/" + item.Photo + "' class='img-responsive' />" +
                                    "</a>" +
                                    "</div>" +
                                    "<div class='showroom-info col-lg-9 col-md-8 col-sm-12 col-xs-12'>" +
                                    "<a target='_blank' href='Intro?id=" + item.Id + "'>" +
                                    "<h3>" + item.Title + "</h3>" +
                                    "<p class='showroom-time'>更新时间: " + new Date(item.CreationDate).Format("yyyy年MM月dd日") + "</p>" +
                                    "<p class='showroom-p'>" +
                                    item.Intro +
                                    "</p>" +
                                    "</a>" +
                                    "</div>" +
                                    "<a target='_blank' class='view-href' href='Intro?id=" + item.Id + "'>" +
                                    "<div class='showroom-btn'>查看更多</div>" +
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

Date.prototype.Format = function (fmt) { //author: meizz 
    var o = {
        "M+": this.getMonth() + 1, //月份 
        "d+": this.getDate(), //日 
        "h+": this.getHours(), //小时 
        "m+": this.getMinutes(), //分 
        "s+": this.getSeconds(), //秒 
        "q+": Math.floor((this.getMonth() + 3) / 3), //季度 
        "S": this.getMilliseconds() //毫秒 
    };
    if (/(y+)/.test(fmt)) fmt = fmt.replace(RegExp.$1, (this.getFullYear() + "").substr(4 - RegExp.$1.length));
    for (var k in o)
        if (new RegExp("(" + k + ")").test(fmt)) fmt = fmt.replace(RegExp.$1, (RegExp.$1.length == 1) ? (o[k]) : (("00" + o[k]).substr(("" + o[k]).length)));
    return fmt;
}
