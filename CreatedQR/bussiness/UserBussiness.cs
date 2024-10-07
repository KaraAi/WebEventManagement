using CreatedQR.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CreatedQR.bussiness
{
    public class UserBussiness
    {
        #region Properties......
        CreateQREntities db = new CreateQREntities();
        #endregion


        public List<User> getViewListUser(string keySearch)
        {
            List<User> lstItem = db.User.AsEnumerable().Where(s =>
               string.IsNullOrEmpty(keySearch) || s.FullName.Standardizing().Contains(keySearch.Standardizing())
               ).ToList();
            return lstItem;
        }
        public List<User> getAllUser(string keySearch, int eventID)
        {
            List<User> groupUser = db.User.AsEnumerable().Where(s => s.EventID == eventID).ToList();
            groupUser = groupUser.AsEnumerable().Where(s =>
                (string.IsNullOrEmpty(keySearch) || s.FullName.Standardizing().Contains(keySearch.Standardizing())
                || s.FullName.Standardizing().Contains(keySearch.Standardizing()))).ToList();
            return groupUser;
        }
        public List<User> getAllUserIsCheck(string keySearch, int eventID, int isCheck)
        {
            List<User> groupUser = db.User.AsEnumerable().Where(s => s.EventID == eventID && s.isCheck == isCheck ).ToList();
            groupUser = groupUser.AsEnumerable().Where(s =>
                (string.IsNullOrEmpty(keySearch) || s.FullName.Standardizing().Contains(keySearch.Standardizing())
                || s.FullName.Standardizing().Contains(keySearch.Standardizing()))).ToList();
            return groupUser;
        }
        public User GetUsertByID(int userID)
        {
            User ev = db.User.Where(s => s.UserID == userID).FirstOrDefault();
            if (ev != null)
                return ev;
            else
                return null;
        }
        public User checkUserCCCDExists(string CCCD)
        {
            User user = db.User.Where(s => s.CCCD == CCCD).FirstOrDefault();
            if (user != null)
                return user;
            else
                return null;
        }
        public int getMaxCodeGuest(string charCode)
        {
            string strMaxAge = db.User.Where(s => s.UserCode.Contains(charCode)).Max(p => p.UserCode);
            if (!string.IsNullOrEmpty(strMaxAge))
            {
                int indexSub = strMaxAge.Length - (int)EnumLenghtCode.CodeLenghtContent;
                strMaxAge = strMaxAge.Substring(indexSub);

            }
            else
                strMaxAge = "0";
            int maxAge = 0;
            int.TryParse(strMaxAge, out maxAge);
            return maxAge + 1;
        }
        public bool CheckCodeExits(string code)
        {
            if (db.User.Where(s => s.UserCode == code).FirstOrDefault() != null)
                return false;
            return true;
        }

        public User InsertUser(int EventID, string UserCode, string FullName, string CCCD, string Phone, string Facility, string Office, string Email, int isCheck, string Description, string UserName)
        {
            try
            {
                using (CreateQREntities entityObject = new CreateQREntities())
                {
                    User user = new User();
                    user.EventID = EventID;
                    user.UserCode = UserCode;
                    user.FullName = FullName;
                    user.CCCD = CCCD;
                    user.Facility = Facility;
                    user.Office = Office;
                    user.isCheck = isCheck;
                    user.Email = Email;
                    user.Phone = Phone;
                    user.Description = Description;

                    user.UserCreated = UserName;
                    user.UserUpdated = UserName;
                    user.DateCreated = DateTime.Now;
                    user.DateUpdated = DateTime.Now;

                    entityObject.User.Add(user);
                    entityObject.SaveChanges();
                    return user;
                }
            }
            catch
            {
                return null;
            }
        }
        public User UpdateUser(int UserID, int EventID, string UserCode, string FullName, string CCCD, string Phone, string Facility, string Office, string Email, int isCheck, string Description, string UserName)
        {
            try
            {
                using(CreateQREntities entityObject = new CreateQREntities())
                {
                    User user = entityObject.User.Where(s => s.UserID == UserID).FirstOrDefault();
                    if(user != null)
                    {
                        user.EventID = EventID;
                        user.UserCode = UserCode;
                        user.FullName = FullName;
                        user.CCCD = CCCD;
                        user.Facility = Facility;
                        user.Office = Office;
                        user.isCheck = isCheck;
                        user.Email = Email;
                        user.Phone = Phone;
                        user.Description = Description;

                        user.UserUpdated = UserName;
                        user.DateUpdated = DateTime.Now;
                        entityObject.SaveChanges();
                        return user;
                    }
                }
                return null;
            }
            catch
            {
                return null;
            }
        }
        public bool deleteUser(int UserID)
        {
            try
            {
                using (CreateQREntities entityObject = new CreateQREntities())
                {
                    User user = entityObject.User.Where(s => s.UserID == UserID).FirstOrDefault();
                    if (user != null)
                    {
                        entityObject.User.Remove(user);
                        //Save to database
                        entityObject.SaveChanges();
                    }
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }


    }
}