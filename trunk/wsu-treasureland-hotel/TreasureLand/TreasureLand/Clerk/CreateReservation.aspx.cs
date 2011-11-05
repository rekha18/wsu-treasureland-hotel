using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TreasureLand.DBM;


namespace TreasureLand.Clerk
{
    /*Items that still need to be addressed. 
     * Select Rooms need to be enabled
     * Total Quoted Cost needs to be calculated
     * Resever button needs to be enabled and programmed
     * Add Guest needs to have check against duplicate entries
     */ 

    public partial class CreateReservation : System.Web.UI.Page
    {
        public App_Code.Reserve reserving = new App_Code.Reserve();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                
                reserving.roomID = -1;
                reserving.view = 0;

                //Initial set up of dates for the reservation page
                lblDateToday.Text = DateTime.Today.Date.ToShortDateString();
                calDateFrom.SelectedDate = DateTime.Today.Date;
                lblDateFrom.Text = calDateFrom.SelectedDate.Date.ToShortDateString();

                TreasureLandDataClassesDataContext ab = new TreasureLandDataClassesDataContext();
                //Sets Discounts in drop down list
                var discounts = from d in ab.Discounts
                                where d.DiscountExpiration > DateTime.Today.Date
                                select d;

                ddlDiscounts.DataSource = discounts.ToList();
                ddlDiscounts.DataBind();
                ddlDiscounts.Items.Insert(0 , new ListItem("-No Discount-", "-1"));

            }


            //Get session roomID for room selection
            reserving = GetRoomNumber();

            if (reserving.roomID != -1)
            {
                lblResFirstName.Text = reserving.firstName;
                lblResSurName.Text = reserving.surName;
                lblResPhone.Text = reserving.phone;
                ddlAdults.SelectedIndex = reserving.numAdults;
                ddlChildren.SelectedIndex = reserving.numChild;
                ddlNumberOfDays.SelectedIndex = reserving.daysStaying - 1;
                lblDateFrom.Text = reserving.reserveDate;
                calDateFrom.SelectedDate = Convert.ToDateTime(reserving.reserveDate);
                ddlDiscounts.SelectedIndex = reserving.Discount;

                btnSelectRoom.Text = "Change Room";
                mvReservation.ActiveViewIndex = reserving.view;

                TreasureLandDataClassesDataContext db = new TreasureLandDataClassesDataContext();
                var roomInfo = from r in db.HotelRoomTypes
                               join o in db.Rooms
                               on r.HotelRoomTypeID equals o.HotelRoomTypeID
                               where (o.RoomID == reserving.roomID)
                               select new { o.RoomNumbers, r.RoomType, curreny = string.Format("{0:c}", r.RoomTypeRackRate) };
                gvRoomInfo.DataSource = roomInfo.ToList();
                gvRoomInfo.DataBind();
                gvRoomInfo.HeaderRow.Cells[0].Text = "Room Number";
                gvRoomInfo.HeaderRow.Cells[1].Text = "Room Type";
                gvRoomInfo.HeaderRow.Cells[2].Text = "Price Per Night";

                 
            }
            


            //Changes date based on number of days changed
            lblDateTo.Text = calDateFrom.SelectedDate.Date.AddDays(Convert.ToInt32(ddlNumberOfDays.SelectedValue)).ToShortDateString();
        }

        protected void btnLocateGuest_Click(object sender, EventArgs e)
        {
            //Locates guest in database based on the values given
            TreasureLandDataClassesDataContext db = new TreasureLandDataClassesDataContext();
            var guest = from g in db.Guests
                        where g.GuestFirstName == txtFirstName.Text || g.GuestSurName == txtSurName.Text || g.GuestPhone == txtPhone.Text
                        select g;
            gvGuest.DataSource = guest.ToList();
            gvGuest.DataBind();
           
        }

        protected void btnAddGuest_Click(object sender, EventArgs e)
        {
            //USes an linq to sql to insert a guest into the guest table
            TreasureLandDataClassesDataContext db = new TreasureLandDataClassesDataContext();
            Guest addGuest = new Guest();
            addGuest.GuestFirstName = txtFirstNameInsert.Text;
            addGuest.GuestSurName = txtSurNameInsert.Text;
            addGuest.GuestPhone = txtPhoneInsert.Text;
            db.Guests.InsertOnSubmit(addGuest);
            db.SubmitChanges();

            //Sets guest into the reservation fields
            lblResFirstName.Text = txtFirstNameInsert.Text;
            lblResSurName.Text = txtSurNameInsert.Text;
            lblResPhone.Text = txtPhoneInsert.Text;        
        }

        protected void calDateFrom_SelectionChanged(object sender, EventArgs e)
        {
            //Shows Date From in label and calculates Date To based on Date from and days stayed
            lblDateFrom.Text = calDateFrom.SelectedDate.Date.ToShortDateString();
            lblDateTo.Text = calDateFrom.SelectedDate.Date.AddDays(Convert.ToInt32(ddlNumberOfDays.SelectedValue)).ToShortDateString();
        }

        protected void btnSelectGuest_Click(object sender, EventArgs e)
        {
            //Sets selected guest into reservation fields
            lblResFirstName.Text = Convert.ToString(gvGuest.SelectedRow.Cells[1].Text);
            lblResSurName.Text = Convert.ToString(gvGuest.SelectedRow.Cells[0].Text);
            lblResPhone.Text = Convert.ToString(gvGuest.SelectedRow.Cells[2].Text);
            reserving.view = 2;
        }

        protected void gvGuest_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Allows user to proceed to reservation only after guest is selected
            btnSelectGuest.Enabled = true;
        }

        protected void btnSelectRoom_Click(object sender, EventArgs e)
        {
            //Collects data for session and sends to select room page
            reserving.reserveDate = lblDateFrom.Text;
            reserving.daysStaying = Convert.ToInt32(ddlNumberOfDays.SelectedValue);
            reserving.view = 0;
            reserving.firstName = lblResFirstName.Text;
            reserving.surName = lblResSurName.Text;
            reserving.phone = lblResPhone.Text;
            reserving.numAdults = ddlAdults.SelectedIndex;
            reserving.numChild = ddlChildren.SelectedIndex;
            reserving.Discount = ddlDiscounts.SelectedIndex;
       
            Response.Redirect("SelectRoom.aspx");
        }


        //Create of retrive session
        private App_Code.Reserve GetRoomNumber()
        {
            if (Session["Room"] == null)
                Session.Add("Room", reserving);
            return (App_Code.Reserve)Session["Room"];
        }


    }
}