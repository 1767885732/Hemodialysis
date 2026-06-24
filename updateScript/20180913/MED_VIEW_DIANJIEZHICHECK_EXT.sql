CREATE OR REPLACE VIEW MED_VIEW_DIANJIEZHICHECK_EXT AS
SELECT T2.NAME as "姓名",
t2.hemodialysis_id as "透析号" ,
t.patient_id as "病人号",T2.TIME_TYPE as "病人来源",
t2.age as "年龄",
t.check_date as "检验日期",
t.COL_10 AS "*钾(mmol/L)",
t.COL_11 AS "*钠(mmol/L)",
t.COL_12 AS "*氯(mmol/L)",
t.COL_13 AS "*血钙(mmol/L)",
t.COL_14 AS "镁(mmol/L)",
t.COL_15 AS "*磷(mmol/L)",
t.COL_16 AS "二氧化碳(mmol/L)",
t.COL_17 AS "阴离子间隙(mmol/L)",
t2.input_code
FROM  MED_HIS_ROWTOCOL_END T  INNER JOIN MED_PATIENTS T2
ON T.PATIENT_ID = T2.PATIENT_ID
AND T2.IS_DELETE !=1
WHERE t.item_name ='电解质检查';
