/*----------------------------------------------------------------
// Copyright (C) 2005 苏州XX公司有限公司
// 描述：病历查询窗体类
// 创建时间：2016-05-17
// 创建者：吕志强
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
using Hemo.IService;
using Hemo.Service;
using Hemo.Model;
using Hemo.Client.Print;
using DevExpress.XtraReports.UI;

namespace Hemo.Client.UI.Patient
{
    public partial class RecordQueyFrm :HemoBaseFrm
    {
        #region 类变量

        private IPatient objPatient = ServiceManager.Instance.PatientService;

        #endregion

        #region 构造函数

        public RecordQueyFrm()
        {
            InitializeComponent();
        }

        #endregion

        #region 事件

        private void gridView1_DoubleClick(object sender, EventArgs e)
        {
            xtraTabControl1.SelectedTabPageIndex = 1;
            this.lblReturn.Visible = true;
            this.memoEdit_action.Text = string.Format("           {0}", (this.gridView1.GetFocusedDataRow() as PatientScheduleModel.MED_PATIENTRECORDRow).ACTION);
            this.memoEdit_fill.Text = string.Format("              {0}", (this.gridView1.GetFocusedDataRow() as PatientScheduleModel.MED_PATIENTRECORDRow).PRESENTILLNESS);
            this.memoEdit_bottom.Text = string.Format("              {0}", (this.gridView1.GetFocusedDataRow() as PatientScheduleModel.MED_PATIENTRECORDRow).PASTILLNESS);
        }

        private void lblReturn_Click(object sender, EventArgs e)
        {
            xtraTabControl1.SelectedTabPageIndex = 0;
            this.lblReturn.Visible = false;

        }

        private void lblReturn_MouseMove(object sender, MouseEventArgs e)
        {
            this.lblReturn.ForeColor = Color.Green;
        }

        private void RecordQueyFrm_Load(object sender, EventArgs e)
        {
            this.beginDataEdit.EditValue = System.DateTime.Now.Date;
            this.endDataEdit.EditValue = System.DateTime.Now.Date.AddDays(1);
        }

        private void btn_Query_Click(object sender, EventArgs e)
        {
            this.gridControl1.DataSource = objPatient.QueryPatientRecordByParams(this.txtHemoID.Text.Trim(), Convert.ToDateTime(this.beginDataEdit.EditValue), Convert.ToDateTime(this.endDataEdit.EditValue));
        }

        private void lblReturn_MouseLeave(object sender, EventArgs e)
        {
            this.lblReturn.ForeColor = Color.Black;
        }

        private void btn_Print_Click(object sender, EventArgs e)
        {
            PatientRecordReport print = new PatientRecordReport();
            if (this.gridView1.GetFocusedDataRow() != null)
            {
                print.Name = (this.gridView1.GetFocusedDataRow() as PatientScheduleModel.MED_PATIENTRECORDRow).NAME;
                print.Age = (this.gridView1.GetFocusedDataRow() as PatientScheduleModel.MED_PATIENTRECORDRow).AGE.ToString();
                print.Sex = (this.gridView1.GetFocusedDataRow() as PatientScheduleModel.MED_PATIENTRECORDRow).SEX;
                print.HemoID = (this.gridView1.GetFocusedDataRow() as PatientScheduleModel.MED_PATIENTRECORDRow).HEMODIALYSIS_ID;
                print.PatientRecord = string.Format("主诉：{0}\r\n\r\n 现病史：{1} \r\n\r\n 既往史：{2}", (this.gridView1.GetFocusedDataRow() as DataRow)["ACTION"].ToString(), (this.gridView1.GetFocusedDataRow() as DataRow)["PRESENTILLNESS"].ToString(), (this.gridView1.GetFocusedDataRow() as DataRow)["PASTILLNESS"].ToString());
                ReportPrintTool pt = new ReportPrintTool(print);
                pt.ShowPreviewDialog();

            }
        }

        #endregion
    }
}