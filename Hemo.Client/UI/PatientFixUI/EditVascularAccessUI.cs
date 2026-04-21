/*----------------------------------------------------------------
// Copyright (C) 2013 苏州XX公司有限公司
// 描述：病人血管通路编辑窗体
// 创建时间：2013-03-12
// 创建者：刘超
//  
// 修改时间：
// 修改人：
// 修改描述：
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
using Hemo.Service;
using Hemo.Utilities;
using Hemo.Client.UI.Patient;
using Hemo.IService.Config;
using System.Drawing.Imaging;
using System.IO;
using DevExpress.XtraTreeList.Nodes;
using DevExpress.XtraTreeList;
using Hemo.Client.UI.Machine;

namespace Hemo.Client.UI.Hemodialysis
{
    public partial class EditVascularAccessUI : ViewBase
    {

        #region 私有成员
        /// <summary>
        /// 血管通路表
        /// </summary>
        private HemoModel.MED_VASCULAR_ACCESSDataTable _vascularAccessDataTable;
        /// <summary>
        /// 病人类实例化
        /// </summary>
        private PatientService objPatient = new PatientService();
        private VascuarAccessService objVascuarAccess = new VascuarAccessService();
        private IConfig _configService = ServiceManager.Instance.ConfigService;
        private string accessType = null;
        private string oldAccessType = null;
        /// <summary>
        /// 是否为新增血管通路
        /// </summary>
        private bool isAdd = false;

        private const double _DefaultMaxPicWidth = 2300;
        private const double _DefaultMaxPicHeight = 1800;
        private string hemodialysisID = string.Empty;
        #endregion

        #region 属性

        /// <summary>
        /// 血管通路
        /// </summary>
        public string AccessType
        {
            get { return accessType; }
        }

        #endregion

        #region 初始化方法
        public EditVascularAccessUI()
        {
            InitializeComponent();
            //   hemodialysisID = pHemodialysisID;
            this.Text = "血管通路";
            //  ProFunctionCount pfc = new ProFunctionCount();
            //  loadData(pHemodialysisID);

        }

        public void LoadData(string pHemodialysisID)
        {
            loadLookUpEditList();
            //传入透析号ID  
            hemodialysisID = pHemodialysisID;
            // ctlUserLongInfo1.LoadPatientInfo();
            //ctlUserLongInfo1.SetControlsEnabled(false);
            // ctlUserLongInfo1.PanelWidth = 900;
            showTreeList(pHemodialysisID);
            txtHEMODIALYSIS_ID.Text = pHemodialysisID;
            BaseControlInfo.SetControlEnabled(panControl, false);
            //this.queryEstimateInBasketUI1.HemoID = pHemodialysisID;
            //this.queryEstimateInBasketUI1.queryData(pHemodialysisID);
            //this.queryEstimateVenousListUI_Temp.HemoId = pHemodialysisID;
            //this.queryEstimateVenousListUI_Temp.IsTemp = true;
            //this.queryEstimateVenousListUI_Temp.InizationDate();
            //this.queryEstimateVenousListUI_Long.HemoId = pHemodialysisID;
            //this.queryEstimateVenousListUI_Long.IsTemp = false;
            //this.queryEstimateVenousListUI_Long.InizationDate();
        }

        /// <summary>
        /// 根据透析号得到病人血管通路日期列表并绑定给树控件
        /// </summary>
        /// <param name="pHEMODIALYSIS_ID">透析号</param>
        private void showTreeList(string pHEMODIALYSIS_ID)
        {
            _vascularAccessDataTable = objVascuarAccess.GetVascularAccessListByHEMODIALYSIS_ID(pHEMODIALYSIS_ID);
            if (_vascularAccessDataTable != null && _vascularAccessDataTable.Rows.Count > 0)
            {
                this.trlCreateDateList.KeyFieldName = _vascularAccessDataTable.Columns["VASCULAR_ACCESS_ID"].ToString();
                trlCreateDateList.DataSource = _vascularAccessDataTable;
                this.treeListColumn1.FieldName = "CREATE_DATE";

                if (_vascularAccessDataTable != null && _vascularAccessDataTable.Rows.Count > 0)
                {
                    DataTable dtVaName = objVascuarAccess.GetVascularAccessAllName(_vascularAccessDataTable.Rows[0]["VASCULAR_ACCESS_ID"].ToString());
                    if (dtVaName != null && dtVaName.Rows.Count > 0)
                    {
                        VName = dtVaName.Rows[0][0].ToString();
                        drawValName(VName);
                        loadValPicture(VName);
                    }
                }
            }
            else
            {
                trlCreateDateList.ClearNodes();
                btnEdit.Enabled = false;
            }
        }

        /// <summary>
        /// 载入窗体下拉框数据
        /// </summary>
        private void loadLookUpEditList()
        {
            BaseControlInfo.BindLookUpEdit(cbxVASCULAR_ACCESS_TYPE, "ITEM_ID", "ITEM_NAME", this._configService.GetConfigList(string.Empty, string.Empty, "血管通路", "1"), "ITEM_NAME", "血管通路");
            BaseControlInfo.BindLookUpEdit(cbxACCESS_MATERIA, "ITEM_ID", "ITEM_NAME", this._configService.GetConfigList(string.Empty, string.Empty, "通路材质", "1"), "ITEM_NAME", "通路材质");
            BaseControlInfo.BindLookUpEdit(cbxLATERAL_POSITION, "ITEM_ID", "ITEM_NAME", this._configService.GetConfigList(string.Empty, string.Empty, "侧位", "1"), "ITEM_NAME", "侧位");
            BaseControlInfo.BindLookUpEdit(cbxVASCULAR_POSTION, "ITEM_ID", "ITEM_NAME", this._configService.GetConfigList(string.Empty, string.Empty, "通路部位", "1"), "ITEM_NAME", "通路部位");
            BaseControlInfo.BindLookUpEdit(cbxBLOOD_VESSEL, "ITEM_ID", "ITEM_NAME", this._configService.GetConfigList(string.Empty, string.Empty, "血管", "1"), "ITEM_NAME", "血管");
            BaseControlInfo.BindLookUpEdit(cbxMODUS_OPERANDI, "ITEM_ID", "ITEM_NAME", this._configService.GetConfigList(string.Empty, string.Empty, "术式", "1"), "ITEM_NAME", "术式");
            BaseControlInfo.BindLookUpEdit(cbxACCESS_CLASS, "ITEM_ID", "ITEM_NAME", this._configService.GetConfigList(string.Empty, string.Empty, "通路分类", "1"), "ITEM_NAME", "通路分类");
            BaseControlInfo.BindLookUpEdit(cmbACCESS_STATUS, "ITEM_ID", "ITEM_NAME", this._configService.GetConfigList(string.Empty, string.Empty, "通路状态", "1"), "ITEM_NAME", "通路状态");
        }
        #endregion

        #region 数据处理
        /// <summary>
        /// 验证数据
        /// </summary>
        /// <returns></returns>
        private bool IsDataValidate()
        {
            bool result = true;
            //result = BaseControlInfo.CheckpLookUpEdit(cbxVASCULAR_ACCESS_TYPE, "请选择血管通路名称。", "血管通路");
            //if (result == false) {
            //    return result;
            //}
            //result = BaseControlInfo.CheckpLookUpEdit(cbxACCESS_MATERIA, "请选择通路材质。", "血管通路");
            //if (result == false) {
            //    return result;
            //}
            //result = BaseControlInfo.CheckpLookUpEdit(cbxLATERAL_POSITION, "请选择侧位。", "血管通路");
            //if (result == false) {
            //    return result;
            //}
            result = BaseControlInfo.CheckpLookUpEdit(cbxVASCULAR_POSTION, "请选择通路部位。", "血管通路");
            if (result == false)
            {
                return result;
            }
            //result = BaseControlInfo.CheckpLookUpEdit(cbxMODUS_OPERANDI, "请选择术式。", "血管通路");
            //if (result == false) {
            //    return result;
            //}
            result = BaseControlInfo.CheckpLookUpEdit(cbxACCESS_CLASS, "请选择通路分类", "血管通路");
            if (result == false)
            {
                return result;
            }
            if (GetByteLength(txtLOSE_REASON.Text) > 500)
            {
                AutoClosedMsgBox.ShowForm("失败原因字数过多！", "血管通路", 1000, MessageBoxIcon.Information);
                result = false;
                return result;
            }

            return result;
        }

        private int GetByteLength(string text)
        {
            return System.Text.Encoding.Default.GetBytes(text).Length;
        }

        /// <summary>
        /// 保存数据
        /// </summary>
        /// <returns></returns>
        private int SaveData()
        {
            int result = 0;
            var dt = BaseControlInfo.GetDataTableByPanel(_vascularAccessDataTable, panControl, isAdd) as HemoModel.MED_VASCULAR_ACCESSDataTable;
            if (this.picVASCULAR_PIC.Image != null)
                dt[0].VASCULAR_PIC = ConvertImageToByte(this.picVASCULAR_PIC.Image);
            result = objVascuarAccess.SaveVascularAccessInfo(dt);
            return result;
        }
        private byte[] ConvertImageToByte(Image image)
        {
            var imageCodecInfoList = ImageCodecInfo.GetImageEncoders();
            string mimeType = "image/jpeg";
            ImageCodecInfo myImageCodec = null;
            foreach (var imgCodec in imageCodecInfoList)
            {
                if (imgCodec.MimeType == mimeType)
                {
                    myImageCodec = imgCodec;
                    break;
                }
            }

            if (myImageCodec == null)
                return null;

            EncoderParameters encoderParams = new EncoderParameters(1);
            System.Drawing.Imaging.Encoder myCompressQuanlityEncoder = System.Drawing.Imaging.Encoder.Quality;
            EncoderParameter myCompressQualityParam = new EncoderParameter(myCompressQuanlityEncoder, 80L);
            encoderParams.Param[0] = myCompressQualityParam;

            double dblPicWidth = 0;
            double dblPicHeight = 0;
            double dblPercent = 0;
            byte[] tempImage;

            if (image.Width > _DefaultMaxPicWidth)
            {
                //去掉按比例图片存储
                dblPercent = _DefaultMaxPicWidth / Convert.ToDouble(image.Width);
                dblPicWidth = _DefaultMaxPicWidth;
                dblPicHeight = Convert.ToInt32(image.Height * dblPercent);


                Bitmap tempPic = new Bitmap(Convert.ToInt32(dblPicWidth), Convert.ToInt32(dblPicHeight));
                var graphics = Graphics.FromImage(tempPic);
                Rectangle recPic = new Rectangle(0, 0, Convert.ToInt32(dblPicWidth), Convert.ToInt32(dblPicHeight));
                graphics.DrawImage(image, recPic);

                using (MemoryStream msPicture = new MemoryStream())
                {
                    tempPic.Save(msPicture, myImageCodec, encoderParams);
                    tempImage = msPicture.ToArray();
                }
                tempPic.Dispose();
            }
            else
            {
                using (Bitmap bmpPic = new Bitmap(image))
                {
                    using (MemoryStream msPicture = new MemoryStream())
                    {
                        bmpPic.Save(msPicture, myImageCodec, encoderParams);
                        tempImage = msPicture.ToArray();
                    }
                }
            }

            return tempImage;
        }
        #endregion

        #region 各种事件

        /// <summary>
        /// 类型改变
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbxVASCULAR_ACCESS_TYPE_EditValueChanged(object sender, EventArgs e)
        {
            loadValPicture(VName);
        }

        /// <summary>
        /// 加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EditVascularAccessUI_Load(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// 保存数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSave_Click(object sender, EventArgs e)
        {

            if (IsDataValidate())
            {
                if (XtraMessageBox.Show("确定保存当前血管通路信息吗？", this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                    return;

                try
                {
                    if (SaveData() > 0)
                    {
                        //BaseControlInfo.ClearControlText(panControl);
                        if (!this.cbxVASCULAR_ACCESS_TYPE.EditValue.ToString().Equals(oldAccessType))
                        {
                            accessType = this.cbxVASCULAR_ACCESS_TYPE.Text;
                        }
                        showTreeList(hemodialysisID);

                        AutoClosedMsgBox.ShowForm("保存成功。", "系统提示", 1000, MessageBoxIcon.Information);

                        //this.Close();
                        //BaseControlInfo.SetControlEnabled(panControl, false);
                        //btnEdit.Text = "编辑";
                        //showTreeList(txtHEMODIALYSIS_ID.Text);
                    }
                }
                catch (Exception ex)
                {
                    AutoClosedMsgBox.ShowForm(ex.Message, "系统提示", 1000, MessageBoxIcon.Error);
                }
            }
        }

        /// <summary>
        /// 点击节点根据ID得到对应日期的血管通路数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void treeList1_FocusedNodeChanged(object sender, DevExpress.XtraTreeList.FocusedNodeChangedEventArgs e)
        {
            //  busyIndicator1.ShowLoadingScreenFor(trlCreateDateList);

            if (e.Node != null)
            {
                using (BackgroundWorker worker = new BackgroundWorker())
                {
                    worker.DoWork += delegate(object sender1, DoWorkEventArgs e1)
                    {
                        _vascularAccessDataTable = objVascuarAccess.GetVascuarAccessListByID(e.Node.GetValue("VASCULAR_ACCESS_ID").ToString());
                    };
                    worker.RunWorkerCompleted += delegate(object sender2, RunWorkerCompletedEventArgs e2)
                    {
                        BaseControlInfo.ClearControlText(panControl);
                        BaseControlInfo.SetControlDataByDataTable(_vascularAccessDataTable, panControl);
                        if (_vascularAccessDataTable != null && _vascularAccessDataTable.Rows.Count > 0)
                        {
                            if (!_vascularAccessDataTable[0].IsVASCULAR_PICNull())
                                this.picVASCULAR_PIC.EditValue = _vascularAccessDataTable[0].VASCULAR_PIC;
                        }
                        this.btnEdit.Enabled = true;
                        oldAccessType = this.cbxVASCULAR_ACCESS_TYPE.EditValue.ToString();
                        // this.busyIndicator1.HideLoadingScreen();
                    };
                    worker.RunWorkerAsync();
                }
            }
            // this.busyIndicator1.HideLoadingScreen();

        }

        /// <summary>
        /// 新增数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAdd_Click(object sender, EventArgs e)
        {
            isAdd = true;
            btnSave.Enabled = true;
            btnEdit.Enabled = false;
            _vascularAccessDataTable.Clear();
            BaseControlInfo.ClearControlText(panControl);
            //得到ID放在清空之后
            txtHEMODIALYSIS_ID.Text = hemodialysisID;
            txtVASCULAR_ACCESS_ID.Text = Guid.NewGuid().ToString();
            txtCREATE_DATE.Text = System.DateTime.Now.ToShortDateString();
            BaseControlInfo.SetControlEnabled(panControl, true);
            if (isAdd)
                this.chkIS_SUCCESS.CheckState = CheckState.Checked;
            // trlCreateDateList.Enabled = false;
            cbxVASCULAR_ACCESS_TYPE.Focus();
        }

        /// <summary>
        /// 编辑数据，控件可用 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (btnEdit.Text == "编辑")
            {
                this.btnAdd.Enabled = false;
                btnSave.Enabled = true;
                BaseControlInfo.SetControlEnabled(panControl, true);
                btnEdit.Text = "取消";
            }
            else
            {
                this.btnAdd.Enabled = true;
                btnSave.Enabled = false;
                BaseControlInfo.SetControlEnabled(panControl, false);
                btnEdit.Text = "编辑";
            }
            cbxVASCULAR_ACCESS_TYPE.Focus();
        }

        /// <summary>
        /// 退出窗体
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnClose_Click(object sender, EventArgs e)
        {

            this.ParentForm.Close();
        }

        /// <summary>
        /// 拍照
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnPicture_Click(object sender, EventArgs e)
        {
            PatientPictureAction frm = new PatientPictureAction();
            frm.Text = "患者通路状态图片采集";
            if (this.picVASCULAR_PIC.Image != null)
                frm.SetPicturePriwView(this.picVASCULAR_PIC.Image);
            if (frm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                this.picVASCULAR_PIC.Image = frm.PatientPicture;
            }
        }

        private void picVASCULAR_PIC_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                PreviewPictureFrm picPreviewFrm = new PreviewPictureFrm();
                picPreviewFrm.DisplayImage(picVASCULAR_PIC.Image);
                picPreviewFrm.ShowDialog();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion

        #region MyRegion

        private void 删除ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (node != null)
            {
                if (XtraMessageBox.Show("是否确认删除此血管通路？", "血管通路", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == System.Windows.Forms.DialogResult.No)
                    return;
                BaseControlInfo.ClearControlText(panControl);
                var data = objVascuarAccess.GetVascuarAccessListByID(node.GetValue("VASCULAR_ACCESS_ID").ToString());
                string hemoId = data[0].HEMODIALYSIS_ID;
                data[0].Delete();
                objVascuarAccess.SaveVascularAccessInfo(data);
                showTreeList(hemoId);
            }
            else
            {
                XtraMessageBox.Show("请选择要删除的血透通路!");
            }
        }
        TreeListNode node = null;
        string VName = string.Empty;
        private void trlCreateDateList_MouseDown(object sender, MouseEventArgs e)
        {
            TreeListHitInfo hitInfo = (sender as TreeList).CalcHitInfo(new Point(e.X, e.Y));
            node = hitInfo.Node;
            node = hitInfo.Node;
            this.trlCreateDateList.SetFocusedNode(node);
            if (e.Button == MouseButtons.Right)
            {
                if (node != null)
                {
                    //node.TreeList.FocusedNode = node;
                    this.contextMenuStrip1.Visible = true;
                    this.contextMenuStrip1.Left = MousePosition.X;
                    this.contextMenuStrip1.Top = MousePosition.Y;
                }
            }
            else if (e.Button == MouseButtons.Left)// && e.Clicks == 2
            {

                if (node != null)
                {
                    using (BackgroundWorker worker = new BackgroundWorker())
                    {
                        worker.DoWork += delegate(object sender1, DoWorkEventArgs e1)
                        {
                            _vascularAccessDataTable = objVascuarAccess.GetVascuarAccessListByID(node.GetValue("VASCULAR_ACCESS_ID").ToString());

                        };
                        worker.RunWorkerCompleted += delegate(object sender2, RunWorkerCompletedEventArgs e2)
                        {
                            BaseControlInfo.ClearControlText(panControl);
                            BaseControlInfo.SetControlDataByDataTable(_vascularAccessDataTable, panControl);
                            oldAccessType = this.cbxVASCULAR_ACCESS_TYPE.EditValue.ToString();
                            if (_vascularAccessDataTable != null && _vascularAccessDataTable.Rows.Count > 0)
                            {
                                DataTable dtVaName = objVascuarAccess.GetVascularAccessAllName(txtVASCULAR_ACCESS_ID.Text);
                                if (dtVaName != null && dtVaName.Rows.Count > 0)
                                {
                                    VName = dtVaName.Rows[0][0].ToString();
                                    drawValName(VName);
                                    loadValPicture(VName);
                                }
                            }
                        };
                        worker.RunWorkerAsync();
                    }
                }
            }
        }

        #endregion

        #region 血管通路写字并载入图片
        private void drawValName(string pName)
        {
            if (_vascularAccessDataTable != null && _vascularAccessDataTable.Rows.Count > 0)
            {
                using (Graphics g = Graphics.FromImage(picVasAccess.Image))
                {
                    g.DrawString(pName, new Font("宋体", 12),
                        Brushes.Red, new PointF(8, 478));
                    g.Flush();
                }
            }
        }

        private void loadValPicture(string pName)
        {
            if (pName.Contains("左侧颈内静脉"))
            {
                picVasAccess.Image = Image.FromFile(Application.StartupPath + @"\Resources\human_organs_1.png");
            }
            else if (pName.Contains("右侧侧颈内静脉"))
            {
                picVasAccess.Image = Image.FromFile(Application.StartupPath + @"\Resources\human_organs_2.png");
            }
            else if (pName.Contains("左侧锁骨下静脉"))
            {
                picVasAccess.Image = Image.FromFile(Application.StartupPath + @"\Resources\human_organs_3.png");
            }
            else if (pName.Contains("右侧锁骨下静脉"))
            {
                picVasAccess.Image = Image.FromFile(Application.StartupPath + @"\Resources\human_organs_4.png");
            }
            else if (pName.Contains("左侧上臂"))
            {
                picVasAccess.Image = Image.FromFile(Application.StartupPath + @"\Resources\human_organs_5.png");
            }
            else if (pName.Contains("右侧下臂"))
            {
                picVasAccess.Image = Image.FromFile(Application.StartupPath + @"\Resources\human_organs_6.png");
            }
            else if (pName.Contains("左侧前臂"))
            {
                picVasAccess.Image = Image.FromFile(Application.StartupPath + @"\Resources\human_organs_7.png");
            }
            else if (pName.Contains("右侧前臂"))
            {
                picVasAccess.Image = Image.FromFile(Application.StartupPath + @"\Resources\human_organs_8.png");
            }
            else if (pName.Contains("左侧股静脉"))
            {
                picVasAccess.Image = Image.FromFile(Application.StartupPath + @"\Resources\human_organs_9.png");
            }
            else if (pName.Contains("右侧股静脉"))
            {
                picVasAccess.Image = Image.FromFile(Application.StartupPath + @"\Resources\human_organs_10.png");
            }
            else
            {
                picVasAccess.Image = Image.FromFile(Application.StartupPath + @"\Resources\human_organs.png");
            }
            drawValName(VName);
        }
        #endregion
    }
}