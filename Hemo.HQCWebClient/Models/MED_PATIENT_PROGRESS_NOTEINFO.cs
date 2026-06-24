using System;
using System.Xml.Serialization;
using System.Runtime.Serialization;

namespace Hemo.HQCWebClient.Models
{
    /// <summary>
    /// MED_PATIENT_PROGRESS_NOTEInfo，DTO对象
    /// </summary>
    [DataContract]
    public class MED_PATIENT_PROGRESS_NOTEINFO
    {
        /// <summary>
        /// 默认构造函数（需要初始化属性的在此处理）
        /// </summary>
        public MED_PATIENT_PROGRESS_NOTEINFO()
        {
            this.ID = System.Guid.NewGuid().ToString();

        }

        #region Property Members

        /// <summary>
        /// 主键
        /// </summary>
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
        /// 病人ID
        /// </summary>
        [DataMember]
        public virtual string PATIENT_ID { get; set; }

        /// <summary>
        /// 透析编号
        /// </summary>
        [DataMember]
        public virtual string HEMODIALYSIS_ID { get; set; }

        /// <summary>
        /// 病程记录
        /// </summary>
        [DataMember]
        public virtual string PROGRESS_NODE { get; set; }

        /// <summary>
        /// 医生编号
        /// </summary>
        [DataMember]
        public virtual string DOCTOR_ID { get; set; }


        /// <summary>
        /// 不适主诉 0=无变化、1=有变化
        /// </summary>
        [DataMember]
        public virtual string COMPLAINTS { get; set; }

        /// <summary>
        /// 容量控制 0=达标、1=不达标
        /// </summary>
        [DataMember]
        public virtual string CAPACITY_CONTROL { get; set; }

        /// <summary>
        /// 干体重
        /// </summary>
        [DataMember]
        public virtual string DRY_WEIHGT { get; set; }

        /// <summary>
        /// 最大脱水量
        /// </summary>
        [DataMember]
        public virtual string MAX_DRY_WEIHGT { get; set; }

        /// <summary>
        /// 最大脱水量除以干体重比值
        /// </summary>
        [DataMember]
        public virtual string PERCENT_DRY_WEIGHT { get; set; }

        /// <summary>
        /// 血压控制 0=达标、1=不达标
        /// </summary>
        [DataMember]
        public virtual string BLOOD_CONTROL { get; set; }

        /// <summary>
        /// 收缩压
        /// </summary>
        [DataMember]
        public virtual string HIGH_BLOOD_PRESSURE { get; set; }

        /// <summary>
        /// 舒张压
        /// </summary>
        [DataMember]
        public virtual string LOW_BLOOD_PRESSURE { get; set; }

        /// <summary>
        /// 血管通路 0=无变化、1=有变化
        /// </summary>
        [DataMember]
        public virtual string VASCULAR_ACCESS { get; set; }

        /// <summary>
        /// 抗凝治疗 0=无变化、1=有变化
        /// </summary>
        [DataMember]
        public virtual string THERAPEUTIC_METHOD { get; set; }

        /// <summary>
        /// 促红素方案
        /// </summary>
        [DataMember]
        public virtual string ERYTHROPOIETIN { get; set; }

        /// <summary>
        /// 净化方式
        /// </summary>
        [DataMember]
        public virtual string PURIFICATION_MODE { get; set; }

        /// <summary>
        /// 并发症
        /// </summary>
        [DataMember]
        public virtual string COMPLICATION { get; set; }

        /// <summary>
        /// 血管通路建议、调整
        /// </summary>
        [DataMember]
        public virtual string VASCULAR_ACCESS_NOTE { get; set; }

        /// <summary>
        /// 抗凝治疗建议、调整
        /// </summary>
        [DataMember]
        public virtual string THERAPEUTIC_METHOD_NOTE { get; set; }

