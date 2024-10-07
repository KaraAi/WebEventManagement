using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CreatedQR.masterpage
{
    public partial class userMasterpage : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["UserName"] == null || Session["PassWord"] == null || Session["ID"] == null)
                {
                    Response.Redirect("~/logins.aspx");
                }
            }
        }
    }
}