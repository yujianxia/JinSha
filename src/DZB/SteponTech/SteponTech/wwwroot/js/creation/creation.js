$(document).ready(function () {
    $.ajaxSetup({ cache: false });
  //回到顶部
    showScroll();
    remai();
  function showScroll() {
  var toTop = $("#toTop")
  var window_temp = $(window)
    window_temp.scroll(function() {
      var scrollValue = window_temp.scrollTop();
      scrollValue > 500 ? toTop.fadeIn(300) : toTop.fadeOut(300);
    });
    toTop.on('click',function() {
      $("body").animate({ scrollTop: 0 }, 200);
    });
  }
});
var ssstnum = 4;
var more_btn = $("#more-btn")
more_btn.on("click", function () {
    $.ajax({
        url: '/Creation/GetSouvenir',
        type: 'Get',
        dataType: "text",
        contentType: "application/json",
        data: {
            st: ssstnum,
            num: 4
        },
        success: function (data) {
            var data = JSON.parse(data);
            var aaaa = "";
            $.each(data, function (i, item) {
                aaaa += '<li class="hot-sell-item col-md-3 col-sm-6 sell-item-new"><div><img src="' +
                    item.pic +
                    '" alt=""></div><p class="sell-item-name">' +
                    item.title +
                    '</p><div class="buy-box"><a target="_blank" href="' + item.urlAddress + '" class="sell-item-buy">点击购买</a></div></li>';
            });
            ssstnum = ssstnum + 4;
            if (aaaa != "") {
                $("#hot-sell-box").append(aaaa);
            }
            else {
                more_btn.hide();
                //$('#no-more').css('display', 'block');
            }

        }
    });
});

function remai() {
    $.ajax({
        url: '/Creation/GetSouvenir',
        type: 'Get',
        dataType: "text",
        contentType: "application/json",
        data: {
            st: 0,
            num: 4
        },
        success: function (data) {
            var aaaa = "";
            var data = JSON.parse(data);
            $.each(data, function (i, item) {
                aaaa += "<li class='hot-sell-item col-md-3 col-sm-6'>" +
                    "<div><img src=" + item.pic + " alt=''></div>" +
                    "<p class='sell-item-name'>" + item.title + "</p>" +
                    "<div class='buy-box'>" +
                    "<a target='_blank' href='" + item.urlAddress + "' class='sell-item-buy'>点击购买</a>" +
                    "</div>" +
                    "</li>"
            });
            $("#hot-sell-box").append(aaaa);
        }
    });
}
