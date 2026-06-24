/*----------------------------------------------------------------
// Copyright (C) 2005 苏州XX公司有限公司
// 描述：医护人员维护用户控件
// 创建时间：2014-04-13
// 创建者：刘超
//  
// 修改时间：2014-05-07
// 修改人：吕志强
// 修改描述：
----------------------------------------------------------------*/

using System;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Hemo.IService.Config;
using Hemo.IService.Dept;
using Hemo.IService.Dict;
using Hemo.IService.Permission;
using Hemo.Model;
using Hemo.Service;
using Hemo.Utilities;
using System.Data;

namespace Hemo.Client.UI.Dict
{
    public partial class EditStaffDict :HemoBaseFrm
    {
        #region 成员变量

        private DictModel.MED_STAFF_DICTDataTable _staffDictDataTable;

        private DictModel.MED_STAFF_DICTRow _staffDictRow;

        private IConfig _configService = ServiceManager.Instance.ConfigService;

        private IStaffDict _staffDictService = ServiceManager.Instance.StaffDictService;

        private IDept _deptService = ServiceManager.Instance.DeptService;

        private IUser _userService = ServiceManager.Instance.UserService;

        string strJobType = string.Empty;

        #endregion

        #region 构造函数

        public EditStaffDict(DictModel.MED_STAFF_DICTDataTable staffDictDataTable, DictModel.MED_STAFF_DICTRow staffDictRow, string pType)
        {
            this.InitializeComponent();
            this._staffDictDataTable = staffDictDataTable;
            this._staffDictRow = staffDictRow;
            strJobType = pType;
        }

        #endregion

        #region 方法

        private void InitializeControls()
        {
            Utility.BindLookUpEdit(this.cbxDEPT, "DEPT_ID", "DEPT_NAME", this._deptService.GetDeptList(), "DEPT_NAME", "科室名称");
            DataTable dtJob = Utility.GetSubTable(this._configService.GetConfigList(string.Empty, string.Empty, "职业", "1") as DataTable, "item_name= '" + strJobType + "'");

            if (dtJob != null && dtJob.Rows.Count > 0)
            {
                Utility.BindLookUpEdit(this.cbxJOB, "ITEM_ID", "ITEM_NAME", dtJob, "ITEM_NAME", "职业");
            }

            Utility.BindLookUpEdit(this.cbxTITLE, "ITEM_ID", "ITEM_NAME", this._configService.GetConfigList(string.Empty, string.Empty, "职称", "1"), "ITEM_NAME", "职称");
            Utility.BindLookUpEdit(this.cbxUSER, "USER_ID", "USER_NAME", this._userService.GetUserList(), "USER_NAME", "用户姓名");

            this.cbxUSER.EditValue = string.Empty;

            if (this._staffDictRow != null) //修改
            {
                this.cbxDEPT.EditValue = this._staffDictRow.DEPT_CODE;
                this.txtNAME.Text = this._staffDictRow.NAME;
                this.txtINPUT_CODE.Text = this._staffDictRow.INPUT_CODE;
                this.cbxJOB.EditValue = this._staffDictRow.JOB;
                this.cbxTITLE.EditValue = this._staffDictRow.TITLE;
                this.cbxUSER.EditValue = this._staffDictRow.USER_NAME;
                loadIsDelete(this._staffDictRow.IS_DELETE);
            }
        }

