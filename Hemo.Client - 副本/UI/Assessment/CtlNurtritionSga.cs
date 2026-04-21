/*----------------------------------------------------------------
// Copyright (C) 2005 苏州XX公司有限公司
// 描述：营养评估用户控件
// 创建时间：2015-03-20
// 创建者：刘超
//  
// 修改时间：
// 修改人：
// 修改描述：
----------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Hemo.Model;
using Hemo.Client.Core;
using Hemo.Utilities;
using Hemo.IService.Config;
using Hemo.Service;
using Hemo.IService;
using DevExpress.XtraEditors;
using Hemo.IService.Dict;

namespace Hemo.Client.UI.Assessment
{
    public partial class CtlNurtritionSga : DevExpress.XtraEditors.XtraUserControl
    {
        #region 变量属性
        private IConfig _configService = ServiceManager.Instance.ConfigService;

        public string AssessmentID { get; set; }
        private IHemodialysis _hemodialysisService = ServiceManager.Instance.HemodialysisService;
        private IPatient _patientService = ServiceManager.Instance.PatientService;
        private IStaffDict _staffDictService = ServiceManager.Instance.StaffDictService;


        private HemoModel.MED_ASSESSMENTMASTERRow _medAssessRow;

        public HemoModel.MED_ASSESSMENTMASTERRow MedAssessRow
        {
            get { return _medAssessRow; }
            set { _medAssessRow = value; }
        }

        private HemoModel.MED_ASSESSMENTMASTERDataTable _medAssessTable;

        public HemoModel.MED_ASSESSMENTMASTERDataTable MedAssessTable
        {
            get { return _medAssessTable; }
            set { _medAssessTable = value; }
        }

        private string currentHemoId = string.Empty;
        public string CurrentHemoId
        {
            get { return currentHemoId; }
            set { currentHemoId = value; }
        }
        private bool isAdd;

        #endregion

        #region 构造函数

        public CtlNurtritionSga()
        {
            InitializeComponent();

        }

        #endregion

        #region 方法
        /// <summary>
        /// 根据数据进行和控件名称进行数据绑定
        /// </summary>
        private void BindData()
        {
            foreach (var ctl in this.Controls)
            {
                if (ctl is BaseEdit)
                {
                    (ctl as BaseEdit).BindingAssessDataRow(this._medAssessRow);
                }
                else if (ctl is DevExpress.XtraEditors.GroupControl)
                {
                    BindGroupControlData(ctl as DevExpress.XtraEditors.GroupControl);
                }

            }
        }
        private void BindGroupControlData(DevExpress.XtraEditors.GroupControl gPanel)
        {
            foreach (var tctl in gPanel.Controls)
            {
                if (tctl is BaseEdit)
                {
                    (tctl as BaseEdit).BindingAssessDataRow(this._medAssessRow);
                }
                else if (tctl is CheckedListBoxControl)
                {
                    (tctl as CheckedListBoxControl).BindingCheckedDataRow(this._medAssessRow);
                }
            }
        }
        /// <summary>
        /// 进行控件初始化并且数据绑定
        /// </summary>
        public void InzationControl()
        {
            InizationControl();
            BindData();
        }
        /// <summary>
        /// 控件进行初始化
        /// </summary>
        private void InizationControl()
        {
            var dtStaffSict = _staffDictService.GetStaffDictList();

            this.control35.Properties.DataSource = Utility.GetSubTable(dtStaffSict, "ZYNAME='医生'");
            this.control36.Properties.DataSource = Utility.GetSubTable(dtStaffSict, "ZYNAME='护士'");
            if (this._medAssessRow == null)
            {
                this.isAdd = true;
                this._medAssessRow = _medAssessTable.NewMED_ASSESSMENTMASTERRow();
                this._medAssessRow.control34 = DateTime.Now.Date.ToString("yyyy-MM-dd");
                this._medAssessRow.control36 = HemoApplicationContext.Current.CurrentUser.EMP_NO;

            }
            else
                this.isAdd = false;


        }
        /// <summary>
        /// 保存数据
        /// </summary>
        /// <returns></returns>
        public int SaveNutritionSga()
        {
            if (this.isAdd)
            {
                this._medAssessRow.ASSESSMENT_ID = Guid.NewGuid().ToString();
                this._medAssessRow.ASSESSMENT_DATE = System.DateTime.Now.Date;
                this._medAssessRow.ASSESSMENT_TYPE = "患者营养性评估";
                this._medAssessRow.ASSESSMENT_NOTE = HemoApplicationContext.Current.CurrentUser.USER_NAME + this._medAssessRow.ASSESSMENT_ID;
                this._medAssessRow.HEMODIALYSIS_ID = currentHemoId;
                this._medAssessRow.CREATE_USER = HemoApplicationContext.Current.CurrentUser.USER_ID;
                this._medAssessTable.AddMED_ASSESSMENTMASTERRow(this._medAssessRow);
            }

            return this._hemodialysisService.SaveAssessmentByDate(this._medAssessTable);
        }

        #endregion

        #region 事件
        
        private void txtWeight2_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (this.control0.Text)
            {
                case "< 5%":
                    control1.Text = "A";
                    break;
                case "5~10%":
                    control1.Text = "B";
                    break;
                case ">10%":
                    control1.Text = "C";
                    break;
            }
        }

        private void control10_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (this.control10.Text)
            {
                case "无变化":
                    control11.Text = "A";
                    break;
                case "稳定":
                    control11.Text = "B";
                    break;
                case "减少/降低":
                    control11.Text = "C";
                    break;
            }
        }

        private void control12_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (this.control12.Text)
            {
                case "不变":
                    control13.Text = "A";
                    break;
                case "增加":
                    control13.Text = "B";
                    break;
                case "减少":
                    control13.Text = "C";
                    break;
            }
        }

        private void control14_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (this.control14.Text)
            {
                case "<2周 ，变化少或无变化":
                    control15.Text = "A";
                    break;
                case ">2 周，中度低于理想摄食量":
                    control15.Text = "B";
                    break;
                case ">2 周，不能进食，饥饿":
                    control15.Text = "C";
                    break;
            }
        }

        private void control20_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (this.control20.Text)
            {
                case "有所改善":
                    control21.Text = "A";
                    break;
                case "无变化":
                    control21.Text = "B";
                    break;
                case "恶化":
                    control21.Text = "C";
                    break;
            }
        }

        private void control22_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (this.control22.Text)
            {
                case "无减少":
                    control23.Text = "A";
                    break;
                case "中度减少":
                    control23.Text = "B";
                    break;
                case "重度减少":
                    control23.Text = "C";
                    break;
            }
        }

        private void control24_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (this.control24.Text)
            {
                case "改变少或无变化":
                    control25.Text = "A";
                    break;
                case "轻或中度改变":
                    control25.Text = "B";
                    break;
                case "重度改变":
                    control25.Text = "C";
                    break;
            }
        }

        private void control26_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (this.control26.Text)
            {
                case "正常或轻微":
                    control27.Text = "A";
                    break;
                case "轻 - 中":
                    control27.Text = "B";
                    break;
                case "重":
                    control27.Text = "C";
                    break;
            }
        }

        private void control28_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (this.control28.Text)
            {
                case "正常或轻微":
                    control29.Text = "A";
                    break;
                case "轻 - 中":
                    control29.Text = "B";
                    break;
                case "重":
                    control29.Text = "C";
                    break;
            }
        }

        private void control30_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (this.control30.Text)
            {
                case "营养良好":
                    control31.Text = "A";
                    break;
                case "中度营养不良":
                    control31.Text = "B";
                    break;
                case "重度营养不良":
                    control31.Text = "C";
                    break;
            }
        }

        #endregion
    }
}

