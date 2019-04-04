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

namespace sys.Application.app.Controllers
{ 
    public class HomeController : BaseController
    {
        private NoticeBLL noticeBLL = new NoticeBLL();
        private MessageReadBLL messageReadBLL = new MessageReadBLL();
        private string Category = "2";//分类：会议是1，公告是2

        //[NeedOAuth]
        // GET: Home
        #region MyRegion 视图
        public ActionResult Index()
        {
            //CCPSMSNotifyUtil.TestNotify("18708508278", "2222", "3");
            //ReturnResult rst1 = CCPSMSNotifyUtil.TestNotify("15208547251","2222","3");
            return View();
        }
        public ActionResult Notice()
        {
            return View();
        }
        public ActionResult NoticeDetail(string Id)
        {
            //浏览量+1
            noticeBLL.PvPlusOne(Id); 
            messageReadBLL.SetForm(OperatorProvider.Provider.Current().UserId,Id, Category, OperatType.AppRead);
            ViewBag.NoticeEntity = noticeBLL.GetEntity(Id);
            return View(); 
        }
        #endregion


        #region MyRegion 获取数据
        public ActionResult GetJisonList(string pageIndex,string queryJson)
        { 
            Pagination pagination = new Pagination();
            pagination.page = string.IsNullOrEmpty(pageIndex) ? 1 : Convert.ToInt32(pageIndex);
            pagination.rows = 10;
            pagination.sidx = "CreateDate";
            pagination.sord= "desc"; 
            var data = noticeBLL.GetPageList(pagination, queryJson).ToList();
            var isred = messageReadBLL.GetList(OperatorProvider.Provider.Current().UserId);
            var readrows= isred.Where(t => t.Category== Category&&t.AppRead==true);
            List<SearchNotice> searchNotice = new List<SearchNotice>();
           
            for (int i = 0; i < data.Count; i++)
            {
                bool isread = false;
                if (readrows.Where(t => t.MessageId.Contains(data[i].NewsId)).ToList().Count>0)
                {
                    isread = true;
                }
                searchNotice.Add(new SearchNotice
                {
                    BriefHead = data[i].BriefHead,
                    NewsContent = data[i].NewsContent,
                    FullHead = data[i].FullHead,
                    FullHeadColor = data[i].FullHeadColor,
                    CreateDate = data[i].CreateDate,
                    IsRead = isread,
                    Cover= data[i].Cover,
                    NewsId= data[i].NewsId,
                    PV= data[i].PV,


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


        #endregion

        #region MyRegion 提交数据

        #endregion


        public class SearchNotice
        {
            #region 实体成员
            /// <summary>
            /// 新闻主键
            /// </summary>		
            public string NewsId { get; set; }
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
            /// 新闻内容
            /// </summary>		
            public string NewsContent { get; set; }
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

            public bool IsRead { get; set; }

            #endregion


        }
    } 
}