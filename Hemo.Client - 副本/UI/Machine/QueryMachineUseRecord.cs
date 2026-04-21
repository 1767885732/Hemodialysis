/*----------------------------------------------------------------
// Copyright (C) 2005 苏州XX公司有限公司
// 描述：血透机使用情况记录窗体
// 创建时间：2016-04-15
// 创建者：吕志强
//  
// 修改时间：
// 修改人：
// 修改描述：
----------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace Hemo.Client.UI.Machine
{
    public partial class QueryMachineUseRecord : HemoBaseFrm
    {
        #region 成员变量

        private bool isMachineUseRecord = true;

        private string machineId;

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

        #endregion

        #region 构造函数

        public QueryMachineUseRecord()
        {
            InitializeComponent();
        }

        #endregion

        #region 事件

        /// <summary>
        /// 加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void QueryMachineUseRecord_Load(object sender, EventArgs e)
        {
            this.Text = isMachineUseRecord ? "血透机使用情况记录" : "水处理机使用情况记录";

            if (isMachineUseRecord)
            {
                MachineUseRecordNew useRecord = new MachineUseRecordNew();
                //useRecord.MachineId = machineId;
                useRecord.Dock = DockStyle.Fill;
                this.pnlContainer.Controls.Clear();
                this.pnlContainer.Controls.Add(useRecord);
            }
            else
            {
                WaterProcessorUseRecord useRecord = new WaterProcessorUseRecord();
                //useRecord.MachineId = machineId;
                useRecord.Dock = DockStyle.Fill;
                this.pnlContainer.Controls.Clear();
                this.pnlContainer.Controls.Add(useRecord);
            }
        }

        #endregion
    }
}