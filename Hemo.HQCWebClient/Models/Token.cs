/*----------------------------------------------------------------
 * Copyright (C) 2005 (苏州)医疗科技发展有限公司
 * 文件功能描述:Hemo.HQCWebClient.Models
 * 创建标识:贺建操-2014年7月27日
 * ----------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hemo.HQCWebClient.Models
{
    /// <summary>
    /// Token实体类
    /// </summary>
    public class Token
    {
        /// <summary>
        /// 用户ID
        /// </summary>
        public int StaffId { get; set; }

        /// <summary>
        /// 用户名对应签名Token
        /// </summary>
        public Guid SignToken { get; set; }

        /// <summary>
        /// Token过期时间
        /// </summary>
        public DateTime ExpireTime { get; set; }
    }
}
