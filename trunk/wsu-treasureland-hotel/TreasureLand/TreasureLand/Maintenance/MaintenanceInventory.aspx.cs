using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TreasureLand.App_Code;

namespace TreasureLand.Maintenance
{
    public partial class MaintenanceInventory : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                txtPurchaseDate.Text = System.DateTime.Now.ToShortDateString();
            }
        }

        protected void btnAddExpense_Click(object sender, EventArgs e)
        {
            try
            {

                if (txtLongTermName.Text == "")
                {
                    txtLongTermName.Text = "None";
                }
             
                LongTermAssetDB.AddTransaction(txtLongTermName.Text, txtLongTermLocation.Text, Convert.ToDecimal(txtCost.Text), Convert.ToBoolean(cbLongTermInUse), Convert.ToDateTime(txtPurchaseDate.Text));
                gvLongTermAsset.DataBind();
                
                if (txtLongTermName.Text == "None")
                {
                    txtLongTermName.Text = "";
                }
                
                txtLongTermName.Text = "";
                
                txtCost.Text = "";
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}