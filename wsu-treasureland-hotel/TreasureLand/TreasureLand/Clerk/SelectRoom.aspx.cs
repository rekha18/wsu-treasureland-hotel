using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TreasureLand.App_Code;

namespace TreasureLand.Clerk
{
    public partial class SelectRoom : System.Web.UI.Page
    {
        private GridRangeView cHome = new GridRangeView();
        private bool requiresUpdate = true;
        private ListItem selectRoomType = new ListItem("-- Select a room type --", "None");
        private static Reserve reserve;
        //private static string CheckInDate;
        //private static int Nights;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack)
                return;
            //else
            //{
                //Session.Add("CheckInDate", "10/31/2011");
                //Session.Add("Nights", 5);
            //}
            
            //SESSION key = "Room". ReservationID
            //Assuming that if a room is passed in the clerk is selecting a room
            //for a reservation. If no room value exists, then the clerk is updating
            //an existing reservation by removing it to a different room
            reserve = (Reserve)Session["Room"];
            if (reserve == null || reserve.view == 0)
            {
                mvRooms.ActiveViewIndex = 0; //Update room display
                lblPageStatus.Text = "Move Reservation to a Different Room:";

                if(reserve != null)
                    if (reserve.reservationID != 0)
                    {
                        DateTime time = cHome.getReservationDate(reserve.reservationID);

                        if (time != DateTime.MinValue)
                            GridRangeView.current = time;
                    }
            }
            else //Adding a room
            {
                mvRooms.ActiveViewIndex = 1; //Select room display
                lblPageStatus.Text = "Add Current Reservation:";
                GridRangeView.current = DateTime.Parse(reserve.reserveDate);
                calDatePicker.SelectedDate = GridRangeView.current;

                //CheckInDate = Session["CheckInDate"].ToString();
                //Nights = Int32.Parse(Session["Nights"].ToString());
                //Session.Remove("CheckInDate");
                //Session.Remove("Nights");
                //reserve = (Reserve)Session["Room"];
                //Session.Remove("Remove");
            }
        }

        /// <summary>
        /// Adds a default "-- Select Room Type --" to the ddlRoomTypes
        /// drop down list
        /// </summary>
        /// <param name="source"></param>
        /// <param name="e"></param>
        public void ddlRoomTypes_OnDataBind(object source, EventArgs e)
        {
            ddlRoomTypes.Items.Insert(0, selectRoomType);
        }

        /// <summary>
        /// Generates the GridRangeView object before the HTML page is
        /// rendered.
        /// </summary>
        /// <param name="sender">lbtnTable</param>
        /// <param name="e"></param>
        protected void generateTable(object sender, EventArgs e)
        {
            if (requiresUpdate)
                cHome.update();
            lblTable.Text = cHome.generateHTMLTablev3(true, ddlRoomTypes.SelectedValue);

            //Generate label information
            lbtnDatePrevious.Text = "Previous " + GridRangeView.DaysDisplayed + " Days";
            lbtnDateFuture.Text = "Next " + GridRangeView.DaysDisplayed + " Days";
            lbtnPageNext.Text = "Next " + GridRangeView.PageSize + " Rooms";
            lbtnPagePrevious.Text = "Previous " + GridRangeView.PageSize + " Rooms";

            if (GridRangeView.RoomIndex < GridRangeView.PageSize)
                lbtnPagePrevious.Enabled = false;
            else
                lbtnPagePrevious.Enabled = true;

            if (GridRangeView.RoomIndex + GridRangeView.PageSize >= GridRangeView.MaxRooms)
                lbtnPageNext.Enabled = false;
            else
                lbtnPageNext.Enabled = true;

            requiresUpdate = false;
        }

        /// <summary>
        /// Tests a specific ReservationID against all records except itself
        /// to test against existing date ranges to make sure a record will
        /// not overlap a currently existing one
        /// </summary>
        /// <param name="ReservationID">ID of the reservation</param>
        /// <param name="RoomNumber">Intended/New RoomNumber of the reservation</param>
        /// <returns>Null if no collision. Else it returns errors</returns>
        private string testDateRangeCollision(int ReservationID, string RoomNumber)
        {
            return cHome.testDateRangeCollision(ReservationID, RoomNumber);
        }

        /// <summary>
        /// Decrements the current date by the DaysDisplayed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void lbtnPrevious_Click(object sender, EventArgs e)
        {
            GridRangeView.current = GridRangeView.current.AddDays(-GridRangeView.DaysDisplayed);
            calDatePicker.SelectedDate = GridRangeView.current; 
            requiresUpdate = true;
        }

