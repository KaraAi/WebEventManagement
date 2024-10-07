using CreatedQR.bussiness;
using CreatedQR.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CreatedQR.guest
{
    public partial class guestIsCheckLists : PageBase
    {
        protected int serialNumber = 0;
        UserBussiness objUser = new UserBussiness();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                loadDDlEvent();
            }

        }
        private void loadDDlEvent()
        {
            EventBussiness objEvent = new EventBussiness();
            List<Event> lstItem = objEvent.GetAllEvents();
            if (lstItem.Count > 0)
            {
                ddlEvent.Items.Clear();
                ListItem Reward = new ListItem();
                Reward.Text = "------ Chọn Sự kiện ------";
                Reward.Value = "0";
                ddlEvent.Items.Add(Reward);
                foreach (Event type in lstItem)
                {
                    ListItem item = new ListItem();
                    item.Text = type.EventName;
                    item.Value = type.EventID.ToString();
                    ddlEvent.Items.Add(item);
                }
            }
        }
        protected void chkAll_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox chkAll = (CheckBox)sender;
            // Duyệt qua từng item trong Repeater và cập nhật trạng thái CheckBox con
            foreach (RepeaterItem item in rptGuest.Items)
            {
                CheckBox chkCheck = (CheckBox)item.FindControl("chkCheck");
                if (chkCheck != null)
                {
                    chkCheck.Checked = chkAll.Checked;
                }
            }
        }
        protected void lbtSearch_Click(object sender, EventArgs e)
        {
            string key = txtKeySearch.Value;
            int eventID = int.Parse(ddlEvent.SelectedValue.ToString());
            bool isCheck = ddlIsCheck.SelectedItem.Text == "Đã check in" ? true : false;
            List<User> lstItem = objUser.getAllUserIsCheck(key, eventID, isCheck);
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
            bool isCheck = ddlIsCheck.SelectedItem.Text == "Đã check in" ? true : false;
            List<User> lstItem = objUser.getAllUserIsCheck(key, eventID, isCheck);
            serialNumber = 0;
            rptGuest.DataSource = lstItem;
            rptGuest.DataBind();

        }
        protected void rptGuest_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                User item = (User)e.Item.DataItem;
                CheckBox chkCheck = e.Item.FindControl("chkCheck") as CheckBox;
                HiddenField hdUserID = e.Item.FindControl("hdUserID") as HiddenField;
                Literal ltrSTT = e.Item.FindControl("ltrSTT") as Literal;
                Literal ltrFullName = e.Item.FindControl("ltrFullName") as Literal;
                Literal ltrCCCD = e.Item.FindControl("ltrCCCD") as Literal;
                Literal ltrPhone = e.Item.FindControl("ltrPhone") as Literal;
                Literal ltrFacility = e.Item.FindControl("ltrFacility") as Literal;
                Literal ltrEmail = e.Item.FindControl("ltrEmail") as Literal;
            
      
                chkCheck.Attributes.Add("UserID", item.UserID.ToString());

                hdUserID.Value = item.UserID.ToString();
                ltrSTT.Text = (serialNumber + 1).ToString();
                ltrFullName.Text = item.FullName.ToString();
                ltrCCCD.Text = item.CCCD.ToString();
                ltrPhone.Text = item.Phone.ToString();
                ltrFacility.Text = item.Facility.ToString();
                ltrEmail.Text = item.Email.ToString();

                serialNumber++;
            }
        }
        #region SendMail......
        protected void lbtSendMail_Click(object sender, EventArgs e)
        {
            List<string> selectedFullNames = new List<string>();

            // Duyệt qua từng item trong Repeater để kiểm tra các CheckBox đã chọn
            foreach (RepeaterItem item in rptGuest.Items)
            {
                CheckBox chkCheck = (CheckBox)item.FindControl("chkCheck");
                HiddenField hdUserID = (HiddenField)item.FindControl("hdUserID");
                Literal ltrFullName = (Literal)item.FindControl("ltrFullName");

                if (chkCheck != null && chkCheck.Checked)
                {
                    // Thu thập thông tin từ Literal và các control khác
                    string fullName = ltrFullName != null ? ltrFullName.Text : string.Empty;

                    // Thêm thông tin vào danh sách
                    selectedFullNames.Add(fullName);
                }
            }
            if(selectedFullNames.Count > 0)
            {
                string EmailFrom = "caodangvanlangsaigon.2020@gmail.com";
                StringBuilder strContent = new StringBuilder();
                strContent.Append("Bạn nhận được thông tin của Administrator từ website caodangvanlangsaigon<br/><br/>");
                strContent.AppendFormat("<b>Kính gửi:</b> <br/><br/>");
                strContent.AppendFormat("<b>Địa chỉ liên lạc:</b><br/><br/>");
                strContent.AppendFormat("<b>Người liên hệ:</b> <br/><br/>");
                strContent.AppendFormat("<b>Điện thoại:</b> <br/><br/>");
                strContent.AppendFormat("<b>Email:</b> <br/><br/>");

                string cententEmail = strContent.ToString();
                SendMail("Thông tin tham dự sự kiện", cententEmail, "Administrator caodangvanlangsaigon", EmailFrom);
            }
            else
            {
                // Nếu không có CheckBox nào được chọn, hiển thị thông báo
                CommonClass.MessageBox.Show("Vui lòng chọn ít nhất một khách mời để gửi mail.");
            }
            //CheckBox chkChose = (CheckBox)sender;
            //if (chkChose.Checked == true)
            //{
            //    string EmailFrom = "caodangvanlangsaigon.2020@gmail.com";
            //    StringBuilder strContent = new StringBuilder();
            //    strContent.Append("Bạn nhận được thông tin của Administrator từ website caodangvanlangsaigon<br/><br/>");
            //    strContent.AppendFormat("<b>Kính gửi:</b> {0} <br/><br/>", FullName);
            //    strContent.AppendFormat("<b>Địa chỉ liên lạc:</b> {0} <br/><br/>", Address);
            //    strContent.AppendFormat("<b>Người liên hệ:</b> {0} <br/><br/>", Contact);
            //    strContent.AppendFormat("<b>Điện thoại:</b> {0} <br/><br/>", Phone);
            //    strContent.AppendFormat("<b>Email:</b> {0} <br/><br/>", Email);

            //    string cententEmail = strContent.ToString();
            //    SendMail("Thông tin tham dự sự kiện", cententEmail, "Administrator caodangvanlangsaigon", EmailFrom);
            //}
            //else
            //{
            //    CommonClass.MessageBox.Show("Lỗi gửi mail hệ thống");
            //}

        }
        //protected void lbtSendMail_Click(object sender, EventArgs e)
        //{
        //    List<string> selectedFullNames = new List<string>();

        //    // Duyệt qua từng item trong Repeater để kiểm tra các CheckBox đã chọn
        //    foreach (RepeaterItem item in rptGuest.Items)
        //    {
        //        CheckBox chkCheck = (CheckBox)item.FindControl("chkCheck");
        //        HiddenField hdUserID = (HiddenField)item.FindControl("hdUserID");
        //        Literal ltrFullName = (Literal)item.FindControl("ltrFullName");

        //        if (chkCheck != null && chkCheck.Checked)
        //        {
        //            // Thu thập thông tin từ Literal và các control khác
        //            string fullName = ltrFullName != null ? ltrFullName.Text : string.Empty;

        //            // Thêm thông tin vào danh sách
        //            selectedFullNames.Add(fullName);
        //        }
        //    }

        //    // Kiểm tra nếu có ít nhất một CheckBox được chọn
        //    if (selectedFullNames.Count > 0)
        //    {
        //        // Tạo nội dung email
        //        string emailContent = $"Bạn nhận được thông tin của Administrator từ website caodangvanlangsaigon<br/><br/>";
        //        emailContent += $"Các khách mời đã chọn: {string.Join(", ", selectedFullNames)}<br/><br/>";

        //        // Gửi email (giả định bạn có một hàm SendMail đã được định nghĩa)
        //        string emailFrom = "caodangvanlangsaigon.2020@gmail.com";
        //        SendMail("Thông tin tham dự sự kiện", emailContent, "Administrator caodangvanlangsaigon", emailFrom);
        //    }
        //    else
        //    {
        //        // Nếu không có CheckBox nào được chọn, hiển thị thông báo
        //        CommonClass.MessageBox.Show("Vui lòng chọn ít nhất một khách mời để gửi mail.");
        //    }
        //}
        #endregion
    }
}