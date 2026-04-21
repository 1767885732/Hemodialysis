using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Hemo.Client.UI.Machine;
using Hemo.IService.Config;
using Hemo.Service;

namespace Hemo.Client.UI.Hemodialysis
{
    public partial class PreoperativeNursingUI : ViewBase
    {
        #region 成员变量        

        private string hemoId;

        private IHemodialysis hemodialysisService = ServiceManager.Instance.HemodialysisService;

        #endregion

        #region 属性        

        /// <summary>
        /// 透析编号
        /// </summary>
        public string HemoId
        {
            get { return hemoId; }
            set { hemoId = value; }
        }

        #endregion

        #region 构造函数

        public PreoperativeNursingUI()
        {
            InitializeComponent();
        }

        #endregion

        #region 方法

        public void QueryData()
        {
            if (!string.IsNullOrEmpty(this.txtName.Text))
            {
                this.gcPreoperativeNursing.DataSource = hemodialysisService.GetEstimateVenousCatheterByNameAndDate(this.txtName.Text, this.deBeginTime.DateTime, this.deEndTime.DateTime) as DataTable;
            }
            else
            {
                this.gcPreoperativeNursing.DataSource = hemodialysisService.GetEstimateVenousCatheterByHemoIdAndDate(hemoId, this.deBeginTime.DateTime, this.deEndTime.DateTime) as DataTable;
            }
        }

        #endregion

        #region 事件
        private void PreoperativeNursingUI_Load(object sender, EventArgs e)
        {
            this.Text = "查询患者术前护理单记录";

            this.deBeginTime.DateTime = DateTime.Now.Date.AddMonths(-DateTime.Now.Month + 1).AddDays(-DateTime.Now.Day + 1);
            this.deEndTime.DateTime = DateTime.Now.Date;

            QueryData();
        }

        private void btnQuery_Click(object sender, EventArgs e)
        {
            QueryData();
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {

        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (this.gvPreoperativeNursing.GetFocusedDataRow() == null)
            {
                XtraMessageBox.Show("请选择一行要删除的记录！");
                return;
            }

            if (XtraMessageBox.Show("是否确认删除当前选择记录？", "删除提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            {
                return;
            }

            string id = this.gvPreoperativeNursing.GetFocusedDataRow()["ID"].ToString();
            int result = hemodialysisService.DeletePreoperativeNursingById(id);

            if (result > 0)
            {
                XtraMessageBox.Show("删除记录成功！");
                QueryData();
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.FindForm().Close();
        }

        private void gcPreoperativeNursing_DoubleClick(object sender, EventArgs e)
        {

        }
        #endregion        
    }
}
