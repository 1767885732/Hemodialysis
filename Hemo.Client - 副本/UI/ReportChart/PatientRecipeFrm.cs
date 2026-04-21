/*----------------------------------------------------------------
 * Copyright (C) 2005 (苏州)医疗科技发展有限公司
 * 文件功能描述:患者处方线形图
 * 创建标识:刘超-2016年9月20日
 * ----------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Hemo.IService;
using Hemo.Service;
using Hemo.Utilities;
using Hemo.Client.UI.Machine;

namespace Hemo.Client.UI.ReportChart
{
    public partial class PatientRecipeFrm : ViewBase
    {
        #region 变量
        
        private IPatient _patientService = ServiceManager.Instance.PatientService;

        private string hemoId = string.Empty;

        public string HemoId
        {
            get { return hemoId; }
            set { hemoId = value; }
        }

        public string Title
        {
            get;
            set;
        }

        #endregion

        #region 构造函数

        public PatientRecipeFrm()
        {
            InitializeComponent();
        }


        #endregion

        #region 方法

        /// <summary>
        /// 初始化时间控件
        /// </summary>
        public void InzationDateControl()
        {
            DateTime dt = DateTime.Now.Date;
            DateTime startQuarter = dt.AddMonths(0 - (dt.Month - 1) % 3).AddDays(1 - dt.Day);  //本季度初 

            DateTime endQuarter = startQuarter.AddMonths(3).AddDays(-1);  //本季度末  

            this.beginTime.DateTime = startQuarter;
            this.endTime.DateTime = endQuarter;
        }

        public delegate DataSet GetPatientRecipeChartDelegate(string hemoId, DateTime beginTime, DateTime endTime);

        public GetPatientRecipeChartDelegate OnGetPatientRecipeChart
        {
            get;
            set;
        }

        /// <summary>
        /// 初始化数据
        /// </summary>
        public void InzationData()
        {
            using (BackgroundWorker worker = new BackgroundWorker())
            {
                var data = new DataSet();
                var tableResult = new DataTable();
                worker.DoWork += delegate(object sender1, DoWorkEventArgs e1)
                {
                    //data = _patientService.GetPatientRecipeChart(hemoId, this.beginTime.DateTime, this.endTime.DateTime);
                    data = OnGetPatientRecipeChart?.Invoke(hemoId, this.beginTime.DateTime, this.endTime.DateTime);
                };
                worker.RunWorkerCompleted += delegate(object sender2, RunWorkerCompletedEventArgs e2)
                {
                    #region 把两个表合并一个表并且进行行转列

                    #region 把两个表中有变化的数据都放到一个临时的表中

                    var dtTemp = new DataTable();

                    dtTemp = data.Tables[0].Clone();
                    dtTemp.Rows.Clear();
                    foreach (DataTable dt in data.Tables)
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            if (i == 0)
                            {
                                dtTemp.ImportRow(dt.Rows[i]);
                            }
                            else
                            {
                                if (dt.Rows[i]["NAME"].ToString() == dt.Rows[i - 1]["NAME"].ToString() &&
                                    dt.Rows[i]["VALUE"].ToString() != dt.Rows[i - 1]["VALUE"].ToString())
                                {
                                    dtTemp.ImportRow(dt.Rows[i]);
                                }
                                else if (dt.Rows[i]["NAME"].ToString() != dt.Rows[i - 1]["NAME"].ToString())
                                {
                                    dtTemp.ImportRow(dt.Rows[i]);
                                }

                            }
                        }
                    }

                    #endregion

                    #region 建立报表展示的表结构    【行转列】
                    tableResult = new DataTable("ChartData");
                    tableResult.Columns.Add(new DataColumn("类型"));
                    List<string> listTime = new List<string>();


                    foreach (DataRow dr in dtTemp.Rows)
                    {
                        //加入list
                        if (!listTime.Contains(dr["TIME"].ToString()))
                        {
                            listTime.Add(dr["TIME"].ToString());
                        }

                    }
                    //排序后加入到table列
                    List<string> listTime1 = listTime.OrderBy(i => i).ToList<string>();
                    foreach (var colunname in listTime1)
                    {
                        //动态添加列
                        if (!tableResult.Columns.Contains(colunname))
                        {
                            tableResult.Columns.Add(new DataColumn(colunname, typeof(string)));

                            //for (int i = 0; i < 3; i++)
                            //{
                            //    tableResult.Columns.Add(new DataColumn(colunname+i, typeof(decimal)));

                            //}
                        }
                    }

                    #endregion
                    #endregion

                    #region 对于行转列的表进行赋值
                    #region 分组显示的数据类型

                    //把两个table的值直接给新结构表
                    //分组
                    var query = from t in dtTemp.AsEnumerable()
                                group t by new { t1 = t.Field<string>("NAME") } into m
                                select new
                                {
                                    name = m.Key.t1
                                };
                    //有几组加几行.
                    if (query.ToList().Count > 0)
                    {

                        query.ToList().ForEach(q =>
                        {
                            var row = tableResult.NewRow();
                            row["类型"] = q.name;
                            tableResult.Rows.Add(row);
                        });
                    }

                    #endregion

                    #region 对表数据进行赋值

                    //对于已加的行其它列进行赋值
                    foreach (DataRow dr in dtTemp.Rows)
                    {
                        foreach (DataRow rdr in tableResult.Rows)
                        {
                            if (rdr["类型"].ToString() == dr["NAME"].ToString())
                            {
                                rdr[dr["TIME"].ToString()] = Utility.CDecimal(dr["VALUE"].ToString());
                            }
                        }
                    }

                    #endregion

                    #region 对于表中的空白行进行处理.如果一开始是空白就让他空白吧

                    //对于结构表中为空的数据取他的上一列数据。表示无变化
                    foreach (DataRow dr in tableResult.Rows)
                    {
                        for (int i = 1; i < tableResult.Columns.Count; i++)
                        {
                            if (string.IsNullOrEmpty(dr[i].ToString()) && i != 1)
                            {
                                //对于为空的列，赋值取上一列的值。
                                dr[i] = dr[i - 1].ToString();
                            }
                        }
                    }

                    #endregion

                    #endregion

                    #region 报表展示

                    //给报表赋值..
                    this.ctlTendencyChart1.InzatioData(tableResult);
                    //this.ctlTendencyChart1.SetContolTitle("干体重与抗凝变化趋势图");
                    this.ctlTendencyChart1.SetContolTitle(this.Title);

                    #endregion
                };
                worker.RunWorkerAsync();
            }
        }

        #endregion

        #region 事件

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnQuery_Click(object sender, EventArgs e)
        {
            InzationData();
        }
        /// <summary>
        /// 导出
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnExpExcel_Click(object sender, EventArgs e)
        {
            this.ctlTendencyChart1.ExportGridToExcel();
        }
        /// <summary>
        /// 打印
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnPrint_Click(object sender, EventArgs e)
        {
            this.ctlTendencyChart1.PrintGridView();
        }

        /// <summary>
        /// 加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PatientRecipeFrm_Load(object sender, EventArgs e)
        {
            //InzationData();
        }

        #endregion
    }
}
