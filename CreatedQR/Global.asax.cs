using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Security;
using System.Web.SessionState;

namespace CreatedQR
{
    public class Global : HttpApplication
    {
        protected void Application_Start()
        {

        }
        void Application_End(object sender, EventArgs e)
        {
            //  Code that runs on application shutdown

        }

        void Application_Error(object sender, EventArgs e)
        {
            // Code that runs when an unhandled error occurs

        }
        protected void Application_PreSendRequestHeaders(object sender, EventArgs e)
        {
            HttpApplication app = sender as HttpApplication;
            if (app != null &&
                app.Context != null)
            {
                app.Context.Response.Headers.Remove("Server");
                app.Context.Response.Headers.Remove("X-Powered-By");
                app.Context.Response.Headers.Remove("X-AspNet-Version");
                app.Context.Response.Headers.Remove("X-AspNetMvc-Version");
            }
        }

        void Session_Start(object sender, EventArgs e)
        {
            // Code that runs when a new session is started

            //Session["PdfPathName"] = string.Empty;
            //Session["UserID"] = 0;
            //Session["Username"] = string.Empty;
            //Session["FullName"] = string.Empty;
            //Session["PassWord"] = true;
            //Session["TypeUserID"] = 0;
        }

        void Session_End(object sender, EventArgs e)
        {
            // Code that runs when a session ends. 
            // Note: The Session_End event is raised only when the sessionstate mode
            // is set to InProc in the Web.config file. If session mode is set to StateServer 
            // or SQLServer, the event is not raised.
            Session["UserID"] = 0;
            Session["Username"] = string.Empty;
            Session["FullName"] = string.Empty;
            Session["ASP.NET_SessionId"] = string.Empty;
            Session["PassWord"] = true;
            Session["TypeUserID"] = 0;

            Session.Abandon();
            Session.Clear();
            ExpireAllCookies();
        }
        private void ExpireAllCookies()
        {
            if (HttpContext.Current != null)
            {
                int cookieCount = HttpContext.Current.Request.Cookies.Count;
                for (var i = 0; i < cookieCount; i++)
                {
                    var cookie = HttpContext.Current.Request.Cookies[i];
                    if (cookie != null)
                    {
                        var expiredCookie = new HttpCookie(cookie.Name)
                        {
                            Expires = DateTime.Now.AddDays(-1),
                            Domain = cookie.Domain
                        };
                        HttpContext.Current.Response.Cookies.Add(expiredCookie); // overwrite it
                    }
                }

                // clear cookies server side
                HttpContext.Current.Request.Cookies.Clear();
            }
        }
    }
}