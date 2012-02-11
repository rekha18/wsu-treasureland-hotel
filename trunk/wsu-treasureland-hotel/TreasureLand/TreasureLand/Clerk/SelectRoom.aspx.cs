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
        private class RowInfo
        {
            public string RoomType;
            public int RowIndex;
            public int PageIndex;
            public string RoomNumber;
            public CheckBox cb;
            private static ushort counter = 0;

            public RowInfo(string RoomType, int RowIndex, int PageIndex, string RoomNumber)
            {
                this.RoomType = RoomType;
                this.RowIndex = RowIndex;
                this.PageIndex = PageIndex;
                this.RoomNumber = RoomNumber;

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
            foreach (KeyValuePair<int, RowInfo> kvp in rowInfos)
            {
                Literal l = new Literal();
                l.Text = " - Room #" + kvp.Value.RoomNumber + "<br />";
                phSelectedRoomsList.Controls.Add(kvp.Value.cb);
                phSelectedRoomsList.Controls.Add(l);
            }

            if (rowInfos.Count > 0)
            {
                btnUpdateCheckedRoomsList.Visible = true;
                phSelectedRoomsList.DataBind();
            }
            else
                btnUpdateCheckedRoomsList.Visible = false;
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
                rowInfo.Add(roomID, new RowInfo(ddlRoomTypes.SelectedValue, rowID, gvOpenRooms.PageIndex, roomNumber));
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
            checkSelectedRooms();
        }

        /// <summary>
        /// When clicked, all rooms that were selected are loaded into a linked list
        /// structure and passed back in a new session variable
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSelect_Click(object sender, EventArgs e)
        {
            LinkedList<int> roomIDs = new LinkedList<int>();

            Dictionary<int, RowInfo> rowInfo = (Dictionary<int, RowInfo>)Session["RowInfo"];
            foreach (KeyValuePair<int, RowInfo> kvp in rowInfo)
                roomIDs.AddFirst(kvp.Key);

            if (Session["roomIDs"] == null)
                Session.Add("roomIDs", roomIDs);
            else
                Session["roomIDs"] = roomIDs;
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

            checkSelectedRooms();
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
    }
}