using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Configuration;
using System.Collections;
using System.Data;

namespace TreasureLand.App_Code
{
    /// <summary>
    /// Summary description for RoomDB
    /// </summary>
    public partial class GuestDB
    {
        //private static string connString = "Data Source=.\\SQLEXPRESS;Initial Catalog=Ghana_Hotel;Integrated Security=True";
        private static IFormatProvider dateFormat = new System.Globalization.CultureInfo("en-GB");

        /// <summary>
        /// Returns the corresponding connection string
        /// </summary>
        /// <returns></returns>
        private static string getConnectionString()
        {
            return ConfigurationManager.ConnectionStrings["TreasureLandDB"].ConnectionString;
        }

        /// <summary>
        /// Counts the total number of rooms currently in the database
        /// </summary>
        /// <returns>The total number of rooms in the database</returns>
        /// <summary>
        /// Searches for a current guest based on the entered information.
        /// </summary>
        /// <param name="FirstName">First Name of guest</param>
        /// <param name="SurName">SurName of guest</param>
        /// <param name="ReservationID">Reservation ID numbmer</param>
        /// <param name="RoomNumber">Room Number</param>
        /// <returns></returns>
        public static IEnumerable LocateGuestRoom(string FirstName, string SurName, string ReservationID)
        {
            SqlConnection con = new SqlConnection(getConnectionString());
            string sel =
                "SELECT Reservation.ReservationID, Guest.GuestFirstName, Guest.GuestSurName, ReservationDetail.ReservationDetailID, ReservationDetail.RoomID, ReservationDetail.Status FROM Reservation INNER JOIN Guest ON Reservation.GuestID = Guest.GuestID INNER JOIN ReservationDetail ON Reservation.ReservationID = ReservationDetail.ReservationID " +
                "WHERE (Guest.GuestFirstName = '" + FirstName + "' OR Guest.GuestSurName = '" + SurName + "' OR Reservation.ReservationID = '" + ReservationID + "') AND ReservationDetail.Status ='A'";
            SqlCommand cmd =
            new SqlCommand(sel, con);
            con.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            return dr;
        }

        public static IEnumerable LocateGuestCheckIn(string FirstName, string SurName, string ReservationID)
        {
            SqlConnection con = new SqlConnection(getConnectionString());
            string sel =
                "SELECT Reservation.ReservationID, Guest.GuestFirstName, Guest.GuestSurName, Guest.GuestPhone, ReservationDetail.ReservationDetailID, ReservationDetail.RoomID FROM Reservation INNER JOIN Guest ON Reservation.GuestID = Guest.GuestID INNER JOIN ReservationDetail ON Reservation.ReservationID = ReservationDetail.ReservationID " +
                "WHERE (Guest.GuestFirstName = '" + FirstName + "' OR Guest.GuestSurName = '" + SurName + "' OR Reservation.ReservationID = '" + ReservationID + "') AND Reservation.ReservationStatus = 'C'";
            SqlCommand cmd =
            new SqlCommand(sel, con);
            con.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            return dr;
        }

        //public static IEnumerable LocateGuestFolio(string Salutation, string FirstName, string Surname, string Phone, string CreditCardNum, string Expiration, string Address, string City, string State, string Country, string PostalCode, string Email)
        public static IEnumerable LocateGuestFolio(string FirstName, string SurName, string PhoneNumber)
        {
            SqlConnection con = new SqlConnection(getConnectionString());
            string sel =
             "SELECT [GuestFirstName], [GuestSurName], [GuestEmail], [GuestPhone], [GuestID] FROM [Guest] WHERE ([GuestFirstName] = @GuestFirstName OR [GuestSurName] = @Guestsurname OR [GuestPhone] = @GuestPhone)";
            SqlCommand cmd =
            new SqlCommand(sel, con);
            cmd.Parameters.AddWithValue("@GuestFirstName", FirstName);
            cmd.Parameters.AddWithValue("@GuestSurname", SurName);
            cmd.Parameters.AddWithValue("@GuestPhone", PhoneNumber);
            con.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            return dr;
        }

