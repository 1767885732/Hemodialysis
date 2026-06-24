/*----------------------------------------------------------------
// Copyright (C) 2013 苏州XX公司
// 描述：病人基础列表窗体
// 创建时间：2013-03-11
// 创建者：刘超
//  
// 修改时间：
// 修改人：
// 修改描述：
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
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraEditors.Controls;
using Hemo.Service;
using Hemo.Model;
using Hemo.IService;
using Hemo.Client.UI.Hemodialysis;
using Hemo.Utilities;
using DevExpress.XtraPrinting;

namespace Hemo.Client.UI.Patient {
    public partial class QueryPatientList : HemoBaseFrm {

        #region 类变量

        private PatientModel.MED_PATIENTSDataTable _patientDataTable;

        private IPatient objPatient = ServiceManager.Instance.PatientService;

        #endregion

        #region 属性

        private bool _isAdd = true;

        /// <summary>
        /// 是否为新增
        /// </summary>
        public bool IsAdd {
            get {
                return _isAdd;
            }
            set {
                _isAdd = value;
            }
        }

        private string _hemodialysisID = string.Empty;

        /// <summary>
        /// 选择行对应透析编号
        /// </summary>
        public string HemodialysisID {
            get {
                return _hemodialysisID;
            }
            set {
                _hemodialysisID = value;
            }
        }

        #endregion

        #region 构造函数

        public QueryPatientList() {
            InitializeComponent();
        }

        #endregion

        #region 事件

        /// <summary>
        /// 加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void QueryPatientList_Load(object sender, EventArgs e)
        {
            BindLookUpEdit();
            LoadPatientList();
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnQuery_Click(object sender, EventArgs e) {
            LoadPatientList();
        }

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAdd_Click(object sender, EventArgs e) {
            IsAdd = true;
            EditPatientNew frm = new EditPatientNew();
            frm.Owner = this;
            frm.ShowDialog();
            if (frm.DialogResult == DialogResult.Yes) {
                LoadPatientList();
            }
        }

        /// <summary>
        /// 编辑
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnEdit_Click(object sender, EventArgs e) {
            DataRow dr = gridView4.GetFocusedDataRow();
            IsAdd = false;
            HemodialysisID = dr["HEMODIALYSIS_ID"].ToString();
            EditPatientNew frm = new EditPatientNew();
            frm.Current = dr as PatientModel.MED_PATIENTSRow;
            frm.Owner = this;
            frm.ShowDialog();
            if (frm.DialogResult == DialogResult.Yes) {
                LoadPatientList();
            }
        }

        private void gridView4_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e) {
            try {
                DataRow dr = gridView4.GetFocusedDataRow();
                if (dr != null) {
                    btnVascularAccess.Enabled = btnEdit.Enabled = true;
                }
                else {
                    btnVascularAccess.Enabled = btnEdit.Enabled = false;
                }
            }
            catch (Exception ex) {
                MessageBox.Show(ex.Message, "病人列表");
            }
        }

        /// <summary>
        /// 测试血管通路窗体功能，通过透析号得到病人血管通路数据数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnVascularAccess_Click(object sender, EventArgs e) {
            DataRow dr = gridView4.GetFocusedDataRow();
            HemodialysisID = dr["HEMODIALYSIS_ID"].ToString();
            EditVascularAccess frm = new EditVascularAccess(HemodialysisID);
            frm.Owner = this;
            frm.ShowDialog();
        }

        /// <summary>
        /// 更新拼音码
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void simpleButton1_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < _patientDataTable.Rows.Count; i++)
            {
                _patientDataTable.Rows[i]["INPUT_CODE"] = PinYinConverter.GetPYString(_patientDataTable.Rows[i]["NAME"].ToString());
            }

            objPatient.SavePatientInfo(_patientDataTable);
        }

        /// <summary>
        /// 导出列表
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnOutExcel_Click(object sender, EventArgs e)
        {
            string fileName = string.Empty;
            if (string.IsNullOrEmpty(this.txtPatientName.Text))
            {
                if (this.lupType.EditValue == null || this.lupType.EditValue == DBNull.Value)
                {
                    fileName = "血透全部患者列表" + DateTime.Now.ToString("yyyyMMdd") + "." + "xls";
                }
                else
                {
                    if (this.lupType.EditValue.Equals("1"))
                    {
                        fileName = this.deStartDate.Text + "至" + this.deEndDate.Text + "血透新登记患者列表" + "." + "xls";
                    }
                    else if (this.lupType.EditValue.Equals("2"))
                    {
                        fileName = this.deStartDate.Text + "至" + this.deEndDate.Text + "血透透析患者列表" + "." + "xls";
                    }
                }
            }
            else
            {
                fileName = "血透透析患者" + _patientDataTable[0].NAME + "." + "xls";
            }
            SaveFileDialog dialog = new SaveFileDialog() { Title = "导出Excel", FileName = fileName, Filter = "Excel文件(*.xls)|*.*", RestoreDirectory = true };
            if (dialog.ShowDialog(this) == DialogResult.OK)
            {
                XlsExportOptions option = new XlsExportOptions() { TextExportMode = TextExportMode.Text };
                this.gridControl1.ExportToXls(dialog.FileName, option);
                AutoClosedMsgBox.ShowForm("导出成功", "提示", 1500, MessageBoxIcon.Information);
            }
        }

        /// <summary>
        /// 患者类型改变
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lupType_EditValueChanged(object sender, EventArgs e)
        {
            this.lblDate.Text = "登记日期";
            if (this.lupType.EditValue.Equals("1"))
            {
                this.lblDate.Text = "登记日期";
            }
            else if (this.lupType.EditValue.Equals("2"))
            {
                this.lblDate.Text = "透析日期";
            }
        }

        #endregion

        #region 方法

        /// <summary>
        /// 下拉列表绑定
        /// </summary>
        private void BindLookUpEdit()
        {
            DataTable dtType = new DataTable();
            dtType.Columns.Add("ITEM_ID", typeof(System.String));
            dtType.Columns.Add("ITEM_NAME", typeof(System.String));
            dtType.Columns.Add("ORDER_NUMBER", typeof(System.Int32));

            DataRow rowType = dtType.NewRow();
            dtType.Rows.Add(rowType);
            rowType = dtType.NewRow();
            rowType["ITEM_ID"] = "1";
            rowType["ITEM_NAME"] = "登记";
            rowType["ORDER_NUMBER"] = 1;
            dtType.Rows.Add(rowType);
            rowType = dtType.NewRow();
            rowType["ITEM_ID"] = "2";
            rowType["ITEM_NAME"] = "透析";
            rowType["ORDER_NUMBER"] = 2;
            dtType.Rows.Add(rowType);
            Utility.BindLookUpEdit(this.lupType, "ITEM_ID", "ITEM_NAME", dtType, "ITEM_NAME", "类型");
        }

        /// <summary>
        /// 加载病人信息
        /// </summary>
        private void LoadPatientList() {
            if (string.IsNullOrEmpty(this.txtPatientName.Text))
            {
                if (this.lupType.EditValue == null || this.lupType.EditValue == DBNull.Value)
                {
                    //查询全部患者
                    _patientDataTable = objPatient.GetPatientList();
                }
                else
                {
                    //查询选定类型患者
                    if (this.lupType.EditValue.Equals("1"))
                    {
                        _patientDataTable = objPatient.GetNewRecordPatientListByDate(Utility.CDate(this.deStartDate.Text), Utility.CDate(this.deEndDate.Text));
                    }
                    else if (this.lupType.EditValue.Equals("2"))
                    {
                        _patientDataTable = objPatient.GetDialysisPatientListByDate(Utility.CDate(this.deStartDate.Text), Utility.CDate(this.deEndDate.Text));
                    }
                }
            }
            else
            {
                //查询指定姓名患者
                _patientDataTable = objPatient.GetPatientListByParams(txtPatientName.Text.Trim(), txtHemoID.Text.Trim());
            }
            
            gridControl1.DataSource = _patientDataTable;
        }

        #endregion
    }
}