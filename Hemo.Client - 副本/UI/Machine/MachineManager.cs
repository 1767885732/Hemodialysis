using System;
using System.Data;
using System.Windows.Forms;
using DevExpress.XtraEditors.Repository;
using Hemo.Client.Controls;
using Hemo.Utilities;
using Hemo.Client.UI.Patient;
using Hemo.WinForm;
using Hemo.Client.UI.ReportChart;
using Hemo.Client.Core;
using Hemo.Client.UI.User;
using DevExpress.XtraEditors;

namespace Hemo.Client.UI.Machine
{
    public partial class MachineManager : DevExpress.XtraEditors.XtraForm {

        #region 变量

        #endregion

        #region 构造函数与方法

        public MachineManager() {
            InitializeComponent();
            this.barBtn_User.Caption = string.IsNullOrEmpty(HemoApplicationContext.Current.CurrentUser.USER_NAME) ? "用户" : HemoApplicationContext.Current.CurrentUser.USER_NAME;
            this.barBtn_Date.Caption = DateTime.Today.ToString("yyyy年MM月dd日");
            this.barBtn_IP.Caption = HemoApplicationContext.Current.IpAddress;
        }

        private void MachineManager_Load(object sender, EventArgs e) 
        {
            MachineUseRecord userRecord = new MachineUseRecord();
            userRecord.Dock = DockStyle.Fill;
            this.pnlContainer.Controls.Clear();
            this.pnlContainer.Controls.Add(userRecord);

        }

        #endregion


        private void barButtonItem1_ItemClick(object senderr, DevExpress.XtraBars.ItemClickEventArgs er) {
            LoginScreen frm = new LoginScreen();
            frm.ShiftRoles = HemoApplicationContext.Current.RolesOffices;
            frm.LoginEvent += delegate(object sender, LoginEventArgs e)
            {
                if (e.RunApp && e.RunAppNames.Length > 0) {
                    switch (e.RunAppNames) {
                        case "病患管理":
                            var frmRun = new PatientMgrFrm();
                            frmRun.Load += delegate
                            {
                                frm.Hide();
                            };
                            this.Hide();
                            frmRun.ShowDialog();


                            break;
                        case "病患治疗":
                            var frmRun1 = new PatientTreantmentFrm();
                            frmRun1.Load += delegate
                            {
                                frm.Hide();
                            };
                            this.Hide();
                            frmRun1.ShowDialog();


                            break;
                        case "病患排班":
                            var frmRun2 = new MachineManager();
                            frmRun2.Load += delegate
                            {
                                frm.Hide();
                            };
                            this.Hide();
                            frmRun2.ShowDialog();

                            break;
                        case "系统管理":
                            var frmRun3 = new frmMain();
                            frmRun3.Load += delegate
                            {
                                frm.Hide();
                            };
                            this.Hide();
                            frmRun3.ShowDialog();
                            break;
                        case "统计报表":
                            var frmRun4 = new ReportMainFrm();
                            frmRun4.Load += delegate
                            {
                                frm.Hide();
                            };
                            this.Hide();
                            frmRun4.ShowDialog();
                            break;
                        default:
                            this.Close();
                            break;
                    }

                }
                else {  //异常出错，退出系统
                    this.Close();
                }

            };
            frm.ShowDialog();
        }

        private void barButtonItem2_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Application.Exit();
            this.Close();
        }

        private void barButtonItem6_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            ChangPwdFrm frm = new ChangPwdFrm(HemoApplicationContext.Current.CurrentUser.USER_ID);
            frm.ShowDialog();
        }

        private void barLargeButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            MachineUseRecord userRecord = new MachineUseRecord();
            userRecord.Dock = DockStyle.Fill;
            this.pnlContainer.Controls.Clear();
            this.pnlContainer.Controls.Add(userRecord);
        }

        private void barLargeButtonItem7_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            RepairRecord frm = new RepairRecord();
            frm.ShowDialog();
        }

        private void barLargeButtonItem2_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            MachineReportFrm frm = new MachineReportFrm();
            frm.ShowDialog();
        }

        private void MachineManager_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (XtraMessageBox.Show("您确定退出当前系统吗？", "提示", MessageBoxButtons.OKCancel) != System.Windows.Forms.DialogResult.OK)
            {
                e.Cancel = true;
            }
        }

        private void barLargeButtonItem3_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            AirPurgeFrm frm = new AirPurgeFrm();
            frm.ShowDialog();
        }

        private void barLargeButtonItem4_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            MahineMixManager frm = new MahineMixManager();
            frm.ShowDialog();
        }

        private void barLargeButtonItem5_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            WaterHemoManager frm = new WaterHemoManager();
            frm.ShowDialog();
        }

        private void barLargeButtonItem6_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            WaterTreatmentManager frm = new WaterTreatmentManager();
            frm.ShowDialog();
        }

        private void barLargeButtonItem8_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            HosEquipmentManager frm = new HosEquipmentManager();
            frm.ShowDialog();
        }

     
    }
}