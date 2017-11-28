$(function () {
	$.ajaxSetup({ cache: false });
    //绑定个人信息
    $.ajax({
        url: '/api/Members/MembersSelect',
        type: 'GET',
        dataType: "text",
        contentType: "application/json",
        success: function (data) {
            var result = JSON.parse(data);
            var data = JSON.parse(result.data);
            $("#mycard").text(data.body.dataTable.dataRow[0].column[5].value);
            $("#mylevel").text(data.body.dataTable.dataRow[0].column[6].value);
            $("#myscore").text(data.body.dataTable.dataRow[0].column[7].value);
            $("#myscore1").text(data.body.dataTable.dataRow[0].column[7].value);
            $("#myscore2").text(data.body.dataTable.dataRow[0].column[7].value);
            $("#mygroup").text(data.body.dataTable.dataRow[0].column[12].value);
            $("#myname").val(data.body.dataTable.dataRow[0].column[1].value);
            $("#name").val(data.body.dataTable.dataRow[0].column[13].value);
            $("#tel").val(data.body.dataTable.dataRow[0].column[2].value);
            $("#email").val(data.body.dataTable.dataRow[0].column[4].value);
            $("#addr").val(data.body.dataTable.dataRow[0].column[3].value);
            $("#userlevel1").text(data.body.dataTable.dataRow[0].column[6].value + "级");
        }
    });

    //保存按钮事件
    $("#save-btn").on("click", function () {

        //电话
        var _tel = $("#tel").val();
        //邮箱
        var _email = $("#email").val();
        //昵称
        var _name = $("#name").val();
        //地址
        var _address = $("#addr").val();
        var parameter = {
            nickname: _name,
            phone: _tel,
            user_name: $("#myname").val(),
            email: _email,
            address: _address
        };
        $.ajax({
            type: "post",
            url: "/api/Members/UpdateMembers",
            data: JSON.stringify(parameter),
            async: true,
            contentType: "application/json",
            success: function (res) {
		var username = $("#myname").val();
                if (username.length > 4) {

                    $(".top-listLi6").html("<a href='/Vip/Index'>" + username.substring(0, 4) + "...</a>")
                }
                else {
                    $(".top-listLi6").html("<a href='/Vip/Index'>" + username + "</a>")
                }
                alert("修改成功")
            }
        });
    });

    //会员活动
    $.ajax({
        //url: '/api/Members/ActivityInformation/1/100',
        url: '/api/Members/MyActivity',
        type: 'GET',
        dataType: "text",
        contentType: "application/json",
        success: function (data) {
            var result = JSON.parse(data);
            var data = JSON.parse(result.data);
            $.each(data.body.dataTable.dataRow, function (i, item) {
                if (item.column[1].value != "10") {
                    $("#membersactitvty").append("<a href='/Vip/MyActitvtyDetail?id=" + item.column[0].value + "' > " +
                        "<div class='cont-sign-info' id='first-info'>" +
                        "<div class='row'>" +
                        "<div class='cont-sign-img col-lg-4 col-md-6 col-sm-6'>" +
                        "<img class='img-responsive' src='" + item.column[8].value + "' />" +
                        "</div>" +
                        "<div class='cont-sign-text col-lg-8 col-md-6 col-sm-6'>" +
                        "<h3>" + item.column[4].value + "</h3>" +
                        "<p>招募人数：<span>" + item.column[3].value + "</span>&nbsp;/&nbsp;<span>" + item.column[2].value + "</span></p>" +
                        "<p>活动时间：<span>" + datetime(Number(item.column[5].value)) + "~" + datetime(Number(item.column[6].value)) + "</span></p>" +
                        "<p>活动地点：<span>金沙博物馆</span></p>" +
                        "<span class='cont-sign-btnhide'>我已报名</span>" +
                        "<span class='cont-sign-btnshow'>我已报名</span>" +
                        "</div>" +
                        "</div>" +
                        "</div>" +
                        "</a>");
                }
                else {
                    $("#valunteeractitvty").append("<a href='/Vip/MyActitvtyDetail/id=" + item.column[0].value + "' > " +
                        "<div class='cont-sign-info' id='first-info'>" +
                        "<div class='row'>" +
                        "<div class='cont-sign-img col-lg-4 col-md-6 col-sm-6'>" +
                        "<img class='img-responsive' src='" + item.column[8].value + "' />" +
                        "</div>" +
                        "<div class='cont-sign-text col-lg-8 col-md-6 col-sm-6'>" +
                        "<h3>" + item.column[4].value + "</h3>" +
                        "<p>招募人数：<span>" + item.column[3].value + "</span>&nbsp;/&nbsp;<span>" + item.column[2].value + "</span></p>" +
                        "<p>活动时间：<span>" + datetime(Number(item.column[5].value)) + "~" + datetime(Number(item.column[6].value)) + "</span></p>" +
                        "<p>活动地点：<span>金沙博物馆</span></p>" +
                        "<span class='cont-sign-btnhide'>我已报名</span>" +
                        "<span class='cont-sign-btnshow'>我已报名</span>" +
                        "</div>" +
                        "</div>" +
                        "</div>" +
                        "</a>");
                }
            });
        }
    });

    //志愿者活动
    //$.ajax({
    //    url: '/api/Volunteers/VolunteersJoinActivity/1/100',
    //    type: 'GET',
    //    dataType: "text",
    //    contentType: "application/json",
    //    success: function (data) {
    //        var result = JSON.parse(data);
    //        var data = JSON.parse(result.data);
    //        $.each(data.body.dataTable.dataRow, function (i, item) {

    //        });
    //    }
    //});

    //我的积分
    $.ajax({
        url: '/api/Members/IntegralChange/1/100',
        type: 'GET',
        dataType: "text",
        contentType: "application/json",
        success: function (data) {
            var result = JSON.parse(data);
            var data = JSON.parse(result.data);
            $.each(data.body.dataTable.dataRow, function (i, item) {
                $("#contentlistintegral").append("<div class='form-cell clearfix'>" +
                    "<div class='form-cell-box clearfix center-block'>" +
                    "<div class='col-sm-6'>" + fromIntegral(item.column[2].value) + "</div>" +
                    "<div class='col-sm-3 time'><span>" + datetime(new Date(Number(item.column[5].value))) + "</span></div>" +
                    "<div class='col-sm-3'>" +
                    "<p class='point-change'>" + addorremoveIntegral(item.column[2].value) + item.column[3].value + "</p>" +
                    "</div>" +
                    "</div>" +
                    "</div> ");
            });
        }
    });

    //积分商城
    $.ajax({
        url: '/api/Members/GiftList/1/100',
        type: 'GET',
        dataType: "text",
        contentType: "application/json",
        success: function (data) {
            var result = JSON.parse(data);
            var data = JSON.parse(result.data);
            $.each(data.body.dataTable.dataRow, function (i, item) {
                $("#myExchangeTab").append("<div class='part-img col-lg-4 col-md-4 col-sm-4 col-xs-4'>" +
                    "<div class='part-img-infoBox center-block'>" +
                    "<div>" +
                    "<img src='" + item.column[2].value + "' alt='' class='img-responsive'>" +
                    "<p class='part-img-mask' tag='" + item.column[0].value + "'>我要兑换</p>" +
                    "</div>" +
                    "<div class='img-infoBox-text'>" +
                    "<span>所需积分：</span>" +
                    "<span class='part-img-score'>" + item.column[3].value + "</span>" +
                    "<p>" + item.column[1].value + "</p>" +
                    "</div>" +
                    "</div>" +
                    "</div>");
            });
            //弹窗
            $("p.part-img-mask").off().on("click", function () {
                var id = $(this).attr("tag");
                $.ajax({
                    url: '/api/Members/GiftExchange/' + id + '',
                    type: 'GET',
                    dataType: "text",
                    contentType: "application/json",
                    success: function (data) {
                        var result = JSON.parse(data);
                        console.log(result)
                        if (result.code == 1) {
                            var data = JSON.parse(result.data);
                            $("#myscore").text(data.body.dataTable.dataRow["0"].column["0"].value);
                            $("#myscore1").text(data.body.dataTable.dataRow["0"].column["0"].value);
                            $("#myscore2").text(data.body.dataTable.dataRow["0"].column["0"].value);
                            alert('兑换成功！');
                        }
                        else {
                            alert('兑换失败！' + result.message);
                        }
                    }
                });


                $("#mask").show();
            })
            $(".close").off().on("click", function () {
                $("#mask").fadeOut();
            })
        }
    });

    //我的兑换
    $.ajax({
        url: '/api/Members/MyExchange',
        type: 'GET',
        dataType: "text",
        contentType: "application/json",
        success: function (data) {
            var result = JSON.parse(data);
            var data = JSON.parse(result.data);
            $.each(data.body.dataTable.dataRow, function (i, item) {
                $("#myIntegralTab").append("<div class='part-img col-lg-4 col-md-4 col-sm-4 col-xs-4'>" +
                    "<div class='part-img-infoBox center-block'>" +
                    "<div>" +
                    "<img src='" + item.column[2].value + "' alt='' class='img-responsive'>" +
                    "<p class='part-img-mask'>我已兑换</p>" +
                    "</div>" +
                    "<div class='img-infoBox-text'>" +
                    "<span>所需积分：</span>" +
                    "<span class='part-img-score'>" + item.column[3].value + "</span>" +
                    "<p>" + item.column[1].value + "</p>" +
                    "</div>" +
                    "</div>" +
                    "</div>");
            });
        }
    });

    // 积分兑换按钮
    goToChange();
    // 点击修改按钮
    update();

    showScroll();


    $(".cont-left-show>ul>li").click(function () {
        $("article>section").addClass("hide");
        var index = $(this).index();
        $("article>section").eq(index).removeClass("hide");
    });

    $(".cont-left-show ul").find("li").click(function () {
        $(this).addClass("clicked").siblings("li").removeClass("clicked");
    });

    // 文化活动与志愿者切换
    $(".Activity-cell>ul>li").click(function () {
        $(".Activity-cell section").addClass("hide");
        var index = $(this).index();
        $(".Activity-cell section").eq(index).removeClass("hide");
    });
    $(".Activity-cell>ul>li").click(function () {
        $(".Activity-cell>ul>li").removeClass("tabs-line");
        $(this).addClass("tabs-line");
    });
});
function showScroll() {
    var toTop = $("#toTop");
    var window_temp = $(window);
    window_temp.scroll(function () {
        var scrollValue = window_temp.scrollTop();
        scrollValue > 500 ? toTop.fadeIn(300) : toTop.fadeOut(300);
    });
    toTop.on("click", function () {
        $("body").animate({ scrollTop: 0 }, 200);
    });
}

