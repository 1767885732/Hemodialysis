-- Create table
create table MED_TEMP_HEMOID
(
  HEMODIALYSIS_ID VARCHAR2(50)
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
-- Add comments to the columns 
comment on column MED_TEMP_HEMOID.HEMODIALYSIS_ID
  is 'Í¸Îö±àºÅ';

-- Add/modify columns 
alter table MED_TEMP_HEMOID add ID VARCHAR2(50) not null;
-- Add comments to the columns 
comment on column MED_TEMP_HEMOID.ID
  is 'Ö÷¼üID';
-- Create/Recreate primary, unique and foreign key constraints 
alter table MED_TEMP_HEMOID
  add constraint PK_MED_TEMP_HEMOID primary key (ID);