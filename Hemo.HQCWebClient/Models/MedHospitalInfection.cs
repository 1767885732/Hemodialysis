/*----------------------------------------------------------------
 * Copyright (C) 2005 (苏州)医疗科技发展有限公司
 * 文件功能描述:MedHospitalInfection实体类
 * 创建标识:吕志强-2017年10月23日
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
    public class MedHospitalInfection
    {
        public MedHospitalInfection()
		{
            this.ID= System.Guid.NewGuid().ToString();
 		}

        #region Property Members
        
		[DataMember]
        public virtual string ID { get; set; }

        /// <summary>
        /// 医院编号
        /// </summary>
		[DataMember]
        public virtual string HospitalId { get; set; }

        /// <summary>
        /// 日期
        /// </summary>
		[DataMember]
        public virtual DateTime HospitalYear { get; set; }

        /// <summary>
        /// 医院名称
        /// </summary>
		[DataMember]
        public virtual string HospitalName { get; set; }

        /// <summary>
        /// 乙型肝炎表面抗原（含e抗原）阳性例
        /// </summary>
		[DataMember]
        public virtual int YXGYCount { get; set; }

        /// <summary>
        /// 乙型肝炎表面抗原（含e抗原）阳性率
        /// </summary>
		[DataMember]
        public virtual string YXGYRate { get; set; }

        /// <summary>
        /// 丙型肝炎抗体阳性例
        /// </summary>
		[DataMember]
        public virtual int BXGYCount { get; set; }

        /// <summary>
        /// 丙型肝炎抗体阳性率
        /// </summary>
		[DataMember]
        public virtual string BXGYRate { get; set; }

		[DataMember]
        public virtual int IsDelete { get; set; }

        /// <summary>
        /// 血透转腹透例数
        /// </summary>
        [DataMember]
        public virtual int XTZFTCount { get; set; }

        /// <summary>
        /// 血透转肾移植例数
        /// </summary>
        [DataMember]
        public virtual int XTZSYZCount { get; set; }

		[DataMember]
        public virtual string Editor { get; set; }

		[DataMember]
        public virtual DateTime Edittime { get; set; }

		[DataMember]
        public virtual string Creator { get; set; }

		[DataMember]
        public virtual DateTime Creattime { get; set; }

        /// <summary>
        /// 艾滋病例数
        /// </summary>
        [DataMember]
        public virtual int AZBCount { get; set; }

        /// <summary>
        /// 艾滋病比例
        /// </summary>
        [DataMember]
        public virtual string AZBRate { get; set; }

        /// <summary>
        /// 梅毒例数
        /// </summary>
        [DataMember]
        public virtual int MDCount { get; set; }

        /// <summary>
        /// 梅毒比例
        /// </summary>
        [DataMember]
        public virtual string MDRate { get; set; }

        /// <summary>
        /// 全阴例数
        /// </summary>
        [DataMember]
        public virtual int QYCount { get; set; }

        /// <summary>
        /// 全阴比例
        /// </summary>
        [DataMember]
        public virtual string QYRate { get; set; }

        /// <summary>
        /// 待查例数
        /// </summary>
        [DataMember]
        public virtual int DCCount { get; set; }

        /// <summary>
        /// 待查比例
        /// </summary>
        [DataMember]
        public virtual string DCRate { get; set; }

        /// <summary>
        /// 正常例数
        /// </summary>
        [DataMember]
        public virtual int ZCCount { get; set; }

        /// <summary>
        /// 正常比例
        /// </summary>
        [DataMember]
        public virtual string ZCRate { get; set; }

        /// <summary>
        /// 除乙肝丙肝其它传染病例数
        /// </summary>
        [DataMember]
        public virtual int QTCount { get; set; }

        /// <summary>
        /// 除乙肝丙肝其它传染病比例
        /// </summary>
        [DataMember]
        public virtual string QTRate { get; set; }

        #endregion
    }
}
