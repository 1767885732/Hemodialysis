using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Hemo.Client.UI.Patient
{
    public partial class MessageBoxFrm : HemoBaseFrm
    {
        public MessageBoxFrm()
        {
            InitializeComponent();
            this.btnOk.Enabled = false;
        }

        public void SetCaption(string caption, string text)
        {
            if (text.Equals("警告"))
            {
                this.labelControl1.ForeColor = Color.Red;
            }
            this.Text = text;
            this.labelControl1.Text = caption;
            this.labelControl1.Focus();
            this.btnOk.Enabled = true;
    
        }

        private void btnOk_Click(object sender, EventArgs e)
        {

        }

    }
}
