/*----------------------------------------------------------------
// Copyright (C) 2013 苏州XX公司有限公司
// 描述：健康宣教 
// 创建时间：2016-10-19
// 创建者：刘配齐
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
using Hemo.Model;
using Hemo.Service;
using Hemo.Utilities;
using DevExpress.XtraEditors.Controls;
using Hemo.IService;
using Hemo.IService.Dict;
using Hemo.Client.Core;
using Hemo.IService.Config;
using Hemo.IService.PatientSchedule;
using Hemo.Client.UI.Machine;

namespace Hemo.Client.UI.Hemodialysis {
    public partial class EditHealthEducationList : ViewBase {

        /// <summary>
        /// 透析编码
        /// </summary>
        private string _hemoId = null;
        private IHemodialysis objHemodialysisService = ServiceManager.Instance.HemodialysisService;

        public string HEMODIALYSIS_ID {
            get { return _hemoId; }
            set { _hemoId = value; }
        }

        #region 事件
        public EditHealthEducationList() {
            InitializeComponent();
            //  this.ctlUserLongInfo1.patientInfoCheck1.patientPickEvent += new EventHandler(patientInfoCheck1_patientPickEvent);
            loadHealthInfo();
        }

        void patientInfoCheck1_patientPickEvent(object sender, EventArgs e) {
            //if (this.ctlUserLongInfo1.patientInfoCheck1._patientDataTable.Rows.Count > 0)
            //{
            //    this._hemoId = this.ctlUserLongInfo1.patientInfoCheck1._patientDataTable[0].HEMODIALYSIS_ID;
            //}
            loadHealthInfo();
            LoadData(HEMODIALYSIS_ID);
        }

        private void loadHealthInfo() {
            //   ctlUserLongInfo1.HEMODIALYSIS_ID = HEMODIALYSIS_ID;
        }

        public void LoadData(string pHemoID) {
            if (string.IsNullOrEmpty(pHemoID))
                return;
            ctlUserLongInfo1.HEMODIALYSIS_ID = pHemoID;
            ctlUserLongInfo1.LoadPatientInfo();
            DataTable oldData = objHemodialysisService.GetHealthEducationListByHemoID(pHemoID);
            this.gcList.DataSource = oldData;
        }

        private void btnAdd_Click(object sender, EventArgs e) {
            EditHealthEducation frm = new EditHealthEducation();
            frm.HEMODIALYSIS_ID = _hemoId;
            frm.ID = string.Empty;
            frm.ShowDialog();
            if (frm.DialogResult == DialogResult.OK) {
                LoadData(HEMODIALYSIS_ID);
            }
        }

        private void EditHealthEducationList_Load(object sender, EventArgs e) {
            LoadData(HEMODIALYSIS_ID);
        }

        private void btnClose_Click(object sender, EventArgs e) {
     //       this.Close();
        }

        private void btnDelete_Click(object sender, EventArgs e) {
            var row = this.gvList.GetFocusedDataRow();
            if (row != null) {
                if (XtraMessageBox.Show("确认要删除选中的记录吗？", "健康宣教列表", MessageBoxButtons.OKCancel) == DialogResult.OK) {
                    try {
                        if (objHemodialysisService.DeleteHealthEducationByHemoIdAndId(row["HEMODIALYSIS_ID"].ToString(),row["ID"].ToString()) > 0)
                        {
                            XtraMessageBox.Show("删除成功", "健康宣教列表");
                        }
                    }
                    catch (Exception ex)
                    {
                        XtraMessageBox.Show("错误",ex.ToString());
                    }


              
                    LoadData(HEMODIALYSIS_ID);
                }
            }
        }

        private void gcList_DoubleClick(object sender, EventArgs e) {
            var row = this.gvList.GetFocusedDataRow();
            if (row != null) {
                EditHealthEducation frm = new EditHealthEducation();
                frm.HEMODIALYSIS_ID = _hemoId;
                frm.ID = row["ID"].ToString();
                frm.ShowDialog();
                if (frm.DialogResult == DialogResult.OK) {
                    LoadData(HEMODIALYSIS_ID);
                }
            }
        }
        #endregion


    }
}
