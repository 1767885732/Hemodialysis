/*----------------------------------------------------------------
 * Copyright (C) 2005 (苏州)医疗科技发展有限公司
 * 文件功能描述:质控患者数据统计查询类
 * 创建标识:刘超-2017年4月28日
 * ----------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Hemo.IService.Config;
using Hemo.Service;
using Hemo.Client.UI.Machine;
using Hemo.Model;
using Hemo.IService.Lab;
using Hemo.Client.Print;
using DevExpress.XtraReports.UI;

namespace Hemo.Client.UI.ReportChart
{
    public partial class QueryQualityPatientData : ViewBase
    {
        #region 成员变量
        /// <summary>
        /// 工作量数据表
        /// </summary>
        private DataTable _queryQualityPatientData = new DataTable();

        /// <summary>
        /// 数据服务层
        /// </summary>
        private ILab _labData = ServiceManager.Instance.LabService;
        #endregion

        #region 构造函数
        /// <summary>
        /// 构造函数
        /// </summary>
        public QueryQualityPatientData()
        {
            InitializeComponent();
        }
        #endregion

        #region 事件
        /// <summary>
        /// 窗体加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// 
        private void QueryQualityPatientData_Load(object sender, EventArgs e)
        {
            this.deBeginTime.DateTime = DateTime.Now.Date;
            Query();
        }
        
        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnQuery_Click(object sender, EventArgs e)
        {
            Query();
        }

        /// <summary>
        /// 打印
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnPrint_Click(object sender, EventArgs e)
        {
            QueryQualityPatientReport frm = new QueryQualityPatientReport(this._queryQualityPatientData);
            ReportPrintTool pt = new ReportPrintTool(frm);
            pt.ShowPreviewDialog();
        }

     
        #endregion

        #region 方法
        /// <summary>
        /// 查询数据
        /// </summary>
        private void Query()
        {
            DateTime dtTarget = deBeginTime.DateTime;
            DateTime FirstDay = dtTarget.AddDays(1 - dtTarget.Day);
            DateTime LastDay = FirstDay.AddMonths(1).AddDays(-1);
            this._queryQualityPatientData = this._labData.GetMedPatientQualityData(FirstDay, LastDay);

            #region 合计算法
            if (this._queryQualityPatientData.Rows.Count > 0)
            {
                int lowCount = 0;
                int higtCount = 0;
                int pjmztxsjOne = 0;
                int pjmztxsjTwo = 0;
                int pjmztxsjThree = 0;
                int pjmztxsjFour = 0;
                int pjmztxsjFive = 0;
                int pjmztxsjSex = 0;
                int rzqc = 0;
                int sxpxjz = 0;
                int glcj = 0;
                int jzpxkj = 0;
                decimal zgss = 0;
                decimal zgqdbs = 0;
                decimal zgzdbs = 0;

                _queryQualityPatientData.AsEnumerable().ToList().ForEach(row =>
                {
                 
                    #region 平均血压 
                    decimal i = decimal.Parse(row["PJXY"].ToString().Substring(0, row["PJXY"].ToString().IndexOf('/')));
                    decimal j = decimal.Parse(row["PJXY"].ToString().Substring(row["PJXY"].ToString().IndexOf('/') + 1));
                    if (i < 150)
                    {
                        lowCount++;
                    }
                    else
                    {
                        higtCount++;
                    }
                    #endregion

                    #region 平均每周透析时间
                    if (decimal.Parse(row["PJHEMODATE"].ToString()) < 8)
                    {
                        pjmztxsjOne++;
                    }
                    else if (decimal.Parse(row["PJHEMODATE"].ToString()) >= 8 && decimal.Parse(row["PJHEMODATE"].ToString()) <= 9)
                    {
                        pjmztxsjTwo++;
                    }
                    else if (decimal.Parse(row["PJHEMODATE"].ToString()) >= 9 && decimal.Parse(row["PJHEMODATE"].ToString()) <= 10)
                    {
                        pjmztxsjThree++;
                    }
                    else if (decimal.Parse(row["PJHEMODATE"].ToString()) >= 10 && decimal.Parse(row["PJHEMODATE"].ToString()) <= 11)
                    {
                        pjmztxsjFour++;
                    }
                    else if (decimal.Parse(row["PJHEMODATE"].ToString()) >= 11 && decimal.Parse(row["PJHEMODATE"].ToString()) <= 12)
                    {
                        pjmztxsjFive++;
                    }
                    else
                    {
                        pjmztxsjSex++;
                    }
                    #endregion

                    #region 溶质清除
                    if (decimal.Parse(row["RZQC"].ToString()) > 65)
                    {
                        rzqc++;
                    }
                    #endregion

                    #region 肾性贫血纠正
                    if (decimal.Parse(row["SJPXJZ"].ToString()) > 65)
                    {
                        sxpxjz++;
                    }
                    #endregion

                    #region 钙磷乘机
                    if (decimal.Parse(row["GLCJ"].ToString()) > 65)
                    {
                        glcj++;
                    }
                    #endregion

                    #region 甲状旁腺功能亢进
                    if (decimal.Parse(row["JZPXKJ"].ToString()) > 110)
                    {
                        jzpxkj++;
                    }
                    #endregion

                    #region 主观舒适度--舒适
                    if (decimal.Parse(row["ZGSSD"].ToString()) > 0)
                    {
                        zgss = zgss + decimal.Parse(row["ZGSSD"].ToString());
                    }
                    #endregion

                    #region 主观舒适度--轻度不适
                    if (int.Parse(row["ZGQDBS"].ToString()) > 0)
                    {
                        zgqdbs = zgqdbs + int.Parse(row["ZGQDBS"].ToString());
                    }
                    #endregion

                    #region 主观舒适度--重度不适
                    if (int.Parse(row["ZGZDBS"].ToString()) > 0)
                    {
                        zgzdbs = zgzdbs + int.Parse(row["ZGZDBS"].ToString());
                    }
                    #endregion

                });

                DataRow rowSum = this._queryQualityPatientData.NewRow();
                rowSum["REPORT_ADD_DATE"] = DateTime.Now;
                rowSum["NAME"] = "总计倒数";
                rowSum["PJXY"] = string.Format("<150/90:{0} S>=150或者D>=90:{1}", lowCount, higtCount);
                rowSum["PJHEMODATE"] = string.Format("<8:{0} 8~9:{1} 9~10:{2} 10~11:{3} 11~12:{4} >12:{5}", pjmztxsjOne, pjmztxsjTwo, pjmztxsjThree, pjmztxsjFour, pjmztxsjFive, pjmztxsjSex);
                rowSum["RZQC"] = rzqc.ToString();
                rowSum["SJPXJZ"] = sxpxjz.ToString();
                rowSum["GLCJ"] = glcj.ToString();
                rowSum["JZPXKJ"] = jzpxkj.ToString();
                rowSum["ZGSSD"] = zgss.ToString();
                rowSum["ZGQDBS"] = zgqdbs.ToString();
                rowSum["ZGZDBS"] = zgzdbs.ToString();
                this._queryQualityPatientData.Rows.Add(rowSum);

            }
            #endregion


            this.gcPatientData.DataSource = this._queryQualityPatientData;
        }
        #endregion
    }
}