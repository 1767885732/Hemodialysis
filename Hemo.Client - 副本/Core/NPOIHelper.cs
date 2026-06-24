using System;
using System.Data;
using System.IO;
using System.Text;
using NPOI.HPSF;
using NPOI.HSSF.UserModel;
using NPOI.HSSF.Util;
using NPOI.HSSF.Model;


namespace Hemo.Client.Core
{
    public class ExcelHelper
    {
        /// <summary>
        /// NPOI简单Demo，快速入门代码
        /// </summary>
        /// <param name="dtSource"></param>
        /// <param name="strFileName"></param>
        /// <remarks>NPOI认为Excel的第一个单元格是：(0，0)</remarks>
        /// <Author>柳永法 http://www.yongfa365.com/ 2010-5-8 22:21:41</Author>
        public static void ExportEasy(DataTable dtSource, string strFileName)
        {
            HSSFWorkbook workbook = new HSSFWorkbook();
            HSSFSheet sheet = workbook.CreateSheet();

            //填充表头
            HSSFRow dataRow = sheet.CreateRow(0);
            foreach (DataColumn column in dtSource.Columns)
            {
                dataRow.CreateCell(column.Ordinal).SetCellValue(column.ColumnName);
            }

            //填充内容
            for (int i = 0; i < dtSource.Rows.Count; i++)
            {
                dataRow = sheet.CreateRow(i + 1);
                for (int j = 0; j < dtSource.Columns.Count; j++)
                {
                    dataRow.CreateCell(j).SetCellValue(dtSource.Rows[i][j].ToString());
                }
            }

            //保存
            using (MemoryStream ms = new MemoryStream())
            {
                using (FileStream fs = new FileStream(strFileName, FileMode.Create, FileAccess.Write))
                {
                    workbook.Write(ms);
                    ms.Flush();
                    ms.Position = 0;
                    byte[] data = ms.ToArray();
                    fs.Write(data, 0, data.Length);
                    fs.Flush();
                }
            }
            sheet.Dispose();
            workbook.Dispose();
        }

