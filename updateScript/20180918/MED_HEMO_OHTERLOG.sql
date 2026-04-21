-- Create table
create table MED_HEMO_OHTERLOG
(
  id             VARCHAR2(45) not null,
  logday         DATE,
  hemocount      NUMBER,
  nocount        NUMBER,
  longtube       NUMBER,
  temptube       NUMBER,
  tubeinfect     NUMBER,
  virusinfect    NUMBER,
  otherhosinfect NUMBER,
  creater        VARCHAR2(45),
  created        DATE
)
tablespace TSP_MEDHEMO
  pctfree 10
  initrans 1
  maxtrans 255
  storage
  (
    initial 64K
    minextents 1
    maxextents unlimited
  );
-- Add comments to the table 
comment on table MED_HEMO_OHTERLOG
  is '血透相关目标性监测日志';
-- Add comments to the columns 
comment on column MED_HEMO_OHTERLOG.id
  is '是键';
comment on column MED_HEMO_OHTERLOG.logday
  is '日期';
comment on column MED_HEMO_OHTERLOG.hemocount
  is '透析人数';
comment on column MED_HEMO_OHTERLOG.nocount
  is '内瘘数';
comment on column MED_HEMO_OHTERLOG.longtube
  is '长期导管数';
comment on column MED_HEMO_OHTERLOG.temptube
  is '临时导管数';
comment on column MED_HEMO_OHTERLOG.tubeinfect
  is '导管感染数';
comment on column MED_HEMO_OHTERLOG.virusinfect
  is '病毒感染数';
comment on column MED_HEMO_OHTERLOG.otherhosinfect
  is '其他医院感染数';
comment on column MED_HEMO_OHTERLOG.creater
  is '创建人';
comment on column MED_HEMO_OHTERLOG.created
  is '时间';
-- Create/Recreate primary, unique and foreign key constraints 
alter table MED_HEMO_OHTERLOG
  add constraint PK_MED_HEMO_OHTERLOG primary key (ID)
  using index 
  tablespace TSP_MEDHEMO
  pctfree 10
  initrans 2
  maxtrans 255
  storage
  (
    initial 64K
    minextents 1
    maxextents unlimited
  );
