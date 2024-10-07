using Itech.Utils;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing.Imaging;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;
using CreatedQR.models;

namespace CreatedQR.bussiness
{
    public class UserControlBase : UserControl
    {
        private string userNameLogin = "";
        public string UserNameLogin
        {
            get { return Session["Username"].ToString(); }
            set { userNameLogin = Session["Username"].ToString(); }
        }

        private int userIDLogin = 0;
        public int UserIDLogin
        {
            get { return int.Parse((Session["ID"] ?? "0").ToString()); }
            set { userIDLogin = int.Parse((Session["ID"] ?? "0").ToString()); }
        }


        //private int userTypeID = 0;
        //public int TypeUserID
        //{
        //    get { return int.Parse(Session["TypeUserID"].ToString()); }
        //    set { coachIDLogin = int.Parse(Session["TypeUserID"].ToString()); }
        //}

   
        public static ImageFormat GetImageFormat(byte[] bytes)
        {
            // see http://www.mikekunz.com/image_file_header.html  
            var bmp = Encoding.ASCII.GetBytes("BM");     // BMP
            var gif = Encoding.ASCII.GetBytes("GIF");    // GIF
            var png = new byte[] { 137, 80, 78, 71 };    // PNG
            var tiff = new byte[] { 73, 73, 42 };         // TIFF
            var tiff2 = new byte[] { 77, 77, 42 };         // TIFF
            var jpeg = new byte[] { 255, 216, 255, 224 }; // jpeg
            var jpeg2 = new byte[] { 255, 216, 255, 225 }; // jpeg canon

            if (bmp.SequenceEqual(bytes.Take(bmp.Length)))
                return ImageFormat.Bmp;

            if (gif.SequenceEqual(bytes.Take(gif.Length)))
                return ImageFormat.Gif;

            if (png.SequenceEqual(bytes.Take(png.Length)))
                return ImageFormat.Png;

            if (tiff.SequenceEqual(bytes.Take(tiff.Length)))
                return ImageFormat.Tiff;

            if (tiff2.SequenceEqual(bytes.Take(tiff2.Length)))
                return ImageFormat.Tiff;

            if (jpeg.SequenceEqual(bytes.Take(jpeg.Length)))
                return ImageFormat.Jpeg;

            if (jpeg2.SequenceEqual(bytes.Take(jpeg2.Length)))
                return ImageFormat.Jpeg;

            return ImageFormat.Icon;
        }
        public void SendMail(string strSubjectMail, string strContentMail, string nameTo, string emailTo)
        {
            System.Configuration.Configuration config = WebConfigurationManager.OpenWebConfiguration(HttpContext.Current.Request.ApplicationPath);
            System.Net.Configuration.MailSettingsSectionGroup settings = (System.Net.Configuration.MailSettingsSectionGroup)config.GetSectionGroup("system.net/mailSettings");
            string strName = ConfigurationManager.AppSettings["NameAdminEmail"];
            string strEmail = settings.Smtp.Network.UserName;

            //ITServicesUtil.SendMail(strName, strEmail, nameTo, emailTo, strSubjectMail, strContentMail);
            ITServicesUtil.SendMailGmail(strName, strEmail, nameTo, emailTo, "", strSubjectMail, strContentMail, settings.Smtp.Network.Host, settings.Smtp.Network.UserName,
             settings.Smtp.Network.Password);
        }
        protected bool CheckExtention(FileUpload name)
        {
            string Extentsion = CommonFunctions.getFileFormat(name.FileName);
            bool checkExtension = false;
            foreach (string strTempExtentsion in ConfigurationManager.AppSettings["FILE_FORMAT_EXCEL"].ToString().Split(".".ToCharArray()))
            {
                if (Extentsion.ToLower() == strTempExtentsion.ToLower())
                {
                    checkExtension = true;
                    return true;
                }
            }
            if (checkExtension == false)
            {
                CommonClass.MessageBox.Show("File Upload phải thuộc các định dạng: .xls .xlsx");
                return false;
            }
            return false;
        }
        public static Bitmap CreateThumbnail(Stream lcFilename, int lnWidth, int lnHeight)
        {

            System.Drawing.Bitmap bmpOut = null;
            try
            {
                Bitmap loBMP = new Bitmap(lcFilename);
                ImageFormat loFormat = loBMP.RawFormat;

                decimal lnRatio;
                int lnNewWidth = 0;
                int lnNewHeight = 0;

                if (loBMP.Width < lnWidth && loBMP.Height < lnHeight)
                    return loBMP;

                if (loBMP.Width > loBMP.Height)
                {
                    lnRatio = (decimal)lnWidth / loBMP.Width;
                    lnNewWidth = lnWidth;
                    decimal lnTemp = loBMP.Height * lnRatio;
                    lnNewHeight = (int)lnTemp;
                }
                else
                {
                    lnRatio = (decimal)lnHeight / loBMP.Height;
                    lnNewHeight = lnHeight;
                    decimal lnTemp = loBMP.Width * lnRatio;
                    lnNewWidth = (int)lnTemp;
                }


                bmpOut = new Bitmap(lnNewWidth, lnNewHeight);
                Graphics g = Graphics.FromImage(bmpOut);
                g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.Low;
                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighSpeed;
                g.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighSpeed;
                g.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.HighSpeed;

                g.FillRectangle(Brushes.White, 0, 0, lnNewWidth, lnNewHeight);
                g.DrawImage(loBMP, 0, 0, lnNewWidth, lnNewHeight);

                loBMP.Dispose();
            }
            catch
            {
                return null;
            }
            return bmpOut;
        }
    }

}