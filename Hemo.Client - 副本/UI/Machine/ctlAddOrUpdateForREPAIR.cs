/*----------------------------------------------------------------
// Copyright (C) 2005 苏州XX公司有限公司
// 描述：消毒设定用户控件类
// 创建时间：2016-08-3
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
using Hemo.Model;
using Hemo.Utilities;
using Hemo.IService.Config;
using Hemo.Service;
using Hemo.IService.Machine;
using Hemo.IService.Dict;

namespace Hemo.Client.UI.Machine
{
    [ToolboxItem(false)]
    public partial class ctlAddOrUpdateForREPAIR : DevExpress.XtraEditors.XtraUserControl
    {
        #region 类变量

        private IMachine machineService = ServiceManager.Instance.MachineService;

        private IStaffDict staffService = ServiceManager.Instance.StaffDictService;

        private IConfig configService = ServiceManager.Instance.ConfigService;

        private MachineModel.MED_EQUIPMENT_REPAIRDataTable _dt = null;

        private MachineModel.MED_EQUIPMENT_REPAIRRow _dr = null;

        private MachineModel.MED_DIALYSIS_MACHINEDataTable dtDialysisMachine = null;

        private MachineModel.MED_DIALYSIS_MACHINEDataTable dtMachineList = null;

        private bool _isAdd;

        private int _flag;

        #endregion

        #region 属性

        public MachineModel.MED_EQUIPMENT_REPAIRDataTable DT
        {
            set
            {
                _dt = value;

            }
            get
            {
                return _dt;
            }
        }
        public MachineModel.MED_EQUIPMENT_REPAIRRow DR
        {
            set
            {

                _dr = value;
            }
            get
            {
                return _dr;
            }
        }

        public int FLAG
        {
            set
            {
                _flag = value;
            }
            get
            {
                return _flag;
            }
        }

        public bool ISADD
        {
            set { _isAdd = value; }
            get { return _isAdd; }
        }

        #endregion

        #region 构造函数

        public ctlAddOrUpdateForREPAIR()
        {
            InitializeComponent();
        }

        #endregion

        #region 事件

        private void ctlAddOrUpdateForMgr_Load(object sender, EventArgs e)
        {
            DevExpress.Utils.AppearanceObject.ControlAppearance.Font = new System.Drawing.Font("Tahoma", 15);
            BindLookUpEdit();
            this.xtraTabControl1.SelectedTabPageIndex = _flag;
            BindData();
        }

        private void txtAREA_ID_EditValueChanged(object sender, EventArgs e)
        {
            var rows = dtDialysisMachine.Select("AREA_ID='" + this.txtAREA_ID.EditValue.ToString() + "'");
            DataTable dtSub = dtDialysisMachine.Clone();
            rows.AsEnumerable().ToList().ForEach(r => dtSub.ImportRow(r));
            if (dtSub.Rows.Count > 0)
            {
                this.txtMACHINE_ID.Properties.DataSource = dtSub;
            }
            string strSelect = string.Empty;
            strSelect = this.txtAREA_ID.EditValue == null ? string.Empty : "AREA_ID = '" + this.txtAREA_ID.EditValue.ToString() + "'";
            var dtBed = Utility.GetSubTable(dtMachineList, strSelect, "CWNO");
            Utility.BindLookUpEdit(this.txtBED_ID, "BED_ID", "CWNAME", dtBed, "CWNAME", "床位");
        }

        private void txtBED_ID_EditValueChanged(object sender, EventArgs e)
        {
            this.txtMACHINE_ID.EditValue = (this.txtBED_ID.GetSelectedDataRow() as DataRowView).Row["MACHINE_ID"].ToString();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            Save();
        }

        private void txtRECEIVER_EditValueChanged(object sender, EventArgs e)
        {
        }

        #endregion

        #region 方法

        void BindLookUpEdit()
        {
            if (_flag == 0)
            {

                dtMachineList = machineService.GetMachineList();
                dtDialysisMachine = machineService.GetMachineListByType("血透机品牌");
                // 病区
                ConfigModel.MED_COMMON_ITEMLISTDataTable dtArea = configService.GetConfigList(string.Empty, string.Empty, "区域", "1");
                if (dtArea != null && dtArea.Rows.Count > 0)
                {
                    Utility.BindLookUpEdit(this.txtAREA_ID, "ITEM_ID", "ITEM_NAME", (DataTable)dtArea, "ITEM_NAME", "区域");
                }
                this.txtAREA_ID.EditValue = string.Empty;

                //机器编号        
                var rows = dtDialysisMachine.Select("AREA_ID='" + this.txtAREA_ID.EditValue.ToString() + "'");
                DataTable dtSub = dtDialysisMachine.Clone();
                rows.AsEnumerable().ToList().ForEach(r => dtSub.ImportRow(r));
                if (dtSub.Rows.Count > 0)
                {
                    this.txtMACHINE_ID.Properties.DataSource = dtSub;
                }
                // 床位
                string strSelect = string.Empty;
                strSelect = this.txtAREA_ID.EditValue == null ? string.Empty : "AREA_ID = '" + this.txtAREA_ID.EditValue.ToString() + "'";
                var dtBed = Utility.GetSubTable(dtMachineList, strSelect, "CWNO");
                Utility.BindLookUpEdit(this.txtBED_ID, "BED_ID", "CWNAME", dtBed, "CWNAME", "床位");
                DataTable dtStaff = staffService.GetStaffDictList();
                dtStaff = Utility.GetSubTable(dtStaff, "ZYNAME='护士'", "NAME");
                Utility.BindLookUpEdit(this.txtREPAIRENGINEER, "USER_NAME", "NAME", dtStaff, "NAME", "人员");
            }
            else if (_flag == 1)
            {
                DataTable dtMachine = machineService.GetWaterMachineListByType("水处理机品牌");
                this.txtMACHINE_ID2.Properties.DataSource = dtMachine;
                DataTable dtStaff = staffService.GetStaffDictList();
                dtStaff = Utility.GetSubTable(dtStaff, "ZYNAME='护士'", "NAME");
                Utility.BindLookUpEdit(this.txtREPAIRENGINEER2, "USER_NAME", "NAME", dtStaff, "NAME", "人员");
                this.txtREPAIRENGINEER2.EditValue = string.Empty;
            }
            else if (_flag == 2)
            {

                DataTable dtStaff = staffService.GetStaffDictList();
                dtStaff = Utility.GetSubTable(dtStaff, "ZYNAME='护士'", "NAME");
                Utility.BindLookUpEdit(this.txtREPAIRENGINEER3, "USER_NAME", "NAME", dtStaff, "NAME", "人员");
                this.txtREPAIRENGINEER3.EditValue = string.Empty;
            }
        }

        void BindData()
        {
            if (_isAdd)  //新增
            {
                _dt = _dt == null ? new MachineModel.MED_EQUIPMENT_REPAIRDataTable() : _dt;
                _dr = _dt.NewMED_EQUIPMENT_REPAIRRow();
                _dr.ID = System.Guid.NewGuid().ToString();
                _dr.KIND = _flag.ToString();
                _dr.REPAIRTIME = System.DateTime.Now;
                _dt.AddMED_EQUIPMENT_REPAIRRow(_dr);
                if (_flag == 0)
                {
                    InitData();
                }
                else if (_flag == 1)
                {
                    InitData();
                    this.txtREPAIRTIME2.EditValue = _dr.REPAIRTIME;
                }
                else if (_flag == 2)
                {
                    InitData();
                    this.txtREPAIRTIME3.EditValue = _dr.REPAIRTIME;
                }
            }
            else //修改
            {
                if (_flag == 0)
                {
                    InitData();
                }
                else if (_flag == 1)
                {
                    InitData();
                    this.txtMACHINE_ID2.EditValue = _dr.MACHINE_ID;
                    this.txtMACHINE_MODEL2.Text = _dr.MACHINE_MODEL;
                    this.txtREPAIRENGINEER2.EditValue = _dr.REPAIRENGINEER;
                    this.txtREPAIRTIME2.EditValue = _dr.REPAIRTIME;
                }
                else if (_flag == 2)
                {
                    InitData();
                    this.txtREPAIRENGINEER3.EditValue = _dr.REPAIRENGINEER;
                    this.txtREPAIRTIME3.EditValue = _dr.REPAIRTIME;
                    this.txtDISINFECTIONMETHOD3.Text = _dr.DISINFECTIONMETHOD;

                }
            }
        }
        void InitData()
        {
            switch (_flag)
            {
                case 0:
                    foreach (var ctl in layoutControl1.Controls)
                    {
                        if (ctl is BaseEdit)
                        {
                            (ctl as BaseEdit).BindingDataRow(_dr, "txt");
                        }
                    }
                    break;
                case 1:
                    foreach (var ctl in layoutControl2.Controls)
                    {
                        if (ctl is BaseEdit)
                        {
                            (ctl as BaseEdit).BindingDataRow(_dr, "txt");
                        }
                    }
                    break;
                case 2:
                    foreach (var ctl in layoutControl3.Controls)
                    {
                        if (ctl is BaseEdit)
                        {
                            (ctl as BaseEdit).BindingDataRow(_dr, "txt");
                        }
                    }
                    break;
                default:
                    break;
            }
        }

        void Save()
        {
            if (!Check())
            {
                return;
            }
            if (_flag == 0)
            {
                _dr.AREA_ID = this.txtAREA_ID.EditValue.ToString();//触发不了只好写死
                _dr.MACHINE_ID = this.txtMACHINE_ID.EditValue.ToString();
                _dr.BED_ID = this.txtBED_ID.EditValue.ToString();

            }
            else if (_flag == 1)
            {

                _dr.MACHINE_ID = this.txtMACHINE_ID2.EditValue.ToString();
                _dr.MACHINE_MODEL = this.txtMACHINE_MODEL2.Text.ToString();
                _dr.REPAIRENGINEER = this.txtREPAIRENGINEER2.EditValue.ToString();
                _dr.REPAIRTIME = Utility.CDate(this.txtREPAIRTIME2.EditValue.ToString());
            }
            else if (_flag == 2)
            {
                _dr.REPAIRENGINEER = this.txtREPAIRENGINEER3.EditValue.ToString();
                _dr.REPAIRTIME = Utility.CDate(this.txtREPAIRTIME3.EditValue.ToString());
                _dr.DISINFECTIONMETHOD = this.txtDISINFECTIONMETHOD3.Text;
            }
            try
            {
                machineService.SaveMED_EQUIPMENT_REPAIR(_dt);
                XtraMessageBox.Show("保存成功", "提示");
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show("保存失败", "提示");
            }
        }

        bool Check()
        {
            var errorMsg = string.Empty;
            this.dxErrorProvider1.ClearErrors();
            if (_flag == 0)
            {
                if (txtAREA_ID.EditValue == null || String.IsNullOrEmpty(txtAREA_ID.EditValue.ToString()))
                {
                    errorMsg = "请选择病区";
                    dxErrorProvider1.SetError(txtAREA_ID, errorMsg);
                    return false;
                }
                if (txtBED_ID.EditValue == null || String.IsNullOrEmpty(txtBED_ID.EditValue.ToString()))
                {
                    errorMsg = "请选择床位";
                    dxErrorProvider1.SetError(txtBED_ID, errorMsg);
                    return false;
                }
                if (txtMACHINE_ID.EditValue == null || String.IsNullOrEmpty(txtMACHINE_ID.EditValue.ToString()))
                {
                    errorMsg = "请选择机器名称";
                    dxErrorProvider1.SetError(txtMACHINE_ID, errorMsg);
                    return false;
                }
                if (txtREPAIRTIME.EditValue == null || String.IsNullOrEmpty(txtREPAIRTIME.EditValue.ToString()))
                {
                    errorMsg = "请输入维修时间";
                    dxErrorProvider1.SetError(txtREPAIRTIME, errorMsg);
                    return false;
                }
                if (txtWORKTIME.EditValue == null || String.IsNullOrEmpty(txtWORKTIME.EditValue.ToString()))
                {
                    errorMsg = "请输入设备工作时间";
                    dxErrorProvider1.SetError(txtWORKTIME, errorMsg);
                    return false;
                }
            }
            else if (_flag == 1)
            {
                if (txtMACHINE_ID2.EditValue == null || String.IsNullOrEmpty(txtMACHINE_ID2.EditValue.ToString()))
                {
                    errorMsg = "请选择机器名称";
                    dxErrorProvider1.SetError(txtMACHINE_ID2, errorMsg);
                    return false;
                }
                if (txtREPAIRTIME2.EditValue == null || String.IsNullOrEmpty(txtREPAIRTIME2.EditValue.ToString()))
                {
                    errorMsg = "请输入消毒时间";
                    dxErrorProvider1.SetError(txtREPAIRTIME2, errorMsg);
                    return false;
                }

            }
            else if (_flag == 2)
            {
                if (txtREPAIRTIME3.EditValue == null || String.IsNullOrEmpty(txtREPAIRTIME3.EditValue.ToString()))
                {
                    errorMsg = "请输入消毒时间";
                    dxErrorProvider1.SetError(txtREPAIRTIME3, errorMsg);
                    return false;
                }
            }
            return true;
        }

        #endregion
    }
}
