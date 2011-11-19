using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TreasureLand.Maintenance
{
    public partial class MaintenanceDefault : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (gvHouseKeeping.Rows.Count == 0)
            {
                lblMaintenance.Text = "There are no rooms that need cleaning";
            }
            else
            {
                lblMaintenance.Text = "";
            }
        }

        protected void DataList1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}