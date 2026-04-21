using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;

namespace Hemo.Web
{
    public partial class Index : System.Web.UI.Page
    {

        public string PatientID = string.Empty;
        public string DefaultEmrFile = string.Empty;
        public string nodeData = string.Empty;
      public  System.Data.DataTable patientTable = new System.Data.DataTable();
        protected void Page_Load(object sender, EventArgs e)
        {
            var basePath = Server.MapPath("EmrFiles");

            PatientID = Request.QueryString["PatientID"];
            var fdate = Request.QueryString["EmrDate"];
            var ftype = Request.QueryString["EmrType"];
            if (PatientID == null || PatientID == string.Empty)
            {
                Response.Clear();
                Response.Write("传入的参数错误,未指定患者编号!");
                Response.End();
            }
            if (fdate != null && fdate != string.Empty && ftype != null && ftype != string.Empty)
            {
                DateTime date = DateTime.Now;
                if (DateTime.TryParse(fdate, out date))
                {
                    var saveFilePath = string.Format("{0}\\{1}\\{2}\\{3}\\{4}.pdf", basePath, PatientID, ftype, date.ToString("yyyy-MM"), date.ToString("yyyy-MM-dd"));
                    if (System.IO.File.Exists(saveFilePath))
                    {
                        DefaultEmrFile = string.Format("http://{0}/EmrFiles/{1}/{2}/{3}/{4}.pdf", Request.Url.Authority, PatientID, ftype, date.ToString("yyyy-MM"), date.ToString("yyyy-MM-dd"));
                    }

                }

            }
            patientTable = Hemo.Web.EmrService.PatientData(PatientID);
            if (!IsPostBack)
            {
                var dinfoBase = new DirectoryInfo(string.Format("{0}\\{1}", basePath, PatientID));
                foreach (var df in dinfoBase.GetDirectories())
                {
                    var idParent = EmrService.GetDataIDByPath(df.FullName);
                    if (nodeData == string.Empty)
                    {
                        nodeData = string.Format("{2} id:\"{0}\", pId: \"0\", name: \"{1}\", open: true {3}", idParent, EmrService.GetTypeNameByType(df.Name), "{", "}");
                    }
                    else
                    {
                        nodeData = string.Format("{0},{3} id:\"{1}\", pId: \"0\", name: \"{2}\", open: true {4}", nodeData, idParent, EmrService.GetTypeNameByType(df.Name), "{", "}");
                    }
                    var typeInfo = df.GetDirectories();
                    foreach (var tdf in typeInfo)
                    {
                        var idtinfo = EmrService.GetDataIDByPath(tdf.FullName);
                        nodeData = string.Format("{0},{4} id:\"{1}\", pId: \"{2}\", name: \"{3}\", open: true {5}", nodeData, idtinfo, idParent, tdf.Name, "{", "}");
                        var files = tdf.GetFiles();

                        foreach (var fi in files)
                        {
                            nodeData = string.Format("{0},{5} id:\"{1}\", pId: \"{2}\", name: \"{3}\", file: \"{4}\" {6}", nodeData, EmrService.GetDataIDByPath(fi.FullName), idtinfo, fi.Name.Replace(".pdf",""),
                                string.Format("http://{0}/EmrFiles/{1}/{2}/{3}/{4}", Request.Url.Authority, PatientID, df.Name, tdf.Name, fi.Name), "{", "}");

                        }
                    }

                }

            }


        }



        //我把ArrayList当成动态数组用，非常好用

    }

    public class TreeNodeItem
    {

        public string NODEID
        {
            get;
            set;
        }
        public string PARENTID
        {
            get;
            set;
        }
        public string NODENAME
        {
            get;
            set;
        }
        public string FileUrl
        {
            get;
            set;
        }
        public bool IsFile
        {
            get;
            set;
        }
    }
}