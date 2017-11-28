;
(function(){
	var a_tab = $(".title_select a");
	var sub_tab = $(".subPages");
	a_tab.on("click",function(){
		let _index = $(this).index();
		$(this).addClass("active").siblings("a").removeClass('active')
		sub_tab.eq(_index).show().siblings(".subPages").hide()
	});
})(jQuery)
