using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Hemo.HQCWebClient.Models
{
    [DataContract]
    public class MedHemoYreaChart
    {
        /// <summary>
        /// 默认构造函数（需要初始化属性的在此处理）
        /// </summary>
        public MedHemoYreaChart()
        {
            this.ID = System.Guid.NewGuid().ToString();
            this.TotalCount = 0;
            this.MaintenanceCount = 0;
            this.Newcount = 0;
            this.Hdcount = 0;
            this.Hdfcount = 0;
            this.Hfcount = 0;
            this.Hpcount = 0;
            this.Hdpcount = 0;
            this.Crrtcount = 0;
            this.Othercount = 0;
            this.DeathCount = 0;

        }

        #region 属性

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
        /// 总透析人次
        /// </summary>
		[DataMember]
        public virtual int TotalCount { get; set; }

        /// <summary>
        /// 维持性透析总人数
        /// </summary>
		[DataMember]
        public virtual int MaintenanceCount { get; set; }

        /// <summary>
        /// 年度新进患者
        /// </summary>
		[DataMember]
        public virtual int Newcount { get; set; }

        /// <summary>
        /// 年度血透(HD)人数
        /// </summary>
		[DataMember]
        public virtual int Hdcount { get; set; }

        /// <summary>
        /// 年度血透(HD)人数比例
        /// </summary>
		[DataMember]
        public virtual string HDRate { get; set; }

        /// <summary>
        /// 年度血滤(HDF)人数
        /// </summary>
		[DataMember]
        public virtual int Hdfcount { get; set; }

        /// <summary>
        /// 年度血滤(HDF)人数
        /// </summary>
		[DataMember]
        public virtual string HDFRate { get; set; }

        /// <summary>
        /// HF人数
        /// </summary>
		[DataMember]
        public virtual int Hfcount { get; set; }

        /// <summary>
        /// HF人数比例
        /// </summary>
		[DataMember]
        public virtual string HFRate { get; set; }

        /// <summary>
        /// HP人数
        /// </summary>
		[DataMember]
        public virtual int Hpcount { get; set; }

        /// <summary>
        /// HP人数比例
        /// </summary>
		[DataMember]
        public virtual string HPRate { get; set; }

        /// <summary>
        /// HDP人数
        /// </summary>
		[DataMember]
        public virtual int Hdpcount { get; set; }

        /// <summary>
        /// HDP人数比例
        /// </summary>
		[DataMember]
        public virtual string HDPRate { get; set; }

        /// <summary>
        /// CRRT人数
        /// </summary>
		[DataMember]
        public virtual int Crrtcount { get; set; }

        /// <summary>
        /// CRRT人数比例
        /// </summary>
		[DataMember]
        public virtual string CRRTRate { get; set; }

        /// <summary>
        /// 其它类型透析数量
        /// </summary>
		[DataMember]
        public virtual int Othercount { get; set; }

        [DataMember]
        public virtual string OTHERRate { get; set; }

        /// <summary>
        /// 年度手术例数
        /// </summary>
		[DataMember]
        public virtual string Operatorcount { get; set; }

        /// <summary>
        /// 透析液钙浓度
        /// </summary>
		[DataMember]
        public virtual string Dcc { get; set; }

        /// <summary>
        /// 年死亡病人数
        /// </summary>
		[DataMember]
        public virtual int DeathCount { get; set; }

        /// <summary>
        /// 年死亡病人率
        /// </summary>
		[DataMember]
        public virtual string DeathRate { get; set; }

        /// <summary>
        /// 删除标示
        /// </summary>
		[DataMember]
        public virtual string IsDelete { get; set; }

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
