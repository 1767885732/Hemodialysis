using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Hemo.IService;
using Hemo.Service;
using Hemo.IService.Config;
using Hemo.Model;
using DevExpress.XtraBars.Docking2010.Customization;

namespace Hemo.Client.UI.PatientFixUI
{
    public partial class PatientExtendUI : UserControl
    {
        private IPatient patientService = ServiceManager.Instance.PatientService;
        private IHemodialysis hemoService = ServiceManager.Instance.HemodialysisService;

        public PatientModel.MED_PATIENTS_EXTENDDataTable patientsDt = null;

        public event EventHandler PatientSetInfoEvent;

        public PatientExtendUI()
        {
            InitializeComponent();
        }
        /// <summary>
        /// patientType:0 在透   1     新入   2  转出
        /// </summary>
        /// <param name="filterTxt"></param>
        /// <param name="patientType"></param>
        public void Query(string filterTxt, string patientType, DateTime dateYear)
        {
            patientsDt = new PatientModel.MED_PATIENTS_EXTENDDataTable();
            patientsDt = patientService.GetPatientExtendByParm(filterTxt);

            foreach (var row in patientsDt)
            {
                row.SPECIFIC_TIME = Utilities.Utility.CDate(row.SPECIFIC_TIME).ToString("yyyy-MM-dd");
                var vascularName = string.Empty;
                var vascularTime = string.Empty;
                var acceDt = hemoService.GetPatientVasular_AccessDt(row.HEMODIALYSIS_ID);
                foreach (var dr in acceDt)
                {
                    vascularName += dr.PATIENT_ID + ";";
                    vascularTime += dr.CREATE_DATE.ToString("yyyy-MM-dd") + ";";
                }
                row.VASCULARNAME = vascularName;
                row.VASCULARTIME = vascularTime;

                //var extDt = patientService.GetPatientExtendByHemoId(row.HEMODIALYSIS_ID);
                //foreach (var dr in extDt)
                //{
                //    row.DIRECTIONNAME += dr.DIRECTIONNAME + "/" + dr.CREATEDATE.ToString("yyyy-MM-dd") + ";";
                //}
            }
         


            var patientsDtSourse = new PatientModel.MED_PATIENTS_EXTENDDataTable();
            if (patientType == "0")
                patientsDt.Where(i => i.IS_NEW == "0").CopyToDataTable(patientsDtSourse, LoadOption.PreserveChanges);
            else if (patientType == "1")
                patientsDt.Where(i => i.IS_NEW == "0" && i.LEAVE_HOSPITAL_TIME.Year == dateYear.Year).CopyToDataTable(patientsDtSourse, LoadOption.PreserveChanges);
            else  if (patientType == "2")
                patientsDt.Where(i => i.IS_NEW != "0" && i.LEAVE_HOSPITAL_TIME.Year == dateYear.Year).CopyToDataTable(patientsDtSourse, LoadOption.PreserveChanges);
            else
                patientsDt.CopyToDataTable(patientsDtSourse, LoadOption.PreserveChanges);



            this.gridControl1.DataSource = patientsDtSourse;
        }

        /// <summary>
        /// 导出Excel
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void ExportToExcel()
        {
            SaveFileDialog fileDialog = new SaveFileDialog();
            fileDialog.Title = "导出Excel";
            fileDialog.Filter = "Excel文件(*.xls)|*.xls";
            fileDialog.FileName = "患者登记信息表";
            fileDialog.RestoreDirectory = true;
            DialogResult dialogResult = fileDialog.ShowDialog(this);
            if (dialogResult == DialogResult.OK)
            {
                DevExpress.XtraPrinting.XlsExportOptions options = new DevExpress.XtraPrinting.XlsExportOptions();
                options.TextExportMode = DevExpress.XtraPrinting.TextExportMode.Text;
                this.gridControl1.ExportToXls(fileDialog.FileName, options);
                DevExpress.XtraEditors.XtraMessageBox.Show("导出成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

        }



        private void gridControl1_Click(object sender, EventArgs e)
        {

        }

        private void gridView4_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void gridView4_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Clicks == 2)
            {
                var dr = gridView4.GetFocusedDataRow() as PatientModel.MED_PATIENTS_EXTENDRow;

                var rowDt = patientService.GetPatientListByHemoIds(dr.HEMODIALYSIS_ID);
                if (rowDt != null && rowDt.Rows.Count > 0)
                {
                    var patientInfoUi = new PatientInfoUI();
                    patientInfoUi.Current = rowDt[0];
                    patientInfoUi.InitalizeData();
                    FlyoutDialog.Show(this.FindForm(), patientInfoUi);
                    if (PatientSetInfoEvent != null)
                        PatientSetInfoEvent(null, null);
                }
            }
        }
    }
}
