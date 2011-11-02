using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TreasureLand.App_Code;
using System.Security;
using System.Web.Security;

namespace TreasureLand.Clerk
{    
    public partial class WebForm7 : System.Web.UI.Page
    {
         protected void Page_Load(object sender, EventArgs e)
        {
        
        }

        /// <summary>
        /// When button is pressed, the textboxes are checked for value.
        /// If any of the textboxes are empty, default values are added.
        /// If all of the textboxes are empty, an error message is shown stating that
        /// at least one textbox must have data.
        /// When data has been properly entered, the gridview is populated with data.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnLocate_Click(object sender, EventArgs e)
        {
            
            if (txtFirstName.Text == "" && txtShowSurName.Text == "" && txtReservation.Text == "")
            {
                lblError.Text = "You must Enter information in at least one box";
            }
            else
            {
            //if there are not values entered, default values are added
            if (txtFirstName.Text == "")
                txtFirstName.Text = "none";
            if (txtSurName.Text == "")
                txtSurName.Text = "none";
            if (txtReservation.Text == "")
                txtReservation.Text = "0";
            
   
            //Gridview is populated with data
            gvGuest.DataSource = App_Code.GuestDB.LocateGuestRoom(txtFirstName.Text, txtSurName.Text, txtReservation.Text);
            gvGuest.DataBind();



            //Clears the default values for the textboxes
            if (txtFirstName.Text == "none")
                txtFirstName.Text = "";
            if (txtSurName.Text == "none")
                txtSurName.Text = "";
            if (txtReservation.Text == "0")
                txtReservation.Text = "";

            if (gvGuest.Rows.Count == 0)
            {
                lblError.Text = "No data found";
            }
            else
            {
                ddlServices.DataSource = App_Code.GuestDB.getGuestServices();
                ddlServices.DataBind();

                lblError.Text = "";
            }
            }

        }

        /// <summary>
        /// When the button is clicked, the gridview is checked to make sure that a customer is selected
        /// If nothing is selected, an error message is shown.
        /// If there is a selection, the view is changed and the textboxes are populated with the values from the textbox.
        /// The dropdown box is populated.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnNext_Click(object sender, EventArgs e)
        {
            //checks for a selection in the gridview
            if (gvGuest.SelectedIndex>-1)
            {
                //switches to the next view
                mvViewGuest.ActiveViewIndex = 1;
                //Grabs the values from the gridview and populates the textboxes with the information
                txtShowReservation.Text = gvGuest.SelectedRow.Cells[0].Text;
                txtShowFirstName.Text = gvGuest.SelectedRow.Cells[1].Text;
                txtShowSurName.Text = gvGuest.SelectedRow.Cells[2].Text;
                txtShowRoom.Text = gvGuest.SelectedRow.Cells[3].Text;
                txtTotal.Text = GuestDB.getTotal(Convert.ToInt32(gvGuest.SelectedRow.Cells[4].Text)).ToString();
                
                //gets the data for the drop down list
                ddlServices.DataSource = App_Code.GuestDB.getGuestServices();
                ddlServices.DataTextField = "BillingCategoryDescription";
                ddlServices.DataValueField = "BillingCategoryID";
                ddlServices.DataBind();


                gvRoomCost.DataSource = GuestDB.getGuestRoom(Convert.ToInt32(txtShowRoom.Text));
                gvRoomCost.DataBind();

                gvGuestServices.DataSource = GuestDB.getGuestServices(Convert.ToInt32(gvGuest.SelectedRow.Cells[4].Text));
                gvGuestServices.DataBind();

                if (Roles.IsUserInRole("Admin")==true)
                {
                    gvGuestServices.Columns[3].Visible = true;
                    gvGuestServices.Columns[4].Visible = true;
                }

                //clears the error label
                lblError.Text = "";
            }
            else
            {
                //if no customer is selected, an error is shown
                lblError.Text = "You Must Select a customer before you can continue";
            }
        }

        /// <summary>
        /// When a guess is selected, the background is changed to yellow
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void gvGuest_SelectedIndexChanged(object sender, EventArgs e)
        {
            btnNext.Visible = true;
            gvGuest.SelectedRow.BackColor = System.Drawing.Color.Yellow;
        }

        /// <summary>
        /// When the selected guest is changed, it changes the background color back to white
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void gvGuest_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
        {
            if (gvGuest.SelectedIndex>-1)
            {
                gvGuest.SelectedRow.BackColor = System.Drawing.Color.White;
            }
        }

        /// <summary>
        /// Adds the service charge to the database
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnAddService_Click(object sender, EventArgs e)
        {
        gvGuestServices.Dispose();
        GuestDB.insertGuestServices(Convert.ToDouble(txtCostofService.Text), Convert.ToInt32(ddlQuantity.SelectedValue), ddlServices.SelectedItem.Text, Convert.ToInt32(gvGuest.SelectedRow.Cells[4].Text));
        gvGuestServices.DataSource = GuestDB.getGuestServices(Convert.ToInt32(gvGuest.SelectedRow.Cells[4].Text));
        gvGuestServices.DataBind();
        txtTotal.Text = GuestDB.getTotal(Convert.ToInt32(gvGuest.SelectedRow.Cells[4].Text)).ToString();
        }

        protected void btnPrevious_Click(object sender, EventArgs e)
        {
            mvViewGuest.ActiveViewIndex = 0;
        }

        protected void gvGuestServices_RowEditing(object sender, GridViewEditEventArgs e)
        {

        }

        protected void gvGuestServices_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {

        }

        protected void gvGuestServices_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {

        }
    }
}