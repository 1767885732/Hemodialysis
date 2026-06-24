/*----------------------------------------------------------------
// Copyright (C) 2013 苏州XX公司有限公司
// 描述：ICD-10 病种选择窗体 
// 创建时间：2013-09-18
// 创建者：刘超
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
using Hemo.Service;
using Hemo.IService.Config;
using Hemo.Model;
using DevExpress.XtraTreeList.Nodes;

namespace Hemo.Client.UI.Hemodialysis {
    public partial class ShowICDList :HemoBaseFrm {

        #region 私有成员
        /// <summary>
        /// 处方表
        /// </summary>
        private IHemodialysis objHemodialysisService = ServiceManager.Instance.HemodialysisService;
        private Hemo.Client.UI.Patient.EditPatientNew patientFrm;
        #endregion

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="pIcd"></param>
        public ShowICDList(string pIcd) {
            InitializeComponent();
        }

        /// <summary>
        /// 选择的ICD病种
        /// </summary>
        private string _icdList = string.Empty;
        public string IcdList {
            get {
                return _icdList;
            }
            set {
                _icdList = value;
            }
        }

        #region 事件
        private void loadTreeList()
        {
            TreeListNode parentNode = null;
            TreeListNode childNode = null;
            TreeListNode grandsonNode = null;
            TreeListNode great_grandsonsNode = null;

            HemodialysisModel.MED_ICD_TYPEDataTable icdType = new HemodialysisModel.MED_ICD_TYPEDataTable();
            icdType = objHemodialysisService.GetIcdType();

            treIcdType.BeginUnboundLoad();
            treIcdType.ClearNodes();
            DataTable dt = icdType;  //整个ICD分类表
            DataTable dtFirst = Utilities.Utility.GetSubTable(dt, "parent_id='1'");//第一级疾病分类
            DataView treeDv = dtFirst.DefaultView;
            DataTable parentDt = null;
            DataTable childDt = null;
            DataTable grandsonDt = null;
            DataTable great_grandsonsDt = null;

            parentDt = treeDv.ToTable();
            foreach (DataRow parentDr in parentDt.Rows)
            {
                TreeListNode treelistnode = null;

                parentNode = this.treIcdType.AppendNode(new object[] { parentDr["TYPE_NAME"], parentDr["PARENT_ID"], parentDr["ID"], parentDr["TYPE_CODE"] }, treelistnode);

                //加载第二级数据
                childDt = Utilities.Utility.GetSubTable(dt, "PARENT_ID='" + parentDr["ID"] + "'");
                foreach (DataRow childDr in childDt.Rows)
                {
                    childNode = this.treIcdType.AppendNode(new object[] { childDr["TYPE_NAME"], childDr["PARENT_ID"], childDr["ID"], childDr["TYPE_CODE"] }, parentNode);

                    //加载第三级数据
                    grandsonDt = Utilities.Utility.GetSubTable(dt, "PARENT_ID='" + childDr["ID"] + "'");

                    foreach (DataRow grandsonDr in grandsonDt.Rows)
                    {
                        grandsonNode = this.treIcdType.AppendNode(new object[] { grandsonDr["TYPE_NAME"], grandsonDr["PARENT_ID"], grandsonDr["ID"], grandsonDr["TYPE_CODE"] }, childNode);
                        //加载第四级数据
                        great_grandsonsDt = Utilities.Utility.GetSubTable(dt, "PARENT_ID='" + grandsonDr["ID"] + "'");
                        foreach (DataRow great_grandsonsDr in great_grandsonsDt.Rows)
                        {
                            great_grandsonsNode = this.treIcdType.AppendNode(new object[] { great_grandsonsDr["TYPE_NAME"], great_grandsonsDr["PARENT_ID"], great_grandsonsDr["ID"], great_grandsonsDr["TYPE_CODE"] }, grandsonNode);
                        }
                    }
                }
            }
            treIcdType.EndUnboundLoad();
        }

        private void ShowICDList_Load(object sender, EventArgs e) {
            patientFrm = (Hemo.Client.UI.Patient.EditPatientNew)this.Owner;
            loadTreeList();
        }

        /// <summary>
        /// 根据ICD类别ID或类别ID跨度，取出对应的病种信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void treIcdType_FocusedNodeChanged(object sender, DevExpress.XtraTreeList.FocusedNodeChangedEventArgs e) {
            if (this.treIcdType.FocusedNode != null) {
                //MessageBox.Show(this.treIcdType.FocusedNode.GetValue("TYPE_CODE").ToString());
                DataTable result = new DataTable();
                string icdList = this.treIcdType.FocusedNode.GetValue("TYPE_CODE").ToString();
                if (icdList.IndexOf("-") > -1 && icdList != "A00-B99") {
                    result = objHemodialysisService.GetIcdListByIDList(icdList);
                }
                else {
                    result = (DataTable)objHemodialysisService.GetIcdListByID(icdList);
                }
                if (result != null && result.Rows.Count > 0) {
                    result.Columns.Add("CHOOSE", System.Type.GetType("System.Boolean"));
                    this.grdIcdList.DataSource = result;
                }
                else {
                    this.grdIcdList.DataSource = null;
                }
            }
        }

        private void btnClose_Click(object sender, EventArgs e) {
            this.Close();
            //int i = objHemodialysisService.UpdateIcdPinYin();
            //MessageBox.Show(i.ToString());
        }

        private void btnOK_Click(object sender, EventArgs e) {
            string value = "";
            string strSelected = "";
            for (int i = 0; i < gridView2.RowCount; i++) {
                value = gridView2.GetDataRow(i)["CHOOSE"].ToString();
                if (value == "True") {
                    strSelected += gridView2.GetRowCellValue(i, "ICD_NAME") + ",";
                }
            }
            IcdList = lblIcdList.Text = strSelected;
            // ((Hemo.Client.UI.Patient.EditPatient)this.Owner).IcdList = strSelected;
            this.Close();
            //MessageBox.Show(strSelected);
        }

        private void btnQuery_Click(object sender, EventArgs e) {
            if (txtIcdName.Text.Trim().Length > 0) 
            {
                HemodialysisModel.MED_ICD_LISTDataTable result = new HemodialysisModel.MED_ICD_LISTDataTable();
                result = objHemodialysisService.GetIcdListByName(txtIcdName.Text);
                if (result != null && result.Rows.Count > 0) {
                    result.Columns.Add("CHOOSE", System.Type.GetType("System.Boolean"));
                    this.grdIcdList.DataSource = result;
                }
                else {
                    this.grdIcdList.DataSource = null;
                }
            }
        }

        private void txtIcdName_MouseDown(object sender, MouseEventArgs e)
        {
            
        }

        private void txtIcdName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (txtIcdName.Text.Trim().Length > 0)
                {
                    HemodialysisModel.MED_ICD_LISTDataTable result = new HemodialysisModel.MED_ICD_LISTDataTable();
                    result = objHemodialysisService.GetIcdListByName(txtIcdName.Text);
                    if (result != null && result.Rows.Count > 0)
                    {
                        result.Columns.Add("CHOOSE", System.Type.GetType("System.Boolean"));
                        this.grdIcdList.DataSource = result;
                    }
                    else
                    {
                        this.grdIcdList.DataSource = null;
                    }
                }
            }
        }
        #endregion
    }
}