/*----------------------------------------------------------------
// Copyright (C) 2013 苏州XX公司
// 描述：病历基本资料事件用户控件类
// 创建时间：2015-07-31
// 创建者：吕志强
//  
// 修改时间：
// 修改人：
// 修改描述：
//
----------------------------------------------------------------*/
using System;
using System.Data;
using Hemo.Model;
using Hemo.Utilities;
using Hemo.IService.Dict;
using Hemo.Service;
using Hemo.Client.Core;
using System.Linq;

namespace Hemo.Client.Controls
{
    public partial class CtlBaseRecordEvent : DevExpress.XtraEditors.XtraUserControl
    {
        #region 类变量

        private IStaffDict staffDictService = ServiceManager.Instance.StaffDictService;

        private string hemoId = string.Empty;

        private HemodialysisModel.MED_BASE_RECORD_EVENTDataTable recordEvent = null;

        #endregion

        #region 属性

        /// <summary>
        /// 透析编号
        /// </summary>
        public string HemoId
        {
            get { return hemoId; }
            set { hemoId = value; }
        }

        /// <summary>
        /// 病历基本资料事件
        /// </summary>
        public HemodialysisModel.MED_BASE_RECORD_EVENTDataTable RecordEvent
        {
            get { return recordEvent; }
            set { recordEvent = value; }
        }

        #endregion

        #region 构造函数

        public CtlBaseRecordEvent()
        {
            InitializeComponent();
        }

        #endregion

        #region 事件

        /// <summary>
        /// 点击列表行
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gvRecord_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            var row = this.gvRecord.GetFocusedDataRow();
            if (row != null)
            {
                this.txtCREATE_DATE.EditValue = Utility.CDate(row["CREATE_DATE"].ToString());
                this.lupDIALYSIS_STAGE.EditValue = row["DIALYSIS_STAGE"].ToString();
                this.lupCREATEBY.EditValue = row["CREATEBY"].ToString();
                this.txtCOMPLICATION.Text = row["COMPLICATION"].ToString();
                this.txtCHRONIC_EVENT.Text = row["CHRONIC_EVENT"].ToString();
                SetControlEnabled(true);
            }
        }

        #endregion

        #region 方法

        /// <summary>
        /// 加载
        /// </summary>
        public void LoadControl()
        {
            BindDropdownItems();
            this.txtCREATE_DATE.EditValue = null;
            this.lupCREATEBY.EditValue = HemoApplicationContext.Current.CurrentUser.EMP_NO;
        }

        /// <summary>
        /// 加载患者病历基本资料事件
        /// </summary>
        public void LoadRecordEvent()
        {
            this.gcRecord.DataSource = recordEvent as DataTable;
        }

        /// <summary>
        /// 绑定下拉项
        /// </summary>
        private void BindDropdownItems()
        {
            DataTable dtStaffSict = staffDictService.GetStaffDictList();
            if (dtStaffSict != null && dtStaffSict.Rows.Count > 0)
            {
                DataTable dtDoctor = Utility.GetSubTable(dtStaffSict, "ZYNAME='医生'", "name");
                if (dtDoctor != null && dtDoctor.Rows.Count > 0)
                {
                    BaseControlInfo.BindLookUpEdit(this.lupCREATEBY, "EMP_NO", "NAME", dtDoctor, "NAME", "记录医生");
                }
            }
        }

        /// <summary>
        /// 控件是否启用
        /// </summary>
        /// <param name="flag"></param>
        private void SetControlEnabled(bool flag)
        {
            this.txtCREATE_DATE.Enabled = flag;
            this.lupDIALYSIS_STAGE.Enabled = flag;
            this.lupCREATEBY.Enabled = flag;
            this.txtCOMPLICATION.Enabled = flag;
            this.txtCHRONIC_EVENT.Enabled = flag;
        }

        /// <summary>
        /// 新增
        /// </summary>
        public void New()
        {
            recordEvent = recordEvent ?? new HemodialysisModel.MED_BASE_RECORD_EVENTDataTable();
            var row = recordEvent.NewMED_BASE_RECORD_EVENTRow();
            row.ID = Guid.NewGuid().ToString();
            row.HEMODIALYSIS_ID = hemoId;
            row.CREATEBY = HemoApplicationContext.Current.CurrentUser.EMP_NO;
            row.CREATE_DATE = DateTime.Now;
            row.IS_DELETE = "0";
            recordEvent.AddMED_BASE_RECORD_EVENTRow(row);
            this.gvRecord.MoveLast();
            gvRecord_RowClick(this.gvRecord, null);
        }

        public HemodialysisModel.MED_BASE_RECORD_EVENTDataTable GetRecordEventDataTable()
        {
            recordEvent = recordEvent ?? new HemodialysisModel.MED_BASE_RECORD_EVENTDataTable();
            var row = this.gvRecord.GetFocusedDataRow();
            if (row != null)
            {
                row = recordEvent.FirstOrDefault(r => r.ID == row["ID"].ToString());
                row["CREATE_DATE"] = this.txtCREATE_DATE.EditValue != null ? Utility.CDate(this.txtCREATE_DATE.EditValue.ToString()) : row["CREATE_DATE"];
                row["CREATEBY"] = this.lupCREATEBY.EditValue.ToString();
                row["DIALYSIS_STAGE"] = this.lupDIALYSIS_STAGE.EditValue.ToString();
                row["COMPLICATION"] = this.txtCOMPLICATION.Text.ToString();
                row["CHRONIC_EVENT"] = this.txtCHRONIC_EVENT.Text.ToString();
            }
            return recordEvent;
        }

        public DataRow GetFocusedDataRow()
        {
            return this.gvRecord.GetFocusedDataRow();
        }

        #endregion
    }
}
