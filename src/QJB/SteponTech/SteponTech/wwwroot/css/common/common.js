function showToast(str){
	var inner = str || ""
	var html = "<div class='loadMask'><div class='toast'><div class='load-gif'></div><h3>"+inner+"</h3></div></div>";
	$(document.body).append(html);
};
function hideToast(){
	$(".loadMask").remove();
}
isLogin();
function isLogin() {
    $.ajax({
        url: "/api/LoginAuthorize/IsBusLogin",
        type: "get",
        dataType:"json",
        success: function (data) {
            var code = data.code;
            if (code == "1") {
                $(".loginoptrator").hide();
                $(".infoOptrator").show();
            }
        }
    })
}
function loginOut() {
    $.ajax({
        url: "/api/Members/MembersLoginout",
        type: "get",
        dataType: "json",
        success: function (data) {
            $(".loginoptrator").show();
            $(".infoOptrator").hide();
            window.location.reload();
        }
    })
}
$(".loginoptrator").hover(function () {
    $(".signIn").show();
}, function () {
    $(".signIn").hide();
    })
$(".infoOptrator").hover(function () {
    $(".myInfo").show();
    $(".loginOut").show();
}, function () {
    $(".myInfo").hide();
    $(".loginOut").hide();
    });
$(".loginOut").on("click", loginOut)
///LoginAuthorize/IsBusLogin GET



$(function () {
    $('.versions').find('a').eq(0).attr('href', 'http://120.25.240.32:8800/');
    $('.versions').find('div').find('a').eq(0).attr('href', 'http://120.25.240.32:8000/');
    $('.versions').find('div').find('a').eq(1).attr('href', 'http://118.114.244.3:8014/');
})()
