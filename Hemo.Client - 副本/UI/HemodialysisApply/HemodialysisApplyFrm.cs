/*----------------------------------------------------------------
// Copyright (C) 2005 苏州XX公司有限公司
// 描述：预约申请排班窗体类
// 创建时间：2014-08-17
// 创建者：贺建操
//  
// 修改时间：
// 修改人：
// 修改描述：
----------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using DevExpress.Utils;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using Hemo.Client.Controls.HemodialysisApply;
using Hemo.IService.PatientSchedule;
using Hemo.Model;
using Hemo.Service;

namespace Hemo.Client.UI.HemodialysisApply
{
    public partial class HemodialysisApplyFrm :HemoBaseFrm
    {
        #region 变量

        private string _hemodialysisID;
        private List<string> _banciList;
        private Dictionary<FlowLayoutPanel, CtlApplyResult> _ctlDict;
        private GroupControl[] _groupControls;
        private IPatientSchedule _patientScheduleService = ServiceManager.Instance.PatientSchedule;
        private PatientScheduleModel.MED_HEMO_APPLYDataTable _hemodialysisApplyDataTable;

        #endregion

        #region 构造函数

        public HemodialysisApplyFrm(string hemodialysisID)
        {
            this.InitializeComponent();

            this._hemodialysisID = hemodialysisID;
        }

        #endregion

        #region 方法

        private void CreateHemodialysisApplyControls()
        {
            int height = 143;

            for (int i = 0; i < this._banciList.Count; i++)
            {
                LabelControl lblRange = new LabelControl();
                lblRange.Appearance.TextOptions.HAlignment = HorzAlignment.Center;
                lblRange.Appearance.TextOptions.VAlignment = VertAlignment.Center;
                lblRange.AutoSizeMode = LabelAutoSizeMode.None;
                lblRange.BorderStyle = BorderStyles.Simple;
                lblRange.Height = height;
                lblRange.Dock = DockStyle.Top;
                lblRange.Text = this._banciList[i];

                this.grpBanci.Controls.Add(lblRange);

                for (int j = 0; j < this._groupControls.Length; j++)
                {
                    int[] array = new int[] { i, j };

                    FlowLayoutPanel panel = new FlowLayoutPanel();
                    panel.BorderStyle = BorderStyle.FixedSingle;
                    panel.ContextMenuStrip = this.contextMenuStrip1;
                    panel.Dock = DockStyle.Top;
                    panel.Height = height;
                    panel.Name = Guid.NewGuid().ToString();
                    panel.Tag = array;

                    PatientScheduleModel.MED_HEMO_APPLYRow[] rows = this._hemodialysisApplyDataTable.Select(string.Format("BANCI_ID = '{0}' AND APPLY_WEEKDAY = '{1}'", array[0], array[1])) as PatientScheduleModel.MED_HEMO_APPLYRow[];

                    CtlApplyResult ctlApplyResult = new CtlApplyResult();
                    ctlApplyResult.SetInfo(rows.Length == 0 ? null : rows[0]);

                    this._ctlDict.Add(panel, ctlApplyResult);

                    panel.Controls.Add(ctlApplyResult);

                    this._groupControls[j].Controls.Add(panel);
                }
            }
        }

        #endregion

        #region 事件

        private void HemodialysisApplyFrm_Load(object sender, EventArgs e)
        {
            this._groupControls = new GroupControl[] { this.grpMonday, this.grpTuesday, this.grpWednesday, this.grpThursday, this.grpFriday, this.grpSaturday };

            this._banciList = new List<string>();
            this._banciList.Add("晚班");
            this._banciList.Add("中班");
            this._banciList.Add("早班");

            this._ctlDict = new Dictionary<FlowLayoutPanel, CtlApplyResult>();

            this._hemodialysisApplyDataTable = this._patientScheduleService.GetHemodialysisApplyList(this._hemodialysisID);

            this.CreateHemodialysisApplyControls();
        }

        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {
            FlowLayoutPanel panel = this.contextMenuStrip1.SourceControl as FlowLayoutPanel;
            CtlApplyResult ctlApplyResult = this._ctlDict[panel];
            bool isNew = ctlApplyResult.HemodialysisApplyRow == null;

            this.tsmiAddApply.Visible = isNew;
            this.tsmiEditApply.Visible = this.tsmiDeleteApply.Visible = !isNew;
        }

        private void tsmiOptApply_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem tsmi = sender as ToolStripMenuItem;
            FlowLayoutPanel panel = (tsmi.GetCurrentParent() as ContextMenuStrip).SourceControl as FlowLayoutPanel;
            CtlApplyResult ctlApplyResult = this._ctlDict[panel];
            int[] array = panel.Tag as int[];
            int banciID = array[0];
            int weekDay = array[1];

            switch (tsmi.Name.ToLower())
            {
                case "tsmiaddapply": //新增预约申请
                case "tsmieditapply": //编辑预约申请
                    EditHemodialysisApply editHemodialysisApply = new EditHemodialysisApply(banciID, weekDay, this._hemodialysisID, this._hemodialysisApplyDataTable, ctlApplyResult.HemodialysisApplyRow);
                    DialogResult result = editHemodialysisApply.ShowDialog();

                    if (result == DialogResult.Yes)
                        ctlApplyResult.SetInfo(editHemodialysisApply.HemodialysisApplyRow);
                    break;

                case "tsmideleteapply": //删除预约申请
                    if (ctlApplyResult.HemodialysisApplyRow == null)
                        return;

                    if (DialogResult.Yes == XtraMessageBox.Show("确定要删除预约申请吗？", this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question))
                    {
                        ctlApplyResult.HemodialysisApplyRow.Delete();

                        this._patientScheduleService.SaveHemodialysisApplyInfo(this._hemodialysisApplyDataTable);

                        ctlApplyResult.SetInfo(null);
                    }
                    break;

                default:
                    break;
            }
        }

        #endregion
    }
}