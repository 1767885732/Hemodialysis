using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Hemo.HQCWebClient.Models
{
    [DataContract]
    public class MedDictData
    {
        public MedDictData() { }

        [DataMember]
        public virtual string DictType_ID { get; set; }

        [DataMember]
        public virtual string Editor { get; set; }

        [DataMember]
        public virtual string ID { get; set; }

        [DataMember]
        public virtual DateTime LastUpdated { get; set; }

        [DataMember]
        public virtual string Name { get; set; }

        [DataMember]
        public virtual string Remark { get; set; }

        [DataMember]
        public virtual string Seq { get; set; }

        [DataMember]
        public virtual string Value { get; set; }
    }
}
