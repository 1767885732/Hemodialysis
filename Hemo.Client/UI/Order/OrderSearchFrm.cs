/*----------------------------------------------------------------
// Copyright (C) 2005 苏州XX公司有限公司
// 描述：医嘱记录查询窗体类
// 创建时间：2014-04-15
// 创建者：刘超
//  
// 修改时间：
// 修改人：
// 修改描述：
----------------------------------------------------------------*/

using System;
using DevExpress.XtraEditors;
using Hemo.Client.Core;
using Hemo.IService.Order;
using Hemo.Model;
using Hemo.Service;
using Hemo.Utilities;

namespace Hemo.Client.UI.Order
{
    public partial class OrderSearchFrm :HemoBaseFrm
    {
        #region 变量

        private PatientModel.MED_PATIENTSRow _patientRow;
        private IOrder _orderService = ServiceManager.Instance.OrderService;

        #endregion

        #region 属性

        private DateTime BeginEnterDate
        {
            get
            {
                return Utility.CDate(this.deBeginEnterDate.DateTime.ToString("yyyy-MM-dd 00:00:00"));
            }
        }

        private DateTime EndEnterDate
        {
            get
            {
                return Utility.CDate(this.deEndEnterDate.DateTime.ToString("yyyy-MM-dd 23:59:59"));
            }
        }

        #endregion

        #region 构造函数

        public OrderSearchFrm(PatientModel.MED_PATIENTSRow patientRow)
        {
            this.InitializeComponent();

            this._patientRow = patientRow;

            this.deBeginEnterDate.DateTime = DateTime.Now.AddDays(-7);
            this.deEndEnterDate.DateTime = DateTime.Now;
        }

        #endregion

        #region 方法

        private void LoadOrderData(bool isShowMsg)
        {
            OrderModel.MED_ORDERSDataTable orderDataTable = this._orderService.GetOrderList(this._patientRow.PATIENT_ID, this.BeginEnterDate, this.EndEnterDate);
            //报错，暂时注释掉
            //orderDataTable.SetOrderGroupSign();

            this.gcOrderInfo.DataSource = orderDataTable;

            if (isShowMsg && orderDataTable.Rows.Count == 0)
                XtraMessageBox.Show("查询不到医嘱数据，请稍后再试！");
        }

        #endregion

        #region 事件

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void LabFrm_Load(object sender, EventArgs e)
        {
            this.LoadOrderData(false);
        }

        /// <summary>
        /// 查询医嘱
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSearchOrder_Click(object sender, EventArgs e)
        {
            this.LoadOrderData(true);
        }

        #endregion
    }
}
