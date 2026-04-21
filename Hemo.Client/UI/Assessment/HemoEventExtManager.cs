using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Hemo.Model;
using Hemo.IService;
using Hemo.Service;
using DevExpress.XtraEditors;
using Hemo.Client.UI.Machine;

namespace Hemo.Client.UI.Assessment
{
    public partial class HemoEventExtManager : ViewBase
    {
        public HemoEventExtManager()
        {
            InitializeComponent();
        }
        private PatientModel.MED_HEMO_EVENTINFODataTable hemoEventDt = null;
        private IPatient objPatient = ServiceManager.Instance.PatientService;
        private HemoEventExtInfo frm = null;

        private void iNewButton_Click(object sender, EventArgs e)
        {
            frm._HemoEventExtInfoRow = null;
            frm.LoadUserInfo(string.Empty);
            frm.InzationData();
            this.xtraTabControl1.SelectedTabPage = this.xtraTabPage2;
        }

        private void iRefreshButton_Click(object sender, EventArgs e)
        {
            InzationData();
        }

        private void InzationData()
        {
            using (var _woker = new BackgroundWorker())
            {
                hemoEventDt = new PatientModel.MED_HEMO_EVENTINFODataTable();
                _woker.DoWork += (o, e) =>
                {
                    hemoEventDt = objPatient.GetHemoEventInfoByBetweenDt(this.beginTime.DateTime, this.endTime.DateTime, "0");
                };
                _woker.RunWorkerCompleted += (o1, e1) =>
               {
                   this.gridControl1.DataSource = hemoEventDt;
               };
                _woker.RunWorkerAsync();
            }
        }

        private void gridView1_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            if (e.Clicks == 2)
            {
                var dataRow = gridView1.GetFocusedDataRow() as PatientModel.MED_HEMO_EVENTINFORow;
                if (dataRow == null) return;

                this.frm._HemoEventExtInfoRow = dataRow;
                frm.LoadUserInfo(dataRow.HEMODIALYSIS_ID);
                frm.InzationData();

                this.xtraTabControl1.SelectedTabPage = this.xtraTabPage2;
            }
        }
        /// <summary>
        /// 是否显示过滤行
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void iFilterCheckEdit_CheckedChanged(object sender, EventArgs e)
        {
            this.gridView1.OptionsView.ShowAutoFilterRow = this.iFilterCheckEdit.Checked;
        }


        private void IEdit_Click(object sender, EventArgs e)
        {
            var dataRow = gridView1.GetFocusedDataRow() as PatientModel.MED_HEMO_EVENTINFORow;
            if (dataRow == null) return;

            frm._HemoEventExtInfoRow = dataRow;
            frm.LoadUserInfo(dataRow.HEMODIALYSIS_ID);
            frm.InzationData();

            this.xtraTabControl1.SelectedTabPage = this.xtraTabPage2;
        }

        private void iDeleteButton_Click(object sender, EventArgs e)
        {
            if (XtraMessageBox.Show("你确定要删除选中的项吗？", "删除提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {

                var dataRow = gridView1.GetFocusedDataRow() as PatientModel.MED_HEMO_EVENTINFORow;
                if (dataRow == null) return;

                this.objPatient.DeleteHemoDefaultModelById(dataRow.ID);

            }
        }

        private void iCloseButton_Click(object sender, EventArgs e)
        {
            this.FindForm().Close();
        }

        private void HemoEventExtManager_Load(object sender, EventArgs e)
        {
            DateTime dt = DateTime.Now;  //当前时间  
            DateTime startMonth = dt.AddDays(1 - dt.Day);  //本月月初 
            DateTime endMonth = startMonth.AddMonths(1).AddDays(-1);

            this.beginTime.DateTime = startMonth;
            this.endTime.DateTime = endMonth;
            InzationData();
            frm = new HemoEventExtInfo();
            frm.Dock = DockStyle.Fill;
            this.xtraTabPage2.Controls.Add(frm);
            this.frm.SaveOkEvent += new EventHandler(frm_SaveOkEvent);



        }

        void frm_SaveOkEvent(object sender, EventArgs e)
        {
            this.xtraTabControl1.SelectedTabPage = this.xtraTabPage1;
            InzationData();

        }

    }
}
