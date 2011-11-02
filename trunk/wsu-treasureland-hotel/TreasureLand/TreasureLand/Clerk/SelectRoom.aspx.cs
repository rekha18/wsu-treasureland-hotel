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

        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack)
                return;
            
            //SESSION key = "Room". ReservationID
            //Assuming that if a room is passed in the clerk is selecting a room
            //for a reservation. If no room value exists, then the clerk is updating
            //an existing reservation by removing it to a different room
            if (Session["Room"] == null)
            {
                mvRooms.ActiveViewIndex = 0; //Update room display
                lblPageStatus.Text = "Move Reservation to a Different Room:";
            }
            else //Adding a room
            {
                mvRooms.ActiveViewIndex = 1; //Select room display
                lblPageStatus.Text = "Add Current Reservation:";
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
                lbtnPagePrevious.Enabled = true;

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
            GridRangeView.RoomIndex -= GridRangeView.PageSize;
            requiresUpdate = true;
        }

        /// <summary>
        /// Sets the paging controls forward by the number of room defined in 
        /// the GridRangeView class
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lbtnPageNext_Click(object sender, EventArgs e)
        {
            GridRangeView.RoomIndex += GridRangeView.PageSize;
            requiresUpdate = true;
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

            int ReservationID = Int32.Parse(txtReservationNumber.Text);
            string errors = testDateRangeCollision(ReservationID, txtRoomNumberUpdate.Text);
            if (errors != null)
            {
                lblUpdateError.Text = errors;
                return;
            }
            lblUpdateError.Text = String.Empty;

            //The database can now be safely updated
            int rows = RoomDB.updateRoom(ReservationID, RoomDB.getRoomId(txtRoomNumberUpdate.Text));

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
            
            int ReservationID = Int32.Parse(Session["Room"].ToString());
            string errors = testDateRangeCollision(ReservationID, txtRoomNumberSelect.Text);
            if (errors != null)
            {
                lblSelectError.Text = errors;
                return;
            }
            lblSelectError.Text = String.Empty;

            //Get the room ID
            int RoomID = RoomDB.getRoomId(txtRoomNumberSelect.Text);

            if (Session["RoomID"] == null)
                Session.Add("RoomID", RoomID);
            else
                Session["RoomID"] = RoomID;

            requiresUpdate = true;
            Session.Remove("Room");
            Response.Redirect("~/Clerk/CreateReservation.aspx");
        }
    }
}