/*----------------------------------------------------------------
// Copyright (C) 2005 苏州XX公司有限公司
// 描述：医嘱记录操作窗体
// 创建时间：2014-06-14
// 创建者：刘超
//  
// 修改时间：
// 修改人：
// 修改描述：
----------------------------------------------------------------*/

using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using Hemo.Client.Core;
using Hemo.IService.Erythropoietin;
using Hemo.IService.Order;
using Hemo.Model;
using Hemo.Service;
using Hemo.Utilities;

namespace Hemo.Client.UI.Erythropoietin
{
    public partial class OrderExecFrm :HemoBaseFrm
    {
        #region 变量

        private string _erythropoietinID;
        private DateTime _enterDate;
        private PatientModel.MED_PATIENTSRow _patientRow;
        private OrderModel.MED_ORDERSDataTable _orderDataTable;
        private IOrder _orderService = ServiceManager.Instance.OrderService;
        private IErythropoietin _erythropoietinService = ServiceManager.Instance.ErythropoietinService;

        #endregion

        #region 属性

        private DateTime BeginEnterDate
        {
            get
            {
                //return Utility.CDate(this._enterDate.ToString("2013-1-1 00:00:00"));
                return Utility.CDate(this._enterDate.ToString("yyyy-MM-dd 00:00:00"));
            }
        }

        private DateTime EndEnterDate
        {
            get
            {
                //return Utility.CDate(this._enterDate.ToString("2013-12-31 23:59:59"));
                return Utility.CDate(this._enterDate.ToString("yyyy-MM-dd 23:59:59"));
            }
        }

        #endregion

        #region 构造函数

        public OrderExecFrm(PatientModel.MED_PATIENTSRow patientRow, string erythropoietinID)
        {
            this.InitializeComponent();

            this._patientRow = patientRow;
            this._erythropoietinID = erythropoietinID;

            this._enterDate = DateTime.Now;
        }

        #endregion

        #region 方法

        /// <summary>
        /// 同步医嘱记录
        /// </summary>
        private void SyncOrderInfo()
        {
            bool result = false;

            this.btnSelectAll.Visible = this.btnCancelSelected.Visible = this.btnExecOrder.Visible = this.btnSyncOrderInfo.Visible = false;
            this.picLoading.Visible = true;

            using (BackgroundWorker worker = new BackgroundWorker())
            {
                worker.DoWork += (o1, e1) =>
                {
                    try
                    {
                        string error = InterfaceUtility.SynchronizeSingleOrder(this._patientRow.PATIENT_ID, Utility.CInt(this._patientRow.VISIT_ID.ToString()));

                        if (string.IsNullOrEmpty(error))
                            result = true;
                        else
                            XtraMessageBox.Show(string.Format("新接口（3.0）报错：{0}", error));
                    }
                    catch (Exception ex)
                    {
                        XtraMessageBox.Show(ex.Message);
                    }
                };
                worker.RunWorkerCompleted += (o2, e2) =>
                {
                    if (result)
                        this.LoadOrderData();

                    this.btnSelectAll.Visible = this.btnCancelSelected.Visible = this.btnExecOrder.Visible = this.btnSyncOrderInfo.Visible = true;
                    this.picLoading.Visible = false;
                };

                worker.RunWorkerAsync();
            }
        }

        private void LoadOrderData()
        {
            this._orderDataTable = this._orderService.GetOrderList4Erythropoietin(this._patientRow.PATIENT_ID, this.BeginEnterDate, this.EndEnterDate);
            //报错，暂时注释掉
       //     this._orderDataTable.SetOrderGroupSign();

            this.gcOrderInfo.DataSource = this._orderDataTable;
        }

        /// <summary>
        /// 全选或者取消
        /// </summary>
        /// <param name="value"></param>
        private void ChangeGridSelected(bool value)
        {
            foreach (OrderModel.MED_ORDERSRow orderRow in this._orderDataTable)
            {
                if (!orderRow.IsDRUG_EXEC_NURSENull() && !string.IsNullOrEmpty(orderRow.DRUG_EXEC_NURSE))
                    orderRow.CHOOSE = false;
                else
                    orderRow.CHOOSE = value;
            }
        }

        #endregion

        #region 事件

        private void btnClose_Click(object sender, EventArgs e) {
            this.Close();
        }

