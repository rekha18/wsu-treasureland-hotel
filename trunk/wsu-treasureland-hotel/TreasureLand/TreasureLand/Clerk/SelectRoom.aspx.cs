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
        private struct RowInfo
        {
            public string RoomType;
            public int RowIndex;

            public RowInfo(string RoomType, int RowIndex)
            {
                this.RoomType = RoomType;
                this.RowIndex = RowIndex;
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
                if(Session["RowInfo"] != null)
                    Session.Remove("RowInfo");
            
            if (Session["RowInfo"] == null)
                Session.Add("RowInfo", new Dictionary<int, RowInfo>());
            if (Session["LastRoomType"] == null)
                Session.Add("LastRoomType", ddlRoomTypes.SelectedValue);
            
            if (Session["StartDate"] == null)
                Session.Add("StartDate", DateTime.Now);
            if (Session["Nights"] == null)
                Session.Add("Nights", 3);
            
            lblStartDate.Text = Session["StartDate"].ToString();
            lNights.Text = Session["Nights"].ToString();

            checkSelectedRooms();
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
                if(ri.RoomType == ddlRoomTypes.SelectedValue)
                {
                    CheckBox cb = (CheckBox)gvOpenRooms.Rows[ri.RowIndex].FindControl("cbSelected");
                    cb.Checked = true;
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSelect_Click(object sender, EventArgs e)
        {

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
                    rowID = rows;
                    break;
                }
                rows++;
            }

            //If the CheckBox hasn't been checked yet
            if (!rowInfo.ContainsKey(roomID))
            {
                //Add it to the linked list
                rowInfo.Add(roomID, new RowInfo(ddlRoomTypes.SelectedValue, rowID));
                cb.Focus();
            }
            else
            {
                //Remove it from the linked list
                rowInfo.Remove(roomID);
                cb.Checked = false;
            }

            updatePageInfo(rowInfo.Count);
        }
    }
}