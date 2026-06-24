using System;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Hemo.IService.PatientSchedule;
using Hemo.Model;
using Hemo.Service;

namespace Hemo.Client.UI.Hemodialysis
{
    public partial class TemplateName : XtraForm
    {
        #region 变量
        private string _templateName = string.Empty;

        public string CurrentTemplateName
        {
            get { return _templateName; }
            set { _templateName = value; }
        }

        #endregion

        #region 构造函数

        public TemplateName()
        {
            this.InitializeComponent();
        }

        #endregion

        #region 方法

     

      

        #endregion

        #region 事件


        private void TemplateName_Load(object sender, EventArgs e)
        {
            
        }

        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSave_Click(object sender, EventArgs e)
        {
            CurrentTemplateName = this.txt_TEMPLATE_NAME.Text;
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
        }

        #endregion

        private void btn_Cancle_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
        }

       
    }
}
