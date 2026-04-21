/*----------------------------------------------------------------
// Copyright (C) 2013 苏州XX公司
// 描述：患者耗材明细维护窗体
// 创建时间：2016-05-18
// 创建者：贺建操
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
using Hemo.Model;
using Hemo.IService;
using Hemo.Service;
using Hemo.IService.Config;
using DevExpress.XtraEditors;
using Hemo.Utilities;
using Hemo.Client.Core;
using Hemo.Client.UI.Machine;

namespace Hemo.Client.UI.Patient
{
    /// <summary>
    /// 界面的基类
    /// </summary>
    [ToolboxItem(true)]
    public partial class PatientMaterialDetailUI : ViewBase
    {
        #region 类变量

        private MaterialScheduleModel.MED_MATERIAL_MASTERDataTable _materialDataTable;
        private IMaterial objMaterial = ServiceManager.Instance.MaterialService;
        private IConfig _configService = ServiceManager.Instance.ConfigService;
        private IPatient patientService = ServiceManager.Instance.PatientService;
        private string currentHemoId = string.Empty;
        private string packageCode = string.Empty;
        /// <summary>
        /// 处方号
        /// </summary>
        private string recipeId = string.Empty;
        private MaterialScheduleModel.MED_MATERIAL_MODELDataTable dtRecord = null;
        private MaterialScheduleModel.MED_PATIENT_MATERIALDataTable dmRecord = null;
        private MaterialScheduleModel.MED_PATIENT_MATERIALRow currentRecordRow = null;
        private bool _materialInfoVisabled = true;

        #endregion

        #region 属性

        public MaterialScheduleModel.MED_PATIENT_MATERIALRow CurrentRecordRow
        {
            get { return currentRecordRow; }
            set { currentRecordRow = value; }
        }
        /// <summary>
        /// 治疗单号
        /// </summary>
        public string PackageCode
        {
            get { return packageCode; }
            set { packageCode = value; }
        }
        /// <summary>
        /// 透析编号
        /// </summary>
        public string CurrentHemoId
        {
            get { return currentHemoId; }
            set { currentHemoId = value; }
        }
        /// <summary>
        /// 处方号
        /// </summary>
        public string RecipeId
        {
            get { return recipeId; }
            set { recipeId = value; }
        }
        public bool MaterialInfoVisabled
        {
            get
            {
                return _materialInfoVisabled;
            }
            set
            {
                _materialInfoVisabled = value;
                lblInfo.Visible = value;
            }
        }

        #endregion

        #region 构造函数

        public PatientMaterialDetailUI()
        {
            InitializeComponent();
        }

        #endregion

        #region 事件

        private void PatientMaterialDetailUI_Load(object sender, EventArgs e)
        {

        }
        private void txtProductFilter_TextChanged(object sender, EventArgs e)
        {
            string Filter = string.Empty;
            if (txtProductFilter.Text.Length == 0)
            {
                _materialDataTable.DefaultView.RowFilter = string.Empty;
            }
            else
            {
                _materialDataTable.DefaultView.RowFilter = "MATERIAL_PINYIN LIKE '%" + txtProductFilter.Text.Trim() + "%' ";
            }
        }
        private void btnCloseDrug_Click(object sender, EventArgs e)
        {
            this.Parent.FindForm().Close();
        }
        private void gridView1_RowCellClick(object sender, DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs e)
        {
            string gridRowKey = string.Empty;
            DataRow tempRow = gridView1.GetFocusedDataRow();
            if (tempRow == null)
                return;
            gridRowKey = tempRow["MATERIAL_ID"].ToString();
            var productName = tempRow["MATERIAL_NAME"].ToString();
            //选择数据
            if (e.Column == gridSelect)
            {
                DataTable modelDT = (DataTable)grdProduct.DataSource;
                DataRow modelRow = modelDT.NewRow();
                foreach (DataRow row in modelDT.Rows)
                {
                    if (row["MATERIAL_ID"].ToString() == gridRowKey)
                    {
                        XtraMessageBox.Show("患者耗材列表已经存在！");
                        return;
                    }
                }
                DataTable packageProductDT = (DataTable)grdProduct.DataSource;
                DataRow productRow = packageProductDT.NewRow();
                productRow["MATERIAL_ID"] = gridRowKey;
                productRow["MATERIAL_NAME"] = productName;
                productRow["MATERIAL_NAME"] = productName;
                productRow["ITEMTYPE"] = tempRow["SUPPLIER"].ToString();
                productRow["MATERTYPE"] = tempRow["MATERIAL_TYPE"].ToString();
                productRow["PRICE"] = tempRow["MATERIAL_PRICE"].ToString();
                productRow["MATERIAL_NUMBER"] = 1;
                productRow["ID"] = Guid.NewGuid().ToString();
                packageProductDT.Rows.Add(productRow);
                tempRow.Delete();
                tempRow.AcceptChanges();
                packageProductDT.AcceptChanges();
                txtProductFilter.Text = string.Empty;
                txtProductFilter.Focus();
            }
        }

        private void gridView2_RowCellClick(object sender, DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs e)
        {
            string gridRowKey = string.Empty;
            DataRow tempRow = gridView2.GetFocusedDataRow();
            if (tempRow == null)
                return;
            gridRowKey = tempRow["MATERIAL_ID"].ToString();
            var productName = tempRow["MATERIAL_NAME"].ToString();
            //删除数据
            if (e.Column == gridDeleteColumn)
            {
                DataTable packageProductDT = (DataTable)gridControl1.DataSource;
                DataRow productRow = packageProductDT.NewRow();
                productRow["MATERIAL_ID"] = gridRowKey;
                productRow["MATERIAL_NAME"] = productName;
                MaterialModel.MED_MATERIAL_MASTERDataTable dtRecord = objMaterial.GetMaterialMasterListByMaterialID(gridRowKey);
                productRow["MATERIAL_SPEC"] = dtRecord[0].IsMATERIAL_SPECNull() ? string.Empty : dtRecord[0].MATERIAL_SPEC;
                productRow["SUPPLIER"] = dtRecord[0].IsSUPPLIERNull() ? string.Empty : dtRecord[0].SUPPLIER;
                productRow["FIRM_NAME"] = dtRecord[0].FIRM_NAME;
                productRow["MATERIAL_PINYIN"] = dtRecord[0].MATERIAL_PINYIN;
                productRow["MATERIAL_TYPE"] = dtRecord[0]["TYPE"].ToString();
                productRow["MATERIAL_PRICE"] = dtRecord[0]["PRICE"].ToString();
                packageProductDT.Rows.Add(productRow);
                tempRow.Delete();
                tempRow.AcceptChanges();
                packageProductDT.AcceptChanges();
            }
        }
        private void btnSaveDrug_Click(object sender, EventArgs e)
        {
            dmRecord = new MaterialScheduleModel.MED_PATIENT_MATERIALDataTable();
            string RECORDID = packageCode;
            DataTable modelDT = (DataTable)grdProduct.DataSource;
            int rowNum = 0;

            if (currentRecordRow != null)//编辑
            {
                RECORDID = currentRecordRow.RECORDID;
                DateTime creatDate = currentRecordRow.CREATEDATE;
                foreach (DataRow row in modelDT.Rows)
                {
                    var rowIndex = dmRecord.NewMED_PATIENT_MATERIALRow();
                    rowIndex.ID = Guid.NewGuid().ToString().Trim();
                    rowIndex.RECORDID = packageCode;
                    rowIndex.HEMODIALYSIS_ID = currentHemoId;
                    rowIndex.CREATEDATE = creatDate;
                    rowIndex.LASTUPDATEBY = HemoApplicationContext.Current.CurrentUser.USER_ID;
                    rowIndex.LASTUPDATEDATE = System.DateTime.Now;
                    rowIndex.MATERIAL_ID = row["MATERIAL_ID"].ToString();
                    rowIndex.MATERIAL_NAME = row["MATERIAL_NAME"].ToString();
                    rowIndex.MATERIAL_NUMBER = Utility.CDecimal(row["MATERIAL_NUMBER"].ToString());
                    rowIndex.ITEMTYPE = row["ITEMTYPE"].ToString();
                    rowIndex.PRICE = (rowIndex.MATERIAL_NUMBER * Utility.CDecimal(row["PRICE"].ToString())).ToString();
                    rowIndex.RECIPEID = this.recipeId;
                    rowIndex.STATE = "0";
                    rowIndex.MATERTYPE = row["MATERTYPE"].ToString();
                    rowIndex.ISDELETE = "0";
                    rowNum++;
                    rowIndex.ROWINDEX = rowNum;
                    dmRecord.AddMED_PATIENT_MATERIALRow(rowIndex);
                }
                int result = patientService.DeleteMaterialRecordByName(RECORDID);
                if (result > 0)
                {
                    int result1 = patientService.SaveMaterialRecord(dmRecord);
                    if (result1 > 0)
                    {
                        XtraMessageBox.Show("保存患者耗材成功！");
                    }
                }
                else
                {
                    XtraMessageBox.Show("保存患者耗材失败！");
                }
            }
            else//新增
            {
                RECORDID = string.IsNullOrEmpty(RECORDID) ? Guid.NewGuid().ToString().Trim() : RECORDID;
                foreach (DataRow row in modelDT.Rows)
                {
                    var rowIndex = dmRecord.NewMED_PATIENT_MATERIALRow();
                    rowIndex.ID = Guid.NewGuid().ToString().Trim();
                    rowIndex.RECORDID = RECORDID;
                    rowIndex.HEMODIALYSIS_ID = currentHemoId;
                    rowIndex.CREATEDATE = DateTime.Now.Date;
                    rowIndex.LASTUPDATEBY = HemoApplicationContext.Current.CurrentUser.USER_ID;
                    rowIndex.LASTUPDATEDATE = System.DateTime.Now;
                    rowIndex.MATERIAL_ID = row["MATERIAL_ID"].ToString();
                    rowIndex.MATERIAL_NAME = row["MATERIAL_NAME"].ToString();
                    rowIndex.MATERIAL_NUMBER = Utility.CDecimal(row["MATERIAL_NUMBER"].ToString());
                    rowIndex.ITEMTYPE = row["ITEMTYPE"].ToString();
                    rowIndex.MATERTYPE = row["MATERTYPE"].ToString();
                    rowIndex.PRICE = (rowIndex.MATERIAL_NUMBER * Utility.CDecimal(row["PRICE"].ToString())).ToString();
                    rowIndex.RECIPEID = this.recipeId;
                    rowIndex.ISDELETE = "0";
                    rowIndex.STATE = "0";
                    rowNum++;
                    rowIndex.ROWINDEX = rowNum;
                    dmRecord.AddMED_PATIENT_MATERIALRow(rowIndex);
                }
                int result1 = patientService.SaveMaterialRecord(dmRecord);
                if (result1 > 0)
                {
                    XtraMessageBox.Show("保存患者耗材成功！");
                }
            }

        }
        private void gridView4_RowCellClick(object sender, DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs e)
        {
            string gridRowKey = string.Empty;
            DataRow tempRow = gridView4.GetFocusedDataRow();
            if (tempRow == null)
                return;
            gridRowKey = tempRow["MATERIAL_MODEL_NAME"].ToString();
            //选择数据
            if (e.Column == gridColumn6)
            {
                DataTable modelDTgrd = (DataTable)grdProduct.DataSource;
                DataTable modelDT2 = patientService.QueryMaterialModelByParams(gridRowKey);
                DataTable packageProductDT = (DataTable)gridControl1.DataSource;
                foreach (DataRow row2 in modelDT2.Rows)
                {
                    int index = 0;
                    for (int i = 0; i < modelDTgrd.Rows.Count; i++)
                    {
                        if (row2["MATERIAL_ID"].ToString() == modelDTgrd.Rows[i]["MATERIAL_ID"].ToString())
                        {
                            XtraMessageBox.Show("患者耗材列表" + row2["MATERIAL_NAME"] + "已经存在！");
                        }
                        else
                        {
                            index = index + 1;
                        }
                    }
                    if (index == modelDTgrd.Rows.Count)
                    {
                        DataRow modelRowgrd = modelDTgrd.NewRow();
                        modelRowgrd["MATERIAL_ID"] = row2["MATERIAL_ID"];
                        modelRowgrd["ID"] = Guid.NewGuid().ToString().Trim();
                        modelRowgrd["MATERIAL_NAME"] = row2["MATERIAL_NAME"];
                        modelRowgrd["MATERIAL_NUMBER"] = row2["MATERIAL_NUMBER"];
                        modelRowgrd["ITEMTYPE"] = row2["ITEMTYPE"].ToString();
                        modelRowgrd["MATERTYPE"] = row2["MATERTYPE"].ToString();
                        modelDTgrd.Rows.Add(modelRowgrd);
                        modelDTgrd.AcceptChanges();
                    }
                    for (int i = 0; i < packageProductDT.Rows.Count; i++)
                    {
                        if (row2["MATERIAL_ID"].ToString() == packageProductDT.Rows[i]["MATERIAL_ID"].ToString())
                        {
                            packageProductDT.Rows.RemoveAt(i);
                        }
                    }
                }
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            PatientMaterialMode recordDetail = new PatientMaterialMode();
            DialogResult result = recordDetail.ShowDialog();
            LoadModel();
        }

        private void lbHide_Click(object sender, EventArgs e)
        {
            if (this.panelControl3.Visible)
            {
                this.panelControl3.Visible = false;
                this.lbHide.Appearance.Image = global::Hemo.Client.Properties.Resources.left2;
            }
            else
            {
                this.panelControl3.Visible = true;
                this.lbHide.Appearance.Image = global::Hemo.Client.Properties.Resources.right2;
            }
        }
        private void lbHide_MouseHover(object sender, EventArgs e)
        {
            this.lbHide.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(243)))), ((int)(((byte)(183)))));
        }

        private void lbHide_MouseLeave(object sender, EventArgs e)
        {
            this.lbHide.Appearance.BackColor = System.Drawing.Color.Transparent;
        }

        #endregion

        #region 方法

        public void InzationDataDetailUi()
        {
            txtProductFilter.Focus();
            loadMaterialMasterList();
            LoadPackageProduct();
            LoadModel();
        }

        private void LoadModel()
        {
            gridControl3.DataSource = patientService.QueryModelByParams();
        }
        private void LoadPackageProduct()
        {
            grdProduct.DataSource = patientService.QueryMaterialDetailByParams(recipeId);
            DataTable modelDTgrd = (DataTable)grdProduct.DataSource;
            DataTable packageProductDT = (DataTable)gridControl1.DataSource;
            foreach (DataRow row2 in modelDTgrd.Rows)
            {
                for (int i = 0; i < packageProductDT.Rows.Count; i++)
                {
                    if (row2["MATERIAL_ID"].ToString() == packageProductDT.Rows[i]["MATERIAL_ID"].ToString())
                    {
                        packageProductDT.Rows.RemoveAt(i);
                    }
                }
            }
        }
        private void loadMaterialMasterList()
        {
            _materialDataTable = new MaterialScheduleModel.MED_MATERIAL_MASTERDataTable();
            _materialDataTable = objMaterial.GetMaterialAll();
            gridControl1.DataSource = _materialDataTable;
        }

        #endregion
    }
}
