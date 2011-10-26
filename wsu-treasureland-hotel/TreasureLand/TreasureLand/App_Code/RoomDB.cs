using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Configuration;

namespace TreasureLand.App_Code
{
    /// <summary>
    /// Summary description for RoomDB
    /// </summary>
    public partial class RoomDB
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
        public static int countRooms()
        {
            SqlConnection conn = new SqlConnection(getConnectionString());

            try
            {
                conn.Open(); //Open the connection

                string command = "SELECT COUNT(RoomID) FROM Room";

                SqlCommand connCommand = new SqlCommand(command, conn);

                SqlDataReader rt = connCommand.ExecuteReader();
                rt.Read();
                int val = Int32.Parse(rt[0].ToString()); //Only one item will be returned
                rt.Close();

                return val;
            }
            catch (Exception e)
            {
            }
            return 0;
        }

        /// <summary>
        /// Retrieves all reservations in the database and packages
        /// </summary>
        /// <param name="beginRange">Ending dates before this time will be skipped</param>
        /// <returns>Beginning dates after this time will be skipped</returns>
        public static List<Row> getReservations(DateTime beginRange, DateTime endRange)
        {
            SqlConnection conn = new SqlConnection(getConnectionString());

            try
            {
                conn.Open(); //Open the connection

                string command = "SELECT res.ReservationDate, rd.Status, rd.Nights, " +
                                 "room.RoomID, room.RoomNumbers FROM Reservation res INNER JOIN ReservationDetail rd " +
                                 "ON res.ReservationID = rd.ReservationID " +
                                 "INNER JOIN Room room ON rd.RoomID = room.RoomID " +
                                 "ORDER BY room.RoomID";

                SqlCommand connCommand = new SqlCommand(command, conn);

                SqlDataReader rt = connCommand.ExecuteReader();

                List<Row> RT = new List<Row>();

                while (rt.Read())
                {
                    string dateTemp = rt[0].ToString();
                    //string debug = dateTemp.Substring(0, dateTemp.IndexOf(' '));
                    DateTime begin = DateTime.Parse(dateTemp.Substring(0,dateTemp.IndexOf(' ')));
                    DateTime end = begin.AddDays(Double.Parse(rt[2].ToString()));
                    if ((end - beginRange).Days < 0 || (begin - endRange).Days > 0)
                        continue; //Skip the rest and go to the next record

                    char resStatus = Char.Parse(rt[1].ToString());
                    short id = Int16.Parse(rt[3].ToString());
                    string roomNumber = rt[4].ToString();

                    Row temp = new Row(id); //Create and fill a Row object
                    temp.Begin = begin;
                    temp.End = end;
                    temp.ReservationType = resStatus;
                    temp.RoomNumber = roomNumber;

                    RT.Add(temp); //Add it to the list
                }

                return RT;
            }
            catch (Exception e)
            {
            }

            return null;
        }
    }
}