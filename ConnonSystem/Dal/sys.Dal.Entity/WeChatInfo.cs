using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sys.Dal.Entity
{
    public class WeChatInfo
    {
        /// <summary>
        /// 编号
        /// </summary>		
        public long Id { get; set; }
        /// <summary>
        /// Country
        /// </summary>		
        public string Country { get; set; }
        /// <summary>
        /// Province
        /// </summary>		
        public string Province { get; set; }
        /// <summary>
        /// City
        /// </summary>		
        public string City { get; set; }
        /// <summary>
        /// HeadImgurl
        /// </summary>		
        public string HeadImgurl { get; set; }
        /// <summary>
        /// NickName
        /// </summary>		
        public string NickName { get; set; }
        /// <summary>
        /// 用户的性别，值为1时是男性，值为2时是女性，值为0时是未知
        /// </summary>		
        public int Sex { get; set; }
        /// <summary>
        /// OpenId
        /// </summary>		
        public string OpenId { get; set; }
        /// <summary>
        /// UnionId
        /// </summary>		
        public string UnionId { get; set; }
    }
}
