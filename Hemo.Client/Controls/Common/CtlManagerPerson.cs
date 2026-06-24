/*----------------------------------------------------------------
 * Copyright (C) 2005 医疗科技发展有限公司
 * 文件功能描述:患者治疗卡片控件
 * 创建标识:吕志强-2014年8月2日
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

namespace Hemo.Client.Controls.Common
{
    public partial class CtlManagerPerson : XtraUserControl
    {
        #region 变量
        public delegate void ContainerCtlClickEventHandler(object sender, ContainerCtlEventArgs args);
        public event ContainerCtlClickEventHandler ContainerPanelClick;
        public delegate void ContainerCtlDoubleClickEventHandler(object sender, ContainerCtlEventArgs args);
        public event ContainerCtlDoubleClickEventHandler ContainerPanelDoubleClick;
        public delegate void CtlMouseRightClickEventHandler(object sender, ContainerCtlEventArgs args);
        public event CtlMouseRightClickEventHandler CtlMouseRightClick;

        public DataRow drPatientRow
        {
            set;
            get;
        }

        #endregion

        #region 构造函数

        public CtlManagerPerson(DataRow dr)
        {
            this.InitializeComponent();
            if (dr != null)
            {
                drPatientRow = dr;
                this.labBedName.Text = dr["BED_NUMBERNAME"].ToString();
                this.labPatientName.Text = dr["NAME"].ToString();
                this.lblSex.Text = dr["SEX"].ToString();
                this.labPURIFICATION_MODE.Text = dr["PURIFICATION_MODE_NAME"].ToString();
                this.labDRY_WEIGHT.Text = dr["DRY_WEIGHT"].ToString();
                this.labUFR.Text = dr["UFR"].ToString();
                this.labFIRST_DRUG_DOSAGE_NAME.Text = dr["FIRST_DRUG_DOSAGE_NAME"].ToString();
                this.pic_STATUS_TAG.EditValue = dr["STATUS_TAG"];
                this.patlabel.Text = dr["PAT_LABEL"].ToString();
                this.SetTimeInfo();
            }
           
        }

        #endregion

        #region 方法

        public void SetSelectedEffect()
        {
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(246)))), ((int)(((byte)(135)))), ((int)(((byte)(5)))));
            this.BackgroundImage  = global::Hemo.Client.Properties.Resources.cardCheck;
            this.labPatientName.ForeColor = System.Drawing.Color.White;
            this.lblSex.ForeColor = System.Drawing.Color.White;
            this.labBedName.ForeColor = System.Drawing.Color.White;
            this.BorderStyle = System.Windows.Forms.BorderStyle.None;
        }

        public void ClearSelectedEffect()
        {
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(229)))), ((int)(((byte)(229)))), ((int)(((byte)(229)))));
            this.BackgroundImage = global::Hemo.Client.Properties.Resources.card;
            this.labPatientName.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51))))); 
            this.lblSex.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(102)))), ((int)(((byte)(102))))); 
            this.labBedName.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(246)))), ((int)(((byte)(135)))), ((int)(((byte)(5)))));
        }

        public void SetBorder()
        {
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
        }

        public void ClearBorder()
        {
            this.BorderStyle = System.Windows.Forms.BorderStyle.None;
        }

        public void SetTimeInfo()
        {           
         //
        }



        #endregion

        #region 事件

        private void pnlTreatmentContainer_Click(object sender, EventArgs e)
        {
            if (this.ContainerPanelClick != null)
            {
                ContainerCtlEventArgs args = new ContainerCtlEventArgs();

                this.ContainerPanelClick(this, args);
            }
        }

        private void pnlTreatmentContainer_DoubleClick(object sender, EventArgs e)
        {
            if (this.ContainerPanelDoubleClick != null)
            {
                ContainerCtlEventArgs args = new ContainerCtlEventArgs();

                this.ContainerPanelDoubleClick(this, args);
            }
        }


        #endregion

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

        private void pnlTreatmentContainer_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                if (CtlMouseRightClick != null)
                {
                    ContainerCtlEventArgs args = new ContainerCtlEventArgs();
                    this.CtlMouseRightClick(this,args);
                 }
            }
        }

    }

    public class ContainerCtlEventArgs : EventArgs
    {
        public PatientModel.MED_PATIENTSRow Patient
        {
            set;
            get;
        }
    }
}
