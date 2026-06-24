/*----------------------------------------------------------------
      // Copyright (C) 2005 (苏州)医疗科技发展有限公司
      // 文件名：OleDbScriptProvider.cs
      // 文件功能描述：OleDbScriptProvider
      // 创建标识：顾伟伟-2011-01ConfigurationDA_GetExcelDtByPathID-14
----------------------------------------------------------------*/

namespace Hemo.DataAccess {
    public class OleDbScriptProvider {
        #region 病人数据相关SQL
        /// <summary>
        /// 根据病人姓名得到病人列表数据SQL
        /// </summary>
        public string GetPatientListByParams {
            get {
                return @"
                    SELECT 
                      p.*, 
                      (SELECT COUNT(1) FROM MED_HEMO_RECIPE r WHERE r.HEMODIALYSIS_ID = p.HEMODIALYSIS_ID) as RECIPECOUNT
                    FROM MED_PATIENTS p
                    WHERE 
                      (p.NAME like '%'||?||'%'  OR upper(p.INPUT_CODE) LIKE upper('%'||?||'%')) and p.HEMODIALYSIS_ID like '%'||?||'%'
                    ORDER BY
                      p.BED_NO";
            }
        }

        /// <summary>
        /// 得到病人列表数据SQL
        /// </summary>
        public string GetPatientList {
            get {
                return @"
                    SELECT 
                      p.*, 
                      (SELECT COUNT(1) FROM MED_HEMO_RECIPE r WHERE r.HEMODIALYSIS_ID = p.HEMODIALYSIS_ID) as RECIPECOUNT
                    FROM MED_PATIENTS p
                    ORDER BY
                      p.BED_NO";
            }
        }

        /// <summary>
        /// 得到病人列表数据SQL
        /// </summary>
        public string GetPatientListByType {
            get {
                return @"
                    SELECT 
                      p.*, 
                      (SELECT COUNT(1) FROM MED_HEMO_RECIPE r WHERE r.HEMODIALYSIS_ID = p.HEMODIALYSIS_ID) as RECIPECOUNT
                    FROM MED_PATIENTS p
                    WHERE 
                      p.TIME_TYPE = ?
                    ORDER BY
                      p.BED_NO";
            }
        }
        /// <summary>
        /// 得到新生成的血透号
        /// </summary>
        /// <returns></returns>
        public string GetNewHemoID {
            get {
                return "SELECT MAX(HEMODIALYSIS_ID)+1 AS HEMODIALYSIS_ID FROM MED_PATIENTS";
            }
        }


        /// <summary>
        /// 筛选病区，根据病人住院号同步获取病人信息
        /// </summary>
        /// <returns>得到病人住院信息</returns>
        public string GetPatientMasterIndexByPatientID {
            get {
                return @"select pat.*,hos.visit_id,hos.ADMISSION_DATE_TIME,dept.dept_name,
                         dept_ward.dept_name as ward_name,hos.bed_no,hos.diagnosis
                         from MED_PAT_MASTER_INDEX pat left join MED_PATS_IN_HOSPITAL hos on
                         pat.patient_id = hos.patient_id 
                         left join med_dept_dict dept on hos.DEPT_CODE = dept.dept_code
                         left join med_dept_dict dept_ward on hos.ward_code =  dept_ward.dept_code 
                         where hos.ward_code = ? and pat.PatientID= ?";
            }
        }

        /// <summary>
        /// 根据病区号码获取全部病人信息列表从HIS表中
        /// </summary>
        public string GetPatientMasterIndexList {
            get {
                return @"select pat.*,hos.visit_id,hos.ADMISSION_DATE_TIME,dept.dept_name,
                         dept_ward.dept_name as ward_name,hos.bed_no,hos.diagnosis
                         from MED_PAT_MASTER_INDEX pat left join MED_PATS_IN_HOSPITAL hos on
                         pat.patient_id = hos.patient_id 
                         left join med_dept_dict dept on hos.DEPT_CODE = dept.dept_code
                         left join med_dept_dict dept_ward on hos.ward_code = dept_ward.dept_code
                         where hos.ward_code = ?";
            }
        }

        public string GetPatientInfoByPatientID {
            get {
                return "select * from MED_PATIENTS where PATIENT_ID= ?";
            }
        }
        #endregion

        #region 血管通路
        /// <summary>
        /// 根据透析号得到病人血透通路日期列表
        /// </summary>
        public string GetVascuarAccessDateListByHemoID {
            get {//CREATE_DATE,VASCULAR_ACCESS_ID
                return "SELECT * FROM MED_VASCULAR_ACCESS WHERE HEMODIALYSIS_ID= ? ORDER BY CREATE_DATE DESC";
            }
        }

        /// <summary>
        /// 根据血管通路编号得到血管通路数据
        /// </summary>
        public string GetVascuarAccessListByID {
            get {
                return "SELECT * FROM MED_VASCULAR_ACCESS WHERE VASCULAR_ACCESS_ID= ?";
            }
        }
        #endregion

        #region 系统参数相关SQL

        /// <summary>
        /// 获取系统参数数据SQL
        /// </summary>
        public string GetConfigList {
            get {
                return @"
                    SELECT
                        t.*
                    FROM MED_COMMON_ITEMLIST t
                    WHERE 
                        t.ITEM_VALUE LIKE '%'||?||'%'
                        AND t.ITEM_NAME LIKE '%'||?ITEM_NAME||'%'
                        AND t.ITEM_TYPE = ?ITEM_TYPE
                        AND (t.STATUS = ?STATUS OR ?STATUS IS NULL)
                    ORDER BY 
                        t.ITEM_TYPE, t.ORDER_NUMBER";
//                return @"
//                    SELECT
//                        t.*,
//                        CASE
//                            WHEN t.STATUS = 0 THEN '停用'
//                            WHEN t.STATUS = 1 THEN '启用'      
//                        END STATUSSTR
//                    FROM MED_COMMON_ITEMLIST t
//                    WHERE 
//                        t.ITEM_VALUE LIKE '%'||?||'%'
//                        AND t.ITEM_NAME LIKE '%'||?ITEM_NAME||'%'
//                        AND t.ITEM_TYPE = ?ITEM_TYPE
//                        AND (t.STATUS = ?STATUS OR ?STATUS IS NULL)
//                    ORDER BY 
//                        t.ITEM_TYPE, t.ORDER_NUMBER";
            }
        }

        /// <summary>
        /// 获取系统参数类型数据SQL
        /// </summary>
        public string GetConfigTypeList {
            get {
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
                return @"SELECT DISTINCT S.*
                          FROM MED_VASCULAR_ACCESS T, MED_COMMON_ITEMLIST S
                         WHERE S.ITEM_TYPE = ?
                           AND (S.STATUS = '1' OR '1' IS NULL)
                           AND S.ITEM_ID = T.VASCULAR_ACCESS_TYPE
                           AND T.HEMODIALYSIS_ID = ?";
            }
        }
        #endregion

        #region 病人数据相关SQL

        /// <summary>
        /// 获取血透前病人信息数据SQL
        /// </summary>
        public string GetBeforeHemodialysisSignList {
            get {
                return "SELECT * FROM MED_BEFORE_HEMODIALYSIS_SIGN ORDER BY CREATE_DATE DESC";
            }
        }
        #endregion

