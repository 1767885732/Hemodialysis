using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Hemo.HQCWebClient.Models
{
    [DataContract]
    public class MED_CURE_MAIN
    {
        /// <summary>
        /// 默认构造函数（需要初始化属性的在此处理）
        /// </summary>
        public MED_CURE_MAIN()
        {
        }

        #region Property Members

        /// <summary>
        /// 主键
        /// </summary>
        [DataMember]
        public virtual string ID { get; set; }

        /// <summary>
        /// 医院ID
        /// </summary>
        [DataMember]
        public virtual string HospitalId { get; set; }

        /// <summary>
        /// 上报日期
        /// </summary>
        [DataMember]
        public virtual DateTime HospitalYear { get; set; }

        /// <summary>
        /// 医院名称
        /// </summary>
        [DataMember]
        public virtual string HospitalName { get; set; }

        /// <summary>
        /// 治疗单编号
        /// </summary>
        [DataMember]
        public virtual string CURE_ID { get; set; }

        /// <summary>
        /// 处方编号
        /// </summary>
        [DataMember]
        public virtual string RECIPE_ID { get; set; }

        [DataMember]
        public virtual string PatientSysID { get; set; }

        /// <summary>
        /// 32323
        /// </summary>
        [DataMember]
        public virtual string HEMODIALYSIS_ID { get; set; }

        [DataMember]
        public virtual string RECIPE_TYPE { get; set; }

        [DataMember]
        public virtual decimal? CALCIUM_ION { get; set; }

        [DataMember]
        public virtual string CURE_STATUS { get; set; }

        [DataMember]
        public virtual string DOCTOR_ID { get; set; }

        [DataMember]
        public virtual DateTime RECIPE_DATE { get; set; }

        [DataMember]
        public virtual decimal? BLOOW_FLOW { get; set; }

        [DataMember]
        public virtual decimal? DIALYSATE_FLOW { get; set; }

        [DataMember]
        public virtual decimal? DIALYSATE_TEMPERATURE { get; set; }

        [DataMember]
        public virtual decimal? UFR { get; set; }

        [DataMember]
        public virtual decimal? SODION { get; set; }

        [DataMember]
        public virtual decimal? POTASSIUM_ION { get; set; }

        [DataMember]
        public virtual DateTime PERFORM_SCHEDULE { get; set; }

        [DataMember]
        public virtual string NURSE_ID { get; set; }

        [DataMember]
        public virtual string PURIFICATION_MODE { get; set; }

        [DataMember]
        public virtual decimal? CLEAN_UP_TIMES { get; set; }

        [DataMember]
        public virtual decimal? FREQUENCY_HOURS { get; set; }

        [DataMember]
        public virtual DateTime BEGIN_TIME { get; set; }

        [DataMember]
        public virtual DateTime END_TIME { get; set; }

        [DataMember]
        public virtual decimal? LAST_TIME_DRY_WEIGHT { get; set; }

        [DataMember]
        public virtual decimal? BEFORE_DRY_WEIGHT { get; set; }

        [DataMember]
        public virtual decimal? AFTER_DRY_WEIGHT { get; set; }

        [DataMember]
        public virtual decimal? BEFORE_SYSTOLIC_PRESSURE { get; set; }

        [DataMember]
        public virtual decimal? BEFORE_DIASTOLIC_PRESSURE { get; set; }

        [DataMember]
        public virtual decimal? AFTER_SYSTOLIC_PRESSURE { get; set; }

        [DataMember]
        public virtual decimal? AFTER_DIASTOLIC_PRESSURE { get; set; }

        [DataMember]
        public virtual decimal? DRY_WATER_VALUE { get; set; }

        [DataMember]
        public virtual decimal? BEFORE_TEMPERATURE { get; set; }

        [DataMember]
        public virtual decimal? AFTER_TEMPERATURE { get; set; }

        [DataMember]
        public virtual decimal? BEFORE_HEART_RATE { get; set; }

        [DataMember]
        public virtual decimal? AFTER_HEART_RATE { get; set; }

        [DataMember]
        public virtual string PRIMARY_NURSE { get; set; }

        [DataMember]
        public virtual string PRIMARY_DOCTOR { get; set; }

        [DataMember]
        public virtual string PUNCTURE_NURSE { get; set; }

        [DataMember]
        public virtual string MACHINE_ID { get; set; }

        [DataMember]
        public virtual string VASCULAR_ACCESS_ID { get; set; }

        [DataMember]
        public virtual string HEPARIN_SPECIES { get; set; }

        [DataMember]
        public virtual decimal? FIRST_HEPARIN { get; set; }

        [DataMember]
        public virtual decimal? DOSIS_SUSTENTATIVA { get; set; }

        [DataMember]
        public virtual string MACHINE_TYPE { get; set; }

        [DataMember]
        public virtual string PURIFIER_NAME { get; set; }

        [DataMember]
        public virtual decimal? PURIFIER_M2 { get; set; }

        [DataMember]
        public virtual string USE_TYPE { get; set; }

        [DataMember]
        public virtual decimal? REUSE_TIMES { get; set; }

        [DataMember]
        public virtual string A_LIQUID { get; set; }

        [DataMember]
        public virtual string B_LIQUID { get; set; }

        [DataMember]
        public virtual decimal? BIRCARBONATE { get; set; }

        [DataMember]
        public virtual decimal? AMYLACEUM { get; set; }

        [DataMember]
        public virtual string SUMMARY { get; set; }

        [DataMember]
        public virtual DateTime CURE_CREATE_DATE { get; set; }

        [DataMember]
        public virtual string VASCULAR_ACCESS_FIRM { get; set; }

        [DataMember]
        public virtual string VASCULAR_ACCESS_GLIDE { get; set; }

        [DataMember]
        public virtual string VASCULAR_ACCESS_SWELLING { get; set; }

        [DataMember]
        public virtual string VASCULAR_ACCESS_ERRHYISIS { get; set; }

        [DataMember]
        public virtual string VASCULAR_ACCESS_THROMBUS { get; set; }

        [DataMember]
        public virtual string VASCULAR_ACCESS_BLOOD { get; set; }

        [DataMember]
        public virtual decimal? FILTRATION_DISPLACEMENT_LIQUID { get; set; }

        [DataMember]
        public virtual decimal? FILTRATION_PERCOLATE { get; set; }

        [DataMember]
        public virtual decimal? DISPLACEMENT_LIQUID { get; set; }

        [DataMember]
        public virtual decimal? PERCOLATE { get; set; }

        [DataMember]
        public virtual string DOCTOR_ADVICE { get; set; }

        [DataMember]
        public virtual string SUMMARY2 { get; set; }

        [DataMember]
        public virtual string CHECK_NURSE { get; set; }

        [DataMember]
        public virtual string FIRST_DRUG_UNIT { get; set; }

        [DataMember]
        public virtual string SECOND_DRUG_UNIT { get; set; }

        [DataMember]
        public virtual string VEIN { get; set; }

        [DataMember]
        public virtual string BEFORE_DRY_WEIGHT_TAG { get; set; }

        [DataMember]
        public virtual string AFTER_DRY_WEIGHT_TAG { get; set; }

        [DataMember]
        public virtual string REUSE_TIMES_TAG { get; set; }

        [DataMember]
        public virtual string MACHINE_ID_TAG { get; set; }

        [DataMember]
        public virtual string BLOOD_UP { get; set; }

        [DataMember]
        public virtual string BLOOD_TYPE { get; set; }

        [DataMember]
        public virtual string BLOOD_TRANSFUSION { get; set; }

        [DataMember]
        public virtual string COAGULATION_IN_DIALYSER { get; set; }

        [DataMember]
        public virtual string IN_BASKET_CLEAN { get; set; }

        [DataMember]
        public virtual string IN_BASKET_RED_HOT { get; set; }

        [DataMember]
        public virtual string IN_BASKET_ECCHYMOSIS { get; set; }

        [DataMember]
        public virtual string IN_BASKET_TREMOR { get; set; }

        [DataMember]
        public virtual string IN_BASKET_NOISE { get; set; }

        [DataMember]
        public virtual string IN_BASKET_VASCULAR_ELASTICITY { get; set; }

        [DataMember]
        public virtual string IN_BASKET_VASCULAR_OTHER { get; set; }

        [DataMember]
        public virtual string IN_BASKET_WOUND_ALLERGY { get; set; }

        [DataMember]
        public virtual string IN_BASKET_PLASTER_ALLERGY { get; set; }

        [DataMember]
        public virtual string VASCULAR_ACCESS_TYPE { get; set; }

        [DataMember]
        public virtual string SUBJECTIVE_COMFORT { get; set; }

        [DataMember]
        public virtual decimal? DRY_WEIGHT { get; set; }

        [DataMember]
        public virtual string DRY_WEIGHT_TAG { get; set; }

        [DataMember]
        public virtual string VASCULAR_ACCESS_STATE { get; set; }

        [DataMember]
        public virtual string MACHINE_STATUS { get; set; }

        [DataMember]
        public virtual decimal? BEFORE_BP { get; set; }

        [DataMember]
        public virtual decimal? AFTER_BP { get; set; }

        [DataMember]
        public virtual string SUMMARY3 { get; set; }

        [DataMember]
        public virtual string WHAT_DEPARTMENT_IN { get; set; }

        [DataMember]
        public virtual string INFECTIOUS_CHECK_RESULT { get; set; }

        [DataMember]
        public virtual decimal? FREQUENCY_MINUTE { get; set; }

        [DataMember]
        public virtual string DISPLACEMENT_MODE { get; set; }

        [DataMember]
        public virtual string DISPLACEMENT_RECIPE { get; set; }

        [DataMember]
        public virtual string DISPLACEMENT_SPECIAL_ADJUST { get; set; }

        [DataMember]
        public virtual string ANTICOAGULANT_USE { get; set; }

        [DataMember]
        public virtual string SPECIAL_MATTER { get; set; }

        [DataMember]
        public virtual decimal? UFR2 { get; set; }

        [DataMember]
        public virtual decimal? DISPLACEMENT_FLOW { get; set; }

        [DataMember]
        public virtual decimal? UF { get; set; }

        [DataMember]
        public virtual decimal? SUM_UF { get; set; }

        [DataMember]
        public virtual string VASCULAR_ACCESS_BLOOD_INFECT { get; set; }

        /// <summary>
        /// 删除标识
        /// </summary>
        [DataMember]
        public virtual int IsDelete { get; set; }

        /// <summary>
        /// 修改人
        /// </summary>
        [DataMember]
        public virtual string Editor { get; set; }

        /// <summary>
        /// 修改时间
        /// </summary>
        [DataMember]
        public virtual DateTime Edittime { get; set; }

        /// <summary>
        /// 创建人
        /// </summary>
        [DataMember]
        public virtual string Creator { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        [DataMember]
        public virtual DateTime Creattime { get; set; }

        /// <summary>
        /// 扩展字段
        /// </summary>
        [DataMember]
        public virtual string EXTEND_COL { get; set; }

        #endregion
    }
}
