/*----------------------------------------------------------------
 * Copyright (C) 2005 医疗科技发展有限公司
 * 文件功能描述:用户控件
 * 创建标识:吕志强-2017年1月25日
 * 
 * 修改时间:2017年6月12日
 * 修改人:贺建操
 * 修改描述:修复系统响应速度慢的问题
 * 
 * 修改时间:2017年7月14日
 * 修改人:顾伟伟
 * 修改描述:增加窗体控件值的方法
 * ----------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Hemo.Model;
using Hemo.IService.Config;
using Hemo.Service;
using Hemo.Client.Doc;
using Hemo.Client.UI.Hemodialysis;
using System.Collections;
using DevExpress.XtraEditors;
using System.IO;
using System.Net;
using Hemo.Utilities;
using System.Windows;
using Hemo.IService.PatientSchedule;

namespace Hemo.Client.Controls
{
    public partial class PatientKnowBooks : DevExpress.XtraEditors.XtraForm
    {
        public PatientKnowBooks()
        {
            InitializeComponent();
        }
        private IHemodialysis _hemodialysisService = ServiceManager.Instance.HemodialysisService;
        private ConfigModel.MED_COMMON_ITEMLISTDataTable dtPurificationMode;
        private IConfig _configService = ServiceManager.Instance.ConfigService;
        private IPatientSchedule scheduleService = ServiceManager.Instance.PatientSchedule;
        private CtlMedicalDocumentContainer _medicalDocContainer = new CtlMedicalDocumentContainer();
        private ConfigModel.MED_COMMON_ITEMLISTDataTable dtServer = null;

        private PatientModel.MED_PATIENTSRow _patientDocRow;

        public PatientModel.MED_PATIENTSRow PatientDocRow
        {
            get { return _patientDocRow; }
            set { _patientDocRow = value; }
        }

        private string areaName;

        public string AreaName
        {
            get { return areaName; }
            set { areaName = value; }
        }

        public void BindDocTree(PatientModel.MED_PATIENTSRow patientRow)
        {
            _patientDocRow = patientRow;
            dtPurificationMode = _configService.GetConfigList(string.Empty, string.Empty, "净化方式", "1");
            var dtCRRTPurificationMode = _configService.GetConfigList(string.Empty, string.Empty, "CRRT净化方式", "1");
            dtCRRTPurificationMode.CopyToDataTable(dtPurificationMode, LoadOption.OverwriteChanges);

            this.tlDocments.Nodes[0].Nodes.Clear();

            //PatientModel.MED_PATIENTSRow patientRow = cardView1.GetFocusedDataRow() as PatientModel.MED_PATIENTSRow;
            BindMedicalDocment(patientRow);
            if (patientRow == null)
                return;

            HemodialysisModel.MED_CURE_MAINDataTable cureMainDataTable = this._hemodialysisService.GetMainCureByHemoID(patientRow.HEMODIALYSIS_ID);

            string strCureDate = string.Empty;
            string strMonth = string.Empty;
            string strYear = string.Empty;
            string strSecondYear = string.Empty;
            string strSecondMonth = string.Empty;

            StringBuilder sbCureID = new StringBuilder();
            foreach (HemodialysisModel.MED_CURE_MAINRow cureMainRow in cureMainDataTable.Rows)
            {
                //透析治疗单信息
                //node.LinkClicked += new DevExpress.XtraNavBar.NavBarLinkEventHandler(itmNarBarCureDocument_LinkClicked);
                //将治疗单汇总，按月份生成。
                TreeNode nodeMonth = new TreeNode();
                strCureDate = cureMainRow.CURE_CREATE_DATE.ToString("yyyy-MM-dd");
                strYear = Utilities.Utility.CDate(strCureDate).Year.ToString();
                strMonth = Utilities.Utility.CDate(strCureDate).Month.ToString();
                nodeMonth.Text = strYear + "年" + strMonth + "月";
                nodeMonth.ImageIndex = 2;
                sbCureID.Append(cureMainRow.CURE_ID).Append(",");
                nodeMonth.Name = strYear + "-" + strMonth;
                //    nodeMonth.Name += cureMainRow.CURE_ID + ",";

                if (!this.tlDocments.Nodes[0].Nodes.ContainsKey(strYear + "-" + strMonth))
                {
                    //拼接治疗单ID用于批量续打

                    //  nodeMonth.ToolTipText = sbCureID.ToString();
                    //    MessageBox.Show(nodeMonth.ToolTipText);
                    this.tlDocments.Nodes[0].Nodes.Add(nodeMonth);
                    sbCureID.Clear();
                    foreach (HemodialysisModel.MED_CURE_MAINRow cureSecondMainRow in cureMainDataTable.Rows)
                    {
                        strCureDate = cureSecondMainRow.CURE_CREATE_DATE.ToString("yyyy-MM-dd");
                        strSecondYear = Utilities.Utility.CDate(strCureDate).Year.ToString();
                        strSecondMonth = Utilities.Utility.CDate(strCureDate).Month.ToString();
                        TreeNode node = new TreeNode();
                        node.ImageIndex = 5;
                        node.Tag = cureSecondMainRow.CURE_ID;

                        if (cureSecondMainRow["PURIFICATION_MODE"] != DBNull.Value)
                        {
                            if (dtPurificationMode.FindByITEM_ID(cureSecondMainRow.PURIFICATION_MODE) != null)
                            {
                                node.Text = string.Format("{0} {1}{2}", cureSecondMainRow.CURE_CREATE_DATE.ToString("yyyy-MM-dd"), dtPurificationMode.FindByITEM_ID(cureSecondMainRow.PURIFICATION_MODE).ITEM_NAME, (cureSecondMainRow.IsRECIPE_TYPENull() || cureSecondMainRow.RECIPE_TYPE != "1") ? string.Empty : "加");
                            }
                            else
                            {
                                node.Text = string.Format("{0} {1}{2}", cureSecondMainRow.CURE_CREATE_DATE.ToString("yyyy-MM-dd"), string.Empty, (cureSecondMainRow.IsRECIPE_TYPENull() || cureSecondMainRow.RECIPE_TYPE != "1") ? string.Empty : "加");
                            }
                        }
                        else
                        {
                            node.Text = string.Format("{0} {1}{2}", cureSecondMainRow.CURE_CREATE_DATE.ToString("yyyy-MM-dd"), string.Empty, (cureSecondMainRow.IsRECIPE_TYPENull() || cureSecondMainRow.RECIPE_TYPE != "1") ? string.Empty : "加");
                        }

                        //  this.tlDocments.Nodes[0].Nodes.Add(node);
                        if (strYear + strMonth == strSecondYear + strSecondMonth)
                        {
                            nodeMonth.Nodes.Add(node);
                        }
                    }
                }
            }

            this.tlDocments.Nodes[0].Expand();
            this.tlDocments.Nodes[1].Expand();
        }

        private void BindMedicalDocment(PatientModel.MED_PATIENTSRow row)
        {
            //PatientModel.MED_PATIENTSRow row = cardView1.GetFocusedDataRow() as PatientModel.MED_PATIENTSRow;
            if (row != null)
            {
                lblName.Text = row.NAME;
                lblAge.Text = row.AGE.ToString();
                lblSex.Text = row.SEX;
                if (row.SEX == "男")
                {
                    lblPicture.BackgroundImage = Properties.Resources.boy;
                }
                else
                {
                    lblPicture.BackgroundImage = Properties.Resources.gril;
                }
            }
        }

        /// <summary>
        /// 文档选择打印
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tlDocments_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {

        }

        private string ConvertToString(object o)
        {
            if (o == null)
                return string.Empty;
            if (o == DBNull.Value || o is DBNull)
                return string.Empty;
            return o.ToString();
        }

        private void lblReturn_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        int fCount = 0;
        private void PatientKnowBooks_Load(object sender, EventArgs e)
        {
            if (fCount == 0)
            {
                this.Show();
                this.lblRoom.Text = areaName;
                this.WindowState = FormWindowState.Normal;
                this.WindowState = FormWindowState.Maximized;
                this.Activate();
                fCount = 1;
            }
            dtServer = _configService.GetConfigList(string.Empty, string.Empty, "患者资料上传", "1");
            BindSignType();
        }

        private void tlDocments_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            var row = _patientDocRow;
            _medicalDocContainer.HaveNextPage = false;
            HemoModel.MED_BOOK_PICTUREDataTable dtBookPicture = _hemodialysisService.GetBookPictureByHemoAndBookName(row.HEMODIALYSIS_ID, e.Node.Text);

            //检查是否上传过同意书扫描件，有的话加载扫描件
            if (e.Node.Text == "血液净化治疗知情同意书")
            {
                if (e.Node.Checked)
                {
                    byte[] data = GetBookImageData(e.Node.Text);
                    if (!_medicalDocContainer.Add("血液净化治疗知情同意书"))
                    {
                        if (data != null && data.Length > 0)
                        {
                            CtlBook book = new CtlBook(data);
                            _medicalDocContainer.Add("血液净化治疗知情同意书", book);
                        }
                        else
                        {
                            血液净化治疗知情同意书 document = new 血液净化治疗知情同意书();
                            if (row != null)
                            {
                                document.PatientRow = row;
                                document.PatientName = row.NAME;
                                document.BookPicture = (dtBookPicture != null && dtBookPicture.Rows.Count > 0) ? dtBookPicture[0].BOOK_PICTURE : null;
                                document.AdmissionNumber = row.IsADMISSION_NUMBERNull() == true ? "" : row.ADMISSION_NUMBER;
                                document.Diagnose = row.IsDIAGNOSENull() == true ? "" : row.DIAGNOSE;
                            }
                            _medicalDocContainer.Add("血液净化治疗知情同意书", document);
                        }
                    }
                }
                else
                {
                    _medicalDocContainer.Remove("血液净化治疗知情同意书");
                }
            }
            else if (e.Node.Text == "连续性肾脏替代治疗知情同意书")
            {
                if (e.Node.Checked)
                {
                    byte[] data = GetBookImageData(e.Node.Text);
                    if (!_medicalDocContainer.Add("连续性肾脏替代治疗知情同意书"))
                    {
                        if (data != null && data.Length > 0)
                        {
                            CtlBook book = new CtlBook(data);
                            _medicalDocContainer.Add("连续性肾脏替代治疗知情同意书", book);
                        }
                        {
                            连续性肾脏替代治疗知情同意书 document = new 连续性肾脏替代治疗知情同意书();
                            if (row != null)
                            {
                                document.PatientRow = row;
                                document.BookPicture = (dtBookPicture != null && dtBookPicture.Rows.Count > 0) ? dtBookPicture[0].BOOK_PICTURE : null;
                                document.LoadDocumentInfo();
                            }
                            _medicalDocContainer.Add("连续性肾脏替代治疗知情同意书", document);
                        }
                    }
                }
                else
                {
                    _medicalDocContainer.Remove("连续性肾脏替代治疗知情同意书");
                }
            }
            else if (e.Node.Text == "血液透析病历首页")
            {
                if (e.Node.Checked)
                {
                    byte[] data = GetBookImageData(e.Node.Text);
                    if (!_medicalDocContainer.Add("血液透析病历首页"))
                    {
                        if (data != null && data.Length > 0)
                        {
                            CtlBook book = new CtlBook(data);
                            _medicalDocContainer.Add("血液透析病历首页", book);
                        }
                        {
                            血液透析病历首页 document = new 血液透析病历首页();
                            if (row != null)
                            {
                                document.PatientRow = row;
                                //document.BookPicture = (dtBookPicture != null && dtBookPicture.Rows.Count > 0) ? dtBookPicture[0].BOOK_PICTURE : null;
                                document.LoadDocumentInfo();
                            }
                            _medicalDocContainer.Add("血液透析病历首页", document);
                        }
                    }
                }
                else
                {
                    _medicalDocContainer.Remove("血液透析病历首页");
                }
            }
            else if (e.Node.Text == "血液灌流知情同意书")
            {
                if (e.Node.Checked)
                {
                    byte[] data = GetBookImageData(e.Node.Text);
                    if (!_medicalDocContainer.Add("血液灌流知情同意书"))
                    {
                        if (data != null && data.Length > 0)
                        {
                            CtlBook book = new CtlBook(data);
                            _medicalDocContainer.Add("血液灌流知情同意书", book);
                        }
                        {
                            血液灌流知情同意书 document = new 血液灌流知情同意书();
                            if (row != null)
                            {
                                document.PatientRow = row;
                                document.BookPicture = (dtBookPicture != null && dtBookPicture.Rows.Count > 0) ? dtBookPicture[0].BOOK_PICTURE : null;
                                document.LoadDocumentInfo();
                            }
                            _medicalDocContainer.Add("血液灌流知情同意书", document);
                        }
                    }
                }
                else
                {
                    _medicalDocContainer.Remove("血液灌流知情同意书");
                }
            }
            else if (e.Node.Text == "中心静脉置管术知情同意书")
            {
                if (e.Node.Checked)
                {
                    byte[] data = GetBookImageData(e.Node.Text);
                    if (!_medicalDocContainer.Add("中心静脉置管术知情同意书"))
                    {
                        if (data != null && data.Length > 0)
                        {
                            CtlBook book = new CtlBook(data);
                            _medicalDocContainer.Add("中心静脉置管术知情同意书", book);
                        }
                        {
                            中心静脉置管术知情同意书 document = new 中心静脉置管术知情同意书();
                            if (row != null)
                            {
                                document.PatientRow = row;
                                document.BookPicture = (dtBookPicture != null && dtBookPicture.Rows.Count > 0) ? dtBookPicture[0].BOOK_PICTURE : null;
                                document.LoadDocumentInfo();
                            }
                            _medicalDocContainer.Add("中心静脉置管术知情同意书", document);
                        }
                    }
                }
                else
                {
                    _medicalDocContainer.Remove("中心静脉置管术知情同意书");
                }
            }
            else if (e.Node.Text == "动静脉内瘘血管吻合术同意书")
            {
                if (e.Node.Checked)
                {
                    byte[] data = GetBookImageData(e.Node.Text);
                    if (!_medicalDocContainer.Add("动静脉内瘘血管吻合术同意书"))
                    {
                        if (data != null && data.Length > 0)
                        {
                            CtlBook book = new CtlBook(data);
                            _medicalDocContainer.Add("动静脉内瘘血管吻合术同意书", book);
                        }
                        {
                            动静脉内瘘血管吻合术同意书 document = new 动静脉内瘘血管吻合术同意书();
                            if (row != null)
                            {
                                document.PatientRow = row;
                                document.BookPicture = (dtBookPicture != null && dtBookPicture.Rows.Count > 0) ? dtBookPicture[0].BOOK_PICTURE : null;
                                document.LoadDocumentInfo();
                            }
                            _medicalDocContainer.Add("动静脉内瘘血管吻合术同意书", document);
                        }
                    }
                }
                else
                {
                    _medicalDocContainer.Remove("动静脉内瘘血管吻合术同意书");
                }
            }
            else if (e.Node.Text == "授权委托书")
            {
                if (e.Node.Checked)
                {
                    byte[] data = GetBookImageData(e.Node.Text);
                    if (!_medicalDocContainer.Add("授权委托书"))
                    {
                        if (data != null && data.Length > 0)
                        {
                            CtlBook book = new CtlBook(data);
                            _medicalDocContainer.Add("授权委托书", book);
                        }
                        {
                            授权委托书 document = new 授权委托书();
                            if (row != null)
                            {
                                document.PatientRow = row;
                                document.BookPicture = (dtBookPicture != null && dtBookPicture.Rows.Count > 0) ? dtBookPicture[0].BOOK_PICTURE : null;
                                document.LoadDocumentInfo();
                            }
                            _medicalDocContainer.Add("授权委托书", document);
                        }
                    }
                }
                else
                {
                    _medicalDocContainer.Remove("授权委托书");
                }
            }
            //else if (e.Node.Text == "透析器重复使用申请书") {
            //    if (e.Node.Checked) {
            //        if (!_medicalDocContainer.Add("透析器重复使用申请书")) {
            //            透析器重复使用申请书 document = new 透析器重复使用申请书();
            //            if (row != null) {
            //                document.PatientRow = row;
            //                document.LoadDocumentInfo();
            //            }
            //            _medicalDocContainer.Add("透析器重复使用申请书", document);
            //        }
            //    }
            //    else {
            //        _medicalDocContainer.Remove("透析器重复使用申请书");
            //    }
            //}
            //else if (e.Node.Text == "血液透析病历") {
            //    if (e.Node.Checked) {
            //        if (!_medicalDocContainer.Add("血液透析病历")) {
            //            血液透析病历 document = new 血液透析病历();
            //            if (row != null) {
            //                document.PatientRow = row;
            //                document.LoadDocumentInfo();
            //            }
            //            _medicalDocContainer.Add("血液透析病历", document);
            //        }
            //    }
            //    else {
            //        _medicalDocContainer.Remove("血液透析病历");
            //    }
            //}
            else if (e.Node.Text == "枸橼酸抗凝同意书")
            {
                if (e.Node.Checked)
                {
                    byte[] data = GetBookImageData(e.Node.Text);
                    if (!_medicalDocContainer.Add("枸橼酸抗凝同意书"))
                    {
                        if (data != null && data.Length > 0)
                        {
                            CtlBook book = new CtlBook(data);
                            _medicalDocContainer.Add("枸橼酸抗凝同意书", book);
                        }
                        {
                            枸橼酸抗凝同意书 document = new 枸橼酸抗凝同意书();
                            if (row != null)
                            {
                                document.PatientRow = row;
                                document.BookPicture = (dtBookPicture != null && dtBookPicture.Rows.Count > 0) ? dtBookPicture[0].BOOK_PICTURE : null;
                                document.LoadDocumentInfo();
                            }
                            _medicalDocContainer.Add("枸橼酸抗凝同意书", document);
                        }
                    }
                }
                else
                {
                    _medicalDocContainer.Remove("枸橼酸抗凝同意书");
                }
            }
            else if (e.Node.Text == "急诊施行血液灌流同意书")
            {
                if (e.Node.Checked)
                {
                    byte[] data = GetBookImageData(e.Node.Text);
                    if (!_medicalDocContainer.Add("急诊施行血液灌流同意书"))
                    {
                        if (data != null && data.Length > 0)
                        {
                            CtlBook book = new CtlBook(data);
                            _medicalDocContainer.Add("急诊施行血液灌流同意书", book);
                        }
                        {
                            急诊施行血液灌流同意书 document = new 急诊施行血液灌流同意书();
                            if (row != null)
                            {
                                document.PatientRow = row;
                                document.BookPicture = (dtBookPicture != null && dtBookPicture.Rows.Count > 0) ? dtBookPicture[0].BOOK_PICTURE : null;
                                document.LoadDocumentInfo();
                            }
                            _medicalDocContainer.Add("急诊施行血液灌流同意书", document);
                        }
                    }
                }
                else
                {
                    _medicalDocContainer.Remove("急诊施行血液灌流同意书");
                }
            }
            else if (e.Node.Text == "抗生素皮试知情同意书")
            {
                if (e.Node.Checked)
                {
                    byte[] data = GetBookImageData(e.Node.Text);
                    if (!_medicalDocContainer.Add("抗生素皮试知情同意书"))
                    {
                        if (data != null && data.Length > 0)
                        {
                            CtlBook book = new CtlBook(data);
                            _medicalDocContainer.Add("抗生素皮试知情同意书", book);
                        }
                        {
                            抗生素皮试知情同意书 document = new 抗生素皮试知情同意书();
                            if (row != null)
                            {
                                document.PatientRow = row;
                                document.BookPicture = (dtBookPicture != null && dtBookPicture.Rows.Count > 0) ? dtBookPicture[0].BOOK_PICTURE : null;
                                document.LoadDocumentInfo();
                            }
                            _medicalDocContainer.Add("抗生素皮试知情同意书", document);
                        }
                    }
                }
                else
                {
                    _medicalDocContainer.Remove("抗生素皮试知情同意书");
                }
            }
            else if (e.Node.Text == "血透同意书")
            {
                if (e.Node.Checked)
                {
                    byte[] data = GetBookImageData(e.Node.Text);
                    if (!_medicalDocContainer.Add("血透同意书"))
                    {
                        if (data != null && data.Length > 0)
                        {
                            CtlBook book = new CtlBook(data);
                            _medicalDocContainer.Add("血透同意书", book);
                        }
                        {
                            血透同意书 document = new 血透同意书();
                            if (row != null)
                            {
                                document.PatientRow = row;
                                document.BookPicture = (dtBookPicture != null && dtBookPicture.Rows.Count > 0) ? dtBookPicture[0].BOOK_PICTURE : null;
                                document.LoadDocumentInfo();
                            }
                            _medicalDocContainer.Add("血透同意书", document);
                        }
                    }
                }
                else
                {
                    _medicalDocContainer.Remove("血透同意书");
                }
            }
            else if (e.Node.Text == "术后告知")
            {
                if (e.Node.Checked)
                {
                    byte[] data = GetBookImageData(e.Node.Text);
                    if (!_medicalDocContainer.Add("术后告知"))
                    {
                        if (data != null && data.Length > 0)
                        {
                            CtlBook book = new CtlBook(data);
                            _medicalDocContainer.Add("术后告知", book);
                        }
                        {
                            术后告知 document = new 术后告知();
                            if (row != null)
                            {
                                document.BookPicture = (dtBookPicture != null && dtBookPicture.Rows.Count > 0) ? dtBookPicture[0].BOOK_PICTURE : null;
                                document.LoadDocumentInfo();
                            }
                            _medicalDocContainer.Add("术后告知", document);
                        }
                    }
                }
                else
                {
                    _medicalDocContainer.Remove("术后告知");
                }
            }
            else if (e.Node.Text == "血透病人告知书")
            {
                if (e.Node.Checked)
                {
                    byte[] data = GetBookImageData(e.Node.Text);
                    if (!_medicalDocContainer.Add("血透病人告知书"))
                    {
                        if (data != null && data.Length > 0)
                        {
                            CtlBook book = new CtlBook(data);
                            _medicalDocContainer.Add("血透病人告知书", book);
                        }
                        {
                            血透病人告知书 document = new 血透病人告知书();
                            if (row != null)
                            {
                                document.PatientRow = row;
                                document.BookPicture = (dtBookPicture != null && dtBookPicture.Rows.Count > 0) ? dtBookPicture[0].BOOK_PICTURE : null;
                                document.LoadDocumentInfo();
                            }
                            _medicalDocContainer.Add("血透病人告知书", document);
                        }
                    }
                }
                else
                {
                    _medicalDocContainer.Remove("血透病人告知书");
                }
            }
            //else if (e.Node.Text == "手术术前风险评估") {
            //    if (e.Node.Checked) {
            //        if (!_medicalDocContainer.Add("手术术前风险评估")) {
            //            手术术前风险评估 document = new 手术术前风险评估();
            //            if (row != null) {
            //                document.PatientRow = row;
            //                document.LoadDocumentInfo();
            //            }
            //            _medicalDocContainer.Add("手术术前风险评估", document);
            //        }
            //    }
            //    else {
            //        _medicalDocContainer.Remove("手术术前风险评估");
            //    }
            //}
            //else if (e.Node.Text == "手术安全核查表") {
            //    if (e.Node.Checked) {
            //        if (!_medicalDocContainer.Add("手术安全核查表")) {
            //            手术安全核查表 document = new 手术安全核查表();
            //            if (row != null) {
            //                document.PatientRow = row;
            //                document.LoadDocumentInfo();
            //            }
            //            _medicalDocContainer.Add("手术安全核查表", document);
            //        }
            //    }
            //    else {
            //        _medicalDocContainer.Remove("手术安全核查表");
            //    }
            //}
            else if (e.Node.Text == "穿刺风险告之书")
            {
                if (e.Node.Checked)
                {
                    byte[] data = GetBookImageData(e.Node.Text);
                    if (!_medicalDocContainer.Add("穿刺风险告之书"))
                    {
                        if (data != null && data.Length > 0)
                        {
                            CtlBook book = new CtlBook(data);
                            _medicalDocContainer.Add("穿刺风险告之书", book);
                        }
                        {
                            血管穿刺风险告之书 document = new 血管穿刺风险告之书();
                            if (row != null)
                            {
                                document.BookPicture = (dtBookPicture != null && dtBookPicture.Rows.Count > 0) ? dtBookPicture[0].BOOK_PICTURE : null;
                                document.LoadDocumentInfo();
                            }
                            _medicalDocContainer.Add("穿刺风险告之书", document);
                        }
                    }
                }
                else
                {
                    _medicalDocContainer.Remove("穿刺风险告之书");
                }
            }
            else if (e.Node.Text == "自购药品使用知情同意书")
            {
                if (e.Node.Checked)
                {
                    byte[] data = GetBookImageData(e.Node.Text);
                    if (!_medicalDocContainer.Add("自购药品使用知情同意书"))
                    {
                        if (data != null && data.Length > 0)
                        {
                            CtlBook book = new CtlBook(data);
                            _medicalDocContainer.Add("自购药品使用知情同意书", book);
                        }
                        {
                            自购药品使用知情同意书 document = new 自购药品使用知情同意书();
                            if (row != null)
                            {
                                document.PatientRow = row;
                                document.BookPicture = (dtBookPicture != null && dtBookPicture.Rows.Count > 0) ? dtBookPicture[0].BOOK_PICTURE : null;
                                document.LoadDocumentInfo();
                            }
                            _medicalDocContainer.Add("自购药品使用知情同意书", document);
                        }
                    }
                }
                else
                {
                    _medicalDocContainer.Remove("自购药品使用知情同意书");
                }
            }
            else if (e.Node.Text == "无肝素血液透析风险知情同意书")
            {
                if (e.Node.Checked)
                {
                    byte[] data = GetBookImageData(e.Node.Text);
                    if (!_medicalDocContainer.Add("无肝素血液透析风险知情同意书"))
                    {
                        if (data != null && data.Length > 0)
                        {
                            CtlBook book = new CtlBook(data);
                            _medicalDocContainer.Add("无肝素血液透析风险知情同意书", book);
                        }
                        {
                            无肝素血液透析风险知情同意书 document = new 无肝素血液透析风险知情同意书();
                            if (row != null)
                            {
                                document.PatientRow = row;
                                document.BookPicture = (dtBookPicture != null && dtBookPicture.Rows.Count > 0) ? dtBookPicture[0].BOOK_PICTURE : null;
                                document.LoadDocumentInfo();
                            }
                            _medicalDocContainer.Add("无肝素血液透析风险知情同意书", document);
                        }
                    }
                }
                else
                {
                    _medicalDocContainer.Remove("无肝素血液透析风险知情同意书");
                }
            }
            else if (e.Node.Text == "危重病人急诊行床边血液净化治疗同意书")
            {
                if (e.Node.Checked)
                {
                    byte[] data = GetBookImageData(e.Node.Text);
                    if (!_medicalDocContainer.Add("危重病人急诊行床边血液净化治疗同意书"))
                    {
                        if (data != null && data.Length > 0)
                        {
                            CtlBook book = new CtlBook(data);
                            _medicalDocContainer.Add("危重病人急诊行床边血液净化治疗同意书", book);
                        }
                        {
                            危重病人急诊行床边血液净化治疗同意书 document = new 危重病人急诊行床边血液净化治疗同意书();
                            if (row != null)
                            {
                                document.PatientRow = row;
                                document.BookPicture = (dtBookPicture != null && dtBookPicture.Rows.Count > 0) ? dtBookPicture[0].BOOK_PICTURE : null;
                                document.LoadDocumentInfo();
                            }
                            _medicalDocContainer.Add("危重病人急诊行床边血液净化治疗同意书", document);
                        }
                    }
                }
                else
                {
                    _medicalDocContainer.Remove("危重病人急诊行床边血液净化治疗同意书");
                }
            }
            else if (e.Node.Text == "透析器重复使用风险知情同意书")
            {
                if (e.Node.Checked)
                {
                    byte[] data = GetBookImageData(e.Node.Text);
                    if (!_medicalDocContainer.Add("透析器重复使用风险知情同意书"))
                    {
                        if (data != null && data.Length > 0)
                        {
                            CtlBook book = new CtlBook(data);
                            _medicalDocContainer.Add("透析器重复使用风险知情同意书", book);
                        }
                        {
                            透析器重复使用风险知情同意书 document = new 透析器重复使用风险知情同意书();
                            if (row != null)
                            {
                                document.PatientRow = row;
                                document.LoadDocumentInfo();
                            }
                            _medicalDocContainer.Add("透析器重复使用风险知情同意书", document);
                        }
                    }
                }
                else
                {
                    _medicalDocContainer.Remove("透析器重复使用风险知情同意书");
                }
            }
            //else if (e.Node.Text == "血液透析记录单") {
            //    if (e.Node.Checked) {
            //        if (!_medicalDocContainer.Add("血液透析记录单")) {
            //            血液透析记录单 document = new 血液透析记录单();
            //            _medicalDocContainer.Add("血液透析记录单", document);
            //        }
            //    }
            //    else {
            //        _medicalDocContainer.Remove("血液透析记录单");
            //    }
            //}
            else if (e.Node.Tag != null)
            {
                if (e.Node.Checked)
                {
                    if (!_medicalDocContainer.Add(e.Node.Tag.ToString()))
                    {
                        loadDocumentByCureID(e.Node.Tag.ToString());
                    }
                    else
                    {
                        Visibility imgVisible = this.lupSignType.EditValue.ToString().Equals("0") ? Visibility.Hidden : Visibility.Visible;
                        Visibility txtVisible = this.lupSignType.EditValue.ToString().Equals("0") ? Visibility.Visible : Visibility.Hidden;
                        WPF_DocumentBase doc = _medicalDocContainer.GetDoc(e.Node.Tag.ToString());
                        SetImageAndTextBoxVisible(doc, imgVisible, txtVisible);
                    }
                }
                else
                {
                    if (!string.IsNullOrEmpty(areaName) && areaName.Equals("CRRT"))
                    {
                        HemodialysisModel.MED_CURE_MAIN_CRRTDataTable dtCRRTCure = _hemodialysisService.GetCRRTCureByCureId(e.Node.Tag.ToString());
                        if (dtCRRTCure != null && dtCRRTCure.Rows.Count > 0)
                        {
                            dtCRRTCure.AsEnumerable().ToList().ForEach(crrt =>
                            {
                                _medicalDocContainer.Remove(crrt.ID);
                            });
                        }
                    }
                    else
                    {
                        _medicalDocContainer.Remove(e.Node.Tag.ToString());
                    }
                }
            }
            else
            {
                //   MessageBox.Show(e.Node.Text + e.Node.ToolTipText);
                string[] strCureList;
                if (e.Node.Text.IndexOf("年") > -1 && e.Node.Checked == true)
                {
                    ChangeChild(e.Node, true);
                    strCureList = lblCureIDList.Text.Split(',');
                    if (lblCureIDList.Text.Length > 0)
                    {
                        //  MessageBox.Show(lblCureIDList.Text);
                        for (int c = 0; c < strCureList.Length; c++)
                        {
                            if (strCureList[c].Length > 0)
                            {
                                //DataSet ds = _hemodialysisService.GetAllCure(strCureList[c].ToString());
                                loadDocumentByCureID(strCureList[c].ToString());
                            }
                        }
                    }
                }
                else if (e.Node.Text.IndexOf("年") > -1 && e.Node.Checked == false)
                {
                    //  MessageBox.Show(lblCureIDList.Text);
                    strCureList = lblCureIDList.Text.Split(',');
                    for (int c = 0; c < strCureList.Length; c++)
                    {
                        if (strCureList[c].Length > 0)
                        {
                            if (!string.IsNullOrEmpty(areaName) && areaName.Equals("CRRT"))
                            {
                                HemodialysisModel.MED_CURE_MAIN_CRRTDataTable dtCRRTCure = _hemodialysisService.GetCRRTCureByCureId(strCureList[c].ToString());
                                if (dtCRRTCure != null && dtCRRTCure.Rows.Count > 0)
                                {
                                    dtCRRTCure.AsEnumerable().ToList().ForEach(crrt =>
                                    {
                                        _medicalDocContainer.Remove(crrt.ID);
                                    });
                                }
                            }
                            else
                            {
                                _medicalDocContainer.Remove(strCureList[c].ToString());
                            }
                        }
                    }
                    this.busyIndicator1.ShowLoadingScreenFor(this.documentContainerHost);
                    ChangeChild(e.Node, false);
                    this.busyIndicator1.HideLoadingScreen();
                    //   MessageBox.Show(lblCureIDList.Text);
                }
            }
            documentContainerHost.Child = _medicalDocContainer;
        }

        /// <summary>
        /// 根据CureID载入透析档案
        /// </summary>
        /// <param name="pCureID"></param>
        private void loadDocumentByCureID(string pCureID)
        {
            //   this.busyIndicator1.ShowLoadingScreenFor(this);

            DataSet ds = null;
            using (BackgroundWorker worker = new BackgroundWorker())
            {
                worker.DoWork += delegate(object sender1, DoWorkEventArgs e1)
                {
                    ds = _hemodialysisService.GetAllCure(pCureID);
                };
                worker.RunWorkerCompleted += delegate(object sender2, RunWorkerCompletedEventArgs e2)
                {
                    if (ds != null)
                    {
                        PatientScheduleModel.MED_PATIENT_SCHEDULERow currentPatientSchedule = null;
                        if (ds.Tables["MED_CURE_MAIN"] != null)
                        {
                            var row = ds.Tables["MED_CURE_MAIN"].Rows[0];
                            currentPatientSchedule = scheduleService.GetPatientScheduleSignle(Utility.CDate(row["CURE_CREATE_DATE"].ToString()), row["HEMODIALYSIS_ID"].ToString())[0];
                            areaName = currentPatientSchedule["AREANAME"].ToString();
                        }

                        if (areaName.Equals("CRRT"))
                        {
                            //CRRT患者
                            HemodialysisModel.MED_CURE_MAIN_CRRTDataTable dtCRRTCure = _hemodialysisService.GetCRRTCureByCureId(pCureID);
                            if (dtCRRTCure != null && dtCRRTCure.Rows.Count > 0)
                            {
                                dtCRRTCure.AsEnumerable().ToList().ForEach(row =>
                                {
                                    DoPage(row.ID, areaName, ds, row, currentPatientSchedule);
                                });
                            }
                        }
                        else
                        {
                            //非CRRT患者
                            DoPage(pCureID, areaName, ds, null, currentPatientSchedule);
                        }
                    }
                };
                worker.RunWorkerAsync();
            }
        }
        public const int WordPixel = 408;

        /// <summary>
        /// 获取字符横向所占像素数
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public int GetPixelb(string str, System.Drawing.Graphics currentGps)
        {
            System.Drawing.Font font;
            System.Windows.Forms.PictureBox pb = new System.Windows.Forms.PictureBox();
            System.Drawing.Graphics g = currentGps == null ? pb.CreateGraphics() : currentGps;// pb.CreateGraphics();
            g.PageUnit = System.Drawing.GraphicsUnit.Pixel;
            int len;
            if (Encoding.Default.GetByteCount(str) == 2)
            {
                font = new System.Drawing.Font("SimSun", 12, System.Drawing.GraphicsUnit.Pixel);
                len = (int)(Math.Round(g.MeasureString(str, font).Width) * 0.75);
            }
            else
            {
                font = new System.Drawing.Font("Arial", 12, System.Drawing.GraphicsUnit.Pixel);
                len = (int)(Math.Round(g.MeasureString(str, font).Width) * 0.7);
            }
            font.Dispose();
            font = null;
            g.Dispose();
            g = null;
            pb.Dispose();
            pb = null;
            return len;
        }
        /// <summary>
        /// 处理分页
        /// </summary>
        /// <param name="cureId"></param>
        /// <param name="areaName"></param>
        /// <param name="dsResult"></param>
        /// <param name="rowCRRT"></param>
        /// <param name="currentPatientSchedule"></param>
        private void DoPage(string cureId, string areaName, DataSet dsResult, HemodialysisModel.MED_CURE_MAIN_CRRTRow rowCRRT, PatientScheduleModel.MED_PATIENT_SCHEDULERow currentPatientSchedule)
        {
            int countNum = 0;
            int countParam;
            int pageParamCount = 10;
            WPF_DocumentBase document = null;
            //CtlMedicalDocument3 document1 = null;
            //CtlMedicalDocument3 document2 = null;
            //CtlMedicalDocument3 document3 = null;
            //CtlMedicalDocument3 document4 = null;

            if (areaName.Equals("CRRT"))
            {
                //CRRT患者
                if (dsResult.Tables["MED_HEMODIALYSIS_PARAMETERS"] != null && rowCRRT != null)
                {
                    var rows = dsResult.Tables["MED_HEMODIALYSIS_PARAMETERS"].AsEnumerable().Where(r => Utility.CDate(r["CREATE_DATE"].ToString()).Date.CompareTo(rowCRRT.CREATE_DATE.Date) == 0 && r["CRRT_CLASS"].ToString().Equals(rowCRRT.CRRT_CLASS));
                    countNum = rows != null ? rows.Count() : 0;
                }
            }
            else
            {
                //非CRRT患者
                if (dsResult.Tables["MED_HEMODIALYSIS_PARAMETERS"] != null)
                {
                    countNum = dsResult.Tables["MED_HEMODIALYSIS_PARAMETERS"].Rows.Count;
                }
            }

            #region 计算这些透析参数能够显示多少行。
            var dtHemoParametersTemp1 = dsResult.Tables["MED_HEMODIALYSIS_PARAMETERS"] as HemodialysisModel.MED_HEMODIALYSIS_PARAMETERSDataTable;
            var dtHemoParameters = new HemodialysisModel.MED_HEMODIALYSIS_PARAMETERSDataTable();
            var dtHemoParametersTemp = new HemodialysisModel.MED_HEMODIALYSIS_PARAMETERSDataTable();
            dtHemoParametersTemp1.CopyToDataTable<HemodialysisModel.MED_HEMODIALYSIS_PARAMETERSRow>(dtHemoParametersTemp, LoadOption.PreserveChanges);

            var itemCollet = new List<string>();
            int p = 0;
            foreach (HemodialysisModel.MED_HEMODIALYSIS_PARAMETERSRow item in dtHemoParametersTemp.Rows)
            {
                p = 0;
                if (!item.IsCLINICAL_MANIFESTATIONNull())
                {
                    List<string> cureResultList = new List<string>();
                    string cureResult = "";
                    string cureResultTotal = string.Empty;
                    //遍历循环数据是不是超出了一行所能放的区域
                    foreach (string str in item.CLINICAL_MANIFESTATION.Split('\r'))
                    {
                        cureResultTotal = "";
                        if (string.IsNullOrEmpty(str))
                        {
                            cureResultList.Add(cureResult);
                        }
                        else
                        {
                            StringBuilder strbuilder = new StringBuilder();
                            int totalLen = 0;
                            for (int i = 0; i < str.Length; i++)
                            {
                                totalLen += GetPixelb(str[i].ToString(), null);
                                if (totalLen < WordPixel)
                                {
                                    strbuilder.Append(str[i]);
                                    if (i == str.Length - 1)
                                    {
                                        totalLen = 0;
                                        cureResultList.Add(strbuilder.ToString());
                                        strbuilder.Clear();
                                    }
                                }
                                else
                                {
                                    totalLen = 0;
                                    i--;
                                    cureResultList.Add(strbuilder.ToString());
                                    strbuilder.Clear();
                                }
                            }
                            strbuilder.Clear();
                            strbuilder = null;
                        }
                    }
                    string allStr = string.Empty;
                    foreach (string itemStr in cureResultList)
                    {
                        allStr += string.Format("{0}$", itemStr);
                    }

                    item.CLINICAL_MANIFESTATION = allStr;



                    var splitItem = item.CLINICAL_MANIFESTATION.Split('$');


                    foreach (var sitem in splitItem)
                    {
                        if (!string.IsNullOrEmpty(sitem))
                        {
                            p++;
                            item.CLINICAL_MANIFESTATION = sitem;
                            if (p == 1)
                                dtHemoParameters.LoadDataRow(item.ItemArray, LoadOption.PreserveChanges);
                            else
                            {
                                var dr = dtHemoParameters.NewMED_HEMODIALYSIS_PARAMETERSRow();
                                dr.HEMODIALYSIS_PARAMETERS_ID = System.Guid.NewGuid().ToString();
                                dr.CLINICAL_MANIFESTATION = sitem;
                                dr.CURE_ID = item.CURE_ID;
                                dr.RECIPE_ID = item.RECIPE_ID;
                                dtHemoParameters.AddMED_HEMODIALYSIS_PARAMETERSRow(dr);
                            }
                        }
                    }
                }
                else
                {
                    dtHemoParameters.LoadDataRow(item.ItemArray, LoadOption.PreserveChanges);
                }
            }

            countNum = dtHemoParameters.Rows.Count;
            #endregion
            if (areaName.Equals("CRRT"))
            {
                document = new CtlMedicalDocumentCRRTNew(currentPatientSchedule, dsResult, 90, 10, rowCRRT.CRRT_CLASS, rowCRRT.CREATE_DATE);
                pageParamCount = document.paramRowNum;

            }
            else
            {
                document = new CtlMedicalDocumentNew(currentPatientSchedule, dsResult);
                pageParamCount = document.paramRowNum;

            }

            _medicalDocContainer.Add(cureId, document);

            document.IsShowGrid(true);

            //进行计算分页
            //计算可以有多少页的参数页
            int pageCount = (countNum - pageParamCount) / 24;
            int pageCountExt = (countNum - pageParamCount) % 24;
            if (pageCountExt > 0)
            {
                pageCount++;
            }
            CtlMedicalDocument3New docNext = null;
            countParam = countNum - pageParamCount;

            for (int i = 2; i < pageCount + 2; i++)
            {
                _medicalDocContainer.Remove(cureId + i.ToString());

                string area = areaName.Equals("CRRT室") ? "CRRT" : areaName;
                CtlMedicalDocument3New document1 = area.Equals("CRRT") ? new CtlMedicalDocument3New(dsResult, rowCRRT, pageParamCount, 20, 1, i, area) : new CtlMedicalDocument3New(dsResult, pageParamCount, 20, "sqlByParams", i, area);
                pageParamCount += 24;

                _medicalDocContainer.Add(cureId + i.ToString(), document1);
                //countParam = countParam - 20;
            }


            #region 注掉


            //if (countNum > 90)
            //{
            //    if (areaName.Equals("CRRT"))
            //    {
            //        document = new CtlMedicalDocumentCRRTNew(currentPatientSchedule, dsResult, 90, 10, rowCRRT.CRRT_CLASS, rowCRRT.CREATE_DATE);
            //    }
            //    else
            //    {
            //        document = new CtlMedicalDocumentNew(currentPatientSchedule, dsResult);
            //    }
            //    document.IsShowGrid(true);
            //}
            //else
            //{
            //    if (areaName.Equals("CRRT"))
            //    {
            //        document = new CtlMedicalDocumentCRRTNew(currentPatientSchedule, dsResult, countNum, 10, rowCRRT.CRRT_CLASS, rowCRRT.CREATE_DATE);
            //    }
            //    else
            //    {
            //        document = new CtlMedicalDocumentNew(currentPatientSchedule, dsResult);
            //    }
            //    document.IsShowGrid(true);
            //}

            #endregion
            if (dsResult.Tables["MED_CURE_MAIN"] != null)
            {
                bool haveNextPage = false;
                string[] records = null;

                if (areaName.Equals("CRRT"))
                {
                    if (rowCRRT.SUMMARY2.Length > 0 || rowCRRT.SUMMARY.Length > 110 || countNum > 0)
                    {
                        haveNextPage = true;
                        records = rowCRRT.SUMMARY2.Split("|".ToCharArray());
                    }
                }
                else
                {
                    if (ConvertToString(dsResult.Tables["MED_CURE_MAIN"].Rows[0]["SUMMARY2"]).Length > 0 || ConvertToString(dsResult.Tables["MED_CURE_MAIN"].Rows[0]["SUMMARY"]).Length > 110 || countNum > 0)
                    {
                        haveNextPage = true;
                        DataTable dtCureMain = dsResult.Tables["MED_CURE_MAIN"];
                        records = dtCureMain.Rows[0]["SUMMARY2"].ToString().Split("|".ToCharArray());
                    }
                }
                #region 以前的注掉
                /*
                if (haveNextPage)
                {
                    if (countNum > 10)
                    {
                        if (countNum < 31)
                        {
                            document1 = areaName.Equals("CRRT") ? new CtlMedicalDocument3(dsResult, rowCRRT, countNum, countNum - 10, 0, 2, areaName) : new CtlMedicalDocument3(dsResult, countNum, countNum - 10, "", 2, areaName);
                        }
                        else
                        {
                            if (countNum > 90)
                            {
                                countParam = 80;
                            }
                            else
                            {
                                countParam = countNum - 10;
                            }

                            document1 = areaName.Equals("CRRT") ? new CtlMedicalDocument3(dsResult, rowCRRT, countParam, 20, 1, 2, areaName) : new CtlMedicalDocument3(dsResult, countParam, 20, "sqlByParams", 2, areaName);
                        }
                    }
                    else
                    {
                        if (records.Length >= 1 && records[0].Trim().Length > 0)
                        {
                            document1 = areaName.Equals("CRRT") ? new CtlMedicalDocument3(dsResult, rowCRRT, countNum, countNum - 10, 0, 2, areaName) : new CtlMedicalDocument3(dsResult, countNum, countNum - 10, "", 2, areaName);
                        }
                    }

                    if (document1 != null) { document.NextPage = document1; }

                    if (countNum > 30)
                    {
                        if (countNum < 51)
                        {
                            document2 = areaName.Equals("CRRT") ? new CtlMedicalDocument3(dsResult, rowCRRT, countNum, countNum - 30, 0, 3, areaName) : new CtlMedicalDocument3(dsResult, countNum, countNum - 30, "", 3, areaName);
                        }
                        else
                        {
                            if (countNum > 90)
                            {
                                countParam = 60;
                            }
                            else
                            {
                                countParam = countNum - 30;
                            }
                            document2 = areaName.Equals("CRRT") ? new CtlMedicalDocument3(dsResult, rowCRRT, countParam, 20, 1, 3, areaName) : new CtlMedicalDocument3(dsResult, countParam, 20, "sqlByParams", 3, areaName);
                        }
                    }
                    else
                    {
                        if (records.Length >= 2 && records[1].Trim().Length > 0)
                        {
                            document2 = areaName.Equals("CRRT") ? new CtlMedicalDocument3(dsResult, rowCRRT, countNum, countNum - 30, 0, 3, areaName) : new CtlMedicalDocument3(dsResult, countNum, countNum - 30, "", 3, areaName);
                        }
                    }

                    if (document1 != null && document2 != null) { document1.NextPage = document2; }

                    if (countNum > 50)
                    {
                        if (countNum < 71)
                        {
                            document3 = areaName.Equals("CRRT") ? new CtlMedicalDocument3(dsResult, rowCRRT, countNum, countNum - 50, 0, 4, areaName) : new CtlMedicalDocument3(dsResult, countNum, countNum - 50, "", 4, areaName);
                        }
                        else
                        {
                            if (countNum > 90)
                            {
                                countParam = 40;
                            }
                            else
                            {
                                countParam = countNum - 50;
                            }
                            document3 = areaName.Equals("CRRT") ? new CtlMedicalDocument3(dsResult, rowCRRT, countParam, 20, 1, 4, areaName) : new CtlMedicalDocument3(dsResult, countParam, 20, "sqlByParams", 4, areaName);
                        }
                    }
                    else
                    {
                        if (records.Length >= 3 && records[2].Trim().Length > 0)
                        {
                            document3 = areaName.Equals("CRRT") ? new CtlMedicalDocument3(dsResult, rowCRRT, countNum, countNum - 50, 0, 4, areaName) : new CtlMedicalDocument3(dsResult, countNum, countNum - 50, "", 4, areaName);
                        }
                    }

                    if (document2 != null && document3 != null) { document2.NextPage = document3; }

                    if (countNum > 70)
                    {
                        if (countNum < 91)
                        {
                            document4 = areaName.Equals("CRRT") ? new CtlMedicalDocument3(dsResult, rowCRRT, countNum, countNum - 70, 0, 5, areaName) : new CtlMedicalDocument3(dsResult, countNum, countNum - 70, "", 5, areaName);
                        }
                        else
                        {
                            if (countNum > 90)
                            {
                                countParam = 20;
                            }
                            else
                            {
                                countParam = countNum - 70;
                            }
                            document4 = areaName.Equals("CRRT") ? new CtlMedicalDocument3(dsResult, rowCRRT, countParam, 20, 1, 5, areaName) : new CtlMedicalDocument3(dsResult, countParam, 20, "sqlByParams", 5, areaName);
                        }
                    }
                    else
                    {
                        if (records.Length >= 4 && records[3].Trim().Length > 0)
                        {
                            document4 = areaName.Equals("CRRT") ? new CtlMedicalDocument3(dsResult, rowCRRT, countNum, countNum - 70, 0, 5, areaName) : new CtlMedicalDocument3(dsResult, countNum, countNum - 70, "", 5, areaName);
                        }
                    }

                    if (document3 != null && document4 != null) { document3.NextPage = document4; }
                }
                */
                #endregion
                _medicalDocContainer.HaveNextPage = haveNextPage;
            }
            else
            {
                _medicalDocContainer.HaveNextPage = false;
            }

            Visibility imgVisible = this.lupSignType.EditValue.ToString().Equals("0") ? Visibility.Hidden : Visibility.Visible;
            Visibility txtVisible = this.lupSignType.EditValue.ToString().Equals("0") ? Visibility.Visible : Visibility.Hidden;
            SetImageAndTextBoxVisible(document, imgVisible, txtVisible);
        }

        //递归子节点跟随其全选或全不选
        private void ChangeChild(TreeNode node, bool state)
        {
            node.Checked = state;
            foreach (TreeNode tn in node.Nodes)
            {
                ChangeChild(tn, state);
            }
            if (state)
            {
                lblCureIDList.Text += node.Tag + ",";
            }
            else
            {
                lblCureIDList.Text = string.Empty;
            }
        }

        //递归父节点跟随其全选或全不选
        private void ChangeParent(TreeNode node)
        {
            if (node.Parent != null)
            {
                //兄弟节点被选中的个数
                int brotherNodeCheckedCount = 0;
                //遍历该节点的兄弟节点
                foreach (TreeNode tn in node.Parent.Nodes)
                {
                    if (tn.Checked == true)
                        brotherNodeCheckedCount++;
                }
                //兄弟节点全没选，其父节点也不选
                if (brotherNodeCheckedCount == 0)
                {
                    node.Parent.Checked = false;
                    ChangeParent(node.Parent);
                }
                //兄弟节点只要有一个被选，其父节点也被选
                if (brotherNodeCheckedCount >= 1)
                {
                    node.Parent.Checked = true;
                    ChangeParent(node.Parent);
                }
            }
        }

        /// <summary>
        /// 签名方式改变
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lupSignType_EditValueChanged(object sender, EventArgs e)
        {
            this.btnPDSign.Enabled = this.btnPNSign.Enabled = this.btnCNSign.Enabled = this.lupSignType.EditValue.ToString().Equals("0") ? false : true;
            Visibility imgVisible = this.lupSignType.EditValue.ToString().Equals("0") ? Visibility.Hidden : Visibility.Visible;
            Visibility txtVisible = this.lupSignType.EditValue.ToString().Equals("0") ? Visibility.Visible : Visibility.Hidden;
            var node = this.tlDocments.Nodes[0];
            foreach (TreeNode n in node.Nodes)
            {
                foreach (TreeNode t in n.Nodes)
                {
                    if (t.Checked)
                    {
                        WPF_DocumentBase doc = _medicalDocContainer.GetDoc(t.Tag.ToString());
                        SetImageAndTextBoxVisible(doc, imgVisible, txtVisible);
                    }
                }
            }
        }

        /// <summary>
        /// 患者签名
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSign_Click(object sender, EventArgs e)
        {
            Sign(SignTypeEnum.Patient);
        }

        /// <summary>
        /// 责任医生签名
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnPDSign_Click(object sender, EventArgs e)
        {
            Sign(SignTypeEnum.PrimaryDoctor);
        }

        /// <summary>
        /// 责任护士签名
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnPNSign_Click(object sender, EventArgs e)
        {
            Sign(SignTypeEnum.PrimaryNurse);
        }

        /// <summary>
        /// 审核护士签名
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCNSign_Click(object sender, EventArgs e)
        {
            Sign(SignTypeEnum.CheckNurse);
        }

        /// <summary>
        /// 上传扫描件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnUpload_Click(object sender, EventArgs e)
        {
            UploadPatientFile uploadFile = new UploadPatientFile();
            uploadFile.Patient = _patientDocRow;
            DialogResult result = uploadFile.ShowDialog();
            if (result == DialogResult.OK)
            {
                var node = this.tlDocments.Nodes[1];
                //移除选择的同意书
                foreach (TreeNode n in node.Nodes)
                {
                    if (n.Checked)
                    {
                        _medicalDocContainer.RemoveDoc(n.Text);
                    }
                }
                //重新加载同意书
                foreach (TreeNode n in node.Nodes)
                {
                    if (n.Checked)
                    {
                        tlDocments_NodeMouseClick(this.tlDocments, new TreeNodeMouseClickEventArgs(n, MouseButtons.Left, 1, 0, 0));
                    }
                }
                documentContainerHost.Child = _medicalDocContainer;
            }
        }

        /// <summary>
        /// 透析单签名
        /// </summary>
        /// <param name="signType"></param>
        private void Sign(SignTypeEnum signType)
        {
            #region 患者签名

            if (signType == SignTypeEnum.Patient)
            {
                List<string> list = new List<string>();
                var node = this.tlDocments.Nodes[1];
                foreach (TreeNode n in node.Nodes)
                {
                    if (n.Checked)
                    {
                        list.Add(n.Text);
                    }
                }

                if (list.Count == 0)
                {
                    XtraMessageBox.Show("请选择要签名的同意书！");
                    return;
                }

                BookSignFrm bookSign = new BookSignFrm();
                bookSign.SignType = signType;
                bookSign.Patient = _patientDocRow;
                bookSign.BookNames = list.ToArray();
                DialogResult result = bookSign.ShowDialog();
                if (result == DialogResult.OK)
                {
                    //移除选择的同意书
                    foreach (TreeNode n in node.Nodes)
                    {
                        if (n.Checked)
                        {
                            _medicalDocContainer.RemoveDoc(n.Text);
                        }
                    }
                    //重新加载同意书
                    foreach (TreeNode n in node.Nodes)
                    {
                        if (n.Checked)
                        {
                            tlDocments_NodeMouseClick(this.tlDocments, new TreeNodeMouseClickEventArgs(n, MouseButtons.Left, 1, 0, 0));
                        }
                    }
                    documentContainerHost.Child = _medicalDocContainer;
                }
            }

            #endregion

            #region 责任医生、责任护士、审核护士签名

            else
            {
                List<string> list = new List<string>();
                var node = this.tlDocments.Nodes[0];
                foreach (TreeNode n in node.Nodes)
                {
                    foreach (TreeNode t in n.Nodes)
                    {
                        if (t.Checked)
                        {
                            list.Add(t.Tag.ToString());
                        }
                    }
                }

                if (list.Count == 0)
                {
                    XtraMessageBox.Show("请选择要签名的透析单！");
                    return;
                }

                BookSignFrm bookSign = new BookSignFrm();
                bookSign.SignType = signType;
                bookSign.Patient = _patientDocRow;
                bookSign.CureIdList = list.ToArray();
                DialogResult result = bookSign.ShowDialog();
                if (result == DialogResult.OK)
                {
                    //移除选择的透析单
                    foreach (TreeNode n in node.Nodes)
                    {
                        foreach (TreeNode t in n.Nodes)
                        {
                            if (t.Checked)
                            {
                                _medicalDocContainer.RemoveDoc(t.Tag.ToString());
                            }
                        }
                    }
                    //重新加载透析单
                    foreach (TreeNode n in node.Nodes)
                    {
                        foreach (TreeNode t in n.Nodes)
                        {
                            if (t.Checked)
                            {
                                tlDocments_NodeMouseClick(this.tlDocments, new TreeNodeMouseClickEventArgs(t, MouseButtons.Left, 1, 0, 0));
                            }
                        }
                    }
                    documentContainerHost.Child = _medicalDocContainer;
                }
            }

            #endregion
        }

        /// <summary>
        /// 获取患者同意书扫描件数据流
        /// </summary>
        /// <param name="bookName"></param>
        /// <returns></returns>
        private byte[] GetBookImageData(string bookName)
        {
            byte[] data = null;
            if (dtServer != null && dtServer.Rows.Count > 0)
            {
                string serverIP = dtServer.First(item => item.ITEM_NAME.Equals("服务端IP")).ITEM_VALUE;
                string account = dtServer.First(item => item.ITEM_NAME.Equals("服务端帐户")).ITEM_VALUE;
                string fullPath = "\\\\" + serverIP + "\\" + "UploadFile" + "\\" + _patientDocRow.HEMODIALYSIS_ID + "\\" + bookName + "\\";
                if (Directory.Exists(fullPath))
                {
                    WebClient client = new WebClient();
                    client.Credentials = new NetworkCredential(account.Substring(0, account.IndexOf("/")), account.Substring(account.IndexOf("/") + 1));

                    try
                    {
                        //获取最新文件
                        var fileInfo = (from f in Directory.GetFiles(fullPath) select new FileInfo(f));
                        var serverFile = (from info in fileInfo where !info.Extension.Equals(".db") orderby info.LastWriteTime descending select info.FullName).ToArray();
                        if (serverFile != null && serverFile.Length > 0)
                        {
                            data = client.DownloadData(serverFile[0]);
                        }
                    }
                    catch (Exception ex)
                    {
                        XtraMessageBox.Show("加载同意书失败：" + ex.Message, "患者同意书");
                    }
                }
            }
            return data;
        }

        /// <summary>
        /// 绑定签名方式下拉项
        /// </summary>
        private void BindSignType()
        {
            DataTable dtType = new DataTable();
            dtType.Columns.Add(new DataColumn("ITEM_VALUE"));
            dtType.Columns.Add(new DataColumn("ITEM_NAME"));

            DataRow row = dtType.NewRow();
            row["ITEM_VALUE"] = "0";
            row["ITEM_NAME"] = "普通签名";
            dtType.Rows.Add(row);

            row = dtType.NewRow();
            row["ITEM_VALUE"] = "1";
            row["ITEM_NAME"] = "电子签名";
            dtType.Rows.Add(row);

            Utility.BindLookUpEdit(this.lupSignType, "ITEM_VALUE", "ITEM_NAME", dtType, "ITEM_NAME", "签名方式");
        }

        /// <summary>
        /// 设置Image、TextBox的是否可见
        /// </summary>
        /// <param name="doc"></param>
        /// <param name="imgVisible"></param>
        /// <param name="txtVisible"></param>
        private void SetImageAndTextBoxVisible(WPF_DocumentBase doc, Visibility imgVisible, Visibility txtVisible)
        {
            if (doc != null)
            {
                doc.SetImageAndTextBoxVisible(imgVisible, txtVisible);
                if (_medicalDocContainer.HaveNextPage)
                {
                    CtlMedicalDocument3 child = doc.NextPage as CtlMedicalDocument3;
                    while (child != null)
                    {
                        child.SetImageAndTextBoxVisible(imgVisible, txtVisible);
                        child = child.NextPage as CtlMedicalDocument3;
                    }
                }
            }
        }
    }
}
