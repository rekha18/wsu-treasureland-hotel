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

                if (txtFirstName.Text == "" && txtSurname.Text == "" && txtReservationNum.Text == "")
                {
                    lblErrorMessage2.Text = "You must enter information in at least one box";
                }
                else
                {
                    //if there are not values entered, default values are added
                    lblErrorMessage2.Text = "";  //clear the error message on screen if there is one
                    btnSelectGuest.Visible = false;//clear the select guest button in case it comes up
                    if (txtFirstName.Text == "")
                        txtFirstName.Text = "none";
                    if (txtSurname.Text == "")
                        txtSurname.Text = "none";
                    if (txtReservationNum.Text == "")
                        txtReservationNum.Text = "0";



                    //Gridview is populated with data
                    gvGuest.DataSource = App_Code.GuestDB.LocateGuestCheckIn(txtFirstName.Text, txtSurname.Text, txtReservationNum.Text);
                    gvGuest.DataBind();
                   


                    //Clears the default values for the textboxes
                    if (txtFirstName.Text == "none")
                        txtFirstName.Text = "";
                    if (txtSurname.Text == "none")
                        txtSurname.Text = "";
                    if (txtReservationNum.Text == "0")
                        txtReservationNum.Text = "";

                    if (gvGuest.Rows.Count == 0)
                    {
                        lblErrorMessage2.Text = "No customers found";
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
//                txtShowRoomType.Text = gvGuest.SelectedRow.Cells[5].Text;
//                txtShowPhone.Text = gvGuest.SelectedRow.Cells[6].Text;
//                txtShowCheckOut.Text = gvGuest.SelectedRow.Cells[7].Text;
//                txtShowNumGuests.Text = gvGuest.SelectedRow.Cells[8].Text;
                txtShowReservationNum.Text = gvGuest.SelectedRow.Cells[1].Text;
                txtShowFirstName.Text = gvGuest.SelectedRow.Cells[2].Text;
                txtShowSurname.Text = gvGuest.SelectedRow.Cells[3].Text;
                txtShowRoomNum.Text = gvGuest.SelectedRow.Cells[0].Text;

                //get the discount
                ArrayList myArrList = new ArrayList();
                myArrList = App_Code.GuestDB.getGuestInformation(Convert.ToInt32(gvGuest.SelectedRow.Cells[0].Text));
                //if there are no items in the arrayList then there is no discount
                if (myArrList.Count != 0)
                {

                    txtShowReservationNum.Text = myArrList[0].ToString();
                    txtShowRoomType.Text = myArrList[1].ToString();
                    txtShowRoomNum.Text = myArrList[2].ToString();
                    txtShowNumGuests.Text = (Convert.ToInt32(myArrList[3]) + Convert.ToInt32(myArrList[4])).ToString();
                    txtShowFirstName.Text = myArrList[5].ToString();
                    txtShowSurname.Text = myArrList[6].ToString();
                    txtShowPhone.Text = myArrList[7].ToString();
                    txtShowCheckOut.Text = string.Format("{0:dd/MM/yyyy}", (Convert.ToDateTime(myArrList[8]).AddDays(Convert.ToInt32(myArrList[9]))));
                
                }
              
            
            }
        }

        protected void btnCheckIn_Click(object sender, EventArgs e)
        {
            App_Code.GuestDB.updateRoomStatus('C', Convert.ToInt32(txtShowRoomNum.Text));
            App_Code.GuestDB.updateReservationDetail('A', Convert.ToInt32(gvGuest.SelectedRow.Cells[0].Text));
            if(App_Code.GuestDB.countConfirmedReservationDetail(Convert.ToInt32(gvGuest.SelectedRow.Cells[3].Text))==0)
            {
                App_Code.GuestDB.updateReservationStatus('A', Convert.ToInt32(gvGuest.SelectedRow.Cells[3].Text));
            }
            mvLocateGuest.ActiveViewIndex = 2;
        }
    }
}