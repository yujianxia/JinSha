;
$(function() {
	$(document).ready(function() {
		//按钮
		$(".menu-btn").click(function() {
			$(".hide-top-list").toggle();
		})
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
		//回到顶部
		//相关链接
		var window_temp = $(window)
		var aside_box = $("#aside-box")
		var bodyHeight = $(document.body).outerHeight(true)
		window_temp.scroll(function() {
			var scrollValue = window_temp.scrollTop();
			scrollValue > 180 ? aside_box.css("top", "80px") : aside_box.css("top", "312px");
			if($(document).scrollTop() + 200 >= $(document).height() - $(window).height()) {
				aside_box.fadeOut(200);
			} else {
				aside_box.fadeIn(200);
			}
		});
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
	})
})
