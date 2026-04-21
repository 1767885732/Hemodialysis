/*----------------------------------------------------------------
 * Copyright (C) 2005 医疗科技发展有限公司
 * 文件功能描述:患者治疗状态控件
 * 创建标识:吕志强-2014年8月2日
 * ----------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.Utils.Win;
using Hemo.Client.Properties;
using Hemo.Client.Modules;
using Hemo.Model;
using Hemo.IService.Machine;
using Hemo.Service;
using Hemo.Utilities;
using Hemo.Client.UI.PatientFixUI;

namespace Hemo.Client.Controls.Common
{
    public partial class ctlGetPatientStatus : DevExpress.XtraEditors.XtraUserControl
    {
        private ctlGetPatientStatusDtl ctlDtl;
        public delegate void ControlFlyEventHandler(object sender, MyMainEventArgs e);
        public event ControlFlyEventHandler ControlFly;
        private IMachine _machine = ServiceManager.Instance.MachineService;

        private ConfigModel.MED_COMMON_ITEMLISTRow _dr;
        private MachineModel.MED_PROCESS_SETDataTable dtDetail; 
        public ConfigModel.MED_COMMON_ITEMLISTRow Dr
        {
            set { _dr = value; }
            get { return _dr; }
        }


        public ctlGetPatientStatus(ConfigModel.MED_COMMON_ITEMLISTRow dr,MachineModel.MED_PROCESS_SETDataTable dt)
        {
            InitializeComponent();
            _dr = dr;
            dtDetail = dt;
            LoadData();
        }

        private void LoadData()
        {
            this.flyoutPanel1.Height = 0;
            this.flyoutPanel1.Width = 130;
            if (_dr != null)
            {
                dtDetail = (MachineModel.MED_PROCESS_SETDataTable) Utility.GetSubTable(dtDetail, " PROCESS_ID = '"+_dr.ITEM_ID +"' and IS_STOP = 0 ");
                if (dtDetail != null)
                {

                    foreach (MachineModel.MED_PROCESS_SETRow r in dtDetail)
                    {
                        this.flyoutPanel1.Height += 43;
                        ctlGetPatientStatusDtl dtl = new ctlGetPatientStatusDtl(r,this);
                        dtl.ControlClick += new ctlGetPatientStatusDtl.ControlClickEventHandler(TestClickEvent);
                        dtl.ControlDoubleClick += new ctlGetPatientStatusDtl.ControlDoubleClickEventHandler(TestDoubleClickEvent);
                        
                        this.flowLayoutPanel1.Controls.Add(dtl);
                    }
                }           
            }             
        }


        private void TestClickEvent(object sender, MyEventArgs e)
        {
            //if (ctlDtl != null)
            //{
            //    ctlDtl.ClearSelectColor();
            //}
            //ctlDtl = (ctlGetPatientStatusDtl)sender;
            //ctlDtl.SetSelectColor();
            JumpInPage(e.Name);
            IsFlyVisible(false);
        }

        void JumpInPage(string name)
        {
            PatientDtlEditMain.pUI.OutChangePage(name);
        }

        private void TestDoubleClickEvent(object sender, MyEventArgs e)
        {
            if (ctlDtl != null)
            {
                ctlDtl.ClearSelectColor();
            }
            ctlDtl = (ctlGetPatientStatusDtl)sender;
            ctlDtl.SetSelectColor();
        }
        private void ctlGetPatientStatus_Load(object sender, EventArgs e)
        {
            this.lblName.Text = _dr.ITEM_NAME.ToString();
            if (_dr.ITEM_NAME.ToString() == "透析开始" || _dr.ITEM_NAME.ToString() == "透析治疗" || _dr.ITEM_NAME.ToString() == "透析结束")
            {

                this.pic1.EditValue = global:: Hemo.Client.Properties.Resources.unchosee;
            }
            else
            { this.pic1.EditValue = global:: Hemo.Client.Properties.Resources.chosee; }

            if (_dr.ITEM_NAME.ToString() != "透析结束")
            {
                this.pic2.EditValue = global::Hemo.Client.Properties.Resources.jiantou;
            }
            else
            {
                this.pic2.Visible = false;
            }
        }


        private void panelControl1_MouseHover(object sender, EventArgs e)
        {
            IsFlyVisible(true);
            if (ctlDtl != null)
            {
                ctlDtl.ClearSelectColor();
            }
            if (ControlFly != null)
            {
                this.ControlFly(this, new MyMainEventArgs());
            }
        }

        public void HideFly()
        {
            this.flyoutPanel1.HidePopup();
        }

        public void IsFlyVisible(bool bl)
        {
            if (bl)
            {
                Point p = new Point();
                p.X = 0;
                p.Y = 35;
                this.flyoutPanel1.Options.Location = p;
                this.flyoutPanel1.Options.AnchorType = PopupToolWindowAnchor.Manual;
                this.flyoutPanel1.Options.AnimationType = PopupToolWindowAnimation.Fade;
                this.flyoutPanel1.ShowPopup();
            }
            else
            {
                this.flyoutPanel1.HidePopup();
            }
        }

        private void panelControl1_MouseLeave(object sender, EventArgs e)
        {
            if (!this.RectangleToScreen(this.ClientRectangle).Contains(Control.MousePosition))
            {
                if (!this.RectangleToScreen(this.flyoutPanel1.ClientRectangle).Contains(MousePosition))
                {
                    IsFlyVisible(false);
                }
            }   
        }
    }

    public class MyMainEventArgs : EventArgs
    { 
       
    }
}
