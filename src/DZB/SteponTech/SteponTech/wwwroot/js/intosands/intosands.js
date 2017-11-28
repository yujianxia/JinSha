
$(function () {
    $(".cont-nav>ul li").on('click',function() { // 锚点
        $("html, body").animate({
            scrollTop: $($(this).attr("href")).offset().top + "px"
        },500);
        return false;
    });
    //点击视频弹框
    $('#gallery').on('click', function () {
        $('.lookVedioMask').css('display', 'block');
    })
    $('.closeImg').on('click', function () {
        $('.lookVedioMask').css('display', 'none');
        document.getElementById('allvideo').pause();
    })
    //点击更多内容（馆长的信）
    $('.boss-speech-more').on('click', function () {
        $('.infoP').css('display', 'none');
        $('.infoall').css('display', 'block');
        $(this).css('display', 'none');
    })
    //手风琴菜单
    $(".listP").on('click',function(){
        $(this).find('i').toggleClass("arrowUp").siblings('.listP').removeClass("arrowUp"); //改变箭头方向
        $(this).toggleClass("focusA").siblings('.listP').removeClass("focusA");             //改变背景颜色
        $(this).parent().toggleClass("parentListBorder").siblings('.listP').removeClass("parentListBorder"); //改变边框
        $(this).next(".childList").slideToggle(500).siblings(".childList").slideUp(500);    //显示内容
    });
    //aboutTextLen()
})

//function aboutTextLen() {
//    //限制个数
//    var limited = 240;
//    //处理后的字符串
//    var afterText = "";
//    var text = $(".sands-introduce-infotext")
//    text.map(function (index, value) {
//        var _text = $.trim($(value).text());
//        if (_text.length >= limited) {
//            afterText = _text.slice(0, limited);
//            afterText += "...";
//        } else {
//            afterText = _text;
//        }
//        $(value).text(afterText);
//    })
//}
