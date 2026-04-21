/*----------------------------------------------------------------
// Copyright (C) 2013 苏州XX公司股份有限公司
// 文件名：ctlAuoSearchCondition.cs
// 文件功能描述：自定意查询条件控件 
// 创建标识：刘超 2013-07-22
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
using Hemo.Utilities;
using DevExpress.XtraBars.Docking2010.Customization;
using Hemo.Client.UI.Hemodialysis;
using DevExpress.XtraSplashScreen;
using Hemo.IService.Lab;
using Hemo.Service;
using DevExpress.XtraPrinting;
using System.Text.RegularExpressions;
using Hemo.IService.Config;

namespace Hemo.Client.Controls
{
    public partial class ctlAutoSearch : DevExpress.XtraEditors.XtraUserControl
    {
        #region 变量
        ILab _ilab = ServiceManager.Instance.LabService;
        public EventHandler BindItems = null;
        private bool isFirst;
        private DataTable _pdt;
        private DataTable _coldt;
        private string _sqlSelect = string.Empty;
        private bool isConstant = true;
        private IHemodialysis _hemodialysisService = ServiceManager.Instance.HemodialysisService;

        public DataTable ItemDt
        {
            get;
            set;
        }

        public DataTable LupCondition
        {
            get;
            set;
        }


        #endregion
        #region 构造函数
        public ctlAutoSearch()
        {
            InitializeComponent();
        }


        #endregion
        #region 事件

        private void ctlAutoSearch_Load(object sender, EventArgs e)
        {

            IsExpend(false);
            Load_LookUp();
            this.beginTime.EditValue = Utility.CDate(System.DateTime.Now.Year + "/1/1");
            this.endTime.EditValue = System.DateTime.Now.Date;
            isFirst = true;
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                this.lblInfo.Text = "";
                if (this.lupItmes.Text.ToString().Trim().Length == 0)
                {
                    XtraMessageBox.Show("请选择项目。");
                    return;
                }

                ShowMessage();

                isConstant = chkConstant.Checked;
                DataTable dtPatient = isConstant ? _hemodialysisService.GetHemoIdInLastWeekAndThreeMonthsByDate(Utility.CDate(this.beginTime.DateTime.ToString()), Utility.CDate(this.endTime.DateTime.ToString())) : _hemodialysisService.GetHemoIdByDate(Utility.CDate(this.beginTime.DateTime.ToString()), Utility.CDate(this.endTime.DateTime.ToString()));
                if (dtPatient != null && dtPatient.Rows.Count > 0)
                {
                    var pdt = new DataTable();

                    if (txtInfo.Text.Trim().Length > 0)
                    { pdt = _ilab.GetLabListByDateAndItemsAndHemoInfo(Utility.CDate(this.beginTime.EditValue.ToString()), Utility.CDate(this.endTime.EditValue.ToString()), this.lupItmes.Text.ToString(), this.txtInfo.Text.Trim()); }
                    else
                    { pdt = _ilab.GetLabListByDateAndItems(Utility.CDate(this.beginTime.EditValue.ToString()), Utility.CDate(this.endTime.EditValue.ToString()), this.lupItmes.Text.ToString()); }

                    if (pdt != null && pdt.Rows.Count > 0)
                    {
                        StringBuilder sbStr = new StringBuilder();

                        if (dtPatient != null && dtPatient.Rows.Count > 0)
                        {
                            string strWhere = string.Empty;

                            foreach (DataRow row in dtPatient.Rows)
                            {
                                sbStr.Append("'").Append(row["HEMODIALYSIS_ID"].ToString()).Append("',");
                            }
                            strWhere = sbStr.ToString();
                            strWhere = strWhere.Substring(0, strWhere.Length - 1);
                            pdt = Utility.GetSubTable(pdt, "透析号 in (" + strWhere + ")", "姓名,检验日期");
                        }
                    }

                    pdt.Columns.Remove("input_code");
                    _pdt = pdt;
                    if (_pdt.Rows.Count == 0)
                    {
                        LabelControl p = new LabelControl() { Text = "查询无数据" };
                        this.flowLayoutPanel1.Controls.Clear();
                        this.flowLayoutPanel1.Controls.Add(p);
                        this.flowLayoutPanel1.Height = 36;
                        IsExpend(true);
                        this.gridView1.Columns.Clear();
                        this.gridControl1.DataSource = null;
                        HideMessage();
                        isFirst = true;
                        return;
                    }
                    _coldt = new DataTable();
                    _coldt.Columns.Add("REPORT_ITEM_NAME", System.Type.GetType("System.String"));
                    foreach (DataColumn col in _pdt.Columns)
                    {
                        if (col.ColumnName.Equals("姓名") || col.ColumnName.Equals("透析号") || col.ColumnName.Equals("病人号") ||
                            col.ColumnName.Equals("病人来源") || col.ColumnName.Equals("年龄")
                            || col.ColumnName.Equals("检验日期"))
                        {
                            continue;
                        }
                        else
                        {
                            var nr = _coldt.NewRow();
                            nr["REPORT_ITEM_NAME"] = col.ColumnName;
                            _coldt.Rows.Add(nr);
                        }
                    }
                    if (isFirst)
                    {
                        Load_SearchConditon();
                    }
                    //验证条件
                    var isContiune = true;
                    _sqlSelect = string.Empty;
                    if (this.flowLayoutPanel1.Controls.Count > 0)
                    {
                        for (int i = 0; i < this.flowLayoutPanel1.Controls.Count; i++)
                        {
                            var x = this.flowLayoutPanel1.Controls[i] as ctlAuoSearchCondition;
                            if (!x.isCheck())
                            {
                                isContiune = false;
                            }
                            else
                            {
                                _sqlSelect += x.ReturnSql();
                            }
                        }
                    }
                    if (!isContiune)
                    {
                        IsExpend(true);
                        HideMessage();
                        return;
                    }

                    Load_Grid();
                    IsExpend(true);
                    HideMessage();
                    isFirst = false;
                }
                else {
                    this.gridView1.Columns.Clear();
                    this.gridControl1.DataSource = null;
                    HideMessage();
                    isFirst = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "系统提醒", MessageBoxButtons.OK, MessageBoxIcon.Error);
                HideMessage();
            }
        
        }

        private void lupItmes_EditValueChanged(object sender, EventArgs e)
        {
            IsExpend(false);
            this.flowLayoutPanel1.Controls.Clear();
            this.gridView1.Columns.Clear();
            this.gridControl1.DataSource = null;
            isFirst = true;
        }

        void FlyVisible()
        {
            if (this.flowLayoutPanel1.Visible == false)
            {
                IsExpend(true);
            }
            else
            {
                IsExpend(false);
            }
        }


        #region SplashScreenManager

        private SplashScreenManager _loadForm;
        /// <summary>
        /// 等待窗体管理对象
        /// </summary>
        protected SplashScreenManager LoadForm
        {
            get
            {
                if (_loadForm == null)
                {
                    this._loadForm = new SplashScreenManager(this.FindForm(), typeof(FrmWaitForm), true, true);
                    //this._loadForm.CloseWaitForm();.ClosingDelay = 0;
                }
                return _loadForm;
            }
        }
        /// <summary>
        /// 显示等待窗体
        /// </summary>
        public void ShowMessage()
        {
            bool flag = !this.LoadForm.IsSplashFormVisible;
            if (flag)
            {
                this.LoadForm.ShowWaitForm();
            }
        }
        /// <summary>
        /// 关闭等待窗体
        /// </summary>
        public void HideMessage()
        {
            bool isSplashFormVisible = this.LoadForm.IsSplashFormVisible;
            if (isSplashFormVisible)
            {
                this.LoadForm.CloseWaitForm();
            }
        }

        #endregion

        private void btnExpExcel_Click_1(object sender, EventArgs e)
        {
            var itemName = string.Empty;
            if (this.lupItmes.EditValue != null)
            {
                itemName = this.lupItmes.EditValue.ToString();
            }

            if (itemName.Trim().Length == 0)
            {
                return;
            }
            string fileName = "质控指标查询" + itemName + DateTime.Now.ToString("yyyyMMdd") + "." + "xls";
            SaveFileDialog dialog = new SaveFileDialog() { Title = "导出Excel", FileName = fileName, Filter = "Excel文件(*.xls)|*.*", RestoreDirectory = true };

            if (dialog.ShowDialog(this) == DialogResult.OK)
            {
                XlsExportOptions option = new XlsExportOptions() { TextExportMode = TextExportMode.Text };
                this.gridControl1.ExportToXls(dialog.FileName);
                AutoClosedMsgBox.ShowForm("保存成功。", "提示", 1500, MessageBoxIcon.Information);
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            if (this.flowLayoutPanel1.Controls.Count > 0)
            {
                for (int i = 0; i < this.flowLayoutPanel1.Controls.Count; i++)
                {
                    (this.flowLayoutPanel1.Controls[i] as ctlAuoSearchCondition).CtlClear();
                    (this.flowLayoutPanel1.Controls[i] as ctlAuoSearchCondition).isCheck();
                }
            }

        }

        private void btnControlP_Click(object sender, EventArgs e)
        {
            FlyVisible();
        }

        private void btnReport_Click(object sender, EventArgs e)
        {
            FlyoutDialog.Show(this.FindForm(), new QualityControlRptInstruct("质控查询"));
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            ShowMessage();
            IConfig _configService = ServiceManager.Instance.ConfigService;
            var dtLupCondition = _configService.GetConfigList(string.Empty, string.Empty, "检验项目明细配置", "1");
            var dtItem = _configService.GetConfigList(string.Empty, string.Empty, "质控指标检验项目", "1");
            string splitstr = string.Empty;
            var dtTitle = new DataTable();
            var item_name = string.Empty;
            DataTable dtSource = new DataTable();
            var distinctrow = dtLupCondition.DefaultView.ToTable(true, "ITEM_NAME");
            for (int i = 0; i < distinctrow.Rows.Count; i++)
            {
                item_name = distinctrow.Rows[i]["ITEM_NAME"].ToString();//监控指标检验项目name
                var rowsd = dtItem.AsEnumerable().FirstOrDefault(k => k.ITEM_NAME.Equals(item_name));
                if (rowsd != null)
                {
                    splitstr = rowsd.ITEM_VALUE.ToString();//监控指标检验项目value
                }
                var rows = dtLupCondition.AsEnumerable().Where(k => k.ITEM_NAME.Equals(item_name));
                if (rows != null && rows.Count() > 0)
                {
                    dtTitle = rows.CopyToDataTable();//监控指标对应的检验项目明细
                }
                dtSource = _ilab.GetLabListByDateAndItemsAndDtl(Utility.CDate(this.beginTime.EditValue.ToString()), Utility.CDate(this.endTime.EditValue.ToString()), splitstr, dtTitle);//所有满足质控指标的检验项目
                _ilab.SAVE_MED_HIS_ROWTOCOL(dtSource, dtTitle, item_name, Utility.CDate(this.beginTime.EditValue.ToString()), Utility.CDate(this.endTime.EditValue.ToString()));
            }
            HideMessage();
        }

        #endregion
        #region 方法


        void IsExpend(bool bl)
        {
            if (bl)
            {
                this.flowLayoutPanel1.Visible = true;
                this.btnControlP.Text = "收起";
            }
            else
            {
                this.flowLayoutPanel1.Visible = false;
                this.btnControlP.Text = "展开";
            }
        }

        void Load_LookUp()
        {
            DataTable dt = new DataTable();
            if (ItemDt.Rows.Count > 0)
            {
                dt = ItemDt.Select("ORDER_NUMBER<=50") == null ? new DataTable() : ItemDt.Select("ORDER_NUMBER<=50").CopyToDataTable();
                //dt = ItemDt.Select("ORDER_NUMBER<=22") == null ? new DataTable() : ItemDt.Select("ORDER_NUMBER<=22").CopyToDataTable();
            }
            Utility.BindLookUpEdit(this.lupItmes, "ITEM_VALUE", "ITEM_NAME", (DataTable)dt, "ITEM_NAME", "项目选择");
        }
        void Load_SearchConditon()
        {
            this.flowLayoutPanel1.Controls.Clear();
            int k = Screen.PrimaryScreen.WorkingArea.Width / 3;
            int h = 0;
            string name = string.Empty;
            if (lupItmes.EditValue != null)
            {
                name = this.lupItmes.Text.Trim();
            }

            for (int i = 0; i < _coldt.Rows.Count; i++)
            {
                ctlAuoSearchCondition ctl = new ctlAuoSearchCondition();
                h = ctl.Height;
                ctl.Width = k - 15;
                var colname = _coldt.Rows[i]["REPORT_ITEM_NAME"].ToString();
                ctl.DataColumnName = colname;
                var rl = string.Empty;
                var t = _pdt.Select("[" + colname + "] IS NOT NULL");
                if (t != null && t.Count() > 0)
                {
                    rl = t.CopyToDataTable().Rows[0][colname].ToString();
                }
                if (Regex.IsMatch(rl, @"^[+-]?\d*[.]?\d*$") && !string.IsNullOrEmpty(rl))
                {
                    ctl.isChoice = false;
                }
                else if (string.IsNullOrEmpty(rl))
                {
                    ctl.isChoice = false;
                }
                else
                {
                    ctl.isChoice = true;
                }
                ctl.LupCondition = LupCondition;
                this.flowLayoutPanel1.Controls.Add(ctl);
            }

            this.flowLayoutPanel1.Height = h + 6 + _coldt.Rows.Count / 3 * (h + 6);
        }

        void Load_Grid()
        {
            this.gridView1.Columns.Clear();
            this.gridControl1.DataSource = null;
            if (_sqlSelect.Length > 0)
            {
                _sqlSelect = _sqlSelect.Trim();
                _sqlSelect = _sqlSelect.Substring(2, _sqlSelect.Length - 2);
                _pdt = Utility.GetSubTable(_pdt, _sqlSelect);
            }

            this.gridControl1.DataSource = _pdt;
            this.gridView1.BestFitColumns();

            DataView dv = _pdt.DefaultView;


            this.lblInfo.Text = this.lupItmes.Text.ToString().Trim() + "病人总人数为：" + dv.ToTable(true, "透析号").Rows.Count + "人";

        }
        #endregion
    }
}
