/*----------------------------------------------------------------
// Copyright (C) 2013 苏州XX公司有限公司
// 描述：查询透析机采集参数表数据
// 创建时间：2014-07-21
// 创建者：刘超
//  
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
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraEditors.Controls;
using Hemo.Service;
using Hemo.Model;
using Hemo.IService;
using Hemo.Client.UI.Hemodialysis;
using Hemo.Utilities;
using Hemo.IService.Config;

namespace Hemo.Client.UI.Patient
{
    public partial class QueryParametersCollection :HemoBaseFrm
    {

        #region 私有成员
        /// <summary>
        /// 病人列表
        /// </summary>
        private IHemodialysis _hemodialysisService = ServiceManager.Instance.HemodialysisService;

        #endregion

        #region 共有成员

        /// <summary>
        /// 选择的行数据透析号
        /// </summary>
        private string _hemodialysisID = string.Empty;
        public string HemodialysisID
        {
            get
            {
                return _hemodialysisID;
            }
            set
            {
                _hemodialysisID = value;
            }
        }

        #endregion

        #region 初始化方法
        public QueryParametersCollection()
        {
            InitializeComponent();
            cmdDate.EditValue = System.DateTime.Now.ToShortDateString();
        }
        #endregion

        #region 各种事件
        private void btnQuery_Click(object sender, EventArgs e)
        {
            if (!validata())
            {
                loadGridData();
            }
        }
        #endregion

        #region 数据方法
        private void loadGridData()
        {
            string strMonitor = cmbMachineLabel.Text;
            DateTime dDate = Utility.CDate(cmdDate.EditValue.ToString());
            HemodialysisModel.MED_HEMO_PARAMETERS_COLLECTIONDataTable dataTable = _hemodialysisService.GetHemoParametersCollectionByMonitorAndDate(strMonitor, dDate);
            if (dataTable != null && dataTable.Rows.Count > 0)
            {
                gridControl1.DataSource = dataTable;
            }
            else
            {
                MessageBox.Show("当前没有采集到血透机的相关数据!");
                gridControl1.DataSource = null;
            }
        }

        private bool validata()
        {
            bool result = false;
            if (cmbMachineLabel.Text.Length == 0)
            {
                MessageBox.Show("请选择要查询的透析机!");
                return true;
            }
            if (cmdDate.EditValue == null)
            {
                MessageBox.Show("请输入透析日期!");
                return true;
            }
            return result;
        }

        private void loadInitData()
        {

            ////班次
            //DataTable dtBANCI = new DataTable();
            //dtBANCI.Columns.Add(new DataColumn("ITEM_ID"));
            //dtBANCI.Columns.Add(new DataColumn("ITEM_NAME"));

            //DataRow row = dtBANCI.NewRow();
            //row["ITEM_ID"] = "1";
            //row["ITEM_NAME"] = "上午";
            //dtBANCI.Rows.Add(row);

            //row = dtBANCI.NewRow();
            //row["ITEM_ID"] = "2";
            //row["ITEM_NAME"] = "下午";
            //dtBANCI.Rows.Add(row);

            //row = dtBANCI.NewRow();
            //row["ITEM_ID"] = "3";
            //row["ITEM_NAME"] = "晚班";
            //dtBANCI.Rows.Add(row);

            //row = dtBANCI.NewRow();
            //row["ITEM_ID"] = "4";
            //row["ITEM_NAME"] = "急诊";
            //dtBANCI.Rows.Add(row);

            //Utility.BindLookUpEdit(cmbBanci, "ITEM_ID", "ITEM_NAME", dtBANCI, "ITEM_NAME", "班次");

            //this.cmbBanci.EditValue = "1";
        }
        #endregion

        /// <summary>
        /// 关闭
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}