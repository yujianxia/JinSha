'use strict';

define(['app', 'domReady!', 'master/empty/service'], function (app) {

    //定义控制器
    var controller = function ($scope, $routeParams, masterEmptyService) {
        //设置菜单
        OpenMenu('master_empty');
        renderControl();
    };

    //注册控制器
    app.register.controller('MasterEmptyController', ['$scope', '$routeParams', 'masterEmptyService', controller]);

});