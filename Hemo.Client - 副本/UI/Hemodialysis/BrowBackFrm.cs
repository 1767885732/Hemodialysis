/*----------------------------------------------------------------
 * Copyright (C) 2005 (苏州)医疗科技发展有限公司
 * 文件功能描述:修改方法
 * 创建标识:贺建操-2013年7月3日
 * 
 * 修改时间:2013年10月11日
 * 修改人:顾伟伟
 * 修改描述:修改方法SQL
 * 
 * 修改时间:2014年1月19日
 * 修改人:吕志强
 * 修改描述:修改方法SQL
 * 
 * 修改时间:2014年4月29日
 * 修改人:顾伟伟
 * 修改描述:修改方法
 * ----------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Hemo.Model;
using Hemo.IService;
using Hemo.Service;
using Hemo.IService.Config;
using DevExpress.XtraEditors.Controls;
using Hemo.Utilities;
using Hemo.IService.Dict;
using Hemo.Client.Core;

namespace Hemo.Client.UI.Hemodialysis
{
    public partial class BrowBackFrm : HemoBaseFrm
    {

        #region 变量
        private IStaffDict _staffDictService = ServiceManager.Instance.StaffDictService;
        private IHemodialysis _hemoService = ServiceManager.Instance.HemodialysisService;
        #endregion
        #region 构造函数
        public BrowBackFrm()
        {
            InitializeComponent();
        }


        private void ShowSummary_Load(object sender, EventArgs e)
        {
            this.busyIndicator1.ShowLoadingScreenFor(this);
            DataTable dtStaffSict = null;

            using (BackgroundWorker worker = new BackgroundWorker())
            {
                worker.DoWork += delegate(object sender1, DoWorkEventArgs e1)
                {
                
                    dtStaffSict = _staffDictService.GetStaffDictList();

                };
                worker.RunWorkerCompleted += delegate(object sender2, RunWorkerCompletedEventArgs e2)
                {
                    txtBorrowDate.DateTime = DateTime.Now;

                    BaseControlInfo.BindLookUpEdit(lupCHECK_NURSE, "EMP_NO", "NAME", dtStaffSict, "NAME", "借出操作人");
                    lupCHECK_NURSE.EditValue = HemoApplicationContext.Current.CurrentUser.EMP_NO;
                    txtPatientName.Text = PatientName;
                    textBackInfo.Text = BackInfo;
                    this.busyIndicator1.HideLoadingScreen();
                };
                worker.RunWorkerAsync();
            }
        }
        #endregion

        #region 事件
        private void btnQuery_Click(object sender, EventArgs e)
        {
            if (txtBorrowDate.EditValue == null)
            {
                XtraMessageBox.Show("日期不能为空");
                return;
            }

            if (lupCHECK_NURSE.EditValue == null || lupCHECK_NURSE.EditValue.ToString().Trim() == "")
            {
                XtraMessageBox.Show("操作人填写错误");

                return;
            }

            _hemoService.SaveBorrowDataBack(BackID, txtBorrowDate.DateTime, string.Format("{0}({1})", lupCHECK_NURSE.Text, lupCHECK_NURSE.EditValue.ToString()), HemoApplicationContext.Current.CurrentUser.EMP_NO);
      
            this.Close();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {

            this.Close();
        }

        #endregion



        #region 变量
        public string PatientName { get; set; }

        public string BackInfo { get; set; }

        public string BackID { get; set; }
        #endregion
    }
}