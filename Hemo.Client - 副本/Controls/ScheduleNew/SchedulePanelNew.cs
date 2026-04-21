/*----------------------------------------------------------------
 * Copyright (C) 2005 医疗科技发展有限公司
 * 文件功能描述:排班控件
 * 创建标识:贺建操-2014年8月2日
 * ----------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Hemo.Client.Core;
using Hemo.Model;

namespace Hemo.Client.Controls.Schedule
{
    public partial class SchedulePanelNew : UserControl
    {
        #region 变量

        public int _dayOfWeek { get; set; }
        public string _banChi { get; set; }
        public Dictionary<ConfigModel.MED_COMMON_ITEMLISTRow, int> _areaDict { get; set; }
        public ConfigModel.MED_COMMON_ITEMLISTDataTable _bedDataTable { get; set; }
        public MachineModel.MED_DIALYSIS_MACHINEDataTable _machineDataTable { get; set; }

        #endregion

        #region 构造函数

        public SchedulePanelNew()
        {
            this.InitializeComponent();
        }

        #endregion

        #region 方法

        private void InitSchedulePanelNew()
        {
            //病区        
            foreach (KeyValuePair<ConfigModel.MED_COMMON_ITEMLISTRow, int> areaItem in this._areaDict)
            {
                Panel panel = new Panel();
                panel.BorderStyle = BorderStyle.FixedSingle;
                panel.Dock = DockStyle.Left;
                panel.Width = areaItem.Value + 10;             

                var pal = new FlowLayoutPanel();
                pal.Dock = DockStyle.Fill;
                pal.BorderStyle = System.Windows.Forms.BorderStyle.None;
                panel.Controls.Add(pal);
                pal.AutoScroll = true;
                pal.Click += new EventHandler(pal_Click);
                //床位
                foreach (var bedRow in this._bedDataTable)
                {
                    //血透机
                    MachineModel.MED_DIALYSIS_MACHINERow[] machineRows = this._machineDataTable.Select(string.Format("AREA_ID = '{0}' AND BED_ID = '{1}'", areaItem.Key.ITEM_ID, bedRow.ITEM_ID)) as MachineModel.MED_DIALYSIS_MACHINERow[];

                    if (machineRows.Length == 0)
                        continue;

                    CtlSchedulePersonNew ctlSchedulePerson = new CtlSchedulePersonNew();
                    ctlSchedulePerson.Size = new System.Drawing.Size(80, 55);
                    ctlSchedulePerson.MouseDown += new MouseEventHandler(ctlSchedulePerson_MouseDown);
                    pal.Controls.Add(ctlSchedulePerson);                    
                }
                this.Controls.Add(panel);
            }
        }

        void ctlSchedulePerson_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                if (sender is CtlSchedulePersonNew)
                {
                    var personControl = sender as CtlSchedulePersonNew;
                    personControl.contextMenuStrip1.Visible = true;
                    personControl.ContextMenuStrip = personControl.contextMenuStrip1;
                    
                    
                }
            }
        }

        void pal_Click(object sender, EventArgs e)
        {
            var obj = sender as FlowLayoutPanel;
            MessageBox.Show(string.Format("bchi{2}高:{0}宽:{1}", obj.Height.ToString(), obj.Width.ToString(), _banChi.ToString()));
        }


        #endregion

        #region 事件

        private void SchedulePanelNew_Load(object sender, EventArgs e)
        {
            this.InitSchedulePanelNew();
        }

        #endregion
    }
}
