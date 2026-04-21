/*----------------------------------------------------------------
 * Copyright (C) 2005 (苏州)医疗科技发展有限公司
 * 文件功能描述:修改列表类
 * 创建标识:刘超-2013年6月4日
 * 
 * 修改时间:2013年9月12日
 * 修改人:刘超
 * 修改描述:新增方法SQL
 * 
 * 修改时间:2013年12月21日
 * 修改人:刘超
 * 修改描述:新增方法SQL
 * 
 * 修改时间:2014年3月31日
 * 修改人:顾伟伟
 * 修改描述:修改方法
 * ----------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Hemo.Client.Controls;
using Hemo.IService.Config;
using Hemo.Service;

namespace Hemo.Client.UI.Hemodialysis {
    public partial class EditCureList : HemoBaseFrm {

        #region 变量
        CtlMedicalDocumentContainer _medicalDocContainer = new CtlMedicalDocumentContainer();
        private IHemodialysis objHemodialysisService = ServiceManager.Instance.HemodialysisService;
        #endregion

        #region 构造函数
        public EditCureList(string pCureID) {
            InitializeComponent();
            loadCureList(pCureID);
        }
        #endregion

        #region 事件
        public void loadCureList(string pCureID) {
            DataSet ds = new DataSet();
            ds = LoadAllCureInfo(pCureID);
            _medicalDocContainer = new CtlMedicalDocumentContainer();
            CtlMedicalDocument document = new CtlMedicalDocument(ds, 0, 0);
            _medicalDocContainer.CurrentMedicalDocument = document;
            documentContainerHost.Child = _medicalDocContainer;

            //CtlUserCureList ctlUserCureList = new CtlUserCureList("");
            //ctlUserCureList.HEMODIALYSIS_ID = pHemoID;
            //ctlUserCureList.LoadCureDateList(pHemoID);
            //ctlUserCureList.ToadyListVisable = false;
            //panControl.Controls.Add(ctlUserCureList);
        }

        public DataSet LoadAllCureInfo(string pCureID) {
            //    pCureID = "2013-04-02-1001";
            return objHemodialysisService.GetAllCure(pCureID);
        }
        #endregion
    }
}