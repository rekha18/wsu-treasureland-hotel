using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

namespace TreasureLand.Clerk
{
    public partial class LocateGuest : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {


        }

        protected void gvGuest_SelectedIndexChanged(object sender, EventArgs e)
        {
            btnSelectGuest.Visible = true;
            gvGuest.SelectedRow.BackColor = System.Drawing.Color.Yellow;
        }

        protected void gvGuest_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
        {
            if (gvGuest.SelectedIndex > -1)
            {
                gvGuest.SelectedRow.BackColor = System.Drawing.Color.White;
            }
        }

        protected void btnLocateGuest_Click(object sender, EventArgs e)
        {
            {

                if (txtFirstName.Text == "" && txtSurname.Text == "" && txtRoomNum.Text == "" && txtReservationNum.Text == "" && txtEmail.Text == "")
                {
                    lblErrorMessageMissingData.Text = "You must enter information in at least one box";
                }
                else
                {
                    //if there are not values entered, default values are added
                    if (txtFirstName.Text == "")
                        txtFirstName.Text = "none";
                    if (txtSurname.Text == "")
                        txtSurname.Text = "none";
                    if (txtRoomNum.Text == "")
                        txtRoomNum.Text = "0";
                    if (txtReservationNum.Text == "")
                        txtReservationNum.Text = "0";
                    if (txtEmail.Text == "")
                        txtEmail.Text = "none";


                    //Gridview is populated with data
                    gvGuest.DataSource = App_Code.GuestDB.LocateGuestCheckIn(txtFirstName.Text, txtSurname.Text, txtReservationNum.Text, txtEmail.Text);
                    gvGuest.DataBind();



                    //Clears the default values for the textboxes
                    if (txtFirstName.Text == "none")
                        txtFirstName.Text = "";
                    if (txtSurname.Text == "none")
                        txtSurname.Text = "";
                    if (txtRoomNum.Text == "0")
                        txtRoomNum.Text = "";
                    if (txtReservationNum.Text == "0")
                        txtReservationNum.Text = "";
                    if (txtEmail.Text == "none")
                        txtEmail.Text = "";

                    if (gvGuest.Rows.Count == 0)
                    {
                        lblErrorMessageNoCustomersFound.Text = "No customers found";
                    }

                }

            }

        }



        protected void btnSelectGuest_Click(object sender, EventArgs e)
        {
            if (gvGuest.SelectedIndex == -1)
                lblErrorMessage2.Text = "You must select a guest";
            else
            {
                //switches to the next view
                mvLocateGuest.ActiveViewIndex = 1;
                //Grabs the values from the gridview and populates the textboxes with the information
//                txtShowRoomNum.Text = gvGuest.SelectedRow.Cells[0].Text;
//                txtShowRoomType.Text = gvGuest.SelectedRow.Cells[5].Text;
//                txtShowPhone.Text = gvGuest.SelectedRow.Cells[6].Text;
//                txtShowCheckOut.Text = gvGuest.SelectedRow.Cells[7].Text;
//                txtShowNumGuests.Text = gvGuest.SelectedRow.Cells[8].Text;
                txtShowReservationNum.Text = gvGuest.SelectedRow.Cells[1].Text;
                txtShowFirstName.Text = gvGuest.SelectedRow.Cells[2].Text;
                txtShowSurname.Text = gvGuest.SelectedRow.Cells[3].Text;
                txtShowEmail.Text = gvGuest.SelectedRow.Cells[4].Text;
            }
        }

        protected void btnCheckIn_Click(object sender, EventArgs e)
        {
            //Needs code to mark room as checked in
        }
    }
}