drag = 0
move = 0

var ie = document.all;
var nn6 = document.getElementById && !document.all;
var isdrag = false;
var y, x;
var oDragObj;

function moveMouse(e) {
    if (isdrag) {
        oDragObj.style.top = (nn6 ? nTY + e.clientY - y : nTY + event.clientY - y) + "px";
        oDragObj.style.left = (nn6 ? nTX + e.clientX - x : nTX + event.clientX - x) + "px";
        if (parseInt($(oDragObj).css("top")) <= -300) {
            oDragObj.style.top = -300 + "px";
        }
        if (parseInt($(oDragObj).css("top")) >= 300) {
            oDragObj.style.top = 300 + "px";
        }
        if (parseInt($(oDragObj).css("left")) <= -300) {
            oDragObj.style.left = -300 + 'px';
        }
        if (parseInt($(oDragObj).css("left")) >= 300) {
            oDragObj.style.left = 300 + 'px';
        }
        return false;
    }
}

function initDrag(e) {
    e = event || e;
    var oDragHandle = nn6 ? e.target : event.srcElement;
    var topElement = "HTML";
    while (oDragHandle.tagName != topElement && oDragHandle.className != "dragAble") {
        oDragHandle = nn6 ? oDragHandle.parentNode : oDragHandle.parentElement;
    }
    if (oDragHandle.className == "dragAble") {
        isdrag = true;
        oDragObj = oDragHandle;
        nTY = parseInt(oDragObj.style.top + 0);
        y = nn6 ? e.clientY : event.clientY;
        nTX = parseInt(oDragObj.style.left + 0);
        x = nn6 ? e.clientX : event.clientX;
        document.onmousemove = moveMouse;
        return false;
    }
}
document.onmousedown = initDrag;
document.onmouseup = new Function("isdrag=false");

function smallit() {
    var obj = document.getElementById("images1");
    zoom = parseFloat(obj.style.zoom);
    if (zoom <= 0.8) return false;
    tZoom = zoom - 0.1;
    if (tZoom < 0.1) return true;
    obj.style.zoom = tZoom;
    return false;
}

function bigit() {
    var obj = document.getElementById("images1");
    zoom = parseFloat(obj.style.zoom);
    if (zoom >= 1.5) return false;
    tZoom = zoom + 0.1;
    if (tZoom < 0.1) return true;
    obj.style.zoom = tZoom;
    return false;
}

function realsize() {
    images1.height = images2.height;
    images1.width = images2.width;
    $("#images1")[0].style.zoom = 1;
    //    images1.height=804;
    //    images1.width=1200;
    block1.style.left = 0;
    block1.style.top = 0;

}

function onWheelZoom(obj) { //        
    console.log(1)
    zoom = parseFloat(obj.style.zoom);
    tZoom = zoom + (event.wheelDelta > 0 ? 0.05 : -0.05);
    if (tZoom < 0.1) return true;
    obj.style.zoom = tZoom;
    return false;
}
