/*----------------------------------------------------------------
// Copyright (C) 2013 苏州XX公司
// 描述：患者透析卡查询类
// 创建时间：2016-05-15
// 创建者：贺建操
//  
// 修改时间：
// 修改人：
// 修改描述：
//
----------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Hemo.Model;
using Hemo.Client.UI.Machine;

namespace Hemo.Client.UI.Patient
{
    [ToolboxItem(true)]
    public partial class PatientGridCtl : ViewBase
    {
        #region 类变量

        private DrugModel.MED_PATIENTS_CARDDataTable _dtCard = null;
        private DrugModel.MED_PATIENTS_CARDDataTable _dtPatient = null;

        #endregion

        #region 构造函数

        public PatientGridCtl()
        {
            InitializeComponent();
        }

        #endregion

        #region 事件

        private void radioGroup_Filter_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (checkBox_ForToday.Checked)
            {
                if (radioGroup_Filter.SelectedIndex == 0)
                {
                    this.gridControl1.MainView = this.gridView1;
                    this.gridControl1.DataSource = _dtCard.Where(i => i["CREATEDATE"] != DBNull.Value && i.CREATEDATE.Date == System.DateTime.Now.Date).ToList();
                }
                else if (radioGroup_Filter.SelectedIndex == 1)
                {
                    this.gridControl1.MainView = this.gridView2;
                    this.gridControl1.DataSource = _dtPatient.Where(i => i["CREATEDATE"] != DBNull.Value && i.CREATEDATE.Date == System.DateTime.Now.Date).ToList();
                }
            }
            else
            {
                if (radioGroup_Filter.SelectedIndex == 0)
                {
                    this.gridControl1.MainView = this.gridView1;
                    this.gridControl1.DataSource = _dtCard;
                }
                else if (radioGroup_Filter.SelectedIndex == 1)
                {
                    this.gridControl1.MainView = this.gridView2;
                    this.gridControl1.DataSource = _dtPatient;
                }
            }

            //if (radioGroup_Filter.SelectedIndex == 0)
            //{
            //    this.gridControl1.MainView = this.gridView1;
            //    this.gridControl1.DataSource = _dtCard;
            //}
            //else if (radioGroup_Filter.SelectedIndex == 1)
            //{
            //    this.gridControl1.MainView = this.gridView2;
            //    this.gridControl1.DataSource = _dtPatient;
            //} 
        }

        private void checkBox_ForToday_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox_ForToday.Checked)
            {
                if (radioGroup_Filter.Visible)
                {
                    if (radioGroup_Filter.SelectedIndex == 0)
                    {
                        this.gridControl1.MainView = this.gridView1;
                        this.gridControl1.DataSource = _dtCard.Where(i => i["CREATEDATE"] != DBNull.Value && i.CREATEDATE.Date == System.DateTime.Now.Date).ToList();
                    }
                    else if (radioGroup_Filter.SelectedIndex == 1)
                    {
                        this.gridControl1.MainView = this.gridView2;
                        this.gridControl1.DataSource = _dtPatient.Where(i => i["CREATEDATE"] != DBNull.Value && i.CREATEDATE.Date == System.DateTime.Now.Date).ToList();
                    }
                }
                else
                {
                    this.gridControl1.MainView = this.gridView1;
                    this.gridControl1.DataSource = _dtCard.Where(i => i["CREATEDATE"] != DBNull.Value && i.CREATEDATE.Date == System.DateTime.Now.Date).ToList();
                }
            }
            else
            {
                if (radioGroup_Filter.Visible)
                {
                    if (radioGroup_Filter.SelectedIndex == 0)
                    {
                        this.gridControl1.MainView = this.gridView1;
                        this.gridControl1.DataSource = _dtCard;
                    }
                    else if (radioGroup_Filter.SelectedIndex == 1)
                    {
                        this.gridControl1.MainView = this.gridView2;
                        this.gridControl1.DataSource = _dtPatient;
                    }

                }
                else
                {
                    this.gridControl1.MainView = this.gridView1;
                    this.gridControl1.DataSource = _dtCard;
                }
            }
        }

        #endregion

        #region 方法

        public void InzationData(DrugModel.MED_PATIENTS_CARDDataTable dtCard, DrugModel.MED_PATIENTS_CARDDataTable dtpatient)
        {
            _dtCard = dtCard;
            _dtPatient = dtpatient;

            if (radioGroup_Filter.Visible)
            {
                if (checkBox_ForToday.Checked)
                {
                    if (radioGroup_Filter.SelectedIndex == 0)
                    {
                        this.gridControl1.MainView = this.gridView1;
                        this.gridControl1.DataSource = _dtCard.Where(i => i["CREATEDATE"] != DBNull.Value && i.CREATEDATE.Date == System.DateTime.Now.Date).ToList();
                    }
                    else if (radioGroup_Filter.SelectedIndex == 1)
                    {
                        this.gridControl1.MainView = this.gridView2;
                        this.gridControl1.DataSource = _dtPatient.Where(i => i["CREATEDATE"] != DBNull.Value && i.CREATEDATE.Date == System.DateTime.Now.Date).ToList();
                    }
                }
                else
                {
                    if (radioGroup_Filter.SelectedIndex == 0)
                    {
                        this.gridControl1.MainView = this.gridView1;
                        this.gridControl1.DataSource = _dtCard;
                    }
                    else if (radioGroup_Filter.SelectedIndex == 1)
                    {
                        this.gridControl1.MainView = this.gridView2;
                        this.gridControl1.DataSource = _dtPatient;
                    }
                }
            }
            else
            {
                if (checkBox_ForToday.Checked)
                {
                    this.gridControl1.MainView = this.gridView1;
                    this.gridControl1.DataSource = _dtCard.Where(i => i["CREATEDATE"] != DBNull.Value && i.CREATEDATE.Date == System.DateTime.Now.Date).ToList();
                }
                else
                {
                    this.gridControl1.MainView = this.gridView1;
                    this.gridControl1.DataSource = _dtCard;
                }
            }
        }

        #endregion
    }
}
