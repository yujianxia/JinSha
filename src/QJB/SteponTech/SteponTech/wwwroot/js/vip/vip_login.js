$(function(){
	//手机号码验证
    $("#tel").on("blur",function(){
    	var _val = $(this).val();
    	if(_val == ''){
    		$(this).siblings("i").html("手机号码不能为空").css({"display":"inline-block"});
    		return false;
    	}
    	if(!(/^1(3|4|5|7|8)\d{9}$/.test(_val))){ 
	        $(this).siblings("i").html("手机号码有误，请重填").css({"display":"inline-block"});
	        return false; 
	    }else{
	    	$(this).siblings("i").html("").hide();
	    }
    });
    $("#psd").on("blur",function(){
    	var _val = $(this).val();
    	if(_val == ''){
    		$(this).siblings("i").html("密码不能为空").css({"display":"inline-block"});
    		return false;
    	}
        if (!(/^.{6,}$/.test(_val))){ 
	        $(this).siblings("i").html("密码格式有误，请重填").css({"display":"inline-block"});
	        return false; 
	    }else{
	    	$(this).siblings("i").html("").hide();
	    }
    });
    

    $("#submit").on("click", function () {
        //是否通过验证
        var flag = true;
		var _tel = $("#tel").val();
		var _password = $("#psd").val();
    	var _input = $('form input');
    	_input.map(function(index,val){
    		if($(val).val() == ''){
    			$(val).siblings("i").html("请输入有效内容").css({"display":"inline-block"});
    			flag = false;
    			return false;
    		}
    		if($(val).siblings("i").is(":visible")){
    			flag = false;
    			return false;
    		}
    	});
        if (flag) {
			$.ajax({
                type: "get",
                url: "/api/Members/" + $("#tel").val() + "/" + $("#psd").val()+"",
                async: true,
                //显示loading；
                beforeSend: showToast(),
                success: function (res) {
                    //请求成功隐藏loading
                    hideToast();
                    if (res.code == 1) {
                        window.location.href = "/VIP/Index";
                    }
                    else
                    {
                        console.log(res);
                        $("#loginMsg").show().find("p").html(res.message);
                    }
				}
			});
		}
    	return false;
    })

    
})