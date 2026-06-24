/*----------------------------------------------------------------
// Copyright (C) 2005 苏州XX公司有限公司
// 描述：患者信息上传用户控件
// 创建时间：2015-04-13
// 创建者：刘超
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
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraEditors.Controls;
using Hemo.Service;
using Hemo.Model;
using Hemo.Client.UI.Hemodialysis;
using Hemo.Client.UI.Machine;
using Hemo.IService.DataReport;
using HEMODataReporter;
using HEMODataReporter.PostEntity;
using Hemo.Client.Core;
using Hemo.Utilities;
using Hemo.Client.Modules;
using HemoDataReporter.Data.Entity;

namespace Hemo.Client.UI.DataReport
{
    public partial class UploadPateientInfo : ViewBase
    {
        #region 私有成员
        /// <summary>
        /// 病人列表
        /// </summary>
        private DataReportModel.MED_PATIENTSDataTable _patientDataTable;
        private IDataReport objDataReport = ServiceManager.Instance.DataReportService;
        private DataReporter reporter = null;
        #endregion

        #region 初始化方法
        public UploadPateientInfo()
        {
            InitializeComponent();

            InzationData();
        }
        #endregion

        #region 各种事件

        private void btnQuery_Click(object sender, EventArgs e)
        {
            InzationData();
        }

        /// <summary>
        /// 对于 已上传的患者，双击进入明细 上传，自动打开明细
        /// 单机事件，进行选择上传
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gridView4_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            var dr = gridView4.GetFocusedDataRow() as DataReportModel.MED_PATIENTSRow;
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
                //双击已上传患者自动打开明细项目
                if (dr.UPSTATE == "已上传")
                {
                    //var form = this.Parent.FindForm() as DataReportManager;
                    var form = this.Parent.Parent.Parent.Parent as DataReportManagerMgr;
                    form._currentPatientRow = dr;
                    form.SetMenuVisble(true);
                    form.barButtonItem2_ItemClick(null, null);

                    form.barButtonItem5_ItemClick(null, null);

                    form.barButtonItem4_ItemClick(null, null);

                }
                else
                {
                    MessageBox.Show("当前患者未上传，请先上传患者信息才可以继续上传其它信息。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }

        }

        private void gridView4_CustomRowCellEdit(object sender, DevExpress.XtraGrid.Views.Grid.CustomRowCellEditEventArgs e)
        {
            if (e.Column == gridColumn15)
            {
                var curRow = (DataReportModel.MED_PATIENTSRow)gridView4.GetDataRow(e.RowHandle);
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
            MessageBox.Show("已上传不能再上传");
        }
        /// <summary>
        /// 过滤
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void radioGroupFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            var patientSource = new DataReportModel.MED_PATIENTSDataTable();
            switch (this.radioGroupFilter.SelectedIndex.ToString())
            {
                case "0":
                    _patientDataTable.CopyToDataTable<DataReportModel.MED_PATIENTSRow>(patientSource, LoadOption.PreserveChanges);
                    break;
                case "1":
                    _patientDataTable.Where(i => i.ISUPLOAD == "2").CopyToDataTable<DataReportModel.MED_PATIENTSRow>(patientSource, LoadOption.PreserveChanges);
                    break;
                case "2":
                    _patientDataTable.Where(i => i.ISUPLOAD == "1").CopyToDataTable<DataReportModel.MED_PATIENTSRow>(patientSource, LoadOption.PreserveChanges);
                    break;
                default:
                    break;
            }
            this.gridControl1.DataSource = patientSource;
        }
        /// <summary>
        /// 登录网站 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnLogin_Click(object sender, EventArgs e)
        {
            //登录事件
            reporter = HemoApplicationContext.Current.IsLoginHospitalHemoPlatForm;

            if (reporter != null && reporter.loginResult.Success)
            {
                AutoClosedMsgBox.Show("登录成功！", "提示", 5000, 0);
            }
            else
            {
                XtraMessageBox.Show("登录失败，请重新登录！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void checkAll_CheckedChanged(object sender, EventArgs e)
        {

            try
            {
                var dtSource = ((System.Data.DataView)(this.gridView4.DataSource)).Table as DataReportModel.MED_PATIENTSDataTable;
                foreach (DataReportModel.MED_PATIENTSRow row in dtSource.Rows)
                {
                    if (row.UPSTATE == "已上传")
                        continue;

                    row.ISUPLOAD = this.checkAll.Checked ? "1" : "0";

                }
            }
            catch (Exception ex) { }
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

        #region 数据方法

        //private void btnUpLoad_Click(object sender, EventArgs e)
        //{
        //    var dtSource = ((System.Data.DataView)(this.gridView4.DataSource)).Table as DataReportModel.MED_PATIENTSDataTable;
        //    var dt = new DataReportModel.MED_PATIENT_DATAREPORTDataTable();
        //    DataReporter reporter = HemoApplicationContext.Current.dataCurrentReport;// new DataReporter("hn002", "0");
        //    //做一个登录状态的判断，如果没有登录那么重新登录。 
        //    //做一个判断 
        //    if (reporter == null || !reporter.loginResult.Success)
        //    {
        //        reporter = HemoApplicationContext.Current.IsLoginHospitalHemoPlatForm;

        //        if (reporter != null && reporter.loginResult.Success)
        //        {
        //            AutoClosedMsgBox.Show("登录成功！", "提示", 5000, 0);
        //        }
        //        else
        //        {
        //            XtraMessageBox.Show("登录失败，请重新登录！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        //            return;
        //        }
        //    }
        //    foreach (DataReportModel.MED_PATIENTSRow row in dtSource.Rows)
        //    {
        //        if (row.ISUPLOAD == "1")
        //        {

        //            //去进行上传操作                   
        //            ReportResult result = reporter.ReportPatientBaseInfo(new PatientBaseInfoEntity(row.NAME, row.INPUT_CODE, row.CREDENTIALS_NUMBER, row.BIRTHDAY.ToString("yyyy-mm-dd"), row.CREATE_DATE.ToString("yyyy-mm-dd")) { Address = row.ADDRESS, BirthPlace = row.NATIVEPLACE, ContactDetail = row.TELEPHONE, Sex = row.SEX == "男" ? "1" : "2", ZStr1 = row.PATIENT_ID, ZStr2 = row.HEMODIALYSIS_ID, });
        //            if (result.Success)
        //            {
        //                var rowExtend = dt.NewMED_PATIENT_DATAREPORTRow();
        //                rowExtend.ID = Guid.NewGuid().ToString();
        //                rowExtend.HEMODIALYSIS_ID = row.HEMODIALYSIS_ID;
        //                rowExtend.BASEINFO = result.Info;
        //                rowExtend.STATE = "1";//成功
        //                rowExtend.TYPE = "0";
        //                rowExtend.EXTEND = "XTXX";
        //                rowExtend.EXTEND1 = "患者基本信息";
        //                rowExtend.EXTEND5="全国数据上报平台";
        //                rowExtend.UPTIME = System.DateTime.Now;
        //                rowExtend.UPUSER = HemoApplicationContext.Current.CurrentUser.USER_ID;
        //                rowExtend.MAPIP = row.HEMODIALYSIS_ID;
        //                dt.AddMED_PATIENT_DATAREPORTRow(rowExtend);
        //                break;
        //            }
        //            else
        //            {
        //                var rowExtend = dt.NewMED_PATIENT_DATAREPORTRow();
        //                rowExtend.ID = Guid.NewGuid().ToString();
        //                rowExtend.HEMODIALYSIS_ID = row.HEMODIALYSIS_ID;
        //                rowExtend.BASEINFO = result.Info;
        //                rowExtend.STATE = "0";//失败
        //                rowExtend.TYPE = "0";
        //                rowExtend.EXTEND = "XTXX";
        //                rowExtend.EXTEND1 = "患者基本信息";
        //                rowExtend.EXTEND5 = "全国数据上报平台";
        //                rowExtend.UPTIME = System.DateTime.Now;
        //                rowExtend.UPUSER = HemoApplicationContext.Current.CurrentUser.USER_ID;
        //                rowExtend.MAPIP = row.HEMODIALYSIS_ID;
        //                dt.AddMED_PATIENT_DATAREPORTRow(rowExtend);
        //            }
        //        }
        //    }
        //    var reINT = objDataReport.SavePatientIsUploadDt(dt);
        //    if (reINT > 0)
        //    {
        //        MessageBox.Show("成功");
        //    }
        //    else
        //    {
        //        MessageBox.Show("失败");
        //    }
        //}

        private void btnUpLoad_Click(object sender, EventArgs e)
        {
            var dtSource = ((System.Data.DataView)(this.gridView4.DataSource)).Table as DataReportModel.MED_PATIENTSDataTable;
            //var dt = new DataReportModel.MED_PATIENT_DATAREPORTDataTable();
            //DataReporter reporter = HemoApplicationContext.Current.dataCurrentReport;// new DataReporter("hn002", "0");
            ////做一个登录状态的判断，如果没有登录那么重新登录。 
            ////做一个判断 
            //if (reporter == null || !reporter.loginResult.Success)
            //{
            //    reporter = HemoApplicationContext.Current.IsLoginHospitalHemoPlatForm;

            //    if (reporter != null && reporter.loginResult.Success)
            //    {
            //        AutoClosedMsgBox.Show("登录成功！", "提示", 5000, 0);
            //    }
            //    else
            //    {
            //        XtraMessageBox.Show("登录失败，请重新登录！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //        return;
            //    }
            //}
            List<HemoDataReporter.Data.Entity.PatientBaseInfoEntity> datas = new List<HemoDataReporter.Data.Entity.PatientBaseInfoEntity>();
            foreach (DataReportModel.MED_PATIENTSRow row in dtSource.Rows)
            {
                if (row.ISUPLOAD == "1")
                {
                    HemoDataReporter.Data.Entity.PatientBaseInfoEntity baseInfo = new HemoDataReporter.Data.Entity.PatientBaseInfoEntity();
                    baseInfo.PrimaryID = row.PATIENT_ID;
                    baseInfo.diaoChaRiQi = row.SPECIFIC_TIME.GetDateString();

                    //首次肾脏替代治疗日期
                    baseInfo.shouCiShENzANGtIdAI = row.SPECIFIC_TIME.GetDateString();


                    baseInfo.chuangJianRiQi = row.CREATE_DATE.GetDateString();

                    baseInfo.name = row.NAME;
                    baseInfo.gender = row.SEX;

                 
                    baseInfo.zhengJianLeiXing = "身份证";
                    baseInfo.sys_cerId = row.CREDENTIALS_NUMBER;
                    baseInfo.minZu = row.NATION;

                    // 婚姻状态，已婚。离婚，未婚，丧婚 ?
                    if (row.MARITAL == "离异")
                        baseInfo.hunYinZhuangKuang = "离婚";
                    else
                        baseInfo.hunYinZhuangKuang = row.MARITAL;


                    baseInfo.age = (int)row.AGE;
                    baseInfo.birth = row.BIRTHDAY.ToString("yyyy-MM-dd");
                    baseInfo.jiaYuChengDu = row.EDUCATION;

                    baseInfo.zhiYe = row.JOB;
                    baseInfo.sys_patientNo_alter2 = "";
                    baseInfo.sys_patientNo_alter1 = row.ADMISSION_NUMBER;
                    baseInfo.touXiBingAnHao = row.HEMODIALYSIS_ID;

                    DateTime currentDate = DateTime.Now;
                    baseInfo.touXiLing = (currentDate.Year - row.SPECIFIC_TIME.Year) * 12 + (currentDate.Month - row.SPECIFIC_TIME.Month) + "";

                    string yibao = "";
                    if (row.MEDICAL_TYPE.Contains("医保"))
                        yibao = "基本医保";
                    else if (row.MEDICAL_TYPE.Contains("自费"))
                        yibao = "自费医疗";
                    else if ("新农合, 公费医疗, 商业保险,军队医疗".Contains(row.MEDICAL_TYPE))
                        yibao = row.MEDICAL_TYPE;
                    else
                    {
                        yibao = "其他";
                        baseInfo.otherfeiBie = row.MEDICAL_TYPE;
                    }

                    baseInfo.feiYongBaoXiao = new List<string> { yibao };
                    baseInfo.sys_address = row.ADDRESS;

                    baseInfo.lianXiRenXingMing = "";
                    baseInfo.lianXiRenDianHua = new lianXiRenDianHua() { };

                    baseInfo.lianXiRenShouJiHao = row.TELEPHONE;
                    datas.Add(baseInfo);
                }
            }
            System.Windows.Forms.SaveFileDialog saveFileDialog = new System.Windows.Forms.SaveFileDialog();
            saveFileDialog.FileName = "全国数据上报";
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                string strSaveFileLocation = saveFileDialog.FileName;//文件路径
                HemoDataReporter.Data.DataHelper.ExportBasicPatients(datas, saveFileDialog.FileName);
                MessageBox.Show("导出完成");
            }
        }


        /// <summary>
        /// 加载病人信息
        /// </summary>
        private void InzationData()
        {
            using (var _worker = new BackgroundWorker())
            {
                _patientDataTable = new DataReportModel.MED_PATIENTSDataTable();
                var patientSource = new DataReportModel.MED_PATIENTSDataTable();
                _worker.DoWork += delegate(object sender, DoWorkEventArgs e)
                {
                    //获取数据
                    _patientDataTable = objDataReport.GetDataReportPatientList();
                    //if (_patientDataTable.Columns.Contains("IS_UP"))
                    //{
                    //    _patientDataTable.Columns.Remove("IS_UP");
                    //}
                };
                _worker.RunWorkerCompleted += delegate(object sender1, RunWorkerCompletedEventArgs r1)
                {
                    //条件过滤
                    if (txtPatientName.Text.Trim().Length > 0 && txtHemoID.Text.Trim().Length > 0)
                    {
                        _patientDataTable.Where(i => (i.NAME.Contains(this.txtPatientName.Text.Trim()) || i.INPUT_CODE.ToUpper().Contains(this.txtPatientName.Text.Trim().ToUpper())) && i.HEMODIALYSIS_ID == this.txtHemoID.Text.Trim()).CopyToDataTable<DataReportModel.MED_PATIENTSRow>(patientSource, LoadOption.PreserveChanges);
                    }
                    else if (txtPatientName.Text.Trim().Length > 0 && txtHemoID.Text.Trim().Length == 0)
                    {
                        _patientDataTable.Where(i => (i.NAME.Contains(this.txtPatientName.Text.Trim()) || i.INPUT_CODE.ToUpper().Contains(this.txtPatientName.Text.Trim().ToUpper()))).CopyToDataTable<DataReportModel.MED_PATIENTSRow>(patientSource, LoadOption.PreserveChanges);
                    }
                    else if (txtPatientName.Text.Trim().Length == 0 && txtHemoID.Text.Trim().Length > 0)
                    {
                        _patientDataTable.Where(i => i.HEMODIALYSIS_ID == this.txtHemoID.Text.Trim()).CopyToDataTable<DataReportModel.MED_PATIENTSRow>(patientSource, LoadOption.PreserveChanges);
                    }
                    else
                    {
                        _patientDataTable.CopyToDataTable<DataReportModel.MED_PATIENTSRow>(patientSource, LoadOption.PreserveChanges);
                    }
                    this.gridControl1.DataSource = patientSource;
                };
                _worker.RunWorkerAsync();
            }
        }

        #endregion
    }
}