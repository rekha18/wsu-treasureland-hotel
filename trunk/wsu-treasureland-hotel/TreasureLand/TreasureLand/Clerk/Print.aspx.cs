using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using TreasureLand.DBM;
using TreasureLand.App_Code;

namespace TreasureLand.Clerk
{
    public partial class Print : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(Session["GuestInfo"] != null)            
            {
                databindGuestInfo();
            }
            if(Session["RoomInfo"] !=null)
            {
                gvRoomCost.DataSource = (DataSet)Session["RoomInfo"];
                gvRoomCost.DataBind();
            }
            if(Session["Charges"] !=null)
            {
                //gvGuestServices.DataSource = (DataSet)Session["Charges"];
                gvGuestServices.DataBind();
            }
            databindGuestServices();            
        }

        private void databindRoomInfo()
        {
              /*
            TreasureLandDataClassesDataContext db = new TreasureLandDataClassesDataContext();
            TreasureLand.DBM.ReservationDetailBilling guests = new TreasureLand.DBM.ReservationDetailBilling();
            IEnumerable<TreasureLand.DBM.ReservationDetailBilling> guest =
                        from g in db.ReservationDetailBillings
                        where g.ReservationDetailID == Convert.ToInt32(Session["Charges"])
                        select g;

            gvGuestServices.DataSource = guest;
            gvGuestServices.DataBind();*/
        }
        
        /// <summary>
        /// gets all services charged to the guest and binds to gridview
        /// </summary>
        private void databindGuestServices()
        {
            TreasureLandDataClassesDataContext db = new TreasureLandDataClassesDataContext();
            TreasureLand.DBM.ReservationDetailBilling guests = new TreasureLand.DBM.ReservationDetailBilling();
            IEnumerable<TreasureLand.DBM.ReservationDetailBilling> guest = 
                        from g in db.ReservationDetailBillings
                        where g.ReservationDetailID == Convert.ToInt32(Session["Charges"])
                        select g;

            gvGuestServices.DataSource = guest;
            gvGuestServices.DataBind();
        }

        /// <summary>
        /// get and sets the guest information
        /// </summary>
        private void databindGuestInfo()
        {

            List<string> list = (List<string>)Session["GuestInfo"];
            lblReservationNumber.Text = list[0];
            lblName.Text = list[1] + " " + list[2];
            lblRoomNumber.Text = list[3];
            lblDate.Text = DateTime.Now.ToShortDateString();

        }
    }
}