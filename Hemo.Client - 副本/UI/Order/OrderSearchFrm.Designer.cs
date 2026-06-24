namespace Hemo.Client.UI.Order
{
    partial class OrderSearchFrm
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
            this.btnClose = new Hemo.Client.Controls.DXSimpleButton();
            this.btnSearchOrder = new Hemo.Client.Controls.DXSimpleButton();
            this.deEndEnterDate = new DevExpress.XtraEditors.DateEdit();
            this.lab2 = new DevExpress.XtraEditors.LabelControl();
            this.deBeginEnterDate = new DevExpress.XtraEditors.DateEdit();
            this.lab1 = new DevExpress.XtraEditors.LabelControl();
            this.repositoryItemCheckEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            this.repositoryItemTextEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.repositoryItemTextEdit2 = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.gvOrderInfo = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn3 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn4 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn5 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn6 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gv_Column_ENTER_DATE_TIME = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn7 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn8 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcOrderInfo = new DevExpress.XtraGrid.GridControl();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.deEndEnterDate.Properties.VistaTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.deEndEnterDate.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.deBeginEnterDate.Properties.VistaTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.deBeginEnterDate.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEdit2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvOrderInfo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcOrderInfo)).BeginInit();
            this.SuspendLayout();
            // 
            // panelControl1
            // 
            this.panelControl1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelControl1.Controls.Add(this.btnClose);
            this.panelControl1.Controls.Add(this.btnSearchOrder);
            this.panelControl1.Controls.Add(this.deEndEnterDate);
            this.panelControl1.Controls.Add(this.lab2);
            this.panelControl1.Controls.Add(this.deBeginEnterDate);
            this.panelControl1.Controls.Add(this.lab1);
            this.panelControl1.Location = new System.Drawing.Point(10, 12);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(962, 34);
            this.panelControl1.TabIndex = 7;
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnClose.ImageIndex = 3;
            this.btnClose.Location = new System.Drawing.Point(382, 5);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(87, 27);
            this.btnClose.TabIndex = 20;
            this.btnClose.Text = "关闭";
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnSearchOrder
            // 
            this.btnSearchOrder.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnSearchOrder.ImageIndex = 8;
            this.btnSearchOrder.Location = new System.Drawing.Point(289, 5);
            this.btnSearchOrder.Name = "btnSearchOrder";
            this.btnSearchOrder.Size = new System.Drawing.Size(87, 27);
            this.btnSearchOrder.TabIndex = 19;
            this.btnSearchOrder.Text = "查询医嘱";
            this.btnSearchOrder.Click += new System.EventHandler(this.btnSearchOrder_Click);
            // 
            // deEndEnterDate
            // 
            this.deEndEnterDate.EditValue = null;
            this.deEndEnterDate.Location = new System.Drawing.Point(183, 8);
            this.deEndEnterDate.Name = "deEndEnterDate";
            this.deEndEnterDate.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.deEndEnterDate.Properties.VistaTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.deEndEnterDate.Size = new System.Drawing.Size(100, 21);
            this.deEndEnterDate.TabIndex = 3;
            // 
            // lab2
            // 
            this.lab2.Location = new System.Drawing.Point(165, 11);
            this.lab2.Name = "lab2";
            this.lab2.Size = new System.Drawing.Size(12, 14);
            this.lab2.TabIndex = 2;
            this.lab2.Text = "至";
            // 
            // deBeginEnterDate
            // 
            this.deBeginEnterDate.EditValue = null;
            this.deBeginEnterDate.Location = new System.Drawing.Point(59, 8);
            this.deBeginEnterDate.Name = "deBeginEnterDate";
            this.deBeginEnterDate.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.deBeginEnterDate.Properties.VistaTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.deBeginEnterDate.Size = new System.Drawing.Size(100, 21);
            this.deBeginEnterDate.TabIndex = 1;
            // 
            // lab1
            // 
            this.lab1.Location = new System.Drawing.Point(5, 11);
            this.lab1.Name = "lab1";
            this.lab1.Size = new System.Drawing.Size(48, 14);
            this.lab1.TabIndex = 0;
            this.lab1.Text = "医嘱时间";
            // 
            // repositoryItemCheckEdit1
            // 
            this.repositoryItemCheckEdit1.AutoHeight = false;
            this.repositoryItemCheckEdit1.DisplayValueChecked = "true";
            this.repositoryItemCheckEdit1.DisplayValueUnchecked = "false";
            this.repositoryItemCheckEdit1.Name = "repositoryItemCheckEdit1";
            // 
            // repositoryItemTextEdit1
            // 
            this.repositoryItemTextEdit1.AutoHeight = false;
            this.repositoryItemTextEdit1.Name = "repositoryItemTextEdit1";
            // 
            // repositoryItemTextEdit2
            // 
            this.repositoryItemTextEdit2.AutoHeight = false;
            this.repositoryItemTextEdit2.Name = "repositoryItemTextEdit2";
            // 
            // gvOrderInfo
            // 
            this.gvOrderInfo.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn1,
            this.gridColumn3,
            this.gridColumn4,
            this.gridColumn5,
            this.gridColumn2,
            this.gridColumn6,
            this.gv_Column_ENTER_DATE_TIME,
            this.gridColumn7,
            this.gridColumn8});
            this.gvOrderInfo.GridControl = this.gcOrderInfo;
            this.gvOrderInfo.Name = "gvOrderInfo";
            this.gvOrderInfo.OptionsBehavior.Editable = false;
            this.gvOrderInfo.OptionsBehavior.EditorShowMode = DevExpress.Utils.EditorShowMode.Click;
            this.gvOrderInfo.OptionsView.ShowGroupPanel = false;
            // 
            // gridColumn1
            // 
            this.gridColumn1.Caption = "药品名称";
            this.gridColumn1.FieldName = "ORDER_TEXT";
            this.gridColumn1.Name = "gridColumn1";
            this.gridColumn1.Visible = true;
            this.gridColumn1.VisibleIndex = 0;
            this.gridColumn1.Width = 190;
            // 
            // gridColumn3
            // 
            this.gridColumn3.Caption = "用药途径";
            this.gridColumn3.FieldName = "ADMINISTRATION";
            this.gridColumn3.Name = "gridColumn3";
            this.gridColumn3.Visible = true;
            this.gridColumn3.VisibleIndex = 1;
            this.gridColumn3.Width = 90;
            // 
            // gridColumn4
            // 
            this.gridColumn4.Caption = "单次剂量";
            this.gridColumn4.FieldName = "DOSAGE";
            this.gridColumn4.Name = "gridColumn4";
            this.gridColumn4.Visible = true;
            this.gridColumn4.VisibleIndex = 2;
            this.gridColumn4.Width = 90;
            // 
            // gridColumn5
            // 
            this.gridColumn5.Caption = "剂量单位";
            this.gridColumn5.FieldName = "DOSAGE_UNITS";
            this.gridColumn5.Name = "gridColumn5";
            this.gridColumn5.Visible = true;
            this.gridColumn5.VisibleIndex = 3;
            this.gridColumn5.Width = 90;
            // 
            // gridColumn2
            // 
            this.gridColumn2.Caption = "频次类型";
            this.gridColumn2.FieldName = "FREQUENCY";
            this.gridColumn2.Name = "gridColumn2";
            this.gridColumn2.Visible = true;
            this.gridColumn2.VisibleIndex = 4;
            this.gridColumn2.Width = 90;
            // 
            // gridColumn6
            // 
            this.gridColumn6.Caption = "医生";
            this.gridColumn6.FieldName = "DOCTOR";
            this.gridColumn6.Name = "gridColumn6";
            this.gridColumn6.Visible = true;
            this.gridColumn6.VisibleIndex = 5;
            this.gridColumn6.Width = 60;
            // 
            // gv_Column_ENTER_DATE_TIME
            // 
            this.gv_Column_ENTER_DATE_TIME.Caption = "医嘱时间";
            this.gv_Column_ENTER_DATE_TIME.ColumnEdit = this.repositoryItemTextEdit1;
            this.gv_Column_ENTER_DATE_TIME.FieldName = "ENTER_DATE_TIME";
            this.gv_Column_ENTER_DATE_TIME.Name = "gv_Column_ENTER_DATE_TIME";
            this.gv_Column_ENTER_DATE_TIME.Visible = true;
            this.gv_Column_ENTER_DATE_TIME.VisibleIndex = 6;
            this.gv_Column_ENTER_DATE_TIME.Width = 120;
            // 
            // gridColumn7
            // 
            this.gridColumn7.AppearanceCell.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.gridColumn7.AppearanceCell.Options.UseForeColor = true;
            this.gridColumn7.Caption = "执行护士";
            this.gridColumn7.FieldName = "DRUG_EXEC_NURSE";
            this.gridColumn7.Name = "gridColumn7";
            this.gridColumn7.Visible = true;
            this.gridColumn7.VisibleIndex = 7;
            this.gridColumn7.Width = 90;
            // 
            // gridColumn8
            // 
            this.gridColumn8.AppearanceCell.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.gridColumn8.AppearanceCell.Options.UseForeColor = true;
            this.gridColumn8.Caption = "执行时间";
            this.gridColumn8.ColumnEdit = this.repositoryItemTextEdit2;
            this.gridColumn8.FieldName = "DRUG_EXEC_EXCUTE_DATE";
            this.gridColumn8.Name = "gridColumn8";
            this.gridColumn8.Visible = true;
            this.gridColumn8.VisibleIndex = 8;
            this.gridColumn8.Width = 121;
            // 
            // gcOrderInfo
            // 
            this.gcOrderInfo.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gcOrderInfo.Location = new System.Drawing.Point(10, 52);
            this.gcOrderInfo.MainView = this.gvOrderInfo;
            this.gcOrderInfo.Name = "gcOrderInfo";
            this.gcOrderInfo.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemCheckEdit1,
            this.repositoryItemTextEdit1,
            this.repositoryItemTextEdit2});
            this.gcOrderInfo.Size = new System.Drawing.Size(962, 298);
            this.gcOrderInfo.TabIndex = 6;
            this.gcOrderInfo.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvOrderInfo});
            // 
            // OrderSearchFrm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(984, 362);
            this.Controls.Add(this.panelControl1);
            this.Controls.Add(this.gcOrderInfo);
            this.MinimumSize = new System.Drawing.Size(1000, 400);
            this.Name = "OrderSearchFrm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "医嘱记录";
            this.Load += new System.EventHandler(this.LabFrm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.panelControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.deEndEnterDate.Properties.VistaTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.deEndEnterDate.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.deBeginEnterDate.Properties.VistaTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.deBeginEnterDate.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEdit2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvOrderInfo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcOrderInfo)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.LabelControl lab1;
        private DevExpress.XtraEditors.LabelControl lab2;
        private DevExpress.XtraEditors.DateEdit deBeginEnterDate;
        private DevExpress.XtraEditors.DateEdit deEndEnterDate;
        private Hemo.Client.Controls.DXSimpleButton btnSearchOrder;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit repositoryItemCheckEdit1;
        private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit repositoryItemTextEdit1;
        private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit repositoryItemTextEdit2;
        public DevExpress.XtraGrid.Views.Grid.GridView gvOrderInfo;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn3;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn4;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn5;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn2;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn6;
        private DevExpress.XtraGrid.Columns.GridColumn gv_Column_ENTER_DATE_TIME;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn7;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn8;
        private DevExpress.XtraGrid.GridControl gcOrderInfo;
        private Hemo.Client.Controls.DXSimpleButton btnClose;
    }
}