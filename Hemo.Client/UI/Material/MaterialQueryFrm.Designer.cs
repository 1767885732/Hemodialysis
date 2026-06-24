namespace Hemo.Client.UI.Material
{
    partial class MaterialQueryFrm
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
            this.xtraTabControl1 = new DevExpress.XtraTab.XtraTabControl();
            this.xtraTabPage1 = new DevExpress.XtraTab.XtraTabPage();
            this.materialPatientQueryUI1 = new Hemo.Client.UI.Material.MaterialPatientQueryUI();
            this.xtraTabPage2 = new DevExpress.XtraTab.XtraTabPage();
            this.patientMaterialQueryUI1 = new Hemo.Client.UI.Material.PatientMaterialQueryUI();
            ((System.ComponentModel.ISupportInitialize)(this.xtraTabControl1)).BeginInit();
            this.xtraTabControl1.SuspendLayout();
            this.xtraTabPage1.SuspendLayout();
            this.xtraTabPage2.SuspendLayout();
            this.SuspendLayout();
            // 
            // xtraTabControl1
            // 
            this.xtraTabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.xtraTabControl1.Location = new System.Drawing.Point(0, 0);
            this.xtraTabControl1.Name = "xtraTabControl1";
            this.xtraTabControl1.SelectedTabPage = this.xtraTabPage1;
            this.xtraTabControl1.Size = new System.Drawing.Size(1379, 599);
            this.xtraTabControl1.TabIndex = 0;
            this.xtraTabControl1.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
            this.xtraTabPage1,
            this.xtraTabPage2});
            this.xtraTabControl1.SelectedPageChanged += new DevExpress.XtraTab.TabPageChangedEventHandler(this.xtraTabControl1_SelectedPageChanged);
            // 
            // xtraTabPage1
            // 
            this.xtraTabPage1.Controls.Add(this.materialPatientQueryUI1);
            this.xtraTabPage1.Name = "xtraTabPage1";
            this.xtraTabPage1.Size = new System.Drawing.Size(1373, 570);
            this.xtraTabPage1.Text = "项目维度";
            // 
            // materialPatientQueryUI1
            // 
            this.materialPatientQueryUI1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.materialPatientQueryUI1.HasDirty = false;
            this.materialPatientQueryUI1.Location = new System.Drawing.Point(0, 0);
            this.materialPatientQueryUI1.Name = "materialPatientQueryUI1";
            this.materialPatientQueryUI1.Size = new System.Drawing.Size(1373, 570);
            this.materialPatientQueryUI1.TabIndex = 0;
            // 
            // xtraTabPage2
            // 
            this.xtraTabPage2.Controls.Add(this.patientMaterialQueryUI1);
            this.xtraTabPage2.Name = "xtraTabPage2";
            this.xtraTabPage2.Size = new System.Drawing.Size(1043, 435);
            this.xtraTabPage2.Text = "患者维度";
            // 
            // patientMaterialQueryUI1
            // 
            this.patientMaterialQueryUI1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.patientMaterialQueryUI1.HasDirty = false;
            this.patientMaterialQueryUI1.Location = new System.Drawing.Point(0, 0);
            this.patientMaterialQueryUI1.Name = "patientMaterialQueryUI1";
            this.patientMaterialQueryUI1.Size = new System.Drawing.Size(1043, 435);
            this.patientMaterialQueryUI1.TabIndex = 0;
            // 
            // MaterialQueryFrm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1379, 599);
            this.Controls.Add(this.xtraTabControl1);
            this.Name = "MaterialQueryFrm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "耗材查询";
            this.Load += new System.EventHandler(this.MaterialQueryFrm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.xtraTabControl1)).EndInit();
            this.xtraTabControl1.ResumeLayout(false);
            this.xtraTabPage1.ResumeLayout(false);
            this.xtraTabPage2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraTab.XtraTabControl xtraTabControl1;
        private DevExpress.XtraTab.XtraTabPage xtraTabPage1;
        private DevExpress.XtraTab.XtraTabPage xtraTabPage2;
        private MaterialPatientQueryUI materialPatientQueryUI1;
        private PatientMaterialQueryUI patientMaterialQueryUI1;
    }
}