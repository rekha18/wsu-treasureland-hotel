using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections;

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
        /// ReservationID of the row
        /// </summary>
        private int ResID;

        /// <summary>
        /// Defines the unique HotelRoomTypeID as in the database
        /// </summary>
        private int hotelRoomTypeID;

        private int reservationDetailID;

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
        /// Gets the ReservationID of the row
        /// </summary>
        public int ReservationID
        {
            get
            {
                return ResID;
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
        /// Gets or sets the RoomType of the row
        /// </summary>
        public string RoomType;

        /// <summary>
        /// Gets the HotelRoomTypeID of the row
        /// </summary>
        public int HotelRoomTypeID
        {
            get
            {
                return hotelRoomTypeID;
            }
        }

        /// <summary>
        /// Gets the reservation detail ID
        /// </summary>
        public int ReservationDetailID
        {
            get
            {
                return reservationDetailID;
            }
        }

        /// <summary>
        /// Constructs a Row object
        /// </summary>
        /// <param name="ID">RoomID of the row</param>
        /// <param name="ReservationID">ReservationID of the row</param>
        /// <param name="HotelRoomTypeID">Room type of the row</param>
        /// <param name="ReservationDetailID">ReservationDetailID of the row</param>
        public Row(short ID, int ReservationID, int HotelRoomTypeID, int ReservationDetailID)
        {
            id = ID;
            ResID = ReservationID;
            reservationDetailID = ReservationDetailID;
            hotelRoomTypeID = HotelRoomTypeID;
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
        public string Canceled = "#FF5526"; //
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

        /// <summary>
        /// Color that the row is highlighted if the room type value is the same as the row's room number
        /// </summary>
        public static string highlightColor = "#FFF18E";
        #endregion

        /// <summary>
        /// A list containing all of the row data that is returned from the database
        /// </summary>
        private List<Row> rows;

        /// <summary>
        /// A Linked list of room names to help the hast table
        /// </summary>
        private List<string[]> roomNames;
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
            roomNames = RoomDB.getRoomNumbers();
        }

        /// <summary>
        /// Generates an HTML range view of the specified dates.
        /// Highlights the rows of the 
        /// </summary>
        /// <param name="centerTable"></param>
        /// <param name="roomType"></param>
        /// <returns></returns>
        public string generateHTMLTablev3(bool centerTable, string roomType)
        {
            Hashtable roomData = new Hashtable();

            #region Hashtable fill
            foreach (string[] s in roomNames)
            {
                string[,] room = new string[DaysDisplayed + 1, 3];
                room[DaysDisplayed,0] = s[1];
                roomData.Add(s[0], room); //Color for each day, data to display
            }

            foreach (Row r in rows)
            {
                //For simplicity, I don't want duplicate string array data under
                //the same key. In this case, remove the key, concatenate the
                //string data, and insert the key and value back in.
                string[,] oldData = (string[,])roomData[r.RoomNumber];
                roomData.Remove(r.RoomNumber);

                fillCell(r, oldData);
                roomData.Add(r.RoomNumber, oldData);
            }
            #endregion

            string table = "<table" +
                (centerTable ? " style='margin-left:auto;margin-right:auto;'>" : ">") + generateRowHeaders();

            bool rowEven = false;
            int rowCount = 1;
            foreach (string[] room in roomNames) //Assuming the rooms were sorted in order
            {
                #region Paging Logic
                if (rowCount < RoomIndex)
                {
                    rowCount++;
                    continue;
                }
                if (rowCount > RoomIndex + PageSize || rowCount > MAX_ROOMS)
                    break;
                #endregion

                string key = room[0];
                string[,] row = (string[,])roomData[key];
                //Create the left-most cell with the room number
                table += "<tr>"; //Open a row
                table += "<td id='row" + key + "' style='background-color: "+ (roomType == row[DaysDisplayed, 0] ? highlightColor : "#AAAAAA") + 
                    ";' onmouseover='select(\"" + key + "\")' onmouseout='deselect(\"" + key + "\")'>" + key + "</td>";

                string backColor = roomType == row[DaysDisplayed,0] ? highlightColor : (rowEven ? "#CCCCCC" : "#FFFFFF");

                //Create the row data
                for(int i = 0; i < DaysDisplayed; i++)
                {
                    if(row[i, 0] == null && row[i, 1] == null && row[i, 2] == null) //No data for this cell
                    {
                        table += "<td id='row" + key + "col" + i + "a' style='background-color:" + backColor + 
                            ";' onmouseover='select(\"" + key + "\")' onmouseout='deselect(\"" + key + "\")' colspan='2'>-</td>";
                    }
                    else if (row[i, 1] != null) //Full day of a guest
                    {
                        table += row[i, 1];
                    }
                    else //Cell contains a customer that checks out and/or checks in
                    {
                        table += "<td id='row" + key + "col" + i + "a' width='40px' style='background-color:" + (row[i, 0] == null ? backColor : row[i, 0]) +
                            ";' onmouseover='select(\"" + key + "\")' onmouseout='deselect(\"" + key + "\")'>&nbsp;</td>";
                        table += "<td id='row" + key + "col" + i + "b' width='40px' style='background-color:" + (row[i, 2] == null ? backColor : row[i, 2]) +
                            ";' onmouseover='select(\"" + key + "\")' onmouseout='deselect(\"" + key + "\")'>&nbsp;</td>";
                    }
                }
                table += "</tr>"; //Close the row

                rowEven = !rowEven;
                rowCount++;
            }

            return table + "</table>";
        }

        /// <summary>
        /// Fills a row of table cells with the correct color and divides the cell in half
        /// if needed
        /// </summary>
        /// <param name="r">Row to analyze</param>
        /// <param name="data">Ragged 2D string array to return color data in</param>
        public void fillCell(Row r, string[,] data)
        {
            //The cells to insert will calculated in terms of the array bounds.
            //If negative or exceeding DaysDisplayed-1, then the information
            //is not added.
            int startIndex = (r.Begin - current).Days; //If the same, then it will start in column 1 (arr[0])
            int endIndex = (r.End - current).Days;

            string color = getColor(r.ReservationType);
            if (endIndex >= 0 && endIndex < DaysDisplayed)//Add the first td tag
                data[endIndex, 0] = color;
            if (startIndex >= 0 && startIndex < DaysDisplayed)//Add the second td tag
                data[startIndex, 2] = color;

            //data[DaysDisplayed, 0] = r.RoomType;
            for (int i = startIndex; i < endIndex; i++)
            {
                //If the index is valid
                if (i >= 0 && i < DaysDisplayed)
                {
                    if(data[i, 2] == null) //Don't overwrite the start date when it comes time to generate the table
                        data[i, 1] = "<td id='row" + r.RoomNumber + "col" + i + "a' colspan='2' style='background-color:" + color +
                                ";' onmouseover='select(\"" + r.RoomNumber + "\")' onmouseout='deselect(\"" + r.RoomNumber + "\")'>RS #" + r.ReservationDetailID + "</td>";
                }
            }
        }

        /// <summary>
        /// Tests a specific ReservationID against all records except itself
        /// to test against existing date ranges to make sure a record will
        /// not overlap a currently existing one
        /// </summary>
        /// <param name="ReservationID">ID of the reservation</param>
        /// <param name="RoomNumber">Intended/New RoomNumber of the reservation</param>
        /// <returns>Null if no collision. Returns error messages</returns>
        public string testDateRangeCollision(int ReservationID, string RoomNumber)
        {
            //Find the row
            Row test = null;
            update();
            foreach (Row r in rows)
                if (r.ReservationDetailID == ReservationID)
                {
                    test = r;
                    break;
                }

            if (test == null) //ReservationID did not match any existing record
                return "No existing records for Reservation # " + ReservationID; 

            bool roomFound = false;
            foreach (string[] s in roomNames)
                if (s[0] == RoomNumber)
                {
                    roomFound = true;
                    break;
                }

            if (!roomFound) //Room does not exist
                return "Specified room number does not exist";

            //Test the row against every other row
            foreach (Row r in rows)
            {
                if (((test.Begin >= r.Begin && test.Begin < r.End) ||
                    (test.End > r.Begin && test.End <= r.End)) &&
                    RoomNumber == r.RoomNumber)
                {
                    return "The selected room already has a guest for this time"; //A collision was detected
                }
            }

            return null;
        }

        /// <summary>
        /// Tests a specific ReservationID against all records except itself
        /// to test against existing date ranges to make sure a record will
        /// not overlap a currently existing one
        /// </summary>
        /// <param name="begin">Beginning date of the reservation</param>
        /// <param name="nightsStayed">Number of nights the guest is planning on staying</param>
        /// <param name="RoomNumber">Intended/New RoomNumber of the reservation</param>
        /// <returns>Null if no collision. Returns error messages</returns>
        public string testDateRangeCollision(string sBegin, int nightsStayed, string RoomNumber)
        {
            update();
            
            DateTime begin = DateTime.Parse(sBegin); //Assuming sBegin is in the proper format
            DateTime end = begin.AddDays(nightsStayed);

            bool roomFound = false;
            foreach (string[] s in roomNames)
                if (s[0] == RoomNumber)
                {
                    roomFound = true;
                    break;
                }

            if (!roomFound) //Room does not exist
                return "Specified room number does not exist";

            //Test the row against every other row
            foreach (Row r in rows)
            {
                if (((begin >= r.Begin && begin < r.End) ||
                    (end > r.Begin && end <= r.End)) &&
                    RoomNumber == r.RoomNumber)
                {
                    return "The selected room already has a guest for this time"; //A collision was detected
                }
            }

            return null;
        }

        #region Old V2
        /*// <summary>
        /// Generates an HTML table containing the data values returned
        /// from the update() command. This updated version differs from
        /// the last in the sense that it now stores the RoomNumber attribute
        /// in a HashMap so that rooms like "Conference 1" or "11A" can be
        /// tracked properly.
        /// </summary>
        /// <param name="centerTable">Whether or not the table will be centered on the page</param>
        /// <returns>A string containing HTML markup</returns>
        public string generateTableHTMLv2(bool centerTable)
        {
            Hashtable roomData = new Hashtable();

            #region Hashtable fill
            foreach (string s in roomNames)
                roomData.Add(s, new string[DaysDisplayed,2]); //Color for each day, data to display


            foreach (Row r in rows)
            {
                //For simplicity, I don't want duplicate string array data under
                //the same key. In this case, remove the key, concatenate the
                //string data, and insert the key and value back in.
                string[,] oldData = (string[,])roomData[r.RoomNumber];
                roomData.Remove(r.RoomNumber);

                fillColors(r, oldData);
                roomData.Add(r.RoomNumber, oldData);
            }
            #endregion

            #region Table Generation
            //Generate the row headers
            string table = "<table" +
                (centerTable ? " style='margin-left:auto;margin-right:auto;'>" : ">") + generateRowHeaders();

            //Generate table rows
            bool rowEven = false;
            int rowCount = 1;
            foreach (string key in roomNames) //Assuming the rooms were sorted in order
            {
                #region Paging Logic
                if (rowCount < RoomIndex)
                {
                    rowCount++;
                    continue;
                }
                if (rowCount > RoomIndex + PageSize || rowCount >= MAX_ROOMS)
                    break;
                #endregion

                //Create the left-most cell with the room number
                table += "<tr>"; //Open a row
                table += "<td id='row" + key + "' style='background: #AAAAAA' onmouseover='select(\"" + key + "\")' onmouseout='deselect(\"" + key + "\")'>" +
                    key + "</td>";
                //Create the row data
                string[,] rowData = (string[,])roomData[key];
                for (int j = 0; j < DaysDisplayed; j++)
                    table += generateRow(rowData, key, j, rowEven);
                table += "</tr>"; //Close the row

                rowEven = !rowEven;
                rowCount++;
            }
            #endregion

            return table + "</table>";
        }*/
        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <param name="rowData"></param>
        /// <param name="key"></param>
        /// <param name="j"></param>
        /// <param name="rowEven"></param>
        /// <returns></returns>
        private string generateRow(string[,] rowData, string key, int j, bool rowEven)
        {
            string RT = "<td id='room" + key + "col" + j + "'>" + 
                "<table><tr>" +
                (rowData[j, 1] == null ? (rowData[j, 0] == null ? (rowEven ? "<td style='background-color:#CCCCCC'>-</td>" : "<td style='background-color:#FFFFFF'>-</td>") : rowData[j, 0]) : rowData[j, 0] + rowData[j, 1]) +
                "</tr></table>";
            return RT;
        }

        /// <summary>
        /// Fills a single string array with a CSS hex color value
        /// </summary>
        /// <param name="r">Reservation</param>
        /// <param name="colorData">string to fill with color data</param>
        private void fillColors(Row r, string[,] colorData)
        {
            //The cells to insert will calculated in terms of the array bounds.
            //If negative or exceeding DaysDisplayed-1, then the information
            //is not added.
            int startIndex = (r.Begin - current).Days; //If the same, then it will start in column 1 (arr[0])
            int endIndex = (r.End - current).Days;

            string color = getColor(r.ReservationType);
            if (endIndex >= 0 || endIndex < DaysDisplayed)//Add the first td tag
                colorData[endIndex, 0] = "<td width='50%' style='background-color:" + color + ";' onmouseover='select(\"" + r.RoomNumber + "\")' onmouseout='deselect(\"" + r.RoomNumber + "\")'" + "></td>";
            else if (startIndex >= 0 || startIndex < DaysDisplayed)//Add the second td tag
                colorData[startIndex, 1] = "<td width='50%' style='background-color:" + color + ";' onmouseover='select(\"" + r.RoomNumber + "\")' onmouseout='deselect(\"" + r.RoomNumber + "\")'" + "></td>";

            for (int i = startIndex+1; i < endIndex; i++)
            {
                //If the index is valid
                if (i >= 0 && i < DaysDisplayed)
                {
                    colorData[i, 0] = "<td style='background-color:" + color + ";'>RS #" + r.ReservationID + "</td>";
                }
            }
        }

        /// <summary>
        /// Creates an HTML table that can be placed as the text value of a label
        /// Update must be called before this statement in order to run correctly
        /// 
        /// This function generates two javascript methods into each table cell.
        /// select(i) and deselect(i) must be present in the base HTML file
        /// </summary>
        /// <param name="centerTable">Whether or not the table will include CSS to center it</param>
        /// <returns>An HTML string containing definitions for a table</returns>
        public string generateTableHTML(bool centerTable)
        {
            string tableRows = String.Empty; //Represents the row portion of the table
            string[, ,] StringColData = new string[MAX_ROOMS, DaysDisplayed, 2]; //row, col, layer info (color, info)

            fillStringColData(StringColData);

            foreach (Row r in rows)
            {
                int foo;
                if(Int32.TryParse(r.RoomNumber, out foo))
                    fillColData(r, StringColData);
            }

            //Now that all the necessary information that needs to be displayed is ready, the table
            //can start being crafted

            //Generate the row headers
            string table = "<table" + 
                (centerTable ? " style='margin-left:auto;margin-right:auto;'>" : ">") + generateRowHeaders();

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
                RT += "<th style='background: #AAAAAA' width='80px' colspan='2'>" + temp.ToString("dddd") + "<br />";
                RT += temp.ToString("dd/MM/yyyy");
                RT += "</th>";
                temp = temp.AddDays(1.0); //Go to the next day
            }
            return RT + "</tr>"; //Return the header row
        }
    }
}