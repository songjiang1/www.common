var file = file || {};

file.createNewRow = function (html) {
    $('#fileList').after($("<div></div>").html(html));
    var ps = $(".listTableIn").position();
    if (ps) {
        $(".float_box").css("position", "absolute");
        $(".float_box").css("right", ps.left); //æ‡¿Î◊Û±ﬂæ‡
        $(".float_box").css("top", +7); //æ‡¿Î…œ±ﬂæ‡
    }
    $('#myModal5').modal('hide');
}




file.error = function (obj) {
    var oTimer = null;
    var i = 0;
    oTimer = setInterval(function () {
        i++;
        i == 5 ? clearInterval(oTimer) : (i % 2 == 0 ? obj.css("background-color", "#ffffff") : obj.css("background-color", "#ffd4d4"));
    }, 200);
}

file.check = function () {
    if ($.trim($('#search').val()).length < 1) {
        file.error($('#search'));
        return false;
    }
}

file.lang = function (str) {
    return str;
}