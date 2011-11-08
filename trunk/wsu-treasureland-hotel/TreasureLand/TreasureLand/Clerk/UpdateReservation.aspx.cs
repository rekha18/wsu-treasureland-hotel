using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TreasureLand.DBM;

namespace TreasureLand.Clerk
{
    public partial class UpdateReservation : System.Web.UI.Page
    {


        protected void Page_Load(object sender, EventArgs e)
        {
            
        }

        protected void btnLocateReservation_Click(object sender, EventArgs e)
        {
            //Locates guest in database based on the values given
            TreasureLandDataClassesDataContext db = new TreasureLandDataClassesDataContext();
            var guest = from g in db.Guests
                        join r in db.Reservations
                        on g.GuestID equals r.GuestID
                        where r.ReservationID.ToString() == txtResNumber.Text || g.GuestFirstName == txtFirstName.Text || g.GuestSurName == txtSurName.Text || g.GuestPhone == txtPhone.Text
                        select new {r.ReservationID, g.GuestFirstName, g.GuestSurName, g.GuestPhone};
            gvGuest.DataSource = guest.ToList();
            gvGuest.DataBind();
        }

        protected void btnSelectReservation_Click(object sender, EventArgs e)
        {
            lblReservationNumber.Text = gvGuest.SelectedRow.Cells[0].Text;
            lblSurName.Text = gvGuest.SelectedRow.Cells[1].Text;
            lblFirstName.Text = gvGuest.SelectedRow.Cells[2].Text;
            lblPhone.Text = gvGuest.SelectedRow.Cells[3].Text;

        }


        protected void gvGuest_SelectedIndexChanged(object sender, EventArgs e)
        {
            btnSelectReservation.Enabled = true;
        }

        protected void gvReservationDetails_SelectedIndexChanged(object sender, EventArgs e)
        {
            TreasureLandDataClassesDataContext db = new TreasureLandDataClassesDataContext();
            var room = from ro in db.Rooms
                       join r in db.HotelRoomTypes
                       on ro.HotelRoomTypeID equals r.HotelRoomTypeID
                       where ro.RoomID == Convert.ToInt16(gvReservationDetails.SelectedRow.Cells[0].Text)
                       select new { ro.RoomNumbers, ro.RoomDescription, ro.RoomBedConfiguration, ro.RoomStatus, r.RoomTypeRackRate };

            gvRoom.DataSource = room.ToList();
            gvRoom.DataBind();
        }

        

        
    }
}