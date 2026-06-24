/*----------------------------------------------------------------
// Copyright (C) 2005 苏州XX公司有限公司
// 描述：维修记录打印窗体
// 创建时间：2016-06-14
// 创建者：刘超
//  
// 修改时间：
// 修改人：
// 修改描述：
----------------------------------------------------------------*/

using System;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Hemo.Utilities;
using Hemo.Client.Print;

namespace Hemo.Client.UI.Machine
{
    public partial class MachineReportFrm :HemoBaseFrm
    {
        #region 构造函数

        public MachineReportFrm(DateTime beginTime,DateTime endDateTime)
        {
            this.InitializeComponent();

            this.barEditItem_begin.EditValue = beginTime;

            this.barEditItem_end.EditValue = endDateTime;

            this.barEditItem_end.EditValueChanged += new EventHandler(barEditReportDate_EditValueChanged);
        }

        #endregion

        #region 方法

        /// <summary>
        /// 初始化报表
        /// </summary>
        private void InitReport()
        {

            if (this.barEditItem_begin.EditValue == null || this.barEditItem_end.EditValue == null)
            {
                XtraMessageBox.Show("请选择日期！", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);

                return;
            }

            if (DateTime.Parse(this.barEditItem_begin.EditValue.ToString()) > DateTime.Parse(this.barEditItem_end.EditValue.ToString()))
            {
                XtraMessageBox.Show("开始日期不能大于结束日期！", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);

                return;
            }

            RepairReport report = new RepairReport(Utility.CDate(this.barEditItem_begin.EditValue.ToString()), Utility.CDate(this.barEditItem_end.EditValue.ToString()));
            this.printControl1.PrintingSystem = report.PrintingSystem;
            report.CreateDocument();
        }

        #endregion

        #region 事件

        private void PatientScheduleReport_Load(object sender, EventArgs e)
        {
            this.InitReport();
        }

        void barEditReportDate_EditValueChanged(object sender, EventArgs e)
        {
            this.InitReport();
        }

        private void barEditItem_end_EditValueChanged(object sender, EventArgs e)
        {
            this.InitReport();
        }

        #endregion
    }
}