/*----------------------------------------------------------------
 * Copyright (C) 2005 (苏州)医疗科技发展有限公司
 * 文件功能描述:通用类
 * 创建标识:吕志强-2013年7月9日
 * 
 * 修改时间:2013年10月17日
 * 修改人:刘超
 * 修改描述:修改方法SQL
 * 
 * 修改时间:2014年1月25日
 * 修改人:贺建操
 * 修改描述:新增方法SQL
 * 
 * 修改时间:2014年5月5日
 * 修改人:贺建操
 * 修改描述:修改方法
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
using Hemo.IService.Config;
using Hemo.Service;
using Hemo.Model;
using Hemo.IService.Dict;
using Hemo.Utilities;
using Hemo.Client.Core;
using Hemo.Client.Print;
using DevExpress.XtraReports.UI;

namespace Hemo.Client.UI.Hemodialysis
{
    /// <summary>
    /// 枚举
    /// </summary>
    public enum ButtonClickEnum
    {
        Add = 1,
        Edit = 2,
        None = 3
    }

    public partial class NursePerformanceAppraisal : HemoBaseFrm
    {
        #region 类变量

        private IStaffDict staffService = ServiceManager.Instance.StaffDictService;

        private IHemodialysis hemoService = ServiceManager.Instance.HemodialysisService;

        private IConfig configService = ServiceManager.Instance.ConfigService;

        private HemodialysisModel.MED_PERFORMANCE_APPRAISALDataTable dtAppraisal = null;

        private HemodialysisModel.MED_PERFORMANCE_APPRAISALRow currentAppraisal = null;

        private HemodialysisModel.MED_PERFORMANCE_APPRAISAL_RULEDataTable dtRule = null;

        private HemodialysisModel.MED_PERFORMANCE_APPRAISAL_RULEDataTable dtRuleItem = null;

        private HemodialysisModel.MED_PERFORMANCE_APPRAISAL_RULEDataTable dtAddItem = null;

        private HemodialysisModel.MED_PERFORMANCE_APPRAISAL_RULEDataTable dtMinusItem = null;

        private DictModel.MED_STAFF_DICTDataTable dtNurse = null;

        private DictModel.MED_STAFF_DICTDataTable dtLeader = null;

        private DictModel.MED_STAFF_DICTDataTable dtMember = null;

        private ConfigModel.MED_COMMON_ITEMLISTDataTable dtConfig = null;

        private ButtonClickEnum buttonClick = ButtonClickEnum.None;

        #endregion

        #region 构造函数

        public NursePerformanceAppraisal()
        {
            InitializeComponent();
        }

        #endregion

        #region 事件

        /// <summary>
        /// 加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NursePerformanceAppraisal_Load(object sender, EventArgs e)
        {
            LoadDefaultSet();
            BindLookUpEdit();
            LoadAppraisalList();
        }

        /// <summary>
        /// 查询绩效考核
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnQuery_Click(object sender, EventArgs e)
        {
            LoadAppraisalList();
        }

        /// <summary>
        /// 新增绩效考核
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (this.lupCheckNurse.EditValue == DBNull.Value)
            {
                XtraMessageBox.Show("请选择考核护士！", "提示");
                return;
            }

            buttonClick = ButtonClickEnum.Add;
            currentAppraisal = null;
            this.xtraTabControl1.SelectedTabPageIndex = 1;
        }

        /// <summary>
        /// 编辑绩效考核
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnEdit_Click(object sender, EventArgs e)
        {
            var row = this.gvAppraisal.GetFocusedDataRow();
            if (row != null)
            {
                buttonClick = ButtonClickEnum.Edit;
                currentAppraisal = row as HemodialysisModel.MED_PERFORMANCE_APPRAISALRow;
                this.xtraTabControl1.SelectedTabPageIndex = 1;
            }
        }

        /// <summary>
        /// 打印绩效考核
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnPrint_Click(object sender, EventArgs e)
        {
            NursePerformanceAppraisalReport report = new NursePerformanceAppraisalReport(dtAddItem, dtMinusItem, this.deBegin.DateTime, this.deEnd.DateTime, HemoApplicationContext.Current.CurrentUser.EMP_NO);
            ReportPrintTool tool = new ReportPrintTool(report);
            tool.ShowPreviewDialog();
        }

        /// <summary>
        /// 保存绩效评估
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSave_Click(object sender, EventArgs e)
        {
            DataSet dsItem = new DataSet("ITEM_LIST");
            DataTable dtItem = new DataTable("ITEM");
            dtItem.Columns.Add("ID", typeof(System.String));
            dtItem.Columns.Add("COUNT", typeof(System.String));

            for (int i = 0; i < this.gvAddScore.RowCount; i++)
            {
                var row = this.gvAddScore.GetDataRow(i) as HemodialysisModel.MED_PERFORMANCE_APPRAISAL_RULERow;
                string value = row["IS_CHECK"].ToString();
                if (value.Equals("1"))
                {
                    var rowItem = dtItem.NewRow();
                    rowItem["ID"] = row.ID;
                    rowItem["COUNT"] = row["ITEM_COUNT"];
                    dtItem.Rows.Add(rowItem);
                }
            }

            for (int i = 0; i < this.gvMinusScore.RowCount; i++)
            {
                var row = this.gvMinusScore.GetDataRow(i) as HemodialysisModel.MED_PERFORMANCE_APPRAISAL_RULERow;
                string value = row["IS_CHECK"].ToString();
                if (value.Equals("1"))
                {
                    var rowItem = dtItem.NewRow();
                    rowItem["ID"] = row.ID;
                    rowItem["COUNT"] = row["ITEM_COUNT"];
                    dtItem.Rows.Add(rowItem);
                }
            }

            dsItem.Tables.Add(dtItem);
            string xml = Utility.Transfer_DataSet_To_XML(dsItem);
            var dtResult = new HemodialysisModel.MED_PERFORMANCE_APPRAISALDataTable();

            if (currentAppraisal != null)
            {
                currentAppraisal.APPRAISAL_CONTENT = xml;
                dtResult.ImportRow(currentAppraisal);
            }
            else
            {
                var rowResult = dtResult.NewMED_PERFORMANCE_APPRAISALRow();
                rowResult.ID = Guid.NewGuid().ToString();
                rowResult.CHECK_NURSE = this.lupCheckNurse.EditValue.ToString();
                rowResult.APPRAISAL_CONTENT = xml;
                rowResult.CHECK_DATE = DateTime.Now;
                dtResult.AddMED_PERFORMANCE_APPRAISALRow(rowResult);
            }

            int result = hemoService.SavePerformanceAppraisal(dtResult);
            if (result > 0)
            {
                currentAppraisal = null;
                AutoClosedMsgBox.ShowForm("保存成功！", "提示", 1000, MessageBoxIcon.Information);
                LoadAppraisalList();
            }
        }

        /// <summary>
        /// 取消绩效评估更改
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancel_Click(object sender, EventArgs e)
        {
            if (currentAppraisal == null)
            {
                buttonClick = ButtonClickEnum.Add;
            }
            else
            {
                buttonClick = ButtonClickEnum.Edit;
            }
            LoadAppraisal();
        }

        /// <summary>
        /// 新增绩效考核规则类别
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAddType_Click(object sender, EventArgs e)
        {
            int orderNumber = 1;
            if (this.gvRuleType.RowCount > 0)
            {
                orderNumber = Utility.CInt(dtRule.Compute("Max(ORDER_NUMBER)", string.Empty).ToString()) + 1;
            }

            EditAppraisalRule appraisalRule = new EditAppraisalRule();
            appraisalRule.OrderNumber = orderNumber;
            DialogResult result = appraisalRule.ShowDialog(this);
            if (result == DialogResult.OK)
            {
                LoadRuleList();
            }
        }

        /// <summary>
        /// 编辑绩效考核规则类别
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnEditType_Click(object sender, EventArgs e)
        {
            var row = this.gvRuleType.GetFocusedDataRow();
            if (row != null)
            {
                EditAppraisalRule appraisalRule = new EditAppraisalRule();
                appraisalRule.CurrentRule = row as HemodialysisModel.MED_PERFORMANCE_APPRAISAL_RULERow;
                DialogResult result = appraisalRule.ShowDialog(this);
                if (result == DialogResult.OK)
                {
                    LoadRuleList();
                }
            }
        }

        /// <summary>
        /// 新增绩效考核规则条目
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAddItem_Click(object sender, EventArgs e)
        {
            int addOrderNumber = 1;
            int minusOrderNumber = 1;

            if (dtAddItem != null && dtAddItem.Rows.Count > 0)
            {
                addOrderNumber = Utility.CInt(dtAddItem.Compute("Max(ORDER_NUMBER)", string.Empty).ToString()) + 1;
            }
            if (dtMinusItem != null && dtMinusItem.Rows.Count > 0)
            {
                minusOrderNumber = Utility.CInt(dtMinusItem.Compute("Max(ORDER_NUMBER)", string.Empty).ToString()) + 1;
            }

            EditAppraisalRuleItem appraisalRuleItem = new EditAppraisalRuleItem();
            appraisalRuleItem.AddOrderNumber = addOrderNumber;
            appraisalRuleItem.MinusOrderNumber = minusOrderNumber;
            appraisalRuleItem.TypeTable = dtRule.DefaultView.ToTable(true, new string[] { "ID", "ITEM_NAME" });
            DialogResult result = appraisalRuleItem.ShowDialog(this);
            if (result == DialogResult.OK)
            {
                LoadRuleList();
            }
        }

        /// <summary>
        /// 编辑绩效考核规则条目
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnEditItem_Click(object sender, EventArgs e)
        {
            var row = this.gvRuleItem.GetFocusedDataRow();
            if (row != null)
            {
                EditAppraisalRuleItem appraisalRuleItem = new EditAppraisalRuleItem();
                appraisalRuleItem.CurrentRuleItem = row as HemodialysisModel.MED_PERFORMANCE_APPRAISAL_RULERow;
                appraisalRuleItem.TypeTable = dtRule.DefaultView.ToTable(true, new string[] { "ID", "ITEM_NAME" });
                DialogResult result = appraisalRuleItem.ShowDialog(this);
                if (result == DialogResult.OK)
                {
                    LoadRuleList();
                }
            }
        }

        /// <summary>
        /// 点击绩效考核规则类别行
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gvRuleType_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            var row = this.gvRuleType.GetFocusedDataRow() as HemodialysisModel.MED_PERFORMANCE_APPRAISAL_RULERow;
            if (row != null)
            {
                dtRuleItem = hemoService.GetPerformanceAppraisalRuleByType(row.ITEM_NAME);
                this.gcRuleItem.DataSource = dtRuleItem;
            }
        }

        /// <summary>
        /// 添加护士组长
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAddLeader_Click(object sender, EventArgs e)
        {
            var row = this.gvNurse.GetFocusedDataRow() as DictModel.MED_STAFF_DICTRow;
            if (row != null)
            {
                dtLeader = dtLeader ?? new DictModel.MED_STAFF_DICTDataTable();
                var findRow = dtLeader.FirstOrDefault(r => r.EMP_NO.Equals(row.EMP_NO));
                if (findRow == null)
                {
                    row.IS_LEADER = "1";
                    row.NURSE_LEADER = HemoApplicationContext.Current.CurrentUser.EMP_NO;
                    dtLeader.ImportRow(row);
                    dtLeader.AcceptChanges();
                }
            }
        }

        /// <summary>
        /// 移除护士组长
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnRemoveLeader_Click(object sender, EventArgs e)
        {
            var row = this.gvLeader.GetFocusedDataRow() as DictModel.MED_STAFF_DICTRow;
            if (row != null)
            {
                var nurseRow = dtNurse.FindByEMP_NO(row.EMP_NO);
                nurseRow.IS_LEADER = string.Empty;
                nurseRow.NURSE_LEADER = string.Empty;
                dtLeader.RemoveMED_STAFF_DICTRow(row);
                dtLeader.AcceptChanges();
            }
        }

        /// <summary>
        /// 添加护士组员
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAddMember_Click(object sender, EventArgs e)
        {
            var row = this.gvNurse.GetFocusedDataRow() as DictModel.MED_STAFF_DICTRow;
            if (row != null)
            {
                dtMember = dtMember ?? new DictModel.MED_STAFF_DICTDataTable();
                var findRow = dtMember.FirstOrDefault(r => r.EMP_NO.Equals(row.EMP_NO));
                if (findRow == null)
                {
                    var leaderRow = this.gvLeader.GetFocusedDataRow() as DictModel.MED_STAFF_DICTRow;
                    row.IS_LEADER = "0";
                    row.NURSE_LEADER = leaderRow.EMP_NO;
                    dtMember.ImportRow(row);
                    dtMember.AcceptChanges();
                }
            }
        }

        /// <summary>
        /// 移除护士组员
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnRemoveMember_Click(object sender, EventArgs e)
        {
            var row = this.gvMember.GetFocusedDataRow() as DictModel.MED_STAFF_DICTRow;
            if (row != null)
            {
                var nurseRow = dtNurse.FindByEMP_NO(row.EMP_NO);
                row.IS_LEADER = string.Empty;
                nurseRow.NURSE_LEADER = string.Empty;
                dtMember.RemoveMED_STAFF_DICTRow(row);
                dtMember.AcceptChanges();
            }
        }

        /// <summary>
        /// 点击护士组长列表行
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gvLeader_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            var row = this.gvLeader.GetFocusedDataRow() as DictModel.MED_STAFF_DICTRow;
            if (row != null)
            {
                dtMember = staffService.GetStaffDictByNurseLeader(row.EMP_NO);
                this.gcMember.DataSource = dtMember;
            }
        }

        /// <summary>
        /// 保存护士
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSave2_Click(object sender, EventArgs e)
        {
            int result = staffService.SaveStaffDictInfo(dtNurse);
            if (result > 0)
            {
                AutoClosedMsgBox.ShowForm("保存成功！", "提示", 1000, MessageBoxIcon.Information);
                BindLookUpEdit();
            }
        }

        /// <summary>
        /// 取消护士更改
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancel2_Click(object sender, EventArgs e)
        {
            LoadNurseList();
        }

        private void worker_DoWork(object sender, DoWorkEventArgs e)
        {
            if (this.lupCheckNurse.EditValue == DBNull.Value)
            {
                ConfigModel.MED_COMMON_ITEMLISTRow row = null;
                if (dtConfig != null && dtConfig.Rows.Count > 0)
                {
                    row = dtConfig.FirstOrDefault(r => r.ITEM_VALUE.Equals(HemoApplicationContext.Current.CurrentUser.EMP_NO));
                }
                if (row != null)
                {
                    dtAppraisal = hemoService.GetPerformanceAppraisalByDate(this.deBegin.DateTime, this.deEnd.DateTime);
                }
                else
                {
                    dtAppraisal = hemoService.GetPerformanceAppraisalByDateAndNurseLeader(this.deBegin.DateTime, this.deEnd.DateTime, HemoApplicationContext.Current.CurrentUser.EMP_NO);
                }
            }
            else
            {
                dtAppraisal = hemoService.GetPerformanceAppraisalByDateAndNurse(this.deBegin.DateTime, this.deEnd.DateTime, this.lupCheckNurse.EditValue.ToString());
            }

            dtAddItem = hemoService.GetPerformanceAppraisalRuleByScoreType("1");
            dtMinusItem = hemoService.GetPerformanceAppraisalRuleByScoreType("0");
        }

        private void worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (dtAppraisal != null && dtAppraisal.Rows.Count > 0)
            {
                dtAppraisal.Columns.Add("ADD_SCORE", typeof(System.Decimal));
                dtAppraisal.Columns.Add("MINUS_SCORE", typeof(System.Decimal));
                dtAppraisal.Columns.Add("TOTAL_SCORE", typeof(System.Decimal));

                var dtTotal = dtAddItem.Copy() as HemodialysisModel.MED_PERFORMANCE_APPRAISAL_RULEDataTable;
                dtMinusItem.CopyToDataTable(dtTotal, LoadOption.OverwriteChanges);
                dtTotal.Where(row => row.STATUS.Equals("1")).CopyToDataTable<HemodialysisModel.MED_PERFORMANCE_APPRAISAL_RULERow>(dtTotal, LoadOption.OverwriteChanges);

                foreach (HemodialysisModel.MED_PERFORMANCE_APPRAISALRow row in dtAppraisal.Rows)
                {
                    decimal addScore = 0;
                    decimal minusScore = 0;
                    var dtContent = Utility.Transfer_XML_To_DataTable(row.APPRAISAL_CONTENT);

                    if (dtContent != null && dtContent.Rows.Count > 0)
                    {
                        foreach (DataRow content in dtContent.Rows)
                        {
                            var item = dtTotal.FindByID(content["ID"].ToString());
                            if (item.SCORE_TYPE.Equals("1"))
                            {
                                addScore += item.ITEM_VALUE * (content["COUNT"] != DBNull.Value ? Utility.CInt(content["COUNT"].ToString()) : 1);
                            }
                            else if (item.SCORE_TYPE.Equals("0"))
                            {
                                minusScore += item.ITEM_VALUE * (content["COUNT"] != DBNull.Value ? Utility.CInt(content["COUNT"].ToString()) : 1);
                            }
                        }
                    }

                    row["ADD_SCORE"] = addScore;
                    row["MINUS_SCORE"] = minusScore;
                    row["TOTAL_SCORE"] = addScore - minusScore;
                }
            }

            this.gcAppraisal.DataSource = dtAppraisal;
            this.busyIndicator1.HideLoadingScreen();
        }

        private void worker2_DoWork(object sender, DoWorkEventArgs e)
        {
            dtRule = hemoService.GetPerformanceAppraisalRuleByType("绩效考核");
            dtAddItem = hemoService.GetPerformanceAppraisalRuleByScoreType("1");
            dtMinusItem = hemoService.GetPerformanceAppraisalRuleByScoreType("0");
        }

        private void worker2_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            this.gcRuleType.DataSource = dtRule;
            var row = this.gvRuleType.GetFocusedDataRow() as HemodialysisModel.MED_PERFORMANCE_APPRAISAL_RULERow;
            if (row != null)
            {
                dtRuleItem = hemoService.GetPerformanceAppraisalRuleByType(row.ITEM_NAME);
                this.gcRuleItem.DataSource = dtRuleItem;
            }

            this.busyIndicator2.HideLoadingScreen();
        }

        private void worker3_DoWork(object sender, DoWorkEventArgs e)
        {
            dtNurse = staffService.GetStaffDictList();
            dtLeader = staffService.GetStaffDictByLeaderFlag("1");
        }

        private void worker3_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (dtNurse != null && dtNurse.Rows.Count > 0)
            {
                dtNurse = Utility.GetSubTable(dtNurse, "ZYNAME='护士'", "NAME") as DictModel.MED_STAFF_DICTDataTable;
                this.gcNurse.DataSource = dtNurse;
            }

            dtLeader.DefaultView.Sort = "NAME";
            this.gcLeader.DataSource = dtLeader;

            var row = this.gvLeader.GetFocusedDataRow() as DictModel.MED_STAFF_DICTRow;
            if (row != null)
            {
                dtMember = staffService.GetStaffDictByNurseLeader(row.EMP_NO);
                dtMember.DefaultView.Sort = "NAME";
                this.gcMember.DataSource = dtMember;
            }

            this.busyIndicator3.HideLoadingScreen();
        }

        /// <summary>
        /// 选项卡界面切换
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void xtraTabControl1_SelectedPageChanged(object sender, DevExpress.XtraTab.TabPageChangedEventArgs e)
        {
            if (e.Page.Name.Equals(this.xtraTabPage2.Name))
            {
                if (buttonClick == ButtonClickEnum.Add)
                {
                    this.lblCheckNurse.Text = this.lupCheckNurse.Text;
                }
                else if (buttonClick == ButtonClickEnum.Edit)
                {
                    this.lblCheckNurse.Text = this.gvAppraisal.GetFocusedRowCellValue("NURSE_NAME").ToString();
                }
                LoadAppraisal();
            }
            else if (e.Page.Name.Equals(this.xtraTabPage3.Name))
            {
                if (this.gcRuleType.DataSource == null)
                {
                    LoadRuleList();
                }
            }
            else if (e.Page.Name.Equals(this.xtraTabPage4.Name))
            {
                if (this.gcNurse.DataSource == null)
                {
                    LoadNurseList();
                }
            }
        }

        #endregion

        #region 方法

        /// <summary>
        /// 下拉项绑定
        /// </summary>
        private void BindLookUpEdit()
        {
            DataTable dtNurse = null;
            DataTable dtNurse2 = null;
            ConfigModel.MED_COMMON_ITEMLISTRow row = null;
            if (dtConfig != null && dtConfig.Rows.Count > 0)
            {
                row = dtConfig.FirstOrDefault(r => r.ITEM_VALUE.Equals(HemoApplicationContext.Current.CurrentUser.EMP_NO));
            }
            if (row != null)
            {
                dtNurse = staffService.GetStaffDictByLeaderFlag("1");
                dtNurse2 = staffService.GetStaffDictByLeaderFlag("0");
                dtNurse2.AsEnumerable().ToList().ForEach(r => dtNurse.ImportRow(r));
            }
            else
            {
                dtNurse = staffService.GetStaffDictByNurseLeader(HemoApplicationContext.Current.CurrentUser.EMP_NO);
            }
            dtNurse.DefaultView.Sort = "NAME ASC";
            BaseControlInfo.BindLookUpEdit(this.lupCheckNurse, "EMP_NO", "NAME", dtNurse, "NAME", "考核护士");
        }

        /// <summary>
        /// 加载默认设置
        /// </summary>
        private void LoadDefaultSet()
        {
            this.deBegin.DateTime = DateTime.Now.AddDays(-DateTime.Now.Day).AddDays(1);
            this.deEnd.DateTime = DateTime.Now.AddMonths(1).AddDays(-DateTime.Now.Day);
            this.groupControl5.Width = this.groupControl3.Width = this.groupControl4.Width = (this.xtraTabPage4.Width - this.panelControl7.Width) / 2;
            this.groupControl3.Height = this.groupControl4.Height = this.xtraTabPage4.Height / 2;
            this.btnAddLeader.Location = new Point(this.btnAddLeader.Location.X, this.groupControl3.Location.Y + this.groupControl3.Height / 2 - this.btnAddLeader.Height - 10);
            this.btnRemoveLeader.Location = new Point(this.btnRemoveLeader.Location.X, this.groupControl3.Location.Y + this.groupControl3.Height / 2 + 10);
            this.btnAddMember.Location = new Point(this.btnAddMember.Location.X, this.groupControl4.Location.Y + this.groupControl4.Height / 2 - this.btnAddMember.Height - 10);
            this.btnRemoveMember.Location = new Point(this.btnRemoveMember.Location.X, this.groupControl4.Location.Y + this.groupControl4.Height / 2 + 10);

            dtConfig = configService.GetConfigList(string.Empty, string.Empty, "护士长", "1");
            ConfigModel.MED_COMMON_ITEMLISTRow row = null;
            if (dtConfig != null && dtConfig.Rows.Count > 0)
            {
                if (HemoApplicationContext.Current.CurrentUser.LOGIN_NAME.Equals("admin"))
                {
                    HemoApplicationContext.Current.CurrentUser.EMP_NO = "10000075";
                }
                row = dtConfig.FirstOrDefault(r => r.ITEM_VALUE.Equals(HemoApplicationContext.Current.CurrentUser.EMP_NO));
            }

            if (row != null)
            {
                //护士长具有规则制定、护士分组的权限
                SetControlEnabled(true);
            }
            else
            {
                SetControlEnabled(false);
            }
        }

        /// <summary>
        /// 设置控件是否可用
        /// </summary>
        /// <param name="flag"></param>
        private void SetControlEnabled(bool flag)
        {
            this.btnAddType.Enabled = flag;
            this.btnEditType.Enabled = flag;
            this.btnAddItem.Enabled = flag;
            this.btnEditItem.Enabled = flag;
            this.btnAddLeader.Enabled = flag;
            this.btnRemoveLeader.Enabled = flag;
            this.btnAddMember.Enabled = flag;
            this.btnRemoveMember.Enabled = flag;
            this.btnSave2.Enabled = flag;
            this.btnCancel2.Enabled = flag;
        }

        /// <summary>
        /// 加载绩效考核列表
        /// </summary>
        private void LoadAppraisalList()
        {
            this.busyIndicator1.ShowLoadingScreenFor(this.gcAppraisal);
            BackgroundWorker worker = new BackgroundWorker();
            worker.DoWork += new DoWorkEventHandler(worker_DoWork);
            worker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(worker_RunWorkerCompleted);
            worker.RunWorkerAsync();
        }

        /// <summary>
        /// 加载规则列表
        /// </summary>
        private void LoadRuleList()
        {
            this.busyIndicator2.ShowLoadingScreenFor(this.gcRuleType);
            BackgroundWorker worker = new BackgroundWorker();
            worker.DoWork += new DoWorkEventHandler(worker2_DoWork);
            worker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(worker2_RunWorkerCompleted);
            worker.RunWorkerAsync();
        }

        /// <summary>
        /// 加载护士列表
        /// </summary>
        private void LoadNurseList()
        {
            this.busyIndicator3.ShowLoadingScreenFor(this.gcNurse);
            BackgroundWorker worker = new BackgroundWorker();
            worker.DoWork += new DoWorkEventHandler(worker3_DoWork);
            worker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(worker3_RunWorkerCompleted);
            worker.RunWorkerAsync();
        }

        /// <summary>
        /// 加载绩效评估
        /// </summary>
        private void LoadAppraisal()
        {
            if (buttonClick == ButtonClickEnum.None)
            {
                if (this.gcAddScore.DataSource != null && this.gvAddScore.RowCount > 0)
                {
                    return;
                }
            }

            if (this.xtraTabControl2.SelectedTabPageIndex == 0)
            {
                this.busyIndicator4.ShowLoadingScreenFor(this.gcAddScore);
            }
            else
            {
                this.busyIndicator5.ShowLoadingScreenFor(this.gcMinusScore);
            }

            if (dtAddItem != null && dtAddItem.Rows.Count > 0)
            {
                var dtAdd = dtAddItem.Copy() as HemodialysisModel.MED_PERFORMANCE_APPRAISAL_RULEDataTable;
                dtAdd.Where(row => row.STATUS.Equals("1")).CopyToDataTable<HemodialysisModel.MED_PERFORMANCE_APPRAISAL_RULERow>(dtAdd, LoadOption.OverwriteChanges);
                dtAdd.Columns.Add("IS_CHECK", typeof(System.String));
                dtAdd.Columns.Add("ITEM_COUNT", typeof(System.Decimal));
                this.gcAddScore.DataSource = dtAdd;
                this.gvAddScore.BestFitColumns();
            }
            if (dtMinusItem != null && dtMinusItem.Rows.Count > 0)
            {
                var dtMinus = dtMinusItem.Copy() as HemodialysisModel.MED_PERFORMANCE_APPRAISAL_RULEDataTable;
                dtMinus.Where(row => row.STATUS.Equals("1")).CopyToDataTable<HemodialysisModel.MED_PERFORMANCE_APPRAISAL_RULERow>(dtMinus, LoadOption.OverwriteChanges);
                dtMinus.Columns.Add("IS_CHECK", typeof(System.String));
                dtMinus.Columns.Add("ITEM_COUNT", typeof(System.Decimal));
                this.gcMinusScore.DataSource = dtMinus;
                this.gvMinusScore.BestFitColumns();
            }

            if (buttonClick == ButtonClickEnum.Edit)
            {
                //初始化绩效评估
                var row = this.gvAppraisal.GetFocusedDataRow() as HemodialysisModel.MED_PERFORMANCE_APPRAISALRow;
                var dtContent = Utility.Transfer_XML_To_DataTable(row.APPRAISAL_CONTENT);
                if (dtContent != null && dtContent.Rows.Count > 0)
                {
                    var dtAdd = this.gcAddScore.DataSource as HemodialysisModel.MED_PERFORMANCE_APPRAISAL_RULEDataTable;
                    var dtMinus = this.gcMinusScore.DataSource as HemodialysisModel.MED_PERFORMANCE_APPRAISAL_RULEDataTable;

                    foreach (DataRow content in dtContent.Rows)
                    {
                        var addItem = dtAdd != null ? dtAdd.FindByID(content["ID"].ToString()) : null;
                        if (addItem != null)
                        {
                            addItem["IS_CHECK"] = "1";
                            addItem["ITEM_COUNT"] = content["COUNT"];
                        }

                        var minusItem = dtMinus != null ? dtMinus.FindByID(content["ID"].ToString()) : null;
                        if (minusItem != null)
                        {
                            minusItem["IS_CHECK"] = "1";
                            minusItem["ITEM_COUNT"] = content["COUNT"];
                        }
                    }

                    if (dtAdd != null) { dtAdd.AcceptChanges(); }
                    if (dtMinus != null) { dtMinus.AcceptChanges(); }
                }
            }

            if (this.xtraTabControl2.SelectedTabPageIndex == 0)
            {
                this.busyIndicator4.HideLoadingScreen();
            }
            else
            {
                this.busyIndicator5.HideLoadingScreen();
            }

            buttonClick = ButtonClickEnum.None;
        }

        #endregion
    }
}