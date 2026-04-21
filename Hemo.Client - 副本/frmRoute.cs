/*----------------------------------------------------------------
// Copyright (C) 2005 苏州XX公司有限公司
// 描述：血透功能菜单导航窗体
// 创建时间：2015-05-13
// 创建者：贺建操
//  
// 修改时间：
// 修改人：
// 修改描述：
----------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using Hemo.Client.Core;
using Hemo.Client.UI.Patient;
using Hemo.Client.UI.PatientSchedule;
using Hemo.IService.Permission;
using Hemo.Service;
using Hemo.WinForm;
using Hemo.Client.UI.ReportChart;

namespace Hemo.Client
{
    public partial class frmRoute :HemoBaseFrm
    {
        #region 变量

        private IDictionary<string, int> _formDict = new Dictionary<string, int>();
        private IUser _userService = ServiceManager.Instance.UserService;

        #endregion

        #region 构造函数

        public frmRoute()
        {
            this.InitializeComponent();
        }

        #endregion

        #region 方法

        private void ShowForm(string key)
        {
            if (this._formDict.ContainsKey(key))
            {
                switch (key)
                {
                    case "病患管理":
                        PatientMgrFrm frmPatientMgr = new PatientMgrFrm();
                        frmPatientMgr.Show();
                        break;

                    case "病患治疗":
                        PatientTreantmentFrm patientTreantmentFrm = new PatientTreantmentFrm();
                        patientTreantmentFrm.Show();
                        break;

                    case "病患排班":
                        PatientScheduleFrm patientScheduleFrm = new PatientScheduleFrm();
                        patientScheduleFrm.Show();
                        break;

                    case "程序设置":
                        frmMain frmMain = new frmMain();
                        frmMain.Show();
                        break;
                    case "统计报表":
                        ReportMainFrm reportFrm = new ReportMainFrm();
                        reportFrm.Show();
                        break;
                    case "主任工作站":
                        PatientTreantmentForDirectorFrm patientTreantmentForDirectorFrm = new PatientTreantmentForDirectorFrm();
                        patientTreantmentForDirectorFrm.Show();
                        break;
                    default:
                        break;
                }
            }
            else
                XtraMessageBox.Show("功能按钮名称与权限名称不匹配！");
        }

        #endregion

        #region 事件

        private void frmRoute_Load(object sender, EventArgs e)
        {
            this._formDict.Add("病患管理", 58);
            this._formDict.Add("病患治疗", 49);
            this._formDict.Add("病患排班", 42);
            this._formDict.Add("程序设置", 12);
            this._formDict.Add("统计报表", 11);
            this._formDict.Add("主任工作站", 0);

            DataTable dtPermissions = this._userService.GetPermissionListByUserID(LoginUser.User.USER_ID);
            List<ImageListBoxItem> itemList = new List<ImageListBoxItem>();

            foreach (string pName in dtPermissions.AsEnumerable().Select(r => r.Field<string>("PERMISSIONNAME")).Distinct())
            {
                if (this._formDict.ContainsKey(pName))
                    itemList.Add(new ImageListBoxItem(pName, this._formDict[pName]));
            }

            this.ilbcRoute.Items.AddRange(itemList.ToArray());

            if (dtPermissions.Rows.Count == 1)
                this.ShowForm(dtPermissions.Rows[0]["PERMISSIONNAME"].ToString());
        }

        private void ilbcRoute_SelectedValueChanged(object sender, EventArgs e)
        {
            this.ShowForm(this.ilbcRoute.SelectedValue.ToString());
        }

        #endregion
    }
}