/*----------------------------------------------------------------
 * Copyright (C) 2005 (苏州)医疗科技股份有限公司
 * 文件功能描述:内瘘评估单报表
 * 创建标识:刘超-2016年2月25日
 * ----------------------------------------------------------------*/

using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using Hemo.Model;
using Hemo.IService.Machine;
using Hemo.Service;
using Hemo.IService.Config;
using System.Data;

namespace Hemo.Client.Print
{
    public partial class EstimateInBasket : DevExpress.XtraReports.UI.XtraReport
    {
        #region 类变量

        private IHemodialysis hemodialysisService = ServiceManager.Instance.HemodialysisService;

        #endregion

        //private string patientName;

        //private string hemoID;

        //private DateTime createDate;

        //private DataTable inBasketTable;

        ///// <summary>
        ///// 病人姓名
        ///// </summary>
        //public string PatientName
        //{
        //    get { return patientName; }
        //    set { patientName = value; }
        //}

        ///// <summary>
        ///// 透析ID
        ///// </summary>
        //public string HemoID
        //{
        //    get { return hemoID; }
        //    set { hemoID = value; }
        //}

        ///// <summary>
        ///// 创建日期
        ///// </summary>
        //public DateTime CreateDate
        //{
        //    get { return createDate; }
        //    set { createDate = value; }
        //}


        //public DataTable InBasketTable
        //{
        //    get
        //    {
        //        return inBasketTable;
        //    }
        //    set
        //    {
        //        inBasketTable = value;
        //    }
        //}

        #region 构造函数

        /// <summary>
        /// 
        /// </summary>
        /// <param name="InBasketTable"></param>
        /// <param name="CreateDate"></param>
        /// <param name="HemoID"></param>
        /// <param name="PatientName"></param>
        public EstimateInBasket(DataTable InBasketTable, DateTime CreateDate, string HemoID, string PatientName)
        {
            InitializeComponent();
            this.txtName.Text = PatientName;
            this.txtPatientID.Text = HemoID;
            this.txtCreateDate.Text = CreateDate.ToShortDateString();
            if (InBasketTable != null && InBasketTable.Rows.Count > 0)
            {
                this.DataMember = "";
                this.DataSource = InBasketTable;
            }
        }

        #endregion
    }
}
