namespace Hemo.Client.Modules.Config
{
    partial class CtlQueryDrugMaster
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
            this.pnlControls = new DevExpress.XtraEditors.PanelControl();
            this.txtDRUG_CODE = new DevExpress.XtraEditors.TextEdit();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.txtFIRM_ID = new DevExpress.XtraEditors.TextEdit();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.txtDRUG_NAME = new DevExpress.XtraEditors.TextEdit();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.gridDrugMaster = new DevExpress.XtraGrid.GridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn3 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn4 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn5 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn6 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn7 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn8 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn9 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.pcTools = new DevExpress.XtraEditors.PanelControl();
            this.btnEdit = new DevExpress.XtraEditors.SimpleButton();
            this.btnAdd = new DevExpress.XtraEditors.SimpleButton();
            this.chkFilter = new DevExpress.XtraEditors.CheckEdit();
            ((System.ComponentModel.ISupportInitialize)(this.pnlControls)).BeginInit();
            this.pnlControls.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtDRUG_CODE.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtFIRM_ID.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDRUG_NAME.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridDrugMaster)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pcTools)).BeginInit();
            this.pcTools.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chkFilter.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlControls
            // 
            this.pnlControls.Controls.Add(this.txtDRUG_CODE);
            this.pnlControls.Controls.Add(this.labelControl3);
            this.pnlControls.Controls.Add(this.txtFIRM_ID);
            this.pnlControls.Controls.Add(this.labelControl2);
            this.pnlControls.Controls.Add(this.txtDRUG_NAME);
            this.pnlControls.Controls.Add(this.labelControl1);
            this.pnlControls.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlControls.Location = new System.Drawing.Point(0, 0);
            this.pnlControls.Name = "pnlControls";
            this.pnlControls.Size = new System.Drawing.Size(1033, 10);
            this.pnlControls.TabIndex = 3;
            // 
            // txtDRUG_CODE
            // 
            this.txtDRUG_CODE.Location = new System.Drawing.Point(439, 18);
            this.txtDRUG_CODE.Name = "txtDRUG_CODE";
            this.txtDRUG_CODE.Size = new System.Drawing.Size(130, 20);
            this.txtDRUG_CODE.TabIndex = 3;
            // 
            // labelControl3
            // 
            this.labelControl3.Location = new System.Drawing.Point(385, 21);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(48, 14);
            this.labelControl3.TabIndex = 12;
            this.labelControl3.Text = "药品编号";
            // 
            // txtFIRM_ID
            // 
            this.txtFIRM_ID.Location = new System.Drawing.Point(249, 18);
            this.txtFIRM_ID.Name = "txtFIRM_ID";
            this.txtFIRM_ID.Size = new System.Drawing.Size(122, 20);
            this.txtFIRM_ID.TabIndex = 2;
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(195, 21);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(48, 14);
            this.labelControl2.TabIndex = 10;
            this.labelControl2.Text = "厂商编号";
            // 
            // txtDRUG_NAME
            // 
            this.txtDRUG_NAME.Location = new System.Drawing.Point(70, 18);
            this.txtDRUG_NAME.Name = "txtDRUG_NAME";
            this.txtDRUG_NAME.Size = new System.Drawing.Size(117, 20);
            this.txtDRUG_NAME.TabIndex = 1;
            this.txtDRUG_NAME.ToolTip = "可输入药品名或拼音码";
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(16, 21);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(48, 14);
            this.labelControl1.TabIndex = 8;
            this.labelControl1.Text = "药品名称";
            // 
            // gridDrugMaster
            // 
            this.gridDrugMaster.Dock = System.Windows.Forms.DockStyle.Top;
            this.gridDrugMaster.Location = new System.Drawing.Point(0, 10);
            this.gridDrugMaster.MainView = this.gridView1;
            this.gridDrugMaster.Name = "gridDrugMaster";
            this.gridDrugMaster.Size = new System.Drawing.Size(1033, 415);
            this.gridDrugMaster.TabIndex = 4;
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
            // 
            // gridColumn1
            // 
            this.gridColumn1.Caption = "药品编号";
            this.gridColumn1.FieldName = "DRUG_CODE";
            this.gridColumn1.Name = "gridColumn1";
            this.gridColumn1.Visible = true;
            this.gridColumn1.VisibleIndex = 1;
            this.gridColumn1.Width = 153;
            // 
            // gridColumn2
            // 
            this.gridColumn2.Caption = "药品名称";
            this.gridColumn2.FieldName = "DRUG_NAME";
            this.gridColumn2.Name = "gridColumn2";
            this.gridColumn2.Visible = true;
            this.gridColumn2.VisibleIndex = 2;
            this.gridColumn2.Width = 153;
            // 
            // gridColumn3
            // 
            this.gridColumn3.Caption = "药品分类";
            this.gridColumn3.FieldName = "DRUG_TYPE";
            this.gridColumn3.Name = "gridColumn3";
            this.gridColumn3.Visible = true;
            this.gridColumn3.VisibleIndex = 0;
            this.gridColumn3.Width = 102;
            // 
            // gridColumn4
            // 
            this.gridColumn4.Caption = "药品厂商编号";
            this.gridColumn4.FieldName = "FIRM_ID";
            this.gridColumn4.Name = "gridColumn4";
            this.gridColumn4.Visible = true;
            this.gridColumn4.VisibleIndex = 3;
            this.gridColumn4.Width = 153;
            // 
            // gridColumn5
            // 
            this.gridColumn5.Caption = "药品厂商名称";
            this.gridColumn5.FieldName = "FIRM_NAME";
            this.gridColumn5.Name = "gridColumn5";
            this.gridColumn5.Visible = true;
            this.gridColumn5.VisibleIndex = 4;
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
            this.gridColumn8.FieldName = "UNITS";
            this.gridColumn8.Name = "gridColumn8";
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
            // pcTools
            // 
            this.pcTools.Controls.Add(this.btnEdit);
            this.pcTools.Controls.Add(this.btnAdd);
            this.pcTools.Controls.Add(this.chkFilter);
            this.pcTools.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pcTools.Location = new System.Drawing.Point(0, 424);
            this.pcTools.Name = "pcTools";
            this.pcTools.Size = new System.Drawing.Size(1033, 33);
            this.pcTools.TabIndex = 321;
            // 
            // btnEdit
            // 
            this.btnEdit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnEdit.Image = global::Hemo.Client.Properties.Resources.edit;
            this.btnEdit.Location = new System.Drawing.Point(935, 7);
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Size = new System.Drawing.Size(75, 23);
            this.btnEdit.TabIndex = 377;
            this.btnEdit.Text = "编辑(&E)";
            // 
            // btnAdd
            // 
            this.btnAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAdd.Image = global::Hemo.Client.Properties.Resources.add;
            this.btnAdd.Location = new System.Drawing.Point(854, 7);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(75, 23);
            this.btnAdd.TabIndex = 376;
            this.btnAdd.Text = "新增(&A)";
            // 
            // chkFilter
            // 
            this.chkFilter.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.chkFilter.Location = new System.Drawing.Point(5, 7);
            this.chkFilter.Name = "chkFilter";
            this.chkFilter.Properties.Caption = "显示过虑行";
            this.chkFilter.Size = new System.Drawing.Size(90, 19);
            this.chkFilter.TabIndex = 311;
            // 
            // CtlQueryDrugMaster
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pcTools);
            this.Controls.Add(this.gridDrugMaster);
            this.Controls.Add(this.pnlControls);
            this.Name = "CtlQueryDrugMaster";
            this.Size = new System.Drawing.Size(1033, 457);
            ((System.ComponentModel.ISupportInitialize)(this.pnlControls)).EndInit();
            this.pnlControls.ResumeLayout(false);
            this.pnlControls.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtDRUG_CODE.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtFIRM_ID.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDRUG_NAME.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridDrugMaster)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pcTools)).EndInit();
            this.pcTools.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.chkFilter.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl pnlControls;
        private DevExpress.XtraEditors.TextEdit txtDRUG_CODE;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.TextEdit txtFIRM_ID;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.TextEdit txtDRUG_NAME;
        private DevExpress.XtraEditors.LabelControl labelControl1;
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
        private DevExpress.XtraEditors.SimpleButton btnEdit;
        private DevExpress.XtraEditors.SimpleButton btnAdd;
        private DevExpress.XtraEditors.CheckEdit chkFilter;

    }
}
