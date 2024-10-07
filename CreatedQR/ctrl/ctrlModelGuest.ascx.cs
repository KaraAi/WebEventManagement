using CreatedQR.bussiness;
using CreatedQR.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CreatedQR.ctrl
{
    public partial class ctrlModelGuest : UserControlBase
    {
        UserBussiness objUser = new UserBussiness();
        LibraryCommon objLibrary = new LibraryCommon();
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                loadDDlEvent();
            }
        }
        private void ClearText()
        {
            txtFullName.Text = "";
            txtCMND.Text = "";
            txtEmail.Text = "";
            txtDescription.Text = "";
            txtFacility.Text = "";
            txtOffice.Text = "";
            txtPhone.Text = "";
            txtUserCode.Text = "";
            ddlEventID.SelectedIndex = 0;
        }
        private void loadDDlEvent()
        {
            EventBussiness objEvent = new EventBussiness();
            List<Events> lstItem = objEvent.GetAllEvents();
            if (lstItem.Count > 0)
            {
                ddlEventID.Items.Clear();
                ListItem Reward = new ListItem();
                Reward.Text = "------ Chọn Sự kiện ------";
                Reward.Value = "0";
                ddlEventID.Items.Add(Reward);
                foreach (Events type in lstItem)
                {
                    ListItem item = new ListItem();
                    item.Text = type.EventName;
                    item.Value = type.EventID.ToString();
                    ddlEventID.Items.Add(item);
                }
            }
        }
        public string CreateSerialNo(string charCode)
        {
            bool existPolicy = false;
            string result = string.Empty;
            string prefix = "";
            int numUser = objUser.getMaxCodeGuest(charCode);

            result = string.Format("{0}{1}", prefix, GetLenghtIDRemain((int)EnumLenghtCode.CodeLenghtContent, numUser));
            existPolicy = objUser.CheckCodeExits(result);

            while (!existPolicy)
            {
                numUser += 1;
                result = string.Format("{0}{1}", prefix, GetLenghtIDRemain((int)EnumLenghtCode.CodeLenghtContent, numUser));
                existPolicy = objUser.CheckCodeExits(result);
            }

            return string.Format("{0}{1}", objLibrary.RemoveUnicode(charCode.ToUpper()), result);
        }
        private string GetLenghtIDRemain(int lenghtUserID, int numUser)
        {
            string result = string.Empty;
            result = numUser.ToString().PadLeft(lenghtUserID, '0');
            return result;
        }

        public void LoadDetailUser(int userID)
        {
            User item = objUser.GetUsertByID(userID);
            if (item != null)
            {
                hdUserID.Value = item.UserID.ToString();
                txtFullName.Text = item.FullName.ToString();
                txtCMND.Text = item.CCCD.ToString();
                txtEmail.Text = item.Email.ToString();
                txtDescription.Text = item.Description.ToString();
                txtFacility.Text = item.Facility.ToString();
                txtOffice.Text = item.Office.ToString();
                txtPhone.Text = item.Phone.ToString();
                txtUserCode.Text = item.UserCode.ToString();
                ddlEventID.SelectedValue = item.EventID.ToString();
            }
            DisplayBlock();
        }
        protected void lbtInsertUser_Click(object sender, EventArgs e)
        {
            int userID = 0;
            int.TryParse(hdUserID.Value, out userID);
            if (userID == 0)
            {
                Insert();
            }
            else
            {
                Update(userID);
            }
        }
        private void Insert()
        {
            try
            {
                string FullName = txtFullName.Text.ToString();
                string UserCode = CreateSerialNo("Guest-" + DateTime.Now.ToString("yyyy") + "/");
                int Event = int.Parse(ddlEventID.SelectedValue.ToString());
                string CCCD = txtCMND.Text.ToString();
                string Phone = txtPhone.Text.ToString();
                string Facility = txtFacility.Text.ToString();
                string Office = txtOffice.Text.ToString();
                string Email = txtEmail.Text.ToString();
                int IsCheck = 0;
                if (chkIsCheck.Checked == true)
                    IsCheck = 1;
                string Description = txtDescription.Text.ToString();
                string UserName = this.UserNameLogin;
                User item = objUser.InsertUser(Event,UserCode,FullName,CCCD,Phone,Facility,Office,Email,IsCheck,Description ,UserName);
                if (item != null)
                {

                    this.Page.GetType().InvokeMember("LoadListUser", System.Reflection.BindingFlags.InvokeMethod, null, this.Page, new object[] { });
                    ClearText();
                    CommonClass.MessageBox.Show("Thêm mới Khách mời thành công !");
                    DisplayNone();
                }
                else
                    CommonClass.MessageBox.Show("Lỗi thêm mới Khách mời.");

            }
            catch
            {
                CommonClass.MessageBox.Show("Lỗi thêm mới Khách mời.");
            }
        }

        private void Update(int userID)
        {
            try
            {
                string FullName = txtFullName.Text.ToString();
                string UserCode = CreateSerialNo("Guest-" + DateTime.Now.ToString("yyyy") + "/");
                int Event = int.Parse(ddlEventID.SelectedValue.ToString());
                string CCCD = txtCMND.Text.ToString();
                string Phone = txtPhone.Text.ToString();
                string Facility = txtFacility.Text.ToString();
                string Office = txtOffice.Text.ToString();
                string Email = txtEmail.Text.ToString();
                int IsCheck = Convert.ToInt16(chkIsCheck.Checked);
                string Description = txtDescription.Text.ToString();
                string UserName = this.UserNameLogin;
                User item = objUser.UpdateUser(userID, Event, UserCode, FullName, CCCD, Phone, Facility, Office, Email, IsCheck, Description, UserName);
                if (item != null)
                {

                    this.Page.GetType().InvokeMember("LoadListUser", System.Reflection.BindingFlags.InvokeMethod, null, this.Page, new object[] { });
                    ClearText();
                    CommonClass.MessageBox.Show("Cập nhật Khách mời thành công !");
                    DisplayNone();
                }
                else
                    CommonClass.MessageBox.Show("Lỗi cập nhật Khách mời.");

            }
            catch
            {
                CommonClass.MessageBox.Show("Lỗi cập nhật Khách mời.");
            }
        }
        protected void lbtClose_Click(object sender, EventArgs e)
        {
            ClearText();
            DisplayNone();
        }
        protected void lbtCloseTop_Click(object sender, EventArgs e)
        {
            ClearText();
            DisplayNone();
        }
        private void DisplayBlock()
        {
            myModelGuest.Attributes.Add("style", "display:block;");
            myModelGuest.Attributes.Add("class", "modal fade in");
        }
        private void DisplayNone()
        {
            myModelGuest.Attributes.Add("style", "display:none;");
            myModelGuest.Attributes.Add("class", "modal fade in");
        }
    }
}