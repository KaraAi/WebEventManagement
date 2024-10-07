using CreatedQR.bussiness;
using CreatedQR.ctrl;
using CreatedQR.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CreatedQR.events
{

    public partial class eventLists : PageBase
    {
        private int _IsUpdate = 1;
        private int _IsDeleted = 1;
        protected int serialNumber = 0;
        EventBussiness objEvent = new EventBussiness();
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                LoadListEvent();
            }
        }
        protected void rptEvent_ItemBinding(object sender, EventArgs e)
        {
            serialNumber = 0;
        }
        public void LoadListEvent()
        {

            string key = txtKey.Value;
            List<Event> lstItem = objEvent.getViewListEvent(key);
            serialNumber = 0;
            rptEvent.DataSource = lstItem;
            rptEvent.DataBind();
        }
        protected void lbtSearch_Click(object sender, EventArgs e)
        {

            string key = txtKey.Value;
            List<Event> lstItem = objEvent.getViewListEvent(key);
            rptEvent.DataSource = lstItem;
            rptEvent.DataBind();
        }
        protected void rptEvent_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                Event item = (Event)e.Item.DataItem;
                HiddenField hdEventID = e.Item.FindControl("hdEventID") as HiddenField;
                Literal ltrEventID = e.Item.FindControl("ltrEventID") as Literal;
                Literal ltrEventName = e.Item.FindControl("ltrEventName") as Literal;
                Literal ltrEventCode = e.Item.FindControl("ltrEventCode") as Literal;
                Literal ltrDateCreated = e.Item.FindControl("ltrDateCreated") as Literal;
                Literal ltrUserCreated = e.Item.FindControl("ltrUserCreated") as Literal;
                LinkButton lbtEdit = e.Item.FindControl("lbtEdit") as LinkButton;
                LinkButton lbtDelete = e.Item.FindControl("lbtDelete") as LinkButton;



                hdEventID.Value = item.EventID.ToString();
                ltrEventID.Text = (serialNumber + 1).ToString();
                ltrEventName.Text = item.EventName.ToString();
                ltrUserCreated.Text = item.UserCreated.ToString();
                ltrDateCreated.Text = item.DateCreated.ToString("dd/MM/yyyy");
                ltrEventCode.Text = item.EventCode.ToString();
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
        protected void rptEvent_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            int eventID = int.Parse(((HiddenField)e.Item.FindControl("hdEventID")).Value);
            switch (e.CommandName)
            {
                case "deleteEvent":
                    if (eventID != 0)
                    {

                        if (objEvent.deleteEvent(eventID) == true)
                        {
                            LoadListEvent();
                            CommonClass.MessageBox.Show("Sự kiện đã được xóa !");
                        }
                        else
                            CommonClass.MessageBox.Show("Lỗi xóa Sự kiện !");
                    }
                    else
                        CommonClass.MessageBox.Show("Không tìm thấy Sự kiện cần xóa!");

                    break;

                case "updateEvent":
                    if (eventID != 0)
                    {
                        ctrlModelEvent.LoadDetailEvent(eventID);
                        LoadListEvent();
                    }
                    else
                        CommonClass.MessageBox.Show("Không tìm thấy Sự kiện cần chỉnh !");

                    break;

            }
        }
    }
}