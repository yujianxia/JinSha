$(function() {
  var window_temp = $(window)
  var aside_box = $("#aside-box")
  var bodyHeight = $(document.body).outerHeight(true)
  window_temp.scroll(function() {
    var scrollValue = window_temp.scrollTop();
    scrollValue > 180 ? aside_box.css("top", "80px") : aside_box.css("top", "312px");
    scrollValue > bodyHeight - 1200 ? aside_box.fadeOut(200) : aside_box.fadeIn(200);
  });
  //回到顶部
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