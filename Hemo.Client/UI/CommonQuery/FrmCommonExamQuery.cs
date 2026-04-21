/*----------------------------------------------------------------
// Copyright (C) 2005 (北京)医疗科技发展有限公司
// 文件名：FrmCommonExamQuery.cs
// 文件功能描述：常规检验查询窗体类
// 创建标识：吕志强-2014-4-9
// 修改时间：
// 修改人：
// 修改描述：
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
using Hemo.IService.Lab;
using Hemo.Service;
using DevExpress.XtraPrinting;
using Hemo.Utilities;

namespace Hemo.Client.UI.CommonQuery
{
    /// <summary>
    /// 常规检验查询窗体
    /// </summary>
    public partial class FrmCommonExamQuery :HemoBaseFrm
    {
        #region 成员变量

        private ILab labService = ServiceManager.Instance.LabService;

        private BackgroundWorker worker = null;

        #endregion

        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public FrmCommonExamQuery()
        {
            InitializeComponent();
        }

        #endregion

        #region 事件

        /// <summary>
        /// 窗体加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FrmCommonExamQuery_Load(object sender, EventArgs e)
        {
            InitWorker();
            LoadData();
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnQuery_Click(object sender, EventArgs e)
        {
            LoadData();
        }

        /// <summary>
        /// 导出Excel
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnOutExcel_Click(object sender, EventArgs e)
        {
            string fileName = "常规检查" + DateTime.Now.ToString("yyyyMMdd") + "." + "xls";
            SaveFileDialog dialog = new SaveFileDialog() { Title = "导出Excel", FileName = fileName, Filter = "Excel文件(*.xls)|*.*", RestoreDirectory = true };

            if (dialog.ShowDialog(this) == DialogResult.OK)
            {
                XlsExportOptions option = new XlsExportOptions() { TextExportMode = TextExportMode.Text };
                this.gcResult.ExportToXls(dialog.FileName, option);
                AutoClosedMsgBox.ShowForm("保存成功。", "提示", 1500, MessageBoxIcon.Information);

               // XtraMessageBox.Show("保存成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        /// <summary>
        /// 列表列重绘
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gvResult_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            if (this.gvResult.GetRow(e.RowHandle) == null)
            {
                return;
            }

            if (this.radioGroup1.SelectedIndex == 0)
            {
                if (e.Column.FieldName == "XUECHANGGUI" || e.Column.FieldName == "SHENGHUA" || e.Column.FieldName == "DIANJIEZHI" || e.Column.FieldName == "XUEZHI" || e.Column.FieldName == "FANYINGDANBAI" || e.Column.FieldName == "JIAZHUANGSU" || e.Column.FieldName == "TIE")
                {
                    e.Appearance.BackColor = e.CellValue.ToString() == "否" ? Color.Red : Color.White;
                }
            }
            else
            {
                if (e.Column.FieldName == "YIGAN" || e.Column.FieldName == "BINGGAN" || e.Column.FieldName == "MEIDU" || e.Column.FieldName == "AIZIBING")
                {
                    e.Appearance.BackColor = e.CellValue.ToString() == "否" ? Color.Red : Color.White;
                }
            }
        }

        #endregion

        #region 方法

        /// <summary>
        /// 初始化后台线程
        /// </summary>
        private void InitWorker()
        {
            DataTable dtList = null;
           
            using (worker = new BackgroundWorker())
            {
                //后台线程操作执行
                worker.DoWork += (o, e) =>
                {
                    try
                    {
                        string strPatientType = string.Empty;
                        if (cmbTimeType.EditValue != null) {
                            strPatientType = cmbTimeType.EditValue.ToString();
                        }
                        dtList = this.radioGroup1.SelectedIndex == 0 ? labService.GetThreeMonthsCommonLabList(strPatientType) : labService.GetSixMonthsCommonLabList(strPatientType);
                    }
                    catch (Exception ex)
                    {
                        XtraMessageBox.Show("查询失败：" + ex.Message, "常规检验查询");
                    }
                };

                //后台线程操作完成
                worker.RunWorkerCompleted += (o, e) =>
                {
                    if (dtList != null && dtList.Rows.Count > 0)
                    {
                        this.gcResult.DataSource = dtList;
                    }
                    else
                    {
                        this.gcResult.DataSource = null;
                    }

                    this.busyIndicator1.HideLoadingScreen();
                    SetShowItem();
                };
            }
        }

        /// <summary>
        /// 加载常规检验列表数据
        /// </summary>
        private void LoadData()
        {
            this.busyIndicator1.ShowLoadingScreenFor(this.gcResult);
            //启动后台线程
            worker.RunWorkerAsync();
        }

        /// <summary>
        /// 设置显示的检查项
        /// </summary>
        private void SetShowItem()
        {
            if (this.radioGroup1.SelectedIndex == 0)
            {
                this.gvResult.Columns["XUECHANGGUI"].Visible = true;
                this.gvResult.Columns["XUECHANGGUI"].VisibleIndex = 9;

                this.gvResult.Columns["SHENGHUA"].Visible = true;
                this.gvResult.Columns["SHENGHUA"].VisibleIndex = 10;

                this.gvResult.Columns["DIANJIEZHI"].Visible = true;
                this.gvResult.Columns["DIANJIEZHI"].VisibleIndex = 11;

                this.gvResult.Columns["XUEZHI"].Visible = true;
                this.gvResult.Columns["XUEZHI"].VisibleIndex = 12;

                this.gvResult.Columns["FANYINGDANBAI"].Visible = true;
                this.gvResult.Columns["FANYINGDANBAI"].VisibleIndex = 13;

                this.gvResult.Columns["JIAZHUANGSU"].Visible = true;
                this.gvResult.Columns["JIAZHUANGSU"].VisibleIndex = 14;

                this.gvResult.Columns["TIE"].Visible = true;
                this.gvResult.Columns["TIE"].VisibleIndex = 15;

                this.gvResult.Columns["YIGAN"].Visible = false;
                this.gvResult.Columns["BINGGAN"].Visible = false;
                this.gvResult.Columns["MEIDU"].Visible = false;
                this.gvResult.Columns["AIZIBING"].Visible = false;
            }
            else
            {
                this.gvResult.Columns["XUECHANGGUI"].Visible = false;
                this.gvResult.Columns["SHENGHUA"].Visible = false;
                this.gvResult.Columns["DIANJIEZHI"].Visible = false;
                this.gvResult.Columns["XUEZHI"].Visible = false;
                this.gvResult.Columns["FANYINGDANBAI"].Visible = false;
                this.gvResult.Columns["JIAZHUANGSU"].Visible = false;
                this.gvResult.Columns["TIE"].Visible = false;

                this.gvResult.Columns["YIGAN"].Visible = true;
                this.gvResult.Columns["YIGAN"].VisibleIndex = 9;

                this.gvResult.Columns["BINGGAN"].Visible = true;
                this.gvResult.Columns["BINGGAN"].VisibleIndex = 10;

                this.gvResult.Columns["MEIDU"].Visible = true;
                this.gvResult.Columns["MEIDU"].VisibleIndex = 11;

                this.gvResult.Columns["AIZIBING"].Visible = true;
                this.gvResult.Columns["AIZIBING"].VisibleIndex = 12;
            }
        }

        #endregion
    }
}