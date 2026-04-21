/*----------------------------------------------------------------
 * Copyright (C) 2005 (苏州)医疗科技发展有限公司
 * 文件功能描述:病患排班服务类
 * 创建标识:刘超-2013年7月4日
 * 
 * 修改时间:2013年10月12日
 * 修改人:吕志强
 * 修改描述:新增方法GetCureInfoByHemoID
 * 
 * 修改时间:2014年1月20日
 * 修改人:顾伟伟
 * 修改描述:修改方法GetSchedulePatientLabResultMain
 * 
 * 修改时间:2014年4月30日
 * 修改人:刘超
 * 修改描述:新增方法GetPatientScheduleListByPara
 * ----------------------------------------------------------------*/
using System;
using Hemo.Business.PatientSchedule;
using Hemo.IService.PatientSchedule;
using System.Linq;
using Hemo.Model;
using System.Data;

namespace Hemo.Service.PatientSchedule
{
    public class PatientScheduleService : MarshalByRefObject, IPatientSchedule
    {
        #region IPatientSchedule 成员

        public PatientScheduleModel.MED_PATIENT_SCHEDULEDataTable GetPatientScheduleSignle(DateTime dialysisDate, string hemodialysisID)
        {
            return PatientScheduleBll.GetPatientScheduleSignle(dialysisDate, hemodialysisID);
        }

        public PatientScheduleModel.MED_PATIENT_SCHEDULEDataTable GetPatientScheduleList(string userId, DateTime beginDialysisDate, DateTime endDialysisDate, string banciID)
        {
            return PatientScheduleBll.GetPatientScheduleList(userId, beginDialysisDate, endDialysisDate, banciID);
        }

        public PatientScheduleModel.MED_PATIENT_SCHEDULEDataTable GetPatientScheduleList4Report(DateTime beginDialysisDate, DateTime endDialysisDate)
        {
            return PatientScheduleBll.GetPatientScheduleList4Report(beginDialysisDate, endDialysisDate);
        }

        public PatientScheduleModel.MED_PATIENT_SCHEDULEDataTable GetPatientScheduleByParames(DateTime beginDialysisDate, DateTime endDialysisDate, string banciID, string areaID)
        {
            return PatientScheduleBll.GetPatientScheduleByParames(beginDialysisDate, endDialysisDate, banciID, areaID);
        }

        public PatientScheduleModel.MED_PATIENT_SCHEDULEDataTable GetPatientScheduleListByTemplateID(string templateID)
        {
            return PatientScheduleBll.GetPatientScheduleListByTemplateID(templateID);
        }

        public PatientScheduleModel.MED_PATIENT_SCHEDULE_TEMPLATEDataTable GetPatientScheduleTemplateList(string banciID)
        {
            return PatientScheduleBll.GetPatientScheduleTemplateList(banciID);
        }
        public PatientScheduleModel.MED_PATIENT_SCHEDULE_TEMP_DATADataTable GetPatientScheduleTempDataList(string templateId)
        {
            return PatientScheduleBll.GetPatientScheduleTempDataList(templateId);
        }
        public int SavePatientScheduleInfo(PatientScheduleModel.MED_PATIENT_SCHEDULEDataTable patientScheduleDataTable)
        {
            return PatientScheduleBll.SavePatientScheduleInfo(patientScheduleDataTable);
        }

        public int SavePatientScheduleTemplateInfo(PatientScheduleModel.MED_PATIENT_SCHEDULE_TEMPLATEDataTable patientScheduleTemplateDataTable)
        {
            return PatientScheduleBll.SavePatientScheduleTemplateInfo(patientScheduleTemplateDataTable);
        }

        public int SavePatientScheduleTemplateDataInfo(PatientScheduleModel.MED_PATIENT_SCHEDULE_TEMP_DATADataTable patientScheduleTemplateDataTable)
        {
            return PatientScheduleBll.SavePatientScheduleTemplateDataInfo(patientScheduleTemplateDataTable);
        }
        public int InSertExecProcLog(string id, string ExecParam)
        {
            return PatientScheduleBll.InSertExecProcLog(id, ExecParam);
        }
        public int DeletePatientSchedule(DateTime beginDialysisDate, DateTime endDialysisDate, string banciID, string userID)
        {
            return PatientScheduleBll.DeletePatientSchedule(beginDialysisDate, endDialysisDate, banciID, userID);
        }

