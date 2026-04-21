/*----------------------------------------------------------------
// Copyright (C) 2005 苏州XX公司有限公司
// 描述：患者营养评估编辑窗体
// 创建时间：2015-03-16
// 创建者：吕志强
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
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Hemo.Service;
using Hemo.IService;
using Hemo.IService.Config;
using Hemo.Model;
using DevExpress.XtraEditors;
using Hemo.Client.Doc;
using Hemo.Client.UI.Hemodialysis;
using Hemo.Client.UI.Machine;
using Hemo.Client.UI.Lab;

namespace Hemo.Client.UI.Assessment
{
    public partial class NutritionAssessment : HemoBaseFrm
    {
        #region 变量

        private IHemodialysis _hemoService = ServiceManager.Instance.HemodialysisService;
        private IPatient objPatient = ServiceManager.Instance.PatientService;

        private HemoModel.MED_ASSESSMENTMASTERRow _medAssessRow =null;

        public HemoModel.MED_ASSESSMENTMASTERRow MedAssessRow
        {
            get { return _medAssessRow; }
            set { _medAssessRow = value; }
        }

        private HemoModel.MED_ASSESSMENTMASTERDataTable _medAssessTable = null;

        public HemoModel.MED_ASSESSMENTMASTERDataTable MedAssessTable
        {
            get { return _medAssessTable; }
            set { _medAssessTable = value; }
        }

        private string currentHemoId = string.Empty;

        public string CurrentHemoId
        {
            get { return currentHemoId; }
            set { currentHemoId = value; }
        }
       
        #endregion

        #region 构造函数

        public NutritionAssessment()
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
        private void NutritionAssessment_Load(object sender, EventArgs e)
        {           
            //加载用户信息控件
            ctlUserLongInfo1.HEMODIALYSIS_ID = currentHemoId;
            ctlUserLongInfo1.LoadPatientInfo();
            //加载评估参数
            ctlNurtritionSga1.CurrentHemoId = currentHemoId;
            ctlNurtritionSga1.MedAssessRow = _medAssessRow;
            ctlNurtritionSga1.MedAssessTable = _medAssessTable;
            ctlNurtritionSga1.InzationControl();

            this.Text = "患者营养评估";
            ProFunctionCount pfc = new ProFunctionCount();
            pfc.SaveFunctionCountFrm(this);
        }

        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (ctlNurtritionSga1.SaveNutritionSga()> 0)
            {
                this.DialogResult = System.Windows.Forms.DialogResult.OK;
                XtraMessageBox.Show("保存患者营养评估记录成功！");
            }
            else
            {
                XtraMessageBox.Show("保存患者营养评估记录失败！");
            }
        }

        /// <summary>
        /// 打印
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnPrint_Click(object sender, EventArgs e)
        {
            //if (currentRecordRow != null)
            //{
            //    var doc = new 透析充分性评估();
            //    doc.PatientRow = currentRecordRow;
            //    doc.Pname = currentHemoName;
            //    doc.LoadDocumentInfo();
            //    var frm = new ShowPrintForm(doc);
            //    frm.StartPosition = FormStartPosition.CenterParent;
            //    frm.ShowDialog();
            //}
        }

        /// <summary>
        /// 关闭
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        #endregion

        private void dxSimpleButton1_Click(object sender, EventArgs e) {
            PatientModel.MED_PATIENTSRow _patientDocRow;
            _patientDocRow = objPatient.GetPatientListByParams(string.Empty, currentHemoId)[0];
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
