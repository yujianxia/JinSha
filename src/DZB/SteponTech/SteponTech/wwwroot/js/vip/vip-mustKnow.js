$(function() {
  $(".check-box").click(function() {
    $(".myIconfont").toggleClass("hide");
  });
  // JS实现跳转
  $(".submit-btn").click(function() {
    window.location.href = "../../pages/vip/vip-signUp.html";
  });
      showScroll()
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
