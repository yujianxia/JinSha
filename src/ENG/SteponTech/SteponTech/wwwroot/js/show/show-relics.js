
$(function () {

  $(document).ready(function () {
    $('#nav-one').click();
  });


    document.getElementById("dituContent").innerHTML = '<object type="text/html" data="../../lib/mapWebSite/map-en.html" width="100%" height="100%"></object>';
    //点击视频弹框
    $('#sacrifice-video').on('click', function() {
        $('.lookVedioMask').css('display','block');
    })
    $('.closeImg').on('click', function() {
        $('.lookVedioMask').css('display','none');
        document.getElementById('allvideo').pause();
    })
    // 点击菜单，切换内容
    $('.cont-nav ul li').on('click',function(){
        $(this).toggleClass('contNav-active').siblings('li').removeClass('contNav-active');
    })
    $('#nav-two').on('click', function(){
     $('#Sacrifice').css('display','block');
        $('#Introduce').css('display','none');
        $('#Archaeology').css('display', 'none');
    })
   $('#nav-one').on('click', function(){
      $('#Sacrifice').css('display','none');
        $('#Introduce').css('display','block');
        $('#Archaeology').css('display', 'none');
    })
    $('#archaeological').on('click', function () {
      $('#Sacrifice').css('display', 'none');
        $('#Introduce').css('display', 'none');
        $('#Archaeology').css('display', 'block');
        initMap()
    })
});
