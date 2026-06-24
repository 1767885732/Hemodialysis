/*----------------------------------------------------------------
// Copyright (C) 2013 苏州XX公司
// 描述：患者出库明细维护窗体
// 创建时间：2016-05-12
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
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Hemo.Model;
using Hemo.IService;
using Hemo.Service;
using Hemo.IService.Config;
using DevExpress.XtraEditors;
using Hemo.Utilities;
using Hemo.Client.Core;

namespace Hemo.Client.UI.Patient
{
    public partial class PatientMaterialDetail : HemoBaseFrm
    {
        #region 类变量

        private string currentHemoId = string.Empty;
        private string packageCode = string.Empty;
        private string recipeId = string.Empty;
        private MaterialScheduleModel.MED_PATIENT_MATERIALRow currentRecordRow = null;

        #endregion

        #region 属性

        public MaterialScheduleModel.MED_PATIENT_MATERIALRow CurrentRecordRow
        {
            get { return currentRecordRow; }
            set { currentRecordRow = value; }
        }
        /// <summary>
        /// 治疗单号
        /// </summary>
        public string PackageCode
        {
            get { return packageCode; }
            set { packageCode = value; }
        }
        /// <summary>
        /// 透析叼
        /// </summary>
        public string CurrentHemoId
        {
            get { return currentHemoId; }
            set { currentHemoId = value; }
        }
        /// <summary>
        /// 处方号
        /// </summary>
        public string RecipeId
        {
            get { return recipeId; }
            set { recipeId = value; }
        }

        #endregion

        #region 构造函数

        public PatientMaterialDetail()
        {
            InitializeComponent();
        }

        #endregion

        #region 事件

        private void PatientMaterialDetail_Load(object sender, EventArgs e)
        {
            this.patientMaterialDetailUI1.CurrentHemoId = currentHemoId;
            this.patientMaterialDetailUI1.PackageCode = packageCode;
            this.patientMaterialDetailUI1.RecipeId = RecipeId;
            this.patientMaterialDetailUI1.CurrentRecordRow = currentRecordRow;
            this.patientMaterialDetailUI1.InzationDataDetailUi();

        }

        #endregion
    }
}
