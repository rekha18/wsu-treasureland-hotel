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
using TreasureLand.App_Code;
using TreasureLand.DBM;

namespace TreasureLand.Clerk
{
    public partial class UpdateGuestFolio : System.Web.UI.Page
    {
        protected void gvGuestFolio_SelectedIndexChanged(object sender, EventArgs e)
        {
            btnSelect.Visible = true;
            gvGuestFolio.SelectedRow.BackColor = System.Drawing.Color.Yellow;
        }

        protected void gvGuestFolio_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
        {
            if (gvGuestFolio.SelectedIndex > -1)
            {
                gvGuestFolio.SelectedRow.BackColor = System.Drawing.Color.White;
            }
        }

        /// <summary>
        /// When the Locate button is pressed, the textboxes are checked for input.
        /// If no information in inputed, an error message is shown.  Otherwise 
        /// the gridview is displayed with the retreived information
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnLocateGuest_Click(object sender, EventArgs e)
        {
                if (txtFirstName.Text == "" && txtSurname.Text == "" && txtPhoneNumber.Text == "")
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
                    if (txtPhoneNumber.Text == "")
                        txtPhoneNumber.Text = "0000000";

                    //Gridview is populated with data
                    gvGuestFolio.DataSource = App_Code.GuestDB.LocateGuestFolio(txtFirstName.Text, txtSurname.Text, (txtPhoneNumber.Text));
                    gvGuestFolio.DataBind();

                    //Clears the default values for the textboxes
                    if (txtFirstName.Text == "none")
                        txtFirstName.Text = "";
                    if (txtSurname.Text == "none")
                        txtSurname.Text = "";
                    if (txtPhoneNumber.Text == "0000000")
                        txtPhoneNumber.Text = "";

                    if (gvGuestFolio.Rows.Count == 0)
                    {
                        lblErrorMessageNoCustomersFound.Text = "No customers found";
                    }
                    else
                    {
                        lblErrorMessageNoCustomersFound.Text = "";
                    }
                }

        }

        protected void btnSelect_Click(object sender, EventArgs e)
        {
            if (gvGuestFolio.SelectedIndex == -1)
                lblErrorMustSelectGuest.Text = "You must select a guest";
            else
            {
                mvUpdateGuestFolio.ActiveViewIndex = 1;
                updateGuestBoxes();
            }
        }

        /// <summary>
        /// Updates the textboxes that contain the guest information
        /// </summary>
        private void updateGuestBoxes()
        {
            ArrayList myArrList = new ArrayList();
            myArrList = App_Code.GuestDB.getCustomerFolio(Convert.ToInt32(gvGuestFolio.SelectedRow.Cells[0].Text));
            //if there are no items in the arrayList then there is no discount
            if (myArrList.Count == 0)
            {
            }
            else
            {
                App_Code.Guest currentGuest = new App_Code.Guest();
                txtSalutation.Text = myArrList[0].ToString();
                txtShowFirstName.Text = myArrList[1].ToString();
                txtShowSurname.Text = myArrList[2].ToString();
                txtAddress.Text = myArrList[3].ToString();
                txtCity.Text = myArrList[4].ToString();
                txtState.Text = myArrList[5].ToString();
                txtPostalCode.Text = myArrList[6].ToString();
                txtCountry.Text = myArrList[7].ToString();
                txtPhone.Text = myArrList[8].ToString();
                txtEmail.Text = myArrList[9].ToString();
                txtGuestID.Text = myArrList[10].ToString();
                txtIssueCountry.Text = myArrList[11].ToString();
                txtComments.Text = myArrList[12].ToString();
            }
            
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnUpdate_Click(object sender, EventArgs e)
        {

            try
            {
                TreasureLandDataClassesDataContext db = new TreasureLandDataClassesDataContext();
                TreasureLand.DBM.Guest guest = new TreasureLand.DBM.Guest();
                var query = from guests in db.Guests
                            where guests.GuestID == Convert.ToInt32(gvGuestFolio.SelectedRow.Cells[0].Text)
                            select guests;

                foreach (var guests in query)
                {
                    guests.GuestSalutation = txtSalutation.Text;
                    guests.GuestSurName = txtShowSurname.Text;
                    guests.GuestFirstName = txtShowFirstName.Text;
                    guests.GuestAddress = txtAddress.Text;
                    guests.GuestCity = txtCity.Text;
                    guests.GuestRegion = txtState.Text;
                    guests.GuestCountry = txtCountry.Text;
                    guests.GuestEmail = txtEmail.Text;
                    guests.GuestPhone = txtPhone.Text;
                    guests.GuestPostalCode = txtPostalCode.Text;
                    guests.GuestComments = txtComments.Text;
                    guests.GuestIDIssueCountry = txtIssueCountry.Text;
                    guests.GuestID = (short)Convert.ToInt32(gvGuestFolio.SelectedRow.Cells[0].Text);
                    guests.GuestIDNumber = txtGuestID.Text;
                }
                db.SubmitChanges();  

                updateGuestBoxes();

                lblError.Text = "Updated successfully";

            }
            catch (Exception)
            {
                
                throw;
            }
        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            lblError.Text = "";
            gvGuestFolio.DataBind();
        }
    }
}