        /// <summary>
        /// DataTable导出到Excel的MemoryStream
        /// </summary>
        /// <param name="dtSource">源DataTable</param>
        /// <param name="strHeaderText">表头文本</param>
        /// <Author>柳永法 http://www.yongfa365.com/ 2010-5-8 22:21:41</Author>
        public static MemoryStream Export(DataTable dtSource, string strHeaderText)
        {
            HSSFWorkbook workbook = new HSSFWorkbook();
            string[] str = strHeaderText.Split('@');
            HSSFCellStyle style = (HSSFCellStyle)workbook.CreateCellStyle();
            style.BorderBottom = CellBorderType.THIN;
            style.BorderLeft = CellBorderType.THIN;
            style.BorderRight = CellBorderType.THIN;
            style.BorderTop = CellBorderType.THIN;

            #region 右击文件 属性信息

            {
                DocumentSummaryInformation dsi = PropertySetFactory.CreateDocumentSummaryInformation();
                dsi.Company = "http://www.medicalsystem.com/";
                workbook.DocumentSummaryInformation = dsi;

                SummaryInformation si = PropertySetFactory.CreateSummaryInformation();
                si.Author = "质控报表导出"; //填加xls文件作者信息
                si.ApplicationName = "血透导出程序"; //填加xls文件创建程序信息
                si.LastAuthor = "报表导出"; //填加xls文件最后保存者信息
                si.Comments = "说明信息"; //填加xls文件作者信息
                si.Title = "血透质控报表导出"; //填加xls文件标题信息
                si.Subject = "血透质控报表导出"; //填加文件主题信息
                si.CreateDateTime = DateTime.Now;
                workbook.SummaryInformation = si;
                //workbook.SetRepeatingRowsAndColumns(
            }

            #endregion 右击文件 属性信息

            for (int k = 0; k < 3; k++)
            {
                #region 血透质控报表

                if (k == 0)
                {
                    HSSFSheet sheet = workbook.CreateSheet("血透质控报表");
                    HSSFCellStyle dateStyle = workbook.CreateCellStyle();
                    HSSFDataFormat format = workbook.CreateDataFormat();
                    //dateStyle.DataFormat = format.GetFormat("yyyy-mm-dd HH:mm:ss fff");
                    sheet.IsGridsPrinted = true;

                    #region 取得列宽

                    int[] arrColWidth = new int[dtSource.Columns.Count];
                    foreach (DataColumn item in dtSource.Columns)
                    {
                        arrColWidth[item.Ordinal] =
                            Encoding.GetEncoding(936).GetBytes(item.ColumnName.ToString()).Length;
                    }
                    for (int i = 0; i < dtSource.Rows.Count; i++)
                    {
                        for (int j = 0; j < dtSource.Columns.Count; j++)
                        {
                            int intTemp = Encoding.GetEncoding(936).GetBytes(dtSource.Rows[i][j].ToString()).Length;
                            if (j == 2)
                            {
                                arrColWidth[j] = 70;
                            }
                            else
                            {
                                if (intTemp > arrColWidth[j])
                                {
                                    arrColWidth[j] = intTemp;
                                }
                            }
                        }
                    }

                    #endregion 取得列宽

                    #region 合并单元格

                    //for (int i = dtSource.Rows.Count; i > 0; i--)
                    //{
                    //    if (i == 2)
                    //        break;
                    //    if (dtSource.Rows[i - 1]["displayname"].ToString() ==
                    //        dtSource.Rows[i - 2]["displayname"].ToString())
                    //    {
                    //        dtSource.Rows[i - 1]["displayname"] = "";
                    //        sheet.AddMergedRegion(new Region(i + 2, (short) 0, i + 3, (short) 0));
                    //    }
                    //    if (dtSource.Rows[i - 1]["activitytypename"].ToString() ==
                    //        dtSource.Rows[i - 2]["activitytypename"].ToString())
                    //    {
                    //        dtSource.Rows[i - 1]["activitytypename"] = "";

                    //        sheet.AddMergedRegion(new Region(i + 2, (short) 1, i + 3, (short) 1));
                    //    }
                    //    if (dtSource.Rows[i - 1]["treatecontent"].ToString() ==
                    //        dtSource.Rows[i - 2]["treatecontent"].ToString())
                    //    {
                    //        dtSource.Rows[i - 1]["treatecontent"] = "";

                    //        sheet.AddMergedRegion(new Region(i + 2, (short) 2, i + 3, (short) 2));
                    //    }
                    //}

                    #endregion 合并单元格 mjr

                    int rowIndex = 0;

                    foreach (DataRow row in dtSource.Rows)
                    {
                        #region 新建表，填充表头，填充列头，样式

                        if (rowIndex == 65535 || rowIndex == 0)
                        {
                            if (rowIndex != 0)
                            {
                                sheet = workbook.CreateSheet();
                            }

                            #region 表头及样式

                            {
                                HSSFRow headerRow = sheet.CreateRow(0);
                                headerRow.HeightInPoints = 25;
                                headerRow.CreateCell(0).SetCellValue("血透系统：");
                                headerRow.CreateCell(1).SetCellValue(str[0] + "医疗机构开展血液透析基本情况");
                                HSSFCellStyle headStyle = workbook.CreateCellStyle();
                                headStyle.Alignment = CellHorizontalAlignment.CENTER;
                                HSSFFont font = workbook.CreateFont();
                                font.FontHeightInPoints = 10;
                                font.Boldweight = 700;
                                headStyle.SetFont(font);

                                headerRow.GetCell(0).CellStyle = headStyle;
                                headerRow.GetCell(1).CellStyle = headStyle;
                                sheet.AddMergedRegion(new Region(0, 1, 0, dtSource.Columns.Count - 1));
                                headerRow.Dispose();
                            }

                            #endregion 表头及样式

                            #region 列头及样式

                            {
                                HSSFRow headerRow = sheet.CreateRow(1);

                                HSSFCellStyle headStyle = workbook.CreateCellStyle();
                                headStyle.Alignment = CellHorizontalAlignment.CENTER;
                                HSSFFont font = workbook.CreateFont();
                                font.FontHeightInPoints = 10;
                                font.Boldweight = 700;
                                headStyle.SetFont(font);
                                string[] ColHead = new string[] { "名称", "内容" };

                                for (int i = 0; i < ColHead.Length; i++)
                                {
                                    headerRow.CreateCell(i).SetCellValue(ColHead[i]);
                                    headerRow.GetCell(i).CellStyle = headStyle;

                                    //设置列宽
                                    sheet.SetColumnWidth(i, (arrColWidth[i] + 1) * 256);
                                }

                                //foreach (DataColumn column in dtSource.Columns)
                                //{
                                //    headerRow.CreateCell(column.Ordinal).SetCellValue(column.ColumnName);
                                //    headerRow.GetCell(column.Ordinal).CellStyle = headStyle;

                                //    //设置列宽
                                //    sheet.SetColumnWidth(column.Ordinal, (arrColWidth[column.Ordinal] + 1) * 256);

                                //}
                                headerRow.Dispose();
                            }

                            #endregion 列头及样式

                            rowIndex = 4;
                        }

                        #endregion 新建表，填充表头，填充列头，样式

                        #region 填充内容

                        HSSFRow dataRow = sheet.CreateRow(rowIndex);
                        // mySheet.get_Range(mySheet.Cells[4, colSum], mySheet.Cells[rowSum, colIndex]).Borders.LineStyle = 1;
                        foreach (DataColumn column in dtSource.Columns)
                        {
                            HSSFCell newCell = dataRow.CreateCell(column.Ordinal);
                            newCell.CellStyle.WrapText = true;

                            newCell.CellStyle = style;

                            if (newCell.ColumnIndex == 0 || newCell.ColumnIndex == 1)
                            {
                                newCell.CellStyle.Alignment = CellHorizontalAlignment.CENTER;
                                newCell.CellStyle.VerticalAlignment = CellVerticalAlignment.CENTER;
                            }
                            else
                            {
                                newCell.CellStyle.Alignment = CellHorizontalAlignment.LEFT;
                                newCell.CellStyle.VerticalAlignment = CellVerticalAlignment.CENTER;
                            }
                            string drValue = row[column].ToString();

                            switch (column.DataType.ToString())
                            {
                                case "System.String": //字符串类型
                                    newCell.SetCellValue(drValue);
                                    break;

                                case "System.DateTime": //日期类型
                                    DateTime dateV;
                                    DateTime.TryParse(drValue, out dateV);
                                    newCell.SetCellValue(dateV);

                                    newCell.CellStyle = dateStyle; //格式化显示
                                    break;

                                case "System.Boolean": //布尔型
                                    bool boolV = false;
                                    bool.TryParse(drValue, out boolV);
                                    newCell.SetCellValue(boolV);
                                    break;

                                case "System.Int16": //整型
                                case "System.Int32":
                                case "System.Int64":
                                case "System.Byte":
                                    int intV = 0;
                                    int.TryParse(drValue, out intV);
                                    newCell.SetCellValue(intV);
                                    break;

                                case "System.Decimal": //浮点型
                                case "System.Double":
                                    //    double doubV = 0;
                                    //    double.TryParse(drValue, out doubV);
                                    //newCell.SetCellValue(doubV);
                                    newCell.SetCellValue(drValue);
                                    break;

                                case "System.DBNull": //空值处理
                                    newCell.SetCellValue("");
                                    break;
                                default:
                                    newCell.SetCellValue("");
                                    break;
                            }
                        }

                        #endregion 填充内容

                        rowIndex++;
                    }
                }

                #endregion 医嘱流程
            }

            using (MemoryStream ms = new MemoryStream())
            {
                workbook.Write(ms);
                ms.Flush();
                ms.Position = 0;
                //workbook.Dispose();
                //sheet.Dispose();
                workbook.Dispose();

                return ms;
            }
        }

