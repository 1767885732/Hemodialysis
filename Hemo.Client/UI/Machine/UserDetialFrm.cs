/*----------------------------------------------------------------
// Copyright (C) 2005 苏州XX公司有限公司
// 描述：血透机使用记录用户控件类
// 创建时间：2015-03-27
// 创建者：刘超
//  
// 修改时间：
// 修改人：
// 修改描述：
----------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Hemo.Utilities;
using Hemo.IService.Dict;
using Hemo.Service;
using Hemo.Model;

namespace Hemo.Client.UI.Machine
{
    public partial class UserDetialFrm :HemoBaseFrm
    {
        #region 类变量

        private IStaffDict _staffDictService = ServiceManager.Instance.StaffDictService;

        private MachineModel.MED_DIALYSIS_MACHINERow _currentRow = null;

        #endregion

        #region 属性

        public MachineModel.MED_DIALYSIS_MACHINERow CurrentRow
        {
            get { return _currentRow; }
            set { _currentRow = value; }
        }

        #endregion

        #region 构造函数

        public UserDetialFrm()
        {
            InitializeComponent();
        }

        #endregion

        #region 事件

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            //保存数据啦啦啦





            this.DialogResult = System.Windows.Forms.DialogResult.OK;
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
        }

        private void UserDetialFrm_Load(object sender, EventArgs e)
        {
            this.label_year_mon.Text = System.DateTime.Now.ToString("yyyy年MM月");
            this.label_day.Text = System.DateTime.Now.Day.ToString();
            this.label_MachineNum.Text = string.Format("机器名称：" + _currentRow.MACHINE_MODEL);

            //载入责任护士、穿刺护士、责任医生下拉框数据
            DataTable dtStaffSict = _staffDictService.GetStaffDictList();
            if (dtStaffSict != null && dtStaffSict.Rows.Count > 0)
            {
                DataTable dtPunctureNurseList = Utility.GetSubTable(dtStaffSict, "ZYNAME='护士'", "name");
                if (dtPunctureNurseList != null && dtPunctureNurseList.Rows.Count > 0)
                {
                    BaseControlInfo.BindLookUpEdit(lopOperatorMor, "EMP_NO", "NAME", dtPunctureNurseList, "NAME", "上午使用人");
                    BaseControlInfo.BindLookUpEdit(lopOperatorAft, "EMP_NO", "NAME", dtPunctureNurseList, "NAME", "中午使用人");
                    BaseControlInfo.BindLookUpEdit(lopOperatorEve, "EMP_NO", "NAME", dtPunctureNurseList, "NAME", "晚上使用人");
                }
            }
        }

        #endregion
    }
}