        #region 药品设置相关SQL
        /// <summary>
        /// 得到全部药品主档列表 
        /// </summary>
        public string GetDrugMasterList {
            get {
                return "SELECT * FROM MED_DRUG_MASTER ORDER BY CREATE_DATE DESC";
            }
        }

        //  public string Get

        /// <summary>
        /// 根据查询条件，得到药品主档列表SQL
        /// </summary>
        public string GetDrugMasterListByParams {
            get {
                return
                    @"
                    SELECT
                        t.*
                    FROM MED_DRUG_MASTER t
                    WHERE 
                        t.DRUG_CODE LIKE '%'||?||'%'
                        and (t.DRUG_NAME LIKE '%'||?||'%' OR t.DRUG_PINYIN LIKE '%'||?||'%') 
                        and t.FIRM_ID LIKE '%'||?||'%'
                        ORDER BY 
                        t.CREATE_DATE DESC";
            }
        }


        /// <summary>
        /// 根据药品编号得到数据
        /// </summary>
        public string GetDrugMasterListByDrugCode {
            get {
                return "SELECT * FROM MED_DRUG_MASTER WHERE DRUG_CODE = ?";
            }
        }


        /// <summary>
        /// 得到新生成的药品编号
        /// </summary>
        /// <returns></returns>
        public string GetNewDrugCode {
            get {
                return "SELECT MAX(DRUG_CODE)+1 AS DRUG_CODE FROM MED_DRUG_MASTER";
            }
        }

        /// <summary>
        /// 得到全部药品厂商列表 
        /// </summary>
        public string GetDrugFirmList {
            get {
                return "SELECT * FROM MED_DRUG_FIRM";
            }
        }

        public string GetDrugFirmListByFirmType {
            get {
                return "SELECT * FROM MED_DRUG_FIRM WHERE FIRM_TYPE = ?";
            }
        }

        /// <summary>
        /// 根据查询条件，得到药厂列表SQL
        /// </summary>
        public string GetDrugFirmListByParams {
            get {
                return
                    @"
                    SELECT * FROM MED_DRUG_FIRM WHERE 
                    FIRM_ID LIKE '%'||?||'%'
                    and (FIRM_NAME LIKE '%'||?||'%' OR FIRM_PINYIN LIKE '%'||?||'%') 
                    and FIRM_ADDRESS LIKE '%'||?||'%'
                    and TELEPHONE  LIKE '%'||?||'%'
                    and MOBILE_PHONE LIKE '%'||?||'%' 
                    and FIRM_TYPE = ?
                    ";
            }
        }

        /// <summary>
        /// 根据药品厂商编号得到数据
        /// </summary>
        public string GetDrugFrimListByFirmID {
            get {
                return "SELECT * FROM MED_DRUG_FIRM WHERE FIRM_ID = ?";
            }
        }

        /// <summary>
        /// 得到新生成的药品厂商编号
        /// </summary>
        /// <returns></returns>
        public string GetNewFirmID {
            get {
                return "SELECT MAX(FIRM_ID)+1 AS FIRM_ID FROM MED_DRUG_FIRM";
            }
        }
        #endregion

        #region 医生资料设定相关SQL

        /// <summary>
        /// 获取医生资料设定数据SQL 
        /// </summary>
        public string GetStaffDictList {
            get {
                return @"
                    SELECT 
                      sd.*, d.DEPT_NAME, zy.ITEM_NAME ZYNAME, zc.ITEM_NAME ZCNAME, u.USER_NAME USERNAMESTR 
                    FROM 
                    MED_STAFF_DICT sd
                      INNER JOIN MED_DEPARTMENT d ON sd.DEPT_CODE = d.DEPT_ID
                      INNER JOIN (SELECT * FROM MED_COMMON_ITEMLIST il WHERE il.ITEM_TYPE = '职业' AND il.STATUS = '1') zy ON sd.JOB = zy.ITEM_ID  
                      INNER JOIN (SELECT * FROM MED_COMMON_ITEMLIST il WHERE il.ITEM_TYPE = '职称' AND il.STATUS = '1') zc ON sd.TITLE = zc.ITEM_ID    
                      INNER JOIN MED_USERS u ON sd.USER_NAME = u.USER_ID  
                    ORDER BY 
                      sd.EMP_NO";
            }
        }

        /// <summary>
        /// 获取最新的人员编号SQL
        /// </summary>
        public string GetNewEMPNO {
            get {
                return "SELECT MAX(EMP_NO) + 1 AS NewEMPNO FROM MED_STAFF_DICT";
            }
        }

        #endregion

        #region 科室相关SQL

        /// <summary>
        /// 获取科室数据SQL 
        /// </summary>
        public string GetDeptList {
            get {
                return "SELECT * FROM MED_DEPARTMENT WHERE STATUS = '1' ORDER BY DEPT_NAME";
            }
        }

        #endregion

        #region 用户相关SQL

        /// <summary>
        /// 获取用户数据SQL 
        /// </summary>
        public string GetUserList {
            get {
                return "SELECT * FROM MED_USERS WHERE IS_VALID = 'T' ORDER BY USER_NAME";
            }
        }

        /// <summary>
        /// 验证登录用户SQL 
        /// </summary>
        public string GetUserLogin {
            get {
                return "SELECT * FROM MED_USERS WHERE UPPER(LOGIN_NAME) = ? AND UPPER(LOGIN_PWD) = ? AND  IS_VALID = 'T'  ";
            }
        }

        /// <summary>
        /// 获取权限数据SQL 
        /// </summary>
        public string GetPermissionListByUserID {
            get {
                return @"
                    SELECT 
                      ur.USER_ID, rp.ROLE_ID, p.PERMISSION_ID, p.NAME PERMISSIONNAME
                    FROM 
                    MED_USERS_ROLES ur
                      INNER JOIN MED_ROLES_PERMISSIONS rp ON ur.ROLE_ID = rp.ROLE_ID
                      INNER JOIN MED_PERMISSIONS p ON rp.PERMISSION_ID = p.PERMISSION_ID  
                    WHERE
                      ur.USER_ID = ?
                    ORDER BY
                      p.SORT_ID";
            }
        }

        /// <summary>
        /// 获取用户和区域关系映射数据SQL
        /// </summary>
        public string GetUserAreaMappingList {
            get {
                return "SELECT * FROM MED_USERAREA_MAPPING WHERE USER_ID = ?";
            }
        }

        /// <summary>
        /// 删除用户和区域关系映射数据SQL
        /// </summary>
        public string DeleteUserAreaMappingInfo {
            get {
                return "DELETE FROM MED_USERAREA_MAPPING WHERE USER_ID = ?";
            }
        }

        /// <summary>
        /// 获取用户有权限访问的区域数据SQL
        /// </summary>
        public string GetAreaList {
            get {
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
                          AND m.USER_ID = ?
                      )";
            }
        }

        #endregion

        #region 耗材维护相关SQL
        /// <summary>
        /// 得到耗材数量
        /// </summary>
        public string GetMaterialMasterCount {
            get {
                return "SELECT COUNT(*) FROM MED_MATERIAL_MASTER";
            }
        }

        /// <summary>
        /// 得到耗材资料列表 
        /// </summary>
        public string GetMaterialMasterList {
            get {
                return "SELECT * FROM MED_MATERIAL_MASTER ORDER BY CREATE_DATE DESC";
            }
        }

