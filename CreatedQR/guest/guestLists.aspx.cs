using CreatedQR.bussiness;
using CreatedQR.models;
using QRCoder;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing.Imaging;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Spire.Doc;
using Spire.Pdf;
using System.Security.Policy;

namespace CreatedQR.guest
{
    public partial class guestLists : PageBase
    {
        private int _IsUpdate = 1;
        private int _IsDeleted = 1;
        protected int serialNumber = 0;
        UserBussiness objUser = new UserBussiness();
        EventBussiness objEvent = new EventBussiness();
        LibraryCommon objLibrary = new LibraryCommon();
        private readonly string pdfPath = HttpContext.Current.Server.MapPath("~/GeneratedFiles/GuestListWithQR.pdf");
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                loadDDlEvent();
                LoadListUser();
            }
        }
        private void loadDDlEvent()
        {
            EventBussiness objEvent = new EventBussiness();
            List<Events> lstItem = objEvent.GetAllEvents();
            if (lstItem.Count > 0)
            {
                ddlEvent.Items.Clear();
                ListItem Reward = new ListItem();
                Reward.Text = "------ Chọn Sự kiện ------";
                Reward.Value = "0";
                ddlEvent.Items.Add(Reward);
                foreach (Events type in lstItem)
                {
                    ListItem item = new ListItem();
                    item.Text = type.EventName;
                    item.Value = type.EventID.ToString();
                    ddlEvent.Items.Add(item);
                }
            }
        }
        protected void lbtSearch_Click(object sender, EventArgs e)
        {
            string key = txtKeySearch.Value;
            int eventID = int.Parse(ddlEvent.SelectedValue.ToString());
            List<User> lstItem = objUser.getAllUser(key,eventID);
            rptGuest.DataSource = lstItem;
            rptGuest.DataBind();
        }
        protected void rptGuest_DataBinding(object sender, EventArgs e)
        {
            serialNumber = 0; // Đặt lại số thứ tự trước khi Repeater bind dữ liệu
        }
        public void LoadListUser()
        {

            string key = txtKeySearch.Value;
            int eventID = int.Parse(ddlEvent.SelectedValue.ToString());
            List<User> lstItem = objUser.getViewListUser(key);
            serialNumber = 0;
            rptGuest.DataSource = lstItem;
            rptGuest.DataBind();

        }
        protected void rptGuest_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                User item = (User)e.Item.DataItem;
                HiddenField hdUserID = e.Item.FindControl("hdUserID") as HiddenField;
                Literal ltrSTT = e.Item.FindControl("ltrSTT") as Literal;
                Literal ltrFullName = e.Item.FindControl("ltrFullName") as Literal;
                Literal ltrCCCD = e.Item.FindControl("ltrCCCD") as Literal;
                Literal ltrPhone = e.Item.FindControl("ltrPhone") as Literal;
                Literal ltrFacility = e.Item.FindControl("ltrFacility") as Literal;
                Literal ltrEmail = e.Item.FindControl("ltrEmail") as Literal;
                LinkButton lbtEdit = e.Item.FindControl("lbtEdit") as LinkButton;
                LinkButton lbtDelete = e.Item.FindControl("lbtDelete") as LinkButton;


                hdUserID.Value = item.UserID.ToString();
                ltrSTT.Text = (serialNumber + 1).ToString();
                ltrFullName.Text = item.FullName.ToString();
                ltrCCCD.Text = item.CCCD.ToString();
                ltrPhone.Text = item.Phone.ToString();
                ltrFacility.Text = item.Facility.ToString();
                ltrEmail.Text = item.Email.ToString();
                if (_IsUpdate == 1)
                    lbtEdit.Enabled = true;
                else
                    lbtEdit.Enabled = false;

                if (_IsDeleted == 1)
                    lbtDelete.Enabled = true;
                else
                    lbtDelete.Enabled = false;


                serialNumber++;
            }
        }
        protected void rptGuest_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            int userID = int.Parse(((HiddenField)e.Item.FindControl("hdUserID")).Value);
            switch (e.CommandName)
            {
                case "deleteUser":
                    if (userID != 0)
                    {
                        if (objUser.deleteUser(userID) == true)
                        {
                            LoadListUser();
                            CommonClass.MessageBox.Show("Khách mời đã được xóa!");
                        }
                        else
                            CommonClass.MessageBox.Show("Lỗi xóa Khách mời !");
                    }
                    else
                        CommonClass.MessageBox.Show("Không tìm thấy khách mời cần xóa !");

                    break;

                case "updateUser":
                    if (userID != 0)
                    {
                        ctrlModelGuest.LoadDetailUser(userID);
                        LoadListUser();
                    }
                    else
                        CommonClass.MessageBox.Show("Không tìm thấy khách mời cần chỉnh !");

                    break;

            }
        }
        
    }
}
