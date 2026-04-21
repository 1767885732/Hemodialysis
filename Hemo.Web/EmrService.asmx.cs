using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;

namespace Hemo.Web
{

  

    /// <summary>
    /// 电子病历服务
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消对下行的注释。
    // [System.Web.Script.Services.ScriptService]
    public class EmrService : System.Web.Services.WebService
    {
        public static System.Data.DataTable PatientData(string patientID)
        {
            var dt = new System.Data.DataTable();
              
            try
            {
                var conString = System.Configuration.ConfigurationManager.ConnectionStrings["docare"].ConnectionString;
                var ad = new System.Data.OracleClient.OracleDataAdapter(string.Format(System.Configuration.ConfigurationManager.AppSettings["GetPatientInfoSql"],patientID), conString);
                ad.Fill(dt);
            }
            catch 
            {
                return dt;
            }
            return dt;

        }
        public static string GetDataIDByPath(string path) {

            return path.Replace(":", "").Replace("\\", "");
        }
        public static string GetTypeNameByType(string type)
        {
            try
            {
                return System.Configuration.ConfigurationManager.AppSettings[string.Format("{0}-EMRTYPE", type)];

            }
            catch
            {

                return "";
            }


        }

        /// <summary>
        /// 保存电子病历pdf
        /// </summary>
        /// <param name="fileByte">文件内容</param>
        /// <param name="patientID">患者标识</param>
        /// <param name="emrDate">病历日期</param>
        /// <param name="emrType">病历类型</param>      
        /// <returns></returns>
        [WebMethod]
        public string SaveEmrToServer(Byte[] fileByte, string patientID, DateTime emrDate, string emrType)
        {
            string strReturn = string.Empty;
            //急诊日期文件夹
            if (string.IsNullOrEmpty(patientID))
            {
                strReturn = "error|患者急诊编号Reg_Id错误！";
                return strReturn;
            }
            //配置的文件保存地址
            var saveFilePath = string.Format("{0}\\{1}\\{2}\\{3}\\{4}.pdf", Server.MapPath("EmrFiles"), patientID, emrType, emrDate.ToString("yyyy-MM"), emrDate.ToString("yyyy-MM-dd"));
            string dirPath = new System.IO.FileInfo(saveFilePath).DirectoryName;
            if (!System.IO.Directory.Exists(dirPath))
            {
                System.IO.Directory.CreateDirectory(dirPath);
            }
            if (System.IO.File.Exists(saveFilePath))
            {
                System.IO.File.Delete(saveFilePath);
            }

            System.IO.FileStream fileStream = new System.IO.FileStream(saveFilePath, System.IO.FileMode.CreateNew);
            try
            {
                fileStream.Write(fileByte, 0, fileByte.Length);
                strReturn = "ok|";
            }
            catch (Exception ex)
            {
                strReturn = "error|" + ex.Message;
            }
            finally
            {
                fileStream.Close();
            }
            return strReturn;
        }
    }


}
