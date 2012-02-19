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
                databindRoomInfo();
            }
            if(Session["Charges"] !=null)
            {

                gvGuestServices.DataBind();
            }
            databindGuestServices();
 
        }


        private void databindRoomInfo()
        {
            List<int> roomListInfo= (List<int>)Session["RoomInfo"];
           
            TreasureLandDataClassesDataContext db = new TreasureLandDataClassesDataContext();
            var guestRoom = from r in db.Reservations
                            join rd in db.ReservationDetails
                            on r.ReservationID equals rd.ReservationID
                            join ro in db.Rooms
                            on rd.RoomID equals ro.RoomID
                            join hrt in db.HotelRoomTypes
                            on ro.HotelRoomTypeID equals hrt.HotelRoomTypeID
                            where r.ReservationID == roomListInfo[0] && ro.RoomID == roomListInfo[1]
                            select new { r.ReservationID, rd.Nights, rd.QuotedRate, hrt.RoomType };

            gvRoomCost.DataSource = guestRoom.ToList();
            gvRoomCost.DataBind();
            HotelRoomType hotelRoomType = new HotelRoomType();
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
            lblRoomTotal.Text = list[4];
            lblServicesTotal.Text = list[5];
            lblTotal.Text = list[6];
            lblDiscount.Text = list[7];
            TreasureLandDataClassesDataContext db = new TreasureLandDataClassesDataContext();
            IEnumerable<TreasureLand.DBM.Guest> guest =
                        from g in db.Guests
                        join r in db.Reservations
                        on g.GuestID equals r.GuestID
                        where r.ReservationID == Convert.ToInt32(list[0])
                        select g;
            foreach (var guests in guest)
            {
                lblAddress.Text = guests.GuestAddress;
                lblCity.Text = guests.GuestCity;
                lblTotalDue.Text = list[6];
            }
        }
    }
}