using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TreasureLand.HouseKeeping
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (gvHouseKeeping.Rows.Count == 0)
            {
                lblHousekeeping.Text = "There are no rooms that need cleaning";
            }
            else
            {
                lblHousekeeping.Text = "";
            }
        }
    }
}