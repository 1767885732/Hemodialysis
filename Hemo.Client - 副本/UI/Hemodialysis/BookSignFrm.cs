/*----------------------------------------------------------------
 * Copyright (C) 2005 (苏州)医疗科技发展有限公司
 * 文件功能描述:Hemo.Client.UI.Hemodialysis
 * 创建标识:吕志强-2013年7月9日
 * 
 * 修改时间:2013年10月17日
 * 修改人:贺建操
 * 修改描述:修改方法
 * 
 * 修改时间:2014年1月25日
 * 修改人:顾伟伟
 * 修改描述:新增方法
 * 
 * 修改时间:2014年5月5日
 * 修改人:顾伟伟
 * 修改描述:修改方法SQL
 * ----------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Hemo.Model;
using Hemo.Client.Controls;

namespace Hemo.Client.UI.Hemodialysis
{
    public partial class BookSignFrm : DevExpress.XtraEditors.XtraForm
    {
        #region 类变量

        private PatientModel.MED_PATIENTSRow patient = null;

        private string[] bookNames = null;

        private string[] cureIdList = null;

        private SignTypeEnum signType;

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

        public BookSignFrm()
        {
            InitializeComponent();
        }

        #endregion

        #region 事件

        /// <summary>
        /// 加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BookSignFrm_Load(object sender, EventArgs e)
        {
            this.ctlPatientSign.SignType = signType;
            this.ctlPatientSign.Patient = patient;
            this.ctlPatientSign.BookNames = bookNames;
            this.ctlPatientSign.CureIdList = cureIdList;

            switch (signType)
            {
                case SignTypeEnum.Patient:
                    this.Text = "患者签名";
                    break;
                case SignTypeEnum.PrimaryDoctor:
                    this.Text = "责任医生签名";
                    break;
                case SignTypeEnum.PrimaryNurse:
                    this.Text = "责任护士签名";
                    break;
                case SignTypeEnum.CheckNurse:
                    this.Text = "审核护士签名";
                    break;
                default:
                    this.Text = "患者签名";
                    break;
            }
        }

        #endregion
    }
}