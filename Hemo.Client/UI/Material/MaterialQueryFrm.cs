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
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Hemo.Client.UI.Material
{
    public partial class MaterialQueryFrm : HemoBaseFrm
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public MaterialQueryFrm()
        {
            InitializeComponent();
        }

        #region 事件 
        private void MaterialQueryFrm_Load(object sender, EventArgs e)
        {
            this.materialPatientQueryUI1.btnQuery_Click(sender, e);
        }

        private void xtraTabControl1_SelectedPageChanged(object sender, DevExpress.XtraTab.TabPageChangedEventArgs e)
        {
            if (this.xtraTabControl1.SelectedTabPage == this.xtraTabPage1)
            {
                this.materialPatientQueryUI1.btnQuery_Click(sender, e);

            }
            else
            {
                this.patientMaterialQueryUI1.btnQuery_Click(sender, e);


            }
        }
        #endregion
    }
}
