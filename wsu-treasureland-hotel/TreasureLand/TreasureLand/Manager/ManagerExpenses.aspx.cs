using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TreasureLand.App_Code;

namespace TreasureLand.Manager
{
    public partial class ManagerExpenses : System.Web.UI.Page
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

                if (txtComments.Text == "")
                {
                    txtComments.Text = "None";
                }
                ExpenseDB.AddTransaction(Convert.ToDateTime(txtDate.Text), Convert.ToDecimal(txtCost.Text), Convert.ToInt32(ddlExpenseType.SelectedItem.Value), txtComments.Text);
                gvExpenses.DataBind();
                if (txtComments.Text == "None")
                {
                    txtComments.Text = "";
                }
                txtComments.Text = "";
                txtCost.Text = "";
            }
            catch (Exception )
            {
                
                throw ;
            }
        }
    }
}