        /// <summary>
        /// DataTable导出到Excel文件
        /// </summary>
        /// <param name="dtSource">源DataTable</param>
        /// <param name="strHeaderText">表头文本</param>
        /// <param name="strFileName">保存位置</param>
        /// <Author>柳永法 http://www.yongfa365.com/ 2010-5-8 22:21:41</Author>
        public static void Export(DataTable dtSource, string strHeaderText, string strFileName)
        {
            using (MemoryStream ms = Export(dtSource, strHeaderText))
            {
                using (FileStream fs = new FileStream(strFileName, FileMode.Create, FileAccess.Write))
                {
                    byte[] data = ms.ToArray();
                    fs.Write(data, 0, data.Length);
                    fs.Flush();
                }
            }
        }

        /// <summary>
        /// 用于Web导出
        /// </summary>
        /// <param name="dtSource">源DataTable</param>
        /// <param name="strHeaderText">表头文本</param>
        /// <param name="strFileName">文件名</param>
        /// <Author>柳永法 http://www.yongfa365.com/ 2010-5-8 22:21:41</Author>
        public static void ExportByWeb(DataTable dtSource, string strHeaderText, string strFileName)
        {
            //HttpContext curContext = HttpContext.Current;

            //// 设置编码和附件格式
            //curContext.Response.ContentType = "application/vnd.ms-excel";
            //curContext.Response.ContentEncoding = Encoding.UTF8;
            //curContext.Response.Charset = "";
            //curContext.Response.AppendHeader("Content-Disposition",
            //    "attachment;filename=" + HttpUtility.UrlEncode(strFileName, Encoding.UTF8));

            //curContext.Response.BinaryWrite(Export(dtSource, strHeaderText).GetBuffer());
            //curContext.Response.End();
        }

