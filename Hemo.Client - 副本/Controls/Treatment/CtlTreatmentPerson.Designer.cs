namespace Hemo.Client.Controls.Treatment
{
    partial class CtlTreatmentPerson
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.lbDrugTips = new DevExpress.XtraEditors.LabelControl();
            this.lblSex = new DevExpress.XtraEditors.LabelControl();
            this.labelControl6 = new DevExpress.XtraEditors.LabelControl();
            this.labFIRST_PURIFIER_MODEL = new DevExpress.XtraEditors.LabelControl();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.labTime = new DevExpress.XtraEditors.LabelControl();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.labFREQUENCY_HOURS = new DevExpress.XtraEditors.LabelControl();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.labPURIFICATION_MODE = new DevExpress.XtraEditors.LabelControl();
            this.labPatientName = new DevExpress.XtraEditors.LabelControl();
            this.pic_FocuseLevel = new DevExpress.XtraEditors.PictureEdit();
            this.pic = new DevExpress.XtraEditors.PictureEdit();
            this.labBedName = new DevExpress.XtraEditors.LabelControl();
            this.labErythropoietinMsg = new DevExpress.XtraEditors.LabelControl();
            this.lblBanci = new DevExpress.XtraEditors.LabelControl();
            ((System.ComponentModel.ISupportInitialize)(this.pic_FocuseLevel.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pic.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // lbDrugTips
            // 
            this.lbDrugTips.Appearance.ForeColor = System.Drawing.Color.Red;
            this.lbDrugTips.Location = new System.Drawing.Point(12, 162);
            this.lbDrugTips.Margin = new System.Windows.Forms.Padding(0);
            this.lbDrugTips.Name = "lbDrugTips";
            this.lbDrugTips.Size = new System.Drawing.Size(96, 14);
            this.lbDrugTips.TabIndex = 15;
            this.lbDrugTips.Text = "有    新    医    嘱";
            this.lbDrugTips.Visible = false;
            this.lbDrugTips.Click += new System.EventHandler(this.pnlTreatmentContainer_Click);
            this.lbDrugTips.DoubleClick += new System.EventHandler(this.pnlTreatmentContainer_DoubleClick);
            // 
            // lblSex
            // 
            this.lblSex.Location = new System.Drawing.Point(63, 6);
            this.lblSex.Name = "lblSex";
            this.lblSex.Size = new System.Drawing.Size(4, 14);
            this.lblSex.TabIndex = 16;
            this.lblSex.Text = "-";
            this.lblSex.Click += new System.EventHandler(this.pnlTreatmentContainer_Click);
            this.lblSex.DoubleClick += new System.EventHandler(this.pnlTreatmentContainer_DoubleClick);
            // 
            // labelControl6
            // 
            this.labelControl6.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(114)))), ((int)(((byte)(197)))));
            this.labelControl6.Location = new System.Drawing.Point(121, 101);
            this.labelControl6.Name = "labelControl6";
            this.labelControl6.Size = new System.Drawing.Size(36, 14);
            this.labelControl6.TabIndex = 20;
            this.labelControl6.Text = "净化器";
            this.labelControl6.Click += new System.EventHandler(this.pnlTreatmentContainer_Click);
            this.labelControl6.DoubleClick += new System.EventHandler(this.pnlTreatmentContainer_DoubleClick);
            // 
            // labFIRST_PURIFIER_MODEL
            // 
            this.labFIRST_PURIFIER_MODEL.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(114)))), ((int)(((byte)(197)))));
            this.labFIRST_PURIFIER_MODEL.Location = new System.Drawing.Point(164, 101);
            this.labFIRST_PURIFIER_MODEL.Name = "labFIRST_PURIFIER_MODEL";
            this.labFIRST_PURIFIER_MODEL.Size = new System.Drawing.Size(4, 14);
            this.labFIRST_PURIFIER_MODEL.TabIndex = 10;
            this.labFIRST_PURIFIER_MODEL.Text = "-";
            this.labFIRST_PURIFIER_MODEL.Click += new System.EventHandler(this.pnlTreatmentContainer_Click);
            this.labFIRST_PURIFIER_MODEL.DoubleClick += new System.EventHandler(this.pnlTreatmentContainer_DoubleClick);
            // 
            // labelControl3
            // 
            this.labelControl3.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(114)))), ((int)(((byte)(197)))));
            this.labelControl3.Location = new System.Drawing.Point(118, 146);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(48, 14);
            this.labelControl3.TabIndex = 15;
            this.labelControl3.Text = "治疗时间";
            this.labelControl3.Click += new System.EventHandler(this.pnlTreatmentContainer_Click);
            this.labelControl3.DoubleClick += new System.EventHandler(this.pnlTreatmentContainer_DoubleClick);
            // 
            // labTime
            // 
            this.labTime.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labTime.Location = new System.Drawing.Point(172, 146);
            this.labTime.Name = "labTime";
            this.labTime.Size = new System.Drawing.Size(10, 14);
            this.labTime.TabIndex = 14;
            this.labTime.Text = "--";
            this.labTime.Click += new System.EventHandler(this.pnlTreatmentContainer_Click);
            this.labTime.DoubleClick += new System.EventHandler(this.pnlTreatmentContainer_DoubleClick);
            // 
            // labelControl2
            // 
            this.labelControl2.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(114)))), ((int)(((byte)(197)))));
            this.labelControl2.Location = new System.Drawing.Point(15, 146);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(48, 14);
            this.labelControl2.TabIndex = 15;
            this.labelControl2.Text = "预定时间";
            this.labelControl2.Click += new System.EventHandler(this.pnlTreatmentContainer_Click);
            this.labelControl2.DoubleClick += new System.EventHandler(this.pnlTreatmentContainer_DoubleClick);
            // 
            // labFREQUENCY_HOURS
            // 
            this.labFREQUENCY_HOURS.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(114)))), ((int)(((byte)(197)))));
            this.labFREQUENCY_HOURS.Location = new System.Drawing.Point(67, 146);
            this.labFREQUENCY_HOURS.Name = "labFREQUENCY_HOURS";
            this.labFREQUENCY_HOURS.Size = new System.Drawing.Size(4, 14);
            this.labFREQUENCY_HOURS.TabIndex = 2;
            this.labFREQUENCY_HOURS.Text = "-";
            this.labFREQUENCY_HOURS.Click += new System.EventHandler(this.pnlTreatmentContainer_Click);
            this.labFREQUENCY_HOURS.DoubleClick += new System.EventHandler(this.pnlTreatmentContainer_DoubleClick);
            // 
            // labelControl1
            // 
            this.labelControl1.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(114)))), ((int)(((byte)(197)))));
            this.labelControl1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.labelControl1.Location = new System.Drawing.Point(15, 101);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(48, 14);
            this.labelControl1.TabIndex = 15;
            this.labelControl1.Text = "治疗项目";
            this.labelControl1.Click += new System.EventHandler(this.pnlTreatmentContainer_Click);
            this.labelControl1.DoubleClick += new System.EventHandler(this.pnlTreatmentContainer_DoubleClick);
            // 
            // labPURIFICATION_MODE
            // 
            this.labPURIFICATION_MODE.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(114)))), ((int)(((byte)(197)))));
            this.labPURIFICATION_MODE.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            this.labPURIFICATION_MODE.Location = new System.Drawing.Point(67, 101);
            this.labPURIFICATION_MODE.Name = "labPURIFICATION_MODE";
            this.labPURIFICATION_MODE.Size = new System.Drawing.Size(4, 14);
            this.labPURIFICATION_MODE.TabIndex = 1;
            this.labPURIFICATION_MODE.Text = "-";
            this.labPURIFICATION_MODE.Click += new System.EventHandler(this.pnlTreatmentContainer_Click);
            this.labPURIFICATION_MODE.DoubleClick += new System.EventHandler(this.pnlTreatmentContainer_DoubleClick);
            // 
            // labPatientName
            // 
            this.labPatientName.Appearance.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labPatientName.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.labPatientName.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.labPatientName.Location = new System.Drawing.Point(2, 4);
            this.labPatientName.Name = "labPatientName";
            this.labPatientName.Size = new System.Drawing.Size(56, 22);
            this.labPatientName.TabIndex = 2;
            this.labPatientName.Text = "-";
            this.labPatientName.Click += new System.EventHandler(this.pnlTreatmentContainer_Click);
            this.labPatientName.DoubleClick += new System.EventHandler(this.pnlTreatmentContainer_DoubleClick);
            // 
            // pic_FocuseLevel
            // 
            this.pic_FocuseLevel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.pic_FocuseLevel.Location = new System.Drawing.Point(136, 5);
            this.pic_FocuseLevel.Name = "pic_FocuseLevel";
            this.pic_FocuseLevel.Properties.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.pic_FocuseLevel.Properties.Appearance.Options.UseBackColor = true;
            this.pic_FocuseLevel.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.pic_FocuseLevel.Properties.ReadOnly = true;
            this.pic_FocuseLevel.Properties.ShowMenu = false;
            this.pic_FocuseLevel.Properties.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Zoom;
            this.pic_FocuseLevel.Size = new System.Drawing.Size(20, 20);
            this.pic_FocuseLevel.TabIndex = 8;
            this.pic_FocuseLevel.Click += new System.EventHandler(this.pnlTreatmentContainer_Click);
            this.pic_FocuseLevel.DoubleClick += new System.EventHandler(this.pnlTreatmentContainer_DoubleClick);
            // 
            // pic
            // 
            this.pic.Location = new System.Drawing.Point(81, 2);
            this.pic.Name = "pic";
            this.pic.Properties.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.pic.Properties.Appearance.Options.UseBackColor = true;
            this.pic.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.pic.Properties.ReadOnly = true;
            this.pic.Properties.ShowMenu = false;
            this.pic.Size = new System.Drawing.Size(40, 25);
            this.pic.TabIndex = 8;
            this.pic.Click += new System.EventHandler(this.pnlTreatmentContainer_Click);
            this.pic.DoubleClick += new System.EventHandler(this.pnlTreatmentContainer_DoubleClick);
            // 
            // labBedName
            // 
            this.labBedName.Appearance.Font = new System.Drawing.Font("Tahoma", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labBedName.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(246)))), ((int)(((byte)(135)))), ((int)(((byte)(5)))));
            this.labBedName.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            this.labBedName.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.labBedName.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.labBedName.Location = new System.Drawing.Point(168, 3);
            this.labBedName.Name = "labBedName";
            this.labBedName.Size = new System.Drawing.Size(40, 25);
            this.labBedName.TabIndex = 0;
            this.labBedName.Text = "1";
            this.labBedName.Click += new System.EventHandler(this.pnlTreatmentContainer_Click);
            this.labBedName.DoubleClick += new System.EventHandler(this.pnlTreatmentContainer_DoubleClick);
            // 
            // labErythropoietinMsg
            // 
            this.labErythropoietinMsg.Appearance.ForeColor = System.Drawing.Color.Red;
            this.labErythropoietinMsg.Location = new System.Drawing.Point(119, 162);
            this.labErythropoietinMsg.Margin = new System.Windows.Forms.Padding(0);
            this.labErythropoietinMsg.Name = "labErythropoietinMsg";
            this.labErythropoietinMsg.Size = new System.Drawing.Size(96, 14);
            this.labErythropoietinMsg.TabIndex = 15;
            this.labErythropoietinMsg.Text = "有要执行的促红素";
            this.labErythropoietinMsg.Visible = false;
            this.labErythropoietinMsg.Click += new System.EventHandler(this.pnlTreatmentContainer_Click);
            this.labErythropoietinMsg.DoubleClick += new System.EventHandler(this.pnlTreatmentContainer_DoubleClick);
            // 
            // lblBanci
            // 
            this.lblBanci.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(114)))), ((int)(((byte)(197)))));
            this.lblBanci.Location = new System.Drawing.Point(15, 56);
            this.lblBanci.Margin = new System.Windows.Forms.Padding(0);
            this.lblBanci.Name = "lblBanci";
            this.lblBanci.Size = new System.Drawing.Size(136, 14);
            this.lblBanci.TabIndex = 21;
            this.lblBanci.Text = "排班：2017-11-20   白天";
            this.lblBanci.Visible = false;
            // 
            // CtlTreatmentPerson
            // 
            this.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.Appearance.Image = global::Hemo.Client.Properties.Resources.cardCheck;
            this.Appearance.Options.UseBackColor = true;
            this.Appearance.Options.UseImage = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::Hemo.Client.Properties.Resources.patient_crrt2;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.Controls.Add(this.lblBanci);
            this.Controls.Add(this.labTime);
            this.Controls.Add(this.labelControl3);
            this.Controls.Add(this.labFIRST_PURIFIER_MODEL);
            this.Controls.Add(this.labelControl6);
            this.Controls.Add(this.labFREQUENCY_HOURS);
            this.Controls.Add(this.labelControl2);
            this.Controls.Add(this.labPURIFICATION_MODE);
            this.Controls.Add(this.labelControl1);
            this.Controls.Add(this.labErythropoietinMsg);
            this.Controls.Add(this.lbDrugTips);
            this.Controls.Add(this.labPatientName);
            this.Controls.Add(this.lblSex);
            this.Controls.Add(this.pic);
            this.Controls.Add(this.pic_FocuseLevel);
            this.Controls.Add(this.labBedName);
            this.Margin = new System.Windows.Forms.Padding(0, 6, 0, 0);
            this.Name = "CtlTreatmentPerson";
            this.Size = new System.Drawing.Size(230, 180);
            this.Click += new System.EventHandler(this.pnlTreatmentContainer_Click);
            this.DoubleClick += new System.EventHandler(this.pnlTreatmentContainer_DoubleClick);
            ((System.ComponentModel.ISupportInitialize)(this.pic_FocuseLevel.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pic.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.LabelControl labFIRST_PURIFIER_MODEL;
        private DevExpress.XtraEditors.PictureEdit pic;
        private DevExpress.XtraEditors.LabelControl labFREQUENCY_HOURS;
        private DevExpress.XtraEditors.LabelControl labPURIFICATION_MODE;
        private DevExpress.XtraEditors.LabelControl labBedName;
        private DevExpress.XtraEditors.LabelControl labTime;
        private DevExpress.XtraEditors.LabelControl labPatientName;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.LabelControl lblSex;
        private DevExpress.XtraEditors.LabelControl labelControl6;
        private DevExpress.XtraEditors.PictureEdit pic_FocuseLevel;
        private DevExpress.XtraEditors.LabelControl lbDrugTips;
        private DevExpress.XtraEditors.LabelControl labErythropoietinMsg;
        private DevExpress.XtraEditors.LabelControl lblBanci;
    }
}
