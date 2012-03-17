using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TreasureLand.App_Code;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;

namespace TreasureLand.Clerk
{
    /// <summary>
    /// Defines room information when passing between pages
    /// </summary>
    internal class RoomInfo
    {
        public int RoomID;
        public string RoomNumbers;

        public RoomInfo(int RoomID, string RoomNumbers)
        {
            this.RoomID = RoomID;
            this.RoomNumbers = RoomNumbers;
        }
    }
    
    public partial class SelectRoom : System.Web.UI.Page
    {           
        private class RowInfo
        {
            public string RoomType;
            public int RowIndex;
            public int PageIndex;
            public string RoomNumber;
            public string RoomTypeString;
            public CheckBox cb;
            private static ushort counter = 0;

            public RowInfo(string RoomType, int RowIndex, int PageIndex,
                string RoomNumber, string RoomTypeString)
            {
                this.RoomType = RoomType;
                this.RowIndex = RowIndex;
                this.PageIndex = PageIndex;
                this.RoomNumber = RoomNumber;
                this.RoomTypeString = RoomTypeString;

                cb = new CheckBox();
                cb.Checked = true;
                cb.EnableViewState = true;
                cb.ID = "DynamicCheckBox" + counter++.ToString();
            }
        }
        
        /// <summary>
        /// Runs when the page loads. The main purpose is to read information
        /// from the session array and set the literals to display the
        /// information
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            //On a fresh load, remove any previous RowInfo
            if (!IsPostBack)
                if (Session["RowInfo"] != null)
                    Session.Remove("RowInfo");
            
            if (Session["RowInfo"] == null)
                Session.Add("RowInfo", new Dictionary<int, RowInfo>());
            if (Session["LastRoomType"] == null)
                Session.Add("LastRoomType", ddlRoomTypes.SelectedValue);
                

            #region Debug Variables
            /*if (Session["StartDate"] == null)
                Session.Add("StartDate", DateTime.Now);
            if (Session["Nights"] == null)
                Session.Add("Nights", 3);*/
            #endregion

            #region SQL Variables for SQL Data Source
            //Load the values from the reservation object in session
            Reserve res = (Reserve)Session["Room"];
            if (Session["StartDate"] == null)
                Session.Add("StartDate", Convert.ToDateTime(res.reserveDate));
            else
                Session["StartDate"] = Convert.ToDateTime(res.reserveDate);

            if (Session["Nights"] == null)
                Session.Add("Nights", res.daysStaying);
            else
                Session["Nights"] = res.daysStaying;
            #endregion

            lblStartDate.Text = Session["StartDate"].ToString();
            lNights.Text = Session["Nights"].ToString();
        }

        /// <summary>
        /// Updates the page for information dependant on the total selected rooms
        /// </summary>
        /// <param name="roomsSelected">Total rooms that have been selected</param>
        private void updatePageInfo(int roomsSelected)
        {
            if (roomsSelected > 0)
            {
                lblTotalRooms.Text = roomsSelected.ToString();
                btnSelect.Enabled = true;
            }
            else
            {
                lblTotalRooms.Text = "0";
                btnSelect.Enabled = false;
            }
        }

        /// <summary>
        /// Re-enables any selected rooms after the room type was changed
        /// </summary>
        private void checkSelectedRooms()
        {
            Dictionary<int, RowInfo> rowInfo = (Dictionary<int, RowInfo>)Session["RowInfo"];

            foreach (KeyValuePair<int, RowInfo> kvp in rowInfo)
            {
                RowInfo ri = kvp.Value;

                //The current room type is also this room's type
                if(ri.RoomType == ddlRoomTypes.SelectedValue
                    && ri.PageIndex == gvOpenRooms.PageIndex)
                {
                    CheckBox cb = (CheckBox)gvOpenRooms.Rows[ri.RowIndex].FindControl("cbSelected");
                    cb.Checked = true;
                    foreach(TableCell c in gvOpenRooms.Rows[ri.RowIndex].Cells)
                        c.BackColor = System.Drawing.Color.Yellow;
                }
            }
        }

        /// <summary>
        /// Dynamically renders a list of checked rooms to be displayed
        /// on the right side of the page
        /// </summary>
        private void renderCheckedRoomsList()
        {
            if (phSelectedRoomsList.Controls.Count > 0 || Session["RowInfo"] == null)
                return;
            
            Dictionary<int, RowInfo> rowInfos = (Dictionary<int, RowInfo>)Session["RowInfo"];

            DataSet ds = new DataSet();
            ds.Tables.Add("SelectedRooms");
            ds.Tables["SelectedRooms"].Columns.Add("RoomNumber");
            ds.Tables["SelectedRooms"].Columns.Add("RoomType");
            ds.Tables["SelectedRooms"].Columns.Add("Deselect");

            foreach (KeyValuePair<int, RowInfo> kvp in rowInfos)
            {
                DataRow dr = ds.Tables["SelectedRooms"].NewRow();
                dr["RoomNumber"] = kvp.Value.RoomNumber;
                dr["RoomType"] = kvp.Value.RoomTypeString;

                ds.Tables["SelectedRooms"].Rows.Add(dr);
            }

            gvSelectedRooms.DataSource = ds;
            gvSelectedRooms.DataBind();
        }

