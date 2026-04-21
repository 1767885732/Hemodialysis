/*----------------------------------------------------------------
 * Copyright (C) 2005 (苏州)医疗科技发展有限公司
 * 文件功能描述:通用类
 * 创建标识:吕志强-2013年7月13日
 * 
 * 修改时间:2013年10月21日
 * 修改人:贺建操
 * 修改描述:新增方法
 * 
 * 修改时间:2014年1月29日
 * 修改人:刘超
 * 修改描述:修改方法
 * 
 * 修改时间:2014年5月9日
 * 修改人:顾伟伟
 * 修改描述:修改方法
 * ----------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using DevExpress.Utils.Frames;
using Hemo.IService.Config;
using Hemo.Service;
using Hemo.Model;
using DevExpress.XtraEditors;
using Hemo.Client.Core;
using Hemo.IService;

namespace Hemo.Client.UI.Hemodialysis {
    public class ProFunctionCount {
        private IConfig _configService = ServiceManager.Instance.ConfigService;
        /// <summary>
        /// 保存控件信息
        /// </summary>
        /// <param name="userControl"></param>
        public void SaveFunctionCountUI(XtraUserControl userControl) {
            var funcionCountDt = new ConfigModel.MED_FUNCION_COUNTDataTable();
            var newRow = funcionCountDt.NewMED_FUNCION_COUNTRow();
            newRow.ID = Guid.NewGuid().ToString();
            newRow.FUNCTIONID = userControl.Name.ToString();
            newRow.FUNCTIONNAME = userControl.Text.ToString();
            newRow.EXTEND = string.Empty;
            newRow.EXTEND1 = string.Empty;
            newRow.COUNT = "1";
            newRow.CREATER = HemoApplicationContext.Current.CurrentUser.EMP_NO;
            newRow.CREATEDATE = DateTime.Now;
            funcionCountDt.AddMED_FUNCION_COUNTRow(newRow);

            _configService.SaveMedFuncitonCount(funcionCountDt);
        }
        /// <summary>
        /// 保存窗体信息
        /// </summary>
        /// <param name="userfrm"></param>
        public void SaveFunctionCountFrm(XtraForm userfrm) {
            var a = userfrm.Name;
            var funcionCountDt = new ConfigModel.MED_FUNCION_COUNTDataTable();
            var newRow = funcionCountDt.NewMED_FUNCION_COUNTRow();
            newRow.ID = Guid.NewGuid().ToString();
            newRow.FUNCTIONID = userfrm.Name.ToString();
            newRow.FUNCTIONNAME = userfrm.Text.ToString();
            newRow.EXTEND = string.Empty;
            newRow.EXTEND1 = string.Empty;
            newRow.COUNT = "1";
            newRow.CREATER = HemoApplicationContext.Current.CurrentUser.EMP_NO;
            newRow.CREATEDATE = DateTime.Now;
            funcionCountDt.AddMED_FUNCION_COUNTRow(newRow);
            _configService.SaveMedFuncitonCount(funcionCountDt);
        }

        /// <summary>
        /// 操作日志记录 
        /// </summary>
        /// <param name="FunctionID">窗体名称</param>
        /// <param name="FunctionName">对应功能模块</param>
        /// <param name="OperationType">1、添加 2、删除 3、修改 4、查询</param>
        public void SaveOpterationLog(string FunctionID,string FunctionName,string OperationType) {
            var funcionCountDt = new ConfigModel.MED_FUNCION_COUNTDataTable();
            var newRow = funcionCountDt.NewMED_FUNCION_COUNTRow();
            newRow.ID = Guid.NewGuid().ToString();
            newRow.FUNCTIONID = FunctionID;
            newRow.FUNCTIONNAME = FunctionName;
            newRow.EXTEND = OperationType;
            newRow.EXTEND1 = string.Empty;
            newRow.COUNT = string.Empty;
            newRow.CREATER = HemoApplicationContext.Current.CurrentUser.EMP_NO;
            newRow.CREATEDATE = DateTime.Now;
            funcionCountDt.AddMED_FUNCION_COUNTRow(newRow);
            _configService.SaveMedFuncitonCount(funcionCountDt);
        }
    }
    /// <summary>
    /// 针对于耗材出入库的数据仓库业务操作
    /// </summary>
    public class HemoDWHApplication {
        private IPatient patientService = ServiceManager.Instance.PatientService;
        /// <summary>
        /// 根据治疗方式获取对应的默认模板，然后根据模板内容进行出库申请.
        /// </summary>
        /// <param name="hemoId">透析号</param>
        /// <param name="recipeId">处方号</param>
        /// <param name="purificationMode">净化方式</param>
        public void ConfirmHemoDWApply(string hemoId, string recipeId, string purificationMode) {
            //根据治疗方式获取默认模板
            var deFaultData = patientService.GetHemoDefaultModels(purificationMode);
            if (deFaultData == null || deFaultData.Rows.Count <= 0)
                return;

            //根据默认模板获取模板数据
            var materialName = deFaultData[0].MATERIAL_MODEL_NAME.ToString();
            var ModelDt = patientService.QueryMaterialModelByParams(materialName);

            var patientMaterialDt = new MaterialScheduleModel.MED_PATIENT_MATERIALDataTable();
            for (int i = 0; i < ModelDt.Rows.Count; i++) {

                var row = patientMaterialDt.NewMED_PATIENT_MATERIALRow();
                row.ID = Guid.NewGuid().ToString();
                row.RECIPEID = recipeId;
                row.RECORDID = "tp" + recipeId;
                row.HEMODIALYSIS_ID = hemoId;
                row.CREATEDATE = DateTime.Now;
                row.LASTUPDATEBY = HemoApplicationContext.Current.CurrentUser.EMP_NO;
                row.LASTUPDATEDATE = DateTime.Now;
                row.STATE = "0";
                row.MATERIAL_ID = ModelDt.Rows[i]["MATERIAL_ID"].ToString();
                row.MATERIAL_NAME = ModelDt.Rows[i]["MATERIAL_NAME"].ToString();
                row.MATERIAL_NUMBER = Convert.ToDecimal(ModelDt.Rows[i]["MATERIAL_NUMBER"].ToString());
                row.ITEMTYPE = ModelDt.Rows[i]["ITEMTYPE"].ToString(); ;
                row.MATERTYPE = ModelDt.Rows[i]["MATERTYPE"].ToString(); ;
                row.ISDELETE = "0";
                row.ROWINDEX = i + 1;
                row.PRICE = ModelDt.Rows[i]["PRICE"].ToString();
                row.EXTAND = string.Empty;
                row.EXTAND1 = string.Empty;
                patientMaterialDt.AddMED_PATIENT_MATERIALRow(row);

            }
            //保存数据
            int result1 = patientService.SaveMaterialRecord(patientMaterialDt);
            if (result1 > 0) {
                //XtraMessageBox.Show("保存患者耗材成功！");
            }
        }
        /// <summary>
        /// 取消数据库的话那么可以根据处方号直接进行删除操作。
        /// </summary>
        /// <param name="recipeId">处方号</param>
        public void CancleConfirHemoDwApply(string recipeId) {
            //根据处方号查询出所有的出库申请库存，然后进行删除更新删除标志位
            int result = patientService.DeleteMaterialRecordByID(recipeId);
            if (result > 0) {
                //XtraMessageBox.Show("删除患者耗材记录成功！");
            }
            else {
                //XtraMessageBox.Show("删除患者耗材记录失败！");
            }
        }
        /// <summary>
        /// 根据患者这些处方号和治疗号进行数据出库
        /// </summary>
        /// <param name="recipeId"></param>
        /// <param name="cureId"></param>
        public void ConfirmHemoDWApplyOut(string recipeId, string cureId) {
            //获取耗材数据
            var applyDt = patientService.QueryMaterialOutByParams(recipeId);
            foreach (DataRow row in applyDt.Rows) {
                row["RECORDID"] = cureId;
                row["STATE"] = "1";//出库标志位更新
                row["LASTUPDATEBY"] = HemoApplicationContext.Current.CurrentUser.EMP_NO;
                row["LASTUPDATEDATE"] = DateTime.Now;
            }
            //更新出库申请表，并且进行出库操作
            int result1 = patientService.SaveMaterialRecordOut(applyDt);
            if (result1 > 0) {
                //XtraMessageBox.Show("出库成功！");
            }
            else {
                //XtraMessageBox.Show("出库失败！");
            }
        }
        /// <summary>
        ///根据患者处方号和治疗号取消出库操作。
        /// </summary>
        /// <param name="recipeId"></param>
        /// <param name="cureId"></param>
        public void CanclConfiHemoDwApplyOut(string recipeId, string cureId) {
            //获取耗材数据

            var applyDt = patientService.QueryMaterialOutByParams(recipeId);
            foreach (DataRow row in applyDt.Rows) {
                row["STATE"] = "0";
                row["LASTUPDATEBY"] = HemoApplicationContext.Current.CurrentUser.EMP_NO;
                row["LASTUPDATEDATE"] = DateTime.Now;
            }
            //更新出库申请表，并且进行取消出库操作
            int result1 = patientService.CancelMaterialRecordOut(applyDt);
            if (result1 > 0) {
                //XtraMessageBox.Show("取消出库成功！");
            }
            else {
                //XtraMessageBox.Show("取消出库失败！");
            }
        }

        
    }

    public enum OperationPoint
    {
   
    }
}
