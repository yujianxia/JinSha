$(function () {
	// 让aside消失
  var window_temp = $(window)
  var aside_box = $("#aside-box")
  var bodyHeight = $(document.body).outerHeight(true)
  window_temp.scroll(function() {
    var scrollValue = window_temp.scrollTop();
    scrollValue > 180 ? aside_box.css("top", "80px") : aside_box.css("top", "312px");
    scrollValue > bodyHeight - 1000 ? aside_box.fadeOut(200) : aside_box.fadeIn(200);
  });
})