        public PatientScheduleModel.MED_HEMO_APPLYDataTable GetHemodialysisApplyList(string hemodialysisID)
        {
            return PatientScheduleBll.GetHemodialysisApplyList(hemodialysisID);
        }

        public int SaveHemodialysisApplyInfo(PatientScheduleModel.MED_HEMO_APPLYDataTable hemodialysisApplyDataTable)
        {
            return PatientScheduleBll.SaveHemodialysisApplyInfo(hemodialysisApplyDataTable);
        }

        public int DeleteHemodialysisApply(string applyID)
        {
            return PatientScheduleBll.DeleteHemodialysisApply(applyID);
        }
        public int DeletePatientScheduleDateTemp(string PATIENT_SCHEDULE_TEMPLATE_ID)
        {
            return PatientScheduleBll.DeletePatientScheduleDateTemp(PATIENT_SCHEDULE_TEMPLATE_ID);
        }
        public int DeletePatientScheduleDateByID(string PATIENT_SCHEDULE_ID)
        {
            return PatientScheduleBll.DeletePatientScheduleDateByID(PATIENT_SCHEDULE_ID);

        }
        public string GetPatientScheduleRecipeIDByStartTime(string pHemoID, DateTime beginDialysisDate)
        {
            return PatientScheduleBll.GetPatientScheduleRecipeIDByStartTime(pHemoID, beginDialysisDate);
        }


        public PatientScheduleModel.MED_PATIENT_SCHEDULEDataTable QueryPatientScheduleByParam(string Patients, string BanChi, string Room)
        {
            return PatientScheduleBll.QueryPatientScheduleByParam(Patients, BanChi, Room);
        }
        public System.Data.DataTable GetSchedulePatientCount(string BanChi, string Room)
        {
            return PatientScheduleBll.GetSchedulePatientCount(BanChi, Room);
        }

        public System.Data.DataTable GetSchedulePatientLabResult(string userId, DateTime beginDate, DateTime endDate, string banchiID, string patientType)
        {
            //throw new NotImplementedException();
            return PatientScheduleBll.GetSchedulePatientLabResult(userId, beginDate, endDate, banchiID, patientType);
        }
        #endregion


        public PatientScheduleModel.TRANS_CURE_INFODataTable GetCureInfoByHemoID(string _hemoID)
        {
            return PatientScheduleBll.GetCureInfoByHemoID(_hemoID);
        }

        public PatientScheduleModel.TRANS_SCHEDULE_INFODataTable GetSCHEDULEInfoByHemoID(string _hemoID)
        {
            return PatientScheduleBll.GetSCHEDULEInfoByHemoID(_hemoID);
        }

        /// <summary>
        /// 获取服务端日期
        /// </summary>
        /// <returns></returns>
        public string GetServerDate()
        {
            return PatientScheduleBll.GetServerDate();
        }


        public System.Data.DataTable GetSchedulePatientLabResultMain(DateTime beginDate, DateTime endDate)
        {
            //throw new NotImplementedException();
            return PatientScheduleBll.GetSchedulePatientLabResultMain(beginDate, endDate);
        }

        public PatientScheduleModel.MED_PATIENT_SCHEDULEDataTable GetPatientScheduleListByPara(string userId, DateTime beginDialysisDate, DateTime endDialysisDate)
        {
            return PatientScheduleBll.GetPatientScheduleListByPara(userId, beginDialysisDate, endDialysisDate);
        }

        public PatientScheduleModel.MED_PATIENT_SCHEDULEDataTable GetPatientScheduleListByPara2(string userId, DateTime beginDialysisDate, DateTime endDialysisDate)
        {
            return PatientScheduleBll.GetPatientScheduleListByPara2(userId, beginDialysisDate, endDialysisDate);
        }

