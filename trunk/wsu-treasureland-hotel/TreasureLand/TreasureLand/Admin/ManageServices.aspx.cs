using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TreasureLand.Admin
{
    public partial class ManageServices : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnAddBilling_Click(object sender, EventArgs e)
        {
            //Insert Values into discounts table using linq
            TreasureLandDataClassesDataContext db = new TreasureLandDataClassesDataContext();
            BillingCategory addBilling = new BillingCategory();
            addBilling.BillingCategoryDescription = txtDesciption.Text;
            
            if (cbTaxable.Checked)
            {
                addBilling.BillingCategoryTaxable = true;
            }
            else
            {
                addBilling.BillingCategoryTaxable = false;
            }
            db.BillingCategories.InsertOnSubmit(addBilling);
            db.SubmitChanges();
        }
    }
}