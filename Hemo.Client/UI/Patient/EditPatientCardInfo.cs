/*----------------------------------------------------------------
// Copyright (C) 2013 苏州XX公司
// 描述：患者制卡管理类
// 创建时间：2016-05-17
// 创建者：贺建操
//  
// 修改时间：
// 修改人：
// 修改描述：
//
----------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Hemo.Client.Core;
using Hemo.Model;
using Hemo.IService;
using Hemo.Service;
using Hemo.Utilities;
using Docare.BarcodeInput;

namespace Hemo.Client.UI.Patient
{
    public partial class EditPatientCardInfo : HemoBaseFrm, ICodeInputHander
    {
        #region 构造函数

        public EditPatientCardInfo()
        {
            InitializeComponent();
            Docare.BarcodeInput.BarcodeInputListener.AddHander(this);

        }

        #endregion

        #region 变量属性

        public DrugModel.MED_PATIENTS_CARDDataTable _patientCard = new DrugModel.MED_PATIENTS_CARDDataTable();
        public string HemoId { get; set; }
        private IPatient objPatient = ServiceManager.Instance.PatientService;

        #endregion

        #region 方法


        /// <summary>
        /// 制卡
        /// </summary>
        /// <param name="inputArg"></param>
        public void BarCodeInput(DeviceInputArg inputArg)
        {

            string inputBarCode = string.Empty;
            if (string.IsNullOrEmpty(inputArg.BarCode)) return;
            if (inputArg.BarCode.Length > 10)
            {
                inputBarCode = inputArg.BarCode.Substring(0, 10);
            }
            else
            {
                inputBarCode = inputArg.BarCode;

            }
            var havingRow = this._patientCard.FirstOrDefault(i => i.STATE == "0" && i.SERIALNUMBER == inputBarCode);
            if (havingRow != null)
            {
                this.labelControl1.Text = "此卡已被占用\r\n请重新刷卡！";
                return;
            }


            //同步完成后进行卡号等信息设置
            var row = this._patientCard.NewMED_PATIENTS_CARDRow();
            row.ID = Guid.NewGuid().ToString();
            row.HEMODIALYSIS_ID = HemoId;
            row.SERIALNUMBER = inputBarCode;
            row.SEC = "1";
            row.CARDKEY = "ID卡";
            row.CARDNO = inputBarCode;
            row.CARDVALUE = inputBarCode;
            row.STATE = "0";
            row.EXTEND1 = "ID卡设备印刷前10位";
            row.CREATEBY = HemoApplicationContext.Current.CurrentUser.USER_ID;
            row.CREATEDATE = System.DateTime.Now;
            this._patientCard.AddMED_PATIENTS_CARDRow(row);

            this.DialogResult = System.Windows.Forms.DialogResult.OK;
        }
        #endregion

        #region 事件

        private void EditPatientCardInfo_Load(object sender, EventArgs e)
        {
            _patientCard = objPatient.GetPatientCardDt();
        }

        #endregion

        private void EditPatientCardInfo_FormClosing(object sender, FormClosingEventArgs e)
        {
            Docare.BarcodeInput.BarcodeInputListener.RemoveHander(this);


        }
    }
}
