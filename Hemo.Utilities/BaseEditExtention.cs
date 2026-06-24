/*----------------------------------------------------------------
 * Copyright (C) 2005 (苏州)医疗科技发展有限公司
 * 文件功能描述:基础控件扩展类
 * 创建标识:刘超-2013年5月31日
 * 
 * 修改时间:2013年9月8日
 * 修改人:刘超
 * 修改描述:新增方法BindingDataRow
 * 
 * 修改时间:2013年12月17日
 * 修改人:刘超
 * 修改描述:新增方法BindingAssessDataRow
 * 
 * 修改时间:2014年3月27日
 * 修改人:顾伟伟
 * 修改描述:修改方法BindingCheckedDataRow
 * ----------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.XtraEditors;
using System.Data;
using System.Windows.Forms;
using Hemo.Model;

namespace Hemo.Utilities
{
    /// <summary>
    /// 扩展方法
    /// </summary>
    public static class BaseEditExtention
    {
        /// <summary>
        /// 根据数据源将数据绑定至控件
        /// </summary>
        /// <param name="edit"></param>
        /// <param name="dataRow"></param>
        /// <param name="filterStr"></param>
        public static void BindingDataRow(this BaseEdit edit, DataRow dataRow, string filterStr)
        {
            if (dataRow != null)
            {
                try
                {
                    string colName = string.Empty;
                    if (!string.IsNullOrEmpty(filterStr) && edit.Name.Contains(filterStr))
                    {
                        colName = edit.Name.Replace(filterStr, string.Empty);
                    }

                    if (dataRow.Table.Columns.Contains(colName))
                    {
                        edit.Text = dataRow[colName] != DBNull.Value ? dataRow[colName].ToString() : string.Empty;
                        edit.EditValue = dataRow[colName] != DBNull.Value ? dataRow[colName].ToString() : null;

                        edit.EditValueChanged += (sender, e) =>
                        {
                            try
                            {
                                if (edit.EditValue != null && !string.IsNullOrEmpty(edit.EditValue.ToString()))
                                {
                                    dataRow[colName] = edit.EditValue;
                                }
                                else
                                {
                                    //以下这么写是因为在进行并发症绑定时不知道为什么绑定完成后边后会给这两个值赋值为空.
                                    //所以用以下的方法去解决这一个问题吧.
                                    if (colName != "FIRST_PURIFIER_MODEL" && colName != "NURSE_ID")
                                        dataRow[colName] = DBNull.Value;
                                    else
                                        edit.EditValue = dataRow[colName].ToString();

                                }
                            }
                            catch { }
                        };
                    }
                }
                catch (Exception e)
                {
                    string a = "";

                }
            }
        }
        /// <summary>
        /// 给不同类型的控件绑定数据
        /// </summary>
        /// <param name="edit"></param>
        /// <param name="dataRow"></param>

        public static void BindingDiffControlDataRow(this BaseEdit edit, DataRow dataRow)
        {
            if (dataRow != null)
            {
                string colName = edit.Name.Substring(3);
                var typename = edit.GetType().FullName.ToString();
                if (dataRow.Table.Columns.Contains(colName))
                {
                    switch (typename)
                    {
                        case "DevExpress.XtraEditors.TextEdit":
                            {
                                edit.Text = dataRow[colName] != DBNull.Value ? dataRow[colName].ToString() : string.Empty;
                                edit.EditValue = dataRow[colName] != DBNull.Value ? dataRow[colName] : null;
                                edit.EditValueChanged += (sender, e) =>
                                {
                                    try
                                    {
                                        if (edit.EditValue != null && !string.IsNullOrEmpty(edit.EditValue.ToString()))
                                        {
                                            dataRow[colName] = edit.EditValue;
                                        }
                                        else
                                        {
                                            dataRow[colName] = DBNull.Value;
                                        }
                                    }
                                    catch { }
                                };
                            }
                            break;
                        case "DevExpress.XtraEditors.RadioGroup":
                            {
                                var editControl = edit as DevExpress.XtraEditors.RadioGroup;
                                editControl.SelectedIndex = dataRow[string.Format("{0}", editControl.Name.Substring(3))] != DBNull.Value ? int.Parse(dataRow[string.Format("{0}", editControl.Name.Substring(3))].ToString()) : 0;
                                editControl.SelectedIndexChanged += (s1, e1) =>
                                {
                                    try
                                    {
                                        dataRow[string.Format("{0}", editControl.Name.Substring(3))] = editControl.SelectedIndex.ToString();

                                    }
                                    catch
                                    { }
                                };
                            }
                            break;
                        case "DevExpress.XtraEditors.CheckEdit":
                            {
                                var editCheckControl = edit as DevExpress.XtraEditors.CheckEdit;
                                editCheckControl.CheckState = dataRow[string.Format("{0}", editCheckControl.Name.Substring(3))] != DBNull.Value ? dataRow[string.Format("{0}", editCheckControl.Name.Substring(3))].ToString() == "0" ? CheckState.Checked : CheckState.Unchecked : CheckState.Unchecked;
                                editCheckControl.CheckStateChanged += (s2, e2) =>
                                {
                                    try
                                    {
                                        dataRow[string.Format("{0}", editCheckControl.Name.Substring(3))] = editCheckControl.CheckState == CheckState.Checked ? "0" : "1";

                                    }
                                    catch
                                    { }
                                };

                            }
                            break;
                        case "DevExpress.XtraEditors.LookUpEdit":
                            {
                                var LookUpControl = edit as DevExpress.XtraEditors.LookUpEdit;
                                LookUpControl.EditValue = dataRow[string.Format("{0}", LookUpControl.Name.Substring(3))] != DBNull.Value ? dataRow[string.Format("{0}", LookUpControl.Name.Substring(3))].ToString() : string.Empty;
                                LookUpControl.EditValueChanged += (s2, e2) =>
                                {
                                    try
                                    {
                                        dataRow[string.Format("{0}", LookUpControl.Name.Substring(3))] = LookUpControl.EditValue;

                                    }
                                    catch
                                    { }
                                };
                            }
                            break;
                        default:
                            edit.Text = dataRow[colName] != DBNull.Value ? dataRow[colName].ToString() : string.Empty;
                            edit.EditValue = dataRow[colName] != DBNull.Value ? dataRow[colName].ToString() : null;

                            edit.EditValueChanged += (sender, e) =>
                            {
                                try
                                {
                                    if (edit.EditValue != null && !string.IsNullOrEmpty(edit.EditValue.ToString()))
                                    {
                                        dataRow[colName] = edit.EditValue;
                                    }
                                    else
                                    {
                                        dataRow[colName] = DBNull.Value;
                                    }
                                }
                                catch { }
                            };

                            break;
                    }
                }

            }
        }

        public static void BindingDiffCheckedDataRow(this CheckedListBoxControl edit, DataRow dataRow)
        {
            if (dataRow != null)
            {
                string colName = edit.Name;

                if (dataRow.Table.Columns.Contains(colName))
                {
                    for (int i = 0; i < edit.Items.Count; i++)
                    {
                        if (dataRow[colName].ToString().Contains(i.ToString()))
                        {
                            edit.Items[i].CheckState = CheckState.Checked;
                        }
                    }
                    edit.ItemCheck += (s2, e2) =>
                    {
                        try
                        {
                            string rowValue = string.Empty;
                            for (int i = 0; i < edit.Items.Count; i++)
                            {
                                if (edit.Items[i].CheckState == CheckState.Checked)
                                {
                                    rowValue = rowValue + i.ToString() + ";";
                                }
                            }


                            dataRow[string.Format("{0}", edit.Name)] = rowValue;

                        }
                        catch
                        { }
                    };


                }
            }
        }
        /// <summary>
        /// 给不同类型的控件绑定数据
        /// </summary>
        /// <param name="edit"></param>
        /// <param name="dataRow"></param>

        public static void BindingAssessDataRow(this BaseEdit edit, HemoModel.MED_ASSESSMENTMASTERRow dataRow)
        {
            if (dataRow != null)
            {
                string colName = edit.Name;
                var typename = edit.GetType().FullName.ToString();
                if (dataRow.Table.Columns.Contains(colName))
                {
                    switch (typename)
                    {
                        case "DevExpress.XtraEditors.TextEdit":
                            {
                                edit.Text = dataRow[colName] != DBNull.Value ? dataRow[colName].ToString() : string.Empty;
                                edit.EditValue = dataRow[colName] != DBNull.Value ? dataRow[colName] : null;
                                edit.EditValueChanged += (sender, e) =>
                                {
                                    try
                                    {
                                        if (edit.EditValue != null && !string.IsNullOrEmpty(edit.EditValue.ToString()))
                                        {
                                            dataRow[colName] = edit.EditValue;
                                        }
                                        else
                                        {
                                            dataRow[colName] = DBNull.Value;
                                        }
                                    }
                                    catch { }
                                };
                            }
                            break;
                        case "DevExpress.XtraEditors.RadioGroup":
                            {
                                var editControl = edit as DevExpress.XtraEditors.RadioGroup;
                                editControl.SelectedIndex = dataRow[string.Format("{0}", editControl.Name)] != DBNull.Value ? int.Parse(dataRow[string.Format("{0}", editControl.Name)].ToString()) : 0;
                                editControl.SelectedIndexChanged += (s1, e1) =>
                                {
                                    try
                                    {
                                        dataRow[string.Format("{0}", editControl.Name)] = editControl.SelectedIndex.ToString();

                                    }
                                    catch
                                    { }
                                };
                            }
                            break;
                        case "DevExpress.XtraEditors.CheckEdit":
                            {
                                var editCheckControl = edit as DevExpress.XtraEditors.CheckEdit;
                                editCheckControl.CheckState = dataRow[string.Format("{0}", editCheckControl.Name)] != DBNull.Value ? dataRow[string.Format("{0}", editCheckControl.Name)].ToString() == "0" ? CheckState.Checked : CheckState.Unchecked : CheckState.Unchecked;
                                editCheckControl.CheckStateChanged += (s2, e2) =>
                                {
                                    try
                                    {
                                        dataRow[string.Format("{0}", editCheckControl.Name)] = editCheckControl.CheckState == CheckState.Checked ? "0" : "1";

                                    }
                                    catch
                                    { }
                                };

                            }
                            break;
                        case "DevExpress.XtraEditors.LookUpEdit":
                            {
                                var LookUpControl = edit as DevExpress.XtraEditors.LookUpEdit;
                                LookUpControl.EditValue = dataRow[string.Format("{0}", LookUpControl.Name)] != DBNull.Value ? dataRow[string.Format("{0}", LookUpControl.Name)].ToString() : string.Empty;
                                LookUpControl.EditValueChanged += (s2, e2) =>
                                {
                                    try
                                    {
                                        dataRow[string.Format("{0}", LookUpControl.Name)] = LookUpControl.EditValue;

                                    }
                                    catch
                                    { }
                                };
                            }
                            break;
                        default:
                            edit.Text = dataRow[colName] != DBNull.Value ? dataRow[colName].ToString() : string.Empty;
                            edit.EditValue = dataRow[colName] != DBNull.Value ? dataRow[colName].ToString() : null;

                            edit.EditValueChanged += (sender, e) =>
                            {
                                try
                                {
                                    if (edit.EditValue != null && !string.IsNullOrEmpty(edit.EditValue.ToString()))
                                    {
                                        dataRow[colName] = edit.EditValue;
                                    }
                                    else
                                    {
                                        dataRow[colName] = DBNull.Value;
                                    }
                                }
                                catch { }
                            };

                            break;
                    }
                }

            }
        }

        /// <summary>
        /// 绑定检查列表控件
        /// </summary>
        /// <param name="edit"></param>
        /// <param name="dataRow"></param>
        public static void BindingCheckedDataRow(this CheckedListBoxControl edit, HemoModel.MED_ASSESSMENTMASTERRow dataRow)
        {
            if (dataRow != null)
            {
                string colName = edit.Name;

                if (dataRow.Table.Columns.Contains(colName))
                {
                    for (int i = 0; i < edit.Items.Count; i++)
                    {
                        if (dataRow[colName].ToString().Contains(i.ToString()))
                        {
                            edit.Items[i].CheckState = CheckState.Checked;
                        }
                    }
                    edit.ItemCheck += (s2, e2) =>
                    {
                        try
                        {
                            string rowValue = string.Empty;
                            for (int i = 0; i < edit.Items.Count; i++)
                            {
                                if (edit.Items[i].CheckState == CheckState.Checked)
                                {
                                    rowValue = rowValue + i.ToString() + ";";
                                }
                            }


                            dataRow[string.Format("{0}", edit.Name)] = rowValue;

                        }
                        catch
                        { }
                    };


                }
            }
        }

    }
}
