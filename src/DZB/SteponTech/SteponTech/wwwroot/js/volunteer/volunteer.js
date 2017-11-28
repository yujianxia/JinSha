$(function () {
    $.ajaxSetup({ cache: false });
    //切换标签页
    showTabs();
    //切换左边的导航
    shownav();
    //按钮显示与隐藏
    btnShow();
    //注册的步骤
    steps();
    //男女的单选
    checked();

    huodong();
    myinformation();
    signvolunteer();


    //个人中心下面的切换
    clickTab()
    //	角色选择
    checkPerson();
    //	分页的配置
    //$('#pageToolbar').Paging({
    //    pagesize: 10,
    //    count: 85,
    //    toolbar: true,
    //    callback: function (page, size, count) {
    //        //			console.log(arguments)
    //        //			alert('当前第 ' +page +'页,每页 '+size+'条,总页数：'+count+'页')
    //    }
    //});


});


//获取活动
function huodong() {
    $.ajax({
        url: '/api/Members/ActivityInformation/1/100',
        type: 'GET',
        dataType: "text",
        contentType: "application/json",
        success: function (data) {
            var result = JSON.parse(data);
            var data = JSON.parse(result.data);
            $.each(data.body.dataTable.dataRow, function (i, item) {
                if (item.column[1].value == "10") {
                    if (Number(item.column[7].value) > new Date().getTime()) {
                        $("#issigninfo").append("<div class='cont-sign-info'>" +
                            "<div style='position:relative' class='row'>" +
                            "<div class='cont-sign-img col-lg-4 col-md-6 col-sm-6'>" +
                            "<img class='img-responsive' src='" + item.column[8].value + "' />" +
                            "</div>" +
                            "<div class='cont-sign-text col-lg-8 col-md-6 col-sm-6'>" +
                            "<p>" + item.column[4].value + "</p>" +
                            "<p>招募人数：<span>" + item.column[3].value + "</span>&nbsp;/&nbsp;<span>" + item.column[2].value + "</span></p>" +
                            "<p>活动时间：<span>" + datetime(Number(item.column[5].value)) + "</span>—<span>" + datetime(Number(item.column[6].value)) + "</span></p>" +
                            "<p>活动地点：<span>金沙博物馆</span></p>" +
                            "<a href='/Volunteer/Intro?id=" + item.column[0].value + "'><span class='cont-sign-btnhide'>申请加入</span></a>" +
                            "<a href='/Volunteer/Intro?id=" + item.column[0].value + "'><span class='cont-sign-btnshow'>申请加入</span></a>" +
                            "</div>" +
                            "</div>" +
                            "</div>");
                    }
                    else {
                        $("#nosigninfo").append("<div class='cont-sign-info'>" +
                            "<div class='row'>" +
                            "<div class='cont-sign-img col-lg-4 col-md-6 col-sm-6'>" +
                            "<img class='img-responsive' src='" + item.column[8].value + "' />" +
                            "</div>" +
                            "<div class='cont-sign-text col-lg-8 col-md-6 col-sm-6'>" +
                            "<p>" + item.column[4].value + "</p>" +
                            "<p>招募人数：<span>" + item.column[3].value + "</span>&nbsp;/&nbsp;<span>" + item.column[2].value + "</span></p>" +
                            "<p>活动时间：<span>" + datetime(Number(item.column[5].value)) + "</span>—<span>" + datetime(Number(item.column[6].value)) + "</span></p>" +
                            "<p>活动地点：<span>金沙博物馆</span></p>" +
                            "<a href='/Volunteer/Intro?id=" + item.column[0].value + "'><span class='cont-sign-btnhide'>报名结束</span></a>" +
                            "<a href='/Volunteer/Intro?id=" + item.column[0].value + "'><span class='cont-sign-btnshow'>报名结束</span></a>" +
                            "</div>" +
                            "</div>" +
                            "</div>");
                    }
                }
            });
        }
    });


    $.ajax({
        url: '/api/Volunteers/ReviewActivities/1/100',
        type: 'GET',
        dataType: "text",
        contentType: "application/json",
        success: function (data) {
            var result = JSON.parse(data);
            var data = JSON.parse(result.data);
            $.each(data.body.dataTable.dataRow, function (i, item) {
                $("#highlights").append("<div class='cont-sign-info'>" +
                    "<div class='row'>" +
                    "<div class='cont-sign-img col-lg-4 col-md-6 col-sm-6'>" +
                    "<img class='img-responsive' src='" + item.column[8].value + "' />" +
                    "</div>" +
                    "<div class='cont-sign-text col-lg-8 col-md-6 col-sm-6'>" +
                    "<p>" + item.column[4].value + "</p>" +
                    "<p>招募人数：<span>" + item.column[3].value + "</span>&nbsp;/&nbsp;<span>" + item.column[2].value + "</span></p>" +
                    "<p>活动时间：<span>" + datetime(Number(item.column[5].value)) + "</span>—<span>" + datetime(Number(item.column[6].value)) + "</span></p>" +
                    "<p>活动地点：<span>金沙博物馆</span></p>" +
                    "<a href='/Volunteer/Intro?id=" + item.column[0].value + "'><span class='cont-sign-btnhide'>精彩回顾</span></a>" +
                    "<a href='/Volunteer/Intro?id=" + item.column[0].value + "'><span class='cont-sign-btnshow'>精彩回顾</span></a>" +
                    "</div>" +
                    "</div>" +
                    "</div>");
            });
        }
    });
}

