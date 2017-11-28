$(function(){
    artslist($("#listspanid").val());
})
function artslist(id) {
    var parameter = {
        Index: 0,
        Size: 6,
        Name: '"InformationYoung"',
        OrderBy: '"CreationDate" desc',
        Condition: {
            Collection: [
                { F: '"ColumnId"', O: "=", P: "@ColumnId::uuid", V: id }
            ]
        }
    };
    $.ajax({
        url: '/api/DataSearch',
        type: 'Post',
        dataType: "text",
        contentType: "application/json",
        data: JSON.stringify(parameter),
        success: function (data) {
            var aaaa = "";
            var data = JSON.parse(data);
            $.each(data.data, function (i, item) {
                aaaa += "<a href='/Strategy/QuestionDetails/?id=" + item.Id + "'>" +
                    "<div class='box'>"+
                    "<div class='box-img'><img src='/upload/Information/" + item.Id + "/" + item.Photo + "'/></div>"+
                        "<p class='box-text'>" + item.Title + "</p>"+
                     "</div>" +
                    "</a>"
            });
            $("#questionDocker").html(aaaa);
            $('#pageToolbar').Paging({
                pagesize: 6, count: data.total, toolbar: true, callback: function (page, size, count) {
                    var parameter = {
                        Index: page - 1,
                        Size: 6,
                        Name: '"InformationYoung"',
                        OrderBy: '"CreationDate" desc',
                        Condition: {
                            Collection: [
                                { F: '"ColumnId"', O: "=", P: "@ColumnId::uuid", V: id }
                            ]
                        }
                    };
                    $.ajax({
                        url: '/api/DataSearch',
                        type: 'Post',
                        dataType: "text",
                        contentType: "application/json",
                        data: JSON.stringify(parameter),
                        success: function (data) {
                            var aaaa = "";
                            var data = JSON.parse(data);
                            $.each(data.data, function (i, item) {
                                aaaa += "<a href='/Strategy/QuestionDetails/?id=" + item.Id + "'>" +
                                    "<div class='box'>" +
                                    "<div class='box-img'><img src='/upload/Information/" + item.Id + "/" + item.Photo + "'/></div>" +
                                    "<p class='box-text'>" + item.Title + "</p>" +
                                    "</div>" +
                                    "</a>"
                            });
                            $("#questionDocker").html(aaaa);
                        }
                    });
                }
            });
        }
    });
}