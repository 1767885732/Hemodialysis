/*----------------------------------------------------------------
// Copyright (C) 2005 苏州XX公司有限公司
// 描述：患者托管药品使用记录列表窗体
// 创建时间：2014-03-10
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
using System.Linq;
using Hemo.Utilities;
using Hemo.IService.Config;
using Hemo.Client.Print;
using DevExpress.XtraReports.UI;

namespace Hemo.Client.UI.Drug {
    public partial class QueryPatientDrugOutput : HemoBaseFrm
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

        public QueryPatientDrugOutput()
        {
            InitializeComponent();
        }

        #endregion

        #region 事件

        private void btnQuery_Click(object sender, EventArgs e)
        {

        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            EditPatientDrugOutput frm = new EditPatientDrugOutput();
            frm.CurrentHemoId = currentHemoId;
            frm.ShowDialog();
            Query();
        }

        private void QueryPatientDrugOutput_Load(object sender, EventArgs e)
        {
            ctlUserLongInfo1.HEMODIALYSIS_ID = currentHemoId;
            ctlUserLongInfo1.LoadPatientInfo();
            var date = this._configService.GetConfigList(string.Empty, string.Empty, "药品单位", "1");
            this.repositoryItemCustomGridLookUpEdit1.DataSource = date;
            this.txtFromDate.EditValue = DateTime.Parse(DateTime.Now.Year.ToString() + "-" + "01" + "-" + "01");
            this.txtToDate.EditValue = DateTime.Now.Date;
            Query();
        }

        private void dxSimpleButton3_Click(object sender, EventArgs e)
        {
            Query();
        }

        private void dxSimpleButton1_Click(object sender, EventArgs e)
        {
            this.Close();
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
            var data = objDrug.QueryPatientDrugInputById(currentHemoId, Convert.ToDateTime(this.txtFromDate.EditValue), Convert.ToDateTime(this.txtToDate.EditValue));


            string inputId = TempRow["INPUT_ID"].ToString();
            GridRowID = TempRow["ID"].ToString();
            GridDrugCode = TempRow["DRUG_CODE"].ToString();
            var rowTp = data.AsEnumerable().FirstOrDefault(i => i["DRUG_CODE"].ToString() == GridDrugCode);

            GridRowSum = Utility.CDecimal(rowTp["DRUG_SUM"].ToString()) + Utility.CDecimal(TempRow["USE_COUNT"].ToString());
            if (XtraMessageBox.Show("确定删除选中的患者药品使用记录吗？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                int result = objDrug.DeletePatientDrugOutputByID(GridRowID);
                if (result > 0)
                {
                    int resultSum = objDrug.UpdatePatientDrugInputByOutPutParam(currentHemoId, GridDrugCode, GridRowSum, inputId);
                    if (resultSum > 0)
                    {
                        XtraMessageBox.Show("删除患者药品使用记录成功！");
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

        private void gridDrugMaster_Click(object sender, EventArgs e)
        {
            if (this.gridView1.GetFocusedDataRow() != null)
            {
                btnDel.Enabled = true;
            }
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            var data = objDrug.QueryPatientDrugOutPutToPrint(currentHemoId, Convert.ToDateTime(this.txtFromDate.EditValue), Convert.ToDateTime(this.txtToDate.EditValue));
            PatientDrugOutPutPrint frm = new PatientDrugOutPutPrint(data, ctlUserLongInfo1.Patient.NAME);
            ReportPrintTool pt = new ReportPrintTool(frm);
            pt.ShowPreviewDialog();
        }

        #endregion

        #region 方法

        private void Query()
        {
            this.gridDrugMaster.DataSource = objDrug.QueryPatientDrugOutputById(currentHemoId, Convert.ToDateTime(this.txtFromDate.EditValue), Convert.ToDateTime(this.txtToDate.EditValue));
        }

        #endregion
    }
}