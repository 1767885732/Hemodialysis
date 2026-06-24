/*----------------------------------------------------------------
 * Copyright (C) 2005 (苏州)医疗科技发展有限公司
 * 文件功能描述:血透室每日交班报表
 * 创建标识:吕志强-2016年5月8日
 * ----------------------------------------------------------------*/

using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Linq;
using Hemo.Model;

namespace Hemo.Client.Print
{
    public partial class NurseChangeWorkReport : DevExpress.XtraReports.UI.XtraReport
    {
        #region 构造函数

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_workMaster"></param>
        /// <param name="_workExtend"></param>
        public NurseChangeWorkReport(HemoModel.MED_HEMO_CHAGEWORKDataTable _workMaster,HemoModel.MED_HEMO_CHAGEWORK_EXTENDDataTable _workExtend)
        {
            InitializeComponent();

            foreach (HemoModel.MED_HEMO_CHAGEWORK_EXTENDRow dr in _workExtend.Rows)
            {
                this.hemoModel1.MED_HEMO_CHAGEWORK_EXTEND.Rows.Add(dr.ItemArray);
            }
            //给有关系的主从表进行赋值
            foreach (HemoModel.MED_HEMO_CHAGEWORKRow dr in _workMaster.Rows)
            {
                this.hemoModel1.MED_HEMO_CHAGEWORK.Rows.Add(dr.ItemArray);
            }
            this.DataSource = hemoModel1;
            this.DetailReport.DataSource = hemoModel1;
            
            //this.DataMember = "";
            //this.DetailReport.DataMember = "";
        }

        #endregion
    }
}
