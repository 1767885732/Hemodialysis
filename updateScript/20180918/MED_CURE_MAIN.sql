-- Add/modify columns 
alter table MED_CURE_MAIN add IN_BED VARCHAR2(1);
-- Add comments to the columns 
comment on column MED_CURE_MAIN.IN_BED
  is ' «∑ÒŒ‘¥≤ 1= «°¢0=∑Ò';