        /// <summary>读取excel
        /// 默认第一行为标头
        /// </summary>
        /// <param name="strFileName">excel文档路径</param>
        /// <returns></returns>
        public static DataTable Import(string strFileName)
        {
            DataTable dt = new DataTable();

            HSSFWorkbook hssfworkbook;
            using (FileStream file = new FileStream(strFileName, FileMode.Open, FileAccess.Read))
            {
                hssfworkbook = new HSSFWorkbook(file);
            }
            HSSFSheet sheet = hssfworkbook.GetSheetAt(0);
            System.Collections.IEnumerator rows = sheet.GetRowEnumerator();

            HSSFRow headerRow = sheet.GetRow(0);
            int cellCount = headerRow.LastCellNum;

            for (int j = 0; j < cellCount; j++)
            {
                HSSFCell cell = headerRow.GetCell(j);
                dt.Columns.Add(cell.ToString());
            }

            for (int i = (sheet.FirstRowNum + 1); i <= sheet.LastRowNum; i++)
            {
                HSSFRow row = sheet.GetRow(i);
                DataRow dataRow = dt.NewRow();

                for (int j = row.FirstCellNum; j < cellCount; j++)
                {
                    if (row.GetCell(j) != null)
                        dataRow[j] = row.GetCell(j).ToString();
                }

                dt.Rows.Add(dataRow);
            }

            //while (rows.MoveNext())
            //{
            //    HSSFRow row = (HSSFRow)rows.Current;
            //    DataRow dr = dt.NewRow();

            //    for (int i = 0; i < row.LastCellNum; i++)
            //    {
            //        HSSFCell cell = row.GetCell(i);

            //        if (cell == null)
            //        {
            //            dr[i] = null;
            //        }
            //        else
            //        {
            //            dr[i] = cell.ToString();
            //        }
            //    }
            //    dt.Rows.Add(dr);
            //}

            return dt;
        }

        public static DataTable Import(Stream ExcelFileStream, string SheetName, int HeaderRowIndex)
        {
            HSSFWorkbook workbook = new HSSFWorkbook(ExcelFileStream);
            HSSFSheet sheet = workbook.GetSheet(SheetName);

            DataTable table = new DataTable();

            HSSFRow headerRow = sheet.GetRow(HeaderRowIndex);
            int cellCount = headerRow.LastCellNum;

            for (int i = headerRow.FirstCellNum; i < cellCount; i++)
            {
                DataColumn column = new DataColumn(headerRow.GetCell(i).StringCellValue);
                table.Columns.Add(column);
            }

            int rowCount = sheet.LastRowNum;

            for (int i = (sheet.FirstRowNum + 1); i < sheet.LastRowNum; i++)
            {
                HSSFRow row = sheet.GetRow(i);
                DataRow dataRow = table.NewRow();

                for (int j = row.FirstCellNum; j < cellCount; j++)
                    dataRow[j] = row.GetCell(j).ToString();
            }

            ExcelFileStream.Close();
            workbook = null;
            sheet = null;
            return table;
        }

