$(function () {
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
                aaaa += "<dl>" +
                    "<dt>" +
                    "<a target='_blank' href='/News/NewsInfo/?id=" + item.Id + "'><img src='/upload/Information/" + item.Id + "/" + item.Photo + "'/></a>" +
                    "</dt>" +
                    "<dd>" +
                    "<p class='info'>" + item.Title + "</p>" +
                    "<p class='date'>" + item.CreationDate.slice(0, item.CreationDate.indexOf('T'))+ "</p>" +
                    "<p class='xian'></p>" +
                    "</dd>" +
                    "</dl>"
            });
            $("#newsDocker").html(aaaa);
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
                                aaaa += "<dl>" +
                                    "<dt>" +
                                    "<a target='_blank' href='/News/NewsInfo/?id=" + item.Id + "'><img src='/upload/Information/" + item.Id + "/" + item.Photo + "'/></a>" +
                                    "</dt>" +
                                    "<dd>" +
                                    "<p class='info'>" + item.Title + "</p>" +
                                    "<p class='date'>" + new Date(item.CreationDate).Format("yyyy-MM-dd") + "</p>" +
                                    "<p class='xian'></p>" +
                                    "</dd>" +
                                    "</dl>"
                            });
                            $("#newsDocker").html(aaaa);
                        }
                    });
                }
            });
        }
    });
}

Date.prototype.Format = function (fmt) { //author: meizz 
    var o = {
        "M+": this.getMonth() + 1, //月份 
        "d+": this.getDate(), //日 
        "h+": this.getHours(), //小时 
        "m+": this.getMinutes(), //分 
        "s+": this.getSeconds(), //秒 
        "q+": Math.floor((this.getMonth() + 3) / 3), //季度 
        "S": this.getMilliseconds() //毫秒 
    };
    if (/(y+)/.test(fmt)) fmt = fmt.replace(RegExp.$1, (this.getFullYear() + "").substr(4 - RegExp.$1.length));
    for (var k in o)
        if (new RegExp("(" + k + ")").test(fmt)) fmt = fmt.replace(RegExp.$1, (RegExp.$1.length == 1) ? (o[k]) : (("00" + o[k]).substr(("" + o[k]).length)));
    return fmt;
}