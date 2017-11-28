
(function () {
    $.ajaxSetup({ cache: false });
	//	头部menu
	$(document).ready(function() {
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
	})
	//选项卡
	var a_tab = $(".title_select a");
	var sub_tab = $(".subPages");
	a_tab.on("click",function(){
		var _index = $(this).index();
		$(this).addClass("active").siblings("a").removeClass('active')
		sub_tab.eq(_index).show().siblings(".subPages").hide()
    });



    //会员活动
    $.ajax({
        url: '/api/Members/ActivityInformation/1/3',
        type: 'GET',
        dataType: "text",
        contentType: "application/json",
        success: function (data) {
            var result = JSON.parse(data);
            var data = JSON.parse(result.data);
            $.each(data.body.dataTable.dataRow, function (i, item) {
                    if (Number(item.column[7].value) > new Date().getTime()) {
                        $("#Istosignup").append("<li class='clearfix'>" +
                            "<a target='_blank' href='/Culture/ActivityInfo?id=" + item.column[0].value+"'>" +
                            "<div class='news_pic col-lg-3 col-md-5 col-sm-5 col-xs-11'>" +
                            "<img src='" + item.column[8].value + "' />" +
                            "</div>" +
                            "<div class='news_inner col-lg-9 col-md-7 col-sm-7 col-xs-11'>" +
                            "<p class='title'>" + item.column[4].value + "</p>" +
                            "<p>招募人数：" + item.column[3].value + "+" + item.column[2].value + "</p>" +
                            "<p>活动时间：" + datetime(Number(item.column[5].value)) + "~" + datetime(Number(item.column[6].value)) + "</p>" +
                            "<p>活动地点：金沙遗址博物馆</p>" +
                            "</div>" +
                            "</a>" +
                            "<a href='/Volunteer/Intro?id=" + item.column[0].value + "' class='more'>我要报名</a>" +
                            "</li>");
                    }
                    else {
                        $("#Hasended").append("<li class='clearfix'>" +
                            "<a target='_blank' href='/Culture/ActivityInfo?id=" + item.column[0].value +"'>" +
                            "<div class='news_pic col-lg-3 col-md-5 col-sm-5 col-xs-11'>" +
                            "<img src='" + item.column[8].value + "' />" +
                            "</div>" +
                            "<div class='news_inner col-lg-9 col-md-7 col-sm-7 col-xs-11'>" +
                            "<p class='title'>" + item.column[4].value + "</p>" +
                            "<p>招募人数：" + item.column[3].value + "+" + item.column[2].value + "</p>" +
                            "<p>活动时间：" + datetime(Number(item.column[5].value)) + "~" + datetime(Number(item.column[6].value)) + "</p>" +
                            "<p>活动地点：金沙遗址博物馆</p>" +
                            "</div>" +
                            "</a>" +
                            "<a href='/Volunteer/Intro?id=" + item.column[0].value + "' class='more'>已结束</a>" +
                            "</li>");
                    }
            });


        }
    });



    //分页
    $('#pageToolbar').Paging({
        pagesize: 3, count: 85, toolbar: true, callback: function (page, size, count) {
            $("#Istosignup").empty(); 
            $("#Hasended").empty(); 
            $.ajax({
                url: '/api/Members/ActivityInformation/' + page + '/' + size+'',
                type: 'GET',
                dataType: "text",
                contentType: "application/json",
                success: function (data) {
                    var result = JSON.parse(data);
                    var data = JSON.parse(result.data);
                    $.each(data.body.dataTable.dataRow, function (i, item) {
                            if (Number(item.column[7].value) > new Date().getTime()) {
                                $("#Istosignup").append("<li class='clearfix'>" +
                                    "<a target='_blank' href='/Volunteer/Intro?id=" + item.column[0].value + "'>" +
                                    "<div class='news_pic col-lg-3 col-md-5 col-sm-5 col-xs-11'>" +
                                    "<img src='" + item.column[8].value + "' />" +
                                    "</div>" +
                                    "<div class='news_inner col-lg-9 col-md-7 col-sm-7 col-xs-11'>" +
                                    "<p class='title'>" + item.column[4].value + "</p>" +
                                    "<p>招募人数：" + item.column[3].value + "+" + item.column[2].value + "</p>" +
                                    "<p>活动时间：" + datetime(Number(item.column[5].value)) + "~" + datetime(Number(item.column[6].value)) + "</p>" +
                                    "<p>活动地点：金沙遗址博物馆</p>" +
                                    "</div>" +
                                    "</a>" +
                                    "<a href='/Volunteer/Intro?id=" + item.column[0].value + "' class='more'>我要报名</a>" +
                                    "</li>");
                            }
                            else {
                                $("#Hasended").append("<li class='clearfix'>" +
                                    "<a target='_blank' href='/Volunteer/Intro?id=" + item.column[0].value + "'>" +
                                    "<div class='news_pic col-lg-3 col-md-5 col-sm-5 col-xs-11'>" +
                                    "<img src='" + item.column[8].value + "' />" +
                                    "</div>" +
                                    "<div class='news_inner col-lg-9 col-md-7 col-sm-7 col-xs-11'>" +
                                    "<p class='title'>" + item.column[4].value + "</p>" +
                                    "<p>招募人数：" + item.column[3].value + "+" + item.column[2].value + "</p>" +
                                    "<p>活动时间：" + datetime(Number(item.column[5].value)) + "~" + datetime(Number(item.column[6].value)) + "</p>" +
                                    "<p>活动地点：金沙遗址博物馆</p>" +
                                    "</div>" +
                                    "</a>" +
                                    "<a href='/Volunteer/Intro?id=" + item.column[0].value + "' class='more'>已结束</a>" +
                                    "</li>");
                            }
                    });
                }
            });

        }
    });
	
	//回到顶部
	showScroll();
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
})(jQuery)

function datetime(date) {
    var str = new Date(date);
    var result = str.getFullYear() + "年" + str.getMonth() + "月" + str.getDay() + "日 ";
    return result;
}
