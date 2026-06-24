/*----------------------------------------------------------------
 * Copyright (C) 2005 (苏州)医疗科技发展有限公司
 * 文件功能描述:患者评估列表类
 * 创建标识:刘超-2016年4月28日
 * ----------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Hemo.IService.Config;
using Hemo.Service;
using Hemo.Client.Print;
using Hemo.IService;
using Hemo.Client.Core;
using Hemo.Model;
using Hemo.Utilities;
using Hemo.Client.UI.Machine;
using Hemo.Client.UI.Lab;

namespace Hemo.Client.UI.Patient {
    public partial class AssessmentListUI : ViewBase
    {
        #region 类变量

        private IHemodialysis hemodialysisService = ServiceManager.Instance.HemodialysisService;
        private IPatient objPatient = ServiceManager.Instance.PatientService;

        private PatientModel.MED_PATIENTSRow _patient;

        #endregion

        #region 属性

        public PatientModel.MED_PATIENTSRow patient
        {
            get
            {
                return _patient;
            }
            set
            {
                _patient = value;
            }
        }

        public string HemoID { get; set; }

        #endregion

        #region 构造函数

        public AssessmentListUI()
        {
            InitializeComponent();
        }

        #endregion

        #region 事件

        /// <summary>
        /// 加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AssessmentListFrm_Load(object sender, EventArgs e)
        {
            LoadInfo();
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_Query_Click(object sender, EventArgs e)
        {
            InzationData(HemoID);
        }

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_Entering_Click(object sender, EventArgs e)
        {

            var dt = hemodialysisService.GetAssParamByHemoID(HemoID);

            AssessmentEditFrm frm = new AssessmentEditFrm(null, null, patient);
            frm.HemoID = HemoID;
            frm.recipeDt = dt;
            if (frm.ShowDialog() == DialogResult.OK)
            {
                InzationData(HemoID);
            }
        }

        /// <summary>
        /// 关闭
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.ParentForm.Close();
        }

        private void gvInBasket_DoubleClick(object sender, EventArgs e)
        {
            editData();
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (this.gvInBasket.GetFocusedDataRow() == null)
            {
                AutoClosedMsgBox.ShowForm("请选择一行要删除的记录！", this.Text, 1000, MessageBoxIcon.Asterisk);

                return;
            }

            if (XtraMessageBox.Show("是否确认删除当前选择记录？", "删除提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            {
                return;
            }

            string id = this.gvInBasket.GetFocusedDataRow()["ASSESSMENT_ID"].ToString();
            int result = hemodialysisService.DeleteAssessmentById(id, HemoApplicationContext.Current.CurrentUser.USER_NAME);

            if (result > 0)
            {
                AutoClosedMsgBox.ShowForm("删除记录成功！", this.Text, 1000, MessageBoxIcon.Asterisk);

                InzationData(HemoID);
            }
        }

        /// <summary>
        /// 编辑
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnEdit_Click(object sender, EventArgs e)
        {
            editData();
        }

        #endregion

        #region 方法

        /// <summary>
        /// 编辑数据
        /// </summary>
        private void editData()
        {
            var dr = gvInBasket.GetFocusedDataRow() as HemoModel.MED_PATIENTS_ASSESSMENTRow;
            if (dr != null)
            {
                //根据ASSESSMENT_ID获取明细数据，包括属性ID和属性内容
                var patientAssessAttr = hemodialysisService.GetAssessmentByAssID(dr.ASSESSMENT_ID);


                var masterTable = new HemoModel.MED_ASSESSMENTMASTERDataTable();
                HemoModel.MED_ASSESSMENTMASTERRow rowMaster = masterTable.NewMED_ASSESSMENTMASTERRow();
                rowMaster.ASSESSMENT_ID = dr.ASSESSMENT_ID;
                rowMaster.ASSESSMENT_DATE = dr.ASSESSMENT_DATE;
                rowMaster.ASSESSMENT_NOTE = dr.ASSESSMENT_NOTE;
                rowMaster.ASSESSMENT_TYPE = dr.ASSESSMENT_TYPE;
                rowMaster.HEMODIALYSIS_ID = dr.HEMODIALYSIS_ID;
                rowMaster.CREATE_USER = dr.CREATE_USER;
                foreach (HemoModel.MED_PATIENTS_ASSESSMENT_ATTRRow row in patientAssessAttr.Rows)
                {
                    if (row.ATTR_ID.Contains("cont") && !row.IsATTR_VALUENull() || row.ATTR_ID.Contains("cmbFIRST_PURIFIER_MODEL"))
                        rowMaster[string.Format("{0}", row.ATTR_ID)] = row.ATTR_VALUE;

                }

                masterTable.AddMED_ASSESSMENTMASTERRow(rowMaster);


                AssessmentEditFrm frm = new AssessmentEditFrm(masterTable, rowMaster, null);
                frm.HemoID = HemoID;
                frm.AssessmentID = dr.ASSESSMENT_ID.ToString();
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    InzationData(HemoID);
                }
            }
            else
            {
                AutoClosedMsgBox.ShowForm("请选择一条要编辑的数据！", "系统提示", 1000, MessageBoxIcon.Warning);
            }
        }

        /// <summary>
        /// 加载数据
        /// </summary>
        public void LoadInfo()
        {
            if (string.IsNullOrEmpty(HemoID))
            {
                AutoClosedMsgBox.ShowForm("请先选择病人！", this.Text, 1000, MessageBoxIcon.Asterisk);

                return;
            }
            DateTime dt = DateTime.Now.Date;
            DateTime startMonth = dt.AddDays(1 - dt.Day);  //本月月初 
            DateTime endMonth = startMonth.AddMonths(1).AddDays(-1);  //本月月末 
            this.beginTime.DateTime = startMonth;
            this.endTime.DateTime = endMonth;


            InzationData(HemoID);
            DataTable dtPatient = objPatient.GetPatientListByParams(string.Empty, HemoID);
            string patientName = string.Empty;
            if (dtPatient != null && dtPatient.Rows.Count > 0)
            {
                patientName = dtPatient.Rows[0]["NAME"].ToString();
                this.Text = string.Format("透析号为{0} 患者名称为{1}的阶段性评估记录", HemoID, patientName);
            }
        }

        /// <summary>
        /// 查询数据
        /// </summary>
        /// <param name="pHemoID"></param>
        private void InzationData(string pHemoID)
        {
            HemoModel.MED_PATIENTS_ASSESSMENTDataTable dt = hemodialysisService.GetAssessmentByParams(pHemoID, this.beginTime.DateTime, this.endTime.DateTime);
            var dtSource = new HemoModel.MED_PATIENTS_ASSESSMENTDataTable();
            dt.Where(i => i.ASSESSMENT_TYPE == "月度透析").CopyToDataTable<HemoModel.MED_PATIENTS_ASSESSMENTRow>(dtSource, LoadOption.PreserveChanges);

            if (dtSource != null && dtSource.Rows.Count > 0)
                this.gcInBasket.DataSource = dtSource;
            else
                this.gcInBasket.DataSource = null;
        }

        #endregion

        private void dxSimpleButton1_Click(object sender, EventArgs e) {
            PatientModel.MED_PATIENTSRow _patientDocRow;
            _patientDocRow = objPatient.GetPatientListByParams(string.Empty, HemoID)[0];
            if (_patientDocRow != null) {
                XtraForm form = new XtraForm();
                form.StartPosition = FormStartPosition.CenterScreen;
                form.Text = _patientDocRow.NAME + "的检验数据";
                ctlLabFrm labFrm = new ctlLabFrm(_patientDocRow);
                form.Size = labFrm.Size;
                labFrm.LoadLabInfo(_patientDocRow);
                labFrm.Dock = DockStyle.Fill;
                form.Controls.Add(labFrm);
                form.Show();
            }
        }
    }
}