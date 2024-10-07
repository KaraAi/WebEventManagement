using CreatedQR.bussiness;
using CreatedQR.models;
using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

namespace CreatedQR.ctrl
{
    public partial class ctrlMenuLeftTop : UserControlBase
    {
        AdminnistratorBussiness objAdministrator = new AdminnistratorBussiness();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Username"] != null)
            {
                ltrUserCode1.Text = Session["Username"].ToString();
            }
            loadImagesAvarta(imgUser1);
        }
        public void loadImagesAvarta(Image imgLoginAvarta)
        {
            int staffID = 0;
            if (Session["ID"] != null)
                staffID = int.Parse(Session["ID"].ToString());
            if (staffID != 0)
            {
                imgLoginAvarta.ImageUrl = "~/images/img.jpg";

                Administrator item = objAdministrator.getAdminByID(staffID);
                if (item != null)
                {
                    string imagePath = "";//item.ImagesPaths;
                    if (!string.IsNullOrEmpty(imagePath))
                    {
                        byte[] bytes = Convert.FromBase64String(imagePath);
                        ImageFormat isImage = GetImageFormat(bytes);
                        if (isImage != ImageFormat.Icon)
                        {
                            imgLoginAvarta.ImageUrl = "data:image/jpeg;base64," + Convert.ToBase64String(bytes);
                            imgLoginAvarta.AlternateText = item.FullName;
                        }
                    }
                }
            }
        }
    }
}