//获取个人信息
function myinformation() {
    $.ajax({
        url: '/api/Volunteers/VolunteersSelectById/-1',
        type: 'GET',
        dataType: "text",
        contentType: "application/json",
        success: function (data) {
            var result = JSON.parse(data);
            var data = JSON.parse(result.data);
            $("#volunteername").text(data.body.dataTable.dataRow[0].column[0].value);
            $("#servicePost").text(data.body.dataTable.dataRow[0].column[16].value);
            $("#political").text(data.body.dataTable.dataRow[0].column[24].value);
            $("#myheight").text(data.body.dataTable.dataRow[0].column[8].value + "cm");
            $("#health").text(data.body.dataTable.dataRow[0].column[11].value);
            var star = 0;
            switch (data.body.dataTable.dataRow[0].column[20].value) {
                case "ONE": star = 1; break;
                case "TWO": star = 2; break;
                case "THREE": star = 3; break;
                case "FOUR": star = 4; break;
                default: star = 5; break;
            }
            for (var i = 0; i < star; i++) {
                $("#starnum").append("<img src='/img/volunteer/star-all.png'/>");
            }
        }
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
                if (item.column[1].value == "10") {
                    if (Number(item.column[6].value) > new Date().getTime()) {
                        $("#enrolactitvty").append("<div class='container-fluid oldAct oldAct-one'>" +
                            "<div class='row'>" +
                            "<div class='cont-sign-img col-lg-4 col-md-6 col-sm-6'>" +
                            "<img class='img-responsive' src='" + item.column[8].value + "' />" +
                            "</div>" +
                            "<div class='cont-sign-text col-lg-8 col-md-6 col-sm-6 pl40'>" +
                            "<p>" + item.column[4].value + "</p>" +
                            "<p>招募人数：<span>" + item.column[3].value + "</span>&nbsp;/&nbsp;<span>" + item.column[2].value + "</span></p>" +
                            "<p>活动时间：<span>" + datetime(Number(item.column[5].value)) + "~" + datetime(Number(item.column[6].value)) + "</span></p>" +
                            "<p>活动地点：<span>金沙博物馆</span></p>" +
                            "</div>" +
                            "</div>" +
                            "</div>");
                    }
                    else {
                        $("#joininactitvty").append("<div class='container-fluid oldAct oldAct-one'>" +
                            "<div class='row'>" +
                            "<div class='cont-sign-img col-lg-4 col-md-6 col-sm-6'>" +
                            "<img class='img-responsive' src='" + item.column[8].value + "' />" +
                            "</div>" +
                            "<div class='cont-sign-text col-lg-8 col-md-6 col-sm-6 pl40'>" +
                            "<p>" + item.column[4].value + "</p>" +
                            "<p>招募人数：<span>" + item.column[3].value + "</span>&nbsp;/&nbsp;<span>" + item.column[2].value + "</span></p>" +
                            "<p>活动时间：<span>" + datetime(Number(item.column[5].value)) + "~" + datetime(Number(item.column[6].value)) + "</span></p>" +
                            "<p>活动地点：<span>金沙博物馆</span></p>" +
                            "</div>" +
                            "</div>" +
                            "</div>");
                    }
                }
            });
        }
    });

}


