/*----------------------------------------------------------------
* Copyright (c) 2013 医疗科技发展有限公司. All Rights Reserved. 
* 建立日期： 2014-1-17
* 创 建 者： 唐少弦
* 摘    要： NPOI操作EXCEL基础类
----------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NPOI.HSSF.UserModel;
using System.Data;
using System.IO;
using NPOI.SS.UserModel;
using NPOI.HSSF.Util;
using System.Windows.Forms;
using NPOI.SS.Util;
using Hemo.Utilities;

namespace Hemo.Utilities {
    public class ExcelNPOIHelper {

        /// <summary>
        /// 类版本
        /// </summary>
        public string version {
            get { return "0.1"; }
        }
        readonly int EXCEL03_MaxRow = 65535;

        /// <summary>
        /// 将DataTable转换为excel2003格式。
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        ///  单DataTabel导出示例代码
        ///  ExcelNPOIHelper myhelper = new ExcelNPOIHelper();
        ///  byte[] data = myhelper.DataTable2Excel(dt,"人员名录");
        ///  string path = "d:\\temp" + DateTime.Now.Ticks.ToString() + ".xls";
        ///  if (!File.Exists(path))
        ///  {
        ///       FileStream fs = new FileStream(path, FileMode.CreateNew);
        ///        fs.Write(data, 0, data.Length);
        ///        fs.Close();
        ///  }      
        public byte[] DataTable2Excel(DataTable dt, string sheetName) {

            IWorkbook book = new HSSFWorkbook();
            if (dt.Rows.Count < EXCEL03_MaxRow)
                DataWrite2Sheet(dt, 0, dt.Rows.Count - 1, book, sheetName);
            else {
                int page = dt.Rows.Count / EXCEL03_MaxRow;
                for (int i = 0; i < page; i++) {
                    int start = i * EXCEL03_MaxRow;
                    int end = (i * EXCEL03_MaxRow) + EXCEL03_MaxRow - 1;
                    DataWrite2Sheet(dt, start, end, book, sheetName + i.ToString());
                }
                int lastPageItemCount = dt.Rows.Count % EXCEL03_MaxRow;
                DataWrite2Sheet(dt, dt.Rows.Count - lastPageItemCount, lastPageItemCount, book, sheetName + page.ToString());
            }
            MemoryStream ms = new MemoryStream();
            book.Write(ms);
            return ms.ToArray();
        }

        /// <summary>
        /// 将DataTable转换为excel2003格式。
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="startRow"></param>
        /// <param name="endRow"></param>
        /// <param name="book"></param>
        /// <param name="sheetName"></param>
        private void DataWrite2Sheet(DataTable dt, int startRow, int endRow, IWorkbook book, string sheetName) {
            ISheet sheet = book.CreateSheet(sheetName);
            IRow header = sheet.CreateRow(0);
            for (int i = 0; i < dt.Columns.Count; i++) {
                ICell cell = header.CreateCell(i);
                string val = dt.Columns[i].Caption ?? dt.Columns[i].ColumnName;
                cell.SetCellValue(val);
            }
            int rowIndex = 1;
            for (int i = startRow; i <= endRow; i++) {
                DataRow dtRow = dt.Rows[i];
                IRow excelRow = sheet.CreateRow(rowIndex++);
                for (int j = 0; j < dtRow.ItemArray.Length; j++) {
                    excelRow.CreateCell(j).SetCellValue(dtRow[j].ToString());
                }
            }

        }

        #region 变量
        /// <summary>
        /// 程序中Excel的模板路径
        /// </summary>
        private string path = System.Windows.Forms.Application.StartupPath + "\\" + templeteFolderName + "\\";

        /// <summary>
        /// 程序中Excel的存放模板的文件夹名称
        /// </summary>
        private const string templeteFolderName = "ExcelTemplete";

        /// <summary>
        /// NPOI操作主对象
        /// </summary>
        HSSFWorkbook hssfworkbook = null;

        /// <summary>
        /// 字体对象
        /// </summary>
        private IFont font = null;

        private HSSFCellStyle cellStyle = null;
        #endregion

        #region 公开属性
        /// <summary>
        /// excel模板名称
        /// </summary>
        public string ExcelTempleteName {
            get;
            set;
        }

        /// <summary>
        /// 数据源
        /// </summary>
        public DataSet DS_Main {
            get;
            set;
        }

        public string TargetFileName
        {
            get;
            set;
        }

        #endregion

        #region 公开方法
        /// <summary>
        /// 根据模板直接导出excel(单个sheet)
        /// </summary>
        /// <param name="pSheetName">要加载的sheet名称</param>
        /// <param name="pRowNum">起始的行下标索引</param>
        /// <param name="pColoumSettingList">配置类</param>
        /// <param name="pCellAutoFit">是否自动宽度</param>
        public void ExportByDirectMode(string pSheetName, int pRowStartNum, IList<ColoumSetting> pColoumSettingList)
        {
            if (File.Exists(FullExcelTempletePath) == false)
            {
                throw new Exception("模板名称:" + ExcelTempleteName + "没找到！");
            }
            FileStream file = new FileStream(FullExcelTempletePath, FileMode.Open, FileAccess.Read);
            hssfworkbook = new HSSFWorkbook(file);
            file.Close();

            font = hssfworkbook.CreateFont();
            cellStyle = hssfworkbook.CreateCellStyle() as HSSFCellStyle;
            ISheet sheet = hssfworkbook.GetSheet(pSheetName);

            if (sheet == null)
            {
                throw new Exception("Excel没有名称为:" + pSheetName + "的Sheet！");
            }

            SetCellValue(sheet, 0, 0, TargetFileName);

            //透析男女比例男性人数

            int totalpeople = 0;
            for (int i = 0; i < DS_Main.Tables[0].Rows.Count; i++)
            {
                if (DS_Main.Tables[0].Rows[i]["ITEM_NAME"].ToString() == "透析男女比例男性人数")
                {
                    totalpeople +=  Utility.CInt(DS_Main.Tables[0].Rows[i]["ITEM_VALUE"].ToString());
                }
                else if (DS_Main.Tables[0].Rows[i]["ITEM_NAME"].ToString() == "透析男女比例女性人数")
                {
                    totalpeople += Utility.CInt(DS_Main.Tables[0].Rows[i]["ITEM_VALUE"].ToString());
                }
                FillText(DS_Main.Tables[0].Rows[i], pColoumSettingList[0], sheet);
            }
            string sick = string.Empty;
            for (int i = 0; i < _sickArray.Length; i++)
            {
                sick += _sickArray[i];
            }            
            SetCellValue(sheet, 31, 0, string.Format("以下请统计维持性血液透析病人（在本透析中心持续透析时间大于3个月的慢性肾衰竭病人，资料完整数据共{0}人统计在内 ）资料： ", totalpeople)); //统计总人数
            SetCellValue(sheet, 32, 1, sick);
        }

        void SetCellValue(ISheet sheet, int row, int cell,string values)
        {
            IRow firstRow = sheet.GetRow(row);
            ICell fisrtcell = firstRow.GetCell(cell);
            fisrtcell.SetCellValue(values);
        }      
        private string[] _sickArray = new string[8];

        /// <summary>
        /// 根据数据库的值填充cell
        /// </summary>
        /// <param name="dr"></param>
        /// <param name="setting"></param>
        /// <param name="sheet"></param>
        void FillText(DataRow dr, ColoumSetting setting, ISheet sheet)
        {
            setting.CellStyle_VerticalAlignment = NPOI.SS.UserModel.VerticalAlignment.Distributed;
            setting.CellStyle_HorizontalAlignment = NPOI.SS.UserModel.HorizontalAlignment.Center;
            #region  动态加载
            var ITEM_NAME = dr["ITEM_NAME"].ToString().Trim();
            var ITEM_VALUE = dr["ITEM_VALUE"].ToString().Trim();
            var EXTEND_COL = dr["EXTEND_COL"].ToString().Trim();
            var UPLOAD_TYPE = dr["UPLOAD_TYPE"].ToString().Trim();
            var a = string.Empty;

            
            switch (UPLOAD_TYPE)
            {
                case "科室信息":

                    if (ITEM_NAME == "HOSPITAL_NAME")
                    {
                        SetCellValue(sheet, 2, 1, ITEM_VALUE);
                    }
                    else if (ITEM_NAME == "HOSPITAL_LEVEL")
                    {
                        SetCellValue(sheet, 3, 1, ITEM_VALUE);
                    }
                    else if (ITEM_NAME == "CONTACT_PEOPLE")
                    {
                        SetCellValue(sheet, 4, 1, ITEM_VALUE);
                    }
                    else if (ITEM_NAME == "CONTACT_PHONE")
                    {
                        SetCellValue(sheet, 5, 1, ITEM_VALUE);
                    }
                    else if (ITEM_NAME == "CONTACT_EMAIL")
                    {
                        SetCellValue(sheet, 6, 1, ITEM_VALUE);
                    }
                    else if (ITEM_NAME == "HEAD_NURSE")
                    {
                        SetCellValue(sheet, 7, 1, ITEM_VALUE);
                    }
                    else if (ITEM_NAME == "HEAD_NURSE_PHONE")
                    {
                        SetCellValue(sheet, 8, 1, ITEM_VALUE);
                    }
                    else if (ITEM_NAME == "HEAD_NURSE_EMAIL")
                    {
                        SetCellValue(sheet, 9, 1, ITEM_VALUE);
                    }
                    else if (ITEM_NAME == "PHYSICIAN_COUNT")
                    {
                        SetCellValue(sheet, 10, 1, ITEM_VALUE);
                    }
                    else if (ITEM_NAME == "NURSE_COUNT")
                    {
                        SetCellValue(sheet, 11, 1, ITEM_VALUE);
                    }
                    else if (ITEM_NAME == "TECHNICIAN_COUNT")
                    {
                        SetCellValue(sheet, 12, 1, ITEM_VALUE);
                    }
                    else if (ITEM_NAME == "CRRT_MACHINECOUNT")
                    {
                        SetCellValue(sheet, 23, 1, ITEM_VALUE);
                    }
                    else if (ITEM_NAME == "CRRT_MODEL")
                    {
                        SetCellValue(sheet, 23, 2, ITEM_VALUE);
                    }
                    else if (ITEM_NAME == "CRRT_COUNT")
                    {
                        SetCellValue(sheet, 24, 1, ITEM_VALUE);
                    }
                    else if (ITEM_NAME == "ONEHEMOMACHINE_MODEL")
                    {
                        SetCellValue(sheet, 26, 1, ITEM_VALUE);
                    }
                    else if (ITEM_NAME == "ISHEMOMACHINE_MULTIPLEX")
                    {
                        SetCellValue(sheet, 27, 1, ITEM_VALUE);
                    }
                    else if (ITEM_NAME == "HEMOMACHINE_MODEL")
                    {
                        SetCellValue(sheet, 27, 2, ITEM_VALUE);
                    }
                    else if (ITEM_NAME == "MULTIPLEX_COUNT")
                    {
                        SetCellValue(sheet, 28, 1, ITEM_VALUE);
                    }

                    else if (ITEM_NAME == "MULTIPLEX_MODEL")
                    {
                        SetCellValue(sheet, 28, 2, ITEM_VALUE);
                    }
                    else if (ITEM_NAME == "MULTIPLEX_ANTISEPTIC_MODEL")
                    {
                        SetCellValue(sheet, 28, 3, ITEM_VALUE);
                    }
                    else if (ITEM_NAME == "DIALYSATE_CA")
                    {
                        SetCellValue(sheet, 29, 1, ITEM_VALUE);
                    }
                    else if (ITEM_NAME == "THERAPEUTIC_PROPERTIES")
                    {
                        SetCellValue(sheet, 15, 1, ITEM_VALUE);
                    }
                    break;
                case "设备信息":
                    if (ITEM_NAME == "血透机总台数")
                    {
                        SetCellValue(sheet, 20, 1, ITEM_VALUE);
                    }
                    else if (ITEM_NAME == "血透机台数、品牌")
                    {
                        SetCellValue(sheet, 21, 1, ITEM_VALUE);
                    }
                    else if (ITEM_NAME == "血滤机台数、品牌")
                    {
                        SetCellValue(sheet, 22, 1, ITEM_VALUE);

                    }
                    else if (ITEM_NAME == "水处理机台数、品牌")
                    {
                        SetCellValue(sheet, 25, 1, ITEM_VALUE);
                    }
                    break;
                case "检验信息":
                    if (ITEM_NAME == "高磷血症>=1.78")
                    {
                        SetCellValue(sheet, 39, 2, ITEM_VALUE);
                        SetCellValue(sheet, 39, 4, EXTEND_COL);

                    }
                    else if (ITEM_NAME == "高磷血症>=1.13 AND <1.78")
                    {
                        SetCellValue(sheet, 40, 2, ITEM_VALUE);
                        SetCellValue(sheet, 40, 4, EXTEND_COL);
                    }
                    else if (ITEM_NAME == "高磷血症<1.13")
                    {
                        SetCellValue(sheet, 41, 2, ITEM_VALUE);
                        SetCellValue(sheet, 41, 4, EXTEND_COL);
                    }
                    else if (ITEM_NAME == "钙>2.5")
                    {
                        SetCellValue(sheet, 42, 2, ITEM_VALUE);
                        SetCellValue(sheet, 42, 4, EXTEND_COL);
                    }
                    else if (ITEM_NAME == "钙>2.10 AND <=2.50")
                    {
                        SetCellValue(sheet, 43, 2, ITEM_VALUE);
                        SetCellValue(sheet, 43, 4, EXTEND_COL);
                    }
                    else if (ITEM_NAME == "钙<=2.10")
                    {
                        SetCellValue(sheet, 44, 2, ITEM_VALUE);
                        SetCellValue(sheet, 44, 4, EXTEND_COL);
                    }
                    else if (ITEM_NAME == "PTH<150")
                    {
                        SetCellValue(sheet, 45, 2, ITEM_VALUE);
                        SetCellValue(sheet, 45, 4, EXTEND_COL);
                    }
                    else if (ITEM_NAME == "PTH>=150 AND <=300")
                    {
                        SetCellValue(sheet, 46, 2, ITEM_VALUE);
                        SetCellValue(sheet, 46, 4, EXTEND_COL);
                    }
                    else if (ITEM_NAME == "PTH>=301 AND <=600")
                    {
                        SetCellValue(sheet, 47, 2, ITEM_VALUE);
                        SetCellValue(sheet, 47, 4, EXTEND_COL);

                    }
                    else if (ITEM_NAME == "PTH>600")
                    {
                        SetCellValue(sheet, 48, 2, ITEM_VALUE);
                        SetCellValue(sheet, 48, 4, EXTEND_COL);
                    }
                    else if (ITEM_NAME == "血红蛋白<110")
                    {
                        SetCellValue(sheet, 49, 2, ITEM_VALUE);
                        SetCellValue(sheet, 49, 4, EXTEND_COL);
                    }
                    else if (ITEM_NAME == "血红蛋白>=110 AND <=130")
                    {
                        SetCellValue(sheet, 50, 2, ITEM_VALUE);
                        SetCellValue(sheet, 50, 4, EXTEND_COL);
                    }
                    else if (ITEM_NAME == "血红蛋白>130")
                    {
                        SetCellValue(sheet, 51, 2, ITEM_VALUE);
                        SetCellValue(sheet, 51, 4, EXTEND_COL);
                    }
                    else if (ITEM_NAME == "白蛋白<30")
                    {
                        SetCellValue(sheet, 52, 2, ITEM_VALUE);
                        SetCellValue(sheet, 52, 4, EXTEND_COL);

                    }
                    else if (ITEM_NAME == "白蛋白>=30 AND <35")
                    {
                        SetCellValue(sheet, 53, 2, ITEM_VALUE);
                        SetCellValue(sheet, 53, 4, EXTEND_COL);
                    }
                    else if (ITEM_NAME == "白蛋白>=35 AND <=40")
                    {
                        SetCellValue(sheet, 54, 2, ITEM_VALUE);
                        SetCellValue(sheet, 54, 4, EXTEND_COL);
                    }
                    else if (ITEM_NAME == "白蛋白>40")
                    {
                        SetCellValue(sheet, 55, 2, ITEM_VALUE);
                        SetCellValue(sheet, 55, 4, EXTEND_COL);
                    }
                    else if (ITEM_NAME == "CRP>8")
                    {
                        SetCellValue(sheet, 56, 2, ITEM_VALUE);
                        SetCellValue(sheet, 56, 4, EXTEND_COL);
                    }
                    else if (ITEM_NAME == "铁蛋白<100")
                    {
                        SetCellValue(sheet, 57, 2, ITEM_VALUE);
                        SetCellValue(sheet, 57, 4, EXTEND_COL);
                    }
                    else if (ITEM_NAME == "铁蛋白>=100 AND <=500")
                    {
                        SetCellValue(sheet, 58, 2, ITEM_VALUE);
                        SetCellValue(sheet, 58, 4, EXTEND_COL);
                    }
                    else if (ITEM_NAME == "铁蛋白>500")
                    {
                        SetCellValue(sheet, 59, 2, ITEM_VALUE);
                        SetCellValue(sheet, 59, 4, EXTEND_COL);
                    }
                    else if (ITEM_NAME == "铁饱和度<25")
                    {
                        SetCellValue(sheet, 60, 2, ITEM_VALUE);
                        SetCellValue(sheet, 60, 4, EXTEND_COL);
                    }
                    else if (ITEM_NAME == "高钾血症>5.5")
                    {
                        SetCellValue(sheet, 61, 2, ITEM_VALUE);
                        SetCellValue(sheet, 61, 4, EXTEND_COL);
                    }
                    else if (ITEM_NAME == "二氧化碳结合力>30")
                    {
                        SetCellValue(sheet, 62, 2, ITEM_VALUE);
                        SetCellValue(sheet, 62, 4, EXTEND_COL);
                    }
                    else if (ITEM_NAME == "二氧化碳结合力<18")
                    {
                        SetCellValue(sheet, 63, 2, ITEM_VALUE);
                        SetCellValue(sheet, 63, 4, EXTEND_COL);
                    }
                    break;
                case "院感信息":

                    if (ITEM_NAME == "传染病例数乙型肝炎")
                    {
                        SetCellValue(sheet, 69, 2, ITEM_VALUE);
                        SetCellValue(sheet, 69, 4, EXTEND_COL);

                    }
                    else if (ITEM_NAME == "传染病例数丙型肝炎")
                    {
                        SetCellValue(sheet, 70, 2, ITEM_VALUE);
                        SetCellValue(sheet, 70, 4, EXTEND_COL);
                    }
                    else if (ITEM_NAME == "传染病例数除乙肝丙肝其他传染病")
                    {
                        SetCellValue(sheet, 71, 2, ITEM_VALUE);
                        SetCellValue(sheet, 71, 4, EXTEND_COL);
                    }
                    break;
                case "治疗信息":

                    if (ITEM_NAME == "透析男女比例男性人数")
                    {
                        SetCellValue(sheet, 33, 2, ITEM_VALUE);
                        SetCellValue(sheet, 33, 4, EXTEND_COL);

                    }
                    else if (ITEM_NAME == "透析男女比例女性人数")
                    {
                        SetCellValue(sheet, 34, 2, ITEM_VALUE);
                        SetCellValue(sheet, 34, 4, EXTEND_COL);
                    }
                    else if (ITEM_NAME == "透析年龄段20岁以下")
                    {
                        SetCellValue(sheet, 35, 2, ITEM_VALUE);
                        SetCellValue(sheet, 35, 4, EXTEND_COL);
                    }
                    else if (ITEM_NAME == "透析年龄段20-40岁")
                    {
                        SetCellValue(sheet, 36, 2, ITEM_VALUE);
                        SetCellValue(sheet, 36, 4, EXTEND_COL);
                    }
                    else if (ITEM_NAME == "透析年龄段41-60岁")
                    {
                        SetCellValue(sheet, 37, 2, ITEM_VALUE);
                        SetCellValue(sheet, 37, 4, EXTEND_COL);
                    }
                    else if (ITEM_NAME == "透析年龄段60岁以上")
                    {
                        SetCellValue(sheet, 38, 2, ITEM_VALUE);
                        SetCellValue(sheet, 38, 4, EXTEND_COL);
                    }
                    else if (ITEM_NAME == "规律透析比例每周3次")
                    {
                        SetCellValue(sheet, 72, 2, ITEM_VALUE);
                        SetCellValue(sheet, 72, 4, EXTEND_COL);
                    }
                    else if (ITEM_NAME == "规律透析比例每周2次")
                    {
                        SetCellValue(sheet, 73, 2, ITEM_VALUE);
                        SetCellValue(sheet, 73, 4, EXTEND_COL);
                    }

                    else if (ITEM_NAME == "规律透析比例每周5次")
                    {
                        SetCellValue(sheet, 74, 2, ITEM_VALUE);
                        SetCellValue(sheet, 74, 4, EXTEND_COL);
                    }
                    else if (ITEM_NAME == "规律透析比例每周4次")
                    {
                        SetCellValue(sheet, 75, 2, ITEM_VALUE);
                        SetCellValue(sheet, 75, 4, EXTEND_COL);
                    }
                    else if (ITEM_NAME == "规律透析比例2周5次")
                    {
                        SetCellValue(sheet, 76, 2, ITEM_VALUE);
                        SetCellValue(sheet, 76, 4, EXTEND_COL);
                    }

                    else if (ITEM_NAME == "规律透析比例无规律")
                    {
                        SetCellValue(sheet, 77, 1, "无规律:" + ITEM_VALUE + "人，比例:" + EXTEND_COL);
                    }
                    else if (ITEM_NAME == "血管通路内瘘例数")
                    {
                        SetCellValue(sheet, 64, 2, ITEM_VALUE);
                        SetCellValue(sheet, 64, 4, EXTEND_COL);
                    }
                    else if (ITEM_NAME == "血管通路移植物内瘘")
                    {
                        SetCellValue(sheet, 65, 2, ITEM_VALUE);
                        SetCellValue(sheet, 65, 4, EXTEND_COL);
                    }

                    else if (ITEM_NAME == "血管通路双静脉例数")
                    {
                        SetCellValue(sheet, 66, 2, ITEM_VALUE);
                        SetCellValue(sheet, 66, 4, EXTEND_COL);
                    }
                    else if (ITEM_NAME == "血管通路带cuff中心静脉留置导管")
                    {
                        SetCellValue(sheet, 67, 2, ITEM_VALUE);
                        SetCellValue(sheet, 67, 4, EXTEND_COL);
                    }
                    else if (ITEM_NAME == "血管通路其他通路例数")
                    {
                        SetCellValue(sheet, 68, 2, ITEM_VALUE);
                        SetCellValue(sheet, 68, 4, EXTEND_COL);
                    }                       
                    else if (ITEM_NAME == "年度总透析例次")
                    {
                        SetCellValue(sheet, 13, 1, ITEM_VALUE);
                    }
                    else if (ITEM_NAME == "维持性透析人数")
                    {
                        SetCellValue(sheet, 14, 1, ITEM_VALUE);
                    }
                    else if (ITEM_NAME == "年死亡病人数，占维持性透析病人比例")
                    {
                        SetCellValue(sheet, 30, 1, ITEM_VALUE);
                        SetCellValue(sheet, 30, 2, EXTEND_COL);
                    }
                    else if (ITEM_NAME == "原发病统计高血压肾病")
                    {
                        _sickArray[0] = ITEM_NAME.Substring(5, ITEM_NAME.Length - 5) + ITEM_VALUE + "人（" + EXTEND_COL + "),";
                    }
                    else if (ITEM_NAME == "原发病统计梗阻性肾病")
                    {
                        _sickArray[1] = ITEM_NAME.Substring(5, ITEM_NAME.Length - 5) + ITEM_VALUE + "人（" + EXTEND_COL + "),";
                    }
                    else if (ITEM_NAME == "原发病统计慢性肾小球肾炎")
                    {
                        _sickArray[2] = ITEM_NAME.Substring(5, ITEM_NAME.Length - 5) + ITEM_VALUE + "人（" + EXTEND_COL + "),";
                    }
                    else if (ITEM_NAME == "原发病统计糖尿病肾病")
                    {
                        _sickArray[3] = ITEM_NAME.Substring(5, ITEM_NAME.Length - 5) + ITEM_VALUE + "人（" + EXTEND_COL + "),";
                    }
                    else if (ITEM_NAME == "原发病统计痛风性肾病")
                    {
                        _sickArray[4] = ITEM_NAME.Substring(5, ITEM_NAME.Length - 5) + ITEM_VALUE + "人（" + EXTEND_COL + "),";
                    }
                    else if (ITEM_NAME == "原发病统计多囊肾")
                    {
                        _sickArray[5] = ITEM_NAME.Substring(5, ITEM_NAME.Length - 5) + ITEM_VALUE + "人（" + EXTEND_COL + "),";
                    }
                    else if (ITEM_NAME == "原发病统计病因不详")
                    {
                        _sickArray[6] = ITEM_NAME.Substring(5, ITEM_NAME.Length - 5) + ITEM_VALUE + "人（" + EXTEND_COL + "),";
                    }
                    else if (ITEM_NAME == "原发病统计其它原发病")
                    {
                        _sickArray[7] = ITEM_NAME.Substring(5, ITEM_NAME.Length - 5) + ITEM_VALUE + "人（" + EXTEND_COL + ")";
                    }
                    else if (ITEM_NAME == "手术临时动静脉内瘘术") //以下部分为手术部分
                    {
                        if (Utility.CInt(ITEM_NAME.ToString()) > 0)
                        {
                            SetCellValue(sheet, 16, 1, "已开展");
                            SetCellValue(sheet, 16, 3, ITEM_VALUE);
                        }
                        else {
                            SetCellValue(sheet, 16, 1, "未开展");
                        }
                       
                    }
                    else if (ITEM_NAME == "手术半永久导管深静脉置入术")
                    {
                        if (Utility.CInt(ITEM_NAME.ToString()) > 0)
                        {
                            SetCellValue(sheet, 18, 1, "已开展");
                            SetCellValue(sheet, 18, 3, ITEM_VALUE);
                        }
                        else
                        {
                            SetCellValue(sheet, 18, 1, "未开展");
                        }                      
                    }
                    break;
                default:
                    break;

            }
            #endregion
        }

        /// <summary>
        /// 根据模板直接导出excel(单个sheet)并可自动填充任何区域文字
        /// </summary>
        /// <param name="pSheetName">要加载的sheet名称</param>
        /// <param name="pRowNum">起始的行下标索引</param>
        /// <param name="pColoumSettingList">配置类</param>
        /// <param name="pCellAutoFit">是否自动宽度</param>
        /// <param name="pCustomerSettingList">自定义单元格</param>
        public void ExportByDirectMode(string pSheetName, int pRowStartNum, IList<ColoumSetting> pColoumSettingList, IList<CustomerSetting> CustomerSettingList) {
            if (File.Exists(FullExcelTempletePath) == false) {
                throw new Exception("模板名称:" + ExcelTempleteName + "没找到！");
            }
            FileStream file = new FileStream(FullExcelTempletePath, FileMode.Open, FileAccess.Read);
            hssfworkbook = new HSSFWorkbook(file);
            file.Close();

            font = hssfworkbook.CreateFont();
            cellStyle = hssfworkbook.CreateCellStyle() as HSSFCellStyle;
            ISheet sheet = hssfworkbook.GetSheet(pSheetName);
            if (sheet == null) {
                throw new Exception("Excel没有名称为:" + pSheetName + "的Sheet！");
            }
            for (int i = 0; i < DS_Main.Tables[0].Rows.Count; i++) {
                foreach (ColoumSetting setting in pColoumSettingList) {
                    CreateRowCell(pRowStartNum, setting.CellNum, sheet);
                    SetValue(pRowStartNum, setting.CellNum, DS_Main.Tables[0].Rows[i][setting.ColumnName], setting, sheet);
                }
                pRowStartNum++;
            }
            if (CustomerSettingList != null) {
                foreach (CustomerSetting customerSetting in CustomerSettingList) {
                    sheet = hssfworkbook.GetSheet(customerSetting.SheetName);
                    if (sheet == null) {
                        throw new Exception("Excel没有名称为:" + pSheetName + "的Sheet！");
                    }
                    if (customerSetting.CellRange != null) {
                        sheet.AddMergedRegion(customerSetting.CellRange);
                    }
                    CreateRowCell(customerSetting.RowNum, customerSetting.CellNum, sheet);
                    SetValue(customerSetting.RowNum, customerSetting.CellNum, customerSetting.Value, customerSetting, sheet);

                }
            }
        }

        /// <summary>
        /// 根据模板导出excel多个sheet方法
        /// </summary>
        /// <param name="pMutiSheetSettingList">sheet对象</param>
        public void ExportMutiByDirectMode(IList<MutiSheetSetting> pMutiSheetSettingList) {
            if (File.Exists(FullExcelTempletePath) == false) {
                throw new Exception("模板名称:" + ExcelTempleteName + "没找到！");
            }
            FileStream file = new FileStream(FullExcelTempletePath, FileMode.Open, FileAccess.Read);
            hssfworkbook = new HSSFWorkbook(file);
            file.Close();

            font = hssfworkbook.CreateFont();
            cellStyle = hssfworkbook.CreateCellStyle() as HSSFCellStyle;
            foreach (MutiSheetSetting mutiSheetSetting in pMutiSheetSettingList) {
                ISheet sheet = hssfworkbook.GetSheet(mutiSheetSetting.SheetName);
                if (sheet == null) {
                    throw new Exception("Excel没有名称为:" + mutiSheetSetting.SheetName + "的Sheet！");
                }
                int rowStartNum = mutiSheetSetting.RowStartNum;
                for (int i = 0; i < DS_Main.Tables[mutiSheetSetting.TableName].Rows.Count; i++) {
                    foreach (ColoumSetting setting in mutiSheetSetting.pColoumSettingList) {
                        CreateRowCell(rowStartNum, setting.CellNum, sheet);
                        SetValue(rowStartNum, setting.CellNum, DS_Main.Tables[mutiSheetSetting.TableName].Rows[i][setting.ColumnName], setting, sheet);
                    }
                    rowStartNum++;
                }
            }
        }

        /// <summary>
        /// 根据模板导出excel多个sheet方法
        /// </summary>
        /// <param name="pMutiSheetSettingList">sheet对象</param>
        /// <param name="pCustomerSettingList">自定义单元格</param>
        public void ExportMutiByDirectMode(IList<MutiSheetSetting> pMutiSheetSettingList, IList<CustomerSetting> CustomerSettingList) {
            if (File.Exists(FullExcelTempletePath) == false) {
                throw new Exception("模板名称:" + ExcelTempleteName + "没找到！");
            }
            FileStream file = new FileStream(FullExcelTempletePath, FileMode.Open, FileAccess.Read);
            hssfworkbook = new HSSFWorkbook(file);
            file.Close();

            font = hssfworkbook.CreateFont();
            cellStyle = hssfworkbook.CreateCellStyle() as HSSFCellStyle;
            if (pMutiSheetSettingList != null) {
                foreach (MutiSheetSetting mutiSheetSetting in pMutiSheetSettingList) {
                    ISheet sheet = hssfworkbook.GetSheet(mutiSheetSetting.SheetName);
                    if (sheet == null) {
                        throw new Exception("Excel没有名称为:" + mutiSheetSetting.SheetName + "的Sheet！");
                    }
                    int rowStartNum = mutiSheetSetting.RowStartNum;
                    for (int i = 0; i < DS_Main.Tables[mutiSheetSetting.TableName].Rows.Count; i++) {
                        foreach (ColoumSetting setting in mutiSheetSetting.pColoumSettingList) {
                            CreateRowCell(rowStartNum, setting.CellNum, sheet);
                            SetValue(rowStartNum, setting.CellNum, DS_Main.Tables[mutiSheetSetting.TableName].Rows[i][setting.ColumnName], setting, sheet);
                        }
                        rowStartNum++;
                    }
                }
            }
            if (CustomerSettingList != null) {
                foreach (CustomerSetting customerSetting in CustomerSettingList) {
                    ISheet sheet = hssfworkbook.GetSheet(customerSetting.SheetName);
                    if (sheet == null) {
                        throw new Exception("Excel没有名称为:" + customerSetting.SheetName + "的Sheet！");
                    }

                    if (customerSetting.CellRange != null) {
                        sheet.AddMergedRegion(customerSetting.CellRange);
                    }

                    CreateRowCell(customerSetting.RowNum, customerSetting.CellNum, sheet);
                    SetValue(customerSetting.RowNum, customerSetting.CellNum, customerSetting.Value, customerSetting, sheet);
                }
            }
        }

        /// <summary>
        /// 根据模板导出excel(直接根据指定的区域填充)
        /// </summary>
        /// <param name="pCustomerSettingList">自定义单元格</param>
        public void ExportByCustomerDirectMode(IList<CustomerSetting> CustomerSettingList) {
            if (File.Exists(FullExcelTempletePath) == false) {
                throw new Exception("模板名称:" + ExcelTempleteName + "没找到！");
            }
            FileStream file = new FileStream(FullExcelTempletePath, FileMode.Open, FileAccess.Read);
            hssfworkbook = new HSSFWorkbook(file);
            file.Close();

            font = hssfworkbook.CreateFont();
            cellStyle = hssfworkbook.CreateCellStyle() as HSSFCellStyle;
            if (CustomerSettingList != null) {
                foreach (CustomerSetting customerSetting in CustomerSettingList) {
                    ISheet sheet = hssfworkbook.GetSheet(customerSetting.SheetName);
                    if (sheet == null) {
                        throw new Exception("Excel没有名称为:" + customerSetting.SheetName + "的Sheet！");
                    }

                    if (customerSetting.CellRange != null) {
                        sheet.AddMergedRegion(customerSetting.CellRange);
                    }

                    CreateRowCell(customerSetting.RowNum, customerSetting.CellNum, sheet);
                    SetValue(customerSetting.RowNum, customerSetting.CellNum, customerSetting.Value, customerSetting, sheet);
                }
            }
        }
        #endregion

        #region 私有属性
        /// <summary>
        /// excel路径
        /// </summary>
        private string FullExcelTempletePath {
            get {
                return path + ExcelTempleteName;
            }
        }
        #endregion

        #region 私有方法
        /// <summary>
        /// 创建单元行和单元格
        /// </summary>
        /// <param name="pRowNum">行号</param>
        /// <param name="pCellNum">单元格</param>
        /// <param name="sheet1">excel表sheet</param>
        public void CreateRowCell(int pRowNum, int pCellNum, ISheet sheet, short? pRowHeight) {
            if (sheet.GetRow(pRowNum) == null) {
                IRow row = sheet.CreateRow(pRowNum);
                if (row != null && pRowHeight.HasValue) {
                    row.Height = pRowHeight.Value;
                }
            }
            if (sheet.GetRow(pRowNum).GetCell(pCellNum) == null) {
                sheet.GetRow(pRowNum).CreateCell(pCellNum);
            }
        }

        /// <summary>
        /// 创建单元行和单元格
        /// </summary>
        /// <param name="pRowNum">行号</param>
        /// <param name="pCellNum">单元格</param>
        /// <param name="sheet1">excel表sheet</param>
        public void CreateRowCell(int pRowNum, int pCellNum, ISheet sheet) {
            if (sheet.GetRow(pRowNum) == null) {
                sheet.CreateRow(pRowNum);
            }
            if (sheet.GetRow(pRowNum).GetCell(pCellNum) == null) {
                sheet.GetRow(pRowNum).CreateCell(pCellNum);
            }
        }

        /// <summary>
        /// 合并单元格
        /// </summary>
        /// <param name="pRowNum">行号</param>
        /// <param name="pCellNum">单元格</param>
        /// <param name="sheet1">excel表sheet</param>
        public void AddMergedRegion(int pStartRow, int pEndRow, int pStartCell, int pEndCell, ISheet sheet) {
            //CellRangeAddress四个参数为：起始行，结束行，起始列，结束列
            sheet.AddMergedRegion(new CellRangeAddress(pStartRow, pEndRow, pStartCell, pEndCell));
        }


        /// <summary>
        /// 设置值并设置格式
        /// </summary>
        /// <param name="pRowNum">行号</param>
        /// <param name="pCellNum">单元格</param>
        /// <param name="pValue">值</param>
        /// <param name="pColoumSetting">列设置对象</param>
        /// <param name="sheet1">excel表sheet</param>
        public void SetValue(int pRowNum, int pCellNum, object pValue, ColoumSetting pColoumSetting, ISheet sheet) {
            var cell = sheet.GetRow(pRowNum).GetCell(pCellNum);

            if (font == null) {
                font = hssfworkbook.CreateFont();
            }
            
            #region 设置字体
            if (pColoumSetting.FontColor > 0) {
                font.Color = pColoumSetting.FontColor;
            }

            if (!string.IsNullOrEmpty(pColoumSetting.FontName)) {
                font.FontName = pColoumSetting.FontName;
            }
            else {
                font.FontName = "宋体";
            }
            font.FontHeightInPoints = 12;
           
            font.IsItalic = pColoumSetting.IsItalic;
            font.Boldweight = pColoumSetting.FontBold ? short.MaxValue : short.MinValue;
            #endregion

            #region 单元格内容水平对齐
            cellStyle.Alignment = pColoumSetting.CellStyle_HorizontalAlignment;
            #endregion

            #region 单元格内容垂直对齐
            cellStyle.VerticalAlignment = pColoumSetting.CellStyle_VerticalAlignment;
            #endregion

            #region 单元格自动换行
            cellStyle.WrapText = pColoumSetting.CellStyle_WrapText;
            #endregion

            #region 单元格边框
            cellStyle.BorderBottom = pColoumSetting.CellStyle_Border == NPOI.SS.UserModel.BorderStyle.None ? NPOI.SS.UserModel.BorderStyle.Thin : pColoumSetting.CellStyle_Border;
            cellStyle.BorderLeft = pColoumSetting.CellStyle_Border == NPOI.SS.UserModel.BorderStyle.None ? NPOI.SS.UserModel.BorderStyle.Thin : pColoumSetting.CellStyle_Border;
            cellStyle.BorderRight = pColoumSetting.CellStyle_Border == NPOI.SS.UserModel.BorderStyle.None ? NPOI.SS.UserModel.BorderStyle.Thin : pColoumSetting.CellStyle_Border;
            cellStyle.BorderTop = pColoumSetting.CellStyle_Border == NPOI.SS.UserModel.BorderStyle.None ? NPOI.SS.UserModel.BorderStyle.Thin : pColoumSetting.CellStyle_Border;
            #endregion

            #region 单元格边框颜色
            cellStyle.BottomBorderColor = pColoumSetting.CellStyle_BorderColor == 0 ? HSSFColor.Black.Index : pColoumSetting.CellStyle_BorderColor;
            cellStyle.LeftBorderColor = pColoumSetting.CellStyle_BorderColor == 0 ? HSSFColor.Black.Index : pColoumSetting.CellStyle_BorderColor;
            cellStyle.RightBorderColor = pColoumSetting.CellStyle_BorderColor == 0 ? HSSFColor.Black.Index : pColoumSetting.CellStyle_BorderColor;
            cellStyle.TopBorderColor = pColoumSetting.CellStyle_BorderColor == 0 ? HSSFColor.Black.Index : pColoumSetting.CellStyle_BorderColor;
            #endregion

            #region 背景色
            cellStyle.FillBackgroundColor = pColoumSetting.FillBackgroundColor == 0 ? HSSFColor.White.Index : pColoumSetting.FillBackgroundColor;
            #endregion

            #region 前景色
            cellStyle.FillForegroundColor = pColoumSetting.FillForegroundColor == 0 ? HSSFColor.White.Index : pColoumSetting.FillForegroundColor;
            #endregion

            #region 设置值并加格式化处理
            switch (pValue.GetType().ToString()) {
                case "System.Int16":
                case "System.Int32":
                case "System.Int64":
                    cell.SetCellValue(pValue.ToString());
                    if (!string.IsNullOrEmpty(pColoumSetting.FormatStyle)) {
                        HSSFDataFormat format = hssfworkbook.CreateDataFormat() as HSSFDataFormat;
                        cellStyle.DataFormat = format.GetFormat(pColoumSetting.FormatStyle);
                    }
                    break;
                case "System.String":
                    cell.SetCellValue(pValue.ToString());
                    break;
                case "System.Boolean":
                    cell.SetCellValue(pValue.ToString());
                    break;
                case "System.DateTime":
                    //cell.SetCellValue(DateTime.Parse(pValue.ToString()));
                    //if (!string.IsNullOrEmpty(pColoumSetting.FormatStyle))
                    //{
                    //    HSSFDataFormat format = hssfworkbook.CreateDataFormat() as HSSFDataFormat;
                    //    cellStyle.DataFormat = format.GetFormat(pColoumSetting.FormatStyle);
                    //}
                    if (!string.IsNullOrEmpty(pColoumSetting.FormatStyle)) {
                        cell.SetCellValue(DateTime.Parse(pValue.ToString()).ToString(pColoumSetting.FormatStyle));
                    }
                    else {
                        cell.SetCellValue(DateTime.Parse(pValue.ToString()).ToString("yyyy-MM-dd HH:mm:ss"));
                    }
                    break;
                case "System.Double":
                    cell.SetCellValue(pValue.ToString());
                    if (!string.IsNullOrEmpty(pColoumSetting.FormatStyle)) {
                        HSSFDataFormat format = hssfworkbook.CreateDataFormat() as HSSFDataFormat;
                        cellStyle.DataFormat = format.GetFormat(pColoumSetting.FormatStyle);
                    }
                    break;
                case "System.Decimal":
                    cell.SetCellValue(pValue.ToString());
                    if (!string.IsNullOrEmpty(pColoumSetting.FormatStyle)) {
                        HSSFDataFormat format = hssfworkbook.CreateDataFormat() as HSSFDataFormat;
                        cellStyle.DataFormat = format.GetFormat(pColoumSetting.FormatStyle);
                    }
                    break;
                default:
                    cell.SetCellValue(pValue.ToString());
                    break;

            }
            #endregion
            cellStyle.SetFont(font);
            cell.CellStyle = cellStyle;
            font = null;
        }

        /// <summary>
        /// 设置值并设置格式
        /// </summary>
        /// <param name="pRowNum">行号</param>
        /// <param name="pCellNum">单元格</param>
        /// <param name="pValue">值</param>
        /// <param name="pColoumSetting">列设置对象</param>
        /// <param name="sheet1">excel表sheet</param>
        public void SetValue(int pRowNum, int pCellNum, object pValue, ColoumSetting pColoumSetting, ISheet sheet, HSSFCellStyle pCellStyle, int pFirstColumn, int pLastColumn) {
            var cell = sheet.GetRow(pRowNum).GetCell(pCellNum);


            if (font == null) {
                font = hssfworkbook.CreateFont();
            }
            #region 设置字体
            if (pColoumSetting.FontColor > 0) {
                font.Color = pColoumSetting.FontColor;
            }

            if (!string.IsNullOrEmpty(pColoumSetting.FontName)) {
                font.FontName = pColoumSetting.FontName;
            }
            else {
                font.FontName = "宋体";
            }
            font.IsItalic = pColoumSetting.IsItalic;
            font.Boldweight = pColoumSetting.FontBold ? short.MaxValue : short.MinValue;

            #endregion

            #region 单元格内容水平对齐
            pCellStyle.Alignment = pColoumSetting.CellStyle_HorizontalAlignment;
            #endregion

            #region 单元格内容垂直对齐
            pCellStyle.VerticalAlignment = pColoumSetting.CellStyle_VerticalAlignment;
            #endregion

            #region 单元格自动换行
            pCellStyle.WrapText = pColoumSetting.CellStyle_WrapText;
            #endregion

            #region 单元格边框
            pCellStyle.BorderBottom = pColoumSetting.CellStyle_Border == NPOI.SS.UserModel.BorderStyle.None ? NPOI.SS.UserModel.BorderStyle.Thin : pColoumSetting.CellStyle_Border;
            pCellStyle.BorderLeft = pColoumSetting.CellStyle_Border == NPOI.SS.UserModel.BorderStyle.None ? NPOI.SS.UserModel.BorderStyle.Thin : pColoumSetting.CellStyle_Border;
            pCellStyle.BorderRight = pColoumSetting.CellStyle_Border == NPOI.SS.UserModel.BorderStyle.None ? NPOI.SS.UserModel.BorderStyle.Thin : pColoumSetting.CellStyle_Border;
            pCellStyle.BorderTop = pColoumSetting.CellStyle_Border == NPOI.SS.UserModel.BorderStyle.None ? NPOI.SS.UserModel.BorderStyle.Thin : pColoumSetting.CellStyle_Border;
            #endregion

            #region 单元格边框颜色
            pCellStyle.BottomBorderColor = pColoumSetting.CellStyle_BorderColor == 0 ? HSSFColor.Black.Index : pColoumSetting.CellStyle_BorderColor;
            pCellStyle.LeftBorderColor = pColoumSetting.CellStyle_BorderColor == 0 ? HSSFColor.Black.Index : pColoumSetting.CellStyle_BorderColor;
            pCellStyle.RightBorderColor = pColoumSetting.CellStyle_BorderColor == 0 ? HSSFColor.Black.Index : pColoumSetting.CellStyle_BorderColor;
            pCellStyle.TopBorderColor = pColoumSetting.CellStyle_BorderColor == 0 ? HSSFColor.Black.Index : pColoumSetting.CellStyle_BorderColor;
            #endregion

            #region 背景色
            pCellStyle.FillBackgroundColor = pColoumSetting.FillBackgroundColor == 0 ? HSSFColor.White.Index : pColoumSetting.FillBackgroundColor;
            #endregion

            #region 前景色
            pCellStyle.FillForegroundColor = pColoumSetting.FillForegroundColor == 0 ? HSSFColor.White.Index : pColoumSetting.FillForegroundColor;
            #endregion

            #region 设置值并加格式化处理
            switch (pValue.GetType().ToString()) {
                case "System.Int16":
                case "System.Int32":
                case "System.Int64":
                    cell.SetCellValue(pValue.ToString());
                    if (!string.IsNullOrEmpty(pColoumSetting.FormatStyle)) {
                        HSSFDataFormat format = hssfworkbook.CreateDataFormat() as HSSFDataFormat;
                        pCellStyle.DataFormat = format.GetFormat(pColoumSetting.FormatStyle);
                    }
                    break;
                case "System.String":
                    cell.SetCellValue(pValue.ToString());
                    break;
                case "System.Boolean":
                    cell.SetCellValue(pValue.ToString());
                    break;
                case "System.DateTime":
                    cell.SetCellValue(DateTime.Parse(pValue.ToString()));
                    if (!string.IsNullOrEmpty(pColoumSetting.FormatStyle)) {
                        HSSFDataFormat format = hssfworkbook.CreateDataFormat() as HSSFDataFormat;
                        pCellStyle.DataFormat = format.GetFormat(pColoumSetting.FormatStyle);
                    }
                    break;
                case "System.Double":
                    cell.SetCellValue(pValue.ToString());
                    if (!string.IsNullOrEmpty(pColoumSetting.FormatStyle)) {
                        HSSFDataFormat format = hssfworkbook.CreateDataFormat() as HSSFDataFormat;
                        pCellStyle.DataFormat = format.GetFormat(pColoumSetting.FormatStyle);
                    }
                    break;
                case "System.Decimal":
                    cell.SetCellValue(pValue.ToString());
                    if (!string.IsNullOrEmpty(pColoumSetting.FormatStyle)) {
                        HSSFDataFormat format = hssfworkbook.CreateDataFormat() as HSSFDataFormat;
                        pCellStyle.DataFormat = format.GetFormat(pColoumSetting.FormatStyle);
                    }
                    break;
                default:
                    cell.SetCellValue(pValue.ToString());
                    break;

            }
            #endregion
            pCellStyle.SetFont(font);

            IRow row = sheet.GetRow(pRowNum);

            for (int j = pFirstColumn; j <= pLastColumn; j++) {
                ICell singleCell = HSSFCellUtil.GetCell(row, (short)j);
                singleCell.CellStyle = pCellStyle;
            }

        }

        /// <summary>
        /// 设置值并设置格式
        /// </summary>
        /// <param name="pRowNum">行号</param>
        /// <param name="pCellNum">单元格</param>
        /// <param name="pValue">值</param>
        /// <param name="pColoumSetting">列设置对象</param>
        /// <param name="sheet1">excel表sheet</param>
        public void SetValue(int pRowNum, int pCellNum, object pValue, ColoumSetting pColoumSetting, ISheet sheet, HSSFCellStyle pCellStyle) {
            var cell = sheet.GetRow(pRowNum).GetCell(pCellNum);

            if (font == null) {
                font = hssfworkbook.CreateFont();
            }
            #region 设置字体
            if (pColoumSetting.FontColor > 0) {
                font.Color = pColoumSetting.FontColor;
            }

            if (!string.IsNullOrEmpty(pColoumSetting.FontName)) {
                font.FontName = pColoumSetting.FontName;
            }
            else {
                font.FontName = "宋体";
            }
            font.IsItalic = pColoumSetting.IsItalic;
            font.Boldweight = pColoumSetting.FontBold ? short.MaxValue : short.MinValue;

            #endregion

            #region 单元格内容水平对齐
            pCellStyle.Alignment = pColoumSetting.CellStyle_HorizontalAlignment;
            #endregion

            #region 单元格内容垂直对齐
            pCellStyle.VerticalAlignment = pColoumSetting.CellStyle_VerticalAlignment;
            #endregion

            #region 单元格自动换行
            pCellStyle.WrapText = pColoumSetting.CellStyle_WrapText;
            #endregion

            #region 单元格边框
            pCellStyle.BorderBottom = pColoumSetting.CellStyle_Border == NPOI.SS.UserModel.BorderStyle.None ? NPOI.SS.UserModel.BorderStyle.Thin : pColoumSetting.CellStyle_Border;
            pCellStyle.BorderLeft = pColoumSetting.CellStyle_Border == NPOI.SS.UserModel.BorderStyle.None ? NPOI.SS.UserModel.BorderStyle.Thin : pColoumSetting.CellStyle_Border;
            pCellStyle.BorderRight = pColoumSetting.CellStyle_Border == NPOI.SS.UserModel.BorderStyle.None ? NPOI.SS.UserModel.BorderStyle.Thin : pColoumSetting.CellStyle_Border;
            pCellStyle.BorderTop = pColoumSetting.CellStyle_Border == NPOI.SS.UserModel.BorderStyle.None ? NPOI.SS.UserModel.BorderStyle.Thin : pColoumSetting.CellStyle_Border;
            #endregion

            #region 单元格边框颜色
            pCellStyle.BottomBorderColor = pColoumSetting.CellStyle_BorderColor == 0 ? HSSFColor.Black.Index : pColoumSetting.CellStyle_BorderColor;
            pCellStyle.LeftBorderColor = pColoumSetting.CellStyle_BorderColor == 0 ? HSSFColor.Black.Index : pColoumSetting.CellStyle_BorderColor;
            pCellStyle.RightBorderColor = pColoumSetting.CellStyle_BorderColor == 0 ? HSSFColor.Black.Index : pColoumSetting.CellStyle_BorderColor;
            pCellStyle.TopBorderColor = pColoumSetting.CellStyle_BorderColor == 0 ? HSSFColor.Black.Index : pColoumSetting.CellStyle_BorderColor;
            #endregion

            #region 背景色
            pCellStyle.FillBackgroundColor = pColoumSetting.FillBackgroundColor == 0 ? HSSFColor.White.Index : pColoumSetting.FillBackgroundColor;
            #endregion

            #region 前景色
            pCellStyle.FillForegroundColor = pColoumSetting.FillForegroundColor == 0 ? HSSFColor.White.Index : pColoumSetting.FillForegroundColor;
            #endregion

            #region 设置值并加格式化处理
            switch (pValue.GetType().ToString()) {
                case "System.Int16":
                case "System.Int32":
                case "System.Int64":
                    //  cell.SetCellValue(ComnonFunction.Utility.ConvertValue<double>(pValue));
                    cell.SetCellValue(Utility.CDouble(pValue.ToString()));
                    if (!string.IsNullOrEmpty(pColoumSetting.FormatStyle)) {
                        HSSFDataFormat format = hssfworkbook.CreateDataFormat() as HSSFDataFormat;
                        pCellStyle.DataFormat = format.GetFormat(pColoumSetting.FormatStyle);
                    }
                    break;
                case "System.String":
                    cell.SetCellValue(pValue.ToString());
                    break;
                case "System.Boolean":
                    cell.SetCellValue(pValue.ToString());
                    break;
                case "System.DateTime":
                    cell.SetCellValue(DateTime.Parse(pValue.ToString()));
                    if (!string.IsNullOrEmpty(pColoumSetting.FormatStyle)) {
                        HSSFDataFormat format = hssfworkbook.CreateDataFormat() as HSSFDataFormat;
                        pCellStyle.DataFormat = format.GetFormat(pColoumSetting.FormatStyle);
                    }
                    break;
                case "System.Double":
                    cell.SetCellValue(pValue.ToString());
                    if (!string.IsNullOrEmpty(pColoumSetting.FormatStyle)) {
                        HSSFDataFormat format = hssfworkbook.CreateDataFormat() as HSSFDataFormat;
                        pCellStyle.DataFormat = format.GetFormat(pColoumSetting.FormatStyle);
                    }
                    break;
                case "System.Decimal":
                    cell.SetCellValue(Utility.CDouble(pValue.ToString()));

                    // cell.SetCellValue(ComnonFunction.Utility.ConvertValue<double>(pValue));
                    if (!string.IsNullOrEmpty(pColoumSetting.FormatStyle)) {
                        HSSFDataFormat format = hssfworkbook.CreateDataFormat() as HSSFDataFormat;
                        pCellStyle.DataFormat = format.GetFormat(pColoumSetting.FormatStyle);
                    }
                    break;
                default:
                    cell.SetCellValue(pValue.ToString());
                    break;

            }
            #endregion
            pCellStyle.SetFont(font);
            cell.CellStyle = pCellStyle;
            font = null;
        }

        /// <summary>
        /// 设置值并设置格式
        /// </summary>
        /// <param name="pRowNum">行号</param>
        /// <param name="pCellNum">单元格</param>
        /// <param name="pValue">值</param>
        /// <param name="pColoumSetting">列设置对象</param>
        /// <param name="sheet1">excel表sheet</param>
        public void SetEvaluateValue(int pRowNum, int pCellNum, string pCellFormula, ColoumSetting pColoumSetting, ISheet sheet, HSSFCellStyle pCellStyle) {
            var cell = sheet.GetRow(pRowNum).GetCell(pCellNum);

            if (!string.IsNullOrEmpty(pCellFormula)) {
                HSSFFormulaEvaluator e = new HSSFFormulaEvaluator(hssfworkbook);
                cell.SetCellFormula(String.Format(pCellFormula));
                cell = e.EvaluateInCell(cell);
                cell.SetCellValue(cell.NumericCellValue);
            }
            if (font == null) {
                font = hssfworkbook.CreateFont();
            }
            #region 设置字体
            if (pColoumSetting.FontColor > 0) {
                font.Color = pColoumSetting.FontColor;
            }

            if (!string.IsNullOrEmpty(pColoumSetting.FontName)) {
                font.FontName = pColoumSetting.FontName;
            }
            else {
                font.FontName = "宋体";
            }
            font.IsItalic = pColoumSetting.IsItalic;
            font.Boldweight = pColoumSetting.FontBold ? short.MaxValue : short.MinValue;

            #endregion

            #region 单元格内容水平对齐
            pCellStyle.Alignment = pColoumSetting.CellStyle_HorizontalAlignment;
            #endregion

            #region 单元格内容垂直对齐
            pCellStyle.VerticalAlignment = pColoumSetting.CellStyle_VerticalAlignment;
            #endregion

            #region 单元格自动换行
            pCellStyle.WrapText = pColoumSetting.CellStyle_WrapText;
            #endregion

            #region 单元格边框
            pCellStyle.BorderBottom = pColoumSetting.CellStyle_Border == NPOI.SS.UserModel.BorderStyle.None ? NPOI.SS.UserModel.BorderStyle.Thin : pColoumSetting.CellStyle_Border;
            pCellStyle.BorderLeft = pColoumSetting.CellStyle_Border == NPOI.SS.UserModel.BorderStyle.None ? NPOI.SS.UserModel.BorderStyle.Thin : pColoumSetting.CellStyle_Border;
            pCellStyle.BorderRight = pColoumSetting.CellStyle_Border == NPOI.SS.UserModel.BorderStyle.None ? NPOI.SS.UserModel.BorderStyle.Thin : pColoumSetting.CellStyle_Border;
            pCellStyle.BorderTop = pColoumSetting.CellStyle_Border == NPOI.SS.UserModel.BorderStyle.None ? NPOI.SS.UserModel.BorderStyle.Thin : pColoumSetting.CellStyle_Border;
            #endregion

            #region 单元格边框颜色
            pCellStyle.BottomBorderColor = pColoumSetting.CellStyle_BorderColor == 0 ? HSSFColor.Black.Index : pColoumSetting.CellStyle_BorderColor;
            pCellStyle.LeftBorderColor = pColoumSetting.CellStyle_BorderColor == 0 ? HSSFColor.Black.Index : pColoumSetting.CellStyle_BorderColor;
            pCellStyle.RightBorderColor = pColoumSetting.CellStyle_BorderColor == 0 ? HSSFColor.Black.Index : pColoumSetting.CellStyle_BorderColor;
            pCellStyle.TopBorderColor = pColoumSetting.CellStyle_BorderColor == 0 ? HSSFColor.Black.Index : pColoumSetting.CellStyle_BorderColor;
            #endregion

            #region 背景色
            pCellStyle.FillBackgroundColor = pColoumSetting.FillBackgroundColor == 0 ? HSSFColor.White.Index : pColoumSetting.FillBackgroundColor;
            #endregion

            #region 前景色
            pCellStyle.FillForegroundColor = pColoumSetting.FillForegroundColor == 0 ? HSSFColor.White.Index : pColoumSetting.FillForegroundColor;
            #endregion
            pCellStyle.SetFont(font);
            cell.CellStyle = pCellStyle;
            font = null;
        }

        public HSSFCellStyle CreateCellStyle() {
            return hssfworkbook.CreateCellStyle() as HSSFCellStyle;
        }

        /// <summary>
        /// 保存execl
        /// </summary>
        public DialogResult SaveExcel(string pFileName) {
            SaveFileDialog fileDialog = new SaveFileDialog();
            fileDialog.Title = "导出Excel";
            fileDialog.FileName = pFileName;
            fileDialog.Filter = "Excel文件(*.xls)|*.xls";
            DialogResult dialogResult = fileDialog.ShowDialog();
            if (dialogResult == DialogResult.OK) {
                FileStream file2 = new FileStream(fileDialog.FileName, FileMode.Create, FileAccess.Write);
                TargetFileName = file2.Name;
                hssfworkbook.Write(file2);
                file2.Close();
            }
            return dialogResult;
        }
        #endregion

        #region 自定义方法
        public HSSFWorkbook Get_HSSFWorkbook() {
            FileStream file = new FileStream(FullExcelTempletePath, FileMode.Open, FileAccess.Read);
            hssfworkbook = new HSSFWorkbook(file);
            file.Close();
            return hssfworkbook;
        }


        #endregion

        # region        extend Source

        public static void RenderDataTableToExcel(DataTable sourceTable, string exportTemplatePath)
        {
            //HSSFWorkbook hssfworkbook;
            //using (FileStream file = new FileStream(exportTemplatePath, FileMode.Open, FileAccess.Read))
            //{
            //    hssfworkbook = new HSSFWorkbook(file);

            //    HSSFSheet sheet = hssfworkbook.GetSheetAt(0);
            //    System.Collections.IEnumerator rows = sheet.GetRowEnumerator();

            //    HSSFRow headerRow = sheet.GetRow(0);
            //    int cellCount = headerRow.LastCellNum;

            //    for (int j = 0; j < cellCount; j++)
            //    {
            //        HSSFCell cell = headerRow.GetCell(j);
            //        //dt.Columns.Add(cell.ToString());
            //    }

            //    for (int i = (sheet.FirstRowNum + 1); i <= sheet.LastRowNum; i++)
            //    {
            //        HSSFRow row = sheet.GetRow(i);
            //        foreach (DataRow dataRow in sourceTable.Rows)
            //        {
            //            if (dataRow[0].ToString() == row.GetCell(0).ToString())
            //            {
            //                row.CreateCell(1).SetCellValue(dataRow[1].ToString());
            //            }
            //        }
            //    }
            //    hssfworkbook.Write(file);
            //}
        }
#endregion


    }

    #region 列设置类
    /// <summary>
    /// 列设置对象
    /// </summary>
    public class ColoumSetting {
        /// <summary>
        /// 单元格列
        /// </summary>
        public int CellNum {
            get;
            set;
        }

        /// <summary>
        /// 数据源列明
        /// </summary>
        public string ColumnName {
            get;
            set;
        }

        /// <summary>
        /// 格式名称
        /// </summary>
        public string FormatStyle {
            get;
            set;
        }

        /// <summary>
        /// 单元格内容水平对齐
        /// </summary>
        public NPOI.SS.UserModel.HorizontalAlignment CellStyle_HorizontalAlignment {
            get;
            set;
        }

        /// <summary>
        /// 单元格内容垂直对齐
        /// </summary>
        public NPOI.SS.UserModel.VerticalAlignment CellStyle_VerticalAlignment {
            get;
            set;
        }

        /// <summary>
        /// 单元格自动换行
        /// </summary>
        public Boolean CellStyle_WrapText {
            get;
            set;
        }

        /// <summary>
        /// 单元格边框
        /// </summary>
        public NPOI.SS.UserModel.BorderStyle CellStyle_Border {
            get;
            set;
        }

        /// <summary>
        /// 单元格边框颜色
        /// </summary>
        public short CellStyle_BorderColor {
            get;
            set;
        }

        /// <summary>
        /// 字体名称
        /// </summary>
        public string FontName {
            get;
            set;
        }

        /// <summary>
        /// 字体是否斜体
        /// </summary>
        public bool IsItalic {
            get;
            set;
        }

        /// <summary>
        /// 字体颜色
        /// </summary>
        public short FontColor {
            get;
            set;
        }

        /// <summary>
        /// 字体加粗
        /// </summary>
        public bool FontBold {
            get;
            set;
        }

        /// <summary>
        /// 前景色
        /// </summary>
        public short FillForegroundColor {
            get;
            set;
        }

        /// <summary>
        /// 背景色
        /// </summary>
        public short FillBackgroundColor {
            get;
            set;
        }
    }

    #endregion

    #region 多sheet设置类
    public class MutiSheetSetting {
        /// <summary>
        /// Excel Sheet名称
        /// </summary>
        public string SheetName {
            get;
            set;
        }

        /// <summary>
        /// 表名称
        /// </summary>
        public string TableName {
            get;
            set;
        }

        /// <summary>
        /// 起始的行
        /// </summary>
        public int RowStartNum {
            get;
            set;
        }

        /// <summary>
        /// 每个sheet 对应的单元格属性
        /// </summary>
        public IList<ColoumSetting> pColoumSettingList {
            get;
            set;
        }

    }
    #endregion

    #region 自定义单元格类
    public class CustomerSetting : ColoumSetting {
        /// <summary>
        /// SheetName
        /// </summary>
        public string SheetName {
            get;
            set;
        }

        /// <summary>
        /// 单元格列
        /// </summary>
        public int RowNum {
            get;
            set;
        }

        /// <summary>
        /// 值
        /// </summary>
        public object Value {
            get;
            set;
        }

        /// <summary>
        /// 合并单元格实际上是声明一个区域，该区域中的单元格将进行合并，合并后的内容与样式以该区域最左上角的单元格为准。
        /// CellRangeAddress四个参数为：起始行，结束行，起始列，结束列
        /// </summary>
        public NPOI.SS.Util.CellRangeAddress CellRange {
            get;
            set;
        }
    }


    #endregion

   
}
