/*----------------------------------------------------------------
 * Copyright (C) 2005 (苏州)医疗科技发展有限公司
 * 文件功能描述:HOSPITAL
 * 创建标识:贺建操-2013年8月3日
 * 
 * 修改时间:2013年11月11日
 * 修改人:刘超
 * 修改描述:新增方法
 * 
 * 修改时间:2014年2月19日
 * 修改人:吕志强
 * 修改描述:新增方法SQL
 * 
 * 修改时间:2014年5月30日
 * 修改人:贺建操
 * 修改描述:修改方法SQL
 * ----------------------------------------------------------------*/
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

namespace Hemo.Client.UI.Hemodialysis
{
    public partial class ctlForHOSPITAL : DevExpress.XtraEditors.XtraUserControl
    {
        #region 变量
        private static ConfigModel.MED_HOSPITAL_INFORow _currentRow = null;
          private ConfigModel.MED_HOSPITAL_INFODataTable _dtmedhospitalinfo = null;
          private IConfig _iconfig = ServiceManager.Instance.ConfigService;
          private bool _isAdd;
        #endregion
          #region 构造函数
          public ctlForHOSPITAL(ConfigModel.MED_HOSPITAL_INFORow row, ConfigModel.MED_HOSPITAL_INFODataTable dtmedhospitalinfo)
          {
              InitializeComponent();
              _currentRow = row;

              _isAdd = (row == null ? true : false);
              _dtmedhospitalinfo = dtmedhospitalinfo;
          }
          #endregion
          #region 事件
          private void btnSave_Click(object sender, EventArgs e)
        {
            if (IsDataValidate())
            {
                _currentRow.HOSPITAL_YEAR = this.txtHOSPITAL_YEAR.Text;
                _currentRow.HOSPITAL_NAME = this.txtHOSPITAL_NAME.Text;
                _currentRow.HOSPITAL_LEVEL = this.txtHOSPITAL_LEVEL.Text;

                _currentRow.CONTACT_PEOPLE = this.txtCONTACT_PEOPLE.Text;
                _currentRow.CONTACT_EMAIL = this.txtCONTACT_EMAIL.Text;
                _currentRow.CONTACT_PHONE = this.txtCONTACT_PHONE.Text;

                _currentRow.HEAD_NURSE = this.txtHEAD_NURSE.Text;
                _currentRow.HEAD_NURSE_EMAIL = this.txtHEAD_NURSE_EMAIL.Text;
                _currentRow.HEAD_NURSE_PHONE = this.txtHEAD_NURSE_PHONE.Text;

                _currentRow.PHYSICIAN_COUNT = this.txtPHYSICIAN_COUNT.Text;
                _currentRow.NURSE_COUNT = this.txtNURSE_COUNT.Text;
                _currentRow.TECHNICIAN_COUNT = this.txtTECHNICIAN_COUNT.Text;
                if (_isAdd)
                {
                    var dt = _iconfig.GetMED_HOSPITAL_INFOList();
                    var row = (dt == null ? null : dt.FirstOrDefault(i => i.HOSPITAL_YEAR == this.txtHOSPITAL_YEAR.Text && i.HOSPITAL_NAME == this.txtHOSPITAL_NAME.Text));
                    if (row != null)
                    {
                        XtraMessageBox.Show("统计年份："+this.txtHOSPITAL_YEAR.Text+"\r\n"+ "医院名称：" + txtHOSPITAL_NAME.Text+"\r\n"+  "已经存在，无法重复新增");
                        return;
                    }
                }
                try
                {
                    if (_iconfig.SaveMED_HOSPITAL_INFO(_dtmedhospitalinfo) > 0)
                    {
                        XtraMessageBox.Show("保存成功");
                    };
                }
                catch (Exception ex)
                {
                    XtraMessageBox.Show(ex.ToString());
                }

            };  
        }

        bool IsDataValidate()
        {
            this.dxErrorProvider1.ClearErrors();
            string errorMsg = string.Empty;

            if (String.IsNullOrEmpty(txtHOSPITAL_YEAR.Text))
            {
                errorMsg = "请输入年份";
                dxErrorProvider1.SetError(txtHOSPITAL_YEAR, errorMsg);
                return false;
            }
            if (String.IsNullOrEmpty(txtHOSPITAL_NAME.Text))
            {
                errorMsg = "请输入医疗机构名称";
                dxErrorProvider1.SetError(txtHOSPITAL_NAME, errorMsg);
                return false;
            }
            if (String.IsNullOrEmpty(txtPHYSICIAN_COUNT.Text))

            {
                errorMsg = "请输入医生数量";
                dxErrorProvider1.SetError(txtPHYSICIAN_COUNT, errorMsg);
                return false;
            }
            if (String.IsNullOrEmpty(txtTECHNICIAN_COUNT.Text))
            {
                errorMsg = "请输入技师数量";
                dxErrorProvider1.SetError(txtTECHNICIAN_COUNT, errorMsg);
                return false;
            }
            if (String.IsNullOrEmpty(txtNURSE_COUNT.Text))
            {
                errorMsg = "请输入护士数量";
                dxErrorProvider1.SetError(txtNURSE_COUNT, errorMsg);
                return false;
            }
            return true;    
        }

        private void ctlForHOSPITAL_Load(object sender, EventArgs e)
        {
            if (_dtmedhospitalinfo == null)
            {
                _dtmedhospitalinfo = new ConfigModel.MED_HOSPITAL_INFODataTable();
            }
       
            if (_currentRow == null)  //新增
            {
                _currentRow = _dtmedhospitalinfo.NewMED_HOSPITAL_INFORow();
                _currentRow.HOSPITAL_ID = System.Guid.NewGuid().ToString();
                _dtmedhospitalinfo.Rows.Add(_currentRow);
                this.txtHOSPITAL_YEAR.EditValue = System.DateTime.Now.ToString("yyyy");
                this.txtHOSPITAL_YEAR.Enabled = true;
                this.txtHOSPITAL_NAME.Text = Utility.GetHospitalName();
            }
            else  //修改
            {
                this.txtHOSPITAL_YEAR.Text = _currentRow.HOSPITAL_YEAR;
                this.txtHOSPITAL_NAME.Text = _currentRow.HOSPITAL_NAME;
                this.txtHOSPITAL_LEVEL.Text = _currentRow.HOSPITAL_LEVEL;

                this.txtCONTACT_PHONE.Text = _currentRow.CONTACT_PHONE;
                this.txtCONTACT_EMAIL.Text = _currentRow.CONTACT_EMAIL;
                this.txtCONTACT_PEOPLE.Text = _currentRow.CONTACT_PEOPLE;

                this.txtHEAD_NURSE.Text = _currentRow.HEAD_NURSE;
                this.txtHEAD_NURSE_EMAIL.Text = _currentRow.HEAD_NURSE_EMAIL;
                this.txtHEAD_NURSE_PHONE.Text = _currentRow.HEAD_NURSE_PHONE;

                this.txtPHYSICIAN_COUNT.Text = _currentRow.PHYSICIAN_COUNT;
                this.txtNURSE_COUNT.Text = _currentRow.NURSE_COUNT;
                this.txtTECHNICIAN_COUNT.Text = _currentRow.TECHNICIAN_COUNT;
                this.txtHOSPITAL_YEAR.Enabled = false;
            }
        }
          #endregion
    }
}
