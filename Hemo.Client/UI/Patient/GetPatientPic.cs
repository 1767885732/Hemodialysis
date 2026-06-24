/*----------------------------------------------------------------
// Copyright (C) 2013 苏州XX公司
// 描述：患者拍照窗体类
// 创建时间：2016-05-27
// 创建者：贺建操
//  
// 修改时间：
// 修改人：
// 修改描述：
//
----------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Hemo.Client.UI.Machine;
using System.IO;
using System.Drawing.Imaging;
using DevExpress.XtraEditors;
using Hemo.Client.Core;

namespace Hemo.Client.UI.Patient
{
    [ToolboxItem(true)]

    public partial class GetPatientPic : ViewBase
    {
        #region 类变量

        private VideoManager _videoManager;
        private const int _WaterTextFontSize = 20;
        private const int _WaterTextAlpha = 150;
        private const double _DefaultMaxPicWidth = 2300;
        private const double _DefaultMaxPicHeight = 1800;
        private Image _patientPic = null;

        #endregion

        #region 属性

        private LoadPictureType CurrentLoadPictureType
        {
            get;
            set;
        }

        public Image PatientPic
        {
            get { return _patientPic; }
            set { _patientPic = value; }
        }

        /// <summary>
        /// 返回图片数据
        /// </summary>
        public ImageDataInfo ImageData
        {
            get
            {
                ImageDataInfo imgInfo = new ImageDataInfo();
                imgInfo.IsSetImage = false;
                imgInfo.Image = null;

                if (CurrentLoadPictureType == LoadPictureType.UploadFile ||
                    CurrentLoadPictureType == LoadPictureType.Capture)
                {
                    if (picMain.Image != null)
                    {
                        imgInfo.IsSetImage = true;
                        imgInfo.Image = ConvertImageToByte(picMain.Image);
                    }
                    else
                    {
                        imgInfo.IsSetImage = true;
                        imgInfo.Image = null;
                    }
                }
                else if (CurrentLoadPictureType == LoadPictureType.Clear)
                {
                    imgInfo.IsSetImage = true;
                    imgInfo.Image = null;
                }
                return imgInfo;
            }
        }

        #endregion

        #region 构造函数

        public GetPatientPic()
        {
            InitializeComponent();
            CurrentLoadPictureType = LoadPictureType.NotSet;
        }

        #endregion

        #region 事件

        private void tabPicture_SelectedPageChanged(object sender, DevExpress.XtraTab.TabPageChangedEventArgs e)
        {
            try
            {
                if (e.Page == tabPageVideo)
                {
                    if (_videoManager == null)
                        _videoManager = new Hemo.Client.Core.VideoManager(picVideo.Handle, picVideo.Left, picVideo.Top, picVideo.Width, (short)this.picVideo.Height);
                    _videoManager.OpenVideo();
                }

                if (e.Page == tabPageMain)
                {
                    if (_videoManager != null)
                        _videoManager.CloseVideo();
                    _videoManager = null;
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }
        private void btnLoadPicture_Click(object sender, EventArgs e)
        {
            try
            {
                openFileDialog.Filter = "All Image Formats  (*.bmp;*.jpg;*.jpeg;*.gif;*.png;*.tif)|" +
                                        "*.bmp;*.jpg;*.jpeg;*.gif;*.png;*.tif|Bitmaps (*.bmp)|*.bmp|" +
                                        "GIFs (*.gif)|*.gif|JPEGs (*.jpg)|*.jpg;*.jpeg|PNGs (*.png)|*.png|TIFs (*.tif)|*.tif|All Files (*.*)|*.*";

                if (openFileDialog.ShowDialog() != DialogResult.OK)
                    return;

                if (!File.Exists(openFileDialog.FileName))
                {
                    XtraMessageBox.Show("选择的文件不存在!", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                FileStream fsPicture = new FileStream(openFileDialog.FileName, FileMode.Open, FileAccess.Read);
                if (Utilities.Utility.GetMaxUploadPictureLength() * 1024 * 1024 < fsPicture.Length)
                {
                    XtraMessageBox.Show(string.Format("选择的文件大小不能超过{0}MB,请重新选择!", Utilities.Utility.GetMaxUploadPictureLength().ToString()), "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                byte[] bytePicture = new byte[fsPicture.Length];
                Image sourceImage = Image.FromStream(fsPicture);
                //去掉加
                //Image testImg = AddWaterText(sourceImage, string.Format(_WaterTextString, DateTime.Now.ToString("yyyy-MM-dd HH:mm")), _WaterTextAlpha, _WaterTextFontSize);
                picMain.Image = sourceImage;
                CurrentLoadPictureType = LoadPictureType.UploadFile;
                fsPicture.Close();
                fsPicture = null;
                _patientPic = picMain.Image;
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }

        private void btnClearPicture_Click(object sender, EventArgs e)
        {
            picMain.Image = null;
            CurrentLoadPictureType = LoadPictureType.Clear;
        }

        private void btnPreview_Click(object sender, EventArgs e)
        {
            try
            {
                PreviewPictureFrm picPreviewFrm = new PreviewPictureFrm();
                picPreviewFrm.DisplayImage(picMain.Image);
                picPreviewFrm.ShowDialog();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// 设置图片数据
        /// </summary>
        /// <param name="imageData"></param>
        public void SetImageData(byte[] imageData)
        {
            if (imageData != null)
            {
                using (MemoryStream msPicStream = new MemoryStream(imageData))
                {
                    picMain.Image = Image.FromStream(msPicStream);
                    CurrentLoadPictureType = LoadPictureType.Capture;
                }
            }
            else
            {
                picMain.Image = null;
            }
        }

        private byte[] ConvertImageToByte(Image image)
        {
            var imageCodecInfoList = ImageCodecInfo.GetImageEncoders();
            string mimeType = "image/jpeg";
            ImageCodecInfo myImageCodec = null;
            foreach (var imgCodec in imageCodecInfoList)
            {
                if (imgCodec.MimeType == mimeType)
                {
                    myImageCodec = imgCodec;
                    break;
                }
            }

            if (myImageCodec == null)
                return null;

            EncoderParameters encoderParams = new EncoderParameters(1);
            System.Drawing.Imaging.Encoder myCompressQuanlityEncoder = System.Drawing.Imaging.Encoder.Quality;
            EncoderParameter myCompressQualityParam = new EncoderParameter(myCompressQuanlityEncoder, 80L);
            encoderParams.Param[0] = myCompressQualityParam;

            double dblPicWidth = 0;
            double dblPicHeight = 0;
            double dblPercent = 0;
            byte[] tempImage;

            if (image.Width > _DefaultMaxPicWidth)
            {
                //去掉按比例图片存储
                dblPercent = _DefaultMaxPicWidth / Convert.ToDouble(image.Width);
                dblPicWidth = _DefaultMaxPicWidth;
                dblPicHeight = Convert.ToInt32(image.Height * dblPercent);


                Bitmap tempPic = new Bitmap(Convert.ToInt32(dblPicWidth), Convert.ToInt32(dblPicHeight));
                var graphics = Graphics.FromImage(tempPic);
                Rectangle recPic = new Rectangle(0, 0, Convert.ToInt32(dblPicWidth), Convert.ToInt32(dblPicHeight));
                graphics.DrawImage(image, recPic);

                using (MemoryStream msPicture = new MemoryStream())
                {
                    tempPic.Save(msPicture, myImageCodec, encoderParams);
                    tempImage = msPicture.ToArray();
                }
                tempPic.Dispose();
            }
            else
            {
                using (Bitmap bmpPic = new Bitmap(image))
                {
                    using (MemoryStream msPicture = new MemoryStream())
                    {
                        bmpPic.Save(msPicture, myImageCodec, encoderParams);
                        tempImage = msPicture.ToArray();
                    }
                }
            }

            return tempImage;
        }

        #endregion

        #region 方法

        public void SetPrewViewImage(Image image)
        {
            picMain.Image = image;

        }

        public void PcitureAction()
        {
            try
            {
                if (_videoManager != null)
                {
                    var imgCapture = _videoManager.CatchVideo();
                    picMain.Image = imgCapture;
                    //去掉加水印
                    //if(chkWater.Checked) 
                    //    picMain.Image = AddWaterText(imgCapture, string.Format(_WaterTextString, DateTime.Now.ToString("yyyy-MM-dd HH:mm")), _WaterTextAlpha, _WaterTextFontSize);
                    //else
                    //    picMain.Image = AddWaterText(imgCapture, "", _WaterTextAlpha, _WaterTextFontSize);
                    _patientPic = picMain.Image;
                }
                if (picMain.Image == null)
                {
                    XtraMessageBox.Show("拍照不成功!");
                    return;
                }
                tabPicture.SelectedTabPageIndex = 0;
                CurrentLoadPictureType = LoadPictureType.Capture;
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }

        #endregion

        #region 图片加水印文字

        private Image AddWaterText(Image sourceImg, string text, int Alpha, int fontsize)
        {
            int imgPhotoWidth = sourceImg.Width;
            int imgPhotoHeight = sourceImg.Height;

            Bitmap bmPhoto = new Bitmap(imgPhotoWidth, imgPhotoHeight, System.Drawing.Imaging.PixelFormat.Format24bppRgb);
            bmPhoto.SetResolution(sourceImg.HorizontalResolution, sourceImg.VerticalResolution);

            Graphics gbmPhoto = Graphics.FromImage(bmPhoto);
            gbmPhoto.Clear(Color.FromName("white"));
            gbmPhoto.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.High;
            gbmPhoto.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            gbmPhoto.DrawImage(sourceImg, new Rectangle(0, 0, imgPhotoWidth, imgPhotoHeight), 0, 0, imgPhotoWidth, imgPhotoHeight, GraphicsUnit.Pixel);

            System.Drawing.Font font = null;
            System.Drawing.SizeF crSize = new SizeF();
            font = new Font("宋体", fontsize, FontStyle.Bold);
            //测量指定区域
            crSize = gbmPhoto.MeasureString(text, font);
            float y = imgPhotoHeight - crSize.Height;
            float x = imgPhotoWidth - crSize.Width - 5;

            if (x > 0 && y > 0)
            {
                System.Drawing.StringFormat StrFormat = new System.Drawing.StringFormat();
                StrFormat.Alignment = System.Drawing.StringAlignment.Center;

                //画两次制造透明效果
                //System.Drawing.SolidBrush semiTransBrush2 = new System.Drawing.SolidBrush(Color.FromArgb(Alpha, 56, 0, 0));
                System.Drawing.SolidBrush semiTransBrush2 = new System.Drawing.SolidBrush(Color.FromArgb(Alpha, 255, 0, 0));
                gbmPhoto.DrawString(text, font, semiTransBrush2, x + 1, y + 1);

                //System.Drawing.SolidBrush semiTransBrush = new System.Drawing.SolidBrush(Color.FromArgb(Alpha, 176, 176, 176));
                System.Drawing.SolidBrush semiTransBrush = new System.Drawing.SolidBrush(Color.FromArgb(Alpha, 255, 255, 3));
                gbmPhoto.DrawString(text, font, semiTransBrush, x, y);

            }
            gbmPhoto.Dispose();
            return bmPhoto;
        }

        #endregion
    }

    internal enum LoadPictureType
    {
        NotSet,
        Clear,
        UploadFile,
        Capture
    }

    public class ImageDataInfo
    {
        /// <summary>
        /// 是否设置了图片
        /// </summary>
        public bool IsSetImage
        {
            get;
            set;
        }

        /// <summary>
        /// 图片
        /// </summary>
        public byte[] Image
        {
            get;
            set;
        }
    }

}
