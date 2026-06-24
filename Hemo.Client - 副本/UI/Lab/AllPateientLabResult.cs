/*----------------------------------------------------------------
// Copyright (C) 2005 苏州XX公司有限公司
// 描述：患者检验信息同步用户控件类
// 创建时间：2016-8-22
// 创建者：贺建操
//  
// 修改时间：
// 修改人：
// 修改描述：
----------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraEditors.Controls;
using Hemo.Service;
using Hemo.Model;
using Hemo.Client.UI.Hemodialysis;
using Hemo.Client.UI.Machine;
using Hemo.Client.Core;
using Hemo.Utilities;
using Hemo.Client.Modules;
using Hemo.IService;
using DevExpress.XtraSplashScreen;
using Hemo.Client.Controls;

namespace Hemo.Client.UI.Lab
{
    public partial class AllPateientLabResult : ViewBase
    {
        #region 私有成员
        /// <summary>
        /// 病人列表
        /// </summary>
        private PatientModel.MED_PATIENTSDataTable _patientDataTable;
        private IPatient objPatientService = ServiceManager.Instance.PatientService;
        private string interFaceUril = string.Empty;
        #endregion

        #region 属性

        private SplashScreenManager _loadForm;
        /// <summary>
        /// 等待窗体管理对象
        /// </summary>
        protected SplashScreenManager LoadForm
        {
            get
            {
                if (_loadForm == null)
                {
                    this._loadForm = new SplashScreenManager(new Form(), typeof(FrmWaitForm), true, true);
                    //this._loadForm.CloseWaitForm();.ClosingDelay = 0;
                }
                return _loadForm;
            }
        }

        #endregion

        #region 构造函数
        public AllPateientLabResult()
        {
            InitializeComponent();

            InzationData();
        }
        #endregion

        #region 事件

        private void btnQuery_Click(object sender, EventArgs e)
        {
            InzationData();
            this.gridView4.BestFitColumns();
        }

        private void btnQuery_Click_1(object sender, EventArgs e)
        {
            InzationData();
        }

        private void btnUpLoad_Click(object sender, EventArgs e)
        {
            //SplashScreen1 frm = new SplashScreen1();
            DevExpress.XtraSplashScreen.SplashScreenManager.ShowForm(this.ParentForm.FindForm(), typeof(SplashScreen1));

            //ShowMessage();
            var dtSource = ((System.Data.DataView)(this.gridView4.DataSource)).Table as PatientModel.MED_PATIENTSDataTable;

            string LabPatientErrorTemp = string.Empty;
            string examPatientErrorTemp = string.Empty;

            string LabPatientError = string.Empty;
            string examPatientError = string.Empty;

            string LabPatientSucess = string.Empty;
            string examPatientSucess = string.Empty;

            int successLabInt = 0;
            int successExamInt = 0;

            int ErrorLabInt = 0;
            int ErrorExamInt = 0;
            int patientCount = 0;
            var item = dtSource.Where(i => i.ISUPLOAD == "1");
            var AllCount = item.Count();
            foreach (PatientModel.MED_PATIENTSRow row in dtSource.Rows)
            {
                if (row.ISUPLOAD == "1")
                {
                    patientCount++;
                    DevExpress.XtraSplashScreen.SplashScreenManager.Default.SendCommand(SplashScreen1.SplashScreenCommand.SetText, string.Format("共{0}个患者，正在同步第{1}个患者：{2}的数据，请稍等..", AllCount, patientCount, row.NAME));
                    try
                    {
                        LabPatientErrorTemp = InterfaceUtility.SynchronizeLabDatasByPatiets(row.PATIENT_ID, interFaceUril);
                    }
                    catch (Exception er)
                    {
                        LabPatientError += er.Message;
                    }
                    try
                    {
                        examPatientErrorTemp = InterfaceUtility.SynchronizeExamDatasByPatientId(row.PATIENT_ID, interFaceUril);
                    }
                    catch (Exception er)
                    {
                        examPatientError += er.Message;
                    }


                    if (!string.IsNullOrEmpty(LabPatientErrorTemp))
                    {
                        ErrorLabInt++;
                        LabPatientError += string.Format("透析编号为:{0} 姓名为:{1}  返回错误：{2}\r\n", row.HEMODIALYSIS_ID, row.NAME, LabPatientErrorTemp);
                    }
                    else
                    {
                        successLabInt++;
                        LabPatientSucess += string.Format("透析编号为:{0} 姓名为:{1}##", row.HEMODIALYSIS_ID, row.NAME);

                    }

                    if (!string.IsNullOrEmpty(examPatientErrorTemp))
                    {
                        ErrorExamInt++;
                        examPatientError += string.Format("透析编号为:{0} 姓名为:{1}   返回错误：{2}\r\n", row.HEMODIALYSIS_ID, row.NAME, examPatientErrorTemp);
                    }
                    else
                    {
                        successExamInt++;
                        LabPatientSucess += string.Format("透析编号为:{0} 姓名为:{1}##", row.HEMODIALYSIS_ID, row.NAME);
                    }
                }
                LabPatientErrorTemp = string.Empty;
                examPatientErrorTemp = string.Empty;
            }

            this.txtMemo.Visible = true;

            string coutStr = string.Format("检验成功人数：{0} 检验失败人数:{1}\r\n     检查成功人数:{2} 检查失败人数：{3}", successLabInt.ToString(), ErrorLabInt.ToString(), successExamInt.ToString(), ErrorExamInt.ToString());
            this.txtMemo.Text = string.Format("接口地址 ：：：{0}\r\n{1}\r\n$$$$$$$$$$$$$$$$$$$检验失败人员信息$$$$$$$$$$$$$$$$$$$\r\n{2}\r\n$$$$$$$$$$$$$$$$$$$检查失败人员$$$$$$$$$$$$$$$$$$$\r\n{3}\r\n$$$$$$$$$$$$$$$$$$$检验成功人员信息$$$$$$$$$$$$$$$$$$$\r\n{4}\r\n$$$$$$$$$$$$$$$$$$$检查成功人员$$$$$$$$$$$$$$$$$$$\r\n{5}", interFaceUril, coutStr, LabPatientError, examPatientError, LabPatientSucess, examPatientSucess);

            Medicalsystem.Docare.Logging.Logger.Write(this.txtMemo.Text);
            //HideMessage();
            DevExpress.XtraSplashScreen.SplashScreenManager.CloseForm();

        }

        void checkAll_CheckedChanged(object sender, System.EventArgs e)
        {
            var dtSource = ((System.Data.DataView)(this.gridView4.DataSource)).Table as PatientModel.MED_PATIENTSDataTable;
            foreach (PatientModel.MED_PATIENTSRow row in dtSource.Rows)
            {
                row.ISUPLOAD = this.checkAll.Checked ? "1" : "0";

            }
        }

        /// <summary>
        /// 对于 已上传的患者，双击进入明细 上传，自动打开明细
        /// 单机事件，进行选择上传
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gridView4_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            var dr = gridView4.GetFocusedDataRow() as PatientModel.MED_PATIENTSRow;
            if (dr == null)
                return;
            if (e.Clicks == 1)
            {
                if (dr.ISUPLOAD == "1")
                {
                    dr.ISUPLOAD = "0";
                }
                else if (dr.ISUPLOAD == "0")
                {
                    dr.ISUPLOAD = "1";
                }
                else
                {

                }
            }
            else if (e.Clicks == 2)
            {
            }

        }

        private void gridView4_CustomRowCellEdit(object sender, DevExpress.XtraGrid.Views.Grid.CustomRowCellEditEventArgs e)
        {
            if (e.Column == gridColumn15)
            {
                var curRow = (PatientModel.MED_PATIENTSRow)gridView4.GetDataRow(e.RowHandle);
                if (curRow == null)
                    return;
                if (curRow.ISUPLOAD == "2")
                {
                    var cloneRepository = e.RepositoryItem.Clone() as DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit;
                    cloneRepository.Click += new EventHandler(cloneRepository_Click);
                    cloneRepository.ReadOnly = true;
                    e.RepositoryItem = cloneRepository;
                }
            }
        }

        void cloneRepository_Click(object sender, EventArgs e)
        {
            MessageBox.Show("禁止");
        }

        /// <summary>
        /// 文本框事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtPatientName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                btnQuery_Click(null, null);
            }
        }

        #endregion

        #region 方法

        /// <summary>
        /// 加载病人信息
        /// </summary>
        private void InzationData()
        {
            this.txtMemo.Visible = false;
            ShowMessage();
            interFaceUril = HemoApplicationContext.Current.InterFaceDate.FirstOrDefault(i => i.ITEM_NAME == "血透同步数据接口").ITEM_VALUE.ToString();
            using (var _worker = new BackgroundWorker())
            {
                _patientDataTable = new PatientModel.MED_PATIENTSDataTable();
                var patientSource = new PatientModel.MED_PATIENTSDataTable();
                _worker.DoWork += delegate(object sender, DoWorkEventArgs e)
                {
                    //获取数据
                    _patientDataTable = objPatientService.GetPatientList();

                };
                _worker.RunWorkerCompleted += delegate(object sender1, RunWorkerCompletedEventArgs r1)
                {
                    foreach (PatientModel.MED_PATIENTSRow RowItem in _patientDataTable)
                    {
                        RowItem.ISUPLOAD = "0";
                        RowItem.AcceptChanges();

                    }
                    _patientDataTable.AcceptChanges();
                    //条件过滤
                    if (txtPatientName.Text.Trim().Length > 0 && txtHemoID.Text.Trim().Length > 0)
                    {
                        _patientDataTable.Where(i => (i.NAME.Contains(this.txtPatientName.Text.Trim()) || i.INPUT_CODE.ToUpper().Contains(this.txtPatientName.Text.Trim().ToUpper())) && i.HEMODIALYSIS_ID == this.txtHemoID.Text.Trim()).CopyToDataTable<PatientModel.MED_PATIENTSRow>(patientSource, LoadOption.PreserveChanges);
                    }
                    else if (txtPatientName.Text.Trim().Length > 0 && txtHemoID.Text.Trim().Length == 0)
                    {
                        _patientDataTable.Where(i => (i.NAME.Contains(this.txtPatientName.Text.Trim()) || i.INPUT_CODE.ToUpper().Contains(this.txtPatientName.Text.Trim().ToUpper()))).CopyToDataTable<PatientModel.MED_PATIENTSRow>(patientSource, LoadOption.PreserveChanges);
                    }
                    else if (txtPatientName.Text.Trim().Length == 0 && txtHemoID.Text.Trim().Length > 0)
                    {
                        _patientDataTable.Where(i => i.HEMODIALYSIS_ID == this.txtHemoID.Text.Trim()).CopyToDataTable<PatientModel.MED_PATIENTSRow>(patientSource, LoadOption.PreserveChanges);
                    }
                    else
                    {
                        _patientDataTable.CopyToDataTable<PatientModel.MED_PATIENTSRow>(patientSource, LoadOption.PreserveChanges);
                    }
                    this.gridControl1.DataSource = patientSource;
                    HideMessage();
                };
                _worker.RunWorkerAsync();
            }
        }

        /// <summary>
        /// 显示等待窗体
        /// </summary>
        public void ShowMessage()
        {
            bool flag = !this.LoadForm.IsSplashFormVisible;
            if (flag)
            {
                this.LoadForm.ShowWaitForm();
            }
        }
        /// <summary>
        /// 关闭等待窗体
        /// </summary>
        public void HideMessage()
        {
            bool isSplashFormVisible = this.LoadForm.IsSplashFormVisible;
            if (isSplashFormVisible)
            {
                this.LoadForm.CloseWaitForm();
            }
        }

        #endregion

        private void BtnDepart_Click(object sender, EventArgs e)
        {
            DevExpress.XtraSplashScreen.SplashScreenManager.ShowForm(this.ParentForm.FindForm(), typeof(SplashScreen1));
            interFaceUril = HemoApplicationContext.Current.InterFaceDate.FirstOrDefault(i => i.ITEM_NAME == "血透同步数据接口").ITEM_VALUE.ToString();
            using (var _worker = new BackgroundWorker())
            {
                string ErrorTemp = string.Empty;
                DevExpress.XtraSplashScreen.SplashScreenManager.Default.SendCommand(SplashScreen1.SplashScreenCommand.SetText, "正在同步部门数据，请稍等..");

                _worker.DoWork += delegate(object sender2, DoWorkEventArgs e2)
                   {
                       ErrorTemp = InterfaceUtility.SynchronizeDictDeptment(interFaceUril);

                   };
                _worker.RunWorkerCompleted += delegate(object sender1, RunWorkerCompletedEventArgs r1)
                {
                    if (!string.IsNullOrEmpty(ErrorTemp))
                    {
                        DevExpress.XtraSplashScreen.SplashScreenManager.Default.SendCommand(SplashScreen1.SplashScreenCommand.SetText, ErrorTemp);

                    }
                    DevExpress.XtraSplashScreen.SplashScreenManager.CloseForm();

                };
                _worker.RunWorkerAsync();
            }
        }
    }
}