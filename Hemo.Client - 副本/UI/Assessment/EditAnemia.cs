/*----------------------------------------------------------------
// Copyright (C) 2005 苏州XX公司有限公司
// 描述：贫血评估窗体
// 创建时间：2015-03-20
// 创建者：刘超
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
using Hemo.Model;
using Hemo.IService.Config;
using Hemo.Service;
using Hemo.Utilities;
using DevExpress.XtraEditors;
using Hemo.IService;
using Hemo.Client.UI.Lab;

namespace Hemo.Client.UI.Assessment
{
    public partial class EditAnemia : HemoBaseFrm
    {
        #region 类变量

        private IHemodialysis _hemoService = ServiceManager.Instance.HemodialysisService;
        private HemodialysisModel.MED_ANEMIA_CKDMBD_ASSESSDataTable dt = null;
        private HemodialysisModel.MED_ANEMIA_CKDMBD_ASSESSRow row = null;
        private IPatient objPatient = ServiceManager.Instance.PatientService;

        #endregion

        #region 属性

        public string HEMODIALYSIS_ID
        {
            get;
            set;
        }

        #endregion

        #region 构造函数

        public EditAnemia(HemodialysisModel.MED_ANEMIA_CKDMBD_ASSESSDataTable pdt, HemodialysisModel.MED_ANEMIA_CKDMBD_ASSESSRow prow) {
            InitializeComponent();
            dt = pdt;
            row = prow;
        }

        #endregion

        #region 事件

        private void btnExit_Click(object sender, EventArgs e) {
            this.DialogResult = DialogResult.Cancel;
        }

        private void btnSave_Click(object sender, EventArgs e) {
            row.XHDB = Utility.CDouble(txtXHDB.Text);
            row.XDB = Utility.CDouble(txtXDB.Text);
            row.ZTJHL = Utility.CDouble(txtZTJHL.Text);
            row.XQT = Utility.CDouble(txtXQT.Text);
            try {

                if (_hemoService.SaveMED_ANEMIA_CKDMBD_ASSESS(dt) > 0) {
                    XtraMessageBox.Show("保存成功");
                    this.DialogResult = DialogResult.OK;
                }
            }
            catch (Exception ex) {
                XtraMessageBox.Show(ex.ToString());
            }

        }

        private void EditCKDMBD_Load(object sender, EventArgs e) {
            if (dt == null) {
                dt = new HemodialysisModel.MED_ANEMIA_CKDMBD_ASSESSDataTable();
            }
            if (row == null) {
                row = dt.NewMED_ANEMIA_CKDMBD_ASSESSRow();
                row.ID = Guid.NewGuid().ToString();
                row.HEMODIALYSIS_ID = HEMODIALYSIS_ID;
                row.ASSESS_TYPE = "贫血";
                row.CREATE_DATE = System.DateTime.Now;
                dt.AddMED_ANEMIA_CKDMBD_ASSESSRow(row);
            }
            else {
                txtXHDB.Text = row.XHDB.ToString();
                txtXDB.Text = row.XDB.ToString();
                txtZTJHL.Text = row.ZTJHL.ToString();
                txtXQT.Text = row.XQT.ToString();
            }
        }

        #endregion

        private void dxSimpleButton1_Click(object sender, EventArgs e) {
            PatientModel.MED_PATIENTSRow _patientDocRow;
            _patientDocRow = objPatient.GetPatientListByParams(string.Empty, HEMODIALYSIS_ID)[0];
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
