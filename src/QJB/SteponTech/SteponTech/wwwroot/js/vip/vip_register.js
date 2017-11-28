$(function () {
    //表单验证。
    $("#nick-name").on("blur", function () {
        var _val = $(this).val();
        if (_val == '') {
            $(this).siblings("i").html("昵称不能为空").css({ "display": "inline-block" });
            return false;
        }
        if (!(/^.{1,18}$/.test(_val))) {
            $(this).siblings("i").html("昵称长度为1-18位").css({ "display": "inline-block" });
            return false;
        } else {
            $(this).siblings("i").html("").hide();
        }
    });
    //会员姓名
    $("#vip-name").on("blur", function () {
        var _val = $(this).val();
        if (_val == '') {
            $(this).siblings("i").html("").hide();
            return false;
        }
    });
    //手机号码验证
    $("#tel").on("blur", function () {
        var _val = $(this).val();
        if (_val == '') {
            $(this).siblings("i").html("手机号码不能为空").css({ "display": "inline-block" });
            return false;
        }
        if (!(/^1(3|4|5|7|8)\d{9}$/.test(_val))) {
            $(this).siblings("i").html("手机号码有误，请重填").css({ "display": "inline-block" });
            return false;
        } else {
            $(this).siblings("i").html("").hide();
        }
    });
    //邮箱地址验证
    $("#email").on("blur", function () {
        var _val = $(this).val();
        if (_val == '') {
            $(this).siblings("i").html("邮箱地址不能为空").css({ "display": "inline-block" });
            return false;
        }
        if (!(/^([0-9A-Za-z\-_\.]+)@([0-9a-z]+\.[a-z]{2,3}(\.[a-z]{2})?)$/g.test(_val))) {
            $(this).siblings("i").html("邮箱地址有误，请重填").css({ "display": "inline-block" });
            return false;
        } else {
            $(this).siblings("i").html("").hide();
        }
    });
    //密码格式验证
    $("#psd-1").on("blur", function () {
        var _val = $(this).val();
        if (_val == '') {
            $(this).siblings("i").html("密码不能为空").css({ "display": "inline-block" });
            return false;
        }
        if (!(/^.{6,18}$/.test(_val))) {
            $(this).siblings("i").html("密码格式有误，请重填").css({ "display": "inline-block" });
            return false;
        } else {
            $(this).siblings("i").html("").hide();
        }
    });
    //再次确认密码
    $("#psd-2").on("blur", function () {
        var _val = $(this).val();
        var befor_pwd = $("#psd-1").val();
        if (_val == '') {
            $(this).siblings("i").html("确认密码不能为空").css({ "display": "inline-block" });
            return false;
        }
        if (_val != befor_pwd) {
            $(this).siblings("i").html("两次密码不一致，请重填").css({ "display": "inline-block" });
            return false;
        } else {
            $(this).siblings("i").html("").hide();
        }
    })
    //register
    $("#registrationbtn").on("click", function () {
        //是否通过验证
        var flag = true;
        var _tel = $("#tel").val();
        var _email = $("#email").val();
        var _password = $("#password").val();
        var _input = $('.user input');
        _input.map(function (index, val) {
            if ($(val).val() == '') {
                if ($(val).attr("id") == 'vip-name') {

                }
                else {
                    $(val).siblings("i").html("请输入有效内容").css({ "display": "inline-block" });
                    flag = false;
                    return false;
                }
            }
            if ($(val).siblings("i").is(":visible")) {
                flag = false;
                return false;
            }
        });
        if (flag) {
            var parameter = {
                nickname: $("#nick-name").val(),
                phone: $("#tel").val(),
                user_name: $("#vip-name").val(),
                email: $("#email").val(),
                register_from: 2,
                password: $("#psd-1").val(),
                token:""
            };
            $.ajax({
                type: "post",
                url: "/api/Members/AddMembers",
                data: JSON.stringify(parameter),
                contentType: "application/json",
                beforeSend:showToast(),
                success: function (res) {
                    if (res.code == 1)
                    {
                        hideToast();
                        $("#errorspan").text("注册成功！即将跳转到登录！");
                        setTimeout("this.location.href= '/Member/Login' ", 2000);
                    }
                    else
                    {
                        hideToast();
                        $("#errorspan").text("注册失败！请重试！");
                    }
                }
            });
        }
    });
});