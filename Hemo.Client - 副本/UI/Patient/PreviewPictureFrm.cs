/*----------------------------------------------------------------
// Copyright (C) 2005 苏州XX公司有限公司
// 描述：患者治疗对应窗体类
// 创建时间：2016-06-05
// 创建者：贺建操
//  
// 修改时间：
// 修改人：
// 修改描述：
----------------------------------------------------------------*/

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
    public partial class PreviewPictureFrm : HemoBaseFrm
    {
        #region 类变量

        #endregion

        #region 属性

        #endregion

        #region 构造函数

        public PreviewPictureFrm()
        {
            InitializeComponent();
        }

        #endregion

        #region 事件

        private void PreviewPictureFrm_Load(object sender, EventArgs e)
        {

        }

        private void picPreview_DoubleClick(object sender, EventArgs e)
        {
            this.Close();
        }

        #endregion

        #region 方法

        public void DisplayImage(Image image)
        {
            picPreview.Image = image;
        }

        #endregion
    }
}
