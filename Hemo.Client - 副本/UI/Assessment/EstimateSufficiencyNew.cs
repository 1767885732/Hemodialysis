/*----------------------------------------------------------------
// Copyright (C) 2013 苏州XX公司
// 描述：URR|Kt/V|TS评估窗体类
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
    public partial class EstimateSufficiencyNew : HemoBaseFrm
    {
        #region 类变量

        private string hemoId = string.Empty;

        private PatientModel.MED_PATIENTSRow currentPatient;

        private int flag = 0;

        private IHemodialysis hemoService = ServiceManager.Instance.HemodialysisService;

        private HemodialysisModel.MED_ESTIMATE_SUFFICIENCYRow currentRow = null;

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

        /// <summary>
        /// 当前患者
        /// </summary>
        public PatientModel.MED_PATIENTSRow CurrentPatient
        {
            get { return currentPatient; }
            set { currentPatient = value; }
        }

        /// <summary>
        /// 标识 0=URR|Kt/V、1=TS、2=MDRD
        /// </summary>
        public int Flag
        {
            get { return flag; }
            set { flag = value; }
        }

        /// <summary>
        /// 当前选中行
        /// </summary>
        public HemodialysisModel.MED_ESTIMATE_SUFFICIENCYRow CurrentRow
        {
            get { return currentRow; }
            set { currentRow = value; }
        }

        #endregion

        #region 构造函数

        public EstimateSufficiencyNew()
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
            this.xtraTabControl1.SelectedTabPageIndex = flag;
            InitData();
            this.Text = "URR|Kt/V|TS|MDRD评估";
            ProFunctionCount pfc = new ProFunctionCount();
            pfc.SaveFunctionCountFrm(this);
        }

        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (this.xtraTabControl1.SelectedTabPageIndex == flag)
            {
                dtSufficiency = dtSufficiency ?? new HemodialysisModel.MED_ESTIMATE_SUFFICIENCYDataTable();
                if (dtSufficiency.Rows.Count == 0)
                {
                    var row = dtSufficiency.NewMED_ESTIMATE_SUFFICIENCYRow();
                    row.ID = Guid.NewGuid().ToString();
                    row.HEMODIALYSIS_ID = hemoId;
                    row.CREATE_DATE = DateTime.Now;
                    row.IS_DELETE = "0";
                    row.FLAG = flag.ToString();
                    dtSufficiency.AddMED_ESTIMATE_SUFFICIENCYRow(row);
                }

                if (flag == 0)
                {
                    dtSufficiency[0].BEFORE_BLOODUREA = Utility.CDecimal(this.txtBEFORE_BLOODUREA.Text);
                    dtSufficiency[0].AFTER_BLOODUREA = Utility.CDecimal(this.txtAFTER_BLOODUREA.Text);
                    dtSufficiency[0].TIME = Utility.CDecimal(this.txtTIME.Text);
                    dtSufficiency[0].UF = Utility.CDecimal(this.txtUF.Text);
                    dtSufficiency[0].WEIGHT = Utility.CDecimal(this.txtWEIGHT.Text);
                    dtSufficiency[0].SPKT_V = Utility.CDecimal(this.txtSPKT_V.Text);
                    dtSufficiency[0].URR = Utility.CDecimal(this.txtURR.Text) / 100;
                }
                else if (flag == 1)
                {
                    dtSufficiency[0].SI = Utility.CDecimal(this.txtSI.Text);
                    dtSufficiency[0].TIBC = Utility.CDecimal(this.txtTIBC.Text);
                    dtSufficiency[0].TS = Utility.CDecimal(this.txtTS.Text) / 100;
                }
                else
                {
                    dtSufficiency[0].SCR = Utility.CDecimal(this.txtSCR.Text);
                    dtSufficiency[0].AGE = Utility.CDecimal(this.txtAge.Text);
                    dtSufficiency[0].WEIGHT = Utility.CDecimal(this.txtWeight2.Text);
                    dtSufficiency[0].IS_FEMALE = this.chkFemale.Checked ? "1" : "0";
                    dtSufficiency[0].IS_BLACK = this.chkBlack.Checked ? "1" : "0";
                    dtSufficiency[0].GFR = Utility.CDecimal(this.txtGFR.Text);
                    dtSufficiency[0].CCR = Utility.CDecimal(this.txtCcr.Text);
                }

                int result = hemoService.SaveEstimateSufficiency(dtSufficiency);
                if (result > 0)
                {
                    this.DialogResult = DialogResult.OK;
                    XtraMessageBox.Show("保存成功！", "URR|Kt/V|TS|MDRD评估");
                }
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
        /// 血清肌酐值改变
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtSCR_EditValueChanged(object sender, EventArgs e)
        {
            EditValueChanged();
        }

        /// <summary>
        /// 年龄值改变
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtAge_EditValueChanged(object sender, EventArgs e)
        {
            EditValueChanged();
        }

        /// <summary>
        /// 体重值改变
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtWeight2_EditValueChanged(object sender, EventArgs e)
        {
            EditValueChanged();
        }

        /// <summary>
        /// 女性选中状态改变
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chkFemale_CheckedChanged(object sender, EventArgs e)
        {
            EditValueChanged();
        }

        /// <summary>
        /// 黑人选中状态改变
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chkBlack_CheckedChanged(object sender, EventArgs e)
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

        /// <summary>
        /// 血清铁值改变
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtSI_EditValueChanged(object sender, EventArgs e)
        {
            EditValueChanged();
        }

        /// <summary>
        /// 总铁结合力值改变
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtTIBC_EditValueChanged(object sender, EventArgs e)
        {
            EditValueChanged();
        }

        /// <summary>
        /// 血清铁文本框录入
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtSI_KeyPress(object sender, KeyPressEventArgs e)
        {
            KeyPress(sender, e);
        }

        /// <summary>
        /// 总铁结合力文本框录入
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtTIBC_KeyPress(object sender, KeyPressEventArgs e)
        {
            KeyPress(sender, e);
        }

        /// <summary>
        /// 血清肌酐文本框录入
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtSCR_KeyPress(object sender, KeyPressEventArgs e)
        {
            KeyPress(sender, e);
        }

        /// <summary>
        /// 年龄文本框录入
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtAge_KeyPress(object sender, KeyPressEventArgs e)
        {
            KeyPress(sender, e);
        }

        /// <summary>
        /// 体重文本框录入
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtWeight2_KeyPress(object sender, KeyPressEventArgs e)
        {
            KeyPress(sender, e);
        }

        #endregion

        #region 方法

        /// <summary>
        /// 初始化数据
        /// </summary>
        private void InitData()
        {
            var date = hemoService.GetMainCureByHemoID(hemoId);
            if (date != null && date.Rows.Count > 0)
            {
                var recipeDate = hemoService.GetRecipeByRecipeID(date[0].RECIPE_ID);
                this.txtSPKT_V1.Text = recipeDate[0].SPKT_V.ToString();
                this.txtURR1.Text = recipeDate[0].URR.ToString();
            }
            else
            {
                this.txtSPKT_V1.Text = "1.2";
                this.txtURR1.Text = "65";
            }

            if (currentRow != null)
            {
                if (flag == 0)
                {
                    this.txtBEFORE_BLOODUREA.Text = currentRow.BEFORE_BLOODUREA.ToString();
                    this.txtAFTER_BLOODUREA.Text = currentRow.AFTER_BLOODUREA.ToString();
                    this.txtTIME.Text = currentRow.TIME.ToString();
                    this.txtUF.Text = currentRow.UF.ToString();
                    this.txtWEIGHT.Text = currentRow.WEIGHT.ToString();
                    this.txtSPKT_V.Text = currentRow.SPKT_V.ToString();
                    this.txtURR.Text = currentRow["DISPLAY_URR"].ToString().Replace("%", string.Empty);
                }
                else if (flag == 1)
                {
                    this.txtSI.Text = currentRow.SI.ToString();
                    this.txtTIBC.Text = currentRow.TIBC.ToString();
                    this.txtTS.Text = currentRow["DISPLAY_TS"].ToString().Replace("%", string.Empty);
                }
                else
                {
                    this.txtSCR.Text = currentRow.SCR.ToString();
                    this.txtAge.Text = currentRow.AGE.ToString();
                    this.txtWeight2.Text = currentRow.WEIGHT.ToString();
                    this.chkFemale.Checked = currentRow.IS_FEMALE == "1" ? true : false;
                    this.chkBlack.Checked = currentRow.IS_BLACK == "1" ? true : false;
                    this.txtGFR.Text = currentRow.GFR.ToString();
                    this.txtCcr.Text = currentRow.CCR.ToString();
                }

                dtSufficiency = hemoService.GetEstimateSufficiencyByHemoIdAndDate(hemoId, flag.ToString(), currentRow.CREATE_DATE);
            }
            else
            {
                this.txtAge.Text = currentPatient.AGE.ToString();
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
        /// 计算并返回TS值
        /// </summary>
        /// <param name="si"></param>
        /// <param name="tibc"></param>
        /// <returns></returns>
        private decimal GetTSResult(decimal si, decimal tibc)
        {
            //TS=SI÷TIBC×100% 
            decimal ts = 0;
            if (tibc == 0)
                return ts;
            ts = si / tibc * 100 / 100;
            ts = decimal.Round(ts, 2);
            return ts;
        }

        /// <summary>
        /// 计算并返回GFR值
        /// </summary>
        /// <param name="scr"></param>
        /// <param name="age"></param>
        /// <param name="isFemale"></param>
        /// <param name="isBlack"></param>
        /// <returns></returns>
        private decimal GetGFRResult(decimal scr, int age, bool isFemale, bool isBlack)
        {
            //GFR(ml/min1.73m2)=186×Scr-1.154x年龄-0.203x(0.742女性)x(1.21黑人)
            decimal gfr = 0;
            decimal ratio1 = isFemale ? (decimal)0.742 : 1;
            decimal ratio2 = isBlack ? (decimal)1.21 : 1;
            gfr = 186 * scr - (decimal)1.154 * age - (decimal)0.203 * ratio1 * ratio2;
            gfr = decimal.Round(gfr, 2);
            return gfr;
        }

        /// <summary>
        /// 计算并返回CCR值
        /// </summary>
        /// <param name="scr"></param>
        /// <param name="age"></param>
        /// <param name="weight"></param>
        /// <param name="isFemale"></param>
        /// <returns></returns>
        private decimal GetCCRResult(decimal scr, int age, decimal weight, bool isFemale)
        {
            //Ccr=(140-年龄)x体重(Kg)x(0.85女性)/(72xScr)
            decimal ccr = 0;
            decimal ratio = isFemale ? (decimal)0.85 : 1;
            ccr = (140 - age) * weight * ratio / (72 * scr);
            ccr = decimal.Round(ccr, 2);
            return ccr;
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
            if (this.xtraTabControl1.SelectedTabPageIndex == 0)
            {
                this.txtURR.Text = string.Empty;
                this.txtSPKT_V.Text = string.Empty;

                if (string.IsNullOrEmpty(this.txtBEFORE_BLOODUREA.Text))
                    return;
                if (string.IsNullOrEmpty(this.txtAFTER_BLOODUREA.Text))
                    return;

                this.txtURR.Text = ((int)(GetURRResult(Utility.CDecimal(this.txtBEFORE_BLOODUREA.Text), Utility.CDecimal(this.txtAFTER_BLOODUREA.Text)) * 100)).ToString();

                if (string.IsNullOrEmpty(this.txtTIME.Text))
                    return;
                if (string.IsNullOrEmpty(this.txtUF.Text))
                    return;
                if (string.IsNullOrEmpty(this.txtWEIGHT.Text))
                    return;

                this.txtSPKT_V.Text = GetSPKT_VResult(Utility.CDecimal(this.txtBEFORE_BLOODUREA.Text), Utility.CDecimal(this.txtAFTER_BLOODUREA.Text), Utility.CDecimal(this.txtTIME.Text), Utility.CDecimal(this.txtUF.Text), Utility.CDecimal(this.txtWEIGHT.Text)).ToString();
            }
            else if (this.xtraTabControl1.SelectedTabPageIndex == 1)
            {
                this.txtTS.Text = string.Empty;

                if (string.IsNullOrEmpty(this.txtSI.Text))
                    return;
                if (string.IsNullOrEmpty(this.txtTIBC.Text))
                    return;

                this.txtTS.Text = ((int)(GetTSResult(Utility.CDecimal(this.txtSI.Text), Utility.CDecimal(this.txtTIBC.Text)) * 100)).ToString();
            }
            else
            {
                this.txtGFR.Text = string.Empty;
                this.txtCcr.Text = string.Empty;

                if (string.IsNullOrEmpty(this.txtSCR.Text))
                    return;
                if (string.IsNullOrEmpty(this.txtAge.Text))
                    return;

                this.txtGFR.Text = GetGFRResult(Utility.CDecimal(this.txtSCR.Text), Utility.CInt(this.txtAge.Text), this.chkFemale.Checked, this.chkBlack.Checked).ToString();

                if (string.IsNullOrEmpty(this.txtWeight2.Text))
                    return;

                this.txtCcr.Text = GetCCRResult(Utility.CDecimal(this.txtSCR.Text), Utility.CInt(this.txtAge.Text), Utility.CDecimal(this.txtWeight2.Text), this.chkFemale.Checked).ToString();
            }
        }

        #endregion
    }
}