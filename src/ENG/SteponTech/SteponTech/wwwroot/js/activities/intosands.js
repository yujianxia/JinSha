
$(function () {
    //回到顶部
    showScroll();
    function showScroll() {
        $(window).scroll(function () {
            var scrollValue = $(window).scrollTop();
            scrollValue > 500 ? $('.toTop').fadeIn(300) : $('.toTop').fadeOut(200);
        });
        $('.toTop').click(function () {
            $("body").animate({scrollTop: 0}, 200);
        });
    }

    // 锚点
    var contNavList = $('.cont-nav>div a');
    for (var i = 0; i < contNavList.length; i++) {
        (function (index) {
            contNavList.eq(index).on('click', function () {
                if (index == 0) {
                    $('html,body').animate({scrollTop: $('#introduce').offset().top}, 500);
                } else if (index == 1) {
                    $('html,body').animate({scrollTop: $('#speech').offset().top}, 500);
                } else if (index == 2) {
                    $('html,body').animate({scrollTop: $('#organization').offset().top}, 500);
                }
            })
        })(i)
    }
    // 点击金沙简介等菜单
    $('.cont-nav>div a').on('click', function () {
        $(this).parent().siblings('div').children('a').removeClass('contNav-active');
        $(this).addClass('contNav-active');
    })
    //点击视频弹框
    $('#gallery').on('click', function () {
        $('.lookVedioMask').css('display', 'block');
    })
    $('.closeImg').on('click', function () {
        $('.lookVedioMask').css('display', 'none');
    })
    //点击更多馆长讲话
    $('.boss-speech-more').on('click', function () {
        $('.speech-info-three').css('display', 'block');
        $('.boss-speech-more').css('display', 'none');
    })
    $(".mylist>p").click(function () {
        if ($(this).hasClass('focusA')) {
            $(this).removeClass('focusA')
        } else {
            $('.mylist').each(function (i) {
                $(this).find('p').removeClass('focusA');
            })
            $(this).addClass('focusA')
        }
    });
})
// 手风琴菜单
    function openMenu(obj) {
        var myul = obj.parentNode;
        var myli = myul.getElementsByClassName('mylist')
        var myol = obj.getElementsByTagName("ol");
        var myolLi = myol[0].getElementsByTagName("li");
        var arrow = document.getElementsByTagName('i');
        var arrow1 = obj.getElementsByTagName('i');
        var oP = document.querySelectorAll('.mylist>p');
        //console.log(obj)
        var oP1 = obj.querySelectorAll('.mylist>p');
        if (obj.style.height == '' || obj.style.height == '60px') {
            for (var i = 0; i < myli.length; i++) {
                myli[i].style.height = "60px";
            }
            for (var x = 0; x < arrow.length; x++) {
                arrow[x].style.transform = "rotateX(0deg)";
            }
            for (var j = 0; j < oP.length; j++) {
                oP[j].style.backgroundColor = "none";
            }
            obj.style.height = (60 + 20 * (myolLi.length) + 80) + "px";
            obj.style.borderBottom = "1px solid #2b190b";
            arrow1[0].style.transform = "rotateX(180deg)";
            oP1[0].style.borderBottomColor = '#170801';
        } else {
            obj.style.height = 60 + "px";
            obj.style.borderBottom = "1px solid #2b190b";
            arrow1[0].style.transform = "rotateX(0deg)";
            oP1[0].style.backgroundColor = "transparent";
            oP1[0].style.borderBottomColor = '#2b190b';
        }
    }




