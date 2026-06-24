/*----------------------------------------------------------------
// Copyright (C) 2005 苏州XX公司有限公司
// 描述：患者药品托管列表窗体
// 创建时间：2014-03-08
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
using Hemo.IService;
using Hemo.Service;
using Hemo.Utilities;
using Hemo.IService.Config;

namespace Hemo.Client.UI.Drug
{
    public partial class QueryPatientDrugInput : HemoBaseFrm
    {
        #region 类变量

        private IDrug objDrug = ServiceManager.Instance.DrugService;
        private IConfig _configService = ServiceManager.Instance.ConfigService;

        private string currentHemoId = string.Empty;

        #endregion

        #region 属性

        public string CurrentHemoId
        {
            get { return currentHemoId; }
            set { currentHemoId = value; }
        }

        #endregion

        #region 构造函数

        public QueryPatientDrugInput()
        {
            InitializeComponent();
        }

        #endregion

        #region 事件

        private void btnAdd_Click(object sender, EventArgs e)
        {
            EditPatientDrugInput frm = new EditPatientDrugInput();
            frm.CurrentHemoId = currentHemoId;
            frm.ShowDialog();
            Query();
        }

        private void QueryPatientDrugInput_Load(object sender, EventArgs e)
        {
            ctlUserLongInfo1.HEMODIALYSIS_ID = currentHemoId;
            ctlUserLongInfo1.LoadPatientInfo();
            var date = this._configService.GetConfigList(string.Empty, string.Empty, "药品单位", "1");
            this.repositoryItemCustomGridLookUpEdit1.DataSource = date;
            this.txtFromDate.EditValue = DateTime.Parse(DateTime.Now.Year.ToString() + "-" + "01" + "-" + "01");
            this.txtToDate.EditValue = DateTime.Now.Date;
            Query();
        }

        private void dxSimpleButton1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dxSimpleButton2_Click(object sender, EventArgs e)
        {
            Query();
        }

        private void gridDrugMaster_Click(object sender, EventArgs e)
        {
            if (this.gridView1.GetFocusedDataRow() != null)
            {
                btnDel.Enabled = true;
            }
        }

        private void btnDel_Click(object sender, EventArgs e)
        {
            string GridRowID = string.Empty;
            string GridDrugCode = string.Empty;
            decimal GridRowSum;
            DataRow TempRow = gridView1.GetFocusedDataRow();
            if (TempRow == null)
            {
                return;
            }
            GridRowID = TempRow["ID"].ToString();
            GridRowSum = Utility.CDecimal(TempRow["DRUG_SUM"].ToString()) - Utility.CDecimal(TempRow["DRUG_COUNT"].ToString());
            GridDrugCode = TempRow["DRUG_CODE"].ToString();
            if (XtraMessageBox.Show("确定删除选中的患者药品托管记录吗？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                int result = objDrug.DeletePatientDrugInputByID(GridRowID);
                if (result > 0)
                {
                    int resultSum = objDrug.UpdatePatientDrugInputByParam(currentHemoId, GridDrugCode, GridRowSum);
                    if (resultSum > 0)
                    {
                        XtraMessageBox.Show("删除患者药品托管记录成功！");
                        Query();
                    }
                    else
                    {
                        XtraMessageBox.Show("更新患者药品托管剩余量失败！");
                    }
                }
                else
                {
                    XtraMessageBox.Show("删除患者药品托管记录失败！");
                }
            }
        }

        #endregion

        #region 方法

        private void Query()
        {
            this.gridDrugMaster.DataSource = objDrug.QueryPatientDrugInputById(currentHemoId, Convert.ToDateTime(this.txtFromDate.EditValue), Convert.ToDateTime(this.txtToDate.EditValue));
        }

        #endregion
    }
}