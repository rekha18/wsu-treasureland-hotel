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
    public partial class UpdateGuestFolio : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

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
        
        protected void btnLocateGuest_Click(object sender, EventArgs e)
        {
                if (txtFirstName.Text == "" && txtSurname.Text == "" && txtEmail.Text == "")
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
                    if (txtEmail.Text == "")
                        txtEmail.Text = "none";


                    //Gridview is populated with data
                    gvGuestFolio.DataSource = App_Code.GuestDB.LocateGuestFolio(txtFirstName.Text, txtSurname.Text, txtEmail.Text);
                    gvGuestFolio.DataBind();



                    //Clears the default values for the textboxes
                    if (txtFirstName.Text == "none")
                        txtFirstName.Text = "";
                    if (txtSurname.Text == "none")
                        txtSurname.Text = "";
                    if (txtEmail.Text == "0")
                        txtEmail.Text = "";

                    if (gvGuestFolio.Rows.Count == 0)
                    {
                        lblErrorMessageNoCustomersFound.Text = "No customers found";
                    }

                }

        }


        protected void btnSelect_Click(object sender, EventArgs e)
        {
            if (gvGuestFolio.SelectedIndex == -1)
                lblErrorMustSelectGuest.Text = "You must select a guest";
            else
                mvUpdateGuestFolio.ActiveViewIndex = 1;
        }
    }
}