/*----------------------------------------------------------------
 * Copyright (C) 2005 医疗科技发展有限公司
 * 文件功能描述:修复系统响应速度慢的问题
 * 创建标识:贺建操-2016年11月27日
 * 
 * 修改时间:2017年4月14日
 * 修改人:贺建操
 * 修改描述:增加窗体控件值的方法
 * 
 * 修改时间:2017年5月16日
 * 修改人:贺建操
 * 修改描述:修复系统响应速度慢的问题
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
using Hemo.Client.Base.Services;

namespace Hemo.Client.Modules
{
    public partial class PatientEdit : BaseMoudleControl
    {
        #region 变量

        private PatientInfoUI patientInfoUi = null;

        public PatientModel.MED_PATIENTSRow currentPatientsRow { set; get; }


        public TaskViewModel ViewModel
        {
            get { return GetViewModel<TaskViewModel>(); }
        }

        #endregion
        #region 构造函数
        public PatientEdit()
        {
            InitializeComponent();
            base.viewModelCore = CreateViewModel<TaskViewModel>();

        }


        #endregion
        #region 方法

        public void LoadPatientInfoMehtno(PatientModel.MED_PATIENTSRow pRow)
        {
            currentPatientsRow = pRow;
            //patientInfoUi.Current = currentPatientsRow;
            //patientInfoUi.InitalizeData();
        }

        #endregion
        #region 事件

        private void PatientEdit_Load(object sender, EventArgs e)
        {
            patientInfoUi = new PatientInfoUI();
            patientInfoUi.Current = currentPatientsRow;
            patientInfoUi.InitalizeData();
            patientInfoUi.Dock = DockStyle.Fill;
            this.panelMain.Controls.Add(patientInfoUi);

        }

        public void CloseCurrentDocument()
        {
            ViewModel.Close();
        }

        protected internal override void OnTransitionCompleted()
        {

            base.OnTransitionCompleted();

            InitializeButtonPanel();
        }

        #endregion




        #region 暂时无用的东西啦.

        void InitializeButtonPanel()
        {
            return;
            ;
            var listBI = new List<ButtonInfo>();
            listBI.Add(new ButtonInfo() { Type = typeof(SimpleButton), Text = "新增", Name = "1", Image = ImageHelper.GetImageFromToolbarResource("NewBtn"), mouseEventHandler = btnAdd_Methond });
            listBI.Add(new ButtonInfo() { Type = typeof(SimpleButton), Text = "制卡", Name = "2", Image = ImageHelper.GetImageFromToolbarResource("CardManager"), mouseEventHandler = btnMakeCardMethond });
            listBI.Add(new ButtonInfo() { Type = typeof(SimpleButton), Text = "保存", Name = "3", Image = ImageHelper.GetImageFromToolbarResource("Save"), mouseEventHandler = btnSaveMethond });
            listBI.Add(new ButtonInfo() { Type = typeof(SimpleButton), Text = "拍照", Name = "4", Image = ImageHelper.GetImageFromToolbarResource("MapView"), mouseEventHandler = btnPicture_Click });

            listBI.Add(new ButtonInfo() { Type = typeof(SimpleButton), Text = "退出", Name = "5", Image = ImageHelper.GetImageFromToolbarResource("Edit"), mouseEventHandler = Exit });
            listBI.Add(new ButtonInfo());
            BottomPanel.InitializeButtons(listBI, false);
            //BottomPanel.Visible = false;
        }

        private void btnAdd_Methond(object sender, EventArgs e)
        {
            patientInfoUi.btnAdd_Click(sender, e);
        }
        private void btnMakeCardMethond(object sender, EventArgs e)
        {
            patientInfoUi.btnMakeCard_Click(sender, e);
        }
        private void btnSaveMethond(object sender, EventArgs e)
        {
            patientInfoUi.btnSave_Click(sender, e);
        }
        private void btnPicture_Click(object sender, EventArgs e)
        {
            patientInfoUi.btnPicture_Click(sender, e);
        }

        private void Exit(object sender, EventArgs e)
        {
            MainFrm.viewModel.SelectModule(ModuleType.PatientMgr);
        }

        private void label1_Click(object sender, EventArgs e)
        {
            MainFrm.viewModel.SelectModule(ModuleType.PatientMgr);
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            MainFrm.viewModel.SelectModule(ModuleType.PatientMgr);
        }


        #endregion

    }
}
