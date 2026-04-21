/*----------------------------------------------------------------
 * Copyright (C) 2005 (苏州)医疗科技发展有限公司
 * 文件功能描述:新增方法
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
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Hemo.Model;
using Hemo.IService;
using Hemo.Service;
using Hemo.IService.Config;
using Hemo.Client.Print;
using DevExpress.XtraReports.UI;

namespace Hemo.Client.UI.Hemodialysis {
    public partial class BorrowMedicineFrm : HemoBaseFrm
    {
        #region 类变量
        private IPatient objPatient = ServiceManager.Instance.PatientService;
        #endregion
        #region 构造函数
        public BorrowMedicineFrm() {
            InitializeComponent();
        }
        #endregion
        #region 事件
        private void ShowSummary_Load(object sender, EventArgs e) {
            ////净化器类型
            txtStart.DateTime = System.DateTime.Now.AddDays(-30);
            txtEnd.DateTime = DateTime.Now;
            BindData();
        }
        private void BindData() {
            DataTable dt = this.objPatient.GetPatientBrowDrugList(txtStart.DateTime, txtEnd.DateTime.AddDays(1), txtPatientName.Text.Trim());
            if (radioGroup1.EditValue.ToString() == "1") {
                dt.DefaultView.RowFilter = " ISBACK='1' ";
            }
            if (radioGroup1.EditValue.ToString() == "2") {
                dt.DefaultView.RowFilter = " ISBACK='0' ";
            }
            gridBrowList.DataSource = dt;
        }
        private void btnQuery_Click(object sender, EventArgs e) {
            BindData();
        }

        private void radioGroup1_SelectedIndexChanged(object sender, EventArgs e) {
            BindData();
        }

        private void btnAdd_Click(object sender, EventArgs e) {
            var browFrm = new BrowAddFrm();
            browFrm.ShowDialog();
            BindData();
        }

        private void toolBack_Click(object sender, EventArgs e) {
            var row = gridBrowListView.GetFocusedDataRow();
            if (row == null) {
                XtraMessageBox.Show("请选择一行借出记录！");
                return;
            }
            if (row["ISBACK"].ToString() == "1") {
                XtraMessageBox.Show("该药品已经归还！");
                return;
            }
            var browFrm = new BrowBackFrm();
            browFrm.PatientName = string.Format("{0}", row["PATIENTNAME"]);
            browFrm.BackInfo = string.Format("{0} {1} {2}", row["MEDICINE_NAME"], row["MEDICINE_COUNT"], row["MEDICINE_UNIT"]);
            browFrm.BackID = string.Format("{0}", row["BORROW_ID"]);
            browFrm.ShowDialog();
            BindData();
        }

        private void btn_Cancle_Click(object sender, EventArgs e) {
            toolBack_Click(sender, e);
        }

        private void btnPrint_Click(object sender, EventArgs e) {
            var print = new BorrowPrintList();
            DataTable dataCopy = (gridBrowList.DataSource as DataTable).DefaultView.ToTable().Clone();
            foreach (DataRow row in (gridBrowList.DataSource as DataTable).DefaultView.ToTable().Rows) {
                DataRow rowCopy = dataCopy.NewRow();
                rowCopy.ItemArray = (object[])row.ItemArray.Clone();
                if (rowCopy["BORROW_USER"] != DBNull.Value) {
                    string[] userStrs = rowCopy["BORROW_USER"].ToString().Split(new char[] { '(' }, StringSplitOptions.RemoveEmptyEntries);
                    if (userStrs.Length == 2) {
                        rowCopy["BORROW_USER"] = userStrs[0];
                    }
                }
                if (rowCopy["BBORROW_USER"] != DBNull.Value) {
                    string[] userStrs = rowCopy["BBORROW_USER"].ToString().Split(new char[] { '(' }, StringSplitOptions.RemoveEmptyEntries);
                    if (userStrs.Length == 2) {
                        rowCopy["BBORROW_USER"] = userStrs[0];
                    }
                }
                dataCopy.Rows.Add(rowCopy);
            }
            print.BindData(dataCopy);
            ReportPrintTool pt = new ReportPrintTool(print);
            pt.ShowPreviewDialog();
        }

        private void btnClose_Click(object sender, EventArgs e) {
            this.Close();
        }
        #endregion
    }
       
}