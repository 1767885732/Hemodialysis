/*----------------------------------------------------------------
// Copyright (C) 2005 苏州XX公司有限公司
// 描述：系统设置主窗体
// 创建时间：2015-04-13
// 创建者：刘超
//  
// 修改时间：
// 修改人：
// 修改描述：
----------------------------------------------------------------*/

using System;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraBars;
using DevExpress.XtraBars.Ribbon;
using DevExpress.XtraTab;
using DevExpress.XtraTab.ViewInfo;
using Hemo.Client.Controls;
using Hemo.Client.UI.Config;
using Hemo.Client.UI.Dict;
using Hemo.Client.UI.Drug;
using Hemo.Client.UI.Hemodialysis;
using Hemo.Client.UI.HemodialysisApply;
using Hemo.Client.UI.Lab;
using Hemo.Client.UI.Machine;
using Hemo.Client.UI.Material;
using Hemo.Client.UI.Patient;
using Hemo.Client.UI.PatientSchedule;
using Hemo.Client.UI.Drug;
using Hemo.Model;
using DevExpress.XtraEditors;
using Hemo.Client.Core;
using Hemo.Client;
using Hemo.Client.UI.Order;

namespace Hemo.WinForm {
    public partial class frmMain : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        #region 类变量

        /// <summary>
        /// 当前点击的Tab
        /// </summary>
        private BaseTabHitInfo CurrentSelectingTab = null;
        /// <summary>
        /// 病患管理
        /// </summary>
        private CtlStartMain _ctlStartMain = null;

        #endregion

        #region 属性

        #endregion

        #region 构造函数

        public frmMain()
        {
            InitializeComponent();
            _ctlStartMain = new CtlStartMain();
            //ShowViewInWorkSpace(_ctlStartMain, "病患管理");
        }

        #endregion

        #region 事件

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void workSpaceControl_MouseDown(object sender, MouseEventArgs e)
        {
            CurrentSelectingTab = workSpaceControl.CalcHitInfo(new Point(e.X, e.Y));
        }
        /// <summary>
        /// 关闭界面
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void workSpaceControl_CloseButtonClick(object sender, EventArgs e)
        {
            if (CurrentSelectingTab != null)
            {
                XtraTabPage page = (XtraTabPage)CurrentSelectingTab.Page;
                this.workSpaceControl.TabPages.Remove((XtraTabPage)CurrentSelectingTab.Page);
            }
        }
        /// <summary>
        /// 新增患者
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ibtnPatientAdd_ItemClick(object sender, ItemClickEventArgs e)
        {
            EditPatientNew frmEditPatient = new EditPatientNew();
            frmEditPatient.Current = null;
            frmEditPatient.ShowDialog();
        }
        /// <summary>
        /// 修改患者
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ibtnPatientEdit_ItemClick(object sender, ItemClickEventArgs e)
        {
            EditPatientNew frmEditPatient = new EditPatientNew();
            frmEditPatient.Current = _ctlStartMain.LayoutViewPatient.GetFocusedDataRow() as PatientModel.MED_PATIENTSRow;

            frmEditPatient.ShowDialog();
        }

        private void barButtonItem6_ItemClick(object sender, ItemClickEventArgs e)
        {
            string strHemoID = (_ctlStartMain.LayoutViewPatient.GetFocusedDataRow()
                 as PatientModel.MED_PATIENTSRow).HEMODIALYSIS_ID;
            EditVascularAccess frmEditVascularAccess = new EditVascularAccess(strHemoID);
            frmEditVascularAccess.ShowDialog();
        }
        /// <summary>
        /// 病患排班
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSchedule_ItemClick(object sender, ItemClickEventArgs e)
        {
            //if (!TryShowViewFromWorkSpace("病患排班")) {
            //    ShowViewInWorkSpace(new CtlScheduleMain(), "病患排班");
            //}

            PatientScheduleFrm patientScheduleFrm = new PatientScheduleFrm();
            patientScheduleFrm.Show();
        }

        private void barButtonItem14_ItemClick(object sender, ItemClickEventArgs e)
        {
            CtlHemodialysisList frmCtlHemodialysisList = new CtlHemodialysisList();
            if (!TryShowViewFromWorkSpace("透前体重"))
            {
                ShowViewInWorkSpace(frmCtlHemodialysisList, "透前体重");
            }
        }

        private void barButtonItem30_ItemClick(object sender, ItemClickEventArgs e)
        {
            loadConfigList("耗材分类");
        }

        private void barButtonItem31_ItemClick(object sender, ItemClickEventArgs e)
        {
            loadConfigList("耗材单位");
        }

