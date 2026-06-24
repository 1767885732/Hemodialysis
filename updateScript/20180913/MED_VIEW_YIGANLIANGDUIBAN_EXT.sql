CREATE OR REPLACE VIEW MED_VIEW_YIGANLIANGDUIBAN_EXT AS
SELECT  T2.NAME as "姓名",
t2.hemodialysis_id as "透析号" ,
t.patient_id as "病人号",T2.TIME_TYPE as "病人来源",
t2.age as "年龄",
t.check_date as "检验日期",
t.COL_10 AS "乙肝两对半(ng/ml)",
t2.input_code
FROM  MED_HIS_ROWTOCOL_END T  INNER JOIN MED_PATIENTS T2
ON T.PATIENT_ID = T2.PATIENT_ID
AND T2.IS_DELETE !=1
WHERE t.item_name ='乙肝两对半';
