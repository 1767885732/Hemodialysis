using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Hemo.HQCWebClient.Models
{
    [DataContract]
    public class SimpleUserInfo
    {
        public SimpleUserInfo() { }

        [DataMember]
        public virtual string Email { get; set; }

        [DataMember]
        public virtual string FullName { get; set; }

        [DataMember]
        public virtual string HandNo { get; set; }

        [DataMember]
        public virtual int ID { get; set; }

        [DataMember]
        public virtual string MobilePhone { get; set; }

        [DataMember]
        public virtual string Name { get; set; }

        [DataMember]
        public virtual string Password { get; set; }
    }
}
