$(document).ready(function(){                          
    var index=0;
    var length=$("#img img").length;
    var i=1;
    $("#cSlideUl ul").css({left:0})
    //关键函数：通过控制i ，来显示图片
    function showImg(i){
        $("#img img")
            .eq(i).stop(true,true).fadeIn(800)
            .siblings("img").hide();
         $("#cbtn li")
            .eq(i).addClass("hov")
            .siblings().removeClass("hov");
    }
    
    function slideNext(){
        if(index >= 0 && index < length-1) {
             ++index;
             showImg(index);
        }else{
			showImg(0);
			index=0;
			aniPx=(length-5)*160+'px'; //所有图片数 - 可见图片数 * 每张的距离 = 最后一张滚动到第一张的距离
			$("#cSlideUl ul").animate({ "left": "+="+aniPx },200);
			i=1;
            return false;
        }
        if(i<0 || i>length-5) {return false;}						  
               $("#cSlideUl ul").animate({ "left": "-=160px" },200)
               i++;
    }
     
    function slideFront(){
       if(index >= 1 ) {
             --index;
             showImg(index);
        }
        if(i<2 || i>length+5) {return false;}
               $("#cSlideUl ul").animate({ "left": "+=160px" },200)
               i--;
    }	
    	//设置文本内容
        $("#img img").eq(0).show();
        $("#cbtn li").eq(0).addClass("hov");
        $("#cbtn tt").each(function(e){
            var str=(e+1)+""+length;
            $(this).html(str)
        })
    
        $(".picSildeRight,#next").click(function(){
               slideNext();
           })
        $(".picSildeLeft,#front").click(function(){
               slideFront();
           })
        $(".left_btn img").on("click",function(){
        	slideFront();
        })
        $(".right_btn img").on("click",function(){
        	slideNext();
        })
        $("#cbtn li").click(function(){
            index  =  $("#cbtn li").index(this);
            showImg(index);
        });	
		$("#next,#front").hover(function(){
			$(this).children("a").fadeIn();
		},function(){
			$(this).children("a").fadeOut();
		})
    })	