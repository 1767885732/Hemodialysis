/*----------------------------------------------------------------
// Copyright (C) 2013 苏州XX公司
// 描述：URR|Kt/V|TS评估列表用户控件类
// 创建时间：2015-08-21
// 创建者：吕志强
//  
// 修改时间：
// 修改人：
// 修改描述：
//
----------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Hemo.IService.Config;
using Hemo.Service;
using Hemo.Model;
using DevExpress.XtraEditors;
using Hemo.Client.UI.Assessment;
using Hemo.Client.UI.Machine;
using Hemo.IService;
using Hemo.Client.UI.Lab;

namespace Hemo.Client.Controls
{
    public partial class CtlPatientSufficiency : ViewBase
    {
        #region 类变量

        private string hemoId = string.Empty;

        private PatientModel.MED_PATIENTSRow currentPatient;

        private IHemodialysis hemoService = ServiceManager.Instance.HemodialysisService;

        private HemodialysisModel.MED_ESTIMATE_SUFFICIENCYDataTable dtSufficiency = null;
        private IPatient objPatient = ServiceManager.Instance.PatientService;

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
        /// 当前患者
        /// </summary>
        public PatientModel.MED_PATIENTSRow CurrentPatient
        {
            get { return currentPatient; }
            set { currentPatient = value; }
        }

        #endregion

        #region 构造函数

        public CtlPatientSufficiency() {
            InitializeComponent();
        }

        #endregion

        #region 事件

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnQuery_Click(object sender, EventArgs e) {
            Query();
        }

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAdd_Click(object sender, EventArgs e) {
            var dtFind = hemoService.GetEstimateSufficiencyByHemoIdAndDate(hemoId, this.xtraTabControl1.SelectedTabPageIndex.ToString(), DateTime.Now);
            if (dtFind != null && dtFind.Rows.Count > 0) {
                XtraMessageBox.Show("当天已经录入过评估记录！", "URR|Kt/V|TS|MDRD评估列表");
                return;
            }

            var frmSufficiency = new EstimateSufficiencyNew();
            frmSufficiency.HemoId = hemoId;
            frmSufficiency.CurrentPatient = currentPatient;
            frmSufficiency.Flag = this.xtraTabControl1.SelectedTabPageIndex;
            frmSufficiency.CurrentRow = null;
            DialogResult result = frmSufficiency.ShowDialog();
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
            var row = this.xtraTabControl1.SelectedTabPageIndex == 0 ? this.gvSufficiency.GetFocusedDataRow() : (this.xtraTabControl1.SelectedTabPageIndex == 1 ? this.gvTS.GetFocusedDataRow() : this.gvMDRD.GetFocusedDataRow());
            if (row != null) {
                if (XtraMessageBox.Show("确认要删除选中的记录吗？", "URR|Kt/V|TS|MDRD评估列表", MessageBoxButtons.OKCancel) == DialogResult.OK) {
                    int result = hemoService.DeleteEstimateSufficiencyById(row["ID"].ToString());
                    if (result > 0) {
                        XtraMessageBox.Show("删除记录成功！", "URR|Kt/V|TS|MDRD评估列表");
                        Query();
                    }
                }
            }
        }

        /// <summary>
        /// 双击URR|Kt/V列表行
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gcSufficiency_DoubleClick(object sender, EventArgs e) {
            DoubleClick();
        }

        /// <summary>
        /// 双击转铁蛋白饱和度列表行
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gcTS_DoubleClick(object sender, EventArgs e) {
            DoubleClick();
        }

        /// <summary>
        /// 双击MDRD列表行
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gcMDRD_DoubleClick(object sender, EventArgs e) {
            DoubleClick();
        }

        /// <summary>
        /// 关闭
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnClose_Click(object sender, EventArgs e) {
            this.ParentForm.Close();
        }

        #endregion

        #region 方法

        /// <summary>
        /// 查询
        /// </summary>
        public void Query() {
            int flag = this.xtraTabControl1.SelectedTabPageIndex;

            if (this.txtFromDate.EditValue != null && this.txtToDate.EditValue == null) {
                XtraMessageBox.Show("请选择结束日期！", "URR|Kt/V|TS|MDRD评估列表");
                return;
            }

            if (this.txtFromDate.EditValue == null && this.txtToDate.EditValue != null) {
                XtraMessageBox.Show("请选择开始日期！", "URR|Kt/V|TS|MDRD评估列表");
                return;
            }

            if (this.txtFromDate.EditValue == null && this.txtToDate.EditValue == null) {
                dtSufficiency = hemoService.GetEstimateSufficiencyByHemoId(hemoId, flag.ToString());
            }
            else if (this.txtFromDate.EditValue != null && this.txtToDate.EditValue != null) {
                dtSufficiency = hemoService.GetEstimateSufficiencyByHemoIdAndDate(hemoId, flag.ToString(), this.txtFromDate.DateTime, this.txtToDate.DateTime);
            }

            this.gcSufficiency.DataSource = flag == 0 ? dtSufficiency : this.gcSufficiency.DataSource;
            this.gcTS.DataSource = flag == 1 ? dtSufficiency : this.gcTS.DataSource;
            this.gcMDRD.DataSource = flag == 2 ? dtSufficiency : this.gcMDRD.DataSource;
        }

        /// <summary>
        /// 双击列表行
        /// </summary>
        /// <param name="add"></param>
        private void DoubleClick() {
            var row = this.xtraTabControl1.SelectedTabPageIndex == 0 ? this.gvSufficiency.GetFocusedDataRow() : (this.xtraTabControl1.SelectedTabPageIndex == 1 ? this.gvTS.GetFocusedDataRow() : this.gvMDRD.GetFocusedDataRow());
            if (row != null) {
                var frmSufficiency = new EstimateSufficiencyNew();
                frmSufficiency.HemoId = hemoId;
                frmSufficiency.Flag = this.xtraTabControl1.SelectedTabPageIndex;
                frmSufficiency.CurrentRow = row as HemodialysisModel.MED_ESTIMATE_SUFFICIENCYRow;
                DialogResult result = frmSufficiency.ShowDialog();
                if (result == DialogResult.OK) {
                    Query();
                }
            }
        }

        #endregion

        private void dxSimpleButton1_Click(object sender, EventArgs e) {
            PatientModel.MED_PATIENTSRow _patientDocRow;
            _patientDocRow = objPatient.GetPatientListByParams(string.Empty, HemoId)[0];
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
