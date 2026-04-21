/*----------------------------------------------------------------
// Copyright (C) 2005 苏州XX公司
// 描述：患者拍照窗体
// 创建时间：2017-05-22
// 创建者：贺建操
//  
// 修改时间：
// 修改人：
// 修改描述：
//
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
    public partial class PatientPictureAction : HemoBaseFrm
    {
        #region 类变量

        public Image PatientPicture = null;

        #endregion

        #region 属性

        #endregion

        #region 构造函数

        public PatientPictureAction()
        {
            InitializeComponent();
        }

        #endregion

        #region 事件

        private void btnCaptiure_Click(object sender, EventArgs e)
        {
            getPatientPic1.PcitureAction();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            PatientPicture = getPatientPic1.PatientPic;
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
        }
        public void SetPicturePriwView(Image image)
        {
            getPatientPic1.SetPrewViewImage(image);
        }
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;

        }

        private void getPatientPic1_Load(object sender, EventArgs e)
        {

        }

        #endregion
    }
}

