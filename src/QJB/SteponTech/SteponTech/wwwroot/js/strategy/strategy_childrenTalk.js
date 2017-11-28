$(function () {
    videolist($("#videoColumId").val(), "video");
    videolist($("#musicColumId").val(), "music");
})
function videolist(id, type) {
    var parameter = {
        Index: 0,
        Size: 1,
        Name: '"InformationYoung"',
        OrderBy: '"CreationDate" desc',
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
            switch (type) {
                case "video":
                    var aaaa = "";
                    var data = JSON.parse(data);
                    $.each(data.data, function (i, item) {
                        aaaa += "<div class='name'>" +
                            "<img src='/img/strategy_childrenTalk/mingzi.png' alt= ''><!--名字-->" +
                            "<p>" + item.PeopleName + "</p>" +
                            "</div>" +
                            "<div class='headImg'><!--头像-->" +
                            "<img src='/upload/Information/" + item.Id + "/" + item.Photo + "' alt=''>" +
                            "</div>" +
                            "<div class='video-text'>" +
                            "<p>" + item.Intro + "</p>" +
                            "</div>" +
                            "<div class='video-video'>" +
                            "<video width='310' height='180' controls>" +
                            "<source src='/upload/Information/" + item.Id + "/" + item.FileName + "' type='video/mp4'>" +
                            "</video>" +
                            "</div>"
                    });
                    $("#videoDocker").html(aaaa);
                    $(".video-text").niceScroll({
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
                    $('#pageTool-1').Paging({
                        pagesize: 1, count: data.total, toolbar: true, callback: function (page, size, count) {
                            var parameter = {
                                Index: page - 1,
                                Size: 1,
                                Name: '"InformationYoung"',
                                OrderBy: '"CreationDate" desc',
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
                                        aaaa += "<div class='name'>" +
                                            "<img src='/img/strategy_childrenTalk/mingzi.png' alt= '' ><!--名字-->" +
                                            "<p>" + item.PeopleName + "</p>" +
                                            "</div>" +
                                            "<div class='headImg'><!--头像-->" +
                                            "<img src='/upload/Information/" + item.Id + "/" + item.Photo + "' alt=''>" +
                                            "</div>" +
                                            "<div class='video-text'>" +
                                            "<p>" + item.Intro + "</p>" +
                                            "</div>" +
                                            "<div class='video-video'>" +
                                            "<video width='310' height='180' controls>" +
                                            "<source src='/upload/Information/" + item.Id + "/" + item.FileName + "' type='video/mp4'>" +
                                            "</video>" +
                                            "</div>"
                                    });
                                    $("#videoDocker").html(aaaa);
                                    $(".video-text").niceScroll({
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
                                }
                            });
                        }
                    });
                    break;
                case "music":
                    var aaaa = "";
                    var data = JSON.parse(data);
                    $.each(data.data, function (i, item) {
                        aaaa += "<img src='/img/strategy_childrenTalk/gold-mask.png' id='cover-img2' alt='gold-mask' />" +
                            "<div class='voice-child'>" +
                            "<audio src='/upload/Information/" + item.Id + "/" + item.Music + "' preload='auto'/>" +
                            "</div>" +
                            "<div class='video-book'>" +
                            "<div class='name'>" +
                            "<img src='/img/strategy_childrenTalk/mingzi.png' alt=''><!--名字-->" +
                            "<p>" + item.PeopleName + "</p>" +
                            "</div>" +
                            "<div class='headImg'><!--头像-->" +
                            " <img src='/upload/Information/" + item.Id + "/" + item.Photo + "' alt=''>" +
                            " </div>" +
                            " <div class='voice-text'>" +
                            "<p>" + item.Intro + "</p>" +
                            "</div>" +
                            "</div>"
                    });
                    $("#musicDocker").html(aaaa);
                    $(".voice-text").niceScroll({
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
                    audiojs.events.ready(function () {
                        audiojs.createAll();
                    });
                    $('#pageTool-2').Paging({
                        pagesize: 1, count: data.total, toolbar: true, callback: function (page, size, count) {
                            var parameter = {
                                Index: page - 1,
                                Size: 1,
                                Name: '"InformationYoung"',
                                OrderBy: '"CreationDate" desc',
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
                                        aaaa += "<div class='voice-child'>" +
                                            "<audio src='/upload/Information/" + item.Id + "/" + item.Music + "' preload='auto'/>" +
                                            "</div>" +
                                            "<div class='video-book'>" +
                                            "<div class='name'>" +
                                            "<img src='/img/strategy_childrenTalk/mingzi.png' alt=''><!--名字-->" +
                                            "<p>" + item.PeopleName + "</p>" +
                                            "</div>" +
                                            "<div class='headImg'><!--头像-->" +
                                            " <img src='/upload/Information/" + item.Id + "/" + item.Photo + "' alt=''>" +
                                            " </div>" +
                                            " <div class='voice-text'>" +
                                            "<p>" + item.Intro + "</p>" +
                                            "</div>" +
                                            "</div>"
                                    });
                                    $("#musicDocker").html(aaaa);
                                    $(".voice-text").niceScroll({
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
                                    audiojs.events.ready(function () {
                                        audiojs.createAll();
                                    });
                                }
                            });
                        }
                    });
                    break
            }
        }
    });
}