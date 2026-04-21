/*----------------------------------------------------------------
// Copyright (C) 2005 苏州XX公司有限公司
// 描述：患者病程记录报表
// 创建时间：2016-06-16
// 创建者：吕志强
//  
// 修改时间：
// 修改人：
// 修改描述：
----------------------------------------------------------------*/

using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using Hemo.Model;
using Hemo.IService;
using Hemo.Service;
using System.Linq;
using Hemo.IService.Config;
using System.Data;

namespace Hemo.Client.Print
{
    public partial class PatientProgressNoteReport : DevExpress.XtraReports.UI.XtraReport
    {
        #region 类变量

        private string hemoId;

        private string[] arrId;

        private PatientModel.MED_PATIENTSDataTable dtPatient;

        private IPatient patientService = ServiceManager.Instance.PatientService;

        private IHemodialysis hemoService = ServiceManager.Instance.HemodialysisService;

        private DataTable dtProgressNode;

        #endregion

        #region 属性

        /// <summary>
        /// 透析编号
        /// </summary>
        public string HemoId
        {
            get { return hemoId; }
            set { hemoId = value; }
        }

        /// <summary>
        /// 主键ID数组
        /// </summary>
        public string[] ArrId
        {
            get { return arrId; }
            set { arrId = value; }
        }

        #endregion

        #region 构造函数

        public PatientProgressNoteReport()
        {
            InitializeComponent();
        }

        #endregion

        #region 事件

        /// <summary>
        /// 打印前
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PatientProgressNoteReport_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            dtPatient = patientService.GetPatientListByParams(string.Empty, hemoId);
            if (dtPatient != null && dtPatient.Rows.Count > 0)
            {
                this.lblNAME.Text = dtPatient[0].NAME;
                this.lblSEX.Text = dtPatient[0].SEX;
                this.lblAGE.Text = dtPatient[0].AGE.ToString();
                this.lblHEMODIALYSIS_ID.Text = hemoId;
            }

            BindControl();
            arrId.ToList().ForEach(id =>
            {
                var table = hemoService.GetPatientProgressNoteById(id);
                if (dtProgressNode == null)
                {
                    dtProgressNode = table.Clone();
                }
                if (table != null && table.Rows.Count > 0)
                {
                    dtProgressNode.ImportRow(table.Rows[0]);
                }
            });
            this.DataSource = dtProgressNode;
        }

        #endregion

        #region 方法

