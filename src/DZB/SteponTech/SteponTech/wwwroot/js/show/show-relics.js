
$(function () {
    document.getElementById("dituContent").innerHTML = '<object type="text/html" data="../../lib/mapWebSite/map.html" width="100%" height="100%"></object>';
    //点击视频弹框
    $('#sacrifice-video').on('click', function() {
        $('.lookVedioMask').css('display','block');
    })
    $('.closeImg').on('click', function() {
        $('.lookVedioMask').css('display','none');
        document.getElementById('allvideo').pause();
    })
    //点击菜单，切换内容
    $('.cont-nav ul li').on('click',function(){
        $(this).toggleClass('contNav-active').siblings('li').removeClass('contNav-active');
    })
    $('#nav-one').on('click', function(){
        $('.infosacrificeBox0').css('display','block');
        $('.infosacrificeBox1').css('display','none');
        $('.infosacrificeBox2').css('display', 'none');
    })
    $('#nav-two').on('click', function(){
        $('.infosacrificeBox0').css('display','none');
        $('.infosacrificeBox1').css('display','block');
        $('.infosacrificeBox2').css('display', 'none');
    })
    $('#archaeological').on('click', function () {
        $('.infosacrificeBox0').css('display', 'none');
        $('.infosacrificeBox1').css('display', 'none');
        $('.infosacrificeBox2').css('display', 'block');
    })  
})