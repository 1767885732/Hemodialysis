/*----------------------------------------------------------------
 * Copyright (C) 2005 医疗科技发展有限公司
 * 文件功能描述:修复系统加载时数据缓存问题
 * 创建标识:吕志强-2017年2月3日
 * 
 * 修改时间:2017年6月21日
 * 修改人:刘超
 * 修改描述:用户控件
 * 
 * 修改时间:2017年7月23日
 * 修改人:顾伟伟
 * 修改描述:修改对外公开的方法
 * ----------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Hemo.Client.Base;
using Hemo.Client.Base.XtraBaseInfo;
using DevExpress.Mvvm.POCO;
using System.Reflection;
using DevExpress.XtraEditors.Senders;
using Hemo.Client.UI.PatientFixUI;
using Hemo.Model;
using Hemo.Client.Controls;
using Hemo.Client.Modules.Patient;
using Hemo.Client.UI.Lab;
using Hemo.Client.UI.Hemodialysis;
using Hemo.Client.UI.Assessment;

namespace Hemo.Client.Modules
{
    public partial class AllPatientList : BaseMoudleControl
    {

        private CtlStartMainBak patientsInfoUi = null;

        private PatientOperatorUI patientoperator = null;

        private AllPatientLabRpt allpatientlabrpt = null;

        private FastRecipeListNew fastRecipeList = null;

        public PatientModel.MED_PATIENTSRow currentPatientsRow { set; get; }

        public DateTime currentCureDate { get; set; }

        public AllPatientList()
        {
            InitializeComponent();
        }

        public void LoadPatientInfoMehtno(PatientModel.MED_PATIENTSRow pRow)
        {
            //currentPatientsRow = pRow;
            //patientInfoUi.Current = currentPatientsRow;
            //patientInfoUi.InitalizeData();
        }

        private void AllPatientList_Load(object sender, EventArgs e)
        {

        }

        public void InzationControl(string type)
        {
            this.panelMain.Controls.Clear();
            if (type == "ALLPATIENT")
            {
                patientsInfoUi = new CtlStartMainBak();
                //patientInfoUi.Current = currentPatientsRow;
                //patientInfoUi.InitalizeData();
                //this.patientInfoUi.TabPatients.SelectedTabPageIndex = 0;
                patientsInfoUi.LoadPatientList("全部");
                patientsInfoUi.Dock = DockStyle.Fill;
                this.panelMain.Controls.Add(patientsInfoUi);
            }
            else if (type == "PATIENTOPERATOR")
            {
                patientoperator = new PatientOperatorUI();
                patientoperator.Margin = new System.Windows.Forms.Padding(50, 0, 0, 0);

                patientoperator.Dock = DockStyle.Fill;
                this.panelMain.Controls.Add(patientoperator);
            }
            else if (type == "AllPatientLabRpt")
            {
                allpatientlabrpt = new AllPatientLabRpt();
                allpatientlabrpt.Margin = new System.Windows.Forms.Padding(50, 0, 0, 0);

                allpatientlabrpt.Dock = DockStyle.Fill;
                this.panelMain.Controls.Add(allpatientlabrpt);
            }
            else if (type == "FASTRECIPELISTMODLES")
            {
                fastRecipeList = new FastRecipeListNew();
                fastRecipeList.Margin = new System.Windows.Forms.Padding(50, 0, 0, 0);
                fastRecipeList.Dock = DockStyle.Fill;
                fastRecipeList.SetBtnCloseFalse();
                fastRecipeList.currentDt = this.currentCureDate;
                fastRecipeList._currentPatientRow = this.currentPatientsRow;
                this.panelMain.Controls.Add(fastRecipeList);
            }
            else if (type == "EventInfoManager")
            {
                var managerUI = new HemoEventManager();
                managerUI.Dock = DockStyle.Fill;
                this.panelMain.Controls.Add(managerUI);
            }
            else if (type == "HemoOtherLogManager")
            {
                var managerUI = new HemoOtherLog();
                managerUI.Dock = DockStyle.Fill;
                this.panelMain.Controls.Add(managerUI);
            }
            else if (type == "EventInfoExtManager")
            {
                var managerUI = new HemoEventExtManager();
                managerUI.Dock = DockStyle.Fill;
                this.panelMain.Controls.Add(managerUI);
            }
        }

        protected internal override void OnTransitionCompleted()
        {
            base.OnTransitionCompleted();
            InitializeButtonPanel();
        }
        void InitializeButtonPanel()
        {
            //var listBI = new List<ButtonInfo>();
            //listBI.Add(new ButtonInfo() { Type = typeof(SimpleButton), Text = "新增", Name = "1", Image = ImageHelper.GetImageFromToolbarResource("NewBtn"), mouseEventHandler = btnAdd_Methond });
            //listBI.Add(new ButtonInfo() { Type = typeof(SimpleButton), Text = "制卡", Name = "2", Image = ImageHelper.GetImageFromToolbarResource("CardManager"), mouseEventHandler = btnMakeCardMethond });
            //listBI.Add(new ButtonInfo() { Type = typeof(SimpleButton), Text = "保存", Name = "3", Image = ImageHelper.GetImageFromToolbarResource("Save"), mouseEventHandler = btnSaveMethond });
            //listBI.Add(new ButtonInfo() { Type = typeof(SimpleButton), Text = "拍照", Name = "4", Image = ImageHelper.GetImageFromToolbarResource("MapView"), mouseEventHandler = btnPicture_Click });

            //listBI.Add(new ButtonInfo() { Type = typeof(SimpleButton), Text = "退出", Name = "5", Image = ImageHelper.GetImageFromToolbarResource("Edit"), mouseEventHandler = Exit });
            ////listBI.Add(new ButtonInfo());
            //BottomPanel.InitializeButtons(listBI, false);
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            MainFrm.viewModel.SelectModule(ModuleType.PatientMgr);
        }

        private void label1_Click(object sender, EventArgs e)
        {
            MainFrm.viewModel.SelectModule(ModuleType.PatientMgr);
        }

    }
}
