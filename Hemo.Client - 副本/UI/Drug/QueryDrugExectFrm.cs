using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors.Repository;
using Hemo.IService.Config;
using Hemo.Service;
using Hemo.Model;
using Hemo.Client.Print;
using DevExpress.XtraReports.UI;
using DevExpress.XtraEditors;
using Hemo.IService.PatientSchedule;
using Hemo.Client.Core;
using Hemo.Utilities;
using DevExpress.XtraPrinting;

namespace Hemo.Client.UI.Drug
{
    public partial class QueryDrugExectFrm : HemoBaseFrm
    {
        private IHemodialysis _hemodialysisService = ServiceManager.Instance.HemodialysisService;
        private IConfig _configService = ServiceManager.Instance.ConfigService;
        private IPatientSchedule _patientScheduleService = ServiceManager.Instance.PatientSchedule;

        private HemodialysisModel.MED_CURE_DRUGDataTable drugDt = null;

        public string roomId { get; set; }
        public string banchiId { get; set; }
        public DateTime currentDt { get; set; }
        public QueryDrugExectFrm()
        {
            InitializeComponent();
        }

        private void QueryDrugExectFrm_Load(object sender, EventArgs e)
        {
            var dtBanci = this._configService.GetConfigList(string.Empty, string.Empty, "班次", "1");
            var dtAREA = this._configService.GetConfigList(string.Empty, string.Empty, "区域", "1");
            this.lopRoom.Properties.DataSource = dtAREA;
            this.lopBanchi.Properties.DataSource = dtBanci;
            //Hemo.Utilities.Utility.BindLookUpEdit(this.lopRoom as LookUpEdit, "ITEM_ID", "ITEM_NAME", dtAREA, "ITEM_NAME", "病室");
            //Hemo.Utilities.Utility.BindLookUpEdit(this.lopBanchi as LookUpEdit, "ITEM_VALUE", "ITEM_NAME", dtAREA, "ITEM_NAME", "班次");
            if (!string.IsNullOrEmpty(roomId))
            {
                this.lopRoom.EditValue = roomId;
            }
            else
            {
                this.lopRoom.EditValue = dtAREA[0].ITEM_ID;
            }
            if (!string.IsNullOrEmpty(banchiId))
            {
                this.lopBanchi.EditValue = banchiId;
            }
            else
            {
                this.lopBanchi.EditValue = dtBanci[0].ITEM_VALUE;
            }

            if (currentDt != null)
            {
                this.deStartDate.DateTime = currentDt.Date;
                this.deEndDate.DateTime = currentDt.Date.AddDays(1).AddSeconds(-1);
            }

            BangDingPatient();
            InzationData();

        }
        private void InzationData()
        {
            using (var _worker = new BackgroundWorker())
            {
                drugDt = new HemodialysisModel.MED_CURE_DRUGDataTable();
                _worker.DoWork += delegate(object o, DoWorkEventArgs e)
                {
                    drugDt = _hemodialysisService.GetValidCureDrugByRoomIdAndData(lopRoom.EditValue.ToString(), lopBanchi.EditValue.ToString(), this.deStartDate.DateTime, this.deEndDate.DateTime, this.lupPatients.EditValue.ToString());
                };
                _worker.RunWorkerCompleted += delegate(object o1, RunWorkerCompletedEventArgs e1)
                {
                    this.gridControl4.DataSource = drugDt;
                };
                _worker.RunWorkerAsync();
            }
        }
        private void btnQuery_Click(object sender, EventArgs e)
        {
            InzationData();
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            var drugExec = drugDt.Clone() as HemodialysisModel.MED_CURE_DRUGDataTable;
            drugDt.Where(row => row.STATUS.Equals("1")).CopyToDataTable(drugExec, LoadOption.OverwriteChanges);
            PrintDrugUserReport printReport = new PrintDrugUserReport(currentDt, lopBanchi.EditValue.ToString(), drugExec);
            ReportPrintTool pt = new ReportPrintTool(printReport);
            printReport.ShowPreviewDialog();
        }

