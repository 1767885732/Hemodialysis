/*----------------------------------------------------------------
// Copyright (C) 2005 苏州XX公司
// 描述：患者耗材模板设计窗体
// 创建时间：2016-05-22
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
using Hemo.IService;
using Hemo.Service;
using Hemo.Model;
using DevExpress.XtraEditors;
using Hemo.Utilities;

namespace Hemo.Client.UI.Patient
{
    public partial class PatientMaterialMode : HemoBaseFrm
    {
        #region 类变量

        private IPatient patientService = ServiceManager.Instance.PatientService;
        private MaterialScheduleModel.MED_MATERIAL_MASTERDataTable _materialDataTable;
        private IMaterial objMaterial = ServiceManager.Instance.MaterialService;

        #endregion

        #region 属性

        #endregion

        #region 构造函数

        public PatientMaterialMode()
        {
            InitializeComponent();
        }

        #endregion

        #region 事件

        private void PatientMaterialMode_Load(object sender, EventArgs e)
        {
            loadMaterialMasterList();
            gridControl3.DataSource = patientService.QueryModelByParams();
            gridControl2.DataSource = patientService.QueryMaterialModelByParams("");
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
            this.Close();
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
                txtModel.Text = gridRowKey;
                gridControl2.DataSource = patientService.QueryMaterialModelByParams(gridRowKey);
                loadMaterialMasterList();
                DataTable packageProductDT = (DataTable)gridControl1.DataSource;
                DataTable modelDT2 = (DataTable)gridControl2.DataSource;
                foreach (DataRow row2 in modelDT2.Rows)
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
            //删除数据
            if (e.Column == gridColumn3)
            {
                if (XtraMessageBox.Show("确定删除选中的耗材模版吗？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    int result = patientService.DeleteMaterialModelByName(gridRowKey);
                    if (result > 0)
                    {
                        XtraMessageBox.Show("删除耗材模版成功！");
                        gridControl3.DataSource = patientService.QueryModelByParams();
                    }
                    else
                    {
                        XtraMessageBox.Show("删除耗材模版失败！");
                    }
                }
            }
        }

        private void gridView3_RowCellClick(object sender, DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs e)
        {
            string gridRowKey = string.Empty;
            DataRow tempRow = gridView3.GetFocusedDataRow();
            if (tempRow == null)
                return;
            gridRowKey = tempRow["MATERIAL_ID"].ToString();
            var productName = tempRow["MATERIAL_NAME"].ToString();
            //删除数据
            if (e.Column == gridDelete)
            {
                DataTable packageProductDT = (DataTable)gridControl1.DataSource;
                DataRow productRow = packageProductDT.NewRow();
                productRow["MATERIAL_ID"] = gridRowKey;
                productRow["MATERIAL_NAME"] = productName;
                MaterialModel.MED_MATERIAL_MASTERDataTable dtRecord = objMaterial.GetMaterialMasterListByMaterialID(gridRowKey);
                productRow["MATERIAL_SPEC"] = dtRecord[0].IsMATERIAL_SPECNull() ? string.Empty : dtRecord[0].MATERIAL_SPEC;
                productRow["SUPPLIER"] = dtRecord[0].IsSUPPLIERNull() ? string.Empty : dtRecord[0].SUPPLIER;
                productRow["FIRM_NAME"] = dtRecord[0].IsFIRM_NAMENull() ? string.Empty : dtRecord[0].FIRM_NAME;
                productRow["MATERIAL_PINYIN"] = dtRecord[0].MATERIAL_PINYIN;
                productRow["MATERIAL_TYPE"] = dtRecord[0]["TYPE"].ToString();
                productRow["MATERIAL_SPEC"] = dtRecord[0]["MATERIAL_SPEC"].ToString();
                //productRow["FIRM_NAME"] = dtRecord[0]["FIRM_NAME"].ToString();
                packageProductDT.Rows.Add(productRow);
                packageProductDT.AcceptChanges();
                tempRow.Delete();
                tempRow.AcceptChanges();
            }
        }

        private void gridView1_RowCellClick(object sender, DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs e)
        {
            string gridRowKey = string.Empty;
            DataRow tempRow = gridView1.GetFocusedDataRow();
            if (tempRow == null)
                return;
            gridRowKey = tempRow["MATERIAL_ID"].ToString();
            var productName = tempRow["MATERIAL_NAME"].ToString();
            if (e.Column == gridSelect)
            {
                DataTable modelDT = (DataTable)gridControl2.DataSource;
                if (modelDT == null)
                    modelDT = patientService.QueryMaterialModelByParams(""); ;
                DataRow modelRow = modelDT.NewRow();
                modelRow["MATERIAL_ID"] = gridRowKey;
                modelRow["MATERIAL_NAME"] = productName;
                modelRow["MATERIAL_NUMBER"] = 1;
                modelRow["ITEMTYPE"] = tempRow["SUPPLIER"].ToString();
                modelRow["MATERTYPE"] = tempRow["MATERIAL_TYPE"].ToString();
                modelRow["MATERIAL_SPEC"] = tempRow["MATERIAL_SPEC"].ToString();
                try
                {
                    modelRow["FIRM_NAME"] = tempRow["FIRM_NAME"].ToString();

                }
                catch
                {
                    modelRow["FIRM_NAME"] = string.Empty;

                }

                foreach (DataRow row in modelDT.Rows)
                {
                    if (row["MATERIAL_ID"].ToString() == gridRowKey)
                    {
                        XtraMessageBox.Show("模板记录已经存在！");
                        return;
                    }
                }
                modelDT.Rows.Add(modelRow);
                modelDT.AcceptChanges();
                tempRow.Delete();
                tempRow.AcceptChanges();
                txtProductFilter.Text = string.Empty;
                txtProductFilter.Focus();
            }
        }

        private void btSave_Click(object sender, EventArgs e)
        {
            if (txtModel.Text == "")
            {
                XtraMessageBox.Show("模版名称不能为空");
                return;
            }
            DataTable modelDT4 = (DataTable)gridControl3.DataSource;
            foreach (DataRow row4 in modelDT4.Rows)
            {
                if (row4["MATERIAL_MODEL_NAME"].ToString() == txtModel.Text)
                {
                    if (XtraMessageBox.Show("模版列表已经存在此名称，你确定覆盖原模版数据吗？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        int result = patientService.DeleteMaterialModelByName(txtModel.Text.Trim());
                    }
                    else
                    {
                        txtModel.Text = "";
                        return;
                    }
                }
            }
            MaterialScheduleModel.MED_MATERIAL_MODELDataTable dtRecord =
                new MaterialScheduleModel.MED_MATERIAL_MODELDataTable();
            DataTable modelDT = (DataTable)gridControl2.DataSource;
            if (modelDT == null)
                return;
            foreach (DataRow row in modelDT.Rows)
            {
                var rowIndex = dtRecord.NewMED_MATERIAL_MODELRow();
                rowIndex.MATERIAL_MODEL_ID = Guid.NewGuid().ToString().Trim();
                rowIndex.MATERIAL_MODEL_NAME = txtModel.Text.Trim();
                rowIndex.MATERIAL_ID = row["MATERIAL_ID"].ToString();
                rowIndex.MATERIAL_NAME = row["MATERIAL_NAME"].ToString();
                rowIndex.MATERIAL_NUMBER = Utility.CDecimal(row["MATERIAL_NUMBER"].ToString());
                rowIndex.ITEMTYPE = row["ITEMTYPE"].ToString();
                rowIndex.MATERTYPE = row["MATERTYPE"].ToString();
                rowIndex.MATERIAL_SPEC = row["MATERIAL_SPEC"].ToString();
                rowIndex.FIRM_NAME = row["FIRM_NAME"].ToString();

                dtRecord.AddMED_MATERIAL_MODELRow(rowIndex);
            }
            int result1 = patientService.SaveMaterialModel(dtRecord);
            if (result1 > 0)
            {
                XtraMessageBox.Show("保存耗材模版成功！");
                gridControl3.DataSource = patientService.QueryModelByParams();
                txtModel.Text = "";
                gridControl2.DataSource = patientService.QueryMaterialModelByParams("");
                gridControl1.DataSource = objMaterial.GetMaterialAll();
            }
            else
            {
                XtraMessageBox.Show("保存耗材模版失败！");
            }
        }

        #endregion

        #region 方法

        private void loadMaterialMasterList()
        {
            _materialDataTable = new MaterialScheduleModel.MED_MATERIAL_MASTERDataTable();
            _materialDataTable = objMaterial.GetMaterialAll();
            gridControl1.DataSource = _materialDataTable;
        }

        #endregion
    }
}
