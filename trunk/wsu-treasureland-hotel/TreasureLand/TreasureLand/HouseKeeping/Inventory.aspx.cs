using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TreasureLand.DBM;

namespace TreasureLand.HouseKeeping
{
    public partial class WebForm3 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                txtDate.Text = System.DateTime.Now.ToShortDateString();
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
                
                //ShortTermInventoryDB.AddTransaction(Convert.ToDateTime(txtDate.Text), txtInventoryName.Text, Convert.ToInt32(ddlInventoryType.SelectedItem.Value));
                gvInventory.DataBind();
                
                if (txtInventoryName.Text == "None")
                {
                    txtInventoryName.Text = "";
                }
                
                txtInventoryName.Text = "";
                
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}