/*----------------------------------------------------------------
 * Copyright (C) 2005 (苏州)医疗科技发展有限公司
 * 文件功能描述:排班模板名称维护类
 * 创建标识:贺建操-2017年3月12日
 * ----------------------------------------------------------------*/

using System;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Hemo.IService.PatientSchedule;
using Hemo.Model;
using System.Linq;
using Hemo.Service;
using System.Data;

namespace Hemo.Client.UI.PatientSchedule
{
    public partial class EditTemplate :HemoBaseFrm
    {
        #region 变量

        private PatientScheduleModel.MED_PATIENT_SCHEDULE_TEMPLATEDataTable _patientScheduleTemplateDataTable;
        private PatientScheduleModel.MED_PATIENT_SCHEDULE_TEMPLATEDataTable _patientScheduleTemplates;

        public PatientScheduleModel.MED_PATIENT_SCHEDULE_TEMPLATERow _patientScheduleTemplateRow;
        private IPatientSchedule _patientScheduleService = ServiceManager.Instance.PatientSchedule;

        public string currentTemplateID { get; set; }
        public string currentTemplateName { get; set; }

        public string Blood_Hemo_Room { get; set; }

        public bool IsHeadNurse { get; set; }

        #endregion

        #region 构造函数

        public EditTemplate()
        {
            this.InitializeComponent();
        }
        private string PATIENT_SCHEDULE_TEMPLATE_ID = string.Empty;
        #endregion

        #region 方法

        /// <summary>
        /// 初始化数据
        /// </summary>
        private void InitializeControls()
        {
            this._patientScheduleTemplateDataTable = new PatientScheduleModel.MED_PATIENT_SCHEDULE_TEMPLATEDataTable();
            if (this._patientScheduleTemplateRow != null)
                this.txtPATIENT_SCHEDULE_TEMPLATE_NAME.Enabled = false;

            var dtTemplate = this._patientScheduleService.GetPatientScheduleAllTemplateList();
            var dtSubTemplate = dtTemplate.Where(t => 1 == 1);
            //if(!IsHeadNurse)
            //{
            //    dtSubTemplate = dtTemplate.Where(t => t.BLOOD_HEMO_ROOM.Equals(Blood_Hemo_Room));
            //}
            if (dtSubTemplate.Any())
            {
                _patientScheduleTemplates = dtTemplate.Clone() as PatientScheduleModel.MED_PATIENT_SCHEDULE_TEMPLATEDataTable;
                dtSubTemplate.CopyToDataTable(_patientScheduleTemplates, LoadOption.OverwriteChanges);
            }
            else
            {
                _patientScheduleTemplates = new PatientScheduleModel.MED_PATIENT_SCHEDULE_TEMPLATEDataTable();
            }
        }

        /// <summary>
        /// 校验数据是否有效
        /// </summary>
        /// <returns></returns>
        private bool IsDataValidate()
        {
            this.errorProvider.ClearErrors();
            this.errorProvider.SetError(this.txtPATIENT_SCHEDULE_TEMPLATE_NAME, string.Empty);

            if (string.IsNullOrEmpty(this.txtPATIENT_SCHEDULE_TEMPLATE_NAME.Text))
            {
                this.txtPATIENT_SCHEDULE_TEMPLATE_NAME.Focus();

                this.errorProvider.SetError(this.txtPATIENT_SCHEDULE_TEMPLATE_NAME, "模板名称不能为空！");

                return false;
            }
            if (this._patientScheduleTemplateRow != null && this.txtPATIENT_SCHEDULE_TEMPLATE_NAME.Enabled == true)
            {
                _patientScheduleTemplateRow.PATIENT_SCHEDULE_TEMPLATE_NAME = this.txtPATIENT_SCHEDULE_TEMPLATE_NAME.Text.ToString();
            }
            if (currentTemplateName != this.txtPATIENT_SCHEDULE_TEMPLATE_NAME.Text.ToString())
            {
                var row = _patientScheduleTemplates.FirstOrDefault(i => i.PATIENT_SCHEDULE_TEMPLATE_NAME.Trim() == this.txtPATIENT_SCHEDULE_TEMPLATE_NAME.Text.Trim());
                if (row != null)
                {
                    this.txtPATIENT_SCHEDULE_TEMPLATE_NAME.Focus();

                    this.errorProvider.SetError(this.txtPATIENT_SCHEDULE_TEMPLATE_NAME, "此名称已存在，不可以重复.");

                    return false;
                }
            }
            return true;
        }

        #endregion

        #region 事件
        /// <summary>
        /// 取消
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void simpleButton2_Click(object sender, EventArgs e) {
            this.DialogResult = DialogResult.Cancel;
            //this.Close();
        }

        /// <summary>
        /// 加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EditTemplate_Load(object sender, EventArgs e)
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
            if (_patientScheduleTemplateRow == null)
            {
                this._patientScheduleTemplateRow = this._patientScheduleTemplateDataTable.NewMED_PATIENT_SCHEDULE_TEMPLATERow();

                this._patientScheduleTemplateRow.PATIENT_SCHEDULE_TEMPLATE_ID = Guid.NewGuid().ToString();
                this._patientScheduleTemplateRow.PATIENT_SCHEDULE_TEMPLATE_NAME = this.txtPATIENT_SCHEDULE_TEMPLATE_NAME.Text;
                this._patientScheduleTemplateRow.PATIENT_SCHEDULE_TEMPLATE_DATE = DateTime.Now;
                this._patientScheduleTemplateRow.BLOOD_HEMO_ROOM = Blood_Hemo_Room;
                this._patientScheduleTemplateDataTable.AddMED_PATIENT_SCHEDULE_TEMPLATERow(this._patientScheduleTemplateRow);

            }
            else
            {
                this._patientScheduleTemplateDataTable.ImportRow(this._patientScheduleTemplateRow);
            }


            this._patientScheduleService.SavePatientScheduleTemplateInfo(this._patientScheduleTemplateDataTable);

            this.Tag = this._patientScheduleTemplateRow.PATIENT_SCHEDULE_TEMPLATE_ID;

            this.DialogResult = DialogResult.Yes;
        }

        /// <summary>
        /// 编辑
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_edit_Click(object sender, EventArgs e)
        {
            this.txtPATIENT_SCHEDULE_TEMPLATE_NAME.Enabled = true;
        }

        #endregion
    }
}

