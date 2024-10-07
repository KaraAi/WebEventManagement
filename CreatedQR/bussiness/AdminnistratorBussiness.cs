using CreatedQR.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CreatedQR.bussiness
{
    public class AdminnistratorBussiness
    {
        #region Properties......
        CreateQREntities db = new CreateQREntities();
        #endregion

        public Administrator CheckAdminlogin(string userName, string passWord)
        {
            Administrator userLogin = db.Administrator.Where(s => s.UserName == userName).FirstOrDefault();
            return userLogin;
        }
        public Administrator getAdminByID(int ID)
        {
            Administrator user = db.Administrator.Where(s => s.ID == ID).FirstOrDefault();
            return user;
        }
    }
}