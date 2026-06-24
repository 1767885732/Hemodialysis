/*----------------------------------------------------------------
// Copyright (C) 2005 苏州XX公司有限公司
// 描述：实验室检查用户控件
// 创建时间：2014-04-17
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

namespace Hemo.Client.UI.DataReport.LabInfos
{
    public partial class LabAndCheckInfo : ViewBase
    {
        #region 类变量

        private ViewBase itemForm = null;

        #endregion

        #region 属性

        public DataReportModel.MED_PATIENTSRow _currentPatientRow { get; set; }

        #endregion

        #region 构造函数

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
            btnSearch_Click(null, null);
        }

        private void treeViewInfo_MouseDown(object sender, MouseEventArgs e)
        {

            #region 获取点击节点的信息
            TreeListHitInfo hi = treeViewInfo.CalcHitInfo(e.Location);
            TreeListNode CurrentNode = hi.Node;
            string currentValue = string.Empty;
            if (CurrentNode != null)
            {
                currentValue = CurrentNode.GetValue(LabAndCheckColumn).ToString();
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
                if (currentValue.Equals("实验室检查"))
                {
                    itemForm = new Item_Lab(this.ctlUserLongInfo1.PatientID, this.ctlUserLongInfo1.HEMODIALYSIS_ID, this.cmbSTART_DATE.DateTime, this.cmbEND_DATE.DateTime);
                    itemForm.Dock = DockStyle.Fill;
                    this.panelContainers.Controls.Add(itemForm);
                }
                else if (currentValue.Equals("辅助检查"))
                {
                    //itemForm = new Item_Check(this.ctlUserLongInfo1.HEMODIALYSIS_ID);
                    //itemForm.Dock = DockStyle.Fill;
                    //this.panelContainers.Controls.Add(itemForm);
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

        private void btnSearch_Click(object sender, EventArgs e)
        {
            if (this.panelContainers.Controls.Count > 0)
            {
                itemForm.Query(this.cmbSTART_DATE.DateTime, this.cmbEND_DATE.DateTime);
            }
        }

        #endregion

        #region 方法

        public LabAndCheckInfo(DataReportModel.MED_PATIENTSRow CurrentPatient)
        {
            InitializeComponent();
            _currentPatientRow = CurrentPatient;
            this.ctlUserLongInfo1.HEMODIALYSIS_ID = CurrentPatient.HEMODIALYSIS_ID;
            this.ctlUserLongInfo1.LoadPatientInfo();
            DateTime dt = DateTime.Now;  //当前时间  
            DateTime startQuarter = dt.AddMonths(0 - (dt.Month - 1) % 3).AddDays(1 - dt.Day);  //本季度初 

            DateTime endQuarter = startQuarter.AddMonths(3).AddDays(-1);  //本季度末  
            this.cmbSTART_DATE.DateTime = startQuarter.Date;
            this.cmbEND_DATE.DateTime = endQuarter.Date;
        }

        #endregion
    }
}