function datetime(date) {
    var str = new Date(date);
    var result = str.getFullYear() + "年" + str.getMonth() + "月" + str.getDay() + "日 ";
    return result;
}

//成为志愿者绑定数据
function signvolunteer() {
    $.ajax({
        url: '/api/Members/MembersSelect',
        type: 'GET',
        dataType: "text",
        contentType: "application/json",
        success: function (data) {
            var result = JSON.parse(data);
            var data = JSON.parse(result.data);
            $("#name").val(data.body.dataTable.dataRow[0].column[1].value);
            $("#phone").val(data.body.dataTable.dataRow[0].column[2].value);
            $("#email").val(data.body.dataTable.dataRow[0].column[4].value);
        }
    });

    //相关选项
    $.ajax({
        url: '/api/Volunteers/VolunteersOptions',
        type: 'GET',
        dataType: "text",
        contentType: "application/json",
        success: function (data) {
            var result = JSON.parse(data);
            var data = JSON.parse(result.data);
            $.each(data.body.dataTable.dataRow, function (i, item) {
                if (item.column[2].value == "NATION") {
                    $("#nationselect").append("<option value=''>" + item.column[3].value + "</option>");
                }
                else if (item.column[2].value == "POLITICAL") {
                    $("#POLITICALselect").append("<option value=''>" + item.column[3].value + "</option>");
                }
                else if (item.column[2].value == "OCCUPATION") {
                    $("#OCCUPATIONselect").append("<option value=''>" + item.column[3].value + "</option>");
                }
                else if (item.column[2].value == "QUALIFICATIONS") {
                    $("#QUALIFICATIONSselect").append("<option value=''>" + item.column[3].value + "</option>");
                }
                else if (item.column[2].value == "CONDITION") {
                    $("#CONDITIONselect").append("<option value=''>" + item.column[3].value + "</option>");
                }
                else if (item.column[2].value == "SERVICETIME") {
                    $("#SERVICETIMEselect").append("<option value=''>" + item.column[3].value + "</option>");
                }
                else if (item.column[2].value == "SERVICETERM") {
                    $("#SERVICETERMselect").append("<option value=''>" + item.column[3].value + "</option>");
                }
                else if (item.column[2].value == "SERVICEPOST") {
                    $("#SERVICEPOSTselect").append("<option value=''>" + item.column[3].value + "</option>");
                }
            });
        }
    });
}


//按钮显示与隐藏
function btnShow() {
    if ($(".menu-btn").css("display") == 'none') {
        $(".hide-top-list").css("display", "none");
        $(".hide-top-list").css("display", "none");
    }
}
//切换标签页
function showTabs() {
    $(".tabs>li").click(function () {
        var index = $(".tabs>li").index($(this));
        $(this).addClass("border-b").siblings("li").removeClass("border-b");
        $(".show-box").eq(index).show().siblings(".show-box").hide();
    })
}
//切换左边的导航
function shownav() {
    $(".cont-left-show>li").click(function () {
        var index = $(".cont-left-show>li").index($(this));
        $(this).addClass("activity").siblings("li").removeClass("activity");
        $(".show-nav").eq(index).show().siblings(".show-nav").hide();
    })
    $(".cont-left-hide>li").click(function () {
        var index = $(".cont-left-hide>li").index($(this));
        $(this).addClass("liColor").siblings("li").removeClass("liColor");
        $(".show-nav").eq(index).show().siblings(".show-nav").hide();
    })
}

