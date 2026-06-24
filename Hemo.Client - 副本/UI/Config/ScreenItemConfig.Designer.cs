namespace Hemo.Client.UI.Config
{
    partial class ScreenItemConfig
    {
        /// <summary> 
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.pcTools = new DevExpress.XtraEditors.PanelControl();
            this.chkFilter = new DevExpress.XtraEditors.CheckEdit();
            this.btnEdit = new Hemo.Client.Controls.DXSimpleButton();
            this.gcConfig = new DevExpress.XtraGrid.GridControl();
            this.gvConfig = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn3 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn4 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemCheckEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            this.gridColumn5 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.xtraTabControl1 = new DevExpress.XtraTab.XtraTabControl();
            this.xtraTabPage1 = new DevExpress.XtraTab.XtraTabPage();
            this.repositoryItemGridLookUpEdit1View = new DevExpress.XtraGrid.Views.Grid.GridView();
            ((System.ComponentModel.ISupportInitialize)(this.pcTools)).BeginInit();
            this.pcTools.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chkFilter.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcConfig)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvConfig)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xtraTabControl1)).BeginInit();
            this.xtraTabControl1.SuspendLayout();
            this.xtraTabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemGridLookUpEdit1View)).BeginInit();
            this.SuspendLayout();
            // 
            // pcTools
            // 
            this.pcTools.Controls.Add(this.chkFilter);
            this.pcTools.Controls.Add(this.btnEdit);
            this.pcTools.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pcTools.Location = new System.Drawing.Point(0, 428);
            this.pcTools.Name = "pcTools";
            this.pcTools.Size = new System.Drawing.Size(929, 40);
            this.pcTools.TabIndex = 313;
            // 
            // chkFilter
            // 
            this.chkFilter.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.chkFilter.Location = new System.Drawing.Point(6, 9);
            this.chkFilter.Name = "chkFilter";
            this.chkFilter.Properties.Caption = "显示过虑行";
            this.chkFilter.Size = new System.Drawing.Size(105, 19);
            this.chkFilter.TabIndex = 311;
            // 
            // btnEdit
            // 
            this.btnEdit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnEdit.ImageIndex = 2;
            this.btnEdit.Location = new System.Drawing.Point(822, 5);
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Size = new System.Drawing.Size(93, 29);
            this.btnEdit.TabIndex = 310;
            this.btnEdit.Text = "编辑(&E)";
            this.btnEdit.Click += new System.EventHandler(this.btnEdit_Click);
            // 
            // gcConfig
            // 
            this.gcConfig.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gcConfig.Location = new System.Drawing.Point(0, 0);
            this.gcConfig.MainView = this.gvConfig;
            this.gcConfig.Name = "gcConfig";
            this.gcConfig.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemCheckEdit1});
            this.gcConfig.Size = new System.Drawing.Size(922, 398);
            this.gcConfig.TabIndex = 312;
            this.gcConfig.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvConfig});
            // 
            // gvConfig
            // 
            this.gvConfig.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn1,
            this.gridColumn2,
            this.gridColumn3,
            this.gridColumn4,
            this.gridColumn5});
            this.gvConfig.GridControl = this.gcConfig;
            this.gvConfig.Name = "gvConfig";
            this.gvConfig.OptionsBehavior.Editable = false;
            this.gvConfig.OptionsBehavior.EditorShowMode = DevExpress.Utils.EditorShowMode.Click;
            this.gvConfig.OptionsView.ShowGroupPanel = false;
            this.gvConfig.RowClick += new DevExpress.XtraGrid.Views.Grid.RowClickEventHandler(this.gvConfig_RowClick);
            // 
            // gridColumn1
            // 
            this.gridColumn1.Caption = "值";
            this.gridColumn1.FieldName = "ITEM_VALUE";
            this.gridColumn1.Name = "gridColumn1";
            this.gridColumn1.Visible = true;
            this.gridColumn1.VisibleIndex = 1;
            // 
            // gridColumn2
            // 
            this.gridColumn2.Caption = "名称";
            this.gridColumn2.FieldName = "ITEM_NAME";
            this.gridColumn2.Name = "gridColumn2";
            this.gridColumn2.Visible = true;
            this.gridColumn2.VisibleIndex = 0;
            // 
            // gridColumn3
            // 
            this.gridColumn3.Caption = "类型";
            this.gridColumn3.FieldName = "ITEM_TYPE";
            this.gridColumn3.Name = "gridColumn3";
            this.gridColumn3.Visible = true;
            this.gridColumn3.VisibleIndex = 2;
            // 
            // gridColumn4
            // 
            this.gridColumn4.Caption = "是否启用";
            this.gridColumn4.ColumnEdit = this.repositoryItemCheckEdit1;
            this.gridColumn4.FieldName = "STATUS";
            this.gridColumn4.Name = "gridColumn4";
            this.gridColumn4.Visible = true;
            this.gridColumn4.VisibleIndex = 3;
            // 
            // repositoryItemCheckEdit1
            // 
            this.repositoryItemCheckEdit1.AutoHeight = false;
            this.repositoryItemCheckEdit1.Name = "repositoryItemCheckEdit1";
            this.repositoryItemCheckEdit1.NullStyle = DevExpress.XtraEditors.Controls.StyleIndeterminate.Unchecked;
            this.repositoryItemCheckEdit1.ValueChecked = "1";
            this.repositoryItemCheckEdit1.ValueUnchecked = "0";
            // 
            // gridColumn5
            // 
            this.gridColumn5.Caption = "排序字段";
            this.gridColumn5.FieldName = "ORDER_NUMBER";
            this.gridColumn5.Name = "gridColumn5";
            this.gridColumn5.Visible = true;
            this.gridColumn5.VisibleIndex = 4;
            // 
            // xtraTabControl1
            // 
            this.xtraTabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.xtraTabControl1.Location = new System.Drawing.Point(0, 0);
            this.xtraTabControl1.Name = "xtraTabControl1";
            this.xtraTabControl1.SelectedTabPage = this.xtraTabPage1;
            this.xtraTabControl1.Size = new System.Drawing.Size(929, 428);
            this.xtraTabControl1.TabIndex = 314;
            this.xtraTabControl1.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
            this.xtraTabPage1});
            // 
            // xtraTabPage1
            // 
            this.xtraTabPage1.Controls.Add(this.gcConfig);
            this.xtraTabPage1.Name = "xtraTabPage1";
            this.xtraTabPage1.Size = new System.Drawing.Size(922, 398);
            this.xtraTabPage1.Text = "大屏配置项";
            // 
            // repositoryItemGridLookUpEdit1View
            // 
            this.repositoryItemGridLookUpEdit1View.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.repositoryItemGridLookUpEdit1View.Name = "repositoryItemGridLookUpEdit1View";
            this.repositoryItemGridLookUpEdit1View.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.repositoryItemGridLookUpEdit1View.OptionsView.ShowGroupPanel = false;
            // 
            // ScreenItemConfig
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.xtraTabControl1);
            this.Controls.Add(this.pcTools);
            this.Name = "ScreenItemConfig";
            this.Size = new System.Drawing.Size(929, 468);
            this.Load += new System.EventHandler(this.ScreenItemConfig_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pcTools)).EndInit();
            this.pcTools.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.chkFilter.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcConfig)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvConfig)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xtraTabControl1)).EndInit();
            this.xtraTabControl1.ResumeLayout(false);
            this.xtraTabPage1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemGridLookUpEdit1View)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl pcTools;
        private DevExpress.XtraEditors.CheckEdit chkFilter;
        private Controls.DXSimpleButton btnEdit;
        private DevExpress.XtraGrid.GridControl gcConfig;
        public DevExpress.XtraGrid.Views.Grid.GridView gvConfig;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn2;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn3;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn4;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit repositoryItemCheckEdit1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn5;
        private DevExpress.XtraTab.XtraTabControl xtraTabControl1;
        private DevExpress.XtraTab.XtraTabPage xtraTabPage1;
        private DevExpress.XtraGrid.Views.Grid.GridView repositoryItemGridLookUpEdit1View;

    }
}
