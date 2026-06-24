/*----------------------------------------------------------------
 * Copyright (C) 2005 (苏州)医疗科技发展有限公司
 * 文件功能描述:上传患者信息和患者病历至质控管理平台
 * 创建标识:刘配齐-2017年11月21日
 * 
 * ----------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Hemo.IService;
using Hemo.Service;
using Hemo.Model;

namespace Hemo.Client.UI.Hemodialysis
{
    public partial class ctlUpPatientInfo : UserControl
    {
        private IPatient _ipatient = ServiceManager.Instance.PatientService;

        private PatientModel.MED_PATIENTSDataTable _dtPatients = new PatientModel.MED_PATIENTSDataTable();
        public ctlUpPatientInfo()
        {
            InitializeComponent();
            if (xtraTabControl2.SelectedTabPageIndex == 0)
            {
                LoadPatientsInfoByConditions();
            }
            else if (xtraTabControl2.SelectedTabPageIndex == 1)
            {
            }
        }


        /// <summary>
        /// 初始化患者个人信息
        /// </summary>
        private void LoadPatientsInfoByConditions()
        {
            string name = string.Empty;
            string hemodialysis = string.Empty;
            name = this.txtName.Text.Trim();
            hemodialysis = this.txtHemoDialysis.Text.Trim();
            using (BackgroundWorker bw = new BackgroundWorker())
            {
                PatientModel.MED_PATIENTSDataTable dtSource = new PatientModel.MED_PATIENTSDataTable();
                bw.DoWork += delegate(object obj, DoWorkEventArgs e)
                {
                    _dtPatients = _ipatient.GetPatientList();
                };
                bw.RunWorkerCompleted += delegate(object obj, RunWorkerCompletedEventArgs e)
                {
                    if (name.Length > 0 && hemodialysis.Length > 0)
                    {
                        _dtPatients.Where(i => (i.NAME.Contains(name) || i.INPUT_CODE.ToUpper().Contains(name.ToUpper())) && i.HEMODIALYSIS_ID == hemodialysis).CopyToDataTable<PatientModel.MED_PATIENTSRow>(dtSource, LoadOption.PreserveChanges);
                    }
                    else if (name.Length > 0 && hemodialysis.Length == 0)
                    {
                        _dtPatients.Where(i => (i.NAME.Contains(name) || i.INPUT_CODE.ToUpper().Contains(name.ToUpper()))).CopyToDataTable<PatientModel.MED_PATIENTSRow>(dtSource, LoadOption.PreserveChanges);
                    }
                    else if (name.Length == 0 && hemodialysis.Length > 0)
                    {
                        _dtPatients.Where(i => i.HEMODIALYSIS_ID == hemodialysis).CopyToDataTable<PatientModel.MED_PATIENTSRow>(dtSource, LoadOption.PreserveChanges);
                    }
                    else
                    {
                        _dtPatients.CopyToDataTable<PatientModel.MED_PATIENTSRow>(dtSource, LoadOption.PreserveChanges);
                    }

                    dtSource.Columns.Add("IS_UPNAME", Type.GetType("System.String"));
                    dtSource.AsEnumerable().ToList().ForEach(row =>
                    {
                        if (row.IS_UP != null)
                        {
                            row["IS_UPNAME"] = (row.IS_UP == "1" ? "已上传" : "未上传");
                        }
                        else
                        {
                            row["IS_UPNAME"] = "未上传";
                        }
                    });

                    this.gridPatients.DataSource = dtSource;
                };
                bw.RunWorkerAsync();
            };
        }

        /// <summary>
        /// 查询患者信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnPatientsInfoQuery_Click(object sender, EventArgs e)
        {
            LoadPatientsInfoByConditions();
        }
        /// <summary>
        /// 全选or取消全选患者信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ckdCheckOrCancel_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                var dtSource = ((System.Data.DataView)this.gridVPatients.DataSource).Table as PatientModel.MED_PATIENTSDataTable;
                foreach (PatientModel.MED_PATIENTSRow row in dtSource)
                {
                    if (row["IS_UPNAME"].ToString() == "已上传")
                    {
                        continue;
                    }
                    row["IS_UP"] = this.ckdCheckOrCancel.Checked ? "1" : "0";
                }
            }
            catch (Exception ex) { }
        }

        private void gridVPatients_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            var row = this.gridVPatients.GetFocusedDataRow() as PatientModel.MED_PATIENTSRow;
            if (row != null)
            {
                if (e.Clicks == 1)
                {
                    if (row["IS_UP"].ToString() == "0")
                    {
                        row["IS_UP"] = 1;
                    }
                    else if (row["IS_UP"].ToString() == "1")
                    {
                        row["IS_UP"] = 0;
                    }
                }
            }
        }

        private void rdgStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            var dt = new PatientModel.MED_PATIENTSDataTable();
            if (rdgStatus.SelectedIndex == 0) //全部
            {
                _dtPatients.CopyToDataTable<PatientModel.MED_PATIENTSRow>(dt, LoadOption.PreserveChanges);
            }
            else if (rdgStatus.SelectedIndex == 1) // 已上传
            {
                _dtPatients.AsEnumerable().Where(i => i.IS_UP == "1").CopyToDataTable<PatientModel.MED_PATIENTSRow>(dt, LoadOption.PreserveChanges);
            }
            else if (rdgStatus.SelectedIndex == 2) //未上传
            {
                _dtPatients.AsEnumerable().Where(i => i.IS_UP == "0").CopyToDataTable<PatientModel.MED_PATIENTSRow>(dt, LoadOption.PreserveChanges);
            }

            dt.Columns.Add("IS_UPNAME", Type.GetType("System.String"));
            dt.AsEnumerable().ToList().ForEach(row =>
            {
                if (row.IS_UP != null)
                {
                    row["IS_UPNAME"] = (row.IS_UP == "1" ? "已上传" : "未上传");
                }
                else
                {
                    row["IS_UPNAME"] = "未上传";
                }
            });

            this.gridPatients.DataSource = dt;
        }
        void cloneRepository_Click(object sender, EventArgs e)
        {
            MessageBox.Show("已上传不能再上传");
        }
        private void xtraTabControl2_SelectedPageChanged(object sender, DevExpress.XtraTab.TabPageChangedEventArgs e)
        {
            if (xtraTabControl2.SelectedTabPageIndex == 0)
            {
                LoadPatientsInfoByConditions();
            }
            else if (xtraTabControl2.SelectedTabPageIndex == 1)
            {
            }
        }

    }
}
