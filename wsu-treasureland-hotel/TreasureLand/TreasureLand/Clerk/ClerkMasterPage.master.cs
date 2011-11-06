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
            Session.Remove("Room");
        }

        protected void btnCheckIn_Click(object sender, EventArgs e)
        {
            Response.Redirect("LocateGuest.aspx");
            Session.Remove("Room");
        }

        protected void Checkout_Click(object sender, EventArgs e)
        {
            Response.Redirect("LocateReservation.aspx");
            Session.Remove("Room");
        }

        protected void btnUpdateGuestFolio_Click(object sender, EventArgs e)
        {
            Response.Redirect("UpdateGuestFolio.aspx");
            Session.Remove("Room");
        }

        protected void btnCreateReservation_Click(object sender, EventArgs e)
        {
            Response.Redirect("CreateReservation.aspx");
            Session.Remove("Room");
        }

        protected void btnUpdateReservation_Click(object sender, EventArgs e)
        {
            Response.Redirect("UpdateReservation.aspx");
            Session.Remove("Room");
        }

        protected void btnViewGuest_Click(object sender, EventArgs e)
        {
            Response.Redirect("ViewGuests.aspx");
            Session.Remove("Room");
        }

        protected void btnReports_Click(object sender, EventArgs e)
        {
            Response.Redirect("Reports.aspx");
            Session.Remove("Room");
        }
    }
}