$(function () {
    textLen()
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
    $(".content-5 .content-5-a span").on("click", function () {
        var _index = $(this).index();
        $(this).addClass("on").siblings("span").removeClass('on')
        ////console.log($(".content-5-box .subPages"))
        $(".content-5-box .subPages").eq(_index).show().siblings(".subPages").hide()
    });
	//banner轮播
	jQuery(".slideBox1").slide({mainCell:".bd ul",effect:"fade",autoPlay:true,trigger:"click"});
	//局部轮播1
	jQuery(".slideBox2").slide({mainCell:".bd ul",effect:"leftLoop",autoPlay:true,trigger:"click"});	
	//	锚点跳转
	$("a.linkRouter").click(function() {
	    $("html, body").animate({
	      scrollTop: $($(this).attr("href")).offset().top + "px"
	    },500);
	    return false;
	  });

    shinian();
    shijieyichanri();
    guojibowuguan();
    
	//下拉菜单
	jQuery.divselect = function(divselectid,inputselectid) {
    	var inputselect = $(inputselectid);
	    $(divselectid+" cite").click(function(){
	        var ul = $(divselectid+" ul");
	        if(ul.css("display")=="none"){
	            ul.slideDown("fast");
	        }else{
	            ul.slideUp("fast");
	        }
	    });
	    $(divselectid+" ul li a").click(function(){
	        var txt = $(this).text();
	        $(divselectid+" cite").html(txt);
	        var value = $(this).attr("selectid");
	        inputselect.val(value);
	        $(divselectid+" ul").hide();
	        
	    });
	};
	 $("body").on("click",function(e){
        if($(e.target).closest('form.select').length){          //判断点击的地方是否是在弹出框里面
                //判断点击对象是否在#box内
        }else{
                $("#divselect ul").hide()
        }                
    });
     $.divselect("#divselect", "#inputselect");
     window.location.hash = '/Culture/Index'
});

