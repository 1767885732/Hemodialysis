/*----------------------------------------------------------------
 * Copyright (C) 2005 医疗科技发展有限公司
 * 文件功能描述:排班控件
 * 创建标识:贺建操-2014年8月2日
 * ----------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevExpress.Utils;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraTreeList;
using DevExpress.XtraTreeList.Nodes;
using Hemo.Client.Controls.Schedule;
using Hemo.Client.Core;
using Hemo.Client.UI.PatientSchedule;
using Hemo.IService;
using Hemo.IService.Config;
using Hemo.IService.Machine;
using Hemo.IService.PatientSchedule;
using Hemo.IService.Permission;
using Hemo.Model;
using Hemo.Service;
using System.ComponentModel;
using Hemo.Client.UI.Patient;
using Hemo.Client.UI.Lab;

namespace Hemo.Client.Controls.Schedule
{
    public partial class CtlScheduleMainNew : XtraUserControl
    {
        #region 变量


        private IUser _userService = ServiceManager.Instance.UserService;
        private IConfig _configService = ServiceManager.Instance.ConfigService;
        private IMachine _machineService = ServiceManager.Instance.MachineService;
        private IPatient _patientService = ServiceManager.Instance.PatientService;
        private IHemodialysis _hemodialysisService = ServiceManager.Instance.HemodialysisService;
        private IPatientSchedule _patientScheduleService = ServiceManager.Instance.PatientSchedule;

        private Dictionary<ConfigModel.MED_COMMON_ITEMLISTRow, int> _areaDict;
        private ConfigModel.MED_COMMON_ITEMLISTDataTable _bedDataTable;
        private ConfigModel.MED_COMMON_ITEMLISTDataTable _purifierModelDataTable;
        private ConfigModel.MED_COMMON_ITEMLISTDataTable _banChiDateTable;
        private MachineModel.MED_DIALYSIS_MACHINEDataTable _machineDataTable;
        private PatientModel.MED_PATIENTSDataTable _patientDataTable;
        private HemodialysisModel.MED_HEMO_RECIPEDataTable _recipeDataTable;

        private PatientScheduleModel.MED_PATIENT_SCHEDULE_TEMPLATEDataTable _patientScheduleTemplateDataTable;
        private PatientScheduleModel.MED_PATIENT_SCHEDULE_TEMPLATERow _patientScheduleTemplateRow;

      


        #endregion

        #region 构造函数

        public CtlScheduleMainNew()
        {
            this.InitializeComponent();
        }

        #endregion

        #region 方法

        /// <summary>
        /// 绑定患者列表数据
        /// </summary>
        private void LoadPatientTreeData()
        {
            this.busyIndicator1.ShowLoadingScreenFor(this.treeListPatient);
            DataTable dtMainCure = new DataTable();
            this.treeListPatient.BeginUnboundLoad();
            this.treeListPatient.Nodes.Clear();

            using (BackgroundWorker worker = new BackgroundWorker())
            {
                worker.DoWork += delegate(object sender1, DoWorkEventArgs e1)
                {
                    DateTime dtBeginMonth = DateTime.Now.Date;//.Parse(string.Format("{0}-{1}-{2}", this._beginDate.Year, this._beginDate.Month, "1")).Date;
                    DateTime dtEndMonth = dtBeginMonth.AddMonths(1).AddDays(-1).Date;
                    dtMainCure = this._hemodialysisService.GetMainCureGroupByHemoIDAndPurificationMode(dtBeginMonth, dtEndMonth);
                };
                worker.RunWorkerCompleted += delegate(object sender2, RunWorkerCompletedEventArgs e2)
                {
                    foreach (PatientModel.MED_PATIENTSRow patientRow in this._patientDataTable.Rows)
                    {
                        DataRow[] rowMainCures = dtMainCure.Select(string.Format("HEMODIALYSIS_ID = '{0}'", patientRow.HEMODIALYSIS_ID));
                        StringBuilder text = new StringBuilder();

                        if (rowMainCures.Length > 0)
                        {
                            text.Append("(");

                            for (int i = 0; i < rowMainCures.Length; i++)
                            {
                                text.AppendFormat("{0}{1}:{2}", i == 0 ? string.Empty : " ", rowMainCures[i]["PURIFICATION_MODE_NAME"], rowMainCures[i]["COUNT"]);
                            }

                            text.Append(")");
                        }

                        this.treeListPatient.AppendNode(new object[] { string.Format("{0} {1}", patientRow.NAME, text.ToString()), patientRow.SEX, patientRow.HEMODIALYSIS_ID }, -1);
                    }

                    foreach (TreeListNode node in this.treeListPatient.Nodes)
                    {
                        if (string.Compare(node.GetValue("SEX").ToString(), "男", true) == 0)
                        {
                            node.StateImageIndex = 0;
                            node.ImageIndex = 0;
                        }
                        else
                        {
                            node.StateImageIndex = 1;
                            node.ImageIndex = 1;
                        }
                    }

                    this.busyIndicator1.HideLoadingScreen();

                };
                worker.RunWorkerAsync();
            }

            this.treeListPatient.EndUnboundLoad();
        }

        /// <summary>
        /// 创建病患排班控件
        /// </summary>
        private void CreatePatientScheduleControls()
        {
            int Width = 0;
            int height = 0;
            #region 病区显示列表
            
            //创建病区显示列表
            foreach (KeyValuePair<ConfigModel.MED_COMMON_ITEMLISTRow, int> areaItem in this._areaDict)
            {
                LabelControl lblOffice = new LabelControl();
                lblOffice.BorderStyle = BorderStyles.Simple;
                lblOffice.Appearance.TextOptions.HAlignment = HorzAlignment.Center;
                lblOffice.Appearance.TextOptions.VAlignment = VertAlignment.Center;
                lblOffice.AutoSizeMode = LabelAutoSizeMode.None;
                lblOffice.Width = areaItem.Value + 10;
                lblOffice.Height = 50;
                lblOffice.Dock = DockStyle.Left;
                lblOffice.Text = areaItem.Key.ITEM_NAME;

                this.groupControl_office.Controls.Add(lblOffice);
                this.groupControl_office.Height = 50;

                Width += areaItem.Value + 10;
            }

            #endregion
            #region 创建班次列表
            
            //创建班次列表
            foreach (ConfigModel.MED_COMMON_ITEMLISTRow row in this._banChiDateTable.OrderByDescending(i => i.ORDER_NUMBER))
            {
                LabelControl lblBanchi = new LabelControl();
                lblBanchi.BorderStyle = BorderStyles.Simple;
                lblBanchi.Appearance.TextOptions.HAlignment = HorzAlignment.Center;
                lblBanchi.Appearance.TextOptions.VAlignment = VertAlignment.Center;
                lblBanchi.AutoSizeMode = LabelAutoSizeMode.None;
                lblBanchi.Height = 320;
                lblBanchi.Dock = DockStyle.Top;
                lblBanchi.Text = row.ITEM_NAME.ToString();
             

                Panel pcontent = new Panel();
                pcontent.Width = Width;
                if (lblBanchi.Text == "上午")
                {
                    lblBanchi.Height = 320 + 30;
                    pcontent.Height = lblBanchi.Height - 30;
                }
                else
                {
                    pcontent.Height = lblBanchi.Height;
                }

                pcontent.Dock = DockStyle.Top;
                pcontent.Tag = row.ITEM_VALUE.ToString();
                pcontent.BorderStyle = System.Windows.Forms.BorderStyle.None;
                this.panel1.Controls.Add(pcontent);

                this.groupControl_banChi.Controls.Add(lblBanchi);


                height = height + lblBanchi.Height;
            }
            this.panel1.Width = Width;
            this.panel1.Height = height;
            this.groupControl_office.Width = Width;
            this.groupControl_banChi.Height = height + 20;

            #endregion
            #region 创建班次病区的床位列表
            
            //创建班次列表
            foreach (Panel panel in this.panel1.Controls)
            {
                SchedulePanelNew ctlSchedulePanel = new SchedulePanelNew();
                ctlSchedulePanel._banChi = panel.Tag.ToString();
                ctlSchedulePanel._dayOfWeek = 1;
                ctlSchedulePanel._areaDict = this._areaDict;
                ctlSchedulePanel._bedDataTable = this._bedDataTable;
                ctlSchedulePanel._machineDataTable = this._machineDataTable;
                ctlSchedulePanel.Dock = DockStyle.Fill;
                panel.Controls.Add(ctlSchedulePanel);
            }

            #endregion
        }

      

       




        private string ConvertToString(object o)
        {
            if (o == null)
                return string.Empty;
            if (o == DBNull.Value || o is DBNull)
                return string.Empty;
            return o.ToString();
        }

        #endregion

        #region 事件

        /// <summary>
        /// 加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CtlScheduleMainNew_Load(object sender, EventArgs e)
        {
            SchedulePersonDragManager.Instance.InitSchedulePersonControlDict();

            this._areaDict = this._userService.GetAreaList(LoginUser.User.USER_ID).OrderByDescending(r => r.ORDER_NUMBER).ToDictionary(r => r, r => 200);
            this._bedDataTable = this._configService.GetConfigList(string.Empty, string.Empty, "床位", "1");
            this._purifierModelDataTable = this._configService.GetConfigList(string.Empty, string.Empty, "净化器类型", "1");
            this._banChiDateTable = this._configService.GetConfigList(string.Empty, string.Empty, "班次", "1");
            this._machineDataTable = this._machineService.GetMachineList();
            this._patientDataTable = this._patientService.GetPatientList();
            this._recipeDataTable = this._hemodialysisService.GetAllRecipe();

            this.CreatePatientScheduleControls();

            this.loadPatients();

        }

 

        /// <summary>
        /// 查询检索
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtSearchPatient_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                string condtion = this.txtSearchPatient.Text.Trim().ToUpper();

                foreach (TreeListNode node in this.treeListPatient.Nodes)
                {
                    if (node.GetValue("NAME").ToString() == condtion ||
                        (node.GetValue("INPUT_CODE") != null && node.GetValue("INPUT_CODE").ToString() == condtion) ||
                        node.GetValue("HEMODIALYSIS_ID").ToString() == condtion)
                        this.treeListPatient.FocusedNode = node;
                }
            }
        }

        private void treeListPatient_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                TreeListHitInfo hInfo = this.treeListPatient.CalcHitInfo(new Point(e.X, e.Y));

                if (hInfo.HitInfoType == HitInfoType.Cell)
                    this.treeListPatient.DoDragDrop(
                        new SchedulePersonDragInfo()
                        {
                            SourceCtlSchedulePerson = null,
                            PatientRow = this._patientDataTable.FindByHEMODIALYSIS_ID(hInfo.Node[2].ToString())
                        }, DragDropEffects.Copy | DragDropEffects.Move);
            }

            if (e.Button == MouseButtons.Right)
            {
                contextMenuStrip2.Show(MousePosition);
            }
        }

        /// <summary>
        /// 提交
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void btnSave_Click(object sender, EventArgs e)
        {
           
        }

        /// <summary>
        /// 保存为模板
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void btnSaveTemplate_Click(object sender, EventArgs e)
        {

            
        }

        /// <summary>
        /// 打开模板
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void btnOpenTemplate_Click(object sender, EventArgs e)
        {
        }

        /// <summary>
        /// 查询病人树数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnQuery_Click(object sender, EventArgs e)
        {
            loadPatients();
        }

        private void loadPatients()
        {
            this.busyIndicator1.ShowLoadingScreenFor(this.treeListPatient);

            using (BackgroundWorker worker = new BackgroundWorker())
            {
                worker.DoWork += delegate(object sender1, DoWorkEventArgs e1)
                {
                    if (txtSearchPatient.Text.Length == 0)
                    {
                        this._patientDataTable = this._patientService.GetPatientList();
                    }
                    else
                    {
                        this._patientDataTable = this._patientService.GetPatientListByParams(txtSearchPatient.Text.Trim(), "");
                        //this._patientDataTable = this._patientService.GetPatientListByType(pType);//if (pType == "住院" || pType == "门诊")
                    }
                };
                worker.RunWorkerCompleted += delegate(object sender2, RunWorkerCompletedEventArgs e2)
                {
                    this.LoadPatientTreeData();
                    this.busyIndicator1.HideLoadingScreen();

                };
                worker.RunWorkerAsync();
            }
        }


        private void treeListPatient_MouseUp(object sender, MouseEventArgs e)
        {

        }

        private void treeListPatient_FocusedNodeChanged(object sender, FocusedNodeChangedEventArgs e)
        {
            if (this.treeListPatient.FocusedNode != null)
            {
                PatientModel.MED_PATIENTSRow PatientRow = this._patientDataTable.FindByHEMODIALYSIS_ID(this.treeListPatient.FocusedNode.GetValue("HEMODIALYSIS_ID").ToString());
                if (PatientRow != null)
                {
                }
            }
        }

        private void txtSearchPatient_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                loadPatients();
            }
        }

        private void 添加给药记录ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EditPatientNew frmEditPatient = new EditPatientNew();
            frmEditPatient.Current = null;
            frmEditPatient.ShowDialog();
        }

        private void 修改患者ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.treeListPatient.FocusedNode != null)
            {
                PatientModel.MED_PATIENTSRow PatientRow = this._patientDataTable.FindByHEMODIALYSIS_ID(this.treeListPatient.FocusedNode.GetValue("HEMODIALYSIS_ID").ToString());
                EditPatientNew frmEditPatient = new EditPatientNew();
                frmEditPatient.Current = PatientRow;
                frmEditPatient.ShowDialog();
            }
        }

        private void labListRecord_Click(object sender, EventArgs e)
        {
            if (this.treeListPatient.FocusedNode != null)
            {
                PatientModel.MED_PATIENTSRow PatientRow = this._patientDataTable.FindByHEMODIALYSIS_ID(this.treeListPatient.FocusedNode.GetValue("HEMODIALYSIS_ID").ToString());
                LabFrm labFrm = new LabFrm(PatientRow);
                labFrm.ShowDialog();
            }
        }

        #endregion

    }
}
