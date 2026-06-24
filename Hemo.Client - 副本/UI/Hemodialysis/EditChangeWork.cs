/*----------------------------------------------------------------
 * Copyright (C) 2005 (苏州)医疗科技发展有限公司
 * 文件功能描述:工作变更
 * 创建标识:贺建操-2013年8月3日
 * 
 * 修改时间:2013年11月11日
 * 修改人:刘超
 * 修改描述:新增方法
 * 
 * 修改时间:2014年2月19日
 * 修改人:吕志强
 * 修改描述:新增方法SQL
 * 
 * 修改时间:2014年5月30日
 * 修改人:贺建操
 * 修改描述:修改方法SQL
 * ----------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Hemo.Model;
using Hemo.Utilities;
using Hemo.IService.Config;
using Hemo.Service;
using DevExpress.XtraEditors.Repository;
using Hemo.IService.Dict;
using Hemo.IService.PatientSchedule;
using Hemo.Client.Core;
using Hemo.IService;

namespace Hemo.Client.UI.Hemodialysis 
{
    public partial class EditChangeWork :HemoBaseFrm
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public EditChangeWork() 
        {
            InitializeComponent();
        }
        #region 变量
        private IConfig _configService = ServiceManager.Instance.ConfigService;
        private IStaffDict _staffDictService = ServiceManager.Instance.StaffDictService;
        private IPatientSchedule patientScheduleService = ServiceManager.Instance.PatientSchedule;
        private IPatient objPatient = ServiceManager.Instance.PatientService;
        private IHemodialysis _hemodialysis = ServiceManager.Instance.HemodialysisService;

        private HemoModel.MED_HEMO_CHAGEWORKRow _currentData = null;

        public HemoModel.MED_HEMO_CHAGEWORKRow CurrentData
        {
            get { return _currentData; }
            set { _currentData = value; }
        }   

        private HemoModel.MED_HEMO_CHAGEWORKDataTable _changeWorkHaving = null;

        public HemoModel.MED_HEMO_CHAGEWORKDataTable ChangeWorkHaving
        {
            get { return _changeWorkHaving; }
            set { _changeWorkHaving = value; }
        }
        public string currentArea
        {
            get;
            set;
        }

        private DateTime CurrentDt = new DateTime();
        private HemoModel.MED_HEMO_CHAGEWORKDataTable _changeWorkMaster = new HemoModel.MED_HEMO_CHAGEWORKDataTable();
        #endregion

        #region 方法
        public void InzationMaterialDate()
        {
            this.Enabled = false;
            using (BackgroundWorker worker = new BackgroundWorker())
            {
                _changeWorkMaster = new HemoModel.MED_HEMO_CHAGEWORKDataTable();
                worker.DoWork += delegate(object sender1, DoWorkEventArgs e1)
                {
                    #region CurrentData
                    
                    if (_currentData != null)
                    {
                        _changeWorkMaster.ImportRow(_currentData);
                       
                    }
                    else
                    {
                        _changeWorkMaster = new HemoModel.MED_HEMO_CHAGEWORKDataTable();   
                    }

                    #endregion

                };
                worker.RunWorkerCompleted += delegate(object sender2, RunWorkerCompletedEventArgs e2)
                {
                    this.MED_BINDING.DataSource = _changeWorkMaster;
                    if (_currentData == null)
                    {
                        this.MED_BINDING.AddNew();
                        this.txtID.Text = Guid.NewGuid().ToString();
                        this.dateEditShift.DateTime = CurrentDt;
                        this.lupNurse.EditValue = HemoApplicationContext.Current.CurrentUser.EMP_NO;
                        this.lupArea.EditValue = currentArea;
                        this.txtNewCount.Text = "0";
                        this.txtNoPairCount.Text = "0";
                        this.txtSalveCount.Text = "0";
                        this.txtUpTakeCount.Text = "0";
                        this.txtEcgCount.Text = "0";
                        this.txtDieCount.Text = "0";
                        this.txtCritical.Text = "0";
                        this.txtBloodCount.Text = "0";
                        dateEditShift.Enabled = true;

                    }
                    else
                    {
                        dateEditShift.Enabled = false;
                    }
              
                    this.Enabled = true;
                };
                worker.RunWorkerAsync();
            }

        }
        private void InzationControl()
        {
            this.lupArea.Properties.DataSource = this._configService.GetConfigList(string.Empty, string.Empty, "区域", "1");
            DataTable dtStaffSict = _staffDictService.GetStaffDictList();
            DataTable dtPunctureNurseList = Utility.GetSubTable(dtStaffSict, "ZYNAME='护士'", "name");
            this.lupNurse.Properties.DataSource = dtPunctureNurseList;
          
        }
        #endregion

        #region 事件


        #endregion
        private void btn_Save_Click(object sender, EventArgs e)
        {
            var dtParm = objPatient.GetChangeWorkByParm(lupArea.EditValue.ToString(), dateEditShift.DateTime.Date);
            if (dtParm != null && dtParm.Rows.Count > 0 && _currentData==null)
            {
                AutoClosedMsgBox.ShowForm("此信息已存在。", "提示", 2000, MessageBoxIcon.Information);
                return; 
            }
            this.xtraTabControl1.SelectedTabPage = this.xtraTabPage2;

            this.MED_BINDING.EndEdit();
            this.MED_BINDING.CurrencyManager.EndCurrentEdit();

            var rowMster = _changeWorkMaster[0];
            rowMster.CREATEBY = HemoApplicationContext.Current.CurrentUser.EMP_NO;
            rowMster.CONTENT = this.txtShiftContent.Text.Trim();
            rowMster.CREATEDATE = System.DateTime.Now;
            rowMster.TYPE = "0";//此标示为护士交班


            if (objPatient.SaveNurseChangeWork(_changeWorkMaster, new HemoModel.MED_HEMO_CHAGEWORK_EXTENDDataTable()) > 0)
            {
                AutoClosedMsgBox.ShowForm("保存成功!", "提示", 2000, MessageBoxIcon.Information);
              
                this.DialogResult = System.Windows.Forms.DialogResult.OK;
            }
            else
            {
                XtraMessageBox.Show("保存失败!");
            }
        }

        private void btn_Close_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
        }

        private void EditChangeWork_Load(object sender, EventArgs e)
        {
            InzationControl();
            InzationMaterialDate();
            CurrentDt = Utility.CDate(patientScheduleService.GetServerDate()).Date;

        }

        private void lupArea_EditValueChanged(object sender, EventArgs e)
        {
            if (this._currentData != null)
            {
                var dtChangeWork = _changeWorkHaving.FirstOrDefault(i => i.AREA == this.lupArea.EditValue.ToString() && i.CHANGETIME.Date == this.dateEditShift.DateTime.Date);
                if (dtChangeWork != null)
                {
                    this._currentData = dtChangeWork;
                    _changeWorkMaster = new HemoModel.MED_HEMO_CHAGEWORKDataTable();
                    _changeWorkMaster.ImportRow(dtChangeWork);
                    var content = this.txtShiftContent.Text.Trim();
                    var nameid = this.lupNurse.EditValue.ToString().Trim();
                   
                    this.MED_BINDING.DataSource = _changeWorkMaster;
                    if (content.Length > 0)
                    {
                        this.txtShiftContent.Text = content;
                    }
                    if (nameid.Length > 0)
                    {
                        this.lupNurse.EditValue = nameid;
                    }
                    var dt = objPatient.GetScheduleCoutByAreaID(this.lupArea.EditValue.ToString(), this.dateEditShift.DateTime.Date);
                    foreach (DataRow dr in dt.Rows)
                    {
                        switch (dr["ITEM_VALUE"].ToString())
                        {
                            case "1":
                                this.txtMCount.Text = dr["COUNT"].ToString();
                                break;
                            case "2":
                                this.txtACount.Text = dr["COUNT"].ToString();
                                break;
                            case "3":
                                this.txtECount.Text = dr["COUNT"].ToString();
                                break;
                            case "ALL":
                                this.txtAllCount.Text = dr["COUNT"].ToString();
                                break;
                            case "HD":
                                this.txtHdCount.Text = dr["COUNT"].ToString();
                                break;
                            case "HDF":
                                this.txtHdfCount.Text = dr["COUNT"].ToString();
                                break;
                            case "HD+HP":
                                this.txtHdpCount.Text = dr["COUNT"].ToString();
                                break;
                        }
                    }
                }
                else
                {
                    var dt = objPatient.GetScheduleCoutByAreaID(this.lupArea.EditValue.ToString(), this.dateEditShift.DateTime.Date);
                    foreach (DataRow dr in dt.Rows)
                    {
                        switch (dr["ITEM_VALUE"].ToString())
                        {
                            case "1":
                                this.txtMCount.Text = dr["COUNT"].ToString();
                                break;
                            case "2":
                                this.txtACount.Text = dr["COUNT"].ToString();
                                break;
                            case "3":
                                this.txtECount.Text = dr["COUNT"].ToString();
                                break;
                            case "ALL":
                                this.txtAllCount.Text = dr["COUNT"].ToString();
                                break;
                            case "HD":
                                this.txtHdCount.Text = dr["COUNT"].ToString();
                                break;
                            case "HDF":
                                this.txtHdfCount.Text = dr["COUNT"].ToString();
                                break;
                            case "HD+HP":
                                this.txtHdpCount.Text = dr["COUNT"].ToString();
                                break;
                        }
                    }
                    if (string.IsNullOrEmpty(this.txtNewCount.Text)) this.txtNewCount.Text = "0";
                    if (string.IsNullOrEmpty(this.txtNoPairCount.Text)) this.txtNoPairCount.Text = "0";
                    if (string.IsNullOrEmpty(this.txtSalveCount.Text)) this.txtSalveCount.Text = "0";
                    if (string.IsNullOrEmpty(this.txtUpTakeCount.Text)) this.txtUpTakeCount.Text = "0";
                    if (string.IsNullOrEmpty(this.txtEcgCount.Text)) this.txtEcgCount.Text = "0";
                    if (string.IsNullOrEmpty(this.txtDieCount.Text)) this.txtDieCount.Text = "0";
                    if (string.IsNullOrEmpty(this.txtCritical.Text)) this.txtCritical.Text = "0";   
                }
            }
            else
            {
                var dt = objPatient.GetScheduleCoutByAreaID(this.lupArea.EditValue.ToString(),this.dateEditShift.DateTime.Date);
                foreach (DataRow dr in dt.Rows)
                {
                    switch (dr["ITEM_VALUE"].ToString())
                    {
                        case "1":
                            this.txtMCount.Text = dr["COUNT"].ToString();
                            break;
                        case "2":
                            this.txtACount.Text = dr["COUNT"].ToString();
                            break;
                        case "3":
                            this.txtECount.Text = dr["COUNT"].ToString();
                            break;
                        case "ALL":
                            this.txtAllCount.Text = dr["COUNT"].ToString();
                            break;
                        case "HD":
                            this.txtHdCount.Text = dr["COUNT"].ToString();
                           break;
                        case "HDF":
                           this.txtHdfCount.Text = dr["COUNT"].ToString();
                           break;
                        case "HD+HP":
                           this.txtHdpCount.Text = dr["COUNT"].ToString();
                           break;
                    }
                }
                if(string.IsNullOrEmpty(this.txtNewCount.Text)) this.txtNewCount.Text = "0";
                if (string.IsNullOrEmpty(this.txtNoPairCount.Text)) this.txtNoPairCount.Text = "0";
                if (string.IsNullOrEmpty(this.txtSalveCount.Text)) this.txtSalveCount.Text = "0";
                if (string.IsNullOrEmpty(this.txtUpTakeCount.Text)) this.txtUpTakeCount.Text = "0";
                if (string.IsNullOrEmpty(this.txtEcgCount.Text)) this.txtEcgCount.Text = "0";
                if (string.IsNullOrEmpty(this.txtDieCount.Text)) this.txtDieCount.Text = "0";
                if (string.IsNullOrEmpty(this.txtCritical.Text)) this.txtCritical.Text = "0";   
            }
        }

        private void xtraTabControl1_SelectedPageChanged(object sender, DevExpress.XtraTab.TabPageChangedEventArgs e)
        {
            lupArea_EditValueChanged(null, null);
        }

        private void dateEditShift_EditValueChanged(object sender, EventArgs e)
        {
            if (this.dateEditShift.DateTime.Date == CurrentDt || this.dateEditShift.DateTime == DateTime.MinValue)
                return;

            CurrentDt = dateEditShift.DateTime.Date;
            var begionTime = CurrentDt.AddDays(1 - CurrentDt.Day);
            var endTime = CurrentDt.AddDays(1 - CurrentDt.Day).AddMonths(1).AddDays(-1);
            _changeWorkHaving = objPatient.GetChangeNurseWorkByDate(begionTime, endTime);

            InzationMaterialDate();


        }
    }
}