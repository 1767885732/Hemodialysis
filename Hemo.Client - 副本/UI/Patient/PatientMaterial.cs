/*----------------------------------------------------------------
// Copyright (C) 2013 苏州XX公司
// 描述：患者治疗记录类
// 创建时间：2016-04-05
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
using Hemo.Client.Properties;
using Hemo.IService;
using Hemo.Service;
using Hemo.Model;
using DevExpress.XtraEditors;
using Hemo.Client.Controls;

namespace Hemo.Client.UI.Patient
{
    public partial class PatientMaterial : HemoBaseFrm
    {
        #region 类变量

        private Controls.CtlUserLongInfo userLong = new CtlUserLongInfo();
        private PatientMaterialDetailUI patientMaterialUi = new PatientMaterialDetailUI();

        private string currentHemoId = string.Empty;
        private string recipeId = string.Empty;
        private string recoderId = string.Empty;
        private IPatient patientService = ServiceManager.Instance.PatientService;
        public bool IsCanEdit = false;

        #endregion

        #region 属性

        /// <summary>
        /// 透析号
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
        /// <summary>
        /// 治疗单号
        /// </summary>
        public string RecoderId
        {
            get { return recoderId; }
            set { recoderId = value; }
        }

        #endregion

        #region 构造函数

        public PatientMaterial()
        {
            InitializeComponent();
        }

        #endregion

        #region 事件

        private void PatientMaterial_Load(object sender, EventArgs e)
        {
            userLong.Dock = DockStyle.Fill;
            userLong.HEMODIALYSIS_ID = currentHemoId;
            userLong.LoadPatientInfo();
            this.panelTop.Controls.Add(userLong);

            var dt = patientService.QueryPatientMaterialByParams(currentHemoId, DateTime.Now, DateTime.Now, RecipeId);
            patientMaterialUi.CurrentHemoId = currentHemoId;
            patientMaterialUi.RecipeId = recipeId;
            patientMaterialUi.PackageCode = RecoderId;
            patientMaterialUi.CurrentRecordRow = dt.Rows.Count > 0 ? dt[0] : null;
            patientMaterialUi.InzationDataDetailUi();
            patientMaterialUi.Dock = DockStyle.Fill;
            this.panelBotom.Controls.Add(patientMaterialUi);
            if (IsCanEdit)
            {
                this.patientMaterialUi.Enabled = true;
            }
            else
            {
                this.patientMaterialUi.Enabled = false;
                this.patientMaterialUi.MaterialInfoVisabled = true;
            }
        }

        #endregion
    }
}
