/*----------------------------------------------------------------
 * Copyright (C) 2005 (苏州)医疗科技发展有限公司
 * 文件功能描述:重写常用控件以满足血透需求
 * 创建标识:顾伟伟-2013年7月4日
 * 
 * 修改时间:2013年10月12日
 * 修改人:刘超
 * 修改描述:重写方法OnCreateLookupDisplayFilter
 * 
 * 修改时间:2014年1月20日
 * 修改人:顾伟伟
 * 修改描述:GridControl重写
 * 
 * 修改时间:2014年4月30日
 * 修改人:吕志强
 * 修改描述:GridPainter重写
 * ----------------------------------------------------------------*/
using System.Collections.Generic;
using System.Windows.Forms;
using DevExpress.Data.Filtering;
using DevExpress.Data.Filtering.Helpers;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Registrator;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Grid.Drawing;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;

namespace Hemo.Utilities
{
    #region 重写GridView控件
    public class CustomGridView : GridView
    {
        public CustomGridView() : base() { }

        public string CustomFilterString = "";

        protected internal virtual void SetGridControlAccessMetod(GridControl newControl)
        {
            SetGridControl(newControl);
        }

        protected override string OnCreateLookupDisplayFilter(string text, string displayMember)
        {
            List<CriteriaOperator> subStringOperators = new List<CriteriaOperator>();
            foreach (string sString in text.Split(' '))
            {
                string exp = LikeData.CreateContainsPattern(sString);
                List<CriteriaOperator> columnsOperators = new List<CriteriaOperator>();
                foreach (GridColumn col in Columns)
                {
                    if (col.Visible && col.ColumnType == typeof(string))
                        columnsOperators.Add(new BinaryOperator(col.FieldName, exp, BinaryOperatorType.Like));
                }
                subStringOperators.Add(new GroupOperator(GroupOperatorType.Or, columnsOperators));
            }
            SendKeys.Send("{Down}");
            return new GroupOperator(GroupOperatorType.And, subStringOperators).ToString();
        }

        protected override string ViewName { get { return "CustomGridView"; } }

        protected internal virtual string GetExtraFilterText { get { return ExtraFilterText; } }
    }
    #endregion

    #region 重写GridControl控件
    public class CustomGridControl : GridControl
    {
        public CustomGridControl() : base() { }

        protected override void RegisterAvailableViewsCore(InfoCollection collection)
        {
            base.RegisterAvailableViewsCore(collection);
            collection.Add(new CustomGridInfoRegistrator());
        }

        protected override BaseView CreateDefaultView()
        {
            return CreateView("CustomGridView");
        }
    }
    #endregion

    #region 重写GridPainter控件
    public class CustomGridPainter : GridPainter
    {
        public CustomGridPainter(GridView view) : base(view) { }

        public virtual new CustomGridView View { get { return (CustomGridView)base.View; } }

        protected override void DrawRowCell(GridViewDrawArgs e, GridCellInfo cell)
        {
            cell.ViewInfo.MatchedStringUseContains = true;
            cell.ViewInfo.MatchedString = View.GetExtraFilterText;
            cell.State = GridRowCellState.Default;
            e.ViewInfo.UpdateCellAppearance(cell);
            base.DrawRowCell(e, cell);
        }
    }
    #endregion

    #region 重写GridInfoRegistrator控件
    public class CustomGridInfoRegistrator : GridInfoRegistrator
    {
        public CustomGridInfoRegistrator() : base() { }

        public override BaseViewPainter CreatePainter(BaseView view) { return new CustomGridPainter(view as DevExpress.XtraGrid.Views.Grid.GridView); }

        public override string ViewName { get { return "CustomGridView"; } }

        public override BaseView CreateView(GridControl grid)
        {
            CustomGridView view = new CustomGridView();
            view.SetGridControlAccessMetod(grid);
            return view;
        }
    }
    #endregion
}