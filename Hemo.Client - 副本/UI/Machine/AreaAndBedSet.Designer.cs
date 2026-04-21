namespace Hemo.Client.UI.Machine
{
    partial class AreaAndBedSet
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
            this.cbxAREA = new DevExpress.XtraEditors.LookUpEdit();
            this.lab1 = new DevExpress.XtraEditors.LabelControl();
            this.simpleButton1 = new Hemo.Client.Controls.DXSimpleButton();
            this.btnSave = new Hemo.Client.Controls.DXSimpleButton();
            this.cbxBED = new DevExpress.XtraEditors.LookUpEdit();
            this.lab2 = new DevExpress.XtraEditors.LabelControl();
            this.errorProvider = new DevExpress.XtraEditors.DXErrorProvider.DXErrorProvider(this.components);
            this.simpleButton2 = new Hemo.Client.Controls.DXSimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.cbxAREA.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cbxBED.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).BeginInit();
            this.SuspendLayout();
            // 
            // cbxAREA
            // 
            this.cbxAREA.EditValue = "";
            this.cbxAREA.Location = new System.Drawing.Point(42, 12);
            this.cbxAREA.Name = "cbxAREA";
            this.cbxAREA.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cbxAREA.Properties.LookAndFeel.SkinName = "ExtendBlue";
            this.cbxAREA.Size = new System.Drawing.Size(238, 21);
            this.cbxAREA.TabIndex = 345;
            // 
            // lab1
            // 
            this.lab1.Location = new System.Drawing.Point(12, 15);
            this.lab1.Name = "lab1";
            this.lab1.Size = new System.Drawing.Size(24, 14);
            this.lab1.TabIndex = 346;
            this.lab1.Text = "区域";
            // 
            // simpleButton1
            // 
            this.simpleButton1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.simpleButton1.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.simpleButton1.ImageIndex = 8;
            this.simpleButton1.Location = new System.Drawing.Point(179, 84);
            this.simpleButton1.Name = "simpleButton1";
            this.simpleButton1.Size = new System.Drawing.Size(101, 31);
            this.simpleButton1.TabIndex = 357;
            this.simpleButton1.Text = "取消";
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSave.ImageIndex = 7;
            this.btnSave.Location = new System.Drawing.Point(72, 84);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(101, 31);
            this.btnSave.TabIndex = 356;
            this.btnSave.Text = "保存(&S)";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // cbxBED
            // 
            this.cbxBED.EditValue = "";
            this.cbxBED.Location = new System.Drawing.Point(42, 39);
            this.cbxBED.Name = "cbxBED";
            this.cbxBED.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cbxBED.Properties.LookAndFeel.SkinName = "ExtendBlue";
            this.cbxBED.Size = new System.Drawing.Size(238, 21);
            this.cbxBED.TabIndex = 358;
            // 
            // lab2
            // 
            this.lab2.Location = new System.Drawing.Point(12, 42);
            this.lab2.Name = "lab2";
            this.lab2.Size = new System.Drawing.Size(24, 14);
            this.lab2.TabIndex = 359;
            this.lab2.Text = "床位";
            // 
            // errorProvider
            // 
            this.errorProvider.ContainerControl = this;
            // 
            // simpleButton2
            // 
            this.simpleButton2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.simpleButton2.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.simpleButton2.ImageIndex = 3;
            this.simpleButton2.Location = new System.Drawing.Point(179, 84);
            this.simpleButton2.Name = "simpleButton2";
            this.simpleButton2.Size = new System.Drawing.Size(101, 31);
            this.simpleButton2.TabIndex = 357;
            this.simpleButton2.Text = "取消(&C)";
            this.simpleButton2.Click += new System.EventHandler(this.simpleButton2_Click);
            // 
            // AreaAndBedSet
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(289, 127);
            this.Controls.Add(this.cbxBED);
            this.Controls.Add(this.lab2);
            this.Controls.Add(this.simpleButton2);
            this.Controls.Add(this.simpleButton1);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.cbxAREA);
            this.Controls.Add(this.lab1);
            this.MinimumSize = new System.Drawing.Size(305, 165);
            this.Name = "AreaAndBedSet";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "床位、区域设定";
            this.Load += new System.EventHandler(this.AreaAndBedSet_Load);
            ((System.ComponentModel.ISupportInitialize)(this.cbxAREA.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cbxBED.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.LookUpEdit cbxAREA;
        private DevExpress.XtraEditors.LabelControl lab1;
        private Hemo.Client.Controls.DXSimpleButton simpleButton1;
        private Hemo.Client.Controls.DXSimpleButton btnSave;
        private DevExpress.XtraEditors.LookUpEdit cbxBED;
        private DevExpress.XtraEditors.LabelControl lab2;
        private DevExpress.XtraEditors.DXErrorProvider.DXErrorProvider errorProvider;
        private Hemo.Client.Controls.DXSimpleButton simpleButton2;
    }
}