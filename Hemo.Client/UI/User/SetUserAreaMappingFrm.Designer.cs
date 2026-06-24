namespace Hemo.Client.UI.User
{
    partial class SetUserAreaMappingFrm
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
            this.cbxUSER = new DevExpress.XtraEditors.LookUpEdit();
            this.lab6 = new DevExpress.XtraEditors.LabelControl();
            this.gcAreaInfo = new DevExpress.XtraGrid.GridControl();
            this.gvAreaInfo = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn8 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemCheckEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemTextEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.btnSave = new Hemo.Client.Controls.DXSimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.cbxUSER.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcAreaInfo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvAreaInfo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEdit1)).BeginInit();
            this.SuspendLayout();
            // 
            // cbxUSER
            // 
            this.cbxUSER.EditValue = "";
            this.cbxUSER.Location = new System.Drawing.Point(81, 9);
            this.cbxUSER.Name = "cbxUSER";
            this.cbxUSER.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cbxUSER.Properties.LookAndFeel.SkinName = "ExtendBlue";
            this.cbxUSER.Size = new System.Drawing.Size(238, 21);
            this.cbxUSER.TabIndex = 340;
            this.cbxUSER.EditValueChanged += new System.EventHandler(this.cbxUSER_EditValueChanged);
            // 
            // lab6
            // 
            this.lab6.Location = new System.Drawing.Point(3, 12);
            this.lab6.Name = "lab6";
            this.lab6.Size = new System.Drawing.Size(72, 14);
            this.lab6.TabIndex = 339;
            this.lab6.Text = "本系统用户名";
            // 
            // gcAreaInfo
            // 
            this.gcAreaInfo.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gcAreaInfo.Location = new System.Drawing.Point(3, 36);
            this.gcAreaInfo.MainView = this.gvAreaInfo;
            this.gcAreaInfo.Name = "gcAreaInfo";
            this.gcAreaInfo.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemCheckEdit1,
            this.repositoryItemTextEdit1});
            this.gcAreaInfo.Size = new System.Drawing.Size(694, 361);
            this.gcAreaInfo.TabIndex = 341;
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
            this.gvAreaInfo.MouseDown += new System.Windows.Forms.MouseEventHandler(this.gvAreaInfo_MouseDown);
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
            // btnSave
            // 
            this.btnSave.ImageIndex = 7;
            this.btnSave.Location = new System.Drawing.Point(325, 7);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 342;
            this.btnSave.Text = "保存(&S)";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // SetUserAreaMappingFrm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.gcAreaInfo);
            this.Controls.Add(this.cbxUSER);
            this.Controls.Add(this.lab6);
            this.MinimumSize = new System.Drawing.Size(700, 400);
            this.Name = "SetUserAreaMappingFrm";
            this.Size = new System.Drawing.Size(700, 400);
            this.Load += new System.EventHandler(this.SetUserAreaMappingFrm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.cbxUSER.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcAreaInfo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvAreaInfo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEdit1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.LookUpEdit cbxUSER;
        private DevExpress.XtraEditors.LabelControl lab6;
        private DevExpress.XtraGrid.GridControl gcAreaInfo;
        public DevExpress.XtraGrid.Views.Grid.GridView gvAreaInfo;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn8;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit repositoryItemCheckEdit1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit repositoryItemTextEdit1;
        private Hemo.Client.Controls.DXSimpleButton btnSave;
    }
}