/*----------------------------------------------------------------
// Copyright (C) 2005 苏州XX公司有限公司
// 描述：患者终止透析登记模板
// 创建时间：2015-07-25
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
using Hemo.Model;
using Hemo.Client.Controls;
using DevExpress.XtraTreeList;
using Hemo.IService.Config;
using Hemo.Service;
using DevExpress.XtraTreeList.Nodes;
using Hemo.Client.Core;
using Hemo.IService;

namespace Hemo.Client.UI.Patient {
    public partial class TemplateOfDealthFrm : HemoBaseFrm
    {
        #region 类变量

        private PatientModel.MED_PATIENTREGDEALTHDataTable _data = new PatientModel.MED_PATIENTREGDEALTHDataTable();

        private IPatient patientService = ServiceManager.Instance.PatientService;

        #endregion

        #region 属性

        public bool HasDirty { get; set; }

        /// <summary>
        /// 获取选择的数据
        /// </summary>
        public PatientModel.MED_PATIENTREGDEALTHRow SelectedDate { get; set; }

        #endregion

        #region 构造函数

        public TemplateOfDealthFrm()
        {
            InitializeComponent();
        }

        #endregion

        #region 事件

        private void TemplateOfDealthFrm_Load(object sender, EventArgs e)
        {
            if (!DesignMode)
            {
                BindData();
            }
        }

        /// <summary>
        /// 右键菜单
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void treeList_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Right || e.Clicks != 1)
                return;

            TreeListHitInfo hitInfo = this.treeList.CalcHitInfo(e.Location);
            if (hitInfo == null || hitInfo.Node == null)
                return;

            this.treeList.FocusedNode = hitInfo.Node;
            var row = GetFocusedRow();


            contextMenuStrip.Visible = true;
            contextMenuStrip.Left = MousePosition.X;
            contextMenuStrip.Top = MousePosition.Y;


            this.iNewCategory.Visible = Convert.ToBoolean(row.EXTEND1);
            this.iNew.Visible = Convert.ToBoolean(row.EXTEND1);
            this.iDelete.Visible = true;
            this.iReName.Visible = true;

        }
        /// <summary>
        /// 选择改变事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void treeList_FocusedNodeChanged(object sender, FocusedNodeChangedEventArgs e)
        {
            if (e.Node == null)
                return;

            this.treeList.FocusedNode = e.Node;

            var row = GetFocusedRow();

            //显示内容
            DisplayContent(row.EVENTANALYSIS, row.CORRECTIVEACTIONS);

            this.HasDirty = false;

            this.btnSave.Enabled = !Convert.ToBoolean(row.EXTEND1);
            this.txtEVENTANALYSIS.Enabled = !Convert.ToBoolean(row.EXTEND1);
            this.txtCORRECTIVEACTIONS.Enabled = !Convert.ToBoolean(row.EXTEND1);
            //this.treeList.OptionsBehavior.DragNodes = true;



        }
        /// <summary>
        /// 选择更改前事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void treeList_BeforeFocusNode(object sender, BeforeFocusNodeEventArgs e)
        {
            if (this.HasDirty)
            {
                if (XtraMessageBox.Show("是否保存修改内容?", "提示信息", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    this.btnSave.PerformClick();
                }
            }

        }
        /// <summary>
        /// 双击选择行
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void treeList_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            var row = GetFocusedRow();
            if (!Convert.ToBoolean(row.EXTEND1))
                this.btnInsert.PerformClick();

        }
        /// <summary>
        ///显示Label信息
        /// </summary>
        /// <param name="name"></param>
        /// <param name="office"></param>
        /// <param name="cateory"></param>
        /// <param name="lasyUpdateBy"></param>
        /// <param name="lastUpdateTime"></param>
        /// <param name="term"></param>
        private void DisplayContent(string EVENTANALYSIS, string CORRECTIVEACTIONS)
        {
            this.txtEVENTANALYSIS.Text = EVENTANALYSIS;
            this.txtCORRECTIVEACTIONS.Text = CORRECTIVEACTIONS;
        }
        /// <summary>
        /// 新建分类
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void iNewCategory_Click(object sender, EventArgs e)
        {
            using (var form = new NameForm())
            {
                form.Text = "新建分类";
                if (form.ShowDialog() == DialogResult.OK)
                {
                    AddChildNode(form.EditValue, true);
                }
            }
        }
        /// <summary>
        /// 新建术语
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void iNew_Click(object sender, EventArgs e)
        {
            using (var form = new NameForm())
            {
                form.Text = "新建名称";
                if (form.ShowDialog() == DialogResult.OK)
                {
                    AddChildNode(form.EditValue, false);
                }
            }

        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void iDelete_Click(object sender, EventArgs e)
        {
            if (this.treeList.FocusedNode == null)
                return;

            var row = GetFocusedRow();


            if (XtraMessageBox.Show(string.Format("是否删除「{0}」{1}?", row.EXTEND2, Convert.ToBoolean(row.EXTEND1) ? "文件夹下的所有数据" : string.Empty), "提示信息", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                var id = new List<string>();
                id.Add(row.ID);
                //分类
                if (Convert.ToBoolean(row.EXTEND1))
                    MoveCategoryItems(row, id);

                patientService.DeleteTemplate(id.ToArray());

                this.HasDirty = false;
                if (this.treeList.FocusedNode.ParentNode != null)
                    this.treeList.FocusedNode.ParentNode.Nodes.Remove(this.treeList.FocusedNode);
                id.Clear();

                _data.AcceptChanges();
            }
        }
        /// <summary>
        /// 重命名
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void iReName_Click(object sender, EventArgs e)
        {
            if (this.treeList.FocusedNode == null)
                return;
            var current = GetFocusedRow();


            using (var form = new NameForm())
            {
                form.Text = "重命名";
                form.EditValue = current.EXTEND2;
                if (form.ShowDialog() == DialogResult.OK)
                {
                    current.EXTEND2 = form.EditValue;
                    patientService.UpdateTemplate(current.ID, current.EXTEND2);
                }
            }
        }
        /// <summary>
        /// 执行创建子模版的方法
        /// </summary>
        private void AddChildNode(string text, bool isCategory)
        {
            if (this.treeList.FocusedNode == null)
            {
                XtraMessageBox.Show("请选择一个节点", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                var parent = GetFocusedRow();

                var row = _data.NewMED_PATIENTREGDEALTHRow();
                row.ID = Guid.NewGuid().ToString();
                row.HEMODIALYSIS_ID = parent.ID;
                row.EXTEND2 = text;
                row.EXTEND1 = isCategory.ToString();
                row.TYPE = "1";
                row.CREATEBY = HemoApplicationContext.Current.CurrentUser.USER_ID;
                row.CREATEDATE = DateTime.Now;
                patientService.InsertTemplate(row.ID, row.HEMODIALYSIS_ID, row.EXTEND1, row.EXTEND2, row.TYPE, row.CREATEBY, row.CREATEDATE);
                this.treeList.FocusedNode = this.treeList.AppendNode(row, this.treeList.FocusedNode);

            }
        }

        /// <summary>
        /// 编辑更改事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void iTermTextEdit_EditValueChanging(object sender, DevExpress.XtraEditors.Controls.ChangingEventArgs e)
        {
            this.HasDirty = true;
            this.btnSave.Enabled = true;
            this.treeList.OptionsBehavior.DragNodes = false;

        }
        /// <summary>
        /// 关闭
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void iCloseButton_Click(object sender, EventArgs e)
        {
            this.SelectedDate = null;
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;

        }
        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void iSaveButton_Click(object sender, EventArgs e)
        {
            if (this.treeList.FocusedNode == null)
                return;
            //保存模式
            var row = GetFocusedRow();
            row.EVENTANALYSIS = this.txtEVENTANALYSIS.Text.Trim();
            row.CORRECTIVEACTIONS = this.txtCORRECTIVEACTIONS.Text.Trim();
            patientService.UpdateTemplate(row.ID, row.EVENTANALYSIS, row.CORRECTIVEACTIONS);
            row.AcceptChanges();
            _data.AcceptChanges();
            this.HasDirty = false;
            this.btnSave.Enabled = false;
            //this.treeList.OptionsBehavior.DragNodes = true;
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
        }

        private void btnInsert_Click(object sender, EventArgs e)
        {
            if (this.treeList.FocusedNode == null)
                return;
            //保存模式
            var row = GetFocusedRow();
            this.SelectedDate = row;

            this.DialogResult = System.Windows.Forms.DialogResult.OK;

        }

        private void txtEVENTANALYSIS_TextChanged(object sender, EventArgs e)
        {
            this.btnSave.Enabled = true;
        }

        /// <summary> 
        /// 设置TreeList显示的图标 
        /// </summary> 
        /// <param name="tl">TreeList组件</param> 
        /// <param name="node">当前结点，从根结构递归时此值必须=null</param> 
        /// <param name="nodeIndex">根结点图标(无子结点)</param> 
        /// <param name="parentIndex">有子结点的图标</param> 
        public static void SetImageIndex(TreeList tl, TreeListNode node, int nodeIndex, int parentIndex)
        {
            if (node == null)
            {
                foreach (TreeListNode N in tl.Nodes)
                    SetImageIndex(tl, N, nodeIndex, parentIndex);
            }
            else
            {
                if (node.HasChildren || node.ParentNode == null)
                {
                    //node.SelectImageIndex = parentIndex; 
                    node.StateImageIndex = parentIndex;
                    node.ImageIndex = parentIndex;
                }
                else
                {
                    //node.SelectImageIndex = nodeIndex; 
                    node.StateImageIndex = nodeIndex;
                    node.ImageIndex = nodeIndex;
                }

                foreach (TreeListNode N in node.Nodes)
                {
                    SetImageIndex(tl, N, nodeIndex, parentIndex);
                }
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void treeList_CustomDrawNodeImages(object sender, DevExpress.XtraTreeList.CustomDrawNodeImagesEventArgs e)
        {

            if (e.Node.Nodes.Count > 0)
            {
                if (e.Node.Expanded)
                {
                    e.SelectImageIndex = 1;
                    return;
                }
                e.SelectImageIndex = 0;
            }
            else
            {
                var row = GetDataRow(e.Node);

                if (Convert.ToBoolean(row.EXTEND1))
                {
                    e.SelectImageIndex = 0;
                }
                else
                {
                    e.SelectImageIndex = 2;

                }
            }
        }

        #endregion

        #region 方法

        /// <summary>
        /// 绑定数据
        /// </summary>
        public void BindData()
        {
            var busyIndicator = new BusyIndicator();
            busyIndicator.ShowLoadingScreenFor(this.treeList);

            this.treeList.Enabled = false;
            using (BackgroundWorker worker = new BackgroundWorker())
            {
                worker.DoWork += delegate(object sender1, DoWorkEventArgs e1)
                {
                    _data = patientService.GetPatientRegDealthTemplate();
                };
                worker.RunWorkerCompleted += delegate(object sender2, RunWorkerCompletedEventArgs e2)
                {
                    this.treeList.DataSource = _data;
                    busyIndicator.Hide();
                    SetImageIndex(treeList, null, 2, 0);
                    this.treeList.ExpandAll();
                    this.treeList.Enabled = true;
                };
                worker.RunWorkerAsync();
            }
        }

        /// <summary>
        /// 获取当前的数据行
        /// </summary>
        /// <returns></returns>
        private PatientModel.MED_PATIENTREGDEALTHRow GetFocusedRow()
        {
            return GetDataRow(this.treeList.FocusedNode);
        }
        /// <summary>
        /// 获取数据行
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        private PatientModel.MED_PATIENTREGDEALTHRow GetDataRow(TreeListNode node)
        {
            var record = this.treeList.GetDataRecordByNode(node);
            if (record is DataRowView)
            {
                var focus = record as DataRowView;
                return focus.Row as PatientModel.MED_PATIENTREGDEALTHRow;
            }
            else
            {
                return record as PatientModel.MED_PATIENTREGDEALTHRow;
            }
        }

        /// <summary>
        /// 分组项位置
        /// </summary>
        /// <param name="items"></param>
        private void MoveCategoryItems(PatientModel.MED_PATIENTREGDEALTHRow category, List<string> childCategoryId)
        {
            var children = _data.Where(i => i.HEMODIALYSIS_ID == category.ID);
            foreach (var c in children)
            {
                childCategoryId.Add(c.ID);

                if (Convert.ToBoolean(c.EXTEND1))
                {
                    MoveCategoryItems(c, childCategoryId);
                }

            }
        }

        #endregion
    }
}