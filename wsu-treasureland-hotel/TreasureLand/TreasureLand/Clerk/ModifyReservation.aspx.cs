using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using TreasureLand.App_Code;

namespace TreasureLand.Clerk
{
    public partial class ModifyReservation : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Reserve res = (Reserve)Session["Room"];

            if (Session["StartDate"] == null)
                Session.Add("StartDate", res.reserveDate);
            else
                Session["StartDate"] = res.reserveDate;

            if (Session["Nights"] == null)
                Session.Add("Nights", res.daysStaying);
            else
                Session["Nights"] = res.daysStaying;

            if (Session["ResDetailID"] == null)
                Session.Add("ResDetailID", res.reservationDetailID);
            else
                Session["ResDetailID"] = res.reservationDetailID;

            if(Session["LastRoomType"] == null)
                Session.Add("LastRooType", ddlRoomTypes.SelectedValue);
        }

        /// <summary>
        /// Removes the selected value from the GridView to prevent a value from
        /// being highlighted on a room type change
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ddlRoomTypes_SelectedIndexChanged(object sender, EventArgs e)
        {
            gvOpenRooms.SelectedIndex = -1;
            btnSelect.Enabled = false;
            Session["LastRoomType"] = ddlRoomTypes.SelectedValue;
        }

        /// <summary>
        /// Enables the "Change Room" button after something has been selected
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void gvOpenRooms_SelectedIndexChanged(object sender, EventArgs e)
        {
            btnSelect.Enabled = true;
        }

        /// <summary>
        /// Deselects any selected rooms when changing the page index since the
        /// chosen value's position is retained
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void gvOpenRooms_PageIndexChanged(object sender, EventArgs e)
        {
            ddlRoomTypes_SelectedIndexChanged(sender, e);
        }

        /// <summary>
        /// Updates the selected reservation to the specified room
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSelect_Click(object sender, EventArgs e)
        {
            int roomID = Convert.ToInt32(gvOpenRooms.SelectedRow.Cells[0].Text);
            
            if (cbRemoveDiscount.Checked)
                updateRemoveDiscount(roomID);
            else
                updateKeepDiscount(roomID);

            ((Reserve)Session["Room"]).roomID = (short)roomID;

            Response.Redirect("~/Clerk/UpdateReservation.aspx");
        }

        /// <summary>
        /// Updates the roomID of the specified ReservationDetailID.
        /// The default rack rate for the room is applied, and the discount
        /// is set to "No Discount"
        /// </summary>
        /// <param name="roomID">roomID to update</param>
        private void updateRemoveDiscount(int roomID)
        {
            using (SqlConnection conn = new SqlConnection(
                ConfigurationManager.ConnectionStrings["TreasureLandDB"].ConnectionString))
            {
                conn.Open();

                string command = "UPDATE [ReservationDetail] SET [RoomID] = @RoomID, " +
                                    "[DiscountID] = @DiscountID " +
                                    "[QuotedRate] = ( " +
                                       "SELECT [RoomTypeRackRate] FROM [HotelRoomType] " +
                                       "WHERE [HotelRoomTypeID] = @HotelRoomTypeID " +
                                       ") " +
                                    "WHERE [ReservationDetailID] = @ReservationDetailID";
                SqlCommand connCommand = new SqlCommand(command, conn);

                connCommand.Parameters.AddWithValue("@RoomID", roomID);
                connCommand.Parameters.AddWithValue("@ReservationDetailID", Convert.ToInt32(Session["ResDetailID"].ToString()));
                connCommand.Parameters.AddWithValue("@DiscountID", 2); //2 is assumed to be the "No Discount"
                connCommand.Parameters.AddWithValue("@HotelRoomTypeID", Convert.ToInt32(ddlRoomTypes.SelectedValue));

                connCommand.ExecuteNonQuery();
            }
        }

        /// <summary>
        /// Changes the roomID of the specified reservation detail id without
        /// modifying the rack rate and any applied discounts
        /// </summary>
        /// <param name="roomID">New roomID to change to</param>
        private void updateKeepDiscount(int roomID)
        {
            using (SqlConnection conn = new SqlConnection(
                ConfigurationManager.ConnectionStrings["TreasureLandDB"].ConnectionString))
            {
                conn.Open();

                string command = "UPDATE [ReservationDetail] SET [RoomID] = @RoomID " +
                                    "WHERE [ReservationDetailID] = @ReservationDetailID";
                SqlCommand connCommand = new SqlCommand(command, conn);

                connCommand.Parameters.AddWithValue("@RoomID", roomID);
                connCommand.Parameters.AddWithValue("@ReservationDetailID", Convert.ToInt32(Session["ResDetailID"].ToString()));

                connCommand.ExecuteNonQuery();
            }
        }

        /// <summary>
        /// Adds in a --Select a room type-- option
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ddlRoomTypes_DataBound(object sender, EventArgs e)
        {
            ddlRoomTypes.Items.Insert(0, new ListItem("Select a Room Type", "-1"));
        }

        /// <summary>
        /// Uses SQL to query results from the database. If no rows are returned,
        /// then select all rooms
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void gvOpenRooms_PreRender(object sender, EventArgs e)
        {
            if (gvOpenRooms.Rows.Count != 0 &&
                Session["LastRoomType"].ToString() == ddlRoomTypes.SelectedValue)
                return;

            using (SqlConnection conn = new SqlConnection(
                ConfigurationManager.ConnectionStrings["TreasureLandDB"].ConnectionString))
            {
                conn.Open();

                string command = "SELECT RoomID, RoomNumbers FROM Room " +
                                   "WHERE RoomID !=" +
                                   "( " +
                                      "SELECT r.RoomID FROM Room r " +
                                         "INNER JOIN HotelRoomType hrt ON hrt.HotelRoomTypeID = r.HotelRoomTypeID " +
                                         "INNER JOIN ReservationDetail rd ON rd.RoomID = r.RoomID " +
                                         "INNER JOIN Reservation res ON res.ReservationID = rd.ReservationID " +
                                         "WHERE (@StartDate <= (DATEADD(day, rd.Nights, res.ReservationDate)) AND " +
                                               "(DATEADD(day, @Nights, @StartDate)) >= res.ReservationDate ) " +
                                   ") " +
                                   "AND HotelRoomTypeID = @HotelRoomType " +
                                   "AND RoomStatus != 'M' " +
                                   "ORDER BY RoomID ";

                SqlCommand connCommand = new SqlCommand(command, conn);

                connCommand.Parameters.AddWithValue("@StartDate", Convert.ToDateTime(Session["StartDate"].ToString()));
                connCommand.Parameters.AddWithValue("@Nights", Convert.ToInt32(Session["Nights"].ToString()));
                connCommand.Parameters.AddWithValue("@HotelRoomType", ddlRoomTypes.SelectedValue.ToString());

                using (SqlDataReader openRooms = connCommand.ExecuteReader())
                {
                    if (openRooms.HasRows)
                        gvOpenRooms.DataSource = openRooms;
                    else
                    {
                        openRooms.Close();
                        //A potential flaw in the above SQL is that no results will be returned if
                        //no reservations exist in the database for the queried date range. In this
                        //case, simply return all rooms because no reservations for the date range mean
                        //that all rooms are open
                        command = "SELECT RoomID, RoomNumbers FROM Room " +
                                  "WHERE HotelRoomTypeID = @HotelRoomType " +
                                  "ORDER BY RoomID ";

                        connCommand = new SqlCommand(command, conn);

                        connCommand.Parameters.AddWithValue("@HotelRoomType", ddlRoomTypes.SelectedValue.ToString());

                        gvOpenRooms.DataSource = connCommand.ExecuteReader();
                    }

                    gvOpenRooms.DataBind();
                }
            }
        }
    }
}