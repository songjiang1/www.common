using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using sys.Aplication.Code;
using sys.Application.app.Controllers.Extensions;
using sys.Util;
using sys.Util.WebControl;
using sys.Dal.Busines.AppManage;
using sys.Dal.Busines.TVShowManage;
using System.Text;
using System.IO;
using sys.Util.WeChatSDK;
using sys.Dal.Entity.TVShowManage;

namespace sys.Application.app.Controllers
{
    public class HomeController : BaseController
    {
        System.ComponentModel.NullableConverter nullableDateTime = new System.ComponentModel.NullableConverter(typeof(DateTime?));
        private NoticeBLL noticeBLL = new NoticeBLL();
        private MessageReadBLL messageReadBLL = new MessageReadBLL();
        private CollectionBLL collectionBll = new CollectionBLL();
        private MeetingBLL meetingBLL = new MeetingBLL();
        private string noticeCategory = "2";//分类：会议是1，公告是2
        private string meetingCategory = "1";//分类：会议是1，公告是2
        //[NeedOAuth]
        // GET: Home
        #region MyRegion 视图
        public ActionResult Index()
        {
            //CCPSMSNotifyUtil.TestNotify("18708508278", "2222", "3");
            //ReturnResult rst1 = CCPSMSNotifyUtil.TestNotify("15208547251","2222","3");
            return View();
        }
        #region MyRegion 通知公告
        public ActionResult Notice()
        {
            return View();
        }
        public ActionResult NoticeDetail(string Id)
        {
            //浏览量+1
            noticeBLL.PvPlusOne(Id);
            messageReadBLL.SetForm(OperatorProvider.Provider.Current().UserId, Id, noticeCategory, OperatType.AppRead);
            ViewBag.NoticeEntity = noticeBLL.GetEntity(Id);
            return View();
        }


        #endregion

        #region MyRegion 会议签到

        public ActionResult Meeting()
        {
            return View();
        }
        public ActionResult MeetingDetail(string Id, int State = 0)
        {
            //浏览量+1
            meetingBLL.PvPlusOne(Id);
            messageReadBLL.SetForm(OperatorProvider.Provider.Current().UserId, Id, meetingCategory, OperatType.AppRead);
            var MeetingEntity = meetingBLL.GetEntity(Id);
            string[] name_list;
            StringBuilder ObjName = new StringBuilder();
            if (MeetingEntity != null && MeetingEntity.ObjectName != null)
            {
                if (!string.IsNullOrWhiteSpace(MeetingEntity.ObjectName))
                {
                    name_list = MeetingEntity.ObjectName.Split('|');
                    foreach (string item in name_list)
                    {
                        ObjName.Append((string.IsNullOrWhiteSpace(item.Split(',')[1]) ? "" : item.Split(',')[1] + "、"));
                    }
                }
            }
            var messageEntity = messageReadBLL.GetEntity(OperatorProvider.Provider.Current().UserId, Id, meetingCategory);
            ViewBag.EffectiveMark = MeetingEntity.ConveneETime > DateTime.Now ? true : false;
            ViewBag.Qualification = messageEntity == null ? false : true;//会议资格
            ViewBag.AttendExpo = messageEntity == null ? false : messageEntity.AttendExpo == true ? true : false;
            ViewBag.SignInDescription = string.IsNullOrWhiteSpace(messageEntity.SignInDescription) ? "" : messageEntity.SignInDescription;
            ViewBag.ObjTitle = State == 0 ? "未开会议" : "已开会议";
            ViewBag.State = State;
            ViewBag.ObjName = ObjName.ToString().TrimEnd('、');
            ViewBag.MeetingEntity = MeetingEntity;
            return View();
        }

        public ActionResult SignIn(string Id)
        {
            ViewBag.MeetingEntity = meetingBLL.GetEntity(Id);
            return View();
        }
        public ActionResult H5Scan(string Id)
        {
            string timpstamp = WxChatHelper.CreateTimestamp().ToString();
            string nonceStr = WxShareApI.GenerateNonceStr();
            string url = Request.Url.ToString().Trim().Split('#')[0]; //Replace("http://", " ");  timestamp, string noncestr, string url
            string sig = new WxShareApI().GetSignature(timpstamp, nonceStr, url); 
            WeChatEntity weChatEntity=new WeChatEntity {
                appId = WeixinConfig.Instance.AppId,
                timestamp = timpstamp,
                nonceStr = nonceStr,
                signature = sig, 
            };
            ViewBag.WeChatEntity = weChatEntity;
            return View();
        }

