namespace Hemo.Client.UI.Order
{
    partial class OrderExecFrm
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
            this.gcOrderInfo = new DevExpress.XtraGrid.GridControl();
            this.gvOrderInfo = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn8 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemCheckEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn3 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn4 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn5 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn6 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gv_Column_ENTER_DATE_TIME = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemTextEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.gv_Column_ORDER_EXEC_STATUS = new DevExpress.XtraGrid.Columns.GridColumn();
            this.btnSyncOrderInfo = new Hemo.Client.Controls.DXSimpleButton();
            this.picLoading = new DevExpress.XtraEditors.PictureEdit();
            this.btnExecOrder = new Hemo.Client.Controls.DXSimpleButton();
            this.btnSelectAll = new Hemo.Client.Controls.DXSimpleButton();
            this.btnCancelSelected = new Hemo.Client.Controls.DXSimpleButton();
            this.btnClose = new Hemo.Client.Controls.DXSimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.gcOrderInfo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvOrderInfo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picLoading.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // gcOrderInfo
            // 
            this.gcOrderInfo.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gcOrderInfo.Location = new System.Drawing.Point(12, 12);
            this.gcOrderInfo.MainView = this.gvOrderInfo;
            this.gcOrderInfo.Name = "gcOrderInfo";
            this.gcOrderInfo.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemCheckEdit1,
            this.repositoryItemTextEdit1});
            this.gcOrderInfo.Size = new System.Drawing.Size(962, 305);
            this.gcOrderInfo.TabIndex = 6;
            this.gcOrderInfo.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvOrderInfo});
            // 
            // gvOrderInfo
            // 
            this.gvOrderInfo.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn8,
            this.gridColumn1,
            this.gridColumn3,
            this.gridColumn4,
            this.gridColumn5,
            this.gridColumn2,
            this.gridColumn6,
            this.gv_Column_ENTER_DATE_TIME,
            this.gv_Column_ORDER_EXEC_STATUS});
            this.gvOrderInfo.GridControl = this.gcOrderInfo;
            this.gvOrderInfo.Name = "gvOrderInfo";
            this.gvOrderInfo.OptionsBehavior.Editable = false;
            this.gvOrderInfo.OptionsBehavior.EditorShowMode = DevExpress.Utils.EditorShowMode.Click;
            this.gvOrderInfo.OptionsView.ShowGroupPanel = false;
            this.gvOrderInfo.CustomDrawCell += new DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventHandler(this.gvOrderInfo_CustomDrawCell);
            this.gvOrderInfo.MouseDown += new System.Windows.Forms.MouseEventHandler(this.gvOrderInfo_MouseDown);
            // 
            // gridColumn8
            // 
            this.gridColumn8.Caption = "选择";
            this.gridColumn8.ColumnEdit = this.repositoryItemCheckEdit1;
            this.gridColumn8.FieldName = "CHOOSE";
            this.gridColumn8.Name = "gridColumn8";
            this.gridColumn8.Visible = true;
            this.gridColumn8.VisibleIndex = 0;
            this.gridColumn8.Width = 50;
            // 
            // repositoryItemCheckEdit1
            // 
            this.repositoryItemCheckEdit1.AutoHeight = false;
            this.repositoryItemCheckEdit1.DisplayValueChecked = "true";
            this.repositoryItemCheckEdit1.DisplayValueUnchecked = "false";
            this.repositoryItemCheckEdit1.Name = "repositoryItemCheckEdit1";
            // 
            // gridColumn1
            // 
            this.gridColumn1.Caption = "药品名称";
            this.gridColumn1.FieldName = "ORDER_TEXT";
            this.gridColumn1.Name = "gridColumn1";
            this.gridColumn1.Visible = true;
            this.gridColumn1.VisibleIndex = 1;
            this.gridColumn1.Width = 200;
            // 
            // gridColumn3
            // 
            this.gridColumn3.Caption = "用药途径";
            this.gridColumn3.FieldName = "ADMINISTRATION";
            this.gridColumn3.Name = "gridColumn3";
            this.gridColumn3.Visible = true;
            this.gridColumn3.VisibleIndex = 2;
            this.gridColumn3.Width = 90;
            // 
            // gridColumn4
            // 
            this.gridColumn4.Caption = "单次剂量";
            this.gridColumn4.FieldName = "DOSAGE";
            this.gridColumn4.Name = "gridColumn4";
            this.gridColumn4.Visible = true;
            this.gridColumn4.VisibleIndex = 3;
            this.gridColumn4.Width = 90;
            // 
            // gridColumn5
            // 
            this.gridColumn5.Caption = "剂量单位";
            this.gridColumn5.FieldName = "DOSAGE_UNITS";
            this.gridColumn5.Name = "gridColumn5";
            this.gridColumn5.Visible = true;
            this.gridColumn5.VisibleIndex = 4;
            this.gridColumn5.Width = 90;
            // 
            // gridColumn2
            // 
            this.gridColumn2.Caption = "频次类型";
            this.gridColumn2.FieldName = "FREQUENCY";
            this.gridColumn2.Name = "gridColumn2";
            this.gridColumn2.Visible = true;
            this.gridColumn2.VisibleIndex = 5;
            this.gridColumn2.Width = 90;
            // 
            // gridColumn6
            // 
            this.gridColumn6.Caption = "医生";
            this.gridColumn6.FieldName = "DOCTOR";
            this.gridColumn6.Name = "gridColumn6";
            this.gridColumn6.Visible = true;
            this.gridColumn6.VisibleIndex = 6;
            this.gridColumn6.Width = 90;
            // 
            // gv_Column_ENTER_DATE_TIME
            // 
            this.gv_Column_ENTER_DATE_TIME.Caption = "医嘱时间";
            this.gv_Column_ENTER_DATE_TIME.ColumnEdit = this.repositoryItemTextEdit1;
            this.gv_Column_ENTER_DATE_TIME.FieldName = "ENTER_DATE_TIME";
            this.gv_Column_ENTER_DATE_TIME.Name = "gv_Column_ENTER_DATE_TIME";
            this.gv_Column_ENTER_DATE_TIME.Visible = true;
            this.gv_Column_ENTER_DATE_TIME.VisibleIndex = 7;
            this.gv_Column_ENTER_DATE_TIME.Width = 130;
            // 
            // repositoryItemTextEdit1
            // 
            this.repositoryItemTextEdit1.AutoHeight = false;
            this.repositoryItemTextEdit1.Name = "repositoryItemTextEdit1";
            // 
            // gv_Column_ORDER_EXEC_STATUS
            // 
            this.gv_Column_ORDER_EXEC_STATUS.Caption = "医嘱执行状态";
            this.gv_Column_ORDER_EXEC_STATUS.FieldName = "ORDER_EXEC_STATUS";
            this.gv_Column_ORDER_EXEC_STATUS.Name = "gv_Column_ORDER_EXEC_STATUS";
            this.gv_Column_ORDER_EXEC_STATUS.Visible = true;
            this.gv_Column_ORDER_EXEC_STATUS.VisibleIndex = 8;
            this.gv_Column_ORDER_EXEC_STATUS.Width = 95;
            // 
            // btnSyncOrderInfo
            // 
            this.btnSyncOrderInfo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSyncOrderInfo.ImageIndex = 10;
            this.btnSyncOrderInfo.Location = new System.Drawing.Point(638, 323);
            this.btnSyncOrderInfo.Name = "btnSyncOrderInfo";
            this.btnSyncOrderInfo.Size = new System.Drawing.Size(113, 27);
            this.btnSyncOrderInfo.TabIndex = 8;
            this.btnSyncOrderInfo.Text = "同步医嘱记录";
            this.btnSyncOrderInfo.Click += new System.EventHandler(this.btnSyncOrderInfo_Click);
            // 
            // picLoading
            // 
            this.picLoading.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.picLoading.BackgroundImage = global::Hemo.Client.Properties.Resources._257;
            this.picLoading.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.picLoading.EditValue = global::Hemo.Client.Properties.Resources._257;
            this.picLoading.Location = new System.Drawing.Point(942, 323);
            this.picLoading.Name = "picLoading";
            this.picLoading.Properties.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.picLoading.Properties.Appearance.Options.UseBackColor = true;
            this.picLoading.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.picLoading.Size = new System.Drawing.Size(44, 40);
            this.picLoading.TabIndex = 17;
            this.picLoading.ToolTip = "正在同步医嘱记录，请稍等";
            this.picLoading.Visible = false;
            // 
            // btnExecOrder
            // 
            this.btnExecOrder.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnExecOrder.ImageIndex = 7;
            this.btnExecOrder.Location = new System.Drawing.Point(757, 323);
            this.btnExecOrder.Name = "btnExecOrder";
            this.btnExecOrder.Size = new System.Drawing.Size(87, 27);
            this.btnExecOrder.TabIndex = 18;
            this.btnExecOrder.Text = "执行医嘱";
            this.btnExecOrder.Click += new System.EventHandler(this.btnExecOrder_Click);
            // 
            // btnSelectAll
            // 
            this.btnSelectAll.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSelectAll.ImageIndex = 12;
            this.btnSelectAll.Location = new System.Drawing.Point(452, 323);
            this.btnSelectAll.Name = "btnSelectAll";
            this.btnSelectAll.Size = new System.Drawing.Size(87, 27);
            this.btnSelectAll.TabIndex = 19;
            this.btnSelectAll.Text = "全选";
            this.btnSelectAll.Click += new System.EventHandler(this.btnSelectAll_Click);
            // 
            // btnCancelSelected
            // 
            this.btnCancelSelected.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancelSelected.ImageIndex = 13;
            this.btnCancelSelected.Location = new System.Drawing.Point(545, 323);
            this.btnCancelSelected.Name = "btnCancelSelected";
            this.btnCancelSelected.Size = new System.Drawing.Size(87, 27);
            this.btnCancelSelected.TabIndex = 20;
            this.btnCancelSelected.Text = "全部取消";
            this.btnCancelSelected.Click += new System.EventHandler(this.btnCancelSelected_Click);
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.ImageIndex = 3;
            this.btnClose.Location = new System.Drawing.Point(849, 323);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(87, 27);
            this.btnClose.TabIndex = 21;
            this.btnClose.Text = "关闭";
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // OrderExecFrm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(984, 362);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnCancelSelected);
            this.Controls.Add(this.btnSelectAll);
            this.Controls.Add(this.btnExecOrder);
            this.Controls.Add(this.picLoading);
            this.Controls.Add(this.btnSyncOrderInfo);
            this.Controls.Add(this.gcOrderInfo);
            this.MinimumSize = new System.Drawing.Size(1000, 400);
            this.Name = "OrderExecFrm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "医嘱记录";
            this.Load += new System.EventHandler(this.LabFrm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gcOrderInfo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvOrderInfo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picLoading.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraGrid.GridControl gcOrderInfo;
        public DevExpress.XtraGrid.Views.Grid.GridView gvOrderInfo;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn3;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn4;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn5;
        private Hemo.Client.Controls.DXSimpleButton btnSyncOrderInfo;
        private DevExpress.XtraEditors.PictureEdit picLoading;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn2;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn6;
        private DevExpress.XtraGrid.Columns.GridColumn gv_Column_ENTER_DATE_TIME;
        private DevExpress.XtraGrid.Columns.GridColumn gv_Column_ORDER_EXEC_STATUS;
        private Hemo.Client.Controls.DXSimpleButton btnExecOrder;
        private Hemo.Client.Controls.DXSimpleButton btnSelectAll;
        private Hemo.Client.Controls.DXSimpleButton btnCancelSelected;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn8;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit repositoryItemCheckEdit1;
        private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit repositoryItemTextEdit1;
        private Hemo.Client.Controls.DXSimpleButton btnClose;
    }
}