        private bool IsDataValidate()
        {
            this.errorProvider.SetError(this.cbxDEPT, string.Empty);
            this.errorProvider.SetError(this.txtNAME, string.Empty);
            this.errorProvider.SetError(this.txtINPUT_CODE, string.Empty);
            this.errorProvider.SetError(this.cbxJOB, string.Empty);
            this.errorProvider.SetError(this.cbxTITLE, string.Empty);
            this.errorProvider.SetError(this.cbxUSER, string.Empty);

            if (string.IsNullOrEmpty(this.cbxDEPT.Text))
            {
                this.cbxDEPT.Focus();

                this.errorProvider.SetError(this.cbxDEPT, "请选择工作人员所在科室！");

                return false;
            }

            if (string.IsNullOrEmpty(this.txtNAME.Text))
            {
                this.txtNAME.Focus();

                this.errorProvider.SetError(this.txtNAME, "姓名不能为空！");

                return false;
            }

            if (string.IsNullOrEmpty(this.txtINPUT_CODE.Text))
            {
                this.txtINPUT_CODE.Focus();

                this.errorProvider.SetError(this.txtINPUT_CODE, "姓名的输入码不能为空！");

                return false;
            }

            if (string.IsNullOrEmpty(this.cbxJOB.Text))
            {
                this.cbxJOB.Focus();

                this.errorProvider.SetError(this.cbxJOB, "请选择职业！");

                return false;
            }

            if (string.IsNullOrEmpty(this.cbxTITLE.Text))
            {
                this.cbxTITLE.Focus();

                this.errorProvider.SetError(this.cbxTITLE, "请选择职称！");

                return false;
            }

            if (string.IsNullOrEmpty(this.cbxUSER.Text))
            {
                this.cbxUSER.Focus();

                this.errorProvider.SetError(this.cbxUSER, "请选择本系统用户！");

                return false;
            }

            return true;
        }

        private void loadIsDelete(string pIsDelete)
        {
            if (pIsDelete == string.Empty || pIsDelete == "1")
            {
                cbxIS_DELETE.SelectedIndex = 0;
            }
            else if (pIsDelete == "0")
            {
                cbxIS_DELETE.SelectedIndex = 1;
            }
        }

        #endregion

        #region 事件

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void EditStaffDict_Load(object sender, EventArgs e)
        {
            this.InitializeControls();
        }

        private void txtNAME_Leave(object sender, EventArgs e)
        {
            if (PinYinConverter.GetPYString(txtNAME.Text.Trim()).Length <= 8)
            {
                txtINPUT_CODE.Text = PinYinConverter.GetPYString(txtNAME.Text.Trim());
            }
            else
            {
                txtINPUT_CODE.Text = PinYinConverter.GetPYString(txtNAME.Text.Trim()).Substring(0, 7);
            }
        }

        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!this.IsDataValidate())
                return;

            if (this._staffDictRow == null) //新增
            {
                //检查输入的姓名是否已经存在于员工表中
                DictModel.MED_STAFF_DICTDataTable dtStaff = _staffDictService.GetStaffDictList();
                DataRow[] rows = dtStaff.Select("USER_NAME='" + this.cbxUSER.EditValue.ToString() + "'");

                if (rows != null && rows.Length > 0)
                {
                    XtraMessageBox.Show("输入的用户已经存在！", "提示");
                    return;
                }

                this._staffDictRow = this._staffDictDataTable.NewMED_STAFF_DICTRow();
                this._staffDictRow.EMP_NO = this._staffDictService.GetNewEMPNO();
               
                this._staffDictDataTable.AddMED_STAFF_DICTRow(this._staffDictRow);
            }

            this._staffDictRow.DEPT_CODE = this.cbxDEPT.EditValue.ToString();
            this._staffDictRow.NAME = this.txtNAME.Text;
            this._staffDictRow.INPUT_CODE = this.txtINPUT_CODE.Text;
            this._staffDictRow.JOB = this.cbxJOB.EditValue.ToString();
            this._staffDictRow.TITLE = this.cbxTITLE.EditValue.ToString();
            this._staffDictRow.USER_NAME = this.cbxUSER.EditValue.ToString();
            if (cbxIS_DELETE.SelectedIndex == 0)
            {
                this._staffDictRow.IS_DELETE = "1";
            }
            else if (cbxIS_DELETE.SelectedIndex == 1)
            {
                this._staffDictRow.IS_DELETE = "0";
            }
            this._staffDictService.SaveStaffDictInfo(this._staffDictDataTable);

            //XtraMessageBox.Show("保存成功！");
            AutoClosedMsgBox.ShowForm("保存成功。", "系统提示", 1500, MessageBoxIcon.Information);

            this.DialogResult = DialogResult.Yes;
        }

        #endregion 
    }
}
