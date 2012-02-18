using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TreasureLand.HouseKeeping
{
    public partial class HouseKeepingMaterPage : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnHome_Click(object sender, EventArgs e)
        {
            Response.Redirect("HouseKeepingDefault.aspx");
        }

        protected void btnCheckIn_Click(object sender, EventArgs e)
        {
            Response.Redirect("Inventory.aspx");
        }

        protected void btnInventory_Click(object sender, EventArgs e)
        {
            Response.Redirect("Inventory.aspx");
        }

        protected void btnReports_Click(object sender, EventArgs e)
        {
            Response.Redirect("HouseKeepingReports.aspx");
        }

        protected void btnLongTermAsset_Click(object sender, EventArgs e)
        {
            Response.Redirect("HousekeepingLongTermAsset.aspx");
        }
    }
}