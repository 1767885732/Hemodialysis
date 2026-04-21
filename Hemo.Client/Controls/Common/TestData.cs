/*----------------------------------------------------------------
 * Copyright (C) 2005 医疗科技发展有限公司
 * 文件功能描述:数据测试类
 * 创建标识:刘配齐-2014年8月2日
 * ----------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace Hemo.Client.Controls.Common
{
    public class TestData
    {
        //private IPatientSchedule _patientScheduleService = ServiceManager.Instance.PatientSchedule;
        public DateTime BeginDateTime
        {
            set;
            get;
        }
        public Image PicImg
        {
            set;
            get;
        }
        public string ActionName
        {
            set;
            get;
        }

        public  static DataTable dtGet()
        {
            DataTable dtMain = new DataTable();
            dtMain.Columns.Add("ID", Type.GetType("System.String"));
            dtMain.Columns.Add("NAME", Type.GetType("System.String"));
            dtMain.Columns.Add("ISEND", Type.GetType("System.String"));
            var row = dtMain.NewRow();
            row["ID"] = System.Guid.NewGuid().ToString();
            row["NAME"] = "透前评估";
            row["ISEND"] = "0";
            dtMain.Rows.Add(row);

            row = dtMain.NewRow();
            row["ID"] = System.Guid.NewGuid().ToString();
            row["NAME"] = "传染病查体";
            row["ISEND"] = "0";
            dtMain.Rows.Add(row);

            row = dtMain.NewRow();
            row["ID"] = System.Guid.NewGuid().ToString();
            row["NAME"] = "血管通路";
            row["ISEND"] = "0";
            dtMain.Rows.Add(row);

            row = dtMain.NewRow();
            row["ID"] = System.Guid.NewGuid().ToString();
            row["NAME"] = "透析方案";
            row["ISEND"] = "0";
            dtMain.Rows.Add(row);

            row = dtMain.NewRow();
            row["ID"] = System.Guid.NewGuid().ToString();
            row["NAME"] = "透析开始";
            row["ISEND"] = "0";
            dtMain.Rows.Add(row);

            row = dtMain.NewRow();
            row["ID"] = System.Guid.NewGuid().ToString();
            row["NAME"] = "透析治疗";
            row["ISEND"] = "0";
            dtMain.Rows.Add(row);

            row = dtMain.NewRow();
            row["ID"] = System.Guid.NewGuid().ToString();
            row["NAME"] = "透析结束";
            row["ISEND"] = "1";
            dtMain.Rows.Add(row);
            return dtMain;
        }

        public int GetPatientCurrentStatus(DateTime dialysisDate, string hemodialysisID)
        {
            //PatientScheduleModel.MED_PATIENT_SCHEDULEDataTable dt = null;
            //dt = _patientScheduleService.GetPatientScheduleSignle(dialysisDate, hemodialysisID);
            //if (dt != null && dt.Rows.Count > 0)
            //{
            //    if (!dt[0].IsEND_TIMENull())
            //    {
            //        return 2;
            //    }
            //    else
            //    {
            //        if (!dt[0].IsSTART_TIMENull())
            //        { return 1; }
            //        else
            //        {

            //            return 0;
            //        }
            //    }
            //}
            return 0;
        }

    }
}
