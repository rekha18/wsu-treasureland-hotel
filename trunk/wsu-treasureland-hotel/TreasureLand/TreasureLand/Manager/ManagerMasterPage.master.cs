using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TreasureLand.Manager
{
    public partial class ManagerMasterPage : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnHome_Click(object sender, EventArgs e)
        {
            Response.Redirect("ManagerExpenses.aspx");
            Session.RemoveAll();
        }

        protected void btnInventory_Click(object sender, EventArgs e)
        {
            Response.Redirect("Inventory.aspx");
            Session.RemoveAll();
        }

        protected void btnReports_Click(object sender, EventArgs e)
        {
            Response.Redirect("ManagerReports.aspx");
            Session.RemoveAll();
        }

        protected void btnExpenses_Click(object sender, EventArgs e)
        {
            Response.Redirect("ManagerExpenses.aspx");
            Session.RemoveAll();
        }

        protected void btnDeposit_Click(object sender, EventArgs e)
        {
            Response.Redirect("ManagerDeposit.aspx");
            Session.RemoveAll();
        }

        protected void btnShortTermInventory_Click(object sender, EventArgs e)
        {
            Response.Redirect("ShortTermAsset.aspx");
            Session.RemoveAll();
        }
    }
}