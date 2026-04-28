/*----------------------------------------------------------------
// Copyright (C) 2013 苏州XX公司
// 描述：患者处方及医嘱查询维护类
// 创建时间：2014-04-10
// 创建者：刘超
//  
// 修改时间：2014-04-30
// 修改人：贺建操
// 修改描述：修改部分业务逻辑
//
// 修改时间：2015-03-24
// 修改人：吕志强
// 修改描述：修改界面及部分业务逻辑
----------------------------------------------------------------*/

using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Hemo.Model;
using Hemo.Service;
using Hemo.Utilities;
using Hemo.IService.Config;
using DevExpress.XtraEditors.Controls;
using Hemo.IService;
using Hemo.IService.Dict;
using Hemo.Client.Core;
using Hemo.IService.Permission;
using Hemo.IService.PatientSchedule;
using Hemo.Client.UI.Machine;
using Hemo.Client.UI.Hemodialysis;
using Microsoft.VisualBasic;
using System.Windows.Forms;

namespace Hemo.Client.UI.PatientFixUI
{
    public partial class QueryRecipeListUI : ViewBase
    {
        #region 类变量

        private IHemodialysis objHemodialysisService = ServiceManager.Instance.HemodialysisService;
        private IConfig _configService = ServiceManager.Instance.ConfigService;
        private IPatientSchedule patientScheduleService = ServiceManager.Instance.PatientSchedule;
        private IStaffDict _staffDictService = ServiceManager.Instance.StaffDictService;
        private IDrug objDrug = ServiceManager.Instance.DrugService;
        private HemodialysisModel.MED_CURE_DRUGDataTable drugDt = null;
        private HemodialysisModel.MED_CURE_LONGDRUGDataTable longdrugdt = null;
        private IUser _userService = ServiceManager.Instance.UserService;
        private ConfigModel.MED_COMMON_RELATIONDataTable _relationData = new ConfigModel.MED_COMMON_RELATIONDataTable();
        private DateTime _currentDt = DateTime.Now;
        private string hemoId = string.Empty;
        private string areaName = string.Empty;
        private int tabIndex = 0;
        DataTable dtMain = null;
        DataTable dtParam = null;
        bool loadTime = false;
        bool drugStatus = false;
        private IPatient _patientService = ServiceManager.Instance.PatientService;
        #endregion

        #region 属性

        public string AreaName
        {
            get { return areaName; }
            set { areaName = value; }
        }

        public DateTime CurrentDt
        {
            get { return _currentDt; }
            set { _currentDt = value; }
        }

        public string currentRecipeIdStr { get; set; }

        #endregion

        #region 构造函数

        public QueryRecipeListUI(string pHemoID, int pTabIndex)
        {
            InitializeComponent();
            this.hemoId = pHemoID;
            this.tabIndex = pTabIndex;
        }

        #endregion

        #region 事件

        private void btnQuery_Click(object sender, EventArgs e)
        {
            loadCureList(UserInfo.HEMODIALYSIS_ID);
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            using (EditPrescribe frm = new EditPrescribe(UserInfo.HEMODIALYSIS_ID, ""))
            {
                frm.AreaName = areaName;
                frm.ShowDialog();
                if (frm.DialogResult == DialogResult.Yes)
                {
                    loadRecipeList(UserInfo.HEMODIALYSIS_ID);
                    loadCureList(UserInfo.HEMODIALYSIS_ID);
                }
            }
        }

        private void gridView1_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
        {
            if (gridVRecipe.GetRow(e.RowHandle) == null)
            {
                return;
            }
            if (e.Column.FieldName == "CURE_STATUS_NAME")
            {
                string status = gridVRecipe.GetRowCellValue(e.RowHandle, "CURE_STATUS").ToString();
                if (status == "3")
                {
                    e.Appearance.BackColor = Color.FromArgb(152, 251, 152);//设置此行的背景颜色
                }
                else if (status == "4")
                {
                    e.Appearance.BackColor = Color.FromArgb(49, 106, 197);
                }
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            DataRow dr = null;
            dr = gridView2.GetFocusedDataRow();
            showRecipeInfo(dr);
        }

        private void gridRecipe_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                contextMenuStrip1.Show(MousePosition);
            }
        }

        private void gridRecipeTemp_MouseDown(object sender, MouseEventArgs e)
        {
            //if (e.Button == MouseButtons.Right) {
            //    contextMenuStrip1.Show(MousePosition);
            //}
        }

        private void gridVRecipe_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {

        }

        private void toolBegion_Click(object sender, EventArgs e)
        {
            DataRow dr = null;
            HemodialysisModel.MED_HEMO_RECIPEDataTable recipe = null;
            dr = gridVRecipe.GetFocusedDataRow();
            if (dr != null)
            {
                recipe = objHemodialysisService.GetRecipeByRecipeID(dr["RECIPE_ID"].ToString());
                HemodialysisModel.MED_HEMO_RECIPERow drRecipe = recipe.Rows[0] as HemodialysisModel.MED_HEMO_RECIPERow;
                drRecipe.STATUS = "1";
                if (recipe.Rows.Count > 0)
                {
                    if (objHemodialysisService.SaveRecipe(recipe) >= 1)
                    {
                        XtraMessageBox.Show("医嘱状态修改成功.", "医嘱下达");
                        loadRecipeList(UserInfo.HEMODIALYSIS_ID);
                    }
                }
            }
        }

        private void toolStop_Click(object sender, EventArgs e)
        {
            DataRow dr = null;
            HemodialysisModel.MED_HEMO_RECIPEDataTable recipe = null;
            dr = gridVRecipe.GetFocusedDataRow();

            if (dr != null)
            {
                recipe = objHemodialysisService.GetRecipeByRecipeID(dr["RECIPE_ID"].ToString());
                HemodialysisModel.MED_HEMO_RECIPERow drRecipe = recipe.Rows[0] as HemodialysisModel.MED_HEMO_RECIPERow;
                drRecipe.STATUS = "0";
                if (recipe.Rows.Count > 0)
                {
                    if (objHemodialysisService.SaveRecipe(recipe) >= 1)
                    {
                        XtraMessageBox.Show("选中的医嘱已停止。", "医嘱下达");
                        loadRecipeList(UserInfo.HEMODIALYSIS_ID);
                    }
                }
            }
        }

