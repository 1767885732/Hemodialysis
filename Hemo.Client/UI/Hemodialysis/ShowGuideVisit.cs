/*----------------------------------------------------------------
 * Copyright (C) 2005 (苏州)医疗科技发展有限公司
 * 文件功能描述:用户控件类
 * 创建标识:刘超-2013年7月2日
 * 
 * 修改时间:2013年10月10日
 * 修改人:刘超
 * 修改描述:新增方法SQL
 * 
 * 修改时间:2014年1月18日
 * 修改人:刘超
 * 修改描述:新增方法SQL
 * 
 * 修改时间:2014年4月28日
 * 修改人:顾伟伟
 * 修改描述:修改方法
 * ----------------------------------------------------------------*/
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

namespace Hemo.Client.UI.Hemodialysis {
    public partial class ShowGuideVisit :HemoBaseFrm {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="patientRow"></param>
        public ShowGuideVisit(PatientModel.MED_PATIENTSRow patientRow) {
            InitializeComponent();
            if (patientRow != null) {
                ctlUserLongInfo1.HEMODIALYSIS_ID = patientRow.HEMODIALYSIS_ID;
                ctlUserLongInfo1.LoadPatientInfo();
                setImageStatus(patientRow);
            }
        }
        #region 事件
        private IHemodialysis _hemodialysisService = ServiceManager.Instance.HemodialysisService;
        private void setImageStatus(PatientModel.MED_PATIENTSRow patientRow) {
            int iTable = 0;
            StringBuilder sbInfo = new StringBuilder();
            if (patientRow != null) {
                DataSet ds = _hemodialysisService.GetPatientGuideVisitInfo(patientRow.HEMODIALYSIS_ID, System.DateTime.Now);
                iTable = ds.Tables.Count;
                if (!patientRow.IsSEXNull()) {
                    if (patientRow.SEX == "男") {
                        this.pic1.EditValue = global::Hemo.Client.Properties.Resources.nan;
                    }
                    else {
                        this.pic1.EditValue = global::Hemo.Client.Properties.Resources.nv;
                    }
                }
                lblDate1.Text = patientRow.CREATE_DATE.ToShortDateString();
                sbInfo.Append("系统提示：该患者的基本信息资料已建立");

                if (ds.Tables.Contains("MED_VASCULAR_ACCESS")) {
                    this.pic2.EditValue = global::Hemo.Client.Properties.Resources.xueguan_1;
                    lblDate2.Text = Utilities.Utility.CDate(ds.Tables["MED_VASCULAR_ACCESS"].Rows[0]["CREATE_DATE"].ToString()).ToShortDateString();
                    sbInfo.Append(",血管通路已建立");
                }
                else {
                    this.pic2.EditValue = global::Hemo.Client.Properties.Resources.xueguan_0;
                    arr2.EditValue = arr3.EditValue = arr4.EditValue = global::Hemo.Client.Properties.Resources.jiantou_0;
                    right2.Visible = right3.Visible = right4.Visible = right5.Visible = false;
                    sbInfo.Append(",血管通路未建立");
                }

                if (ds.Tables.Contains("MED_PATIENT_SCHEDULE")) {
                    this.pic3.EditValue = global::Hemo.Client.Properties.Resources.paiban_1;
                    lblDate3.Text = Utilities.Utility.CDate(ds.Tables["MED_PATIENT_SCHEDULE"].Rows[0]["DIALYSIS_DATE"].ToString()).ToShortDateString();
                    sbInfo.Append(",透析排班日期已确定");
                }
                else {
                    this.pic3.EditValue = global::Hemo.Client.Properties.Resources.paiban_0;
                    arr3.EditValue = arr4.EditValue = global::Hemo.Client.Properties.Resources.jiantou_0;
                    right3.Visible = right4.Visible = right5.Visible = false;
                    sbInfo.Append(",透析排班日期未确定");
                }

                if (ds.Tables.Contains("MED_PATIENT_SCHEDULE")) {
                    DataTable recipe = ds.Tables["MED_PATIENT_SCHEDULE"];
                    //recipe = Utilities.Utility.GetSubTable(recipe, "status='1'");
                    if (recipe != null && recipe.Rows[0]["dialysis_date"].ToString().Length > 0) {
                        sbInfo.Append(",血透医嘱已确定");
                        lblDate4.Text = Utilities.Utility.CDate(ds.Tables["MED_PATIENT_SCHEDULE"].Rows[0]["dialysis_date"].ToString()).ToShortDateString();
                        this.pic4.EditValue = global::Hemo.Client.Properties.Resources.yizhu_1;
                    }
                    //else if (recipe != null && recipe.Rows.Count > 1) {
                    //    sbInfo.Append(",请确认一张有效的血透医嘱");
                    //}
                    else {
                        this.pic4.EditValue = global::Hemo.Client.Properties.Resources.yizhu_0;
                        arr4.EditValue = global::Hemo.Client.Properties.Resources.jiantou_0;
                        right4.Visible = right5.Visible = false;
                        sbInfo.Append(",血透医嘱未确定");
                    }
                }
                else {
                    this.pic4.EditValue = global::Hemo.Client.Properties.Resources.yizhu_0;
                    arr4.EditValue = global::Hemo.Client.Properties.Resources.jiantou_0;
                    right4.Visible = right5.Visible = false;
                    sbInfo.Append(",血透医嘱未确定");
                }

                if (ds.Tables.Contains("GetCureList")) {
                    this.pic5.EditValue = global::Hemo.Client.Properties.Resources.zhiliao_1;
                    lblDate5.Text = Utilities.Utility.CDate(ds.Tables["GetCureList"].Rows[0]["CURE_CREATE_DATE"].ToString()).ToShortDateString();
                    sbInfo.Append(",治疗单已执行。");
                }
                else {
                    this.pic5.EditValue = global::Hemo.Client.Properties.Resources.zhiliao_0;
                    right5.Visible = false;
                    sbInfo.Append(",治疗单未执行。");
                }
            }
            lblInfo.Text = sbInfo.ToString();
        }

        private void pic1_MouseHover(object sender, EventArgs e) {
        }

        private void pic1_MouseLeave(object sender, EventArgs e) {
        }

        private void pic2_MouseHover(object sender, EventArgs e) {

        }

        private void pic2_MouseLeave(object sender, EventArgs e) {

        }
        #endregion
    }
}