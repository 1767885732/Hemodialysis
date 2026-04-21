/*----------------------------------------------------------------
// Copyright (C) 2013 苏州XX公司有限公司
// 描述：治疗单窗体 
// 创建时间：2013-04-02
// 创建者：血透操作指导
//  
// 修改时间：
// 修改人：
// 修改描述：
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
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace Hemo.Client.UI.Hemodialysis {
    public partial class ShowHemoGuide :HemoBaseFrm {
        /// <summary>
        /// 构造函数
        /// </summary>
        public ShowHemoGuide() {
            InitializeComponent();
        }
        #region 事件
        private void treCatalog_FocusedNodeChanged(object sender, DevExpress.XtraTreeList.FocusedNodeChangedEventArgs e) {
            if (e.Node != null) {
                string strTitle = e.Node.GetDisplayText(treColumn).ToString();
                StringBuilder sbContent = new StringBuilder();
                // MessageBox.Show(e.Node.GetDisplayText(treColumn).ToString());
                switch (strTitle) {
                    case "透析中低血压":
                        sbContent.Append("透析中低血压是指透析中收缩压下降＞20mmHg 或平均动脉压降低10mmHg 以上，并有低血压症状。其处理程序如下：");
                        sbContent.AppendLine("1、紧急处理：对有症状的透析中低血压应立即采取措施处理。");
                        sbContent.AppendLine("（1）采取头低位。");
                        sbContent.Append("（2）停止超滤。\r\n");
                        sbContent.AppendLine("（3）补充生理盐水100ml，或20%甘露醇、或白蛋白溶液等。");
                        sbContent.AppendLine("（4）上述处理后，如血压好转，则逐步恢复超滤，期间仍应密切监测血压变化；如血压无好转，应再次予以补充生理盐水等扩容治疗，减慢血流速度，并立即寻找原因，对可纠正诱因进行干预。如上述处理后血压仍快速降低，则需应用升压药物治疗，并停止血透，必要时可以转换治疗模式，如单纯超滤、血液滤过或腹膜透析。其中最常采用的技术是单纯超滤与透析治疗结合的序贯治疗。");

                        break;
                }
                txtContent.Text = sbContent.ToString();
            }
        }

        private void simpleButton1_Click(object sender, EventArgs e) {
            MessageBox.Show(System.DateTime.Now.ToString());
        }
        #endregion
    }
}