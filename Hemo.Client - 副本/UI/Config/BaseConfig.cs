/*----------------------------------------------------------------
// Copyright (C) 2005 苏州XX公司有限公司
// 描述：程序基础配置用户控件
// 创建时间：2014-03-06
// 创建者：刘超
//  
// 修改时间：
// 修改人：
// 修改描述：
----------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Hemo.Client.UI.Config;

namespace Hemo.Client.UI.Config
{
    public partial class BaseConfig : DevExpress.XtraEditors.XtraUserControl
    {
        #region 构造函数

        public BaseConfig()
        {
            InitializeComponent();
            #region tabcontrolAddUI

            CtlConfigList ctlConfig = null;
            ctlConfig = new CtlConfigList("血管通路");
            xtraTabPage1.Controls.Add(ctlConfig);
            ctlConfig.Dock = DockStyle.Fill;

            ctlConfig = new CtlConfigList("净化器类型");
            xtraTabPage2.Controls.Add(ctlConfig);
            ctlConfig.Dock = DockStyle.Fill;

            ctlConfig = new CtlConfigList("净化方式");
            xtraTabPage3.Controls.Add(ctlConfig);
            ctlConfig.Dock = DockStyle.Fill;

            ctlConfig = new CtlConfigList("透析液种类");
            xtraTabPage4.Controls.Add(ctlConfig);
            ctlConfig.Dock = DockStyle.Fill;

            ctlConfig = new CtlConfigList("透析液品牌");
            xtraTabPage27.Controls.Add(ctlConfig);
            ctlConfig.Dock = DockStyle.Fill;

            ctlConfig = new CtlConfigList("临时医嘱");
            xtraTabPage5.Controls.Add(ctlConfig);
            ctlConfig.Dock = DockStyle.Fill;

            ctlConfig = new CtlConfigList("通路材质");
            xtraTabPage6.Controls.Add(ctlConfig);
            ctlConfig.Dock = DockStyle.Fill;

            ctlConfig = new CtlConfigList("侧位");
            xtraTabPage7.Controls.Add(ctlConfig);
            ctlConfig.Dock = DockStyle.Fill;

            ctlConfig = new CtlConfigList("通路部位");
            xtraTabPage8.Controls.Add(ctlConfig);
            ctlConfig.Dock = DockStyle.Fill;

            ctlConfig = new CtlConfigList("血管");
            xtraTabPage9.Controls.Add(ctlConfig);
            ctlConfig.Dock = DockStyle.Fill;

            ctlConfig = new CtlConfigList("术式");
            xtraTabPage10.Controls.Add(ctlConfig);
            ctlConfig.Dock = DockStyle.Fill;

            ctlConfig = new CtlConfigList("通路分类");
            xtraTabPage11.Controls.Add(ctlConfig);
            ctlConfig.Dock = DockStyle.Fill;

            ctlConfig = new CtlConfigList("治疗方法");
            xtraTabPage12.Controls.Add(ctlConfig);
            ctlConfig.Dock = DockStyle.Fill;

            ctlConfig = new CtlConfigList("注射方式");
            xtraTabPage13.Controls.Add(ctlConfig);
            ctlConfig.Dock = DockStyle.Fill;

            ctlConfig = new CtlConfigList("透析膜");
            xtraTabPage14.Controls.Add(ctlConfig);
            ctlConfig.Dock = DockStyle.Fill;

            ctlConfig = new CtlConfigList("肝素种类");
            xtraTabPage15.Controls.Add(ctlConfig);
            ctlConfig.Dock = DockStyle.Fill;

            ctlConfig = new CtlConfigList("A液类型");
            xtraTabPage16.Controls.Add(ctlConfig);
            ctlConfig.Dock = DockStyle.Fill;

            ctlConfig = new CtlConfigList("B液类型");
            xtraTabPage17.Controls.Add(ctlConfig);
            ctlConfig.Dock = DockStyle.Fill;

            ctlConfig = new CtlConfigList("促红素设定");
            xtraTabPage18.Controls.Add(ctlConfig);
            ctlConfig.Dock = DockStyle.Fill;

            ctlConfig = new CtlConfigList("检查值");
            xtraTabPage19.Controls.Add(ctlConfig);
            ctlConfig.Dock = DockStyle.Fill;

            ctlConfig = new CtlConfigList("护士转抄医嘱");
            xtraTabPage20.Controls.Add(ctlConfig);
            ctlConfig.Dock = DockStyle.Fill;

            ctlConfig = new CtlConfigList("透析小结模版");
            xtraTabPage21.Controls.Add(ctlConfig);
            ctlConfig.Dock = DockStyle.Fill;

            ctlConfig = new CtlConfigList("血型");
            xtraTabPage22.Controls.Add(ctlConfig);
            ctlConfig.Dock = DockStyle.Fill;

            ctlConfig = new CtlConfigList("检验过滤项目");
            xtraTabPage23.Controls.Add(ctlConfig);
            ctlConfig.Dock = DockStyle.Fill;

            ctlConfig = new CtlConfigList("并发症知识库");
            xtraTabPage24.Controls.Add(ctlConfig);
            ctlConfig.Dock = DockStyle.Fill;

            ctlConfig = new CtlConfigList("通路状态");
            xtraTabPage25.Controls.Add(ctlConfig);
            ctlConfig.Dock = DockStyle.Fill;


            ctlConfig = new CtlConfigList("化验检查项目");
            xtraTabPage26.Controls.Add(ctlConfig);
            ctlConfig.Dock = DockStyle.Fill;


            #endregion
            this.xtraTabControl1.Focus();
            this.xtraTabControl1.MouseWheel += new MouseEventHandler(xtraTabControl1_MouseWheel);

        }

        #endregion

        #region 事件

        /// <summary>
        /// 鼠标滚动时实现 切换tab页
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void xtraTabControl1_MouseWheel(object sender, MouseEventArgs e)
        {
            int tabpageCount = xtraTabControl1.TabPages.Count;
            int newPageIndex = (xtraTabControl1.SelectedTabPageIndex - Math.Sign(e.Delta));
            if (newPageIndex <= 0)
            {
                xtraTabControl1.SelectedTabPageIndex = 0;
            }
            else if (newPageIndex >= tabpageCount)
            {
                xtraTabControl1.SelectedTabPageIndex = tabpageCount;
            }
            else
            {
                xtraTabControl1.SelectedTabPageIndex = newPageIndex;
            }
        }

        #endregion
    }
}
