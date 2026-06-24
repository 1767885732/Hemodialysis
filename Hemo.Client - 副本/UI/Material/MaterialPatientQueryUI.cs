/*----------------------------------------------------------------
 * Copyright (C) 2005 (苏州)医疗科技发展有限公司
 * 文件功能描述:用户控件类
 * 创建标识:吕志强-2013年7月9日
 * 
 * 修改时间:2013年10月17日
 * 修改人:吕志强
 * 修改描述:修改方法
 * 
 * 修改时间:2014年1月25日
 * 修改人:贺建操
 * 修改描述:修改方法SQL
 * 
 * 修改时间:2014年5月5日
 * 修改人:刘超
 * 修改描述:修改方法SQL
 * ----------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Hemo.Client.UI.Machine;
using Hemo.IService;
using Hemo.Service;
using DevExpress.XtraCharts;

namespace Hemo.Client.UI.Material
{
    [ToolboxItem(true)]
    public partial class MaterialPatientQueryUI : ViewBase
    {
        #region 变量
        private IPatient patientService = ServiceManager.Instance.PatientService;

        private DataTable MaterDt = new DataTable();
        #endregion

        /// <summary>
        /// 构造函数
        /// </summary>
        public MaterialPatientQueryUI()
        {
            InitializeComponent();
            if (!this.DesignMode)
            {
                dtStar.EditValue = DateTime.Now.Date;
                dtEnd.EditValue = DateTime.Now.Date.AddDays(1).AddSeconds(-1);
            }
        }

        #region 事件
        public void btnQuery_Click(object sender, EventArgs e)
        {
            using (BackgroundWorker worker = new BackgroundWorker())
            {
                MaterDt = new DataTable();
                worker.DoWork += delegate(object sender1, DoWorkEventArgs e1)
                {
                    MaterDt = patientService.QueryMaterialPatientDataByParam(this.dtStar.DateTime, this.dtEnd.DateTime);

                };
                worker.RunWorkerCompleted += delegate(object sender2, RunWorkerCompletedEventArgs e2)
                {
                    this.gridControl1.DataSource = MaterDt;

                    if (MaterDt.Rows.Count>0)
                    {
                        DrawChartPie(string.Empty, "PRICE");
                        this.panelChart.Visible = true;
                    }
                    else
                    {
                        labelControl3_Click(null, null);

                    }


                    this.gridView1.ExpandAllGroups();

                };
                worker.RunWorkerAsync();
            }
        }

        private void DrawChartPie(string Title, string ChartType)
        {
            this.chartControl.Series.Clear();
            Series _pieSeries = new Series(Title, ViewType.Pie);
            //_pieSeries.ValueDataMembers[0] = "Value";  
            //_pieSeries.ArgumentDataMember = "Name";  
            SeriesPoint point;

            var dtMaterial = from p in MaterDt.AsEnumerable()
                             group p by new { itemType = p.Field<string>("ITEMTYPE") } into g
                             select new
                             {
                                 ID = g.Key.itemType,
                                 price = g.Sum(n => n.Field<decimal>("PRICE")),
                                 count = g.Sum(n => n.Field<decimal>("COUNT"))
                             };

            if (ChartType.Equals("PRICE"))
            {
                dtMaterial.ToList().ForEach(k =>
                {
                    point = new SeriesPoint(k.ID, k.price);
                    _pieSeries.Points.Add(point);
                });

                _pieSeries.LegendPointOptions.PointView = PointView.ArgumentAndValues;
                _pieSeries.Label.Font = new Font("宋体", 8);
                _pieSeries.Label.LineLength = 50;

                _pieSeries.DataSource = dtMaterial.ToList();
                this.chartControl.Series.Add(_pieSeries);
            }
            else
            {
                dtMaterial.ToList().ForEach(k =>
                {
                    point = new SeriesPoint(k.ID, k.count);
                    _pieSeries.Points.Add(point);
                });

                _pieSeries.LegendPointOptions.PointView = PointView.ArgumentAndValues;
                _pieSeries.Label.Font = new Font("宋体", 8);
                _pieSeries.Label.LineLength = 50;

                _pieSeries.DataSource = dtMaterial.ToList();
                this.chartControl.Series.Add(_pieSeries);
            }








        }
        private void gridView1_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            if (e.Clicks == 2)
            {
                var dtRow = this.gridView1.GetFocusedDataRow();
                var MATERIAL_ID = dtRow["MATERIAL_ID"].ToString();
                var MaterDetail = patientService.QueryMaterialPatientDetailByparam(this.dtStar.DateTime, this.dtEnd.DateTime, MATERIAL_ID);
                this.gridControl2.DataSource = MaterDetail;
                this.xtraTabControl1.SelectedTabPage = this.xtraTabPage2;

            }
        }

        private void checkEdit1_CheckedChanged(object sender, EventArgs e)
        {
            this.gridView1.OptionsView.ShowAutoFilterRow = this.checkEdit1.Checked;
            this.gridView2.OptionsView.ShowAutoFilterRow = this.checkEdit1.Checked;


        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.FindForm().Close();
        }

        private void labelControl3_Click(object sender, EventArgs e)
        {
            if (this.panelChart.Visible)
            {
                this.panelChart.Visible = false;
                this.labelControl3.Appearance.Image = global::Hemo.Client.Properties.Resources.left2;

            }
            else
            {
                this.panelChart.Visible = true;
                this.labelControl3.Appearance.Image = global::Hemo.Client.Properties.Resources.right2;


            }
        }
        private void lbHide_MouseHover(object sender, EventArgs e)
        {
            this.labelControl3.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(243)))), ((int)(((byte)(183)))));

        }

        private void lbHide_MouseLeave(object sender, EventArgs e)
        {
            this.labelControl3.Appearance.BackColor = System.Drawing.Color.Transparent;


        }
        private void lbSetChartControl(object sender, EventArgs e)
        {
            if (((DevExpress.XtraEditors.LabelControl) sender).Text.Equals("费用比"))
            {
                DrawChartPie("价格比", "PRICE");
            }
            else
            {
                DrawChartPie("数量比", "COUNT");

            }
        }
        #endregion

    }
}
