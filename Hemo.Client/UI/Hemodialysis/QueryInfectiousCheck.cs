/*----------------------------------------------------------------
 * Copyright (C) 2005 (苏州)医疗科技发展有限公司
 * 文件功能描述:用户控件类
 * 创建标识:顾伟伟-2013年6月28日
 * 
 * 修改时间:2013年10月6日
 * 修改人:顾伟伟
 * 修改描述:修改方法
 * 
 * 修改时间:2014年1月14日
 * 修改人:吕志强
 * 修改描述:修改方法SQL
 * 
 * 修改时间:2014年4月24日
 * 修改人:刘超
 * 修改描述:新增方法SQL
 * ----------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraGrid.Views.BandedGrid;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraEditors.Repository;
using Hemo.IService.Config;
using Hemo.Service;
using Hemo.Model;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using Hemo.Utilities;
using System.Threading;


namespace Hemo.Client.UI.Hemodialysis {
    public partial class QueryAirDisnfect :HemoBaseFrm {
        /// <summary>
        /// 变量
        /// </summary>
        private IHemodialysis objHemodialysisService = ServiceManager.Instance.HemodialysisService;
        private const string CHOOSE_FIELDNAME = "CHOOSE";
        private DataTable dt;
        /// <summary>
        /// 构造函数
        /// </summary>
        public QueryAirDisnfect() {
            InitializeComponent();

            this.Text = "传染病入院检查";

            ProFunctionCount pfc = new ProFunctionCount();
            pfc.SaveFunctionCountFrm(this);

            InitGrid();
        }
        #region 事件
        public void InitGrid() {

            BandedGridView view = advBandedGridView1 as BandedGridView;

            view.BeginUpdate(); //开始视图的编辑，防止触发其他事件
            view.BeginDataUpdate(); //开始数据的编辑

            view.Bands.Clear();

            //修改附加选项
            view.OptionsView.ShowColumnHeaders = false;                         //因为有Band列了，所以把ColumnHeader隐藏
            view.OptionsView.ShowGroupPanel = false;                            //如果没必要分组，就把它去掉
            view.OptionsView.EnableAppearanceEvenRow = false;                   //是否启用偶数行外观
            view.OptionsView.EnableAppearanceOddRow = true;                     //是否启用奇数行外观
            view.OptionsView.ShowFilterPanelMode = ShowFilterPanelMode.Never;   //是否显示过滤面板
            view.OptionsCustomization.AllowColumnMoving = false;                //是否允许移动列
            view.OptionsCustomization.AllowColumnResizing = false;              //是否允许调整列宽
            view.OptionsCustomization.AllowGroup = false;                       //是否允许分组
            view.OptionsCustomization.AllowFilter = false;                      //是否允许过滤
            view.OptionsCustomization.AllowSort = true;                         //是否允许排序
            view.OptionsSelection.EnableAppearanceFocusedCell = true;           //???
            view.OptionsBehavior.Editable = false;                               //是否允许用户编辑单元格


            //添加列标题
            GridBand bandInfectiousID = view.Bands.AddBand("ID");
            bandInfectiousID.Visible = false; //隐藏ID列
            GridBand bandCheckUserID = view.Bands.AddBand("CHECK_USER_ID");
            bandCheckUserID.Visible = false; //隐藏ID列
         //   GridBand bandChkSelect = view.Bands.AddBand("选择");

            GridBand bandHemoID = view.Bands.AddBand("透析号");
            GridBand bandName = view.Bands.AddBand("姓名");
            GridBand bandHepatitisBTitle = view.Bands.AddBand("乙肝");
            GridBand bandHepatitisB = bandHepatitisBTitle.Children.AddBand("检查结果");
            GridBand bandHepatitis_B_Date = bandHepatitisBTitle.Children.AddBand("日期");
            GridBand bandHepatitisCTitle = view.Bands.AddBand("丙肝");
            GridBand bandHepatitisC = bandHepatitisCTitle.Children.AddBand("检查结果");
            GridBand bandHepatitis_C_Date = bandHepatitisCTitle.Children.AddBand("日期");
            GridBand bandSyphilisTitle = view.Bands.AddBand("梅毒");//
            GridBand bandSyphilis = bandSyphilisTitle.Children.AddBand("检查结果");
            GridBand bandSyphilis_Date = bandSyphilisTitle.Children.AddBand("日期");
            GridBand bandHivTitle = view.Bands.AddBand("HIV");//
            GridBand bandHiv = bandHivTitle.Children.AddBand("检查结果");
            GridBand bandHiv_Date = bandHivTitle.Children.AddBand("日期");
            GridBand bandCheckUser = view.Bands.AddBand("确认人");
            GridBand bandCreate_Date = view.Bands.AddBand("确认日期");

            //bandChkSelect.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            bandHemoID.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            bandName.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            bandHepatitisBTitle.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            bandHepatitisB.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            bandHepatitis_B_Date.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            bandHepatitisCTitle.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            bandHepatitisC.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            bandHepatitis_C_Date.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            bandSyphilisTitle.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            bandSyphilis.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            bandSyphilis_Date.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            bandHivTitle.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            bandHiv.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            bandHiv_Date.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            bandCheckUser.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            bandCreate_Date.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;


            dt = objHemodialysisService.GetMedInfectiousCheck();

            if (dt != null && dt.Rows.Count > 0) {
            //    dt.Columns.Add(new DataColumn(CHOOSE_FIELDNAME, typeof(bool)));
                grdInfectious.DataSource = dt;
                grdInfectious.MainView.PopulateColumns();

                view.Columns["CHECK_USER"].OwnerBand = bandCheckUser;
                view.Columns["INFECTIOUS_ID"].OwnerBand = bandInfectiousID;

        //        view.Columns["CHOOSE"].OwnerBand = bandChkSelect;
                view.Columns["PHEMODIALYSIS_ID"].OwnerBand = bandHemoID;
                view.Columns["NAME"].OwnerBand = bandName;
                view.Columns["HEPATITIS_B_NAME"].OwnerBand = bandHepatitisB;
                view.Columns["HEPATITIS_B_DATE"].OwnerBand = bandHepatitis_B_Date;
                view.Columns["HEPATITIS_C_NAME"].OwnerBand = bandHepatitisC;
                view.Columns["HEPATITIS_C_DATE"].OwnerBand = bandHepatitis_C_Date;
                view.Columns["SYPHILIS_NAME"].OwnerBand = bandSyphilis;
                view.Columns["SYPHILIS_DATE"].OwnerBand = bandSyphilis_Date;
                view.Columns["HIV_NAME"].OwnerBand = bandHiv;
                view.Columns["HIV_DATE"].OwnerBand = bandHiv_Date;
                view.Columns["CHECK_USER_ID"].OwnerBand = bandCheckUserID;
                view.Columns["CREATE_DATE"].OwnerBand = bandCreate_Date;
                //  bandChkSelect.View.OptionsBehavior.Editable = true;

            }
            view.EndDataUpdate();//结束数据的编辑
            view.EndUpdate();   //结束视图的编辑
        }

        private void btnQuery_Click(object sender, EventArgs e) {
            InitGrid();
        }

        private void btnConfirm_Click(object sender, EventArgs e) {
            EditInfectiousCheck frm = new EditInfectiousCheck();
            frm.HemodialysisID = advBandedGridView1.GetFocusedDataRow()["PHEMODIALYSIS_ID"].ToString();
            frm.infectiousDataTable = dt;
            frm.ShowDialog();
            if (frm.DialogResult == System.Windows.Forms.DialogResult.Yes) {
                InitGrid();
            }
        }

        private void grdInfectious_MouseDown(object sender, MouseEventArgs e) {

        }
        #endregion
    }
}