
$(function () {
	//回到顶部
	showScroll();
    function showScroll() {
        $(window).scroll(function () {
            var scrollValue = $(window).scrollTop();
            scrollValue > 500 ? $('.toTop').fadeIn(300) : $('.toTop').fadeOut(300);
        });
        $('.toTop').click(function () {
            $("body").animate({ scrollTop: 0 }, 200);
        });
    }
});
