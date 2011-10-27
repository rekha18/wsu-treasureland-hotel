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
        private GridRangeView cHome = new GridRangeView();

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// Generates the GridRangeView object before the HTML page is
        /// rendered.
        /// </summary>
        /// <param name="sender">lbtnTable</param>
        /// <param name="e"></param>
        protected void generateTable(object sender, EventArgs e)
        {
            cHome.update();
            lblTable.Text = cHome.generateTableHTML(true);

            //Generate label information
            lbtnDatePrevious.Text = "Previous " + GridRangeView.DaysDisplayed + " Days";
            lbtnDateFuture.Text = "Next " + GridRangeView.DaysDisplayed + " Days";
            lbtnPageNext.Text = "Next " + GridRangeView.PageSize + " Rooms";
            lbtnPagePrevious.Text = "Next " + GridRangeView.PageSize + " Rooms";

            if (GridRangeView.RoomIndex < GridRangeView.PageSize)
                lbtnPagePrevious.Enabled = false;
            else
                lbtnPagePrevious.Enabled = true;

            if (GridRangeView.RoomIndex + GridRangeView.PageSize >= GridRangeView.MaxRooms)
                lbtnPageNext.Enabled = false;
            else
                lbtnPagePrevious.Enabled = true;
        }

        /// <summary>
        /// Decrements the current date by the DaysDisplayed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void lbtnPrevious_Click(object sender, EventArgs e)
        {
            GridRangeView.current = GridRangeView.current.AddDays(-GridRangeView.DaysDisplayed);
        }

        /// <summary>
        /// Increments the current date by the DaysDisplayed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void lbtnFuture_Click(object sender, EventArgs e)
        {
            GridRangeView.current = GridRangeView.current.AddDays(GridRangeView.DaysDisplayed);
        }

        /// <summary>
        /// Sets the current date to the current time
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void lbtnToday_Click(object sender, EventArgs e)
        {
            GridRangeView.current = DateTime.Now.Date;
        }
    }
}