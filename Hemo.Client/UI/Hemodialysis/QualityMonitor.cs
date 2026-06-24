using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Hemo.Client.UI.Machine;

namespace Hemo.Client.UI.Hemodialysis
{
    public partial class QualityMonitor : ViewBase
    {
        public QualityMonitor()
        {
            InitializeComponent();
        }

        private void labelControl1_Click(object sender, EventArgs e)
        {


            if (layoutControlItem2.Visibility == DevExpress.XtraLayout.Utils.LayoutVisibility.Always)
            {
                layoutControlItem2.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                this.labelControl1.Appearance.Image = global::Hemo.Client.Properties.Resources.arrowTop;
            }
            else
            {
                layoutControlItem2.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                this.labelControl1.Appearance.Image = global::Hemo.Client.Properties.Resources.arrowBottom;
            }

        }
    }
}
