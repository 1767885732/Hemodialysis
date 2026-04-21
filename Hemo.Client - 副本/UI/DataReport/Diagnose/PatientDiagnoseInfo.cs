/*----------------------------------------------------------------
// Copyright (C) 2005 苏州XX公司有限公司
// 描述：患者诊断信息上传用户控件
// 创建时间：2015-10-25
// 创建者：刘超
//  
// 修改时间：
// 修改人：
// 修改描述：
----------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Hemo.Client.UI.Machine;
using DevExpress.XtraTreeList;
using DevExpress.XtraTreeList.Nodes;
using Hemo.Model;

namespace Hemo.Client.UI.DataReport.Diagnose
{
    public partial class PatientDiagnoseInfo : ViewBase
    {
        #region 类变量

        public DataReportModel.MED_PATIENTSRow _currentPatientRow { get; set; }
        private ViewBase itemForm = null;

        #endregion

        #region 属性

        #endregion

        #region 构造函数

        public PatientDiagnoseInfo(DataReportModel.MED_PATIENTSRow CurrentPatient)
        {
            InitializeComponent();
            _currentPatientRow = CurrentPatient;
            this.ctlUserLongInfo1.HEMODIALYSIS_ID = CurrentPatient.HEMODIALYSIS_ID;
            this.ctlUserLongInfo1.LoadPatientInfo();
        }

        #endregion

        #region 事件

        private void btnUpLoad_Click(object sender, EventArgs e)
        {
            if (this.panelContainers.Controls.Count > 0)
            {
                itemForm.GetVascualToUpLoad(_currentPatientRow.BASEINFO);
            }
            else
            {
                MessageBox.Show("上传失败，无上传内容！", "提醒", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            itemForm.InzationData();

        }

        private void treeViewInfo_MouseDown(object sender, MouseEventArgs e)
        {
            #region 获取点击节点的信息
            TreeListHitInfo hi = treeViewInfo.CalcHitInfo(e.Location);
            TreeListNode CurrentNode = hi.Node;
            string currentValue = string.Empty;
            if (CurrentNode != null)
            {
                currentValue = CurrentNode.GetValue(diagnoseColumn).ToString();
            }
            else
            {
                return;
            }
            #endregion
            if (e.Button == MouseButtons.Left)//左键
            {
                this.panelContainers.Controls.Clear();
                //Do something
                if (currentValue.Equals("传染病诊断"))
                {
                    itemForm = new Item_Infection(this.ctlUserLongInfo1.HEMODIALYSIS_ID);
                    itemForm.Dock = DockStyle.Fill;
                    this.panelContainers.Controls.Add(itemForm);
                }
                else if (currentValue.Equals("转归情况"))
                {
                    itemForm = new Item_Sheft(this.ctlUserLongInfo1.HEMODIALYSIS_ID);
                    itemForm.Dock = DockStyle.Fill;
                    this.panelContainers.Controls.Add(itemForm);
                }
                else if (currentValue.Equals("原发病诊断"))
                {
                    //暂无
                }
                else if (currentValue.Equals("病理诊断"))
                {
                    //暂无

                }
                else if (currentValue.Equals("并发症诊断"))
                {
                    //暂无

                }
                else if (currentValue.Equals("肿瘤诊断"))
                {
                    //暂无

                }
                else if (currentValue.Equals("过敏诊断"))
                {
                    //暂无

                }
            }
            else if (e.Button == MouseButtons.Right)//右键
            {
                //Do something
            }
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {

        }

        #endregion
    }
}
