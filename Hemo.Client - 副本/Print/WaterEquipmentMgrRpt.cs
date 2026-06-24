/*----------------------------------------------------------------
// Copyright (C) 2005 苏州XX公司有限公司
// 描述：水处理机管理报表
// 创建时间：2015-12-27
// 创建者：刘超
//  
// 修改时间：
// 修改人：
// 修改描述：
----------------------------------------------------------------*/

using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Data;
using Hemo.Model;
using System.IO;

namespace Hemo.Client.Print
{
    public partial class WaterEquipmentMgrRpt : DevExpress.XtraReports.UI.XtraReport
    {
        #region 构造函数

        public WaterEquipmentMgrRpt(MachineModel.MED_EQUIPMENT_MGRRow row, MachineModel.MED_EQUIPMENT_MAINTENANCEDataTable dt2, MachineModel.MED_EQUIPMENT_REPAIRDataTable dt3)
        {
            InitializeComponent();
            CreateTable();
            this.DetailReport1.DataSource = dt2;
            this.DetailReport2.DataSource = dt3;
            BindMgr(row);
            try
            {
                Image docImage = null;
                using (Stream imageStream = new MemoryStream(row.PIC))
                {
                    docImage = new Bitmap(imageStream);
                }
                this.xrPictureBox1.Image = docImage;
            }
            catch (Exception ex)
            {

            }
        }

        #endregion

        #region 方法

        void BindMgr(MachineModel.MED_EQUIPMENT_MGRRow row)
        {
            this.MACHINE_ID.Text = row.MACHINE_ID.ToString();
            this.MACHINE_MODEL.Text = row.MACHINE_MODEL.ToString();
            this.STARTDATE.Text = row.STARTDATE.ToString();
        }

        private void CreateTable()
        {

        }

        #endregion
    }
}
