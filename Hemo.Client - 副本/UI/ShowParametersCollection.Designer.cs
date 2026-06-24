namespace Hemo.Client.UI {
    partial class ShowParametersCollection {
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
            this.components = new System.ComponentModel.Container();
            this.panelControl3 = new DevExpress.XtraEditors.PanelControl();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.lookUpEdit_Machine = new DevExpress.XtraEditors.LookUpEdit();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.lookUpEdit_Area = new DevExpress.XtraEditors.LookUpEdit();
            this.btnQuery = new Hemo.Client.Controls.DXSimpleButton();
            this.lookUpEdit_Class = new DevExpress.XtraEditors.LookUpEdit();
            this.gcDataList = new DevExpress.XtraGrid.GridControl();
            this.gvDataList = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colCheck = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemCheckEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            this.colCreateDate = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colVenousPressure = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colTransmembranePressure = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colConductivity = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colDisplacement = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colBloodFlow = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colDialysateRate = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colUrf = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colTemperature = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCardiotach = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colSystolicPressure = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colDiastolicPressure = new DevExpress.XtraGrid.Columns.GridColumn();
            this.panelControl2 = new DevExpress.XtraEditors.PanelControl();
            this.btnUncheckAll = new Hemo.Client.Controls.DXSimpleButton();
            this.btnSave = new Hemo.Client.Controls.DXSimpleButton();
            this.btnCheckAll = new Hemo.Client.Controls.DXSimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl3)).BeginInit();
            this.panelControl3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lookUpEdit_Machine.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lookUpEdit_Area.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lookUpEdit_Class.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcDataList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvDataList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).BeginInit();
            this.panelControl2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelControl3
            // 
            this.panelControl3.Controls.Add(this.labelControl3);
            this.panelControl3.Controls.Add(this.lookUpEdit_Machine);
            this.panelControl3.Controls.Add(this.labelControl2);
            this.panelControl3.Controls.Add(this.labelControl1);
            this.panelControl3.Controls.Add(this.lookUpEdit_Area);
            this.panelControl3.Controls.Add(this.btnQuery);
            this.panelControl3.Controls.Add(this.lookUpEdit_Class);
            this.panelControl3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelControl3.Location = new System.Drawing.Point(0, 0);
            this.panelControl3.Name = "panelControl3";
            this.panelControl3.Size = new System.Drawing.Size(782, 36);
            this.panelControl3.TabIndex = 3;
            // 
            // labelControl3
            // 
            this.labelControl3.Location = new System.Drawing.Point(512, 11);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(24, 14);
            this.labelControl3.TabIndex = 7;
            this.labelControl3.Text = "班次";
            // 
            // lookUpEdit_Machine
            // 
            this.lookUpEdit_Machine.Location = new System.Drawing.Point(397, 8);
            this.lookUpEdit_Machine.Name = "lookUpEdit_Machine";
            this.lookUpEdit_Machine.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lookUpEdit_Machine.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("CWNAME", 40, "床位"),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("FLNAME", 70, "设备分类"),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("MACHINE_MODEL", 70, "设备型号"),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("OTHER_THERAPEUTIC", 70, "设备标识")});
            this.lookUpEdit_Machine.Properties.DisplayMember = "OTHER_THERAPEUTIC";
            this.lookUpEdit_Machine.Properties.NullText = "";
            this.lookUpEdit_Machine.Properties.PopupWidth = 250;
            this.lookUpEdit_Machine.Properties.ValueMember = "OTHER_THERAPEUTIC";
            this.lookUpEdit_Machine.Size = new System.Drawing.Size(100, 21);
            this.lookUpEdit_Machine.TabIndex = 6;
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(367, 11);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(24, 14);
            this.labelControl2.TabIndex = 5;
            this.labelControl2.Text = "设备";
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(222, 11);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(24, 14);
            this.labelControl1.TabIndex = 4;
            this.labelControl1.Text = "病区";
            // 
            // lookUpEdit_Area
            // 
            this.lookUpEdit_Area.Location = new System.Drawing.Point(252, 8);
            this.lookUpEdit_Area.Name = "lookUpEdit_Area";
            this.lookUpEdit_Area.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lookUpEdit_Area.Properties.NullText = "";
            this.lookUpEdit_Area.Size = new System.Drawing.Size(100, 21);
            this.lookUpEdit_Area.TabIndex = 3;
            // 
            // btnQuery
            // 
            this.btnQuery.ImageIndex = 8;
            this.btnQuery.Location = new System.Drawing.Point(659, 7);
            this.btnQuery.Name = "btnQuery";
            this.btnQuery.Size = new System.Drawing.Size(75, 23);
            this.btnQuery.TabIndex = 2;
            this.btnQuery.Text = "查询";
            // 
            // lookUpEdit_Class
            // 
            this.lookUpEdit_Class.Location = new System.Drawing.Point(542, 8);
            this.lookUpEdit_Class.Name = "lookUpEdit_Class";
            this.lookUpEdit_Class.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lookUpEdit_Class.Properties.NullText = "";
            this.lookUpEdit_Class.Size = new System.Drawing.Size(100, 21);
            this.lookUpEdit_Class.TabIndex = 0;
            // 
            // gcDataList
            // 
            this.gcDataList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gcDataList.Location = new System.Drawing.Point(0, 36);
            this.gcDataList.MainView = this.gvDataList;
            this.gcDataList.Name = "gcDataList";
            this.gcDataList.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemCheckEdit1});
            this.gcDataList.Size = new System.Drawing.Size(782, 408);
            this.gcDataList.TabIndex = 4;
            this.gcDataList.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvDataList});
            // 
            // gvDataList
            // 
            this.gvDataList.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colCheck,
            this.colCreateDate,
            this.colVenousPressure,
            this.colTransmembranePressure,
            this.colConductivity,
            this.colDisplacement,
            this.colBloodFlow,
            this.colDialysateRate,
            this.colUrf,
            this.colTemperature,
            this.colCardiotach,
            this.colSystolicPressure,
            this.colDiastolicPressure});
            this.gvDataList.GridControl = this.gcDataList;
            this.gvDataList.Name = "gvDataList";
            this.gvDataList.OptionsView.ColumnAutoWidth = false;
            this.gvDataList.OptionsView.ShowGroupPanel = false;
            // 
            // colCheck
            // 
            this.colCheck.Caption = "选择";
            this.colCheck.ColumnEdit = this.repositoryItemCheckEdit1;
            this.colCheck.FieldName = "CHOOSE";
            this.colCheck.Name = "colCheck";
            this.colCheck.Visible = true;
            this.colCheck.VisibleIndex = 0;
            this.colCheck.Width = 60;
            // 
            // repositoryItemCheckEdit1
            // 
            this.repositoryItemCheckEdit1.AutoHeight = false;
            this.repositoryItemCheckEdit1.Name = "repositoryItemCheckEdit1";
            // 
            // colCreateDate
            // 
            this.colCreateDate.Caption = "日期";
            this.colCreateDate.DisplayFormat.FormatString = "yyyy-MM-dd HH:mm:ss";
            this.colCreateDate.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.colCreateDate.FieldName = "CREATE_DATE";
            this.colCreateDate.Name = "colCreateDate";
            this.colCreateDate.OptionsColumn.AllowEdit = false;
            this.colCreateDate.Visible = true;
            this.colCreateDate.VisibleIndex = 1;
            this.colCreateDate.Width = 130;
            // 
            // colVenousPressure
            // 
            this.colVenousPressure.Caption = "静脉压";
            this.colVenousPressure.FieldName = "VENOUS_PRESSURE";
            this.colVenousPressure.Name = "colVenousPressure";
            this.colVenousPressure.OptionsColumn.AllowEdit = false;
            this.colVenousPressure.Visible = true;
            this.colVenousPressure.VisibleIndex = 2;
            // 
            // colTransmembranePressure
            // 
            this.colTransmembranePressure.Caption = "跨膜压";
            this.colTransmembranePressure.FieldName = "TRANSMEMBRANE_PRESSURE";
            this.colTransmembranePressure.Name = "colTransmembranePressure";
            this.colTransmembranePressure.OptionsColumn.AllowEdit = false;
            this.colTransmembranePressure.Visible = true;
            this.colTransmembranePressure.VisibleIndex = 3;
            // 
            // colConductivity
            // 
            this.colConductivity.Caption = "电导度";
            this.colConductivity.FieldName = "CONDUCTIVITY";
            this.colConductivity.Name = "colConductivity";
            this.colConductivity.OptionsColumn.AllowEdit = false;
            this.colConductivity.Visible = true;
            this.colConductivity.VisibleIndex = 4;
            // 
            // colDisplacement
            // 
            this.colDisplacement.Caption = "置换量";
            this.colDisplacement.FieldName = "DISPLACEMENT";
            this.colDisplacement.Name = "colDisplacement";
            this.colDisplacement.OptionsColumn.AllowEdit = false;
            this.colDisplacement.Visible = true;
            this.colDisplacement.VisibleIndex = 5;
            // 
            // colBloodFlow
            // 
            this.colBloodFlow.Caption = "血流量";
            this.colBloodFlow.FieldName = "BLOOD_FLOW";
            this.colBloodFlow.Name = "colBloodFlow";
            this.colBloodFlow.OptionsColumn.AllowEdit = false;
            this.colBloodFlow.Visible = true;
            this.colBloodFlow.VisibleIndex = 6;
            // 
            // colDialysateRate
            // 
            this.colDialysateRate.Caption = "透析液流量";
            this.colDialysateRate.FieldName = "DIALYSATE_RATE";
            this.colDialysateRate.Name = "colDialysateRate";
            this.colDialysateRate.OptionsColumn.AllowEdit = false;
            this.colDialysateRate.Visible = true;
            this.colDialysateRate.VisibleIndex = 7;
            this.colDialysateRate.Width = 80;
            // 
            // colUrf
            // 
            this.colUrf.Caption = "超滤率";
            this.colUrf.FieldName = "URF";
            this.colUrf.Name = "colUrf";
            this.colUrf.OptionsColumn.AllowEdit = false;
            this.colUrf.Visible = true;
            this.colUrf.VisibleIndex = 8;
            // 
            // colTemperature
            // 
            this.colTemperature.Caption = "体温";
            this.colTemperature.FieldName = "TEMPERATURE";
            this.colTemperature.Name = "colTemperature";
            this.colTemperature.OptionsColumn.AllowEdit = false;
            this.colTemperature.Visible = true;
            this.colTemperature.VisibleIndex = 9;
            // 
            // colCardiotach
            // 
            this.colCardiotach.Caption = "脉搏";
            this.colCardiotach.FieldName = "CARDIOTACH";
            this.colCardiotach.Name = "colCardiotach";
            this.colCardiotach.OptionsColumn.AllowEdit = false;
            this.colCardiotach.Visible = true;
            this.colCardiotach.VisibleIndex = 10;
            // 
            // colSystolicPressure
            // 
            this.colSystolicPressure.Caption = "收缩压";
            this.colSystolicPressure.FieldName = "SYSTOLIC_PRESSURE";
            this.colSystolicPressure.Name = "colSystolicPressure";
            this.colSystolicPressure.OptionsColumn.AllowEdit = false;
            this.colSystolicPressure.Visible = true;
            this.colSystolicPressure.VisibleIndex = 11;
            // 
            // colDiastolicPressure
            // 
            this.colDiastolicPressure.Caption = "舒张压";
            this.colDiastolicPressure.FieldName = "DIASTOLIC_PRESSURE";
            this.colDiastolicPressure.Name = "colDiastolicPressure";
            this.colDiastolicPressure.OptionsColumn.AllowEdit = false;
            this.colDiastolicPressure.Visible = true;
            this.colDiastolicPressure.VisibleIndex = 12;
            // 
            // panelControl2
            // 
            this.panelControl2.Controls.Add(this.btnUncheckAll);
            this.panelControl2.Controls.Add(this.btnSave);
            this.panelControl2.Controls.Add(this.btnCheckAll);
            this.panelControl2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelControl2.Location = new System.Drawing.Point(0, 411);
            this.panelControl2.Name = "panelControl2";
            this.panelControl2.Size = new System.Drawing.Size(782, 33);
            this.panelControl2.TabIndex = 5;
            // 
            // btnUncheckAll
            // 
            this.btnUncheckAll.ImageIndex = 13;
            this.btnUncheckAll.Location = new System.Drawing.Point(578, 5);
            this.btnUncheckAll.Name = "btnUncheckAll";
            this.btnUncheckAll.Size = new System.Drawing.Size(75, 23);
            this.btnUncheckAll.TabIndex = 2;
            this.btnUncheckAll.Text = "取消全选";
            // 
            // btnSave
            // 
            this.btnSave.ImageIndex = 7;
            this.btnSave.Location = new System.Drawing.Point(659, 5);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 1;
            this.btnSave.Text = "保存";
            // 
            // btnCheckAll
            // 
            this.btnCheckAll.ImageIndex = 12;
            this.btnCheckAll.Location = new System.Drawing.Point(497, 5);
            this.btnCheckAll.Name = "btnCheckAll";
            this.btnCheckAll.Size = new System.Drawing.Size(75, 23);
            this.btnCheckAll.TabIndex = 0;
            this.btnCheckAll.Text = "全选";
            // 
            // ShowParametersCollection
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(782, 444);
            this.Controls.Add(this.panelControl2);
            this.Controls.Add(this.gcDataList);
            this.Controls.Add(this.panelControl3);
            this.Name = "ShowParametersCollection";
            this.Text = "ShowParametersCollection";
            ((System.ComponentModel.ISupportInitialize)(this.panelControl3)).EndInit();
            this.panelControl3.ResumeLayout(false);
            this.panelControl3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lookUpEdit_Machine.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lookUpEdit_Area.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lookUpEdit_Class.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcDataList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvDataList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).EndInit();
            this.panelControl2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl panelControl3;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.LookUpEdit lookUpEdit_Machine;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.LookUpEdit lookUpEdit_Area;
        private Controls.DXSimpleButton btnQuery;
        private DevExpress.XtraEditors.LookUpEdit lookUpEdit_Class;
        private DevExpress.XtraGrid.GridControl gcDataList;
        private DevExpress.XtraGrid.Views.Grid.GridView gvDataList;
        private DevExpress.XtraGrid.Columns.GridColumn colCheck;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit repositoryItemCheckEdit1;
        private DevExpress.XtraGrid.Columns.GridColumn colCreateDate;
        private DevExpress.XtraGrid.Columns.GridColumn colVenousPressure;
        private DevExpress.XtraGrid.Columns.GridColumn colTransmembranePressure;
        private DevExpress.XtraGrid.Columns.GridColumn colConductivity;
        private DevExpress.XtraGrid.Columns.GridColumn colDisplacement;
        private DevExpress.XtraGrid.Columns.GridColumn colBloodFlow;
        private DevExpress.XtraGrid.Columns.GridColumn colDialysateRate;
        private DevExpress.XtraGrid.Columns.GridColumn colUrf;
        private DevExpress.XtraGrid.Columns.GridColumn colTemperature;
        private DevExpress.XtraGrid.Columns.GridColumn colCardiotach;
        private DevExpress.XtraGrid.Columns.GridColumn colSystolicPressure;
        private DevExpress.XtraGrid.Columns.GridColumn colDiastolicPressure;
        private DevExpress.XtraEditors.PanelControl panelControl2;
        private Controls.DXSimpleButton btnUncheckAll;
        private Controls.DXSimpleButton btnSave;
        private Controls.DXSimpleButton btnCheckAll;
    }
}