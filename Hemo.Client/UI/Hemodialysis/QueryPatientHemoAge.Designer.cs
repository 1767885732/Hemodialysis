namespace Hemo.Client.UI.Hemodialysis
{
    partial class QueryPatientHemoAge
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
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.labelControl5 = new DevExpress.XtraEditors.LabelControl();
            this.chkConstant = new DevExpress.XtraEditors.CheckEdit();
            this.beginTime = new DevExpress.XtraEditors.DateEdit();
            this.btnOutput = new Hemo.Client.Controls.DXSimpleButton();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.txtHemoAge = new DevExpress.XtraEditors.SpinEdit();
            this.endTime = new DevExpress.XtraEditors.DateEdit();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.txtName = new DevExpress.XtraEditors.TextEdit();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.btn_Query = new Hemo.Client.Controls.DXSimpleButton();
            this.btn_Print = new Hemo.Client.Controls.DXSimpleButton();
            this.txtAge = new DevExpress.XtraEditors.SpinEdit();
            this.labelControl117 = new DevExpress.XtraEditors.LabelControl();
            this.panel1 = new System.Windows.Forms.Panel();
            this.gcInBasket = new DevExpress.XtraGrid.GridControl();
            this.gvInBasket = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn5 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn6 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn3 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn4 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn7 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn8 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.toolTipController1 = new DevExpress.Utils.ToolTipController(this.components);
            this.panelControl2 = new DevExpress.XtraEditors.PanelControl();
            this.btnClose = new Hemo.Client.Controls.DXSimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chkConstant.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.beginTime.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.beginTime.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtHemoAge.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.endTime.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.endTime.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtAge.Properties)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gcInBasket)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvInBasket)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).BeginInit();
            this.panelControl2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.labelControl5);
            this.panelControl1.Controls.Add(this.chkConstant);
            this.panelControl1.Controls.Add(this.beginTime);
            this.panelControl1.Controls.Add(this.btnOutput);
            this.panelControl1.Controls.Add(this.labelControl2);
            this.panelControl1.Controls.Add(this.txtHemoAge);
            this.panelControl1.Controls.Add(this.endTime);
            this.panelControl1.Controls.Add(this.labelControl1);
            this.panelControl1.Controls.Add(this.txtName);
            this.panelControl1.Controls.Add(this.labelControl4);
            this.panelControl1.Controls.Add(this.btn_Query);
            this.panelControl1.Controls.Add(this.btn_Print);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelControl1.Location = new System.Drawing.Point(0, 0);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(869, 48);
            this.panelControl1.TabIndex = 1;
            // 
            // labelControl5
            // 
            this.labelControl5.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl5.Location = new System.Drawing.Point(134, 16);
            this.labelControl5.Name = "labelControl5";
            this.labelControl5.Size = new System.Drawing.Size(48, 14);
            this.labelControl5.TabIndex = 377;
            this.labelControl5.Text = "透析日期";
            // 
            // chkConstant
            // 
            this.chkConstant.EditValue = true;
            this.chkConstant.Location = new System.Drawing.Point(537, 16);
            this.chkConstant.Name = "chkConstant";
            this.chkConstant.Properties.Caption = "维持性透析";
            this.chkConstant.Size = new System.Drawing.Size(80, 19);
            this.chkConstant.TabIndex = 818;
            // 
            // beginTime
            // 
            this.beginTime.EditValue = null;
            this.beginTime.Location = new System.Drawing.Point(188, 12);
            this.beginTime.Name = "beginTime";
            this.beginTime.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 11F);
            this.beginTime.Properties.Appearance.Options.UseFont = true;
            this.beginTime.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.beginTime.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.beginTime.Size = new System.Drawing.Size(85, 24);
            this.beginTime.TabIndex = 376;
            // 
            // btnOutput
            // 
            this.btnOutput.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOutput.ImageIndex = 9;
            this.btnOutput.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleLeft;
            this.btnOutput.Location = new System.Drawing.Point(707, 13);
            this.btnOutput.Name = "btnOutput";
            this.btnOutput.Size = new System.Drawing.Size(73, 23);
            this.btnOutput.TabIndex = 449;
            this.btnOutput.Text = "导出(&E)";
            this.btnOutput.Click += new System.EventHandler(this.btnOutput_Click);
            // 
            // labelControl2
            // 
            this.labelControl2.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl2.Location = new System.Drawing.Point(278, 14);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(9, 14);
            this.labelControl2.TabIndex = 378;
            this.labelControl2.Text = "~";
            // 
            // txtHemoAge
            // 
            this.txtHemoAge.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.txtHemoAge.EnterMoveNextControl = true;
            this.txtHemoAge.Location = new System.Drawing.Point(466, 14);
            this.txtHemoAge.Name = "txtHemoAge";
            this.txtHemoAge.Properties.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.txtHemoAge.Properties.Appearance.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtHemoAge.Properties.Appearance.ForeColor = System.Drawing.Color.Blue;
            this.txtHemoAge.Properties.Appearance.Options.UseBackColor = true;
            this.txtHemoAge.Properties.Appearance.Options.UseFont = true;
            this.txtHemoAge.Properties.Appearance.Options.UseForeColor = true;
            this.txtHemoAge.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.txtHemoAge.Size = new System.Drawing.Size(54, 24);
            this.txtHemoAge.TabIndex = 448;
            // 
            // endTime
            // 
            this.endTime.EditValue = null;
            this.endTime.Location = new System.Drawing.Point(293, 11);
            this.endTime.Name = "endTime";
            this.endTime.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 11F);
            this.endTime.Properties.Appearance.Options.UseFont = true;
            this.endTime.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.endTime.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.endTime.Size = new System.Drawing.Size(88, 24);
            this.endTime.TabIndex = 375;
            // 
            // labelControl1
            // 
            this.labelControl1.Appearance.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.labelControl1.Location = new System.Drawing.Point(388, 17);
            this.labelControl1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(72, 17);
            this.labelControl1.TabIndex = 447;
            this.labelControl1.Text = "透析年龄大于";
            // 
            // txtName
            // 
            this.txtName.Location = new System.Drawing.Point(51, 15);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(65, 20);
            this.txtName.TabIndex = 23;
            // 
            // labelControl4
            // 
            this.labelControl4.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl4.Location = new System.Drawing.Point(21, 16);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(24, 14);
            this.labelControl4.TabIndex = 22;
            this.labelControl4.Text = "姓名";
            // 
            // btn_Query
            // 
            this.btn_Query.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_Query.ImageIndex = 8;
            this.btn_Query.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleLeft;
            this.btn_Query.Location = new System.Drawing.Point(623, 14);
            this.btn_Query.Name = "btn_Query";
            this.btn_Query.Size = new System.Drawing.Size(73, 23);
            this.btn_Query.TabIndex = 19;
            this.btn_Query.Text = "查询(&Q)";
            this.btn_Query.Click += new System.EventHandler(this.btn_Query_Click);
            // 
            // btn_Print
            // 
            this.btn_Print.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_Print.ImageIndex = 6;
            this.btn_Print.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleLeft;
            this.btn_Print.Location = new System.Drawing.Point(790, 13);
            this.btn_Print.Name = "btn_Print";
            this.btn_Print.Size = new System.Drawing.Size(73, 23);
            this.btn_Print.TabIndex = 18;
            this.btn_Print.Text = "打印(&P)";
            this.btn_Print.Visible = false;
            this.btn_Print.Click += new System.EventHandler(this.btn_Print_Click);
            // 
            // txtAge
            // 
            this.txtAge.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.txtAge.EnterMoveNextControl = true;
            this.txtAge.Location = new System.Drawing.Point(497, 6);
            this.txtAge.Name = "txtAge";
            this.txtAge.Properties.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.txtAge.Properties.Appearance.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtAge.Properties.Appearance.ForeColor = System.Drawing.Color.Blue;
            this.txtAge.Properties.Appearance.Options.UseBackColor = true;
            this.txtAge.Properties.Appearance.Options.UseFont = true;
            this.txtAge.Properties.Appearance.Options.UseForeColor = true;
            this.txtAge.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.txtAge.Size = new System.Drawing.Size(102, 24);
            this.txtAge.TabIndex = 446;
            this.txtAge.Visible = false;
            // 
            // labelControl117
            // 
            this.labelControl117.Appearance.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.labelControl117.Location = new System.Drawing.Point(419, 9);
            this.labelControl117.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.labelControl117.Name = "labelControl117";
            this.labelControl117.Size = new System.Drawing.Size(72, 17);
            this.labelControl117.TabIndex = 445;
            this.labelControl117.Text = "患者年龄大于";
            this.labelControl117.Visible = false;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.gcInBasket);
            this.panel1.Controls.Add(this.panelControl2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 48);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(869, 359);
            this.panel1.TabIndex = 373;
            // 
            // gcInBasket
            // 
            this.gcInBasket.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gcInBasket.Location = new System.Drawing.Point(0, 0);
            this.gcInBasket.MainView = this.gvInBasket;
            this.gcInBasket.Name = "gcInBasket";
            this.gcInBasket.Size = new System.Drawing.Size(869, 324);
            this.gcInBasket.TabIndex = 311;
            this.gcInBasket.ToolTipController = this.toolTipController1;
            this.gcInBasket.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvInBasket});
            // 
            // gvInBasket
            // 
            this.gvInBasket.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn2,
            this.gridColumn1,
            this.gridColumn5,
            this.gridColumn6,
            this.gridColumn3,
            this.gridColumn4,
            this.gridColumn7,
            this.gridColumn8});
            this.gvInBasket.GridControl = this.gcInBasket;
            this.gvInBasket.Name = "gvInBasket";
            this.gvInBasket.OptionsBehavior.Editable = false;
            this.gvInBasket.OptionsBehavior.EditorShowMode = DevExpress.Utils.EditorShowMode.Click;
            this.gvInBasket.OptionsView.ShowGroupPanel = false;
            this.gvInBasket.RowClick += new DevExpress.XtraGrid.Views.Grid.RowClickEventHandler(this.gvInBasket_RowClick);
            // 
            // gridColumn2
            // 
            this.gridColumn2.Caption = "HEMODIALYSIS_ID";
            this.gridColumn2.FieldName = "HEMODIALYSIS_ID";
            this.gridColumn2.Name = "gridColumn2";
            // 
            // gridColumn1
            // 
            this.gridColumn1.Caption = "姓名";
            this.gridColumn1.FieldName = "NAME";
            this.gridColumn1.Name = "gridColumn1";
            this.gridColumn1.Visible = true;
            this.gridColumn1.VisibleIndex = 0;
            // 
            // gridColumn5
            // 
            this.gridColumn5.Caption = "性别";
            this.gridColumn5.DisplayFormat.FormatString = "yyyy-MM-dd";
            this.gridColumn5.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.gridColumn5.FieldName = "SEX";
            this.gridColumn5.Name = "gridColumn5";
            this.gridColumn5.Visible = true;
            this.gridColumn5.VisibleIndex = 1;
            // 
            // gridColumn6
            // 
            this.gridColumn6.Caption = "年龄";
            this.gridColumn6.FieldName = "PATIENT_AGE";
            this.gridColumn6.Name = "gridColumn6";
            this.gridColumn6.Visible = true;
            this.gridColumn6.VisibleIndex = 3;
            // 
            // gridColumn3
            // 
            this.gridColumn3.Caption = "电话";
            this.gridColumn3.FieldName = "TELEPHONE";
            this.gridColumn3.Name = "gridColumn3";
            this.gridColumn3.Visible = true;
            this.gridColumn3.VisibleIndex = 2;
            // 
            // gridColumn4
            // 
            this.gridColumn4.Caption = "诊断";
            this.gridColumn4.FieldName = "DIAGNOSE";
            this.gridColumn4.Name = "gridColumn4";
            this.gridColumn4.Visible = true;
            this.gridColumn4.VisibleIndex = 4;
            this.gridColumn4.Width = 100;
            // 
            // gridColumn7
            // 
            this.gridColumn7.Caption = "首次透析日期";
            this.gridColumn7.FieldName = "SPECIFIC_TIME";
            this.gridColumn7.Name = "gridColumn7";
            this.gridColumn7.Visible = true;
            this.gridColumn7.VisibleIndex = 5;
            this.gridColumn7.Width = 149;
            // 
            // gridColumn8
            // 
            this.gridColumn8.Caption = "透析龄";
            this.gridColumn8.FieldName = "HEMOAGE";
            this.gridColumn8.Name = "gridColumn8";
            this.gridColumn8.Visible = true;
            this.gridColumn8.VisibleIndex = 6;
            this.gridColumn8.Width = 155;
            // 
            // panelControl2
            // 
            this.panelControl2.Controls.Add(this.txtAge);
            this.panelControl2.Controls.Add(this.labelControl117);
            this.panelControl2.Controls.Add(this.btnClose);
            this.panelControl2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelControl2.Location = new System.Drawing.Point(0, 324);
            this.panelControl2.Name = "panelControl2";
            this.panelControl2.Size = new System.Drawing.Size(869, 35);
            this.panelControl2.TabIndex = 310;
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.ImageIndex = 3;
            this.btnClose.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleLeft;
            this.btnClose.Location = new System.Drawing.Point(768, 5);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(73, 22);
            this.btnClose.TabIndex = 374;
            this.btnClose.Text = "关闭(&C)";
            this.btnClose.Visible = false;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // QueryPatientHemoAge
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panelControl1);
            this.Name = "QueryPatientHemoAge";
            this.Size = new System.Drawing.Size(869, 407);
            this.Load += new System.EventHandler(this.QueryEstimateInBasket_Load);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.panelControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chkConstant.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.beginTime.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.beginTime.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtHemoAge.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.endTime.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.endTime.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtAge.Properties)).EndInit();
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gcInBasket)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvInBasket)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).EndInit();
            this.panelControl2.ResumeLayout(false);
            this.panelControl2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl panelControl1;
        private Hemo.Client.Controls.DXSimpleButton btn_Query;
        private Hemo.Client.Controls.DXSimpleButton btn_Print;
        private System.Windows.Forms.Panel panel1;
        private DevExpress.XtraEditors.PanelControl panelControl2;
        private Hemo.Client.Controls.DXSimpleButton btnClose;
        private DevExpress.XtraGrid.GridControl gcInBasket;
        public DevExpress.XtraGrid.Views.Grid.GridView gvInBasket;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn2;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn5;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn3;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn6;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn4;
        private DevExpress.XtraEditors.TextEdit txtName;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private DevExpress.Utils.ToolTipController toolTipController1;
        private DevExpress.XtraEditors.LabelControl labelControl117;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn7;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn8;
        private DevExpress.XtraEditors.SpinEdit txtAge;
        private DevExpress.XtraEditors.SpinEdit txtHemoAge;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private Controls.DXSimpleButton btnOutput;
        private DevExpress.XtraEditors.CheckEdit chkConstant;
        private DevExpress.XtraEditors.LabelControl labelControl5;
        private DevExpress.XtraEditors.DateEdit beginTime;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.DateEdit endTime;

    }
}