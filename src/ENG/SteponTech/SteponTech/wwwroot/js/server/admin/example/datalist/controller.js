'use strict';

define(['app', 'domReady!'], function (app) {

    //定义控制器
    var controller = function ($scope, $routeParams, $http) {
        //设置菜单
        OpenMenu('example_datalist');
        renderControl();

        $("#pager").pageButton({
            pageIndex: 0,
            pageSize: "pageSize",
            numCount: 5,
            showFirstLast: true,
            showPrevNext: true,
            searchBtn: "searchBtn",
            onPageChange: function (callback) {
                var search = {
                    Index: this.pageIndex,
                    Size: this.pageSize,
                    Name: "ArticleInfo", //指定要获取的实体资源名称，如果是采用自定义的处理类，则Nothing需指定
                    Condition: {
                        Collection: [
                            //注意这里的Condition是个对象，Collection才是集合
                            //F表示字段，O表示操作符，P表示参数名称，加两个ATA符号是因为MVC模板原因，V表示值，N表示不进行验证的值
                            //这里说明下这个的用法，首先，操作符支持=,>,<,<=,>=,%,!=,#，其中%表示模糊匹配，#表示不为空
                            //如果在参数名称（P）的值设置"#@@Name"表示后台自动生成查询语句时，排除该参数对应的数据，这时，就可以手动从代码进行处理                              
                            { F: "Title", O: "%", P: "?Title", V: $("#name").val() },
                            { F: "Source", O: "%", P: "?Source", V: $("#source").val() },
                            { F: "IsHomeShow", O: "=", P: "?IsHomeShow", V: $("#isHomeShow").val(), N: "-1" }
                        ]
                    }
                };

                $http({
                    method: 'Post',
                    url: '/api/DataSearch',
                    data: search
                }).success(function (data) {
                    if (data && data.data) {
                        $scope.list = data.data;
                    }
                    callback(data.total);
                });
            }
        });

        $scope.delete = function (id) {
            easyConfirm("确认删除？", "此操作将不可逆转，数据将永久删除", "取消", "是的，删除", function (isConfirm) {
                if (isConfirm) {
                    swal("操作提示!", "数据删除成功", "success");
                    //easyGritter(id + " 的信息已经删除", "success");
                }
            });
        }
    };

    //注册控制器
    app.register.controller('ExampleDataListController', ['$scope', '$routeParams', '$http', controller]);

});