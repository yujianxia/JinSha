
$(function () {
    // 锚点
    $(".cont-nav>ul li").click(function() {
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
    
    //手风琴菜单
    $(".listP").on('click',function(){
        $(this).find('i').toggleClass("arrowUp").siblings('.listP').removeClass("arrowUp"); //改变箭头方向
        $(this).toggleClass("focusA").siblings('.listP').removeClass("focusA");             //改变背景颜色
        $(this).parent().toggleClass("parentListBorder").siblings('.listP').removeClass("parentListBorder"); //改变边框
        $(this).next(".childList").slideToggle(500).siblings(".childList").slideUp(500);    //显示内容
    });
})
