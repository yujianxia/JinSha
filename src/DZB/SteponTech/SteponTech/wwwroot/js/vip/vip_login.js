$(function () {
	$.ajaxSetup({ cache: false });
    showScroll()
    
});
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
  var errorArr = [false, false]
  function Reg(abc) {
      // inputs
      var phone = $('#username');
      var pwd = $('#inputPassword3');
      var inputs = [phone, pwd];
      //正则
      var regPhone = /^1[3-8]\d{9}$/;
      var regPwd = /^.{1,}$/;
      var regs = [regPhone, regPwd];
      if (regs[abc].test(inputs[abc].val())) {
          errorArr[abc] = true;
          inputs[abc].next("p").css({ "visibility": "hidden" });
      } else {
          // 提示错误信息
          switch (abc) {
              case 0:
                  errorArr[0] = false;
                  $("#phoneP").css({ "visibility": "visible", "color": "red" });
                  break;
              case 1:
                  errorArr[1] = false;
                  $("#pwdP").css({ "visibility": "visible", "color": "red" });
                  break;
              default:
                  break;
          }
      }
  }
  $("#login-btn").click(function () {
      var bool = true;
      for (var i = 0; i < errorArr.length; i++) {
          if (!errorArr[i]) {
              bool = false
              break
          }
      }
      if (bool) {
          $.ajax({
              type: "get",
              url: "/api/Members/" + $("#username").val() + "/" + $("#inputPassword3").val() + "",
              async: true,
              success: function (res) {
                  if (res.code == 1) {
                      window.location.href = "/Vip/Index";
                  }
                  else {
                      alert(res.message);
                  }
              }
          });
      } else {
          alert('登录失败')
      }
  });
  $("body").keydown(function () {
      if (event.keyCode == "13") {//keyCode=13是回车键
          $("#login-btn").click();
      }
  });











      
   


