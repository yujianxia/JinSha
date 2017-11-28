'use strict';

define(['app'], function (app) {

    //定义服务
    var service = function ($http) {
        var factory = {};

        return factory;
    };
    app.register.service('masterEmptyService', ['$http', service]);
});