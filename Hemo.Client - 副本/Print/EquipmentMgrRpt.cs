/*----------------------------------------------------------------
 * Copyright (C) 2005 (苏州)医疗科技股份有限公司
 * 文件功能描述:设备管理报表
 * 创建标识:刘超-2016年3月14日
 * ----------------------------------------------------------------*/

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
    public partial class EquipmentMgrRpt : DevExpress.XtraReports.UI.XtraReport
    {
        #region 构造函数

        public EquipmentMgrRpt(MachineModel.MED_EQUIPMENT_MGRRow row,MachineModel.MED_EQUIPMENT_MAINTENANCEDataTable dt2,MachineModel.MED_EQUIPMENT_REPAIRDataTable dt3  )
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
            catch(Exception ex)
            {
            
            }
        }

        #endregion

        #region 方法

        /// <summary>
        /// 初始化数据
        /// </summary>
        /// <param name="row"></param>
        private void BindMgr(MachineModel.MED_EQUIPMENT_MGRRow row)
        {
            this.MACHINE_ID.Text = row.MACHINE_ID.ToString();
            this.MACHINE_MODEL.Text = row.MACHINE_MODEL.ToString();
            this.STARTDATE.Text = row.STARTDATE.ToString();
            this.BED_ID.Text = row.BED_ID.ToString();
            this.THERAPEUTIC_PROPERTIES.Text = row.THERAPEUTIC_PROPERTIES.ToString();
            this.AREA_ID.Text = row.AREA_ID.ToString();         
        }

        private void CreateTable()
        {

        }

        #endregion
    }
}
