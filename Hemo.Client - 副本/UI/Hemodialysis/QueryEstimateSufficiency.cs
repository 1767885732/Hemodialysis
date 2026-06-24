/*----------------------------------------------------------------
 * Copyright (C) 2005 (苏州)医疗科技发展有限公司
 * 文件功能描述:用户控件类
 * 创建标识:吕志强-2013年7月19日
 * 
 * 修改时间:2013年10月27日
 * 修改人:刘超
 * 修改描述:新增方法SQL
 * 
 * 修改时间:2014年2月4日
 * 修改人:顾伟伟
 * 修改描述:修改方法SQL
 * 
 * 修改时间:2014年5月15日
 * 修改人:贺建操
 * 修改描述:修改方法SQL
 * ----------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Collections;
using Hemo.IService.Config;
using Hemo.Model;
using Hemo.Service;
using DevExpress.XtraPrinting;

namespace Hemo.Client.UI.Hemodialysis
{
    public partial class QueryEstimateSufficiency : HemoBaseFrm
    {
        #region 类变量

        private string hemoId;

        private IHemodialysis hemoService = ServiceManager.Instance.HemodialysisService;

        private HemodialysisModel.MED_ESTIMATE_SUFFICIENCYDataTable dtSufficiency = null;

        #endregion

        #region 属性

        /// <summary>
        /// 透析编号
        /// </summary>
        public string HemoId
        {
            get { return hemoId; }
            set { hemoId = value; }
        }

        #endregion

        #region 构造函数

        public QueryEstimateSufficiency() {
            InitializeComponent();
        }

        #endregion

        #region 事件

        /// <summary>
        /// 加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void QueryEstimateSufficiency_Load(object sender, EventArgs e) {
            this.chkURR.Checked = true;
            this.chkTS.Checked = true;
            this.chkMDRD.Checked = true;

            this.Text = "查询患者URR|Kt/V|TS|MDRD评估记录";

            ProFunctionCount pfc = new ProFunctionCount();
            pfc.SaveFunctionCountFrm(this);
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnQuery_Click(object sender, EventArgs e) {
            Query();
        }

        #endregion

        #region 方法

        /// <summary>
        /// 查询
        /// </summary>
        private void Query() {
            if (this.txtFromDate.EditValue != null && this.txtToDate.EditValue == null) {
                XtraMessageBox.Show("请选择结束日期！", "查询患者URR|Kt/V|TS|MDRD评估记录");
                return;
            }

            if (this.txtFromDate.EditValue == null && this.txtToDate.EditValue != null) {
                XtraMessageBox.Show("请选择开始日期！", "查询患者URR|Kt/V|TS|MDRD评估记录");
                return;
            }

            if (this.chkURR.Checked == false && this.chkTS.Checked == false && this.chkMDRD.Checked == false) {
                dtSufficiency = null;
            }
            else {
                ArrayList list = new ArrayList();
                if (this.chkURR.Checked) {
                    list.Add("0");
                }
                if (this.chkTS.Checked) {
                    list.Add("1");
                }
                if (this.chkMDRD.Checked) {
                    list.Add("2");
                }

                if (this.txtFromDate.EditValue == null && this.txtToDate.EditValue == null) {
                    dtSufficiency = hemoService.GetEstimateSufficiencyByFlag((string[])list.ToArray(typeof(string)));
                }
                else if (this.txtFromDate.EditValue != null && this.txtToDate.EditValue != null) {
                    dtSufficiency = hemoService.GetEstimateSufficiencyByFlagAndDate((string[])list.ToArray(typeof(string)), this.txtFromDate.DateTime, this.txtToDate.DateTime);
                }
            }

            this.gcSufficiency.DataSource = dtSufficiency;
        }

        #endregion
        /// <summary>
        /// 报表查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void printableComponentLink1_CreateReportHeaderArea(object sender, DevExpress.XtraPrinting.CreateAreaEventArgs e) {
            string reportHeader = "血液透析患者评估情况";
            e.Graph.StringFormat = new BrickStringFormat(StringAlignment.Center);
            e.Graph.Font = new Font("Tahoma", 14, FontStyle.Bold);
            RectangleF rec = new RectangleF(0, 0, e.Graph.ClientPageSize.Width, 50);
            e.Graph.DrawString(reportHeader, Color.Black, rec, BorderSide.None);
        }
        /// <summary>
        /// 打印
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        private void btnPrint_Click(object sender, EventArgs e) {
            this.printableComponentLink1.CreateDocument();
            this.printableComponentLink1.ShowPreview();
        }


    }
}