using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TreasureLand.App_Code;
using System.Security;
using System.Web.Security;
using System.Collections;
using System.Data;
using System.Data.SqlClient;
using TreasureLand.DBM;

namespace TreasureLand
{
    public partial class LoginRedirect : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Roles.IsUserInRole("Admin"))
            {
                btnAdmin.Visible = true;
            }
            if (Roles.IsUserInRole("Manager"))
            {
                btnManager.Visible = true;
            } 
            if (Roles.IsUserInRole("Clerk"))
            {
                btnClerk.Visible = true;
            }
            if (Roles.IsUserInRole("Housekeeping"))
            {
                btnHousekeeping.Visible = true;
            }
            if (Roles.IsUserInRole("Maintenance"))
            {
                btnMaintenance.Visible = true;
            }
        }

        protected void btnAdmin_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Admin/AdminDefault.aspx");
        }

        protected void btnManager_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Manager/ManageExpenses.aspx");
        }

        protected void btnClerk_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Clerk/ClerkDefault.aspx");
        }

        protected void btnHousekeeping_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Housekeeping/HousekeepingDefault.aspx");
        }

        protected void btnMaintenance_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Maintenance/MaintenanceDefault.aspx");
        }
    }
}