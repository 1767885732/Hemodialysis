/*----------------------------------------------------------------
 * Copyright (C) 2005 医疗科技发展有限公司
 * 文件功能描述:排班容器
 * 创建标识:贺建操-2014年8月2日
 * ----------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Hemo.Client.Core;
using Hemo.Model;

namespace Hemo.Client.Controls.Schedule
{
    public partial class SchedulePanel : UserControl
    {
        #region 变量

        private int _dayOfWeek;
        private Dictionary<ConfigModel.MED_COMMON_ITEMLISTRow, int> _areaDict;
        private ConfigModel.MED_COMMON_ITEMLISTDataTable _bedDataTable;
        private MachineModel.MED_DIALYSIS_MACHINEDataTable _machineDataTable;

        #endregion

        #region 构造函数

        public SchedulePanel(int dayOfWeek, Dictionary<ConfigModel.MED_COMMON_ITEMLISTRow, int> areaDict, ConfigModel.MED_COMMON_ITEMLISTDataTable bedDataTable, MachineModel.MED_DIALYSIS_MACHINEDataTable machineDataTable)
        {
            this.InitializeComponent();

            this._dayOfWeek = dayOfWeek;
            this._areaDict = areaDict;
            this._bedDataTable = bedDataTable;
            this._machineDataTable = machineDataTable;
        }

        #endregion

        #region 方法

        private void InitSchedulePanel()
        {
            //病区        
            foreach (KeyValuePair<ConfigModel.MED_COMMON_ITEMLISTRow, int> areaItem in this._areaDict)
            {
                Panel panel = new Panel();
                panel.BorderStyle = BorderStyle.FixedSingle;
                panel.Dock = DockStyle.Top;
                panel.Height = areaItem.Value + 20;

                var pal = new FlowLayoutPanel();
                pal.Dock = DockStyle.Top;
                panel.Controls.Add(pal);
                pal.AutoScroll = true;
                pal.Height = areaItem.Value;
                //床位
                foreach (var bedRow in this._bedDataTable)
                {
                    //血透机
                    MachineModel.MED_DIALYSIS_MACHINERow[] machineRows = this._machineDataTable.Select(string.Format("AREA_ID = '{0}' AND BED_ID = '{1}'", areaItem.Key.ITEM_ID, bedRow.ITEM_ID)) as MachineModel.MED_DIALYSIS_MACHINERow[];

                    if (machineRows.Length == 0)
                        continue;

                    CtlSchedulePerson ctlSchedulePerson = new CtlSchedulePerson(this._dayOfWeek, areaItem.Key, bedRow, machineRows[0]);

                    pal.Controls.Add(ctlSchedulePerson);

                    SchedulePersonDragManager.Instance.AddSchedulePerson(this._dayOfWeek, ctlSchedulePerson);
                }
                var lbl = new DevExpress.XtraEditors.LabelControl();
                lbl.Text = "              " + string.Format("{0}【{1}】", areaItem.Key.ITEM_NAME.ToString(), this.Parent.Tag.ToString());
                lbl.Dock = DockStyle.Bottom;
                lbl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
                lbl.BackColor = System.Drawing.Color.LightSteelBlue;
                lbl.Height = 20;
                panel.Controls.Add(lbl);
                this.Controls.Add(panel);
            }
        }

        #endregion

        #region 事件

        private void SchedulePanel_Load(object sender, EventArgs e)
        {
            this.InitSchedulePanel();
        }

        #endregion
    }
}
