using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TreasureLand.DBM;

namespace TreasureLand.Admin
{
    public partial class ManageDiscounts : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnInsertDiscount_Click(object sender, EventArgs e)
        {
            //Insert Values into discounts table using linq
            TreasureLandDataClassesDataContext db = new TreasureLandDataClassesDataContext();
            Discount addDiscount = new Discount();
            addDiscount.DiscountDescription = txtDiscountDescription.Text;
            addDiscount.DiscountExpiration = Convert.ToDateTime(txtDiscountExpiration.Text);
            addDiscount.DiscountRules = txtDiscountRules.Text;
            addDiscount.DiscountAmount = Convert.ToDecimal(txtDiscountAmount.Text);

            if (cbIsPercentage.Checked)
            {
                addDiscount.IsPrecentage = true;
            }
            else
            {
                addDiscount.IsPrecentage = false;
            }
            db.Discounts.InsertOnSubmit(addDiscount);
            db.SubmitChanges();
            GridView1.DataBind();
        }
    }
}