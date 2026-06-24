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
using Hemo.Model;
using Hemo.Utilities;

namespace Hemo.Client.UI.Hemodialysis
{
    public partial class PatTransferHandoverUI : ViewBase
    {
        #region 成员变量        

        private string hemoId;

        private IHemodialysis hemodialysisService = ServiceManager.Instance.HemodialysisService;

        private HemodialysisModel.MED_PAT_TRANSFER_HANDOVERDataTable dtPatTransferHandover;        
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

        public PatTransferHandoverUI()
        {
            InitializeComponent();            
        }

        #endregion

        #region 方法

        public void QueryData()
        {
            if (!string.IsNullOrEmpty(this.txtName.Text))
            {
                this.gcPatTransferHandover.DataSource = hemodialysisService.GetPatTransferHandoverByNameAndDate(this.txtName.Text, this.deBeginTime.DateTime, this.deEndTime.DateTime.AddDays(1)) as DataTable;
            }
            else
            {
                this.gcPatTransferHandover.DataSource = hemodialysisService.GetPatTransferHandoverByHemoIdAndDate(hemoId, this.deBeginTime.DateTime, this.deEndTime.DateTime.AddDays(1)) as DataTable;
            }
        }

        #endregion

        #region 事件
        private void PatTransferHandoverUI_Load(object sender, EventArgs e)
        {
            this.Text = "查询患者术前护理单记录";

            this.deBeginTime.DateTime = DateTime.Now.Date.AddMonths(-DateTime.Now.Month + 1).AddDays(-DateTime.Now.Day + 1);
            this.deEndTime.DateTime = DateTime.Now.Date;

            QueryData();

            this.dtPatTransferHandover = this.gcPatTransferHandover.DataSource as HemodialysisModel.MED_PAT_TRANSFER_HANDOVERDataTable;
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
            this.bandedGridView1.AddNewRow();
            this.bandedGridView1.ShowEditorByMouse();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (this.bandedGridView1.GetFocusedDataRow() == null)
            {
                XtraMessageBox.Show("请选择一行要删除的记录！");
                return;
            }

            if (XtraMessageBox.Show("是否确认删除当前选择记录？", "删除提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            {
                return;
            }

            string id = this.bandedGridView1.GetFocusedDataRow()["ID"].ToString();
            int result = hemodialysisService.DeletePatTransferHandoverById(id);

            if (result > 0)
            {
                XtraMessageBox.Show("删除记录成功！");
                QueryData();
            }

            this.bandedGridView1.DeleteSelectedRows();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.FindForm().Close();
        }        

        private void btnSave_Click(object sender, EventArgs e)
        {
            DataRow newRow = this.bandedGridView1.GetFocusedDataRow();

            if (!string.IsNullOrEmpty(newRow["HEMODIALYSIS_ID"].ToString()))
            {
                XtraMessageBox.Show("请先新增一条记录！");
            }

            newRow["ID"] = Guid.NewGuid().ToString();
            newRow["HEMODIALYSIS_ID"] = this.hemoId;

            if (string.IsNullOrEmpty(newRow["CREATE_DATE"].ToString()))
            {
                newRow["CREATE_DATE"] = DateTime.Now;
            }

            DateTime newRowCreateDate = DateTime.Parse(newRow["CREATE_DATE"].ToString());

            //新增
            if (dtPatTransferHandover != null && dtPatTransferHandover.Rows.Count > 0)
            {
                foreach (DataRow row in dtPatTransferHandover.Rows)
                {
                    if ((DateTime.Parse(row["CREATE_DATE"].ToString()) == newRowCreateDate))
                    {
                        XtraMessageBox.Show("同一录入日期的记录不能重复！");
                        return;
                    }
                }
            }

            newRow["IS_DELETE"] = "0";

            int result = hemodialysisService.SavePatTransferHandover(this.gcPatTransferHandover.DataSource as HemodialysisModel.MED_PAT_TRANSFER_HANDOVERDataTable);
            if (result > 0)
            {                
                AutoClosedMsgBox.ShowForm("保存成功。", "系统提示", 1500, MessageBoxIcon.Information);
            }
            else
            {
                XtraMessageBox.Show("保存患者转运记录失败！");
            }
        }
        #endregion

        private void bandedGridView1_ShowingEditor(object sender, CancelEventArgs e)
        {
            DataRow row = this.bandedGridView1.GetDataRow(this.bandedGridView1.FocusedRowHandle);
            if (row != null)
            {
                if (!string.IsNullOrEmpty(row["HEMODIALYSIS_ID"].ToString()))
                {
                    e.Cancel = true;//该行不可编辑
                }                
            }
        }        
    } 
}