        /// <summary>
        /// Removes the selected checkbox from the selected rooms list
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void cbDeselect_CheckedChanged(object sender, EventArgs e)
        {
            Dictionary<int, RowInfo> rowInfo = (Dictionary<int, RowInfo>)Session["RowInfo"];
            GridViewRow r = null;
            int rowID = 0;

            foreach (GridViewRow gvr in gvSelectedRooms.Rows)
            {
                if (gvr.Cells[2].FindControl("cbDeselect") == sender)
                {
                    r = gvr;
                    break;
                }
            }

            foreach (KeyValuePair<int, RowInfo> kvp in rowInfo)
            {
                if (kvp.Value.RoomNumber == r.Cells[0].Text)
                {
                    rowID = kvp.Value.RowIndex;
                    rowInfo.Remove(kvp.Key);
                    break;
                }
            }

            colorRow(gvOpenRooms, rowID, System.Drawing.Color.White);

            if (gvOpenRooms.Rows.Count > rowID)
                ((CheckBox)gvOpenRooms.Rows[rowID].FindControl("cbSelected")).Checked = false;
            renderCheckedRoomsList();
            lblTotalRooms.Text = rowInfo.Count.ToString();
            if (rowInfo.Count == 0)
                btnSelect.Enabled = false;
        }

        /// <summary>
        /// Fires when a checkbox in the selected rooms list is changed
        /// </summary>
        /// <param name="sender">CheckBox that was toggled</param>
        /// <param name="e"></param>
        protected void cbSelected_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox cb = (CheckBox)sender;
            int roomID = 0, rowID = 0;
            string roomNumber = String.Empty;
            Dictionary<int, RowInfo> rowInfo = (Dictionary<int, RowInfo>)Session["RowInfo"];
            
            //Determine the exact RoomID of the checkbox
            int rows = 0;
            foreach (GridViewRow r in gvOpenRooms.Rows)
            {
                CheckBox temp = ((CheckBox)r.FindControl("cbSelected"));

                //If the checkboxes have the same address
                if(temp == cb)
                {
                    roomID = Convert.ToInt32(r.Cells[0].Text);
                    roomNumber = r.Cells[1].Text;
                    rowID = rows;
                    break;
                }
                rows++;
            }

            //If the CheckBox hasn't been checked yet
            if (!rowInfo.ContainsKey(roomID))
            {
                //Add it to the linked list
                rowInfo.Add(roomID, new RowInfo(ddlRoomTypes.SelectedValue, rowID, gvOpenRooms.PageIndex, roomNumber, ddlRoomTypes.SelectedItem.ToString()));
                colorRow(gvOpenRooms, rowID, System.Drawing.Color.Yellow);
                cb.Focus();
            }
            else
            {
                //Remove it from the linked list
                rowInfo.Remove(roomID);
                colorRow(gvOpenRooms, rowID, System.Drawing.Color.White);
                cb.Checked = false;
            }

