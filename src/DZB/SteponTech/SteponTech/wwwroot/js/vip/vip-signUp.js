$(function () {

    $.ajaxSetup({ cache: false });
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

var errorArr = [false, false, false, false, false, false]
function testReg(abc) {
    // inputs
    var phone = $('#phone');
    var email = $('#email');
    var nickname = $('#nickname');
    var username = $('#username');
    var password = $('#password');
    var inputs = [phone, email, nickname, username, password];
    //正则
    var regPhone = /^1[3-8]\d{9}$/;
    var regEmail = /^([a-zA-Z0-9]+[_|\_|\.]?)*[a-zA-Z0-9]+@([a-zA-Z0-9]+[_|\_|\.]?)*[a-zA-Z0-9]+\.[a-zA-Z]{2,3}$/;
    var nickName = /^.{1,}$/;
    var userName = /^.{1,}$/;
    var passWord = /^.{1,}$/;
    var regs = [regPhone, regEmail, nickName, userName, passWord];
    if (regs[abc].test(inputs[abc].val())) {
        if (abc == 5 && inputs[4].val() === inputs[5].val()) {
            errorArr[abc] = true;
        } else {
            errorArr[abc] = false;
        }
        errorArr[abc] = true;
        inputs[abc].next("p").css({ "visibility": "hidden" });
    } else {
        // 提示错误信息
        switch (abc) {
            case 0:
                errorArr[0] = false;
                $("#Phone").css({ "visibility": "visible" });
                break;
            case 1:
                errorArr[1] = false;
                $("#Email").css({ "visibility": "visible" });
                break;
            case 2:
                errorArr[2] = false;
                $("#nickName").css({ "visibility": "visible" });
                break;
            case 3:
                errorArr[3] = false;
                $("#Name").css({ "visibility": "visible" });
                break;
            case 4:
                errorArr[4] = false;
                $("#Pwd").css({ "visibility": "visible" });
                again()
                break;
            default:
                break;
        }
    }
}
function again() {
    var password11 = $('#password').val();
    var password22 = $('#password2').val();
    if (password11 != "" && password22 != "" && password22 == password11) {
        errorArr.splice(5, 1, true)
        $("#againPwd").css({ "visibility": "hidden" });
    } else {
        errorArr.splice(5, 1, false)
        $("#againPwd").css({ "visibility": "visible" });
    }
    console.log(errorArr)
}

$("#signUp-btn").click(function () {
    again() 
    var bool = true;
    for (var i = 0; i < errorArr.length; i++) {
        if (!errorArr[i]) {
            bool = false
            break
        }
    }
    if (bool) {
        var parameter = {
            nickname: $("#nickname").val(),
            phone: $("#phone").val(),
            user_name: $("#username").val(),
            email: $("#email").val(),
            register_from: 2,
            password: $("#password").val(),
            token: ""
        };
        $.ajax({
            type: "post",
            url: "/api/Members/AddMembers",
            data: JSON.stringify(parameter),
            contentType: "application/json",
            success: function (res) {
                if (res.code == 1) {
                    alert("注册成功！");
                    setTimeout("window.location.href= '/Vip/Login' ", 1);
                }
                else {
                    alert(res.message);
                }
            }
        });
    } else {
        alert("注册失败")
    }
});
