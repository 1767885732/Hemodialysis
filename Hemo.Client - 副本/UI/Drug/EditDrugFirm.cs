/*----------------------------------------------------------------
// Copyright (C) 2013 苏州XX公司有限公司
// 描述：药品厂商维护窗体
// 创建时间：2013-03-21
// 创建者：刘超
//  
// 修改时间：2014-5-6
// 修改人：吕志强
// 修改描述：更新局部逻辑代码
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
using Hemo.Service;
using Hemo.Model;
using Hemo.IService;
using Hemo.Utilities;

namespace Hemo.Client.UI.Drug
{
    public partial class EditDrugFirm :HemoBaseFrm
    {
        #region 成员变量

        /// <summary>
        /// 药品主档数据服务对象
        /// </summary>
        private IDrug objDrug = ServiceManager.Instance.DrugService;

        /// <summary>
        /// 药品主档表
        /// </summary>
        DrugModel.MED_DRUG_FIRMDataTable tmpDataTable = new DrugModel.MED_DRUG_FIRMDataTable();

        /// <summary>
        /// 是否新增
        /// </summary>
        private bool isAdd = true;

        #endregion

        #region 构造方法

        public EditDrugFirm()
        {
            InitializeComponent();
        }

        public EditDrugFirm(bool pIsAdd, string pFirmID)
        {
            InitializeComponent();
            isAdd = pIsAdd;
            txtFIRM_ID.Text = pFirmID;
        }

        #endregion

        #region 事件

        /// <summary>
        ///根据药厂编号得到对应数据并赋值
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EditDrugFirm_Load(object sender, EventArgs e)
        {
            string firmID = txtFIRM_ID.Text.Trim();
            if (!isAdd)
            {
                if (firmID.Length > 0)
                {
                    tmpDataTable = objDrug.GetDrugFrimListByFirmID(firmID);
                    if (tmpDataTable != null && tmpDataTable.Rows.Count > 0)
                    {
                        BaseControlInfo.SetControlDataByDataTable(tmpDataTable, pnlControls);
                    }
                }
            }
        }

        /// <summary>
        /// 拼音码
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtFIRM_NAME_Leave(object sender, EventArgs e)
        {
            txtFIRM_PINYIN.Text = PinYinConverter.GetPYString(txtFIRM_NAME.Text);
        }

        /// <summary>
        /// 保存数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (saveData() == 1)
            {
                //XtraMessageBox.Show("保存成功", "药品厂商");
                AutoClosedMsgBox.ShowForm("保存成功。", "系统提示", 1500, MessageBoxIcon.Information);

                this.DialogResult = System.Windows.Forms.DialogResult.Yes;
            }
        }

        /// <summary>
        /// 新增数据 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAdd_Click(object sender, EventArgs e)
        {
            isAdd = true;
            BaseControlInfo.ClearControlText(pnlControls);
            txtFIRM_NAME.Focus();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        #endregion

        #region 数据方法

        /// <summary>
        /// 保存数据
        /// </summary>
        /// <returns></returns>
        private int saveData()
        {
            int result = 0;
            if (isAdd)
            {
                txtFIRM_ID.Text = objDrug.GetNewFirmID();//需要新生成药品厂商ID
            }
            DataTable dt = BaseControlInfo.GetDataTableByPanel(tmpDataTable, pnlControls, isAdd);
            result = objDrug.SaveDrugFirmInfo((DrugModel.MED_DRUG_FIRMDataTable)dt);
            return result;
        }

        #endregion
    }
}