            updatePageInfo(rowInfo.Count);
            renderCheckedRoomsList();
        }

        /// <summary>
        /// Iterates through each cell of the row in the gridview object, setting
        /// the corresponding cells to the specified color
        /// </summary>
        /// <param name="gv">GridView object</param>
        /// <param name="rowIndex">Row to be colored</param>
        /// <param name="color">Color to set background color to</param>
        private void colorRow(GridView gv, int rowIndex, System.Drawing.Color color)
        {
            if (gv.Rows.Count <= rowIndex)
                return;
            
            foreach (TableCell c in gv.Rows[rowIndex].Cells)
                c.BackColor = color;
        }

        /// <summary>
        /// After the rooms have been returned from the database, check to see which
        /// rooms have been previously clicked to correctly update the rooms list
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void gvOpenRooms_DataBound(object sender, EventArgs e)
        {
            if (gvOpenRooms.Rows.Count > 0)
            {
                checkSelectedRooms();
                lblNoOpenRooms.Text = String.Empty;
            }
            else
                lblNoOpenRooms.Text = "There are no " + ddlRoomTypes.SelectedItem.Text + " rooms open.";
        }

        /// <summary>
        /// When clicked, all rooms that were selected are loaded into a linked list
        /// structure and passed back in a new session variable
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSelect_Click(object sender, EventArgs e)
        {
            gvOpenRooms.Columns[0].Visible = true;
            gvOpenRooms.DataBind();
            LinkedList<RoomInfo> roomIDs = new LinkedList<RoomInfo>();

            Dictionary<int, RowInfo> rowInfo = (Dictionary<int, RowInfo>)Session["RowInfo"];
            foreach (KeyValuePair<int, RowInfo> kvp in rowInfo)
                roomIDs.AddFirst(new RoomInfo(kvp.Key, kvp.Value.RoomNumber));

            if (Session["roomIDs"] == null)
                Session.Add("roomIDs", roomIDs);
            else
                Session["roomIDs"] = roomIDs;
            gvOpenRooms.Columns[0].Visible = false;
            gvOpenRooms.DataBind();
            Response.Redirect("~/Clerk/CreateReservation.aspx");
        }

        /// <summary>
        /// After updating the checked rooms list, this function will deselect any
        /// rooms that were unchecked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnUpdateCheckedRoomsList_OnClick(object sender, EventArgs e)
        {
            renderCheckedRoomsList(); //Fill with values prior to execution
            
            Dictionary<int, RowInfo> rowInfos = (Dictionary<int, RowInfo>)Session["RowInfo"];
            LinkedList<int> RemoveValues = new LinkedList<int>();
            foreach (KeyValuePair<int, RowInfo> kvp in rowInfos)
            {
                if (!((CheckBox)phSelectedRoomsList.FindControl(kvp.Value.cb.ID)).Checked)
                {
                    RemoveValues.AddFirst(kvp.Key);
                    colorRow(gvOpenRooms, kvp.Value.RowIndex, System.Drawing.Color.White);
                    ((CheckBox)gvOpenRooms.Rows[kvp.Value.RowIndex].FindControl("cbSelected")).Checked = false;
                }
            }

            foreach (int i in RemoveValues)
                rowInfos.Remove(i);

            if(RemoveValues.Count > 0)
                checkSelectedRooms();
        }

        /// <summary>
        /// Row color remains persistent when the page is changed, so
        /// this function will appropriately "decolor" unchecked items
        /// and recolor colored items.
        /// Also rechecks checkboxes on a different page
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void gvOpenRooms_PageIndexChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < gvOpenRooms.Rows.Count; i++)
            {
                colorRow(gvOpenRooms, i, System.Drawing.Color.White);
                ((CheckBox)gvOpenRooms.Rows[i].FindControl("cbSelected")).Checked = false;
            }

            //checkSelectedRooms();
        }

        /// <summary>
        /// Renders the checked rooms list
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void phSelectedRoomedsList_PreRender(object sender, EventArgs e)
        {
            renderCheckedRoomsList();
        }

        /// <summary>
        /// Gathers the information from the dynamic checkboxes to update their
        /// logical counterpart so information is not lost on re-loading
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void phSelectedRoomedsList_Unload(object sender, EventArgs e)
        {         
            Dictionary<int, RowInfo> rowInfos = (Dictionary<int, RowInfo>)Session["RowInfo"];
            LinkedList<int> RemoveValues = new LinkedList<int>();

            foreach (KeyValuePair<int, RowInfo> kvp in rowInfos)
                kvp.Value.cb.Checked =
                    ((CheckBox)phSelectedRoomsList.FindControl(kvp.Value.cb.ID)).Checked;
        }

        /// <summary>
        /// Uses SQL to query results from the database. If no rows are returned,
        /// then select all rooms
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void gvOpenRooms_PreRender(object sender, EventArgs e)
        {
            checkSelectedRooms();
            #region Old Code
            /*if (gvOpenRooms.Rows.Count != 0 && 
                Session["LastRoomType"].ToString() == ddlRoomTypes.SelectedValue)
                return;
            
            using (SqlConnection conn = new SqlConnection(
                ConfigurationManager.ConnectionStrings["HotelDB"].ConnectionString))
            {
                conn.Open();

                string command = "SELECT RoomID, RoomNumbers FROM Room " +
                                   "WHERE RoomID NOT IN" +
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

                connCommand.Parameters.AddWithValue("@StartDate", Session["StartDate"]);
                connCommand.Parameters.AddWithValue("@Nights", Session["Nights"]);
                connCommand.Parameters.AddWithValue("@HotelRoomType", ddlRoomTypes.SelectedValue);

                using (SqlDataReader openRooms = connCommand.ExecuteReader())
                {
                    gvOpenRooms.DataSource = openRooms;
                    gvOpenRooms.DataBind();


                    /*if(gvOpenRooms.Rows.Count == 0)
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

                        gvOpenRooms.DataBind();
                    }
                }
            }*/
            #endregion
        }

        /// <summary>
        /// Update the last room type change
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ddlRoomTypes_SelectedIndexChanged(object sender, EventArgs e)
        {
            Session["LastRoomType"] = ddlRoomTypes.SelectedValue;
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
    }
}