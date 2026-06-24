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
    public partial class HemoEventInfo : UserControl
    {

        public PatientModel.MED_HEMO_EVENTINFORow _hemoEventInfoRow { get; set; }

        private PatientModel.MED_HEMO_EVENTINFODataTable hemoEventDt = null;

        private IPatient objPatient = ServiceManager.Instance.PatientService;

        public event EventHandler SaveOkEvent;


        public HemoEventInfo()
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
                    if (_hemoEventInfoRow == null)
                    {
                        try
                        {
                            if (this.ctlUserLongInfo1.patientInfoCheck1._patientDataTable != null && this.ctlUserLongInfo1.patientInfoCheck1._patientDataTable.Rows.Count > 0)
                            {
                                //根据透析号和日期年月。获取数据
                                hemoEventDt = objPatient.GetHemoEventInfo(this.ctlUserLongInfo1.patientInfoCheck1._patientDataTable[0].HEMODIALYSIS_ID, this.TXTCREATERTIME.DateTime, "1");
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
                        hemoEventDt = objPatient.GetHemoEventInfo(_hemoEventInfoRow.HEMODIALYSIS_ID, _hemoEventInfoRow.CREATERTIME, "1");

                    }
                };
                _doWoker.RunWorkerCompleted += (o1, e1) =>
                {
                    if (hemoEventDt.Rows.Count > 0)
                    {
                        this._hemoEventInfoRow = hemoEventDt[0];
                    }
                    else
                    {
                        if (this.ctlUserLongInfo1.patientInfoCheck1._patientDataTable != null && this.ctlUserLongInfo1.patientInfoCheck1._patientDataTable.Rows.Count > 0)
                        {
                            this._hemoEventInfoRow = hemoEventDt.NewMED_HEMO_EVENTINFORow();
                            this._hemoEventInfoRow.ID = Guid.NewGuid().ToString();
                            this._hemoEventInfoRow.HEMODIALYSIS_ID = this.ctlUserLongInfo1.patientInfoCheck1._patientDataTable[0].HEMODIALYSIS_ID;
                            this._hemoEventInfoRow.CREATERTIME = this.TXTCREATERTIME.DateTime == null ? DateTime.Now : this.TXTCREATERTIME.DateTime;
                            this._hemoEventInfoRow.CREATER = HemoApplicationContext.Current.CurrentUser.USER_ID;
                            this._hemoEventInfoRow.EVENTTYPE = "1";
                            this._hemoEventInfoRow.NAME = this.ctlUserLongInfo1.patientInfoCheck1._patientDataTable[0].NAME;
                            this._hemoEventInfoRow.SEX = this.ctlUserLongInfo1.patientInfoCheck1._patientDataTable[0].SEX;
                            this._hemoEventInfoRow.AGE = this.ctlUserLongInfo1.patientInfoCheck1._patientDataTable[0].AGE.ToString();
                            hemoEventDt.AddMED_HEMO_EVENTINFORow(this._hemoEventInfoRow);
                        }
                        else
                        {

                            ClearPanelData(this.printDocumentPanel1 as DevExpress.XtraEditors.PanelControl);
                            this.TXTCREATERTIME.DateTime = DateTime.Now;
                            this.TXTHEMOEVENTDT.DateTime = DateTime.Now;

                        }
                    }
                    BindData(this.printDocumentPanel1 as DevExpress.XtraEditors.PanelControl);

                };
                _doWoker.RunWorkerAsync();
            }

        }

        private void ClearPanelData(DevExpress.XtraEditors.PanelControl panel)
        {
            Utilities.BaseControlInfo.ClearControlText(panel);
            foreach (var ctl in panel.Controls)
            {

                if (ctl is DevExpress.XtraEditors.PanelControl)
                {
                    ClearPanelData(ctl as DevExpress.XtraEditors.PanelControl);
                }
                else if (ctl is System.Windows.Forms.TableLayoutPanel)
                {
                    ClearTableLayoutPanelData(ctl as System.Windows.Forms.TableLayoutPanel);
                }
            }
        }

        private void ClearTableLayoutPanelData(System.Windows.Forms.TableLayoutPanel panel)
        {

            foreach (var ctl in panel.Controls)
            {
                if (ctl is DevExpress.XtraEditors.PanelControl)
                {
                    Utilities.BaseControlInfo.ClearControlText(ctl as DevExpress.XtraEditors.PanelControl);
                }
                else
                {

                }
            }
        }


        /// <summary>
        /// 保存操作直接 保存北
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSave_Click(object sender, EventArgs e)
        {
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

        private void btnPrint_Click(object sender, EventArgs e)
        {
            ControlPrint frm = new ControlPrint(this.printDocumentPanel1);
            frm.ApplyBestSize();
            printPreviewDialog1.Document = (PrintDocument)frm;
            printPreviewDialog1.ShowDialog();
        }


        #region 方法

        private void BindData(DevExpress.XtraEditors.PanelControl panel)
        {
            foreach (var ctl in panel.Controls)
            {
                if (ctl is BaseEdit)
                {
                    (ctl as BaseEdit).BindingDiffControlDataRow(this._hemoEventInfoRow);
                }
                else if (ctl is DevExpress.XtraEditors.PanelControl)
                {
                    BindData(ctl as DevExpress.XtraEditors.PanelControl);
                }
                else if (ctl is CheckedListBoxControl)
                {
                    (ctl as CheckedListBoxControl).BindingDiffCheckedDataRow(this._hemoEventInfoRow);
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
                    (tctl as BaseEdit).BindingDiffControlDataRow(this._hemoEventInfoRow);
                }
                else if (tctl is DevExpress.XtraEditors.PanelControl)
                {
                    BindData(tctl as DevExpress.XtraEditors.PanelControl);
                }
                else if (tctl is CheckedListBoxControl)
                {
                    (tctl as CheckedListBoxControl).BindingDiffCheckedDataRow(this._hemoEventInfoRow);
                }
                else if (tctl is System.Windows.Forms.TableLayoutPanel)
                {
                    BindTableLayoutData(tctl as System.Windows.Forms.TableLayoutPanel);
                }

            }
        }

        #endregion

        private void TXTCONCRETEPROBLEM7EXT_EditValueChanged(object sender, EventArgs e)
        {
            if (this.TXTCONCRETEPROBLEM7EXT.Text.Length > 0)
            {
                this.CHKCONCRETEPROBLEM7.Checked = true;
            }
        }

        private void CHKOTHERINFECT_CheckedChanged(object sender, EventArgs e)
        {
            if (((CheckEdit)sender).Checked)
            {
                this.CHKOTHERINFECTEXT.Checked = true;
            }
        }

        private void HemoEventInfo_Load(object sender, EventArgs e)
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
                InzationData();
            }
        }

        #region 控制事件
        private void TXTHEMOTUBEEXDTIME1_EditValueChanged(object sender, EventArgs e)
        {
            if (TXTHEMOTUBEEXDTIME1.DateTime > DateTime.MinValue)
            {
                CHKHEMOTUBEEXDTIME1NO.Checked = false;
            }
        }

        private void TXTHEMOTUBEEXDTIME2_EditValueChanged(object sender, EventArgs e)
        {
            if (TXTHEMOTUBEEXDTIME2.DateTime > DateTime.MinValue)
            {
                CHKHEMOTUBEEXDTIME2NO.Checked = false;
            }
        }

        private void TXTHEMOTUBEEXDTIME3_EditValueChanged(object sender, EventArgs e)
        {
            if (TXTHEMOTUBEEXDTIME3.DateTime > DateTime.MinValue)
            {
                CHKHEMOTUBEEXDTIME3NO.Checked = false;
            }
        }

        private void TXTHEMOTUBEEXDTIM4_EditValueChanged(object sender, EventArgs e)
        {
            if (TXTHEMOTUBEEXDTIM4.DateTime > DateTime.MinValue)
            {
                CHKHEMOTUBEEXDTIM4NO.Checked = false;
            }
        }

        private void TXTHEMOTUBEEXDTIM5_EditValueChanged(object sender, EventArgs e)
        {
            if (TXTHEMOTUBEEXDTIM5.DateTime > DateTime.MinValue)
            {
                CHKHEMOTUBEEXDTIM5NO.Checked = false;
            }
        }

        private void CHKHEMOTUBEEXDTIME1NO_CheckedChanged(object sender, EventArgs e)
        {
            if (CHKHEMOTUBEEXDTIME1NO.Checked)
            {
                TXTHEMOTUBEEXDTIME1.EditValue = null;
            }
        }

        private void CHKHEMOTUBEEXDTIME2NO_CheckedChanged(object sender, EventArgs e)
        {
            if (CHKHEMOTUBEEXDTIME2NO.Checked)
            {
                TXTHEMOTUBEEXDTIME2.EditValue = null;
            }
        }

        private void CHKHEMOTUBEEXDTIME3NO_CheckedChanged(object sender, EventArgs e)
        {
            if (CHKHEMOTUBEEXDTIME3NO.Checked)
            {
                TXTHEMOTUBEEXDTIME3.EditValue = null;
            }
        }

        private void CHKHEMOTUBEEXDTIM4NO_CheckedChanged(object sender, EventArgs e)
        {
            if (CHKHEMOTUBEEXDTIM4NO.Checked)
            {
                TXTHEMOTUBEEXDTIM4.EditValue = null;

            }
        }

        private void CHKHEMOTUBEEXDTIM5NO_CheckedChanged(object sender, EventArgs e)
        {
            if (CHKHEMOTUBEEXDTIM5NO.Checked)
            {
                TXTHEMOTUBEEXDTIM5.EditValue = null;
            }
        }


        #endregion

        private void BtnCalcle_Click(object sender, EventArgs e)
        {
            if (SaveOkEvent != null)
            {
                SaveOkEvent(null, null);
            }
        }
    }
}