        public int SavePatientScheduleInfoNew(PatientScheduleModel.MED_PATIENT_SCHEDULEDataTable patientScheduleDataTable, DateTime beginDate, DateTime endDate, string userId, bool dateIsFromTemplate, string bloodHemoRoom, bool isHeadNurse)
        {
            var dtSchedule = PatientScheduleBll.GetPatientScheduleListByPara(userId, beginDate, endDate);
            var dtSubSchedule = dtSchedule.Where(s => 1 == 1);
            //if (!isHeadNurse)
            //{
            //    dtSubSchedule = bloodHemoRoom.Equals("5") ? dtSchedule.Where(s => s.AREANAME.Equals("透析室E区") || s.AREANAME.Equals("透析室F区") || s.AREANAME.Equals("透析室G区")) : dtSchedule.Where(s => !s.AREANAME.Equals("透析室E区") && !s.AREANAME.Equals("透析室F区") && !s.AREANAME.Equals("透析室G区"));
            //}
            var patientSchedule = dtSchedule.Clone() as PatientScheduleModel.MED_PATIENT_SCHEDULEDataTable;
            dtSubSchedule.CopyToDataTable(patientSchedule, LoadOption.OverwriteChanges);
            var patientScheduleNew = new PatientScheduleModel.MED_PATIENT_SCHEDULEDataTable();
            foreach (PatientScheduleModel.MED_PATIENT_SCHEDULERow row in patientScheduleDataTable.Rows)
            {
                //var havingRow = patientSchedule.FindByPATIENT_SCHEDULE_ID(row.PATIENT_SCHEDULE_ID);
                var havingRow = patientSchedule.FirstOrDefault(i => i.DIALYSIS_DATE == row.DIALYSIS_DATE && i.DIALYSIS_ROOM_ID == row.DIALYSIS_ROOM_ID && i.BED_NUMBER == row.BED_NUMBER && i.BANCI_ID == row.BANCI_ID);

                if (havingRow == null)
                {
                    row.PATIENT_SCHEDULE_ID = Guid.NewGuid().ToString();

                    //新增
                    patientScheduleNew.ImportRow(row);
                }
                else
                { 
                    //修改
                    if (havingRow.IsSTART_TIMENull())
                    {
                        havingRow.PATIENT_ID = row.PATIENT_ID;
                        havingRow.BANCI_ID = row.BANCI_ID;
                        havingRow.MONITOR_LABEL = row.MONITOR_LABEL;
                        havingRow.DIALYSIS_DATE = row.DIALYSIS_DATE;
                        havingRow.DIALYSIS_ROOM_ID = row.DIALYSIS_ROOM_ID;
                        havingRow.HEMODIALYSIS_ID = row.HEMODIALYSIS_ID;
                        havingRow.USER_ID = row.USER_ID;
                        havingRow.REMARK = row.REMARK;
                        havingRow.PURIFICATION_MODE = row.PURIFICATION_MODE;
                        havingRow.BED_NUMBER = row.BED_NUMBER;
                        patientScheduleNew.ImportRow(havingRow);
                    }
                }
            }
            if (true)
            {
                foreach (PatientScheduleModel.MED_PATIENT_SCHEDULERow row in patientSchedule.Rows)
                {
                    if (row.IsSTART_TIMENull() && row.RowState == System.Data.DataRowState.Unchanged && row.IsRECIPE_IDNull())
                    {
                        row.Delete();
                        patientScheduleNew.ImportRow(row);
                    }
                }
            }
            //保存数据
            return PatientScheduleBll.SavePatientScheduleInfo(patientScheduleNew);  
        }

        #region 排班相关


        public int SavePatientScheduleTemplateDataInfoNew(PatientScheduleModel.MED_PATIENT_SCHEDULE_TEMP_DATADataTable patientScheduleTemplateDataTable)
        {
            return PatientScheduleBll.SavePatientScheduleTemplateDataInfo(patientScheduleTemplateDataTable);
        }


        public PatientScheduleModel.MED_PATIENT_SCHEDULE_TEMPLATEDataTable GetPatientScheduleAllTemplateList()
        {
            return PatientScheduleBll.GetPatientScheduleAllTemplateList();
        }


        public PatientScheduleModel.MED_PATIENT_SCHEDULE_TEMP_DATADataTable GetPatientScheduleTempDataListNew(string templateId)
        {
            return PatientScheduleBll.GetPatientScheduleTempDataListNew(templateId);
        }


        public int SaveScheduleRemark(PermissionModel.MED_SCHEDULEREMARKDataTable data)
        {
            return PatientScheduleBll.SaveScheduleRemark(data);
        }

