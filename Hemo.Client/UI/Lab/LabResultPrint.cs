/*----------------------------------------------------------------
// Copyright (C) 2005 苏州XX公司有限公司
// 描述：检验单打印窗体类
// 创建时间：2014-03-22
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
using Hemo.Client.Controls;
using Hemo;
using System.Linq;
using Hemo.Model;
using Hemo.Utilities;
namespace Hemo.Client.UI.Lab {
    public partial class LabResultPrint : DevExpress.XtraEditors.XtraForm
    {
        #region 类变量

        private DataTable _dtPatientLab;

        private CtlMedicalDocumentContainer _medicalDocContainer = new CtlMedicalDocumentContainer();

        private PatientModel.MED_PATIENTSRow _patientRow;

        #endregion

        #region 属性

        public DataTable dtPatientLab
        {
            get { return _dtPatientLab; }
            set { _dtPatientLab = value; }
        }

        public PatientModel.MED_PATIENTSRow PatientRow
        {
            get { return _patientRow; }
            set { _patientRow = value; }
        }

        #endregion

        #region 构造函数

        public LabResultPrint()
        {
            InitializeComponent();
            //_medicalDocContainer.HaveNextPage = true;
        }

        #endregion

        #region 事件

        private void LabResultPrint_Load(object sender, EventArgs e)
        {
            if (_dtPatientLab.Rows.Count == 0)
            {
                MessageBox.Show("没有可打印的数据");
                return;
            }
            _medicalDocContainer = new CtlMedicalDocumentContainer();
            var query = from t in _dtPatientLab.AsEnumerable()
                        group t by new { t1 = t.Field<string>("TEST_NO") } into m
                        select new
                        {
                            TEST_NO = m.Key.t1
                        };
            var itemCount = query.ToList().Count();

            foreach (var item in query.ToList())
            {
                var itemRows = _dtPatientLab.AsEnumerable().Where(i => i["TEST_NO"].ToString() == item.TEST_NO).Count();
                if (itemRows > 20)
                {
                    var rw = itemRows / 20;
                    if (itemRows % 20 != 0)
                    {
                        rw = rw + 1;
                    }
                    itemCount = itemCount + rw;
                }
            }

            List<DataSet> dsList = new List<DataSet>();
            foreach (var item in query.ToList())
            {
                DataSet ds = new DataSet();
                var itemRows = _dtPatientLab.AsEnumerable().Where(i => i["TEST_NO"].ToString() == item.TEST_NO);
                DataTable dt = _dtPatientLab.Clone();

                if (itemRows.ToList().Count <= 10)
                {
                    foreach (var item1 in itemRows)
                    {
                        dt.ImportRow(item1);
                    }
                    ds.Tables.Add(dt);
                    dsList.Add(ds);
                }
                else
                {
                    var tableCount = itemRows.ToList().Count / 20;
                    int k = 0;
                    for (int y = 0; y < tableCount; y++)
                    {
                        for (int x = 0; x < y * 2 + 2; x++)
                        {
                            for (int j = k; j < x * 10 + 10; j++)
                            {
                                dt.ImportRow(itemRows.ToList()[j]);
                            }
                            //if (ds.Tables.IndexOf(dt) == -1)
                            //{
                            k = k + 10;
                            ds.Tables.Add(dt);
                            dsList.Add(ds);
                            dt = new DataTable();
                            dt = _dtPatientLab.Clone();
                            //}
                        }
                    }
                    if (itemRows.ToList().Count % 20 != 0)
                    {
                        for (int j = tableCount * 20; j < itemRows.ToList().Count; j++)
                        {
                            dt.ImportRow(itemRows.ToList()[j]);
                        }
                        //if (ds.Tables.IndexOf(dt) == -1)
                        //{
                        ds.Tables.Add(dt);
                        dsList.Add(ds);
                        dt = new DataTable();
                        dt = _dtPatientLab.Clone();

                        //}
                    }
                }
            }
            var pageCount = itemCount / 2;
            if (itemCount % 2 != 0)
            {
                pageCount = pageCount + 1;
            }

            wpfLabResult firstWpf = new wpfLabResult(dsList[0], dsList[1], _patientRow);

            WPF_DocumentBase wpf = firstWpf;
            if (pageCount > 1)
            {
                wpf.NextPage = new wpfLabResult(dsList[2], dsList[3], _patientRow);

                WPF_DocumentBase NewPage = null;
                for (int i = 2; i < pageCount; i++)
                {
                    NewPage = new wpfLabResult(dsList[i + 2], dsList[i + 3], _patientRow);

                    NewPage.NextPage = wpf;
                    wpf = NewPage;
                }
            }
            _medicalDocContainer.HaveNextPage = true;
            _medicalDocContainer.CurrentMedicalDocument = wpf;
            this.documentContainerHost.Child = _medicalDocContainer;
            //documentContainer1.NextPage = NewPage;
        }

        #endregion
    }
}