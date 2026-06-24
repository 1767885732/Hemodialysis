/*----------------------------------------------------------------
// Copyright (C) 2005 苏州XX公司有限公司
// 描述：血液透析设备使用情况记录单窗体类
// 创建时间：2014-04-16
// 创建者：吕志强
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
using Hemo.Model;
using System.Data;

namespace Hemo.Client.UI.Machine
{
    public partial class UseRecordReport : HemoBaseFrm
    {
        #region 成员变量

        private bool isMachineUseRecord = true;

        private string machineId;

        private string area;

        private string bed;

        private DataRow rowMachine;

        #endregion

        #region 属性

        /// <summary>
        /// 是否血透机使用记录
        /// </summary>
        public bool IsMachineUseRecord
        {
            get { return isMachineUseRecord; }
            set { isMachineUseRecord = value; }
        }

        /// <summary>
        /// 机器ID
        /// </summary>
        public string MachineId
        {
            get { return machineId; }
            set { machineId = value; }
        }

        /// <summary>
        /// 病区
        /// </summary>
        public string Area
        {
            get { return area; }
            set { area = value; }
        }

        /// <summary>
        /// 床位
        /// </summary>
        public string Bed
        {
            get { return bed; }
            set { bed = value; }
        }

        /// <summary>
        /// 机器数据行
        /// </summary>
        public DataRow RowMachine
        {
            get { return rowMachine; }
            set { rowMachine = value; }
        }

        #endregion

        #region 构造函数

        public UseRecordReport()
        {
            this.InitializeComponent();

            this.barEditReportDate.EditValue = DateTime.Now.Date;

            this.barEditReportDate.EditValueChanged += new EventHandler(barEditReportDate_EditValueChanged);
        }

        #endregion

        #region 事件

        /// <summary>
        /// 加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UseRecordReport_Load(object sender, EventArgs e)
        {
            InitReport();
        }

        /// <summary>
        /// 日期改变
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barEditReportDate_EditValueChanged(object sender, EventArgs e)
        {
            InitReport();
        }

        #endregion

        #region 方法

        /// <summary>
        /// 初始化报表
        /// </summary>
        private void InitReport()
        {
            if (this.barEditReportDate.EditValue == null)
            {
                XtraMessageBox.Show("请选择月份！", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (isMachineUseRecord)
            {
                MachineUserReportNew report = new MachineUserReportNew(Utility.CDate(this.barEditReportDate.EditValue.ToString()), area, bed, rowMachine);
                this.printControl1.PrintingSystem = report.PrintingSystem;
                report.CreateDocument();
            }
            else
            {
                WaterProcessorUseReport report = new WaterProcessorUseReport(machineId, Utility.CDate(this.barEditReportDate.EditValue.ToString()));
                this.printControl1.PrintingSystem = report.PrintingSystem;
                report.CreateDocument();
            }
        }

        #endregion
    }
}