        private void barButtonItem32_ItemClick(object sender, ItemClickEventArgs e)
        {
            CtlQueryMaterialFirm frmCtlQueryMaterialFirm = new CtlQueryMaterialFirm();
            if (!TryShowViewFromWorkSpace("耗材厂商"))
            {
                ShowViewInWorkSpace(frmCtlQueryMaterialFirm, "耗材厂商");
            }
        }

        private void barButtonItem33_ItemClick(object sender, ItemClickEventArgs e)
        {
            CtlQueryMaterialMaster frmCtlQueryMaterialMaster = new CtlQueryMaterialMaster();
            if (!TryShowViewFromWorkSpace("耗材主信息设置"))
            {
                ShowViewInWorkSpace(frmCtlQueryMaterialMaster, "耗材主信息设置");
            }
        }

        private void barButtonItem34_ItemClick(object sender, ItemClickEventArgs e)
        {

            loadConfigList("临时医嘱");
        }

        private void barButtonItem35_ItemClick(object sender, ItemClickEventArgs e)
        {
            loadConfigList("药品单位");
        }

        private void barButtonItem36_ItemClick(object sender, ItemClickEventArgs e)
        {
            loadConfigList("药品分类");
        }

        private void barButtonItem37_ItemClick(object sender, ItemClickEventArgs e)
        {
            CtlQueryDrugFirm frmCtlQueryDrugFirm = new CtlQueryDrugFirm();
            if (!TryShowViewFromWorkSpace("药品厂商"))
            {
                ShowViewInWorkSpace(frmCtlQueryDrugFirm, "药品厂商");
            }
        }

        private void barButtonItem38_ItemClick(object sender, ItemClickEventArgs e)
        {
            CtlQueryDrugMaster frmCtlQueryDrugMaster = new CtlQueryDrugMaster();
            if (!TryShowViewFromWorkSpace("药品主档"))
            {
                ShowViewInWorkSpace(frmCtlQueryDrugMaster, "药品主档");
            }
        }

        private void barButtonItem11_ItemClick(object sender, ItemClickEventArgs e)
        {
            CtlStaffDictList frmCCtlStaffDictList = new CtlStaffDictList("医生");
            if (!TryShowViewFromWorkSpace("医生资料"))
            {
                ShowViewInWorkSpace(frmCCtlStaffDictList, "医生资料");
            }
        }

        private void barButtonItem19_ItemClick(object sender, ItemClickEventArgs e)
        {
            CtlStaffDictList frmCCtlStaffDictList = new CtlStaffDictList("护士");
            if (!TryShowViewFromWorkSpace("护士资料"))
            {
                ShowViewInWorkSpace(frmCCtlStaffDictList, "护士资料");
            }
        }

        private void barButtonItem20_ItemClick(object sender, ItemClickEventArgs e)
        {
            CtlStaffDictList frmCCtlStaffDictList = new CtlStaffDictList("技术员");
            if (!TryShowViewFromWorkSpace("工程师资料"))
            {
                ShowViewInWorkSpace(frmCCtlStaffDictList, "工程师资料");
            }
        }

        private void barButtonItem21_ItemClick(object senderr, ItemClickEventArgs ee)
        {
            LoginScreen frm = new LoginScreen();
            frm.ShiftRoles = HemoApplicationContext.Current.RolesOffices;
            frm.LoginEvent += delegate(object sender, LoginEventArgs e)
            {
                frm.Dispose();
                this.Dispose();
                Program.Show(e.RunApp, e.RunAppNames);

            };
            frm.ShowDialog();
        }

        private void barButtonItem22_ItemClick(object sender, ItemClickEventArgs e)
        {
            this.Close();
        }

        private void barButtonItem23_ItemClick(object sender, ItemClickEventArgs e)
        {
            Hemo.Client.UI.Drug.CtlQueryMaterialInput frmInput = new CtlQueryMaterialInput();
            if (!TryShowViewFromWorkSpace("入库信息"))
            {
                ShowViewInWorkSpace(frmInput, "入库信息");
            }
        }

        private void barButtonItem24_ItemClick(object sender, ItemClickEventArgs e)
        {
            loadConfigList("净化器类型");
        }

        private void barButtonItem25_ItemClick(object sender, ItemClickEventArgs e)
        {
            Hemo.Client.UI.Drug.CtlQueryMaterialOutput frmInput = new CtlQueryMaterialOutput();
            if (!TryShowViewFromWorkSpace("出库信息"))
            {
                ShowViewInWorkSpace(frmInput, "出库信息");
            }
        }

