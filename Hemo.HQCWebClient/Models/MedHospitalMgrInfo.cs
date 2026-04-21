/*----------------------------------------------------------------
 * Copyright (C) 2005 (苏州)医疗科技发展有限公司
 * 文件功能描述:Hemo.HQCWebClient.Models
 * 创建标识:贺建操-2014年8月2日
 * ----------------------------------------------------------------*/
using System;
using System.Xml.Serialization;
using System.Runtime.Serialization;

namespace Hemo.HQCWebClient.Models
{
    /// <summary>
    /// 科室信息实体类
    /// </summary>
    [DataContract]
    public class MedHospitalMgrInfo
    { 
        /// <summary>
        /// 默认构造函数（需要初始化属性的在此处理）
        /// </summary>
	    public MedHospitalMgrInfo()
		{
            this.ID= System.Guid.NewGuid().ToString();
 		}

        #region Property Members
        
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


        [DataMember]
        public virtual DateTime HospitalYear { get; set; }

        /// <summary>
        /// 医院名称
        /// </summary>

        [DataMember]
        public virtual string HospitalName { get; set; }

        /// <summary>
        /// 医院等级
        /// </summary>

        [DataMember]
        public virtual string HospitalLevel { get; set; }

        /// <summary>
        /// 血透室负责人
        /// </summary>

        [DataMember]
        public virtual string ContactPeople { get; set; }

        /// <summary>
        /// 负责人联系电话
        /// </summary>
        [DataMember]
        public virtual string ContactPhone { get; set; }

        /// <summary>
        /// 负责人邮箱
        /// </summary>

        [DataMember]
        public virtual string ContactEmail { get; set; }

        /// <summary>
        /// 血透室护士长
        /// </summary>

        [DataMember]
        public virtual string HeadNurse { get; set; }

        /// <summary>
        /// 护士长联系电话
        /// </summary>

        [DataMember]
        public virtual string HeadNursePhone { get; set; }

        /// <summary>
        /// 护士长邮件
        /// </summary>

        [DataMember]
        public virtual string HeadNurseEmail { get; set; }

        /// <summary>
        /// 医生数量
        /// </summary>

        [DataMember]
        public virtual int PhysicianCount { get; set; }

        /// <summary>
        /// 护士数量
        /// </summary>

        [DataMember]
        public virtual int NurseCount { get; set; }

        /// <summary>
        /// 技师数量
        /// </summary>

        [DataMember]
        public virtual int TechnicianCount { get; set; }

        /// <summary>
        /// 是否有储物室
        /// </summary>

        [DataMember]
        public virtual string HasStorageRoom { get; set; }

        /// <summary>
        /// 是否有污物处理区
        /// </summary>

        [DataMember]
        public virtual string HasDirtArea { get; set; }

        /// <summary>
        /// 血透室内透析区数量
        /// </summary>

        [DataMember]
        public virtual int DialysisAreaCount { get; set; }

        /// <summary>
        /// 血透室内床位数量
        /// </summary>

        [DataMember]
        public virtual int BedCount { get; set; }

        /// <summary>
        /// 传染病人专用透析区
        /// </summary>

        [DataMember]
        public virtual string HasInfectArea { get; set; }

        /// <summary>
        /// 是否处于删除状态
        /// </summary>

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