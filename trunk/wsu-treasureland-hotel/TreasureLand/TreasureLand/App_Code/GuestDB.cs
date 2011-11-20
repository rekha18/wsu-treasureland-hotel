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
                "SELECT Reservation.ReservationID, Guest.GuestFirstName, Guest.GuestSurName, ReservationDetail.ReservationDetailID, ReservationDetail.RoomID, Reservation.ReservationStatus FROM Reservation INNER JOIN Guest ON Reservation.GuestID = Guest.GuestID INNER JOIN ReservationDetail ON Reservation.ReservationID = ReservationDetail.ReservationID " +
                "WHERE (Guest.GuestFirstName = '" + FirstName + "' OR Guest.GuestSurName = '" + SurName + "' OR Reservation.ReservationID = '" + ReservationID + "') AND (Reservation.ReservationStatus ='A' OR Reservation.ReservationStatus = 'F')";
            SqlCommand cmd =
            new SqlCommand(sel, con);
            con.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            return dr;
        }

        /// <summary>
        /// Gets the guest that needs to be checked in
        /// </summary>
        /// <param name="FirstName">Guest first name</param>
        /// <param name="SurName">Guest surname</param>
        /// <param name="ReservationID">ReservationID</param>
        /// <returns></returns>
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

        /// <summary>
        /// Locates the guestFolio
        /// </summary>
        /// <param name="FirstName">Guest First Name</param>
        /// <param name="SurName">Guest Surname</param>
        /// <param name="PhoneNumber">Guest PhoneNumber</param>
        /// <returns></returns>
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

        /// <summary>
        /// gets all of the guest services
        /// </summary>
        /// <returns>all guest services</returns>
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

        /// <summary>
        /// gets all discounts
        /// </summary>
        /// <returns>all available discounts</returns>
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
        public static double getTotal(decimal reservationID)
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
               
                return (Convert.ToDouble(myArrList[0]));
            }
            else
                return 0;
        }


        /// <summary>
        /// Updates the guest Information
        /// </summary>
        /// <param name="GuestCompany"></param>
        /// <param name="GuestAddress"></param>
        /// <param name="GuestCity"></param>
        /// <param name="GuestRegion"></param>
        /// <param name="GuestPostalCode"></param>
        /// <param name="GuestCountry"></param>
        /// <param name="GuestFax"></param>
        /// <param name="GuestEmail"></param>
        /// <param name="GuestComments"></param>
        /// <param name="GuestIDNumber"></param>
        /// <param name="GuestIDCountry"></param>
        /// <param name="GuestIDComment"></param>
        /// <param name="GuestID"></param>
        /// <returns>returns if the update succeeded</returns>
        public static int updateGuestInformation(string GuestCompany, string GuestAddress, string GuestCity, String GuestRegion, string GuestPostalCode, string GuestCountry, 
                        string GuestFax, string GuestEmail, string GuestComments, string GuestIDNumber, string GuestIDCountry, string GuestIDComment, int GuestID)
        {
            try
            {
                SqlConnection conn = new SqlConnection(getConnectionString());
                string update =   "UPDATE Guest " + 
                        "SET GuestCompany = @GuestCompany, GuestAddress = @GuestAddress, GuestCity = @GuestCity, GuestRegion = @GuestRegion, GuestPostalCode = @GuestPostalCode, " +
                        "GuestCountry = @GuestCountry, GuestFax = @GuestFax, GuestEmail = @GuestEmail, GuestComments =@GuestComments, GuestIDNumber = @GuestIDNumber, " +
                        "GuestIDIssueCountry = @GuestIDIssueCountry, GuestIDComment = @GuestIDComment WHERE " +
                        "GuestID = @GuestID";
                conn.Open(); //Open the connection
                SqlCommand connCommand = new SqlCommand(update, conn);
                connCommand.Parameters.AddWithValue("@GuestCompany", GuestCompany);
                connCommand.Parameters.AddWithValue("@GuestAddress", GuestAddress);
                connCommand.Parameters.AddWithValue("@GuestCity", GuestCity);
                connCommand.Parameters.AddWithValue("@GuestRegion", GuestRegion);
                connCommand.Parameters.AddWithValue("@GuestPostalCode", GuestPostalCode);
                connCommand.Parameters.AddWithValue("@GuestCountry", GuestCountry);
                connCommand.Parameters.AddWithValue("@GuestFax", GuestFax);
                connCommand.Parameters.AddWithValue("@GuestEmail", GuestEmail);
                connCommand.Parameters.AddWithValue("@GuestComments", GuestComments);
                connCommand.Parameters.AddWithValue("@GuestIDNumber", GuestID);
                connCommand.Parameters.AddWithValue("@GuestIDIssueCountry", GuestIDCountry);
                connCommand.Parameters.AddWithValue("@GuestIDComment", GuestIDComment);
                connCommand.Parameters.AddWithValue("@GuestID", GuestID);
                return connCommand.ExecuteNonQuery(); 
            }
            catch (Exception e)
            {
                
                throw e;
            }            
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="roomID"></param>
        /// <returns></returns>
        public static IEnumerable getGuestRoom(int roomID, int reservationID)
        {
            SqlConnection con = new SqlConnection(getConnectionString());
            string sel = "SELECT Reservation.ReservationID, ReservationDetail.Nights, ReservationDetail.QuotedRate, HotelRoomType.RoomType " +
                         "FROM Reservation INNER JOIN ReservationDetail ON Reservation.ReservationID = ReservationDetail.ReservationID INNER JOIN " +
                         "Room ON ReservationDetail.RoomID = Room.RoomID INNER JOIN HotelRoomType ON Room.HotelRoomTypeID = HotelRoomType.HotelRoomTypeID WHERE Reservation.ReservationID = '" + reservationID + "' AND ReservationDetail.RoomID = '" + roomID + "' AND (ReservationDetail.Status = 'A' OR ReservationDetail.Status = 'F')";
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
            string sel = "SELECT BillingDescription, BillingItemQty, BillingItemDate, BillingAmount, ReservationBillingID, Comments FROM ReservationDetailBilling WHERE ReservationDetailID = " + reservationDetailID;
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

        /// <summary>
        /// adds a discount to the selected reservation
        /// </summary>
        /// <param name="discountID"></param>
        /// <param name="reservationID"></param>
        /// <returns></returns>
        public static int addDiscount(int discountID, int reservationDetailID)
        {
            SqlConnection conn = new SqlConnection(getConnectionString());
            try
            {
                conn.Open(); //Open the connection
                string update = "UPDATE [ReservationDetail] SET [DiscountID] = @discountID " +
                      "WHERE [ReservationDetailID] = @reservationDetailID";
                SqlCommand connCommand = new SqlCommand(update, conn);
                connCommand.Parameters.AddWithValue("@discountID", discountID);
                connCommand.Parameters.AddWithValue("@reservationDetailID", reservationDetailID);
                return connCommand.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// updates a reservation status 
        /// </summary>
        /// <param name="reservationStatus">new reservation status </param>
        /// <param name="reservationID">reservation ID to be updated</param>
        /// <returns></returns>
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

        /// <summary>
        /// updates a reservationDetail status 
        /// </summary>
        /// <param name="reservationStatus">new reservation status </param>
        /// <param name="reservationID">reservation Detail ID to be updated</param>
        /// <returns></returns>
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

        /// <summary>
        /// updates a Room status 
        /// </summary>
        /// <param name="reservationStatus">new room status </param>
        /// <param name="reservationID">room ID to be updated</param>
        /// <returns></returns>
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
            string sel = "SELECT Reservation.ReservationID, HotelRoomType.RoomType, Room.RoomNumbers, ReservationDetail.NumberOfAdults, ReservationDetail.NumberOfChildren, " +
                      "Guest.GuestFirstName, Guest.GuestSurName, Guest.GuestPhone, ReservationDetail.CheckinDate, ReservationDetail.Nights " +
                      "FROM Reservation INNER JOIN " +
                      "ReservationDetail ON Reservation.ReservationID = ReservationDetail.ReservationID INNER JOIN " +
                      "Guest ON Reservation.GuestID = Guest.GuestID INNER JOIN " + 
                      "Room ON ReservationDetail.RoomID = Room.RoomID INNER JOIN " +
                      "HotelRoomType ON Room.HotelRoomTypeID = HotelRoomType.HotelRoomTypeID";


            SqlCommand cmd = new SqlCommand(sel, con);
            con.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            ArrayList myArrList = new ArrayList();
            while (dr.Read())
            {
                // add the column value to the ArrayList 
                myArrList.Add(dr["ReservationID"].ToString());
                myArrList.Add(dr["RoomType"].ToString());
                myArrList.Add(dr["RoomNumbers"].ToString());
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

        /// <summary>
        /// gets the guest ID based on the ReservationID
        /// </summary>
        /// <param name="FirstName"></param>
        /// <param name="SurName"></param>
        /// <param name="ReservationID"></param>
        /// <returns></returns>
        public static int getGuestID(int ReservationID)
        {
            SqlConnection con = new SqlConnection(getConnectionString());
            string sel = "SELECT GuestID " +
                          "FROM Reservation " +
                          "Where ReservationID = @ReservationID";
            
                SqlCommand cmd =
            new SqlCommand(sel, con);
            con.Open();
            cmd.Parameters.AddWithValue("@ReservationID", ReservationID);
            SqlDataReader dr = cmd.ExecuteReader();
            ArrayList myArrList = new ArrayList();
            while (dr.Read())
            {
                // add the column value to the ArrayList 
                myArrList.Add(dr["GuestID"].ToString());
            }
            return Convert.ToInt32(myArrList[0].ToString());
        }

        /// <summary>
        /// check to see if there are any ReservationDetail IDs of the provided ID with the status 'C'
        /// 
        /// </summary>
        /// <param name="reservationID"></param>
        /// <returns>returns 1 if there are rows, 0 if there are no rows</returns>
        public static int countConfirmedReservationDetail(int reservationID)
        {

            SqlConnection con = new SqlConnection(getConnectionString());
            string sel = "SELECT * FROM ReservationDetail Where ReservationID = " + reservationID + " AND Status = 'C'";

            SqlCommand cmd = new SqlCommand(sel, con);
            con.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            if ((dr.HasRows))
            { 
                return 1; 
            }
            else 
                return 0;
        }

        /// <summary>
        /// check to see if there are any ReservationDetail IDs of the provided ID with the status 'A'
        /// 
        /// </summary>
        /// <param name="reservationID"></param>
        /// <returns>returns 1 if there are rows, 0 if there are no rows</returns>
        public static int countActiveReservationDetail(int reservationID)
        {

            SqlConnection con = new SqlConnection(getConnectionString());
            string sel = "SELECT * FROM ReservationDetail Where ReservationID = " + reservationID + " AND Status = 'C'";

            SqlCommand cmd = new SqlCommand(sel, con);
            con.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            if ((dr.HasRows))
            {
                return 1;
            }
            else
                return 0;
           

        }
    }
}