        public static IEnumerable getGuestServices()
        {
            SqlConnection con = new SqlConnection(getConnectionString());
            string sel =
                "SELECT BillingCategoryID, BillingCategoryDescription FROM BillingCategory";
            SqlCommand cmd =
            new SqlCommand(sel, con);
            con.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            return dr;
        }

        public static IEnumerable getAllDiscounts()
        {
            SqlConnection con = new SqlConnection(getConnectionString());
            string sel =
                "SELECT * FROM Discount";
            SqlCommand cmd =
            new SqlCommand(sel, con);
            con.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            return dr;
        }

        /// <summary>
        /// Gets the total of all billed items to a specific Reservation and muliplies by the quantity
        /// </summary>
        /// <param name="reservationID">reservation Detail ID</param>
        /// <returns>Total bill</returns>
        public static double getTotal(int reservationID)
        {
            SqlConnection con = new SqlConnection(getConnectionString());
            string sel = "SELECT SUM(BillingAmount * BillingItemQty) AS BillTotal FROM ReservationDetailBilling WHERE ReservationDetailID = " + reservationID;

            SqlCommand cmd = new SqlCommand(sel, con);
            con.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            ArrayList myArrList = new ArrayList();
            while (dr.Read())
            {
                // add the column value to the ArrayList 
                myArrList.Add(dr["BillTotal"].ToString());
            }
            if (myArrList[0] != "")
            {
                return Convert.ToDouble(myArrList[0]);
            }
            else
                return 0;
        }

        /// <summary>
        /// Inserts a service bill 
        /// </summary>
        /// <param name="billAmount">amount per item</param>
        /// <param name="billQty">number of items</param>
        /// <param name="billingDescription">description of item</param>
        /// <param name="reservationID">ReservationID to add to</param>
        /// <returns></returns>
        public static IEnumerable insertGuestServices(double billAmount, int billQty, string billingDescription, int reservationID)
        {
            SqlConnection con = new SqlConnection(getConnectionString());
            string update = "INSERT INTO ReservationDetailBilling " +
                        "(BillingAmount, BillingItemQty, BillingCategoryID,  BillingDescription, ReservationDetailID, BillingItemDate) " +
                        "VALUES(" + billAmount + "," + billQty + ",1,'" + billingDescription + "'," + reservationID + ", " + System.DateTime.Now.ToString("d") + ")";
            SqlCommand cmd =
            new SqlCommand(update, con);
            con.Open();
            SqlDataReader dr = cmd.ExecuteReader();

            return dr;
        }


        public static IEnumerable getGuestRoom(int roomID)
        {
            SqlConnection con = new SqlConnection(getConnectionString());
            string sel = "SELECT Reservation.ReservationID, ReservationDetail.Nights, ReservationDetail.QuotedRate, Room.RoomDescription " +
                         "FROM Reservation INNER JOIN ReservationDetail ON Reservation.ReservationID = ReservationDetail.ReservationID INNER JOIN " +
                         "Room ON ReservationDetail.RoomID = Room.RoomID WHERE ReservationDetail.RoomID = '" + roomID + "'";
            SqlCommand cmd =
            new SqlCommand(sel, con);
            con.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            return dr;
        }

        /// <summary>
        /// Gets all transaction for a specific reservationDetailBilling ID
        /// </summary>
        /// <param name="reservationID"></param>
        /// <returns></returns>
        public static IEnumerable getGuestServices(int reservationDetailID)
        {
            SqlConnection con = new SqlConnection(getConnectionString());
            string sel = "SELECT BillingDescription, BillingItemQty, BillingItemDate, BillingAmount, ReservationBillingID FROM ReservationDetailBilling WHERE ReservationDetailID = " + reservationDetailID;
            SqlCommand cmd = new SqlCommand(sel, con);
            con.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            return dr;
        }

