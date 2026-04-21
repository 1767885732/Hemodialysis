using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Hemo.Client.Print;
using System.Drawing.Printing;
using DevExpress.XtraEditors;
using Hemo.Model;
using Hemo.Utilities;
using Hemo.Client.Core;
using Hemo.IService;
using Hemo.Service;

namespace Hemo.Client.UI.Assessment
{
    public partial class HemoEventExtInfo : UserControl
    {

        public PatientModel.MED_HEMO_EVENTINFORow _HemoEventExtInfoRow { get; set; }

        private PatientModel.MED_HEMO_EVENTINFODataTable hemoEventDt = null;

        private IPatient objPatient = ServiceManager.Instance.PatientService;

        public event EventHandler SaveOkEvent;


        public HemoEventExtInfo()
        {
            InitializeComponent();
        }


        public void InzationData()
        {

            using (var _doWoker = new BackgroundWorker())
            {
                hemoEventDt = new PatientModel.MED_HEMO_EVENTINFODataTable();
                _doWoker.DoWork += (o, e) =>
                {
                    if (_HemoEventExtInfoRow == null)
                    {
                        try
                        {
                            if (this.ctlUserLongInfo1.patientInfoCheck1._patientDataTable != null && this.ctlUserLongInfo1.patientInfoCheck1._patientDataTable.Rows.Count > 0)
                            {
                                //根据透析号和日期年月。获取数据
                                hemoEventDt = objPatient.GetHemoEventInfo(this.ctlUserLongInfo1.patientInfoCheck1._patientDataTable[0].HEMODIALYSIS_ID, this.TXTHEMOEVENTDT.DateTime, "0");
                            }
                            else
                            {
                                hemoEventDt = new PatientModel.MED_HEMO_EVENTINFODataTable();
                            }
                        }
                        catch { }
                    }
                    else
                    {
                        hemoEventDt = objPatient.GetHemoEventInfo(_HemoEventExtInfoRow.HEMODIALYSIS_ID, _HemoEventExtInfoRow.CREATERTIME, "0");

                    }
                };
                _doWoker.RunWorkerCompleted += (o1, e1) =>
                {
                    if (hemoEventDt.Rows.Count > 0)
                    {
                        this._HemoEventExtInfoRow = hemoEventDt[0];
                    }
                    else
                    {
                        if (this.ctlUserLongInfo1.patientInfoCheck1._patientDataTable != null && this.ctlUserLongInfo1.patientInfoCheck1._patientDataTable.Rows.Count > 0)
                        {
                            this._HemoEventExtInfoRow = hemoEventDt.NewMED_HEMO_EVENTINFORow();
                            this._HemoEventExtInfoRow.ID = Guid.NewGuid().ToString();
                            this._HemoEventExtInfoRow.HEMODIALYSIS_ID = this.ctlUserLongInfo1.patientInfoCheck1._patientDataTable[0].HEMODIALYSIS_ID;
                            this._HemoEventExtInfoRow.CREATERTIME = this.TXTHEMOEVENTDT.DateTime == null ? DateTime.Now : this.TXTHEMOEVENTDT.DateTime;
                            this._HemoEventExtInfoRow.CREATER = HemoApplicationContext.Current.CurrentUser.USER_ID;
                            this._HemoEventExtInfoRow.EVENTTYPE = "0";
                            this._HemoEventExtInfoRow.NAME = this.ctlUserLongInfo1.patientInfoCheck1._patientDataTable[0].NAME;
                            this._HemoEventExtInfoRow.SEX = this.ctlUserLongInfo1.patientInfoCheck1._patientDataTable[0].SEX;
                            this._HemoEventExtInfoRow.AGE = this.ctlUserLongInfo1.patientInfoCheck1._patientDataTable[0].AGE.ToString();
                            hemoEventDt.AddMED_HEMO_EVENTINFORow(this._HemoEventExtInfoRow);
                        }
                        else
                        {
                            Utilities.BaseControlInfo.ClearControlText(panelControl2);
                            this.TXTHEMOEVENTDT.DateTime = DateTime.Now;

                        }
                    }
                    BindData(this.panelControl2 as DevExpress.XtraEditors.PanelControl);

                };
                _doWoker.RunWorkerAsync();
            }

        }


