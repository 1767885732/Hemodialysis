/*----------------------------------------------------------------
 * Copyright (C) 2005 (苏州)医疗科技发展有限公司
 * 文件功能描述:MedWorkLoadAccount实体类
 * 创建标识:吕志强-2017年10月25日
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
    public class MedWorkLoadAccount
    {
        public MedWorkLoadAccount()
		{
            this.ID= Guid.NewGuid().ToString();      
		}

        #region 属性
        
		[DataMember]
        public virtual string ID { get; set; }

        /// <summary>
        /// 医院ID
        /// </summary>
		[DataMember]
        public virtual string HospitalId { get; set; }

        /// <summary>
        /// 日期
        /// </summary>
		[DataMember]
        public virtual DateTime HospitalYear { get; set; }


        [DataMember]
        public virtual string HospitalName { get; set; }

        /// <summary>
        /// 透析男性患者
        /// </summary>
		[DataMember]
        public virtual string ManCount { get; set; }

        /// <summary>
        /// 透析女性人数
        /// </summary>
		[DataMember]
        public virtual string Female { get; set; }

        /// <summary>
        /// 20岁以下
        /// </summary>
		[DataMember]
        public virtual string FloAge { get; set; }

        /// <summary>
        /// 20到40岁
        /// </summary>
		[DataMember]
        public virtual string BLeAge { get; set; }

        /// <summary>
        /// 41岁到60岁
        /// </summary>
		[DataMember]
        public virtual string UpAge { get; set; }

        /// <summary>
        /// 60岁以上患者
        /// </summary>
		[DataMember]
        public virtual string LastAge { get; set; }

        /// <summary>
        /// 自体内瘘患者
        /// </summary>
		[DataMember]
        public virtual string AutoFistula { get; set; }

        /// <summary>
        /// 移值物内瘘患者
        /// </summary>
		[DataMember]
        public virtual string GraftsFistula { get; set; }

        /// <summary>
        /// 双静脉患者
        /// </summary>
		[DataMember]
        public virtual string DoubleVein { get; set; }

        /// <summary>
        /// Cuff中心静脉置管患者
        /// </summary>
		[DataMember]
        public virtual string CuffVenous { get; set; }

        /// <summary>
        /// 其它维持性透析患者
        /// </summary>
		[DataMember]
        public virtual string OtherVenous { get; set; }

        /// <summary>
        /// 乙型肝炎表面抗原阳性
        /// </summary>
		[DataMember]
        public virtual string HBVCount { get; set; }

        /// <summary>
        /// 丙型肝炎抗体阳性
        /// </summary>
		[DataMember]
        public virtual string RNACount { get; set; }

        /// <summary>
        /// 每周规律透析3次者
        /// </summary>
		[DataMember]
        public virtual string WeekRoleThree { get; set; }

        /// <summary>
        /// 每周规律透析2次者
        /// </summary>
		[DataMember]
        public virtual string WeekRoleTwo { get; set; }

        /// <summary>
        /// 其它透析频率及比例
        /// </summary>
		[DataMember]
        public virtual string OtherHemoRate { get; set; }

        [DataMember]
        public virtual int IsDelete { get; set; }


        /// <summary>
        /// 编辑人
        /// </summary>
		[DataMember]
        public virtual string Editor { get; set; }

        /// <summary>
        /// 编辑时间
        /// </summary>
		[DataMember]
        public virtual DateTime EditTime { get; set; }

        /// <summary>
        /// 创建人
        /// </summary>
		[DataMember]
        public virtual string Creator { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
		[DataMember]
        public virtual DateTime CreateTime { get; set; }

        #endregion
    }
}
