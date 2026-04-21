/*----------------------------------------------------------------
// Copyright (C) 2013 苏州XX公司有限公司
// 描述：药品主档维护窗体
// 创建时间：2013-03-21
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
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Hemo.Service;
using Hemo.Model;
using Hemo.IService;
using Hemo.Utilities;
using Hemo.IService.Config;

namespace Hemo.Client.UI.Drug
{
    public partial class EditDrugMaster : HemoBaseFrm
    {
        #region 私有成员

        /// <summary>
        /// 药品主档数据服务对象
        /// </summary>
        private IDrug objDrug = ServiceManager.Instance.DrugService;
        //private DrugService objDrug = new DrugService();
        private IConfig _configService = ServiceManager.Instance.ConfigService;

        /// <summary>
        /// 是否新增
        /// </summary>
        private bool isAdd = true;
        #endregion

        #region 初始化方法
        public EditDrugMaster()
        {
            InitializeComponent();
        }
        public EditDrugMaster(bool pIsAdd, string pDrugCode)
        {
            InitializeComponent();
            isAdd = pIsAdd;
            txtDRUG_CODE.Text = pDrugCode;
            BaseControlInfo.BindLookUpEdit(cbxUNITS, "ITEM_ID", "ITEM_NAME", this._configService.GetConfigList(string.Empty, string.Empty, "药品单位", "1"), "ITEM_NAME", "药品单位");
            var date = this._configService.GetConfigList(string.Empty, string.Empty, "药品分类", "1");
            var gateDrug = this._configService.GetConfigList(string.Empty, string.Empty, "托管药品分类", "1");
            date.LoadDataRow(gateDrug.Rows[0].ItemArray, true);
            BaseControlInfo.BindLookUpEdit(cbxDRUG_TYPE, "ITEM_ID", "ITEM_NAME", date, "ITEM_NAME", "药品分类");

            txtFIRM_NAME.Properties.DataSource = objDrug.GetDrugFirmListByFirmType("0");

        }



        /// <summary>
        /// 药品主档表
        /// </summary>
        DrugModel.MED_DRUG_MASTERDataTable tmpDataTable = new DrugModel.MED_DRUG_MASTERDataTable();
        /// <summary>
        ///根据药品编号得到对应数据并赋值
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EditDrugMaster_Load(object sender, EventArgs e)
        {
            string drugCode = txtDRUG_CODE.Text;
            //string drugCode = ((QueryDrugMaster)this.Owner).DrugCode;
            txtCREATE_DATE.Text = System.DateTime.Now.ToShortDateString();
            // isAdd = ((QueryDrugMaster)this.Owner).IsAdd;
            if (!isAdd)
            {
                if (drugCode.Length > 0)
                {
                    tmpDataTable = objDrug.GetDrugMasterListByDrugCode(drugCode);
                    if (tmpDataTable != null && tmpDataTable.Rows.Count > 0)
                    {
                        BaseControlInfo.SetControlDataByDataTable(tmpDataTable, pnlControls);

                        this.txtFIRM_NAME.EditValue = tmpDataTable[0].FIRM_ID;
                        this.cbxUNITS.EditValue = tmpDataTable[0].UNITS;
                        this.txtMADE_DATE.EditValue = tmpDataTable[0].MADE_DATE;
                        this.txtUSELESS_DATE.EditValue = tmpDataTable[0].USELESS_DATE;



                    }
                }
            }
        }
        #endregion

        #region 事件
        /// <summary>
        /// 拼音码
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtDRUG_NAME_Leave(object sender, EventArgs e)
        {
            txtDRUG_PINYIN.Text = PinYinConverter.GetPYString(txtDRUG_NAME.Text);
        }
        /// <summary>
        /// 保存之前判断数据输入是否合理 
        /// </summary>
        /// <returns></returns>
        private bool IsDataValidate()
        {
            bool result = true;
            int iDate = 0;
            this.errorProvider.ClearErrors();
            if (txtCREATE_DATE.Text.Length > 0)
            {
                iDate = Utility.CDate(txtCREATE_DATE.Text).CompareTo(System.DateTime.Now);
                if (iDate > 0)
                {
                    txtCREATE_DATE.Focus();
                    errorProvider.SetError(txtCREATE_DATE, "请输入正确的录入时间。");
                    return false;
                }
                else
                {
                    errorProvider.SetError(txtCREATE_DATE, string.Empty);
                }
            }
            if (string.IsNullOrEmpty(this.cbxDRUG_TYPE.EditValue.ToString()))
            {
                errorProvider.SetError(cbxDRUG_TYPE, "请选择药品类型。");
                return false;
            }
            if (string.IsNullOrEmpty(this.cbxUNITS.EditValue.ToString()))
            {
                errorProvider.SetError(cbxUNITS, "请选择单位。");
                return false;
            }

            if (string.IsNullOrEmpty(spiPRICE.EditValue.ToString()))
            {
                errorProvider.SetError(spiPRICE, "请输入单价。");
                return false;
            }

            if (!string.IsNullOrEmpty(spiPRICE.EditValue.ToString()))
            {
                if ( Convert.ToDouble(spiPRICE.EditValue.ToString()) < 0)
                {
                    errorProvider.SetError(spiPRICE, "单价必须大于0。");
                    return false;
                }
            }

            return result;
        }

        private void cbxDRUG_TYPE_EditValueChanged(object sender, EventArgs e)
        {
            if (this.cbxDRUG_TYPE.Text == "托管药品类别")
            {
                this.txtMEMOS.Text = "用于托管药品功能";
            }
            else
            {
                this.txtMEMOS.Text = string.Empty;
            }
        }

        private void txtFIRM_NAME_EditValueChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(this.txtFIRM_ID.Text))
            {
                this.txtFIRM_ID.Text = string.Empty;
            }
            try
            {
                this.txtFIRM_ID.Text = this.txtFIRM_NAME.EditValue.ToString();
            }
            catch
            {
                this.txtFIRM_ID.Text = string.Empty;

            }

        }

        /// <summary>
        /// 保存数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (IsDataValidate())
            {
                if (saveData() == 1)
                {
                    AutoClosedMsgBox.ShowForm("保存成功", "药品主档", 1000, MessageBoxIcon.Information);
                    this.DialogResult = System.Windows.Forms.DialogResult.Yes;
                    //    BaseControlInfo.ClearControlText(pnlControls);
                }
            }
        }

        /// <summary>
        /// 新增数据 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAdd_Click(object sender, EventArgs e)
        {
            isAdd = true;
            BaseControlInfo.ClearControlText(pnlControls);
            txtDRUG_NAME.Focus();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion

        #region 数据方法
        /// <summary>
        /// 保存数据
        /// </summary>
        /// <returns></returns>
        private int saveData()
        {
            int result = 0;
            if (isAdd)
            {
                txtDRUG_CODE.Text = objDrug.GetNewDrugCode();//需要新生成血透号药品编号
            }
            DataTable dt = BaseControlInfo.GetDataTableByPanel(tmpDataTable, pnlControls, isAdd);
            result = objDrug.SaveDrugMasterInfo((DrugModel.MED_DRUG_MASTERDataTable)dt);
            return result;
        }
        #endregion
    }
}