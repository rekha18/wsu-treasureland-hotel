using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TreasureLand.Maintenance
{
    public partial class MaintenanceMasterPage : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnHome_Click(object sender, EventArgs e)
        {
            Response.Redirect("MaintenanceDefault.aspx");
        }

        protected void btnReports_Click(object sender, EventArgs e)
        {
            Response.Redirect("MaintenanceReports.aspx");
        }

        protected void btnInventory_Click(object sender, EventArgs e)
        {
            Response.Redirect("MaintenanceInventory.aspx");
        }
    }
}