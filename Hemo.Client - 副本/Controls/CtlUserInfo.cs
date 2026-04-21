/*----------------------------------------------------------------
// Copyright (C) 2013 苏州XX公司有限公司
// 描述：通用用户基本信息控件
// 创建时间：2013-03-14
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
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Hemo.Model;
using Hemo.Service;
using Hemo.Utilities;

namespace Hemo.Client.UI.Hemodialysis {
    public partial class CtlUserInfo : DevExpress.XtraEditors.XtraUserControl {

        #region 私有成员
        /// <summary>
        /// 实例化病人服务对象
        /// </summary>
        private PatientService objPatient = new PatientService();
        /// <summary>
        /// 病人数据表  
        /// </summary>
        private PatientModel.MED_PATIENTSDataTable _patientDataTable;
        #endregion

        #region 公共属性
        /// <summary>
        /// 父窗口容器
        /// </summary>
        public EditVascularAccess FormContainer { get; set; }
        /// <summary>
        /// 透析号
        /// </summary>
        private string _hemodialysisID = string.Empty;
        public string HEMODIALYSIS_ID {
            set {
                _hemodialysisID = value;
            }
            get {
                return _hemodialysisID;
            }
        }

        #endregion

        #region 初始化方法
        public CtlUserInfo() {
            InitializeComponent();
        }
        #endregion

        #region 公共方法
        /// <summary>
        /// 根据西透析号载入病人数据并给控件赋值
        /// </summary>
        public void LoadPatientInfo() {
            _patientDataTable = objPatient.GetPatientListByParams("", HEMODIALYSIS_ID);
            BaseControlInfo.SetControlDataByDataTable(_patientDataTable, panControl);
        }
        public void IsShowCaption(bool isShow)
        {
            this.groupControl2.ShowCaption = isShow;
        }
        /// <summary>
        /// 设置控件是否可用
        /// </summary>
        /// <param name="pValue"></param>
        public void SetControlsEnabled(bool pValue) 
        {
            BaseControlInfo.SetControlEnabled(panControl, pValue);
        }
        #endregion
    }
}