        #endregion

        #region MyRegion 代表信息
        public ActionResult Represent()
        {
            return View();
        }
        public ActionResult RepresentDetail(string Id)
        {
            CollectionEntity collectionEntity = new CollectionEntity();
            collectionEntity=collectionBll.GetEntity(Id);
            ViewBag.CollectionEntity = collectionEntity;
            return View();
        }

        #endregion

        #region MyRegion  议案建议
        public ActionResult Proposal()
        {
            return View();
        }
        public ActionResult ProposalDetail(string Id)
        {
            CollectionEntity collectionEntity = new CollectionEntity();
            collectionEntity = collectionBll.GetEntity(Id);
            ViewBag.CollectionEntity = collectionEntity;
            return View();
        }
        #endregion

        #endregion


        #region MyRegion 获取数据
        /// <summary>
        /// 获取通知公告下拉数据
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="queryJson"></param>
        /// <returns></returns>
        public ActionResult GetNoticeJisonList(string pageIndex, string queryJson)
        {
            Pagination pagination = new Pagination();
            pagination.page = string.IsNullOrEmpty(pageIndex) ? 1 : Convert.ToInt32(pageIndex);
            pagination.rows = 10;
            pagination.sidx = "CreateDate";
            pagination.sord = "desc";
            var data = noticeBLL.GetPageList(pagination, queryJson).ToList();
            var isred = messageReadBLL.GetList(OperatorProvider.Provider.Current().UserId);
            var readrows = isred.Where(t => t.Category == noticeCategory && t.AppRead == true);
            List<SearchCommonEntity> searchNotice = new List<SearchCommonEntity>();

            for (int i = 0; i < data.Count; i++)
            {
                bool isread = false;
                if (readrows.Where(t => t.MessageId.Contains(data[i].NewsId)).ToList().Count > 0)
                {
                    isread = true;
                }
                searchNotice.Add(new SearchCommonEntity
                {
                    BriefHead = data[i].BriefHead,
                    Content = data[i].NewsContent,
                    FullHead = data[i].FullHead,
                    FullHeadColor = data[i].FullHeadColor,
                    CreateDate = data[i].CreateDate,
                    IsRead = isread,
                    Cover = data[i].Cover,
                    Id = data[i].NewsId,
                    PV = data[i].PV,


                });
            }
            var JsonData = new
            {
                rows = searchNotice,
                total = pagination.total,
                page = pagination.page,
                records = pagination.records,
            };
            return Success(JsonData.ToJson());
        }

        /// <summary>
        /// 获取会议下拉数据
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="state">会议状态：0 未开；1 已开</param>
        /// <param name="queryJson"></param>
        /// <returns></returns>
        public ActionResult GetMeetingJisonList(string pageIndex, string queryJson)
        {
            Pagination pagination = new Pagination();
            pagination.page = string.IsNullOrEmpty(pageIndex) ? 1 : Convert.ToInt32(pageIndex);
            pagination.rows = 10;
            pagination.sidx = "CreateDate";
            pagination.sord = "desc";
            var data = meetingBLL.GetPageList(pagination, queryJson).ToList();
            var nowTime = DateTime.Now;
            var isred = messageReadBLL.GetList(OperatorProvider.Provider.Current().UserId);
            var readrows = isred.Where(t => t.Category == meetingCategory && t.AppRead == true);
            List<SearchCommonEntity> searchNotice = new List<SearchCommonEntity>();
            for (int i = 0; i < data.Count; i++)
            {
                bool isread = false;
                int meetingState = 0;
                if (data[i].ConveneSTime > nowTime) { meetingState = 0; }
                if (data[i].ConveneSTime <= nowTime && data[i].ConveneETime > nowTime) { meetingState = 2; }
                if (data[i].ConveneETime <= nowTime) { meetingState = 1; }
                if (readrows.Where(t => t.MessageId.Contains(data[i].MeetingId)).ToList().Count > 0)
                {
                    isread = true;
                }
                searchNotice.Add(new SearchCommonEntity
                {
                    BriefHead = data[i].BriefHead,
                    Content = data[i].MeetingContent,
                    FullHead = data[i].FullHead,
                    FullHeadColor = data[i].FullHeadColor,
                    CreateDate = data[i].CreateDate,
                    IsRead = isread,
                    Cover = data[i].Cover,
                    Id = data[i].MeetingId,
                    PV = data[i].PV,
                    ConveneSTime = data[i].ConveneSTime,
                    ConveneETime = data[i].ConveneETime,
                    MeetingState = meetingState,
                    SignQRCode = data[i].SignQRCode,
                });
            }
            var JsonData = new
            {
                rows = searchNotice,
                total = pagination.total,
                page = pagination.page,
                records = pagination.records,
            };
            return Success(JsonData.ToJson());
        }