        /// <summary>
        /// Increments the current date by the DaysDisplayed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void lbtnFuture_Click(object sender, EventArgs e)
        {
            GridRangeView.current = GridRangeView.current.AddDays(GridRangeView.DaysDisplayed);
            calDatePicker.SelectedDate = GridRangeView.current; 
            requiresUpdate = true;
        }

        /// <summary>
        /// Sets the current date to the current time
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void lbtnToday_Click(object sender, EventArgs e)
        {
            GridRangeView.current = DateTime.Now.Date;
            calDatePicker.SelectedDate = GridRangeView.current;
            requiresUpdate = true;
        }

        /// <summary>
        /// Sets the paging controls back by the number of room defined in the GridRangeView class
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lbtnPagePrevious_Click(object sender, EventArgs e)
        {
            if (GridRangeView.RoomIndex >= GridRangeView.PageSize)
            {
                GridRangeView.RoomIndex -= GridRangeView.PageSize;
                lbtnPagePrevious.Focus();
                requiresUpdate = true;
            }
        }

        /// <summary>
        /// Sets the paging controls forward by the number of room defined in 
        /// the GridRangeView class
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lbtnPageNext_Click(object sender, EventArgs e)
        {
            if (GridRangeView.RoomIndex + GridRangeView.PageSize < GridRangeView.MaxRooms)
            {
                GridRangeView.RoomIndex += GridRangeView.PageSize;
                lbtnPageNext.Focus();
                requiresUpdate = true;
            }
        }

        /// <summary>
        /// Sets the GridRangeView table to display a specific date
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void calDatePicker_SelectionChanged(object sender, EventArgs e)
        {
            GridRangeView.current = calDatePicker.SelectedDate;
            requiresUpdate = true;
        }

        /// <summary>
        /// Updates a currently existing reservation by moving it to a new room
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnUpdateReservation_Click(object sender, EventArgs e)
        {
            if (txtReservationNumber.Text == String.Empty)
            {
                lblUpdateError.Text = "Reservation number was not specified";
                return;
            }
            if (txtRoomNumberUpdate.Text == String.Empty)
            {
                lblUpdateError.Text = "Room number was not specified";
                return;
            }

            int ReservationDetailID = Int32.Parse(txtReservationNumber.Text);
            string errors = testDateRangeCollision(ReservationDetailID, txtRoomNumberUpdate.Text);
            int ReservationID = cHome.getReservationNumber(ReservationDetailID);
            if (errors != null)
            {
                lblUpdateError.Text = errors;
                return;
            }
            lblUpdateError.Text = String.Empty;

            //The database can now be safely updated
            int rows = RoomDB.updateRoom(ReservationID, ReservationDetailID, RoomDB.getRoomId(txtRoomNumberUpdate.Text),
                DateTime.Parse(txtReservationDate.Text), Int32.Parse(ddlNightsStayed.Items[ddlNightsStayed.SelectedIndex].Value));

            if (rows <= 0)
                lblUpdateError.Text = "There was a problem updating the room number.";
            else
                lblUpdateError.Text = "Room was successfully changed.";

            requiresUpdate = true;
        }

        /// <summary>
        /// Assuming the reservation is already in the database (but just lacks 
        /// a definitive room number), the user will select a room number
        /// and then that room number will be passed back in a Session
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSelectRoom_Click(object sender, EventArgs e)
        {
            if (txtRoomNumberSelect.Text == String.Empty)
            {
                lblUpdateError.Text = "Room number was not specified";
                return;
            }
            
            //int ReservationID = Int32.Parse(Session["Room"].ToString());
            string errors = cHome.testDateRangeCollision(reserve.reserveDate, reserve.daysStaying, txtRoomNumberSelect.Text);
            if (errors != null)
            {
                lblSelectError.Text = errors;
                return;
            }
            lblSelectError.Text = String.Empty;

            //Get the room ID
            reserve.roomID = (short)RoomDB.getRoomId(txtRoomNumberSelect.Text);
            reserve.view = 2;

            requiresUpdate = true;
            Response.Redirect("~/Clerk/CreateReservation.aspx");
        }

        /// <summary>
        /// Simply sets focus to the ddlRoomTypes control
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ddlRoomTypes_onSelectedIndexChanged(object sender, EventArgs e)
        {
            ddlRoomTypes.Focus();
        }

        /// <summary>
        /// Modifies the internal reserve object so that the update view is not mistakenly
        /// returned to when selecting a room
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnBackToUpdateReservation_Click(object sender, EventArgs e)
        {
            reserve = reserve.Clone(); //Safely clone the object
            reserve.view = 1;
            Response.Redirect("~/Clerk/UpdateReservation.aspx");
        }
    }
}