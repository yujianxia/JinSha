;
(function ($) {
    $("#actitvtycontent").niceScroll({
        cursorcolor: "#6e3b1b", // 改变滚动条颜色，使用16进制颜色值
        cursoropacitymin: 0, // 当滚动条是隐藏状态时改变透明度, 值范围 1 到 0
        cursoropacitymax: 1, // 当滚动条是显示状态时改变透明度, 值范围 1 到 0
        cursorwidth: "10px", // 滚动条的宽度，单位：便素
        cursorborder: "1px solid #e6af61", // CSS方式定义滚动条边框
        cursorborderradius: "5px", // 滚动条圆角（像素）
        zindex: "auto" | 999, // 改变滚动条的DIV的z-index值
        scrollspeed: 60, // 滚动速度
        mousescrollstep: 40, // 鼠标滚轮的滚动速度 (像素)
        touchbehavior: false, // 激活拖拽滚动
        hwacceleration: true, // 激活硬件加速
        autohidemode: scroll, // 隐藏滚动条的方式, 可用的值:
        background: "#e6af61", // 轨道的背景颜色
        iframeautoresize: true, // 在加载事件时自动重置iframe大小
        cursorminheight: 32, // 设置滚动条的最小高度 (像素)
        preservenativescrolling: true, // 你可以用鼠标滚动可滚动区域的滚动条和增加鼠标滚轮事件
        railoffset: false, // 可以使用top/left来修正位置
        bouncescroll: false, // (only hw accell) 启用滚动跳跃的内容移动
        spacebarenabled: true, // 当按下空格时使页面向下滚动
        railpadding: { top: 0, right: 0, left: 0, bottom: 0 }, // 设置轨道的内间距
        disableoutline: true, // 当选中一个使用nicescroll的div时，chrome浏览器中禁用outline
        horizrailenabled: true, // nicescroll可以管理水平滚动
        enabletranslate3d: true, // nicescroll 可以使用CSS变型来滚动内容
        enablemousewheel: true, // nicescroll可以管理鼠标滚轮事件
        enablekeyboard: true, // nicescroll可以管理键盘事件
        smoothscroll: true, // ease动画滚动
        sensitiverail: true, // 单击轨道产生滚动
        enablemouselockapi: true, // 可以用鼠标锁定API标题 (类似对象拖动)
        cursorfixedheight: false, // 修正光标的高度（像素）
        hidecursordelay: 400, // 设置滚动条淡出的延迟时间（毫秒）
        directionlockdeadzone: 6, // 设定死区，为激活方向锁定（像素）
        nativeparentscrolling: true, // 检测内容底部便于让父级滚动
        enablescrollonselection: true, // 当选择文本时激活内容自动滚动
    });
	//显示弹窗
	$(".btn_doThis").on("click",function(){
		$(".right").hide();
		$(".right_mask").show();
	})
    $(".close").on("click", function () {
        $(".alert").hide();
    });
    $('.icon-close').on('click', function () {
        $(this).parent(".login-mask").fadeOut();
    })
    huodongDeatil();
})(jQuery)


function huodongDeatil() {
    var id = $("#actitvtyid").attr("tag");
    $.ajax({
        url: '/api/Members/ActivityInformationById/1/1/' + id + '',
        type: 'GET',
        dataType: "text",
        contentType: "application/json",
        success: function (data) {
            var result = JSON.parse(data);
            var data = JSON.parse(result.data);
            var item = data.body.dataTable.dataRow["0"].column;
            $("#detailtitle").text(item[4].value);
            $("#actitvtyspan").text(item[3].value + "/" + item[2].value);
            $("#actitvtytime").text(datetime(Number(item[5].value)) + "~" + datetime(Number(item[6].value)));
            $("#Bysigningup").text(datetime(Number(item[7].value)));
            $("#actitvtyimg").attr('src', item[8].value);
            $("#actitvtycontent").append(item[10].value);
            if (Number(Number(item[6].value)) < new Date().getTime()) {
                $("#bottomimg").attr('src', "");
                $("#topimg").attr('src', "");
            }
            else
            {
                $("#bottomimg").on("click", function () {
                    $.ajax({
                        url: '/api/Members/ActivitiesAppointment/1/' + id + '/官网预约',
                        type: 'POST',
                        dataType: "text",
                        contentType: "application/json",
                        success: function (data) {
                            if (data.code == 4) {
                                //尚未登录
                                window.location("/Member/Login")
                            }
                            else if (data.code == 1) {
                                $('.success-apply').show();
                            }
                            else {
                                //预约失败
                                $('.success-apply').show().find("img").attr({ "src":"/img/common/fail.png"});
                            }
                        }
                    });
                });
            }
        }
    });
}
function datetime(date) {
    var str = new Date(date);
    var result = str.getFullYear() + "-" + str.getMonth() + "-" + str.getDay();
    return result;
}