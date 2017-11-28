"use strict";

define(["app", "domReady!"],
    function (app) {

        //定义控制器
        var controller = function ($scope, $routeParams, $http) {
            //设置菜单
            OpenMenu("example_form");

            renderControl();

            //https://github.com/summernote/angular-summernote 简易富文本编辑器的使用参考这里

            //初始化值
            $scope.article = {};
            $scope.article.Type = "1";

            //设置富文本编辑器的字体
            $scope.summernoteoptions = {
                fontNames: [
                    "SimHei", "SimSun", "Microsoft YaHei", "KaiTi", "STHupo", "STLiti", "STXingkai", "STXinwei",
                    "STHeiti", "STSong", "STFangsong"
                ]
            };

            //手动增加对日期控件的验证
            $("#publishDate")
                .datetimepicker({
                    locale: "zh-cn",
                    format: "YYYY-MM-DD"
                })
                .on("dp.change",
                    function (e) {
                        $scope.$apply(function () {
                            if (e.date !== null)
                                $scope.article.PublishDate = e.date.format("YYYY-MM-DD");
                        });
                    });

            //手动增加对NG数据的绑定
            $("#category")
                .on("change",
                    function () {
                        $scope.$apply(function () {
                            $scope.article.Type = $("#category").val();
                        });
                    });

            $scope.submit = function () {
                $scope.article.Color = $("#color").val();
                //这里POST你的数据吧
                $http({
                    method: 'Post',
                    url: '/Admin/Example/Form',
                    data: $scope.article
                }).success(function (data) {
                    if (data && data.success) {
                        swal("提示", data.message, "success");
                    } else {
                        swal("提示", data.message, "error");
                    }
                });
            };
            $scope.goback = function () {
                easyGritter("这里是模拟，可以是返回或者是重置表达", "info");
            };
        };

        //注册控制器
        app.register.controller("ExampleFormController", ["$scope", "$routeParams", "$http", controller]);

    });