(function ($) {
    $(".hover-box div img").click(function () {
        $(".pic-box>div img").attr('src', this.src);
        var index = $(".hover-box div img").index($(this));
        $(".text").eq(index).show().siblings(".text").hide()
    })
    //  切换
    $(".title>div").click(function () {
        var index = $(".title>div").index($(this));
        $(this).addClass("active").siblings("div").removeClass("active");
        $(".shows-box").eq(index).show().siblings(".shows-box").hide();
    })
    //	选项卡高亮
    var _li = $(".desktop_box li");
    var _treasures_li = $(".treasures li.classic");
    var myCarousel = $('#myCarousel');
    var screenWidth = $("body").width();
    _treasures_li.eq(0).addClass("onActive").find(".treasures_content").css({
        backgroundImage: "url(../../img/classicImg/background.png)"
    })
    //	典藏珍品滑动
    _treasures_li.hover(function () {
        $(this).addClass("onActive").find(".treasures_content").css({
            backgroundImage: "url(../../img/classicImg/background.png)"
        }).end().siblings("li").removeClass("onActive").find(".treasures_content").css({
            backgroundImage: "none"
        });
    })
    //典藏正品弹窗
    _treasures_li.on("click", function (event) {
        $("body").css({ "overflow": "hidden" }, { "height": "100%" })
        $("html").css({ "overflow": "hidden" }, { "height": "100%" })
        event.stopPropagation();
        var _this = $(this);
        $(this).find(".mask-show").show();
        var _scrollHeight = 0;
        _this.find(".wrapper,.font-info").show();
        //缩略图实例化
        _this.find('.ad-gallery').adGallery()[0].settings.effect = "none"

        //3d连接按钮显示
        if (_this.find(".ad-thumb-list a").eq(0).attr("data-href") != "") {
            _this.find('.font-info a.link-3d').css({
                "display": "block"
            });
            _this.find(".font-info a.link-3d")[0].href = _this.find(".ad-thumb-list a").eq(0).attr("data-href");
        } else {
            _this.find('.font-info a.link-3d').hide()
        }
        //video视频
        if (_this.find(".ad-thumb-list a").eq(0).attr("data-video") != "") {
            _this.find('.font-info a.video-link').css({
                "display": "block"
            });
            _this.find(".font-info a.video-link")[0].href = _this.find(".ad-thumb-list a").eq(0).attr("data-video");
        } else {
            _this.find('.font-info a.video-link').hide()
        }
        _this.find('.font-info h3').html(_this.find(".ad-thumb-list a").eq(0).find("img").attr("title"));
        _this.find('.font-info p').html(_this.find(".ad-thumb-list a").eq(0).find("p").html());
        _this.find("button.close").on("click", function (event) {
            $("body").css({ "overflow": "visible" }, { "height": "auto" })
            $("html").css({ "overflow": "visible" }, { "height": "auto" })
            event.stopPropagation()
            _this.find(".mask-show").fadeOut();
        });
        //点击缩略图
        $(".ad-thumb-list li a img").on("click", function (event) {
            if ($(this).parent("a").attr("data-href") != "") {
                _this.find('.font-info a.link-3d').css({
                    "display": "block"
                });
                _this.find(".font-info a.link-3d")[0].href = $(this).parent("a").attr("data-href");
            } else {
                _this.find('.font-info a.link-3d').hide()
            }
            if ($(this).parent("a").attr("data-video") != "") {
                _this.find('.font-info a.video-link').css({
                    "display": "block"
                });
                _this.find(".font-info a.video-link")[0].href = $(this).parent("a").attr("data-video");
            } else {
                _this.find('.font-info a.video-link').hide()
            }
            _scrollHeight = _this.find(".ad-image-wrapper").height();
            _this.find('.font-info h3').html($(this).attr("title"));
            _this.find('.font-info p').html($(this).siblings("p").html());
        })

        _this.find(".ad-prev,.ad-prev-1").on("click", function (event) {
            event.stopPropagation();
            if (_this.find(".ad-thumb-list a.ad-active").attr("data-href") != "") {
                _this.find('.font-info a.link-3d').css({
                    "display": "block"
                });
                _this.find(".font-info a.link-3d")[0].href = _this.find(".ad-thumb-list a.ad-active").attr("data-href");
            } else {
                _this.find('.font-info a.link-3d').hide()
            }
            if (_this.find(".ad-thumb-list a.ad-active").attr("data-video") != "") {
                _this.find('.font-info a.video-link').css({
                    "display": "block"
                });
                _this.find(".font-info a.video-link")[0].href = _this.find(".ad-thumb-list a.ad-active").attr("data-video");
            } else {
                _this.find('.font-info a.video-link').hide()
            }
            _this.find('.font-info h3').html(_this.find(".ad-thumb-list a.ad-active").find("img").attr("title"));
            _this.find('.font-info p').html(_this.find(".ad-thumb-list a.ad-active").find("p").html());
        })
        _this.find(".ad-next,.ad-next-1").on("click", function (event) {
            event.stopPropagation();
            if (_this.find(".ad-thumb-list a.ad-active").attr("data-href") != "") {
                _this.find('.font-info a.link-3d').css({
                    "display": "block"
                });
                _this.find(".font-info a.link-3d")[0].href = _this.find(".ad-thumb-list a.ad-active").attr("data-href");
            } else {
                _this.find('.font-info a.link-3d').hide()
            }
            if (_this.find(".ad-thumb-list a.ad-active").attr("data-video") != "") {
                _this.find('.font-info a.video-link').css({
                    "display": "block"
                });
                _this.find(".font-info a.video-link")[0].href = _this.find(".ad-thumb-list a.ad-active").attr("data-video");
            } else {
                _this.find('.font-info a.video-link').hide()
            }
            _this.find('.font-info h3').html(_this.find(".ad-thumb-list a.ad-active").find("img").attr("title"));
            _this.find('.font-info p').html(_this.find(".ad-thumb-list a.ad-active").find("p").html());
        })
    });
    $(".modal-content").off("click").on("click", function (event) {
        event.stopPropagation();
        return false;
    })
    //滚动条显示隐藏
    $(".font-info .scroll").hover(function () {
        $(this).css("overflow", "auto");
    }, function () {
        $(this).css("overflow", "hidden")
    })
    //	精美桌面
    _li.hover(function () {
        $(this).find('.desktop_title').stop().slideDown()
    }, function () {
        $(this).find('.desktop_title').stop().slideUp()
    });
    var backpage = 1;
    var total = 9;
    $(window).on("scroll", function () {
        if ($(document).scrollTop() + 200 >= $(document).height() - $(window).height()) {
            var inner = '';
            var parameter = {
                Index: backpage,
                Size: 9,
                Name: '"InformationEnglishAll"',
               OrderBy: '"IsTop" desc',
                Condition: {
                    Collection: [
                        { F: '"ColumName"', O: "=", P: "@ColumName", V: "Wallpapers" }
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
                    total = data.total;
                    $.each(data.data, function (i, item) {
                        var src = "~/upload/Information/" + item.Id + "/" + item.Photo;
                        inner += '<li class="col-lg-4 col-md-4 col-sm-6 col-xs-12">' +
                            '<div class="desktop_pic">' +
                            '<a href="/Collection/Wallpaper?id=' + item.Id + '"><img src=' + src + ' alt="wallpaper" /></a>' +
                            '<div class="desktop_title">' +
                            '<div class="title_left">' +
                            item.Title +
                            '</div>' +
                            '<div class="title_right">' +
                            '<div class="icon"></div>' +
                            '<a href="/Collection/Wallpaper?id=' + item.Id + '">Download</a>' +
                            ' </div>' +
                            ' </div>' +
                            '</div>' +
                            ' </li>'

                    });
                   
                }
            });

            backpage += 1;
            if ($('.wrapper_inner li').length >= total) {
                $(".drop-down").html("nothing...")
                return false;
            }
            $('.wrapper_inner ul').append(inner);
            return false;
        }
    })


    //视频弹窗
    $(".mask-close").on("click", function (event) {
        event.stopPropagation();
        $(".video-mask").fadeOut()
        $(this).siblings("video")[0].pause()
    });
    $(".video-link").on("click", function (event) {
        event.stopPropagation();
        $(".video-mask").find("video")[0].src = $(this)[0].href;
        $(".video-mask").show();
        return false
    })
})(jQuery)



