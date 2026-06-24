/*----------------------------------------------------------------
 * Copyright (C) 2005 (苏州)医疗科技发展有限公司
 * 文件功能描述:查询信息
 * 创建标识:贺建操-2013年5月27日
 * 
 * 修改时间:2013年9月4日
 * 修改人:贺建操
 * 修改描述:新增方法
 * 
 * 修改时间:2013年12月13日
 * 修改人:贺建操
 * 修改描述:修改方法
 * 
 * 修改时间:2014年3月23日
 * 修改人:顾伟伟
 * 修改描述:新增方法SQL
 * ----------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Hemo.Client.UI.Machine;
using DevExpress.XtraBars.Docking2010.Customization;
using Hemo.IService.Config;
using Hemo.Service;
using DevExpress.XtraSplashScreen;
using System.Threading;
using Hemo.Client.Controls;
using Hemo.Model;
using Hemo.Client.UI.ReportChart;
using Hemo.Utilities;
using DevExpress.XtraBars.Docking2010.Views.WindowsUI;
using Hemo.Client.Core;
using Hemo.HQCWebClient;
using System.Configuration;
using Hemo.HQCWebClient.Models;
using Newtonsoft.Json;
using Hemo.IService;

namespace Hemo.Client.UI.Hemodialysis
{
    public partial class QualityControlData : ViewBase
    {
        #region 变量

        private IConfig _iconfig = ServiceManager.Instance.ConfigService;

        private IHemodialysis hemoService = ServiceManager.Instance.HemodialysisService;

        private ConfigModel.MED_HOSPITAL_INFODataTable dtmedhospitalinfo = new ConfigModel.MED_HOSPITAL_INFODataTable();

        private ConfigModel.MED_QUALITY_BASEDataTable dtALLINFO = new ConfigModel.MED_QUALITY_BASEDataTable();

        private ConfigModel.MED_COMMON_ITEMLISTDataTable dtConfig = new ConfigModel.MED_COMMON_ITEMLISTDataTable();

        private DateTime dtStar = new DateTime();

        private DateTime dtEnd = new DateTime();

        private string loginName = string.Empty;

        private string getUserApi = string.Empty;

        private string getTokenApi = string.Empty;

        #endregion

        /// <summary>
        /// 构造函数
        /// </summary>
        public QualityControlData()
        {
            InitializeComponent();
        }

        #region 事件

        private void QualityControlData_Load(object sender, EventArgs e)
        {
            var year = System.DateTime.Now.Year;
            for (int i = -10; i < 10; i++)
            {
                this.cmbYear.Properties.Items.Add(year + i);

            }
            this.cmbYear.EditValue = year;
            dtStar = new DateTime(Utilities.Utility.CInt(year.ToString()), 1, 1);
            dtEnd = dtStar.AddYears(1).AddSeconds(-1);

            dtConfig = _iconfig.GetConfigList(string.Empty, string.Empty, "质控平台访问配置", "1");
            if (dtConfig != null && dtConfig.Rows.Count > 0)
            {
                loginName = dtConfig.FirstOrDefault(r => r.ITEM_NAME.Equals("QCLoginName")).ITEM_VALUE;
                getUserApi = dtConfig.FirstOrDefault(r => r.ITEM_NAME.Equals("GetUserByName")).ITEM_VALUE;
                getTokenApi = dtConfig.FirstOrDefault(r => r.ITEM_NAME.Equals("GetToken")).ITEM_VALUE;
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (FlyoutDialog.Show(this.FindForm(), new ctlForHOSPITAL(null, dtmedhospitalinfo)) == DialogResult.Yes)
            {
                GetMED_HOSPITAL_INFO();
            };
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (this.gridVHOSPITAL_INFO.GetFocusedRow() != null)
            {
                if (FlyoutDialog.Show(this.FindForm(), new ctlForHOSPITAL((ConfigModel.MED_HOSPITAL_INFORow)this.gridVHOSPITAL_INFO.GetFocusedDataRow(), dtmedhospitalinfo)) == DialogResult.Yes)
                {
                    GetMED_HOSPITAL_INFO();
                };
            }
        }

        private void btnQuery_Click(object sender, EventArgs e)
        {
            if (this.xtraTabControl1.SelectedTabPage == this.xtraTabPage1)
            {
                GetMED_HOSPITAL_INFO();
            }
            else if (this.xtraTabControl1.SelectedTabPage == this.xtraTabPage4)
            {
                GetEquipment_INFO();
            }

            else if (this.xtraTabControl1.SelectedTabPage == this.xtraTabPage2)
            {
                var control = this.pnlLab.Controls[0] as QueryQualityMonitorLabReport;
                var year = this.cmbYear.EditValue.ToString();
                DateTime dtStarer = new DateTime(Utility.CInt(year), 1, 1);
                DateTime dtEnder = dtStar.AddYears(1).AddSeconds(-1);
                control.dtStar = dtStarer;
                control.dtEnd = dtEnder;
                control.InzationData();
            }
            else if (this.xtraTabControl1.SelectedTabPage == this.xtraTabPage3)
            {
                var control = this.pnlInfect.Controls[0] as QueryQualityMonitorInfectReport;
                var year = this.cmbYear.EditValue.ToString();
                control.BeginTime = new DateTime(Utility.CInt(year), 1, 1);
                control.EndTime = control.BeginTime.AddYears(1).AddDays(-1);
                control.LoadData();
            }
            else if (this.xtraTabControl1.SelectedTabPage == this.xtraTabPage5)
            {
                var control = this.pnlCure.Controls[0] as QueryQualityMonitorCureReport;
                var year = this.cmbYear.EditValue.ToString();
                control.BeginTime = new DateTime(Utility.CInt(year), 1, 1);
                control.EndTime = control.BeginTime.AddYears(1).AddDays(-1);
                control.LoadData();
            }
            else if (this.xtraTabControl1.SelectedTabPage == this.xtraTabPage6)
            {
                GetALLINFO();
            }
        }

        private void GetMED_HOSPITAL_INFO()
        {
           var dt = _iconfig.GetMED_HOSPITAL_INFOList();
           dtmedhospitalinfo = new ConfigModel.MED_HOSPITAL_INFODataTable();
           dt.AsEnumerable().Where(i => i.HOSPITAL_YEAR.Equals(this.cmbYear.EditValue.ToString())).CopyToDataTable(dtmedhospitalinfo, LoadOption.OverwriteChanges);
            this.gridCHOSPITAL_INFO.DataSource = dtmedhospitalinfo;
        }

        private void GetEquipment_INFO()
        {
            var dt = _iconfig.GetMED_HOSPITAL_INFOList();
            dtmedhospitalinfo = new ConfigModel.MED_HOSPITAL_INFODataTable();
            dt.AsEnumerable().Where(i => i.HOSPITAL_YEAR.Equals(this.cmbYear.EditValue.ToString())).CopyToDataTable(dtmedhospitalinfo, LoadOption.OverwriteChanges);
            this.gridCEQUIPMENT_INFO.DataSource = dtmedhospitalinfo;
        }

        private void GetALLINFO()
        {
            ConfigModel.MED_HOSPITAL_INFORow drHospital = null;
            var data = _iconfig.GetMED_HOSPITAL_INFOList();
            var z = data.FirstOrDefault(i => i.HOSPITAL_YEAR == this.cmbYear.EditValue.ToString());
            if (z != null)
            {
                drHospital = z;
            }
            if (drHospital != null)
            {
                dtALLINFO = _iconfig.GetMED_QUALITY_BASE(drHospital.HOSPITAL_ID);
                for (int i = 0; i < dtALLINFO.Rows.Count; i++)
                {
                    if ((dtALLINFO.Rows[i]["ITEM_NAME"].ToString() == "HOSPITAL_ID") || (dtALLINFO.Rows[i]["ITEM_NAME"].ToString() == "HOSPITAL_YEAR"))
                    {
                        dtALLINFO.Rows[i].Delete();
                    }
                }
                dtALLINFO.AcceptChanges();
                this.gridCAllINFO.DataSource = dtALLINFO;

            }
        }

        #region SplashScreenManager

        private SplashScreenManager _loadForm;
        /// <summary>
        /// 等待窗体管理对象
        /// </summary>
        protected SplashScreenManager LoadForm
        {
            get
            {
                if (_loadForm == null)
                {
                    this._loadForm = new SplashScreenManager(this.FindForm(), typeof(FrmWaitForm), true, true);
                    //this._loadForm.CloseWaitForm();.ClosingDelay = 0;
                }
                return _loadForm;
            }
        }
        /// <summary>
        /// 显示等待窗体
        /// </summary>
        public void ShowMessage()
        {
            bool flag = !this.LoadForm.IsSplashFormVisible;
            if (flag)
            {
                this.LoadForm.ShowWaitForm();
            }
        }
        /// <summary>
        /// 关闭等待窗体
        /// </summary>
        public void HideMessage()
        {
            bool isSplashFormVisible = this.LoadForm.IsSplashFormVisible;
            if (isSplashFormVisible)
            {
                this.LoadForm.CloseWaitForm();
            }
        }

        #endregion

        private void btnBeginCount_Click(object sender, EventArgs e)
        {
            ShowMessage();
            using (BackgroundWorker worker = new BackgroundWorker())
            {
                var qualityBase = new ConfigModel.MED_QUALITY_BASEDataTable();
                ConfigModel.MED_HOSPITAL_INFORow drHospital = null;
                var data = _iconfig.GetMED_HOSPITAL_INFOList();
                var z = data.FirstOrDefault(i => i.HOSPITAL_YEAR == this.cmbYear.EditValue.ToString());
                if (z != null)
                {
                    drHospital = z;
                }
                if (drHospital == null)
                {
                    HideMessage();
                    XtraMessageBox.Show("请先统计科室信息！");
                    return;
                }
                var x = _iconfig.GetMED_QUALITY_BASE(drHospital.HOSPITAL_ID);

                //科室信息
                if (this.xtraTabControl1.SelectedTabPage == this.xtraTabPage1)
                {
                    if (x.AsEnumerable().FirstOrDefault(i => i.UPLOAD_TYPE == "科室信息") != null)
                    {
                        HideMessage();
                        if (XtraMessageBox.Show("科室信息已经统计过，是否重新统计?", "科室信息", MessageBoxButtons.OKCancel) == DialogResult.Cancel)
                        {
                            return;
                        };
                    };
                    ShowMessage();
                    _iconfig.DeleteMED_QUALITY_BASEbyHOSPITAL_IDandKind(drHospital.HOSPITAL_ID, "科室信息");
                    ConfigModel.MED_QUALITY_BASERow baseRow = null;
                    for (int i = 0; i < drHospital.Table.Columns.Count; i++)
                    {
                        baseRow = qualityBase.NewMED_QUALITY_BASERow();
                        baseRow.ID = Guid.NewGuid().ToString();
                        baseRow.HOSPITAL_ID = drHospital.HOSPITAL_ID;
                        baseRow.ITEM_NAME = drHospital.Table.Columns[i].ColumnName;
                        baseRow.ITEM_VALUE = drHospital[drHospital.Table.Columns[i].ColumnName].ToString();
                        baseRow.EXTEND_COL = "";
                        baseRow.UPLOAD_TYPE = "科室信息";
                        baseRow.IS_UPLOAD = "0";
                        qualityBase.AddMED_QUALITY_BASERow(baseRow);
                    }
                }
                //卡点

                string head = drHospital.HOSPITAL_YEAR + "福建省医疗机构开展血液透析基本情况";

                //检验信息

                if (this.xtraTabControl1.SelectedTabPage == this.xtraTabPage2)
                {

                    if (x.AsEnumerable().FirstOrDefault(i => i.UPLOAD_TYPE == "检验信息") != null)
                    {
                        HideMessage();
                        if (XtraMessageBox.Show("检验信息已经统计过，是否重新统计?", "检验信息", MessageBoxButtons.OKCancel) == DialogResult.Cancel)
                        {
                            return;
                        };
                    };
                    ShowMessage();
                    _iconfig.DeleteMED_QUALITY_BASEbyHOSPITAL_IDandKind(drHospital.HOSPITAL_ID, "检验信息");

                    var labDataTable = new ConfigModel.MED_COMMON_ITEMLISTDataTable();
                    var control = this.pnlLab.Controls[0] as QueryQualityMonitorLabReport;
                    labDataTable = control.commData;
                    if (labDataTable.Rows.Count == 0)
                    {
                        HideMessage();
                        if (XtraMessageBox.Show("无检验信息,是否继续?", "检验信息", MessageBoxButtons.OKCancel) == DialogResult.Cancel)
                        {
                            return;
                        };
                    }
                    ShowMessage();


                    foreach (ConfigModel.MED_COMMON_ITEMLISTRow itemlistRow in labDataTable.Rows)
                    {
                        var baseRow = qualityBase.NewMED_QUALITY_BASERow();
                        baseRow.ID = Guid.NewGuid().ToString();
                        baseRow.HOSPITAL_ID = drHospital.HOSPITAL_ID;
                        baseRow.ITEM_NAME = itemlistRow.ITEM_NAME + itemlistRow.ITEM_VALUE;
                        baseRow.ITEM_VALUE = itemlistRow.PARENT;
                        baseRow.EXTEND_COL = itemlistRow.PRICE;
                        baseRow.UPLOAD_TYPE = "检验信息";
                        baseRow.IS_UPLOAD = "0";
                        qualityBase.AddMED_QUALITY_BASERow(baseRow);
                    }
                }


                //院感信息

                if (this.xtraTabControl1.SelectedTabPage == this.xtraTabPage3)
                {
                    if (x.AsEnumerable().FirstOrDefault(i => i.UPLOAD_TYPE == "院感信息") != null)
                    {
                        HideMessage();
                        if (XtraMessageBox.Show("院感信息已经统计过，是否重新统计?", "院感信息", MessageBoxButtons.OKCancel) == DialogResult.Cancel)
                        {
                            return;
                        };
                    };
                    ShowMessage();
                    _iconfig.DeleteMED_QUALITY_BASEbyHOSPITAL_IDandKind(drHospital.HOSPITAL_ID, "院感信息");

                    var control = this.pnlInfect.Controls[0] as QueryQualityMonitorInfectReport;
                    var dtMonitorInfect = control.dtMonitorInfect;
                    if (dtMonitorInfect.Rows.Count == 0)
                    {
                        HideMessage();
                        if (XtraMessageBox.Show("无院感信息,是否继续?", "院感信息", MessageBoxButtons.OKCancel) == DialogResult.Cancel)
                        {
                            return;
                        };
                    }


                    ShowMessage();

                    if (dtMonitorInfect != null)
                    {
                        for (int i = 0; i < dtMonitorInfect.Rows.Count; i++)
                        {
                            var baseRow = qualityBase.NewMED_QUALITY_BASERow();
                            baseRow.ID = Guid.NewGuid().ToString();
                            baseRow.HOSPITAL_ID = drHospital.HOSPITAL_ID;
                            baseRow.ITEM_NAME = dtMonitorInfect.Rows[i]["INFECT_NAME"].ToString() + dtMonitorInfect.Rows[i]["INFECT_CONDITION"].ToString();
                            baseRow.ITEM_VALUE = dtMonitorInfect.Rows[i]["INFECT_COUNT"].ToString();
                            baseRow.EXTEND_COL = dtMonitorInfect.Rows[i]["INFECT_RATIO"].ToString();
                            baseRow.UPLOAD_TYPE = "院感信息";
                            baseRow.IS_UPLOAD = "0";
                            qualityBase.AddMED_QUALITY_BASERow(baseRow);
                        }
                    }
                }

                //设备信息

                if (this.xtraTabControl1.SelectedTabPage == this.xtraTabPage4)
                {

                    if (x.AsEnumerable().FirstOrDefault(i => i.UPLOAD_TYPE == "设备信息") != null)
                    {
                        HideMessage();
                        if (XtraMessageBox.Show("设备信息已经统计过，是否重新统计?", "设备信息", MessageBoxButtons.OKCancel) == DialogResult.Cancel)
                        {
                            return;
                        };
                    };
                    ShowMessage();
                    _iconfig.DeleteMED_QUALITY_BASEbyHOSPITAL_IDandKind(drHospital.HOSPITAL_ID, "设备信息");

                    var dtEquipment = _iconfig.GetQualityControlEquipmentInfoByHospitalID(drHospital.HOSPITAL_ID);

                    if (dtEquipment != null)
                    {

                        if (dtEquipment.Rows.Count == 0)
                        {
                            HideMessage();
                            if (XtraMessageBox.Show("无设备信息,是否继续?", "设备信息", MessageBoxButtons.OKCancel) == DialogResult.Cancel)
                            {
                                return;
                            };
                        }
                        ShowMessage();
                        var sumObject = dtEquipment.AsEnumerable().Select(d => Convert.ToInt32(d.Field<string>("COUNT"))).Sum();

                        ConfigModel.MED_QUALITY_BASERow baseRow = null;
                        _iconfig.DeleteMED_QUALITY_BASEbyHOSPITAL_IDandKind(drHospital.HOSPITAL_ID, "科室信息");
                        for (int i = 0; i < drHospital.Table.Columns.Count; i++)
                        {
                            baseRow = qualityBase.NewMED_QUALITY_BASERow();
                            baseRow.ID = Guid.NewGuid().ToString();
                            baseRow.HOSPITAL_ID = drHospital.HOSPITAL_ID;
                            baseRow.ITEM_NAME = drHospital.Table.Columns[i].ColumnName;
                            baseRow.ITEM_VALUE = drHospital[drHospital.Table.Columns[i].ColumnName].ToString();
                            baseRow.EXTEND_COL = "";
                            baseRow.UPLOAD_TYPE = "科室信息";
                            baseRow.IS_UPLOAD = "0";
                            qualityBase.AddMED_QUALITY_BASERow(baseRow);
                        }


                        baseRow = qualityBase.NewMED_QUALITY_BASERow();

                        baseRow.ID = Guid.NewGuid().ToString();
                        baseRow.HOSPITAL_ID = drHospital.HOSPITAL_ID;
                        baseRow.ITEM_NAME = "血透机总台数";
                        baseRow.ITEM_VALUE = sumObject.ToString();
                        baseRow.EXTEND_COL = "";
                        baseRow.UPLOAD_TYPE = "设备信息";
                        baseRow.IS_UPLOAD = "0";
                        qualityBase.AddMED_QUALITY_BASERow(baseRow);
                        DataTable t1 = null;
                        DataTable t2 = null;
                        DataTable t3 = null;

                        if (dtEquipment.Rows.Count > 0)
                        {
                            var t11 = dtEquipment.AsEnumerable().Where(d => d.KIND == "血透机");
                            var t22 = dtEquipment.AsEnumerable().Where(d => d.KIND == "血滤机");
                            var t33 = dtEquipment.AsEnumerable().Where(d => d.KIND == "水处理机");
                            if (t11 != null && t11.Count() > 0)
                            {
                                t1 = t11.CopyToDataTable();
                            }
                            if (t22 != null && t22.Count() > 0)
                            {
                                t2 = t22.CopyToDataTable();
                            }
                            if (t33 != null && t33.Count() > 0)
                            {
                                t3 = t33.CopyToDataTable();
                            }
                            //t1 = dtEquipment.AsEnumerable().Where(d => d.KIND == "血透机").CopyToDataTable();
                            //t2 = dtEquipment.AsEnumerable().Where(d => d.KIND == "血滤机").CopyToDataTable();
                            //t3 = dtEquipment.AsEnumerable().Where(d => d.KIND == "水处理机").CopyToDataTable();
                        }

                        if (t1 != null)
                        {
                            var s = string.Empty;
                            for (int i = 0; i < t1.Rows.Count; i++)
                            {
                                s += t1.Rows[i]["FLNAME"] + ":" + t1.Rows[i]["COUNT"] + "台;" + "\r\n";
                            }
                            if (s.Length > 0)
                            {
                                baseRow = qualityBase.NewMED_QUALITY_BASERow();
                                baseRow.ID = Guid.NewGuid().ToString();
                                baseRow.HOSPITAL_ID = drHospital.HOSPITAL_ID;
                                baseRow.ITEM_NAME = "血透机台数、品牌";
                                baseRow.ITEM_VALUE = s;
                                baseRow.EXTEND_COL = "";
                                baseRow.UPLOAD_TYPE = "设备信息";
                                baseRow.IS_UPLOAD = "0";
                                qualityBase.AddMED_QUALITY_BASERow(baseRow);
                            }
                        }
                        if (t2 != null)
                        {
                            var s = string.Empty;
                            for (int i = 0; i < t2.Rows.Count; i++)
                            {
                                s += t2.Rows[i]["FLNAME"] + ":" + t2.Rows[i]["COUNT"] + "台;" + "\r\n";
                            }
                            if (s.Length > 0)
                            {
                                baseRow = qualityBase.NewMED_QUALITY_BASERow();
                                baseRow.ID = Guid.NewGuid().ToString();
                                baseRow.HOSPITAL_ID = drHospital.HOSPITAL_ID;
                                baseRow.ITEM_NAME = "血滤机台数、品牌";
                                baseRow.ITEM_VALUE = s;
                                baseRow.EXTEND_COL = "";
                                baseRow.UPLOAD_TYPE = "设备信息";
                                baseRow.IS_UPLOAD = "0";
                                qualityBase.AddMED_QUALITY_BASERow(baseRow);
                            }
                        }
                        if (t3 != null)
                        {
                            var s = string.Empty;
                            for (int i = 0; i < t3.Rows.Count; i++)
                            {
                                s += t3.Rows[i]["FLNAME"] + ":" + t3.Rows[i]["COUNT"] + "台;" + "\r\n";
                            }
                            if (s.Length > 0)
                            {
                                baseRow = qualityBase.NewMED_QUALITY_BASERow();
                                baseRow.ID = Guid.NewGuid().ToString();
                                baseRow.HOSPITAL_ID = drHospital.HOSPITAL_ID;
                                baseRow.ITEM_NAME = "水处理机台数、品牌";
                                baseRow.ITEM_VALUE = s;
                                baseRow.EXTEND_COL = "";
                                baseRow.UPLOAD_TYPE = "设备信息";
                                baseRow.IS_UPLOAD = "0";
                                qualityBase.AddMED_QUALITY_BASERow(baseRow);
                            }
                        }
                    }
                }


                //治疗信息
                if (this.xtraTabControl1.SelectedTabPage == this.xtraTabPage5)
                {
                    if (x.AsEnumerable().FirstOrDefault(i => i.UPLOAD_TYPE == "治疗信息") != null)
                    {
                        HideMessage();
                        if (XtraMessageBox.Show("治疗信息已经统计过，是否重新统计?", "治疗信息", MessageBoxButtons.OKCancel) == DialogResult.Cancel)
                        {
                            return;
                        };
                    };
                    ShowMessage();
                    _iconfig.DeleteMED_QUALITY_BASEbyHOSPITAL_IDandKind(drHospital.HOSPITAL_ID, "治疗信息");

                    var control = this.pnlCure.Controls[0] as QueryQualityMonitorCureReport;
                    var dtMonitorCure = control.dtMonitorCure;
                    if (dtMonitorCure.Rows.Count == 0)
                    {
                        HideMessage();
                        if (XtraMessageBox.Show("无治疗信息,是否继续?", "治疗信息", MessageBoxButtons.OKCancel) == DialogResult.Cancel)
                        {
                            return;
                        };
                    }
                    ShowMessage();

                    if (dtMonitorCure != null)
                    {
                        for (int i = 0; i < dtMonitorCure.Rows.Count; i++)
                        {
                            var baseRow = qualityBase.NewMED_QUALITY_BASERow();
                            baseRow.ID = Guid.NewGuid().ToString();
                            baseRow.HOSPITAL_ID = drHospital.HOSPITAL_ID;
                            baseRow.ITEM_NAME = dtMonitorCure.Rows[i]["CURE_NAME"].ToString() + dtMonitorCure.Rows[i]["CURE_CONDITION"].ToString();
                            baseRow.ITEM_VALUE = dtMonitorCure.Rows[i]["CURE_COUNT"].ToString();
                            baseRow.EXTEND_COL = dtMonitorCure.Rows[i]["CURE_RATIO"].ToString();
                            baseRow.UPLOAD_TYPE = "治疗信息";
                            baseRow.IS_UPLOAD = "0";
                            qualityBase.AddMED_QUALITY_BASERow(baseRow);
                        }
                    }
                }

                worker.DoWork += delegate(object sender1, DoWorkEventArgs e1)
                {
                    if (qualityBase != null || qualityBase.Rows.Count > 0)
                    {
                        try
                        {
                            _iconfig.SaveMED_QUALITY_BASE(qualityBase);
                        }
                        catch (Exception ex)
                        {
                            HideMessage();
                            XtraMessageBox.Show(ex.Message.ToString());
                        }
                    }

                };
                worker.RunWorkerCompleted += delegate(object sender2, RunWorkerCompletedEventArgs e2)
                {
                    HideMessage();
                    XtraMessageBox.Show("统计成功！");
                };
                worker.RunWorkerAsync();
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (this.gridVHOSPITAL_INFO.GetFocusedDataRow() != null)
            {
                try
                {
                    if (XtraMessageBox.Show("是否删除该条记录?", "提醒", MessageBoxButtons.OKCancel) == DialogResult.OK)
                    {
                        if (_iconfig.DeleteMED_HOSPITAL_INFOById(this.gridVHOSPITAL_INFO.GetFocusedDataRow()["HOSPITAL_ID"].ToString()) > 0)
                        {
                            GetMED_HOSPITAL_INFO();
                        };
                    }

                }
                catch (Exception ex)
                {
                    XtraMessageBox.Show(ex.ToString());
                }
            }
        }

        private void btnEquipmentEdit_Click(object sender, EventArgs e)
        {
            if (this.gridVEQUIPMENT_INFO.GetFocusedRow() != null)
            {
                if (FlyoutDialog.Show(this.FindForm(), new ctlEquipmentEdit(this.gridVEQUIPMENT_INFO.GetFocusedDataRow()["HOSPITAL_ID"].ToString())) == DialogResult.Yes)
                {
                    GetEquipment_INFO();
                };
            }
        }

        private void gridVEQUIPMENT_INFO_DoubleClick(object sender, EventArgs e)
        {
            if (this.gridVEQUIPMENT_INFO.GetFocusedRow() != null)
            {
                if (FlyoutDialog.Show(this.FindForm(), new ctlEquipmentEdit(this.gridVEQUIPMENT_INFO.GetFocusedDataRow()["HOSPITAL_ID"].ToString())) == DialogResult.Yes)
                {
                    GetEquipment_INFO();
                };
            }
        }

        private void gridVHOSPITAL_INFO_DoubleClick(object sender, EventArgs e)
        {
            if (this.gridVHOSPITAL_INFO.GetFocusedRow() != null)
            {
                if (FlyoutDialog.Show(this.FindForm(), new ctlForHOSPITAL((ConfigModel.MED_HOSPITAL_INFORow)this.gridVHOSPITAL_INFO.GetFocusedDataRow(), dtmedhospitalinfo)) == DialogResult.Yes)
                {
                    GetMED_HOSPITAL_INFO();
                };
            }
        }

        private void xtraTabControl1_SelectedPageChanged(object sender, DevExpress.XtraTab.TabPageChangedEventArgs e)
        {
            if (xtraTabControl1.SelectedTabPage == xtraTabPage2)
            {
                if (this.pnlLab.Controls.Count == 0)
                {
                    var ucItemForLab = new QueryQualityMonitorLabReport();
                    ucItemForLab.Dock = DockStyle.Fill;
                    this.pnlLab.Controls.Add(ucItemForLab);
                }
            }
            else if (xtraTabControl1.SelectedTabPage == xtraTabPage3)
            {
                if (this.pnlInfect.Controls.Count == 0)
                {
                    var infectReport = new QueryQualityMonitorInfectReport();
                    infectReport.Dock = DockStyle.Fill;
                    this.pnlInfect.Controls.Add(infectReport);
                }
            }
            else if (xtraTabControl1.SelectedTabPage == xtraTabPage5)
            {
                if (this.pnlCure.Controls.Count == 0)
                {
                    var cureReport = new QueryQualityMonitorCureReport();
                    cureReport.Dock = DockStyle.Fill;
                    this.pnlCure.Controls.Add(cureReport);
                }
            }
        }

        private void cmbYear_SelectedIndexChanged(object sender, EventArgs e)
        {
            var year = this.cmbYear.EditValue;
            dtStar = new DateTime(Utilities.Utility.CInt(year.ToString()), 1, 1);
            dtEnd = dtStar.AddYears(1).AddSeconds(-1);
        }

        private void cmbCountYear_SelectedIndexChanged(object sender, EventArgs e)
        {
            var year = this.cmbYear.EditValue;
            dtStar = new DateTime(Utilities.Utility.CInt(year.ToString()), 1, 1);
            dtEnd = dtStar.AddYears(1).AddSeconds(-1);
        }

        private void btnExcel_Click(object sender, EventArgs e)
        {
            ExcelNPOIHelper npoiHelp = new ExcelNPOIHelper();

            List<ColoumSetting> list = new List<ColoumSetting>()
           {
              new ColoumSetting(){ CellNum = 1,ColumnName ="ID"},
           };

            ConfigModel.MED_HOSPITAL_INFORow drHospital = null;
            var data = _iconfig.GetMED_HOSPITAL_INFOList();
            var z = data.FirstOrDefault(i => i.HOSPITAL_YEAR == this.cmbYear.EditValue.ToString());
            if (z != null)
            {
                drHospital = z;
            }

            if (drHospital == null)
            {
                XtraMessageBox.Show("请先统计科室信息！");
                return;
            }
            npoiHelp.ExcelTempleteName = "福建省医疗机构开展血液透析基本情况.xls";
            npoiHelp.DS_Main = new DataSet();
            npoiHelp.DS_Main.Tables.Add(_iconfig.GetMED_QUALITY_BASE(drHospital.HOSPITAL_ID));
            npoiHelp.TargetFileName = drHospital.HOSPITAL_YEAR + "福建省医疗机构开展血液透析基本情况";
            npoiHelp.ExportByDirectMode("血透信息", 0, list);

            string filename = "福建省医疗机构开展血液透析基本情况" + System.DateTime.Now.ToString("HHmmss");
            npoiHelp.SaveExcel(filename);

            FlyoutAction aciton = new FlyoutAction();
            aciton.Description = "是否直接打开文件?";
            aciton.Caption = "提示";
            aciton.Commands.Add(FlyoutCommand.Yes);
            aciton.Commands.Add(FlyoutCommand.No);
            if ((FlyoutDialog.Show(this.FindForm(), aciton) == DialogResult.Yes) && (npoiHelp.TargetFileName != null))
            {
                System.Diagnostics.Process.Start(npoiHelp.TargetFileName);
            }
        }

        private void btnALLSAVE_Click(object sender, EventArgs e)
        {
            if (dtALLINFO == null)
            {
                return;
            }
            try
            {
                if (_iconfig.SaveMED_QUALITY_BASE(dtALLINFO) > 0)
                {
                    XtraMessageBox.Show("保存成功");
                };
                GetALLINFO();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.ToString());
            }

        }

        private void gridVAllINFO_CustomColumnDisplayText(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventArgs e)
        {

            if (e.Column == gridColumn30)
            {
                var x = e.Value.ToString();
                switch (x)
                {
                    case "HOSPITAL_NAME":
                        e.DisplayText = "医疗机构名称";
                        break;
                    case "HOSPITAL_LEVEL":
                        e.DisplayText = "医院等级";
                        break;
                    case "CONTACT_PEOPLE":
                        e.DisplayText = "血透室负责人姓名";
                        break;
                    case "CONTACT_PHONE":
                        e.DisplayText = "血透室负责人手机";
                        break;
                    case "CONTACT_EMAIL":
                        e.DisplayText = "血透室负责邮箱";
                        break;
                    case "HEAD_NURSE":
                        e.DisplayText = "血透室护士长姓名";
                        break;
                    case "HEAD_NURSE_PHONE":
                        e.DisplayText = "血透室护士长手机";
                        break;
                    case "HEAD_NURSE_EMAIL":
                        e.DisplayText = "护士长邮箱";
                        break;
                    case "PHYSICIAN_COUNT":
                        e.DisplayText = "血透室医师数量";
                        break;
                    case "NURSE_COUNT":
                        e.DisplayText = "血透室护士数量";
                        break;
                    case "TECHNICIAN_COUNT":
                        e.DisplayText = "血透室技师数量";
                        break;
                    case "ONEHEMOMACHINE_MODEL":
                        e.DisplayText = "一次性血透器品牌";
                        break;
                    case "ISHEMOMACHINE_MULTIPLEX":
                        e.DisplayText = "血透器是否复用";
                        break;
                    case "HEMOMACHINE_MODEL":
                        e.DisplayText = "血透器是否复用品牌";
                        break;
                    case "MULTIPLEX_COUNT":
                        e.DisplayText = "复用机台数";
                        break;
                    case "MULTIPLEX_MODEL":
                        e.DisplayText = "复用机品牌";
                        break;
                    case "MULTIPLEX_ANTISEPTIC_MODEL":
                        e.DisplayText = "消毒剂品牌";
                        break;
                    case "DIALYSATE_CA":
                        e.DisplayText = "透析液钙浓度";
                        break;
                    case "CRRT_MACHINECOUNT":
                        e.DisplayText = "CRRT机台数";
                        break;
                    case "CRRT_MODEL":
                        e.DisplayText = "CRRT品牌";
                        break;
                    case "CRRT_COUNT":
                        e.DisplayText = "开展CRRT治疗例数";
                        break;
                    case "THERAPEUTIC_PROPERTIES":
                        e.DisplayText = "开展血液净化项目";
                        break;
                    default:
                        break;
                }
            }
        }

        private void btnInstruction_Click(object sender, EventArgs e)
        {
            FlyoutDialog.Show(this.FindForm(), new QualityControlRptInstruct("质控平台"));
        }

        /// <summary>
        /// 上传科室信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnUpload_Click(object sender, EventArgs e)
        {
            var dtInfo = this.gridCHOSPITAL_INFO.DataSource as ConfigModel.MED_HOSPITAL_INFODataTable;
            if (dtInfo != null && dtInfo.Rows.Count > 0)
            {
                string msg = string.Empty;
                bool success = true;
                string saveApi = dtConfig.FirstOrDefault(r => r.ITEM_NAME.Equals("SaveHospitalMgrInfo")).ITEM_VALUE;

                //获取用户信息
                ResultMsg<MedUserInfo> resultMsg = WebApiClient.GetUserByName(loginName, getUserApi, getTokenApi);
                if (resultMsg.StatusCode == (int)StatusCodeEnum.Success)
                {
                    var dtResult = JsonConvert.DeserializeObject<MedUserInfo>(resultMsg.Data.ToString());
                    if (dtResult != null)
                    {
                        dtInfo.AsEnumerable().ToList().ForEach(row =>
                        {
                            MedHospitalMgrInfo info = new MedHospitalMgrInfo();
                            info.ID = Guid.NewGuid().ToString();
                            info.HospitalId = dtResult.Company_ID;
                            info.HospitalYear = new DateTime(int.Parse(this.cmbYear.Text), 1, 1);
                            info.HospitalName = row.HOSPITAL_NAME;
                            info.HospitalLevel = row.HOSPITAL_LEVEL;
                            info.ContactPeople = row.CONTACT_PEOPLE;
                            info.ContactPhone = row.CONTACT_PHONE;
                            info.ContactEmail = row.CONTACT_EMAIL;
                            info.HeadNurse = row.HEAD_NURSE;
                            info.HeadNursePhone = row.HEAD_NURSE_PHONE;
                            info.HeadNurseEmail = row.HEAD_NURSE_EMAIL;
                            info.PhysicianCount = Utility.CInt(row.PHYSICIAN_COUNT);
                            info.NurseCount = Utility.CInt(row.NURSE_COUNT);
                            info.TechnicianCount = Utility.CInt(row.TECHNICIAN_COUNT);
                            info.HasStorageRoom = "有";
                            info.HasDirtArea = "有";
                            //info.DialysisAreaCount = 0;
                            //info.BedCount = 0;
                            info.HasInfectArea = "有";
                            info.IsDelete = 0;
                            //info.Editor = string.Empty;
                            //info.Edittime = DateTime.Now;
                            info.Creator = HemoApplicationContext.Current.CurrentUser.USER_NAME;
                            info.Creattime = DateTime.Now;
                            ResultMsg<string> result = WebApiClient.SaveHospitalMgrInfo(info, saveApi, getTokenApi);
                            if (result.StatusCode != (int)StatusCodeEnum.Success)
                            {
                                success = false;
                                msg = result.Info;
                                return;
                            }
                        });
                    }
                    else
                    {
                        success = false;
                        msg = "质控平台用户信息为空！";
                    }
                }
                else
                {
                    success = false;
                    msg = resultMsg.Info;
                }

                if (success)
                {
                    XtraMessageBox.Show("上传成功！");
                }
                else
                {
                    XtraMessageBox.Show("上传失败！\r\n" + msg);
                }
            }
        }

        /// <summary>
        /// 上传检验信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnUploadLab_Click(object sender, EventArgs e)
        {
            //上传之前先检查是否已经上传过检验信息
            string itemName="质控数据-检验信息";
            var dtUploadLog = hemoService.GetUploadLogByItemNameAndYear(itemName, this.cmbYear.Text);
            if (dtUploadLog != null && dtUploadLog.Rows.Count > 0)
            {
                DialogResult dialog = XtraMessageBox.Show(this.cmbYear.Text + "年度检验信息已经上传过，是否继续？", "质控数据", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                if (dialog == DialogResult.Cancel)
                { return; }
            }

            var dtInfo = (this.pnlLab.Controls[0] as QueryQualityMonitorLabReport).GetLabResult();
            if (dtInfo != null && dtInfo.Rows.Count > 0)
            {
                string msg = string.Empty;
                bool success = true;
                string getDictApi = dtConfig.FirstOrDefault(r => r.ITEM_NAME.Equals("GetDictListByType")).ITEM_VALUE;
                string saveMonitorIndexApi = dtConfig.FirstOrDefault(r => r.ITEM_NAME.Equals("SaveMonitorIndex")).ITEM_VALUE;
                string savePrimaryDataApi = dtConfig.FirstOrDefault(r => r.ITEM_NAME.Equals("SavePrimaryData")).ITEM_VALUE;

                //获取用户信息
                ResultMsg<MedUserInfo> resultMsg = WebApiClient.GetUserByName(loginName, getUserApi, getTokenApi);
                if (resultMsg.StatusCode == (int)StatusCodeEnum.Success)
                {
                    var dtResult = JsonConvert.DeserializeObject<MedUserInfo>(resultMsg.Data.ToString());
                    if (dtResult != null)
                    {
                        //上传MedMonitorIndex信息
                        MedMonitorIndex index = new MedMonitorIndex();
                        index.ID = Guid.NewGuid().ToString();
                        index.HospitalId = dtResult.Company_ID;
                        index.HospitalName = dtResult.CompanyName;
                        index.HospitalYear = new DateTime(int.Parse(this.cmbYear.Text), 1, 1);
                        index.IsDelete = 0;
                        index.Creator = HemoApplicationContext.Current.CurrentUser.USER_NAME;
                        index.Creattime = DateTime.Now;
                        ResultMsg<string> result = WebApiClient.SaveMonitorIndex(index, saveMonitorIndexApi, getTokenApi);
                        if (result.StatusCode == (int)StatusCodeEnum.Success)
                        {
                            List<MedDictData> dictList = new List<MedDictData>();
                            ResultMsg<List<MedDictData>> resultDict = WebApiClient.GetDictListByType("监控指标", getDictApi, getTokenApi);
                            if (resultDict.StatusCode == (int)StatusCodeEnum.Success)
                            {
                                dictList = JsonConvert.DeserializeObject<List<MedDictData>>(resultDict.Data.ToString());
                            }

                            //上传MedPrimaryData信息
                            dtInfo.AsEnumerable().ToList().ForEach(row =>
                            {
                                var dictData = dictList.FirstOrDefault(r => r.Value.Contains(row.ITEM_NAME+":" +row.ITEM_VALUE));
                                if (dictData != null)
                                {
                                    MedPrimaryData data = new MedPrimaryData();
                                    data.ID = Guid.NewGuid().ToString();
                                    data.ClinicalId = index.ID;
                                    data.PrimaryId = dictData.ID;
                                    data.PrimaryName = dictData.Name;
                                    data.Count = int.Parse(row.PARENT);
                                    data.Rate = row.PRICE;
                                    result = WebApiClient.SavePrimaryData(data, savePrimaryDataApi, getTokenApi);
                                    if (result.StatusCode != (int)StatusCodeEnum.Success)
                                    {
                                        success = false;
                                        msg = result.Info;
                                        return;
                                    }
                                }
                            });
                        }
                        else
                        {
                            success = false;
                            msg = result.Info;
                        }
                    }
                    else
                    {
                        success = false;
                        msg = "质控平台用户信息为空！";
                    }
                }
                else
                {
                    success = false;
                    msg = resultMsg.Info;
                }

                if (success)
                {
                    //记录上传日志
                    dtUploadLog = new HemodialysisModel.MED_UPLOAD_LOGDataTable();
                    var drUploadLog = dtUploadLog.NewMED_UPLOAD_LOGRow();
                    drUploadLog.ID = Guid.NewGuid().ToString();
                    drUploadLog.UPLOAD_ITEM_NAME = "质控数据-检验信息";
                    drUploadLog.BELONG_YEAR = this.cmbYear.Text;
                    drUploadLog.UPLOADER = HemoApplicationContext.Current.CurrentUser.USER_NAME;
                    drUploadLog.UPLOAD_DATE = DateTime.Now;
                    dtUploadLog.AddMED_UPLOAD_LOGRow(drUploadLog);
                    hemoService.SaveUploadLog(dtUploadLog);
                    XtraMessageBox.Show("上传成功！");
                }
                else
                {
                    XtraMessageBox.Show("上传失败！\r\n" + msg);
                }
            }
        }

        /// <summary>
        /// 上传院感信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnUploadInfect_Click(object sender, EventArgs e)
        {
            //上传之前先检查是否已经上传过院感信息
            string itemName = "质控数据-院感信息";
            var dtUploadLog = hemoService.GetUploadLogByItemNameAndYear(itemName, this.cmbYear.Text);
            if (dtUploadLog != null && dtUploadLog.Rows.Count > 0)
            {
                DialogResult dialog = XtraMessageBox.Show(this.cmbYear.Text + "年度院感信息已经上传过，是否继续？", "质控数据", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                if (dialog == DialogResult.Cancel)
                { return; }
            }

            var dtInfo = (this.pnlInfect.Controls[0] as QueryQualityMonitorInfectReport).GetInfectionResult();
            if (dtInfo != null && dtInfo.Rows.Count > 0)
            {
                string msg = string.Empty;
                bool success = true;
                string saveHospitalInfectionApi = dtConfig.FirstOrDefault(r => r.ITEM_NAME.Equals("SaveHospitalInfection")).ITEM_VALUE;

                //获取用户信息
                ResultMsg<MedUserInfo> resultMsg = WebApiClient.GetUserByName(loginName, getUserApi, getTokenApi);
                if (resultMsg.StatusCode == (int)StatusCodeEnum.Success)
                {
                    var dtResult = JsonConvert.DeserializeObject<MedUserInfo>(resultMsg.Data.ToString());
                    if (dtResult != null)
                    {
                        //上传MedHospitalInfection信息
                        MedHospitalInfection infection = new MedHospitalInfection();
                        var rowYXGY = dtInfo.AsEnumerable().FirstOrDefault(r => r["INFECT_CONDITION"].Equals("乙型肝炎"));
                        var rowBXGY = dtInfo.AsEnumerable().FirstOrDefault(r => r["INFECT_CONDITION"].Equals("丙型肝炎"));
                        var rowAZB = dtInfo.AsEnumerable().FirstOrDefault(r => r["INFECT_CONDITION"].Equals("艾滋病"));
                        var rowMD = dtInfo.AsEnumerable().FirstOrDefault(r => r["INFECT_CONDITION"].Equals("梅毒"));
                        var rowQY = dtInfo.AsEnumerable().FirstOrDefault(r => r["INFECT_CONDITION"].Equals("全阴"));
                        var rowDC = dtInfo.AsEnumerable().FirstOrDefault(r => r["INFECT_CONDITION"].Equals("待查"));
                        var rowZC = dtInfo.AsEnumerable().FirstOrDefault(r => r["INFECT_CONDITION"].Equals("无传染病"));
                        var rowQT = dtInfo.AsEnumerable().FirstOrDefault(r => r["INFECT_CONDITION"].Equals("除乙肝丙肝其他传染病"));
                        infection.ID = Guid.NewGuid().ToString();
                        infection.HospitalId = dtResult.Company_ID;
                        infection.HospitalName = dtResult.CompanyName;
                        infection.HospitalYear = new DateTime(int.Parse(this.cmbYear.Text), 1, 1);
                        infection.YXGYCount = rowYXGY != null ? Utility.CInt(rowYXGY["INFECT_COUNT"].ToString()) : infection.YXGYCount;
                        infection.YXGYRate = rowYXGY != null ? rowYXGY["INFECT_RATIO"].ToString() : infection.YXGYRate;
                        infection.BXGYCount = rowBXGY != null ? Utility.CInt(rowBXGY["INFECT_COUNT"].ToString()) : infection.BXGYCount;
                        infection.BXGYRate = rowBXGY != null ? rowBXGY["INFECT_RATIO"].ToString() : infection.BXGYRate;
                        infection.AZBCount = rowAZB != null ? Utility.CInt(rowAZB["INFECT_COUNT"].ToString()) : infection.AZBCount;
                        infection.AZBRate = rowAZB != null ? rowAZB["INFECT_RATIO"].ToString() : infection.AZBRate;
                        infection.MDCount = rowMD != null ? Utility.CInt(rowMD["INFECT_COUNT"].ToString()) : infection.MDCount;
                        infection.MDRate = rowMD != null ? rowMD["INFECT_RATIO"].ToString() : infection.MDRate;
                        infection.QYCount = rowQY != null ? Utility.CInt(rowQY["INFECT_COUNT"].ToString()) : infection.QYCount;
                        infection.QYRate = rowQY != null ? rowQY["INFECT_RATIO"].ToString() : infection.QYRate;
                        infection.DCCount = rowDC != null ? Utility.CInt(rowDC["INFECT_COUNT"].ToString()) : infection.DCCount;
                        infection.DCRate = rowDC != null ? rowDC["INFECT_RATIO"].ToString() : infection.DCRate;
                        infection.ZCCount = rowZC != null ? Utility.CInt(rowZC["INFECT_COUNT"].ToString()) : infection.ZCCount;
                        infection.ZCRate = rowZC != null ? rowZC["INFECT_RATIO"].ToString() : infection.ZCRate;
                        infection.QTCount = rowQT != null ? Utility.CInt(rowQT["INFECT_COUNT"].ToString()) : infection.QTCount;
                        infection.QTRate = rowQT != null ? rowQT["INFECT_RATIO"].ToString() : infection.QTRate;
                        infection.IsDelete = 0;
                        infection.Creator = HemoApplicationContext.Current.CurrentUser.USER_NAME;
                        infection.Creattime = DateTime.Now;
                        ResultMsg<string> result = WebApiClient.SaveHospitalInfection(infection, saveHospitalInfectionApi, getTokenApi);
                        if (result.StatusCode != (int)StatusCodeEnum.Success)
                        {
                            success = false;
                            msg = result.Info;
                        }
                    }
                    else
                    {
                        success = false;
                        msg = "质控平台用户信息为空！";
                    }
                }
                else
                {
                    success = false;
                    msg = resultMsg.Info;
                }

                if (success)
                {
                    //记录上传日志
                    dtUploadLog = new HemodialysisModel.MED_UPLOAD_LOGDataTable();
                    var drUploadLog = dtUploadLog.NewMED_UPLOAD_LOGRow();
                    drUploadLog.ID = Guid.NewGuid().ToString();
                    drUploadLog.UPLOAD_ITEM_NAME = "质控数据-院感信息";
                    drUploadLog.BELONG_YEAR = this.cmbYear.Text;
                    drUploadLog.UPLOADER = HemoApplicationContext.Current.CurrentUser.USER_NAME;
                    drUploadLog.UPLOAD_DATE = DateTime.Now;
                    dtUploadLog.AddMED_UPLOAD_LOGRow(drUploadLog);
                    hemoService.SaveUploadLog(dtUploadLog);
                    XtraMessageBox.Show("上传成功！");
                }
                else
                {
                    XtraMessageBox.Show("上传失败！\r\n" + msg);
                }
            }
        }

        /// <summary>
        /// 上传设备信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnUploadMachine_Click(object sender, EventArgs e)
        {
            if (this.gridVEQUIPMENT_INFO.RowCount > 0)
            {
                string msg = string.Empty;
                bool success = true;
                string getDictTypeApi = dtConfig.FirstOrDefault(r => r.ITEM_NAME.Equals("GetDictTypeList")).ITEM_VALUE;
                string getDictApi = dtConfig.FirstOrDefault(r => r.ITEM_NAME.Equals("GetDictList")).ITEM_VALUE;
                string saveMachineMgrApi = dtConfig.FirstOrDefault(r => r.ITEM_NAME.Equals("SaveMachineMgr")).ITEM_VALUE;

                //获取用户信息
                ResultMsg<MedUserInfo> resultMsg = WebApiClient.GetUserByName(loginName, getUserApi, getTokenApi);
                if (resultMsg.StatusCode == (int)StatusCodeEnum.Success)
                {
                    var dtResult = JsonConvert.DeserializeObject<MedUserInfo>(resultMsg.Data.ToString());
                    if (dtResult != null)
                    {
                        //上传MedMachineMgr信息
                        DataTable dtMachine = _iconfig.GetQualityControlEquipmentInfo();
                        if (dtMachine != null && dtMachine.Rows.Count > 0)
                        {
                            List<MedDictType> typeList = new List<MedDictType>();
                            ResultMsg<List<MedDictType>> resultType = WebApiClient.GetDictTypeList(getDictTypeApi, getTokenApi);
                            if (resultType.StatusCode == (int)StatusCodeEnum.Success)
                            {
                                typeList = JsonConvert.DeserializeObject<List<MedDictType>>(resultType.Data.ToString());
                            }

                            List<MedDictData> dictList = new List<MedDictData>();
                            ResultMsg<List<MedDictData>> resultDict = WebApiClient.GetDictList(getDictApi, getTokenApi);
                            if (resultDict.StatusCode == (int)StatusCodeEnum.Success)
                            {
                                dictList = JsonConvert.DeserializeObject<List<MedDictData>>(resultDict.Data.ToString());
                            }
                            //获取所有需要上传的机器类型：水处理机、血透机、血滤机
                            var listType = dtMachine.AsEnumerable().GroupBy(i => i["KIND"]).Select(g => (new { ptype = g.Key }));

                            listType.AsEnumerable().ToList().ForEach(r =>
                            {
                              //按照机器类型上传
                                var m = dtMachine.AsEnumerable().Where(i => i.Field<string>("KIND").Equals(r.ptype));
                               var id = Guid.NewGuid().ToString();
                               var creattime = DateTime.Now;
                               m.AsEnumerable().ToList().ForEach(mr => {

                                   MedDictType type = typeList.FirstOrDefault(tr => tr.Name.Equals(mr["KIND"].ToString()));
                                   MedDictData data = type != null ? dictList.FirstOrDefault(fr => fr.DictType_ID.Equals(type.ID) && fr.Name.Equals(mr["FLNAME"].ToString())) : null;
                                   if (data != null)
                                   {

                                       MedMachineMgr machine = new MedMachineMgr();
                                       machine.ID = id;
                                       machine.HospitalId = dtResult.Company_ID;
                                       machine.HospitalName = dtResult.CompanyName;
                                       machine.HospitalYear = new DateTime(int.Parse(this.cmbYear.Text), 1, 1);
                                       machine.MachineType = data.DictType_ID;
                                       machine.MachineTypeName = type.Name;
                                       machine.MachineId = data.ID;
                                       machine.MachineName = data.Name;
                                       machine.MachineCount = Utility.CInt(mr["COUNT"].ToString());
                                       machine.IsDelete = 0;
                                       machine.Creator = HemoApplicationContext.Current.CurrentUser.USER_NAME;
                                       machine.Creattime = creattime;
                                       ResultMsg<string> result = WebApiClient.SaveMachineMgr(machine, saveMachineMgrApi, getTokenApi);
                                       if (result.StatusCode != (int)StatusCodeEnum.Success)
                                       {
                                           success = false;
                                           msg = result.Info;
                                           return;
                                       }
                                   }
                               
                               });

                             });
                            #region 旧的写法
                            //dtMachine.AsEnumerable().ToList().ForEach(row =>
                            //{
                            //    MedDictType type = typeList.FirstOrDefault(r => r.Name.Equals(row["KIND"].ToString()));
                            //    MedDictData data = type != null ? dictList.FirstOrDefault(r => r.DictType_ID.Equals(type.ID) && r.Name.Equals(row["FLNAME"].ToString())) : null;
                            //    if (data != null)
                            //    {

                            //        MedMachineMgr machine = new MedMachineMgr();
                            //        machine.ID = Guid.NewGuid().ToString();
                            //        machine.HospitalId = dtResult.Company_ID;
                            //        machine.HospitalName = dtResult.CompanyName;
                            //        machine.HospitalYear = new DateTime(int.Parse(this.cmbYear.Text), 1, 1);
                            //        machine.MachineType = data.DictType_ID;
                            //        machine.MachineTypeName = type.Name;
                            //        machine.MachineId = data.ID;
                            //        machine.MachineName = data.Name;
                            //        machine.MachineCount = Utility.CInt(row["COUNT"].ToString());
                            //        machine.IsDelete = 0;
                            //        machine.Creator = HemoApplicationContext.Current.CurrentUser.USER_NAME;
                            //        machine.Creattime = DateTime.Now;
                            //        ResultMsg<string> result = WebApiClient.SaveMachineMgr(machine, saveMachineMgrApi, getTokenApi);
                            //        if (result.StatusCode != (int)StatusCodeEnum.Success)
                            //        {
                            //            success = false;
                            //            msg = result.Info;
                            //            return;
                            //        }
                            //    }
                            //});
                            #endregion 
                        }
                        else
                        {
                            success = false;
                            msg = "设备信息为空！";
                        }
                    }
                    else
                    {
                        success = false;
                        msg = "质控平台用户信息为空！";
                    }
                }
                else
                {
                    success = false;
                    msg = resultMsg.Info;
                }

                if (success)
                {
                    XtraMessageBox.Show("上传成功！");
                }
                else
                {
                    XtraMessageBox.Show("上传失败！\r\n" + msg);
                }
            }
        }

        /// <summary>
        /// 上传治疗信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnUploadCure_Click(object sender, EventArgs e)
        {
            //上传之前先检查是否已经上传过治疗信息
            string itemName = "质控数据-治疗信息";
            var dtUploadLog = hemoService.GetUploadLogByItemNameAndYear(itemName, this.cmbYear.Text);
            if (dtUploadLog != null && dtUploadLog.Rows.Count > 0)
            {
                DialogResult dialog = XtraMessageBox.Show(this.cmbYear.Text + "年度治疗信息已经上传过，是否继续？", "质控数据", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                if (dialog == DialogResult.Cancel)
                { return; }
            }

            var dtInfo = (this.pnlCure.Controls[0] as QueryQualityMonitorCureReport).GetCureResult();
            if (dtInfo != null && dtInfo.Rows.Count > 0)
            {
                string msg = string.Empty;
                bool success = true;
                string getDictApi = dtConfig.FirstOrDefault(r => r.ITEM_NAME.Equals("GetDictListByType")).ITEM_VALUE;
                string saveWorkLoadAccountApi = dtConfig.FirstOrDefault(r => r.ITEM_NAME.Equals("SaveWorkLoadAccount")).ITEM_VALUE;
                string saveHemoYreaChartApi = dtConfig.FirstOrDefault(r => r.ITEM_NAME.Equals("SaveHemoYreaChart")).ITEM_VALUE;
                string saveClinicalMgrApi = dtConfig.FirstOrDefault(r => r.ITEM_NAME.Equals("SaveClinicalMgr")).ITEM_VALUE;
                string savePrimaryDataApi = dtConfig.FirstOrDefault(r => r.ITEM_NAME.Equals("SavePrimaryData")).ITEM_VALUE;

                //获取用户信息
                ResultMsg<MedUserInfo> resultMsg = WebApiClient.GetUserByName(loginName, getUserApi, getTokenApi);
                if (resultMsg.StatusCode == (int)StatusCodeEnum.Success)
                {
                    var dtResult = JsonConvert.DeserializeObject<MedUserInfo>(resultMsg.Data.ToString());
                    if (dtResult != null)
                    {
                        #region 上传透析男女比例|透析年龄段|血管通路|规律透析比例

                        var rowMan = dtInfo.AsEnumerable().FirstOrDefault(r => r["CURE_NAME"].Equals("透析男女比例") && r["CURE_CONDITION"].Equals("男性人数"));
                        var rowFemale = dtInfo.AsEnumerable().FirstOrDefault(r => r["CURE_NAME"].Equals("透析男女比例") && r["CURE_CONDITION"].Equals("女性人数"));
                        var row1_20 = dtInfo.AsEnumerable().FirstOrDefault(r => r["CURE_NAME"].Equals("透析年龄段") && r["CURE_CONDITION"].Equals("20岁以下"));
                        var row20_40 = dtInfo.AsEnumerable().FirstOrDefault(r => r["CURE_NAME"].Equals("透析年龄段") && r["CURE_CONDITION"].Equals("20-40岁"));
                        var row40_60 = dtInfo.AsEnumerable().FirstOrDefault(r => r["CURE_NAME"].Equals("透析年龄段") && r["CURE_CONDITION"].Equals("41-60岁"));
                        var row60_100 = dtInfo.AsEnumerable().FirstOrDefault(r => r["CURE_NAME"].Equals("透析年龄段") && r["CURE_CONDITION"].Equals("60岁以上"));
                        var rowAutoFistula = dtInfo.AsEnumerable().FirstOrDefault(r => r["CURE_NAME"].Equals("血管通路") && r["CURE_CONDITION"].Equals("内瘘例数"));
                        var rowGraftsFistula = dtInfo.AsEnumerable().FirstOrDefault(r => r["CURE_NAME"].Equals("血管通路") && r["CURE_CONDITION"].Equals("移植物内瘘"));
                        var rowDoubleVein = dtInfo.AsEnumerable().FirstOrDefault(r => r["CURE_NAME"].Equals("血管通路") && r["CURE_CONDITION"].Equals("双静脉例数"));
                        var rowCuffVenous = dtInfo.AsEnumerable().FirstOrDefault(r => r["CURE_NAME"].Equals("血管通路") && r["CURE_CONDITION"].Equals("带cuff中心静脉留置导管"));
                        var rowOtherVenous = dtInfo.AsEnumerable().FirstOrDefault(r => r["CURE_NAME"].Equals("血管通路") && r["CURE_CONDITION"].Equals("其他通路例数"));
                        var rowRuleTwo = dtInfo.AsEnumerable().FirstOrDefault(r => r["CURE_NAME"].Equals("规律透析比例") && r["CURE_CONDITION"].Equals("每周2次"));
                        var rowRuleThree = dtInfo.AsEnumerable().FirstOrDefault(r => r["CURE_NAME"].Equals("规律透析比例") && r["CURE_CONDITION"].Equals("每周3次"));
                        var rowRuleFour = dtInfo.AsEnumerable().FirstOrDefault(r => r["CURE_NAME"].Equals("规律透析比例") && r["CURE_CONDITION"].Equals("每周4次"));
                        var rowRuleFive = dtInfo.AsEnumerable().FirstOrDefault(r => r["CURE_NAME"].Equals("规律透析比例") && r["CURE_CONDITION"].Equals("每周5次"));
                        var rowRuleOther = dtInfo.AsEnumerable().FirstOrDefault(r => r["CURE_NAME"].Equals("规律透析比例") && r["CURE_CONDITION"].Equals("无规律"));

                        int ruleFourCount = rowRuleFour != null ? Utility.CInt(rowRuleFour["CURE_COUNT"].ToString()) : 0;
                        int ruleFiveCount = rowRuleFive != null ? Utility.CInt(rowRuleFive["CURE_COUNT"].ToString()) : 0;
                        int ruleOtherCount = rowRuleOther != null ? Utility.CInt(rowRuleOther["CURE_COUNT"].ToString()) : 0;
                        Double ruleFourRatio = rowRuleFour != null ? Utility.CDouble(rowRuleFour["CURE_RATIO"].ToString().Substring(0, rowRuleFour["CURE_RATIO"].ToString().IndexOf("%"))) : 0;
                        Double ruleFiveRatio = rowRuleFive != null ? Utility.CDouble(rowRuleFive["CURE_RATIO"].ToString().Substring(0, rowRuleFive["CURE_RATIO"].ToString().IndexOf("%"))) : 0;
                        Double ruleOtherRatio = rowRuleOther != null ? Utility.CDouble(rowRuleOther["CURE_RATIO"].ToString().Substring(0, rowRuleOther["CURE_RATIO"].ToString().IndexOf("%"))) : 0;

                        //上传MedWorkLoadAccount信息
                        MedWorkLoadAccount workLoadAccount = new MedWorkLoadAccount();
                        workLoadAccount.ID = Guid.NewGuid().ToString();
                        workLoadAccount.HospitalId = dtResult.Company_ID;
                        workLoadAccount.HospitalName = dtResult.CompanyName;
                        workLoadAccount.HospitalYear = new DateTime(int.Parse(this.cmbYear.Text), 1, 1);
                        workLoadAccount.ManCount = rowMan != null ? rowMan["CURE_COUNT"].ToString() : workLoadAccount.ManCount;
                        workLoadAccount.Female = rowFemale != null ? rowFemale["CURE_COUNT"].ToString() : workLoadAccount.Female;
                        workLoadAccount.FloAge = row1_20 != null ? row1_20["CURE_COUNT"].ToString() : workLoadAccount.FloAge;
                        workLoadAccount.BLeAge = row20_40 != null ? row20_40["CURE_COUNT"].ToString() : workLoadAccount.BLeAge;
                        workLoadAccount.UpAge = row40_60 != null ? row40_60["CURE_COUNT"].ToString() : workLoadAccount.UpAge;
                        workLoadAccount.LastAge = row60_100 != null ? row60_100["CURE_COUNT"].ToString() : workLoadAccount.LastAge;
                        workLoadAccount.AutoFistula = rowAutoFistula != null ? rowAutoFistula["CURE_COUNT"].ToString() : workLoadAccount.AutoFistula;
                        workLoadAccount.GraftsFistula = rowGraftsFistula != null ? rowGraftsFistula["CURE_COUNT"].ToString() : workLoadAccount.GraftsFistula;
                        workLoadAccount.DoubleVein = rowDoubleVein != null ? rowDoubleVein["CURE_COUNT"].ToString() : workLoadAccount.DoubleVein;
                        workLoadAccount.CuffVenous = rowCuffVenous != null ? rowCuffVenous["CURE_COUNT"].ToString() : workLoadAccount.CuffVenous;
                        workLoadAccount.OtherVenous = rowOtherVenous != null ? rowOtherVenous["CURE_COUNT"].ToString() : workLoadAccount.OtherVenous;
                        workLoadAccount.WeekRoleTwo = rowRuleTwo != null ? rowRuleTwo["CURE_COUNT"].ToString() : workLoadAccount.WeekRoleTwo;
                        workLoadAccount.WeekRoleThree = rowRuleThree != null ? rowRuleThree["CURE_COUNT"].ToString() : workLoadAccount.WeekRoleThree;
                        workLoadAccount.OtherHemoRate = (ruleFourCount + ruleFiveCount + ruleOtherCount).ToString() + "|" + (ruleFourRatio + ruleFiveRatio + ruleOtherRatio) + "%";
                        workLoadAccount.IsDelete = 0;
                        workLoadAccount.Creator = HemoApplicationContext.Current.CurrentUser.USER_NAME;
                        workLoadAccount.CreateTime = DateTime.Now;
                        ResultMsg<string> result = WebApiClient.SaveWorkLoadAccount(workLoadAccount, saveWorkLoadAccountApi, getTokenApi);
                        if (result.StatusCode != (int)StatusCodeEnum.Success)
                        {
                            success = false;
                            msg = result.Info;
                        }

                        #endregion

                        #region 上传透析人次|维持性透析人数|年度总透析例次|年死亡病人数及比例

                        if (success)
                        {
                            var rowHD = dtInfo.AsEnumerable().FirstOrDefault(r => r["CURE_NAME"].Equals("透析人次") && r["CURE_CONDITION"].Equals("HD人次"));
                            var rowHDF = dtInfo.AsEnumerable().FirstOrDefault(r => r["CURE_NAME"].Equals("透析人次") && r["CURE_CONDITION"].Equals("HDF人次"));
                            var rowHF = dtInfo.AsEnumerable().FirstOrDefault(r => r["CURE_NAME"].Equals("透析人次") && r["CURE_CONDITION"].Equals("HF人次"));
                            var rowHP = dtInfo.AsEnumerable().FirstOrDefault(r => r["CURE_NAME"].Equals("透析人次") && r["CURE_CONDITION"].Equals("HP人次"));
                            var rowHDHP = dtInfo.AsEnumerable().FirstOrDefault(r => r["CURE_NAME"].Equals("透析人次") && r["CURE_CONDITION"].Equals("HD+HP人次"));
                            var rowCRRT = dtInfo.AsEnumerable().FirstOrDefault(r => r["CURE_NAME"].Equals("透析人次") && r["CURE_CONDITION"].Equals("CRRT人次"));
                            var rowMaintainCount = dtInfo.AsEnumerable().FirstOrDefault(r => r["CURE_NAME"].Equals("维持性透析人数"));
                            var rowTotalCount = dtInfo.AsEnumerable().FirstOrDefault(r => r["CURE_NAME"].Equals("年度总透析例次"));
                            var rowDeathCount = dtInfo.AsEnumerable().FirstOrDefault(r => r["CURE_NAME"].Equals("年死亡病人数，占维持性透析病人比例"));

                            //上传MedHemoYreaChart信息
                            MedHemoYreaChart hemoYreaChart = new MedHemoYreaChart();
                            hemoYreaChart.ID = Guid.NewGuid().ToString();
                            hemoYreaChart.HospitalId = dtResult.Company_ID;
                            hemoYreaChart.HospitalYear = new DateTime(int.Parse(this.cmbYear.Text), 1, 1);
                            hemoYreaChart.HospitalName = dtResult.CompanyName;
                            hemoYreaChart.Hdcount = rowHD != null ? Utility.CInt(rowHD["CURE_COUNT"].ToString()) : hemoYreaChart.Hdcount;
                            hemoYreaChart.HDRate = rowHD != null ? rowHD["CURE_RATIO"].ToString() : hemoYreaChart.HDRate;
                            hemoYreaChart.Hdfcount = rowHDF != null ? Utility.CInt(rowHDF["CURE_COUNT"].ToString()) : hemoYreaChart.Hdfcount;
                            hemoYreaChart.HDFRate = rowHDF != null ? rowHDF["CURE_RATIO"].ToString() : hemoYreaChart.HDFRate;
                            hemoYreaChart.Hfcount = rowHF != null ? Utility.CInt(rowHF["CURE_COUNT"].ToString()) : hemoYreaChart.Hfcount;
                            hemoYreaChart.HFRate = rowHF != null ? rowHF["CURE_RATIO"].ToString() : hemoYreaChart.HFRate;
                            hemoYreaChart.Hpcount = rowHP != null ? Utility.CInt(rowHP["CURE_COUNT"].ToString()) : hemoYreaChart.Hpcount;
                            hemoYreaChart.HPRate = rowHP != null ? rowHP["CURE_RATIO"].ToString() : hemoYreaChart.HPRate;
                            hemoYreaChart.Hdpcount = rowHDHP != null ? Utility.CInt(rowHDHP["CURE_COUNT"].ToString()) : hemoYreaChart.Hdpcount;
                            hemoYreaChart.HDPRate = rowHDHP != null ? rowHDHP["CURE_RATIO"].ToString() : hemoYreaChart.HDPRate;
                            hemoYreaChart.Crrtcount = rowCRRT != null ? Utility.CInt(rowCRRT["CURE_COUNT"].ToString()) : hemoYreaChart.Crrtcount;
                            hemoYreaChart.CRRTRate = rowCRRT != null ? rowCRRT["CURE_RATIO"].ToString() : hemoYreaChart.CRRTRate;
                            hemoYreaChart.MaintenanceCount = rowMaintainCount != null ? Utility.CInt(rowMaintainCount["CURE_COUNT"].ToString()) : hemoYreaChart.MaintenanceCount;
                            hemoYreaChart.TotalCount = rowTotalCount != null ? Utility.CInt(rowTotalCount["CURE_COUNT"].ToString()) : hemoYreaChart.TotalCount;
                            hemoYreaChart.DeathCount = rowDeathCount != null ? Utility.CInt(rowDeathCount["CURE_COUNT"].ToString()) : hemoYreaChart.DeathCount;
                            hemoYreaChart.DeathRate = rowDeathCount != null ? rowDeathCount["CURE_RATIO"].ToString() : hemoYreaChart.DeathRate;
                            hemoYreaChart.IsDelete = "0";
                            hemoYreaChart.Creator = HemoApplicationContext.Current.CurrentUser.USER_NAME;
                            hemoYreaChart.CreateTime = DateTime.Now;
                            result = WebApiClient.SaveHemoYreaChart(hemoYreaChart, saveHemoYreaChartApi, getTokenApi);
                            if (result.StatusCode != (int)StatusCodeEnum.Success)
                            {
                                success = false;
                                msg = result.Info;
                            }
                        }

                        #endregion

                        #region 上传原发病统计|手术统计

                        if (success)
                        {
                            var rowOP1 = dtInfo.AsEnumerable().FirstOrDefault(r => r["CURE_NAME"].Equals("手术") && r["CURE_CONDITION"].Equals("临时动静脉内瘘术"));
                            var rowOP2 = dtInfo.AsEnumerable().FirstOrDefault(r => r["CURE_NAME"].Equals("手术") && r["CURE_CONDITION"].Equals("半永久导管深静脉置入术"));

                            //上传MedClinicalMgr信息
                            MedClinicalMgr clinicalMgr = new MedClinicalMgr();
                            clinicalMgr.ID = Guid.NewGuid().ToString();
                            clinicalMgr.HospitalId = dtResult.Company_ID;
                            clinicalMgr.HospitalName = dtResult.CompanyName;
                            clinicalMgr.HospitalYear = new DateTime(int.Parse(this.cmbYear.Text), 1, 1);
                            clinicalMgr.AFSCount = rowOP1 != null ? Utility.CInt(rowOP1["CURE_COUNT"].ToString()) : clinicalMgr.AFSCount;
                            clinicalMgr.BYJCount = rowOP2 != null ? Utility.CInt(rowOP2["CURE_COUNT"].ToString()) : clinicalMgr.BYJCount;
                            clinicalMgr.HasAFS = clinicalMgr.AFSCount > 0 ? "开展" : "未开展";
                            clinicalMgr.HasBYJ = clinicalMgr.BYJCount > 0 ? "开展" : "未开展";
                            clinicalMgr.IsDelete = 0;
                            clinicalMgr.Creator = HemoApplicationContext.Current.CurrentUser.USER_NAME;
                            clinicalMgr.Creattime = DateTime.Now;
                            result = WebApiClient.SaveClinicalMgr(clinicalMgr, saveClinicalMgrApi, getTokenApi);
                            if (result.StatusCode == (int)StatusCodeEnum.Success)
                            {
                                List<MedDictData> dictList = new List<MedDictData>();
                                ResultMsg<List<MedDictData>> resultDict = WebApiClient.GetDictListByType("原发病", getDictApi, getTokenApi);
                                if (resultDict.StatusCode == (int)StatusCodeEnum.Success)
                                {
                                    dictList = JsonConvert.DeserializeObject<List<MedDictData>>(resultDict.Data.ToString());
                                }
                                //上传MedPrimaryData信息
                                var rows = dtInfo.AsEnumerable().Where(row => row["CURE_NAME"].Equals("原发病统计"));
                                if (rows != null && rows.Count() > 0)
                                {
                                    rows.ToList().ForEach(row =>
                                    {
                                        var dictData = dictList.FirstOrDefault(r => r.Value.Contains(row["CURE_CONDITION"].ToString()));
                                        MedPrimaryData data = new MedPrimaryData();
                                        data.ID = Guid.NewGuid().ToString();
                                        data.ClinicalId = clinicalMgr.ID;
                                        data.PrimaryId = dictData != null ? dictData.ID : data.PrimaryId;
                                        data.PrimaryName = dictData != null ? dictData.Name : data.PrimaryName;
                                        data.Count = int.Parse(row["CURE_COUNT"].ToString());
                                        data.Rate = row["CURE_RATIO"].ToString();
                                        result = WebApiClient.SavePrimaryData(data, savePrimaryDataApi, getTokenApi);
                                        if (result.StatusCode != (int)StatusCodeEnum.Success)
                                        {
                                            success = false;
                                            msg = result.Info;
                                            return;
                                        }
                                    });
                                }
                            }
                            else
                            {
                                success = false;
                                msg = result.Info;
                            }
                        }

                        #endregion
                    }
                    else
                    {
                        success = false;
                        msg = "质控平台用户信息为空！";
                    }
                }
                else
                {
                    success = false;
                    msg = resultMsg.Info;
                }

                if (success)
                {
                    //记录上传日志
                    dtUploadLog = new HemodialysisModel.MED_UPLOAD_LOGDataTable();
                    var drUploadLog = dtUploadLog.NewMED_UPLOAD_LOGRow();
                    drUploadLog.ID = Guid.NewGuid().ToString();
                    drUploadLog.UPLOAD_ITEM_NAME = "质控数据-治疗信息";
                    drUploadLog.BELONG_YEAR = this.cmbYear.Text;
                    drUploadLog.UPLOADER = HemoApplicationContext.Current.CurrentUser.USER_NAME;
                    drUploadLog.UPLOAD_DATE = DateTime.Now;
                    dtUploadLog.AddMED_UPLOAD_LOGRow(drUploadLog);
                    hemoService.SaveUploadLog(dtUploadLog);
                    XtraMessageBox.Show("上传成功！");
                }
                else
                {
                    XtraMessageBox.Show("上传失败！\r\n" + msg);
                }
            }
        }

        #endregion

        private void Tablctl_SelectedPageChanged(object sender, DevExpress.XtraTab.TabPageChangedEventArgs e)
        {
            if (Tablctl.SelectedTabPageIndex == 1)
            {
                var ctl = new ctlUpPatientInfo();
                ctl.Dock = DockStyle.Fill;
                this.tbp2.Controls.Add(ctl);
            }
        }
    }
}
