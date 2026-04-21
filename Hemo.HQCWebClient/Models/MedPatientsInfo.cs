using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Hemo.HQCWebClient.Models
{
    /// <summary>
    /// MedPatientsInfo，DTO对象
    /// </summary>
    [DataContract]
    public class MedPatientsInfo
    {
        /// <summary>
        /// 默认构造函数（需要初始化属性的在此处理）
        /// </summary>
        public MedPatientsInfo()
        {
      
        }

        #region Property Members

        /// <summary>
        /// 编号
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
        /// 透析号
        /// </summary>
        [DataMember]
        public virtual string HemodialysisId { get; set; }

        /// <summary>
        /// 病人ID
        /// </summary>
        [DataMember]
        public virtual string PatientId { get; set; }

        /// <summary>
        /// 病人姓名
        /// </summary>
        [DataMember]
        public virtual string Name { get; set; }

        /// <summary>
        /// 性别
        /// </summary>
        [DataMember]
        public virtual string Sex { get; set; }

        /// <summary>
        /// 生日
        /// </summary>
        [DataMember]
        public virtual DateTime Birthday { get; set; }

        /// <summary>
        /// 年龄
        /// </summary>
        [DataMember]
        public virtual int Age { get; set; }

        /// <summary>
        /// 籍贯
        /// </summary>
        [DataMember]
        public virtual string Nativeplace { get; set; }

        /// <summary>
        /// 工作
        /// </summary>
        [DataMember]
        public virtual string Job { get; set; }

        /// <summary>
        /// 婚姻
        /// </summary>
        [DataMember]
        public virtual string Marital { get; set; }

        /// <summary>
        /// 证件类型
        /// </summary>
        [DataMember]
        public virtual string CredentialsType { get; set; }

        /// <summary>
        /// 证件号码
        /// </summary>
        [DataMember]
        public virtual string CredentialsNumber { get; set; }

        /// <summary>
        /// 学历
        /// </summary>
        [DataMember]
        public virtual string Education { get; set; }

        /// <summary>
        /// 民族
        /// </summary>
        [DataMember]
        public virtual string Nation { get; set; }

        /// <summary>
        /// 工作电话
        /// </summary>
        [DataMember]
        public virtual string WorkTelephone { get; set; }

        /// <summary>
        /// 家庭地址
        /// </summary>
        [DataMember]
        public virtual string Address { get; set; }

        /// <summary>
        /// 医保类型
        /// </summary>
        [DataMember]
        public virtual string MedicalType { get; set; }

        /// <summary>
        /// 联系电话
        /// </summary>
        [DataMember]
        public virtual string Telephone { get; set; }

        /// <summary>
        /// 时间类型
        /// </summary>
        [DataMember]
        public virtual string TimeType { get; set; }

        /// <summary>
        /// 具体时间
        /// </summary>
        [DataMember]
        public virtual DateTime SpecificTime { get; set; }

        /// <summary>
        /// 住院号
        /// </summary>
        [DataMember]
        public virtual string AdmissionNumber { get; set; }

        /// <summary>
        /// 是否新入
        /// </summary>
        [DataMember]
        public virtual string IsNew { get; set; }

        /// <summary>
        /// 何院转入
        /// </summary>
        [DataMember]
        public virtual string WhatHospitalIn { get; set; }

        /// <summary>
        /// 何科转入
        /// </summary>
        [DataMember]
        public virtual string WhatDepartmentIn { get; set; }

        /// <summary>
        /// 初诊
        /// </summary>
        [DataMember]
        public virtual string FirstVisit { get; set; }

        /// <summary>
        /// 诊断
        /// </summary>
        [DataMember]
        public virtual string Diagnose { get; set; }

        /// <summary>
        /// 出院时间
        /// </summary>
        [DataMember]
        public virtual DateTime LeaveHospitalTime { get; set; }

        /// <summary>
        /// 传染病检验结果
        /// </summary>
        [DataMember]
        public virtual string InfectiousCheckResult { get; set; }

        /// <summary>
        /// 姓名输入码
        /// </summary>
        [DataMember]
        public virtual string InputCode { get; set; }

        /// <summary>
        /// 随访号
        /// </summary>
        [DataMember]
        public virtual int VisitId { get; set; }

        /// <summary>
        /// 所在病区代码
        /// </summary>
        [DataMember]
        public virtual string WardCode { get; set; }

        /// <summary>
        /// 床号
        /// </summary>
        [DataMember]
        public virtual string BedNo { get; set; }

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
