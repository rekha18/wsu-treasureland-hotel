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
        public static IEnumerable LocateGuestRoom(string FirstName, string SurName, string ReservationID, int RoomID)
        {
            SqlConnection con = new SqlConnection(getConnectionString());
            string sel =
                "SELECT Reservation.ReservationID, Guest.GuestFirstName, Guest.GuestSurName, ReservationDetail.ReservationDetailID, ReservationDetail.RoomID FROM Reservation INNER JOIN Guest ON Reservation.GuestID = Guest.GuestID INNER JOIN ReservationDetail ON Reservation.ReservationID = ReservationDetail.ReservationID " +
                "WHERE Guest.GuestFirstName = '" + FirstName + "' OR Guest.GuestSurName = '" + SurName +"' OR Reservation.ReservationID = '" + ReservationID + "' OR ReservationDetail.RoomID = '" + RoomID + "'";
            SqlCommand cmd =
            new SqlCommand(sel, con);
            con.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            return dr;
        }

        /// <summary>
        /// Searches for a current guest based on the entered information.
        /// </summary>
        /// <param name="FirstName">First Name of guest</param>
        /// <param name="SurName">SurName of guest</param>
        /// <param name="ReservationID">Reservation ID numbmer</param>
        /// <param name="RoomNumber">Room Number</param>
        /// <returns></returns>
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
            return Convert.ToDouble(myArrList[0]);
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
                        "(BillingAmount, BillingItemQty, BillingCategoryID,  BillingDescription, ReservationDetailID, BillingItemDate) "+
                        "VALUES(" + billAmount +"," + billQty + ",1,'" + billingDescription + "'," + reservationID + ", '" + System.DateTime.Now+"')";
            SqlCommand cmd =
            new SqlCommand(update, con);
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
            string sel = "SELECT BillingDescription, BillingItemQty, BillingAmount FROM ReservationDetailBilling WHERE ReservationDetailID = " + reservationDetailID;
            SqlCommand cmd = new SqlCommand(sel, con);
            con.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            return dr;
        }


    }
}