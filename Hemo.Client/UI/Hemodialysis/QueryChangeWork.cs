/*----------------------------------------------------------------
 * Copyright (C) 2005 (苏州)医疗科技发展有限公司
 * 文件功能描述:工作量
 * 创建标识:吕志强-2013年7月9日
 * 
 * 修改时间:2013年10月17日
 * 修改人:贺建操
 * 修改描述:修改方法
 * 
 * 修改时间:2014年1月25日
 * 修改人:顾伟伟
 * 修改描述:新增方法
 * 
 * 修改时间:2014年5月5日
 * 修改人:顾伟伟
 * 修改描述:修改方法SQL
 * ----------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace Hemo.Client.UI.Hemodialysis {
    public partial class QueryChangeWork : DevExpress.XtraEditors.XtraUserControl {
       /// <summary>
       /// 构造函数
       /// </summary>
        public QueryChangeWork() {
            InitializeComponent();
            BindGridView();
        }
        
        /// <summary>
        /// 绑定数据
        /// </summary>
        private void BindGridView() {
            DataTable dataTable = new DataTable();
            DataColumn dtCol1 = new DataColumn("交班", System.Type.GetType("System.String"));
            DataColumn dtCol2 = new DataColumn("班次", System.Type.GetType("System.String"));
            DataColumn dtCol3 = new DataColumn("病室", System.Type.GetType("System.String"));
            DataColumn dtCol3_1 = new DataColumn("床位", System.Type.GetType("System.String"));
            DataColumn dtCol4 = new DataColumn("患者", System.Type.GetType("System.String"));
            DataColumn dtCol5 = new DataColumn("医生", System.Type.GetType("System.String"));
            DataColumn dtCol6 = new DataColumn("内容", System.Type.GetType("System.String"));
            DataColumn dtCol7 = new DataColumn("护理小结", System.Type.GetType("System.String"));

            dataTable.Columns.Add(dtCol1);
            dataTable.Columns.Add(dtCol2);
            dataTable.Columns.Add(dtCol3);
            dataTable.Columns.Add(dtCol3_1);
            dataTable.Columns.Add(dtCol4);
            dataTable.Columns.Add(dtCol5);
            dataTable.Columns.Add(dtCol6);
            dataTable.Columns.Add(dtCol7);
        }

        /// <summary>
        /// 点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void simpleButton1_Click(object sender, EventArgs e) {
            EditChangeWork frm = new EditChangeWork();
            frm.Show();
        }

        private void simpleButton1_Click_1(object sender, EventArgs e) {

        }
    }
}
