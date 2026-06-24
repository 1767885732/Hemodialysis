/*----------------------------------------------------------------
// Copyright (C) 2013 苏州XX公司
// 描述：透析充分性评估明细用户控件类
// 创建时间：2015-08-18
// 创建者：吕志强
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
using Hemo.Model;
using Hemo.Client.Core;
using Hemo.Utilities;

namespace Hemo.Client.Controls
{
    public partial class CtlSufficiencyRecord : DevExpress.XtraEditors.XtraUserControl
    {
        #region 类变量

        private PatientKolcabaModel.MED_PATIENT_SUFFICIENCYRow currentRecordRow = null;

        private string currentHemoId = string.Empty;

        #endregion

        #region 属性

        public string CurrentHemoId
        {
            get { return currentHemoId; }
            set { currentHemoId = value; }
        }

        public PatientKolcabaModel.MED_PATIENT_SUFFICIENCYRow CurrentRecordRow
        {
            get { return currentRecordRow; }
            set { currentRecordRow = value; }
        }

        #endregion

        #region 构造函数

        public CtlSufficiencyRecord()
        {
            InitializeComponent();
 
        }

        #endregion

        #region 方法

        public void loadAllTipList() {
            DateTime dtEndDate = System.DateTime.Now;
            DateTime dtBeginDate = dtEndDate.AddMonths(-3);
            ctlPastDataQuery1.InzationBPData(CurrentHemoId, dtBeginDate, dtEndDate);
            ctlPastDataQuery2.InzationWeightData(CurrentHemoId, dtBeginDate, dtEndDate);
            ctlPastDataQuery3.InzationSufficiencyData(CurrentHemoId, dtBeginDate, dtEndDate);
            //InzationSufficiencyData
        }

        public void LoadMedicalRecord()
        {
            if (currentRecordRow != null)
            {
                this.INDIALYSISBP.EditValue = currentRecordRow["INDIALYSISBP"].ToString();
                this.DIALYSISBP.EditValue = currentRecordRow["DIALYSISBP"].ToString();
                this.HEART.EditValue = currentRecordRow["HEART"].ToString();
                this.EDEMA.EditValue = currentRecordRow["EDEMA"].ToString();
                this.WEIGHTADD.EditValue = currentRecordRow["WEIGHTADD"].ToString();
                this.NUTRITION.EditValue = currentRecordRow["NUTRITION"].ToString();
                this.SKIN.EditValue = currentRecordRow["SKIN"].ToString();
                this.BONEPAIN.EditValue = currentRecordRow["BONEPAIN"].ToString();
                this.SLEEP.EditValue = currentRecordRow["SLEEP"].ToString();
                if (currentRecordRow["COMPLICATION"].ToString() == "0")
                {
                    this.COMPLICATION.EditValue = "0";
                }
                else
                {
                    this.COMPLICATION.EditValue = "1";
                    this.txtCOMPLICATION.Text = currentRecordRow["COMPLICATION"].ToString();
                    txtCOMPLICATION.Enabled = true;
                }
                this.ANEMIA.EditValue = currentRecordRow["ANEMIA"].ToString();
                this.DISORDER.EditValue = currentRecordRow["DISORDER"].ToString();
                this.P.EditValue = currentRecordRow["P"].ToString();
                this.CA.EditValue = currentRecordRow["CA"].ToString();
                this.IPTH.EditValue = currentRecordRow["IPTH"].ToString();
                this.KTV.EditValue = currentRecordRow["KTV"].ToString();
                this.URR.EditValue = currentRecordRow["URR"].ToString();
                this.txtSUFFICIENCY.Text = currentRecordRow["SUFFICIENCY"].ToString();
                this.txtPLAN.Text = currentRecordRow["PLAN"].ToString();
                this.txtDRUG.Text = currentRecordRow["DRUG"].ToString();
            }
        }

        public PatientKolcabaModel.MED_PATIENT_SUFFICIENCYDataTable GetPatientSufficiencyDataTable(PatientKolcabaModel.MED_PATIENT_SUFFICIENCYDataTable dtRecord)
        {
            if (dtRecord == null)
            {
                //新增
                dtRecord = new PatientKolcabaModel.MED_PATIENT_SUFFICIENCYDataTable();
                var row = dtRecord.NewMED_PATIENT_SUFFICIENCYRow();
                row.ID = Guid.NewGuid().ToString().Trim();
                row.HEMODIALYSIS_ID = currentHemoId;
                row.CREATEDATE = DateTime.Now.Date;
                row.LASTUPDATEBY = HemoApplicationContext.Current.CurrentUser.USER_ID;
                row.LASTUPDATEDATE = System.DateTime.Now;
                row.INDIALYSISBP = Utility.CDecimal(this.INDIALYSISBP.EditValue.ToString());
                row.DIALYSISBP = Utility.CDecimal(this.DIALYSISBP.EditValue.ToString());
                row.HEART = Utility.CDecimal(this.HEART.EditValue.ToString());
                row.EDEMA = Utility.CDecimal(this.EDEMA.EditValue.ToString());
                row.WEIGHTADD = Utility.CDecimal(this.WEIGHTADD.EditValue.ToString());
                row.NUTRITION = Utility.CDecimal(this.NUTRITION.EditValue.ToString());
                row.SKIN = Utility.CDecimal(this.SKIN.EditValue.ToString());
                row.BONEPAIN = Utility.CDecimal(this.BONEPAIN.EditValue.ToString());
                row.SLEEP = Utility.CDecimal(this.SLEEP.EditValue.ToString());
                if (this.COMPLICATION.EditValue.ToString() == "0")
                {
                    row.COMPLICATION = "0";
                }
                else
                {
                    row.COMPLICATION = this.txtCOMPLICATION.Text;
                }
                row.ANEMIA = Utility.CDecimal(this.ANEMIA.EditValue.ToString());
                row.DISORDER = Utility.CDecimal(this.DISORDER.EditValue.ToString());
                row.P = Utility.CDecimal(this.P.EditValue.ToString());
                row.CA = Utility.CDecimal(this.CA.EditValue.ToString());
                row.IPTH = Utility.CDecimal(this.IPTH.EditValue.ToString());
                row.KTV = Utility.CDecimal(this.KTV.EditValue.ToString());
                row.URR = Utility.CDecimal(this.URR.EditValue.ToString());
                row.SUFFICIENCY = this.txtSUFFICIENCY.Text;
                row.PLAN = this.txtPLAN.Text;
                row.DRUG = this.txtDRUG.Text;
                row.ISDELETE = "0";
                dtRecord.AddMED_PATIENT_SUFFICIENCYRow(row);
            }
            else
            {
                //编辑
                dtRecord[0].LASTUPDATEBY = HemoApplicationContext.Current.CurrentUser.USER_ID;
                dtRecord[0].LASTUPDATEDATE = System.DateTime.Now;
                dtRecord[0].INDIALYSISBP = Utility.CDecimal(this.INDIALYSISBP.EditValue.ToString());
                dtRecord[0].DIALYSISBP = Utility.CDecimal(this.DIALYSISBP.EditValue.ToString());
                dtRecord[0].HEART = Utility.CDecimal(this.HEART.EditValue.ToString());
                dtRecord[0].EDEMA = Utility.CDecimal(this.EDEMA.EditValue.ToString());
                dtRecord[0].WEIGHTADD = Utility.CDecimal(this.WEIGHTADD.EditValue.ToString());
                dtRecord[0].NUTRITION = Utility.CDecimal(this.NUTRITION.EditValue.ToString());
                dtRecord[0].SKIN = Utility.CDecimal(this.SKIN.EditValue.ToString());
                dtRecord[0].BONEPAIN = Utility.CDecimal(this.BONEPAIN.EditValue.ToString());
                dtRecord[0].SLEEP = Utility.CDecimal(this.SLEEP.EditValue.ToString());
                if (this.COMPLICATION.EditValue.ToString() == "0")
                {
                    dtRecord[0].COMPLICATION = "0";
                }
                else
                {
                    dtRecord[0].COMPLICATION = this.txtCOMPLICATION.Text;
                }
                dtRecord[0].ANEMIA = Utility.CDecimal(this.ANEMIA.EditValue.ToString());
                dtRecord[0].DISORDER = Utility.CDecimal(this.DISORDER.EditValue.ToString());
                dtRecord[0].P = Utility.CDecimal(this.P.EditValue.ToString());
                dtRecord[0].CA = Utility.CDecimal(this.CA.EditValue.ToString());
                dtRecord[0].IPTH = Utility.CDecimal(this.IPTH.EditValue.ToString());
                dtRecord[0].KTV = Utility.CDecimal(this.KTV.EditValue.ToString());
                dtRecord[0].URR = Utility.CDecimal(this.URR.EditValue.ToString());
                dtRecord[0].SUFFICIENCY = this.txtSUFFICIENCY.Text;
                dtRecord[0].PLAN = this.txtPLAN.Text;
                dtRecord[0].DRUG = this.txtDRUG.Text;
            }
            return dtRecord;
        }

        private void COMPLICATION_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.COMPLICATION.EditValue.ToString() == "0")
            {
                this.txtCOMPLICATION.Text = "";
                txtCOMPLICATION.Enabled = false;
            }
            else
            {
                txtCOMPLICATION.Enabled = true;
            }
        }

        #endregion
    }
}
