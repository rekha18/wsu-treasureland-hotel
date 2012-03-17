using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TreasureLand.DBM;

namespace TreasureLand.Manager
{
    public partial class Collections : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

            }
        }

        protected void gvCollections_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        protected void gvCollections_RowUpdating(object sender, GridViewUpdateEventArgs e)
        { 
            int collectionID = Convert.ToInt32(gvCollections.Rows[e.RowIndex].Cells[0].Text);
           TextBox txtamount = (TextBox)(gvCollections.Rows[e.RowIndex].FindControl("Textbox1"));


           decimal amount = Convert.ToDecimal(txtamount.Text);
            TreasureLandDataClassesDataContext db = new TreasureLandDataClassesDataContext();
            Collection updateCollection = db.Collections.Single(c => c.CollectionsID == collectionID);
            updateCollection.CollectionsAmountOwed = amount;
            db.SubmitChanges();
            gvCollections.DataBind();
        }

    }
}