namespace Hemo.Client.Modules.Config
{
    partial class SetUserAreaMappingFrm
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
            this.lab6 = new DevExpress.XtraEditors.LabelControl();
            this.cbxUSER = new DevExpress.XtraEditors.DropDownButton();
            this.btnSave = new DevExpress.XtraEditors.SimpleButton();
            this.gcAreaInfo = new DevExpress.XtraGrid.GridControl();
            this.gvAreaInfo = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn8 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemCheckEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemTextEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            ((System.ComponentModel.ISupportInitialize)(this.gcAreaInfo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvAreaInfo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEdit1)).BeginInit();
            this.SuspendLayout();
            // 
            // lab6
            // 
            this.lab6.Location = new System.Drawing.Point(14, 20);
            this.lab6.Name = "lab6";
            this.lab6.Size = new System.Drawing.Size(72, 14);
            this.lab6.TabIndex = 340;
            this.lab6.Text = "本系统用户名";
            // 
            // cbxUSER
            // 
            this.cbxUSER.Location = new System.Drawing.Point(107, 16);
            this.cbxUSER.Name = "cbxUSER";
            this.cbxUSER.Size = new System.Drawing.Size(135, 23);
            this.cbxUSER.TabIndex = 341;
            // 
            // btnSave
            // 
            this.btnSave.Image = global::Hemo.Client.Properties.Resources.save;
            this.btnSave.Location = new System.Drawing.Point(248, 16);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 342;
            this.btnSave.Text = "保存(&S)";
            // 
            // gcAreaInfo
            // 
            this.gcAreaInfo.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gcAreaInfo.Location = new System.Drawing.Point(14, 45);
            this.gcAreaInfo.MainView = this.gvAreaInfo;
            this.gcAreaInfo.Name = "gcAreaInfo";
            this.gcAreaInfo.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemCheckEdit1,
            this.repositoryItemTextEdit1});
            this.gcAreaInfo.Size = new System.Drawing.Size(694, 375);
            this.gcAreaInfo.TabIndex = 343;
            this.gcAreaInfo.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvAreaInfo});
            // 
            // gvAreaInfo
            // 
            this.gvAreaInfo.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn8,
            this.gridColumn1});
            this.gvAreaInfo.GridControl = this.gcAreaInfo;
            this.gvAreaInfo.Name = "gvAreaInfo";
            this.gvAreaInfo.OptionsBehavior.Editable = false;
            this.gvAreaInfo.OptionsBehavior.EditorShowMode = DevExpress.Utils.EditorShowMode.Click;
            this.gvAreaInfo.OptionsView.ShowGroupPanel = false;
            // 
            // gridColumn8
            // 
            this.gridColumn8.Caption = "选择";
            this.gridColumn8.ColumnEdit = this.repositoryItemCheckEdit1;
            this.gridColumn8.FieldName = "CHOOSE";
            this.gridColumn8.Name = "gridColumn8";
            this.gridColumn8.Visible = true;
            this.gridColumn8.VisibleIndex = 0;
            this.gridColumn8.Width = 30;
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
            this.gridColumn1.Caption = "区域名称";
            this.gridColumn1.FieldName = "ITEM_NAME";
            this.gridColumn1.Name = "gridColumn1";
            this.gridColumn1.Visible = true;
            this.gridColumn1.VisibleIndex = 1;
            this.gridColumn1.Width = 643;
            // 
            // repositoryItemTextEdit1
            // 
            this.repositoryItemTextEdit1.AutoHeight = false;
            this.repositoryItemTextEdit1.Name = "repositoryItemTextEdit1";
            // 
            // SetUserAreaMappingFrm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.gcAreaInfo);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.cbxUSER);
            this.Controls.Add(this.lab6);
            this.Name = "SetUserAreaMappingFrm";
            this.Size = new System.Drawing.Size(711, 423);
            ((System.ComponentModel.ISupportInitialize)(this.gcAreaInfo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvAreaInfo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEdit1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.LabelControl lab6;
        private DevExpress.XtraEditors.DropDownButton cbxUSER;
        private DevExpress.XtraEditors.SimpleButton btnSave;
        private DevExpress.XtraGrid.GridControl gcAreaInfo;
        public DevExpress.XtraGrid.Views.Grid.GridView gvAreaInfo;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn8;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit repositoryItemCheckEdit1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit repositoryItemTextEdit1;
    }
}
