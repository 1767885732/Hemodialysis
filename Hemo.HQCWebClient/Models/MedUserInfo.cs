using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Hemo.HQCWebClient.Models
{
    /// <summary>
    /// 质控平台用户信息实体类
    /// </summary>
    [DataContract]
    public class MedUserInfo : SimpleUserInfo
    {
        public const int IdentityLen = 50;

        public MedUserInfo() { }

        [DataMember]
        public virtual string Address { get; set; }

        [DataMember]
        public virtual string AuditStatus { get; set; }

        [DataMember]
        public virtual DateTime Birthday { get; set; }

        [DataMember]
        public virtual string Company_ID { get; set; }

        [DataMember]
        public virtual string CompanyName { get; set; }

        [DataMember]
        public virtual DateTime CreateTime { get; set; }

        [DataMember]
        public virtual string Creator { get; set; }

        [DataMember]
        public virtual string Creator_ID { get; set; }

        [DataMember]
        public virtual string CurrentLoginIP { get; set; }

        [DataMember]
        public virtual DateTime CurrentLoginTime { get; set; }

        [DataMember]
        public virtual string CurrentMacAddress { get; set; }

        [DataMember]
        public virtual string CustomField { get; set; }

        [DataMember]
        public virtual bool Deleted { get; set; }

        [DataMember]
        public virtual string Dept_ID { get; set; }

        [DataMember]
        public virtual string DeptName { get; set; }

        [DataMember]
        public virtual string Editor { get; set; }

        [DataMember]
        public virtual string Editor_ID { get; set; }

        [DataMember]
        public virtual DateTime EditTime { get; set; }

        [DataMember]
        public virtual string Gender { get; set; }

        [DataMember]
        public virtual string HomePhone { get; set; }

        [DataMember]
        public virtual string IdentityCard { get; set; }

        [DataMember]
        public virtual bool IsExpire { get; set; }

        [DataMember]
        public virtual string Nickname { get; set; }

        [DataMember]
        public virtual string Note { get; set; }

        [DataMember]
        public virtual string OfficePhone { get; set; }

        [DataMember]
        public virtual int PID { get; set; }

        [DataMember]
        public virtual byte[] Portrait { get; set; }

        [DataMember]
        public virtual string QQ { get; set; }

        [DataMember]
        public virtual string Signature { get; set; }

        [DataMember]
        public virtual string SortCode { get; set; }

        [DataMember]
        public virtual string Title { get; set; }

        [DataMember]
        public virtual string WorkAddr { get; set; }
    }
}
