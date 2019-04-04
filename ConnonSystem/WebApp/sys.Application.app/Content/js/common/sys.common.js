function GetCheckCodeForMyMobile(ele) {
    if ($(ele).hasClass('sended'))
        return;
    if (!data.newuser.CheckMobile) { }

}
//获取页面url中的传递参数
var QueryString = function (queryStringName) {
    var result = location.search.match(new RegExp("[\?\&]" + queryStringName + "=([^\&]+)", "i"));
    if (result == null || result.length < 1) {
        return "";
    }
    return result[1];
};
function RenderTime(data) {
    var da = eval('new ' + data.replace('/', '', 'g').replace('/', '', 'g'));
    return da.getFullYear() + "年" + da.getMonth() + "月" + da.getDay() + "日" + da.getHours() + ":" + da.getSeconds() + ":" + da.getMinutes();
}
//弹出框
function alerts(msg) {
    layer.open({
        content: msg,
        btn: '确定'
    });
}
function alertsrect(msg, url) {
    layer.open({
        content: msg,
        btn: '确定',
        yes: function (index) {
            window.location.href = url;
        }
    });
}
//JS格式化字符串
String.prototype.format = function () {
    if (arguments.length == 0) return this;
    for (var s = this, i = 0; i < arguments.length; i++)
        s = s.replace(new RegExp("\\{" + i + "\\}", "g"), arguments[i]);
    return s;
};