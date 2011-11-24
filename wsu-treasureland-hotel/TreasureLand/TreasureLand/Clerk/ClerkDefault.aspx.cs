using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TreasureLand.App_Code;

namespace TreasureLand.Clerk
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        private GridRangeView cHome;
        private bool requiresUpdate = true;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Date"] == null)
            {
                Session.Add("Date", DateTime.Now.Date);
                Session.Add("RoomIndex", 0);
                cHome = new GridRangeView((DateTime)Session["Date"], 0);
            }
            else
                cHome = new GridRangeView((DateTime)Session["Date"], (int)Session["RoomIndex"]);
            
        }

        /// <summary>
        /// Generates the GridRangeView object before the HTML page is
        /// rendered.
        /// </summary>
        /// <param name="sender">lbtnTable</param>
        /// <param name="e"></param>
        protected void generateTable(object sender, EventArgs e)
        {
            if(requiresUpdate)
                cHome.update();
            lblTable.Text = cHome.generateHTMLTablev3(true, "None");

            //Generate label information
            lbtnDatePrevious.Text = "Previous " + GridRangeView.DaysDisplayed + " Days";
            lbtnDateFuture.Text = "Next " + GridRangeView.DaysDisplayed + " Days";
            lbtnPageNext.Text = "Next " + GridRangeView.PageSize + " Rooms";
            lbtnPagePrevious.Text = "Previous " + GridRangeView.PageSize + " Rooms";

            if (cHome.RoomIndex < GridRangeView.PageSize)
                lbtnPagePrevious.Enabled = false;
            else
                lbtnPagePrevious.Enabled = true;

            if (cHome.RoomIndex + GridRangeView.PageSize >= GridRangeView.MaxRooms)
                lbtnPageNext.Enabled = false;
            else
                lbtnPageNext.Enabled = true;

            requiresUpdate = false;
        }

        /// <summary>
        /// Decrements the current date by the DaysDisplayed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void lbtnPrevious_Click(object sender, EventArgs e)
        {
            cHome.current = cHome.current.AddDays(-GridRangeView.DaysDisplayed);
            Session["Date"] = cHome.current;
            requiresUpdate = true;
        }

        /// <summary>
        /// Increments the current date by the DaysDisplayed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void lbtnFuture_Click(object sender, EventArgs e)
        {
            cHome.current = cHome.current.AddDays(GridRangeView.DaysDisplayed);
            Session["Date"] = cHome.current;
            requiresUpdate = true;
        }

        /// <summary>
        /// Sets the current date to the current time
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void lbtnToday_Click(object sender, EventArgs e)
        {
            cHome.current = DateTime.Now.Date;
            calDatePicker.SelectedDate = cHome.current;
            Session["Date"] = cHome.current;
            requiresUpdate = true;
        }

        /// <summary>
        /// Sets the paging controls back by the number of room defined in the GridRangeView class
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lbtnPagePrevious_Click(object sender, EventArgs e)
        {
            if (cHome.RoomIndex >= GridRangeView.PageSize)
            {
                cHome.RoomIndex -= GridRangeView.PageSize;
                Session["RoomIndex"] = cHome.RoomIndex;
                lbtnPagePrevious.Focus();
                requiresUpdate = true;
            }
        }

        /// <summary>
        /// Sets the paging controls forward by the number of room defined in 
        /// the GridRangeView class
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lbtnPageNext_Click(object sender, EventArgs e)
        {
            if (cHome.RoomIndex + GridRangeView.PageSize < GridRangeView.MaxRooms)
            {
                cHome.RoomIndex += GridRangeView.PageSize;
                Session["RoomIndex"] = cHome.RoomIndex;
                lbtnPageNext.Focus();
                requiresUpdate = true;
            }
        }

        /// <summary>
        /// Sets the GridRangeView table to display a specific date
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void calDatePicker_SelectionChanged(object sender, EventArgs e)
        {
            cHome.current = calDatePicker.SelectedDate;
            Session["Date"] = cHome.current;
            requiresUpdate = true;
        }
    }
}