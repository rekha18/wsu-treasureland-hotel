using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TreasureLand.App_Code
{

    /// <summary>
    /// Represents a row in the grid view
    /// </summary>
    public class Row
    {
        /// <summary>
        /// Room the row information belongs to
        /// </summary>
        private short id;

        /// <summary>
        /// Gets the row's room ID
        /// </summary>
        public short ID
        {
            get
            {
                return id;
            }
        }

        /// <summary>
        /// Gets or sets the beginning date of the reservation stay
        /// </summary>
        public DateTime Begin;

        /// <summary>
        /// Gets or sets the ending date of the reservation stay
        /// </summary>
        public DateTime End;

        /// <summary>
        /// Gets or sets the status of the reservation
        /// </summary>
        public char ReservationType;

        /// <summary>
        /// Number of the room
        /// </summary>
        public string RoomNumber;

        /// <summary>
        /// Constructs a Row object
        /// </summary>
        /// <param name="ID">RoomID of the row</param>
        public Row(short ID)
        {
            id = ID;
        }
    }

    /// <summary>
    /// Provides capability of drawing a grid view which outlines a specific
    /// range of dates in certain colors.
    /// </summary>
    public class GridRangeView
    {
        #region Class Level Attributes
        #region Colors
        //The colors have been specifically chosen to allow a large number on the screen
        //without being hard to look at. Bright colors like red, green, and blue can be
        //very stressful to look at when it dominates the screen.

        /// <summary>
        /// Color denoting a reservation that has been checked in
        /// </summary>
        public string CheckedIn = "#00F29D"; //Green-blue

        /// <summary>
        /// Color of a reservation that has been confirmed
        /// </summary>
        public string Confirmed = "#78B200"; //Grassy green

        /// <summary>
        /// Color denoting a reservation that has been made but no yet confirmed
        /// </summary>
        public string Unconfirmed = "#F2D100"; //Yellow-orange

        /// <summary>
        /// Color denoting a reservation that has been cancelled by the holder
        /// or from failure to call back and confirm
        /// </summary>
        public string Canceled = "#CC2B28"; //Crimson
        #endregion
        #region Static Attributes
        /// <summary>
        /// Defines the current date to start displaying dates from
        /// </summary>
        public static DateTime current = DateTime.Now;

        /// <summary>
        /// Defines which room to start displaying
        /// </summary>
        public static int RoomIndex = 1;

        /// <summary>
        /// Defines how many days to display
        /// </summary>
        public static int DaysDisplayed = 7;

        /// <summary>
        /// Denotes the maximum number of rooms displayed per page
        /// </summary>
        public static int PageSize = 15;

        /// <summary>
        /// Defines the total number of rooms in the hotel
        /// </summary>
        private static int MAX_ROOMS;

        /// <summary>
        /// Gets the total number of rooms in the database
        /// </summary>
        public static int MaxRooms
        {
            get
            {
                return MAX_ROOMS;
            }
        }

        /// <summary>
        /// Defines the reservation type char in the database
        /// </summary>
        private enum COLORS { ACTIVE = 'A', UNCONFIRMED = 'U', CONFIRMED = 'C', CANCELED = 'X' };
        #endregion

        /// <summary>
        /// A list containing all of the row data that is returned from the database
        /// </summary>
        private List<Row> rows;
        #endregion

        /// <summary>
        /// Constructs a GridRangeView object
        /// </summary>
        public GridRangeView()
        {
            current = current.Date; //The time portion will mess up calculations
        }

        /// <summary>
        /// Retrieves information from the database that will be used when rendering
        /// the GridRangeView object
        /// </summary>
        public void update()
        {
            MAX_ROOMS = RoomDB.countRooms();
            rows = RoomDB.getReservations(current, current.AddDays(DaysDisplayed - 1));
        }

        /// <summary>
        /// Creates an HTML table that can be placed as the text value of a label
        /// Update must be called before this statement in order to run correctly
        /// 
        /// This function generates two javascript methods into each table cell.
        /// select(i) and deselect(i) must be present in the base HTML file
        /// </summary>
        public string generateTableHTML()
        {
            string tableRows = String.Empty; //Represents the row portion of the table
            string[, ,] StringColData = new string[MAX_ROOMS, DaysDisplayed, 2]; //row, col, layer info (color, info)

            fillStringColData(StringColData);

            foreach (Row r in rows)
                fillColData(r, StringColData);

            //Now that all the necessary information that needs to be displayed is ready, the table
            //can start being crafted

            //Generate the row headers
            string table = "<table>" + generateRowHeaders();

            //Generate the data rows
            for (int rowIndex = RoomIndex-1; rowIndex < RoomIndex + PageSize && rowIndex < MAX_ROOMS; rowIndex++)
            {
                //Create the left-most cell with the room number
                table += "<tr>"; //Open a row
                table += "<td id='row" + rowIndex + "' style='background: #AAAAAA' onmouseover='select(" + rowIndex + ")' onmouseout='deselect(" + rowIndex + ")'>" +
                    (rowIndex+1) + "</td>";
                //Create the row data
                for (int j = 0; j < DaysDisplayed; j++)
                    table += "<td id='room" + rowIndex + "col" + j + "' style='background:" + StringColData[rowIndex, j, 0] + "'" +
                        "onmouseover='select(" + rowIndex + ")' onmouseout='deselect(" + rowIndex + ")'" + ">" + StringColData[rowIndex, j, 1] + "</td>";
                table += "</tr>"; //Close the row
            }

            return table + "</table>"; //Return the table
        }

        /// <summary>
        /// Loads the row data dynamically into a ragged string array
        /// </summary>
        /// <param name="r">Row to be inserted</param>
        /// <param name="arr">String array to insert values into</param>
        private void fillColData(Row r, string[, ,] arr)
        {
            //The cells to insert will calculated in terms of the array bounds.
            //If negative or exceeding DaysDisplayed-1, then the information
            //is not added.
            int startIndex = (r.Begin - current).Days; //If the same, then it will start in column 1 (arr[0])
            int endIndex = (r.End - current).Days;

            string color = getColor(r.ReservationType);
            for (int i = startIndex; i < endIndex; i++)
            {
                //If the index is valid
                if (i >= 0 && i < DaysDisplayed)
                {
                    arr[Int32.Parse(r.RoomNumber) - 1, i, 0] = color;
                    arr[Int32.Parse(r.RoomNumber) - 1, i, 1] = "RN #" + r.RoomNumber;
                }
            }
        }

        /// <summary>
        /// Returns the HTML hex color value
        /// </summary>
        /// <param name="type">Reservation status</param>
        /// <returns>The corresponding HTML string in hexadecimal</returns>
        private string getColor(char type)
        {
            switch (type)
            {
                case (char)COLORS.ACTIVE: return CheckedIn;
                case (char)COLORS.CONFIRMED: return Confirmed;
                case (char)COLORS.UNCONFIRMED: return Unconfirmed;
                case (char)COLORS.CANCELED: return Canceled;
            }
            return "#FFFFFF"; //White as default
        }

        /// <summary>
        /// Fills the string matrix with the default empty data
        /// </summary>
        /// <param name="arr">String matrix to fill with data</param>
        private void fillStringColData(string[, ,] arr)
        {
            for (int i = 0; i < arr.GetLength(0); i++)
                for (int j = 0; j < arr.GetLength(1); j++)
                {
                    //Creates the alternating row color
                    if (i % 2 == 0)
                        arr[i, j, 0] = "#CCCCCC";
                    else
                        arr[i, j, 0] = "#FFFFFF";

                    arr[i, j, 1] = "-";
                }
        }

        /// <summary>
        /// Generates the row headers with the appropriate date and format
        /// </summary>
        /// <returns>HTML headers as defined above</returns>
        private string generateRowHeaders()
        {
            DateTime temp = current; //struct, so changes are not copied over

            string RT = "<tr><th>Room<br />#</th>";
            for (int i = 0; i < DaysDisplayed; i++)
            {
                RT += "<th style='background: #AAAAAA'>" + temp.ToString("dddd") + "<br />";
                RT += temp.ToString("dd/MM/yyyy");
                RT += "</th>";
                temp = temp.AddDays(1.0); //Go to the next day
            }
            return RT + "</tr>"; //Return the header row
        }
    }
}