-- Create table
create table MED_HEMO_EVENTINFO
(
  id                  VARCHAR2(36) not null,
  hemodialysis_id     VARCHAR2(40) not null,
  qssykjyw            VARCHAR2(1),
  xqyyx               VARCHAR2(1),
  xgtlbwzz            VARCHAR2(1),
  xlgr                VARCHAR2(1),
  xgccgr              VARCHAR2(1),
  tlxgxgr             VARCHAR2(1),
  tlgr                VARCHAR2(1),
  hemoeventdt         DATE,
  eventhazard         VARCHAR2(300),
  hemotube1           VARCHAR2(1),
  hemotubeexd1        VARCHAR2(1),
  hemotubeexdtime1    VARCHAR2(30),
  hemotube2           VARCHAR2(1),
  hemotubeexdtime2    VARCHAR2(30),
  hemotube3           VARCHAR2(1),
  hemotubeexdtime3    VARCHAR2(30),
  hemotube4           VARCHAR2(1),
  hemotubeexdtim4     VARCHAR2(30),
  hemotube5           VARCHAR2(1),
  hemotubeexdtim5     VARCHAR2(30),
  hemoeventotherinfo  VARCHAR2(300),
  concreteevent       VARCHAR2(1),
  concreteevent1      VARCHAR2(1),
  concreteevent2      VARCHAR2(1),
  concreteevent3      VARCHAR2(300),
  concreteevent4      VARCHAR2(300),
  besource            VARCHAR2(1),
  besource1           VARCHAR2(1),
  besource2           VARCHAR2(1),
  besource3           VARCHAR2(1),
  vaucularpostion     VARCHAR2(1),
  vaucularpostion1    VARCHAR2(1),
  vaucularpostion2    VARCHAR2(1),
  vaucularpostion3    VARCHAR2(1),
  otherinfect         VARCHAR2(1),
  otherinfect1        VARCHAR2(1),
  otherinfect2        VARCHAR2(1),
  otherinfect3        VARCHAR2(1),
  concreteproblem1    VARCHAR2(1),
  concreteproblem2    VARCHAR2(1),
  concreteproblem3    VARCHAR2(1),
  concreteproblem4    VARCHAR2(1),
  concreteproblem5    VARCHAR2(1),
  concreteproblem6    VARCHAR2(1),
  concreteproblem7    VARCHAR2(1),
  concreteproblem7ext VARCHAR2(100),
  inhospital          VARCHAR2(1),
  inhospital1         VARCHAR2(1),
  inhospital2         VARCHAR2(1),
  concretedie         VARCHAR2(1),
  concretedie1        VARCHAR2(1),
  concretedie2        VARCHAR2(1),
  creater             VARCHAR2(100),
  creatertime         DATE,
  eventtype           VARCHAR2(1)
)
tablespace TSP_MEDHEMO
  pctfree 10
  initrans 1
  maxtrans 255
  storage
  (
    initial 64
    minextents 1
    maxextents unlimited
  );
-- Add comments to the table 
comment on table MED_HEMO_EVENTINFO
  is '透析事件一览表';
-- Add comments to the columns 
comment on column MED_HEMO_EVENTINFO.hemodialysis_id
  is '透析号';
comment on column MED_HEMO_EVENTINFO.qssykjyw
  is '血液透析事件类型:1、全身使用抗菌药物';
comment on column MED_HEMO_EVENTINFO.xqyyx
  is '血液透析事件类型:2、血培养阳性';
comment on column MED_HEMO_EVENTINFO.xgtlbwzz
  is '血液透析事件类型:3、血管通路部位出现脓、发红或肿胀加剧';
comment on column MED_HEMO_EVENTINFO.xlgr
  is '血液透析事件类型:4、血流感染';
comment on column MED_HEMO_EVENTINFO.xgccgr
  is '血液透析事件类型:5、血管穿刺部位感染';
comment on column MED_HEMO_EVENTINFO.tlxgxgr
  is '血液透析事件类型:6、血管通路相关性血流感染';
comment on column MED_HEMO_EVENTINFO.tlgr
  is '血液透析事件类型:7、血管通路感染';
comment on column MED_HEMO_EVENTINFO.hemoeventdt
  is '血液透析事件发生日期';
comment on column MED_HEMO_EVENTINFO.eventhazard
  is '血液透析事件危险因素：【也用于第二种的诊断】';
comment on column MED_HEMO_EVENTINFO.hemotube1
  is '血液透析类型和置管日期1、内瘘';
comment on column MED_HEMO_EVENTINFO.hemotubeexd1
  is '血液透析类型和置管日期1、内瘘 :扣眼穿刺';
comment on column MED_HEMO_EVENTINFO.hemotubeexdtime1
  is '血液透析类型和置管日期1、内瘘 :扣眼穿刺 日期或者是不清楚';
comment on column MED_HEMO_EVENTINFO.hemotube2
  is '血液透析类型和置管日期2、人工血管       置管日期：';
comment on column MED_HEMO_EVENTINFO.hemotubeexdtime2
  is '血液透析类型和置管日期2、人工血管       置管日期： 日期或者是不清楚';
comment on column MED_HEMO_EVENTINFO.hemotube3
  is '血液透析类型和置管日期3、隧道式中心静脉导管       置管日期：';
