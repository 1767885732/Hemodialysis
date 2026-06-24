namespace Hemo.Client.UI.Patient
{
    partial class PatientRecordNew
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
            this.components = new System.ComponentModel.Container();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.ctlMedicalUserInfo = new Hemo.Client.Controls.CtlMedicalUserInfo();
            this.panelControl2 = new DevExpress.XtraEditors.PanelControl();
            this.btnQuery = new Hemo.Client.Controls.DXSimpleButton();
            this.txtToDate = new DevExpress.XtraEditors.DateEdit();
            this.lblToDate = new DevExpress.XtraEditors.LabelControl();
            this.txtFromDate = new DevExpress.XtraEditors.DateEdit();
            this.lblFromDate = new DevExpress.XtraEditors.LabelControl();
            this.panelControl3 = new DevExpress.XtraEditors.PanelControl();
            this.gcRecord = new DevExpress.XtraGrid.GridControl();
            this.gvRecord = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colHemoId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCreateDate = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colWEIGHT = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colSicklyLook = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colIMX = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colHCV = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colTPPA = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colAIDS = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCreateBy = new DevExpress.XtraGrid.Columns.GridColumn();
            this.panelControl4 = new DevExpress.XtraEditors.PanelControl();
            this.btnExit = new Hemo.Client.Controls.DXSimpleButton();
            this.btnDrug = new Hemo.Client.Controls.DXSimpleButton();
            this.btnAdd = new Hemo.Client.Controls.DXSimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).BeginInit();
            this.panelControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtToDate.Properties.VistaTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtToDate.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtFromDate.Properties.VistaTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtFromDate.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl3)).BeginInit();
            this.panelControl3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gcRecord)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvRecord)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl4)).BeginInit();
            this.panelControl4.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.ctlMedicalUserInfo);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelControl1.Location = new System.Drawing.Point(0, 0);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(764, 145);
            this.panelControl1.TabIndex = 0;
            // 
            // ctlMedicalUserInfo
            // 
            this.ctlMedicalUserInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ctlMedicalUserInfo.HemoId = "";
            this.ctlMedicalUserInfo.Location = new System.Drawing.Point(2, 2);
            this.ctlMedicalUserInfo.Name = "ctlMedicalUserInfo";
            this.ctlMedicalUserInfo.Size = new System.Drawing.Size(760, 141);
            this.ctlMedicalUserInfo.TabIndex = 0;
            // 
            // panelControl2
            // 
            this.panelControl2.Controls.Add(this.btnQuery);
            this.panelControl2.Controls.Add(this.txtToDate);
            this.panelControl2.Controls.Add(this.lblToDate);
            this.panelControl2.Controls.Add(this.txtFromDate);
            this.panelControl2.Controls.Add(this.lblFromDate);
            this.panelControl2.Location = new System.Drawing.Point(2, 146);
            this.panelControl2.Name = "panelControl2";
            this.panelControl2.Size = new System.Drawing.Size(762, 27);
            this.panelControl2.TabIndex = 1;
            // 
            // btnQuery
            // 
            this.btnQuery.ImageIndex = 8;
            this.btnQuery.Location = new System.Drawing.Point(349, 2);
            this.btnQuery.Name = "btnQuery";
            this.btnQuery.Size = new System.Drawing.Size(75, 23);
            this.btnQuery.TabIndex = 313;
            this.btnQuery.Text = "查询";
            this.btnQuery.Click += new System.EventHandler(this.btnQuery_Click);
            // 
            // txtToDate
            // 
            this.txtToDate.EditValue = null;
            this.txtToDate.Location = new System.Drawing.Point(233, 2);
            this.txtToDate.Name = "txtToDate";
            this.txtToDate.Properties.Appearance.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtToDate.Properties.Appearance.ForeColor = System.Drawing.Color.Blue;
            this.txtToDate.Properties.Appearance.Options.UseFont = true;
            this.txtToDate.Properties.Appearance.Options.UseForeColor = true;
            this.txtToDate.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.txtToDate.Properties.LookAndFeel.SkinName = "ExtendBlue";
            this.txtToDate.Properties.VistaTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.txtToDate.Size = new System.Drawing.Size(100, 23);
            this.txtToDate.TabIndex = 311;
            // 
            // lblToDate
            // 
            this.lblToDate.Appearance.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblToDate.Appearance.Options.UseFont = true;
            this.lblToDate.Location = new System.Drawing.Point(179, 5);
            this.lblToDate.Name = "lblToDate";
            this.lblToDate.Size = new System.Drawing.Size(48, 17);
            this.lblToDate.TabIndex = 312;
            this.lblToDate.Text = "结束日期";
            // 
            // txtFromDate
            // 
            this.txtFromDate.EditValue = null;
            this.txtFromDate.Location = new System.Drawing.Point(64, 2);
            this.txtFromDate.Name = "txtFromDate";
            this.txtFromDate.Properties.Appearance.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtFromDate.Properties.Appearance.ForeColor = System.Drawing.Color.Blue;
            this.txtFromDate.Properties.Appearance.Options.UseFont = true;
            this.txtFromDate.Properties.Appearance.Options.UseForeColor = true;
            this.txtFromDate.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.txtFromDate.Properties.LookAndFeel.SkinName = "ExtendBlue";
            this.txtFromDate.Properties.VistaTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.txtFromDate.Size = new System.Drawing.Size(100, 23);
            this.txtFromDate.TabIndex = 309;
            // 
            // lblFromDate
            // 
            this.lblFromDate.Appearance.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFromDate.Appearance.Options.UseFont = true;
            this.lblFromDate.Location = new System.Drawing.Point(10, 5);
            this.lblFromDate.Name = "lblFromDate";
            this.lblFromDate.Size = new System.Drawing.Size(48, 17);
            this.lblFromDate.TabIndex = 310;
            this.lblFromDate.Text = "开始日期";
            // 
            // panelControl3
            // 
            this.panelControl3.Controls.Add(this.gcRecord);
            this.panelControl3.Location = new System.Drawing.Point(2, 174);
            this.panelControl3.Name = "panelControl3";
            this.panelControl3.Size = new System.Drawing.Size(762, 318);
            this.panelControl3.TabIndex = 2;
            // 
            // gcRecord
            // 
            this.gcRecord.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gcRecord.Location = new System.Drawing.Point(2, 2);
            this.gcRecord.MainView = this.gvRecord;
            this.gcRecord.Name = "gcRecord";
            this.gcRecord.Size = new System.Drawing.Size(758, 314);
            this.gcRecord.TabIndex = 0;
            this.gcRecord.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvRecord});
            this.gcRecord.DoubleClick += new System.EventHandler(this.gcRecord_DoubleClick);
            // 
            // gvRecord
            // 
            this.gvRecord.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colHemoId,
            this.colCreateDate,
            this.colWEIGHT,
            this.colSicklyLook,
            this.colIMX,
            this.colHCV,
            this.colTPPA,
            this.colAIDS,
            this.colCreateBy});
            this.gvRecord.GridControl = this.gcRecord;
            this.gvRecord.Name = "gvRecord";
            this.gvRecord.OptionsBehavior.Editable = false;
            this.gvRecord.OptionsBehavior.ReadOnly = true;
            this.gvRecord.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gvRecord.OptionsView.ShowGroupPanel = false;
            // 
            // colHemoId
            // 
            this.colHemoId.Caption = "透析编号";
            this.colHemoId.FieldName = "HEMODIALYSIS_ID";
            this.colHemoId.MaxWidth = 80;
            this.colHemoId.Name = "colHemoId";
            this.colHemoId.Visible = true;
            this.colHemoId.VisibleIndex = 0;
            this.colHemoId.Width = 79;
            // 
            // colCreateDate
            // 
            this.colCreateDate.Caption = "录入日期";
            this.colCreateDate.FieldName = "CREATEDATE";
            this.colCreateDate.MaxWidth = 80;
            this.colCreateDate.Name = "colCreateDate";
            this.colCreateDate.Visible = true;
            this.colCreateDate.VisibleIndex = 1;
            this.colCreateDate.Width = 79;
            // 
            // colWEIGHT
            // 
            this.colWEIGHT.Caption = "体重(单位kg)";
            this.colWEIGHT.FieldName = "WEIGHT";
            this.colWEIGHT.MaxWidth = 95;
            this.colWEIGHT.Name = "colWEIGHT";
            this.colWEIGHT.Visible = true;
            this.colWEIGHT.VisibleIndex = 2;
            this.colWEIGHT.Width = 94;
            // 
            // colSicklyLook
            // 
            this.colSicklyLook.Caption = "病容";
            this.colSicklyLook.FieldName = "SICKLY_LOOKNAME";
            this.colSicklyLook.MaxWidth = 60;
            this.colSicklyLook.Name = "colSicklyLook";
            this.colSicklyLook.Visible = true;
            this.colSicklyLook.VisibleIndex = 3;
            this.colSicklyLook.Width = 59;
            // 
            // colIMX
            // 
            this.colIMX.Caption = "乙肝两对半";
            this.colIMX.FieldName = "IMXNAME";
            this.colIMX.MaxWidth = 85;
            this.colIMX.Name = "colIMX";
            this.colIMX.Visible = true;
            this.colIMX.VisibleIndex = 4;
            this.colIMX.Width = 84;
            // 
            // colHCV
            // 
            this.colHCV.Caption = "丙肝抗体";
            this.colHCV.FieldName = "HCVNAME";
            this.colHCV.MaxWidth = 80;
            this.colHCV.Name = "colHCV";
            this.colHCV.Visible = true;
            this.colHCV.VisibleIndex = 5;
            this.colHCV.Width = 69;
            // 
            // colTPPA
            // 
            this.colTPPA.Caption = "梅毒螺旋体抗体";
            this.colTPPA.FieldName = "TPPANAME";
            this.colTPPA.Name = "colTPPA";
            this.colTPPA.Visible = true;
            this.colTPPA.VisibleIndex = 6;
            this.colTPPA.Width = 108;
            // 
            // colAIDS
            // 
            this.colAIDS.Caption = "爱滋病抗体";
            this.colAIDS.FieldName = "AIDSNAME";
            this.colAIDS.MaxWidth = 90;
            this.colAIDS.Name = "colAIDS";
            this.colAIDS.Visible = true;
            this.colAIDS.VisibleIndex = 7;
            this.colAIDS.Width = 85;
            // 
            // colCreateBy
            // 
            this.colCreateBy.Caption = "创建人";
            this.colCreateBy.FieldName = "CREATEBY";
            this.colCreateBy.MaxWidth = 75;
            this.colCreateBy.Name = "colCreateBy";
            this.colCreateBy.Visible = true;
            this.colCreateBy.VisibleIndex = 8;
            // 
            // panelControl4
            // 
            this.panelControl4.Controls.Add(this.btnExit);
            this.panelControl4.Controls.Add(this.btnDrug);
            this.panelControl4.Controls.Add(this.btnAdd);
            this.panelControl4.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelControl4.Location = new System.Drawing.Point(0, 482);
            this.panelControl4.Name = "panelControl4";
            this.panelControl4.Size = new System.Drawing.Size(764, 40);
            this.panelControl4.TabIndex = 3;
            // 
            // btnExit
            // 
            this.btnExit.ImageIndex = 3;
            this.btnExit.Location = new System.Drawing.Point(673, 8);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(75, 25);
            this.btnExit.TabIndex = 301;
            this.btnExit.Text = "关闭(&Q)";
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // btnDrug
            // 
            this.btnDrug.ImageIndex = 4;
            this.btnDrug.Location = new System.Drawing.Point(567, 8);
            this.btnDrug.Name = "btnDrug";
            this.btnDrug.Size = new System.Drawing.Size(100, 25);
            this.btnDrug.TabIndex = 300;
            this.btnDrug.Text = "药品医嘱(&D)";
            this.btnDrug.Click += new System.EventHandler(this.btnDrug_Click);
            // 
            // btnAdd
            // 
            this.btnAdd.ImageIndex = 0;
            this.btnAdd.Location = new System.Drawing.Point(463, 8);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(100, 25);
            this.btnAdd.TabIndex = 299;
            this.btnAdd.Text = "新增病历(&A)";
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // PatientRecordNew
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(764, 522);
            this.Controls.Add(this.panelControl4);
            this.Controls.Add(this.panelControl3);
            this.Controls.Add(this.panelControl2);
            this.Controls.Add(this.panelControl1);
            this.Name = "PatientRecordNew";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "病人病历";
            this.Load += new System.EventHandler(this.PatientRecordNew_Load);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).EndInit();
            this.panelControl2.ResumeLayout(false);
            this.panelControl2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtToDate.Properties.VistaTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtToDate.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtFromDate.Properties.VistaTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtFromDate.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl3)).EndInit();
            this.panelControl3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gcRecord)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvRecord)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl4)).EndInit();
            this.panelControl4.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl panelControl1;
        private Controls.CtlMedicalUserInfo ctlMedicalUserInfo;
        private DevExpress.XtraEditors.PanelControl panelControl2;
        private DevExpress.XtraEditors.DateEdit txtToDate;
        private DevExpress.XtraEditors.LabelControl lblToDate;
        private DevExpress.XtraEditors.DateEdit txtFromDate;
        private DevExpress.XtraEditors.LabelControl lblFromDate;
        private Hemo.Client.Controls.DXSimpleButton btnQuery;
        private DevExpress.XtraEditors.PanelControl panelControl3;
        private DevExpress.XtraGrid.GridControl gcRecord;
        private DevExpress.XtraGrid.Views.Grid.GridView gvRecord;
        private DevExpress.XtraGrid.Columns.GridColumn colHemoId;
        private DevExpress.XtraGrid.Columns.GridColumn colCreateDate;
        private DevExpress.XtraGrid.Columns.GridColumn colWEIGHT;
        private DevExpress.XtraEditors.PanelControl panelControl4;
        private Hemo.Client.Controls.DXSimpleButton btnAdd;
        private DevExpress.XtraGrid.Columns.GridColumn colSicklyLook;
        private DevExpress.XtraGrid.Columns.GridColumn colIMX;
        private DevExpress.XtraGrid.Columns.GridColumn colHCV;
        private DevExpress.XtraGrid.Columns.GridColumn colTPPA;
        private DevExpress.XtraGrid.Columns.GridColumn colAIDS;
        private DevExpress.XtraGrid.Columns.GridColumn colCreateBy;
        private Hemo.Client.Controls.DXSimpleButton btnDrug;
        private Hemo.Client.Controls.DXSimpleButton btnExit;
    }
}