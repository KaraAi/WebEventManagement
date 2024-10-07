using CreatedQR.bussiness;
using CreatedQR.models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CreatedQR.ctrl
{
    public partial class ctrlImportExcel : UserControlBase
    {
        UserBussiness objUser = new UserBussiness();
        EventBussiness objEvent = new EventBussiness();
        LibraryCommon objLibrary = new LibraryCommon();
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {

            }
        }
        private void ClearText()
        {
            FileUploadUser.TabIndex = 0;
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
        public string CreateSerialNo1(string charCode)
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
        protected void lbtImportUser_Click(object sender, EventArgs e)
        {
            string file_name = "";
            DataTable dt = null;

            if (!string.IsNullOrEmpty(FileUploadUser.FileName) && CheckExtention(FileUploadUser) == true)
            {
                Sycomore.UploadFile Upload = new Sycomore.UploadFile();
                Upload.StrFileName = FileUploadUser.FileName;
                Upload.StrFolder = Server.MapPath("~/images/upload/");
                Upload.Upload(FileUploadUser);
                file_name = "~/images/upload/" + Upload.StrFileName;

                //xác định file trên server
                string pathFileServer = string.Concat(Server.MapPath(file_name));
                //import
                DataSet ds = ITServicesUtil.ImportByFileExcel(pathFileServer);
                dt = ds.Tables[0];
            }
            else
            {
                CommonClass.MessageBox.Show("Vui lòng chọn file .xls để import danh sách Cá nhân !");
            }
            if (dt != null && dt.Rows.Count > 0)
            {
                if (dt.Columns.Count != 15)
                {
                    CommonClass.MessageBox.Show("File import không đúng định dạng.");
                    return;
                }

                UserBussiness objUser = new UserBussiness();
                string userName = this.UserNameLogin;
                string Notification = "";

                foreach (DataRow dr in dt.Rows)
                {
                    int STT = 0;
                    string EventName = "";
                    string UserCode = "";
                    string FullName = "";
                    string CCCD = "";
                    string Phone = "";
                    string Facility = "";
                    string Office = "";
                    string Email = "";
                    string Description = "";
                    string UserCreated = "";
                    string UserUpdated = "";
                    DateTime DateCreated = DateTime.Now;
                    DateTime DateUpdated = DateTime.Now;

                    int EventID = 0;
                    int isCheck = 0;
                    int UserID = 0;

                    int.TryParse(dr[0].ToString(), out STT);
                    EventName = dr[1].ToString();
                    UserCode = dr[2].ToString();
                    FullName = dr[3].ToString();
                    CCCD = dr[4].ToString();
                    Phone = dr[5].ToString();
                    Facility = dr[6].ToString();
                    Office = dr[7].ToString();
                    Email = dr[8].ToString();
                    if (dr.Table.Columns.Contains("9") && int.TryParse(dr[9].ToString(), out int checkValue))
                    {
                        isCheck = checkValue;
                    }
                    else
                    {
                        isCheck = 0; // Đặt mặc định là 0 nếu không có cột trong file Excel
                        dr[9] = isCheck;
                    }
                    Description = dr[10].ToString();
                    UserCreated = dr[11].ToString();
                    UserUpdated = dr[12].ToString();
                    // Lấy giá trị từ cột 'DateCreated' nếu tồn tại, nếu không gán mặc định là DateTime.Now
                    if (dr.Table.Columns.Contains("13") && DateTime.TryParse(dr[13].ToString(), out DateTime dateValue))
                    {
                        DateCreated = dateValue;
                    }
                    else
                    {
                        DateCreated = DateTime.Now; // Đặt mặc định là thời gian hiện tại
                        dr[13] = DateCreated;
                    }
                    if (dr.Table.Columns.Contains("14") && DateTime.TryParse(dr[14].ToString(), out DateTime dateValue1))
                    {
                        DateUpdated = dateValue1;
                    }
                    else
                    {
                        DateUpdated = DateTime.Now; // Đặt mặc định là thời gian hiện tại
                        dr[14] = DateUpdated;
                    }
                    //kiểm tra các field null
                    if (!string.IsNullOrEmpty(FullName) || !string.IsNullOrEmpty(CCCD) || !string.IsNullOrEmpty(EventName))
                    {
                        //check office exists
                        EventBussiness objEvent = new EventBussiness();
                        Event eventItem = objEvent.checkEventNameExists(EventName);
                        if (eventItem != null)
                            EventID = eventItem.EventID;
                        else
                        {
                            //insert
                            string eventCode = CreateSerialNo("EV-" + DateTime.Now.ToString("yyyy") + "/");
                            Event item = objEvent.insertEvent(eventCode, EventName, userName);
                            if (item != null)
                                EventID = item.EventID;
                        }

                        User UserItem = objUser.checkUserCCCDExists(CCCD);
                        if (UserItem != null)
                            UserID = UserItem.UserID;
                        else
                        {
                            int Event = EventID;
                            string Code = CreateSerialNo1("Guest-" + DateTime.Now.ToString("yyyy") + "/");
                            string Name = FullName;
                            string CCCDUser = CCCD;
                            string PhoneUser = Phone;
                            string FacilityUser = Facility;
                            string OfficeUser = Office;
                            string EmailUser = Email;
                            bool IsCheck = false;
                            string DescriptionUser = Description;
                            User insertuser = objUser.InsertUser(Event, Code, Name, CCCDUser, Phone, FacilityUser, OfficeUser, EmailUser, IsCheck, DescriptionUser, userName);
                            if (insertuser != null)
                                UserID = insertuser.UserID;
                        }
                    }

                    else
                        Notification += STT + ",";
                }
                if (string.IsNullOrEmpty(Notification))
                {
                    
                    CommonClass.MessageBox.Show("Import hoàn tất!");
                }
                else
                {
                    CommonClass.MessageBox.Show("Import hoàn tất!, Dữ liệu ở dòng " + "[ " + Notification + " ]" + " import không thành công vui lòng kiểm tra lại");
                }
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
            myModelImportExcel.Attributes.Add("style", "display:block;");
            myModelImportExcel.Attributes.Add("class", "modal fade in");
        }
        private void DisplayNone()
        {
            myModelImportExcel.Attributes.Add("style", "display:none;");
            myModelImportExcel.Attributes.Add("class", "modal fade in");
        }
    }
}