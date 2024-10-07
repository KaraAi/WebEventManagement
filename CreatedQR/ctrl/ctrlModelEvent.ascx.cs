using CreatedQR.bussiness;
using CreatedQR.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using static System.Runtime.CompilerServices.RuntimeHelpers;

namespace CreatedQR.ctrl
{
    public partial class ctrlModelEvent : UserControlBase
    {
        EventBussiness objEvent = new EventBussiness();
        LibraryCommon objLibrary = new LibraryCommon();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

            }
        }
        private void ClearText()
        {
            txtEventName.Text = "";
            txtEventCode.Text = "";
        }
        public string CreateSerialNo(string charCode)
        {
            bool existPolicy = false;
            string result = string.Empty;
            string prefix = "";
            int numUser = objEvent.getMaxCodeEvent(charCode);

            result = string.Format("{0}{1}", prefix, GetLenghtIDRemain((int)EnumLenghtCode.CodeLenghtContent, numUser));
            existPolicy = objEvent.CheckCodeExits(result);

            while (!existPolicy)
            {
                numUser += 1;
                result = string.Format("{0}{1}", prefix, GetLenghtIDRemain((int)EnumLenghtCode.CodeLenghtContent, numUser));
                existPolicy = objEvent.CheckCodeExits(result);
            }

            return string.Format("{0}{1}", objLibrary.RemoveUnicode(charCode.ToUpper()), result);
        }
        private string GetLenghtIDRemain(int lenghtUserID, int numUser)
        {
            string result = string.Empty;
            result = numUser.ToString().PadLeft(lenghtUserID, '0');
            return result;
        }

        public void LoadDetailEvent(int EventID)
        {
            Events ev = objEvent.GetEventByID(EventID);
            if (ev != null)
            {
                hdEventID.Value = ev.EventID.ToString();
                txtEventCode.Text = ev.EventCode.ToString();
                txtEventName.Text = ev.EventName.ToString();
            }
            DisplayBlock();
        }
        protected void lbtInsert_Click(object sender, EventArgs e)
        {
            int eventID = 0;
            int.TryParse(hdEventID.Value, out eventID);
            if (eventID == 0)
            {
                Insert();
            }
            else
            {
                Update(eventID);
            }
        }
        private void Insert()
        {
            try
            {
                string EventName = txtEventName.Text.ToString();
                string EventCode = CreateSerialNo("EV-" + DateTime.Now.ToString("yyyy") + "/");
                string UserName = this.UserNameLogin;   
                Events item = objEvent.insertEvent(EventCode,EventName, UserName);
                if (item != null)
                {

                    this.Page.GetType().InvokeMember("LoadListEvent", System.Reflection.BindingFlags.InvokeMethod, null, this.Page, new object[] { });
                    ClearText();
                    CommonClass.MessageBox.Show("Thêm mới Sự kiện thành công !");
                    DisplayNone();
                }
                else
                    CommonClass.MessageBox.Show("Lỗi thêm mới Sự kiện.");

            }
            catch
            {
                CommonClass.MessageBox.Show("Lỗi thêm mới Sự kiện.");
            }
        }

        private void Update(int eventID)
        {
            try
            {
                string EventName = txtEventName.Text.ToString();
                string EventCode = txtEventCode.Text.ToString();
                string UserName = this.UserNameLogin;
                Events item = objEvent.updateEvent(eventID, EventCode, EventName, UserName);
                if (item != null)
                {

                    this.Page.GetType().InvokeMember("LoadListEvent", System.Reflection.BindingFlags.InvokeMethod, null, this.Page, new object[] { });
                    ClearText();
                    CommonClass.MessageBox.Show("Cập nhật Sự kiện thành công !");
                    DisplayNone();
                }
                else
                    CommonClass.MessageBox.Show("Lỗi cập nhật Sự kiện.");

            }
            catch
            {
                CommonClass.MessageBox.Show("Lỗi cập nhật Sự kiện.");
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
            myModelEvent.Attributes.Add("style", "display:block;");
            myModelEvent.Attributes.Add("class", "modal fade in");
        }
        private void DisplayNone()
        {
            myModelEvent.Attributes.Add("style", "display:none;");
            myModelEvent.Attributes.Add("class", "modal fade in");
        }
    }
}