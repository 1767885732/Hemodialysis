/*----------------------------------------------------------------
// Copyright (C) 2005 苏州XX公司有限公司
// 描述：患者透析信息上传用户控件
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

namespace Hemo.Client.UI.DataReport.HemoInfos
{
    public partial class PatientHemoInfo : ViewBase
    {
        #region 类变量

        private ViewBase itemForm = null;

        #endregion

        #region 属性

        public DataReportModel.MED_PATIENTSRow _currentPatientRow { get; set; }

        #endregion

        #region 构造函数

        public PatientHemoInfo(DataReportModel.MED_PATIENTSRow CurrentPatient)
        {
            InitializeComponent();
            _currentPatientRow = CurrentPatient;
            this.ctlUserLongInfo1.HEMODIALYSIS_ID = CurrentPatient.HEMODIALYSIS_ID;
            this.ctlUserLongInfo1.LoadPatientInfo();
        }

        #endregion

        #region 事件

        /// <summary>
        /// 上传事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
            //上传完成后加载数据，更新列表状态
            itemForm.InzationData();

        }
        /// <summary>
        /// 选择需要上传的项目
        /// 传入项目
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void treeViewInfo_MouseDown(object sender, MouseEventArgs e)
        {
            #region 获取点击节点的信息
            TreeListHitInfo hi = treeViewInfo.CalcHitInfo(e.Location);
            TreeListNode CurrentNode = hi.Node;
            string currentValue = string.Empty;
            if (CurrentNode != null)
            {
                currentValue = CurrentNode.GetValue(hemoColumn).ToString();
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
                if (currentValue.Equals("血管通路"))
                {
                    itemForm = new Item_Vasular(this.ctlUserLongInfo1.HEMODIALYSIS_ID);
                    itemForm.Dock = DockStyle.Fill;
                    this.panelContainers.Controls.Add(itemForm);
                }
                else if (currentValue.Equals("透析处方"))
                {
                    itemForm = new Item_Recipe(this.ctlUserLongInfo1.HEMODIALYSIS_ID);
                    itemForm.Dock = DockStyle.Fill;
                    this.panelContainers.Controls.Add(itemForm);
                }
                else if (currentValue.Equals("血压测量"))
                {
                    itemForm = new Item_Blood(this.ctlUserLongInfo1.HEMODIALYSIS_ID);
                    itemForm.Dock = DockStyle.Fill;
                    this.panelContainers.Controls.Add(itemForm);
                }
                else if (currentValue.Equals("透析充分性"))
                {
                    itemForm = new Item_Sufficiency(this.ctlUserLongInfo1.HEMODIALYSIS_ID);
                    itemForm.Dock = DockStyle.Fill;
                    this.panelContainers.Controls.Add(itemForm);
                }
                else if (currentValue.Equals("抗凝剂"))
                {
                    itemForm = new Item_AnticoaGulant(this.ctlUserLongInfo1.HEMODIALYSIS_ID);
                    itemForm.Dock = DockStyle.Fill;
                    this.panelContainers.Controls.Add(itemForm);
                }
                else if (currentValue.Equals("干体重"))
                {
                    itemForm = new Item_Weight(this.ctlUserLongInfo1.HEMODIALYSIS_ID);
                    itemForm.Dock = DockStyle.Fill;
                    this.panelContainers.Controls.Add(itemForm);
                }
                else if (currentValue.Equals("合用其它透析模式"))
                {

                }
            }
            else if (e.Button == MouseButtons.Right)//右键
            {
                //Do something
            }
        }
        /// <summary>
        /// 一键上传。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnUploadAll_Click(object sender, EventArgs e)
        {
            this.panelContainers.Controls.Clear();
            itemForm = new Item_Vasular(this.ctlUserLongInfo1.HEMODIALYSIS_ID);
            itemForm.Dock = DockStyle.Fill;
            this.panelContainers.Controls.Add(itemForm);
            this.btnUpLoad_Click(null, null);
            this.panelContainers.Controls.Clear();
            itemForm = new Item_Recipe(this.ctlUserLongInfo1.HEMODIALYSIS_ID);
            itemForm.Dock = DockStyle.Fill;
            this.btnUpLoad_Click(null, null);
            this.panelContainers.Controls.Clear();

            itemForm = new Item_Blood(this.ctlUserLongInfo1.HEMODIALYSIS_ID);
            itemForm.Dock = DockStyle.Fill;
            this.panelContainers.Controls.Add(itemForm);
            this.btnUpLoad_Click(null, null);
            this.panelContainers.Controls.Clear();

            itemForm = new Item_Sufficiency(this.ctlUserLongInfo1.HEMODIALYSIS_ID);
            itemForm.Dock = DockStyle.Fill;
            this.panelContainers.Controls.Add(itemForm);
            this.btnUpLoad_Click(null, null);
            this.panelContainers.Controls.Clear();

            itemForm = new Item_AnticoaGulant(this.ctlUserLongInfo1.HEMODIALYSIS_ID);
            itemForm.Dock = DockStyle.Fill;
            this.panelContainers.Controls.Add(itemForm);
            this.btnUpLoad_Click(null, null);
            this.panelContainers.Controls.Clear();
            itemForm = new Item_Weight(this.ctlUserLongInfo1.HEMODIALYSIS_ID);
            itemForm.Dock = DockStyle.Fill;
            this.panelContainers.Controls.Add(itemForm);
            this.btnUpLoad_Click(null, null);
            this.panelContainers.Controls.Clear();

        }

        #endregion
    }
}
