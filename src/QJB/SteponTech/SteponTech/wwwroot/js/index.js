;
(function ($) {
    //显示弹窗
    $("#images1 a").on("click", function () {
        var id = $(this)[0].id;
        $.ajax({
            url: '/api/GetInfo/GetDetails/' + id,
            type: 'Get',
            dataType: "json",
            contentType: "application/json",
            success: function (data) {
                var title = data.title;
                var intro = data.intro;
                var photo = data.photo;
                var src = "/upload/Information/" + id + "/" + photo;
                $("#titleId").html(title);
                $("#introId").html(intro);
                $("#photoId").attr({ "src": src }); 
                $(".alert").show();
            }
        });
    })
    $(".close").on("click", function () {
        $(".alert").hide();
    })
})(jQuery)