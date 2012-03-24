using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TreasureLand.Clerk.Reports
{
    public partial class Clerkreports : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnDailyCheckIn_Click(object sender, EventArgs e)
        {
            Response.Redirect("CheckinReports.aspx");
        }

        protected void btnDailyCheckOut_Click(object sender, EventArgs e)
        {
            Response.Redirect("CheckoutReports.aspx");
        }

        protected void btnRestaurantSales_Click(object sender, EventArgs e)
        {
            Response.Redirect("RestaurantCashSalesReport.aspx");
        }

        protected void btnCreditRestaurantSales_Click(object sender, EventArgs e)
        {
            Response.Redirect("RestaurantCreditSalesReport.aspx");
        }

        protected void btnRestaurantReceipt_Click(object sender, EventArgs e)
        {
            Response.Redirect("RestaurantSalesReport.aspx");
        }

        protected void btnOwedByRoom_Click(object sender, EventArgs e)
        {
            Response.Redirect("OwedByRoomReport.aspx");
        }

        protected void btnRoomStatus_Click(object sender, EventArgs e)
        {
            Response.Redirect("DailyRoomStatusReport.aspx");
        }

    }
}