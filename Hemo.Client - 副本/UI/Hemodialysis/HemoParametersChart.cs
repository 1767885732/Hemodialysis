/*----------------------------------------------------------------
 * Copyright (C) 2005 (苏州)医疗科技发展有限公司
 * 文件功能描述:参数chart图
 * 创建标识:贺建操-2013年6月10日
 * 
 * 修改时间:2013年9月18日
 * 修改人:刘超
 * 修改描述:新增方法SQL
 * 
 * 修改时间:2013年12月27日
 * 修改人:贺建操
 * 修改描述:修改方法SQL
 * 
 * 修改时间:2014年4月6日
 * 修改人:吕志强
 * 修改描述:新增方法
 * ----------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using DevExpress.XtraCharts;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Base;
using Hemo.IService.Config;
using Hemo.Model;
using Hemo.Service;
using Hemo.Utilities;

namespace Hemo.Client.UI.Hemodialysis
{
    public partial class HemoParametersChart :HemoBaseFrm
    {
        #region 变量

        private const char SEPARATOR = '/';
        private Color[] _colorArray = new Color[] { Color.Red, Color.Yellow, Color.Blue, Color.Green, Color.Orange, Color.Violet };
        private HemoModel.MED_HEMODIALYSIS_PARAMS_TYPEDataTable _paramsTypeDataTable;
        private IHemodialysis _hemodialysisService = ServiceManager.Instance.HemodialysisService;

        #endregion

        #region 构造函数

        public HemoParametersChart()
        {
            this.InitializeComponent();
        }

        #endregion

        #region 方法

        private void InitializeControls()
        {
            this._paramsTypeDataTable = this._hemodialysisService.GetHemoParametersType();

            var result = from r in this._paramsTypeDataTable.AsEnumerable()
                         group r by r.GROUPID into g
                         select new
                         {
                             IdArray = (from r in this._paramsTypeDataTable
                                        where r.GROUPID == g.Key
                                        select r.ID).ToArray()
                         };

            DataTable dtBind = new DataTable();

            dtBind.Columns.Add(new DataColumn("id", typeof(string)));
            dtBind.Columns.Add(new DataColumn("text", typeof(string)));

            foreach (var item in result)
            {
                DataRow rowNew = dtBind.NewRow();

                rowNew["id"] = string.Join(SEPARATOR.ToString(), item.IdArray);
                rowNew["text"] = string.Join(SEPARATOR.ToString(), (from id in item.IdArray select this._paramsTypeDataTable.FindByID(id).DISPLAYNAME).ToArray());

                dtBind.Rows.Add(rowNew);
            }

            this.deBeginCreateTime.DateTime = DateTime.Now;
            this.deEndCreateTime.DateTime = DateTime.Now;

            Utility.BindLookUpEdit(this.lueType, "id", "text", dtBind, "text", "治疗参数");
        }

        private void BindData()
        {
            DateTime beginCreateTime = DateTime.Parse(this.deBeginCreateTime.DateTime.ToString("yyyy/MM/dd 00:00:00"));
            DateTime endCreateTime = DateTime.Parse(this.deEndCreateTime.DateTime.ToString("yyyy/MM/dd 23:59:59"));
            DataTable chartBindDataTable = new DataTable();
            HemodialysisModel.MED_HEMODIALYSIS_PARAMETERSDataTable paramsDataTable = this._hemodialysisService.GetHemoParameters(this.txtHemoID.Text, beginCreateTime, endCreateTime);

            this.chartControl1.Series.Clear();

            this.gvParams.Columns.Clear();

            //以治疗参数 + 记录时间作为图标控件数据源和GridView的列头
            foreach (var item in (new string[] { "治疗参数" })
                .Union(paramsDataTable.AsEnumerable().Select(r => r.CREATE_DATE.ToString("yyyy-MM-dd hh:mm:ss"))))
            {
                chartBindDataTable.Columns.Add(new DataColumn(item));

                this.gvParams.Columns.Add(new GridColumn()
                {
                    Caption = item,
                    FieldName = item,
                    VisibleIndex = this.gvParams.Columns.Count
                });
            }

            /*  
            添加图标控件数据源上的数据
            例如：
            治疗参数    2013/5/31 9:00:00   2013/5/31 10:00:00  2013/5/31 11:00:00
            p1              10                                20                                 30
            p2              20                                30                                 40
            */
            foreach (var id in Utility.Split(this.lueType.EditValue.ToString(), SEPARATOR))
            {
                HemoModel.MED_HEMODIALYSIS_PARAMS_TYPERow paramTypeRow = this._paramsTypeDataTable.FindByID(id);
                List<object> valueList = new List<object>();

                valueList.Add(paramTypeRow.DISPLAYNAME);

                valueList.AddRange(from r in paramsDataTable.AsEnumerable()
                                   let value = r[paramTypeRow.FILEDNAME]
                                   select value == DBNull.Value ? 0 : value);

                chartBindDataTable.Rows.Add(valueList.ToArray());
            }

            this.chartControl1.DataSource = this.gcParams.DataSource = chartBindDataTable;

            //创建坐标点
            foreach (DataRow row in chartBindDataTable.Rows)
            {
                DataColumnCollection columns = row.Table.Columns;
                Series series = new Series(row[columns[0].ColumnName].ToString(), ViewType.Line);

                series.View.Color = this._colorArray[chartBindDataTable.Rows.IndexOf(row)];

                for (int i = 1; i < columns.Count; i++)
                {
                    series.Points.Add(new SeriesPoint(columns[i].ColumnName, row[columns[i].ColumnName]));
                }

                this.chartControl1.Series.Add(series);
            }
        }

        #endregion

        #region 事件

        private void HemoParametersChart_Load(object sender, EventArgs e)
        {
            this.InitializeControls();
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSearch_Click(object sender, EventArgs e)
        {
            this.errorProvider.SetError(this.txtHemoID, string.Empty);

            if (string.IsNullOrEmpty(this.txtHemoID.Text))
            {
                this.txtHemoID.Focus();

                this.errorProvider.SetError(this.txtHemoID, "透析号不能为空！");

                return;
            }

            this.BindData();
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

        private void gvParams_CustomDrawCell(object sender, RowCellCustomDrawEventArgs e)
        {
            if (e.RowHandle >= 0 && e.Column.ColumnHandle >= 0)
                e.Appearance.ForeColor = this.chartControl1.Series[this.gvParams.GetDataRow(e.RowHandle).ItemArray[0].ToString()].View.Color;
        }

        #endregion
    }
}
