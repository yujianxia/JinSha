$(function(){
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
		//报名
		$(".btn_baoming").on("click",function(){
			$("#mask").show();
		})
		//关闭弹窗
		$("#remove").on("click",function(){
			$("#mask").hide();
			$("form")[0].reset();
		})
		//回到顶部
		showScroll();
		//回到顶部
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
		});
})
