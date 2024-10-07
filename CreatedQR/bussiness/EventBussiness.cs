using CreatedQR.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Management;

namespace CreatedQR.bussiness
{
    public class EventBussiness
    {
        #region Properties......
        CreateQREntities db = new CreateQREntities();
        #endregion

        public List<Event> GetAllEvents()
        {
            List<Event> lstItem = db.Events.OrderByDescending(s => s.EventID).ToList();
            return lstItem;
        }
        public Event checkEventNameExists(string eventName)
        {
            Event item = db.Events.Where(s => s.EventName == eventName).FirstOrDefault();
            if (item != null)
                return item;
            else
                return null;
        }
        public List<Event> getViewListEvent(string keySearch)
        {
            List<Event> lstItem = db.Events.AsEnumerable().Where(s =>
               string.IsNullOrEmpty(keySearch) || s.EventName.Standardizing().Contains(keySearch.Standardizing())
               ).ToList();
            return lstItem;
        }
        public Event GetEventByID(int eventID)
        {
            Event ev = db.Events.Where(s => s.EventID == eventID).FirstOrDefault();
            if (ev != null)
                return ev;
            else
                return null;
        }
        public int getMaxCodeEvent(string charCode)
        {
            string strMaxAge = db.Events.Where(s => s.EventCode.Contains(charCode)).Max(p => p.EventCode);
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
            if (db.Events.Where(s => s.EventCode == code).FirstOrDefault() != null)
                return false;
            return true;
        }
        public Event insertEvent(string EventCode,string TimeName, string UserName)
        {
            try
            {
                using (CreateQREntities entityObject = new CreateQREntities())
                {
                    Event events = new Event();
                    events.EventCode = EventCode;
                    events.EventName = TimeName;
                    events.UserCreated = UserName;
                    events.UserUpdated = UserName;
                    events.DateCreated = DateTime.Now;
                    events.DateUpdated = DateTime.Now;

                    entityObject.Events.Add(events);

                    entityObject.SaveChanges();
                    return events;
                }
            }
            catch
            {
                return null;
            }
        }

        public Event updateEvent(int EventID, string EventCode, string EventName, string UserName)
        {
            try
            {
                using (CreateQREntities entityObject = new CreateQREntities())
                {
                    Event events = entityObject.Events.Where(s => s.EventID == EventID).FirstOrDefault();
                    if (events != null)
                    {
                        events.EventCode = EventCode;
                        events.EventName = EventName;

                        events.DateUpdated = DateTime.Now;
                        events.UserUpdated = UserName;

                        entityObject.SaveChanges();
                        return events;
                    }
                }
                return null;
            }
            catch
            {
                return null;
            }
        }
        public bool deleteEvent(int EventID)
        {
            try
            {
                using (CreateQREntities entityObject = new CreateQREntities())
                {
                    Event events = entityObject.Events.Where(s => s.EventID == EventID).FirstOrDefault();
                    if (events != null)
                    {
                        entityObject.Events.Remove(events);
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