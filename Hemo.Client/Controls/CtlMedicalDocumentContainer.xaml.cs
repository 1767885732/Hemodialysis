/*----------------------------------------------------------------
// Copyright (C) 2013 苏州XX公司股份有限公司
// 文件名：CtlMedicalDocumentContainer.cs
// 文件功能描述：血 液 净 化 治 疗 记 录 单 容器
// 创建标识：刘超 2013-07-22
----------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Hemo.Client.Controls;
using System.Windows.Markup;
using System.ComponentModel;
using System.Printing;
using Hemo.Utilities;
using System.IO;
using System.IO.Packaging;
using System.Windows.Xps.Packaging;
using System.Windows.Xps.Serialization;
using System.ComponentModel;
using System.Printing;

namespace Hemo.Client.Controls
{
    /// <summary>
    /// CtlMedicalDocumentContainer.xaml 的交互逻辑
    /// </summary>
    public partial class CtlMedicalDocumentContainer : UserControl
    {
        private List<MedicalDocumentForPrint> docList = new List<MedicalDocumentForPrint>();
        public UserControl CurrentMedicalDocument
        {
            set
            {
                this.Add(value);
            }
        }
        private bool _haveNextPage = false;
        public bool HaveNextPage
        {
            get { return _haveNextPage; }
            set
            {
                _haveNextPage = value;
            }
        }

        public CtlMedicalDocumentContainer()
        {
            InitializeComponent();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            //隐藏搜索框
            //    var PART_FindToolBarHost = this.preview.Template.FindName("PART_FindToolBarHost", this.preview) as ContentControl;
            //    PART_FindToolBarHost.Visibility = Visibility.Collapsed;
            //CtlMedicalDocument ctlMedicalDocument = CurrentMedicalDocument;//new CtlMedicalDocument();
        }

        private void btnPrint_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                System.Windows.Forms.PrintDialog winFormsPrintDialog = new System.Windows.Forms.PrintDialog();
                winFormsPrintDialog.AllowSomePages = true;
                var dlg = winFormsPrintDialog.ShowDialog();
                if (dlg == System.Windows.Forms.DialogResult.OK)
                {
                    PrintTicket ticket = new PrintTicket();
                    ticket.CopyCount = winFormsPrintDialog.PrinterSettings.Copies;

                    var pd = new PrintDialogWithPageRange();
                    pd.PageRangeSelection = winFormsPrintDialog.PrinterSettings.PrintRange == System.Drawing.Printing.PrintRange.SomePages ? PageRangeSelection.UserPages : PageRangeSelection.AllPages;
                    if (pd.PageRangeSelection == PageRangeSelection.UserPages)
                    {
                        PageRange pr = new PageRange() { PageFrom = winFormsPrintDialog.PrinterSettings.FromPage, PageTo = winFormsPrintDialog.PrinterSettings.ToPage };

                        pd.PageRange = pr;
                        //pd.PageRange.PageTo = winFormsPrintDialog.PrinterSettings.ToPage;
                    }
                    pd.PrintTicket = ticket;
                    pd.UserPageRangeEnabled = true;
                    var server = new PrintServer();
                    var queues = server.GetPrintQueues(new[] { EnumeratedPrintQueueTypes.Local, EnumeratedPrintQueueTypes.Connections });
                    foreach (var queue in queues)
                    {
                        if (queue.Name == winFormsPrintDialog.PrinterSettings.PrinterName)
                            pd.PrintQueue = queue;
                    }

                    pd.PrintDocument(this.preview.Document.DocumentPaginator, "print");

                }

            }
            catch (PrintQueueException ex)
            {
                MessageBox.Show(ex.PrinterName + "\r\n" + ex.Message + "\r\n" + ex.StackTrace);

                throw ex;
            }
        }

        /// <summary>
        /// 打印空白记录单
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnPrintBlank_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // 创建空白记录单
                CtlMedicalDocumentNew blankDoc = new CtlMedicalDocumentNew();

                // 清空当前文档列表，添加空白记录单
                this.docList.Clear();
                this.docList.Add(new MedicalDocumentForPrint("", blankDoc, this._haveNextPage));
                this.RefreshPage();

                // 延迟执行打印，等文档渲染完成
                System.Windows.Threading.DispatcherTimer timer = new System.Windows.Threading.DispatcherTimer();
                timer.Interval = TimeSpan.FromMilliseconds(100);
                timer.Tick += (s, args) =>
                {
                    timer.Stop();
                    // 调用原有的打印逻辑
                    btnPrint_Click(sender, e);
                };
                timer.Start();
            }
            catch (Exception ex)
            {
                MessageBox.Show("打印空白记录单失败：" + ex.Message, "错误", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public bool Add(string id)
        {
            var result = from doc in this.docList where doc.Id.Equals(id) select doc;
            if (result.Count() == 0)
            {
                return false;
            }
            else
            {
                if (!result.ElementAt(0).IsShow)
                {
                    result.ElementAt(0).IsShow = true;
                    this.RefreshPage();
                }
                return true;
            }
        }

        public void Add(string id, UserControl document)
        {
            var result = from doc in this.docList where doc.Id.Equals(id) select doc;
            if (result.Count() == 0)
            {
                this.docList.Add(new MedicalDocumentForPrint(id, document, this._haveNextPage));
                this.RefreshPage();
            }
            else
            {
                result.ElementAt(0).IsShow = true;
                this.RefreshPage();
            }
        }

        public void Add(UserControl document)
        {
            this.docList.Clear();
            this.docList.Add(new MedicalDocumentForPrint("", document, this._haveNextPage));
            this.RefreshPage();
        }

        public void Add(UserControl document, string id)
        {
            this.docList.Add(new MedicalDocumentForPrint("", document, this._haveNextPage));
            this.RefreshPage();
        }

        public void Remove(string id)
        {
            var result = from doc in this.docList where doc.Id.Contains(id) select doc;
            if (result.Count() > 0)
            {
                for (int i = 0; i < result.Count(); i++)
                {
                    if (result.ElementAt(i).IsShow)
                    {
                        result.ElementAt(i).IsShow = false;
                        this.RefreshPage();
                    }
                }
                //if (result.ElementAt(0).IsShow)
                //{
                //    result.ElementAt(0).IsShow = false;
                //    this.RefreshPage();
                //}
            }
        }

        public void RemoveDoc(string id)
        {
            var result = from doc in this.docList where doc.Id.Equals(id) select doc;
            if (result.Count() > 0)
            {
                docList.Remove(result.First());
            }
        }

        public WPF_DocumentBase GetDoc(string id)
        {
            var result = from doc in this.docList where doc.Id.Equals(id) select doc;
            if (result.Count() > 0)
            {
                var userControl = (UserControl)result.ElementAt(0).Document;
                if (userControl.GetType().Equals(typeof(Hemo.Client.Controls.CtlMedicalDocumentNew)))
                {
                    return userControl as CtlMedicalDocumentNew;
                }
                else
                {
                    return userControl as CtlMedicalDocumentCRRTNew;
                }
            }
            return null;
        }

        public void RefreshPage()
        {
            FixedDocument _fixeddoc = new FixedDocument();

            foreach (var doc in this.docList)
            {
                if (doc.IsShow)
                {
                    foreach (var pc in doc.CreatePage())
                    {
                        _fixeddoc.Pages.Add(pc);
                    }
                }
            }
            preview.Document = _fixeddoc;
        }

        private void SaveToImage(FrameworkElement ui, string fileName)
        {
            System.IO.FileStream fs = new System.IO.FileStream(fileName, System.IO.FileMode.Create);
            RenderTargetBitmap bmp = new RenderTargetBitmap((int)ui.Width, (int)ui.Height, 96d, 96d,
            PixelFormats.Pbgra32);
            bmp.Render(ui);
            BitmapEncoder encoder = new PngBitmapEncoder();
            encoder.Frames.Add(BitmapFrame.Create(bmp));
            encoder.Save(fs);
            fs.Close();
        }
        public byte[] GetPdfByte()
        {

            Byte[] buffer;
            using (var stream = new MemoryStream())
            {
                using (var package = Package.Open(stream, FileMode.Create, FileAccess.ReadWrite))
                {
                    using (var xpsDoc = new XpsDocument(package, CompressionOption.Maximum))
                    {
                        var rsm = new XpsSerializationManager(new XpsPackagingPolicy(xpsDoc), false);
                        var paginator = ((IDocumentPaginatorSource)preview.Document).DocumentPaginator;
                        rsm.SaveAsXaml(paginator);
                        rsm.Commit();
                    }
                }
                stream.Position = 0;

                var pdfXpsDoc = PdfSharp.Xps.XpsModel.XpsDocument.Open(stream);
                var fileName = string.Format("{0}.pdf", Guid.NewGuid().ToString());
                PdfSharp.Xps.XpsConverter.Convert(pdfXpsDoc, fileName, 0);

                using (var streamFile = new FileInfo(fileName).OpenRead())
                {
                    buffer = new Byte[streamFile.Length];
                    //从流中读取字节块并将该数据写入给定缓冲区buffer中
                    streamFile.Read(buffer, 0, Convert.ToInt32(streamFile.Length));
                }
                System.IO.File.Delete(fileName);

            }
            return buffer;
        }
        internal class MedicalDocumentForPrint
        {
            private Size size = new Size(816, 1100);
            private string id;
            private UserControl document;
            private bool isShow = true;
            private bool haveNext = false;

            public string Id
            {
                get { return this.id; }
            }
            public UIElement Document
            {
                get { return this.document; }
            }
            public bool IsShow
            {
                get { return this.isShow; }
                set { this.isShow = value; }
            }

            public MedicalDocumentForPrint(string id, UserControl document, bool haveNext)
            {
                this.id = id;
                this.document = document;
                this.haveNext = haveNext;
            }

            public List<PageContent> CreatePage()
            {
                List<PageContent> pcList = new List<PageContent>();
                pcList.Add(this.CreatePage(this.document));
                if (this.haveNext)
                {
                    UserControl child = null;
                    if (this.document.GetType().Equals(typeof(Hemo.Client.Controls.CtlMedicalDocumentCRRTNew)))
                    {
                        child = (this.document as CtlMedicalDocumentCRRTNew).NextPage;
                    }
                    else
                    {
                        child = (this.document as CtlMedicalDocumentNew).NextPage;
                    }
                    while (child != null)
                    {
                        pcList.Add(this.CreatePage(child));
                        child = (child as WPF_DocumentBase).NextPage;
                    }
                }

                return pcList;
            }

            private PageContent CreatePage(UserControl document)
            {
                if (document.Parent != null)
                {
                    ((document.Parent) as FixedPage).Children.Clear();
                }

                FixedPage fixedpage = new FixedPage() { Width = size.Width, Height = size.Height };
                fixedpage.Children.Add(document);

                PageContent page = new PageContent();
                page.MouseLeftButtonDown += new MouseButtonEventHandler(pc_MouseLeftButtonDown);
                ((IAddChild)page).AddChild(fixedpage);

                return page;
            }

            /// <summary>
            /// 取消点击事件
            /// </summary>
            /// <param name="sender"></param>
            /// <param name="e"></param>
            private void pc_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
            {
                e.Handled = true;
            }
        }
    }
}
