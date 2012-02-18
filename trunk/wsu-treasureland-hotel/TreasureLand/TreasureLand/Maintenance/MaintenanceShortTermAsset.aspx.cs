using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TreasureLand.App_Code;

namespace TreasureLand.Maintenance
{
    public partial class MaintenanceShortTermAsset : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

            }
        }

        protected void btnAddExpense_Click(object sender, EventArgs e)
        {
            try
            {

                if (txtInventoryName.Text == "")
                {
                    txtInventoryName.Text = "None";
                }

                if (txtTotalQuantity.Text == "")
                {
                    txtTotalQuantity.Text = "None";
                }

                ShortTermInventoryDB.AddTransaction(txtInventoryName.Text, Convert.ToInt16(txtTotalQuantity.Text), 2);
                gvInventory.DataBind();

                if (txtInventoryName.Text == "None")
                {
                    txtInventoryName.Text = "";
                }

                if (txtTotalQuantity.Text == "None")
                {
                    txtTotalQuantity.Text = "";
                }

                txtInventoryName.Text = "";
                txtTotalQuantity.Text = "";
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}