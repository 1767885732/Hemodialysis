/*----------------------------------------------------------------
 * Copyright (C) 2005 (苏州)医疗科技发展有限公司
 * 文件功能描述:MedPrimaryData实体类
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
    public class MedPrimaryData
    {
        /// <summary>
        /// 默认构造函数
        /// </summary>
        public MedPrimaryData()
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
        /// ClinicalId
        /// </summary>
        [DataMember]
        public virtual string ClinicalId { get; set; }

        /// <summary>
        /// PrimaryId
        /// </summary>
        [DataMember]
        public virtual string PrimaryId { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        [DataMember]
        public virtual string PrimaryName { get; set; }

        /// <summary>
        /// 数量
        /// </summary>
        [DataMember]
        public virtual int Count { get; set; }

        /// <summary>
        /// 比例
        /// </summary>
        [DataMember]
        public virtual string Rate { get; set; }

        #endregion
    }
}
