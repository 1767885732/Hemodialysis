/*----------------------------------------------------------------
// Copyright (C) 2005 苏州XX公司有限公司
// 描述：抗凝药物用户控件
// 创建时间：2015-04-17
// 创建者：贺建操
//  
// 修改时间：
// 修改人：
// 修改描述：
----------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Hemo.Model;
using Hemo.IService.DataReport;
using Hemo.Service;
using HEMODataReporter;
using HEMODataReporter.PostEntity;
using Hemo.Client.Core;
using Hemo.Client.UI.Machine;
using Hemo.Utilities;

namespace Hemo.Client.UI.DataReport.HemoInfos
{
    public partial class Item_AnticoaGulant : ViewBase
    {
        #region 类变量

        private DataReportModel.MED_CURE_MAINDataTable _cureMainDataTable;
        private IDataReport objDataReport = ServiceManager.Instance.DataReportService;

        #endregion

        #region 属性

        public string _currentPatientHemoId { get; set; }

        #endregion

        #region 构造函数

        public Item_AnticoaGulant(string CurrentPatientHemoId)
        {
            InitializeComponent();
            _currentPatientHemoId = CurrentPatientHemoId;
            InzationData();
        }

        #endregion

        #region 事件

        private void gridView4_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            var dr = gridView4.GetFocusedDataRow() as DataReportModel.MED_CURE_MAINRow;
            if (dr == null)
                return;
            if (e.Clicks == 1)
            {
                if (dr.ISUPLOAD == "1")
                {
                    dr.ISUPLOAD = "0";
                }
                else if (dr.ISUPLOAD == "0")
                {
                    dr.ISUPLOAD = "1";
                }
                else
                {

                }
            }
            else if (e.Clicks == 2)
            {

            }
        }

        private void gridView4_CustomRowCellEdit(object sender, DevExpress.XtraGrid.Views.Grid.CustomRowCellEditEventArgs e)
        {
            if (e.Column == gridColumn15)
            {
                var curRow = (DataReportModel.MED_CURE_MAINRow)gridView4.GetDataRow(e.RowHandle);
                if (curRow == null)
                    return;
                if (curRow.ISUPLOAD == "2")
                {
                    var cloneRepository = e.RepositoryItem.Clone() as DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit;
                    cloneRepository.Click += new EventHandler(cloneRepository_Click);
                    cloneRepository.ReadOnly = true;
                    e.RepositoryItem = cloneRepository;
                }
            }
        }
        void cloneRepository_Click(object sender, EventArgs e)
        {
            MessageBox.Show("已上传不能再上传");
        }

        #endregion

        #region 方法

        /// <summary>
        /// 加载通路信息
        /// </summary>
        public override void InzationData()
        {
            using (var _worker = new BackgroundWorker())
            {
                _cureMainDataTable = new DataReportModel.MED_CURE_MAINDataTable();
                _worker.DoWork += delegate(object sender, DoWorkEventArgs e)
                {
                    _cureMainDataTable = objDataReport.GetDataReportPatientAncitoaList(_currentPatientHemoId, "5");

                };
                _worker.RunWorkerCompleted += delegate(object sender1, RunWorkerCompletedEventArgs r1)
                {
                    this.gridControl1.DataSource = _cureMainDataTable;
                };
                _worker.RunWorkerAsync();
            }
        }

        public override void GetVascualToUpLoad(string resultInfo)
        {
            var dtSource = ((System.Data.DataView)(this.gridView4.DataSource)).Table as DataReportModel.MED_CURE_MAINDataTable;
            var dt = new DataReportModel.MED_PATIENT_DATAREPORTDataTable();
            DataReporter reporter = HemoApplicationContext.Current.dataCurrentReport;// new DataReporter("hn002", "0");
            //做一个判断 
            if (reporter == null || !reporter.loginResult.Success)
            {
                reporter = HemoApplicationContext.Current.IsLoginHospitalHemoPlatForm;

                if (reporter != null && reporter.loginResult.Success)
                {
                    AutoClosedMsgBox.Show("登录成功！", "提示", 5000, 0);
                }
                else
                {
                    XtraMessageBox.Show("登录失败，请重新登录！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }
            foreach (DataReportModel.MED_CURE_MAINRow row in dtSource.Rows)
            {
                if (row.ISUPLOAD == "1")
                {
                    var aindex = row.IsFIRST_HEPARINNull() ? 0 : row.FIRST_HEPARIN.ToString().IndexOf('.') == -1 ? row.FIRST_HEPARIN.ToString().Length : row.FIRST_HEPARIN.ToString().IndexOf('.');
                    var bindex = row.IsDOSIS_SUSTENTATIVANull() ? 0 : row.DOSIS_SUSTENTATIVA.ToString().IndexOf('.') == -1 ? row.DOSIS_SUSTENTATIVA.ToString().Length : row.DOSIS_SUSTENTATIVA.ToString().IndexOf('.');
                    var cindex = row.IsDOCTOR_ADVICENull() ? 0 : row.DOCTOR_ADVICE.ToString().IndexOf('.') == -1 ? row.DOSIS_SUSTENTATIVA.ToString().Length : row.DOCTOR_ADVICE.ToString().IndexOf('.');
                    var FirNum = row.IsFIRST_HEPARINNull() ? "0" : row.FIRST_HEPARIN.ToString().Substring(0, aindex);
                    var SecNum = row.IsDOSIS_SUSTENTATIVANull() ? "0" : row.DOSIS_SUSTENTATIVA.ToString().Substring(0, bindex);
                    var TimNum = row.IsDOCTOR_ADVICENull() ? "0" : row.DOCTOR_ADVICE.ToString().Substring(0, cindex);
                    var SFSYTemp = string.IsNullOrEmpty(row.ITEM_NAME) ? "否" : "是";
                    var GSSJLTemp = !row.ITEM_NAME.Equals("低分子肝素") ? FirNum : string.Empty;
                    var GSZJJLTemp = !row.ITEM_NAME.Equals("低分子肝素") ? SecNum : string.Empty;
                    var GSZJLTemp = !row.ITEM_NAME.Equals("低分子肝素") ? Convert.ToString(Utilities.Utility.CInt(FirNum) + Convert.ToInt32(SecNum)) : string.Empty;
                    var DFZGSFLTemp = row.ITEM_NAME.Equals("低分子肝素") ? "低分子肝素钠" : string.Empty;
                    var DFZGSNTemp = row.ITEM_NAME.Equals("低分子肝素") ? "法安明" : string.Empty;
                    var DFZJLTemp = row.ITEM_NAME.Equals("低分子肝素") ? FirNum : string.Empty;
                    var DFZGSZJJLTemp = row.ITEM_NAME.Equals("低分子肝素") ? SecNum : string.Empty;
                    var DFZGSZJSJTemp = row.ITEM_NAME.Equals("低分子肝素") ? TimNum : string.Empty;
                    var DFZGSZJLTemp = row.ITEM_NAME.Equals("低分子肝素") ? (Utilities.Utility.CDecimal(FirNum) + Convert.ToDecimal(SecNum) * Convert.ToDecimal(TimNum)).ToString("G") : string.Empty;



                    //去进行上传操作                   
                    ReportResult result = reporter.ReportXTKNJ(new XTKNJEntity(resultInfo, row.CURE_CREATE_DATE.Date, row.ITEM_NAME == "普通肝素" ? "肝素" : string.IsNullOrEmpty(row.ITEM_NAME) ? "无抗凝剂" : row.ITEM_NAME.Equals("低分子肝素") ? "低分子肝素" : row.ITEM_NAME.Equals(" 枸橼酸") ? "枸橼酸" : "其它")
                    {
                        SFSY = SFSYTemp,//是否使用
                        GSSJL = GSSJLTemp,//肝素首剂量
                        GSZJJL = GSZJJLTemp,//肝素追加剂量
                        GSZJL = GSZJLTemp,//肝素总量
                        DFZGSFL = DFZGSFLTemp,//低分子肝素钠
                        DFZGSN = DFZGSNTemp,//法明安
                        DFZJL = DFZJLTemp,//低分子肝素剂量
                        DFZGSZJJL = DFZGSZJJLTemp,//低分子肝素追加剂量
                        DFZGSZJSJ = DFZGSZJSJTemp,//低分子肝素追加时间
                        DFZGSZJL = DFZGSZJLTemp//低分子肝素总剂量
                    });
                    if (result.Success)
                    {
                        var rowExtend = dt.NewMED_PATIENT_DATAREPORTRow();
                        rowExtend.ID = Guid.NewGuid().ToString();
                        rowExtend.HEMODIALYSIS_ID = row.HEMODIALYSIS_ID;
                        rowExtend.BASEINFO = result.Info;
                        rowExtend.STATE = "1";//成功
                        rowExtend.TYPE = "5";
                        rowExtend.EXTEND = "XTXX";
                        rowExtend.EXTEND1 = "血透信息";
                        rowExtend.EXTEND5 = "全国数据上报平台";
                        rowExtend.UPTIME = System.DateTime.Now;
                        rowExtend.UPUSER = HemoApplicationContext.Current.CurrentUser.USER_ID;
                        rowExtend.MAPIP = row.CURE_ID;
                        dt.AddMED_PATIENT_DATAREPORTRow(rowExtend);
                        //break;
                    }
                    else
                    {
                        var rowExtend = dt.NewMED_PATIENT_DATAREPORTRow();
                        rowExtend.ID = Guid.NewGuid().ToString();
                        rowExtend.HEMODIALYSIS_ID = row.HEMODIALYSIS_ID;
                        rowExtend.BASEINFO = result.Info;
                        rowExtend.STATE = "0";//失败
                        rowExtend.TYPE = "5";
                        rowExtend.EXTEND = "XTXX";
                        rowExtend.EXTEND1 = "血透信息";
                        rowExtend.EXTEND5="全国数据上报平台";
                        rowExtend.UPTIME = System.DateTime.Now;
                        rowExtend.UPUSER = HemoApplicationContext.Current.CurrentUser.USER_ID;
                        rowExtend.MAPIP = row.CURE_ID;
                        dt.AddMED_PATIENT_DATAREPORTRow(rowExtend);
                    }
                }
            }
            var reINT = objDataReport.SavePatientIsUploadDt(dt);
            if (reINT > 0)
            {
                MessageBox.Show("成功");
            }
            else
            {
                MessageBox.Show("失败");
            }
        }

        #endregion
    }
}
