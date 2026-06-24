namespace Hemo.Client.UI.Hemodialysis {
    partial class QueryAirDisnfect {
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
            this.panelControl2 = new DevExpress.XtraEditors.PanelControl();
            this.btnConfirm = new Hemo.Client.Controls.DXSimpleButton();
            this.SEX = new DevExpress.XtraEditors.ComboBoxEdit();
            this.textEdit4 = new DevExpress.XtraEditors.TextEdit();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.btnQuery = new Hemo.Client.Controls.DXSimpleButton();
            this.grdInfectious = new DevExpress.XtraGrid.GridControl();
            this.advBandedGridView1 = new DevExpress.XtraGrid.Views.BandedGrid.AdvBandedGridView();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).BeginInit();
            this.panelControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.SEX.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEdit4.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdInfectious)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.advBandedGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // panelControl2
            // 
            this.panelControl2.Controls.Add(this.btnConfirm);
            this.panelControl2.Controls.Add(this.SEX);
            this.panelControl2.Controls.Add(this.textEdit4);
            this.panelControl2.Controls.Add(this.labelControl1);
            this.panelControl2.Controls.Add(this.labelControl2);
            this.panelControl2.Controls.Add(this.btnQuery);
            this.panelControl2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelControl2.Location = new System.Drawing.Point(0, 0);
            this.panelControl2.Name = "panelControl2";
            this.panelControl2.Size = new System.Drawing.Size(928, 59);
            this.panelControl2.TabIndex = 45;
            // 
            // btnConfirm
            // 
            this.btnConfirm.ImageIndex = 2;
            this.btnConfirm.Location = new System.Drawing.Point(427, 18);
            this.btnConfirm.Name = "btnConfirm";
            this.btnConfirm.Size = new System.Drawing.Size(75, 25);
            this.btnConfirm.TabIndex = 28;
            this.btnConfirm.Text = "编辑(&E)";
            this.btnConfirm.Click += new System.EventHandler(this.btnConfirm_Click);
            // 
            // SEX
            // 
            this.SEX.Location = new System.Drawing.Point(200, 20);
            this.SEX.Name = "SEX";
            this.SEX.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.SEX.Properties.Items.AddRange(new object[] {
            "男",
            "女"});
            this.SEX.Properties.LookAndFeel.SkinName = "ExtendBlue";
            this.SEX.Size = new System.Drawing.Size(100, 21);
            this.SEX.TabIndex = 27;
            // 
            // textEdit4
            // 
            this.textEdit4.Location = new System.Drawing.Point(53, 20);
            this.textEdit4.Name = "textEdit4";
            this.textEdit4.Size = new System.Drawing.Size(100, 21);
            this.textEdit4.TabIndex = 26;
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(170, 23);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(24, 14);
            this.labelControl1.TabIndex = 25;
            this.labelControl1.Text = "性别";
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(23, 24);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(24, 14);
            this.labelControl2.TabIndex = 24;
            this.labelControl2.Text = "姓名";
            // 
            // btnQuery
            // 
            this.btnQuery.ImageIndex = 8;
            this.btnQuery.Location = new System.Drawing.Point(330, 18);
            this.btnQuery.Name = "btnQuery";
            this.btnQuery.Size = new System.Drawing.Size(75, 25);
            this.btnQuery.TabIndex = 16;
            this.btnQuery.Text = "查询(&Q)";
            this.btnQuery.Click += new System.EventHandler(this.btnQuery_Click);
            // 
            // grdInfectious
            // 
            this.grdInfectious.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdInfectious.Location = new System.Drawing.Point(0, 59);
            this.grdInfectious.MainView = this.advBandedGridView1;
            this.grdInfectious.Name = "grdInfectious";
            this.grdInfectious.Size = new System.Drawing.Size(928, 413);
            this.grdInfectious.TabIndex = 48;
            this.grdInfectious.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.advBandedGridView1});
            // 
            // advBandedGridView1
            // 
            this.advBandedGridView1.GridControl = this.grdInfectious;
            this.advBandedGridView1.Name = "advBandedGridView1";
            this.advBandedGridView1.OptionsView.ShowGroupPanel = false;
            // 
            // QueryAirDisnfect
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(928, 472);
            this.Controls.Add(this.grdInfectious);
            this.Controls.Add(this.panelControl2);
            this.Name = "QueryAirDisnfect";
            this.Text = "查询传染病入院检查";
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).EndInit();
            this.panelControl2.ResumeLayout(false);
            this.panelControl2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.SEX.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEdit4.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdInfectious)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.advBandedGridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl panelControl2;
        private DevExpress.XtraEditors.ComboBoxEdit SEX;
        private DevExpress.XtraEditors.TextEdit textEdit4;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private Hemo.Client.Controls.DXSimpleButton btnQuery;
        private Hemo.Client.Controls.DXSimpleButton btnConfirm;
        private DevExpress.XtraGrid.GridControl grdInfectious;
        private DevExpress.XtraGrid.Views.BandedGrid.AdvBandedGridView advBandedGridView1;
    }
}