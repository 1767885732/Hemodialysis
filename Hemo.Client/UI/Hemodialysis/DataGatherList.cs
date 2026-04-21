/*----------------------------------------------------------------
 * Copyright (C) 2005 (苏州)医疗科技发展有限公司
 * 文件功能描述:列表
 * 创建标识:吕志强-2013年7月31日
 * 
 * 修改时间:2013年11月8日
 * 修改人:顾伟伟
 * 修改描述:新增方法
 * 
 * 修改时间:2014年2月16日
 * 修改人:吕志强
 * 修改描述:新增方法SQL
 * 
 * 修改时间:2014年5月27日
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
using Hemo.IService.Config;
using Hemo.Service;
using System.Linq;
using Hemo.Model;
using Hemo.Utilities;
using Hemo.IService.Machine;

namespace Hemo.Client.UI.Hemodialysis
{
    public partial class DataGatherList :HemoBaseFrm
    {
        #region 成员变量

        private string machineLabel = string.Empty;

        private DateTime createDate;

        private string cureId = string.Empty;

        private string recipeId = string.Empty;

        private string classId = "1";

        private string sickArea = string.Empty;

        private DataTable dtList = null;

        private DataTable dtMachine = null;

        private HemodialysisModel.MED_HEMODIALYSIS_PARAMETERSDataTable dtParam = null;

        private HemodialysisModel.MED_HEMODIALYSIS_PARAMETERSDataTable dtDelParam = null;

        private IHemodialysis hemoService = ServiceManager.Instance.HemodialysisService;

        private IConfig configService = ServiceManager.Instance.ConfigService;

        private IMachine machineService = ServiceManager.Instance.MachineService;

        #endregion

        #region 属性

        /// <summary>
        /// 设备编号
        /// </summary>
        public string MachineLabel
        {
            get { return machineLabel; }
            set { machineLabel = value; }
        }

        /// <summary>
        /// 记录日期
        /// </summary>
        public DateTime CreateDate
        {
            get { return createDate; }
            set { createDate = value; }
        }

        /// <summary>
        /// 治疗单编号
        /// </summary>
        public string CureId
        {
            get { return cureId; }
            set { cureId = value; }
        }

        /// <summary>
        /// 处方编号
        /// </summary>
        public string RecipeId
        {
            get { return recipeId; }
            set { recipeId = value; }
        }

        /// <summary>
        /// 班次
        /// </summary>
        public string ClassId
        {
            get { return classId; }
            set { classId = value; }
        }

        /// <summary>
        /// 病区
        /// </summary>
        public string SickArea
        {
            get { return sickArea; }
            set { sickArea = value; }
        }

        #endregion

        #region 构造函数

        public DataGatherList()
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
        private void DataGatherList_Load(object sender, EventArgs e)
        {
            this.Text = "数据采集列表" + "(" + "设备：" + machineLabel + ")";
            dtMachine = machineService.GetNewMachineList();
            BindLookUpEdit();
            dtParam = hemoService.GetHemoParametersByCureID(cureId);
            dtDelParam = hemoService.GetDeleteHemoParametersByCureID(cureId);
            BindList();
        }

        /// <summary>
        /// 全选
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCheckAll_Click(object sender, EventArgs e)
        {
            if (dtList != null)
            {
                dtList.AsEnumerable().ToList().ForEach(row => row["CHOOSE"] = true);
            }
        }

        /// <summary>
        /// 取消全选
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnUncheckAll_Click(object sender, EventArgs e)
        {
            if (dtList != null)
            {
                dtList.AsEnumerable().ToList().ForEach(row => row["CHOOSE"] = false);
            }
        }

        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (dtList == null || dtList.Rows.Count == 0)
            {
                return;
            }

            int count = dtList.Select("CHOOSE=true").Length;
            if (count == 0)
            {
                XtraMessageBox.Show("请选择要保存的记录！");
                return;
            }

            HemodialysisModel.MED_HEMODIALYSIS_PARAMETERSDataTable dtHemoParam = new HemodialysisModel.MED_HEMODIALYSIS_PARAMETERSDataTable();

            dtList.AsEnumerable().ToList().ForEach(row =>
            {
                if ((bool)row["CHOOSE"] == true)
                {
                    if (dtParam != null)
                    {
                        var drParam = dtParam.FirstOrDefault(param => param.HEMODIALYSIS_PARAMETERS_ID == row["HEMO_PARAMETERS_COLLECTION_ID"].ToString());
                        if (drParam == null)
                        {
                            SaveParam(dtHemoParam, drParam, row);
                        }
                    }
                    else
                    {
                        HemodialysisModel.MED_HEMODIALYSIS_PARAMETERSRow drParam = null;
                        SaveParam(dtHemoParam, drParam, row);
                    }
                }
            });

            try
            {
                hemoService.SaveCureMainSaveHemoParameters(dtHemoParam);
                hemoService.SaveCureMainSaveHemoParameters(dtDelParam);
                //XtraMessageBox.Show("保存成功！");
                AutoClosedMsgBox.ShowForm("保存成功。", "系统提示", 1500, MessageBoxIcon.Information);
                this.DialogResult = DialogResult.OK;
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show("保存失败！\n" + ex.Message);
            }
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnQuery_Click(object sender, EventArgs e)
        {
            BindList();
        }

        /// <summary>
        /// 病区编辑值改变
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lookUpEdit_Area_EditValueChanged(object sender, EventArgs e)
        {
            var rows = dtMachine.Select("AREA_ID='" + this.lookUpEdit_Area.EditValue.ToString() + "'");
            DataTable dtSub = dtMachine.Clone();
            rows.AsEnumerable().ToList().ForEach(r => dtSub.ImportRow(r));
            if (dtSub.Rows.Count > 0)
            {
                this.lookUpEdit_Machine.Properties.DataSource = dtSub;
                this.lookUpEdit_Machine.EditValue = machineLabel;
            }
        }

        /// <summary>
        /// 设备编辑值改变
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lookUpEdit_Machine_EditValueChanged(object sender, EventArgs e)
        {
            this.Text = "数据采集列表" + "(" + "设备：" + this.lookUpEdit_Machine.EditValue.ToString() + ")";
        }

        #endregion

        #region 方法

        /// <summary>
        /// 绑定列表
        /// </summary>
        private void BindList()
        {
            DateTime beginTime = createDate.Date.AddHours(6);
            DateTime endTime = createDate.Date.AddHours(12);

            if (this.lookUpEdit_Class.EditValue.ToString() == "2")
            {
                beginTime = createDate.Date.AddHours(12);
                endTime = createDate.Date.AddHours(18);
            }
            else if (this.lookUpEdit_Class.EditValue.ToString() == "3")
            {
                beginTime = createDate.Date.AddHours(18);
                endTime = createDate.Date.AddHours(18).AddHours(6).AddHours(6);
            }

            this.gcDataList.DataSource = dtList = hemoService.GetHemoParametersCollectionByMonitorAndDoubleDate(this.lookUpEdit_Machine.EditValue.ToString(), createDate, beginTime, endTime);
            if (dtList != null)
            {
                dtList.Columns.Add(new DataColumn("CHOOSE", Type.GetType("System.Boolean")));
                dtList.AsEnumerable().ToList().ForEach(row =>
                {
                    row["CHOOSE"] = false;
                    if (dtParam != null)
                    {
                        var drParam = dtParam.FirstOrDefault(param => param.HEMODIALYSIS_PARAMETERS_ID == row["HEMO_PARAMETERS_COLLECTION_ID"].ToString());
                        if (drParam != null)
                        {
                            row["CHOOSE"] = true;
                        }
                    }
                });
            }
        }

        /// <summary>
        /// 下拉框绑定
        /// </summary>
        private void BindLookUpEdit()
        {
            //病区
            ConfigModel.MED_COMMON_ITEMLISTDataTable dtConfig = configService.GetConfigList(string.Empty, string.Empty, "区域", "1");
            if (dtConfig != null && dtConfig.Rows.Count > 0)
            {
                Utility.BindLookUpEdit(this.lookUpEdit_Area, "ITEM_ID", "ITEM_NAME", (DataTable)dtConfig, "ITEM_NAME", "区域");
                this.lookUpEdit_Area.EditValue = sickArea;
            }

            //设备
            var rows = dtMachine.Select("AREA_ID='" + sickArea + "'");
            DataTable dtSub = dtMachine.Clone();
            rows.AsEnumerable().ToList().ForEach(r => dtSub.ImportRow(r));
            if (dtSub.Rows.Count > 0)
            {
                this.lookUpEdit_Machine.Properties.DataSource = dtSub;
                this.lookUpEdit_Machine.EditValue = machineLabel;
            }

            //班次
            DataTable dtClass = new DataTable();
            dtClass.Columns.Add(new DataColumn("ITEM_ID"));
            dtClass.Columns.Add(new DataColumn("ITEM_NAME"));

            DataRow row = dtClass.NewRow();
            row["ITEM_ID"] = "1";
            row["ITEM_NAME"] = "上午";
            dtClass.Rows.Add(row);

            row = dtClass.NewRow();
            row["ITEM_ID"] = "2";
            row["ITEM_NAME"] = "下午";
            dtClass.Rows.Add(row);

            //row = dtClass.NewRow();
            //row["ITEM_ID"] = "3";
            //row["ITEM_NAME"] = "晚班";
            //dtClass.Rows.Add(row);

            Utility.BindLookUpEdit(this.lookUpEdit_Class, "ITEM_ID", "ITEM_NAME", dtClass, "ITEM_NAME", "班次");
            this.lookUpEdit_Class.EditValue = classId;
        }

        /// <summary>
        /// 保存透析参数
        /// </summary>
        /// <param name="dtHemoParam"></param>
        /// <param name="drParam"></param>
        /// <param name="row"></param>
        private void SaveParam(HemodialysisModel.MED_HEMODIALYSIS_PARAMETERSDataTable dtHemoParam, HemodialysisModel.MED_HEMODIALYSIS_PARAMETERSRow drParam, DataRow row)
        {
            if (dtDelParam != null)
            {
                drParam = dtDelParam.FirstOrDefault(param => param.HEMODIALYSIS_PARAMETERS_ID == row["HEMO_PARAMETERS_COLLECTION_ID"].ToString());
                if (drParam == null)
                {
                    UpdateParam(dtHemoParam, drParam, row, true);
                }
                else
                {
                    UpdateParam(dtHemoParam, drParam, row, false);
                }
            }
            else
            {
                UpdateParam(dtHemoParam, drParam, row, true);
            }
        }

        /// <summary>
        /// 新增或编辑透析参数
        /// </summary>
        /// <param name="dtHemoParam"></param>
        /// <param name="drParam"></param>
        /// <param name="row"></param>
        /// <param name="isAdd"></param>
        private void UpdateParam(HemodialysisModel.MED_HEMODIALYSIS_PARAMETERSDataTable dtHemoParam, HemodialysisModel.MED_HEMODIALYSIS_PARAMETERSRow drParam, DataRow row, bool isAdd)
        {
            if (isAdd)
            {
                var r = dtHemoParam.NewMED_HEMODIALYSIS_PARAMETERSRow();
                r.HEMODIALYSIS_PARAMETERS_ID = row["HEMO_PARAMETERS_COLLECTION_ID"].ToString();
                r.CURE_ID = cureId;
                r.RECIPE_ID = recipeId;
                r.CREATE_DATE = Utility.CDate(row["CREATE_DATE"].ToString());
                r.VENOUS_PRESSURE = Utility.CDecimal(row["VENOUS_PRESSURE"].ToString());
                r.TRANSMEMBRANE_PRESSURE = Utility.CDecimal(row["TRANSMEMBRANE_PRESSURE"].ToString());
                r.TEMPERATURE = Utility.CDecimal(row["TEMPERATURE"].ToString());
                r.SYSTOLIC_PRESSURE = Utility.CDecimal(row["SYSTOLIC_PRESSURE"].ToString());
                r.DIASTOLIC_PRESSURE = Utility.CDecimal(row["DIASTOLIC_PRESSURE"].ToString());
                r.CARDIOTACH = Utility.CDecimal(row["CARDIOTACH"].ToString());
                r.BREATH = Utility.CDecimal(row["BREATH"].ToString());
                r.KT_V = row["KT_V"].ToString();
                r.BLOOD_FLOW = Utility.CDecimal(row["BLOOD_FLOW"].ToString());
                r.SODIUM_ION = Utility.CDecimal(row["SODIUM_ION"].ToString());
                r.DIALYSATE_RATE = Utility.CDecimal(row["DIALYSATE_RATE"].ToString());
                r.URF = Utility.CDecimal(row["URF"].ToString());
                r.CONDUCTIVITY = Utility.CDecimal(row["CONDUCTIVITY"].ToString());
                r.DISPLACEMENT = Utility.CDecimal(row["DISPLACEMENT"].ToString());
                r.EXTENDED_FIELD_1 = row["EXTENDED_FIELD_1"].ToString();
                r.EXTENDED_FIELD_2 = row["EXTENDED_FIELD_2"].ToString();
                r.EXTENDED_FIELD_3 = row["EXTENDED_FIELD_3"].ToString();
                r.EXTENDED_FIELD_4 = row["EXTENDED_FIELD_4"].ToString();
                r.EXTENDED_FIELD_5 = row["EXTENDED_FIELD_5"].ToString();
                dtHemoParam.AddMED_HEMODIALYSIS_PARAMETERSRow(r);
            }
            else
            {
                //记录处于删除状态，更新为正常状态
                drParam.CURE_ID = cureId;
                drParam.RECIPE_ID = recipeId;
                drParam.CREATE_DATE = Utility.CDate(row["CREATE_DATE"].ToString());
                drParam.VENOUS_PRESSURE = Utility.CDecimal(row["VENOUS_PRESSURE"].ToString());
                drParam.TRANSMEMBRANE_PRESSURE = Utility.CDecimal(row["TRANSMEMBRANE_PRESSURE"].ToString());
                drParam.TEMPERATURE = Utility.CDecimal(row["TEMPERATURE"].ToString());
                drParam.SYSTOLIC_PRESSURE = Utility.CDecimal(row["SYSTOLIC_PRESSURE"].ToString());
                drParam.DIASTOLIC_PRESSURE = Utility.CDecimal(row["DIASTOLIC_PRESSURE"].ToString());
                drParam.CARDIOTACH = Utility.CDecimal(row["CARDIOTACH"].ToString());
                drParam.BREATH = Utility.CDecimal(row["BREATH"].ToString());
                drParam.KT_V = row["KT_V"].ToString();
                drParam.BLOOD_FLOW = Utility.CDecimal(row["BLOOD_FLOW"].ToString());
                drParam.SODIUM_ION = Utility.CDecimal(row["SODIUM_ION"].ToString());
                drParam.DIALYSATE_RATE = Utility.CDecimal(row["DIALYSATE_RATE"].ToString());
                drParam.URF = Utility.CDecimal(row["URF"].ToString());
                drParam.CONDUCTIVITY = Utility.CDecimal(row["CONDUCTIVITY"].ToString());
                drParam.DISPLACEMENT = Utility.CDecimal(row["DISPLACEMENT"].ToString());
                drParam.EXTENDED_FIELD_2 = row["EXTENDED_FIELD_2"].ToString();
                drParam.EXTENDED_FIELD_3 = row["EXTENDED_FIELD_3"].ToString();
                drParam.EXTENDED_FIELD_4 = row["EXTENDED_FIELD_4"].ToString();
                drParam.EXTENDED_FIELD_5 = row["EXTENDED_FIELD_5"].ToString();
                drParam.EXTENDED_FIELD_1 = "0";
            }
        }

        #endregion
    }
}