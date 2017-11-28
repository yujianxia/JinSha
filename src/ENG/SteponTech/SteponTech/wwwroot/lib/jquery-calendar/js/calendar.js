// 关于月份： 在设置时要-1，使用时要+1
$(function () {

    $('#calendar').calendar({
        ifSwitch: true, // 是否切换月份
        hoverDate: true, // hover是否显示当天信息
        backToday: true // 是否返回当天
    });

});

;
(function ($, window, document, undefined) {

    var Calendar = function (elem, options) {
        this.$calendar = elem;

        this.defaults = {
            ifSwitch: true,
            hoverDate: false,
            backToday: false
        };

        this.opts = $.extend({}, this.defaults, options);

        // //console.log(this.opts);
    };

    Calendar.prototype = {
        // showHoverInfo: function (obj) { // hover 时显示当天信息
        //   var _dateStr = $(obj).attr('data');
        //   var offset_t = $(obj).offset().top + (this.$calendar_today.height() - $(obj).height()) / 2;
        //   var offset_l = $(obj).offset().left + $(obj).width();
        //   var changeStr = _dateStr.substr(0, 4) + '-' + _dateStr.substr(4, 2) + '-' + _dateStr.substring(6);
        //   var _week = changingStr(changeStr).getDay();
        //   var _weekStr = '';
        //
        //   this.$calendar_today.show();
        //
        //   this.$calendar_today
        //         .css({left: offset_l + 30, top: offset_t})
        //         .stop()
        //         .animate({left: offset_l + 16, top: offset_t, opacity: 1});
        //
        //   switch(_week) {
        //     case 0:
        //       _weekStr = '星期日';
        //     break;
        //     case 1:
        //       _weekStr = '星期一';
        //     break;
        //     case 2:
        //       _weekStr = '星期二';
        //     break;
        //     case 3:
        //       _weekStr = '星期三';
        //     break;
        //     case 4:
        //       _weekStr = '星期四';
        //     break;
        //     case 5:
        //       _weekStr = '星期五';
        //     break;
        //     case 6:
        //       _weekStr = '星期六';
        //     break;
        //   }
        //
        //   this.$calendarToday_date.text(changeStr);
        //   this.$calendarToday_week.text(_weekStr);
        // },

        showCalendar: function () { // 输入数据并显示
            var now = new Date();
            var nowTime = now.getTime();
            var day = now.getDay();
            var oneDayLong = 24 * 60 * 60 * 1000;
            var MondayTime = nowTime - (day - 1) * oneDayLong;
            var monday = new Date(MondayTime);
            var mouth = now.getMonth()
            var enMot = ["Jan", "Feb", "Mar", "Apr", "May", "June", "July", "Aug", "Sept", "Oct", "Nov", "Dec"]
            var self = this;
            var year = dateObj.getDate().getFullYear();
            var month = dateObj.getDate().getMonth() + 1;
            var dateStr = returnDateStr(dateObj.getDate());
            var firstDay = new Date(year, month - 1, 1); // 当前月的第一天
            //闭馆日()
            var col = $("#rightCalendar").attr("data-closedate");
            //除开1、2、7、8月的法定节假日的数组  劳动节，端午节，国庆节3天
            var Holiday = ["0501", "0618", "1001", "1002", "1003"]
            //头部的年月
            this.$calendarTitle_text.text(enMot[mouth] + '  ' + year);
            this.$calendarDate_item.each(function (i) {
                // allDay: 得到当前列表显示的所有天数
                var allDay = new Date(year, month - 1, i + 1 - firstDay.getDay());
                var allDay_str = returnDateStr(allDay);
                //      var DateArr = []
                //      DateArr.push(allDay_str)
                //      //console.log(DateArr)
                //      自己写的判断日期的方法
                //文化活动
                //var cul = "20170805"
                //社教活动
                //var soc = "20170816"
                //志愿者活动
                //var vol = "20170828"
                $(this).text(allDay.getDate()).attr('data', allDay_str);
                if (isInArray(col, allDay.getDate()) && month === allDay.getMonth() + 1) {
                    $(this).attr('class', 'item item-curDay');
                }
                //      else if (cul === allDay_str) {
                //        $(this).attr('class', 'item item-cul');
                //        $(this).css('cursor', 'pointer');
                //        // 文化活动的事件
                //        $(this).on("click", function(){
                //        	alert(+cul)
                //        })
                //      } else if (soc === allDay_str) {
                //        $(this).attr('class', 'item item-soc');
                //        $(this).css('cursor', 'pointer');
                //        //社教活动的事件
                //        $(this).on("click", function(){
                //        	alert(soc)
                //        })
                //      } else if (vol === allDay_str) {
                //        $(this).attr('class', 'item item-vol');
                //        $(this).css('cursor', 'pointer');
                //        // 志愿者活动的事件
                //        $(this).on("click", function(){
                //        	alert(vol)
                //        })
                //      } 
                else if (returnDateStr(firstDay).substr(0, 6) === allDay_str.substr(0, 6)) {
                    $(this).attr('class', 'item item-curMonth');
                } else {
                    $(this).attr('class', 'item');
                }
            });
        },

        renderDOM: function () { // 渲染DOM
            this.$calendar_title = $('<div class="calendar-title"></div>');
            this.$calendar_week = $('<ul class="calendar-week"></ul>');
            this.$calendar_date = $('<ul class="calendar-date"></ul>');
            this.$calendar_today = $('<div class="calendar-today"></div>');
            this.$calendar_footer = $('<div class="calendar-footer"></div>');

            var _titleStr = '<span class="title"></span>';
            var _weekStr = '<li class="item">SUN</li>' +
                '<li class="item">MON</li>' +
                '<li class="item">TUE</li>' +
                '<li class="item">WED</li>' +
                '<li class="item">THU</li>' +
                '<li class="item">FRI</li>' +
                '<li class="item">SAT</li>';
            var _dateStr = '';
            var _dayStr = '<i class="triangle"></i>' +
                '<p class="date"></p>' +
                '<p class="week"></p>';

            for (var i = 0; i < 5; i++) {
                _dateStr += '<li class="item">26</li>' +
                    '<li class="item">26</li>' +
                    '<li class="item">26</li>' +
                    '<li class="item">26</li>' +
                    '<li class="item">26</li>' +
                    '<li class="item">26</li>' +
                    '<li class="item">26</li>';
            }
            var _footerTextStr = '<ul class="textList">' +
                '<li><span class="textCircle circleFour"></span><span>Closed day</span></li>' +
                '</ul>'
            this.$calendar_title.html(_titleStr);
            this.$calendar_week.html(_weekStr);
            this.$calendar_date.html(_dateStr);
            this.$calendar_today.html(_dayStr);
            this.$calendar_footer.html(_footerTextStr);
            this.$calendar.append(this.$calendar_title, this.$calendar_week, this.$calendar_date, this.$calendar_today, this.$calendar_footer);
            this.$calendar.show();
        },

        inital: function () { // 初始化
            var self = this;

            this.renderDOM();
            this.$calendarTitle_text = this.$calendar_title.find('.title');
            this.$backToday = $('#backToday');
            this.$arrow_prev = this.$calendar_title.find('.arrow-prev');
            this.$arrow_next = this.$calendar_title.find('.arrow-next');
            this.$calendarDate_item = this.$calendar_date.find('.item');
            this.$calendarToday_date = this.$calendar_today.find('.date');
            this.$calendarToday_week = this.$calendar_today.find('.week');

            this.showCalendar();

            if (this.opts.ifSwitch) {
                this.$arrow_prev.bind('click', function () {
                    var _date = dateObj.getDate();

                    dateObj.setDate(new Date(_date.getFullYear(), _date.getMonth() - 1, 1));

                    self.showCalendar();
                });

                this.$arrow_next.bind('click', function () {
                    var _date = dateObj.getDate();

                    dateObj.setDate(new Date(_date.getFullYear(), _date.getMonth() + 1, 1));

                    self.showCalendar();
                });
            }

            if (this.opts.backToday) {
                this.$backToday.bind('click', function () {
                    if (!self.$calendarDate_item.hasClass('item-curDay')) {
                        dateObj.setDate(new Date());

                        self.showCalendar();
                    }
                });
            }

            // this.$calendarDate_item.hover(function () {
            //   self.showHoverInfo($(this));
            // }, function () {
            //   self.$calendar_today.css({left: 0, top: 0}).hide();
            // });
        },

        constructor: Calendar
    };

    $.fn.calendar = function (options) {
        var calendar = new Calendar(this, options);

        return calendar.inital();
    };

    // ========== 使用到的方法 ==========

    var dateObj = (function () {
        var _date = new Date();

        return {
            getDate: function () {
                return _date;
            },

            setDate: function (date) {
                _date = date;
            }
        }
    })();

    function isInArray(col, value) {//判断闭馆日
        if (col != null) {
            var arr = col.split(',');
            for (var i = 0; i < arr.length; i++) {
                if (value === parseInt(arr[i])) {
                    return true;
                }
            }
        }
        return false;
    }

    function returnDateStr(date) { // 日期转字符串
        var year = date.getFullYear();
        var month = date.getMonth() + 1;
        var day = date.getDate();

        month = month < 9 ? ('0' + month) : ('' + month);
        day = day < 9 ? ('0' + day) : ('' + day);

        return year + month + day;
    };

    function changingStr(fDate) { // 字符串转日期
        var fullDate = fDate.split("-");

        return new Date(fullDate[0], fullDate[1] - 1, fullDate[2]);
    };

})(jQuery, window, document);
