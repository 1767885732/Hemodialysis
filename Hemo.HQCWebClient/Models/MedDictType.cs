/*----------------------------------------------------------------
 * Copyright (C) 2005 (苏州)医疗科技发展有限公司
 * 文件功能描述:MedDictType实体类
 * 创建标识:吕志强-2017年10月24日
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
    public class MedDictType
    {
        public MedDictType() { }

        [DataMember]
        public virtual string Editor { get; set; }

        [DataMember]
        public virtual string ID { get; set; }

        [DataMember]
        public virtual DateTime LastUpdated { get; set; }

        [DataMember]
        public virtual string Name { get; set; }

        [DataMember]
        public string PID { get; set; }

        [DataMember]
        public virtual string Remark { get; set; }

        [DataMember]
        public virtual string Seq { get; set; }
    }
}