        /// <summary>
        /// 获取会议代表信息 按人名
        /// </summary>
        /// <param name="ParentId"></param>
        /// <param name="TVShowMenuId"></param>
        /// <param name="OrganizeId"></param>
        /// <param name="OrganizeId"></param>
        /// <returns></returns>
        public ActionResult GeRspresentJsonList(string ParentId, string TVShowMenuId,string OrganizeId,string Title)
        {
            CollectionEntity collectionEntity= new CollectionEntity();
            collectionEntity.ParentId = ParentId;
            collectionEntity.TVShowMenuId = TVShowMenuId;
            collectionEntity.OrganizeId = OrganizeId;
            collectionEntity.Title = Title;
            var dtInitials = collectionBll.GetInitials(collectionEntity);
            var dtrows = collectionBll.GetTable(collectionEntity,"");
            var JsonData = new
            {
                dtInitials = dtInitials,
                dtrows = dtrows,
            };
            return Success(JsonData.ToJson());
        }


        /// <summary>
        /// 获取会议代表信息 按区域
        /// </summary>
        /// <param name="ParentId"></param>
        /// <param name="TVShowMenuId"></param>
        /// <param name="OrganizeId"></param>
        /// <param name="OrganizeId"></param>
        /// <returns></returns>
        public ActionResult GeRspresentAddressList(string ParentId, string TVShowMenuId, string OrganizeId, string Title,string  UserId="",string OrderBy="")
        {
            CollectionEntity collectionEntity = new CollectionEntity();
            collectionEntity.ParentId = ParentId;
            collectionEntity.TVShowMenuId = TVShowMenuId;
            collectionEntity.OrganizeId = OrganizeId;
            collectionEntity.Title = Title;
            collectionEntity.CreateUserId = UserId;
            var dtrows = collectionBll.GetTable(collectionEntity , OrderBy); 
            return Success(dtrows.ToJson());
        }

        #endregion

        #region MyRegion 提交数据 AttendExpo

        public ActionResult UpdateAttendExpo(string Id, int State, string SignInDescription)
        {
            var meetingEntity = meetingBLL.GetEntity(Id);
            if (meetingEntity != null)
            {
                var flag = 0;
                if (State == 1)
                {
                    flag = messageReadBLL.SignInMark(OperatorProvider.Provider.Current().UserId, Id, meetingCategory, OperatType.AttendExpo, SignInDescription);
                }
                else
                {
                    flag = messageReadBLL.SignInMark(OperatorProvider.Provider.Current().UserId, Id, meetingCategory, OperatType.NoAttendExpo, SignInDescription);
                }

                if (flag == 1)
                {
                    return Success("确认成功");
                }
                else
                {
                    return Error("没有参加本次会议，不能确认");
                }


            }
            return Error("无效参数错误");

        }

        public ActionResult UpdateSignInMark(string Id)
        {
            var nowTime = DateTime.Now;
            var meetingEntity = meetingBLL.GetEntity(Id);
            if (meetingEntity != null)
            {

                if (meetingEntity.ConveneETime > nowTime)
                {
                    var flag = 0;
                    flag = messageReadBLL.SignInMark(OperatorProvider.Provider.Current().UserId, Id, meetingCategory, OperatType.SignIn, "");



                    if (flag == 1)
                    {
                        return Success(nowTime.ToString("yyyy年MM月dd日 ttHH:mm"));
                    }
                    else
                    {
                        return Error("没有参加本次会议，不能签到");
                    }
                }
                else
                {
                    return Error("会议已经结束，不能签到了");
                }
            }
            return Error("无效的链接");

        }

