require.config({
    baseUrl: "/js/server",
    paths: {
        domReady: "/lib/domready/domready",
        emodule: "/lib/stepon/stepon.module.min",
        routeResolver: "/lib/stepon/services/routeresolver.min",
        app: "/js/server/app"
    }
});

//require仅用于业务逻辑模块js加载，其他js已经在页面上预加载
require(["domReady!","app"],
    function () {
        var ie = (function () {
            var undef,
                v = 3,
                div = document.createElement("div"),
                all = div.getElementsByTagName("i");
            while (
                div.innerHTML === "<!--[if gt IE " + (++v) + "]><i></i><![endif]-->",
                all[0]
            );
            return v > 4 ? v : undef;
        }());

        if (ie === 9) {
            $("html").addClass("ie9");
        }

        initFramework();
        renderControl();

        angular.bootstrap(document, ["so"]);
    });