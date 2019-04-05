using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZXing;
using ZXing.Common;
using ZXing.QrCode;
using ZXing.QrCode.Internal;

namespace sys.Util
{
   public static class ZXingNetHelper
    {
        static int DefaultQrCode = 500;
        private static int QrCodeWidth =int.TryParse(sys.Util.Config.GetValue("DefaultQrCodeWidth"),out DefaultQrCode) ?int.Parse(sys.Util.Config.GetValue("DefaultQrCodeWidth")) : DefaultQrCode;
        private static int QrCodeHeight = int.TryParse(sys.Util.Config.GetValue("DefaultQrCodeHeight"), out DefaultQrCode) ? int.Parse(sys.Util.Config.GetValue("DefaultQrCodeHeight")) : DefaultQrCode;

        #region MyRegion /// 生成二维码,保存成图片,返回文件名
        /// <summary>生成二维码,保存成图片,返回文件名
        /// <param name="text">内容</param>
        /// <param name="virtualPath">路径</param>
        /// </summary> 
        public static string GenerateQrCode(string text, string virtualPath)
        {
            BarcodeWriter writer = new BarcodeWriter();
            writer.Format = BarcodeFormat.QR_CODE;
            QrCodeEncodingOptions options = new QrCodeEncodingOptions();
            options.DisableECI = true;
            //设置内容编码
            options.CharacterSet = "UTF-8";
            //设置二维码的宽度和高度
            options.Width  = QrCodeWidth;
            options.Height = QrCodeHeight;
            //设置二维码的边距,单位不是固定像素
            options.Margin = 1;
            writer.Options = options;

            //如果不存在就创建file文件夹 
            if (System.IO.Directory.Exists(virtualPath) == false)//如果不存在就创建file文件夹
            {
                System.IO.Directory.CreateDirectory(virtualPath);
            }
            var NowTime = DateTime.Now.ToString("yyyyMMddHHmmss");
            Random rNum = new Random();
            string rNumStr = rNum.Next(1000, 10000).ToString();
            string filename = string.Format("{0}{1}_{2}.png", virtualPath, NowTime, rNumStr);

            Bitmap map = writer.Write(text);
            //string filename = @"H:\桌面\截图\generate1.png";
            map.Save(filename, ImageFormat.Png);
            map.Dispose();
            return string.Format("{0}_{1}.png", NowTime, rNumStr);
        }


        #endregion
        #region MyRegion /// 生成条形码,保存成图片,返回文件名
        /// <summary>生成条形码,保存成图片,返回文件名
        /// <param name="text">内容</param>
        /// <param name="virtualPath">路径</param> 
        public static string GenerateBarCode(string text, string virtualPath)
        {
            BarcodeWriter writer = new BarcodeWriter();
            //使用ITF 格式，不能被现在常用的支付宝、微信扫出来
            //如果想生成可识别的可以使用 CODE_128 格式
            //writer.Format = BarcodeFormat.ITF;
            writer.Format = BarcodeFormat.CODE_128;
            EncodingOptions options = new EncodingOptions()
            {
                Width = 150,
                Height = 50,
                Margin = 2
            };
            writer.Options = options;

            //如果不存在就创建file文件夹 
            if (System.IO.Directory.Exists(virtualPath) == false)//如果不存在就创建file文件夹
            {
                System.IO.Directory.CreateDirectory(virtualPath);
            }
            var NowTime = DateTime.Now.ToString("yyyyMMddHHmmss");
            Random rNum = new Random();
            string rNumStr = rNum.Next(1000, 10000).ToString();
            string filename = string.Format("{0}{1}_{2}.png", virtualPath, NowTime, rNumStr);

            Bitmap map = writer.Write(text); 
            map.Save(filename, ImageFormat.Png);
            return string.Format("{0}_{1}.png", NowTime, rNumStr);
        }
        #endregion
        #region MyRegion /// 读取二维码
        /// <summary>读取二维码
        /// 读取二维码
        /// 读取失败，返回空字符串
        /// </summary>
        /// <param name="filename">指定二维码图片位置</param>
        public static string Read1(string filename)
        {
            BarcodeReader reader = new BarcodeReader();
            reader.Options.CharacterSet = "UTF-8";
            Bitmap map = new Bitmap(filename);
            Result result = reader.Decode(map);
            return result == null ? "" : result.Text;
        }
        #endregion
        #region MyRegion /// 生成带Logo的二维码,返回文件名

        /// <summary>
        /// 生成带Logo的二维码
        /// </summary>
        /// <param name="text"></param>
        public static string GenerateLogoQrCode(string text,string virtualPath)
        {
            //Logo 图片
            Bitmap logo = new Bitmap(sys.Util.Config.GetValue("QRCodDefaultIconPath"));
            //构造二维码写码器
            MultiFormatWriter writer = new MultiFormatWriter();
            Dictionary<EncodeHintType, object> hint = new Dictionary<EncodeHintType, object>();
            hint.Add(EncodeHintType.CHARACTER_SET, "UTF-8");
            hint.Add(EncodeHintType.ERROR_CORRECTION, ErrorCorrectionLevel.H); 
            //生成二维码 
            BitMatrix bm = writer.encode(text, BarcodeFormat.QR_CODE, QrCodeWidth, QrCodeHeight, hint);
            BarcodeWriter barcodeWriter = new BarcodeWriter();
            Bitmap map = barcodeWriter.Write(bm);


            //获取二维码实际尺寸（去掉二维码两边空白后的实际尺寸）
            int[] rectangle = bm.getEnclosingRectangle();

            //计算插入图片的大小和位置
            int middleW = Math.Min((int)(rectangle[2] / 3.5), logo.Width);
            int middleH = Math.Min((int)(rectangle[3] / 3.5), logo.Height);
            int middleL = (map.Width - middleW) / 2;
            int middleT = (map.Height - middleH) / 2;

            //将img转换成bmp格式，否则后面无法创建Graphics对象
            Bitmap bmpimg = new Bitmap(map.Width, map.Height, PixelFormat.Format32bppArgb);
            using (Graphics g = Graphics.FromImage(bmpimg))
            {
                g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                g.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
                g.DrawImage(map, 0, 0);
            }
            //将二维码插入图片
            Graphics myGraphic = Graphics.FromImage(bmpimg);
            //白底
            myGraphic.FillRectangle(Brushes.White, middleL, middleT, middleW, middleH);
            myGraphic.DrawImage(logo, middleL, middleT, middleW, middleH);

            //如果不存在就创建file文件夹 
            if (System.IO.Directory.Exists(virtualPath) == false)//如果不存在就创建file文件夹
            {
                System.IO.Directory.CreateDirectory(virtualPath);
            }
            var NowTime = DateTime.Now.ToString("yyyyMMddHHmmss");
            Random rNum = new Random();
            string rNumStr = rNum.Next(1000, 10000).ToString();
            string filename = string.Format("{0}{1}_{2}.png", virtualPath, NowTime, rNumStr); 
            //保存成图片
            bmpimg.Save(filename, ImageFormat.Png);
            return string.Format("{0}_{1}.png", NowTime, rNumStr);
        }
        #endregion
         



    }
}
