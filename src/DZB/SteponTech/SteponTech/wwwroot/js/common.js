$(function () {
    $(window).scroll(function () {         // 获取滚动条距离
        $(window).scrollTop() > 500 ? $('.toTop').fadeIn(300) : $('.toTop').fadeOut(200);
    });
    $('.toTop').click(function () {      // 点击回到顶部按钮
        $("html, body").animate({scrollTop: 0}, 200);
    });
    $(".menu-btn").click(function () {   //平板缩小后点击menu图标
        $(".hide-top-list").toggle();
    });
});
