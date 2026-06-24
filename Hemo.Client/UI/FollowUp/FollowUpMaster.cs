/*----------------------------------------------------------------
// Copyright (C) 2005 苏州XX公司有限公司
// 描述：透析随访记录单主窗体
// 创建时间：2014-09-11
// 创建者：贺建操
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
using Hemo.Service;
using Hemo.Model;
using Hemo.Utilities;
using Hemo.Client.Print;
using Hemo.IService;
using System.Drawing.Printing;
using Hemo.Client.UI.Hemodialysis;
using Hemo.IService.PatientSchedule;

namespace Hemo.Client.UI.FollowUp
{
    public partial class FollowUpMaster : HemoBaseFrm
    {
        #region 类变量

        private IMaterial objMaterial = ServiceManager.Instance.MaterialService;
        private IPatientSchedule patientScheduleService = ServiceManager.Instance.PatientSchedule;
        public bool isHavingInDatabase = false;
        private PatientModel.MED_PATIENTSRow _patientRow = null;

        #endregion

        #region 属性

        public PatientModel.MED_PATIENTSRow PatientRow
        {
            get { return _patientRow; }
            set
            {
                _patientRow = value;
                if (value != null)
                {
                    var dtRow = objMaterial.GetFollowUpByHemoID(Utility.CDate(patientScheduleService.GetServerDate()), _patientRow.HEMODIALYSIS_ID);
                    if (dtRow != null && dtRow.Count > 0)
                    {
                        isHavingInDatabase = true;
                        SetFollowUp(dtRow.Rows[0] as DrugModel.MED_PATIENT_FOLLOWUPRow);
                    }
                    else
                    {
                        DrugModel.MED_PATIENT_FOLLOWUPDataTable dt = new DrugModel.MED_PATIENT_FOLLOWUPDataTable();
                        var row = dt.NewMED_PATIENT_FOLLOWUPRow();
                        row.HEMODIALYSIS_ID = _patientRow.HEMODIALYSIS_ID;
                        row.PATIENT = _patientRow.PATIENT_ID;
                        row.NAME = _patientRow.NAME;
                        row.FOLLOWDATE = System.DateTime.Now;
                        row.FOLLOWYEAR = System.DateTime.Now.Year.ToString();
                        row.FOLLOWMOTH = System.DateTime.Now.Month.ToString();
                        row.FOLLOWDAY = System.DateTime.Now.Day.ToString();
                        row.ID = Guid.NewGuid().ToString();
                        isHavingInDatabase = false;
                        SetFollowUp(row);
                    }

                    followUpControl1.isFromDatabase = isHavingInDatabase;

                }
            }
        }

        #endregion

        #region 构造函数

        public FollowUpMaster()
        {
            InitializeComponent();
        }

        #endregion

        #region 事件

        private void FollowUpMaster_Load(object sender, EventArgs e)
        {
            this.Text = "随访记录";
            ProFunctionCount pfc = new ProFunctionCount();
            pfc.SaveFunctionCountFrm(this);
            followUpControl1.isFromDatabase = isHavingInDatabase;
            this.followUpControl1.InzationMaterialDate();
        }

        private void btn_Save_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.followUpControl1.SaveData() > 0)
                { AutoClosedMsgBox.ShowForm("保存成功。", "随访信息", 1000, MessageBoxIcon.Information); }
                else
                {
                    AutoClosedMsgBox.ShowForm("失败。", "随访信息", 1000, MessageBoxIcon.Error);
                }
                this.DialogResult = System.Windows.Forms.DialogResult.OK;
            }
            catch (Exception ex)
            {
                AutoClosedMsgBox.ShowForm(ex.Message, "随访信息", 1000, MessageBoxIcon.Error);
            }

        }

        private void btn_Cancle_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btn_Print_Click(object sender, EventArgs e)
        {
            #region MyRegion

            ControlPrint frm = new ControlPrint(this.followUpControl1);
            printPreviewDialog1.Document = (PrintDocument)frm;
            printPreviewDialog1.ShowDialog();
            #endregion

            #region //也可用下边的方式进行打印

            //ControlPrint m_print = new ControlPrint();
            //m_print.StretchControl = true;
            //m_print.SetControl(this.followUpControl1);
            //m_print.PrintWidth = m_print.CalculateSize().Width;
            //m_print.PrintHeight = m_print.CalculateSize().Height;
            //printPreviewDialog1.Document = (PrintDocument)m_print;
            //printPreviewDialog1.ShowDialog();


            //ControlPrint m_print2 = new ControlPrint(this.followUpControl1);
            //m_print2.Print();
            #endregion
        }

        #endregion

        #region 方法

        public void SetFollowUp(DrugModel.MED_PATIENT_FOLLOWUPRow row)
        {
            this.followUpControl1.CurrentData = row;
        }

        #endregion
    }

}