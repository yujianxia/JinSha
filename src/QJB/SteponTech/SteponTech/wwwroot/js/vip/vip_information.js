$(function () {
    $.ajax({
        url: '/api/Members/MembersSelect',
        type: 'GET',
        dataType: "text",
        contentType: "application/json",
        success: function (data) {
            var result = JSON.parse(data);
            var data = JSON.parse(result.data);
            $("#mycard").text(data.body.dataTable.dataRow[0].column[5].value);
            $("#mylevel").text(data.body.dataTable.dataRow[0].column[6].value);
            $("#myscore").text(data.body.dataTable.dataRow[0].column[7].value);
            $("#mygroup").text(data.body.dataTable.dataRow[0].column[12].value);
            $("#myname").text(data.body.dataTable.dataRow[0].column[1].value);
            $("#name").val(data.body.dataTable.dataRow[0].column[13].value);
            $("#tel").val(data.body.dataTable.dataRow[0].column[2].value);
            $("#email").val(data.body.dataTable.dataRow[0].column[4].value);
            $("#addr").val(data.body.dataTable.dataRow[0].column[3].value);
        }
    });


	//修改
	$("span.modify").on("click",function(){
		$(this).siblings("input").css({border:"1px solid #aa3d38","border-radius": "5px"}).removeAttr("readonly")
	});
	//号码验证
	$("#name").on("blur",function(){
    	var _val = $(this).val();
    	if(_val == ''){
    		$(this).siblings("i").html("昵称不能为空").css({"display":"inline-block	"});
    		return false;
    	}
    	if(!(/^[0-9a-zA-Z\u4e00-\u9fa5_]{2,16}$/.test(_val))){ 
	        $(this).siblings("i").html("只能是2-16位汉字、数字、字母（大小写）、下划线").css({"display":"inline-block	"});
	        return false; 
	    }else{
	    	$(this).siblings("i").html("").hide();
	    }
    });
	$("#tel").on("blur",function(){
    	var _val = $(this).val();
    	if(_val == ''){
    		$(this).siblings("i").html("手机号码不能为空").css({"display":"inline-block	"});
    		return false;
    	}
    	if(!(/^1(3|4|5|7|8)\d{9}$/.test(_val))){ 
	        $(this).siblings("i").html("手机号码有误，请重填").css({"display":"inline-block	"});
	        return false; 
	    }else{
	    	$(this).siblings("i").html("").hide();
	    }
    });
    //邮箱地址验证
    $("#email").on("blur",function(){
    	var _val = $(this).val();
    	if(_val == ''){
    		$(this).siblings("i").html("邮箱地址不能为空").css({"display":"inline-block	"});
    		return false;
    	}
    	if(!(/^([0-9A-Za-z\-_\.]+)@([0-9a-z]+\.[a-z]{2,3}(\.[a-z]{2})?)$/g.test(_val))){ 
	        $(this).siblings("i").html("邮箱地址有误，请重填").css({"display":"inline-block	"});
	        return false; 
	    }else{
	    	$(this).siblings("i").html("").hide();
	    }
    });
//	save
	$('.save').on("click",function(){
		//是否通过验证
    	var flag = true;
    	//电话
		var _tel = $("#tel").val();
		//邮箱
		var _email = $("#email").val();
		//昵称
		var _name = $("#name").val();
		//地址
        var _address = $("#addr").val();
        var _input = $('.informationFrom input');
    	_input.map(function(index,val){
    		if($(val).val() == ''){
    			$(val).siblings("i").html("请输入有效内容").css({"display":"inline-block"});
    			flag = false;
    			return false;
            }
    		if($(val).siblings("i").is(":visible")){
                flag = false;
                console.log(1)
    			return false;
    		}
    	});
		if(flag){
            var parameter = {
                nickname: _name,
                phone: _tel,
                user_name: $("#myname").text(),
                email: _email,
                address: _address
            };
			$.ajax({
				type:"post",
                url: "/api/Members/UpdateMembers",
                data: JSON.stringify(parameter),
                async: true,
                contentType: "application/json",
                beforeSend: showToast(),
                success: function (res) {
                    location.reload();
				}
			});
		}
    	return false;
	})
})