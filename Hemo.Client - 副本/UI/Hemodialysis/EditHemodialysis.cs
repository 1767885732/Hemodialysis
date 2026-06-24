/*----------------------------------------------------------------
 * Copyright (C) 2005 (苏州)医疗科技发展有限公司
 * 文件功能描述:透析修改
 * 创建标识:贺建操-2013年7月9日
 * 
 * 修改时间:2013年10月17日
 * 修改人:顾伟伟
 * 修改描述:修改方法SQL
 * 
 * 修改时间:2014年1月25日
 * 修改人:贺建操
 * 修改描述:修改方法SQL
 * 
 * 修改时间:2014年5月5日
 * 修改人:顾伟伟
 * 修改描述:新增方法
 * ----------------------------------------------------------------*/
using System;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Hemo.IService.Config;
using Hemo.Model;
using Hemo.Service;
using Hemo.Utilities;

namespace Hemo.Client.UI.Hemodialysis
{
    public partial class EditHemodialysis :HemoBaseFrm
    {
        #region 变量

        private HemodialysisModel.MED_BEFORE_HEMODIALYSIS_SIGNDataTable _hemodialysisDataTable;
        private HemodialysisModel.MED_BEFORE_HEMODIALYSIS_SIGNRow _hemodialysisRow;
        private IHemodialysis _hemodialysisService = ServiceManager.Instance.HemodialysisService;

        #endregion

        #region 构造函数

        public EditHemodialysis(HemodialysisModel.MED_BEFORE_HEMODIALYSIS_SIGNDataTable hemodialysisDataTable, HemodialysisModel.MED_BEFORE_HEMODIALYSIS_SIGNRow hemodialysisRow)
        {
            this.InitializeComponent();

            this._hemodialysisDataTable = hemodialysisDataTable;
            this._hemodialysisRow = hemodialysisRow;
        }

        #endregion

        #region 方法

        private void InitializeControls()
        {
            if (this._hemodialysisRow != null) //修改
            {
                this.txtPATIENT_ID.Text = this._hemodialysisRow.PATIENT_ID;
                this.txtWEIGHT.Text = this._hemodialysisRow.WEIGHT.ToString();
                this.txtSYSTOLIC_PRESSURE.Text = this._hemodialysisRow.SYSTOLIC_PRESSURE.ToString();
                this.txtDIASTOLIC_PRESSURE.Text = this._hemodialysisRow.DIASTOLIC_PRESSURE;
                this.txtHEAT_RATE.Text = this._hemodialysisRow.HEAT_RATE.ToString();
                this.txtHEPAR.Text = this._hemodialysisRow.HEPAR;
                this.txtSPLEEN.Text = this._hemodialysisRow.SPLEEN;
                this.txtKIDNEY.Text = this._hemodialysisRow.KIDNEY;
                this.txtBLOOD_TYPE.Text = this._hemodialysisRow.BLOOD_TYPE;
                this.txtREMARK.Text = this._hemodialysisRow.REMARK;
            }
        }

        private bool IsDataValidate()
        {
            this.errorProvider.SetError(this.txtPATIENT_ID, string.Empty);

            if (string.IsNullOrEmpty(this.txtPATIENT_ID.Text))
            {
                this.txtPATIENT_ID.Focus();

                this.errorProvider.SetError(this.txtPATIENT_ID, "病人唯一编号不能为空！");

                return false;
            }

            return true;
        }

        #endregion

        #region 事件

        private void EditHemodialysis_Load(object sender, EventArgs e)
        {
            this.InitializeControls();
        }

        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!this.IsDataValidate())
                return;

            if (this._hemodialysisRow == null) //新增
            {
                this._hemodialysisRow = this._hemodialysisDataTable.NewMED_BEFORE_HEMODIALYSIS_SIGNRow();

                this._hemodialysisRow.HEMODIALYSIS_SIGN_ID = Guid.NewGuid().ToString();
                this._hemodialysisRow.HEMODIALYSIS_ID = Guid.NewGuid().ToString();
                this._hemodialysisRow.CREATE_DATE = DateTime.Now;

                this._hemodialysisDataTable.AddMED_BEFORE_HEMODIALYSIS_SIGNRow(this._hemodialysisRow);
            }

            this._hemodialysisRow.PATIENT_ID = this.txtPATIENT_ID.Text;
            this._hemodialysisRow.WEIGHT = Utility.CDecimal(this.txtWEIGHT.Text);
            this._hemodialysisRow.SYSTOLIC_PRESSURE = Utility.CDecimal(this.txtSYSTOLIC_PRESSURE.Text);
            this._hemodialysisRow.DIASTOLIC_PRESSURE = this.txtDIASTOLIC_PRESSURE.Text;
            this._hemodialysisRow.HEAT_RATE = Utility.CDecimal(this.txtHEAT_RATE.Text);
            this._hemodialysisRow.HEPAR = this.txtHEPAR.Text;
            this._hemodialysisRow.SPLEEN = this.txtSPLEEN.Text;
            this._hemodialysisRow.KIDNEY = this.txtKIDNEY.Text;
            this._hemodialysisRow.BLOOD_TYPE = this.txtBLOOD_TYPE.Text;
            this._hemodialysisRow.REMARK = this.txtREMARK.Text;

            this._hemodialysisService.SaveBeforeHemodialysisSignInfo(this._hemodialysisDataTable);
            AutoClosedMsgBox.ShowForm("保存成功。", "系统提示", 1500, MessageBoxIcon.Information);

           // XtraMessageBox.Show("保存成功！");

            this.DialogResult = DialogResult.Yes;
        }

        #endregion
    }
}
