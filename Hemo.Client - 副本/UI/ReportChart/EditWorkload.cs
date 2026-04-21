/*----------------------------------------------------------------
 * Copyright (C) 2005 (苏州)医疗科技发展有限公司
 * 文件功能描述:工作量编辑类
 * 创建标识:吕志强-2016年6月8日
 * ----------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Hemo.Model;
using Hemo.Service;
using Hemo.Utilities;
using Hemo.IService;
using Hemo.IService.Config;
using Hemo.IService.Dict;
using Hemo.Client.Core;

namespace Hemo.Client.UI.ReportChart
{
    public partial class EditWorkload : HemoBaseFrm
    {
        #region 字段属性
        private HemoModel.MED_WORKLOADDataTable workloadTable;
        private IStaffDict _staffDictService = ServiceManager.Instance.StaffDictService;
        private HemoModel.MED_USER_WORKLOAD_EXTENDERDataTable workUsrs = null;
        private HemoModel.MED_WORKLOADRow workload;

        private bool isAdd;
        private IConfig _configService = ServiceManager.Instance.ConfigService;
        private IPatient objPatient = ServiceManager.Instance.PatientService;

        private IHemodialysis hemodialysisService = ServiceManager.Instance.HemodialysisService;
        public string _workclassNum { get; set; }
        public DateTime _workDate { get; set; }
        public string _workArea { get; set; }
        public string _tjr { get; set; }
        #endregion

        #region 构造函数
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="workload"></param>
        public EditWorkload(HemoModel.MED_WORKLOADDataTable workloadTable, HemoModel.MED_WORKLOADRow workload)
        {
            InitializeComponent();
            DataTable dtAREA = this._configService.GetConfigList(string.Empty, string.Empty, "区域", "1");

            Utility.BindLookUpEdit(txtWORKAREA, "ITEM_ID", "ITEM_NAME", dtAREA, "ITEM_NAME", "病室");
            DataTable dtBanChi = this._configService.GetConfigList(string.Empty, string.Empty, "班次", "1");

            Utility.BindLookUpEdit(txtWORKCLASSNUM, "ITEM_VALUE", "ITEM_NAME", dtBanChi, "ITEM_NAME", "班次");

            DataTable dtStaffSict = _staffDictService.GetStaffDictList();

            DataTable dtPunctureNurseList = Utility.GetSubTable(dtStaffSict, "ZYNAME='护士'", "name");

            if (dtPunctureNurseList != null && dtPunctureNurseList.Rows.Count > 0)
            {
                BaseControlInfo.BindLookUpEdit(txtTJR, "EMP_NO", "NAME", dtPunctureNurseList, "NAME", "记录护士");
            }

            this.workloadTable = workloadTable;
            if (workload == null)
            {
                this.isAdd = true;
                this.workload = this.workloadTable.NewMED_WORKLOADRow();
                try
                {
                    workload.TJR = HemoApplicationContext.Current.CurrentUser.USER_ID;
                }
                catch (Exception e)
                { }
                this.workload.WORKCLASSNUM = _workclassNum;
                this.workload.WORKDATE = _workDate;
                this.workload.WORKAREA = _workArea;
            }
            else
            {
                this.isAdd = false;
                this.workload = workload;
                _workArea = workload.WORKAREA.ToString();
                _tjr = workload.TJR;
                this.txtWORKAREA.Enabled = false;
                this.txtWORKCLASSNUM.Enabled = false;
                this.txtWORKDATE.Enabled = false;

                this.txtLSQYTX.Enabled = true;
                this.txtXTLS.Enabled = true;
                this.txtXLLS.Enabled = true;
                this.txtXYGL.Enabled = true;
            }
        }
        #endregion

        #region 事件
        /// <summary>
        /// 窗体加载事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EditWorkload_Load(object sender, EventArgs e)
        {
            if (this.workload == null)
            {
                return;
            }

            foreach (var ctl in this.Controls)
            {

                if (ctl is BaseEdit)
                {
                    if (!(ctl as BaseEdit).Visible)
                        continue;
                    (ctl as BaseEdit).BindingDataRow(this.workload, "txt");
                }
            }


            if (this.isAdd)
            {
                this.txtWORKAREA.EditValue = _workArea;
                this.txtTJR.EditValue = _tjr;

                this.txtWORKCLASSNUM.EditValue = _workclassNum;
                this.txtWORKDATE.EditValue = _workDate;
                string banchiID = _workclassNum;// == "早班" ? "1" : _workclassNum == "晚班" ? "2" : "3";
                var dt = GetScheduleCount(_workArea, banchiID, _workDate);
                foreach (DataRow dr in dt.Rows)
                {
                    if (dr["ITEM_VALUE"].ToString() == _workclassNum)
                    {
                        this.txtLSQYTX.Text = dr["COUNT"].ToString();
                        continue;
                    }
                    switch (dr["ITEM_VALUE"].ToString())
                    {
                        case "HD":
                            this.txtXTLS.Text = dr["COUNT"].ToString();
                            break;
                        case "HDF":
                            this.txtXLLS.Text = dr["COUNT"].ToString();
                            break;
                        case "HD+HP":
                            this.txtXYGL.Text = dr["COUNT"].ToString();
                            break;
                    }
                }
            }
            FilterWorkNurse();

        }

        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (txtTJR.EditValue == null || string.IsNullOrEmpty(txtTJR.Text))
            {
                XtraMessageBox.Show("请录入护士！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            var UpDownMaster = hemodialysisService.GetParamUserWorkExtend(this.txtWORKDATE.DateTime.Date);

            var Items = UpDownMaster.Where(i => i.AREAID == this.txtWORKAREA.EditValue.ToString() && i.BANCI_ID == this.txtWORKCLASSNUM.EditValue.ToString());
            bool isCan = false;
            foreach (var row in Items)
            {
                if (row.NURSE_ID.Trim() == this.txtTJR.EditValue.ToString().Trim())
                {
                    isCan = true;
                    break;
                }
            }
            if (!isCan)
            {
                XtraMessageBox.Show(string.Format("用户{0} 在{1}班次的{2}，没有工作量，不能录入！", txtTJR.Text, this.txtWORKCLASSNUM.Text, this.txtWORKAREA.Text), "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            var workData = hemodialysisService.GetWorkloadByParmas(this.txtWORKAREA.EditValue.ToString(), this.txtWORKCLASSNUM.Text, this.txtWORKDATE.DateTime.Date);
            if (workData != null && workData.Rows.Count > 0 && isAdd)
            {
                XtraMessageBox.Show("数据已存在，请重新录入。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (this.txtWORKAREA.EditValue == null)
            {
                XtraMessageBox.Show("请选择区域！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (this.txtWORKCLASSNUM.EditValue == null)
            {
                XtraMessageBox.Show("请选择班次！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (this.txtWORKDATE.EditValue == null)
            {
                XtraMessageBox.Show("请选择日期！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (txtTJR.EditValue == null || string.IsNullOrEmpty(txtTJR.Text))
            {
                XtraMessageBox.Show("请录入护士！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (this.isAdd)
            {
                this.workload.ID = Guid.NewGuid().ToString();
                this.workload.TJR = this.txtTJR.EditValue.ToString();
                this.workloadTable.AddMED_WORKLOADRow(this.workload);
            }

            StringBuilder sb = new StringBuilder();

            if (!this.workload.IsXJZHNull())
            {
                sb.Append(string.Format("血浆置换（双重）：{0}；", this.workload.XJZH));
            }
            if (!this.workload.IsHMCLNull())
            {
                sb.Append(string.Format("缓慢超滤：{0}；", this.workload.HMCL));
            }
            if (!this.workload.IsDHSXFNull())
            {
                sb.Append(string.Format("胆红素吸附：{0}；", this.workload.DHSXF));
            }
            if (!this.workload.IsTWXHKNNull())
            {
                sb.Append(string.Format("体外循环抗凝：{0}；", this.workload.TWXHKN));
            }
            if (!this.workload.IsACTJCNull())
            {
                sb.Append(string.Format("心电监护：{0}；", this.workload.ACTJC));
            }
            if (!this.workload.IsXQFXNull())
            {
                sb.Append(string.Format("血气分析：{0}；", this.workload.XQFX));
            }
            if (!this.workload.IsWLBSYNull())
            {
                sb.Append(string.Format("微量泵使用：{0}；", this.workload.WLBSY));
            }
            if (!this.workload.IsSYBSYNull())
            {
                sb.Append(string.Format("输液泵使用：{0}；", this.workload.SYBSY));
            }
            if (!this.workload.IsJYHXQSYNull())
            {
                sb.Append(string.Format("简易呼吸器使用：{0}；", this.workload.JYHXQSY));
            }
            if (!this.workload.IsFYXYQSYNull())
            {
                sb.Append(string.Format("负压吸引器使用：{0}；", this.workload.FYXYQSY));
            }
            #region 九龙去除
            //if (!this.workload.IsDJTXNull())
            //{
            //    sb.Append(string.Format("低钾透析：{0}；", this.workload.DJTX));
            //}
            //if (!this.workload.IsGYSXYTXNull()) {
            //    sb.Append(string.Format("枸缘酸血液透析：{0}；", this.workload.GYSXYTX));
            //}
            //if (!this.workload.IsXTZFTLCNull()) {
            //    sb.Append(string.Format("血透转腹透例数：{0}；", this.workload.XTZFTLC));
            //}

            //if (!this.workload.IsFMTXLCNull()) {
            //    sb.Append(string.Format("腹膜透析例数：{0}；", this.workload.FMTXLC));
            //}
            //if (!this.workload.IsXTZSYZLCNull()) {
            //    sb.Append(string.Format("血透转肾移植例数：{0}；", this.workload.XTZSYZLC));
            //}


            #endregion
            if (!this.workload.IsXTLSNull())
            {
                sb.Append(string.Format("血透例数：{0}；", this.workload.XTLS));
            }

            if (!this.workload.IsXLLSNull())
            {
                sb.Append(string.Format("血滤例数：{0}；", this.workload.XLLS));
            }

            this.workload.REMARK = sb.ToString();
            this.hemodialysisService.SaveWorkload(this.workloadTable);
            this.DialogResult = DialogResult.OK;
        }
        /// <summary>
        /// 过滤护士
        /// </summary>
        private void FilterWorkNurse()
        {
            if (this.txtWORKDATE.DateTime.Date == DateTime.MinValue || string.IsNullOrEmpty(txtWORKCLASSNUM.Text) || txtWORKAREA.EditValue == null || string.IsNullOrEmpty(txtWORKAREA.EditValue.ToString()))
                return;
            workUsrs = hemodialysisService.GetUserWorkExtendFromCureMain(this.txtWORKDATE.DateTime.Date);


            var workUsder = workUsrs.Where(i => i.AREAID == this.txtWORKAREA.EditValue.ToString() && i.BANCI_ID == this.txtWORKCLASSNUM.EditValue.ToString());

            var dtStaffSict = _staffDictService.GetStaffDictList();

            foreach (var item in workUsder)
            {
                if (item.IsNURSE_IDNull()  || string.IsNullOrEmpty(item.NURSE_ID))
                    continue;
                foreach (var row in dtStaffSict)
                {
                    if (item.NURSE_ID == row.EMP_NO)
                    {
                        row.ZYNAME = "过滤护士";
                        break;
                    }
                }
            }
            dtStaffSict.AcceptChanges();

            DataTable FilterNurseList = Utility.GetSubTable(dtStaffSict, "ZYNAME='过滤护士'", "name");


            if (FilterNurseList != null && FilterNurseList.Rows.Count > 0)
            {
                BaseControlInfo.BindLookUpEdit(txtTJR, "EMP_NO", "NAME", FilterNurseList, "NAME", "记录护士");
            }


        }
        /// <summary>
        /// 关闭
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// txtWORKDATE改变
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtWORKDATE_EditValueChanged(object sender, EventArgs e)
        {
            if (this.txtWORKDATE.DateTime.Date == DateTime.MinValue || string.IsNullOrEmpty(txtWORKCLASSNUM.Text) || txtWORKAREA.EditValue == null || string.IsNullOrEmpty(txtWORKAREA.EditValue.ToString()))
                return;

            //if (txtWORKAREA.EditValue.ToString() == _workArea && txtWORKCLASSNUM.Text == _workclassNum && this.txtWORKDATE.DateTime.Date == _workDate.Date)
            //    return;
            string banchiID = txtWORKCLASSNUM.EditValue.ToString().Trim();//txtWORKCLASSNUM.Text.Trim() == "早班" ? "1" : txtWORKCLASSNUM.Text.Trim() == "晚班" ? "2" : "3";

            var dt = GetScheduleCount(txtWORKAREA.EditValue.ToString(), banchiID, txtWORKDATE.DateTime.Date);
            foreach (DataRow dr in dt.Rows)
            {
                if (dr["ITEM_VALUE"].ToString() == txtWORKCLASSNUM.Text.Trim())
                {
                    this.txtLSQYTX.Text = dr["COUNT"].ToString();
                    continue;
                }
                switch (dr["ITEM_VALUE"].ToString())
                {
                    case "HD":
                        this.txtXTLS.Text = dr["COUNT"].ToString();
                        break;
                    case "HDF":
                        this.txtXLLS.Text = dr["COUNT"].ToString();
                        break;
                    case "HD+HP":
                        this.txtXYGL.Text = dr["COUNT"].ToString();
                        break;
                }
            }
            FilterWorkNurse();
        }

        /// <summary>
        /// txtWORKAREA改变
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtWORKAREA_EditValueChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtWORKCLASSNUM.Text) || txtWORKAREA.EditValue == null || string.IsNullOrEmpty(txtWORKAREA.EditValue.ToString()) || this.txtWORKDATE.DateTime.Date == DateTime.MinValue)
                return;
            //if (txtWORKAREA.EditValue.ToString() == _workArea && txtWORKCLASSNUM.Text == _workclassNum && this.txtWORKDATE.DateTime.Date == _workDate.Date)
            //    return;
            string banchiID = txtWORKCLASSNUM.EditValue.ToString().Trim();//txtWORKCLASSNUM.Text.Trim() == "早班" ? "1" : txtWORKCLASSNUM.Text.Trim() == "晚班" ? "2" : "3";

            var dt = GetScheduleCount(txtWORKAREA.EditValue.ToString(), banchiID, txtWORKDATE.DateTime.Date);
            foreach (DataRow dr in dt.Rows)
            {
                if (dr["ITEM_VALUE"].ToString() == txtWORKCLASSNUM.Text.Trim())
                {
                    this.txtLSQYTX.Text = dr["COUNT"].ToString();
                    continue;
                }
                switch (dr["ITEM_VALUE"].ToString())
                {
                    case "HD":
                        this.txtXTLS.Text = dr["COUNT"].ToString();
                        break;
                    case "HDF":
                        this.txtXLLS.Text = dr["COUNT"].ToString();
                        break;
                    case "HD+HP":
                        this.txtXYGL.Text = dr["COUNT"].ToString();
                        break;
                }
            }
            FilterWorkNurse();
        }

        /// <summary>
        /// txtWORKCLASSNUM改变
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtWORKCLASSNUM_EditValueChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtWORKCLASSNUM.Text) || txtWORKAREA.EditValue == null || string.IsNullOrEmpty(txtWORKAREA.EditValue.ToString()) || this.txtWORKDATE.DateTime.Date == DateTime.MinValue)
                return;
            //if (txtWORKAREA.EditValue.ToString() == _workArea && txtWORKCLASSNUM.Text == _workclassNum && this.txtWORKDATE.DateTime.Date == _workDate.Date)
            //    return;
            string banchiID = txtWORKCLASSNUM.EditValue.ToString().Trim(); // txtWORKCLASSNUM.Text.Trim() == "早班" ? "1" : txtWORKCLASSNUM.Text.Trim() == "晚班" ? "2" : "3";

            var dt = GetScheduleCount(txtWORKAREA.EditValue.ToString(), banchiID, txtWORKDATE.DateTime.Date);
            foreach (DataRow dr in dt.Rows)
            {
                if (dr["ITEM_VALUE"].ToString() == txtWORKCLASSNUM.Text.Trim())
                {
                    this.txtLSQYTX.Text = dr["COUNT"].ToString();
                    continue;
                }
                switch (dr["ITEM_VALUE"].ToString())
                {
                    case "HD":
                        this.txtXTLS.Text = dr["COUNT"].ToString();
                        break;
                    case "HDF":
                        this.txtXLLS.Text = dr["COUNT"].ToString();
                        break;
                    case "HD+HP":
                        this.txtXYGL.Text = dr["COUNT"].ToString();
                        break;
                }
            }
            FilterWorkNurse();
        }
        #endregion

        #region 方法

        /// <summary>
        /// 获取排班数目
        /// </summary>
        /// <param name="area"></param>
        /// <param name="banchi"></param>
        /// <param name="dt"></param>
        /// <returns></returns>
        private DataTable GetScheduleCount(string area, string banchi, DateTime dt)
        {
            SetItemEmpty();

            DataTable dtScheduleCount = new DataTable();
            dtScheduleCount.Columns.Add("ITEM_VALUE", typeof(string));
            dtScheduleCount.Columns.Add("COUNT", typeof(string));

            workUsrs = hemodialysisService.GetUserWorkExtendFromCureMain(this.txtWORKDATE.DateTime.Date);
            var workUsder = workUsrs.Where(i => i.AREAID == area && i.BANCI_ID == banchi);
            if (this.txtTJR.EditValue == null || string.IsNullOrEmpty(this.txtTJR.EditValue.ToString()))
            {
                //var ls = persons1.GroupBy(a => a.Name).Select(g => (new { name = g.Key, count = g.Count(), ageC = g.Sum(item => item.Age), moneyC = g.Sum(item => item.Money) }));  

                var groupUsers = workUsder.GroupBy(i => i.REMARK).Select(i => (new { ITEM_VALUE = i.Key, COUNT = i.Count() }));
                foreach (var item in groupUsers)
                {
                    DataRow dr = dtScheduleCount.NewRow();
                    dr["ITEM_VALUE"] = item.ITEM_VALUE.ToString();
                    dr["COUNT"] = item.COUNT.ToString();
                    dtScheduleCount.Rows.Add(dr);
                }
            }
            else
            {
                var groupUsers = workUsder.GroupBy(i => new { i.NURSE_ID, i.REMARK }).Select(i => (new { NURSE_ID = i.Key.NURSE_ID, ITEM_VALUE = i.Key.REMARK, COUNT = i.Count() }));
                foreach (var item in groupUsers)
                {
                    if (item.NURSE_ID == this.txtTJR.EditValue.ToString())
                    {
                        DataRow dr = dtScheduleCount.NewRow();
                        dr["ITEM_VALUE"] = item.ITEM_VALUE.ToString();
                        dr["COUNT"] = item.COUNT.ToString();
                        dtScheduleCount.Rows.Add(dr);
                    }
                }
            }

            return dtScheduleCount;


            //return objPatient.GetScheduleCoutByParmars(area, banchi, dt);
        }

        #endregion

        private void txtTJR_EditValueChanged(object sender, EventArgs e)
        {
            SetItemEmpty();
            if (string.IsNullOrEmpty(txtWORKCLASSNUM.Text) || txtWORKAREA.EditValue == null || string.IsNullOrEmpty(txtWORKAREA.EditValue.ToString()) || this.txtWORKDATE.DateTime.Date == DateTime.MinValue)
                return;
            //if (txtWORKAREA.EditValue.ToString() == _workArea && txtWORKCLASSNUM.Text == _workclassNum && this.txtWORKDATE.DateTime.Date == _workDate.Date)
            //    return;
            string banchiID = txtWORKCLASSNUM.EditValue.ToString().Trim(); // txtWORKCLASSNUM.Text.Trim() == "早班" ? "1" : txtWORKCLASSNUM.Text.Trim() == "晚班" ? "2" : "3";

            var dt = GetScheduleCount(txtWORKAREA.EditValue.ToString(), banchiID, txtWORKDATE.DateTime.Date);

            foreach (DataRow dr in dt.Rows)
            {
                if (dr["ITEM_VALUE"].ToString() == txtWORKCLASSNUM.Text.Trim())
                {
                    this.txtLSQYTX.Text = dr["COUNT"].ToString();
                    continue;
                }
                switch (dr["ITEM_VALUE"].ToString())
                {
                    case "HD":
                        this.txtXTLS.Text = dr["COUNT"].ToString();
                        break;
                    case "HDF":
                        this.txtXLLS.Text = dr["COUNT"].ToString();
                        break;
                    case "HD+HP":
                        this.txtXYGL.Text = dr["COUNT"].ToString();
                        break;
                }
            }
         
        }
        private void SetItemEmpty()
        {
            this.txtXTLS.Text = string.Empty;
            this.txtXLLS.Text = string.Empty;
            this.txtXYGL.Text = string.Empty; 


        }
    }
}