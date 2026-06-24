/*----------------------------------------------------------------
 * Copyright (C) 2005 (苏州)医疗科技发展有限公司
 * 文件功能描述:治疗方式透析器关系维护类
 * 创建标识:贺建操-2016年5月9日
 * ----------------------------------------------------------------*/

using System;
using System.Data;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Hemo.IService.Config;
using Hemo.Model;
using Hemo.Service;
using Hemo.Utilities;

namespace Hemo.Client.UI.PatientSchedule
{
    public partial class SetTreatInfo :HemoBaseFrm
    {
        #region 变量

        private IConfig _configService = ServiceManager.Instance.ConfigService;
        private IHemodialysis _hemodialysisService = ServiceManager.Instance.HemodialysisService;

        private ConfigModel.MED_COMMON_ITEMLISTDataTable _purifierModelDataTable;
        private HemodialysisModel.MED_HEMO_RECIPEDataTable _recipeDataTable;

        #endregion

        #region 属性

        public string RECIPE_ID
        {
            private set;
            get;
        }

        public string PURIFICATION_MODE_NAME
        {
            private set;
            get;
        }

        public string PURIFIER_MODEL_ID
        {
            private set;
            get;
        }

        public string PURIFIER_MODEL_NAME
        {
            private set;
            get;
        }

        #endregion

        #region 构造函数

        public SetTreatInfo(string hemodialysisID, string recipeID, string purifierModelID)
        {
            this.InitializeComponent();

            this._purifierModelDataTable = this._configService.GetConfigList(string.Empty, string.Empty, "净化器类型", "1");
            this._recipeDataTable = this._hemodialysisService.GetRecipeByHemodialysisID(hemodialysisID);

            this.RECIPE_ID = recipeID;
            this.PURIFIER_MODEL_ID = purifierModelID;
        }

        #endregion

        #region 方法

        /// <summary>
        /// 初始化
        /// </summary>
        private void InitializeControls()
        {
            Utility.BindLookUpEdit(this.cbxPURIFICATION_MODE, "RECIPE_ID", "PURIFICATION_MODE_NAME", this._recipeDataTable, "PURIFICATION_MODE_NAME", "净化方式");

            this.cbxPURIFIER_MODEL.Properties.NullText = string.Empty;

            if (!string.IsNullOrEmpty(this.RECIPE_ID))
                this.cbxPURIFICATION_MODE.EditValue = this.RECIPE_ID;
        }

        /// <summary>
        /// 绑定实体
        /// </summary>
        private void BindPURIFIER_MODEL()
        {
            DataTable dtPURIFIER_MODEL = new DataTable();
            dtPURIFIER_MODEL.Columns.Add(new DataColumn("ID", typeof(string)));
            dtPURIFIER_MODEL.Columns.Add(new DataColumn("NAME", typeof(string)));

            DataRow[] rows = this._recipeDataTable.Select(string.Format("RECIPE_ID = '{0}'", this.cbxPURIFICATION_MODE.EditValue));

            if (rows.Length > 0)
            {
                DataRow row = null;

                if (rows[0]["FIRST_PURIFIER_MODEL"] != DBNull.Value)
                {
                    row = dtPURIFIER_MODEL.NewRow();

                    row["ID"] = rows[0]["FIRST_PURIFIER_MODEL"];
                    row["NAME"] = this._purifierModelDataTable.FindByITEM_ID(rows[0]["FIRST_PURIFIER_MODEL"].ToString()).ITEM_NAME;

                    dtPURIFIER_MODEL.Rows.Add(row);
                }

                if (rows[0]["SECOND_PURIFIER_MODEL"] != DBNull.Value)
                {
                    row = dtPURIFIER_MODEL.NewRow();

                    row["ID"] = rows[0]["SECOND_PURIFIER_MODEL"];
                    row["NAME"] = this._purifierModelDataTable.FindByITEM_ID(rows[0]["SECOND_PURIFIER_MODEL"].ToString()).ITEM_NAME;

                    dtPURIFIER_MODEL.Rows.Add(row);
                }
            }

            Utility.BindLookUpEdit(this.cbxPURIFIER_MODEL, "ID", "NAME", dtPURIFIER_MODEL, "NAME", "净化器型号");

            if (!string.IsNullOrEmpty(this.PURIFIER_MODEL_ID))
                this.cbxPURIFIER_MODEL.EditValue = this.PURIFIER_MODEL_ID;
        }

        #endregion

        #region 事件

        /// <summary>
        /// 关闭
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnClose_Click(object sender, EventArgs e) {
            this.Close();
        }

        /// <summary>
        /// 加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SetTreatInfo_Load(object sender, EventArgs e)
        {
            this.InitializeControls();
        }

        /// <summary>
        /// 治疗方式改变
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbxPURIFICATION_MODE_EditValueChanged(object sender, EventArgs e)
        {
            this.BindPURIFIER_MODEL();
        }

        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSave_Click(object sender, EventArgs e)
        {
            this.RECIPE_ID = this.cbxPURIFICATION_MODE.EditValue == null ? string.Empty : this.cbxPURIFICATION_MODE.EditValue.ToString();
            this.PURIFICATION_MODE_NAME = this.cbxPURIFICATION_MODE.Text;

            this.PURIFIER_MODEL_ID = this.cbxPURIFIER_MODEL.EditValue == null ? string.Empty : this.cbxPURIFIER_MODEL.EditValue.ToString();
            this.PURIFIER_MODEL_NAME = this.cbxPURIFIER_MODEL.Text;

            this.DialogResult = DialogResult.Yes;
        }

        #endregion
     
    }
}
