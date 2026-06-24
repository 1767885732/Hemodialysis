/*----------------------------------------------------------------
 * Copyright (C) 2005 (苏州)医疗科技发展有限公司
 * 文件功能描述:血透机运行记录报表
 * 创建标识:吕志强-2016年7月15日
 * ----------------------------------------------------------------*/

using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using Hemo.IService.Machine;
using Hemo.Service;
using System.Data;

namespace Hemo.Client.Print
{
    public partial class MachineUserReportNew : DevExpress.XtraReports.UI.XtraReport
    {
        #region 成员变量

        private DateTime useDate;

        private string area;

        private string bed;

        private DataRow rowMachine;

        private IMachine machineService = ServiceManager.Instance.MachineService;

        #endregion

        #region 构造函数

        public MachineUserReportNew(DateTime useDate, string area, string bed, DataRow rowMachine)
        {
            this.useDate = useDate;
            this.area = area;
            this.bed = bed;
            this.rowMachine = rowMachine;
            InitializeComponent();
            BindTableCell();
            LoadReport();
        }

        #endregion

        #region 方法

        /// <summary>
        /// 绑定表格列
        /// </summary>
        private void BindTableCell()
        {
            this.xrTableCell2.DataBindings.Clear();
            this.xrTableCell2.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "BANCI_NAME")});
        }

        /// <summary>
        /// 加载报表
        /// </summary>
        private void LoadReport()
        {
            this.lblHead.Text = string.Format("透析室：{0}   床位：{1}   品牌：{2}   编号：{3}   透析方式：{4}", area, bed, rowMachine["FLNAME"].ToString(), rowMachine["MACHINE_NO"].ToString(), rowMachine["THERAPEUTIC_PROPERTIES"].ToString());
            this.DataSource = machineService.GetMachineUseRecordByIdAndSingleDate(rowMachine["MACHINE_ID"].ToString(), useDate);
        }

        #endregion
    }
}
