using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Hemo.HQCWebClient.Models
{
    [DataContract]
    public class MedClinicalMgr
    {
        /// <summary>
        /// 默认构造函数（需要初始化属性的在此处理）
        /// </summary>
        public MedClinicalMgr()
		{
            this.ID= System.Guid.NewGuid().ToString();
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
        /// 总透析历次
        /// </summary>
		[DataMember]
        public virtual int TotalCount { get; set; }

        /// <summary>
        /// 维持性透析人数
        /// </summary>
		[DataMember]
        public virtual int MaintenanceCount { get; set; }

        /// <summary>
        /// 透析项目
        /// </summary>
		[DataMember]
        public virtual string DialysisProject { get; set; }

        /// <summary>
        /// 动静脉内瘘手术
        /// </summary>
		[DataMember]
        public virtual string HasAFS { get; set; }

        /// <summary>
        /// 动静脉内瘘手术例数
        /// </summary>
		[DataMember]
        public virtual int AFSCount { get; set; }

        /// <summary>
        /// 半永久导管深静脉置入术
        /// </summary>
		[DataMember]
        public virtual string HasBYJ { get; set; }

        /// <summary>
        /// 半永久导管深静脉置入术例
        /// </summary>
		[DataMember]
        public virtual int BYJCount { get; set; }

        /// <summary>
        /// 临时导管深静脉置入术
        /// </summary>
		[DataMember]
        public virtual string HasLS { get; set; }

        /// <summary>
        /// 临时导管深静脉置入术例
        /// </summary>
		[DataMember]
        public virtual int LSCount { get; set; }

        /// <summary>
        /// 动静脉内瘘手术术式
        /// </summary>
		[DataMember]
        public virtual string HasAFSS { get; set; }

        /// <summary>
        /// 动静脉内瘘手术术式例
        /// </summary>
		[DataMember]
        public virtual int AFSSCount { get; set; }

        /// <summary>
        /// CRRT治疗例数
        /// </summary>
		[DataMember]
        public virtual int CRRTCount { get; set; }

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

		[DataMember]
        public virtual int IsDelete { get; set; }

		[DataMember]
        public virtual string Editor { get; set; }

		[DataMember]
        public virtual DateTime Edittime { get; set; }

		[DataMember]
        public virtual string Creator { get; set; }

		[DataMember]
        public virtual DateTime Creattime { get; set; }

        #endregion
    }
}
