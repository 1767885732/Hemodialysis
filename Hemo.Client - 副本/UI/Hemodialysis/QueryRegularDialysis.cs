/*----------------------------------------------------------------
 * Copyright (C) 2005 (苏州)医疗科技发展有限公司
 * 文件功能描述:用户控件类
 * 创建标识:刘超-2013年5月31日
 * 
 * 修改时间:2013年9月8日
 * 修改人:刘超
 * 修改描述:修改方法SQL
 * 
 * 修改时间:2013年12月17日
 * 修改人:贺建操
 * 修改描述:新增方法SQL
 * 
 * 修改时间:2014年3月27日
 * 修改人:吕志强
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
using Hemo.Utilities;

namespace Hemo.Client.UI.Hemodialysis
{
    public partial class QueryRegularDialysis : HemoBaseFrm
    {
        #region 类变量

        private IHemodialysis hemoService = ServiceManager.Instance.HemodialysisService;

        #endregion

        #region 构造函数

        public QueryRegularDialysis()
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
        private void QueryRegularDialysis_Load(object sender, EventArgs e)
        {
            BindLookUpEdit();
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnQuery_Click(object sender, EventArgs e)
        {
            Query();
        }

        /// <summary>
        /// 导出Excel
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnExpExcel_Click(object sender, EventArgs e)
        {
            if (this.gvResult.RowCount > 0)
            {
                SaveFileDialog fileDialog = new SaveFileDialog();
                fileDialog.Title = "导出Excel";
                fileDialog.Filter = "Excel文件(*.xls)|*.xls";
                fileDialog.FileName = this.lupTime.Text + "透析患者列表";
                fileDialog.RestoreDirectory = true;
                DialogResult dialogResult = fileDialog.ShowDialog(this);
                if (dialogResult == DialogResult.OK)
                {
                    DevExpress.XtraPrinting.XlsExportOptions options = new DevExpress.XtraPrinting.XlsExportOptions();
                    options.TextExportMode = DevExpress.XtraPrinting.TextExportMode.Text;
                    this.gvResult.ExportToXls(fileDialog.FileName, options);
                    DevExpress.XtraEditors.XtraMessageBox.Show("导出成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
            DataTable dtTime = new DataTable();
            dtTime.Columns.Add("Time", typeof(string));
            dtTime.Columns.Add("Value", typeof(int));

            var row = dtTime.NewRow();
            row["Time"] = "近一周";
            row["Value"] = 1;
            dtTime.Rows.Add(row);

            row = dtTime.NewRow();
            row["Time"] = "近两周";
            row["Value"] = 2;
            dtTime.Rows.Add(row);

            row = dtTime.NewRow();
            row["Time"] = "近三周";
            row["Value"] = 3;
            dtTime.Rows.Add(row);

            row = dtTime.NewRow();
            row["Time"] = "近四周";
            row["Value"] = 4;
            dtTime.Rows.Add(row);

            row = dtTime.NewRow();
            row["Time"] = "近五周";
            row["Value"] = 5;
            dtTime.Rows.Add(row);

            row = dtTime.NewRow();
            row["Time"] = "近六周";
            row["Value"] = 6;
            dtTime.Rows.Add(row);

            row = dtTime.NewRow();
            row["Time"] = "近七周";
            row["Value"] = 7;
            dtTime.Rows.Add(row);

            row = dtTime.NewRow();
            row["Time"] = "近八周";
            row["Value"] = 8;
            dtTime.Rows.Add(row);

            row = dtTime.NewRow();
            row["Time"] = "近九周";
            row["Value"] = 9;
            dtTime.Rows.Add(row);

            row = dtTime.NewRow();
            row["Time"] = "近十周";
            row["Value"] = 10;
            dtTime.Rows.Add(row);

            BaseControlInfo.BindLookUpEdit(this.lupTime, "Value", "Time", dtTime, "Time", "时间范围");

            DataTable dtFrequency = new DataTable();
            dtFrequency.Columns.Add("Frequency", typeof(string));
            dtFrequency.Columns.Add("Value", typeof(int));

            var row2 = dtFrequency.NewRow();
            row2["Frequency"] = "一周一次";
            row2["Value"] = 1;
            dtFrequency.Rows.Add(row2);

            row2 = dtFrequency.NewRow();
            row2["Frequency"] = "一周两次";
            row2["Value"] = 2;
            dtFrequency.Rows.Add(row2);

            BaseControlInfo.BindLookUpEdit(this.lupFrequency, "Value", "Frequency", dtFrequency, "Frequency", "透析次数");
        }

        /// <summary>
        /// 查询
        /// </summary>
        private void Query()
        {
            if (this.lupTime.EditValue == DBNull.Value)
            {
                XtraMessageBox.Show("请选择时间范围！", "提示");
                return;
            }
            if (this.lupFrequency.EditValue == DBNull.Value)
            {
                XtraMessageBox.Show("请选择透析次数！", "提示");
                return;
            }
            DataTable dtResult = hemoService.GetCurePatientByTimeAndFrequency(Utility.CInt(this.lupTime.EditValue.ToString()) - 1, Utility.CInt(this.lupFrequency.EditValue.ToString()));
            this.gcResult.DataSource = dtResult;
        }

        #endregion
    }
}