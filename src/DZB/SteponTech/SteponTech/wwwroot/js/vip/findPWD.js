var errorArr = [false,false]
	  function Reg(abc) {
     	
     	    // inputs
    var phone =  $('#phone');
    var email =  $('#email');
    var inputs = [phone, email];
    //正则
    var regPhone = /^1[3-8]\d{9}$/;
    var regEmail = /^([a-zA-Z0-9]+[_|\_|\.]?)*[a-zA-Z0-9]+@([a-zA-Z0-9]+[_|\_|\.]?)*[a-zA-Z0-9]+\.[a-zA-Z]{2,3}$/;
    var regs = [regPhone, regEmail];
     if (regs[abc].test(inputs[abc].val())) {
            	errorArr[abc] = true;
            	inputs[abc].next("p").css({"visibility":"hidden"});
            }else{
                // 提示错误信息
                switch (abc) { 
                case 0:
                    errorArr[0] = false;
                    $("#phoneP").css({"visibility":"visible","color":"red"});
                    break;
                case 1:
                errorArr[1] = false;
                    $("#emailP").css({"visibility":"visible","color":"red"});
                    break;
                default:
                    break;
                }   
     	}
            }

      $("#login-btn").click(function () {
          $.ajaxSetup({ cache: false });
		var bool = true;
		for (var i = 0; i < errorArr.length; i++) {
			if (!errorArr[i]) {
				bool = false
				break
			}
        }
        if (bool) {
            var parameter = {
                phone: $("#phone").val(),
                email: $("#email").val()
            };
            $.ajax({
                type: "POST",
                contentType: "application/json",
                url: "/api/Members/ForgotPassword",
                data: JSON.stringify(parameter),
                async: true,
                success: function (res) {
                    if (res.code == 1) {
                        var data = JSON.parse(res.data);
                        alert("重置后的密码：" + data.body.dataTable.dataRow["0"].column["0"].value);
                    }
                    else {
                        alert(res.message);
                    }
                    
                }
            });
		}else{
			alert("字段填写错误")
		}
	});

$("body").keydown(function () {
    if (event.keyCode == "13") {//keyCode=13是回车键
        $("#login-btn").click();
    }
});
   