namespace Hemo.Client {
    partial class TestInterFace {
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
            this.simpleButton1 = new Hemo.Client.Controls.DXSimpleButton();
            this.simpleButton2 = new Hemo.Client.Controls.DXSimpleButton();
            this.simpleButton3 = new Hemo.Client.Controls.DXSimpleButton();
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // simpleButton1
            // 
            this.simpleButton1.Location = new System.Drawing.Point(75, 58);
            this.simpleButton1.Name = "simpleButton1";
            this.simpleButton1.Size = new System.Drawing.Size(107, 23);
            this.simpleButton1.TabIndex = 0;
            this.simpleButton1.Text = "测试病人数据";
            this.simpleButton1.Click += new System.EventHandler(this.simpleButton1_Click);
            // 
            // simpleButton2
            // 
            this.simpleButton2.Location = new System.Drawing.Point(75, 106);
            this.simpleButton2.Name = "simpleButton2";
            this.simpleButton2.Size = new System.Drawing.Size(107, 23);
            this.simpleButton2.TabIndex = 0;
            this.simpleButton2.Text = "测试医嘱数据";
            this.simpleButton2.Click += new System.EventHandler(this.simpleButton2_Click);
            // 
            // simpleButton3
            // 
            this.simpleButton3.Location = new System.Drawing.Point(75, 157);
            this.simpleButton3.Name = "simpleButton3";
            this.simpleButton3.Size = new System.Drawing.Size(107, 23);
            this.simpleButton3.TabIndex = 1;
            this.simpleButton3.Text = "测试检验数据";
            this.simpleButton3.Click += new System.EventHandler(this.simpleButton3_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(44, 208);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(170, 23);
            this.button1.TabIndex = 2;
            this.button1.Text = "测试DataTablez转Json";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // TestInterFace
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(267, 261);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.simpleButton3);
            this.Controls.Add(this.simpleButton2);
            this.Controls.Add(this.simpleButton1);
            this.Name = "TestInterFace";
            this.Text = "TestInterFace";
            this.ResumeLayout(false);

        }

        #endregion

        private Hemo.Client.Controls.DXSimpleButton simpleButton1;
        private Hemo.Client.Controls.DXSimpleButton simpleButton2;
        private Hemo.Client.Controls.DXSimpleButton simpleButton3;
        private System.Windows.Forms.Button button1;
    }
}