/*----------------------------------------------------------------
// Copyright (C) 2013 苏州XX公司有限公司
// 描述：耗材资料查询列表
// 创建时间：2013-03-22
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
using Hemo.Service;
using Hemo.Model;
using Hemo.Utilities;
using Hemo.IService;
using Hemo.IService.Config;
namespace Hemo.Client.UI.Material {
    public partial class CtlQueryMaterialMaster : DevExpress.XtraEditors.XtraUserControl
    {


        #region 私有成员
        /// <summary>
        /// 耗材资料表
        /// </summary>
        private MaterialModel.MED_MATERIAL_MASTERDataTable _materialDataTable;
        /// <summary>
        /// 耗材资料数据服务对象
        /// </summary>
        private IMaterial objMaterial = ServiceManager.Instance.MaterialService;
        private IConfig _configService = ServiceManager.Instance.ConfigService;
        private ConfigModel.MED_COMMON_ITEMLISTDataTable _materialTypes;
        private ConfigModel.MED_COMMON_ITEMLISTDataTable _materialUntie;

        #endregion

        #region  共有成员
        #endregion

        #region 初始化方法
        public CtlQueryMaterialMaster()
        {
            InitializeComponent();
            loadMaterialMasterList();
        }
        #endregion

        #region 各种事件

        private void btnAdd_Click(object sender, EventArgs e)
        {
            EditMaterialMaster frm = new EditMaterialMaster();

            frm.CurrentData = null;
            frm._materialdtMaster = _materialDataTable;
            if (DialogResult.OK == frm.ShowDialog())
            {
                loadMaterialMasterList();
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            //   this.Close();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            var dr = gridView1.GetFocusedDataRow() as MaterialModel.MED_MATERIAL_MASTERRow;
            EditMaterialMaster frm = new EditMaterialMaster();
            frm.CurrentData = dr;
            frm._materialdtMaster = _materialDataTable;
            if (DialogResult.OK == frm.ShowDialog())
            {
                loadMaterialMasterList();
            }
          
        }

        private void gridView1_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            try
            {
                DataRow dr = gridView1.GetFocusedDataRow();
                if (dr != null)
                {
                    btnEdit.Enabled = true;
                }
                else
                {
                    btnEdit.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "药品主档");
            }
        }

      
        #endregion

        #region 数据方法
        /// <summary>
        /// 加载耗材数据列表
        /// </summary>
        private void loadMaterialMasterList()
        {
            this.Enabled = false;
            _materialDataTable = new MaterialModel.MED_MATERIAL_MASTERDataTable();
            using (BackgroundWorker worker = new BackgroundWorker())
            {
                worker.DoWork += delegate(object sender1, DoWorkEventArgs e1)
                {
                    _materialDataTable = objMaterial.GetMaterialMasterList();
                    var date = this._configService.GetConfigList(string.Empty, string.Empty, "辅材类型", "1");               
                    this._materialTypes = date;
                    this._materialUntie = this._configService.GetConfigList(string.Empty, string.Empty, "耗材单位", "1");      
                };
                worker.RunWorkerCompleted += delegate(object sender2, RunWorkerCompletedEventArgs e2)
                {
                    this.repositoryItemCustomGridLookUpEdit1.DataSource = _materialTypes;
                    this.repositoryItemCustomGridLookUpEdit2.DataSource = _materialUntie;
                    gridMaterialMaster.DataSource = _materialDataTable;
                    this.Enabled = true;
                };
                worker.RunWorkerAsync();
            }
        }

      
        #endregion

       #region 事件
        private void chkFilter_CheckedChanged(object sender, EventArgs e)
        {
            this.gridView1.OptionsView.ShowAutoFilterRow = this.chkFilter.Checked;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.ParentForm.Close();

        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            var dr = gridView1.GetFocusedDataRow() as MaterialModel.MED_MATERIAL_MASTERRow;
            if (dr != null)
            {
                if (XtraMessageBox.Show("是否确认删除?", "提示", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    if (objMaterial.DeleteMaterialInfo(dr.MATERIAL_ID.ToString()) > 0)
                    {
                        loadMaterialMasterList();
                        XtraMessageBox.Show("删除成功!");
                    }
                    else
                    { XtraMessageBox.Show("删除失败!"); }
                }
            }
            else
            {
                XtraMessageBox.Show("未选择删除数据");
            }
        }

        private void gridView1_DoubleClick(object sender, EventArgs e)
        {
            var dr = gridView1.GetFocusedDataRow() as MaterialModel.MED_MATERIAL_MASTERRow;
            EditMaterialMaster frm = new EditMaterialMaster();
            frm.CurrentData = dr;
            frm._materialdtMaster = _materialDataTable;
            if (DialogResult.OK == frm.ShowDialog())
            {
                loadMaterialMasterList();
            }
        }
       #endregion

    }
}
