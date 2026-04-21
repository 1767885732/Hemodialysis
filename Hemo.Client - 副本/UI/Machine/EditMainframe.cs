/*----------------------------------------------------------------
// Copyright (C) 2005 苏州XX公司有限公司
// 描述：医疗设备主机维护窗体
// 创建时间：2015-03-05
// 创建者：刘超
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
using Hemo.Model;
using Hemo.IService.Machine;
using Hemo.Service;
using Hemo.IService.Dict;
using Hemo.Utilities;
using Hemo.Client.Core;

namespace Hemo.Client.UI.Machine
{
    public partial class EditMainframe : PureTextEditForm
    {
        #region 字段
        /// <summary>
        /// 员工相关服务
        /// </summary>
        private IStaffDict _staffDictService = ServiceManager.Instance.StaffDictService;

        /// <summary>
        /// 血透机服务
        /// </summary>
        private IMachine _machineService = ServiceManager.Instance.MachineService;

        /// <summary>
        /// 当前主机表
        /// </summary>
        private MachineModel.MED_MACHINE_MAINFRAMEDataTable _currentMainFrameData;

        /// <summary>
        /// 当前主机
        /// </summary>
        private MachineModel.MED_MACHINE_MAINFRAMERow _currentMainFrame;
        #endregion

        #region 构造函数
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="currentMainFrameData"></param>
        /// <param name="currentMainFrame"></param>
        public EditMainframe(MachineModel.MED_MACHINE_MAINFRAMEDataTable currentMainFrameData, MachineModel.MED_MACHINE_MAINFRAMERow currentMainFrame)
        {
            InitializeComponent();

            this._currentMainFrameData = currentMainFrameData;
            this._currentMainFrame = currentMainFrame;
        }
        #endregion

        #region 事件
        /// <summary>
        /// 窗体加载事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EditMainframe_Load(object sender, EventArgs e)
        {
            //自动选择当前医生
            DataTable dtStaffSict = this._staffDictService.GetStaffDictList();
            if (dtStaffSict != null && dtStaffSict.Rows.Count > 0)
            {
                DataTable dtPunctureDoctorList = Utility.GetSubTable(dtStaffSict, "ZYNAME='医生'");
                if (dtPunctureDoctorList != null && dtPunctureDoctorList.Rows.Count > 0)
                {
                    BaseControlInfo.BindLookUpEdit(this.txt_Submitter, "EMP_NO", "NAME", dtPunctureDoctorList, "NAME", "提交人");
                    BaseControlInfo.BindLookUpEdit(this.txt_Receiver, "EMP_NO", "NAME", dtPunctureDoctorList, "NAME", "接收人");
                }
            }
            this.txt_Submitter.EditValue = HemoApplicationContext.Current.CurrentUser.EMP_NO;
            this.FillUiDataByDataRow(this._currentMainFrame);
        }

        /// <summary>
        /// 查看附属设备信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_AccessoryEquip_Click(object sender, EventArgs e)
        {
            if (this._currentMainFrame == null)
            {
                XtraMessageBox.Show("请先录入主机设备并保存！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var frm = new QueryAccessoryEquip(this._currentMainFrame);
            frm.ShowDialog();
        }

        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_Save_Click(object sender, EventArgs e)
        {
            if (this._currentMainFrameData == null)
            {
                this._currentMainFrameData = new MachineModel.MED_MACHINE_MAINFRAMEDataTable();
            }

            if (!this.CheckInputData())
            {
                return;
            }

            if (this._currentMainFrame == null)
            {
                this._currentMainFrame = this._currentMainFrameData.NewMED_MACHINE_MAINFRAMERow();
                this.FillDataRowByUi(this._currentMainFrame);
                this._currentMainFrame.ID = Guid.NewGuid().ToString();
                this._currentMainFrame.SUBMITDATE = DateTime.Now;
                this._currentMainFrameData.AddMED_MACHINE_MAINFRAMERow(this._currentMainFrame);
            }
            else
            {
                this.FillDataRowByUi(this._currentMainFrame);
            }

            try
            {
                this._currentMainFrame.ISDELETE = "0";
                this._machineService.SaveMainframeData(this._currentMainFrameData);
            }
            catch (Exception ex)
            {
                //日志
                XtraMessageBox.Show("保存失败！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            this.DialogResult = System.Windows.Forms.DialogResult.OK;
        }

        /// <summary>
        /// 点击取消按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_Cancle_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.No;
        }
        #endregion

        #region 方法
        /// <summary>
        /// 验证输入数据的合法性
        /// </summary>
        /// <returns></returns>
        private bool CheckInputData()
        {
            decimal d;
            if (!string.IsNullOrEmpty(this.txt_AgeLimit.Text.Trim()) && !decimal.TryParse(this.txt_AgeLimit.Text.Trim(), out d))
            {
                XtraMessageBox.Show("'使用年限'请填入正确的数字！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }
            if (!string.IsNullOrEmpty(this.txt_UseQuota.Text.Trim()) && !decimal.TryParse(this.txt_UseQuota.Text.Trim(), out d))
            {
                XtraMessageBox.Show("'使用定额'请填入正确的数字！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }
            if (!string.IsNullOrEmpty(this.txt_PriceByRMB.Text.Trim()) && !decimal.TryParse(this.txt_PriceByRMB.Text.Trim(), out d))
            {
                XtraMessageBox.Show("'人民币'请填入正确的数字！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }
            if (!string.IsNullOrEmpty(this.txt_PriceByDollor.Text.Trim()) && !decimal.TryParse(this.txt_PriceByDollor.Text.Trim(), out d))
            {
                XtraMessageBox.Show("'美元'请填入正确的数字！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }
            if (!string.IsNullOrEmpty(this.txt_Appropriation.Text.Trim()) && !decimal.TryParse(this.txt_Appropriation.Text.Trim(), out d))
            {
                XtraMessageBox.Show("'上级拨款'请填入正确的数字！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }
            if (!string.IsNullOrEmpty(this.txt_SelfFinancing.Text.Trim()) && !decimal.TryParse(this.txt_SelfFinancing.Text.Trim(), out d))
            {
                XtraMessageBox.Show("'自筹金额'请填入正确的数字！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }

            return true;
        }
        #endregion
    }
}