var errorArr = [true, false, true, false, false, true, true, false, true, false]
var type = "volunt"
//注册的步骤
function steps() {
    //第一步骤的下一步
    $(".agree i").click(function () {
        if ($(".hideImg").css("display") === "none") {
            $(".hideImg").show()
        } else {
            $(".hideImg").hide()
        }
    })
    $(".one-nextBtn").click(function () {
        $("html, body").animate({
            scrollTop: 0
        }, 0);
        if ($(".hideImg").css("display") === "inline") {
            $(".step-one").hide();
            $(".step-two").show();
            $(".step-three").hide();
            $(".step-line1").addClass("b-color")
            $(".step2 span").addClass("span-color")
            $(".step2 p").addClass("p-color")
        } else {
            alert("请您先阅读志愿者章程")
        }

    });
    //第二步骤的上一步
    $(".two-upBtn").click(function () {
        $("html, body").animate({
            scrollTop: 0
        }, 0);
        $(".step-one").show();
        $(".step-two").hide();
        $(".step-three").hide();
        $(".step-line1").removeClass("b-color")
        $(".step2 span").removeClass("span-color")
        $(".step2 p").removeClass("p-color")
    });
    //第二步骤的下一步
    $(".two-nextBtn").click(function () {
        $("html, body").animate({
            scrollTop: 0
        }, 0);
        $(".step-one").hide();
        $(".step-two").hide();
        $(".step-three").show();
        $(".step-line2").addClass("b-color")
        $(".step3 span").addClass("span-color")
        $(".step3 p").addClass("p-color")
        if (type == "explain") {
            $("#familyBox").show();
            $("#explain_school").text("* 学校：");
            $("#explain_idcard").text("* 监护人身份证：")
            errorArr.pop();
            errorArr.push(false)
        } else if (type == "volunt") {
            $("#familyBox").hide();
            $("#explain_school").text("* 单位 / 学校：");
            $("#explain_phone").text("手机号码：");
            $("#explain_idcard").text("* 身份证号码：")
            $("#explain_email").text("邮箱：")
            errorArr.pop();
            errorArr.push(true)
        }
    });
    //第三步骤的上一步
    $(".upBtn").click(function () {
        $("html, body").animate({
            scrollTop: 0
        }, 0);
        $(".step-one").hide();
        $(".step-two").show();
        $(".step-three").hide();
        $(".step-line2").removeClass("b-color")
        $(".step3 span").removeClass("span-color")
        $(".step3 p").removeClass("p-color")
        $("#date").val("")
        $("#school").val("")
        $("#work").val("")
        $("#idCard").val("")
        $("#good").val("")
        $("#family").val("")
    });
    //	第二步骤的提交
    $(".subBtn").click(function () {
        var bool = true;
        for (var i = 0; i < errorArr.length; i++) {
            if (!errorArr[i]) {
                bool = false
                break
            }
        }
        if (bool) {
            var parameter = {
                name: $("#name").val(),
                sex: $(".bg").attr("tag"),
                nation: $('#nationselect option:selected').text(),
                birthday: $("#date").val(),
                height: $("#height").val(),
                political: $('#POLITICALselect option:selected').text(),
                occupation: $('#OCCUPATIONselect option:selected').text(),
                qualifications: $('#QUALIFICATIONSselect option:selected').text(),
                health: $('#CONDITIONselect option:selected').text(),
                mobile: $("#phone").val(),
                email: $("#email").val(),
                idNumber: $("#idCard").val(),
                unit: $("#school").val(),
                specialty: $("#good").val(),
                rAndE: $("#rande").val(),
                serviceTime: $('#SERVICETIMEselect option:selected').text(),
                serviceTerm: $('#SERVICETERMselect option:selected').text(),
                servicePost: $('#SERVICEPOSTselect option:selected').text(),
                type: type == "volunt" ? "成人志愿者" : "小小志愿者",
                guardian: $("#family").val(),
                img: ""
            };
            $.ajax({
                type: "post",
                url: "/api/Volunteers/VolunteersAdd",
                data: JSON.stringify(parameter),
                contentType: "application/json",
                success: function (res) {
                    if (res.code == 1) {
                        $("#errorspan").text("注册成功！即将跳转到登录！");
                        setTimeout("this.location.href= '/Vip/Login' ", 2000);
                    }
                    else {
                        $("#errorspan").text("注册失败！请重试！");
                    }
                }
            });



            alert("提交成功")
        } else {
            alert("提交失败，请完善个人信息")
        }
    });
}
//角色的选择
function checkPerson() {
    $(".explain").click(function () {
        $(this).addClass("bgI")
        type = "explain"
        $(".volunt").removeClass("bgI")
    })
    $(".volunt").click(function () {
        $(this).addClass("bgI")
        type = "volunt"
        $(".explain").removeClass("bgI")
    })
}
//男女的单选
function checked() {
    $(".man>i").click(function () {
        $(this).addClass("bg")
        $(".woman>i").removeClass("bg")
    })
    $(".woman>i").click(function () {
        $(this).addClass("bg")
        $(".man>i").removeClass("bg")
    })
};
//按钮显示与隐藏
function clickTab() {
    $(".ing").click(function () {
        $(this).addClass("clickColor");
        $(".ed").removeClass("clickColor");
        $(".me-one").show();
        $(".me-two").hide();
    })
    $(".ed").click(function () {
        $(this).addClass("clickColor");
        $(".ing").removeClass("clickColor");
        $(".me-two").show();
        $(".me-one").hide();
    })
};
function testReg(abc) {
    // inputs
    var name = $('#name');
    var bri = $('#date');
    var phone = $('#phone');
    var school = $('#school');
    var card = $('#idCard');
    var family = $('#family');
    var work = $('#work');
    var email = $('#email');
    var good = $('#good');
    var height = $('#height');
    var inputs = [name, bri, phone, school, card, work, email, good, family, height];
    // 正则
    //  不为空
    var regName = /^.{1,}$/;
    var regDate = /^((((1[6-9]|[2-9]\d)\d{2})-(0?[13578]|1[02])-(0?[1-9]|[12]\d|3[01]))|(((1[6-9]|[2-9]\d)\d{2})-(0?[13456789]|1[012])-(0?[1-9]|[12]\d|30))|(((1[6-9]|[2-9]\d)\d{2})-0?2-(0?[1-9]|1\d|2[0-8]))|(((1[6-9]|[2-9]\d)(0[48]|[2468][048]|[13579][26])|((16|[2468][048]|[3579][26])00))-0?2-29-))$/;
    var regPhone = /^1[3-8]\d{9}$/;
    var regSchool = /^.{1,}$/;
    var regCard = /^[1-9]\d{7}((0\d)|(1[0-2]))(([0|1|2]\d)|3[0-1])\d{3}$|^[1-9]\d{5}[1-9]\d{3}((0\d)|(1[0-2]))(([0|1|2]\d)|3[0-1])\d{3}([0-9]|X)$/;
    var regFamily = /^.{1,}$/;
    var regWork = /^.{1,}$/;
    var regEmail = /^([a-zA-Z0-9]+[_|\_|\.]?)*[a-zA-Z0-9]+@([a-zA-Z0-9]+[_|\_|\.]?)*[a-zA-Z0-9]+\.[a-zA-Z]{2,3}$/;
    var regGood = /^.{1,}$/;
    var regHeight = /^[1-9]\d{0,2}$/;
    var regs = [regName, regDate, regPhone, regSchool, regCard, regWork, regEmail, regGood, regFamily, regHeight];

    if (regs[abc].test(inputs[abc].val())) {
        errorArr[abc] = true;
        inputs[abc].next("b").css({ "display": "none" });
    } else {
        // 提示错误信息
        switch (abc) {
            case 0:
                errorArr[0] = false;
                inputs[0].next("b").css({ "display": "block" });
                break;
            case 1:
                errorArr[1] = false;
                inputs[1].next("b").css({ "display": "block" });
                break;
            case 2:
                errorArr[2] = false;
                inputs[2].next("b").css({ "display": "block" });
                break;
            case 3:
                errorArr[3] = false;
                inputs[3].next("b").css({ "display": "block" });
                break;
            case 4:
                errorArr[4] = false;
                inputs[4].next("b").css({ "display": "block" });
                break;
            case 5:
                errorArr[5] = false;
                inputs[5].next("b").css({ "display": "block" });
                break;
            case 6:
                errorArr[6] = false;
                inputs[6].next("b").css({ "display": "block" });
                break;
            case 7:
                errorArr[7] = false;
                inputs[7].next("b").css({ "display": "block" });
                break;
            case 8:
                errorArr[8] = false;
                inputs[8].next("b").css({ "display": "block" });
                break;
            case 9:
                errorArr[9] = false;
                inputs[9].next("b").css({ "display": "block" });
                break;
            default:
                break;
        }
    }
}
$(".step-three").keydown(function () {
    if (event.keyCode == "13") {//keyCode=13是回车键
        $('.subBtn').click();
    }
});