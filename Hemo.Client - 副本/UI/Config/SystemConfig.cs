/*----------------------------------------------------------------
// Copyright (C) 2005 苏州XX公司有限公司
// 描述：系统配置用户控件
// 创建时间：2014-03-06
// 创建者：刘超
//  
// 修改时间：
// 修改人：
// 修改描述：
----------------------------------------------------------------*/

using System.Windows.Forms;
using Hemo.Client.UI.Machine;
using Hemo.Client.UI.User;
using System;

namespace Hemo.Client.UI.Config
{
    public partial class SystemConfig : DevExpress.XtraEditors.XtraUserControl
    {
        #region 构造函数

        public SystemConfig()
        {
            InitializeComponent();

            #region tablControlAddUI

            CtlConfigList ctlConfig = null;
            ctlConfig = new CtlConfigList("血透机品牌");
            xtraTabPage1.Controls.Add(ctlConfig);
            ctlConfig.Dock = DockStyle.Fill;

            ctlConfig = new CtlConfigList("透析机");
            this.xtraTabPage2.Controls.Add(ctlConfig);
            ctlConfig.Dock = DockStyle.Fill;

            ctlConfig = new CtlConfigList("床位");
            xtraTabPage3.Controls.Add(ctlConfig);
            ctlConfig.Dock = DockStyle.Fill;

            ctlConfig = new CtlConfigList("区域");
            xtraTabPage4.Controls.Add(ctlConfig);
            ctlConfig.Dock = DockStyle.Fill;

            //ctlConfig = new CtlConfigList("记账项目维护");
            //this.xtraTabPage18.Controls.Add(ctlConfig);
            //ctlConfig.Dock = DockStyle.Fill;

            ctlConfig = new CtlConfigList("隔离病区");
            this.xtraTabPage5.Controls.Add(ctlConfig);
            ctlConfig.Dock = DockStyle.Fill;

            CtlMachineList ctl = new CtlMachineList();
            xtraTabPage6.Controls.Add(ctl);
            ctl.Dock = DockStyle.Fill;

            SetUserAreaMappingFrm setUserAreaMappingFrm = new SetUserAreaMappingFrm();
            xtraTabPage7.Controls.Add(setUserAreaMappingFrm);
            setUserAreaMappingFrm.Dock = DockStyle.Fill;

            ctlConfig = new CtlConfigList("药品分类");
            xtraTabPage8.Controls.Add(ctlConfig);
            ctlConfig.Dock = DockStyle.Fill;


            ctlConfig = new CtlConfigList("药品单位");
            xtraTabPage9.Controls.Add(ctlConfig);
            ctlConfig.Dock = DockStyle.Fill;

            ctlConfig = new CtlConfigList("辅材类型");
            xtraTabPage10.Controls.Add(ctlConfig);
            ctlConfig.Dock = DockStyle.Fill;

            ctlConfig = new CtlConfigList("耗材单位");
            xtraTabPage11.Controls.Add(ctlConfig);
            ctlConfig.Dock = DockStyle.Fill;


            ctlConfig = new CtlConfigList("职业");
            xtraTabPage12.Controls.Add(ctlConfig);
            ctlConfig.Dock = DockStyle.Fill;

            ctlConfig = new CtlConfigList("职称");
            xtraTabPage13.Controls.Add(ctlConfig);
            ctlConfig.Dock = DockStyle.Fill;

            ctlConfig = new CtlConfigList("民族");
            this.xtraTabPage14.Controls.Add(ctlConfig);
            ctlConfig.Dock = DockStyle.Fill;

            ctlConfig = new CtlConfigList("提醒参数设定");
            this.xtraTabPage15.Controls.Add(ctlConfig);
            ctlConfig.Dock = DockStyle.Fill;

            ctlConfig = new CtlConfigList("统计类型");
            this.xtraTabPage16.Controls.Add(ctlConfig);
            ctlConfig.Dock = DockStyle.Fill;

            AccountItem accountCtl = new AccountItem();
            xtraTabPage17.Controls.Add(accountCtl);
            accountCtl.Dock = DockStyle.Fill;

            CtlWaterProcessorList ctlWaterProcessor = new CtlWaterProcessorList();
            this.xtraTabPage18.Controls.Add(ctlWaterProcessor);
            ctlWaterProcessor.Dock = DockStyle.Fill;

            ctlConfig = new CtlConfigList("水处理机品牌");
            xtraTabPage19.Controls.Add(ctlConfig);
            ctlConfig.Dock = DockStyle.Fill;
            ctlConfig = new CtlConfigList("质控校验");
            xtraTabPage20.Controls.Add(ctlConfig);
            ctlConfig.Dock = DockStyle.Fill;

            ctlConfig = new CtlConfigList("假期类型");
            xtraTabPage21.Controls.Add(ctlConfig);
            ctlConfig.Dock = DockStyle.Fill;


            ScreenItemConfig screenCtl = new ScreenItemConfig();
            xtraTabPage22.Controls.Add(screenCtl);
            screenCtl.Dock = DockStyle.Fill;

            ctlConfig = new CtlConfigList("请假离院原因");
            xtraTabPage23.Controls.Add(ctlConfig);
            ctlConfig.Dock = DockStyle.Fill;

            CtlDataGatherSet ctlDataGatherSet = new CtlDataGatherSet();
            this.xtraTabPage24.Controls.Add(ctlDataGatherSet);
            ctlDataGatherSet.Dock = DockStyle.Fill;

            ctlConfig = new CtlConfigList("平台用户信息");
            xtraTabPage25.Controls.Add(ctlConfig);
            ctlConfig.Dock = DockStyle.Fill;

            ctlConfig = new CtlConfigList("流程节点");
            xtraTabPage26.Controls.Add(ctlConfig);
            ctlConfig.Dock = DockStyle.Fill;

            ctlConfig = new CtlConfigList("接口");
            xtraTabPage28.Controls.Add(ctlConfig);
            ctlConfig.Dock = DockStyle.Fill;

            ctlConfig = new CtlConfigList("检验统计");
            xtraTabPage29.Controls.Add(ctlConfig);
            ctlConfig.Dock = DockStyle.Fill;

            ctlConfig = new CtlConfigList("质控指标检验项目");
            xtraTabPage30.Controls.Add(ctlConfig);
            ctlConfig.Dock = DockStyle.Fill;

            ctlConfig = new CtlConfigList("质控指标查询条件");
            xtraTabPage31.Controls.Add(ctlConfig);
            ctlConfig.Dock = DockStyle.Fill;

            CtlConfigListGroup ctlConfigGroup = null;
            ctlConfigGroup = new CtlConfigListGroup("检验项目明细配置");
            xtraTabPage32.Controls.Add(ctlConfigGroup);
            ctlConfigGroup.Dock = DockStyle.Fill;

            CtlConfigListGroup ctlConfigGroup2 = null;
            ctlConfigGroup2 = new CtlConfigListGroup("检验项目配置");
            xtraTabPage34.Controls.Add(ctlConfigGroup2);
            ctlConfigGroup2.Dock = DockStyle.Fill;

            ctlProcessDtlSet ctlSet = new ctlProcessDtlSet();
            xtraTabPage27.Controls.Add(ctlSet);
            ctlSet.Dock = DockStyle.Fill;

            CtlConfigSet ctlFSet = new CtlConfigSet();
            xtraTabPage33.Controls.Add(ctlFSet);
            ctlFSet.Dock = DockStyle.Fill;

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

        private void xtraTabPage33_Click(object sender, EventArgs e) {

        }
    }
}
