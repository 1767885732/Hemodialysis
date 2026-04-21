using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Hemo.Utilities;

namespace Hemo.Client.Base
{
    public partial class InputTimeFrm : DevExpress.XtraEditors.XtraForm
    {
        #region 变量

        private DateTime startTime;
        private DateTime endTime;
        #endregion

        #region 属性

        public DateTime StartTime
        {
            get { return startTime; }
            set { startTime = value; }
        }

        public DateTime EndTime
        {
            get { return endTime; }
            set { endTime = value; }
        }
        #endregion

        #region 构造函数

        public InputTimeFrm()
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
        private void InputTimeFrm_Load(object sender, EventArgs e)
        {
            if (this.startTime != null && this.endTime != null)
            {
                this.labelControlStar.Text = string.Format("开始治疗时间:{0}", startTime.ToString());
                this.labelControlEnd.Text = string.Format("结束治疗时间:{0}", endTime.ToString());

                var timeSpan = endTime - startTime;
                this.txtTimeHourse.EditValue = timeSpan.Days * 24 + timeSpan.Hours;
                this.txtTimeMinutes.EditValue = timeSpan.Minutes;
            }
        }

        /// <summary>
        /// 点击是
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnYes_Click(object sender, EventArgs e)
        {

            this.endTime = this.startTime.AddHours(Utility.CInt(this.txtTimeHourse.EditValue.ToString())).AddMinutes(Utility.CInt(this.txtTimeMinutes.EditValue.ToString()));


            this.DialogResult = DialogResult.Yes;
        }

        /// <summary>
        /// 点击否
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnNo_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.No;
        }

        #endregion

        private void txtTimeHourse_EditValueChanged(object sender, EventArgs e)
        {
            CompurateTimeSpan();
        }
        private void CompurateTimeSpan()
        {
            this.endTime = this.startTime.AddHours(Utility.CInt(this.txtTimeHourse.EditValue.ToString())).AddMinutes(Utility.CInt(this.txtTimeMinutes.EditValue.ToString()));
            this.labelControlEnd.Text = string.Format("结束治疗时间:{0}", endTime.ToString());
        }

        private void txtTimeMinutes_EditValueChanged(object sender, EventArgs e)
        {
            CompurateTimeSpan();

        }
    }
}