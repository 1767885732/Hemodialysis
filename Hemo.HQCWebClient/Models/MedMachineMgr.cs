/*----------------------------------------------------------------
 * Copyright (C) 2005 (苏州)医疗科技发展有限公司
 * 文件功能描述:MedMachineMgr实体类
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
    public class MedMachineMgr
    {
        /// <summary>
        /// 默认构造函数
        /// </summary>
        public MedMachineMgr()
		{
            this.ID= Guid.NewGuid().ToString();
 		}

        #region 属性
        
		[DataMember]
        public virtual string ID { get; set; }

		[DataMember]
        public virtual string HospitalId { get; set; }

		[DataMember]
        public virtual DateTime HospitalYear { get; set; }

		[DataMember]
        public virtual string HospitalName { get; set; }

		[DataMember]
        public virtual string MachineType { get; set; }

		[DataMember]
        public virtual string MachineTypeName { get; set; }

		[DataMember]
        public virtual string MachineId { get; set; }

		[DataMember]
        public virtual string MachineName { get; set; }

		[DataMember]
        public virtual int MachineCount { get; set; }

		[DataMember]
        public virtual int IsDelete { get; set; }

		[DataMember]
        public virtual string Editor { get; set; }

		[DataMember]
        public virtual DateTime Edittime { get; set; }

		[DataMember]
        public virtual string Creator { get; set; }

		[DataMember]
        public virtual DateTime? Creattime { get; set; }

        #endregion
    }
}
