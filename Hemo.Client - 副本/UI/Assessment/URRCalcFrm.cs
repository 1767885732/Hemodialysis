/*----------------------------------------------------------------
// Copyright (C) 2005 苏州XX公司有限公司
// 描述：URR计算统计编辑窗体
// 创建时间：2015-03-16
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
using Hemo.Model;
using Hemo.IService;
using Hemo.Service;
using Hemo.Utilities;
using Hemo.IService.Config;
using Hemo.Client.UI.Hemodialysis;

namespace Hemo.Client.UI.Assessment
{
    public partial class URRCalcFrm : HemoBaseFrm
    {
        #region 成员变量

        private string hemoId = string.Empty;

        private IPatient patientService = ServiceManager.Instance.PatientService;

        private IHemodialysis hemoService = ServiceManager.Instance.HemodialysisService;

        private HemoModel.MED_PATIENTS_URRDataTable dtPatientURR = null;

        #endregion

        #region 属性

        /// <summary>
        /// 透析编号
        /// </summary>
        public string HemoId
        {
            get { return hemoId; }
            set { hemoId = value; }
        }

        #endregion

        #region 构造函数

        public URRCalcFrm()
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
        private void URRCalcFrm_Load(object sender, EventArgs e)
        {
            this.Text = "URR计算统计";
            ProFunctionCount pfc = new ProFunctionCount();
            pfc.SaveFunctionCountFrm(this);
            LoadPatientInfo();
            InitData();
        }

        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSave_Click(object sender, EventArgs e)
        {
            dtPatientURR = dtPatientURR ?? new HemoModel.MED_PATIENTS_URRDataTable();
            if (dtPatientURR.Rows.Count == 0)
            {
                var row = dtPatientURR.NewMED_PATIENTS_URRRow();
                row.HEMODIALYSIS_ID = hemoId;
                dtPatientURR.AddMED_PATIENTS_URRRow(row);
            }
            dtPatientURR[0].BEFORE_BUN = Utility.CDecimal(this.txtBEFORE_BUN.Text);
            dtPatientURR[0].AFTER_BUN = Utility.CDecimal(this.txtAFTER_BUN.Text);
            dtPatientURR[0].URR = Utility.CDecimal(this.txtURR.Text);
            int result = hemoService.SavePatientURR(dtPatientURR);
            if (result > 0)
            {
                XtraMessageBox.Show("保存成功！", "URR计算统计");
            }
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

        /// <summary>
        /// 透前BUN值改变
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtBEFORE_BUN_EditValueChanged(object sender, EventArgs e)
        {
            EditValueChanged();
        }

        /// <summary>
        /// 透后BUN值改变
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtAFTER_BUN_EditValueChanged(object sender, EventArgs e)
        {
            EditValueChanged();
        }

        /// <summary>
        /// 透前BUN文本框录入
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtBEFORE_BUN_KeyPress(object sender, KeyPressEventArgs e)
        {
            KeyPress(sender, e);
        }

        /// <summary>
        /// 透后BUN文本框录入
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtAFTER_BUN_KeyPress(object sender, KeyPressEventArgs e)
        {
            KeyPress(sender, e);
        }

        #endregion

        #region 方法

        /// <summary>
        /// 加载病人信息
        /// </summary>
        private void LoadPatientInfo()
        {
            PatientModel.MED_PATIENTSDataTable dtPatient = patientService.GetPatientListByParams(string.Empty, hemoId);
            if (dtPatient != null && dtPatient.Rows.Count > 0)
            {
                this.ctlUserLongInfo.HEMODIALYSIS_ID = dtPatient[0].HEMODIALYSIS_ID;
                this.ctlUserLongInfo.LoadPatientInfo();
            }
        }

        /// <summary>
        /// 初始化数据
        /// </summary>
        private void InitData()
        {
            dtPatientURR = hemoService.GetPatientURRByHemoId(hemoId);
            if (dtPatientURR != null && dtPatientURR.Rows.Count > 0)
            {
                this.txtBEFORE_BUN.Text = dtPatientURR[0].BEFORE_BUN.ToString();
                this.txtAFTER_BUN.Text = dtPatientURR[0].AFTER_BUN.ToString();
                this.txtURR.Text = dtPatientURR[0].URR.ToString();
            }
        }

        /// <summary>
        /// 计算并返回URR值
        /// </summary>
        /// <param name="beforeBUN"></param>
        /// <param name="afterBUN"></param>
        /// <returns></returns>
        private decimal GetURRResult(decimal beforeBUN, decimal afterBUN)
        {
            //URR=（透前BUN-透后BUN）÷透前BUN ×100% 
            decimal urr = 0;
            if (beforeBUN == 0)
                return urr;
            if (beforeBUN <= afterBUN)
                return urr;
            urr = (beforeBUN - afterBUN) / beforeBUN * 100 / 100;
            urr = decimal.Round(urr, 2);
            return urr;
        }

        /// <summary>
        /// 文本框录入
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void KeyPress(object sender, KeyPressEventArgs e)
        {
            decimal result = 0;
            if (e.KeyChar != '\b' && e.KeyChar != '.')
            {
                if (!decimal.TryParse(e.KeyChar.ToString(), out result))
                    e.Handled = true;
            }
        }

        /// <summary>
        /// 文本值改变
        /// </summary>
        private void EditValueChanged()
        {
            this.txtURR.Text = string.Empty;
            if (string.IsNullOrEmpty(this.txtBEFORE_BUN.Text))
                return;
            if (string.IsNullOrEmpty(this.txtAFTER_BUN.Text))
                return;
            this.txtURR.Text = GetURRResult(Utility.CDecimal(this.txtBEFORE_BUN.Text), Utility.CDecimal(this.txtAFTER_BUN.Text)).ToString();
        }

        #endregion
    }
}