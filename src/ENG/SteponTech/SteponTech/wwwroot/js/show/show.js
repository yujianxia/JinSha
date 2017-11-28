
$(function () {
    //限制文字的长度
    textLen()
    partText()
    // 锚点
    $(".cont-nav>ul li").click(function() {
        $("html, body").animate({
            scrollTop: $($(this).attr("href")).offset().top + "px"
        },500);
        return false;
    });
    //  陈列馆
    $('.display-twobtn-left').on('click', function(){ //点击左边箭头
        $('.firstDisplayBox').css('display','block');
        $('.secondDisplayBox').css('display', 'none');
    });
    $('.display-twobtn-right').on('click', function(){ //点击右边箭头
        $('.secondDisplayBox').css('display', 'block');
        $('.firstDisplayBox').css('display', 'none');
    });
    //  文化景观切换
    var partArray = [
        { a:'.num_li0',b:'#part_div0'},
        { a:'.num_li1',b:'#part_div1'},
        { a:'.num_li2',b:'#part_div2'},
        { a:'.num_li3',b:'#part_div3'},
        { a:'.num_li4',b:'#part_div4'},
    ];
    partArray.forEach(function(v){
        $(v.a).on('click', function(){
            $(v.b).css('display', 'block');
            $(v.b).find('.part-text').css('display','block');
            $(v.b).find('.part-all').css('display','none');
            $(v.b).find('.culture-left-btn').css('display','block');
            $(v.b).siblings('.part').css('display', 'none');
            $(this).toggleClass('nav-cultureLi').siblings('li').removeClass('nav-cultureLi');
        });
    });
    //  文化景观模块当到768px的时候 点击图标
    $('#menuImg').on('click',function(){
        $(this).next(".childrenNav").slideToggle(500).siblings(".childrenNav").slideUp(500);    //显示内容
    });
    // 文化景观中下拉列表
    $(document).click(function(){
        $(".changeRow-nav ul").slideUp();
    })
    $(".select_result").click(function(e){
        e.stopPropagation();
        var ul=$(this).next();
        ul.stop().slideToggle();
    })
    $('.changeRow-nav li').click(function(e){
        e.stopPropagation();
        var a =$(this).parent().prev().children(".one-li").text();
        $(this).parent().prev().children("span").text($(this).text());
        $(this).text(a)
        $('.changeRow-nav ul').stop().slideUp();

    })
    //  分页
    tezhan();
    zhanlan();
});
//限制文字的长度
function textLen() {
    //限制个数
    var limited = 450;
    //处理后的字符串
    var afterText = "";
    var text = $(".relic-cont-infotext > p")
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
//限制文字的长度
function partText() {
    //限制个数
    var limited = 450;
    //处理后的字符串
    var afterText = "";
    var text = $(".part-text")
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
    function tezhan() {
        var parameter = {
            Index: 0,
            Size: 3,
            Name: '"InformationEnglishAll"',
           OrderBy: '"IsTop" desc',
            Condition: {
                Collection: [
                    { F: '"ColumName"', O: "=", P: "@ColumName", V: "Featured Exhibition" }
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
                $.each(data.data, function (i, item) {
                    aaaa += "<div class='showroom-one row'>" +
                        "<!--特展厅图片1-->" +
                        "<a target='_blank' href='Special?id=" + item.Id + "'>" +
                        "<div class='showroom-img col-lg-3 col-md-4 col-sm-12 col-xs-12'>" +
                        "<img src='/upload/Information/" + item.Id + "/" + item.Photo + "' />" +
                        "</div>" +
                        "</a>" +
                        "<!--特展厅文字1-->" +
                        "<a target='_blank' href='Special?id=" + item.Id + "'>" +
                        "<div class='showroom-info col-lg-9 col-md-8 col-sm-12 col-xs-12'>" +
                        "<h3>" + item.Title + "</h3>" +
                        "<p class='showroom-time'>Exhibition time: " + item.InformationTime + "</p>" +
                        "<p class='showroom-p'>" + item.Intro +"</p>"+
                        " <a target='_blank' href='Special?id=" + item.Id + "'><div class='showroom-btn'>Learn more</div></a>" +
                        "</div >" +
                        "</a >" +
                        "</div > "
                });
                $("#asdasdasdasdas1").html(aaaa);
                limtLen()
                $('#pageToolbar1').Paging({
                    pagesize: 3, count: data.total, toolbar: true, callback: function (page, size, count) {
                        var parameter = {
                            Index: page - 1,
                            Size: 3,
                            Name: '"InformationEnglishAll"',
                           OrderBy: '"IsTop" desc',
                            Condition: {
                                Collection: [
                                    { F: '"ColumName"', O: "=", P: "@ColumName", V: "Featured Exhibition" }
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
                                $.each(data.data, function (i, item) {
                                    aaaa += "<div class='showroom-one row'>" +
                                        "<!--特展厅图片1-->" +
                                        "<a target='_blank' href='Special?id=" + item.Id + "'>" +
                                        "<div class='showroom-img col-lg-3 col-md-4 col-sm-12 col-xs-12'>" +
                                        "<img src='/upload/Information/" + item.Id + "/" + item.Photo + "' />" +
                                        "</div>" +
                                        "</a>" +
                                        "<!--特展厅文字1-->" +
                                        "<a target='_blank' href='Special?id=" + item.Id + "'>" +
                                        "<div class='showroom-info col-lg-9 col-md-8 col-sm-12 col-xs-12'>" +
                                        "<h3>" + item.Title + "</h3>" +
                                        "<p class='showroom-time'>Exhibition time: " + item.InformationTime + "</p>" +
                                        "<p class='showroom-p'>" + item.Intro + "</p>" +
                                        " <a target='_blank' href='Special?id=" + item.Id + "'><div class='showroom-btn'>Learn more</div></a>" +
                                        "</div >" +
                                        "</a >" +
                                        "</div > "
                                });
                                $("#asdasdasdasdas1").html(aaaa);
                            }
                        });
                        var tezhan = $("#tezhan").val();
                        limtLen()
                        window.location.href = '/Exhibition/Index#' + tezhan
                    }
                });

            }
        });
    }

    function zhanlan() {
        var parameter = {
            Index: 0,
            Size: 3,
            Name: '"InformationEnglishAll"',
           OrderBy: '"IsTop" desc',
            Condition: {
                Collection: [
                    { F: '"ColumName"', O: "=", P: "@ColumName", V: "Exhibition for Hire" }
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
                $.each(data.data, function (i, item) {
                    aaaa += "<div class='showroom-one row'>" +
                        "<!--展览图片1-->" +
                        "<a target='_blank' href='Special?id=" + item.Id + "'>" +
                        "<div class='showroom-img col-lg-3 col-md-4 col-sm-12 col-xs-12'>" +
                        "<img src='/upload/Information/" + item.Id + "/" + item.Photo + "' />" +
                        "</div>" +
                        "</a>" +
                        "<!--展览文字1-->" +
                        "<a target='_blank' href='Special?id=" + item.Id + "'>" +
                        "<div class='showroom-info col-lg-9 col-md-8 col-sm-12 col-xs-12'>" +
                        "<h3>" + item.Title + "</h3>" +
                        "<p class='showroom-time'>Exhibition time: " + item.InformationTime + "</p>" +
                        "<p class='showroom-p'>" + item.Intro + "</p>" +
                        " <a target='_blank' href='Special?id=" + item.Id + "'><div class='showroom-btn'>Learn more</div></a>" +
                        "</div >" +
                        "</a >" +
                        "</div > "
                });
                $("#asdasdasdasdas2").html(aaaa);
                limtLen()
                $('#pageToolbar2').Paging({
                    pagesize: 3, count: data.total, toolbar: true, callback: function (page, size, count) {
                        var parameter = {
                            Index: page - 1,
                            Size: 3,
                            Name: '"InformationEnglishAll"',
                           OrderBy: '"IsTop" desc',
                            Condition: {
                                Collection: [
                                    { F: '"ColumName"', O: "=", P: "@ColumName", V: "Exhibition for Hire" }
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
                                $.each(data.data, function (i, item) {
                                    aaaa += "<div class='showroom-one row'>" +
                                        "<!--展览图片1-->" +
                                        "<a target='_blank' href='Special?id=" + item.Id + "'>" +
                                        "<div class='showroom-img col-lg-3 col-md-4 col-sm-12 col-xs-12'>" +
                                        "<img src='/upload/Information/" + item.Id + "/" + item.Photo + "' />" +
                                        "</div>" +
                                        "</a>" +
                                        "<!--展览文字1-->" +
                                        "<a target='_blank' href='Special?id=" + item.Id + "'>" +
                                        "<div class='showroom-info col-lg-9 col-md-8 col-sm-12 col-xs-12'>" +
                                        "<h3>" + item.Title + "</h3>" +
                                        "<p class='showroom-time'>Exhibition time: " + item.InformationTime + "</p>" +
                                        "<p class='showroom-p'>" + item.Intro + "</p>" +
                                        " <a target='_blank' href='Special?id=" + item.Id + "'><div class='showroom-btn'>Learn more</div></a>" +
                                        "</div >" +
                                        "</a >" +
                                        "</div > "
                                });
                                $("#asdasdasdasdas2").html(aaaa);
                                limtLen()
                            }
                        });
                        var zhanlan = $("#zhanlan").val();
                        window.location.href = '/Exhibition/Index#' + zhanlan
                    }
                });
            }
        });
    }


