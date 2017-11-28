$(function() {
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
	//回到顶部
	showScroll();

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

	//点击视频弹框
	$('#gallery').on('click', function() {
		$('.lookVedioMask').css('display', 'block');
	})
	$('.closeImg').on('click', function() {
		$('.lookVedioMask').css('display', 'none');
		$("#allvideo")[0].pause();
	})
	//
	$(".sands-introduce-imgMask").hover(function() {
		$(".mask").show()
	}, function() {
		$(".mask").hide()
	})
})