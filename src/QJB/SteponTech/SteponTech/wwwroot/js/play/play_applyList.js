$(function () {
    //切换
    $(".title_join .title_join-a1").on("click", function () {
        $(this).find("img").attr({ "src": "/img/play_applyList/title_1.png" }).end().siblings("a").find("img").attr({ "src": "/img/play_applyList/title_2.png" })
        $(".inner_join_1").show();
        $(".inner_join_2").hide();
        $(".changeInfo").find("img").attr({ "src": "/img/play_applyList/red.png" })
    });
    $(".title_join .title_join-a2").on("click", function () {
        $(this).find("img").attr({ "src": "/img/play_applyList/finish.png" }).end().siblings("a").find("img").attr({ "src": "/img/play_applyList/loading.png" })
        $(".inner_join_2").show();
        $(".inner_join_1").hide();
        $(".changeInfo").find("img").attr({ "src": "/img/play_applyList/red2.png" })
    });
    $(".inner_join_1,.inner_join_2").niceScroll({
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
    huodong();
})
function huodong() {
    $.ajax({
        url: '/api/Members/ActivityInformation/1/100',
        type: 'GET',
        dataType: "text",
        contentType: "application/json",
        success: function (data) {
            var result = JSON.parse(data);
            var data = JSON.parse(result.data);
            console.log(data)
            $.each(data.body.dataTable.dataRow, function (i, item) {
                if (item.column[1].value != "10")
                {
                    if (Number(item.column[7].value) > new Date().getTime()) {
                        var img = "";
                        if (Number(item.column[3].value) < Number(item.column[2].value))
                            img = "btn_text.png";
                        else
                            img = "btn_text_1.png";
                        $("#Istosignup").append("<dl>" +
                            "<dt><img src='" + item.column[8].value + "' /></dt>" +
                            "<dd> " +
                            "<h3> " + item.column[4].value + "</h3 > " +
                            "<p> 起止时间：" + datetime(Number(item.column[5].value)) + "~" + datetime(Number(item.column[6].value)) + "</p > " +
                            "<p> 活动人数：" + item.column[3].value + " / " + item.column[2].value + "</p > " +
                            "<a target='_blank' href='/PlayJinSha/EnrollmentDetali?id=" + item.column["0"].value + "'><img src='/img/play_applyList/" + img + "' /></a> " +
                            "</dl> " +
                            "</dl>");
                    }
                    else {
                        $("#Hasended").append("<dl>" +
                            "<dt><img src='" + item.column[8].value + "' /></dt>" +
                            "<dd> " +
                            "<h3> " + item.column[4].value + "</h3 > " +
                            "<p> 起止时间：" + datetime(Number(item.column[5].value)) + "~" + datetime(Number(item.column[6].value)) + "</p > " +
                            "<p> 活动人数：" + item.column[3].value + " / " + item.column[2].value + "</p > " +
                            "<a target='_blank' href='/PlayJinSha/EnrollmentDetali?id=" + item.column["0"].value + "'><img src='/img/play_applyList/btn_text_1.png' /></a> " +
                            "</dl> " +
                            "</dl>");
                    }
                }
            });
        }
    });
}
function datetime(date) {
    var str = new Date(date);
    var result = str.getFullYear() + "-" + str.getMonth() + "-" + str.getDay();
    return result;
}