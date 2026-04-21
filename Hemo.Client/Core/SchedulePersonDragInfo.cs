/*----------------------------------------------------------------
 * Copyright (C) 2005 医疗科技发展有限公司
 * 文件功能描述: 排班时人员
 * 创建标识:贺建操-2017年1月30日
 * ----------------------------------------------------------------*/
using Hemo.Model;
using Hemo.Client.Controls.Schedule;

namespace Hemo.Client.Core
{
    public class SchedulePersonDragInfo
    {
        public CtlSchedulePersonNew SourceCtlSchedulePersonNew
        {
            get;
            set;
        }

        public CtlSchedulePerson SourceCtlSchedulePerson
        {
            set;
            get;
        }

        public PatientModel.MED_PATIENTSRow PatientRow
        {
            set;
            get;
        }
    }
}
