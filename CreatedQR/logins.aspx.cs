using CreatedQR.bussiness;
using CreatedQR.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CreatedQR
{
    public partial class logins : PageBase
    {
        AdminnistratorBussiness objAdmin = new AdminnistratorBussiness();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.txtCode.Attributes.Add("onkeypress", "button_click(this,'" + this.lbtLogin.ClientID + "')");
                this.txtPassWord.Attributes.Add("onkeypress", "button_click(this,'" + this.lbtLogin.ClientID + "')");
            }
        }
        protected void lbtLogin_Click(object sender, EventArgs e)
        {
            string strUserName = CommonClass.StringValidator.GetSafeString(txtCode.Text.Trim());
            string strPassWord = CommonClass.StringValidator.GetMD5String(txtPassWord.Text.Trim());

            Administrator item = objAdmin.CheckAdminlogin(strUserName, strPassWord);
            if (item != null)
            {
                if (!string.IsNullOrEmpty(item.UserName))
                {
                    Session["ID"] = item.ID;
                    Session["UserName"] = strUserName;
                    Session["FullName"] = item.FullName;
                    Session["PassWord"] = true;

                    this.UserNameLogin = strUserName;
                    Response.Redirect("~/guest/guestLists.aspx");
                }
            }
            else
            {
                CommonClass.MessageBox.Show("Tên truy cập hoặc mật khẩu truy cập không đúng");
                txtPassWord.Text = string.Empty;
                txtPassWord.Focus();
            }

        }

    }
}