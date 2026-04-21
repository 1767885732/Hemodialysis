/*----------------------------------------------------------------
 * Copyright (C) 2005 (苏州)医疗科技发展有限公司
 * 文件功能描述:处方确认
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
using Hemo.Utilities;
using Hemo.Service;
using Hemo.IService;
using Hemo.IService.Config;
using Hemo.Client.Properties;
using Hemo.IService.PatientSchedule;
using Hemo.Client.UI.Lab;

namespace Hemo.Client.UI.Hemodialysis {
    public partial class EditRecipeConfirmList :HemoBaseFrm
    {
        #region 变量
        private IPatient objPatient = ServiceManager.Instance.PatientService;
        private IConfig _configService = ServiceManager.Instance.ConfigService;
        private IPatientSchedule _patientScheduleService = ServiceManager.Instance.PatientSchedule;
        private IHemodialysis _hemodialysisService = ServiceManager.Instance.HemodialysisService;

        DataTable patientDataTable = null;
        #endregion
        /// <summary>
        /// 构造函数
        /// </summary>
        public EditRecipeConfirmList() {
            InitializeComponent();
            InitList();
        }

        #region 事件
        /// <summary>
        /// 根据当前患者排班患者插入当天的处方数据
        /// </summary>
        private void insertPatientTodayRecipe() {
            int result = _hemodialysisService.CreatePatientRecipeBydate(Utility.CDate(txtStartDate.EditValue.ToString()));
        }

        private void btnQuery_Click(object sender, EventArgs e) {
            CalculateWeek();
        }

        private void InitList() {
            Hemo.Utilities.Utility.BindLookUpEdit(ediBanCi, "ITEM_ID", "ITEM_NAME", Utility.GetBanCi(), "ITEM_NAME", "班次");
            ediBanCi.Text = "全部";
            ConfigModel.MED_COMMON_ITEMLISTDataTable config = this._configService.GetConfigList(string.Empty, string.Empty, "区域", "1");
            if (config != null && config.Rows.Count > 0) {
                DataRow SickAreaRow = config.NewRow();
                SickAreaRow["ITEM_NAME"] = "全部";
                SickAreaRow["ITEM_ID"] = "c570d95c-76a2-4af4-893a-1357065623bf";
                SickAreaRow["ORDER_NUMBER"] = 0;
                config.Rows.InsertAt(SickAreaRow, 0);
                Hemo.Utilities.Utility.BindLookUpEdit(ediSickArea, "ITEM_ID", "ITEM_NAME", (DataTable)config, "ITEM_NAME", "区域");
            }
            ediSickArea.Text = "全部";
            txtStartDate.EditValue = System.DateTime.Now.ToShortDateString();
        }

        /// <summary>
        /// 根据选择条件查询对应的排班病人数据
        /// </summary>
        private void CalculateWeek() {
            busyIndicator1.Visible = true;
            busyIndicator1.ShowLoadingScreenFor(panelControl1);
            using (BackgroundWorker worker = new BackgroundWorker()) {
                worker.DoWork += delegate(object sender1, DoWorkEventArgs e1)
                {
                    if (txtStartDate.EditValue != null) {
                        DateTime startDate = Utility.CDate(txtStartDate.EditValue.ToString());
                        string strBanCi = string.Empty;
                        if (ediBanCi.EditValue == null || ediBanCi.EditValue.ToString() == "0") {
                            strBanCi = string.Empty;
                        }
                        else {
                            strBanCi = ediBanCi.EditValue.ToString();
                        }
                        string strSickArea = string.Empty;
                        if (ediSickArea.EditValue == null || ediSickArea.EditValue.ToString() == "c570d95c-76a2-4af4-893a-1357065623bf") {
                            strSickArea = string.Empty;
                        }
                        else {
                            strSickArea = ediSickArea.EditValue.ToString();
                        }
                        patientDataTable = objPatient.GetPatientListBySchedule(startDate, strSickArea, strBanCi);
                        //生成当日临时处方
                        insertPatientTodayRecipe();
                        patientDataTable.Columns.Add("PATIENT_HEAD_PORTRAIT", System.Type.GetType("System.Byte[]"));
                        patientDataTable.Columns.Add("PATIENT_TAG", System.Type.GetType("System.Byte[]"));
                        patientDataTable.Columns.Add("STATUS_TAG", System.Type.GetType("System.Byte[]"));
                        foreach (DataRow patientRow in patientDataTable.Rows) {
                            if (patientRow["SEX"].ToString() == "男") {
                                patientRow["PATIENT_HEAD_PORTRAIT"] = Utility.BitmapToBytes(Resources.boy);
                            }
                            else {
                                patientRow["PATIENT_HEAD_PORTRAIT"] = Utility.BitmapToBytes(Resources.gril);
                            }
                            if (patientRow["FOCUS_LEVEL"].ToString() == "1") {
                                patientRow["PATIENT_TAG"] = Utility.BitmapToBytes(Resources.favorite_love);
                            }
                            else if (patientRow["FOCUS_LEVEL"].ToString() == "0") {
                                patientRow["PATIENT_TAG"] = Utility.BitmapToBytes(Resources.favorite_love_0);
                            }
                            else {
                                patientRow["PATIENT_TAG"] = Utility.BitmapToBytes(Resources.Image1);
                            }
                            //有效的一条处方尚未确认
                            if (patientRow["purifier_model_id"].ToString().Length == 0) {
                                //patientRow["STATUS_TAG"] = "透析处方尚未确定!";
                                patientRow["STATUS_TAG"] = Utility.BitmapToBytes(Resources.delete_2);
                            }
                            else {
                                //patientRow["STATUS_TAG"] = "透析处方已经确定!";
                                patientRow["STATUS_TAG"] = Utility.BitmapToBytes(Resources.check_64);
                            }
                            if (patientRow["purification_mode_name"].ToString().Length == 0) {
                                // patientRow["STATUS_TAG"] = "有效处方尚未确定!";
                                patientRow["STATUS_TAG"] = Utility.BitmapToBytes(Resources.minus_2);
                            }
                        }
                    }
                    else {
                        if (txtStartDate.EditValue == null) {
                            XtraMessageBox.Show("请选择病人透析排班日期后，查询病人列表信息。", "病患管理");
                            return;
                        }
                    }
                };
                worker.RunWorkerCompleted += delegate(object sender2, RunWorkerCompletedEventArgs e2)
                {
                    if (patientDataTable != null && patientDataTable.Rows.Count > 0) {
                        gridPatientList.DataSource = patientDataTable;
                    }
                    else {
                        gridPatientList.DataSource = null;
                    }
                    // _ctlStartMain.LoadPatientListSchedule(patientDataTable);
                    // this._ctlStartMain.Dock = DockStyle.Fill;
                    // this.panelControl1.Controls.Add(this._ctlStartMain);
                    this.busyIndicator1.HideLoadingScreen();
                };
                worker.RunWorkerAsync();
            }

        }

        private void cardView1_CustomCardCaptionImage(object sender, DevExpress.XtraGrid.Views.Card.CardCaptionImageEventArgs e) {
            var row = cardView1.GetDataRow(e.RowHandle) as DataRow;
            if (row["SEX"].ToString() == "男") {
                e.ImageIndex = 0;
            }
            else {
                e.ImageIndex = 1;
            }
        }

        private void cardView1_MouseDown(object sender, MouseEventArgs e) {
            if (e.Button == MouseButtons.Right) {
                contextMenuStrip1.Show(MousePosition);
            }
        }

        private void cardView1_CustomDrawCardCaption(object sender, DevExpress.XtraGrid.Views.Card.CardCaptionCustomDrawEventArgs e) {
            if (this.patientDataTable.Rows[e.RowHandle]["purifier_model_name"].ToString().Length != 0) {
                e.Appearance.ForeColor = Color.Blue;
            }
        }

        private void EditRecipeConfirmList_Load(object sender, EventArgs e) {
            CalculateWeek();
            // insertPatientTodayRecipe();
        }

        private void btnConfirm_Click(object sender, EventArgs e) {

            if (cardView1.RowCount == 0) {
                XtraMessageBox.Show("请先安排病人透析排班信息，再确认处方信息。", "病患管理");
                return;
            }
            if (txtStartDate.EditValue == null) {
                XtraMessageBox.Show("请选择病人透析排班日期，再确认处方信息。", "病患管理");
                return;
            }

            if (XtraMessageBox.Show("确定确认当前病人列表透析处方吗？", this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                return;

            //确认确认的处方信息，更新排班表数据
            DataTable dt = gridPatientList.DataSource as DataTable;
            string HemoID = string.Empty;
            DataTable dtTemp = new DataTable();
            if (dt != null && dt.Rows.Count > 0) {
                DateTime startDate = Utility.CDate(txtStartDate.EditValue.ToString());
                string strBanCi = string.Empty;
                if (ediBanCi.EditValue == null || ediBanCi.EditValue.ToString() == "0") {
                    strBanCi = string.Empty;
                }
                else {
                    strBanCi = ediBanCi.EditValue.ToString();
                }
                string strSickArea = string.Empty;
                if (ediSickArea.EditValue == null || ediSickArea.EditValue.ToString() == "c570d95c-76a2-4af4-893a-1357065623bf") {
                    strSickArea = string.Empty;
                }
                else {
                    strSickArea = ediSickArea.EditValue.ToString();
                }

                PatientScheduleModel.MED_PATIENT_SCHEDULEDataTable patientScheduleDataTable = _patientScheduleService.GetPatientScheduleByParames(startDate, startDate, strBanCi, strSickArea);

                foreach (PatientScheduleModel.MED_PATIENT_SCHEDULERow rowSchedule in patientScheduleDataTable.Rows) {
                    HemoID = rowSchedule["HEMODIALYSIS_ID"].ToString();
                    HemodialysisModel.MED_HEMO_RECIPEDataTable recipeTable = this._hemodialysisService.GetRecipeByHemodialysisID(HemoID);
                    // dtTemp = Utility.GetSubTable(recipeTable, "TRUNC(recipe_date) = TRUNC(" + txtStartDate.EditValue.ToString() + ")") as HemodialysisModel.MED_HEMO_RECIPEDataTable;

                    if (recipeTable != null && recipeTable.Rows.Count > 0) {
                        rowSchedule["RECIPE_ID"] = recipeTable.Rows[0]["RECIPE_ID"].ToString();
                        rowSchedule["PURIFIER_MODEL_ID"] = recipeTable.Rows[0]["FIRST_PURIFIER_MODEL"].ToString();
                    }
                    else {
                        XtraMessageBox.Show("病人透析ID:" + HemoID + "的临时处方尚未创建,请先创建临时处方。", "病患管理");
                        return;
                    }
                }
                int result = _patientScheduleService.SavePatientScheduleInfo(patientScheduleDataTable);
                if (result > 0) {
                    XtraMessageBox.Show("当前病人列表中处方信息已经确认。", "病患管理");
                    CalculateWeek();
                }
            }
        }

        private void btnClose_Click(object sender, EventArgs e) {
            this.Close();
        }

        private void 患者处方ToolStripMenuItem_Click(object sender, EventArgs e) {
            DataRow row = cardView1.GetFocusedDataRow();

            if (row != null) {
                QueryRecipeList frm = new QueryRecipeList(row["HEMODIALYSIS_ID"].ToString(), 0);
                frm.ShowDialog();
            }
            else {
                XtraMessageBox.Show("请先选择一个病人！", "病患管理");
            }
        }

        private void 促红素ToolStripMenuItem_Click(object sender, EventArgs e) {
            //PatientModel.MED_PATIENTSRow patientRow = cardView1.GetFocusedDataRow() as PatientModel.MED_PATIENTSRow;
            //ErythropoietinFrm erythropoietinFrm = new ErythropoietinFrm(patientRow, false);
            //erythropoietinFrm.ShowDialog();
        }

        private void 药品医嘱ToolStripMenuItem_Click(object sender, EventArgs e) {
            DataRow row = cardView1.GetFocusedDataRow();
            if (row != null) {
                QueryRecipeList frm = new QueryRecipeList(row["HEMODIALYSIS_ID"].ToString(), 1);
                frm.ShowDialog();
            }
            else {
                XtraMessageBox.Show("请先选择一个病人！", "病患管理");
            }
        }

        private void 血管通路ToolStripMenuItem_Click(object sender, EventArgs e) {

            DataRow row = cardView1.GetFocusedDataRow();
            if (row != null) {
                EditVascularAccess frmEditVascularAccess = new EditVascularAccess(row["HEMODIALYSIS_ID"].ToString());
                frmEditVascularAccess.ShowDialog();
            }
            else {
                XtraMessageBox.Show("请先选择一个病人！", "病患管理");
            }
        }

        private void 检查检验ToolStripMenuItem_Click(object sender, EventArgs e) {
            // PatientModel.MED_PATIENTSRow patientRow = cardView1.GetFocusedDataRow() as PatientModel.MED_PATIENTSRow;
            //  LabFrm labFrm = new LabFrm(patientRow);
            //  labFrm.ShowDialog();
        }

        //private void 补录治疗记录单ToolStripMenuItem_Click(object sender, EventArgs e) {
        //    DataRow row = cardView1.GetFocusedDataRow();
        //    if (row != null) {
        //        string _strCureID = _hemodialysisService.GetCureID(System.DateTime.Now.ToString(), row["HEMODIALYSIS_ID"].ToString());
        //        EditTreatment frmTreatment = new EditTreatment(row["HEMODIALYSIS_ID"].ToString(), _strCureID, 0);
        //        frmTreatment.IsReplenishTreat = true;
        //        frmTreatment.ShowDialog();
        //    }
        //    else {
        //        XtraMessageBox.Show("请先选择一个病人！", "病患管理");
        //    }
        //}

        private void setPatientFocusLevel(string pLevel) {
            DataRow row = cardView1.GetFocusedDataRow();
            int result = 0;
            if (row != null) {
                DateTime startDate = Utility.CDate(txtStartDate.EditValue.ToString());
                PatientScheduleModel.MED_PATIENT_SCHEDULEDataTable dataTableSchedule = _patientScheduleService.GetPatientScheduleSignle(startDate, row["HEMODIALYSIS_ID"].ToString());
                if (dataTableSchedule != null && dataTableSchedule.Rows.Count > 0) {
                    PatientScheduleModel.MED_PATIENT_SCHEDULERow drSchedule = dataTableSchedule.Rows[0] as PatientScheduleModel.MED_PATIENT_SCHEDULERow;
                    drSchedule.FOCUS_LEVEL = pLevel;
                    result = _patientScheduleService.SavePatientScheduleInfo(dataTableSchedule);
                    if (result > 0) {
                        XtraMessageBox.Show("病人状态更新成功！", "病患管理");
                        CalculateWeek();
                    }
                }
            }
            else {
                XtraMessageBox.Show("请先选择一个病人！", "病患管理");
            }
        }

        private void 标注为重点ToolStripMenuItem_Click(object sender, EventArgs e) {
            setPatientFocusLevel("1");
        }

        private void 标注为一般ToolStripMenuItem_Click(object sender, EventArgs e) {
            setPatientFocusLevel("0");
        }

        private void 清空标注ToolStripMenuItem_Click(object sender, EventArgs e) {
            setPatientFocusLevel("");
        }

        /// <summary>
        /// 处方确认
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void 确认处方ToolStripMenuItem_Click(object sender, EventArgs e) {
            if (XtraMessageBox.Show("确定确认该病人透析处方吗？", this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                return;

            DataRow row = cardView1.GetFocusedDataRow();
            DataTable dtTemp = new DataTable();
            int result = 0;
            if (row != null) {
                DateTime startDate = Utility.CDate(txtStartDate.EditValue.ToString());
                PatientScheduleModel.MED_PATIENT_SCHEDULEDataTable dataTableSchedule = _patientScheduleService.GetPatientScheduleSignle(startDate, row["HEMODIALYSIS_ID"].ToString());
                if (dataTableSchedule != null && dataTableSchedule.Rows.Count > 0) {
                    PatientScheduleModel.MED_PATIENT_SCHEDULERow drSchedule = dataTableSchedule.Rows[0] as PatientScheduleModel.MED_PATIENT_SCHEDULERow;
                    //drSchedule.FOCUS_LEVEL = "";
                    HemodialysisModel.MED_HEMO_RECIPEDataTable recipeTable = this._hemodialysisService.GetRecipeByHemodialysisID(row["HEMODIALYSIS_ID"].ToString());
                    //dtTemp = Utility.GetSubTable(recipeTable, "status = 1") as HemodialysisModel.MED_HEMO_RECIPEDataTable;

                    if (recipeTable != null && recipeTable.Rows.Count > 0) {
                        drSchedule.RECIPE_ID = recipeTable.Rows[0]["RECIPE_ID"].ToString();
                        drSchedule.PURIFIER_MODEL_ID = recipeTable.Rows[0]["FIRST_PURIFIER_MODEL"].ToString();
                    }
                    else {
                        XtraMessageBox.Show("病人透析ID:" + row["HEMODIALYSIS_ID"].ToString() + "的临时处方尚未创建,请先创建临时处方。", "病患管理");
                        return;
                    }

                    result = _patientScheduleService.SavePatientScheduleInfo(dataTableSchedule);
                    if (result > 0) {
                        XtraMessageBox.Show("该病人处方已确认！", "病患管理");
                        CalculateWeek();
                    }
                }
            }
            else {
                XtraMessageBox.Show("请先选择一个病人！", "病患管理");
            }
        }

        private void 超滤率ToolStripMenuItem_Click(object sender, EventArgs e) {
            DataRow row = cardView1.GetFocusedDataRow();
            if (row != null) {
                EditUFR frm = new EditUFR(row["HEMODIALYSIS_ID"].ToString(), Utility.CDate(txtStartDate.EditValue.ToString()));
                frm.ShowDialog();
            }
        }
        #endregion
    }
}