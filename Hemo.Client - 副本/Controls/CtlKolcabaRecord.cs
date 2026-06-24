/*----------------------------------------------------------------
// Copyright (C) 2013 苏州XX公司股份有限公司
// 文件名：CtlKolcabaRecord.cs
// 文件功能描述：Kolcaba的舒适状况量表
// 创建标识：刘超 2013-07-22
----------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Hemo.Model;
using Hemo.Client.Core;
using Hemo.Utilities;

namespace Hemo.Client.Controls
{
    public partial class CtlKolcabaRecord :DevExpress.XtraEditors.XtraUserControl
    {
        #region 变量

        private PatientKolcabaModel.MED_PATIENT_KOLCABARow currentRecordRow = null;

        private string currentHemoId = string.Empty;

        public string CurrentHemoId
        {
            get { return currentHemoId; }
            set { currentHemoId = value; }
        }

        public PatientKolcabaModel.MED_PATIENT_KOLCABARow CurrentRecordRow
        {
            get { return currentRecordRow; }
            set { currentRecordRow = value; }
        }

        #endregion

        public CtlKolcabaRecord()
        {
            InitializeComponent();
        }

        #region 方法
        /// <summary>
        /// 全选
        /// </summary>
        /// <returns></returns>
        public bool checkFill()
        {
            if (!ctlCheckBox1.checkIsFill())
            {
                AutoClosedMsgBox.ShowForm("项目1没有填写", "提示", 1000, MessageBoxIcon.Warning);
                return false;
            }
            if (!ctlCheckOppositeBox2.checkIsFill())
            {
                AutoClosedMsgBox.ShowForm("项目2没有填写", "提示", 1000, MessageBoxIcon.Warning);
                return false;
            }
            if (!ctlCheckOppositeBox3.checkIsFill())
            {
                AutoClosedMsgBox.ShowForm("项目3没有填写", "提示", 1000, MessageBoxIcon.Warning);
                return false;
            }
            if (!ctlCheckBox4.checkIsFill())
            {
                AutoClosedMsgBox.ShowForm("项目4没有填写", "提示", 1000, MessageBoxIcon.Warning);
                return false;
            }
            if (!ctlCheckBox5.checkIsFill())
            {
                AutoClosedMsgBox.ShowForm("项目5没有填写", "提示", 1000, MessageBoxIcon.Warning);
                return false;
            }
            if (!ctlCheckBox6.checkIsFill())
            {
                AutoClosedMsgBox.ShowForm("项目6没有填写", "提示", 1000, MessageBoxIcon.Warning);
                return false;
            }
            if (!ctlCheckOppositeBox7.checkIsFill())
            {
                AutoClosedMsgBox.ShowForm("项目7没有填写", "提示", 1000, MessageBoxIcon.Warning);
                return false;
            }
            if (!ctlCheckOppositeBox8.checkIsFill())
            {
                AutoClosedMsgBox.ShowForm("项目8没有填写", "提示", 1000, MessageBoxIcon.Warning);
                return false;
            }
            if (!ctlCheckOppositeBox9.checkIsFill())
            {
                AutoClosedMsgBox.ShowForm("项目9没有填写", "提示", 1000, MessageBoxIcon.Warning);
                return false;
            }
            if (!ctlCheckOppositeBox10.checkIsFill())
            {
                AutoClosedMsgBox.ShowForm("项目10没有填写", "提示", 1000, MessageBoxIcon.Warning);
                return false;
            }
            if (!ctlCheckOppositeBox11.checkIsFill())
            {
                AutoClosedMsgBox.ShowForm("项目11没有填写", "提示", 1000, MessageBoxIcon.Warning);
                return false;
            }
            if (!ctlCheckOppositeBox12.checkIsFill())
            {
                AutoClosedMsgBox.ShowForm("项目12没有填写", "提示", 1000, MessageBoxIcon.Warning);
                return false;
            }
            if (!ctlCheckOppositeBox13.checkIsFill())
            {
                AutoClosedMsgBox.ShowForm("项目13没有填写", "提示", 1000, MessageBoxIcon.Warning);
                return false;
            }
            if (!ctlCheckOppositeBox14.checkIsFill())
            {
                AutoClosedMsgBox.ShowForm("项目14没有填写", "提示", 1000, MessageBoxIcon.Warning);
                return false;
            }
            if (!ctlCheckOppositeBox15.checkIsFill())
            {
                AutoClosedMsgBox.ShowForm("项目15没有填写", "提示", 1000, MessageBoxIcon.Warning);
                return false;
            }
            if (!ctlCheckOppositeBox16.checkIsFill())
            {
                AutoClosedMsgBox.ShowForm("项目16没有填写", "提示", 1000, MessageBoxIcon.Warning);
                return false;
            }
            if (!ctlCheckBox17.checkIsFill())
            {
                AutoClosedMsgBox.ShowForm("项目17没有填写", "提示", 1000, MessageBoxIcon.Warning);
                return false;
            }
            if (!ctlCheckOppositeBox18.checkIsFill())
            {
                AutoClosedMsgBox.ShowForm("项目18没有填写", "提示", 1000, MessageBoxIcon.Warning);
                return false;
            }
            if (!ctlCheckBox19.checkIsFill())
            {
                AutoClosedMsgBox.ShowForm("项目19没有填写", "提示", 1000, MessageBoxIcon.Warning);
                return false;
            }
            if (!ctlCheckOppositeBox20.checkIsFill())
            {
                AutoClosedMsgBox.ShowForm("项目20没有填写", "提示", 1000, MessageBoxIcon.Warning);
                return false;
            }
            if (!ctlCheckOppositeBox21.checkIsFill())
            {
                AutoClosedMsgBox.ShowForm("项目21没有填写", "提示", 1000, MessageBoxIcon.Warning);
                return false;
            }
            if (!ctlCheckBox22.checkIsFill())
            {
                AutoClosedMsgBox.ShowForm("项目22没有填写", "提示", 1000, MessageBoxIcon.Warning);
                return false;
            }
            if (!ctlCheckBox23.checkIsFill())
            {
                AutoClosedMsgBox.ShowForm("项目23没有填写", "提示", 1000, MessageBoxIcon.Warning);
                return false;
            }
            if (!ctlCheckOppositeBox24.checkIsFill())
            {
                AutoClosedMsgBox.ShowForm("项目24没有填写", "提示", 1000, MessageBoxIcon.Warning);
                return false;
            }
            if (!ctlCheckOppositeBox25.checkIsFill())
            {
                AutoClosedMsgBox.ShowForm("项目25没有填写", "提示", 1000, MessageBoxIcon.Warning);
                return false;
            }
            if (!ctlCheckBox26.checkIsFill())
            {
                AutoClosedMsgBox.ShowForm("项目26没有填写", "提示", 1000, MessageBoxIcon.Warning);
                return false;
            }
            if (!ctlCheckOppositeBox27.checkIsFill())
            {
                AutoClosedMsgBox.ShowForm("项目27没有填写", "提示", 1000, MessageBoxIcon.Warning);
                return false;
            }
            if (!ctlCheckBox28.checkIsFill())
            {
                AutoClosedMsgBox.ShowForm("项目28没有填写", "提示", 1000, MessageBoxIcon.Warning);
                return false;
            }
            return true;
        }
        /// <summary>
        /// 加载 
        /// </summary>
        public void LoadMedicalRecord()
        {
            if (currentRecordRow != null)
            {
                if (currentRecordRow["ITEM1"].ToString() == "1")
                {
                    ctlCheckBox1.CheckId = "1";
                }
                else if (currentRecordRow["ITEM1"].ToString() == "2")
                {
                    ctlCheckBox1.CheckId = "2";
                }
                else if (currentRecordRow["ITEM1"].ToString() == "3")
                {
                    ctlCheckBox1.CheckId = "3";
                }
                else if (currentRecordRow["ITEM1"].ToString() == "4")
                {
                    ctlCheckBox1.CheckId = "4";
                }
                ctlCheckBox1.checkChoose();
                if (currentRecordRow["ITEM2"].ToString() == "1")
                {
                    ctlCheckOppositeBox2.CheckId = "4";
                }
                else if (currentRecordRow["ITEM2"].ToString() == "2")
                {
                    ctlCheckOppositeBox2.CheckId = "3";
                }
                else if (currentRecordRow["ITEM2"].ToString() == "3")
                {
                    ctlCheckOppositeBox2.CheckId = "2";
                }
                else if (currentRecordRow["ITEM2"].ToString() == "4")
                {
                    ctlCheckOppositeBox2.CheckId = "1";
                }
                ctlCheckOppositeBox2.checkChoose();
                if (currentRecordRow["ITEM3"].ToString() == "1")
                {
                    ctlCheckOppositeBox3.CheckId = "4";
                }
                else if (currentRecordRow["ITEM3"].ToString() == "2")
                {
                    ctlCheckOppositeBox3.CheckId = "3";
                }
                else if (currentRecordRow["ITEM3"].ToString() == "3")
                {
                    ctlCheckOppositeBox3.CheckId = "2";
                }
                else if (currentRecordRow["ITEM3"].ToString() == "4")
                {
                    ctlCheckOppositeBox3.CheckId = "1";
                }
                ctlCheckOppositeBox3.checkChoose();
                if (currentRecordRow["ITEM4"].ToString() == "1")
                {
                    ctlCheckBox4.CheckId = "1";
                }
                else if (currentRecordRow["ITEM4"].ToString() == "2")
                {
                    ctlCheckBox4.CheckId = "2";
                }
                else if (currentRecordRow["ITEM4"].ToString() == "3")
                {
                    ctlCheckBox4.CheckId = "3";
                }
                else if (currentRecordRow["ITEM4"].ToString() == "4")
                {
                    ctlCheckBox4.CheckId = "4";
                }
                ctlCheckBox4.checkChoose();
                if (currentRecordRow["ITEM5"].ToString() == "1")
                {
                    ctlCheckBox5.CheckId = "1";
                }
                else if (currentRecordRow["ITEM5"].ToString() == "2")
                {
                    ctlCheckBox5.CheckId = "2";
                }
                else if (currentRecordRow["ITEM5"].ToString() == "3")
                {
                    ctlCheckBox5.CheckId = "3";
                }
                else if (currentRecordRow["ITEM5"].ToString() == "4")
                {
                    ctlCheckBox5.CheckId = "4";
                }
                ctlCheckBox5.checkChoose();
                if (currentRecordRow["ITEM6"].ToString() == "1")
                {
                    ctlCheckBox6.CheckId = "1";
                }
                else if (currentRecordRow["ITEM6"].ToString() == "2")
                {
                    ctlCheckBox6.CheckId = "2";
                }
                else if (currentRecordRow["ITEM6"].ToString() == "3")
                {
                    ctlCheckBox6.CheckId = "3";
                }
                else if (currentRecordRow["ITEM6"].ToString() == "4")
                {
                    ctlCheckBox6.CheckId = "4";
                }
                ctlCheckBox6.checkChoose();
                if (currentRecordRow["ITEM7"].ToString() == "1")
                {
                    ctlCheckOppositeBox7.CheckId = "4";
                }
                else if (currentRecordRow["ITEM7"].ToString() == "2")
                {
                    ctlCheckOppositeBox7.CheckId = "3";
                }
                else if (currentRecordRow["ITEM7"].ToString() == "3")
                {
                    ctlCheckOppositeBox7.CheckId = "2";
                }
                else if (currentRecordRow["ITEM7"].ToString() == "4")
                {
                    ctlCheckOppositeBox7.CheckId = "1";
                }
                ctlCheckOppositeBox7.checkChoose();
                if (currentRecordRow["ITEM8"].ToString() == "1")
                {
                    ctlCheckOppositeBox8.CheckId = "4";
                }
                else if (currentRecordRow["ITEM8"].ToString() == "2")
                {
                    ctlCheckOppositeBox8.CheckId = "3";
                }
                else if (currentRecordRow["ITEM8"].ToString() == "3")
                {
                    ctlCheckOppositeBox8.CheckId = "2";
                }
                else if (currentRecordRow["ITEM8"].ToString() == "4")
                {
                    ctlCheckOppositeBox8.CheckId = "1";
                }
                ctlCheckOppositeBox8.checkChoose();
                if (currentRecordRow["ITEM9"].ToString() == "1")
                {
                    ctlCheckOppositeBox9.CheckId = "4";
                }
                else if (currentRecordRow["ITEM9"].ToString() == "2")
                {
                    ctlCheckOppositeBox9.CheckId = "3";
                }
                else if (currentRecordRow["ITEM9"].ToString() == "3")
                {
                    ctlCheckOppositeBox9.CheckId = "2";
                }
                else if (currentRecordRow["ITEM9"].ToString() == "4")
                {
                    ctlCheckOppositeBox9.CheckId = "1";
                }
                ctlCheckOppositeBox9.checkChoose();
                if (currentRecordRow["ITEM10"].ToString() == "1")
                {
                    ctlCheckOppositeBox10.CheckId = "4";
                }
                else if (currentRecordRow["ITEM10"].ToString() == "2")
                {
                    ctlCheckOppositeBox10.CheckId = "3";
                }
                else if (currentRecordRow["ITEM10"].ToString() == "3")
                {
                    ctlCheckOppositeBox10.CheckId = "2";
                }
                else if (currentRecordRow["ITEM10"].ToString() == "4")
                {
                    ctlCheckOppositeBox10.CheckId = "1";
                }
                ctlCheckOppositeBox10.checkChoose();
                if (currentRecordRow["ITEM11"].ToString() == "1")
                {
                    ctlCheckOppositeBox11.CheckId = "4";
                }
                else if (currentRecordRow["ITEM11"].ToString() == "2")
                {
                    ctlCheckOppositeBox11.CheckId = "3";
                }
                else if (currentRecordRow["ITEM11"].ToString() == "3")
                {
                    ctlCheckOppositeBox11.CheckId = "2";
                }
                else if (currentRecordRow["ITEM11"].ToString() == "4")
                {
                    ctlCheckOppositeBox11.CheckId = "1";
                }
                ctlCheckOppositeBox11.checkChoose();
                if (currentRecordRow["ITEM12"].ToString() == "1")
                {
                    ctlCheckOppositeBox12.CheckId = "4";
                }
                else if (currentRecordRow["ITEM12"].ToString() == "2")
                {
                    ctlCheckOppositeBox12.CheckId = "3";
                }
                else if (currentRecordRow["ITEM12"].ToString() == "3")
                {
                    ctlCheckOppositeBox12.CheckId = "2";
                }
                else if (currentRecordRow["ITEM12"].ToString() == "4")
                {
                    ctlCheckOppositeBox12.CheckId = "1";
                }
                ctlCheckOppositeBox12.checkChoose();
                if (currentRecordRow["ITEM13"].ToString() == "1")
                {
                    ctlCheckOppositeBox13.CheckId = "4";
                }
                else if (currentRecordRow["ITEM13"].ToString() == "2")
                {
                    ctlCheckOppositeBox13.CheckId = "3";
                }
                else if (currentRecordRow["ITEM13"].ToString() == "3")
                {
                    ctlCheckOppositeBox13.CheckId = "2";
                }
                else if (currentRecordRow["ITEM13"].ToString() == "4")
                {
                    ctlCheckOppositeBox13.CheckId = "1";
                }
                ctlCheckOppositeBox13.checkChoose();
                if (currentRecordRow["ITEM14"].ToString() == "1")
                {
                    ctlCheckOppositeBox14.CheckId = "4";
                }
                else if (currentRecordRow["ITEM14"].ToString() == "2")
                {
                    ctlCheckOppositeBox14.CheckId = "3";
                }
                else if (currentRecordRow["ITEM14"].ToString() == "3")
                {
                    ctlCheckOppositeBox14.CheckId = "2";
                }
                else if (currentRecordRow["ITEM14"].ToString() == "4")
                {
                    ctlCheckOppositeBox14.CheckId = "1";
                }
                ctlCheckOppositeBox14.checkChoose();
                if (currentRecordRow["ITEM15"].ToString() == "1")
                {
                    ctlCheckOppositeBox15.CheckId = "4";
                }
                else if (currentRecordRow["ITEM15"].ToString() == "2")
                {
                    ctlCheckOppositeBox15.CheckId = "3";
                }
                else if (currentRecordRow["ITEM15"].ToString() == "3")
                {
                    ctlCheckOppositeBox15.CheckId = "2";
                }
                else if (currentRecordRow["ITEM15"].ToString() == "4")
                {
                    ctlCheckOppositeBox15.CheckId = "1";
                }
                ctlCheckOppositeBox15.checkChoose();
                if (currentRecordRow["ITEM16"].ToString() == "1")
                {
                    ctlCheckOppositeBox16.CheckId = "4";
                }
                else if (currentRecordRow["ITEM16"].ToString() == "2")
                {
                    ctlCheckOppositeBox16.CheckId = "3";
                }
                else if (currentRecordRow["ITEM16"].ToString() == "3")
                {
                    ctlCheckOppositeBox16.CheckId = "2";
                }
                else if (currentRecordRow["ITEM16"].ToString() == "4")
                {
                    ctlCheckOppositeBox16.CheckId = "1";
                }
                ctlCheckOppositeBox16.checkChoose();
                if (currentRecordRow["ITEM17"].ToString() == "1")
                {
                    ctlCheckBox17.CheckId = "1";
                }
                else if (currentRecordRow["ITEM17"].ToString() == "2")
                {
                    ctlCheckBox17.CheckId = "2";
                }
                else if (currentRecordRow["ITEM17"].ToString() == "3")
                {
                    ctlCheckBox17.CheckId = "3";
                }
                else if (currentRecordRow["ITEM17"].ToString() == "4")
                {
                    ctlCheckBox17.CheckId = "4";
                }
                ctlCheckBox17.checkChoose();
                if (currentRecordRow["ITEM18"].ToString() == "1")
                {
                    ctlCheckOppositeBox18.CheckId = "4";
                }
                else if (currentRecordRow["ITEM18"].ToString() == "2")
                {
                    ctlCheckOppositeBox18.CheckId = "3";
                }
                else if (currentRecordRow["ITEM18"].ToString() == "3")
                {
                    ctlCheckOppositeBox18.CheckId = "2";
                }
                else if (currentRecordRow["ITEM18"].ToString() == "4")
                {
                    ctlCheckOppositeBox18.CheckId = "1";
                }
                ctlCheckOppositeBox18.checkChoose();
                if (currentRecordRow["ITEM19"].ToString() == "1")
                {
                    ctlCheckBox19.CheckId = "1";
                }
                else if (currentRecordRow["ITEM19"].ToString() == "2")
                {
                    ctlCheckBox19.CheckId = "2";
                }
                else if (currentRecordRow["ITEM19"].ToString() == "3")
                {
                    ctlCheckBox19.CheckId = "3";
                }
                else if (currentRecordRow["ITEM19"].ToString() == "4")
                {
                    ctlCheckBox19.CheckId = "4";
                }
                ctlCheckBox19.checkChoose();
                if (currentRecordRow["ITEM20"].ToString() == "1")
                {
                    ctlCheckOppositeBox20.CheckId = "4";
                }
                else if (currentRecordRow["ITEM20"].ToString() == "2")
                {
                    ctlCheckOppositeBox20.CheckId = "3";
                }
                else if (currentRecordRow["ITEM20"].ToString() == "3")
                {
                    ctlCheckOppositeBox20.CheckId = "2";
                }
                else if (currentRecordRow["ITEM20"].ToString() == "4")
                {
                    ctlCheckOppositeBox20.CheckId = "1";
                }
                ctlCheckOppositeBox20.checkChoose();
                if (currentRecordRow["ITEM21"].ToString() == "1")
                {
                    ctlCheckOppositeBox21.CheckId = "4";
                }
                else if (currentRecordRow["ITEM21"].ToString() == "2")
                {
                    ctlCheckOppositeBox21.CheckId = "3";
                }
                else if (currentRecordRow["ITEM21"].ToString() == "3")
                {
                    ctlCheckOppositeBox21.CheckId = "2";
                }
                else if (currentRecordRow["ITEM21"].ToString() == "4")
                {
                    ctlCheckOppositeBox21.CheckId = "1";
                }
                ctlCheckOppositeBox21.checkChoose();
                if (currentRecordRow["ITEM22"].ToString() == "1")
                {
                    ctlCheckBox22.CheckId = "1";
                }
                else if (currentRecordRow["ITEM22"].ToString() == "2")
                {
                    ctlCheckBox22.CheckId = "2";
                }
                else if (currentRecordRow["ITEM22"].ToString() == "3")
                {
                    ctlCheckBox22.CheckId = "3";
                }
                else if (currentRecordRow["ITEM22"].ToString() == "4")
                {
                    ctlCheckBox22.CheckId = "4";
                }
                ctlCheckBox22.checkChoose();
                if (currentRecordRow["ITEM23"].ToString() == "1")
                {
                    ctlCheckBox23.CheckId = "1";
                }
                else if (currentRecordRow["ITEM23"].ToString() == "2")
                {
                    ctlCheckBox23.CheckId = "2";
                }
                else if (currentRecordRow["ITEM23"].ToString() == "3")
                {
                    ctlCheckBox23.CheckId = "3";
                }
                else if (currentRecordRow["ITEM23"].ToString() == "4")
                {
                    ctlCheckBox23.CheckId = "4";
                }
                ctlCheckBox23.checkChoose();
                if (currentRecordRow["ITEM24"].ToString() == "1")
                {
                    ctlCheckOppositeBox24.CheckId = "4";
                }
                else if (currentRecordRow["ITEM24"].ToString() == "2")
                {
                    ctlCheckOppositeBox24.CheckId = "3";
                }
                else if (currentRecordRow["ITEM24"].ToString() == "3")
                {
                    ctlCheckOppositeBox24.CheckId = "2";
                }
                else if (currentRecordRow["ITEM24"].ToString() == "4")
                {
                    ctlCheckOppositeBox24.CheckId = "1";
                }
                ctlCheckOppositeBox24.checkChoose();
                if (currentRecordRow["ITEM25"].ToString() == "1")
                {
                    ctlCheckOppositeBox25.CheckId = "4";
                }
                else if (currentRecordRow["ITEM25"].ToString() == "2")
                {
                    ctlCheckOppositeBox25.CheckId = "3";
                }
                else if (currentRecordRow["ITEM25"].ToString() == "3")
                {
                    ctlCheckOppositeBox25.CheckId = "2";
                }
                else if (currentRecordRow["ITEM25"].ToString() == "4")
                {
                    ctlCheckOppositeBox25.CheckId = "1";
                }
                ctlCheckOppositeBox25.checkChoose();
                if (currentRecordRow["ITEM26"].ToString() == "1")
                {
                    ctlCheckBox26.CheckId = "1";
                }
                else if (currentRecordRow["ITEM26"].ToString() == "2")
                {
                    ctlCheckBox26.CheckId = "2";
                }
                else if (currentRecordRow["ITEM26"].ToString() == "3")
                {
                    ctlCheckBox26.CheckId = "3";
                }
                else if (currentRecordRow["ITEM26"].ToString() == "4")
                {
                    ctlCheckBox26.CheckId = "4";
                }
                ctlCheckBox26.checkChoose();
                if (currentRecordRow["ITEM27"].ToString() == "1")
                {
                    ctlCheckOppositeBox27.CheckId = "4";
                }
                else if (currentRecordRow["ITEM27"].ToString() == "2")
                {
                    ctlCheckOppositeBox27.CheckId = "3";
                }
                else if (currentRecordRow["ITEM27"].ToString() == "3")
                {
                    ctlCheckOppositeBox27.CheckId = "2";
                }
                else if (currentRecordRow["ITEM27"].ToString() == "4")
                {
                    ctlCheckOppositeBox27.CheckId = "1";
                }
                ctlCheckOppositeBox27.checkChoose();
                if (currentRecordRow["ITEM28"].ToString() == "1")
                {
                    ctlCheckBox28.CheckId = "1";
                }
                else if (currentRecordRow["ITEM28"].ToString() == "2")
                {
                    ctlCheckBox28.CheckId = "2";
                }
                else if (currentRecordRow["ITEM28"].ToString() == "3")
                {
                    ctlCheckBox28.CheckId = "3";
                }
                else if (currentRecordRow["ITEM28"].ToString() == "4")
                {
                    ctlCheckBox28.CheckId = "4";
                }
                ctlCheckBox28.checkChoose();
            }
        }
        /// <summary>
        /// 获取
        /// </summary>
        /// <param name="dtRecord"></param>
        /// <returns></returns>
        public PatientKolcabaModel.MED_PATIENT_KOLCABADataTable GetPatientKolcabaDataTable(PatientKolcabaModel.MED_PATIENT_KOLCABADataTable dtRecord)
        {
            if (dtRecord == null)
            {
                //新增
                dtRecord = new PatientKolcabaModel.MED_PATIENT_KOLCABADataTable();
                var row = dtRecord.NewMED_PATIENT_KOLCABARow();
                row.ID = Guid.NewGuid().ToString().Trim();
                row.HEMODIALYSIS_ID = currentHemoId;
                //row.CREATEBY = HemoApplicationContext.Current.CurrentUser.USER_ID;
                row.CREATEDATE = DateTime.Now.Date;
                row.LASTUPDATEBY = HemoApplicationContext.Current.CurrentUser.USER_ID;
                row.LASTUPDATEDATE = System.DateTime.Now;
                row.ITEM1 = Utility.CDecimal(this.ctlCheckBox1.CheckId);
                row.ITEM2 = Utility.CDecimal(this.ctlCheckOppositeBox2.CheckId);
                row.ITEM3 = Utility.CDecimal(this.ctlCheckOppositeBox3.CheckId);
                row.ITEM4 = Utility.CDecimal(this.ctlCheckBox4.CheckId);
                row.ITEM5 = Utility.CDecimal(this.ctlCheckBox5.CheckId);
                row.ITEM6 = Utility.CDecimal(this.ctlCheckBox6.CheckId);
                row.ITEM7 = Utility.CDecimal(this.ctlCheckOppositeBox7.CheckId);
                row.ITEM8 = Utility.CDecimal(this.ctlCheckOppositeBox8.CheckId);
                row.ITEM9 = Utility.CDecimal(this.ctlCheckOppositeBox9.CheckId);
                row.ITEM10 = Utility.CDecimal(this.ctlCheckOppositeBox10.CheckId);
                row.ITEM11 = Utility.CDecimal(this.ctlCheckOppositeBox11.CheckId);
                row.ITEM12 = Utility.CDecimal(this.ctlCheckOppositeBox12.CheckId);
                row.ITEM13 = Utility.CDecimal(this.ctlCheckOppositeBox13.CheckId);
                row.ITEM14 = Utility.CDecimal(this.ctlCheckOppositeBox14.CheckId);
                row.ITEM15 = Utility.CDecimal(this.ctlCheckOppositeBox15.CheckId);
                row.ITEM16 = Utility.CDecimal(this.ctlCheckOppositeBox16.CheckId);
                row.ITEM17 = Utility.CDecimal(this.ctlCheckBox17.CheckId);
                row.ITEM18 = Utility.CDecimal(this.ctlCheckOppositeBox18.CheckId);
                row.ITEM19 = Utility.CDecimal(this.ctlCheckBox19.CheckId);
                row.ITEM20 = Utility.CDecimal(this.ctlCheckOppositeBox20.CheckId);
                row.ITEM21 = Utility.CDecimal(this.ctlCheckOppositeBox21.CheckId);
                row.ITEM22 = Utility.CDecimal(this.ctlCheckBox22.CheckId);
                row.ITEM23 = Utility.CDecimal(this.ctlCheckBox23.CheckId);
                row.ITEM24 = Utility.CDecimal(this.ctlCheckOppositeBox24.CheckId);
                row.ITEM25 = Utility.CDecimal(this.ctlCheckOppositeBox25.CheckId);
                row.ITEM26 = Utility.CDecimal(this.ctlCheckBox26.CheckId);
                row.ITEM27 = Utility.CDecimal(this.ctlCheckOppositeBox27.CheckId);
                row.ITEM28 = Utility.CDecimal(this.ctlCheckBox28.CheckId);
                row.TOTALSCORE = row.ITEM1 + row.ITEM2 + row.ITEM3 + row.ITEM4 + row.ITEM5 + row.ITEM6 + row.ITEM7 + row.ITEM8 + row.ITEM9 + row.ITEM10 + row.ITEM11 + row.ITEM12 + row.ITEM13 + row.ITEM14 
                    + row.ITEM15 + row.ITEM16 + row.ITEM17 + row.ITEM18 + row.ITEM19 + row.ITEM20 + row.ITEM21 + row.ITEM22 + row.ITEM23 + row.ITEM24 + row.ITEM25 + row.ITEM26 + row.ITEM27 + row.ITEM28;
                dtRecord.AddMED_PATIENT_KOLCABARow(row);
            }
            else
            {
                //编辑
                dtRecord[0].LASTUPDATEBY = HemoApplicationContext.Current.CurrentUser.USER_ID;
                dtRecord[0].LASTUPDATEDATE = System.DateTime.Now;
                dtRecord[0].ITEM1 = Utility.CDecimal(this.ctlCheckBox1.CheckId);
                dtRecord[0].ITEM2 = Utility.CDecimal(this.ctlCheckOppositeBox2.CheckId);
                dtRecord[0].ITEM3 = Utility.CDecimal(this.ctlCheckOppositeBox3.CheckId);
                dtRecord[0].ITEM4 = Utility.CDecimal(this.ctlCheckBox4.CheckId);
                dtRecord[0].ITEM5 = Utility.CDecimal(this.ctlCheckBox5.CheckId);
                dtRecord[0].ITEM6 = Utility.CDecimal(this.ctlCheckBox6.CheckId);
                dtRecord[0].ITEM7 = Utility.CDecimal(this.ctlCheckOppositeBox7.CheckId);
                dtRecord[0].ITEM8 = Utility.CDecimal(this.ctlCheckOppositeBox8.CheckId);
                dtRecord[0].ITEM9 = Utility.CDecimal(this.ctlCheckOppositeBox9.CheckId);
                dtRecord[0].ITEM10 = Utility.CDecimal(this.ctlCheckOppositeBox10.CheckId);
                dtRecord[0].ITEM11 = Utility.CDecimal(this.ctlCheckOppositeBox11.CheckId);
                dtRecord[0].ITEM12 = Utility.CDecimal(this.ctlCheckOppositeBox12.CheckId);
                dtRecord[0].ITEM13 = Utility.CDecimal(this.ctlCheckOppositeBox13.CheckId);
                dtRecord[0].ITEM14 = Utility.CDecimal(this.ctlCheckOppositeBox14.CheckId);
                dtRecord[0].ITEM15 = Utility.CDecimal(this.ctlCheckOppositeBox15.CheckId);
                dtRecord[0].ITEM16 = Utility.CDecimal(this.ctlCheckOppositeBox16.CheckId);
                dtRecord[0].ITEM17 = Utility.CDecimal(this.ctlCheckBox17.CheckId);
                dtRecord[0].ITEM18 = Utility.CDecimal(this.ctlCheckOppositeBox18.CheckId);
                dtRecord[0].ITEM19 = Utility.CDecimal(this.ctlCheckBox19.CheckId);
                dtRecord[0].ITEM20 = Utility.CDecimal(this.ctlCheckOppositeBox20.CheckId);
                dtRecord[0].ITEM21 = Utility.CDecimal(this.ctlCheckOppositeBox21.CheckId);
                dtRecord[0].ITEM22 = Utility.CDecimal(this.ctlCheckBox22.CheckId);
                dtRecord[0].ITEM23 = Utility.CDecimal(this.ctlCheckBox23.CheckId);
                dtRecord[0].ITEM24 = Utility.CDecimal(this.ctlCheckOppositeBox24.CheckId);
                dtRecord[0].ITEM25 = Utility.CDecimal(this.ctlCheckOppositeBox25.CheckId);
                dtRecord[0].ITEM26 = Utility.CDecimal(this.ctlCheckBox26.CheckId);
                dtRecord[0].ITEM27 = Utility.CDecimal(this.ctlCheckOppositeBox27.CheckId);
                dtRecord[0].ITEM28 = Utility.CDecimal(this.ctlCheckBox28.CheckId);
                dtRecord[0].TOTALSCORE = dtRecord[0].ITEM1 + dtRecord[0].ITEM2 + dtRecord[0].ITEM3 + dtRecord[0].ITEM4 + dtRecord[0].ITEM5 + dtRecord[0].ITEM6 + dtRecord[0].ITEM7 + dtRecord[0].ITEM8 + dtRecord[0].ITEM9 + dtRecord[0].ITEM10 + dtRecord[0].ITEM11 + dtRecord[0].ITEM12 + dtRecord[0].ITEM13 
                            + dtRecord[0].ITEM14 + dtRecord[0].ITEM15 + dtRecord[0].ITEM16 + dtRecord[0].ITEM17 + dtRecord[0].ITEM18 + dtRecord[0].ITEM19 + dtRecord[0].ITEM20 + dtRecord[0].ITEM21 + dtRecord[0].ITEM22 + dtRecord[0].ITEM23 + dtRecord[0].ITEM24 + dtRecord[0].ITEM25 + dtRecord[0].ITEM26 + dtRecord[0].ITEM27 + dtRecord[0].ITEM28;
            }
            return dtRecord;
        }

        #endregion
    }
}
