
namespace Hemo.Client.Modules
{
    partial class PatientSearch
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PatientSearch));
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.btnSearch = new DevExpress.XtraEditors.SimpleButton();
            this.customGridLookUpEdit1 = new Hemo.Utilities.CustomGridLookUpEdit();
            this.customGridLookUpEdit1View = new Hemo.Utilities.CustomGridView();
            this.gridColumn5 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn6 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn7 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.label3 = new System.Windows.Forms.Label();
            this.txt_FilterPatient = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.gridControlPatient = new DevExpress.XtraGrid.GridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumnPATIENTID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumnName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumnAGE = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumnBEDNO = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumnInputCode = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumnDIAGNOSE = new DevExpress.XtraGrid.Columns.GridColumn();
            this.busyIndicator1 = new Hemo.Client.Controls.BusyIndicator();
            this.panelControl2 = new DevExpress.XtraEditors.PanelControl();
            this.btnClose = new DevExpress.XtraEditors.SimpleButton();
            this.btnOk = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.customGridLookUpEdit1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.customGridLookUpEdit1View)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControlPatient)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).BeginInit();
            this.panelControl2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.btnSearch);
            this.panelControl1.Controls.Add(this.customGridLookUpEdit1);
            this.panelControl1.Controls.Add(this.label3);
            this.panelControl1.Controls.Add(this.txt_FilterPatient);
            this.panelControl1.Controls.Add(this.label1);
            resources.ApplyResources(this.panelControl1, "panelControl1");
            this.panelControl1.Name = "panelControl1";
            // 
            // btnSearch
            // 
            this.btnSearch.Image = ((System.Drawing.Image)(resources.GetObject("btnSearch.Image")));
            this.btnSearch.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleLeft;
            resources.ApplyResources(this.btnSearch, "btnSearch");
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // customGridLookUpEdit1
            // 
            resources.ApplyResources(this.customGridLookUpEdit1, "customGridLookUpEdit1");
            this.customGridLookUpEdit1.Name = "customGridLookUpEdit1";
            this.customGridLookUpEdit1.Properties.AutoComplete = false;
            this.customGridLookUpEdit1.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(((DevExpress.XtraEditors.Controls.ButtonPredefines)(resources.GetObject("customGridLookUpEdit1.Properties.Buttons"))))});
            this.customGridLookUpEdit1.Properties.DisplayMember = "ITEM_NAME";
            this.customGridLookUpEdit1.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard;
            this.customGridLookUpEdit1.Properties.ValueMember = "ITEM_ID";
            this.customGridLookUpEdit1.Properties.View = this.customGridLookUpEdit1View;
            // 
            // customGridLookUpEdit1View
            // 
            this.customGridLookUpEdit1View.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn5,
            this.gridColumn6,
            this.gridColumn7});
            this.customGridLookUpEdit1View.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.customGridLookUpEdit1View.Name = "customGridLookUpEdit1View";
            this.customGridLookUpEdit1View.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.customGridLookUpEdit1View.OptionsView.ShowGroupPanel = false;
            // 
            // gridColumn5
            // 
            resources.ApplyResources(this.gridColumn5, "gridColumn5");
            this.gridColumn5.FieldName = "ITEM_NAME";
            this.gridColumn5.Name = "gridColumn5";
            // 
            // gridColumn6
            // 
            resources.ApplyResources(this.gridColumn6, "gridColumn6");
            this.gridColumn6.FieldName = "ITEM_ID";
            this.gridColumn6.Name = "gridColumn6";
            // 
            // gridColumn7
            // 
            resources.ApplyResources(this.gridColumn7, "gridColumn7");
            this.gridColumn7.FieldName = "ITEM_VALUE";
            this.gridColumn7.Name = "gridColumn7";
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.Name = "label3";
            // 
            // txt_FilterPatient
            // 
            resources.ApplyResources(this.txt_FilterPatient, "txt_FilterPatient");
            this.txt_FilterPatient.Name = "txt_FilterPatient";
            this.txt_FilterPatient.TextChanged += new System.EventHandler(this.txt_FilterPatient_TextChanged);
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // gridControlPatient
            // 
            resources.ApplyResources(this.gridControlPatient, "gridControlPatient");
            this.gridControlPatient.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Office2003;
            this.gridControlPatient.MainView = this.gridView1;
            this.gridControlPatient.Name = "gridControlPatient";
            this.gridControlPatient.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            // 
            // gridView1
            // 
            this.gridView1.Appearance.HeaderPanel.Options.UseTextOptions = true;
            this.gridView1.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridView1.Appearance.HeaderPanel.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.gridView1.Appearance.HorzLine.Options.UseTextOptions = true;
            this.gridView1.Appearance.HorzLine.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridView1.Appearance.HorzLine.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.gridView1.Appearance.OddRow.BackColor = ((System.Drawing.Color)(resources.GetObject("gridView1.Appearance.OddRow.BackColor")));
            this.gridView1.Appearance.OddRow.Options.UseBackColor = true;
            this.gridView1.ColumnPanelRowHeight = 20;
            this.gridView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumnPATIENTID,
            this.gridColumnName,
            this.gridColumnAGE,
            this.gridColumnBEDNO,
            this.gridColumnInputCode,
            this.gridColumnDIAGNOSE});
            this.gridView1.GridControl = this.gridControlPatient;
            resources.ApplyResources(this.gridView1, "gridView1");
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsBehavior.AutoPopulateColumns = false;
            this.gridView1.OptionsBehavior.Editable = false;
            this.gridView1.OptionsCustomization.AllowColumnMoving = false;
            this.gridView1.OptionsDetail.ShowDetailTabs = false;
            this.gridView1.OptionsMenu.EnableColumnMenu = false;
            this.gridView1.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gridView1.OptionsView.EnableAppearanceOddRow = true;
            this.gridView1.OptionsView.ShowGroupPanel = false;
            this.gridView1.OptionsView.ShowIndicator = false;
            this.gridView1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.gridView1_MouseDown);
            // 
            // gridColumnPATIENTID
            // 
            resources.ApplyResources(this.gridColumnPATIENTID, "gridColumnPATIENTID");
            this.gridColumnPATIENTID.FieldName = "PATIENT_ID";
            this.gridColumnPATIENTID.Name = "gridColumnPATIENTID";
            // 
            // gridColumnName
            // 
            resources.ApplyResources(this.gridColumnName, "gridColumnName");
            this.gridColumnName.FieldName = "NAME";
            this.gridColumnName.Name = "gridColumnName";
            // 
            // gridColumnAGE
            // 
            resources.ApplyResources(this.gridColumnAGE, "gridColumnAGE");
            this.gridColumnAGE.FieldName = "AGE";
            this.gridColumnAGE.Name = "gridColumnAGE";
            // 
            // gridColumnBEDNO
            // 
            resources.ApplyResources(this.gridColumnBEDNO, "gridColumnBEDNO");
            this.gridColumnBEDNO.FieldName = "BED_NO";
            this.gridColumnBEDNO.Name = "gridColumnBEDNO";
            // 
            // gridColumnInputCode
            // 
            resources.ApplyResources(this.gridColumnInputCode, "gridColumnInputCode");
            this.gridColumnInputCode.FieldName = "INPUT_CODE";
            this.gridColumnInputCode.Name = "gridColumnInputCode";
            // 
            // gridColumnDIAGNOSE
            // 
            resources.ApplyResources(this.gridColumnDIAGNOSE, "gridColumnDIAGNOSE");
            this.gridColumnDIAGNOSE.FieldName = "DIAGNOSE";
            this.gridColumnDIAGNOSE.Name = "gridColumnDIAGNOSE";
            // 
            // busyIndicator1
            // 
            resources.ApplyResources(this.busyIndicator1, "busyIndicator1");
            this.busyIndicator1.Name = "busyIndicator1";
            // 
            // panelControl2
            // 
            this.panelControl2.Controls.Add(this.btnClose);
            this.panelControl2.Controls.Add(this.btnOk);
            resources.ApplyResources(this.panelControl2, "panelControl2");
            this.panelControl2.Name = "panelControl2";
            // 
            // btnClose
            // 
            this.btnClose.Image = ((System.Drawing.Image)(resources.GetObject("btnClose.Image")));
            resources.ApplyResources(this.btnClose, "btnClose");
            this.btnClose.Name = "btnClose";
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnOk
            // 
            this.btnOk.Image = ((System.Drawing.Image)(resources.GetObject("btnOk.Image")));
            this.btnOk.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleLeft;
            resources.ApplyResources(this.btnOk, "btnOk");
            this.btnOk.Name = "btnOk";
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // PatientSearch
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panelControl2);
            this.Controls.Add(this.busyIndicator1);
            this.Controls.Add(this.gridControlPatient);
            this.Controls.Add(this.panelControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "PatientSearch";
            this.Load += new System.EventHandler(this.PatientSearch_Load);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.panelControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.customGridLookUpEdit1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.customGridLookUpEdit1View)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControlPatient)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).EndInit();
            this.panelControl2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl panelControl1;
        private Hemo.Utilities.CustomGridLookUpEdit customGridLookUpEdit1;
        private Hemo.Utilities.CustomGridView customGridLookUpEdit1View;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn5;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn6;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn7;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txt_FilterPatient;
        private System.Windows.Forms.Label label1;
        private DevExpress.XtraGrid.GridControl gridControlPatient;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnPATIENTID;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnName;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnAGE;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnBEDNO;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnInputCode;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnDIAGNOSE;
        private Controls.BusyIndicator busyIndicator1;
        private DevExpress.XtraEditors.PanelControl panelControl2;
        private DevExpress.XtraEditors.SimpleButton btnClose;
        private DevExpress.XtraEditors.SimpleButton btnOk;
        private DevExpress.XtraEditors.SimpleButton btnSearch;
    }
}