function fromIntegral(type) {
    switch (type) {
        case "1": return "微信签到";
        case "2": return "转发消息";
        case "3": return "参与活动";
        case "4": return "积分兑换";
        case "5": return "API修改";
        case "6": return "后台修改";
    }
}

function addorremoveIntegral(type) {
    switch (type) {
        case "4": return "-";
        default: return "+"
    }
}

function datetime(date) {
    var str = new Date(date);
    var result = str.getFullYear() + "年" + str.getMonth() + "月" + str.getDay() + "日 ";
    return result;
}

function goToChange() {
    $("#chang-btn").on("click", function () {
        $("#Number-cell").addClass("hide");
        $("#exchange-cell").removeClass("hide");
    });
}

function update() {
    $("#phone-update").on("click", function () {
        $("#phone")
            .removeAttr("readonly")
            .trigger("focus")
            .addClass("input-active");
    });
    $("#e-mail-update").on("click", function () {
        $("#e-mail")
            .removeAttr("readonly")
            .trigger("focus")
            .addClass("input-active");
    });
}
function myexchange() {
    var oList = $(".exchange-cell-tabNav ul li");
    oList.each(function (i) {
        $(this).click(function () {
            if ($(this).index() == 0) {
                $(".myshop").css("display", "block");
                $(".myexchange").css("display", "none");
            } else if ($(this).index() == 1) {
                $(".myexchange").css("display", "block");
                $(".myshop").css("display", "none");
            }
            $(this).addClass("tabNav-active");
            oList.each(function (i) {
                $(this).removeClass("tabNav-active");
            });
            $(this).addClass("tabNav-active");
        });
    });
}
myexchange();
//切换标签页
function showTabs() {
    // 选项卡
    $(".tabs>li").click(function () {
        $(".tabs>li").removeClass("tabs-line");
        $(this).addClass("tabs-line");
    });
}
//按钮积分兑换