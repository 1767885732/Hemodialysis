/*----------------------------------------------------------------
// Copyright (C) 2005 苏州XX公司有限公司
// 描述：铁蛋白维护用户控件
// 创建时间：2015-03-18
// 创建者：刘超
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
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Hemo.IService.Config;
using Hemo.Service;
using Hemo.Model;

namespace Hemo.Client.UI.Assessment
{
    public partial class AnemiaAssessment : DevExpress.XtraEditors.XtraUserControl
    {
        #region 类变量

        private IHemodialysis _hemoService = ServiceManager.Instance.HemodialysisService;
        private HemodialysisModel.MED_ANEMIA_CKDMBD_ASSESSDataTable dt = null;
        private HemodialysisModel.MED_ANEMIA_CKDMBD_ASSESSRow row = null;

        #endregion

        #region 属性

        public string HEMODIALYSIS_ID
        {
            set;
            get;
        }

        #endregion

        #region 构造函数

        public AnemiaAssessment() {
            InitializeComponent();
        }

        #endregion

        #region 事件

        private void btnAdd_Click(object sender, EventArgs e) {
            using (EditAnemia frm = new EditAnemia(dt, null)) {
                frm.HEMODIALYSIS_ID = HEMODIALYSIS_ID;
                if (frm.ShowDialog() == DialogResult.OK) {
                    Query();
                };
            }

        }

        private void gridView1_DoubleClick(object sender, EventArgs e) {
            if (this.gridView1.GetFocusedDataRow() != null) {
                using (EditAnemia frm = new EditAnemia(dt, (HemodialysisModel.MED_ANEMIA_CKDMBD_ASSESSRow)this.gridView1.GetFocusedDataRow())) {
                    frm.HEMODIALYSIS_ID = HEMODIALYSIS_ID;
                    if (frm.ShowDialog() == DialogResult.OK) {
                        Query();
                    };
                }
            }
        }

        private void simpleButton1_Click(object sender, EventArgs e) {
            Query();
        }

        private void btnEdit_Click(object sender, EventArgs e) {
            if (this.gridView1.GetFocusedDataRow() != null) {
                using (EditAnemia frm = new EditAnemia(dt, (HemodialysisModel.MED_ANEMIA_CKDMBD_ASSESSRow)this.gridView1.GetFocusedDataRow())) {
                    frm.HEMODIALYSIS_ID = HEMODIALYSIS_ID;
                    if (frm.ShowDialog() == DialogResult.OK) {
                        Query();
                    };
                }
            }
        }

        private void btnDelete_Click(object sender, EventArgs e) {
            if (this.gridView1.GetFocusedDataRow() != null) {
                using (EditAnemia frm = new EditAnemia(dt, (HemodialysisModel.MED_ANEMIA_CKDMBD_ASSESSRow)this.gridView1.GetFocusedDataRow()))
                    if (XtraMessageBox.Show("是否删除?", "提示", MessageBoxButtons.OKCancel) == DialogResult.OK) {
                        try {
                            if (_hemoService.DeleteMED_ANEMIA_CKDMBD_ASSESSbyID(this.gridView1.GetFocusedDataRow()["ID"].ToString()) > 0) {
                                Query();
                            }
                        }
                        catch (Exception ex) {
                            XtraMessageBox.Show(ex.ToString());
                        }
                    }
            }

        }

        private void AnemiaAssessment_Load(object sender, EventArgs e) {
            this.txtFromDate.EditValue = System.DateTime.Now;
            this.txtToDate.EditValue = System.DateTime.Now;
            Query();
        }

        #endregion

        #region 方法

        public void LoadData(string HEMODIALYSIS_ID) {
            Query();
        }

        void Query() {
            if (this.txtFromDate.EditValue != null && this.txtToDate.EditValue == null) {
                XtraMessageBox.Show("请选择结束日期！", "提示");
                return;
            }

            if (this.txtFromDate.EditValue == null && this.txtToDate.EditValue != null) {
                XtraMessageBox.Show("请选择开始日期！", "提示");
                return;
            }

            if (this.txtFromDate.EditValue == null && this.txtToDate.EditValue == null) {
                dt = _hemoService.GetMED_ANEMIA_CKDMBD_ASSESS("贫血", HEMODIALYSIS_ID);
            }
            else if (this.txtFromDate.EditValue != null && this.txtToDate.EditValue != null) {
                dt = _hemoService.GetMED_ANEMIA_CKDMBD_ASSESSbyDate(this.txtFromDate.DateTime, this.txtToDate.DateTime, "贫血", HEMODIALYSIS_ID);
            }
            this.gridControl1.DataSource = dt;
        }

        #endregion
    }
}
