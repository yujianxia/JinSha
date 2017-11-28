/**
 * Created by admin on 2017/8/2.
 */
$(function () {
    showIndex();
    jQuery(".slideBox").slide({ mainCell: ".bd ul", effact: 'fade', defaultIndex: showIndex()});
    function showIndex() {
        var hash = location.hash;
        var liList = $(".bd li");
        var indexOf = 0;
        hash = hash.slice(1);
        liList.each(function (index, item) {
            if (item.id.indexOf(hash) != -1) {
                indexOf =  index;
            }
        })
        return indexOf;
    }
});