        [ValidateInput(false)]
        public ActionResult QrCode(string Img)
        {
            // Server.MapPath("") 或者 Server.MapPath("~/")
            try
            {
                Img=Img.Replace("data:image/png;base64,", "");
                var basePath = Server.MapPath("~/");
                string directoryPath = basePath + "/Resource/QRCodFile/H5Scan/" + DateTime.Now.ToString("yyyy-MM-dd");
                string fileName = string.Format("{0}{1}", DateTime.Now.ToString("yyyyMMddHHmmss"), new Random().Next(1000, 10000));
                string savePath = string.Empty;
                if (!Directory.Exists(directoryPath))
                {
                    Directory.CreateDirectory(directoryPath);
                }
                savePath = directoryPath + "/" + fileName + ".png";
                FileStream fs = System.IO.File.Create(savePath);
                byte[] bytes = Convert.FromBase64String(Img);
                fs.Write(bytes, 0, bytes.Length);
                fs.Close();
                fs.Dispose();
                return Content(ZXingNetHelper.ReadQrCode(savePath));
            }
            catch (Exception ex)
            {
                return Content(ex.Message);
            }



        }

        #endregion


        

    }
    #region MyRegion 返回类公共实体
    public class SearchCommonEntity
    {
        #region 实体成员
        /// <summary>
        ///  主键
        /// </summary>		
        public string Id { get; set; }
        /// <summary>
        /// 类型（1-新闻2-公告）
        /// </summary>		
        public int? TypeId { get; set; }
        /// <summary>
        /// 父级主键
        /// </summary>		
        public string ParentId { get; set; }
        /// <summary>
        /// 所属类别
        /// </summary>		
        public string Category { get; set; }
        /// <summary>
        /// 完整标题
        /// </summary>		
        public string FullHead { get; set; }
        /// <summary>
        /// 标题颜色
        /// </summary>		
        public string FullHeadColor { get; set; }
        /// <summary>
        /// 简略标题
        /// </summary>		
        public string BriefHead { get; set; }
        /// <summary>
        /// 作者
        /// </summary>		
        public string AuthorName { get; set; }
        /// <summary>
        /// 编辑
        /// </summary>		
        public string CompileName { get; set; }
        /// <summary>
        /// Tag词
        /// </summary>		
        public string TagWord { get; set; }
        /// <summary>
        /// 关键字
        /// </summary>		
        public string Keyword { get; set; }
        /// <summary>
        /// 来源
        /// </summary>		
        public string SourceName { get; set; }
        /// <summary>
        /// 来源地址
        /// </summary>		
        public string SourceAddress { get; set; }
        /// <summary>
        /// 内容
        /// </summary>		
        public string Content { get; set; }
        /// <summary>
        /// 浏览量
        /// </summary>		
        public int? PV { get; set; }
        /// <summary>
        /// 发布时间
        /// </summary>		
        public DateTime? ReleaseTime { get; set; }
        /// <summary>
        /// 排序码
        /// </summary>		
        public int? SortCode { get; set; }
        /// <summary>
        /// 备注
        /// </summary>		
        public string Description { get; set; }
        /// <summary>
        /// 创建日期
        /// </summary>		
        public DateTime? CreateDate { get; set; }
        /// <summary>
        /// 封面图
        /// </summary>
        public string Cover { get; set; }
        /// <summary>
        /// 已读
        /// </summary>
        public bool IsRead { get; set; }
        /// <summary>
        /// 会议开始时间
        /// </summary>
        public DateTime? ConveneSTime { get; set; }
        /// <summary>
        /// 会议结束时间
        /// </summary>
        public DateTime? ConveneETime { get; set; }
        /// <summary>
        /// 会议地点
        /// </summary>		
        public string MeetingAddress { get; set; }
        /// <summary>
        /// 参会人员姓名
        /// </summary>		
        public string ObjectName { get; set; }
        /// <summary>
        /// 签到
        /// </summary>
        public bool SignInMark { get; set; }
        /// <summary>
        /// 会议状态 0 未开，1已开，2会议中
        /// </summary>
        public int MeetingState { get; set; }

        /// <summary>
        ///二维码 签到
        /// </summary>
        public string SignQRCode { get; set; }
        #endregion


    }

    public class WeChatEntity
    {
        #region 实体成员 
        /// <summary>
        /// appId
        /// </summary>		
        public string appId { get; set; }
        /// <summary>
        /// timestamp
        /// </summary>		
        public string timestamp { get; set; }
        /// <summary>
        /// nonceStr
        /// </summary>		
        public string nonceStr { get; set; }
        /// <summary>
        /// signature
        /// </summary>		
        public string signature { get; set; }
        #endregion


    }

    #endregion

}