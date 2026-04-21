using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Hemo.Model;
using Hemo.IService.Config;
using Hemo.Service;
using Hemo.Client.UI.Machine;

namespace Hemo.Client.Modules.NurseModel
{
    public partial class AllPtientFileter : ViewBase
    {
        private IHemodialysis hemoService = ServiceManager.Instance.HemodialysisService;
        public AllPtientFileter()
        {
            InitializeComponent();
            this.dateEditYear.DateTime = DateTime.Now;
            this.patientExtendUI1.PatientSetInfoEvent += new EventHandler(patientExtendUI1_PatientSetInfoEvent);
        }

        void patientExtendUI1_PatientSetInfoEvent(object sender, EventArgs e)
        {
            this.patientExtendUI1.Query(this.txtNAME.Text, radioGroup1.SelectedIndex.ToString(), this.dateEditYear.DateTime);
            SetInfo(this.patientExtendUI1.patientsDt);
        }

        private void btnQuery_Click(object sender, EventArgs e)
        {
            this.patientExtendUI1.Query(this.txtNAME.Text, radioGroup1.SelectedIndex.ToString(), this.dateEditYear.DateTime);
            SetInfo(this.patientExtendUI1.patientsDt);

        }
        public void InitalizeData()
        {
            this.patientExtendUI1.Query(this.txtNAME.Text, radioGroup1.SelectedIndex.ToString(), this.dateEditYear.DateTime);
            SetInfo(this.patientExtendUI1.patientsDt);
        }

        private void SetInfo(PatientModel.MED_PATIENTS_EXTENDDataTable patientsDt)
        {
            int allcount = 0;
            int newCount = 0;
            int ountCount = 0;
            int manCount = 0;
            int famanCount = 0;
            int incureCount = 0;
            foreach (var row in patientsDt)
            {
                if (row.LEAVE_HOSPITAL_TIME.Year == this.dateEditYear.DateTime.Year)
                {
                    allcount++;
                }
                if (row.IS_NEW == "0" && row.LEAVE_HOSPITAL_TIME.Year == this.dateEditYear.DateTime.Year)
                {
                    newCount++;
                }
                if (row.IS_NEW != "0" && row.LEAVE_HOSPITAL_TIME.Year == this.dateEditYear.DateTime.Year)
                {
                    ountCount++;
                }
                if (row.SEX.Trim().Equals("男") && row.LEAVE_HOSPITAL_TIME.Year == this.dateEditYear.DateTime.Year)
                {
                    manCount++;
                }
                else if (row.SEX.Trim().Equals("女") && row.LEAVE_HOSPITAL_TIME.Year == this.dateEditYear.DateTime.Year)
                {
                    famanCount++;
                }
                var cureDt = hemoService.GetCureListByHemoId(row.HEMODIALYSIS_ID, DateTime.Now.AddDays(-280));
                if (cureDt.Rows.Count > 0 && row.IS_NEW == "0")
                {
                    incureCount++;
                }
            }

            labelControl4.Text = string.Format("     性别：男{0}人   女：{1}人   比例为:{2}%", manCount, famanCount, famanCount == 0 ? manCount : Math.Round(((decimal)manCount / (famanCount)), 2));
            labelControl3.Text = string.Format("在透病人数据：{0} \r\r {1}新入科病人数：{2}人  转出病人数{3}人", incureCount, this.dateEditYear.DateTime.Year.ToString(), newCount, ountCount);

            //this.labelControl3.Text = string.Format("{0}年病人总数：{1}人 新入科病人数：{2}人  转出病人数{3}人  ", this.dateEditYear.DateTime.Year.ToString(), allcount, newCount, ountCount);
            //this.labelControl4.Text = string.Format("{0}年  在透人数：{1} ", this.dateEditYear.DateTime.Year.ToString(), incureCount);
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            this.patientExtendUI1.ExportToExcel();
        }


    }
}

