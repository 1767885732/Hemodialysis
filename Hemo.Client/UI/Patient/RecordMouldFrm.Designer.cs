namespace Hemo.Client.UI.Patient
{
    partial class RecordMouldFrm
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
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.treRecordMouldList = new DevExpress.XtraTreeList.TreeList();
            this.treeListColumn1 = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.treeListColumn2 = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.treeListColumn3 = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.treeListColumn4 = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.treeListColumn5 = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.panelControl2 = new DevExpress.XtraEditors.PanelControl();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.memoEdit_Fill = new DevExpress.XtraEditors.MemoEdit();
            this.memoEdit_Top = new DevExpress.XtraEditors.MemoEdit();
            this.memoEdit_bottom = new DevExpress.XtraEditors.MemoEdit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.treRecordMouldList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).BeginInit();
            this.panelControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.memoEdit_Fill.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.memoEdit_Top.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.memoEdit_bottom.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.treRecordMouldList);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelControl1.Location = new System.Drawing.Point(0, 0);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(190, 423);
            this.panelControl1.TabIndex = 0;
            // 
            // treRecordMouldList
            // 
            this.treRecordMouldList.Columns.AddRange(new DevExpress.XtraTreeList.Columns.TreeListColumn[] {
            this.treeListColumn1,
            this.treeListColumn2,
            this.treeListColumn3,
            this.treeListColumn4,
            this.treeListColumn5});
            this.treRecordMouldList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treRecordMouldList.Location = new System.Drawing.Point(2, 2);
            this.treRecordMouldList.Name = "treRecordMouldList";
            this.treRecordMouldList.OptionsBehavior.Editable = false;
            this.treRecordMouldList.Size = new System.Drawing.Size(186, 419);
            this.treRecordMouldList.TabIndex = 5;
            this.treRecordMouldList.FocusedNodeChanged += new DevExpress.XtraTreeList.FocusedNodeChangedEventHandler(this.treRecordMouldList_FocusedNodeChanged);
            this.treRecordMouldList.DockChanged += new System.EventHandler(this.treRecordMouldList_DockChanged);
            this.treRecordMouldList.DoubleClick += new System.EventHandler(this.treRecordMouldList_DoubleClick);
            // 
            // treeListColumn1
            // 
            this.treeListColumn1.Caption = "模板名称";
            this.treeListColumn1.FieldName = "NAME";
            this.treeListColumn1.Name = "treeListColumn1";
            this.treeListColumn1.Visible = true;
            this.treeListColumn1.VisibleIndex = 0;
            // 
            // treeListColumn2
            // 
            this.treeListColumn2.Caption = "treeListColumn2";
            this.treeListColumn2.FieldName = "ACTION";
            this.treeListColumn2.Name = "treeListColumn2";
            // 
            // treeListColumn3
            // 
            this.treeListColumn3.Caption = "treeListColumn3";
            this.treeListColumn3.FieldName = "PRESENTILLNESS";
            this.treeListColumn3.Name = "treeListColumn3";
            // 
            // treeListColumn4
            // 
            this.treeListColumn4.Caption = "treeListColumn4";
            this.treeListColumn4.FieldName = "PASTILLNESS";
            this.treeListColumn4.Name = "treeListColumn4";
            // 
            // treeListColumn5
            // 
            this.treeListColumn5.Caption = "treeListColumn5";
            this.treeListColumn5.FieldName = "ID";
            this.treeListColumn5.Name = "treeListColumn5";
            // 
            // panelControl2
            // 
            this.panelControl2.Controls.Add(this.labelControl3);
            this.panelControl2.Controls.Add(this.labelControl2);
            this.panelControl2.Controls.Add(this.labelControl1);
            this.panelControl2.Controls.Add(this.memoEdit_Fill);
            this.panelControl2.Controls.Add(this.memoEdit_Top);
            this.panelControl2.Controls.Add(this.memoEdit_bottom);
            this.panelControl2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelControl2.Location = new System.Drawing.Point(190, 0);
            this.panelControl2.Name = "panelControl2";
            this.panelControl2.Size = new System.Drawing.Size(671, 423);
            this.panelControl2.TabIndex = 1;
            // 
            // labelControl3
            // 
            this.labelControl3.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.labelControl3.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl3.Appearance.Options.UseBackColor = true;
            this.labelControl3.Appearance.Options.UseFont = true;
            this.labelControl3.Location = new System.Drawing.Point(6, 293);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(52, 14);
            this.labelControl3.TabIndex = 10;
            this.labelControl3.Text = "既往史：";
            // 
            // labelControl2
            // 
            this.labelControl2.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.labelControl2.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl2.Appearance.Options.UseBackColor = true;
            this.labelControl2.Appearance.Options.UseFont = true;
            this.labelControl2.Location = new System.Drawing.Point(6, 135);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(52, 14);
            this.labelControl2.TabIndex = 9;
            this.labelControl2.Text = "现病史：";
            // 
            // labelControl1
            // 
            this.labelControl1.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.labelControl1.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl1.Appearance.Options.UseBackColor = true;
            this.labelControl1.Appearance.Options.UseFont = true;
            this.labelControl1.Location = new System.Drawing.Point(7, 5);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(39, 14);
            this.labelControl1.TabIndex = 8;
            this.labelControl1.Text = "主诉：";
            // 
            // memoEdit_Fill
            // 
            this.memoEdit_Fill.Dock = System.Windows.Forms.DockStyle.Fill;
            this.memoEdit_Fill.EditValue = "";
            this.memoEdit_Fill.Location = new System.Drawing.Point(2, 132);
            this.memoEdit_Fill.Name = "memoEdit_Fill";
            this.memoEdit_Fill.Size = new System.Drawing.Size(667, 159);
            this.memoEdit_Fill.TabIndex = 6;
            // 
            // memoEdit_Top
            // 
            this.memoEdit_Top.Dock = System.Windows.Forms.DockStyle.Top;
            this.memoEdit_Top.EditValue = "";
            this.memoEdit_Top.Location = new System.Drawing.Point(2, 2);
            this.memoEdit_Top.Name = "memoEdit_Top";
            this.memoEdit_Top.Size = new System.Drawing.Size(667, 130);
            this.memoEdit_Top.TabIndex = 5;
            // 
            // memoEdit_bottom
            // 
            this.memoEdit_bottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.memoEdit_bottom.EditValue = "";
            this.memoEdit_bottom.Location = new System.Drawing.Point(2, 291);
            this.memoEdit_bottom.Name = "memoEdit_bottom";
            this.memoEdit_bottom.Size = new System.Drawing.Size(667, 130);
            this.memoEdit_bottom.TabIndex = 7;
            // 
            // RecordMouldFrm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(861, 423);
            this.Controls.Add(this.panelControl2);
            this.Controls.Add(this.panelControl1);
            this.Name = "RecordMouldFrm";
            this.ShowIcon = false;
            this.Text = "病历模版";
            this.Load += new System.EventHandler(this.RecordMouldFrm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.treRecordMouldList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).EndInit();
            this.panelControl2.ResumeLayout(false);
            this.panelControl2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.memoEdit_Fill.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.memoEdit_Top.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.memoEdit_bottom.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.PanelControl panelControl2;
        private DevExpress.XtraEditors.MemoEdit memoEdit_Fill;
        private DevExpress.XtraEditors.MemoEdit memoEdit_Top;
        private DevExpress.XtraEditors.MemoEdit memoEdit_bottom;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraTreeList.TreeList treRecordMouldList;
        private DevExpress.XtraTreeList.Columns.TreeListColumn treeListColumn1;
        private DevExpress.XtraTreeList.Columns.TreeListColumn treeListColumn2;
        private DevExpress.XtraTreeList.Columns.TreeListColumn treeListColumn3;
        private DevExpress.XtraTreeList.Columns.TreeListColumn treeListColumn4;
        private DevExpress.XtraTreeList.Columns.TreeListColumn treeListColumn5;
    }
}