$(function() {
    $('.carousel').carousel()
	//回到顶部
	showScroll();
    //回到顶部
    function showScroll() {
        $(window).scroll(function() {
            var scrollValue = $(window).scrollTop();
            scrollValue > 500 ? $('.toTop').fadeIn(300) : $('.toTop').fadeOut(200);
        });
        $('.toTop').click(function() {
            $("body").animate({
                scrollTop: 0
            }, 200);
        });
    }

});