        private void lblToDate_Click(object sender, EventArgs e)
        {

        }

        private void lopRoom_EditValueChanged(object sender, EventArgs e)
        {
            BangDingPatient();
        }

        private void lopBanchi_EditValueChanged(object sender, EventArgs e)
        {
            BangDingPatient();

        }

        private void BangDingPatient()
        {
            if (string.IsNullOrEmpty(this.lopBanchi.EditValue.ToString()) || string.IsNullOrEmpty(this.lopRoom.EditValue.ToString()))
                return;
            DataTable dtScheduleList = _patientScheduleService.GetPatientScheduleList(LoginUser.User.USER_ID, this.deEndDate.DateTime.Date, this.deEndDate.DateTime.Date, this.lopBanchi.EditValue.ToString());
            var dt = Utility.GetSubTable(dtScheduleList, string.Format("DIALYSIS_ROOM_ID = '{0}'", this.lopRoom.EditValue.ToString())) as PatientScheduleModel.MED_PATIENT_SCHEDULEDataTable;
            var row = dt.NewMED_PATIENT_SCHEDULERow();
            row.PATIENT_SCHEDULE_ID = Guid.NewGuid().ToString();
            row.HEMODIALYSIS_ID = "kka";
            dt.Rows.InsertAt(row, 0);
            this.lupPatients.Properties.DataSource = dt;
        }

        private void gridView4_CustomColumnDisplayText(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventArgs e)
        {
            var rowCurrent = gridView4.GetDataRow(e.ListSourceRowIndex) as HemodialysisModel.MED_CURE_DRUGRow;
            if (rowCurrent == null)
            {
                return;
            }

            if (e.Column == gridColumn_COM)
            {
                var exitrows = drugDt.Count(wh => wh.COM_NO == rowCurrent.COM_NO);
                var smalCount = drugDt.Count(wh => wh.COM_NO == rowCurrent.COM_NO && Convert.ToInt32(wh.COM_SUB_NO) < Convert.ToInt32(rowCurrent.COM_SUB_NO));
                var bigCount = drugDt.Count(wh => wh.COM_NO == rowCurrent.COM_NO && Convert.ToInt32(wh.COM_SUB_NO) > Convert.ToInt32(rowCurrent.COM_SUB_NO));
                if (exitrows == 1)
                {
                    e.DisplayText = "";
                    return;
                }

                if (smalCount == 0)
                {
                    e.DisplayText = "┏";
                    return;
                }
                if (bigCount == 0)
                {
                    e.DisplayText = "┗";
                    return;
                }
                e.DisplayText = "┃";
                return;

            }
        }

        private void btnPrintT_Click(object sender, EventArgs e)
        {
            PrintableComponentLink link = new PrintableComponentLink(new PrintingSystem());
            link.Component = this.gridControl4;
            System.Drawing.Printing.Margins  margins = new System.Drawing.Printing.Margins(10, 10, 50, 50);

            link.Margins = margins;
            link.Landscape = true;
            link.CreateMarginalHeaderArea += new CreateAreaEventHandler(Link_CreateMarginalHeaderArea);
            link.CreateDocument();
            link.ShowPreview();
        }
        private void Link_CreateMarginalHeaderArea(object sender, CreateAreaEventArgs e)
        {
            string title = string.Format("医嘱执行记录");
            PageInfoBrick brick = e.Graph.DrawPageInfo(PageInfo.None, title, Color.DarkBlue,
               new RectangleF(0, 0, 150, 21), BorderSide.None);

            brick.LineAlignment = BrickAlignment.Center;
            brick.Alignment = BrickAlignment.Center;
            brick.AutoWidth = false;
            brick.Font = new System.Drawing.Font("宋体", 11f, FontStyle.Bold);


        }
    }
}
