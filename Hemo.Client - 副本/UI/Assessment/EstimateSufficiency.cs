/*----------------------------------------------------------------
// Copyright (C) 2005 苏州XX公司有限公司
// 描述：透析充分性评估编辑窗体
// 创建时间：2015-03-14
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

namespace Hemo.Client.UI.Assessment
{
    public partial class EstimateSufficiency : HemoBaseFrm
    {
        #region 成员变量

        private string hemoId = string.Empty;

        private IPatient patientService = ServiceManager.Instance.PatientService;

        private IHemodialysis hemoService = ServiceManager.Instance.HemodialysisService;

        private HemodialysisModel.MED_ESTIMATE_SUFFICIENCYDataTable dtSufficiency = null;

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

        public EstimateSufficiency()
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
        private void EstimateSufficiency_Load(object sender, EventArgs e)
        {
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
            dtSufficiency = dtSufficiency ?? new HemodialysisModel.MED_ESTIMATE_SUFFICIENCYDataTable();
            if (dtSufficiency.Rows.Count == 0)
            {
                var row = dtSufficiency.NewMED_ESTIMATE_SUFFICIENCYRow();
                row.HEMODIALYSIS_ID = hemoId;
                dtSufficiency.AddMED_ESTIMATE_SUFFICIENCYRow(row);
            }
            dtSufficiency[0].BEFORE_BLOODUREA = Utility.CDecimal(this.txtBEFORE_BLOODUREA.Text);
            dtSufficiency[0].AFTER_BLOODUREA = Utility.CDecimal(this.txtAFTER_BLOODUREA.Text);
            dtSufficiency[0].TIME = Utility.CDecimal(this.txtTIME.Text);
            dtSufficiency[0].UF = Utility.CDecimal(this.txtUF.Text);
            dtSufficiency[0].WEIGHT = Utility.CDecimal(this.txtWEIGHT.Text);
            dtSufficiency[0].SPKT_V = Utility.CDecimal(this.txtSPKT_V.Text);
            int result = hemoService.SaveEstimateSufficiency(dtSufficiency);
            if (result > 0)
            {
                XtraMessageBox.Show("保存成功！", "透析充分性评估");
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
        /// 透前血尿素值改变
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtBEFORE_BLOODUREA_EditValueChanged(object sender, EventArgs e)
        {
            EditValueChanged();
        }

        /// <summary>
        /// 透后血尿素值改变
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtAFTER_BLOODUREA_EditValueChanged(object sender, EventArgs e)
        {
            EditValueChanged();
        }

        /// <summary>
        /// 时间值改变
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtTIME_EditValueChanged(object sender, EventArgs e)
        {
            EditValueChanged();
        }

        /// <summary>
        /// 超滤量值改变
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtUF_EditValueChanged(object sender, EventArgs e)
        {
            EditValueChanged();
        }

        /// <summary>
        /// 体重值改变
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtWEIGHT_EditValueChanged(object sender, EventArgs e)
        {
            EditValueChanged();
        }

        /// <summary>
        /// 透前血尿素文本框录入
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtBEFORE_BLOODUREA_KeyPress(object sender, KeyPressEventArgs e)
        {
            KeyPress(sender, e);
        }

        /// <summary>
        /// 透后血尿素文本框录入
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtAFTER_BLOODUREA_KeyPress(object sender, KeyPressEventArgs e)
        {
            KeyPress(sender, e);
        }

        /// <summary>
        /// 时间文本框录入
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtTIME_KeyPress(object sender, KeyPressEventArgs e)
        {
            KeyPress(sender, e);
        }

        /// <summary>
        /// 超滤量文本框录入
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtUF_KeyPress(object sender, KeyPressEventArgs e)
        {
            KeyPress(sender, e);
        }

        /// <summary>
        /// 体重文本框录入
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtWEIGHT_KeyPress(object sender, KeyPressEventArgs e)
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
            dtSufficiency = hemoService.GetEstimateSufficiencyByHemoId(hemoId, "0");
            if (dtSufficiency != null && dtSufficiency.Rows.Count > 0)
            {
                this.txtBEFORE_BLOODUREA.Text = dtSufficiency[0].BEFORE_BLOODUREA.ToString();
                this.txtAFTER_BLOODUREA.Text = dtSufficiency[0].AFTER_BLOODUREA.ToString();
                this.txtTIME.Text = dtSufficiency[0].TIME.ToString();
                this.txtUF.Text = dtSufficiency[0].UF.ToString();
                this.txtWEIGHT.Text = dtSufficiency[0].WEIGHT.ToString();
                this.txtSPKT_V.Text = dtSufficiency[0].SPKT_V.ToString();
            }
        }

        /// <summary>
        /// 计算并返回spKt/V值
        /// </summary>
        /// <param name="beforeBloodUrea"></param>
        /// <param name="afterBloodUrea"></param>
        /// <param name="time"></param>
        /// <param name="uf"></param>
        /// <param name="weight"></param>
        /// <returns></returns>
        private decimal GetSPKT_VResult(decimal beforeBloodUrea, decimal afterBloodUrea, decimal time, decimal uf, decimal weight)
        {
            //spKt/V=-ln(R-0.008t)+(4-3.5R)x(ΔBW/BW)
            decimal result = 0;
            if (beforeBloodUrea == 0)
                return result;
            if (weight == 0)
                return result;
            if (afterBloodUrea / beforeBloodUrea <= (decimal)0.008 * time)
                return result;
            result = -(decimal)Math.Log((double)(afterBloodUrea / beforeBloodUrea - (decimal)0.008 * time)) + (4 - (decimal)3.5 * (afterBloodUrea / beforeBloodUrea)) * (uf / weight);
            result = decimal.Round(result, 2);
            return result;
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
            this.txtSPKT_V.Text = string.Empty;
            if (string.IsNullOrEmpty(this.txtBEFORE_BLOODUREA.Text))
                return;
            if (string.IsNullOrEmpty(this.txtAFTER_BLOODUREA.Text))
                return;
            if (string.IsNullOrEmpty(this.txtTIME.Text))
                return;
            if (string.IsNullOrEmpty(this.txtUF.Text))
                return;
            if (string.IsNullOrEmpty(this.txtWEIGHT.Text))
                return;
            this.txtSPKT_V.Text = GetSPKT_VResult(Utility.CDecimal(this.txtBEFORE_BLOODUREA.Text), Utility.CDecimal(this.txtAFTER_BLOODUREA.Text), Utility.CDecimal(this.txtTIME.Text), Utility.CDecimal(this.txtUF.Text), Utility.CDecimal(this.txtWEIGHT.Text)).ToString();
        }

        #endregion
    }
}