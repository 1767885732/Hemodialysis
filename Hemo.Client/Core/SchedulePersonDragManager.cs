/*----------------------------------------------------------------
 * Copyright (C) 2005 医疗科技发展有限公司
 * 文件功能描述:修复系统响应速度慢的问题
 * 创建标识:顾伟伟-2016年12月16日
 * 
 * 修改时间:2017年5月3日
 * 修改人:顾伟伟
 * 修改描述:用户控件
 * 
 * 修改时间:2017年6月4日
 * 修改人:顾伟伟
 * 修改描述:修复系统加载时数据缓存问题
 * ----------------------------------------------------------------*/

using System.Collections.Generic;
using Hemo.Client.Controls.Schedule;

namespace Hemo.Client.Core
{
    public class SchedulePersonDragManager
    {
        #region 变量

        private static object _lock = new object();
        /// <summary>
        /// 维护一个每天对应有多少个病患排班控件的字典集合
        /// </summary>
        private Dictionary<int, List<CtlSchedulePerson>> _schedulePersonControlDict;
        /// <summary>
        /// 维护一个6（星期一到星期六） * 病区数的字典集合，用于控制一个区块内的透析号不能重复
        /// </summary>
        private Dictionary<int, List<string>> _hemoIDDict;
        private static SchedulePersonDragManager _instance;

        #endregion

        #region 属性

        public static SchedulePersonDragManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (_lock)
                    {
                        if (_instance == null)
                            _instance = new SchedulePersonDragManager();
                    }
                }

                return _instance;
            }
        }

        public Dictionary<int, List<CtlSchedulePerson>> SchedulePersonControlDict
        {
            get
            {
                return this._schedulePersonControlDict;
            }
        }

        #endregion

        #region 构造函数

        private SchedulePersonDragManager()
        {
            this._schedulePersonControlDict = new Dictionary<int, List<CtlSchedulePerson>>();

            this._hemoIDDict = new Dictionary<int, List<string>>();
        }

        #endregion

        #region 方法

        public void InitSchedulePersonControlDict()
        {
            this._schedulePersonControlDict.Clear();
        }

        public void InitHemoIDDict()
        {
            this._hemoIDDict.Clear();
        }

        public void AddSchedulePerson(int key, CtlSchedulePerson ctlSchedulePerson)
        {
            if (this._schedulePersonControlDict.ContainsKey(key))
                this._schedulePersonControlDict[key].Add(ctlSchedulePerson);
            else
                this._schedulePersonControlDict.Add(key, new List<CtlSchedulePerson>() { ctlSchedulePerson });
        }

        public bool ContainsHemoID(int key, string hemoID)
        {
            if (this._hemoIDDict.ContainsKey(key))
                return this._hemoIDDict[key].Contains(hemoID);
            else
                return false;
        }

        public void AddHemoID(int key, string hemoID)
        {
            if (this._hemoIDDict.ContainsKey(key))
                this._hemoIDDict[key].Add(hemoID);
            else
                this._hemoIDDict.Add(key, new List<string>() { hemoID });
        }

        public void RemoveHemoID(int key, string hemoID)
        {
            if (this._hemoIDDict.ContainsKey(key))
                this._hemoIDDict[key].Remove(hemoID);
        }

        #endregion
    }
}
