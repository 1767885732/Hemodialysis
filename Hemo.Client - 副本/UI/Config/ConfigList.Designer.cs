namespace Hemo.Client.UI.Config
{
    partial class ConfigList
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
            this.gcConfig = new DevExpress.XtraGrid.GridControl();
            this.gvConfig = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn3 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn4 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn5 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.txtITEM_NAME = new DevExpress.XtraEditors.TextEdit();
            this.txtITEM_VALUE = new DevExpress.XtraEditors.TextEdit();
            this.lab2 = new DevExpress.XtraEditors.LabelControl();
            this.lab1 = new DevExpress.XtraEditors.LabelControl();
            this.lab4 = new DevExpress.XtraEditors.LabelControl();
            this.cbxSTATUS = new DevExpress.XtraEditors.ComboBoxEdit();
            this.btnQuery = new Hemo.Client.Controls.DXSimpleButton();
            this.btnAdd = new Hemo.Client.Controls.DXSimpleButton();
            this.btnEdit = new Hemo.Client.Controls.DXSimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.gcConfig)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvConfig)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtITEM_NAME.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtITEM_VALUE.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cbxSTATUS.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // gcConfig
            // 
            this.gcConfig.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.gcConfig.Location = new System.Drawing.Point(12, 36);
            this.gcConfig.MainView = this.gvConfig;
            this.gcConfig.Name = "gcConfig";
            this.gcConfig.Size = new System.Drawing.Size(771, 331);
            this.gcConfig.TabIndex = 5;
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
            this.gridColumn1.VisibleIndex = 0;
            // 
            // gridColumn2
            // 
            this.gridColumn2.Caption = "名称";
            this.gridColumn2.FieldName = "ITEM_NAME";
            this.gridColumn2.Name = "gridColumn2";
            this.gridColumn2.Visible = true;
            this.gridColumn2.VisibleIndex = 1;
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
            this.gridColumn4.Caption = "状态";
            this.gridColumn4.FieldName = "STATUSSTR";
            this.gridColumn4.Name = "gridColumn4";
            this.gridColumn4.Visible = true;
            this.gridColumn4.VisibleIndex = 3;
            // 
            // gridColumn5
            // 
            this.gridColumn5.Caption = "排序字段";
            this.gridColumn5.FieldName = "ORDER_NUMBER";
            this.gridColumn5.Name = "gridColumn5";
            this.gridColumn5.Visible = true;
            this.gridColumn5.VisibleIndex = 4;
            // 
            // txtITEM_NAME
            // 
            this.txtITEM_NAME.Location = new System.Drawing.Point(156, 8);
            this.txtITEM_NAME.Name = "txtITEM_NAME";
            this.txtITEM_NAME.Size = new System.Drawing.Size(86, 21);
            this.txtITEM_NAME.TabIndex = 23;
            // 
            // txtITEM_VALUE
            // 
            this.txtITEM_VALUE.Location = new System.Drawing.Point(34, 8);
            this.txtITEM_VALUE.Name = "txtITEM_VALUE";
            this.txtITEM_VALUE.Size = new System.Drawing.Size(86, 21);
            this.txtITEM_VALUE.TabIndex = 22;
            // 
            // lab2
            // 
            this.lab2.Location = new System.Drawing.Point(126, 11);
            this.lab2.Name = "lab2";
            this.lab2.Size = new System.Drawing.Size(24, 14);
            this.lab2.TabIndex = 25;
            this.lab2.Text = "名称";
            // 
            // lab1
            // 
            this.lab1.Location = new System.Drawing.Point(16, 11);
            this.lab1.Name = "lab1";
            this.lab1.Size = new System.Drawing.Size(12, 14);
            this.lab1.TabIndex = 24;
            this.lab1.Text = "值";
            // 
            // lab4
            // 
            this.lab4.Location = new System.Drawing.Point(248, 11);
            this.lab4.Name = "lab4";
            this.lab4.Size = new System.Drawing.Size(24, 14);
            this.lab4.TabIndex = 296;
            this.lab4.Text = "状态";
            // 
            // cbxSTATUS
            // 
            this.cbxSTATUS.EditValue = "";
            this.cbxSTATUS.Location = new System.Drawing.Point(278, 8);
            this.cbxSTATUS.Name = "cbxSTATUS";
            this.cbxSTATUS.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cbxSTATUS.Properties.Items.AddRange(new object[] {
            "停用",
            "启用"});
            this.cbxSTATUS.Properties.LookAndFeel.SkinName = "ExtendBlue";
            this.cbxSTATUS.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.cbxSTATUS.Size = new System.Drawing.Size(86, 21);
            this.cbxSTATUS.TabIndex = 297;
            // 
            // btnQuery
            // 
            this.btnQuery.ImageIndex = 8;
            this.btnQuery.Location = new System.Drawing.Point(370, 6);
            this.btnQuery.Name = "btnQuery";
            this.btnQuery.Size = new System.Drawing.Size(74, 25);
            this.btnQuery.TabIndex = 298;
            this.btnQuery.Text = "查询";
            this.btnQuery.Click += new System.EventHandler(this.btnQuery_Click);
            // 
            // btnAdd
            // 
            this.btnAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnAdd.ImageIndex = 0;
            this.btnAdd.Location = new System.Drawing.Point(12, 373);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(74, 25);
            this.btnAdd.TabIndex = 299;
            this.btnAdd.Text = "新增(&A)";
            this.btnAdd.Click += new System.EventHandler(this.btnOpt_Click);
            // 
            // btnEdit
            // 
            this.btnEdit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnEdit.Enabled = false;
            this.btnEdit.ImageIndex = 2;
            this.btnEdit.Location = new System.Drawing.Point(92, 373);
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Size = new System.Drawing.Size(74, 25);
            this.btnEdit.TabIndex = 300;
            this.btnEdit.Text = "编辑(E)";
            this.btnEdit.Click += new System.EventHandler(this.btnOpt_Click);
            // 
            // ConfigList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(795, 405);
            this.Controls.Add(this.btnEdit);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.btnQuery);
            this.Controls.Add(this.cbxSTATUS);
            this.Controls.Add(this.lab4);
            this.Controls.Add(this.txtITEM_NAME);
            this.Controls.Add(this.txtITEM_VALUE);
            this.Controls.Add(this.lab2);
            this.Controls.Add(this.lab1);
            this.Controls.Add(this.gcConfig);
            this.Name = "ConfigList";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Load += new System.EventHandler(this.ConfigList_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gcConfig)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvConfig)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtITEM_NAME.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtITEM_VALUE.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cbxSTATUS.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraGrid.GridControl gcConfig;
        public DevExpress.XtraGrid.Views.Grid.GridView gvConfig;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private DevExpress.XtraEditors.TextEdit txtITEM_NAME;
        private DevExpress.XtraEditors.TextEdit txtITEM_VALUE;
        private DevExpress.XtraEditors.LabelControl lab2;
        private DevExpress.XtraEditors.LabelControl lab1;
        private DevExpress.XtraEditors.LabelControl lab4;
        private DevExpress.XtraEditors.ComboBoxEdit cbxSTATUS;
        private Hemo.Client.Controls.DXSimpleButton btnQuery;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn2;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn3;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn4;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn5;
        private Hemo.Client.Controls.DXSimpleButton btnAdd;
        private Hemo.Client.Controls.DXSimpleButton btnEdit;
    }
}