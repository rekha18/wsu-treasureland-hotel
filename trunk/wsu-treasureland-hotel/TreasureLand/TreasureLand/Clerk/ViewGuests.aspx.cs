using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TreasureLand.App_Code;

namespace TreasureLand.Clerk
{
    
    public partial class WebForm7 : System.Web.UI.Page
    {
      
        protected void Page_Load(object sender, EventArgs e)
        {
           // gvGuest.EnableDynamicData(typeof(RoomDB)); 

        } 

        protected void gvGuest_SelectedIndexChanged(object sender, EventArgs e)
        {
            mvViewGuest.ActiveViewIndex = 1;
            txtShowReservation.Text = gvGuest.SelectedRow.Cells[0].Text;
            txtShowFirstName.Text = gvGuest.SelectedRow.Cells[1].Text;
            txtShowSurName.Text = gvGuest.SelectedRow.Cells[2].Text;
            txtShowRoom.Text = gvGuest.SelectedRow.Cells[3].Text;
        }


        protected void btnLocate_Click(object sender, EventArgs e)
        {
            if (txtFirstName.Text == "")
                txtFirstName.Text = " ";
            if (txtSurName.Text == "")
                txtSurName.Text = " ";
            if (txtRoomNumber.Text == "")
                txtRoomNumber.Text = " ";
            if (txtReservation.Text == "")
                txtReservation.Text = " ";

            gvGuest.DataSource = App_Code.RoomDB.LocateGuestRoom(txtFirstName.Text, txtSurName.Text, txtReservation.Text, Convert.ToInt32(txtRoomNumber.Text));
            
            gvGuest.DataBind();
        
        }
    }
}