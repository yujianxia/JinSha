$(function() {
//	显示时间
    time();
    timenumber()
//	百度地图
	//Map();
	//网上订票
	buy();
// 锚点
   cc()
   //交通切换
   tab()
   //天气
   aa()
});
$(window).resize(function () {
    if (window.innerWidth < 992) {
        $(".visitImg").removeClass("pull-right")
    } else {
        $(".visitImg").addClass("pull-right")
    }
})
//锚点
function cc(){
    $(".cont-nav>ul>a").click(function () {
        $("html, body").animate({
            scrollTop: $($(this).attr("href")).offset().top + "px"
        }, 500);
        return false;
    });
}
//天气（原来的插件）
//function aa(){
//	 $('#weather').leoweather({
//        format:'<div class="weatherBox"> ' +
//        '<i class="icon-{图标} col-lg-5 col-md-5 col-sm-5 col-xs-5 img-responsive"></i>' +
//        '<p class="weather-text col-lg-7 col-md-7 col-sm-7 col-xs-7"><span  class="tianqi">{天气}</span><span  class="tianqi">{风向}</span><span class="qiwen">{气温}℃</span></i></p>'+
//        '</div>'
//    });
//}

//天气（模拟的数据）
function aa() {
    $('#weather').leoweather({
        format: '<div class="weatherBox"> ' +
        '<i class="icon-yin icon-{图标} col-lg-5 col-md-5 col-sm-5 col-xs-5 img-responsive"></i>' +
        '<p class="weather-text col-lg-7 col-md-7 col-sm-7 col-xs-7"><span  class="tianqi">阴</span><span  class="tianqi">微风</span><span class="qiwen">15℃</span></i></p>' +
        '</div>'
    });
}
//	显示时间
function time() {
	var a = new Date();
    //	年月日
	var years = a.getFullYear() + "年" + (a.getMonth() + 1) + "月" + a.getDate() + "日";
//	am或者pm
    var hours = a.getHours() < 10 ? '0' + a.getHours() : a.getHours()
//  星期
	var weeks = new Array("日", "一", "二", "三", "四", "五", "六");
	var week = new Date().getDay();
	var str = "星期" + weeks[week];
    $("#dataSpan1").html(years)
    $("#dataSpan2").html(str)
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
    $(".timeNumber").html(time)
}
setInterval("timenumber()", 1000)
//function Map() {
//    // 百度地图API功能
//    var map = new BMap.Map("map");    // 创建Map实例
//    var poi = new BMap.Point(104.019281, 30.687698);
//    map.centerAndZoom(poi, 17);// 初始化地图,用城市名设置地图中心点
//    map.addControl(new BMap.MapTypeControl());   //添加地图类型控件
//    map.setCurrentCity("成都");          // 设置地图显示的城市 此项是必须设置的
//    map.enableScrollWheelZoom(true);     //开启鼠标滚轮缩放
//    var b = new BMap.Bounds(new BMap.Point(104.010298, 30.679934), new BMap.Point(104.028264, 30.695462));
//    try {
//        BMapLib.AreaRestriction.setBounds(map, b);
//    } catch (e) {
//        alert(e);
//    }
//}
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
        { content: "金沙遗址博物馆北大门", title: "北大门", imageOffset: { width: -46, height: -21 }, position: { lat: 30.690128, lng: 104.020228 } },
        { content: "金沙遗址博物馆东大门", title: "东大门", imageOffset: { width: -46, height: -21 }, position: { lat: 30.686914, lng: 104.021333 } },
        { content: "金沙遗址博物馆西门", title: "西门", imageOffset: { width: -46, height: -21 }, position: { lat: 30.690350, lng: 104.017919 } },
        { content: "金沙遗址博物馆西南门", title: "西南门", imageOffset: { width: -46, height: -21 }, position: { lat: 30.685408, lng: 104.01809 }},
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

//网上订票
function buy(){
	$("#buy").on("click", function(){
		$(".buyP").show()
	})
	$(".threeLink").children().on("click", function(){
		$(".buyP").hide()
	})
}

function tab(){
	$(".goMothods-box ul li").click(function() {
		var index = $(".goMothods-box ul li").index($(this));
        $(".goMothods-text").eq(index).show().siblings(".goMothods-text").hide();
        $(this).addClass("contNav-active").siblings("li").removeClass("contNav-active")
	})
}



