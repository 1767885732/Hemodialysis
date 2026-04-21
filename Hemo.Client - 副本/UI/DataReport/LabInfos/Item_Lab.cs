/*----------------------------------------------------------------
// Copyright (C) 2005 苏州XX公司有限公司
// 描述：检验项目用户控件
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
using System.Linq;
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

namespace Hemo.Client.UI.DataReport.LabInfos
{
    public partial class Item_Lab : ViewBase
    {
        #region 类变量

        private DataReportModel.MED_LAB_MASTERDataTable _patientLabDataTable;
        private IDataReport objDataReport = ServiceManager.Instance.DataReportService;

        #endregion

        #region 属性

        private string _currentPatientId { get; set; }

        private string _currentPatientHemoId { get; set; }

        private DateTime StarTime { get; set; }
        private DateTime EndTiem { get; set; }

        #endregion

        #region 构造函数

        public Item_Lab(string CurrentPatientId, string CurrentPatientHemoID, DateTime startime, DateTime endtime)
        {
            InitializeComponent();
            _currentPatientId = CurrentPatientId;
            _currentPatientHemoId = CurrentPatientHemoID;
            StarTime = startime;
            EndTiem = endtime;
            Query(StarTime, EndTiem);
        }

        #endregion

        #region 事件

        private void gridView4_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            var dr = gridView4.GetFocusedDataRow() as DataReportModel.MED_LAB_MASTERRow;
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
                var curRow = (DataReportModel.MED_LAB_MASTERRow)gridView4.GetDataRow(e.RowHandle);
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
        /// 加载检验信息
        /// </summary>
        /// <param name="dtStar">开始时间</param>
        /// <param name="dtEnd">结束时间</param>
        public override void Query(DateTime dtStar, DateTime dtEnd)
        {
            using (var _worker = new BackgroundWorker())
            {
                _patientLabDataTable = new DataReportModel.MED_LAB_MASTERDataTable();
                _worker.DoWork += delegate(object sender, DoWorkEventArgs e)
                {
                    _patientLabDataTable = objDataReport.GetDataReportPatientLabList(_currentPatientId, "0", dtStar, dtEnd);

                };
                _worker.RunWorkerCompleted += delegate(object sender1, RunWorkerCompletedEventArgs r1)
                {
                    this.gridControl1.DataSource = _patientLabDataTable;
                };
                _worker.RunWorkerAsync();
            }
        }

        /// <summary>
        /// 上传的方法
        /// </summary>
        /// <param name="resultInfo"></param>
        public override void GetVascualToUpLoad(string resultInfo)
        {
            var dtSource = ((System.Data.DataView)(this.gridView4.DataSource)).Table as DataReportModel.MED_LAB_MASTERDataTable;
            var dt = new DataReportModel.MED_PATIENT_DATAREPORTDataTable();

            DataReporter reporter = HemoApplicationContext.Current.dataCurrentReport;// new DataReporter("hn002", "0");
            //做一个登录状态的判断，如果没有登录那么重新登录。 
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
            //进行分组
            var query = from t in dtSource
                        group t by new { t.REPORT_ITEM_CODE } into m
                        select new
                        {
                            name = m.Key,
                            count = m.Count()
                        };
            //获取最大组的数量
            int rowCoun = 0;
            foreach (var item in query)
            {
                if (item.count > rowCoun)
                {
                    rowCoun = item.count;
                }
            }
            var labDetail = new DataReportModel.MED_LAB_UPLOADDataTable();
            //将数据进行行列转换，以便于进行数据封装去进行上传操作.
            for (int i = 0; i < rowCoun; i++)
            {
                var newRow = labDetail.NewMED_LAB_UPLOADRow();
                newRow.ID = Guid.NewGuid().ToString();
                bool isAdd = false;
                foreach (DataReportModel.MED_LAB_MASTERRow row in dtSource.Rows)
                {
                    isAdd = false;

                    var roDefault = dt.FirstOrDefault(p => p.EXTEND == "LABEXAM" && p.MAPIP == row.TEST_NO && p.EXTEND2 == row.REPORT_ITEM_CODE);
                    if (row.ISUPLOAD == "1" && roDefault == null)
                    {
                        #region 血常规
                        if (string.IsNullOrEmpty(newRow.BXBJC) && row.REPORT_ITEM_NAME == "白细胞")
                        {
                            newRow.BXBJC = row.REPORT_ITEM_NAME == "白细胞" ? ReplaceSpecialChar(row.RESULT) : string.Empty;
                            isAdd = true;
                        }

                        if (string.IsNullOrEmpty(newRow.XHDBJC) && row.REPORT_ITEM_NAME == "血红蛋白")
                        {
                            newRow.XHDBJC = row.REPORT_ITEM_NAME == "血红蛋白" ? ReplaceSpecialChar(row.RESULT) : string.Empty;
                            isAdd = true;
                        }


                        if (string.IsNullOrEmpty(newRow.HXBYJJC) && row.REPORT_ITEM_NAME == "红细胞压积")
                        {
                            newRow.HXBYJJC = row.REPORT_ITEM_NAME == "红细胞压积" ? ReplaceSpecialChar(row.RESULT) : string.Empty;
                            isAdd = true;
                        }


                        if (string.IsNullOrEmpty(newRow.XXBJC) && row.REPORT_ITEM_NAME == "血小板")
                        {
                            newRow.XXBJC = row.REPORT_ITEM_NAME == "血小板" ? ReplaceSpecialChar(row.RESULT) : string.Empty;
                            isAdd = true;
                        }


                        //日期
                        if (!string.IsNullOrEmpty(newRow.XCGRQ) || !string.IsNullOrEmpty(newRow.XHDBJC) || !string.IsNullOrEmpty(newRow.HXBYJJC) || !string.IsNullOrEmpty(newRow.XXBJC))
                        {
                            if (string.IsNullOrEmpty(newRow.XCGRQ))
                            {
                                newRow.XCGRQ = row.RESULTS_RPT_DATE_TIME;
                                isAdd = true;
                            }
                        }
                        #endregion
                        #region 骨矿物质代谢
                        if (string.IsNullOrEmpty(newRow.XZG) && row.REPORT_ITEM_NAME == "血总钙")
                        {
                            newRow.XZG = row.REPORT_ITEM_NAME == "血总钙" ? ReplaceSpecialChar(row.RESULT) : string.Empty;
                            isAdd = true;
                        }

                        if (string.IsNullOrEmpty(newRow.XL) && row.REPORT_ITEM_NAME == "血磷")
                        {
                            newRow.XL = row.REPORT_ITEM_NAME == "血磷" ? ReplaceSpecialChar(row.RESULT) : string.Empty;
                            isAdd = true;
                        }

                        if (string.IsNullOrEmpty(newRow.IPTH) && row.REPORT_ITEM_NAME == "iPTH")
                        {
                            newRow.IPTH = row.REPORT_ITEM_NAME == "iPTH" ? ReplaceSpecialChar(row.RESULT) : string.Empty;
                            isAdd = true;
                        }



                        if (!string.IsNullOrEmpty(newRow.XZG) || !string.IsNullOrEmpty(newRow.XL) || !string.IsNullOrEmpty(newRow.IPTH))
                        {
                            if (string.IsNullOrEmpty(newRow.JCRQXZG))
                            {
                                newRow.JCRQXZG = row.RESULTS_RPT_DATE_TIME;
                                isAdd = true;
                            }

                        }
                        #endregion
                        #region 铁代谢
                        if (string.IsNullOrEmpty(newRow.XTQE) && row.REPORT_ITEM_NAME == "血清铁")
                        {
                            newRow.XTQE = row.REPORT_ITEM_NAME == "血清铁" ? ReplaceSpecialChar(row.RESULT) : string.Empty;
                            isAdd = true;
                        }

                        if (string.IsNullOrEmpty(newRow.ZTJHL) && row.REPORT_ITEM_NAME == "总铁结合力")
                        {
                            newRow.ZTJHL = row.REPORT_ITEM_NAME == "总铁结合力" ? ReplaceSpecialChar(row.RESULT) : string.Empty;
                            isAdd = true;
                        }
                        if (string.IsNullOrEmpty(newRow.ZTBHD) && row.REPORT_ITEM_NAME == "转铁饱和度")
                        {
                            newRow.ZTBHD = row.REPORT_ITEM_NAME == "转铁饱和度" ? ReplaceSpecialChar(row.RESULT) : string.Empty;
                            isAdd = true;
                        }

                        if (string.IsNullOrEmpty(newRow.TDB) && row.REPORT_ITEM_NAME == "铁蛋白")
                        {
                            newRow.TDB = row.REPORT_ITEM_NAME == "铁蛋白" ? ReplaceSpecialChar(row.RESULT) : string.Empty;
                            isAdd = true;
                        }


                        if (!string.IsNullOrEmpty(newRow.XTQE) || !string.IsNullOrEmpty(newRow.ZTJHL) || !string.IsNullOrEmpty(newRow.ZTBHD) || !string.IsNullOrEmpty(newRow.TDB))
                        {
                            if (string.IsNullOrEmpty(newRow.JCRQC))
                            {
                                newRow.JCRQC = row.RESULTS_RPT_DATE_TIME;
                                isAdd = true;
                            }


                        }
                        #endregion
                        #region 生化检查（透析前）
                        if (string.IsNullOrEmpty(newRow.TQNSMGDL) && row.REPORT_ITEM_NAME == "尿素" && row.UNITS == "mmol/L")
                        {
                            newRow.TQNSMGDL = row.REPORT_ITEM_NAME == "尿素" ? row.UNITS == "mmol/L" ? string.Empty : row.RESULT : string.Empty;
                            isAdd = true;
                        }

                        if (string.IsNullOrEmpty(newRow.NIAOSU) && row.REPORT_ITEM_NAME == "尿素" && row.UNITS != "mmol/L")
                        {
                            newRow.NIAOSU = row.REPORT_ITEM_NAME == "尿素" ? row.UNITS != "mmol/L" ? string.Empty : row.RESULT : string.Empty;
                            isAdd = true;
                        }

                        if (string.IsNullOrEmpty(newRow.THJGMGDL) && row.REPORT_ITEM_NAME == "肌酐" && row.UNITS == "μmol/L")
                        {
                            newRow.THJGMGDL = row.REPORT_ITEM_NAME == "肌酐" ? row.UNITS == "μmol/L" ? string.Empty : row.RESULT : string.Empty;
                            isAdd = true;
                        }

                        if (string.IsNullOrEmpty(newRow.JIGAN) && row.REPORT_ITEM_NAME == "肌酐" && row.UNITS != "μmol/L")
                        {
                            newRow.JIGAN = row.REPORT_ITEM_NAME == "肌酐" ? row.UNITS != "μmol/L" ? string.Empty : row.RESULT : string.Empty;
                            isAdd = true;
                        }

                        if (string.IsNullOrEmpty(newRow.ZZDBA) && row.REPORT_ITEM_NAME == "血总蛋白")
                        {
                            newRow.ZZDBA = row.REPORT_ITEM_NAME == "血总蛋白" ? ReplaceSpecialChar(row.RESULT) : string.Empty;
                            isAdd = true;
                        }

                        if (string.IsNullOrEmpty(newRow.XBDBV) && row.REPORT_ITEM_NAME == "血白蛋白")
                        {
                            newRow.XBDBV = row.REPORT_ITEM_NAME == "血白蛋白" ? ReplaceSpecialChar(row.RESULT) : string.Empty;
                            isAdd = true;
                        }

                        if (string.IsNullOrEmpty(newRow.ASTADD) && row.REPORT_ITEM_NAME == "AST")
                        {
                            newRow.ASTADD = row.REPORT_ITEM_NAME == "AST" ? ReplaceSpecialChar(row.RESULT) : string.Empty;
                            isAdd = true;
                        }

                        if (string.IsNullOrEmpty(newRow.ALTADD) && row.REPORT_ITEM_NAME == "ALT")
                        {
                            newRow.ALTADD = row.REPORT_ITEM_NAME == "ALT" ? ReplaceSpecialChar(row.RESULT) : string.Empty;
                            isAdd = true;
                        }

                        if (string.IsNullOrEmpty(newRow.ZDHSADD) && row.REPORT_ITEM_NAME == "总胆红素")
                        {
                            newRow.ZDHSADD = row.REPORT_ITEM_NAME == "总胆红素" ? ReplaceSpecialChar(row.RESULT) : string.Empty;
                            isAdd = true;
                        }

                        if (string.IsNullOrEmpty(newRow.GYSZ) && row.REPORT_ITEM_NAME == "甘油三酯")
                        {
                            newRow.GYSZ = row.REPORT_ITEM_NAME == "甘油三酯" ? ReplaceSpecialChar(row.RESULT) : string.Empty;
                            isAdd = true;
                        }

                        if (string.IsNullOrEmpty(newRow.ZDGC) && row.REPORT_ITEM_NAME == "总胆固醇")
                        {
                            newRow.ZDGC = row.REPORT_ITEM_NAME == "总胆固醇" ? ReplaceSpecialChar(row.RESULT) : string.Empty;
                            isAdd = true;
                        }

                        if (string.IsNullOrEmpty(newRow.DMDZDB) && row.REPORT_ITEM_NAME == "低密度脂蛋白")
                        {
                            newRow.DMDZDB = row.REPORT_ITEM_NAME == "低密度脂蛋白" ? ReplaceSpecialChar(row.RESULT) : string.Empty;
                            isAdd = true;
                        }

                        if (string.IsNullOrEmpty(newRow.GMDZDB) && row.REPORT_ITEM_NAME == "高密度脂蛋白")
                        {
                            newRow.GMDZDB = row.REPORT_ITEM_NAME == "高密度脂蛋白" ? ReplaceSpecialChar(row.RESULT) : string.Empty;
                            isAdd = true;
                        }

                        if (string.IsNullOrEmpty(newRow.XUETANG) && row.REPORT_ITEM_NAME == "血糖")
                        {
                            newRow.XUETANG = row.REPORT_ITEM_NAME == "血糖" ? ReplaceSpecialChar(row.RESULT) : string.Empty;
                            isAdd = true;
                        }

                        if (string.IsNullOrEmpty(newRow.XUEJIA) && row.REPORT_ITEM_NAME == "血钾")
                        {
                            newRow.XUEJIA = row.REPORT_ITEM_NAME == "血钾" ? ReplaceSpecialChar(row.RESULT) : string.Empty;
                            isAdd = true;
                        }

                        if (string.IsNullOrEmpty(newRow.XUENA) && row.REPORT_ITEM_NAME == "血钠")
                        {
                            newRow.XUENA = row.REPORT_ITEM_NAME == "血钠" ? ReplaceSpecialChar(row.RESULT) : string.Empty;
                            isAdd = true;
                        }

                        if (string.IsNullOrEmpty(newRow.XUELV) && row.REPORT_ITEM_NAME == "血氯")
                        {
                            newRow.XUELV = row.REPORT_ITEM_NAME == "血氯" ? ReplaceSpecialChar(row.RESULT) : string.Empty;
                            isAdd = true;
                        }

                        if (string.IsNullOrEmpty(newRow.ERYANGHUATAN) && row.REPORT_ITEM_NAME == "二氧化碳")
                        {
                            newRow.ERYANGHUATAN = row.REPORT_ITEM_NAME == "二氧化碳" ? ReplaceSpecialChar(row.RESULT) : string.Empty;
                            isAdd = true;

                        }


                        if (!string.IsNullOrEmpty(newRow.XTQE) || !string.IsNullOrEmpty(newRow.ZTJHL) || !string.IsNullOrEmpty(newRow.ZTBHD) || !string.IsNullOrEmpty(newRow.TDB))
                        {
                            if (string.IsNullOrEmpty(newRow.JCRQN))
                            {
                                newRow.JCRQN = row.RESULTS_RPT_DATE_TIME;
                                isAdd = true;
                            }

                        }

                        #endregion
                        #region C反应蛋白与β2微球蛋白
                        if (string.IsNullOrEmpty(newRow.CFYDBJC) && row.REPORT_ITEM_NAME == "C反应蛋白")
                        {
                            newRow.CFYDBJC = row.REPORT_ITEM_NAME == "C反应蛋白" ? ReplaceSpecialChar(row.RESULT) : string.Empty;
                            isAdd = true;
                        }

                        if (string.IsNullOrEmpty(newRow.BTEWQDBJC) && row.REPORT_ITEM_NAME == "β2微球蛋白")
                        {
                            newRow.BTEWQDBJC = row.REPORT_ITEM_NAME == "β2微球蛋白" ? ReplaceSpecialChar(row.RESULT) : string.Empty;
                            isAdd = true;

                        }

                        if (!string.IsNullOrEmpty(newRow.CFYDBJC) || !string.IsNullOrEmpty(newRow.BTEWQDBJC))
                        {
                            if (string.IsNullOrEmpty(newRow.JCRQZZZ))
                            {
                                newRow.JCRQZZZ = row.RESULTS_RPT_DATE_TIME;
                                isAdd = true;
                            }

                        }
                        #endregion
                        #region 乙肝二对半（定性）
                        if (string.IsNullOrEmpty(newRow.HBSAG) && row.REPORT_ITEM_NAME == "HBsAg")
                        {
                            newRow.HBSAG = row.REPORT_ITEM_NAME == "HBsAg" ? ReplaceSpecialChar(row.RESULT) : string.Empty;
                            isAdd = true;

                        }

                        if (string.IsNullOrEmpty(newRow.ANTIHBS) && row.REPORT_ITEM_NAME == "AntiHBs")
                        {
                            newRow.ANTIHBS = row.REPORT_ITEM_NAME == "AntiHBs" ? ReplaceSpecialChar(row.RESULT) : string.Empty;
                            isAdd = true;

                        }

                        if (string.IsNullOrEmpty(newRow.HBEAG) && row.REPORT_ITEM_NAME == "HBeAg")
                        {
                            newRow.HBEAG = row.REPORT_ITEM_NAME == "HBeAg" ? ReplaceSpecialChar(row.RESULT) : string.Empty;
                            isAdd = true;
                        }

                        if (string.IsNullOrEmpty(newRow.ANTIHBE) && row.REPORT_ITEM_NAME == "AntiHBe")
                        {
                            newRow.ANTIHBE = row.REPORT_ITEM_NAME == "AntiHBe" ? ReplaceSpecialChar(row.RESULT) : string.Empty;
                            isAdd = true;
                        }

                        if (string.IsNullOrEmpty(newRow.ANTIHBC) && row.REPORT_ITEM_NAME == "AntiHBc")
                        {
                            newRow.ANTIHBC = row.REPORT_ITEM_NAME == "AntiHBc" ? ReplaceSpecialChar(row.RESULT) : string.Empty;
                            isAdd = true;
                        }

                        if (!string.IsNullOrEmpty(newRow.HBSAG) || !string.IsNullOrEmpty(newRow.ANTIHBS) || !string.IsNullOrEmpty(newRow.HBEAG) || !string.IsNullOrEmpty(newRow.ANTIHBE) || !string.IsNullOrEmpty(newRow.ANTIHBC))
                        {
                            if (string.IsNullOrEmpty(newRow.JCRQE))
                            {
                                newRow.JCRQE = row.RESULTS_RPT_DATE_TIME;
                                isAdd = true;
                            }

                        }
                        #endregion
                        #region HBV DNA
                        if (string.IsNullOrEmpty(newRow.HBV_NUMBER) && row.REPORT_ITEM_NAME == "HBVDNA")
                        {
                            newRow.HBV_NUMBER = row.REPORT_ITEM_NAME == "HBVDNA" ? ReplaceSpecialChar(row.RESULT) : string.Empty;

                            isAdd = true;
                        }


                        if (!string.IsNullOrEmpty(newRow.HBV_NUMBER))
                        {
                            if (string.IsNullOrEmpty(newRow.JCRQD))
                            {
                                newRow.JCRQD = row.RESULTS_RPT_DATE_TIME;
                                isAdd = true;
                            }

                        }

                        #endregion
                        #region HBV_NUMBER
                        if (string.IsNullOrEmpty(newRow.HCVBB) && row.REPORT_ITEM_NAME == "AntiHCV")
                        {
                            newRow.HCVBB = row.REPORT_ITEM_NAME == "AntiHCV" ? ReplaceSpecialChar(row.RESULT) : string.Empty;
                            isAdd = true;
                        }


                        if (!string.IsNullOrEmpty(newRow.HCVBB))
                        {
                            if (string.IsNullOrEmpty(newRow.JCRQF))
                            {
                                newRow.JCRQF = row.RESULTS_RPT_DATE_TIME;
                                isAdd = true;
                            }

                        }
                        #endregion
                        #region HCV-RNA
                        if (string.IsNullOrEmpty(newRow.HCVRNAV_NUMBER) && row.REPORT_ITEM_NAME == "HCV-RNA")
                        {
                            newRow.HCVRNAV_NUMBER = row.REPORT_ITEM_NAME == "HCV-RNA" ? ReplaceSpecialChar(row.RESULT) : string.Empty;
                            isAdd = true;
                        }


                        if (!string.IsNullOrEmpty(newRow.HCVRNAV_NUMBER))
                        {
                            if (string.IsNullOrEmpty(newRow.JCRQHCV))
                            {
                                newRow.JCRQHCV = row.RESULTS_RPT_DATE_TIME;
                                isAdd = true;
                            }

                        }


                        #endregion
                        #region 艾滋病
                        if (string.IsNullOrEmpty(newRow.AIDRES) && row.REPORT_ITEM_NAME == "艾滋病")
                        {
                            newRow.AIDRES = row.REPORT_ITEM_NAME == "艾滋病" ? ReplaceSpecialChar(row.RESULT) : string.Empty;
                            isAdd = true;
                        }


                        if (!string.IsNullOrEmpty(newRow.AIDRES))
                        {
                            if (string.IsNullOrEmpty(newRow.JCRQAIDS))
                            {
                                newRow.JCRQAIDS = row.RESULTS_RPT_DATE_TIME;
                                isAdd = true;
                            }

                        }

                        #endregion
                        #region 梅毒
                        if (string.IsNullOrEmpty(newRow.MEDIDURES) && row.REPORT_ITEM_NAME == "梅毒 ")
                        {
                            newRow.MEDIDURES = row.REPORT_ITEM_NAME == "梅毒 " ? ReplaceSpecialChar(row.RESULT) : string.Empty;
                            isAdd = true;
                        }


                        if (!string.IsNullOrEmpty(newRow.MEDIDURES))
                        {
                            if (string.IsNullOrEmpty(newRow.JCRQMEIDU))
                            {
                                newRow.JCRQMEIDU = row.RESULTS_RPT_DATE_TIME;
                                isAdd = true;
                            }

                        }
                        #endregion
                        #region 结核
                        if (string.IsNullOrEmpty(newRow.JHKT) && row.REPORT_ITEM_NAME == "结核抗体")
                        {
                            newRow.JHKT = row.REPORT_ITEM_NAME == "结核抗体" ? ReplaceSpecialChar(row.RESULT) : string.Empty;
                            isAdd = true;
                        }

                        if (string.IsNullOrEmpty(newRow.JHKTSY) && row.REPORT_ITEM_NAME == "结核菌素试验")
                        {
                            newRow.JHKTSY = row.REPORT_ITEM_NAME == "结核菌素试验" ? ReplaceSpecialChar(row.RESULT) : string.Empty;
                            isAdd = true;
                        }

                        #endregion

                        #region 加入到表中
                        if (isAdd)
                        {
                            //把每项加入到本地表中
                            var rowExtend = dt.NewMED_PATIENT_DATAREPORTRow();
                            rowExtend.ID = Guid.NewGuid().ToString();
                            rowExtend.HEMODIALYSIS_ID = _currentPatientHemoId;
                            //rowExtend.BASEINFO = result.Info;
                            //rowExtend.STATE = "1";//成功
                            rowExtend.TYPE = "0";
                            rowExtend.UPTIME = System.DateTime.Now;
                            rowExtend.UPUSER = HemoApplicationContext.Current.CurrentUser.USER_ID;
                            rowExtend.MAPIP = row.TEST_NO;
                            rowExtend.EXTEND = "LABEXAM";
                            rowExtend.EXTEND1 = "实验室辅助检查";
                            rowExtend.EXTEND2 = row.REPORT_ITEM_CODE;
                            rowExtend.EXTEND3 = _currentPatientId;
                            rowExtend.EXTEND4 = string.Empty;
                            rowExtend.EXTEND5 = "全国数据上报平台";
                            dt.AddMED_PATIENT_DATAREPORTRow(rowExtend);
                        }
                        #endregion
                    }
                }
                labDetail.AddMED_LAB_UPLOADRow(newRow);
            }

            var isSucess = false;
            string returnInfo = string.Empty;
            foreach (DataReportModel.MED_LAB_UPLOADRow item in labDetail.Rows)
            {
                ReportResult result = reporter.ReportJC(new JCEntity(resultInfo)
                {
                    XCGJCRQ = Utility.CDate(item.XCGRQ),
                    BXBJC = item.BXBJC,
                    XHDBJC = item.XHDBJC,
                    HXBYJJC = item.HXBYJJC,
                    XXBJC = item.XXBJC,
                    JCRQXZG = Utility.CDate(item.JCRQXZG),
                    XZG = item.XZG,
                    XL = item.XL,
                    IPTH = item.IPTH,
                    JCRQC = Utility.CDate(item.JCRQC),
                    XTQE = item.XTQE,
                    ZTJHL = item.ZTJHL,
                    ZTBHD = item.ZTBHD,
                    TDB = item.TDB,
                    JCRQN = Utility.CDate(item.JCRQN),
                    TQNSMGDL = item.TQNSMGDL,
                    NIAOSU = item.NIAOSU,
                    THJGMGDL = item.THJGMGDL,
                    JIGAN = item.JIGAN,
                    ZZDBA = item.ZZDBA,
                    XBDBV = item.XBDBV,
                    ASTADD = item.ASTADD,
                    ALTADD = item.ALTADD,
                    ZDHSADD = item.ZDHSADD,
                    GYSZ = item.GYSZ,
                    ZDGC = item.ZDGC,
                    DMDZDB = item.DMDZDB,
                    GMDZDB = item.GMDZDB,
                    XUETANG = item.XUETANG,
                    XUEJIA = item.XUEJIA,
                    XUENA = item.XUENA,
                    XUELV = item.XUELV,
                    ERYANGHUATAN = item.ERYANGHUATAN,
                    JCRQZZZ = Utility.CDate(item.JCRQZZZ),
                    CFYDBJC = item.CFYDBJC,
                    BTEWQDBJC = item.BTEWQDBJC,
                    JCRQE = Utility.CDate(item.JCRQE),
                    HBsAg = item.HBSAG,
                    AntiHBs = item.ANTIHBS,
                    HBeAg = item.HBEAG,
                    AntiHBe = item.ANTIHBE,
                    AntiHBc = item.ANTIHBC,
                    JCRQD = Utility.CDate(item.JCRQD),
                    HBV_NUMBER = item.HBV_NUMBER,
                    JCRQF = Utility.CDate(item.JCRQF),
                    HCVBB = item.HCVBB,
                    JCRQHCV = Utility.CDate(item.JCRQHCV),
                    HCVRNAV_NUMBER = item.HCVRNAV_NUMBER,
                    JCRQAIDS = Utility.CDate(item.JCRQAIDS),
                    AIDRES = item.AIDRES,
                    JCRQMEIDU = Utility.CDate(item.JCRQMEIDU),
                    MEIDURES = item.MEDIDURES,
                    JHKT = item.JHKT,
                    JHKTSY = item.JHKTSY

                });
                if (result.Success)
                {
                    //成功后赋值，以便于后边是否保存数据的标记
                    isSucess = true;
                    returnInfo = result.Info;
                }
            }

            //上传成功的话去保存，不然清空，表示没有上传成功。
            if (isSucess)
            {
                foreach (DataReportModel.MED_PATIENT_DATAREPORTRow rowExtend in dt.Rows)
                {
                    rowExtend.BASEINFO = returnInfo;
                    rowExtend.STATE = "1";//成功

                }
            }
            else
            {
                dt.Rows.Clear();
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
        /// <summary>
        /// 去掉特殊符号，不然会上传失败的
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        private string ReplaceSpecialChar(string value)
        {
            string newValue = string.Empty;
            newValue = value.Replace(@"↓", "").Replace(@"""↑↑""", "").Replace(@"""↑""", "").Replace(@"""↓↓""", "").Replace(@"""-""", "");
            return newValue;
        }

        #endregion
    }
}
