/*----------------------------------------------------------------
// Copyright (C) 2013 苏州XX公司
// 描述：耗材列表展示
// 创建时间：2016-04-05
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
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraBars.Docking2010.Customization;
using Hemo.Client.UI.Machine;
using Hemo.IService;
using Hemo.Service;
using Hemo.Client.UI.Material;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;

namespace Hemo.Client.UI.Patient
{
    /// <summary>
    /// 界面的基类
    /// </summary>
    [ToolboxItem(true)]
    public partial class PatientMaterialCureUI : ViewBase
    {
        #region 类变量

        private IPatient patientService = ServiceManager.Instance.PatientService;

        #endregion

        #region 属性

        #endregion

        #region 构造函数

        public PatientMaterialCureUI()
        {
            InitializeComponent();
        }

        #endregion

        #region 事件

        private void btnEdit_Click(object sender, EventArgs e)
        {
            PatientMaterialMode recordDetail = new PatientMaterialMode();
            DialogResult result = recordDetail.ShowDialog();
        }

        private void btnDefaultItem_Click(object sender, EventArgs e)
        {
            using (HemoDefaultModeManagementView DefaultItem = new HemoDefaultModeManagementView())
            {
                var diagresult = FlyoutDialog.Show(this.FindForm(), DefaultItem);
                if (diagresult == System.Windows.Forms.DialogResult.OK)
                {
                    //this.Close();
                }
            }
        }

        private void gridView2_CustomDrawGroupRow(object sender, DevExpress.XtraGrid.Views.Base.RowObjectCustomDrawEventArgs e)
        {
            //GridGroupRowInfo gridGroupRowInfo = e.Info as GridGroupRowInfo;
            //gridGroupRowInfo.GroupText = string.Format();
        }

        #endregion

        #region 方法

        /// <summary>
        /// 根据治疗方式加载默认模板数据
        /// </summary>
        /// <param name="cureMethond"></param>
        public void InzationData(string cureMethond)
        {
            this.grdProduct.DataSource = null;

            //获取治疗方式对应的默认模板
            var deFaultData = patientService.GetHemoDefaultModels(cureMethond);
            if (deFaultData == null || deFaultData.Rows.Count <= 0)
            {
                return;
            }

            //根据默认模板获取模板数据
            var materialName = deFaultData[0].MATERIAL_MODEL_NAME.ToString();
            this.busyIndicator1.ShowLoadingScreenFor(this.grdProduct);
            using (BackgroundWorker worker = new BackgroundWorker())
            {
                DataTable ModelDt = null;
                worker.DoWork += delegate(object sender1, DoWorkEventArgs e1)
                {
                    ModelDt = patientService.QueryMaterialModelByParams(materialName);
                };
                worker.RunWorkerCompleted += delegate(object sender2, RunWorkerCompletedEventArgs e2)
                {
                    this.grdProduct.DataSource = ModelDt;

                    //for (int i = -this.gridView2.GroupCount; i < 0; i++)
                    //{
                    this.gridView2.ExpandAllGroups();

                    //}
                    this.busyIndicator1.HideLoadingScreen();
                };
                worker.RunWorkerAsync();
            }
        }

        #endregion
    }
}
