-- Add/modify columns 
alter table MED_CURE_MAIN add ACTUAL_CLEANUP_HOUR NUMBER(10,2);
alter table MED_CURE_MAIN add ACTUAL_CLEANUP_MINUTE NUMBER(10,2);
-- Add comments to the columns 
comment on column MED_CURE_MAIN.ACTUAL_CLEANUP_HOUR
  is '实际净化时间';
comment on column MED_CURE_MAIN.ACTUAL_CLEANUP_MINUTE
  is '实际净化时间分钟';
