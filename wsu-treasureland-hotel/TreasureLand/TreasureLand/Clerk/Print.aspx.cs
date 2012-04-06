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
            lblRoomTotal.Text = list[4];
            lblServicesTotal.Text = list[5];
            lblTotal.Text = list[6];
            lblDiscount.Text = list[7];
            lblCheckinDate.Text = list[8];
            lblCheckoutDate.Text = list[9];
            lblReservationDetailID.Text = list[10];
            
            
        }
        private void checkout()
        {
            try
            {
                List<string> list = (List<string>)Session["GuestInfo"];
                string resdetailID = list[10];
                GuestDB.updateReservationDetail('F', Convert.ToInt32(resdetailID));

                if (App_Code.GuestDB.countActiveReservationDetail(Convert.ToInt32(lblReservationNumber.Text)) == 0)
                {
                    App_Code.GuestDB.updateReservationStatus('F', Convert.ToInt32(lblReservationNumber.Text));
                }
                TreasureLandDataClassesDataContext db = new TreasureLandDataClassesDataContext();
                var roomID = from r in db.Rooms
                             where r.RoomNumbers == lblRoomNumber.Text
                             select new { r.RoomID };

              
                GuestDB.updateRoomStatus('H', roomID.First().RoomID);

            }
            catch (Exception)
            {                
                throw;
            }
        }
        /// <summary>
        /// Checks out the guest.  Changes the ReservationDetail status, Reservation status and updateStatus.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void GoToCheckOut_Click(object sender, EventArgs e)
        {

            if (Convert.ToDouble(lblTotal.Text)==0)
            {
                PnCollections.Visible = false;
                PnGuestOwedMoney.Visible = false;
                pnCheckout.Visible = true;
                checkout();
                btnGoToCheckOut.Visible = false;
            }
            else if (Convert.ToDouble(lblTotal.Text)>0)
            {
                PnGuestOwedMoney.Visible = false;
                PnCollections.Visible = true;
                pnCheckout.Visible = false;
            }
            else
            {
                PnGuestOwedMoney.Visible = true;
                pnCheckout.Visible = false;
                PnCollections.Visible = false;
            }
        }

        protected void btnSendToCollections_Click(object sender, EventArgs e)
        {
            TreasureLandDataClassesDataContext db = new TreasureLandDataClassesDataContext();
            Collection addCollection = new Collection();
            addCollection.CollectionsAmountOwed = Convert.ToDecimal(lblTotal.Text);
            addCollection.ReservationDetailID = Convert.ToInt16(lblReservationDetailID.Text);
            db.Collections.InsertOnSubmit(addCollection);
            db.SubmitChanges();
            checkout();
            pnCheckout.Visible = true;
            PnCollections.Visible = false;
            btnGoToCheckOut.Visible = false;
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            Response.Redirect("ClerkDefault.aspx");
        }
    }
}