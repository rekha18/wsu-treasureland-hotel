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
            Session.RemoveAll();
        }

        protected void btnCheckIn_Click(object sender, EventArgs e)
        {
            Response.Redirect("LocateGuest.aspx");
            Session.RemoveAll();
        }

        protected void Checkout_Click(object sender, EventArgs e)
        {
            Response.Redirect("LocateReservation.aspx");
            Session.RemoveAll();
        }

        protected void btnUpdateGuestFolio_Click(object sender, EventArgs e)
        {
            Response.Redirect("UpdateGuestFolio.aspx");
            Session.RemoveAll();
        }

        protected void btnCreateReservation_Click(object sender, EventArgs e)
        {
            Response.Redirect("CreateReservation.aspx");
            Session.RemoveAll();
        }

        protected void btnUpdateReservation_Click(object sender, EventArgs e)
        {
            Response.Redirect("UpdateReservation.aspx");
            Session.RemoveAll();
        }

        protected void btnViewGuest_Click(object sender, EventArgs e)
        {
            Response.Redirect("ViewGuests.aspx");
            Session.RemoveAll();
        }

        protected void btnReports_Click(object sender, EventArgs e)
        {
            Response.Redirect("Reports.aspx");
            Session.RemoveAll();
        }
    }
}