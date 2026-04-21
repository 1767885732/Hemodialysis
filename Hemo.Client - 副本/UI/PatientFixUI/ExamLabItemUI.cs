/*----------------------------------------------------------------
 * Copyright (C) 2005 (苏州)医疗科技发展有限公司
 * 文件功能描述:患者检验检查查询类
 * 创建标识:刘超-2017年4月5日
 * ----------------------------------------------------------------*/

using System;
using System.Data;
using Hemo.Model;
using Hemo.Service;
using Hemo.IService;
using System.Windows.Forms;
using Hemo.IService.Config;
using Hemo.Utilities;
using DevExpress.XtraEditors;
using Hemo.Client.Print;
using Hemo.Client.UI.Machine;
using Hemo.Client.Print;
using Hemo.Client.UI.Hemodialysis;
using Hemo.IService.Lab;
using DevExpress.XtraPrinting;


namespace Hemo.Client.UI.PatientFixUI {
    public partial class ExamLabItemUI : ViewBase
    {
        #region 类变量

        private IConfig _configService = ServiceManager.Instance.ConfigService;
        private ILab _labService = ServiceManager.Instance.LabService;
        string patient_id = string.Empty;
        string patient_name = string.Empty;

        #endregion

        #region 属性

        #endregion

        #region 构造函数

        public ExamLabItemUI()
        {
            InitializeComponent();
            //   patient_id = patientRow.PATIENT_ID;
            //   patient_name = patientRow.NAME;
            //    ctlUserInfo.HEMODIALYSIS_ID = patientRow.HEMODIALYSIS_ID;
            //    ctlUserInfo.LoadPatientInfo();
            //     loadLabDetails(patient_id);
        }

        #endregion

        #region 事件

        /// <summary>
        /// 导出
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnExport_Click(object sender, EventArgs e)
        {
            string fileName = patient_name + xtraTabControl1.SelectedTabPage.Text + DateTime.Now.ToString("yyyyMMdd") + "." + "xls";
            SaveFileDialog dialog = new SaveFileDialog() { Title = "导出Excel", FileName = fileName, Filter = "Excel文件(*.xls)|*.*", RestoreDirectory = true };

            if (dialog.ShowDialog(this) == DialogResult.OK)
            {
                XlsExportOptions option = new XlsExportOptions() { TextExportMode = TextExportMode.Text };
                this.gridControl1.ExportToXls(dialog.FileName);


                if (xtraTabControl1.SelectedTabPage.Text == "电解质检查")
                {
                    this.gridControl1.ExportToXls(dialog.FileName);
                }
                else if (xtraTabControl1.SelectedTabPage.Text == "急诊潜血试验")
                {
                    this.gridControl2.ExportToXls(dialog.FileName);
                }
                else if (xtraTabControl1.SelectedTabPage.Text == "急诊生化（干化学）")
                {
                    this.gridControl3.ExportToXls(dialog.FileName);

                }
                else if (xtraTabControl1.SelectedTabPage.Text == "梅毒血清试验（TRUST法）")
                {
                    this.gridControl4.ExportToXls(dialog.FileName);
                }
                else if (xtraTabControl1.SelectedTabPage.Text == "脑利钠肽BNP")
                {
                    this.gridControl5.ExportToXls(dialog.FileName);
                }
                else if (xtraTabControl1.SelectedTabPage.Text == "尿常规+沉渣检测")
                {
                    this.gridControl6.ExportToXls(dialog.FileName);
                }
                else if (xtraTabControl1.SelectedTabPage.Text == "脑利钠肽凝血四项（PT+APTT+TT+FIB）")
                {
                    this.gridControl7.ExportToXls(dialog.FileName);
                }
                else if (xtraTabControl1.SelectedTabPage.Text == "常规生化全套检查")
                {
                    this.gridControl8.ExportToXls(dialog.FileName);
                }
                else if (xtraTabControl1.SelectedTabPage.Text == "术前四项")
                {
                    this.gridControl9.ExportToXls(dialog.FileName);
                }
                else if (xtraTabControl1.SelectedTabPage.Text == "心梗三项")
                {
                    this.gridControl10.ExportToXls(dialog.FileName);
                }
                else if (xtraTabControl1.SelectedTabPage.Text == "血脂四项")
                {
                    this.gridControl11.ExportToXls(dialog.FileName);
                }
                else if (xtraTabControl1.SelectedTabPage.Text == "叶酸")
                {
                    this.gridControl12.ExportToXls(dialog.FileName);
                }
                else if (xtraTabControl1.SelectedTabPage.Text == "乙肝表面抗体")
                {
                    this.gridControl13.ExportToXls(dialog.FileName);
                }
                else if (xtraTabControl1.SelectedTabPage.Text == "乙肝两对半定量检测")
                {
                    this.gridControl14.ExportToXls(dialog.FileName);
                }
                else if (xtraTabControl1.SelectedTabPage.Text == "总铁结合力")
                {
                    this.gridControl15.ExportToXls(dialog.FileName);
                }
                AutoClosedMsgBox.ShowForm("保存成功。", "提示", 1500, MessageBoxIcon.Information);
            }
        }

