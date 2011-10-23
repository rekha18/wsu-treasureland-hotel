using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TreasureLand.Clerk
{
    /*Items that still need to be addressed.
     * Discounts need to be attached 
     * RoomTypes need to be attached
     * Select Rooms need to be enabled
     * Total Quoted Cost needs to be calculated
     * Resever button needs to be enabled and programmed
     * Add Guest needs to have check against duplicate entries
     */ 
    public partial class CreateReservation : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //Initial set up of dates for the reservation page
                lblDateToday.Text = DateTime.Today.Date.ToShortDateString();
                calDateFrom.SelectedDate = DateTime.Today.Date;
                lblDateFrom.Text = calDateFrom.SelectedDate.Date.ToShortDateString();
            }
            //Changes date based on number of days changed
            lblDateTo.Text = calDateFrom.SelectedDate.Date.AddDays(Convert.ToInt32(ddlNumberOfDays.SelectedValue)).ToShortDateString();
        }

        protected void btnLocateGuest_Click(object sender, EventArgs e)
        {
            //Locates guest in database based on the values given
            odsLocateGuest.Select();
        }

        protected void btnAddGuest_Click(object sender, EventArgs e)
        {
            //USes an linq to sql to insert a guest into the guest table
            TreasureLandDataClassesDataContext db = new TreasureLandDataClassesDataContext();
            Guest addGuest = new Guest();
            addGuest.GuestFirstName = txtFirstNameInsert.Text;
            addGuest.GuestSurName = txtSurNameInsert.Text;
            addGuest.GuestPhone = txtPhoneInsert.Text;
            db.Guests.InsertOnSubmit(addGuest);
            db.SubmitChanges();

            //Sets guest into the reservation fields
            lblResFirstName.Text = txtFirstNameInsert.Text;
            lblResSurName.Text = txtSurNameInsert.Text;
            lblResPhone.Text = txtPhoneInsert.Text;        
        }

        protected void calDateFrom_SelectionChanged(object sender, EventArgs e)
        {
            //Shows Date From in label and calculates Date To based on Date from and days stayed
            lblDateFrom.Text = calDateFrom.SelectedDate.Date.ToShortDateString();
            lblDateTo.Text = calDateFrom.SelectedDate.Date.AddDays(Convert.ToInt32(ddlNumberOfDays.SelectedValue)).ToShortDateString();
        }

        protected void btnSelectGuest_Click(object sender, EventArgs e)
        {
            //Sets selected guest into reservation fields
            lblResFirstName.Text = Convert.ToString(gvGuest.SelectedRow.Cells[1].Text);
            lblResSurName.Text = Convert.ToString(gvGuest.SelectedRow.Cells[0].Text);
            lblResPhone.Text = Convert.ToString(gvGuest.SelectedRow.Cells[2].Text);
        }

        protected void gvGuest_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Allows user to proceed to reservation only after guest is selected
            btnSelectGuest.Enabled = true;
        }
    }
}