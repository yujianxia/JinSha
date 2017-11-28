$(function () {
    $.ajaxSetup({ cache: false });
    var page = 0;
    $.ajax({
        url: '/api/Search/' + $("#searchstring").val() + ',' + page + '',
        type: 'Post',
        dataType: "text",
        contentType: "application/json",
        success: function (data) {
            var data = JSON.parse(data);
            $('#countspan').text(data.count);
            if (data.data.length == 20) {
                $("#downup").css("visibility", "visible");
            }
            for (var key in data.data)
            {
                var value = data.data[key].value.split('*');
                $("#searchresult").append('<div class="notice row">' +
                    '<a class="notice-cell" target="_blank" href="' + data.data[key].key + '">' +
                    '<p class="col-sm-8"><span class="notice-span">' + value[0] + '</span></p>' +
                    '<div class="col-sm-4"><span class="time-span">' + value[1] + '</span></div>' +
                    '</a>' +
                    '</div>');
            }
            page++;
        }
    });


    $("#downup").on("click", function () {
        $.ajax({
            url: '/api/Search/' + $("#searchstring").val() + ',' + page+'',
            type: 'Post',
            dataType: "text",
            contentType: "application/json",
            success: function (data) {
                var data = JSON.parse(data);
                $('#countspan').text(data.count)
                $("#downup").css("visibility", "hidden");
                if (data.data.length == 20) {
                    $("#downup").css("visibility", "visible");
                }
                for (var key in data.data) {
                    var value = data.data[key].value.split('*');
                    $("#searchresult").append('<div class="notice row">' +
                        '<a class="notice-cell" target="_blank" href="' + data.data[key].key + '">' +
                        '<p class="col-sm-8"><span class="notice-span">' + value[0] + '</span></p>' +
                        '<div class="col-sm-4"><span class="time-span">' + value[1] + '</span></div>' +
                        '</a>' +
                        '</div>');
                }
                page++;
            }
        });
    })

    $("#searchstring").keyup(function (e) {
        if (e.keyCode == 13) {
            window.location.href = "/JinSha/SearchResults?searchstring=" + this.value + ""
        }
    });
    $("#searchBtn").on("click", function () {
        window.location.href = "/JinSha/SearchResults?searchstring=" + $("#searchstring").val() + "";
    })
})