        /// <summary>
        /// Gets discount for current reservation
        /// </summary>
        /// <param name="reservationID"></param>
        /// <returns></returns>
        public static ArrayList getGuestDiscount(int reservationDetailID)
        {
            SqlConnection con = new SqlConnection(getConnectionString());
            string sel = "SELECT ReservationDetail.DiscountID AS DiscountID, Discount.DiscountAmount AS DiscountAmount, Discount.IsPrecentage AS IsPercent FROM Discount INNER JOIN ReservationDetail ON Discount.DiscountID = ReservationDetail.DiscountID WHERE ReservationDetail.ReservationDetailID = " + reservationDetailID;

            SqlCommand cmd = new SqlCommand(sel, con);
            con.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            ArrayList myArrList = new ArrayList();
            while (dr.Read())
            {
                // add the column value to the ArrayList 
                myArrList.Add(dr["DiscountID"].ToString());
                myArrList.Add(dr["DiscountAmount"].ToString());
                myArrList.Add(dr["IsPercent"].ToString());
            }
            return myArrList;
        }

        /// <summary>
        /// Updates a guest service in the database
        /// </summary>
        /// <param name="BillingDetailID">Unique id for the service</param>
        /// <param name="Qty">number of services</param>
        /// <param name="Cost">cost of each service</param>
        /// <param name="Comments">any additional comments (not a required)</param>
        /// <returns></returns>
        public static int updateService(int BillingDetailID, int Qty, double Cost, string Comments)
        {
            SqlConnection conn = new SqlConnection(getConnectionString());

            try
            {
                conn.Open(); //Open the connection

                string update = "UPDATE [ReservationDetailBilling] SET [BillingItemQty] = @Qty, [BillingAmount] = @Cost " +
                                 "WHERE [ReservationBillingID] = @ReservationBillingID";
                SqlCommand connCommand = new SqlCommand(update, conn);
                connCommand.Parameters.AddWithValue("@Qty", Qty);
                connCommand.Parameters.AddWithValue("@Cost", Cost);
                connCommand.Parameters.AddWithValue("@Comments", Comments);
                connCommand.Parameters.AddWithValue("@ReservationBillingID", BillingDetailID);
                return connCommand.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                throw e;
            }

        }

