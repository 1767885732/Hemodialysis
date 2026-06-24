namespace Hemo.Client.UI.Machine
{
    partial class AirPurgeInput
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AirPurgeInput));
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.dateEdit_PurgeDate = new DevExpress.XtraEditors.DateEdit();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.txt_workstate = new DevExpress.XtraEditors.TextEdit();
            this.labelControl5 = new DevExpress.XtraEditors.LabelControl();
            this.btn_Save = new Hemo.Client.Controls.DXSimpleButton();
            this.btn_Cancle = new Hemo.Client.Controls.DXSimpleButton();
            this.lopOperator = new DevExpress.XtraEditors.LookUpEdit();
            this.timeEdit_TrendTime = new DevExpress.XtraEditors.TimeEdit();
            this.timeEdit_StaticTime = new DevExpress.XtraEditors.TimeEdit();
            this.timeEdit_TrendTimeEND = new DevExpress.XtraEditors.TimeEdit();
            this.timeEdit_StaticTimeEND = new DevExpress.XtraEditors.TimeEdit();
            this.labelControl6 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl7 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl8 = new DevExpress.XtraEditors.LabelControl();
            this.ediSickArea = new DevExpress.XtraEditors.LookUpEdit();
            this.lopTrendPurger = new DevExpress.XtraEditors.LookUpEdit();
            this.labelControl9 = new DevExpress.XtraEditors.LabelControl();
            this.txt_trendworkstate = new DevExpress.XtraEditors.TextEdit();
            this.labelControl10 = new DevExpress.XtraEditors.LabelControl();
            ((System.ComponentModel.ISupportInitialize)(this.dateEdit_PurgeDate.Properties.VistaTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEdit_PurgeDate.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txt_workstate.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lopOperator.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.timeEdit_TrendTime.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.timeEdit_StaticTime.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.timeEdit_TrendTimeEND.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.timeEdit_StaticTimeEND.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ediSickArea.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lopTrendPurger.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txt_trendworkstate.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // labelControl1
            // 
            resources.ApplyResources(this.labelControl1, "labelControl1");
            this.labelControl1.Name = "labelControl1";
            // 
            // dateEdit_PurgeDate
            // 
            resources.ApplyResources(this.dateEdit_PurgeDate, "dateEdit_PurgeDate");
            this.dateEdit_PurgeDate.Name = "dateEdit_PurgeDate";
            this.dateEdit_PurgeDate.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(((DevExpress.XtraEditors.Controls.ButtonPredefines)(resources.GetObject("dateEdit_PurgeDate.Properties.Buttons"))))});
            this.dateEdit_PurgeDate.Properties.VistaTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            // 
            // labelControl2
            // 
            resources.ApplyResources(this.labelControl2, "labelControl2");
            this.labelControl2.Name = "labelControl2";
            // 
            // labelControl3
            // 
            resources.ApplyResources(this.labelControl3, "labelControl3");
            this.labelControl3.Name = "labelControl3";
            // 
            // labelControl4
            // 
            resources.ApplyResources(this.labelControl4, "labelControl4");
            this.labelControl4.Name = "labelControl4";
            // 
            // txt_workstate
            // 
            resources.ApplyResources(this.txt_workstate, "txt_workstate");
            this.txt_workstate.Name = "txt_workstate";
            // 
            // labelControl5
            // 
            resources.ApplyResources(this.labelControl5, "labelControl5");
            this.labelControl5.Name = "labelControl5";
            // 
            // btn_Save
            // 
            this.btn_Save.ImageIndex = 7;
            this.btn_Save.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleLeft;
            resources.ApplyResources(this.btn_Save, "btn_Save");
            this.btn_Save.Name = "btn_Save";
            this.btn_Save.Click += new System.EventHandler(this.btn_Save_Click);
            // 
            // btn_Cancle
            // 
            this.btn_Cancle.ImageIndex = 3;
            this.btn_Cancle.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleLeft;
            resources.ApplyResources(this.btn_Cancle, "btn_Cancle");
            this.btn_Cancle.Name = "btn_Cancle";
            this.btn_Cancle.Click += new System.EventHandler(this.btn_Cancle_Click);
            // 
            // lopOperator
            // 
            resources.ApplyResources(this.lopOperator, "lopOperator");
            this.lopOperator.EnterMoveNextControl = true;
            this.lopOperator.Name = "lopOperator";
            this.lopOperator.Properties.Appearance.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.lopOperator.Properties.Appearance.ForeColor = System.Drawing.Color.Blue;
            this.lopOperator.Properties.Appearance.Options.UseFont = true;
            this.lopOperator.Properties.Appearance.Options.UseForeColor = true;
            this.lopOperator.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(((DevExpress.XtraEditors.Controls.ButtonPredefines)(resources.GetObject("lopOperator.Properties.Buttons"))))});
            this.lopOperator.Properties.LookAndFeel.SkinName = "ExtendBlue";
            this.lopOperator.Properties.NullText = resources.GetString("lopOperator.Properties.NullText");
            // 
            // timeEdit_TrendTime
            // 
            resources.ApplyResources(this.timeEdit_TrendTime, "timeEdit_TrendTime");
            this.timeEdit_TrendTime.Name = "timeEdit_TrendTime";
            this.timeEdit_TrendTime.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.timeEdit_TrendTime.Properties.DisplayFormat.FormatString = "HH:mm:ss";
            this.timeEdit_TrendTime.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            // 
            // timeEdit_StaticTime
            // 
            resources.ApplyResources(this.timeEdit_StaticTime, "timeEdit_StaticTime");
            this.timeEdit_StaticTime.Name = "timeEdit_StaticTime";
            this.timeEdit_StaticTime.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.timeEdit_StaticTime.Properties.DisplayFormat.FormatString = "HH:mm:ss";
            this.timeEdit_StaticTime.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            // 
            // timeEdit_TrendTimeEND
            // 
            resources.ApplyResources(this.timeEdit_TrendTimeEND, "timeEdit_TrendTimeEND");
            this.timeEdit_TrendTimeEND.Name = "timeEdit_TrendTimeEND";
            this.timeEdit_TrendTimeEND.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.timeEdit_TrendTimeEND.Properties.DisplayFormat.FormatString = "HH:mm:ss";
            this.timeEdit_TrendTimeEND.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            // 
            // timeEdit_StaticTimeEND
            // 
            resources.ApplyResources(this.timeEdit_StaticTimeEND, "timeEdit_StaticTimeEND");
            this.timeEdit_StaticTimeEND.Name = "timeEdit_StaticTimeEND";
            this.timeEdit_StaticTimeEND.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.timeEdit_StaticTimeEND.Properties.DisplayFormat.FormatString = "HH:mm:ss";
            this.timeEdit_StaticTimeEND.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            // 
            // labelControl6
            // 
            resources.ApplyResources(this.labelControl6, "labelControl6");
            this.labelControl6.Name = "labelControl6";
            // 
            // labelControl7
            // 
            resources.ApplyResources(this.labelControl7, "labelControl7");
            this.labelControl7.Name = "labelControl7";
            // 
            // labelControl8
            // 
            resources.ApplyResources(this.labelControl8, "labelControl8");
            this.labelControl8.Name = "labelControl8";
            // 
            // ediSickArea
            // 
            resources.ApplyResources(this.ediSickArea, "ediSickArea");
            this.ediSickArea.EnterMoveNextControl = true;
            this.ediSickArea.Name = "ediSickArea";
            this.ediSickArea.Properties.Appearance.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.ediSickArea.Properties.Appearance.ForeColor = System.Drawing.Color.Blue;
            this.ediSickArea.Properties.Appearance.Options.UseFont = true;
            this.ediSickArea.Properties.Appearance.Options.UseForeColor = true;
            this.ediSickArea.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(((DevExpress.XtraEditors.Controls.ButtonPredefines)(resources.GetObject("ediSickArea.Properties.Buttons"))))});
            this.ediSickArea.Properties.LookAndFeel.SkinName = "ExtendBlue";
            this.ediSickArea.Properties.NullText = resources.GetString("ediSickArea.Properties.NullText");
            // 
            // lopTrendPurger
            // 
            resources.ApplyResources(this.lopTrendPurger, "lopTrendPurger");
            this.lopTrendPurger.EnterMoveNextControl = true;
            this.lopTrendPurger.Name = "lopTrendPurger";
            this.lopTrendPurger.Properties.Appearance.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.lopTrendPurger.Properties.Appearance.ForeColor = System.Drawing.Color.Blue;
            this.lopTrendPurger.Properties.Appearance.Options.UseFont = true;
            this.lopTrendPurger.Properties.Appearance.Options.UseForeColor = true;
            this.lopTrendPurger.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(((DevExpress.XtraEditors.Controls.ButtonPredefines)(resources.GetObject("lopTrendPurger.Properties.Buttons"))))});
            this.lopTrendPurger.Properties.LookAndFeel.SkinName = "ExtendBlue";
            this.lopTrendPurger.Properties.NullText = resources.GetString("lopTrendPurger.Properties.NullText");
            // 
            // labelControl9
            // 
            resources.ApplyResources(this.labelControl9, "labelControl9");
            this.labelControl9.Name = "labelControl9";
            // 
            // txt_trendworkstate
            // 
            resources.ApplyResources(this.txt_trendworkstate, "txt_trendworkstate");
            this.txt_trendworkstate.Name = "txt_trendworkstate";
            // 
            // labelControl10
            // 
            resources.ApplyResources(this.labelControl10, "labelControl10");
            this.labelControl10.Name = "labelControl10";
            // 
            // AirPurgeInput
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lopTrendPurger);
            this.Controls.Add(this.labelControl9);
            this.Controls.Add(this.txt_trendworkstate);
            this.Controls.Add(this.labelControl10);
            this.Controls.Add(this.ediSickArea);
            this.Controls.Add(this.timeEdit_StaticTimeEND);
            this.Controls.Add(this.timeEdit_StaticTime);
            this.Controls.Add(this.timeEdit_TrendTimeEND);
            this.Controls.Add(this.timeEdit_TrendTime);
            this.Controls.Add(this.lopOperator);
            this.Controls.Add(this.btn_Cancle);
            this.Controls.Add(this.btn_Save);
            this.Controls.Add(this.labelControl5);
            this.Controls.Add(this.txt_workstate);
            this.Controls.Add(this.labelControl4);
            this.Controls.Add(this.labelControl3);
            this.Controls.Add(this.labelControl2);
            this.Controls.Add(this.dateEdit_PurgeDate);
            this.Controls.Add(this.labelControl7);
            this.Controls.Add(this.labelControl8);
            this.Controls.Add(this.labelControl6);
            this.Controls.Add(this.labelControl1);
            this.Name = "AirPurgeInput";
            this.Load += new System.EventHandler(this.AirPurgeInput_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dateEdit_PurgeDate.Properties.VistaTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEdit_PurgeDate.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txt_workstate.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lopOperator.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.timeEdit_TrendTime.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.timeEdit_StaticTime.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.timeEdit_TrendTimeEND.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.timeEdit_StaticTimeEND.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ediSickArea.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lopTrendPurger.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txt_trendworkstate.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.DateEdit dateEdit_PurgeDate;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private DevExpress.XtraEditors.TextEdit txt_workstate;
        private DevExpress.XtraEditors.LabelControl labelControl5;
        private Hemo.Client.Controls.DXSimpleButton btn_Save;
        private Hemo.Client.Controls.DXSimpleButton btn_Cancle;
        private DevExpress.XtraEditors.LookUpEdit lopOperator;
        private DevExpress.XtraEditors.TimeEdit timeEdit_TrendTime;
        private DevExpress.XtraEditors.TimeEdit timeEdit_StaticTime;
        private DevExpress.XtraEditors.TimeEdit timeEdit_TrendTimeEND;
        private DevExpress.XtraEditors.TimeEdit timeEdit_StaticTimeEND;
        private DevExpress.XtraEditors.LabelControl labelControl6;
        private DevExpress.XtraEditors.LabelControl labelControl7;
        private DevExpress.XtraEditors.LabelControl labelControl8;
        private DevExpress.XtraEditors.LookUpEdit ediSickArea;
        private DevExpress.XtraEditors.LookUpEdit lopTrendPurger;
        private DevExpress.XtraEditors.LabelControl labelControl9;
        private DevExpress.XtraEditors.TextEdit txt_trendworkstate;
        private DevExpress.XtraEditors.LabelControl labelControl10;

    }
}