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
        }


        protected void gvGuest_SelectedIndexChanged(object sender, EventArgs e)
        {
            btnSelectReservation.Enabled = true;
        }

        

        
    }
}