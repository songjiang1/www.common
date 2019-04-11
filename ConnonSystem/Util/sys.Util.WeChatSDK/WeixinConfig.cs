 using Senparc.Weixin.MP.TenPayLibV3; 
namespace sys.Util.WeChatSDK
{
    public  class WeixinConfig
    {
        public static WeixinConfig Instance = SingleInstance<WeixinConfig>.Instance;

        public   string Token;
        public   string AppId;
        public   string AppSecret;
        public   string WeixinServer;
        public   string EncodingAesKey;
        public   string Key;
        public   string MchId;
        public   string Notify;

        public  WeixinConfig()
        {
            Token = Config.GetValue("WeixinToken");//与微信公众账号后台的Token设置保持一致，区分大小写。
            AppId = Config.GetValue("WeixinAppId"); //与微信公众账号后台的AppId设置保持一致，区分大小写。
            AppSecret = Config.GetValue("WeixinAppSecret");
            EncodingAesKey = Config.GetValue("WeixinEncodingAESKey");
            WeixinServer = Config.GetValue("WeixinServer");
            Key = Config.GetValue("WeixinKey");
            MchId = Config.GetValue("WeixinMchId");
            Notify = Config.GetValue("WeixinNotify");
        }

        /// <summary>
        /// 注册微信支付
        /// </summary>
        public static void RegisterWeixinPay()
        {
            var tenPayV3Info = new TenPayV3Info(Instance.AppId, Instance.AppSecret, Instance.MchId, Instance.Key, Instance.Notify);
            TenPayV3InfoCollection.Register(tenPayV3Info);
        }
    }
}
