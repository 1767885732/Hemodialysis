namespace Hemo.Client.UI.Patient
{
    partial class PatientKolcaba
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PatientKolcaba));
            this.ctlMedicalUserInfo = new Hemo.Client.Controls.CtlMedicalUserInfo();
            this.gcRecord = new DevExpress.XtraGrid.GridControl();
            this.gvRecord = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colHemoId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCreateDate = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colTotalScore = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colLastUpdateBy = new DevExpress.XtraGrid.Columns.GridColumn();
            this.btnExit = new Hemo.Client.Controls.DXSimpleButton();
            this.btnAdd = new Hemo.Client.Controls.DXSimpleButton();
            this.panelControl2 = new DevExpress.XtraEditors.PanelControl();
            this.btnQuery = new Hemo.Client.Controls.DXSimpleButton();
            this.txtToDate = new DevExpress.XtraEditors.DateEdit();
            this.lblToDate = new DevExpress.XtraEditors.LabelControl();
            this.txtFromDate = new DevExpress.XtraEditors.DateEdit();
            this.lblFromDate = new DevExpress.XtraEditors.LabelControl();
            ((System.ComponentModel.ISupportInitialize)(this.gcRecord)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvRecord)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).BeginInit();
            this.panelControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtToDate.Properties.VistaTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtToDate.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtFromDate.Properties.VistaTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtFromDate.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // ctlMedicalUserInfo
            // 
            this.ctlMedicalUserInfo.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(245)))), ((int)(((byte)(241)))));
            this.ctlMedicalUserInfo.Appearance.Options.UseBackColor = true;
            this.ctlMedicalUserInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ctlMedicalUserInfo.HemoId = "";
            this.ctlMedicalUserInfo.Location = new System.Drawing.Point(0, 0);
            this.ctlMedicalUserInfo.Name = "ctlMedicalUserInfo";
            this.ctlMedicalUserInfo.Size = new System.Drawing.Size(727, 502);
            this.ctlMedicalUserInfo.TabIndex = 1;
            this.ctlMedicalUserInfo.Load += new System.EventHandler(this.ctlMedicalUserInfo_Load);
            // 
            // gcRecord
            // 
            this.gcRecord.Location = new System.Drawing.Point(15, 195);
            this.gcRecord.MainView = this.gvRecord;
            this.gcRecord.Name = "gcRecord";
            this.gcRecord.Size = new System.Drawing.Size(700, 260);
            this.gcRecord.TabIndex = 319;
            this.gcRecord.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvRecord});
            this.gcRecord.DoubleClick += new System.EventHandler(this.gcRecord_DoubleClick);
            // 
            // gvRecord
            // 
            this.gvRecord.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colHemoId,
            this.colCreateDate,
            this.colTotalScore,
            this.colLastUpdateBy});
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
            this.colHemoId.MaxWidth = 145;
            this.colHemoId.Name = "colHemoId";
            this.colHemoId.OptionsColumn.AllowEdit = false;
            this.colHemoId.Visible = true;
            this.colHemoId.VisibleIndex = 0;
            this.colHemoId.Width = 145;
            // 
            // colCreateDate
            // 
            this.colCreateDate.Caption = "录入日期";
            this.colCreateDate.FieldName = "CREATEDATE";
            this.colCreateDate.MaxWidth = 145;
            this.colCreateDate.Name = "colCreateDate";
            this.colCreateDate.OptionsColumn.AllowEdit = false;
            this.colCreateDate.Visible = true;
            this.colCreateDate.VisibleIndex = 1;
            this.colCreateDate.Width = 145;
            // 
            // colTotalScore
            // 
            this.colTotalScore.AppearanceCell.Options.UseTextOptions = true;
            this.colTotalScore.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            this.colTotalScore.Caption = "Kolcaba值";
            this.colTotalScore.FieldName = "TOTALSCORE";
            this.colTotalScore.MaxWidth = 145;
            this.colTotalScore.Name = "colTotalScore";
            this.colTotalScore.Visible = true;
            this.colTotalScore.VisibleIndex = 2;
            this.colTotalScore.Width = 145;
            // 
            // colLastUpdateBy
            // 
            this.colLastUpdateBy.AppearanceCell.Options.UseTextOptions = true;
            this.colLastUpdateBy.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            this.colLastUpdateBy.Caption = "录入人员";
            this.colLastUpdateBy.FieldName = "LASTUPDATEBY";
            this.colLastUpdateBy.MaxWidth = 145;
            this.colLastUpdateBy.Name = "colLastUpdateBy";
            this.colLastUpdateBy.Visible = true;
            this.colLastUpdateBy.VisibleIndex = 3;
            this.colLastUpdateBy.Width = 145;
            // 
            // btnExit
            // 
            this.btnExit.ImageIndex = 3;
            this.btnExit.Location = new System.Drawing.Point(628, 462);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(87, 27);
            this.btnExit.TabIndex = 321;
            this.btnExit.Text = "关闭(&Q)";
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // btnAdd
            // 
            this.btnAdd.ImageIndex = 0;
            this.btnAdd.Location = new System.Drawing.Point(477, 462);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(143, 27);
            this.btnAdd.TabIndex = 320;
            this.btnAdd.Text = "新增Kolcaba(&A)";
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // panelControl2
            // 
            this.panelControl2.Controls.Add(this.btnQuery);
            this.panelControl2.Controls.Add(this.txtToDate);
            this.panelControl2.Controls.Add(this.lblToDate);
            this.panelControl2.Controls.Add(this.txtFromDate);
            this.panelControl2.Controls.Add(this.lblFromDate);
            this.panelControl2.Location = new System.Drawing.Point(14, 159);
            this.panelControl2.Name = "panelControl2";
            this.panelControl2.Size = new System.Drawing.Size(700, 31);
            this.panelControl2.TabIndex = 322;
            // 
            // btnQuery
            // 
            this.btnQuery.ImageIndex = 8;
            this.btnQuery.Location = new System.Drawing.Point(407, 2);
            this.btnQuery.Name = "btnQuery";
            this.btnQuery.Size = new System.Drawing.Size(87, 27);
            this.btnQuery.TabIndex = 313;
            this.btnQuery.Text = "查询";
            this.btnQuery.Click += new System.EventHandler(this.btnQuery_Click);
            // 
            // txtToDate
            // 
            this.txtToDate.EditValue = null;
            this.txtToDate.Location = new System.Drawing.Point(283, 2);
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
            this.txtToDate.Size = new System.Drawing.Size(117, 23);
            this.txtToDate.TabIndex = 311;
            // 
            // lblToDate
            // 
            this.lblToDate.Appearance.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblToDate.Appearance.Options.UseFont = true;
            this.lblToDate.Location = new System.Drawing.Point(209, 6);
            this.lblToDate.Name = "lblToDate";
            this.lblToDate.Size = new System.Drawing.Size(48, 17);
            this.lblToDate.TabIndex = 312;
            this.lblToDate.Text = "结束日期";
            // 
            // txtFromDate
            // 
            this.txtFromDate.EditValue = null;
            this.txtFromDate.Location = new System.Drawing.Point(79, 2);
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
            this.txtFromDate.Size = new System.Drawing.Size(117, 23);
            this.txtFromDate.TabIndex = 309;
            // 
            // lblFromDate
            // 
            this.lblFromDate.Appearance.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFromDate.Appearance.Options.UseFont = true;
            this.lblFromDate.Location = new System.Drawing.Point(1, 7);
            this.lblFromDate.Name = "lblFromDate";
            this.lblFromDate.Size = new System.Drawing.Size(48, 17);
            this.lblFromDate.TabIndex = 310;
            this.lblFromDate.Text = "开始日期";
            // 
            // PatientKolcaba
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(727, 502);
            this.Controls.Add(this.panelControl2);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.gcRecord);
            this.Controls.Add(this.ctlMedicalUserInfo);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "PatientKolcaba";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "病人Kolcaba";
            ((System.ComponentModel.ISupportInitialize)(this.gcRecord)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvRecord)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).EndInit();
            this.panelControl2.ResumeLayout(false);
            this.panelControl2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtToDate.Properties.VistaTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtToDate.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtFromDate.Properties.VistaTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtFromDate.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Controls.CtlMedicalUserInfo ctlMedicalUserInfo;
        private DevExpress.XtraGrid.GridControl gcRecord;
        private DevExpress.XtraGrid.Views.Grid.GridView gvRecord;
        private DevExpress.XtraGrid.Columns.GridColumn colHemoId;
        private DevExpress.XtraGrid.Columns.GridColumn colCreateDate;
        private Controls.DXSimpleButton btnExit;
        private Controls.DXSimpleButton btnAdd;
        private DevExpress.XtraEditors.PanelControl panelControl2;
        private Controls.DXSimpleButton btnQuery;
        private DevExpress.XtraEditors.DateEdit txtToDate;
        private DevExpress.XtraEditors.LabelControl lblToDate;
        private DevExpress.XtraEditors.DateEdit txtFromDate;
        private DevExpress.XtraEditors.LabelControl lblFromDate;
        private DevExpress.XtraGrid.Columns.GridColumn colTotalScore;
        private DevExpress.XtraGrid.Columns.GridColumn colLastUpdateBy;
    }
}