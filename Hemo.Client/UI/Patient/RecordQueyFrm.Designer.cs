namespace Hemo.Client.UI.Patient {
    partial class RecordQueyFrm {
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.components = new System.ComponentModel.Container();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.btn_Print = new Hemo.Client.Controls.DXSimpleButton();
            this.btn_Query = new Hemo.Client.Controls.DXSimpleButton();
            this.endDataEdit = new DevExpress.XtraEditors.DateEdit();
            this.beginDataEdit = new DevExpress.XtraEditors.DateEdit();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.txtHemoID = new DevExpress.XtraEditors.TextEdit();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.lblReturn = new System.Windows.Forms.Label();
            this.xtraTabControl1 = new DevExpress.XtraTab.XtraTabControl();
            this.xtraTabPage1 = new DevExpress.XtraTab.XtraTabPage();
            this.gridControl1 = new DevExpress.XtraGrid.GridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn3 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn4 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn5 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.xtraTabPage2 = new DevExpress.XtraTab.XtraTabPage();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl5 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl6 = new DevExpress.XtraEditors.LabelControl();
            this.memoEdit_bottom = new DevExpress.XtraEditors.MemoEdit();
            this.memoEdit_action = new DevExpress.XtraEditors.MemoEdit();
            this.memoEdit_fill = new DevExpress.XtraEditors.MemoEdit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.endDataEdit.Properties.VistaTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.endDataEdit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.beginDataEdit.Properties.VistaTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.beginDataEdit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtHemoID.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.xtraTabControl1)).BeginInit();
            this.xtraTabControl1.SuspendLayout();
            this.xtraTabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            this.xtraTabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.memoEdit_bottom.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.memoEdit_action.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.memoEdit_fill.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.btn_Print);
            this.panelControl1.Controls.Add(this.btn_Query);
            this.panelControl1.Controls.Add(this.endDataEdit);
            this.panelControl1.Controls.Add(this.beginDataEdit);
            this.panelControl1.Controls.Add(this.labelControl3);
            this.panelControl1.Controls.Add(this.txtHemoID);
            this.panelControl1.Controls.Add(this.labelControl2);
            this.panelControl1.Controls.Add(this.labelControl1);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelControl1.Location = new System.Drawing.Point(0, 0);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(850, 45);
            this.panelControl1.TabIndex = 0;
            // 
            // btn_Print
            // 
            this.btn_Print.ImageIndex = 6;
            this.btn_Print.Location = new System.Drawing.Point(712, 12);
            this.btn_Print.Name = "btn_Print";
            this.btn_Print.Size = new System.Drawing.Size(88, 24);
            this.btn_Print.TabIndex = 3;
            this.btn_Print.Text = "打印(&P)";
            this.btn_Print.Click += new System.EventHandler(this.btn_Print_Click);
            // 
            // btn_Query
            // 
            this.btn_Query.ImageIndex = 8;
            this.btn_Query.Location = new System.Drawing.Point(594, 12);
            this.btn_Query.Name = "btn_Query";
            this.btn_Query.Size = new System.Drawing.Size(88, 24);
            this.btn_Query.TabIndex = 3;
            this.btn_Query.Text = "查询(&Q)";
            this.btn_Query.Click += new System.EventHandler(this.btn_Query_Click);
            // 
            // endDataEdit
            // 
            this.endDataEdit.EditValue = null;
            this.endDataEdit.Location = new System.Drawing.Point(437, 12);
            this.endDataEdit.Name = "endDataEdit";
            this.endDataEdit.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.endDataEdit.Properties.VistaTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.endDataEdit.Size = new System.Drawing.Size(100, 21);
            this.endDataEdit.TabIndex = 2;
            // 
            // beginDataEdit
            // 
            this.beginDataEdit.EditValue = null;
            this.beginDataEdit.Location = new System.Drawing.Point(262, 12);
            this.beginDataEdit.Name = "beginDataEdit";
            this.beginDataEdit.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.beginDataEdit.Properties.VistaTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.beginDataEdit.Size = new System.Drawing.Size(100, 21);
            this.beginDataEdit.TabIndex = 2;
            // 
            // labelControl3
            // 
            this.labelControl3.Location = new System.Drawing.Point(373, 15);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(56, 14);
            this.labelControl3.TabIndex = 0;
            this.labelControl3.Text = "结束时间 :";
            // 
            // txtHemoID
            // 
            this.txtHemoID.Location = new System.Drawing.Point(93, 12);
            this.txtHemoID.Name = "txtHemoID";
            this.txtHemoID.Size = new System.Drawing.Size(90, 21);
            this.txtHemoID.TabIndex = 1;
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(198, 15);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(56, 14);
            this.labelControl2.TabIndex = 0;
            this.labelControl2.Text = "开始时间 :";
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(9, 15);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(77, 14);
            this.labelControl1.TabIndex = 0;
            this.labelControl1.Text = "透析号/姓名：";
            // 
            // groupControl1
            // 
            this.groupControl1.Controls.Add(this.lblReturn);
            this.groupControl1.Controls.Add(this.xtraTabControl1);
            this.groupControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupControl1.Location = new System.Drawing.Point(0, 45);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Size = new System.Drawing.Size(850, 436);
            this.groupControl1.TabIndex = 1;
            this.groupControl1.Text = "病历内容";
            // 
            // lblReturn
            // 
            this.lblReturn.BackColor = System.Drawing.Color.Transparent;
            this.lblReturn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblReturn.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblReturn.ForeColor = System.Drawing.Color.Black;
            this.lblReturn.Location = new System.Drawing.Point(800, 1);
            this.lblReturn.Name = "lblReturn";
            this.lblReturn.Size = new System.Drawing.Size(44, 18);
            this.lblReturn.TabIndex = 11;
            this.lblReturn.Text = "返回";
            this.lblReturn.Visible = false;
            this.lblReturn.Click += new System.EventHandler(this.lblReturn_Click);
            this.lblReturn.MouseLeave += new System.EventHandler(this.lblReturn_MouseLeave);
            this.lblReturn.MouseMove += new System.Windows.Forms.MouseEventHandler(this.lblReturn_MouseMove);
            // 
            // xtraTabControl1
            // 
            this.xtraTabControl1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.xtraTabControl1.BorderStylePage = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.xtraTabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.xtraTabControl1.Location = new System.Drawing.Point(2, 23);
            this.xtraTabControl1.Name = "xtraTabControl1";
            this.xtraTabControl1.SelectedTabPage = this.xtraTabPage1;
            this.xtraTabControl1.ShowTabHeader = DevExpress.Utils.DefaultBoolean.False;
            this.xtraTabControl1.Size = new System.Drawing.Size(846, 411);
            this.xtraTabControl1.TabIndex = 0;
            this.xtraTabControl1.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
            this.xtraTabPage1,
            this.xtraTabPage2});
            // 
            // xtraTabPage1
            // 
            this.xtraTabPage1.Controls.Add(this.gridControl1);
            this.xtraTabPage1.Name = "xtraTabPage1";
            this.xtraTabPage1.Size = new System.Drawing.Size(839, 404);
            this.xtraTabPage1.Text = "xtraTabPage1";
            // 
            // gridControl1
            // 
            this.gridControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControl1.Location = new System.Drawing.Point(0, 0);
            this.gridControl1.MainView = this.gridView1;
            this.gridControl1.Name = "gridControl1";
            this.gridControl1.Size = new System.Drawing.Size(839, 404);
            this.gridControl1.TabIndex = 0;
            this.gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            // 
            // gridView1
            // 
            this.gridView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn1,
            this.gridColumn2,
            this.gridColumn3,
            this.gridColumn4,
            this.gridColumn5});
            this.gridView1.GridControl = this.gridControl1;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsBehavior.Editable = false;
            this.gridView1.OptionsSelection.MultiSelect = true;
            this.gridView1.OptionsView.ShowGroupPanel = false;
            this.gridView1.DoubleClick += new System.EventHandler(this.gridView1_DoubleClick);
            // 
            // gridColumn1
            // 
            this.gridColumn1.Caption = "患者姓名";
            this.gridColumn1.FieldName = "NAME";
            this.gridColumn1.Name = "gridColumn1";
            this.gridColumn1.Visible = true;
            this.gridColumn1.VisibleIndex = 0;
            // 
            // gridColumn2
            // 
            this.gridColumn2.Caption = "性别";
            this.gridColumn2.FieldName = "SEX";
            this.gridColumn2.Name = "gridColumn2";
            this.gridColumn2.Visible = true;
            this.gridColumn2.VisibleIndex = 1;
            // 
            // gridColumn3
            // 
            this.gridColumn3.Caption = "年龄";
            this.gridColumn3.FieldName = "AGE";
            this.gridColumn3.Name = "gridColumn3";
            this.gridColumn3.Visible = true;
            this.gridColumn3.VisibleIndex = 2;
            // 
            // gridColumn4
            // 
            this.gridColumn4.Caption = "病历日期";
            this.gridColumn4.FieldName = "CREATEDATE";
            this.gridColumn4.Name = "gridColumn4";
            this.gridColumn4.Visible = true;
            this.gridColumn4.VisibleIndex = 3;
            // 
            // gridColumn5
            // 
            this.gridColumn5.Caption = "创建人";
            this.gridColumn5.FieldName = "CREATEBY";
            this.gridColumn5.Name = "gridColumn5";
            this.gridColumn5.Visible = true;
            this.gridColumn5.VisibleIndex = 4;
            // 
            // xtraTabPage2
            // 
            this.xtraTabPage2.Controls.Add(this.labelControl4);
            this.xtraTabPage2.Controls.Add(this.labelControl5);
            this.xtraTabPage2.Controls.Add(this.labelControl6);
            this.xtraTabPage2.Controls.Add(this.memoEdit_bottom);
            this.xtraTabPage2.Controls.Add(this.memoEdit_action);
            this.xtraTabPage2.Controls.Add(this.memoEdit_fill);
            this.xtraTabPage2.Name = "xtraTabPage2";
            this.xtraTabPage2.Size = new System.Drawing.Size(839, 404);
            this.xtraTabPage2.Text = "xtraTabPage2";
            // 
            // labelControl4
            // 
            this.labelControl4.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.labelControl4.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl4.Appearance.Options.UseBackColor = true;
            this.labelControl4.Appearance.Options.UseFont = true;
            this.labelControl4.Location = new System.Drawing.Point(4, 302);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(52, 14);
            this.labelControl4.TabIndex = 8;
            this.labelControl4.Text = "既往史：";
            // 
            // labelControl5
            // 
            this.labelControl5.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.labelControl5.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl5.Appearance.Options.UseBackColor = true;
            this.labelControl5.Appearance.Options.UseFont = true;
            this.labelControl5.Location = new System.Drawing.Point(4, 156);
            this.labelControl5.Name = "labelControl5";
            this.labelControl5.Size = new System.Drawing.Size(52, 14);
            this.labelControl5.TabIndex = 7;
            this.labelControl5.Text = "现病史：";
            // 
            // labelControl6
            // 
            this.labelControl6.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.labelControl6.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl6.Appearance.Options.UseBackColor = true;
            this.labelControl6.Appearance.Options.UseFont = true;
            this.labelControl6.Location = new System.Drawing.Point(3, 2);
            this.labelControl6.Name = "labelControl6";
            this.labelControl6.Size = new System.Drawing.Size(39, 14);
            this.labelControl6.TabIndex = 6;
            this.labelControl6.Text = "主诉：";
            // 
            // memoEdit_bottom
            // 
            this.memoEdit_bottom.Dock = System.Windows.Forms.DockStyle.Fill;
            this.memoEdit_bottom.EditValue = "";
            this.memoEdit_bottom.Location = new System.Drawing.Point(0, 154);
            this.memoEdit_bottom.Name = "memoEdit_bottom";
            this.memoEdit_bottom.Size = new System.Drawing.Size(839, 146);
            this.memoEdit_bottom.TabIndex = 2;
            // 
            // memoEdit_action
            // 
            this.memoEdit_action.Dock = System.Windows.Forms.DockStyle.Top;
            this.memoEdit_action.EditValue = "";
            this.memoEdit_action.Location = new System.Drawing.Point(0, 0);
            this.memoEdit_action.Name = "memoEdit_action";
            this.memoEdit_action.Size = new System.Drawing.Size(839, 154);
            this.memoEdit_action.TabIndex = 0;
            // 
            // memoEdit_fill
            // 
            this.memoEdit_fill.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.memoEdit_fill.EditValue = "";
            this.memoEdit_fill.Location = new System.Drawing.Point(0, 300);
            this.memoEdit_fill.Name = "memoEdit_fill";
            this.memoEdit_fill.Size = new System.Drawing.Size(839, 104);
            this.memoEdit_fill.TabIndex = 1;
            // 
            // RecordQueyFrm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(850, 481);
            this.Controls.Add(this.groupControl1);
            this.Controls.Add(this.panelControl1);
            this.Name = "RecordQueyFrm";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "病历查询";
            this.Load += new System.EventHandler(this.RecordQueyFrm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.panelControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.endDataEdit.Properties.VistaTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.endDataEdit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.beginDataEdit.Properties.VistaTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.beginDataEdit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtHemoID.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.xtraTabControl1)).EndInit();
            this.xtraTabControl1.ResumeLayout(false);
            this.xtraTabPage1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            this.xtraTabPage2.ResumeLayout(false);
            this.xtraTabPage2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.memoEdit_bottom.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.memoEdit_action.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.memoEdit_fill.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.GroupControl groupControl1;
        private DevExpress.XtraTab.XtraTabControl xtraTabControl1;
        private DevExpress.XtraTab.XtraTabPage xtraTabPage1;
        private DevExpress.XtraGrid.GridControl gridControl1;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraTab.XtraTabPage xtraTabPage2;
        private DevExpress.XtraEditors.MemoEdit memoEdit_action;
        private DevExpress.XtraEditors.TextEdit txtHemoID;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private Hemo.Client.Controls.DXSimpleButton btn_Query;
        private DevExpress.XtraEditors.DateEdit endDataEdit;
        private DevExpress.XtraEditors.DateEdit beginDataEdit;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn2;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn3;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn4;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn5;
        private System.Windows.Forms.Label lblReturn;
        private DevExpress.XtraEditors.MemoEdit memoEdit_bottom;
        private DevExpress.XtraEditors.MemoEdit memoEdit_fill;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private DevExpress.XtraEditors.LabelControl labelControl5;
        private DevExpress.XtraEditors.LabelControl labelControl6;
        private Hemo.Client.Controls.DXSimpleButton btn_Print;
    }
}