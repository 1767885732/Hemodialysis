/*----------------------------------------------------------------
 * Copyright (C) 2005 医疗科技发展有限公司
 * 文件功能描述:用户控件
 * 创建标识:吕志强-2017年1月25日
 * 
 * 修改时间:2017年6月12日
 * 修改人:贺建操
 * 修改描述:修复系统响应速度慢的问题
 * 
 * 修改时间:2017年7月14日
 * 修改人:顾伟伟
 * 修改描述:增加窗体控件值的方法
 * ----------------------------------------------------------------*/
using System.Windows.Controls;
using System.Windows.Documents;

namespace Hemo.Client.Controls
{
    public class PrintDialogWithPageRange : PrintDialog
    {
        /// <summary>
        /// 打印方法
        /// </summary>
        /// <param name="doc"></param>
        /// <param name="desp"></param>
        public new void PrintDocument(DocumentPaginator doc, string desp)
        {
            if (this.PageRangeSelection == PageRangeSelection.AllPages)
            {
                base.PrintDocument(doc, desp);
            }
            else
            {
                //string timeStamp = DateTime.Now.DayOfYear.ToString() + DateTime.Now.Hour + DateTime.Now.Minute + DateTime.Now.Second;
                //string pack = "pack://temp" + timeStamp + ".xps";
                //MemoryStream ms = new MemoryStream();
                //Package package = Package.Open(ms, FileMode.Create, FileAccess.ReadWrite);
                //PackageStore.AddPackage(new Uri(pack), package);
                //XpsDocument xpsDoc = new XpsDocument(package, CompressionOption.SuperFast, pack);
                //XpsDocumentWriter xpsDocumentWriter = XpsDocument.CreateXpsDocumentWriter(xpsDoc);
                //xpsDocumentWriter.Write(doc);
                //FixedDocumentSequence fdsCopy = xpsDoc.GetFixedDocumentSequence();

                ////write only the page visuals we want to print to the print queue
                //XpsDocumentWriter xdw =System.Printing.PrintQueue.CreateXpsDocumentWriter(this.PrintQueue);
                //VisualsToXpsDocument vtxd = (VisualsToXpsDocument)xdw.CreateVisualsCollator();
                //for (int i = this.PageRange.PageFrom - 1; i < this.PageRange.PageTo; i++)
                //{
                //    Visual v = fdsCopy.DocumentPaginator.GetPage(i).Visual;
                //    ContainerVisual cv = new ContainerVisual();
                //    cv.Children.Add(v);
                //    vtxd.Write(cv, this.PrintTicket);
                //    cv.Children.Remove(v); //remove so that if they print again it will not try to set a second parent
                //}
                //vtxd.EndBatchWrite();

                ////clean up
                //xpsDoc.Close();
                //ms.Close();
                //ms.Dispose();

                for (int i = this.PageRange.PageFrom - 1; i < this.PageRange.PageTo; i++)
                {
                    base.PrintVisual(doc.GetPage(i).Visual, desp);
                }
            }
        }
    }
}