
(function () {
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
		})
	})
	//选项卡切换
	var a_tab = $(".title_select span");
	var sub_tab = $(".subPages");
	a_tab.on("click",function(){
		var _index = $(this).index();
		$(this).addClass("active").siblings("span").removeClass('active')
		sub_tab.eq(_index).show().siblings(".subPages").hide()
	});
	$(".act_vid li").on("click",function(){
		$(this).find(".maskShow").show();
		$(this).find(".videoPlay").show();
	})
	$(".videoPlay .close").on("click",function(){
		$(this).parent().parent(".videoPlay").siblings(".maskShow").fadeOut();
		$(this).parent().parent(".videoPlay").fadeOut();
		$(this).parent().siblings().find(".video")[0].pause();
		
    })
    textLen()
})(jQuery)
function textLen() {
        //限制个数
        var limited = 140;
        //处理后的字符串
        var afterText = "";
        var text = $(".showroom-p")
        text.map(function (index, value) {
            var _text = $.trim($(value).text());
            if (_text.length >= limited) {
                afterText = _text.slice(0, limited);
                afterText += "...";
            } else {
                afterText = _text;
            }
            $(value).text(afterText);
        })
    }