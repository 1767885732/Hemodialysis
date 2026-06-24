/*----------------------------------------------------------------
 * Copyright (C) 2005 医疗科技发展有限公司
 * 文件功能描述:用户控件
 * 创建标识:顾伟伟-2017年1月20日
 * 
 * 修改时间:2017年6月7日
 * 修改人:刘超
 * 修改描述:增加窗体控件值的方法
 * 
 * 修改时间:2017年7月9日
 * 修改人:吕志强
 * 修改描述:用户控件
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
using Hemo.Client.UI.Patient;

namespace Hemo.Client.Modules
{
    /// <summary>
    /// 病人卡片管理
    /// </summary>
    public partial class PatientCardMgr : BaseMoudleControl
    {
        #region 变量
        private PatientCardOperatorUI patientCardOperatorUi = null;
        private PatientModel.MED_PATIENTSRow currentPatientsRow { set; get; }
#endregion

        public PatientCardMgr()
        {
            InitializeComponent();
        }

        #region 方法
        /// <summary>
        /// 加载 用户控件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PatientCardMgr_Load(object sender, EventArgs e)
        {
            patientCardOperatorUi = new PatientCardOperatorUI();
            //patientCardOperatorUi.currentHemoId = currentPatientsRow.HEMODIALYSIS_ID;
            //patientCardOperatorUi.InzationData();
            patientCardOperatorUi.Dock = DockStyle.Fill;
            this.panelMain.Controls.Add(patientCardOperatorUi);
        }

        public void LoadPatientInfoMehtno(PatientModel.MED_PATIENTSRow patientsRow)
        {
            currentPatientsRow = patientsRow;
            patientCardOperatorUi.currentHemoId = currentPatientsRow.HEMODIALYSIS_ID;
            patientCardOperatorUi.InzationData();
        }

        protected internal override void OnTransitionCompleted()
        {
            base.OnTransitionCompleted();
            InitializeButtonPanel();
        }
        /// <summary>
        /// 初始化菜单
        /// </summary>
        void InitializeButtonPanel()
        {
            var listBI = new List<ButtonInfo>();
            listBI.Add(new ButtonInfo() { Type = typeof(SimpleButton), Text = "新增", Name = "1", Image = ImageHelper.GetImageFromToolbarResource("NewBtn"), mouseEventHandler = btnAdd_Methond });
            listBI.Add(new ButtonInfo() { Type = typeof(SimpleButton), Text = "制卡", Name = "2", Image = ImageHelper.GetImageFromToolbarResource("CardManager"), mouseEventHandler = btnMakeCardMethond });
            listBI.Add(new ButtonInfo() { Type = typeof(SimpleButton), Text = "保存", Name = "3", Image = ImageHelper.GetImageFromToolbarResource("Save"), mouseEventHandler = btnSaveMethond });
            listBI.Add(new ButtonInfo() { Type = typeof(SimpleButton), Text = "拍照", Name = "4", Image = ImageHelper.GetImageFromToolbarResource("MapView"), mouseEventHandler = btnPicture_Click });

            listBI.Add(new ButtonInfo() { Type = typeof(SimpleButton), Text = "退出", Name = "5", Image = ImageHelper.GetImageFromToolbarResource("Edit"), mouseEventHandler = Exit });
            listBI.Add(new ButtonInfo());
            BottomPanel.InitializeButtons(listBI, false);
        }


        #endregion
        #region 事件

        private void btnAdd_Methond(object sender, EventArgs e)
        {
            //patientInfoUi.btnAdd_Click(sender, e);
        }
        private void btnMakeCardMethond(object sender, EventArgs e)
        {
            //patientInfoUi.btnMakeCard_Click(sender, e);
        }
        private void btnSaveMethond(object sender, EventArgs e)
        {
            //patientInfoUi.btnSave_Click(sender, e);
        }
        private void btnPicture_Click(object sender, EventArgs e)
        {
            ////patientInfoUi.btnPicture_Click(sender, e);
        }
        private void Exit(object sender, EventArgs e)
        {
            MainFrm.viewModel.SelectModule(ModuleType.PatientMgr);
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            MainFrm.viewModel.SelectModule(ModuleType.PatientMgr);
        }

        private void label1_Click(object sender, EventArgs e)
        {
            MainFrm.viewModel.SelectModule(ModuleType.PatientMgr);
        }

        #endregion
    }
}
