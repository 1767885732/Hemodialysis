/*----------------------------------------------------------------
 * Copyright (C) 2005 医疗科技发展有限公司
 * 文件功能描述:患者治疗时人员的控件
 * 创建标识:吕志强-2014年8月2日
 * 
 * 修改时间:2014年12月18日
 * 修改人:贺建操
 * 修改描述:修复系统响应速度慢的问题
 * 
 * 修改时间:2012年4月28日
 * 修改人:顾伟伟
 * 修改描述:通用窗体取值、赋值、清空值、验证控件值方法
 * 
 * 修改时间:2012年9月4日
 * 修改人:顾伟伟
 * 修改描述:增加全局调用方法验证
 * ----------------------------------------------------------------*/

using System;
using System.Drawing;
using DevExpress.XtraEditors;
using Hemo.IService.Erythropoietin;
using System.Linq;
using System.Data;
using Hemo.Model;
using Hemo.Service;
using Hemo.Utilities;
using Hemo.IService.Config;
using System.Windows.Forms;
using DevExpress.XtraCharts;
using DashStyle = System.Drawing.Drawing2D.DashStyle;
using Hemo.IService.PatientSchedule;

namespace Hemo.Client.Controls.Treatment
{
    public partial class CtlTreatmentPerson : XtraUserControl
    {
        #region 变量

        private double _initializeSeconds;
        private DateTime _treamentDate;
        public delegate void ContainerPanelClickEventHandler(object sender, ContainerPanelEventArgs args);
        public event ContainerPanelClickEventHandler ContainerPanelClick;
        public delegate void ContainerPanelDoubleClickEventHandler(object sender, ContainerPanelEventArgs args);
        public event ContainerPanelDoubleClickEventHandler ContainerPanelDoubleClick;
        private IPatientSchedule _objscheduleService = ServiceManager.Instance.PatientSchedule;
        private IErythropoietin _erythropoietinService = ServiceManager.Instance.ErythropoietinService;
        private IHemodialysis objHemodialysisService = ServiceManager.Instance.HemodialysisService;
        private HemodialysisModel.MED_CURE_MAINRow _currentCureMainRow = null;
        private int flickerTicks = 0;

        #endregion

        #region 属性

        private DateTime BeginErythropoietinCreateDate
        {
            get
            {
                return Utility.CDate(this._treamentDate.ToString("yyyy-MM-dd 00:00:00"));
            }
        }

        private DateTime EndErythropoietinCreateDate
        {
            get
            {
                return Utility.CDate(this._treamentDate.ToString("yyyy-MM-dd 23:59:59"));
            }
        }

        public PatientScheduleModel.MED_PATIENT_SCHEDULERow PatientScheduleRow
        {
            set;
            get;
        }

        /// <summary>
        /// 病人信息 
        /// </summary>
        public PatientModel.MED_PATIENTSRow PatientRow
        {
            set;
            get;
        }

        /// <summary>
        /// 设备信息
        /// </summary>
        public MachineModel.MED_DIALYSIS_MACHINERow MachineRow
        {
            set;
            get;
        }

        public bool IsAllowSyncTime
        {
            private set;
            get;
        }

        public HemodialysisModel.MED_HEMO_RECIPERow CurentRecipeRow
        {
            get;
            set;
        }

        public HemodialysisModel.MED_CURE_MAINRow CureMainRow
        {
            get { return _currentCureMainRow; }
            set { _currentCureMainRow = value; }
        }

        #endregion

        #region 构造函数

