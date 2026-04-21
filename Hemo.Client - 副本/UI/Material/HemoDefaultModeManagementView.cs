/*----------------------------------------------------------------
 * Copyright (C) 2005 (苏州)医疗科技发展有限公司
 * 文件功能描述:用户控件类
 * 创建标识:吕志强-2013年7月19日
 * 
 * 修改时间:2013年10月27日
 * 修改人:刘超
 * 修改描述:新增方法SQL
 * 
 * 修改时间:2014年2月4日
 * 修改人:顾伟伟
 * 修改描述:修改方法SQL
 * 
 * 修改时间:2014年5月15日
 * 修改人:贺建操
 * 修改描述:修改方法SQL
 * ----------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Hemo.Client.Core;
using Hemo.Client.UI.Machine;
using Hemo.Model;
using Hemo.IService.Config;
using Hemo.Service;
using Hemo.IService;
using Hemo.Client.UI.Patient;

namespace Hemo.Client.UI.Material
{
    /// <summary>
    /// 维护科室患者的默认病历
    /// </summary>
    public partial class HemoDefaultModeManagementView : ViewBase
    {
        #region 变量
        private IConfig _configService = ServiceManager.Instance.ConfigService;
        private IPatient patientService = ServiceManager.Instance.PatientService;

        private MaterialScheduleModel.MED_HEMO_CURE_DEFAULT_MODEDataTable _data = null;
        #endregion

        /// <summary>
        /// 构造函数
        /// </summary>
        public HemoDefaultModeManagementView()
        {
            InitializeComponent();
        }
        #region 事件
        /// <summary>
        /// 初使化
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DefaultRecordManagementView_Load(object sender, EventArgs e)
        {
            BindOffice();
        }
        /// <summary>
        /// 绑定科室
        /// </summary>
        private void BindOffice()
        {
            ConfigModel.MED_COMMON_ITEMLISTDataTable _configDataTable = null;

            this.busyIndicator1.ShowLoadingScreenFor(this.panel1);

            using (BackgroundWorker worker = new BackgroundWorker())
            {

                worker.DoWork += delegate(object sender1, DoWorkEventArgs e1)
                {
                    _configDataTable = _configService.GetConfigList(string.Empty, string.Empty, "净化方式", string.Empty);

                };
                worker.RunWorkerCompleted += delegate(object sender2, RunWorkerCompletedEventArgs e2)
                {
                    this.officelookUpEdit.Properties.DataSource = _configDataTable;
                    this.officelookUpEdit.EditValue = _configDataTable[0].ITEM_ID;
                    this.officelookUpEdit.Enabled = true;
                };
                worker.RunWorkerAsync();
            }
        }
        /// <summary>
        /// 选择科室
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void officelookUpEdit_EditValueChanged(object sender, EventArgs e)
        {
            if (officelookUpEdit.EditValue == null)
                return;

            var itemId = officelookUpEdit.EditValue.ToString();

            SetButtonState(false);

            DataTable template = null;

            if (this.busyIndicator1.Visible == false)
                this.busyIndicator1.ShowLoadingScreenFor(this.panel1);

            using (BackgroundWorker worker = new BackgroundWorker())
            {
                _data = new MaterialScheduleModel.MED_HEMO_CURE_DEFAULT_MODEDataTable();
                worker.DoWork += delegate(object sender1, DoWorkEventArgs e1)
                {
                    _data = patientService.GetHemoDefaultModels(itemId);

                    template = patientService.QueryModelByParams();

                };
                worker.RunWorkerCompleted += delegate(object sender2, RunWorkerCompletedEventArgs e2)
                {
                    _data.AcceptChanges();
                    this.bindingSource1.DataSource = _data;
                    this.treeList1.DataSource = template;
                    this.busyIndicator1.HideLoadingScreen();
                    this.HasDirty = false;
                    this.iSaveButton.Enabled = false;
                    this.iMoveRightButton.Enabled = this.gridView1.FocusedRowHandle >= 0;
                    this.iMoveLeftButton.Enabled = this.treeList1.FocusedNode != null && this.treeList1.FocusedNode.Nodes.Count == 0;
                };
                worker.RunWorkerAsync();
            }
        }
        /// <summary>
        /// 关闭界面
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void iCloseButton_Click(object sender, EventArgs e)
        {
            base.CloseView();
        }
        /// <summary>
        /// 设置按纽的状态
        /// </summary>
        /// <param name="enable"></param>
        private void SetButtonState(bool enable)
        {
            this.iSaveButton.Enabled = enable;
            this.iMoveLeftButton.Enabled = enable;
            this.iMoveRightButton.Enabled = enable;
            this.iMoveUpButton.Enabled = enable;
            this.iMoveDownButton.Enabled = enable;
        }
        /// <summary>
        /// 模版树选择改变事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void treeList1_FocusedNodeChanged(object sender, DevExpress.XtraTreeList.FocusedNodeChangedEventArgs e)
        {
            this.iMoveLeftButton.Enabled = e.Node != null && e.Node.Nodes.Count == 0;
            this.treeList1.ClearColumnErrors();

            var current = this.treeList1.GetDataRecordByNode(this.treeList1.FocusedNode) as DataRowView;
            if (current == null)
                return;
            var template = current.Row as DataRow;
            var treeNodeSeleteName = template["MATERIAL_MODEL_NAME"].ToString();
            gridControl2.DataSource = patientService.QueryMaterialModelByParams(treeNodeSeleteName);

        }

        private void treeList1_NodeChanged(object sender, DevExpress.XtraTreeList.NodeChangedEventArgs e)
        {

        }
        /// <summary>
        /// 病历表格选择改变事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            this.iMoveRightButton.Enabled = e.FocusedRowHandle >= 0;
            this.iMoveUpButton.Enabled = e.FocusedRowHandle > 0;
            this.iMoveDownButton.Enabled = e.FocusedRowHandle < this.gridView1.DataRowCount - 1 && e.FocusedRowHandle >= 0;
        }
        /// <summary>
        /// 左移
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void iMoveLeftButton_Click(object sender, EventArgs e)
        {
            var current = this.treeList1.GetDataRecordByNode(this.treeList1.FocusedNode) as DataRowView;
            if (current == null)
                return;
            var template = current.Row as DataRow;

            this.iMoveLeftButton.Enabled = false;

            if (_data.Where(i => i.RowState != DataRowState.Deleted).FirstOrDefault(i => i.MATERIAL_MODEL_ID == template["MATERIAL_MODEL_NAME"].ToString()) != null)
            {
                this.treeList1.SetColumnError(this.treeListColumn1, "列表中已存在相同的模板", DevExpress.XtraEditors.DXErrorProvider.ErrorType.Warning);
                return;
            }
            var record = _data.NewMED_HEMO_CURE_DEFAULT_MODERow();
            record.ID = Guid.NewGuid().ToString();
            record.MATERIAL_MODEL_ID = template["MATERIAL_MODEL_NAME"].ToString();
            record.MATERIAL_MODEL_NAME = template["MATERIAL_MODEL_NAME"].ToString();
            record.RELATIONID = officelookUpEdit.EditValue.ToString();
            record.RELATIONNAME = officelookUpEdit.Text.ToString();
            record.CREATED_BY = HemoApplicationContext.Current.CurrentUser.USER_ID;
            record.CREATED_DATE = DateTime.Now;
            record.LAST_UPDATE_BY = HemoApplicationContext.Current.CurrentUser.USER_ID;
            record.LAST_UPDATE_DATE = DateTime.Now;
            record.VISIBLE_INDEX = _data.Count + 1;

            _data.AddMED_HEMO_CURE_DEFAULT_MODERow(record);

            this.gridView1.FocusedRowHandle = _data.Count - 1;
            this.iSaveButton.Enabled = true;
            this.HasDirty = true;

        }
        /// <summary>
        /// 右移病历
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void iMoveRightButton_Click(object sender, EventArgs e)
        {
            var record = this.gridView1.GetFocusedDataRow() as MaterialScheduleModel.MED_HEMO_CURE_DEFAULT_MODERow;
            if (record == null)
                return;
            if (XtraMessageBox.Show("你确定要从列表中移除此病历吗？", "删除提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                patientService.DeleteHemoDefaultModelById(record.ID);
                var last = this.gridView1.FocusedRowHandle == this.gridView1.DataRowCount - 1;
                _data.RemoveMED_HEMO_CURE_DEFAULT_MODERow(record);
                this.HasDirty = false;
                var rows = _data.Where(i => i.RowState != DataRowState.Deleted);
                int j = 0;
                foreach (var row in rows)
                {
                    if (!last)
                    {
                        _data[j].VISIBLE_INDEX = j + 1;
                        j++;
                    }
                    if (row.RowState == DataRowState.Added || row.RowState == DataRowState.Modified)
                        this.HasDirty = true;
                }
                this.iSaveButton.Enabled = this.HasDirty;
            }
        }
        /// <summary>
        /// 保存数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void iSaveButton_Click(object sender, EventArgs e)
        {
            this.bindingSource1.EndEdit();
            this.bindingSource1.CurrencyManager.EndCurrentEdit();

            patientService.SaveHemoDefaultModel(_data);

            _data.AcceptChanges();
            this.HasDirty = false;
            this.iSaveButton.Enabled = false;
        }
        /// <summary>
        /// 上移
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void iMoveUpButton_Click(object sender, EventArgs e)
        {
            MoveItemVisibleIndex(true);
        }
        /// <summary>
        /// 下称
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void iMoveDownButton_Click(object sender, EventArgs e)
        {
            MoveItemVisibleIndex(false);
        }
        /// <summary>
        /// 上移或者下移列表项
        /// </summary>
        /// <param name="up"></param>
        private void MoveItemVisibleIndex(bool up)
        {
            var movePosition = this.bindingSource1.CurrencyManager.Position;

            var targetPosition = up ? movePosition - 1 : movePosition + 1;
            if (targetPosition < 0 || targetPosition >= _data.Count)
                return;

            var move = _data.NewMED_HEMO_CURE_DEFAULT_MODERow();
            move.ItemArray = _data[movePosition].ItemArray;

            var target = _data.NewMED_HEMO_CURE_DEFAULT_MODERow();
            target.ItemArray = _data[targetPosition].ItemArray;

            _data[movePosition].ID = "MoveId";
            _data[targetPosition].ID = "TargetId";

            _data[movePosition].ItemArray = target.ItemArray;
            _data[targetPosition].ItemArray = move.ItemArray;

            for (int i = 0; i < _data.Count; i++)
            {
                _data[i].VISIBLE_INDEX = i + 1;
            }

            this.HasDirty = true;
            this.iSaveButton.Enabled = true;

            this.bindingSource1.CurrencyManager.Position = targetPosition;

            move = null;
            target = null;
        }
        /// <summary>
        /// 脏数据检查
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void officelookUpEdit_EditValueChanging(object sender, DevExpress.XtraEditors.Controls.ChangingEventArgs e)
        {
            if (this.HasDirty && XtraMessageBox.Show("当前界面含有示保存的数据,是否继续刷新？", "保存提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            {
                e.Cancel = true;
            }
        }
        /// <summary>
        /// 关闭前清理资源
        /// </summary>
        protected override void OnClosingEventHandler()
        {
            if (_data != null)
                _data.Dispose();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            PatientMaterialMode recordDetail = new PatientMaterialMode();
            recordDetail.ShowDialog();
            officelookUpEdit_EditValueChanged(null, null);
        }
        #endregion
    }
}
