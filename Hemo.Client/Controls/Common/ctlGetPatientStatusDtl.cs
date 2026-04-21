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
using Hemo.Model;

namespace Hemo.Client.Controls.Common
{
    public partial class ctlGetPatientStatusDtl : DevExpress.XtraEditors.XtraUserControl
    {

        public delegate void ControlClickEventHandler(object sender, MyEventArgs e);
        public event ControlClickEventHandler ControlClick;
        public delegate void ControlDoubleClickEventHandler(object sender, MyEventArgs e);
        public event ControlDoubleClickEventHandler ControlDoubleClick;
        private MachineModel.MED_PROCESS_SETRow _t;
        private ctlGetPatientStatus _pctl = null;


        public ctlGetPatientStatusDtl(MachineModel.MED_PROCESS_SETRow t,ctlGetPatientStatus pctl)
        {
            InitializeComponent();
            _t = t;
            _pctl = pctl;
            this.label1.Text =t.NAME;
        }

        private void ctlGetPatientStatusDtl_Load(object sender, EventArgs e)
        {
            if (_t != null)
            {
                if (_t.PROCESS_NAME == "透析开始" || _t.PROCESS_NAME == "透析治疗" || _t.PROCESS_NAME == "透析结束")
                {
                    this.pic1.EditValue = global:: Hemo.Client.Properties.Resources.unchosee;
                }
                else
                {
                    this.pic1.EditValue = global:: Hemo.Client.Properties.Resources.chosee;
                }
            }

            else
            {
                this.pic1.EditValue = global:: Hemo.Client.Properties.Resources.chosee;
            }
        }

        public void SetSelectColor()
        {
            this.panelControl1.BackColor = Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(245)))));
        }

        public void ClearSelectColor()
        {
            this.panelControl1.BackColor = Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
        }


        private void panelControl1_Click(object sender, EventArgs e)
        {
            if (ControlClick != null)
            {
                MyEventArgs args = new MyEventArgs();
                args.Name = _t.NAME;
                this.ControlClick(this, args);
            }
        }

        private void panelControl1_DoubleClick(object sender, EventArgs e)
        {
            if (ControlDoubleClick != null)
            {
                MyEventArgs args = new MyEventArgs();
                args.Name = _t.NAME;
                this.ControlDoubleClick(this, args);
            }
        }

        public void panelControl1_MouseHover(object sender, EventArgs e)
        {
            _pctl.IsFlyVisible(true);
            if (ControlDoubleClick != null)
            {
                MyEventArgs args = new MyEventArgs();
                args.Name = _t.NAME;
                this.ControlDoubleClick(this, args);
            }
        }
    }
    public class MyEventArgs :EventArgs
    {
        public string Name
        {
          set;
          get;
        }
    }
}
