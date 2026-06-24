/*----------------------------------------------------------------
 * Copyright (C) 2005 (苏州)医疗科技发展有限公司
 * 文件功能描述:治疗模板
 * 创建标识:贺建操-2013年7月9日
 * 
 * 修改时间:2013年10月17日
 * 修改人:顾伟伟
 * 修改描述:修改方法SQL
 * 
 * 修改时间:2014年1月25日
 * 修改人:贺建操
 * 修改描述:修改方法SQL
 * 
 * 修改时间:2014年5月5日
 * 修改人:顾伟伟
 * 修改描述:新增方法
 * ----------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Hemo.IService.Config;
using Hemo.IService.Dict;
using Hemo.Service;
using Hemo.Utilities;
using Hemo.Model;
using Hemo.Client.Core;

namespace Hemo.Client.UI.Hemodialysis
{
    public partial class CureDrugTemplateList : HemoBaseFrm
    {
        #region 变量
        private IConfig _configService = ServiceManager.Instance.ConfigService;
        private IStaffDict _staffDictService = ServiceManager.Instance.StaffDictService;
        private IHemodialysis _hemoService = ServiceManager.Instance.HemodialysisService;
        private HemodialysisModel.MED_CURE_DRUG_TEMPLATEDataTable _currentData = null;
        private bool _isLong = false;
        private string recipeId;
        #endregion
        #region 公共变量
        public string RecipeId
        {
            get { return recipeId; }
            set { recipeId = value; }
        }

        public bool IsLong
        {
            get { return _isLong; }
            set { _isLong = value; }
        }

        public string CurrentHemoID
        {
            get;
            set;
        }

        public string CurrentPatientID
        {
            get;
            set;
        }
        #endregion
        #region 构造函数
        public CureDrugTemplateList()
        {
            InitializeComponent();


            #region  LOOKUPEDIT初始化
            this.repositoryItemLookUpEdit1.DataSource = this._configService.GetConfigList(string.Empty, string.Empty, "药品单位", "1");
            this.repositoryItemLookUpEdit2.DataSource = this._configService.GetConfigList(string.Empty, string.Empty, "注射方式", "1");


            DataTable dtStaffSict = _staffDictService.GetStaffDictList();
            DataTable dtDoctorList = Utility.GetSubTable(dtStaffSict, "ZYNAME='医生'");
            if (dtDoctorList != null && dtDoctorList.Rows.Count > 0)
            {
                this.repositoryItemLookUpEdit3.DataSource = dtDoctorList;
            }
            #endregion

        }

        private void CureDrugTemplateList_Load(object sender, EventArgs e)
        {
            intGridData();
        }

        private void intGridData()
        {
            _currentData = _hemoService.GetTemplateByParmas(string.Empty);
            this.gridControl1.DataSource = _currentData;
        }

        private void gridView1_CustomColumnDisplayText(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventArgs e)
        {

            //  var rowCurrent = gridView1.GetDataRow(e.RowHandle) as HemodialysisModel.MED_CURE_DRUG_TEMPLATERow;
            var rowCurrent = gridView1.GetDataRow(e.ListSourceRowIndex) as HemodialysisModel.MED_CURE_DRUG_TEMPLATERow;
            if (rowCurrent == null)
            {
                return;
            }

            if (e.Column == gridColumn_COM)
            {
                var exitrows = _currentData.Count(wh => wh.COM_NO == rowCurrent.COM_NO);
                var smalCount = _currentData.Count(wh => wh.COM_NO == rowCurrent.COM_NO && Convert.ToInt32(wh.COM_SUB_NO) < Convert.ToInt32(rowCurrent.COM_SUB_NO));
                var bigCount = _currentData.Count(wh => wh.COM_NO == rowCurrent.COM_NO && Convert.ToInt32(wh.COM_SUB_NO) > Convert.ToInt32(rowCurrent.COM_SUB_NO));
                if (exitrows == 1)
                {
                    e.DisplayText = "";
                    return;
                }

                if (smalCount == 0)
                {
                    e.DisplayText = "┏";
                    return;
                }
                if (bigCount == 0)
                {
                    e.DisplayText = "┗";
                    return;
                }
                e.DisplayText = "┃";
                return;

            }
        }


        private void gridView1_CellValueChanging(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            var rowCurrent = gridView1.GetDataRow(e.RowHandle) as HemodialysisModel.MED_CURE_DRUG_TEMPLATERow;
            if (rowCurrent == null)
            {
                return;
            }

            if (e.Column == this.gridColumn_Check)
            {
                if (Convert.ToBoolean(e.Value))
                {
                    for (int i = 0; i < _currentData.Rows.Count; i++)
                    {
                        if (rowCurrent.COM_NO == _currentData.Rows[i]["COM_NO"].ToString())
                        {
                            _currentData.Rows[i]["CHECK"] = true;
                        }
                    }
                }
                else
                {
                    for (int i = 0; i < _currentData.Rows.Count; i++)
                    {
                        if (rowCurrent.COM_NO == _currentData.Rows[i]["COM_NO"].ToString())
                        {
                            _currentData.Rows[i]["CHECK"] = false;
                        }
                    }
                }
            }
        }

        private void btn_Save_Click(object sender, EventArgs e)
        {
            int result = 0;
            string comNo = System.DateTime.Now.Date.ToString("yyyymmdd") + _hemoService.GetOrderComNo();
            if (!IsLong)
            {
                #region 保存临时医嘱
                HemodialysisModel.MED_CURE_DRUGDataTable _shortDrug = new HemodialysisModel.MED_CURE_DRUGDataTable();
                for (int i = 0; i < _currentData.Rows.Count; i++)
                {
                    var _currentROW = _currentData.Rows[i] as HemodialysisModel.MED_CURE_DRUG_TEMPLATERow;

                    if (Convert.ToBoolean(_currentROW.CHECK))
                    {
                        var newRow = _shortDrug.NewMED_CURE_DRUGRow();
                        newRow.CURE_DRUG_ID = Guid.NewGuid().ToString();
                        newRow.DRUG_CODE = _currentROW.DRUG_CODE;
                        newRow.DRUG_NAME = _currentROW.DRUG_NAME;
                        newRow.DOSAGE = _currentROW.DOSAGE;
                        newRow.DOSAGE_UNITS = _currentROW.DOSAGE_UNITS;
                        newRow.STATUS = "0";
                        newRow.DOCTOR_ID = HemoApplicationContext.Current.CurrentUser.EMP_NO;
                        newRow.CREATE_DATE = System.DateTime.Now;
                        newRow.DRUG_MODE = _currentROW.DRUG_MODE;
                        newRow.DRUG_TIMETYPE = _currentROW.IsDRUG_TIMETYPENull() ? string.Empty : _currentROW.DRUG_TIMETYPE;
                        try
                        {
                            newRow.DRUG_DAYS = _currentROW.DRUG_DAYS;
                        }
                        catch
                        {
                            newRow.DRUG_DAYS = string.Empty;
                        }
                        newRow.PATIENT_ID = CurrentPatientID;
                        newRow.HEMODIALYSIS_ID = CurrentHemoID;
                        newRow.RECIPE_ID = recipeId;
                        newRow.COM_NO = Convert.ToString(comNo + _currentROW.COM_NO);
                        newRow.COM_SUB_NO = _currentROW.COM_SUB_NO;
                        _shortDrug.AddMED_CURE_DRUGRow(newRow);
                    }
                }
                #endregion
                result = _hemoService.SaveCureDrug(_shortDrug);

            }
            else
            {
                #region 保存长期医嘱
                HemodialysisModel.MED_CURE_LONGDRUGDataTable _longDrug = new HemodialysisModel.MED_CURE_LONGDRUGDataTable();
                for (int i = 0; i < _currentData.Rows.Count; i++)
                {
                    var _currentROW = _currentData.Rows[i] as HemodialysisModel.MED_CURE_DRUG_TEMPLATERow;

                    if (Convert.ToBoolean(_currentROW.CHECK))
                    {
                        var newRow = _longDrug.NewMED_CURE_LONGDRUGRow();
                        newRow.CURE_DRUG_ID = Guid.NewGuid().ToString();
                        newRow.DRUG_CODE = _currentROW.DRUG_CODE;
                        newRow.DRUG_NAME = _currentROW.DRUG_NAME;
                        newRow.DOSAGE = _currentROW.DOSAGE;
                        newRow.DOSAGE_UNITS = _currentROW.DOSAGE_UNITS;
                        newRow.STATUS = "0";
                        newRow.DOCTOR_ID = HemoApplicationContext.Current.CurrentUser.EMP_NO; ;
                        newRow.CREATE_DATE = System.DateTime.Now;
                        newRow.DRUG_MODE = _currentROW.DRUG_MODE;
                        newRow.DRUG_TIMETYPE = _currentROW.IsDRUG_TIMETYPENull() ? string.Empty : _currentROW.DRUG_TIMETYPE;
                        newRow.DRUG_DAYS = _currentROW.DRUG_DAYS;
                        newRow.PATIENT_ID = CurrentPatientID;
                        newRow.HEMODIALYSIS_ID = CurrentHemoID;
                        newRow.COM_NO = Convert.ToString(comNo + _currentROW.COM_NO);
                        newRow.COM_SUB_NO = _currentROW.COM_SUB_NO;
                        _longDrug.AddMED_CURE_LONGDRUGRow(newRow);
                    }
                }
                #endregion
                result = _hemoService.SaveCureLongDrug(_longDrug);

            }

            #region 保存结果
            if (result >= 1)
            {
                XtraMessageBox.Show("写入药品信息成功.", "用药信息");
            }
            else
            {
                XtraMessageBox.Show("写入药品信息失败.", "用药信息");
            }
            #endregion

            this.DialogResult = System.Windows.Forms.DialogResult.OK;
        }

        private void btn_Cancle_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
        }

        private void btn_Delete_Click(object sender, EventArgs e)
        {
            if (XtraMessageBox.Show("确认删除选择行的模版数据吗？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != System.Windows.Forms.DialogResult.Yes)
            {
                return;
            }

            for (int i = 0; i < _currentData.Rows.Count; i++)
            {
                var _currentROW = _currentData.Rows[i] as HemodialysisModel.MED_CURE_DRUG_TEMPLATERow;

                if (Convert.ToBoolean(_currentROW.CHECK))
                {
                    _currentROW.Delete();
                }
            }

            _hemoService.SaveCureDrugTemplate(_currentData);
            _currentData.AcceptChanges();
            //intGridData();
        }
        #endregion
    }
}
