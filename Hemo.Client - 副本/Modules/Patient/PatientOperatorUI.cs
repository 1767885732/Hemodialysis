/*----------------------------------------------------------------
 * Copyright (C) 2005 医疗科技发展有限公司
 * 文件功能描述:修复系统加载时数据缓存问题
 * 创建标识:刘超-2016年12月7日
 * 
 * 修改时间:2017年4月24日
 * 修改人:贺建操
 * 修改描述:修复系统响应速度慢的问题
 * 
 * 修改时间:2017年5月26日
 * 修改人:顾伟伟
 * 修改描述:修改对外公开的方法
 * ----------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Hemo.Client.Base;
using Hemo.Model;
using Hemo.Utilities;
using Hemo.Client.Controls;
using Hemo.IService;
using Hemo.Service;
using DevExpress.XtraEditors;
using Hemo.IService.Config;
using Hemo.IService.Dict;
using Hemo.Client.UI.Hemodialysis;

namespace Hemo.Client.Modules.Patient
{
    public partial class PatientOperatorUI : BaseMoudleControl
    {
        #region 变量

        private IPatient objPatientService = ServiceManager.Instance.PatientService;

        private IConfig _configService = ServiceManager.Instance.ConfigService;

        private IStaffDict staffService = ServiceManager.Instance.StaffDictService;

        private ConfigModel.MED_PATIENTS_OPERATORDataTable _data = null;

        private ConfigModel.MED_PATIENTS_OPERATORRow currentRow = null;

        private bool isAdd = true;

        public TaskViewModel ViewModel
        {
            get { return GetViewModel<TaskViewModel>(); }
        }

        #endregion

        #region 构造函数
        public PatientOperatorUI()
        {
            InitializeComponent();
            base.viewModelCore = CreateViewModel<TaskViewModel>();

            if (!DesignMode)
            {
                BindLookUpEdit();
                this.patientInfoCheck1.patientPickEvent += new EventHandler(patientInfoCheck1_patientPickEvent);
                dtStar.EditValue = DateTime.Now.Date.AddDays(-30);
                dtEnd.EditValue = DateTime.Now.Date;
                InzationData();
            }

        }

        #endregion

        #region 事件

        /// <summary>
        /// 点击行事件显示编辑界面
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gridView4_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            currentRow = this.gridView4.GetFocusedDataRow() as ConfigModel.MED_PATIENTS_OPERATORRow;
            if (currentRow == null)
                return;

            if (e.Button == MouseButtons.Left && e.Clicks == 2)
            {
                SetBtnVisit(false);
                isAdd = false;
                currentRow = this.gridView4.GetFocusedDataRow() as ConfigModel.MED_PATIENTS_OPERATORRow;
                this.patientInfoCheck1.SetTxtHemoIdText(currentRow.HEMODIALYSIS_ID);

                IsShowEditUi();
            }
        }

        /// <summary>
        /// 选择时
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void patientInfoCheck1_patientPickEvent(object sender, EventArgs e)
        {
            this.txtNAME.EditValue = this.patientInfoCheck1._patientDataTable[0].NAME;
            this.txtSEX.EditValue = this.patientInfoCheck1._patientDataTable[0].SEX;
            this.txtAge.EditValue = this.patientInfoCheck1._patientDataTable[0].AGE;
            this.txtHEMODIALYSIS_ID.EditValue = this.patientInfoCheck1._patientDataTable[0].HEMODIALYSIS_ID;
        }

        protected internal override void OnTransitionCompleted()
        {
            base.OnTransitionCompleted();
        }

        /// <summary>
        /// 诊断
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnOPE_DIAGNOSE_Click(object sender, EventArgs e)
        {
            ShowICDList frm = new ShowICDList(string.Empty);
            frm.ShowDialog();
            this.txtOPE_DIAGNOSE.Text += frm.IcdList;
            this.txtOPE_DIAGNOSE.Text = this.txtOPE_DIAGNOSE.Text.Substring(0, this.txtOPE_DIAGNOSE.Text.Length - 1);
        }

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAdd_Click(object sender, EventArgs e)
        {
            SetBtnVisit(false);
            isAdd = true;
            currentRow = _data.NewMED_PATIENTS_OPERATORRow();
            IsShowEditUi();
            this.patientInfoCheck1.SetTxtHemoIdText(string.Empty);

            txtOPE_STAR.EditValue = DateTime.Now.AddHours(-1);
            txtOPE_END.EditValue = DateTime.Now;
        }

        /// <summary>
        /// 编辑
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnEdit_Click(object sender, EventArgs e)
        {
            currentRow = this.gridView4.GetFocusedDataRow() as ConfigModel.MED_PATIENTS_OPERATORRow;
            if (currentRow == null)
                return;
            SetBtnVisit(false);
            isAdd = false;
            this.patientInfoCheck1.SetTxtHemoIdText(currentRow.HEMODIALYSIS_ID);
            IsShowEditUi();
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDelete_Click(object sender, EventArgs e)
        {
            var row = this.gridView4.GetFocusedDataRow() as ConfigModel.MED_PATIENTS_OPERATORRow;
            if (row != null)
            {
                if (DialogResult.OK == MessageBox.Show("是否确认删除？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning))
                {
                    int count = objPatientService.DeletePatientOperatorById(row.ID);
                    if (count > 0)
                    {
                        MessageBox.Show("删除成功！");
                        InzationData();
                    }
                }
            }

        }

        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (CheckInputValue())
            {
                SetBtnVisit(true);
                if (this.isAdd)
                {
                    currentRow.ID = Guid.NewGuid().ToString();
                    _data.AddMED_PATIENTS_OPERATORRow(currentRow);
                }
                else
                {
                    currentRow.AGE = txtAge.Text;
                    currentRow.HEMODIALYSIS_ID = txtHEMODIALYSIS_ID.Text;
                    currentRow.NAME = txtNAME.Text;
                }
                objPatientService.SavePatientsOperator(_data);
                InzationData();
            }
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnQuery_Click(object sender, EventArgs e)
        {
            InzationData();
        }

        /// <summary>
        /// 关闭
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnClose_Click(object sender, EventArgs e)
        {
            currentRow = null;
            SetBtnVisit(true);
            InzationData();
        }

        #endregion

        #region 方法

        /// <summary>
        /// 绑定下拉项
        /// </summary>
        private void BindLookUpEdit()
        {
            DataTable dtStaff = staffService.GetStaffDictList();
            DataTable dtDoctor = Utility.GetSubTable(dtStaff, "ZYNAME='医生'", "NAME");
            DataTable dtNurse = Utility.GetSubTable(dtStaff, "ZYNAME='护士'", "NAME");
            BaseControlInfo.BindLookUpEdit(txtOPE_DOC, "EMP_NO", "NAME", dtDoctor, "NAME", "手术医师");
            BaseControlInfo.BindLookUpEdit(txtANESTHESIOLOGIST, "EMP_NO", "NAME", dtDoctor, "NAME", "麻醉医师");
            BaseControlInfo.BindLookUpEdit(txtANESTHESIOLOGIST2, "EMP_NO", "NAME", dtDoctor, "NAME", "麻醉医师");
            BaseControlInfo.BindLookUpEdit(txtTRANSFUSIONDOCTOR, "EMP_NO", "NAME", dtDoctor, "NAME", "输血医师");
            BaseControlInfo.BindLookUpEdit(txtSTAGENURSE, "EMP_NO", "NAME", dtNurse, "NAME", "台上护士");
            BaseControlInfo.BindLookUpEdit(txtSUPPLYNURSE, "EMP_NO", "NAME", dtNurse, "NAME", "供应护士");
            BaseControlInfo.BindLookUpEdit(txtSUPPLYNURSETOW, "EMP_NO", "NAME", dtNurse, "NAME", "供应护士");
            BaseControlInfo.BindLookUpEdit(txtANESTHESIAMETHOD, "ITEM_ID", "ITEM_NAME", _configService.GetConfigList(string.Empty, string.Empty, "麻醉方法", "1"), "ITEM_NAME", "麻醉方法");
            BaseControlInfo.BindLookUpEdit(txtOPE_NAME, "ITEM_ID", "ITEM_NAME", _configService.GetConfigList(string.Empty, string.Empty, "手术名称", "1"), "ITEM_NAME", "手术名称");
            BaseControlInfo.BindLookUpEdit(txtVASCULARV_TYPE, "ITEM_ID", "ITEM_NAME", _configService.GetConfigList(string.Empty, string.Empty, "通路分类", "1"), "ITEM_NAME", "通路分类");
        }

        /// <summary>
        /// 验证输入内容
        /// </summary>
        /// <returns></returns>
        private bool CheckInputValue()
        {
            bool result = true;
            this.errorProvider.ClearErrors();
            if (string.IsNullOrEmpty(this.txtNAME.Text))
            {
                this.errorProvider.SetError(this.txtNAME, "请输入患者姓名！");
                return false;
            }
            if (string.IsNullOrEmpty(txtOPE_NAME.Text))
            {
                this.errorProvider.SetError(this.txtOPE_NAME, "请选择实施手术！");
                return false;
            }
            if (string.IsNullOrEmpty(txtOPE_LEVEL.Text.ToString()))
            {
                this.errorProvider.SetError(this.txtOPE_LEVEL, "请选择手术等级！");
                return false;
            }

            if (string.IsNullOrEmpty(txtCUT_LEVEL.Text))
            {
                this.errorProvider.SetError(this.txtCUT_LEVEL, "请选择手术切口等级！");
                return false;
            }
            if (string.IsNullOrEmpty(txtVASCULARV_TYPE.Text.ToString()))
            {
                this.errorProvider.SetError(this.txtVASCULARV_TYPE, "请选择导管类型！");
                return false;
            }

            if (string.IsNullOrEmpty(txtOPE_DOC.Text.ToString()))
            {
                this.errorProvider.SetError(this.txtOPE_DOC, "请选择手术医师！");
                return false;
            }
            if (string.IsNullOrEmpty(txtANESTHESIAMETHOD.Text.ToString()))
            {
                this.errorProvider.SetError(this.txtANESTHESIAMETHOD, "请选择麻醉方法！");
                return false;
            }
            if (string.IsNullOrEmpty(txtSTAGENURSE.Text.ToString()))
            {
                this.errorProvider.SetError(this.txtSTAGENURSE, "请选择台上护士！");
                return false;
            }

            if (string.IsNullOrEmpty(txtOPE_STAR.EditValue.ToString()))
            {
                this.errorProvider.SetError(this.txtOPE_STAR, "请输入开始时间！");
                return false;
            }

            if (string.IsNullOrEmpty(txtOPE_END.EditValue.ToString()))
            {
                this.errorProvider.SetError(this.txtOPE_END, "请输入结束时间！");
                return false;
            }

            return result;
        }
        
        /// <summary>
        /// 初始化数据
        /// </summary>
        public void InzationData()
        {
            this.xtraTabControl1.SelectedTabPage = this.xtraTabPageQuery;

            BusyIndicatorHelp busyIndicatorHelp = new BusyIndicatorHelp();
            using (var _worker = new BackgroundWorker())
            {
                busyIndicatorHelp.ShowMessage();
                _data = new ConfigModel.MED_PATIENTS_OPERATORDataTable();
                currentRow = null;
                _worker.DoWork += delegate(object sender, DoWorkEventArgs e)
                {
                    _data = objPatientService.GetPatientOperatorByDate(dtStar.DateTime, dtEnd.DateTime, this.txtFilterName.Text.Trim());
                };
                _worker.RunWorkerCompleted += delegate(object sender1, RunWorkerCompletedEventArgs r1)
                {
                    this.gridControl1.DataSource = _data;
                    busyIndicatorHelp.HideMessage();
                };
                _worker.RunWorkerAsync();

            }
        }

        /// <summary>
        /// 设置按钮的显示与隐藏
        /// </summary>
        /// <param name="IsVisit"></param>
        private void SetBtnVisit(bool IsVisit)
        {
            this.btnAdd.Visible = IsVisit;
            this.btnEdit.Visible = IsVisit;
            this.btnDelete.Visible = IsVisit;
            this.btnSave.Visible = !IsVisit;
            this.btnClose.Visible = !IsVisit;
            if (IsVisit)
            {
                this.xtraTabControl1.SelectedTabPage = this.xtraTabPageQuery;
            }
            else
            {
                this.xtraTabControl1.SelectedTabPage = this.xtraTabPageEdit;
            }
        }

        /// <summary>
        /// 显示编辑界面
        /// </summary>
        /// <param name="row"></param>
        private void IsShowEditUi()
        {
            foreach (var ctl in this.xtraTabPageEdit.Controls)
            {
                if (ctl is BaseEdit)
                {
                    (ctl as BaseEdit).BindingDataRow(currentRow, "txt");
                }
                else if (ctl is DevExpress.XtraEditors.GroupControl)
                {
                    BindGroupControlData(ctl as DevExpress.XtraEditors.GroupControl);
                }
            }
        }

        /// <summary>
        /// 优化
        /// </summary>
        /// <param name="gPanel"></param>
        private void BindGroupControlData(DevExpress.XtraEditors.GroupControl gPanel)
        {
            foreach (var tctl in gPanel.Controls)
            {
                if (tctl is BaseEdit)
                {
                    (tctl as BaseEdit).BindingDataRow(currentRow, "txt");
                }
            }
        }

        #endregion
    }
}
