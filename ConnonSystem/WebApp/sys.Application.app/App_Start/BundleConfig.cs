using System.Web;
using System.Web.Optimization;

namespace sys.Application.app
{
    public class BundleConfig
    {
        // 有关绑定的详细信息，请访问 http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/BaseScripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/BaseScripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/BaseScripts/bootstrap.js",
                      "~/Scripts/BaseScripts/respond.js"));
            //轮播效果插件
            bundles.Add(new ScriptBundle("~/bundles/jquerytouch").Include(
                      "~/Scripts/BaseScripts/jquery.touchSwipe.min.js"));
            //移动端弹框插件js
            bundles.Add(new ScriptBundle("~/bundles/layerjs").Include(
                      "~/Scripts/BaseScripts/layer.js"));
            //滑动加载效果js插件
            bundles.Add(new ScriptBundle("~/bundles/droploadjs").Include(
                      "~/Scripts/BaseScripts/dropload.min.js"));
            bundles.Add(new ScriptBundle("~/bundles/vue").Include(
                      "~/Scripts/Vue/vue.js",
                      "~/Scripts/Vue/vue-resource.min.js"
                //,"~/Scripts/Vue/vue-extend.js"
                      ));
            //日期
            bundles.Add(new ScriptBundle("~/bundles/dateJs").Include(
                "~/Scripts/BaseScripts/mobiscroll_002.min.js",
                "~/Scripts/BaseScripts/mobiscroll.min.js"));
            //微信分享
            bundles.Add(new ScriptBundle("~/bundles/WxShare").Include(
                "~/Scripts/WxChat/WxShareApi.js"
                ));
            //文件上传
            bundles.Add(new ScriptBundle("~/bundles/FilesUpload").Include(
                "~/Scripts/BaseScripts/plupload.full.min.js"
                ));
            bundles.Add(new ScriptBundle("~/bundles/sortjs").Include(
                "~/Scripts/BaseScripts/charfirst.js",
                "~/Scripts/BaseScripts/sort.js"
                ));
            /*****************************Css文件引用**************************/
            bundles.Add(new StyleBundle("~/bundles/css").Include(
                      "~/Content/Css/BaseCss/main.css"
                      ));
            //移动端弹框样式
            bundles.Add(new StyleBundle("~/bundles/layer").Include(
                      "~/Content/Css/BaseCss/layer.css"));
            bundles.Add(new StyleBundle("~/bundles/loadstyle").Include(
                      "~/Content/Css/BaseCss/loaders.min.css"));
            //滑动加载效果样式
            bundles.Add(new StyleBundle("~/bundles/dropload").Include(
                      "~/Content/Css/BaseCss/dropload.css"));
            bundles.Add(new StyleBundle("~/bundles/dateCss").Include(
          "~/Content/Css/BaseCss/mobiscroll.min.css"));
            bundles.Add(new StyleBundle("~/bundles/sortcss").Include(
                      "~/Content/Css/BaseCss/sort.css"));

        }
    }
}
