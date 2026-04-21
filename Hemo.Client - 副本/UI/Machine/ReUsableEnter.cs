/*----------------------------------------------------------------
// Copyright (C) 2005 苏州XX公司有限公司
// 描述：血透室透析器复用情况记录窗体类
// 创建时间：2015-06-16
// 创建者：刘超
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
using Hemo.Model;
using Hemo.Utilities;
using Hemo.IService.Config;
using Hemo.Service;
using Hemo.IService.Dict;
using Hemo.IService.Machine;

namespace Hemo.Client.UI.Machine
{
    public partial class ReUsableEnter :HemoBaseFrm
    {
        #region 类变量

        private PatientModel.MED_PATIENTSRow _patientRow;
        private IConfig _configService = ServiceManager.Instance.ConfigService;
        private IStaffDict _staffDictService = ServiceManager.Instance.StaffDictService;
        private MachineModel.MED_MACHINE_REUSABLEDataTable _reUsable = null;
        private MachineModel.MED_MACHINE_REUSABLERow _current = null;
        private IMachine _machineService = ServiceManager.Instance.MachineService;

        #endregion

        #region 属性

        /// <summary>
        /// 获取或者设置当前的患者
        /// </summary>
        public MachineModel.MED_MACHINE_REUSABLERow Current
        {
            get
            {
                return _current;
            }
            set
            {
                _current = value;
                if (value != null)
                {
                    // this.txtPATIENT_ID.Enabled = false;
                    //  this.txtNAME.Enabled = false;
                }
            }
        }    

        #endregion

        #region 构造函数

        #endregion

        #region 事件

        private void btn_Cancle_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        private void btn_SAVE_Click(object sender, EventArgs e)
        {
            if (IsDataValidate())
            {
                try
                {
                    if (SaveData() > 0)
                    {
                        XtraMessageBox.Show("保存成功。", "复用记录");
                        this.DialogResult = System.Windows.Forms.DialogResult.OK;
                    }

                }
                catch (Exception ex)
                {
                    XtraMessageBox.Show(ex.Message, "复用记录");
                }
            }
        }

        private void ReUsableEnter_Load(object sender, EventArgs e)
        {
            InitalizeData();
        }

        private void radio_PROGRAMCHECK_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.radio_PROGRAMCHECK.Tag = this.radio_PROGRAMCHECK.SelectedIndex;
        }

        private void radio_FLUXCHECK_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.radio_FLUXCHECK.Tag = this.radio_FLUXCHECK.SelectedIndex;
        }

        #endregion

        #region 方法

        public ReUsableEnter(PatientModel.MED_PATIENTSRow patientRow)
        {
            InitializeComponent();
            this._patientRow = patientRow;
            this.lab_UerInfos.Text = string.Format("姓名:{0}                   性别:{1}                     病案号:{2}                      病人ID:{3}", this._patientRow.NAME, this._patientRow.SEX, this._patientRow.HEMODIALYSIS_ID, this._patientRow.PATIENT_ID);
            BaseControlInfo.BindLookUpEdit(lupMACHINE_TYPE, "ITEM_ID", "ITEM_NAME", this._configService.GetConfigList(string.Empty, string.Empty, "净化器类型", "1"), "ITEM_NAME", "净化器类型");
            DataTable dtStaffSict = _staffDictService.GetStaffDictList();
            if (dtStaffSict != null && dtStaffSict.Rows.Count > 0)
            {
                DataTable dtPunctureNurseList = Utility.GetSubTable(dtStaffSict, "ZYNAME='护士'", "name");
                if (dtPunctureNurseList != null && dtPunctureNurseList.Rows.Count > 0)
                {
                    BaseControlInfo.BindLookUpEdit(cmbPRIMARY_NURSE, "EMP_NO", "NAME", dtPunctureNurseList, "NAME", "工作人员");
                }
            }
        }

        /// <summary>
        /// 保存之前判断数据输入是否合理 
        /// </summary>
        /// <returns></returns>
        private bool IsDataValidate()
        {
            bool result = true;
            int iDate = 0;
            if (lupMACHINE_TYPE.EditValue.ToString().Length <= 0)
            {
                errorProvider.SetError(lupMACHINE_TYPE, "请选择透析器型号");
                this.lupMACHINE_TYPE.Focus();
                return false;

            }
            if (FirstUseTime.Text.Length > 0)
            {
                iDate = Utility.CDate(FirstUseTime.Text).CompareTo(Utility.CDate(FirstUseTime.Text));
                if (iDate > 0)
                {
                    FirstUseTime.Focus();
                    errorProvider.SetError(FirstUseTime, "请输入正确的入院时间。");
                    return false;
                }

            }

            if (DateAndTimeEdit.Text.Length > 0)
            {
                iDate = Utility.CDate(DateAndTimeEdit.Text).CompareTo(Utility.CDate(DateAndTimeEdit.Text));
                if (iDate > 0)
                {
                    DateAndTimeEdit.Focus();
                    errorProvider.SetError(DateAndTimeEdit, "请输入正确的入院时间。");
                    return false;
                }
            }
            if (cmbPRIMARY_NURSE.Text.Length <= 0)
            {
                cmbPRIMARY_NURSE.Focus();
                errorProvider.SetError(cmbPRIMARY_NURSE, "请选择工作人员签名。");
                return false;
            }

            if (txt_TCVCHECK.Text.Length <= 0)
            {
                txt_TCVCHECK.Focus();
                errorProvider.SetError(txt_TCVCHECK, "TCV容积检测");
                return false;
            }
            return result;
        }

        /// <summary>
        /// 病人数据保存方法  
        /// </summary>
        /// <returns></returns>
        private int SaveData()
        {
            this.medReUsableBinding.EndEdit();
            this.medReUsableBinding.CurrencyManager.EndCurrentEdit();

            var row = _reUsable[0];
            if (row.ID.Length <= 0)
            {
                row.ID = Guid.NewGuid().ToString();
            }
            row.CREATEDATE = System.DateTime.Now;
            row.ISDELETE = "0";
            return _machineService.SaveReUsableDatas(_reUsable);

        }

        /// <summary>
        /// 初使化数据
        /// </summary>
        private void InitalizeData()
        {
            this.Enabled = false;

            _reUsable = new MachineModel.MED_MACHINE_REUSABLEDataTable();
            using (BackgroundWorker worker = new BackgroundWorker())
            {
                worker.DoWork += delegate(object sender1, DoWorkEventArgs e1)
                {

                    if (_current != null)
                    {
                        //_reUsable.AddMED_MACHINE_REUSABLERow(_current);

                        _reUsable.LoadDataRow(_current.ItemArray, true);

                    }
                    else
                    {
                        _reUsable = new MachineModel.MED_MACHINE_REUSABLEDataTable();
                    }

                };
                worker.RunWorkerCompleted += delegate(object sender2, RunWorkerCompletedEventArgs e2)
                {
                    this.medReUsableBinding.DataSource = _reUsable;
                    if (_current == null)
                    {
                        this.medReUsableBinding.AddNew();
                        this.txt_DISINFECTANTLD.Text = "合格";
                        this.txt_DIALYZERLAB.Text = "完整";
                        this.txt_DISINFECTANTCL.Text = "合格";
                        this.txt_PREDIAPPEARANCE.Text = "好";
                        this.txt_BACKDIAPPEARANCE.Text = "好";
                        this.radio_PROGRAMCHECK.SelectedIndex = 1;
                        this.radio_FLUXCHECK.SelectedIndex = 0;
                        this.radio_PROGRAMCHECK.Tag = 1;
                        this.radio_FLUXCHECK.Tag = 0;
                        this.txt_ID.Text = Guid.NewGuid().ToString();
                        this.txt_hemoID.Text = _patientRow.HEMODIALYSIS_ID.ToString();

                        this.cmbPRIMARY_NURSE.EditValue = Hemo.Client.Core.HemoApplicationContext.Current.CurrentUser.EMP_NO;

                        //this.lupMACHINE_TYPE.EditValue = _patientRow.EDUCATION;
                        this.spnREUSE_TIMES.EditValue = _patientRow.INPUT_CODE.ToString();
                        if (_patientRow.INPUT_CODE.ToString().Equals("0"))
                        {
                            this.FirstUseTime.EditValue = System.DateTime.Now.ToString("yyyy-MM-dd");

                        }
                        else
                        {
                            this.FirstUseTime.EditValue = null;
                        }


                        this.DateAndTimeEdit.EditValue = System.DateTime.Now.ToString("yyyy-MM-dd");
                    }
                    else
                    {
                        this.radio_PROGRAMCHECK.SelectedIndex = this.radio_PROGRAMCHECK.Tag.ToString() == "" ? 0 : 1;
                        this.radio_FLUXCHECK.SelectedIndex = this.radio_FLUXCHECK.Tag.ToString() == "" ? 1 : 0;
                        this.cmbPRIMARY_NURSE.Text = _current.PRIMARY_NURSE;
                        this.lupMACHINE_TYPE.Focus();
                    }

                    this.Enabled = true;

                };
                worker.RunWorkerAsync();
            }
        }

        #endregion
    }
}