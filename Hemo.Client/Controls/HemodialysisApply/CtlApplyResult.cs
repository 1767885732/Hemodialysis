/*----------------------------------------------------------------
 * Copyright (C) 2005 医疗科技发展有限公司
 * 文件功能描述:预约排班
 * 创建标识:吕志强-2014年8月2日
 * ----------------------------------------------------------------*/

using DevExpress.XtraEditors;
using Hemo.IService.PatientSchedule;
using Hemo.Model;
using Hemo.Service;

namespace Hemo.Client.Controls.HemodialysisApply
{
    public partial class CtlApplyResult : XtraUserControl
    {
        #region 变量

        private IPatientSchedule _patientScheduleService = ServiceManager.Instance.PatientSchedule;

        #endregion

        #region 属性

        public PatientScheduleModel.MED_HEMO_APPLYRow HemodialysisApplyRow
        {
            private set;
            get;
        }

        #endregion

        #region 构造函数

        public CtlApplyResult()
        {
            this.InitializeComponent();
        }

        #endregion

        #region 方法

        public void SetInfo(PatientScheduleModel.MED_HEMO_APPLYRow hemodialysisApplyRow)
        {
            this.HemodialysisApplyRow = hemodialysisApplyRow;

            if (this.HemodialysisApplyRow == null)
                this.labTime.Text = this.labMsg.Text = this.labRemark.Text = string.Empty;
            else
            {
                this.labTime.Text = string.Format("{0}-{1}", this.HemodialysisApplyRow.START_TIME, this.HemodialysisApplyRow.END_TIME);
                this.labMsg.Text = "预约治疗";
                if (!this.HemodialysisApplyRow.IsAPPLY_COMMENTNull())
                    this.labRemark.Text = string.Format("备注：{0}", this.HemodialysisApplyRow.APPLY_COMMENT);
            }
        }

        #endregion
    }
}