comment on column MED_HEMO_EVENTINFO.hemotubeexdtime3
  is '血液透析类型和置管日期3、隧道式中心静脉导管       置管日期： 日期或者是不清楚';
comment on column MED_HEMO_EVENTINFO.hemotube4
  is '血液透析类型和置管日期4、非隧道式中心静脉导管';
comment on column MED_HEMO_EVENTINFO.hemotubeexdtim4
  is '血液透析类型和置管日期4、非隧道式中心静脉导管       置管日期：日期或者是不清楚';
comment on column MED_HEMO_EVENTINFO.hemotube5
  is '血液透析类型和置管日期5、其他隧道';
comment on column MED_HEMO_EVENTINFO.hemotubeexdtim5
  is '血液透析类型和置管日期5、其他隧道       置管日期： 日期或者是不清楚';
comment on column MED_HEMO_EVENTINFO.hemoeventotherinfo
  is '血液透析事件相关情况：【也用于第二种的现状】';
comment on column MED_HEMO_EVENTINFO.concreteevent
  is '具体透析事件:1、全身使用抗菌药物 用药途径1、口服';
comment on column MED_HEMO_EVENTINFO.concreteevent1
  is '具体透析事件:1、全身使用抗菌药物 用药途径2、肌注';
comment on column MED_HEMO_EVENTINFO.concreteevent2
  is '具体透析事件:1、全身使用抗菌药物 用药途径3、静脉';
comment on column MED_HEMO_EVENTINFO.concreteevent3
  is '具体透析事件:1、全身使用抗菌药物 药物名称  【也用于第二种的处理】';
comment on column MED_HEMO_EVENTINFO.concreteevent4
  is '具体透析事件:1、全身使用抗菌药物 使用原因   【也用于第二种的结果】';
comment on column MED_HEMO_EVENTINFO.besource
  is '可以来源：1、血管通路';
comment on column MED_HEMO_EVENTINFO.besource1
  is '可以来源：2、非血管通路';
comment on column MED_HEMO_EVENTINFO.besource2
  is '可以来源：3、污染';
comment on column MED_HEMO_EVENTINFO.besource3
  is '可以来源：4、不确定';
comment on column MED_HEMO_EVENTINFO.vaucularpostion
  is '3、血管通路部位脓、红或肿胀加重 1、穿刺点';
comment on column MED_HEMO_EVENTINFO.vaucularpostion1
  is '3、血管通路部位脓、红或肿胀加重 2、穿刺点周围皮肤';
comment on column MED_HEMO_EVENTINFO.vaucularpostion2
  is '3、血管通路部位脓、红或肿胀加重 3、隧道口';
comment on column MED_HEMO_EVENTINFO.vaucularpostion3
  is '3、血管通路部位脓、红或肿胀加重 4、穿刺点皮下组织';
comment on column MED_HEMO_EVENTINFO.otherinfect
  is '具体问题:其他感染1、下呼吸道感染';
comment on column MED_HEMO_EVENTINFO.otherinfect1
  is '具体问题:其他感染2、上呼吸道感染';
comment on column MED_HEMO_EVENTINFO.otherinfect2
  is '具体问题:其他感染3、尿路感染';
comment on column MED_HEMO_EVENTINFO.otherinfect3
  is '具体问题:其他感染4、其他感染';
comment on column MED_HEMO_EVENTINFO.concreteproblem1
  is '具体问题:2、当日最高体温≥37.3℃';
comment on column MED_HEMO_EVENTINFO.concreteproblem2
  is '具体问题:3、高热';
comment on column MED_HEMO_EVENTINFO.concreteproblem3
  is '具体问题:4、寒颤';
comment on column MED_HEMO_EVENTINFO.concreteproblem4
  is '具体问题:5、血压下降';
comment on column MED_HEMO_EVENTINFO.concreteproblem5
  is '具体问题:6、伤口（与血管通路无关）出现脓或红肿加重';
comment on column MED_HEMO_EVENTINFO.concreteproblem6
  is '具体问题:7、蜂窝组织炎（皮肤红、热、或非开放性伤口疼痛）';
comment on column MED_HEMO_EVENTINFO.concreteproblem7
  is '具体问题:8、其他';
comment on column MED_HEMO_EVENTINFO.concreteproblem7ext
  is '具体问题:8、其他（具体填写）';
comment on column MED_HEMO_EVENTINFO.inhospital
  is '住院是否不清楚';
comment on column MED_HEMO_EVENTINFO.inhospital1
  is '住院是否不清楚';
comment on column MED_HEMO_EVENTINFO.inhospital2
  is '住院是否不清楚';
comment on column MED_HEMO_EVENTINFO.concretedie
  is '死亡是否不清楚';
comment on column MED_HEMO_EVENTINFO.concretedie1
  is '死亡是否不清楚';
comment on column MED_HEMO_EVENTINFO.concretedie2
  is '死亡是否不清楚';
comment on column MED_HEMO_EVENTINFO.creater
  is '创建人';
comment on column MED_HEMO_EVENTINFO.creatertime
  is '创建时间';
comment on column MED_HEMO_EVENTINFO.eventtype
  is '类型是正常的还是EXT的 1：正常的0是EXT的';
-- Create/Recreate primary, unique and foreign key constraints 
alter table MED_HEMO_EVENTINFO
  add constraint PK_MED_HEMO_EVENTINFO primary key (ID)
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
