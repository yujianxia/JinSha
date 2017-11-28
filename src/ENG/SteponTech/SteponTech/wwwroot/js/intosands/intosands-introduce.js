$(function () {
    //点击视频弹框
    $('#gallery').on('click', function () {
        $('.lookVedioMask').css('display', 'block');
    })
    $('.closeImg').on('click', function () {
        $('.lookVedioMask').css('display', 'none');
        document.getElementById('allvideo').pause();
    })
})
