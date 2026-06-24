using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Hemo.IService.Config;
using Hemo.Service;
using Hemo.Client.Controls;
using Hemo.Model;
using Hemo.Utilities;
using Hemo.IService.PatientSchedule;

namespace Hemo.Client.UI.Hemodialysis
{
    public partial class ShowCureInfo : HemoBaseFrm
    {
        private IHemodialysis hemoService = ServiceManager.Instance.HemodialysisService;

        private IPatientSchedule scheduleService = ServiceManager.Instance.PatientSchedule;

        private CtlMedicalDocumentContainer medicalDocContainer = new CtlMedicalDocumentContainer();

        private string cureId;

        private string areaName;

        public string CureId
        {
            get { return cureId; }
            set { cureId = value; }
        }
        private string recipeid;

        public string RECIPE_ID
        {
            get { return recipeid; }
            set { recipeid = value; }
        }
        public string AreaName
        {
            get { return areaName; }
            set { areaName = value; }
        }

        public ShowCureInfo()
        {
            InitializeComponent();
        }

        private void ShowCureInfo_Load(object sender, EventArgs e)
        {
            LoadCureDocument();
        }

        private void LoadCureDocument()
        {
            medicalDocContainer.HaveNextPage = false;
            int countNum = 0;
            int countParam;
            int pageParamCount = 10;

            string[] records = null;
            bool isShow = true;
            HemodialysisModel.MED_CURE_MAIN_CRRTDataTable dtCRRTCure = null;
            DataSet data = hemoService.GetAllCure(cureId);

            if (data.Tables["MED_HEMODIALYSIS_PARAMETERS"] != null)
            {
                if (areaName.Equals("CRRT"))
                {
                    var dtCRRTTemp = hemoService.GetCRRTCureByCureId(cureId);
                    if (dtCRRTTemp != null && dtCRRTTemp.Rows.Count > 0)
                    {
                        dtCRRTCure = dtCRRTTemp.Clone() as HemodialysisModel.MED_CURE_MAIN_CRRTDataTable;
                        dtCRRTCure.ImportRow(dtCRRTTemp.OrderByDescending(row => row.CREATE_DATE).First());
                        var rows = data.Tables["MED_HEMODIALYSIS_PARAMETERS"].AsEnumerable().Where(r => Utility.CDate(r["CREATE_DATE"].ToString()).Date.CompareTo(dtCRRTCure[0].CREATE_DATE) == 0 && r["CRRT_CLASS"].ToString().Equals(dtCRRTCure[0].CRRT_CLASS));
                        countNum = rows != null ? rows.Count() : 0;
                    }
                }
                else
                {
                    countNum = data.Tables["MED_HEMODIALYSIS_PARAMETERS"].Rows.Count;
                }
            }

            if (data.Tables["MED_CURE_MAIN"] != null)
            {
                DataTable dtCureMain = data.Tables["MED_CURE_MAIN"];
                if (areaName.Equals("CRRT"))
                {
                    if (dtCRRTCure != null && dtCRRTCure.Rows.Count > 0)
                    {
                        records = dtCRRTCure[0].SUMMARY2.Split("|".ToCharArray());
                    }
                }
                else
                {
                    records = dtCureMain.Rows[0]["SUMMARY2"].ToString().Split("|".ToCharArray());
                }
            }

            WPF_DocumentBase document = null;
            if (areaName.Equals("CRRT"))
            {
                var schedule = scheduleService.GetPatientScheduleByRecipeId(dtCRRTCure[0].RECIPE_ID);
                var row = (schedule != null && schedule.Rows.Count > 0) ? schedule[0] : null;
                document = new CtlMedicalDocumentCRRT(row, data, countNum, 10, dtCRRTCure[0].CRRT_CLASS, dtCRRTCure[0].CREATE_DATE);
            }
            else
            {
                var schedule = scheduleService.GetPatientScheduleByRecipeId(RECIPE_ID);
                var row = (schedule != null && schedule.Rows.Count > 0) ? schedule[0] : null;
                document = new CtlMedicalDocumentNew(row, data);
                countNum = countNum - document.currentParamNoShowInt;
                pageParamCount = document.paramRowNum;
            }
            document.IsShowGrid(isShow);
            medicalDocContainer.Add(document);

            //进行计算分页
            //计算可以有多少页的参数页
            int pageCount = (countNum - 10) / 20;
            int pageCountExt = (countNum - 10) % 20;
            if (pageCountExt > 0)
            {
                pageCount++;
            }
            countParam = countNum - 10;

            for (int i = 2; i < pageCount + 2; i++)
            {
                medicalDocContainer.Remove(i.ToString());
                CtlMedicalDocument3 document1 = areaName.Equals("CRRT") ? new CtlMedicalDocument3(data, (dtCRRTCure != null && dtCRRTCure.Rows.Count > 0) ? dtCRRTCure[0] : null, countParam, 20, 1, i, areaName) : new CtlMedicalDocument3(data, countParam, 20, "sqlByParams", i, areaName);
                medicalDocContainer.Add(i.ToString(), document1);
                countParam = countParam - 20;
            }

            DataTable cureMainDataTable = data.Tables["MED_CURE_MAIN"];

            if (cureMainDataTable != null)
            {
                string strSummary = cureMainDataTable.Rows[0]["SUMMARY"].ToString();

                string strSummary1 = cureMainDataTable.Rows[0]["SUMMARY2"].ToString().Replace("|||", "");

                //病情记录
                if (strSummary.Length > 164 || strSummary1.Length > 0)
                {
                    var lastDoc = pageCount + 2;
                    CtlMedicalDocument2 document2 = new CtlMedicalDocument2(data);
                    medicalDocContainer.Add(lastDoc.ToString(), document2);
                }
            }

            this.elementHost.Child = medicalDocContainer;
        }
    }
}