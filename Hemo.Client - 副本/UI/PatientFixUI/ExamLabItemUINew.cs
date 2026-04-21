/*----------------------------------------------------------------
 * Copyright (C) 2005 (苏州)医疗科技发展有限公司
 * 文件功能描述:新患者检验检查查询类
 * 创建标识:吕志强-2017年6月15日
 * ----------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Hemo.Client.UI.Machine;
using Hemo.Service;
using Hemo.IService.Lab;
using Hemo.Utilities;
using DevExpress.XtraPrinting;
using Hemo.Client.Controls;
using Hemo.IService.Config;
using Hemo.Model;
using System.Linq;
using DevExpress.XtraNavBar;

namespace Hemo.Client.UI.PatientFixUI
{
    public partial class ExamLabItemUINew : ViewBase
    {
        #region 类变量

        private ILab labService = ServiceManager.Instance.LabService;

        private IConfig configService = ServiceManager.Instance.ConfigService;

        private string patientId = string.Empty;

        private string patientName = string.Empty;

        private string hemoId = string.Empty;

        private string itemName = string.Empty;

        private Color foreColor;

        private ConfigModel.MED_COMMON_ITEMLISTDataTable dtItem = null;

        private DataSet dsResult = null;

        #region 检验结果DataTable-原来代码

        //private DataTable dtSQSX = null;

        //private DataTable dtYGLDB = null;

        //private DataTable dtMDXQSY = null;

        //private DataTable dtYGBDDNA = null;

        //private DataTable dtBGBDRNA = null;

        //private DataTable dtXXBFX = null;

        //private DataTable dtNCGCZ = null;

        //private DataTable dtDBCG = null;

        //private DataTable dtDBQX = null;

        //private DataTable dtDJZQX = null;

        //private DataTable dtGGSYX = null;

        //private DataTable dtSGSX = null;

        //private DataTable dtXZSX = null;

        //private DataTable dtSHSSSX = null;

        //private DataTable dtJZSH = null;

        //private DataTable dtJZPXJS = null;

        //private DataTable dtTDB = null;

        //private DataTable dtTZTJHL = null;

        //private DataTable dtNXSX = null;

        //private DataTable dtDEJT = null;

        //private DataTable dtNLNT = null;

        //private DataTable dtCMCFYDB = null;

        //private DataTable dtXGSX = null;

        //private DataTable dtJGDB = null;

        #endregion

        private DataTable dtLabExport = null;

        private ViewBase currentParent = null;

        #endregion

        #region 属性

        public string HemoId
        {
            get { return hemoId; }
            set { hemoId = value; }
        }

        public string PatientId
        {
            get { return patientId; }
            set { patientId = value; }
        }

        public string PatientName
        {
            get { return patientName; }
            set { patientName = value; }
        }

        public ViewBase CurrentParent
        {
            get { return currentParent; }
            set { currentParent = value; }
        }

        #endregion

        #region 构造函数

        public ExamLabItemUINew()
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
        private void ExamLabItemUINew_Load(object sender, EventArgs e)
        {
            LoadMenuItems();
            foreColor = this.navBarControl1.Items[0].Appearance.ForeColor;
            //foreColor = this.navBarItem1.Appearance.ForeColor;
            LoadResultList();
        }

        private void work_DoWork(object sender, DoWorkEventArgs e)
        {
            LoadLabResult();
        }

        private void work_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            RefreshResultList();
            this.busyIndicator.HideLoadingScreen();
        }

        #region 点击项目-原来代码

        /// <summary>
        /// 术前四项
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void navBarItem1_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            //SetExamLabItem(e.Link.Item.Caption);
            //SetBarItemAppearance(e);
            //if (this.gcResult.DataSource != dtSQSX)
            //{
            //    this.gvResult.Columns.Clear();
            //    this.gcResult.DataSource = dtSQSX;
            //    dtLabExport = dtSQSX;
            //    this.gvResult.BestFitColumns();

            //}
        }

        /// <summary>
        /// 乙肝两对半
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void navBarItem2_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            //SetExamLabItem(e.Link.Item.Caption);
            //SetBarItemAppearance(e);
            //if (this.gcResult.DataSource != dtYGLDB)
            //{
            //    this.gvResult.Columns.Clear();
            //    this.gcResult.DataSource = dtYGLDB;
            //    dtLabExport = dtYGLDB;
            //    this.gvResult.BestFitColumns();
            //}
        }

        /// <summary>
        /// 梅毒血清试验（TRUST法）+滴
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void navBarItem3_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            //SetExamLabItem(e.Link.Item.Caption);
            //SetBarItemAppearance(e);
            //if (this.gcResult.DataSource != dtMDXQSY)
            //{
            //    this.gvResult.Columns.Clear();
            //    this.gcResult.DataSource = dtMDXQSY;
            //    dtLabExport = dtMDXQSY;
            //    this.gvResult.BestFitColumns();
            //}
        }

        /// <summary>
        /// 乙肝病毒DNA检测
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void navBarItem4_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            //SetExamLabItem(e.Link.Item.Caption);
            //SetBarItemAppearance(e);
            //if (this.gcResult.DataSource != dtYGBDDNA)
            //{
            //    this.gvResult.Columns.Clear();
            //    this.gcResult.DataSource = dtYGBDDNA;
            //    dtLabExport = dtYGBDDNA;
            //    this.gvResult.BestFitColumns();
            //}
        }

        /// <summary>
        /// 丙肝病毒RNA检测
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void navBarItem5_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            //SetExamLabItem(e.Link.Item.Caption);
            //SetBarItemAppearance(e);
            //if (this.gcResult.DataSource != dtBGBDRNA)
            //{
            //    this.gvResult.Columns.Clear();
            //    this.gcResult.DataSource = dtBGBDRNA;
            //    dtLabExport = dtBGBDRNA;
            //    this.gvResult.BestFitColumns();
            //}
        }

        /// <summary>
        /// 血细胞分析
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void navBarItem6_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            //SetExamLabItem(e.Link.Item.Caption);
            //SetBarItemAppearance(e);
            //if (this.gcResult.DataSource != dtXXBFX)
            //{
            //    this.gvResult.Columns.Clear();
            //    this.gcResult.DataSource = dtXXBFX;
            //    dtLabExport = dtXXBFX;
            //    this.gvResult.BestFitColumns();
            //}
        }

        /// <summary>
        /// 尿常规+沉渣检测
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void navBarItem7_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            //SetExamLabItem(e.Link.Item.Caption);
            //SetBarItemAppearance(e);
            //if (this.gcResult.DataSource != dtNCGCZ)
            //{
            //    this.gvResult.Columns.Clear();
            //    this.gcResult.DataSource = dtNCGCZ;
            //    dtLabExport = dtNCGCZ;
            //    this.gvResult.BestFitColumns();
            //}
        }

        /// <summary>
        /// 大便常规
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void navBarItem8_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            //SetExamLabItem(e.Link.Item.Caption);
            //SetBarItemAppearance(e);
            //if (this.gcResult.DataSource != dtDBCG)
            //{
            //    this.gvResult.Columns.Clear();
            //    this.gcResult.DataSource = dtDBCG;
            //    dtLabExport = dtDBCG;
            //    this.gvResult.BestFitColumns();
            //}
        }

        /// <summary>
        /// 大便潜血
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void navBarItem9_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            //SetExamLabItem(e.Link.Item.Caption);
            //SetBarItemAppearance(e);
            //if (this.gcResult.DataSource != dtDBQX)
            //{
            //    this.gvResult.Columns.Clear();
            //    this.gcResult.DataSource = dtDBQX;
            //    dtLabExport = dtDBQX;
            //    this.gvResult.BestFitColumns();
            //}
        }

        /// <summary>
        /// 电解质检查
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void navBarItem10_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            //SetExamLabItem(e.Link.Item.Caption);
            //SetBarItemAppearance(e);
            //if (this.gcResult.DataSource != dtDJZQX)
            //{
            //    this.gvResult.Columns.Clear();
            //    this.gcResult.DataSource = dtDJZQX;
            //    dtLabExport = dtDJZQX;
            //    this.gvResult.BestFitColumns();
            //}
        }

        /// <summary>
        /// 肝功11项
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void navBarItem11_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            //SetExamLabItem(e.Link.Item.Caption);
            //SetBarItemAppearance(e);
            //if (this.gcResult.DataSource != dtGGSYX)
            //{
            //    this.gvResult.Columns.Clear();
            //    this.gcResult.DataSource = dtGGSYX;
            //    dtLabExport = dtGGSYX;
            //    this.gvResult.BestFitColumns();
            //}
        }

        /// <summary>
        /// 肾功能检查
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void navBarItem12_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            //SetExamLabItem(e.Link.Item.Caption);
            //SetBarItemAppearance(e);
            //if (this.gcResult.DataSource != dtSGSX)
            //{
            //    this.gvResult.Columns.Clear();
            //    this.gcResult.DataSource = dtSGSX;
            //    dtLabExport = dtSGSX;
            //    this.gvResult.BestFitColumns();
            //}
        }

        /// <summary>
        /// 血脂4项
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void navBarItem13_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            //SetExamLabItem(e.Link.Item.Caption);
            //SetBarItemAppearance(e);
            //if (this.gcResult.DataSource != dtXZSX)
            //{
            //    this.gvResult.Columns.Clear();
            //    this.gcResult.DataSource = dtXZSX;
            //    dtLabExport = dtXZSX;
            //    this.gvResult.BestFitColumns();
            //}
        }

        /// <summary>
        /// 常规生化全套检查
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void navBarItem14_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            //SetExamLabItem(e.Link.Item.Caption);
            //SetBarItemAppearance(e);
            //if (this.gcResult.DataSource != dtSHSSSX)
            //{
            //    this.gvResult.Columns.Clear();
            //    this.gcResult.DataSource = dtSHSSSX;
            //    dtLabExport = dtSHSSSX;
            //    this.gvResult.BestFitColumns();
            //}
        }

        /// <summary>
        /// 急诊生化
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void navBarItem15_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            //SetExamLabItem(e.Link.Item.Caption);
            //SetBarItemAppearance(e);
            //if (this.gcResult.DataSource != dtJZSH)
            //{
            //    this.gvResult.Columns.Clear();
            //    this.gcResult.DataSource = dtJZSH;
            //    dtLabExport = dtJZSH;
            //    this.gvResult.BestFitColumns();
            //}
        }

        /// <summary>
        /// 甲状旁腺激素测定
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void navBarItem27_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            //SetExamLabItem(e.Link.Item.Caption);
            //SetBarItemAppearance(e);
            //if (this.gcResult.DataSource != dtJZPXJS)
            //{
            //    this.gvResult.Columns.Clear();
            //    this.gcResult.DataSource = dtJZPXJS;
            //    dtLabExport = dtJZPXJS;
            //    this.gvResult.BestFitColumns();
            //}
        }

        /// <summary>
        /// 铁蛋白
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void navBarItem16_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            //SetExamLabItem(e.Link.Item.Caption);
            //SetBarItemAppearance(e);
            //if (this.gcResult.DataSource != dtTDB)
            //{
            //    this.gvResult.Columns.Clear();
            //    this.gcResult.DataSource = dtTDB;
            //    dtLabExport = dtTDB;
            //    this.gvResult.BestFitColumns();
            //}
        }

        /// <summary>
        /// 铁，总铁结合力
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void navBarItem17_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            //SetExamLabItem(e.Link.Item.Caption);
            //SetBarItemAppearance(e);
            //if (this.gcResult.DataSource != dtTZTJHL)
            //{
            //    this.gvResult.Columns.Clear();
            //    this.gcResult.DataSource = dtTZTJHL;
            //    dtLabExport = dtTZTJHL;
            //    this.gvResult.BestFitColumns();
            //}
        }

        /// <summary>
        /// 凝血四项
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void navBarItem18_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            //SetExamLabItem(e.Link.Item.Caption);
            //SetBarItemAppearance(e);
            //if (this.gcResult.DataSource != dtNXSX)
            //{
            //    this.gvResult.Columns.Clear();
            //    this.gcResult.DataSource = dtNXSX;
            //    dtLabExport = dtNXSX;
            //    this.gvResult.BestFitColumns();
            //}
        }

        /// <summary>
        /// D-二聚体
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void navBarItem19_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            //SetExamLabItem(e.Link.Item.Caption);
            //SetBarItemAppearance(e);
            //if (this.gcResult.DataSource != dtDEJT)
            //{
            //    this.gvResult.Columns.Clear();
            //    this.gcResult.DataSource = dtDEJT;
            //    dtLabExport = dtDEJT;
            //    this.gvResult.BestFitColumns();
            //}
        }

        /// <summary>
        /// 脑利钠肽
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void navBarItem28_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            //SetExamLabItem(e.Link.Item.Caption);
            //SetBarItemAppearance(e);
            //if (this.gcResult.DataSource != dtNLNT)
            //{
            //    this.gvResult.Columns.Clear();
            //    this.gcResult.DataSource = dtNLNT;
            //    dtLabExport = dtNLNT;
            //    this.gvResult.BestFitColumns();
            //}
        }

        /// <summary>
        /// 超敏C反应蛋白
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void navBarItem29_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            //SetExamLabItem(e.Link.Item.Caption);
            //SetBarItemAppearance(e);
            //if (this.gcResult.DataSource != dtCMCFYDB)
            //{
            //    this.gvResult.Columns.Clear();
            //    this.gcResult.DataSource = dtCMCFYDB;
            //    dtLabExport = dtCMCFYDB;
            //    this.gvResult.BestFitColumns();
            //}
        }

        /// <summary>
        /// 心梗三项
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void navBarItem20_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            //SetExamLabItem(e.Link.Item.Caption);
            //SetBarItemAppearance(e);
            //if (this.gcResult.DataSource != dtXGSX)
            //{
            //    this.gvResult.Columns.Clear();
            //    this.gcResult.DataSource = dtXGSX;
            //    dtLabExport = dtXGSX;
            //    this.gvResult.BestFitColumns();
            //}
        }

        /// <summary>
        /// 肌钙蛋白
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void navBarItem21_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            //SetExamLabItem(e.Link.Item.Caption);
            //SetBarItemAppearance(e);
            //if (this.gcResult.DataSource != dtJGDB)
            //{
            //    this.gvResult.Columns.Clear();
            //    this.gcResult.DataSource = dtJGDB;
            //    dtLabExport = dtJGDB;
            //    this.gvResult.BestFitColumns();
            //}
        }

        #endregion

        /// <summary>
        /// 点击项目
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void item_LinkClicked(object sender, NavBarLinkEventArgs e)
        {
            SetExamLabItem(e.Link.Item.Caption);
            SetBarItemAppearance(e);
            var item = e.Link.Item;
            if (dsResult != null && dsResult.Tables.Count > 0)
            {
                if (this.gcResult.DataSource != null)
                {
                    if (!(this.gcResult.DataSource as DataTable).TableName.Equals(item.Caption))
                    {
                        this.gvResult.Columns.Clear();
                        this.gcResult.DataSource = dsResult.Tables[item.Caption];
                        dtLabExport = dsResult.Tables[item.Caption];
                        this.gvResult.BestFitColumns();
                    }
                }
                else
                {
                    this.gvResult.Columns.Clear();
                    this.gcResult.DataSource = dsResult.Tables[item.Caption];
                    dtLabExport = dsResult.Tables[item.Caption];
                    this.gvResult.BestFitColumns();
                }
            }
            else
            {
                this.gvResult.Columns.Clear();
                this.gcResult.DataSource = null;
                dtLabExport = null;
                this.gvResult.BestFitColumns();
            }

            #region 绑定列表-原来代码

            //if (this.gcResult.DataSource != dtJGDB)
            //{
            //    this.gvResult.Columns.Clear();
            //    this.gcResult.DataSource = dtJGDB;
            //    dtLabExport = dtJGDB;
            //    this.gvResult.BestFitColumns();
            //}

            #endregion
        }

        /// <summary>
        /// 导出
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnExport_Click(object sender, EventArgs e)
        {
            string fileName = patientName + itemName + DateTime.Now.ToString("yyyyMMdd") + "." + "xls";
            SaveFileDialog dialog = new SaveFileDialog() { Title = "导出Excel", FileName = fileName, Filter = "Excel文件(*.xls)|*.*", RestoreDirectory = true };

            if (dialog.ShowDialog(this) == DialogResult.OK)
            {
                XlsExportOptions option = new XlsExportOptions() { TextExportMode = TextExportMode.Text };
                this.gcResult.ExportToXls(dialog.FileName);
                AutoClosedMsgBox.ShowForm("保存成功。", "提示", 1500, MessageBoxIcon.Information);
            }
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnQuery_Click(object sender, EventArgs e)
        {
            DataTable dtLab = this.gcResult.DataSource as DataTable;
            if (dtLab != null && dtLab.Rows.Count > 0)
            {
                if (beginTime.EditValue != null && endTime.EditValue != null)
                {
                    dtLab = Utilities.Utility.GetSubTable(dtLab, "检验日期 >='" + Utilities.Utility.CDate(beginTime.EditValue.ToString()) + "' AND 检验日期 <='" + Utilities.Utility.CDate(endTime.EditValue.ToString()) + "'");
                }
                if (dtLab != null && dtLab.Rows.Count > 0)
                {
                    this.gcResult.DataSource = dtLab;
                }
            }
        }

        #endregion

        #region 方法

        /// <summary>
        /// 加载菜单项目
        /// </summary>
        private void LoadMenuItems()
        {
            this.navBarControl1.Groups.Clear();
            this.navBarControl1.Items.Clear();
            ConfigModel.MED_COMMON_ITEMLISTDataTable dtMenu = configService.GetConfigList(string.Empty, string.Empty, "检验项目配置", "1");
            ConfigModel.MED_COMMON_ITEMLISTDataTable dtMenuItem=dtMenu.Clone() as ConfigModel.MED_COMMON_ITEMLISTDataTable;
            dtMenu.OrderBy(row => row.ITEM_NAME).ThenBy(row => row.ITEM_VALUE).CopyToDataTable(dtMenuItem, LoadOption.OverwriteChanges);
            if (dtMenuItem != null && dtMenuItem.Rows.Count > 0)
            {
                dtMenuItem.ToList().ForEach(row => 
                {
                    var item = new NavBarItem(row.ITEM_VALUE);
                    item.LinkClicked += new NavBarLinkEventHandler(item_LinkClicked);
                    this.navBarControl1.Items.Add(item);
                    var group = this.navBarControl1.Groups.FirstOrDefault(g => g.Caption.Equals(row.ITEM_NAME));
                    if (group == null)
                    {
                        group = new NavBarGroup(row.ITEM_NAME);
                        group.Expanded = true;
                        this.navBarControl1.Groups.Add(group);
                    }
                    group.ItemLinks.Add(item);
                });
            }
        }

        /// <summary>
        /// 加载列表
        /// </summary>
        public void LoadResultList()
        {
            this.busyIndicator.ShowLoadingScreenFor(this.gcResult);
            using (BackgroundWorker work = new BackgroundWorker())
            {
                work.DoWork += new DoWorkEventHandler(work_DoWork);
                work.RunWorkerCompleted += new RunWorkerCompletedEventHandler(work_RunWorkerCompleted);
                work.RunWorkerAsync();
            }
        }

        /// <summary>
        /// 加载检验结果
        /// </summary>
        private void LoadLabResult()
        {
            try
            {
                dtItem = configService.GetConfigList(string.Empty, string.Empty, "质控指标检验项目", "1");
                DataTable dtLab = labService.GetPatientLabList(hemoId);
                if (dtLab != null && dtLab.Rows.Count > 0)
                {
                    #region 加载检验结果-原来代码

                    #region 传染病指标

                    //LoadLabResultByName(dtLab, this.navBarItem1.Caption);
                    //LoadLabResultByName(dtLab, this.navBarItem2.Caption);
                    //LoadLabResultByName(dtLab, this.navBarItem3.Caption);
                    //LoadLabResultByName(dtLab, this.navBarItem4.Caption);
                    //LoadLabResultByName(dtLab, this.navBarItem5.Caption);

                    #endregion

                    #region 三大常规

                    //LoadLabResultByName(dtLab, this.navBarItem6.Caption);
                    //LoadLabResultByName(dtLab, this.navBarItem7.Caption);
                    //LoadLabResultByName(dtLab, this.navBarItem8.Caption);
                    //LoadLabResultByName(dtLab, this.navBarItem9.Caption);

                    #endregion

                    #region 生化

                    //LoadLabResultByName(dtLab, this.navBarItem10.Caption);
                    //LoadLabResultByName(dtLab, this.navBarItem11.Caption);
                    //LoadLabResultByName(dtLab, this.navBarItem12.Caption);
                    //LoadLabResultByName(dtLab, this.navBarItem13.Caption);
                    //LoadLabResultByName(dtLab, this.navBarItem14.Caption);
                    //LoadLabResultByName(dtLab, this.navBarItem15.Caption);

                    #endregion

                    #region 甲状旁腺激素

                    //LoadLabResultByName(dtLab, this.navBarItem27.Caption);

                    #endregion

                    #region 铁代谢

                    //LoadLabResultByName(dtLab, this.navBarItem16.Caption);
                    //LoadLabResultByName(dtLab, this.navBarItem17.Caption);

                    #endregion

                    #region 凝血四项

                    //LoadLabResultByName(dtLab, this.navBarItem18.Caption);
                    //LoadLabResultByName(dtLab, this.navBarItem19.Caption);

                    #endregion

                    #region 脑利钠肽

                    //LoadLabResultByName(dtLab, this.navBarItem28.Caption);

                    #endregion

                    #region 超敏C反应蛋白

                    //LoadLabResultByName(dtLab, this.navBarItem29.Caption);

                    #endregion

                    #region 心梗三项，肌钙蛋白

                    //LoadLabResultByName(dtLab, this.navBarItem20.Caption);
                    //LoadLabResultByName(dtLab, this.navBarItem21.Caption);

                    #endregion

                    #endregion

                    dsResult = new DataSet();

                    this.navBarControl1.Items.ToList().ForEach(item =>
                    {
                        LoadLabResultByName(dtLab, item.Caption);
                    });
                }
                else
                {
                    dsResult = null;
                }
            }
            catch (Exception e)
            {
                AutoClosedMsgBox.ShowForm(e.Message, "检验检查", 2000, MessageBoxIcon.Information);
            }
        }

        /// <summary>
        /// 根据检验项目名称加载检验结果
        /// </summary>
        /// <param name="dtLab"></param>
        /// <param name="name"></param>
        private void LoadLabResultByName(DataTable dtLab, string name)
        {
            DataTable dtSubLab = null;
            StringBuilder strFind = new StringBuilder();
            string strSelect = strFind.Append("ITEM_NAME LIKE '%" + name + "%'").ToString();
            var rowItem = dtItem.FirstOrDefault(row => row.ITEM_NAME.Equals(name));
            if (rowItem != null)
            {
                strFind.Clear();
                string[] items = rowItem.ITEM_VALUE.Split("，".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                if (name.Equals("铁，总铁结合力"))
                {
                    items.AsEnumerable().ToList().ForEach(item =>
                    {
                        strFind.Append("ITEM_NAME='" + item + "'");
                        strFind.Append(" " + "OR" + " ");
                    });
                }
                else
                {
                    items.AsEnumerable().ToList().ForEach(item =>
                    {
                        strFind.Append("ITEM_NAME LIKE '%" + item + "%'");
                        strFind.Append(" " + "OR" + " ");
                    });
                }
                strSelect = strFind.ToString(0, strFind.Length - 4);
            }

            dtSubLab = Utility.GetSubTable(dtLab, strSelect);

            DataTable dtResult = Utility.DataTableRowtoColumn(dtSubLab, "REPORT_ITEM_NAME");
            dtResult = dtResult ?? new DataTable();
            DataTable dtOrderResult = dtResult.Clone();
            if (dtResult.Rows.Count > 0)
            {
                dtResult.AsEnumerable().OrderByDescending(row => row["检验日期"]).CopyToDataTable(dtOrderResult, LoadOption.OverwriteChanges);
            }
            dtOrderResult.TableName = name;
            dsResult.Tables.Add(dtOrderResult);

            #region DataTable行转列-原来代码

            //switch (name)
            //{
            //    case "术前四项":
            //        dtSQSX = Utility.DataTableRowtoColumn(dtSubLab, "REPORT_ITEM_NAME");
            //        break;
            //    case "乙肝两对半":
            //        dtYGLDB = Utility.DataTableRowtoColumn(dtSubLab, "REPORT_ITEM_NAME");
            //        break;
            //    case "梅毒血清试验（TRUST法）+滴度":
            //        dtMDXQSY = Utility.DataTableRowtoColumn(dtSubLab, "REPORT_ITEM_NAME");
            //        break;
            //    case "乙肝病毒DNA检测":
            //        dtYGBDDNA = Utility.DataTableRowtoColumn(dtSubLab, "REPORT_ITEM_NAME");
            //        break;
            //    case "丙肝病毒RNA检测":
            //        dtBGBDRNA = Utility.DataTableRowtoColumn(dtSubLab, "REPORT_ITEM_NAME");
            //        break;
            //    case "血细胞分析":
            //        dtXXBFX = Utility.DataTableRowtoColumn(dtSubLab, "REPORT_ITEM_NAME");
            //        break;
            //    case "尿常规+沉渣检测":
            //        dtNCGCZ = Utility.DataTableRowtoColumn(dtSubLab, "REPORT_ITEM_NAME");
            //        break;
            //    case "大便常规":
            //        dtDBCG = Utility.DataTableRowtoColumn(dtSubLab, "REPORT_ITEM_NAME");
            //        break;
            //    case "大便潜血":
            //        dtDBQX = Utility.DataTableRowtoColumn(dtSubLab, "REPORT_ITEM_NAME");
            //        break;
            //    case "电解质检查":
            //        dtDJZQX = Utility.DataTableRowtoColumn(dtSubLab, "REPORT_ITEM_NAME");
            //        break;
            //    case "肝功能检查":
            //        dtGGSYX = Utility.DataTableRowtoColumn(dtSubLab, "REPORT_ITEM_NAME");
            //        break;
            //    case "肾功能检查":
            //        dtSGSX = Utility.DataTableRowtoColumn(dtSubLab, "REPORT_ITEM_NAME");
            //        break;
            //    case "血脂检查":
            //        dtXZSX = Utility.DataTableRowtoColumn(dtSubLab, "REPORT_ITEM_NAME");
            //        break;
            //    case "常规生化全套检查":
            //        dtSHSSSX = Utility.DataTableRowtoColumn(dtSubLab, "REPORT_ITEM_NAME");
            //        break;
            //    case "急诊生化":
            //        dtJZSH = Utility.DataTableRowtoColumn(dtSubLab, "REPORT_ITEM_NAME");
            //        break;
            //    case "甲状旁腺激素测定":
            //        dtJZPXJS = Utility.DataTableRowtoColumn(dtSubLab, "REPORT_ITEM_NAME");
            //        break;
            //    case "铁蛋白":
            //        dtTDB = Utility.DataTableRowtoColumn(dtSubLab, "REPORT_ITEM_NAME");
            //        break;
            //    case "铁，总铁结合力":
            //        dtTZTJHL = Utility.DataTableRowtoColumn(dtSubLab, "REPORT_ITEM_NAME");
            //        break;
            //    case "凝血四项":
            //        dtNXSX = Utility.DataTableRowtoColumn(dtSubLab, "REPORT_ITEM_NAME");
            //        break;
            //    case "D-二聚体":
            //        dtDEJT = Utility.DataTableRowtoColumn(dtSubLab, "REPORT_ITEM_NAME");
            //        break;
            //    case "脑利钠肽":
            //        dtNLNT = Utility.DataTableRowtoColumn(dtSubLab, "REPORT_ITEM_NAME");
            //        break;
            //    case "超敏C反应蛋白":
            //        dtCMCFYDB = Utility.DataTableRowtoColumn(dtSubLab, "REPORT_ITEM_NAME");
            //        break;
            //    case "心梗三项":
            //        dtXGSX = Utility.DataTableRowtoColumn(dtSubLab, "REPORT_ITEM_NAME");
            //        break;
            //    case "肌钙蛋白":
            //        dtJGDB = Utility.DataTableRowtoColumn(dtSubLab, "REPORT_ITEM_NAME");
            //        break;
            //}

            #endregion
        }

        /// <summary>
        /// 刷新检验结果列表
        /// </summary>
        private void RefreshResultList()
        {
            string itemName = string.Empty;
            if (currentParent != null)
            {
                if (currentParent.GetType().Name.Equals("PatientFixInfosUI"))
                {
                    itemName = (currentParent as PatientFixInfosUI).CurrentExamLabItem;
                }
                else if (currentParent.GetType().Name.Equals("CtlUserCureList"))
                {
                    itemName = (currentParent as CtlUserCureList).CurrentExamLabItem;
                }
            }
            else
            {
                //itemName = "术前四项";
                itemName = this.navBarControl1.Items[0].Caption;
            }

            if (!string.IsNullOrEmpty(itemName))
            {
                var item = this.navBarControl1.Items.FirstOrDefault(i => i.Caption.Equals(itemName));
                if (item != null)
                {
                    item_LinkClicked(item, new DevExpress.XtraNavBar.NavBarLinkEventArgs(item.Links[0]));
                }

                #region 触发点击项目-原来代码

                //switch (itemName)
                //{
                //    case "术前四项":
                //        navBarItem1_LinkClicked(this.navBarItem1, new DevExpress.XtraNavBar.NavBarLinkEventArgs(this.navBarItem1.Links[0]));
                //        break;
                //    case "乙肝两对半":
                //        navBarItem2_LinkClicked(this.navBarItem2, new DevExpress.XtraNavBar.NavBarLinkEventArgs(this.navBarItem2.Links[0]));
                //        break;
                //    case "梅毒血清试验（TRUST法）+滴度":
                //        navBarItem3_LinkClicked(this.navBarItem3, new DevExpress.XtraNavBar.NavBarLinkEventArgs(this.navBarItem3.Links[0]));
                //        break;
                //    case "乙肝病毒DNA检测":
                //        navBarItem4_LinkClicked(this.navBarItem4, new DevExpress.XtraNavBar.NavBarLinkEventArgs(this.navBarItem4.Links[0]));
                //        break;
                //    case "丙肝病毒RNA检测":
                //        navBarItem5_LinkClicked(this.navBarItem5, new DevExpress.XtraNavBar.NavBarLinkEventArgs(this.navBarItem5.Links[0]));
                //        break;
                //    case "血细胞分析":
                //        navBarItem6_LinkClicked(this.navBarItem6, new DevExpress.XtraNavBar.NavBarLinkEventArgs(this.navBarItem6.Links[0]));
                //        break;
                //    case "尿常规+沉渣检测":
                //        navBarItem7_LinkClicked(this.navBarItem7, new DevExpress.XtraNavBar.NavBarLinkEventArgs(this.navBarItem7.Links[0]));
                //        break;
                //    case "大便常规":
                //        navBarItem8_LinkClicked(this.navBarItem8, new DevExpress.XtraNavBar.NavBarLinkEventArgs(this.navBarItem8.Links[0]));
                //        break;
                //    case "大便潜血":
                //        navBarItem9_LinkClicked(this.navBarItem9, new DevExpress.XtraNavBar.NavBarLinkEventArgs(this.navBarItem9.Links[0]));
                //        break;
                //    case "电解质检查":
                //        navBarItem10_LinkClicked(this.navBarItem10, new DevExpress.XtraNavBar.NavBarLinkEventArgs(this.navBarItem10.Links[0]));
                //        break;
                //    case "肝功能检查":
                //        navBarItem11_LinkClicked(this.navBarItem11, new DevExpress.XtraNavBar.NavBarLinkEventArgs(this.navBarItem11.Links[0]));
                //        break;
                //    case "肾功能检查":
                //        navBarItem12_LinkClicked(this.navBarItem12, new DevExpress.XtraNavBar.NavBarLinkEventArgs(this.navBarItem12.Links[0]));
                //        break;
                //    case "血脂检查":
                //        navBarItem13_LinkClicked(this.navBarItem13, new DevExpress.XtraNavBar.NavBarLinkEventArgs(this.navBarItem13.Links[0]));
                //        break;
                //    case "常规生化全套检查":
                //        navBarItem14_LinkClicked(this.navBarItem14, new DevExpress.XtraNavBar.NavBarLinkEventArgs(this.navBarItem14.Links[0]));
                //        break;
                //    case "急诊生化":
                //        navBarItem15_LinkClicked(this.navBarItem15, new DevExpress.XtraNavBar.NavBarLinkEventArgs(this.navBarItem15.Links[0]));
                //        break;
                //    case "甲状旁腺激素测定":
                //        navBarItem27_LinkClicked(this.navBarItem27, new DevExpress.XtraNavBar.NavBarLinkEventArgs(this.navBarItem27.Links[0]));
                //        break;
                //    case "铁蛋白":
                //        navBarItem16_LinkClicked(this.navBarItem16, new DevExpress.XtraNavBar.NavBarLinkEventArgs(this.navBarItem16.Links[0]));
                //        break;
                //    case "铁，总铁结合力":
                //        navBarItem17_LinkClicked(this.navBarItem17, new DevExpress.XtraNavBar.NavBarLinkEventArgs(this.navBarItem17.Links[0]));
                //        break;
                //    case "凝血四项":
                //        navBarItem18_LinkClicked(this.navBarItem18, new DevExpress.XtraNavBar.NavBarLinkEventArgs(this.navBarItem18.Links[0]));
                //        break;
                //    case "D-二聚体":
                //        navBarItem19_LinkClicked(this.navBarItem19, new DevExpress.XtraNavBar.NavBarLinkEventArgs(this.navBarItem19.Links[0]));
                //        break;
                //    case "脑利钠肽":
                //        navBarItem28_LinkClicked(this.navBarItem28, new DevExpress.XtraNavBar.NavBarLinkEventArgs(this.navBarItem28.Links[0]));
                //        break;
                //    case "超敏C反应蛋白":
                //        navBarItem29_LinkClicked(this.navBarItem29, new DevExpress.XtraNavBar.NavBarLinkEventArgs(this.navBarItem29.Links[0]));
                //        break;
                //    case "心梗三项":
                //        navBarItem20_LinkClicked(this.navBarItem20, new DevExpress.XtraNavBar.NavBarLinkEventArgs(this.navBarItem20.Links[0]));
                //        break;
                //    case "肌钙蛋白":
                //        navBarItem21_LinkClicked(this.navBarItem21, new DevExpress.XtraNavBar.NavBarLinkEventArgs(this.navBarItem21.Links[0]));
                //        break;
                //}

                #endregion
            }
        }

        /// <summary>
        /// 设置选中菜单项外观
        /// </summary>
        /// <param name="e"></param>
        private void SetBarItemAppearance(DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            foreach (DevExpress.XtraNavBar.NavBarItem item in this.navBarControl1.Items)
            {
                item.Appearance.ForeColor = foreColor;
            }
            e.Link.Item.Appearance.ForeColor = Color.Blue;
        }

        /// <summary>
        /// 设置当前选中项
        /// </summary>
        /// <param name="name"></param>
        private void SetExamLabItem(string name)
        {
            if (currentParent != null)
            {
                itemName = name;
                if (currentParent.GetType().Name.Equals("PatientFixInfosUI"))
                {
                    (currentParent as PatientFixInfosUI).CurrentExamLabItem = name;
                }
                else if (currentParent.GetType().Name.Equals("CtlUserCureList"))
                {
                    (currentParent as CtlUserCureList).CurrentExamLabItem = name;
                }
            }
        }

        /// <summary>
        /// 图表数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnChart_Click(object sender, EventArgs e)
        {
            if (gvResult.DataSource != null)
            {
                string fileName = this.gvResult.FocusedColumn.FieldName;

                DataTable dtLab = dtLabExport.Copy();

                if (beginTime.EditValue != null && endTime.EditValue != null)
                {
                    dtLab = Utilities.Utility.GetSubTable(dtLab, "检验日期 >='" + Utilities.Utility.CDate(beginTime.EditValue.ToString()) + "' AND 检验日期 <='" + Utilities.Utility.CDate(endTime.EditValue.ToString()) + "'");
                }

                if (dtLab != null && dtLab.Rows.Count > 0)
                {
                    string columnName = string.Empty;

                    string strResult = string.Empty;

                    for (int z = dtLab.Columns.Count - 1; z >= 1; z--)
                    {
                        columnName = dtLab.Columns[z].ColumnName;
                        if (fileName.Contains(columnName) == false)
                        {
                            dtLab.Columns.Remove(columnName);
                        }
                    }

                    if (dtLab.Columns.Count > 1)
                    {
                        ShowCustomerLabChart frm = new ShowCustomerLabChart();
                        frm.ChartDataTable = dtLab;
                        frm.ChartTitle = fileName;
                        dtLab = Utility.GetSubTable(dtLab, "", "检验日期 DESC");
                        frm.ChartDataTable = dtLab;
                        frm.SeriesTitle = fileName;
                        frm.ChartTitle = fileName;

                        frm.Column1 = dtLab.Columns[0].ToString();
                        frm.Column2 = dtLab.Columns[1].ToString();
                        frm.ShowDialog();
                    }
                }
            }
        }
        #endregion
    }
}
