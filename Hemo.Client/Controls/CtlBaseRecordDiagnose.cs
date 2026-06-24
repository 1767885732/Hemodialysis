/*----------------------------------------------------------------
// Copyright (C) 2013 苏州XX公司
// 描述：病历基本资料诊断用户控件类
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
using Hemo.IService.Dict;
using Hemo.Service;
using Hemo.Client.Core;
using Hemo.Utilities;
using System.Linq;

namespace Hemo.Client.Controls
{
    public partial class CtlBaseRecordDiagnose : DevExpress.XtraEditors.XtraUserControl
    {
        #region 类变量

        private IStaffDict staffDictService = ServiceManager.Instance.StaffDictService;

        private string hemoId = string.Empty;

        private HemodialysisModel.MED_BASE_RECORD_DIAGNOSEDataTable recordDiagnose = null;

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
        /// 病历基本资料诊断
        /// </summary>
        public HemodialysisModel.MED_BASE_RECORD_DIAGNOSEDataTable RecordDiagnose
        {
            get { return recordDiagnose; }
            set { recordDiagnose = value; }
        }

        #endregion

        #region 构造函数

        public CtlBaseRecordDiagnose()
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
                if (row["IN_HOSPITAL_DATE"] != DBNull.Value)
                {
                    this.txtIN_HOSPITAL_DATE.EditValue = Utility.CDate(row["IN_HOSPITAL_DATE"].ToString());
                }
                else
                {
                    this.txtIN_HOSPITAL_DATE.EditValue = null;
                }

                if (row["LEAVE_HOSPITAL_DATE"] != DBNull.Value)
                {
                    this.txtLEAVE_HOSPITAL_DATE.EditValue = Utility.CDate(row["LEAVE_HOSPITAL_DATE"].ToString());
                }
                else
                {
                    this.txtLEAVE_HOSPITAL_DATE.EditValue = null;
                }

                this.lupCREATEBY.EditValue = row["CREATEBY"].ToString();
                this.txtDIAGNOSE.Text = row["DIAGNOSE"].ToString();
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
            this.lupCREATEBY.EditValue = HemoApplicationContext.Current.CurrentUser.EMP_NO;
        }

        /// <summary>
        /// 加载患者病历基本资料诊断
        /// </summary>
        public void LoadRecordDiagnose()
        {
            this.gcRecord.DataSource = recordDiagnose as DataTable;
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
            this.txtIN_HOSPITAL_DATE.Enabled = flag;
            this.txtLEAVE_HOSPITAL_DATE.Enabled = flag;
            this.lupCREATEBY.Enabled = flag;
            this.txtDIAGNOSE.Enabled = flag;
        }

        /// <summary>
        /// 新增
        /// </summary>
        public void New()
        {
            recordDiagnose = recordDiagnose ?? new HemodialysisModel.MED_BASE_RECORD_DIAGNOSEDataTable();
            var row = recordDiagnose.NewMED_BASE_RECORD_DIAGNOSERow();
            row.ID = Guid.NewGuid().ToString();
            row.HEMODIALYSIS_ID = hemoId;
            row.CREATEBY = HemoApplicationContext.Current.CurrentUser.EMP_NO;
            row.CREATE_DATE = DateTime.Now;
            row.IS_DELETE = "0";
            recordDiagnose.AddMED_BASE_RECORD_DIAGNOSERow(row);
            this.gvRecord.MoveLast();
            gvRecord_RowClick(this.gvRecord, null);
        }

        public HemodialysisModel.MED_BASE_RECORD_DIAGNOSEDataTable GetRecordDiagnoseDataTable()
        {
            recordDiagnose = recordDiagnose ?? new HemodialysisModel.MED_BASE_RECORD_DIAGNOSEDataTable();
            var row = this.gvRecord.GetFocusedDataRow();
            if (row != null)
            {
                row = recordDiagnose.FirstOrDefault(r => r.ID == row["ID"].ToString());
                row["CREATEBY"] = this.lupCREATEBY.EditValue.ToString();
                if (this.txtIN_HOSPITAL_DATE.EditValue != null)
                {
                    row["IN_HOSPITAL_DATE"] = Utility.CDate(this.txtIN_HOSPITAL_DATE.EditValue.ToString());
                }
                else
                {
                    row["IN_HOSPITAL_DATE"] = DBNull.Value;
                }
                if (this.txtLEAVE_HOSPITAL_DATE.EditValue != null)
                {
                    row["LEAVE_HOSPITAL_DATE"] = Utility.CDate(this.txtLEAVE_HOSPITAL_DATE.EditValue.ToString());
                }
                else
                {
                    row["LEAVE_HOSPITAL_DATE"] = DBNull.Value;
                }
                row["DIAGNOSE"] = this.txtDIAGNOSE.EditValue.ToString();
            }
            return recordDiagnose;
        }

        public DataRow GetFocusedDataRow()
        {
            return this.gvRecord.GetFocusedDataRow();
        }

        #endregion
    }
}
