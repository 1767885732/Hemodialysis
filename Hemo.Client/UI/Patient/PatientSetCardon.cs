/*----------------------------------------------------------------
// Copyright (C) 2005 苏州XX公司
// 描述：患者警戒线设置窗体
// 创建时间：2016-6-20
// 创建者：刘超
//  
// 修改时间：
// 修改人：
// 修改描述：
//
----------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Hemo.IService.Config;
using Hemo.Service;
using Hemo.Model;
using Hemo.IService;
using Hemo.Utilities;
using Hemo.Client.Core;

namespace Hemo.Client.UI.Patient
{
    public partial class PatientSetCardon : HemoBaseFrm
    {
        #region 类变量

        private IConfig _configService = ServiceManager.Instance.ConfigService;

        private IPatient _patientService = ServiceManager.Instance.PatientService;

        private HemoModel.MED_HEMO_CORDONDataTable _data = null;

        public PatientModel.MED_PATIENTSDataTable _patientDataTable;

        #endregion

        #region 属性

        #endregion

        #region 构造函数

        public PatientSetCardon()
        {
            InitializeComponent();
        }

        #endregion

        #region 事件

        private void btnQuery_Click(object sender, EventArgs e)
        {
            InzationData();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(this.lupArea.EditValue.ToString()) && string.IsNullOrEmpty(this.gridLookPatient.Text.ToString()))
            {
                foreach (HemoModel.MED_HEMO_CORDONRow row in _data.Rows)
                {
                    if (row.AREAID != this.lupArea.EditValue.ToString())
                        continue;
                    row.CORDON = Utility.CDecimal(txtCorDon.Text);
                    row.CREATEBY = HemoApplicationContext.Current.CurrentUser.EMP_NO;
                    row.CREATENAME = HemoApplicationContext.Current.CurrentUser.USER_NAME;
                    row.CREATEDATE = System.DateTime.Now;
                }

            }
            else if (!string.IsNullOrEmpty(this.lupArea.EditValue.ToString()) && !string.IsNullOrEmpty(this.gridLookPatient.Text.ToString()))
            {
                var dr = gridView1.GetFocusedDataRow() as HemoModel.MED_HEMO_CORDONRow;
                if (dr != null)
                {
                    dr.CORDON = Utility.CDecimal(txtCorDon.Text);
                    dr.CREATEBY = HemoApplicationContext.Current.CurrentUser.EMP_NO;
                    dr.CREATENAME = HemoApplicationContext.Current.CurrentUser.USER_NAME;
                    dr.CREATEDATE = System.DateTime.Now;
                }
            }
            else if (string.IsNullOrEmpty(this.lupArea.EditValue.ToString()) && !string.IsNullOrEmpty(this.gridLookPatient.Text.ToString()))
            {
                var dr = gridView1.GetFocusedDataRow() as HemoModel.MED_HEMO_CORDONRow;
                if (dr != null)
                {
                    dr.CORDON = Utility.CDecimal(txtCorDon.Text);
                    dr.CREATEBY = HemoApplicationContext.Current.CurrentUser.EMP_NO;
                    dr.CREATENAME = HemoApplicationContext.Current.CurrentUser.USER_NAME;
                    dr.CREATEDATE = System.DateTime.Now;
                }
            }
            else
            {
                foreach (HemoModel.MED_HEMO_CORDONRow row in _data.Rows)
                {
                    row.CORDON = Utility.CDecimal(txtCorDon.Text);
                    row.CREATEBY = HemoApplicationContext.Current.CurrentUser.EMP_NO;
                    row.CREATENAME = HemoApplicationContext.Current.CurrentUser.USER_NAME;
                    row.CREATEDATE = System.DateTime.Now;
                }
            }
            this.gridView1.ClearColumnErrors();
            this.bindingSource1.EndEdit();
            this.bindingSource1.CurrencyManager.EndCurrentEdit();

            foreach (HemoModel.MED_HEMO_CORDONRow row in _data.Rows)
            {
                if (row.IsCORDONNull() || string.IsNullOrEmpty(row.CORDON.ToString()) || row.CORDON == 0)
                {
                    row.CORDON = 0;
                }
                row.CREATEBY = HemoApplicationContext.Current.CurrentUser.EMP_NO;
                row.CREATENAME = HemoApplicationContext.Current.CurrentUser.USER_NAME;
                row.CREATEDATE = System.DateTime.Now;
            }
            //保存
            if (this._patientService.SaveHemoPatientCorDon(_data) > 0)
            {
                AutoClosedMsgBox.ShowForm("保存成功。", "系统提示", 1500, MessageBoxIcon.Information);
            }
        }

        private void gridView1_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            var dr = gridView1.GetFocusedDataRow() as HemoModel.MED_HEMO_CORDONRow;
            if (dr != null)
            {
                this.lupArea.EditValue = dr.AREAID;
                this.gridLookPatient.EditValue = dr.HEMODIALYSIS_ID;
                this.txtCorDon.Text = dr.CORDON.ToString();
            }
        }

        private void PatientSetCardon_Load(object sender, EventArgs e)
        {
            InzationData();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        #endregion

        #region 方法

        private void InzationData()
        {
            using (var _work = new BackgroundWorker())
            {
                var filterDate = new HemoModel.MED_HEMO_CORDONDataTable();
                var areaItems = new ConfigModel.MED_COMMON_ITEMLISTDataTable();
                _data = new HemoModel.MED_HEMO_CORDONDataTable();
                _work.DoWork += delegate(object sender, DoWorkEventArgs e)
                {
                    filterDate = _patientService.GetHemoPatientCorDon();
                    this._patientDataTable = this._patientService.GetPatientList();
                    areaItems = this._configService.GetConfigList(string.Empty, string.Empty, "区域", "1");


                };
                _work.RunWorkerCompleted += delegate(object sender1, RunWorkerCompletedEventArgs e1)
                {
                    var row = areaItems.NewMED_COMMON_ITEMLISTRow();
                    row.ITEM_ID = "9999";
                    row.ITEM_VALUE = "9999";
                    row.ITEM_NAME = "其它";
                    areaItems.AddMED_COMMON_ITEMLISTRow(row);
                    var row1 = areaItems.NewMED_COMMON_ITEMLISTRow();
                    row1.ITEM_ID = "1111";
                    row1.ITEM_VALUE = "1111";
                    row1.ITEM_NAME = "全部";
                    areaItems.Rows.InsertAt(row1, 0);
                    this.lupArea.Properties.DataSource = areaItems;

                    var orowN = _patientDataTable.NewMED_PATIENTSRow();

                    orowN.ItemArray = _patientDataTable[0].ItemArray;
                    orowN.NAME = "";
                    orowN.HEMODIALYSIS_ID = "";
                    _patientDataTable.Rows.InsertAt(orowN, 0);

                    this.gridLookPatient.Properties.DataSource = this._patientDataTable;
                    if (this.gridLookPatient.EditValue == null || string.IsNullOrEmpty(this.gridLookPatient.Text))
                        this.gridLookPatient.EditValue = string.Empty;


                    if (!string.IsNullOrEmpty(lupArea.EditValue.ToString()) && !string.IsNullOrEmpty(this.gridLookPatient.EditValue.ToString()))
                    {
                        string areaid = string.Empty;
                        if (lupArea.EditValue.ToString() != "9999")
                        {
                            areaid = lupArea.EditValue.ToString();
                        }
                        if (areaid == "1111")
                        {
                            filterDate.Where(i => i.HEMODIALYSIS_ID == this.gridLookPatient.EditValue.ToString().Trim()).CopyToDataTable<HemoModel.MED_HEMO_CORDONRow>(_data, LoadOption.PreserveChanges);

                        }
                        else
                        {
                            filterDate.Where(i => i.AREAID == areaid && i.HEMODIALYSIS_ID == this.gridLookPatient.EditValue.ToString().Trim()).CopyToDataTable<HemoModel.MED_HEMO_CORDONRow>(_data, LoadOption.PreserveChanges);
                        }
                    }
                    else if (!string.IsNullOrEmpty(lupArea.EditValue.ToString()) && string.IsNullOrEmpty(this.gridLookPatient.EditValue.ToString()))
                    {
                        string areaid = string.Empty;
                        if (lupArea.EditValue.ToString() != "9999")
                        {
                            areaid = lupArea.EditValue.ToString();
                        }
                        if (areaid == "1111")
                        {
                            filterDate.CopyToDataTable<HemoModel.MED_HEMO_CORDONRow>(_data, LoadOption.PreserveChanges);
                        }
                        else
                        {
                            filterDate.Where(i => i.AREAID == areaid).CopyToDataTable<HemoModel.MED_HEMO_CORDONRow>(_data, LoadOption.PreserveChanges);
                        }
                    }
                    else if (string.IsNullOrEmpty(lupArea.EditValue.ToString()) && !string.IsNullOrEmpty(this.gridLookPatient.EditValue.ToString()))
                    {
                        filterDate.Where(i => i.HEMODIALYSIS_ID == this.gridLookPatient.EditValue.ToString()).CopyToDataTable<HemoModel.MED_HEMO_CORDONRow>(_data, LoadOption.PreserveChanges);
                    }
                    else
                    {
                        filterDate.CopyToDataTable<HemoModel.MED_HEMO_CORDONRow>(_data, LoadOption.PreserveChanges);
                    }
                    this.bindingSource1.DataSource = _data;
                    this.lupArea.EditValue = "1111";
                    this.gridLookPatient.EditValue = "";

                };
                _work.RunWorkerAsync();
            }
        }

        #endregion
    }
}
