using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Hemo.HQCWebClient.Models
{
    /// <summary>
    /// MedBaseRecordInfo，DTO对象
    /// </summary>
    [DataContract]
    public class MedBaseRecordInfo
    {
        /// <summary>
        /// 默认构造函数（需要初始化属性的在此处理）
        /// </summary>
        public MedBaseRecordInfo()
        {
           

        }

        #region Property Members

        [DataMember]
        public virtual string ID { get; set; }

        /// <summary>
        /// 医院编码
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
        /// 透析编号
        /// </summary>
        [DataMember]
        public virtual string HemodialysisId { get; set; }

        /// <summary>
        /// 患者姓名
        /// </summary>
        [DataMember]
        public virtual string PatientName { get; set; }

        /// <summary>
        /// 慢性肾小球肾炎
        /// </summary>
        [DataMember]
        public virtual string Cgn { get; set; }

        /// <summary>
        /// 宫颈上皮内瘤变
        /// </summary>
        [DataMember]
        public virtual string Cin { get; set; }

        /// <summary>
        /// 糖尿病肾病
        /// </summary>
        [DataMember]
        public virtual string Dn { get; set; }

        /// <summary>
        /// 高血压
        /// </summary>
        [DataMember]
        public virtual string Htn { get; set; }

        /// <summary>
        /// 多囊性肾病
        /// </summary>
        [DataMember]
        public virtual string Pckd { get; set; }

        /// <summary>
        /// 梗阻性肾病
        /// </summary>
        [DataMember]
        public virtual string Uuo { get; set; }

        /// <summary>
        /// 肾肿瘤
        /// </summary>
        [DataMember]
        public virtual string RenalTumor { get; set; }

        /// <summary>
        /// 其他原发病
        /// </summary>
        [DataMember]
        public virtual string OtherProtopathy { get; set; }

        /// <summary>
        /// 其他原发病内容
        /// </summary>
        [DataMember]
        public virtual string OtherProtopathyText { get; set; }

        /// <summary>
        /// 糖尿病
        /// </summary>
        [DataMember]
        public virtual string Dm { get; set; }

        /// <summary>
        /// 冠心病
        /// </summary>
        [DataMember]
        public virtual string Cad { get; set; }

        /// <summary>
        /// 慢性心衰
        /// </summary>
        [DataMember]
        public virtual string Chf { get; set; }

        /// <summary>
        /// 脑血管病
        /// </summary>
        [DataMember]
        public virtual string Cva { get; set; }

        /// <summary>
        /// 外周动脉阻塞性疾病
        /// </summary>
        [DataMember]
        public virtual string Paod { get; set; }

        /// <summary>
        /// 阻塞性肺气肿
        /// </summary>
        [DataMember]
        public virtual string Copd { get; set; }

        /// <summary>
        /// 痴呆
        /// </summary>
        [DataMember]
        public virtual string Anoia { get; set; }

        /// <summary>
        /// 其他合并症
        /// </summary>
        [DataMember]
        public virtual string OtherComorbidity { get; set; }

        /// <summary>
        /// 其他合并症内容
        /// </summary>
        [DataMember]
        public virtual string OtherComorbidityText { get; set; }

        /// <summary>
        /// 抽烟，0=无、1=有
        /// </summary>
        [DataMember]
        public virtual string Smoke { get; set; }

        /// <summary>
        /// 抽烟年数
        /// </summary>
        [DataMember]
        public virtual int SmokeYear { get; set; }

        /// <summary>
        /// 抽烟支数
        /// </summary>
        [DataMember]
        public virtual int SmokeNum { get; set; }

        /// <summary>
        /// 家族史
        /// </summary>
        [DataMember]
        public virtual string FamilyHistory { get; set; }

        /// <summary>
        /// 手术史
        /// </summary>
        [DataMember]
        public virtual string OperationHistory { get; set; }

        /// <summary>
        /// 药物过敏史
        /// </summary>
        [DataMember]
        public virtual string DrugAllergy { get; set; }

        /// <summary>
        /// 食物过敏史
        /// </summary>
        [DataMember]
        public virtual string FoodAllergy { get; set; }

        /// <summary>
        /// 透析器过敏史
        /// </summary>
        [DataMember]
        public virtual string DialyzerAllergy { get; set; }

        /// <summary>
        /// 透析起始日期
        /// </summary>
        [DataMember]
        public virtual DateTime DialysisBegin { get; set; }

        /// <summary>
        /// 透析终止日期
        /// </summary>
        [DataMember]
        public virtual DateTime DialysisEnd { get; set; }

        /// <summary>
        /// 腹膜透析，0=从未、1=曾经
        /// </summary>
        [DataMember]
        public virtual string PdExist { get; set; }

        /// <summary>
        /// 腹膜透析年数
        /// </summary>
        [DataMember]
        public virtual int PdYear { get; set; }

        /// <summary>
        /// 肾移植，0=从未、1=曾经
        /// </summary>
        [DataMember]
        public virtual string RenalTransplantExist { get; set; }

        /// <summary>
        /// 肾移植年数
        /// </summary>
        [DataMember]
        public virtual int RenalTransplantYear { get; set; }

        /// <summary>
        /// 转入日期
        /// </summary>
        [DataMember]
        public virtual DateTime IntoDate { get; set; }

        /// <summary>
        /// 转入医院
        /// </summary>
        [DataMember]
        public virtual string IntoHospital { get; set; }

        /// <summary>
        /// 透析终止原因
        /// </summary>
        [DataMember]
        public virtual string DialysisEndReason { get; set; }

        /// <summary>
        /// 死亡日期
        /// </summary>
        [DataMember]
        public virtual DateTime DeadDate { get; set; }

        /// <summary>
        /// 死亡原因
        /// </summary>
        [DataMember]
        public virtual string DeadReason { get; set; }

        /// <summary>
        /// 起病经过
        /// </summary>
        [DataMember]
        public virtual string OnsetPass { get; set; }

        /// <summary>
        /// 酗酒，0=无、1=有
        /// </summary>
        [DataMember]
        public virtual string Xujiu { get; set; }

        /// <summary>
        /// 酗酒描述
        /// </summary>
        [DataMember]
        public virtual string XujiuDesc { get; set; }

        /// <summary>
        /// 本院是否首次透析
        /// </summary>
        [DataMember]
        public virtual string Isfirstdialysis { get; set; }

        /// <summary>
        /// 透析年份
        /// </summary>
        [DataMember]
        public virtual string DialysisYears { get; set; }

        /// <summary>
        /// 痛风性肾病
        /// </summary>
        [DataMember]
        public virtual string Tfsb { get; set; }

        /// <summary>
        /// 用药记录
        /// </summary>
        [DataMember]
        public virtual string PROGRESSNODE { get; set; }





        [DataMember]
        public virtual int IsDelete { get; set; }

        [DataMember]
        public virtual string Editor { get; set; }

        [DataMember]
        public virtual DateTime Edittime { get; set; }

        [DataMember]
        public virtual string Creator { get; set; }

        [DataMember]
        public virtual DateTime Createtime { get; set; }


        #endregion

    }
}
