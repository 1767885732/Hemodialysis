/*----------------------------------------------------------------
// Copyright (C) 2005 苏州XX公司有限公司
// 描述：血透机列表窗体
// 创建时间：2016-07-05
// 创建者：吕志强
//  
// 修改时间：
// 修改人：
// 修改描述：
----------------------------------------------------------------*/

using System;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Grid;
using Hemo.IService.Machine;
using Hemo.Model;
using Hemo.Service;
using System.Data;

namespace Hemo.Client.UI.Machine {
    public partial class MachineList : HemoBaseFrm {
        #region 变量

        private string machineType = "血透机品牌";

        private bool isMachineUseRecord = true;

        private string areaId = string.Empty;

        #endregion

        #region 属性

        /// <summary>
        /// 是否血透机使用记录
        /// </summary>
        public bool IsMachineUseRecord {
            get { return isMachineUseRecord; }
            set { isMachineUseRecord = value; }
        }

        public string AreaId
        {
            get { return areaId; }
            set { areaId = value; }
        }

        #endregion

        #region 构造函数

        public MachineList() {
            this.InitializeComponent();
        }

        #endregion

        #region 事件

        /// <summary>
        /// 加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MachineList_Load(object sender, EventArgs e) {
            this.Text = isMachineUseRecord ? "血透机运行记录" : "水处理机运行记录";
            machineType = isMachineUseRecord ? "血透机品牌" : "水处理机品牌";
            ShowUseRecord();
        }

        #endregion

        #region 方法

        /// <summary>
        /// 显示使用记录
        /// </summary>
        private void ShowUseRecord() {
            if (isMachineUseRecord) {
                MachineUseRecordNew useRecord = new MachineUseRecordNew();;
                useRecord.AreaId = this.areaId;
                useRecord.Dock = DockStyle.Fill;
                this.pnlRecordList.Controls.Clear();
                this.pnlRecordList.Controls.Add(useRecord);
            }
            else {
                WaterProcessorUseRecord useRecord = new WaterProcessorUseRecord();
                useRecord.Dock = DockStyle.Fill;
                this.pnlRecordList.Controls.Clear();
                this.pnlRecordList.Controls.Add(useRecord);
            }
        }

        #endregion
    }
}
