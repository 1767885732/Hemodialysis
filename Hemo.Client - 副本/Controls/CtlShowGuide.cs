/*----------------------------------------------------------------
// Copyright (C) 2013 苏州XX公司有限公司
// 描述：透析导向用户控件 
// 创建时间：2013-08-20
// 创建者：刘超
//  
// 修改时间：
// 修改人：
// 修改描述：
//
// 修改时间：
// 修改人：
// 修改描述：
----------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Hemo.Model;
using Hemo.IService.Config;
using Hemo.Service;

namespace Hemo.Client.Controls {
    public partial class CtlShowGuide : DevExpress.XtraEditors.XtraUserControl {
        public CtlShowGuide() {
            InitializeComponent();
        }


        #region 显示病人透析导航的状态导航图片
        private IHemodialysis _hemodialysisService = ServiceManager.Instance.HemodialysisService;
        public string SetImageStatus(PatientModel.MED_PATIENTSRow patientRow) {
            int iTable = 0;
            StringBuilder sbInfo = new StringBuilder();
            if (patientRow != null) {
                DataSet ds = _hemodialysisService.GetPatientGuideVisitInfo(patientRow.HEMODIALYSIS_ID, System.DateTime.Now);
                iTable = ds.Tables.Count;
                if (!patientRow.IsSEXNull()) {
                    if (patientRow.SEX == "男") {
                        this.pic1.EditValue = global::Hemo.Client.Properties.Resources.nan_s;
                    }
                    else {
                        this.pic1.EditValue = global::Hemo.Client.Properties.Resources.nv_s;
                    }
                    if (patientRow.NAME.Length == 2) {
                        lblTitle1.Text = "  " + patientRow.NAME;
                    }
                    else {
                        lblTitle1.Text = patientRow.NAME;
                    }
                }
                lblDate1.Text = patientRow["CREATE_DATE"] != DBNull.Value ? patientRow.CREATE_DATE.ToShortDateString() : lblDate1.Text;
                sbInfo.Append("患者【" + patientRow.NAME + "】的基本信息资料已建立,\r\n");
                this.pic1.ToolTip = "该患者的基本信息资料已建立";

                if (ds.Tables.Contains("MED_VASCULAR_ACCESS")) {
                    this.pic2.EditValue = global::Hemo.Client.Properties.Resources.xueguan_1_s;
                    lblDate2.Text = Utilities.Utility.CDate(ds.Tables["MED_VASCULAR_ACCESS"].Rows[0]["CREATE_DATE"].ToString()).ToShortDateString();
                    sbInfo.Append("血管通路已建立,");
                    right2.Visible = true;
                    this.pic2.ToolTip = "血管通路已建立";
                }
                else {
                    this.pic2.EditValue = global::Hemo.Client.Properties.Resources.xueguan_0_s;
                    this.pic2.ToolTip = "血管通路未建立";
                    arr2.EditValue = arr3.EditValue = arr4.EditValue = global::Hemo.Client.Properties.Resources.jiantou_shu_0;
                    right2.Visible = right3.Visible = right4.Visible = right5.Visible = false;
                    sbInfo.Append("血管通路未建立,");
                }

                if (ds.Tables.Contains("MED_PATIENT_SCHEDULE")) {
                    this.pic3.EditValue = global::Hemo.Client.Properties.Resources.paiban_1_s;
                    lblDate3.Text = Utilities.Utility.CDate(ds.Tables["MED_PATIENT_SCHEDULE"].Rows[0]["DIALYSIS_DATE"].ToString()).ToShortDateString();
                    sbInfo.Append("透析排班日期已确定,\r\n");
                    this.pic3.ToolTip = "透析排班日期已确定";
                    right3.Visible = true;
                }
                else {
                    this.pic3.EditValue = global::Hemo.Client.Properties.Resources.paiban_0_s;
                    arr3.EditValue = arr4.EditValue = global::Hemo.Client.Properties.Resources.jiantou_shu_0;
                    right3.Visible = right4.Visible = right5.Visible = false;
                    sbInfo.Append("透析排班日期未确定,\r\n");
                    this.pic3.ToolTip = "透析排班日期未确定";
                }

                if (ds.Tables.Contains("MED_PATIENT_SCHEDULE")) {
                    DataTable recipe = ds.Tables["MED_PATIENT_SCHEDULE"];
                    //recipe = Utilities.Utility.GetSubTable(recipe, "status='1'");
                    if (recipe != null && recipe.Rows[0]["recipe_id"].ToString().Length > 0) {
                        sbInfo.Append("血透医嘱已确定,");
                        right4.Visible = true;
                        this.pic4.ToolTip = "血透医嘱已确定";
                        lblDate4.Text = Utilities.Utility.CDate(ds.Tables["MED_PATIENT_SCHEDULE"].Rows[0]["dialysis_date"].ToString()).ToShortDateString();
                        this.pic4.EditValue = global::Hemo.Client.Properties.Resources.yizhu_1_s;
                    }
                    //else if (recipe != null && recipe.Rows.Count > 1) {
                    //    sbInfo.Append(",请确认一张有效的血透医嘱");
                    //}
                    else {
                        this.pic4.EditValue = global::Hemo.Client.Properties.Resources.yizhu_0_s;
                        arr4.EditValue = global::Hemo.Client.Properties.Resources.jiantou_shu_0;
                        right4.Visible = right5.Visible = false;
                        sbInfo.Append("血透医嘱未确定,");
                        this.pic4.ToolTip = "血透医嘱未确定";
                    }
                }
                else {
                    this.pic4.EditValue = global::Hemo.Client.Properties.Resources.yizhu_0_s;
                    arr4.EditValue = global::Hemo.Client.Properties.Resources.jiantou_shu_0;
                    right4.Visible = right5.Visible = false;
                    sbInfo.Append("血透医嘱未确定,");
                    this.pic4.ToolTip = "血透医嘱未确定";
                }

                if (ds.Tables.Contains("GetCureList")) {
                    this.pic5.EditValue = global::Hemo.Client.Properties.Resources.zhiliao_1_s;
                    lblDate5.Text = Utilities.Utility.CDate(ds.Tables["GetCureList"].Rows[0]["CURE_CREATE_DATE"].ToString()).ToShortDateString();
                    sbInfo.Append("治疗单已执行。\r\n");
                    this.pic5.ToolTip = "治疗单已执行";
                    right5.Visible = true;
                }
                else {
                    this.pic5.EditValue = global::Hemo.Client.Properties.Resources.zhiliao_0_s;
                    right5.Visible = false;
                    sbInfo.Append("治疗单未执行。\r\n");
                    this.pic5.ToolTip = "治疗单未执行";
                }
            }
            return sbInfo.ToString();
        }
        #endregion
     
    }
}
