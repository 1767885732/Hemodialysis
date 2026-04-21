/*----------------------------------------------------------------
 * Copyright (C) 2005 (苏州)医疗科技发展有限公司
 * 文件功能描述:修改
 * 创建标识:刘超-2013年7月8日
 * 
 * 修改时间:2013年10月16日
 * 修改人:吕志强
 * 修改描述:新增方法
 * 
 * 修改时间:2014年1月24日
 * 修改人:顾伟伟
 * 修改描述:修改方法SQL
 * 
 * 修改时间:2014年5月4日
 * 修改人:刘超
 * 修改描述:新增方法SQL
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
using Hemo.Utilities;
using Hemo.Client.Core;

namespace Hemo.Client.UI.Hemodialysis
{
    public partial class EditAppraisalRule : HemoBaseFrm
    {
        #region 类变量

        private IHemodialysis hemoService = ServiceManager.Instance.HemodialysisService;

        private HemodialysisModel.MED_PERFORMANCE_APPRAISAL_RULERow currentRule = null;

        private HemodialysisModel.MED_PERFORMANCE_APPRAISAL_RULEDataTable dtRule = new HemodialysisModel.MED_PERFORMANCE_APPRAISAL_RULEDataTable();

        private int orderNumber = 1;

        #endregion

        #region 属性

        public HemodialysisModel.MED_PERFORMANCE_APPRAISAL_RULERow CurrentRule
        {
            get { return currentRule; }
            set { currentRule = value; }
        }

        public int OrderNumber
        {
            get { return orderNumber; }
            set { orderNumber = value; }
        }

        #endregion

        #region 构造函数

        public EditAppraisalRule()
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
        private void EditAppraisalRule_Load(object sender, EventArgs e)
        {
            BindLookUpEdit();
            LoadAppraisalRule();
        }

        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (currentRule["ID"] != DBNull.Value)
            {
                dtRule.ImportRow(currentRule);
            }
            else
            {
                currentRule.ID = Guid.NewGuid().ToString();
                currentRule.ITEM_TYPE = "绩效考核";
                currentRule.CREATE_DATE = DateTime.Now;
                currentRule.CREATE_USER = HemoApplicationContext.Current.CurrentUser.EMP_NO;
                dtRule.AddMED_PERFORMANCE_APPRAISAL_RULERow(currentRule);
            }

            int result = hemoService.SavePerformanceAppraisalRule(dtRule);
            if (result > 0)
            {
                AutoClosedMsgBox.ShowForm("保存成功！", "提示", 1000, MessageBoxIcon.Information);
                this.DialogResult = DialogResult.OK;
            }
        }

        /// <summary>
        /// 取消
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        #endregion

        #region 方法

        /// <summary>
        /// 加载考核规则
        /// </summary>
        private void LoadAppraisalRule()
        {
            currentRule = currentRule ?? dtRule.NewMED_PERFORMANCE_APPRAISAL_RULERow();
            if (currentRule["ID"] == DBNull.Value) { currentRule.ORDER_NUMBER = orderNumber; }
            this.bindingSource.DataSource = currentRule;
        }

        /// <summary>
        /// 绑定下拉框
        /// </summary>
        private void BindLookUpEdit()
        {
            DataTable dtStatus = new DataTable();
            dtStatus.Columns.Add("ITEM_NAME", typeof(System.String));
            dtStatus.Columns.Add("ITEM_VALUE", typeof(System.String));

            var row = dtStatus.NewRow();
            row["ITEM_NAME"] = "停用";
            row["ITEM_VALUE"] = "0";
            dtStatus.Rows.Add(row);

            row = dtStatus.NewRow();
            row["ITEM_NAME"] = "启用";
            row["ITEM_VALUE"] = "1";
            dtStatus.Rows.Add(row);

            BaseControlInfo.BindLookUpEdit(this.lupSTATUS, "ITEM_VALUE", "ITEM_NAME", dtStatus, "ITEM_NAME", "是否启用");
        }

        #endregion
    }
}