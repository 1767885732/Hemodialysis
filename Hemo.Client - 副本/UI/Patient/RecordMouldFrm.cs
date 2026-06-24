/*----------------------------------------------------------------
// Copyright (C) 2005 苏州XX公司有限公司
// 描述：病历模板窗体类
// 创建时间：2016-03-05
// 创建者：刘超
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
using Hemo.IService;
using Hemo.Service;
using Hemo.Model;
using DevExpress.XtraTreeList;
using DevExpress.XtraTreeList.Nodes;

namespace Hemo.Client.UI.Patient
{
    public partial class RecordMouldFrm :HemoBaseFrm
    {
        #region 类变量

        TreeListNode node = null;
        private string _pastliness;
        private string _presentliness;
        private string _action;
        private IPatient objPatient = ServiceManager.Instance.PatientService;
        private PatientScheduleModel.MED_RECORDMOULDDataTable data = new PatientScheduleModel.MED_RECORDMOULDDataTable();

        #endregion

        #region 属性

        public string Action
        {
            get { return _action; }
            set { _action = value; }
        }

        public string Presentliness
        {
            get { return _presentliness; }
            set { _presentliness = value; }
        }

        public string Pastliness
        {
            get { return _pastliness; }
            set { _pastliness = value; }
        }

        #endregion

        #region 构造函数

        public RecordMouldFrm()
        {
            InitializeComponent();
        }

        #endregion

        #region 事件

        private void RecordMouldFrm_Load(object sender, EventArgs e)
        {
            BoundTreeList();
        }

        private void treRecordMouldList_FocusedNodeChanged(object sender, DevExpress.XtraTreeList.FocusedNodeChangedEventArgs e)
        {
            if (this.treRecordMouldList.FocusedNode != null)
            {
                this.memoEdit_Top.Text = string.Format("          {0}", this.treRecordMouldList.FocusedNode.GetValue("ACTION").ToString());
                this.memoEdit_Fill.Text = string.Format("             {0}", this.treRecordMouldList.FocusedNode.GetValue("PRESENTILLNESS").ToString());
                this.memoEdit_bottom.Text = string.Format("             {0}", this.treRecordMouldList.FocusedNode.GetValue("PASTILLNESS").ToString());
            }
        }
        
        private void treRecordMouldList_DoubleClick(object sender, EventArgs e)
        {
            var me = e as MouseEventArgs;
            TreeListHitInfo hitInfo = (sender as TreeList).CalcHitInfo(new Point(me.X, me.Y));
            //TreeListNode node = hitInfo.Node;
            node = hitInfo.Node;
            this.treRecordMouldList.SetFocusedNode(node);
            if (node != null)
            {
                _action = this.treRecordMouldList.FocusedNode.GetValue("ACTION").ToString();
                _presentliness = this.treRecordMouldList.FocusedNode.GetValue("PRESENTILLNESS").ToString();
                _pastliness = this.treRecordMouldList.FocusedNode.GetValue("PASTILLNESS").ToString();
                this.DialogResult = System.Windows.Forms.DialogResult.OK;
            }
        }

        private void treRecordMouldList_DockChanged(object sender, EventArgs e)
        {

        }

        #endregion

        #region 方法

        private void BoundTreeList()
        {
            data = objPatient.GetRecordMouldList();
            this.treRecordMouldList.DataSource = data;
        }

        #endregion
    }
}