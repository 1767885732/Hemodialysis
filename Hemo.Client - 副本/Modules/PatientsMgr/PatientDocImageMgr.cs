/*----------------------------------------------------------------
 * Copyright (C) 2005 医疗科技发展有限公司
 * 文件功能描述:增加窗体控件值的方法
 * 创建标识:刘超-2016年12月11日
 * 
 * 修改时间:2017年4月28日
 * 修改人:贺建操
 * 修改描述:增加窗体控件值的方法
 * 
 * 修改时间:2017年5月30日
 * 修改人:刘超
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
using Hemo.Client.Base.Services;

namespace Hemo.Client.Modules
{
    /// <summary>
    /// 电子扫描件管理
    /// </summary>
    public partial class PatientDocImageMgr : BaseMoudleControl
    {
        #region 变量

        public TaskViewModel ViewModel
        {
            get { return GetViewModel<TaskViewModel>(); }
        }

        private PatientDocImageUI patientImnUi = null;

        public PatientModel.MED_PATIENTSRow currentPatientsRow { set; get; }
        #endregion  
        #region 构造函数
        public PatientDocImageMgr()
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
        private void PatientDocImageMgr_Load(object sender, EventArgs e)
        {
            patientImnUi = new PatientDocImageUI(currentPatientsRow);
            this.LbTitle.Text = string.Format("患者-{0}的电子扫描件管理", currentPatientsRow.NAME);

            patientImnUi.Dock = DockStyle.Fill;
            this.panelMain.Controls.Add(patientImnUi);

        }

        public void CloseCurrentDocument()
        {
            ViewModel.Close();
        }
        
		 
	#endregion
    }
}