        public CtlTreatmentPerson(ConfigModel.MED_COMMON_ITEMLISTRow bedRow, ConfigModel.MED_COMMON_ITEMLISTRow purifierRow, MachineModel.MED_DIALYSIS_MACHINERow machineRow, PatientModel.MED_PATIENTSRow patientRow, HemodialysisModel.MED_CURE_MAINRow cureMainRow, HemodialysisModel.MED_HEMO_RECIPERow recipeRow, PatientScheduleModel.MED_PATIENT_SCHEDULERow patientScheduleRow, DateTime treamentDate)
        {
            this.InitializeComponent();

            this._treamentDate = treamentDate;

            this.PatientScheduleRow = patientScheduleRow;
            this.PatientRow = patientRow;
            this.MachineRow = machineRow;
            Logger.WriteErrorLogContet($"CtlTreatmentPerson: patientRow is {(patientRow == null ? "null" : patientRow.NAME)}");
            this.labBedName.Text = bedRow.ITEM_VALUE;
            this.labPatientName.Text = patientRow.NAME;
            this.lblSex.Text = patientRow.SEX;
            this.patLabel.Text = patientRow.IsPAT_LABELNull()?"":PatientRow.PAT_LABEL;

            this.lblBanci.Visible = false;            
            if (patientScheduleRow.AREANAME.Equals("CRRT"))
            {
                this.Size = new Size(230, 180);
                this.BackgroundImage = global::Hemo.Client.Properties.Resources.patient_crrt2;
                this.lblBanci.Visible = true;
                this.lblBanci.Location = new Point(15, 56);
                this.labelControl1.Location = new Point(15, 101);
                this.labPURIFICATION_MODE.Location = new Point(67, 101);
                this.labelControl6.Location = new Point(121, 101);
                this.labFIRST_PURIFIER_MODEL.Location = new Point(164, 101);
                this.labelControl2.Location = new Point(15, 146);
                this.labFREQUENCY_HOURS.Location = new Point(67, 146);
                this.labelControl3.Location = new Point(118, 146);
                this.labTime.Location = new Point(172, 146);
                this.lbDrugTips.Location = new Point(12, 162);
                this.labErythropoietinMsg.Location = new Point(119, 162);

                if (patientScheduleRow.BANCI_ID.Equals("1"))
                {
                    this.lblBanci.Text = "排班：" + patientScheduleRow.DIALYSIS_DATE.ToString("yyyy-MM-dd") + "   " + "白天";
                }
                else if (patientScheduleRow.BANCI_ID.Equals("2"))
                {
                    this.lblBanci.Text = "排班：" + patientScheduleRow.DIALYSIS_DATE.ToString("yyyy-MM-dd") + "   " + "小夜";
                }
                else if (patientScheduleRow.BANCI_ID.Equals("3"))
                {
                    this.lblBanci.Text = "排班：" + patientScheduleRow.DIALYSIS_DATE.ToString("yyyy-MM-dd") + "   " + "大夜";
                }
            }
            else
            {
                this.Size = new Size(230, 140);
                this.BackgroundImage = global::Hemo.Client.Properties.Resources.card;
                this.labelControl1.Location = new Point(15, 56);
                this.labPURIFICATION_MODE.Location = new Point(67, 56);
                this.labelControl6.Location = new Point(121, 56);
                this.labFIRST_PURIFIER_MODEL.Location = new Point(164, 56);
                this.labelControl2.Location = new Point(15, 101);
                this.labFREQUENCY_HOURS.Location = new Point(67, 101);
                this.labelControl3.Location = new Point(118, 101);
                this.labTime.Location = new Point(172, 101);
                this.lbDrugTips.Location = new Point(12, 123);
                this.labErythropoietinMsg.Location = new Point(119, 123);
            }

            if (cureMainRow != null)
            {
                _currentCureMainRow = cureMainRow;
                //this._initializeSeconds = Utility.CDouble(cureMainRow.FREQUENCY_HOURS.ToString()) * 3600;
                //this.labPURIFICATION_MODE.Text = cureMainRow["PURIFICATION_MODE_NAME"].ToString();
                //this.labFREQUENCY_HOURS.Text = string.Format("{0}H", cureMainRow.FREQUENCY_HOURS);
                this._initializeSeconds = Utility.CDouble((cureMainRow.FREQUENCY_HOURS * 3600 + Utility.CDouble(cureMainRow.FREQUENCY_MINUTE) * 60).ToString());
                this.labPURIFICATION_MODE.Text = cureMainRow["PURIFICATION_MODE_NAME"].ToString();
                double minute = 0;
                if (!cureMainRow.IsFREQUENCY_MINUTENull())
                {
                    minute = Math.Round(Utility.CDouble(cureMainRow.FREQUENCY_MINUTE) / 60, 2);
                }

                this.labFREQUENCY_HOURS.Text = string.Format("{0}H", cureMainRow.FREQUENCY_HOURS + minute);
                this.Tag = cureMainRow["CURE_ID"].ToString();
            }
            else
            {
                this.Tag = string.Empty;

                if (recipeRow != null)
                {
                    CurentRecipeRow = recipeRow;
                    this._initializeSeconds = Utility.CDouble(recipeRow.FREQUENCY_HOURS.ToString()) * 3600 + Utility.CDouble(recipeRow.FREQUENCY_MINUTE) * 60;
                    double minute = 0;
                    if (!recipeRow.IsFREQUENCY_MINUTENull())
                    {
                        minute = Math.Round(Utility.CDouble(recipeRow.FREQUENCY_MINUTE) / 60, 2);
                    }

                    this.labPURIFICATION_MODE.Text = recipeRow["PURIFICATION_MODE_NAME"].ToString();
                    this.labFREQUENCY_HOURS.Text = string.Format("{0}H", Utility.CDouble(recipeRow.FREQUENCY_HOURS.ToString()) + minute);
                }
                else
                {
                    this.labPURIFICATION_MODE.Text = "！";
                    this.labPURIFICATION_MODE.Appearance.ForeColor = Color.Red;
                }
            }

            if (purifierRow != null)
                this.labFIRST_PURIFIER_MODEL.Text = purifierRow.ITEM_NAME;

            ErythropoietinModel.MED_ERYTHROPOIETINDataTable erythropoietinDataTable = this._erythropoietinService.GetErythropoietinListByTimeSpan(patientRow.HEMODIALYSIS_ID, this.BeginErythropoietinCreateDate, this.EndErythropoietinCreateDate);

            if (erythropoietinDataTable.Rows.Count > 0)
            {
                this.labErythropoietinMsg.Text = "有要执行的促红素";

                this.labErythropoietinMsg.Visible = true;
            }
            else
                this.labErythropoietinMsg.Visible = false;

            this.SetTimeInfo();
        }