        /// <summary>
        /// 拷贝选中的处方信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCopy_Click(object sender, EventArgs e)
        {
            DataRow dr = null;
            //if (xtraTabControl1.SelectedTabPageIndex == 0) {
            //    dr = gridVRecipe.GetFocusedDataRow();
            //}
            //if (xtraTabControl1.SelectedTabPageIndex == 1) {
            //    dr = gridView3.GetFocusedDataRow();
            //}
            dr = gridView2.GetFocusedDataRow();
            if (dr != null)
            {
                if (XtraMessageBox.Show("确定复制选中的医嘱信息吗？", this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                    return;

                HemodialysisModel.MED_HEMO_RECIPEDataTable recipe = objHemodialysisService.GetRecipeByRecipeID(dr["RECIPE_ID"].ToString());
                HemodialysisModel.MED_HEMO_RECIPERow drRecipe = recipe.Rows[0] as HemodialysisModel.MED_HEMO_RECIPERow;
                drRecipe.RECIPE_ID = objHemodialysisService.GetNewRecipeID();
                HemodialysisModel.MED_HEMO_RECIPEDataTable newRecipe = new HemodialysisModel.MED_HEMO_RECIPEDataTable();
                HemodialysisModel.MED_HEMO_RECIPERow newDrRecipe = newRecipe.NewMED_HEMO_RECIPERow();
                newDrRecipe.RECIPE_ID = objHemodialysisService.GetNewRecipeID();
                //newDrRecipe.HEMODIALYSIS_ID = UserInfo.HEMODIALYSIS_ID;
                if (!drRecipe.IsPATIENT_IDNull())
                {
                    newDrRecipe.PATIENT_ID = drRecipe.PATIENT_ID;
                }
                if (!drRecipe.IsRECIPE_DATENull())
                {
                    newDrRecipe.RECIPE_DATE = System.DateTime.Now;
                }
                if (!drRecipe.IsPURIFICATION_MODENull())
                {
                    newDrRecipe.PURIFICATION_MODE = drRecipe.PURIFICATION_MODE;
                }
                if (!drRecipe.IsDRY_WEIGHTNull())
                {
                    newDrRecipe.DRY_WEIGHT = drRecipe.DRY_WEIGHT;
                }
                if (!drRecipe.IsFREQUENCY_WEEKNull())
                {
                    newDrRecipe.FREQUENCY_WEEK = drRecipe.FREQUENCY_WEEK;
                }
                if (!drRecipe.IsFREQUENCY_TIMESNull())
                {
                    newDrRecipe.FREQUENCY_TIMES = drRecipe.FREQUENCY_TIMES;
                }
                if (!drRecipe.IsFREQUENCY_HOURSNull())
                {
                    newDrRecipe.FREQUENCY_HOURS = drRecipe.FREQUENCY_HOURS;
                }
                if (!drRecipe.IsSPKT_VNull())
                {
                    newDrRecipe.SPKT_V = drRecipe.SPKT_V;
                }
                if (!drRecipe.IsURRNull())
                {
                    newDrRecipe.URR = drRecipe.URR;
                }
                if (!drRecipe.IsFIRST_PURIFIER_MODELNull())
                {
                    newDrRecipe.FIRST_PURIFIER_MODEL = drRecipe.FIRST_PURIFIER_MODEL;
                }
                if (!drRecipe.IsFIRST_PURIFIER_NAMENull())
                {
                    newDrRecipe.FIRST_PURIFIER_NAME = drRecipe.FIRST_PURIFIER_NAME;
                }
                if (!drRecipe.IsFIRST_PURIFIER_M2Null())
                {
                    newDrRecipe.FIRST_PURIFIER_M2 = drRecipe.FIRST_PURIFIER_M2;
                }
                if (!drRecipe.IsFIRST_PURIFIER_KOANull())
                {
                    newDrRecipe.FIRST_PURIFIER_KOA = drRecipe.FIRST_PURIFIER_KOA;
                }
                if (!drRecipe.IsFIRST_PURIFIER_KUFNull())
                {
                    newDrRecipe.FIRST_PURIFIER_KUF = drRecipe.FIRST_PURIFIER_KUF;
                }
                if (!drRecipe.IsSECOND_PURIFIER_MODELNull())
                {
                    newDrRecipe.SECOND_PURIFIER_MODEL = drRecipe.SECOND_PURIFIER_MODEL;
                }
                if (!drRecipe.IsSECOND_PURIFIER_NAMENull())
                {
                    newDrRecipe.SECOND_PURIFIER_NAME = drRecipe.SECOND_PURIFIER_NAME;
                }
                if (!drRecipe.IsSECOND_PURIFIER_M2Null())
                {
                    newDrRecipe.SECOND_PURIFIER_M2 = drRecipe.SECOND_PURIFIER_M2;
                }
                if (!drRecipe.IsSECOND_PURIFIER_KOANull())
                {
                    newDrRecipe.SECOND_PURIFIER_KOA = drRecipe.SECOND_PURIFIER_KOA;
                }
                if (!drRecipe.IsSECOND_PURIFIER_KUFNull())
                {
                    newDrRecipe.SECOND_PURIFIER_KUF = drRecipe.SECOND_PURIFIER_KUF;
                }
                if (!drRecipe.IsSODIONNull())
                {
                    newDrRecipe.SODION = drRecipe.SODION;
                }
                if (!drRecipe.IsPOTASSIUM_IONNull())
                {
                    newDrRecipe.POTASSIUM_ION = drRecipe.POTASSIUM_ION;
                }
                if (!drRecipe.IsCALCIUM_IONNull())
                {
                    newDrRecipe.CALCIUM_ION = drRecipe.CALCIUM_ION;
                }
                if (!drRecipe.IsBICARBONATE_RADICALNull())
                {
                    newDrRecipe.BICARBONATE_RADICAL = drRecipe.BICARBONATE_RADICAL;
                }
                if (!drRecipe.IsBLOOW_FLOWNull())
                {
                    newDrRecipe.BLOOW_FLOW = drRecipe.BLOOW_FLOW;
                }
                if (!drRecipe.IsDIALYSATE_FLOWNull())
                {
                    newDrRecipe.DIALYSATE_FLOW = drRecipe.DIALYSATE_FLOW;
                }
                if (!drRecipe.IsDIALYSATE_TEMPERATURENull())
                {
                    newDrRecipe.DIALYSATE_TEMPERATURE = drRecipe.DIALYSATE_TEMPERATURE;
                }
                if (!drRecipe.IsDISPLACEMENT_LIQUIDNull())
                {
                    newDrRecipe.DISPLACEMENT_LIQUID = drRecipe.DISPLACEMENT_LIQUID;
                }
                if (!drRecipe.IsBLOOD_DISPLACEMENTNull())
                {
                    newDrRecipe.BLOOD_DISPLACEMENT = drRecipe.BLOOD_DISPLACEMENT;
                }
                if (!drRecipe.IsTHERAPEUTIC_METHODNull())
                {
                    newDrRecipe.THERAPEUTIC_METHOD = drRecipe.THERAPEUTIC_METHOD;
                }
                if (!drRecipe.IsFIRST_DRUG_NAMENull())
                {
                    newDrRecipe.FIRST_DRUG_NAME = drRecipe.FIRST_DRUG_NAME;
                }
                if (!drRecipe.IsFIRST_DRUG_DOSAGENull())
                {
                    newDrRecipe.FIRST_DRUG_DOSAGE = drRecipe.FIRST_DRUG_DOSAGE;
                }
                if (!drRecipe.IsFIRST_DRUG_UNITNull())
                {
                    newDrRecipe.FIRST_DRUG_UNIT = drRecipe.FIRST_DRUG_UNIT;
                }
                if (!drRecipe.IsFIRST_DRUG_MODENull())
                {
                    newDrRecipe.FIRST_DRUG_MODE = drRecipe.FIRST_DRUG_MODE;
                }
                if (!drRecipe.IsSECOND_DRUG_NAMENull())
                {
                    newDrRecipe.SECOND_DRUG_NAME = drRecipe.SECOND_DRUG_NAME;
                }
                if (!drRecipe.IsSECOND_DRUG_DOSAGENull())
                {
                    newDrRecipe.SECOND_DRUG_DOSAGE = drRecipe.SECOND_DRUG_DOSAGE;
                }
                if (!drRecipe.IsSECOND_DRUG_UNITNull())
                {
                    newDrRecipe.SECOND_DRUG_UNIT = drRecipe.SECOND_DRUG_UNIT;
                }
                if (!drRecipe.IsSECOND_DRUG_MODENull())
                {
                    newDrRecipe.SECOND_DRUG_MODE = drRecipe.SECOND_DRUG_MODE;
                }
                if (!drRecipe.IsUSER_IDNull())
                {
                    newDrRecipe.USER_ID = drRecipe.USER_ID;
                }
                newDrRecipe.HEMODIALYSIS_ID = drRecipe.HEMODIALYSIS_ID;
                if (!drRecipe.IsSTATUSNull())
                {
                    newDrRecipe.STATUS = "0";
                }
                if (!drRecipe.IsVASCULAR_ACCESS_IDNull())
                {
                    newDrRecipe.VASCULAR_ACCESS_ID = drRecipe.VASCULAR_ACCESS_ID;
                }
                if (!drRecipe.IsRECIPE_TYPENull())
                {
                    newDrRecipe.RECIPE_TYPE = drRecipe.RECIPE_TYPE;
                }
                newRecipe.Rows.Add(newDrRecipe);
                if (newRecipe.Rows.Count > 0)
                {
                    if (objHemodialysisService.SaveRecipe(newRecipe) >= 1)
                    {
                        XtraMessageBox.Show("复制的医嘱信息保存成功.", "医嘱下达");
                        loadRecipeList(UserInfo.HEMODIALYSIS_ID);
                        loadCureList(UserInfo.HEMODIALYSIS_ID);
                        using (EditPrescribe frm = new EditPrescribe(UserInfo.HEMODIALYSIS_ID, dr["RECIPE_ID"].ToString()))
                        {
                            frm.AreaName = areaName;
                            frm.ShowDialog();
                            if (frm.DialogResult == DialogResult.Yes)
                            {
                                loadRecipeList(UserInfo.HEMODIALYSIS_ID);
                                loadCureList(UserInfo.HEMODIALYSIS_ID);
                            }
                        }
                    }
                }
            }
        }

        private void grdCureList_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                contextMenuStrip3.Show(MousePosition);
            }
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            DataRow dr = gridVRecipe.GetFocusedDataRow();
            HemodialysisModel.MED_HEMO_RECIPEDataTable recipe = null;

            if (dr != null)
            {
                if (XtraMessageBox.Show("确定停止当前医嘱信息吗？", this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                    return;

                recipe = objHemodialysisService.GetRecipeByRecipeID(dr["RECIPE_ID"].ToString());
                HemodialysisModel.MED_HEMO_RECIPERow drRecipe = recipe.Rows[0] as HemodialysisModel.MED_HEMO_RECIPERow;
                drRecipe.STATUS = "0";
                if (recipe.Rows.Count > 0)
                {
                    if (objHemodialysisService.SaveRecipe(recipe) >= 1)
                    {
                        XtraMessageBox.Show("当前医嘱已停用.", "医嘱下达");
                        loadRecipeList(UserInfo.HEMODIALYSIS_ID);
                    }
                }
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            loadRecipeList(UserInfo.HEMODIALYSIS_ID);
            loadCureList(UserInfo.HEMODIALYSIS_ID);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.ParentForm.DialogResult = DialogResult.Yes;
        }

        private void btnClean_Click(object sender, EventArgs e)
        {
            cmbStatus.EditValue = string.Empty;
            lupRECIPE_TYPE.EditValue = string.Empty;
            cmbPURIFICATION_MODE.EditValue = string.Empty;
            cmbTHERAPEUTIC_METHOD.EditValue = string.Empty;
            loadCureList(UserInfo.HEMODIALYSIS_ID);
        }

        private void xtraTabControl2_TabIndexChanged(object sender, EventArgs e)
        {

        }

        private void xtraTabControl2_SelectedPageChanged(object sender, DevExpress.XtraTab.TabPageChangedEventArgs e)
        {
            if (this.xtraTabControl2.SelectedTabPageIndex == 0)
            {
                this.lupRECIPE_TYPE.EditValue = "1";
            }
            else
            {
                this.lupRECIPE_TYPE.EditValue = "0";
            }

            if (this.xtraTabControl2.SelectedTabPageIndex == 2)
            {
                loadChart(this.patientRecipeFrm1);
            }

            if (this.xtraTabControl2.SelectedTabPageIndex == 3)
            {                
                loadChart(this.patientRecipeFrm2);
            }
        }

        private void gridVRecipe_DoubleClick(object sender, EventArgs e)
        {
            //    DataTable dt = objHemodialysisService.GetMainCureListByHemoID(pHemoID);
            DataRow dr = this.gridVRecipe.GetFocusedDataRow() as DataRow;
            showRecipeInfo(dr);
        }

        private void gridView2_DoubleClick(object sender, EventArgs e)
        {
            DataRow dr = null;
            dr = gridView2.GetFocusedDataRow();
            showRecipeInfo(dr);
        }

        private void xtraTabControl3_SelectedPageChanged(object sender, DevExpress.XtraTab.TabPageChangedEventArgs e)
        {
            if (this.xtraTabControl3.SelectedTabPageIndex == 0)
            {
                this.cmbDRUG_TIMES.Visible = false;
                //    this.labelControl55.Visible = false;
                this.cbmDRUG_DAYS.Visible = false;
                //this.spnDRUG_TIMES.Visible = false;
                this.toolStripMenuItemForLong.Visible = false;
                this.toolStripMenuItemDrug.Visible = true;
                Utilities.BaseControlInfo.ClearControlText(panDrug);
                Utilities.BaseControlInfo.SetControlEnabled(panDrug, false);
            }
            else if (this.xtraTabControl3.SelectedTabPageIndex == 1)
            {
                this.cmbDRUG_TIMES.Visible = true;
                //  this.labelControl55.Visible = true;
                this.cbmDRUG_DAYS.Visible = true;
                // this.spnDRUG_TIMES.Visible = true;
                this.toolStripMenuItemForLong.Visible = true;
                this.toolStripMenuItemDrug.Visible = false;
                Utilities.BaseControlInfo.ClearControlText(panDrug);
                Utilities.BaseControlInfo.SetControlEnabled(panDrug, false);
            }
            this.btnAddDrug.Enabled = true;
        }

        private void gridView7_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            if (gridView7.GetRow(e.RowHandle) == null)
            {
                return;
            }
            else
            {
                if (e.RowHandle == gridView7.FocusedRowHandle)
                {
                    return;
                }
                //获取所在行指定列的值
                string status = gridView7.GetRowCellValue(e.RowHandle, "STATUS").ToString();

                //比较指定列的状态
                if (status == "0")
                {
                    //e.Appearance.BackColor = Color.SkyBlue;//设置开立的背景颜色
                    e.Appearance.ForeColor = Color.Black;
                }
                else if (status == "1")
                {
                    //e.Appearance.BackColor = Color.LightYellow;//设置执行的背景颜色
                    e.Appearance.ForeColor = Color.Green;
                }
                else if (status == "2")
                {
                    //e.Appearance.BackColor = Color.Gray;//设置返回的背景颜色 
                    e.Appearance.ForeColor = Color.Gray;
                }
                else if (status == "3")
                {
                    //e.Appearance.BackColor = Color.Red;//设置停止的背景颜色 
                    e.Appearance.ForeColor = Color.Red;
                }
            }
        }

        private void gridView5_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            if (gridView5.GetRow(e.RowHandle) == null)
            {
                return;
            }
            else
            {
                if (e.RowHandle == gridView5.FocusedRowHandle)
                {
                    return;
                }
                //获取所在行指定列的值
                string status = gridView5.GetRowCellValue(e.RowHandle, "STATUS").ToString();

                //比较指定列的状态
                if (status == "0")
                {
                    //e.Appearance.BackColor = Color.SkyBlue;//设置开立的背景颜色
                    e.Appearance.ForeColor = Color.Black;
                }
                else if (status == "1")
                {
                    //e.Appearance.BackColor = Color.LightYellow;//设置执行的背景颜色
                    e.Appearance.ForeColor = Color.Green;
                }
                else if (status == "2")
                {
                    //e.Appearance.BackColor = Color.Gray;//设置返回的背景颜色 
                    e.Appearance.ForeColor = Color.Gray;
                }
                else if (status == "3")
                {
                    //e.Appearance.BackColor = Color.Red;//设置停止的背景颜色 
                    e.Appearance.ForeColor = Color.Red;
                }
            }

        }

        private void toolStripMenuItemForLong_Click(object sender, EventArgs e)
        {
            var dr = gridView7.GetFocusedDataRow() as HemodialysisModel.MED_CURE_LONGDRUGRow;

            if (dr != null)
            {
                if (dr.STATUS == "3")
                {
                    this.toolStripMenuItemForLong.Enabled = false;
                    return;
                }
                else { this.toolStripMenuItemForLong.Enabled = true; }

                if (XtraMessageBox.Show("确定停止当前用药信息吗？", this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                    return;
                objHemodialysisService.SaveExeDrugLongStatus("3", dr.CURE_DRUG_ID);
                loadCureDurgList(dr.HEMODIALYSIS_ID);
            }
        }

        private void toolStripMenuItemDrug_Click(object sender, EventArgs e)
        {
            var dr = gridView5.GetFocusedDataRow() as HemodialysisModel.MED_CURE_DRUGRow;

            if (dr != null)
            {
                if (dr.STATUS == "3")
                {
                    this.toolStripMenuItemDrug.Enabled = false;
                    return;
                }
                else { this.toolStripMenuItemDrug.Enabled = true; }

                if (XtraMessageBox.Show("确定停止当前用药信息吗？", this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                    return;
                objHemodialysisService.SaveExeDrugStatus("3", dr.CURE_DRUG_ID);
                loadCureDurgList(dr.HEMODIALYSIS_ID);
            }
        }

        private void gridView7_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            var dr = gridView7.GetFocusedDataRow() as HemodialysisModel.MED_CURE_LONGDRUGRow;
            if (dr != null && (dr.STATUS == "0" || dr.STATUS == "2"))
            {
                drugStatus = true;
                HemodialysisModel.MED_CURE_LONGDRUGDataTable tmpCrue = new HemodialysisModel.MED_CURE_LONGDRUGDataTable();
                tmpCrue.Rows.Add(dr.ItemArray);
                BaseControlInfo.SetControlDataByDataTable(tmpCrue, panDrug);
                cmbCreate_Time.Time = Utility.CDate(dr.CREATE_DATE.ToString());
                txtDRUG_NAME.EditValue = dr.DRUG_CODE.ToString().Trim();
                txtDOSAGE.Text = dr.IsDOSAGENull() ? string.Empty : dr.DOSAGE;
                cmbDOSAGE_UNITS.EditValue = dr.IsDOSAGE_UNITSNull() ? string.Empty : dr.DOSAGE_UNITS;
                cmbDRUG_MODE.EditValue = dr.IsDRUG_MODENull() ? "" : dr.DRUG_MODE;
                //给药频率下拉框显示自定义文本 龙宇涵 2026-4-16
                string drugTimes = dr.IsDRUG_TIMESNull() ? "" : dr.DRUG_TIMES.ToString().Trim();
                cmbDRUG_TIMES.EditValue = null;
                cmbDRUG_TIMES.Properties.NullText = "";

                if (!string.IsNullOrEmpty(drugTimes))
                {
                    if (drugTimes == "1")
                        cmbDRUG_TIMES.EditValue = "1";
                    else if (drugTimes == "3")
                        cmbDRUG_TIMES.EditValue = "3";
                    else
                    {
                        cmbDRUG_TIMES.Properties.NullText = drugTimes;
                    }
                    cmbDRUG_TIMES.Refresh();
                }

                if (!dr.IsDRUG_DAYSNull() && !string.IsNullOrEmpty(dr.DRUG_DAYS))
                {
                    string dbValue = dr.DRUG_DAYS.Trim();
                    // 先取消所有选中
                    for (int i = 0; i < cbmDRUG_DAYS.Properties.Items.Count; i++)
                    {
                        cbmDRUG_DAYS.Properties.Items[i].CheckState = CheckState.Unchecked;
                    }
                    // 按逗号分隔，逐项匹配勾选
                    string[] days = dbValue.Split(new char[] { ',', '，' }, StringSplitOptions.RemoveEmptyEntries);
                    foreach (string day in days)
                    {
                        string trimmedDay = day.Trim();
                        for (int i = 0; i < cbmDRUG_DAYS.Properties.Items.Count; i++)
                        {
                            if (cbmDRUG_DAYS.Properties.Items[i].Value.ToString() == trimmedDay)
                            {
                                cbmDRUG_DAYS.Properties.Items[i].CheckState = CheckState.Checked;
                                break;
                            }
                        }
                    }
                    cbmDRUG_DAYS.Refresh();
                }
                else
                {
                    cbmDRUG_DAYS.EditValue = null;
                    cbmDRUG_DAYS.Properties.NullText = "";
                }
                //if (!dr.IsDRUG_DAYSNull() && !string.IsNullOrEmpty(dr.DRUG_DAYS))
                //{

                //    string dbValue = dr.DRUG_DAYS;
                //    System.Diagnostics.Debug.WriteLine($"数据库值: [{dbValue}]");
                //    System.Diagnostics.Debug.WriteLine($"值的长度: {dbValue.Length}");
                //    System.Diagnostics.Debug.WriteLine($"值的字符: {string.Join(" ", dbValue.ToCharArray().Select(c => ((int)c).ToString()))}");
                //    cbmDRUG_DAYS.EditValue = dr.DRUG_DAYS;
                //}
                //else
                //{
                //    cbmDRUG_DAYS.EditValue = null;
                //    cbmDRUG_DAYS.Properties.NullText = "";
                //}

                tmpCrue = null;
                if (Utility.CDate(dr.CREATE_DATE.ToString()).ToString("yyyy-MM-dd") == Utility.CDate(patientScheduleService.GetServerDate()).ToString("yyyy-MM-dd"))
                {
                    Utilities.BaseControlInfo.SetControlEnabled(panDrug, true);
                    this.btnSaveDrug.Enabled = true;
                }
                else
                {
                    Utilities.BaseControlInfo.SetControlEnabled(panDrug, false);
                    this.btnSaveDrug.Enabled = false;
                }

                isAddDrug = false;
            }
            else
            {
                Utilities.BaseControlInfo.SetControlEnabled(panDrug, false);
                this.btnSaveDrug.Enabled = false; ;
            }
        }

        private void gridDrugList_MouseDown(object sender, MouseEventArgs e)
        {
            var rows = gridView5.GetSelectedRows();
            if (rows.Length > 1)
            {
                bool isShow = true;
                for (int i = 0; i < rows.Length; i++)
                {
                    if (gridView5.GetRowCellDisplayText(int.Parse(rows[i].ToString()), COM_gridColumn).Length > 0)
                    {
                        isShow = false;
                        break;
                    }
                }

                if (isShow && e.Button == System.Windows.Forms.MouseButtons.Right)
                {
                    this.contextMenuStrip2.Show(MousePosition);
                    this.ToolStripMenuItemDelete.Visible = false;
                    this.toolStripMenuItemDrug.Visible = false;
                    this.ToolStripMenuItemForCom.Visible = true;
                    this.ToolStripMenuItemForComCancle.Visible = false;
                }
                else if (e.Button == System.Windows.Forms.MouseButtons.Right)
                {
                    this.contextMenuStrip2.Show(MousePosition);
                    this.ToolStripMenuItemDelete.Visible = false;
                    this.toolStripMenuItemDrug.Visible = false;
                    this.ToolStripMenuItemForCom.Visible = false;
                    this.ToolStripMenuItemForComCancle.Visible = true;
                }
                else
                {
                    this.contextMenuStrip2.Hide();
                }

            }
            else
            {

                var dr = gridView5.GetFocusedDataRow() as HemodialysisModel.MED_CURE_DRUGRow;
                if (dr == null)
                {
                    this.contextMenuStrip2.Hide();
                    return;
                }
                if (e.Button == System.Windows.Forms.MouseButtons.Right && (dr.STATUS == "2"))
                {
                    this.contextMenuStrip2.Show(MousePosition);
                    this.ToolStripMenuItemDelete.Visible = false;
                    this.toolStripMenuItemDrug.Visible = true;
                    this.ToolStripMenuItemForCom.Visible = false;
                    this.ToolStripMenuItemForComCancle.Visible = false;

                }
                else if (e.Button == System.Windows.Forms.MouseButtons.Right && (dr.STATUS == "0"))
                {
                    this.contextMenuStrip2.Show(MousePosition);
                    this.ToolStripMenuItemDelete.Visible = true;
                    this.toolStripMenuItemDrug.Visible = false;
                    this.ToolStripMenuItemForCom.Visible = false;
                    this.ToolStripMenuItemForComCancle.Visible = false;
                }
                else
                {
                    this.contextMenuStrip2.Hide();
                }
            }
        }

        private void gridDrugListLong_MouseDown(object sender, MouseEventArgs e)
        {
            var rows = gridView7.GetSelectedRows();
            if (rows.Length > 1)
            {
                bool isShow = true;
                for (int i = 0; i < rows.Length; i++)
                {
                    if (gridView7.GetRowCellDisplayText(int.Parse(rows[i].ToString()), COM_gridColumnlONG).Length > 0)
                    {
                        isShow = false;
                        break;
                    }
                }

                if (isShow && e.Button == System.Windows.Forms.MouseButtons.Right)
                {
                    this.contextMenuStrip2.Show(MousePosition);
                    this.ToolStripMenuItemDelete.Visible = false;
                    this.toolStripMenuItemForLong.Visible = false;
                    this.ToolStripMenuItemForCom.Visible = true;
                    this.ToolStripMenuItemForComCancle.Visible = false;
                }
                else if (e.Button == System.Windows.Forms.MouseButtons.Right)
                {
                    this.contextMenuStrip2.Show(MousePosition);
                    this.ToolStripMenuItemDelete.Visible = false;
                    this.toolStripMenuItemDrug.Visible = false;
                    this.ToolStripMenuItemForCom.Visible = false;
                    this.ToolStripMenuItemForComCancle.Visible = true;

                }
                else { this.contextMenuStrip2.Hide(); }

            }
            else
            {
                var dr = gridView7.GetFocusedDataRow() as HemodialysisModel.MED_CURE_LONGDRUGRow;
                if (dr == null)
                {
                    this.contextMenuStrip2.Hide();
                    return;
                }
                if (e.Button == System.Windows.Forms.MouseButtons.Right && (dr.STATUS == "2"))
                {
                    this.contextMenuStrip2.Show(MousePosition);
                    this.ToolStripMenuItemDelete.Visible = false;
                    this.toolStripMenuItemForLong.Visible = true;
                    this.ToolStripMenuItemForCom.Visible = false;
                    this.ToolStripMenuItemForComCancle.Visible = false;
                }
                else if (e.Button == System.Windows.Forms.MouseButtons.Right && (dr.STATUS == "0"))
                {
                    this.contextMenuStrip2.Show(MousePosition);
                    this.ToolStripMenuItemDelete.Visible = true;
                    this.toolStripMenuItemForLong.Visible = false;
                    this.ToolStripMenuItemForCom.Visible = false;
                    this.ToolStripMenuItemForComCancle.Visible = false;
                }
                else
                {
                    this.contextMenuStrip2.Hide();
                }
            }
        }

        private void ToolStripMenuItemDelete_Click(object sender, EventArgs e)
        {
            if (this.xtraTabControl3.SelectedTabPageIndex == 0)
            {
                var dr = gridView5.GetFocusedDataRow() as HemodialysisModel.MED_CURE_DRUGRow;
                if (XtraMessageBox.Show("是否确认删除当前临时医嘱？", this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                    return;
                objHemodialysisService.DeleteCureORLongDrugByID("short", dr.CURE_DRUG_ID);

            }
            else
            {
                var dr = gridView7.GetFocusedDataRow() as HemodialysisModel.MED_CURE_LONGDRUGRow;
                if (XtraMessageBox.Show("是否确认删除当前长期医嘱？", this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                    return;
                objHemodialysisService.DeleteCureORLongDrugByID("long", dr.CURE_DRUG_ID);
            }
            loadCureDurgList(UserInfo.HEMODIALYSIS_ID);
        }


        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {

            //    var dr = grdCureList.GetFocusedDataRow();
            //    if (XtraMessageBox.Show("是否确认作废当前临时遗嘱？", this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            //        return;
            //    objHemodialysisService.DeleteCureORLongDrugByID("long", dr.CURE_DRUG_ID);
            //}

        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            DataRow dr = null;
            HemodialysisModel.MED_HEMO_RECIPEDataTable recipe = null;
            dr = gridView2.GetFocusedDataRow();

            if (dr != null)
            {
                if (XtraMessageBox.Show("是否作废当前临时医嘱？", this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                {
                    return;
                }

                if (dr["STATUS_NAME"].ToString() == "已执行")
                {
                    XtraMessageBox.Show("该医嘱已执行不能作废");
                    return;
                }


                recipe = objHemodialysisService.GetRecipeByRecipeID(dr["RECIPE_ID"].ToString());
                HemodialysisModel.MED_HEMO_RECIPERow drRecipe = recipe.Rows[0] as HemodialysisModel.MED_HEMO_RECIPERow;
                //if (drRecipe.STATUS == "1")
                //{
                //    if (XtraMessageBox.Show("此处方已执行禁止作废！", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning) == DialogResult.OK)
                //        return;
                //}
                drRecipe.STATUS = "2";//临时医嘱停用标识
                if (recipe.Rows.Count > 0)
                {
                    if (objHemodialysisService.SaveRecipe(recipe) >= 1)
                    {
                        XtraMessageBox.Show("选中的临时医嘱已作废。", "医嘱下达");
                        loadRecipeList(UserInfo.HEMODIALYSIS_ID);
                        loadCureList(UserInfo.HEMODIALYSIS_ID);
                    }
                }
            }
            else
            {
                XtraMessageBox.Show("请选择一条临时遗嘱.", "医嘱下达");
            }
        }

        private void gridView2_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            if (gridView2.GetRow(e.RowHandle) == null)
            {
                return;
            }
            else
            {
                //获取所在行指定列的值
                string status = gridView2.GetRowCellValue(e.RowHandle, "STATUS").ToString();

                //比较指定列的状态
                if (status == "2")
                {
                    e.Appearance.BackColor = Color.Red;//设置作废的背景颜色 
                    e.Appearance.ForeColor = Color.Black;
                    //  e.Appearance.ForeColor = Color.Black;
                }
            }
        }

        private void gridView5_CustomColumnDisplayText(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventArgs e)
        {
            var rowCurrent = gridView5.GetDataRow(e.ListSourceRowIndex) as HemodialysisModel.MED_CURE_DRUGRow;
            if (rowCurrent == null)
            {
                return;
            }

            if (e.Column == COM_gridColumn)
            {
                var exitrows = drugDt.Count(wh => wh.COM_NO == rowCurrent.COM_NO);
                var smalCount = drugDt.Count(wh => wh.COM_NO == rowCurrent.COM_NO && Convert.ToInt32(wh.COM_SUB_NO) < Convert.ToInt32(string.IsNullOrEmpty(rowCurrent.COM_SUB_NO) ? "1" : rowCurrent.COM_SUB_NO));
                var bigCount = drugDt.Count(wh => wh.COM_NO == rowCurrent.COM_NO && Convert.ToInt32(wh.COM_SUB_NO) > Convert.ToInt32(string.IsNullOrEmpty(rowCurrent.COM_SUB_NO) ? "1" : rowCurrent.COM_SUB_NO));
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

        private void gridView7_CustomColumnDisplayText(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventArgs e)
        {
            var rowCurrent = gridView7.GetDataRow(e.ListSourceRowIndex) as HemodialysisModel.MED_CURE_LONGDRUGRow;
            if (rowCurrent == null)
            {
                return;
            }

            if (e.Column == COM_gridColumnlONG)
            {
                var exitrows = longdrugdt.Count(wh => wh.COM_NO == rowCurrent.COM_NO);
                var smalCount = longdrugdt.Count(wh => wh.COM_NO == rowCurrent.COM_NO && Convert.ToInt32(string.IsNullOrEmpty(wh.COM_SUB_NO) ? "0" : wh.COM_SUB_NO) < Convert.ToInt32(string.IsNullOrEmpty(rowCurrent.COM_SUB_NO) ? "0" : rowCurrent.COM_SUB_NO));
                var bigCount = longdrugdt.Count(wh => wh.COM_NO == rowCurrent.COM_NO && Convert.ToInt32(string.IsNullOrEmpty(wh.COM_SUB_NO) ? "0" : wh.COM_SUB_NO) > Convert.ToInt32(string.IsNullOrEmpty(rowCurrent.COM_SUB_NO) ? "1" : rowCurrent.COM_SUB_NO));
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

        private void ToolStripMenuItemForCom_Click(object sender, EventArgs e)
        {
            int result = -1;
            if (xtraTabControl3.SelectedTabPageIndex == 0)
            {
                string COM_NOS = string.Empty;
                string COM_SUB_NOS = string.Empty;
                string drugmode = string.Empty;
                var rows = gridView5.GetSelectedRows();
                for (int i = 0; i < rows.Length; i++)
                {
                    var row = drugDt.Rows[rows[i]] as HemodialysisModel.MED_CURE_DRUGRow;
                    if (row.IsDRUG_MODENull())
                    {
                        MessageBox.Show("组合医嘱用法不能为空！");
                        return;
                    }
                    if (string.IsNullOrEmpty(drugmode) && !string.IsNullOrEmpty(row.DRUG_MODE))
                    {
                        drugmode = row.DRUG_MODE;
                    }
                    if (COM_NOS.Length <= 0)
                    {
                        COM_NOS = row.COM_NO;
                        COM_SUB_NOS = row.COM_SUB_NO;
                    }
                    row.COM_NO = COM_NOS;
                    row.COM_SUB_NO = Convert.ToString(int.Parse(COM_SUB_NOS) + i);
                }
                for (int i = 0; i < rows.Length; i++)
                {
                    var row = drugDt.Rows[rows[i]] as HemodialysisModel.MED_CURE_DRUGRow;
                    row.DRUG_MODE = drugmode;
                }
                result = objHemodialysisService.SaveCureDrug(drugDt);


            }
            else
            {
                string COM_NOS = string.Empty;
                string COM_SUB_NOS = string.Empty;
                string drugmode = string.Empty;
                var rows = gridView7.GetSelectedRows();
                for (int i = 0; i < rows.Length; i++)
                {
                    var row = longdrugdt.Rows[rows[i]] as HemodialysisModel.MED_CURE_LONGDRUGRow;
                    if (row.IsDRUG_MODENull())
                    {
                        MessageBox.Show("组合医嘱用法不能为空！");
                        return;
                    }
                    if (string.IsNullOrEmpty(drugmode) && !string.IsNullOrEmpty(row.DRUG_MODE))
                    {
                        drugmode = row.DRUG_MODE;
                    }
                    if (COM_NOS.Length <= 0)
                    {
                        COM_NOS = row.COM_NO;
                        COM_SUB_NOS = row.COM_SUB_NO;
                    }
                    row.COM_NO = COM_NOS;
                    row.COM_SUB_NO = Convert.ToString(int.Parse(COM_SUB_NOS) + i);
                }
                for (int i = 0; i < rows.Length; i++)
                {
                    var row = longdrugdt.Rows[rows[i]] as HemodialysisModel.MED_CURE_LONGDRUGRow;
                    row.DRUG_MODE = drugmode;
                }
                result = objHemodialysisService.SaveCureLongDrug(longdrugdt);
            }


            if (result >= 1)
            {
                XtraMessageBox.Show("药品信息保存成功.", "用药信息");
            }
            else
            {
                XtraMessageBox.Show("药品信息保存失败.", "用药信息");
            }

            loadCureDurgList(UserInfo.HEMODIALYSIS_ID);
        }

        private void ToolStripMenuItemForComCancle_Click(object sender, EventArgs e)
        {
            int result = -1;
            if (xtraTabControl3.SelectedTabPageIndex == 0)
            {
                //string COM_NOS = objHemodialysisService.GetOrderComNo();

                var rows = gridView5.GetSelectedRows();
                for (int i = 0; i < rows.Length; i++)
                {
                    var row = drugDt.Rows[rows[i]] as HemodialysisModel.MED_CURE_DRUGRow;

                    row.COM_NO = objHemodialysisService.GetOrderComNo();
                    row.COM_SUB_NO = "1";
                }
                result = objHemodialysisService.SaveCureDrug(drugDt);
            }
            else
            {
                //string COM_NOS = objHemodialysisService.GetOrderComNo();

                var rows = gridView7.GetSelectedRows();
                for (int i = 0; i < rows.Length; i++)
                {
                    var row = longdrugdt.Rows[rows[i]] as HemodialysisModel.MED_CURE_LONGDRUGRow;

                    row.COM_NO = objHemodialysisService.GetOrderComNo();
                    row.COM_SUB_NO = "1";
                }
                result = objHemodialysisService.SaveCureLongDrug(longdrugdt);
            }

            if (result >= 1)
            {
                XtraMessageBox.Show("药品信息保存成功.", "用药信息");
            }
            else
            {
                XtraMessageBox.Show("药品信息保存失败.", "用药信息");
            }
            loadCureDurgList(UserInfo.HEMODIALYSIS_ID);
        }

        private void ToolStripMenuItemForSaveTemplate_Click(object sender, EventArgs e)
        {
            saveDrugTemp();
        }

        private void btn_UseTemplate_Click(object sender, EventArgs e)
        {
            #region 调用模版
            bool islong = false;
            if (xtraTabControl3.SelectedTabPageIndex == 0)
            {
                islong = false;
            }
            else if (xtraTabControl3.SelectedTabPageIndex == 1)
            {
                islong = true;
            }

            CureDrugTemplateList frm = new CureDrugTemplateList();
            frm.CurrentHemoID = this.UserInfo.HEMODIALYSIS_ID;
            frm.CurrentPatientID = this.UserInfo.PatientID;
            frm.RecipeId = currentRecipeIdStr;
            frm.IsLong = islong;
            if (frm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                loadCureDurgList(UserInfo.HEMODIALYSIS_ID);
            }
            #endregion
        }

        private void txtDRUG_NAME_EditValueChanged(object sender, EventArgs e)
        {

            var _relationshift = new ConfigModel.MED_COMMON_RELATIONDataTable();
            _relationData.Where(i => i.RELATIONTYPE == "0").CopyToDataTable<ConfigModel.MED_COMMON_RELATIONRow>(_relationshift, LoadOption.PreserveChanges);
            if (txtDRUG_NAME.EditValue != null)
            {
                var row = _relationshift.FirstOrDefault(i => i.ITEMNAME.Trim() == this.txtDRUG_NAME.EditValue.ToString().Trim());

                if (row != null)
                {
                    txtDOSAGE.Text = row.DOSAGE;
                    cmbDOSAGE_UNITS.EditValue = row.UNIT;
                    cmbDRUG_MODE.EditValue = row.DRUGMODE;
                }
                else
                {
                    txtDOSAGE.Text = string.Empty;
                    cmbDOSAGE_UNITS.EditValue = string.Empty;
                    cmbDRUG_MODE.EditValue = string.Empty;
                }

            }
            //if (txtDRUG_NAME.Text == "左卡尼汀")
            //{
            //    txtDOSAGE.Text = "1";
            //    cmbDOSAGE_UNITS.EditValue = "abc34159-41a1-4e4c-8896-7fb8f418e5cf";
            //    cmbDRUG_MODE.EditValue = "d18bc2cf-a2f6-4544-ad1c-2dfe93b82d7d";
            //    lupDRUG_TIMETYPE.Text = "透析后";
            //}
            //else if (txtDRUG_NAME.Text == "益比奥")
            //{
            //    txtDOSAGE.Text = "10000";
            //    cmbDOSAGE_UNITS.EditValue = "74e7e438-7f88-4e51-b8f1-376023e803c1";
            //    cmbDRUG_MODE.EditValue = "db3a026e-404d-44f3-a474-00e9ad339b45";
            //    lupDRUG_TIMETYPE.Text = "透析后";
            //}
            //else if (txtDRUG_NAME.Text == "依普定")
            //{
            //    txtDOSAGE.Text = "6000";
            //    cmbDOSAGE_UNITS.EditValue = "74e7e438-7f88-4e51-b8f1-376023e803c1";
            //    cmbDRUG_MODE.EditValue = "db3a026e-404d-44f3-a474-00e9ad339b45";
            //    lupDRUG_TIMETYPE.Text = "透析后";
            //}
            //else
            //{
            //    txtDOSAGE.Text = "";
            //    cmbDOSAGE_UNITS.EditValue = "";
            //    cmbDRUG_MODE.EditValue = "";
            //    lupDRUG_TIMETYPE.Text = "";
            //}
        }

        private void btnSaveTemp_Click(object sender, EventArgs e)
        {
            saveDrugTemp();
        }

        private void radDRUG_RATE_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void txtDRUG_NAME_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (this.txtDRUG_NAME.Text.Trim() != string.Empty)
                {
                    //从药库同步药
                    int result = objDrug.DownDrugFromBaseByName(this.txtDRUG_NAME.Text.Trim());
                    txtDRUG_NAME.Properties.DataSource = objDrug.GetDrugMasterList();
                }
            }
        }

        private void QueryRecipeListUI_Load(object sender, EventArgs e)
        {
            LoadInfo(hemoId, tabIndex);
        }

        /// <summary>
        /// 添加新的给药记录
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAddDrug_Click(object sender, EventArgs e)
        {
            Utilities.BaseControlInfo.ClearControlText(panDrug);
            // 清空每周所有勾选项，防止残留上次新增时的选择
            for (int i = 0; i < cbmDRUG_DAYS.Properties.Items.Count; i++)
            {
                cbmDRUG_DAYS.Properties.Items[i].CheckState = CheckState.Unchecked;
            }
            cbmDRUG_DAYS.EditValue = null;
            cbmDRUG_DAYS.Properties.NullText = "";
            cbmDRUG_DAYS.Refresh();

            loadGiveDrugTime();
            txtDRUG_NAME.Focus();
            txtCURE_DRUG_ID.Text = System.Guid.NewGuid().ToString();
            //txtCure_ID.Text = objHemodialysisService.GetCureID(txtCREATE_DATE.EditValue.ToString(), UserInfo.HEMODIALYSIS_ID);
            HemodialysisModel.MED_HEMO_RECIPEDataTable recipeDt = objHemodialysisService.GetRecipeByHemodialysisID(UserInfo.HEMODIALYSIS_ID);
            if (recipeDt != null && recipeDt.Rows.Count > 0)
            {
                DataTable tmpDt = Utility.GetSubTable(((DataTable)recipeDt), "status = '1'");
                if (tmpDt != null && tmpDt.Rows.Count > 0)
                {
                    txtRECIPE_ID.Text = tmpDt.Rows[0]["RECIPE_ID"].ToString();
                }
            }
            isAddDrug = true;
            Utilities.BaseControlInfo.SetControlEnabled(panDrug, true);
            this.btnSaveDrug.Enabled = true;
            lopDOCTOR_ID.EditValue = HemoApplicationContext.Current.CurrentUser.EMP_NO;
            this.btnAddDrug.Enabled = false;
        }

        private void txtCREATE_DATE_EditValueChanged(object sender, EventArgs e)
        {
            if (!drugStatus)
            {
                loadCureDurgList(UserInfo.HEMODIALYSIS_ID);
                drugStatus = false;
            }
        }

        private void gridView5_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            var dr = gridView5.GetFocusedDataRow() as HemodialysisModel.MED_CURE_DRUGRow;
            if (dr != null && (dr.STATUS == "0" || dr.STATUS == "2"))
            {
                drugStatus = true;
                Utilities.BaseControlInfo.SetControlEnabled(panDrug, true);
                HemodialysisModel.MED_CURE_DRUGDataTable tmpCrue = new HemodialysisModel.MED_CURE_DRUGDataTable();
                tmpCrue.Rows.Add(dr.ItemArray);
                BaseControlInfo.SetControlDataByDataTable(tmpCrue, panDrug);
                cmbCreate_Time.Time = Utility.CDate(dr.CREATE_DATE.ToString());
                txtDRUG_NAME.EditValue = dr.IsDRUG_CODENull() ? "" : dr.DRUG_CODE.Trim();
                txtDOSAGE.Text = dr.IsDOSAGENull() ? string.Empty : dr.DOSAGE;
                cmbDOSAGE_UNITS.EditValue = dr.IsDOSAGE_UNITSNull() ? string.Empty : dr.DOSAGE_UNITS;
                cmbDRUG_MODE.EditValue = dr.IsDRUG_MODENull() ? string.Empty : dr.DRUG_MODE;
                tmpCrue = null;
                this.btnSaveDrug.Enabled = true;
            }
            else
            {
                HemodialysisModel.MED_CURE_DRUGDataTable tmpCrue = new HemodialysisModel.MED_CURE_DRUGDataTable();
                tmpCrue.Rows.Add(dr.ItemArray);
                BaseControlInfo.SetControlDataByDataTable(tmpCrue, panDrug);
                cmbCreate_Time.Time = Utility.CDate(dr.CREATE_DATE.ToString());
                txtDRUG_NAME.EditValue = dr.IsDRUG_CODENull() ? "" : dr.DRUG_CODE.Trim();
                txtDOSAGE.Text = dr.IsDOSAGENull() ? string.Empty : dr.DOSAGE;
                cmbDOSAGE_UNITS.EditValue = dr.IsDOSAGE_UNITSNull() ? string.Empty : dr.DOSAGE_UNITS;
                cmbDRUG_MODE.EditValue = dr.IsDRUG_MODENull() ? string.Empty : dr.DRUG_MODE;
                this.btnSaveDrug.Enabled = false;
                Utilities.BaseControlInfo.SetControlEnabled(panDrug, false);
            }
        }

        private void btnCloseDrug_Click(object sender, EventArgs e)
        {
            this.ParentForm.Close();
        }

        private void btnEditDrug_Click(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// 保存临时给药信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSaveDrug_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            int result = 0;
            if (string.IsNullOrEmpty(this.txtDRUG_NAME.Text.Trim()))
            {
                AutoClosedMsgBox.ShowForm("请录入药品名称!", this.Text, 1000, MessageBoxIcon.Warning);
                this.btnAddDrug.Enabled = true;
                return;
            }
            if (this.xtraTabControl3.SelectedTabPageIndex == 0) //保存临时医嘱
            {
                if (isAddDrug)
                {
                    drugDt.Clear();
                    txtCURE_DRUG_ID.Text = System.Guid.NewGuid().ToString();

                    dt = BaseControlInfo.GetDataTableByPanel(drugDt, panDrug, isAddDrug);
                }
                else
                {
                    dt = Utility.GetSubTable(drugDt as DataTable, "CURE_DRUG_ID = '" + txtCURE_DRUG_ID.Text + "'");
                    if (dt.Rows.Count > 0)
                    {
                        dt = BaseControlInfo.GetDataTableByPanel(dt, panDrug, isAddDrug);
                    }
                    else { return; }
                }
                if (dt != null && dt.Rows.Count > 0)
                {
                    dt.Rows[0]["DRUG_CODE"] = txtDRUG_NAME.EditValue;
                    dt.Rows[0]["DRUG_NAME"] = txtDRUG_NAME.Text.ToString();
                    dt.Rows[0]["CREATE_DATE"] = Utility.CDate(txtCREATE_DATE.Text + " " + cmbCreate_Time.Text);
                    dt.Rows[0]["HEMODIALYSIS_ID"] = UserInfo.HEMODIALYSIS_ID;
                    dt.Rows[0]["RECIPE_ID"] = currentRecipeIdStr;
                    dt.Rows[0]["PATIENT_ID"] = UserInfo.PatientID;
                    dt.Rows[0]["COM_NO"] = isAddDrug ? objHemodialysisService.GetOrderComNo() : dt.Rows[0]["COM_NO"];
                    dt.Rows[0]["COM_SUB_NO"] = isAddDrug ? "1" : dt.Rows[0]["COM_SUB_NO"];
                    dt.Rows[0]["STATUS"] = "0";//临时用药新开立状态
                    result = objHemodialysisService.SaveCureDrug((HemodialysisModel.MED_CURE_DRUGDataTable)dt);
                }

                if (result == 1)
                {
                    XtraMessageBox.Show("药品信息保存成功.", "用药信息");
                }
                else
                {
                    XtraMessageBox.Show("药品信息保存失败.", "用药信息");
                }
                loadCureDurgList(UserInfo.HEMODIALYSIS_ID);

                //   this.Close();

            }
            else  //保存长期医嘱
            {
                DataTable dtshort = new DataTable();

                if (isAddDrug)
                {
                    longdrugdt.Clear();
                    dt = BaseControlInfo.GetDataTableByPanel(longdrugdt, panDrug, isAddDrug);

                }
                else
                {
                    dt = Utility.GetSubTable(longdrugdt as DataTable, "CURE_DRUG_ID = '" + txtCURE_DRUG_ID.Text + "'");
                    if (dt.Rows.Count > 0)
                    {
                        dt = BaseControlInfo.GetDataTableByPanel(dt, panDrug, isAddDrug);
                    }
                    else { return; }
                }
                if (dt != null && dt.Rows.Count > 0)
                {
                    dt.Rows[0]["DRUG_CODE"] = txtDRUG_NAME.EditValue;
                    dt.Rows[0]["DRUG_NAME"] = txtDRUG_NAME.Text.ToString();
                    dt.Rows[0]["CREATE_DATE"] = Utility.CDate(txtCREATE_DATE.Text + " " + cmbCreate_Time.Text);
                    dt.Rows[0]["HEMODIALYSIS_ID"] = UserInfo.HEMODIALYSIS_ID;
                    dt.Rows[0]["PATIENT_ID"] = UserInfo.PatientID;
                    dt.Rows[0]["COM_NO"] = objHemodialysisService.GetOrderComNo();
                    dt.Rows[0]["COM_SUB_NO"] = "1";
                    if (!string.IsNullOrEmpty(cmbDRUG_TIMES.Properties.NullText))
                    {
                        dt.Rows[0]["DRUG_TIMES"] = cmbDRUG_TIMES.Properties.NullText.Trim();
                    }
                    if (cmbDRUG_TIMES.EditValue != null)
                    {
                        dt.Rows[0]["DRUG_TIMES"] = cmbDRUG_TIMES.EditValue;
                    }

                    dt.Rows[0]["STATUS"] = "0";//临时用药新开立状态
                    result = objHemodialysisService.SaveCureLongDrug((HemodialysisModel.MED_CURE_LONGDRUGDataTable)dt);

                    //长期医嘱保存完成后去baocunwei临时医嘱
                    drugDt = objHemodialysisService.GetCureDrugByHemoID(UserInfo.HEMODIALYSIS_ID, _currentDt);
                    var row = drugDt.FirstOrDefault(i => i.CURE_DRUG_ID == dt.Rows[0]["CURE_DRUG_ID"].ToString());
                    //var row = drugDt.FirstOrDefault();
                    bool stated = false;
                    if (row == null)
                        stated = true;
                    else
                        stated = false;
                    //2026-04-20 龙宇涵 将每周下拉框的数字改成中文
                    //2026-04-22 龙宇涵，注射方式为口服时不生成临时医嘱
                    string currentWeekChinese = ConvertWeekToChinese(System.DateTime.Now.DayOfWeek.ToString());
                    bool isOral = cmbDRUG_MODE.Text == "口服";
                    if (!isOral && dt.Rows[0]["drug_days"].ToString().Contains(currentWeekChinese) && stated)
                    {
                        drugDt.Clear();
                        dtshort = BaseControlInfo.GetDataTableByPanel(drugDt, panDrug, true);
                        dtshort.Rows[0]["CURE_DRUG_ID"] = row == null ? Guid.NewGuid().ToString() : row.CURE_DRUG_ID;
                        dtshort.Rows[0]["DRUG_CODE"] = txtDRUG_NAME.EditValue;
                        dtshort.Rows[0]["DRUG_NAME"] = txtDRUG_NAME.Text.ToString();
                        dtshort.Rows[0]["CREATE_DATE"] = Utility.CDate(_currentDt.Date.ToString("yyyy-MM-dd") + " " + DateTime.Now.ToString("HH:mm:ss"));
                        dtshort.Rows[0]["HEMODIALYSIS_ID"] = UserInfo.HEMODIALYSIS_ID;
                        dtshort.Rows[0]["PATIENT_ID"] = UserInfo.PatientID;
                        dtshort.Rows[0]["COM_NO"] = dt.Rows[0]["COM_NO"].ToString();
                        //20260414 刘建超 医生反馈长期医嘱生成临时医嘱，在“护士工作站”看不到医生开立的临时医嘱信息问题
                        dtshort.Rows[0]["RECIPE_ID"] = currentRecipeIdStr;
                        dtshort.Rows[0]["COM_SUB_NO"] = dt.Rows[0]["COM_SUB_NO"].ToString();
                        dtshort.Rows[0]["STATUS"] = "0";//临时用药新开立状态
                        dtshort.Rows[0]["drug_days"] = string.Empty;
                        result = objHemodialysisService.SaveCureDrug((HemodialysisModel.MED_CURE_DRUGDataTable)dtshort);
                        //长期医嘱生成临时医嘱后进行存储过程日志生成。确保存储过程不再去生成此条临时医嘱
                        //获取是否已经有生成日志，如果有日志就不再写入不然就要写入。
                        int k = objHemodialysisService.ExecuteProLogInfos();
                    }
                }

                if (result == 1)
                {
                    XtraMessageBox.Show("药品信息保存成功.", "用药信息");
                }
                else
                {
                    XtraMessageBox.Show("药品信息保存失败.", "用药信息");
                }
                loadCureDurgList(UserInfo.HEMODIALYSIS_ID);
            }
            isAddDrug = false;
            this.btnAddDrug.Enabled = true;
        }

        #endregion

        #region 方法

        public void LoadInfo(string pHemoID, int pTabIndex)
        {
            this.Text = "透析方案";

            ProFunctionCount pfc = new ProFunctionCount();
            pfc.SaveFunctionCountUI(this);

            loadLoopUpEdit();
            UserInfo.HEMODIALYSIS_ID = pHemoID;
            UserInfo.LoadPatientInfo();
            loadRecipeList(UserInfo.HEMODIALYSIS_ID);
            loadCureList(UserInfo.HEMODIALYSIS_ID);
            loadGiveDrugTime();
            loadCureDurgList(UserInfo.HEMODIALYSIS_ID);
            if (pTabIndex == 0)
            {
                xtraTabControl2.SelectedTabPage = xtraTabPage3;
                this.lupRECIPE_TYPE.EditValue = "1";
            }
            else if (pTabIndex == 1)
            {
                xtraTabControl2.SelectedTabPage = xtraTabPage4;
                this.lupRECIPE_TYPE.EditValue = "0";
            }
            this.cmbDRUG_TIMES.Visible = false;
            Utilities.BaseControlInfo.ClearControlText(panDrug);
            Utilities.BaseControlInfo.SetControlEnabled(panDrug, false);
            this.toolStripMenuItemForLong.Visible = false;
            //  loadChart();
            _relationData = this._configService.GetCommRelation();
        }

        private void loadChart(ReportChart.PatientRecipeFrm patientRecipe)
        {
            switch (patientRecipe.Name)
            {
                case "patientRecipeFrm1":
                    patientRecipe.Title = "干体重与抗凝变化趋势图";
                    patientRecipe.OnGetPatientRecipeChart += _patientService.GetPatientRecipeChart;
                    break;
                case "patientRecipeFrm2":
                    patientRecipe.Title = "干体重与抗凝变化趋势图";
                    patientRecipe.OnGetPatientRecipeChart += _patientService.GetPatientRecipeChart;
                    break;
                case "patientRecipeFrm3":
                    patientRecipe.Title = "spkt/V变化趋势图";
                    patientRecipe.OnGetPatientRecipeChart += _patientService.GetPatientRecipeSPKTChart;
                    break;
                case "patientRecipeFrm4":
                    patientRecipe.Title = "URR变化趋势图";
                    patientRecipe.OnGetPatientRecipeChart += _patientService.GetPatientRecipeURRChart;
                    break;
                default:
                    break;
            }
            patientRecipe.HemoId = UserInfo.HEMODIALYSIS_ID;

            patientRecipe.InzationDateControl();
            patientRecipe.InzationData();
            //this.patientRecipeFrm1.HemoId = UserInfo.HEMODIALYSIS_ID;

            //this.patientRecipeFrm1.InzationDateControl();
            //this.patientRecipeFrm1.InzationData();

            //  if (dtMain == null) {
            //     dtMain = objHemodialysisService.GetMainCureByHemoIDAndDate(UserInfo.HEMODIALYSIS_ID, System.DateTime.Now, System.DateTime.Now.AddMonths(-10));
            //    dtMain = Utility.GetSubTable(dtMain, "CURE_CREATE_DATE BETWEEN TO_DATE('" + System.DateTime.Now + "','yyyy-MM-dd') AND TO_DATE('" + System.DateTime.Now.AddMonths(-2) + "','yyyy-MM-dd')");
            // }

            // if (dtParam == null) {
            //     dtParam = objHemodialysisService.GetHemoParamsByHemoID(UserInfo.HEMODIALYSIS_ID);
            //   dtParam = Utility.GetSubTable(dtParam, "CREATE_DATE BETWEEN '" + System.DateTime.Now + "' AND '" + System.DateTime.Now.AddMonths(-2) + "'");
            // }

            //if (dtMain != null && dtMain.Rows.Count > 0 && loadTime == false) {
            //    ctlSignChart1.DrawWeightChart(dtMain);
            //    // ctlSignChart3.DrawBloowFlowChart(dtMain);
            //    ctlSignChart3.DrawDryWeight(dtMain);
            //    ctlSignChart4.DrawUFRChart(dtMain);
            //}

            //if (dtParam != null && dtParam.Rows.Count > 0 && loadTime == false) {
            //    ctlSignChart2.DrawPressureChart(dtParam);
            //}
            loadTime = true;
        }

        private void loadLoopUpEdit()
        {
            xtraTabPage3.Image = global::Hemo.Client.Properties.Resources.yizhu_s;
            xtraTabPage4.Image = global::Hemo.Client.Properties.Resources.drug_s;
            xtraTabPage7.Image = global::Hemo.Client.Properties.Resources.pie_chart_graph;
            xtraTabPage8.Image = global::Hemo.Client.Properties.Resources.pie_chart_graph;

            xtraTabPage5.Image = global::Hemo.Client.Properties.Resources.drug_temp;
            xtraTabPage6.Image = global::Hemo.Client.Properties.Resources.drug_long;
            xtraTabPage1.Image = global::Hemo.Client.Properties.Resources.long_recipe_s;

            DataTable dtType = new DataTable();
            dtType.Columns.Add(new DataColumn("ITEM_ID"));
            dtType.Columns.Add(new DataColumn("ITEM_NAME"));

            DataRow row = dtType.NewRow();
            row["ITEM_ID"] = "1";
            row["ITEM_NAME"] = "长期";
            dtType.Rows.Add(row);

            row = dtType.NewRow();
            row["ITEM_ID"] = "0";
            row["ITEM_NAME"] = "临时";
            dtType.Rows.Add(row);

            Utility.BindLookUpEdit(lupRECIPE_TYPE, "ITEM_ID", "ITEM_NAME", dtType, "ITEM_NAME", "类型");


            DataTable dtStatus = new DataTable();
            dtStatus.Columns.Add(new DataColumn("ITEM_ID"));
            dtStatus.Columns.Add(new DataColumn("ITEM_NAME"));

            DataRow rowStatus = dtStatus.NewRow();
            rowStatus = dtStatus.NewRow();
            rowStatus["ITEM_ID"] = "3";
            rowStatus["ITEM_NAME"] = "已执行";
            dtStatus.Rows.Add(rowStatus);

            Utility.BindLookUpEdit(cmbStatus, "ITEM_ID", "ITEM_NAME", dtStatus, "ITEM_NAME", "状态");
            string type = areaName.Equals("CRRT") ? "CRRT净化方式" : "净化方式";
            BaseControlInfo.BindLookUpEdit(cmbPURIFICATION_MODE, "ITEM_ID", "ITEM_NAME", _configService.GetConfigList(string.Empty, string.Empty, type, "1"), "ITEM_NAME", "净化方式");
            BaseControlInfo.BindLookUpEdit(cmbTHERAPEUTIC_METHOD, "ITEM_ID", "ITEM_NAME", this._configService.GetConfigList(string.Empty, string.Empty, "治疗方法", "1"), "ITEM_NAME", "治疗方法");


            //载入药品信息
            txtDRUG_NAME.Properties.DataSource = objDrug.GetDrugMasterList();
            txtDRUG_NAME.Properties.PopupFormSize = new Size(400, 230);
            txtDRUG_NAME.Properties.DisplayMember = "DRUG_NAME";//要显示的字段,Text获得
            txtDRUG_NAME.Properties.ValueMember = "DRUG_CODE";//实际值的字段,EditValue获得 // DeptID
            BaseControlInfo.BindLookUpEdit(cmbDOSAGE_UNITS, "ITEM_ID", "ITEM_NAME", this._configService.GetConfigList(string.Empty, string.Empty, "药品单位", "1"), "ITEM_NAME", "药品单位");
            BaseControlInfo.BindLookUpEdit(cmbDRUG_MODE, "ITEM_ID", "ITEM_NAME", this._configService.GetConfigList(string.Empty, string.Empty, "注射方式", "1"), "ITEM_NAME", "使用方式");
            DataTable dtStaffSict = _staffDictService.GetStaffDictList();
            DataTable dtDoctorList = Utility.GetSubTable(dtStaffSict, "ZYNAME='医生'");
            if (dtDoctorList != null && dtDoctorList.Rows.Count > 0)
            {
                BaseControlInfo.BindLookUpEdit(lopDOCTOR_ID, "EMP_NO", "NAME", dtDoctorList, "NAME", "开药医生");
            }
            lopDOCTOR_ID.EditValue = HemoApplicationContext.Current.CurrentUser.EMP_NO;

            DateTime dtNow = new DateTime(System.DateTime.Now.Year, System.DateTime.Now.Month, 1);
            this.txtBeginDate.EditValue = dtNow.AddMonths(-3).AddDays(-1);
            this.txtEndDate.EditValue = dtNow.AddMonths(1).AddDays(-1);

            DataTable dt = new DataTable();
            dt.Columns.Add("VALUE");
            dt.Columns.Add("TEXT");

            DataRow dr = dt.NewRow();
            dr["VALUE"] = "0";
            dr["TEXT"] = "";
            dt.Rows.Add(dr);

            DataRow dr1 = dt.NewRow();
            dr1["VALUE"] = "1";
            dr1["TEXT"] = "隔次";
            dt.Rows.Add(dr1);

            DataRow dr2 = dt.NewRow();
            dr2["VALUE"] = "2";
            dr2["TEXT"] = "每天";
            dt.Rows.Add(dr2);

            DataRow dr3 = dt.NewRow();
            dr3["VALUE"] = "3";
            dr3["TEXT"] = "每周";
            dt.Rows.Add(dr3);
            
            DataRow dr4 = dt.NewRow();
            dr4["VALUE"] = "4";
            dr4["TEXT"] = "其他";
            dt.Rows.Add(dr4);
            //DataRow dr4 = dt.NewRow();
            //dr4["VALUE"] = "4";
            //dr4["TEXT"] = "月";
            //dt.Rows.Add(dr4);

            BaseControlInfo.BindLookUpEdit(cmbDRUG_TIMES, "VALUE", "TEXT", dt, "TEXT", "选择");

            //cmbDRUG_TIMES.Properties.DataSource = dt;
            //cmbDRUG_TIMES.Properties.DisplayMember = "TEXT";//要显示的字段,Text获得
            //cmbDRUG_TIMES.Properties.ValueMember = "VALUE";//实
            //cmbDRUG_RATE.Properties.
            //checkedComboBoxEdit1.DataBindings =
        }

        private void loadRecipeList(string pHemoID)
        {
            DataTable dt = objHemodialysisService.GetMainCureListByHemoID(pHemoID);
            if (dt != null && dt.Rows.Count > 0)
            {
                gridRecipe.DataSource = dt;
                if (dt.Rows.Count >= 2)
                {
                    XtraMessageBox.Show("当前有多条有效医嘱，请确认只有一条有效医嘱方。", "医嘱下达");
                    return;
                }
            }
            else
            {
                gridRecipe.DataSource = null;
            }
        }

        private void loadCureList(string pHemoID)
        {
            busyIndicator1.ShowLoadingScreenFor(grdCureList);
            DataTable dt = null;
            using (BackgroundWorker worker = new BackgroundWorker())
            {
                worker.DoWork += delegate(object sender1, DoWorkEventArgs e1)
                {
                    dt = objHemodialysisService.GetQueryRecipeList(pHemoID);
                    //拼接过滤查询条件 
                    StringBuilder strSqlQuery = new StringBuilder();
                    strSqlQuery.Append("1=1");
                    if (cmbStatus.EditValue != null)
                    {
                        if (cmbStatus.EditValue.ToString() == "3")
                        {
                            strSqlQuery.Append(" and cure_status ='3'");
                        }
                    }
                    //if (lupRECIPE_TYPE.EditValue != null && lupRECIPE_TYPE.EditValue.ToString().Length > 0) {
                    //    strSqlQuery.Append(" and recipe_type ='" + lupRECIPE_TYPE.EditValue.ToString() + "'");
                    //}
                    if (cmbPURIFICATION_MODE.EditValue != null && cmbPURIFICATION_MODE.EditValue.ToString().Length > 0)
                    {
                        strSqlQuery.Append(" and PURIFICATION_MODE ='" + cmbPURIFICATION_MODE.EditValue.ToString() + "'");
                    }
                    if (cmbTHERAPEUTIC_METHOD.EditValue != null && cmbTHERAPEUTIC_METHOD.EditValue.ToString().Length > 0)
                    {
                        strSqlQuery.Append(" and therapeutic_method ='" + cmbTHERAPEUTIC_METHOD.EditValue.ToString() + "'");
                    }
                    strSqlQuery.Append(" and (status='0') and recipe_type ='0'");// /*or status ='2' or status ='1'*/
                    if (txtBeginDate.EditValue != null && txtBeginDate.EditValue.ToString().Length > 0)
                    {
                        strSqlQuery.Append(" and recipe_date >='" + txtBeginDate.EditValue.ToString() + "'");// /*or status ='2' or status ='1'*/
                    }

                    if (txtEndDate.EditValue != null && txtEndDate.EditValue.ToString().Length > 0)
                    {
                        strSqlQuery.Append(" and recipe_date <='" + txtEndDate.EditValue.ToString() + "'");// /*or status ='2' or status ='1'*/
                    }

                    dt = Utility.GetSubTable(dt, strSqlQuery.ToString(), "recipe_date desc");
                };
                worker.RunWorkerCompleted += delegate(object sender2, RunWorkerCompletedEventArgs e2)
                {
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        grdCureList.DataSource = dt;
                    }
                    else
                    {
                        grdCureList.DataSource = null;
                    }
                    this.busyIndicator1.HideLoadingScreen();
                };
                worker.RunWorkerAsync();
            }
        }

        private void showRecipeInfo(DataRow dr)
        {
            if (dr != null)
            {
                using (EditPrescribe frm = new EditPrescribe(UserInfo.HEMODIALYSIS_ID, dr["RECIPE_ID"].ToString()))
                {
                    if (dr["RECIPE_TYPE"].ToString() == "1")
                        frm.IsLong = true;
                    else
                        frm.IsLong = false;
                    frm.AreaName = areaName;
                    frm.ShowDialog();
                    if (frm.DialogResult == DialogResult.Yes)
                    {
                        loadRecipeList(UserInfo.HEMODIALYSIS_ID);
                        loadCureList(UserInfo.HEMODIALYSIS_ID);
                    }
                }
            }
        }

        private string ConvertToString(object o)
        {
            if (o == null)
                return string.Empty;
            if (o == DBNull.Value || o is DBNull)
                return string.Empty;
            return o.ToString().Trim();
        }

        private void saveDrugTemp()
        {
            HemodialysisModel.MED_CURE_DRUG_TEMPLATEDataTable drugTemplateDT = new HemodialysisModel.MED_CURE_DRUG_TEMPLATEDataTable();

            //定义模版名称
            //TemplateName tempName = new TemplateName();
            //if (tempName.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            //{
            //string templateName = tempName.CurrentTemplateName.Trim();

            string templateName = System.DateTime.Now.ToString();
            if (xtraTabControl3.SelectedTabPageIndex == 0)
            {
                #region 处理临时医嘱
                var rows = gridView5.GetSelectedRows();
                if (rows.Length <= 0)
                {
                    XtraMessageBox.Show("没有需要保存模板的医嘱信息，请选择医嘱再保存为模板.", "用药信息");
                    return;
                }
                for (int i = 0; i < rows.Length; i++)
                {
                    var row = drugDt.Rows[rows[i]] as HemodialysisModel.MED_CURE_DRUGRow;
                    var temRow = drugTemplateDT.NewMED_CURE_DRUG_TEMPLATERow();
                    temRow.TEMPLATE_ID = Guid.NewGuid().ToString();
                    temRow.TEMPLATE_NAME = templateName;
                    temRow.DRUG_CODE = ConvertToString(row.DRUG_CODE);
                    temRow.DRUG_NAME = ConvertToString(row.DRUG_NAME);
                    temRow.DRUG_TIMES = ConvertToString(row.IsDRUG_TIMESNull() ? null : row.DRUG_TIMES);
                    if (!row.IsADMINISTRATIONNull())
                    {
                        temRow.ADMINISTRATION = ConvertToString(row.ADMINISTRATION);
                    }
                    temRow.DOSAGE = ConvertToString(row.DOSAGE);
                    temRow.DOSAGE_UNITS = ConvertToString(row.DOSAGE_UNITS);
                    if (!row.IsREMARKNull())
                    {
                        temRow.REMARK = row.REMARK;
                    }
                    temRow.STATUS = ConvertToString(row.STATUS);

                    temRow.DOCTOR_ID = HemoApplicationContext.Current.CurrentUser.USER_ID;
                    temRow.CREATE_DATE = row.CREATE_DATE;
                    temRow.DRUG_MODE = ConvertToString(row.DRUG_MODE);
                    temRow.DRUG_TIMETYPE = ConvertToString(row.IsDRUG_TIMETYPENull() ? null : row.DRUG_TIMETYPE);
                    if (!row.IsDRUG_DAYSNull())
                    {
                        temRow.DRUG_DAYS = row.DRUG_DAYS;
                    }

                    temRow.PATIENT_ID = ConvertToString(row.PATIENT_ID);
                    temRow.HEMODIALYSIS_ID = ConvertToString(row.HEMODIALYSIS_ID);
                    temRow.COM_NO = ConvertToString(row.COM_NO);
                    temRow.COM_SUB_NO = ConvertToString(row.COM_SUB_NO);
                    drugTemplateDT.AddMED_CURE_DRUG_TEMPLATERow(temRow);
                }
                #endregion
            }
            else
            {
                #region 处理长期医嘱
                var rows = gridView7.GetSelectedRows();
                if (rows.Length <= 0)
                {
                    XtraMessageBox.Show("没有需要保存模板的医嘱信息，请选择医嘱再保存为模板.", "用药信息");
                    return;
                }
                for (int i = 0; i < rows.Length; i++)
                {
                    var row = longdrugdt.Rows[rows[i]] as HemodialysisModel.MED_CURE_LONGDRUGRow;
                    var temRow = drugTemplateDT.NewMED_CURE_DRUG_TEMPLATERow();
                    temRow.TEMPLATE_ID = Guid.NewGuid().ToString();
                    temRow.TEMPLATE_NAME = templateName;
                    temRow.DRUG_CODE = ConvertToString(row.DRUG_CODE.Trim());
                    temRow.DRUG_NAME = ConvertToString(row.DRUG_NAME.Trim());
                    temRow.DRUG_TIMES = ConvertToString(row.IsDRUG_TIMESNull() ? null : row.DRUG_TIMES);
                    if (!row.IsADMINISTRATIONNull())
                    {
                        temRow.ADMINISTRATION = ConvertToString(row.ADMINISTRATION);
                    }
                    temRow.DOSAGE = ConvertToString(row.DOSAGE);
                    temRow.DOSAGE_UNITS = ConvertToString(row.DOSAGE_UNITS);
                    if (!row.IsREMARKNull())
                    {
                        temRow.REMARK = row.REMARK;
                    }
                    temRow.STATUS = ConvertToString(row.STATUS);

                    temRow.DOCTOR_ID = HemoApplicationContext.Current.CurrentUser.USER_ID;
                    temRow.CREATE_DATE = row.CREATE_DATE;
                    temRow.DRUG_MODE = ConvertToString(row.DRUG_MODE);
                    temRow.DRUG_TIMETYPE = ConvertToString(row.IsDRUG_TIMETYPENull() ? null : row.DRUG_TIMETYPE);

                    temRow.DRUG_DAYS = ConvertToString(row.DRUG_DAYS);

                    temRow.PATIENT_ID = ConvertToString(row.PATIENT_ID);
                    temRow.HEMODIALYSIS_ID = ConvertToString(row.HEMODIALYSIS_ID);
                    temRow.COM_NO = ConvertToString(row.COM_NO);
                    temRow.COM_SUB_NO = ConvertToString(row.COM_SUB_NO);
                    drugTemplateDT.AddMED_CURE_DRUG_TEMPLATERow(temRow);
                }
                #endregion
            }
            int result = objHemodialysisService.SaveCureDrugTemplate(drugTemplateDT);
            if (result >= 1)
            {
                XtraMessageBox.Show("药品信息保存成功.", "用药信息");
            }
            else
            {
                XtraMessageBox.Show("药品信息保存失败.", "用药信息");
            }
            //}
        }

        private bool checkDrugInfo()
        {
            bool result = true;
            if (txtCure_ID.Text.Length == 0)
            {
                XtraMessageBox.Show("请选择一条有效的治疗单数据再为该病人开药。", "临时开药");
                return false;
            }
            return result;
        }

        private void loadGiveDrugTime()
        {
            txtCREATE_DATE.EditValue = System.DateTime.Now.ToShortDateString();
            cmbCreate_Time.EditValue = System.DateTime.Now.ToShortTimeString();
        }

        private bool isAddDrug = false;

        private string ConvertWeekToChinese(string dayofweek)
        {
            switch (dayofweek.ToUpper())
            {
                case "MONDAY": return "周一";
                case "TUESDAY": return "周二";
                case "WEDNESDAY": return "周三";
                case "THURSDAY": return "周四";
                case "FRIDAY": return "周五";
                case "SATURDAY": return "周六";
                case "SUNDAY": return "周日";
                default: return "";
            }
        }
        /// <summary>
        /// 根据透析号，载入病人当天治疗单信息
        /// </summary>
        /// <param name="pHemoID"></param>
        private void loadCureDurgList(string pHemoID)
        {
            drugDt = new HemodialysisModel.MED_CURE_DRUGDataTable();
            var drugDtTemp = new HemodialysisModel.MED_CURE_DRUGDataTable();

            //txtCure_ID.Text = objHemodialysisService.GetCureID(txtCREATE_DATE.EditValue.ToString(), pHemoID);
            //drugDt = objHemodialysisService.GetCureDrugByCureID(txtCure_ID.Text);   
            //if (drugDt != null && drugDt.Rows.Count > 0)
            //{
            //    gridDrugList.DataSource = drugDt;
            //}
            //else
            //{
            //    gridDrugList.DataSource = null;
            //}

            //上面的方法屏蔽
            drugDtTemp = !string.IsNullOrEmpty(currentRecipeIdStr) ? objHemodialysisService.GetCureDrugByHemoIDAndRecipeId(pHemoID, currentRecipeIdStr) : objHemodialysisService.GetValidCureDrugByHemoID(pHemoID, CurrentDt);
            if (drugDtTemp == null || drugDtTemp.Rows.Count <= 0)
            {
                drugDtTemp = objHemodialysisService.GetValidCureDrugByHemoID(pHemoID, CurrentDt);
            }

            drugDtTemp.Where(i => i.IsEXECUTE_STATUSNull() || i.EXECUTE_STATUS != "1").CopyToDataTable<HemodialysisModel.MED_CURE_DRUGRow>(drugDt, LoadOption.PreserveChanges);

            longdrugdt = objHemodialysisService.GetLongCureDrugByHemoID(pHemoID);


            if (drugDt != null && drugDt.Rows.Count > 0)
            {
                gridDrugList.DataSource = drugDt;
            }
            else
            {
                gridDrugList.DataSource = null;
            }

            if (longdrugdt != null && longdrugdt.Rows.Count > 0)
            {
                gridDrugListLong.DataSource = longdrugdt;
            }
            else
            {
                gridDrugListLong.DataSource = null;
            }
        }

        #endregion

        private void panelControl1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void xtraTabControl4_SelectedPageChanged(object sender, DevExpress.XtraTab.TabPageChangedEventArgs e)
        {
            if (this.xtraTabControl4.SelectedTabPageIndex == 0)
            {
                loadChart(this.patientRecipeFrm2);
            }

            if (this.xtraTabControl4.SelectedTabPageIndex == 1)
            {
                loadChart(this.patientRecipeFrm3);
            }

            if (this.xtraTabControl4.SelectedTabPageIndex == 2)
            {
                loadChart(this.patientRecipeFrm4);
            }
        }
        // 为每天和其他添加输入框 龙宇涵 2026-4-16
        private void cmbDRUG_TIMES_EditValueChanged(object sender, EventArgs e)
        {
            if (cmbDRUG_TIMES.EditValue == null) return;

            string val = cmbDRUG_TIMES.EditValue.ToString();
            string showText = "";
            cbmDRUG_DAYS.EditValue = null;
            cbmDRUG_DAYS.Properties.NullText = "";
            if (val == "2")
            {
                cbmDRUG_DAYS.EditValue = "周一,周二,周三,周四,周五,周六,周日";
                string input = Interaction.InputBox("请输入每日次数", "给药频率", "1");
                if (!string.IsNullOrWhiteSpace(input))
                {
                    showText = $"每天{input}次";
                }
            }
            else if (val == "4")
            {
                string input = Interaction.InputBox("请输入自定义频率", "给药频率", "");
                if (!string.IsNullOrWhiteSpace(input))
                {
                    showText = input;
                }
            }
            if (!string.IsNullOrEmpty(showText))
            {
                cmbDRUG_TIMES.Properties.NullText = showText;
                cmbDRUG_TIMES.EditValue = null;
                cmbDRUG_TIMES.Refresh();
            }
        }
    }
}