/*----------------------------------------------------------------
// Copyright (C) 2013 苏州XX公司股份有限公司
// 文件名：CtlPastDataQuery.cs
// 文件功能描述： 自定义控件历史数据查询控件
// 创建标识：刘超 2013-07-22
----------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Hemo.Client.UI.Machine;
using Hemo.IService.Config;
using Hemo.Service;

namespace Hemo.Client.Controls
{
    [ToolboxItem(true)]
    public partial class CtlPastDataQuery : ViewBase
    {
        public CtlPastDataQuery()
        {
            InitializeComponent();
        }
        #region 变量
        /// <summary>
        /// 业务类
        /// </summary>
        private IHemodialysis objHemodialysisService = ServiceManager.Instance.HemodialysisService;


        #endregion
        #region 方法
        /// <summary>
        /// 直接控件绑定以往体重数据
        /// </summary>
        /// <param name="pHemoID">透析号</param>
        /// <param name="dtStar">开始时间</param>
        /// <param name="dtEnd">结束时间</param>
        public void InzationWeightData(string pHemoID, DateTime dtStar, DateTime dtEnd)
        {
            InBpOrWeightControl("体重");
            this.customGridLookUpEdit1.Properties.DataSource = objHemodialysisService.GetPastWeightListByParams(pHemoID, dtStar, dtEnd);

        }
        /// <summary>
        /// 数据进行控件绑定
        /// </summary>
        /// <param name="colvalueName"></param>
        private void InBpOrWeightControl(string colvalueName)
        {
            var gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            var gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
            gridColumn1.Caption = "日期";
            gridColumn1.FieldName = "日期";
            gridColumn1.Name = "gridColumn1";
            gridColumn1.Visible = true;
            gridColumn1.VisibleIndex = 0;


            gridColumn2.Caption = colvalueName;
            gridColumn2.FieldName = "值";
            gridColumn2.Name = "gridColumn2";
            gridColumn2.Visible = true;
            gridColumn2.VisibleIndex = 1;


            this.customGridLookUpEdit1View.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            gridColumn1,gridColumn2});
            this.customGridLookUpEdit1View.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.customGridLookUpEdit1View.Name = "customGridLookUpEdit1View";
            this.customGridLookUpEdit1View.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.customGridLookUpEdit1View.OptionsView.ShowGroupPanel = false;


            this.customGridLookUpEdit1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.customGridLookUpEdit1.Name = "customGridLookUpEdit1";
            this.customGridLookUpEdit1.Properties.DisplayMember = "DISPLAY";
            this.customGridLookUpEdit1.Properties.ValueMember = "DISPLAY";
            this.customGridLookUpEdit1.Properties.NullText = "";

            if (colvalueName == "血压")
            {
                gridColumn1.Width = 150;
                this.customGridLookUpEdit1.Properties.PopupFormSize = new System.Drawing.Size(250, 0);
            }


        }
        /// <summary>
        /// 直接控件绑定以往血压数据
        /// </summary>
        /// <param name="pHemoID">透析号</param>
        /// <param name="dtStar">开始时间</param>
        /// <param name="dtEnd">结束时间</param>
        public void InzationBPData(string pHemoID, DateTime dtStar, DateTime dtEnd)
        {
            InBpOrWeightControl("血压");

            this.customGridLookUpEdit1.Properties.DataSource = objHemodialysisService.GetPastBloodPresureListByParams(pHemoID, dtStar, dtEnd);

        }
        /// <summary>
        /// 直接控件绑定以往充分性评估的数据
        /// </summary>
        /// <param name="pHemoID">透析号</param>
        /// <param name="dtStar">开始时间</param>
        /// <param name="dtEnd">结束时间</param>
        public void InzationSufficiencyData(string pHemoID, DateTime dtStar, DateTime dtEnd)
        {
            var dt = objHemodialysisService.GetEstimateSufficiencyByHemoIdAndDate(pHemoID, "0", dtStar, dtEnd);
            if (dt == null)
                return;
            BangGrid(dt);
            this.customGridLookUpEdit1.Properties.DataSource = dt;
        }
        /// <summary>
        /// 直接绑定数据源
        /// 传进来的Table中必须要有一列为DISPLAY进行下拉选择后的显示项
        /// </summary>
        /// <param name="dt"></param>
        public void InzationControlData(DataTable dt)
        {
            if (dt != null && dt.Rows.Count > 0)
            {
                BangGrid(dt);
                this.customGridLookUpEdit1.Properties.DataSource = dt;
            }

        }
        /// <summary>
        /// 控件动态绑定
        /// </summary>
        /// <param name="dt"></param>
        private void BangGrid(DataTable dt)
        {
            for (int i = 0; i < dt.Columns.Count; i++)
            {
                if (dt.TableName == "MED_ESTIMATE_SUFFICIENCY")
                {
                    if (dt.Columns[i].ToString() == "DISPLAY" || dt.Columns[i].ToString() == "DISPLAY_URR" || dt.Columns[i].ToString() == "HEMODIALYSIS_ID" || dt.Columns[i].ToString() == "IS_DELETE" || dt.Columns[i].ToString() == "ID")
                        continue;
                }
                else
                {
                    if (dt.Columns[i].ToString() == "DISPLAY")
                        continue;
                }
                var gridColumn = new DevExpress.XtraGrid.Columns.GridColumn();
                gridColumn.Caption = dt.Columns[i].Caption.ToString();
                gridColumn.FieldName = dt.Columns[i].ToString();
                gridColumn.Name = "gridColumn" + i.ToString();
                gridColumn.VisibleIndex = i;
                gridColumn.Visible = true;
                this.customGridLookUpEdit1View.Columns.Add(gridColumn);
            }
            this.customGridLookUpEdit1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.customGridLookUpEdit1.Name = "customGridLookUpEdit1";
            string valuemeber = string.Empty;
            if (dt.Columns["DISPLAY"] == null)
                valuemeber = dt.Columns[0].ToString();
            else
                valuemeber = dt.Columns["DISPLAY"].ToString();

            this.customGridLookUpEdit1.Properties.DisplayMember = valuemeber;
            this.customGridLookUpEdit1.Properties.ValueMember = valuemeber;
            this.customGridLookUpEdit1.Properties.NullText = "";

            this.customGridLookUpEdit1.Properties.PopupFormSize = new System.Drawing.Size(dt.Columns.Count * 80, 0);
        }


        #endregion

    }
}
