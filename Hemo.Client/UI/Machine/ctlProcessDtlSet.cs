/*----------------------------------------------------------------
// Copyright (C) 2005 苏州XX公司有限公司
// 描述：流程控制户控件类
// 创建时间：2016-04-21
// 创建者：贺建操
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
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Hemo.IService.Machine;
using Hemo.Service;
using Hemo.Model;

namespace Hemo.Client.UI.Machine
{
    public partial class ctlProcessDtlSet : DevExpress.XtraEditors.XtraUserControl
    {
        #region 变量
        IMachine _imachine = ServiceManager.Instance.MachineService;
        private MachineModel.MED_PROCESS_SETDataTable dtMPS;
        #endregion

        #region 构造函数

        public ctlProcessDtlSet()
        {
            InitializeComponent();
        }

        #endregion

        #region 事件

        private void ctlProcessDtlSet_Load(object sender, EventArgs e)
        {
            GetList();
        }

        private void btnQuery_Click(object sender, EventArgs e)
        {
            GetList();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            using (EditProcess frm = new EditProcess(null, this.dtMPS))
            {
                frm.ShowDialog();
                if (frm.DialogResult == DialogResult.OK)
                {
                    GetList();
                }
            };
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            var row = this.gridView1.GetFocusedDataRow();
            if (row != null)
            {
                using (EditProcess frm = new EditProcess((MachineModel.MED_PROCESS_SETRow)row, this.dtMPS))
                {
                    frm.ShowDialog();
                    if (frm.DialogResult == DialogResult.OK)
                    {
                        GetList();
                    }
                }
            }


        }

        private void gridView1_DoubleClick(object sender, EventArgs e)
        {
            var row = this.gridView1.GetFocusedDataRow();
            if (row != null)
            {

                using (EditProcess frm = new EditProcess((MachineModel.MED_PROCESS_SETRow)row, this.dtMPS))
                {
                    frm.ShowDialog();
                    if (frm.DialogResult == DialogResult.OK)
                    {
                        GetList();
                    }
                }
            }
        }

        private void chkFilter_CheckedChanged(object sender, EventArgs e)
        {
            this.gridView1.OptionsView.ShowAutoFilterRow = this.chkFilter.Checked;
        }

        private void dxSimpleButton1_Click(object sender, EventArgs e)
        {
            var row = this.gridView1.GetFocusedDataRow();
            if (row != null)
            {
                var result = _imachine.DeleteProcessSetDataById(row["ID"].ToString());
                if (result > 0)
                {
                    MessageBox.Show("删除成功");
                    GetList();
                }
            }
        }

        #endregion

        #region 方法

        private void GetList()
        {
            dtMPS = _imachine.GetProcessSetDataList();
            this.gridControl1.DataSource = dtMPS;
        }

        #endregion
    }
}