        /// <summary>
        /// 加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ExamLabItemUI_Load(object sender, EventArgs e)
        {
            //    loadLabDetails(patient_id);
        }

        #endregion

        #region 方法

        /// <summary>
        /// 根据每个检查检验单项载入数据
        /// </summary>
        public void LoadLabDetails(string pPatientID)
        {
            try
            {
                DataTable dtLab = _labService.GetPatientLabList(pPatientID);
                string labName = string.Empty;

                if (dtLab != null && dtLab.Rows.Count > 0)
                {
                    for (int i = 0; i < xtraTabControl1.TabPages.Count; i++)
                    {
                        labName = xtraTabControl1.TabPages[i].Text;
                        loadPatientLabByName(dtLab, labName);
                    }
                }
            }
            catch (Exception e)
            {
                AutoClosedMsgBox.ShowForm(e.Message, "系统提示", 2000, MessageBoxIcon.Information);

            }
        }

        /// <summary>
        /// 根据检验名称载入对应检验项
        /// </summary>
        /// <param name="LabName"></param>
        private void loadPatientLabByName(DataTable dtLab, string LabName)
        {
            DataTable dtSubLab = Utilities.Utility.GetSubTable(dtLab, "ITEM_NAME='" + LabName + "'");
            if (dtSubLab != null && dtSubLab.Rows.Count > 0)
            {
                DataTable dtLabRowToColumn = Utilities.Utility.DataTableRowtoColumn(dtSubLab, "REPORT_ITEM_NAME");

                if (LabName == "电解质检查")
                {
                    if (dtLabRowToColumn != null && dtLabRowToColumn.Rows.Count > 0)
                    {
                        gridControl1.DataSource = dtLabRowToColumn;
                    }
                }
                else if (LabName == "急诊潜血试验")
                {
                    if (dtLabRowToColumn != null && dtLabRowToColumn.Rows.Count > 0)
                    {
                        gridControl2.DataSource = dtLabRowToColumn;
                    }
                }
                else if (LabName == "急诊生化（干化学）")
                {
                    if (dtLabRowToColumn != null && dtLabRowToColumn.Rows.Count > 0)
                    {
                        gridControl3.DataSource = dtLabRowToColumn;
                    }
                }
                else if (LabName == "梅毒血清试验（TRUST法）")
                {
                    if (dtLabRowToColumn != null && dtLabRowToColumn.Rows.Count > 0)
                    {
                        gridControl4.DataSource = dtLabRowToColumn;
                    }
                }
                else if (LabName == "脑利钠肽BNP")
                {
                    if (dtLabRowToColumn != null && dtLabRowToColumn.Rows.Count > 0)
                    {
                        gridControl5.DataSource = dtLabRowToColumn;
                    }
                }
                else if (LabName == "尿常规+沉渣检测")
                {
                    if (dtLabRowToColumn != null && dtLabRowToColumn.Rows.Count > 0)
                    {
                        gridControl6.DataSource = dtLabRowToColumn;
                    }
                }
                else if (LabName == "脑利钠肽凝血四项（PT+APTT+TT+FIB）")
                {
                    if (dtLabRowToColumn != null && dtLabRowToColumn.Rows.Count > 0)
                    {
                        gridControl7.DataSource = dtLabRowToColumn;
                    }
                }
                else if (LabName == "常规生化全套检查")
                {
                    if (dtLabRowToColumn != null && dtLabRowToColumn.Rows.Count > 0)
                    {
                        gridControl8.DataSource = dtLabRowToColumn;
                    }
                }
                else if (LabName == "术前四项")
                {
                    if (dtLabRowToColumn != null && dtLabRowToColumn.Rows.Count > 0)
                    {
                        gridControl9.DataSource = dtLabRowToColumn;
                    }
                }
                else if (LabName == "心梗三项")
                {
                    if (dtLabRowToColumn != null && dtLabRowToColumn.Rows.Count > 0)
                    {
                        gridControl10.DataSource = dtLabRowToColumn;
                    }
                }
                else if (LabName == "血脂四项")
                {
                    if (dtLabRowToColumn != null && dtLabRowToColumn.Rows.Count > 0)
                    {
                        gridControl11.DataSource = dtLabRowToColumn;
                    }
                }
                else if (LabName == "叶酸")
                {
                    if (dtLabRowToColumn != null && dtLabRowToColumn.Rows.Count > 0)
                    {
                        gridControl12.DataSource = dtLabRowToColumn;
                    }
                }
                else if (LabName == "乙肝表面抗体")
                {
                    if (dtLabRowToColumn != null && dtLabRowToColumn.Rows.Count > 0)
                    {
                        gridControl13.DataSource = dtLabRowToColumn;
                    }
                }
                else if (LabName == "乙肝两对半定量检测")
                {
                    if (dtLabRowToColumn != null && dtLabRowToColumn.Rows.Count > 0)
                    {
                        gridControl14.DataSource = dtLabRowToColumn;
                    }
                }
                else if (LabName == "总铁结合力")
                {
                    if (dtLabRowToColumn != null && dtLabRowToColumn.Rows.Count > 0)
                    {
                        gridControl15.DataSource = dtLabRowToColumn;
                    }
                }
            }
        }

        #endregion
    }
}
