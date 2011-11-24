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
        private GridRangeView cHome;
        private bool requiresUpdate = true;
        private ListItem selectRoomType = new ListItem("-- Select a room type --", "None");
        private static Reserve reserve;
        //private static string CheckInDate;
        //private static int Nights;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Date"] == null)
            {
                Session.Add("Date", DateTime.Now.Date);
                Session.Add("RoomIndex", 0);
                cHome = new GridRangeView((DateTime)Session["Date"], 0);
            }
            else
                cHome = new GridRangeView((DateTime)Session["Date"], (int)Session["RoomIndex"]);
            
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
                calDatePicker.Visible = true;

                Row res = null;
                if(reserve != null)
                    if (reserve.reservationID != 0)
                    {
                        res = cHome.getRowInformation(reserve.reservationID);

                        if (res != null)
                        {
                            cHome.current = res.Begin;
                            Session["Date"] = cHome.current;
                            lGuestName.Text = res.GuestName;
                            lReservationID.Text = res.ReservationID + String.Empty;
                            lDetailID.Text = res.ReservationDetailID + String.Empty;
                        }
                    }

                lDID.Text = "Detail ID:";
                lResID.Text = "Reservation ID:";
                lname.Text = "Guest Name:";
                loriginal.Text = "Original Info:";
                lblNewTableInfo.Text = "<table>" +
                    "<tr><th colspan='2'>Selected Info:</th></tr>" +
                    "<tr><td>Name: </td><td id='idName'></td></tr>" +
                    "<tr><td>Reservation ID: </td><td id='idReservationID2'></td></tr>" +
                    "<tr><td>Detail ID: </td><td id='idDetailID2'></td></tr>" +
                    "</table>";
                if (res != null)
                {
                    txtReservationNumber.Text = res.ReservationDetailID + String.Empty;
                    txtReservationDate.Text = res.Begin.ToString("dd/MM/yyyy");
                    ddlNightsStayed.SelectedIndex = (res.End - res.Begin).Days - 1;
                }
            }
            else //Adding a room
            {
                mvRooms.ActiveViewIndex = 1; //Select room display
                lblPageStatus.Text = "Add Current Reservation:";
                cHome.current = DateTime.Parse(reserve.reserveDate);
                calDatePicker.SelectedDate = cHome.current;
                Session["Date"] = cHome.current;
                calDatePicker.Visible = false;

                lblDateBegin.Text = reserve.reserveDate;
                lblDateEnd.Text = DateTime.Parse(reserve.reserveDate).AddDays(reserve.daysStaying).ToString("dd/MM/yyyy");
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

            if (cHome.RoomIndex < GridRangeView.PageSize)
                lbtnPagePrevious.Enabled = false;
            else
                lbtnPagePrevious.Enabled = true;

            if (cHome.RoomIndex + GridRangeView.PageSize >= GridRangeView.MaxRooms)
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
            cHome.current = cHome.current.AddDays(-GridRangeView.DaysDisplayed);
            Session["Date"] = cHome.current;
            requiresUpdate = true;
        }

        /// <summary>
        /// Increments the current date by the DaysDisplayed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void lbtnFuture_Click(object sender, EventArgs e)
        {
            cHome.current = cHome.current.AddDays(GridRangeView.DaysDisplayed);
            Session["Date"] = cHome.current;
            requiresUpdate = true;
        }

        /// <summary>
        /// Sets the current date to the current time
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void lbtnToday_Click(object sender, EventArgs e)
        {
            cHome.current = DateTime.Now.Date;
            calDatePicker.SelectedDate = cHome.current;
            Session["Date"] = cHome.current;
            requiresUpdate = true;
        }

        /// <summary>
        /// Sets the paging controls back by the number of room defined in the GridRangeView class
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lbtnPagePrevious_Click(object sender, EventArgs e)
        {
            if (cHome.RoomIndex >= GridRangeView.PageSize)
            {
                cHome.RoomIndex -= GridRangeView.PageSize;
                Session["RoomIndex"] = cHome.RoomIndex;
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
            if (cHome.RoomIndex + GridRangeView.PageSize < GridRangeView.MaxRooms)
            {
                cHome.RoomIndex += GridRangeView.PageSize;
                Session["RoomIndex"] = cHome.RoomIndex;
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
            cHome.current = calDatePicker.SelectedDate;
            Session["Date"] = cHome.current;
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

            DateTime resDate = DateTime.Parse(txtReservationDate.Text);
            //The database can now be safely updated
            int rows = RoomDB.updateRoom(ReservationID, ReservationDetailID, RoomDB.getRoomId(txtRoomNumberUpdate.Text),
                resDate, Int32.Parse(ddlNightsStayed.Items[ddlNightsStayed.SelectedIndex].Value));

            if (rows <= 0)
                lblUpdateError.Text = "There was a problem updating the room number.";
            else
                lblUpdateError.Text = "Room was successfully changed.";

            cHome.current = resDate;
            Session["Date"] = resDate;
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

            Session["Date"] = DateTime.Parse(reserve.reserveDate);
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