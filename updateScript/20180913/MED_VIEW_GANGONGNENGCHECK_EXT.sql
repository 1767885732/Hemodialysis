CREATE OR REPLACE VIEW MED_VIEW_GANGONGNENGCHECK_EXT AS
SELECT  T2.NAME as "姓名",
t.hemodialysis_id as "透析号" ,
t.patient_id as "病人号",T2.TIME_TYPE as "病人来源",
t2.age as "年龄",
t.check_date as "检验日期",
t.COL_10 AS "*白蛋白(g/L)",
t.COL_11 AS  "*谷氨酰转肽酶(U/L)",
t.COL_12 AS  "*谷丙转氨酶(U/L)",
t.COL_13 AS  "*谷草转氨酶(U/L)",
t.COL_14 AS  "*碱性磷酸酶(U/L)",
t.COL_15 AS  "*直接胆红素(umol/L)",
t.COL_16 AS  "*总胆红素(umol/L)",
t.COL_17 AS  "*总蛋白(g/L)",
t.COL_18 AS  "白蛋白/球蛋白",
t.COL_19 AS  "胆碱脂酶(IU/L)",
t.COL_20 AS  "谷草/谷丙",
t.COL_21 AS  "肌酸激酶同功酶(U/L)",
t.COL_22 AS  "间接胆红素(umol/L)",
t.COL_23 AS  "前白蛋白(mg/L)",
t.COL_24 AS  "球蛋白(g/L)",
t.COL_25 AS  "总胆汁酸(umol/L)",
t2.input_code
FROM  MED_HIS_ROWTOCOL_END T  INNER JOIN MED_PATIENTS T2
ON T.PATIENT_ID = T2.PATIENT_ID
AND T2.IS_DELETE !=1
WHERE t.item_name ='肝功能检查';
