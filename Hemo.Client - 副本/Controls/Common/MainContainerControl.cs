using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Hemo.Client.Modules.Config;

namespace Hemo.Client.Controls.Common
{
    public partial class MainContainerControl : DevExpress.XtraEditors.XtraUserControl
    {
        public MainContainerControl()
        {
            InitializeComponent();
            //var control = new UserControl();
            //control.Text = "Document1";
            //var lbl = new Label();
            //lbl.Text = "AAA";
            //control.Controls.Add(lbl);
            //documentManager.View.AddDocument(control);

            var control = new SystemConfig();
            documentManager.View.AddDocument(control);
        }
    }
}
