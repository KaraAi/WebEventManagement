using CreatedQR.bussiness;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CreatedQR
{
    public partial class logout : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Session["UserID"] = 0;
            Session["Username"] = string.Empty;
            Session["FullName"] = string.Empty;
            Session["ASP.NET_SessionId"] = string.Empty;
            Session["PassWord"] = true;
            Session["TypeUserID"] = 0;

            Session.Abandon();
            Session.Clear();

            Response.Cookies["UserID"].Expires = DateTime.Now.AddDays(-1);
            Response.Cookies["Username"].Expires = DateTime.Now.AddDays(-1);
            Response.Cookies["FullName"].Expires = DateTime.Now.AddDays(-1);
            Response.Cookies["PassWord"].Expires = DateTime.Now.AddDays(-1);
            Response.Cookies["ASP.NET_SessionId"].Expires = DateTime.Now.AddDays(-1);
            Response.Cookies["TypeUserID"].Expires = DateTime.Now.AddDays(-1);
            ExpireAllCookies();

            Response.Redirect("~/logins.aspx");
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