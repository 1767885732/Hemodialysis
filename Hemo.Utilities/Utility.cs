/*----------------------------------------------------------------
// Copyright (C) 2013 苏州XX公司有限公司
// 描述：通用验证、Datatable处理、身份证号码验证、字符处理类、XML转DataTable、DataTable转XML
// 创建时间：2013-03-12
// 创建者：刘超
//  
// 修改时间：
// 修改人：
// 修改描述：
//
// 修改时间：
// 修改人：
// 修改描述：
----------------------------------------------------------------*/
using System;
using System.Data;
using System.Drawing;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraEditors.Repository;
using System.Collections.Generic;
using System.Reflection;
using System.Xml;
using System.Linq;
using System.Xml.Serialization;
using Medicalsystem.Auth.Client;
using System.Configuration;
using Hemo.IService.Config;
using System.Runtime.InteropServices;

namespace Hemo.Utilities
{
    public class Utility
    {
        /// <summary>
        /// 特殊的XML字符数组对象
        /// </summary>
        private readonly static string[] SPECIAL_XML_STRING = new string[] { "&", "<", ">", "'", @"""" };

        /// <summary>
        /// 特殊的XML字符替换后的字符数组对象
        /// </summary>
        private readonly static string[] SPECIAL_XML_STRING_REPLACED = new string[] { "&amp;", "&lt;", "&gt;", "&apos;", @"&quot;" };

        #region 字符拆分
        public static string[] Split(string value, char key)
        {
            return value.Split(new char[] { key }, StringSplitOptions.RemoveEmptyEntries);
        }
        #endregion

        #region 整型数据验证
        public static bool IsInt(string value)
        {
            int result;

            return int.TryParse(value, out result);
        }

        public static double CDouble(string value)
        {
            double result;

            double.TryParse(value, out result);

            return result;
        }
        #endregion

        #region Bool安全转换
        public static bool CBool(string value)
        {
            bool result;

            if (bool.TryParse(value, out result))
                return result;

            return false;
        }
        #endregion

        #region 整型安全转换
        public static int CInt(string value)
        {
            int result;

            if (int.TryParse(value, out result))
                return result;

            return 0;
        }
        #endregion

        #region 日期安全转换
        public static DateTime CDate(string value)
        {
            DateTime result;

            if (DateTime.TryParse(value, out result))
                return result;

            return DateTime.MinValue;
        }
        #endregion

        #region Decimal安全转换
        public static decimal CDecimal(string value)
        {
            decimal result;

            if (decimal.TryParse(value, out result))
                return result;

            return result;
        }

        #endregion

        #region 验证中文字符串
        public static bool IsChineseChars(string source)
        {
            return Regex.IsMatch(source, @"^[\u4e00-\u9fa5]{0,}$", RegexOptions.IgnoreCase);
        }
        #endregion

        #region 验证邮箱验证邮箱
        /// <summary>
        /// 验证邮箱
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static bool IsEmail(string source)
        {
            return Regex.IsMatch(source, @"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$", RegexOptions.IgnoreCase);
        }
        #endregion

        #region 验证网址
        /// <summary>
        /// 验证网址
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static bool IsUrl(string source)
        {
            return Regex.IsMatch(source, @"^(((file|gopher|news|nntp|telnet|http|ftp|https|ftps|sftp)://)|(www\.))+(([a-zA-Z0-9\._-]+\.[a-zA-Z]{2,6})|([0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}))(/[a-zA-Z0-9\&amp;%_\./-~-]*)?$", RegexOptions.IgnoreCase);
        }
        #endregion

        #region 验证日期
        /// <summary>
        /// 验证日期
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static bool IsDateTime(string source)
        {
            try
            {
                DateTime time = Convert.ToDateTime(source);
                return true;
            }
            catch
            {
                return false;
            }
        }
        #endregion

        #region 验证手机号
        /// <summary>
        /// 验证手机号
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static bool IsMobile(string source)
        {
            return Regex.IsMatch(source, @"^1[35]\d{9}$", RegexOptions.IgnoreCase);
        }
        #endregion

        #region 验证IP
        /// <summary>
        /// 验证IP
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static bool IsIP(string source)
        {
            return Regex.IsMatch(source, @"^(25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[1-9])\.(25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[1-9]|0)\.(25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[1-9]|0)\.(25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[0-9])$", RegexOptions.IgnoreCase);
        }
        #endregion

        #region 验证身份证是否有效
        /**/
        /// <summary>
        /// 验证身份证是否有效
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public static bool IsIDCard(string Id)
        {
            if (Id.Length == 18)
            {
                bool check = IsIDCard18(Id);
                return check;
            }
            else if (Id.Length == 15)
            {
                bool check = IsIDCard15(Id);
                return check;
            }
            else
            {
                return false;
            }
        }

        public static bool IsIDCard18(string Id)
        {
            long n = 0;
            if (long.TryParse(Id.Remove(17), out n) == false || n < Math.Pow(10, 16) || long.TryParse(Id.Replace('x', '0').Replace('X', '0'), out n) == false)
            {
                return false;//数字验证
            }
            string address = "11x22x35x44x53x12x23x36x45x54x13x31x37x46x61x14x32x41x50x62x15x33x42x51x63x21x34x43x52x64x65x71x81x82x91";
            if (address.IndexOf(Id.Remove(2)) == -1)
            {
                return false;//省份验证
            }
            string birth = Id.Substring(6, 8).Insert(6, "-").Insert(4, "-");
            DateTime time = new DateTime();
            if (DateTime.TryParse(birth, out time) == false)
            {
                return false;//生日验证
            }
            string[] arrVarifyCode = ("1,0,x,9,8,7,6,5,4,3,2").Split(',');
            string[] Wi = ("7,9,10,5,8,4,2,1,6,3,7,9,10,5,8,4,2").Split(',');
            char[] Ai = Id.Remove(17).ToCharArray();
            int sum = 0;
            for (int i = 0; i < 17; i++)
            {
                sum += int.Parse(Wi[i]) * int.Parse(Ai[i].ToString());
            }
            int y = -1;
            Math.DivRem(sum, 11, out y);
            if (arrVarifyCode[y] != Id.Substring(17, 1).ToLower())
            {
                return false;//校验码验证
            }
            return true;//符合GB11643-1999标准
        }

        public static bool IsIDCard15(string Id)
        {
            long n = 0;
            if (long.TryParse(Id, out n) == false || n < Math.Pow(10, 14))
            {
                return false;//数字验证
            }
            string address = "11x22x35x44x53x12x23x36x45x54x13x31x37x46x61x14x32x41x50x62x15x33x42x51x63x21x34x43x52x64x65x71x81x82x91";
            if (address.IndexOf(Id.Remove(2)) == -1)
            {
                return false;//省份验证
            }
            string birth = Id.Substring(6, 6).Insert(4, "-").Insert(2, "-");
            DateTime time = new DateTime();
            if (DateTime.TryParse(birth, out time) == false)
            {
                return false;//生日验证
            }
            return true;//符合15位身份证标准
        }
        #endregion

        #region 看字符串的长度是不是在限定数之间 一个中文为两个字符
        /// <summary>
        /// 看字符串的长度是不是在限定数之间 一个中文为两个字符
        /// </summary>
        /// <param name="source">字符串</param>
        /// <param name="begin">大于等于</param>
        /// <param name="end">小于等于</param>
        /// <returns></returns>
        public static bool IsLengthStr(string source, int begin, int end)
        {
            int length = Regex.Replace(source, @"[^\x00-\xff]", "OK").Length;
            if ((length <= begin) && (length >= end))
            {
                return false;
            }
            return true;
        }
        #endregion

        #region 是不是中国电话，格式010-85849685
        /// <summary>
        /// 是不是中国电话，格式010-85849685
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static bool IsTel(string source)
        {
            return Regex.IsMatch(source, @"^\d{3,4}-?\d{6,8}$", RegexOptions.IgnoreCase);
        }
        #endregion

        #region 邮政编码 6个数字
        /// <summary>
        /// 邮政编码 6个数字
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static bool IsPostCode(string source)
        {
            return Regex.IsMatch(source, @"^\d{6}$", RegexOptions.IgnoreCase);
        }
        #endregion

        #region DataTable处理,抽取自表并按条件排序

        /// <summary>
        /// 对一个DataTable筛选排序，抽出子表
        /// </summary>
        /// <param name="pDt">源表</param>
        /// <param name="pStrSelect">查询条件</param>
        /// <returns>返回子表</returns>
        public static System.Data.DataTable GetSubTable(System.Data.DataTable pDt, string pStrSelect)
        {
            return GetSubTable(pDt, pStrSelect, null);
        }
        /// <summary>
        /// 对一个DataTable筛选排序，抽出子表
        /// </summary>
        /// <param name="pDt">源表</param>
        /// <param name="pStrSelect">查询条件</param>
        /// <param name="pStrSort">排序字段</param>
        /// <returns>返回子表</returns>
        public static System.Data.DataTable GetSubTable(System.Data.DataTable pDt, string pStrSelect, string pStrSort)
        {
            System.Data.DataTable dtRet = null;
            if (pDt != null)
            {
                dtRet = GetSubTable(pDt, pStrSelect, pStrSort, pDt.TableName);
            }
            return dtRet;
        }

        /// <summary>
        /// 对一个DataTable筛选排序，抽出子表
        /// </summary>
        /// <param name="pDt">源表</param>
        /// <param name="pStrSelect">查询条件</param>
        /// <param name="pStrSort">排序字段</param>
        /// <param name="pTableName">表名</param>
        /// <returns>返回子表</returns>
        public static System.Data.DataTable GetSubTable(System.Data.DataTable pDt, string pStrSelect, string pStrSort, string pTableName)
        {
            try
            {
                System.Data.DataTable retDt = null;
                if (pDt != null)
                {
                    System.Data.DataRow[] drRows = pDt.Select(pStrSelect, pStrSort);
                    if (drRows != null && drRows.Length > 0)
                    {
                        retDt = GetDataTableFromDataRow(drRows);
                    }
                    else
                    {
                        retDt = pDt.Clone();
                    }
                    retDt.TableName = pTableName;
                }
                return retDt;
            }
            catch (System.Exception ex)
            {
                //子表抽出失败！
                throw new System.Exception("子表抽出失败!", ex);
            }
        }

        /// <summary>
        /// 依据指定的DataRow集合，生成一个DataTable
        /// </summary>
        /// <param name="pDataRowCollection">指定的DataRow集合</param>
        /// <returns>获取的DataTable</returns>
        public static System.Data.DataTable GetDataTableFromDataRow(System.Data.DataRowCollection pDataRowCollection)
        {
            //提高效率
            System.Data.DataTable dtRet = null;
            if (pDataRowCollection != null && pDataRowCollection.Count > 0)
            {
                dtRet = pDataRowCollection[0].Table.Clone();
                dtRet.BeginLoadData();
                foreach (System.Data.DataRow drRow in pDataRowCollection)
                {
                    dtRet.ImportRow(drRow);
                }
                dtRet.EndLoadData();
            }
            return dtRet;
        }

        /// <summary>
        /// 依据指定的DataRow，生成一个DataTable
        /// </summary>
        /// <param name="pDataRow">指定的DataRow</param>
        /// <returns>获取的DataTable</returns>
        public static System.Data.DataTable GetDataTableFromDataRow(System.Data.DataRow pDataRow)
        {
            return GetDataTableFromDataRow(new System.Data.DataRow[] { pDataRow });
        }

        /// <summary>
        /// 依据指定的DataRow数组，生成一个DataTable
        /// </summary>
        /// <param name="pDataRows">指定的DataRow数组</param>
        /// <returns>获取的DataTable</returns>
        public static System.Data.DataTable GetDataTableFromDataRow(System.Data.DataRow[] pDataRows)
        {
            System.Data.DataTable dtRet = null;
            if (pDataRows != null && pDataRows.Length > 0)
            {
                dtRet = pDataRows[0].Table.Clone();
                dtRet.BeginLoadData();
                foreach (System.Data.DataRow drRow in pDataRows)
                {
                    dtRet.ImportRow(drRow);
                }
                dtRet.EndLoadData();
            }
            return dtRet;
        }

        #endregion

        #region DataTable转XML     XML转DataTable
        /// <summary>
        /// 将DataTable转化为XML字符串
        /// </summary>
        /// <param name="inDt">传入DataTable（带TableName）</param>
        /// <returns>返回XML字符串</returns>
        public static string Transfer_DataTable_To_XML(System.Data.DataTable inDt)
        {
            return Transfer_DataTable_To_XML(inDt, true);
        }
        /// <summary>
        /// 将DataTable转化为XML字符串
        /// </summary>
        /// <param name="inDt">传入DataTable（带TableName）</param>
        /// <param name="pIsWithoutSchema">是否带Schema结构</param>
        /// <returns>返回XML字符串</returns>
        public static string Transfer_DataTable_To_XML(System.Data.DataTable inDt, bool pIsWithoutSchema)
        {
            string rtnXML = "";
            if (inDt != null && inDt.Rows.Count > 0)
            {
                System.Xml.XmlDocument xmlDom = new System.Xml.XmlDocument();
                System.Data.DataSet xmlDS = new System.Data.DataSet("XMLInfo");
                xmlDS.Tables.Add(inDt.Copy());
                System.IO.StringWriter sw2 = new System.IO.StringWriter();
                if (pIsWithoutSchema)
                {
                    xmlDS.WriteXml(sw2, System.Data.XmlWriteMode.WriteSchema);
                }
                else
                {
                    xmlDS.WriteXml(sw2);
                }
                xmlDom.LoadXml(sw2.ToString());
                rtnXML = xmlDom.InnerXml;
            }
            return rtnXML;
        }
        /// <summary>
        /// 读取数据表的架构XML
        /// </summary>
        /// <param name="pDt"></param>
        /// <returns></returns>
        public static string GetDataTableStruture(System.Data.DataTable pDt)
        {
            string rtnXML = "";
            if (pDt.Rows.Count > 0)
            {
                pDt.Rows.Clear();
            }
            System.Xml.XmlDocument xmlDom = new System.Xml.XmlDocument();
            System.Data.DataSet xmlDS = new System.Data.DataSet("XMLInfo");
            xmlDS.Tables.Add(pDt.Copy());
            System.IO.StringWriter sw2 = new System.IO.StringWriter();
            xmlDS.WriteXml(sw2, System.Data.XmlWriteMode.WriteSchema);
            xmlDom.LoadXml(sw2.ToString());
            rtnXML = xmlDom.InnerXml;
            return rtnXML;
        }

        /// <summary>
        /// 将DataTable转化为XML字符串（将每列值都做为行属性处理）
        /// </summary>
        /// <param name="inDt"></param>
        /// <returns></returns>
        public static string Transfer_DataTable_To_LineXML(System.Data.DataTable inDt)
        {
            string rtnXML = string.Empty;
            StringBuilder sbXML = new StringBuilder();
            if (inDt != null && inDt.Rows.Count > 0)
            {
                sbXML.Append("<root>");
                for (int i = 0; i < inDt.Rows.Count; i++)
                {
                    sbXML.Append("<").Append(inDt.TableName);
                    for (int j = 0; j < inDt.Columns.Count; j++)
                    {
                        if (!string.IsNullOrEmpty(inDt.Rows[i][j].ToString().Trim()))
                        {
                            sbXML.Append(" ").Append(inDt.Columns[j].ColumnName).Append("=\"").Append(GetXMLValue(inDt.Rows[i][j].ToString()).Trim()).Append("\"");
                        }
                    }
                    sbXML.Append(" />");
                }
                sbXML.Append("</root>");
                rtnXML = sbXML.ToString();
            }
            return rtnXML;
        }
        /// <summary>
        /// 将DataTable转化为XML字符串（将每列值都做为行属性处理）
        /// </summary>
        /// <param name="inDt"></param>
        /// <returns></returns>
        public static string Transfer_DataRow_To_LineXML(System.Data.DataTable inDt)
        {
            string rtnXML = string.Empty;
            StringBuilder sbXML = new StringBuilder();
            if (inDt != null && inDt.Rows.Count > 0)
            {
                sbXML.Append("<root>");
                for (int i = 0; i < inDt.Rows.Count; i++)
                {
                    sbXML.Append("<").Append(inDt.TableName);
                    for (int j = 0; j < inDt.Columns.Count; j++)
                    {
                        if (!string.IsNullOrEmpty(inDt.Rows[i][j].ToString().Trim()))
                        {
                            sbXML.Append(" ").Append(inDt.Columns[j].ColumnName).Append("=\"").Append(inDt.Rows[i][j].ToString().Trim()).Append("\"");
                        }
                    }
                    sbXML.Append(" />");
                }
                sbXML.Append("</root>");
                rtnXML = sbXML.ToString();
            }
            return rtnXML;
        }

        /// <summary>
        /// 将DataTable转化为简化版XML字符串
        /// </summary>
        /// <param name="inDt">数据表</param>
        /// <returns></returns>
        public static string[] Transfer_DataTable_To_SimpleXML(System.Data.DataTable inDt)
        {
            return Transfer_DataTable_To_SimpleXML(inDt, false);
        }

        /// <summary>
        /// 将DataTable转化为简化版XML字符串
        /// </summary>
        /// <param name="inDt">数据表</param>
        /// <param name="pIsUnAuto">是否手动</param>
        /// <returns></returns>
        public static string[] Transfer_DataTable_To_SimpleXML(System.Data.DataTable inDt, bool pIsUnAuto)
        {
            string[] rtnArr = new string[] { "", "" };
            System.Data.DataTable pDt = new DataTable(inDt.TableName);
            if (inDt.Columns.Count > 0)
            {
                string strColumns = "";
                for (int i = 0; i < inDt.Columns.Count; i++)
                {
                    strColumns += "<A" + inDt.Columns[i].ColumnName.ToLower() + ">C" + (i + 1).ToString() + "</A" + inDt.Columns[i].ColumnName.ToLower() + ">";
                    inDt.Columns[i].ColumnName = "C" + (i + 1).ToString();
                    if (inDt.Columns[i].DataType == System.Type.GetType("System.DateTime"))
                    {
                        pDt.Columns.Add(inDt.Columns[i].ColumnName, System.Type.GetType("System.String"));
                    }
                    else
                    {
                        pDt.Columns.Add(inDt.Columns[i].ColumnName, inDt.Columns[i].DataType);
                    }
                }
                rtnArr[1] = strColumns;
                //复制表格
                for (int i = 0; i < inDt.Rows.Count; i++)
                {
                    DataRow drNew = pDt.NewRow();
                    for (int j = 0; j < inDt.Columns.Count; j++)
                    {
                        if (inDt.Columns[j].DataType == System.Type.GetType("System.DateTime"))
                        {
                            if (!string.IsNullOrEmpty(inDt.Rows[i][j].ToString()))
                            {
                                drNew[j] = CDate(inDt.Rows[i][j].ToString()).ToShortDateString();
                            }
                        }
                        else
                        {
                            drNew[j] = inDt.Rows[i][j];
                        }
                    }
                    pDt.Rows.Add(drNew);
                }
            }
            if (pIsUnAuto)
            {
                rtnArr[0] = Transfer_DataTable_To_XML_NoAuto(pDt);
            }
            else
            {
                rtnArr[0] = Transfer_DataTable_To_XML(pDt, false);
            }
            pDt.Dispose();
            inDt.Dispose();
            return rtnArr;
        }

        /// <summary>
        /// 手动拼接 将DataTable转化为XML字符串
        /// </summary>
        /// <param name="pDt"></param>
        /// <returns></returns>
        public static string Transfer_DataTable_To_XML_NoAuto(DataTable pDt)
        {
            string rtnXML = string.Empty;
            if (pDt != null && pDt.Rows.Count > 0)
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("<XMLInfo>");
                for (int i = 0; i < pDt.Rows.Count; i++)
                {
                    sb.Append("<" + pDt.TableName + ">");
                    for (int j = 0; j < pDt.Columns.Count; j++)
                    {
                        if (string.IsNullOrEmpty(pDt.Rows[i][j].ToString()))
                        {
                            sb.Append("<").Append(pDt.Columns[j].ColumnName).Append(" />");
                        }
                        else
                        {
                            string strVal = pDt.Rows[i][j].ToString();
                            sb.Append("<").Append(pDt.Columns[j].ColumnName).Append(">");
                            if (strVal.Contains("<") || strVal.Contains(">") || strVal.Contains("&") || strVal.Contains("%"))
                            {
                                sb.Append("<![CDATA[");
                                sb.Append(strVal);
                                sb.Append("]]>");
                            }
                            else
                            {
                                sb.Append(strVal);
                            }
                            sb.Append("</").Append(pDt.Columns[j].ColumnName).Append(">");
                        }
                    }
                    sb.Append("</" + pDt.TableName + ">");
                }
                sb.Append("</XMLInfo>");
                rtnXML = sb.ToString();
            }
            return rtnXML;
        }


        /// <summary>
        /// 将XML字符串转化为DataTable
        /// </summary>
        /// <param name="xmlStr">传入XML字符串</param>
        /// <returns>返回DataTable</returns>
        public static System.Data.DataTable Transfer_XML_To_DataTable(string xmlStr)
        {
            System.Data.DataTable rtnDT = null;
            if (!string.IsNullOrEmpty(xmlStr))
            {
                try
                {
                    System.IO.StringReader sr = new System.IO.StringReader(xmlStr);
                    System.Data.DataSet loadDt = new System.Data.DataSet();
                    //loadDt.ReadXml(sr, System.Data.XmlReadMode.ReadSchema);
                    if (loadDt == null || loadDt.Tables.Count == 0)
                    {
                        loadDt.ReadXml(sr);
                    }
                    if (loadDt.Tables.Count == 1)
                    {
                        rtnDT = loadDt.Tables[0].Copy();
                    }
                }
                catch (System.Exception ex)
                {
                    throw ex;
                }
            }
            return rtnDT;
        }

        /// <summary>
        /// 转换Dataset到xml字符串
        /// </summary>
        /// <param name="inDs"></param>
        /// <returns></returns>
        public static string Transfer_DataSet_To_XML(System.Data.DataSet inDs)
        {
            string rtnXML = "";
            if (inDs != null && inDs.Tables.Count > 0)
            {
                if (string.IsNullOrEmpty(inDs.DataSetName)) inDs.DataSetName = "XMLInfo";
                System.IO.StringWriter sw2 = new System.IO.StringWriter();
                inDs.WriteXml(sw2, System.Data.XmlWriteMode.IgnoreSchema);
                rtnXML = sw2.ToString();
            }
            return rtnXML;
        }

        /// <summary>
        /// 转换XML到DataSet
        /// </summary>
        /// <param name="pXMLStr"></param>
        /// <returns></returns>
        public static DataSet Transfer_XML_To_DataSet(string pXMLStr)
        {
            System.Data.DataSet rtnDs = new DataSet();
            if (!string.IsNullOrEmpty(pXMLStr))
            {
                try
                {
                    System.IO.StringReader sr = new System.IO.StringReader(pXMLStr);
                    rtnDs.ReadXml(sr, System.Data.XmlReadMode.ReadSchema);
                    if (rtnDs == null || rtnDs.Tables.Count == 0)
                    {
                        rtnDs.ReadXml(sr);
                    }
                }
                catch (System.Exception ex)
                {
                    throw ex;
                }
            }
            return rtnDs;
        }


        /// <summary>
        /// 获取的电话号码类型
        /// </summary>
        /// <param name="pPhoneNumber">电话号码</param>
        /// <returns>0 为手机号码  1为电话号码</returns>
        public static string GetTelType(string pPhoneNumber)
        {
            string strRet = "1";
            string[] strTemp = pPhoneNumber.Split('-');
            if (strTemp.Length >= 2)
            {
                pPhoneNumber = strTemp[1];
            }
            if (pPhoneNumber.Length >= 11)
            {
                strRet = "0";
            }
            return strRet;
        }

        /// <summary>
        /// 依据指定的值，获取XML对应字符串
        /// </summary>
        /// <param name="pValue">指定的值</param>
        /// <returns>获取的XML对应字符串</returns>
        public static string GetXMLValue(string pValue)
        {
            string strRet = pValue;
            if (!System.String.IsNullOrEmpty(pValue))
            {
                if (SPECIAL_XML_STRING != null && SPECIAL_XML_STRING.Length > 0 && SPECIAL_XML_STRING_REPLACED != null && SPECIAL_XML_STRING_REPLACED.Length > 0)
                {
                    for (int i = 0; i < SPECIAL_XML_STRING.Length; i++)
                    {
                        if (!System.String.IsNullOrEmpty(SPECIAL_XML_STRING[i]) && pValue.Contains(SPECIAL_XML_STRING[i]) && SPECIAL_XML_STRING_REPLACED.Length > i)
                        {
                            strRet = strRet.Replace(SPECIAL_XML_STRING[i], SPECIAL_XML_STRING_REPLACED[i]);
                        }
                    }
                }
            }
            return strRet;
        }
        #endregion

        #region Bitmap转byte[]
        public static byte[] BitmapToBytes(Bitmap Bitmap)
        {
            MemoryStream ms = null;
            try
            {
                ms = new MemoryStream();
                Bitmap.Save(ms, Bitmap.RawFormat);
                byte[] byteImage = new Byte[ms.Length];
                byteImage = ms.ToArray();
                return byteImage;
            }
            catch (ArgumentNullException ex)
            {
                throw ex;
            }
            finally
            {
                ms.Close();
            }
        }

        #endregion

        #region [加密方法]
        /// <summary>
        /// 加密方法
        /// </summary>
        /// <param name="Source">待加密的串</param>
        /// <returns>经过加密的串</returns>
        public static string Encrypto(string Source)
        {
            byte[] bt = UTF8Encoding.UTF8.GetBytes(Source);//UTF8需要对Text的引用
            MD5CryptoServiceProvider objMD5;
            objMD5 = new MD5CryptoServiceProvider();
            byte[] output = objMD5.ComputeHash(bt);

            string[] password = BitConverter.ToString(output).Split(new char[] { '-' });
            string returnValue = "";
            for (int index = 0; index < password.Length; index++)
                returnValue += password[index];
            returnValue = returnValue.ToUpper();
            return returnValue;
        }
        #endregion


        #region 根据出生日期计算年龄
        public static int GetAge(string pBirthday)
        {
            int result = 0;
            if (pBirthday.Length > 0)
            {
                int m_Y1 = DateTime.Parse(pBirthday).Year;
                int m_Y2 = DateTime.Now.Year;
                int m_Age = m_Y2 - m_Y1;
                result = m_Age;
            }
            return result;
        }
        public static int GetAgeByCureDate(string pBirthday, string pCureDate)
        {
            int result = 0;
            if (pBirthday.Length > 0)
            {
                int m_Y1 = DateTime.Parse(pBirthday).Year;
                int m_Y2 = DateTime.Parse(pCureDate).Year;
                int m_Age = m_Y2 - m_Y1;
                result = m_Age;
            }
            return result;
        }

        /// <summary>
        /// 根据日期获取透析龄
        /// </summary>
        /// <param name="firtHemoDt">首次透析时间</param>
        /// <param name="LastCureDt">最后透析时间</param>
        /// <returns></returns>
        public static string GetPatientHemoAge(DateTime firtHemoDt, DateTime LastCureDt)
        {
            string strAge = string.Empty; // 透析年龄的字符串表示
            int intYear = 0; // 年 
            int intMonth = 0; // 月
            int intDay = 0; // 天

            // 如果没有设定出生日期, 返回空
            if (DateTime.MinValue == firtHemoDt || string.IsNullOrEmpty(firtHemoDt.ToString()))
            {
                return string.Empty;
            }

            // 计算天数
            intDay = LastCureDt.Day - firtHemoDt.Day;
            if (intDay < 0)
            {
                LastCureDt = LastCureDt.AddMonths(-1);
                intDay += DateTime.DaysInMonth(LastCureDt.Year, LastCureDt.Month);
            }

            // 计算月数
            intMonth = LastCureDt.Month - firtHemoDt.Month;
            if (intMonth < 0)
            {
                intMonth += 12;
                LastCureDt = LastCureDt.AddYears(-1);
            }

            // 计算年数
            intYear = LastCureDt.Year - firtHemoDt.Year;

            // 格式化年龄输出
            if (intYear >= 1) // 年份输出
            {
                strAge = intYear.ToString() + "年";
            }

            if (intMonth > 0) // 五岁以下可以输出月数 && intYear <= 1
            {
                strAge += intMonth.ToString() + "月";
            }

            if (intDay >= 0) // 一岁以下可以输出天数  && intYear < 1
            {
                if (strAge.Length == 0 || intDay > 0)
                {
                    strAge += intDay.ToString() + "天";
                }
            }
            if (strAge.Equals("0天"))
            {
                strAge = "1天";
            }
            return strAge;
        }
        #endregion


        public static string GetRelativePath(string name)
        {
            name = "Doc\\" + name;
            string path = System.Windows.Forms.Application.StartupPath;
            string s = "\\";
            //if (!System.IO.File.Exists(path + s + name))
            //{
            //    File.Create(path + s + name);
            //}
            return path + s + name;
        }

        /// <summary>
        /// 获取星期一的日期
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static DateTime GetMonday(DateTime dt)
        {
            if (dt.DayOfWeek == DayOfWeek.Sunday)
                return dt.AddDays(-6);
            else
                return dt.AddDays(1 - (int)dt.DayOfWeek);
        }

        /// <summary>
        /// 绑定LookUpEdit控件
        /// </summary>
        /// <param name="lookUpEdit"></param>
        /// <param name="valueMember"></param>
        /// <param name="displayMember"></param>
        /// <param name="dataSource"></param>
        /// <param name="viewFieldName"></param>
        /// <param name="viewCaption"></param>
        public static void BindLookUpEdit(LookUpEdit lookUpEdit, string valueMember, string displayMember, DataTable dataSource, string viewFieldName, string viewCaption)
        {
            lookUpEdit.Properties.ValueMember = valueMember;
            lookUpEdit.Properties.DisplayMember = displayMember;
            lookUpEdit.Properties.DataSource = dataSource;

            LookUpColumnInfoCollection colCollection1 = lookUpEdit.Properties.Columns;
            colCollection1.Clear();
            colCollection1.Add(new LookUpColumnInfo(viewFieldName, viewCaption));

            lookUpEdit.ItemIndex = 0;

            lookUpEdit.Properties.NullText = string.Empty;
        }

        /// <summary>
        /// 绑定RepositoryItemLookUpEdit控件
        /// </summary>
        /// <param name="lookUpEdit"></param>
        /// <param name="valueMember"></param>
        /// <param name="displayMember"></param>
        /// <param name="dataSource"></param>
        /// <param name="viewFieldName"></param>
        /// <param name="viewCaption"></param>
        public static void BindLookUpEdit(RepositoryItemLookUpEdit lookUpEdit, string valueMember, string displayMember, DataTable dataSource, string viewFieldName, string viewCaption)
        {
            lookUpEdit.ValueMember = valueMember;
            lookUpEdit.DisplayMember = displayMember;
            lookUpEdit.DataSource = dataSource;

            LookUpColumnInfoCollection colCollection1 = lookUpEdit.Columns;
            colCollection1.Clear();
            colCollection1.Add(new LookUpColumnInfo(viewFieldName, viewCaption));


            lookUpEdit.NullText = string.Empty;
        }

        /// <summary>
        /// 为DataTable增加行
        /// </summary>
        /// <param name="lookUpEdit"></param>
        /// <param name="defaultStr4Sel"></param>
        public static void AddEmptyItem(LookUpEdit lookUpEdit, string defaultStr4Sel)
        {
            DataTable dt = lookUpEdit.Properties.DataSource as DataTable;

            DataRow row = dt.NewRow();
            row["ITEM_ID"] = string.Empty;
            row["ITEM_NAME"] = defaultStr4Sel;

            dt.Rows.InsertAt(row, 0);
        }

        /// <summary>
        ///  为DataTable增加行
        /// </summary>
        /// <param name="lookUpEdit"></param>
        /// <param name="defaultStr4Sel"></param>
        public static void AddEmptyItem(RepositoryItemLookUpEdit lookUpEdit, string defaultStr4Sel)
        {
            DataTable dt = lookUpEdit.DataSource as DataTable;

            DataRow row = dt.NewRow();
            row["ITEM_ID"] = string.Empty;
            row["ITEM_NAME"] = defaultStr4Sel;

            dt.Rows.InsertAt(row, 0);
        }

        /// <summary>
        /// 构造并返回班次Datatable
        /// </summary>
        /// <returns></returns>
        public static DataTable GetBanCi()
        {
            DataTable dtBANCI = new DataTable();
            dtBANCI.Columns.Add(new DataColumn("ITEM_ID"));
            dtBANCI.Columns.Add(new DataColumn("ITEM_NAME"));

            DataRow row = dtBANCI.NewRow();
            row["ITEM_ID"] = "0";
            row["ITEM_NAME"] = " 全部";
            dtBANCI.Rows.Add(row);

            row = dtBANCI.NewRow();
            row["ITEM_ID"] = "1";
            row["ITEM_NAME"] = "上午";
            dtBANCI.Rows.Add(row);

            row = dtBANCI.NewRow();
            row["ITEM_ID"] = "2";
            row["ITEM_NAME"] = "下午";
            dtBANCI.Rows.Add(row);

            //row = dtBANCI.NewRow();
            //row["ITEM_ID"] = "3";
            //row["ITEM_NAME"] = "晚班";
            //dtBANCI.Rows.Add(row);

            row = dtBANCI.NewRow();
            row["ITEM_ID"] = "4";
            row["ITEM_NAME"] = "急诊";
            dtBANCI.Rows.Add(row);
            return dtBANCI;
        }

        /// <summary>
        /// 通过分钟计算小时
        /// </summary>
        /// <param name="pMinute">分钟</param>
        /// <returns></returns>
        public static string GetHoursByMinute(string pMinute)
        {
            decimal totalSeconds = CDecimal(pMinute) * 60;
            decimal Minute = totalSeconds / 60 / 60;
            return Minute.ToString();
        }

        /// <summary>
        /// 通过小时计算分钟
        /// </summary>
        /// <param name="pMinute">分钟</param>
        /// <returns></returns>
        public static string GetMinuteByHours(string pHours)
        {
            decimal Hours = CDecimal("0." + pHours) * 60;
            return Hours.ToString();
        }

        #region dataTable转换成Json带返回是否成功标记
        public static string DataTableToJsonBySuccess(DataTable dt)
        {
            StringBuilder jsonBuilder = new StringBuilder();
            if (dt != null && dt.Rows.Count > 0)
            {

                jsonBuilder.Append("{\"SUCCESS\":\"TRUE\",\"");
                jsonBuilder.Append(dt.TableName);
                jsonBuilder.Append("\":[");
                // jsonBuilder.Append("[");
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    jsonBuilder.Append("{");
                    for (int j = 0; j < dt.Columns.Count; j++)
                    {
                        jsonBuilder.Append("\"");
                        jsonBuilder.Append(dt.Columns[j].ColumnName);
                        jsonBuilder.Append("\":\"");
                        jsonBuilder.Append(dt.Rows[i][j].ToString());
                        jsonBuilder.Append("\",");
                    }
                    jsonBuilder.Remove(jsonBuilder.Length - 1, 1);
                    jsonBuilder.Append("},");
                }
                jsonBuilder.Remove(jsonBuilder.Length - 1, 1);
                jsonBuilder.Append("]");
                jsonBuilder.Append("}");
            }
            else
            {
                return "{\"SUCCESS\":\"FALSE\"}";
            }
            return jsonBuilder.ToString();
        }
        #endregion

        #region dataTable转换成Json格式
        /// <summary>  
        /// dataTable转换成Json格式  
        /// </summary>  
        /// <param name="dt"></param>  
        /// <returns></returns>  
        public static string DataTableToJson(DataTable dt)
        {
            StringBuilder jsonBuilder = new StringBuilder();
            jsonBuilder.Append("{\"");
            jsonBuilder.Append(dt.TableName);
            jsonBuilder.Append("\":[");
            //    jsonBuilder.Append("[");
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                jsonBuilder.Append("{");
                for (int j = 0; j < dt.Columns.Count; j++)
                {
                    jsonBuilder.Append("\"");
                    jsonBuilder.Append(dt.Columns[j].ColumnName);
                    jsonBuilder.Append("\":\"");
                    jsonBuilder.Append(dt.Rows[i][j].ToString());
                    jsonBuilder.Append("\",");
                }
                jsonBuilder.Remove(jsonBuilder.Length - 1, 1);
                jsonBuilder.Append("},");
            }
            jsonBuilder.Remove(jsonBuilder.Length - 1, 1);
            jsonBuilder.Append("]");
            jsonBuilder.Append("}");
            return jsonBuilder.ToString();
        }

        #endregion dataTable转换成Json格式

        #region dataTable转换成xml
        /// <summary> 
        /// 序列化DataTable 
        /// </summary> 
        /// <param name="pDt">包含数据的DataTable</param> 
        /// <returns>序列化的DataTable</returns> 
        public static string DataTableToXml(DataTable pDt)
        {
            StringBuilder sb = new StringBuilder();
            XmlWriter writer = XmlWriter.Create(sb);
            XmlSerializer serializer = new XmlSerializer(typeof(DataTable));
            serializer.Serialize(writer, pDt);
            writer.Close();
            return sb.ToString();
        }
        #endregion dataTable转换成xml

        #region dataTable转换成object
        public static List<T> ConvertTableToObject<T>(DataTable t) where T : new()
        {
            List<T> list = new List<T>();
            foreach (DataRow row in t.Rows)
            {
                T obj = ConvertToObject<T>(row);
                list.Add(obj);
            }

            return list;
        }

        /// <summary>
        /// DataTable行转列
        /// </summary>
        /// <param name="dtable">需要转换的表</param>
        /// <param name="head">转换表表头对应旧表字段（小写）</param>
        /// <returns></returns>
        public static DataTable DataTableRowtoColumn(DataTable dtable, string head)
        {
            var rows = dtable.AsEnumerable().Where(row => !String.IsNullOrEmpty(row[head].ToString()) && !String.IsNullOrEmpty(row["RESULT"].ToString()));
            if (rows == null || rows.Count() == 0)
            {
                return null;
            }

            dtable = rows.CopyToDataTable();
            var dtTestNo = dtable.DefaultView.ToTable(true, "TEST_NO");
            var dtItemName = dtable.DefaultView.ToTable(true, "REPORT_ITEM_NAME", "UNITS");

            DataTable dtLabResult = new DataTable();
            dtLabResult.Columns.Add("检验日期", typeof(System.String));

            //创建表头
            foreach (DataRow row in dtItemName.Rows)
            {
                if (row["UNITS"].ToString() != string.Empty)
                {
                    if (!dtLabResult.Columns.Contains(row["REPORT_ITEM_NAME"].ToString() + "(" + row["UNITS"].ToString() + ")"))
                    {
                        DataColumn column = new DataColumn(row["REPORT_ITEM_NAME"].ToString() + "(" + row["UNITS"].ToString() + ")", typeof(System.String));
                        column.ExtendedProperties.Add("HasUnit", true);
                        dtLabResult.Columns.Add(column);
                    }
                }
                else
                {
                    if (!dtLabResult.Columns.Contains(row["REPORT_ITEM_NAME"].ToString()))
                    {
                        DataColumn column = new DataColumn(row["REPORT_ITEM_NAME"].ToString(), typeof(System.String));
                        column.ExtendedProperties.Add("HasUnit", false);
                        dtLabResult.Columns.Add(column);
                    }
                }
            }

            //填充表数据
            foreach (DataRow row in dtTestNo.Rows)
            {
                var r = dtLabResult.NewRow();
                foreach (DataColumn col in dtLabResult.Columns)
                {
                    var temp = dtable.AsEnumerable().FirstOrDefault(c => c["TEST_NO"].ToString().Equals(row["TEST_NO"].ToString()));
                    if (col.ColumnName.Equals("检验日期"))
                    {
                        r[col.ColumnName] = temp["RESULTS_RPT_DATE_TIME"].ToString();
                    }
                    else
                    {
                        string name = ((bool)col.ExtendedProperties["HasUnit"]) ? col.ColumnName.Substring(0, col.ColumnName.LastIndexOf("(")) : col.ColumnName;
                        temp = dtable.AsEnumerable().FirstOrDefault(c => c["TEST_NO"].ToString().Equals(row["TEST_NO"].ToString()) && c["REPORT_ITEM_NAME"].ToString().Equals(name));
                        if (temp != null)
                        {
                            r[col.ColumnName] = temp["RESULT"].ToString();
                        }
                    }
                }
                dtLabResult.Rows.Add(r);
            }

            dtLabResult.DefaultView.Sort = "检验日期 DESC";
            return dtLabResult;
        }



        /// <summary>
        /// DataTable行转列
        /// </summary>
        /// <param name="dtable">需要转换的表</param>
        /// <param name="head">转换表表头对应旧表字段（小写）</param>
        /// <returns></returns>
        public static DataTable DataTableRowtoColumnForQuality(DataTable dtable, string head)
        {
            var rows = dtable.AsEnumerable().Where(row => !String.IsNullOrEmpty(row[head].ToString()) && !String.IsNullOrEmpty(row["RESULT"].ToString()));
            if (rows == null || rows.Count() == 0)
            {
                return null;
            }

            dtable = rows.CopyToDataTable();
            var dtTestNo = dtable.DefaultView.ToTable(true, "TEST_NO");
            var dtItemName = dtable.DefaultView.ToTable(true, "REPORT_ITEM_NAME", "UNITS");

            DataTable dtLabResult = new DataTable();
            dtLabResult.Columns.Add("透析号", typeof(System.String));
            dtLabResult.Columns.Add("姓名", typeof(System.String));
            dtLabResult.Columns.Add("性别", typeof(System.String));
            dtLabResult.Columns.Add("病人号", typeof(System.String));
            dtLabResult.Columns.Add("单号", typeof(System.String));
            dtLabResult.Columns.Add("检验日期", typeof(System.String));



            //创建表头
            foreach (DataRow row in dtItemName.Rows)
            {

                if (row["UNITS"].ToString() != string.Empty)
                {
                    if (!dtLabResult.Columns.Contains(row["REPORT_ITEM_NAME"].ToString() + "(" + row["UNITS"].ToString() + ")"))
                    {
                        DataColumn column = new DataColumn(row["REPORT_ITEM_NAME"].ToString() + "(" + row["UNITS"].ToString() + ")", typeof(System.String));
                        column.ExtendedProperties.Add("HasUnit", true);
                        dtLabResult.Columns.Add(column);
                    }
                }
                else
                {
                    if (!dtLabResult.Columns.Contains(row["REPORT_ITEM_NAME"].ToString()))
                    {
                        DataColumn column = new DataColumn(row["REPORT_ITEM_NAME"].ToString(), typeof(System.String));
                        column.ExtendedProperties.Add("HasUnit", false);
                        dtLabResult.Columns.Add(column);
                    }
                }
                //if (!dtLabResult.Columns.Contains(row["REPORT_ITEM_NAME"].ToString() + "(" + row["UNITS"].ToString() + ")"))
                //{
                //    dtLabResult.Columns.Add(row["REPORT_ITEM_NAME"].ToString() + "(" + (row["UNITS"].ToString() == "" ? "参考" : row["UNITS"].ToString()) + ")", typeof(System.String));
                //}
            }

            //填充表数据
            foreach (DataRow row in dtTestNo.Rows)
            {
                var r = dtLabResult.NewRow();
                foreach (DataColumn col in dtLabResult.Columns)
                {
                    var temp = dtable.AsEnumerable().FirstOrDefault(c => c["TEST_NO"].ToString().Equals(row["TEST_NO"].ToString()));
                    if (col.ColumnName.Equals("检验日期"))
                    {
                        r[col.ColumnName] = temp["RESULTS_RPT_DATE_TIME"].ToString();
                    }
                    else if (col.ColumnName.Equals("透析号"))
                    {
                        r[col.ColumnName] = temp["HEMODIALYSIS_ID"].ToString();
                    }
                    else if (col.ColumnName.Equals("姓名"))
                    {
                        r[col.ColumnName] = temp["NAME"].ToString();
                    }
                    else if (col.ColumnName.Equals("性别"))
                    {
                        r[col.ColumnName] = temp["SEX"].ToString();
                    }
                    else if (col.ColumnName.Equals("病人号"))
                    {
                        r[col.ColumnName] = temp["PATIENT_ID"].ToString();
                    }
                    else if (col.ColumnName.Equals("单号"))
                    {
                        r[col.ColumnName] = temp["TEST_NO"].ToString();
                    }
                    else
                    {
                        string name = ((bool)col.ExtendedProperties["HasUnit"]) ? col.ColumnName.Substring(0, col.ColumnName.LastIndexOf("(")) : col.ColumnName;
                        temp = dtable.AsEnumerable().FirstOrDefault(c => c["TEST_NO"].ToString().Equals(row["TEST_NO"].ToString()) && c["REPORT_ITEM_NAME"].ToString().Equals(name));
                        if (temp != null)
                        {
                            r[col.ColumnName] = temp["RESULT"].ToString();
                        }
                    }
                }
                dtLabResult.Rows.Add(r);
            }
            dtLabResult.DefaultView.Sort = "检验日期 DESC";
            return dtLabResult;
        }

        /// <summary>
        /// DataTable行转列
        /// </summary>
        /// <param name="dtable">需要转换的表</param>
        /// <param name="head">转换表表头对应旧表字段（小写）</param>
        /// <returns></returns>
        public static DataTable DataTableRowtoColumnForQuality(DataTable dtable, string head, DataTable dtTitle)
        {
            var rows = dtable.AsEnumerable().Where(row => !String.IsNullOrEmpty(row[head].ToString()) && !String.IsNullOrEmpty(row["RESULT"].ToString()));
            if (rows == null || rows.Count() == 0)
            {
                return null;
            }

            dtable = rows.CopyToDataTable();
            var dtTestNo = dtable.DefaultView.ToTable(true, "TEST_NO");
            var dtItemName = dtable.DefaultView.ToTable(true, "REPORT_ITEM_NAME", "UNITS");

            DataTable dtLabResult = new DataTable();
            dtLabResult.Columns.Add("透析号", typeof(System.String));
            dtLabResult.Columns.Add("姓名", typeof(System.String));
            dtLabResult.Columns.Add("性别", typeof(System.String));
            dtLabResult.Columns.Add("病人号", typeof(System.String));
            dtLabResult.Columns.Add("单号", typeof(System.String));
            dtLabResult.Columns.Add("检验日期", typeof(System.String));

            //创建表头
            foreach (DataRow row in dtItemName.Rows)
            {

                if (row["UNITS"].ToString() != string.Empty)
                {
                    if (!dtLabResult.Columns.Contains(row["REPORT_ITEM_NAME"].ToString() + "(" + row["UNITS"].ToString() + ")"))
                    {
                        DataColumn column = new DataColumn(row["REPORT_ITEM_NAME"].ToString() + "(" + row["UNITS"].ToString() + ")", typeof(System.String));
                        column.ExtendedProperties.Add("HasUnit", true);
                        dtLabResult.Columns.Add(column);
                    }
                }
                else
                {
                    if (!dtLabResult.Columns.Contains(row["REPORT_ITEM_NAME"].ToString()))
                    {
                        DataColumn column = new DataColumn(row["REPORT_ITEM_NAME"].ToString(), typeof(System.String));
                        column.ExtendedProperties.Add("HasUnit", false);
                        dtLabResult.Columns.Add(column);
                    }
                }
            }

            //填充表数据
            foreach (DataRow row in dtTestNo.Rows)
            {
                var r = dtLabResult.NewRow();
                foreach (DataColumn col in dtLabResult.Columns)
                {
                    var temp = dtable.AsEnumerable().FirstOrDefault(c => c["TEST_NO"].ToString().Equals(row["TEST_NO"].ToString()));
                    if (col.ColumnName.Equals("检验日期"))
                    {
                        r[col.ColumnName] = temp["RESULTS_RPT_DATE_TIME"].ToString();
                    }
                    else if (col.ColumnName.Equals("透析号"))
                    {
                        r[col.ColumnName] = temp["HEMODIALYSIS_ID"].ToString();
                    }
                    else if (col.ColumnName.Equals("姓名"))
                    {
                        r[col.ColumnName] = temp["NAME"].ToString();
                    }
                    else if (col.ColumnName.Equals("性别"))
                    {
                        r[col.ColumnName] = temp["SEX"].ToString();
                    }
                    else if (col.ColumnName.Equals("病人号"))
                    {
                        r[col.ColumnName] = temp["PATIENT_ID"].ToString();
                    }
                    else if (col.ColumnName.Equals("单号"))
                    {
                        r[col.ColumnName] = temp["TEST_NO"].ToString();
                    }
                    else
                    {
                        string name = ((bool)col.ExtendedProperties["HasUnit"]) ? col.ColumnName.Substring(0, col.ColumnName.LastIndexOf("(")) : col.ColumnName;
                        temp = dtable.AsEnumerable().FirstOrDefault(c => c["TEST_NO"].ToString().Equals(row["TEST_NO"].ToString()) && c["REPORT_ITEM_NAME"].ToString().Equals(name));
                        if (temp != null)
                        {
                            r[col.ColumnName] = temp["RESULT"].ToString();
                        }
                    }
                }
                dtLabResult.Rows.Add(r);
            }
            dtLabResult.DefaultView.Sort = "检验日期 DESC";
            return dtLabResult;
        }


        /// <summary>
        /// DataTable行转列
        /// </summary>
        /// <param name="dtable">需要转换的表</param>
        /// <param name="head">转换表表头对应旧表字段（小写）</param>
        /// <returns></returns>
        public static DataTable DataTableRowtoColumnForQualityNew(DataTable dtable, DataTable dtTitle)
        {
            if (dtable == null || dtable.Rows.Count == 0)
            {
                return null;
            }

            var dtItemName = dtable.DefaultView.ToTable(true, "REPORT_ITEM_NAME", "UNITS");

            DataTable dtLabResult = new DataTable();
            dtLabResult.Columns.Add("透析号", typeof(System.String));
            dtLabResult.Columns.Add("姓名", typeof(System.String));
            dtLabResult.Columns.Add("性别", typeof(System.String));
            dtLabResult.Columns.Add("病人号", typeof(System.String));
            dtLabResult.Columns.Add("单号", typeof(System.String));
            dtLabResult.Columns.Add("检验日期", typeof(System.String));
            dtLabResult.Columns.Add("EXT_XML", typeof(System.String));


            //创建表头
            foreach (DataRow row in dtItemName.Rows)
            {

                if (row["UNITS"].ToString() != string.Empty)
                {
                    if (!dtLabResult.Columns.Contains(row["REPORT_ITEM_NAME"].ToString() + "(" + row["UNITS"].ToString() + ")"))
                    {
                        DataColumn column = new DataColumn(row["REPORT_ITEM_NAME"].ToString() + "(" + row["UNITS"].ToString() + ")", typeof(System.String));
                        column.ExtendedProperties.Add("HasUnit", true);
                        dtLabResult.Columns.Add(column);
                    }
                }
                else
                {
                    if (!dtLabResult.Columns.Contains(row["REPORT_ITEM_NAME"].ToString()))
                    {
                        DataColumn column = new DataColumn(row["REPORT_ITEM_NAME"].ToString(), typeof(System.String));
                        column.ExtendedProperties.Add("HasUnit", false);
                        dtLabResult.Columns.Add(column);
                    }
                }
            }

            //填充表数据
            var r = dtLabResult.NewRow();
            StringBuilder sbXml = new StringBuilder();
            sbXml.Append("<Data><Line");
            foreach (DataColumn col in dtLabResult.Columns)
            {
                var temp = dtable.Rows[0]; // dtable.AsEnumerable().FirstOrDefault(c => c["TEST_NO"].ToString().Equals(row["TEST_NO"].ToString()));
                if (col.ColumnName.Equals("检验日期"))
                {
                    r[col.ColumnName] = temp["RESULTS_RPT_DATE_TIME"].ToString();
                }
                else if (col.ColumnName.Equals("透析号"))
                {
                    r[col.ColumnName] = temp["HEMODIALYSIS_ID"].ToString();
                }
                else if (col.ColumnName.Equals("姓名"))
                {
                    r[col.ColumnName] = temp["NAME"].ToString();
                }
                else if (col.ColumnName.Equals("性别"))
                {
                    r[col.ColumnName] = temp["SEX"].ToString();
                }
                else if (col.ColumnName.Equals("病人号"))
                {
                    r[col.ColumnName] = temp["PATIENT_ID"].ToString();
                }
                else if (col.ColumnName.Equals("单号"))
                {
                    r[col.ColumnName] = temp["TEST_NO"].ToString();
                }
                else if (col.ColumnName.Equals("EXT_XML"))
                {
                    r[col.ColumnName] = "";
                }
                else
                {
                    string name = ((bool)col.ExtendedProperties["HasUnit"]) ? col.ColumnName.Substring(0, col.ColumnName.LastIndexOf("(")) : col.ColumnName;
                    temp = dtable.AsEnumerable().FirstOrDefault(c => c["REPORT_ITEM_NAME"].ToString().Equals(name));
                    if (temp != null)
                    {                        
                        r[col.ColumnName] = temp["RESULT"].ToString().Trim();
                        var sdata = temp["RESULT"].ToString().Trim();
                        var edata = string.Empty;
                        if (Regex.IsMatch(sdata, @"^[+-]?\d*[.]?\d*$")) //数字
                        {
                            edata = sdata;
                        }
                        else//非数字
                        {
                            if (sdata.Contains("阴"))
                            {
                                edata = "negative";
                            }
                            else if (sdata.Contains("阳"))
                            {
                                edata = "positive";
                            }
                            else
                            {
                                edata = "";
                            }
                        }

                        var rr = dtTitle.AsEnumerable().FirstOrDefault(i => name.Equals(i["ITEM_VALUE"].ToString()) || name.Contains(i["ITEM_VALUE"].ToString() + "("));
                        if (rr != null)
                        {
                            if (!sbXml.ToString().Contains("COL_" + rr["ORDER_NUMBER"].ToString()))
                            {
                                sbXml.Append(" ");
                                sbXml.Append("COL_" + rr["ORDER_NUMBER"].ToString());
                                sbXml.Append(" = ");
                                sbXml.Append("\"" + edata + "\"");
                            }
                        }
                    }
                }
            }
            sbXml.Append("></Line></Data>");
            r["EXT_XML"] = sbXml.ToString();
            dtLabResult.Rows.Add(r);
            return dtLabResult;
        }

        public static DataTable GetSpliterDt<T>(T t, int flag) where T : DataTable
        {
            try
            {
                var dt = t as DataTable;
                dt = dt.Select(" KIND = '" + flag + "'").CopyToDataTable();
                return dt;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        /// <summary>
        /// 递归比较相邻的两行值相加是否为5
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static bool IsTwoOrThirdPatient(DataTable dt)
        {
            var count = dt.Rows.Count;
            var first = 0;
            var two = 1;
            if (two > count)
            {
                return true;
            }
            if (Utility.CInt(dt.Rows[first]["COUNT"].ToString()) + Utility.CInt(dt.Rows[two]["COUNT"].ToString()) == 5)
            {
                dt.Rows[first].Delete();
                dt.Rows[two].Delete();
                dt.AcceptChanges();
                return IsTwoOrThirdPatient(dt);
            }
            else
            {
                return false;
            }
        }

        /// <summary>  
        /// 将DataRow转换成object  
        /// </summary>  
        /// <typeparam name="T"></typeparam>  
        /// <param name="row"></param>  
        /// <returns></returns>  
        public static T ConvertToObject<T>(DataRow row) where T : new()
        {
            object obj = new T();
            if (row != null)
            {
                DataTable t = row.Table;
                GetObject(t.Columns, row, obj);
            }
            if (obj != null && obj is T)
                return (T)obj;
            else
                return default(T);

        }

        /// <summary>
        /// 根据dr类型得到对象类型
        /// </summary>
        /// <param name="cols"></param>
        /// <param name="dr"></param>
        /// <param name="obj"></param>
        private static void GetObject(DataColumnCollection cols, DataRow dr, Object obj)
        {
            Type t = obj.GetType();

            PropertyInfo[] props = t.GetProperties();
            foreach (PropertyInfo pro in props)
            {
                if (cols.Contains(pro.Name))
                {
                    pro.SetValue(obj,
                        dr[pro.Name] == DBNull.Value ? null : dr[pro.Name],
                        null);
                }
            }
        }
        #endregion dataTable转换成object

        #region UIHelper

        /// <summary>
        /// 获得最大上传图片大小
        /// </summary>
        /// <returns></returns>
        public static Int32 GetMaxUploadPictureLength()
        {
            int defaultMaxLength = 10;

            if (string.IsNullOrEmpty(ConfigurationManager.AppSettings["MaxUploadPictureLength"]))
                return defaultMaxLength;
            else
            {
                int maxLength = 0;
                string strMaxLength = ConfigurationManager.AppSettings["MaxUploadPictureLength"];
                if (Int32.TryParse(strMaxLength, out maxLength))
                    return Int32.Parse(strMaxLength);
                else
                    return defaultMaxLength;
            }
        }

        #region 检查水处理、空气消毒、血透机运行记录录入时间是否合理

        public static bool CheckRecordTimeIsValid(string type, DateTime start, DateTime end, Model.ConfigModel.MED_COMMON_ITEMLISTDataTable dtConfig)
        {
            bool result = true;

            if (dtConfig != null && dtConfig.Rows.Count > 0)
            {
                var row = dtConfig.FirstOrDefault(i => i.ITEM_NAME.Equals(type));
                if (row != null)
                {
                    if (type.Equals("血透机运行记录"))
                    {
                        if (DateTime.Compare(DateTime.Now, start) >= 0 && DateTime.Compare(DateTime.Now, end) <= 0)
                        {
                            result = true;
                        }
                        else
                        {
                            result = false;
                        }
                    }
                    else if (type.Equals("空气消毒记录") || type.Equals("透析参数记录"))
                    {
                        if (DateTime.Compare(DateTime.Now, start) >= 0)
                        {
                            result = true;
                        }
                        else
                        {
                            result = false;
                        }
                    }
                }
            }
            return result;
        }

        #endregion

        #endregion

        #region MyReg加密狗ion

        public static readonly string dogTipStr = "您的系统未授权,请联系厂家给予授权！";
        public static readonly string dogTipStr1 = "您的系统已超过授权数量{0},请联系厂家给予新的授权！";
        /// <summary>
        /// 统一获取医院名称，用于系统中所有需要显示医院名称的地方
        /// </summary>
        /// <returns></returns>
        public static string GetHospitalName()
        {
            string result = string.Empty;
            //try
            //{
            //    result = DAuthContext.Current.HospitalName.Trim();
            //}
            //catch (Exception)
            //{
            //    result = string.Empty;
            //}
            if (string.IsNullOrEmpty(result))
                result = "上海市普陀区人民医院";
            return result;
        }
        public static int GetHospitalBedCount()
        {
            int result = 40;// DAuthContext.Current.LimitedOne;
            return result;
        }

        #endregion
    }

    #region 枚举
    /// <summary>
    /// 枚举
    /// </summary>
    public enum ERROR_ID
    {
        ERROR_SUCCESS = 0,  // Success 
        ERROR_BUSY = 170,
        ERROR_MORE_DATA = 234,
        ERROR_NO_BROWSER_SERVERS_FOUND = 6118,
        ERROR_INVALID_LEVEL = 124,
        ERROR_ACCESS_DENIED = 5,
        ERROR_INVALID_PASSWORD = 86,
        ERROR_INVALID_PARAMETER = 87,
        ERROR_BAD_DEV_TYPE = 66,
        ERROR_NOT_ENOUGH_MEMORY = 8,
        ERROR_NETWORK_BUSY = 54,
        ERROR_BAD_NETPATH = 53,
        ERROR_NO_NETWORK = 1222,
        ERROR_INVALID_HANDLE_STATE = 1609,
        ERROR_EXTENDED_ERROR = 1208,
        ERROR_DEVICE_ALREADY_REMEMBERED = 1202,
        ERROR_NO_NET_OR_BAD_PATH = 1203
    }

    /// <summary>
    /// 枚举
    /// </summary>
    public enum RESOURCE_SCOPE
    {
        RESOURCE_CONNECTED = 1,
        RESOURCE_GLOBALNET = 2,
        RESOURCE_REMEMBERED = 3,
        RESOURCE_RECENT = 4,
        RESOURCE_CONTEXT = 5
    }

    /// <summary>
    /// 枚举
    /// </summary>
    public enum RESOURCE_TYPE
    {
        RESOURCETYPE_ANY = 0,
        RESOURCETYPE_DISK = 1,
        RESOURCETYPE_PRINT = 2,
        RESOURCETYPE_RESERVED = 8,
    }


    public enum RESOURCE_USAGE
    {
        RESOURCEUSAGE_CONNECTABLE = 1,
        RESOURCEUSAGE_CONTAINER = 2,
        RESOURCEUSAGE_NOLOCALDEVICE = 4,
        RESOURCEUSAGE_SIBLING = 8,
        RESOURCEUSAGE_ATTACHED = 16,
        RESOURCEUSAGE_ALL = (RESOURCEUSAGE_CONNECTABLE | RESOURCEUSAGE_CONTAINER | RESOURCEUSAGE_ATTACHED),
    }

    public enum RESOURCE_DISPLAYTYPE
    {
        RESOURCEDISPLAYTYPE_GENERIC = 0,
        RESOURCEDISPLAYTYPE_DOMAIN = 1,
        RESOURCEDISPLAYTYPE_SERVER = 2,
        RESOURCEDISPLAYTYPE_SHARE = 3,
        RESOURCEDISPLAYTYPE_FILE = 4,
        RESOURCEDISPLAYTYPE_GROUP = 5,
        RESOURCEDISPLAYTYPE_NETWORK = 6,
        RESOURCEDISPLAYTYPE_ROOT = 7,
        RESOURCEDISPLAYTYPE_SHAREADMIN = 8,
        RESOURCEDISPLAYTYPE_DIRECTORY = 9,
        RESOURCEDISPLAYTYPE_TREE = 10,
        RESOURCEDISPLAYTYPE_NDSCONTAINER = 11
    }
    #endregion

    /// <summary>
    /// 结构体
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct NETRESOURCE
    {
        public RESOURCE_SCOPE dwScope;
        public RESOURCE_TYPE dwType;
        public RESOURCE_DISPLAYTYPE dwDisplayType;
        public RESOURCE_USAGE dwUsage;

        [MarshalAs(UnmanagedType.LPStr)]
        public string lpLocalName;

        [MarshalAs(UnmanagedType.LPStr)]
        public string lpRemoteName;

        [MarshalAs(UnmanagedType.LPStr)]
        public string lpComment;

        [MarshalAs(UnmanagedType.LPStr)]
        public string lpProvider;
    }

    /// <summary>
    /// 网络连接
    /// </summary>
    public class NetworkConnection
    {
        [DllImport("mpr.dll")]
        public static extern int WNetAddConnection2A(NETRESOURCE[] lpNetResource, string lpPassword, string lpUserName, int dwFlags);

        [DllImport("mpr.dll")]
        public static extern int WNetCancelConnection2A(string sharename, int dwFlags, int fForce);

        public static int Connect(string remotePath, string username, string password)
        {
            NETRESOURCE[] share_driver = new NETRESOURCE[1];
            share_driver[0].dwScope = RESOURCE_SCOPE.RESOURCE_GLOBALNET;
            share_driver[0].dwType = RESOURCE_TYPE.RESOURCETYPE_DISK;
            share_driver[0].dwDisplayType = RESOURCE_DISPLAYTYPE.RESOURCEDISPLAYTYPE_SHARE;
            share_driver[0].dwUsage = RESOURCE_USAGE.RESOURCEUSAGE_CONNECTABLE;
            share_driver[0].lpRemoteName = remotePath;

            int ret = WNetAddConnection2A(share_driver, password, username, 1);

            return ret;
        }

        /// <summary>
        /// 断开连接
        /// </summary>
        /// <param name="localpath"></param>
        /// <returns></returns>
        public static int Disconnect(string localpath)
        {
            return WNetCancelConnection2A(localpath, 1, 1);
        }


    }

    /// <summary>
    /// 身份证操作辅助类
    /// </summary>
    public class IDCardHelper
    {
        private static DataTable dt_IdType;
        static IDCardHelper()
        {
            dt_IdType = new DataTable();
            dt_IdType.Columns.Add("text");
            dt_IdType.Columns.Add("value");
            dt_IdType.Rows.Add(new object[] { "居民身份证", "A" });
            dt_IdType.Rows.Add(new object[] { "军官证", "C" });
            dt_IdType.Rows.Add(new object[] { "士兵证", "D" });
            dt_IdType.Rows.Add(new object[] { "军官离退休证", "E" });
            dt_IdType.Rows.Add(new object[] { "境外人员身份证明", "F" });
            dt_IdType.Rows.Add(new object[] { "外交人员身份证明", "G" });
        }

        /// <summary>
        /// 绑定身份证类别的名称
        /// </summary>
        /// <param name="cb">ComboBox控件</param>
        public static void InitIdType(System.Windows.Forms.ComboBox cb)
        {
            cb.DataSource = dt_IdType;
            cb.DisplayMember = "text";
            cb.ValueMember = "value";
            cb.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
        }

        /// <summary>
        /// 获取身份证类别的名称（居民身份证、军官证、士兵证、军官离退休证、境外人员身份证明、外交人员身份证明）
        /// </summary>
        /// <returns></returns>
        public static DataTable CreateIDType()
        {
            return dt_IdType;
        }

        /// <summary>
        /// 验证身份证结果
        /// </summary>
        /// <param name="idcard">身份证号码</param>
        /// <returns>正确的时候返回string.Empty</returns>
        public static string Validate(string idcard)
        {
            if (idcard.Length != 18 && idcard.Length != 15)
            {
                return "身份证明号码必须是15或者18位！";
            }

            Regex rg = new Regex(@"^\d{17}(\d|X)$");
            if (!rg.Match(idcard).Success)
            {
                return "身份证号码必须为数字或者X！";
            }
            if (idcard.Length == 15)
            {
                idcard = IdCard15To18(idcard);
            }
            else if (idcard.Length == 18)
            {
                int ll_sum = 0, tmp = 0;
                for (int i = 0; i < 17; i++)
                {
                    tmp = int.Parse(idcard.Substring(i, 1));
                    ll_sum += tmp * li_quan[i];
                }
                ll_sum = ll_sum % 11;
                if (idcard.Substring(17, 1) != ls_jy[ll_sum])
                {
                    return "身份证号码最后一位应该是" + ls_jy[ll_sum] + "！";
                }
            }

            try
            {
                DateTime.Parse(idcard.Substring(6, 4) + "-" + idcard.Substring(10, 2) + "-" + idcard.Substring(12, 2));
            }
            catch
            {
                return "非法生日";
            }
            return string.Empty;
        }

        private static string[] ls_jy = { "1", "0", "X", "9", "8", "7", "6", "5", "4", "3", "2" };
        private static int[] li_quan = { 7, 9, 10, 5, 8, 4, 2, 1, 6, 3, 7, 9, 10, 5, 8, 4, 2, 1 };

        /// <summary>
        /// 15位身份证明号码转化成18位用来编码
        /// </summary>
        /// <param name="idcard">15位的身份证号码</param>
        /// <returns></returns>
        public static string IdCard15To18(string idcard)
        {
            /*             
             string ls_jy[] =  { "1", "0", "X", "9", "8", "7", "6", "5", "4", "3", "2"}, t, ls_sfzmhm
integer li_quan[] =  { 7, 9, 10, 5, 8, 4, 2, 1, 6, 3, 7, 9, 10, 5, 8, 4, 2, 1}
int ll_sum = 0, i

ls_sfzmhm = mid(sfzmhm, 1, 6) + '19' + mid(sfzmhm, 7)
for i = 1 to len(ls_sfzmhm)
	ll_sum += integer(mid(ls_sfzmhm, i, 1)) * li_quan[i]
next

ll_sum = mod(ll_sum, 11)

ls_sfzmhm += ls_jy[ll_sum + 1]

return ls_sfzmhm
             */

            if (idcard == null || idcard.Length != 15)
            {
                return idcard;
            }
            else
            {
                string result = string.Empty;
                int ll_sum = 0, tmp = 0;
                result = idcard.Substring(0, 6) + "19" + idcard.Substring(6, 9);
                for (int i = 0; i < 17; i++)
                {
                    tmp = int.Parse(result.Substring(i, 1));
                    ll_sum += tmp * li_quan[i];
                }
                ll_sum = ll_sum % 11;
                result += ls_jy[ll_sum];
                return result;

            }
        }

        /// <summary>
        /// 获取身份证对应省份的区划
        /// </summary>
        /// <param name="id">身份证</param>
        /// <returns>头两位+4个0</returns>
        public static string GetProvince(string id)
        {
            return id.Substring(0, 2) + "0000";
        }

        /// <summary>
        /// 获取身份证对应县市的区划
        /// </summary>
        /// <param name="id">身份证</param>
        /// <returns>头4位+2个0</returns>
        public static string GetCity(string id)
        {
            return id.Substring(0, 4) + "00";
        }

        /// <summary>
        /// 获取身份证对应地区的区划
        /// </summary>
        /// <param name="id">身份证</param>
        /// <returns>头6位</returns>
        public static string GetArea(string id)
        {
            return id.Substring(0, 6);
        }

        /// <summary>
        /// 根据身份证判断是否男女
        /// </summary>
        /// <param name="id">身份证号码</param>
        /// <returns>返回"男"或者"女"</returns>
        public static string GetSexName(string id)
        {
            int sexStr = 0;
            if (id.Length == 15)
            {
                sexStr = Convert.ToInt32(id.Substring(14, 1));
            }
            else if (id.Length == 18)
            {
                sexStr = Convert.ToInt32(id.Substring(16, 1));
            }
            else
            {
                throw new ArgumentException("身份证号码不是15或者18位！");
            }
            return sexStr % 2 == 0 ? "女" : "男";
        }

        /// <summary>
        /// 根据身份证获取出生年月
        /// </summary>
        /// <param name="id">身份证号码</param>
        /// <returns>出生年月</returns>
        public static DateTime GetBirthday(string id)
        {
            string result = string.Empty;
            if (id.Length == 15)
            {
                result = "19" + id.Substring(6, 2) + "-" + id.Substring(8, 2) + "-" + id.Substring(10, 2);
            }
            else if (id.Length == 18)
            {
                result = id.Substring(6, 4) + "-" + id.Substring(10, 2) + "-" + id.Substring(12, 2);
            }
            else
            {
                throw new ArgumentException("身份证号码不是15或者18位！");
            }
            return Convert.ToDateTime(result);
        }
        /// <summary>
        /// 根据日期获取年龄
        /// </summary>
        /// <param name="pBirthday"></param>
        /// <returns></returns>
        public static int GetAgeByDate(string pBirthday)
        {
            int result = 0;
            if (pBirthday.Length > 0)
            {
                int m_Y1 = DateTime.Parse(pBirthday).Year;
                int m_Y2 = DateTime.Now.Year;
                int m_Age = m_Y2 - m_Y1;
                result = m_Age;
            }
            return result;
        }

        /// <summary>
        /// 根据出生日期获取年龄
        /// </summary>
        /// <param name="pBirthday"></param>
        /// <returns></returns>
        public static int GetAgeByIDCard(string id)
        {
            var pBirthday = GetBirthday(id);
            int result = 0;

            int m_Y1 = pBirthday.Year;
            int m_Y2 = DateTime.Now.Year;
            int m_Age = m_Y2 - m_Y1;
            result = m_Age;

            return result;
        }

    }
}
