using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Hemo.Client.UI.Machine;
using Hemo.Model;
using Hemo.IService.Config;
using Hemo.Service;

namespace Hemo.Client.UI.Config
{
     [ToolboxItem(true)]
    public partial class ScreenMsgManager : ViewBase
    {
        public ScreenMsgManager()
        {
            InitializeComponent();
            this.deBeginTime.DateTime = DateTime.Now.AddDays(-3);
            this.deEndTime.DateTime = DateTime.Now;
            InzationDate();
        }

        private ConfigModel.MED_SCREEN_MSGDataTable msgDt = null;
        private IConfig configServer = ServiceManager.Instance.ConfigService;
        private OperatorMembers opera;

        private void TableControlsJump(DevExpress.XtraTab.XtraTabPage page)
        {
            this.xtraTabControl1.SelectedTabPage = page;
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            TableControlsJump(this.xtraTabPage1);
            InzationDate();
        }

        private void InzationDate()
        {
            busyIndicator1.Visible = true;
            busyIndicator1.ShowLoadingScreenFor(this.gridData);
            msgDt = new ConfigModel.MED_SCREEN_MSGDataTable();
            TableControlsJump(xtraTabPage1);
            this.errorProvider.ClearErrors();
            this.opera = OperatorMembers.OpeDelete;
            this.textCount.EditValue = 0; this.txtMsgContent.Text = string.Empty;
            using (BackgroundWorker worker = new BackgroundWorker())
            {
                worker.DoWork += delegate(object sender1, DoWorkEventArgs e1)
                {
                    msgDt = configServer.GetMsgByParams(this.deBeginTime.DateTime, this.deEndTime.DateTime);

                };
                worker.RunWorkerCompleted += delegate(object sender2, RunWorkerCompletedEventArgs e2)
                {
                    this.gridData.DataSource = msgDt;

                    this.busyIndicator1.HideLoadingScreen();
                };
                worker.RunWorkerAsync();
            }
        }
        /// <summary>
        /// 新增或者删除 
        /// </summary>
        private void AddOrEditMethond(OperatorMembers opeMember)
        {
            opera = opeMember;
            var dr = this.gridView1.GetFocusedDataRow() as ConfigModel.MED_SCREEN_MSGRow;
            switch (opeMember)
            {
                case OperatorMembers.OpeAdd:
                    this.textCount.EditValue = 0;
                    this.txtMsgContent.Text = string.Empty;

                    break;
                case OperatorMembers.OpeEdit:
                    if (dr != null)
                    {
                        this.textCount.EditValue = dr.COUNTS;
                        this.txtMsgContent.Text = dr.MSG;

                    }
                    break;
                case OperatorMembers.OpeDelete:
                    break;
            }
        }
        private void btnAdd_Click(object sender, EventArgs e)
        {
            TableControlsJump(this.xtraTabPage2);
            AddOrEditMethond(OperatorMembers.OpeAdd);

        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            TableControlsJump(this.xtraTabPage2);
            AddOrEditMethond(OperatorMembers.OpeEdit);

        }

        private void btnDelete_Click(object sender, EventArgs e)
        {

        }
        private bool CheckValue()
        {
            this.errorProvider.ClearErrors();
            if (string.IsNullOrEmpty(this.txtMsgContent.Text.ToString()))
            {
                errorProvider.SetError(txtMsgContent, "请输入播报内容！");
                return false;
            }
            return true;
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (CheckValue())
            {
                if (opera == OperatorMembers.OpeEdit)
                {
                    var dr = this.gridView1.GetFocusedDataRow() as ConfigModel.MED_SCREEN_MSGRow;
                    dr.MSG = this.txtMsgContent.Text;
                    dr.COUNTS = Convert.ToInt32(this.textCount.EditValue);
                }
                else if (opera == OperatorMembers.OpeAdd)
                {
                    var newRow = msgDt.NewMED_SCREEN_MSGRow();
                    newRow.ID = Guid.NewGuid().ToString();
                    newRow.MSG = this.txtMsgContent.Text;
                    newRow.COUNTS = Convert.ToInt32(this.textCount.EditValue);
                    newRow.INSERT_TIME = DateTime.Now;
                    newRow.USER_ID = Hemo.Client.Core.HemoApplicationContext.Current.CurrentUser.USER_NAME;
                    newRow.TYPE = 2;
                    newRow.STATUS = 0;
                    newRow.OTHER1 = 0;
                    msgDt.AddMED_SCREEN_MSGRow(newRow);
                }
            }
            if (configServer.SaveMsg(this.msgDt) > 0)
            {
                InzationDate();
            }

        }
    }
    public enum OperatorMembers
    {
        OpeAdd = 0,
        OpeEdit = 1,
        OpeDelete = 2
    }
}
