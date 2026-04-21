/*----------------------------------------------------------------
// Copyright (C) 2013 苏州XX公司有限公司
// 描述：耗材厂商维护窗体
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

namespace Hemo.Client.UI.Material {
    public partial class EditMaterialFirm :HemoBaseFrm {

        #region 私有成员

        /// <summary>
        /// 药品主档数据服务对象
        /// </summary>
        private IDrug objDrug = ServiceManager.Instance.DrugService;
        /// <summary>
        /// 是否新增
        /// </summary>
        private bool isAdd = true;
        #endregion

        #region 初始化方法
        public EditMaterialFirm() {
            InitializeComponent();
        }

        public EditMaterialFirm(bool pIsAdd, string pFirmID) {
            InitializeComponent();
            isAdd = pIsAdd;
            txtFIRM_ID.Text = pFirmID;
        }

        /// <summary>
        /// 保存之前判断数据输入是否合理 
        /// </summary>
        /// <returns></returns>
        private bool IsDataValidate() {
            bool result = true;

            if (txtFIRM_NAME.Text.Length == 0) {
                errorProvider.SetError(txtFIRM_NAME, "请输入厂商名称。");
                return false;
            }
            else {
                errorProvider.SetError(txtFIRM_NAME, string.Empty);
            }

            if (txtTELEPHONE.Text.Length > 0) {
                result = Utility.IsTel(txtTELEPHONE.Text);
                if (!result) {
                    errorProvider.SetError(txtTELEPHONE, "请输入正确的电话格式。（电话：010-2000000）");
                    return result;
                }
                else {
                    errorProvider.SetError(txtTELEPHONE, string.Empty);
                }
            }

            if (txtFAX.Text.Length > 0) {
                result = Utility.IsTel(txtFAX.Text);
                if (!result) {
                    errorProvider.SetError(txtFAX, "请输入正确的传真格式。（电话：010-2000000）");
                    return result;
                }
                else {
                    errorProvider.SetError(txtFAX, string.Empty);
                }
            }

            if (txtMOBILE_PHONE.Text.Length > 0) {
                result = Utility.IsMobile(txtMOBILE_PHONE.Text);
                if (!result) {
                    errorProvider.SetError(txtMOBILE_PHONE, "请输入正确的手机格式。（手机：13800000000）");
                    return result;
                }
                else {
                    errorProvider.SetError(txtMOBILE_PHONE, string.Empty);
                }
            }

            return result;
        }

        /// <summary>
        /// 药品主档表
        /// </summary>
        DrugModel.MED_DRUG_FIRMDataTable tmpDataTable = new DrugModel.MED_DRUG_FIRMDataTable();
        /// <summary>
        ///根据药厂编号得到对应数据并赋值
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EditDrugFirm_Load(object sender, EventArgs e) {
            // string firmID = ((QueryMaterialFirm)this.Owner).FirmID;
            // isAdd = ((QueryMaterialFirm)this.Owner).IsAdd;
            string firmID = txtFIRM_ID.Text.Trim();
            if (!isAdd) {
                if (firmID.Length > 0) {
                    tmpDataTable = objDrug.GetDrugFrimListByFirmID(firmID);
                    if (tmpDataTable != null && tmpDataTable.Rows.Count > 0) {
                        BaseControlInfo.SetControlDataByDataTable(tmpDataTable, pnlControls);
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
        private void txtFIRM_NAME_Leave(object sender, EventArgs e) {
            txtFIRM_PINYIN.Text = PinYinConverter.GetPYString(txtFIRM_NAME.Text);
        }

        /// <summary>
        /// 保存数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSave_Click(object sender, EventArgs e) {
            if (IsDataValidate()) {
                if (saveData() == 1) {
                    XtraMessageBox.Show("保存成功", "药品厂商");
                    this.DialogResult = System.Windows.Forms.DialogResult.Yes;
                }
            }
        }

        /// <summary>
        /// 新增数据 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAdd_Click(object sender, EventArgs e) {
            isAdd = true;
            BaseControlInfo.ClearControlText(pnlControls);
            txtFIRM_NAME.Focus();
        }

        private void btnCancel_Click(object sender, EventArgs e) {
            this.Close();
        }

        #endregion

        #region 数据方法
        /// <summary>
        /// 保存数据
        /// </summary>
        /// <returns></returns>
        private int saveData() {
            int result = 0;
            if (isAdd) {
                txtFIRM_ID.Text = objDrug.GetNewFirmID();//需要新生成药品厂商ID
            }
            DataTable dt = BaseControlInfo.GetDataTableByPanel(tmpDataTable, pnlControls, isAdd);
            result = objDrug.SaveDrugFirmInfo((DrugModel.MED_DRUG_FIRMDataTable)dt);
            return result;
        }
        #endregion




    }
}