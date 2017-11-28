$(function () {
    var index = 0;
    var length = $("#img img").length;
    var i = 1;
    var screenWidth = $("#cSlideUl").width();
    var _liWidth = parseInt($("#cSlideUl li").css("width"));
    var _liMargin = parseInt($("#cSlideUl li").css("margin-left")) * 2;
    var sum = _liWidth + _liMargin;
    var doCount = doCount = Math.floor(screenWidth / sum);
    $("#cSlideUl ul").css({
        left: 0,
        width: $("#cSlideUl li").length * sum
    })
    //关键函数：通过控制i ，来显示图片
    function showImg(i) {
        $("#img img")
            .eq(i).stop(true, true).fadeIn(800)
            .siblings("img").hide();
        $("#cbtn li")
            .eq(i).addClass("hov")
            .siblings().removeClass("hov");
    }

    function slideNext() {
        if (index >= 0 && index < length - 1) {
            ++index;
            showImg(index);
        } else {
            showImg(0);
            index = 0;
            aniPx = (length - doCount) * sum + 'px'; //所有图片数 - 可见图片数 * 每张的距离 = 最后一张滚动到第一张的距离
            if (length <= doCount) {
                return false;
            } else {
                $("#cSlideUl ul").animate({
                    "left": "+=" + aniPx
                }, 200);
                i = 1;
                return false;
            }

        }
        if (i < 0 || i > length - doCount || length <= doCount) {
            return false;
        }
        $("#cSlideUl ul").animate({
            "left": "-=" + sum + "px"
        }, 200)
        i++;
    }

    function slideFront() {
        if (index >= 1) {
            --index;
            showImg(index);
        }
        if (i < 2 || i > length + doCount) {
            return false;
        }
        $("#cSlideUl ul").animate({
            "left": "+=" + sum + "px"
        }, 200)
        i--;
    }
    //设置文本内容
    $("#img img").eq(0).show();
    $("#cbtn li").eq(0).addClass("hov");
    $("#cbtn tt").each(function (e) {
        var str = (e + 1) + "" + length;
        $(this).html(str)
    })

    $(".picSildeRight,#next").click(function () {
        slideNext();
    })
    $(".picSildeLeft,#front").click(function () {
        slideFront();
    })
    $(".left_btn img").on("click", function () {
        slideFront();
    })
    $(".right_btn img").on("click", function () {
        slideNext();
    })
    $("#cbtn li").click(function () {
        index = $("#cbtn li").index(this);
        showImg(index);
    });
    $("#next,#front").hover(function () {
        $(this).children("a").fadeIn();
    }, function () {
        $(this).children("a").fadeOut();
    })
})
