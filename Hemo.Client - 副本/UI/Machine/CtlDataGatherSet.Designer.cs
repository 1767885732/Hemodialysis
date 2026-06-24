namespace Hemo.Client.UI.Machine
{
    partial class CtlDataGatherSet
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
            this.panelControl3 = new DevExpress.XtraEditors.PanelControl();
            this.seCount = new DevExpress.XtraEditors.SpinEdit();
            this.labelControl7 = new DevExpress.XtraEditors.LabelControl();
            this.btnCancel = new Hemo.Client.Controls.DXSimpleButton();
            this.btnSet = new Hemo.Client.Controls.DXSimpleButton();
            this.btnRefresh = new Hemo.Client.Controls.DXSimpleButton();
            this.txtMachine = new DevExpress.XtraEditors.TextEdit();
            this.labelControl6 = new DevExpress.XtraEditors.LabelControl();
            this.seActual = new DevExpress.XtraEditors.SpinEdit();
            this.labelControl5 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.seDefault = new DevExpress.XtraEditors.SpinEdit();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.panelControl2 = new DevExpress.XtraEditors.PanelControl();
            this.gcMachine = new DevExpress.XtraGrid.GridControl();
            this.gvMachine = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colArea = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colBed = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colMachine = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colStatus = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemCheckEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl3)).BeginInit();
            this.panelControl3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.seCount.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtMachine.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.seActual.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.seDefault.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).BeginInit();
            this.panelControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gcMachine)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvMachine)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit1)).BeginInit();
            this.SuspendLayout();
            // 
            // panelControl3
            // 
            this.panelControl3.Controls.Add(this.seCount);
            this.panelControl3.Controls.Add(this.labelControl7);
            this.panelControl3.Controls.Add(this.btnCancel);
            this.panelControl3.Controls.Add(this.btnSet);
            this.panelControl3.Controls.Add(this.btnRefresh);
            this.panelControl3.Controls.Add(this.txtMachine);
            this.panelControl3.Controls.Add(this.labelControl6);
            this.panelControl3.Controls.Add(this.seActual);
            this.panelControl3.Controls.Add(this.labelControl5);
            this.panelControl3.Controls.Add(this.labelControl4);
            this.panelControl3.Controls.Add(this.seDefault);
            this.panelControl3.Controls.Add(this.labelControl3);
            this.panelControl3.Controls.Add(this.labelControl2);
            this.panelControl3.Controls.Add(this.labelControl1);
            this.panelControl3.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelControl3.Location = new System.Drawing.Point(0, 356);
            this.panelControl3.Name = "panelControl3";
            this.panelControl3.Size = new System.Drawing.Size(746, 124);
            this.panelControl3.TabIndex = 1;
            // 
            // seCount
            // 
            this.seCount.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.seCount.Location = new System.Drawing.Point(479, 49);
            this.seCount.Name = "seCount";
            this.seCount.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.seCount.Size = new System.Drawing.Size(100, 20);
            this.seCount.TabIndex = 13;
            // 
            // labelControl7
            // 
            this.labelControl7.Location = new System.Drawing.Point(425, 52);
            this.labelControl7.Name = "labelControl7";
            this.labelControl7.Size = new System.Drawing.Size(48, 14);
            this.labelControl7.TabIndex = 12;
            this.labelControl7.Text = "采集次数";
            // 
            // btnCancel
            // 
            this.btnCancel.ImageIndex = 13;
            this.btnCancel.Location = new System.Drawing.Point(632, 90);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(99, 23);
            this.btnCancel.TabIndex = 11;
            this.btnCancel.Text = "取消血透机";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnSet
            // 
            this.btnSet.ImageIndex = 12;
            this.btnSet.Location = new System.Drawing.Point(528, 90);
            this.btnSet.Name = "btnSet";
            this.btnSet.Size = new System.Drawing.Size(91, 23);
            this.btnSet.TabIndex = 10;
            this.btnSet.Text = "设置血透机";
            this.btnSet.Click += new System.EventHandler(this.btnSet_Click);
            // 
            // btnRefresh
            // 
            this.btnRefresh.ImageIndex = 10;
            this.btnRefresh.Location = new System.Drawing.Point(440, 90);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(75, 23);
            this.btnRefresh.TabIndex = 9;
            this.btnRefresh.Text = "刷新";
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // txtMachine
            // 
            this.txtMachine.Enabled = false;
            this.txtMachine.Location = new System.Drawing.Point(77, 12);
            this.txtMachine.Name = "txtMachine";
            this.txtMachine.Size = new System.Drawing.Size(315, 20);
            this.txtMachine.TabIndex = 8;
            // 
            // labelControl6
            // 
            this.labelControl6.Location = new System.Drawing.Point(398, 52);
            this.labelControl6.Name = "labelControl6";
            this.labelControl6.Size = new System.Drawing.Size(12, 14);
            this.labelControl6.TabIndex = 7;
            this.labelControl6.Text = "秒";
            // 
            // seActual
            // 
            this.seActual.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.seActual.Location = new System.Drawing.Point(292, 49);
            this.seActual.Name = "seActual";
            this.seActual.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.seActual.Size = new System.Drawing.Size(100, 20);
            this.seActual.TabIndex = 6;
            // 
            // labelControl5
            // 
            this.labelControl5.Location = new System.Drawing.Point(262, 52);
            this.labelControl5.Name = "labelControl5";
            this.labelControl5.Size = new System.Drawing.Size(24, 14);
            this.labelControl5.TabIndex = 5;
            this.labelControl5.Text = "实际";
            // 
            // labelControl4
            // 
            this.labelControl4.Location = new System.Drawing.Point(235, 52);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(12, 14);
            this.labelControl4.TabIndex = 4;
            this.labelControl4.Text = "秒";
            // 
            // seDefault
            // 
            this.seDefault.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.seDefault.Location = new System.Drawing.Point(129, 49);
            this.seDefault.Name = "seDefault";
            this.seDefault.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.seDefault.Size = new System.Drawing.Size(100, 20);
            this.seDefault.TabIndex = 3;
            // 
            // labelControl3
            // 
            this.labelControl3.Location = new System.Drawing.Point(99, 52);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(24, 14);
            this.labelControl3.TabIndex = 2;
            this.labelControl3.Text = "默认";
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(35, 52);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(48, 14);
            this.labelControl2.TabIndex = 1;
            this.labelControl2.Text = "采集频率";
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(35, 15);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(36, 14);
            this.labelControl1.TabIndex = 0;
            this.labelControl1.Text = "血透机";
            // 
            // panelControl2
            // 
            this.panelControl2.Controls.Add(this.gcMachine);
            this.panelControl2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelControl2.Location = new System.Drawing.Point(0, 0);
            this.panelControl2.Name = "panelControl2";
            this.panelControl2.Size = new System.Drawing.Size(746, 356);
            this.panelControl2.TabIndex = 0;
            // 
            // gcMachine
            // 
            this.gcMachine.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gcMachine.Location = new System.Drawing.Point(2, 2);
            this.gcMachine.MainView = this.gvMachine;
            this.gcMachine.Name = "gcMachine";
            this.gcMachine.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemCheckEdit1});
            this.gcMachine.Size = new System.Drawing.Size(742, 352);
            this.gcMachine.TabIndex = 0;
            this.gcMachine.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvMachine});
            this.gcMachine.Click += new System.EventHandler(this.gcMachine_Click);
            // 
            // gvMachine
            // 
            this.gvMachine.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colArea,
            this.colBed,
            this.colMachine,
            this.colStatus});
            this.gvMachine.GridControl = this.gcMachine;
            this.gvMachine.Name = "gvMachine";
            this.gvMachine.OptionsBehavior.Editable = false;
            this.gvMachine.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gvMachine.OptionsView.ShowGroupPanel = false;
            // 
            // colArea
            // 
            this.colArea.Caption = "病区";
            this.colArea.FieldName = "QYNAME";
            this.colArea.Name = "colArea";
            this.colArea.Visible = true;
            this.colArea.VisibleIndex = 0;
            // 
            // colBed
            // 
            this.colBed.Caption = "床位";
            this.colBed.FieldName = "CWNAME";
            this.colBed.Name = "colBed";
            this.colBed.Visible = true;
            this.colBed.VisibleIndex = 1;
            // 
            // colMachine
            // 
            this.colMachine.Caption = "血透机";
            this.colMachine.FieldName = "MACHINE_LABEL";
            this.colMachine.Name = "colMachine";
            this.colMachine.Visible = true;
            this.colMachine.VisibleIndex = 2;
            // 
            // colStatus
            // 
            this.colStatus.Caption = "是否启用";
            this.colStatus.ColumnEdit = this.repositoryItemCheckEdit1;
            this.colStatus.FieldName = "STATUS";
            this.colStatus.Name = "colStatus";
            this.colStatus.Visible = true;
            this.colStatus.VisibleIndex = 3;
            // 
            // repositoryItemCheckEdit1
            // 
            this.repositoryItemCheckEdit1.AutoHeight = false;
            this.repositoryItemCheckEdit1.Name = "repositoryItemCheckEdit1";
            this.repositoryItemCheckEdit1.ValueChecked = "1";
            this.repositoryItemCheckEdit1.ValueUnchecked = "0";
            // 
            // CtlDataGatherSet
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panelControl2);
            this.Controls.Add(this.panelControl3);
            this.Name = "CtlDataGatherSet";
            this.Size = new System.Drawing.Size(746, 480);
            this.Load += new System.EventHandler(this.CtlDataGatherSet_Load);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl3)).EndInit();
            this.panelControl3.ResumeLayout(false);
            this.panelControl3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.seCount.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtMachine.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.seActual.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.seDefault.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).EndInit();
            this.panelControl2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gcMachine)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvMachine)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        //private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.PanelControl panelControl3;
        private DevExpress.XtraEditors.PanelControl panelControl2;
        private DevExpress.XtraGrid.GridControl gcMachine;
        private DevExpress.XtraGrid.Views.Grid.GridView gvMachine;
        private DevExpress.XtraGrid.Columns.GridColumn colArea;
        private DevExpress.XtraGrid.Columns.GridColumn colBed;
        private DevExpress.XtraGrid.Columns.GridColumn colMachine;
        private DevExpress.XtraEditors.SpinEdit seCount;
        private DevExpress.XtraEditors.LabelControl labelControl7;
        private Hemo.Client.Controls.DXSimpleButton btnCancel;
        private Hemo.Client.Controls.DXSimpleButton btnSet;
        private Hemo.Client.Controls.DXSimpleButton btnRefresh;
        private DevExpress.XtraEditors.TextEdit txtMachine;
        private DevExpress.XtraEditors.LabelControl labelControl6;
        private DevExpress.XtraEditors.SpinEdit seActual;
        private DevExpress.XtraEditors.LabelControl labelControl5;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private DevExpress.XtraEditors.SpinEdit seDefault;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraGrid.Columns.GridColumn colStatus;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit repositoryItemCheckEdit1;
    }
}