/*----------------------------------------------------------------
// Copyright (C) 2013 苏州XX公司
// 描述：透析患者综合信息查询类
// 创建时间：2015-04-15
// 创建者：刘超
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
using Hemo.Client.UI.Machine;
using Hemo.Model;
using Hemo.IService;
using Hemo.Service;
using Hemo.IService.Config;
using DevExpress.XtraTreeList;
using DevExpress.XtraTreeList.Nodes;

namespace Hemo.Client.UI.Patient
{
    public partial class IntegratedPatientInfo : HemoBaseFrm
    {
        #region 构造函数

        public IntegratedPatientInfo()
        {
            InitializeComponent();
        }

        #endregion
        
        #region 变量
        private PatientModel.MED_PATIENTSDataTable allPatients = null;
        private IPatient objPatient = ServiceManager.Instance.PatientService;
        private IConfig _configService = ServiceManager.Instance.ConfigService;
        private ConfigModel.MED_COMMON_ITEMLISTDataTable _treeList;

        public PatientModel.MED_PATIENTSRow patientSelect = null;

        public bool IsOnlyShowSchedulePatients = false;

        #endregion

        #region 方法

        private void InzationData()
        {
            using (var _work = new BackgroundWorker())
            {
                this.busyIndicator1.ShowLoadingScreenFor(this.treePatientArea);
                var data = new PatientModel.MED_PATIENTSDataTable();
                allPatients = new PatientModel.MED_PATIENTSDataTable();
                _work.DoWork += delegate(object sender, DoWorkEventArgs e)
                {
                    data = this.objPatient.GetPatientList();
                    this._treeList = this._configService.GetConfigList(string.Empty, string.Empty, "区域", "1"); 
                };
                _work.RunWorkerCompleted += delegate(object sender1, RunWorkerCompletedEventArgs e1)
                {
                    #region 建造树的数据 
                    if (IsOnlyShowSchedulePatients)
                        data.Where(i => !i.IsROOMIDNull()).CopyToDataTable<PatientModel.MED_PATIENTSRow>(allPatients, LoadOption.PreserveChanges);
                    else
                        data.CopyToDataTable<PatientModel.MED_PATIENTSRow>(allPatients, LoadOption.PreserveChanges);

                    foreach (ConfigModel.MED_COMMON_ITEMLISTRow item in this._treeList.Rows)
                    {
                        item.PARENT = "12345";
                    }
                    var row = this._treeList.NewMED_COMMON_ITEMLISTRow();
                    row.ITEM_ID = "1234";
                    row.ITEM_VALUE = "wpbhz";
                    row.ITEM_NAME = "未排班患者";
                    row.ITEM_TYPE = "区域";
                    row.STATUS = "1";
                    row.ORDER_NUMBER = 55;
                    this._treeList.AddMED_COMMON_ITEMLISTRow(row);
                    var row1 = this._treeList.NewMED_COMMON_ITEMLISTRow();
                    row1.ITEM_ID = "12345";
                    row1.ITEM_VALUE = "wpbhz";
                    row1.ITEM_NAME = "排班患者";
                    row1.ITEM_TYPE = "区域";
                    row1.STATUS = "1";
                    row1.ORDER_NUMBER = 55;
                    this._treeList.AddMED_COMMON_ITEMLISTRow(row1);
                    //绑定数据
                    this.treePatientArea.DataSource = this._treeList;
                    this.treePatientArea.Nodes[0].Selected = true;
                    

                    #endregion

                    //清屏
                    this.listView1.Clear();
                    //创建列
                    this.CreateHeaders();
                    RefreshView(allPatients);

                    this.busyIndicator1.HideLoadingScreen();
                };
                _work.RunWorkerAsync();
            }
        }

        #region ListView方法
   
        /// <summary>
        /// 创建ListView的列
        /// </summary>
        private void CreateHeaders()
        {
            //
            ColumnHeader colHead;
            colHead = new ColumnHeader();
            colHead.Text = "患者姓名";
            colHead.Width = 150;
            this.listView1.Columns.Add(colHead);
            //
            colHead = new ColumnHeader();
            colHead.Text = "性别";
            colHead.Width = 40;
            this.listView1.Columns.Add(colHead);
            //
            colHead = new ColumnHeader();
            colHead.Text = "透析号";
            colHead.Width = 80;
            this.listView1.Columns.Add(colHead);
            //
            colHead = new ColumnHeader();
            colHead.Text = "组别";
            colHead.Width = 100;
            this.listView1.Columns.Add(colHead);

        }
        /// <summary>
        /// 刷新列表
        /// </summary>
        public void RefreshView(PatientModel.MED_PATIENTSDataTable filterStr)
        {
            this.listView1.BeginUpdate();
            //显示数据
            this.PaintListView(filterStr);
            this.listView1.EndUpdate();
        }

        /// <summary>
        /// 显示数据
        /// </summary>
        protected virtual void PaintListView(PatientModel.MED_PATIENTSDataTable filterStr)
        {
            this.listView1.LargeImageList = imageList1;
            this.listView1.View = View.LargeIcon;
            //清空
            this.listView1.Items.Clear();
            try
            {
              
                this.groupControl1.Text = "患者信息   合计人数：" + Convert.ToString(filterStr.Count);

                foreach (PatientModel.MED_PATIENTSRow ptInfo in filterStr.Rows)
                {
                    ListViewItem lst = this.GetListViewInfo(ptInfo);


                    if (lst != null)
                    {
                        this.listView1.Items.Add(lst);
                        this.listView1.Show();
                    }
                }
                if (this.listView1.Items.Count > 0)
                {
                    this.listView1.Items[0].Selected = true;
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

                return;
            }

        }
        /// <summary>
        /// 根据选中的病区患者信息生成listview节点
        /// </summary>
        /// <param name="patientView">患者信息</param>
        /// <returns></returns>
        private ListViewItem GetListViewInfo(PatientModel.MED_PATIENTSRow patientView)
        {
            System.Windows.Forms.ListViewItem lvi = new ListViewItem();
            if (patientView != null)
            {
                lvi.Tag = patientView;
                lvi.SubItems.Add(patientView.NAME);
                lvi.SubItems.Add(patientView.SEX);
                lvi.SubItems.Add(patientView.HEMODIALYSIS_ID);
                lvi.SubItems.Add(patientView.WARD_CODE);
                lvi.ImageIndex = patientView.SEX == "男" ? 2 : 4;
                lvi.Text = patientView.NAME;
            }
            return lvi;
        }


        #endregion
        #endregion

        #region 事件

        private void listView1_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            PatientModel.MED_PATIENTSRow patientRow = null;

            if (this.listView1.Items.Count != 0)
            {

                patientRow = (PatientModel.MED_PATIENTSRow)e.Item.Tag;//this.neuListView1.sele.FocusedItem.Tag;

                #region 为患者信息赋值
                this.txtHEMODIALYSIS_ID.Text = patientRow.HEMODIALYSIS_ID;
                this.txtPATIENT_ID.Text = patientRow.PATIENT_ID;
                this.txtNAME.Text = patientRow.NAME;
                this.txtSEX.Text = patientRow.SEX;
                this.txtAge.Text = patientRow.AGE.ToString();
                this.txtBIRTHDAY.EditValue = patientRow.BIRTHDAY;
                this.txtDIAGNOSE.Text = patientRow.DIAGNOSE;
                this.cbxTIME_TYPE.EditValue = patientRow.TIME_TYPE;
                this.txtMARITAL.Text = patientRow.MARITAL;
                this.txtCardNo.Text = patientRow.CARDNO;
                #endregion

                #region 为选中的患者信息赋值
                this.patientSelect = patientRow;
                #endregion
            }
        }

        private void listView1_DoubleClick(object sender, EventArgs e)
        {
            if (this.listView1.SelectedItems != null && this.listView1.SelectedItems.Count > 0)
            {
                btnOk_Click(this, e);
            }
        }

        private void btnOk_Click(object sender, EventArgs e)
        {

            if (this.patientSelect != null && !string.IsNullOrEmpty(this.patientSelect.HEMODIALYSIS_ID))
            {
                this.DialogResult = DialogResult.OK;           
            }
            else
            {
                MessageBox.Show("请选择患者");
                return;
            }
        }

        private void IntegratedPatientInfo_Load(object sender, EventArgs e)
        {
            InzationData();
        }

        private void treePatientArea_MouseDown(object sender, MouseEventArgs e)
        {
            TreeListHitInfo hitInfo = (sender as TreeList).CalcHitInfo(new Point(e.X, e.Y));
            TreeListNode node = hitInfo.Node;
            node = hitInfo.Node;
            this.treePatientArea.SetFocusedNode(node);
            if (e.Button == MouseButtons.Left && e.Clicks == 1 && node != null)
            {               
                var clickStr = node.GetValue(treeListColumn2).ToString();
                if (clickStr == "1234" || clickStr == "12345")
                {
                    clickStr = string.Empty;
                }
                PatientModel.MED_PATIENTSDataTable _data = new PatientModel.MED_PATIENTSDataTable();

                allPatients.Where(i => i.ROOMID == clickStr).CopyToDataTable(_data, LoadOption.PreserveChanges);

                RefreshView(_data);
            }
        }

        private void txtHEMODIALYSIS_ID_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                PatientModel.MED_PATIENTSDataTable _data = new PatientModel.MED_PATIENTSDataTable();

                if (!string.IsNullOrEmpty(this.txtHEMODIALYSIS_ID.Text.Trim()))
                {
                    allPatients.Where(i => i.HEMODIALYSIS_ID == this.txtHEMODIALYSIS_ID.Text.Trim()).CopyToDataTable(_data, LoadOption.PreserveChanges);
                }
                else
                {
                    _data = allPatients;
                }
                RefreshView(_data);
            }
        }

        private void txtNAME_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                PatientModel.MED_PATIENTSDataTable _data = new PatientModel.MED_PATIENTSDataTable();

                if (!string.IsNullOrEmpty(this.txtNAME.Text.Trim()))
                {
                    allPatients.Where(i => i.NAME.Contains(this.txtNAME.Text.Trim()) || PinYinConverter.GetPYString(i.NAME).Contains(this.txtNAME.Text.Trim())).CopyToDataTable(_data, LoadOption.PreserveChanges);
                }
                else
                {
                    _data = allPatients;
                }
                RefreshView(_data);
            }
        }

        #endregion
    }
}