        /// <summary>
        /// 保存操作直接 保存北
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSave_Click(object sender, EventArgs e)
        {
            foreach (PatientModel.MED_HEMO_EVENTINFORow row in this.hemoEventDt.Rows)
            {
                row.CREATERTIME = DateTime.Now;
            }
            if (this.objPatient.SaveHemoEventInfo(this.hemoEventDt) >= 1)
            {
                AutoClosedMsgBox.ShowForm("保存成功！", "系统提示", 1000, MessageBoxIcon.Warning);
                if (SaveOkEvent != null)
                {
                    SaveOkEvent(null, null);
                }
            }
            else
            {
                AutoClosedMsgBox.ShowForm("保存失败！", "系统提示", 1000, MessageBoxIcon.Warning);
            }
        }



        #region 方法

        private void BindData(DevExpress.XtraEditors.PanelControl panel)
        {
            foreach (var ctl in panel.Controls)
            {
                if (ctl is BaseEdit)
                {
                    (ctl as BaseEdit).BindingDiffControlDataRow(this._HemoEventExtInfoRow);
                }
                else if (ctl is DevExpress.XtraEditors.PanelControl)
                {
                    BindData(ctl as DevExpress.XtraEditors.PanelControl);
                }
                else if (ctl is CheckedListBoxControl)
                {
                    (ctl as CheckedListBoxControl).BindingDiffCheckedDataRow(this._HemoEventExtInfoRow);
                }
                else if (ctl is System.Windows.Forms.TableLayoutPanel)
                {
                    BindTableLayoutData(ctl as System.Windows.Forms.TableLayoutPanel);
                }
            }
        }

        private void BindTableLayoutData(System.Windows.Forms.TableLayoutPanel tPanel)
        {
            foreach (var tctl in tPanel.Controls)
            {
                if (tctl is BaseEdit)
                {
                    (tctl as BaseEdit).BindingDiffControlDataRow(this._HemoEventExtInfoRow);
                }
                else if (tctl is DevExpress.XtraEditors.PanelControl)
                {
                    BindData(tctl as DevExpress.XtraEditors.PanelControl);
                }
                else if (tctl is CheckedListBoxControl)
                {
                    (tctl as CheckedListBoxControl).BindingDiffCheckedDataRow(this._HemoEventExtInfoRow);
                }
                else if (tctl is System.Windows.Forms.TableLayoutPanel)
                {
                    BindTableLayoutData(tctl as System.Windows.Forms.TableLayoutPanel);
                }

            }
        }

        #endregion



        private void HemoEventExtInfo_Load(object sender, EventArgs e)
        {
            this.ctlUserLongInfo1.patientInfoCheck1.patientPickEvent += new EventHandler(patientInfoCheck1_patientPickEvent);
        }
        /// <summary>
        /// 病人改变时
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void patientInfoCheck1_patientPickEvent(object sender, EventArgs e)
        {
            LoadUserInfo(this.ctlUserLongInfo1.patientInfoCheck1._patientDataTable[0].HEMODIALYSIS_ID);
            InzationData();
        }
        /// <summary>
        /// 加载用户信息表
        /// </summary>
        /// <param name="hemoId"></param>
        public void LoadUserInfo(string hemoId)
        {
            this.ctlUserLongInfo1.HEMODIALYSIS_ID = hemoId;
            this.ctlUserLongInfo1.LoadPatientInfo();
        }

        private void TXTCREATERTIME_EditValueChanged(object sender, EventArgs e)
        {
            if (this.ctlUserLongInfo1.patientInfoCheck1._patientDataTable != null && this.ctlUserLongInfo1.patientInfoCheck1._patientDataTable.Rows.Count > 0)
            {
                //InzationData();
            }
        }


        private void BtnCalcle_Click(object sender, EventArgs e)
        {
            if (SaveOkEvent != null)
            {
                SaveOkEvent(null, null);
            }
        }
    }
}
