/*----------------------------------------------------------------
// Copyright (C) 2013 苏州XX公司有限公司
// 描述：通用窗体取值、赋值、清空值、验证控件值方法
// 创建时间：2013-03-13
// 创建者：刘超
//  
// 修改时间：2014-5-6
// 修改人：吕志强
// 修改描述：更新GetDataTableByPanel方法
//
// 修改时间：
// 修改人：
// 修改描述：
----------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using DevExpress.XtraEditors;
using Hemo.Utilities;
using DevExpress.XtraEditors.Controls;

namespace Hemo.Utilities
{
    public class BaseControlInfo
    {

        /// <summary>
        /// 根据数据源列名称，遍历Panel控件子控件并赋值
        /// </summary>
        /// <param name="pDt">数据源</param>
        /// <param name="pPanel">控件面板</param>
        public static void SetControlDataByDataTable(DataTable pDt, DevExpress.XtraEditors.PanelControl pPanel)
        {
            if (pDt != null && pDt.Rows.Count > 0 && pPanel.Controls.Count > 0)
            {
                for (int i = 0; i < pPanel.Controls.Count; i++)
                {
                    //panel中包含GroupControl递归调用
                    if (pPanel.Controls[i].GetType().ToString() == "DevExpress.XtraEditors.GroupControl")
                    {
                        SetControlDataByDataTable(pDt, ((DevExpress.XtraEditors.GroupControl)pPanel.Controls[i]));
                    }
                    if (pPanel.Controls[i].GetType().ToString() == "DevExpress.XtraEditors.PanelControl")
                    {
                        SetControlDataByDataTable(pDt, ((DevExpress.XtraEditors.PanelControl)pPanel.Controls[i]));
                    }

                    if (pPanel.Controls[i].GetType().ToString() == "DevExpress.XtraTab.XtraTabControl")
                    {
                        foreach (DevExpress.XtraTab.XtraTabPage item in ((DevExpress.XtraTab.XtraTabControl)pPanel.Controls[i]).TabPages)
                        {
                            SetControlDataByDataTable(pDt, item);
                        }
                    }
                }
                for (int j = 0; j < pDt.Columns.Count; j++)
                {
                    for (int i = 0; i < pPanel.Controls.Count; i++)
                    {
                        if (pPanel.Controls[i].Name.Substring(3, pPanel.Controls[i].Name.Length - 3).ToUpper() == pDt.Columns[j].Caption.ToUpper() && (pPanel.Controls[i].GetType().ToString() == "DevExpress.XtraEditors.TextEdit" ||
                         pPanel.Controls[i].GetType().ToString() == "DevExpress.XtraEditors.ComboBoxEdit" || pPanel.Controls[i].GetType().ToString() == "DevExpress.XtraEditors.DateEdit" || pPanel.Controls[i].GetType().ToString() == "DevExpress.XtraEditors.MemoEdit" ||
                         pPanel.Controls[i].GetType().ToString() == "DevExpress.XtraEditors.CheckEdit" || pPanel.Controls[i].GetType().ToString() == "DevExpress.XtraEditors.SpinEdit" || pPanel.Controls[i].GetType().ToString() == "DevExpress.XtraEditors.LookUpEdit" ||
                         pPanel.Controls[i].GetType().ToString() == "DevExpress.XtraEditors.CheckedComboBoxEdit" || pPanel.Controls[i].GetType().ToString() == "DevExpress.XtraEditors.RadioGroup" || pPanel.Controls[i].GetType().ToString() == "Hemo.Utilities.CustomGridLookUpEdit"))
                        {

                            if (pPanel.Controls[i].GetType().ToString() == "DevExpress.XtraEditors.DateEdit")
                            {
                                if (!string.IsNullOrEmpty(pDt.Rows[0][j].ToString()))
                                {
                                    if (Utility.CDate(pDt.Rows[0][j].ToString()).ToShortDateString() != "0001/1/1")
                                    {
                                        pPanel.Controls[i].Text = Utility.CDate(pDt.Rows[0][j].ToString()).ToShortDateString();
                                    }
                                    break;
                                }
                            }
                            else if (pPanel.Controls[i].GetType().ToString() == "DevExpress.XtraEditors.CheckEdit")
                            {
                                ((DevExpress.XtraEditors.CheckEdit)pPanel.Controls[i]).Checked = pDt.Rows[0][j].ToString() == "1" ? true : false;
                                break;
                            }
                            else if (pPanel.Controls[i].GetType().ToString() == "DevExpress.XtraEditors.SpinEdit")
                            {   //数字
                                ((DevExpress.XtraEditors.SpinEdit)pPanel.Controls[i]).Value = Utility.CDecimal(pDt.Rows[0][j].ToString());
                                break;
                            }
                            else if (pPanel.Controls[i].GetType().ToString() == "DevExpress.XtraEditors.LookUpEdit")
                            {   //下拉框不可输入,下拉多选框
                                ((DevExpress.XtraEditors.LookUpEdit)pPanel.Controls[i]).EditValue = pDt.Rows[0][j].ToString();
                                break;
                            }
                            else if (pPanel.Controls[i].GetType().ToString() == "DevExpress.XtraEditors.CheckedComboBoxEdit")
                            {   //下拉框不可输入,下拉多选框
                                ((DevExpress.XtraEditors.CheckedComboBoxEdit)pPanel.Controls[i]).EditValue = pDt.Rows[0][j].ToString();
                                break;
                            }
                            else if (pPanel.Controls[i].GetType().ToString() == "DevExpress.XtraEditors.RadioGroup")
                            {   //单选框组合
                                ((DevExpress.XtraEditors.RadioGroup)pPanel.Controls[i]).EditValue = pDt.Rows[0][j].ToString();
                                break;
                            }
                            else if (pPanel.Controls[i].GetType().ToString() == "Hemo.Utilities.CustomGridLookUpEdit")
                            {  //下拉多列匹配自定义控件
                                ((Hemo.Utilities.CustomGridLookUpEdit)pPanel.Controls[i]).EditValue = pDt.Rows[0][j].ToString();
                                break;
                            }
                            else if (pPanel.Controls[i].GetType().ToString() == "DevExpress.XtraEditors.PictureEdit")
                            {  //下拉多列匹配自定义控件
                                ((DevExpress.XtraEditors.PictureEdit)pPanel.Controls[i]).EditValue = pDt.Rows[0][j];
                                break;
                            }
                            else
                            {
                                pPanel.Controls[i].Text = pDt.Rows[0][j].ToString();
                                break;
                            }
                        }

                    }
                }
            }
        }

        /// <summary>
        /// 根据数据源列名称，遍历XtraScrollableControl控件子控件并赋值
        /// </summary>
        /// <param name="pDt">数据源</param>
        /// <param name="pPanel">控件面板</param>
        public static void SetControlDataByDataTable(DataTable pDt, DevExpress.XtraEditors.XtraScrollableControl pPanel)
        {
            if (pDt != null && pDt.Rows.Count > 0 && pPanel.Controls.Count > 0)
            {
                for (int i = 0; i < pPanel.Controls.Count; i++)
                {
                    //panel中包含GroupControl递归调用  
                    if (pPanel.Controls[i].GetType().ToString() == "DevExpress.XtraEditors.GroupControl")
                    {
                        SetControlDataByDataTable(pDt, ((DevExpress.XtraEditors.GroupControl)pPanel.Controls[i]));
                    }
                    if (pPanel.Controls[i].GetType().ToString() == "DevExpress.XtraEditors.PanelControl")
                    {
                        SetControlDataByDataTable(pDt, ((DevExpress.XtraEditors.PanelControl)pPanel.Controls[i]));
                    }

                    if (pPanel.Controls[i].GetType().ToString() == "DevExpress.XtraTab.XtraTabControl")
                    {
                        foreach (DevExpress.XtraTab.XtraTabPage item in ((DevExpress.XtraTab.XtraTabControl)pPanel.Controls[i]).TabPages)
                        {
                            SetControlDataByDataTable(pDt, item);
                        }
                    }
                }
                for (int j = 0; j < pDt.Columns.Count; j++)
                {
                    for (int i = 0; i < pPanel.Controls.Count; i++)
                    {
                        if (pPanel.Controls[i].Name.Substring(3, pPanel.Controls[i].Name.Length - 3).ToUpper() == pDt.Columns[j].Caption.ToUpper() && (pPanel.Controls[i].GetType().ToString() == "DevExpress.XtraEditors.TextEdit" ||
                         pPanel.Controls[i].GetType().ToString() == "DevExpress.XtraEditors.ComboBoxEdit" || pPanel.Controls[i].GetType().ToString() == "DevExpress.XtraEditors.DateEdit" || pPanel.Controls[i].GetType().ToString() == "DevExpress.XtraEditors.MemoEdit" ||
                         pPanel.Controls[i].GetType().ToString() == "DevExpress.XtraEditors.CheckEdit" || pPanel.Controls[i].GetType().ToString() == "DevExpress.XtraEditors.SpinEdit" || pPanel.Controls[i].GetType().ToString() == "DevExpress.XtraEditors.LookUpEdit" ||
                         pPanel.Controls[i].GetType().ToString() == "DevExpress.XtraEditors.CheckedComboBoxEdit" || pPanel.Controls[i].GetType().ToString() == "DevExpress.XtraEditors.RadioGroup" || pPanel.Controls[i].GetType().ToString() == "Hemo.Utilities.CustomGridLookUpEdit"))
                        {
                            if (pPanel.Controls[i].GetType().ToString() == "DevExpress.XtraEditors.DateEdit")
                            {
                                if (!string.IsNullOrEmpty(pDt.Rows[0][j].ToString()))
                                {
                                    if (Utility.CDate(pDt.Rows[0][j].ToString()).ToShortDateString() != "0001/1/1")
                                    {
                                        pPanel.Controls[i].Text = Utility.CDate(pDt.Rows[0][j].ToString()).ToShortDateString();
                                    }
                                    break;
                                }
                            }
                            else if (pPanel.Controls[i].GetType().ToString() == "DevExpress.XtraEditors.CheckEdit")
                            {
                                ((DevExpress.XtraEditors.CheckEdit)pPanel.Controls[i]).Checked = pDt.Rows[0][j].ToString() == "1" ? true : false;
                                break;
                            }
                            else if (pPanel.Controls[i].GetType().ToString() == "DevExpress.XtraEditors.SpinEdit")
                            {   //数字
                                ((DevExpress.XtraEditors.SpinEdit)pPanel.Controls[i]).Value = Utility.CDecimal(pDt.Rows[0][j].ToString());
                                break;
                            }
                            else if (pPanel.Controls[i].GetType().ToString() == "DevExpress.XtraEditors.LookUpEdit")
                            {   //下拉框不可输入,下拉多选框
                                ((DevExpress.XtraEditors.LookUpEdit)pPanel.Controls[i]).EditValue = pDt.Rows[0][j].ToString();
                                break;
                            }
                            else if (pPanel.Controls[i].GetType().ToString() == "DevExpress.XtraEditors.CheckedComboBoxEdit")
                            {   //下拉框不可输入,下拉多选框
                                ((DevExpress.XtraEditors.CheckedComboBoxEdit)pPanel.Controls[i]).EditValue = pDt.Rows[0][j].ToString();
                                break;
                            }
                            else if (pPanel.Controls[i].GetType().ToString() == "DevExpress.XtraEditors.RadioGroup")
                            {   //单选框组合
                                ((DevExpress.XtraEditors.RadioGroup)pPanel.Controls[i]).EditValue = pDt.Rows[0][j].ToString();
                                break;
                            }
                            else if (pPanel.Controls[i].GetType().ToString() == "Hemo.Utilities.CustomGridLookUpEdit")
                            {  //下拉多列匹配自定义控件
                                ((Hemo.Utilities.CustomGridLookUpEdit)pPanel.Controls[i]).EditValue = pDt.Rows[0][j].ToString();
                                break;
                            }
                            else if (pPanel.Controls[i].GetType().ToString() == "DevExpress.XtraEditors.PictureEdit")
                            {  //下拉多列匹配自定义控件
                                ((DevExpress.XtraEditors.PictureEdit)pPanel.Controls[i]).EditValue = pDt.Rows[0][j];
                                break;
                            }
                            else
                            {
                                pPanel.Controls[i].Text = pDt.Rows[0][j].ToString();
                                break;
                            }
                        }

                    }
                }

            }
        }

        /// <summary>
        /// 根据数据源列名称，遍历Panel控件子控件并赋值
        /// </summary>
        /// <param name="pDt">数据源</param>
        /// <param name="pPanel">控件面板</param>
        /// <param name="pIsNew">是否新增</param>
        public static DataTable GetDataTableByPanel(DataTable pDt, DevExpress.XtraEditors.PanelControl pPanel, bool pIsNew)
        {
            if (pPanel.Controls.Count > 0)
            {
                DataRow dr;
                if (pIsNew)
                {
                    //新增
                    dr = pDt.NewRow();
                }
                else
                {
                    //修改
                    dr = (pDt != null && pDt.Rows.Count > 0) ? pDt.Rows[0] : pDt.NewRow();
                }
                if (pDt != null)
                {
                    for (int i = 0; i < pPanel.Controls.Count; i++)
                    {
                        for (int j = 0; j < pDt.Columns.Count; j++)
                        {
                            if (pPanel.Controls[i].Name.Substring(3, pPanel.Controls[i].Name.Length - 3).ToUpper() == pDt.Columns[j].Caption.ToUpper() && (pPanel.Controls[i].GetType().ToString() == "DevExpress.XtraEditors.TextEdit" ||
                                pPanel.Controls[i].GetType().ToString() == "DevExpress.XtraEditors.ComboBoxEdit" || pPanel.Controls[i].GetType().ToString() == "DevExpress.XtraEditors.DateEdit" || pPanel.Controls[i].GetType().ToString() == "DevExpress.XtraEditors.SpinEdit" ||
                                pPanel.Controls[i].GetType().ToString() == "DevExpress.XtraEditors.MemoEdit" || pPanel.Controls[i].GetType().ToString() == "DevExpress.XtraEditors.CheckEdit" || pPanel.Controls[i].GetType().ToString() == "DevExpress.XtraEditors.LookUpEdit" ||
                                pPanel.Controls[i].GetType().ToString() == "DevExpress.XtraEditors.CheckedComboBoxEdit" || pPanel.Controls[i].GetType().ToString() == "DevExpress.XtraEditors.RadioGroup" || pPanel.Controls[i].GetType().ToString() == "Hemo.Utilities.CustomGridLookUpEdit"))
                            {
                                if (pPanel.Controls[i].GetType().ToString() == "DevExpress.XtraEditors.DateEdit")
                                {
                                    if (((DevExpress.XtraEditors.DateEdit)pPanel.Controls[i]).EditValue != null)
                                    {
                                        dr[pDt.Columns[j].Caption] = Utility.CDate(((DevExpress.XtraEditors.DateEdit)pPanel.Controls[i]).EditValue.ToString());
                                        break;
                                    }
                                    else
                                    {
                                        dr[pDt.Columns[j].Caption] = Utility.CDate(string.Empty);
                                    }
                                }
                                else if (pPanel.Controls[i].GetType().ToString() == "DevExpress.XtraEditors.CheckEdit")
                                {     //checkbox控件
                                    dr[pDt.Columns[j].Caption] = ((DevExpress.XtraEditors.CheckEdit)pPanel.Controls[i]).Checked == true ? "1" : "0";
                                    break;
                                }
                                else if (pPanel.Controls[i].GetType().ToString() == "DevExpress.XtraEditors.SpinEdit")
                                {   //数字
                                    dr[pDt.Columns[j].Caption] = ((DevExpress.XtraEditors.SpinEdit)pPanel.Controls[i]).Value;
                                    break;
                                }
                                else if (pPanel.Controls[i].GetType().ToString() == "DevExpress.XtraEditors.LookUpEdit")
                                {   //下拉框，不可输入 ,下拉多选框
                                    dr[pDt.Columns[j].Caption] = ((DevExpress.XtraEditors.LookUpEdit)pPanel.Controls[i]).EditValue;
                                    break;
                                }
                                else if (pPanel.Controls[i].GetType().ToString() == "DevExpress.XtraEditors.CheckedComboBoxEdit")
                                {   //下拉框，不可输入 ,下拉多选框
                                    dr[pDt.Columns[j].Caption] = ((DevExpress.XtraEditors.CheckedComboBoxEdit)pPanel.Controls[i]).EditValue;
                                    break;
                                }
                                else if (pPanel.Controls[i].GetType().ToString() == "DevExpress.XtraEditors.RadioGroup")
                                {   //单选框组
                                    pDt.Rows[0][pDt.Columns[j].Caption] = ((DevExpress.XtraEditors.RadioGroup)pPanel.Controls[i]).EditValue;
                                    break;
                                }
                                else if (pPanel.Controls[i].GetType().ToString() == "DevExpress.XtraEditors.PictureEdit")
                                {   //单选框组
                                    pDt.Rows[0][pDt.Columns[j].Caption] = ((DevExpress.XtraEditors.PictureEdit)pPanel.Controls[i]).EditValue;
                                    break;
                                }
                                else
                                {
                                    dr[pDt.Columns[j].Caption] = pPanel.Controls[i].Text.Trim();
                                    break;
                                }
                            }

                        }
                        //panel中包含GroupControl递归调用
                        //if (pPanel.Controls[i].GetType().ToString() == "DevExpress.XtraEditors.GroupControl") {
                        //    pDt = GetDataTableByPanel(pDt, ((DevExpress.XtraEditors.GroupControl)pPanel.Controls[i]), false);
                        //    break;
                        //}
                    }
                }
                if (pIsNew)
                {
                    pDt.Rows.Add(dr);
                }
            }
            return pDt;
        }

        /// <summary>
        /// 根据数据源列名称，遍历Panel控件子控件并赋值
        /// </summary>
        /// <param name="pDt">数据源</param>
        /// <param name="pPanel">控件面板</param>
        /// <param name="pIsNew">是否新增</param>
        public static DataTable GetDataTableByPanel(DataTable pDt, DevExpress.XtraEditors.PanelControl pPanel)
        {
            if (pPanel.Controls.Count > 0)
            {
                //DataRow dr;
                //if (pIsNew) {
                //    //新增
                //    dr = pDt.NewRow();
                //}
                //else {
                //    //修改
                //   dr = pDt.Rows[0];
                //}
                if (pDt != null)
                {
                    for (int i = 0; i < pPanel.Controls.Count; i++)
                    {
                        //panel中包含GroupControl递归调用
                        if (pPanel.Controls[i].GetType().ToString() == "DevExpress.XtraEditors.GroupControl")
                        {
                            pDt = GetDataTableByPanel(pDt, ((DevExpress.XtraEditors.GroupControl)pPanel.Controls[i]));
                            //  break;
                        }
                        if (pPanel.Controls[i].GetType().ToString() == "DevExpress.XtraEditors.PanelControl")
                        {
                            pDt = GetDataTableByPanel(pDt, ((DevExpress.XtraEditors.PanelControl)pPanel.Controls[i]));
                        }

                        if (pPanel.Controls[i].GetType().ToString() == "DevExpress.XtraTab.XtraTabControl")
                        {
                            foreach (DevExpress.XtraTab.XtraTabPage item in ((DevExpress.XtraTab.XtraTabControl)pPanel.Controls[i]).TabPages)
                            {
                                pDt = GetDataTableByPanel(pDt, item);
                            }
                        }
                        for (int j = 0; j < pDt.Columns.Count; j++)
                        {
                            if (pPanel.Controls[i].Name.Substring(3, pPanel.Controls[i].Name.Length - 3).ToUpper() == pDt.Columns[j].Caption.ToUpper() && (pPanel.Controls[i].GetType().ToString() == "DevExpress.XtraEditors.TextEdit" ||
                                pPanel.Controls[i].GetType().ToString() == "DevExpress.XtraEditors.ComboBoxEdit" || pPanel.Controls[i].GetType().ToString() == "DevExpress.XtraEditors.DateEdit" || pPanel.Controls[i].GetType().ToString() == "DevExpress.XtraEditors.SpinEdit" ||
                                pPanel.Controls[i].GetType().ToString() == "DevExpress.XtraEditors.MemoEdit" || pPanel.Controls[i].GetType().ToString() == "DevExpress.XtraEditors.CheckEdit" || pPanel.Controls[i].GetType().ToString() == "DevExpress.XtraEditors.LookUpEdit" ||
                                pPanel.Controls[i].GetType().ToString() == "DevExpress.XtraEditors.CheckedComboBoxEdit" || pPanel.Controls[i].GetType().ToString() == "DevExpress.XtraEditors.RadioGroup"))
                            {
                                if (pPanel.Controls[i].GetType().ToString() == "DevExpress.XtraEditors.CheckEdit")
                                {     //checkbox控件
                                    pDt.Rows[0][pDt.Columns[j].Caption] = ((DevExpress.XtraEditors.CheckEdit)pPanel.Controls[i]).Checked == true ? "1" : "0";
                                    break;
                                }
                                else if (pPanel.Controls[i].GetType().ToString() == "DevExpress.XtraEditors.SpinEdit")
                                {   //数字
                                    pDt.Rows[0][pDt.Columns[j].Caption] = ((DevExpress.XtraEditors.SpinEdit)pPanel.Controls[i]).Value;
                                    break;
                                }
                                else if (pPanel.Controls[i].GetType().ToString() == "DevExpress.XtraEditors.LookUpEdit")
                                {   //下拉框，不可输入 
                                    pDt.Rows[0][pDt.Columns[j].Caption] = ((DevExpress.XtraEditors.LookUpEdit)pPanel.Controls[i]).EditValue;
                                    break;
                                }
                                else if (pPanel.Controls[i].GetType().ToString() == "DevExpress.XtraEditors.CheckedComboBoxEdit")
                                {   //下拉框，不可输入 
                                    pDt.Rows[0][pDt.Columns[j].Caption] = ((DevExpress.XtraEditors.CheckedComboBoxEdit)pPanel.Controls[i]).EditValue;
                                    break;
                                }
                                else if (pPanel.Controls[i].GetType().ToString() == "DevExpress.XtraEditors.DateEdit")
                                {   //日期控件
                                    pDt.Rows[0][pDt.Columns[j].Caption] = Utilities.Utility.CDate(((DevExpress.XtraEditors.DateEdit)pPanel.Controls[i]).Text.ToString());
                                    break;
                                }
                                else if (pPanel.Controls[i].GetType().ToString() == "DevExpress.XtraEditors.RadioGroup")
                                {   //单选框组
                                    pDt.Rows[0][pDt.Columns[j].Caption] = ((DevExpress.XtraEditors.RadioGroup)pPanel.Controls[i]).EditValue;
                                    break;
                                }
                                else
                                {
                                    try
                                    {
                                        pDt.Rows[0][pDt.Columns[j].Caption] = pPanel.Controls[i].Text.Trim();
                                        break;
                                    }
                                    catch (Exception ex)
                                    {
                                        pDt.Rows[0][pDt.Columns[j].Caption] = string.IsNullOrEmpty(pPanel.Controls[i].Text.Trim()) ? 0 : Convert.ToDecimal(pPanel.Controls[i].Text.Trim());

                                    }

                                }
                            }

                        }
                    }
                }
                //if (pIsNew) {
                //    pDt.Rows.Add(dr);
                //}
            }
            return pDt;
        }

        /// <summary>
        /// 根据数据源列名称，遍历XtraScrollableControl控件子控件并赋值
        /// </summary>
        /// <param name="pDt">数据源</param>
        /// <param name="pPanel">滚动面板</param>
        /// <param name="pIsNew">是否新增</param>
        public static DataTable GetDataTableByPanel(DataTable pDt, DevExpress.XtraEditors.XtraScrollableControl pPanel)
        {
            if (pPanel.Controls.Count > 0)
            {
                if (pDt != null)
                {
                    for (int i = 0; i < pPanel.Controls.Count; i++)
                    {

                        //panel中包含GroupControl递归调用
                        if (pPanel.Controls[i].GetType().ToString() == "DevExpress.XtraEditors.GroupControl")
                        {
                            pDt = GetDataTableByPanel(pDt, ((DevExpress.XtraEditors.GroupControl)pPanel.Controls[i]));
                            //  break;
                        }
                        if (pPanel.Controls[i].GetType().ToString() == "DevExpress.XtraEditors.PanelControl")
                        {
                            pDt = GetDataTableByPanel(pDt, ((DevExpress.XtraEditors.PanelControl)pPanel.Controls[i]));
                        }

                        if (pPanel.Controls[i].GetType().ToString() == "DevExpress.XtraTab.XtraTabControl")
                        {
                            foreach (DevExpress.XtraTab.XtraTabPage item in ((DevExpress.XtraTab.XtraTabControl)pPanel.Controls[i]).TabPages)
                            {
                                pDt = GetDataTableByPanel(pDt, item);
                            }
                        }
                        for (int j = 0; j < pDt.Columns.Count; j++)
                        {
                            if (pPanel.Controls[i].Name.Substring(3, pPanel.Controls[i].Name.Length - 3).ToUpper() == pDt.Columns[j].Caption.ToUpper() && (pPanel.Controls[i].GetType().ToString() == "DevExpress.XtraEditors.TextEdit" ||
                                pPanel.Controls[i].GetType().ToString() == "DevExpress.XtraEditors.ComboBoxEdit" || pPanel.Controls[i].GetType().ToString() == "DevExpress.XtraEditors.DateEdit" || pPanel.Controls[i].GetType().ToString() == "DevExpress.XtraEditors.SpinEdit" ||
                                pPanel.Controls[i].GetType().ToString() == "DevExpress.XtraEditors.MemoEdit" || pPanel.Controls[i].GetType().ToString() == "DevExpress.XtraEditors.CheckEdit" || pPanel.Controls[i].GetType().ToString() == "DevExpress.XtraEditors.LookUpEdit" ||
                                pPanel.Controls[i].GetType().ToString() == "DevExpress.XtraEditors.CheckedComboBoxEdit" || pPanel.Controls[i].GetType().ToString() == "DevExpress.XtraEditors.RadioGroup"))
                            {
                                if (pPanel.Controls[i].GetType().ToString() == "DevExpress.XtraEditors.CheckEdit")
                                {     //checkbox控件
                                    pDt.Rows[0][pDt.Columns[j].Caption] = ((DevExpress.XtraEditors.CheckEdit)pPanel.Controls[i]).Checked == true ? "1" : "0";
                                    break;
                                }
                                else if (pPanel.Controls[i].GetType().ToString() == "DevExpress.XtraEditors.SpinEdit")
                                {   //数字
                                    pDt.Rows[0][pDt.Columns[j].Caption] = ((DevExpress.XtraEditors.SpinEdit)pPanel.Controls[i]).Value;
                                    break;
                                }
                                else if (pPanel.Controls[i].GetType().ToString() == "DevExpress.XtraEditors.LookUpEdit")
                                {   //下拉框，不可输入 
                                    pDt.Rows[0][pDt.Columns[j].Caption] = ((DevExpress.XtraEditors.LookUpEdit)pPanel.Controls[i]).EditValue;
                                    break;
                                }
                                else if (pPanel.Controls[i].GetType().ToString() == "DevExpress.XtraEditors.CheckedComboBoxEdit")
                                {   //下拉框，不可输入 
                                    pDt.Rows[0][pDt.Columns[j].Caption] = ((DevExpress.XtraEditors.CheckedComboBoxEdit)pPanel.Controls[i]).EditValue;
                                    break;
                                }
                                else if (pPanel.Controls[i].GetType().ToString() == "DevExpress.XtraEditors.DateEdit")
                                {   //日期控件
                                    pDt.Rows[0][pDt.Columns[j].Caption] = Utilities.Utility.CDate(((DevExpress.XtraEditors.DateEdit)pPanel.Controls[i]).Text.ToString());
                                    break;
                                }
                                else if (pPanel.Controls[i].GetType().ToString() == "DevExpress.XtraEditors.RadioGroup")
                                {   //单选框组
                                    pDt.Rows[0][pDt.Columns[j].Caption] = ((DevExpress.XtraEditors.RadioGroup)pPanel.Controls[i]).EditValue;
                                    break;
                                }
                                else
                                {
                                    pDt.Rows[0][pDt.Columns[j].Caption] = pPanel.Controls[i].Text.Trim();
                                    break;
                                }
                            }

                        }
                    }
                }
            }
            return pDt;
        }

        /// <summary>
        /// 清空Panel中的控件值
        /// </summary>
        /// <param name="pPanel">对应的容器</param>
        public static void ClearControlText(DevExpress.XtraEditors.PanelControl pPanel)
        {
            if (pPanel.Controls.Count > 0)
            {
                for (int i = 0; i < pPanel.Controls.Count; i++)
                {
                    //panel中包含GroupControl递归调用
                    if (pPanel.Controls[i].GetType().ToString() == "DevExpress.XtraEditors.GroupControl")
                    {
                        ClearControlText((DevExpress.XtraEditors.GroupControl)pPanel.Controls[i]);
                        //  break;
                    }
                    if (pPanel.Controls[i].GetType().ToString() == "DevExpress.XtraEditors.PanelControl")
                    {
                        ClearControlText((DevExpress.XtraEditors.PanelControl)pPanel.Controls[i]);
                    }

                    if (pPanel.Controls[i].GetType().ToString() == "DevExpress.XtraTab.XtraTabControl")
                    {
                        foreach (DevExpress.XtraTab.XtraTabPage item in ((DevExpress.XtraTab.XtraTabControl)pPanel.Controls[i]).TabPages)
                        {
                            ClearControlText(item);
                        }
                    }
                    if (pPanel.Controls[i].GetType().ToString() == "DevExpress.XtraEditors.TextEdit" || pPanel.Controls[i].GetType().ToString() == "DevExpress.XtraEditors.ComboBoxEdit" || pPanel.Controls[i].GetType().ToString() == "DevExpress.XtraEditors.DateEdit" || pPanel.Controls[i].GetType().ToString() == "DevExpress.XtraEditors.MemoEdit")
                    {
                        pPanel.Controls[i].Text = string.Empty;
                    }
                    else if (pPanel.Controls[i].GetType().ToString() == "DevExpress.XtraEditors.LookUpEdit")
                    {
                        ((DevExpress.XtraEditors.LookUpEdit)pPanel.Controls[i]).EditValue = string.Empty;
                    }
                    else if (pPanel.Controls[i].GetType().ToString() == "DevExpress.XtraEditors.CheckedComboBoxEdit")
                    {
                        ((DevExpress.XtraEditors.CheckedComboBoxEdit)pPanel.Controls[i]).EditValue = string.Empty;
                    }
                    else if (pPanel.Controls[i].GetType().ToString() == "DevExpress.XtraEditors.TimeEdit")
                    {
                        ((DevExpress.XtraEditors.TimeEdit)pPanel.Controls[i]).EditValue = string.Empty;
                    }
                    else if (pPanel.Controls[i].GetType().ToString() == "DevExpress.XtraEditors.SpinEdit")
                    {
                        ((DevExpress.XtraEditors.SpinEdit)pPanel.Controls[i]).Value = 0;
                    }
                    else if (pPanel.Controls[i].GetType().ToString() == "DevExpress.XtraEditors.CheckEdit")
                    {
                        ((DevExpress.XtraEditors.CheckEdit)pPanel.Controls[i]).Checked = false;
                    }
                    else if (pPanel.Controls[i].GetType().ToString() == "Hemo.Utilities.CustomGridLookUpEdit")
                    {
                        ((Hemo.Utilities.CustomGridLookUpEdit)pPanel.Controls[i]).EditValue = string.Empty;
                    }
                    else if (pPanel.Controls[i].GetType().ToString() == "DevExpress.XtraEditors.CheckedListBoxControl")
                    {
                        foreach (DevExpress.XtraEditors.Controls.CheckedListBoxItem item in ((DevExpress.XtraEditors.CheckedListBoxControl)pPanel.Controls[i]).Items)
                        {
                            item.CheckState = System.Windows.Forms.CheckState.Unchecked;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 清空XtraScrollableControl中的控件值
        /// </summary>
        /// <param name="pPanel">对应的容器</param>
        public static void ClearControlText(DevExpress.XtraEditors.XtraScrollableControl pPanel)
        {
            if (pPanel.Controls.Count > 0)
            {
                for (int i = 0; i < pPanel.Controls.Count; i++)
                {
                    //panel中包含GroupControl递归调用
                    if (pPanel.Controls[i].GetType().ToString() == "DevExpress.XtraEditors.GroupControl")
                    {
                        ClearControlText((DevExpress.XtraEditors.GroupControl)pPanel.Controls[i]);
                        //  break;
                    }
                    if (pPanel.Controls[i].GetType().ToString() == "DevExpress.XtraEditors.PanelControl")
                    {
                        ClearControlText((DevExpress.XtraEditors.PanelControl)pPanel.Controls[i]);
                    }

                    if (pPanel.Controls[i].GetType().ToString() == "DevExpress.XtraTab.XtraTabControl")
                    {
                        foreach (DevExpress.XtraTab.XtraTabPage item in ((DevExpress.XtraTab.XtraTabControl)pPanel.Controls[i]).TabPages)
                        {
                            ClearControlText(item);
                        }
                    }
                    if (pPanel.Controls[i].GetType().ToString() == "DevExpress.XtraEditors.TextEdit" || pPanel.Controls[i].GetType().ToString() == "DevExpress.XtraEditors.ComboBoxEdit" || pPanel.Controls[i].GetType().ToString() == "DevExpress.XtraEditors.DateEdit" || pPanel.Controls[i].GetType().ToString() == "DevExpress.XtraEditors.MemoEdit")
                    {
                        pPanel.Controls[i].Text = string.Empty;
                    }
                    else if (pPanel.Controls[i].GetType().ToString() == "DevExpress.XtraEditors.LookUpEdit")
                    {
                        ((DevExpress.XtraEditors.LookUpEdit)pPanel.Controls[i]).EditValue = string.Empty;
                    }
                    else if (pPanel.Controls[i].GetType().ToString() == "DevExpress.XtraEditors.CheckedComboBoxEdit")
                    {
                        ((DevExpress.XtraEditors.CheckedComboBoxEdit)pPanel.Controls[i]).EditValue = string.Empty;
                    }
                    else if (pPanel.Controls[i].GetType().ToString() == "DevExpress.XtraEditors.TimeEdit")
                    {
                        ((DevExpress.XtraEditors.TimeEdit)pPanel.Controls[i]).EditValue = string.Empty;
                    }
                    else if (pPanel.Controls[i].GetType().ToString() == "DevExpress.XtraEditors.SpinEdit")
                    {
                        ((DevExpress.XtraEditors.SpinEdit)pPanel.Controls[i]).Value = 0;
                    }
                    else if (pPanel.Controls[i].GetType().ToString() == "DevExpress.XtraEditors.CheckEdit")
                    {
                        ((DevExpress.XtraEditors.CheckEdit)pPanel.Controls[i]).Checked = false;
                    }
                    else if (pPanel.Controls[i].GetType().ToString() == "Hemo.Utilities.CustomGridLookUpEdit")
                    {
                        ((Hemo.Utilities.CustomGridLookUpEdit)pPanel.Controls[i]).EditValue = string.Empty;
                    }
                    else if (pPanel.Controls[i].GetType().ToString() == "DevExpress.XtraEditors.CheckedListBoxControl")
                    {
                        foreach (DevExpress.XtraEditors.Controls.CheckedListBoxItem item in ((DevExpress.XtraEditors.CheckedListBoxControl)pPanel.Controls[i]).Items)
                        {
                            item.CheckState = System.Windows.Forms.CheckState.Unchecked;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 清空GroupPanel中的控件值
        /// </summary>
        /// <param name="pPanel"></param>
        public static void ClearControlText(DevExpress.XtraEditors.GroupControl pPanel)
        {
            if (pPanel.Controls.Count > 0)
            {
                for (int i = 0; i < pPanel.Controls.Count; i++)
                {
                    if (pPanel.Controls[i].GetType().ToString() == "DevExpress.XtraEditors.TextEdit" || pPanel.Controls[i].GetType().ToString() == "DevExpress.XtraEditors.ComboBoxEdit" || pPanel.Controls[i].GetType().ToString() == "DevExpress.XtraEditors.DateEdit" || pPanel.Controls[i].GetType().ToString() == "DevExpress.XtraEditors.MemoEdit")
                    {
                        pPanel.Controls[i].Text = string.Empty;
                    }
                    else if (pPanel.Controls[i].GetType().ToString() == "DevExpress.XtraEditors.LookUpEdit")
                    {
                        ((DevExpress.XtraEditors.LookUpEdit)pPanel.Controls[i]).EditValue = string.Empty;
                    }
                    else if (pPanel.Controls[i].GetType().ToString() == "DevExpress.XtraEditors.CheckedComboBoxEdit")
                    {
                        ((DevExpress.XtraEditors.CheckedComboBoxEdit)pPanel.Controls[i]).EditValue = string.Empty;
                    }
                    else if (pPanel.Controls[i].GetType().ToString() == "DevExpress.XtraEditors.TimeEdit")
                    {
                        ((DevExpress.XtraEditors.TimeEdit)pPanel.Controls[i]).EditValue = string.Empty;
                    }
                    else if (pPanel.Controls[i].GetType().ToString() == "DevExpress.XtraEditors.SpinEdit")
                    {
                        ((DevExpress.XtraEditors.SpinEdit)pPanel.Controls[i]).Value = 0;
                    }
                    else if (pPanel.Controls[i].GetType().ToString() == "DevExpress.XtraEditors.CheckEdit")
                    {
                        ((DevExpress.XtraEditors.CheckEdit)pPanel.Controls[i]).Checked = false;
                    }
                    else if (pPanel.Controls[i].GetType().ToString() == "Hemo.Utilities.CustomGridLookUpEdit")
                    {
                        ((Hemo.Utilities.CustomGridLookUpEdit)pPanel.Controls[i]).EditValue = string.Empty;
                    }
                    else if (pPanel.Controls[i].GetType().ToString() == "DevExpress.XtraEditors.CheckedListBoxControl")
                    {
                        foreach (DevExpress.XtraEditors.Controls.CheckedListBoxItem item in ((DevExpress.XtraEditors.CheckedListBoxControl)pPanel.Controls[i]).Items)
                        {
                            item.CheckState = System.Windows.Forms.CheckState.Unchecked;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 设置容器中控件值是否可用
        /// </summary>
        /// <param name="pPanel">对应的容器</param>
        /// <param name="pValue">是否可用</param>
        public static void SetControlEnabled(DevExpress.XtraEditors.PanelControl pPanel, bool pValue)
        {
            if (pPanel.Controls.Count > 0)
            {
                for (int i = 0; i < pPanel.Controls.Count; i++)
                {
                    if (pPanel.Controls[i].GetType().ToString() == "DevExpress.XtraEditors.TextEdit" ||
                        pPanel.Controls[i].GetType().ToString() == "DevExpress.XtraEditors.ComboBoxEdit" ||
                        pPanel.Controls[i].GetType().ToString() == "DevExpress.XtraEditors.DateEdit" ||
                        pPanel.Controls[i].GetType().ToString() == "DevExpress.XtraEditors.MemoEdit" ||
                        pPanel.Controls[i].GetType().ToString() == "DevExpress.XtraEditors.LookUpEdit" ||
                        pPanel.Controls[i].GetType().ToString() == "DevExpress.XtraEditors.SpinEdit" ||
                        pPanel.Controls[i].GetType().ToString() == "DevExpress.XtraEditors.CheckedComboBoxEdit" ||
                        pPanel.Controls[i].GetType().ToString() == "DevExpress.XtraEditors.TimeEdit" ||
                        pPanel.Controls[i].GetType().ToString() == "DevExpress.XtraEditors.RadioGroup" ||
                        pPanel.Controls[i].GetType().ToString() == "Hemo.Utilities.CustomGridLookUpEdit" ||
                        pPanel.Controls[i].GetType().ToString() == "Hemo.Utilities.CustomGridLookUpEdit" ||
                        pPanel.Controls[i].GetType().ToString() == "DevExpress.XtraEditors.CheckEdit")
                    {
                        pPanel.Controls[i].Enabled = pValue;
                    }
                }
            }
        }

        /// <summary>
        /// 设置容器中控件值是否可用
        /// </summary>
        /// <param name="pPanel">对应的容器</param>
        /// <param name="pValue">是否可用</param>
        public static void SetControlEnabled(DevExpress.XtraEditors.XtraScrollableControl pPanel, bool pValue)
        {
            if (pPanel.Controls.Count > 0)
            {
                for (int i = 0; i < pPanel.Controls.Count; i++)
                {
                    if (pPanel.Controls[i].GetType().ToString() == "DevExpress.XtraEditors.TextEdit" ||
                        pPanel.Controls[i].GetType().ToString() == "DevExpress.XtraEditors.ComboBoxEdit" ||
                        pPanel.Controls[i].GetType().ToString() == "DevExpress.XtraEditors.DateEdit" ||
                        pPanel.Controls[i].GetType().ToString() == "DevExpress.XtraEditors.MemoEdit" ||
                        pPanel.Controls[i].GetType().ToString() == "DevExpress.XtraEditors.LookUpEdit" ||
                        pPanel.Controls[i].GetType().ToString() == "DevExpress.XtraEditors.SpinEdit" ||
                        pPanel.Controls[i].GetType().ToString() == "DevExpress.XtraEditors.CheckedComboBoxEdit" ||
                        pPanel.Controls[i].GetType().ToString() == "DevExpress.XtraEditors.TimeEdit" ||
                        pPanel.Controls[i].GetType().ToString() == "DevExpress.XtraEditors.RadioGroup" ||
                        pPanel.Controls[i].GetType().ToString() == "Hemo.Utilities.CustomGridLookUpEdit" ||
                        pPanel.Controls[i].GetType().ToString() == "Hemo.Utilities.CustomGridLookUpEdit" ||
                        pPanel.Controls[i].GetType().ToString() == "DevExpress.XtraEditors.CheckEdit")
                    {
                        pPanel.Controls[i].Enabled = pValue;
                    }
                }
            }
        }

        /// <summary>
        /// 设置容器中控件值是否可用
        /// </summary>
        /// <param name="pPanel">对应的容器</param>
        /// <param name="pValue">是否可用</param>
        public static void SetControlEnabled(DevExpress.XtraEditors.GroupControl pPanel, bool pValue)
        {
            if (pPanel.Controls.Count > 0)
            {
                for (int i = 0; i < pPanel.Controls.Count; i++)
                {
                    if (pPanel.Controls[i].GetType().ToString() == "DevExpress.XtraEditors.TextEdit" ||
                        pPanel.Controls[i].GetType().ToString() == "DevExpress.XtraEditors.ComboBoxEdit" ||
                        pPanel.Controls[i].GetType().ToString() == "DevExpress.XtraEditors.DateEdit" ||
                        pPanel.Controls[i].GetType().ToString() == "DevExpress.XtraEditors.MemoEdit" ||
                        pPanel.Controls[i].GetType().ToString() == "DevExpress.XtraEditors.LookUpEdit" ||
                        pPanel.Controls[i].GetType().ToString() == "DevExpress.XtraEditors.SpinEdit" ||
                        pPanel.Controls[i].GetType().ToString() == "DevExpress.XtraEditors.CheckedComboBoxEdit" ||
                        pPanel.Controls[i].GetType().ToString() == "DevExpress.XtraEditors.TimeEdit" ||
                        pPanel.Controls[i].GetType().ToString() == "DevExpress.XtraEditors.RadioGroup" ||
                        pPanel.Controls[i].GetType().ToString() == "Hemo.Utilities.CustomGridLookUpEdit" ||
                        pPanel.Controls[i].GetType().ToString() == "DevExpress.XtraEditors.LookUpEdit" ||
                        pPanel.Controls[i].GetType().ToString() == "DevExpress.XtraEditors.CheckEdit")
                    {
                        pPanel.Controls[i].Enabled = pValue;
                    }
                }
            }
        }

        /// <summary>
        /// 验证文本框控件的值是否为空并弹出提示信息
        /// </summary>
        /// <param name="pTextEdit">文本框控件</param>
        /// <param name="pMessage">提示信息</param>
        /// <param name="pCaption">标题</param>
        /// <returns>返回验证布尔值</returns>
        public static bool CheckTextEdit(DevExpress.XtraEditors.TextEdit pTextEdit, string pMessage, string pCaption)
        {
            if (pTextEdit.Text.Length == 0)
            {
                XtraMessageBox.Show(pMessage, pCaption);
                pTextEdit.Focus();
                return false;
            }
            return true;
        }

        public static bool CheckSpinEdit(DevExpress.XtraEditors.SpinEdit spinEdit, string pMessage, string pCaption)
        {
            if (spinEdit.Text.Length == 0 || spinEdit.Text == "0")
            {
                XtraMessageBox.Show(pMessage, pCaption);
                spinEdit.Focus();
                return false;
            }
            return true;
        }

        /// <summary>
        /// 验证日期控件的值是否为空并弹出提示信息
        /// </summary>
        /// <param name="pTextEdit">日期控件</param>
        /// <param name="pMessage">提示信息</param>
        /// <param name="pCaption">标题</param>
        /// <returns>返回验证布尔值</returns>
        public static bool CheckDateEdit(DevExpress.XtraEditors.DateEdit pDateEdit, string pMessage, string pCaption)
        {
            if (pDateEdit.Text.Length == 0)
            {
                XtraMessageBox.Show(pMessage, pCaption);
                pDateEdit.Focus();
                return false;
            }
            return true;
        }

        /// <summary>
        /// 验证LookUpEdit的值是否为空并弹出提示信息
        /// </summary>
        /// <param name="pTextEdit">下拉控件</param>
        /// <param name="pMessage">提示信息</param>
        /// <param name="pCaption">标题</param>
        /// <returns>返回验证布尔值</returns>
        public static bool CheckpLookUpEdit(DevExpress.XtraEditors.LookUpEdit pLookUpEdit, string pMessage, string pCaption)
        {
            if (pLookUpEdit.Text.Length == 0)
            {
                XtraMessageBox.Show(pMessage, pCaption);
                pLookUpEdit.Focus();
                return false;
            }
            return true;
        }

        /// <summary>
        /// 验证下拉控件的值是否为空并弹出提示信息
        /// </summary>
        /// <param name="pTextEdit">下拉控件</param>
        /// <param name="pMessage">提示信息</param>
        /// <param name="pCaption">标题</param>
        /// <returns>返回验证布尔值</returns>
        public static bool CheckComboBoxEdit(DevExpress.XtraEditors.ComboBoxEdit pComboBoxEdit, string pMessage, string pCaption)
        {
            if (pComboBoxEdit.Text.Length == 0)
            {
                XtraMessageBox.Show(pMessage, pCaption);
                pComboBoxEdit.Focus();
                return false;
            }
            return true;
        }

        /// <summary>
        /// 绑定下拉框控件
        /// </summary>
        /// <param name="lookUpEdit">控件</param>
        /// <param name="valueMember">VALUE值</param>
        /// <param name="displayMember">TEXT值</param>
        /// <param name="dataSource">数据源</param>
        /// <param name="fieldName">类型</param>
        /// <param name="caption">标题</param>
        public static void BindLookUpEdit(LookUpEdit lookUpEdit, string valueMember, string displayMember, DataTable dataSource, string fieldName, string caption)
        {
            lookUpEdit.Properties.ValueMember = valueMember;
            lookUpEdit.Properties.DisplayMember = displayMember;
            if (dataSource != null && dataSource.Rows.Count > 0)
            {
                //构建第一行空数据

                DataTable dt = dataSource.Copy();
                try
                {
                    dt.Columns[0].AllowDBNull = true;
                    DataRow dr = dt.NewRow();
                    dt.Rows.InsertAt(dr, 0);
                }
                catch
                { }
                lookUpEdit.Properties.DataSource = dt;
            }
            LookUpColumnInfoCollection colCollection1 = lookUpEdit.Properties.Columns;
            colCollection1.Clear();
            if (caption.Equals("血管通路") && !dataSource.TableName.Equals("MED_COMMON_ITEMLIST"))
            {
                lookUpEdit.Properties.PopupWidth = 300;
                colCollection1.Add(new LookUpColumnInfo("CREATE_DATE", 210, "通路日期"));
                colCollection1.Add(new LookUpColumnInfo(fieldName, 300, caption));
            }
            else
            {
                colCollection1.Add(new LookUpColumnInfo(fieldName, caption));
            }


            lookUpEdit.ItemIndex = 0;
            lookUpEdit.Properties.NullText = string.Empty;
        }
    }
}
