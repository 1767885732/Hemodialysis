/*----------------------------------------------------------------
// Copyright (C) 2005 苏州XX公司
// 描述：透析充分性评估窗体类
// 创建时间：2015-08-18
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
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Hemo.IService;
using Hemo.Service;
using Hemo.IService.Config;
using DevExpress.XtraEditors;
using Hemo.Model;
using Hemo.Client.Doc;
using Hemo.Client.UI.Machine;

namespace Hemo.Client.UI.Patient
{
    public partial class PatientSufficiency : HemoBaseFrm
    {
        #region 类变量

        private string currentHemoId = string.Empty;

        private string currentHemoName = string.Empty;

        private IPatient patientService = ServiceManager.Instance.PatientService;

        #endregion

        #region 属性

        public string CurrentHemoId
        {
            get { return currentHemoId; }
            set { currentHemoId = value; }
        }

        public string CurrentHemoName
        {
            get { return currentHemoName; }
            set { currentHemoName = value; }
        }

        #endregion

        #region 构造函数

        public PatientSufficiency()
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
        private void PatientSufficiency_Load(object sender, EventArgs e)
        {
            this.ctlMedicalUserInfo.HemoId = currentHemoId;
            this.ctlMedicalUserInfo.LoadUserInfo();

            this.txtFromDate.EditValue = DateTime.Parse(DateTime.Now.Year.ToString() + "-" + "01" + "-" + "01");
            this.txtToDate.EditValue = DateTime.Now.Date;

            Query();
            //初始化數據值進行數據綁定的時候進行顯示
            IniGridLookUpEditData();
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnQuery_Click(object sender, EventArgs e)
        {
            Query();
        }

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAdd_Click(object sender, EventArgs e)
        {
            PatientSufficiencyDetail recordDetail = new PatientSufficiencyDetail();
            recordDetail.CurrentHemoId = currentHemoId;
            recordDetail.CurrentRecordRow = null;
            DialogResult result = recordDetail.ShowDialog();
            if (result == DialogResult.OK)
            {
                Query();
            }
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (this.gvRecord.GetFocusedDataRow() == null)
            {
                XtraMessageBox.Show("请选择一行要删除的记录！");
                return;
            }

            if (XtraMessageBox.Show("是否确认删除当前选择记录？", "删除提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            {
                return;
            }

            string id = this.gvRecord.GetFocusedDataRow()["ID"].ToString();
            int result = patientService.DeletePatientSufficiencyById(id);

            if (result > 0)
            {
                XtraMessageBox.Show("删除记录成功！");
                Query();
            }
        }

        /// <summary>
        /// 关闭
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// 列表行双击
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gvRecord_DoubleClick(object sender, EventArgs e)
        {
            if (this.gvRecord.GetFocusedDataRow() != null)
            {
                PatientSufficiencyDetail recordDetail = new PatientSufficiencyDetail();
                recordDetail.CurrentHemoId = currentHemoId;
                recordDetail.CurrentHemoName = currentHemoName;
                recordDetail.CurrentRecordRow = this.gvRecord.GetFocusedDataRow() as PatientKolcabaModel.MED_PATIENT_SUFFICIENCYRow;

                DialogResult result = recordDetail.ShowDialog();
                if (result == DialogResult.OK)
                {
                    this.gcRecord.DataSource = patientService.QueryPatientSufficiencyByParams(currentHemoId, Convert.ToDateTime(this.txtFromDate.EditValue), Convert.ToDateTime(this.txtToDate.EditValue));
                }
            }
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            if (this.gvRecord.GetFocusedDataRow() != null)
            {
                var doc = new 透析充分性评估();
                doc.PatientRow = this.gvRecord.GetFocusedDataRow() as PatientKolcabaModel.MED_PATIENT_SUFFICIENCYRow; ;
                doc.Pname = currentHemoName;
                doc.LoadDocumentInfo();
                var frm = new ShowPrintForm(doc);
                frm.StartPosition = FormStartPosition.CenterParent;
                frm.ShowDialog();
            }

        }

        #endregion

        #region 方法

        /// <summary>
        /// 查询
        /// </summary>
        private void Query()
        {
            this.gcRecord.DataSource = patientService.QueryPatientSufficiencyByParams(currentHemoId, Convert.ToDateTime(this.txtFromDate.EditValue), Convert.ToDateTime(this.txtToDate.EditValue));
        }

        /// <summary>
        /// 初始化數據值進行數據綁定的時候進行顯示
        /// </summary>
        private void IniGridLookUpEditData()
        {
            #region 血壓/營養狀態  良好--欠佳--差

            var dt = new ConfigModel.MED_COMMON_ITEMLISTDataTable();
            var row = dt.NewMED_COMMON_ITEMLISTRow();
            row.ITEM_ID = Guid.NewGuid().ToString();
            row.ITEM_VALUE = "0";
            row.ITEM_NAME = "良好";
            row.ITEM_TYPE = "分類";
            row.STATUS = "0";
            row.ORDER_NUMBER = 1;
            dt.AddMED_COMMON_ITEMLISTRow(row);
            var row1 = dt.NewMED_COMMON_ITEMLISTRow();
            row1.ITEM_ID = Guid.NewGuid().ToString();
            row1.ITEM_VALUE = "1";
            row1.ITEM_NAME = "欠佳";
            row1.ITEM_TYPE = "分類";
            row1.STATUS = "0";
            row1.ORDER_NUMBER = 1;
            dt.AddMED_COMMON_ITEMLISTRow(row1);
            var row2 = dt.NewMED_COMMON_ITEMLISTRow();
            row2.ITEM_ID = Guid.NewGuid().ToString();
            row2.ITEM_VALUE = "2";
            row2.ITEM_NAME = "差";
            row2.ITEM_TYPE = "分類";
            row2.STATUS = "0";
            row2.ORDER_NUMBER = 1;
            dt.AddMED_COMMON_ITEMLISTRow(row2);

            this.repositoryItemCustomGridLookUpEdit2.DataSource = dt;
            this.repositoryItemCustomGridLookUpEdit3.DataSource = dt;

            #endregion

            #region 體重


            var dtt = new ConfigModel.MED_COMMON_ITEMLISTDataTable();
            var rowt = dtt.NewMED_COMMON_ITEMLISTRow();
            rowt.ITEM_ID = Guid.NewGuid().ToString();
            rowt.ITEM_VALUE = "0";
            rowt.ITEM_NAME = "<5%";
            rowt.ITEM_TYPE = "分類";
            rowt.STATUS = "0";
            rowt.ORDER_NUMBER = 1;
            dtt.AddMED_COMMON_ITEMLISTRow(rowt);
            var rowt1 = dtt.NewMED_COMMON_ITEMLISTRow();
            rowt1.ITEM_ID = Guid.NewGuid().ToString();
            rowt1.ITEM_VALUE = "1";
            rowt1.ITEM_NAME = ">5%";
            rowt1.ITEM_TYPE = "分類";
            rowt1.STATUS = "0";
            rowt1.ORDER_NUMBER = 1;
            dtt.AddMED_COMMON_ITEMLISTRow(rowt1);

            this.repositoryItemCustomGridLookUpEdit1.DataSource = dtt;

            #endregion

            #region KT/V


            var dtta = new ConfigModel.MED_COMMON_ITEMLISTDataTable();
            var rowta = dtta.NewMED_COMMON_ITEMLISTRow();
            rowta.ITEM_ID = Guid.NewGuid().ToString();
            rowta.ITEM_VALUE = "0";
            rowta.ITEM_NAME = "<1.2";
            rowta.ITEM_TYPE = "分類";
            rowta.STATUS = "0";
            rowta.ORDER_NUMBER = 1;
            dtta.AddMED_COMMON_ITEMLISTRow(rowta);
            var rowta1 = dtta.NewMED_COMMON_ITEMLISTRow();
            rowta1.ITEM_ID = Guid.NewGuid().ToString();
            rowta1.ITEM_VALUE = "1";
            rowta1.ITEM_NAME = "1.2-1.4";
            rowta1.ITEM_TYPE = "分類";
            rowta1.STATUS = "0";
            rowta1.ORDER_NUMBER = 1;
            dtta.AddMED_COMMON_ITEMLISTRow(rowta1);
            var rowta2 = dtta.NewMED_COMMON_ITEMLISTRow();
            rowta2.ITEM_ID = Guid.NewGuid().ToString();
            rowta2.ITEM_VALUE = "2";
            rowta2.ITEM_NAME = ">1.4";
            rowta2.ITEM_TYPE = "分類";
            rowta2.STATUS = "0";
            rowta2.ORDER_NUMBER = 1;
            dtta.AddMED_COMMON_ITEMLISTRow(rowta2);

            this.repositoryItemCustomGridLookUpEdit4.DataSource = dtta;

            #endregion

            #region URR


            var dta = new ConfigModel.MED_COMMON_ITEMLISTDataTable();
            var rta = dta.NewMED_COMMON_ITEMLISTRow();
            rta.ITEM_ID = Guid.NewGuid().ToString();
            rta.ITEM_VALUE = "0";
            rta.ITEM_NAME = "<65%";
            rta.ITEM_TYPE = "分類";
            rta.STATUS = "0";
            rta.ORDER_NUMBER = 1;
            dta.AddMED_COMMON_ITEMLISTRow(rta);
            var rta1 = dta.NewMED_COMMON_ITEMLISTRow();
            rta1.ITEM_ID = Guid.NewGuid().ToString();
            rta1.ITEM_VALUE = "1";
            rta1.ITEM_NAME = "65% - 70%";
            rta1.ITEM_TYPE = "分類";
            rta1.STATUS = "0";
            rta1.ORDER_NUMBER = 1;
            dta.AddMED_COMMON_ITEMLISTRow(rta1);
            var rta2 = dta.NewMED_COMMON_ITEMLISTRow();
            rta2.ITEM_ID = Guid.NewGuid().ToString();
            rta2.ITEM_VALUE = "2";
            rta2.ITEM_NAME = ">70%";
            rta2.ITEM_TYPE = "分類";
            rta2.STATUS = "0";
            rta2.ORDER_NUMBER = 1;
            dta.AddMED_COMMON_ITEMLISTRow(rta2);
            this.repositoryItemCustomGridLookUpEdit5.DataSource = dta;


            #endregion
        }

        #endregion
    }
}