function shinian() {
        var parameter = {
            Index: 0,
            Size: 3,
            Name: '"InformationEnglishAll"',
           OrderBy: '"IsTop" desc',
            Condition: {
                Collection: [
                    { F: '"ColumName"', O: "=", P: "@ColumName", V: "Ten-year Jinsha" }
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
                   h+= "<div class='showroom-one row'>" +
                        "<div class='showroom-img col-lg-3 col-md-4 col-sm-12 col-xs-12'>" +
                        "<a target='_blank' href='/culture/ActivityDetail?id=" + item.Id + "'>" +
                        "<img src='/upload/Information/" + item.Id + "/" + item.Photo + "' class='img-responsive' />" +
                        "</a>" +
                        "</div>" +
                        "<div class='showroom-info col-lg-9 col-md-8 col-sm-12 col-xs-12'>" +
                        "<a target='_blank' href='/culture/ActivityDetail?id=" + item.Id + "'>" +
                        "<h3>" + item.Title + "</h3>" +
                       "<p class='showroom-time'>Updated time: " + getCurrentDate(new Date(item.CreationDate)) + "</p>" +
                        "<p class='showroom-p'>" +
                        item.Intro +
                        "</p>" +
                        "</a>" +
                       "<a target='_blank' href='/culture/ActivityDetail?id=" + item.Id + "'> "+
                       "<div class='showroom-btn'>Learn more</div>"+
                        "</a>" +
                        "</div>" +
                        "</div>"

                });
                $("#showroom").html(h); 
                  limtLen();
                $('#ten').Paging({
                    pagesize: 3, count: data.total, toolbar: true, callback: function (page, size, count) {
                        var parameter = {
                            Index: page - 1,
                            Size: size,
                            Name: '"InformationEnglishAll"',
                           OrderBy: '"IsTop" desc',
                            Condition: {
                                Collection: [
                                    { F: '"ColumName"', O: "=", P: "@ColumName", V: "Ten-year Jinsha" }
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
                                        "<a target='_blank' href='/culture/ActivityDetail?id=" + item.Id + "'>" +
                                        "<img src='/upload/Information/" + item.Id + "/" + item.Photo + "' class='img-responsive' />" +
                                        "</a>" +
                                        "</div>" +
                                        "<div class='showroom-info col-lg-9 col-md-8 col-sm-12 col-xs-12'>" +
                                        "<a target='_blank' href='/culture/ActivityDetail?id=" + item.Id + "'>" +
                                        "<h3>" + item.Title + "</h3>" +
                                        "<p class='showroom-time'>Updated time: " + getCurrentDate(new Date(item.CreationDate)) + "</p>" +
                                        "<p class='showroom-p'>" +
                                        item.Intro +
                                        "</p>" +
                                        "</a>" +
                                        "<div class='showroom-btn'>" +
                                        "<a target='_blank' href='/culture/ActivityDetail?id=" + item.Id + "'>Learn more</a>" +
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
            Name: '"InformationEnglishAll"',
           OrderBy: '"IsTop" desc',
            Condition: {
                Collection: [
                    { F: '"ColumName"', O: "=", P: "@ColumName", V: "International Museum Day" }
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
                        "<a target='_blank' href='/culture/ActivityDetail?id=" + item.Id + "'>" +
                        "<img src='/upload/Information/" + item.Id + "/" + item.Photo + "' class='img-responsive' />" +
                        "</a>" +
                        "</div>" +
                        "<div class='showroom-info col-lg-9 col-md-8 col-sm-12 col-xs-12'>" +
                        "<a target='_blank' href='/culture/ActivityDetail?id=" + item.Id + "'>" +
                        "<h3>" + item.Title + "</h3>" +
                        "<p class='showroom-time'>Updated time: " + getCurrentDate(new Date(item.CreationDate)) + "</p>" +
                        "<p class='showroom-p'>" +
                        item.Intro +
                        "</p>" +
                        "</a>" +
                        "<div class='showroom-btn'>" +
                        "<a target='_blank' href='/culture/ActivityDetail?id=" + item.Id + "'>Learn more</a>" +
                        "</div>" +
                        "</div>" +
                        "</div>"

                });
                $("#showroom2").html(h);
          limtLen();
                $('#mus').Paging({
                    pagesize: 3, count: data.total, toolbar: true, callback: function (page, size, count) {
                        var parameter = {
                            Index: page - 1,
                            Size: size,
                            Name: '"InformationEnglishAll"',
                           OrderBy: '"IsTop" desc',
                            Condition: {
                                Collection: [
                                    { F: '"ColumName"', O: "=", P: "@ColumName", V: "International Museum Day" }
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
                                        "<a target='_blank' href='/culture/ActivityDetail?id=" + item.Id + "'>" +
                                        "<img src='/upload/Information/" + item.Id + "/" + item.Photo + "' class='img-responsive' />" +
                                        "</a>" +
                                        "</div>" +
                                        "<div class='showroom-info col-lg-9 col-md-8 col-sm-12 col-xs-12'>" +
                                        "<a target='_blank' href='/culture/ActivityDetail?id=" + item.Id + "'>" +
                                        "<h3>" + item.Title + "</h3>" +
                                        "<p class='showroom-time'>Updated time: " + getCurrentDate(new Date(item.CreationDate)) + "</p>" +
                                        "<p class='showroom-p'>" +
                                        item.Intro +
                                        "</p>" +
                                        "</a>" +
                                        "<div class='showroom-btn'>" +
                                        "<a target='_blank' href='/culture/ActivityDetail?id=" + item.Id + "'>Learn more</a>" +
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
            Name: '"InformationEnglishAll"',
           OrderBy: '"IsTop" desc',
            Condition: {
                Collection: [
                    { F: '"ColumName"', O: "=", P: "@ColumName", V: "National Cultural Heritage Day" }
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
                        "<a target='_blank' href='/culture/ActivityDetail?id=" + item.Id + "'>" +
                        "<img src='/upload/Information/" + item.Id + "/" + item.Photo + "' class='img-responsive' />" +
                        "</a>" +
                        "</div>" +
                        "<div class='showroom-info col-lg-9 col-md-8 col-sm-12 col-xs-12'>" +
                        "<a target='_blank' href='/culture/ActivityDetail?id=" + item.Id + "'>" +
                        "<h3>" + item.Title + "</h3>" +
                        "<p class='showroom-time'>Updated time: " + getCurrentDate(new Date(item.CreationDate)) + "</p>" +
                        "<p class='showroom-p'>" +
                        item.Intro +
                        "</p>" +
                        "</a>" +
                        "<div class='showroom-btn'>" +
                        "<a target='_blank' href='/culture/ActivityDetail?id=" + item.Id + "'>Learn more</a>" +
                        "</div>" +
                        "</div>" +
                        "</div>"

                });
                $("#showroom3").html(h);
          limtLen();
                $('#legacy').Paging({
                    pagesize: 3, count: data.total, toolbar: true, callback: function (page, size, count) {
                        var parameter = {
                            Index: page - 1,
                            Size: size,
                            Name: '"InformationEnglishAll"',
                           OrderBy: '"IsTop" desc',
                            Condition: {
                                Collection: [
                                    { F: '"ColumName"', O: "=", P: "@ColumName", V: "National Cultural Heritage Day" }
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
                                        "<a target='_blank' href='/culture/ActivityDetail?id=" + item.Id + "'>" +
                                        "<img src='/upload/Information/" + item.Id + "/" + item.Photo + "' class='img-responsive' />" +
                                        "</a>" +
                                        "</div>" +
                                        "<div class='showroom-info col-lg-9 col-md-8 col-sm-12 col-xs-12'>" +
                                        "<a target='_blank' href='/culture/ActivityDetail?id=" + item.Id + "'>" +
                                        "<h3>" + item.Title + "</h3>" +
                                        "<p class='showroom-time'>Updated time: " + getCurrentDate(new Date(item.CreationDate)) + "</p>" +
                                        "<p class='showroom-p'>" +
                                        item.Intro +
                                        "</p>" +
                                        "</a>" +
                                        "<div class='showroom-btn'>" +
                                        "<a target='_blank' href='/culture/ActivityDetail?id=" + item.Id + "'>Learn more</a>" +
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


function getCurrentDate(datetime) {
    var date = datetime;
    var monthArray = new Array
        ("January", "February", "March", "April", "May", "June", "July", "August",
        "September", "October", "November", "December");
    var weekArray = new Array("Sunday", "Monday", "Tuesday",
        "Wednesday", "Thursday", "Friday", "Saturday");
    month = date.getMonth();
    day = date.getDate();
    if (day.toString().length == 1) {
        day = "0" + day.toString();
    }
    return monthArray[month] + " " + day + ", " + date.getFullYear();
}

//限制文字的长度
function textLen() {
    //限制个数
    var limited = 450;
    //处理后的字符串
    var afterText = "";
    var text = $(".content-1-text p span")
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
