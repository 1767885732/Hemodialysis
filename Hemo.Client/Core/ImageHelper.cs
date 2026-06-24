/*----------------------------------------------------------------
 * Copyright (C) 2005 医疗科技发展有限公司
 * 文件功能描述:图片处理类
 * 创建标识 贺建操-2017年1月30日
 * ----------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace Hemo.Client.Core
{
    public class ImageHelper
    {
        /// <summary>
        /// 转换成Bype
        /// </summary>
        /// <param name="byteImages"></param>
        /// <param name="defaultMaxPicWidth"></param>
        /// <param name="defaultMaxPicHeight"></param>
        /// <returns></returns>
        public static byte[] ConvertByteImageToByte(byte[] byteImages, int defaultMaxPicWidth, int? defaultMaxPicHeight)
        {
            Image image;
            using (MemoryStream ms = new MemoryStream(byteImages))
            {
                image = Image.FromStream(ms);
            }
            return ConvertImageToByte(image, defaultMaxPicWidth, defaultMaxPicHeight);
        }
        /// <summary>
        /// 转换成image
        /// </summary>
        /// <param name="imageByte"></param>
        /// <returns></returns>
        public static Image ConvertByteToImage(byte[] imageByte)
        {

            Image image;
            using (MemoryStream ms = new MemoryStream(imageByte))
            {
                image = Image.FromStream(ms);
            }
            return image;

        }
        /// <summary>
        /// 转image到byte
        /// </summary>
        /// <param name="image"></param>
        /// <param name="defaultMaxPicWidth"></param>
        /// <param name="defaultMaxPicHeight"></param>
        /// <returns></returns>
        public static byte[] ConvertImageToByte(Image image, int defaultMaxPicWidth, int? defaultMaxPicHeight)
        {
            var imageCodecInfoList = ImageCodecInfo.GetImageEncoders();
            string mimeType = "image/bmp";
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

            if (!defaultMaxPicHeight.HasValue)
            {
                dblPercent = defaultMaxPicWidth / Convert.ToDouble(image.Width);
                dblPicWidth = defaultMaxPicWidth;
                dblPicHeight = Convert.ToInt32(image.Height * dblPercent);
            }
            else
            {
                dblPicHeight = defaultMaxPicHeight.Value;
                dblPicWidth = defaultMaxPicWidth;
            }

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

            return tempImage;
        }
    }
}
