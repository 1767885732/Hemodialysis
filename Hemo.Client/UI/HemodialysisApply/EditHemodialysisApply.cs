/*----------------------------------------------------------------
// Copyright (C) 2005 苏州XX公司有限公司
// 描述：预约申请窗体类
// 创建时间：2014-08-17
// 创建者：刘超
//  
// 修改时间：
// 修改人：
// 修改描述：
----------------------------------------------------------------*/

using System;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Hemo.IService.PatientSchedule;
using Hemo.Model;
using Hemo.Service;
using Hemo.Utilities;

namespace Hemo.Client.UI.HemodialysisApply
{
    public partial class EditHemodialysisApply :HemoBaseFrm
    {
        #region 变量

        private int _banciID;
        private int _weekDay;
        private string _hemodialysisID;
        private IPatientSchedule _patientScheduleService = ServiceManager.Instance.PatientSchedule;
        private PatientScheduleModel.MED_HEMO_APPLYDataTable _hemodialysisApplyDataTable;

        #endregion

        #region 属性

        public PatientScheduleModel.MED_HEMO_APPLYRow HemodialysisApplyRow
        {
            private set;
            get;
        }

        #endregion

        #region 构造函数

        public EditHemodialysisApply(int banciID, int weekDay, string hemodialysisID, PatientScheduleModel.MED_HEMO_APPLYDataTable hemodialysisApplyDataTable, PatientScheduleModel.MED_HEMO_APPLYRow hemodialysisApplyRow)
        {
            this.InitializeComponent();

            this._banciID = banciID;
            this._weekDay = weekDay;
            this._hemodialysisID = hemodialysisID;
            this._hemodialysisApplyDataTable = hemodialysisApplyDataTable;
            this.HemodialysisApplyRow = hemodialysisApplyRow;
        }

        #endregion

        #region 方法

        private DateTime GetDateTime(string text)
        {
            return DateTime.Parse(string.Format("{0} {1}", DateTime.Now.ToShortDateString(), text));
        }

        private void InitializeControls()
        {
            if (this.HemodialysisApplyRow == null) //新增
            {
                this.txtSTART_TIME.Time = DateTime.Now;
                this.txtEND_TIME.Time = DateTime.Now.AddHours(1);
            }
            else //修改
            {
                this.txtSTART_TIME.Time = this.GetDateTime(this.HemodialysisApplyRow.START_TIME);
                this.txtEND_TIME.Time = this.GetDateTime(this.HemodialysisApplyRow.END_TIME);
                if (!this.HemodialysisApplyRow.IsAPPLY_COMMENTNull())
                    this.txtAPPLY_COMMENT.Text = this.HemodialysisApplyRow.APPLY_COMMENT;
            }
        }

        private bool IsDataValidate()
        {
            this.errorProvider.SetError(this.txtSTART_TIME, string.Empty);
            this.errorProvider.SetError(this.txtEND_TIME, string.Empty);

            if (string.IsNullOrEmpty(this.txtSTART_TIME.Text))
            {
                this.txtSTART_TIME.Focus();

                this.errorProvider.SetError(this.txtSTART_TIME, "开始时间不能为空！");

                return false;
            }

            if (string.IsNullOrEmpty(this.txtEND_TIME.Text))
            {
                this.txtEND_TIME.Focus();

                this.errorProvider.SetError(this.txtEND_TIME, "结束时间不能为空！");

                return false;
            }

            DateTime startTime = this.GetDateTime(this.txtSTART_TIME.Text);
            DateTime endTime = this.GetDateTime(this.txtEND_TIME.Text);

            if (startTime > endTime)
            {
                this.txtSTART_TIME.Focus();

                this.errorProvider.SetError(this.txtSTART_TIME, "开始时间不能大于结束时间！");

                return false;
            }

            return true;
        }

        #endregion

        #region 事件

        private void EditMachine_Load(object sender, EventArgs e)
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

            if (this.HemodialysisApplyRow == null) //新增
            {
                this.HemodialysisApplyRow = this._hemodialysisApplyDataTable.NewMED_HEMO_APPLYRow();

                this.HemodialysisApplyRow.APPLY_ID = Guid.NewGuid().ToString();
                this.HemodialysisApplyRow.APPLY_WEEKDAY = this._weekDay.ToString();
                this.HemodialysisApplyRow.BANCI_ID = this._banciID.ToString();
                this.HemodialysisApplyRow.HEMODIALYSIS_ID = this._hemodialysisID;

                this._hemodialysisApplyDataTable.AddMED_HEMO_APPLYRow(this.HemodialysisApplyRow);
            }

            this.HemodialysisApplyRow.START_TIME = this.txtSTART_TIME.Text;
            this.HemodialysisApplyRow.END_TIME = this.txtEND_TIME.Text;
            this.HemodialysisApplyRow.APPLY_COMMENT = this.txtAPPLY_COMMENT.Text;

            this._patientScheduleService.SaveHemodialysisApplyInfo(this._hemodialysisApplyDataTable);

            AutoClosedMsgBox.ShowForm("保存成功。", "系统提示", 1500, MessageBoxIcon.Information);
            //     XtraMessageBox.Show("保存成功！");

            this.DialogResult = DialogResult.Yes;
        }

        #endregion
    }
}
