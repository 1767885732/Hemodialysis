/*----------------------------------------------------------------
 * Copyright (C) 2005 (苏州)医疗科技发展有限公司
 * 文件功能描述:并发症编辑类
 * 创建标识:吕志强-2016年5月20日
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
using Hemo.Service;
using Hemo.Utilities;
using Hemo.IService;
using Hemo.IService.Config;
using Hemo.IService.Dict;
using Hemo.Client.Core;

namespace Hemo.Client.UI.ReportChart
{
    public partial class EditComplication :HemoBaseFrm
    {
        #region 字段属性
        private HemoModel.MED_COMPLICATION_OTHERDataTable complicatinTable;

        private HemoModel.MED_COMPLICATION_OTHERRow complication;

        private bool isAdd;

        private IHemodialysis hemodialysisService = ServiceManager.Instance.HemodialysisService;
        #endregion

        #region 构造函数
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="workload"></param>
        public EditComplication(HemoModel.MED_COMPLICATION_OTHERDataTable complicatinTable, HemoModel.MED_COMPLICATION_OTHERRow complication)
        {
            InitializeComponent();

            this.complicatinTable = complicatinTable;
            if (complication == null)
            {
                this.isAdd = true;
                this.complication = this.complicatinTable.NewMED_COMPLICATION_OTHERRow();
            }
            else
            {
                this.isAdd = false;
                this.complication = complication;
            }
        }
        #endregion

        #region 事件
        /// <summary>
        /// 窗体加载事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EditWorkload_Load(object sender, EventArgs e)
        {
            if (this.complication == null)
            {
                return;
            }

            foreach (var ctl in this.Controls)
            {
                if (ctl is BaseEdit)
                {
                    (ctl as BaseEdit).BindingDataRow(this.complication, "txt");
                }
            }
            foreach (var ctl in this.groupControl1.Controls)
            {
                if (ctl is BaseEdit)
                {
                    (ctl as BaseEdit).BindingDataRow(this.complication, "txt");
                }
            }
            foreach (var ctl in this.groupControl2.Controls)
            {
                if (ctl is BaseEdit)
                {
                    (ctl as BaseEdit).BindingDataRow(this.complication, "txt");
                }
            }
        }

        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (this.txtWORK_CLASSNUM.EditValue == null)
            {
                XtraMessageBox.Show("请选择班次！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (this.txtWORK_DATE.EditValue == null)
            {
                XtraMessageBox.Show("请选择日期！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (this.isAdd)
            {
                this.complication.ID = Guid.NewGuid().ToString();
                this.complicatinTable.AddMED_COMPLICATION_OTHERRow(this.complication);
            }

            this.hemodialysisService.SaveComplication(this.complicatinTable);
            this.DialogResult = DialogResult.OK;
        }

        /// <summary>
        /// 关闭
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion
    }
}