/*----------------------------------------------------------------
 * Copyright (C) 2005 (苏州)医疗科技发展有限公司
 * 文件功能描述:用户控件类
 * 创建标识:吕志强-2013年7月19日
 * 
 * 修改时间:2013年10月27日
 * 修改人:刘超
 * 修改描述:新增方法SQL
 * 
 * 修改时间:2014年2月4日
 * 修改人:顾伟伟
 * 修改描述:修改方法SQL
 * 
 * 修改时间:2014年5月15日
 * 修改人:贺建操
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
using Hemo.IService;
using Hemo.Service;
using Hemo.IService.Config;

namespace Hemo.Client.UI.Hemodialysis {
    public partial class ShowSummary :HemoBaseFrm
    {
        #region 变量
        private string _summary = null;
        private IConfig _configService = ServiceManager.Instance.ConfigService;

        public string Summary {
            get { return _summary; }
            set { _summary = value; }
        }

        private IPatient objPatient = ServiceManager.Instance.PatientService;
        #endregion
        /// <summary>
        /// 构造函数
        /// </summary>
        public ShowSummary() {
            InitializeComponent();
        }
        #region 事件

        private void btn_Cancle_Click(object sender, EventArgs e) {
            this.Close();
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
        }

        private void btn_OK_Click(object sender, EventArgs e) {
            DataRow dr = gridView1.GetFocusedDataRow();
            if (dr != null) {
                _summary = dr["ITEM_NAME"].ToString();
                DialogResult = System.Windows.Forms.DialogResult.Yes;
                this.Close();
            }
            else {
                XtraMessageBox.Show("请一条透析小结模版数据。");
            }
        }

        private void ShowSummary_Load(object sender, EventArgs e) {
            ////净化器类型
            this.Text = "透析小结";

            ProFunctionCount pfc = new ProFunctionCount();
            pfc.SaveFunctionCountFrm(this);
            DataTable dt = this._configService.GetConfigList(string.Empty, string.Empty, "透析小结模版", "1");
            if (dt != null && dt.Rows.Count > 0) {
                gridControl1.DataSource = dt;
            }
        }

        private void checkEdit_Other_CheckedChanged(object sender, EventArgs e) {

        }
        #endregion
    }
}