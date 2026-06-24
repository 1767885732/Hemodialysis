/*----------------------------------------------------------------
// Copyright (C) 2005 苏州XX公司有限公司
// 描述：透析随访记录单登记用户控件类
// 创建时间：2014-09-11
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
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevExpress.Xpf.Grid;
using Hemo.Model;
using Hemo.Service;
using DevExpress.XtraEditors;
using Hemo.IService.Dict;
using Hemo.Utilities;
using Hemo.Client.Print;
using Hemo.Client.UI.Machine;
using Hemo.IService;
using Hemo.IService.Config;
using Hemo.Client.Core;
using Hemo.IService.PatientSchedule;

namespace Hemo.Client.UI.FollowUp
{
      [ToolboxItem(true)]
    public partial class FollowUpControl : ViewBase
    {
        #region 类变量

        private IMaterial objMaterial = ServiceManager.Instance.MaterialService;
        private IPatientSchedule patientScheduleService = ServiceManager.Instance.PatientSchedule;

        private DrugModel.MED_PATIENT_FOLLOWUPDataTable _dataTable = null;

        private DrugModel.MED_PATIENT_FOLLOWUPRow _currentData;

        public bool isFromDatabase = false;

        #endregion

        #region 属性

        public DrugModel.MED_PATIENT_FOLLOWUPRow CurrentData
        {
            get { return _currentData; }
            set { _currentData = value; }
        }

        #endregion

        #region 构造函数

        public FollowUpControl()
        {
            InitializeComponent();
        }

        #endregion

        #region 方法

        public void InzationMaterialDate()
        {
            this.Enabled = false;
            using (BackgroundWorker worker = new BackgroundWorker())
            {
                _dataTable = new DrugModel.MED_PATIENT_FOLLOWUPDataTable();
                worker.DoWork += delegate(object sender1, DoWorkEventArgs e1)
                {
                    if (_currentData != null)
                    {
                        if (isFromDatabase)
                        {
                            _dataTable.ImportRow(_currentData);
                        }
                        else
                        {
                            var row = _dataTable.NewMED_PATIENT_FOLLOWUPRow();
                            row.ID = _currentData.ID;
                            row.HEMODIALYSIS_ID = _currentData.HEMODIALYSIS_ID;
                            row.PATIENT = _currentData.PATIENT;
                            row.NAME = _currentData.NAME;
                            row.FOLLOWDATE = System.DateTime.Now;
                            row.FOLLOWYEAR = System.DateTime.Now.Year.ToString();
                            row.FOLLOWMOTH = System.DateTime.Now.Month.ToString();
                            row.FOLLOWDAY = System.DateTime.Now.Day.ToString();
                            row.DOCNAME = LoginUser.User.USER_NAME.ToString();
                            _dataTable.AddMED_PATIENT_FOLLOWUPRow(row);
                        }
                    }
                    else
                    {
                        _dataTable = new DrugModel.MED_PATIENT_FOLLOWUPDataTable();
                    }
                };
                worker.RunWorkerCompleted += delegate(object sender2, RunWorkerCompletedEventArgs e2)
                {
                    this.HemoDateBindings.DataSource = _dataTable;
                    if (_currentData == null)
                    {
                        this.HemoDateBindings.AddNew();
                        this.txt_ID.Text = Guid.NewGuid().ToString();
                    }
                    else
                    {
                        this.lbl_Title.Text = string.Format("{0}透析随访记录单", _currentData.NAME);
                    }
                    this.Enabled = true;
                };
                worker.RunWorkerAsync();
            }

        }

        public int SaveData()
        {
            this.HemoDateBindings.EndEdit();
            this.HemoDateBindings.CurrencyManager.EndCurrentEdit();

            var row = _dataTable[0];
            row.DOCNAME = LoginUser.User.USER_NAME.ToString();
            row.FOLLOWDATE = Convert.ToDateTime(string.Format("{0}-{1}-{2}", ltxt_Year.Text, lTxt_Moth.Text, lTxt_Day.Text));
            row.CREATEDATE = Utility.CDate(patientScheduleService.GetServerDate());
            return objMaterial.SaveFollowUp(_dataTable);
        }

        #endregion
    }
}
