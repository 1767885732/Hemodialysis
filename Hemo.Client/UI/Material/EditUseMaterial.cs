/*----------------------------------------------------------------
 * Copyright (C) 2005 (苏州)医疗科技发展有限公司
 * 文件功能描述:用户控件类
 * 创建标识:吕志强-2013年7月9日
 * 
 * 修改时间:2013年10月17日
 * 修改人:吕志强
 * 修改描述:修改方法
 * 
 * 修改时间:2014年1月25日
 * 修改人:贺建操
 * 修改描述:修改方法SQL
 * 
 * 修改时间:2014年5月5日
 * 修改人:刘超
 * 修改描述:修改方法SQL
 * ----------------------------------------------------------------*/
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
using Hemo.IService.Dict;
using Hemo.Client.Print;
using DevExpress.XtraReports.UI;

namespace Hemo.Client.UI.Material {
    public partial class EditUseMaterial :HemoBaseFrm {

        #region 私有成员
        /// <summary>
        /// 药品主档数据服务对象
        /// </summary>
        private IMaterial objMaterial = ServiceManager.Instance.MaterialService;
        private IStaffDict _staffDictService = ServiceManager.Instance.StaffDictService;

        /// <summary>
        /// 是否新增
        /// </summary>
        private bool isAdd = true;
        #endregion

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="pHemoID"></param>
        /// <param name="pRecipeID"></param>
        public EditUseMaterial(string pHemoID, string pRecipeID) {
            InitializeComponent();
            ctlUserLongInfo.HEMODIALYSIS_ID = pHemoID;
            txtHEMODIALYSIS_ID.Text = pHemoID;
            ctlUserLongInfo.LoadPatientInfo();
            loadLookUpEditList();
            Hemo.Model.HemoModel.MED_COMPLICATION_OTHERDataTable dt = new HemoModel.MED_COMPLICATION_OTHERDataTable();
        }

        /// <summary>
        /// 载入下拉数据框 
        /// </summary>
        private void loadLookUpEditList() {
            //载入责任护士、穿刺护士、责任医生下拉框数据
            DataTable dtStaffSict = _staffDictService.GetStaffDictList();
            if (dtStaffSict != null && dtStaffSict.Rows.Count > 0) {
                DataTable dtPunctureNurseList = Utility.GetSubTable(dtStaffSict, "ZYNAME='护士'","name");
                if (dtPunctureNurseList != null && dtPunctureNurseList.Rows.Count > 0) {
                    BaseControlInfo.BindLookUpEdit(cmbPRIMARY_NURSE, "EMP_NO", "NAME", dtPunctureNurseList, "NAME", "录入人员");
                }
            }
        }
     /// <summary>
     /// 取消
     /// </summary>
     /// <param name="sender"></param>
     /// <param name="e"></param>
        private void btnCancel_Click(object sender, EventArgs e) {
            this.Close();
        }
        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSave_Click(object sender, EventArgs e) {
            if (IsDataValidate()) {
                if (saveData() == 1) {
                    XtraMessageBox.Show("保存成功", "血液透析耗材");
                    this.DialogResult = System.Windows.Forms.DialogResult.Yes;
                }
            }
        }

        #region 数据方法
        /// <summary>
        /// 保存之前判断数据输入是否合理 
        /// </summary>
        /// <returns></returns>
        private bool IsDataValidate() {
            if (cmbMACHINE_TYPE.Text.Length == 0) {
                cmbMACHINE_TYPE.Focus();
                errorProvider.SetError(cmbMACHINE_TYPE, "请选择透析器。");
                return false;
            }
            else {
                errorProvider.SetError(cmbMACHINE_TYPE, string.Empty);
            }
            return true;
        }
        /// <summary>
        /// 保存数据
        /// </summary>
        /// <returns></returns>
        private int saveData() {
            int result = 0;
            if (isAdd) {
                txtUSE_MATERIAL_ID.Text = System.Guid.NewGuid().ToString();
            }
            MaterialModel.MED_HEMO_MATERIALDataTable tmpDataTable = new MaterialModel.MED_HEMO_MATERIALDataTable();
            DataTable dt = BaseControlInfo.GetDataTableByPanel(tmpDataTable, pnlControls, isAdd);
            result = objMaterial.SaveHemoMaterialInfo((MaterialModel.MED_HEMO_MATERIALDataTable)dt);
            return result;
        }
        #endregion

        /// <summary>
        /// 打印
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnPrint_Click(object sender, EventArgs e) {
            //HemoScheduleReport report = new HemoScheduleReport(Utility.CDate(this.barEditReportDate.EditValue.ToString()));
            //this.printControl1.PrintingSystem = report.PrintingSystem;
            //report.CreateDocument();
            HemoMaterialReport report = new HemoMaterialReport(txtHEMODIALYSIS_ID.Text, "09b36fe3-bea8-44a5-b015-896ce0ee6bce");
            ReportPrintTool pt = new ReportPrintTool(report);
            pt.ShowPreviewDialog();

        }
    }
}