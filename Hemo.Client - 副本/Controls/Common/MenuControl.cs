/*----------------------------------------------------------------
 * Copyright (C) 2005 医疗科技发展有限公司
 * 文件功能描述:菜单
 * 创建标识:吕志强-2014年8月2日
 * ----------------------------------------------------------------*/

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
using DevExpress.XtraBars.Navigation;

namespace Hemo.Client.Controls.Common
{
    public partial class MenuControl : DevExpress.XtraEditors.XtraUserControl
    {
        public MenuControl()
        {
            InitializeComponent();
        }

        private void MenuControl_Load(object sender, EventArgs e)
        {
            AccordionControlElement accordionControlElement = new AccordionControlElement();
            accordionControlElement.Text = "New";
            menuCtl.Elements.Add(accordionControlElement);            
        }

        private void InitialMenu()
        {
 
        }

        private void GetMenus()
        {
            
        }

    }
}
