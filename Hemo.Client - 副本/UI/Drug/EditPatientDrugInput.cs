/*----------------------------------------------------------------
// Copyright (C) 2005 苏州XX公司有限公司
// 描述：患者药品托管窗体
// 创建时间：2013-05-21
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
using Hemo.IService;
using Hemo.Service;
using Hemo.Model;
using Hemo.Utilities;
using Hemo.Client.Core;
using Hemo.IService.Config;

namespace Hemo.Client.UI.Drug {
    public partial class EditPatientDrugInput : HemoBaseFrm
    {
        #region 类变量

        private string currentHemoId = string.Empty;
        private string drugSum;
        private IDrug objDrug = ServiceManager.Instance.DrugService;
        private DrugModel.MED_DRUG_MASTERDataTable _relationData = new DrugModel.MED_DRUG_MASTERDataTable();
        private IConfig _configService = ServiceManager.Instance.ConfigService;

        #endregion

        #region 属性

        public string CurrentHemoId
        {
            get { return currentHemoId; }
            set { currentHemoId = value; }
        }

        #endregion

        #region 构造函数

        public EditPatientDrugInput()
        {
            InitializeComponent();
            _relationData = objDrug.GetDaleGateDrugMasterList();
        }

        #endregion

        #region 事件

        private void EditPatientDrugInput_Load(object sender, EventArgs e)
        {
            ctlUserLongInfo1.HEMODIALYSIS_ID = currentHemoId;
            ctlUserLongInfo1.LoadPatientInfo();
            //载入药品信息
            txtDRUG_NAME.Properties.DataSource = objDrug.GetDaleGateDrugMasterList();
            txtDRUG_NAME.Properties.PopupFormSize = new Size(400, 230);
            txtDRUG_NAME.Properties.DisplayMember = "DRUG_NAME";//要显示的字段,Text获得
            txtDRUG_NAME.Properties.ValueMember = "DRUG_CODE";//实际值的字段,EditValue获得 // DeptID


            BaseControlInfo.BindLookUpEdit(cbxUNITS, "ITEM_ID", "ITEM_NAME", this._configService.GetConfigList(string.Empty, string.Empty, "药品单位", "1"), "ITEM_NAME", "药品单位");

            BaseControlInfo.BindLookUpEdit(cbxDRUG_TYPE, "ITEM_ID", "ITEM_NAME", this._configService.GetConfigList(string.Empty, string.Empty, "托管药品分类", "1"), "ITEM_NAME", "药品单位");

        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtDRUG_NAME_EditValueChanged(object sender, EventArgs e)
        {
            var row = _relationData.FindByDRUG_CODE(this.txtDRUG_NAME.EditValue.ToString().Trim());

            if (row != null)
            {
                txtDRUG_SPEC.Text = row.DRUG_SPEC;
                cbxDRUG_TYPE.EditValue = row.DRUG_TYPE.ToString();
                cbxUNITS.EditValue = row.UNITS.ToString();
                spiPRICE.EditValue = row.PRICE;
                this.txtFIRM_NAME.Text = row.FIRM_NAME;
                this.txtFIRM_ID.Text = row.FIRM_ID;
            }
            else
            {
                txtDRUG_SPEC.Text = string.Empty;
                cbxDRUG_TYPE.EditValue = string.Empty;
                cbxUNITS.EditValue = string.Empty;
                cbxUNITS.Text = string.Empty;
                spiPRICE.EditValue = string.Empty;
                this.txtFIRM_NAME.Text = string.Empty;
                this.txtFIRM_ID.Text = string.Empty;
            }
            DataTable dtdrugSum = objDrug.GetPatientDrugNumberByParam(currentHemoId, this.txtDRUG_NAME.EditValue.ToString().Trim());
            if (dtdrugSum.Rows.Count == 0)
            {
                drugSum = "0";
            }
            else
            {
                drugSum = dtdrugSum.Rows[0]["drug_Sum"].ToString();
            }
            if (!string.IsNullOrEmpty(txtDRUG_NAME.Text))
                lbDrugInfo.Text = string.Format("{0}当前的托管数量为:{1} {2}", txtDRUG_NAME.Text, drugSum, cbxUNITS.Text);
            txt_COUNT.Text = "";
            txt_COUNT.Focus();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (CheckInputValue())
                {
                    SaveData();
                    txt_COUNT.Text = "";
                    txtDRUG_NAME.EditValue = "";
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "药品托管信息");
            }
            this.Close();
        }

        private void txt_COUNT_KeyPress(object sender, KeyPressEventArgs e)
        {
            //判断按键是不是要输入的类型。
            if (((int)e.KeyChar < 48 || (int)e.KeyChar > 57) && (int)e.KeyChar != 8 && (int)e.KeyChar != 46)
                e.Handled = true;

            //小数点的处理。
            if ((int)e.KeyChar == 46)                           //小数点
            {
                if (txt_COUNT.Text.Length <= 0)
                    e.Handled = true;   //小数点不能在第一位
                else
                {
                    float f;
                    float oldf;
                    bool b1 = false, b2 = false;
                    b1 = float.TryParse(txt_COUNT.Text, out oldf);
                    b2 = float.TryParse(txt_COUNT.Text + e.KeyChar.ToString(), out f);
                    if (b2 == false)
                    {
                        if (b1 == true)
                            e.Handled = true;
                        else
                            e.Handled = false;
                    }
                }
            }
        }

        private void labelControl4_Click(object sender, EventArgs e)
        {

        }

        #endregion

        #region 方法

        private void SaveData()
        {
            DrugModel.MED_PATIENT_DRUG_INPUTDataTable dtRecord = new DrugModel.MED_PATIENT_DRUG_INPUTDataTable();
            var row = dtRecord.NewMED_PATIENT_DRUG_INPUTRow();
            row.ID = Guid.NewGuid().ToString().Trim();
            row.HEMODIALYSIS_ID = currentHemoId;
            row.DRUG_CODE = this.txtDRUG_NAME.EditValue.ToString().Trim();
            row.DRUG_COUNT = Utility.CDecimal(txt_COUNT.Text.ToString());
            row.INPUT_DATE = DateTime.Now.Date;
            row.APPLYID = HemoApplicationContext.Current.CurrentUser.USER_ID;
            row.STATUS = "0";
            row.ISDELETE = "0";
            row.DRUG_REMAIN = Utility.CDecimal(txt_COUNT.Text.ToString());
            row.INPUT_ID = Guid.NewGuid().ToString().Trim();
            row.DRUG_SUM = Utility.CDecimal(drugSum) + Utility.CDecimal(txt_COUNT.Text.ToString());
            dtRecord.AddMED_PATIENT_DRUG_INPUTRow(row);
            int result = objDrug.SavePatientDrugInput(dtRecord);
            if (result > 0)
            {
                int resultUpdate = objDrug.UpdatePatientDrugInputByParam(currentHemoId, this.txtDRUG_NAME.EditValue.ToString().Trim(), row.DRUG_SUM);
                if (resultUpdate > 0)
                {
                    XtraMessageBox.Show("保存患者药品托管信息成功！");
                }
                else
                {
                    XtraMessageBox.Show("更新患者药品托管库存信息失败！");
                }
            }
            else
            {
                XtraMessageBox.Show("保存患者药品托管信息失败！");
            }
        }

        private bool CheckInputValue()
        {
            bool result = true;
            if (string.IsNullOrEmpty(this.txtDRUG_NAME.Text))
            {
                this.errorProvider.SetError(this.txtDRUG_NAME, "药品名称必须录入");
                return false;
            }
            if (int.Parse(this.txt_COUNT.Text) <= 0)
            {
                this.errorProvider.SetError(this.txt_COUNT, "请输入托管数量！");
                return false;
            }
            return result;
        }

        #endregion  
    }
}