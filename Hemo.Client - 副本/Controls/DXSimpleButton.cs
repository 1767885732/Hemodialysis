/*----------------------------------------------------------------
// Copyright (C) 2013 苏州XX公司
// 描述：自定义按钮，用于系统统一按钮样式
// 创建时间：2015-08-21
// 创建者：吕志强
//  
// 修改时间：
// 修改人：
// 修改描述：
//
----------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Hemo.Utilities;

namespace Hemo.Client.Controls
{
    public class DXSimpleButton : DevExpress.XtraEditors.SimpleButton
    {
        private DevExpress.Utils.ImageCollection btnImageList;
        private System.ComponentModel.IContainer components;
      
        /// <summary>
        /// 添加他的图片内容，以便使用时直接选择
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DXSimpleButton));
            this.btnImageList = new DevExpress.Utils.ImageCollection();
            ((System.ComponentModel.ISupportInitialize)(this.btnImageList)).BeginInit();
            this.SuspendLayout();
            // 
            // btnImageList
            // 
            this.btnImageList.ImageStream = ((DevExpress.Utils.ImageCollectionStreamer)(resources.GetObject("btnImageList.ImageStream")));
            this.btnImageList.Images.SetKeyName(0, "INew.png");
            this.btnImageList.Images.SetKeyName(1, "IDelete.png");
            this.btnImageList.Images.SetKeyName(2, "IEdit.png");
            this.btnImageList.Images.SetKeyName(3, "IExit.png");
            this.btnImageList.Images.SetKeyName(4, "IPTExcel.png");
            this.btnImageList.Images.SetKeyName(5, "IPreview.png");
            this.btnImageList.Images.SetKeyName(6, "IPrint.png");
            this.btnImageList.Images.SetKeyName(7, "ISave.png");
            this.btnImageList.Images.SetKeyName(8, "IQuery.png");
            this.btnImageList.Images.SetKeyName(9, "IExport.png");
            this.btnImageList.Images.SetKeyName(10, "IRefresh.png");
            this.btnImageList.Images.SetKeyName(11, "ICheck.png");
            this.btnImageList.Images.SetKeyName(12, "IComb.png");
            this.btnImageList.Images.SetKeyName(13, "IBack.png");
            this.btnImageList.Images.SetKeyName(14, "ICopy.png");
            this.btnImageList.Images.SetKeyName(15, "ILast.png");
            this.btnImageList.Images.SetKeyName(16, "INext.png");
            this.btnImageList.Images.SetKeyName(17, "IPicture.png");
            this.btnImageList.Images.SetKeyName(18, "IStop.png");
            this.btnImageList.Images.SetKeyName(19, "IUpdate.png");
            this.btnImageList.Images.SetKeyName(20, "AllPatient.png");
            this.btnImageList.Images.SetKeyName(21, "CardManager.png");
            this.btnImageList.Images.SetKeyName(22, "EditBtn.png");
            this.btnImageList.Images.SetKeyName(23, "QuekRecipe.png");
            this.btnImageList.Images.SetKeyName(24, "Lock.png");
            this.btnImageList.Images.SetKeyName(25, "recycle.png");
            this.btnImageList.Images.SetKeyName(26, "UnLock.png");
            // 
            // DXSimpleButton
            // 
            this.ImageIndex = 0;
            this.ImageList = this.btnImageList;
            ((System.ComponentModel.ISupportInitialize)(this.btnImageList)).EndInit();
            this.ResumeLayout(false);

        }


        public DXSimpleButton() 
        {
            InitializeComponent();         
        }
        /// <summary>
        /// 释放内丰
        /// </summary>
        /// <param name="disposing"></param>
        protected override void Dispose(bool disposing)
        {
            this.btnImageList.Clear();
            this.btnImageList.Dispose();
            if (disposing && (components != null))
            {
                components.Dispose();

            }
            base.Dispose(disposing);
        }


      
    }
}
