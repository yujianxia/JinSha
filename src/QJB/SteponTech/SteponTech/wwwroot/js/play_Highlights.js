;
$(function(){
	//轮播
	jQuery("#slideBox").slide({mainCell:".bd ul",autoPlay:true,playStateCell: '.playState',mouseOverStop:false});
	//弹窗
	$("#slideBox li a").on("click",function(){
		var _index = $(this).parent("li").index();
		$("#mask").show();
		//弹窗轮播
		jQuery("#slideBox2").slide({mainCell:".bd ul",defaultIndex:_index});
	})
	$("#mask .close").on("click",function(){
		$("#mask").fadeOut()
	})
})