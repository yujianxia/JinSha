$(function () {
    
	//天气
    $('#weather').leoweather({
        format:'<div class="weatherBox"> ' +
        '<i class="icon-{图标} col-lg-5 col-md-5 col-sm-5 col-xs-5 img-responsive"></i>' +
        '<p class="weather-text col-lg-7 col-md-7 col-sm-7 col-xs-7"><span class="qiwen">16 ℃</span></i></p>'+
        '</div>'
    });
	//回到顶部
	showScroll();
//	显示时间
	time();
    timenumber();
	//交通切换
	tab();
	//网上订票

buy();
// 锚点
$(".cont-nav a").click(function() {
	    $("html, body").animate({
	      scrollTop: $($(this).attr("href")).offset().top + "px"
	    },500);
	    return false;
	  });

});

$(window).resize(function () {
    if (window.innerWidth < 1200) {
        $(".visitImg1").removeClass("pull-right")
        //console.log(000)
    } else {
        $(".visitImg1").addClass("pull-right")
    }
})
//回到顶部
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
//	显示时间
function time() {
    var a = new Date();
    //	年月日
    var years =a.getDate() + " / " + (a.getMonth() + 1) + " / " +  a.getFullYear();
    //	am或者pm
    var hours = a.getHours() < 10 ? '0' + a.getHours() : a.getHours()
    //var dn = "am"
    //if (hours > 12) {
    //    dn = "pm"
    //    hours = hours - 12
    //}
    //  星期
    var weeks = new Array("Sunday", "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday");
    var week = new Date().getDay();
    var str = weeks[week];
    $("#dataSpan1").html(years)
    $("#dataSpan2").html(str)
    //$(".dn").text(dn)
}
function timenumber() {
    var b = new Date();
    //	小时
    var hours = b.getHours() < 10 ? '0' + b.getHours() : b.getHours()
    //	分钟
    var minutes = b.getMinutes() < 10 ? '0' + b.getMinutes() : b.getMinutes()
    var time = hours + ':' + minutes
    //  星期
    //	节点赋值
    $(".timeNumber").text(time)
}
setInterval("timenumber()", 1000)
//创建和初始化地图函数：
function initMap() {
    createMap();//创建地图
    setMapEvent();//设置地图事件
    addMapOverlay();//向地图添加覆盖物
    var b = new BMap.Bounds(new BMap.Point(104.010298, 30.679934), new BMap.Point(104.028264, 30.695462));
    try {
        BMapLib.AreaRestriction.setBounds(map, b);
    } catch (e) {
        alert(e);
    }
}
function createMap() { 
    map = new BMap.Map("map");
    map.centerAndZoom(new BMap.Point(104.01955, 30.687539), 17);
}
function setMapEvent() {
    map.enableScrollWheelZoom();
    map.enableKeyboard();
    map.enableDragging();
}
function addClickHandler(target, window) {
    target.addEventListener("click", function () {
        target.openInfoWindow(window);
    });
}
function addMapOverlay() {
    var markers = [
        { content: "", title: "North entrance", imageOffset: { width: -46, height: -21 }, position: { lat: 30.690128, lng: 104.020228 } },
        { content: "", title: "East entrance", imageOffset: { width: -46, height: -21 }, position: { lat: 30.686914, lng: 104.021333 } },
        { content: "", title: "West entrance", imageOffset: { width: -46, height: -21 }, position: { lat: 30.690350, lng: 104.017919 } },
        { content: "", title: "South entrance", imageOffset: { width: -46, height: -21 }, position: { lat: 30.685408, lng: 104.01809 } },
    ];
    for (var index = 0; index < markers.length; index++) {
        var point = new BMap.Point(markers[index].position.lng, markers[index].position.lat);
        var marker = new BMap.Marker(point, {
            icon: new BMap.Icon("http://api.map.baidu.com/lbsapi/createmap/images/icon.png", new BMap.Size(20, 25), {
                imageOffset: new BMap.Size(markers[index].imageOffset.width, markers[index].imageOffset.height)
            })
        });
        var label = new BMap.Label(markers[index].title, { offset: new BMap.Size(25, 5) });
        var opts = {
            width: 200,
            title: markers[index].title,
            enableMessage: false
        };
        var infoWindow = new BMap.InfoWindow(markers[index].content, opts);
        marker.setLabel(label);
        addClickHandler(marker, infoWindow);
        map.addOverlay(marker);
    };
}
var map;
initMap();
function tab(){
    $(".goMothods-box ul li").click(function () {
        var index = $(".goMothods-box ul li").index($(this));
        $(".goMothods-text").eq(index).show().siblings(".goMothods-text").hide();
	$(this).addClass("contNav-active").siblings("li").removeClass("contNav-active");
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