        /// <summary>
        /// 贫血纠正 0=达标、1=不达标
        /// </summary>
        [DataMember]
        public virtual string ANEMIA_CORRECTION { get; set; }

        /// <summary>
        /// 贫血纠正建议、调整
        /// </summary>
        [DataMember]
        public virtual string ANEMIA_CORRECTION_NOTE { get; set; }

        /// <summary>
        /// 贫血纠正不达标记录
        /// </summary>
        [DataMember]
        public virtual string ANEMIA_CORRECTION_BAD { get; set; }

        /// <summary>
        /// 骨矿物质代谢絮乱控制 0=达标、1=不达标
        /// </summary>
        [DataMember]
        public virtual string BONE_MINERAL { get; set; }

        /// <summary>
        /// 骨矿物质代谢絮乱控制建议、调整
        /// </summary>
        [DataMember]
        public virtual string BONE_MINERAL_NOTE { get; set; }

        /// <summary>
        /// 骨矿物质代谢絮乱控制不达标记录
        /// </summary>
        [DataMember]
        public virtual string BONE_MINERAL_BAD { get; set; }

        /// <summary>
        /// 营养情况 0=达标、1=不达标
        /// </summary>
        [DataMember]
        public virtual string NUTRITIONAL_STATUS { get; set; }

        /// <summary>
        /// 营养情况建议、调整
        /// </summary>
        [DataMember]
        public virtual string NUTRITIONAL_STATUS_NOTE { get; set; }

        /// <summary>
        /// 营养情况不达标记录
        /// </summary>
        [DataMember]
        public virtual string NUTRITIONAL_STATUS_BAD { get; set; }

        /// <summary>
        /// 血管通路变化
        /// </summary>
        [DataMember]
        public virtual string VASCULAR_ACCESS_CHANGE { get; set; }

        /// <summary>
        /// 抗凝治疗变化
        /// </summary>
        [DataMember]
        public virtual string THERAPEUTIC_METHOD_CHANGE { get; set; }

        /// <summary>
        /// 不适主诉内容
        /// </summary>
        [DataMember]
        public virtual string COMPLAINTS_CONTENT { get; set; }

        /// <summary>
        /// 血管通路 0=建议、1=调整
        /// </summary>
        [DataMember]
        public virtual string VASCULAR_ACCESS_SUGADJ { get; set; }

        /// <summary>
        /// 抗凝治疗 0=建议、1=调整
        /// </summary>
        [DataMember]
        public virtual string THERAPEUTIC_METHOD_SUGADJ { get; set; }

        /// <summary>
        /// 贫血纠正 0=建议、1=调整
        /// </summary>
        [DataMember]
        public virtual string ANEMIA_CORRECTION_SUGADJ { get; set; }

        /// <summary>
        /// 骨矿物质代谢絮乱控制 0=建议、1=调整
        /// </summary>
        [DataMember]
        public virtual string BONE_MINERAL_SUGADJ { get; set; }

        /// <summary>
        /// 营养情况 0=建议、1=调整
        /// </summary>
        [DataMember]
        public virtual string NUTRITIONAL_STATUS_SUGADJ { get; set; }

        /// <summary>
        /// 扩展字段
        /// </summary>
        [DataMember]
        public virtual string EXTEND_COL { get; set; }

        /// <summary>
        /// 是否删除
        /// </summary>
        [DataMember]
        public virtual int IsDelete { get; set; }

        /// <summary>
        /// 修改人
        /// </summary>
        [DataMember]
        public virtual string Editor { get; set; }

        /// <summary>
        /// 修改日期
        /// </summary>
        [DataMember]
        public virtual DateTime Edittime { get; set; }

        /// <summary>
        /// 创建人
        /// </summary>
        [DataMember]
        public virtual string Creator { get; set; }

        /// <summary>
        /// 创建日期
        /// </summary>
        [DataMember]
        public virtual DateTime Creattime { get; set; }


        #endregion

    }
}