        /// <summary>
        /// Deletes a service in the database
        /// </summary>
        /// <param name="BillingDetailID">unique id</param>
        /// <returns></returns>
        public static int deleteService(int BillingDetailID)
        {
            SqlConnection conn = new SqlConnection(getConnectionString());

            try
            {
                conn.Open(); //Open the connection

                string update = "DELETE [ReservationDetailBilling] " +
                                 "WHERE [ReservationBillingID] = @ReservationBillingID";

                SqlCommand connCommand = new SqlCommand(update, conn);
                connCommand.Parameters.AddWithValue("@ReservationBillingID", BillingDetailID);
                return connCommand.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// get all the selected customers information
        /// </summary>
        /// <param name="GuestIDNumber"></param>
        /// <returns></returns>
        public static ArrayList getCustomerFolio(int GuestIDNumber)
        {

            SqlConnection con = new SqlConnection(getConnectionString());
            string sel = "SELECT GuestSalutation, GuestFirstName, GuestSurName, GuestAddress, GuestCity, GuestRegion, GuestPostalCode, GuestCountry, GuestPhone, GuestEmail, GuestIDNumber, GuestIDIssueCountry, GuestComments FROM Guest WHERE GuestID = " + GuestIDNumber;
            SqlCommand cmd = new SqlCommand(sel, con);
            con.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            ArrayList myArrList = new ArrayList();
            while (dr.Read())
            {
                // add the column value to the ArrayList 
                myArrList.Add(dr["GuestSalutation"].ToString());
                myArrList.Add(dr["GuestFirstName"].ToString());
                myArrList.Add(dr["GuestSurName"].ToString());
                myArrList.Add(dr["GuestAddress"].ToString());
                myArrList.Add(dr["GuestCity"].ToString());
                myArrList.Add(dr["Guestregion"].ToString());
                myArrList.Add(dr["GuestPostalCode"].ToString());
                myArrList.Add(dr["GuestCountry"].ToString());
                myArrList.Add(dr["GuestPhone"].ToString());
                myArrList.Add(dr["GuestEmail"].ToString());
                myArrList.Add(dr["GuestIDNumber"].ToString());
                myArrList.Add(dr["GuestIDIssueCountry"].ToString());
                myArrList.Add(dr["GuestComments"].ToString());
            }
            return myArrList;

        }

        /// <summary>
        /// updates the guests information
        /// </summary>
        /// <param name="currentGuest"></param>
        /// <returns>whether the insert has succeeded or not</returns>
        public static int updateGuestFolio(Guest currentGuest)
        {

            SqlConnection conn = new SqlConnection(getConnectionString());

            try
            {
                conn.Open(); //Open the connection

                string update = "UPDATE [Guest] SET [GuestSalutation] = @GuestSalutation, [GuestFirstName] = @GuestFirstName, " +
                    "[GuestSurName] = @GuestSurName, [GuestAddress] = @GuestAddress, [GuestCity] = @GuestCity, [GuestRegion] = @GuestRegion, " +
                    "[GuestPostalCode] = @GuestPostalCode, [GuestCountry] = @GuestCountry, [GuestPhone] = @GuestPhone, [GuestEmail] = @GuestEmail, " +
                    "[GuestComments] = @GuestComments, [GuestIDNumber] = @GuestIDNumber, [GuestIDIssueCountry] = @GuestIDIssueCountry " +
                    "WHERE [GuestID] = @GuestID";
                SqlCommand connCommand = new SqlCommand(update, conn);
                connCommand.Parameters.AddWithValue("@GuestSalutation", currentGuest._salutation);
                connCommand.Parameters.AddWithValue("@GuestFirstName", currentGuest._firstName);
                connCommand.Parameters.AddWithValue("@GuestSurName", currentGuest._surname);
                connCommand.Parameters.AddWithValue("@GuestAddress", currentGuest._address);
                connCommand.Parameters.AddWithValue("@GuestCity", currentGuest._city);
                connCommand.Parameters.AddWithValue("@GuestRegion", currentGuest._state);
                connCommand.Parameters.AddWithValue("@GuestPostalCode", currentGuest._postalCode);
                connCommand.Parameters.AddWithValue("@GuestCountry", currentGuest._country);
                connCommand.Parameters.AddWithValue("@GuestPhone", currentGuest._phoneNumber);
                connCommand.Parameters.AddWithValue("@GuestEmail", currentGuest._emailAddress);
                connCommand.Parameters.AddWithValue("@GuestID", currentGuest._ID);
                connCommand.Parameters.AddWithValue("@GuestIDIssueCountry", currentGuest._issuecountry);
                connCommand.Parameters.AddWithValue("@GuestIDNumber", currentGuest._guestidnumber);
                connCommand.Parameters.AddWithValue("@GuestComments", currentGuest._guestcomments);
                return connCommand.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                throw e;
            }

        }

        public static int addDiscount(int discountID, int reservationID)
        {
            SqlConnection conn = new SqlConnection(getConnectionString());

            try
            {
                conn.Open(); //Open the connection
                string update = "UPDATE [ReservationDetail] SET [DiscountID] = @discountID " +
                      "WHERE [ReservationDetailID] = @reservationID";
                SqlCommand connCommand = new SqlCommand(update, conn);
                connCommand.Parameters.AddWithValue("@discountID", discountID);
                connCommand.Parameters.AddWithValue("@reservationID", reservationID);
                return connCommand.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public static int updateReservationStatus(char reservationStatus, int reservationID)
        {
            SqlConnection conn = new SqlConnection(getConnectionString());

            try
            {
                conn.Open(); //Open the connection
                string update = "UPDATE [Reservation] SET [ReservationStatus] = @reservationStatus " +
                      "WHERE [ReservationID] = @reservationID";
                SqlCommand connCommand = new SqlCommand(update, conn);
                connCommand.Parameters.AddWithValue("@reservationStatus", reservationStatus);
                connCommand.Parameters.AddWithValue("@reservationID", reservationID);
                return connCommand.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public static int updateReservationDetail(char status, int reservationID)
        {
            SqlConnection conn = new SqlConnection(getConnectionString());

            try
            {
                conn.Open(); //Open the connection
                string update = "UPDATE [ReservationDetail] SET [Status] = @status " +
                      "WHERE [ReservationDetailID] = @reservationID";
                SqlCommand connCommand = new SqlCommand(update, conn);
                connCommand.Parameters.AddWithValue("@status", status);
                connCommand.Parameters.AddWithValue("@reservationID", reservationID);
                return connCommand.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                throw e;
            }

        }

        public static int updateRoomStatus(char roomStatus, int roomID)
        {
            SqlConnection conn = new SqlConnection(getConnectionString());

            try
            {
                conn.Open(); //Open the connection
                string update = "UPDATE [Room] SET [RoomStatus] = @roomStatus " +
                      "WHERE [RoomID] = @roomID";
                SqlCommand connCommand = new SqlCommand(update, conn);
                connCommand.Parameters.AddWithValue("@roomStatus", roomStatus);
                connCommand.Parameters.AddWithValue("@roomID", roomID);
                return connCommand.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// Gets guest information
        /// </summary>
        /// <param name="reservationID"></param>
        /// <returns></returns>
        public static ArrayList getGuestInformation(int reservationID)
        {
            SqlConnection con = new SqlConnection(getConnectionString());
            string sel = "SELECT ReservationDetail.ReservationDetailID, HotelRoomType.RoomType, ReservationDetail.RoomID, ReservationDetail.NumberOfAdults, ReservationDetail.NumberOfChildren, Guest.GuestFirstName, " +
                         "Guest.GuestSurName, Guest.GuestPhone, ReservationDetail.CheckinDate, ReservationDetail.Nights " +
                         "FROM HotelRoomType INNER JOIN " + 
                         "Room ON HotelRoomType.HotelRoomTypeID = Room.HotelRoomTypeID INNER JOIN " +
                         "ReservationDetail ON Room.RoomID = ReservationDetail.RoomID INNER JOIN " +
                         "Reservation INNER JOIN " +
                         "Guest ON Reservation.GuestID = Guest.GuestID ON ReservationDetail.ReservationID = Reservation.ReservationID WHERE "+
                         "ReservationDetail.ReservationDetailID = " + reservationID;


            SqlCommand cmd = new SqlCommand(sel, con);
            con.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            ArrayList myArrList = new ArrayList();
            while (dr.Read())
            {
                // add the column value to the ArrayList 
                myArrList.Add(dr["ReservationDetailID"].ToString());
                myArrList.Add(dr["RoomType"].ToString());
                myArrList.Add(dr["RoomID"].ToString());
                myArrList.Add(dr["NumberOfAdults"].ToString());
                myArrList.Add(dr["NumberofChildren"].ToString());
                myArrList.Add(dr["GuestFirstName"].ToString());
                myArrList.Add(dr["GuestSurName"].ToString());
                myArrList.Add(dr["GuestPhone"].ToString());
                myArrList.Add(dr["CheckInDate"].ToString());
                myArrList.Add(dr["Nights"].ToString());
            }
            return myArrList;
        }

        public static int countConfirmedReservationDetail(int reservationID)
        {

            SqlConnection con = new SqlConnection(getConnectionString());
            string sel = "SELECT COUNT (*) FROM ReservationDetail Where ReservationID = " + reservationID + " AND Status = 'C'";

            SqlCommand cmd = new SqlCommand(sel, con);
            con.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.HasRows)
            { 
                return 1; 
            }
            else 
                return 0;

        }

        public static int countActiveReservationDetail(int reservationID)
        {

            SqlConnection con = new SqlConnection(getConnectionString());
            string sel = "SELECT COUNT (*) FROM ReservationDetail Where ReservationID = " + reservationID + " AND Status = 'C'";

            SqlCommand cmd = new SqlCommand(sel, con);
            con.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            return Convert.ToInt32(dr);

        }
    }
}