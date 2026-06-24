/*----------------------------------------------------------------
 * Copyright (C) 2005 (苏州)医疗科技发展有限公司
 * 文件功能描述:规则
 * 创建标识:顾伟伟-2013年5月15日
 * 
 * 修改时间:2013年8月23日
 * 修改人:贺建操
 * 修改描述:新增方法
 * 
 * 修改时间:2013年12月1日
 * 修改人:贺建操
 * 修改描述:修改方法
 * 
 * 修改时间:2014年3月11日
 * 修改人:贺建操
 * 修改描述:修改方法SQL
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
using Hemo.Client.Core;
using Hemo.Utilities;

namespace Hemo.Client.UI.Hemodialysis
{
    public partial class EditAppraisalRuleItem : HemoBaseFrm
    {
        #region 类变量

        private IHemodialysis hemoService = ServiceManager.Instance.HemodialysisService;

        private HemodialysisModel.MED_PERFORMANCE_APPRAISAL_RULERow currentRuleItem = null;

        private DataTable typeTable = null;

        private HemodialysisModel.MED_PERFORMANCE_APPRAISAL_RULEDataTable dtRuleItem = new HemodialysisModel.MED_PERFORMANCE_APPRAISAL_RULEDataTable();

        private int addOrderNumber = 1;

        private int minusOrderNumber = 1;

        #endregion

        #region 属性

        public HemodialysisModel.MED_PERFORMANCE_APPRAISAL_RULERow CurrentRuleItem
        {
            get { return currentRuleItem; }
            set { currentRuleItem = value; }
        }

        public DataTable TypeTable
        {
            get { return typeTable; }
            set { typeTable = value; }
        }

        public int AddOrderNumber
        {
            get { return addOrderNumber; }
            set { addOrderNumber = value; }
        }

        public int MinusOrderNumber
        {
            get { return minusOrderNumber; }
            set { minusOrderNumber = value; }
        }

        #endregion

        #region 构造函数

        public EditAppraisalRuleItem()
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
        private void EditAppraisalRuleItem_Load(object sender, EventArgs e)
        {
            BindLookUpEdit();
            LoadAppraisalRuleItem();
        }

        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (currentRuleItem["ID"] != DBNull.Value)
            {
                dtRuleItem.ImportRow(currentRuleItem);
            }
            else
            {
                currentRuleItem.ID = Guid.NewGuid().ToString();
                currentRuleItem.PARENT_ID = (this.lupITEM_TYPE.GetSelectedDataRow() as DataRowView).Row["ID"].ToString();
                currentRuleItem.CREATE_DATE = DateTime.Now;
                currentRuleItem.CREATE_USER = HemoApplicationContext.Current.CurrentUser.EMP_NO;
                dtRuleItem.AddMED_PERFORMANCE_APPRAISAL_RULERow(currentRuleItem);
            }

            int result = hemoService.SavePerformanceAppraisalRule(dtRuleItem);
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

        /// <summary>
        /// 得分类型改变
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lupSCORE_TYPE_EditValueChanged(object sender, EventArgs e)
        {
            if (currentRuleItem != null && currentRuleItem["ID"] == DBNull.Value)
            {
                if (this.lupSCORE_TYPE.EditValue.ToString() != string.Empty)
                {
                    currentRuleItem.ORDER_NUMBER = this.lupSCORE_TYPE.EditValue.ToString().Equals("1") ? addOrderNumber : minusOrderNumber;
                }
            }
        }

        #endregion

        #region 方法

        /// <summary>
        /// 加载考核规则条目
        /// </summary>
        private void LoadAppraisalRuleItem()
        {
            currentRuleItem = currentRuleItem ?? dtRuleItem.NewMED_PERFORMANCE_APPRAISAL_RULERow();
            if (currentRuleItem["ID"] == DBNull.Value)
            {
                currentRuleItem.ITEM_VALUE = 1;
                currentRuleItem.SCORE_TYPE = "1";
                this.lupSCORE_TYPE.EditValue = "1";
            }
            this.bindingSource.DataSource = currentRuleItem;
        }

        /// <summary>
        /// 绑定下拉框
        /// </summary>
        private void BindLookUpEdit()
        {
            BaseControlInfo.BindLookUpEdit(this.lupITEM_TYPE, "ITEM_NAME", "ITEM_NAME", typeTable, "ITEM_NAME", "类别");

            DataTable dtScoreType = new DataTable();
            dtScoreType.Columns.Add("ITEM_NAME", typeof(System.String));
            dtScoreType.Columns.Add("ITEM_VALUE", typeof(System.String));

            var rowScoreType = dtScoreType.NewRow();
            rowScoreType["ITEM_NAME"] = "减分项";
            rowScoreType["ITEM_VALUE"] = "0";
            dtScoreType.Rows.Add(rowScoreType);

            rowScoreType = dtScoreType.NewRow();
            rowScoreType["ITEM_NAME"] = "加分项";
            rowScoreType["ITEM_VALUE"] = "1";
            dtScoreType.Rows.Add(rowScoreType);

            BaseControlInfo.BindLookUpEdit(this.lupSCORE_TYPE, "ITEM_VALUE", "ITEM_NAME", dtScoreType, "ITEM_NAME", "得分类型");

            DataTable dtStatus = new DataTable();
            dtStatus.Columns.Add("ITEM_NAME", typeof(System.String));
            dtStatus.Columns.Add("ITEM_VALUE", typeof(System.String));

            var rowStatus = dtStatus.NewRow();
            rowStatus["ITEM_NAME"] = "停用";
            rowStatus["ITEM_VALUE"] = "0";
            dtStatus.Rows.Add(rowStatus);

            rowStatus = dtStatus.NewRow();
            rowStatus["ITEM_NAME"] = "启用";
            rowStatus["ITEM_VALUE"] = "1";
            dtStatus.Rows.Add(rowStatus);

            BaseControlInfo.BindLookUpEdit(this.lupSTATUS, "ITEM_VALUE", "ITEM_NAME", dtStatus, "ITEM_NAME", "是否启用");
        }

        #endregion
    }
}