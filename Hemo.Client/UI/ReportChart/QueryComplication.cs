/*----------------------------------------------------------------
 * Copyright (C) 2005 (苏州)医疗科技发展有限公司
 * 文件功能描述:并发症统计类
 * 创建标识:吕志强-2017年4月17日
 * ----------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Hemo.IService.Config;
using Hemo.Service;
using Hemo.Client.UI.Machine;
using Hemo.Model;

namespace Hemo.Client.UI.ReportChart
{
    public partial class QueryComplication : ViewBase
    {
        #region 成员变量
        /// <summary>
        /// 工作量数据表
        /// </summary>
        private HemoModel.MED_COMPLICATION_OTHERDataTable complicationTable = new HemoModel.MED_COMPLICATION_OTHERDataTable();

        /// <summary>
        /// 数据服务层
        /// </summary>
        private IHemodialysis hemodialysisService = ServiceManager.Instance.HemodialysisService;
        #endregion

        #region 构造函数
        /// <summary>
        /// 构造函数
        /// </summary>
        public QueryComplication()
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
        /// 
        private void QueryWorkload_Load(object sender, EventArgs e)
        {
            this.deBeginTime.DateTime = DateTime.Now.Date;
            Query();
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
        /// 打印
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnPrint_Click(object sender, EventArgs e)
        {
            ComplicationReport report = new ComplicationReport(this.deBeginTime.DateTime, this.complicationTable);
            report.ShowDialog();
        }

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAdd_Click(object sender, EventArgs e)
        {
            EditComplication editForm = new EditComplication(this.complicationTable, null);
            if (editForm.ShowDialog() == DialogResult.OK)
            {
                Query();
            }
        }

        /// <summary>
        /// 删除一行数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (this.gvComplication.GetFocusedDataRow() == null)
            {
                XtraMessageBox.Show("请选择一行要删除的记录！");
                return;
            }

            if (XtraMessageBox.Show("是否确认删除当前选择记录？", "删除提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            {
                return;
            }

            HemoModel.MED_COMPLICATION_OTHERRow dr = ((DataRowView)this.gvComplication.GetFocusedRow()).Row as HemoModel.MED_COMPLICATION_OTHERRow;
            if (dr != null)
            {
                dr.Delete();
                this.hemodialysisService.SaveComplication(this.complicationTable);
            }
        }

        /// <summary>
        /// 双击列表行
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gcWorkload_DoubleClick(object sender, EventArgs e)
        {
            HemoModel.MED_COMPLICATION_OTHERRow dr = ((DataRowView)this.gvComplication.GetFocusedRow()).Row as HemoModel.MED_COMPLICATION_OTHERRow;
            if (dr != null)
            {
                EditComplication editForm = new EditComplication(this.complicationTable, dr);
                if (editForm.ShowDialog() == DialogResult.OK)
                {
                    Query();
                }
            }
        }
        #endregion

        #region 方法
        /// <summary>
        /// 查询数据
        /// </summary>
        private void Query()
        {
            this.complicationTable = this.hemodialysisService.GetComplicationByDate(this.deBeginTime.DateTime);
            this.gcComplication.DataSource = this.complicationTable;
        }
        #endregion
    }
}