/*----------------------------------------------------------------
// Copyright (C) 2013 苏州XX公司有限公司
// 描述：药品耗材入库窗体
// 创建时间：2013-07-30
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
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using Hemo.Service;
using Hemo.IService;
using Hemo.Utilities;
using Hemo.IService.Config;
using Hemo.Model;
using Hemo.IService.Dict;
using Hemo.Client.Core;

namespace Hemo.Client.UI.Drug
{
    public partial class EditMaterialInput : HemoBaseFrm
    {
        #region 类变量

        private IDrug objDrug = ServiceManager.Instance.DrugService;
        private IPatient objPatient = ServiceManager.Instance.PatientService;
        private IConfig _configService = ServiceManager.Instance.ConfigService;
        private IStaffDict _staffDictService = ServiceManager.Instance.StaffDictService;
        private IMaterial objMaterial = ServiceManager.Instance.MaterialService;

        private ConfigModel.MED_COMMON_ITEMLISTDataTable _materialTypes;
        private ConfigModel.MED_COMMON_ITEMLISTDataTable _materialUnits;


        private MaterialModel.MED_MATERIAL_MASTERDataTable _materialDataTable = null;
        private DrugModel.MED_MATERIAL_INPUTDataTable _materialInputDataTable = null;

        private DrugModel.MED_MATERIAL_INPUTRow _currentData;
        DrugModel.MED_MATERIAL_INPUTDataTable tmpDataTable = new DrugModel.MED_MATERIAL_INPUTDataTable();

        #endregion

        #region 属性

        public DrugModel.MED_MATERIAL_INPUTRow CurrentData
        {
            get { return _currentData; }
            set { _currentData = value; }
        }

        #endregion

        #region 构造函数

        public EditMaterialInput()
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
                    { XtraMessageBox.Show("保存成功。", "基础信息"); }
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

        private void spiPRICE_TextChanged(object sender, EventArgs e)
        {
            if (spiPRICE.Text.IndexOf('-') > -1)
            {
                spiPRICE.Text = spiPRICE.Text.Replace("-", "");
            }
        }
        /// <summary>
        /// 数量改变时，直接计算价格
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void spnF_COUNT_TextChanged(object sender, EventArgs e)
        {
            if (spnF_COUNT.Text.IndexOf('-') > -1)
            {
                spnF_COUNT.Text = spnF_COUNT.Text.Replace("-", "");
            }

            if (spnF_COUNT.EditValue != null && Utility.CDecimal(spnF_COUNT.EditValue.ToString()) > 0)
            {
                this.spiPRICE.EditValue = Utility.CDecimal(spnF_COUNT.EditValue.ToString()) * Utility.CDecimal(lupMaterPrice.EditValue.ToString());
            }
        }

        private void EditMaterialInput_Load(object sender, EventArgs e)
        {
            InzationMaterialDate();
        }
        private void btnAdd_Click(object sender, EventArgs e)
        {
            //Clear();
        }

        private void lup_MaterialName_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                var dr = ((Hemo.Model.MaterialModel.MED_MATERIAL_MASTERRow)(((System.Data.DataRowView)(lup_MaterialName.GetSelectedDataRow())).Row));
                if (dr != null)
                {
                    this.txt_Space.Text = dr.IsMATERIAL_SPECNull() ? string.Empty : dr.MATERIAL_SPEC.ToString();
                    this.cbxUNITS.EditValue = dr.IsUNITNull() ? string.Empty : dr.UNIT.ToString();

                    this.txt_Firm_id.Text = dr.IsFIRM_NAMENull() ? string.Empty : dr.FIRM_NAME.ToString();
                    this.lupMaterPrice.EditValue = dr.IsPRICENull() ? 0 : dr.PRICE;
                }
            }
            catch
            { }
        }

        private void lupMaterialType_EditValueChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(lupMaterialType.EditValue.ToString()))
            {
                var dt = new MaterialModel.MED_MATERIAL_MASTERDataTable();

                _materialDataTable.Where(i => i.MATERIAL_TYPE == this.lupMaterialType.EditValue.ToString()).CopyToDataTable(dt, LoadOption.PreserveChanges);
                this.lup_MaterialName.Properties.DataSource = dt;
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
                this.errorProvider.SetError(this.lup_MaterialName, "耗材名称必须录入");
                return false;
            }
            //if (string.IsNullOrEmpty(this.txt_BATCH_NUMBER.Text))
            //{
            //    this.errorProvider.SetError(this.txt_BATCH_NUMBER, "批号必须录入");
            //    return false;
            //}
            if (int.Parse(spnF_COUNT.Text) <= 0)
            {
                this.errorProvider.SetError(this.spnF_COUNT, "请输入出库数量！");
                return false;
            }
            if (string.IsNullOrEmpty(this.spnF_COUNT.Text))
            {
                this.errorProvider.SetError(this.spnF_COUNT, "入库数量必须录入");
                return false;
            }
            if (string.IsNullOrEmpty(this.cbxUNITS.Text))
            {
                this.errorProvider.SetError(this.cbxUNITS, "入库耗材单位必须录入");
                return false;
            }

            return result;
        }

        public void InzationMaterialDate()
        {
            this.Enabled = false;
            _materialInputDataTable = new DrugModel.MED_MATERIAL_INPUTDataTable();
            using (BackgroundWorker worker = new BackgroundWorker())
            {
                var dtStaffSict = new DictModel.MED_STAFF_DICTDataTable();
                _materialDataTable = new MaterialModel.MED_MATERIAL_MASTERDataTable();
                worker.DoWork += delegate(object sender1, DoWorkEventArgs e1)
                {
                    if (_currentData != null)
                    {
                        _materialInputDataTable.ImportRow(_currentData);
                    }
                    else
                    {
                        _materialInputDataTable = new DrugModel.MED_MATERIAL_INPUTDataTable();
                    }
                    _materialDataTable = objMaterial.GetMaterialMasterList();

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
                    this.MED_MATERIAL_INPUT.DataSource = _materialInputDataTable;
                    this.cbxUNITS.Properties.DataSource = _materialUnits;
                    if (_currentData == null)
                    {
                        this.MED_MATERIAL_INPUT.AddNew();
                        this.txt_ID.Text = Guid.NewGuid().ToString();
                        //this.txtMADE_DATE.EditValue = System.DateTime.Now;
                        this.txtUSELESS_DATE.EditValue = System.DateTime.Now.AddYears(3);
                        this.txtINPUT_DATE.EditValue = System.DateTime.Now;
                        this.txt_BATCH_NUMBER.Text = "20150701";
                        this.txt_InvoiceNo.Text = "20150701";
                        this.lupSTOREMANAGER.EditValue = HemoApplicationContext.Current.CurrentUser.EMP_NO;
                        this.txtOPERATOR_ID.EditValue = HemoApplicationContext.Current.CurrentUser.EMP_NO;
                        this.lupMaterialType.EditValue = this._materialTypes.FirstOrDefault(i => i.ITEM_NAME == "透析器").ITEM_ID;
                    }
                    this.Enabled = true;
                };
                worker.RunWorkerAsync();
            }

        }

        private int SaveData()
        {
            this.MED_MATERIAL_INPUT.EndEdit();
            this.MED_MATERIAL_INPUT.CurrencyManager.EndCurrentEdit();

            var row = _materialInputDataTable[0];
            row.CREATE_DATE = System.DateTime.Now;
            row.OPERATOR_ID = LoginUser.User.USER_ID;
            row.MATERIAL_NAME = this.lup_MaterialName.Text.ToString();
            row.APPLYID = LoginUser.User.USER_ID;
            row.STATUS = "1";
            row.PRICE = this.spiPRICE.Text;


            return objMaterial.SaveMedMaterialInputNew(_materialInputDataTable);
        }

        #endregion
    }
}