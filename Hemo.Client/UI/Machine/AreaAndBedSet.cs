/*----------------------------------------------------------------
// Copyright (C) 2005 苏州XX公司有限公司
// 描述：床位区域设定窗体
// 创建时间：2015-08-3
// 创建者：刘超
//  
// 修改时间：
// 修改人：
// 修改描述：
----------------------------------------------------------------*/

using System;
using System.Data;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Hemo.IService.Config;
using Hemo.IService.Machine;
using Hemo.Model;
using Hemo.Service;
using Hemo.Utilities;
using Hemo.Client.Core;
using Medicalsystem.Auth.Client;

namespace Hemo.Client.UI.Machine {
    public partial class AreaAndBedSet : HemoBaseFrm {
        #region 变量

        private string defaultStr4Sel = "请选择";
        private MachineModel.MED_DIALYSIS_MACHINEDataTable _machineDataTable;
        private MachineModel.MED_DIALYSIS_MACHINERow _machineDictRow;
        private IConfig _configService = ServiceManager.Instance.ConfigService;
        private IMachine _machineService = ServiceManager.Instance.MachineService;

        #endregion

        #region 构造函数

        public AreaAndBedSet(MachineModel.MED_DIALYSIS_MACHINEDataTable machineDataTable, MachineModel.MED_DIALYSIS_MACHINERow machineDictRow) {
            this.InitializeComponent();

            this._machineDataTable = machineDataTable;
            this._machineDictRow = machineDictRow;
        }

        #endregion

        #region 方法

        private void InitializeControls() {
            Utility.BindLookUpEdit(this.cbxAREA, "ITEM_ID", "ITEM_NAME", this._configService.GetConfigList(string.Empty, string.Empty, "区域", "1"), "ITEM_NAME", "区域");

            Utility.BindLookUpEdit(this.cbxBED, "ITEM_ID", "ITEM_NAME", this._configService.GetConfigList(string.Empty, string.Empty, "床位", "1"), "ITEM_NAME", "床位");

            Utility.AddEmptyItem(this.cbxAREA, defaultStr4Sel);

            Utility.AddEmptyItem(this.cbxBED, defaultStr4Sel);

            if (this._machineDictRow != null) //修改
            {
                if (this._machineDictRow.IsAREA_IDNull() && this._machineDictRow.IsBED_IDNull())
                    this.cbxAREA.Text = this.cbxBED.Text = this.defaultStr4Sel;
                else {
                    this.cbxAREA.EditValue = this._machineDictRow.AREA_ID;
                    this.cbxBED.EditValue = this._machineDictRow.BED_ID;
                }
            }
        }

        private bool IsDataValidate() {
            this.errorProvider.ClearErrors();
            this.errorProvider.SetError(this.cbxAREA, string.Empty);
            this.errorProvider.SetError(this.cbxBED, string.Empty);

            bool isSelArea = string.Compare(this.cbxAREA.Text, this.defaultStr4Sel, true) != 0;
            bool isSelBed = string.Compare(this.cbxBED.Text, this.defaultStr4Sel, true) != 0;

            if ((isSelArea && !isSelBed) || (!isSelArea && isSelBed)) {

               

                if (isSelArea) {
                    this.cbxBED.Focus();

                    this.errorProvider.SetError(this.cbxBED, "区域和床位两者必须同时选择！");

                    return false;
                }
                else {
                    this.cbxAREA.Focus();

                    this.errorProvider.SetError(this.cbxAREA, "区域和床位两者必须同时选择！");

                    return false;
                }
               
            }

            #region MyRegion
            
            //if ((isSelArea && isSelBed))
            //{

            //    IMachine _machineService = ServiceManager.Instance.MachineService;
            //    var _machineDataTable = _machineService.GetNewMachineList();
            //    var data = new MachineModel.MED_DIALYSIS_MACHINEDataTable();
            //    _machineDataTable.Where(i => !string.IsNullOrEmpty(i.AREA_ID) && !string.IsNullOrEmpty(i.BED_ID)).CopyToDataTable<MachineModel.MED_DIALYSIS_MACHINERow>(data, LoadOption.PreserveChanges);
            //    if (data != null && data.Rows.Count > 0)
            //    {
            //        int bedCount = data.Rows.Count + 1;
            //        int dogCount = Utilities.Utility.GetHospitalBedCount();
            //        if (bedCount > dogCount)
            //        {
            //            if (DAuthContext.Current.HospitalID != 1)
            //            {
            //                this.errorProvider.SetError(cbxBED, string.Format(Utilities.Utility.dogTipStr1,dogCount));
            //            }
            //            else
            //            {
            //                this.errorProvider.SetError(cbxBED, Utilities.Utility.dogTipStr);
            //            }
            //            return false;
            //        }
            //    }
            //}

            #endregion
          
           
            return true;
        }

        #endregion

        #region 事件
        private void simpleButton2_Click(object sender, EventArgs e) {
            this.Close();
        }

        private void AreaAndBedSet_Load(object sender, EventArgs e) {
            this.InitializeControls();
        }

        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSave_Click(object sender, EventArgs e) {
            if (!this.IsDataValidate())
                return;

            DataRow[] rows = this._machineDataTable.Select(string.Format("AREA_ID = '{0}' AND BED_ID = '{1}'", this.cbxAREA.EditValue.ToString(), this.cbxBED.EditValue.ToString()));

            if (rows.Length > 0 && string.Compare(rows[0]["MACHINE_ID"].ToString(), this._machineDictRow.MACHINE_ID, true) != 0)
                if (XtraMessageBox.Show("该区域和床位已被其他血透机使用，是否替换成当前血透机？", this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                    rows[0]["AREA_ID"] = rows[0]["BED_ID"] = string.Empty;
                else
                    return;

            this._machineDictRow.AREA_ID = this.cbxAREA.EditValue.ToString();
            this._machineDictRow.BED_ID = this.cbxBED.EditValue.ToString();

            this._machineService.SaveMachineInfo(this._machineDataTable);
            AutoClosedMsgBox.ShowForm("保存成功。", "系统提示", 1500, MessageBoxIcon.Information);

            //XtraMessageBox.Show("保存成功！");

            this.DialogResult = DialogResult.Yes;
        }

        #endregion
    }
}
