/*----------------------------------------------------------------
 * Copyright (C) 2005 (苏州)医疗科技发展有限公司
 * 文件功能描述:模板列表类
 * 创建标识:贺建操-2016年4月28日
 * ----------------------------------------------------------------*/

using System;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Hemo.IService.PatientSchedule;
using Hemo.Service;
using Hemo.Utilities;
using Hemo.Model;
using System.Configuration;
using System.Data;
using System.Linq;
using Hemo.Client.Core;

namespace Hemo.Client.UI.PatientSchedule
{
    public partial class TemplateList :HemoBaseFrm
    {
        #region 变量

        private IPatientSchedule _patientScheduleService = ServiceManager.Instance.PatientSchedule;
        public string templateName = string.Empty;
        public PatientScheduleModel.MED_PATIENT_SCHEDULE_TEMPLATERow _patientScheduleTemplateRow;

        private static readonly string Blood_Hemo_Room = ConfigurationManager.AppSettings["Blood_Hemo_Room"].ToString();

        private static readonly string Head_Nurse = "10000107";

        #endregion

        #region 构造函数

        public TemplateList()
        {
            this.InitializeComponent();
        }

        #endregion

        #region 方法

        /// <summary>
        /// 初始化控件
        /// </summary>
        private void InitializeControls()
        {
            var dtTemplate = this._patientScheduleService.GetPatientScheduleAllTemplateList();
            var dtSubTemplate = dtTemplate.Where(t => 1 == 1);
            //if (!HemoApplicationContext.Current.CurrentUser.EMP_NO.Equals(Head_Nurse))
            //{
            //    dtSubTemplate = dtTemplate.Where(t => t.BLOOD_HEMO_ROOM.Equals(Blood_Hemo_Room));
            //}
            var dtResult = dtTemplate.Clone() as PatientScheduleModel.MED_PATIENT_SCHEDULE_TEMPLATEDataTable;
            if (dtSubTemplate.Any())
            {
                dtSubTemplate.CopyToDataTable(dtResult, LoadOption.OverwriteChanges);
            }
            Utility.BindLookUpEdit(this.cbxTemplate, "PATIENT_SCHEDULE_TEMPLATE_ID", "PATIENT_SCHEDULE_TEMPLATE_NAME", dtResult, "PATIENT_SCHEDULE_TEMPLATE_NAME", "模板");
        }

        /// <summary>
        /// 校验
        /// </summary>
        /// <returns></returns>
        private bool IsDataValidate()
        {
            this.errorProvider.SetError(this.cbxTemplate, string.Empty);

            if (string.IsNullOrEmpty(this.cbxTemplate.Text))
            {
                this.cbxTemplate.Focus();

                this.errorProvider.SetError(this.cbxTemplate, "请选择模板！");

                return false;
            }

            return true;
        }

        #endregion

        #region 事件

        /// <summary>
        /// 关闭
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void simpleButton3_Click(object sender, EventArgs e) {
            this.Close();
        }

        /// <summary>
        /// 加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TemplateList_Load(object sender, EventArgs e)
        {
            this.InitializeControls();
        }

        /// <summary>
        /// 选择
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!this.IsDataValidate())
                return;

            this.Tag = this.cbxTemplate.EditValue;
            this._patientScheduleTemplateRow = ((Hemo.Model.PatientScheduleModel.MED_PATIENT_SCHEDULE_TEMPLATERow)(((System.Data.DataRowView)(cbxTemplate.GetSelectedDataRow())).Row));
            this.templateName = this.cbxTemplate.Text.ToString();

            this.DialogResult = DialogResult.Yes;
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_Delete_Click(object sender, EventArgs e)
        {
            string templateId = this.cbxTemplate.EditValue.ToString();
            if (DialogResult.Yes == MessageBox.Show(string.Format("你是否确定要删除名称为{0}的模板？", this.cbxTemplate.Text), "是否删除模板", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
            {

                if (_patientScheduleService.DeleteScheduleTemplateByTemplateId(templateId) > 0)
                {
                    MessageBox.Show("删除成功！");
                }
            }
        }

        #endregion
    }
}

