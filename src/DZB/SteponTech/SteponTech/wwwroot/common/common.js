$(function () {
    //轮播(先判断页面是否存在轮播)
    if ($(".mySlideBox").length > 0) {
        $(".mySlideBox").slide({ mainCell: ".bd ul", effect: "leftLoop", autoPlay: true, trigger: "click" });
    }

	$(".menu-btn").click(function() { //平板缩小后点击menu图标
		$(".hide-top-list").toggle();
	});
	//检测浏览器宽度
	$(window).resize(function() {
		if($(window).width() > 992) {
			$(".hide-top-list").hide();
		}
	});
	$(window).scroll(function() { // 获取滚动条距离
		$(window).scrollTop() > 500 ? $('.toTop').fadeIn(300) : $('.toTop').fadeOut(200);
	});
	$('.toTop').click(function() { // 点击回到顶部按钮
		$("html, body").animate({
			scrollTop: 0
		}, 200);
	});
	$(".China").click(function() {
		$(".hideOl").toggle()
	})
	$("body").on("click", function(event) {
		if(event.target.id != "China" && event.target.id != "hideOl") {
			$("#hideOl").hide();
		}
		if($(".hideOl").css("display") == 'block') {
			$(".Uparr").css({
				"transform": "rotate(270deg)"
			})
		} else if($(".hideOl").css("display") == 'none') {
			$(".Uparr").css({
				"transform": "rotate(90deg)"
			})
		}
	})
	//模拟下拉
	$(".divselect cite").on("click", function() {
		var _ul = $(this).siblings("ul");
		if(_ul.css("display") == "none") {
			_ul.slideDown("fast");
		} else {
			_ul.slideUp("fast");
		}
	});
	$("body").on("click", function(e) {
		if($(e.target).closest('.divselect').length) { //判断点击的地方是否是在弹出框里面
			//判断点击对象是否在#box内
		} else {
			$(".divselect ul").hide()
		}
	});
});

//版本链接
$('.top-list').find('ul').find('li').eq(1).on('click', function () {
    window.location.href = 'http://120.25.240.32:8000/'
});
$('.top-list').find('ul').find('li').eq(2).on('click', function () {
    window.location.href = 'http://118.114.244.3:8014/'
});
$('.top-list').find('ul').find('li').eq(3).on('click', function () {
    window.location.href = 'http://120.25.240.32:8700/'
})