        public PermissionModel.MED_SCHEDULEREMARKDataTable GetScheduleRemarkByDate(DateTime _begin, DateTime _endTime)
        {
            return PatientScheduleBll.GetScheduleRemarkByDate(_begin, _endTime);
        }


        public int DeleteScheduleTemplateByTemplateId(string templateId)
        {
            return PatientScheduleBll.DeleteScheduleTemplateByTemplateId(templateId);
        }


        public System.Data.DataTable GetPurificationModeCountByParam(DateTime dt)
        {
            return PatientScheduleBll.GetPurificationModeCountByParam(dt);
        }

        public System.Data.DataTable GetCureCountByParam(DateTime dt)
        {
            return PatientScheduleBll.GetCureCountByParam(dt);

        }


        public System.Data.DataTable GetCureBillByCureID(string cureID)
        {
            return PatientScheduleBll.GetCureBillByCureID(cureID);
        }



        public PermissionModel.MED_USERS_WEEKDUTYDataTable GetWeekDutyByDate(DateTime starDate, DateTime endData)
        {
            return PatientScheduleBll.GetWeekDutyByDate(starDate, endData);
        }
        public PermissionModel.MED_USERS_WEEKDUTYDataTable GetWeekDutyByDateDoctor(DateTime starDate, DateTime endData)
        {
            return PatientScheduleBll.GetWeekDutyByDateDoctor(starDate, endData);
        }

        public int SaveWeekDutyData(PermissionModel.MED_USERS_WEEKDUTYDataTable data)
        {
            return PatientScheduleBll.SaveWeekDutyData(data);
        }


        public int CreateCurrntDataByLastWeek(DateTime starDate, DateTime endData)
        {
            return PatientScheduleBll.CreateCurrntDataByLastWeek(starDate, endData);

        }
        public int CreateCurrntDataByLastWeekDoctor(DateTime starDate, DateTime endData)
        {
            return PatientScheduleBll.CreateCurrntDataByLastWeekDoctor(starDate, endData);

        }

        public System.Data.DataTable GetCurrentDutyUser()
        {
            return PatientScheduleBll.GetCurrentDutyUser();

        }


        public ReportRelationModel.MED_PATIENTDUTYDataTable GetWeekDutyByTime(DateTime starDate, DateTime endData)
        {
            return PatientScheduleBll.GetWeekDutyByTime(starDate, endData);
        }


        public ReportRelationModel.MED_PATIENT_SCHEDULEDataTable GetPatientScheduleListReportForJL(DateTime beginDialysisDate, DateTime endDialysisDate, string banChi)
        {
            return PatientScheduleBll.GetPatientScheduleListReportForJL(beginDialysisDate, endDialysisDate,banChi);
        }


        public ReportRelationModel.SCHEDULEPATIENTINFODataTable GetQuerySchedulePatientInfo(DateTime queryData)
        {
            return PatientScheduleBll.GetQuerySchedulePatientInfo(queryData);
        }


        public string GetCurrentDateNurseDuty(DateTime dt)
        {
            return PatientScheduleBll.GetCurrentDateNurseDuty(dt);
        }


        public System.Data.DataTable GetSchedulePatientCheck(DateTime queryData, string banChi)
        {
            return PatientScheduleBll.GetSchedulePatientCheck(queryData, banChi);
        }


        public PatientScheduleModel.MED_PATIENT_SCHEDULEDataTable GetPatientScheduleByRecipeId(string recipeId)
        {
            return PatientScheduleBll.GetPatientScheduleByRecipeId(recipeId);
        }
        #endregion

        /// <summary>
        /// 根据透析编号获取患者当周排班信息
        /// </summary>
        /// <param name="hemoId"></param>
        /// <returns></returns>
        public string GetCurrentScheduleInfoByHemoId(string hemoId)
        {
            return PatientScheduleBll.GetCurrentScheduleInfoByHemoId(hemoId);
        }

        /// <summary>
        /// 根据床号获取最新的排班记录
        /// </summary>
        /// <param name="bedNumber"></param>
        /// <returns></returns>
        public PatientScheduleModel.MED_PATIENT_SCHEDULEDataTable GetLatestScheduleByBedNumber(string bedNumber)
        {
            return PatientScheduleBll.GetLatestScheduleByBedNumber(bedNumber);
        }
    }
}

