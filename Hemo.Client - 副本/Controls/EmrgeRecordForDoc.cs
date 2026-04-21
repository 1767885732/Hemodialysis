/*----------------------------------------------------------------
// Copyright (C) 2013 苏州XX公司
// 描述：抢救病历
// 创建时间：2015-08-21
// 创建者：吕志强
//  
// 修改时间：
// 修改人：
// 修改描述：
//
----------------------------------------------------------------*/
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

namespace Hemo.Client.Controls
{
    public partial class EmrgeRecordForDoc : DevExpress.XtraEditors.XtraForm
    {
        #region 变量
        public string patientHemoId { get; set; }

        public HemodialysisModel.MED_CURE_MAINRow _cureRow { get; set; }

        private IHemodialysis _hemodialysisService = ServiceManager.Instance.HemodialysisService;


        #endregion

        public EmrgeRecordForDoc()
        {
            InitializeComponent();
        }

        #region 事件
        /// <summary>
        /// 加载 时
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
            var data = new HemodialysisModel.MED_CURE_MAINDataTable();
            _cureRow.SUMMARY3 = this.txtRecord.Text.Trim();
            //_cureRow.RowState = DataRowState.Modified;
            data.ImportRow(_cureRow);
            var t = _hemodialysisService.SaveCureMain(data);
            if (t > 0) { MessageBox.Show("保存成功！"); }
            else
            { MessageBox.Show("保存失败！"); }
            this.Close();

        }
        /// <summary>
        /// 加载 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EmrgeRecordForDoc_Load(object sender, EventArgs e)
        {
            if (_cureRow != null && !_cureRow.IsSUMMARY3Null())
                this.txtRecord.Text = _cureRow.SUMMARY3.ToString();

        }
        /// <summary>
        /// 关闭
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        #endregion
    }
}
