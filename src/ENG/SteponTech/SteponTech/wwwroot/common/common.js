$(function () {
    //轮播(先判断页面是否存在轮播)
    if ($(".mySlideBox").length > 0) {
      $(".mySlideBox").slide({ mainCell: ".bd ul", effect: "leftLoop", autoPlay: true, trigger: "click" });
    } 
    
    //控制相关连接的显示
    showWidth();
    $(".menu-btn").on("click", function () {
        $(".hide-top-list").toggle();
    })
    $(window).scroll(function () {         // 获取滚动条距离
        $(window).scrollTop() > 500 ? $('.toTop').fadeIn(300) : $('.toTop').fadeOut(200);
    });
    $('.toTop').click(function () {      // 点击回到顶部按钮
        $("html, body").animate({scrollTop: 0}, 200);
    });
   
    $(".China").click(function(){
        $(".hideOl").toggle()
    })  
    $("body").on("click", function (event) {
		if (event.target.id != "China" && event.target.id != "hideOl") {
			$("#hideOl").hide();
		}
        if ($(".hideOl").css("display") == 'block') {
            $(".Uparr").css({ "transform":"rotate(90deg)"})
        } else if ($(".hideOl").css("display") == 'none') {
            $(".Uparr").css({"transform":"rotate(270deg)"})
        }
	})
    //模拟下拉
    $(".divselect cite").on("click", function () {
    	var _ul = $(this).siblings("ul");
    	if(_ul.css("display") == "none"){
    		_ul.slideDown("fast");
    	}else{
    		_ul.slideUp("fast");
    	}
    });
	$("body").on("click",function(e){
	    if($(e.target).closest('.divselect').length){          //判断点击的地方是否是在弹出框里面
	            //判断点击对象是否在#box内
	    }else{
	            $(".divselect ul").hide()
	    }                
    });
    //限制文字的长度
});
//限制文字的长度
function limtLen() {
    //限制个数
    var limited = 200;
    //处理后的字符串
    var afterText = "";
    var text = $(".showroom-info .showroom-p");
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
//根据屏幕高度来判断相关链接是否显示；
function showWidth() {
    //相关链接隐藏；
    if ($('.footer').offset().top <= 400) {
        $('#aside-box').hide()        
    }
}



//跳转
$('.stude').on('click', function () {
  window.location.href = 'http://118.114.244.3:8014/'
})
$('.hideOl').find('li').eq(0).on('click', function () {
  window.location.href = 'http://120.25.240.32:8800/'
})
$('.children').on('click', function () {
  window.location.href = 'http://120.25.240.32:8700/'
})