        #endregion

        #region 方法

        public void SetCureMain(HemodialysisModel.MED_CURE_MAINRow cureMainRow)
        {
            this._currentCureMainRow = cureMainRow;
        }

        public void SetSelectedEffect()
        {
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(246)))), ((int)(((byte)(135)))), ((int)(((byte)(5)))));
            this.BackgroundImage = PatientScheduleRow.AREANAME.Equals("CRRT") ? global::Hemo.Client.Properties.Resources.patient_crrt1 : global::Hemo.Client.Properties.Resources.cardCheck;
            this.labPatientName.ForeColor = System.Drawing.Color.White;
            this.lblSex.ForeColor = System.Drawing.Color.White;
            this.labBedName.ForeColor = System.Drawing.Color.White;
            //2026-05-28 刘建超 设置患者标注选中文字背景色不变
            this.patLabel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(253)))), ((int)(((byte)(249)))), ((int)(((byte)(245)))));

            //this.pnlTreatmentContainer.BackColor = Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(226)))), ((int)(((byte)(143)))));
            if (!this.PatientScheduleRow.IsEND_TIMENull())
            {
                this.pic.EditValue = global::Hemo.Client.Properties.Resources.wancheng2;
            }
            else
            {
                if (!this.PatientScheduleRow.IsSTART_TIMENull())
                {
                    this.pic.EditValue = global::Hemo.Client.Properties.Resources.zhiliaozhong2;
                }
                else
                {
                    this.pic.EditValue = global::Hemo.Client.Properties.Resources.zhunbei2;
                }
            }
        }

        public void ClearSelectedEffect()
        {
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(229)))), ((int)(((byte)(229)))), ((int)(((byte)(229)))));
            this.BackgroundImage = PatientScheduleRow.AREANAME.Equals("CRRT") ? global::Hemo.Client.Properties.Resources.patient_crrt2 : global::Hemo.Client.Properties.Resources.card;
            this.labPatientName.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51))))); ;
            this.lblSex.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(102)))), ((int)(((byte)(102))))); ;
            this.labBedName.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(246)))), ((int)(((byte)(135)))), ((int)(((byte)(5))))); ;
            this.patLabel.BackColor = Color.White;
            //this.pnlTreatmentContainer.BackColor = Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(234)))), ((int)(((byte)(247)))));
            if (!this.PatientScheduleRow.IsEND_TIMENull())
            {
                this.pic.EditValue = global::Hemo.Client.Properties.Resources.wancheng;
            }
            else
            {
                if (!this.PatientScheduleRow.IsSTART_TIMENull())
                {
                    this.pic.EditValue = global::Hemo.Client.Properties.Resources.zhiliaozhong;
                }
                else
                {
                    this.lbDrugTips.Visible = false;

                    this.pic.EditValue = global::Hemo.Client.Properties.Resources.zhunbei;
                }
            }
        }

        public void SetTimeInfo()
        {
            if (!this.PatientScheduleRow.IsEND_TIMENull())
            {
                this.IsAllowSyncTime = false;
                var realTime = this.PatientScheduleRow.END_TIME - this.PatientScheduleRow.START_TIME;
                this.labTime.Text = string.Format("{0}:{1}", realTime.Hours.ToString(), realTime.Minutes.ToString());// this.labFREQUENCY_HOURS.Text;

                this.pic.EditValue = global::Hemo.Client.Properties.Resources.wancheng;
                this.pic.ToolTip = "结束治疗";
                this.labTime.Appearance.ForeColor = System.Drawing.Color.Red;
                if (this.PatientScheduleRow.IsFOCUS_LEVELNull())
                {
                    this.pic_FocuseLevel.Visible = false;
                }
                //this.pic.Enabled = false;
            }
            else
            {
                if (!this.PatientScheduleRow.IsSTART_TIMENull())
                {
                    TimeSpan ts1 = new TimeSpan(DateTime.Now.Ticks);
                    TimeSpan ts2 = new TimeSpan(this.PatientScheduleRow.START_TIME.Ticks);
                    TimeSpan ts = ts1.Subtract(ts2).Duration();

                    this._initializeSeconds -= ts.TotalSeconds;

                    this.IsAllowSyncTime = this._initializeSeconds > 0;

                    this.pic.EditValue = global::Hemo.Client.Properties.Resources.zhiliaozhong;
                    this.pic.ToolTip = "开始治疗";
                    this.labTime.Appearance.ForeColor = System.Drawing.Color.Green;

                }
                else
                {
                    if (_currentCureMainRow != null)
                    {
                        this._initializeSeconds = Utility.CDouble((_currentCureMainRow.FREQUENCY_HOURS * 3600 + Utility.CDouble(_currentCureMainRow.FREQUENCY_MINUTE) * 60).ToString());
                    }
                    else
                    {
                        if (CurentRecipeRow != null)
                        {
                            this._initializeSeconds = Utility.CDouble(CurentRecipeRow.FREQUENCY_HOURS.ToString()) * 3600 + Utility.CDouble(CurentRecipeRow.FREQUENCY_MINUTE) * 60; ;
                        }
                        else
                        {
                            this._initializeSeconds = Utility.CDouble("4") * 3600;

                        }
                    }


                    this.labTime.Text = "0";
                    this.labTime.Appearance.ForeColor = System.Drawing.Color.Black;

                    this.IsAllowSyncTime = false;
                    this.pic.EditValue = global::Hemo.Client.Properties.Resources.zhunbei;
                    this.pic.ToolTip = "准备治疗";

                    //  this.labTime.Appearance.ForeColor = System.Drawing.Color.Yellow;

                }
            }
            if (this.PatientScheduleRow.IsFOCUS_LEVELNull())
            {
                this.pic_FocuseLevel.Visible = false;
            }
            else if (this.PatientScheduleRow.FOCUS_LEVEL.Equals("1"))
            {
                this.pic_FocuseLevel.Visible = true;
                this.pic_FocuseLevel.EditValue = global::Hemo.Client.Properties.Resources.common;
                this.pic_FocuseLevel.ToolTip = "病重";
            }
            else if (this.PatientScheduleRow.FOCUS_LEVEL.Equals("2"))
            {
                this.pic_FocuseLevel.Visible = true;
                this.pic_FocuseLevel.EditValue = global::Hemo.Client.Properties.Resources.bad;
                this.pic_FocuseLevel.ToolTip = "病危";
            }
            else
            {
                this.pic_FocuseLevel.EditValue = global::Hemo.Client.Properties.Resources.custom;
                this.pic_FocuseLevel.ToolTip = "一般";
            }
        }

        /// <summary>
        /// 同步时间
        /// </summary>
        /// <param name="ts"></param>
        public void SyncTime(out TimeSpan ts)
        {
            this.IsAllowSyncTime = this._initializeSeconds > 0;

            ts = TimeSpan.FromSeconds(this._initializeSeconds);

            this.labTime.Text = string.Format("{0}:{1}:{2}", ts.Hours, ts.Minutes, ts.Seconds);

            this._initializeSeconds--;

            #region ToShowDrugTips
            flickerTicks++;
            bool IsShow = false;

            //this.PatientScheduleRow = _objscheduleService.GetPatientScheduleSignle(this.PatientScheduleRow.DIALYSIS_DATE, this.PatientScheduleRow.HEMODIALYSIS_ID)[0];

            var ds = objHemodialysisService.GetValidCureDrugByHemoRecipeID(this.PatientScheduleRow.HEMODIALYSIS_ID, PatientScheduleRow.RECIPE_ID);//.GetCureDrugByCureID(pCureID);
            //var ds = objHemodialysisService.GetValidCureDrugByHemoID(this.PatientScheduleRow.HEMODIALYSIS_ID,this._treamentDate);//.GetCureDrugByCureID(pCureID);

            var _CureDrugDatatable = new HemodialysisModel.MED_CURE_DRUGDataTable();

            //对于新开立的医嘱进行拷贝
            ds.Where(i => i.STATUS == "0").CopyToDataTable<HemodialysisModel.MED_CURE_DRUGRow>(_CureDrugDatatable, LoadOption.PreserveChanges);//|| i.STATUS == "2"

            if (_CureDrugDatatable != null && _CureDrugDatatable.Rows.Count > 0)
            {
                IsShow = true;
            }
            else
                IsShow = false;
            this.lbDrugTips.Visible = IsShow;
            if (this.lbDrugTips.Visible)
            {
                this.lbDrugTips.Text = "有    新    医    嘱";
                if (flickerTicks % 2 == 0)
                {
                    this.lbDrugTips.ForeColor = Color.Red;
                }
                else
                {
                    this.lbDrugTips.ForeColor = Color.PaleVioletRed;
                }
            }
            else if (ts.Hours == 0 && ts.Minutes < 2)
            {
                this.lbDrugTips.Visible = true;
                this.lbDrugTips.Text = "距离透析结束还有不到2分钟";

                if (flickerTicks % 2 == 0)
                {
                    this.lbDrugTips.ForeColor = Color.Red;
                }
                else
                {
                    this.lbDrugTips.ForeColor = Color.PaleVioletRed;
                }
                //透析倒计时结束的话就结束治疗
                if (ts.Hours == 0 && ts.Minutes == 0 && ts.Seconds == 0)
                {
                    //这边如果直接结束的话，是否会有影响。比如没有执行医嘱怎么办。他的出库信息不完整怎么办。会有一系列的问题出现。所以不能直接结束治疗 。有待商榷！！
                }
            }
            #endregion

        }

        #endregion

        #region 事件
        private void panelControl1_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
        {
            DevExpress.XtraEditors.PanelControl pnl = (DevExpress.XtraEditors.PanelControl)sender;
            Pen pen = new Pen(System.Drawing.Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(114)))), ((int)(((byte)(197))))));
            pen.DashStyle = DashStyle.Dot;
            e.Graphics.DrawLine(pen, 0, 0, 0, pnl.Height - 0);
            e.Graphics.DrawLine(pen, 0, 0, pnl.Width - 0, 0);
            e.Graphics.DrawLine(pen, pnl.Width - 1, pnl.Height - 1, 0, pnl.Height - 1);
            e.Graphics.DrawLine(pen, pnl.Width - 1, pnl.Height - 1, pnl.Width - 1, 0);
        }

        private void pnlTreatmentContainer_Paint(object sender, PaintEventArgs e)
        {

        }
        private void pnlTreatmentContainer_Click(object sender, EventArgs e)
        {
            if (this.ContainerPanelClick != null)
            {
                ContainerPanelEventArgs args = new ContainerPanelEventArgs();

                this.ContainerPanelClick(this, args);
            }
        }

        private void pnlTreatmentContainer_DoubleClick(object sender, EventArgs e)
        {
            if (this.ContainerPanelDoubleClick != null)
            {
                ContainerPanelEventArgs args = new ContainerPanelEventArgs();

                this.ContainerPanelDoubleClick(this, args);
            }
        }

        #endregion
    }

    public class ContainerPanelEventArgs : EventArgs
    {
        public PatientModel.MED_PATIENTSRow Patient
        {
            set;
            get;
        }
    }
}
