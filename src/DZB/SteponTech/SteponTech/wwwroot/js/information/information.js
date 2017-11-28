$(function () {
    $.ajaxSetup({ cache: false });
  //回到顶部
    showScroll();
    kuaixun();
    gonggao();
    
    
});
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
  function kuaixun() {
      var parameter = {
          Index: 0,
          Size: 3,
          Name: '"InformationAll"',
          OrderBy: '"IsTop" desc',
          Condition: {
              Collection: [
                  { F: '"ColumName"', O: "=", P: "@ColumName", V: "金沙快讯" }
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

                  aaaa += "<div class='showroom-one row'>" +
                      "<div class='showroom-img col-lg-4 col-md-4 col-sm-12 col-xs-12'>" +
                      "<a target='_blank' href='News?id=" + item.Id + "'>" +
                      "<img src='/upload/Information/" + item.Id + "/" + item.Photo + "' class='img-responsive ccccc' />" +
                      "</a>" +
                      "</div>" +
                      "<div class='showroom-info col-lg-8 col-md-8 col-sm-12 col-xs-12'>" +
                      "<a target='_blank' href='News?id=" + item.Id + "'>" +
                      "<h3>" + item.Title + "</h3>" +
                      "<p class='showroom-time'>发布时间: " + new Date(item.CreationDate).Format("yyyy年MM月dd日") + "</p>" +
                      "<p class='showroom-p'>" +
                      item.Intro +
                      "</p>" +
                      "</a>" +
                      "<a target='_blank' href='News?id=" + item.Id + "'>"  +
                       "<div class='showroom-btn'>查看详情</div>" +
                      "</a > "
                      +
                      "</div>" +
                      "</div>"
              });
              $("#NewsDivDocker").html(aaaa);
              textLen()
              $('.ccccc').each(function () {
                  if (!this.complete || typeof this.naturalWidth == "undefined" || this.naturalWidth == 0) {
                      this.src = '/img/ErrorImg/ErrorImg.png';
                  }
              });
              $('#pageToolbar1').Paging({
                  pagesize: 3, count: data.total, toolbar: true, callback: function (page, size, count) {
                      var parameter = {
                          Index: page - 1,
                          Size: 3,
                          Name: '"InformationAll"',
                          OrderBy: '"IsTop" desc',
                          Condition: {
                              Collection: [
                                  { F: '"ColumName"', O: "=", P: "@ColumName", V: "金沙快讯" }
                              ]
                          }
                      };
                      window.location.href = '/News/Index#showroom'
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
                                  aaaa += "<div class='showroom-one row'>" +
                                      "<div class='showroom-img col-lg-4 col-md-4 col-sm-12 col-xs-12'>" +
                                      "<a target='_blank' href='News?id=" + item.Id + "'>" +
                                      "<img src='/upload/Information/" + item.Id + "/" + item.Photo + "' class='img-responsive ccccc' />" +
                                      "</a>" +
                                      "</div>" +
                                      "<div class='showroom-info col-lg-8 col-md-8 col-sm-12 col-xs-12'>" +
                                      "<a target='_blank' href='News?id=" + item.Id + "'>" +
                                      "<h3>" + item.Title + "</h3>" +
                                      "<p class='showroom-time'>发布时间: " + new Date(item.CreationDate).Format("yyyy年MM月dd日") + "</p>" +
                                      "<p class='showroom-p'>" +
                                      item.Intro +
                                      "</p>" +
                                      "</a>" +
                                      "<div class='showroom-btn'>" +
                                      "<a target='_blank' href='News?id=" + item.Id + "'>查看详情</a>" +
                                      "</div>" +
                                      "</div>" +
                                      "</div>"
                              });
                              $("#NewsDivDocker").html(aaaa);
                              textLen()
                              $('.ccccc').each(function () {
                                  if (!this.complete || typeof this.naturalWidth == "undefined" || this.naturalWidth == 0) {
                                      this.src = '/img/ErrorImg/ErrorImg.png';
                                  }
                              });
                          }
                      });
                  }
              });
          }
      });
  }

  function gonggao() {
      var parameter = {
          Index: 0,
          Size: 6,
          Name: '"InformationAll"',
          OrderBy: '"IsTop" desc',
          Condition: {
              Collection: [
                  { F: '"ColumName"', O: "=", P: "@ColumName", V: "金沙公告" }
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
                  aaaa += "<div class='notice row'>" +
                      "<a target='_blank' class='notice-cell' href='Notice?id=" + item.Id + "'>" +
                      "<p class='col-sm-8'><span class='notice-span'>" + item.Title + "</span></p>" +
                      "<div class='col-sm-4'><span class='time-span'>" + new Date(item.CreationDate).Format("yyyy-MM-dd") + "</span></div>" +
                      "</a>" +
                      "</div>"
              });
              $("#AnnouncementsDivDocker").html(aaaa);
              $('#pageToolbar2').Paging({
                  pagesize: 6, count: data.total, toolbar: true, callback: function (page, size, count) {
                      var parameter = {
                          Index: page - 1,
                          Size: 6,
                          Name: '"InformationAll"',
                          OrderBy: '"IsTop" desc',
                          Condition: {
                              Collection: [
                                  { F: '"ColumName"', O: "=", P: "@ColumName", V: "金沙公告" }
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
                                  aaaa += "<div class='notice row'>" +
                                      "<a target='_blank' class='notice-cell' href='Notice?id=" + item.Id + "'>" +
                                      "<p class='col-sm-8'><span class='notice-span'>" + item.Title + "</span></p>" +
                                      "<div class='col-sm-4'><span class='time-span'>" + new Date(item.CreationDate).Format("yyyy-MM-dd") + "</span></div>" +
                                      "</a>" +
                                      "</div>"
                              });
                              $("#AnnouncementsDivDocker").html(aaaa);
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
function textLen() {
    //限制个数
    var limited = 140;
    //处理后的字符串
    var afterText = "";
    var text = $(".showroom-p")
    text.map(function (index, value) {
        var _text = $.trim($(value).text());
        if (_text.length >= limited) {
            afterText = _text.slice(0, limited);
            afterText += "...";
        } else {
            afterText = _text;
        }
        $(value).text(afterText);
    })
}
