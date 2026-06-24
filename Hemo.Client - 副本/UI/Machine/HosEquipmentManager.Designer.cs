namespace Hemo.Client.UI.Machine
{
    partial class HosEquipmentManager
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
            this.gcMachine = new DevExpress.XtraGrid.GridControl();
            this.gvMachine = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn5 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn4 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn3 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn9 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn10 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn11 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn6 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.btn_Delete = new Hemo.Client.Controls.DXSimpleButton();
            this.txt_machine = new DevExpress.XtraEditors.TextEdit();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.btn_Query = new Hemo.Client.Controls.DXSimpleButton();
            this.btn_Print = new Hemo.Client.Controls.DXSimpleButton();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.endTime = new DevExpress.XtraEditors.DateEdit();
            this.beginTime = new DevExpress.XtraEditors.DateEdit();
            this.btn_Entering = new Hemo.Client.Controls.DXSimpleButton();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolDelete = new System.Windows.Forms.ToolStripMenuItem();
            this.busyIndicator1 = new Hemo.Client.Controls.BusyIndicator();
            ((System.ComponentModel.ISupportInitialize)(this.gcMachine)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvMachine)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txt_machine.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.endTime.Properties.VistaTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.endTime.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.beginTime.Properties.VistaTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.beginTime.Properties)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // gcMachine
            // 
            this.gcMachine.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gcMachine.Location = new System.Drawing.Point(0, 0);
            this.gcMachine.MainView = this.gvMachine;
            this.gcMachine.Name = "gcMachine";
            this.gcMachine.Size = new System.Drawing.Size(857, 407);
            this.gcMachine.TabIndex = 310;
            this.gcMachine.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvMachine});
            this.gcMachine.MouseDown += new System.Windows.Forms.MouseEventHandler(this.gcMachine_MouseDown);
            // 
            // gvMachine
            // 
            this.gvMachine.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn1,
            this.gridColumn5,
            this.gridColumn2,
            this.gridColumn4,
            this.gridColumn3,
            this.gridColumn9,
            this.gridColumn10,
            this.gridColumn11,
            this.gridColumn6});
            this.gvMachine.GridControl = this.gcMachine;
            this.gvMachine.Name = "gvMachine";
            this.gvMachine.OptionsBehavior.Editable = false;
            this.gvMachine.OptionsBehavior.EditorShowMode = DevExpress.Utils.EditorShowMode.Click;
            this.gvMachine.OptionsView.ShowGroupPanel = false;
            this.gvMachine.DoubleClick += new System.EventHandler(this.gvMachine_DoubleClick);
            // 
            // gridColumn1
            // 
            this.gridColumn1.Caption = "ID";
            this.gridColumn1.FieldName = "ID";
            this.gridColumn1.Name = "gridColumn1";
            // 
            // gridColumn5
            // 
            this.gridColumn5.Caption = "贵重设备";
            this.gridColumn5.FieldName = "MACHINE";
            this.gridColumn5.Name = "gridColumn5";
            this.gridColumn5.Visible = true;
            this.gridColumn5.VisibleIndex = 0;
            // 
            // gridColumn2
            // 
            this.gridColumn2.Caption = "启用时间";
            this.gridColumn2.FieldName = "DISINFECTDATE";
            this.gridColumn2.Name = "gridColumn2";
            this.gridColumn2.Visible = true;
            this.gridColumn2.VisibleIndex = 1;
            // 
            // gridColumn4
            // 
            this.gridColumn4.Caption = "维修";
            this.gridColumn4.FieldName = "EVENTTEXT";
            this.gridColumn4.Name = "gridColumn4";
            this.gridColumn4.Visible = true;
            this.gridColumn4.VisibleIndex = 2;
            // 
            // gridColumn3
            // 
            this.gridColumn3.Caption = "保养";
            this.gridColumn3.FieldName = "RESULT";
            this.gridColumn3.Name = "gridColumn3";
            this.gridColumn3.Visible = true;
            this.gridColumn3.VisibleIndex = 3;
            // 
            // gridColumn9
            // 
            this.gridColumn9.Caption = "创建时间";
            this.gridColumn9.DisplayFormat.FormatString = "HH:mm:ss";
            this.gridColumn9.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.gridColumn9.FieldName = "CREATEDATE";
            this.gridColumn9.Name = "gridColumn9";
            // 
            // gridColumn10
            // 
            this.gridColumn10.Caption = "创建者";
            this.gridColumn10.FieldName = "WORKSTATE";
            this.gridColumn10.Name = "gridColumn10";
            // 
            // gridColumn11
            // 
            this.gridColumn11.Caption = "删除";
            this.gridColumn11.FieldName = "ISDELETE";
            this.gridColumn11.Name = "gridColumn11";
            // 
            // gridColumn6
            // 
            this.gridColumn6.Caption = "效验时间";
            this.gridColumn6.FieldName = "CHECKDATE";
            this.gridColumn6.Name = "gridColumn6";
            this.gridColumn6.Visible = true;
            this.gridColumn6.VisibleIndex = 4;
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.btn_Delete);
            this.panelControl1.Controls.Add(this.txt_machine);
            this.panelControl1.Controls.Add(this.labelControl2);
            this.panelControl1.Controls.Add(this.btn_Query);
            this.panelControl1.Controls.Add(this.btn_Print);
            this.panelControl1.Controls.Add(this.labelControl3);
            this.panelControl1.Controls.Add(this.labelControl1);
            this.panelControl1.Controls.Add(this.endTime);
            this.panelControl1.Controls.Add(this.beginTime);
            this.panelControl1.Controls.Add(this.btn_Entering);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelControl1.Location = new System.Drawing.Point(0, 407);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(857, 40);
            this.panelControl1.TabIndex = 311;
            // 
            // btn_Delete
            // 
            this.btn_Delete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_Delete.ImageIndex = 1;
            this.btn_Delete.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleLeft;
            this.btn_Delete.Location = new System.Drawing.Point(684, 7);
            this.btn_Delete.Name = "btn_Delete";
            this.btn_Delete.Size = new System.Drawing.Size(75, 25);
            this.btn_Delete.TabIndex = 32;
            this.btn_Delete.Text = "删除(&D)";
            this.btn_Delete.Click += new System.EventHandler(this.btn_Delete_Click);
            // 
            // txt_machine
            // 
            this.txt_machine.Location = new System.Drawing.Point(385, 13);
            this.txt_machine.Name = "txt_machine";
            this.txt_machine.Size = new System.Drawing.Size(101, 21);
            this.txt_machine.TabIndex = 28;
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(343, 16);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(40, 14);
            this.labelControl2.TabIndex = 27;
            this.labelControl2.Text = "机器名:";
            // 
            // btn_Query
            // 
            this.btn_Query.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_Query.ImageIndex = 8;
            this.btn_Query.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleLeft;
            this.btn_Query.Location = new System.Drawing.Point(522, 8);
            this.btn_Query.Name = "btn_Query";
            this.btn_Query.Size = new System.Drawing.Size(75, 25);
            this.btn_Query.TabIndex = 26;
            this.btn_Query.Text = "查询(&Q)";
            this.btn_Query.Click += new System.EventHandler(this.btn_Query_Click);
            // 
            // btn_Print
            // 
            this.btn_Print.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_Print.ImageIndex = 6;
            this.btn_Print.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleLeft;
            this.btn_Print.Location = new System.Drawing.Point(765, 7);
            this.btn_Print.Name = "btn_Print";
            this.btn_Print.Size = new System.Drawing.Size(75, 25);
            this.btn_Print.TabIndex = 25;
            this.btn_Print.Text = "打印(&P)";
            this.btn_Print.Click += new System.EventHandler(this.btn_Print_Click);
            // 
            // labelControl3
            // 
            this.labelControl3.Location = new System.Drawing.Point(175, 16);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(60, 14);
            this.labelControl3.TabIndex = 24;
            this.labelControl3.Text = "结束日期：";
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(3, 16);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(60, 14);
            this.labelControl1.TabIndex = 23;
            this.labelControl1.Text = "开始日期：";
            // 
            // endTime
            // 
            this.endTime.EditValue = null;
            this.endTime.Location = new System.Drawing.Point(233, 12);
            this.endTime.Name = "endTime";
            this.endTime.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.endTime.Properties.VistaTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.endTime.Size = new System.Drawing.Size(104, 21);
            this.endTime.TabIndex = 21;
            // 
            // beginTime
            // 
            this.beginTime.EditValue = null;
            this.beginTime.Location = new System.Drawing.Point(63, 13);
            this.beginTime.Name = "beginTime";
            this.beginTime.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.beginTime.Properties.VistaTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.beginTime.Size = new System.Drawing.Size(108, 21);
            this.beginTime.TabIndex = 22;
            // 
            // btn_Entering
            // 
            this.btn_Entering.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_Entering.ImageIndex = 0;
            this.btn_Entering.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleLeft;
            this.btn_Entering.Location = new System.Drawing.Point(603, 7);
            this.btn_Entering.Name = "btn_Entering";
            this.btn_Entering.Size = new System.Drawing.Size(75, 25);
            this.btn_Entering.TabIndex = 20;
            this.btn_Entering.Text = "新增(&A)";
            this.btn_Entering.Click += new System.EventHandler(this.btn_Entering_Click);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolDelete});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(101, 26);
            // 
            // toolDelete
            // 
            this.toolDelete.Name = "toolDelete";
            this.toolDelete.Size = new System.Drawing.Size(100, 22);
            this.toolDelete.Text = "删除";
            this.toolDelete.Click += new System.EventHandler(this.toolDelete_Click);
            // 
            // busyIndicator1
            // 
            this.busyIndicator1.Location = new System.Drawing.Point(350, 168);
            this.busyIndicator1.Name = "busyIndicator1";
            this.busyIndicator1.Size = new System.Drawing.Size(136, 63);
            this.busyIndicator1.TabIndex = 313;
            this.busyIndicator1.Visible = false;
            // 
            // HosEquipmentManager
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.busyIndicator1);
            this.Controls.Add(this.gcMachine);
            this.Controls.Add(this.panelControl1);
            this.Name = "HosEquipmentManager";
            this.Size = new System.Drawing.Size(857, 447);
            this.Load += new System.EventHandler(this.HosEquipmentManager_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gcMachine)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvMachine)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.panelControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txt_machine.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.endTime.Properties.VistaTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.endTime.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.beginTime.Properties.VistaTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.beginTime.Properties)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraGrid.GridControl gcMachine;
        public DevExpress.XtraGrid.Views.Grid.GridView gvMachine;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn2;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn4;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn3;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn9;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn10;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn11;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private Hemo.Client.Controls.DXSimpleButton btn_Query;
        private Hemo.Client.Controls.DXSimpleButton btn_Print;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.DateEdit endTime;
        private DevExpress.XtraEditors.DateEdit beginTime;
        private Hemo.Client.Controls.DXSimpleButton btn_Entering;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem toolDelete;
        private Controls.BusyIndicator busyIndicator1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn5;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.TextEdit txt_machine;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn6;
        private Controls.DXSimpleButton btn_Delete;
    }
}