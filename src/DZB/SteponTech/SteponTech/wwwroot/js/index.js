$(document).ready(function() {
	//banner轮播
	jQuery(".slideBox1").slide({mainCell:".bd ul",effect:"fade",autoPlay:true,trigger:"click"});
		//搜索框的变化
	searchData();
	//网上订票
    buy();
    aboutTextLen()  
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
        textLen()
    })
    $("#searchInput").keyup(function (e) {
        if (e.keyCode == 13) {
            window.location.href = encodeURI("/JinSha/SearchResults?searchstring=" + this.value + "")
        }
    });
    $("#input-lg").keyup(function (e) {
        if (e.keyCode == 13) {
            window.location.href = encodeURI("/JinSha/SearchResults?searchstring=" + this.value + "")
        }
    });
})
//搜索框的变化
function searchData() {
    $(".input-group-addon").on("click", function () {
        window.location.href = encodeURI("/JinSha/SearchResults?searchstring=" + $("#input-lg").val() + "")
    });
	$("body").on("click", function(event) {
		$("#searchSpan").show()
        if (event.target.id != "S_btn" && $("#searchInput").val() == '' && event.target.id != "searchInput" && event.target.id != "searchSpan") {
			$("#searchInput").hide();
		} else {
			if($("#searchInput").css("display") == 'none') {
				$("#searchInput").show()
				$("#searchSpan").hide()
			} else if($("#searchInput").css("display") == 'inline-block') {
				$("#searchInput").show()
                $("#searchSpan").hide()
                $("#S_btn").on("click", function () {
                    window.location.href = encodeURI("/JinSha/SearchResults?searchstring=" + $("#searchInput").val() + "")
                })
			}
		}
	});
	//小屏幕下的搜索框
	$("#input-lg").focus(function(){
		$("#showAll-M").show();
	})
	$("#input-lg").blur(function(){
		if($("#input-lg").val() == ""){
		   $("#showAll-M").hide();
		}
	})
}



//网上订票
function buy(){
	$("#buy").on("click", function(){
		$(".buyP").show()
	})
	$(".threeLink").children().on("click", function(){
		$(".buyP").hide()
	})
}


function aboutTextLen() {
    //限制个数
    var limited = 110;
    //处理后的字符串
    var afterText = "";
    var text = $(".introduction-info")
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

