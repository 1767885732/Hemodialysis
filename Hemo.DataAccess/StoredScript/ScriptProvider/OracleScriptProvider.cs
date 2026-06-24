/*----------------------------------------------------------------
// Copyright (C) 2005 (苏州)医疗科技发展有限公司
// 文件名：OracleScriptProvider.cs
// 文件功能描述：OracleScriptProvider
// 创建标识：顾伟伟-2011-01-14
// 修改时间:2013-3-13
// 修改人：刘超
// 修改描述：添加血管通路模块相关SQL
//
// 修改时间：2014-4-9
// 修改人：吕志强
// 修改描述：添加常规检查相关SQL
// 修改时间：2014-4-29
// 修改人：吕志强
// 修改描述：添加透析处方特征参数相关SQL
//
// 修改时间：2014-5-4
// 修改人：吕志强
// 修改描述：更新获取血透机使用费用数据SQL
//
// 修改时间：2014-5-6
// 修改人：吕志强
// 修改描述：更新根据药品厂商编号得到数据SQL
//
// 修改时间：2014-7-16
// 修改人：吕志强
// 修改描述：更新GetPatientScheduleList SQL，添加Sex字段
//
// 修改时间：2014-7-16
// 修改人：吕志强
// 修改描述：更新GetPatientScheduleList SQL，添加关联净化器类型SQL，添加MODELNAME字段
//
// 修改时间：2014-9-9
// 修改人：吕志强
// 修改描述：添加GetHemoParametersByHemoParamId SQL
----------------------------------------------------------------*/

namespace Hemo.DataAccess
{
    public class OracleScriptProvider
    {
        #region 病人数据相关SQL
        /// <summary>
        /// 根据病人姓名得到病人列表数据SQL
        /// </summary>
        public string GetPatientListByParams
        {
            get
            {
                return @"
                    SELECT 
                      p.*,   T.DRUG_ALLERGY || T.FOOD_ALLERGY || T.DIALYZER_ALLERGY AS BASEINFO,
                       0 as RECIPECOUNT,
                     --'' ROOMID,
                    (SELECT T.DIALYSIS_ROOM_ID FROM MED_PATIENT_SCHEDULE T WHERE T.HEMODIALYSIS_ID = P.HEMODIALYSIS_ID AND T.DIALYSIS_DATE=TRUNC(SYSDATE) AND ROWNUM=1) ROOMID,
                     (SELECT D.SERIALNUMBER FROM MED_PATIENTS_CARD D WHERE D.HEMODIALYSIS_ID = P.HEMODIALYSIS_ID AND D.STATE='0') AS CARDNO
                    FROM MED_PATIENTS p   LEFT JOIN MED_BASE_RECORD T ON P.HEMODIALYSIS_ID= T.HEMODIALYSIS_ID
                    WHERE (P.is_delete != '1' OR P.IS_DELETE IS NULL)  AND
                      (p.NAME like '%'||:NAME||'%' OR upper(p.INPUT_CODE) LIKE upper('%'||:NAME||'%')) 
                    OR(p.HEMODIALYSIS_ID =:HEMODIALYSIS_ID or p.PATIENT_ID =:HEMODIALYSIS_ID
                        or P.HEMODIALYSIS_ID = (SELECT D.HEMODIALYSIS_ID  FROM MED_PATIENTS_CARD D WHERE D.SERIALNUMBER = :HEMODIALYSIS_ID))
                    ORDER BY
                     p.name";
            }
            //p.create_date,
        }

        /// <summary>
        /// 得到病人列表数据SQL
        /// </summary>
        public string GetPatientList
        {
            get
            {
                return @"
                    SELECT 
                      p.*, 
                     0 as RECIPECOUNT,
                    (SELECT T.DIALYSIS_ROOM_ID FROM MED_PATIENT_SCHEDULE T WHERE T.HEMODIALYSIS_ID = P.HEMODIALYSIS_ID AND T.DIALYSIS_DATE=TRUNC(SYSDATE) AND ROWNUM=1) ROOMID,                    
                     (SELECT D.SERIALNUMBER FROM MED_PATIENTS_CARD D WHERE D.HEMODIALYSIS_ID = P.HEMODIALYSIS_ID AND D.STATE='0') AS CARDNO
                        FROM MED_PATIENTS p
                    WHERE (P.is_delete != '1' OR P.IS_DELETE IS NULL) ORDER BY p.NAME";
            }
            //p.create_date desc,
        }

        /// <summary>
        /// 得到病人列表数据SQL
        /// </summary>
        public string GetPatientListByType
        {
            get
            {
                return @"
                    SELECT 
                      p.*,  0 as RECIPECOUNT,
                     (SELECT T.DIALYSIS_ROOM_ID FROM MED_PATIENT_SCHEDULE T WHERE T.HEMODIALYSIS_ID = P.HEMODIALYSIS_ID AND T.DIALYSIS_DATE=TRUNC(SYSDATE) AND ROWNUM=1) ROOMID, 
                      (SELECT D.SERIALNUMBER FROM MED_PATIENTS_CARD D WHERE D.HEMODIALYSIS_ID = P.HEMODIALYSIS_ID AND D.STATE='0') AS CARDNO
                    FROM MED_PATIENTS p
                    WHERE 
                      p.TIME_TYPE = :TIME_TYPE and (P.is_delete != '1' OR P.IS_DELETE IS NULL)
                 ";
            }
            //p.create_date,
        }
        /// <summary>
        /// 得到病人列表数据SQL
        /// </summary>
        public string GetPatientListByWhere
        {
            get
            {
                return @"
                    SELECT 
                      p.*
                    FROM MED_PATIENTS p
                    WHERE p.IS_NEW = :IS_NEW and (P.is_delete != '1' OR P.IS_DELETE IS NULL)
                 ";
            }
        }
        /// <summary>
        /// 根据日期获取新登记患者列表
        /// </summary>
        public string GetNewRecordPatientListByDate
        {
            get
            {
                return @"SELECT P.* FROM MED_PATIENTS P WHERE (P.IS_DELETE != '1' OR P.IS_DELETE IS NULL) AND TRUNC(P.CREATE_DATE,'MM')>=TRUNC(:STARTDATE,'MM')
                        AND TRUNC(P.CREATE_DATE,'MM')<=TRUNC(:ENDDATE,'MM')
                        AND NOT EXISTS(SELECT DISTINCT T.HEMODIALYSIS_ID FROM MED_CURE_MAIN T WHERE TRUNC(T.CURE_CREATE_DATE,'MM')<TRUNC(:STARTDATE,'MM')
                        AND T.HEMODIALYSIS_ID=P.HEMODIALYSIS_ID) ORDER BY P.NAME";
            }
        }

        /// <summary>
        /// 根据日期获取透析患者列表
        /// </summary>
        public string GetDialysisPatientListByDate
        {
            get
            {
                return @"SELECT P.* FROM MED_PATIENTS P
                        WHERE (P.IS_DELETE != '1' OR P.IS_DELETE IS NULL) AND P.HEMODIALYSIS_ID IN
                        (SELECT DISTINCT T.HEMODIALYSIS_ID FROM MED_CURE_MAIN T WHERE TRUNC(T.CURE_CREATE_DATE,'MM')>=TRUNC(:STARTDATE,'MM')
                        AND TRUNC(T.CURE_CREATE_DATE,'MM')<=TRUNC(:ENDDATE,'MM')) ORDER BY P.NAME";
            }
        }

        /// <summary>
        /// 得到新生成的血透号
        /// </summary>
        /// <returns></returns>
        public string GetNewHemoID
        {
            get
            {
                return "SELECT MED_PATIENT_HEMOID.NEXTVAL FROM DUAL";
                //return "SELECT MAX(HEMODIALYSIS_ID)+1 AS HEMODIALYSIS_ID FROM MED_PATIENTS";
            }
        }

        /// <summary>
        /// 获取临时透析编号
        /// </summary>
        public string GetTempHemoId
        {
            get
            {
                return "SELECT * FROM MED_TEMP_HEMOID";
            }
        }

        /// <summary>
        /// 筛选病区，根据病人住院号同步获取病人信息
        /// </summary>
        /// <returns>得到病人住院信息</returns>
        public string GetPatientMasterIndexByPatientID
        {
            get
            {
                //return @"SELECT DECODE(HOS.DEPT_CODE, '', PAT.INP_NO, PAT.PATIENT_ID) PATIENT_ID,       
                //               INP_NO,
                //               NAME,
                //               NAME_PHONETIC,
                //               SEX,
                //               DATE_OF_BIRTH,
                //               BIRTH_PLACE,
                //               CITIZENSHIP,
                //               NATION,
                //               ID_NO,
                //               IDENTITY,
                //               CHARGE_TYPE,
                //               UNIT_IN_CONTRACT,
                //               MAILING_ADDRESS,
                //               ZIP_CODE,
                //               PHONE_NUMBER_HOME,
                //               PHONE_NUMBER_BUSINESS,
                //               NEXT_OF_KIN,
                //               RELATIONSHIP,
                //               NEXT_OF_KIN_ADDR,
                //               NEXT_OF_KIN_ZIP_CODE,
                //               NEXT_OF_KIN_PHONE,
                //               LAST_VISIT_DATE,
                //               VIP_INDICATOR,
                //               CREATE_DATE,
                //               OPERATOR,
                //               HOS.VISIT_ID,
                //               HOS.ADMISSION_DATE_TIME,
                //               DEPT.DEPT_NAME,
                //               DEPT_WARD.DEPT_NAME     AS WARD_NAME,
                //               HOS.BED_NO,
                //               HOS.DIAGNOSIS
                //          FROM MED_PAT_MASTER_INDEX PAT
                //          LEFT JOIN MED_PATS_IN_HOSPITAL HOS
                //            ON PAT.PATIENT_ID = HOS.PATIENT_ID
                //          LEFT JOIN MED_DEPT_DICT DEPT
                //            ON HOS.DEPT_CODE = DEPT.DEPT_CODE
                //          LEFT JOIN MED_DEPT_DICT DEPT_WARD
                //            ON HOS.WARD_CODE = DEPT_WARD.DEPT_CODE
                //         WHERE PAT.PATIENT_ID = :PATIENTID
                //        OR PAT.INP_NO = :PATIENTID";
                return @"SELECT PAT.PATIENT_ID PATIENT_ID,       
                               INP_NO,
                               NAME,
                               NAME_PHONETIC,
                               SEX,
                               DATE_OF_BIRTH,
                               BIRTH_PLACE,
                               CITIZENSHIP,
                               NATION,
                               ID_NO,
                               IDENTITY,
                               CHARGE_TYPE,
                               UNIT_IN_CONTRACT,
                               MAILING_ADDRESS,
                               ZIP_CODE,
                               PHONE_NUMBER_HOME,
                               PHONE_NUMBER_BUSINESS,
                               NEXT_OF_KIN,
                               RELATIONSHIP,
                               NEXT_OF_KIN_ADDR,
                               NEXT_OF_KIN_ZIP_CODE,
                               NEXT_OF_KIN_PHONE,
                               LAST_VISIT_DATE,
                               VIP_INDICATOR,
                               CREATE_DATE,
                               OPERATOR,
                               HOS.VISIT_ID,
                               HOS.ADMISSION_DATE_TIME,
                               DEPT.DEPT_NAME,
                               DEPT_WARD.DEPT_NAME     AS WARD_NAME,
                               HOS.BED_NO,
                               HOS.DIAGNOSIS
                          FROM MED_PAT_MASTER_INDEX PAT
                          LEFT JOIN MED_PATS_IN_HOSPITAL HOS
                            ON PAT.PATIENT_ID = HOS.PATIENT_ID
                          LEFT JOIN MED_DEPT_DICT DEPT
                            ON HOS.DEPT_CODE = DEPT.DEPT_CODE
                          LEFT JOIN MED_DEPT_DICT DEPT_WARD
                            ON HOS.WARD_CODE = DEPT_WARD.DEPT_CODE
                         WHERE PAT.PATIENT_ID = :PATIENTID
                        OR PAT.INP_NO = :PATIENTID";
            }
        }

        /// <summary>
        /// 根据Inp_no号同步获取病人信息
        /// </summary>
        /// <returns>得到病人住院信息</returns>
        public string GetPatientMasterIndexByInpNo
        {
            get
            {
                //                return @"select pat.*,hos.visit_id,hos.ADMISSION_DATE_TIME,dept.dept_name,
                //                         dept_ward.dept_name as ward_name,hos.bed_no,hos.diagnosis
                //                         from MED_PAT_MASTER_INDEX pat left join MED_PATS_IN_HOSPITAL hos on
                //                         pat.patient_id = hos.patient_id 
                //                         left join med_dept_dict dept on hos.DEPT_CODE = dept.dept_code
                //                         left join med_dept_dict dept_ward on hos.ward_code =  dept_ward.dept_code 
                //                         where hos.ward_code = :Ward_Code and pat.Patient_ID=:PatientID";
                return @"select pat.*,hos.visit_id,hos.ADMISSION_DATE_TIME,dept.dept_name,
                       dept_ward.dept_name as ward_name,hos.bed_no,hos.diagnosis
                        from MED_PAT_MASTER_INDEX pat left join MED_PATS_IN_HOSPITAL hos on
                        pat.patient_id = hos.patient_id 
                        left join med_dept_dict dept on hos.DEPT_CODE = dept.dept_code
                       left join med_dept_dict dept_ward on hos.ward_code = dept_ward.dept_code 
                       where pat.Inp_no=:InpNo";
            }
        }

        /// <summary>
        /// 根据病区号码获取全部病人信息列表从HIS表中
        /// </summary>
        public string GetPatientMasterIndexList
        {
            get
            {
                return @"select pat.*,hos.visit_id,hos.ADMISSION_DATE_TIME,dept.dept_name,
                         dept_ward.dept_name as ward_name,hos.bed_no,hos.diagnosis
                         from MED_PAT_MASTER_INDEX pat left join MED_PATS_IN_HOSPITAL hos on
                         pat.patient_id = hos.patient_id 
                         left join med_dept_dict dept on hos.DEPT_CODE = dept.dept_code
                         left join med_dept_dict dept_ward on hos.ward_code = dept_ward.dept_code
                         where hos.ward_code = :Ward_Code";
            }
        }

        public string GetPatientInfoByPatientID
        {
            get
            {
                return "select * from MED_PATIENTS where (PATIENT_ID=:PATIENT_ID or ADMISSION_NUMBER=:PATIENT_ID) and (is_delete != '1' OR IS_DELETE IS NULL)";
            }
        }

        public string GetPatientInfoByInpNo
        {
            get
            {
                return "select * from MED_PATIENTS where ADMISSION_NUMBER=:ADMISSION_NUMBER";// and (is_delete != '1' OR IS_DELETE IS NULL)
            }
        }

        public string DeletePatientByPatient_id
        {

            get
            {
                return "UPDATE MED_PATIENTS SET IS_DELETE = '1' WHERE HEMODIALYSIS_ID = :HEMODIALYSIS_ID";
            }
        }

        public string DeleteLongDrugByID
        {
            get
            {
                return @"DELETE FROM MED_CURE_LONGDRUG WHERE CURE_DRUG_ID = :CURE_DRUG_ID";
            }
        }

        public string DeleteDrugListByID
        {
            get
            {
                return @"DELETE FROM MED_CURE_DRUG WHERE CURE_DRUG_ID=:CURE_DRUG_ID";
            }
        }

        public string GetUNExcuteOrdersbyData
        {
            get
            {
                return @"SELECT distinct (SELECT S.NAME
                                  FROM MED_PATIENTS S
                                 WHERE S.HEMODIALYSIS_ID = T.HEMODIALYSIS_ID) AS NAME
                               
                          FROM MED_CURE_MAIN T, MED_CURE_DRUG T1 
                         WHERE T.CURE_ID = T1.CURE_ID AND (T1.STATUS='0' or T1.STATUS IS NULL)
                           AND trunc(T.Cure_Create_Date) = trunc(:Cure_Create_Date)
                            AND T1.DRUG_TIMETYPE != '透析后'
                            AND  exists(   
                          select e.hemodialysis_id from med_patient_schedule e where e.status='1' and t.hemodialysis_id = e.hemodialysis_id )";
            }//, T1.DRUG_NAME
        }

        public string GetHemoEventInfo
        {
            get
            {
                return @"SELECT T.ID,
                                   T.HEMODIALYSIS_ID,
                                   T.QSSYKJYW,
                                   T.XQYYX,
                                   T.XGTLBWZZ,
                                   T.XLGR,
                                   T.XGCCGR,
                                   T.TLXGXGR,
                                   T.TLGR,
                                   T.HEMOEVENTDT,
                                   T.EVENTHAZARD,
                                   T.HEMOTUBE1,
                                   T.HEMOTUBEEXD1,
                                   T.HEMOTUBEEXDTIME1,
                                 DECODE(T.HEMOTUBEEXDTIME1, NULL, '0', '1') HEMOTUBEEXDTIME1NO,
                                   T.HEMOTUBE2,
                                   T.HEMOTUBEEXDTIME2,
                                   DECODE(T.HEMOTUBEEXDTIME2, NULL, '0', '1') HEMOTUBEEXDTIME2NO,
                                   T.HEMOTUBE3,
                                   T.HEMOTUBEEXDTIME3,
                                   DECODE(T.HEMOTUBEEXDTIME3, NULL, '0', '1') HEMOTUBEEXDTIME3NO,
                                   T.HEMOTUBE4,
                                   T.HEMOTUBEEXDTIM4,
                                   DECODE(T.HEMOTUBEEXDTIM4, NULL, '0', '1') HEMOTUBEEXDTIM4NO,
                                   T.HEMOTUBE5,
                                   T.HEMOTUBEEXDTIM5,
                                   DECODE(T.HEMOTUBEEXDTIM5, NULL, '0', '1') HEMOTUBEEXDTIM5NO,
                                   T.HEMOEVENTOTHERINFO,
                                   T.CONCRETEEVENT,
                                   T.CONCRETEEVENT1,
                                   T.CONCRETEEVENT2,
                                   T.CONCRETEEVENT3,
                                   T.CONCRETEEVENT4,
                                   T.BESOURCE,
                                   T.BESOURCE1,
                                   T.BESOURCE2,
                                   T.BESOURCE3,
                                   T.VAUCULARPOSTION,
                                   T.VAUCULARPOSTION1,
                                   T.VAUCULARPOSTION2,
                                   T.VAUCULARPOSTION3,    
                                   T.OTHERINFECT,
                                   T.OTHERINFECT1,
                                   T.OTHERINFECT2,
                                   T.OTHERINFECT3,
                                   T.CONCRETEPROBLEM1,
                                   T.CONCRETEPROBLEM2,
                                   T.CONCRETEPROBLEM3,
                                   T.CONCRETEPROBLEM4,
                                   T.CONCRETEPROBLEM5,
                                   T.CONCRETEPROBLEM6,
                                   T.CONCRETEPROBLEM7,
                                   T.CONCRETEPROBLEM7EXT,
                                   T.INHOSPITAL,
                                   T.INHOSPITAL1,
                                   T.INHOSPITAL2,
                                   T.CONCRETEDIE,
                                   T.CONCRETEDIE1,
                                   T.CONCRETEDIE2,
                                   T.CREATER,
                                   T.CREATERTIME,T.EVENTTYPE,
                                   S.NAME,
                                   S.SEX,
                                   S.AGE
                              FROM MED_HEMO_EVENTINFO T
                              LEFT JOIN MED_PATIENTS S
                                ON T.HEMODIALYSIS_ID = S.HEMODIALYSIS_ID
                              WHERE T.HEMODIALYSIS_ID= :HEMODIALYSIS_ID
                                AND TO_CHAR(T.CREATERTIME,'YYYY-MM')=TO_CHAR(:CREATERTIME,'YYYY-MM') AND T.EVENTTYPE=:EVENTTYPE";
            }
        }

        public string DeleteHemoEventInfoById
        {
            get
            {
                return @"DELETE FROM MED_HEMO_EVENTINFO WHERE ID = :ID";
            }
        }

        public string GetHemoEventInfoByBetweenDt
        {
            get
            {
                return @"SELECT T.ID,
                                   T.HEMODIALYSIS_ID,
                                   T.QSSYKJYW,
                                   T.XQYYX,
                                   T.XGTLBWZZ,
                                   T.XLGR,
                                   T.XGCCGR,
                                   T.TLXGXGR,
                                   T.TLGR,
                                   T.HEMOEVENTDT,
                                   T.EVENTHAZARD,
                                   T.HEMOTUBE1,
                                   T.HEMOTUBEEXD1,
                                   T.HEMOTUBEEXDTIME1,
                                 DECODE(T.HEMOTUBEEXDTIME1, NULL, '0', '1') HEMOTUBEEXDTIME1NO,
                                   T.HEMOTUBE2,
                                   T.HEMOTUBEEXDTIME2,
                                   DECODE(T.HEMOTUBEEXDTIME2, NULL, '0', '1') HEMOTUBEEXDTIME2NO,
                                   T.HEMOTUBE3,
                                   T.HEMOTUBEEXDTIME3,
                                   DECODE(T.HEMOTUBEEXDTIME3, NULL, '0', '1') HEMOTUBEEXDTIME3NO,
                                   T.HEMOTUBE4,
                                   T.HEMOTUBEEXDTIM4,
                                   DECODE(T.HEMOTUBEEXDTIM4, NULL, '0', '1') HEMOTUBEEXDTIM4NO,
                                   T.HEMOTUBE5,
                                   T.HEMOTUBEEXDTIM5,
                                   DECODE(T.HEMOTUBEEXDTIM5, NULL, '0', '1') HEMOTUBEEXDTIM5NO,
                                   T.HEMOEVENTOTHERINFO,
                                   T.CONCRETEEVENT,
                                   T.CONCRETEEVENT1,
                                   T.CONCRETEEVENT2,
                                   T.CONCRETEEVENT3,
                                   T.CONCRETEEVENT4,
                                   T.BESOURCE,
                                   T.BESOURCE1,
                                   T.BESOURCE2,
                                   T.BESOURCE3,
                                   T.VAUCULARPOSTION,
                                   T.VAUCULARPOSTION1,
                                   T.VAUCULARPOSTION2,
                                   T.VAUCULARPOSTION3,    
                                   T.OTHERINFECT,
                                   T.OTHERINFECT1,
                                   T.OTHERINFECT2,
                                   T.OTHERINFECT3,
                                   T.CONCRETEPROBLEM1,
                                   T.CONCRETEPROBLEM2,
                                   T.CONCRETEPROBLEM3,
                                   T.CONCRETEPROBLEM4,
                                   T.CONCRETEPROBLEM5,
                                   T.CONCRETEPROBLEM6,
                                   T.CONCRETEPROBLEM7,
                                   T.CONCRETEPROBLEM7EXT,
                                   T.INHOSPITAL,
                                   T.INHOSPITAL1,
                                   T.INHOSPITAL2,
                                   T.CONCRETEDIE,
                                   T.CONCRETEDIE1,
                                   T.CONCRETEDIE2,
                                   T.CREATER,
                                   T.CREATERTIME,T.EVENTTYPE,
                                   S.NAME,
                                   S.SEX,
                                   S.AGE
                              FROM MED_HEMO_EVENTINFO T
                              LEFT JOIN MED_PATIENTS S
                                ON T.HEMODIALYSIS_ID = S.HEMODIALYSIS_ID
                              WHERE  TO_CHAR(T.CREATERTIME, 'YYYY-MM-DD') >= TO_CHAR(:DTSTAR, 'YYYY-MM-DD') 
                                   AND TO_CHAR(T.CREATERTIME, 'YYYY-MM-DD') <= TO_CHAR(:DTEND, 'YYYY-MM-DD') AND T.EVENTTYPE=:EVENTTYPE";
            }
        }

        public string GetHemoOhterLogByDt
        {
            get
            {
                return @"SELECT T.*
                          FROM MED_HEMO_OHTERLOG T
                         WHERE TO_CHAR(T.CREATED, 'YYYY-MM') = TO_CHAR(:DTMONTH, 'YYYY-MM') ORDER BY T.LOGDAY";
            }
        }
        public string GetHemoSingleOhterLogByDt
        {
            get
            {
                return @"SELECT T.* FROM MED_HEMO_OHTERLOG T WHERE TRUNC(T.LOGDAY) = TRUNC(:DTDAY)";
            }
        }
        public string DeleteHemoOtherLogById
        {
            get
            {
                return @"DELETE FROM MED_HEMO_OHTERLOG T WHERE T.ID=:ID";
            }
        }
        public string GetHemoOtherLogCureDtByTime
        {
            get
            {
                return @"SELECT T1.ITEM_NAME || T2.ITEM_NAME AS NAME, COUNT(1) AS COUNT
                              FROM MED_CURE_MAIN T
                              LEFT JOIN MED_VASCULAR_ACCESS S
                                ON T.VASCULAR_ACCESS_ID = S.VASCULAR_ACCESS_ID
                              LEFT JOIN MED_COMMON_ITEMLIST T1
                                ON S.VASCULAR_ACCESS_TYPE = T1.ITEM_ID
                              LEFT JOIN MED_COMMON_ITEMLIST T2
                                ON S.ACCESS_CLASS = T2.ITEM_ID
                             WHERE T.CURE_STATUS != '4'
                               AND T.VASCULAR_ACCESS_ID IS NOT NULL
                               AND TO_CHAR(T.CURE_CREATE_DATE, 'YYYY-MM-DD') = TO_CHAR(:CURE_DATE, 'YYYY-MM-DD')
                             GROUP BY T1.ITEM_NAME, T2.ITEM_NAME";
            }
        }
        public string GetCureCountByDt
        {
            get
            {
                return @"SELECT COUNT(1) AS PATIENTCOUNT
                          FROM (SELECT T.HEMODIALYSIS_ID, COUNT(1)
                                  FROM MED_CURE_MAIN T
                                 WHERE T.CURE_STATUS != '4'
                                   AND TRUNC(T.CURE_CREATE_DATE)>= TRUNC(:DTSTAR)
                                   AND TRUNC(T.CURE_CREATE_DATE)<=TRUNC(:DTEND)
                                 GROUP BY T.HEMODIALYSIS_ID)";
            }
        }



        #endregion

        #region 血管通路
        /// <summary>
        /// 根据透析号得到病人血透通路日期列表
        /// </summary>
        public string GetVascuarAccessDateListByHemoID
        {
            get
            {//CREATE_DATE,VASCULAR_ACCESS_ID
                return "SELECT * FROM MED_VASCULAR_ACCESS WHERE HEMODIALYSIS_ID=:HEMODIALYSIS_ID ORDER BY CREATE_DATE DESC";
            }
        }
        /// <summary>
        /// 根据治疗单号获取那次透析的血管通路名称
        /// </summary>
        public string GetVascularAccessNameByCureId
        {
            get
            {
                return @"SELECT L.ITEM_NAME
                          FROM MED_CURE_MAIN T
                          LEFT JOIN MED_VASCULAR_ACCESS S
                            ON T.VASCULAR_ACCESS_ID = S.VASCULAR_ACCESS_ID
                          LEFT JOIN MED_COMMON_ITEMLIST L
                            ON S.VASCULAR_ACCESS_TYPE = L.ITEM_ID
                         WHERE T.CURE_ID = :CURE_ID";
            }
        }
        /// <summary>
        /// 根据血管通路编号得到血管通路数据
        /// </summary>
        public string GetVascuarAccessListByID
        {
            get
            {
                return "SELECT * FROM MED_VASCULAR_ACCESS WHERE VASCULAR_ACCESS_ID=:VASCULAR_ACCESS_ID";
                // return @"SELECT t.*,m.item_name as VASCULAR_POSTION_NAME,mm.item_name as vascular_access_type_name FROM MED_VASCULAR_ACCESS t left join med_common_itemlist m
                //         on t.VASCULAR_POSTION = m.item_id left join med_common_itemlist mm on t.vascular_access_type = mm.item_id";
            }
        }


        /// <summary>
        /// 根据血管通路编号得到血管通路数据中文列数据
        /// </summary>
        public string GetVascuarAccessNameByID
        {
            get
            {
                return @"SELECT t.*,m.item_name as VASCULAR_POSTION_NAME,mm.item_name as vascular_access_type_name FROM MED_VASCULAR_ACCESS t left join med_common_itemlist m
                        on t.VASCULAR_POSTION = m.item_id left join med_common_itemlist mm on t.vascular_access_type = mm.item_id
                         WHERE t.VASCULAR_ACCESS_ID=:VASCULAR_ACCESS_ID";
            }
        }
        public string GetNotAssentDayByHemoId
        {
            get
            {
                return @"SELECT NVL((TRUNC(SYSDATE) - MAX(T.ASSESSMENT_DATE)),-1) AS DAY FROM MED_PATIENTS_ASSESSMENT T WHERE T.HEMODIALYSIS_ID = :HEMODIALYSIS_ID";
            }

        }
        #endregion

        #region 系统参数相关SQL

        /// <summary>
        /// 获取系统参数数据SQL
        /// </summary>
        public string GetConfigList
        {
            get
            {
                return @"
                    SELECT
                        t.*
                    FROM MED_COMMON_ITEMLIST t
                    WHERE 
                        t.ITEM_VALUE LIKE '%'||:ITEM_VALUE||'%' 
                        AND t.ITEM_NAME LIKE '%'||:ITEM_NAME||'%' 
                        AND t.ITEM_TYPE = :ITEM_TYPE
                        AND (t.STATUS = :STATUS OR :STATUS IS NULL) ";
                //                return @"
                //                    SELECT
                //                        t.*,
                //                        CASE
                //                            WHEN t.STATUS = 0 THEN '停用'
                //                            WHEN t.STATUS = 1 THEN '启用'      
                //                        END STATUSSTR
                //                    FROM MED_COMMON_ITEMLIST t
                //                    WHERE 
                //                        t.ITEM_VALUE LIKE '%'||:ITEM_VALUE||'%' 
                //                        AND t.ITEM_NAME LIKE '%'||:ITEM_NAME||'%' 
                //                        AND t.ITEM_TYPE = :ITEM_TYPE
                //                        AND (t.STATUS = :STATUS OR :STATUS IS NULL)
                //                    ORDER BY 
                //                        t.ITEM_TYPE, t.ORDER_NUMBER";
            }
        }

        public string GetFeeItemConfigList
        {
            get
            {
                return @"SELECT * FROM MED_COMMON_ITEMLIST T
                            WHERE T.ITEM_TYPE=:ITEM_TYPE";
            }

        }
        public string GetConfigAccountItem
        {
            get
            {
                return @"SELECT T.*,
                           (SELECT L.ITEM_NAME
                              FROM MED_COMMON_ITEMLIST L
                             WHERE L.ITEM_VALUE = T.ITEM_ID
                               AND L.ITEM_TYPE = '费用') PRICE
                      FROM MED_COMMON_ITEMLIST T
                     WHERE T.ITEM_TYPE IN ('大记账项目', '小记账项目')";
            }
        }
        /// <summary>
        /// 获取系统参数类型数据SQL
        /// </summary>
        public string GetConfigTypeList
        {
            get
            {
                return @"
                SELECT 
                    t.ITEM_TYPE, COUNT(1) AS Count 
                FROM MED_COMMON_ITEMLIST t 
                GROUP BY 
                    t.ITEM_TYPE 
                ORDER BY 
                    t.ITEM_TYPE";
            }
        }
        public string GetConfigListByHemoIDandItemType
        {
            get
            {
                return @"SELECT DISTINCT I.*,T.* FROM (SELECT T.VASCULAR_ACCESS_TYPE,T.CREATE_DATE
                        FROM (SELECT T.VASCULAR_ACCESS_TYPE,T.CREATE_DATE,RANK() OVER(PARTITION BY T.VASCULAR_ACCESS_TYPE ORDER BY T.CREATE_DATE DESC) AS TYPE_RANK
                        FROM MED_VASCULAR_ACCESS T WHERE T.HEMODIALYSIS_ID = :HEMODIALYSIS_ID) T WHERE TYPE_RANK = 1 ORDER BY T.CREATE_DATE DESC) T
                        INNER JOIN MED_COMMON_ITEMLIST I ON T.VASCULAR_ACCESS_TYPE = I.ITEM_ID
                        WHERE I.ITEM_TYPE = :ITEM_TYPE AND (I.STATUS = '1' OR I.STATUS IS NULL)";
            }
        }
        public string GetPatientVasular_AccessDt
        {
            get
            {
                return @"SELECT T4.ITEM_NAME || T1.ITEM_NAME || T2.ITEM_NAME || T3.ITEM_NAME AS PATIENT_ID,
                              T.VASCULAR_ACCESS_ID,
                              T.VASCULAR_ACCESS_TYPE,
                              T.ACCESS_MATERIA,
                              T.LATERAL_POSITION,
                              T.VASCULAR_POSTION,
                              T.BLOOD_VESSEL,
                              T.MODUS_OPERANDI,
                              T.ACCESS_CLASS,
                              T.CREATE_DATE,
                              T.HOSPITAL,
                              T.ACCESS_STATUS,
                              T.FIRST_DATE,
                              T.IS_SUCCESS,
                              T.FIRST_LOSE_DATE,
                              T.TUBE_DRAWING_DATE,
                              T.LOSE_REASON,
                              T.HEMODIALYSIS_ID
                         FROM MED_VASCULAR_ACCESS T
                         LEFT JOIN MED_COMMON_ITEMLIST T1
                           ON T.VASCULAR_ACCESS_TYPE = T1.ITEM_ID
                         LEFT JOIN MED_COMMON_ITEMLIST T2
                           ON T.VASCULAR_POSTION = T2.ITEM_ID
                         LEFT JOIN MED_COMMON_ITEMLIST T3
                           ON T.ACCESS_CLASS = T3.ITEM_ID
                         LEFT JOIN MED_COMMON_ITEMLIST T4
                           ON T.LATERAL_POSITION = T4.ITEM_ID
                        WHERE T.IS_SUCCESS = '1'
                          AND T.HEMODIALYSIS_ID = :HEMODIALYSIS_ID
                        ORDER BY T.CREATE_DATE DESC";
            }
        }
        public string GetCommRelation
        {
            get { return @"SELECT * FROM MED_COMMON_RELATION"; }
        }
        public string DeleteCommonRelationById
        {
            get
            {
                return @"DELETE FROM MED_COMMON_RELATION WHERE ID = :ID";
            }
        }
        #endregion

        #region 病人数据相关SQL

        /// <summary>
        /// 获取血透前病人信息数据SQL
        /// </summary>
        public string GetBeforeHemodialysisSignList
        {
            get
            {
                return "SELECT * FROM MED_BEFORE_HEMODIALYSIS_SIGN ORDER BY CREATE_DATE DESC";
            }
        }


        public string GetPatientsDt
        {
            get
            {
                return @" SELECT T.HEMODIALYSIS_ID AS ID,
                                   T.HEMODIALYSIS_ID,
                                   T.PATIENT_ID,
                                   T.NAME,
                                   T.SEX,
                                   T.AGE,
                                   T.CREATE_DATE AS CREATEDATE
                               FROM MED_PATIENTS T
                              WHERE T.HEMODIALYSIS_ID NOT IN
                                    (SELECT HEMODIALYSIS_ID FROM MED_PATIENTS_CARD)";
            }
        }

        public string GetPatientCardDt
        {
            get
            {
                return @"SELECT T.HEMODIALYSIS_ID,
                               T.PATIENT_ID,
                               T.NAME,
                               T.SEX,
                               T.AGE,
                               T1.ID,
                               T1.SERIALNUMBER,
                               T1.SEC,
                               T1.CARDKEY,
                               T1.CARDNO,
                               T1.CARDVALUE,
                               T1.STATE,
                               T1.CREATEBY,
                               (SELECT S.USER_NAME FROM MED_USERS S WHERE S.USER_ID = T1.CREATEBY) USER_NAME,
                               T1.CREATEDATE
                          FROM MED_PATIENTS T, MED_PATIENTS_CARD T1
                         WHERE T.HEMODIALYSIS_ID = T1.HEMODIALYSIS_ID ";
            }
        }

        public string GetCardInfoByCardInfo
        {
            get
            {
                return @"SELECT * FROM MED_PATIENTS_CARD T                            
                             WHERE T.SERIALNUMBER = :SERIALNUMBER
                               AND T.CARDNO = :CARDNO";
            }
        }

        public string GetCardInfoByInfo
        {
            get
            {
                return @"SELECT * FROM MED_PATIENTS_CARD T
                             WHERE  (T.HEMODIALYSIS_ID = :HEMODIALYSIS_ID   OR T.Serialnumber = :CARDNO)
                               "; //AND T.STATE IN ('0')
            }
        }

        public string UpdateCardStateByParam
        {
            get
            {
                return @"  UPDATE MED_PATIENTS_CARD
                              SET STATE= :STATE
                           WHERE HEMODIALYSIS_ID = :HEMODIALYSIS_ID";
            }
        }

        public string GetPatientKolcabaByParams
        {
            get
            {
                return @"SELECT T.ID,T.HEMODIALYSIS_ID,T.CREATEDATE,
                         (SELECT S.USER_NAME FROM MED_USERS S WHERE S.USER_ID = T.LASTUPDATEBY) AS LASTUPDATEBY,
                         T.LASTUPDATEDATE,T.TOTALSCORE,T.ITEM1,T.ITEM2,T.ITEM3,T.ITEM4,T.ITEM5,T.ITEM6,T.ITEM7,T.ITEM8,T.ITEM9,
                         T.ITEM10,T.ITEM11,T.ITEM12,T.ITEM13,T.ITEM14,T.ITEM15,T.ITEM16,T.ITEM17,T.ITEM18,T.ITEM19,T.ITEM20,
                         T.ITEM21,T.ITEM22,T.ITEM23,T.ITEM24,T.ITEM25,T.ITEM26,T.ITEM27,T.ITEM28
                         FROM MED_PATIENT_KOLCABA T WHERE
                         T.HEMODIALYSIS_ID LIKE '%'||:HEMODIALYSIS_ID||'%'
                         AND  T.CREATEDATE >= :BEGINTIME
                         AND  T.CREATEDATE <= :ENDTIME";
            }
        }

        public string QueryPatientMaterialByParams
        {
            get
            {
                return @"SELECT T.ID,
                               T.HEMODIALYSIS_ID,
                               T.CREATEDATE,
                               T.LASTUPDATEDATE,
                               T.RECORDID,
                               T.MATERIAL_ID,
                               T.MATERIAL_NAME,
                               T.MATERIAL_NUMBER,
                               T.ROWINDEX,
                               T.ISDELETE,
                               T.ITEMTYPE,
                               T.MATERTYPE,
                               T.RECIPEID,
                               T.STATE,
                               (SELECT S.USER_NAME FROM MED_USERS S WHERE S.USER_ID = T.LASTUPDATEBY) AS LASTUPDATEBY
                          FROM MED_PATIENT_MATERIAL T
                         WHERE T.HEMODIALYSIS_ID LIKE '%' || :HEMODIALYSIS_ID || '%'
                           AND T.RECIPEID LIKE '%' || :RECIPEID || '%'
                        /*   AND T.CREATEDATE >= :BEGINTIME
                           AND T.CREATEDATE <= :ENDTIME*/
                           AND T.ROWINDEX = 1
                           AND T.ISDELETE = '0'";
            }
        }

        public string GetPatientKolcabaByHemoIDandDate
        {
            get
            {
                return "SELECT * FROM MED_PATIENT_KOLCABA  T WHERE T.HEMODIALYSIS_ID = :HEMODIALYSIS_ID AND trunc(T.CREATEDATE) =trunc(:CREATEDATE)";
            }
        }

        public string DeleteAirPurgeData
        {
            get
            {
                return @"DELETE MED_MACHINE_AIRPURGE  WHERE ID=:ID";
            }
        }
        public string QueryPatientSufficiencyByParams
        {
            get
            {
                return @"SELECT T.*,(SELECT S.USER_NAME FROM MED_USERS S WHERE S.USER_ID = T.LASTUPDATEBY) AS USERNAME
                         FROM MED_PATIENT_SUFFICIENCY T WHERE ISDELETE='0' AND
                         T.HEMODIALYSIS_ID LIKE '%'||:HEMODIALYSIS_ID||'%'
                         AND  T.CREATEDATE >= :BEGINTIME
                         AND  T.CREATEDATE <= :ENDTIME ORDER BY CREATEDATE DESC";
            }
        }

        /// <summary>
        /// 根据ID删除舒充分性评估数据
        /// </summary>
        public string DeletePatientSufficiencyById
        {
            get
            {
                return @"UPDATE MED_PATIENT_SUFFICIENCY SET ISDELETE='1' WHERE ID=:ID";
            }
        }

        public string GetPatientSufficiencyByHemoIDandDate
        {
            get
            {
                return "SELECT * FROM MED_PATIENT_SUFFICIENCY WHERE ID = :ID ";
            }
        }

        public string GetRastBillByHemoID
        {
            get
            {
                return @"SELECT *
                        FROM(SELECT SYS_GUID() ||'1' AS ID,
                            T.HEMODIALYSIS_ID,
                           T.ITEM_ID,
                            (SELECT L.ITEM_NAME
                              FROM MED_COMMON_ITEMLIST L
                             WHERE L.ITEM_ID = T.ITEM_ID) ITEM_NAME,
                           SUM(T.ITEM_COUNT) - NVL((SELECT SUM(L.BILL_COUNT)
                                                     FROM MED_CURE_MAIN_BILL L
                                                    WHERE L.HEMODIALYSIS_ID = T.HEMODIALYSIS_ID
                                                      AND L.BILL_ITEM_ID = T.ITEM_ID),
                                                   0) ITEM_COUNT,
                           SUM(T.PREPAYCOST) - NVL((SELECT SUM(L.BILL_PRICE)
                                                     FROM MED_CURE_MAIN_BILL L
                                                    WHERE L.HEMODIALYSIS_ID = T.HEMODIALYSIS_ID
                                                      AND L.BILL_ITEM_ID = T.ITEM_ID),
                                                   0) PREPAYCOST
                      FROM MED_PATIENT_PREPAY T
                      WHERE T.HEMODIALYSIS_ID=:HEMODIALYSIS_ID
                     GROUP BY T.HEMODIALYSIS_ID, T.ITEM_ID
                         )Z WHERE Z.ITEM_COUNT >0";
            }
        }

        public string GetCurrentRastByHemoID
        {
            get
            {
                return @"SELECT SYS_GUID() || '1' AS ID,
                                   T.HEMODIALYSIS_ID,
                                   NVL(SUM(Y.PREPAYCOST), 0) -
                                   NVL((SELECT SUM(L.PRICE)
                                         FROM MED_PATIENT_MATERIAL L
                                        WHERE L.HEMODIALYSIS_ID = T.HEMODIALYSIS_ID
                                          AND L.ISDELETE = '0'
                                          AND L.STATE = '1'),
                                       0) PREPAYCOST
                              FROM MED_PATIENTS T
                              LEFT JOIN MED_PATIENT_PREPAY Y
                                ON T.HEMODIALYSIS_ID = Y.HEMODIALYSIS_ID
                             WHERE T.HEMODIALYSIS_ID = :HEMODIALYSIS_ID
                             GROUP BY T.HEMODIALYSIS_ID";
            }
        }
        public string GetPatientPrePayInfos
        {
            get
            {
                return @"SELECT *
                          FROM (SELECT T.ID,
                                       T.HEMODIALYSIS_ID,
                                       T.ITEM_ID,
                                       (SELECT L.ITEM_NAME
                                          FROM MED_COMMON_ITEMLIST L
                                         WHERE L.ITEM_ID = T.ITEM_ID) ITEM_NAME,
                                       T.ITEM_COUNT,
                                       T.PREPAYCOST,
                                       T.PAYTIME,
                                       T.CREATEBY,
                                       T.CREATEDATE,
                                       (SELECT S.USER_NAME
                                          FROM MED_USERS S
                                         WHERE S.USER_ID = T.CREATEBY) AS CREATENAME
                                  FROM MED_PATIENT_PREPAY T
                                 WHERE T.HEMODIALYSIS_ID = :HEMODIALYSIS_ID
                                 ORDER BY T.PAYTIME DESC) Z";
            }
        }

        public string GetHemoAccountCostByHemoId
        {
            get
            {
                return @"SELECT (SUM(PAYCOST) - SUM(BILLCOST) - SUM(WARINGCOST)) AS COST
                          FROM (SELECT NVL(SUM(T.PREPAYCOST), 0) PAYCOST,
                                       0 AS BILLCOST,
                                       0 AS WARINGCOST
                                  FROM MED_PATIENT_PREPAY T
                                 WHERE T.HEMODIALYSIS_ID = :HEMODIALYSIS_ID
                                UNION
                                SELECT 0 AS PAYCOST,
                                       NVL(SUM(T.BILL_PRICE), 0) AS BILLCOST,
                                       0 AS WARINGCOST
                                  FROM MED_CURE_MAIN_BILL T
                                 WHERE T.HEMODIALYSIS_ID = :HEMODIALYSIS_ID
                                UNION
                                SELECT 0 AS PAYCOST,
                                       0 AS BILLCOST,
                                       NVL(SUM(N.CORDON), 0) AS WARINGCOST
                                  FROM MED_HEMO_CORDON N
                                 WHERE N.HEMODIALYSIS_ID = :HEMODIALYSIS_ID) Z";
            }
        }

        public string GetPatientRecipeWeigthChart
        {
            get
            {
                return @"SELECT '干体重' NAME, TO_CHAR(RECIPE_DATE,'yyyy-mm-dd') AS TIME, DRY_WEIGHT AS VALUE
                              FROM (SELECT MIN(TRUNC(T.RECIPE_DATE)) RECIPE_DATE, T.DRY_WEIGHT
                                      FROM MED_HEMO_RECIPE T
                                     WHERE T.RECIPE_TYPE = '0'
                                       AND T.STATUS = '0'
                                       AND T.HEMODIALYSIS_ID = :HEMODIALYSIS_ID
                                       AND TRUNC(T.RECIPE_DATE) > = :BEGINDATE
                                       AND TRUNC(T.RECIPE_DATE) <= :ENDDATE
                                     GROUP BY T.DRY_WEIGHT,T.RECIPE_DATE)
                             ORDER BY RECIPE_DATE";
            }
        }

        public string GetPatientRecipeCureChart
        {
            get
            {
                return @"SELECT NAME, to_char(time,'yyyy-mm-dd') as TIME, value
                          FROM (SELECT (SELECT L.ITEM_NAME
                                          FROM MED_COMMON_ITEMLIST L
                                         WHERE L.ITEM_ID = T.THERAPEUTIC_METHOD) NAME,
                                       MIN(TRUNC(T.RECIPE_DATE)) TIME,
                                       T.FIRST_DRUG_DOSAGE VALUE
                                  FROM MED_HEMO_RECIPE T
                                 WHERE T.RECIPE_TYPE = '0'
                                   AND T.STATUS = '0'
                                   AND T.HEMODIALYSIS_ID = :HEMODIALYSIS_ID
                                   AND TRUNC(T.RECIPE_DATE) > = :BEGINDATE
                                   AND TRUNC(T.RECIPE_DATE) <= :ENDDATE
                                 GROUP BY T.THERAPEUTIC_METHOD, T.FIRST_DRUG_DOSAGE,T.RECIPE_DATE)
                         ORDER BY TIME";
            }
        }

        public string GetPatientRecipeSPKTChart
        {
            get
            {
                return @"SELECT 'SPKT' NAME, TO_CHAR(RECIPE_DATE,'yyyy-mm-dd') AS TIME, SPKT_V AS VALUE
                              FROM (SELECT MIN(TRUNC(T.CREATE_DATE)) RECIPE_DATE, T.SPKT_V
                                      FROM MED_ESTIMATE_SUFFICIENCY T
                                     WHERE T.IS_DELETE = '0'                                      
                                       AND T.HEMODIALYSIS_ID = :HEMODIALYSIS_ID
                                       AND TRUNC(T.CREATE_DATE) > = :BEGINDATE
                                       AND TRUNC(T.CREATE_DATE) <= :ENDDATE
                                     GROUP BY T.SPKT_V,T.CREATE_DATE)
                             ORDER BY RECIPE_DATE";
            }
        }

        public string GetPatientRecipeURRChart
        {
            get
            {
                return @"SELECT 'URR' NAME, TO_CHAR(RECIPE_DATE,'yyyy-mm-dd') AS TIME, URR AS VALUE
                              FROM (SELECT MIN(TRUNC(T.CREATE_DATE)) RECIPE_DATE, T.URR
                                      FROM MED_ESTIMATE_SUFFICIENCY T
                                     WHERE T.IS_DELETE = '0'                                      
                                       AND T.HEMODIALYSIS_ID = :HEMODIALYSIS_ID
                                       AND TRUNC(T.CREATE_DATE) > = :BEGINDATE
                                       AND TRUNC(T.CREATE_DATE) <= :ENDDATE
                                     GROUP BY T.URR,T.CREATE_DATE)
                             ORDER BY RECIPE_DATE";
            }
        }

        public string GetPatientPrePayInfosByUserId
        {
            get
            {
                return @"SELECT T.ID,
                           T.HEMODIALYSIS_ID,
                           (SELECT P.NAME
                              FROM MED_PATIENTS P
                             WHERE P.HEMODIALYSIS_ID = T.HEMODIALYSIS_ID) NAME,
                           (SELECT L.ITEM_NAME
                              FROM MED_COMMON_ITEMLIST L
                             WHERE L.ITEM_ID = T.ITEM_ID) ITEM_NAME,
                           T.ITEM_COUNT,
                           T.PREPAYCOST,
                           T.PAYTIME,
                           T.CREATEBY,
                           T.CREATEDATE,
                           (SELECT S.USER_NAME FROM MED_USERS S WHERE S.USER_ID = T.CREATEBY) AS CREATENAME
                      FROM MED_PATIENT_PREPAY T
                     WHERE T.CREATEBY = :CREATEBY
                       AND TRUNC(T.PAYTIME) = TRUNC(SYSDATE)
                     ORDER BY T.PAYTIME DESC";
            }
        }
        public string GetHemoPatientCorDon
        {

            get
            {
                return @"SELECT T.HEMODIALYSIS_ID,
                                   T.PATIENT_ID,
                                   T.NAME,
                                   T.SEX,
                                   T.AGE,
                                   (SELECT (SELECT ITEM_NAME
                                              FROM MED_COMMON_ITEMLIST S
                                             WHERE S.ITEM_ID = L.DIALYSIS_ROOM_ID)
                                      FROM MED_PATIENT_SCHEDULE L
                                     WHERE L.HEMODIALYSIS_ID = T.HEMODIALYSIS_ID AND L.DIALYSIS_DATE=TRUNC(SYSDATE)
                                       AND ROWNUM = 1) AREANAME,
                                  (SELECT L.DIALYSIS_ROOM_ID
                                          FROM MED_PATIENT_SCHEDULE L
                                         WHERE L.HEMODIALYSIS_ID = T.HEMODIALYSIS_ID AND L.DIALYSIS_DATE=TRUNC(SYSDATE)
                                           AND ROWNUM = 1) AREAID,
                                   (SELECT N.CORDON
                                      FROM MED_HEMO_CORDON N
                                     WHERE N.HEMODIALYSIS_ID = T.HEMODIALYSIS_ID) CORDON,
                                   (SELECT N.CREATEBY
                                      FROM MED_HEMO_CORDON N
                                     WHERE N.HEMODIALYSIS_ID = T.HEMODIALYSIS_ID) CREATEBY,
                                   (SELECT N.CREATEDATE
                                      FROM MED_HEMO_CORDON N
                                     WHERE N.HEMODIALYSIS_ID = T.HEMODIALYSIS_ID) CREATEDATE,
                                   (SELECT (SELECT NAME FROM MED_STAFF_DICT D WHERE D.EMP_NO = N.CREATEBY)
                                      FROM MED_HEMO_CORDON N
                                     WHERE N.HEMODIALYSIS_ID = T.HEMODIALYSIS_ID) CREATENAME
                              FROM MED_PATIENTS T
                             WHERE T.IS_DELETE != '1'";
            }
        }

        public string GetHavingHemoPatientCorDon
        {
            get
            {
                return @"SELECT * FROM MED_HEMO_CORDON T";
            }
        }
        #region 患者风险评估
        public string GetPatientAssessScoreByDate
        {
            get
            {
                return @"SELECT T.ID,
                    T.ASSENEMENT,
                    T.HEMODIALYSIS_ID,
                    T.CANAL,
                    T.PRESSURE,
                    T.FALL,
                    T.NURSERECORD,
                    T.NURSEID,
                    T.CREATEBY,
                    T.CREATEDATE,S.NAME,E.NAME AS NURSENAME FROM MED_PATIENTS_ASSESSMENT_SCORE T 
                    LEFT JOIN MED_PATIENTS S ON T.HEMODIALYSIS_ID = S.HEMODIALYSIS_ID
                    LEFT JOIN MED_STAFF_DICT E ON E.EMP_NO = T.NURSEID
                    WHERE TRUNC(T.ASSENEMENT) >= :BEGINDATE
                    AND TRUNC(T.ASSENEMENT) <= :ENDDATE
                    AND S.NAME LIKE '%'||:NAME||'%'
                    AND T.NURSEID LIKE '%'||:NURSEID||'%'
                    ORDER BY T.ASSENEMENT";
            }
        }
        public string DeletePatientAssessScoreById
        {
            get
            {
                return @"DELETE MED_PATIENTS_ASSESSMENT_SCORE
                         WHERE ID = :ID";
            }
        }

        #endregion
        #region 交班
        public string GetChangeWorkByDate
        {
            get
            {
                return @"SELECT T.*,
                           (SELECT P.HEMODIALYSIS_ID
                              FROM MED_PATIENTS P
                             WHERE P.HEMODIALYSIS_ID = T.PATIENT) AS HEMODIALYSIS_ID,
                           (SELECT P.SEX FROM MED_PATIENTS P WHERE P.HEMODIALYSIS_ID = T.PATIENT) AS SEX,
                           (SELECT P.AGE FROM MED_PATIENTS P WHERE P.HEMODIALYSIS_ID = T.PATIENT) AS AGE,
                           (SELECT P.NAME FROM MED_PATIENTS P WHERE P.HEMODIALYSIS_ID = T.PATIENT) AS NAME,
                           (SELECT P.DIAGNOSE FROM MED_PATIENTS P WHERE P.HEMODIALYSIS_ID = T.PATIENT) AS DIAGNOSE
                      FROM MED_HEMO_CHAGEWORK T
                     WHERE T.TYPE = '1'
                       AND T.CHANGETIME >= TRUNC(:BEGINDATE)
                       AND T.CHANGETIME <= TRUNC(:ENDDATE)";
            }
        }
        public string GetChangeNurseWorkByDate
        {
            get
            {
                return @"SELECT T.*,
                           (SELECT P.NAME FROM MED_STAFF_DICT P WHERE P.EMP_NO = T.CHANGEUSER) AS HEMODIALYSIS_ID,
                           (SELECT P.ITEM_NAME
                              FROM MED_COMMON_ITEMLIST P
                             WHERE P.ITEM_ID = T.AREA) AS DIAGNOSE,
                            (SELECT P.ITEM_VALUE
                                      FROM MED_COMMON_ITEMLIST P
                                     WHERE P.ITEM_ID = T.AREA) AS SEX
                      FROM MED_HEMO_CHAGEWORK T
                     WHERE T.TYPE = '0' 
                       AND T.CHANGETIME >= TRUNC(:BEGINDATE)
                       AND T.CHANGETIME <= TRUNC(:ENDDATE)
                      ORDER BY SEX ";
            }
        }
        public string GetChageWorkExtendByMonth
        {
            get
            {
                return @"SELECT SYS_GUID() || 'A' AS ID,
                               SUM(ALLCOUNT) ALLCOUNT,
                               SUM(NEWCOUNT) NEWCOUNT,
                               SUM(CRITICAL) CRITICAL, -- 危重病人数
                               SUM(HDCOUNT) HDCOUNT, --  透析人数
                               SUM(HDFCOUNT) HDFCOUNT, --  透析滤过人数
                               SUM(HDPCOUNT) HDPCOUNT, --  透析灌流人数
                               SUM(UPTAKECOUNT) UPTAKECOUNT, --  吸氧气人数
                               SUM(NOPAIRCOUNT) NOPAIRCOUNT, --       无肝素人数
                               SUM(DIECOUNT) DIECOUNT, --       死亡人数
                               SUM(ECGCOUNT) ECGCOUNT, --   心电监护人数
                               SUM(SALVECOUNT) SALVECOUNT, --     抢救人数
                               SUM(BLOODCOUNT) BLOODCOUNT, --     输血人数
                               CHANGETIME, --     交班时间
                               'AA' CREATEBY, --     创建人员
                               SYSDATE CREATEDATE --    创建时间
                          FROM MED_HEMO_CHAGEWORK T
                         WHERE T.CHANGETIME >= TRUNC(:BEGINDATE)
                           AND T.CHANGETIME <= TRUNC(:ENDDATE)
                         GROUP BY T.CHANGETIME
                         ORDER BY T.CHANGETIME";
            }
        }
        public string DeleteChangeWorkById
        {
            get
            {
                return @"DELETE MED_HEMO_CHAGEWORK WHERE ID =:ID";
            }
        }
        public string GetScheduleCoutByAreaID
        {
            get
            {
                return @"SELECT T.ITEM_VALUE,
                               (SELECT COUNT(DISTINCT T1.PATIENT_SCHEDULE_ID)
                                  FROM MED_PATIENT_SCHEDULE T1, MED_CURE_MAIN N
                                 WHERE T1.BANCI_ID = T.ITEM_VALUE
                                   AND TRUNC(T1.DIALYSIS_DATE) = TRUNC(N.CURE_CREATE_DATE)  AND T1.RECIPE_ID = N.RECIPE_ID
                                   AND T1.DIALYSIS_DATE = TRUNC(:DIALYSIS_DATE)
                                   AND T1.DIALYSIS_ROOM_ID = :AREAID) COUNT
                          FROM MED_COMMON_ITEMLIST T
                         WHERE T.ITEM_TYPE = '班次'
                        UNION
                        SELECT T.ITEM_VALUE,
                               (SELECT COUNT(DISTINCT T1.PATIENT_SCHEDULE_ID)
                                  FROM MED_PATIENT_SCHEDULE T1, MED_CURE_MAIN N
                                 WHERE TRUNC(T1.DIALYSIS_DATE) = TRUNC(N.CURE_CREATE_DATE)  AND T1.RECIPE_ID = N.RECIPE_ID
                                   AND N.PURIFICATION_MODE = T.ITEM_ID
                                   AND T1.DIALYSIS_DATE = TRUNC(:DIALYSIS_DATE)
                                   AND T1.DIALYSIS_ROOM_ID = :AREAID) COUNT
                          FROM MED_COMMON_ITEMLIST T
                         WHERE T.ITEM_TYPE = '净化方式'
                           AND T.ITEM_NAME IN ('HD', 'HDF', 'HD+HP')
                        UNION
                        SELECT 'ALL' ITEM_VALUE, COUNT(DISTINCT T1.PATIENT_SCHEDULE_ID) COUNT
                          FROM MED_PATIENT_SCHEDULE T1, MED_CURE_MAIN N
                         WHERE TRUNC(T1.DIALYSIS_DATE) = TRUNC(N.CURE_CREATE_DATE)  AND T1.RECIPE_ID = N.RECIPE_ID
                           AND T1.DIALYSIS_DATE = TRUNC(:DIALYSIS_DATE)
                           AND T1.DIALYSIS_ROOM_ID = :AREAID";
            }
        }

        public string GetScheduleCoutByParmars
        {
            get
            {
                return @"SELECT (SELECT L.ITEM_NAME
                                  FROM MED_COMMON_ITEMLIST L
                                 WHERE L.ITEM_VALUE = T1.BANCI_ID
                                   AND L.ITEM_TYPE = '班次') ITEM_VALUE,  COUNT(DISTINCT T1.PATIENT_SCHEDULE_ID) COUNT
                          FROM MED_PATIENT_SCHEDULE T1, MED_CURE_MAIN N
                         WHERE T1.RECIPE_ID = N.RECIPE_ID
                           AND T1.DIALYSIS_DATE = TRUNC(:DIALYSIS_DATE)
                           AND T1.DIALYSIS_ROOM_ID = :AREAID
                           AND T1.BANCI_ID = :BANCI_ID
                            GROUP BY T1.BANCI_ID
                        UNION
                        SELECT T.ITEM_VALUE,
                               (SELECT COUNT(DISTINCT T1.PATIENT_SCHEDULE_ID)
                                  FROM MED_PATIENT_SCHEDULE T1, MED_CURE_MAIN N
                                 WHERE T1.RECIPE_ID = N.RECIPE_ID
                                   AND N.PURIFICATION_MODE = T.ITEM_ID
                                   AND T1.DIALYSIS_DATE = TRUNC(:DIALYSIS_DATE)
                                   AND T1.DIALYSIS_ROOM_ID = :AREAID
                                   AND T1.BANCI_ID = :BANCI_ID) COUNT
                          FROM MED_COMMON_ITEMLIST T
                         WHERE T.ITEM_TYPE = '净化方式'
                           AND T.ITEM_NAME IN ('HD', 'HDF', 'HD+HP')";
            }
        }

        public string GetChangeWorkByParm
        {
            get
            {
                return @"SELECT *
                          FROM MED_HEMO_CHAGEWORK T
                         WHERE T.AREA = :AREAID
                           AND T.CHANGETIME = TRUNC(:CHANGETIME)";
            }
        }

        public string GetPatientOperatorByDate
        {
            get
            {
                return @"SELECT T.*,O.ITEM_NAME AS OPERATE_NAME,M.ITEM_NAME AS ANESTHESIA_METHOD,S1.NAME AS ANESTHESIOLOGIST_NAME,S2.NAME AS OPE_DOC_NAME FROM MED_PATIENTS_OPERATOR T
                         LEFT JOIN MED_COMMON_ITEMLIST O ON T.OPE_NAME=O.ITEM_ID
                         LEFT JOIN MED_COMMON_ITEMLIST M ON T.ANESTHESIAMETHOD=M.ITEM_ID
                         LEFT JOIN MED_STAFF_DICT S1 ON T.ANESTHESIOLOGIST=S1.EMP_NO
                         LEFT JOIN MED_STAFF_DICT S2 ON T.OPE_DOC=S2.EMP_NO
                         WHERE TRUNC(T.OPE_STAR) >= TRUNC(:OPE_STAR)
                           AND TRUNC(T.OPE_END) <= TRUNC(:OPE_END)";
            }
        }
        public string DeletePatientOperatorById
        {
            get { return @"DELETE MED_PATIENTS_OPERATOR WHERE ID=:ID"; }
        }


        #endregion

        public string GetBaseRecordByHemoId
        {
            get
            {
                return @"SELECT T.*,P.NAME AS PatientName FROM MED_BASE_RECORD T
                        LEFT JOIN MED_PATIENTS P ON T.HEMODIALYSIS_ID=P.HEMODIALYSIS_ID
                        WHERE T.HEMODIALYSIS_ID=:HEMODIALYSIS_ID";
            }
        }

        public string GetRecordEventByHemoId
        {
            get
            {
                return @"SELECT E.ID,E.HEMODIALYSIS_ID,E.CREATEBY,E.CREATE_DATE,E.UPDATEBY,E.UPDATE_DATE,E.DIALYSIS_STAGE,E.COMPLICATION,E.CHRONIC_EVENT,S.NAME AS DOCTOR_NAME
                        FROM MED_BASE_RECORD_EVENT E LEFT JOIN MED_STAFF_DICT S ON E.CREATEBY = S.EMP_NO WHERE E.HEMODIALYSIS_ID =:HEMODIALYSIS_ID AND E.IS_DELETE='0' ORDER BY E.CREATE_DATE";
            }
        }

        public string DeleteRecordEventById
        {
            get
            {
                return @"UPDATE MED_BASE_RECORD_EVENT SET IS_DELETE='1' WHERE ID=:ID";
            }
        }

        public string GetRecordDiagnoseByHemoId
        {
            get
            {
                return @"SELECT E.ID,E.HEMODIALYSIS_ID,E.CREATEBY,E.CREATE_DATE,E.UPDATEBY,E.UPDATE_DATE,E.IN_HOSPITAL_DATE,E.LEAVE_HOSPITAL_DATE,E.DIAGNOSE,S.NAME AS DOCTOR_NAME
                        FROM MED_BASE_RECORD_DIAGNOSE E LEFT JOIN MED_STAFF_DICT S ON E.CREATEBY = S.EMP_NO WHERE E.HEMODIALYSIS_ID =:HEMODIALYSIS_ID AND E.IS_DELETE='0' ORDER BY E.CREATE_DATE";
            }
        }

        public string DeleteRecordDiagnoseById
        {
            get
            {
                return @"UPDATE MED_BASE_RECORD_DIAGNOSE SET IS_DELETE='1' WHERE ID=:ID";
            }
        }

        #endregion

        #region 药品设置相关SQL
        /// <summary>
        /// 得到全部药品主档列表 
        /// </summary>
        public string GetDrugMasterList
        {
            get
            {
                return "SELECT * FROM MED_DRUG_MASTER ORDER BY CREATE_DATE DESC";
            }
        }
        public string GetDaleGateDrugMasterList
        {
            get
            {
                return @"SELECT *
                          FROM MED_DRUG_MASTER T
                         WHERE T.DRUG_TYPE IN (SELECT L.ITEM_ID
                                                 FROM MED_COMMON_ITEMLIST L
                                                WHERE L.ITEM_TYPE = '托管药品分类')
                         ORDER BY CREATE_DATE DESC";
            }
        }
        /// <summary>
        /// 根据查询条件，得到药品主档列表SQL
        /// </summary>
        public string GetDrugMasterListByParams
        {
            get
            {
                return
                    @"
                    SELECT
                        t.*
                    FROM MED_DRUG_MASTER t
                    WHERE 
                        t.DRUG_CODE LIKE '%'||:DRUG_CODE||'%'
                        and (t.DRUG_NAME LIKE '%'||:DRUG_NAME||'%' OR t.DRUG_PINYIN LIKE '%'||:DRUG_NAME||'%') 
                        and t.FIRM_ID LIKE '%'||:FIRM_ID||'%'
                        ORDER BY 
                        t.CREATE_DATE DESC";
            }
        }

        /// <summary>
        /// 根据药品编号得到数据
        /// </summary>
        public string GetDrugMasterListByDrugCode
        {
            get
            {
                return "SELECT * FROM MED_DRUG_MASTER WHERE DRUG_CODE = :DRUG_CODE";
            }
        }

        /// <summary>
        /// 得到新生成的药品编号
        /// </summary>
        /// <returns></returns>
        public string GetNewDrugCode
        {
            get
            {
                return "SELECT MAX(DRUG_CODE)+1 AS DRUG_CODE FROM MED_DRUG_MASTER";
            }
        }

        /// <summary>
        /// 得到全部药品厂商列表 
        /// </summary>
        public string GetDrugFirmList
        {
            get
            {
                return "SELECT * FROM MED_DRUG_FIRM";
            }
        }

        public string GetDrugFirmListByFirmType
        {
            get
            {
                return "SELECT * FROM MED_DRUG_FIRM WHERE FIRM_TYPE = :FIRM_TYPE";
            }
        }

        /// <summary>
        /// 根据查询条件，得到药厂列表SQL
        /// </summary>
        public string GetDrugFirmListByParams
        {
            get
            {
                return
                    @"
                    SELECT * FROM MED_DRUG_FIRM WHERE 
                    FIRM_ID LIKE '%'||:FIRM_ID||'%' 
                    and (FIRM_NAME LIKE '%'||:FIRM_NAME||'%' OR FIRM_PINYIN LIKE '%'||:FIRM_PINYIN||'%') 
                    and FIRM_ADDRESS LIKE '%'||:FIRM_ADDRESS||'%'
                    and TELEPHONE  LIKE '%'||:TELEPHONE||'%' 
                    and MOBILE_PHONE LIKE '%'||:MOBILE_PHONE||'%' 
                    and FIRM_TYPE = :FIRM_TYPE 
                    ";
            }
        }

        /// <summary>
        /// 根据药品厂商编号得到数据
        /// </summary>
        public string GetDrugFrimListByFirmID
        {
            get
            {
                return "SELECT * FROM MED_DRUG_FIRM WHERE TRIM(FIRM_ID) = :FIRM_ID";
            }
        }
        /// <summary>
        /// 删除药厂
        /// </summary>
        public string DeleteDrugFirmInfo
        {
            get
            {
                return @"DELETE MED_DRUG_FIRM
                             WHERE FIRM_ID = :FIRM_ID";
            }
        }
        /// <summary>
        /// 得到新生成的药品厂商编号
        /// </summary>
        /// <returns></returns>
        public string GetNewFirmID
        {
            get
            {
                return "SELECT MAX(FIRM_ID)+1 AS FIRM_ID FROM MED_DRUG_FIRM";
            }
        }
        #endregion

        #region 医生资料设定相关SQL

        /// <summary>
        /// 获取医护人员资料设定数据SQL 
        /// </summary>
        public string GetStaffDictList
        {
            get
            {
                return @"
                    SELECT sd.*,CASE SD.IS_DELETE WHEN '0' THEN '启用' WHEN '1' THEN '停用'
                    END AS DELETE_STATUS, d.DEPT_NAME, zy.ITEM_NAME ZYNAME, zc.ITEM_NAME ZCNAME, u.USER_NAME USERNAMESTR FROM 
                    MED_STAFF_DICT sd
                      INNER JOIN MED_DEPARTMENT d ON sd.DEPT_CODE = d.DEPT_ID
                      INNER JOIN (SELECT * FROM MED_COMMON_ITEMLIST il WHERE il.ITEM_TYPE = '职业' AND il.STATUS = '1') zy ON sd.JOB = zy.ITEM_ID  
                      INNER JOIN (SELECT * FROM MED_COMMON_ITEMLIST il WHERE il.ITEM_TYPE = '职称' AND il.STATUS = '1') zc ON sd.TITLE = zc.ITEM_ID    
                      INNER JOIN MED_USERS u ON sd.USER_NAME = u.USER_ID  WHERE (SD.IS_DELETE != '1' OR SD.IS_DELETE IS NULL)
                      ORDER BY sd.EMP_NO";
            }
        }

        /// <summary>
        /// 根据组长标识获取护士组长或组员
        /// </summary>
        public string GetStaffDictByLeaderFlag
        {
            get
            {
                return @"SELECT T.* FROM MED_STAFF_DICT T
                        INNER JOIN (SELECT * FROM MED_COMMON_ITEMLIST L WHERE L.ITEM_TYPE='职业' AND L.ITEM_NAME='护士' AND L.STATUS='1') L ON T.JOB=L.ITEM_ID
                        WHERE T.IS_LEADER=:IS_LEADER AND (T.NURSE_LEADER IS NOT NULL OR (T.IS_LEADER='1' AND T.NURSE_LEADER IS NULL)) AND (T.IS_DELETE !='1' OR T.IS_DELETE IS NULL) ORDER BY T.NAME";
            }
        }

        /// <summary>
        /// 根据护士组长获取护士组员
        /// </summary>
        public string GetStaffDictByNurseLeader
        {
            get
            {
                return @"SELECT T.* FROM MED_STAFF_DICT T
                        INNER JOIN (SELECT * FROM MED_COMMON_ITEMLIST L WHERE L.ITEM_TYPE='职业' AND L.ITEM_NAME='护士' AND L.STATUS='1') L ON T.JOB=L.ITEM_ID
                        WHERE T.NURSE_LEADER=:NURSE_LEADER AND (T.IS_DELETE !='1' OR T.IS_DELETE IS NULL) ORDER BY T.NAME";
            }
        }

        public string GetAllStaffDictByNurseLeader
        {
            get
            {
                return @"SELECT T.* FROM (SELECT * FROM MED_STAFF_DICT T WHERE T.NURSE_LEADER=:NURSE_LEADER
                        UNION SELECT * FROM MED_STAFF_DICT T WHERE T.NURSE_LEADER IN
                        (SELECT T.EMP_NO FROM MED_STAFF_DICT T WHERE T.NURSE_LEADER=:NURSE_LEADER)) T
                        INNER JOIN (SELECT * FROM MED_COMMON_ITEMLIST L WHERE L.ITEM_TYPE='职业' AND L.ITEM_NAME='护士' AND L.STATUS='1') L ON T.JOB=L.ITEM_ID
                        WHERE T.IS_DELETE !='1' OR T.IS_DELETE IS NULL ORDER BY T.NAME";
            }
        }

        public string GetAllStaffDictList
        {
            get
            {
                return @"
                    SELECT sd.*,CASE SD.IS_DELETE WHEN '0' THEN '启用' WHEN '1' THEN '停用'
                    END AS DELETE_STATUS, d.DEPT_NAME, zy.ITEM_NAME ZYNAME, zc.ITEM_NAME ZCNAME, u.USER_NAME USERNAMESTR FROM 
                    MED_STAFF_DICT sd
                      INNER JOIN MED_DEPARTMENT d ON sd.DEPT_CODE = d.DEPT_ID
                      INNER JOIN (SELECT * FROM MED_COMMON_ITEMLIST il WHERE il.ITEM_TYPE = '职业' AND il.STATUS = '1') zy ON sd.JOB = zy.ITEM_ID  
                      INNER JOIN (SELECT * FROM MED_COMMON_ITEMLIST il WHERE il.ITEM_TYPE = '职称' AND il.STATUS = '1') zc ON sd.TITLE = zc.ITEM_ID    
                      INNER JOIN MED_USERS u ON sd.USER_NAME = u.USER_ID
                      ORDER BY sd.EMP_NO";
            }
        }

        /// <summary>
        /// 获取最新的人员编号SQL
        /// </summary>
        public string GetNewEMPNO
        {
            get
            {
                return "SELECT MAX(EMP_NO) + 1 AS NewEMPNO FROM MED_STAFF_DICT";
            }
        }

        #endregion

        #region 科室相关SQL

        /// <summary>
        /// 获取科室数据SQL 
        /// </summary>
        public string GetDeptList
        {
            get
            {
                return "SELECT * FROM MED_DEPARTMENT WHERE STATUS = '1' ORDER BY DEPT_NAME";
            }
        }

        #endregion

        #region 用户相关SQL

        public string GetUserSkinDt
        {
            get
            {
                return @"SELECT * FROM MED_USERS_SKIN";
            }
        }


        /// <summary>
        /// 获取用户数据SQL 
        /// </summary>
        public string GetUserList
        {
            get
            {
                return "SELECT * FROM MED_USERS WHERE IS_VALID = 'T' ORDER BY USER_NAME";
            }
        }

        /// <summary>
        /// 验证登录用户SQL 
        /// </summary>
        public string GetUserLogin
        {
            get
            {
                return @"SELECT t.*,d.emp_no FROM MED_USERS t
                left join med_staff_dict d on d.user_name = t.user_id
                WHERE UPPER(t.LOGIN_NAME) = :USER_NAME AND UPPER(t.LOGIN_PWD) = :USER_PWD AND  t.IS_VALID = 'T'  ";
            }
        }

        /// <summary>
        /// 获取权限数据SQL 
        /// </summary>
        public string GetPermissionListByUserID
        {
            get
            {
                return @"
                    SELECT 
                      ur.USER_ID, rp.ROLE_ID, p.PERMISSION_ID, p.NAME PERMISSIONNAME
                    FROM 
                    MED_USERS_ROLES ur
                      INNER JOIN MED_ROLES_PERMISSIONS rp ON ur.ROLE_ID = rp.ROLE_ID
                      INNER JOIN MED_PERMISSIONS p ON rp.PERMISSION_ID = p.PERMISSION_ID  
                    WHERE
                      ur.USER_ID = :USER_ID
                    ORDER BY
                      p.SORT_ID";
            }
        }

        /// <summary>
        /// 获取用户和区域关系映射数据SQL
        /// </summary>
        public string GetUserAreaMappingList
        {
            get
            {
                return "SELECT * FROM MED_USERAREA_MAPPING WHERE USER_ID = :USER_ID";
            }
        }

        /// <summary>
        /// 删除用户和区域关系映射数据SQL
        /// </summary>
        public string DeleteUserAreaMappingInfo
        {
            get
            {
                return "DELETE FROM MED_USERAREA_MAPPING WHERE USER_ID = :USER_ID";
            }
        }

        /// <summary>
        /// 获取用户有权限访问的区域数据SQL
        /// </summary>
        public string GetAreaList
        {
            get
            {
                return @"
                    SELECT 
                      * 
                    FROM MED_COMMON_ITEMLIST c 
                    WHERE 
                      c.ITEM_TYPE = '区域' 
                      AND c.STATUS = '1'
                      AND EXISTS
                      (
                        SELECT 
                          1 
                        FROM MED_USERAREA_MAPPING m 
                        WHERE 
                          c.ITEM_ID = m.AREA_ID 
                          AND m.USER_ID = :USER_ID
                      )";
            }
        }

        public string GetPatientRecordByHemoIDandDate
        {
            get
            {
                return "SELECT * FROM  MED_PATIENTRECORD T WHERE T.HEMODIALYSIS_ID = :HEMODIALYSIS_ID AND trunc(T.CREATEDATE) =trunc(:CREATEDATE)";
            }
        }

        public string QueryPatientRecordByParams
        {
            get
            {
                return @"SELECT S.NAME,
                                S.SEX,
                                   S.AGE,
                                   T.ID,
                                   T.HEMODIALYSIS_ID,
                                   T.ACTION,
                                   T.PRESENTILLNESS,
                                   T.PASTILLNESS,
                                   (SELECT S.USER_NAME FROM MED_USERS S WHERE S.USER_ID = T.CREATEBY) AS CREATEBY,
                                   T.CREATEDATE,
                                   (SELECT S.USER_NAME FROM MED_USERS S WHERE S.USER_ID = T.LASTUPDATEBY) AS LASTUPDATEBY,
                                   T.LASTUPDATEDATE
                         FROM MED_PATIENTRECORD T, MED_PATIENTS S
                         WHERE T.hemodialysis_id = s.hemodialysis_id
                         AND (T.hemodialysis_id LIKE '%'||:hemodialysis_id||'%' OR S.NAME like '%'||:hemodialysis_id||'%' OR upper(S.INPUT_CODE) LIKE upper('%'||:hemodialysis_id||'%'))
                         AND T.CREATEDATE >= :begintime
                         AND T.CREATEDATE <= :endtime";
            }
        }

        public string GetPatientRecordByParams
        {
            get
            {
                return @"SELECT T.ID,
                                T.HEMODIALYSIS_ID,
                                T.CONTENT,
                                (SELECT S.USER_NAME FROM MED_USERS S WHERE S.USER_ID = T.CREATEBY) AS CREATEBY,
                                T.CREATEDATE,
                                (SELECT S.USER_NAME FROM MED_USERS S WHERE S.USER_ID = T.LASTUPDATEBY) AS LASTUPDATEBY,
                                T.LASTUPDATEDATE,
                                T.WEIGHT,
                                T.ACTION,
                                T.PRESENTILLNESS,
                                T.SUGAR_DIABETES,
                                T.HIGH_BLOOD_PRESSURE,
                                T.CHRONIC_NEPHRITIS,
                                T.POLYCYSTIC_KIDNEY,
                                T.URINARY_CALCULUS,
                                T.OTHER_PROTOPATHY,
                                T.DRUG_ALLERGY_HISTORY,
                                T.T,
                                T.P,
                                T.R,
                                T.BP,
                                T.CONSCIOUSNESS,
                                T.BODY_POSITION,
                                T.CHECK_UP,
                                T.FULL_BODY_SKIN_1,
                                T.SICKLY_LOOK,
                                decode(T.SICKLY_LOOK,'1','急性','慢性') as SICKLY_LOOKNAME,
                                T.ANEMIA,
                                T.EDEMA,
                                T.CARDIACDULLNESS,
                                T.HEART_RATE,
                                T.NOISE,
                                T.DOUBLE_LUNG_AUSCULTATION,
                                T.ASCITES,
                                T.OTHER_PAST_HISTORY,
                                T.RABAT,
                                T.RENAL_SIZE,
                                T.STRUCTURE,
                                T.OTHER_B_ULTRASONIC,
                                T.RBC,
                                T.WBC,
                                T.HGB,
                                T.PLT,
                                T.HCT,
                                T.BUN,
                                T.SCR,
                                T.UA,
                                T.K,
                                T.CA,
                                T.PH,
                                T.IMX,
                                T.HCV,
                                T.TPPA,
                                T.AIDS,
                                decode(T.IMX,'1','阳性','阴性') as IMXNAME,
                                decode(T.HCV,'1','阳性','阴性') as HCVNAME,
                                decode(T.TPPA,'1','阳性','阴性') as TPPANAME,
                                decode(T.AIDS,'1','阳性','阴性') as AIDSNAME,
                                T.OTHER_INFECTION,
                                T.CLOTTING_MECHANISM,
                                T.PT,
                                T.APTT,
                                T.INR,
                                T.SERUM_IRON,
                                T.TIBC,
                                T.FERROPROTEIN,
                                T.PTH,
                                T.DRUG_APPLICATION,
                                T.DIAGNOSIS,
                                T.TREATMENT_PLAN,
                                T.DRUG_USE,
                                T.FULL_BODY_SKIN_2,
                                S.NAME,
                                S.SEX,
                                S.AGE,
                                S.CREDENTIALS_NUMBER,
                                S.ADDRESS,
                                S.TELEPHONE
                         FROM MED_PATIENTRECORD T, MED_PATIENTS S
                         WHERE T.hemodialysis_id = s.hemodialysis_id
                         AND (T.hemodialysis_id LIKE '%'||:HEMODIALYSIS_ID||'%' OR S.NAME like '%'||:HEMODIALYSIS_ID||'%' OR upper(S.INPUT_CODE) LIKE upper('%'||:HEMODIALYSIS_ID||'%'))
                         AND T.CREATEDATE >= :BEGINTIME
                         AND T.CREATEDATE <= :ENDTIME";
            }
        }
        public string DeleteMaterialRecordByID
        {
            get
            {
                return @"UPDATE MED_PATIENT_MATERIAL SET ISDELETE='1' WHERE RECIPEID=:RECIPEID";
            }
        }
        public string QueryModelByParams
        {
            get
            {
                return @"SELECT DISTINCT MATERIAL_MODEL_NAME FROM MED_MATERIAL_MODEL";
            }
        }

        public string GetHemoDefaultModels
        {
            get
            {
                return @"SELECT *
                              FROM MED_HEMO_CURE_DEFAULT_MODE T
                             WHERE T.RELATIONID =:RELATIONID
                             ORDER BY T.VISIBLE_INDEX ASC";
            }
        }

        public string DeleteHemoDefaultModelById
        {
            get
            {
                return @"DELETE FROM MED_HEMO_CURE_DEFAULT_MODE WHERE ID=:ID";
            }
        }

        public string QueryMaterialPatientDataByParam
        {
            get
            {
                return @"SELECT T.MATERIAL_ID,
                                   T.MATERIAL_NAME,
                                   Z.MATERIAL_SPEC,
                                   Z.SUPPLIER,
                                   Z.FIRM_NAME,
                                   SUM(T.MATERIAL_NUMBER) COUNT,
                                   SUM(T.PRICE) PRICE,
                                   T.ITEMTYPE,
                                   Z.MATERIAL_TYPE
                              FROM MED_PATIENT_MATERIAL T
                              LEFT JOIN (SELECT MATERIAL_ID,
                                                MATERIAL_NAME,
                                                MATERIAL_SPEC,
                                                '耗材' SUPPLIER,
                                                FIRM_NAME,
                                                MATERIAL_PINYIN,
                                                'MATERIAL' AS MATERIAL_TYPE
                                           FROM MED_MATERIAL_MASTER
                                         UNION
                                         SELECT DRUG_CODE AS MATERIAL_ID,
                                                DRUG_NAME AS MATERIAL_NAME,
                                                DRUG_SPEC AS MATERIAL_SPEC,
                                                (SELECT L.ITEM_NAME
                                                   FROM MED_COMMON_ITEMLIST L
                                                  WHERE L.ITEM_ID = DRUG_TYPE) AS SUPPLIER,
                                                FIRM_NAME AS FIRM_NAME,
                                                DRUG_PINYIN AS MATERIAL_PINYIN,
                                                'OTHER' AS MATERIAL_TYPE
                                           FROM MED_DRUG_MASTER) Z
                                ON T.MATERIAL_ID = Z.MATERIAL_ID
                             WHERE TRUNC(T.LASTUPDATEDATE) >= :StartTime
                               AND TRUNC(T.LASTUPDATEDATE) <= :EndTime
                               AND T.STATE = '1'
                               AND T.ISDELETE = '0'
                             GROUP BY T.MATERIAL_ID,
                                      T.MATERIAL_NAME,
                                      T.ITEMTYPE,
                                      Z.MATERIAL_SPEC,
                                      Z.SUPPLIER,
                                      Z.MATERIAL_TYPE,
                                      Z.FIRM_NAME
                             ORDER BY Z.MATERIAL_TYPE, T.ITEMTYPE";
            }
        }

        public string QueryMaterialPatientDetailByparam
        {
            get
            {
                return @"SELECT T.HEMODIALYSIS_ID,
                                   P.NAME,
                                   T.LASTUPDATEBY,
                                   D.NAME,
                                   T.LASTUPDATEDATE,
                                   T.MATERIAL_NAME,
                                   T.MATERIAL_NUMBER,
                                   T.PRICE
                              FROM MED_PATIENT_MATERIAL T
                              LEFT JOIN MED_PATIENTS P
                                ON T.HEMODIALYSIS_ID = P.HEMODIALYSIS_ID
                              LEFT JOIN MED_STAFF_DICT D
                                ON T.LASTUPDATEBY = D.EMP_NO
                             WHERE T.MATERIAL_ID = :MATERIAL_ID AND T.STATE = '1'
                               AND TRUNC(T.LASTUPDATEDATE) >= :StartTime
                               AND TRUNC(T.LASTUPDATEDATE) <= :EndTime";
            }
        }

        public string QueryPatientMaterialDataByParam
        {
            get
            {
                return @"SELECT T.HEMODIALYSIS_ID, P.NAME, P.SEX,  P.BIRTHDAY, P.DIAGNOSE
                              FROM MED_PATIENT_MATERIAL T
                              LEFT JOIN MED_PATIENTS P
                                ON T.HEMODIALYSIS_ID = P.HEMODIALYSIS_ID
                             WHERE TRUNC(T.LASTUPDATEDATE) >= :StartTime
                               AND TRUNC(T.LASTUPDATEDATE) <= :EndTime
                               AND T.STATE = '1'
                               AND T.ISDELETE = '0'
                             GROUP BY T.HEMODIALYSIS_ID, P.NAME, P.SEX, P.SEX, P.BIRTHDAY, P.DIAGNOSE";
            }
        }

        public string QueryPatientMaterialDetailByparam
        {
            get
            {
                return @"SELECT P.HEMODIALYSIS_ID,
                                   P.NAME,
                                   T.MATERIAL_ID,
                                   T.MATERIAL_NAME,
                                   Z.MATERIAL_SPEC,
                                   Z.FIRM_NAME,
                                   SUM(T.MATERIAL_NUMBER) COUNT,
                                   SUM(T.PRICE) PRICE,
                                   T.ITEMTYPE,
                                   Z.MATERIAL_TYPE,
                                   TRUNC(T.LASTUPDATEDATE) LASTUPDATEDATE
                              FROM MED_PATIENT_MATERIAL T
                              LEFT JOIN MED_PATIENTS P
                                ON T.HEMODIALYSIS_ID = P.HEMODIALYSIS_ID
                              LEFT JOIN (SELECT MATERIAL_ID,
                                                MATERIAL_NAME,
                                                MATERIAL_SPEC,
                                                '耗材' SUPPLIER,
                                                FIRM_NAME,
                                                MATERIAL_PINYIN,
                                                'MATERIAL' AS MATERIAL_TYPE
                                           FROM MED_MATERIAL_MASTER
                                         UNION
                                         SELECT DRUG_CODE AS MATERIAL_ID,
                                                DRUG_NAME AS MATERIAL_NAME,
                                                DRUG_SPEC AS MATERIAL_SPEC,
                                                (SELECT L.ITEM_NAME
                                                   FROM MED_COMMON_ITEMLIST L
                                                  WHERE L.ITEM_ID = DRUG_TYPE) AS SUPPLIER,
                                                FIRM_NAME AS FIRM_NAME,
                                                DRUG_PINYIN AS MATERIAL_PINYIN,
                                                'OTHER' AS MATERIAL_TYPE
                                           FROM MED_DRUG_MASTER) Z
                                ON T.MATERIAL_ID = Z.MATERIAL_ID
                             WHERE TRUNC(T.LASTUPDATEDATE) >= :StartTime
                               AND TRUNC(T.LASTUPDATEDATE) <= :EndTime
                               AND T.HEMODIALYSIS_ID=:HEMODIALYSIS_ID
                               AND T.STATE = '1'
                               AND T.ISDELETE = '0'
                             GROUP BY P.HEMODIALYSIS_ID,
                                      P.NAME,
                                      T.MATERIAL_ID,
                                      T.MATERIAL_NAME,
                                      T.ITEMTYPE,
                                      Z.MATERIAL_SPEC,
                                      Z.MATERIAL_TYPE,
                                      Z.FIRM_NAME,TRUNC(T.LASTUPDATEDATE)
                             ORDER BY Z.MATERIAL_TYPE, T.ITEMTYPE";
            }
        }

        public string QueryMaterialDetailByParams
        {
            get
            {
                return @"SELECT *
                          FROM MED_PATIENT_MATERIAL
                         WHERE RECIPEID = :RECIPEID AND ISDELETE='0'";
            }
        }

        public string QueryMaterialOutByParams
        {
            get
            {
                return @"SELECT T.*,
                               R.MATERIAL_SPEC MATERIALSPECE,
                               R.FIRM_NAME     FRIMNAME,
                               S.ITEM_NAME     MATERIALUNIT,
                               R.MATERIAL_TYPE TYPE
                          FROM MED_PATIENT_MATERIAL T
                          LEFT JOIN MED_MATERIAL_MASTER R
                            ON T.MATERIAL_ID = R.MATERIAL_ID
                          LEFT JOIN MED_COMMON_ITEMLIST S
                            ON R.UNIT = S.ITEM_ID
                         WHERE T.RECIPEID = :RECIPEID
                           AND T.ISDELETE = '0'
                          ";
            }
        }
        public string GetMaterialAll
        {
            get
            {
                return @"SELECT MATERIAL_ID,
                               MATERIAL_NAME,
                               MATERIAL_SPEC,
                               '耗材' SUPPLIER,
                               FIRM_NAME,
                               MATERIAL_PINYIN,
                               'MATERIAL' AS MATERIAL_TYPE,
                               PRICE MATERIAL_PRICE
                          FROM MED_MATERIAL_MASTER
                        UNION
                        SELECT DRUG_CODE AS MATERIAL_ID,
                               DRUG_NAME AS MATERIAL_NAME,
                               DRUG_SPEC AS MATERIAL_SPEC,
                               (SELECT L.ITEM_NAME FROM MED_COMMON_ITEMLIST L WHERE L.ITEM_ID= DRUG_TYPE)   AS SUPPLIER,       
                               FIRM_NAME AS FIRM_NAME,
                               DRUG_PINYIN AS MATERIAL_PINYIN,
                               'OTHER' AS MATERIAL_TYPE,
                               PRICE MATERIAL_PRICE
                          FROM MED_DRUG_MASTER";
            }
        }
        public string DeleteMaterialRecordByName
        {
            get
            {
                return @"DELETE FROM MED_PATIENT_MATERIAL WHERE RECORDID=:RECORDID";
            }
        }
        public string QueryMaterialModelByParams
        {
            get
            {
                return @"SELECT MATERIAL_MODEL_ID,
                                   MATERIAL_ID,
                                   MATERIAL_NAME,
                                   MATERIAL_NUMBER,
                                   ITEMTYPE,
                                   MATERTYPE,
                                   MATERIAL_SPEC,
                                   FIRM_NAME,
                                   T.MATERIAL_NUMBER * Z.PRICE AS PRICE
                              FROM MED_MATERIAL_MODEL T
                              LEFT JOIN (SELECT T.MATERIAL_ID AS ITEMCODE, T.PRICE
                                           FROM MED_MATERIAL_MASTER T
                                         UNION
                                         SELECT R.DRUG_CODE AS ITEMCODE, R.PRICE FROM MED_DRUG_MASTER R) Z
                                ON T.MATERIAL_ID = Z.ITEMCODE
                             WHERE MATERIAL_MODEL_NAME = :MATERIAL_MODEL_NAME ";
            }
        }
        public string DeleteMaterialModelByName
        {
            get
            {
                return @"DELETE FROM MED_MATERIAL_MODEL WHERE MATERIAL_MODEL_NAME=:MATERIAL_MODEL_NAME";
            }
        }
        public string GetRecordMouldList
        {
            get
            {
                return "SELECT * FROM MED_RECORDMOULD ";
            }
        }

        public string SaveMaterialRecordOut
        {
            get
            {
                return @"INSERT INTO MED_MATERIAL_OUTPUT
                          (ID, CODE, OPERATOR_ID, OUPUT_DATE, PRICE, ISOUT, F_COUNT, HEMO_ID, REMARK, FIRM_ID, STATUS,MATERIAL_NAME,APPLYID,SPACE,UNITS,
                           MODETYPE,BATCH_NUMBER,STOREMANAGER)
                        VALUES
        ('{0}','{1}','{2}',TO_DATE('{3}','yyyy/mm/dd hh24:mi:ss'),'{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}','{12}','{13}','{14}','{15}','{16}','{17}')";
            }
        }

        public string GetMaterialOutPutByRecipeId
        {
            get
            {
                return @"SELECT * FROM MED_MATERIAL_OUTPUT T WHERE T.BATCH_NUMBER=:RECIPEID";
            }
        }

        #endregion

        #region 耗材维护相关SQL
        /// <summary>
        /// 得到耗材数量
        /// </summary>
        public string GetMaterialMasterCount
        {
            get
            {
                return "SELECT COUNT(*) FROM MED_MATERIAL_MASTER";
            }
        }

        /// <summary>
        /// 得到耗材资料列表 
        /// </summary>
        public string GetMaterialMasterList
        {
            get
            {
                return "SELECT * FROM MED_MATERIAL_MASTER ORDER BY CREATE_DATE DESC";
            }
        }

        /// <summary>
        /// 根据查询条件，得到耗材资料列表SQL
        /// </summary>
        public string GetMaterialMasterListByParams
        {
            get
            {
                return
                    @"
                    SELECT
                        t.*
                    FROM MED_MATERIAL_MASTER t
                    WHERE 
                        t.MATERIAL_ID LIKE '%'||:MATERIAL_ID||'%' 
                        and (t.MATERIAL_NAME LIKE '%'||:MATERIAL_NAME||'%' OR t.MATERIAL_PINYIN LIKE '%'||:MATERIAL_NAME||'%') 
                        and t.FIRM_NAME LIKE '%'||:FIRM_NAME||'%' 
                        ORDER BY 
                        t.CREATE_DATE DESC";
            }
        }

        /// <summary>
        /// 根据耗材编号得到数据
        /// </summary>
        public string GetMaterialMasterListByMaterialID
        {
            get
            {
                return @"SELECT *
                          FROM (SELECT MATERIAL_ID,
                                       MATERIAL_NAME,
                                       MATERIAL_SPEC,
                                       '耗材' SUPPLIER,
                                       FIRM_NAME,
                                       MATERIAL_PINYIN,
                                       'MATERIAL' AS TYPE,
                                       PRICE
                                  FROM MED_MATERIAL_MASTER
                                UNION
                                SELECT DRUG_CODE AS MATERIAL_ID,
                                       DRUG_NAME AS MATERIAL_NAME,
                                       DRUG_SPEC AS MATERIAL_SPEC,
                                       (SELECT L.ITEM_NAME
                                          FROM MED_COMMON_ITEMLIST L
                                         WHERE L.ITEM_ID = DRUG_TYPE) AS SUPPLIER,
                                       FIRM_NAME AS FIRM_NAME,
                                       DRUG_PINYIN AS MATERIAL_PINYIN,
                                       'OTHER' AS TYPE,
                                       PRICE
                                  FROM MED_DRUG_MASTER)
                         WHERE MATERIAL_ID = :MATERIAL_ID";
            }
        }

        /// <summary>
        /// 得到新生成的耗材编号
        /// </summary>
        /// <returns></returns>
        public string GetNewMaterialID
        {
            get
            {
                return "SELECT MAX(MATERIAL_ID)+1 AS MATERIAL_ID FROM MED_MATERIAL_MASTER";
            }
        }

        /// <summary>
        /// 根据耗材领用编号得到耗材保镖数据
        /// </summary>
        public string GetMaterialReport
        {
            get
            {
                return @"
                select m_report.* from (
                select '透析器' as caption,machine_type as type_name,1 as all_count,use_material_id, hemodialysis_id, recipe_id from med_hemo_material
                union all
                select '血路管' as caption,blood_filter as type_name,blood_filter_count as all_count ,use_material_id, hemodialysis_id, recipe_id from med_hemo_material
                union all 
                select '穿刺针' as caption,puncture_needle as type_name,puncture_needle_count as all_count,use_material_id, hemodialysis_id, recipe_id from med_hemo_material
                union all 
                select '透析机A液' as caption,a_powder as type_name,a_powder_count as all_count,use_material_id, hemodialysis_id, recipe_id from med_hemo_material
                union all 
                select '透析机B液' as caption,b_powder as type_name,b_powder_count as all_count,use_material_id, hemodialysis_id, recipe_id from med_hemo_material
                union all 
                select '一次性使用无菌注射器' as caption,injector_2ml as type_name,injector_2ml_count as all_count,use_material_id, hemodialysis_id, recipe_id from med_hemo_material
                union all 
                select '一次性使用无菌注射器' as caption,injector_20ml as type_name,injector_20ml_count as all_count,use_material_id, hemodialysis_id, recipe_id from med_hemo_material
                union all 
                select '次氯酸钠' as caption, clorox as type_name,clorox_count as all_count,use_material_id, hemodialysis_id, recipe_id from med_hemo_material
                union all 
                select '柠檬酸' as caption,citric_acid as type_name,citric_acid_count as all_count,use_material_id, hemodialysis_id, recipe_id from med_hemo_material
                union all 
                select '肝素钠注射液' as caption,heparin_sodium as type_name,heparin_sodium_count as all_count,use_material_id, hemodialysis_id, recipe_id  from med_hemo_material
                union all 
                select '氯化钠注射液' as caption,sodium_chloride as type_name,sodium_chloride_count as all_count,use_material_id, hemodialysis_id, recipe_id from med_hemo_material
                union all 
                select '' as caption,rubber as type_name,rubber_count as all_count,use_material_id, hemodialysis_id, recipe_id from med_hemo_material
                union all 
                select '' as caption,gauze as type_name,gauze_count as all_count,use_material_id, hemodialysis_id,recipe_id from med_hemo_material
                union all 
                select '' as caption, tampon as type_name,tampon_count as all_count,use_material_id, hemodialysis_id, recipe_id from med_hemo_material
                union all 
                select '' as caption,draw_sheet as type_name,draw_sheet_count as all_count,use_material_id, hemodialysis_id, recipe_id from med_hemo_material
                union all 
                select '' as caption,protective_plaster as type_name,protective_count as all_count,use_material_id, hemodialysis_id, recipe_id from med_hemo_material
                )   m_report where use_material_id = :use_material_id
                ";
            }
        }

        /// <summary>
        /// 根据透析号得到耗材领用列表
        /// </summary>
        public string GetUseMaterialList
        {
            get
            {
                return @" SELECT * FROM MED_HEMO_MATERIAL WHERE HEMODIALYSIS_ID=:HEMODIALYSIS_ID ORDER BY CREATE_DATE DESC";
            }
        }

        public string GetFollowUpByData
        {
            get
            {
                return @"SELECT t.*,decode(t.followtype,0,'电话','1','门诊','2','住院','3','家访') FOLLOWTYPENAME
                          FROM MED_PATIENT_FOLLOWUP T
                         WHERE T.FOLLOWDATE >= :BEGINDATE
                           AND T.FOLLOWDATE  <= :ENDDATE
                           AND T.HEMODIALYSIS_ID = :HEMODIALYSIS_ID";
            }
        }
        public string GetFollowUpByDateTime
        {
            get
            {
                return @"SELECT t.*,decode(t.followtype,0,'电话','1','门诊','2','住院','3','家访') FOLLOWTYPENAME
                          FROM MED_PATIENT_FOLLOWUP T
                         WHERE T.FOLLOWDATE >=:STARTTIME
                           AND T.FOLLOWDATE <=:ENDTIME";
            }
        }
        public string GetFollowUpByHemoID
        {
            get
            {
                return @"SELECT *
                          FROM MED_PATIENT_FOLLOWUP T
                         WHERE T.HEMODIALYSIS_ID = :HEMODIALYSIS_ID
                              AND TRUNC(T.FOLLOWDATE) = TRUNC(:FOLLOWDATE)";
            }
        }
        #endregion

        #region 血透机相关SQL

        public string GetMachineListByUserID
        {
            get
            {
                return @"SELECT * FROM (
                          SELECT DM.*, FL.ITEM_NAME FLNAME,QY.ITEM_VALUE QYVALUE, QY.ITEM_NAME QYNAME, CW.ITEM_NAME CWNAME, CW.ITEM_VALUE CWVALUE
                              FROM MED_DIALYSIS_MACHINE DM
                             INNER JOIN (SELECT *
                                           FROM MED_COMMON_ITEMLIST IL
                                          WHERE IL.ITEM_TYPE = '血透机品牌'
                                            AND IL.STATUS = '1') FL
                                ON DM.TYPE = FL.ITEM_ID
                             RIGHT JOIN (SELECT *
                                           FROM MED_COMMON_ITEMLIST IL
                                          WHERE IL.ITEM_TYPE = '区域'
                                            AND IL.STATUS = '1'
                                            AND EXISTS
                                          (SELECT 1
                                                   FROM MED_USERAREA_MAPPING M
                                                  WHERE IL.ITEM_ID = M.AREA_ID
                                                    AND M.USER_ID = :USER_ID)) QY
                                ON DM.AREA_ID = QY.ITEM_ID
                              LEFT JOIN (SELECT *
                                           FROM MED_COMMON_ITEMLIST IL
                                          WHERE IL.ITEM_TYPE = '床位'
                                            AND IL.STATUS = '1') CW
                                ON DM.BED_ID = CW.ITEM_ID
                             WHERE DM.AREA_ID IS NOT NULL
                             ORDER BY TO_NUMBER(DM.MACHINE_SERIAL_NO))Z WHERE Z.CWNAME IS NOT NULL";
            }
        }
        /// <summary>
        /// 获取血透机数据SQL 
        /// </summary>
        public string GetMachineList
        {
            get
            {
                return @"
                    SELECT 
                      dm.*, fl.ITEM_NAME FLNAME, qy.ITEM_NAME QYNAME, cw.ITEM_NAME CWNAME, TO_NUMBER(cw.ITEM_VALUE) CWNO
                    FROM 
                    MED_DIALYSIS_MACHINE dm
                      INNER JOIN (SELECT * FROM MED_COMMON_ITEMLIST il WHERE il.ITEM_TYPE = '血透机品牌' AND il.STATUS = '1') fl ON dm.TYPE = fl.ITEM_ID  
                      LEFT JOIN (SELECT * FROM MED_COMMON_ITEMLIST il WHERE il.ITEM_TYPE = '区域' AND il.STATUS = '1') qy ON dm.AREA_ID = qy.ITEM_ID   
                      LEFT JOIN (SELECT * FROM MED_COMMON_ITEMLIST il WHERE il.ITEM_TYPE = '床位' AND il.STATUS = '1') cw ON dm.BED_ID = cw.ITEM_ID  
            WHERE dm.area_id is not null  
                    ORDER BY to_number(dm.machine_serial_no)";
            }
        }

        /// <summary>
        /// 获取全部血透机数据SQL 
        /// </summary>
        public string GetNewMachineList
        {
            get
            {
                return @"
                    SELECT 
                      dm.*, fl.ITEM_NAME FLNAME, qy.ITEM_NAME QYNAME, cw.ITEM_NAME CWNAME
                    FROM 
                    MED_DIALYSIS_MACHINE dm
                      INNER JOIN (SELECT * FROM MED_COMMON_ITEMLIST il WHERE il.ITEM_TYPE = '血透机品牌' AND il.STATUS = '1') fl ON dm.TYPE = fl.ITEM_ID  
                      LEFT JOIN (SELECT * FROM MED_COMMON_ITEMLIST il WHERE il.ITEM_TYPE = '区域' AND il.STATUS = '1') qy ON dm.AREA_ID = qy.ITEM_ID   
                      LEFT JOIN (SELECT * FROM MED_COMMON_ITEMLIST il WHERE il.ITEM_TYPE = '床位' AND il.STATUS = '1') cw ON dm.BED_ID = cw.ITEM_ID  
                    ORDER BY to_number(dm.machine_serial_no)";
            }
        }

        /// <summary>
        /// 获取全部水处理机数据SQL 
        /// </summary>
        public string GetWaterProcessorList
        {
            get
            {
                return @"
                    SELECT 
                      dm.*, fl.ITEM_NAME FLNAME
                    FROM 
                    MED_DIALYSIS_MACHINE dm
                      INNER JOIN (SELECT * FROM MED_COMMON_ITEMLIST il WHERE il.ITEM_TYPE = '水处理机品牌' AND il.STATUS = '1') fl ON dm.TYPE = fl.ITEM_ID 
                    ORDER BY to_number(dm.machine_serial_no)";
            }
        }

        /// <summary>
        /// 获取对应数据采集的设备列表
        /// </summary>
        public string GetMachineListForDataGather
        {
            get
            {
                return @"
                        SELECT
                        dm.MACHINE_ID,
                        dm.MACHINE_NAME,
                        dm.TYPE,
                        dm.MACHINE_MODEL,
                        dm.MACHINE_MODEL||'-'||dm.MACHINE_NAME MACHINE_LABEL,
                        dm.AREA_ID,
                        dm.BED_ID,
                        fl.ITEM_NAME FLNAME,
                        qy.ITEM_NAME QYNAME,
                        cw.ITEM_NAME CWNAME,
                        DECODE(se.STATUS,'1','1','0') STATUS
                        FROM MED_DIALYSIS_MACHINE dm
                        INNER JOIN (SELECT * FROM MED_COMMON_ITEMLIST il WHERE il.ITEM_TYPE = '血透机品牌' AND il.STATUS = '1') fl ON dm.TYPE = fl.ITEM_ID  
                        LEFT JOIN (SELECT * FROM MED_COMMON_ITEMLIST il WHERE il.ITEM_TYPE = '区域' AND il.STATUS = '1') qy ON dm.AREA_ID = qy.ITEM_ID   
                        LEFT JOIN (SELECT * FROM MED_COMMON_ITEMLIST il WHERE il.ITEM_TYPE = '床位' AND il.STATUS = '1') cw ON dm.BED_ID = cw.ITEM_ID
                        LEFT JOIN (SELECT * FROM MED_HEMO_PARAMETERS_SETTING) se ON dm.MACHINE_ID=se.MACHINE_ID
                        ORDER BY to_number(dm.machine_serial_no)";
            }
        }

        public string GetUSEDATEByParms
        {
            get
            {
                return @"SELECT * FROM MED_MACHINE_USERECORD
                            WHERE MACHINE_ID = :MACHINE_ID
                              AND TRUNC(USEDATE) = TRUNC(:USEDATA)
                              AND T.ISDELETE='0'";
            }
        }

        public string GetUseAllDataByMachineID
        {
            get
            {
                return @"SELECT T.*,
                               TO_CHAR(T.USEDATE, 'DD') ||
                               DECODE(T.BANCI_ID, '1', '  早班', '2', '  中午', '  晚班') ROWTITLE,
                               (SELECT C.NAME FROM MED_STAFF_DICT C WHERE C.EMP_NO = T.OPERATION) NAME,
                               T.USERTIME || 'H' USERTIMENAME
                          FROM MED_MACHINE_USERECORD T
                            WHERE  T.DIALYSIS_ROOM_ID LIKE '%' || :DIALYSIS_ROOM_ID || '%'
                               AND T.BED_NUMBER LIKE '%' || :BED_NUMBER || '%'
                               AND T.BANCI_ID LIKE '%' || :BANCI_ID || '%'
                               AND TO_CHAR(T.USEDATE,'yyyy-mm') LIKE '%' || :USEDATA || '%' 
                               AND T.ISDELETE='0'
                          ORDER BY T.USEDATE, T.BANCI_ID";
            }
        }
        // public string GetUseDataBy
        public string GetUseAllDataByMachineIDAndData
        {
            get
            {
                return @"SELECT T.*,
                               TO_CHAR(T.USEDATE, 'DD') ||
                               DECODE(T.BANCI_ID, '1', '  早班', '2', '  中午', '  晚班') ROWTITLE,
                               (SELECT C.NAME FROM MED_STAFF_DICT C WHERE C.EMP_NO = T.OPERATION) NAME,
                               T.USERTIME || 'H' USERTIMENAME
                          FROM MED_MACHINE_USERECORD T
                         WHERE T.MACHINE_ID = :MACHINE_ID
                           AND  TO_CHAR(T.USEDATE, 'YYYY-MM')= TO_CHAR(:USEDATA,'YYYY-MM')
                           AND T.ISDELETE='0'
                         ORDER BY T.USEDATE, T.BANCI_ID";
            }
        }

        public string GetAllRepairData
        {
            get
            {
                return @"SELECT T.*,
                           (SELECT L.ITEM_NAME
                              FROM MED_COMMON_ITEMLIST L
                             WHERE L.ITEM_ID = T.AREAROOM) AS AREAROOMNAME
                      FROM MED_MACHINE_REPAIRSITUATION T
                     WHERE T.CREATETIME >= TRUNC(:beginData)
                       AND T.CREATETIME <= TRUNC(:endData)
                       AND T.ISDELETE = '0'";
            }
        }

        public string GetDataByDate
        {
            get
            {
                return @"SELECT * FROM MED_MACHINE_REPAIRSITUATION WHERE TRUNC(USETIME) = TRUNC(:USETIME)";
            }
        }

        public string GetReUsableData
        {
            get
            {
                return @"SELECT ID,
                                   HEMODIALYSIS_ID,
                                   MACHINETYPE,
                                   FIRSTUSETIME,
                                   REUSEDATETIME,
                                   REUSECOUNT,
                                   (SELECT NAME FROM MED_STAFF_DICT WHERE EMP_NO = PRIMARY_NURSE) AS PRIMARY_NURSE ,
                                   TCVCHECK,
                                   DIALYZERLAB,
                                   DISINFECTANTLD,
                                   DISINFECTANTCL,
                                   PREDIAPPEARANCE,
                                   BACKDIAPPEARANCE,
                                   DECODE(PROGRAMCHECK,'1',utl_raw.cast_to_varchar2('A1CC'),'') PROGRAMCHECK ,
                                   DECODE(PROGRAMCHECK,'0',utl_raw.cast_to_varchar2('A1CC'),'') PROGRAMCHECK2 ,
                                   DECODE(FLUXCHECK,'0',utl_raw.cast_to_varchar2('A1CC'),'') FLUXCHECK,
                                   DECODE(FLUXCHECK,'1',utl_raw.cast_to_varchar2('A1CC'),'') FLUXCHECK2,
                                   CREATEDATE,
                                   (SELECT T.ITEM_NAME FROM MED_COMMON_ITEMLIST T WHERE T.ITEM_ID = MACHINETYPE) MACHINETYPENAME
                          FROM MED_MACHINE_REUSABLE
                         WHERE HEMODIALYSIS_ID = :HEMODIALYSIS_ID
                           AND CREATEDATE >= :BEGINDATE
                           AND CREATEDATE <= :ENDDATE 
                           AND MACHINETYPE like '%'|| :MACHINETYPE ||'%'
                           AND ISDELETE= '0'
                           ORDER BY REUSEDATETIME";
            }
        }

        public string GetAirPurgeDataById
        {
            get
            {
                return @"SELECT T.* FROM MED_MACHINE_AIRPURGE T WHERE T.ID=:ID AND T.ISDELETE='0'";
            }
        }

        public string GetAirPurgeData
        {
            get
            {
                return @"SELECT T.*,(SELECT K.NAME FROM MED_STAFF_DICT K WHERE K.EMP_NO = PURGER) AS  PURGERNAME,

                               TO_CHAR(T.TRENDPURGETIME,'HH24:MI:SS') || '--' || TO_CHAR(T.TRENDPURGETIMEEND,'HH24:MI:SS') TRENDWIDTH,

                               (SELECT K.NAME FROM MED_STAFF_DICT K WHERE K.EMP_NO = TRENDPURGER) AS  TRENDPURGERNAME,

                               TO_CHAR(T.STATICPURGETIME,'HH24:MI:SS')  || '--' || TO_CHAR(T.STATICPURGETIMEEND,'HH24:MI:SS') STATICWIDTH
                               ,C.ITEM_NAME AS ROOM_NAME
                            FROM MED_MACHINE_AIRPURGE T LEFT JOIN MED_COMMON_ITEMLIST C ON T.ROOM_ID = C.ITEM_ID
                            WHERE TRUNC(T.PURGEDATE) >= TRUNC(:BEGINDATE)
                              AND TRUNC(T.PURGEDATE) <= TRUNC(:ENDDATE)
                              AND T.ROOM_ID=:ROOM_ID
                              AND T.ISDELETE='0'
                          ORDER BY PURGEDATE";
            }
        }

        /// <summary>
        /// 根据唯一ID获取血透机使用费用数据
        /// </summary>
        public string GetUSEFEEDataById
        {
            get
            {
                return @"SELECT * FROM MED_MACHINE_USEFEE WHERE id=:id";
            }
        }

        /// <summary>
        /// 获取血透机使用费用数据
        /// </summary>
        public string GetUSEFEEData
        {
            get
            {
                return @"SELECT A.ID,to_char(A.USEDATE,'yyyy') dateyear,to_char(A.USEDATE,'mm') datemonth,to_char(A.USEDATE,'dd') dateday,
A.VALUATION||'/'||A.ARMYMANTIME VALUATION_ARMYMANTIME,A.CHARGE||'/'||A.LOCATIONMANTIME CHARGE_LOCATIONMANTIME,A.USEHOUR,A.MACHINESTATE,
(SELECT K.EMP_NO FROM MED_STAFF_DICT K WHERE K.EMP_NO = A.MACHINEUSER) MACHINEUSER,(SELECT K.NAME FROM MED_STAFF_DICT K WHERE K.EMP_NO = A.MACHINEUSER) NAME,A.ISDELETE,A.DIALYSIS_ROOM_ID,A.BED_NUMBER,A.BANCI_ID 
FROM MED_MACHINE_USEFEE A WHERE A.USEDATE >= TRUNC(:beginData) AND A.USEDATE <= TRUNC(:endData) +1 AND A.ISDELETE='0' AND A.DIALYSIS_ROOM_ID=:DIALYSIS_ROOM_ID AND A.BED_NUMBER=:BED_NUMBER ORDER BY A.USEDATE ";
            }
        }

        /// <summary>
        /// 获取血透机使用费用统计数据
        /// </summary>
        public string GetUSEFEEStatisticsData
        {
            get
            {
                return @"SELECT SUM(T.VALUATION)||'/'||SUM(T.ARMYMANTIME) ARMYTOTAL,
SUM(T.CHARGE)||'/'||SUM(T.LOCATIONMANTIME) LOCATIONTOTAL,
(NVL(SUM(T.VALUATION),0)+NVL(SUM(T.CHARGE),0))||'元/'||(NVL(SUM(T.ARMYMANTIME),0)+NVL(SUM(T.LOCATIONMANTIME),0))||'人次' TOTAL,
ROUND((NVL(SUM(T.USEHOUR),0)/(NVL(SUM(T.ARMYMANTIME),0)+NVL(SUM(T.LOCATIONMANTIME),0))),1) AVGHOUR,
ROUND(((NVL(SUM(T.VALUATION),0)+NVL(SUM(T.CHARGE),0))/(NVL(SUM(T.ARMYMANTIME),0)+NVL(SUM(T.LOCATIONMANTIME),0))),2) PRICEPERMAN,
(NVL(SUM(T.VALUATION),0)+NVL(SUM(T.CHARGE),0)) TOTALPRICE
FROM MED_MACHINE_USEFEE T WHERE T.ISDELETE='0' AND T.USEDATE >= TRUNC(:beginData) AND T.USEDATE <= TRUNC(:endData)";
            }
        }

        /// <summary>
        /// 获取医疗设备主机信息
        /// </summary>
        public string GetMainframeData
        {
            get
            {
                return @"SELECT A.ID,A.CHINESENAME,A.ENGLISHNAME,A.TYPE,A.MANUFACTURER,A.CONTRACT,A.FACTORYDATE,
A.ARRIVALDATE,A.STARTDATE,A.AGELIMIT,A.USEQUOTA,A.PRICEBYRMB,A.PRICEBYDOLLOR,A.APPROPRIATION,A.SELFFINANCING,A.USEADDRESS,
A.ISDELETE,(SELECT K.NAME FROM MED_STAFF_DICT K WHERE K.EMP_NO = A.SUBMITTER) SUBMITTER,
(SELECT K.NAME FROM MED_STAFF_DICT K WHERE K.EMP_NO = A.RECEIVER) RECEIVER,A.SUBMITDATE FROM MED_MACHINE_MAINFRAME A WHERE A.ISDELETE='0'";
            }
        }

        /// <summary>
        /// 获取医疗设备主机附属设备信息
        /// </summary>
        public string GetAccessoryEquipData
        {
            get
            {
                return @"SELECT * FROM MED_MACHINE_ACCESSORYEQUIP WHERE MAINFRAMEID=:mainframeId";
            }
        }

        public string GetMixDataByParms
        {
            get
            {
                return @"SELECT T.*,
                             (SELECT C.NAME FROM MED_STAFF_DICT C WHERE C.EMP_NO = T.SIGN) AS SIGNER
                          FROM MED_MACHINE_MIXBARREL T
                         WHERE T.DISINFECTDATE > :BEGINDATE
                           AND T.DISINFECTDATE < :ENDDATE
                           AND T.ISDELETE ='0' AND T.TYPE='0'";
            }
        }

        public string GetWaterHemoDataByParms
        {
            get
            {
                return @"SELECT T.*,
                         (SELECT C.NAME FROM MED_STAFF_DICT C WHERE C.EMP_NO = T.SIGN) AS SIGNER,
                        (SELECT L.ITEM_NAME FROM MED_COMMON_ITEMLIST L WHERE L.ITEM_ID = T.MACHINE AND L.ITEM_TYPE='消毒机器') AS MACHINER
                          FROM MED_MACHINE_MIXBARREL T
                         WHERE DISINFECTDATE > :BEGINDATE
                           AND DISINFECTDATE < :ENDDATE
                           AND ISDELETE = '0'
                           AND TYPE = '1'
                           AND MACHINE LIKE '%' || :MACHINE || '%'";
            }
        }

        public string GetWaterTreatmentDataByParms
        {
            get
            {
                return @"SELECT  T.*,
                         (SELECT C.NAME FROM MED_STAFF_DICT C WHERE C.EMP_NO = T.SIGN) AS SIGNER,
                        (SELECT L.ITEM_NAME FROM MED_COMMON_ITEMLIST L WHERE L.ITEM_ID = T.MACHINE AND L.ITEM_TYPE='消毒机器') AS MACHINER
                          FROM MED_MACHINE_MIXBARREL T
                         WHERE DISINFECTDATE > :BEGINDATE
                           AND DISINFECTDATE < :ENDDATE
                           AND ISDELETE = '0'
                           AND TYPE = '2'
                           AND MACHINE LIKE '%' || :MACHINE || '%'";
            }
        }
        public string PatientScheduleTemplateDeleteByTemplateId
        {
            get
            {
                return @"DELETE FROM MED_PATIENT_SCHEDULE_TEMP_DATA T WHERE T.PATIENT_SCHEDULE_TEMPLATE_ID=:PATIENT_SCHEDULE_TEMPLATE_ID";
            }
        }
        public string PatientScheduleTemplateMenuDeleteByTemplateId
        {
            get
            {
                return @"DELETE FROM MED_PATIENT_SCHEDULE_TEMPLATE T WHERE T.PATIENT_SCHEDULE_TEMPLATE_ID=:PATIENT_SCHEDULE_TEMPLATE_ID";
            }
        }
        public string GetHosEquipmentDataByParms
        {
            get
            {
                return @"SELECT *
                          FROM MED_MACHINE_MIXBARREL
                         WHERE DISINFECTDATE > :BEGINDATE
                           AND DISINFECTDATE < :ENDDATE
                           AND ISDELETE = '0'
                           AND TYPE = '3'
                           AND MACHINE LIKE '%' || :MACHINE || '%'";
            }
        }

        /// <summary>
        /// 根据类型获取机器列表
        /// </summary>
        public string GetMachineListByType
        {
            get
            {
                return @"SELECT DM.*,DM.MACHINE_MODEL||'-'||DM.MACHINE_NAME MACHINE_NO,FL.ITEM_NAME FLNAME FROM MED_DIALYSIS_MACHINE DM
                        INNER JOIN (SELECT * FROM MED_COMMON_ITEMLIST IL WHERE IL.ITEM_TYPE = :TYPE AND IL.STATUS = '1') FL ON DM.TYPE = FL.ITEM_ID 
                        WHERE DM.AREA_ID IS NOT NULL ORDER BY TO_NUMBER(DM.MACHINE_SERIAL_NO)";
            }
        }
        /// <summary>
        /// 根据类型获取机器列表
        /// </summary>
        public string GetWaterMachineListByType
        {
            get
            {
                return @"SELECT DM.*,DM.MACHINE_MODEL||'-'||DM.MACHINE_NAME MACHINE_NO,FL.ITEM_NAME FLNAME FROM MED_DIALYSIS_MACHINE DM
                        INNER JOIN (SELECT * FROM MED_COMMON_ITEMLIST IL WHERE IL.ITEM_TYPE = :TYPE AND IL.STATUS = '1') FL ON DM.TYPE = FL.ITEM_ID 
                        ORDER BY TO_NUMBER(DM.MACHINE_SERIAL_NO)";
            }
        }
        /// <summary>
        /// 根据类型、床位获取机器列表
        /// </summary>
        public string GetMachineListByTypeAndBedId
        {
            get
            {
                return @"SELECT DM.*,FL.ITEM_NAME FLNAME FROM MED_DIALYSIS_MACHINE DM
                        INNER JOIN (SELECT * FROM MED_COMMON_ITEMLIST IL WHERE IL.ITEM_TYPE = :TYPE AND IL.STATUS = '1') FL ON DM.TYPE = FL.ITEM_ID 
                        WHERE DM.AREA_ID IS NOT NULL AND DM.BED_ID=:BED_ID ORDER BY TO_NUMBER(DM.MACHINE_SERIAL_NO)";
            }
        }
        /// <summary>
        /// 根据机器ID&日期获取水处理记录列表
        /// </summary>
        public string GetWaterProcessorRecordByIdAndDate
        {
            get
            {
                return @"SELECT WU.*,FL.ITEM_NAME FLNAME FROM MED_WATERPROCESSOR_USERECORD WU
                        INNER JOIN (SELECT * FROM MED_COMMON_ITEMLIST IL WHERE IL.STATUS = '1') FL ON WU.MACHINE_TYPE = FL.ITEM_ID
                        WHERE WU.MACHINE_ID=:MACHINE_ID AND WU.USEDATE>=:BEGINDATE AND WU.USEDATE<=:ENDDATE AND WU.ISDELETE='0' ORDER BY WU.USEDATE";
            }
        }

        /// <summary>
        /// 根据机器ID&单个日期获取水处理记录列表
        /// </summary>
        public string GetWaterProcessorRecordByIdAndSingleDate
        {
            get
            {
                return @"SELECT E.MACHINE_ID,
                        E.USEDATE,
                        E.OUTWATER_PRESSURE,
                        E.INWATER_PRESSURE,
                        E.INWATER_CONDUCTIVITY,
                        E.OUTWATER_CONDUCTIVITY,
                        E.WASTEWATER_FLOW,
                        DECODE(E.SANDJAR,'1','√','×') as SANDJAR,
                        DECODE(E.CARBONJAR,'1','√','×') as CARBONJAR,
                        DECODE(E.RESINJAR,'1','√','×') as RESINJAR,
                        E.RESIDUALCHLORINE_TESTRESULT,
                        E.HARDNESS_TESTRESULT,
                        E.EMP_NO,
                        E.RECORD_ID
                        FROM MED_WATERPROCESSOR_USERECORD E
                        WHERE TO_CHAR(E.USEDATE,'YYYY-MM')=TO_CHAR(:USEDATE,'YYYY-MM')
                        AND E.MACHINE_ID=:MACHINE_ID AND E.ISDELETE='0'";
            }
        }

        /// <summary>
        /// 根据机器ID获取机器信息
        /// </summary>
        public string GetMachineById
        {
            get
            {
                return @"SELECT DM.*,FL.ITEM_NAME FLNAME FROM MED_DIALYSIS_MACHINE DM
                        INNER JOIN (SELECT * FROM MED_COMMON_ITEMLIST IL WHERE IL.STATUS = '1') FL ON DM.TYPE = FL.ITEM_ID 
                        WHERE DM.MACHINE_ID=:MACHINE_ID AND DM.AREA_ID IS NOT NULL ORDER BY TO_NUMBER(DM.MACHINE_SERIAL_NO)";
            }
        }
        /// <summary>
        /// 根据机器ID获取机器信息
        /// </summary>
        public string GetWaterMachineById
        {
            get
            {
                return @"SELECT DM.*,FL.ITEM_NAME FLNAME FROM MED_DIALYSIS_MACHINE DM
                        INNER JOIN (SELECT * FROM MED_COMMON_ITEMLIST IL WHERE IL.STATUS = '1') FL ON DM.TYPE = FL.ITEM_ID 
                        WHERE DM.MACHINE_ID=:MACHINE_ID ORDER BY TO_NUMBER(DM.MACHINE_SERIAL_NO)";
            }
        }
        public string GetMachineByMachineName
        {
            get
            {
                return @"SELECT DM.*,FL.ITEM_NAME FLNAME FROM MED_DIALYSIS_MACHINE DM
                        INNER JOIN (SELECT * FROM MED_COMMON_ITEMLIST IL WHERE IL.STATUS = '1' AND IL.item_type='血透机品牌') FL ON DM.TYPE = FL.ITEM_ID 
                        WHERE DM.AREA_ID=:AREA_ID AND DM.MACHINE_NAME=:MACHINE_NAME AND DM.AREA_ID IS NOT NULL ORDER BY TO_NUMBER(DM.MACHINE_SERIAL_NO)";
            }
        }
        /// <summary>
        /// 根据机器ID&日期获取血透机运行记录列表
        /// </summary>
        public string GetMachineUseRecordByIdAndDate
        {
            get
            {
                return @"SELECT MU.*,FL.ITEM_NAME FLNAME FROM MED_MACHINE_USERECORD MU
                        INNER JOIN (SELECT * FROM MED_COMMON_ITEMLIST IL WHERE IL.STATUS = '1') FL ON MU.MACHINE_TYPE = FL.ITEM_ID
                        WHERE MU.MACHINE_ID=:MACHINE_ID AND TRUNC(MU.USEDATE)>=TRUNC(:BEGINDATE) AND TRUNC(MU.USEDATE)<=TRUNC(:ENDDATE) AND MU.ISDELETE='0' ORDER BY MU.USEDATE,MU.MACHINE_MODEL,MU.MACHINE_NAME";
            }
        }

        /// <summary>
        /// 根据病区、床位、班次、日期获取血透机运行记录列表
        /// </summary>
        public string GetMachineUseRecordList
        {
            get
            {
                return @"SELECT R.*,R.MACHINE_MODEL||'-'||R.MACHINE_NAME MACHINE_FULL_NAME,FL.ITEM_NAME FLNAME FROM MED_MACHINE_USERECORD R
                        INNER JOIN (SELECT * FROM MED_COMMON_ITEMLIST FL WHERE FL.STATUS = '1') FL ON R.MACHINE_TYPE = FL.ITEM_ID
                        WHERE R.DIALYSIS_ROOM_ID LIKE '%'||:DIALYSIS_ROOM_ID||'%' AND R.BED_NUMBER LIKE '%'||:BED_NUMBER||'%'
                        AND R.BANCI_ID LIKE '%'||:BANCI_ID||'%' AND TRUNC(R.USEDATE)>=TRUNC(:BEGINDATE) AND TRUNC(R.USEDATE)<=TRUNC(:ENDDATE) AND R.ISDELETE='0' 
                        ORDER BY R.USEDATE,R.MACHINE_MODEL,R.MACHINE_NAME";
            }
        }

        /// <summary>
        /// 根据签名&日期获取血透机运行记录列表
        /// </summary>
        public string GetMachineUseRecordBySignIdAndDate
        {
            get
            {
                return @"SELECT MU.*,FL.ITEM_NAME FLNAME FROM MED_MACHINE_USERECORD MU
                        INNER JOIN (SELECT * FROM MED_COMMON_ITEMLIST IL WHERE IL.STATUS = '1') FL ON MU.MACHINE_TYPE = FL.ITEM_ID
                        WHERE MU.USEDATE>=:BEGINDATE AND MU.USEDATE<=:ENDDATE AND MU.ISDELETE='0' ORDER BY MU.USEDATE,MU.MACHINE_MODEL,MU.MACHINE_NAME";
            }//MU.MACHINE_ID in (:SIGN_NAME) AND 
        }

        /// <summary>
        /// 根据机器ID&单个日期获取血透机运行记录列表
        /// </summary>
        public string GetMachineUseRecordByIdAndSingleDate
        {
            get
            {
                return @"SELECT M.MACHINE_ID,M.MACHINE_NAME,M.MACHINE_TYPE,M.MACHINE_MODEL,M.THERAPEUTIC_PROPERTIES,M.BANCI_ID,
                        DECODE(M.BANCI_ID,'1','上午','2','下午','3','晚班','急诊') BANCI_NAME,M.DIALYSIS_ROOM_ID,M.BED_NUMBER,
                        M.MACHINE_CHECK,M.MACHINE_ALARM,M.DEGASSING,M.WORKING,M.USERTIME,M.OPERATION,M.DEALWITH,M.USEDATE,
                        M.CREATEDATE,M.ISDELETE,DECODE(M.INNER_DEGASSING,'0','未消毒','1','已消毒') INNER_DEGASSING,
                        M.CLASS_WAY,DECODE(M.OUTER_DEGASSING,'0','未消毒','1','已消毒') OUTER_DEGASSING,M.DAY_WAY,
                       (SELECT S.NAME FROM MED_STAFF_DICT S WHERE S.EMP_NO=M.SIGN_NAME) SIGN_NAME,M.RECORD_ID
                        FROM MED_MACHINE_USERECORD M
                        WHERE TO_CHAR(M.USEDATE,'YYYY-MM')=TO_CHAR(:USEDATE,'YYYY-MM')
                        AND M.MACHINE_ID=:MACHINE_ID  AND M.ISDELETE='0' ORDER BY M.USEDATE,M.BANCI_ID";
            }
        }

        /// <summary>
        /// 获取血红蛋白趋势
        /// </summary>
        public string GetHBTrend
        {
            get
            {
                return @"SELECT DISTINCT T.RESULTS_RPT_DATE_TIME,R.REPORT_ITEM_NAME,R.RESULT,R.UNITS,R.REFERENCE_RESULT,R.RESULT_DATE_TIME FROM MED_LAB_TEST_MASTER T
                        LEFT JOIN MED_LAB_RESULT R ON T.TEST_NO=R.TEST_NO
                        WHERE T.TEST_NO IN
                        (
                        SELECT T.TEST_NO FROM MED_LAB_TEST_ITEMS T
                        WHERE T.ITEM_NAME LIKE '%血常规（门诊）%' OR T.ITEM_NAME LIKE '%血常规(门诊)%' OR T.ITEM_NAME LIKE '%血细胞分析6分类%' OR T.ITEM_NAME LIKE '%急诊血细胞分析%'
                        )
                        AND T.PATIENT_ID=:PATIENT_ID AND TRUNC(T.RESULTS_RPT_DATE_TIME) BETWEEN TRUNC(:STARTDATE) AND TRUNC(:ENDDATE)
                        AND R.REPORT_ITEM_NAME LIKE '%血红蛋白浓度%' ORDER BY T.RESULTS_RPT_DATE_TIME";
            }
        }
        #endregion

        #region 长期处方
        /// <summary>
        /// 获取全部长期处方数据SQL
        /// </summary>
        public string GetAllRecipe
        {
            get
            {
                return "SELECT r.*, item.item_name as PURIFICATION_MODE_NAME FROM MED_HEMO_RECIPE r left join med_common_itemlist item on r.PURIFICATION_MODE = item.item_id";
            }
        }

        /// <summary>
        /// 根据处方编号得到对应长期处方数据SQL
        /// </summary>
        public string GetRecipeByRecipeID
        {
            get
            {
                return "SELECT r.*, item.item_name as PURIFICATION_MODE_NAME FROM MED_HEMO_RECIPE r left join med_common_itemlist item on r.PURIFICATION_MODE = item.item_id WHERE r.RECIPE_ID = :RECIPE_ID";
            }
        }

        /// <summary>
        /// 根据处方ID列表得到对应的处方数据
        /// </summary>
        public string SaveRecipeUserIDByRecipeIDList
        {
            get
            {
                return "update MED_HEMO_RECIPE set USER_ID='{0}' where RECIPE_ID in ({1})";
            }
        }

        /// <summary>
        /// 根据透析号得到对应当天临时处方数据SQL
        /// </summary>
        public string GetRecipeByHemodialysisID
        {
            get
            {
                return @"SELECT distinct MED_HEMO_RECIPE.*,item.item_name as PURIFICATION_MODE_NAME, '' as MACHINE_ID,
                        itemAccess.Item_Name as Vascular_Access_Name, case MED_HEMO_RECIPE.status when '1' then '已启用' else '未启用' end as STATUSNAME
                         FROM MED_HEMO_RECIPE
                          LEFT JOIN MED_COMMON_ITEMLIST ITEM
                            ON MED_HEMO_RECIPE.PURIFICATION_MODE = ITEM.ITEM_ID
                          LEFT JOIN MED_PATIENT_SCHEDULE SCH
                            ON SCH.RECIPE_ID = MED_HEMO_RECIPE.RECIPE_ID
                          LEFT JOIN MED_COMMON_ITEMLIST ITEMMONITOR
                            ON SCH.BED_NUMBER = ITEMMONITOR.ITEM_ID
                          LEFT JOIN MED_VASCULAR_ACCESS S
                            ON  MED_HEMO_RECIPE.VASCULAR_ACCESS_ID=S.VASCULAR_ACCESS_ID
                          LEFT JOIN MED_COMMON_ITEMLIST ITEMACCESS
                            ON S.VASCULAR_ACCESS_TYPE = ITEMACCESS.ITEM_ID
                        WHERE MED_HEMO_RECIPE.HEMODIALYSIS_ID = :HEMODIALYSIS_ID
                        AND  trunc(recipe_date)=trunc(sysdate) and Recipe_Type='0' and MED_HEMO_RECIPE.status !='2'";
            }
        }

        /// <summary>
        /// 根据透析编号获取长期处方
        /// </summary>
        public string GetLongRecipeByHemodialysisID
        {
            get
            {
                return @"SELECT T.* FROM MED_HEMO_RECIPE T
                        WHERE T.HEMODIALYSIS_ID = :HEMODIALYSIS_ID AND T.RECIPE_TYPE='1' AND T.STATUS !='2'";
            }
        }

        /// <summary>
        /// 根据透析号得到对应当天临时处方数据SQL
        /// </summary>
        public string GetRecipeByHemodialysisIDAndDate
        {
            get
            {
                return @"SELECT MED_HEMO_RECIPE.*,item.item_name as PURIFICATION_MODE_NAME, '' as MACHINE_ID,
                        itemAccess.Item_Name as Vascular_Access_Name,DECODE( MED_HEMO_RECIPE.STATUS,'1','已启用','未启用') STATUSNAME
                         FROM MED_HEMO_RECIPE
                          left join med_common_itemlist item
                            on MED_HEMO_RECIPE.PURIFICATION_MODE = item.item_id
                          left join med_patient_schedule sch
                            on sch.RECIPE_ID = MED_HEMO_RECIPE.RECIPE_ID
                          left join med_common_itemlist itemMonitor
                            on sch.bed_number = itemMonitor.item_id
                          LEFT JOIN MED_VASCULAR_ACCESS S
                            ON MED_HEMO_RECIPE.VASCULAR_ACCESS_ID = S.VASCULAR_ACCESS_ID
                          LEFT JOIN MED_COMMON_ITEMLIST ITEMACCESS
                            ON S.VASCULAR_ACCESS_TYPE = ITEMACCESS.ITEM_ID
                        WHERE MED_HEMO_RECIPE.HEMODIALYSIS_ID = :HEMODIALYSIS_ID
                        AND  trunc(recipe_date)=trunc(:RecipeDate) and Recipe_Type='0' and MED_HEMO_RECIPE.status !='2'";
            }
        }

        /// <summary>
        /// 得到当天日期处方编号的数量
        /// </summary>
        public string GetRecipeIDCount
        {
            get
            {
                return "SELECT COUNT(1) FROM MED_HEMO_RECIPE WHERE SUBSTR(RECIPE_ID,0,10) = :TodayDate";
            }
        }

        /// <summary>
        /// 得到新生成的处方日期号
        /// </summary>
        public string GetNewRecipeID
        {
            get
            {
                return @"
                        SELECT to_char(sysdate,'yyyy-mm-dd')||'-'||(MAX(SUBSTR(RECIPE_ID,12,4))+1) as RECIPE_ID
                        FROM MED_HEMO_RECIPE WHERE SUBSTR(RECIPE_ID,0,10) = :TodayDate";
            }
        }
        public string ExecuteProLogInfos
        {
            get
            {
                return @"SELECT COUNT(T.ID)
                              FROM MED_PROCLOG T
                             WHERE T.EXECPARAM = TO_CHAR(TRUNC(SYSDATE),'YYYY-MM-DD')
                               AND T.PROCNAME = 'PRO_MED_CUREDRUG'";
            }
        }
        public string ExecuteProLogInfos2
        {
            get
            {
                return @"INSERT INTO MED_PROCLOG
                              (ID, PROCNAME, EXECPARAM, EXECDATA)
                            VALUES
                              (sys_guid(), 'PRO_MED_CUREDRUG', trunc(sysdate), SYSDATE)";
            }
        }
        /// <summary>
        /// 在治疗单页面，未开始治疗时读取默认处方信息的内容
        /// </summary>
        public string GetRecipeInfoInCureFunction
        {
            get
            {
                return @"SELECT R.*,
                           ITEM.ITEM_NAME  AS PURIFICATION_MODE_NAME,
                           P.ITEM_NAME     AS PURIFIER_NAME,
                           V.ITEM_NAME     AS VASCULAR_ACCESS_NAME,
                           M.ITEM_NAME     AS MACHINE_TYPE_NAME,
                           T.ITEM_NAME     AS HEPARIN_SPECIES_NAME,
                           MS.MACHINE_NAME
                      FROM MED_HEMO_RECIPE R
                      LEFT JOIN MED_COMMON_ITEMLIST ITEM
                        ON R.PURIFICATION_MODE = ITEM.ITEM_ID
                      LEFT JOIN MED_VASCULAR_ACCESS S
                        ON R.VASCULAR_ACCESS_ID=S.VASCULAR_ACCESS_ID
                      LEFT JOIN MED_COMMON_ITEMLIST V
                        ON S.VASCULAR_ACCESS_TYPE = V.ITEM_ID
                      LEFT JOIN MED_COMMON_ITEMLIST M
                        ON R.FIRST_PURIFIER_MODEL = M.ITEM_ID
                      LEFT JOIN MED_COMMON_ITEMLIST T
                        ON R.THERAPEUTIC_METHOD = T.ITEM_ID
                      LEFT JOIN MED_COMMON_ITEMLIST P
                        ON R.FIRST_PURIFIER_NAME = P.ITEM_ID
                      LEFT JOIN MED_PATIENT_SCHEDULE SCH
                        ON R.RECIPE_ID = SCH.RECIPE_ID
                      LEFT JOIN MED_DIALYSIS_MACHINE MS
                        ON SCH.MONITOR_LABEL = MS.MACHINE_ID
                     WHERE R.RECIPE_ID = :RECIPE_ID";
            }
        }

        public string SaveTodayRecipes
        {
            get
            {
                return @"insert into med_hemo_recipe
                        select sys_guid() as recipe_id,t.patient_id,sysdate as recipe_date,t.purification_mode,t.dry_weight,
                        t.frequency_week,t.frequency_times,t.frequency_hours,t.spkt_v,t.urr,t.first_purifier_model,
                        t.first_purifier_name,t.first_purifier_m2,t.first_purifier_koa,t.first_purifier_kuf,t.SECOND_PURIFIER_MODEL,
                        t.second_purifier_name,t.second_purifier_m2,t.second_purifier_koa,t.second_purifier_kuf,
                        t.sodion,t.potassium_ion,t.calcium_ion,t.bicarbonate_radical,t.bloow_flow,t.dialysate_flow,
                        t.dialysate_temperature,t.displacement_liquid,t.blood_displacement,t.therapeutic_method,
                        t.first_drug_name,t.first_drug_dosage,t.first_drug_unit,t.first_drug_mode,
                        t.second_drug_name,t.second_drug_dosage,t.second_drug_unit,t.second_drug_mode,t.user_id,
                        t.hemodialysis_id,'0' as STATUS,t.vascular_access_id,'0' as RECIPE_TYPE,0 as UFR,0 as TODAY_WEIGHT,0 as TODAY_BLOODA,0 as TODAY_BLOODB,
                        T.REMARK,0 AS TODAY_BLOODP,T.DRY_WEIGHT_REMARK,T.FREQUENCY_MINUTE from med_hemo_recipe t 
                        where t.status='1' and t.hemodialysis_id in(select t.hemodialysis_id from med_hemo_recipe t 
                        where trunc(t.recipe_date) not in trunc(sysdate) and t.status = '1' and t.hemodialysis_id in (:HemoList))";
            }
        }

        public string CreatePatientRecipeBydate
        {
            get
            {
                return @"INSERT INTO MED_HEMO_RECIPE
                          SELECT TO_CHAR(:RECIPEDATE, 'YYYYMMDD') ||
                                 LPAD(MED_RECIPE_NEWID.NEXTVAL, 8, '0') AS RECIPE_ID,
                                 T.PATIENT_ID,
                                 :RECIPEDATE AS RECIPE_DATE,
                                 T.PURIFICATION_MODE,
                                 T.DRY_WEIGHT,
                                 T.FREQUENCY_WEEK,
                                 T.FREQUENCY_TIMES,
                                 T.FREQUENCY_HOURS,
                                 T.SPKT_V,
                                 T.URR,
                                 T.FIRST_PURIFIER_MODEL,
                                 T.FIRST_PURIFIER_NAME,
                                 T.FIRST_PURIFIER_M2,
                                 T.FIRST_PURIFIER_KOA,
                                 T.FIRST_PURIFIER_KUF,
                                 T.SECOND_PURIFIER_MODEL,
                                 T.SECOND_PURIFIER_NAME,
                                 T.SECOND_PURIFIER_M2,
                                 T.SECOND_PURIFIER_KOA,
                                 T.SECOND_PURIFIER_KUF,
                                 T.SODION,
                                 T.POTASSIUM_ION,
                                 T.CALCIUM_ION,
                                 T.BICARBONATE_RADICAL,
                                 T.BLOOW_FLOW,
                                 T.DIALYSATE_FLOW,
                                 T.DIALYSATE_TEMPERATURE,
                                 T.DISPLACEMENT_LIQUID,
                                 T.BLOOD_DISPLACEMENT,
                                 T.THERAPEUTIC_METHOD,
                                 T.FIRST_DRUG_NAME,
                                 T.FIRST_DRUG_DOSAGE,
                                 T.FIRST_DRUG_UNIT,
                                 T.FIRST_DRUG_MODE,
                                 T.SECOND_DRUG_NAME,
                                 T.SECOND_DRUG_DOSAGE,
                                 T.SECOND_DRUG_UNIT,
                                 T.SECOND_DRUG_MODE,
                                 T.USER_ID,
                                 T.HEMODIALYSIS_ID,
                                 '0' AS STATUS,
                                 T.VASCULAR_ACCESS_ID,
                                 '0' AS RECIPE_TYPE,
                                 0 AS UFR,
                                 0 AS TODAY_WEIGHT,
                                 0,
                                 0,
                                 T.REMARK,
                                 0 AS TODAY_BLOODP,
                                 T.DRY_WEIGHT_REMARK,
                                 T.FREQUENCY_MINUTE,
                                 T.DISPLACEMENT_MODE,
                                 T.DISPLACEMENT_RECIPE,
                                 T.DISPLACEMENT_SPECIAL_ADJUST,
                                 T.ANTICOAGULANT_USE,
                                 T.SPECIAL_MATTER,
                                 T.UFR2,
                                 T.DISPLACEMENT_FLOW,
                                 T.FOCUS_LEVEL,
                                 T.SENSES,
                                 T.ALLERGIC,T.BR,T.BEFORE_TEMPERATURE
                            FROM MED_HEMO_RECIPE T
                           WHERE T.RECIPE_TYPE = '1'
                             AND T.STATUS = '1'
                             AND T.HEMODIALYSIS_ID IN
                                 (SELECT T.HEMODIALYSIS_ID
                                    FROM MED_HEMO_RECIPE T
                                   WHERE T.RECIPE_TYPE = '1'
                                     AND T.HEMODIALYSIS_ID IN
                                         (SELECT T.HEMODIALYSIS_ID
                                            FROM MED_PATIENT_SCHEDULE T
                                           WHERE TRUNC(T.DIALYSIS_DATE) = TRUNC(:RECIPEDATE))
                                     AND T.HEMODIALYSIS_ID NOT IN
                                         (SELECT R.HEMODIALYSIS_ID
                                            FROM MED_HEMO_RECIPE R
                                           WHERE TRUNC(R.RECIPE_DATE) = TRUNC(:RECIPEDATE)
                                             AND R.RECIPE_TYPE = '0'))";
            }
        }
        public string DeleteUnExcuteRecipeByHemoID
        {
            get
            {
                return @"DELETE FROM MED_HEMO_RECIPE P
                             WHERE P.RECIPE_ID IN (SELECT T.RECIPE_ID
                                                     FROM MED_HEMO_RECIPE T
                                                     LEFT JOIN MED_PATIENT_SCHEDULE P
                                                       ON TRUNC(T.RECIPE_DATE) = TRUNC(P.DIALYSIS_DATE)
                                                      AND T.HEMODIALYSIS_ID = P.HEMODIALYSIS_ID
                                                    WHERE TRUNC(T.RECIPE_DATE) >= TRUNC(:RECIPE_DATE)
                                                      AND T.RECIPE_TYPE = '0'
                                                      AND T.HEMODIALYSIS_ID = :HEMODIALYSIS_ID
                                                      AND P.PURIFIER_MODEL_ID IS NULL)";
            }
        }

        #endregion

        #region 病患排班相关SQL
        public string GetWeekDutyByTime
        {
            get
            {
                return @"SELECT Z.USER_NAME,
                               MAX(DECODE(Z.WEEKDAY, 'MONDAY', Z.DEPT_NAME)) AS MONDAY,
                               MAX(DECODE(Z.WEEKDAY, 'TUESDAY', Z.DEPT_NAME)) AS TUESDAY,
                               MAX(DECODE(Z.WEEKDAY, 'WEDNESDAY', Z.DEPT_NAME)) AS WEDNESDAY,
                               MAX(DECODE(Z.WEEKDAY, 'THURSDAY', Z.DEPT_NAME)) AS THURSDAY,
                               MAX(DECODE(Z.WEEKDAY, 'FRIDAY', Z.DEPT_NAME)) AS FRIDAY,
                               MAX(DECODE(Z.WEEKDAY, 'SATURDAY', Z.DEPT_NAME)) AS SATURDAY,
                               MAX(DECODE(Z.WEEKDAY, 'SUNDAY', Z.DEPT_NAME)) AS SUNDAY
                          FROM (SELECT (SELECT S.NAME
                                          FROM MED_STAFF_DICT S
                                         WHERE S.EMP_NO = T.USER_ID) AS USER_NAME,
                                       UPPER(T.WEEKDAY) WEEKDAY,
                                       T.DUTYDAY,
                                       (SELECT C.ITEM_NAME
                                          FROM MED_COMMON_ITEMLIST C
                                         WHERE C.ITEM_ID = T.OFFICEID) AS DEPT_NAME
                                  FROM MED_USERS_WEEKDUTY T
                                 WHERE T.TYPE = 'N' AND TRUNC(T.DUTYDAY) >= :BEGINDATE AND TRUNC(T.DUTYDAY)<= :ENDDATE
                                 ORDER BY T.DUTYDAY) Z
                         GROUP BY Z.USER_NAME";
            }
        }

        public string GetPatientScheduleByRecipeId
        {
            get
            {
                return @"SELECT * FROM MED_PATIENT_SCHEDULE T WHERE T.RECIPE_ID = :RECIPE_ID";
            }
        }
        public string CreateCurrntDataByLastWeek
        {
            get
            {
                return @"INSERT INTO MED_USERS_WEEKDUTY
                          (ID, USER_ID, WEEKDAY, DUTYDAY, CREATEDATE, CREATEBY, TYPE,OFFICEID)
                          SELECT SYS_GUID() AS ID,
                                 T.USER_ID,
                                 T.WEEKDAY,
                                 T.DUTYDAY + 7 as DUTYDAY,
                                 SYSDATE as CREATEDATE,
                                 T.CREATEBY as CREATEBY,
                                 T.TYPE,T.OFFICEID
                            FROM MED_USERS_WEEKDUTY T
                           WHERE TRUNC(T.DUTYDAY) >= TRUNC(:BEGINDATE)
                             AND TRUNC(T.DUTYDAY) <= TRUNC(:ENDDATE)
                             AND T.TYPE='N'";
            }
        }
        public string CreateCurrntDataByLastWeekDoctor
        {
            get
            {
                return @"INSERT INTO MED_USERS_WEEKDUTY
                          (ID, USER_ID, WEEKDAY, DUTYDAY, CREATEDATE, CREATEBY, TYPE,OFFICEID)
                          SELECT SYS_GUID() AS ID,
                                 T.USER_ID,
                                 T.WEEKDAY,
                                 T.DUTYDAY + 7 as DUTYDAY,
                                  SYSDATE as CREATEDATE,
                                 T.CREATEBY as CREATEBY,
                                 T.TYPE,T.OFFICEID
                            FROM MED_USERS_WEEKDUTY T
                           WHERE TRUNC(T.DUTYDAY) >= TRUNC(:BEGINDATE)
                             AND TRUNC(T.DUTYDAY) <= TRUNC(:ENDDATE)
                             AND T.TYPE='D'";
            }
        }
        public string GetWeekDutyByDate
        {
            get
            {
                return @"SELECT T.*,
                             (SELECT L.ITEM_NAME FROM MED_COMMON_ITEMLIST L WHERE L.ITEM_ID = T.OFFICEID AND L.ITEM_TYPE in ('区域','假期类型')) AS OFFICENAME
                          FROM MED_USERS_WEEKDUTY T
                          WHERE TRUNC(T.DUTYDAY) >=:BEGINDATE   AND TRUNC(T.DUTYDAY) <= :ENDDATE  AND T.TYPE='N'";
            }
        }
        public string GetWeekDutyByDateDoctor
        {
            get
            {
                return @"SELECT * FROM MED_USERS_WEEKDUTY T WHERE TRUNC(T.DUTYDAY) >=:BEGINDATE   AND TRUNC(T.DUTYDAY) <= :ENDDATE  AND T.TYPE='D'";
            }
        }
        public string PatientDutyDeleteByData
        {
            get
            {
                return @"DELETE MED_USERS_WEEKDUTY T
                             WHERE TRUNC(T.DUTYDAY) >= TRUNC(:BEGINDATE)
                               AND TRUNC(T.DUTYDAY) <= TRUNC(:ENDDATE) AND T.TYPE= :TYPE";
            }
        }
        public string InSertExecProcLog
        {
            get
            {
                return @"INSERT INTO MED_PROCLOG
                              (ID, PROCNAME, EXECPARAM, EXECDATA)
                            VALUES
                              (:ID, 'PRO_MED_PATIENTSCHEDULE', :EXECPARAM, SYSDATE)";
            }
        }

        /// <summary>
        /// 获取病患排班数据SQL
        /// </summary>
        public string GetPatientScheduleSignle
        {
            get
            {
                return @"SELECT s.patient_schedule_id,
                                   s.patient_id,
                                   s.monitor_label,
                                   s.dialysis_date,
                                   s.banci_id,
                                   s.dialysis_room_id,
                                   s.bed_number,
                                   s.start_time,
                                   s.end_time,
                                   s.status,
                                   s.hemodialysis_id,
                                   s.remark,
                                   s.recipe_id,
                                   decode(decode(s.recipe_id,
                                                 null,
                                                 s.purifier_model_id,
                                                 (select distinct d.machine_type
                                                    from med_cure_main d
                                                   where d.recipe_id = s.recipe_id and d.recipe_type !='1')),
                                          null,
                                          s.purifier_model_id,
                                          (select distinct d.machine_type
                                             from med_cure_main d
                                            where d.recipe_id = s.recipe_id and d.recipe_type !='1')) as purifier_model_id,
                                   s.user_id,
                                   s.focus_level,
                                   p.NAME PATIENTNAME,
                                   p.SEX,
                                   p.INFECTIOUS_CHECK_RESULT,
                                   p.DIAGNOSE,
                                   qy.ITEM_NAME AREANAME,
                                   cw.ITEM_NAME BEDNAME,
                                   m.MACHINE_NAME
                              FROM MED_PATIENT_SCHEDULE s
                             INNER JOIN MED_PATIENTS p
                                ON s.HEMODIALYSIS_ID = p.HEMODIALYSIS_ID
                             INNER JOIN (SELECT *
                                           FROM MED_COMMON_ITEMLIST il
                                          WHERE il.ITEM_TYPE = '区域'
                                            AND il.STATUS = '1') qy
                                ON s.DIALYSIS_ROOM_ID = qy.ITEM_ID
                             INNER JOIN (SELECT *
                                           FROM MED_COMMON_ITEMLIST il
                                          WHERE il.ITEM_TYPE = '床位'
                                            AND il.STATUS = '1') cw
                                ON s.BED_NUMBER = cw.ITEM_ID
                             INNER JOIN MED_DIALYSIS_MACHINE m
                                ON s.MONITOR_LABEL = m.MACHINE_ID
                    WHERE
                      1 = 1
                      AND s.DIALYSIS_DATE like to_date(:DIALYSIS_DATE,'yyyy-mm-dd') AND s.HEMODIALYSIS_ID = :HEMODIALYSIS_ID";
            }
        }
        public string GetUserWorkExtendFromCureMain
        {
            get
            {
                return @"SELECT T.PRIMARY_NURSE NURSE_ID,
                               T.CURE_CREATE_DATE,
                               T5.ITEM_NAME  REMARK,
                               T1.BANCI_ID,
                               T1.DIALYSIS_ROOM_ID AREAID,
                               T1.BED_NUMBER       BEDID,
                               T2.ITEM_NAME        AREANAME,
                               T3.ITEM_NAME        BEDNAME,
                               T4.ITEM_NAME        BANCINAME
                          FROM MED_CURE_MAIN T
                          LEFT JOIN MED_PATIENT_SCHEDULE T1
                            ON T.RECIPE_ID = T1.RECIPE_ID
                          LEFT JOIN MED_COMMON_ITEMLIST T2
                            ON T1.DIALYSIS_ROOM_ID = T2.ITEM_ID
                          LEFT JOIN MED_COMMON_ITEMLIST T3
                            ON T1.BED_NUMBER = T3.ITEM_ID
                          LEFT JOIN (SELECT * FROM MED_COMMON_ITEMLIST WHERE ITEM_TYPE = '班次') T4
                            ON T1.BANCI_ID = T4.ITEM_VALUE
                          LEFT JOIN MED_COMMON_ITEMLIST T5
                          ON T.PURIFICATION_MODE=T5.ITEM_ID
                         WHERE T1.DIALYSIS_DATE = TRUNC(:DIALYSIS_DATE)
                           AND (T.CURE_STATUS IS NULL OR T.CURE_STATUS != '4')
                         ORDER BY T.CURE_CREATE_DATE";
            }
        }
        public string GetParamUserWorkExtend
        {
            get
            {
                return @"SELECT T.NURSE_ID,
                               T.CREATE_DATE,T.EXTENDED_FIELD_2 REMARK,
                               T1.BANCI_ID,
                               T1.DIALYSIS_ROOM_ID AREAID,
                               T1.BED_NUMBER       BEDID,
                               T2.ITEM_NAME        AREANAME,
                               T3.ITEM_NAME        BEDNAME,
                               T4.ITEM_NAME        BANCINAME
                          FROM MED_HEMODIALYSIS_PARAMETERS T
                          LEFT JOIN MED_PATIENT_SCHEDULE T1
                            ON T.RECIPE_ID = T1.RECIPE_ID
                          LEFT JOIN MED_COMMON_ITEMLIST T2
                            ON T1.DIALYSIS_ROOM_ID = T2.ITEM_ID
                          LEFT JOIN MED_COMMON_ITEMLIST T3
                            ON T1.BED_NUMBER = T3.ITEM_ID
                          LEFT JOIN (SELECT * FROM MED_COMMON_ITEMLIST WHERE ITEM_TYPE = '班次') T4
                            ON T1.BANCI_ID = T4.ITEM_VALUE
                         WHERE T1.DIALYSIS_DATE = TRUNC(:DIALYSIS_DATE) AND (T.EXTENDED_FIELD_1 IS NULL OR  T.EXTENDED_FIELD_1!='1')
                         ORDER BY T.CREATE_DATE";
            }
        }
        public string GetParamALLUserWorkExtend
        {
            get
            {
                return @"SELECT Z.NURSE_ID, Z.REMARK, COUNT(1) AS BEDNAME
                              FROM (SELECT T.NURSE_ID,
                                           T.CREATE_DATE,
                                           CASE
                                             WHEN (INSTR(T.EXTENDED_FIELD_2, '上机') > 0) THEN
                                              '上机'
                                             WHEN (INSTR(T.EXTENDED_FIELD_2, '下机') > '0') THEN
                                              '下机'
                                           END REMARK,
                                           T.EXTENDED_FIELD_2 REMARKDDDD,
                                           T1.BANCI_ID,
                                           T1.DIALYSIS_ROOM_ID AREAID,
                                           T1.BED_NUMBER BEDID,
                                           T2.ITEM_NAME AREANAME,
                                           T3.ITEM_NAME BEDNAME,
                                           T4.ITEM_NAME BANCINAME
                                      FROM MED_HEMODIALYSIS_PARAMETERS T
                                      LEFT JOIN MED_PATIENT_SCHEDULE T1
                                        ON T.RECIPE_ID = T1.RECIPE_ID
                                      LEFT JOIN MED_COMMON_ITEMLIST T2
                                        ON T1.DIALYSIS_ROOM_ID = T2.ITEM_ID
                                      LEFT JOIN MED_COMMON_ITEMLIST T3
                                        ON T1.BED_NUMBER = T3.ITEM_ID
                                      LEFT JOIN (SELECT *
                                                  FROM MED_COMMON_ITEMLIST
                                                 WHERE ITEM_TYPE = '班次') T4
                                        ON T1.BANCI_ID = T4.ITEM_VALUE
                                     WHERE T1.DIALYSIS_DATE >= TRUNC(:DTSTAR)
                                       AND T1.DIALYSIS_DATE <= TRUNC(:DTEND)
                                       AND ((T.EXTENDED_FIELD_2 LIKE '%上机%') OR
                                           (T.EXTENDED_FIELD_2 LIKE '%下机%'))
                                     ORDER BY T.CREATE_DATE) Z
                             GROUP BY Z.NURSE_ID, Z.REMARK";
            }
        }
        /// <summary>
        /// 获取病患排班数据SQL 
        /// </summary>
        public string GetPatientScheduleList
        {
            get
            {
                return @"
                    SELECT s.patient_schedule_id,
                           s.patient_id,
                           s.monitor_label,
                           s.dialysis_date,
                           s.banci_id,
                           s.dialysis_room_id,
                           s.bed_number,
                           s.start_time,
                           s.end_time,
                           s.status,
                           s.hemodialysis_id,
                           s.remark,
                           s.recipe_id,
                             DECODE(S.START_TIME,
                              NULL,
                              S.PURIFIER_MODEL_ID,
                              FUN_PURIFIER_MODEL_ID(S.RECIPE_ID)) AS PURIFIER_MODEL_ID,
                           s.user_id,
                           s.focus_level,
                      p.NAME PATIENTNAME, p.SEX, p.INFECTIOUS_CHECK_RESULT, qy.ITEM_NAME AREANAME, cw.ITEM_NAME BEDNAME, m.MACHINE_NAME
                    FROM MED_PATIENT_SCHEDULE s
                    INNER JOIN MED_PATIENTS p ON s.HEMODIALYSIS_ID = p.HEMODIALYSIS_ID
                    INNER JOIN (SELECT * FROM MED_COMMON_ITEMLIST il WHERE il.ITEM_TYPE = '区域' AND il.STATUS = '1') qy ON s.DIALYSIS_ROOM_ID = qy.ITEM_ID  
                    INNER JOIN (SELECT * FROM MED_COMMON_ITEMLIST il WHERE il.ITEM_TYPE = '床位' AND il.STATUS = '1') cw ON s.BED_NUMBER = cw.ITEM_ID
                    INNER JOIN MED_DIALYSIS_MACHINE m ON s.MONITOR_LABEL = m.MACHINE_ID
                    WHERE
                      1 = 1
                      AND (P.is_delete != '1' OR P.IS_DELETE IS NULL)
                      AND s.DIALYSIS_DATE >=TO_DATE(:BEGINDIALYSIS_DATE,'YYYY/MM/DD HH24:MI:SS')
                      AND s.DIALYSIS_DATE <=TO_DATE(:ENDDIALYSIS_DATE,'YYYY/MM/DD HH24:MI:SS')
                      AND s.BANCI_ID =:BANCI_ID
                      AND EXISTS
                      (
                        SELECT 
                            1 
                        FROM MED_USERAREA_MAPPING m 
                        WHERE 
                            s.DIALYSIS_ROOM_ID = m.AREA_ID
                            AND m.USER_ID LIKE '%'||:USER_ID||'%'
                        )
                    ORDER BY 
                      qy.ORDER_NUMBER, TO_NUMBER(M.MACHINE_SERIAL_NO), s.BANCI_ID";
            }
        }

        /// <summary>
        /// 获取病患排班数据SQL 
        /// </summary>
        public string GetPatientScheduleListByPara
        {
            get
            {                
                return @"
                    SELECT s.patient_schedule_id,
                           s.patient_id,
                           s.monitor_label,
                           s.dialysis_date,
                           to_char(s.dialysis_date, 'day') as week,
                           s.banci_id,
                           s.dialysis_room_id,
                           s.bed_number,
                           s.start_time,
                           s.PURIFICATION_MODE,
                           s.end_time,
                           s.status,
                           s.hemodialysis_id,
                           s.remark,
                           s.recipe_id,
                            DECODE(S.START_TIME,
                                      NULL,
                                      S.PURIFIER_MODEL_ID,
                                      FUN_PURIFIER_MODEL_ID(S.RECIPE_ID)) AS PURIFIER_MODEL_ID,
                           s.user_id,
                           s.focus_level,s.IS_CRRT,
                      p.NAME PATIENTNAME, p.SEX, p.INFECTIOUS_CHECK_RESULT, qy.ITEM_NAME AREANAME, cw.ITEM_NAME BEDNAME, m.MACHINE_NAME
                    FROM MED_PATIENT_SCHEDULE s
                    INNER JOIN MED_PATIENTS p ON s.HEMODIALYSIS_ID = p.HEMODIALYSIS_ID
                    INNER JOIN (SELECT * FROM MED_COMMON_ITEMLIST il WHERE il.ITEM_TYPE = '区域' AND il.STATUS = '1') qy ON s.DIALYSIS_ROOM_ID = qy.ITEM_ID  
                    INNER JOIN (SELECT * FROM MED_COMMON_ITEMLIST il WHERE il.ITEM_TYPE = '床位' AND il.STATUS = '1') cw ON s.BED_NUMBER = cw.ITEM_ID
                    INNER JOIN MED_DIALYSIS_MACHINE m ON s.MONITOR_LABEL = m.MACHINE_ID
                    WHERE
                      1 = 1
                      AND (P.is_delete != '1' OR P.IS_DELETE IS NULL)
                      AND s.DIALYSIS_DATE >= :BEGINDIALYSIS_DATE
                      AND s.DIALYSIS_DATE <= :ENDDIALYSIS_DATE
                      AND EXISTS
                      (
                        SELECT 
                            1 
                        FROM MED_USERAREA_MAPPING m 
                        WHERE 
                            s.DIALYSIS_ROOM_ID = m.AREA_ID 
                            AND m.USER_ID LIKE '%'||:USER_ID||'%'
                        )
                    ORDER BY 
                      qy.ORDER_NUMBER, TO_NUMBER(m.MACHINE_SERIAL_NO), s.BANCI_ID";
            }
        }

        /// <summary>
        /// 获取病患排班数据SQL 
        /// </summary>
        public string GetPatientScheduleListByPara2
        {
            get
            {
                return @"
                    SELECT s.patient_schedule_id,
                           s.patient_id,
                           s.monitor_label,
                           s.dialysis_date,
                           to_char(s.dialysis_date, 'day') as week,
                           s.banci_id,
                           s.dialysis_room_id,
                           s.bed_number,
                           s.start_time,
                           s.PURIFICATION_MODE,
                           s.end_time,
                           s.status,
                           s.hemodialysis_id,
                           s.remark,
                           s.recipe_id,
                            DECODE(S.START_TIME,
                                      NULL,
                                      S.PURIFIER_MODEL_ID,
                                      FUN_PURIFIER_MODEL_ID(S.RECIPE_ID)) AS PURIFIER_MODEL_ID,
                           s.user_id,
                           s.focus_level,s.IS_CRRT,
                      p.NAME PATIENTNAME, p.SEX, p.INFECTIOUS_CHECK_RESULT, qy.ITEM_NAME AREANAME, cw.ITEM_NAME BEDNAME, m.MACHINE_NAME
                    FROM MED_PATIENT_SCHEDULE s
                    INNER JOIN MED_PATIENTS p ON s.HEMODIALYSIS_ID = p.HEMODIALYSIS_ID
                    INNER JOIN (SELECT * FROM MED_COMMON_ITEMLIST il WHERE il.ITEM_TYPE = '区域' AND il.STATUS = '1') qy ON s.DIALYSIS_ROOM_ID = qy.ITEM_ID  
                    INNER JOIN (SELECT * FROM MED_COMMON_ITEMLIST il WHERE il.ITEM_TYPE = '床位' AND il.STATUS = '1') cw ON s.BED_NUMBER = cw.ITEM_ID
                    INNER JOIN MED_DIALYSIS_MACHINE m ON s.MONITOR_LABEL = m.MACHINE_ID
                    WHERE
                      1 = 1
                      AND (P.is_delete != '1' OR P.IS_DELETE IS NULL)
                      AND ((s.DIALYSIS_DATE <= :BEGINDIALYSIS_DATE
                      AND s.START_TIME IS NOT NULL
                      AND (s.END_TIME >= :ENDDIALYSIS_DATE OR s.END_TIME IS NULL))
                      OR (s.DIALYSIS_DATE = :BEGINDIALYSIS_DATE AND s.START_TIME IS NULL))
                      AND s.IS_CRRT='1'
                      AND EXISTS
                      (
                        SELECT 
                            1 
                        FROM MED_USERAREA_MAPPING m 
                        WHERE 
                            s.DIALYSIS_ROOM_ID = m.AREA_ID 
                            AND m.USER_ID LIKE '%'||:USER_ID||'%'
                        )
                    ORDER BY 
                      qy.ORDER_NUMBER, s.DIALYSIS_DATE, s.BANCI_ID, cw.ORDER_NUMBER";
            }
        }

        public string GetSchedulePatientLabReport
        {
            get
            {
                return @"SELECT TRUNC(K.DATER) a,
                                   K.PATIENT_ID b,
                                   K.NAME c,
                                   SUM(DECODE(K.ITEM_NAME, '尿素', k.ITEM_RESULT, 0)) d,
                                   SUM(DECODE(K.ITEM_NAME, '肌酐', k.ITEM_RESULT, 0)) e,
                                   SUM(DECODE(K.ITEM_NAME, '尿酸', k.ITEM_RESULT, 0)) f,
                                   SUM(DECODE(K.ITEM_NAME, '总蛋白', k.ITEM_RESULT, 0)) g,
                                   SUM(DECODE(K.ITEM_NAME, '白蛋白', k.ITEM_RESULT, 0)) h,
                                   SUM(DECODE(K.ITEM_NAME, '丙氨酸转氨酶', k.ITEM_RESULT, 0)) i,
                                   SUM(DECODE(K.ITEM_NAME, '碱性磷酸酶', k.ITEM_RESULT, 0)) j,
                                   SUM(DECODE(K.ITEM_NAME, '总胆红素', k.ITEM_RESULT, 0)) k,
                                   SUM(DECODE(K.ITEM_NAME, '直接胆红素', k.ITEM_RESULT, 0)) l,
                                   SUM(DECODE(K.ITEM_NAME, '肌酸磷酸激酶', k.ITEM_RESULT, 0))m,
                                   SUM(DECODE(K.ITEM_NAME, 'CK-MB', k.ITEM_RESULT, 0)) n,
                                   SUM(DECODE(K.ITEM_NAME, '谷草转氨', k.ITEM_RESULT, 0)) o,
                                   SUM(DECODE(K.ITEM_NAME, '乳酸脱氢酶', k.ITEM_RESULT, 0)) p,
                                   SUM(DECODE(K.ITEM_NAME, '谷草转氨', k.ITEM_RESULT, 0)) q,
                                   SUM(DECODE(K.ITEM_NAME, '甘油三酯', k.ITEM_RESULT, 0)) r,
                                   SUM(DECODE(K.ITEM_NAME, '高密度脂蛋白胆固醇', k.ITEM_RESULT, 0)) s,
                                   SUM(DECODE(K.ITEM_NAME, '低密度脂蛋白胆固醇', k.ITEM_RESULT, 0)) t,
                                   SUM(DECODE(K.ITEM_NAME, '钾', k.ITEM_RESULT, 0)) u,
                                   SUM(DECODE(K.ITEM_NAME, '钠', k.ITEM_RESULT, 0)) v,
                                   SUM(DECODE(K.ITEM_NAME, '氯化物', k.ITEM_RESULT, 0)) w,
                                   SUM(DECODE(K.ITEM_NAME, '钙', k.ITEM_RESULT, 0)) x,
                                   SUM(DECODE(K.ITEM_NAME, '镁', k.ITEM_RESULT, 0)) y,
                                   SUM(DECODE(K.ITEM_NAME, '磷', k.ITEM_RESULT, 0)) z,
                                   SUM(DECODE(K.ITEM_NAME, '二氧化碳', k.ITEM_RESULT, 0)) aa,
                                   SUM(DECODE(K.ITEM_NAME, '白细胞计数', k.ITEM_RESULT, 0)) bb,
                                   SUM(DECODE(K.ITEM_NAME, '粒细胞计数', k.ITEM_RESULT, 0)) cc,
                                   SUM(DECODE(K.ITEM_NAME, '嗜酸性细胞百分比', k.ITEM_RESULT, 0)) dd,
                                   SUM(DECODE(K.ITEM_NAME, '嗜酸性细胞数', k.ITEM_RESULT, 0)) ee,
                                   SUM(DECODE(K.ITEM_NAME, '红细胞计数', k.ITEM_RESULT, 0)) ff,
                                   SUM(DECODE(K.ITEM_NAME, '血红蛋白浓度', k.ITEM_RESULT, 0)) gg,
                                   SUM(DECODE(K.ITEM_NAME, '红细胞压积', k.ITEM_RESULT, 0)) hh,
                                   SUM(DECODE(K.ITEM_NAME, '红细胞MCHC', k.ITEM_RESULT, 0)) ii,
                                   SUM(DECODE(K.ITEM_NAME, '血小板计数', k.ITEM_RESULT, 0)) jj,
                                   SUM(DECODE(K.ITEM_NAME, '血小板压积', k.ITEM_RESULT, 0)) kk
                          FROM (SELECT t.SPCM_RECEIVED_DATE_TIME AS DATER,
                                       t.TEST_NO                 AS TEST_NO,
                                       t.PATIENT_ID              AS PATIENT_ID,
                                       t.NAME                    AS NAME,
                                       z.REPORT_ITEM_NAME        AS ITEM_NAME,
                                       z.RESULT                  AS ITEM_RESULT,
                                       z.UNITS                   as ITEM_UNITS
                                  from MED_LAB_TEST_MASTER t, MED_LAB_RESULT z
                                 where t.test_no = z.test_no
                                   AND (VISIT_ID = 1 or VISIT_ID IS NULL or visit_id = 0)
                                   AND RESULT_STATUS >= 4
                                   AND isnumeric(Z.RESULT) > 0
                                   AND Z.RESULT NOT LIKE '%-%'
                                   AND Z.RESULT NOT LIKE '%<%'
                                   AND Z.RESULT NOT LIKE '%>%'
                                   AND TRUNC(T.results_rpt_date_time) >= TRUNC(:BEGINDIALYSIS_DATE)
                                   AND TRUNC(T.results_rpt_date_time) <= TRUNC(:ENDDIALYSIS_DATE)
                                   AND T.PATIENT_ID IN
                                       (SELECT distinct s.patient_id
                                          FROM MED_PATIENT_SCHEDULE s
                                         INNER JOIN MED_PATIENTS p
                                            ON s.HEMODIALYSIS_ID = p.HEMODIALYSIS_ID
                                         WHERE (P.is_delete != '1' OR P.IS_DELETE IS NULL)
                                           AND s.DIALYSIS_DATE >= :BEGINDIALYSIS_DATE
                                           AND s.DIALYSIS_DATE <= :ENDDIALYSIS_DATE
                                           AND (s.BANCI_ID = :BANCI_ID OR s.BANCI_ID ='1' OR s.BANCI_ID = '2' 
                                           OR s.BANCI_ID = '3' OR  s.BANCI_ID = '4')
                                           AND EXISTS
                                         (SELECT 1
                                                  FROM MED_USERAREA_MAPPING m
                                                 WHERE s.DIALYSIS_ROOM_ID = m.AREA_ID
                                                   AND m.USER_ID LIKE '%' || :USER_ID || '%')
                                     )
                                 order by SPCM_RECEIVED_DATE_TIME desc) K

                         GROUP BY K.NAME, K.PATIENT_ID, TRUNC(K.DATER)
                         ORDER BY K.NAME, K.PATIENT_ID, TRUNC(K.DATER)";
            }
        }

        /// <summary>
        /// 检验数据报表导出
        /// </summary>
        public string GetSchedulePatientLabResult
        {
            get
            {
                return @"SELECT TEST_NO,
                               TO_CHAR(TO_DATE(T.DATER,'dd-mm-yy'),'yyyy-mm-dd') DATER,
                               PATIENT_ID,
                               NAME,       D,       E,       F,       G,       H,       I,       J,       K,       L,       M,       N,       O,       P,  
                               Q,       R,       S,       T,       U,       V,       W,       X,       Y,       Z,       AA,       BB,       CC,       DD,
                               EE,       FF,       GG,       HH,       II,       JJ,       KK,       YG1,       YG2,       YG3,       YG4,       YG5,       YG6,
                               YG7,       YG8,       YG9,       YG10,       YG11,       YG12,       BG1,       BG2,       BG3,       MD1,       MD2,
                               MD3,       MD4,       AZ,       FE,       SF,       TIBC,       PTH,       HSCRP                        
                        FROM LAB_RESULT_DATA T WHERE T.DATER >= :BEGINDIALYSIS_DATE
                                   AND T.DATER <= :ENDDIALYSIS_DATE
                                   AND T.PATIENT_ID IN
                                       (SELECT distinct s.patient_id
                                          FROM MED_PATIENT_SCHEDULE s
                                         INNER JOIN (SELECT * FROM MED_PATIENTS WHERE TIME_TYPE LIKE '%' || :PATIENT_TYPE || '%') p
                                            ON s.HEMODIALYSIS_ID = p.HEMODIALYSIS_ID
                                         WHERE (P.is_delete != '1' OR P.IS_DELETE IS NULL) 
                                           AND s.DIALYSIS_DATE >= :BEGINDIALYSIS_DATE
                                           AND s.DIALYSIS_DATE <= :ENDDIALYSIS_DATE
                                           AND s.BANCI_ID = :BANCI_ID OR (s.BANCI_ID = '1' OR s.BANCI_ID ='2' OR s.BANCI_ID='3')
                                           AND EXISTS
                                         (SELECT 1
                                                  FROM MED_USERAREA_MAPPING m
                                                 WHERE s.DIALYSIS_ROOM_ID = m.AREA_ID
                                                   AND m.USER_ID LIKE '%' || :USER_ID || '%')
                                     ) ORDER BY T.DATER,T.NAME";
            }
        }

        /// <summary>
        /// 获取病患排班数据SQL 
        /// </summary>
        public string GetPatientScheduleList4Report
        {
            get
            {
                return @"
                    SELECT 
                      s.*, p.NAME ||s.REMARK  PATIENTNAME, qy.ITEM_NAME AREANAME, cw.ITEM_VALUE BEDNAME, m.MACHINE_NAME
                    FROM MED_PATIENT_SCHEDULE s
                    INNER JOIN MED_PATIENTS p ON s.HEMODIALYSIS_ID = p.HEMODIALYSIS_ID
                    INNER JOIN (SELECT * FROM MED_COMMON_ITEMLIST il WHERE il.ITEM_TYPE = '区域' AND il.STATUS = '1') qy ON s.DIALYSIS_ROOM_ID = qy.ITEM_ID  
                    INNER JOIN (SELECT * FROM MED_COMMON_ITEMLIST il WHERE il.ITEM_TYPE = '床位' AND il.STATUS = '1') cw ON s.BED_NUMBER = cw.ITEM_ID
                    INNER JOIN MED_DIALYSIS_MACHINE m ON s.MONITOR_LABEL = m.MACHINE_ID
                    WHERE
                      1 = 1
                       AND (P.is_delete != '1' OR P.IS_DELETE IS NULL)
                      AND s.DIALYSIS_DATE >= :BEGINDIALYSIS_DATE
                      AND s.DIALYSIS_DATE <= :ENDDIALYSIS_DATE
                    ORDER BY 
                      qy.ORDER_NUMBER, TO_NUMBER(M.MACHINE_SERIAL_NO), s.BANCI_ID,qy.ITEM_NAME";
            }
        }
        public string GetPatientScheduleListReportForJL
        {
            get
            {
                return @"SELECT Z.AREANAME,
                               Z.BEDNAME,
                               MAX(DECODE(Z.WEEK, '星期一', Z.PATIENTNAME)) AS MONDAY,
                               MAX(DECODE(Z.WEEK, '星期二', Z.PATIENTNAME)) AS TUESDAY,
                               MAX(DECODE(Z.WEEK, '星期三', Z.PATIENTNAME)) AS WEDNESDAY,
                               MAX(DECODE(Z.WEEK, '星期四', Z.PATIENTNAME)) AS THURSDAY,
                               MAX(DECODE(Z.WEEK, '星期五', Z.PATIENTNAME)) AS FRIDAY,
                               MAX(DECODE(Z.WEEK, '星期六', Z.PATIENTNAME)) AS SATURDAY,
                               Z.AREAVALUE, Z.BEDVALUE
                          FROM (SELECT S.DIALYSIS_DATE,
                                       TO_CHAR(S.DIALYSIS_DATE, 'DAY') AS WEEK,
                                       P.NAME || ' ' || (SELECT L.ITEM_NAME from MED_COMMON_ITEMLIST L WHERE L.ITEM_ID = S.PURIFICATION_MODE) ||  ' ' || S.REMARK PATIENTNAME,
                                       QY.ITEM_NAME AREANAME,
                                       QY.ITEM_VALUE AREAVALUE,
                                       CW.ITEM_NAME BEDNAME,
                                       CW.ITEM_VALUE BEDVALUE,
                                       M.MACHINE_NAME
                                  FROM MED_PATIENT_SCHEDULE S
                                 INNER JOIN MED_PATIENTS P
                                    ON S.HEMODIALYSIS_ID = P.HEMODIALYSIS_ID
                                 INNER JOIN (SELECT *
                                              FROM MED_COMMON_ITEMLIST IL
                                             WHERE IL.ITEM_TYPE = '区域'
                                               AND IL.STATUS = '1') QY
                                    ON S.DIALYSIS_ROOM_ID = QY.ITEM_ID
                                 INNER JOIN (SELECT *
                                              FROM MED_COMMON_ITEMLIST IL
                                             WHERE IL.ITEM_TYPE = '床位'
                                               AND IL.STATUS = '1') CW
                                    ON S.BED_NUMBER = CW.ITEM_ID
                                 INNER JOIN MED_DIALYSIS_MACHINE M
                                    ON S.MONITOR_LABEL = M.MACHINE_ID
                                 WHERE 1 = 1
                                   AND (P.IS_DELETE != '1' OR P.IS_DELETE IS NULL)
                                   AND S.DIALYSIS_DATE >= :BEGINDIALYSIS_DATE
                                   AND S.DIALYSIS_DATE <= :ENDDIALYSIS_DATE
                                   AND S.BANCI_ID = :BANCI_ID
                                 ORDER BY QY.ORDER_NUMBER,
                                         TO_NUMBER(M.MACHINE_SERIAL_NO),
                                          S.BANCI_ID,
                                          QY.ITEM_NAME) Z
                         GROUP BY Z.AREANAME, Z.BEDNAME, Z.AREAVALUE, Z.BEDVALUE
                         ORDER BY TO_NUMBER(Z.AREAVALUE), TO_NUMBER(Z.BEDVALUE)";
            }
        }
        /// <summary>
        /// 获取相关参数
        /// </summary>
        public string GetCureInfoByHemoID
        {
            get
            {
                return @"SELECT MIN(T.CURE_CREATE_DATE) FIRSTCUREDATE,
                               (SELECT S.INFECTIOUS_CHECK_RESULT
                                  FROM MED_PATIENTS S
                                 WHERE S.HEMODIALYSIS_ID = T.HEMODIALYSIS_ID) INFECTIOUS,
                               (SELECT COUNT(R.PURIFICATION_MODE) HD
                                  FROM MED_HEMO_RECIPE R
                                 WHERE R.HEMODIALYSIS_ID = T.HEMODIALYSIS_ID
                                   AND R.PURIFICATION_MODE =
                                       (SELECT J.ITEM_ID
                                          FROM MED_COMMON_ITEMLIST J
                                         WHERE J.ITEM_TYPE = '净化方式'
                                           AND J.STATUS = '1'
                                           AND J.ITEM_VALUE = 'HD')) HDCOUNT,
                               (SELECT COUNT(R.PURIFICATION_MODE) HF
                                  FROM MED_HEMO_RECIPE R
                                 WHERE R.HEMODIALYSIS_ID = T.HEMODIALYSIS_ID
                                   AND R.PURIFICATION_MODE =
                                       (SELECT J.ITEM_ID
                                          FROM MED_COMMON_ITEMLIST J
                                         WHERE J.ITEM_TYPE = '净化方式'
                                           AND J.STATUS = '1'
                                           AND J.ITEM_VALUE = 'HF')) HFCOUNT
                          FROM MED_CURE_MAIN T
                         WHERE T.HEMODIALYSIS_ID = :HEMODIALYSIS_ID
                         GROUP BY T.HEMODIALYSIS_ID";
            }
        }

        /// <summary>
        /// 获取相关参数
        /// </summary>
        public string GetSCHEDULEInfoByHemoID
        {
            get
            {
                return @"SELECT DECODE(TO_CHAR(T.DIALYSIS_DATE, 'D'),
                                       '1',
                                      '周日',
                                      '2',
                                      '周一',
                                      '3',
                                      '周二',
                                      '4',
                                      '周三',
                                      '5',
                                      '周四',
                                      '6',
                                      '周五',
                                      '周六') WEEK,
                               DECODE(T.BANCI_ID, '1', '早班', '2', '午班', '3', '晚班', '急诊') BACHI,
                               (SELECT L.ITEM_NAME
                                  FROM MED_COMMON_ITEMLIST L
                                 WHERE L.ITEM_ID = T.DIALYSIS_ROOM_ID) ROOM,
                               (SELECT L.ITEM_NAME
                                  FROM MED_COMMON_ITEMLIST L
                                 WHERE L.ITEM_ID = T.BED_NUMBER) BED
                          FROM MED_PATIENT_SCHEDULE T
                         WHERE T.HEMODIALYSIS_ID = :HEMODIALYSIS_ID
                           AND T.DIALYSIS_DATE IN (SELECT WDATE
                                                     FROM (SELECT TO_CHAR(SYSDATE, 'IW') SWEEK,
                                                                  SYSDATE SDATE,
                                                                  TO_CHAR(SYSDATE - 7 + LEVEL, 'IW') WWEEK,
                                                                  TRUNC(SYSDATE) - 7 + LEVEL WDATE
                                                             FROM DUAL
                                                           CONNECT BY LEVEL <= 14)
                                                    WHERE SWEEK = WWEEK)";
            }
        }

        /// <summary>
        /// 根据条件，获取病患排班数据SQL 
        /// </summary>
        public string GetPatientScheduleByParames
        {
            get
            {
                return @"
                    SELECT 
                      s.*, p.NAME,p.DIAGNOSE,qy.ITEM_NAME AREANAME, cw.ITEM_NAME BEDNAME, m.MACHINE_NAME
                    FROM MED_PATIENT_SCHEDULE s
                    INNER JOIN MED_PATIENTS p ON s.HEMODIALYSIS_ID = p.HEMODIALYSIS_ID
                    INNER JOIN (SELECT * FROM MED_COMMON_ITEMLIST il WHERE il.ITEM_TYPE = '区域' AND il.STATUS = '1') qy ON s.DIALYSIS_ROOM_ID = qy.ITEM_ID  
                    INNER JOIN (SELECT * FROM MED_COMMON_ITEMLIST il WHERE il.ITEM_TYPE = '床位' AND il.STATUS = '1') cw ON s.BED_NUMBER = cw.ITEM_ID
                    INNER JOIN MED_DIALYSIS_MACHINE m ON s.MONITOR_LABEL = m.MACHINE_ID
                    WHERE
                      1 = 1
                      AND (P.is_delete != '1' OR P.IS_DELETE IS NULL)
                      AND s.DIALYSIS_DATE >= :BEGINDIALYSIS_DATE
                      AND s.DIALYSIS_DATE <= :ENDDIALYSIS_DATE
                      AND m.AREA_ID LIKE '%'||:AREA_ID||'%'
                      AND s.BANCI_ID LIKE '%'||:BANCI_ID||'%'
                    ORDER BY 
                      qy.ORDER_NUMBER, TO_NUMBER(M.MACHINE_SERIAL_NO), s.BANCI_ID";
            }

        }

        /// <summary>
        /// 根据排班日期得到排班表数据 
        /// </summary>
        public string GetPatientListBySchedule
        {
            get
            {
                return @"
              select distinct t.*,
                j.item_name       as purification_mode_name,  --净化方式
                r.DRY_WEIGHT, --干体重
                r.ufr, --预计脱水
                r.first_drug_dosage || k1.item_name as first_drug_dosage_name , --首剂
                l.item_name as purifier_model_name,r.frequency_hours,r.frequency_minute from
              (SELECT 
              p.*,s.recipe_id,s.banci_id,s.purifier_model_id,s.dialysis_date,m.machine_model,m.area_id,s.FOCUS_LEVEL,s.bed_number,qy.item_value area_value,
              decode(qy.item_value,'001','1室','002','2室','003','3室','004','4室','005','5室','006','6室','007','7室','008','8室','009','9室','010','CRRT室') as area_name
              FROM MED_PATIENT_SCHEDULE s
              INNER JOIN MED_PATIENTS p ON s.HEMODIALYSIS_ID = p.HEMODIALYSIS_ID
              INNER JOIN (SELECT * FROM MED_COMMON_ITEMLIST il WHERE il.ITEM_TYPE = '区域' AND il.STATUS = '1') qy ON s.DIALYSIS_ROOM_ID = qy.ITEM_ID  
              INNER JOIN (SELECT * FROM MED_COMMON_ITEMLIST il WHERE il.ITEM_TYPE = '床位' AND il.STATUS = '1') cw ON s.BED_NUMBER = cw.ITEM_ID
              INNER JOIN MED_DIALYSIS_MACHINE m ON s.MONITOR_LABEL = m.MACHINE_ID
              ) t
              left join (select * from MED_HEMO_RECIPE r where r.RECIPE_DATE like to_date(:DIALYSIS_DATE,'yyyy-mm-dd') and r.recipe_type='0' and r.status !='2')  r on r.hemodialysis_id = t.hemodialysis_id
              left join (SELECT * FROM MED_COMMON_ITEMLIST j WHERE (j.ITEM_TYPE = '净化方式' OR j.ITEM_TYPE = 'CRRT净化方式') AND j.STATUS = '1') j
              on r.purification_mode = j.item_id
              left join (SELECT * FROM MED_COMMON_ITEMLIST l WHERE l.ITEM_TYPE = '净化器类型' AND l.STATUS = '1') l
              on r.first_purifier_model =  l.item_id
              left join (SELECT * FROM MED_COMMON_ITEMLIST l) k1
              on r.first_drug_unit  = k1.item_id 
                WHERE
                1 = 1
                AND (t.is_delete != '1' OR T.IS_DELETE IS NULL)
                AND t.DIALYSIS_DATE like to_date(:DIALYSIS_DATE,'yyyy-mm-dd')
                AND t.AREA_ID LIKE '%'||:AREA_ID||'%'
                AND t.BANCI_ID LIKE '%'||:BANCI_ID||'%'                   
                ORDER BY T.NAME--  purification_mode_name, t.BANCI_ID,t.bed_number";
            }
        }
        public string GetPatientPicByHemoId
        {
            get
            {
                return @"SELECT * FROM MED_PATIENTS_PIC T WHERE T.HEMODIALYSIS_ID=:HEMODIALYSIS_ID";
            }
        }
        /// <summary>
        /// 根据排班日期、患者姓名得到排班表数据 
        /// </summary>
        public string GetPatientListByScheduleAndName
        {
            get
            {
                return @"
              select distinct t.*,j.item_name as purification_mode_name,l.item_name as purifier_model_name,r.frequency_hours from
              (SELECT 
              p.*,'' as recipe_id,s.banci_id,s.purifier_model_id,s.dialysis_date,m.machine_model,m.area_id,s.FOCUS_LEVEL,s.bed_number,
              decode(qy.item_value,'001','1室','002','2室','003','3室','004','4室','005','5室','006','6室','007','7室','008','8室','009','9室','010','CRRT室') as area_name
              FROM MED_PATIENT_SCHEDULE s
              INNER JOIN MED_PATIENTS p ON s.HEMODIALYSIS_ID = p.HEMODIALYSIS_ID
              INNER JOIN (SELECT * FROM MED_COMMON_ITEMLIST il WHERE il.ITEM_TYPE = '区域' AND il.STATUS = '1') qy ON s.DIALYSIS_ROOM_ID = qy.ITEM_ID  
              INNER JOIN (SELECT * FROM MED_COMMON_ITEMLIST il WHERE il.ITEM_TYPE = '床位' AND il.STATUS = '1') cw ON s.BED_NUMBER = cw.ITEM_ID
              INNER JOIN MED_DIALYSIS_MACHINE m ON s.MONITOR_LABEL = m.MACHINE_ID
              ) t
              left join (select * from MED_HEMO_RECIPE r where r.RECIPE_DATE like to_date(:DIALYSIS_DATE,'yyyy-mm-dd') and r.recipe_type='0' and r.status !='2')  r on r.hemodialysis_id = t.hemodialysis_id
              left join (SELECT * FROM MED_COMMON_ITEMLIST j WHERE (j.ITEM_TYPE = '净化方式' OR j.ITEM_TYPE = 'CRRT净化方式') AND j.STATUS = '1') j
              on r.purification_mode = j.item_id
              left join (SELECT * FROM MED_COMMON_ITEMLIST l WHERE l.ITEM_TYPE = '净化器类型' AND l.STATUS = '1') l
              on r.first_purifier_model =  l.item_id
                WHERE
                1 = 1
                AND (t.is_delete != '1' OR T.IS_DELETE IS NULL)
                AND t.DIALYSIS_DATE like to_date(:DIALYSIS_DATE,'yyyy-mm-dd')
                AND t.AREA_ID LIKE '%'||:AREA_ID||'%'
                AND t.BANCI_ID LIKE '%'||:BANCI_ID||'%'
                AND (t.NAME like '%'||:NAME||'%' OR upper(t.INPUT_CODE) LIKE upper('%'||:NAME||'%'))                   
                ORDER BY T.NAME--  purification_mode_name, t.BANCI_ID,t.bed_number";
            }
        }

        /// <summary>
        /// 删除病患排班数据SQL
        /// </summary>
        public string DeletePatientSchedule
        {
            get
            {
                return @"
                    DELETE FROM MED_PATIENT_SCHEDULE t
                    WHERE
                        t.DIALYSIS_DATE >= :BEGINDIALYSIS_DATE
                        AND t.DIALYSIS_DATE <= :ENDDIALYSIS_DATE
                        AND t.BANCI_ID = :BANCI_ID
                        AND EXISTS
                        (
                            SELECT 
                                1 
                            FROM MED_USERAREA_MAPPING m 
                            WHERE 
                                t.DIALYSIS_ROOM_ID = m.AREA_ID 
                                AND m.USER_ID = :USER_ID
                        )
                        AND (t.START_TIME IS NULL AND t.END_TIME IS NULL)
                        AND (T.RECIPE_ID IS NULL AND T.PURIFIER_MODEL_ID IS NULL)";
            }
        }

        public string DeletePatientScheduleDateTemp
        {
            get
            {
                return @"DELETE FROM MED_PATIENT_SCHEDULE_TEMP_DATA WHERE PATIENT_SCHEDULE_TEMPLATE_ID = :PATIENT_SCHEDULE_TEMPLATE_ID";
            }
        }
        public string DeletePatientScheduleDateByID
        {
            get
            {
                return @"DELETE FROM MED_PATIENT_SCHEDULE T WHERE T.PATIENT_SCHEDULE_ID = :PATIENT_SCHEDULE_ID";
            }
        }
        /// <summary>
        /// 根据模板ID获取病患排班数据SQL
        /// </summary>
        public string GetPatientScheduleListByTemplateID
        {
            get
            {
                return @"
                    SELECT 
                        MED_SCHEDULE_TEMPLATE_DATA_ID PATIENT_SCHEDULE_ID, NULL START_TIME, NULL END_TIME, t.* 
                    FROM MED_PATIENT_SCHEDULE_TEMP_DATA t
                    WHERE
                        1 = 1                                              
                        AND t.PATIENT_SCHEDULE_TEMPLATE_ID = :PATIENT_SCHEDULE_TEMPLATE_ID";
            }
        }

        /// <summary>
        /// 获取病患排班模板数据SQL 
        /// </summary>
        public string GetPatientScheduleTemplateList
        {
            get
            {
                return @"
                    SELECT 
                      * 
                    FROM MED_PATIENT_SCHEDULE_TEMPLATE t 
                    WHERE 
                      EXISTS
                      (
                        SELECT 
                          1 
                        FROM MED_PATIENT_SCHEDULE_TEMP_DATA td
                        WHERE 
                          td.BANCI_ID = :BANCI_ID 
                          AND td.PATIENT_SCHEDULE_TEMPLATE_ID = t.PATIENT_SCHEDULE_TEMPLATE_ID
                      ) 
                    ORDER BY t.PATIENT_SCHEDULE_TEMPLATE_NAME";
            }
        }
        /// <summary>
        /// 获取病患排班模板数据SQL 
        /// </summary>
        public string GetPatientScheduleAllTemplateList
        {
            get
            {
                return @"
                    SELECT 
                      * 
                    FROM MED_PATIENT_SCHEDULE_TEMPLATE t 
                    WHERE 
                      EXISTS
                      (
                        SELECT 
                          1 
                        FROM MED_PATIENT_SCHEDULE_TEMP_DATA td
                        WHERE  td.PATIENT_SCHEDULE_TEMPLATE_ID = t.PATIENT_SCHEDULE_TEMPLATE_ID
                      ) 
                    ORDER BY t.PATIENT_SCHEDULE_TEMPLATE_NAME";
            }
        }
        public string GetPatientScheduleTempDataList
        {
            get
            {
                return @"SELECT * FROM MED_PATIENT_SCHEDULE_TEMP_DATA WHERE PATIENT_SCHEDULE_TEMPLATE_ID = :PATIENT_SCHEDULE_TEMPLATE_ID";
            }
        }
        public string GetPatientScheduleTempDataListNew
        {
            get
            {
                return @"SELECT T.*,
                               (SELECT S.NAME FROM MED_PATIENTS S WHERE S.Hemodialysis_Id = T.Hemodialysis_Id) as PATIENTNAME
                          FROM MED_PATIENT_SCHEDULE_TEMP_DATA T
                         WHERE T.PATIENT_SCHEDULE_TEMPLATE_ID = :PATIENT_SCHEDULE_TEMPLATE_ID";
            }
        }
        /// <summary>
        /// 根据排班表透析ID和开始时间得到一条处方ID
        /// </summary>
        public string GetPatientScheduleRecipeIDByStartTime
        {
            get
            {
                return @"select * from med_patient_schedule  where hemodialysis_id=:HEMODIALYSIS_ID 
                          and start_time like to_date(:START_TIME,'yyyy-mm-dd')";
            }
        }

        public string QueryPatientScheduleByParam
        {
            get
            {
                return @"SELECT t.patient_schedule_id, t.hemodialysis_id,
                                   t.patient_id,
                                   S.NAME PATIENTNAME,
                                   t.dialysis_date,
                                   decode(t.banci_id, '1', utl_raw.cast_to_varchar2('C9CFCEE7'), '2', utl_raw.cast_to_varchar2('CFC2CEE7'), utl_raw.cast_to_varchar2('CDEDC9CF')) BANCI_ID,
                                   (select l.item_name
                                      from med_common_itemlist l
                                     where l.item_id = t.dialysis_room_id) AREANAME,
                                   (select l.item_name
                                      from med_common_itemlist l
                                     where l.item_id = t.bed_number) BEDNAME
                              FROM MED_PATIENT_SCHEDULE T, med_patients S
                             WHERE T.HEMODIALYSIS_ID = S.HEMODIALYSIS_ID
                               AND (T.HEMODIALYSIS_ID LIKE '%' || :PATIENTS || '%' OR
                                   T.PATIENT_ID LIKE '%' || :PATIENTS || '%' OR
                                   S.NAME LIKE '%' || :PATIENTS || '%' OR S.INPUT_CODE  LIKE '%' || :PATIENTS || '%')
                               AND T.BANCI_ID LIKE '%' || :BANCI_ID || '%'
                               AND T.DIALYSIS_ROOM_ID LIKE '%' || :DIALYSIS_ROOM_ID || '%'
                               AND T.DIALYSIS_DATE IN (
                           
                                                       SELECT wdate
                                                         FROM (SELECT to_char(SYSDATE, 'iw') sweek,
                                                                       SYSDATE sdate,
                                                                       to_char(SYSDATE - 7 + LEVEL, 'iw') wweek,
                                                                       TRUNC(SYSDATE) - 7 + LEVEL wdate
                                                                  FROM dual
                                                                CONNECT BY LEVEL <= 14)
                                                        WHERE sweek = wweek)";
            }
        }

        public string GetSchedulePatientCount
        {
            get
            {
                return @"SELECT SUM(ALLS) ALLS, SUM(HD) HD, SUM(HDF) HDF
                              FROM (select count(*) alls, 0 HD, 0 HDF
                                      from med_patient_schedule t
                                     where T.BANCI_ID LIKE '%' || :BANCI_ID || '%'
                                       AND T.DIALYSIS_ROOM_ID LIKE '%' || :DIALYSIS_ROOM_ID || '%'
                                       AND T.DIALYSIS_DATE IN
                                           (
                
                                            SELECT wdate
                                              FROM (SELECT to_char(SYSDATE, 'iw') sweek,
                                                            SYSDATE sdate,
                                                            to_char(SYSDATE - 7 + LEVEL, 'iw') wweek,
                                                            TRUNC(SYSDATE) - 7 + LEVEL wdate
                                                       FROM dual
                                                     CONNECT BY LEVEL <= 14)
                                             WHERE sweek = wweek)
                                    union
                                    select 0 alls, COUNT(*) HD, 0 HDF
                                      from med_patient_schedule t
                                     where T.REMARK = 'HD'
                                       AND T.BANCI_ID LIKE '%' || :BANCI_ID || '%'
                                       AND T.DIALYSIS_ROOM_ID LIKE '%' || :DIALYSIS_ROOM_ID || '%'
                                       AND T.DIALYSIS_DATE IN
                                           (
                
                                            SELECT wdate
                                              FROM (SELECT to_char(SYSDATE, 'iw') sweek,
                                                            SYSDATE sdate,
                                                            to_char(SYSDATE - 7 + LEVEL, 'iw') wweek,
                                                            TRUNC(SYSDATE) - 7 + LEVEL wdate
                                                       FROM dual
                                                     CONNECT BY LEVEL <= 14)
                                             WHERE sweek = wweek)
                                    UNION
                                    select 0 alls, 0 HD, COUNT(*) HDF
                                      from med_patient_schedule t
                                     where T.REMARK = 'HDF'
                                       AND T.BANCI_ID LIKE '%' || :BANCI_ID || '%'
                                       AND T.DIALYSIS_ROOM_ID LIKE '%' || :DIALYSIS_ROOM_ID || '%'
                                       AND T.DIALYSIS_DATE IN
                                           (
                
                                            SELECT wdate
                                              FROM (SELECT to_char(SYSDATE, 'iw') sweek,
                                                            SYSDATE sdate,
                                                            to_char(SYSDATE - 7 + LEVEL, 'iw') wweek,
                                                            TRUNC(SYSDATE) - 7 + LEVEL wdate
                                                       FROM dual
                                                     CONNECT BY LEVEL <= 14)
                                             WHERE sweek = wweek))";
            }
        }

        /// <summary>
        /// 获取服务端日期
        /// </summary>
        public string GetServerDate
        {
            get
            {
                return @"SELECT SYSDATE FROM DUAL";
            }
        }

        public string GetPurificationModeCountByParam
        {
            get
            {
                return @"SELECT DECODE(T.BANCI_ID, '1', '上午', '2', '下午', '3', '急诊') as banchi,
                                DECODE((SELECT L.ITEM_NAME
                                        FROM MED_COMMON_ITEMLIST L
                                       WHERE L.ITEM_ID = T.PURIFICATION_MODE),
                                      '',
                                      '其它',
                                      (SELECT L.ITEM_NAME
                                         FROM MED_COMMON_ITEMLIST L
                                        WHERE L.ITEM_ID = T.PURIFICATION_MODE)) AS PURIFICATION_MODE_NAME,
                               COUNT(1) as count
                          FROM MED_PATIENT_SCHEDULE T
                          WHERE T.DIALYSIS_DATE = TRUNC(:DIALYSIS_DATE)
                         GROUP BY T.BANCI_ID, T.PURIFICATION_MODE
                         ORDER BY T.BANCI_ID";
            }

        }

        public string GetCureCountByParam
        {
            get
            {
                return @"SELECT DECODE(T.BANCI_ID, '1', '上午', '2', '下午', '3', '晚班','急诊') as banchi,
                               (SELECT COUNT(1)
                                  FROM MED_PATIENT_SCHEDULE T1
                                 WHERE T1.DIALYSIS_DATE = T.DIALYSIS_DATE
                                   AND T1.BANCI_ID = T.BANCI_ID
                                   AND T1.START_TIME IS NULL) AS a,
                               (SELECT COUNT(1)
                                  FROM MED_PATIENT_SCHEDULE T1
                                 WHERE T1.DIALYSIS_DATE = T.DIALYSIS_DATE
                                   AND T1.BANCI_ID = T.BANCI_ID
                                   AND T1.START_TIME IS NOT NULL
                                   AND T1.END_TIME IS NULL) AS b,
                               (SELECT COUNT(1)
                                  FROM MED_PATIENT_SCHEDULE T1
                                 WHERE T1.DIALYSIS_DATE = T.DIALYSIS_DATE
                                   AND T1.BANCI_ID = T.BANCI_ID
                                   AND T1.END_TIME IS NOT NULL) AS c
                          FROM MED_PATIENT_SCHEDULE T
                          WHERE T.DIALYSIS_DATE = TRUNC(:DIALYSIS_DATE)
                         GROUP BY T.BANCI_ID, T.DIALYSIS_DATE
                         ORDER BY T.BANCI_ID";
            }
        }
        public string GetQuerySchedulePatientInfo
        {
            get
            {
                return @"SELECT T.DIALYSIS_DATE,  T.HEMODIALYSIS_ID,(SELECT L.ITEM_VALUE
                              FROM MED_COMMON_ITEMLIST L
                             WHERE L.ITEM_ID = T.DIALYSIS_ROOM_ID) AREAID,
                           (SELECT L.ITEM_NAME
                              FROM MED_COMMON_ITEMLIST L
                             WHERE L.ITEM_ID = T.DIALYSIS_ROOM_ID) AREANAME,
                           (SELECT L.ITEM_NAME
                              FROM MED_COMMON_ITEMLIST L
                             WHERE L.ITEM_VALUE = T.BANCI_ID
                               AND L.ITEM_TYPE = '班次') BANCHINAME,
                           (SELECT L.ITEM_VALUE
                              FROM MED_COMMON_ITEMLIST L
                             WHERE L.ITEM_VALUE = T.BANCI_ID
                               AND L.ITEM_TYPE = '班次') BANCHIID,
                           (SELECT P.NAME
                              FROM MED_PATIENTS P
                             WHERE P.HEMODIALYSIS_ID = T.HEMODIALYSIS_ID) PATIENTNAME,
                           (SELECT L.ITEM_NAME
                              FROM MED_COMMON_ITEMLIST L
                             WHERE L.ITEM_ID = T.BED_NUMBER) BEDNAME,
                           (SELECT L.ITEM_NAME
                              FROM MED_COMMON_ITEMLIST L
                             WHERE L.ITEM_ID =
                                   (SELECT N.PURIFICATION_MODE
                                      FROM MED_HEMO_RECIPE N
                                     WHERE N.HEMODIALYSIS_ID = T.HEMODIALYSIS_ID AND N.RECIPE_TYPE='0'  AND N.STATUS='0'
                                       AND TRUNC(N.RECIPE_DATE) = TRUNC(:QUERYDATE))) PURIFICATION_MODE,
                           (SELECT L.ITEM_NAME
                              FROM MED_COMMON_ITEMLIST L
                             WHERE L.ITEM_ID =
                                   (SELECT N.FIRST_PURIFIER_MODEL
                                      FROM MED_HEMO_RECIPE N
                                     WHERE N.HEMODIALYSIS_ID = T.HEMODIALYSIS_ID AND N.RECIPE_TYPE='0'  AND N.STATUS='0'
                                       AND TRUNC(N.RECIPE_DATE) = TRUNC(:QUERYDATE))) MACHINE_TYPE,
                           (SELECT L.ITEM_NAME
                              FROM MED_COMMON_ITEMLIST L
                             WHERE L.ITEM_ID =
                                   (SELECT N.THERAPEUTIC_METHOD
                                      FROM MED_HEMO_RECIPE N
                                     WHERE N.HEMODIALYSIS_ID = T.HEMODIALYSIS_ID AND N.RECIPE_TYPE='0'  AND N.STATUS='0'
                                       AND TRUNC(N.RECIPE_DATE) = TRUNC(:QUERYDATE))) ||
                           (SELECT N.FIRST_DRUG_DOSAGE
                              FROM MED_HEMO_RECIPE N
                             WHERE N.HEMODIALYSIS_ID = T.HEMODIALYSIS_ID AND N.RECIPE_TYPE='0'  AND N.STATUS='0'
                               AND TRUNC(N.RECIPE_DATE) = TRUNC(:QUERYDATE)) ||
                           (SELECT L.ITEM_NAME
                              FROM MED_COMMON_ITEMLIST L
                             WHERE L.ITEM_ID =
                                   (SELECT N.FIRST_DRUG_UNIT
                                      FROM MED_HEMO_RECIPE N
                                     WHERE N.HEMODIALYSIS_ID = T.HEMODIALYSIS_ID AND N.RECIPE_TYPE='0'  AND N.STATUS='0'
                                       AND TRUNC(N.RECIPE_DATE) = TRUNC(:QUERYDATE))) ||
                             (SELECT DECODE(N.SECOND_DRUG_DOSAGE,
                                          '',
                                          '',
                                          ' 追加' || N.SECOND_DRUG_DOSAGE)
                              FROM MED_HEMO_RECIPE N
                             WHERE N.HEMODIALYSIS_ID = T.HEMODIALYSIS_ID
                               AND N.RECIPE_TYPE = '0'  AND N.STATUS = '0'
                               AND TRUNC(N.RECIPE_DATE) = TRUNC(:QUERYDATE)) ||
                           (SELECT L.ITEM_NAME
                              FROM MED_COMMON_ITEMLIST L
                             WHERE L.ITEM_ID =
                                   (SELECT N.SECOND_DRUG_UNIT
                                      FROM MED_HEMO_RECIPE N
                                     WHERE N.HEMODIALYSIS_ID = T.HEMODIALYSIS_ID
                                       AND N.RECIPE_TYPE = '0'  AND N.STATUS='0'
                                       AND TRUNC(N.RECIPE_DATE) = TRUNC(:QUERYDATE))) AS FIRST_HEPARIN,
                           (SELECT L.ITEM_NAME
                              FROM MED_COMMON_ITEMLIST L
                             WHERE L.ITEM_ID =
                                   (SELECT S.VASCULAR_ACCESS_TYPE
                                          FROM MED_HEMO_RECIPE N LEFT JOIN MED_VASCULAR_ACCESS S ON N.VASCULAR_ACCESS_ID= S.VASCULAR_ACCESS_ID
                                     WHERE N.HEMODIALYSIS_ID = T.HEMODIALYSIS_ID AND N.RECIPE_TYPE='0'  AND N.STATUS='0'
                                       AND TRUNC(N.RECIPE_DATE) = TRUNC(:QUERYDATE))) AS VASCULAR_ACCESS_ID,
                           T.REMARK
                      FROM MED_PATIENT_SCHEDULE T
                     WHERE T.DIALYSIS_DATE = TRUNC(:QUERYDATE)
                     ORDER BY AREAID, BANCHIID, BEDNAME";
            }
        }
        public string GetCurrentDateNurseDuty
        {
            get
            {
                return @"SELECT D.NAME
                          FROM MED_STAFF_DICT D
                         WHERE D.EMP_NO =
                               (SELECT DISTINCT (T.USER_ID)
                                  FROM MED_USERS_WEEKDUTY T
                                 WHERE TRUNC(T.DUTYDAY) = TRUNC(:DUTYDAY)
                                   AND ROWNUM = 1
                                   AND T.OFFICEID = (SELECT L.ITEM_ID
                                                       FROM MED_COMMON_ITEMLIST L
                                                      WHERE L.ITEM_TYPE = '假期类型'
                                                        AND L.ITEM_NAME = '急诊'))";
            }
        }
        public string GetSchedulePatientCheck
        {
            get
            {
                return @"SELECT Z.*, RANK() OVER(PARTITION BY Z.ROMMNAME ORDER BY TO_NUMBER(Z.CHECKNUM)) RANKER
                              FROM (SELECT T.HEMODIALYSIS_ID,
                                           T.CHECKDATE,
                                           T.CHECKNUM,
                                           T1.DIALYSIS_ROOM_ID,
                                           T1.BED_NUMBER,
                                           T2.ITEM_NAME  ROMMNAME,
                                           T3.ITEM_NAME  BEDNAME,
                                           T3.ORDER_NUMBER
                                      FROM MED_PATIENTS_CHECKIN T
                                      LEFT JOIN MED_PATIENT_SCHEDULE T1
                                        ON T.HEMODIALYSIS_ID = T1.HEMODIALYSIS_ID
                                      LEFT JOIN MED_COMMON_ITEMLIST T2
                                        ON T1.DIALYSIS_ROOM_ID = T2.ITEM_ID
                                      LEFT JOIN MED_COMMON_ITEMLIST T3
                                        ON T1.BED_NUMBER = T3.ITEM_ID
                                     WHERE TRUNC(T.CHECKDATE) = TRUNC(:CHECKDATE)
                                       AND T1.DIALYSIS_DATE = TRUNC(:CHECKDATE)
                                       AND T.BANCHI = :BANCHI
                                       AND CHECKNUM =
                                           (select MIN(TO_NUMBER(CHECKNUM))
                                              from MED_PATIENTS_CHECKIN K
                                             where TRUNC(K.CHECKDATE) = TRUNC(T.CHECKDATE)
                                               AND K.BANCHI = :BANCHI
                                               AND K.HEMODIALYSIS_ID = T.HEMODIALYSIS_ID)
                                     ORDER BY T2.ORDER_NUMBER) Z
                             ORDER BY Z.ORDER_NUMBER, Z.CHECKNUM";
            }
        }
        public string UpdatePatientRecipePurificationModeBydate
        {
            get
            {
                return @"UPDATE MED_HEMO_RECIPE P
                                   SET P.PURIFICATION_MODE   =
                                       (SELECT DECODE(L.PURIFICATION_MODE,
                                                      NULL,
                                                      '9c01f053-ad09-4873-b68f-b96d03b8572f',
                                                      L.PURIFICATION_MODE)
                                          FROM MED_PATIENT_SCHEDULE L
                                         WHERE L.HEMODIALYSIS_ID = P.HEMODIALYSIS_ID
                                           AND ROWNUM = 1
                                           AND TRUNC(L.DIALYSIS_DATE) = TRUNC(P.RECIPE_DATE))
                                 WHERE TRUNC(P.RECIPE_DATE) = TRUNC(:RECIPEDATE)
                                   AND P.STATUS = '0'
                                   AND P.RECIPE_TYPE = '0'";
            }
        }

        /// <summary>
        /// 根据透析编号获取患者当周排班信息
        /// </summary>
        public string GetCurrentScheduleInfoByHemoId
        {
            get
            {
                return @"SELECT DECODE(TRIM(TO_CHAR(T.DIALYSIS_DATE,'DAY')),'MONDAY','周一','TUESDAY','周二','WEDNESDAY','周三','THURSDAY','周四','FRIDAY','周五','SATURDAY','周六','SUNDAY','周日') WEEK,
                DECODE(T.BANCI_ID,'1','上午','2','下午','3','晚班') CLASS_NAME,
                DECODE(A.ITEM_VALUE,'001','1室','002','2室','003','3室','004','4室','005','5室','006','6室','007','7室','008','8室','009','9室','010','CRRT室') ROOM,
                B.ITEM_VALUE||'床' BED
                FROM MED_PATIENT_SCHEDULE T
                LEFT JOIN MED_COMMON_ITEMLIST A ON T.DIALYSIS_ROOM_ID=A.ITEM_ID
                LEFT JOIN MED_COMMON_ITEMLIST B ON T.BED_NUMBER=B.ITEM_ID
                WHERE T.HEMODIALYSIS_ID=:HEMODIALYSIS_ID AND TRUNC(T.DIALYSIS_DATE)>=TRUNC(SYSDATE,'IW') AND TRUNC(T.DIALYSIS_DATE)<=TRUNC(SYSDATE,'IW')+6
                ORDER BY T.DIALYSIS_DATE";
            }
        }

        /// <summary>
        /// 根据透析编号获取患者当周排班信息
        /// </summary>
        public string GetCurrentScheduleInfoByHemoIdAndDate
        {
            get
            {
                return @"SELECT DECODE(TRIM(TO_CHAR(T.DIALYSIS_DATE,'DAY')),'MONDAY','周一','TUESDAY','周二','WEDNESDAY','周三','THURSDAY','周四','FRIDAY','周五','SATURDAY','周六','SUNDAY','周日') WEEK,
                DECODE(T.BANCI_ID,'1','上午','2','下午','3','晚班') CLASS_NAME,
                DECODE(A.ITEM_VALUE,'001','1室','002','2室','003','3室','004','4室','005','5室','006','6室','007','7室','008','8室','009','9室','010','CRRT室') ROOM,
                B.ITEM_VALUE||'床' BED
                FROM MED_PATIENT_SCHEDULE T
                LEFT JOIN MED_COMMON_ITEMLIST A ON T.DIALYSIS_ROOM_ID=A.ITEM_ID
                LEFT JOIN MED_COMMON_ITEMLIST B ON T.BED_NUMBER=B.ITEM_ID
                WHERE T.HEMODIALYSIS_ID=:HEMODIALYSIS_ID AND TRUNC(T.DIALYSIS_DATE)>=TRUNC(SYSDATE,'IW') AND TRUNC(T.DIALYSIS_DATE)<=TRUNC(SYSDATE,'IW')+6
                ORDER BY T.DIALYSIS_DATE";
            }
        }

        /// <summary>
        /// 根据床号获取最新的排班记录
        /// </summary>
        public string GetLatestScheduleByBedNumber
        {
            get
            {
                return @"SELECT T.* FROM (SELECT T.* FROM MED_PATIENT_SCHEDULE T WHERE T.BED_NUMBER=:BED_NUMBER
                        ORDER BY T.DIALYSIS_DATE DESC,T.BANCI_ID DESC) T WHERE ROWNUM=1";
            }
        }

        #endregion

        #region 治疗单

        public string GetCleanUpTimes
        {
            get
            {
                return @"select decode(max(t.Clean_Up_Times) + 1, '', 1, max(t.Clean_Up_Times) + 1)
                          from MED_CURE_MAIN t
                         where t.HEMODIALYSIS_ID = :HEMODIALYSIS_ID AND T.CURE_STATUS != '4'";
            }
        }

        /// <summary>
        /// 根据治疗单编号得到对应治疗单数据SQL
        /// </summary>
        public string GetMainCureByCureID
        {
            get
            {
                //SELECT * FROM MED_CURE_MAIN WHERE CURE_ID = :CURE_ID
                return @"SELECT T.CURE_CREATE_DATE,
                           T.CURE_ID,
                           T.RECIPE_ID,
                           T.HEMODIALYSIS_ID,
                           T.RECIPE_TYPE,
                           T.CALCIUM_ION,
                           T.CURE_STATUS,
                           T.DOCTOR_ID,
                           T.RECIPE_DATE,
                           T.BLOOW_FLOW,
                           T.DIALYSATE_FLOW,
                           T.DIALYSATE_TEMPERATURE,
                           T.UFR,
                           T.SODION,
                           T.POTASSIUM_ION,
                           T.PERFORM_SCHEDULE,
                           T.NURSE_ID,
                           T.PURIFICATION_MODE,
                           T.CLEAN_UP_TIMES,
                           T.FREQUENCY_HOURS,
                           T.BEGIN_TIME,
                           T.END_TIME,
                           DECODE(T.LAST_TIME_DRY_WEIGHT, 0, '', T.LAST_TIME_DRY_WEIGHT) LAST_TIME_DRY_WEIGHT,
                           DECODE(T.BEFORE_DRY_WEIGHT, 0, '', T.BEFORE_DRY_WEIGHT) BEFORE_DRY_WEIGHT,
                           DECODE(T.AFTER_DRY_WEIGHT, 0, '', T.AFTER_DRY_WEIGHT) AFTER_DRY_WEIGHT,
                           DECODE(T.BEFORE_SYSTOLIC_PRESSURE, 0, '', T.BEFORE_SYSTOLIC_PRESSURE) BEFORE_SYSTOLIC_PRESSURE,
                           DECODE(T.BEFORE_DIASTOLIC_PRESSURE, 0,'',T.BEFORE_DIASTOLIC_PRESSURE) BEFORE_DIASTOLIC_PRESSURE,
                           DECODE(T.AFTER_SYSTOLIC_PRESSURE, 0, '', T.AFTER_SYSTOLIC_PRESSURE) AFTER_SYSTOLIC_PRESSURE,
                           DECODE(T.AFTER_DIASTOLIC_PRESSURE, 0, '', T.AFTER_DIASTOLIC_PRESSURE) AFTER_DIASTOLIC_PRESSURE,
                           DECODE(T.DRY_WATER_VALUE, 0, '', T.DRY_WATER_VALUE) DRY_WATER_VALUE,
                           DECODE(T.BEFORE_TEMPERATURE, 0, '', T.BEFORE_TEMPERATURE) BEFORE_TEMPERATURE,
                           DECODE(T.AFTER_TEMPERATURE, 0, '', T.AFTER_TEMPERATURE) AFTER_TEMPERATURE,
                           DECODE(T.BEFORE_HEART_RATE, 0, '', T.BEFORE_HEART_RATE) BEFORE_HEART_RATE,
                           DECODE(T.AFTER_HEART_RATE, 0, '', T.AFTER_HEART_RATE) AFTER_HEART_RATE,
       
                           T.PRIMARY_NURSE,
                           T.PRIMARY_DOCTOR,
                           T.PUNCTURE_NURSE,
                           T.MACHINE_ID,
                           T.VASCULAR_ACCESS_ID,
                           T.HEPARIN_SPECIES,
                           DECODE(T.FIRST_HEPARIN, 0, '', T.FIRST_HEPARIN) FIRST_HEPARIN,
                           DECODE(T.DOSIS_SUSTENTATIVA, 0, '', T.DOSIS_SUSTENTATIVA) DOSIS_SUSTENTATIVA,
       
                           T.MACHINE_TYPE,
                           T.PURIFIER_NAME,
                           T.PURIFIER_M2,
                           T.USE_TYPE,
                           DECODE(T.REUSE_TIMES, NULL, 0, T.REUSE_TIMES) REUSE_TIMES,
                           T.A_LIQUID,
                           T.B_LIQUID,
                           T.BIRCARBONATE,
                           T.AMYLACEUM,
                           T.SUMMARY,
                           T.CURE_CREATE_DATE,
                           T.VASCULAR_ACCESS_FIRM,
                           T.VASCULAR_ACCESS_GLIDE,
                           T.VASCULAR_ACCESS_SWELLING,
                           T.VASCULAR_ACCESS_ERRHYISIS,
                           T.VASCULAR_ACCESS_THROMBUS,
                           T.VASCULAR_ACCESS_BLOOD,
                           T.VASCULAR_ACCESS_BLOOD_INFECT,
                           T.FILTRATION_DISPLACEMENT_LIQUID,
                           T.FILTRATION_PERCOLATE,
                           T.DISPLACEMENT_LIQUID,
                           T.PERCOLATE,
       
                           T.DOCTOR_ADVICE,
                           T.SUMMARY2,
                           T.SUMMARY3,
                           T.CHECK_NURSE,
                           T.FIRST_DRUG_UNIT,
                           T.SECOND_DRUG_UNIT,
                           T.VEIN,
                           T.BEFORE_DRY_WEIGHT_TAG,
                           T.AFTER_DRY_WEIGHT_TAG,
                           T.REUSE_TIMES_TAG,
                           T.MACHINE_ID_TAG,
                           T.BLOOD_UP,
                           T.BEFORE_BP,T.AFTER_BP,
                           (SELECT Z.ITEM_NAME
                              FROM MED_COMMON_ITEMLIST Z
                             WHERE Z.ITEM_TYPE = '血型'
                               AND Z.ITEM_ID = T.BLOOD_TYPE) AS BLOOD_TYPE, --T.BLOOD_TYPE,
                           T.BLOOD_TRANSFUSION,
                           T.COAGULATION_IN_DIALYSER,
                           T.IN_BASKET_CLEAN,
                           T.IN_BASKET_RED_HOT,
                           T.IN_BASKET_ECCHYMOSIS,
                           T.IN_BASKET_TREMOR,
                           T.IN_BASKET_NOISE,
                           T.IN_BASKET_VASCULAR_ELASTICITY,
                           T.IN_BASKET_VASCULAR_OTHER,
                           T.IN_BASKET_WOUND_ALLERGY,
                           T.IN_BASKET_PLASTER_ALLERGY,
                           T.VASCULAR_ACCESS_TYPE,
                           T.DRY_WEIGHT,
                           T.DRY_WEIGHT_TAG,
                           T.SUBJECTIVE_COMFORT,
                           CK.ITEM_NAME SUBJECTIVE_COMFORTNAME,
                           T.INFECTIOUS_CHECK_RESULT,
                           T.WHAT_DEPARTMENT_IN,
                           T.FREQUENCY_MINUTE,
                           T.DISPLACEMENT_MODE,
                           T.DISPLACEMENT_RECIPE,
                           T.DISPLACEMENT_SPECIAL_ADJUST,
                           T.ANTICOAGULANT_USE,
                           T.SPECIAL_MATTER,
                           T.UFR2,
                           T.DISPLACEMENT_FLOW,
                           T.UF,
                           T.SUM_UF,
                           T.FOCUS_LEVEL,
                           T. SENSES,
                           CKK.ITEM_NAME SENSESNAME,
                           T.ALLERGIC,                     
                           T.BR,
                           T.AFTERBR,
                           T.IN_BED,
                           T.ACTUAL_CLEANUP_HOUR,
                           T.ACTUAL_CLEANUP_MINUTE,
                           I.ITEM_NAME || T1.ITEM_NAME || T2.ITEM_NAME AS VASCULAR_ACCESS_NAME,
                           M.ITEM_NAME AS MACHINE_TYPE_NAME,
                           MSD.NAME AS DOCTOR_NAME,
                           MSDN.NAME AS NURSE_NAME,
                           MSDNC.NAME AS CHECK_NURSE_NAME,
                           MSDNCP.NAME AS PUNCTURE_NURSE_NAME,
                           CI.ITEM_NAME AS FIRST_DRUG_UNIT_NAME,
                           CII.ITEM_NAME AS SECOND_DRUG_UNIT_NAME,
                           (SELECT MACHINE_MODEL
                              FROM MED_DIALYSIS_MACHINE I
                             WHERE I.MACHINE_ID =
                                   (SELECT DISTINCT K.MONITOR_LABEL
                                      FROM MED_PATIENT_SCHEDULE K
                                     WHERE K.HEMODIALYSIS_ID = T.HEMODIALYSIS_ID
                                       AND TRUNC(K.DIALYSIS_DATE) = TRUNC(T.CURE_CREATE_DATE)
                                       AND ROWNUM = 1)) AS MACHINE_NAME, --MACHINE.MACHINE_NAME,
                           Z.ITEM_NAME AS PURIFICATION_MODE_NAME,
                           J.ITEM_NAME AS PURIFIER_NEW_NAME,
                           C.ITEM_NAME AS HEPARIN_SPECIES_NAME
                      FROM MED_CURE_MAIN T
                      LEFT JOIN MED_COMMON_ITEMLIST Z
                        ON T.PURIFICATION_MODE = Z.ITEM_ID
                      LEFT JOIN MED_VASCULAR_ACCESS S
                        ON T.VASCULAR_ACCESS_ID = S.VASCULAR_ACCESS_ID
                      LEFT JOIN MED_COMMON_ITEMLIST I
                        ON S.VASCULAR_ACCESS_TYPE = I.ITEM_ID
                      LEFT JOIN MED_COMMON_ITEMLIST T1
                        ON S.LATERAL_POSITION=T1.ITEM_ID
                      LEFT JOIN MED_COMMON_ITEMLIST T2
                        ON S.VASCULAR_POSTION=T2.ITEM_ID
                      LEFT JOIN MED_COMMON_ITEMLIST M
                        ON T.MACHINE_TYPE = M.ITEM_ID
                      LEFT JOIN MED_STAFF_DICT MSD
                        ON T.PRIMARY_DOCTOR = MSD.EMP_NO
                      LEFT JOIN MED_STAFF_DICT MSDN
                        ON T.PRIMARY_NURSE = MSDN.EMP_NO
                      LEFT JOIN MED_STAFF_DICT MSDNC
                        ON T.CHECK_NURSE = MSDNC.EMP_NO
                      LEFT JOIN MED_STAFF_DICT MSDNCP
                        ON T.PUNCTURE_NURSE = MSDNCP.EMP_NO
                      LEFT JOIN MED_DIALYSIS_MACHINE MACHINE
                        ON T.MACHINE_ID = MACHINE.MACHINE_ID
                      LEFT JOIN MED_COMMON_ITEMLIST J
                        ON T.PURIFIER_NAME = J.ITEM_ID
                      LEFT JOIN MED_COMMON_ITEMLIST C
                        ON T.HEPARIN_SPECIES = C.ITEM_ID
                      LEFT JOIN MED_COMMON_ITEMLIST CI
                        ON T.FIRST_DRUG_UNIT = CI.ITEM_ID
                      LEFT JOIN MED_COMMON_ITEMLIST CII
                        ON T.SECOND_DRUG_UNIT = CII.ITEM_ID
                      LEFT JOIN MED_COMMON_ITEMLIST CK
                        ON T.SUBJECTIVE_COMFORT = CK.ITEM_ID
                      LEFT JOIN MED_COMMON_ITEMLIST CKK
                        ON T.SENSES = CKK.ITEM_ID
                     WHERE  T.CURE_ID = :CURE_ID
                          AND T.CURE_STATUS != '4'";
            }
        }

        /// <summary>
        /// 根据病人透析号得到对应治疗单数据SQL
        /// </summary>
        public string GetMainCureByHemoID
        {
            get
            {
                return "SELECT * FROM MED_CURE_MAIN WHERE HEMODIALYSIS_ID = :HEMODIALYSIS_ID  AND CURE_STATUS != '4'  ORDER BY CURE_CREATE_DATE DESC";
            }
        }


        /// <summary>
        /// 根据病人透析号与治疗日期得到对应治疗单数据SQL
        /// </summary>
        public string GetMainCureByHemoIDAndDate
        {
            get
            {
                return @"SELECT T.*,L.ITEM_NAME AS PURIFICATION_MODE_NAME,DRECIPE.ITEM_NAME AS DISPLACEMENT_RECIPE_NAME,DMODE.ITEM_NAME AS DISPLACEMENT_MODE_NAME,ROOM.ITEM_NAME AS AREA_NAME,UNIT1.ITEM_NAME AS FIRST_DRUG_UNIT_NAME,UNIT2.ITEM_NAME AS SECOND_DRUG_UNIT_NAME,VTYPE.ITEM_NAME AS VASCULAR_ACCESS_NAME,HEPARIN.ITEM_NAME AS HEPARIN_SPECIES_NAME,MTYPE.ITEM_NAME AS MACHINE_TYPE_NAME,PURIFIER.ITEM_NAME AS PURIFIER_FULL_NAME,P.NAME AS PATIENT_NAME,PN.NAME AS PRIMARY_NURSE_NAME,PD.NAME AS PRIMARY_DOCTOR_NAME,PUN.NAME AS PUNCTURE_NURSE_NAME,CHK.NAME AS CHECK_NURSE_NAME,DECODE(T1.BASEINFO, NULL, '1', '2') ISUPLOAD,DECODE(T1.BASEINFO, NULL, '未上传', '已上传') UPSTATE FROM MED_CURE_MAIN T
                        LEFT JOIN MED_PATIENT_SCHEDULE S ON T.RECIPE_ID=S.RECIPE_ID AND T.HEMODIALYSIS_ID=S.HEMODIALYSIS_ID
                        LEFT JOIN MED_PATIENTS P ON T.HEMODIALYSIS_ID=P.HEMODIALYSIS_ID
                        LEFT JOIN MED_VASCULAR_ACCESS VACCESS ON T.VASCULAR_ACCESS_ID=VACCESS.VASCULAR_ACCESS_ID
                        LEFT JOIN MED_COMMON_ITEMLIST L ON T.PURIFICATION_MODE=L.ITEM_ID
                        LEFT JOIN MED_COMMON_ITEMLIST ROOM ON S.DIALYSIS_ROOM_ID=ROOM.ITEM_ID
                        LEFT JOIN MED_COMMON_ITEMLIST VTYPE ON VACCESS.VASCULAR_ACCESS_TYPE=VTYPE.ITEM_ID
                        LEFT JOIN MED_COMMON_ITEMLIST HEPARIN ON T.HEPARIN_SPECIES=HEPARIN.ITEM_ID
                        LEFT JOIN MED_COMMON_ITEMLIST MTYPE ON T.MACHINE_TYPE=MTYPE.ITEM_ID
                        LEFT JOIN MED_COMMON_ITEMLIST PURIFIER ON T.PURIFIER_NAME=PURIFIER.ITEM_ID
                        LEFT JOIN MED_COMMON_ITEMLIST UNIT1 ON T.FIRST_DRUG_UNIT=UNIT1.ITEM_ID
                        LEFT JOIN MED_COMMON_ITEMLIST UNIT2 ON T.SECOND_DRUG_UNIT=UNIT2.ITEM_ID
                        LEFT JOIN MED_COMMON_ITEMLIST DMODE ON T.DISPLACEMENT_MODE=DMODE.ITEM_ID
                        LEFT JOIN MED_COMMON_ITEMLIST DRECIPE ON T.DISPLACEMENT_RECIPE=DRECIPE.ITEM_ID
                        LEFT JOIN MED_STAFF_DICT PN ON T.PRIMARY_NURSE=PN.EMP_NO
                        LEFT JOIN MED_STAFF_DICT PD ON T.PRIMARY_DOCTOR=PD.EMP_NO
                        LEFT JOIN MED_STAFF_DICT PUN ON T.PUNCTURE_NURSE=PUN.EMP_NO
                        LEFT JOIN MED_STAFF_DICT CHK ON T.CHECK_NURSE=CHK.EMP_NO
                        LEFT JOIN (SELECT * FROM MED_PATIENT_DATAREPORT Z WHERE Z.STATE = '1' AND Z.TYPE = '3'AND Z.EXTEND = 'HZZLXX'  AND Z.EXTEND5 = '福建省上报平台')T1  ON (T.HEMODIALYSIS_ID = T1.HEMODIALYSIS_ID AND T.CURE_ID=T1.MAPIP)
                        WHERE T.HEMODIALYSIS_ID = :HEMODIALYSIS_ID  AND T.CURE_STATUS != '4' AND (TRUNC(T.CURE_CREATE_DATE)
                        BETWEEN TRUNC(:BEGIN_DATE) AND TRUNC(:END_DATE)) AND S.START_TIME IS NOT NULL AND S.END_TIME IS NOT NULL ORDER BY T.CURE_CREATE_DATE DESC";
            }
        }

        /// <summary>
        /// 根据病人透析号获取透析参数数据SQL
        /// </summary>
        public string GetHemoParamsByHemoID
        {
            get
            {
                return @"SELECT P.* FROM MED_HEMODIALYSIS_PARAMETERS P,MED_CURE_MAIN C WHERE P.CURE_ID=C.CURE_ID AND C.CURE_STATUS != '4'  AND P.RECIPE_ID=C.RECIPE_ID AND C.HEMODIALYSIS_ID=:HEMODIALYSIS_ID   ORDER BY CREATE_DATE";
            }
        }

        /// <summary>
        /// 根据透析参数ID获取透析参数数据SQL
        /// </summary>
        public string GetHemoParametersByHemoParamId
        {
            get
            {
                return @"SELECT P.* FROM MED_HEMODIALYSIS_PARAMETERS P WHERE P.HEMODIALYSIS_PARAMETERS_ID=:HEMODIALYSIS_PARAMETERS_ID";
            }
        }

        /// <summary>
        /// 根据病人透析号得到对应治疗单数据SQL
        /// </summary>
        public string GetMainCureByRecipeId
        {
            get
            {
                return @"SELECT t.*, item.item_name as PURIFICATION_MODE_NAME,
                         (select machine_model
                         from med_dialysis_machine i
                         where i.machine_id =
                         (select distinct k.monitor_label
                         from med_patient_schedule k
                         where k.hemodialysis_id = t.hemodialysis_id
                         and trunc(k.dialysis_date) = trunc(t.cure_create_date) and rownum=1)) as machine_name
                         FROM MED_CURE_MAIN t
                         left join med_common_itemlist item
                         on t.PURIFICATION_MODE = item.item_id
                         WHERE t.RECIPE_ID = :RECIPE_ID  AND T.CURE_STATUS != '4'
                         ORDER BY CURE_CREATE_DATE DESC";
            }
        }

        public string GetOrderComNo
        {
            get
            {
                return @"SELECT MED_ORDER_COM_NO.NEXTVAL FROM DUAL";
            }
        }

        public string GetHemoParametersByHemoParamRow
        {
            get
            {
                return @"SELECT * FROM(SELECT * FROM (SELECT case to_char(SYSTOLIC_PRESSURE) when '0' then '' else to_char(SYSTOLIC_PRESSURE) end || '/' ||        
                case to_char(DIASTOLIC_PRESSURE) when '0' then '' else to_char(DIASTOLIC_PRESSURE) end as BLOOD_PRESSURE,
                HEMODIALYSIS_PARAMETERS_ID, CURE_ID, RECIPE_ID, CREATE_DATE, VENOUS_PRESSURE, 
                TRANSMEMBRANE_PRESSURE, TEMPERATURE, SYSTOLIC_PRESSURE, DIASTOLIC_PRESSURE, CARDIOTACH, 
                BREATH, KT_V, CURE_MODE, CLINICAL_MANIFESTATION, BLOOD_FLOW, SODIUM_ION, DIALYSATE_RATE, URF, ARTERY_PRESSURE,
                CONDUCTIVITY, NURSE_ID,DISPLACEMENT,VASCULAR_ACCESS_ERRHYISIS,VASCULAR_ACCESS_GLIDE,ANTICOAGULANT,ANTICOAGULANTUNIT,CRRT_CLASS,
                (SELECT S.ITEM_NAME FROM MED_COMMON_ITEMLIST S WHERE S.ITEM_VALUE = EXTENDED_FIELD_3 AND S.ITEM_TYPE='治疗参数类型') AS EXTENDED_FIELD_3
                FROM  MED_HEMODIALYSIS_PARAMETERS
                WHERE CURE_ID = :CURE_ID and (EXTENDED_FIELD_1 != '1' OR EXTENDED_FIELD_1 IS NULL) ORDER BY CREATE_DATE DESC) 
                WHERE ROWNUM <= :ROWNUMBER1 ORDER BY CREATE_DATE) WHERE ROWNUM <= :ROWNUMBER2 ";
            }
        }
        public string GetHemoParametersByHemoParamRowNew
        {
            get
            {
                return @"SELECT * FROM(SELECT * FROM (SELECT case to_char(SYSTOLIC_PRESSURE) when '0' then '' else to_char(SYSTOLIC_PRESSURE) end || '/' ||        
                case to_char(DIASTOLIC_PRESSURE) when '0' then '' else to_char(DIASTOLIC_PRESSURE) end as BLOOD_PRESSURE,
                HEMODIALYSIS_PARAMETERS_ID, CURE_ID, RECIPE_ID, CREATE_DATE, VENOUS_PRESSURE, 
                TRANSMEMBRANE_PRESSURE, TEMPERATURE, SYSTOLIC_PRESSURE, DIASTOLIC_PRESSURE, CARDIOTACH, 
                BREATH, KT_V, CURE_MODE, CLINICAL_MANIFESTATION, BLOOD_FLOW, SODIUM_ION, DIALYSATE_RATE, URF, 
                CONDUCTIVITY, NURSE_ID,DISPLACEMENT,VASCULAR_ACCESS_ERRHYISIS,VASCULAR_ACCESS_GLIDE,ANTICOAGULANT,ANTICOAGULANTUNIT,CRRT_CLASS,
                (SELECT S.ITEM_NAME FROM MED_COMMON_ITEMLIST S WHERE S.ITEM_VALUE = EXTENDED_FIELD_3 AND S.ITEM_TYPE='治疗参数类型') AS EXTENDED_FIELD_3
                FROM  MED_HEMODIALYSIS_PARAMETERS
                WHERE CURE_ID = :CURE_ID and (EXTENDED_FIELD_1 != '1' OR EXTENDED_FIELD_1 IS NULL) ORDER BY CREATE_DATE DESC) 
                WHERE ROWNUM <= :ROWNUMBER1 ORDER BY CREATE_DATE DESC) WHERE ROWNUM <= :ROWNUMBER2 ORDER BY CREATE_DATE";
            }
        }

        public string GetDrugRecord
        {
            get
            {
                return @"select a.create_date,b.name,a.clinical_manifestation 
                from MED_HEMODIALYSIS_PARAMETERS a inner join MED_STAFF_DICT b on a.nurse_id=b.emp_no 
                inner join med_cure_main m on m.cure_id = a.cure_id 
                where m.hemodialysis_id = :CURE_ID and (a.EXTENDED_FIELD_1 != '1' OR a.EXTENDED_FIELD_1 IS NULL) 
                and m.cure_create_date between :BEGIN_CREATE_DATE and :END_CREATE_DATE ORDER BY m.cure_create_date desc";
            }
        }

        /// <summary>
        /// 根据治疗单编号得到对应透析参数数据列表SQL
        /// </summary>
        public string GetHemoParametersByCureID
        {
            get
            {
                return @"SELECT  case to_char(SYSTOLIC_PRESSURE) when '0' then '' else to_char(SYSTOLIC_PRESSURE) end || '/' ||        
                case to_char(DIASTOLIC_PRESSURE) when '0' then '' else to_char(DIASTOLIC_PRESSURE) end as BLOOD_PRESSURE,
                HEMODIALYSIS_PARAMETERS_ID, CURE_ID, RECIPE_ID, CREATE_DATE, VENOUS_PRESSURE, ARTERY_PRESSURE,
                TRANSMEMBRANE_PRESSURE, TEMPERATURE, SYSTOLIC_PRESSURE, DIASTOLIC_PRESSURE, CARDIOTACH, 
                BREATH, KT_V, CURE_MODE, CLINICAL_MANIFESTATION, BLOOD_FLOW, SODIUM_ION, DIALYSATE_RATE, URF,ONLINE_CLEARANCE_RATE,
                CONDUCTIVITY, NURSE_ID,DISPLACEMENT,VASCULAR_ACCESS_ERRHYISIS,VASCULAR_ACCESS_GLIDE,EXTENDED_FIELD_1,EXTENDED_FIELD_2,EXTENDED_FIELD_3,EXTENDED_FIELD_4,EXTENDED_FIELD_5,ANTICOAGULANT,VENOUS_PRESSURE_UNIT,ANTICOAGULANTUNIT,
                (CLINICAL_MANIFESTATION||' '||EXTENDED_FIELD_2) as NEW_CLINICAL_MANIFESTATION,CRRT_CLASS FROM  MED_HEMODIALYSIS_PARAMETERS
                WHERE CURE_ID = :CURE_ID and (EXTENDED_FIELD_1 != '1' OR EXTENDED_FIELD_1 IS NULL) ORDER BY CREATE_DATE ";
            }
        }

        /// <summary>
        /// 根据治疗单编号得到对应已删除透析参数数据列表SQL
        /// </summary>
        public string GetDeleteHemoParametersByCureID
        {
            get
            {
                return @"SELECT  to_char(SYSTOLIC_PRESSURE)||'/'||to_char(DIASTOLIC_PRESSURE) as BLOOD_PRESSURE,HEMODIALYSIS_PARAMETERS_ID, CURE_ID, RECIPE_ID, CREATE_DATE, VENOUS_PRESSURE, 
                TRANSMEMBRANE_PRESSURE, TEMPERATURE, SYSTOLIC_PRESSURE, DIASTOLIC_PRESSURE, CARDIOTACH, 
                BREATH, KT_V, CURE_MODE, CLINICAL_MANIFESTATION, BLOOD_FLOW, SODIUM_ION, DIALYSATE_RATE, URF, 
                CONDUCTIVITY, NURSE_ID,DISPLACEMENT,VASCULAR_ACCESS_ERRHYISIS,VASCULAR_ACCESS_GLIDE,ANTICOAGULANT
                FROM  MED_HEMODIALYSIS_PARAMETERS
                WHERE CURE_ID = :CURE_ID and EXTENDED_FIELD_1 = '1' ORDER BY CREATE_DATE ";
            }
        }

        /// <summary>
        /// 得到对应透析参数数据列表
        /// </summary>
        public string GetHemoParameters
        {
            get
            {
                return @"
                    SELECT 
                      p.*
                    FROM MED_CURE_MAIN c
                      INNER JOIN MED_HEMODIALYSIS_PARAMETERS p ON c.CURE_ID = p.CURE_ID
                    WHERE
                      TRUNC(p.CREATE_DATE) >= TRUNC(:BEGIN_CREATE_DATE)
                      AND TRUNC(p.CREATE_DATE) <= TRUNC(:END_CREATE_DATE)
                      AND c.HEMODIALYSIS_ID = :HEMODIALYSIS_ID AND C.CURE_STATUS != '4'
                      and (p.EXTENDED_FIELD_1 != '1' OR p.EXTENDED_FIELD_1 IS NULL)
                    ORDER BY
                      p.CREATE_DATE";
            }
        }

        public string UpdataCureDrugStateByParma
        {
            get
            {
                return @"UPDATE MED_CURE_DRUG
                            SET STATUS= :STATUS,
                               CURE_ID  = :CURE_ID,
                               RECIPE_ID =:RECIPE_ID,
                               EXEC_DATE =:EXEC_DATE,
                               DRUG_NURSE_ID=:DRUG_NURSE_ID
                         WHERE  HEMODIALYSIS_ID= :HEMODIALYSIS_ID
                            AND COM_NO = :COM_NO
                            AND TRUNC(CREATE_DATE) = TRUNC(:CREATE_DATE)";
            }
        }

        public string UpdataCureDrugStateByParma2
        {
            get
            {
                return @"UPDATE MED_CURE_DRUG
                            SET STATUS= :STATUS,
                               CURE_ID  = :CURE_ID,
                               RECIPE_ID =:RECIPE_ID,
                               EXEC_DATE =:EXEC_DATE,
                               DRUG_NURSE_ID=:DRUG_NURSE_ID
                         WHERE  HEMODIALYSIS_ID= :HEMODIALYSIS_ID
                            AND COM_NO = :COM_NO 
                            AND TRUNC(CREATE_DATE) = TRUNC(:CREATE_DATE)";//AND COM_SUB_NO = :COM_SUB_NO
            }
        }

        public string GetHemoParametersType
        {
            get
            {
                return @"
                    SELECT 
                      *
                    FROM MED_HEMODIALYSIS_PARAMS_TYPE t
                    ORDER BY
                      t.GROUPID, t.SORT";
            }
        }

        public string GetDrugUseRecordListByHemoId
        {
            get
            {
                return @"SELECT * FROM MED_DRUG_USE_RECORD WHERE HEMODIALYSIS_ID = :HEMODIALYSIS_ID";
            }
        }

        /// <summary>
        /// 根据治疗单编号得到对应给药数据列表SQL
        //        / </summary>
        public string GetCureDrugByCureID
        {
            get
            {
                // return "SELECT * FROM MED_CURE_DRUG WHERE CURE_ID = :CURE_ID";
                return @"
                SELECT DRUG.*,I.ITEM_NAME AS UNIT_NAME,M.ITEM_NAME AS DRUG_MODE_NAME,d.drug_name as NEW_DRUG_NAME,s.name FROM MED_CURE_DRUG DRUG 
                LEFT JOIN MED_COMMON_ITEMLIST  I ON DRUG.DOSAGE_UNITS = I.ITEM_ID 
                LEFT JOIN  MED_COMMON_ITEMLIST M ON DRUG.DRUG_MODE = M.ITEM_ID
                left join med_drug_master d on TRIM(drug.drug_code) = TRIM(d.drug_code)
                left join med_staff_dict s on s.emp_no = drug.doctor_id
                WHERE DRUG.CURE_ID = :CURE_ID
                ";
            }
        }

        public string SaveExeDrugStatus
        {
            get
            {
                return @"UPDATE MED_CURE_DRUG
                            SET STATUS = :STATUS,STOP_TIME=SYSDATE
                         WHERE CURE_DRUG_ID=:CURE_DRUG_ID";
            }
        }
        public string GetComplicationOther
        {
            get
            {
                return @"SELECT DECODE(T.CCSW, '是', '穿刺失误', '') CCSW,
                                   DECODE(T.CCCSZ, '是', '穿刺血肿', '') CCCSZ,
                                   DECODE(T.XGTLGR, '是', '通路感染堵塞', '') XGTLGR,
                                   DECODE(T.CCZTL, '是', '穿刺针脱落', '') CCZTL,
                                   DECODE(RGSNG, '是', '人工肾凝固', '') RGSNG,
                                   DECODE(GXJ, '是', '高血钾', '') GXJ,
                                   DECODE(SX, '是', '失血', '') SX,
                                   DECODE(TXQPM, '是', '透析器破膜', '') TXQPM,
                                   DECODE(SYPK, '是', '输液排空', '') SYPK,
                                   DECODE(TYTT, '是', '头晕、头痛', '') TYTT,
                                   DECODE(EXOT, '是', '恶心、呕吐', '') EXOT,
                                   DECODE(TWSG, '是', '体温升高', '') TWSG,
                                   DECODE(PFSY, '是', '皮肤瘙痒', '') PFSY,
                                   DECODE(GM, '是', '过敏', '') GM,
                                   DECODE(XMXT, '是', '胸闷、胸痛', '') XMXT,
                                   DECODE(FT, '是', '腹痛', '') FT,
                                   DECODE(JRJL, '是', '肌肉痉挛', '') FRJL,
                                   DECODE(DXT, '是', '低血糖', '') DXT,
                                   DECODE(XYXJXK, '是', '血压下降休克', '') XYXJXK,
                                   DECODE(XYSG, '是', '血压升高', '') XYSG,
                                   DECODE(XLSC, '是', '心律失常', '') XLSC,
                                   DECODE(XS, '是', '心衰', '') XS,
                                   DECODE(CC, '是', '抽搐', '') CC,
                                   DECODE(YSFYCD, '是', '意识反应迟钝', '') YSFYCD,
                                   DECODE(TSWZ, '是', '脱水误差≥1KG', '') TSWZ
                              FROM MED_COMPLICATION_OTHER T
                             WHERE T.CURE_ID = :CURE_ID";
            }
        }
        public string SaveExeDrugLongStatus
        {
            get
            {
                return @"UPDATE MED_CURE_LONGDRUG
                            SET STATUS = :STATUS,STOP_TIME=SYSDATE
                         WHERE CURE_DRUG_ID=:CURE_DRUG_ID";
            }
        }

        public string GetCureDrugByHemoID
        {
            get
            {
                return @"SELECT DRUG.*,
                                   I.ITEM_NAME AS UNIT_NAME,
                                   M.ITEM_NAME AS DRUG_MODE_NAME,
                                   d.drug_name as NEW_DRUG_NAME,
                                   s.name,
                                   decode(drug.status,'0','新开立','1','已执行','2','返回','3','停止','其它') AS STATUS2,
                                   (select k.name from med_staff_dict k where k.emp_no = drug.drug_nurse_id) as DRUG_NURSE_NAME
                              FROM MED_CURE_DRUG DRUG
                              LEFT JOIN MED_COMMON_ITEMLIST I
                                ON DRUG.DOSAGE_UNITS = I.ITEM_ID
                              LEFT JOIN MED_COMMON_ITEMLIST M
                                ON DRUG.DRUG_MODE = M.ITEM_ID
                              left join med_drug_master d
                                on TRIM(drug.drug_code) = TRIM(d.drug_code)
                              left join med_staff_dict s
                                on s.emp_no = drug.doctor_id
                             WHERE DRUG.HEMODIALYSIS_ID = :HEMODIALYSIS_ID
                              AND  trunc(DRUG.create_date) = trunc(:CURRENTDT)
                                ORDER BY DRUG.COM_NO,DRUG.COM_SUB_NO,DRUG.CREATE_DATE";
            }
        }
        public string GetCureDrugByHemoIDAndRecipeId
        {
            get
            {
                return @"SELECT DRUG.*,
                                   I.ITEM_NAME AS UNIT_NAME,
                                   M.ITEM_NAME AS DRUG_MODE_NAME,
                                   d.drug_name as NEW_DRUG_NAME,
                                   s.name,
                                   decode(drug.status,'0','新开立','1','已执行','2','返回','3','停止','其它') AS STATUS2,
                                   (select k.name from med_staff_dict k where k.emp_no = drug.drug_nurse_id) as DRUG_NURSE_NAME
                              FROM MED_CURE_DRUG DRUG
                              LEFT JOIN MED_COMMON_ITEMLIST I
                                ON DRUG.DOSAGE_UNITS = I.ITEM_ID
                              LEFT JOIN MED_COMMON_ITEMLIST M
                                ON DRUG.DRUG_MODE = M.ITEM_ID
                              left join med_drug_master d
                                on TRIM(drug.drug_code) = TRIM(d.drug_code)
                              left join med_staff_dict s
                                on s.emp_no = drug.doctor_id
                             WHERE DRUG.HEMODIALYSIS_ID = :HEMODIALYSIS_ID
                                  AND DRUG.RECIPE_ID=:RECIPE_ID
                                ORDER BY DRUG.COM_NO,DRUG.COM_SUB_NO,DRUG.CREATE_DATE";
            }
        }
        public string GetCureDrugForPatientRecord
        {
            get
            {
                return @"SELECT DRUG.*,I.ITEM_NAME AS UNIT_NAME FROM MED_CURE_DRUG DRUG LEFT JOIN MED_COMMON_ITEMLIST I
                        ON DRUG.DOSAGE_UNITS = I.ITEM_ID WHERE DRUG.HEMODIALYSIS_ID = :HEMODIALYSIS_ID";
            }
        }

        public string GetValidCureDrugByHemoID
        {
            get
            {
                return @"SELECT DRUG.*,
                               I.ITEM_NAME AS UNIT_NAME,
                               M.ITEM_NAME AS DRUG_MODE_NAME,
                               DRUG.DRUG_NAME || ' '|| DRUG.DOSAGE ||  I.ITEM_NAME || ' ' || M.ITEM_NAME AS NEW_DRUG_NAME,
                               S.NAME,
                               DECODE(DRUG.STATUS,
                                      '0',
                                      '新开立',
                                      '1',
                                      '已执行',
                                      '2',
                                      '返回',
                                      '3',
                                      '停止',
                                      '其它') AS STATUS2,
                               (SELECT K.NAME
                                  FROM MED_STAFF_DICT K
                                 WHERE K.EMP_NO = DRUG.DRUG_NURSE_ID) AS DRUG_NURSE_NAME,
                               (SELECT K.NAME FROM MED_STAFF_DICT K WHERE K.EMP_NO = DRUG.CHECKNURSE) AS CHECKNURSE_NAME,
                               (SELECT K.NAME
                                  FROM MED_STAFF_DICT K
                                 WHERE K.EMP_NO = DRUG.DISPENSINGNURSE) AS DISPENSINGNURSE_NAME
                              FROM MED_CURE_DRUG DRUG
                              LEFT JOIN MED_COMMON_ITEMLIST I
                                ON DRUG.DOSAGE_UNITS = I.ITEM_ID
                              LEFT JOIN MED_COMMON_ITEMLIST M
                                ON DRUG.DRUG_MODE = M.ITEM_ID
                              left join med_drug_master d
                                on TRIM(drug.drug_code) = TRIM(d.drug_code)
                              left join med_staff_dict s
                                on s.emp_no = drug.doctor_id
                             WHERE DRUG.HEMODIALYSIS_ID = :HEMODIALYSIS_ID 
                               AND  DRUG.STATUS IN('0','1','2')
                               AND  TRUNC(DRUG.CREATE_DATE) = TRUNC(:CREATE_DATE) order by DRUG.com_no,DRUG.com_sub_no";
            }
        }
        public string GetValidCureDrugByRoomIdAndData
        {
            get
            {
                return @"SELECT DISTINCT DRUG.*,
                                   I.ITEM_NAME AS UNIT_NAME,K.NAME as PATIENTNAME,
                                   M.ITEM_NAME AS DRUG_MODE_NAME,
                                   (SELECT CASE
                                              WHEN MAX(R.COM_SUB_NO) = MIN(R.COM_SUB_NO) THEN
                                               '  '
                                              WHEN DRUG.COM_SUB_NO = MIN(R.COM_SUB_NO) THEN
                                               '┌'
                                              WHEN DRUG.COM_SUB_NO = MAX(R.COM_SUB_NO) THEN
                                               '└'
                                              ELSE
                                               '│'
                                            END
                                       FROM MED_CURE_DRUG R
                                      WHERE R.COM_NO = DRUG.COM_NO) || DRUG_NAME || ' ' ||
                                    DRUG.DOSAGE || I.ITEM_NAME || ' ' || M.ITEM_NAME AS NEW_DRUG_NAME,
                                   S.NAME,
                                   DECODE(DRUG.STATUS,
                                          '0',
                                          '新开立',
                                          '1',
                                          '已执行',
                                          '2',
                                          '返回',
                                          '3',
                                          '停止',
                                          '其它') AS STATUS2,
                                   (SELECT K.NAME
                                      FROM MED_STAFF_DICT K
                                     WHERE K.EMP_NO = DRUG.DRUG_NURSE_ID) AS DRUG_NURSE_NAME,
                                   (SELECT K.NAME FROM MED_STAFF_DICT K WHERE K.EMP_NO = DRUG.CHECKNURSE) AS CHECKNURSE_NAME,
                                   (SELECT K.NAME
                                      FROM MED_STAFF_DICT K
                                     WHERE K.EMP_NO = DRUG.DISPENSINGNURSE) AS DISPENSINGNURSE_NAME,
                                     T1.ITEM_NAME AS ROOMNAME,
                                     T2.ITEM_VALUE || '床' AS BEDNAME
                              FROM MED_CURE_DRUG DRUG
                              LEFT JOIN MED_COMMON_ITEMLIST I
                                ON DRUG.DOSAGE_UNITS = I.ITEM_ID
                              LEFT JOIN MED_COMMON_ITEMLIST M
                                ON DRUG.DRUG_MODE = M.ITEM_ID
                              LEFT JOIN MED_DRUG_MASTER D
                                ON TRIM(DRUG.DRUG_CODE) = TRIM(D.DRUG_CODE)
                              LEFT JOIN MED_STAFF_DICT S
                                ON S.EMP_NO = DRUG.DOCTOR_ID
                              LEFT JOIN MED_PATIENT_SCHEDULE T
                                ON (DRUG.HEMODIALYSIS_ID = T.HEMODIALYSIS_ID AND DRUG.RECIPE_ID= T.RECIPE_ID)
                              LEFT JOIN MED_COMMON_ITEMLIST T1
                                ON T.DIALYSIS_ROOM_ID = T1.ITEM_ID
                              LEFT JOIN MED_COMMON_ITEMLIST T2
                                ON T.BED_NUMBER= T2.ITEM_ID
                                  LEFT JOIN MED_PATIENTS K
                                ON DRUG.HEMODIALYSIS_ID= K.HEMODIALYSIS_ID
                             WHERE DRUG.STATUS IN ('0', '1', '2')
                               AND T.DIALYSIS_ROOM_ID = :DIALYSIS_ROOM_ID
                               AND T.DIALYSIS_DATE >= TRUNC(:DTSTAR)
                               AND T.DIALYSIS_DATE <= TRUNC(:DTEND)
                               AND TRUNC(DRUG.CREATE_DATE) >= TRUNC(:DTSTAR) 
                               AND TRUNC(DRUG.CREATE_DATE) <= TRUNC(:DTEND) 
                               AND T.BANCI_ID=:BANCI_ID
                               {0}
                             ORDER BY DRUG.COM_NO, DRUG.COM_SUB_NO";
            }
        }
        public string GetValidCureDrugByHemoRecipeID
        {
            get
            {
                return @"SELECT DRUG.*,
                                   I.ITEM_NAME AS UNIT_NAME,
                                   M.ITEM_NAME AS DRUG_MODE_NAME,
                                   d.drug_name as NEW_DRUG_NAME,
                                   s.name,
                                   decode(drug.status,'0','新开立','1','已执行','2','返回','3','停止','其它') AS STATUS2,
                                    (select k.name from med_staff_dict k where k.emp_no = drug.drug_nurse_id) as DRUG_NURSE_NAME
                              FROM MED_CURE_DRUG DRUG
                              LEFT JOIN MED_COMMON_ITEMLIST I
                                ON DRUG.DOSAGE_UNITS = I.ITEM_ID
                              LEFT JOIN MED_COMMON_ITEMLIST M
                                ON DRUG.DRUG_MODE = M.ITEM_ID
                              left join med_drug_master d
                                on TRIM(drug.drug_code) = TRIM(d.drug_code)
                              left join med_staff_dict s
                                on s.emp_no = drug.doctor_id
                             WHERE DRUG.HEMODIALYSIS_ID = :HEMODIALYSIS_ID 
                               AND  DRUG.STATUS IN('0','1','2')
                               AND  DRUG.RECIPE_ID=:RECIPE_ID order by DRUG.com_no,DRUG.com_sub_no";
            }
        }
        public string GetUnExcuteCureDrugByHemoID
        {
            get
            {
                return @"SELECT DRUG.*,
                                   I.ITEM_NAME AS UNIT_NAME,
                                   M.ITEM_NAME AS DRUG_MODE_NAME,
                                   d.drug_name as NEW_DRUG_NAME,
                                   s.name,
                                   decode(drug.status,'0','新开立','1','已执行','2','返回','3','停止','其它') AS STATUS2,
                                    (select k.name from med_staff_dict k where k.emp_no = drug.drug_nurse_id) as DRUG_NURSE_NAME
                              FROM MED_CURE_DRUG DRUG
                              LEFT JOIN MED_COMMON_ITEMLIST I
                                ON DRUG.DOSAGE_UNITS = I.ITEM_ID
                              LEFT JOIN MED_COMMON_ITEMLIST M
                                ON DRUG.DRUG_MODE = M.ITEM_ID
                              left join med_drug_master d
                                on TRIM(drug.drug_code) = TRIM(d.drug_code)
                              left join med_staff_dict s
                                on s.emp_no = drug.doctor_id
                             WHERE DRUG.HEMODIALYSIS_ID = :HEMODIALYSIS_ID 
                               AND  DRUG.STATUS IN('0')
                               AND  TRUNC(DRUG.CREATE_DATE) = TRUNC(:CREATE_DATE) order by DRUG.com_no,DRUG.com_sub_no";
            }
        }
        public string GetUnExcuteCureDrugByHemoRecipeId
        {
            get
            {
                return @"SELECT DRUG.*,
                                   I.ITEM_NAME AS UNIT_NAME,
                                   M.ITEM_NAME AS DRUG_MODE_NAME,
                                   d.drug_name as NEW_DRUG_NAME,
                                   s.name,
                                   decode(drug.status,'0','新开立','1','已执行','2','返回','3','停止','其它') AS STATUS2,
                                    (select k.name from med_staff_dict k where k.emp_no = drug.drug_nurse_id) as DRUG_NURSE_NAME
                              FROM MED_CURE_DRUG DRUG
                              LEFT JOIN MED_COMMON_ITEMLIST I
                                ON DRUG.DOSAGE_UNITS = I.ITEM_ID
                              LEFT JOIN MED_COMMON_ITEMLIST M
                                ON DRUG.DRUG_MODE = M.ITEM_ID
                              left join med_drug_master d
                                on TRIM(drug.drug_code) = TRIM(d.drug_code)
                              left join med_staff_dict s
                                on s.emp_no = drug.doctor_id
                             WHERE DRUG.HEMODIALYSIS_ID = :HEMODIALYSIS_ID 
                               AND  DRUG.STATUS IN('0')
                               AND  DRUG.RECIPE_ID=:RECIPE_ID order by DRUG.com_no,DRUG.com_sub_no";
            }
        }
        public string GetLongCureDrugByHemoID
        {
            get
            {
                return @"SELECT DRUG.*,
                                   I.ITEM_NAME AS UNIT_NAME,
                                   M.ITEM_NAME AS DRUG_MODE_NAME,
                                   d.drug_name as NEW_DRUG_NAME,
                                   s.name,
                                   decode(drug.status,'0','新开立','1','已执行','2','返回','3','停止','其它') AS STATUS2
                              FROM MED_CURE_LONGDRUG DRUG
                              LEFT JOIN MED_COMMON_ITEMLIST I
                                ON DRUG.DOSAGE_UNITS = I.ITEM_ID
                              LEFT JOIN MED_COMMON_ITEMLIST M
                                ON DRUG.DRUG_MODE = M.ITEM_ID
                              left join med_drug_master d
                                on TRIM(drug.drug_code) = TRIM(d.drug_code)
                              left join med_staff_dict s
                                on s.emp_no = drug.doctor_id
                             WHERE DRUG.HEMODIALYSIS_ID = :HEMODIALYSIS_ID
                             ORDER BY DRUG.COM_NO,DRUG.COM_SUB_NO";
            }
        }

        /// <summary>
        /// 得到治疗单历史列表
        /// </summary>
        public string GetCureList
        {
            get
            {
                return @"SELECT DISTINCT T.CURE_CREATE_DATE,
                        S.DIALYSIS_DATE,
                        CASE S.BANCI_ID
                        WHEN '1' THEN '上午班'
                        WHEN '2' THEN '下午班'
                        WHEN '3' THEN '晚班'
                        END AS BANCI_NAME,
                        ROOM.ITEM_NAME AS ROOM_NAME,
                        COMM.ITEM_NAME AS BED_NAME,
                        S.HEMODIALYSIS_ID,
                        PAT.NAME,
                        PAT.SEX,
                        PAT.BIRTHDAY,
                        T.PRIMARY_NURSE, T.RECIPE_TYPE,
                        T.FREQUENCY_HOURS,
                        T.PURIFICATION_MODE,
                        T.CURE_ID
                        FROM MED_CURE_MAIN T
                        LEFT JOIN MED_PATIENT_SCHEDULE S ON T.HEMODIALYSIS_ID = S.HEMODIALYSIS_ID
                        LEFT JOIN MED_COMMON_ITEMLIST COMM ON S.BED_NUMBER = COMM.ITEM_ID
                        LEFT JOIN MED_COMMON_ITEMLIST ROOM ON S.DIALYSIS_ROOM_ID = ROOM.ITEM_ID
                        LEFT JOIN MED_PATIENTS PAT ON S.HEMODIALYSIS_ID = PAT.HEMODIALYSIS_ID
                        WHERE T.CURE_STATUS != '4'
                        AND TRUNC(T.CURE_CREATE_DATE) = TRUNC(S.DIALYSIS_DATE)
                        AND T.CURE_CREATE_DATE LIKE TO_DATE(:DIALYSIS_DATE, 'YYYY-MM-DD')
                        AND S.BANCI_ID LIKE '%'||:BANCI_ID||'%'
                        AND PAT.NAME LIKE '%'||:NAME||'%'
                        AND PAT.HEMODIALYSIS_ID LIKE '%'||:HEMODIALYSIS_ID||'%'";
            }
        }

        public string GetCureIDByHemoIDAndCureData
        {
            get
            {
                return @"  SELECT CURE_ID
                             FROM MED_CURE_MAIN
                            WHERE CURE_CREATE_DATE like to_date(:CURE_CREATE_DATE, 'yyyy-mm-dd')
                              AND CURE_STATUS != '4' AND HEMODIALYSIS_ID like '%' || :HEMODIALYSIS_ID || '%'";

            }
        }

        /// <summary>
        /// 根据病人透析号和治疗方式分组得到治疗数据SQL
        /// </summary>
        public string GetMainCureGroupByHemoIDAndPurificationMode
        {
            get
            {
                return @"
                    SELECT 
                      t.HEMODIALYSIS_ID, t.PURIFICATION_MODE, (SELECT ITEM_NAME FROM MED_COMMON_ITEMLIST li WHERE li.ITEM_ID = t.PURIFICATION_MODE) PURIFICATION_MODE_NAME, COUNT(1) COUNT
                    FROM
                      MED_CURE_MAIN t
                    WHERE
                      1 = 1
                      AND T.CURE_STATUS != '4' 
                      AND t.CURE_CREATE_DATE >= :BEGINCURE_CREATE_DATE
                      AND t.CURE_CREATE_DATE <= :ENDCURE_CREATE_DATE
                    GROUP BY 
                      t.HEMODIALYSIS_ID, t.PURIFICATION_MODE";
            }
        }

        /// <summary>
        /// 根据病人透析号和治疗单编号得到治疗单数量
        /// </summary>
        public string GetMainCureCountByCreateDate
        {
            get
            {
                return @"SELECT * FROM MED_CURE_MAIN WHERE HEMODIALYSIS_ID=:HEMODIALYSIS_ID  AND CURE_STATUS != '4' AND CURE_CREATE_DATE like to_date(:CURE_CREATE_DATE,'yyyy-mm-dd')";
            }
        }

        /// <summary>
        /// 药品参数匹配
        /// </summary>
        public string GetPamarsDrugInfo
        {
            get
            {
                return @"
                select z.create_date,z.HEMODIALYSIS_PARAMETERS_ID from (select t.* from med_hemodialysis_parameters t where (t.EXTENDED_FIELD_1 != '1' OR t.EXTENDED_FIELD_1 IS NULL) and t.cure_id=:CURE_ID and t.create_date <= to_date(:CREATE_DATE,'yyyy-mm-dd,hh24:mi:ss') order by t.create_date desc) z where rownum = 1";
            }
        }

        /// <summary>
        /// 得到治疗单打印列表 
        /// </summary>
        public string GetPrintCureList
        {
            get
            {
                return @"
                select  t.*,c.item_name as purification_mode_name,cm.cure_id,cm.cure_create_date from (
                select  m.item_name as bedName,p.name,p.sex,p.age,p.hemodialysis_id,r.purification_mode,r.frequency_hours,case t.banci_id when '1' then '上午'
                 when '2' then '下午' when '3' then '晚班' end as BanCiName,t.dialysis_date from med_patient_schedule t 
                inner join med_common_itemlist m on t.bed_number = m.item_id
                inner join med_patients p on p.hemodialysis_id = t.hemodialysis_id
                inner join med_hemo_recipe r on r.recipe_id = t.recipe_id
                where t.BANCI_ID LIKE '%'||:BANCI_ID||'%' and t.hemodialysis_id LIKE '%'||:HEMODIALYSIS_ID||'%' and P.NAME LIKE '%'||:NAME||'%' and to_char(t.dialysis_date,'yyyy/mm/dd') like '%'||:CURE_CREATE_DATE||'%' 
                order by t.dialysis_date desc
                ) t
                inner join 
                med_common_itemlist c on c.item_id = t.purification_mode
                 inner join MED_CURE_MAIN cm on cm.hemodialysis_id = t.hemodialysis_id
                where cm.CURE_STATUS != '4'  AND   to_char(cm.cure_create_date,'yyyy/mm/dd') like '%'||:CURE_CREATE_DATE||'%'                
                ";
            }
            //and t.dialysis_date like to_date(:CURE_CREATE_DATE,'yyyy-mm-dd') 
            // where cm.cure_create_date like to_date(:CURE_CREATE_DATE,'yyyy-mm-dd')                 
        }

        /// <summary>
        /// 计算患者透析次数
        /// </summary>
        public string GetCureCountByHemoID
        {
            get
            {
                return "select count(1) as curecount from med_cure_main where HEMODIALYSIS_ID = :HEMODIALYSIS_ID  AND CURE_STATUS != '4'";
            }
        }

        /// <summary>
        /// 删除透析参数
        /// </summary>
        public string DeleteHemodialysisParametersByID
        {

            get
            {//HEMODIALYSIS_PARAMETERS
                return "UPDATE MED_HEMODIALYSIS_PARAMETERS SET EXTENDED_FIELD_1 = '1' WHERE HEMODIALYSIS_PARAMETERS_ID = :HEMODIALYSIS_PARAMETERS_ID";
            }
        }

        /// <summary>
        /// 根据透析ID得到患者上次治疗单内容
        /// </summary>
        /// <returns></returns>
        public string GetLastTimeCureDataByID
        {
            get
            {
                return @"SELECT T.*
                        FROM MED_CURE_MAIN T
                        WHERE T.HEMODIALYSIS_ID = :HEMODIALYSIS_ID AND T.CURE_CREATE_DATE < :CURE_CREATE_DATE ORDER BY T.CURE_CREATE_DATE DESC";
            }
        }

        /// <summary>
        /// 根据透析ID得到患者历次透过体重及时间
        /// </summary>
        /// <returns></returns>
        public string GetDryWeightListByHemoID
        {
            get
            {
                return @"SELECT ROWNUM,A.* FROM (
                SELECT TO_CHAR(T.CURE_CREATE_DATE,'YYYY-MM-DD')||' | '||T.AFTER_DRY_WEIGHT as AHTERWEIGHT, T.ROWID 
                FROM MED_CURE_MAIN T WHERE T.HEMODIALYSIS_ID=:HEMODIALYSIS_ID  AND T.CURE_STATUS != '4'
                ORDER BY T.CURE_CREATE_DATE DESC) A WHERE ROWNUM <=20";
            }
        }

        /// <summary>
        /// 根据透析ID得到患者最近治疗单信息及时间
        /// </summary>
        /// <returns></returns>
        public string GetRecentCureInfoByHemoId
        {
            get
            {
                return @"SELECT ROWNUM,A.* FROM (
                SELECT TO_CHAR(T.CURE_CREATE_DATE,'YYYY.MM.DD')||'~'||T.AFTER_DRY_WEIGHT as AFTERWEIGHT, T.ROWID,T.* 
                FROM MED_CURE_MAIN T WHERE T.HEMODIALYSIS_ID=:HEMODIALYSIS_ID
                ORDER BY T.CURE_CREATE_DATE DESC) A WHERE ROWNUM <=3";
            }
        }

        /// <summary>
        /// 根据透析ID得到患者最近血压及时间
        /// </summary>
        /// <returns></returns>
        public string GetRecentPressureByHemoId
        {
            get
            {
                return @"SELECT ROWNUM,T.* FROM  (
                SELECT CREATE_DATE,TO_CHAR(P.CREATE_DATE,'YYYY.MM.DD')||'~'||P.SYSTOLIC_PRESSURE as DISPLAY_SYSTOLIC_PRESSURE,SYSTOLIC_PRESSURE,
                TO_CHAR(P.CREATE_DATE,'YYYY.MM.DD')||'~'||P.DIASTOLIC_PRESSURE as DISPLAY_DIASTOLIC_PRESSURE,DIASTOLIC_PRESSURE,
                TO_CHAR(P.CREATE_DATE,'YYYY.MM.DD')||'~'||P.CARDIOTACH as DISPLAY_CARDIOTACH,CARDIOTACH,
                RANK() OVER (PARTITION BY TRUNC(CREATE_DATE) ORDER BY CREATE_DATE DESC) PARAM_RANK
                FROM MED_HEMODIALYSIS_PARAMETERS P,MED_CURE_MAIN C
                WHERE  P.CURE_ID=C.CURE_ID AND C.HEMODIALYSIS_ID=:HEMODIALYSIS_ID
                ORDER BY P.CREATE_DATE DESC) T WHERE T.PARAM_RANK=1 AND ROWNUM <=3";
            }
        }

        /// <summary>
        /// 根据透析ID得到患者最近血压及时间与治疗记录
        /// </summary>
        /// <returns></returns>
        public string GetPatientCureAndPastPressureByHemoId
        {
            get
            {
                return @"SELECT ROWNUM,
                           T.HEMODIALYSIS_PARAMETERS_ID,
                           T.CREATE_DATE,
                           T.SYSTOLIC_PRESSURE,
                           T.DIASTOLIC_PRESSURE,
                           T.CARDIOTACH,
                           T.PARAM_RANK,
                           T.LOT,T.RECIPE_ID,
                           T.ITEM_NAME,T.AREA_NAME,
                           DECODE((SELECT COUNT(EPO.DRUG_NAME) FROM MED_CURE_DRUG EPO WHERE INSTR(EPO.DRUG_NAME,'促红素')>0 AND EPO.CURE_ID=T.CURE_ID),0,0,1)
                           ||'/'||DECODE((SELECT COUNT(FE.DRUG_NAME) FROM MED_CURE_DRUG FE WHERE INSTR(FE.DRUG_NAME,'蔗糖铁')>0 AND FE.CURE_ID=T.CURE_ID),0,0,1) EPO_FE,
                           T.CURE_ID,T.CURE_CREATE_DATE,T.BEFORE_DRY_WEIGHT,T.UFR,T.AFTER_DRY_WEIGHT,T.DRY_WEIGHT
                      FROM (SELECT CREATE_DATE,C.CURE_ID,C.CURE_CREATE_DATE,C.BEFORE_DRY_WEIGHT,C.UFR,C.AFTER_DRY_WEIGHT,C.RECIPE_ID,
                                   HEMODIALYSIS_PARAMETERS_ID,                                       
                                      C.BEFORE_SYSTOLIC_PRESSURE || '/' || c.before_diastolic_pressure AS SYSTOLIC_PRESSURE,
                               DIASTOLIC_PRESSURE,
                               C.BEFORE_BP AS CARDIOTACH,
                                   C.HEPARIN_SPECIES,
                                   (C.FIRST_HEPARIN + C.DOSIS_SUSTENTATIVA) AS LOT,C.DRY_WEIGHT,
                                   L.ITEM_NAME,ROOM.ITEM_NAME AS AREA_NAME,
                                   RANK() OVER(PARTITION BY TRUNC(P.CURE_ID) ORDER BY P.CREATE_DATE DESC) PARAM_RANK
                              FROM MED_HEMODIALYSIS_PARAMETERS P
                              LEFT JOIN MED_CURE_MAIN C ON P.CURE_ID = C.CURE_ID
                              LEFT JOIN MED_PATIENT_SCHEDULE S ON C.RECIPE_ID=S.RECIPE_ID
                              LEFT JOIN MED_COMMON_ITEMLIST ROOM ON S.DIALYSIS_ROOM_ID=ROOM.ITEM_ID
                              LEFT JOIN MED_COMMON_ITEMLIST L ON C.HEPARIN_SPECIES = L.ITEM_ID
                             WHERE C.HEMODIALYSIS_ID = :HEMODIALYSIS_ID AND C.CURE_STATUS !='4' 
                             ORDER BY P.CREATE_DATE DESC) T
                     WHERE T.PARAM_RANK = 1 AND ROWNUM<=10";
            }
        }

        /// <summary>
        /// 根据透析ID得到患者最近血压及时间与治疗记录
        /// </summary>
        /// <returns></returns>
        public string GetPatientCureAndPastPressureByParam
        {
            get
            {
                return @"SELECT ROWNUM,Z.* FROM (SELECT ROWNUM ROWTIP,
                                           T.HEMODIALYSIS_PARAMETERS_ID,
                                           T.CREATE_DATE,
                                           T.SYSTOLIC_PRESSURE,
                                           T.DIASTOLIC_PRESSURE,
                                           T.CARDIOTACH,
                                           T.PARAM_RANK,
                                           T.LOT,
                                           T.ITEM_NAME,
                                           T.CURE_ID,
                                           T.CURE_CREATE_DATE,
                                           T.BEFORE_DRY_WEIGHT,
                                           T.UFR,
                                           T.AFTER_DRY_WEIGHT,
                                           T.DRY_WEIGHT
                                      FROM (SELECT CREATE_DATE,
                                                   C.CURE_ID,
                                                   C.CURE_CREATE_DATE,
                                                   C.BEFORE_DRY_WEIGHT,
                                                   C.UFR,
                                                   C.AFTER_DRY_WEIGHT,
                                                   HEMODIALYSIS_PARAMETERS_ID,
                                                   SYSTOLIC_PRESSURE || '/' || DIASTOLIC_PRESSURE AS SYSTOLIC_PRESSURE,
                                                   DIASTOLIC_PRESSURE,
                                                   CARDIOTACH,
                                                   C.HEPARIN_SPECIES,
                                                   (C.FIRST_HEPARIN + C.DOSIS_SUSTENTATIVA) AS LOT,
                                                   C.DRY_WEIGHT,
                                                   L.ITEM_NAME,
                                                   RANK() OVER(PARTITION BY TRUNC(P.CURE_ID) ORDER BY P.CREATE_DATE DESC) PARAM_RANK
                                              FROM MED_HEMODIALYSIS_PARAMETERS P
                                              LEFT JOIN MED_CURE_MAIN C
                                                ON P.CURE_ID = C.CURE_ID
                                              LEFT JOIN MED_COMMON_ITEMLIST L
                                                ON C.HEPARIN_SPECIES = L.ITEM_ID
                                             WHERE C.HEMODIALYSIS_ID = :HEMODIALYSIS_ID
                                               AND C.CURE_STATUS != '4'
                                             ORDER BY P.CREATE_DATE DESC) T
                                     WHERE T.PARAM_RANK = 1
                                       AND ROWNUM <= :MAXDATA)Z
                             WHERE ROWTIP > :MINDATA";
            }
        }

        /// <summary>
        /// 根据透析ID得到患者历次治疗单信息
        /// </summary>
        public string GetPastCureInfoByHemoId
        {
            get
            {
                return @"SELECT ROWNUM, T.*
                        FROM (SELECT CURE_ID,
                                     CURE_CREATE_DATE,
                                     DISPLACEMENT_LIQUID,
                                     ZHFS.ITEM_NAME DISPLACEMENT_MODE,
                                     BLOOW_FLOW,
                                     UFR,
                                     ZHPF.ITEM_NAME DISPLACEMENT_RECIPE
                                     FROM MED_CURE_MAIN T
                                     LEFT JOIN MED_COMMON_ITEMLIST ZHFS ON T.DISPLACEMENT_MODE=ZHFS.ITEM_ID
                                     LEFT JOIN MED_COMMON_ITEMLIST ZHPF ON T.DISPLACEMENT_RECIPE=ZHPF.ITEM_ID
                                 WHERE HEMODIALYSIS_ID = :HEMODIALYSIS_ID
                                 ORDER BY CURE_CREATE_DATE DESC) T
                         WHERE ROWNUM <= 10";
            }
        }

        /// <summary>
        /// 根据透析ID得到患者历次血压信息
        /// </summary>
        public string GetPastPressureByHemoId
        {
            get
            {
                return @"SELECT ROWNUM,
                               T.HEMODIALYSIS_PARAMETERS_ID,
                               T.CREATE_DATE,
                               T.SYSTOLIC_PRESSURE,
                               T.DIASTOLIC_PRESSURE,
                               T.CARDIOTACH,
                               T.PARAM_RANK,
                               T.LOT,
                               T.ITEM_NAME
                          FROM (SELECT HEMODIALYSIS_PARAMETERS_ID,
                                       CREATE_DATE,
                                       SYSTOLIC_PRESSURE || '/' || DIASTOLIC_PRESSURE AS SYSTOLIC_PRESSURE,
                                       DIASTOLIC_PRESSURE,
                                       CARDIOTACH,
                                       C.HEPARIN_SPECIES,
                                       (C.FIRST_HEPARIN + C.DOSIS_SUSTENTATIVA) AS LOT,
                                       L.ITEM_NAME,
                                       RANK() OVER(PARTITION BY TRUNC(CREATE_DATE) ORDER BY CREATE_DATE DESC) PARAM_RANK
                                  FROM MED_HEMODIALYSIS_PARAMETERS P
                                  LEFT JOIN MED_CURE_MAIN C ON P.CURE_ID = C.CURE_ID
                                  LEFT JOIN MED_COMMON_ITEMLIST L ON C.HEPARIN_SPECIES = L.ITEM_ID
                                 WHERE C.HEMODIALYSIS_ID = :HEMODIALYSIS_ID
                                 ORDER BY P.CREATE_DATE DESC) T
                         WHERE T.PARAM_RANK = 1 AND ROWNUM<=10";
            }
        }

        /// <summary>
        /// 根据透析日期统计患者透析器个数，从透析单取，只有开始治疗才能有数据
        /// </summary>
        public string GetCurePurificationModeCountByDate
        {
            get
            {
                return @"SELECT M.ITEM_NAME, COUNT(M.ITEM_NAME) AS PCOUNT,'净化器类型' PTYPE
              FROM MED_CURE_MAIN T
              LEFT JOIN MED_COMMON_ITEMLIST C
                ON T.PURIFICATION_MODE = C.ITEM_ID
              LEFT JOIN MED_COMMON_ITEMLIST M
                ON T.MACHINE_TYPE = M.ITEM_ID
             WHERE TRUNC(T.CURE_CREATE_DATE) = TRUNC(:CURE_CREATE_DATE)  AND T.CURE_STATUS != '4'
             GROUP BY M.ITEM_NAME
            UNION
            SELECT (SELECT L.ITEM_NAME
                      FROM MED_COMMON_ITEMLIST L
                     WHERE L.ITEM_ID = T.PURIFICATION_MODE) ITEM_NAME,
                   COUNT(1) PCOUNT,'透析方式' PTYPE
              FROM MED_CURE_MAIN T
             WHERE TRUNC(T.CURE_CREATE_DATE) = TRUNC(:CURE_CREATE_DATE)  AND T.CURE_STATUS != '4'
             GROUP BY T.PURIFICATION_MODE
            UNION
            SELECT 'ALL' ITEM_NAME, COUNT(1) PCOUNT ,'透析方式' PTYPE
              FROM MED_CURE_MAIN T
             WHERE TRUNC(T.CURE_CREATE_DATE) = TRUNC(:CURE_CREATE_DATE)  AND T.CURE_STATUS != '4'
            ";
            }
        }

        /// <summary>
        /// 根据治疗单ID和班次获取CRRT治疗单记录
        /// </summary>
        public string GetCRRTCureByCureIdAndBanci
        {
            get
            {
                return @"SELECT T.*,PD.NAME AS PRIMARY_DOCTOR_NAME,PN.NAME AS PRIMARY_NURSE_NAME,CN.NAME AS CHECK_NURSE_NAME FROM MED_CURE_MAIN_CRRT T
                        LEFT JOIN MED_STAFF_DICT PD ON T.PRIMARY_DOCTOR=PD.EMP_NO
                        LEFT JOIN MED_STAFF_DICT PN ON T.PRIMARY_NURSE=PN.EMP_NO
                        LEFT JOIN MED_STAFF_DICT CN ON T.CHECK_NURSE=CN.EMP_NO
                        WHERE T.CURE_ID=:CURE_ID AND T.CRRT_CLASS=:CRRT_CLASS AND TRUNC(T.CREATE_DATE)=TRUNC(:CREATE_DATE)";
            }
        }

        /// <summary>
        /// 根据治疗单ID获取CRRT治疗单记录
        /// </summary>
        public string GetCRRTCureByCureId
        {
            get
            {
                return @"SELECT T.*,PD.NAME AS PRIMARY_DOCTOR_NAME,PN.NAME AS PRIMARY_NURSE_NAME,CN.NAME AS CHECK_NURSE_NAME FROM MED_CURE_MAIN_CRRT T
                        LEFT JOIN MED_STAFF_DICT PD ON T.PRIMARY_DOCTOR=PD.EMP_NO
                        LEFT JOIN MED_STAFF_DICT PN ON T.PRIMARY_NURSE=PN.EMP_NO
                        LEFT JOIN MED_STAFF_DICT CN ON T.CHECK_NURSE=CN.EMP_NO
                        WHERE T.CURE_ID=:CURE_ID ORDER BY T.CREATE_DATE";
            }
        }

        #endregion

        #region 预约申请相关SQL

        /// <summary>
        /// 获取预约申请数据SQL 
        /// </summary>
        public string GetHemodialysisApplyList
        {
            get
            {
                return @"
                    SELECT 
                        * 
                    FROM MED_HEMO_APPLY t
                    WHERE
                        t.HEMODIALYSIS_ID = :HEMODIALYSIS_ID";
            }
        }

        /// <summary>
        /// 删除预约申请数据SQL
        /// </summary>
        public string DeleteHemodialysisApply
        {
            get
            {
                return @"
                    DELETE FROM MED_HEMO_APPLY t
                    WHERE
                        t.APPLY_ID = :APPLY_ID";
            }
        }

        #endregion

        #region 检验相关SQL
        public string GetMedPatientQualityData
        {
            get
            {
                return @"SELECT T.REPORT_ADD_DATE,
                                 (SELECT S.NAME
                                  FROM MED_PATIENTS S
                                 WHERE S.HEMODIALYSIS_ID = T.HEMODIALYSIS_ID) NAME,
                               T.SYSTOLIC_PRESSURE || '/' || T.DIASTOLIC_PRESSURE AS PJXY,
                               TO_CHAR(ROUND(T.PJHEMODATE, 2)) AS PJHEMODATE,
                               T.RZQC,
                               T.SJPXJZ,
                               T.GLCJ,
                               T.JZPXKJ,
                               T.ZGSSD,
                               T.ZGQDBS,
                               T.ZGZDBS
                          FROM MED_REPORT_PATIENTDATE T
                        WHERE T.REPORT_ADD_DATE >= :STARTDATE
                          AND T.REPORT_ADD_DATE <= :ENDDATE";
            }
        }

        /// <summary>
        /// 获取检验数据SQL 
        /// </summary>
        public string GetPatientLabList
        {
            get
            {
                return @"SELECT M.TEST_NO,
                           M.SPECIMEN,
                           M.RESULTS_RPT_DATE_TIME,
                            M.TEST_CAUSE AS SUBJECT,
                           I.ITEM_NO,
                           I.ITEM_NAME,
                           R.REPORT_ITEM_NAME,
                           R.RESULT,
                           R.UNITS,
                           R.REFERENCE_RESULT
                      FROM MED_LAB_TEST_MASTER M
                      LEFT JOIN MED_LAB_TEST_ITEMS I
                        ON M.TEST_NO = I.TEST_NO
                      LEFT JOIN MED_LAB_RESULT R
                        ON M.TEST_NO = R.TEST_NO
                     WHERE 1 = 1
                       AND M.BARCODE = :PATIENT_ID
                     ORDER BY M.RESULTS_RPT_DATE_TIME DESC,M.TEST_NO, I.ITEM_NO";
            }
        }

        /// <summary>
        /// 根据日期范围和项目获取检验数据
        /// </summary>
        public string GetLabListByDateAndItems
        {
            get
            {
                return @"
                        SELECT PAT.NAME,PAT.SEX,
                               PAT.HEMODIALYSIS_ID,PAT.PATIENT_ID,
                               M.TEST_NO,
                               M.SPECIMEN,
                               M.RESULTS_RPT_DATE_TIME,
                               M.TEST_CAUSE AS SUBJECT,
                               I.ITEM_NO,
                               I.ITEM_NAME,
                               R.REPORT_ITEM_NAME,
                               R.RESULT,
                               R.UNITS,
                               R.REFERENCE_RESULT
                          FROM MED_PATIENTS PAT
                         INNER JOIN MED_LAB_TEST_MASTER M
                            ON PAT.PATIENT_ID = M.PATIENT_ID OR PAT.ADMISSION_NUMBER = M.PATIENT_ID OR PAT.HEMODIALYSIS_ID=M.BARCODE
                         INNER JOIN MED_LAB_TEST_ITEMS I
                            ON M.TEST_NO = I.TEST_NO
                         INNER JOIN MED_LAB_RESULT R
                            ON m.TEST_NO = R.TEST_NO
                         WHERE 1 = 1 ";
            }
        }
        /// <summary>
        /// 根据日期范围和项目和患者信息获取检验数据
        /// </summary>
        public string GetLabListByDateAndItemsAndHemoInfo
        {
            get
            {
                return @"
                        SELECT 
                               PAT.NAME,PAT.SEX,PAT.HEMODIALYSIS_ID,PAT.PATIENT_ID,
                               M.TEST_NO,
                               M.SPECIMEN,
                               M.RESULTS_RPT_DATE_TIME,
                               M.TEST_CAUSE  AS SUBJECT,
                               I.ITEM_NO,
                               I.ITEM_NAME,
                               R.REPORT_ITEM_NAME,
                               R.RESULT,
                               R.UNITS,
                               R.REFERENCE_RESULT
                          FROM MED_PATIENTS PAT
                         INNER JOIN MED_LAB_TEST_MASTER M
                            ON PAT.PATIENT_ID = M.PATIENT_ID OR PAT.ADMISSION_NUMBER = M.PATIENT_ID OR PAT.HEMODIALYSIS_ID=M.BARCODE
                         INNER JOIN MED_LAB_TEST_ITEMS I
                            ON M.TEST_NO = I.TEST_NO
                         INNER JOIN MED_LAB_RESULT R
                            ON m.TEST_NO = R.TEST_NO
                         WHERE 1 = 1";
            }
        }


        /// <summary>
        /// 根据检验时间段获取检验数据SQL
        /// 现场应用
        /// </summary>
        public string GetPatientLabListByDate
        {
            get
            {
                return @"
                    SELECT
                      m.TEST_NO, m.SPECIMEN,m.TEST_CAUSE,m.RESULTS_RPT_DATE_TIME,  M.TEST_CAUSE AS SUBJECT,
                      i.ITEM_NO, i.ITEM_NAME,
                      r.REPORT_ITEM_NAME, r.RESULT, r.UNITS, r.REFERENCE_RESULT
                    FROM MED_LAB_TEST_MASTER m
                    LEFT JOIN MED_LAB_TEST_ITEMS i ON m.TEST_NO = i.TEST_NO
                    LEFT JOIN MED_LAB_RESULT r ON m.TEST_NO = r.TEST_NO
                    WHERE 1 = 1 AND m.BARCODE = :PATIENT_ID
                      AND m.RESULTS_RPT_DATE_TIME >= :STARTDATE 
                      AND m.RESULTS_RPT_DATE_TIME <= :ENDDATE      
                    ORDER BY m.RESULTS_RPT_DATE_TIME DESC,
                      m.TEST_NO, i.ITEM_NO";
            }
        }

        /// <summary>
        /// 根据检验时间段获取检验数据SQL
        /// 本地开发测试应用
        /// </summary>
        //        public string GetPatientLabListByDate
        //        {
        //            get
        //            {
        //                return @"
        //                    SELECT
        //                      m.TEST_NO, m.SPECIMEN,m.TEST_CAUSE,m.RESULTS_RPT_DATE_TIME,m.SUBJECT,
        //                      i.ITEM_NO, i.ITEM_NAME,
        //                      r.REPORT_ITEM_NAME, r.RESULT, r.UNITS, r.Reference_Result
        //                    FROM MEDCOMM.MED_LAB_TEST_MASTER m
        //                    LEFT JOIN MEDCOMM.MED_LAB_TEST_ITEMS i ON m.TEST_NO = i.TEST_NO
        //                    LEFT JOIN MEDCOMM.MED_LAB_RESULT r ON m.TEST_NO = r.TEST_NO
        //                    WHERE 1 = 1 AND m.PATIENT_ID = :PATIENT_ID
        //                      AND m.RESULTS_RPT_DATE_TIME >= :STARTDATE 
        //                      AND m.RESULTS_RPT_DATE_TIME <= :ENDDATE      
        //                    ORDER BY m.RESULTS_RPT_DATE_TIME DESC,
        //                      m.TEST_NO, i.ITEM_NO";
        //            }
        //        }

        /// <summary>
        /// 根据PatientId获取患者检查列表
        /// </summary>
        public string GetPatientExamList
        {
            get
            {
                return @"SELECT T.* FROM MED_EXAM_MASTER T WHERE T.PATIENT_ID = :PATIENT_ID
                        ORDER BY T.EXAM_DATE_TIME DESC,T.EXAM_NO";
            }
        }

        /// <summary>
        /// 根据PatientId和日期获取患者检查列表
        /// </summary>
        public string GetPatientExamListByDate
        {
            get
            {
                return @"SELECT T.* FROM MED_EXAM_MASTER T WHERE T.PATIENT_ID = :PATIENT_ID
                        AND T.EXAM_DATE_TIME >= :STARTDATE AND T.EXAM_DATE_TIME <= :ENDDATE ORDER BY T.EXAM_DATE_TIME DESC,T.EXAM_NO";
            }
        }

        /// <summary>
        /// 根据检查序号获取患者检查明细列表
        /// </summary>
        public string GetPatientExamDetailListByNo
        {
            get
            {
                return @"SELECT I.EXAM_NO,I.EXAM_ITEM_NO,I.EXAM_ITEM,R.DESCRIPTION,R.IMPRESSION,R.RECOMMENDATION,DECODE(R.IS_ABNORMAL,'1','阳性','阴性') IS_ABNORMAL
                        FROM MED_EXAM_ITEMS I INNER JOIN MED_EXAM_REPORT R ON I.EXAM_NO=R.EXAM_NO
                        WHERE I.EXAM_NO = :EXAM_NO";
            }
        }

        /// <summary>
        /// 获取三个月内常规检验数据SQL
        /// </summary>
        public string GetThreeMonthsCommonLabList
        {
            get
            {
                return @"SELECT p.PATIENT_ID,
                       p.NAME,
                       p.ADMISSION_NUMBER,
                       p.SEX,
                       p.BIRTHDAY,
                       p.AGE,
                       p.NATIVEPLACE,
                       p.JOB,
                       p.MARITAL,
                       p.CREDENTIALS_TYPE,
                       p.CREDENTIALS_NUMBER,
                       p.EDUCATION,
                       p.NATION,
                       p.ADDRESS,
                       p.MEDICAL_TYPE,
                       p.TELEPHONE,
                       m.EXECUTE_DATE,
                       WM_CONCAT(m.SUBJECT) MERGE_SUBJECT,
                       DECODE(INSTR(WM_CONCAT(m.SUBJECT), '血常规'), 0, '否', '是') XUECHANGGUI,
                       DECODE(INSTR(WM_CONCAT(m.SUBJECT), '生化'), 0, '否', '是') SHENGHUA,
                       DECODE(INSTR(WM_CONCAT(m.SUBJECT), '电解质'), 0, '否', '是') DIANJIEZHI,
                       DECODE(INSTR(WM_CONCAT(m.SUBJECT), '血脂'), 0, '否', '是') XUEZHI,
                       DECODE(INSTR(WM_CONCAT(m.SUBJECT), 'C-反应蛋白'), 0, '否', '是') FANYINGDANBAI,
                       DECODE(INSTR(WM_CONCAT(m.SUBJECT), '甲状旁腺素'), 0, '否', '是') JIAZHUANGSU,
                       DECODE(INSTR(WM_CONCAT(m.SUBJECT), '铁'), 0, '否', '是') TIE
                  FROM MED_LAB_TEST_MASTER m
                 INNER JOIN (SELECT * FROM MED_PATIENTS WHERE TIME_TYPE LIKE '%' || :PATIENT_TYPE || '%') p
                    ON m.PATIENT_ID = P.PATIENT_ID
                 WHERE m.EXECUTE_DATE >= ADD_MONTHS(SYSDATE, -3)
                   AND (SUBJECT LIKE '%血常规%' OR SUBJECT LIKE '%生化%' OR SUBJECT LIKE '%电解质%' OR
                       SUBJECT LIKE '%血脂%' OR SUBJECT LIKE '%C-反应蛋白%' OR
                       SUBJECT LIKE '%甲状旁腺素%' OR SUBJECT LIKE '%铁%')
                 GROUP BY p.PATIENT_ID,
                          m.EXECUTE_DATE,
                          p.NAME,
                          p.ADMISSION_NUMBER,
                          p.SEX,
                          p.BIRTHDAY,
                          p.AGE,
                          p.NATIVEPLACE,
                          p.JOB,
                          p.MARITAL,
                          p.CREDENTIALS_TYPE,
                          p.CREDENTIALS_NUMBER,
                          p.EDUCATION,
                          p.NATION,
                          p.ADDRESS,
                          p.MEDICAL_TYPE,
                          p.TELEPHONE
                 ORDER BY p.PATIENT_ID, m.EXECUTE_DATE";
            }
        }

        /// <summary>
        /// 获取半年内常规检验数据SQL
        /// </summary>
        public string GetSixMonthsCommonLabList
        {
            get
            {
                return @" SELECT p.PATIENT_ID,
                        p.NAME,
                        p.ADMISSION_NUMBER,
                        p.SEX,
                        p.BIRTHDAY,
                        p.AGE,
                        p.NATIVEPLACE,
                        p.JOB,
                        p.MARITAL,
                        p.CREDENTIALS_TYPE,
                        p.CREDENTIALS_NUMBER,
                        p.EDUCATION,
                        p.NATION,
                        p.ADDRESS,
                        p.MEDICAL_TYPE,
                        p.TELEPHONE,
                        m.EXECUTE_DATE,
                        WM_CONCAT(m.SUBJECT) MERGE_SUBJECT,
                        DECODE(INSTR(WM_CONCAT(m.SUBJECT), '乙肝'), 0, '否', '是') YIGAN,
                        DECODE(INSTR(WM_CONCAT(m.SUBJECT), '丙肝'), 0, '否', '是') BINGGAN,
                        DECODE(INSTR(WM_CONCAT(m.SUBJECT), '梅毒'), 0, '否', '是') MEIDU,
                        DECODE(INSTR(WM_CONCAT(m.SUBJECT), '艾滋病'), 0, '否', '是') AIZIBING
                   FROM MED_LAB_TEST_MASTER m
                  INNER JOIN (SELECT * FROM MED_PATIENTS WHERE TIME_TYPE LIKE '%' || :PATIENT_TYPE || '%') p
                     ON m.PATIENT_ID = P.PATIENT_ID
                  WHERE m.EXECUTE_DATE >= ADD_MONTHS(SYSDATE, -6)
                    AND (SUBJECT LIKE '%乙肝%' OR SUBJECT LIKE '%丙肝%' OR SUBJECT LIKE '%梅毒%' OR
                        SUBJECT LIKE '%艾滋病%')
                  GROUP BY p.PATIENT_ID,
                           m.EXECUTE_DATE,
                           p.NAME,
                           p.ADMISSION_NUMBER,
                           p.SEX,
                           p.BIRTHDAY,
                           p.AGE,
                           p.NATIVEPLACE,
                           p.JOB,
                           p.MARITAL,
                           p.CREDENTIALS_TYPE,
                           p.CREDENTIALS_NUMBER,
                           p.EDUCATION,
                           p.NATION,
                           p.ADDRESS,
                           p.MEDICAL_TYPE,
                           p.TELEPHONE
                  ORDER BY p.PATIENT_ID, m.EXECUTE_DATE";
            }
        }


        /// <summary>
        /// 获取三个月内常规检验数据SQL
        /// </summary>
        public string GetThreeMonthsCommonLabListByHemoID
        {
            get
            {
                return @"SELECT p.PATIENT_ID,
                                   p.NAME,
                                   p.ADMISSION_NUMBER,
                                   p.SEX,
                                   p.BIRTHDAY,
                                   p.AGE,
                                   p.NATIVEPLACE,
                                   p.JOB,
                                   p.MARITAL,
                                   p.CREDENTIALS_TYPE,
                                   p.CREDENTIALS_NUMBER,
                                   p.EDUCATION,
                                   p.NATION,
                                   p.ADDRESS,
                                   p.MEDICAL_TYPE,
                                   p.TELEPHONE,
                                   m.EXECUTE_DATE,
                                   WM_CONCAT(m.SUBJECT) MERGE_SUBJECT,
                                   DECODE(INSTR(WM_CONCAT(m.SUBJECT), '血常规'), 0, '否', '是') XUECHANGGUI,
                                   DECODE(INSTR(WM_CONCAT(m.SUBJECT), '生化'), 0, '否', '是') SHENGHUA,
                                   DECODE(INSTR(WM_CONCAT(m.SUBJECT), '电解质'), 0, '否', '是') DIANJIEZHI,
                                   DECODE(INSTR(WM_CONCAT(m.SUBJECT), '血脂'), 0, '否', '是') XUEZHI,
                                   DECODE(INSTR(WM_CONCAT(m.SUBJECT), 'C-反应蛋白'), 0, '否', '是') FANYINGDANBAI,
                                   DECODE(INSTR(WM_CONCAT(m.SUBJECT), '甲状旁腺素'), 0, '否', '是') JIAZHUANGSU,
                                   DECODE(INSTR(WM_CONCAT(m.SUBJECT), '铁'), 0, '否', '是') TIE,
 DECODE(INSTR(WM_CONCAT(m.SUBJECT), '肝功能'), 0, '否', '是') GANGONGNEG,
 DECODE(INSTR(WM_CONCAT(m.SUBJECT), '肾功能'), 0, '否', '是') SHENGGONGNENG
                              FROM MED_LAB_TEST_MASTER m
                             INNER JOIN MED_PATIENTS p
                                ON m.PATIENT_ID = P.PATIENT_ID
                             WHERE P.HEMODIALYSIS_ID=:HEMODIALYSIS_ID
                               AND m.EXECUTE_DATE >= ADD_MONTHS(SYSDATE, -3)
                               AND (SUBJECT LIKE '%血常规%' OR SUBJECT LIKE '%生化%' OR SUBJECT LIKE '%电解质%' OR
                                   SUBJECT LIKE '%血脂%' OR SUBJECT LIKE '%C-反应蛋白%' OR
                                   SUBJECT LIKE '%甲状旁腺素%' OR SUBJECT LIKE '%铁%' OR SUBJECT LIKE '%肝功能%' OR SUBJECT LIKE '%肾功能%')
                             GROUP BY p.PATIENT_ID,
                                      m.EXECUTE_DATE,
                                      p.NAME,
                                      p.ADMISSION_NUMBER,
                                      p.SEX,
                                      p.BIRTHDAY,
                                      p.AGE,
                                      p.NATIVEPLACE,
                                      p.JOB,
                                      p.MARITAL,
                                      p.CREDENTIALS_TYPE,
                                      p.CREDENTIALS_NUMBER,
                                      p.EDUCATION,
                                      p.NATION,
                                      p.ADDRESS,
                                      p.MEDICAL_TYPE,
                                      p.TELEPHONE
                             ORDER BY p.PATIENT_ID, m.EXECUTE_DATE";
            }
        }

        /// <summary>
        /// 获取三个月内常规传染检验数据SQL
        /// </summary>
        public string GetSixMonthsCommonLabListByHemoID
        {
            get
            {
                return @"SELECT p.PATIENT_ID,
                                   p.NAME,
                                   p.ADMISSION_NUMBER,
                                   p.SEX,
                                   p.BIRTHDAY,
                                   p.AGE,
                                   p.NATIVEPLACE,
                                   p.JOB,
                                   p.MARITAL,
                                   p.CREDENTIALS_TYPE,
                                   p.CREDENTIALS_NUMBER,
                                   p.EDUCATION,
                                   p.NATION,
                                   p.ADDRESS,
                                   p.MEDICAL_TYPE,
                                   p.TELEPHONE,
                                   m.EXECUTE_DATE,
                                   WM_CONCAT(m.SUBJECT) MERGE_SUBJECT,
                                   DECODE(INSTR(WM_CONCAT(m.SUBJECT), '乙肝'), 0, '否', '是') YIGAN,
                                   DECODE(INSTR(WM_CONCAT(m.SUBJECT), '丙肝'), 0, '否', '是') BINGGAN,
                                   DECODE(INSTR(WM_CONCAT(m.SUBJECT), '梅毒'), 0, '否', '是') MEIDU,
                                   DECODE(INSTR(WM_CONCAT(m.SUBJECT), '艾滋病'), 0, '否', '是') AIZIBING
                              FROM MED_LAB_TEST_MASTER m
                             INNER JOIN MED_PATIENTS p
                                ON m.PATIENT_ID = P.PATIENT_ID
                             WHERE P.HEMODIALYSIS_ID=:HEMODIALYSIS_ID AND P.INFECTIOUS_CHECK_RESULT IS NOT NULL
                               AND m.EXECUTE_DATE >= ADD_MONTHS(SYSDATE, -6)
                               AND (SUBJECT LIKE '%乙肝%' OR SUBJECT LIKE '%丙肝%' OR SUBJECT LIKE '%梅毒%' OR
                                   SUBJECT LIKE '%艾滋病%')
                             GROUP BY p.PATIENT_ID,
                                      m.EXECUTE_DATE,
                                      p.NAME,
                                      p.ADMISSION_NUMBER,
                                      p.SEX,
                                      p.BIRTHDAY,
                                      p.AGE,
                                      p.NATIVEPLACE,
                                      p.JOB,
                                      p.MARITAL,
                                      p.CREDENTIALS_TYPE,
                                      p.CREDENTIALS_NUMBER,
                                      p.EDUCATION,
                                      p.NATION,
                                      p.ADDRESS,
                                      p.MEDICAL_TYPE,
                                      p.TELEPHONE
                             ORDER BY p.PATIENT_ID, m.EXECUTE_DATE";
            }
        }

        #endregion

        #region 医嘱相关SQL

        /// <summary>
        /// 获取医嘱数据SQL 
        /// </summary>
        public string GetOrderList
        {
            get
            {
                return @"
                    SELECT 
                      o.*, e.NURSE DRUG_EXEC_NURSE, e.EXCUTE_DATE DRUG_EXEC_EXCUTE_DATE,
                      CASE
                        WHEN e.NURSE IS NULL THEN '尚未执行'
                        ELSE '执行完毕'
                      END ORDER_EXEC_STATUS
                    FROM MED_ORDERS o 
                    LEFT JOIN MED_DRUG_EXEC e ON o.ORDER_NO = e.DRUG_ORDER_ID
                    WHERE 
                      EXISTS (SELECT p.PATIENT_ID FROM MED_PATIENTS p WHERE o.PATIENT_ID = p.PATIENT_ID)
                      AND o.PATIENT_ID = :PATIENT_ID 
                      AND o.ENTER_DATE_TIME >= :BEGINENTER_DATE_TIME 
                      AND o.ENTER_DATE_TIME <= :ENDENTER_DATE_TIME        
                      AND o.ORDER_CLASS = 'A' 
                      AND o.ORDER_STATUS = 2
                    ORDER BY o.ORDER_NO, o.ORDER_SUB_NO";
            }
        }

        /// <summary>
        /// 获取医嘱数据SQL 
        /// </summary>
        public string GetOrderList4Erythropoietin
        {
            get
            {
                return @"
                    SELECT 
                      o.*, e.NURSE DRUG_EXEC_NURSE, e.EXCUTE_DATE DRUG_EXEC_EXCUTE_DATE,
                      CASE
                        WHEN e.NURSE IS NULL THEN '尚未执行'
                        ELSE '执行完毕'
                      END ORDER_EXEC_STATUS
                    FROM MED_ORDERS o 
                    LEFT JOIN MED_ERYTHROPOIETIN_EXEC e ON o.ORDER_NO = e.DRUG_ORDER_ID
                    WHERE 
                      EXISTS (SELECT p.PATIENT_ID FROM MED_PATIENTS p WHERE o.PATIENT_ID = p.PATIENT_ID)
                      AND o.PATIENT_ID = :PATIENT_ID 
                      AND o.ENTER_DATE_TIME >= :BEGINENTER_DATE_TIME 
                      AND o.ENTER_DATE_TIME <= :ENDENTER_DATE_TIME        
                      AND o.ORDER_CLASS = 'A' 
                      AND o.ORDER_STATUS = 2
                    ORDER BY o.ORDER_NO, o.ORDER_SUB_NO";
            }
        }
        public string GetTemplateByParmas
        {
            get
            {
                return @"SELECT *
                          FROM MED_CURE_DRUG_TEMPLATE
                         WHERE DOCTOR_ID LIKE '%' || :DOCTOR_ID || '%'";
            }
        }
        #endregion

        #region 促红素相关SQL

        /// <summary>
        /// 获取促红素数据SQL 
        /// </summary>
        public string GetErythropoietinList
        {
            get
            {
                return @"
                    SELECT
                      e.*, 
                      (
                      SELECT 
                        x.EXCUTE_DATE
                      FROM 
                      (
                        SELECT 
                          x.* 
                        FROM MED_ERYTHROPOIETIN_EXEC x 
                        ORDER BY 
                          x.EXCUTE_DATE DESC
                      ) x
                      WHERE 
                        x.ERYTHROPOIETIN_ID = e.ERYTHROPOIETIN_ID 
                        AND ROWNUM = 1
                      ) LAST_EXCUTE_DATE,
                      e.QW || ' || ' || e.TIME_TYPE || ' || ' || e.FREQUENCY FREQUENCYSTR,
                      p.NAME PATIENTNAME, p.BED_NO, 
                      d.DRUG_NAME
                    FROM MED_ERYTHROPOIETIN e
                      INNER JOIN MED_PATIENTS p ON e.HEMODIALYSIS_ID = p.HEMODIALYSIS_ID
                      INNER JOIN MED_DRUG_MASTER d ON e.DRUG_CODE = d.DRUG_CODE  
                    WHERE
                      1 = 1
                      AND e.HEMODIALYSIS_ID = :HEMODIALYSIS_ID
                    ORDER BY
                      e.CREATE_TIME";
            }
        }

        /// <summary>
        /// 获取促红素数据SQL 
        /// </summary>
        public string GetErythropoietinListByTimeSpan
        {
            get
            {
                return @"
                    SELECT
                      e.*
                    FROM MED_ERYTHROPOIETIN e
                    WHERE
                      1 = 1
                      AND e.HEMODIALYSIS_ID = :HEMODIALYSIS_ID
                      AND e.CREATE_TIME >= :BEGINCREATE_TIME 
                      AND e.CREATE_TIME <= :ENDCREATE_TIME";
            }
        }

        /// <summary>
        /// 获取促红素执行数据SQL 
        /// </summary>
        public string GetErythropoietinExecList
        {
            get
            {
                return @"
                    SELECT
                      x.*, e.ERYTHROPOIETIN_TYPE, dm.ITEM_NAME DRUG_MODESTR, e.DOSAGE, unit.ITEM_NAME UNITSTR
                    FROM MED_ERYTHROPOIETIN e
                      INNER JOIN MED_ERYTHROPOIETIN_EXEC x ON e.ERYTHROPOIETIN_ID = x.ERYTHROPOIETIN_ID
                      INNER JOIN MED_COMMON_ITEMLIST dm on e.DRUG_MODE = dm.ITEM_ID
                      INNER JOIN MED_COMMON_ITEMLIST unit on e.UNIT = unit.ITEM_ID  
                    WHERE
                      1 = 1
                      AND x.ERYTHROPOIETIN_ID = :ERYTHROPOIETIN_ID
                      AND x.EXCUTE_DATE >= :BEGINEXEC_DATE_TIME 
                      AND x.EXCUTE_DATE <= :ENDEXEC_DATE_TIME     
                    ORDER BY
                      x.EXCUTE_DATE";
            }
        }

        #endregion

        #region 系统消息相关SQL

        /// <summary>
        /// 获取所有启用的系统消息数据SQL 
        /// </summary>
        public string GetAllMessage
        {
            get
            {
                //STATUS 1：未读；2：已读
                return @"
                    SELECT
                      *
                    FROM MED_COMMON_MESSAGE m
                    WHERE
                      1 = 1
                      AND m.TYPE = :TYPE
                      AND m.STATUS = '1'
                    ORDER BY
                      m.CREATETIME DESC";
            }
        }

        #endregion

        #region 报表相关
        /// <summary>
        /// 根据透析类型得到时间段内透析次数
        /// </summary>
        public string GetAllCureTypeCount
        {
            get
            {
                return @"
                   select 'HD' AS PURIFICATION_MODE ,count(1) AS AllCount  from MED_CURE_MAIN t where t.CURE_CREATE_DATE >= :BEGINCURE_CREATE_DATE
                      AND t.CURE_CREATE_DATE <= :ENDCURE_CREATE_DATE AND T.CURE_STATUS != '4' and t.PURIFICATION_MODE =( 
                     select item_id from med_common_itemlist where item_name= 'HD' and item_type='净化方式') 
                    union all
                    select 'HDF' AS PURIFICATION_MODE ,count(1) AS AllCount  from MED_CURE_MAIN t where t.CURE_CREATE_DATE >= :BEGINCURE_CREATE_DATE
                      AND t.CURE_CREATE_DATE <= :ENDCURE_CREATE_DATE  AND T.CURE_STATUS != '4' and t.PURIFICATION_MODE =( 
                     select item_id from med_common_itemlist where item_name= 'HDF' and item_type='净化方式') 
                     union all
                    select 'HF' AS PURIFICATION_MODE ,count(1) AS AllCount  from MED_CURE_MAIN t where  t.CURE_CREATE_DATE >= :BEGINCURE_CREATE_DATE
                      AND t.CURE_CREATE_DATE <= :ENDCURE_CREATE_DATE AND T.CURE_STATUS != '4' and t.PURIFICATION_MODE =( 
                     select item_id from med_common_itemlist where item_name= 'HF' and item_type='净化方式') 
                      union all
                    select 'HP' AS PURIFICATION_MODE ,count(1) AS AllCount  from MED_CURE_MAIN t where t.CURE_CREATE_DATE >= :BEGINCURE_CREATE_DATE
                      AND t.CURE_CREATE_DATE <= :ENDCURE_CREATE_DATE AND T.CURE_STATUS != '4' and t.PURIFICATION_MODE =( 
                     select item_id from med_common_itemlist where item_name= 'HP' and item_type='净化方式') 
                       union all
                    select 'HD+HP' AS PURIFICATION_MODE ,count(1) AS AllCount  from MED_CURE_MAIN t where t.CURE_CREATE_DATE >= :BEGINCURE_CREATE_DATE
                      AND t.CURE_CREATE_DATE <= :ENDCURE_CREATE_DATE AND T.CURE_STATUS != '4' and t.PURIFICATION_MODE =( 
                     select item_id from med_common_itemlist where item_name= 'HD+HP' and item_type='净化方式')    
                     union all
                    select 'CRRT' AS PURIFICATION_MODE ,count(1) AS AllCount  from MED_CURE_MAIN t where   t.CURE_CREATE_DATE >= :BEGINCURE_CREATE_DATE
                      AND t.CURE_CREATE_DATE <= :ENDCURE_CREATE_DATE AND T.CURE_STATUS != '4' and t.PURIFICATION_MODE =( 
                     select item_id from med_common_itemlist where item_name= 'CRRT' AND ITEM_TYPE = '净化方式') ";
            }
        }

        /// <summary>
        /// 根据月份得到透析次数
        /// </summary>
        public string GetAllCureCountByMonth
        {
            get
            {
                return @"SELECT TO_CHAR(CURE_CREATE_DATE, 'YYYY-MM') curemonth, COUNT(1) AS COUNT_1
                          FROM MED_CURE_MAIN T
                         WHERE TRUNC(T.CURE_CREATE_DATE) >= TRUNC(:BEGINTIME)
                           AND TRUNC(T.CURE_CREATE_DATE) <= TRUNC(:ENDTIME)
                                AND T.CURE_STATUS != '4'
                         GROUP BY TO_CHAR(CURE_CREATE_DATE, 'YYYY-MM') 
                        HAVING(COUNT(1) > 0) ORDER BY TO_CHAR(CURE_CREATE_DATE, 'YYYY-MM')";
                #region old
                /*
                select  '1月' as curemonth, count(1) AS count_1  
                from MED_CURE_MAIN t where t.CURE_CREATE_DATE >=  :BEGINTIME
                AND t.CURE_CREATE_DATE <= :ENDTIME
                union all
                select  '2月' as curemonth, count(1) AS count_1  
                from MED_CURE_MAIN t where t.CURE_CREATE_DATE >= :BEGINTIME
                AND t.CURE_CREATE_DATE <= :ENDTIME
                union all
                select  '3月' as curemonth, count(1) AS count_1  
                from MED_CURE_MAIN t where t.CURE_CREATE_DATE >= :BEGINTIME
                AND t.CURE_CREATE_DATE <= :ENDTIME
                union all
                select  '4月' as curemonth, count(1) AS count_1  
                from MED_CURE_MAIN t where t.CURE_CREATE_DATE >=:BEGINTIME
                AND t.CURE_CREATE_DATE <= :ENDTIME
                union all
                select  '5月' as curemonth, count(1) AS count_1  
                from MED_CURE_MAIN t where t.CURE_CREATE_DATE >= :BEGINTIME
                AND t.CURE_CREATE_DATE <= :ENDTIME
                union all
                select  '6月' as curemonth, count(1) AS count_1  
                from MED_CURE_MAIN t where t.CURE_CREATE_DATE >=:BEGINTIME
                AND t.CURE_CREATE_DATE <=  :ENDTIME
                union all
                select  '7月' as curemonth, count(1) AS count_1  
                from MED_CURE_MAIN t where t.CURE_CREATE_DATE >= :BEGINTIME
                AND t.CURE_CREATE_DATE <= :ENDTIME
                union all
                select  '8月' as curemonth, count(1) AS count_1  
                from MED_CURE_MAIN t where t.CURE_CREATE_DATE >= :BEGINTIME
                AND t.CURE_CREATE_DATE <=:ENDTIME
                union all
                select  '9月' as curemonth, count(1) AS count_1  
                from MED_CURE_MAIN t where t.CURE_CREATE_DATE >= :BEGINTIME
                AND t.CURE_CREATE_DATE <= :ENDTIME
                union all
                select  '10月' as curemonth, count(1) AS count_1  
                from MED_CURE_MAIN t where t.CURE_CREATE_DATE >= :BEGINTIME
                AND t.CURE_CREATE_DATE <= :ENDTIME
                union all
                select  '11月' as curemonth, count(1) AS count_1  
                from MED_CURE_MAIN t where t.CURE_CREATE_DATE >= :BEGINTIME
                AND t.CURE_CREATE_DATE <= :ENDTIME
                union all
                select  '12月' as curemonth, count(1) AS count_1  
                from MED_CURE_MAIN t where t.CURE_CREATE_DATE >= :BEGINTIME
                AND t.CURE_CREATE_DATE <= :ENDTIME
                    */
                #endregion
            }
        }

        public string GetBaseHemoInfo
        {
            get
            {
                return @"SELECT utl_raw.cast_to_varchar2('C4DAF0FC') as name, count(*) as count
                                  FROM MED_VASCULAR_ACCESS
                                 WHERE CREATE_DATE >= :BEGINTIME
                                   AND CREATE_DATE <= :ENDTIME
                                   AND VASCULAR_ACCESS_TYPE IN
                                       (SELECT l.item_id
                                          FROM MED_COMMON_ITEMLIST L
                                         WHERE L.Item_Type = '血管通路'
                                           AND L.ITEM_NAME LIKE '%内瘘%'
                                           AND L.STATUS = '1')

                                UNION
                                SELECT utl_raw.cast_to_varchar2('C1D9CAB1B9DC') as name, count(*) as count
                                  FROM MED_VASCULAR_ACCESS
                                 WHERE CREATE_DATE >= :BEGINTIME
                                   AND CREATE_DATE <= :ENDTIME
                                   AND ACCESS_CLASS IN (SELECT l.item_id
                                                          FROM MED_COMMON_ITEMLIST L
                                                         WHERE L.Item_Type = '通路分类'
                                                           AND L.ITEM_NAME LIKE '%临时性通路%'
                                                           AND L.STATUS = '1')
                                UNION
                                SELECT utl_raw.cast_to_varchar2('B0EBD3C0BEC3B5BCB9DC') as name,
                                       count(*) count
                                  FROM MED_VASCULAR_ACCESS
                                 WHERE CREATE_DATE >= :BEGINTIME
                                   AND CREATE_DATE <= :ENDTIME
                                   AND ACCESS_CLASS IN (SELECT l.item_id
                                                          FROM MED_COMMON_ITEMLIST L
                                                         WHERE L.Item_Type = '通路分类'
                                                           AND L.ITEM_NAME LIKE '%永久性通路%'
                                                           AND L.STATUS = '1')";
            }
        }

        public string GetSexScale
        {
            get
            {
                return @"  SELECT T.SEX NAME, COUNT(*) COUNT
                           FROM MED_PATIENTS T
                           WHERE T.CREATE_DATE >= :BEGINTIME
                             AND T.CREATE_DATE <= :ENDTIME
                           GROUP BY T.SEX
                          HAVING(COUNT(*)) > 0";

            }
        }
        public string GetAllHemoCount
        {
            get
            {
                return @"SELECT COUNT(*)
                        FROM MED_CURE_MAIN T
                        WHERE T.CURE_CREATE_DATE >=:BEGINTIME
                          AND T.CURE_CREATE_DATE <=:ENDTIME
                          AND T.CURE_STATUS != '4'";
            }
        }

        public string GetAgeScale
        {
            get
            {
                return @"SELECT '20岁以下' NAME, COUNT(*) COUNT
                          FROM MED_PATIENTS T
                         WHERE T.CREATE_DATE >= :BEGINTIME
                           AND T.CREATE_DATE <= :ENDTIME
                           AND T.AGE <= 20
                        UNION
                        SELECT '20-40岁' NAME, COUNT(*) COUNT
                          FROM MED_PATIENTS T
                         WHERE T.CREATE_DATE >= :BEGINTIME
                           AND T.CREATE_DATE <= :ENDTIME
                           AND T.AGE >= 20
                           AND T.AGE <= 40
                        UNION
                        SELECT '40-60岁' NAME, COUNT(*) COUNT
                          FROM MED_PATIENTS T
                         WHERE T.CREATE_DATE >= :BEGINTIME
                           AND T.CREATE_DATE <= :ENDTIME
                           AND T.AGE >= 40
                           AND T.AGE <= 60
                        UNION
                        SELECT '60岁以上' NAME, COUNT(*) COUNT
                          FROM MED_PATIENTS T
                         WHERE T.CREATE_DATE >= :BEGINTIME
                           AND T.CREATE_DATE <= :ENDTIME
                           AND T.AGE >= 60";

            }
        }

        public string GetInfectiousScale
        {
            get
            {
                return @"SELECT '无传染病' NAME, COUNT(1) COUNT
                            FROM MED_PATIENTS T
                            WHERE T.INFECTIOUS_CHECK_RESULT IS NULL
                              AND T.CREATE_DATE >=:BEGINTIME
                              AND T.CREATE_DATE <= :ENDTIME
                        UNION
                        SELECT '乙型肝炎' NAME, COUNT(1) COUNT
                            FROM MED_PATIENTS T
                            WHERE T.INFECTIOUS_CHECK_RESULT LIKE '%乙%'
                              AND T.CREATE_DATE >=:BEGINTIME
                              AND T.CREATE_DATE <= :ENDTIME
                        UNION
                        SELECT '丙型肝炎' NAME, COUNT(1) COUNT
                            FROM MED_PATIENTS T
                            WHERE T.INFECTIOUS_CHECK_RESULT LIKE '%丙%'
                              AND T.CREATE_DATE >=:BEGINTIME
                              AND T.CREATE_DATE <= :ENDTIME
                        UNION
                        SELECT '艾滋病' NAME, COUNT(1) COUNT
                            FROM MED_PATIENTS T
                            WHERE T.INFECTIOUS_CHECK_RESULT LIKE '%艾滋%'
                              AND T.CREATE_DATE >=:BEGINTIME
                              AND T.CREATE_DATE <= :ENDTIME
                        UNION
                        SELECT '梅毒' NAME, COUNT(1) COUNT
                            FROM MED_PATIENTS T
                            WHERE T.INFECTIOUS_CHECK_RESULT LIKE '%梅毒%'
                              AND T.CREATE_DATE >=:BEGINTIME
                              AND T.CREATE_DATE <= :ENDTIME
                        UNION
                        SELECT '全阴' NAME, COUNT(1) COUNT
                            FROM MED_PATIENTS T
                            WHERE T.INFECTIOUS_CHECK_RESULT LIKE '%全阴%'
                              AND T.CREATE_DATE >=:BEGINTIME
                              AND T.CREATE_DATE <= :ENDTIME";
            }
        }

        public string GetHemoCoutScale
        {
            get
            {
                return @"SELECT '上周 3 次' name, count(1) count
                          from (SELECT t.hemodialysis_id,
                                       TO_CHAR(CURE_CREATE_DATE, 'IW') CUREMONTH,
                                       COUNT(1) AS COUNT_1
                                  FROM MED_CURE_MAIN T
                                 WHERE T.CURE_CREATE_DATE >= :BEGINTIME
                                   AND T.CURE_CREATE_DATE <= :ENDTIME
                                     AND T.CURE_STATUS != '4'
                                 GROUP BY TO_CHAR(CURE_CREATE_DATE, 'IW'), t.hemodialysis_id
                                HAVING(COUNT(1) = 3))
                        union
                        SELECT '上周 2 次' name, count(1) count
                          from (SELECT t.hemodialysis_id,
                                       TO_CHAR(CURE_CREATE_DATE, 'IW') CUREMONTH,
                                       COUNT(1) AS COUNT_1
                                  FROM MED_CURE_MAIN T
                                 WHERE T.CURE_CREATE_DATE >= :BEGINTIME
                                   AND T.CURE_CREATE_DATE <= :ENDTIME
                                     AND T.CURE_STATUS != '4'
                                 GROUP BY TO_CHAR(CURE_CREATE_DATE, 'IW'), t.hemodialysis_id
                                HAVING(COUNT(1) = 2))
                        union
                        SELECT '上周 1 次' name, count(1) count
                          from (SELECT t.hemodialysis_id,
                                       TO_CHAR(CURE_CREATE_DATE, 'IW') CUREMONTH,
                                       COUNT(1) AS COUNT_1
                                  FROM MED_CURE_MAIN T
                                 WHERE T.CURE_CREATE_DATE >= :BEGINTIME
                                   AND T.CURE_CREATE_DATE <= :ENDTIME
                                         AND T.CURE_STATUS != '4'
                                 GROUP BY TO_CHAR(CURE_CREATE_DATE, 'IW'), t.hemodialysis_id
                                HAVING(COUNT(1) = 1))";
            }
        }

        /// <summary>
        /// 获取感染检查列表
        /// </summary>
        //        public string GetInfectionCheckList
        //        {
        //            get
        //            {
        //                return @"SELECT M.DATE_MONTH,DECODE(R1.NEGATIVE,NULL,0,R1.NEGATIVE) NEGATIVE,DECODE(R2.HBSAG_POSITIVE,NULL,0,R2.HBSAG_POSITIVE) HBSAG_POSITIVE
        //                        ,DECODE(R3.HBEAG_POSITIVE,NULL,0,R3.HBEAG_POSITIVE) HBEAG_POSITIVE,DECODE(R4.ANTI_HCV_POSITIVE,NULL,0,R4.ANTI_HCV_POSITIVE) ANTI_HCV_POSITIVE
        //                        ,DECODE(R5.ANTI_TP_POSITIVE,NULL,0,R5.ANTI_TP_POSITIVE) ANTI_TP_POSITIVE,DECODE(R6.HIV_POSITIVE,NULL,0,R6.HIV_POSITIVE) HIV_POSITIVE
        //                        ,DECODE(R7.POSITIVE,NULL,0,R7.POSITIVE) POSITIVE FROM
        //                        (SELECT TO_CHAR(RESULTS_RPT_DATE_TIME,'YYYY-MM') DATE_MONTH FROM MED_LAB_TEST_MASTER
        //                        WHERE TO_CHAR(RESULTS_RPT_DATE_TIME,'YYYY')=:YEAR
        //                        GROUP BY TO_CHAR(RESULTS_RPT_DATE_TIME,'YYYY-MM') ORDER BY TO_CHAR(RESULTS_RPT_DATE_TIME,'YYYY-MM')) M
        //                        LEFT JOIN
        //                        (SELECT TO_CHAR(M.RESULTS_RPT_DATE_TIME,'YYYY-MM') DATE_MONTH,COUNT(R.RESULT) NEGATIVE FROM MED_LAB_TEST_MASTER M LEFT JOIN V_LAB_RESULT R
        //                        ON M.TEST_NO=R.TEST_NO WHERE R.ABNORMAL_INDICATOR='N' OR R.RESULT LIKE '%阴性%'
        //                        GROUP BY TO_CHAR(M.RESULTS_RPT_DATE_TIME,'YYYY-MM') ORDER BY TO_CHAR(M.RESULTS_RPT_DATE_TIME,'YYYY-MM')) R1 ON M.DATE_MONTH=R1.DATE_MONTH
        //                        LEFT JOIN
        //                        (SELECT TO_CHAR(M.RESULTS_RPT_DATE_TIME,'YYYY-MM') DATE_MONTH,COUNT(R.RESULT) HBSAG_POSITIVE FROM MED_LAB_TEST_MASTER M LEFT JOIN V_LAB_RESULT R
        //                        ON M.TEST_NO=R.TEST_NO WHERE R.REPORT_ITEM_NAME='乙肝表面抗原' AND (R.ABNORMAL_INDICATOR='H' OR R.RESULT LIKE '%阳性%')
        //                        GROUP BY TO_CHAR(M.RESULTS_RPT_DATE_TIME,'YYYY-MM')) R2 ON M.DATE_MONTH=R2.DATE_MONTH
        //                        LEFT JOIN
        //                        (SELECT TO_CHAR(M.RESULTS_RPT_DATE_TIME,'YYYY-MM') DATE_MONTH,COUNT(R.RESULT) HBEAG_POSITIVE FROM MED_LAB_TEST_MASTER M LEFT JOIN V_LAB_RESULT R
        //                        ON M.TEST_NO=R.TEST_NO WHERE R.REPORT_ITEM_NAME='乙肝E抗原' AND (R.ABNORMAL_INDICATOR='H' OR R.RESULT LIKE '%阳性%')
        //                        GROUP BY TO_CHAR(M.RESULTS_RPT_DATE_TIME,'YYYY-MM')) R3 ON M.DATE_MONTH=R3.DATE_MONTH
        //                        LEFT JOIN
        //                        (SELECT TO_CHAR(M.RESULTS_RPT_DATE_TIME,'YYYY-MM') DATE_MONTH,COUNT(R.RESULT) ANTI_HCV_POSITIVE FROM MED_LAB_TEST_MASTER M LEFT JOIN V_LAB_RESULT R
        //                        ON M.TEST_NO=R.TEST_NO WHERE R.REPORT_ITEM_NAME='丙肝病毒抗体' AND (R.ABNORMAL_INDICATOR='H' OR R.RESULT LIKE '%阳性%')
        //                        GROUP BY TO_CHAR(M.RESULTS_RPT_DATE_TIME,'YYYY-MM')) R4 ON M.DATE_MONTH=R4.DATE_MONTH
        //                        LEFT JOIN
        //                        (SELECT TO_CHAR(M.RESULTS_RPT_DATE_TIME,'YYYY-MM') DATE_MONTH,COUNT(R.RESULT) ANTI_TP_POSITIVE FROM MED_LAB_TEST_MASTER M LEFT JOIN V_LAB_RESULT R
        //                        ON M.TEST_NO=R.TEST_NO WHERE R.REPORT_ITEM_NAME='梅毒抗体' AND (R.ABNORMAL_INDICATOR='H' OR R.RESULT LIKE '%阳性%')
        //                        GROUP BY TO_CHAR(M.RESULTS_RPT_DATE_TIME,'YYYY-MM')) R5 ON M.DATE_MONTH=R5.DATE_MONTH
        //                        LEFT JOIN
        //                        (SELECT TO_CHAR(M.RESULTS_RPT_DATE_TIME,'YYYY-MM') DATE_MONTH,COUNT(R.RESULT) HIV_POSITIVE FROM MED_LAB_TEST_MASTER M LEFT JOIN V_LAB_RESULT R
        //                        ON M.TEST_NO=R.TEST_NO WHERE R.REPORT_ITEM_NAME='艾滋病病毒抗体' AND (R.ABNORMAL_INDICATOR='H' OR R.RESULT LIKE '%阳性%')
        //                        GROUP BY TO_CHAR(M.RESULTS_RPT_DATE_TIME,'YYYY-MM')) R6 ON M.DATE_MONTH=R6.DATE_MONTH
        //                        LEFT JOIN
        //                        (SELECT TO_CHAR(M.RESULTS_RPT_DATE_TIME,'YYYY-MM') DATE_MONTH,COUNT(R.RESULT) POSITIVE FROM MED_LAB_TEST_MASTER M LEFT JOIN V_LAB_RESULT R
        //                        ON M.TEST_NO=R.TEST_NO WHERE R.ABNORMAL_INDICATOR='H' OR R.RESULT LIKE '%阳性%'
        //                        GROUP BY TO_CHAR(M.RESULTS_RPT_DATE_TIME,'YYYY-MM')) R7 ON M.DATE_MONTH=R7.DATE_MONTH
        //                        ORDER BY M.DATE_MONTH";
        //            }
        //        }

        /// <summary>
        /// 获取血透治疗统计列表
        /// </summary>
        public string GetHemoCureCountList
        {
            get
            {
                return @"SELECT C.DATE_MONTH,DECODE(C1.HD_COUNT,NULL,0,C1.HD_COUNT) HD_COUNT,DECODE(C2.HDF_COUNT,NULL,0,C2.HDF_COUNT) HDF_COUNT,
                        DECODE(C3.HF_COUNT,NULL,0,C3.HF_COUNT) HF_COUNT,DECODE(C4.HP_COUNT,NULL,0,C4.HP_COUNT) HP_COUNT,
                        DECODE(C5.HD_HP_COUNT,NULL,0,C5.HD_HP_COUNT) HD_HP_COUNT FROM
                        (SELECT TO_CHAR(CURE_CREATE_DATE,'YYYY-MM') DATE_MONTH FROM MED_CURE_MAIN
                        WHERE TO_CHAR(CURE_CREATE_DATE,'YYYY')=:YEAR  AND CURE_STATUS != '4'
                        GROUP BY TO_CHAR(CURE_CREATE_DATE,'YYYY-MM') ORDER BY TO_CHAR(CURE_CREATE_DATE,'YYYY-MM')) C
                        LEFT JOIN
                        (SELECT TO_CHAR(C.CURE_CREATE_DATE, 'YYYY-MM') DATE_MONTH, COUNT(C.CURE_ID) HD_COUNT FROM MED_CURE_MAIN C
                        LEFT JOIN MED_COMMON_ITEMLIST L ON C.PURIFICATION_MODE=L.ITEM_ID
                        WHERE L.ITEM_NAME='HD' AND  C.CURE_STATUS != '4' GROUP BY TO_CHAR(C.CURE_CREATE_DATE, 'YYYY-MM') ORDER BY TO_CHAR(C.CURE_CREATE_DATE, 'YYYY-MM')) C1 ON C.DATE_MONTH=C1.DATE_MONTH
                        LEFT JOIN
                        (SELECT TO_CHAR(C.CURE_CREATE_DATE, 'YYYY-MM') DATE_MONTH, COUNT(C.CURE_ID) HDF_COUNT FROM MED_CURE_MAIN C
                        LEFT JOIN MED_COMMON_ITEMLIST L ON C.PURIFICATION_MODE=L.ITEM_ID
                        WHERE L.ITEM_NAME='HDF' AND  C.CURE_STATUS != '4' GROUP BY TO_CHAR(C.CURE_CREATE_DATE, 'YYYY-MM') ORDER BY TO_CHAR(C.CURE_CREATE_DATE, 'YYYY-MM')) C2 ON C.DATE_MONTH=C2.DATE_MONTH
                        LEFT JOIN
                        (SELECT TO_CHAR(C.CURE_CREATE_DATE, 'YYYY-MM') DATE_MONTH, COUNT(C.CURE_ID) HF_COUNT FROM MED_CURE_MAIN C
                        LEFT JOIN MED_COMMON_ITEMLIST L ON C.PURIFICATION_MODE=L.ITEM_ID
                        WHERE L.ITEM_NAME='HF' AND C.CURE_STATUS != '4'  GROUP BY TO_CHAR(C.CURE_CREATE_DATE, 'YYYY-MM') ORDER BY TO_CHAR(C.CURE_CREATE_DATE, 'YYYY-MM')) C3 ON C.DATE_MONTH=C3.DATE_MONTH
                        LEFT JOIN
                        (SELECT TO_CHAR(C.CURE_CREATE_DATE, 'YYYY-MM') DATE_MONTH, COUNT(C.CURE_ID) HP_COUNT FROM MED_CURE_MAIN C
                        LEFT JOIN MED_COMMON_ITEMLIST L ON C.PURIFICATION_MODE=L.ITEM_ID
                        WHERE L.ITEM_NAME='HP' AND C.CURE_STATUS != '4'  GROUP BY TO_CHAR(C.CURE_CREATE_DATE, 'YYYY-MM') ORDER BY TO_CHAR(C.CURE_CREATE_DATE, 'YYYY-MM')) C4 ON C.DATE_MONTH=C4.DATE_MONTH
                        LEFT JOIN
                        (SELECT TO_CHAR(C.CURE_CREATE_DATE, 'YYYY-MM') DATE_MONTH, COUNT(C.CURE_ID) HD_HP_COUNT FROM MED_CURE_MAIN C
                        LEFT JOIN MED_COMMON_ITEMLIST L ON C.PURIFICATION_MODE=L.ITEM_ID
                        WHERE L.ITEM_NAME='HD+HP' AND C.CURE_STATUS != '4'  GROUP BY TO_CHAR(C.CURE_CREATE_DATE, 'YYYY-MM') ORDER BY TO_CHAR(C.CURE_CREATE_DATE, 'YYYY-MM')) C5 ON C.DATE_MONTH=C5.DATE_MONTH
                        ORDER BY C.DATE_MONTH";
            }
        }

        /// <summary>
        /// 获取肾性贫血纠正例数统计列表
        /// </summary>
        public string GetRenalAnemiaCountList
        {
            get
            {
                return @"SELECT M.DATE_MONTH,DECODE(R.RENALANEMIA_COUNT,NULL,0,R.RENALANEMIA_COUNT) RENALANEMIA_COUNT FROM
                        (SELECT TO_CHAR(RESULTS_RPT_DATE_TIME,'YYYY-MM') DATE_MONTH FROM MED_LAB_TEST_MASTER
                        WHERE TO_CHAR(RESULTS_RPT_DATE_TIME,'YYYY')=:YEAR
                        GROUP BY TO_CHAR(RESULTS_RPT_DATE_TIME,'YYYY-MM') ORDER BY TO_CHAR(RESULTS_RPT_DATE_TIME,'YYYY-MM')) M
                        LEFT JOIN
                        (SELECT TO_CHAR(M.RESULTS_RPT_DATE_TIME,'YYYY-MM') DATE_MONTH,COUNT(R.RESULT) RENALANEMIA_COUNT FROM MED_LAB_TEST_MASTER M LEFT JOIN V_LAB_RESULT R
                        ON M.TEST_NO=R.TEST_NO WHERE R.REPORT_ITEM_NAME='血红蛋白' AND TO_NUMBER(R.RESULT)>=110
                        GROUP BY TO_CHAR(M.RESULTS_RPT_DATE_TIME,'YYYY-MM')) R ON M.DATE_MONTH=R.DATE_MONTH
                        ORDER BY M.DATE_MONTH";
            }
        }

        /// <summary>
        /// 按年份获取感染检查列表
        /// </summary>
        public string GetInfectionCheckListByYear
        {
            get
            {
                return @"SELECT D.DATE_MONTH,DECODE(D1.NEGATIVE,NULL,0,D1.NEGATIVE) NEGATIVE,DECODE(D1.NEGATIVE_ADD,NULL,0,D1.NEGATIVE_ADD) NEGATIVE_ADD,
                        DECODE(D2.HBSAG_POSITIVE,NULL,0,D2.HBSAG_POSITIVE) HBSAG_POSITIVE,DECODE(D2.HBSAG_POSITIVE_ADD,NULL,0,D2.HBSAG_POSITIVE_ADD) HBSAG_POSITIVE_ADD,
                        DECODE(D2.HBSAG_POSITIVE,NULL,0,D2.HBSAG_POSITIVE)||'(新增'||DECODE(D2.HBSAG_POSITIVE_ADD,NULL,0,D2.HBSAG_POSITIVE_ADD)||'人)' HBSAG_POSITIVE_DESC,
                        DECODE(D3.HBEAG_POSITIVE,NULL,0,D3.HBEAG_POSITIVE) HBEAG_POSITIVE,DECODE(D3.HBEAG_POSITIVE_ADD,NULL,0,D3.HBEAG_POSITIVE_ADD) HBEAG_POSITIVE_ADD,
                        DECODE(D4.ANTI_HCV_POSITIVE,NULL,0,D4.ANTI_HCV_POSITIVE) ANTI_HCV_POSITIVE,DECODE(D4.ANTI_HCV_POSITIVE_ADD,NULL,0,D4.ANTI_HCV_POSITIVE_ADD) ANTI_HCV_POSITIVE_ADD,
                        DECODE(D5.ANTI_TP_POSITIVE,NULL,0,D5.ANTI_TP_POSITIVE) ANTI_TP_POSITIVE,DECODE(D5.ANTI_TP_POSITIVE_ADD,NULL,0,D5.ANTI_TP_POSITIVE_ADD) ANTI_TP_POSITIVE_ADD,
                        DECODE(D6.HIV_POSITIVE,NULL,0,D6.HIV_POSITIVE) HIV_POSITIVE,DECODE(D6.HIV_POSITIVE_ADD,NULL,0,D6.HIV_POSITIVE_ADD) HIV_POSITIVE_ADD,
                        DECODE(D7.POSITIVE,NULL,0,D7.POSITIVE) POSITIVE,DECODE(D7.POSITIVE_ADD,NULL,0,D7.POSITIVE_ADD) POSITIVE_ADD FROM
                        (SELECT TO_CHAR(REPORT_DATE,'YYYY-MM') DATE_MONTH FROM MED_REPORT_DATA
                        WHERE TO_CHAR(REPORT_DATE,'YYYY')=:YEAR GROUP BY TO_CHAR(REPORT_DATE,'YYYY-MM') ORDER BY TO_CHAR(REPORT_DATE,'YYYY-MM')) D
                        LEFT JOIN
                        (SELECT TO_CHAR(REPORT_DATE,'YYYY-MM') DATE_MONTH,SUM(REPORT_CURRENT_COUNT) NEGATIVE,SUM(REPORT_ADD_COUNT) NEGATIVE_ADD FROM MED_REPORT_DATA
                        WHERE REPORT_TYPE='001' GROUP BY TO_CHAR(REPORT_DATE,'YYYY-MM') ORDER BY TO_CHAR(REPORT_DATE,'YYYY-MM')) D1 ON D.DATE_MONTH=D1.DATE_MONTH
                        LEFT JOIN
                        (SELECT TO_CHAR(REPORT_DATE,'YYYY-MM') DATE_MONTH,SUM(REPORT_CURRENT_COUNT) HBSAG_POSITIVE,SUM(REPORT_ADD_COUNT) HBSAG_POSITIVE_ADD FROM MED_REPORT_DATA
                        WHERE REPORT_TYPE='002' GROUP BY TO_CHAR(REPORT_DATE,'YYYY-MM') ORDER BY TO_CHAR(REPORT_DATE,'YYYY-MM')) D2 ON D.DATE_MONTH=D2.DATE_MONTH
                        LEFT JOIN
                        (SELECT TO_CHAR(REPORT_DATE,'YYYY-MM') DATE_MONTH,SUM(REPORT_CURRENT_COUNT) HBEAG_POSITIVE,SUM(REPORT_ADD_COUNT) HBEAG_POSITIVE_ADD FROM MED_REPORT_DATA
                        WHERE REPORT_TYPE='003' GROUP BY TO_CHAR(REPORT_DATE,'YYYY-MM') ORDER BY TO_CHAR(REPORT_DATE,'YYYY-MM')) D3 ON D.DATE_MONTH=D3.DATE_MONTH
                        LEFT JOIN
                        (SELECT TO_CHAR(REPORT_DATE,'YYYY-MM') DATE_MONTH,SUM(REPORT_CURRENT_COUNT) ANTI_HCV_POSITIVE,SUM(REPORT_ADD_COUNT) ANTI_HCV_POSITIVE_ADD FROM MED_REPORT_DATA
                        WHERE REPORT_TYPE='004' GROUP BY TO_CHAR(REPORT_DATE,'YYYY-MM') ORDER BY TO_CHAR(REPORT_DATE,'YYYY-MM')) D4 ON D.DATE_MONTH=D4.DATE_MONTH
                        LEFT JOIN
                        (SELECT TO_CHAR(REPORT_DATE,'YYYY-MM') DATE_MONTH,SUM(REPORT_CURRENT_COUNT) ANTI_TP_POSITIVE,SUM(REPORT_ADD_COUNT) ANTI_TP_POSITIVE_ADD FROM MED_REPORT_DATA
                        WHERE REPORT_TYPE='006' GROUP BY TO_CHAR(REPORT_DATE,'YYYY-MM') ORDER BY TO_CHAR(REPORT_DATE,'YYYY-MM')) D5 ON D.DATE_MONTH=D5.DATE_MONTH
                        LEFT JOIN
                        (SELECT TO_CHAR(REPORT_DATE,'YYYY-MM') DATE_MONTH,SUM(REPORT_CURRENT_COUNT) HIV_POSITIVE,SUM(REPORT_ADD_COUNT) HIV_POSITIVE_ADD FROM MED_REPORT_DATA
                        WHERE REPORT_TYPE='005' GROUP BY TO_CHAR(REPORT_DATE,'YYYY-MM') ORDER BY TO_CHAR(REPORT_DATE,'YYYY-MM')) D6 ON D.DATE_MONTH=D6.DATE_MONTH
                        LEFT JOIN
                        (SELECT TO_CHAR(REPORT_DATE,'YYYY-MM') DATE_MONTH,SUM(REPORT_CURRENT_COUNT) POSITIVE,SUM(REPORT_ADD_COUNT) POSITIVE_ADD FROM MED_REPORT_DATA
                        WHERE REPORT_TYPE='007' GROUP BY TO_CHAR(REPORT_DATE,'YYYY-MM') ORDER BY TO_CHAR(REPORT_DATE,'YYYY-MM')) D7 ON D.DATE_MONTH=D7.DATE_MONTH
                        ORDER BY D.DATE_MONTH";
            }
        }

        /// <summary>
        /// 按日期获取感染检查列表
        /// </summary>
        public string GetInfectionCheckListByDate
        {
            get
            {
                return @"SELECT (TO_CHAR(:BEGINDATE,'YYYY-MM-DD')||'~'||TO_CHAR(:ENDDATE,'YYYY-MM-DD')) DATE_MONTH,DECODE(D.NEGATIVE,NULL,0,D.NEGATIVE) NEGATIVE,
                        DECODE(D.NEGATIVE_ADD,NULL,0,D.NEGATIVE_ADD) NEGATIVE_ADD,DECODE(D.HBSAG_POSITIVE,NULL,0,D.HBSAG_POSITIVE) HBSAG_POSITIVE,
                        DECODE(D.HBSAG_POSITIVE_ADD,NULL,0,D.HBSAG_POSITIVE_ADD) HBSAG_POSITIVE_ADD,DECODE(D.HBSAG_POSITIVE_DESC,NULL,0||'(新增'||0||'人)',D.HBSAG_POSITIVE_DESC) HBSAG_POSITIVE_DESC,
                        DECODE(D.HBEAG_POSITIVE,NULL,0,D.HBEAG_POSITIVE) HBEAG_POSITIVE,DECODE(D.HBEAG_POSITIVE_ADD,NULL,0,D.HBEAG_POSITIVE_ADD) HBEAG_POSITIVE_ADD,
                        DECODE(D.ANTI_HCV_POSITIVE,NULL,0,D.ANTI_HCV_POSITIVE) ANTI_HCV_POSITIVE,DECODE(D.ANTI_HCV_POSITIVE_ADD,NULL,0,D.ANTI_HCV_POSITIVE_ADD) ANTI_HCV_POSITIVE_ADD,
                        DECODE(D.ANTI_TP_POSITIVE,NULL,0,D.ANTI_TP_POSITIVE) ANTI_TP_POSITIVE,DECODE(D.ANTI_TP_POSITIVE_ADD,NULL,0,D.ANTI_TP_POSITIVE_ADD) ANTI_TP_POSITIVE_ADD,
                        DECODE(D.HIV_POSITIVE,NULL,0,D.HIV_POSITIVE) HIV_POSITIVE,DECODE(D.HIV_POSITIVE_ADD,NULL,0,D.HIV_POSITIVE_ADD) HIV_POSITIVE_ADD,
                        DECODE(D.POSITIVE,NULL,0,D.POSITIVE) POSITIVE,DECODE(D.POSITIVE_ADD,NULL,0,D.POSITIVE_ADD) POSITIVE_ADD FROM (SELECT * FROM
                        (SELECT SUM(REPORT_CURRENT_COUNT) NEGATIVE,SUM(REPORT_ADD_COUNT) NEGATIVE_ADD FROM MED_REPORT_DATA
                        WHERE REPORT_TYPE='001' AND TRUNC(REPORT_DATE)>=:BEGINDATE AND TRUNC(REPORT_DATE)<=:ENDDATE),
                        (SELECT SUM(REPORT_CURRENT_COUNT) HBSAG_POSITIVE,SUM(REPORT_ADD_COUNT) HBSAG_POSITIVE_ADD,SUM(REPORT_CURRENT_COUNT)||'(新增'||SUM(REPORT_ADD_COUNT)||'人)' HBSAG_POSITIVE_DESC FROM MED_REPORT_DATA
                        WHERE REPORT_TYPE='002' AND TRUNC(REPORT_DATE)>=:BEGINDATE AND TRUNC(REPORT_DATE)<=:ENDDATE),
                        (SELECT SUM(REPORT_CURRENT_COUNT) HBEAG_POSITIVE,SUM(REPORT_ADD_COUNT) HBEAG_POSITIVE_ADD FROM MED_REPORT_DATA
                        WHERE REPORT_TYPE='003' AND TRUNC(REPORT_DATE)>=:BEGINDATE AND TRUNC(REPORT_DATE)<=:ENDDATE),
                        (SELECT SUM(REPORT_CURRENT_COUNT) ANTI_HCV_POSITIVE,SUM(REPORT_ADD_COUNT) ANTI_HCV_POSITIVE_ADD FROM MED_REPORT_DATA
                        WHERE REPORT_TYPE='004' AND TRUNC(REPORT_DATE)>=:BEGINDATE AND TRUNC(REPORT_DATE)<=:ENDDATE),
                        (SELECT SUM(REPORT_CURRENT_COUNT) ANTI_TP_POSITIVE,SUM(REPORT_ADD_COUNT) ANTI_TP_POSITIVE_ADD FROM MED_REPORT_DATA
                        WHERE REPORT_TYPE='006' AND TRUNC(REPORT_DATE)>=:BEGINDATE AND TRUNC(REPORT_DATE)<=:ENDDATE),
                        (SELECT SUM(REPORT_CURRENT_COUNT) HIV_POSITIVE,SUM(REPORT_ADD_COUNT) HIV_POSITIVE_ADD FROM MED_REPORT_DATA
                        WHERE REPORT_TYPE='005' AND TRUNC(REPORT_DATE)>=:BEGINDATE AND TRUNC(REPORT_DATE)<=:ENDDATE),
                        (SELECT SUM(REPORT_CURRENT_COUNT) POSITIVE,SUM(REPORT_ADD_COUNT) POSITIVE_ADD FROM MED_REPORT_DATA
                        WHERE REPORT_TYPE='007' AND TRUNC(REPORT_DATE)>=:BEGINDATE AND TRUNC(REPORT_DATE)<=:ENDDATE)) D";
            }
        }

        /// <summary>
        /// 按年份获取质量管理基础数据列表
        /// </summary>
        public string GetQualityControlBaseDataByYear
        {
            get
            {
                return @"SELECT D.DATE_MONTH,DECODE(D1.HEMO_COUNT,NULL,0,D1.HEMO_COUNT) HEMO_COUNT,DECODE(D2.HD_COUNT,NULL,0,D2.HD_COUNT) HD_COUNT,
                        DECODE(D3.HDF_COUNT,NULL,0,D3.HDF_COUNT) HDF_COUNT,DECODE(D4.HF_COUNT,NULL,0,D4.HF_COUNT) HF_COUNT,
                        DECODE(D5.HP_COUNT,NULL,0,D5.HP_COUNT) HP_COUNT,DECODE(D6.HD_HP_COUNT,NULL,0,D6.HD_HP_COUNT) HD_HP_COUNT,
                        DECODE(D7.DEATH_COUNT,NULL,0,D7.DEATH_COUNT) DEATH_COUNT,DECODE(D8.DEATH_RATE,NULL,0,D8.DEATH_RATE) DEATH_RATE,
                        DECODE(D9.SEVERE_COMPLICATION,NULL,0,D9.SEVERE_COMPLICATION) SEVERE_COMPLICATION,DECODE(D10.HBSAG_POSITIVE,NULL,0,D10.HBSAG_POSITIVE) HBSAG_POSITIVE,
                        DECODE(D11.HBEAG_POSITIVE,NULL,0,D11.HBEAG_POSITIVE) HBEAG_POSITIVE,DECODE(D12.ANTI_HCV_POSITIVE,NULL,0,D12.ANTI_HCV_POSITIVE) ANTI_HCV_POSITIVE,
                        DECODE(D13.PERITONEAL_DIALYSIS,NULL,0,D13.PERITONEAL_DIALYSIS) PERITONEAL_DIALYSIS,DECODE(D14.RENAL_TRANSPLANT,NULL,0,D14.RENAL_TRANSPLANT) RENAL_TRANSPLANT FROM
                        (SELECT TO_CHAR(REPORT_DATE,'YYYY-MM') DATE_MONTH FROM MED_REPORT_DATA
                        WHERE TO_CHAR(REPORT_DATE,'YYYY')=:YEAR GROUP BY TO_CHAR(REPORT_DATE,'YYYY-MM') ORDER BY TO_CHAR(REPORT_DATE,'YYYY-MM')) D
                        LEFT JOIN
                        (SELECT TO_CHAR(REPORT_DATE,'YYYY-MM') DATE_MONTH,SUM(REPORT_CURRENT_COUNT) HEMO_COUNT FROM MED_REPORT_DATA
                        WHERE REPORT_TYPE='011' GROUP BY TO_CHAR(REPORT_DATE,'YYYY-MM') ORDER BY TO_CHAR(REPORT_DATE,'YYYY-MM')) D1 ON D.DATE_MONTH=D1.DATE_MONTH
                        LEFT JOIN
                        (SELECT TO_CHAR(REPORT_DATE,'YYYY-MM') DATE_MONTH,SUM(REPORT_CURRENT_COUNT) HD_COUNT FROM MED_REPORT_DATA
                        WHERE REPORT_TYPE='HD' GROUP BY TO_CHAR(REPORT_DATE,'YYYY-MM') ORDER BY TO_CHAR(REPORT_DATE,'YYYY-MM')) D2 ON D.DATE_MONTH=D2.DATE_MONTH
                        LEFT JOIN
                        (SELECT TO_CHAR(REPORT_DATE,'YYYY-MM') DATE_MONTH,SUM(REPORT_CURRENT_COUNT) HDF_COUNT FROM MED_REPORT_DATA
                        WHERE REPORT_TYPE='HDF' GROUP BY TO_CHAR(REPORT_DATE,'YYYY-MM') ORDER BY TO_CHAR(REPORT_DATE,'YYYY-MM')) D3 ON D.DATE_MONTH=D3.DATE_MONTH
                        LEFT JOIN
                        (SELECT TO_CHAR(REPORT_DATE,'YYYY-MM') DATE_MONTH,SUM(REPORT_CURRENT_COUNT) HF_COUNT FROM MED_REPORT_DATA
                        WHERE REPORT_TYPE='HF' GROUP BY TO_CHAR(REPORT_DATE,'YYYY-MM') ORDER BY TO_CHAR(REPORT_DATE,'YYYY-MM')) D4 ON D.DATE_MONTH=D4.DATE_MONTH
                        LEFT JOIN
                        (SELECT TO_CHAR(REPORT_DATE,'YYYY-MM') DATE_MONTH,SUM(REPORT_CURRENT_COUNT) HP_COUNT FROM MED_REPORT_DATA
                        WHERE REPORT_TYPE='HP' GROUP BY TO_CHAR(REPORT_DATE,'YYYY-MM') ORDER BY TO_CHAR(REPORT_DATE,'YYYY-MM')) D5 ON D.DATE_MONTH=D5.DATE_MONTH
                        LEFT JOIN
                        (SELECT TO_CHAR(REPORT_DATE,'YYYY-MM') DATE_MONTH,SUM(REPORT_CURRENT_COUNT) HD_HP_COUNT FROM MED_REPORT_DATA
                        WHERE REPORT_TYPE='HD+HP' GROUP BY TO_CHAR(REPORT_DATE,'YYYY-MM') ORDER BY TO_CHAR(REPORT_DATE,'YYYY-MM')) D6 ON D.DATE_MONTH=D6.DATE_MONTH
                        LEFT JOIN
                        (SELECT TO_CHAR(REPORT_DATE,'YYYY-MM') DATE_MONTH,SUM(REPORT_CURRENT_COUNT) DEATH_COUNT FROM MED_REPORT_DATA
                        WHERE REPORT_TYPE='049' GROUP BY TO_CHAR(REPORT_DATE,'YYYY-MM') ORDER BY TO_CHAR(REPORT_DATE,'YYYY-MM')) D7 ON D.DATE_MONTH=D7.DATE_MONTH
                        LEFT JOIN
                        (SELECT TO_CHAR(REPORT_DATE,'YYYY-MM') DATE_MONTH,SUM(REPORT_CURRENT_COUNT) DEATH_RATE FROM MED_REPORT_DATA
                        WHERE REPORT_TYPE='050' GROUP BY TO_CHAR(REPORT_DATE,'YYYY-MM') ORDER BY TO_CHAR(REPORT_DATE,'YYYY-MM')) D8 ON D.DATE_MONTH=D8.DATE_MONTH
                        LEFT JOIN
                        (SELECT TO_CHAR(REPORT_DATE,'YYYY-MM') DATE_MONTH,SUM(REPORT_CURRENT_COUNT) SEVERE_COMPLICATION FROM MED_REPORT_DATA
                        WHERE REPORT_TYPE='052' GROUP BY TO_CHAR(REPORT_DATE,'YYYY-MM') ORDER BY TO_CHAR(REPORT_DATE,'YYYY-MM')) D9 ON D.DATE_MONTH=D9.DATE_MONTH
                        LEFT JOIN
                        (SELECT TO_CHAR(REPORT_DATE,'YYYY-MM') DATE_MONTH,SUM(REPORT_CURRENT_COUNT) HBSAG_POSITIVE FROM MED_REPORT_DATA
                        WHERE REPORT_TYPE='055' GROUP BY TO_CHAR(REPORT_DATE,'YYYY-MM') ORDER BY TO_CHAR(REPORT_DATE,'YYYY-MM')) D10 ON D.DATE_MONTH=D10.DATE_MONTH
                        LEFT JOIN
                        (SELECT TO_CHAR(REPORT_DATE,'YYYY-MM') DATE_MONTH,SUM(REPORT_CURRENT_COUNT) HBEAG_POSITIVE FROM MED_REPORT_DATA
                        WHERE REPORT_TYPE='056' GROUP BY TO_CHAR(REPORT_DATE,'YYYY-MM') ORDER BY TO_CHAR(REPORT_DATE,'YYYY-MM')) D11 ON D.DATE_MONTH=D11.DATE_MONTH
                        LEFT JOIN
                        (SELECT TO_CHAR(REPORT_DATE,'YYYY-MM') DATE_MONTH,SUM(REPORT_CURRENT_COUNT) ANTI_HCV_POSITIVE FROM MED_REPORT_DATA
                        WHERE REPORT_TYPE='057' GROUP BY TO_CHAR(REPORT_DATE,'YYYY-MM') ORDER BY TO_CHAR(REPORT_DATE,'YYYY-MM')) D12 ON D.DATE_MONTH=D12.DATE_MONTH
                        LEFT JOIN
                        (SELECT TO_CHAR(REPORT_DATE,'YYYY-MM') DATE_MONTH,SUM(REPORT_CURRENT_COUNT) PERITONEAL_DIALYSIS FROM MED_REPORT_DATA
                        WHERE REPORT_TYPE='053' GROUP BY TO_CHAR(REPORT_DATE,'YYYY-MM') ORDER BY TO_CHAR(REPORT_DATE,'YYYY-MM')) D13 ON D.DATE_MONTH=D13.DATE_MONTH
                        LEFT JOIN
                        (SELECT TO_CHAR(REPORT_DATE,'YYYY-MM') DATE_MONTH,SUM(REPORT_CURRENT_COUNT) RENAL_TRANSPLANT FROM MED_REPORT_DATA
                        WHERE REPORT_TYPE='054' GROUP BY TO_CHAR(REPORT_DATE,'YYYY-MM') ORDER BY TO_CHAR(REPORT_DATE,'YYYY-MM')) D14 ON D.DATE_MONTH=D14.DATE_MONTH
                        ORDER BY D.DATE_MONTH";
            }
        }

        /// <summary>
        /// 按日期获取质量管理基础数据列表
        /// </summary>
        public string GetQualityControlBaseDataByDate
        {
            get
            {
                return @"SELECT (TO_CHAR(:BEGINDATE,'YYYY-MM-DD')||'~'||TO_CHAR(:ENDDATE,'YYYY-MM-DD')) DATE_MONTH,DECODE(D.HEMO_COUNT,NULL,0,D.HEMO_COUNT) HEMO_COUNT,
                        DECODE(D.HD_COUNT,NULL,0,D.HD_COUNT) HD_COUNT,DECODE(D.HDF_COUNT,NULL,0,D.HDF_COUNT) HDF_COUNT,
                        DECODE(D.HF_COUNT,NULL,0,D.HF_COUNT) HF_COUNT,DECODE(D.HP_COUNT,NULL,0,D.HP_COUNT) HP_COUNT,
                        DECODE(D.HD_HP_COUNT,NULL,0,D.HD_HP_COUNT) HD_HP_COUNT,DECODE(D.DEATH_COUNT,NULL,0,D.DEATH_COUNT) DEATH_COUNT,
                        DECODE(D.DEATH_RATE,NULL,0,D.DEATH_RATE) DEATH_RATE,DECODE(D.SEVERE_COMPLICATION,NULL,0,D.SEVERE_COMPLICATION) SEVERE_COMPLICATION,
                        DECODE(D.HBSAG_POSITIVE,NULL,0,D.HBSAG_POSITIVE) HBSAG_POSITIVE,DECODE(D.HBEAG_POSITIVE,NULL,0,D.HBEAG_POSITIVE) HBEAG_POSITIVE,
                        DECODE(D.ANTI_HCV_POSITIVE,NULL,0,D.ANTI_HCV_POSITIVE) ANTI_HCV_POSITIVE,DECODE(D.PERITONEAL_DIALYSIS,NULL,0,D.PERITONEAL_DIALYSIS) PERITONEAL_DIALYSIS,
                        DECODE(D.RENAL_TRANSPLANT,NULL,0,D.RENAL_TRANSPLANT) RENAL_TRANSPLANT FROM (SELECT * FROM
                        (SELECT SUM(REPORT_CURRENT_COUNT) HEMO_COUNT FROM MED_REPORT_DATA
                        WHERE REPORT_TYPE='011' AND TRUNC(REPORT_DATE)>=:BEGINDATE AND TRUNC(REPORT_DATE)<=:ENDDATE),
                        (SELECT SUM(REPORT_CURRENT_COUNT) HD_COUNT FROM MED_REPORT_DATA
                        WHERE REPORT_TYPE='HD' AND TRUNC(REPORT_DATE)>=:BEGINDATE AND TRUNC(REPORT_DATE)<=:ENDDATE),
                        (SELECT SUM(REPORT_CURRENT_COUNT) HDF_COUNT FROM MED_REPORT_DATA
                        WHERE REPORT_TYPE='HDF' AND TRUNC(REPORT_DATE)>=:BEGINDATE AND TRUNC(REPORT_DATE)<=:ENDDATE),
                        (SELECT SUM(REPORT_CURRENT_COUNT) HF_COUNT FROM MED_REPORT_DATA
                        WHERE REPORT_TYPE='HF' AND TRUNC(REPORT_DATE)>=:BEGINDATE AND TRUNC(REPORT_DATE)<=:ENDDATE),
                        (SELECT SUM(REPORT_CURRENT_COUNT) HP_COUNT FROM MED_REPORT_DATA
                        WHERE REPORT_TYPE='HP' AND TRUNC(REPORT_DATE)>=:BEGINDATE AND TRUNC(REPORT_DATE)<=:ENDDATE),
                        (SELECT SUM(REPORT_CURRENT_COUNT) HD_HP_COUNT FROM MED_REPORT_DATA
                        WHERE REPORT_TYPE='HD+HP' AND TRUNC(REPORT_DATE)>=:BEGINDATE AND TRUNC(REPORT_DATE)<=:ENDDATE),
                        (SELECT SUM(REPORT_CURRENT_COUNT) DEATH_COUNT FROM MED_REPORT_DATA
                        WHERE REPORT_TYPE='049' AND TRUNC(REPORT_DATE)>=:BEGINDATE AND TRUNC(REPORT_DATE)<=:ENDDATE),
                        (SELECT SUM(REPORT_CURRENT_COUNT) DEATH_RATE FROM MED_REPORT_DATA
                        WHERE REPORT_TYPE='050' AND TRUNC(REPORT_DATE)>=:BEGINDATE AND TRUNC(REPORT_DATE)<=:ENDDATE),
                        (SELECT SUM(REPORT_CURRENT_COUNT) SEVERE_COMPLICATION FROM MED_REPORT_DATA
                        WHERE REPORT_TYPE='052' AND TRUNC(REPORT_DATE)>=:BEGINDATE AND TRUNC(REPORT_DATE)<=:ENDDATE),
                        (SELECT SUM(REPORT_CURRENT_COUNT) HBSAG_POSITIVE FROM MED_REPORT_DATA
                        WHERE REPORT_TYPE='055' AND TRUNC(REPORT_DATE)>=:BEGINDATE AND TRUNC(REPORT_DATE)<=:ENDDATE),
                        (SELECT SUM(REPORT_CURRENT_COUNT) HBEAG_POSITIVE FROM MED_REPORT_DATA
                        WHERE REPORT_TYPE='056' AND TRUNC(REPORT_DATE)>=:BEGINDATE AND TRUNC(REPORT_DATE)<=:ENDDATE),
                        (SELECT SUM(REPORT_CURRENT_COUNT) ANTI_HCV_POSITIVE FROM MED_REPORT_DATA
                        WHERE REPORT_TYPE='057' AND TRUNC(REPORT_DATE)>=:BEGINDATE AND TRUNC(REPORT_DATE)<=:ENDDATE),
                        (SELECT SUM(REPORT_CURRENT_COUNT) PERITONEAL_DIALYSIS FROM MED_REPORT_DATA
                        WHERE REPORT_TYPE='053' AND TRUNC(REPORT_DATE)>=:BEGINDATE AND TRUNC(REPORT_DATE)<=:ENDDATE),
                        (SELECT SUM(REPORT_CURRENT_COUNT) RENAL_TRANSPLANT FROM MED_REPORT_DATA
                        WHERE REPORT_TYPE='054' AND TRUNC(REPORT_DATE)>=:BEGINDATE AND TRUNC(REPORT_DATE)<=:ENDDATE)) D";
            }
        }

        /// <summary>
        /// 获取血透机、专职人员统计数据
        /// </summary>
        public string GetMachineAndSpecialistCount
        {
            //            get {
            //                return @"SELECT DECODE(D.MACHINECOUNT,NULL,0,D.MACHINECOUNT) MACHINECOUNT,DECODE(D.SPECIALISTSCOUNT,NULL,0,D.SPECIALISTSCOUNT) SPECIALISTSCOUNT,
            //                        DECODE(D.PARAMEDICCOUNT,NULL,0,D.PARAMEDICCOUNT) PARAMEDICCOUNT FROM
            //                        (SELECT * FROM (SELECT SUM(REPORT_CURRENT_COUNT) MACHINECOUNT FROM MED_REPORT_DATA WHERE REPORT_TYPE='008'),
            //                        (SELECT SUM(REPORT_CURRENT_COUNT) SPECIALISTSCOUNT FROM MED_REPORT_DATA WHERE REPORT_TYPE='009'),
            //                        (SELECT SUM(REPORT_CURRENT_COUNT) PARAMEDICCOUNT FROM MED_REPORT_DATA WHERE REPORT_TYPE='010')) D";
            //            }
            get
            {
                return @"SELECT *
                FROM (SELECT '008' JOB, COUNT(*) DCOUNT, '血透机台数' JOBNAME
                        FROM MED_DIALYSIS_MACHINE MC
                        WHERE 1 = 1
                        AND NVL(MC.AREA_ID, 'NO') <> 'NO'
                        AND NVL(MC.BED_ID, 'NO') <> 'NO')
                UNION (SELECT DECODE(JOB,
                                    'f6658d4c-ce41-4961-9f97-05834380831f',
                                    '009',
                                    '458c812d-2762-4c8a-8e37-19382c4146b4',
                                    '010',
                                    '044') JOB,
                            COUNT(*) DCOUNT,
                            DECODE(JOB,
                                    'f6658d4c-ce41-4961-9f97-05834380831f',
                                    '医生人数',
                                    '458c812d-2762-4c8a-8e37-19382c4146b4',
                                    '护士人数',
                                    '技工人数') JOBNAME
                        FROM MED_STAFF_DICT
                    GROUP BY JOB)
                ";
            }
        }

        /// <summary>
        /// 按年份获取患者质量监测指标列表
        /// </summary>
        public string GetQualityMonitorIndicatorByYear
        {
            get
            {
                return @"SELECT D.DATE_MONTH,DECODE(D1.UREA_REMOVE,NULL,0,D1.UREA_REMOVE) UREA_REMOVE,DECODE(D2.RENAL_ANEMIA,NULL,0,D2.RENAL_ANEMIA) RENAL_ANEMIA,
                        DECODE(D3.CA_P_METABOLISM,NULL,0,D3.CA_P_METABOLISM) CA_P_METABOLISM,DECODE(D4.SECONDARY_SHPT,NULL,0,D4.SECONDARY_SHPT) SECONDARY_SHPT,
                        DECODE(D5.PRESSURE_CONTROL,NULL,0,D5.PRESSURE_CONTROL) PRESSURE_CONTROL,DECODE(D6.TIME_LESS_8,NULL,0,D6.TIME_LESS_8) TIME_LESS_8,
                        DECODE(D7.TIME_8_9,NULL,0,D7.TIME_8_9) TIME_8_9,DECODE(D8.TIME_9_10,NULL,0,D8.TIME_9_10) TIME_9_10,
                        DECODE(D9.TIME_10_11,NULL,0,D9.TIME_10_11) TIME_10_11,DECODE(D10.TIME_11_12,NULL,0,D10.TIME_11_12) TIME_11_12,
                        DECODE(D11.TIME_MORE_12,NULL,0,D11.TIME_MORE_12) TIME_MORE_12,DECODE(D12.VENOUS_CATHETER,NULL,0,D12.VENOUS_CATHETER) VENOUS_CATHETER,
                        DECODE(D13.AUTOLOGOUS_FISTULA,NULL,0,D13.AUTOLOGOUS_FISTULA) AUTOLOGOUS_FISTULA,DECODE(D14.TEMP_VENOUS_CATHETER,NULL,0,D14.TEMP_VENOUS_CATHETER) TEMP_VENOUS_CATHETER,
                        DECODE(D15.ARTIFICIAL_VESSEL,NULL,0,D15.ARTIFICIAL_VESSEL) ARTIFICIAL_VESSEL,DECODE(D16.DOUBLE_VEIN,NULL,0,D16.DOUBLE_VEIN) DOUBLE_VEIN,
                        DECODE(D17.HIGH_AVF,NULL,0,D17.HIGH_AVF) HIGH_AVF,DECODE(D18.JUGULAR_VENOUS_CATHETER,NULL,0,D18.JUGULAR_VENOUS_CATHETER) JUGULAR_VENOUS_CATHETER,
                        DECODE(D19.SUBCLAVIAN_VENOUS_CATHETER,NULL,0,D19.SUBCLAVIAN_VENOUS_CATHETER) SUBCLAVIAN_VENOUS_CATHETER,DECODE(D20.FEMORAL_VENOUS_CATHETER,NULL,0,D20.FEMORAL_VENOUS_CATHETER) FEMORAL_VENOUS_CATHETER,
                        DECODE(D21.COMFORT,NULL,0,D21.COMFORT) COMFORT,DECODE(D22.MILD_DISCOMFORT,NULL,0,D22.MILD_DISCOMFORT) MILD_DISCOMFORT,
                        DECODE(D23.SEVERE_DISCOMFORT,NULL,0,D23.SEVERE_DISCOMFORT) SEVERE_DISCOMFORT,DECODE(D24.PERITONEAL_DIALYSIS,NULL,0,D24.PERITONEAL_DIALYSIS) PERITONEAL_DIALYSIS,
                        DECODE(D25.DJMNL,NULL,0,D25.DJMNL) DJMNL,
                        DECODE(D26.DGSJM,NULL,0,D26.DGSJM) DGSJM,
                        DECODE(D27.DJMRG,NULL,0,D27.DJMRG) DJMRG,
                        DECODE(D28.QTXGTL,NULL,0,D28.QTXGTL) QTXGTL
                        FROM
                        (SELECT TO_CHAR(REPORT_DATE,'YYYY-MM') DATE_MONTH FROM MED_REPORT_DATA
                        WHERE TO_CHAR(REPORT_DATE,'YYYY')=:YEAR GROUP BY TO_CHAR(REPORT_DATE,'YYYY-MM') ORDER BY TO_CHAR(REPORT_DATE,'YYYY-MM')) D
                        LEFT JOIN
                        (SELECT TO_CHAR(REPORT_DATE,'YYYY-MM') DATE_MONTH,SUM(REPORT_CURRENT_COUNT) UREA_REMOVE FROM MED_REPORT_DATA
                        WHERE REPORT_TYPE='017' GROUP BY TO_CHAR(REPORT_DATE,'YYYY-MM') ORDER BY TO_CHAR(REPORT_DATE,'YYYY-MM')) D1 ON D.DATE_MONTH=D1.DATE_MONTH
                        LEFT JOIN
                        (SELECT TO_CHAR(REPORT_DATE,'YYYY-MM') DATE_MONTH,SUM(REPORT_CURRENT_COUNT) RENAL_ANEMIA FROM MED_REPORT_DATA
                        WHERE REPORT_TYPE='018' GROUP BY TO_CHAR(REPORT_DATE,'YYYY-MM') ORDER BY TO_CHAR(REPORT_DATE,'YYYY-MM')) D2 ON D.DATE_MONTH=D2.DATE_MONTH
                        LEFT JOIN
                        (SELECT TO_CHAR(REPORT_DATE,'YYYY-MM') DATE_MONTH,SUM(REPORT_CURRENT_COUNT) CA_P_METABOLISM FROM MED_REPORT_DATA
                        WHERE REPORT_TYPE='019' GROUP BY TO_CHAR(REPORT_DATE,'YYYY-MM') ORDER BY TO_CHAR(REPORT_DATE,'YYYY-MM')) D3 ON D.DATE_MONTH=D3.DATE_MONTH
                        LEFT JOIN
                        (SELECT TO_CHAR(REPORT_DATE,'YYYY-MM') DATE_MONTH,SUM(REPORT_CURRENT_COUNT) SECONDARY_SHPT FROM MED_REPORT_DATA
                        WHERE REPORT_TYPE='020' GROUP BY TO_CHAR(REPORT_DATE,'YYYY-MM') ORDER BY TO_CHAR(REPORT_DATE,'YYYY-MM')) D4 ON D.DATE_MONTH=D4.DATE_MONTH
                        LEFT JOIN
                        (SELECT TO_CHAR(REPORT_DATE,'YYYY-MM') DATE_MONTH,SUM(REPORT_CURRENT_COUNT) PRESSURE_CONTROL FROM MED_REPORT_DATA
                        WHERE REPORT_TYPE='021' GROUP BY TO_CHAR(REPORT_DATE,'YYYY-MM') ORDER BY TO_CHAR(REPORT_DATE,'YYYY-MM')) D5 ON D.DATE_MONTH=D5.DATE_MONTH
                        LEFT JOIN
                        (SELECT TO_CHAR(REPORT_DATE,'YYYY-MM') DATE_MONTH,SUM(REPORT_CURRENT_COUNT) TIME_LESS_8 FROM MED_REPORT_DATA
                        WHERE REPORT_TYPE='031' GROUP BY TO_CHAR(REPORT_DATE,'YYYY-MM') ORDER BY TO_CHAR(REPORT_DATE,'YYYY-MM')) D6 ON D.DATE_MONTH=D6.DATE_MONTH
                        LEFT JOIN
                        (SELECT TO_CHAR(REPORT_DATE,'YYYY-MM') DATE_MONTH,SUM(REPORT_CURRENT_COUNT) TIME_8_9 FROM MED_REPORT_DATA
                        WHERE REPORT_TYPE='032' GROUP BY TO_CHAR(REPORT_DATE,'YYYY-MM') ORDER BY TO_CHAR(REPORT_DATE,'YYYY-MM')) D7 ON D.DATE_MONTH=D7.DATE_MONTH
                        LEFT JOIN
                        (SELECT TO_CHAR(REPORT_DATE,'YYYY-MM') DATE_MONTH,SUM(REPORT_CURRENT_COUNT) TIME_9_10 FROM MED_REPORT_DATA
                        WHERE REPORT_TYPE='033' GROUP BY TO_CHAR(REPORT_DATE,'YYYY-MM') ORDER BY TO_CHAR(REPORT_DATE,'YYYY-MM')) D8 ON D.DATE_MONTH=D8.DATE_MONTH
                        LEFT JOIN
                        (SELECT TO_CHAR(REPORT_DATE,'YYYY-MM') DATE_MONTH,SUM(REPORT_CURRENT_COUNT) TIME_10_11 FROM MED_REPORT_DATA
                        WHERE REPORT_TYPE='034' GROUP BY TO_CHAR(REPORT_DATE,'YYYY-MM') ORDER BY TO_CHAR(REPORT_DATE,'YYYY-MM')) D9 ON D.DATE_MONTH=D9.DATE_MONTH
                        LEFT JOIN
                        (SELECT TO_CHAR(REPORT_DATE,'YYYY-MM') DATE_MONTH,SUM(REPORT_CURRENT_COUNT) TIME_11_12 FROM MED_REPORT_DATA
                        WHERE REPORT_TYPE='035' GROUP BY TO_CHAR(REPORT_DATE,'YYYY-MM') ORDER BY TO_CHAR(REPORT_DATE,'YYYY-MM')) D10 ON D.DATE_MONTH=D10.DATE_MONTH
                        LEFT JOIN
                        (SELECT TO_CHAR(REPORT_DATE,'YYYY-MM') DATE_MONTH,SUM(REPORT_CURRENT_COUNT) TIME_MORE_12 FROM MED_REPORT_DATA
                        WHERE REPORT_TYPE='036' GROUP BY TO_CHAR(REPORT_DATE,'YYYY-MM') ORDER BY TO_CHAR(REPORT_DATE,'YYYY-MM')) D11 ON D.DATE_MONTH=D11.DATE_MONTH
                        LEFT JOIN
                        (SELECT TO_CHAR(REPORT_DATE,'YYYY-MM') DATE_MONTH,SUM(REPORT_CURRENT_COUNT) VENOUS_CATHETER FROM MED_REPORT_DATA
                        WHERE REPORT_TYPE='022' GROUP BY TO_CHAR(REPORT_DATE,'YYYY-MM') ORDER BY TO_CHAR(REPORT_DATE,'YYYY-MM')) D12 ON D.DATE_MONTH=D12.DATE_MONTH
                        LEFT JOIN
                        (SELECT TO_CHAR(REPORT_DATE,'YYYY-MM') DATE_MONTH,SUM(REPORT_CURRENT_COUNT) AUTOLOGOUS_FISTULA FROM MED_REPORT_DATA
                        WHERE REPORT_TYPE='023' GROUP BY TO_CHAR(REPORT_DATE,'YYYY-MM') ORDER BY TO_CHAR(REPORT_DATE,'YYYY-MM')) D13 ON D.DATE_MONTH=D13.DATE_MONTH
                        LEFT JOIN
                        (SELECT TO_CHAR(REPORT_DATE,'YYYY-MM') DATE_MONTH,SUM(REPORT_CURRENT_COUNT) TEMP_VENOUS_CATHETER FROM MED_REPORT_DATA
                        WHERE REPORT_TYPE='024' GROUP BY TO_CHAR(REPORT_DATE,'YYYY-MM') ORDER BY TO_CHAR(REPORT_DATE,'YYYY-MM')) D14 ON D.DATE_MONTH=D14.DATE_MONTH
                        LEFT JOIN
                        (SELECT TO_CHAR(REPORT_DATE,'YYYY-MM') DATE_MONTH,SUM(REPORT_CURRENT_COUNT) ARTIFICIAL_VESSEL FROM MED_REPORT_DATA
                        WHERE REPORT_TYPE='025' GROUP BY TO_CHAR(REPORT_DATE,'YYYY-MM') ORDER BY TO_CHAR(REPORT_DATE,'YYYY-MM')) D15 ON D.DATE_MONTH=D15.DATE_MONTH
                        LEFT JOIN
                        (SELECT TO_CHAR(REPORT_DATE,'YYYY-MM') DATE_MONTH,SUM(REPORT_CURRENT_COUNT) DOUBLE_VEIN FROM MED_REPORT_DATA
                        WHERE REPORT_TYPE='026' GROUP BY TO_CHAR(REPORT_DATE,'YYYY-MM') ORDER BY TO_CHAR(REPORT_DATE,'YYYY-MM')) D16 ON D.DATE_MONTH=D16.DATE_MONTH
                        LEFT JOIN
                        (SELECT TO_CHAR(REPORT_DATE,'YYYY-MM') DATE_MONTH,SUM(REPORT_CURRENT_COUNT) HIGH_AVF FROM MED_REPORT_DATA
                        WHERE REPORT_TYPE='027' GROUP BY TO_CHAR(REPORT_DATE,'YYYY-MM') ORDER BY TO_CHAR(REPORT_DATE,'YYYY-MM')) D17 ON D.DATE_MONTH=D17.DATE_MONTH
                        LEFT JOIN
                        (SELECT TO_CHAR(REPORT_DATE,'YYYY-MM') DATE_MONTH,SUM(REPORT_CURRENT_COUNT) JUGULAR_VENOUS_CATHETER FROM MED_REPORT_DATA
                        WHERE REPORT_TYPE='028' GROUP BY TO_CHAR(REPORT_DATE,'YYYY-MM') ORDER BY TO_CHAR(REPORT_DATE,'YYYY-MM')) D18 ON D.DATE_MONTH=D18.DATE_MONTH
                        LEFT JOIN
                        (SELECT TO_CHAR(REPORT_DATE,'YYYY-MM') DATE_MONTH,SUM(REPORT_CURRENT_COUNT) SUBCLAVIAN_VENOUS_CATHETER FROM MED_REPORT_DATA
                        WHERE REPORT_TYPE='029' GROUP BY TO_CHAR(REPORT_DATE,'YYYY-MM') ORDER BY TO_CHAR(REPORT_DATE,'YYYY-MM')) D19 ON D.DATE_MONTH=D19.DATE_MONTH
                        LEFT JOIN
                        (SELECT TO_CHAR(REPORT_DATE,'YYYY-MM') DATE_MONTH,SUM(REPORT_CURRENT_COUNT) FEMORAL_VENOUS_CATHETER FROM MED_REPORT_DATA
                        WHERE REPORT_TYPE='030' GROUP BY TO_CHAR(REPORT_DATE,'YYYY-MM') ORDER BY TO_CHAR(REPORT_DATE,'YYYY-MM')) D20 ON D.DATE_MONTH=D20.DATE_MONTH
                        LEFT JOIN
                        (SELECT TO_CHAR(REPORT_DATE,'YYYY-MM') DATE_MONTH,SUM(REPORT_CURRENT_COUNT) COMFORT FROM MED_REPORT_DATA
                        WHERE REPORT_TYPE='037' GROUP BY TO_CHAR(REPORT_DATE,'YYYY-MM') ORDER BY TO_CHAR(REPORT_DATE,'YYYY-MM')) D21 ON D.DATE_MONTH=D21.DATE_MONTH
                        LEFT JOIN
                        (SELECT TO_CHAR(REPORT_DATE,'YYYY-MM') DATE_MONTH,SUM(REPORT_CURRENT_COUNT) MILD_DISCOMFORT FROM MED_REPORT_DATA
                        WHERE REPORT_TYPE='038' GROUP BY TO_CHAR(REPORT_DATE,'YYYY-MM') ORDER BY TO_CHAR(REPORT_DATE,'YYYY-MM')) D22 ON D.DATE_MONTH=D22.DATE_MONTH
                        LEFT JOIN
                        (SELECT TO_CHAR(REPORT_DATE,'YYYY-MM') DATE_MONTH,SUM(REPORT_CURRENT_COUNT) SEVERE_DISCOMFORT FROM MED_REPORT_DATA
                        WHERE REPORT_TYPE='039' GROUP BY TO_CHAR(REPORT_DATE,'YYYY-MM') ORDER BY TO_CHAR(REPORT_DATE,'YYYY-MM')) D23 ON D.DATE_MONTH=D23.DATE_MONTH
                        LEFT JOIN
                        (SELECT TO_CHAR(REPORT_DATE,'YYYY-MM') DATE_MONTH,SUM(REPORT_CURRENT_COUNT) PERITONEAL_DIALYSIS FROM MED_REPORT_DATA
                        WHERE REPORT_TYPE='040' GROUP BY TO_CHAR(REPORT_DATE,'YYYY-MM') ORDER BY TO_CHAR(REPORT_DATE,'YYYY-MM')) D24 ON D.DATE_MONTH=D24.DATE_MONTH
                        LEFT JOIN
                        (SELECT TO_CHAR(REPORT_DATE,'YYYY-MM') DATE_MONTH,SUM(REPORT_CURRENT_COUNT) DJMNL FROM MED_REPORT_DATA
                        WHERE REPORT_TYPE='058' GROUP BY TO_CHAR(REPORT_DATE,'YYYY-MM') ORDER BY TO_CHAR(REPORT_DATE,'YYYY-MM')) D25 ON D.DATE_MONTH=D25.DATE_MONTH
                        LEFT JOIN
                        (SELECT TO_CHAR(REPORT_DATE,'YYYY-MM') DATE_MONTH,SUM(REPORT_CURRENT_COUNT) DGSJM FROM MED_REPORT_DATA
                        WHERE REPORT_TYPE='059' GROUP BY TO_CHAR(REPORT_DATE,'YYYY-MM') ORDER BY TO_CHAR(REPORT_DATE,'YYYY-MM')) D26 ON D.DATE_MONTH=D26.DATE_MONTH
                        LEFT JOIN
                        (SELECT TO_CHAR(REPORT_DATE,'YYYY-MM') DATE_MONTH,SUM(REPORT_CURRENT_COUNT) DJMRG FROM MED_REPORT_DATA
                        WHERE REPORT_TYPE='060' GROUP BY TO_CHAR(REPORT_DATE,'YYYY-MM') ORDER BY TO_CHAR(REPORT_DATE,'YYYY-MM')) D27 ON D.DATE_MONTH=D27.DATE_MONTH
                        LEFT JOIN
                        (SELECT TO_CHAR(REPORT_DATE,'YYYY-MM') DATE_MONTH,SUM(REPORT_CURRENT_COUNT) QTXGTL FROM MED_REPORT_DATA
                        WHERE REPORT_TYPE='061' GROUP BY TO_CHAR(REPORT_DATE,'YYYY-MM') ORDER BY TO_CHAR(REPORT_DATE,'YYYY-MM')) D28 ON D.DATE_MONTH=D28.DATE_MONTH
                        ORDER BY D.DATE_MONTH";
            }
        }

        /// <summary>
        /// 按日期获取患者质量监测指标列表
        /// </summary>
        public string GetQualityMonitorIndicatorByDate
        {
            get
            {
                return @"SELECT (TO_CHAR(:BEGINDATE,'YYYY-MM-DD')||'~'||TO_CHAR(:ENDDATE,'YYYY-MM-DD')) DATE_MONTH,DECODE(D.UREA_REMOVE,NULL,0,D.UREA_REMOVE) UREA_REMOVE,
                        DECODE(D.RENAL_ANEMIA,NULL,0,D.RENAL_ANEMIA) RENAL_ANEMIA,DECODE(D.CA_P_METABOLISM,NULL,0,D.CA_P_METABOLISM) CA_P_METABOLISM,
                        DECODE(D.SECONDARY_SHPT,NULL,0,D.SECONDARY_SHPT) SECONDARY_SHPT,DECODE(D.PRESSURE_CONTROL,NULL,0,D.PRESSURE_CONTROL) PRESSURE_CONTROL,
                        DECODE(D.TIME_LESS_8,NULL,0,D.TIME_LESS_8) TIME_LESS_8,DECODE(D.TIME_8_9,NULL,0,D.TIME_8_9) TIME_8_9,
                        DECODE(D.TIME_9_10,NULL,0,D.TIME_9_10) TIME_9_10,DECODE(D.TIME_10_11,NULL,0,D.TIME_10_11) TIME_10_11,
                        DECODE(D.TIME_11_12,NULL,0,D.TIME_11_12) TIME_11_12,DECODE(D.TIME_MORE_12,NULL,0,D.TIME_MORE_12) TIME_MORE_12,
                        DECODE(D.VENOUS_CATHETER,NULL,0,D.VENOUS_CATHETER) VENOUS_CATHETER,DECODE(D.AUTOLOGOUS_FISTULA,NULL,0,D.AUTOLOGOUS_FISTULA) AUTOLOGOUS_FISTULA,
                        DECODE(D.TEMP_VENOUS_CATHETER,NULL,0,D.TEMP_VENOUS_CATHETER) TEMP_VENOUS_CATHETER,DECODE(D.ARTIFICIAL_VESSEL,NULL,0,D.ARTIFICIAL_VESSEL) ARTIFICIAL_VESSEL,
                        DECODE(D.DOUBLE_VEIN,NULL,0,D.DOUBLE_VEIN) DOUBLE_VEIN,DECODE(D.HIGH_AVF,NULL,0,D.HIGH_AVF) HIGH_AVF,
                        DECODE(D.JUGULAR_VENOUS_CATHETER,NULL,0,D.JUGULAR_VENOUS_CATHETER) JUGULAR_VENOUS_CATHETER,DECODE(D.SUBCLAVIAN_VENOUS_CATHETER,NULL,0,D.SUBCLAVIAN_VENOUS_CATHETER) SUBCLAVIAN_VENOUS_CATHETER,
                        DECODE(D.FEMORAL_VENOUS_CATHETER,NULL,0,D.FEMORAL_VENOUS_CATHETER) FEMORAL_VENOUS_CATHETER,DECODE(D.COMFORT,NULL,0,D.COMFORT) COMFORT,
                        DECODE(D.MILD_DISCOMFORT,NULL,0,D.MILD_DISCOMFORT) MILD_DISCOMFORT,DECODE(D.SEVERE_DISCOMFORT,NULL,0,D.SEVERE_DISCOMFORT) SEVERE_DISCOMFORT,
                        DECODE(D.PERITONEAL_DIALYSIS,NULL,0,D.PERITONEAL_DIALYSIS) PERITONEAL_DIALYSIS,
                        DECODE(D.DJMNL,NULL,0,D.DJMNL) DJMNL,
                        DECODE(D.DGSJM,NULL,0,D.DGSJM) DGSJM,
                        DECODE(D.DJMRG,NULL,0,D.DJMRG) DJMRG,
                        DECODE(D.QTXGTL,NULL,0,D.QTXGTL) QTXGTL
                        FROM (SELECT * FROM
                        (SELECT SUM(REPORT_CURRENT_COUNT) UREA_REMOVE FROM MED_REPORT_DATA
                        WHERE REPORT_TYPE='017' AND TRUNC(REPORT_DATE)>=:BEGINDATE AND TRUNC(REPORT_DATE)<=:ENDDATE),
                        (SELECT SUM(REPORT_CURRENT_COUNT) RENAL_ANEMIA FROM MED_REPORT_DATA
                        WHERE REPORT_TYPE='018' AND TRUNC(REPORT_DATE)>=:BEGINDATE AND TRUNC(REPORT_DATE)<=:ENDDATE),
                        (SELECT SUM(REPORT_CURRENT_COUNT) CA_P_METABOLISM FROM MED_REPORT_DATA
                        WHERE REPORT_TYPE='019' AND TRUNC(REPORT_DATE)>=:BEGINDATE AND TRUNC(REPORT_DATE)<=:ENDDATE),
                        (SELECT SUM(REPORT_CURRENT_COUNT) SECONDARY_SHPT FROM MED_REPORT_DATA
                        WHERE REPORT_TYPE='020' AND TRUNC(REPORT_DATE)>=:BEGINDATE AND TRUNC(REPORT_DATE)<=:ENDDATE),
                        (SELECT SUM(REPORT_CURRENT_COUNT) PRESSURE_CONTROL FROM MED_REPORT_DATA
                        WHERE REPORT_TYPE='021' AND TRUNC(REPORT_DATE)>=:BEGINDATE AND TRUNC(REPORT_DATE)<=:ENDDATE),
                        (SELECT SUM(REPORT_CURRENT_COUNT) TIME_LESS_8 FROM MED_REPORT_DATA
                        WHERE REPORT_TYPE='031' AND TRUNC(REPORT_DATE)>=:BEGINDATE AND TRUNC(REPORT_DATE)<=:ENDDATE),
                        (SELECT SUM(REPORT_CURRENT_COUNT) TIME_8_9 FROM MED_REPORT_DATA
                        WHERE REPORT_TYPE='032' AND TRUNC(REPORT_DATE)>=:BEGINDATE AND TRUNC(REPORT_DATE)<=:ENDDATE),
                        (SELECT SUM(REPORT_CURRENT_COUNT) TIME_9_10 FROM MED_REPORT_DATA
                        WHERE REPORT_TYPE='033' AND TRUNC(REPORT_DATE)>=:BEGINDATE AND TRUNC(REPORT_DATE)<=:ENDDATE),
                        (SELECT SUM(REPORT_CURRENT_COUNT) TIME_10_11 FROM MED_REPORT_DATA
                        WHERE REPORT_TYPE='034' AND TRUNC(REPORT_DATE)>=:BEGINDATE AND TRUNC(REPORT_DATE)<=:ENDDATE),
                        (SELECT SUM(REPORT_CURRENT_COUNT) TIME_11_12 FROM MED_REPORT_DATA
                        WHERE REPORT_TYPE='035' AND TRUNC(REPORT_DATE)>=:BEGINDATE AND TRUNC(REPORT_DATE)<=:ENDDATE),
                        (SELECT SUM(REPORT_CURRENT_COUNT) TIME_MORE_12 FROM MED_REPORT_DATA
                        WHERE REPORT_TYPE='036' AND TRUNC(REPORT_DATE)>=:BEGINDATE AND TRUNC(REPORT_DATE)<=:ENDDATE),
                        (SELECT SUM(REPORT_CURRENT_COUNT) VENOUS_CATHETER FROM MED_REPORT_DATA
                        WHERE REPORT_TYPE='022' AND TRUNC(REPORT_DATE)>=:BEGINDATE AND TRUNC(REPORT_DATE)<=:ENDDATE),
                        (SELECT SUM(REPORT_CURRENT_COUNT) AUTOLOGOUS_FISTULA FROM MED_REPORT_DATA
                        WHERE REPORT_TYPE='023' AND TRUNC(REPORT_DATE)>=:BEGINDATE AND TRUNC(REPORT_DATE)<=:ENDDATE),
                        (SELECT SUM(REPORT_CURRENT_COUNT) TEMP_VENOUS_CATHETER FROM MED_REPORT_DATA
                        WHERE REPORT_TYPE='024' AND TRUNC(REPORT_DATE)>=:BEGINDATE AND TRUNC(REPORT_DATE)<=:ENDDATE),
                        (SELECT SUM(REPORT_CURRENT_COUNT) ARTIFICIAL_VESSEL FROM MED_REPORT_DATA
                        WHERE REPORT_TYPE='025' AND TRUNC(REPORT_DATE)>=:BEGINDATE AND TRUNC(REPORT_DATE)<=:ENDDATE),
                        (SELECT SUM(REPORT_CURRENT_COUNT) DOUBLE_VEIN FROM MED_REPORT_DATA
                        WHERE REPORT_TYPE='026' AND TRUNC(REPORT_DATE)>=:BEGINDATE AND TRUNC(REPORT_DATE)<=:ENDDATE),
                        (SELECT SUM(REPORT_CURRENT_COUNT) HIGH_AVF FROM MED_REPORT_DATA
                        WHERE REPORT_TYPE='027' AND TRUNC(REPORT_DATE)>=:BEGINDATE AND TRUNC(REPORT_DATE)<=:ENDDATE),
                        (SELECT SUM(REPORT_CURRENT_COUNT) JUGULAR_VENOUS_CATHETER FROM MED_REPORT_DATA
                        WHERE REPORT_TYPE='028' AND TRUNC(REPORT_DATE)>=:BEGINDATE AND TRUNC(REPORT_DATE)<=:ENDDATE),
                        (SELECT SUM(REPORT_CURRENT_COUNT) SUBCLAVIAN_VENOUS_CATHETER FROM MED_REPORT_DATA
                        WHERE REPORT_TYPE='029' AND TRUNC(REPORT_DATE)>=:BEGINDATE AND TRUNC(REPORT_DATE)<=:ENDDATE),
                        (SELECT SUM(REPORT_CURRENT_COUNT) FEMORAL_VENOUS_CATHETER FROM MED_REPORT_DATA
                        WHERE REPORT_TYPE='030' AND TRUNC(REPORT_DATE)>=:BEGINDATE AND TRUNC(REPORT_DATE)<=:ENDDATE),
                        (SELECT SUM(REPORT_CURRENT_COUNT) COMFORT FROM MED_REPORT_DATA
                        WHERE REPORT_TYPE='037' AND TRUNC(REPORT_DATE)>=:BEGINDATE AND TRUNC(REPORT_DATE)<=:ENDDATE),
                        (SELECT SUM(REPORT_CURRENT_COUNT) MILD_DISCOMFORT FROM MED_REPORT_DATA
                        WHERE REPORT_TYPE='038' AND TRUNC(REPORT_DATE)>=:BEGINDATE AND TRUNC(REPORT_DATE)<=:ENDDATE),
                        (SELECT SUM(REPORT_CURRENT_COUNT) SEVERE_DISCOMFORT FROM MED_REPORT_DATA
                        WHERE REPORT_TYPE='039' AND TRUNC(REPORT_DATE)>=:BEGINDATE AND TRUNC(REPORT_DATE)<=:ENDDATE),
                        (SELECT SUM(REPORT_CURRENT_COUNT) PERITONEAL_DIALYSIS FROM MED_REPORT_DATA
                        WHERE REPORT_TYPE='040' AND TRUNC(REPORT_DATE)>=:BEGINDATE AND TRUNC(REPORT_DATE)<=:ENDDATE),
                        (SELECT SUM(REPORT_CURRENT_COUNT) DJMNL FROM MED_REPORT_DATA
                        WHERE REPORT_TYPE='058' AND TRUNC(REPORT_DATE)>=:BEGINDATE AND TRUNC(REPORT_DATE)<=:ENDDATE),
                        (SELECT SUM(REPORT_CURRENT_COUNT) DGSJM FROM MED_REPORT_DATA
                        WHERE REPORT_TYPE='059' AND TRUNC(REPORT_DATE)>=:BEGINDATE AND TRUNC(REPORT_DATE)<=:ENDDATE),
                        (SELECT SUM(REPORT_CURRENT_COUNT) DJMRG FROM MED_REPORT_DATA
                        WHERE REPORT_TYPE='060' AND TRUNC(REPORT_DATE)>=:BEGINDATE AND TRUNC(REPORT_DATE)<=:ENDDATE),
                        (SELECT SUM(REPORT_CURRENT_COUNT) QTXGTL FROM MED_REPORT_DATA
                        WHERE REPORT_TYPE='061' AND TRUNC(REPORT_DATE)>=:BEGINDATE AND TRUNC(REPORT_DATE)<=:ENDDATE)) D";
            }
        }

        /// <summary>
        /// 取患者是否是最新三个月开始治疗的，如果是就算新入患者返回1，否则返回0。
        /// </summary>
        public string GetPatientTypeIsNew
        {
            get
            {
                return @"SELECT COUNT(0) FROM DUAL WHERE (SELECT CURE_CREATE_DATE FROM (SELECT * FROM MED_CURE_MAIN  
                        WHERE HEMODIALYSIS_ID = :HEMODIALYSIS_ID ORDER BY CURE_CREATE_DATE 
                        )WHERE ROWNUM  = 1 ) > ADD_MONTHS(SYSDATE,-3)";
            }
        }

        /// <summary>
        /// 根据日期获取最近一次透析为基准，上周透析过、三个月内连续透析过患者透析编号
        /// </summary>
        public string GetHemoIdInLastWeekAndThreeMonthsByDate
        {
            get
            {
                return @"
                        SELECT DISTINCT T.HEMODIALYSIS_ID FROM
                        (
                          SELECT * FROM (SELECT RANK() OVER (PARTITION BY HEMODIALYSIS_ID ORDER BY CURE_CREATE_DATE DESC) CURE_RANK,HEMODIALYSIS_ID,PURIFICATION_MODE,CURE_CREATE_DATE FROM MED_CURE_MAIN
                          WHERE TRUNC(CURE_CREATE_DATE)>=TRUNC(:BEGINTIME) AND TRUNC(CURE_CREATE_DATE)<=TRUNC(:ENDTIME) AND CURE_STATUS != '4')
                          WHERE CURE_RANK=1
                        ) T
                        WHERE EXISTS
                        (
                          SELECT T2.HEMODIALYSIS_ID FROM
                          (
                            SELECT DISTINCT T1.HEMODIALYSIS_ID,TRUNC(T1.CURE_CREATE_DATE,'IW') FROM MED_CURE_MAIN T1
                            WHERE TRUNC(T1.CURE_CREATE_DATE)>=TRUNC(SYSDATE,'IW')-7-11*7 AND TRUNC(T1.CURE_CREATE_DATE)<=TRUNC(SYSDATE,'IW')-1 AND T1.CURE_STATUS != '4'
                            GROUP BY T1.HEMODIALYSIS_ID,TRUNC(T1.CURE_CREATE_DATE,'IW') HAVING COUNT(T1.HEMODIALYSIS_ID)>=2
                          ) T2
                          WHERE T2.HEMODIALYSIS_ID=T.HEMODIALYSIS_ID GROUP BY T2.HEMODIALYSIS_ID HAVING(COUNT(T2.HEMODIALYSIS_ID)=12)
                        )";
            }
        }

        /// <summary>
        /// 获取每周2次或者3次的患者
        /// </summary>
        public string GetWeekTwoOrThirdCountPatientsHemoId
        {
            get
            {
                return @"
                   SELECT DISTINCT TRUNC(CURE_CREATE_DATE, 'IW') AS WEEK_MONDAY,
                                    HEMODIALYSIS_ID,
                                    COUNT(HEMODIALYSIS_ID) AS COUNT
                      FROM MED_CURE_MAIN
                     WHERE TRUNC(CURE_CREATE_DATE) >= TRUNC(SYSDATE, 'IW') - 7 - 11 * 7
                       AND TRUNC(CURE_CREATE_DATE) <= TRUNC(SYSDATE, 'IW') - 1
                       AND CURE_STATUS != '4'
                     GROUP BY HEMODIALYSIS_ID, TRUNC(CURE_CREATE_DATE, 'IW')
                     HAVING COUNT(HEMODIALYSIS_ID) = 2 OR COUNT(HEMODIALYSIS_ID) = 3
                     ORDER BY HEMODIALYSIS_ID";
            }
        }


        /// <summary>
        /// 根据日期获取患者透析编号
        /// </summary>
        public string GetHemoIdByDate
        {
            get
            {
                return @"
                        SELECT DISTINCT T.HEMODIALYSIS_ID FROM
                        (
                          SELECT * FROM (SELECT RANK() OVER (PARTITION BY HEMODIALYSIS_ID ORDER BY CURE_CREATE_DATE DESC) CURE_RANK,HEMODIALYSIS_ID,PURIFICATION_MODE,CURE_CREATE_DATE FROM MED_CURE_MAIN
                          WHERE TRUNC(CURE_CREATE_DATE)>=TRUNC(:BEGINTIME) AND TRUNC(CURE_CREATE_DATE)<=TRUNC(:ENDTIME) AND CURE_STATUS != '4')
                          WHERE CURE_RANK=1
                        ) T";
            }
        }

        public string GetPatientBaseRecordProtopathyByDate
        {
            get
            {
                return @"SELECT SUM(K.CGN) CGN,SUM(K.DN) DN,SUM(K.PCKD) PCKD,SUM(K.HTN) HTN,SUM(K.UUO) UUO,SUM(K.TFSB) TFSB,SUM(K.BYBX) BYBX,SUM(K.OTHER_PROTOPATHY) OTHER_PROTOPATHY
                    FROM MED_BASE_RECORD K
                    WHERE K.HEMODIALYSIS_ID IN (
                        SELECT DISTINCT T.HEMODIALYSIS_ID FROM
                        (
                          SELECT * FROM (SELECT RANK() OVER (PARTITION BY HEMODIALYSIS_ID ORDER BY CURE_CREATE_DATE DESC) CURE_RANK,HEMODIALYSIS_ID,PURIFICATION_MODE,CURE_CREATE_DATE FROM MED_CURE_MAIN
                          WHERE TRUNC(CURE_CREATE_DATE)>=TRUNC(:BEGINTIME) AND TRUNC(CURE_CREATE_DATE)<=TRUNC(:ENDTIME) AND CURE_STATUS != '4')
                          WHERE CURE_RANK=1
                        ) T
                        WHERE EXISTS
                        (
                          SELECT T2.HEMODIALYSIS_ID FROM
                          (
                            SELECT DISTINCT T1.HEMODIALYSIS_ID,TRUNC(T1.CURE_CREATE_DATE,'IW') FROM MED_CURE_MAIN T1
                            WHERE TRUNC(T1.CURE_CREATE_DATE)>=TRUNC(SYSDATE,'IW')-7-11*7 AND TRUNC(T1.CURE_CREATE_DATE)<=TRUNC(SYSDATE,'IW')-1 AND T1.CURE_STATUS != '4'
                            GROUP BY T1.HEMODIALYSIS_ID,TRUNC(T1.CURE_CREATE_DATE,'IW') HAVING COUNT(T1.HEMODIALYSIS_ID)>=2
                          ) T2
                          WHERE T2.HEMODIALYSIS_ID=T.HEMODIALYSIS_ID GROUP BY T2.HEMODIALYSIS_ID HAVING(COUNT(T2.HEMODIALYSIS_ID)=12)
                        ))";
            }
        }

        /// <summary>
        /// 获取当前时间为基准，上周透析过、三个月内连续透析过患者透析编号
        /// </summary>
        public string GetHemoIdInLastWeekAndThreeMonths
        {
            get
            {
                return @"SELECT T.HEMODIALYSIS_ID FROM
                        (
                          SELECT DISTINCT T1.HEMODIALYSIS_ID,TRUNC(T1.CURE_CREATE_DATE,'IW') FROM MED_CURE_MAIN T1
                          WHERE TRUNC(T1.CURE_CREATE_DATE)>=TRUNC(SYSDATE,'IW')-7-11*7 AND TRUNC(T1.CURE_CREATE_DATE)<=TRUNC(SYSDATE,'IW')-1 AND T1.CURE_STATUS != '4'
                          GROUP BY T1.HEMODIALYSIS_ID,TRUNC(T1.CURE_CREATE_DATE,'IW') HAVING COUNT(T1.HEMODIALYSIS_ID)>=2
                        ) T
                        GROUP BY T.HEMODIALYSIS_ID HAVING(COUNT(T.HEMODIALYSIS_ID)=12)";
            }
        }

        /// <summary>
        /// 按月份根据透析编号和日期获取患者透析次数
        /// </summary>
        public string GetCureCountByHemoIdAndDate
        {
            get
            {
                return @"SELECT T.*,(T.HD_COUNT+T.HDF_COUNT+T.HF_COUNT+T.HP_COUNT+T.HDHP_COUNT+T.CRRT_COUNT) AS SUB_COUNT FROM
                        (SELECT TO_CHAR(CURE_CREATE_DATE, 'YYYY-MM') CURE_MONTH,COUNT(DECODE(T.PURIFICATION_MODE,(SELECT ITEM_ID FROM MED_COMMON_ITEMLIST WHERE ITEM_NAME= 'HD' AND ITEM_TYPE='净化方式'),T.PURIFICATION_MODE)) AS HD_COUNT,
                        COUNT(DECODE(T.PURIFICATION_MODE,(SELECT ITEM_ID FROM MED_COMMON_ITEMLIST WHERE ITEM_NAME= 'HDF' AND ITEM_TYPE='净化方式'),T.PURIFICATION_MODE)) AS HDF_COUNT,
                        COUNT(DECODE(T.PURIFICATION_MODE,(SELECT ITEM_ID FROM MED_COMMON_ITEMLIST WHERE ITEM_NAME= 'HF' AND ITEM_TYPE='净化方式'),T.PURIFICATION_MODE)) AS HF_COUNT,
                        COUNT(DECODE(T.PURIFICATION_MODE,(SELECT ITEM_ID FROM MED_COMMON_ITEMLIST WHERE ITEM_NAME= 'HP' AND ITEM_TYPE='净化方式'),T.PURIFICATION_MODE)) AS HP_COUNT,
                        COUNT(DECODE(T.PURIFICATION_MODE,(SELECT ITEM_ID FROM MED_COMMON_ITEMLIST WHERE ITEM_NAME= 'HD+HP' AND ITEM_TYPE='净化方式'),T.PURIFICATION_MODE)) AS HDHP_COUNT,
                        COUNT(DECODE(T.PURIFICATION_MODE,(SELECT ITEM_ID FROM MED_COMMON_ITEMLIST WHERE ITEM_NAME= 'CRRT' AND ITEM_TYPE='净化方式'),T.PURIFICATION_MODE)) AS CRRT_COUNT
                        FROM MED_CURE_MAIN T
                        WHERE TRUNC(T.CURE_CREATE_DATE) >= TRUNC(:BEGINTIME) AND TRUNC(T.CURE_CREATE_DATE) <= TRUNC(:ENDTIME)
                        AND T.CURE_STATUS != '4' AND T.HEMODIALYSIS_ID=:HEMODIALYSIS_ID
                        GROUP BY TO_CHAR(T.CURE_CREATE_DATE, 'YYYY-MM')
                        HAVING(COUNT(1) > 0) ORDER BY TO_CHAR(T.CURE_CREATE_DATE, 'YYYY-MM')) T";
            }
        }

        /// <summary>
        /// 按月份根据透析编号和日期获取患者血管通路例数
        /// </summary>
        /// 1、使用自体内瘘的患者（自体内瘘）
        /// 使用移植物内瘘的患者（人造血管）
        /// 使用双静脉作为维持血管通路的患者（双静脉）
        /// 使用带cuff中心静脉留置导管作为维持血管通路的患者（颈内静脉+永久性通路）
        /// 使用其他维持血管通路的患者以上是基本资料统计文档中的内容，如何与系统中通路方式对应
        public string GetAccessCountByHemoIdAndDate
        {
            get
            {
                return @"SELECT T.*, (NL_COUNT + SJM_COUNT + RZXG_COUNT + LSG_COUNT + BYJDG_COUNT + YJDG_COUNT) AS SUB_COUNT
                                FROM (SELECT CREATE_MONTH,
                                            SUM(NL_COUNT) AS NL_COUNT,
                                            SUM(SJM_COUNT) AS SJM_COUNT,
                                            SUM(RZXG_COUNT) AS RZXG_COUNT,
                                            SUM(LSG_COUNT) AS LSG_COUNT,
                                            SUM(BYJDG_COUNT) AS BYJDG_COUNT,
                                            SUM(YJDG_COUNT) AS YJDG_COUNT
                                        FROM (SELECT TO_CHAR(CURE_CREATE_DATE, 'YYYY-MM') CREATE_MONTH,
                                                    COUNT(VASCULAR_ACCESS_TYPE) AS NL_COUNT,
                                                    0 AS SJM_COUNT,
                                                     0 AS RZXG_COUNT,
                                                    0 AS LSG_COUNT,
                                                    0 AS BYJDG_COUNT,
                                                    0 AS YJDG_COUNT
                                                FROM ( SELECT K.CURE_CREATE_DATE, S.VASCULAR_ACCESS_TYPE
                                                    FROM (SELECT *
                                                            FROM (SELECT T.*
                                                                    FROM MED_CURE_MAIN T
                                                                   WHERE T.HEMODIALYSIS_ID = :HEMODIALYSIS_ID
                                                                     AND TRUNC(T.CURE_CREATE_DATE) >= TRUNC(:BEGINTIME)
                                                                     AND TRUNC(T.CURE_CREATE_DATE) <= TRUNC(:ENDTIME)
                                                                   ORDER BY T.CURE_CREATE_DATE DESC) Z
                                                           WHERE ROWNUM = 1) K
                                                    LEFT JOIN MED_VASCULAR_ACCESS S
                                                      ON K.VASCULAR_ACCESS_ID = S.VASCULAR_ACCESS_ID
                                                     AND K.HEMODIALYSIS_ID = S.HEMODIALYSIS_ID
                                                   WHERE S.VASCULAR_ACCESS_TYPE =
                                                         (SELECT ITEM_ID
                                                            FROM MED_COMMON_ITEMLIST
                                                           WHERE ITEM_TYPE = '血管通路'
                                                             AND ITEM_NAME = '自体内瘘'
                                                             AND STATUS = '1'))
                                                   WHERE ROWNUM = 1
                                                GROUP BY TO_CHAR(CURE_CREATE_DATE, 'YYYY-MM')
                                            UNION
                                            SELECT TO_CHAR(CURE_CREATE_DATE, 'YYYY-MM') CREATE_MONTH,
                                                    0 AS NL_COUNT,
                                                    COUNT(VASCULAR_ACCESS_TYPE) AS SJM_COUNT,
                                                     0 AS RZXG_COUNT,
                                                    0 AS LSG_COUNT,
                                                    0 AS BYJDG_COUNT,
                                                    0 AS YJDG_COUNT
                                                FROM (SELECT K.CURE_CREATE_DATE, S.VASCULAR_ACCESS_TYPE
                                                    FROM (SELECT *
                                                            FROM (SELECT T.*
                                                                    FROM MED_CURE_MAIN T
                                                                   WHERE T.HEMODIALYSIS_ID = :HEMODIALYSIS_ID
                                                                     AND TRUNC(T.CURE_CREATE_DATE) >= TRUNC(:BEGINTIME)
                                                                     AND TRUNC(T.CURE_CREATE_DATE) <= TRUNC(:ENDTIME)
                                                                   ORDER BY T.CURE_CREATE_DATE DESC) Z
                                                           WHERE ROWNUM = 1) K
                                                    LEFT JOIN MED_VASCULAR_ACCESS S
                                                      ON K.VASCULAR_ACCESS_ID = S.VASCULAR_ACCESS_ID
                                                     AND K.HEMODIALYSIS_ID = S.HEMODIALYSIS_ID
                                                   WHERE S.VASCULAR_ACCESS_TYPE =
                                                         (SELECT ITEM_ID
                                                            FROM MED_COMMON_ITEMLIST
                                                           WHERE ITEM_TYPE = '血管通路'
                                                             AND ITEM_NAME = '双静脉'
                                                             AND STATUS = '1')) WHERE ROWNUM = 1
                                                GROUP BY TO_CHAR(CURE_CREATE_DATE, 'YYYY-MM')
                                           UNION
                                                    SELECT TO_CHAR(CURE_CREATE_DATE, 'YYYY-MM') CREATE_MONTH,
                                                           0 AS NL_COUNT,
                                                           0 AS SJM_COUNT,
                                                           COUNT(VASCULAR_ACCESS_TYPE) AS RZXG_COUNT,
                                                           0 AS LSG_COUNT,
                                                           0 AS BYJDG_COUNT,
                                                           0 AS YJDG_COUNT
                                                      FROM (SELECT K.CURE_CREATE_DATE, S.VASCULAR_ACCESS_TYPE
                                                        FROM (SELECT *
                                                                FROM (SELECT T.*
                                                                        FROM MED_CURE_MAIN T
                                                                       WHERE T.HEMODIALYSIS_ID = :HEMODIALYSIS_ID
                                                                         AND TRUNC(T.CURE_CREATE_DATE) >= TRUNC(:BEGINTIME)
                                                                         AND TRUNC(T.CURE_CREATE_DATE) <= TRUNC(:ENDTIME)
                                                                       ORDER BY T.CURE_CREATE_DATE DESC) Z
                                                               WHERE ROWNUM = 1) K
                                                        LEFT JOIN MED_VASCULAR_ACCESS S
                                                          ON K.VASCULAR_ACCESS_ID = S.VASCULAR_ACCESS_ID
                                                         AND K.HEMODIALYSIS_ID = S.HEMODIALYSIS_ID
                                                       WHERE S.VASCULAR_ACCESS_TYPE =
                                                             (SELECT ITEM_ID
                                                                FROM MED_COMMON_ITEMLIST
                                                               WHERE ITEM_TYPE = '血管通路'
                                                                 AND ITEM_NAME = '人造血管'
                                                                 AND STATUS = '1')) WHERE ROWNUM = 1
                                                     GROUP BY TO_CHAR(CURE_CREATE_DATE, 'YYYY-MM')
                                            UNION
                                            SELECT TO_CHAR(CURE_CREATE_DATE, 'YYYY-MM') CREATE_MONTH,
                                                    0 AS NL_COUNT,
                                                    0 AS SJM_COUNT,
                                                     0 AS RZXG_COUNT,
                                                    COUNT(ACCESS_CLASS) AS LSG_COUNT,
                                                    0 AS BYJDG_COUNT,
                                                    0 AS YJDG_COUNT
                                                FROM (SELECT K.CURE_CREATE_DATE, S.ACCESS_CLASS
                                                    FROM (SELECT *
                                                            FROM (SELECT T.*
                                                                    FROM MED_CURE_MAIN T
                                                                   WHERE T.HEMODIALYSIS_ID = :HEMODIALYSIS_ID
                                                                     AND TRUNC(T.CURE_CREATE_DATE) >= TRUNC(:BEGINTIME)
                                                                     AND TRUNC(T.CURE_CREATE_DATE) <= TRUNC(:ENDTIME)
                                                                   ORDER BY T.CURE_CREATE_DATE DESC) Z
                                                           WHERE ROWNUM = 1) K
                                                    LEFT JOIN MED_VASCULAR_ACCESS S
                                                      ON K.VASCULAR_ACCESS_ID = S.VASCULAR_ACCESS_ID
                                                     AND K.HEMODIALYSIS_ID = S.HEMODIALYSIS_ID
                                                   WHERE S.ACCESS_CLASS =
                                                         (SELECT ITEM_ID
                                                            FROM MED_COMMON_ITEMLIST
                                                           WHERE ITEM_TYPE = '通路分类'
                                                             AND ITEM_NAME = '临时性通路'
                                                             AND STATUS = '1')) WHERE ROWNUM = 1
                                                GROUP BY TO_CHAR(CURE_CREATE_DATE, 'YYYY-MM')
                                            UNION
                                            SELECT TO_CHAR(CURE_CREATE_DATE, 'YYYY-MM') CREATE_MONTH,
                                                    0 AS NL_COUNT,
                                                    0 AS SJM_COUNT,
                                                    0 AS RZXG_COUNT,
                                                    0 AS LSG_COUNT,
                                                    COUNT(ACCESS_CLASS) AS BYJDG_COUNT,
                                                    0 AS YJDG_COUNT
                                                FROM (SELECT K.CURE_CREATE_DATE, S.ACCESS_CLASS
                                                    FROM (SELECT *
                                                            FROM (SELECT T.*
                                                                    FROM MED_CURE_MAIN T
                                                                   WHERE T.HEMODIALYSIS_ID = :HEMODIALYSIS_ID
                                                                     AND TRUNC(T.CURE_CREATE_DATE) >= TRUNC(:BEGINTIME)
                                                                     AND TRUNC(T.CURE_CREATE_DATE) <= TRUNC(:ENDTIME)
                                                                   ORDER BY T.CURE_CREATE_DATE DESC) Z
                                                           WHERE ROWNUM = 1) K
                                                    LEFT JOIN MED_VASCULAR_ACCESS S
                                                      ON K.VASCULAR_ACCESS_ID = S.VASCULAR_ACCESS_ID
                                                     AND K.HEMODIALYSIS_ID = S.HEMODIALYSIS_ID
                                                   WHERE S.ACCESS_CLASS =
                                                         (SELECT ITEM_ID
                                                            FROM MED_COMMON_ITEMLIST
                                                           WHERE ITEM_TYPE = '通路分类'
                                                             AND ITEM_NAME = '半永久通管'
                                                             AND STATUS = '1')) WHERE ROWNUM = 1
                                                GROUP BY TO_CHAR(CURE_CREATE_DATE, 'YYYY-MM')
                                            UNION
                                            SELECT TO_CHAR(CURE_CREATE_DATE, 'YYYY-MM') CREATE_MONTH,
                                                    0 AS NL_COUNT,
                                                    0 AS SJM_COUNT,
                                                     0 AS RZXG_COUNT,
                                                    0 AS LSG_COUNT,
                                                    0 AS BYJDG_COUNT,
                                                    COUNT(ACCESS_CLASS) AS YJDG_COUNT
                                                FROM (SELECT K.CURE_CREATE_DATE, S.ACCESS_CLASS
                                                    FROM (SELECT *
                                                            FROM (SELECT T.*
                                                                    FROM MED_CURE_MAIN T
                                                                   WHERE T.HEMODIALYSIS_ID = :HEMODIALYSIS_ID
                                                                     AND TRUNC(T.CURE_CREATE_DATE) >= TRUNC(:BEGINTIME)
                                                                     AND TRUNC(T.CURE_CREATE_DATE) <= TRUNC(:ENDTIME)
                                                                   ORDER BY T.CURE_CREATE_DATE DESC) Z
                                                           WHERE ROWNUM = 1) K
                                                    LEFT JOIN MED_VASCULAR_ACCESS S
                                                      ON K.VASCULAR_ACCESS_ID = S.VASCULAR_ACCESS_ID
                                                     AND K.HEMODIALYSIS_ID = S.HEMODIALYSIS_ID
                                                   WHERE S.VASCULAR_ACCESS_TYPE =
                                                         (SELECT ITEM_ID
                                                            FROM MED_COMMON_ITEMLIST
                                                           WHERE ITEM_TYPE = '血管通路'
                                                             AND ITEM_NAME = '颈内静脉置管'
                                                             AND STATUS = '1')
                                                     AND S.ACCESS_CLASS = (SELECT ITEM_ID
                                                                             FROM MED_COMMON_ITEMLIST
                                                                            WHERE ITEM_TYPE = '通路分类'
                                                                              AND ITEM_NAME = '永久性通路'
                                                                              AND STATUS = '1'))  WHERE ROWNUM = 1
                                                GROUP BY TO_CHAR(CURE_CREATE_DATE, 'YYYY-MM'))
                                        GROUP BY CREATE_MONTH
                                        ORDER BY CREATE_MONTH) T";
            }
        }

        /// <summary>
        /// 根据透析编号和日期获取患者导管手术例数
        /// </summary>
        public string GetDuctOperationCountByHemoIdAndDate
        {
            get
            {
                return @"SELECT T.*,(NL_COUNT + LSG_COUNT + BYJDG_COUNT + YJDG_COUNT) AS SUB_COUNT FROM
                        (
                          SELECT T.CREATE_MONTH,SUM(T.NL_COUNT) AS NL_COUNT,SUM(T.LSG_COUNT) AS LSG_COUNT,SUM(T.BYJDG_COUNT) AS BYJDG_COUNT,SUM(T.YJDG_COUNT) AS YJDG_COUNT FROM
                          (
                            SELECT TO_CHAR(T.CREATEDDATE, 'YYYY-MM') CREATE_MONTH,COUNT(T.OPE_NAME) AS NL_COUNT,0 AS LSG_COUNT,0 AS BYJDG_COUNT,0 AS YJDG_COUNT FROM
                            (
                            SELECT T.CREATEDDATE,T.OPE_NAME FROM MED_PATIENTS_OPERATOR T
                            LEFT JOIN MED_COMMON_ITEMLIST ITEM ON T.OPE_NAME=ITEM.ITEM_ID
                            WHERE T.HEMODIALYSIS_ID = :HEMODIALYSIS_ID AND ITEM.ITEM_TYPE='手术名称' AND ITEM.ITEM_NAME='动静脉内瘘术' AND ITEM.STATUS = '1'
                            AND TRUNC(T.CREATEDDATE) >= TRUNC(:BEGINTIME)
                            AND TRUNC(T.CREATEDDATE) <= TRUNC(:ENDTIME)
                            ORDER BY T.CREATEDDATE DESC
                            ) T
                            WHERE ROWNUM = 1 GROUP BY TO_CHAR(T.CREATEDDATE, 'YYYY-MM')
                            UNION
                            SELECT TO_CHAR(T.CREATEDDATE, 'YYYY-MM') CREATE_MONTH,0 AS NL_COUNT,COUNT(T.VASCULARV_TYPE) AS LSG_COUNT,0 AS BYJDG_COUNT,0 AS YJDG_COUNT FROM
                            (
                            SELECT T.CREATEDDATE,T.VASCULARV_TYPE FROM MED_PATIENTS_OPERATOR T
                            LEFT JOIN MED_COMMON_ITEMLIST ITEM ON T.VASCULARV_TYPE=ITEM.ITEM_ID
                            WHERE T.HEMODIALYSIS_ID = :HEMODIALYSIS_ID AND ITEM.ITEM_TYPE='通路分类' AND ITEM.ITEM_NAME='临时性通路' AND ITEM.STATUS = '1'
                            AND TRUNC(T.CREATEDDATE) >= TRUNC(:BEGINTIME)
                            AND TRUNC(T.CREATEDDATE) <= TRUNC(:ENDTIME)
                            ORDER BY T.CREATEDDATE DESC
                            ) T
                            WHERE ROWNUM = 1 GROUP BY TO_CHAR(T.CREATEDDATE, 'YYYY-MM')
                            UNION
                            SELECT TO_CHAR(T.CREATEDDATE, 'YYYY-MM') CREATE_MONTH,0 AS NL_COUNT,0 AS LSG_COUNT,COUNT(T.VASCULARV_TYPE) AS BYJDG_COUNT,0 AS YJDG_COUNT FROM
                            (
                            SELECT T.CREATEDDATE,T.VASCULARV_TYPE FROM MED_PATIENTS_OPERATOR T
                            LEFT JOIN MED_COMMON_ITEMLIST ITEM ON T.VASCULARV_TYPE=ITEM.ITEM_ID
                            WHERE T.HEMODIALYSIS_ID = :HEMODIALYSIS_ID AND ITEM.ITEM_TYPE='通路分类' AND ITEM.ITEM_NAME='半永久通管' AND ITEM.STATUS = '1'
                            AND TRUNC(T.CREATEDDATE) >= TRUNC(:BEGINTIME)
                            AND TRUNC(T.CREATEDDATE) <= TRUNC(:ENDTIME)
                            ORDER BY T.CREATEDDATE DESC
                            ) T
                            WHERE ROWNUM = 1 GROUP BY TO_CHAR(T.CREATEDDATE, 'YYYY-MM')
                            UNION
                            SELECT TO_CHAR(T.CREATEDDATE, 'YYYY-MM') CREATE_MONTH,0 AS NL_COUNT,0 AS LSG_COUNT,0 AS BYJDG_COUNT,COUNT(T.VASCULARV_TYPE) AS YJDG_COUNT FROM
                            (
                            SELECT T.CREATEDDATE,T.VASCULARV_TYPE FROM MED_PATIENTS_OPERATOR T
                            LEFT JOIN MED_COMMON_ITEMLIST ITEM ON T.VASCULARV_TYPE=ITEM.ITEM_ID
                            WHERE T.HEMODIALYSIS_ID = :HEMODIALYSIS_ID AND ITEM.ITEM_TYPE='通路分类' AND ITEM.ITEM_NAME='永久性通路' AND ITEM.STATUS = '1'
                            AND TRUNC(T.CREATEDDATE) >= TRUNC(:BEGINTIME)
                            AND TRUNC(T.CREATEDDATE) <= TRUNC(:ENDTIME)
                            ORDER BY T.CREATEDDATE DESC
                            ) T
                            WHERE ROWNUM = 1 GROUP BY TO_CHAR(T.CREATEDDATE, 'YYYY-MM')
                          ) T
                          GROUP BY T.CREATE_MONTH ORDER BY T.CREATE_MONTH
                        ) T";
            }
        }

        /// <summary>
        /// 按月份根据透析编号和日期获取透析患者男女例次
        /// </summary>
        public string GetSexCountByHemoIdAndDate
        {
            get
            {
                return @"SELECT T.*,(MAN_COUNT+WOMAN_COUNT) AS SUB_COUNT FROM(
                        SELECT TO_CHAR(T.CURE_CREATE_DATE, 'YYYY-MM') AS CREATE_MONTH,COUNT(DECODE(P.SEX,'男',P.SEX)) AS MAN_COUNT,COUNT(DECODE(P.SEX,'女',P.SEX)) AS WOMAN_COUNT FROM MED_CURE_MAIN T
                        LEFT JOIN MED_PATIENTS P ON T.HEMODIALYSIS_ID=P.HEMODIALYSIS_ID
                        WHERE TRUNC(T.CURE_CREATE_DATE) >= TRUNC(:BEGINTIME) AND TRUNC(T.CURE_CREATE_DATE) <= TRUNC(:ENDTIME)
                        AND T.CURE_STATUS != '4' AND T.HEMODIALYSIS_ID=:HEMODIALYSIS_ID AND (P.IS_DELETE='0' OR P.IS_DELETE IS NULL)
                        GROUP BY TO_CHAR(T.CURE_CREATE_DATE, 'YYYY-MM') HAVING(COUNT(1) > 0) ORDER BY TO_CHAR(T.CURE_CREATE_DATE, 'YYYY-MM')) T";
            }
        }

        /// <summary>
        /// 根据透析编号和日期获取透析患者男女例数
        /// </summary>
        public string GetSexCountByHemoIdAndDate2
        {
            get
            {
                return @"SELECT T.*,(MAN_COUNT+WOMAN_COUNT) AS SUB_COUNT FROM(
                        SELECT TO_CHAR(:BEGINTIME, 'YYYY-MM-DD')||'/'||TO_CHAR(:ENDTIME, 'YYYY-MM-DD') AS CREATE_MONTH,COUNT(DECODE(P.SEX,'男',P.SEX)) AS MAN_COUNT,COUNT(DECODE(P.SEX,'女',P.SEX)) AS WOMAN_COUNT FROM (
                        SELECT DISTINCT T.HEMODIALYSIS_ID FROM MED_CURE_MAIN T
                        WHERE TRUNC(T.CURE_CREATE_DATE) >= TRUNC(:BEGINTIME) AND TRUNC(T.CURE_CREATE_DATE) <= TRUNC(:ENDTIME)
                        AND T.CURE_STATUS != '4') T
                        LEFT JOIN MED_PATIENTS P ON T.HEMODIALYSIS_ID=P.HEMODIALYSIS_ID
                        WHERE T.HEMODIALYSIS_ID=:HEMODIALYSIS_ID AND (P.IS_DELETE='0' OR P.IS_DELETE IS NULL)) T";
            }
        }

        /// <summary>
        /// 根据日期获取透析患者男女例次
        /// </summary>
        public string GetSexCountByDate
        {
            get
            {
                return @"SELECT T.*,(MAN_COUNT+WOMAN_COUNT) AS SUB_COUNT FROM(
                        SELECT TO_CHAR(T.CURE_CREATE_DATE, 'YYYY-MM') AS CREATE_MONTH,COUNT(DECODE(P.SEX,'男',P.SEX)) AS MAN_COUNT,COUNT(DECODE(P.SEX,'女',P.SEX)) AS WOMAN_COUNT FROM MED_CURE_MAIN T
                        LEFT JOIN MED_PATIENTS P ON T.HEMODIALYSIS_ID=P.HEMODIALYSIS_ID
                        WHERE TRUNC(T.CURE_CREATE_DATE) >= TRUNC(:BEGINTIME) AND TRUNC(T.CURE_CREATE_DATE) <= TRUNC(:ENDTIME)
                        AND T.CURE_STATUS != '4' AND EXISTS (SELECT HEMODIALYSIS_ID FROM
                        (
                        SELECT DISTINCT T.HEMODIALYSIS_ID FROM
                        (
                          SELECT * FROM (SELECT RANK() OVER (PARTITION BY HEMODIALYSIS_ID ORDER BY CURE_CREATE_DATE DESC) CURE_RANK,HEMODIALYSIS_ID,PURIFICATION_MODE,CURE_CREATE_DATE FROM MED_CURE_MAIN
                          WHERE TRUNC(CURE_CREATE_DATE)>=TRUNC(:BEGINTIME) AND TRUNC(CURE_CREATE_DATE)<=TRUNC(:ENDTIME) AND CURE_STATUS != '4')
                          WHERE CURE_RANK=1
                        ) T
                        WHERE EXISTS
                        (
                          SELECT T2.HEMODIALYSIS_ID FROM
                          (
                            SELECT DISTINCT T1.HEMODIALYSIS_ID,TRUNC(T1.CURE_CREATE_DATE,'IW') FROM MED_CURE_MAIN T1
                            WHERE TRUNC(T1.CURE_CREATE_DATE)>=TRUNC(SYSDATE,'IW')-7-11*7 AND TRUNC(T1.CURE_CREATE_DATE)<=TRUNC(SYSDATE,'IW')-1 AND T1.CURE_STATUS != '4'
                            GROUP BY T1.HEMODIALYSIS_ID,TRUNC(T1.CURE_CREATE_DATE,'IW') HAVING COUNT(T1.HEMODIALYSIS_ID)>=2
                          ) T2
                          WHERE T2.HEMODIALYSIS_ID=T.HEMODIALYSIS_ID GROUP BY T2.HEMODIALYSIS_ID HAVING(COUNT(T2.HEMODIALYSIS_ID)=12)
                        )
                        ) WHERE HEMODIALYSIS_ID=T.HEMODIALYSIS_ID) AND (P.IS_DELETE='0' OR P.IS_DELETE IS NULL)
                        GROUP BY TO_CHAR(T.CURE_CREATE_DATE, 'YYYY-MM') HAVING(COUNT(1) > 0) ORDER BY TO_CHAR(T.CURE_CREATE_DATE, 'YYYY-MM')) T";
            }
        }

        /// <summary>
        /// 根据日期获取透析患者男女例数
        /// </summary>
        public string GetSexCountByDate2
        {
            get
            {
                return @"SELECT T.*,(MAN_COUNT+WOMAN_COUNT) AS SUB_COUNT FROM(
                        SELECT TO_CHAR(:BEGINTIME, 'YYYY-MM')||'/'||TO_CHAR(:ENDTIME, 'YYYY-MM') AS CREATE_MONTH,COUNT(DECODE(P.SEX,'男',P.SEX)) AS MAN_COUNT,COUNT(DECODE(P.SEX,'女',P.SEX)) AS WOMAN_COUNT FROM (
                        SELECT DISTINCT T.HEMODIALYSIS_ID FROM MED_CURE_MAIN T
                        WHERE TRUNC(T.CURE_CREATE_DATE) >= TRUNC(:BEGINTIME) AND TRUNC(T.CURE_CREATE_DATE) <= TRUNC(:ENDTIME)
                        AND T.CURE_STATUS != '4') T
                        LEFT JOIN MED_PATIENTS P ON T.HEMODIALYSIS_ID=P.HEMODIALYSIS_ID
                        WHERE EXISTS (SELECT HEMODIALYSIS_ID FROM
                        (
                        SELECT DISTINCT T.HEMODIALYSIS_ID FROM
                        (
                          SELECT * FROM (SELECT RANK() OVER (PARTITION BY HEMODIALYSIS_ID ORDER BY CURE_CREATE_DATE DESC) CURE_RANK,HEMODIALYSIS_ID,PURIFICATION_MODE,CURE_CREATE_DATE FROM MED_CURE_MAIN
                          WHERE TRUNC(CURE_CREATE_DATE)>=TRUNC(:BEGINTIME) AND TRUNC(CURE_CREATE_DATE)<=TRUNC(:ENDTIME) AND CURE_STATUS != '4')
                          WHERE CURE_RANK=1
                        ) T
                        WHERE EXISTS
                        (
                          SELECT T2.HEMODIALYSIS_ID FROM
                          (
                            SELECT DISTINCT T1.HEMODIALYSIS_ID,TRUNC(T1.CURE_CREATE_DATE,'IW') FROM MED_CURE_MAIN T1
                            WHERE TRUNC(T1.CURE_CREATE_DATE)>=TRUNC(SYSDATE,'IW')-7-11*7 AND TRUNC(T1.CURE_CREATE_DATE)<=TRUNC(SYSDATE,'IW')-1 AND T1.CURE_STATUS != '4'
                            GROUP BY T1.HEMODIALYSIS_ID,TRUNC(T1.CURE_CREATE_DATE,'IW') HAVING COUNT(T1.HEMODIALYSIS_ID)>=2
                          ) T2
                          WHERE T2.HEMODIALYSIS_ID=T.HEMODIALYSIS_ID GROUP BY T2.HEMODIALYSIS_ID HAVING(COUNT(T2.HEMODIALYSIS_ID)=12)
                        )
                        ) WHERE HEMODIALYSIS_ID=T.HEMODIALYSIS_ID) AND (P.IS_DELETE='0' OR P.IS_DELETE IS NULL)) T";
            }
        }

        /// <summary>
        /// 根据透析编号和日期获取透析患者不同年龄段例数
        /// </summary>
        public string GetAgeCountByHemoIdAndDate
        {
            get
            {
                return @"SELECT T.*,(COUNT_1_20+COUNT_20_40+COUNT_40_60+COUNT_60_100) AS SUB_COUNT FROM(
                        SELECT TO_CHAR(:BEGINTIME, 'YYYY-MM-DD')||'/'||TO_CHAR(:ENDTIME, 'YYYY-MM-DD') AS CREATE_MONTH,COUNT(P1.HEMODIALYSIS_ID) AS COUNT_1_20,COUNT(P2.HEMODIALYSIS_ID) AS COUNT_20_40,COUNT(P3.HEMODIALYSIS_ID) AS COUNT_40_60,COUNT(P4.HEMODIALYSIS_ID) AS COUNT_60_100 FROM
                        (SELECT DISTINCT HEMODIALYSIS_ID FROM MED_CURE_MAIN
                        WHERE TRUNC(CURE_CREATE_DATE) >= TRUNC(:BEGINTIME) AND TRUNC(CURE_CREATE_DATE) <= TRUNC(:ENDTIME)
                        AND HEMODIALYSIS_ID=:HEMODIALYSIS_ID AND CURE_STATUS != '4') T
                        LEFT JOIN (SELECT HEMODIALYSIS_ID FROM MED_PATIENTS WHERE TRUNC(MONTHS_BETWEEN(TRUNC(SYSDATE),TRUNC(BIRTHDAY))/12)<20 AND (IS_DELETE='0' OR IS_DELETE IS NULL)) P1 ON T.HEMODIALYSIS_ID=P1.HEMODIALYSIS_ID
                        LEFT JOIN (SELECT HEMODIALYSIS_ID FROM MED_PATIENTS WHERE TRUNC(MONTHS_BETWEEN(TRUNC(SYSDATE),TRUNC(BIRTHDAY))/12)>=20 AND TRUNC(MONTHS_BETWEEN(TRUNC(SYSDATE),TRUNC(BIRTHDAY))/12)<=40 AND (IS_DELETE='0' OR IS_DELETE IS NULL)) P2 ON T.HEMODIALYSIS_ID=P2.HEMODIALYSIS_ID
                        LEFT JOIN (SELECT HEMODIALYSIS_ID FROM MED_PATIENTS WHERE TRUNC(MONTHS_BETWEEN(TRUNC(SYSDATE),TRUNC(BIRTHDAY))/12)>40 AND TRUNC(MONTHS_BETWEEN(TRUNC(SYSDATE),TRUNC(BIRTHDAY))/12)<=60 AND (IS_DELETE='0' OR IS_DELETE IS NULL)) P3 ON T.HEMODIALYSIS_ID=P3.HEMODIALYSIS_ID
                        LEFT JOIN (SELECT HEMODIALYSIS_ID FROM MED_PATIENTS WHERE TRUNC(MONTHS_BETWEEN(TRUNC(SYSDATE),TRUNC(BIRTHDAY))/12)>60 AND (IS_DELETE='0' OR IS_DELETE IS NULL)) P4 ON T.HEMODIALYSIS_ID=P4.HEMODIALYSIS_ID) T";
            }
        }

        /// <summary>
        /// 根据透析编号和日期获取透析患者传染病例数
        /// </summary>
        public string GetInfectousCountByHemoIdAndDate
        {
            get
            {
                return @"SELECT T.*,(YXGY_COUNT+BXGY_COUNT+AZB_COUNT+MD_COUNT+QY_COUNT+DC_COUNT+NORMAL_COUNT) AS SUB_COUNT FROM(
                        SELECT TO_CHAR(:BEGINTIME, 'YYYY-MM-DD')||'/'||TO_CHAR(:ENDTIME, 'YYYY-MM-DD') AS CREATE_MONTH,COUNT(DECODE(INSTR(T.INFECTIOUS_CHECK_RESULT,'乙型肝炎'),0,DECODE(INSTR(T.INFECTIOUS_CHECK_RESULT,'乙肝'),0,NULL,T.INFECTIOUS_CHECK_RESULT),T.INFECTIOUS_CHECK_RESULT)) AS YXGY_COUNT,
                        COUNT(DECODE(INSTR(T.INFECTIOUS_CHECK_RESULT,'丙型肝炎'),0,DECODE(INSTR(T.INFECTIOUS_CHECK_RESULT,'丙肝'),0,NULL,T.INFECTIOUS_CHECK_RESULT),T.INFECTIOUS_CHECK_RESULT)) AS BXGY_COUNT,
                        COUNT(DECODE(INSTR(T.INFECTIOUS_CHECK_RESULT,'艾滋病'),0,DECODE(INSTR(T.INFECTIOUS_CHECK_RESULT,'艾滋'),0,NULL,T.INFECTIOUS_CHECK_RESULT),T.INFECTIOUS_CHECK_RESULT)) AS AZB_COUNT,
                        COUNT(DECODE(INSTR(T.INFECTIOUS_CHECK_RESULT,'梅毒'),0,NULL,T.INFECTIOUS_CHECK_RESULT)) AS MD_COUNT,
                        COUNT(DECODE(INSTR(T.INFECTIOUS_CHECK_RESULT,'全阴'),0,NULL,T.INFECTIOUS_CHECK_RESULT)) AS QY_COUNT,
                        COUNT(DECODE(INSTR(T.INFECTIOUS_CHECK_RESULT,'待查'),0,DECODE(INSTR(T.INFECTIOUS_CHECK_RESULT,'待报'),0,NULL,T.INFECTIOUS_CHECK_RESULT),T.INFECTIOUS_CHECK_RESULT)) AS DC_COUNT,
                        COUNT(DECODE(T.INFECTIOUS_CHECK_RESULT,NULL,1,NULL)) AS NORMAL_COUNT FROM(
                        SELECT T1.CURE_CREATE_DATE,T1.INFECTIOUS_CHECK_RESULT FROM MED_CURE_MAIN T1,(SELECT CURE_CREATE_DATE AS BEGIN_DATE,CURE_CREATE_DATE+6 AS END_DATE FROM(
                        SELECT CURE_CREATE_DATE FROM MED_CURE_MAIN WHERE TRUNC(CURE_CREATE_DATE) >= TRUNC(:BEGINTIME) AND TRUNC(CURE_CREATE_DATE) <= TRUNC(:ENDTIME)
                        AND HEMODIALYSIS_ID=:HEMODIALYSIS_ID AND CURE_STATUS != '4' ORDER BY CURE_CREATE_DATE) WHERE ROWNUM=1) T2
                        WHERE TRUNC(T1.CURE_CREATE_DATE) >= TRUNC(T2.BEGIN_DATE) AND TRUNC(T1.CURE_CREATE_DATE) <= TRUNC(T2.END_DATE)
                        AND HEMODIALYSIS_ID=:HEMODIALYSIS_ID AND CURE_STATUS != '4' ORDER BY T1.CURE_CREATE_DATE DESC) T WHERE ROWNUM=1) T";
            }
        }

        /// <summary>
        /// 根据透析编号和日期获取规律透析患者例数
        /// </summary>
        public string GetRegularCountByHemoIdAndDate
        {
            get
            {
                return @"SELECT TO_CHAR(:BEGINTIME, 'YYYY-MM-DD')||'/'||TO_CHAR(:ENDTIME, 'YYYY-MM-DD') AS CURE_MONTH,TWO_TIME,THREE_TIME,FOUR_TIME,FIVE_TIME,0 AS UNREGULAR,(TWO_TIME+THREE_TIME+FOUR_TIME+FIVE_TIME) AS SUB_COUNT FROM
                        (SELECT DECODE(COUNT(HEMODIALYSIS_ID),0,0,1) AS TWO_TIME FROM(SELECT HEMODIALYSIS_ID FROM
                        (SELECT DISTINCT TRUNC(CURE_CREATE_DATE,'IW'),HEMODIALYSIS_ID FROM MED_CURE_MAIN
                        WHERE TRUNC(CURE_CREATE_DATE) >= TRUNC(SYSDATE,'IW')-7-11*7 AND TRUNC(CURE_CREATE_DATE) <= TRUNC(SYSDATE,'IW')-1
                        AND HEMODIALYSIS_ID=:HEMODIALYSIS_ID AND CURE_STATUS != '4' GROUP BY HEMODIALYSIS_ID,TRUNC(CURE_CREATE_DATE,'IW') HAVING COUNT(HEMODIALYSIS_ID)=2) GROUP BY HEMODIALYSIS_ID HAVING COUNT(HEMODIALYSIS_ID)=12)),
                        (SELECT DECODE(COUNT(HEMODIALYSIS_ID),0,0,1) AS THREE_TIME FROM(SELECT HEMODIALYSIS_ID FROM
                        (SELECT DISTINCT TRUNC(CURE_CREATE_DATE,'IW'),HEMODIALYSIS_ID FROM MED_CURE_MAIN
                        WHERE TRUNC(CURE_CREATE_DATE) >= TRUNC(SYSDATE,'IW')-7-11*7 AND TRUNC(CURE_CREATE_DATE) <= TRUNC(SYSDATE,'IW')-1
                        AND HEMODIALYSIS_ID=:HEMODIALYSIS_ID AND CURE_STATUS != '4' GROUP BY HEMODIALYSIS_ID,TRUNC(CURE_CREATE_DATE,'IW') HAVING COUNT(HEMODIALYSIS_ID)=3) GROUP BY HEMODIALYSIS_ID HAVING COUNT(HEMODIALYSIS_ID)=12)),
                        (SELECT DECODE(COUNT(HEMODIALYSIS_ID),0,0,1) AS FOUR_TIME FROM(SELECT HEMODIALYSIS_ID FROM
                        (SELECT DISTINCT TRUNC(CURE_CREATE_DATE,'IW'),HEMODIALYSIS_ID FROM MED_CURE_MAIN
                        WHERE TRUNC(CURE_CREATE_DATE) >= TRUNC(SYSDATE,'IW')-7-11*7 AND TRUNC(CURE_CREATE_DATE) <= TRUNC(SYSDATE,'IW')-1
                        AND HEMODIALYSIS_ID=:HEMODIALYSIS_ID AND CURE_STATUS != '4' GROUP BY HEMODIALYSIS_ID,TRUNC(CURE_CREATE_DATE,'IW') HAVING COUNT(HEMODIALYSIS_ID)=4) GROUP BY HEMODIALYSIS_ID HAVING COUNT(HEMODIALYSIS_ID)=12)),
                        (SELECT DECODE(COUNT(HEMODIALYSIS_ID),0,0,1) AS FIVE_TIME FROM(SELECT HEMODIALYSIS_ID FROM
                        (SELECT DISTINCT TRUNC(CURE_CREATE_DATE,'IW'),HEMODIALYSIS_ID FROM MED_CURE_MAIN
                        WHERE TRUNC(CURE_CREATE_DATE) >= TRUNC(SYSDATE,'IW')-7-11*7 AND TRUNC(CURE_CREATE_DATE) <= TRUNC(SYSDATE,'IW')-1
                        AND HEMODIALYSIS_ID=:HEMODIALYSIS_ID AND CURE_STATUS != '4' GROUP BY HEMODIALYSIS_ID,TRUNC(CURE_CREATE_DATE,'IW') HAVING COUNT(HEMODIALYSIS_ID)=5) GROUP BY HEMODIALYSIS_ID HAVING COUNT(HEMODIALYSIS_ID)=12))";
            }
        }

        #endregion

        public string GET_MED_UPTYPE_MGR
        {
            get
            {
                return " SELECT * FROM MED_UPTYPE_MGR WHERE UPTYPE=:UPTYPE ";
            }

        }


        #region 检验项目质控
        /// <summary>
        /// 得到病人 乙肝、丙肝、梅毒、HIV检查结果列表
        /// </summary>
        public string GetMedInfectiousCheckList
        {
            get
            {
                return @"SELECT P.NAME,
                               P.SPECIFIC_TIME,
                               U.NAME AS CHECK_USER,
                               T.INFECTIOUS_ID,
                               T.HEMODIALYSIS_ID,
                               T.HEPATITIS_B,
                               T.HEPATITIS_C,
                               T.SYPHILIS,
                               T.HIV,
                               T.CHECK_USER_ID,
                               T.STATUS,
                               T.CREATE_DATE,
                               TO_CHAR(T.HEPATITIS_B_DATE, 'YYYY-MM-DD') HEPATITIS_B_DATE,
                               TO_CHAR(T.HEPATITIS_C_DATE, 'YYYY-MM-DD') HEPATITIS_C_DATE,
                               TO_CHAR(T.SYPHILIS_DATE, 'YYYY-MM-DD') SYPHILIS_DATE,
                               TO_CHAR(T.HIV_DATE, 'YYYY-MM-DD') HIV_DATE,
                               T.PATIENT_ID,
                               P.HEMODIALYSIS_ID AS PHEMODIALYSIS_ID,
                               M_B.ITEM_NAME AS HEPATITIS_B_NAME,
                               M_C.ITEM_NAME AS HEPATITIS_C_NAME,
                               M_S.ITEM_NAME AS SYPHILIS_NAME,
                               M_H.ITEM_NAME AS HIV_NAME
                          FROM MED_PATIENTS P
                          LEFT JOIN MED_INFECTIOUS_CHECK T
                            ON T.HEMODIALYSIS_ID = P.HEMODIALYSIS_ID
                          LEFT JOIN MED_STAFF_DICT U
                            ON T.CHECK_USER_ID = U.USER_NAME
                          LEFT JOIN MED_COMMON_ITEMLIST M_B
                            ON T.HEPATITIS_B = M_B.ITEM_ID
                          LEFT JOIN MED_COMMON_ITEMLIST M_C
                            ON T.HEPATITIS_C = M_C.ITEM_ID
                          LEFT JOIN MED_COMMON_ITEMLIST M_S
                            ON T.SYPHILIS = M_S.ITEM_ID
                          LEFT JOIN MED_COMMON_ITEMLIST M_H
                            ON T.HIV = M_H.ITEM_ID
                         WHERE T.HEMODIALYSIS_ID = :HEMODIALYSIS_ID
                           AND T.STATUS = '0'";
            }
        }

        public string GetMedInfectiousInfoByID
        {
            get
            {
                return @"select * from MED_INFECTIOUS_CHECK where INFECTIOUS_ID = :INFECTIOUS_ID";
            }
        }

        /// <summary>
        /// 质控数据-患者检验信息
        /// </summary>
        public string GetHoldLabItemDt
        {
            //            get {
            //                return @"SELECT DISTINCT K.PATIENT_ID
            //                          FROM (SELECT ROWNUM, T.PATIENT_ID, T2.RESULT
            //                                  FROM MED_LAB_TEST_MASTER T
            //                                  LEFT JOIN MED_LAB_TEST_ITEMS T1
            //                                    ON T.TEST_NO = T1.TEST_NO
            //                                  LEFT JOIN MED_LAB_RESULT T2
            //                                    ON T1.TEST_NO = T2.TEST_NO
            //                                 WHERE T.BARCODE IN ({0})
            //                                   AND TRUNC(T.SPCM_RECEIVED_DATE_TIME) >= TRUNC(TO_DATE('{1}','YYYY-MM-DD hh24:mi:ss'))
            //                                   AND TRUNC(T.SPCM_RECEIVED_DATE_TIME) <= TRUNC(TO_DATE('{2}','YYYY-MM-DD  hh24:mi:ss'))
            //                                   AND T2.REPORT_ITEM_NAME LIKE '%' || '{3}' || '%'
            //                                   AND ISNUMERIC(T2.RESULT) = 1) K
            //                         WHERE 1 = 1";
            //            }

            get
            {
                return @"SELECT DISTINCT K.PATIENT_ID
                          FROM (SELECT ROWNUM, T.PATIENT_ID, T2.RESULT,RANK() OVER(PARTITION BY T.PATIENT_ID,T2.REPORT_ITEM_NAME ORDER BY T.RESULTS_RPT_DATE_TIME DESC) RK
                                  FROM MED_PATIENTS PAT INNER JOIN
MED_LAB_TEST_MASTER T ON  PAT.PATIENT_ID = T.PATIENT_ID
                                  INNER JOIN MED_LAB_TEST_ITEMS T1
                                    ON T.TEST_NO = T1.TEST_NO
                                  INNER JOIN MED_LAB_RESULT T2
                                    ON T1.TEST_NO = T2.TEST_NO
                                 WHERE PAT.HEMODIALYSIS_ID IN ({0})
                                   AND TRUNC(T.RESULTS_RPT_DATE_TIME) >= TRUNC(TO_DATE('{1}','YYYY-MM-DD hh24:mi:ss'))
                                   AND TRUNC(T.RESULTS_RPT_DATE_TIME) <= TRUNC(TO_DATE('{2}','YYYY-MM-DD  hh24:mi:ss'))
                                   AND {3}
                                   AND ISNUMERIC(T2.RESULT) = 1) K
                         WHERE 1 = 1 AND RK = 1 ";
            }
        }

        /// <summary>
        /// 质控数据-患者检验信息-铁饱和度
        /// </summary>
        public string GetHoldLabItemDtTie
        {
            get
            {
                return @"SELECT*FROM(SELECT T.PATIENT_ID,
                                   T2.RESULT,
                                   T.TEST_NO,
                                   T1.ITEM_NAME,
                                   T2.REPORT_ITEM_NAME,RANK() OVER(PARTITION BY T.PATIENT_ID,T2.REPORT_ITEM_NAME ORDER BY T.RESULTS_RPT_DATE_TIME DESC) RK
                              FROM 
MED_PATIENTS PAT INNER JOIN
MED_LAB_TEST_MASTER T ON  PAT.PATIENT_ID = T.PATIENT_ID
                              INNER JOIN MED_LAB_TEST_ITEMS T1 ON T.TEST_NO = T1.TEST_NO
                              INNER JOIN MED_LAB_RESULT T2 ON T1.TEST_NO = T2.TEST_NO
                             WHERE  PAT.HEMODIALYSIS_ID IN ({0})
                               AND TRUNC(T.RESULTS_RPT_DATE_TIME) >=
                                   TRUNC(TO_DATE('{1}', 'YYYY-MM-DD hh24:mi:ss'))
                               AND TRUNC(T.RESULTS_RPT_DATE_TIME) <=
                                   TRUNC(TO_DATE('{2}', 'YYYY-MM-DD  hh24:mi:ss'))
                               AND {3} AND ISNUMERIC(T2.RESULT) = 1) WHERE RK =1 ";
            }
        }


        #endregion

        #region 药品耗材库存管理
        /// <summary>
        /// 得到药品耗材入库列表
        /// </summary>
        public string GetMedMaterialInputList
        {
            get
            {
                return @"select t.*, d.drug_code,m.item_name as drug_type_name,mm.item_name as unit_name,
                p.name,d.name as operator_name,dr.drug_name,dr.made_date as d_made_date,dr.useless_date as d_useless_date from MED_MATERIAL_INPUT t left join 
                med_drug_master d on t.code = TRIM(d.drug_code)
                left join med_common_itemlist m on d.drug_type = m.item_id
                left join med_common_itemlist mm on d.units = mm.item_id
                left join med_patients p on t.hemo_id = p.HEMODIALYSIS_ID
                left join med_staff_dict d on d.emp_no =  t.operator_id
                left join med_drug_master dr on t.code = TRIM(dr.drug_code)
                where t.STATUS ='1'
                order by t.INPUT_DATE desc
                ";
            }
        }

        public string DeleteMaterialInfo
        {
            get
            {
                return @"DELETE FROM MED_MATERIAL_MASTER WHERE MATERIAL_ID=:MaterialID";
            }
        }
        public string GetMedMaterialInputDetail
        {
            get
            {
                return @"SELECT T.ID,
                                   T.CODE,
                                   T.FIRM_ID,
                                   T.MADE_DATE,
                                   T.USELESS_DATE,
                                   T.INPUT_DATE,
                                   T.BAR_CODE,
                                   T.F_COUNT,
                                   T.PRICE,
                                   T.HEMO_ID,
                                   T.OPERATOR_ID,
                                   T.CREATE_DATE,
                                   T.REMARK,
                                   T.STATUS,
                                   T.INVOICE_NUMBER,
                                   T.BATCH_NUMBER,
                                   T.MODETYPE,
                                   (SELECT R.MATERIAL_NAME
                                      FROM MED_MATERIAL_MASTER R
                                     WHERE R.MATERIAL_ID = T.CODE) MATERIAL_NAME,
                                   (SELECT R.MATERIAL_SPEC
                                      FROM MED_MATERIAL_MASTER R
                                     WHERE R.MATERIAL_ID = T.CODE) SPACE,
                                   T.UNITS,
                                   T.APPLYID,
                                   T.STOREMANAGER,
                               (SELECT L.ITEM_NAME FROM MED_COMMON_ITEMLIST L WHERE L.ITEM_ID = T.MODETYPE) AS MATERIALTYPENAME,
                                (SELECT L.ITEM_NAME FROM MED_COMMON_ITEMLIST L WHERE L.ITEM_ID = T.UNITS) AS UNTISNAME,
                                (SELECT C.User_Name FROM MED_USERS C WHERE C.User_Id = T.OPERATOR_ID) AS OPERATOR_NAME,
                                (SELECT C.NAME FROM MED_STAFF_DICT C WHERE C.EMP_NO = T.APPLYID) AS APPLYNAME,
                                 (SELECT C.NAME FROM MED_STAFF_DICT C WHERE C.EMP_NO = T.STOREMANAGER) AS STOREMANAGERNAME
                          FROM MED_MATERIAL_INPUT T
                         WHERE T.STATUS = '1' 
                           AND TO_CHAR(T.INPUT_DATE,  'YYYY-MM-DD') >= TO_CHAR(:DTSTAR, 'YYYY-MM-DD')
                           AND TO_CHAR(T.INPUT_DATE, 'YYYY-MM-DD') <= TO_CHAR(:DTEND, 'YYYY-MM-DD')
                         ORDER BY T.INPUT_DATE DESC";
            }
        }

        public string GetMedMaterialInputDetailByCodeAndBatchNum
        {
            get
            {
                return @"SELECT T.ID,
                                   T.CODE,
                                   T.FIRM_ID,
                                   T.MADE_DATE,
                                   T.USELESS_DATE,
                                   T.INPUT_DATE,
                                   T.BAR_CODE,
                                   T.F_COUNT,
                                   T.PRICE,
                                   T.HEMO_ID,
                                   T.OPERATOR_ID,
                                   T.CREATE_DATE,
                                   T.REMARK,
                                   T.STATUS,
                                   T.INVOICE_NUMBER,
                                   T.BATCH_NUMBER,
                                   T.MODETYPE,
                                   (SELECT R.MATERIAL_NAME
                                      FROM MED_MATERIAL_MASTER R
                                     WHERE R.MATERIAL_ID = T.CODE) MATERIAL_NAME,
                                   (SELECT R.MATERIAL_SPEC
                                      FROM MED_MATERIAL_MASTER R
                                     WHERE R.MATERIAL_ID = T.CODE) SPACE,
                                   T.UNITS,
                                   T.APPLYID,
                                   T.STOREMANAGER,
                               (SELECT L.ITEM_NAME FROM MED_COMMON_ITEMLIST L WHERE L.ITEM_ID = T.MODETYPE) AS MATERIALTYPENAME,
                                (SELECT L.ITEM_NAME FROM MED_COMMON_ITEMLIST L WHERE L.ITEM_ID = T.UNITS) AS UNTISNAME,
                                (SELECT C.User_Name FROM MED_USERS C WHERE C.User_Id = T.OPERATOR_ID) AS OPERATOR_NAME,
                                (SELECT C.NAME FROM MED_STAFF_DICT C WHERE C.EMP_NO = T.APPLYID) AS APPLYNAME,
                                 (SELECT C.NAME FROM MED_STAFF_DICT C WHERE C.EMP_NO = T.STOREMANAGER) AS STOREMANAGERNAME
                          FROM MED_MATERIAL_INPUT T
                         WHERE T.STATUS = '1' 
                           AND TO_CHAR(T.INPUT_DATE,'YYYY-MM-DD') <= TO_CHAR(:INPUT_DATE,'YYYY-MM-DD')                      
                           AND CODE = :CODE AND BATCH_NUMBER = :BATCH_NUM
                         ORDER BY T.INPUT_DATE DESC";
            }
        }


        public string GetMedMaterialInputMaster
        {
            get
            {
                return @"SELECT sys_guid() || 'a' AS ID,
                               T.MODETYPE,
                               T.CODE,
                               (SELECT R.MATERIAL_NAME
                                  FROM MED_MATERIAL_MASTER R
                                 WHERE R.MATERIAL_ID = T.CODE) MATERIAL_NAME,
                               (SELECT R.MATERIAL_SPEC
                                  FROM MED_MATERIAL_MASTER R
                                 WHERE R.MATERIAL_ID = T.CODE) SPACE,
                               T.UNITS,
                               SUM(T.PRICE) PRICE,
                               SUM(T.F_COUNT) F_COUNT,
                               (SELECT L.ITEM_NAME
                                  FROM MED_COMMON_ITEMLIST L
                                 WHERE L.ITEM_ID = T.MODETYPE) AS MATERIALTYPENAME,
                               (SELECT L.ITEM_NAME
                                  FROM MED_COMMON_ITEMLIST L
                                 WHERE L.ITEM_ID = T.UNITS) AS UNTISNAME
                          FROM MED_MATERIAL_INPUT T
                         WHERE T.STATUS = '1'
                          AND TO_CHAR(T.INPUT_DATE,  'YYYY-MM-DD') >= TO_CHAR(:DTSTAR, 'YYYY-MM-DD')
                           AND TO_CHAR(T.INPUT_DATE, 'YYYY-MM-DD') <= TO_CHAR(:DTEND, 'YYYY-MM-DD')
                         GROUP BY T.MODETYPE,
                                  T.CODE,
                                  T.MATERIAL_NAME,
                                  T.UNITS";

            }
        }
        public string GetMedMaterialOutputDetail
        {
            get
            {
                return @"SELECT T.ID,
                                   T.CODE,
                                   T.OPERATOR_ID,
                                   T.OUPUT_DATE,
                                   T.PRICE,
                                   T.ISOUT,
                                   T.F_COUNT,
                                   T.HEMO_ID,
                                   T.REMARK,
                                   T.FIRM_ID,
                                   T.STATUS,
                                   (SELECT R.MATERIAL_NAME
                                      FROM MED_MATERIAL_MASTER R
                                     WHERE R.MATERIAL_ID = T.CODE) MATERIAL_NAME,
                                   T.APPLYID,
                                   (SELECT R.MATERIAL_SPEC
                                      FROM MED_MATERIAL_MASTER R
                                     WHERE R.MATERIAL_ID = T.CODE) SPACE,
                                   T.UNITS,
                                   T.MODETYPE,
                                   T.BATCH_NUMBER,
                                   T.STOREMANAGER,
                               (SELECT L.ITEM_NAME
                                  FROM MED_COMMON_ITEMLIST L
                                 WHERE L.ITEM_ID = T.MODETYPE) AS MATERIALTYPENAME,
                               T.UNITS AS UNTISNAME,
                               (SELECT C.USER_NAME FROM MED_USERS C WHERE C.USER_ID = T.OPERATOR_ID) AS OPERATOR_NAME,
                               (SELECT C.NAME FROM MED_STAFF_DICT C WHERE C.EMP_NO = T.APPLYID) AS APPLYNAME,
                               (SELECT T1.USELESS_DATE
                                  FROM MED_MATERIAL_INPUT T1
                                 WHERE T1.CODE = T.CODE
                                   AND T1.BATCH_NUMBER = T.BATCH_NUMBER and rownum=1) USELESS_DATE,
                               (SELECT C.NAME FROM MED_STAFF_DICT C WHERE C.EMP_NO = T.STOREMANAGER) AS STOREMANAGERNAME
                          FROM MED_MATERIAL_OUTPUT T
                         WHERE T.STATUS = '1'
                             AND TO_CHAR(T.OUPUT_DATE, 'YYYY-MM-DD') >= TO_CHAR(:DTSTAR, 'YYYY-MM-DD')
                            AND TO_CHAR(T.OUPUT_DATE, 'YYYY-MM-DD') <= TO_CHAR(:DTEND, 'YYYY-MM-DD')
                         ORDER BY T.OUPUT_DATE DESC";
            }
        }

        public string GetMedMaterialOutputDetailByCodeAndBatchNum
        {
            get
            {
                return @"SELECT T.ID,
                                   T.CODE,
                                   T.OPERATOR_ID,
                                   T.OUPUT_DATE,
                                   T.PRICE,
                                   T.ISOUT,
                                   T.F_COUNT,
                                   T.HEMO_ID,
                                   T.REMARK,
                                   T.FIRM_ID,
                                   T.STATUS,
                                   (SELECT R.MATERIAL_NAME
                                      FROM MED_MATERIAL_MASTER R
                                     WHERE R.MATERIAL_ID = T.CODE) MATERIAL_NAME,
                                   T.APPLYID,
                                   (SELECT R.MATERIAL_SPEC
                                      FROM MED_MATERIAL_MASTER R
                                     WHERE R.MATERIAL_ID = T.CODE) SPACE,
                                   T.UNITS,
                                   T.MODETYPE,
                                   T.BATCH_NUMBER,
                                   T.STOREMANAGER,
                               (SELECT L.ITEM_NAME
                                  FROM MED_COMMON_ITEMLIST L
                                 WHERE L.ITEM_ID = T.MODETYPE) AS MATERIALTYPENAME,
                               T.UNITS AS UNTISNAME,
                               (SELECT C.USER_NAME FROM MED_USERS C WHERE C.USER_ID = T.OPERATOR_ID) AS OPERATOR_NAME,
                               (SELECT C.NAME FROM MED_STAFF_DICT C WHERE C.EMP_NO = T.APPLYID) AS APPLYNAME,
                               (SELECT T1.USELESS_DATE
                                  FROM MED_MATERIAL_INPUT T1
                                 WHERE T1.CODE = T.CODE
                                   AND T1.BATCH_NUMBER = T.BATCH_NUMBER and rownum=1) USELESS_DATE,
                               (SELECT C.NAME FROM MED_STAFF_DICT C WHERE C.EMP_NO = T.STOREMANAGER) AS STOREMANAGERNAME
                          FROM MED_MATERIAL_OUTPUT T
                         WHERE T.STATUS = '1'
                            AND TO_CHAR(T.OUPUT_DATE, 'YYYY-MM-DD') <= TO_CHAR(:OUPUT_DATE, 'YYYY-MM-DD')
                           AND CODE = :CODE AND BATCH_NUMBER = :BATCH_NUM
                         ORDER BY T.OUPUT_DATE DESC";
            }
        }

        public string GetMedMaterialOutputMaster
        {
            get
            {
                return @"SELECT sys_guid() || 'a'  AS ID,
                               T.MODETYPE,
                               T.CODE,
                               (SELECT R.MATERIAL_NAME
                                  FROM MED_MATERIAL_MASTER R
                                 WHERE R.MATERIAL_ID = T.CODE) MATERIAL_NAME,
                               (SELECT R.MATERIAL_SPEC
                                  FROM MED_MATERIAL_MASTER R
                                 WHERE R.MATERIAL_ID = T.CODE) SPACE,
                               T.UNITS,
                               SUM(T.F_COUNT) F_COUNT,
                               SUM(T.PRICE) PRICE,
                               (SELECT L.ITEM_NAME
                                  FROM MED_COMMON_ITEMLIST L
                                 WHERE L.ITEM_ID = T.MODETYPE) AS MATERIALTYPENAME,
                               T.UNITS AS UNTISNAME
                          FROM MED_MATERIAL_OUTPUT T
                         WHERE T.STATUS = '1'
                             AND TO_CHAR(T.OUPUT_DATE, 'YYYY-MM-DD') >= TO_CHAR(:DTSTAR, 'YYYY-MM-DD')
                            AND TO_CHAR(T.OUPUT_DATE, 'YYYY-MM-DD') <= TO_CHAR(:DTEND, 'YYYY-MM-DD')
                         GROUP BY T.MODETYPE,
                                  T.CODE,
                                  T.UNITS";
            }
        }


        /// <summary>
        /// 根据ID得到一条入库耗材信息
        /// </summary>
        public string GetMedMaterialInputByID
        {
            get
            {
                return @"select * from MED_MATERIAL_INPUT where ID = :ID AND STATUS ='1'";
            }
        }

        /// <summary>
        /// 根据透析号和药品编号得到入库的价格和数量
        /// </summary>
        public string GetMedMaterialInputByHemoIdAndCode
        {
            get
            {
                return @"select f_count,price from MED_MATERIAL_INPUT where hemo_id=:HEMO_ID and code = trim(:CODE) AND STATUS ='1'";
            }
        }

        /// <summary>
        /// 根据透析号和药品编号得到出库的价格和数量
        /// </summary>
        public string GetMedMaterialOutputByHemoIdAndCode
        {
            get
            {
                return @"select f_count,price from MED_MATERIAL_OUTPUT where hemo_id=:HEMO_ID and code = trim(:CODE) AND STATUS ='1'";
            }
        }

        /// <summary>
        /// 根据透析号和药品编号得到实际库存数量
        /// </summary>
        public string GetMedMaterialCheckByHemoIdAndCode
        {
            get
            {
                return @"select * from MED_MATERIAL_CHECK where hemo_id=:HEMO_ID and code = trim(:CODE)";
            }
        }

        /// <summary>
        /// 根据ID得到一条出库耗材信息
        /// </summary>
        public string GetMedMaterialOutputByID
        {
            get
            {
                return @"select * from MED_MATERIAL_OUTPUT where ID = :ID AND STATUS ='1'";
            }
        }

        /// <summary>
        /// 得到药品耗材出库列表
        /// </summary>
        public string GetMedMaterialOutputList
        {
            get
            {
                return @"select t.*, d.drug_code,m.item_name as drug_type_name,mm.item_name as unit_name,
                p.name,d.name as  operator_name,dr.drug_name,dr.made_date as d_made_date,dr.useless_date as d_useless_date from MED_MATERIAL_OUTPUT t left join 
                med_drug_master d on t.code = TRIM(d.drug_code)
                left join med_common_itemlist m on d.drug_type = m.item_id
                left join med_common_itemlist mm on d.units = mm.item_id
                left join med_patients p on t.hemo_id = p.HEMODIALYSIS_ID
                left join med_staff_dict d on d.emp_no =  t.operator_id
                left join med_drug_master dr on t.code = TRIM(dr.drug_code)
                where t.STATUS='1'
                order by t.OUPUT_DATE desc
                ";
            }
        }

        /// <summary>
        /// 得到药品耗材盘点列表
        /// </summary>
        public string GetMedMaterialCheckList
        {
            get
            {
                return @"SELECT T.*,
                                   T1.MATERIAL_SPEC,
                                   T1.VALID_DATE,
                                   T1.MEMO,
                                   (SELECT S.USER_NAME FROM MED_USERS S WHERE S.USER_ID = T.CHECKER) CHECKERNAME,
                                   (SELECT S.ITEM_NAME
                                      FROM MED_COMMON_ITEMLIST S
                                     WHERE S.ITEM_TYPE = '辅材类型'
                                       AND S.ITEM_ID = T.MODETYPE) MODETYPENAME,
                                   (SELECT S.ITEM_NAME
                                      FROM MED_COMMON_ITEMLIST S
                                     WHERE S.ITEM_TYPE  IN( '药品单位','耗材单位')
                                       AND S.ITEM_ID = T.UNITS) UNITSNAME,
                                     (SELECT C.NAME FROM MED_STAFF_DICT C WHERE C.EMP_NO = T.STOREMANAGER) AS STOREMANAGERNAME
                              FROM MED_MATERIAL_CHECK T, MED_MATERIAL_MASTER T1
                             WHERE T.CODE = T1.MATERIAL_ID  AND T.CHECK_DATE = (SELECT MAX(CHECK_DATE) FROM MED_MATERIAL_CHECK MC
                             WHERE TO_CHAR(MC.CHECK_DATE,'YYYY-MM-DD') = TO_CHAR(:CHECK_DATE,'YYYY-MM-DD') {0})
                                AND TO_CHAR(T.CHECK_DATE,'YYYY-MM-DD') = TO_CHAR(:CHECK_DATE,'YYYY-MM-DD')
                                   ";
                //                return @"select t.*, d.drug_code,m.item_name as drug_type_name,mm.item_name as unit_name,
                //                p.name,dr.drug_name,dr.made_date as d_made_date,dr.useless_date as d_useless_date from MED_MATERIAL_CHECK t left join 
                //                med_drug_master d on t.code = TRIM(d.drug_code)
                //                left join med_common_itemlist m on d.drug_type = m.item_id
                //                left join med_common_itemlist mm on d.units = mm.item_id
                //                left join med_patients p on t.hemo_id = p.HEMODIALYSIS_ID
                //                left join med_drug_master dr on t.code = TRIM(dr.drug_code)
                //                order by t.CHECK_DATE desc
                //                ";
            }
        }
        public string GetMedMaterialCheckHisList
        {
            get
            {
                return @"SELECT SYS_GUID() || '1' AS ID,
                               TO_CHAR(T.CHECK_DATE, 'YYYY-MM-DD') MATERIAL_NAME,
                               T.CHECKER,0 AS F_COUNT,
                               (SELECT S.USER_NAME FROM MED_USERS S WHERE S.USER_ID = T.CHECKER) CHECKERNAME
                          FROM MED_MATERIAL_CHECK T
                         GROUP BY TO_CHAR(T.CHECK_DATE, 'YYYY-MM-DD'), T.CHECKER
                         ORDER BY TO_CHAR(T.CHECK_DATE, 'YYYY-MM-DD') DESC";
            }
        }

        public string GetMedMaterialListByTypeId
        {
            get
            {
                return @"SELECT *
                              FROM (SELECT SYS_GUID()|| '1' ID,
                                           T.CODE,
                                           T.MATERIAL_NAME,
                                           T.MODETYPE,
                                           T.UNITS,(SELECT L.ITEM_NAME FROM MED_COMMON_ITEMLIST L WHERE L.ITEM_ID = T.UNITS) UNTISNAME,
                                           T.BATCH_NUMBER,
                                           (SELECT P.MATERIAL_SPEC
                                              FROM MED_MATERIAL_MASTER P
                                             WHERE P.MATERIAL_ID = T.CODE) SPACE,
                                           (SELECT R.MATERIAL_PINYIN
                                              FROM MED_MATERIAL_MASTER R
                                             WHERE R.MATERIAL_ID = T.CODE) MATERIAL_PINYIN,
                                           (SUM(T.F_COUNT) -
                                           (SELECT NVL(SUM(T1.F_COUNT), 0)
                                               FROM MED_MATERIAL_OUTPUT T1
                                              WHERE T1.CODE = T.CODE AND T1.UNITS = T.UNITS
                                                AND T1.BATCH_NUMBER = T.BATCH_NUMBER)) AS F_COUNT
                                      FROM MED_MATERIAL_INPUT T
                                    WHERE T.MODETYPE LIKE '%' || ':MODETYPE' || '%' OR 'ALL' LIKE ':MODETYPE'
                                     GROUP BY T.CODE,T.UNITS,
                                              T.BATCH_NUMBER,
                                              T.MODETYPE,
                                              T.MATERIAL_NAME) Z
                             WHERE Z.F_COUNT > 0";
            }
        }
        public string CheckMaterialInOutStore
        {
            get
            {
                return @"INSERT INTO MED_MATERIAL_CHECK
                          (ID,
                           CODE,
                           CHECK_DATE,
                           ALL_PRICE,
                           F_COUNT,
                           FIRM_ID,
                           HEMO_ID,
                           MODETYPE,
                           CHECKER,
                           UNITS,
                           BATCH_NUMBER,
                           MATERIAL_NAME,
                           CHECKTYPE,
                           STOREMANAGER)
                          SELECT SYS_GUID() AS ID,
                                 Z.CODE,
                                 SYSDATE AS CHECK_DATE,
                                 Z.PRICE AS ALL_PRICE,
                                 Z.F_COUNT,
                                 Z.FIRM_ID,
                                 '' AS HEMO_ID,
                                 Z.MODETYPE,
                                 ':CHECKER' AS CHECKER,
                                 Z.UNITS,
                                 Z.BATCH_NUMBER,
                                 Z.MATERIAL_NAME,
                                 '1' AS CHECKTYPE,
                                 '' as STOREMANAGER
                            FROM (SELECT T.CODE,
                                         T.MODETYPE,
                                         T.FIRM_ID,
                                         T.UNITS,
                                         (SUM(T.F_COUNT) -
                                         (SELECT NVL(SUM(T1.F_COUNT), 0)
                                             FROM MED_MATERIAL_OUTPUT T1
                                            WHERE T1.CODE = T.CODE
                                              AND T1.BATCH_NUMBER = T.BATCH_NUMBER
                                           AND T1.UNITS=(SELECT L.ITEM_NAME FROM MED_COMMON_ITEMLIST L WHERE L.ITEM_ID = T.UNITS))) AS F_COUNT,
                                            (SUM(T.PRICE) -
                                       (SELECT NVL(SUM(T1.PRICE), 0)
                                           FROM MED_MATERIAL_OUTPUT T1
                                          WHERE T1.CODE = T.CODE
                                            AND T1.BATCH_NUMBER = T.BATCH_NUMBER
                                            AND T1.UNITS = (SELECT L.ITEM_NAME
                                                              FROM MED_COMMON_ITEMLIST L
                                                             WHERE L.ITEM_ID = T.UNITS))) AS PRICE,
                                         T.BATCH_NUMBER,
                                         T.MATERIAL_NAME
                                    FROM MED_MATERIAL_INPUT T
                                   GROUP BY T.CODE,
                                            T.MODETYPE,
                                            T.FIRM_ID,
                                            T.UNITS,
                                            T.BATCH_NUMBER,
                                            t.MATERIAL_NAME) Z";
            }
        }
        public string CheckMaterialInOutStoreDetail
        {
            get
            {
                return @"INSERT INTO MED_MATERIAL_CHECK
                                  (ID,
                                   CODE,
                                   CHECK_DATE,
                                   ALL_PRICE,
                                   F_COUNT,
                                   FIRM_ID,
                                   HEMO_ID,
                                   MODETYPE,
                                   CHECKER,
                                   MATERIAL_NAME,
                                   UNITS,
                                   CHECKTYPE)
                                  SELECT SYS_GUID() AS ID,
                                         Z.CODE,
                                         SYSDATE AS CHECK_DATE,
                                         '' AS ALL_PRICE,
                                         Z.F_COUNT,
                                         Z.FIRM_ID,
                                         '' AS HEMO_ID,
                                         Z.MODETYPE,
                                         ':CHECKER' AS CHECKER,
                                         Z.MATERIAL_NAME,
                                         Z.UNITS,
                                         '0' AS CHECKTYPE
                                    FROM (SELECT T.CODE,
                                                 T.MATERIAL_NAME,
                                                 (SELECT R.MATERIAL_SPEC
                                                    FROM MED_MATERIAL_MASTER R
                                                   WHERE R.MATERIAL_ID = T.CODE) SPACE,
                                                 T.MODETYPE,
                                                 T.FIRM_ID,
                                                 T.UNITS,
                                                 (SUM(T.F_COUNT) -
                                                 (SELECT NVL(SUM(T1.F_COUNT), 0)
                                                     FROM MED_MATERIAL_OUTPUT T1
                                                    WHERE T1.CODE = T.CODE
                                            AND T1.UNITS=(SELECT L.ITEM_NAME FROM MED_COMMON_ITEMLIST L WHERE L.ITEM_ID = T.UNITS))) AS F_COUNT
                                            FROM MED_MATERIAL_INPUT T
                                           GROUP BY T.CODE, T.MATERIAL_NAME, T.UNITS, T.MODETYPE, T.FIRM_ID) Z";
            }
        }

        public string GetMaterialStoreInOutByCode
        {
            get
            {
                return @"SELECT SUM(Z.INCOUNT) INCOUNT, SUM(Z.OUTCOUNT) OUTCOUNT
                          FROM (SELECT NVL(SUM(T.F_COUNT), 0) INCOUNT, 0 AS OUTCOUNT
                                  FROM MED_MATERIAL_INPUT T
                                 WHERE T.CODE = :CODE
                                UNION
                                SELECT 0 AS INCOUNT, NVL(SUM(T.F_COUNT), 0) OUTCOUNT
                                  FROM MED_MATERIAL_OUTPUT T
                                 WHERE T.CODE = :CODE) Z";
            }

        }

        public string GetOutPutByCode
        {
            get
            {
                return @"SELECT T.*
                          FROM MED_MATERIAL_OUTPUT T
                         WHERE TRUNC(T.OUPUT_DATE) = TRUNC(SYSDATE)
                           AND T.CODE = :CODE";
            }
        }


        public string DeleteMaterialInPut
        {
            get
            {
                return @"DELETE MED_MATERIAL_INPUT
                        WHERE ID=:ID";
            }
        }
        public string DeleteMaterialOutPut
        {
            get
            {
                return @"DELETE MED_MATERIAL_OUTPUT
                        WHERE ID=:ID";
            }
        }

        #endregion

        #region 获取临时长期处方执行状态与内容列表
        public string GetQueryRecipeList
        {
            get
            {
                return @"select t.recipe_id,t.RECIPE_DATE,'每'||to_char(t.frequency_week)||'周'||to_char(t.frequency_times)||'次，每次'||to_char(t.frequency_hours)||'小时' as Hemo_Times, t.PURIFICATION_MODE,t.therapeutic_method,t.status,
                t.dry_weight,cm.item_name as purification_mode_name,cmm.item_name as purifier_model,cmmm.item_name as therapeutic_method_name,di.name as doctor_name,
                case cure.cure_status when '3' then '已执行' end AS status_name,t.status,t.recipe_type,cure.cure_status,
                case t.status when '0' then '有效' when '1' then '有效'  when '2' then '作废' end as recipe_type_name,
                cure.cure_create_date,msdn.name as NURSE_NAME
                from med_hemo_recipe t left join med_common_itemlist cm on t.purification_mode = cm.item_id
                left join med_common_itemlist cmm on t.first_purifier_model = cmm.item_id
                left join med_common_itemlist cmmm on t.therapeutic_method = cmmm.item_id
                left join MED_STAFF_DICT di on t.user_id =  di.emp_no
                left join med_cure_main cure on cure.recipe_id = t.recipe_id
                left join med_staff_dict msdn
                on cure.PRIMARY_NURSE = msdn.emp_no 
                where t.HEMODIALYSIS_ID = :HEMODIALYSIS_ID 
                order by t.recipe_date desc,cure.cure_create_date desc";
            }
        }

        /// <summary>
        /// 根据患者透析ID与日期范围获取对应的透析方案列表
        /// </summary>
        public string GetQueryRecipeListByDate
        {
            get
            {
                return @"select t.recipe_id,t.RECIPE_DATE,'每'||to_char(t.frequency_week)||'周'||to_char(t.frequency_times)||'次，每次'||to_char(t.frequency_hours)||'小时' as Hemo_Times, t.PURIFICATION_MODE,t.therapeutic_method,t.status,
                t.dry_weight,cm.item_name as purification_mode_name,cmm.item_name as purifier_model,cmmm.item_name as therapeutic_method_name,di.name as doctor_name,
                case cure.cure_status when '3' then '已执行' end AS status_name,t.status,t.recipe_type,cure.cure_status,
                case t.status when '0' then '有效' when '1' then '有效'  when '2' then '作废' end as recipe_type_name,
                cure.cure_create_date,msdn.name as NURSE_NAME
                from med_hemo_recipe t left join med_common_itemlist cm on t.purification_mode = cm.item_id
                left join med_common_itemlist cmm on t.first_purifier_model = cmm.item_id
                left join med_common_itemlist cmmm on t.therapeutic_method = cmmm.item_id
                left join MED_STAFF_DICT di on t.user_id =  di.emp_no
                left join med_cure_main cure on cure.recipe_id = t.recipe_id
                left join med_staff_dict msdn
                on cure.PRIMARY_NURSE = msdn.emp_no 
                where t.HEMODIALYSIS_ID = :HEMODIALYSIS_ID AND T.CREATE_DATE>=:BEGINDATE AND T.CREATE_DATE<=:ENDDATE
                order by t.recipe_date desc,cure.cure_create_date desc";
            }
        }

        /// <summary>
        /// 根据透析号得到对应治疗单数据SQL
        /// </summary>
        public string GetMainCureListByHemoID
        {
            get
            {
                //                return @"
                //                select t.*,i.item_name as vascular_access_name,m.item_name as machine_type_name,
                //                msd.name as doctor_name,msdn.name as nurse_name,machine.machine_name,z.item_name as PURIFICATION_MODE_NAME,j.item_name as purifier_new_name
                //                from med_cure_main t left join MED_COMMON_ITEMLIST z on t.PURIFICATION_MODE = z.item_id
                //                left join MED_COMMON_ITEMLIST i on t.vascular_access_id = i.item_id
                //                left join MED_COMMON_ITEMLIST m on t.machine_type = m.item_id left join med_staff_dict msd
                //                on t.primary_doctor = msd.emp_no 
                //                left join med_staff_dict msdn
                //                on t.PRIMARY_NURSE = msdn.emp_no 
                //                left join med_dialysis_machine machine 
                //                on t.machine_id = machine.machine_id 
                //                left join MED_COMMON_ITEMLIST j on t.purifier_name = j.item_id
                //                WHERE t.HEMODIALYSIS_ID = :HEMODIALYSIS_ID
                //                order by t.CURE_CREATE_DATE desc
                //                ";
                return @"select t.recipe_id,t.RECIPE_DATE,'每'||to_char(t.frequency_week)||'周'||to_char(t.frequency_times)||'次，每次'||to_char(t.frequency_hours)||'小时' as Hemo_Times,
                t.dry_weight,cm.item_name as purification_mode_name,cmm.item_name as purifier_model,cmmm.item_name as therapeutic_method_name,di.name as doctor_name,
                case t.status when '1' then '已启用' when '0' then '已停用' end AS status_name,t.status,t.recipe_type,
                case t.recipe_type when '1' then '长期' when '0' then '临时' end as recipe_type_name,t.remark
                from med_hemo_recipe t left join med_common_itemlist cm on t.purification_mode = cm.item_id
                left join med_common_itemlist cmm on t.first_purifier_model = cmm.item_id
                left join med_common_itemlist cmmm on t.therapeutic_method = cmmm.item_id
                left join MED_STAFF_DICT di on t.user_id =  di.emp_no
                where t.HEMODIALYSIS_ID = :HEMODIALYSIS_ID and t.status = '1' and t.recipe_type='1'
                order by t.recipe_date desc";
            }
        }
        #endregion

        #region 获取疾病ICD10编码数据
        /// <summary>
        /// 获取ICD编码疾病分类信息
        /// </summary>
        public string GetIcdType
        {
            get
            {
                return @"select t.*, t.rowid from med_icd_type t";
            }
        }


        public string GetIcdList
        {
            get
            {
                return @"select * from med_icd_list";
            }
        }

        /// <summary>
        /// 获取根据ICD编号获取疾病列表
        /// </summary>
        public string GetIcdListByID
        {
            get
            {
                return @"select * from med_icd_list where substr(id,0,3) = :ICD ";
            }
        }

        /// <summary>
        /// 根据疾病名称和拼音码得到疾病列表
        /// </summary>
        public string GetIcdListByName
        {
            get
            {
                return @"select * from med_icd_list where icd_name like '%'||:ICD_NAME||'%' OR icd_pinyin LIKE '%'||:ICD_NAME||'%'";
            }
        }
        #endregion

        #region 健康宣教
        /// <summary>
        /// 根据患者透析号得到宣教数据
        /// </summary>
        public string GetHealthEducationByHemoID
        {
            get
            {
                return @"select * from MED_HEALTH_EDUCATION where HEMODIALYSIS_ID=:HEMODIALYSIS_ID order by order_num";
            }
        }
        public string GetHealthEducationByDateTime
        {
            get
            {
                return @"SELECT * FROM MED_HEALTH_EDUCATION T
                            WHERE T.CREATE_DATE >= :STARTTIME
                             AND T.CREATE_DATE < :ENDTIME";
            }
        }

        public string GetHealthEducationByHemoIdAndId
        {
            get
            {
                return @"select * from MED_HEALTH_EDUCATION where HEMODIALYSIS_ID=:HEMODIALYSIS_ID AND ID=:ID order by order_num";
            }
        }

        public string GetHealthEducationListByHemoID
        {
            get
            {
                return @"SELECT  ID,HEMODIALYSIS_ID,max(create_date) as create_date FROM MED_HEALTH_EDUCATION WHERE HEMODIALYSIS_ID=:HEMODIALYSIS_ID 
                         GROUP BY HEMODIALYSIS_ID,ID ORDER BY create_date desc";
            }
        }


        /// <summary>
        /// 得到健教宣教报表数据
        /// </summary>
        public string GetHealthEducationReportByHemoID
        {
            get
            {
                return @"select t.health_id,t.health_type,t.health_nurse_date,case t.health_verbal when '1' then '√' end as health_verbal,
                case t.health_written when '1' then '√' end as health_written,case t.health_appraise 
                when '掌握' then '√'  when '部分掌握' then '-'
                when '未掌握' then '×'  end as health_appraise,s.name as health_nurse_id,t.health_headman_date,
                case t.health_headman_appraise 
                when '掌握' then '√'  when '部分掌握' then '-'
                when '未掌握' then '×'  end as health_headman_appraise,h.name as health_headman_id,m.name as health_headnurse_id
                from med_health_education t
                left join med_staff_dict s on t.health_nurse_id = s.emp_no 
                left join med_staff_dict h on t.health_headman_id = h.emp_no
                left join med_staff_dict m on t.health_headnurse_id = m.emp_no
                where t.HEMODIALYSIS_ID=:HEMODIALYSIS_ID and ID=:ID
                order by t.order_num";
            }
        }
        #endregion

        #region 患者病程记录

        /// <summary>
        /// 根据ID得到病程记录数据
        /// </summary>
        public string GetPatientProgressNoteById
        {
            get
            {
                return @"SELECT T.ID,T.PATIENT_ID,T.HEMODIALYSIS_ID,T.PROGRESS_NODE,T.DOCTOR_ID,T.CREATE_DATE,T.IS_DELETE,
                        DECODE(T.COMPLAINTS,'0','1','0') COMPLAINTS_NOTHAS,DECODE(T.COMPLAINTS,'0','0','1') COMPLAINTS_HAS,T.COMPLAINTS_CONTENT,
                        DECODE(T.VASCULAR_ACCESS,'0','1','0') VASCULAR_ACCESS_NOCHANGE,DECODE(T.VASCULAR_ACCESS,'1','1','0') VASCULAR_ACCESS_CHANGE,DECODE(T.VASCULAR_ACCESS_SUGADJ,'0','1','0') VASCULAR_ACCESS_SUGGEST,DECODE(T.VASCULAR_ACCESS_SUGADJ,'1','1','0') VASCULAR_ACCESS_ADJUST,T.VASCULAR_ACCESS_CHANGE VASCULAR_ACCESS_CHANGE_DESC,T.VASCULAR_ACCESS_NOTE,
                        DECODE(T.THERAPEUTIC_METHOD,'0','1','0') THERAPEUTIC_METHOD_NOCHANGE,DECODE(T.THERAPEUTIC_METHOD,'1','1','0') THERAPEUTIC_METHOD_CHANGE,DECODE(T.THERAPEUTIC_METHOD_SUGADJ,'0','1','0') THERAPEUTIC_METHOD_SUGGEST,DECODE(T.THERAPEUTIC_METHOD_SUGADJ,'1','1','0') THERAPEUTIC_METHOD_ADJUST,T.THERAPEUTIC_METHOD_CHANGE THERAPEUTIC_METHOD_CHANGE_DESC,T.THERAPEUTIC_METHOD_NOTE,
                        DECODE(T.CAPACITY_CONTROL,'0','1','0') CAPACITY_CONTROL_GOOD,DECODE(T.CAPACITY_CONTROL,'1','1','0') CAPACITY_CONTROL_NOTGOOD,T.DRY_WEIHGT,T.MAX_DRY_WEIHGT,T.PERCENT_DRY_WEIGHT,
                        DECODE(T.BLOOD_CONTROL,'0','1','0') BLOOD_CONTROL_GOOD,DECODE(T.BLOOD_CONTROL,'1','1','0') BLOOD_CONTROL_NOTGOOD,T.HIGH_BLOOD_PRESSURE,T.LOW_BLOOD_PRESSURE,
                        DECODE(T.ANEMIA_CORRECTION,'0','1','0') ANEMIA_CORRECTION_GOOD,DECODE(T.ANEMIA_CORRECTION,'1','1','0') ANEMIA_CORRECTION_NOTGOOD,DECODE(T.ANEMIA_CORRECTION_SUGADJ,'0','1','0') ANEMIA_CORRECTION_SUGGEST,DECODE(T.ANEMIA_CORRECTION_SUGADJ,'1','1','0') ANEMIA_CORRECTION_ADJUST,T.ANEMIA_CORRECTION_BAD,T.ANEMIA_CORRECTION_NOTE,
                        DECODE(T.BONE_MINERAL,'0','1','0') BONE_MINERAL_GOOD,DECODE(T.BONE_MINERAL,'1','1','0') BONE_MINERAL_NOTGOOD,DECODE(T.BONE_MINERAL_SUGADJ,'0','1','0') BONE_MINERAL_SUGGEST,DECODE(T.BONE_MINERAL_SUGADJ,'1','1','0') BONE_MINERAL_ADJUST,T.BONE_MINERAL_BAD,T.BONE_MINERAL_NOTE,
                        DECODE(T.NUTRITIONAL_STATUS,'0','1','0') NUTRITIONAL_STATUS_GOOD,DECODE(T.NUTRITIONAL_STATUS,'1','1','0') NUTRITIONAL_STATUS_NOTGOOD,DECODE(T.NUTRITIONAL_STATUS_SUGADJ,'0','1','0') NUTRITIONAL_STATUS_SUGGEST,DECODE(T.NUTRITIONAL_STATUS_SUGADJ,'1','1','0') NUTRITIONAL_STATUS_ADJUST,T.NUTRITIONAL_STATUS_BAD,T.NUTRITIONAL_STATUS_NOTE,
                        S.NAME DOCTOR_NAME FROM MED_PATIENT_PROGRESS_NOTE T LEFT JOIN MED_STAFF_DICT S ON T.DOCTOR_ID = S.EMP_NO WHERE T.ID =:ID AND T.IS_DELETE='0' ORDER BY T.CREATE_DATE";
            }
        }

        /// <summary>
        /// 根据患者透析ID得到病程记录数据
        /// </summary>
        public string GetPatientProgressNoteByHemoId
        {
            get
            {
                //return @"SELECT T.*, T.ROWID,P.NAME AS PATIENT_NAME,S.NAME AS DOCTOR_NAME FROM MED_PATIENT_PROGRESS_NOTE T LEFT JOIN MED_PATIENTS P ON T.HEMODIALYSIS_ID=P.HEMODIALYSIS_ID LEFT JOIN MED_STAFF_DICT S ON T.DOCTOR_ID = S.EMP_NO WHERE T.HEMODIALYSIS_ID =:HEMODIALYSIS_ID AND T.IS_DELETE='0'";
                return @"SELECT T.ID,T.PATIENT_ID,T.HEMODIALYSIS_ID,T.PROGRESS_NODE,T.DOCTOR_ID,T.CREATE_DATE,T.IS_DELETE,T.COMPLAINTS,T.COMPLAINTS_CONTENT,
                        DECODE(T.COMPLAINTS,'0','无',T.COMPLAINTS_CONTENT) COMPLAINTS_CONTENT_DESC,T.CAPACITY_CONTROL,
                        DECODE(T.CAPACITY_CONTROL,'0','达标','1','不达标') CAPACITY_CONTROL_DESC,T.DRY_WEIHGT,T.MAX_DRY_WEIHGT,
                        T.PERCENT_DRY_WEIGHT,T.BLOOD_CONTROL,DECODE(T.BLOOD_CONTROL,'0','达标','1','不达标') BLOOD_CONTROL_DESC,
                        T.HIGH_BLOOD_PRESSURE,T.LOW_BLOOD_PRESSURE,T.VASCULAR_ACCESS,T.VASCULAR_ACCESS_CHANGE,T.VASCULAR_ACCESS_SUGADJ,T.VASCULAR_ACCESS_NOTE,DECODE(T.VASCULAR_ACCESS,'0','无变化','1',T.VASCULAR_ACCESS_CHANGE) VASCULAR_ACCESS_DESC,
                        T.THERAPEUTIC_METHOD,T.THERAPEUTIC_METHOD_CHANGE,T.THERAPEUTIC_METHOD_SUGADJ,T.THERAPEUTIC_METHOD_NOTE,DECODE(T.THERAPEUTIC_METHOD,'0','无变化','1',T.THERAPEUTIC_METHOD_CHANGE) THERAPEUTIC_METHOD_DESC,
                        T.ANEMIA_CORRECTION,T.ANEMIA_CORRECTION_BAD,T.ANEMIA_CORRECTION_SUGADJ,T.ANEMIA_CORRECTION_NOTE,DECODE(T.ANEMIA_CORRECTION,'0','达标','1',T.ANEMIA_CORRECTION_BAD) ANEMIA_CORRECTION_DESC,
                        T.BONE_MINERAL,T.BONE_MINERAL_BAD,T.BONE_MINERAL_SUGADJ,T.BONE_MINERAL_NOTE,DECODE(T.BONE_MINERAL,'0','达标','1',T.BONE_MINERAL_BAD) BONE_MINERAL_DESC,
                        T.NUTRITIONAL_STATUS,T.NUTRITIONAL_STATUS_BAD,T.NUTRITIONAL_STATUS_SUGADJ,T.NUTRITIONAL_STATUS_NOTE,DECODE(T.NUTRITIONAL_STATUS,'0','达标','1',T.NUTRITIONAL_STATUS_BAD) NUTRITIONAL_STATUS_DESC,
                        T.ROWID,P.NAME AS PATIENT_NAME,S.NAME AS DOCTOR_NAME , DECODE(T1.BASEINFO, NULL, '1', '2') ISUPLOAD, T1.BASEINFO,DECODE(T1.BASEINFO, NULL, '未上传', '已上传') UPSTATE
                        FROM MED_PATIENT_PROGRESS_NOTE T LEFT JOIN MED_PATIENTS P ON T.HEMODIALYSIS_ID=P.HEMODIALYSIS_ID LEFT JOIN MED_STAFF_DICT S ON T.DOCTOR_ID = S.EMP_NO   LEFT JOIN (SELECT *
                        FROM MED_PATIENT_DATAREPORT Z WHERE Z.STATE = '1' AND Z.TYPE = '2'  AND Z.EXTEND = 'HZBCXX' AND Z.EXTEND5 = '福建省上报平台') T1 ON (T.HEMODIALYSIS_ID = T1.HEMODIALYSIS_ID AND T.ID=T1.MAPIP)
                        WHERE T.HEMODIALYSIS_ID =:HEMODIALYSIS_ID AND T.IS_DELETE='0' ORDER BY T.CREATE_DATE";
            }
        }

        /// <summary>
        /// 根据患者透析ID、日期得到病程记录数据
        /// </summary>
        public string GetPatientProgressNoteByHemoIdAndDate
        {
            get
            {
                return @"SELECT T.ID,T.PATIENT_ID,T.HEMODIALYSIS_ID,T.PROGRESS_NODE,T.DOCTOR_ID,T.CREATE_DATE,T.IS_DELETE,T.COMPLAINTS,T.COMPLAINTS_CONTENT,
                        DECODE(T.COMPLAINTS,'0','无',T.COMPLAINTS_CONTENT) COMPLAINTS_CONTENT_DESC,T.CAPACITY_CONTROL,
                        DECODE(T.CAPACITY_CONTROL,'0','达标','1','不达标') CAPACITY_CONTROL_DESC,T.DRY_WEIHGT,T.MAX_DRY_WEIHGT,
                        T.PERCENT_DRY_WEIGHT,T.BLOOD_CONTROL,DECODE(T.BLOOD_CONTROL,'0','达标','1','不达标') BLOOD_CONTROL_DESC,
                        T.HIGH_BLOOD_PRESSURE,T.LOW_BLOOD_PRESSURE,T.VASCULAR_ACCESS,T.VASCULAR_ACCESS_CHANGE,T.VASCULAR_ACCESS_SUGADJ,T.VASCULAR_ACCESS_NOTE,DECODE(T.VASCULAR_ACCESS,'0','无变化','1',T.VASCULAR_ACCESS_CHANGE) VASCULAR_ACCESS_DESC,
                        T.THERAPEUTIC_METHOD,T.THERAPEUTIC_METHOD_CHANGE,T.THERAPEUTIC_METHOD_SUGADJ,T.THERAPEUTIC_METHOD_NOTE,DECODE(T.THERAPEUTIC_METHOD,'0','无变化','1',T.THERAPEUTIC_METHOD_CHANGE) THERAPEUTIC_METHOD_DESC,
                        T.ANEMIA_CORRECTION,T.ANEMIA_CORRECTION_BAD,T.ANEMIA_CORRECTION_SUGADJ,T.ANEMIA_CORRECTION_NOTE,DECODE(T.ANEMIA_CORRECTION,'0','达标','1',T.ANEMIA_CORRECTION_BAD) ANEMIA_CORRECTION_DESC,
                        T.BONE_MINERAL,T.BONE_MINERAL_BAD,T.BONE_MINERAL_SUGADJ,T.BONE_MINERAL_NOTE,DECODE(T.BONE_MINERAL,'0','达标','1',T.BONE_MINERAL_BAD) BONE_MINERAL_DESC,
                        T.NUTRITIONAL_STATUS,T.NUTRITIONAL_STATUS_BAD,T.NUTRITIONAL_STATUS_SUGADJ,T.NUTRITIONAL_STATUS_NOTE,DECODE(T.NUTRITIONAL_STATUS,'0','达标','1',T.NUTRITIONAL_STATUS_BAD) NUTRITIONAL_STATUS_DESC,
                        T.ROWID,P.NAME AS PATIENT_NAME,S.NAME AS DOCTOR_NAME, DECODE(T1.BASEINFO, NULL, '1', '2') ISUPLOAD,T1.BASEINFO,DECODE(T1.BASEINFO, NULL, '未上传', '已上传') UPSTATE 
                    FROM MED_PATIENT_PROGRESS_NOTE T LEFT JOIN MED_PATIENTS P ON T.HEMODIALYSIS_ID=P.HEMODIALYSIS_ID LEFT JOIN MED_STAFF_DICT S ON T.DOCTOR_ID = S.EMP_NO  LEFT JOIN (SELECT * FROM MED_PATIENT_DATAREPORT Z WHERE Z.STATE = '1' AND Z.TYPE = '2'AND Z.EXTEND = 'HZBCXX'  AND Z.EXTEND5 = '福建省上报平台')T1  ON (T.HEMODIALYSIS_ID = T1.HEMODIALYSIS_ID AND T.ID=T1.MAPIP)
                        WHERE T.HEMODIALYSIS_ID =:HEMODIALYSIS_ID AND T.CREATE_DATE>=TRUNC(:BEGINDATE,'MM') AND T.CREATE_DATE<TRUNC(:ENDDATE,'MM') AND T.IS_DELETE='0' ORDER BY T.CREATE_DATE";
            }
        }

        /// <summary>
        /// 根据ID删除患者病程记录
        /// </summary>
        public string DeletePatientProgressNoteById
        {
            get
            {
                return @"UPDATE MED_PATIENT_PROGRESS_NOTE SET IS_DELETE='1' WHERE ID = :ID";
            }
        }

        /// <summary>
        /// 根据患者透析ID和日期得到病程记录
        /// </summary>
        public string GetPatientProgressNoteByIDAndCreateDate
        {
            get
            {
                return @"SELECT T.* FROM MED_PATIENT_PROGRESS_NOTE T WHERE T.HEMODIALYSIS_ID=:HEMODIALYSIS_ID AND trunc(T.CREATE_DATE) = trunc(:CREATEDATE)";
            }
        }
        #endregion

        #region 获取血透机采集表数据功能

        public string GetHemoParametersCollectionByMonitorAndDate
        {
            get
            {
                return @"SELECT T.*,'' as ANTICOAGULANT, to_char(t.SYSTOLIC_PRESSURE)||'/'||to_char(t.DIASTOLIC_PRESSURE) as BLOOD_PRESSURE,
                        T.ROWID,T.EXTENDED_FIELD_1 AS VASCULAR_ACCESS_ERRHYISIS,T.EXTENDED_FIELD_2 AS VASCULAR_ACCESS_GLIDE,T.EXTENDED_FIELD_3 AS CLINICAL_MANIFESTATION  FROM MED_HEMO_PARAMETERS_COLLECTION T WHERE T.MONITOR_LABEL=:MONITOR_LABEL
                        AND TRUNC(T.CREATE_DATE) = TRUNC(:CREATE_DATE) ORDER BY T.CREATE_DATE ";
            }
        }

        public string GetHemoParametersCollectionByMonitorAndDoubleDate
        {
            get
            {
                return @"SELECT T.*,'' as ANTICOAGULANT, to_char(t.SYSTOLIC_PRESSURE)||'/'||to_char(t.DIASTOLIC_PRESSURE) as BLOOD_PRESSURE,
                        T.ROWID,T.EXTENDED_FIELD_1 AS VASCULAR_ACCESS_ERRHYISIS,T.EXTENDED_FIELD_2 AS VASCULAR_ACCESS_GLIDE,T.EXTENDED_FIELD_3 AS CLINICAL_MANIFESTATION  FROM MED_HEMO_PARAMETERS_COLLECTION T WHERE T.MONITOR_LABEL=:MONITOR_LABEL
                        AND TRUNC(T.CREATE_DATE) = TRUNC(:CREATE_DATE) AND T.CREATE_DATE>:BEGINTIME AND T.CREATE_DATE<:ENDTIME ORDER BY T.CREATE_DATE ";
            }
        }

        /// <summary>
        /// 获取数据采集设置列表
        /// </summary>
        public string GetDataGatherSetList
        {
            get
            {
                return @"SELECT * FROM MED_HEMO_PARAMETERS_SETTING";
            }
        }

        #endregion

        #region 血管通路评估
        /// <summary>
        /// 根据ID得到内瘘评估数据
        /// </summary>
        public string GetEstimateInBasketByID
        {
            get
            {
                return @"SELECT * FROM MED_ESTIMATE_IN_BASKET WHERE ID =:ID";
            }
        }

        /// <summary>
        /// 根据患者ID与日期跨度得到患者内瘘评估单列表
        /// </summary>
        public string GetEstimateInBasketByParams
        {
            get
            {
                return
                    @"SELECT E.HEMODIALYSIS_ID,E.ID,E.CREATE_DATE,
                        DECODE(E.CLEAN,'1','√','×') as CLEAN,
                        DECODE(E.RED_HOT,'1','√','×') as RED_HOT,
                        DECODE(E.SWOLLEN_PAIN,'1','√','×') as SWOLLEN_PAIN,
                        DECODE(E.ECCHYMOSIS,'1','√','×') as ECCHYMOSIS,
                        DECODE(E.TREMOR,'1','√','×') as TREMOR,
                        DECODE(E.NOISE,'1','√','×') as NOISE,            
                        DECODE(E.VASCULAR_ELASTICITY,'0','A','1','B','2','C','3','D','4','E','5','F') || E.Vascular_Other AS VASCULAR_ELASTICITY,
                        DECODE(E.FLOW_BETTER,'1','☆','')|| DECODE(E.SUCTION,'1','±','') ||  DECODE(E.MOVEMENT_REVERSAL,'1','↓','') as FLOW_BETTER,
                        PUNCTURE_DISTANCE,
                        DECODE(E.PUNCTURE_DIRECTION,'1','↓','↑') as PUNCTURE_DIRECTION,
                        DECODE(E.FISTULA_SPACING,'1','√','×') as FISTULA_SPACING,
                        DECODE(E.WOUND_ALLERGY,'1','√','×') as WOUND_ALLERGY,
                        DECODE(E.PLASTER_ALLERGY,'1','√','×') as PLASTER_ALLERGY,
                        (SELECT T.USER_NAME FROM MED_USERS T WHERE T.USER_ID=E.ASSESSMENT_PEOPLE) AS ASSESSMENT_PEOPLE,--E.ASSESSMENT_PEOPLE,
                         E.VASCULAR_OTHER AS REMARK --E.REMARK 
                        FROM MED_ESTIMATE_IN_BASKET E  
                        LEFT JOIN MED_PATIENTS P ON E.HEMODIALYSIS_ID=P.HEMODIALYSIS_ID
                        WHERE E.IS_DELETE = '0' AND E.CREATE_DATE>=:BEGINDATE AND E.CREATE_DATE<=:ENDDATE
                        AND E.IS_DELETE='0' AND P.HEMODIALYSIS_ID = :HEMODIALYSIS_ID
                        AND P.NAME LIKE '%'||:NAME||'%' AND P.IS_DELETE='0' ORDER BY E.CREATE_DATE";
            }
        }

        /// <summary>
        /// 根据ID删除内瘘评估数据
        /// </summary>
        public string DeleteEstimateInBasketById
        {
            get
            {
                return @"UPDATE MED_ESTIMATE_IN_BASKET SET IS_DELETE='1' WHERE ID=:ID";
            }
        }
        #endregion

        #region 静脉导管评估

        /// <summary>
        /// 根据病人姓名&日期获取长期留置静脉导管评估数据列表
        /// </summary>
        public string GetEstimateLongVenousByNameAndDate
        {
            get
            {
                return @"SELECT E.ID,
                        E.CREATE_DATE,
                        DECODE(E.APPLICATION,'1','是','否') as APPLICATION,
                        E.HEMODIALYSIS_ID,
                        DECODE(E.CATHETER,'1','是','否') as CATHETER,
                        DECODE(E.CLEAN_AND_DRY,'1','是','否') as CLEAN_AND_DRY,
                        DECODE(E.FLOW_POLLUTION,'1','有','无') as FLOW_POLLUTION,
                        DECODE(E.RED,'1','是','否') as RED,
                        DECODE(E.SWOLLEN,'1','是','否') as SWOLLEN,
                        DECODE(E.HOT,'1','是','否') as HOT,
                        DECODE(E.PAIN,'1','是','否') as PAIN,
                        DECODE(E.ARTERY_CLARIFICATION,'1','是','否') as ARTERY_CLARIFICATION,
                        DECODE(E.ARTERY_BLOOD,'1','有','无') as ARTERY_BLOOD,
                        DECODE(E.ARTERY_FLOW_VELOCITY,'1','是','否') as ARTERY_FLOW_VELOCITY,
                        DECODE(E.ARTERY_REFLUX,'1','是','否') as ARTERY_REFLUX,
                        DECODE(E.ARTERY_THROMBOSIS,'1','是','否') as ARTERY_THROMBOSIS,
                        DECODE(E.VEIN_CLARIFICATION,'1','是','否') as VEIN_CLARIFICATION,
                        DECODE(E.VEIN_BLOOD,'1','有','无') as VEIN_BLOOD,
                        DECODE(E.VEIN_FLOW_VELOCITY,'1','是','否') as VEIN_FLOW_VELOCITY,
                        DECODE(E.VEIN_REFLUX,'1','是','否') as VEIN_REFLUX,
                        DECODE(E.VEIN_THROMBOSIS,'1','是','否') as VEIN_THROMBOSIS,
                        DECODE(E.FLOW_BETTER,'1','是','否') as FLOW_BETTER,
                        DECODE(E.SUCTION,'1','有','无') as SUCTION,
                        E.BLOOD_LEAD_END,
                        E.POSITION_REQUIREMENT,
                        E.HEPARIN_SODIUM,
                        E.UROKINASE,
                        E.OTHER,
                        DECODE(E.IN_DRESSING,'1','是','否') as IN_DRESSING,
                        DECODE(E.INFECTED,'1','是','否') as INFECTED,
                        DECODE(E.THROMBOLYSIS,'1','是','否') as THROMBOLYSIS
                        FROM MED_ESTIMATE_LONG_VENOUS E
                        LEFT JOIN MED_PATIENTS P ON E.HEMODIALYSIS_ID=P.HEMODIALYSIS_ID
                        WHERE E.CREATE_DATE>=:BEGINDATE AND E.CREATE_DATE<=:ENDDATE
                        AND E.IS_DELETE='0'
                        AND (P.NAME LIKE '%'||:NAME||'%' OR P.INPUT_CODE LIKE '%'||:NAME||'%') AND P.IS_DELETE='0'";
            }
        }

        /// <summary>
        /// 根据病人姓名&日期获取临时留置静脉导管评估数据列表
        /// </summary>
        public string GetEstimateVenousCatheterByNameAndDate
        {
            get
            {
                return @"SELECT E.ID,
                        E.CREATE_DATE,
                        DECODE(E.APPLICATION,'1','是','否') as APPLICATION,
                        DECODE(E.SUTURE,'1','是','否') as SUTURE,
                        E.HEMODIALYSIS_ID,
                        DECODE(E.CATHETER,'1','是','否') as CATHETER,
                        DECODE(E.CLEAN_AND_DRY,'1','是','否') as CLEAN_AND_DRY,
                        DECODE(E.FLOW_POLLUTION,'1','有','无') as FLOW_POLLUTION,
                        DECODE(E.RED,'1','是','否') as RED,
                        DECODE(E.SWOLLEN,'1','是','否') as SWOLLEN,
                        DECODE(E.HOT,'1','是','否') as HOT,
                        DECODE(E.PAIN,'1','是','否') as PAIN,
                        DECODE(E.ARTERY_CLARIFICATION,'1','是','否') as ARTERY_CLARIFICATION,
                        DECODE(E.ARTERY_BLOOD,'1','有','无') as ARTERY_BLOOD,
                        DECODE(E.ARTERY_FLOW_VELOCITY,'1','是','否') as ARTERY_FLOW_VELOCITY,
                        DECODE(E.ARTERY_REFLUX,'1','是','否') as ARTERY_REFLUX,
                        DECODE(E.ARTERY_THROMBOSIS,'1','是','否') as ARTERY_THROMBOSIS,
                        DECODE(E.VEIN_CLARIFICATION,'1','是','否') as VEIN_CLARIFICATION,
                        DECODE(E.VEIN_BLOOD,'1','有','无') as VEIN_BLOOD,
                        DECODE(E.VEIN_FLOW_VELOCITY,'1','是','否') as VEIN_FLOW_VELOCITY,
                        DECODE(E.VEIN_REFLUX,'1','是','否') as VEIN_REFLUX,
                        DECODE(E.VEIN_THROMBOSIS,'1','是','否') as VEIN_THROMBOSIS,
                        DECODE(E.FLOW_BETTER,'1','是','否') as FLOW_BETTER,
                        DECODE(E.SUCTION,'1','有','无') as SUCTION,
                        E.BLOOD_LEAD_END,
                        E.POSITION_REQUIREMENT,
                        E.HEPARIN_SODIUM,
                        E.UROKINASE,
                        E.OTHER,
                        DECODE(E.IN_DRESSING,'1','是','否') as IN_DRESSING,
                        DECODE(E.INFECTED,'1','是','否') as INFECTED,
                        DECODE(E.THROMBOLYSIS,'1','是','否') as THROMBOLYSIS
                        FROM MED_ESTIMATE_VENOUS_CATHETER E
                        LEFT JOIN MED_PATIENTS P ON E.HEMODIALYSIS_ID=P.HEMODIALYSIS_ID
                        WHERE E.CREATE_DATE>=:BEGINDATE AND E.CREATE_DATE<=:ENDDATE
                        AND E.IS_DELETE='0'
                        AND (P.NAME LIKE '%'||:NAME||'%' OR P.INPUT_CODE LIKE '%'||:NAME||'%') AND P.IS_DELETE='0'";
            }
        }

        /// <summary>
        /// 根据病人透析编号&日期获取长期留置静脉导管评估数据列表
        /// </summary>
        public string GetEstimateLongVenousByHemoIdAndDate
        {
            get
            {
                return @"SELECT E.ID,
                        E.CREATE_DATE,
                        DECODE(E.APPLICATION,'1','是','否') as APPLICATION,
                        E.HEMODIALYSIS_ID,
                        DECODE(E.CATHETER,'1','是','否') as CATHETER,
                        DECODE(E.CLEAN_AND_DRY,'1','是','否') as CLEAN_AND_DRY,
                        DECODE(E.FLOW_POLLUTION,'1','有','无') as FLOW_POLLUTION,
                        DECODE(E.RED,'1','是','否') as RED,
                        DECODE(E.SWOLLEN,'1','是','否') as SWOLLEN,
                        DECODE(E.HOT,'1','是','否') as HOT,
                        DECODE(E.PAIN,'1','是','否') as PAIN,
                        DECODE(E.ARTERY_CLARIFICATION,'1','是','否') as ARTERY_CLARIFICATION,
                        DECODE(E.ARTERY_BLOOD,'1','有','无') as ARTERY_BLOOD,
                        DECODE(E.ARTERY_FLOW_VELOCITY,'1','是','否') as ARTERY_FLOW_VELOCITY,
                        DECODE(E.ARTERY_REFLUX,'1','是','否') as ARTERY_REFLUX,
                        DECODE(E.ARTERY_THROMBOSIS,'1','是','否') as ARTERY_THROMBOSIS,
                        DECODE(E.VEIN_CLARIFICATION,'1','是','否') as VEIN_CLARIFICATION,
                        DECODE(E.VEIN_BLOOD,'1','有','无') as VEIN_BLOOD,
                        DECODE(E.VEIN_FLOW_VELOCITY,'1','是','否') as VEIN_FLOW_VELOCITY,
                        DECODE(E.VEIN_REFLUX,'1','是','否') as VEIN_REFLUX,
                        DECODE(E.VEIN_THROMBOSIS,'1','是','否') as VEIN_THROMBOSIS,
                        DECODE(E.FLOW_BETTER,'1','是','否') as FLOW_BETTER,
                        DECODE(E.SUCTION,'1','有','无') as SUCTION,
                        E.BLOOD_LEAD_END,
                        E.POSITION_REQUIREMENT,
                        E.HEPARIN_SODIUM,
                        E.UROKINASE,
                        E.OTHER,
                        DECODE(E.IN_DRESSING,'1','是','否') as IN_DRESSING,
                        DECODE(E.INFECTED,'1','是','否') as INFECTED,
                        DECODE(E.THROMBOLYSIS,'1','是','否') as THROMBOLYSIS
                        FROM MED_ESTIMATE_LONG_VENOUS E
                        WHERE E.CREATE_DATE>=:BEGINDATE AND E.CREATE_DATE<=:ENDDATE
                        AND E.HEMODIALYSIS_ID=:HEMOID
                        AND E.IS_DELETE='0'";
            }
        }

        /// <summary>
        /// 根据病人透析编号&日期获取临时留置静脉导管评估数据列表
        /// </summary>
        public string GetEstimateVenousCatheterByHemoIdAndDate
        {
            get
            {
                return @"SELECT E.ID,
                        E.CREATE_DATE,
                        DECODE(E.APPLICATION,'1','是','否') as APPLICATION,
                        DECODE(E.SUTURE,'1','是','否') as SUTURE,
                        E.HEMODIALYSIS_ID,
                        DECODE(E.CATHETER,'1','是','否') as CATHETER,
                        DECODE(E.CLEAN_AND_DRY,'1','是','否') as CLEAN_AND_DRY,
                        DECODE(E.FLOW_POLLUTION,'1','有','无') as FLOW_POLLUTION,
                        DECODE(E.RED,'1','是','否') as RED,
                        DECODE(E.SWOLLEN,'1','是','否') as SWOLLEN,
                        DECODE(E.HOT,'1','是','否') as HOT,
                        DECODE(E.PAIN,'1','是','否') as PAIN,
                        DECODE(E.ARTERY_CLARIFICATION,'1','是','否') as ARTERY_CLARIFICATION,
                        DECODE(E.ARTERY_BLOOD,'1','有','无') as ARTERY_BLOOD,
                        DECODE(E.ARTERY_FLOW_VELOCITY,'1','是','否') as ARTERY_FLOW_VELOCITY,
                        DECODE(E.ARTERY_REFLUX,'1','是','否') as ARTERY_REFLUX,
                        DECODE(E.ARTERY_THROMBOSIS,'1','是','否') as ARTERY_THROMBOSIS,
                        DECODE(E.VEIN_CLARIFICATION,'1','是','否') as VEIN_CLARIFICATION,
                        DECODE(E.VEIN_BLOOD,'1','有','无') as VEIN_BLOOD,
                        DECODE(E.VEIN_FLOW_VELOCITY,'1','是','否') as VEIN_FLOW_VELOCITY,
                        DECODE(E.VEIN_REFLUX,'1','是','否') as VEIN_REFLUX,
                        DECODE(E.VEIN_THROMBOSIS,'1','是','否') as VEIN_THROMBOSIS,
                        DECODE(E.FLOW_BETTER,'1','是','否') as FLOW_BETTER,
                        DECODE(E.SUCTION,'1','有','无') as SUCTION,
                        E.BLOOD_LEAD_END,
                        E.POSITION_REQUIREMENT,
                        E.HEPARIN_SODIUM,
                        E.UROKINASE,
                        E.OTHER,
                        DECODE(E.IN_DRESSING,'1','是','否') as IN_DRESSING,
                        DECODE(E.INFECTED,'1','是','否') as INFECTED,
                        DECODE(E.THROMBOLYSIS,'1','是','否') as THROMBOLYSIS
                        FROM MED_ESTIMATE_VENOUS_CATHETER E
                        WHERE E.CREATE_DATE>=:BEGINDATE AND E.CREATE_DATE<=:ENDDATE
                        AND E.HEMODIALYSIS_ID=:HEMOID
                        AND E.IS_DELETE='0'";
            }
        }

        /// <summary>
        /// 获取长期留置静脉导管评估数据列表
        /// </summary>
        public string GetEstimateLongVenousList
        {
            get
            {
                return @"SELECT * FROM MED_ESTIMATE_LONG_VENOUS WHERE IS_DELETE='0'";
            }
        }

        /// <summary>
        /// 获取临时留置静脉导管评估数据列表
        /// </summary>
        public string GetEstimateVenousCatheterList
        {
            get
            {
                return @"SELECT * FROM MED_ESTIMATE_VENOUS_CATHETER WHERE IS_DELETE='0'";
            }
        }

        /// <summary>
        /// 根据ID获取长期留置静脉导管评估数据列表
        /// </summary>
        public string GetEstimateLongVenousById
        {
            get
            {
                return @"SELECT * FROM MED_ESTIMATE_LONG_VENOUS WHERE ID=:ID AND IS_DELETE='0'";
            }
        }

        /// <summary>
        /// 根据ID获取临时留置静脉导管评估数据列表
        /// </summary>
        public string GetEstimateVenousCatheterById
        {
            get
            {
                return @"SELECT * FROM MED_ESTIMATE_VENOUS_CATHETER WHERE ID=:ID AND IS_DELETE='0'";
            }
        }

        /// <summary>
        /// 根据病人透析编号&单个日期获取长期留置静脉导管评估数据列表
        /// </summary>
        public string GetEstimateLongVenousByHemoIdAndSingleDate
        {
            get
            {
                return @"SELECT E.ID,
                        E.CREATE_DATE,
                        DECODE(E.APPLICATION,'1','√','×') as APPLICATION,
                        E.HEMODIALYSIS_ID,
                        DECODE(E.CATHETER,'1','√','×') as CATHETER,
                        DECODE(E.CLEAN_AND_DRY,'1','√','×') as CLEAN_AND_DRY,
                        DECODE(E.FLOW_POLLUTION,'1','√','×') as FLOW_POLLUTION,
                        DECODE(E.RED,'1','√','×') as RED,
                        DECODE(E.SWOLLEN,'1','√','×') as SWOLLEN,
                        DECODE(E.HOT,'1','√','×') as HOT,
                        DECODE(E.PAIN,'1','√','×') as PAIN,
                        DECODE(E.ARTERY_CLARIFICATION,'1','√','×') as ARTERY_CLARIFICATION,
                        DECODE(E.ARTERY_BLOOD,'1','√','×') as ARTERY_BLOOD,
                        DECODE(E.ARTERY_FLOW_VELOCITY,'1','√','×') as ARTERY_FLOW_VELOCITY,
                        DECODE(E.ARTERY_REFLUX,'1','√','×') as ARTERY_REFLUX,
                        DECODE(E.ARTERY_THROMBOSIS,'1','√','×') as ARTERY_THROMBOSIS,
                        DECODE(E.VEIN_CLARIFICATION,'1','√','×') as VEIN_CLARIFICATION,
                        DECODE(E.VEIN_BLOOD,'1','√','×') as VEIN_BLOOD,
                        DECODE(E.VEIN_FLOW_VELOCITY,'1','√','×') as VEIN_FLOW_VELOCITY,
                        DECODE(E.VEIN_REFLUX,'1','√','×') as VEIN_REFLUX,
                        DECODE(E.VEIN_THROMBOSIS,'1','√','×') as VEIN_THROMBOSIS,
                        DECODE(E.FLOW_BETTER,'1','√','×') as FLOW_BETTER,
                        DECODE(E.SUCTION,'1','√','×') as SUCTION,
                        E.BLOOD_LEAD_END,
                        E.POSITION_REQUIREMENT,
                        E.HEPARIN_SODIUM,
                        E.UROKINASE,
                        E.OTHER,
                        DECODE(E.IN_DRESSING,'1','√','×') as IN_DRESSING,
                        DECODE(E.INFECTED,'1','√','×') as INFECTED,
                        DECODE(E.THROMBOLYSIS,'1','√','×') as THROMBOLYSIS
                        FROM MED_ESTIMATE_LONG_VENOUS E
                        WHERE TO_CHAR(E.CREATE_DATE,'YYYY-MM')=TO_CHAR(:CREATE_DATE,'YYYY-MM')
                        AND E.HEMODIALYSIS_ID=:HEMOID
                        AND E.IS_DELETE='0'";
            }
        }

        /// <summary>
        /// 根据病人透析编号&单个日期获取临时留置静脉导管评估数据列表
        /// </summary>
        public string GetEstimateVenousCatheterByHemoIdAndSingleDate
        {
            get
            {
                return @"SELECT E.ID,
                        E.CREATE_DATE,
                        DECODE(E.APPLICATION,'1','√','×') as APPLICATION,
                        DECODE(E.SUTURE,'1','√','×') as SUTURE,
                        E.HEMODIALYSIS_ID,
                        DECODE(E.CATHETER,'1','√','×') as CATHETER,
                        DECODE(E.CLEAN_AND_DRY,'1','√','×') as CLEAN_AND_DRY,
                        DECODE(E.FLOW_POLLUTION,'1','√','×') as FLOW_POLLUTION,
                        DECODE(E.RED,'1','√','×') as RED,
                        DECODE(E.SWOLLEN,'1','√','×') as SWOLLEN,
                        DECODE(E.HOT,'1','√','×') as HOT,
                        DECODE(E.PAIN,'1','√','×') as PAIN,
                        DECODE(E.ARTERY_CLARIFICATION,'1','√','×') as ARTERY_CLARIFICATION,
                        DECODE(E.ARTERY_BLOOD,'1','√','×') as ARTERY_BLOOD,
                        DECODE(E.ARTERY_FLOW_VELOCITY,'1','√','×') as ARTERY_FLOW_VELOCITY,
                        DECODE(E.ARTERY_REFLUX,'1','√','×') as ARTERY_REFLUX,
                        DECODE(E.ARTERY_THROMBOSIS,'1','√','×') as ARTERY_THROMBOSIS,
                        DECODE(E.VEIN_CLARIFICATION,'1','√','×') as VEIN_CLARIFICATION,
                        DECODE(E.VEIN_BLOOD,'1','√','×') as VEIN_BLOOD,
                        DECODE(E.VEIN_FLOW_VELOCITY,'1','√','×') as VEIN_FLOW_VELOCITY,
                        DECODE(E.VEIN_REFLUX,'1','√','×') as VEIN_REFLUX,
                        DECODE(E.VEIN_THROMBOSIS,'1','√','×') as VEIN_THROMBOSIS,
                        DECODE(E.FLOW_BETTER,'1','√','×') as FLOW_BETTER,
                        DECODE(E.SUCTION,'1','√','×') as SUCTION,
                        E.BLOOD_LEAD_END,
                        E.POSITION_REQUIREMENT,
                        E.HEPARIN_SODIUM,
                        E.UROKINASE,
                        E.OTHER,
                        DECODE(E.IN_DRESSING,'1','√','×') as IN_DRESSING,
                        DECODE(E.INFECTED,'1','√','×') as INFECTED,
                        DECODE(E.THROMBOLYSIS,'1','√','×') as THROMBOLYSIS
                        FROM MED_ESTIMATE_VENOUS_CATHETER E
                        WHERE TO_CHAR(E.CREATE_DATE,'YYYY-MM')=TO_CHAR(:CREATE_DATE,'YYYY-MM')
                        AND E.HEMODIALYSIS_ID=:HEMOID
                        AND E.IS_DELETE='0'";
            }
        }

        /// <summary>
        /// 根据ID删除长期留置静脉导管评估数据
        /// </summary>
        public string DeleteEstimateLongVenousById
        {
            get
            {
                return @"UPDATE MED_ESTIMATE_LONG_VENOUS SET IS_DELETE='1' WHERE ID=:ID";
            }
        }

        /// <summary>
        /// 根据ID删除临时留置静脉导管评估数据
        /// </summary>
        public string DeleteEstimateVenousCatheterById
        {
            get
            {
                return @"UPDATE MED_ESTIMATE_VENOUS_CATHETER SET IS_DELETE='1' WHERE ID=:ID";
            }
        }
        public string GetWorkloadByParamsFZ
        {
            get
            {
                return @"SELECT Z.NURSE_ID, T2.NAME, T1.ITEM_NAME, COUNT(1) AS COUNT
                          FROM (SELECT T.CURE_ID,
                                       (SELECT T1.NURSE_ID
                                          FROM MED_HEMODIALYSIS_PARAMETERS T1
                                         WHERE T.CURE_ID = T1.CURE_ID
                                           AND ROWNUM = 1) AS NURSE_ID,
                                       T.CURE_CREATE_DATE,
                                       T.PURIFICATION_MODE
                                  FROM MED_CURE_MAIN T
                                 WHERE T.CURE_STATUS != '4') Z
                          LEFT JOIN MED_COMMON_ITEMLIST T1
                            ON Z.PURIFICATION_MODE = T1.ITEM_ID
                          LEFT JOIN MED_STAFF_DICT T2
                            ON Z.NURSE_ID = T2.EMP_NO
                         WHERE Z.CURE_CREATE_DATE >= TRUNC(:BEGINDATE)
                           AND Z.CURE_CREATE_DATE <= TRUNC(:ENDDATE)
                           AND Z.NURSE_ID IS NOT NULL
                         GROUP BY Z.NURSE_ID, T2.NAME, T1.ITEM_NAME";
            }
        }
        public string GetWorkloadByParams
        {
            get
            {
                return @"SELECT ID,
                               WORKDATE,
                               DECODE(WORKCLASSNUM,'0','上午','1','下午','2','晚班','急诊') as WORKCLASSNUM,
                               LSQYTX,
                               XYTXLG,
                               XYGL,
                               WGSXT,
                               GTLTX,
                               NLCC,
                               DGHLLS,
                               DGHLCQ,
                               LSZGSCSY,
                               CQZGSCSY,
                               XTJC,
                               XDJH,
                               XFFS,
                               QJRC,
                               SW,
                               BZ,
                               BW,
                               TJR,
                               REMARK,
                               DJTX,
                               XJZH,
                               HMCL,
                               DHSXF,
                               TWXHKN,
                               ACTJC,
                               XQFX,
                               GYSXYTX,
                               WLBSY,
                               SYBSY,
                               JYHXQSY,
                               FYXYQSY,
                               XTZFTLC,
                               FMTXLC,
                               XTZSYZLC,
                               XTLS,
                               XLLS,
                               WORKAREA,
                               (SELECT ITEM_NAME FROM MED_COMMON_ITEMLIST WHERE ITEM_ID = WORKAREA) AREANAME,
                                 (SELECT T.NAME FROM MED_STAFF_DICT T WHERE T.EMP_NO= TJR ) NURSE_ID
                          FROM MED_WORKLOAD
                         WHERE WORKDATE BETWEEN :BEGINDATE AND :ENDDATE ORDER BY WORKDATE ASC ,AREANAME,WORKCLASSNUM";
            }
        }
        public string GetWorkLoadCountByDate
        {
            get
            {
                return @"SELECT  sys_guid() ||'1' ID,
                               WORKDATE,
                               SUM(NVL(LSQYTX,0)) LSQYTX,
                               SUM(NVL(XYTXLG,0)) XYTXLG,
                               SUM(NVL(XYGL,0)) XYGL,
                               SUM(NVL(WGSXT,0)) WGSXT,
                               SUM(NVL(GTLTX,0)) GTLTX,
                               SUM(NVL(NLCC,0)) NLCC,
                               SUM(NVL(DGHLLS,0)) DGHLLS,
                               SUM(NVL(DGHLCQ,0)) DGHLCQ,
                               SUM(NVL(LSZGSCSY,0)) LSZGSCSY,
                               SUM(NVL(CQZGSCSY,0)) CQZGSCSY,
                               SUM(NVL(XTJC,0)) XTJC,
                               SUM(NVL(XDJH,0)) XDJH,
                               SUM(NVL(XFFS,0)) XFFS,
                               SUM(NVL(QJRC,0)) QJRC,
                               SUM(NVL(SW,0)) SW,
                               SUM(NVL(BZ,0)) BZ,
                               SUM(NVL(BW,0)) BW,
                               SUM(NVL(DJTX,0)) DJTX,
                               SUM(NVL(XJZH,0)) XJZH,
                               SUM(NVL(HMCL,0)) HMCL,
                               SUM(NVL(DHSXF,0)) DHSXF,
                               SUM(NVL(TWXHKN,0)) TWXHKN,
                               SUM(NVL(ACTJC,0)) ACTJC,
                               SUM(NVL(XQFX,0)) XQFX,
                               SUM(NVL(GYSXYTX,0)) GYSXYTX,
                               SUM(NVL(WLBSY,0)) WLBSY,
                               SUM(NVL(SYBSY,0)) SYBSY,
                               SUM(NVL(JYHXQSY,0)) JYHXQSY,
                               SUM(NVL(FYXYQSY,0)) FYXYQSY,
                               SUM(NVL(XTZFTLC,0)) XTZFTLC,
                               SUM(NVL(FMTXLC,0)) FMTXLC,
                               SUM(NVL(XTZSYZLC,0)) XTZSYZLC,
                               SUM(NVL(XTLS,0)) XTLS,
                               SUM(NVL(XLLS,0)) XLLS
                          FROM MED_WORKLOAD
                         WHERE WORKDATE BETWEEN :BEGINDATE AND :ENDDATE
                         GROUP BY WORKDATE
                         ORDER BY WORKDATE ASC";
            }
        }
        public string GetWorkLoadNurseCountByDate
        {
            get
            {
                return @"SELECT sys_guid() ||'1' ID,
                       T.NAME AS TJR,
                       SYSDATE AS WORKDATE,
                       SUM(NVL(LSQYTX,0)) LSQYTX,
                       SUM(NVL(XYTXLG,0)) XYTXLG,
                       SUM(NVL(XYGL,0)) XYGL,
                       SUM(NVL(WGSXT,0)) WGSXT,
                       SUM(NVL(GTLTX,0)) GTLTX,
                       SUM(NVL(NLCC,0)) NLCC,
                       SUM(NVL(DGHLLS,0)) DGHLLS,
                       SUM(NVL(DGHLCQ,0)) DGHLCQ,
                       SUM(NVL(LSZGSCSY,0)) LSZGSCSY,
                       SUM(NVL(CQZGSCSY,0)) CQZGSCSY,
                       SUM(NVL(XTJC,0)) XTJC,
                       SUM(NVL(XDJH,0)) XDJH,
                       SUM(NVL(XFFS,0)) XFFS,
                       SUM(NVL(QJRC,0)) QJRC,
                       SUM(NVL(SW,0)) SW,
                       SUM(NVL(BZ,0)) BZ,
                       SUM(NVL(BW,0)) BW,
                       SUM(NVL(DJTX,0)) DJTX,
                       SUM(NVL(XJZH,0)) XJZH,
                       SUM(NVL(HMCL,0)) HMCL,
                       SUM(NVL(DHSXF,0)) DHSXF,
                       SUM(NVL(TWXHKN,0)) TWXHKN,
                       SUM(NVL(ACTJC,0)) ACTJC,
                       SUM(NVL(XQFX,0)) XQFX,
                       SUM(NVL(GYSXYTX,0)) GYSXYTX,
                       SUM(NVL(WLBSY,0)) WLBSY,
                       SUM(NVL(SYBSY,0)) SYBSY,
                       SUM(NVL(JYHXQSY,0)) JYHXQSY,
                       SUM(NVL(FYXYQSY,0)) FYXYQSY,
                       SUM(NVL(XTZFTLC,0)) XTZFTLC,
                       SUM(NVL(FMTXLC,0)) FMTXLC,
                       SUM(NVL(XTZSYZLC,0)) XTZSYZLC,
                       SUM(NVL(XTLS,0)) XTLS,
                       SUM(NVL(XLLS,0)) XLLS
                  FROM MED_WORKLOAD
                  LEFT JOIN MED_STAFF_DICT T
                    ON TJR = T.EMP_NO
                 WHERE WORKDATE BETWEEN :BEGINDATE AND :ENDDATE
                 GROUP BY T.NAME
                 ORDER BY T.NAME ASC";
            }
        }
        public string GetComplicationByParams
        {
            get
            {
                return @"SELECT T.*,
                            (SELECT L.ITEM_NAME
                                      FROM MED_COMMON_ITEMLIST L
                                     WHERE L.ITEM_ID = T.FIRST_PURIFIER_MODEL) AS FIRSTPURIFIERNAME,
                                   T.MACHINE_ID || ' ' || T.MACHINE_ID_TAG AS MACHINENAME,
                                   (SELECT P.NAME
                                      FROM MED_PATIENTS P
                                     WHERE P.HEMODIALYSIS_ID = T.HEMODIALYSIS_ID) PATIENTNAME,
                                (SELECT D.NAME FROM MED_STAFF_DICT D WHERE D.EMP_NO = T.NURSE_ID) NURSENAME
                            FROM MED_COMPLICATION_OTHER T WHERE WORK_DATE BETWEEN :BEGINDATE AND :ENDDATE ORDER BY WORK_DATE ASC";
            }
        }
        public string GetWorkloadByParmas
        {
            get
            {
                return @"SELECT *
                          FROM MED_WORKLOAD T
                         WHERE T.WORKAREA = :WORKAREA
                           AND T.WORKCLASSNUM = :WORKCLASSNUM
                           AND TRUNC(T.WORKDATE) = TRUNC(:WORKDATE)";
            }
        }
        public string GetScheduleRemarkByDate
        {
            get
            {
                return @"SELECT *
                          FROM (SELECT *
                                  FROM MED_SCHEDULEREMARK T        
                                 ORDER BY T.BEGINTIME DESC)
                         WHERE ROWNUM = 1";
            }

        }

        /// <summary>
        /// 舒适度评价表统计查询sql add by jiangguang 14.12.22
        /// </summary>
        public string GetSubjectiveComfortData
        {

            get
            {
                return @"SELECT DISTINCT *
                  FROM (SELECT P.NAME PATIENTNAME,
                               (SELECT BL.SYSTOLIC_PRESSURE || '/' || BL.DIASTOLIC_PRESSURE
                                  FROM MED_HEMODIALYSIS_PARAMETERS BL
                                 WHERE BL.CURE_ID IN
                                       (SELECT CM.CURE_ID
                                          FROM MED_CURE_MAIN CM
                                         WHERE CM.HEMODIALYSIS_ID = P.HEMODIALYSIS_ID
                                           AND CM.CURE_STATUS != '4'
                                           AND TO_CHAR(CM.CURE_CREATE_DATE, 'yyyy-MM') =
                                               :DATEMONTH)
                                   AND BL.SYSTOLIC_PRESSURE > 0
                                   AND ROWNUM = 1) BLOODINFO,
                               (SELECT AVG(FREQUENCY_HOURS)
                                  FROM MED_CURE_MAIN
                                 WHERE HEMODIALYSIS_ID = P.HEMODIALYSIS_ID
                                   AND CURE_STATUS != '4'
                                   AND TO_CHAR(CURE_CREATE_DATE, 'yyyy-MM') = :DATEMONTH) FREQUENCY_HOURS,
                               (SELECT IL.ITEM_NAME
                                  FROM MED_COMMON_ITEMLIST IL
                                 WHERE IL.ITEM_ID IN
                                       (SELECT L.VASCULAR_ACCESS_TYPE
                                          FROM MED_CURE_MAIN Z
                                          LEFT JOIN MED_VASCULAR_ACCESS L
                                            ON Z.VASCULAR_ACCESS_ID = l.vascular_access_id
                                         WHERE Z.HEMODIALYSIS_ID = P.HEMODIALYSIS_ID
                                           AND CURE_STATUS != '4'
                                           AND L.HEMODIALYSIS_ID = Z.Hemodialysis_Id
                                           AND TO_CHAR(CURE_CREATE_DATE, 'yyyy-MM') =
                                               :DATEMONTH)
                                   AND ROWNUM = 1) VASCULAR_ACCESS_ID,
                               (select count(CURE_ID)
                                  from MED_CURE_MAIN
                                 where HEMODIALYSIS_ID = P.HEMODIALYSIS_ID
                                   AND CURE_STATUS != '4'
                                   and SUBJECTIVE_COMFORT = '0'
                                   AND TO_CHAR(CURE_CREATE_DATE, 'yyyy-MM') = :DATEMONTH) SUBJECTIVE_COMFORT0,
               
                               (select count(CURE_ID)
                                  from MED_CURE_MAIN
                                 where HEMODIALYSIS_ID = P.HEMODIALYSIS_ID
                                   AND CURE_STATUS != '4'
                                   and SUBJECTIVE_COMFORT = '1'
                                   AND TO_CHAR(CURE_CREATE_DATE, 'yyyy-MM') = :DATEMONTH) SUBJECTIVE_COMFORT1,
               
                               (select count(CURE_ID)
                                  from MED_CURE_MAIN
                                 where HEMODIALYSIS_ID = P.HEMODIALYSIS_ID
                                   AND CURE_STATUS != '4'
                                   and SUBJECTIVE_COMFORT = '2'
                                   AND TO_CHAR(CURE_CREATE_DATE, 'yyyy-MM') = :DATEMONTH) SUBJECTIVE_COMFORT2
                          FROM MED_PATIENTS P
                         INNER JOIN MED_CURE_MAIN MP
                            ON P.HEMODIALYSIS_ID = MP.HEMODIALYSIS_ID
                           AND MP.CURE_STATUS != '4'
                           AND TO_CHAR(MP.CURE_CREATE_DATE, 'yyyy-MM') = :DATEMONTH)";
            }
        }

        /// <summary>
        /// 获取患者借药管理sql add by jiangguang 14.12.23
        /// </summary>
        public string GetPatientBrowDrugList
        {

            get
            {
                return @"SELECT T.*,DECODE(T.BBORROW_USER,'','0','1') ISBACK FROM
(SELECT DTL.BORROW_ID,PAT.NAME PATIENTNAME,DTL.BORROW_DAY,DTL.MEDICINE_NAME,DTL.MEDICINE_COUNT,DTL.MEDICINE_UNIT,DTL.BORROW_USER,DTL1.BORROW_USER BBORROW_USER,DTL1.BORROW_DAY BBORROW_DAY FROM MED_BORROW_MEDICINE_DETAIL DTL INNER JOIN MED_PATIENTS PAT
ON DTL.HEMODIALYSIS_ID=PAT.HEMODIALYSIS_ID LEFT JOIN MED_BORROW_MEDICINE_DETAIL DTL1 ON DTL.BORROW_ID=DTL1.OLD_ID where dtl.borrow_type='0' AND DTL.BORROW_DAY BETWEEN :STARTTIME AND :ENDTIME AND PAT.NAME LIKE '%'||:PATNAME||'%' ) T";
            }
        }
        /// <summary>
        /// 获取患者借药品记录
        /// </summary>
        public string GetPatientBrowDrugListByID
        {
            get
            {
                return " SELECT * FROM MED_BORROW_MEDICINE_DETAIL T WHERE T.BORROW_ID=:BORROW_ID ";
            }

        }

        public string GetCheckUserBillByBILL_CURE_ID
        {
            get
            {
                return "SELECT * FROM MED_CURE_MAIN_BILL WHERE RECIPE_ID=:RECIPE_ID";
            }

        }
        public string GetUserWeekBillByBILL_CURE_ID
        {
            get
            {
                return "SELECT * FROM MED_CURE_MAIN_BILL T1 WHERE T1. BILL_DATE BETWEEN  TRUNC(:HEMODIALYSIS_IDDATE,'IW') AND TRUNC(:HEMODIALYSIS_IDDATE,'IW') +7 AND T1.HEMODIALYSIS_ID=:HEMODIALYSIS_ID";
            }

        }

        public string GetUserBillListByStartEndDate
        {
            get
            {
                return string.Format(baseString, "");
            }
        }
        private string baseString = @"SELECT T.HEMODIALYSIS_ID,
                                               T.PATIENTNAME,
                                               T.FREECOUNT,
                                               T.ALLCOUNT,
                                               (ALLCOUNT - FREECOUNT) BILLCOUNT,
                                               T.ALLFEE,
                                               T.PAYFEE,
                                               (T.PAYFEE - T.ALLFEE) RESTFEE
                                          FROM (SELECT DISTINCT HEMODIALYSIS_ID,
                                                                PATIENTNAME,
                                                                SUM(FREECOUNT) FREECOUNT,
                                                                COUNT(HEMODIALYSIS_ID) ALLCOUNT,
                                                                SUM(ALLFEE) ALLFEE,
                                                         NVL((SELECT SUM(P.PREPAYCOST)
                                                                          FROM MED_PATIENT_PREPAY P
                                                                         WHERE P.HEMODIALYSIS_ID = Z.HEMODIALYSIS_ID
                                                                           AND P.PAYTIME BETWEEN :BEGINDATE AND:ENDDATE),
                                                                        0) PAYFEE
                                                  FROM (SELECT PAT.HEMODIALYSIS_ID,
                                                               PAT.NAME PATIENTNAME,
                                                               NVL((SELECT 1
                                                                     FROM MED_CURE_MAIN_BILL BILL
                                                                    WHERE BILL.RECIPE_ID = CMB.RECIPE_ID
                                                                      AND BILL.BILL_TYPE = '21'
                                                                      AND ROWNUM = 1),
                                                                   0) FREECOUNT,
                                                               NVL((SELECT SUM(BILL.BILL_PRICE)
                                                                     FROM MED_CURE_MAIN_BILL BILL
                                                                    WHERE BILL.RECIPE_ID = CMB.RECIPE_ID),
                                                                   0) ALLFEE
                                                          FROM MED_PATIENTS PAT
                                                         INNER JOIN MED_CURE_MAIN CMB
                                                            ON PAT.HEMODIALYSIS_ID = CMB.HEMODIALYSIS_ID
                                                         WHERE {0}
                                                                CMB.CURE_CREATE_DATE BETWEEN :BEGINDATE AND :ENDDATE)Z
                                                 GROUP BY HEMODIALYSIS_ID, PATIENTNAME) T";

        public string GetUserBillListByStartEndDateHomeID
        {
            get
            {
                return string.Format(baseString, " PAT.HEMODIALYSIS_ID=:HEMODIALYSIS_ID AND ");
            }
        }
        /// <summary>
        /// 查询患者治疗账单记录 jiangg 050111
        /// </summary>
        public string GetPatientBillByHemoIDListInfo
        {
            get
            {
                return @"SELECT * FROM MED_CURE_MAIN_BILL T WHERE (T.BILL_TYPE='20' OR  T.BILL_TYPE='21') AND T.HEMODIALYSIS_ID=:HEMODIALYSIS_ID AND T.BILL_DATE BETWEEN :BEGINDATE AND :ENDDATE";
            }
        }
        /// <summary>
        /// 查询患者透析评估记录 jiangg 050121
        /// </summary>
        public string GetAssessmentByParams
        {
            get
            {
                return @"SELECT * FROM MED_PATIENTS_ASSESSMENT WHERE HEMODIALYSIS_ID=:HEMODIALYSIS_ID AND ASSESSMENT_DATE BETWEEN :BEGINDATE AND :ENDDATE AND STATUS=1 ";
            }
        }
        public string GetAssessmentByAssID
        {
            get
            {
                return @"SELECT * FROM MED_PATIENTS_ASSESSMENT_ATTR WHERE ASSESSMENT_ID=:ASSESSMENT_ID";
            }
        }
        public string DeleteAssessmentInfosByAssessmentId
        {
            get
            {
                return @"DELETE FROM MED_PATIENTS_ASSESSMENT T WHERE T.ASSESSMENT_ID = :ASSESSMENT_ID";
            }
        }
        public string DeleteAssessmentArrtInfosByAssessmentId
        {
            get
            {
                return @"DELETE FROM MED_PATIENTS_ASSESSMENT_ATTR T WHERE T.ASSESSMENT_ID = :ASSESSMENT_ID";
            }
        }
        public string GetAssParamByHemoID
        {
            get
            {
                return @"SELECT t.recipe_id,T.HEMODIALYSIS_ID,
                                       (SELECT L.ITEM_NAME
                                          FROM MED_COMMON_ITEMLIST L
                                         WHERE L.ITEM_ID = T.PURIFICATION_MODE) PURIFICATION_MODE,
                                       T.FREQUENCY_TIMES,
                                       T.FREQUENCY_HOURS,
                                       (SELECT P.UFR
                                          FROM MED_HEMO_RECIPE P
                                         WHERE P.HEMODIALYSIS_ID = :HEMODIALYSIS_ID AND RECIPE_TYPE='0'
                                           AND TRUNC(RECIPE_DATE) = TRUNC(SYSDATE)) AS UFR,
                                       (SELECT P.TODAY_WEIGHT
                                          FROM MED_HEMO_RECIPE P
                                         WHERE P.HEMODIALYSIS_ID = :HEMODIALYSIS_ID  AND RECIPE_TYPE='0'
                                           AND TRUNC(RECIPE_DATE) = TRUNC(SYSDATE)) AS TODAY_WEIGHT,
                                       (SELECT P.TODAY_BLOODA 
                                          FROM MED_HEMO_RECIPE P
                                         WHERE P.HEMODIALYSIS_ID = :HEMODIALYSIS_ID  AND RECIPE_TYPE='0'
                                           AND TRUNC(RECIPE_DATE) = TRUNC(SYSDATE)) AS TODAY_BLOODA,
                                        (SELECT P.TODAY_BLOODB 
                                      FROM MED_HEMO_RECIPE P
                                     WHERE P.HEMODIALYSIS_ID = :HEMODIALYSIS_ID  AND RECIPE_TYPE='0'
                                       AND TRUNC(RECIPE_DATE) = TRUNC(SYSDATE)) AS TODAY_BLOODB,
                                       (SELECT (SELECT L.ITEM_NAME
                                                  FROM MED_COMMON_ITEMLIST L
                                                 WHERE L.ITEM_ID = P.THERAPEUTIC_METHOD) AS THERAPEUTIC_METHOD
                                          FROM MED_HEMO_RECIPE P
                                         WHERE P.HEMODIALYSIS_ID = :HEMODIALYSIS_ID  AND RECIPE_TYPE='0'
                                           AND TRUNC(RECIPE_DATE) = TRUNC(SYSDATE)) AS THERAPEUTIC_METHOD,
                                       (SELECT P.FIRST_DRUG_DOSAGE
                                          FROM MED_HEMO_RECIPE P
                                         WHERE P.HEMODIALYSIS_ID = :HEMODIALYSIS_ID  AND RECIPE_TYPE='0'
                                           AND TRUNC(RECIPE_DATE) = TRUNC(SYSDATE)) AS FIRST_DRUG_DOSAGE,
                                       (SELECT (SELECT L.ITEM_NAME
                                                  FROM MED_COMMON_ITEMLIST L
                                                 WHERE L.ITEM_ID = P.FIRST_DRUG_UNIT) AS FIRST_DRUG_UNIT
                                          FROM MED_HEMO_RECIPE P
                                         WHERE P.HEMODIALYSIS_ID = :HEMODIALYSIS_ID  AND RECIPE_TYPE='0'
                                           AND TRUNC(RECIPE_DATE) = TRUNC(SYSDATE)) AS FIRST_DRUG_UNIT
                                  FROM MED_HEMO_RECIPE T
                                 WHERE HEMODIALYSIS_ID = :HEMODIALYSIS_ID
                                   AND RECIPE_TYPE = '1'
                                   AND STATUS = '1'";
            }
        }
        #endregion

        /// <summary>
        /// 根据透析编号获取患者URR统计数据
        /// </summary>
        public string GetPatientURRByHemoId
        {
            get { return @"SELECT * FROM MED_PATIENTS_URR WHERE HEMODIALYSIS_ID=:HEMODIALYSIS_ID"; }
        }

        /// <summary>
        /// 根据透析编号获取充分性评估数据
        /// </summary>
        public string GetEstimateSufficiencyByHemoId
        {
            get { return @"SELECT T.*,T.URR*100||'%' AS DISPLAY_URR,T.TS*100||'%' AS DISPLAY_TS,DECODE(T.IS_FEMALE,'1','是','否') AS FEMALE,DECODE(T.IS_BLACK,'1','是','否') AS BLACK FROM MED_ESTIMATE_SUFFICIENCY T WHERE T.HEMODIALYSIS_ID=:HEMODIALYSIS_ID AND T.FLAG=:FLAG AND T.IS_DELETE='0' ORDER BY T.CREATE_DATE"; }
        }

        /// <summary>
        /// 根据Flag获取充分性评估数据
        /// </summary>
        public string GetEstimateSufficiencyByFlag
        {
            get { return @"SELECT T.*,P.NAME,P.SEX,DECODE(T.URR,NULL,'',T.URR*100||'%') AS DISPLAY_URR,DECODE(T.TS,NULL,'',T.TS*100||'%') AS DISPLAY_TS,DECODE(T.IS_FEMALE,'1','是','0','否',T.IS_FEMALE) AS FEMALE,DECODE(T.IS_BLACK,'1','是','0','否',T.IS_BLACK) AS BLACK FROM MED_ESTIMATE_SUFFICIENCY T INNER JOIN MED_PATIENTS P ON T.HEMODIALYSIS_ID = P.HEMODIALYSIS_ID WHERE T.FLAG in({0}) AND T.IS_DELETE='0' AND (P.IS_DELETE != '1' OR P.IS_DELETE IS NULL) ORDER BY T.CREATE_DATE,T.FLAG"; }
        }

        /// <summary>
        /// 根据透析编号、日期获取充分性评估数据
        /// </summary>
        public string GetEstimateSufficiencyByHemoIdAndDate
        {
            get
            {
                return @"SELECT T.*,T.URR*100||'%' AS DISPLAY_URR,T.TS*100||'%' AS DISPLAY_TS,DECODE(T.IS_FEMALE,'1','是','否') AS FEMALE,DECODE(T.IS_BLACK,'1','是','否') AS BLACK,
                                 '透析号' || HEMODIALYSIS_ID || ' ' || '尿素清除指数' || SPKT_V || ' ' ||
                               '透前血尿素' || BEFORE_BLOODUREA || ' ' || '透后血尿素' || AFTER_BLOODUREA || ' ' || '时间' || TIME || ' ' ||
                               '超滤量' || UF || ' ' || URR || ' ' || '录入日期' || CREATE_DATE AS DISPLAY
                            FROM MED_ESTIMATE_SUFFICIENCY T WHERE HEMODIALYSIS_ID=:HEMODIALYSIS_ID AND FLAG=:FLAG AND TRUNC(CREATE_DATE)>=TRUNC(:BEGINDATE) AND TRUNC(CREATE_DATE)<=TRUNC(:ENDDATE) AND IS_DELETE='0' ORDER BY CREATE_DATE";
            }
        }

        /// <summary>
        /// 根据Flag、日期获取充分性评估数据
        /// </summary>
        public string GetEstimateSufficiencyByFlagAndDate
        {
            get
            {
                return @"SELECT T.*,P.NAME,P.SEX,DECODE(T.URR,NULL,'',T.URR*100||'%') AS DISPLAY_URR,DECODE(T.TS,NULL,'',T.TS*100||'%') AS DISPLAY_TS,DECODE(T.IS_FEMALE,'1','是','0','否',T.IS_FEMALE) AS FEMALE,DECODE(T.IS_BLACK,'1','是','0','否',T.IS_BLACK) AS BLACK,
                                 '透析号' || T.HEMODIALYSIS_ID || ' ' || '尿素清除指数' || SPKT_V || ' ' ||
                               '透前血尿素' || BEFORE_BLOODUREA || ' ' || '透后血尿素' || AFTER_BLOODUREA || ' ' || '时间' || TIME || ' ' ||
                               '超滤量' || UF || ' ' || URR || ' ' || '录入日期' || T.CREATE_DATE AS DISPLAY
                            FROM MED_ESTIMATE_SUFFICIENCY T INNER JOIN MED_PATIENTS P ON T.HEMODIALYSIS_ID = P.HEMODIALYSIS_ID WHERE T.FLAG in({0}) AND TRUNC(T.CREATE_DATE)>=TRUNC({1}) AND TRUNC(T.CREATE_DATE)<=TRUNC({2}) AND T.IS_DELETE='0' AND (P.IS_DELETE != '1' OR P.IS_DELETE IS NULL) ORDER BY T.CREATE_DATE,T.FLAG";
            }
        }

        /// <summary>
        /// 根据透析编号、日期获取充分性评估数据
        /// </summary>
        public string GetEstimateSufficiencyByHemoIdAndCreateDate
        {
            get { return @"SELECT * FROM MED_ESTIMATE_SUFFICIENCY WHERE HEMODIALYSIS_ID=:HEMODIALYSIS_ID AND FLAG=:FLAG AND TRUNC(CREATE_DATE)=TRUNC(:CREATEDATE) AND IS_DELETE='0'"; }
        }

        /// <summary>
        /// 根据ID删除充分性评估记录
        /// </summary>
        public string DeleteEstimateSufficiencyById
        {
            get { return @"UPDATE MED_ESTIMATE_SUFFICIENCY SET IS_DELETE='1' WHERE ID=:ID"; }
        }

        public string GetPastWeightListByParams
        {
            get
            {
                return @"SELECT TO_CHAR(T.CURE_CREATE_DATE, 'YYYY-MM-DD') as 日期,
                               T.AFTER_DRY_WEIGHT as 值 ,
                              '日期:' ||   TO_CHAR(T.CURE_CREATE_DATE, 'YYYY-MM-DD') || '  体重:' || T.AFTER_DRY_WEIGHT AS DISPLAY
                          FROM MED_CURE_MAIN T
                         WHERE T.HEMODIALYSIS_ID = :HEMODIALYSIS_ID
                           AND T.AFTER_DRY_WEIGHT !='0'
                           AND T.CURE_CREATE_DATE BETWEEN :BEGINDATE AND :ENDDATE
                         ORDER BY T.CURE_CREATE_DATE DESC";
            }
        }
        public string GetPastBloodPresureListByParams
        {
            get
            {
                return @" SELECT TO_CHAR(S.CREATE_DATE, 'YYYY-MM-DD HH24:MI') 日期,
                                   S.SYSTOLIC_PRESSURE || '/' || S.DIASTOLIC_PRESSURE 值,
                               '日期:' || TO_CHAR(S.CREATE_DATE, 'YYYY-MM-DD HH24:MI')|| '  血压:' || S.SYSTOLIC_PRESSURE || '/' || S.DIASTOLIC_PRESSURE AS DISPLAY
                              FROM MED_HEMODIALYSIS_PARAMETERS S
                             WHERE S.CURE_ID IN (SELECT T.CURE_ID
                                                   FROM MED_CURE_MAIN T
                                                  WHERE T.HEMODIALYSIS_ID = :HEMODIALYSIS_ID)
                               AND S.SYSTOLIC_PRESSURE != '0'
                               AND S.CREATE_DATE BETWEEN :BEGINDATE AND :ENDDATE
                             ORDER BY S.CREATE_DATE DESC";
            }
        }

        #region 获取未评估未做检验项目的病人数据
        public string GetInfectedPatients
        {
            get
            {
                return @"SELECT *
                          FROM MED_PATIENTS P
                         WHERE  P.IS_NEW = '0' AND NOT EXISTS (select *
                                  from MED_LAB_TEST_MASTER t
                                 WHERE T.BARCODE = P.HEMODIALYSIS_ID
                                    AND (TEST_CAUSE LIKE '%乙肝%' OR TEST_CAUSE LIKE '%丙肝%' OR TEST_CAUSE LIKE '%梅毒%' OR
                                   TEST_CAUSE LIKE '%艾滋病%' OR TEST_CAUSE LIKE '%HIV%' OR TEST_CAUSE LIKE '%HCV%' OR TEST_CAUSE LIKE '%RPR%')
                                   AND T.RESULTS_RPT_DATE_TIME >SYSDATE -180)";
            }
        }
        public string GetRouBloodPatients
        {
            get
            {
                return @"SELECT *
                              FROM MED_PATIENTS P
                             WHERE  P.IS_NEW = '0' AND NOT EXISTS (select *
                                      from MED_LAB_TEST_MASTER t
                                     WHERE T.BARCODE = P.HEMODIALYSIS_ID
                                       AND T.TEST_CAUSE LIKE  '%血常规%'
                                       AND T.RESULTS_RPT_DATE_TIME >SYSDATE -30)";
            }
        }
        public string GetRenalPatients
        {
            get
            {
                return @"SELECT *
                          FROM MED_PATIENTS P
                         WHERE  P.IS_NEW = '0' AND NOT EXISTS (select *
                                  from MED_LAB_TEST_MASTER t
                                 WHERE T.BARCODE = P.HEMODIALYSIS_ID
                                   AND T.TEST_CAUSE LIKE '%肾功能%'
                                    AND T.RESULTS_RPT_DATE_TIME >SYSDATE -30)";
            }
        }

        /// <summary>
        /// 删除评估记录 jiangg 050121
        /// </summary>
        public string GetDeleteAssessmentByParams
        {
            get
            {
                return @"UPDATE MED_PATIENTS_ASSESSMENT SET STATUS='0',ASSESSMENT_NOTE=ASSESSMENT_NOTE||:ASSESSMENT_NOTE WHERE ASSESSMENT_ID=:ASSESSMENT_ID ";
            }
        }

        public string GetElectrolytePatients
        {
            get
            {
                return @"SELECT *
                          FROM MED_PATIENTS P
                         WHERE  P.IS_NEW = '0' AND NOT EXISTS (select *
                                  from MED_LAB_TEST_MASTER t
                                 WHERE T.BARCODE = P.HEMODIALYSIS_ID
                                   AND T.TEST_CAUSE LIKE '%电解质%'
                                    AND T.RESULTS_RPT_DATE_TIME >SYSDATE -90)";
            }
        }

        public string GetBasketPatients
        {
            get
            {
                return @"SELECT *
                          FROM MED_PATIENTS P
                         WHERE  P.IS_NEW = '0' AND EXISTS (SELECT *
                                  FROM MED_ESTIMATE_IN_BASKET T
                                 WHERE T.IS_DELETE != '1'
                                   AND T.HEMODIALYSIS_ID = P.HEMODIALYSIS_ID
                                   AND T.CREATE_DATE >SYSDATE -30)";
            }
        }
        public string GetLongVenousPatients
        {
            get
            {
                return @"SELECT *
                          FROM MED_PATIENTS P
                         WHERE  P.IS_NEW = '0' AND EXISTS (SELECT *
                                  FROM MED_ESTIMATE_LONG_VENOUS T
                                 WHERE T.IS_DELETE != '1'
                                   AND T.HEMODIALYSIS_ID = P.HEMODIALYSIS_ID
                                   AND T.CREATE_DATE >SYSDATE -30)";
            }
        }
        public string GetVenousCatheterPatients
        {
            get
            {
                return @"SELECT *
                          FROM MED_PATIENTS P
                         WHERE  P.IS_NEW = '0' AND EXISTS (SELECT *
                                  FROM MED_ESTIMATE_VENOUS_CATHETER T
                                 WHERE T.IS_DELETE != '1'
                                   AND T.HEMODIALYSIS_ID = P.HEMODIALYSIS_ID
                                   AND T.CREATE_DATE >SYSDATE -30)";
            }
        }
        public string GetAssessmentPatients
        {
            get
            {
                return @"SELECT *
                          FROM MED_PATIENTS P
                         WHERE P.IS_NEW = '0'
                           AND NOT EXISTS (SELECT *
                                  FROM MED_PATIENTS_ASSESSMENT T
                                 WHERE T.HEMODIALYSIS_ID = P.HEMODIALYSIS_ID
                                   AND T.STATUS = '1'
                                   AND T.ASSESSMENT_TYPE = '患者透析评估表'
                                   AND T.ASSESSMENT_DATE > SYSDATE - 30)";
            }
        }
        #endregion

        #region 科室总览
        public string GetCurrentWeekCount
        {
            get
            {
                //本周共排班XX人次
                return @"SELECT COUNT(1)
                          FROM MED_PATIENT_SCHEDULE T
                         WHERE T.DIALYSIS_DATE >= :BEGINDATE
                           AND T.DIALYSIS_DATE <= :ENDDATE";
            }
        }
        public string GetWeekedCount
        {
            get
            {
                //已上机XX人次
                return @"SELECT COUNT(1)
                          FROM MED_PATIENT_SCHEDULE T
                         WHERE T.DIALYSIS_DATE >= :BEGINDATE
                           AND T.DIALYSIS_DATE <= :ENDDATE
                           AND T.START_TIME IS NOT NULL";
            }
        }
        public string GetMoningCountPatients
        {
            get
            {
                //今天上午XX人
                return @"SELECT '上午' AS NAME,COUNT(1)COUNT
                          FROM MED_PATIENT_SCHEDULE T
                         WHERE T.DIALYSIS_DATE = TRUNC(SYSDATE)
                           AND T.BANCI_ID = '1'
                        UNION
                        SELECT '住院' AS NAME, COUNT(1) COUNT
                          FROM MED_PATIENT_SCHEDULE T
                         WHERE T.DIALYSIS_DATE = TRUNC(SYSDATE)
                           AND T.BANCI_ID = '1'
                           AND T.HEMODIALYSIS_ID IN
                               (SELECT S.HEMODIALYSIS_ID
                                  FROM MED_PATIENTS S
                                 WHERE S.TIME_TYPE = '住院')";
            }
        }
        public string GetMoningHemoPatients
        {
            get
            {
                //现在透析XX人   ：等待:总数减去透析的人   
                return @"SELECT COUNT(1)
                          FROM MED_PATIENT_SCHEDULE T
                         WHERE T.DIALYSIS_DATE = TRUNC(SYSDATE)
                           AND T.BANCI_ID='1' AND T.START_TIME IS NOT NULL";
            }
        }
        public string GetAfterCountPatients
        {
            get
            {
                //今日下午XX人
                return @"SELECT '下午' AS NAME,COUNT(1)COUNT
                          FROM MED_PATIENT_SCHEDULE T
                         WHERE T.DIALYSIS_DATE = TRUNC(SYSDATE)
                           AND T.BANCI_ID='2'
                          UNION
                         SELECT '住院' AS NAME, COUNT(1) COUNT
                          FROM MED_PATIENT_SCHEDULE T
                         WHERE T.DIALYSIS_DATE = TRUNC(SYSDATE)
                           AND T.BANCI_ID='2'
                             AND T.HEMODIALYSIS_ID IN
                               (SELECT S.HEMODIALYSIS_ID
                                  FROM MED_PATIENTS S
                                 WHERE S.TIME_TYPE = '住院')";
            }
        }
        public string GetAfterHemoPatients
        {
            get
            {
                //现在透析 XX人  等待:下午人减去现在透析的人就是等待的人  
                return @"SELECT COUNT(1)
                          FROM MED_PATIENT_SCHEDULE T
                         WHERE T.DIALYSIS_DATE = TRUNC(SYSDATE)
                           AND T.BANCI_ID='2'  AND T.START_TIME IS NOT NULL";
            }
        }
        public string GetFirstHemoPatients
        {
            get
            {
                //首次透析人数
                return @"SELECT COUNT(1)
                              FROM MED_PATIENT_SCHEDULE T
                             WHERE T.DIALYSIS_DATE >= :BEGINDATE
                               AND T.DIALYSIS_DATE <= :ENDDATE
                               AND NOT EXISTS
                             (SELECT N.HEMODIALYSIS_ID
                                      FROM MED_CURE_MAIN N
                                     WHERE N.HEMODIALYSIS_ID = T.HEMODIALYSIS_ID)";
            }
        }
        public string GetEmergeHemoPatients
        {
            get
            {
                //急诊患者人数
                return @"SELECT COUNT(1)
                          FROM MED_PATIENT_SCHEDULE T
                         WHERE T.DIALYSIS_DATE >= :BEGINDATE
                           AND T.DIALYSIS_DATE <= :ENDDATE
                           AND T.BANCI_ID in (select t.item_value
                                                from MED_COMMON_ITEMLIST t
                                               where t.item_type = '班次'
                                                 and t.item_name = '急诊')";
            }
        }
        public string GetInHosEmergePatients
        {
            get
            {
                //在科抢救患者的人数
                return @"SELECT COUNT(1)
                          FROM MED_PATIENT_SCHEDULE T,MED_CURE_MAIN L
                         WHERE T.DIALYSIS_DATE >= :BEGINDATE
                           AND T.DIALYSIS_DATE <= :ENDDATE
                           AND T.START_TIME IS NOT NULL
                           AND T.HEMODIALYSIS_ID = L.HEMODIALYSIS_ID 
                           AND L.SUMMARY2 IS NOT NULL";
            }
        }
        public string GetCRRTPatients
        {
            get
            {
                //CRRT患者
                return @"SELECT COUNT(1)
                          FROM MED_PATIENT_SCHEDULE T,MED_CURE_MAIN L
                         WHERE T.DIALYSIS_DATE >= TRUNC(SYSDATE) --:BEGINDATE
                          -- AND T.DIALYSIS_DATE <= :ENDDATE
                           AND T.HEMODIALYSIS_ID = L.HEMODIALYSIS_ID 
                           AND L.Purification_Mode= (SELECT L.ITEM_ID FROM MED_COMMON_ITEMLIST L WHERE L.ITEM_TYPE = '净化方式' AND L.ITEM_NAME = 'CRRT')";
            }
        }
        public string GetVasularAccessPatients
        {
            get
            {
                //血管通路手术 XX 台
                return @"SELECT 'a' ind, '通路个数' AS NAME, COUNT(1) AS COUNT
                          FROM MED_VASCULAR_ACCESS T
                         WHERE T.CREATE_DATE >=  :BEGINDATE
                           AND T.CREATE_DATE <= :ENDDATE
                        UNION
                        SELECT 'b' ind,'通畅数' AS NAME, COUNT(1) AS COUNT
                          FROM MED_CURE_MAIN T
                         WHERE T.VASCULAR_ACCESS_STATE = '1'
                           AND T.CURE_CREATE_DATE >=  :BEGINDATE
                           AND T.CURE_CREATE_DATE <= :ENDDATE
                        UNION
                        SELECT 'c' ind,'不通畅数' AS NAME, COUNT(1) AS COUNT
                          FROM MED_CURE_MAIN T
                         WHERE T.VASCULAR_ACCESS_STATE = '0' 
                           AND T.CURE_CREATE_DATE >=  :BEGINDATE
                           AND T.CURE_CREATE_DATE <= :ENDDATE";

                //@"SELECT COUNT(1)
                //                          FROM MED_VASCULAR_ACCESS T
                //                         WHERE T.CREATE_DATE >= :BEGINDATE
                //                           AND T.CREATE_DATE <= :ENDDATE";
            }
        }
        public string GetUnDilogsPatients
        {
            get
            {
                //未下诊断的 XX 人
                return @"SELECT COUNT(1)
                          FROM MED_PATIENT_SCHEDULE T, MED_PATIENTS S
                         WHERE T.HEMODIALYSIS_ID = S.HEMODIALYSIS_ID
                           AND S.DIAGNOSE IS NULL
                           AND T.DIALYSIS_DATE >= :BEGINDATE
                           AND T.DIALYSIS_DATE <= :ENDDATE";
            }
        }
        public string GetComplactionOtherPatients
        {

            get
            {
                //透析中并发症发生 XX次  ，不良事件发生 XX 起
                return @"SELECT COUNT(1)
                          FROM MED_CURE_MAIN T, MED_COMPLICATION_OTHER S
                         WHERE T.HEMODIALYSIS_ID = S.HEMODIALYSIS_ID
                           AND T.CURE_ID = S.CURE_ID
                           AND T.CURE_CREATE_DATE >= :BEGINDATE
                           AND T.CURE_CREATE_DATE <= :ENDDATE";

            }
        }
        public string GetEducationPatients
        {
            get
            {
                //宣教 XX  人  、  未宣教 XX   人
                return @"SELECT SUM(C1) C1, SUM(C2) C2
                              FROM (SELECT COUNT(1) AS C1, 0 AS C2
                                      FROM (SELECT T.HEMODIALYSIS_ID
                                              FROM MED_PATIENT_SCHEDULE T
                                             WHERE T.DIALYSIS_DATE >= :BEGINDATE
                                               AND T.DIALYSIS_DATE <= :ENDDATE
                                               AND T.HEMODIALYSIS_ID IN
                                                   (SELECT L.HEMODIALYSIS_ID
                                                      FROM MED_HEALTH_EDUCATION L
                                                     WHERE L.CREATE_DATE >= :BEGINDATE
                                                       AND L.CREATE_DATE <= :ENDDATE
                                                     GROUP BY L.HEMODIALYSIS_ID)
                                             GROUP BY T.HEMODIALYSIS_ID) Z
                                    UNION
                                    SELECT 0 AS C1, COUNT(1) AS C2
                                      FROM (SELECT T.HEMODIALYSIS_ID
                                              FROM MED_PATIENT_SCHEDULE T
                                             WHERE T.DIALYSIS_DATE >= :BEGINDATE
                                               AND T.DIALYSIS_DATE <= :ENDDATE
                                               AND T.HEMODIALYSIS_ID IN
                                                   (SELECT L.HEMODIALYSIS_ID
                                                      FROM MED_HEALTH_EDUCATION L
                                                     WHERE L.CREATE_DATE >= :BEGINDATE
                                                       AND L.CREATE_DATE <= :ENDDATE
                                                       AND L.HEALTH_TYPE = 'VASCULAR_ACCESS_4_1'
                                                     GROUP BY L.HEMODIALYSIS_ID)
                                             GROUP BY T.HEMODIALYSIS_ID) Z) K";
            }
        }

        public string GetComplicationByDialysisAndCure
        {
            get
            {
                return @"SELECT * FROM MED_COMPLICATION_OTHER WHERE HEMODIALYSIS_ID=:HEMODIALYSIS_ID AND CURE_ID=:CURE_ID";
            }
        }

        public string GetCurrentDutyUser
        {
            get
            {
                return @"SELECT T.TYPE,
                               T.USER_ID  --(SELECT C.NAME FROM MED_STAFF_DICT C WHERE C.EMP_NO = T.USER_ID) AS CURRENTNAME
                          FROM MED_USERS_WEEKDUTY T
                         WHERE TRUNC(T.DUTYDAY) = TRUNC(SYSDATE)
                         --AND (T.TYPE = 'D' OR T.OFFICEID = 'f949cf36-68cb-4953-9475-fa8ee5e6a6f0')";
            }
        }
        public string DeleteNurseWorkOverTimeByID
        {
            get
            {
                return @"DELETE MED_HEMO_WORKOVERTIME WHERE ID = :ID";
            }
        }
        public string GetNurseWorkOverTimeRecordByDate
        {
            get
            {
                return @"SELECT T.ID,
                               T.WORKDATE,
                               (SELECT NAME FROM MED_STAFF_DICT C WHERE C.EMP_NO = T.USERID) AS USERNAME,
                               T.USERID,
                               T.CURETYPE,
                               T.HEMODIALYSIS_ID,
                               (SELECT S.NAME
                                  FROM MED_PATIENTS S
                                 WHERE S.HEMODIALYSIS_ID = T.HEMODIALYSIS_ID) AS NAME,
                               T.WORKTIME,
                               T.CREATEBY,
                               T.CREATEDATE
                          FROM MED_HEMO_WORKOVERTIME T
                        WHERE TRUNC(T.WORKDATE) >=TRUNC(:BEGINDATE) AND TRUNC(T.WORKDATE) <= TRUNC(:ENDDATE)";
            }
        }
        #endregion

        #region 患者数据上传实现

        #region 全国数据上报平台
        public string GetDataReportPatientList
        {
            get
            {
                return @"SELECT T.*,
                               DECODE(T1.BASEINFO, NULL, '1', '2') ISUPLOAD, T1.BASEINFO,
                               DECODE(T1.BASEINFO, NULL, '未上传', '已上传') UPSTATE
                          FROM MED_PATIENTS T
                          LEFT JOIN (SELECT * FROM MED_PATIENT_DATAREPORT Z WHERE Z.STATE = '1' AND Z.TYPE='0' AND Z.EXTEND='XTXX' AND Z.EXTEND5='全国数据上报平台') T1
                            ON T.HEMODIALYSIS_ID = T1.HEMODIALYSIS_ID";
            }
        }
        public string GetDataReportPatientVascularList
        {
            get
            {
                return @"SELECT T.*,
                               T2.ITEM_NAME VASCULAR_ACCESS_TYPENAME,
                               T3.ITEM_NAME ACCESS_MATERIANAME,
                               T4.ITEM_NAME LATERAL_POSITIONNAME,
                               T5.ITEM_NAME VASCULAR_POSTIONNAME,
                               T6.ITEM_NAME ACCESS_CLASSNAME,
                               T7.ITEM_NAME ACCESS_STATUSNAME,
                               DECODE(T1.BASEINFO, NULL, '1', '2') ISUPLOAD,
                               DECODE(T1.BASEINFO, NULL, '未上传', '已上传') UPSTATE
                          FROM MED_VASCULAR_ACCESS T
                          LEFT JOIN (SELECT *
                                       FROM MED_PATIENT_DATAREPORT Z
                                      WHERE Z.STATE = '1' AND Z.EXTEND='XTXX'
                                        AND Z.TYPE = '1' AND Z.EXTEND5='全国数据上报平台') T1
                            ON T.HEMODIALYSIS_ID = T1.HEMODIALYSIS_ID AND T.VASCULAR_ACCESS_ID=T1.MAPIP
                          LEFT JOIN MED_COMMON_ITEMLIST T2
                            ON T.VASCULAR_ACCESS_TYPE = T2.ITEM_ID
                          LEFT JOIN MED_COMMON_ITEMLIST T3
                            ON T.ACCESS_MATERIA = T3.ITEM_ID
                          LEFT JOIN MED_COMMON_ITEMLIST T4
                            ON T.LATERAL_POSITION = T4.ITEM_ID
                          LEFT JOIN MED_COMMON_ITEMLIST T5
                            ON T.VASCULAR_POSTION = T5.ITEM_ID
                          LEFT JOIN MED_COMMON_ITEMLIST T6
                            ON T.ACCESS_CLASS = T6.ITEM_ID
                          LEFT JOIN MED_COMMON_ITEMLIST T7
                            ON T.ACCESS_STATUS = T7.ITEM_ID
                        WHERE T.HEMODIALYSIS_ID = :HEMODIALYSIS_ID";
            }
        }
        public string GetDataReportPatientRecipeList
        {
            get
            {
                return @"SELECT T.*,
                                CASE
                                        WHEN T.FIRST_PURIFIER_M2 < 1.2 THEN
                                        '<1.2  (m^2)'
                                        WHEN T.FIRST_PURIFIER_M2 = 1.2 THEN
                                        '1.2---  (m^2)'
                                        WHEN T.FIRST_PURIFIER_M2 = 1.4 THEN
                                        '1.4---  (m^2)'
                                        WHEN T.FIRST_PURIFIER_M2 = 1.6 THEN
                                        '1.6--- (m^2)'
                                        WHEN T.FIRST_PURIFIER_M2 >= 1.8 THEN
                                        '>1.8  (m^2)'
                                        ELSE
                                        '<1.2  (m^2)'
                                    END AS FIRST_PURIFIER_M2INDEX,
                               T2.ITEM_NAME PURIFICATION_MODENAME,
                               T3.ITEM_NAME FIRST_PURIFIER_MODELNAME,
                               T4.ITEM_NAME FIRST_PURIFIER_NAMESTR,
                               DECODE(T1.BASEINFO, NULL, '1', '2') ISUPLOAD,
                               DECODE(T1.BASEINFO, NULL, '未上传', '已上传') UPSTATE
                          FROM MED_HEMO_RECIPE T
                          LEFT JOIN (SELECT *
                                       FROM MED_PATIENT_DATAREPORT Z
                                      WHERE Z.STATE = '1' AND Z.EXTEND='XTXX' AND Z.EXTEND5='全国数据上报平台'
                                        AND Z.TYPE = :TYPE) T1
                            ON T.HEMODIALYSIS_ID = T1.HEMODIALYSIS_ID AND T.RECIPE_ID=T1.MAPIP
                          LEFT JOIN MED_COMMON_ITEMLIST T2
                            ON T.PURIFICATION_MODE = T2.ITEM_ID
                          LEFT JOIN MED_COMMON_ITEMLIST T3
                            ON T.FIRST_PURIFIER_MODEL = T3.ITEM_ID
                          LEFT JOIN MED_COMMON_ITEMLIST T4
                            ON T.FIRST_PURIFIER_NAME = T4.ITEM_ID
                         WHERE T.RECIPE_TYPE = '0'
                           AND T.STATUS != '2' AND T.HEMODIALYSIS_ID = :HEMODIALYSIS_ID";
            }
        }
        public string GetDataReportPatientBloodList
        {
            get
            {
                return @"SELECT T.*,
                               DECODE(T1.BASEINFO, NULL, '1', '2') ISUPLOAD,
                               DECODE(T1.BASEINFO, NULL, '未上传', '已上传') UPSTATE
                          FROM MED_CURE_MAIN T
                          LEFT JOIN (SELECT *
                                       FROM MED_PATIENT_DATAREPORT Z
                                      WHERE Z.STATE = '1' AND Z.EXTEND='XTXX' AND Z.EXTEND5='全国数据上报平台'
                                        AND Z.TYPE = :TYPE) T1
                            ON T.HEMODIALYSIS_ID = T1.HEMODIALYSIS_ID
                           AND T.CURE_ID = T1.MAPIP
                         WHERE T.CURE_STATUS != '4'
                           AND T.HEMODIALYSIS_ID = :HEMODIALYSIS_ID";
            }
        }
        public string GetDataReportPatientAncitoaList
        {
            get
            {
                return @"SELECT T.*,
                               T2.ITEM_NAME,
                               DECODE(T1.BASEINFO, NULL, '1', '2') ISUPLOAD,
                               DECODE(T1.BASEINFO, NULL, '未上传', '已上传') UPSTATE
                          FROM MED_CURE_MAIN T
                          LEFT JOIN (SELECT *
                                       FROM MED_PATIENT_DATAREPORT Z
                                      WHERE Z.STATE = '1' AND Z.EXTEND='XTXX' AND Z.EXTEND5='全国数据上报平台'
                                        AND Z.TYPE = :TYPE) T1
                            ON T.HEMODIALYSIS_ID = T1.HEMODIALYSIS_ID  
                           AND T.CURE_ID = T1.MAPIP
                         LEFT JOIN MED_COMMON_ITEMLIST T2
                         ON T.HEPARIN_SPECIES = T2.ITEM_ID
                         WHERE T.CURE_STATUS != '4'
                           AND T.HEMODIALYSIS_ID = :HEMODIALYSIS_ID
                           AND T.HEPARIN_SPECIES IS NOT NULL";
            }
        }
        public string GetDataReportPatientEstimateSufficiencyList
        {
            get
            {
                return @"SELECT T.*,
                               DECODE(T1.BASEINFO, NULL, '1', '2') ISUPLOAD,
                               DECODE(T1.BASEINFO, NULL, '未上传', '已上传') UPSTATE
                          FROM MED_ESTIMATE_SUFFICIENCY T
                          LEFT JOIN (SELECT *
                                       FROM MED_PATIENT_DATAREPORT Z
                                      WHERE Z.STATE = '1' AND Z.EXTEND='XTXX' AND Z.EXTEND5='全国数据上报平台'
                                        AND Z.TYPE = '6') T1
                            ON T.HEMODIALYSIS_ID = T1.HEMODIALYSIS_ID
                           AND T.ID = T1.MAPIP
                         WHERE T.Is_Delete = '0'
                           AND T.HEMODIALYSIS_ID = :HEMODIALYSIS_ID";
            }
        }
        public string GetDataReportPatientDiagnoseList
        {
            get
            {
                return @"SELECT T.HEMODIALYSIS_ID,
                                   T.PATIENT_ID,
                                   T.NAME,
                                   T.SEX,
                                   T.BIRTHDAY,
                                   T.AGE,
                                   T.NATIVEPLACE,
                                   T.JOB,
                                   T.MARITAL,
                                   T.CREDENTIALS_TYPE,
                                   T.CREDENTIALS_NUMBER,
                                   T.EDUCATION,
                                   T.NATION,
                                   T.WORK_TELEPHONE,
                                   T.ADDRESS,
                                   T.MEDICAL_TYPE,
                                   T.TELEPHONE,
                                   T.TIME_TYPE,
                                   T.SPECIFIC_TIME,
                                   T.ADMISSION_NUMBER,
                                   DECODE(T.IS_NEW,'0','转入','1','转出','2','死亡','3','肾移植','4','退出','转入') IS_NEW,
                                   T.WHAT_HOSPITAL_IN,
                                   T.WHAT_DEPARTMENT_IN,
                                   T.FIRST_VISIT,
                                   T.DIAGNOSE,
                                   T.LEAVE_HOSPITAL_TIME,
                                   DECODE(T.INFECTIOUS_CHECK_RESULT,'','无',T.INFECTIOUS_CHECK_RESULT) INFECTIOUS_CHECK_RESULT,
                                   T.INPUT_CODE,
                                   T.VISIT_ID,
                                   T.WARD_CODE,
                                   T.BED_NO,
                                   T.CREATE_DATE,
                                   T.IS_DELETE,
                               DECODE(T1.BASEINFO, NULL, '1', '2') ISUPLOAD, T1.BASEINFO,
                               DECODE(T1.BASEINFO, NULL, '未上传', '已上传') UPSTATE
                          FROM MED_PATIENTS T
                          LEFT JOIN (SELECT * FROM MED_PATIENT_DATAREPORT Z WHERE Z.STATE = '1' AND Z.TYPE= :TYPE AND Z.EXTEND='ZDXX' AND Z.EXTEND5='全国数据上报平台') T1
                            ON T.HEMODIALYSIS_ID = T1.HEMODIALYSIS_ID
                        WHERE T.IS_DELETE = '0'
                           AND T.HEMODIALYSIS_ID = :HEMODIALYSIS_ID";
            }
        }
        public string GetDataReportPatientLabList
        {
            get
            {
                return @"SELECT *
  FROM (SELECT m.TEST_NO,
               m.SPECIMEN,
               m.RESULTS_RPT_DATE_TIME,
               i.ITEM_NO,
               i.ITEM_NAME,
               r.REPORT_ITEM_CODE,
               r.REPORT_ITEM_NAME,
               r.RESULT,
               r.UNITS,
               r.REFERENCE_RESULT,
               DECODE(T1.BASEINFO, NULL, '1', '2') ISUPLOAD,
               DECODE(T1.BASEINFO, NULL, '未上传', '已上传') UPSTATE
          FROM MED_LAB_TEST_MASTER m
          LEFT JOIN MED_LAB_TEST_ITEMS i
            ON m.TEST_NO = i.TEST_NO
          LEFT JOIN MED_LAB_RESULT r
            ON m.TEST_NO = r.TEST_NO
          LEFT JOIN (SELECT *
                      FROM MED_PATIENT_DATAREPORT Z
                     WHERE Z.STATE = '1'
                       AND Z.EXTEND = 'LABEXAM' AND Z.EXTEND5='全国数据上报平台'
                       AND Z.TYPE = :TYPE) T1
            ON M.PATIENT_ID = T1.EXTEND3
           AND R.REPORT_ITEM_CODE = T1.EXTEND2
           AND M.TEST_NO = T1.MAPIP
         WHERE 1 = 1
           AND m.PATIENT_ID = :HEMODIALYSIS_ID
           AND M.RESULTS_RPT_DATE_TIME BETWEEN :DTSTAR AND :DTEND
         ORDER BY m.TEST_NO, i.ITEM_NO) K
 WHERE K.REPORT_ITEM_NAME IN ('白细胞',
                              '血红蛋白',
                              '红细胞压积',
                              '血小板',
                              '血总钙',
                              '血磷',
                              'iPTH',
                              '血清铁',
                              '总铁结合力',
                              '转铁饱和度',
                              '铁蛋白',
                              '尿素',
                              '肌酐',
                              '血总蛋白',
                              '血白蛋白',
                              'AST',
                              'ALT',
                              '总胆红素',
                              '甘油三酯',
                              '总胆固醇',
                              '低密度脂蛋白',
                              '高密度脂蛋白',
                              '血糖',
                              '血钾',
                              '血钠',
                              '血氯',
                              '二氧化碳',
                              'C反应蛋白',
                              'β2微球蛋白',
                              'HBsAg',
                              'AntiHBs',
                              'HBeAg',
                              'AntiHBe',
                              'AntiHBc',
                              'HBVDNA',
                              'AntiHCV',
                              'HCV-RNA	',
                              '艾滋病',
                              '梅毒',
                              '结核抗体',
                              '结核菌素试验')";
            }
        }
        #endregion
        #region 福建省数据上报平台
        public string GetDataReportPatientListFZ
        {
            get
            {
                return @"SELECT T.*,
                               DECODE(T1.BASEINFO, NULL, '1', '2') ISUPLOAD, T1.BASEINFO,
                               DECODE(T1.BASEINFO, NULL, '未上传', '已上传') UPSTATE
                          FROM MED_PATIENTS T
                          LEFT JOIN (SELECT * FROM MED_PATIENT_DATAREPORT Z WHERE Z.STATE = '1' AND Z.TYPE='0' AND Z.EXTEND='HZXX' AND Z.EXTEND5='福建省上报平台') T1
                            ON T.HEMODIALYSIS_ID = T1.HEMODIALYSIS_ID";
            }
        }
        public string GetHavingUpLoadPatient
        {
            get
            {
                return @"SELECT * FROM MED_PATIENT_DATAREPORT T WHERE T.EXTEND5=:EXTEND5 AND T.STATE=:STATE AND T.EXTEND=:EXTEND AND T.TYPE=:TYPE";
            }
        }
        #endregion


        #endregion

        #region 单个病人药品出入库管理

        /// <summary>
        /// 根据透析号查询病人药品入库
        /// </summary>
        public string QueryPatientDrugInputById
        {
            get
            {
                return @"SELECT T.ID,T.HEMODIALYSIS_ID,T.DRUG_CODE,T.DRUG_COUNT,T.INPUT_DATE,T.DRUG_SUM,M.DRUG_NAME,M.DRUG_SPEC,M.PRICE,M.UNITS,T.INPUT_ID,
                         CASE STATUS WHEN '0' THEN '使用中' ELSE '使用完' END AS STATUS, 
                         (SELECT S.USER_NAME FROM MED_USERS S WHERE S.USER_ID = T.APPLYID) AS USER_NAME
                         FROM MED_PATIENT_DRUG_INPUT T
                         LEFT OUTER JOIN MED_DRUG_MASTER M ON M.DRUG_CODE=T.DRUG_CODE
                         WHERE T.HEMODIALYSIS_ID =:ID AND T.ISDELETE='0'
                         AND  T.INPUT_DATE >= :BEGINTIME
                         AND  T.INPUT_DATE <= :ENDTIME
                         ORDER BY T.STATUS, T.INPUT_DATE DESC";
            }
        }
        /// <summary>
        /// 根据透析号查询病人药品出库
        /// </summary>
        public string QueryPatientDrugOutputById
        {
            get
            {
                return @"SELECT T.ID,T.HEMODIALYSIS_ID,T.DRUG_CODE,T.USE_COUNT,T.OUTPUT_DATE,M.DRUG_NAME,M.DRUG_SPEC,M.PRICE,M.UNITS,T.INPUT_ID,
                         (SELECT S.USER_NAME FROM MED_USERS S WHERE S.USER_ID = T.APPLYID) AS USER_NAME
                         FROM MED_PATIENT_DRUG_OUTPUT T
                         LEFT OUTER JOIN MED_DRUG_MASTER M ON M.DRUG_CODE=T.DRUG_CODE
                         WHERE T.HEMODIALYSIS_ID =:ID AND T.ISDELETE='0'
                         AND  T.OUTPUT_DATE >= :BEGINTIME
                         AND  T.OUTPUT_DATE <= :ENDTIME
                         ORDER BY T.OUTPUT_DATE DESC";
            }
        }
        public string QueryPatientDrugOutPutToPrint
        {
            get
            {
                return @"SELECT R.DRUG_NAME || ' ' || R.DRUG_SPEC AS DRUG_NAME,
                               SUM(T.USE_COUNT) AS USE_COUNT,
                               T.OUTPUT_DATE OUTPUT_DATE,
                               (SELECT S.USER_NAME FROM MED_USERS S WHERE S.USER_ID = T.APPLYID) AS USER_NAME,
                               (SELECT DISTINCT P.DRUG_SUM
                                  FROM MED_PATIENT_DRUG_INPUT P
                                 WHERE P.DRUG_CODE = T.DRUG_CODE
                                   AND ROWNUM = 1) AS DRUG_SUM
                          FROM MED_PATIENT_DRUG_OUTPUT T, MED_DRUG_MASTER R
                         WHERE T.DRUG_CODE = R.DRUG_CODE
                           AND T.HEMODIALYSIS_ID = :ID
                           AND T.ISDELETE = '0'
                           AND T.OUTPUT_DATE >= :BEGINTIME
                           AND T.OUTPUT_DATE <= :ENDTIME
                         GROUP BY T.OUTPUT_DATE, R.DRUG_NAME, T.APPLYID, T.DRUG_CODE, R.DRUG_SPEC
                         ORDER BY T.OUTPUT_DATE DESC";
            }
        }
        /// <summary>
        /// 根据透析号查询病人药品入库
        /// </summary>
        public string GetPatientDrugNumberByParam
        {
            get
            {
                return @"SELECT * FROM MED_PATIENT_DRUG_INPUT WHERE HEMODIALYSIS_ID=:ID AND DRUG_CODE=:CODE AND STATUS=0  AND ISDELETE='0' ORDER BY INPUT_DATE";
            }
        }
        public string UpdatePatientDrugInputByParam
        {
            get
            {
                return @"UPDATE MED_PATIENT_DRUG_INPUT SET DRUG_SUM=:SUM WHERE HEMODIALYSIS_ID=:ID AND DRUG_CODE=:CODE";
            }
        }
        public string UpdatePatientDrugInputByOutPutParam
        {
            get
            {
                return @"UPDATE MED_PATIENT_DRUG_INPUT
                       SET DRUG_SUM = :SUM, DRUG_REMAIN = :SUM - DRUG_SUM + DRUG_REMAIN , STATUS='0'
                     WHERE HEMODIALYSIS_ID = :ID
                       AND DRUG_CODE = :CODE
                       AND INPUT_ID = :INPUT_ID";
            }
        }
        public string UpdatePatientDrugInputRemainByParam
        {
            get
            {
                return @"UPDATE MED_PATIENT_DRUG_INPUT SET DRUG_REMAIN=:REMAIN WHERE ID=:ID";
            }
        }
        public string DeletePatientDrugInputByID
        {
            get
            {
                return @"UPDATE MED_PATIENT_DRUG_INPUT SET ISDELETE='1' WHERE ID=:ID AND ISDELETE='0'";
            }
        }
        public string DeletePatientDrugOutputByID
        {
            get
            {
                return @"UPDATE MED_PATIENT_DRUG_OUTPUT SET ISDELETE='1' WHERE ID=:ID AND ISDELETE='0'";
            }
        }
        public string UpdatePatientDrugInputStatusByID
        {
            get
            {
                return @"UPDATE MED_PATIENT_DRUG_INPUT SET STATUS='1' WHERE ID=:ID";
            }
        }
        /// <summary>
        /// 得到病人药品入库列表 
        /// </summary>
        public string GetDrugInputList
        {
            get
            {
                return @"SELECT DISTINCT T.DRUG_CODE,
                                T.DRUG_SUM,
                                M.DRUG_NAME,
                                M.DRUG_PINYIN,
                                M.DRUG_SPEC,
                                M.PRICE,
                                M.UNITS,
                                M.DRUG_TYPE,
                                M.FIRM_ID,
                                M.FIRM_NAME
                  FROM MED_PATIENT_DRUG_INPUT T
                  LEFT OUTER JOIN MED_DRUG_MASTER M
                    ON M.DRUG_CODE = T.DRUG_CODE
                    WHERE T.HEMODIALYSIS_ID= :HEMODIALYSIS_ID";
            }
        }
        #endregion

        /// <summary>
        /// 根据透析编号和同意书名字获取同意书签名
        /// </summary>
        public string GetBookPictureByHemoAndBookName
        {
            get
            {
                return @"SELECT * FROM MED_BOOK_PICTURE WHERE HEMODIALYSIS_ID=:HEMODIALYSIS_ID AND BOOK_NAME=:BOOK_NAME";
            }
        }

        /// <summary>
        /// 根据透析编号和治疗单ID获取透析单签字
        /// </summary>
        public string GetCureSignByHemoIdAndCureId
        {
            get
            {
                return @"SELECT * FROM MED_CURE_SIGN WHERE HEMODIALYSIS_ID=:HEMODIALYSIS_ID AND CURE_ID=:CURE_ID";
            }
        }

        /// <summary>
        /// 获取患者血管通路全称
        /// </summary>
        public string GetVascularAccessAllName
        {
            get
            {
                return @"SELECT DISTINCT(C.ITEM_NAME||M.ITEM_NAME||N.ITEM_NAME||I.ITEM_NAME) AS VA FROM MED_VASCULAR_ACCESS V LEFT JOIN MED_COMMON_ITEMLIST C
                ON V.LATERAL_POSITION =  C.ITEM_ID LEFT JOIN MED_COMMON_ITEMLIST M ON V.VASCULAR_POSTION=
                M.ITEM_ID LEFT JOIN MED_COMMON_ITEMLIST N ON N.ITEM_ID = V.VASCULAR_ACCESS_TYPE INNER JOIN MED_COMMON_ITEMLIST
                I ON V.ACCESS_CLASS =  I.ITEM_ID WHERE V.VASCULAR_ACCESS_ID=:VASCULAR_ACCESS_ID";
            }
        }

        /// <summary>
        /// 根据ID获取流程节点明细
        /// </summary>

        public string GetProcessSetDataById
        {
            get
            {
                return @"SELECT T.* FROM  MED_PROCESS_SET T WHERE T.ID = :id";
            }
        }
        /// <summary>
        /// GetProcessSetDataList
        /// </summary>
        public string GetProcessSetDataList
        {
            get
            {
                return @"SELECT T.*,l.item_name as PROCESS_NAME,
                        DECODE(T.IS_STOP,0,'启用',1,'停止') as STOP_NAME 
                        FROM MED_PROCESS_SET T LEFT JOIN MED_COMMON_ITEMLIST l 
                        on t.process_id =  l.item_id and l.item_type='流程节点' ORDER BY  l.order_number,t.sort_id   ";
            }
        }
        /// <summary>
        /// DeleteProcessSetDataById
        /// </summary>
        public string DeleteProcessSetDataById
        {
            get { return @"DELETE FROM MED_PROCESS_SET WHERE ID = :id"; }
        }

        /// <summary>
        /// 患者透析龄查询(算法需要调整)
        /// </summary>
        public string GetPatientHemoAge
        {
            get
            {
                return @"SELECT P.NAME,P.SEX,(TO_CHAR(SYSDATE,'YYYY')-TO_CHAR(P.BIRTHDAY, 'YYYY')) PATIENT_AGE,P.HEMODIALYSIS_ID,
                    P.TELEPHONE,P.DIAGNOSE,P.SPECIFIC_TIME,(TO_CHAR(SYSDATE,'YYYY')-TO_CHAR(P.SPECIFIC_TIME, 'YYYY')) HEMOAGE
                    FROM MED_PATIENTS P WHERE P.SPECIFIC_TIME != TO_DATE('0001/1/1','YYYY/MM/DD') 
                    AND (TO_CHAR(SYSDATE,'YYYY')-TO_CHAR(P.BIRTHDAY, 'YYYY'))  >= :AGE 
                    AND (TO_CHAR(SYSDATE,'YYYY')-TO_CHAR(P.SPECIFIC_TIME, 'YYYY')) >= :HEMOAGE 
                    AND (p.NAME like '%'||:NAME||'%' OR upper(p.INPUT_CODE) LIKE upper('%'||:NAME||'%')) ";
            }
        }

        /// <summary>
        /// 患者透析综合信息查询  
        /// </summary>
        public string QueryPatientMoreInfoList
        {
            get
            {
                return @"SELECT distinct T.NAME,T.HEMODIALYSIS_ID,T.SEX,(TO_CHAR(SYSDATE,'YYYY')-TO_CHAR(T.BIRTHDAY, 'YYYY')) PATIENT_AGE,
                        T.SPECIFIC_TIME,T.DIAGNOSE,T.CREDENTIALS_NUMBER, R.RECIPE_TYPE,'每'||TO_CHAR(R.FREQUENCY_WEEK)||'周'||TO_CHAR(R.FREQUENCY_TIMES)||'次，每次'||TO_CHAR(R.FREQUENCY_HOURS)||'小时' 
                        AS HEMO_TIMES,cm.item_name,r.FIRST_DRUG_DOSAGE,tm.item_name as THERAPEUTIC_METHOD,pg.createdate as end_date,pg.eventanalysis,pg.correctiveactions 
                        FROM MED_PATIENTS T 
                         inner join MED_HEMO_RECIPE R
                            on t.hemodialysis_id = r.hemodialysis_id
                            LEFT JOIN MED_VASCULAR_ACCESS S 
                            ON R.VASCULAR_ACCESS_ID=S.VASCULAR_ACCESS_ID
                            LEFT JOIN med_common_itemlist cm
                            ON S.VASCULAR_ACCESS_TYPE = cm.item_id
                         inner join med_common_itemlist tm
                            on r.THERAPEUTIC_METHOD = tm.item_id
                          left join MED_PATIENTREGDEALTH pg
                            on t.hemodialysis_id = pg.hemodialysis_id
                          left join med_cure_main cure
                            on t.hemodialysis_id = cure.hemodialysis_id
                         WHERE R.RECIPE_TYPE = '1'";
            }
        }

        /// <summary>
        /// 检验数据报表导出检验项目和患者名
        /// </summary>
        public string GetSchedulePatientLabResultMain
        {
            get
            {
                return @"SELECT T1.TEST_NO, T1.PATIENT_ID, T1.PATIENT_NAME, T1.DATETIME, T1.ITEM_NAME,
                        WM_CONCAT(T2.REPORT_ITEM_NAME || ':' || T2.RESULT || T2.UNITS) AS RESULT FROM
                        (SELECT T.TEST_NO, T.PATIENT_ID, T.PATIENT_NAME, T.DATETIME, T.ITEM_NAME FROM 
                        (SELECT TO_CHAR(T1.RESULTS_RPT_DATE_TIME, 'YYYY-MM-DD') AS DATETIME, T1.TEST_NO AS TEST_NO, 
                        T1.PATIENT_ID AS PATIENT_ID, T1.NAME AS PATIENT_NAME, T2.ITEM_NAME AS ITEM_NAME
                        FROM MED_LAB_TEST_MASTER T1, MED_LAB_TEST_ITEMS T2
                        WHERE T1.RESULTS_RPT_DATE_TIME >= :BEGINDIALYSIS_DATE 
                        AND T1.RESULTS_RPT_DATE_TIME < :ENDDIALYSIS_DATE AND
                        T1.TEST_NO = T2.TEST_NO AND T1.PATIENT_ID IN (SELECT PATIENT_ID FROM MED_PATIENTS)
                        AND T2.ITEM_NAME IN ('电解质检查', '急诊潜血试验', '急诊生化(干化学)', '急诊生化（干化学）',
                        '梅毒血清试验（TRUST法）', '梅毒血清试验(TRUST法)',  '脑利钠肽BNP', '尿常规+沉渣检测', 
                        '凝血四项（PT+APTT+TT+FIB）', '凝血四项(PT+APTT+TT+FIB)', '常规生化全套检查', 
                        '术前四项', '心梗三项', '血脂四项', '叶酸', '乙肝表面抗体', 
                        '乙肝两对半定量检测', '总铁结合力')ORDER BY 
                        T1.RESULTS_RPT_DATE_TIME, T1.NAME, T2.ITEM_NAME) T ) T1, MED_LAB_RESULT T2
                        WHERE T1.TEST_NO = T2.TEST_NO 
                        GROUP BY T1.DATETIME, T1.TEST_NO, T1.PATIENT_ID, T1.PATIENT_NAME, T1.ITEM_NAME
                        ORDER BY T1.DATETIME,T1.PATIENT_NAME,T1.ITEM_NAME";
            }
        }
        /// <summary>
        /// 根据ID或者科室建制信息
        /// </summary>
        public string GetMED_HOSPITAL_INFOById
        {
            get
            {
                return @" SELECT  * FROM MED_HOSPITAL_INFO WHERE HOSPITAL_ID = :HOSPITAL_ID";
            }
        }
        /// <summary>
        ///获取科室建制信息列表 
        /// </summary>
        public string GetMED_HOSPITAL_INFOList
        {
            get
            {
                return @"SELECT  * FROM MED_HOSPITAL_INFO ";
            }
        }
        /// <summary>
        /// 获取质控平台血透机，血滤机，水处理机的台数和品牌
        /// </summary>
        public string GetQualityControlEquipmentInfo
        {
            get
            {
                return @"
                        SELECT COUNT(1) AS COUNT,
                               fl.ITEM_NAME FLNAME,
                               DECODE(dm.therapeutic_properties, 'HD', '血透机', '血滤机') AS KIND
                          FROM MED_DIALYSIS_MACHINE dm
                         INNER JOIN (SELECT *
                                       FROM MED_COMMON_ITEMLIST il
                                      WHERE il.ITEM_TYPE = '血透机品牌'
                                        AND il.STATUS = '1') fl
                            ON dm.TYPE = fl.ITEM_ID
                         GROUP BY fl.ITEM_NAME, dm.therapeutic_properties
                        UNION ALL
                        SELECT COUNT(1) AS COUNT, fl.ITEM_NAME FLNAME, '水处理机' AS KIND
                          FROM MED_DIALYSIS_MACHINE dm
                         INNER JOIN (SELECT *
                                       FROM MED_COMMON_ITEMLIST il
                                      WHERE il.ITEM_TYPE = '水处理机品牌'
                                        AND il.STATUS = '1') fl
                            ON dm.TYPE = fl.ITEM_ID
                         GROUP BY fl.ITEM_NAME";
            }
        }

        /// <summary>
        /// 根据医院ID获取机器相关信息
        /// </summary>
        public string GetQualityControlEquipmentInfoByHospitalID
        {
            get
            {
                return @"select * from med_equipment_info WHERE HOSPITAL_ID=:HOSPITAL_ID";
            }
        }
        /// <summary>
        /// 删除健康宣教
        /// </summary>
        public string DeleteHealthEducationByHemoIdAndId
        {
            get
            {
                return @"DELETE FROM MED_HEALTH_EDUCATION WHERE HEMODIALYSIS_ID=:HEMODIALYSIS_ID AND ID = :ID";
            }
        }

        /// <summary>
        /// 获取CMKDMBD信息
        /// </summary>
        public string GetMED_ANEMIA_CKDMBD_ASSESSbyDate
        {
            get
            {
                return @"SELECT t.* FROM MED_ANEMIA_CKDMBD_ASSESS  t WHERE t.Assess_Type =:Assess_Type  AND T.HEMODIALYSIS_ID=:HEMODIALYSIS_ID 
                         AND TRUNC(t.CREATE_DATE)>=TRUNC(:beginTime) AND TRUNC(T.CREATE_DATE) <=TRUNC(:endTime) ORDER BY t.CREATE_DATE DESC  ";
            }
        }

        /// <summary>
        /// 获取CMKMBD信息
        /// </summary>
        public string GetMED_ANEMIA_CKDMBD_ASSESS
        {
            get
            {
                return @"SELECT t.* FROM MED_ANEMIA_CKDMBD_ASSESS  t WHERE t.Assess_Type =:Assess_Type  AND T.HEMODIALYSIS_ID=:HEMODIALYSIS_ID 
                         ORDER BY t.CREATE_DATE DESC ";
            }
        }

        /// <summary>
        /// 获取死亡率信息
        /// </summary>
        public string GetDeathRate
        {
            get
            {
                return @"SELECT *
                FROM MED_PATIENTS T WHERE T.IS_NEW='1' ";
            }
        }

        /// <summary>
        /// 获取处方数量
        /// </summary>
        public string GetTempRecipeCount
        {
            get
            {
                return @"SELECT COUNT(1) FROM med_hemo_recipe WHERE STATUS='0' AND HEMODIALYSIS_ID=:HEMODIALYSIS_ID";
            }
        }

        #region HIS列换行信息

        public string INSERTMED_HIS_ROWTOCOL
        {
            get
            {
                return @"INSERT INTO med_his_rowtocol(hemodialysis_id, patient_id, test_no, check_date, item_name,Ext_Xml)
                 VALUES(:hemodialysis_id, :patient_id, :test_no, :check_date, :item_name,:Ext_Xml)";
            }
        }

        public string UPDATEMED_HIS_ROWTOCOL
        {
            get
            {
                return @"
                  UPDATE med_his_rowtocol SET EXT_XML = :EXT_XML WHERE  hemodialysis_id=:hemodialysis_id AND patient_id=:patient_id AND test_no=:test_no AND
                   check_date=:check_date AND item_name=:item_name";
            }
        }

        public string GETMED_HIS_ROWTOCOL
        {
            get
            {
                return @"
                  SELECT * FROM  med_his_rowtocol  WHERE  hemodialysis_id=:hemodialysis_id and patient_id=:patient_id and test_no=:test_no and
                   check_date=:check_date and item_name=:item_name";
            }
        }

        public string get_med_vw_xuehongdanbai_ext
        {
            get
            {
                return @"SELECT * FROM med_vw_xuehongdanbai_ext where trunc(""检验日期"") >=trunc(:begintime) and trunc(""检验日期"") <=trunc(:endtime)
                        order by ""检验日期"" desc";
            }
        }
        #endregion
        /// <summary>
        /// 根据日期获取护士绩效考核记录列表
        /// </summary>
        public string GetPerformanceAppraisalByDate
        {
            get
            {
                return @"SELECT T.*,D.NAME NURSE_NAME,D.NURSE_LEADER FROM MED_PERFORMANCE_APPRAISAL T
                        LEFT JOIN MED_STAFF_DICT D ON T.CHECK_NURSE=D.EMP_NO
                        WHERE TRUNC(T.CHECK_DATE) >= TRUNC(:BEGINTIME)
                        AND TRUNC(T.CHECK_DATE) <= TRUNC(:ENDTIME)
                        AND (D.IS_DELETE !='1' OR D.IS_DELETE IS NULL)
                        ORDER BY T.CHECK_DATE,D.NAME";
            }
        }

        /// <summary>
        /// 根据日期、护士组长获取护士绩效考核记录列表
        /// </summary>
        public string GetPerformanceAppraisalByDateAndNurseLeader
        {
            get
            {
                return @"SELECT T.*,D.NAME NURSE_NAME,D.NURSE_LEADER FROM MED_PERFORMANCE_APPRAISAL T
                        LEFT JOIN MED_STAFF_DICT D ON T.CHECK_NURSE=D.EMP_NO
                        WHERE TRUNC(T.CHECK_DATE) >= TRUNC(:BEGINTIME)
                        AND TRUNC(T.CHECK_DATE) <= TRUNC(:ENDTIME)
                        AND D.NURSE_LEADER=:NURSE_LEADER
                        AND (D.IS_DELETE !='1' OR D.IS_DELETE IS NULL)
                        ORDER BY T.CHECK_DATE,D.NAME";
            }
        }

        /// <summary>
        /// 根据日期、组长标识获取护士组长或组员绩效考核记录列表
        /// </summary>
        public string GetPerformanceAppraisalByDateAndLeaderFlag
        {
            get
            {
                return @"SELECT T.*,D.NAME NURSE_NAME,D.NURSE_LEADER FROM MED_PERFORMANCE_APPRAISAL T
                        LEFT JOIN MED_STAFF_DICT D ON T.CHECK_NURSE=D.EMP_NO
                        WHERE TRUNC(T.CHECK_DATE) >= TRUNC(:BEGINTIME)
                        AND TRUNC(T.CHECK_DATE) <= TRUNC(:ENDTIME)
                        AND D.IS_LEADER=:IS_LEADER
                        AND (D.IS_DELETE !='1' OR D.IS_DELETE IS NULL)
                        ORDER BY T.CHECK_DATE,D.NAME";
            }
        }

        /// <summary>
        /// 根据日期、护士获取护士绩效考核记录列表
        /// </summary>
        public string GetPerformanceAppraisalByDateAndNurse
        {
            get
            {
                return @"SELECT T.*,D.NAME NURSE_NAME,D.NURSE_LEADER FROM MED_PERFORMANCE_APPRAISAL T
                        LEFT JOIN MED_STAFF_DICT D ON T.CHECK_NURSE=D.EMP_NO
                        WHERE TRUNC(T.CHECK_DATE) >= TRUNC(:BEGINTIME)
                        AND TRUNC(T.CHECK_DATE) <= TRUNC(:ENDTIME)
                        AND T.CHECK_NURSE=:CHECK_NURSE
                        AND (D.IS_DELETE !='1' OR D.IS_DELETE IS NULL)
                        ORDER BY T.CHECK_DATE,D.NAME";
            }
        }

        /// <summary>
        /// 根据类型获取护士绩效考核规则记录列表
        /// </summary>
        public string GetPerformanceAppraisalRuleByType
        {
            get
            {
                return @"SELECT * FROM MED_PERFORMANCE_APPRAISAL_RULE T
                        WHERE T.ITEM_TYPE=:ITEM_TYPE ORDER BY T.ORDER_NUMBER";
            }
        }

        /// <summary>
        /// 根据得分类型获取护士绩效考核规则记录列表
        /// </summary>
        public string GetPerformanceAppraisalRuleByScoreType
        {
            get
            {
                return @"SELECT * FROM MED_PERFORMANCE_APPRAISAL_RULE T
                        WHERE T.SCORE_TYPE=:SCORE_TYPE ORDER BY T.ORDER_NUMBER";
            }
        }

        /// <summary>
        /// 根据ID获取护士绩效考核规则记录
        /// </summary>
        public string GetPerformanceAppraisalRuleById
        {
            get
            {
                return @"SELECT * FROM MED_PERFORMANCE_APPRAISAL_RULE T
                        WHERE T.ID=:ID";
            }
        }

        #region 检查检验信息保存至MED_HIS_ROWTOCOL_END

        public string SELECT_MED_HIS_ROWTOCOL_END
        {
            get
            {
                return @"
                        SELECT  T.* FROM  MED_HIS_ROWTOCOL_END T WHERE 
                        T.HEMODIALYSIS_ID = :HEMODIALYSIS_ID AND T.PATIENT_ID =:PATIENT_ID 
                        AND T.TEST_NO =:TEST_NO AND T.CHECK_DATE =:CHECK_DATE AND T.ITEM_NAME =:ITEM_NAME";
            }
        }


        public string INSERT_UPDATE_MED_HIS_ROWTOCOL_END
        {
            get
            {
                return @"MERGE INTO MED_HIS_ROWTOCOL_END T
                            USING (SELECT HEMODIALYSIS_ID,
                                            PATIENT_ID,
                                            TEST_NO,
                                            CHECK_DATE,
                                            ITEM_NAME,
                                            t.EXT_XML.EXTRACT('//Data/Line/@COL_10').getStringVal() AS COL_10,
                                            t.EXT_XML.EXTRACT('//Data/Line/@COL_11').getStringVal() AS COL_11,
                                            t.EXT_XML.EXTRACT('//Data/Line/@COL_12').getStringVal() AS COL_12,
                                            t.EXT_XML.EXTRACT('//Data/Line/@COL_13').getStringVal() AS COL_13,
                                            t.EXT_XML.EXTRACT('//Data/Line/@COL_14').getStringVal() AS COL_14,
                                            t.EXT_XML.EXTRACT('//Data/Line/@COL_15').getStringVal() AS COL_15,
                                            t.EXT_XML.EXTRACT('//Data/Line/@COL_16').getStringVal() AS COL_16,
                                            t.EXT_XML.EXTRACT('//Data/Line/@COL_17').getStringVal() AS COL_17,
                                            t.EXT_XML.EXTRACT('//Data/Line/@COL_18').getStringVal() AS COL_18,
                                            t.EXT_XML.EXTRACT('//Data/Line/@COL_19').getStringVal() AS COL_19,
                                            t.EXT_XML.EXTRACT('//Data/Line/@COL_20').getStringVal() AS COL_20,
                                            t.EXT_XML.EXTRACT('//Data/Line/@COL_21').getStringVal() AS COL_21,
                                            t.EXT_XML.EXTRACT('//Data/Line/@COL_22').getStringVal() AS COL_22,
                                            t.EXT_XML.EXTRACT('//Data/Line/@COL_23').getStringVal() AS COL_23,
                                            t.EXT_XML.EXTRACT('//Data/Line/@COL_24').getStringVal() AS COL_24,
                                            t.EXT_XML.EXTRACT('//Data/Line/@COL_25').getStringVal() AS COL_25,
                                            t.EXT_XML.EXTRACT('//Data/Line/@COL_26').getStringVal() AS COL_26,
                                            t.EXT_XML.EXTRACT('//Data/Line/@COL_27').getStringVal() AS COL_27,
                                            t.EXT_XML.EXTRACT('//Data/Line/@COL_28').getStringVal() AS COL_28,
                                            t.EXT_XML.EXTRACT('//Data/Line/@COL_29').getStringVal() AS COL_29,
                                            t.EXT_XML.EXTRACT('//Data/Line/@COL_30').getStringVal() AS COL_30,
                                            t.EXT_XML.EXTRACT('//Data/Line/@COL_31').getStringVal() AS COL_31,
                                            t.EXT_XML.EXTRACT('//Data/Line/@COL_32').getStringVal() AS COL_32,
                                            t.EXT_XML.EXTRACT('//Data/Line/@COL_33').getStringVal() AS COL_33,
                                            t.EXT_XML.EXTRACT('//Data/Line/@COL_34').getStringVal() AS COL_34,
                                            t.EXT_XML.EXTRACT('//Data/Line/@COL_35').getStringVal() AS COL_35,
                                            t.EXT_XML.EXTRACT('//Data/Line/@COL_36').getStringVal() AS COL_36,
                                            t.EXT_XML.EXTRACT('//Data/Line/@COL_37').getStringVal() AS COL_37,
                                            t.EXT_XML.EXTRACT('//Data/Line/@COL_38').getStringVal() AS COL_38,
                                            t.EXT_XML.EXTRACT('//Data/Line/@COL_39').getStringVal() AS COL_39,
                                            t.EXT_XML.EXTRACT('//Data/Line/@COL_40').getStringVal() AS COL_40,
                                            t.EXT_XML.EXTRACT('//Data/Line/@COL_41').getStringVal() AS COL_41,
                                            t.EXT_XML.EXTRACT('//Data/Line/@COL_42').getStringVal() AS COL_42,
                                            t.EXT_XML.EXTRACT('//Data/Line/@COL_43').getStringVal() AS COL_43,
                                            t.EXT_XML.EXTRACT('//Data/Line/@COL_44').getStringVal() AS COL_44,
                                            t.EXT_XML.EXTRACT('//Data/Line/@COL_45').getStringVal() AS COL_45
                                        FROM MED_HIS_ROWTOCOL T
                                    WHERE  TRUNC(T.CHECK_DATE) >=TRUNC(:BEGINTIME)
                                        AND TRUNC(T.CHECK_DATE) <= TRUNC(:ENDTIME)
                                        AND T.ITEM_NAME = :ITEM_NAME) T2
                            ON (T.HEMODIALYSIS_ID = T2.HEMODIALYSIS_ID AND T.PATIENT_ID = T2.PATIENT_ID AND T.TEST_NO = T2.TEST_NO AND T.CHECK_DATE = T2.CHECK_DATE AND T.ITEM_NAME = T2.ITEM_NAME)
                            WHEN MATCHED THEN
                                UPDATE
                                    SET T.COL_10 = T2.COL_10,
                                        T.COL_11 = T2.COL_11,
                                        T.COL_12 = T2.COL_12,
                                        T.COL_13 = T2.COL_13,
                                        T.COL_14 = T2.COL_14,
                                        T.COL_15 = T2.COL_15,
                                        T.COL_16 = T2.COL_16,
                                        T.COL_17 = T2.COL_17,
                                        T.COL_18 = T2.COL_18,
                                        T.COL_19 = T2.COL_19,
                                        T.COL_20 = T2.COL_20,
                                        T.COL_21 = T2.COL_21,
                                        T.COL_22 = T2.COL_22,
                                        T.COL_23 = T2.COL_23,
                                        T.COL_24 = T2.COL_24,
                                        T.COL_25 = T2.COL_25,
                                        T.COL_26 = T2.COL_26,
                                        T.COL_27 = T2.COL_27,
                                        T.COL_28 = T2.COL_28,
                                        T.COL_29 = T2.COL_29,
                                        T.COL_30 = T2.COL_30,
                                        T.COL_31 = T2.COL_31,
                                        T.COL_32 = T2.COL_32,
                                        T.COL_33 = T2.COL_33,
                                        T.COL_34 = T2.COL_34,
                                        T.COL_35 = T2.COL_35,
                                        T.COL_36 = T2.COL_36,
                                        T.COL_37 = T2.COL_37,
                                        T.COL_38 = T2.COL_38,
                                        T.COL_39 = T2.COL_39,
                                        T.COL_40 = T2.COL_40,
                                        T.COL_41 = T2.COL_41,
                                        T.COL_42 = T2.COL_42,
                                        T.COL_43 = T2.COL_43,
                                        T.COL_44 = T2.COL_44,
                                        T.COL_45 = T2.COL_45
                                WHERE T.HEMODIALYSIS_ID = T2.HEMODIALYSIS_ID
                                    AND T.PATIENT_ID = T2.PATIENT_ID
                                    AND T.TEST_NO = T2.TEST_NO
                                    AND T.CHECK_DATE = T2.CHECK_DATE
                                    AND T.ITEM_NAME = T2.ITEM_NAME
                                    AND TRUNC(T.CHECK_DATE) >=TRUNC(:BEGINTIME)
                                    AND TRUNC(T.CHECK_DATE) <= TRUNC(:ENDTIME)
                                    AND T.ITEM_NAME = :ITEM_NAME
                            WHEN NOT MATCHED THEN
                                INSERT
                                VALUES
                                (T2.HEMODIALYSIS_ID,
                                    T2.PATIENT_ID,
                                    T2.TEST_NO,
                                    T2.CHECK_DATE,
                                    T2.ITEM_NAME,
                                    T2.COL_10,
                                    T2.COL_11,
                                    T2.COL_12,
                                    T2.COL_13,
                                    T2.COL_14,
                                    T2.COL_15,
                                    T2.COL_16,
                                    T2.COL_17,
                                    T2.COL_18,
                                    T2.COL_19,
                                    T2.COL_20,
                                    T2.COL_21,
                                    T2.COL_22,
                                    T2.COL_23,
                                    T2.COL_24,
                                    T2.COL_25,
                                    T2.COL_26,
                                    T2.COL_27,
                                    T2.COL_28,
                                    T2.COL_29,
                                    T2.COL_30,
                                    T2.COL_31,
                                    T2.COL_32,
                                    T2.COL_33,
                                    T2.COL_34,
                                    T2.COL_35,
                                    T2.COL_36,
                                    T2.COL_37,
                                    T2.COL_38,
                                    T2.COL_39,
                                    T2.COL_40,
                                    T2.COL_41,
                                    T2.COL_42,
                                    T2.COL_43,
                                    T2.COL_44,
                                    T2.COL_45)";
            }
        }
        #endregion

        #region 检验项目视图

        /// <summary>
        /// 常规生化全套检查
        /// </summary>
        public string MED_VIEW_CHANGGUISHENGHUA_EXT_Long
        {
            get
            {
                return @"SELECT T.* FROM MED_VIEW_CHANGGUISHENGHUA_EXT T
                        WHERE TRUNC(T.""检验日期"") >= TRUNC(:STARTDATE)
                        AND TRUNC(T.""检验日期"") <= TRUNC(:ENDDATE)
                        AND (T.""姓名"" LIKE '%' || :PATIENTINFO || '%' 
                        OR T.""透析号"" LIKE '%' || :PATIENTINFO || '%' 
                        OR UPPER(T.INPUT_CODE) LIKE UPPER('%' || :PATIENTINFO || '%'))";
            }
        }

        /// <summary>
        /// 常规生化全套检查
        /// </summary>
        public string MED_VIEW_CHANGGUISHENGHUA_EXT_Short
        {
            get
            {
                return @"SELECT T.* FROM MED_VIEW_CHANGGUISHENGHUA_EXT T 
                        WHERE TRUNC(T.""检验日期"") >= TRUNC(:STARTDATE) 
                        AND TRUNC(T.""检验日期"") <=TRUNC(:ENDDATE)";
            }
        }

        /// <summary>
        /// 电解质检查
        /// </summary>
        public string MED_VIEW_DIANJIEZHICHECK_EXT_Long
        {
            get
            {
                return @"SELECT T.* FROM MED_VIEW_DIANJIEZHICHECK_EXT T
                        WHERE TRUNC(T.""检验日期"") >= TRUNC(:STARTDATE)
                        AND TRUNC(T.""检验日期"") <= TRUNC(:ENDDATE)
                        AND (T.""姓名"" LIKE '%' || :PATIENTINFO || '%' 
                        OR T.""透析号"" LIKE '%' || :PATIENTINFO || '%' 
                        OR UPPER(T.INPUT_CODE) LIKE UPPER('%' || :PATIENTINFO || '%'))";
            }
        }

        /// <summary>
        /// 电解质检查
        /// </summary>
        public string MED_VIEW_DIANJIEZHICHECK_EXT_Short
        {
            get
            {
                return @"SELECT T.* FROM MED_VIEW_DIANJIEZHICHECK_EXT T 
                        WHERE TRUNC(T.""检验日期"") >= TRUNC(:STARTDATE) 
                        AND TRUNC(T.""检验日期"") <=TRUNC(:ENDDATE)";
            }
        }

        /// <summary>
        /// 血常规(含有核红细胞五分类)
        /// </summary>
        public string MED_VIEW_XUECHANGGUI_EXT_Long
        {
            get
            {
                return @"SELECT T.* FROM MED_VIEW_XUECHANGGUI_EXT T 
                        WHERE TRUNC(T.""检验日期"") >= TRUNC(:STARTDATE) 
                        AND TRUNC(T.""检验日期"") <=TRUNC(:ENDDATE) 
                        AND (T.""姓名"" LIKE '%'||:PATIENTINFO||'%' 
                        OR T.""透析号"" LIKE '%'||:PATIENTINFO||'%' 
                        OR UPPER(T.INPUT_CODE) LIKE UPPER('%'||:PATIENTINFO||'%'))";
            }
        }

        /// <summary>
        /// 血常规(含有核红细胞五分类)
        /// </summary>
        public string MED_VIEW_XUECHANGGUI_EXT_Short
        {
            get
            {
                return @"SELECT T.* FROM MED_VIEW_XUECHANGGUI_EXT T 
                        WHERE TRUNC(T.""检验日期"") >= TRUNC(:STARTDATE) 
                        AND TRUNC(T.""检验日期"") <=TRUNC(:ENDDATE)";
            }
        }

        /// <summary>
        /// 血常规（含网积红及有核红）
        /// </summary>
        public string MED_VIEW_XUECHANGGUI_2_EXT_Long
        {
            get
            {
                return @"SELECT T.* FROM MED_VIEW_XUECHANGGUI_2_EXT T
                        WHERE TRUNC(T.""检验日期"") >= TRUNC(:STARTDATE)
                        AND TRUNC(T.""检验日期"") <= TRUNC(:ENDDATE)
                        AND (T.""姓名"" LIKE '%' || :PATIENTINFO || '%' 
                        OR T.""透析号"" LIKE '%' || :PATIENTINFO || '%' 
                        OR UPPER(T.INPUT_CODE) LIKE UPPER('%' || :PATIENTINFO || '%'))";
            }
        }

        /// <summary>
        /// 血常规（含网积红及有核红）
        /// </summary>
        public string MED_VIEW_XUECHANGGUI_2_EXT_Short
        {
            get
            {
                return @"SELECT T.* FROM MED_VIEW_XUECHANGGUI_2_EXT T 
                        WHERE TRUNC(T.""检验日期"") >= TRUNC(:STARTDATE) 
                        AND TRUNC(T.""检验日期"") <=TRUNC(:ENDDATE)";
            }
        }

        /// <summary>
        /// 肝功能检查
        /// </summary>
        public string MED_VIEW_GANGONGNENGCHECK_EXT_Long
        {
            get
            {
                return @"SELECT T.* FROM MED_VIEW_GANGONGNENGCHECK_EXT T
                        WHERE TRUNC(T.""检验日期"") >= TRUNC(:STARTDATE)
                        AND TRUNC(T.""检验日期"") <= TRUNC(:ENDDATE)
                        AND (T.""姓名"" LIKE '%' || :PATIENTINFO || '%' 
                        OR T.""透析号"" LIKE '%' || :PATIENTINFO || '%' 
                        OR UPPER(T.INPUT_CODE) LIKE UPPER('%' || :PATIENTINFO || '%'))";
            }
        }

        /// <summary>
        /// 肝功能检查
        /// </summary>
        public string MED_VIEW_GANGONGNENGCHECK_EXT_Short
        {
            get
            {
                return @"SELECT T.* FROM MED_VIEW_GANGONGNENGCHECK_EXT T 
                        WHERE TRUNC(T.""检验日期"") >= TRUNC(:STARTDATE) 
                        AND TRUNC(T.""检验日期"") <=TRUNC(:ENDDATE)";
            }
        }

        /// <summary>
        /// 肾功能检查
        /// </summary>
        public string MED_VIEW_SHENGONGNENGCHECK_EXT_Long
        {
            get
            {
                return @"SELECT T.* FROM MED_VIEW_SHENGONGNENGCHECK_EXT T
                        WHERE TRUNC(T.""检验日期"") >= TRUNC(:STARTDATE)
                        AND TRUNC(T.""检验日期"") <= TRUNC(:ENDDATE)
                        AND (T.""姓名"" LIKE '%' || :PATIENTINFO || '%' 
                        OR T.""透析号"" LIKE '%' || :PATIENTINFO || '%' 
                        OR UPPER(T.INPUT_CODE) LIKE UPPER('%' || :PATIENTINFO || '%'))";
            }
        }

        /// <summary>
        /// 肾功能检查
        /// </summary>
        public string MED_VIEW_SHENGONGNENGCHECK_EXT_Short
        {
            get
            {
                return @"SELECT T.* FROM MED_VIEW_SHENGONGNENGCHECK_EXT T 
                        WHERE TRUNC(T.""检验日期"") >= TRUNC(:STARTDATE) 
                        AND TRUNC(T.""检验日期"") <=TRUNC(:ENDDATE)";
            }
        }

        /// <summary>
        /// 输血前四项检查
        /// </summary>
        public string MED_VIEW_SHUXUEQIANSIXIANG_EXT_Long
        {
            get
            {
                return @"SELECT T.* FROM MED_VIEW_SHUXUEQIANSIXIANG_EXT T
                        WHERE TRUNC(T.""检验日期"") >= TRUNC(:STARTDATE)
                        AND TRUNC(T.""检验日期"") <= TRUNC(:ENDDATE)
                        AND (T.""姓名"" LIKE '%' || :PATIENTINFO || '%' 
                        OR T.""透析号"" LIKE '%' || :PATIENTINFO || '%' 
                        OR UPPER(T.INPUT_CODE) LIKE UPPER('%' || :PATIENTINFO || '%'))";
            }
        }

        /// <summary>
        /// 输血前四项检查
        /// </summary>
        public string MED_VIEW_SHUXUEQIANSIXIANG_EXT_Short
        {
            get
            {
                return @"SELECT T.* FROM MED_VIEW_SHUXUEQIANSIXIANG_EXT T 
                        WHERE TRUNC(T.""检验日期"") >= TRUNC(:STARTDATE) 
                        AND TRUNC(T.""检验日期"") <=TRUNC(:ENDDATE)";
            }
        }

        /// <summary>
        /// 甲状旁腺素
        /// </summary>
        public string MED_VIEW_JIAZHUANGPANGXIAN_EXT_Long
        {
            get
            {
                return @"SELECT T.* FROM MED_VIEW_JIAZHUANGPANGXIAN_EXT T
                        WHERE TRUNC(T.""检验日期"") >= TRUNC(:STARTDATE)
                        AND TRUNC(T.""检验日期"") <= TRUNC(:ENDDATE)
                        AND (T.""姓名"" LIKE '%' || :PATIENTINFO || '%' 
                        OR T.""透析号"" LIKE '%' || :PATIENTINFO || '%' 
                        OR UPPER(T.INPUT_CODE) LIKE UPPER('%' || :PATIENTINFO || '%'))";
            }
        }

        /// <summary>
        /// 甲状旁腺素
        /// </summary>
        public string MED_VIEW_JIAZHUANGPANGXIAN_EXT_Short
        {
            get
            {
                return @"SELECT T.* FROM MED_VIEW_JIAZHUANGPANGXIAN_EXT T 
                        WHERE TRUNC(T.""检验日期"") >= TRUNC(:STARTDATE) 
                        AND TRUNC(T.""检验日期"") <=TRUNC(:ENDDATE)";
            }
        }

        /// <summary>
        /// 乙肝两对半
        /// </summary>
        public string MED_VIEW_YIGANLIANGDUIBAN_EXT_Long
        {
            get
            {
                return @"SELECT T.* FROM MED_VIEW_YIGANLIANGDUIBAN_EXT T
                        WHERE TRUNC(T.""检验日期"") >= TRUNC(:STARTDATE)
                        AND TRUNC(T.""检验日期"") <= TRUNC(:ENDDATE)
                        AND (T.""姓名"" LIKE '%' || :PATIENTINFO || '%' 
                        OR T.""透析号"" LIKE '%' || :PATIENTINFO || '%' 
                        OR UPPER(T.INPUT_CODE) LIKE UPPER('%' || :PATIENTINFO || '%'))";
            }
        }

        /// <summary>
        /// 乙肝两对半
        /// </summary>
        public string MED_VIEW_YIGANLIANGDUIBAN_EXT_Short
        {
            get
            {
                return @"SELECT T.* FROM MED_VIEW_YIGANLIANGDUIBAN_EXT T 
                        WHERE TRUNC(T.""检验日期"") >= TRUNC(:STARTDATE) 
                        AND TRUNC(T.""检验日期"") <=TRUNC(:ENDDATE)";
            }
        }

        public string MED_MED_VIEW_GANYANXILIE_EXT_long
        {
            get
            {
                return @"select t.* from MED_VIEW_GANYANXILIE_EXT t where  trunc(t.""检验日期"") >= trunc(:STARTDATE)  and
                         trunc(t.""检验日期"") <=trunc(:ENDDATE) and (t.""姓名"" like '%'||:PATIENTINFO||'%' or t.""透析号"" like '%'||:PATIENTINFO||'%'
                           or upper(t.input_code) like upper('%'||:PATIENTINFO||'%'))";
            }
        }

        public string MED_MED_VIEW_GANYANXILIE_EXT_short
        {
            get
            {
                return @"select t.* from MED_VIEW_GANYANXILIE_EXT t where  trunc(t.""检验日期"") >= trunc(:STARTDATE)  and
                         trunc(t.""检验日期"") <=trunc(:ENDDATE)";
            }
        }

        public string MED_VIEW_KONGFUXUETANG_EXT_long
        {
            get
            {
                return @"select t.* from MED_VIEW_KONGFUXUETANG_EXT t where  trunc(t.""检验日期"") >= trunc(:STARTDATE)  and
                         trunc(t.""检验日期"") <=trunc(:ENDDATE) and (t.""姓名"" like '%'||:PATIENTINFO||'%' or t.""透析号"" like '%'||:PATIENTINFO||'%'
                           or upper(t.input_code) like upper('%'||:PATIENTINFO||'%'))";
            }
        }

        public string MED_VIEW_KONGFUXUETANG_EXT_short
        {
            get
            {
                return @"select t.* from MED_VIEW_KONGFUXUETANG_EXT t where  trunc(t.""检验日期"") >= trunc(:STARTDATE)  and
                         trunc(t.""检验日期"") <=trunc(:ENDDATE)";
            }
        }

        public string MED_VIEW_XUEZHICHECK_EXT_long
        {
            get
            {
                return @"select t.* from MED_VIEW_XUEZHICHECK_EXT t where  trunc(t.""检验日期"") >= trunc(:STARTDATE)  and
                         trunc(t.""检验日期"") <=trunc(:ENDDATE) and (t.""姓名"" like '%'||:PATIENTINFO||'%' or t.""透析号"" like '%'||:PATIENTINFO||'%'
                           or upper(t.input_code) like upper('%'||:PATIENTINFO||'%'))";
            }
        }

        public string MED_VIEW_XUEZHICHECK_EXT_short
        {
            get
            {
                return @"select t.* from MED_VIEW_XUEZHICHECK_EXT t where  trunc(t.""检验日期"") >= trunc(:STARTDATE)  and
                         trunc(t.""检验日期"") <=trunc(:ENDDATE)";
            }
        }

        public string MED_VIEW_TIEWUXIANG_EXT_long
        {
            get
            {
                return @"select t.* from MED_VIEW_TIEWUXIANG_EXT t where  trunc(t.""检验日期"") >= trunc(:STARTDATE)  and
                         trunc(t.""检验日期"") <=trunc(:ENDDATE) and (t.""姓名"" like '%'||:PATIENTINFO||'%' or t.""透析号"" like '%'||:PATIENTINFO||'%'
                           or upper(t.input_code) like upper('%'||:PATIENTINFO||'%'))";
            }
        }

        public string MED_VIEW_TIEWUXIANG_EXT_short
        {
            get
            {
                return @"select t.* from MED_VIEW_TIEWUXIANG_EXT t where  trunc(t.""检验日期"") >= trunc(:STARTDATE)  and
                         trunc(t.""检验日期"") <=trunc(:ENDDATE)";
            }
        }

        public string MED_VIEW_XUEHONGDANBAI_EXT_long
        {
            get
            {
                return @"SELECT * FROM med_vw_xuehongdanbai_ext t where trunc(t.""检验日期"") >=trunc(:STARTDATE) and trunc(t.""检验日期"") <=trunc(:ENDDATE)
                        and (t.""姓名"" like '%'||:PATIENTINFO||'%' or t.""透析号"" like '%'||:PATIENTINFO||'%'
                           or upper(t.input_code) like upper('%'||:PATIENTINFO||'%'))"; 
            }
        }

        public string MED_VIEW_XUEHONGDANBAI_EXT_short
        {
            get
            {
                return @"SELECT * FROM med_vw_xuehongdanbai_ext t where trunc(t.""检验日期"") >=trunc(:STARTDATE) and trunc(t.""检验日期"") <=trunc(:ENDDATE)
                        ";
            }
        }
        #endregion

        #region 设备管理、设备保养、设备维修相关
        public string DeleteMED_EQUIPMENT_MGR
        {
            get
            {
                return @"DELETE FROM MED_EQUIPMENT_MGR WHERE id =:id";
            }
        }

        public string SelectMED_EQUIPMENT_MGRByTime
        {
            get
            {
                return @"SELECT * FROM MED_EQUIPMENT_MGR WHERE TRUNC(STARTDATE)>= TRUNC(:beginTime) AND TRUNC(STARTDATE)<= TRUNC(:endTime)";
            }
        }

        public string DeleteMED_EQUIPMENT_MAINTENANCE
        {
            get
            {
                return @"DELETE FROM MED_EQUIPMENT_MAINTENANCE WHERE id =:id";
            }
        }

        public string SelectMED_EQUIPMENT_MAINTENANCEByTime
        {
            get
            {
                return @"SELECT * FROM MED_EQUIPMENT_MAINTENANCE WHERE TRUNC(MAINTAINTIME)>= TRUNC(:beginTime) AND TRUNC(MAINTAINTIME)<= TRUNC(:endTime)";
            }
        }


        public string DeleteMED_EQUIPMENT_REPAIR
        {
            get
            {
                return @"DELETE FROM MED_EQUIPMENT_REPAIR WHERE id =:id";
            }
        }

        public string SelectMED_EQUIPMENT_REPAIRByTime
        {
            get
            {
                return @"SELECT * FROM MED_EQUIPMENT_REPAIR WHERE TRUNC(REPAIRTIME)>= TRUNC(:beginTime) AND TRUNC(REPAIRTIME)<= TRUNC(:endTime)";
            }
        }
        #endregion

        /// <summary>
        /// 根据时间、透析次数获取透析患者
        /// </summary>
        public string GetCurePatientByTimeAndFrequency
        {
            get
            {
                return @"SELECT T.HEMODIALYSIS_ID,P.NAME,TO_CHAR(CURE_CREATE_DATE, 'YYYY-MM-DD') || '～' ||TO_CHAR(CURE_CREATE_DATE + 5, 'YYYY-MM-DD') AS CURE_CREATE_DATE
                        FROM (SELECT DISTINCT TRUNC(CURE_CREATE_DATE, 'IW') CURE_CREATE_DATE,HEMODIALYSIS_ID
                        FROM MED_CURE_MAIN
                        WHERE TRUNC(CURE_CREATE_DATE) >= TRUNC(SYSDATE, 'IW') - :TIME * 7
                        AND TRUNC(CURE_CREATE_DATE) <= TRUNC(SYSDATE, 'IW') + 5
                        AND CURE_STATUS != '4'
                        GROUP BY HEMODIALYSIS_ID, TRUNC(CURE_CREATE_DATE, 'IW')
                        HAVING COUNT(HEMODIALYSIS_ID) = :FREQUENCY) T LEFT JOIN MED_PATIENTS P ON T.HEMODIALYSIS_ID=P.HEMODIALYSIS_ID
                        WHERE P.IS_DELETE!='1' OR P.IS_DELETE IS NULL
                        ORDER BY CURE_CREATE_DATE,NAME";
            }
        }

        /// <summary>
        /// 根据项名称和归属年份获取上传数据日志
        /// </summary>
        public string GetUploadLogByItemNameAndYear
        {
            get
            {
                return @"SELECT * FROM MED_UPLOAD_LOG T WHERE T.UPLOAD_ITEM_NAME=:UPLOAD_ITEM_NAME AND T.BELONG_YEAR=:BELONG_YEAR";
            }
        }
        public string GetBloodControlsReport
        {
            get
            {
                return @"SELECT Z.CREATE_MONTH,
                               SUM(Z.ALL_COUNT-Z.HIGHCOUNT-Z.LOWCOUNT) XYKZ_COUNT,
                               SUM(Z.HIGHCOUNT) HIGH_COUNT,
                               SUM(Z.LOWCOUNT) LOW_COUNT,
                               SUM(Z.ALL_COUNT) AS SUB_COUNT
                          FROM (SELECT TO_CHAR(CURE_CREATE_DATE, 'YYYY-MM') CREATE_MONTH,
                                       COUNT(1) XYKZ_COUNT,
                                       0 HIGHCOUNT,
                                       0 LOWCOUNT,
                                       0 ALL_COUNT
                                  FROM MED_CURE_MAIN T
                                 WHERE ((T.BEFORE_SYSTOLIC_PRESSURE >= 90 AND
                                       T.BEFORE_SYSTOLIC_PRESSURE <= 140) AND
                                       (T.BEFORE_DIASTOLIC_PRESSURE >= 60 and
                                       T.BEFORE_DIASTOLIC_PRESSURE <= 90))
                                   AND TRUNC(T.CURE_CREATE_DATE) >= TRUNC(:BEGINTIME)
                                   AND TRUNC(T.CURE_CREATE_DATE) <= TRUNC(:ENDTIME)
                                   AND T.CURE_STATUS != '4'
                                   AND T.HEMODIALYSIS_ID = :HEMODIALYSIS_ID
                                 GROUP BY TO_CHAR(CURE_CREATE_DATE, 'YYYY-MM')
                                union all
                                SELECT TO_CHAR(CURE_CREATE_DATE, 'YYYY-MM') CREATE_MONTH,
                                       0 XYKZ_COUNT,
                                       COUNT(1) HIGHCOUNT,
                                       0 LOWCOUNT,
                                       0 ALL_COUNT
                                  FROM MED_CURE_MAIN T
                                 WHERE T.BEFORE_SYSTOLIC_PRESSURE > 140
                                   AND TRUNC(T.CURE_CREATE_DATE) >= TRUNC(:BEGINTIME)
                                   AND TRUNC(T.CURE_CREATE_DATE) <= TRUNC(:ENDTIME)
                                   AND T.CURE_STATUS != '4'
                                   AND T.HEMODIALYSIS_ID = :HEMODIALYSIS_ID
                                 GROUP BY TO_CHAR(CURE_CREATE_DATE, 'YYYY-MM')
                                union all
                                SELECT TO_CHAR(CURE_CREATE_DATE, 'YYYY-MM') CREATE_MONTH,
                                       0 XYKZ_COUNT,
                                       0 HIGHCOUNT,
                                       COUNT(1) LOWCOUNT,
                                       0 ALL_COUNT
                                  FROM MED_CURE_MAIN T
                                 WHERE ((T.BEFORE_SYSTOLIC_PRESSURE < 90) or
                                   (T.BEFORE_SYSTOLIC_PRESSURE < 140 AND
                                   t.BEFORE_DIASTOLIC_PRESSURE < 60))
                                   AND TRUNC(T.CURE_CREATE_DATE) >= TRUNC(:BEGINTIME)
                                   AND TRUNC(T.CURE_CREATE_DATE) <= TRUNC(:ENDTIME)
                                   AND T.CURE_STATUS != '4'
                                   AND T.HEMODIALYSIS_ID = :HEMODIALYSIS_ID
                                 GROUP BY TO_CHAR(CURE_CREATE_DATE, 'YYYY-MM')
                                UNION ALL
                                SELECT TO_CHAR(CURE_CREATE_DATE, 'YYYY-MM') CREATE_MONTH,
                                       0 XYKZ_COUNT,
                                       0 HIGHCOUNT,
                                       0 LOWCOUNT,
                                       COUNT(1) ALL_COUNT
                                  FROM MED_CURE_MAIN T
                                 WHERE TRUNC(T.CURE_CREATE_DATE) >= TRUNC(:BEGINTIME)
                                   AND TRUNC(T.CURE_CREATE_DATE) <= TRUNC(:ENDTIME)
                                   AND T.CURE_STATUS != '4'
                                   AND T.HEMODIALYSIS_ID = :HEMODIALYSIS_ID
                                 GROUP BY TO_CHAR(CURE_CREATE_DATE, 'YYYY-MM')) Z
                         GROUP BY Z.CREATE_MONTH
                         ORDER BY Z.CREATE_MONTH";
            }
        }

        public string GetCommonItemListByItemType
        {
            get
            {
                return @"SELECT * FROM MED_COMMON_ITEMLIST WHERE ITEM_TYPE=:ITEM_TYPE";
            }
        }
        public string GetCureListByHemoId
        {
            get
            {
                return @"SELECT *
                          FROM MED_CURE_MAIN T
                         WHERE T.HEMODIALYSIS_ID = :HEMODIALYSIS_ID
                           AND TRUNC(T.CURE_CREATE_DATE) >= TRUNC(:CURE_CREATE_DATE)
                           AND T.CURE_STATUS != '4'";
            }
        }


        public string GetCureMainByHemoIdAndDate
        {
            get
            {
                return @"SELECT p.name,M.hemodialysis_id,purification_mode,item_name,dry_weight,before_dry_weight,dry_weight_tag 
                        FROM MED_CURE_MAIN m 
                        LEFT JOIN MED_COMMON_ITEMLIST ON purification_mode=item_id
                        LEFT JOIN MED_PATIENTS p ON p.hemodialysis_id=m.hemodialysis_id
                        WHERE TRUNC(CURE_CREATE_DATE)>= TRUNC(:BEGINTIME) 
                        AND TRUNC(CURE_CREATE_DATE)<=TRUNC(:ENDTIME)
                        AND M.PURIFICATION_MODE IS NOT NULL
                        AND hemodialysis_id =:HEMODIALYSIS_ID ";
            }
        }
        public string GetPatientByHemoId
        {
            get
            {
                return @"SELECT * FROM MED_PATIENTS WHERE hemodialysis_id=:HEMODIALYSIS_ID";
            }
        }

        public string GetCureVascularTypeCountByHemoIdAndDate
        {
            get
            {
                return @"SELECT TO_CHAR(T.CREATE_DATE, 'YYYY-MM') CREATE_MONTH,
                               SUM(DECODE(INSTR(T1.ITEM_NAME, '内瘘', 1, 1), 0, 0, 1)) +
                               SUM(DECODE(INSTR(T1.ITEM_NAME, '人造血管', 1, 1), 0, 0, 1)) NL_COUNT,
                               SUM(DECODE(INSTR(T1.ITEM_NAME, '置管', 1, 1), 0, 0, 1)) ZXJM_COUNT,
                               (SUM(DECODE(INSTR(T1.ITEM_NAME, '内瘘', 1, 1), 0, 0, 1)) +
                               SUM(DECODE(INSTR(T1.ITEM_NAME, '人造血管', 1, 1), 0, 0, 1)) +
                               SUM(DECODE(INSTR(T1.ITEM_NAME, '置管', 1, 1), 0, 0, 1))) AS SUB_COUNT
                          FROM MED_CURE_MAIN TT
                          LEFT JOIN MED_VASCULAR_ACCESS T
                            ON TT.VASCULAR_ACCESS_ID = T.VASCULAR_ACCESS_ID
                          LEFT JOIN MED_COMMON_ITEMLIST T1
                            ON T.VASCULAR_ACCESS_TYPE = T1.ITEM_ID
                         WHERE T.HEMODIALYSIS_ID = :HEMODIALYSIS_ID
                           AND T.IS_SUCCESS = '1'
                           AND TT.CURE_STATUS != '4'
                           AND (T1.ITEM_NAME LIKE '%置管%' OR T1.ITEM_NAME LIKE '%内瘘%' OR
                               T1.ITEM_NAME LIKE '%人造血管%')
                           AND TRUNC(TT.CURE_CREATE_DATE) >= TRUNC(:BEGINTIME)
                           AND TRUNC(TT.CURE_CREATE_DATE) <= TRUNC(:ENDTIME)
                         GROUP BY TO_CHAR(T.CREATE_DATE, 'YYYY-MM')";
            }
        }

        public string GetInfectiousCountByParams
        {
            get
            {
                return @"SELECT t.*
                          FROM MED_LAB_RESULT T
                         WHERE T.REPORT_ITEM_NAME LIKE '%'||:REPORT_ITEM_NAME|| '%'
                           AND T.REPORT_ITEM_CODE LIKE '%'||:REPORT_ITEM_CODE|| '%'
                           AND T.INSTRUMENT_ID = :HEMODIALYSIS_ID
                           AND TRUNC(T.RESULT_DATE_TIME)>TRUNC(:BEGINTIME)
                           AND TRUNC(T.RESULT_DATE_TIME)<TRUNC(:ENDTIME)";
            }
        }
        public string GetInfectiousCountByParamsExt
        {
            get
            {
                return @"  SELECT  t1.*
                               FROM MED_LAB_TEST_MASTER T
                               LEFT JOIN MED_LAB_RESULT T1
                                 ON T.TEST_NO = T1.TEST_NO
                              WHERE T.TEST_CAUSE LIKE '%' || :REPORT_ITEM_NAME ||'%'
                                AND T1.REPORT_ITEM_NAME LIKE '%'|| :REPORT_ITEM_CODE || '%'
                                AND T1.INSTRUMENT_ID = :HEMODIALYSIS_ID
                           AND TRUNC(T1.RESULT_DATE_TIME)>TRUNC(:BEGINTIME)
                           AND TRUNC(T1.RESULT_DATE_TIME)<TRUNC(:ENDTIME)";
            }
        }


        public string GetLabValueLineTypeByparams
        {
            get
            {
                return @"SELECT TO_CHAR(T.RESULT_DATE_TIME, 'YYYY-MM-DD') CREATE_MONTH,
                                   T.INSTRUMENT_ID,
                                   T.REPORT_ITEM_NAME,
                                   T.RESULT
                              FROM MED_LAB_RESULT T
                             WHERE T.REPORT_ITEM_NAME = :REPORT_ITEM_NAME
                               AND T.INSTRUMENT_ID = :HEMODIALYSIS_ID
                               AND TRUNC(T.RESULT_DATE_TIME) >= TRUNC(:BEGINTIME)
                               AND TRUNC(T.RESULT_DATE_TIME) <= TRUNC(:ENDTIME)
                             GROUP BY T.RESULT_DATE_TIME, T.REPORT_ITEM_NAME, RESULT, T.INSTRUMENT_ID
                             ORDER BY T.RESULT_DATE_TIME";
            }
        }
        public string GetLabValueLineTypeByparamsThree
        {
            get
            {
                return @"SELECT TO_CHAR(T.RESULT_DATE_TIME, 'YYYY-MM-DD') CREATE_MONTH,
                                   T.INSTRUMENT_ID,
                                   T.REPORT_ITEM_NAME,
                                   T.RESULT
                              FROM MED_LAB_RESULT T
                             WHERE T.REPORT_ITEM_NAME LIKE '%'|| :REPORT_ITEM_NAME||'%'
                               AND T.INSTRUMENT_ID = :HEMODIALYSIS_ID
                               AND TRUNC(T.RESULT_DATE_TIME) >= TRUNC(:BEGINTIME)
                               AND TRUNC(T.RESULT_DATE_TIME) <= TRUNC(:ENDTIME)
                             GROUP BY T.RESULT_DATE_TIME, T.REPORT_ITEM_NAME, RESULT, T.INSTRUMENT_ID
                             ORDER BY T.RESULT_DATE_TIME";
            }
        }
        public string GetLabValueLineTypeByparamsONE
        {
            get
            {
                return @"SELECT TO_CHAR(T.RESULT_DATE_TIME, 'YYYY-MM-DD') CREATE_MONTH,
                               T.INSTRUMENT_ID,
                               T.REPORT_ITEM_NAME,
                               T.RESULT
                          FROM MED_LAB_RESULT T
                          LEFT JOIN MED_LAB_TEST_MASTER T1
                            ON T.TEST_NO = T1.TEST_NO
                         WHERE T.REPORT_ITEM_NAME = :REPORT_ITEM_NAME
                           AND T.INSTRUMENT_ID = :HEMODIALYSIS_ID
                           AND TRUNC(T.RESULT_DATE_TIME) >= TRUNC(:BEGINTIME)
                           AND TRUNC(T.RESULT_DATE_TIME) <= TRUNC(:ENDTIME)
                           AND T1.TEST_CAUSE  LIKE  '%生化全套%' 
                         GROUP BY T.RESULT_DATE_TIME, T.REPORT_ITEM_NAME, RESULT, T.INSTRUMENT_ID
                         ORDER BY T.RESULT_DATE_TIME";
            }
        }
        public string GetLabValueLineTypeByparamsTWO
        {
            get
            {
                return @"SELECT TO_CHAR(T.RESULT_DATE_TIME, 'YYYY-MM-DD') CREATE_MONTH,
                               T.INSTRUMENT_ID,
                               T.REPORT_ITEM_NAME,
                               T.RESULT
                          FROM MED_LAB_RESULT T
                          LEFT JOIN MED_LAB_TEST_MASTER T1
                            ON T.TEST_NO = T1.TEST_NO
                         WHERE T.REPORT_ITEM_NAME = :REPORT_ITEM_NAME
                           AND T.INSTRUMENT_ID = :HEMODIALYSIS_ID
                           AND TRUNC(T.RESULT_DATE_TIME) >= TRUNC(:BEGINTIME)
                           AND TRUNC(T.RESULT_DATE_TIME) <= TRUNC(:ENDTIME)
                           AND (T1.TEST_CAUSE LIKE  '%生化全套%' or T1.TEST_CAUSE LIKE  '%肾功电解质（透前）%')
                         GROUP BY T.RESULT_DATE_TIME, T.REPORT_ITEM_NAME, RESULT, T.INSTRUMENT_ID
                         ORDER BY T.RESULT_DATE_TIME";
            }
        }

        public string GetVascularTypeCountByHemoIdAndDate
        {
            get
            {
                return @"SELECT TO_CHAR(T.CREATE_DATE, 'YYYY-MM') CREATE_MONTH,
                                 SUM(DECODE(INSTR(T1.ITEM_NAME, '内瘘', 1, 1), 0, 0, 1)) +
                                   SUM(DECODE(INSTR(T1.ITEM_NAME, '人造血管', 1, 1), 0, 0, 1)) NL_COUNT,
                                   SUM(DECODE(INSTR(T1.ITEM_NAME, '置管', 1, 1), 0, 0, 1)) ZXJM_COUNT,
                                   (SUM(DECODE(INSTR(T1.ITEM_NAME, '内瘘', 1, 1), 0, 0, 1)) +
                                   SUM(DECODE(INSTR(T1.ITEM_NAME, '人造血管', 1, 1), 0, 0, 1)) +
                                   SUM(DECODE(INSTR(T1.ITEM_NAME, '置管', 1, 1), 0, 0, 1))) AS SUB_COUNT
                      FROM MED_VASCULAR_ACCESS T
                      LEFT JOIN MED_COMMON_ITEMLIST T1
                        ON T.VASCULAR_ACCESS_TYPE = T1.ITEM_ID
                     WHERE T.HEMODIALYSIS_ID = :HEMODIALYSIS_ID
                       AND T.IS_SUCCESS = '1'
                       AND (T1.ITEM_NAME LIKE '%置管%' OR T1.ITEM_NAME LIKE '%内瘘%' OR T1.ITEM_NAME LIKE '%人造血管%')
                       AND TRUNC(T.CREATE_DATE) >= TRUNC(:BEGINTIME)
                       AND TRUNC(T.CREATE_DATE) <= TRUNC(:ENDTIME)
                     GROUP BY TO_CHAR(T.CREATE_DATE, 'YYYY-MM')";
            }
        }


        public string GetPatientExtendByParm
        {
            get
            {
                return @"SELECT SYS_GUID()|| '1' AS ID,T1.Hemodialysis_Id,
                                   T1.NAME,
                                   T1.SEX,
                                   floor(months_between(SYSDATE, t1.birthday) / 12) AS AGE,
                                   T1.SPECIFIC_TIME,
                                   t1.DIAGNOSE,
                                   t1.INFECTIOUS_CHECK_RESULT,
                                   T1.CREDENTIALS_NUMBER,
                                   T1.WORK_TELEPHONE,
                                   T1.ADDRESS,  T1.IS_NEW,
                                   T1.LEAVE_HOSPITAL_TIME,
                                   T1.CREATE_DATE AS CREATEDATE,
                                   '' AS VASCULARNAME,
                                   '' AS VASCULARTIME,
                                   DECODE(T1.IS_NEW,
                                          '0',
                                          '入科',
                                          '1',
                                          '死亡',
                                          '2',
                                          '转其它透析室',
                                          '3',
                                          '转腹透',
                                          '4',
                                          '肾移值',
                                          '5',
                                          '放弃治疗',
                                          '6',
                                          '暂不需要治疗')|| '/' || TO_CHAR(T1.LEAVE_HOSPITAL_TIME,'YYYY-MM-DD') DIRECTIONNAME
                              FROM MED_PATIENTS T1
                             WHERE (T1.HEMODIALYSIS_ID LIKE '%' || :PATIENTS || '%' OR
                                   T1.PATIENT_ID LIKE '%' || :PATIENTS || '%' OR
                                   T1.NAME LIKE '%' || :PATIENTS || '%' OR
                                   T1.INPUT_CODE LIKE '%' || :PATIENTS || '%')
                                 ORDER BY T1.Hemodialysis_Id DESC ,T1.NAME";

            }
        }

        public string GetHemoAgeByHemoID
        {
            get
            {
                return @"SELECT *
                          FROM (SELECT TO_CHAR(t.CURE_CREATE_DATE,'yyyy-MM-dd') HEMOAGE
                                  FROM MED_CURE_MAIN T
                                 WHERE T.HEMODIALYSIS_ID = :HEMODIALYSIS_ID
                                 ORDER BY T.CURE_CREATE_DATE DESC)
                         WHERE ROWNUM = 1";
            }
        }


        #region 上海普陀区人民医院术前护理评估单和患者转运记录单
        /// <summary>
        /// 根据病人姓名&日期获取临时留置静脉导管评估数据列表
        /// </summary>
        public string GetPreoperativeNursingByNameAndDate
        {
            get
            {
                return @"SELECT E.ID,
                        E.CREATE_DATE,
                        E.HEMODIALYSIS_ID,
                        DECODE(E.CONFIRM_IDENTITY_INFO,'T','已确认','未确认') as CONFIRM_IDENTITY_INFO,                        
                        E.WRIST_BAND,
                        E.OPERATION_NAME,
                        E.TEMPERATURE,
                        E.PULSE,
                        E.BREATH,
                        E.BLOOD_PRESURE,
                        DECODE(E.BLOOD_PREPARATION,'T','完成','无') as BLOOD_PREPARATION,
                        DECODE(E.SKIN_PREPARATION,'T','完成','无') as SKIN_PREPARATION,
                        DECODE(E.BOWEL_PREPARATION,'T','完成','无') as BOWEL_PREPARATION,
                        DECODE(E.FAST,'T','已宣教','无') as FAST,
                        DECODE(E.MENSTRUAL_PERIOD,'T','是','否') as MENSTRUAL_PERIOD,
                        DECODE(E.FALSE_TEETH,'T','有','无') as FALSE_TEETH,
                        DECODE(E.PREMEDICATION,'T','有','无') as PREMEDICATION,
                        DECODE(E.PREOPERATIVE_GUIDANCE,'T','完成','无') as PREOPERATIVE_GUIDANCE,
                        DECODE(E.MENTAL_STATE,'0','平稳','焦虑') as MENTAL_STATE,
                        E.OPERATING_ROOM_NO, 
                        DECODE(E.SPESIAL_INFECTION,'T','有','无') as SPESIAL_INFECTION,
                        DECODE(E.MENTAL_NURSING,'T','完成','未完成') as MENTAL_NURSING,
                        DECODE(E.PREOPERATIVE_EXAMINATION,'T','完成','无') as PREOPERATIVE_EXAMINATION,
                        DECODE(E.OPERATION_MARK,'L','左','右') as OPERATION_MARK,                                               
                        E.NURSE
                        FROM MED_PREOPERATIVE_NURSING E
                        LEFT JOIN MED_PATIENTS P ON E.HEMODIALYSIS_ID=P.HEMODIALYSIS_ID
                        WHERE E.CREATE_DATE>=:BEGINDATE AND E.CREATE_DATE<=:ENDDATE
                        AND E.IS_DELETE='0'
                        AND (P.NAME LIKE '%'||:NAME||'%' OR P.INPUT_CODE LIKE '%'||:NAME||'%') AND P.IS_DELETE='0'";                
            }
        }

        /// <summary>
        /// 根据病人透析编号&日期获取长期留置静脉导管评估数据列表
        /// </summary>
        public string GetPreoperativeNursingByHemoIdAndDate
        {
            get
            {
                return @"SELECT E.ID,
                        E.CREATE_DATE,
                        E.HEMODIALYSIS_ID,
                        DECODE(E.CONFIRM_IDENTITY_INFO,'T','已确认','未确认') as CONFIRM_IDENTITY_INFO,                        
                        E.WRIST_BAND,
                        E.OPERATION_NAME,
                        E.TEMPERATURE,
                        E.PULSE,
                        E.BREATH,
                        E.BLOOD_PRESURE,
                        DECODE(E.BLOOD_PREPARATION,'T','完成','无') as BLOOD_PREPARATION,
                        DECODE(E.SKIN_PREPARATION,'T','完成','无') as SKIN_PREPARATION,
                        DECODE(E.BOWEL_PREPARATION,'T','完成','无') as BOWEL_PREPARATION,
                        DECODE(E.FAST,'T','已宣教','无') as FAST,
                        DECODE(E.MENSTRUAL_PERIOD,'T','是','否') as MENSTRUAL_PERIOD,
                        DECODE(E.FALSE_TEETH,'T','有','无') as FALSE_TEETH,
                        DECODE(E.PREMEDICATION,'T','有','无') as PREMEDICATION,
                        DECODE(E.PREOPERATIVE_GUIDANCE,'T','完成','无') as PREOPERATIVE_GUIDANCE,
                        DECODE(E.MENTAL_STATE,'0','平稳','焦虑') as MENTAL_STATE,
                        E.OPERATING_ROOM_NO, 
                        DECODE(E.SPESIAL_INFECTION,'T','有','无') as SPESIAL_INFECTION,
                        DECODE(E.MENTAL_NURSING,'T','完成','未完成') as MENTAL_NURSING,
                        DECODE(E.PREOPERATIVE_EXAMINATION,'T','完成','无') as PREOPERATIVE_EXAMINATION,
                        DECODE(E.OPERATION_MARK,'L','左','右') as OPERATION_MARK,                                               
                        E.NURSE
                        FROM MED_PREOPERATIVE_NURSING E                        
                        WHERE E.CREATE_DATE>=:BEGINDATE AND E.CREATE_DATE<=:ENDDATE
                        AND E.IS_DELETE='0'
                        AND E.HEMODIALYSIS_ID=:HEMODIALYSIS_ID";
            }
        }

        /// <summary>
        /// 根据病人姓名&日期获取临时留置静脉导管评估数据列表
        /// </summary>
        public string GetPatTransferHandoverByNameAndDate
        {
            get
            {
                return @"SELECT E.ID,
                        E.CREATE_DATE,
                        E.HEMODIALYSIS_ID,
                        E.SENSE,
                        E.PULSE,
                        E.BREATH,
                        E.BLOOD_PRESURE,
                        E.TUBE,
                        E.INFUSION_TUBE,
                        DECODE(E.SKIN_COMPLETE,'T','是','否') as SKIN_COMPLETE,
                        DECODE(E.GRUG_HANDOVER,'T','是','否') as GRUG_HANDOVER,
                        E.GRUG_HANDOVER,
                        E.INFUSION_BOTTLE,
                        E.BLOOD_BAG,
                        E.X_RAY,
                        E.CT,
                        E.MRI,
                        E.CATHETERIZATION_PACKER,
                        E.GASTRIC_TUBE,
                        E.BELLYBAND,
                        E.OPERATION_MARK,
                        E.OUT_DEPT,
                        E.OUT_DEPT_SIGN,
                        E.IN_DEPT,
                        E.IN_DEPT_SIGN,
                        E.SKIN_STATE
                        FROM MED_PAT_TRANSFER_HANDOVER E
                        LEFT JOIN MED_PATIENTS P ON E.HEMODIALYSIS_ID=P.HEMODIALYSIS_ID
                        WHERE E.CREATE_DATE>=:BEGINDATE AND E.CREATE_DATE<=:ENDDATE
                        AND E.IS_DELETE='0'
                        AND (P.NAME LIKE '%'||:NAME||'%' OR P.INPUT_CODE LIKE '%'||:NAME||'%') AND P.IS_DELETE='0'";
            }
        }

        /// <summary>
        /// 根据病人透析编号&日期获取长期留置静脉导管评估数据列表
        /// </summary>
        public string GetPatTransferHandoverByHemoIdAndDate
        {
            get
            {
                return @"SELECT E.ID,
                        E.CREATE_DATE,
                        E.HEMODIALYSIS_ID,
                        E.SENSE,
                        E.PULSE,
                        E.BREATH,
                        E.BLOOD_PRESURE,
                        E.TUBE,
                        E.INFUSION_TUBE,
                        E.SKIN_COMPLETE,
                        E.GRUG_HANDOVER,
                        E.GRUG_HANDOVER,
                        E.INFUSION_BOTTLE,
                        E.BLOOD_BAG,
                        E.X_RAY,
                        E.CT,
                        E.MRI,
                        E.CATHETERIZATION_PACKER,
                        E.GASTRIC_TUBE,
                        E.BELLYBAND,
                        E.OPERATION_MARK,
                        E.OUT_DEPT,
                        E.OUT_DEPT_SIGN,
                        E.IN_DEPT,
                        E.IN_DEPT_SIGN,
                        E.SKIN_STATE
                        FROM MED_PAT_TRANSFER_HANDOVER E
                        WHERE E.CREATE_DATE>=:BEGINDATE AND E.CREATE_DATE<=:ENDDATE
                        AND E.IS_DELETE='0'
                        AND E.HEMODIALYSIS_ID=:HEMODIALYSIS_ID";
            }
        }

        /// <summary>
        /// 根据ID删除患者术前护理评估
        /// </summary>
        public string DeletePreoperativeNursingById
        {
            get
            {
                return @"UPDATE MED_PREOPERATIVE_NURSING SET IS_DELETE='1' WHERE ID=:ID";
            }
        }

        /// <summary>
        /// 根据ID删除患者转运交接单信息
        /// </summary>
        public string DeletePatTransferHandoverById
        {
            get
            {
                return @"UPDATE MED_PAT_TRANSFER_HANDOVER SET IS_DELETE='1' WHERE ID=:ID";
            }
        }
        #endregion


        #region 操作审计日志相关SQL

        /// <summary>
        /// 新增操作审计日志
        /// </summary>
        public string InsertOperationLog
        {
            get
            {
                return @"INSERT INTO MED_OPERATION_LOG 
                    (LOG_ID, USER_ID, USER_NAME, LOGIN_NAME, OPERATION_TYPE, MODULE_NAME, 
                     ELEMENT_NAME, ELEMENT_ID, CHANGE_DETAIL, CURE_ID, HEMODIALYSIS_ID, 
                     OPERATION_TIME, REMARK, CREATED_DATE)
                    VALUES 
                    (:LOG_ID, :USER_ID, :USER_NAME, :LOGIN_NAME, :OPERATION_TYPE, :MODULE_NAME, 
                     :ELEMENT_NAME, :ELEMENT_ID, :CHANGE_DETAIL, :CURE_ID, :HEMODIALYSIS_ID, 
                     SYSDATE, :REMARK, SYSDATE)";
            }
        }

        #endregion
    }
}
