/*----------------------------------------------------------------
 * Copyright (C) 2005 (苏州)医疗科技发展有限公司
 * 文件功能描述:MedMonitorIndex实体类
 * 创建标识:吕志强-2017年10月16日
 * 
 * 修改时间:
 * 修改人:
 * 修改描述:
 * ----------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Hemo.HQCWebClient.Models
{
    [DataContract]
    public class MedMonitorIndex
    {
        /// <summary>
        /// 默认构造函数
        /// </summary>
        public MedMonitorIndex()
		{
            this.ID= System.Guid.NewGuid().ToString();
        }

        #region 属性

        /// <summary>
        /// ID
        /// </summary>
        [DataMember]
        public virtual string ID { get; set; }

        /// <summary>
        /// 医院编号
        /// </summary>
        [DataMember]
        public virtual string HospitalId { get; set; }

        /// <summary>
        /// 医院年份
        /// </summary>
        [DataMember]
        public virtual DateTime HospitalYear { get; set; }

        /// <summary>
        /// 医院名称
        /// </summary>
        [DataMember]
        public virtual string HospitalName { get; set; }

        // <summary>
        /// 删除标识
        /// </summary>
        [DataMember]
        public virtual int IsDelete { get; set; }

        /// <summary>
        /// 编辑者
        /// </summary>
        [DataMember]
        public virtual string Editor { get; set; }

        /// <summary>
        /// 编辑时间
        /// </summary>
        [DataMember]
        public virtual DateTime Edittime { get; set; }

        /// <summary>
        /// 创建者
        /// </summary>
        [DataMember]
        public virtual string Creator { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        [DataMember]
        public virtual DateTime Creattime { get; set; }

        #endregion
    }
}
