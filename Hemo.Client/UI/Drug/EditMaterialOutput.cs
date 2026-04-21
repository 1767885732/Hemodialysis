/*----------------------------------------------------------------
// Copyright (C) 2005 苏州XX公司有限公司
// 描述：耗材出库窗体
// 创建时间：2013-03-21
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
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using Hemo.Service;
using Hemo.IService;
using System.Linq;
using Hemo.Utilities;
using Hemo.IService.Config;
using Hemo.Model;
using Hemo.IService.Dict;
using Hemo.Client.Core;
using System.Collections;
using Hemo.IService.PatientSchedule;

namespace Hemo.Client.UI.Drug 
{
    public partial class EditMaterialOutput : HemoBaseFrm
    {
        #region 类变量

        private IDrug objDrug = ServiceManager.Instance.DrugService;
        private IPatient objPatient = ServiceManager.Instance.PatientService;
        private IConfig _configService = ServiceManager.Instance.ConfigService;
        private IStaffDict _staffDictService = ServiceManager.Instance.StaffDictService;
        private IMaterial objMaterial = ServiceManager.Instance.MaterialService;
        private IHemodialysis objHemo = ServiceManager.Instance.HemodialysisService;
        private IPatientSchedule patientScheduleService = ServiceManager.Instance.PatientSchedule;

        private DrugModel.MED_MATERIAL_OUTPUTDataTable tmpDataTable = new DrugModel.MED_MATERIAL_OUTPUTDataTable();

        private DrugModel.MED_MATERIAL_OUTPUTDataTable _materialOutputDataTable = null;

        private DrugModel.MED_MATERIAL_OUTPUTRow _currentData;

        private ConfigModel.MED_COMMON_ITEMLISTDataTable _materialTypes;
        private ConfigModel.MED_COMMON_ITEMLISTDataTable _materialUnits;

        int resate = 0;
        private int maxvalue = 0;
        private string drugCode = string.Empty;
        DrugModel.MED_MATERIAL_INPUTMASTERDataTable dtInputMaterial = new DrugModel.MED_MATERIAL_INPUTMASTERDataTable();

        #endregion

        #region 属性

        public DrugModel.MED_MATERIAL_OUTPUTRow CurrentData
        {
            get { return _currentData; }
            set { _currentData = value; }
        }

        #endregion

        #region 构造函数

        public EditMaterialOutput()
        {
            InitializeComponent();
        }

        #endregion

        #region 事件

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (CheckInputValue())
                {
                    if (SaveData() > 0)
                    {
                        XtraMessageBox.Show("保存成功。", "基础信息");
                    }
                    else
                    {
                        XtraMessageBox.Show("失败。", "基础信息");
                    }
                    this.DialogResult = System.Windows.Forms.DialogResult.OK;
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "基础信息");
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
        }

        private void EditMaterialInput_Load(object sender, EventArgs e)
        {
            InzationMaterialDate();
            loadInfo();
        }
        private void btnAdd_Click(object sender, EventArgs e)
        {
            //Clear();
        }

        private void lupMaterialType_EditValueChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.lupMaterialType.EditValue.ToString()))
                return;
            dtInputMaterial = objMaterial.GetMedMaterialListByTypeId(this.lupMaterialType.EditValue.ToString());

            var dtMaterial = from p in dtInputMaterial
                             group p by new { p.ID, p.CODE, p.MATERIAL_NAME, p.SPACE, p.BATCH_NUMBER, p.UNTISNAME } into g
                             select new
                             {
                                 ID = g.Key.ID,
                                 CODE = g.Key.CODE,
                                 MATERIAL_NAME = g.Key.MATERIAL_NAME,
                                 SPACE = g.Key.SPACE,
                                 BATCH_NUMBER = g.Key.BATCH_NUMBER,
                                 UNITSNAME = g.Key.UNTISNAME
                             };
            this.lup_MaterialName.Properties.DataSource = dtMaterial.ToList();
        }

        private void lup_MaterialName_EditValueChanged(object sender, EventArgs e)
        {
            try
            {

                if (string.IsNullOrEmpty(lup_MaterialName.SelectedText))
                    return;
                var dt = dtInputMaterial.Where(i => i.MATERIAL_NAME.Equals(lup_MaterialName.SelectedText));
                var dtBathcNumber = from p in dt
                                    group p by new
                                    {
                                        p.ID,
                                        p.BATCH_NUMBER,
                                        p.UNITS,
                                        p.SPACE,
                                        p.F_COUNT

                                    } into g
                                    select new
                                    {
                                        ID = g.Key.ID,
                                        BATCH_NUMBER = g.Key.BATCH_NUMBER,
                                        UNITS = g.Key.UNITS,
                                        SPACE = g.Key.SPACE,
                                        F_COUNT = g.Key.F_COUNT
                                    };
                this.lupBatchNumber.Properties.DataSource = dtBathcNumber.ToList();

                var dr = dtInputMaterial.FirstOrDefault(i => i.ID == this.lup_MaterialName.EditValue.ToString() && i.MATERIAL_NAME.Equals(lup_MaterialName.SelectedText));
                this.lupBatchNumber.EditValue = dr.ID;
                LoadStoreById(dr.CODE);
                drugCode = dr.CODE;
                this.txt_Space.Text = dr.SPACE.ToString();
                try
                {
                    this.txtMADE_DATE.EditValue = dr.MADE_DATE;
                }
                catch
                { }

                this.cbxUNITS.EditValue = dr.UNITS;
                try
                {
                    this.txtUSELESS_DATE.EditValue = dr.USELESS_DATE;
                }
                catch
                {

                }
                try
                {
                    this.txtOUPUT_DATE.EditValue = System.DateTime.Now;
                }
                catch
                { }
                maxvalue = int.Parse(dr.F_COUNT);
                this.spnF_COUNT.EditValue = 0;

            }
            catch
            { }
        }
        
        private void lupBatchNumber_EditValueChanged(object sender, EventArgs e)
        {
            if (lupBatchNumber.EditValue == null)
                return;
            var dr = dtInputMaterial.FindByID(lupBatchNumber.EditValue.ToString());
            if (dr != null)
            {
                this.txt_Space.Text = dr.SPACE.ToString();
                try
                {
                    this.txtMADE_DATE.EditValue = dr.MADE_DATE;
                }
                catch
                { }
                this.cbxUNITS.EditValue = dr.UNITS;
                //this.spnF_COUNT.Properties.MaxValue = int.Parse(dr.F_COUNT);
                maxvalue = int.Parse(dr.F_COUNT);
                this.spnF_COUNT.EditValue = 0;
                try
                {
                    this.txtUSELESS_DATE.EditValue = dr.USELESS_DATE;
                    this.txtOUPUT_DATE.EditValue = System.DateTime.Now;
                }
                catch
                { }
            }
        }

        #endregion

        #region 方法

        /// <summary>
        /// 验证输入内容
        /// </summary>
        /// <returns></returns>
        private bool CheckInputValue()
        {
            bool result = true;
            this.errorProvider.ClearErrors();
            if (string.IsNullOrEmpty(this.lup_MaterialName.Text))
            {
                this.errorProvider.SetError(this.lup_MaterialName, "请选择出库耗材！");
                return false;
            }

            //if (string.IsNullOrEmpty(lupBatchNumber.Text))
            //{
            //    this.errorProvider.SetError(this.lupBatchNumber, "请选择要出库的批号！");
            //    return false;
            //}
            if (int.Parse(spnF_COUNT.Text) <= 0)
            {
                this.errorProvider.SetError(this.spnF_COUNT, "请输入出库数量！");
                return false;
            }
            if (int.Parse(spnF_COUNT.EditValue.ToString()) > resate)
            {
                XtraMessageBox.Show(string.Format("超过此耗材库存剩余值{0},请重新录入出库数量！", resate.ToString()), this.Text);
                return false;

            }
            if (string.IsNullOrEmpty(txtOPERATOR_ID.Text))
            {
                this.errorProvider.SetError(this.txtOPERATOR_ID, "请选择申领人员！");
                return false;
            }
            if (!string.IsNullOrEmpty(drugCode))
            {
                var dt = objMaterial.GetOutPutByCode(drugCode);
                if (dt != null && dt.Rows.Count > 0)
                {
                    if (XtraMessageBox.Show("此耗材已进行过出库，是否继续？", "提示", MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.No)
                        return false;
                }
            }
            return result;
        }

        /// <summary>
        /// 载入今日治疗单所用耗材数量，根据治疗单统计

        ///
        /// </summary>
        private void loadInfo()
        {
            DataTable dtTodayPurificationMode = objHemo.GetCurePurificationModeCountByDate(patientScheduleService.GetServerDate());
            if (dtTodayPurificationMode != null && dtTodayPurificationMode.Rows.Count > 0)
            {
                dtTodayPurificationMode = Utility.GetSubTable(dtTodayPurificationMode, "PTYPE='净化器类型'");
                if (dtTodayPurificationMode != null && dtTodayPurificationMode.Rows.Count > 0)
                {
                    lblInfo.Text = "今日耗材:";
                    for (int i = 0; i < dtTodayPurificationMode.Rows.Count; i++)
                    {
                        lblInfo.Text += dtTodayPurificationMode.Rows[i]["ITEM_NAME"].ToString() + ",使用数量:" + dtTodayPurificationMode.Rows[i]["PCOUNT"].ToString() + "支 ";
                    }
                }
            }
        }

        private void LoadStoreById(string id)
        {
            int inStore = 0;
            int outStore = 0;
            var ds = objMaterial.GetMaterialStoreInOutByCode(id);
            if (ds != null && ds.Rows.Count > 0)
            {
                inStore = int.Parse(ds.Rows[0]["INCOUNT"].ToString());
                outStore = int.Parse(ds.Rows[0]["OUTCOUNT"].ToString());
            }
            resate = inStore - outStore;
            lbStore.Text = string.Format("入库数量:{0}  出库数量：{1} 剩余:{2}", inStore, outStore, resate);
        }

        public void InzationMaterialDate()
        {
            this.Enabled = false;
            _materialOutputDataTable = new DrugModel.MED_MATERIAL_OUTPUTDataTable();
            using (BackgroundWorker worker = new BackgroundWorker())
            {
                var dtDetail = new DrugModel.MED_MATERIAL_OUTPUTDataTable();
                var dtStaffSict = new DictModel.MED_STAFF_DICTDataTable();
                var _materialDataTable = new DrugModel.MED_MATERIAL_INPUTMASTERDataTable();
                worker.DoWork += delegate(object sender1, DoWorkEventArgs e1)
                {
                    if (_currentData != null)
                    {
                        _materialOutputDataTable.ImportRow(_currentData);
                    }
                    else
                    {
                        _materialOutputDataTable = new DrugModel.MED_MATERIAL_OUTPUTDataTable();
                    }
                    var dt = DateTime.Now;
                    DateTime startMonth = dt.AddDays(1 - dt.Day).Date;  //本月月初 

                    DateTime endMonth = startMonth.AddMonths(1).AddDays(-1).Date;  //本月月末 //DateTime endMonth = startMonth.AddDays((dt.AddMonths(1) - dt).Days - 1);  //本月月末

                    _materialDataTable = objMaterial.GetMedMaterialListByTypeId(string.Empty);
                    dtDetail = objMaterial.GetMedMaterialOutputDetail(startMonth, endMonth);

                    this._materialTypes = this._configService.GetConfigList(string.Empty, string.Empty, "辅材类型", "1");
                    this._materialUnits = this._configService.GetConfigList(string.Empty, string.Empty, "耗材单位", "1");
                    dtStaffSict = _staffDictService.GetStaffDictList();
                };
                worker.RunWorkerCompleted += delegate(object sender2, RunWorkerCompletedEventArgs e2)
                {
                    this.lup_MaterialName.Properties.DataSource = _materialDataTable;
                    this.lupMaterialType.Properties.DataSource = this._materialTypes;
                    this.txtOPERATOR_ID.Properties.DataSource = Utility.GetSubTable(dtStaffSict, "ZYNAME='护士'");
                    this.lupSTOREMANAGER.Properties.DataSource = Utility.GetSubTable(dtStaffSict, "ZYNAME='护士'");
                    this.cbxUNITS.Properties.DataSource = _materialUnits;

                    this.MED_MATERIAL_OUTPUT.DataSource = _materialOutputDataTable;
                    this.gridControl2.DataSource = dtDetail;

                    if (_currentData == null)
                    {
                        this.MED_MATERIAL_OUTPUT.AddNew();
                        this.txt_ID.Text = Guid.NewGuid().ToString();
                        this.lupMaterialType.EditValue = this._materialTypes[0].ITEM_ID;
                        //this.txtMADE_DATE.EditValue = System.DateTime.Now;
                        this.txtUSELESS_DATE.EditValue = System.DateTime.Now.AddYears(3);
                        this.txtOUPUT_DATE.EditValue = System.DateTime.Now.AddYears(3);
                        this.txtOPERATOR_ID.EditValue = HemoApplicationContext.Current.CurrentUser.EMP_NO;
                        this.lupSTOREMANAGER.EditValue = HemoApplicationContext.Current.CurrentUser.EMP_NO;
                    }
                    this.Enabled = true;
                };
                worker.RunWorkerAsync();
            }
        }

        private int SaveData()
        {
            var dr = dtInputMaterial.FindByID(lupBatchNumber.EditValue.ToString());
            this.MED_MATERIAL_OUTPUT.EndEdit();
            this.MED_MATERIAL_OUTPUT.CurrencyManager.EndCurrentEdit();

            var row = _materialOutputDataTable[0];
            row.CODE = dr.CODE;
            row.OPERATOR_ID = LoginUser.User.USER_ID;
            row.MATERIAL_NAME = this.lup_MaterialName.Text.ToString();
            row.APPLYID = this.txtOPERATOR_ID.EditValue.ToString();
            row.MODETYPE = this.lupMaterialType.EditValue.ToString();
            row.BATCH_NUMBER = dr.BATCH_NUMBER;
            //row.STOREMANAGER = dr.STOREMANAGER;
            row.ISOUT = "1";
            row.UNITS = this.cbxUNITS.Text.ToString();
            row.STATUS = "1";
            return objMaterial.SaveMedMaterialOutputNew(_materialOutputDataTable);
        }

        #endregion
    }
}