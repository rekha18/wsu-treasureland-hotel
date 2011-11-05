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
                "WHERE (Guest.GuestFirstName = '" + FirstName + "' OR Guest.GuestSurName = '" + SurName +"' OR Reservation.ReservationID = '" + ReservationID + "') AND ReservationDetail.Status ='A'";
            SqlCommand cmd =
            new SqlCommand(sel, con);
            con.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            return dr;
        }

        public static IEnumerable LocateGuestCheckIn(string FirstName, string SurName, string ReservationID, string Email)
        {
            SqlConnection con = new SqlConnection(getConnectionString());
            string sel =
                "SELECT Reservation.ReservationID, Guest.GuestFirstName, Guest.GuestSurName, Guest.GuestEmail, ReservationDetail.ReservationDetailID, ReservationDetail.RoomID FROM Reservation INNER JOIN Guest ON Reservation.GuestID = Guest.GuestID INNER JOIN ReservationDetail ON Reservation.ReservationID = ReservationDetail.ReservationID " +
                "WHERE Guest.GuestFirstName = '" + FirstName + "' OR Guest.GuestSurName = '" + SurName + "' OR Reservation.ReservationID = '" + ReservationID + "' OR Guest.GuestEmail = '" + Email + "'";
            SqlCommand cmd =
            new SqlCommand(sel, con);
            con.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            return dr;
        }

        //public static IEnumerable LocateGuestFolio(string Salutation, string FirstName, string Surname, string Phone, string CreditCardNum, string Expiration, string Address, string City, string State, string Country, string PostalCode, string Email)
        public static IEnumerable LocateGuestFolio( string FirstName, string SurName, string Email)
        {
            SqlConnection con = new SqlConnection(getConnectionString());
            string sel =
                "SELECT Guest.GuestFirstName, Guest.GuestSurName, Guest.GuestEmail" +
                //"WHERE GuestSalutation = '" + Salutation + "' OR GuestFirstName = '" + FirstName + "' OR GuestSurName = '" + Surname + "' OR PhoneNumber = '" + Phone + "' OR CreditCardNum = '" + CreditCardNum + "' OR Expiration = '" + Expiration + "' OR Address = '" + Address + "' OR City = '" + City + "' OR State = '" + State + "' OR Country = '" + Country + "' OR PostalCode = '" + PostalCode + "' OR GuestEmail = '" + Email + "'";
                "WHERE Guest.GuestFirstName = '" + FirstName + "' OR Guest.GuestSurName = '" + SurName + "' OR Guest.GuestEmail = '" + Email + "'";
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
                        "(BillingAmount, BillingItemQty, BillingCategoryID,  BillingDescription, ReservationDetailID, BillingItemDate) "+
                        "VALUES(" + billAmount +"," + billQty + ",1,'" + billingDescription + "'," + reservationID + ", '" + System.DateTime.Now+"')";
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

                string update = "UPDATE [ReservationDetailBilling] SET [BillingItemQty] = @Qty, [BillingAmount] = @Cost "  +
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
            }
            return 0;
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
    }
}