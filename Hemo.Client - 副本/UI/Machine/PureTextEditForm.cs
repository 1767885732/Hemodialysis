/*----------------------------------------------------------------
// Copyright (C) 2005 苏州XX公司有限公司
// 描述：PureText编辑窗体
// 创建时间：2016-03-13
// 创建者：贺建操
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

namespace Hemo.Client.UI.Machine
{
    public partial class PureTextEditForm :HemoBaseFrm
    {
        #region 构造函数

        public PureTextEditForm()
        {
            InitializeComponent();
        }

        #endregion

        #region 方法

        /// <summary>
        /// 根据给定的数据行填充UI中控件的值
        /// </summary>
        /// <param name="row"></param>
        protected void FillUiDataByDataRow(DataRow row)
        {
            if (row == null)
            {
                return;
            }

            foreach (Control ctl in this.Controls)
            {
                if (ctl is TextEdit)
                {
                    TextEdit textEdit = ctl as TextEdit;
                    if (!textEdit.Name.Contains("txt_"))
                    {
                        continue;
                    }

                    try
                    {
                        //根据文本框的name属性，获取对应设备主机的属性
                        string mainframePropertyName = textEdit.Name.Remove(0, 4);
                        textEdit.Text = row[mainframePropertyName] != DBNull.Value ? row[mainframePropertyName].ToString() : string.Empty;
                    }
                    catch
                    {
                        continue;
                    }
                }
            }
        }

        /// <summary>
        /// 根据UI中控件值填充给定的数据行
        /// </summary>
        /// <param name="row"></param>
        protected void FillDataRowByUi(DataRow row)
        {
            if (row == null)
            {
                return;
            }

            foreach (DataColumn column in row.Table.Columns)
            {
                TextEdit targetControl = null;

                foreach (Control ctl in this.Controls)
                {
                    if (ctl is TextEdit && ctl.Name.ToLower().Contains(column.ColumnName.ToLower()))
                    {
                        targetControl = ctl as TextEdit;
                        break;
                    }
                }

                if (targetControl == null)
                {
                    continue;
                }

                switch (column.DataType.Name.ToLower())
                {
                    case "string":
                        row[column.ColumnName] = targetControl.Text.Trim();
                        break;
                    case "decimal":
                        decimal d;
                        if (decimal.TryParse(targetControl.Text.Trim(), out d))
                        {
                            row[column.ColumnName] = d;
                        }
                        break;
                    case "datetime":
                        DateTime date;
                        if (DateTime.TryParse(targetControl.Text.Trim(), out date))
                        {
                            row[column.ColumnName] = date;
                        }
                        break;
                    default:
                        break;
                }
            }
        }

        #endregion
    }
}