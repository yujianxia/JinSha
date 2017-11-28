$(function () {
    $.ajaxSetup({ cache: false });
    huodongDeatil();
	//	头部menu
	$(document).ready(function() {
		// 图片
		$(".hover-box div img").click(function() {
			$(".pic-box>div img").attr('src', this.src);
			var index = $(".hover-box div img").index($(this));
			$(".text").eq(index).show().siblings(".text").hide()
		})
		//  切换
		$(".title>div").click(function() {
			var index = $(".title>div").index($(this));
			$(this).addClass("active").siblings("div").removeClass("active");
			$(".shows-box").eq(index).show().siblings(".shows-box").hide();
		});
		//报名
        $(".btn_baoming").on("click", function () {
            baoming();
			//$("#mask").show();
		})
		//关闭弹窗
		$("#remove").on("click", function() {
			$("#mask").hide();
			$("form")[0].reset();
		})
		//表单判断
		$("#sure").on("click",function(){
			var _name = $("#name").val();
			var _number = $("#number").val();
			var _tel = $("#tel").val();
			//姓名验证
			if(_name == ''){
				$("#errMsg").html("请输入姓名");
				$("#name").css({'border':"1px solid red"});
				return false;
			}
			if(_number == ''){
				$("#errMsg").html("请输入报名人数");
				$("#number").css({'border':"1px solid red"});
				return false;
			}
			if(_tel == ''){
				$("#errMsg").html("请输入联系电话");
				$("#tel").css({'border':"1px solid red"});
				return false;
			}
		})
		$(".form input").on("focus",function(){
			$("#errMsg").html("");
			$(this).css({"border":'1px solid #89785d'});
		})
		//回到顶部
		showScroll();
		//回到顶部
		function showScroll() {
			$(window).scroll(function() {
				var scrollValue = $(window).scrollTop();
				scrollValue > 500 ? $('.toTop').fadeIn(300) : $('.toTop').fadeOut(200);
			});
			$('.toTop').click(function() {
				$("html, body").animate({
					scrollTop: 0
				}, 200);
			});
		}
	});
})


function huodongDeatil() {
    var id = $("#actitvtyid").attr("tag");
    $.ajax({
        url: '/api/Members/ActivityInformationById/1/1/' + id + '',
        type: 'GET',
        dataType: "text",
        contentType: "application/json",
        success: function (data) {
            var result = JSON.parse(data);
            var data = JSON.parse(result.data);
            var item = data.body.dataTable.dataRow["0"].column;
            $("#detailtitle").text(item[4].value);
            $("#actitvtyspan").text(item[3].value + "/" + item[2].value);
            $("#actitvtytime").text(datetime(Number(item[5].value)) + "~" + datetime(Number(item[6].value)));
            $("#actitvtyimg").attr('src', item[8].value);
            $("#actitvtycontent").append(item[10].value);
            if (Number(Number(item[6].value)) < new Date().getTime()) {
                $("#baominga").hide();
            }
        }
    });
}

function baoming()
{
    var id = $("#actitvty").attr("tag");
    $.ajax({
        url: '/api/Members/ActivitiesAppointment/1/' + id + '/官网预约',
        type: 'POST',
        dataType: "text",
        contentType: "application/json",
        success: function (data) {
            if (data.code == 4) {
                //尚未登录
                window.location("/Vip/Login")
            }
            else if (data.code == 1) {
                alert("预约成功!");
                //$('.success-apply').show();
            }
            else {
                //预约失败
                alert("预约失败！请重试");
                //$('.success-apply').show().find("img").attr({ "src": "/img/common/fail.png" });
            }
        }
    });
}
function datetime(date) {
    var str = new Date(date);
    var result = str.getFullYear() + "年" + str.getMonth() + "月" + str.getDay() + "日 ";
    return result;
}