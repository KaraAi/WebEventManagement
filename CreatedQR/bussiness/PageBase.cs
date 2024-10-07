using Itech.Utils;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.UI.WebControls;

namespace CreatedQR.bussiness
{
    public class PageBase : System.Web.UI.Page
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
        //    set { userIDLogin = int.Parse(Session["TypeUserID"].ToString()); }
        //}

        public string PdfPathName
        {
            get { return Session["PdfPathName"].ToString(); }
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

    }
}