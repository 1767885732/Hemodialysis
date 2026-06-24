/*----------------------------------------------------------------
 * Copyright (C) 2005 (苏州)医疗科技发展有限公司
 * 文件功能描述:修改方法
 * 创建标识:吕志强-2013年7月31日
 * 
 * 修改时间:2013年11月8日
 * 修改人:顾伟伟
 * 修改描述:新增方法
 * 
 * 修改时间:2014年2月16日
 * 修改人:吕志强
 * 修改描述:新增方法SQL
 * 
 * 修改时间:2014年5月27日
 * 修改人:贺建操
 * 修改描述:修改方法SQL
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
using DevExpress.XtraEditors.Controls;
using Hemo.Utilities;
using Hemo.IService.Dict;
using Hemo.Client.Core;

namespace Hemo.Client.UI.Hemodialysis
{
    public partial class BrowAddFrm : HemoBaseFrm
    {

        #region 变量
        private IPatient objPatient = ServiceManager.Instance.PatientService;
        private PatientModel.MED_PATIENTSDataTable _patientDataTable;
        private IDrug objDrug = ServiceManager.Instance.DrugService;
        private DrugModel.MED_DRUG_MASTERDataTable _drugData;
        private IConfig _configService = ServiceManager.Instance.ConfigService;
        private IStaffDict _staffDictService = ServiceManager.Instance.StaffDictService;
        private IHemodialysis _hemoService = ServiceManager.Instance.HemodialysisService;
        #endregion
        #region 构造函数
        public BrowAddFrm()
        {
            InitializeComponent();
        }
        #endregion
        #region 事件
        private void ShowSummary_Load(object sender, EventArgs e)
        {
            this.busyIndicator1.ShowLoadingScreenFor(this);
            DataTable dtStaffSict = null;

            using (BackgroundWorker worker = new BackgroundWorker())
            {
                worker.DoWork += delegate(object sender1, DoWorkEventArgs e1)
                {
                    this._patientDataTable = this.objPatient.GetPatientList();
                    this._drugData = objDrug.GetDrugMasterList();//绑定数据源
                    dtStaffSict = _staffDictService.GetStaffDictList();

                };
                worker.RunWorkerCompleted += delegate(object sender2, RunWorkerCompletedEventArgs e2)
                {
                    txtDRUG_NAME.Properties.PopupFormSize = new Size(400, 230);
                    txtDRUG_NAME.Properties.DisplayMember = "DRUG_NAME";//要显示的字段,Text获得
                    txtDRUG_NAME.Properties.ValueMember = "DRUG_CODE";//实际值的字段,EditValue获得
                    BaseControlInfo.BindLookUpEdit(cmbDOSAGE_UNITS, "ITEM_ID", "ITEM_NAME", this._configService.GetConfigList(string.Empty, string.Empty, "药品单位", "1"), "ITEM_NAME", "药品单位");

                    this.gridLookPatient.Properties.DataSource = this._patientDataTable;
                    txtDRUG_NAME.Properties.DataSource = this._drugData;
                    txtBorrowDate.DateTime = DateTime.Now;

                    BaseControlInfo.BindLookUpEdit(lupCHECK_NURSE, "EMP_NO", "NAME", dtStaffSict, "NAME", "借出操作人");
                    lupCHECK_NURSE.EditValue = HemoApplicationContext.Current.CurrentUser.EMP_NO;

                    this.busyIndicator1.HideLoadingScreen();
                };
                worker.RunWorkerAsync();
            }
        }
        private bool SaveCurrentData()
        {
            if (gridLookPatient.EditValue == null)
            {
                XtraMessageBox.Show("患者姓名不能为空");
                return false;
            }
            if (txtBorrowDate.EditValue == null)
            {
                XtraMessageBox.Show("日期不能为空");
                return false;
            }
            if (txtDRUG_NAME.EditValue == null)
            {
                XtraMessageBox.Show("药品不能为空");
                return false;
            }
            if (spnDRUG_TIMES.Value < 0)
            {
                XtraMessageBox.Show("数量填写错误");
                return false;
            }
            if (cmbDOSAGE_UNITS.EditValue == null || cmbDOSAGE_UNITS.EditValue.ToString().Trim() == "")
            {
                XtraMessageBox.Show("单位填写错误");
                return false;
            }
            if (lupCHECK_NURSE.EditValue == null || lupCHECK_NURSE.EditValue.ToString().Trim()=="")
            {
                XtraMessageBox.Show("操作人填写错误");

                return false;
            }
            var medData = new HemodialysisModel.MED_BORROW_MEDICINE_DETAILDataTable();

            var rowN = medData.NewMED_BORROW_MEDICINE_DETAILRow();
            rowN.BORROW_DAY = txtBorrowDate.DateTime;
            rowN.BORROW_ID = Guid.NewGuid().ToString();
            rowN.BORROW_TYPE = "0";
            rowN.BORROW_USER = string.Format("{0}({1})", lupCHECK_NURSE.Text, lupCHECK_NURSE.EditValue.ToString());
            rowN.HEMODIALYSIS_ID = gridLookPatient.EditValue.ToString();
            rowN.MEDICINE_COUNT = spnDRUG_TIMES.Value.ToString();
            rowN.MEDICINE_ID = txtDRUG_NAME.EditValue.ToString();
            rowN.MEDICINE_NAME = txtDRUG_NAME.Text;
            rowN.MEDICINE_UNIT = cmbDOSAGE_UNITS.Text;
            rowN.OLD_ID = "";
            rowN.OPT_USER = HemoApplicationContext.Current.CurrentUser.EMP_NO;
            medData.AddMED_BORROW_MEDICINE_DETAILRow(rowN);
            _hemoService.SaveBorrowData(medData);
            return true;
        }

        private void btnQuery_Click(object sender, EventArgs e)
        {
            if (SaveCurrentData())
            {
                AutoClosedMsgBox.ShowForm("保存成功！请继续操作。", "提示", 1500, MessageBoxIcon.Information);

                //XtraMessageBox.Show("保存成功！请继续操作。");
            }
            else
            {
                XtraMessageBox.Show("保存失败！");
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (SaveCurrentData())
            {
                AutoClosedMsgBox.ShowForm("保存成功。", "系统提示", 1500, MessageBoxIcon.Information);
               // XtraMessageBox.Show("保存成功！");
                this.Close();
            }
            else
            {
                XtraMessageBox.Show("保存失败！");
            }
        }

        #endregion

    }
}