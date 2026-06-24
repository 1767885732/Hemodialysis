namespace Hemo.Client.UI.Drug {
    partial class CtlQueryDrugMaster {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.components = new System.ComponentModel.Container();
            this.panelControl2 = new DevExpress.XtraEditors.PanelControl();
            this.pcTools = new DevExpress.XtraEditors.PanelControl();
            this.btnEdit = new Hemo.Client.Controls.DXSimpleButton();
            this.btnAdd = new Hemo.Client.Controls.DXSimpleButton();
            this.chkFilter = new DevExpress.XtraEditors.CheckEdit();
            this.gridDrugMaster = new DevExpress.XtraGrid.GridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn3 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemCustomGridLookUpEdit1 = new Hemo.Utilities.RepositoryItemCustomGridLookUpEdit();
            this.repositoryItemCustomGridLookUpEdit1View = new Hemo.Utilities.CustomGridView();
            this.gridColumn4 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn5 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn6 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn7 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn8 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemGridLookUpEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemGridLookUpEdit();
            this.repositoryItemGridLookUpEdit1View = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn12 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn13 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn9 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.txtDRUG_NAME = new DevExpress.XtraEditors.TextEdit();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.txtFIRM_ID = new DevExpress.XtraEditors.TextEdit();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.txtDRUG_CODE = new DevExpress.XtraEditors.TextEdit();
            this.btnQuery = new Hemo.Client.Controls.DXSimpleButton();
            this.pnlControls = new DevExpress.XtraEditors.PanelControl();
            this.gridColumn10 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn11 = new DevExpress.XtraGrid.Columns.GridColumn();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).BeginInit();
            this.panelControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pcTools)).BeginInit();
            this.pcTools.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chkFilter.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridDrugMaster)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCustomGridLookUpEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCustomGridLookUpEdit1View)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemGridLookUpEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemGridLookUpEdit1View)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDRUG_NAME.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtFIRM_ID.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDRUG_CODE.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pnlControls)).BeginInit();
            this.pnlControls.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelControl2
            // 
            this.panelControl2.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panelControl2.Controls.Add(this.pcTools);
            this.panelControl2.Controls.Add(this.gridDrugMaster);
            this.panelControl2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelControl2.Location = new System.Drawing.Point(0, 10);
            this.panelControl2.Name = "panelControl2";
            this.panelControl2.Padding = new System.Windows.Forms.Padding(0, 8, 0, 0);
            this.panelControl2.Size = new System.Drawing.Size(878, 526);
            this.panelControl2.TabIndex = 3;
            // 
            // pcTools
            // 
            this.pcTools.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pcTools.Controls.Add(this.btnEdit);
            this.pcTools.Controls.Add(this.btnAdd);
            this.pcTools.Controls.Add(this.chkFilter);
            this.pcTools.Location = new System.Drawing.Point(6, 489);
            this.pcTools.Name = "pcTools";
            this.pcTools.Size = new System.Drawing.Size(872, 34);
            this.pcTools.TabIndex = 313;
            // 
            // btnEdit
            // 
            this.btnEdit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnEdit.Enabled = false;
            this.btnEdit.ImageIndex = 2;
            this.btnEdit.Location = new System.Drawing.Point(778, 6);
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Size = new System.Drawing.Size(75, 23);
            this.btnEdit.TabIndex = 313;
            this.btnEdit.Text = "编辑(&E)";
            this.btnEdit.Click += new System.EventHandler(this.btnEdit_Click);
            // 
            // btnAdd
            // 
            this.btnAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAdd.ImageIndex = 0;
            this.btnAdd.Location = new System.Drawing.Point(697, 6);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(75, 23);
            this.btnAdd.TabIndex = 312;
            this.btnAdd.Text = "新增(&A)";
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // chkFilter
            // 
            this.chkFilter.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.chkFilter.Location = new System.Drawing.Point(5, 8);
            this.chkFilter.Name = "chkFilter";
            this.chkFilter.Properties.Caption = "显示过虑行";
            this.chkFilter.Size = new System.Drawing.Size(90, 19);
            this.chkFilter.TabIndex = 311;
            this.chkFilter.CheckedChanged += new System.EventHandler(this.chkFilter_CheckedChanged);
            // 
            // gridDrugMaster
            // 
            this.gridDrugMaster.Dock = System.Windows.Forms.DockStyle.Top;
            this.gridDrugMaster.Location = new System.Drawing.Point(0, 8);
            this.gridDrugMaster.MainView = this.gridView1;
            this.gridDrugMaster.Name = "gridDrugMaster";
            this.gridDrugMaster.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemCustomGridLookUpEdit1,
            this.repositoryItemGridLookUpEdit1});
            this.gridDrugMaster.Size = new System.Drawing.Size(878, 468);
            this.gridDrugMaster.TabIndex = 3;
            this.gridDrugMaster.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            // 
            // gridView1
            // 
            this.gridView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn1,
            this.gridColumn2,
            this.gridColumn3,
            this.gridColumn4,
            this.gridColumn5,
            this.gridColumn6,
            this.gridColumn7,
            this.gridColumn8,
            this.gridColumn9});
            this.gridView1.GridControl = this.gridDrugMaster;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsBehavior.Editable = false;
            this.gridView1.OptionsBehavior.EditorShowMode = DevExpress.Utils.EditorShowMode.Click;
            this.gridView1.OptionsView.ShowGroupPanel = false;
            this.gridView1.RowClick += new DevExpress.XtraGrid.Views.Grid.RowClickEventHandler(this.gridView1_RowClick);
            this.gridView1.DoubleClick += new System.EventHandler(this.gridView1_DoubleClick);
            // 
            // gridColumn1
            // 
            this.gridColumn1.Caption = "项目编号";
            this.gridColumn1.FieldName = "DRUG_CODE";
            this.gridColumn1.Name = "gridColumn1";
            this.gridColumn1.Visible = true;
            this.gridColumn1.VisibleIndex = 1;
            this.gridColumn1.Width = 153;
            // 
            // gridColumn2
            // 
            this.gridColumn2.Caption = "项目名称";
            this.gridColumn2.FieldName = "DRUG_NAME";
            this.gridColumn2.Name = "gridColumn2";
            this.gridColumn2.Visible = true;
            this.gridColumn2.VisibleIndex = 2;
            this.gridColumn2.Width = 153;
            // 
            // gridColumn3
            // 
            this.gridColumn3.Caption = "项目分类";
            this.gridColumn3.ColumnEdit = this.repositoryItemCustomGridLookUpEdit1;
            this.gridColumn3.FieldName = "DRUG_TYPE";
            this.gridColumn3.Name = "gridColumn3";
            this.gridColumn3.Visible = true;
            this.gridColumn3.VisibleIndex = 0;
            this.gridColumn3.Width = 102;
            // 
            // repositoryItemCustomGridLookUpEdit1
            // 
            this.repositoryItemCustomGridLookUpEdit1.AutoComplete = false;
            this.repositoryItemCustomGridLookUpEdit1.AutoHeight = false;
            this.repositoryItemCustomGridLookUpEdit1.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemCustomGridLookUpEdit1.DisplayMember = "ITEM_NAME";
            this.repositoryItemCustomGridLookUpEdit1.Name = "repositoryItemCustomGridLookUpEdit1";
            this.repositoryItemCustomGridLookUpEdit1.NullText = "";
            this.repositoryItemCustomGridLookUpEdit1.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard;
            this.repositoryItemCustomGridLookUpEdit1.ValueMember = "ITEM_ID";
            this.repositoryItemCustomGridLookUpEdit1.View = this.repositoryItemCustomGridLookUpEdit1View;
            // 
            // repositoryItemCustomGridLookUpEdit1View
            // 
            this.repositoryItemCustomGridLookUpEdit1View.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.repositoryItemCustomGridLookUpEdit1View.Name = "repositoryItemCustomGridLookUpEdit1View";
            this.repositoryItemCustomGridLookUpEdit1View.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.repositoryItemCustomGridLookUpEdit1View.OptionsView.ShowGroupPanel = false;
            // 
            // gridColumn4
            // 
            this.gridColumn4.Caption = "药品厂商编号";
            this.gridColumn4.FieldName = "FIRM_ID";
            this.gridColumn4.Name = "gridColumn4";
            this.gridColumn4.Width = 153;
            // 
            // gridColumn5
            // 
            this.gridColumn5.Caption = "药品厂商名称";
            this.gridColumn5.FieldName = "FIRM_NAME";
            this.gridColumn5.Name = "gridColumn5";
            this.gridColumn5.Visible = true;
            this.gridColumn5.VisibleIndex = 3;
            this.gridColumn5.Width = 153;
            // 
            // gridColumn6
            // 
            this.gridColumn6.Caption = "规格";
            this.gridColumn6.FieldName = "DRUG_SPEC";
            this.gridColumn6.Name = "gridColumn6";
            this.gridColumn6.Visible = true;
            this.gridColumn6.VisibleIndex = 5;
            this.gridColumn6.Width = 153;
            // 
            // gridColumn7
            // 
            this.gridColumn7.Caption = "单价";
            this.gridColumn7.FieldName = "PRICE";
            this.gridColumn7.Name = "gridColumn7";
            this.gridColumn7.Visible = true;
            this.gridColumn7.VisibleIndex = 6;
            this.gridColumn7.Width = 153;
            // 
            // gridColumn8
            // 
            this.gridColumn8.Caption = "单位";
            this.gridColumn8.ColumnEdit = this.repositoryItemGridLookUpEdit1;
            this.gridColumn8.FieldName = "UNITS";
            this.gridColumn8.Name = "gridColumn8";
            this.gridColumn8.Visible = true;
            this.gridColumn8.VisibleIndex = 4;
            // 
            // repositoryItemGridLookUpEdit1
            // 
            this.repositoryItemGridLookUpEdit1.AutoHeight = false;
            this.repositoryItemGridLookUpEdit1.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemGridLookUpEdit1.DisplayMember = "ITEM_NAME";
            this.repositoryItemGridLookUpEdit1.Name = "repositoryItemGridLookUpEdit1";
            this.repositoryItemGridLookUpEdit1.NullText = "";
            this.repositoryItemGridLookUpEdit1.ValueMember = "ITEM_ID";
            this.repositoryItemGridLookUpEdit1.View = this.repositoryItemGridLookUpEdit1View;
            // 
            // repositoryItemGridLookUpEdit1View
            // 
            this.repositoryItemGridLookUpEdit1View.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn12,
            this.gridColumn13});
            this.repositoryItemGridLookUpEdit1View.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.repositoryItemGridLookUpEdit1View.Name = "repositoryItemGridLookUpEdit1View";
            this.repositoryItemGridLookUpEdit1View.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.repositoryItemGridLookUpEdit1View.OptionsView.ShowGroupPanel = false;
            // 
            // gridColumn12
            // 
            this.gridColumn12.Caption = "编号";
            this.gridColumn12.FieldName = "ITEM_ID";
            this.gridColumn12.Name = "gridColumn12";
            this.gridColumn12.Visible = true;
            this.gridColumn12.VisibleIndex = 0;
            // 
            // gridColumn13
            // 
            this.gridColumn13.Caption = "名称";
            this.gridColumn13.FieldName = "ITEM_NAME";
            this.gridColumn13.Name = "gridColumn13";
            this.gridColumn13.Visible = true;
            this.gridColumn13.VisibleIndex = 1;
            // 
            // gridColumn9
            // 
            this.gridColumn9.Caption = "用途说明";
            this.gridColumn9.FieldName = "MEMOS";
            this.gridColumn9.Name = "gridColumn9";
            this.gridColumn9.Visible = true;
            this.gridColumn9.VisibleIndex = 7;
            this.gridColumn9.Width = 156;
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(16, 21);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(48, 14);
            this.labelControl1.TabIndex = 8;
            this.labelControl1.Text = "药品名称";
            // 
            // txtDRUG_NAME
            // 
            this.txtDRUG_NAME.Location = new System.Drawing.Point(70, 18);
            this.txtDRUG_NAME.Name = "txtDRUG_NAME";
            this.txtDRUG_NAME.Size = new System.Drawing.Size(117, 20);
            this.txtDRUG_NAME.TabIndex = 1;
            this.txtDRUG_NAME.ToolTip = "可输入药品名或拼音码";
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(195, 21);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(48, 14);
            this.labelControl2.TabIndex = 10;
            this.labelControl2.Text = "厂商编号";
            // 
            // txtFIRM_ID
            // 
            this.txtFIRM_ID.Location = new System.Drawing.Point(249, 18);
            this.txtFIRM_ID.Name = "txtFIRM_ID";
            this.txtFIRM_ID.Size = new System.Drawing.Size(122, 20);
            this.txtFIRM_ID.TabIndex = 2;
            // 
            // labelControl3
            // 
            this.labelControl3.Location = new System.Drawing.Point(385, 21);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(48, 14);
            this.labelControl3.TabIndex = 12;
            this.labelControl3.Text = "药品编号";
            // 
            // txtDRUG_CODE
            // 
            this.txtDRUG_CODE.Location = new System.Drawing.Point(439, 18);
            this.txtDRUG_CODE.Name = "txtDRUG_CODE";
            this.txtDRUG_CODE.Size = new System.Drawing.Size(130, 20);
            this.txtDRUG_CODE.TabIndex = 3;
            // 
            // btnQuery
            // 
            this.btnQuery.ImageIndex = 8;
            this.btnQuery.Location = new System.Drawing.Point(596, 16);
            this.btnQuery.Name = "btnQuery";
            this.btnQuery.Size = new System.Drawing.Size(75, 23);
            this.btnQuery.TabIndex = 33;
            this.btnQuery.Text = "查询";
            this.btnQuery.Click += new System.EventHandler(this.btnQuery_Click);
            // 
            // pnlControls
            // 
            this.pnlControls.Controls.Add(this.btnQuery);
            this.pnlControls.Controls.Add(this.txtDRUG_CODE);
            this.pnlControls.Controls.Add(this.labelControl3);
            this.pnlControls.Controls.Add(this.txtFIRM_ID);
            this.pnlControls.Controls.Add(this.labelControl2);
            this.pnlControls.Controls.Add(this.txtDRUG_NAME);
            this.pnlControls.Controls.Add(this.labelControl1);
            this.pnlControls.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlControls.Location = new System.Drawing.Point(0, 0);
            this.pnlControls.Name = "pnlControls";
            this.pnlControls.Size = new System.Drawing.Size(878, 10);
            this.pnlControls.TabIndex = 2;
            // 
            // gridColumn10
            // 
            this.gridColumn10.Caption = "名称";
            this.gridColumn10.FieldName = "ITEM_NAME";
            this.gridColumn10.Name = "gridColumn10";
            this.gridColumn10.Visible = true;
            this.gridColumn10.VisibleIndex = 0;
            // 
            // gridColumn11
            // 
            this.gridColumn11.Caption = "gridColumn11";
            this.gridColumn11.Name = "gridColumn11";
            this.gridColumn11.Visible = true;
            this.gridColumn11.VisibleIndex = 1;
            // 
            // CtlQueryDrugMaster
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panelControl2);
            this.Controls.Add(this.pnlControls);
            this.Name = "CtlQueryDrugMaster";
            this.Size = new System.Drawing.Size(878, 536);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).EndInit();
            this.panelControl2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pcTools)).EndInit();
            this.pcTools.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.chkFilter.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridDrugMaster)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCustomGridLookUpEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCustomGridLookUpEdit1View)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemGridLookUpEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemGridLookUpEdit1View)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDRUG_NAME.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtFIRM_ID.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDRUG_CODE.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pnlControls)).EndInit();
            this.pnlControls.ResumeLayout(false);
            this.pnlControls.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl panelControl2;
        private DevExpress.XtraGrid.GridControl gridDrugMaster;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn2;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn3;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn4;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn5;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn6;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn7;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn8;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn9;
        private DevExpress.XtraEditors.PanelControl pcTools;
        private Hemo.Client.Controls.DXSimpleButton btnEdit;
        private Hemo.Client.Controls.DXSimpleButton btnAdd;
        private DevExpress.XtraEditors.CheckEdit chkFilter;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.TextEdit txtDRUG_NAME;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.TextEdit txtFIRM_ID;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.TextEdit txtDRUG_CODE;
        private Hemo.Client.Controls.DXSimpleButton btnQuery;
        private DevExpress.XtraEditors.PanelControl pnlControls;
        private Utilities.RepositoryItemCustomGridLookUpEdit repositoryItemCustomGridLookUpEdit1;
        private Utilities.CustomGridView repositoryItemCustomGridLookUpEdit1View;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn10;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn11;
        private DevExpress.XtraEditors.Repository.RepositoryItemGridLookUpEdit repositoryItemGridLookUpEdit1;
        private DevExpress.XtraGrid.Views.Grid.GridView repositoryItemGridLookUpEdit1View;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn12;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn13;
    }
}
