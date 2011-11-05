using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TreasureLand.Admin
{
    public partial class AdminMasterPage1 : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnHome_Click(object sender, EventArgs e)
        {
            Response.Redirect("AdminDefault.aspx");
        }

        protected void btnReports_Click(object sender, EventArgs e)
        {
            Response.Redirect("ReportsAdmin.aspx");
        }

        protected void btnManageRooms_Click(object sender, EventArgs e)
        {
            Response.Redirect("ManageRooms.aspx");
        }

        protected void btnManageUsers_Click(object sender, EventArgs e)
        {
            Response.Redirect("ManageUsers.aspx");
        }

        protected void btnCreateUser_Click(object sender, EventArgs e)
        {
            Response.Redirect("CreateUser.aspx");
        }

        protected void btnManageStatuses_Click(object sender, EventArgs e)
        {
            Response.Redirect("ManageStatuses.aspx");
        }

        protected void btnManageDiscounts_Click(object sender, EventArgs e)
        {
            Response.Redirect("ManageDiscounts.aspx");
        }

        protected void btnManageBillingCategories_Click(object sender, EventArgs e)
        {
            Response.Redirect("ManageBillingCategories.aspx");
        }

        protected void btnManageRoomTypes_Click(object sender, EventArgs e)
        {
            Response.Redirect("ManageRoomTypes.aspx");
        }
    }
}