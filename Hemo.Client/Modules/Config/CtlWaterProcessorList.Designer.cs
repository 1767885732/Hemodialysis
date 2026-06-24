namespace Hemo.Client.Modules.Config
{
    partial class CtlWaterProcessorList
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
            this.gcMachine = new DevExpress.XtraGrid.GridControl();
            this.gvMachine = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn3 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn12 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn13 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn14 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.pcTools = new DevExpress.XtraEditors.PanelControl();
            this.btnEdit = new DevExpress.XtraEditors.SimpleButton();
            this.btnAdd = new DevExpress.XtraEditors.SimpleButton();
            this.btnRefresh = new DevExpress.XtraEditors.SimpleButton();
            this.chkFilter = new DevExpress.XtraEditors.CheckEdit();
            ((System.ComponentModel.ISupportInitialize)(this.gcMachine)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvMachine)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pcTools)).BeginInit();
            this.pcTools.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chkFilter.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // gcMachine
            // 
            this.gcMachine.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gcMachine.Location = new System.Drawing.Point(0, 0);
            this.gcMachine.MainView = this.gvMachine;
            this.gcMachine.Name = "gcMachine";
            this.gcMachine.Size = new System.Drawing.Size(863, 422);
            this.gcMachine.TabIndex = 315;
            this.gcMachine.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvMachine});
            // 
            // gvMachine
            // 
            this.gvMachine.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn3,
            this.gridColumn12,
            this.gridColumn13,
            this.gridColumn14,
            this.gridColumn1,
            this.gridColumn2});
            this.gvMachine.GridControl = this.gcMachine;
            this.gvMachine.Name = "gvMachine";
            this.gvMachine.OptionsBehavior.Editable = false;
            this.gvMachine.OptionsBehavior.EditorShowMode = DevExpress.Utils.EditorShowMode.Click;
            this.gvMachine.OptionsView.ShowGroupPanel = false;
            // 
            // gridColumn3
            // 
            this.gridColumn3.Caption = "编号";
            this.gridColumn3.FieldName = "MACHINE_NAME";
            this.gridColumn3.Name = "gridColumn3";
            this.gridColumn3.Visible = true;
            this.gridColumn3.VisibleIndex = 0;
            // 
            // gridColumn12
            // 
            this.gridColumn12.Caption = "分类";
            this.gridColumn12.FieldName = "FLNAME";
            this.gridColumn12.Name = "gridColumn12";
            this.gridColumn12.Visible = true;
            this.gridColumn12.VisibleIndex = 1;
            // 
            // gridColumn13
            // 
            this.gridColumn13.Caption = "型号";
            this.gridColumn13.FieldName = "MACHINE_MODEL";
            this.gridColumn13.Name = "gridColumn13";
            this.gridColumn13.Visible = true;
            this.gridColumn13.VisibleIndex = 2;
            // 
            // gridColumn14
            // 
            this.gridColumn14.Caption = "供应商";
            this.gridColumn14.FieldName = "SUPPLIER";
            this.gridColumn14.Name = "gridColumn14";
            this.gridColumn14.Visible = true;
            this.gridColumn14.VisibleIndex = 3;
            // 
            // gridColumn1
            // 
            this.gridColumn1.Caption = "治疗特征";
            this.gridColumn1.FieldName = "THERAPEUTIC_PROPERTIES";
            this.gridColumn1.Name = "gridColumn1";
            this.gridColumn1.Visible = true;
            this.gridColumn1.VisibleIndex = 4;
            // 
            // gridColumn2
            // 
            this.gridColumn2.Caption = "采集绑定标示";
            this.gridColumn2.FieldName = "OTHER_THERAPEUTIC";
            this.gridColumn2.Name = "gridColumn2";
            this.gridColumn2.Visible = true;
            this.gridColumn2.VisibleIndex = 5;
            // 
            // pcTools
            // 
            this.pcTools.Controls.Add(this.btnEdit);
            this.pcTools.Controls.Add(this.btnAdd);
            this.pcTools.Controls.Add(this.btnRefresh);
            this.pcTools.Controls.Add(this.chkFilter);
            this.pcTools.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pcTools.Location = new System.Drawing.Point(0, 462);
            this.pcTools.Name = "pcTools";
            this.pcTools.Size = new System.Drawing.Size(863, 33);
            this.pcTools.TabIndex = 321;
            // 
            // btnEdit
            // 
            this.btnEdit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnEdit.Image = global::Hemo.Client.Properties.Resources.edit;
            this.btnEdit.Location = new System.Drawing.Point(671, 7);
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Size = new System.Drawing.Size(75, 23);
            this.btnEdit.TabIndex = 377;
            this.btnEdit.Text = "编辑(&E)";
            // 
            // btnAdd
            // 
            this.btnAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAdd.Image = global::Hemo.Client.Properties.Resources.add;
            this.btnAdd.Location = new System.Drawing.Point(590, 7);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(75, 23);
            this.btnAdd.TabIndex = 376;
            this.btnAdd.Text = "新增(&A)";
            // 
            // btnRefresh
            // 
            this.btnRefresh.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRefresh.Image = global::Hemo.Client.Properties.Resources.search_16;
            this.btnRefresh.Location = new System.Drawing.Point(752, 7);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(75, 23);
            this.btnRefresh.TabIndex = 375;
            this.btnRefresh.Text = "查询(&Q)";
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
            // CtlWaterProcessorList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pcTools);
            this.Controls.Add(this.gcMachine);
            this.Name = "CtlWaterProcessorList";
            this.Size = new System.Drawing.Size(863, 495);
            ((System.ComponentModel.ISupportInitialize)(this.gcMachine)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvMachine)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pcTools)).EndInit();
            this.pcTools.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.chkFilter.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraGrid.GridControl gcMachine;
        public DevExpress.XtraGrid.Views.Grid.GridView gvMachine;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn3;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn12;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn13;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn14;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn2;
        private DevExpress.XtraEditors.PanelControl pcTools;
        private DevExpress.XtraEditors.SimpleButton btnEdit;
        private DevExpress.XtraEditors.SimpleButton btnAdd;
        private DevExpress.XtraEditors.SimpleButton btnRefresh;
        private DevExpress.XtraEditors.CheckEdit chkFilter;
    }
}
