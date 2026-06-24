/*----------------------------------------------------------------
 * Copyright (C) 2005 (苏州)医疗科技发展有限公司
 * 文件功能描述:评估统计查询类
 * 创建标识:刘超-2017年4月23日
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
using Hemo.IService;
using Hemo.Service;
using Hemo.IService.Config;
using Hemo.Utilities;
using Hemo.Client.Core;
using Hemo.IService.Permission;
using Hemo.Client.UI.Hemodialysis;
using Hemo.Client.UI.Machine;

namespace Hemo.Client.UI.Assessment {
    public partial class NutritionSGAUI : ViewBase
    {
        #region 构造函数

        public NutritionSGAUI()
        {
            InitializeComponent();
        }

        #endregion
        
        #region 变量

        private IHemodialysis _hemodialysisService = ServiceManager.Instance.HemodialysisService;
        private IUser _userService = ServiceManager.Instance.UserService;
        private IPatient objPatient = ServiceManager.Instance.PatientService;

        private string currentHemoId = string.Empty;
        public string CurrentHemoId {
            get { return currentHemoId; }
            set { currentHemoId = value; }
        }

        #endregion

        #region 事件
        /// <summary>
        /// 加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NutritionSGAFrm_Load(object sender, EventArgs e) {
            //this.ctlMedicalUserInfo.HemoId = currentHemoId;
            //this.ctlMedicalUserInfo.LoadUserInfo();

            this.beginTime.EditValue = DateTime.Parse(DateTime.Now.Year.ToString() + "-" + "01" + "-" + "01");
            this.endTime.EditValue = DateTime.Now.Date;
            var userList = _userService.GetUserList();
            this.repositoryItemCustomGridLookUpEdit1.DataSource = userList;

            Query();
            this.Text = "营养性评估";
            //   ProFunctionCount pfc = new ProFunctionCount();
            //   pfc.SaveFunctionCountFrm(this);
        }


        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnQuery_Click(object sender, EventArgs e) {
            Query();
        }

        /// <summary>
        /// 打印
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnPrint_Click(object sender, EventArgs e) {
            if (this.gvInBasket.GetFocusedDataRow() != null) {
                //var doc = new 透析充分性评估();
                //doc.PatientRow = this.gvInBasket.GetFocusedDataRow() as PatientKolcabaModel.MED_PATIENT_SUFFICIENCYRow; ;
                //doc.Pname = currentHemoName;
                //doc.LoadDocumentInfo();
                //var frm = new ShowPrintForm(doc);
                //frm.StartPosition = FormStartPosition.CenterParent;
                //frm.ShowDialog();
            }

        }
        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAdd_Click(object sender, EventArgs e) {
            NutritionAssessment recordDetail = new NutritionAssessment();
            recordDetail.CurrentHemoId = currentHemoId;
            recordDetail.MedAssessRow = null;
            recordDetail.MedAssessTable = new HemoModel.MED_ASSESSMENTMASTERDataTable();
            DialogResult result = recordDetail.ShowDialog();
            if (result == DialogResult.OK) {
                Query();
            }
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDelete_Click(object sender, EventArgs e) {
            if (this.gvInBasket.GetFocusedDataRow() == null) {
                AutoClosedMsgBox.ShowForm("请选择一行要删除的记录！", this.Text, 1000, MessageBoxIcon.Asterisk);

                return;
            }

            if (XtraMessageBox.Show("是否确认删除当前选择记录？", "删除提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No) {
                return;
            }
            string id = this.gvInBasket.GetFocusedDataRow()["ASSESSMENT_ID"].ToString();
            int result = _hemodialysisService.DeleteAssessmentById(id, HemoApplicationContext.Current.CurrentUser.USER_NAME);

            if (result > 0) {
                AutoClosedMsgBox.ShowForm("删除记录成功！", this.Text, 1000, MessageBoxIcon.Asterisk);

                Query();
            }
        }

        /// <summary>
        /// 关闭
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnExit_Click(object sender, EventArgs e) {
            this.ParentForm.Close();
        }

        /// <summary>
        /// 列表行双击
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gvRecord_DoubleClick(object sender, EventArgs e) {
            editData();
        }
        /// <summary>
        /// 编辑
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnEdit_Click(object sender, EventArgs e) {
            editData();
        }
        #endregion

        #region 方法
        /// <summary>
        /// 去编辑选择的数据
        /// </summary>
        private void editData() {
            var dr = gvInBasket.GetFocusedDataRow() as HemoModel.MED_PATIENTS_ASSESSMENTRow;
            if (dr != null) {
                //根据ASSESSMENT_ID获取明细数据，包括属性ID和属性内容
                var patientAssessAttr = _hemodialysisService.GetAssessmentByAssID(dr.ASSESSMENT_ID);


                var masterTable = new HemoModel.MED_ASSESSMENTMASTERDataTable();
                HemoModel.MED_ASSESSMENTMASTERRow rowMaster = masterTable.NewMED_ASSESSMENTMASTERRow();
                rowMaster.ASSESSMENT_ID = dr.ASSESSMENT_ID;
                rowMaster.ASSESSMENT_DATE = dr.ASSESSMENT_DATE;
                rowMaster.ASSESSMENT_NOTE = dr.ASSESSMENT_NOTE;
                rowMaster.ASSESSMENT_TYPE = dr.ASSESSMENT_TYPE;
                rowMaster.HEMODIALYSIS_ID = dr.HEMODIALYSIS_ID;
                rowMaster.CREATE_USER = dr.CREATE_USER;
                foreach (HemoModel.MED_PATIENTS_ASSESSMENT_ATTRRow row in patientAssessAttr.Rows) {
                    if (row.ATTR_ID.Contains("cont") && !row.IsATTR_VALUENull())
                        rowMaster[string.Format("{0}", row.ATTR_ID)] = row.ATTR_VALUE;
                }

                masterTable.AddMED_ASSESSMENTMASTERRow(rowMaster);


                NutritionAssessment frm = new NutritionAssessment();
                frm.CurrentHemoId = CurrentHemoId;
                frm.MedAssessRow = rowMaster;
                frm.MedAssessTable = masterTable;
                if (frm.ShowDialog() == DialogResult.OK) {
                    Query();
                }
            }
            else {
                AutoClosedMsgBox.ShowForm("请选择一条要编辑的数据！", "系统提示", 1000, MessageBoxIcon.Warning);
            }
        }
        /// <summary>
        /// 查询
        /// </summary>
        public void Query() {
            HemoModel.MED_PATIENTS_ASSESSMENTDataTable dt = _hemodialysisService.GetAssessmentByParams(currentHemoId, Convert.ToDateTime(this.beginTime.DateTime.ToString()), Convert.ToDateTime(this.endTime.DateTime.ToString()));
            var dtSource = new HemoModel.MED_PATIENTS_ASSESSMENTDataTable();
            dt.Where(i => i.ASSESSMENT_TYPE == "患者营养性评估").CopyToDataTable<HemoModel.MED_PATIENTS_ASSESSMENTRow>(dtSource, LoadOption.PreserveChanges);

            if (dtSource != null && dtSource.Rows.Count > 0)
                this.gcInBasket.DataSource = dtSource;
            else
                this.gcInBasket.DataSource = null;
        }
        #endregion

    }
}