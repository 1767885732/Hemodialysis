/*----------------------------------------------------------------
 * Copyright (C) 2005 (苏州)医疗科技发展有限公司
 * 文件功能描述:用户区域关系维护类
 * 创建标识:刘超-2016年5月9日
 * ----------------------------------------------------------------*/

using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using Hemo.IService.Config;
using Hemo.IService.Permission;
using Hemo.Model;
using Hemo.Service;
using Hemo.Utilities;

namespace Hemo.Client.UI.User
{
    public partial class SetUserAreaMappingFrm : XtraUserControl
    {
        #region 变量

        private const string CHOOSE_FIELDNAME = "CHOOSE";

        private IUser _userService = ServiceManager.Instance.UserService;
        private IConfig _configService = ServiceManager.Instance.ConfigService;

        private ConfigModel.MED_COMMON_ITEMLISTDataTable _areaDt;

        #endregion

        #region 构造函数

        public SetUserAreaMappingFrm()
        {
            this.InitializeComponent();
        }

        #endregion

        #region 方法

        /// <summary>
        /// 初始化
        /// </summary>
        private void InitializeControls()
        {
            this._areaDt = this._configService.GetConfigList(string.Empty, string.Empty, "区域", "1");
            this._areaDt.Columns.Add(new DataColumn(CHOOSE_FIELDNAME, typeof(bool)));

            this.gcAreaInfo.DataSource = this._areaDt;

            Utility.BindLookUpEdit(this.cbxUSER, "USER_ID", "USER_NAME", this._userService.GetUserList(), "USER_NAME", "用户姓名");
        }

        /// <summary>
        /// 绑定列表
        /// </summary>
        private void BindGird()
        {
            if (this.cbxUSER.EditValue == null)
                return;

            PermissionModel.MED_USERAREA_MAPPINGDataTable userAreaMappingDt = this._userService.GetUserAreaMappingList(this.cbxUSER.EditValue.ToString());

            foreach (ConfigModel.MED_COMMON_ITEMLISTRow areaRow in this._areaDt.Rows)
            {
                areaRow[CHOOSE_FIELDNAME] = userAreaMappingDt.FindByUSER_IDAREA_ID(this.cbxUSER.EditValue.ToString(), areaRow.ITEM_ID) != null;
            }
        }

        #endregion

        #region 事件

        /// <summary>
        /// 加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SetUserAreaMappingFrm_Load(object sender, EventArgs e)
        {
            this.InitializeControls();
        }

        /// <summary>
        /// 用户名改变
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbxUSER_EditValueChanged(object sender, EventArgs e)
        {
            this.BindGird();
        }

        /// <summary>
        /// 鼠标按下
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gvAreaInfo_MouseDown(object sender, MouseEventArgs e)
        {
            GridView gv = sender as GridView;

            if (gv == null)
                return;

            GridHitInfo hitInfo = gv.CalcHitInfo(new Point(e.X, e.Y));

            if (hitInfo == null)
                return;

            if (e.Button == MouseButtons.Left && hitInfo.InRowCell) {
                if (hitInfo.Column.VisibleIndex == 0)
                    this._areaDt.Rows[hitInfo.RowHandle][CHOOSE_FIELDNAME] = !Utility.CBool(this._areaDt.Rows[hitInfo.RowHandle][CHOOSE_FIELDNAME].ToString());
            }
        }

        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (this.cbxUSER.EditValue == null)
            {
                AutoClosedMsgBox.ShowForm("请选择用户！", "提示", 1000, MessageBoxIcon.Information);

                return;
            }

            PermissionModel.MED_USERAREA_MAPPINGDataTable userAreaMappingDt = new PermissionModel.MED_USERAREA_MAPPINGDataTable();

            this._userService.DeleteUserAreaMappingInfo(this.cbxUSER.EditValue.ToString());

            foreach (ConfigModel.MED_COMMON_ITEMLISTRow areaRow in this._areaDt.Select(string.Format("{0} = true", CHOOSE_FIELDNAME)))
            {
                PermissionModel.MED_USERAREA_MAPPINGRow userAreaMappingRow = userAreaMappingDt.NewMED_USERAREA_MAPPINGRow();
                userAreaMappingRow.USER_ID = this.cbxUSER.EditValue.ToString();
                userAreaMappingRow.AREA_ID = areaRow.ITEM_ID;

                userAreaMappingDt.AddMED_USERAREA_MAPPINGRow(userAreaMappingRow);
            }

            this._userService.SaveUserAreaMappingInfo(userAreaMappingDt);

            AutoClosedMsgBox.ShowForm("保存成功！", "提示", 1000, MessageBoxIcon.Information);
        }

        #endregion
    }
}
