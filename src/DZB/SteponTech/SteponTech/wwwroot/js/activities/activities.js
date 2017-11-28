$(function () {
    $.ajaxSetup({ cache: false });
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
	//限制行数；
	textLen();
    limtlen()
	function textLen() {
		//限制个数
		var limited = 200;
		//处理后的字符串
		var afterText = "";
		var text = $(".showroom-info .showroom-p")
		text.map(function(index, value) {
			var _text = $.trim($(value).text());
			//console.log(_text.length)
			if(_text.length >= limited) {
				afterText = _text.slice(0, limited);
				afterText += "...";
			} else {
				afterText = _text;
			}
			$(value).text(afterText);
		})
	}
	//banner轮播
	jQuery(".slideBox1").slide({
		mainCell: ".bd ul",
		effect: "fade",
		autoPlay: true,
		trigger: "click"
	});
	//局部轮播1
	jQuery(".slideBox2").slide({
		mainCell: ".bd ul",
		effect: "fade",
		autoPlay: true,
		trigger: "click"
	});
	//	锚点跳转
	$("a.linkRouter").click(function() {
		$("html, body").animate({
			scrollTop: $($(this).attr("href")).offset().top + "px"
		}, 500);
		return false;
	});
	//回到顶部
	showScroll();
	$(".content-5 .content-5-a span").on("click", function() {
		var _index = $(this).index();
		$(this).addClass("on").siblings("span").removeClass('on')
		//console.log($(".content-5-box .subPages"))
		$(".content-5-box .subPages").eq(_index).show().siblings(".subPages").hide()
    });

    shinian();
    shijieyichanri();
    guojibowuguan();
    window.location.hash = '/Culture/Index'
	//下拉菜单
	jQuery.divselect = function(divselectid, inputselectid) {
		var inputselect = $(inputselectid);
		$(divselectid + " cite").click(function() {
			var ul = $(divselectid + " ul");
			if(ul.css("display") == "none") {
				ul.slideDown("fast");
			} else {
				ul.slideUp("fast");
			}
		});
		$(divselectid + " ul li a").click(function() {
			var txt = $(this).text();
			$(divselectid + " cite").html(txt);
			var value = $(this).attr("selectid");
			inputselect.val(value);
			$(divselectid + " ul").hide();

		});
	};
	$("body").on("click", function(e) {
		if($(e.target).closest('form.select').length) { //判断点击的地方是否是在弹出框里面
			//判断点击对象是否在#box内
		} else {
			$("#divselect ul").hide()
		}
	});
	$.divselect("#divselect", "#inputselect");
});
function limtlen() {
    //限制个数
    var limited =220;
    //处理后的字符串
    var afterText = "";
    var text = $(".content-1-text>.content")
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
//回到顶部
function showScroll() {
	$(window).scroll(function() {
		var scrollValue = $(window).scrollTop();
		scrollValue > 500 ? $('.toTop').fadeIn(300) : $('.toTop').fadeOut(200);
	});
	$('.toTop').click(function() {
		//console.log(1)
		$("html, body").animate({
			scrollTop: 0
		}, 200);
	});
}



function shinian() {
    var parameter = {
        Index: 0,
        Size: 3,
        Name: '"InformationAll"',
        OrderBy: '"IsTop" desc',
        Condition: {
            Collection: [
                { F: '"ColumName"', O: "=", P: "@ColumName", V: "金沙十年" }
            ]
        }
    };
 
    //console.log(xlwhhd)
    $.ajax({
        url: '/api/DataSearch',
        type: 'Post',
        dataType: "text",
        contentType: "application/json",
        data: JSON.stringify(parameter),
        success: function (data) {
            var h = "";
            var data = JSON.parse(data);
            $.each(data.data, function (i, item) {
                h += "<div class='showroom-one row'>" +
                    "<div class='showroom-img col-lg-3 col-md-4 col-sm-12 col-xs-12'>" +
                    "<a target='_blank' href='/Culture/ActivityDetail?id=" + item.Id + "'>" +
                    "<img src='/upload/Information/" + item.Id + "/" + item.Photo + "' class='img-responsive' />" +
                    "</a>" +
                    "</div>" +
                    "<div class='showroom-info col-lg-9 col-md-8 col-sm-12 col-xs-12'>" +
                    "<a target='_blank' href='/Culture/ActivityDetail?id=" + item.Id + "'>" +
                    "<h3>" + item.Title + "</h3>" +
                    "<p class='showroom-time'>发布时间: " + new Date(item.CreationDate).Format("yyyy年MM月dd日") + "</p>" +
                    "<p class='showroom-p'>" +
                    item.Intro +
                    "</p>" +
                    "</a>" +
                    "<div class='showroom-btn'>" +
                    "<a target='_blank' href='/Culture/ActivityDetail?id=" + item.Id + "'>查看详情</a>" +
                    "</div>" +
                    "</div>" +
                    "</div>"

            });
          
            $("#showroom").html(h);
            $('#ten').Paging({
                pagesize: 3, count: data.total, toolbar: true, callback: function (page, size, count) {
                    var parameter = {
                        Index: page - 1,
                        Size: size,
                        Name: '"InformationAll"',
                        OrderBy: '"IsTop" desc',
                        Condition: {
                            Collection: [
                                { F: '"ColumName"', O: "=", P: "@ColumName", V: "金沙十年" }
                            ]
                        }
                    };
                
                    $.ajax({
                        url: '/api/DataSearch',
                        type: 'Post',
                        dataType: "text",
                        contentType: "application/json",
                        data: JSON.stringify(parameter),
                        success: function (data) {
                            var xlwhhd = $("#xlwhhd").val();
                            window.location.href = '/Culture/Index#' + xlwhhd
                            var h = "";
                            var data = JSON.parse(data);

                            $.each(data.data, function (i, item) {
                                h += "<div class='showroom-one row'>" +
                                    "<div class='showroom-img col-lg-3 col-md-4 col-sm-12 col-xs-12'>" +
                                    "<a target='_blank' href='/Culture/ActivityDetail?id=" + item.Id + "'>" +
                                    "<img src='/upload/Information/" + item.Id + "/" + item.Photo + "' class='img-responsive' />" +
                                    "</a>" +
                                    "</div>" +
                                    "<div class='showroom-info col-lg-9 col-md-8 col-sm-12 col-xs-12'>" +
                                    "<a target='_blank' href='/Culture/ActivityDetail?id=" + item.Id + "'>" +
                                    "<h3>" + item.Title + "</h3>" +
                                    "<p class='showroom-time'>发布时间: " + new Date(item.CreationDate).Format("yyyy年MM月dd日") + "</p>" +
                                    "<p class='showroom-p'>" +
                                    item.Intro +
                                    "</p>" +
                                    "</a>" +
                                    "<div class='showroom-btn'>" +
                                    "<a target='_blank' href='/Culture/ActivityDetail?id=" + item.Id + "'>查看详情</a>" +
                                    "</div>" +
                                    "</div>" +
                                    "</div>"

                            });
                            $("#showroom").html(h);
                        }
                    });
                }
            });
        }
    });
};






function guojibowuguan() {
    var parameter = {
        Index: 0,
        Size: 3,
        Name: '"InformationAll"',
        OrderBy: '"IsTop" desc',
        Condition: {
            Collection: [
                { F: '"ColumName"', O: "=", P: "@ColumName", V: "国际博物馆日" }
            ]
        }
    };

    $.ajax({
        url: '/api/DataSearch',
        type: 'Post',
        dataType: "text",
        contentType: "application/json",
        data: JSON.stringify(parameter),
        success: function (data) {
            var h = "";
            var data = JSON.parse(data);
            $.each(data.data, function (i, item) {
                h += "<div class='showroom-one row'>" +
                    "<div class='showroom-img col-lg-3 col-md-4 col-sm-12 col-xs-12'>" +
                    "<a target='_blank' href='/Culture/ActivityDetail?id=" + item.Id + "'>" +
                    "<img src='/upload/Information/" + item.Id + "/" + item.Photo + "' class='img-responsive' />" +
                    "</a>" +
                    "</div>" +
                    "<div class='showroom-info col-lg-9 col-md-8 col-sm-12 col-xs-12'>" +
                    "<a target='_blank' href='/Culture/ActivityDetail?id=" + item.Id + "'>" +
                    "<h3>" + item.Title + "</h3>" +
                    "<p class='showroom-time'>发布时间: " + new Date(item.CreationDate).Format("yyyy年MM月dd日") + "</p>" +
                    "<p class='showroom-p'>" +
                    item.Intro +
                    "</p>" +
                    "</a>" +
                    "<div class='showroom-btn'>" +
                    "<a target='_blank' href='/Culture/ActivityDetail?id=" + item.Id + "'>查看详情</a>" +
                    "</div>" +
                    "</div>" +
                    "</div>"

            });
            $("#showroom2").html(h);
            $('#mus').Paging({
                pagesize: 3, count: data.total, toolbar: true, callback: function (page, size, count) {
                    var parameter = {
                        Index: page - 1,
                        Size: size,
                        Name: '"InformationAll"',
                        OrderBy: '"IsTop" desc',
                        Condition: {
                            Collection: [
                                { F: '"ColumName"', O: "=", P: "@ColumName", V: "国际博物馆日" }
                            ]
                        }
                    };
          
                    $.ajax({
                        url: '/api/DataSearch',
                        type: 'Post',
                        dataType: "text",
                        contentType: "application/json",
                        data: JSON.stringify(parameter),
                        success: function (data) {
                            var xlwhhd = $("#xlwhhd").val();
                            window.location.href = '/Culture/Index#' + xlwhhd
                            var h = "";
                            var data = JSON.parse(data);
                            $.each(data.data, function (i, item) {
                                h += "<div class='showroom-one row'>" +
                                    "<div class='showroom-img col-lg-3 col-md-4 col-sm-12 col-xs-12'>" +
                                    "<a target='_blank' href='/Culture/ActivityDetail?id=" + item.Id + "'>" +
                                    "<img src='/upload/Information/" + item.Id + "/" + item.Photo + "' class='img-responsive' />" +
                                    "</a>" +
                                    "</div>" +
                                    "<div class='showroom-info col-lg-9 col-md-8 col-sm-12 col-xs-12'>" +
                                    "<a target='_blank' href='/Culture/ActivityDetail?id=" + item.Id + "'>" +
                                    "<h3>" + item.Title + "</h3>" +
                                    "<p class='showroom-time'>发布时间: " + new Date(item.CreationDate).Format("yyyy年MM月dd日") + "</p>" +
                                    "<p class='showroom-p'>" +
                                    item.Intro +
                                    "</p>" +
                                    "</a>" +
                                    "<div class='showroom-btn'>" +
                                    "<a target='_blank' href='/Culture/ActivityDetail?id=" + item.Id + "'>查看详情</a>" +
                                    "</div>" +
                                    "</div>" +
                                    "</div>"

                            });
                            $("#showroom2").html(h);
                        }
                    });
                }
            });
        }
    });

};



function shijieyichanri() {
    var parameter = {
        Index: 0,
        Size: 3,
        Name: '"InformationAll"',
        OrderBy: '"IsTop" desc',
        Condition: {
            Collection: [
                { F: '"ColumName"', O: "=", P: "@ColumName", V: "国际文化遗产日" }
            ]
        }
    };

    $.ajax({
        url: '/api/DataSearch',
        type: 'Post',
        dataType: "text",
        contentType: "application/json",
        data: JSON.stringify(parameter),
        success: function (data) {
            var h = "";
            var data = JSON.parse(data);
            $.each(data.data, function (i, item) {
                h += "<div class='showroom-one row'>" +
                    "<div class='showroom-img col-lg-3 col-md-4 col-sm-12 col-xs-12'>" +
                    "<a target='_blank' href='/Culture/ActivityDetail?id=" + item.Id + "'>" +
                    "<img src='/upload/Information/" + item.Id + "/" + item.Photo + "' class='img-responsive' />" +
                    "</a>" +
                    "</div>" +
                    "<div class='showroom-info col-lg-9 col-md-8 col-sm-12 col-xs-12'>" +
                    "<a target='_blank' href='/Culture/ActivityDetail?id=" + item.Id + "'>" +
                    "<h3>" + item.Title + "</h3>" +
                    "<p class='showroom-time'>发布时间: " + new Date(item.CreationDate).Format("yyyy年MM月dd日") + "</p>" +
                    "<p class='showroom-p'>" +
                    item.Intro +
                    "</p>" +
                    "</a>" +
                    "<div class='showroom-btn'>" +
                    "<a target='_blank' href='/Culture/ActivityDetail?id=" + item.Id + "'>查看详情</a>" +
                    "</div>" +
                    "</div>" +
                    "</div>"

            });
            $("#showroom3").html(h);
            $('#legacy').Paging({
                pagesize: 3, count: data.total, toolbar: true, callback: function (page, size, count) {
                    var parameter = {
                        Index: page - 1,
                        Size: size,
                        Name: '"InformationAll"',
                        OrderBy: '"IsTop" desc',
                        Condition: {
                            Collection: [
                                { F: '"ColumName"', O: "=", P: "@ColumName", V: "国际文化遗产日" }
                            ]
                        }
                    };
           
                    $.ajax({
                        url: '/api/DataSearch',
                        type: 'Post',
                        dataType: "text",
                        contentType: "application/json",
                        data: JSON.stringify(parameter),
                        success: function (data) {
                            var xlwhhd = $("#xlwhhd").val();
                            window.location.href = '/Culture/Index#' + xlwhhd
                            var h = "";
                            var data = JSON.parse(data);
                            $.each(data.data, function (i, item) {
                                h += "<div class='showroom-one row'>" +
                                    "<div class='showroom-img col-lg-3 col-md-4 col-sm-12 col-xs-12'>" +
                                    "<a target='_blank' href='/Culture/ActivityDetail?id=" + item.Id + "'>" +
                                    "<img src='/upload/Information/" + item.Id + "/" + item.Photo + "' class='img-responsive' />" +
                                    "</a>" +
                                    "</div>" +
                                    "<div class='showroom-info col-lg-9 col-md-8 col-sm-12 col-xs-12'>" +
                                    "<a target='_blank' href='/Culture/ActivityDetail?id=" + item.Id + "'>" +
                                    "<h3>" + item.Title + "</h3>" +
                                    "<p class='showroom-time'>发布时间: " + new Date(item.CreationDate).Format("yyyy年MM月dd日") + "</p>" +
                                    "<p class='showroom-p'>" +
                                    item.Intro +
                                    "</p>" +
                                    "</a>" +
                                    "<div class='showroom-btn'>" +
                                    "<a target='_blank' href='/Culture/ActivityDetail?id=" + item.Id + "'>查看详情</a>" +
                                    "</div>" +
                                    "</div>" +
                                    "</div>"

                            });
                            $("#showroom3").html(h);
                        }
                    });
                }
            });
        }
    });

};
 Date.prototype.Format = function (fmt) { //author: meizz 
      var o = {
          "M+": this.getMonth() + 1, //月份 
          "d+": this.getDate(), //日 
          "h+": this.getHours(), //小时 
          "m+": this.getMinutes(), //分 
          "s+": this.getSeconds(), //秒 
          "q+": Math.floor((this.getMonth() + 3) / 3), //季度 
          "S": this.getMilliseconds() //毫秒 
      };
      if (/(y+)/.test(fmt)) fmt = fmt.replace(RegExp.$1, (this.getFullYear() + "").substr(4 - RegExp.$1.length));
      for (var k in o)
          if (new RegExp("(" + k + ")").test(fmt)) fmt = fmt.replace(RegExp.$1, (RegExp.$1.length == 1) ? (o[k]) : (("00" + o[k]).substr(("" + o[k]).length)));
      return fmt;
  }


