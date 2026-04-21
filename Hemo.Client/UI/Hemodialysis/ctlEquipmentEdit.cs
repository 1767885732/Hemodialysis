/*----------------------------------------------------------------
 * Copyright (C) 2005 (苏州)医疗科技发展有限公司
 * 文件功能描述:设备修改
 * 创建标识:顾伟伟-2013年5月15日
 * 
 * 修改时间:2013年8月23日
 * 修改人:贺建操
 * 修改描述:新增方法
 * 
 * 修改时间:2013年12月1日
 * 修改人:贺建操
 * 修改描述:修改方法
 * 
 * 修改时间:2014年3月11日
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
using Hemo.IService.Config;
using Hemo.Service;
using Hemo.Model;

namespace Hemo.Client.UI.Hemodialysis
{
    public partial class ctlEquipmentEdit : DevExpress.XtraEditors.XtraUserControl
    {
        #region  变量
        private IConfig _iconfig = ServiceManager.Instance.ConfigService;
        private string _id = null;
        private ConfigModel.MED_HOSPITAL_INFODataTable dtMed = null;
        private ConfigModel.MED_HOSPITAL_INFORow drMed =null;
        private DataTable dtDtl = null;
        #endregion

        #region 构造函数
        public ctlEquipmentEdit(string id)
        {
            InitializeComponent();
            _id = id;
        }
        #endregion
        #region 事件
        private void ctlEquipmentEdit_Load(object sender, EventArgs e)
        {
            LoadData();
            LoadDtlData();
        }

        void LoadDtlData()
        {
            dtDtl = _iconfig.GetQualityControlEquipmentInfo();
            this.gridControl1.DataSource = dtDtl;
        }

        void LoadData()
        {
            dtMed = _iconfig.GetMED_HOSPITAL_INFOById(_id);
            if (dtMed != null && dtMed.Rows.Count > 0)
            {
                drMed = (ConfigModel.MED_HOSPITAL_INFORow)dtMed.Rows[0];
                txtHOSPITAL_YEAR.Text = drMed.HOSPITAL_YEAR;
                txtHOSPITAL_NAME.Text = drMed.HOSPITAL_NAME;
                txtHOSPITAL_LEVEL.Text = drMed.HOSPITAL_LEVEL;
                txtONEHEMOMACHINE_MODEL.Text = drMed.ONEHEMOMACHINE_MODEL;
                cmbISHEMOMACHINE_MULTIPLEX.Text = drMed.ISHEMOMACHINE_MULTIPLEX;
                txtHEMOMACHINE_MODEL.Text = drMed.HEMOMACHINE_MODEL;
                txtMULTIPLEX_COUNT.Text = drMed.MULTIPLEX_COUNT;
                txtMULTIPLEX_MODEL.Text = drMed.MULTIPLEX_MODEL;
                txtMULTIPLEX_ANTISEPTIC_MODEL.Text = drMed.MULTIPLEX_ANTISEPTIC_MODEL;
                txtDIALYSATE_CA.Text = drMed.DIALYSATE_CA;
                txtCRRT_MACHINECOUNT.Text = drMed.CRRT_MACHINECOUNT;
                txtCRRT_MODEL.Text = drMed.CRRT_MODEL;
                txtCRRT_COUNT.Text = drMed.CRRT_COUNT;
                cbxTHERAPEUTIC_PROPERTIES.EditValue = drMed.THERAPEUTIC_PROPERTIES;
            }
        }

        void SaveDtl()
        {
            try
            {
                _iconfig.DeleteQualityControlEquipmentInfoByHospitalID(_id);
              
                if (dtDtl != null)
                {
                    ConfigModel.MED_EQUIPMENT_INFODataTable dt = new ConfigModel.MED_EQUIPMENT_INFODataTable();
                    ConfigModel.MED_EQUIPMENT_INFORow dr = null;
                    for (int i = 0; i < dtDtl.Rows.Count; i++)
                    {
                        dr = dt.NewMED_EQUIPMENT_INFORow();
                        dr.HOSPITAL_ID = _id;
                        dr.ID = System.Guid.NewGuid().ToString();
                        dr.FLNAME = dtDtl.Rows[i]["FLNAME"].ToString();
                        dr.COUNT = dtDtl.Rows[i]["COUNT"].ToString();
                        dr.KIND = dtDtl.Rows[i]["KIND"].ToString();
                        dt.Rows.Add(dr);
                    }

                    if (dt.Rows.Count >0)
                    {
                        _iconfig.SaveQualityControlEquipmentInfo(dt);
                    }
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.ToString());
            }

                  
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            drMed.ONEHEMOMACHINE_MODEL = txtONEHEMOMACHINE_MODEL.Text;
            drMed.ISHEMOMACHINE_MULTIPLEX = cmbISHEMOMACHINE_MULTIPLEX.Text;
            drMed.HEMOMACHINE_MODEL = txtHEMOMACHINE_MODEL.Text;
            drMed.MULTIPLEX_COUNT = txtMULTIPLEX_COUNT.Text;
            drMed.MULTIPLEX_MODEL = txtMULTIPLEX_MODEL.Text;
            drMed.MULTIPLEX_ANTISEPTIC_MODEL = txtMULTIPLEX_ANTISEPTIC_MODEL.Text;
            drMed.DIALYSATE_CA = txtDIALYSATE_CA.Text;
            drMed.CRRT_MACHINECOUNT = txtCRRT_MACHINECOUNT.Text;
            drMed.CRRT_MODEL = txtCRRT_MODEL.Text;
            drMed.CRRT_COUNT = txtCRRT_COUNT.Text;
            drMed.THERAPEUTIC_PROPERTIES = cbxTHERAPEUTIC_PROPERTIES.EditValue.ToString();

            SaveDtl();
            try {
                if (_iconfig.SaveMED_HOSPITAL_INFO(dtMed) > 0)
                {
                    XtraMessageBox.Show("保存成功");
                }              
            }
            catch(Exception ex) {
                XtraMessageBox.Show(ex.ToString());
            }
        }
        #endregion
    }
}
