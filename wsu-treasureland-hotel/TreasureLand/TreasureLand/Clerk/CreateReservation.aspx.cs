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
     * Resever button needs to be enabled and programmed
     * Check to make sure room is not occupied
     */ 

    public partial class CreateReservation : System.Web.UI.Page
    {
        public App_Code.Reserve reserving = new App_Code.Reserve();
        public decimal quotedPrice;
        public decimal adjust = 0.00M;
        public bool isPercent = false;


        protected void Page_Load(object sender, EventArgs e)
        {
            #region Initializing Data
            
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
            //Changes date based on number of days changed
            lblDateTo.Text = calDateFrom.SelectedDate.Date.AddDays(Convert.ToInt32(ddlNumberOfDays.SelectedValue)).ToShortDateString();
            #endregion Initializing Data

            #region Getting Room
            
            //Get session roomID for room selection
            reserving = GetRoomNumber();
            if (reserving.roomID != -1)
            {
                if (!IsPostBack)
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
                }
                mvReservation.ActiveViewIndex = reserving.returnView;

                TreasureLandDataClassesDataContext db = new TreasureLandDataClassesDataContext();
                var roomInfo = from r in db.HotelRoomTypes
                               join o in db.Rooms
                               on r.HotelRoomTypeID equals o.HotelRoomTypeID
                               where (o.RoomID == reserving.roomID)
                               select new { o.RoomNumbers, r.RoomType, currenct = string.Format("{0:0.00}", r.RoomTypeRackRate) };//currency = string.Format("{0:c}", r.RoomTypeRackRate) };
                gvRoomInfo.DataSource = roomInfo.ToList();
                gvRoomInfo.DataBind();
                gvRoomInfo.HeaderRow.Cells[0].Text = "Room Number";
                gvRoomInfo.HeaderRow.Cells[1].Text = "Room Type";
                gvRoomInfo.HeaderRow.Cells[2].Text = "Price Per Night";
                gvRoomInfo.SelectRow(0);

                quotedPrice = Convert.ToDecimal(gvRoomInfo.SelectedRow.Cells[2].Text.Replace("$", "").Replace(",", ""));
                quotedPrice *= Convert.ToInt16(ddlNumberOfDays.SelectedValue);
            }

            else
            {

                mvReservation.ActiveViewIndex = reserving.view;
            }
            #endregion Getting Room

            #region Discount Grid View Control

            if (ddlDiscounts.SelectedIndex > 0)
            {
                TreasureLandDataClassesDataContext db = new TreasureLandDataClassesDataContext();
                var discount = from d in db.Discounts
                               where d.DiscountID == Convert.ToInt16(ddlDiscounts.SelectedValue)
                               select new { d.DiscountRules, d.DiscountAmount, d.IsPrecentage };
                gvDiscount.DataSource = discount.ToList();
                gvDiscount.DataBind();
                gvDiscount.SelectRow(0);
                adjust = Convert.ToDecimal(gvDiscount.SelectedRow.Cells[1].Text);
                isPercent = ((CheckBox)gvDiscount.SelectedRow.Cells[2].Controls[0]).Checked;
            }
            else
            {
                gvDiscount.DataSource = null;
                gvDiscount.DataBind();
                adjust = 0.00M;
                isPercent = false;
            }
            #endregion Discount Grid View Control

            #region Quote Calculator

            if (isPercent)
            {
                adjust /= 100;
                quotedPrice -= quotedPrice * adjust;
            }
            else
            {
                quotedPrice -= adjust;
            }
            lblTotalCost.Text = quotedPrice.ToString();
            #endregion Quote Calculator

            
        }

        #region Locate Guest Button

        protected void btnLocateGuest_Click(object sender, EventArgs e)
        {
            //Locates guest in database based on the values given
            TreasureLandDataClassesDataContext db = new TreasureLandDataClassesDataContext();
            var guest = from g in db.Guests
                        where g.GuestFirstName == txtFirstName.Text || g.GuestSurName == txtSurName.Text || g.GuestPhone == txtPhone.Text
                        select g;
            gvGuest.DataSource = guest.ToList();
            gvGuest.DataBind();

            if (gvGuest.Rows.Count == 0)
            {
                lblErrorNoGuest.Text = "No guests where found please create a new guest";
            }
            else
            {
                lblErrorNoGuest.Text = "";
            }
           
        }
        #endregion Locate Guest Button

        #region Calander Selection Change Event

        protected void calDateFrom_SelectionChanged(object sender, EventArgs e)
        {
            if (calDateFrom.SelectedDate.Date < DateTime.Today.Date)
            {
                lblError.Text = "Cannot select previous dates";
                calDateFrom.SelectedDate = DateTime.Today.Date;
                lblDateFrom.Text = calDateFrom.SelectedDate.Date.ToShortDateString();
                lblDateTo.Text = calDateFrom.SelectedDate.Date.AddDays(Convert.ToInt32(ddlNumberOfDays.SelectedValue)).ToShortDateString();
            }
            else
            {
                //Shows Date From in label and calculates Date To based on Date from and days stayed
                lblError.Text = "";
                lblDateFrom.Text = calDateFrom.SelectedDate.Date.ToShortDateString();
                lblDateTo.Text = calDateFrom.SelectedDate.Date.AddDays(Convert.ToInt32(ddlNumberOfDays.SelectedValue)).ToShortDateString();

            }
        }
        #endregion Calander Selection Change Event

        #region Select Guest Buttons

        protected void btnSelectGuest_Click(object sender, EventArgs e)
        {
            //Sets selected guest into reservation fields
            lblResFirstName.Text = Convert.ToString(gvGuest.SelectedRow.Cells[2].Text);
            lblResSurName.Text = Convert.ToString(gvGuest.SelectedRow.Cells[1].Text);
            lblResPhone.Text = Convert.ToString(gvGuest.SelectedRow.Cells[3].Text);
            reserving.GuestID = Convert.ToInt16(gvGuest.SelectedRow.Cells[0].Text);
            reserving.view = 2;
        }


        #endregion Select Guest Buttons

        #region Grid View Guest Index Change Event

        protected void gvGuest_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Allows user to proceed to reservation only after guest is selected
            btnSelectGuest.Enabled = true;
        }

        #endregion Grid View Guest Index Change Event

        #region Select Room Button

        protected void btnSelectRoom_Click(object sender, EventArgs e)
        {
            //Collects data for session and sends to select room page
            Session.Clear();
            reserving = GetRoomNumber();
            reserving.reserveDate = lblDateFrom.Text;
            reserving.daysStaying = Convert.ToInt32(ddlNumberOfDays.SelectedValue);
            reserving.view = 2;
            reserving.returnView = 3;
            reserving.firstName = lblResFirstName.Text;
            reserving.surName = lblResSurName.Text;
            reserving.phone = lblResPhone.Text;
            reserving.numAdults = ddlAdults.SelectedIndex;
            reserving.numChild = ddlChildren.SelectedIndex;
            reserving.Discount = ddlDiscounts.SelectedIndex;
       
            Response.Redirect("SelectRoom.aspx");
        }
        #endregion Select Room Button

        #region Session Control

        //Create of retrive session
        private App_Code.Reserve GetRoomNumber()
        {
            if (Session["Room"] == null)
                Session.Add("Room", reserving);
            return (App_Code.Reserve)Session["Room"];
        }
        #endregion Session Control

        protected void btnReserve_Click(object sender, EventArgs e)
        {
            if (gvRoomInfo.Rows.Count > 0)
            {
                TreasureLandDataClassesDataContext db = new TreasureLandDataClassesDataContext();
                Reservation res = new Reservation();
                ReservationDetail resDetail = new ReservationDetail();
                res.GuestID = reserving.GuestID;
                res.ReservationDate = calDateFrom.SelectedDate;
                res.ReservationStatus = 'U';
                db.Reservations.InsertOnSubmit(res);
                db.SubmitChanges();

                resDetail.ReservationID = res.ReservationID;
                resDetail.CheckinDate = calDateFrom.SelectedDate;
                resDetail.RoomID = reserving.roomID;
                resDetail.QuotedRate = quotedPrice / Convert.ToInt16(ddlNumberOfDays.SelectedValue);
                resDetail.ReservationStatus = 'A';
                resDetail.Nights = Convert.ToByte(ddlNumberOfDays.SelectedValue);
                resDetail.NumberOfAdults = Convert.ToByte(ddlAdults.SelectedValue);
                resDetail.NumberOfChildren = Convert.ToByte(ddlChildren.SelectedValue);
                db.ReservationDetails.InsertOnSubmit(resDetail);
                db.SubmitChanges();
                lblFinalReservationNumber.Text = res.ReservationID.ToString();
                reserving.returnView = 4;
            }
            else
            {
                lblError.Text = "Please select a room before commting a reservation";
            }
        }

        protected void btnBack2_Click(object sender, EventArgs e)
        {
            reserving.roomID = -1;
            reserving.view = 2;
            reserving.returnView = 3;
            Response.Redirect("SelectRoom.aspx");
        }

        protected void btnDone_Click(object sender, EventArgs e)
        {
            Session.RemoveAll();
            Response.Redirect("ClerkDefault.aspx");
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Session.RemoveAll();
            Response.Redirect("ClerkDefault.aspx");
        }

        protected void btnNewGuest_Click(object sender, EventArgs e)
        {

            TreasureLandDataClassesDataContext db = new TreasureLandDataClassesDataContext();
            var gu = from g in db.Guests.Where(g => g.GuestFirstName == txtFirstName.Text && g.GuestSurName == txtSurName.Text && g.GuestPhone == txtPhone.Text)
                     select g;
            gvGuest.DataSource = gu.ToList();
            gvGuest.DataBind();


            if (gvGuest.Rows.Count == 0)
            {
                //USes an linq to sql to insert a guest into the guest table
                Guest addGuest = new Guest();
                addGuest.GuestFirstName = txtFirstName.Text;
                addGuest.GuestSurName = txtSurName.Text;
                addGuest.GuestPhone = txtPhone.Text;
                db.Guests.InsertOnSubmit(addGuest);
                db.SubmitChanges();

                lblResFirstName.Text = txtFirstName.Text;
                lblResSurName.Text = txtSurName.Text;
                lblResPhone.Text = txtPhone.Text;
                reserving.GuestID = addGuest.GuestID;

                reserving.view = 2;
                btnNewGuest.CommandArgument = "2";
            }
            else
            {
                lblErrorInsertGuest.Text = "Guest already exists please select below or enter a new guest";
                btnNewGuest.CommandArgument = "0";
                reserving.view = 0;

            }
        }




    }
}