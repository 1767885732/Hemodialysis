using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Hemo.Client.UI.Assessment
{
    public partial class frmEventInfoManager : Form
    {
        public UserControl currentUI { get; set; }
        public frmEventInfoManager()
        {
            InitializeComponent();
        }

        private void frmEventInfoManager_Load(object sender, EventArgs e)
        {
            //if (currentUI.Name.Equals("HemoEventManager"))
            //{
            //    currentUI = new HemoEventManager();
            //}
            //else
            //{
            //    currentUI = new UserControl();
            //}
            currentUI.Dock = DockStyle.Fill;
            this.Controls.Add(currentUI);
        }




    }
}
