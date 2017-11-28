$(document).ready(function(){    
	var temp = 0;
    var index=0;
    var length=0;
    var i=1;
    var screenWidth = $("body").width();
    //显示图片数量
    var doCount = Math.floor(screenWidth / 156);
    $(".cSlideUl ul").css({left:0})
    showText();
    //关键函数：通过控制i ，来显示图片
    function showImg(i){
        $(".img").eq(temp).find("img")
            .eq(i).stop(true,true).fadeIn(800)
            .siblings("img").hide();
        $(".cbtn").eq(temp).find("li")
            .eq(i).addClass("hov")
            .siblings().removeClass("hov");
    }
    
    function slideNext(){
        if(index >= 0 && index < length-1) {
             ++index;
             showImg(index);
             if(index == length-1){
             	$(".cSlideUl ul").eq(temp).css({left:"",right:0});
             	return false;
             }
        }else{
			showImg(0);
			index=0;
			aniPx=(length-doCount)*156+'px'; //所有图片数 - 可见图片数 * 每张的距离 = 最后一张滚动到第一张的距离
			$(".cSlideUl ul").eq(temp).animate({ "left": 0 },200);
			i=1;
            return false;
        }
        if(i<0 || i>length-doCount) {return false;}						  
       	$(".cSlideUl ul").animate({ "left": "-=156px" },200)
       	i++;
    }
     
    function slideFront(){
    	if(index >= 1 ) {
            --index;
            showImg(index);
        }
    	if(index == 0){
    		$(".cSlideUl ul").eq(temp).animate({ "left": 0 },200);
    		return false;
    	}
        if(i<2 || i>length+doCount) {return false;}
        	$(".cSlideUl ul").animate({ "left": "+=156px" },200)
        i--;
    }
    function showText(){
    	$(".font-info").eq(temp).find("h3").html($(".cSlideUl").eq(temp).find("ul li.hov").find("h3").html());
    	$(".font-info").eq(temp).find("p").html($(".cSlideUl").eq(temp).find("ul li.hov").find("p").html());
		if($(".cSlideUl").eq(temp).find("ul li.hov").find("a.look").length != 0){
			$(".font-info").eq(temp).find("a").show();
			$(".font-info").eq(temp).find("a")[0].href = $(".cSlideUl").eq(temp).find("ul li.hov").find("a.look")[0].href;
		}
		else{
			$(".font-info").eq(temp).find("a").hide();
		}
    }
    function computedHeight(){
    	//设置弹出层内容高度
		var screenHeight = $(window).height();
		var cSlideUlHeight = $(".cbtn").height();
		var modalHeight = $(".imgnav").height();
		var _height = (screenHeight-cSlideUlHeight-modalHeight) / 2;
//		$(".modal-body").css({paddingTop:_height})
		
    }
    // 图片
	$(".treasures li.classic").on("click",function(){
//		document.documentElement.style.overflowY = 'hidden';
		temp = $(this).index();
		length = $(this).find(".cSlideUl li").length;
		$(this).find(".mask-show").show();
		$(this).find(".modal-content").show();
		$(".cSlideUl ul").eq(temp).css({width:length * 156 + "px"})
		$(".cSlideUl").eq(temp).find("li").off("click").on("click",function(){
	    	$(".cultural_info").hide()
	    	$(".wrapper").show();
	    	$(".font-info").show();
	        index  =  $(".cbtn").eq(temp).find("li").index(this);
	        if(index == 0){
	    		$(".cSlideUl ul").eq(temp).animate({ "left": 0 },200);
	    	}
	        if(index == length-1){
             	$(".cSlideUl ul").eq(temp).css({left:"",right:0});
            }
	        showImg(index);
	        showText();
	        computedHeight();
	    })
	})
//  设置文本
    $(".img img").eq(0).show();
    $(".cbtn tt").each(function(e){
        var str=(e+1)+""+length;
        $(this).html(str)
    })

    $(".picSildeRight,#next").click(function(){
    	$(this).parent(".cbtn").siblings(".modal-body").find(".cultural_info").hide();
    	$(this).parent(".cbtn").siblings(".modal-body").find(".wrapper").show();
    	$(this).parent(".cbtn").siblings(".modal-body").find(".font-info").show();
       	slideNext();
       	showText();
       })
    $(".picSildeLeft,#front").click(function(){
    	$(".cultural_info").hide()
    	$(".wrapper").show();
    	$(".font-info").show();
           slideFront();
           showText();
       })
    $(".left_btn img").on("click",function(){
    	$(".cultural_info").hide()
    	$(".wrapper").show();
    	$(".font-info").show();
    	slideFront();
    	showText();
    })
    $(".right_btn img").on("click",function(){
    	$(".cultural_info").hide()
    	$(".wrapper").show();
    	$(".font-info").show();
    	slideNext();
    	showText();
    })
    $(".cbtn").eq(temp).find("li").click(function(){
    	//console.log(temp)
    	$(".cultural_info").hide()
    	$(".wrapper").show();
    	$(".font-info").show();
        index  =  $(".cbtn li").index(this);
        showImg(index);
        showText();
        computedHeight();
    });	
    $(".cultural_info a").on("click",function(){
    	$(".cultural_info").hide()
    	$(".wrapper").show();
    	$(".font-info").show();
        index  =  $(".cbtn li").index(this);
        showImg(index);
        showText();
        computedHeight();
    });
    //关闭重置
    $(".modal-header .close").on("click",function(){
    	document.documentElement.style.overflowY = 'auto';
    	$(".modal-content").fadeOut();
    	$(".mask-show").fadeOut();
    	$(".cultural_info").show();
    	$(".wrapper").hide();
    	$(".font-info").hide();
    	index = 0;
    })
})	