        private void LabFrm_Load(object sender, EventArgs e)
        {
            this.LoadOrderData();
        }

        private void gvOrderInfo_CustomDrawCell(object sender, RowCellCustomDrawEventArgs e)
        {
            if (e.RowHandle >= 0 && e.Column.ColumnHandle >= 0)
            {
                if (e.Column.VisibleIndex == 0)
                {
                    if (string.Compare(this._orderDataTable.Rows[e.RowHandle]["ORDER_SUB_NO"].ToString(), "1", true) != 0
                        || !string.IsNullOrEmpty(this._orderDataTable.Rows[e.RowHandle]["DRUG_EXEC_NURSE"].ToString()))
                    {
                        using (Brush gridBrush = new SolidBrush(e.Appearance.BackColor2), backColorBrush = new SolidBrush(e.Appearance.BackColor))
                        {
                            using (Pen gridLinePen = new Pen(gridBrush))
                            {
                                //清除单元格
                                e.Graphics.FillRectangle(backColorBrush, e.Bounds.Left, e.Bounds.Top, e.Bounds.Width, e.Bounds.Height);

                                e.Handled = true;
                            }
                        }
                    }
                }
                else if (e.Column.VisibleIndex == 8)
                    e.Appearance.ForeColor = string.Compare(e.CellValue.ToString(), "尚未执行", true) == 0 ? Color.Red : Color.Green;
            }
        }

        private void gvOrderInfo_MouseDown(object sender, MouseEventArgs e)
        {
            GridView gv = sender as GridView;

            if (gv == null)
                return;

            GridHitInfo hitInfo = gv.CalcHitInfo(new Point(e.X, e.Y));

            if (hitInfo == null)
                return;

            if (e.Button == MouseButtons.Left && hitInfo.InRowCell)
            {
                if (hitInfo.Column.VisibleIndex == 0)
                    this._orderDataTable.Rows[hitInfo.RowHandle]["CHOOSE"] = !Utility.CBool(this._orderDataTable.Rows[hitInfo.RowHandle]["CHOOSE"].ToString());
            }
        }

        /// <summary>
        /// 全选
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSelectAll_Click(object sender, EventArgs e)
        {
            this.ChangeGridSelected(true);
        }

        /// <summary>
        /// 全部取消
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancelSelected_Click(object sender, EventArgs e)
        {
            this.ChangeGridSelected(false);
        }

        /// <summary>
        /// 同步医嘱记录
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSyncOrderInfo_Click(object sender, EventArgs e)
        {
            this.SyncOrderInfo();
        }

        /// <summary>
        /// 执行医嘱
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnExecOrder_Click(object sender, EventArgs e)
        {
            OrderModel.MED_ORDERSRow[] orderRows = this._orderDataTable.Select("CHOOSE = true") as OrderModel.MED_ORDERSRow[];

            if (orderRows.Length == 0)
                XtraMessageBox.Show("请选择要执行的医嘱！");
            else
            {
                ErythropoietinModel.MED_ERYTHROPOIETIN_EXECDataTable erythropoietinExecDataTable = new ErythropoietinModel.MED_ERYTHROPOIETIN_EXECDataTable();

                foreach (var orderRow in orderRows)
                {
                    ErythropoietinModel.MED_ERYTHROPOIETIN_EXECRow erythropoietinExecRow = erythropoietinExecDataTable.NewMED_ERYTHROPOIETIN_EXECRow();

                    erythropoietinExecRow.EXEC_ID = Guid.NewGuid().ToString();
                    erythropoietinExecRow.ERYTHROPOIETIN_ID = this._erythropoietinID;
                    erythropoietinExecRow.REMARK = this.txtREMARK.Text;
                    erythropoietinExecRow.NURSE = LoginUser.User.USER_NAME;
                    erythropoietinExecRow.EXCUTE_DATE = DateTime.Now;
                    erythropoietinExecRow.DRUG_ORDER_ID = orderRow.ORDER_NO;

                    erythropoietinExecDataTable.AddMED_ERYTHROPOIETIN_EXECRow(erythropoietinExecRow);
                }

                this._erythropoietinService.SaveErythropoietinExecInfo(erythropoietinExecDataTable);

                XtraMessageBox.Show("医嘱执行成功！");

                this.DialogResult = DialogResult.Yes;
            }
        }

        #endregion
    }
}
