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
using TreasureLand.DBM;

namespace TreasureLand.Clerk
{

    public partial class LocateGuest : System.Web.UI.Page
    {
        public static bool change = false;
        protected void Page_Load(object sender, EventArgs e)
        {


        }


        protected void gvGuest_SelectedIndexChanged(object sender, EventArgs e)
        {



            if (gvUnconfirmedGuest.SelectedIndex >= 0 && change == true)
            {
                btnSelectGuest.Visible = false;
                btnConfirm.Visible = true;
                change = false;
                gvUnconfirmedGuest.SelectRow(-1);
                change = true;
                lblUnconfirmedGuests.Visible = false;
                lblConfirmedGuests.Visible = true;

            }
            else
            {
                change = true;

            }
            btnSelectGuest.Visible = true;
            btnConfirm.Visible = false;
            lblUnconfirmedGuests.Visible = true;
            lblConfirmedGuests.Visible = false;
        }
        protected void gvUnconfirmedGuest_SelectedIndexChanged(object sender, EventArgs e)
        {


            if (gvGuest.SelectedIndex >= 0 && change == true)
            {
                btnSelectGuest.Visible = true;
                btnConfirm.Visible = false;
                change = false;
                gvGuest.SelectRow(-1);
                change = true;
                
                
            }
            else
            {
                change = true;

            }
            btnSelectGuest.Visible = false;
            btnConfirm.Visible = true;
        }
        
        /// <summary>
        /// Locates the guest based on the entered information
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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

                    TreasureLandDataClassesDataContext db = new TreasureLandDataClassesDataContext();
                    var guest = from g in db.Guests
                                join r in db.Reservations
                                on g.GuestID equals r.GuestID
                                join rd in db.ReservationDetails
                                on r.ReservationID equals rd.ReservationID
                                join ro in db.Rooms
                                on rd.RoomID equals ro.RoomID
                                where (r.ReservationID == Convert.ToInt16(txtReservationNum.Text) || g.GuestFirstName == txtFirstName.Text || g.GuestSurName == txtSurname.Text) && (r.ReservationStatus == 'C')
                                select new { r.ReservationID, g.GuestFirstName, g.GuestSurName, rd.ReservationDetailID, };

                    gvGuest.DataSource = guest.ToList();
                    gvGuest.DataBind();

                    var guests = from g in db.Guests
                                join r in db.Reservations
                                on g.GuestID equals r.GuestID
                                join rd in db.ReservationDetails
                                on r.ReservationID equals rd.ReservationID
                                join ro in db.Rooms
                                on rd.RoomID equals ro.RoomID
                                where (r.ReservationID == Convert.ToInt16(txtReservationNum.Text) || g.GuestFirstName == txtFirstName.Text || g.GuestSurName == txtSurname.Text) && (r.ReservationStatus == 'U')
                                select new { r.ReservationID, g.GuestFirstName, g.GuestSurName, rd.ReservationDetailID, r.ReservationStatus};

                    gvUnconfirmedGuest.DataSource = guests.ToList();
                    gvUnconfirmedGuest.DataBind();

                    //Clears the default values for the textboxes
                    if (txtFirstName.Text == "none")
                        txtFirstName.Text = "";
                    if (txtSurname.Text == "none")
                        txtSurname.Text = "";
                    if (txtReservationNum.Text == "0")
                        txtReservationNum.Text = "";

                    if (gvGuest.Rows.Count == 0)
                    {
                        lblErrorMessage2.Text = "No confirmed guests found";
                    }
                    else
                    {
                        lblErrorMessage2.Text = "";
                    }

                    if (gvUnconfirmedGuest.Rows.Count == 0)
                    {
                        lblErrorMessage3.Text = "No unconfirmed guests found";
                    }
                    else
                    {
                        lblErrorMessage3.Text = "";
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

                //get the discount
                ArrayList myArrList = new ArrayList();
                myArrList = App_Code.GuestDB.getGuestInformation(Convert.ToInt32(gvGuest.SelectedRow.Cells[3].Text));
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
                    lblCustomerId.Text = App_Code.GuestDB.getGuestID(Convert.ToInt32(txtShowReservationNum.Text)).ToString();
                    lblReservationDetailID.Text = gvGuest.SelectedRow.Cells[0].Text;
                }
              
            
            }
        }

        protected void btnConfirm_Click(object sender, EventArgs e)
        {
            if (gvUnconfirmedGuest.SelectedIndex == -1)
                lblErrorMessage2.Text = "You must select a guest";
            else
            {
                TreasureLandDataClassesDataContext db = new TreasureLandDataClassesDataContext();
                Reservation res = db.Reservations.Single(r=> r.ReservationID == Convert.ToInt32(gvUnconfirmedGuest.SelectedRow.Cells[3].Text));

                res.ReservationStatus = 'C';
                db.SubmitChanges();
                
                //switches to the next view
                mvLocateGuest.ActiveViewIndex = 1;

                //get the discount
                ArrayList myArrList = new ArrayList();
                myArrList = App_Code.GuestDB.getGuestInformation(Convert.ToInt32(gvUnconfirmedGuest.SelectedRow.Cells[3].Text));
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
                    lblCustomerId.Text = App_Code.GuestDB.getGuestID(Convert.ToInt32(txtShowReservationNum.Text)).ToString();
                    lblReservationDetailID.Text = gvUnconfirmedGuest.SelectedRow.Cells[0].Text;
                }


            }
        }


        /// <summary>
        /// When the check in button is pressed, the entered data is updated.  All or none of the data may be entered.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnCheckIn_Click(object sender, EventArgs e)
        {   
            //SQL command to update the guests entered information
            App_Code.GuestDB.updateGuestInformation(txtCompany.Text, txtAddress.Text, txtCity.Text, txtRegion.Text, 
                txtPostalCode.Text, txtCountry.Text, txtFax.Text, txtEmail.Text, txtComments.Text, txtIdNumber.Text, 
                txtIdCountry.Text, txtIdComments.Text, Convert.ToInt32(lblCustomerId.Text));

            //updates the roomStatus to checked in, and updates the reservationdetail to active
            App_Code.GuestDB.updateRoomStatus('C', Convert.ToInt32(txtShowRoomNum.Text));
            App_Code.GuestDB.updateReservationDetail('A', Convert.ToInt16(lblReservationDetailID.Text));
            
            //Updates the Reservation status to Active if all reservation detail status associated with the reservation are set to active
            if(App_Code.GuestDB.countConfirmedReservationDetail(Convert.ToInt32(txtShowReservationNum.Text))==0)
            {
                App_Code.GuestDB.updateReservationStatus('A', Convert.ToInt32(txtShowReservationNum.Text));
            }
            mvLocateGuest.ActiveViewIndex = 2;
        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            
            gvGuest.DataBind();
            gvUnconfirmedGuest.DataBind();
            btnConfirm.Visible = false;
            btnSelectGuest.Visible = false;

        }

 


    }
}