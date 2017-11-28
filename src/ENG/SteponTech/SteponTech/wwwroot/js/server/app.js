"use strict";

define(["emodule", "routeResolver"],
    function(extender) {
        var app = angular.module("so", ["ngRoute", "routeResolverService", "summernote"]);
        extender.decorate(app, true);

        app.config([
            "$routeProvider", "routeResolverProvider", "$controllerProvider", "$compileProvider", "$filterProvider",
            "$provide",
            function($routeProvider,
                routeResolverProvider,
                $controllerProvider,
                $compileProvider,
                $filterProvider,
                $provide) {

                app.register = {
                    controller: $controllerProvider.register,
                    directive: $compileProvider.directive,
                    filter: $filterProvider.register,
                    factory: $provide.factory,
                    service: $provide.service
                };

                flowFactoryProvider.defaults = {
                    chunkSize: 1024 * 1024 * 1024,
                    target: "/api/UploadFile/UploadFileAsync",
                    permanentErrors: [404, 500, 501],
                    maxChunkRetries: 1,
                    chunkRetryInterval: 5000,
                    simultaneousUploads: 1,
                    testChunks: false
                };

                //用于动态注入模块
                app._lazyProviders = {
                    $compileProvider: $compileProvider,
                    $controllerProvider: $controllerProvider,
                    $filterProvider: $filterProvider,
                    $provide: $provide
                };

                var route = routeResolverProvider.route;

                //路由解析参数
                //1，视图路径，可以是ASP.NET路由路径，也可以是任意HTML文件路径
                //2，脚本路径，如果为null，则采用视图路径自动生成默认脚本路径（视图路径必须是ASP.NET路由），如果是字符串，则要求为相对根目录的路径，如果为字符串数组，则加载多个脚本
                //3，控制器名称，需要和脚本定义当中的一致
                //4，是否允许视图缓存，默认为true
                //5，视图路径生成函数，可以通过函数来动态生成视图路径
                $routeProvider
                    .when("/master/welcome", route.resolve("/master/welcome",null, "MasterWelcomeController"))
                    .when("/master/example", route.resolve("/master/example",null, "MasterExampleController"))
                    .when("/master/empty", route.resolve("/master/empty",null, "MasterEmptyController"))
                    .when("/example/form", route.resolve("/admin/example/form",null, "ExampleFormController"))
                    .when("/example/datalist", route.resolve("/admin/example/datalist",null, "ExampleDataListController"))
                    .otherwise({
                        redirectTo: "/master/welcome"
                    });
            }
        ]);

        //相当于是入口函数，应用会先执行此处的方法
        app.run([
            "$rootScope",
            function($rootScope) { //注意这种引用变量的方法，可以避免压缩问题，后面所有的angular函数使用都将采用此方式
                $rootScope.title = "StepOn";
                $rootScope.footer = "Power By StepOn Techonology";

                var mask = simpleMask();
                $rootScope.$on("$routeChangeStart",
                    function() {
                        mask.show();
                    });

                $rootScope.$on("$routeChangeSuccess",
                    function() {
                        mask.hide();
                    });
            }
        ]);

        return app;
    });