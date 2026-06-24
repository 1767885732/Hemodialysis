/*----------------------------------------------------------------
 * Copyright (C) 2005 (苏州)医疗科技发展有限公司
 * 文件功能描述:透析列表
 * 创建标识:贺建操-2013年6月21日
 * 
 * 修改时间:2013年9月29日
 * 修改人:贺建操
 * 修改描述:修改方法
 * 
 * 修改时间:2014年1月7日
 * 修改人:刘超
 * 修改描述:修改方法
 * 
 * 修改时间:2014年4月17日
 * 修改人:顾伟伟
 * 修改描述:新增方法
 * ----------------------------------------------------------------*/
using System;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Grid;
using Hemo.IService.Config;
using Hemo.Model;
using Hemo.Service;

namespace Hemo.Client.UI.Hemodialysis
{
    public partial class HemodialysisList :HemoBaseFrm
    {
        #region 变量

        private HemodialysisModel.MED_BEFORE_HEMODIALYSIS_SIGNDataTable _hemodialysisDataTable;
        private IHemodialysis _hemodialysisService = ServiceManager.Instance.HemodialysisService;

        #endregion

        #region 构造函数

        public HemodialysisList()
        {
            this.InitializeComponent();
        }

        #endregion

        #region 方法

        private void BindGrid()
        {
            this.gcHemodialysis.DataSource = this._hemodialysisDataTable = this._hemodialysisService.GetBeforeHemodialysisSignList();
        }

        #endregion

        #region 事件

        private void HemodialysisList_Load(object sender, EventArgs e)
        {
            this.BindGrid();
        }

        private void gvHemodialysis_RowClick(object sender, RowClickEventArgs e)
        {
            this.btnEdit.Enabled = this.gvHemodialysis.GetFocusedDataRow() != null;
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnQuery_Click(object sender, EventArgs e)
        {
            this.BindGrid();
        }

        /// <summary>
        /// 操作
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnOpt_Click(object sender, EventArgs e)
        {
            bool isAdd = string.Compare(((SimpleButton)sender).Name, "btnAdd", true) == 0;

            EditHemodialysis frm = new EditHemodialysis(this._hemodialysisDataTable, isAdd ? null : this.gvHemodialysis.GetFocusedDataRow() as HemodialysisModel.MED_BEFORE_HEMODIALYSIS_SIGNRow);
            frm.ShowDialog();

            if (frm.DialogResult == DialogResult.Yes)
                this.BindGrid();
        }

        #endregion
    }
}
