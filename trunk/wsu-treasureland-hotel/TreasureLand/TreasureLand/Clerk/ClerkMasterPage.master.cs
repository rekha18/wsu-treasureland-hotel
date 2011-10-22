using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TreasureLand.Clerk
{
    public partial class ClerkMasterPage : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnHome_Click(object sender, EventArgs e)
        {
            Response.Redirect("ClerkDefault.aspx");
        }

        protected void btnCheckIn_Click(object sender, EventArgs e)
        {
            Response.Redirect("CheckIn.aspx");
        }

        protected void Checkout_Click(object sender, EventArgs e)
        {
            Response.Redirect("CheckOut.aspx");
        }

        protected void btnUpdateGuestFolio_Click(object sender, EventArgs e)
        {
            Response.Redirect("UpdateGuestFolio.aspx");
        }

        protected void btnCreateReservation_Click(object sender, EventArgs e)
        {
            Response.Redirect("CreateReservation.aspx");
        }

        protected void btnUpdateReservation_Click(object sender, EventArgs e)
        {
            Response.Redirect("UpdateReservation.aspx");
        }

        protected void btnViewGuest_Click(object sender, EventArgs e)
        {
            Response.Redirect("ViewGuests.aspx");
        }

        protected void btnReports_Click(object sender, EventArgs e)
        {
            Response.Redirect("Reports.aspx");
        }
    }
}