        /// <summary>
        /// 根据查询条件，得到耗材资料列表SQL
        /// </summary>
        public string GetMaterialMasterListByParams {
            get {
                return
                    @"
                    SELECT
                        t.*
                    FROM MED_MATERIAL_MASTER t
                    WHERE 
                        t.MATERIAL_ID LIKE '%'||?||'%'
                        and (t.MATERIAL_NAME LIKE '%'||?||'%' OR t.MATERIAL_PINYIN LIKE '%'||?||'%') 
                        and t.FIRM_NAME LIKE '%'||?||'%'
                        ORDER BY 
                        t.CREATE_DATE DESC";
            }
        }

        /// <summary>
        /// 根据耗材编号得到数据
        /// </summary>
        public string GetMaterialMasterListByMaterialID {
            get {
                return "SELECT * FROM MED_MATERIAL_MASTER WHERE MATERIAL_ID = ?";
            }
        }

        /// <summary>
        /// 得到新生成的耗材编号
        /// </summary>
        /// <returns></returns>
        public string GetNewMaterialID {
            get {
                return "SELECT MAX(MATERIAL_ID)+1 AS MATERIAL_ID FROM MED_MATERIAL_MASTER";
            }
        }

        /// <summary>
        /// 根据耗材领用编号得到耗材保镖数据
        /// </summary>
        public string GetMaterialReport {
            get {
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
                )   m_report where use_material_id = ?
                ";
            }
        }

        /// <summary>
        /// 根据透析号得到耗材领用列表
        /// </summary>
        public string GetUseMaterialList {
            get {
                return @"select * from med_hemo_material where hemodialysis_id=? order by create_date desc";
            }
        }
        #endregion

        #region 血透机相关SQL

        /// <summary>
        /// 获取血透机数据SQL 
        /// </summary>
        public string GetMachineList {
            get {
                return @"
                    SELECT 
                      dm.*, fl.ITEM_NAME FLNAME, qy.ITEM_NAME QYNAME, cw.ITEM_NAME CWNAME
                    FROM 
                    MED_DIALYSIS_MACHINE dm
                      INNER JOIN (SELECT * FROM MED_COMMON_ITEMLIST il WHERE il.ITEM_TYPE = '血透机品牌' AND il.STATUS = '1') fl ON dm.TYPE = fl.ITEM_ID  
                      LEFT JOIN (SELECT * FROM MED_COMMON_ITEMLIST il WHERE il.ITEM_TYPE = '区域' AND il.STATUS = '1') qy ON dm.AREA_ID = qy.ITEM_ID   
                      LEFT JOIN (SELECT * FROM MED_COMMON_ITEMLIST il WHERE il.ITEM_TYPE = '床位' AND il.STATUS = '1') cw ON dm.BED_ID = cw.ITEM_ID    
                    ORDER BY 
                      dm.MACHINE_NAME";
            }
        }

        #endregion

        #region 长期处方
        /// <summary>
        /// 获取全部长期处方数据SQL
        /// </summary>
        public string GetAllRecipe {
            get {
                return "SELECT r.*, item.item_name as PURIFICATION_MODE_NAME FROM MED_HEMO_RECIPE r left join med_common_itemlist item on r.PURIFICATION_MODE = item.item_id";
            }
        }

        /// <summary>
        /// 根据处方编号得到对应长期处方数据SQL
        /// </summary>
        public string GetRecipeByRecipeID {
            get {
                return "SELECT r.*, item.item_name as PURIFICATION_MODE_NAME FROM MED_HEMO_RECIPE r left join med_common_itemlist item on r.PURIFICATION_MODE = item.item_id WHERE r.RECIPE_ID = ?";
            }
        }

        /// <summary>
        /// 根据透析号得到对应长期处方数据SQL
        /// </summary>
        public string GetRecipeByHemodialysisID {
            get {
                return @"SELECT distinct MED_HEMO_RECIPE.*,item.item_name as PURIFICATION_MODE_NAME, '' as MACHINE_ID,
                        itemAccess.Item_Name as Vascular_Access_Name, case MED_HEMO_RECIPE.status when '1' then '已启用' else '未启用' end as STATUSNAME
                        FROM MED_HEMO_RECIPE  
                        left join med_common_itemlist item on MED_HEMO_RECIPE.PURIFICATION_MODE = item.item_id
                        left join med_patient_schedule sch on sch.RECIPE_ID = MED_HEMO_RECIPE.RECIPE_ID
                        left join med_common_itemlist itemMonitor on sch.bed_number = itemMonitor.item_id
                        left join med_common_itemlist itemAccess on MED_HEMO_RECIPE.VASCULAR_ACCESS_ID = itemAccess.item_id 
                        WHERE MED_HEMO_RECIPE.HEMODIALYSIS_ID = ?";
            }
        }

        /// <summary>
        /// 得到当天日期处方编号的数量
        /// </summary>
        public string GetRecipeIDCount {
            get {
                return "SELECT COUNT(1) FROM MED_HEMO_RECIPE WHERE SUBSTR(RECIPE_ID,0,10) = ?";
            }
        }

        /// <summary>
        /// 得到新生成的处方日期号
        /// </summary>
        public string GetNewRecipeID {
            get {
                return @"
                        SELECT to_char(sysdate,'yyyy-mm-dd')||'-'||(MAX(SUBSTR(RECIPE_ID,12,4))+1) as RECIPE_ID
                        FROM MED_HEMO_RECIPE WHERE SUBSTR(RECIPE_ID,0,10) = ?";
            }
        }


        /// <summary>
        /// 在治疗单页面，未开始治疗时读取默认处方信息的内容
        /// </summary>
        public string GetRecipeInfoInCureFunction {
            get {
                return @"
                  SELECT r.*, item.item_name as PURIFICATION_MODE_NAME,
                  v.item_name as VASCULAR_ACCESS_NAME,m.item_name as MACHINE_TYPE_NAME,
                  ms.machine_name  FROM MED_HEMO_RECIPE r left join
                  med_common_itemlist item on r.PURIFICATION_MODE = item.item_id
                  left join med_common_itemlist v on r.vascular_access_id = v.item_id
                  left join med_common_itemlist m on r.FIRST_PURIFIER_MODEL = m.item_id
                  left join MED_PATIENT_SCHEDULE sch on r.recipe_id = sch.recipe_id
                  left join  MED_DIALYSIS_MACHINE ms on sch.MONITOR_LABEL =  ms.machine_id
                  WHERE r.RECIPE_ID = ? 
                ";
            }
        }
        #endregion

        #region 病患排班相关SQL

        /// <summary>
        /// 获取病患排班数据SQL
        /// </summary>
        public string GetPatientScheduleSignle {
            get {
                return @"
                    SELECT 
                      s.*
                    FROM MED_PATIENT_SCHEDULE s
                    WHERE
                      1 = 1
                      AND s.DIALYSIS_DATE like to_date(?,'yyyy-mm-dd') AND s.HEMODIALYSIS_ID = ?";
            }
        }

        /// <summary>
        /// 获取病患排班数据SQL 
        /// </summary>
        public string GetPatientScheduleList {
            get {
                return @"
                    SELECT 
                      s.*, p.NAME PATIENTNAME, qy.ITEM_NAME AREANAME, cw.ITEM_NAME BEDNAME, m.MACHINE_NAME
                    FROM MED_PATIENT_SCHEDULE s
                    INNER JOIN MED_PATIENTS p ON s.HEMODIALYSIS_ID = p.HEMODIALYSIS_ID
                    INNER JOIN (SELECT * FROM MED_COMMON_ITEMLIST il WHERE il.ITEM_TYPE = '区域' AND il.STATUS = '1') qy ON s.DIALYSIS_ROOM_ID = qy.ITEM_ID  
                    INNER JOIN (SELECT * FROM MED_COMMON_ITEMLIST il WHERE il.ITEM_TYPE = '床位' AND il.STATUS = '1') cw ON s.BED_NUMBER = cw.ITEM_ID
                    INNER JOIN MED_DIALYSIS_MACHINE m ON s.MONITOR_LABEL = m.MACHINE_ID
                    WHERE
                      1 = 1
                      AND s.DIALYSIS_DATE >= ?
                      AND s.DIALYSIS_DATE <= ?
                      AND s.BANCI_ID = ?
                      AND EXISTS
                      (
                        SELECT 
                            1 
                        FROM MED_USERAREA_MAPPING m 
                        WHERE 
                            s.DIALYSIS_ROOM_ID = m.AREA_ID 
                            AND m.USER_ID LIKE '%'||?||'%'
                        )
                    ORDER BY 
                      qy.ORDER_NUMBER, m.MACHINE_NAME, s.BANCI_ID";
            }
        }

        /// <summary>
        /// 获取病患排班数据SQL 
        /// </summary>
        public string GetPatientScheduleList4Report {
            get {
                return @"
                    SELECT 
                      s.*, p.NAME PATIENTNAME, qy.ITEM_NAME AREANAME, cw.ITEM_NAME BEDNAME, m.MACHINE_NAME
                    FROM MED_PATIENT_SCHEDULE s
                    INNER JOIN MED_PATIENTS p ON s.HEMODIALYSIS_ID = p.HEMODIALYSIS_ID
                    INNER JOIN (SELECT * FROM MED_COMMON_ITEMLIST il WHERE il.ITEM_TYPE = '区域' AND il.STATUS = '1') qy ON s.DIALYSIS_ROOM_ID = qy.ITEM_ID  
                    INNER JOIN (SELECT * FROM MED_COMMON_ITEMLIST il WHERE il.ITEM_TYPE = '床位' AND il.STATUS = '1') cw ON s.BED_NUMBER = cw.ITEM_ID
                    INNER JOIN MED_DIALYSIS_MACHINE m ON s.MONITOR_LABEL = m.MACHINE_ID
                    WHERE
                      1 = 1
                      AND s.DIALYSIS_DATE >= ?
                      AND s.DIALYSIS_DATE <= ?
                    ORDER BY 
                      qy.ORDER_NUMBER, m.MACHINE_NAME, s.BANCI_ID";
            }
        }


        /// <summary>
        /// 根据条件，获取病患排班数据SQL 
        /// </summary>
        public string GetPatientScheduleByParames {
            get {
                return @"
                    SELECT 
                      s.*, p.NAME PATIENTNAME, qy.ITEM_NAME AREANAME, cw.ITEM_NAME BEDNAME, m.MACHINE_NAME
                    FROM MED_PATIENT_SCHEDULE s
                    INNER JOIN MED_PATIENTS p ON s.HEMODIALYSIS_ID = p.HEMODIALYSIS_ID
                    INNER JOIN (SELECT * FROM MED_COMMON_ITEMLIST il WHERE il.ITEM_TYPE = '区域' AND il.STATUS = '1') qy ON s.DIALYSIS_ROOM_ID = qy.ITEM_ID  
                    INNER JOIN (SELECT * FROM MED_COMMON_ITEMLIST il WHERE il.ITEM_TYPE = '床位' AND il.STATUS = '1') cw ON s.BED_NUMBER = cw.ITEM_ID
                    INNER JOIN MED_DIALYSIS_MACHINE m ON s.MONITOR_LABEL = m.MACHINE_ID
                    WHERE
                      1 = 1
                      AND s.DIALYSIS_DATE >= ?
                      AND s.DIALYSIS_DATE <= ?
                      AND m.AREA_ID LIKE '%'||?||'%'
                      AND s.BANCI_ID LIKE '%'||?||'%'
                      AND m.AREA_ID= ?
                      AND s.BANCI_ID = ?
                    ORDER BY 
                      qy.ORDER_NUMBER, m.MACHINE_NAME, s.BANCI_ID";
            }

        }



        /// <summary>
        /// 根据排班日期得到排班表数据 
        /// </summary>
        public string GetPatientListBySchedule {
            //AND r.status='1'
            //  left join med_hemo_recipe r on p.hemodialysis_id = r.hemodialysis_id
            get {
                return @"
                    SELECT 
                    p.*,'' as recipe_id,'' as purification_mode
                    FROM MED_PATIENT_SCHEDULE s
                    INNER JOIN MED_PATIENTS p ON s.HEMODIALYSIS_ID = p.HEMODIALYSIS_ID
                    INNER JOIN (SELECT * FROM MED_COMMON_ITEMLIST il WHERE il.ITEM_TYPE = '区域' AND il.STATUS = '1') qy ON s.DIALYSIS_ROOM_ID = qy.ITEM_ID  
                    INNER JOIN (SELECT * FROM MED_COMMON_ITEMLIST il WHERE il.ITEM_TYPE = '床位' AND il.STATUS = '1') cw ON s.BED_NUMBER = cw.ITEM_ID
                    INNER JOIN MED_DIALYSIS_MACHINE m ON s.MONITOR_LABEL = m.MACHINE_ID
                    WHERE
                      1 = 1
                      AND s.DIALYSIS_DATE = ?
                      AND m.AREA_ID LIKE '%'||?||'%'
                      AND s.BANCI_ID LIKE '%'||?||'%'               
                    ORDER BY 
                      qy.ORDER_NUMBER, m.MACHINE_NAME, s.BANCI_ID";
            }
        }



        /// <summary>
        /// 删除病患排班数据SQL
        /// </summary>
        public string DeletePatientSchedule {
            get {
                return @"
                    DELETE FROM MED_PATIENT_SCHEDULE t
                    WHERE
                        t.DIALYSIS_DATE >= ?
                        AND t.DIALYSIS_DATE <= ?
                        AND t.BANCI_ID = ?
                        AND EXISTS
                        (
                            SELECT 
                                1 
                            FROM MED_USERAREA_MAPPING m 
                            WHERE 
                                t.DIALYSIS_ROOM_ID = m.AREA_ID 
                                AND m.USER_ID = ?
                        )
                        AND (t.START_TIME IS NULL AND t.END_TIME IS NULL)";
            }
        }

        /// <summary>
        /// 根据模板ID获取病患排班数据SQL
        /// </summary>
        public string GetPatientScheduleListByTemplateID {
            get {
                return @"
                    SELECT 
                        MED_SCHEDULE_TEMPLATE_DATA_ID PATIENT_SCHEDULE_ID, NULL START_TIME, NULL END_TIME, t.* 
                    FROM MED_PATIENT_SCHEDULE_TEMP_DATA t
                    WHERE
                        1 = 1                                              
                        AND t.PATIENT_SCHEDULE_TEMPLATE_ID = ?";
            }
        }

        /// <summary>
        /// 获取病患排班模板数据SQL 
        /// </summary>
        public string GetPatientScheduleTemplateList {
            get {
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
                          td.BANCI_ID = ? 
                          AND td.PATIENT_SCHEDULE_TEMPLATE_ID = t.PATIENT_SCHEDULE_TEMPLATE_ID
                      ) 
                    ORDER BY t.PATIENT_SCHEDULE_TEMPLATE_NAME";
            }
        }

        /// <summary>
        /// 根据排班表透析ID和开始时间得到一条处方ID
        /// </summary>
        public string GetPatientScheduleRecipeIDByStartTime {
            get {
                return @"select * from med_patient_schedule  where hemodialysis_id= ?
                          and start_time like to_date(?,'yyyy-mm-dd')";
            }
        }
        #endregion

        #region 治疗单
        /// <summary>
        /// 根据治疗单编号得到对应治疗单数据SQL
        /// </summary>
        public string GetMainCureByCureID {
            get {
                return @"
                select t.*,i.item_name as vascular_access_name,m.item_name as machine_type_name,
                msd.name as doctor_name,msdn.name as nurse_name,machine.machine_name,z.item_name as PURIFICATION_MODE_NAME,j.item_name as purifier_new_name
                from med_cure_main t left join MED_COMMON_ITEMLIST z on t.PURIFICATION_MODE = z.item_id
                left join MED_COMMON_ITEMLIST i on t.vascular_access_id = i.item_id
                left join MED_COMMON_ITEMLIST m on t.machine_type = m.item_id left join med_staff_dict msd
                on t.primary_doctor = msd.emp_no 
                left join med_staff_dict msdn
                on t.PRIMARY_NURSE = msdn.emp_no 
                left join med_dialysis_machine machine 
                on t.machine_id = machine.machine_id 
                left join MED_COMMON_ITEMLIST j on t.purifier_name = j.item_id
                WHERE t.CURE_ID = ?
                ";
            }
        }

        /// <summary>
        /// 根据病人透析号得到对应治疗单数据SQL
        /// </summary>
        public string GetMainCureByHemoID {
            get {
                return "SELECT * FROM MED_CURE_MAIN WHERE HEMODIALYSIS_ID = ? ORDER BY CURE_CREATE_DATE DESC";
            }
        }

        /// <summary>
        /// 根据治疗单编号得到对应透析参数数据列表SQL
        /// </summary>
        public string GetHemoParametersByCureID {
            get {
                return @"SELECT  to_char(SYSTOLIC_PRESSURE)||'/'||to_char(DIASTOLIC_PRESSURE) as BLOOD_PRESSURE,HEMODIALYSIS_PARAMETERS_ID, CURE_ID, RECIPE_ID, CREATE_DATE, VENOUS_PRESSURE, 
                TRANSMEMBRANE_PRESSURE, TEMPERATURE, SYSTOLIC_PRESSURE, DIASTOLIC_PRESSURE, CARDIOTACH, 
                BREATH, KT_V, CURE_MODE, CLINICAL_MANIFESTATION, BLOOD_FLOW, SODIUM_ION, DIALYSATE_RATE, URF, 
                CONDUCTIVITY, NURSE_ID,DISPLACEMENT,VASCULAR_ACCESS_ERRHYISIS,VASCULAR_ACCESS_GLIDE,ANTICOAGULANT
                FROM  MED_HEMODIALYSIS_PARAMETERS
                WHERE CURE_ID = ? ORDER BY CREATE_DATE DESC";
            }
        }

        /// <summary>
        /// 得到对应透析参数数据列表
        /// </summary>
        public string GetHemoParameters {
            get {
                return @"
                    SELECT 
                      *
                    FROM MED_CURE_MAIN c
                      INNER JOIN MED_HEMODIALYSIS_PARAMETERS p ON c.CURE_ID = p.CURE_ID
                    WHERE
                      p.CREATE_DATE >= ?
                      AND p.CREATE_DATE <= ?
                      AND c.HEMODIALYSIS_ID = ?
                    ORDER BY
                      p.CREATE_DATE";
            }
        }

        public string GetHemoParametersType {
            get {
                return @"
                    SELECT 
                      *
                    FROM MED_HEMODIALYSIS_PARAMS_TYPE t
                    ORDER BY
                      t.GROUPID, t.SORT";
            }
        }

        /// <summary>
        /// 根据治疗单编号得到对应给药数据列表SQL
        /// </summary>
        public string GetCureDrugByCureID {
            get {
                return @"
                SELECT DRUG.*,I.ITEM_NAME AS UNIT_NAME,M.ITEM_NAME AS DRUG_MODE_NAME,d.drug_name as NEW_DRUG_NAME,s.NAME FROM MED_CURE_DRUG DRUG 
                LEFT JOIN MED_COMMON_ITEMLIST  I ON DRUG.DOSAGE_UNITS = I.ITEM_ID 
                LEFT JOIN  MED_COMMON_ITEMLIST M ON DRUG.DRUG_MODE = M.ITEM_ID
                left join med_drug_master d on drug.drug_name = TRIM(d.drug_code)
                left join med_staff_dict s on s.emp_no = drug.doctor_id
                WHERE DRUG.CURE_ID = ?
                ";
            }
        }

        /// <summary>
        /// 得到治疗单历史列表
        /// </summary>
        public string GetCureList {
            get {
                return @"
                select distinct  c_main.CURE_CREATE_DATE,t.dialysis_date,case t.banci_id when '1' then '上午班'  when '2' then '下午班'  when '3' then '晚班'  end as banci_name,
                room.item_name as room_name,comm.item_name as bed_name, t.hemodialysis_id,pat.name,
                pat.sex,pat.birthday,c_main.FREQUENCY_HOURS,c_main.PURIFICATION_MODE,c_main.cure_id from med_patient_schedule t 
                left join med_common_itemlist comm  on t.bed_number =  comm.item_id left join med_common_itemlist room on t.dialysis_room_id = room.item_id
                left join med_patients pat on pat.hemodialysis_id = t.hemodialysis_id left join MED_CURE_MAIN
                c_main on t.hemodialysis_id = c_main.hemodialysis_id 
                where c_main.CURE_CREATE_DATE like to_date(?,'yyyy-mm-dd') and t.banci_id like '%'||?||'%' and pat.name like '%'||?||'%' and pat.hemodialysis_id like '%'||?||'%'
                ";
            }
        }
        public string GetCureIDByHemoIDAndCureData
        {
            get
            {
                return @"  SELECT CURE_ID
                             FROM MED_CURE_MAIN
                            WHERE CURE_CREATE_DATE like to_date(?CURE_CREATE_DATE, 'yyyy-mm-dd')
                              AND HEMODIALYSIS_ID like '%' || ?HEMODIALYSIS_ID || '%'";

            }
        }
        /// <summary>
        /// 根据病人透析号和治疗方式分组得到治疗数据SQL
        /// </summary>
        public string GetMainCureGroupByHemoIDAndPurificationMode {
            get {
                return @"
                    SELECT 
                      t.HEMODIALYSIS_ID, t.PURIFICATION_MODE, (SELECT ITEM_NAME FROM MED_COMMON_ITEMLIST li WHERE li.ITEM_ID = t.PURIFICATION_MODE) PURIFICATION_MODE_NAME, COUNT(1) COUNT
                    FROM
                      MED_CURE_MAIN t
                    WHERE
                      1 = 1
                      AND t.CURE_CREATE_DATE >= ?
                      AND t.CURE_CREATE_DATE <= ?
                    GROUP BY 
                      t.HEMODIALYSIS_ID, t.PURIFICATION_MODE";
            }
        }

        /// <summary>
        /// 根据病人透析号和治疗单编号得到治疗单数量
        /// </summary>
        public string GetMainCureCountByCreateDate {
            get {
                return @"SELECT * FROM MED_CURE_MAIN WHERE HEMODIALYSIS_ID=? AND CURE_CREATE_DATE like to_date(?,'yyyy-mm-dd')";
            }
        }

        /// <summary>
        /// 得到治疗单打印列表 
        /// </summary>
        public string GetPrintCureList {
            get {
                return @"
                select  t.*,c.item_name as purification_mode_name,cm.cure_id,cm.cure_create_date from (
                select  m.item_name as bedName,p.name,p.sex,p.age,p.hemodialysis_id,r.purification_mode,r.frequency_hours,case t.banci_id when '1' then '上午'
                 when '2' then '下午' when '3' then '晚班' end as BanCiName,t.dialysis_date from med_patient_schedule t 
                inner join med_common_itemlist m on t.bed_number = m.item_id
                inner join med_patients p on p.hemodialysis_id = t.hemodialysis_id
                inner join med_hemo_recipe r on r.recipe_id = t.recipe_id
                where t.BANCI_ID LIKE '%'||?||'%' and t.hemodialysis_id LIKE '%'||?||'%' and P.NAME LIKE '%'||?||'%' and to_char(t.dialysis_date,'yyyy/mm/dd') like '%'||?||'%'
                order by t.dialysis_date desc
                ) t
                inner join 
                med_common_itemlist c on c.item_id = t.purification_mode
                 inner join MED_CURE_MAIN cm on cm.hemodialysis_id = t.hemodialysis_id
                where to_char(cm.cure_create_date,'yyyy/mm/dd') like '%{1}%'                
                ";
            }
        }

        /// <summary>
        /// 计算患者透析次数
        /// </summary>
        public string GetCureCountByHemoID {
            get {
                return "select count(1) +1 as curecount from med_cure_main where HEMODIALYSIS_ID = ?";
            }
        }
        #endregion

        #region 预约申请相关SQL

        /// <summary>
        /// 获取预约申请数据SQL 
        /// </summary>
        public string GetHemodialysisApplyList {
            get {
                return @"
                    SELECT 
                        * 
                    FROM MED_HEMO_APPLY t
                    WHERE
                        t.HEMODIALYSIS_ID = ?";
            }
        }

        /// <summary>
        /// 删除预约申请数据SQL
        /// </summary>
        public string DeleteHemodialysisApply {
            get {
                return @"
                    DELETE FROM MED_HEMO_APPLY t
                    WHERE
                        t.APPLY_ID = ?";
            }
        }

        #endregion

        #region 检验相关SQL

        /// <summary>
        /// 获取检验数据SQL 
        /// </summary>
        public string GetPatientLabList {
            get {
                return @"
                    SELECT
                      m.TEST_NO, m.SPECIMEN, m.RESULTS_RPT_DATE_TIME,
                      i.ITEM_NO, i.ITEM_NAME,
                      r.REPORT_ITEM_NAME, r.RESULT, r.UNITS, r.REFERENCE_RESULT
                    FROM MED_LAB_TEST_MASTER m
                    LEFT JOIN MED_LAB_TEST_ITEMS i ON m.TEST_NO = i.TEST_NO
                    LEFT JOIN MED_LAB_RESULT r ON m.TEST_NO = r.TEST_NO
                    WHERE
                      1 = 1
                      AND m.PATIENT_ID = ?
                    ORDER BY
                      m.TEST_NO, i.ITEM_NO";
            }
        }

        #endregion

        #region 医嘱相关SQL

        /// <summary>
        /// 获取医嘱数据SQL 
        /// </summary>
        public string GetOrderList {
            get {
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
                      AND o.PATIENT_ID = ? 
                      AND o.ENTER_DATE_TIME >= ? 
                      AND o.ENTER_DATE_TIME <= ?        
                      AND o.ORDER_CLASS = 'A' 
                      AND o.ORDER_STATUS = 2
                    ORDER BY o.ORDER_NO, o.ORDER_SUB_NO";
            }
        }

        /// <summary>
        /// 获取医嘱数据SQL 
        /// </summary>
        public string GetOrderList4Erythropoietin {
            get {
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
                      AND o.PATIENT_ID = ? 
                      AND o.ENTER_DATE_TIME >= ? 
                      AND o.ENTER_DATE_TIME <= ?        
                      AND o.ORDER_CLASS = 'A' 
                      AND o.ORDER_STATUS = 2
                    ORDER BY o.ORDER_NO, o.ORDER_SUB_NO";
            }
        }

        #endregion

        #region 促红素相关SQL

        /// <summary>
        /// 获取促红素数据SQL 
        /// </summary>
        public string GetErythropoietinList {
            get {
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
                      AND e.HEMODIALYSIS_ID = ?
                    ORDER BY
                      e.CREATE_TIME";
            }
        }

        /// <summary>
        /// 获取促红素数据SQL 
        /// </summary>
        public string GetErythropoietinListByTimeSpan {
            get {
                return @"
                    SELECT
                      e.*
                    FROM MED_ERYTHROPOIETIN e
                    WHERE
                      1 = 1
                      AND e.HEMODIALYSIS_ID = ?
                      AND e.CREATE_TIME >= ? 
                      AND e.CREATE_TIME <= ?";
            }
        }

        /// <summary>
        /// 获取促红素执行数据SQL 
        /// </summary>
        public string GetErythropoietinExecList {
            get {
                return @"
                    SELECT
                      x.*, e.ERYTHROPOIETIN_TYPE, dm.ITEM_NAME DRUG_MODESTR, e.DOSAGE, unit.ITEM_NAME UNITSTR
                    FROM MED_ERYTHROPOIETIN e
                      INNER JOIN MED_ERYTHROPOIETIN_EXEC x ON e.ERYTHROPOIETIN_ID = x.ERYTHROPOIETIN_ID
                      INNER JOIN MED_COMMON_ITEMLIST dm on e.DRUG_MODE = dm.ITEM_ID
                      INNER JOIN MED_COMMON_ITEMLIST unit on e.UNIT = unit.ITEM_ID  
                    WHERE
                      1 = 1
                      AND x.ERYTHROPOIETIN_ID = ?
                      AND x.EXCUTE_DATE >= ? 
                      AND x.EXCUTE_DATE <= ?     
                    ORDER BY
                      x.EXCUTE_DATE";
            }
        }

        #endregion

        #region 系统消息相关SQL

        /// <summary>
        /// 获取所有启用的系统消息数据SQL 
        /// </summary>
        public string GetAllMessage {
            get {
                //STATUS 1：未读；2：已读
                return @"
                    SELECT
                      *
                    FROM MED_COMMON_MESSAGE m
                    WHERE
                      1 = 1
                      AND m.TYPE = ?
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
        public string GetAllCureTypeCount {
            get {
                return @"
                   select 'HD' AS PURIFICATION_MODE ,count(1) AS AllCount  from MED_CURE_MAIN t where t.CURE_CREATE_DATE >= ?
                      AND t.CURE_CREATE_DATE <= ? and t.PURIFICATION_MODE =( 
                     select item_id from med_common_itemlist where item_name= 'HD') 
                    union all
                    select 'HDF' AS PURIFICATION_MODE ,count(1) AS AllCount  from MED_CURE_MAIN t where t.CURE_CREATE_DATE >= ?
                      AND t.CURE_CREATE_DATE <= ? and t.PURIFICATION_MODE =( 
                     select item_id from med_common_itemlist where item_name= 'HDF') 
                     union all
                    select 'HF' AS PURIFICATION_MODE ,count(1) AS AllCount  from MED_CURE_MAIN t where  t.CURE_CREATE_DATE >= ?
                      AND t.CURE_CREATE_DATE <= ? and t.PURIFICATION_MODE =( 
                     select item_id from med_common_itemlist where item_name= 'HF') 
                      union all
                    select 'HP' AS PURIFICATION_MODE ,count(1) AS AllCount  from MED_CURE_MAIN t where t.CURE_CREATE_DATE >= ?
                      AND t.CURE_CREATE_DATE <= ? and t.PURIFICATION_MODE =( 
                     select item_id from med_common_itemlist where item_name= 'HP') 
                       union all
                    select 'HD+HP' AS PURIFICATION_MODE ,count(1) AS AllCount  from MED_CURE_MAIN t where t.CURE_CREATE_DATE >= ?
                      AND t.CURE_CREATE_DATE <= ? and t.PURIFICATION_MODE =( 
                     select item_id from med_common_itemlist where item_name= 'HD+HP')    
                     union all
                    select 'CRRT' AS PURIFICATION_MODE ,count(1) AS AllCount  from MED_CURE_MAIN t where   t.CURE_CREATE_DATE >= ?
                      AND t.CURE_CREATE_DATE <= ? and t.PURIFICATION_MODE =( 
                     select item_id from med_common_itemlist where item_name= 'CRRT') ";
            }
        }

        /// <summary>
        /// 根据月份得到透析次数
        /// </summary>
        public string GetAllCureCountByMonth {
            get {
                return @"
                select  '1月' as curemonth, count(1) AS count_1  
                from MED_CURE_MAIN t where t.CURE_CREATE_DATE >= to_date('2013-1-1','YYYY-MM-DD')
                AND t.CURE_CREATE_DATE <= to_date('2013-01-31','yyyy-mm-dd')
                union all
                select  '2月' as curemonth, count(1) AS count_1  
                from MED_CURE_MAIN t where t.CURE_CREATE_DATE >= to_date('2013-2-1','YYYY-MM-DD')
                AND t.CURE_CREATE_DATE <= to_date('2013-02-28','yyyy-mm-dd')
                union all
                select  '3月' as curemonth, count(1) AS count_1  
                from MED_CURE_MAIN t where t.CURE_CREATE_DATE >= to_date('2013-3-1','YYYY-MM-DD')
                AND t.CURE_CREATE_DATE <= to_date('2013-03-31','yyyy-mm-dd')
                union all
                select  '4月' as curemonth, count(1) AS count_1  
                from MED_CURE_MAIN t where t.CURE_CREATE_DATE >= to_date('2013-4-1','YYYY-MM-DD')
                AND t.CURE_CREATE_DATE <= to_date('2013-04-30','yyyy-mm-dd')
                union all
                select  '5月' as curemonth, count(1) AS count_1  
                from MED_CURE_MAIN t where t.CURE_CREATE_DATE >= to_date('2013-5-1','YYYY-MM-DD')
                AND t.CURE_CREATE_DATE <= to_date('2013-05-31','yyyy-mm-dd')
                union all
                select  '6月' as curemonth, count(1) AS count_1  
                from MED_CURE_MAIN t where t.CURE_CREATE_DATE >= to_date('2013-6-1','YYYY-MM-DD')
                AND t.CURE_CREATE_DATE <= to_date('2013-06-30','yyyy-mm-dd')
                union all
                select  '7月' as curemonth, count(1) AS count_1  
                from MED_CURE_MAIN t where t.CURE_CREATE_DATE >= to_date('2013-07-1','YYYY-MM-DD')
                AND t.CURE_CREATE_DATE <= to_date('2013-07-31','yyyy-mm-dd')
                union all
                select  '8月' as curemonth, count(1) AS count_1  
                from MED_CURE_MAIN t where t.CURE_CREATE_DATE >= to_date('2013-8-1','YYYY-MM-DD')
                AND t.CURE_CREATE_DATE <= to_date('2013-08-30','yyyy-mm-dd')
                union all
                select  '9月' as curemonth, count(1) AS count_1  
                from MED_CURE_MAIN t where t.CURE_CREATE_DATE >= to_date('2013-9-1','YYYY-MM-DD')
                AND t.CURE_CREATE_DATE <= to_date('2013-09-30','yyyy-mm-dd')
                union all
                select  '10月' as curemonth, count(1) AS count_1  
                from MED_CURE_MAIN t where t.CURE_CREATE_DATE >= to_date('2013-10-1','YYYY-MM-DD')
                AND t.CURE_CREATE_DATE <= to_date('2013-10-31','yyyy-mm-dd')
                union all
                select  '11月' as curemonth, count(1) AS count_1  
                from MED_CURE_MAIN t where t.CURE_CREATE_DATE >= to_date('2013-11-1','YYYY-MM-DD')
                AND t.CURE_CREATE_DATE <= to_date('2013-11-30','yyyy-mm-dd')
                union all
                select  '12月' as curemonth, count(1) AS count_1  
                from MED_CURE_MAIN t where t.CURE_CREATE_DATE >= to_date('2013-12-1','YYYY-MM-DD')
                AND t.CURE_CREATE_DATE <= to_date('2013-12-30','yyyy-mm-dd')
                ";
            }
        }

        #endregion

        #region 检验项目质控
        /// <summary>
        /// 得到病人 乙肝、丙肝、梅毒、HIV检查结果列表
        /// </summary>
        public string GetMedInfectiousCheckList {
            get {
                return @"
                select  p.name,p.SPECIFIC_TIME,u.name as check_user,t.*,p.hemodialysis_id as PHEMODIALYSIS_ID,m_b.item_name as HEPATITIS_B_NAME,
                m_c.item_name as HEPATITIS_C_NAME,m_s.item_name as syphilis_name,m_h.item_name as hiv_name
                from med_patients p
                left join med_infectious_check t on t.hemodialysis_id = p.hemodialysis_id
                left join MED_STAFF_DICT u on t.check_user_id = u.user_name
                left join med_common_itemlist m_b on t.hepatitis_b =  m_b.item_id
                left join med_common_itemlist m_c on t.hepatitis_c =  m_c.item_id
                left join med_common_itemlist m_s on t.syphilis =  m_s.item_id
                left join med_common_itemlist m_h on t.hiv =  m_h.item_id
                ";
            }
        }

        public string GetMedInfectiousInfoByID {
            get {
                return @"select * from MED_INFECTIOUS_CHECK where INFECTIOUS_ID = ?";
            }
        }
        #endregion

        #region 药品耗材库存管理
        /// <summary>
        /// 得到药品耗材入库列表
        /// </summary>
        public string GetMedMaterialInputList {
            get {
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

        /// <summary>
        /// 根据ID得到一条入库耗材信息
        /// </summary>
        public string GetMedMaterialInputByID {
            get {
                return @"select * from MED_MATERIAL_INPUT where ID = ? AND STATUS ='1'";
            }
        }

        /// <summary>
        /// 根据透析号和药品编号得到入库的价格和数量
        /// </summary>
        public string GetMedMaterialInputByHemoIdAndCode {
            get {
                return @"select f_count,price from MED_MATERIAL_INPUT where hemo_id=? and code = trim(?) AND STATUS ='1'";
            }
        }

        /// <summary>
        /// 根据透析号和药品编号得到出库的价格和数量
        /// </summary>
        public string GetMedMaterialOutputByHemoIdAndCode {
            get {
                return @"select f_count,price from MED_MATERIAL_OUTPUT where hemo_id=? and code = trim(?) AND STATUS ='1'";
            }
        }

        /// <summary>
        /// 根据透析号和药品编号得到实际库存数量
        /// </summary>
        public string GetMedMaterialCheckByHemoIdAndCode {
            get {
                return @"select * from MED_MATERIAL_CHECK where hemo_id=? and code = trim(?)";
            }
        }

        /// <summary>
        /// 根据ID得到一条出库耗材信息
        /// </summary>
        public string GetMedMaterialOutputByID {
            get {
                return @"select * from MED_MATERIAL_OUTPUT where ID = ? AND STATUS ='1'";
            }
        }

        /// <summary>
        /// 得到药品耗材出库列表
        /// </summary>
        public string GetMedMaterialOutputList {
            get {
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
        public string GetMedMaterialCheckList {
            get {
                return @"select t.*, d.drug_code,m.item_name as drug_type_name,mm.item_name as unit_name,
                p.name,dr.drug_name,dr.made_date as d_made_date,dr.useless_date as d_useless_date from MED_MATERIAL_CHECK t left join 
                med_drug_master d on t.code = TRIM(d.drug_code)
                left join med_common_itemlist m on d.drug_type = m.item_id
                left join med_common_itemlist mm on d.units = mm.item_id
                left join med_patients p on t.hemo_id = p.HEMODIALYSIS_ID
                left join med_drug_master dr on t.code = TRIM(dr.drug_code)
                order by t.CHECK_DATE desc
                ";
            }
        }
        #endregion

        #region 获取临时长期处方执行状态与内容列表
        public string GetQueryRecipeList {
            get {
                return @"select t.recipe_id,t.RECIPE_DATE,'每'||to_char(t.frequency_week)||'周'||to_char(t.frequency_times)||'次，每次'||to_char(t.frequency_hours)||'小时' as Hemo_Times, t.PURIFICATION_MODE,t.therapeutic_method,
                t.dry_weight,cm.item_name as purification_mode_name,cmm.item_name as purifier_model,cmmm.item_name as therapeutic_method_name,di.name as doctor_name,
                case cure.cure_status when '3' then '已执行' end AS status_name,t.status,t.recipe_type,cure.cure_status,
                case t.recipe_type when '1' then '长期' when '0' then '临时' end as recipe_type_name,
                cure.cure_create_date,msdn.name as NURSE_NAME
                from med_hemo_recipe t left join med_common_itemlist cm on t.purification_mode = cm.item_id
                left join med_common_itemlist cmm on t.first_purifier_model = cmm.item_id
                left join med_common_itemlist cmmm on t.therapeutic_method = cmmm.item_id
                left join MED_STAFF_DICT di on t.user_id =  di.emp_no
                left join med_cure_main cure on cure.recipe_id = t.recipe_id
                left join med_staff_dict msdn
                on cure.PRIMARY_NURSE = msdn.emp_no 
                where t.HEMODIALYSIS_ID = ?
                order by t.recipe_date desc,cure.cure_create_date desc";
            }
        }

        /// <summary>
        /// 根据透析号得到对应治疗单数据SQL
        /// </summary>
        public string GetMainCureListByHemoID {
            get {
                return @"select t.recipe_id,t.RECIPE_DATE,'每'||to_char(t.frequency_week)||'周'||to_char(t.frequency_times)||'次，每次'||to_char(t.frequency_hours)||'小时' as Hemo_Times,
                t.dry_weight,cm.item_name as purification_mode_name,cmm.item_name as purifier_model,cmmm.item_name as therapeutic_method_name,di.name as doctor_name,
                case t.status when '1' then '已启用' when '0' then '已停用' end AS status_name,t.status,t.recipe_type,
                case t.recipe_type when '1' then '长期' when '0' then '临时' end as recipe_type_name
                from med_hemo_recipe t left join med_common_itemlist cm on t.purification_mode = cm.item_id
                left join med_common_itemlist cmm on t.first_purifier_model = cmm.item_id
                left join med_common_itemlist cmmm on t.therapeutic_method = cmmm.item_id
                left join MED_STAFF_DICT di on t.user_id =  di.emp_no
                where t.HEMODIALYSIS_ID = ? and t.status = '1'
                order by t.recipe_date desc";
            }
        }
        #endregion
        /// <summary>
        /// 舒适度评价表统计查询sql
        /// </summary>
        public string GetSubjectiveComfortData
        {

            get
            {
                return @"
SELECT DISTINCT * FROM (
SELECT P.NAME PATIENTNAME,
(SELECT BL.SYSTOLIC_PRESSURE||'/'|| BL.DIASTOLIC_PRESSURE FROM MED_HEMODIALYSIS_PARAMETERS BL 
WHERE BL.CURE_ID IN(
SELECT CM.CURE_ID FROM MED_CURE_MAIN CM WHERE CM.HEMODIALYSIS_ID=P.HEMODIALYSIS_ID AND   TO_CHAR(CM.CURE_CREATE_DATE,'yyyy-MM')=:DATEMONTH
) AND BL.SYSTOLIC_PRESSURE>0  AND ROWNUM=1 
) BLOODINFO,
(SELECT AVG(FREQUENCY_HOURS) FROM MED_CURE_MAIN WHERE   HEMODIALYSIS_ID=P.HEMODIALYSIS_ID  AND   TO_CHAR(CURE_CREATE_DATE,'yyyy-MM')=:DATEMONTH )FREQUENCY_HOURS,
(SELECT IL.ITEM_NAME FROM MED_COMMON_ITEMLIST IL WHERE IL.ITEM_ID IN(SELECT VASCULAR_ACCESS_ID  FROM MED_CURE_MAIN 
WHERE HEMODIALYSIS_ID=P.HEMODIALYSIS_ID  AND     TO_CHAR(CURE_CREATE_DATE,'yyyy-MM')=:DATEMONTH)  AND ROWNUM=1  ) VASCULAR_ACCESS_ID,
(select count(CURE_ID) from MED_CURE_MAIN where  HEMODIALYSIS_ID=P.HEMODIALYSIS_ID and SUBJECTIVE_COMFORT='0'  AND   TO_CHAR(CURE_CREATE_DATE,'yyyy-MM')=:DATEMONTH ) SUBJECTIVE_COMFORT0,

(select count(CURE_ID) from MED_CURE_MAIN where  HEMODIALYSIS_ID=P.HEMODIALYSIS_ID and SUBJECTIVE_COMFORT='1' AND   TO_CHAR(CURE_CREATE_DATE,'yyyy-MM')=:DATEMONTH ) SUBJECTIVE_COMFORT1,

(select count(CURE_ID) from MED_CURE_MAIN where  HEMODIALYSIS_ID=P.HEMODIALYSIS_ID and SUBJECTIVE_COMFORT='2'AND   TO_CHAR(CURE_CREATE_DATE,'yyyy-MM')=:DATEMONTH  ) SUBJECTIVE_COMFORT2
FROM MED_PATIENTS P INNER JOIN MED_CURE_MAIN MP ON P.HEMODIALYSIS_ID=MP.HEMODIALYSIS_ID AND    TO_CHAR(MP.CURE_CREATE_DATE,'yyyy-MM')=:DATEMONTH 
)
";
            }
        }
    }
}