        private void barButtonItem26_ItemClick(object sender, ItemClickEventArgs e)
        {
            Hemo.Client.UI.Drug.CtlQueryMaterialCheck frm = new CtlQueryMaterialCheck();
            if (!TryShowViewFromWorkSpace("库存盘点"))
            {
                ShowViewInWorkSpace(frm, "库存盘点");
            }
        }

        private void barButtonItem29_ItemClick(object sender, ItemClickEventArgs e)
        {
            SystemConfig frmSystemConfig = new SystemConfig();
            if (!TryShowViewFromWorkSpace("系统数据设定"))
            {
                ShowViewInWorkSpace(frmSystemConfig, "系统数据设定");
            }
        }

        private void barButtonItem39_ItemClick(object sender, ItemClickEventArgs e)
        {
            loadConfigList("床位");
        }

        private void barButtonItem40_ItemClick(object sender, ItemClickEventArgs e)
        {
            loadConfigList("区域");
        }

        private void barButtonItem41_ItemClick(object sender, ItemClickEventArgs e)
        {
            CtlMachineList frmCtlMachineList = new CtlMachineList();
            if (!TryShowViewFromWorkSpace("区域、床位、血透机对应设定"))
            {
                ShowViewInWorkSpace(frmCtlMachineList, "区域、床位、血透机对应设定");
            }
        }

        private void barButtonItem42_ItemClick(object sender, ItemClickEventArgs e)
        {
            BaseConfig frmBaseConfig = new BaseConfig();
            if (!TryShowViewFromWorkSpace("血透数据设定"))
            {
                ShowViewInWorkSpace(frmBaseConfig, "血透数据设定");
            }
        }
        /// <summary>
        /// Tab 切换
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ribbon_SelectedPageChanged(object sender, EventArgs e)
        {
            RibbonPage ribbonPage = ribbon.SelectedPage;
            switch (ribbonPage.Text)
            {
                case "病患管理":
                    if (!TryShowViewFromWorkSpace("病患管理"))
                    {
                        ShowViewInWorkSpace(_ctlStartMain, "病患管理");
                        _ctlStartMain.ReturnVisible = true;
                    }
                    break;
                case "治疗准备":
                    if (!TryShowViewFromWorkSpace("病患排班"))
                    {
                        ShowViewInWorkSpace(new CtlScheduleMain(), "病患排班");
                    }
                    break;
                //case "病患治疗":
                //    if (!TryShowViewFromWorkSpace("病患治疗")) {
                //        ShowViewInWorkSpace(new CtlPatientTreatMain(), "病患治疗");
                //    }
                //  break;
                default:
                    break;


            }
        }

        /// <summary>
        /// 透析处方
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barButtonItem5_ItemClick(object sender, ItemClickEventArgs e)
        {
            string strHemoID = (_ctlStartMain.LayoutViewPatient.GetFocusedDataRow()
                as PatientModel.MED_PATIENTSRow).HEMODIALYSIS_ID;
            EditPrescribe frmEditPrescribe = new EditPrescribe(strHemoID, "");
            frmEditPrescribe.ShowDialog();
        }

        /// <summary>
        /// 病患治疗
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnPatientTreament_ItemClick(object sender, ItemClickEventArgs e)
        {
            PatientTreantmentFrm patientTreantmentFrm = new PatientTreantmentFrm();
            patientTreantmentFrm.Show();
        }


        private void barButtonItem9_ItemClick(object sender, ItemClickEventArgs e)
        {
            //string strHemoID = (_ctlStartMain.LayoutViewPatient.GetFocusedDataRow()
            //   as PatientModel.MED_PATIENTSRow).HEMODIALYSIS_ID;
            //string strName = (_ctlStartMain.LayoutViewPatient.GetFocusedDataRow()
            //   as PatientModel.MED_PATIENTSRow).NAME;
            //string strAge = (_ctlStartMain.LayoutViewPatient.GetFocusedDataRow()
            //   as PatientModel.MED_PATIENTSRow).AGE.ToString();
            //string strSex = (_ctlStartMain.LayoutViewPatient.GetFocusedDataRow()
            //   as PatientModel.MED_PATIENTSRow).SEX;
            //CtlUserCureList ctlUserCureList = new CtlUserCureList(strName);
            //ctlUserCureList.HEMODIALYSIS_ID = strHemoID;
            //ctlUserCureList.LoadCureDateList(strHemoID);
            //string strInfo = strName + " " + strSex + " " + strAge + "岁";
            //if (!TryShowViewFromWorkSpace(strInfo)) {
            //    ShowViewInWorkSpace(ctlUserCureList, strInfo);
            //}
            //ctlUserCureList.ToadyListVisable = false;

            CtlQueryCureList frmCureList = new CtlQueryCureList();
            if (!TryShowViewFromWorkSpace("透析治疗单列表"))
            {
                ShowViewInWorkSpace(frmCureList, "透析治疗单列表");
            }
        }
        /// <summary>
        /// 治疗预约
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnHemoApply_ItemClick(object sender, ItemClickEventArgs e)
        {
            string strHemoID = (_ctlStartMain.LayoutViewPatient.GetFocusedDataRow()
               as PatientModel.MED_PATIENTSRow).HEMODIALYSIS_ID;
            HemodialysisApplyFrm frmHemodialysisApply = new HemodialysisApplyFrm(strHemoID);

            frmHemodialysisApply.ShowDialog();
        }

