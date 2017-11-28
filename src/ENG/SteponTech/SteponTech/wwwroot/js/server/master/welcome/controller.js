'use strict';

define(['app', 'domReady!'], function (app) {

    //定义控制器
    var controller = function ($scope) {
        //设置菜单
        OpenMenu('master_welcome');
        renderControl();
    };

    //注册控制器
    app.register.controller('MasterWelcomeController', ['$scope', controller]);

});