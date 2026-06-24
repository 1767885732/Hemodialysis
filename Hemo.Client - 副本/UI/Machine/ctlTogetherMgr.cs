/*----------------------------------------------------------------
// Copyright (C) 2005 苏州XX公司有限公司
// 描述：集中液管理户控件类
// 创建时间：2016-06-12
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

namespace Hemo.Client.UI.Machine
{
    public partial class ctlTogetherMgr :ViewBase
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

        private const int FLAG = 2;

        private DataTable dtStaff;

        #endregion

        #region 属性

        #endregion

        #region 构造函数

        public ctlTogetherMgr()
        {
            InitializeComponent();
        }

        #endregion

        #region 事件

        private void ctlEquipmentMgr_Load(object sender, EventArgs e)
        {
            this.beginTime.EditValue = System.DateTime.Now.AddDays(-7);
            this.endTime.EditValue = System.DateTime.Now;
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
        { Utility.BindLookUpEdit(this.lupREPAIRENGINEER3, "USER_NAME", "NAME", dtStaff, "NAME", "人员"); }

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
