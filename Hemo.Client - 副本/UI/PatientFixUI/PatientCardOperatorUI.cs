/*----------------------------------------------------------------
// Copyright (C) 2013 苏州XX公司
// 描述：患者透析卡管理类
// 创建时间：2016-05-18
// 创建者：贺建操
//  
// 修改时间：2016-10-22
// 修改人：贺建操
// 修改描述：修改界面及部分业务逻辑
//
----------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Hemo.Service;
using Hemo.Model;
using Hemo.Utilities;
using DevExpress.XtraEditors;
using Hemo.Client.UI.Machine;

namespace Hemo.Client.UI.Patient
{
    /// <summary>
    /// 界面的基类
    /// </summary>
    [ToolboxItem(true)]
    public partial class PatientCardOperatorUI : ViewBase
    {
        #region 构造函数

        public PatientCardOperatorUI()
        {
            InitializeComponent();
            this.patientInfoForCard1.GetHemoEventHandler += new EventHandler(patientInfoForCard1_GetHemoEventHandler);
            this.patientInfoForCard2.GetHemoEventHandler += new EventHandler(patientInfoForCard2_GetHemoEventHandler);
        }

        #endregion

        #region 变量

        private PatientService objPatient = new PatientService();
        /// <summary>
        /// 所有制卡人员
        /// </summary>
        private DrugModel.MED_PATIENTS_CARDDataTable _patientCardDt;
        /// <summary>
        /// 所有未制卡人员
        /// </summary>
        private DrugModel.MED_PATIENTS_CARDDataTable _patientDt;

        /// <summary>
        /// 当前患者
        /// </summary>
        public string currentHemoId { get; set; }
        /// <summary>
        /// 挂失的患者
        /// </summary>
        private string _hemoIdCard1 = string.Empty;
        /// <summary>
        /// 补卡的患者
        /// </summary>
        private string _hemoIdCard2 = string.Empty;

        #endregion

        #region 方法

        /// <summary>
        /// 初始化数据
        /// </summary>
        public void InzationData()
        {
            using (BackgroundWorker worker = new BackgroundWorker())
            {
                worker.DoWork += delegate (object sender1, DoWorkEventArgs e1)
                {
                    _patientCardDt = objPatient.GetPatientCardDt();
                    _patientDt = objPatient.GetPatientsDt();
                };
                worker.RunWorkerCompleted += delegate (object sender2, RunWorkerCompletedEventArgs e2)
                {
                    if (!string.IsNullOrEmpty(currentHemoId))
                    {
                        this.patientInfoForCard1.InzationData(currentHemoId);
                        this.patientInfoForCard2.InzationData(currentHemoId);
                        this.patientGridCtl2.radioGroup_Filter.Visible = false;
                        this.patientGridCtl3.radioGroup_Filter.Visible = false;
                    }
                };
                worker.RunWorkerAsync();
            }
        }

        #endregion

        #region 事件

        /// <summary>
        /// 补卡
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void patientInfoForCard2_GetHemoEventHandler(object sender, EventArgs e)
        {
            _hemoIdCard2 = this.patientInfoForCard2.hemoID;
        }
        /// <summary>
        /// 挂失
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void patientInfoForCard1_GetHemoEventHandler(object sender, EventArgs e)
        {
            DrugModel.MED_PATIENTS_CARDDataTable cardInfo = objPatient.GetCardInfoByInfo(this.patientInfoForCard1.hemoID, string.Empty);
            this.currentHemoId = this.patientInfoForCard1.hemoID;
            if (cardInfo == null)
            {
                this.groupControl1.Text = string.Format("☆卡信息  状态：此人没有制卡！");
                this.btnLock.Enabled = false;
                this.btnUnLock.Visible = false;
            }
            else if (cardInfo.Rows.Count <= 0)
            {
                this.groupControl1.Text = string.Format("☆卡信息  状态：此人没有制卡！");
                this.btnLock.Enabled = false;
                this.btnUnLock.Visible = false;
            }
            else if (cardInfo.Rows.Count > 0)
            {
                BaseControlInfo.SetControlDataByDataTable(cardInfo, downpanel);
                string state = string.Empty;
                string seriaNumber = string.Empty;
                if (cardInfo.Count > 1)
                {
                    var row = cardInfo.FirstOrDefault(i => i.STATE == "0");
                    if (row == null)
                    {
                        row = cardInfo.FirstOrDefault(i => i.STATE == "1");
                        if (row == null)
                            row = cardInfo.FirstOrDefault(i => i.STATE == "2");
                    }
                    state = row != null ? row.STATE : state;
                    seriaNumber = row != null ? row.SERIALNUMBER : seriaNumber;
                }
                else
                {
                    state = cardInfo[0].STATE;
                    seriaNumber = cardInfo[0].SERIALNUMBER;
                }
                switch (state)
                {
                    case "0":
                        {
                            this.groupControl1.Text = string.Format("☆卡信息  卡当前状态：正常 卡号为：{0}", seriaNumber);
                            this.btnLock.Enabled = true;
                            this.btnLock.Visible = true;
                            //this.btnUnLock.Visible = true;
                            break;
                        }
                    case "1":
                        {
                            this.groupControl1.Text = string.Format("☆卡信息  卡当前状态：挂失 卡号为：{0}", seriaNumber);
                            this.btnLock.Enabled = false;
                            this.btnLock.Visible = false;
                            this.btnUnLock.Visible = true;
                            this.btnUnLock.Enabled = true;
                            break;
                        }
                    case "2":
                        {
                            this.groupControl1.Text = string.Format("☆卡信息  卡当前状态：作废 卡号为：{0}", seriaNumber);
                            this.btnLock.Enabled = false;
                            this.btnLock.Visible = false;
                            this.btnUnLock.Visible = false;
                            break;
                        }
                }

            }
            this._hemoIdCard1 = this.patientInfoForCard1.hemoID;
            this.downpanel.Visible = false;
        }
        /// <summary>
        /// 挂失
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnLock_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(_hemoIdCard1))
            {

                AutoClosedMsgBox.ShowForm("请录入挂失患者信息！", this.Text, 10000, MessageBoxIcon.Asterisk);
                return;
            }
            if (XtraMessageBox.Show("是否确认挂失此卡信息？", "提示", MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.No)
                return;

            if (objPatient.UpdateCardStateByParam("1", _hemoIdCard1) > 0)
            {
                AutoClosedMsgBox.ShowForm("挂失成功！", this.Text, 1000, MessageBoxIcon.Asterisk);
                //this.Close();
            }
            else
            {
                AutoClosedMsgBox.ShowForm("挂失失败！", this.Text, 10000, MessageBoxIcon.Asterisk);
            }
            InzationData();

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            var parent = this.Parent as DevExpress.XtraBars.Docking2010.Customization.FlyoutDialog;
            if (parent != null)
            {
                parent.DialogResult = DialogResult.OK;
            }
        }
        /// <summary>
        /// 补卡操作
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_Sup_Click(object sender, EventArgs e)
        {
            var patients = _patientCardDt.FirstOrDefault(i => i.HEMODIALYSIS_ID == _hemoIdCard2 && i.STATE == "0");
            if (patients != null)
            {
                XtraMessageBox.Show("患者卡未作废或回收，请先作废或回收再进行补卡!");
                return;
            }

            //EditPatientCard frm = new EditPatientCard();
            EditPatientCardInfo frm = new EditPatientCardInfo();
            frm.HemoId = _hemoIdCard2;
            if (frm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                var data = frm._patientCard;
                if (objPatient.SavePatCardInfo(data) > 0)
                {
                    AutoClosedMsgBox.ShowForm("补卡成功！", this.Text, 1000, MessageBoxIcon.Asterisk);
                    //this.Close();
                }
                else
                {
                    AutoClosedMsgBox.ShowForm("补卡失败！", this.Text, 10000, MessageBoxIcon.Asterisk);
                }
                InzationData();

            }
        }
        private void PatientCardOperatorUI_Load(object sender, EventArgs e)
        {
            this.groupControl1.Text = string.Empty;
            //InzationData();
        }
        /// <summary>
        /// 取消挂失
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnUnLock_Click(object sender, EventArgs e)
        {
            if (objPatient.UpdateCardStateByParam("0", _hemoIdCard1) > 0)
            {
                AutoClosedMsgBox.ShowForm("解除挂失成功！", this.Text, 1000, MessageBoxIcon.Asterisk);
                //this.Close();
            }
            else
            {
                AutoClosedMsgBox.ShowForm("解除挂失失败！", this.Text, 10000, MessageBoxIcon.Asterisk);
            }
            InzationData();

        }

        private void btnDELETE_Click(object sender, EventArgs e)
        {
            if (XtraMessageBox.Show("是否确认作废此卡或回收，作废或回收后不可取消！", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == System.Windows.Forms.DialogResult.No)
                return;

            if (objPatient.UpdateCardStateByParam("2", _hemoIdCard1) > 0)
            {
                AutoClosedMsgBox.ShowForm("此卡已作废或回收！", this.Text, 1000, MessageBoxIcon.Asterisk);
                //this.Close();
            }
            else
            {
                AutoClosedMsgBox.ShowForm("作废或回收失败！", this.Text, 10000, MessageBoxIcon.Asterisk);
            }
            InzationData();

        }
        private void xtraTabControl1_TabIndexChanged(object sender, EventArgs e)
        {

        }

        private void xtraTabControl1_SelectedPageChanged(object sender, DevExpress.XtraTab.TabPageChangedEventArgs e)
        {
            InzationData();

            if (this.xtraTabControl1.SelectedTabPage == this.xtraTabPage3)
            {
                DrugModel.MED_PATIENTS_CARDDataTable dt = new DrugModel.MED_PATIENTS_CARDDataTable();
                if (_patientCardDt != null && _patientCardDt.Rows.Count > 0)
                {
                    this._patientCardDt.Where(i => i.STATE == "0").CopyToDataTable(dt, LoadOption.PreserveChanges);
                }
                this.patientGridCtl1.InzationData(dt, this._patientDt);
            }
            else if (this.xtraTabControl1.SelectedTabPage == this.xtraTabPage4)
            {
                DrugModel.MED_PATIENTS_CARDDataTable dt = new DrugModel.MED_PATIENTS_CARDDataTable();
                if (_patientCardDt != null && _patientCardDt.Rows.Count > 0)
                    this._patientCardDt.Where(i => i.STATE == "1").CopyToDataTable(dt, LoadOption.PreserveChanges);
                this.patientGridCtl2.InzationData(dt, this._patientDt);
            }
            else if (this.xtraTabControl1.SelectedTabPage == this.xtraTabPage5)
            {
                DrugModel.MED_PATIENTS_CARDDataTable dt = new DrugModel.MED_PATIENTS_CARDDataTable();
                if (_patientCardDt != null && _patientCardDt.Rows.Count > 0)
                    this._patientCardDt.Where(i => i.STATE == "2" || i.STATE == "3").CopyToDataTable(dt, LoadOption.PreserveChanges);
                this.patientGridCtl3.InzationData(dt, this._patientDt);
            }
        }

        #endregion
    }
}
