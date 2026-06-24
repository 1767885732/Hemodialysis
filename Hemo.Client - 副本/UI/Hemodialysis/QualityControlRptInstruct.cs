/*----------------------------------------------------------------
 * Copyright (C) 2005 (苏州)医疗科技发展有限公司
 * 文件功能描述:用户控件
 * 创建标识:贺建操-2013年7月13日
 * 
 * 修改时间:2013年10月21日
 * 修改人:吕志强
 * 修改描述:新增方法
 * 
 * 修改时间:2014年1月29日
 * 修改人:吕志强
 * 修改描述:新增方法SQL
 * 
 * 修改时间:2014年5月9日
 * 修改人:顾伟伟
 * 修改描述:新增方法
 * ----------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace Hemo.Client.UI.Hemodialysis
{
    public partial class QualityControlRptInstruct : DevExpress.XtraEditors.XtraUserControl
    {
        /// <summary>
        /// 变量
        /// </summary>
        private static string _formtype;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="formtype"></param>
        public QualityControlRptInstruct(string formtype)
        {
            InitializeComponent();
            _formtype = formtype;
        }
        #region 事件
        private void QualityControlRptInstruct_Load(object sender, EventArgs e)
        {
            var str = string.Empty;

            switch (_formtype)
            {
                case "质控平台":
                    str = "1.报表说明：该报表可按年份查询、统计福建省医疗机构开展血液透析的基本情况" + "\r\n";
                    str += "2.报表分为6部分：科室信息，检验信息，院感信息，设备信息，治疗信息,汇总信息" + "\r\n";
                    str += "3.统计步骤为：必须先统计科室信息，接下来可按随机顺序统计检验信息，院感信息，设备信息，治疗信息" + "\r\n";
                    str += "4.最后的统计结果会全部展示于汇总信息，用户可修改内容和比例，名称和是否已上传不可修改。" + "\r\n";
                    str += "5.全部数据可导出至EXCEL" + "\r\n";
                    break;
                case "质量管理":
                    this.labelControl2.Text = "报表名称：质量管理基础数据";
                    str = "1.报表提供多种条件查询：用户可按日期范围、年份、季度、月份查询。" + "\r\n";
                    str += "2.严重并发症：取并发症里严重危及患者生命为是的数据。" + "\r\n";
                    str += "3.报表支持Excel、图表查看。";
                    break;
                case "监测指标":
                    this.labelControl2.Text = "报表名称：监测指标";
                    str = "1.报表提供多种条件查询：用户可按日期范围、年份、季度、月份查询。" + "\r\n";
                    str += "2.患者类型为维持性透析患者。" + "\r\n";
                    str += "3.报表支持Excel、图表查看。";
                    break;   
                case "质控查询":
                    this.labelControl2.Text = "报表名称：质控指标查询";
                    str = "报表支持动态条件查询。" + "\r\n";
                    break;
                default:
                    break;
            }





            this.memoEdit1.Text = str;
        }
        #endregion
    }
}
