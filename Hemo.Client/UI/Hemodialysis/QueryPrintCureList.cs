/*----------------------------------------------------------------
// Copyright (C) 2013 苏州XX公司有限公司
// 描述：根据排班与病人信息查询透析单
// 创建时间：2013-07-25
// 创建者：刘超
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
using Hemo.Utilities;
using Hemo.IService.Config;
using Hemo.Service;
using Hemo.IService;
using Hemo.Model;
using Hemo.Client.Controls;

namespace Hemo.Client.UI.Hemodialysis {
    public partial class QueryPrintCureList :HemoBaseFrm
    {
        #region 变量
        private IHemodialysis objHemodialysisService = ServiceManager.Instance.HemodialysisService;
        private IPatient objPatient = ServiceManager.Instance.PatientService;
        #endregion
        /// <summary>
        /// 构造函数
        /// </summary>
        public QueryPrintCureList() {
            InitializeComponent();
        }

        #region 事件
        private void loadBanCiList() {
            DataTable dtBANCI = new DataTable();
            dtBANCI.Columns.Add(new DataColumn("ITEM_ID"));
            dtBANCI.Columns.Add(new DataColumn("ITEM_NAME"));
        }

        private void QueryPrintCureList_Load(object sender, EventArgs e) {
            loadBanCiList();
        }

        private void btnQuery_Click(object sender, EventArgs e) {
            string pHemoID = string.Empty;
            string pCureCreateDate = string.Empty;
            string pBanCi = string.Empty;
            string pName = string.Empty;
            //if (txtCureCreateDate.EditValue != null) {
            //    pCureCreateDate = txtCureCreateDate.EditValue.ToString();
            //}
            if (txtHemoID.Text.Length > 0)
            {
                pHemoID = txtHemoID.Text;
            }
            if (txtName.Text.Length > 0)
            {
                pName = txtName.Text;
            }
            //if (txtBanCi.EditValue != null) {
            //    pBanCi = txtBanCi.EditValue.ToString();
            //}

            // DataTable dt = objHemodialysisService.GetPrintCureList(pHemoID, pCureCreateDate, pBanCi, pName);
            DataTable dt = objPatient.GetPatientListByParams(pName, pHemoID);
            if (dt != null && dt.Rows.Count > 0)
            {
                grdCureList.DataSource = dt;
            }
            else
            {
                grdCureList.DataSource = null;
            }
        }

        private void btnPrint_Click(object sender, EventArgs e) {
            DataRow dr = this.gridView1.GetFocusedDataRow();
            if (dr != null)
            {
                PatientModel.MED_PATIENTSRow PatientDocRow;
                PatientDocRow = gridView1.GetFocusedDataRow() as PatientModel.MED_PATIENTSRow;
                PatientKnowBooks FRM = new PatientKnowBooks();
                FRM.BindDocTree(PatientDocRow);
                FRM.Show();
            }
            else
            {
                XtraMessageBox.Show("请先选择一个患者，然后查询透析治疗单信息。", "透析治疗单");
            }
        }

        private void gridView1_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e) {
            try {
                DataRow dr = gridView1.GetFocusedDataRow();
                if (dr != null) {
                    btnPrint.Enabled = true;
                }
                else {
                    btnPrint.Enabled = false;
                }
            }
            catch (Exception ex) {
                MessageBox.Show(ex.Message, "血透记录单");
            }
        }
        #endregion
    }
}