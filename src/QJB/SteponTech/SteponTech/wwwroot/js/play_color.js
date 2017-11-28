$(function () {
    huodong();
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
            $("#contentlist").empty();
            if (txt != "全部活动")
            {
                var parameter = {
                    Index: 0,
                    Size: 3,
                    Name: '"InformationYoungView"',
                    OrderBy: '"LastUpdate" desc',
                    Condition: {
                        Collection: [
                            { F: '"ColumName"', O: "=", P: "@ColumName", V: $("#activitycite").html() }
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
                        var data = JSON.parse(data);
                        if (data.total == 0) {
                            $(".pageToolbar").hide();
                        } else {
                            $(".pageToolbar").show();
                        }
                        $.each(data.data, function (i, item) {

                            $("#contentlist").append("<li>" +
                                "<a href='/PlayJinSha/PolychromeDetail/" + item.Id + "'>" +
                                "<h3>" + item.Title + "</h3>" +
                                "<div class='img'><img src='/upload/Information/" + item.Id + "/" + item.Photo + "'/></div>" +
                                "<p>" + item.Intro + "</p>" +
                                "</a>" +
                                "</li>")
                        });
                        $('.pageToolbar').empty();
                        $('.pageToolbar').Paging({
                            pagesize: 3, count: data.total, toolbar: true, callback: function (page, size, count) {
                                var parameter = {
                                    Index: page - 1,
                                    Size: 3,
                                    Name: '"InformationYoungView"',
                                    OrderBy: '"LastUpdate" desc',
                                    Condition: {
                                        Collection: [
                                            { F: '"ColumName"', O: "=", P: "@ColumName", V: $("#activitycite").html() }
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
                                        var aaaa = "";
                                        var data = JSON.parse(data);
                                        $("#contentlist").empty();
                                        $.each(data.data, function (i, item) {
                                            $("#contentlist").append("<li>" +
                                                "<a href='/PlayJinSha/PolychromeDetail/" + item.Id + "'>" +
                                                "<h3>" + item.Title + "</h3>" +
                                                "<div class='img'><img src='/upload/Information/" + item.Id + "/" + item.Photo + "'/></div>" +
                                                "<p>" + item.Intro + "</p>" +
                                                "</a>" +
                                                "</li>");
                                        });
                                    }
                                });
                            }
                        });
                    }
                });
            }
            else
            {
                huodong();
            }

		});
	};
	$("body").on("click", function(e) {
		if($(e.target).closest('form #select').length) { //判断点击的地方是否是在弹出框里面
			//判断点击对象是否在#box内
		} else {
			$("#select ul").hide()
		}
	});
    $.divselect("#select", "#inputselect");




})


function huodong() {
    var parameter = {
        Index: 0,
        Size: 3,
        Name: '"InformationYoungView"',
        OrderBy: '"LastUpdate" desc',
        Condition: {
            Collection: [
                { F: '"ColumName"', O: "=", P: "@ColumName1", V: "特色体验活动" },
                { F: '"ColumName"', O: "=", P: "@ColumName2", V: "特展社教活动", L: "or" },
                { F: '"ColumName"', O: "=", P: "@ColumName3", V: "特色艺术活动", L: "or" },
                { F: '"ColumName"', O: "=", P: "@ColumName4", V: "假期品牌活动", L: "or" },
                { F: '"ColumName"', O: "=", P: "@ColumName5", V: "校园文化活动", L: "or" }
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
            var data = JSON.parse(data);

            
            $.each(data.data, function (i, item) {
                $("#contentlist").append("<li>" +
                    "<a href='/PlayJinSha/PolychromeDetail/" + item.Id + "'>" +
                    "<h3>" + item.Title + "</h3>" +
                    "<div class='img'><img src='/upload/Information/" + item.Id + "/" + item.Photo + "'/></div>" +
                    "<p>" + item.Intro +"</p>"+
                    "</a>"+
                    "</li>")
            });
            $('.pageToolbar').empty();
            $('.pageToolbar').Paging({
                pagesize: 3, count: data.total, toolbar: true, callback: function (page, size, count) {
                    var parameter = {
                        Index: page - 1,
                        Size: 3,
                        Name: '"InformationYoungView"',
                        OrderBy: '"LastUpdate" desc',
                        Condition: {
                            Collection: [
                                { F: '"ColumName"', O: "=", P: "@ColumName1", V: "特色体验活动" },
                                { F: '"ColumName"', O: "=", P: "@ColumName2", V: "特展社教活动", L: "or" },
                                { F: '"ColumName"', O: "=", P: "@ColumName3", V: "特色艺术活动", L: "or" },
                                { F: '"ColumName"', O: "=", P: "@ColumName4", V: "假期品牌活动", L: "or" },
                                { F: '"ColumName"', O: "=", P: "@ColumName5", V: "校园文化活动", L: "or" }
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
                            var aaaa = "";
                            var data = JSON.parse(data);
                            $("#contentlist").empty();
                            $.each(data.data, function (i, item) {
                                $("#contentlist").append("<li>" +
                                    "<a href='/PlayJinSha/PolychromeDetail/" + item.Id + "'>" +
                                    "<h3>" + item.Title + "</h3>" +
                                    "<div class='img'><img src='/upload/Information/" + item.Id + "/" + item.Photo + "'/></div>" +
                                    "<p>" + item.Intro + "</p>" +
                                    "</a>" +
                                    "</li>");
                            });
                        }
                    });
                }
            });
        }
    });
}