        private void barButtonItem4_ItemClick(object sender, ItemClickEventArgs e)
        {
            string strHemoID = (_ctlStartMain.LayoutViewPatient.GetFocusedDataRow()
              as PatientModel.MED_PATIENTSRow).HEMODIALYSIS_ID;
            EditUseMaterial frmuseMaterial = new EditUseMaterial(strHemoID, "");
            frmuseMaterial.ShowDialog();
        }

        private void btnPatientMgr_ItemClick(object sender, ItemClickEventArgs e)
        {
            PatientMgrFrm frmPatientMgr = new PatientMgrFrm();
            frmPatientMgr.ShowDialog();
        }

        private void barButtonItem8_ItemClick(object sender, ItemClickEventArgs e)
        {
            LabFrm labFrm = new LabFrm(this._ctlStartMain.LayoutViewPatient.GetFocusedDataRow() as PatientModel.MED_PATIENTSRow);

            labFrm.ShowDialog();
        }

        /// <summary>
        /// 促红素设定
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnErythropoietinSet_ItemClick(object sender, ItemClickEventArgs e)
        {
            this.loadConfigList("促红素设定");
        }

        private void frmMain_FormClosed(object sender, FormClosedEventArgs e)
        {
            //Application.Exit();
        }

        private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (XtraMessageBox.Show("您确定退出当前系统吗？", "提示", MessageBoxButtons.OKCancel) !=
        System.Windows.Forms.DialogResult.OK)
            {

                e.Cancel = true;
            }
            else
            {
                Program.HideClose = true;
            }
        }

        private void barButtonItem7_ItemClick(object sender, ItemClickEventArgs e)
        {
            HemoRelationFrm frmInput = new HemoRelationFrm();
            if (!TryShowViewFromWorkSpace("处方默认值维护"))
            {
                ShowViewInWorkSpace(frmInput, "处方默认值维护");
            }
        }

        private void barButtonItem10_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (!TryShowViewFromWorkSpace("治疗默认模板"))
            {
                ShowViewInWorkSpace(new HemoDefaultModeManagementView(), "治疗默认模板");
            }
        }

        #endregion

        #region 方法

        /// <summary>
        /// 将指定的界面添加到工作区域中,并设置Tab标题
        /// </summary>
        /// <param name="view"></param>
        /// <param name="caption"></param>
        private void ShowViewInWorkSpace(UserControl view, string name)
        {
            var page = new XtraTabPage();
            page.Controls.Add(view);

            view.Dock = DockStyle.Fill;
            view.Name = name;
            page.BorderStyle = BorderStyle.None;
            page.Text = name;

            this.workSpaceControl.Visible = true;
            this.workSpaceControl.Dock = DockStyle.Fill;
            this.workSpaceControl.TabPages.Add(page);
            this.workSpaceControl.SelectedTabPage = page;
        }

        /// <summary>
        /// 根据界面名称,尝试从工作区域中打开已存在的界面
        /// </summary>
        /// <param name="caption"></param>
        /// <returns></returns>
        private bool TryShowViewFromWorkSpace(string name)
        {
            foreach (XtraTabPage page in this.workSpaceControl.TabPages)
            {
                if (page.Controls.Count > 0 && page.Controls[0].Name == name)
                {
                    this.workSpaceControl.SelectedTabPage = page;
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// 通用载入配置窗体方法
        /// </summary>
        /// <param name="pType">类型字段设置</param>
        private void loadConfigList(string pType)
        {
            CtlConfigList frmCtrlConfigList = new CtlConfigList(pType);
            if (!TryShowViewFromWorkSpace(pType))
            {
                ShowViewInWorkSpace(frmCtrlConfigList, pType);
            }
        }

        #endregion
    }
}
