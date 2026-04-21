/*----------------------------------------------------------------
 * Copyright (C) 2005 (苏州)医疗科技发展有限公司
 * 文件功能描述:护士抢救记录编辑类
 * 创建标识:刘超-2017年3月5日
 * ----------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Hemo.Client.UI.Hemodialysis;
using Hemo.Model;
using Hemo.IService.Config;
using Hemo.Service;
using Hemo.Client.UI.Machine;

namespace Hemo.Client.Controls {
    public partial class EmrgeRecordForDocUI : ViewBase
    {
        #region 类变量

        private IHemodialysis _hemodialysisService = ServiceManager.Instance.HemodialysisService;

        #endregion

        #region 属性

        public string patientHemoId { get; set; }

        public HemodialysisModel.MED_CURE_MAINRow _cureRow { get; set; }

        #endregion

        #region 构造函数

        public EmrgeRecordForDocUI()
        {
            InitializeComponent();
        }

        #endregion

        #region 事件

        /// <summary>
        /// 抢救记录
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_NurseRecord_Click(object sender, EventArgs e)
        {
            ShowRescueRecord frm = null;
            if (_cureRow != null)
            {
                string emrgeRecordForNurse = _cureRow.IsSUMMARY2Null() ? string.Empty : _cureRow.SUMMARY2;
                frm = new ShowRescueRecord(emrgeRecordForNurse);
            }
            else
            {
                frm = new ShowRescueRecord("无记录！");
            }
            frm.ShowDialog();
        }

        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_save_Click(object sender, EventArgs e)
        {
            if (_cureRow == null)
            {
                MessageBox.Show("患者没有开始治疗，不可以录入抢救记录！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var data = new HemodialysisModel.MED_CURE_MAINDataTable();
            _cureRow.SUMMARY3 = this.txtRecord.Text.Trim();
            //_cureRow.RowState = DataRowState.Modified;
            data.ImportRow(_cureRow);
            var t = _hemodialysisService.SaveCureMain(data);
            if (t > 0) { MessageBox.Show("保存成功！"); }
            else { MessageBox.Show("保存失败！"); }
            // this.ParentForm.Close();
        }

        /// <summary>
        /// 加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EmrgeRecordForDoc_Load(object sender, EventArgs e)
        {
            LoadInfo();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.ParentForm.Close();
        }

        #endregion

        #region 方法

        public void LoadInfo()
        {
            if (_cureRow != null && !_cureRow.IsSUMMARY3Null())
                this.txtRecord.Text = _cureRow.SUMMARY3.ToString();
        }

        #endregion
    }
}
