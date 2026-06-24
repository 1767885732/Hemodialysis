/*----------------------------------------------------------------
// Copyright (C) 2005 苏州XX公司有限公司
// 描述：维护促红素信息窗体
// 创建时间：2014-09-14
// 创建者：刘超
//  
// 修改时间：
// 修改人：
// 修改描述：
----------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Hemo.Client.Core;
using Hemo.IService;
using Hemo.IService.Config;
using Hemo.IService.Erythropoietin;
using Hemo.Model;
using Hemo.Service;
using Hemo.Utilities;

namespace Hemo.Client.UI.Erythropoietin {
    public partial class EditErythropoietinFrm :HemoBaseFrm{
        #region 变量

        private PatientModel.MED_PATIENTSRow _patientRow;
        private ErythropoietinModel.MED_ERYTHROPOIETINDataTable _erythropoietinDataTable;
        private ErythropoietinModel.MED_ERYTHROPOIETINRow _erythropoietinDataRow;
        private IConfig _configService = ServiceManager.Instance.ConfigService;
        private IDrug _drugService = ServiceManager.Instance.DrugService;
        private IErythropoietin _erythropoietinService = ServiceManager.Instance.ErythropoietinService;

        #endregion

        #region 构造函数

        public EditErythropoietinFrm(PatientModel.MED_PATIENTSRow patientRow, ErythropoietinModel.MED_ERYTHROPOIETINDataTable erythropoietinDataTable, ErythropoietinModel.MED_ERYTHROPOIETINRow erythropoietinDataRow) {
            this.InitializeComponent();

            this._patientRow = patientRow;
            this._erythropoietinDataTable = erythropoietinDataTable;
            this._erythropoietinDataRow = erythropoietinDataRow;
        }

        #endregion

        #region 方法

        private void InitializeControls() {
            List<string> codeList = this._configService.GetConfigList(string.Empty, string.Empty, "促红素设定", "1").Select(r => r.ITEM_VALUE.ToLower()).ToList();
            var filterResult = this._drugService.GetDrugMasterList().Where(r => codeList.Contains(r.DRUG_CODE.Trim().ToLower()));
            DataTable dtDrugMaster = null;

            if (filterResult.Count() > 0)
                dtDrugMaster = filterResult.CopyToDataTable();

            Utility.BindLookUpEdit(this.cbxDRUG_NAME, "DRUG_CODE", "DRUG_NAME", dtDrugMaster, "DRUG_NAME", "药品名称");
            Utility.BindLookUpEdit(this.cbxDRUG_MODE, "ITEM_ID", "ITEM_NAME", this._configService.GetConfigList(string.Empty, string.Empty, "注射方式", "1"), "ITEM_NAME", "注射方式");
            Utility.BindLookUpEdit(this.cbxUNIT, "ITEM_ID", "ITEM_NAME", this._configService.GetConfigList(string.Empty, string.Empty, "药品单位", "1"), "ITEM_NAME", "药品单位");

            if (this._erythropoietinDataRow != null) //修改
            {
                this.cbxDRUG_NAME.EditValue = this._erythropoietinDataRow.DRUG_CODE;
                this.cbxQW.EditValue = this._erythropoietinDataRow.QW;
                this.cbxTIME_TYPE.EditValue = this._erythropoietinDataRow.TIME_TYPE;
                this.txtFREQUENCY.Text = this._erythropoietinDataRow.FREQUENCY;
                this.cbxHEMOGLOBIN_TYPE.EditValue = this._erythropoietinDataRow.ERYTHROPOIETIN_TYPE;
                this.cbxDRUG_MODE.EditValue = this._erythropoietinDataRow.DRUG_MODE;
                this.txtDOSAGE.Text = this._erythropoietinDataRow.DOSAGE;
                this.cbxUNIT.EditValue = this._erythropoietinDataRow.UNIT;
                if (!this._erythropoietinDataRow.IsREMARKNull())
                    this.txtREMARK.Text = this._erythropoietinDataRow.REMARK;
            }
        }

        private bool IsDataValidate() {
            this.errorProvider.SetError(this.cbxDRUG_NAME, string.Empty);
            this.errorProvider.SetError(this.txtFREQUENCY, string.Empty);
            this.errorProvider.SetError(this.cbxDRUG_MODE, string.Empty);
            this.errorProvider.SetError(this.txtDOSAGE, string.Empty);
            this.errorProvider.SetError(this.cbxUNIT, string.Empty);

            if (string.IsNullOrEmpty(this.cbxDRUG_NAME.Text)) {
                this.cbxDRUG_NAME.Focus();

                this.errorProvider.SetError(this.cbxDRUG_NAME, "请选择药品！");

                return false;
            }

            if (string.IsNullOrEmpty(this.txtFREQUENCY.Text)) {
                this.txtFREQUENCY.Focus();

                this.errorProvider.SetError(this.txtFREQUENCY, "请填写频次！");

                return false;
            }

            if (string.IsNullOrEmpty(this.cbxDRUG_MODE.Text)) {
                this.cbxDRUG_MODE.Focus();

                this.errorProvider.SetError(this.cbxDRUG_MODE, "请选择用药途径！");

                return false;
            }

            if (string.IsNullOrEmpty(this.txtDOSAGE.Text)) {
                this.txtDOSAGE.Focus();

                this.errorProvider.SetError(this.txtDOSAGE, "请填写剂量！");

                return false;
            }

            if (string.IsNullOrEmpty(this.cbxUNIT.Text)) {
                this.cbxUNIT.Focus();

                this.errorProvider.SetError(this.cbxUNIT, "请选择单位！");

                return false;
            }

            return true;
        }

        #endregion

        #region 事件

        private void btnClose_Click(object sender, EventArgs e) {
            this.Close();
        }

        private void EditErythropoietinFrm_Load(object sender, EventArgs e) {
            this.InitializeControls();
        }

        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSave_Click(object sender, EventArgs e) {
            if (!this.IsDataValidate())
                return;

            if (this._erythropoietinDataRow == null) //新增
            {
                this._erythropoietinDataRow = this._erythropoietinDataTable.NewMED_ERYTHROPOIETINRow();

                this._erythropoietinDataRow.ERYTHROPOIETIN_ID = Guid.NewGuid().ToString();
                this._erythropoietinDataRow.CREATE_USERID = LoginUser.User.USER_ID;
                this._erythropoietinDataRow.CREATE_TIME = DateTime.Now;
                this._erythropoietinDataRow.HEMODIALYSIS_ID = this._patientRow.HEMODIALYSIS_ID;

                this._erythropoietinDataTable.AddMED_ERYTHROPOIETINRow(this._erythropoietinDataRow);
            }

            this._erythropoietinDataRow.DRUG_CODE = this.cbxDRUG_NAME.EditValue.ToString();
            this._erythropoietinDataRow.QW = this.cbxQW.EditValue.ToString();
            this._erythropoietinDataRow.TIME_TYPE = this.cbxTIME_TYPE.EditValue.ToString();
            this._erythropoietinDataRow.FREQUENCY = this.txtFREQUENCY.Text;
            this._erythropoietinDataRow.ERYTHROPOIETIN_TYPE = this.cbxHEMOGLOBIN_TYPE.EditValue.ToString();
            this._erythropoietinDataRow.DRUG_MODE = this.cbxDRUG_MODE.EditValue.ToString();
            this._erythropoietinDataRow.DOSAGE = this.txtDOSAGE.Text;
            this._erythropoietinDataRow.UNIT = this.cbxUNIT.EditValue.ToString();
            this._erythropoietinDataRow.REMARK = this.txtREMARK.Text;

            this._erythropoietinService.SaveErythropoietinInfo(this._erythropoietinDataTable);
            AutoClosedMsgBox.ShowForm("保存成功。", "系统提示", 1500, MessageBoxIcon.Information);

            //XtraMessageBox.Show("保存成功！");

            this.DialogResult = DialogResult.Yes;
        }

        #endregion
    }
}
