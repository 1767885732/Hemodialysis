/*----------------------------------------------------------------
// Copyright (C) 2013 苏州XX公司股份有限公司
// 文件名：ctlAuoSearchCondition.cs
// 文件功能描述：自定意查询条件控件 
// 创建标识：刘超 2013-07-22
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
using Hemo.Utilities;

namespace Hemo.Client.Controls
{
    public partial class ctlAuoSearchCondition : DevExpress.XtraEditors.XtraUserControl
    {
        #region 变量

        public EventHandler InitCondition = null;

        public bool isChoice
        {
            get;
            set;
        }

        public string DataColumnName
        {
            get;
            set;
        }
        public DataTable LupCondition
        {
            set;
            get;
        }


        #endregion

        #region 构造函数
        public ctlAuoSearchCondition()
        {
            InitializeComponent();
        }


        #endregion
        #region 事件
        private void ctlAuoSearchCondition_Load(object sender, EventArgs e)
        {
            if (isChoice)
            {
                this.layoutCondition.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                this.layoutRange.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                this.layoutChoice.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                this.layoutChoice.Text = DataColumnName;
                DataTable dt = new DataTable();
                dt.Columns.Add("ITEM_NAME", System.Type.GetType("System.String"));
                dt.Columns.Add("ITEM_VALUE", System.Type.GetType("System.String"));
                DataRow dr = null;
                dr = dt.NewRow();
                dr["ITEM_NAME"] = " ";
                dr["ITEM_VALUE"] = " ";
                dt.Rows.Add(dr);
                dr = dt.NewRow();
                dr["ITEM_NAME"] = "阴性";
                dr["ITEM_VALUE"] = "阴性";
                dt.Rows.Add(dr);
                dr = dt.NewRow();
                dr["ITEM_NAME"] = "阳性";
                dr["ITEM_VALUE"] = "阳性";
                dt.Rows.Add(dr);
                Utility.BindLookUpEdit(this.lupChoice, "ITEM_VALUE", "ITEM_NAME", dt, "ITEM_NAME", "条件选择");
            }
            else
            {
                this.layoutCondition.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                this.layoutRange.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                this.layoutChoice.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                this.layoutCondition.Text = DataColumnName;
                Utility.BindLookUpEdit(this.lupCondition, "ITEM_VALUE", "ITEM_NAME", LupCondition, "ITEM_NAME", "条件选择");
            }


        }

        private void lupCondition_EditValueChanged(object sender, EventArgs e)
        {
            if (lupCondition.EditValue != null)
            {
                var s = lupCondition.EditValue.ToString();
                if (s == "betweenand")
                {
                    this.txtRange.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx;
                    this.txtRange.Properties.Mask.EditMask = @"\d+/\d+";
                    this.txtRange.Enabled = true;
                    this.txtRange.Properties.NullValuePrompt = "请输入范围";
                }
                else if (s == "betweenin")
                {
                    this.txtRange.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx;
                    this.txtRange.Properties.Mask.EditMask = @"\d+/\d+";
                    this.txtRange.Enabled = true;
                    this.txtRange.Properties.NullValuePrompt = "请输入范围";
                }
                else if (s.Trim() == "")
                {
                    this.txtRange.Enabled = false;
                    this.txtRange.Properties.NullValuePrompt = "请选择条件";
                }
                else
                {
                    this.txtRange.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
                    this.txtRange.Properties.Mask.EditMask = null;
                    this.txtRange.Enabled = true;
                    this.txtRange.Properties.NullValuePrompt = "请输入数字";
                }
            }
        }


        #endregion

        #region 方法
        public bool isCheck()
        {
            bool result = true;
            this.errorProvider.ClearErrors();
            if (lupCondition.EditValue != null)
            {
                if (lupCondition.EditValue.ToString().Trim() != "")
                {
                    if (txtRange.Text.Trim() == "")
                    {
                        this.errorProvider.SetError(this.txtRange, "请输入");
                        return false;
                    }
                }

            }
            return result;
        }

        public void CtlClear()
        {
            if (isChoice)
            {
                this.lupChoice.ItemIndex = -1;
            }
            else
            {
                this.lupCondition.ItemIndex = -1;
                this.txtRange.Text = null;
            }
        }


        public String ReturnSql()
        {
            var sql = string.Empty;
            if (isChoice == false)
            {
                if (lupCondition.EditValue != null)
                {
                    var s = lupCondition.EditValue.ToString();

                    if (s == "betweenand")
                    {
                        var x = this.txtRange.Text.Split('/');
                        sql = " or(";
                        sql += "[" + DataColumnName + "] >= " + Utility.CDecimal(x[0]).ToString("#0.00") + " AND [" + DataColumnName + "]<=" + Utility.CDecimal(x[1]).ToString("#0.00");
                        sql += ")";

                    }
                    else if (s == "betweenin")
                    {
                        var x = this.txtRange.Text.Split('/');
                        sql = " or(";
                        sql += " [" + DataColumnName + "]  > " + Utility.CDecimal(x[0]).ToString("#0.00") + " AND [" + DataColumnName + "]  < " + Utility.CDecimal(x[1]).ToString("#0.00");
                        sql += ")";

                    }
                    else if (s.Trim() == "")
                    {
                        return sql;
                    }
                    else
                    {
                        sql = " or(";
                        sql += "[" + DataColumnName + "]" + this.lupCondition.EditValue.ToString() + Utility.CDecimal(this.txtRange.Text.Trim()).ToString("#0.00");
                        sql += ")";
                    }
                }
            }
            else
            {
                if (this.lupChoice.EditValue != null)
                {
                    var x = this.lupChoice.EditValue.ToString().Trim();
                    if (x == "")
                    {
                        return sql;
                    }
                    sql = " or (";
                    sql += " [" + DataColumnName + "] LIKE '%" + this.lupChoice.EditValue.ToString() + "%'";
                    sql += ")";
                }
            }
            return sql;
        }


        #endregion
    }

    public class MyQualityDataTable
    {
        public bool isChoice
        {
            get;
            set;
        }

        public string DataColumnName
        {
            get;
            set;
        }
    }

}
