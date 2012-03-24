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
            ContentPlaceHolder1.Focus();
            
        }

        protected void btnHome_Click(object sender, EventArgs e)
        {
            Session.RemoveAll();
            Response.Redirect("ClerkDefault.aspx");

        }

        protected void btnCheckIn_Click(object sender, EventArgs e)
        {
            Session.RemoveAll();
            Response.Redirect("LocateGuest.aspx");
           
        }

        protected void Checkout_Click(object sender, EventArgs e)
        {
            Session.RemoveAll();
            Response.Redirect("LocateReservation.aspx");
            
        }

        protected void btnUpdateGuestFolio_Click(object sender, EventArgs e)
        {
            Session.RemoveAll();
            Response.Redirect("UpdateGuestFolio.aspx");
            
        }

        protected void btnCreateReservation_Click(object sender, EventArgs e)
        {
            Session.RemoveAll();
            Response.Redirect("CreateReservation.aspx");
            
        }

        protected void btnUpdateReservation_Click(object sender, EventArgs e)
        {
            Session.RemoveAll();
            Response.Redirect("UpdateReservation.aspx");
            
        }

        protected void btnViewGuest_Click(object sender, EventArgs e)
        {
            Session.RemoveAll();

            bool ischeckout = false;
            Session.Add("Checkout", ischeckout);
            Response.Redirect("ViewGuests.aspx");
            
        }

        protected void btnCheckout_Click(object sender, EventArgs e)
        {
            Session.RemoveAll();
            bool ischeckout = true;
            Session.Add("Checkout", ischeckout);
            Response.Redirect("ViewGuests.aspx");

        }

        protected void btnReports_Click(object sender, EventArgs e)
        {
            Session.RemoveAll();
            Response.Redirect("ReportDefault.aspx");
            
        }
    }
}