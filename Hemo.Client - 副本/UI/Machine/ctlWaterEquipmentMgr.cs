/*----------------------------------------------------------------
// Copyright (C) 2005 苏州XX公司有限公司
// 描述：水处理机管理户控件类
// 创建时间：2016-06-16
// 创建者：贺建操
//  
// 修改时间：
// 修改人：
// 修改描述：
----------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Hemo.IService.Machine;
using Hemo.Service;
using Hemo.IService.Dict;
using Hemo.IService.Config;
using Hemo.Model;
using DevExpress.XtraBars.Docking2010.Customization;
using Hemo.Utilities;
using Hemo.Client.Print;
using DevExpress.XtraReports.UI;

namespace Hemo.Client.UI.Machine
{
    public partial class ctlWaterEquipmentMgr :ViewBase
    {
        #region 类变量

        private string areaId = string.Empty;

        private IMachine machineService = ServiceManager.Instance.MachineService;

        private IStaffDict staffService = ServiceManager.Instance.StaffDictService;

        private IConfig configService = ServiceManager.Instance.ConfigService;

        private MachineModel.MED_EQUIPMENT_MGRDataTable dtRecordMgr = null;

        private MachineModel.MED_EQUIPMENT_MGRRow drRecordMgr = null;

        private MachineModel.MED_EQUIPMENT_MAINTENANCEDataTable dtRecordMAINTENANCE = null;

        private MachineModel.MED_EQUIPMENT_MAINTENANCERow drRecordMAINTENANCE = null;

        private MachineModel.MED_EQUIPMENT_REPAIRDataTable dtRecordREPAIR = null;

        private MachineModel.MED_EQUIPMENT_REPAIRRow drRecordREPAIR = null;

        private const int FLAG = 1;

        private MachineModel.MED_DIALYSIS_MACHINEDataTable dtMachineList;
        private MachineModel.MED_DIALYSIS_MACHINEDataTable dtDialysisMachine;
        private DataTable dtStaff;
        private ConfigModel.MED_COMMON_ITEMLISTDataTable dtArea;
        private DataTable dtMachine;

        #endregion

        #region 属性

        #endregion

        #region 构造函数

        public ctlWaterEquipmentMgr()
        {
            InitializeComponent();
        }

        #endregion

        #region 事件

        private void ctlEquipmentMgr_Load(object sender, EventArgs e)
        {
            this.beginTime.EditValue = System.DateTime.Now.AddDays(-7);
            this.endTime.EditValue = System.DateTime.Now;
            dtMachineList = machineService.GetMachineList();
            dtMachine = machineService.GetWaterMachineListByType("水处理机品牌");
            dtDialysisMachine = machineService.GetMachineListByType("血透机品牌");
            dtStaff = staffService.GetStaffDictList();
            dtStaff = Utility.GetSubTable(dtStaff, "ZYNAME='护士'", "NAME");
            BindLup();
            Query();
        }

        private void gridView1_DoubleClick(object sender, EventArgs e)
        {
            this.drRecordMgr = (MachineModel.MED_EQUIPMENT_MGRRow)this.gridView1.GetFocusedDataRow();
            if (drRecordMgr == null)
            {
                return;
            }
            var ctl = new ctlAddOrUpdateForMgr();
            ctl.DT = this.dtRecordMgr;
            ctl.DR = this.drRecordMgr;
            ctl.FLAG = FLAG;
            ctl.ISADD = false;
            FlyoutDialog.Show(this.FindForm(), ctl);
        }

        private void gridView2_DoubleClick(object sender, EventArgs e)
        {
            this.drRecordMAINTENANCE = (MachineModel.MED_EQUIPMENT_MAINTENANCERow)this.gridView2.GetFocusedDataRow();
            if (drRecordMAINTENANCE == null)
            {
                return;
            }
            var ctl = new ctlAddOrUpdateForMAINTENANCE();
            ctl.DT = this.dtRecordMAINTENANCE;
            ctl.DR = this.drRecordMAINTENANCE;
            ctl.FLAG = FLAG;
            ctl.ISADD = false;
            FlyoutDialog.Show(this.FindForm(), ctl);
        }

        private void gridView3_DoubleClick(object sender, EventArgs e)
        {
            this.drRecordREPAIR = (MachineModel.MED_EQUIPMENT_REPAIRRow)this.gridView3.GetFocusedDataRow();
            if (drRecordREPAIR == null)
            {
                return;
            }
            var ctl = new ctlAddOrUpdateForREPAIR();
            ctl.DT = this.dtRecordREPAIR;
            ctl.DR = this.drRecordREPAIR;
            ctl.FLAG = FLAG;
            ctl.ISADD = false;
            FlyoutDialog.Show(this.FindForm(), ctl);
        }

        private void xtraTabControl1_SelectedPageChanged(object sender, DevExpress.XtraTab.TabPageChangedEventArgs e)
        {
            BindLup();
            Query();
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            DevExpress.XtraSplashScreen.SplashScreenManager.ShowForm(this.ParentForm.FindForm(), typeof(SplashScreen1));
            DevExpress.XtraSplashScreen.SplashScreenManager.Default.SendCommand(SplashScreen1.SplashScreenCommand.SetText, string.Format("报表加载中，请稍候。。。"));
            try
            {
                var tempdt2 = machineService.SelectMED_EQUIPMENT_MAINTENANCEByTime(Utility.CDate(this.beginTime.EditValue.ToString()), Utility.CDate(this.endTime.EditValue.ToString()));
                var tempdt3 = machineService.SelectMED_EQUIPMENT_REPAIRByTime(Utility.CDate(this.beginTime.EditValue.ToString()), Utility.CDate(this.endTime.EditValue.ToString()));
                var dt2 = new MachineModel.MED_EQUIPMENT_MAINTENANCEDataTable();
                var dt3 = new MachineModel.MED_EQUIPMENT_REPAIRDataTable();
                var row = this.gridView1.GetFocusedDataRow() as MachineModel.MED_EQUIPMENT_MGRRow;
                if (row == null)
                {
                    DevExpress.XtraSplashScreen.SplashScreenManager.CloseForm();
                    XtraMessageBox.Show("请在水处理机登记里选择需要打印的水处理机", "提示");
                    return;
                }
                var MACHINE_ID = row.MACHINE_ID.ToString();
                tempdt2.AsEnumerable().Where(i => i.KIND.Equals(FLAG.ToString()) && i.MACHINE_ID.Equals(MACHINE_ID)).CopyToDataTable(dt2, LoadOption.PreserveChanges);
                tempdt3.AsEnumerable().Where(i => i.KIND.Equals(FLAG.ToString()) && i.MACHINE_ID.Equals(MACHINE_ID)).CopyToDataTable(dt3, LoadOption.PreserveChanges);
                var newMACHINE_ID = dtMachine.AsEnumerable().FirstOrDefault(i => i["MACHINE_ID"].Equals(MACHINE_ID))["FLNAME"].ToString();
                row.MACHINE_ID = newMACHINE_ID;
                foreach (MachineModel.MED_EQUIPMENT_MAINTENANCERow dt2row in dt2.Rows)
                {
                    var x = dtStaff.AsEnumerable().FirstOrDefault(i => i["USER_NAME"].Equals(dt2row.MAINTAINENGINEER));
                    if (x != null)
                    {
                        dt2row.MAINTAINENGINEER = x["NAME"].ToString();
                    }
                }

                foreach (MachineModel.MED_EQUIPMENT_REPAIRRow dt3row in dt3.Rows)
                {
                    var x = dtStaff.AsEnumerable().FirstOrDefault(i => i["USER_NAME"].Equals(dt3row.REPAIRENGINEER));
                    if (x != null)
                    {
                        dt3row.REPAIRENGINEER = x["NAME"].ToString();
                    }
                }
                DevExpress.XtraSplashScreen.SplashScreenManager.CloseForm();
                WaterEquipmentMgrRpt rpt = new WaterEquipmentMgrRpt(row, dt2, dt3);
                ReportPrintTool pt = new ReportPrintTool(rpt);
                pt.ShowPreviewDialog();
                this.xtraTabControl1.SelectedTabPageIndex = 0;
                Query();
            }
            catch (Exception ex)
            {
                DevExpress.XtraSplashScreen.SplashScreenManager.CloseForm();
                XtraMessageBox.Show("导出报表失败！", "提示");
                this.xtraTabControl1.SelectedTabPageIndex = 0;
                Query();

            }
        }

        private void btnDocMgr_Click(object sender, EventArgs e)
        {
            var row = this.gridView1.GetFocusedDataRow() as MachineModel.MED_EQUIPMENT_MGRRow;
            if (row == null)
            {
                DevExpress.XtraSplashScreen.SplashScreenManager.CloseForm();
                XtraMessageBox.Show("请在水处理机登记里选择需要上传文档的水处理", "提示");
                return;
            }
            FlyoutDialog.Show(this.FindForm(), new ctlMachineUI(this.dtRecordMgr, row));
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (this.xtraTabControl1.SelectedTabPage == this.xtp_register)
            {
                this.drRecordMgr = (MachineModel.MED_EQUIPMENT_MGRRow)this.gridView1.GetFocusedDataRow();
                if (drRecordMgr == null)
                {
                    return;
                }
                var ctl = new ctlAddOrUpdateForMgr();
                ctl.DT = this.dtRecordMgr;
                ctl.DR = this.drRecordMgr;
                ctl.FLAG = FLAG;
                ctl.ISADD = false;
                FlyoutDialog.Show(this.FindForm(), ctl);
            }
            else if (this.xtraTabControl1.SelectedTabPage == this.xtp_protect)
            {
                this.drRecordMAINTENANCE = (MachineModel.MED_EQUIPMENT_MAINTENANCERow)this.gridView2.GetFocusedDataRow();
                if (drRecordMAINTENANCE == null)
                {
                    return;
                }
                var ctl = new ctlAddOrUpdateForMAINTENANCE();
                ctl.DT = this.dtRecordMAINTENANCE;
                ctl.DR = this.drRecordMAINTENANCE;
                ctl.FLAG = FLAG;
                ctl.ISADD = false;
                FlyoutDialog.Show(this.FindForm(), ctl);
            }
            else if (this.xtraTabControl1.SelectedTabPage == this.xtp_repair)
            {
                this.drRecordREPAIR = (MachineModel.MED_EQUIPMENT_REPAIRRow)this.gridView3.GetFocusedDataRow();
                if (drRecordREPAIR == null)
                {
                    return;
                }
                var ctl = new ctlAddOrUpdateForREPAIR();
                ctl.DT = this.dtRecordREPAIR;
                ctl.DR = this.drRecordREPAIR;
                ctl.FLAG = FLAG;
                ctl.ISADD = false;
                FlyoutDialog.Show(this.FindForm(), ctl);
            }
        }
        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (this.xtraTabControl1.SelectedTabPage == this.xtp_register)
            {

                var ctl = new ctlAddOrUpdateForMgr();
                ctl.DT = this.dtRecordMgr;
                ctl.DR = null;
                ctl.FLAG = FLAG;
                ctl.ISADD = true;
                FlyoutDialog.Show(this.FindForm(), ctl);
            }
            else if (this.xtraTabControl1.SelectedTabPage == this.xtp_protect)
            {
                var ctl = new ctlAddOrUpdateForMAINTENANCE();
                ctl.DT = this.dtRecordMAINTENANCE;
                ctl.DR = null;
                ctl.FLAG = FLAG;
                ctl.ISADD = true;
                FlyoutDialog.Show(this.FindForm(), ctl);
            }
            else if (this.xtraTabControl1.SelectedTabPage == this.xtp_repair)
            {
                var ctl = new ctlAddOrUpdateForREPAIR();
                ctl.DT = this.dtRecordREPAIR;
                ctl.DR = null;
                ctl.FLAG = FLAG;
                ctl.ISADD = true;
                FlyoutDialog.Show(this.FindForm(), ctl);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (this.xtraTabControl1.SelectedTabPage == xtp_register)
            {
                this.drRecordMgr = (MachineModel.MED_EQUIPMENT_MGRRow)this.gridView1.GetFocusedDataRow();
                if (this.drRecordMgr == null) return;
                if (DialogResult.OK == MessageBox.Show("是否确认删除？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning))
                    machineService.DeleteMED_EQUIPMENT_MGR(drRecordMgr.ID);

            }
            else if (this.xtraTabControl1.SelectedTabPage == xtp_protect)
            {
                this.drRecordMAINTENANCE = (MachineModel.MED_EQUIPMENT_MAINTENANCERow)this.gridView2.GetFocusedDataRow();
                if (this.drRecordMAINTENANCE == null) return;
                if (DialogResult.OK == MessageBox.Show("是否确认删除？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning))
                    machineService.DeleteMED_EQUIPMENT_MAINTENANCE(drRecordMAINTENANCE.ID);
            }
            else if (this.xtraTabControl1.SelectedTabPage == xtp_repair)
            {
                this.drRecordREPAIR = (MachineModel.MED_EQUIPMENT_REPAIRRow)this.gridView3.GetFocusedDataRow();
                if (this.drRecordREPAIR == null) return;
                if (DialogResult.OK == MessageBox.Show("是否确认删除？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning))
                    machineService.DeleteMED_EQUIPMENT_REPAIR(drRecordREPAIR.ID);
            }
            Query();
        }

        private void btnPrintSimple_Click(object sender, EventArgs e)
        {

        }
        private void btnQuery_Click(object sender, EventArgs e)
        {
            Query();
        }

        #endregion

        #region 方法

        void BindLup()
        {
            if (this.xtraTabControl1.SelectedTabPage == xtp_register)
            {
                this.lupMachine.DataSource = dtMachine;
                //病区             
                if (dtArea != null && dtArea.Rows.Count > 0)
                {
                    Utility.BindLookUpEdit(this.lupAREA_ID, "ITEM_ID", "ITEM_NAME", (DataTable)dtArea, "ITEM_NAME", "区域");
                }
                //床位
                Utility.BindLookUpEdit(this.lupBED_ID, "BED_ID", "CWNAME", dtMachineList, "CWNAME", "床位");
                Utility.BindLookUpEdit(this.lupRECEIVER, "USER_NAME", "NAME", dtStaff, "NAME", "人员");
            }
            else if (this.xtraTabControl1.SelectedTabPage == xtp_protect)
            {
                this.lupMachine2.DataSource = dtMachine;
                //病区             
                if (dtArea != null && dtArea.Rows.Count > 0)
                {
                    Utility.BindLookUpEdit(this.lupAREA_ID2, "ITEM_ID", "ITEM_NAME", (DataTable)dtArea, "ITEM_NAME", "区域");
                }
                //床位
                Utility.BindLookUpEdit(this.lupBED_ID2, "BED_ID", "CWNAME", dtMachineList, "CWNAME", "床位");
                Utility.BindLookUpEdit(this.lupMAINTAINENGINEER2, "USER_NAME", "NAME", dtStaff, "NAME", "人员");
            }
            else if (this.xtraTabControl1.SelectedTabPage == xtp_repair)
            {
                this.lupMachine3.DataSource = dtMachine;
                //病区             
                if (dtArea != null && dtArea.Rows.Count > 0)
                {
                    Utility.BindLookUpEdit(this.lupAREA_ID3, "ITEM_ID", "ITEM_NAME", (DataTable)dtArea, "ITEM_NAME", "区域");
                }
                //床位
                Utility.BindLookUpEdit(this.lupBED_ID3, "BED_ID", "CWNAME", dtMachineList, "CWNAME", "床位");
                Utility.BindLookUpEdit(this.lupREPAIRENGINEER3, "USER_NAME", "NAME", dtStaff, "NAME", "人员");
            }
        }
        void Query()
        {
            if (this.xtraTabControl1.SelectedTabPage == xtp_register)
            {
                var tempdt = machineService.SelectMED_EQUIPMENT_MGRByTime(Utility.CDate(this.beginTime.EditValue.ToString()), Utility.CDate(this.endTime.EditValue.ToString()));
                dtRecordMgr = new MachineModel.MED_EQUIPMENT_MGRDataTable();
                tempdt.AsEnumerable().Where(i => i.KIND.Equals(FLAG.ToString())).CopyToDataTable(dtRecordMgr, LoadOption.PreserveChanges);

                this.gridControl1.DataSource = dtRecordMgr;
            }
            else if (this.xtraTabControl1.SelectedTabPage == xtp_protect)
            {
                var tempdt = machineService.SelectMED_EQUIPMENT_MAINTENANCEByTime(Utility.CDate(this.beginTime.EditValue.ToString()), Utility.CDate(this.endTime.EditValue.ToString()));
                dtRecordMAINTENANCE = new MachineModel.MED_EQUIPMENT_MAINTENANCEDataTable();
                tempdt.AsEnumerable().Where(i => i.KIND.Equals(FLAG.ToString())).CopyToDataTable(dtRecordMAINTENANCE, LoadOption.PreserveChanges);
                this.gridControl2.DataSource = dtRecordMAINTENANCE;
                this.gridView2.BestFitColumns();
            }

            else if (this.xtraTabControl1.SelectedTabPage == xtp_repair)
            {
                var tempdt = machineService.SelectMED_EQUIPMENT_REPAIRByTime(Utility.CDate(this.beginTime.EditValue.ToString()), Utility.CDate(this.endTime.EditValue.ToString()));
                dtRecordREPAIR = new MachineModel.MED_EQUIPMENT_REPAIRDataTable();
                tempdt.AsEnumerable().Where(i => i.KIND.Equals(FLAG.ToString())).CopyToDataTable(dtRecordREPAIR, LoadOption.PreserveChanges);
                this.gridControl3.DataSource = dtRecordREPAIR;
            }
        }

        #endregion
    }
}
