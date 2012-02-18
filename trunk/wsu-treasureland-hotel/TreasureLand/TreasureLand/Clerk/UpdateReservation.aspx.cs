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
        public App_Code.Reserve reserving = new App_Code.Reserve();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                reserving.roomID = -1;
                reserving.returnView = 0;
                lblReservationNumber.Text = reserving.roomID.ToString();
                mvUpdateReservation.ActiveViewIndex = reserving.returnView;
            }

            reserving = GetRoomNumber();
            
            
            
        }

        protected void btnLocateReservation_Click(object sender, EventArgs e)
        {
            if (txtFirstName.Text == String.Empty && txtPhone.Text == String.Empty &&
                txtResNumber.Text == String.Empty && txtSurName.Text == String.Empty)
            {
                lblNothingSelected.Text = "Please enter information into at least one box";
                return;
            }
            lblNothingSelected.Text = String.Empty;
            
            //Locates guest in database based on the values given
            TreasureLandDataClassesDataContext db = new TreasureLandDataClassesDataContext();
            var guest = from g in db.Guests
                        join r in db.Reservations
                        on g.GuestID equals r.GuestID
                        where ((r.ReservationID.ToString() == txtResNumber.Text || g.GuestFirstName == txtFirstName.Text || g.GuestSurName == txtSurName.Text || g.GuestPhone == txtPhone.Text) && (r.ReservationStatus == 'U' || r.ReservationStatus == 'C'))
                        select new {r.ReservationID, g.GuestFirstName, g.GuestSurName, g.GuestPhone};
            gvGuest.DataSource = guest.ToList();
            gvGuest.DataBind();

            if (gvGuest.Rows.Count == 0)
                lblNothingSelected.Text = "No records were found";
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
            btnModifyReservation.Enabled = true;
        }

        protected void btnCancelReservation_Click(object sender, EventArgs e)
        {
            TreasureLandDataClassesDataContext db = new TreasureLandDataClassesDataContext();
            var deleteRows = from rs in db.ReservationDetails
                             where rs.ReservationID == Convert.ToInt16(lblReservationNumber.Text)
                             select rs;
            
            db.ReservationDetails.DeleteAllOnSubmit(deleteRows);

            db.SubmitChanges();

            var deleteRes = from r in db.Reservations
                            where r.ReservationID == Convert.ToInt16(lblReservationNumber.Text)
                            select r;
            db.Reservations.DeleteAllOnSubmit(deleteRes);

            db.SubmitChanges();

            
         }

        protected void btnConfirmReservation_Click(object sender, EventArgs e)
        {
            TreasureLandDataClassesDataContext db = new TreasureLandDataClassesDataContext();
            Reservation res = db.Reservations.Single(p=> p.ReservationID == Convert.ToInt16(lblReservationNumber.Text));
            res.ReservationStatus = 'C';
            db.SubmitChanges();
        }

        protected void btnFinished_Click(object sender, EventArgs e)
        {
            Response.Redirect("ClerkDefault.aspx");
            Session.RemoveAll();
        }

        protected void btnFinished2_Click(object sender, EventArgs e)
        {
            Response.Redirect("ClerkDefault.aspx");
            Session.RemoveAll();
        }

        protected void btnModifyReservation_Click(object sender, EventArgs e)
        {
            reserving.returnView = 1;
            reserving.reservationID = Convert.ToInt16(lblReservationNumber.Text);
            reserving.reservationDetailID = (int) gvReservationDetails.DataKeys[gvReservationDetails.SelectedIndex].Value;
            reserving.reserveDate = gvReservationDetails.SelectedRow.Cells[2].Text;
            reserving.daysStaying = Convert.ToInt32(gvReservationDetails.SelectedRow.Cells[3].Text);
            Response.Redirect("ModifyReservation.aspx");
        }

        #region Session Control

        //Create of retrive session
        private App_Code.Reserve GetRoomNumber()
        {
            if (Session["Room"] == null)
                Session.Add("Room", reserving);
            return (App_Code.Reserve)Session["Room"];
        }
        #endregion Session Control

        protected void btnBack_Click(object sender, EventArgs e)
        {
     
        }
    }
}