        /// <summary>
        /// 绑定控件
        /// </summary>
        private void BindControl()
        {
            this.lblCREATE_DATE.DataBindings.AddRange(new XRBinding[] { new XRBinding("Text", null, "CREATE_DATE", "{0:yyyy-MM-dd}") });
            this.lblDOCTOR.DataBindings.AddRange(new XRBinding[] { new XRBinding("Text", null, "DOCTOR_NAME") });

            //不适主诉
            this.chkCOMPLAINTS_NOTHAS.DataBindings.AddRange(new XRBinding[] { new XRBinding("CheckState", null, "COMPLAINTS_NOTHAS") });
            this.chkCOMPLAINTS_HAS.DataBindings.AddRange(new XRBinding[] { new XRBinding("CheckState", null, "COMPLAINTS_HAS") });
            this.lblCOMPLAINTS_CONTENT.DataBindings.AddRange(new XRBinding[] { new XRBinding("Text", null, "COMPLAINTS_CONTENT") });

            //血管通路
            this.chkVASCULAR_ACCESS_NOCHANGE.DataBindings.AddRange(new XRBinding[] { new XRBinding("CheckState", null, "VASCULAR_ACCESS_NOCHANGE") });
            this.chkVASCULAR_ACCESS_CHANGE.DataBindings.AddRange(new XRBinding[] { new XRBinding("CheckState", null, "VASCULAR_ACCESS_CHANGE") });
            this.lblVASCULAR_ACCESS_CHANGE.DataBindings.AddRange(new XRBinding[] { new XRBinding("Text", null, "VASCULAR_ACCESS_CHANGE_DESC") });
            //this.chkVASCULAR_ACCESS_SUGGEST.DataBindings.AddRange(new XRBinding[] { new XRBinding("CheckState", null, "VASCULAR_ACCESS_SUGGEST") });
            //this.chkVASCULAR_ACCESS_ADJUST.DataBindings.AddRange(new XRBinding[] { new XRBinding("CheckState", null, "VASCULAR_ACCESS_ADJUST") });
            this.lblVASCULAR_ACCESS_NOTE.DataBindings.AddRange(new XRBinding[] { new XRBinding("Text", null, "VASCULAR_ACCESS_NOTE") });

            //抗凝治疗
            chkTHERAPEUTIC_METHOD_NOCHANGE.DataBindings.AddRange(new XRBinding[] { new XRBinding("CheckState", null, "THERAPEUTIC_METHOD_NOCHANGE") });
            chkTHERAPEUTIC_METHOD_CHANGE.DataBindings.AddRange(new XRBinding[] { new XRBinding("CheckState", null, "THERAPEUTIC_METHOD_CHANGE") });
            lblTHERAPEUTIC_METHOD_CHANGE.DataBindings.AddRange(new XRBinding[] { new XRBinding("Text", null, "THERAPEUTIC_METHOD_CHANGE_DESC") });
            //chkTHERAPEUTIC_METHOD_SUGGEST.DataBindings.AddRange(new XRBinding[] { new XRBinding("CheckState", null, "THERAPEUTIC_METHOD_SUGGEST") });
            //chkTHERAPEUTIC_METHOD_ADJUST.DataBindings.AddRange(new XRBinding[] { new XRBinding("CheckState", null, "THERAPEUTIC_METHOD_ADJUST") });
            lblTHERAPEUTIC_METHOD_NOTE.DataBindings.AddRange(new XRBinding[] { new XRBinding("Text", null, "THERAPEUTIC_METHOD_NOTE") });

            //容量控制
            this.chkCAPACITY_CONTROL_GOOD.DataBindings.AddRange(new XRBinding[] { new XRBinding("CheckState", null, "CAPACITY_CONTROL_GOOD") });
            this.chkCAPACITY_CONTROL_NOTGOOD.DataBindings.AddRange(new XRBinding[] { new XRBinding("CheckState", null, "CAPACITY_CONTROL_NOTGOOD") });
            this.lblDRY_WEIHGT.DataBindings.AddRange(new XRBinding[] { new XRBinding("Text", null, "DRY_WEIHGT") });
            this.lblMAX_DRY_WEIHGT.DataBindings.AddRange(new XRBinding[] { new XRBinding("Text", null, "MAX_DRY_WEIHGT") });
            this.lblPERCENT_DRY_WEIGHT.DataBindings.AddRange(new XRBinding[] { new XRBinding("Text", null, "PERCENT_DRY_WEIGHT") });

            //血压控制
            this.chkBLOOD_CONTROL_GOOD.DataBindings.AddRange(new XRBinding[] { new XRBinding("CheckState", null, "BLOOD_CONTROL_GOOD") });
            this.chkBLOOD_CONTROL_NOTGOOD.DataBindings.AddRange(new XRBinding[] { new XRBinding("CheckState", null, "BLOOD_CONTROL_NOTGOOD") });
            this.lblHIGH_BLOOD_PRESSURE.DataBindings.AddRange(new XRBinding[] { new XRBinding("Text", null, "HIGH_BLOOD_PRESSURE") });
            this.lblLOW_BLOOD_PRESSURE.DataBindings.AddRange(new XRBinding[] { new XRBinding("Text", null, "LOW_BLOOD_PRESSURE") });

            //贫血纠正
            this.chkANEMIA_CORRECTION_GOOD.DataBindings.AddRange(new XRBinding[] { new XRBinding("CheckState", null, "ANEMIA_CORRECTION_GOOD") });
            this.chkANEMIA_CORRECTION_NOTGOOD.DataBindings.AddRange(new XRBinding[] { new XRBinding("CheckState", null, "ANEMIA_CORRECTION_NOTGOOD") });
            this.lblANEMIA_CORRECTION_BAD.DataBindings.AddRange(new XRBinding[] { new XRBinding("Text", null, "ANEMIA_CORRECTION_BAD") });
            //this.chkANEMIA_CORRECTION_SUGGEST.DataBindings.AddRange(new XRBinding[] { new XRBinding("CheckState", null, "ANEMIA_CORRECTION_SUGGEST") });
            //this.chkANEMIA_CORRECTION_ADJUST.DataBindings.AddRange(new XRBinding[] { new XRBinding("CheckState", null, "ANEMIA_CORRECTION_ADJUST") });
            this.lblANEMIA_CORRECTION_NOTE.DataBindings.AddRange(new XRBinding[] { new XRBinding("Text", null, "ANEMIA_CORRECTION_NOTE") });

            //骨矿物质代谢絮乱控制
            this.chkBONE_MINERAL_GOOD.DataBindings.AddRange(new XRBinding[] { new XRBinding("CheckState", null, "BONE_MINERAL_GOOD") });
            this.chkBONE_MINERAL_NOTGOOD.DataBindings.AddRange(new XRBinding[] { new XRBinding("CheckState", null, "BONE_MINERAL_NOTGOOD") });
            this.lblBONE_MINERAL_BAD.DataBindings.AddRange(new XRBinding[] { new XRBinding("Text", null, "BONE_MINERAL_BAD") });
            //this.chkBONE_MINERAL_SUGGEST.DataBindings.AddRange(new XRBinding[] { new XRBinding("CheckState", null, "BONE_MINERAL_SUGGEST") });
            //this.chkBONE_MINERAL_ADJUST.DataBindings.AddRange(new XRBinding[] { new XRBinding("CheckState", null, "BONE_MINERAL_ADJUST") });
            this.lblBONE_MINERAL_NOTE.DataBindings.AddRange(new XRBinding[] { new XRBinding("Text", null, "BONE_MINERAL_NOTE") });

            //营养情况
            chkNUTRITIONAL_STATUS_GOOD.DataBindings.AddRange(new XRBinding[] { new XRBinding("CheckState", null, "NUTRITIONAL_STATUS_GOOD") });
            chkNUTRITIONAL_STATUS_NOTGOOD.DataBindings.AddRange(new XRBinding[] { new XRBinding("CheckState", null, "NUTRITIONAL_STATUS_NOTGOOD") });
            lblNUTRITIONAL_STATUS_BAD.DataBindings.AddRange(new XRBinding[] { new XRBinding("Text", null, "NUTRITIONAL_STATUS_BAD") });
            //chkNUTRITIONAL_STATUS_SUGGEST.DataBindings.AddRange(new XRBinding[] { new XRBinding("CheckState", null, "NUTRITIONAL_STATUS_SUGGEST") });
            //chkNUTRITIONAL_STATUS_ADJUST.DataBindings.AddRange(new XRBinding[] { new XRBinding("CheckState", null, "NUTRITIONAL_STATUS_ADJUST") });
            lblNUTRITIONAL_STATUS_NOTE.DataBindings.AddRange(new XRBinding[] { new XRBinding("Text", null, "NUTRITIONAL_STATUS_NOTE") });

            this.txtPROGRESS_NODE.DataBindings.AddRange(new XRBinding[] { new XRBinding("Rtf", null, "PROGRESS_NODE") });
        }

        #endregion
    }
}
