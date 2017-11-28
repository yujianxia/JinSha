
(function($){
	//	头部menu
	$(document).ready(function() {
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
		});
//		下载
		var href = $('input[name="inlineRadioOptions"]:checked').val();
		$(".download_btn a")[0].href = href;
		console.log(href)
		$('input[name="inlineRadioOptions"]').on("change",function(){
			href = $('input[name="inlineRadioOptions"]:checked').val();
			$(".download_btn a")[0].href = href;
		})
		//单选按钮
		$("input.radio").on("click",function(){
			$(this).next("label").addClass("checked").siblings("label").removeClass("checked");
		})
	})
	function showScroll() {
		$(window).scroll(function() {
			var scrollValue = $(window).scrollTop();
			scrollValue > 500 ? $('.toTop').fadeIn(300) : $('.toTop').fadeOut(200);
		});
		$('.toTop').click(function() {
			console.log(1)
			$("html, body").animate({
				scrollTop: 0
			}, 200);
		});
	}
	//单选按钮
	
})(jQuery)