        public static DataTable Import(Stream ExcelFileStream, int SheetIndex, int HeaderRowIndex)
        {
            HSSFWorkbook workbook = new HSSFWorkbook(ExcelFileStream);
            HSSFSheet sheet = workbook.GetSheetAt(SheetIndex);

            DataTable table = new DataTable();

            HSSFRow headerRow = sheet.GetRow(HeaderRowIndex);
            int cellCount = headerRow.LastCellNum;

            for (int i = headerRow.FirstCellNum; i < cellCount; i++)
            {
                DataColumn column = new DataColumn(headerRow.GetCell(i).StringCellValue);
                table.Columns.Add(column);
            }

            int rowCount = sheet.LastRowNum;

            for (int i = (sheet.FirstRowNum + 1); i < sheet.LastRowNum; i++)
            {
                HSSFRow row = sheet.GetRow(i);
                DataRow dataRow = table.NewRow();

                for (int j = row.FirstCellNum; j < cellCount; j++)
                {
                    if (row.GetCell(j) != null)
                        dataRow[j] = row.GetCell(j).ToString();
                }

                table.Rows.Add(dataRow);
            }

            ExcelFileStream.Close();
            workbook = null;
            sheet = null;
            return table;
        }



        public void NpoiExportExcel(string reportName, DataTable dt, string exportTemplatePath)
        {
            //Stream s = RenderDataTableToExcel(dt, exportTemplatePath);
            //if (s != null)
            //{
            //    MemoryStream ms = s as MemoryStream;
            //    HttpContext.Current.Response.AddHeader("Content-Disposition", string.Format("attachment;filename=" + HttpUtility.UrlEncode(reportName) + DateTime.Now.ToString("yyyyMMdd") + ".xlsx"));
            //    HttpContext.Current.Response.AddHeader("Content-Length", ms.ToArray().Length.ToString());
            //    HttpContext.Current.Response.BinaryWrite(ms.ToArray());
            //    HttpContext.Current.Response.Flush();
            //    ms.Close();
            //    ms.Dispose();
            //}
            //else
            //    HttpContext.Current.Response.Write("出错，无法下载！");
        }


        public static void RenderDataTableToExcel(DataTable sourceTable, string exportTemplatePath)
        {
            HSSFWorkbook hssfworkbook;
            using (FileStream file = new FileStream(exportTemplatePath, FileMode.Open, FileAccess.Read))
            {
                hssfworkbook = new HSSFWorkbook(file);

                HSSFSheet sheet = hssfworkbook.GetSheetAt(0);
                System.Collections.IEnumerator rows = sheet.GetRowEnumerator();

                HSSFRow headerRow = sheet.GetRow(0);
                int cellCount = headerRow.LastCellNum;

                for (int j = 0; j < cellCount; j++)
                {
                    HSSFCell cell = headerRow.GetCell(j);
                    //dt.Columns.Add(cell.ToString());
                }

                for (int i = (sheet.FirstRowNum + 1); i <= sheet.LastRowNum; i++)
                {
                    HSSFRow row = sheet.GetRow(i);
                    foreach (DataRow dataRow in sourceTable.Rows)
                    {
                        if (dataRow[0].ToString() == row.GetCell(0).ToString())
                        {
                            row.CreateCell(1).SetCellValue(dataRow[1].ToString());
                        }
                    }
                }
                hssfworkbook.Write(file);
            }

            //HSSFWorkbook workbook = null;
            //MemoryStream ms = null;
            //HSSFSheet  sheet = null;
            //HSSFRow headerRow = null;
            //string templetFileName = exportTemplatePath;// HttpContext.Current.Server.MapPath(exportTemplatePath);
            //FileStream file = new FileStream(templetFileName, FileMode.Open, FileAccess.Read);
            //workbook = new HSSFWorkbook(file);
            //try
            //{

            //    ms = new MemoryStream();
            //    sheet = workbook.GetSheet("Sheet1");
            //    int rowIndex = sheet.LastRowNum;
            //    foreach (DataRow row in sourceTable.Rows)
            //    {
            //        HSSFRow dataRow = (HSSFRow)sheet.CreateRow(rowIndex);
            //        foreach (DataColumn column in sourceTable.Columns)
            //            dataRow.CreateCell(column.Ordinal).SetCellValue(row[column].ToString());
            //        ++rowIndex;
            //    }
            //    //列宽自适应，只对英文和数字有效
            //    for (int i = 0; i <= sourceTable.Columns.Count; ++i)
            //        sheet.AutoSizeColumn(i);
            //    workbook.Write(ms);
            //    ms.Flush();
            //}
            //catch (Exception ex)
            //{
            //    throw;
            //    return null;
            //}
            //finally
            //{
            //    ms.Close();
            //    sheet = null;
            //    headerRow = null;
            //    workbook = null;
            //}
            //return ms;
        }

    }
}