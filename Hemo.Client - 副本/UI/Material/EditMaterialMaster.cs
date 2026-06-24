/*----------------------------------------------------------------
 * Copyright (C) 2005 (苏州)医疗科技发展有限公司
 * 文件功能描述:用户控件类
 * 创建标识:贺建操-2013年6月17日
 * 
 * 修改时间:2013年9月25日
 * 修改人:吕志强
 * 修改描述:修改方法SQL
 * 
 * 修改时间:2014年1月3日
 * 修改人:贺建操
 * 修改描述:修改方法
 * 
 * 修改时间:2014年4月13日
 * 修改人:顾伟伟
 * 修改描述:修改方法SQL
 * ----------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Hemo.Service;
using Hemo.Model;
using Hemo.IService;
using Hemo.Utilities;
using Hemo.IService.Config;


namespace Hemo.Client.UI.Material
{
    public partial class EditMaterialMaster : HemoBaseFrm
    {

        #region 私有成员
        /// <summary>
        /// 药品主档数据服务对象
        /// </summary>
        private IMaterial objMaterial = ServiceManager.Instance.MaterialService;
        private IConfig _configService = ServiceManager.Instance.ConfigService;
        private ConfigModel.MED_COMMON_ITEMLISTDataTable _materialTypes;
        private MaterialModel.MED_MATERIAL_MASTERDataTable _materialDataTable;
        private IDrug objDrug = ServiceManager.Instance.DrugService;
        private ConfigModel.MED_COMMON_ITEMLISTDataTable _materialUnits;

        private MaterialModel.MED_MATERIAL_MASTERRow _currentData;

        public MaterialModel.MED_MATERIAL_MASTERRow CurrentData
        {
            get { return _currentData; }
            set { _currentData = value; }
        }

        public MaterialModel.MED_MATERIAL_MASTERDataTable _materialdtMaster { get; set; }

        #endregion

        /// <summary>
        /// 构造函数
        /// </summary>
        public EditMaterialMaster()
        {
            InitializeComponent();
        }
        #region 事件
        private void EditMaterialMaster_Load(object sender, EventArgs e)
        {
            InzationMaterialDate();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            Clear();
            this.txtMATERIAL_ID.Text = Guid.NewGuid().ToString();
        }
        /// <summary>
        /// 验证输入内容
        /// </summary>
        /// <returns></returns>
        private bool CheckInputValue()
        {
            bool result = true;
            this.errorProvider.ClearErrors();
            if (string.IsNullOrEmpty(this.txtMATERIAL_NAME.Text))
            {
                this.errorProvider.SetError(this.txtMATERIAL_NAME, "耗材名称必须录入");
                return false;
            }
            if (string.IsNullOrEmpty(this.customGridLookUpEdit_Type.Text) || string.IsNullOrEmpty(this.customGridLookUpEdit_Type.EditValue.ToString()))
            {
                this.errorProvider.SetError(this.customGridLookUpEdit_Type, "耗材类型不能为空");
                return false;
            }
            if (string.IsNullOrEmpty(this.txtMATERIAL_SPEC.Text))
            {
                this.errorProvider.SetError(this.txtMATERIAL_SPEC, "耗材规格不能为空");
                return false;
            }
            if (string.IsNullOrEmpty(this.txtVALID_DATE.Text))
            {
                this.errorProvider.SetError(this.txtVALID_DATE, "耗材有效期不能为空");
                return false;
            }
            if (string.IsNullOrEmpty(this.cbxUNITS.Text))
            {
                this.errorProvider.SetError(this.cbxUNITS, "入库耗材单位必须录入");
                return false;
            }
            if (_currentData == null && _materialdtMaster != null && _materialdtMaster.Rows.Count > 0)
            {
                var row = _materialdtMaster.FirstOrDefault(i => i.MATERIAL_NAME == this.txtMATERIAL_NAME.Text && i.MATERIAL_TYPE == this.customGridLookUpEdit_Type.EditValue.ToString() && i.FIRM_NAME == this.txtFIRM_NAME.EditValue.ToString() && i.MATERIAL_SPEC == this.txtMATERIAL_SPEC.Text.Trim());
                if (row != null)
                {
                    this.errorProvider.SetError(this.txtMATERIAL_NAME, "耗材名称不能重复");
                    return false;
                }
            }

            if (spiPRICE.EditValue == null)
            {
                errorProvider.SetError(spiPRICE, "请输入价格。");
                return false;
            }

            if (string.IsNullOrEmpty(spiPRICE.EditValue.ToString()))
            {
                errorProvider.SetError(spiPRICE, "请输入价格。");
                return false;
            }

            if (!string.IsNullOrEmpty(spiPRICE.EditValue.ToString()))
            {
                if (Convert.ToDouble(spiPRICE.EditValue.ToString()) < 0)
                {
                    errorProvider.SetError(spiPRICE, "价格必须大于0。");
                    return false;
                }
            }




            return result;
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (CheckInputValue())
                {
                    if (SaveData() > 0) { XtraMessageBox.Show("保存成功。", "基础信息"); }
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

        private void txtMATERIAL_NAME_EditValueChanged(object sender, EventArgs e)
        {
            txtMATERIAL_PINYIN.Text = PinYinConverter.GetPYString(txtMATERIAL_NAME.Text);
        }


        #endregion

        #region 数据方法
        /// <summary>
        /// 清除界面数据
        /// </summary>
        private void Clear()
        {
            this.txtMATERIAL_NAME.Text = string.Empty;
            this.txtMATERIAL_PINYIN.Text = string.Empty;
            this.customGridLookUpEdit_Type.Text = string.Empty;
            this.txtFIRM_NAME.Text = string.Empty;
            this.txtMATERIAL_SPEC.Text = string.Empty;
            this.txtSYSTEM_BARCODE.Text = string.Empty;
            this.txtPACK_BARCODE.Text = string.Empty;
            this.txtMEMO.Text = string.Empty;
        }

        public void InzationMaterialDate()
        {
            this.Enabled = false;
            _materialDataTable = new MaterialModel.MED_MATERIAL_MASTERDataTable();
            var _firmDataTable = new DrugModel.MED_DRUG_FIRMDataTable();
            _materialUnits = new ConfigModel.MED_COMMON_ITEMLISTDataTable();
            using (BackgroundWorker worker = new BackgroundWorker())
            {
                worker.DoWork += delegate(object sender1, DoWorkEventArgs e1)
                {
                    if (_currentData != null)
                    {
                        _materialDataTable.ImportRow(_currentData);
                    }
                    else
                    {
                        _materialDataTable = new MaterialModel.MED_MATERIAL_MASTERDataTable();
                    }
                    var date = this._configService.GetConfigList(string.Empty, string.Empty, "辅材类型", "1");
                    this._materialUnits = this._configService.GetConfigList(string.Empty, string.Empty, "耗材单位", "1");

                    this._materialTypes = date;
                    _firmDataTable = objDrug.GetDrugFirmListByFirmType("1");



                };
                worker.RunWorkerCompleted += delegate(object sender2, RunWorkerCompletedEventArgs e2)
                {
                    this.customGridLookUpEdit_Type.Properties.DataSource = this._materialTypes;
                    this.cbxUNITS.Properties.DataSource = _materialUnits;
                    this.MED_MATERIAL_MASTER.DataSource = _materialDataTable;
                    this.txtFIRM_NAME.Properties.DataSource = _firmDataTable;
                    if (_currentData == null)
                    {
                        this.MED_MATERIAL_MASTER.AddNew();
                        this.txtMATERIAL_ID.Text = Guid.NewGuid().ToString();
                        this.txtCREATE_DATE.EditValue = System.DateTime.Now;
                        this.txtVALID_DATE.EditValue = System.DateTime.Now.AddYears(3);
                    }
                    this.Enabled = true;
                };
                worker.RunWorkerAsync();
            }

        }

        private int SaveData()
        {
            this.MED_MATERIAL_MASTER.EndEdit();
            this.MED_MATERIAL_MASTER.CurrencyManager.EndCurrentEdit();

            var row = _materialDataTable[0];
            row.CREATE_DATE = System.DateTime.Now;
            return objMaterial.SaveMaterialMasterInfo(_materialDataTable);
        }
        #endregion

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void customGridLookUpEdit_Type_EditValueChanged(object sender, EventArgs e)
        {

        }

    }
}