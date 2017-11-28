$(function () {
    //$(".mySlideBox").slide({ mainCell: ".bd ul", effect: "leftLoop", autoPlay: true, trigger: "click" });  
    //弹框出现
    var index = 0;
    var pageindex = 1;
    //	点击显示
    $("#picBox").on("click", ".picBoximg1>div", function () {
        var title = $(this).attr("data-title");
        var imgsrc = $(this).attr("data-imgsrc");
        var content = $(this).attr("data-content");
        index = $(this).attr("data-index");
        $(".modelTitle").text(title);
        $("#modelImg").attr("src", imgsrc);
        $(".modelBoxInfo").html(content);
        $("#modelBox").show()
        $("body").css({ "overflow": "hidden" }, { "height": "100%" })
        $("html").css({ "overflow": "hidden" }, { "height": "100%" })

    })
    $("#modelleft").on("click", function () {
        if (index > 0) {
            $.ajax({
                url: '/Exhibition/PhotoLastAndNext?pageIndex=' + (pageindex - 1) + '&pageSize=12&id=' + $("#picBox").attr("data-id") + '&index=' + (index - 1) + '',
                type: 'Get',
                success: function (data) {
                    index--;
                    $(".modelTitle").text(data.data.title);
                    $("#modelImg").attr("src", "/upload/Information/" + $("#picBox").attr("data-id") + "/" + data.data.name + "");
                    $(".modelBoxInfo").html(data.data.content);
                    $("#modelBox").show()
                }
            });
        }
    })
    $("#modelright").on("click", function () {
        if (parseInt(index) != $("#picBox").attr("data-count") - 1) {
            $.ajax({
                url: '/Exhibition/PhotoLastAndNext?id=' + $("#picBox").attr("data-id") + '&index=' + (parseInt(index) + 1) + '',
                type: 'Get',
                success: function (data) {
                    index++;
                    $(".modelTitle").text(data.data.title);
                    $("#modelImg").attr("src", "/upload/Information/" + $("#picBox").attr("data-id") + "/" + data.data.name + "");
                    $(".modelBoxInfo").html(data.data.content);
                    $("#modelBox").show()
                }
            });
        }
    })
    //	点击关闭
    $("#closeBox").on("click", function () {
        $("#modelBox").hide();
        $("body").css({ "overflow": "visible" }, { "height": "auto" })
        $("html").css({ "overflow": "visible" }, { "height": "auto" })
    })


    $('#pageToolbar').Paging({
        pagesize: 12, count: $("#picBox").attr("data-count"), toolbar: true, callback: function (page, size, count) {
            $.ajax({
                url: '/Exhibition/PhotoPage?pageIndex=' + (page - 1) + '&pageSize=12&id=' + $("#picBox").attr("data-id") + '',
                type: 'Get',
                success: function (data) {
                    pageindex = page;
                    var aaa = "";
                    $.each(data.data, function (i, item) {
                        var xxxx = (parseInt((page - 1) * 12) + parseInt(i));
                        aaa += '<div class="col-lg-3 col-md-3 col-sm-3 col-xs-12 picBoximg1">' +
                            '<div class="divcontent" data-index="' + xxxx + '" data-content="' + item.content + '" data-imgsrc="/upload/Information/' + $("#picBox").attr("data-id") + '/' + item.name + '" data-title="' + item.title + '">' +
                            '<div class="borderBox">' +
                            '<div class="hoverDiv"></div>' +
                            '<img class="img-responsive center-block" src="/upload/Information/' + $("#picBox").attr("data-id") + '/' + item.name + '" />' +
                            '<p>' + item.title + '</p>' +
                            '</div>' +
                            '</div>' +
                            '</div>';

                    });
                    $("#picBox").html(aaa);
                }
            });
        }
    });

});