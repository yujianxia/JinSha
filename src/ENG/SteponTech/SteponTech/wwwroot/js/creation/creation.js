$(document).ready(function () {
    remai();

  
});
// 循环渲染热卖纪念品
var sellItem1 = {
    url: "../../img/creation/picture-four.png",
    name: "Gold Mask Ornament"
};
var sellItem2 = {
    url: "../../img/creation/picture-five.png",
    name: "Jinsha Redwood Plate"
};
var sellItem3 = {
    url: "../../img/creation/picture-six.png",
    name: "Jinsha Night Lamp"
};
var sellItem4 = {
    url: "../../img/creation/picture-six.png",
    name: "Jinsha Night Lamp"
};
var sellItem5 = {
    url: "../../img/creation/picture-six.png",
    name: "Gold Mask Ornament"
};
var sellItem6 = {
    url: "../../img/creation/picture-six.png",
    name: "Jinsha Redwood Plate"
};
var sellItem7 = {
    url: "../../img/creation/picture-six.png",
    name: "Jinsha Night Lamp"
};
var sellItem8 = {
    url: "../../img/creation/picture-six.png",
    name: "Jinsha Night Lamp"
};
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
            console.log(data)
            var aaaa = "";
            var arr = [];
            var str = [];
            $.each(data, function (i, item) {                  
                    aaaa = '<li class="hot-sell-item col-md-3 col-sm-6 sell-item-new"><div><img style="width:240px;height:240px;" src="' +
                        item.pic + '" alt=""></div><p class="sell-item-name">' +
                        item.title + '</p><div class="buy-box"><a target="_blank" href="' + item.urlAddress + '" class="sell-item-buy ">Click to buy</a></div></li>';
                    arr.push($(aaaa));
                    str.push(item.title);
                });
                ssstnum = ssstnum + 4;
                if (aaaa != "")
                {
                    for (var i = 0; i < arr.length; i++) {
                        $(arr[i]).find('.sell-item-name').attr('title',str[i])
                      $("#hot-sell-box").append(arr[i]);
                    }
                    
                }
                else
                {
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
            var arr = []
            var data = JSON.parse(data);
    
            $.each(data, function (i, item) {
                var str = item.title;
                
                aaaa = "<li class='hot-sell-item col-md-3 col-sm-6'>" +
                    "<div><img style='width: 240px; height: 240px;' src=" + item.pic + " alt=''></div>" +
                    "<p class='sell-item-name'>" + item.title + "</p>" +
                    "<div class='buy-box'>" +
                    "<a target='_blank' href='" + item.urlAddress + "' class='sell-item-buy'>Click to buy</a>" +
                    "</div>" +
                    "</li>";
                arr.push($(aaaa).find('.sell-item-name').attr('title', str).parent())
            });
            for (var i = 0; i < arr.length; i++) {
                $("#hot-sell-box").append(arr[i]);
            }
            
        }
    });
}
