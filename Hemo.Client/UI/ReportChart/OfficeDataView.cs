/*----------------------------------------------------------------
 * Copyright (C) 2005 (苏州)医疗科技发展有限公司
 * 文件功能描述:Office数据视图
 * 创建标识:贺建操-2016年7月25日
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
using Hemo.Client.Properties;
using Hemo.IService.Dict;
using Hemo.Service;
using Hemo.Model;

namespace Hemo.Client.UI.ReportChart {
    [ToolboxItem(true)]
    public partial class OfficeDataView : ViewBase
    {
        #region 类变量

        private Hemo.IService.Config.IHemodialysis _hemodialysis = Hemo.Service.ServiceManager.Instance.HemodialysisService;
        private DateTime startWeek = DateTime.Now;
        private DateTime endWeek = DateTime.Now;
        private IStaffDict _staffDictService = ServiceManager.Instance.StaffDictService;
        private DictModel.MED_STAFF_DICTDataTable dtStaffSict = new DictModel.MED_STAFF_DICTDataTable();

        #endregion

        #region 构造函数

        public OfficeDataView()
        {
            InitializeComponent();
        }

        #endregion

        #region 事件

        /// <summary>
        /// 时间改变
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void beginTime_EditValueChanged(object sender, EventArgs e)
        {
            DateTime dt = beginTime.DateTime;
            startWeek = dt.AddDays(1 - Convert.ToInt32(dt.DayOfWeek.ToString("d")));  //本周周一 
            endWeek = startWeek.AddDays(6);  //本周周日  
            //获取当前时间是第几周
            this.label_TITLE.Text = string.Format("当前为{0}年第{1}周数据", dt.Year.ToString(), GetWeekOfYear(dt));
            InzationData();
        }

        /// <summary>
        /// 面板绘制
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void panelControl1_Paint(object sender, PaintEventArgs e)
        {

        }

        #endregion

        #region 方法

        /// <summary>
        /// 获取一年中的周数
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        private static int GetWeekOfYear(DateTime dt)
        {
            //一.找到第一周的最后一天（先获取1月1日是星期几，从而得知第一周周末是几）
            int firstWeekend = 7 - Convert.ToInt32(DateTime.Parse(dt.Year + "-1-1").DayOfWeek);

            //二.获取今天是一年当中的第几天
            int currentDay = dt.DayOfYear;
            //三.（今天 减去 第一周周末）/7 等于 距第一周有多少周 再加上第一周的1 就是今天是今年的第几周了
            //    刚好考虑了惟一的特殊情况就是，今天刚好在第一周内，那么距第一周就是0 再加上第一周的1 最后还是1
            return Convert.ToInt32(Math.Ceiling((currentDay - firstWeekend) / 7.0)) + 1;
        }

        /// <summary>
        /// 初始化数据
        /// </summary>
        public void InzationData()
        {
            dtStaffSict = _staffDictService.GetStaffDictList();

            DataSet ds = new DataSet();
            ds = _hemodialysis.GetALLOfficeData(startWeek, endWeek);
            this.beginTime.DateTime = startWeek;

            //dt15为当前的值班医生和护士。请去取数据进行显示
            //var doctorname = string.Empty;
            //var nursename = string.Empty;//护士名称
            //if (ds.Tables["dt15"] != null && ds.Tables["dt15"].Rows.Count > 0) {
            //    doctorname = ds.Tables["dt15"].Rows[0][1].ToString();//医生名称
            //    nursename = ds.Tables["dt15"].Rows[1][1].ToString();//医生名称
            //}
            string doctors = string.Empty;
            string nusers = string.Empty;
            if (ds.Tables["15"] != null)
            {
                try
                {
                    string[] sArray = ds.Tables["dt15"].Rows[0][1].ToString().Split(',');
                    foreach (var str in sArray)
                    {
                        var row = dtStaffSict.FindByEMP_NO(str.Trim());
                        if (row != null)
                            doctors += row.NAME + " ";
                    }
                    string[] sArray1 = ds.Tables["dt15"].Rows[1][1].ToString().Split(',');
                    foreach (var str in sArray1)
                    {
                        var row = dtStaffSict.FindByEMP_NO(str.Trim());
                        if (row != null)
                            nusers += row.NAME + " ";
                    }
                }
                catch (Exception e) { }
            }
            if (ds != null)
            {
                this.labelControl_FirstOne.Text = string.Format("◎ 共排班<Color=blue> {0} </Color>人次 ,已上机<Color=blue> {1} </Color>人次\r\n", ds.Tables["dt1"].Rows[0][0].ToString(), ds.Tables["dt2"].Rows[0][0].ToString());

                this.labelControl_Two.Text = string.Format("◎ 今日值班医生<Color=blue>" + doctors + " </Color>,上午透析<Color=blue> {0} </Color>人,住院<Color=blue> {1} </Color>人 ,透析中<Color=blue> {2} </Color>人，等待<Color=blue> {3} </Color>人次", ds.Tables["dt3"].Rows[0][1].ToString(), ds.Tables["dt3"].Rows[1][1].ToString(), ds.Tables["dt4"].Rows[0][0].ToString(), int.Parse(ds.Tables["dt3"].Rows[0][1].ToString()) - int.Parse(ds.Tables["dt4"].Rows[0][0].ToString()));

                this.labelControl_three.Text = string.Format("◎ 今日值班护士<Color=blue>" + nusers + " </Color>,下午透析<Color=blue> {0} </Color>人 ,住院<Color=blue> {1} </Color>人 ,透析中<Color=blue> {2} </Color>人，等待<Color=blue> {3} </Color>人次", ds.Tables["dt5"].Rows[0][1].ToString(), ds.Tables["dt5"].Rows[1][1].ToString(), ds.Tables["dt6"].Rows[0][0].ToString(), int.Parse(ds.Tables["dt5"].Rows[0][1].ToString()) - int.Parse(ds.Tables["dt6"].Rows[0][0].ToString()));

                this.labelControl_four.Text = string.Format("◎ 首次透析<Color=blue> {0} </Color>人,共<Color=blue>  {1} </Color>人", ds.Tables["dt7"].Rows[0][0].ToString(), ds.Tables["dt1"].Rows[0][0].ToString());

                this.labelControl_five.Text = string.Format("◎ 急诊人数<Color=blue> {0} </Color>人,在科抢救人数<Color=blue> {1} </Color>人 共<Color=blue> {2} </Color>人", ds.Tables["dt8"].Rows[0][0].ToString(), ds.Tables["dt9"].Rows[0][0].ToString(), ds.Tables["dt1"].Rows[0][0].ToString());

                //  this.labelControl_six.Text = string.Format("◎ ", ds.Tables["dt9"].Rows[0][0].ToString(), ds.Tables["dt1"].Rows[0][0].ToString());
                // this.labelControl_seven.Text =

                this.labelControl_eight.Text = string.Format("◎ 血管通路手术<Color=blue> {0} </Color>人, 通畅<Color=blue> {1} </Color>人,不通畅<Color=blue> {2} </Color>人,共{3} </Color>人\r\n", ds.Tables["dt11"].Rows[0][2].ToString(), ds.Tables["dt11"].Rows[1][2].ToString(), ds.Tables["dt11"].Rows[2][2].ToString(), ds.Tables["dt1"].Rows[0][0].ToString());

                //this.labelControl1_nine.Text = string.Format("在科抢救人数{0}人 共{1}人");

                this.labelControl_ten.Text = string.Format("◎ 未下诊断<Color=blue> {0} </Color>人,共<Color=blue> {1} </Color>人", ds.Tables["dt12"].Rows[0][0].ToString(), ds.Tables["dt1"].Rows[0][0].ToString()) + string.Format("CRRT患者<Color=blue> {0} </Color>人,共<Color=blue> {1} </Color>人 ", ds.Tables["dt10"].Rows[0][0].ToString(), ds.Tables["dt1"].Rows[0][0].ToString());
                this.labelControl_Eleven.Text = string.Format("◎ 透析中并发症发生<Color=blue> {0} </Color>人,共<Color=blue> {1} </Color>人", ds.Tables["dt13"].Rows[0][0].ToString(), ds.Tables["dt1"].Rows[0][0].ToString());
                string commCount = ds.Tables["dt14"].Rows[0][0].ToString();
                string specialCount = ds.Tables["dt14"].Rows[0][1].ToString();
                this.labelControl_Twelve.Text = string.Format("◎ 健康宣教：常规宣教<Color=blue> {0} </Color>人,特殊宣教<Color=blue> {1} </Color>人,未宣教<Color=blue> {2} </Color>人", commCount, int.Parse(commCount) - int.Parse(specialCount), int.Parse(ds.Tables["dt1"].Rows[0][0].ToString()) - int.Parse(commCount));
            }
            //using (BackgroundWorker worker = new BackgroundWorker())
            //{
            //    worker.DoWork += delegate(object sender1, DoWorkEventArgs e1)
            //    {
            //       ds =  _hemodialysis.GetALLOfficeData(startWeek, endWeek);
            //    };
            //    worker.RunWorkerCompleted += delegate(object sender2, RunWorkerCompletedEventArgs e2)
            //    {


            //    };
            //}
        }

        /// <summary>
        /// 初始化控件
        /// </summary>
        public void IniDateControl()
        {
            DateTime dt = DateTime.Now;
            startWeek = dt.AddDays(1 - Convert.ToInt32(dt.DayOfWeek.ToString("d")));  //本周周一 
            endWeek = startWeek.AddDays(6);  //本周周日 
            this.beginTime.DateTime = startWeek;
            this.label_TITLE.Text = string.Format("当前为{0}年第{1}周数据", dt.Year.ToString(), GetWeekOfYear(dt));

            this.bg1.Image = bg2.Image = global::Hemo.Client.Properties.Resources.line;
            //  this.bg3.Image = global::Hemo.Client.Properties.Resources.k1;
            //  this.bg5.Image = global::Hemo.Client.Properties.Resources.g;
            //   this.bg9.Image = global::Hemo.Client.Properties.Resources.q;

        }

        #endregion
    }
}
