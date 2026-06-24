/*----------------------------------------------------------------
 * Copyright (C) 2005 (苏州)医疗科技发展有限公司
 * 文件功能描述:护士交班报表
 * 创建标识:顾伟伟-2013年7月4日
 * 
 * 修改时间:2013年10月12日
 * 修改人:刘超
 * 修改描述:新增方法
 * 
 * 修改时间:2014年1月20日
 * 修改人:吕志强
 * 修改描述:新增方法SQL
 * 
 * 修改时间:2014年4月30日
 * 修改人:贺建操
 * 修改描述:新增方法
 * ----------------------------------------------------------------*/
using System;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Hemo.Utilities;
using Hemo.Client.Print;
using System.Data;
using System.Linq;
using Hemo.Model;
using Hemo.IService.Config;
using Hemo.Service;

namespace Hemo.Client.UI.Hemodialysis
{
    public partial class NurseChangeWorkDateReport : HemoBaseFrm
    {
        #region 构造函数
        private IConfig _configService = ServiceManager.Instance.ConfigService;

        private HemoModel.MED_HEMO_CHAGEWORKDataTable workMaster = new HemoModel.MED_HEMO_CHAGEWORKDataTable();
        private HemoModel.MED_HEMO_CHAGEWORK_EXTENDDataTable workExtend = new HemoModel.MED_HEMO_CHAGEWORK_EXTENDDataTable();

        public NurseChangeWorkDateReport(HemoModel.MED_HEMO_CHAGEWORKDataTable _workMaster, HemoModel.MED_HEMO_CHAGEWORK_EXTENDDataTable _workExtend)
        {
            this.InitializeComponent();
            workMaster = _workMaster;
            workExtend = _workExtend;

            this.barEditReportDate.EditValue = DateTime.Now.Date;

            this.barEditReportDate.EditValueChanged += new EventHandler(barEditReportDate_EditValueChanged);

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
                XtraMessageBox.Show("请选择日期！", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);

                return;
            }

            DateTime date = Utility.CDate(this.barEditReportDate.EditValue.ToString());
            var dtMaster =  new HemoModel.MED_HEMO_CHAGEWORKDataTable();
            var dtDetail = new HemoModel.MED_HEMO_CHAGEWORK_EXTENDDataTable();
            workMaster.Where(i=>i.CHANGETIME.Date == date.Date).CopyToDataTable<HemoModel.MED_HEMO_CHAGEWORKRow>(dtMaster,LoadOption.PreserveChanges);
            workExtend.Where(i=>i.CHANGETIME.Date == date.Date).CopyToDataTable<HemoModel.MED_HEMO_CHAGEWORK_EXTENDRow>(dtDetail,LoadOption.PreserveChanges);
            NurseChangeWorkReport report = new NurseChangeWorkReport(dtMaster, dtDetail);
            this.printControl1.PrintingSystem = report.PrintingSystem;
            report.CreateDocument();
        }

        #endregion

        #region 事件

        private void NurseChangeWorkReport_Load(object sender, EventArgs e)
        {
            this.InitReport();
        }

        void barEditReportDate_EditValueChanged(object sender, EventArgs e)
        {
            this.InitReport();
        }

        #endregion

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barEditItem_BanChi_EditValueChanged(object sender, EventArgs e)
        {
            this.InitReport();
        }
    }
}