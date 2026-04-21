CREATE OR REPLACE VIEW MED_VIEW_SHUXUEQIANSIXIANG_EXT AS
SELECT  T2.NAME as "姓名",
t2.hemodialysis_id as "透析号" ,
t.patient_id as "病人号",T2.TIME_TYPE as "病人来源",
t2.age as "年龄",
t.check_date as "检验日期",
DECODE(t.COL_10,'negative','阴性','positive','阳性',t.COL_10) AS "*乙肝表面抗原(IU/ml)",
DECODE(t.COL_11,'negative','阴性','positive','阳性',t.COL_11) AS "梅毒螺旋体抗体(S/CO)",
DECODE(t.COL_12,'negative','阴性','positive','阳性',t.COL_12) AS "丙肝抗体(S/CO)",
DECODE(t.COL_13,'negative','阴性','positive','阳性',t.COL_13) AS "*艾滋病抗体(S/CO)",
t2.input_code
FROM  MED_HIS_ROWTOCOL_END T  INNER JOIN MED_PATIENTS T2
ON T.PATIENT_ID = T2.PATIENT_ID
AND T2.IS_DELETE !=1
WHERE t.item_name ='输血前四项检查';
