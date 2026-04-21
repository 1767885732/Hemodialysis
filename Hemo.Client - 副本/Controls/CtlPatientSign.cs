/*----------------------------------------------------------------
// Copyright (C) 2013 苏州XX公司股份有限公司
// 文件名：CtlPatientSign.cs
// 文件功能描述： 病人知情同意书签名
// 创建标识：刘超 2013-07-22
----------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Hemo.Model;
using Hemo.IService.Config;
using Hemo.Service;
using System.Linq;
using Hemo.Utilities;

namespace Hemo.Client.Controls
{
    public partial class CtlPatientSign : DevExpress.XtraEditors.XtraUserControl
    {
        #region 类变量

        private PatientModel.MED_PATIENTSRow patient = null;

        private string[] bookNames = null;

        private string[] cureIdList = null;

        private SignTypeEnum signType;

        private IHemodialysis hemoService = ServiceManager.Instance.HemodialysisService;

        #endregion

        #region 属性

        /// <summary>
        /// 患者
        /// </summary>
        public PatientModel.MED_PATIENTSRow Patient
        {
            get { return patient; }
            set { patient = value; }
        }

        /// <summary>
        /// 签名类型
        /// </summary>
        public SignTypeEnum SignType
        {
            get { return signType; }
            set { signType = value; }
        }

        /// <summary>
        /// 同意书名字
        /// </summary>
        public string[] BookNames
        {
            get { return bookNames; }
            set { bookNames = value; }
        }

        /// <summary>
        /// 透析单ID
        /// </summary>
        public string[] CureIdList
        {
            get { return cureIdList; }
            set { cureIdList = value; }
        }

        #endregion

        #region 构造函数

        public CtlPatientSign()
        {
            InitializeComponent();
            Init();
        }

        #endregion

        #region 事件

        /// <summary>
        /// 重新签名
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSign_Click(object sender, EventArgs e)
        {
            this.axHWPenSign.HWClearPenSign();
        }

        /// <summary>
        /// 保存签名
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSave_Click(object sender, EventArgs e)
        {
            var image = this.axHWPenSign.HWGetBase64Stream(1);//0-bmp，1-jpg，2-png，3-gif
            //Base64字符串数据流转换成Byte字节数组
            var imgBytes = Convert.FromBase64String(image);

            #region 保存同意书签名

            if (signType == SignTypeEnum.Patient)
            {
                if (bookNames != null && bookNames.Length > 0)
                {
                    try
                    {
                        bookNames.ToList().ForEach(name =>
                        {
                            //检查同意书签名是否存在
                            HemoModel.MED_BOOK_PICTUREDataTable dtBookPicture = hemoService.GetBookPictureByHemoAndBookName(patient.HEMODIALYSIS_ID, name);
                            if (dtBookPicture == null || dtBookPicture.Rows.Count == 0)
                            {
                                //新增同意书签名
                                dtBookPicture = dtBookPicture ?? new HemoModel.MED_BOOK_PICTUREDataTable();
                                var row = dtBookPicture.NewMED_BOOK_PICTURERow();
                                row.ID = Guid.NewGuid().ToString();
                                row.HEMODIALYSIS_ID = patient.HEMODIALYSIS_ID;
                                row.BOOK_NAME = name;
                                row.BOOK_PICTURE = imgBytes;
                                dtBookPicture.AddMED_BOOK_PICTURERow(row);
                                hemoService.SaveBookPicture(dtBookPicture);
                            }
                            else
                            {
                                //修改同意书签名
                                dtBookPicture[0].BOOK_PICTURE = imgBytes;
                                hemoService.SaveBookPicture(dtBookPicture);
                            }
                        });
                        AutoClosedMsgBox.ShowForm("保存成功！", this.ParentForm.Text, 2000, MessageBoxIcon.Information);
                        this.ParentForm.DialogResult = DialogResult.OK;
                    }
                    catch (Exception ex)
                    {
                        AutoClosedMsgBox.ShowForm("保存失败！", this.ParentForm.Text, 2000, MessageBoxIcon.Warning);
                    }
                }
            }

            #endregion

            #region 保存透析单签名

            else
            {
                if (cureIdList != null && cureIdList.Length > 0)
                {
                    try
                    {
                        cureIdList.ToList().ForEach(id =>
                        {
                            //检查透析单签名是否存在
                            HemoModel.MED_CURE_SIGNDataTable dtCureSign = hemoService.GetCureSignByHemoIdAndCureId(patient.HEMODIALYSIS_ID, id);
                            if (dtCureSign == null || dtCureSign.Rows.Count == 0)
                            {
                                //新增透析单签名
                                dtCureSign = dtCureSign ?? new HemoModel.MED_CURE_SIGNDataTable();
                                var row = dtCureSign.NewMED_CURE_SIGNRow();
                                row.ID = Guid.NewGuid().ToString();
                                row.HEMODIALYSIS_ID = patient.HEMODIALYSIS_ID;
                                row.CURE_ID = id;
                                if (signType == SignTypeEnum.PrimaryDoctor) { row.PRIMARY_DOCTOR_SIGN = imgBytes; }
                                else if (signType == SignTypeEnum.PrimaryNurse) { row.PRIMARY_NURSE_SIGN = imgBytes; }
                                else if (signType == SignTypeEnum.CheckNurse) { row.CHECK_NURSE_SIGN = imgBytes; }
                                dtCureSign.AddMED_CURE_SIGNRow(row);
                                hemoService.SaveCureSign(dtCureSign);
                            }
                            else
                            {
                                //修改透析单签名
                                if (signType == SignTypeEnum.PrimaryDoctor) { dtCureSign[0].PRIMARY_DOCTOR_SIGN = imgBytes; }
                                else if (signType == SignTypeEnum.PrimaryNurse) { dtCureSign[0].PRIMARY_NURSE_SIGN = imgBytes; }
                                else if (signType == SignTypeEnum.CheckNurse) { dtCureSign[0].CHECK_NURSE_SIGN = imgBytes; }
                                hemoService.SaveCureSign(dtCureSign);
                            }
                        });
                        AutoClosedMsgBox.ShowForm("保存成功！", this.ParentForm.Text, 2000, MessageBoxIcon.Information);
                        this.ParentForm.DialogResult = DialogResult.OK;
                    }
                    catch (Exception ex)
                    {
                        AutoClosedMsgBox.ShowForm("保存失败！", this.ParentForm.Text, 2000, MessageBoxIcon.Warning);
                    }
                }
            }

            #endregion
        }

        #endregion

        #region 方法

        /// <summary>
        /// 初始化
        /// </summary>
        private void Init()
        {
            this.axHWPenSign.HWFinalize();
            this.axHWPenSign.HWSetBkColor(0xE0F8E0);
            this.axHWPenSign.HWSetCtlFrame(2, 0x00000000);
            this.axHWPenSign.HWSetPenMode(0);//0-毛笔，1-钢笔
            this.axHWPenSign.HWSetPenWidth(5);
            this.axHWPenSign.HWInitialize();
        }

        #endregion
    }

    #region 签名枚举

    public enum SignTypeEnum
    {
        Patient = 1,
        PrimaryDoctor = 2,
        PrimaryNurse = 3,
        CheckNurse = 4
    }

    #endregion
}
