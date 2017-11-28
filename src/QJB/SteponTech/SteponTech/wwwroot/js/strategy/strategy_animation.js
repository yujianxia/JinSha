$(function () {
    videoShow();
    function videoShow() {
        var _src = $(".box").eq(0).attr("data-video");
        $(".video-box video").attr({ "src": _src });
        $(".box").on("click", function () {
            var _thisSrc = $(this).attr("data-video");
            $(".video-box video").attr({ "src": _thisSrc });
        })
    }
})