using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Hemo.HQCWebClient.Models
{
    [DataContract]
    public class MED_HEMODIALYSIS_PARAMETERS
    {
        public MED_HEMODIALYSIS_PARAMETERS()
		{
            this.ID= System.Guid.NewGuid().ToString();
 		}

        #region Property Members
        
		[DataMember]
        public virtual string ID { get; set; }

		[DataMember]
        public virtual string CURE_ID { get; set; }

		[DataMember]
        public virtual string RECIPE_ID { get; set; }

		[DataMember]
        public virtual DateTime CREATE_DATE { get; set; }

		[DataMember]
        public virtual decimal? VENOUS_PRESSURE { get; set; }

		[DataMember]
        public virtual decimal? TRANSMEMBRANE_PRESSURE { get; set; }

		[DataMember]
        public virtual decimal? TEMPERATURE { get; set; }

		[DataMember]
        public virtual decimal? SYSTOLIC_PRESSURE { get; set; }

		[DataMember]
        public virtual decimal? DIASTOLIC_PRESSURE { get; set; }

		[DataMember]
        public virtual decimal? CARDIOTACH { get; set; }

		[DataMember]
        public virtual decimal? BREATH { get; set; }

		[DataMember]
        public virtual string KT_V { get; set; }

		[DataMember]
        public virtual string CURE_MODE { get; set; }

		[DataMember]
        public virtual string CLINICAL_MANIFESTATION { get; set; }

		[DataMember]
        public virtual decimal? BLOOD_FLOW { get; set; }

		[DataMember]
        public virtual decimal? SODIUM_ION { get; set; }

		[DataMember]
        public virtual decimal? DIALYSATE_RATE { get; set; }

		[DataMember]
        public virtual decimal? URF { get; set; }

		[DataMember]
        public virtual decimal? CONDUCTIVITY { get; set; }

		[DataMember]
        public virtual string NURSE_ID { get; set; }

		[DataMember]
        public virtual decimal? DISPLACEMENT { get; set; }

		[DataMember]
        public virtual string VASCULAR_ACCESS_ERRHYISIS { get; set; }

		[DataMember]
        public virtual string VASCULAR_ACCESS_GLIDE { get; set; }

		[DataMember]
        public virtual string EXTENDED_FIELD_1 { get; set; }

		[DataMember]
        public virtual string EXTENDED_FIELD_2 { get; set; }

		[DataMember]
        public virtual string EXTENDED_FIELD_3 { get; set; }

		[DataMember]
        public virtual string EXTENDED_FIELD_4 { get; set; }

		[DataMember]
        public virtual string EXTENDED_FIELD_5 { get; set; }

		[DataMember]
        public virtual decimal? ANTICOAGULANT { get; set; }

		[DataMember]
        public virtual string VENOUS_PRESSURE_UNIT { get; set; }

		[DataMember]
        public virtual string ANTICOAGULANTUNIT { get; set; }

		[DataMember]
        public virtual decimal? ARTERY_PRESSURE { get; set; }

		[DataMember]
        public virtual string CRRT_CLASS { get; set; }

        #endregion
    }
}
