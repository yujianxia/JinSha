;
(function(){
	// 图片
	$(".hover-box div img").click(function() {
		$(".pic-box>div img").attr('src', this.src);
		var index = $(".hover-box div img").index($(this));
		$(".text").eq(index).show().siblings(".text").hide()
	})
	//  切换
	$(".title>div").click(function() {
		var index = $(".title>div").index($(this));
		$(this).addClass("active").siblings("div").removeClass("active");
		$(".shows-box").eq(index).show().siblings(".shows-box").hide();
	})
	//选项卡
	var a_tab = $(".title_select a");
	var sub_tab = $(".subPages");
	a_tab.on("click",function(){
		var _index = $(this).index();
		$(this).addClass("active").siblings("a").removeClass('active')
		sub_tab.eq(_index).show().siblings(".subPages").hide()
	});
	//回到顶部
	showScroll();
	function showScroll() {
		$(window).scroll(function() {
			var scrollValue = $(window).scrollTop();
			scrollValue > 500 ? $('.toTop').fadeIn(300) : $('.toTop').fadeOut(200);
		});
		$('.toTop').click(function() {
			////console.log(1)
			$("html, body").animate({
				scrollTop: 0
			}, 200);
		});
	}
})(jQuery)
