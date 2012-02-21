using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TreasureLand.DBM;
using Microsoft.Reporting.WebForms;

namespace TreasureLand.Clerk
{
    

    public partial class WebForm8 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {                

                txtBeginDate.Text = DateTime.Now.ToShortDateString();
                txtEndDate.Text = DateTime.Now.ToShortDateString();
            }
        }

        protected void btnCreateReport_Click(object sender, EventArgs e)
        {
            /*
            TreasureLandDataClassesDataContext db = new TreasureLandDataClassesDataContext();
            IEnumerable <reportData> ds = from l in db.LineItems
                     join rd in db.ReservationDetailBillings
                     on l.ReservationDetailBillingID equals rd.ReservationDetailBillingID
                     join m in db.MenuItems
                     on l.MenuItemID equals m.MenuItemID
                     join d in db.FoodDrinkCategories
                     on m.MenuItemID equals d.FoodDrinkCategoryID
                     where rd.ReservationDetailBillingID == Convert.ToInt16(ddlTransactions.SelectedValue)
                     select new reportData{ LineItemAmount = l.LineItemAmount, LineItemTransactionID = l.LineItemTransactionID, MenuItemName = m.MenuItemName};
            */
            if(ddlTransactions.SelectedIndex>-1)
            {
            ReportViewer1.Visible = true;
            ReportDataSource rds = new ReportDataSource("reportDatasource", sdsReport);
            

            //Resets the ReportViewer, adds the new datasource, and changes the name of the report
            
            ReportViewer1.LocalReport.DataSources.Add(rds);
            ReportViewer1.LocalReport.DisplayName = "reportDatasource";
            ReportViewer1.DataBind();
            }
        }

        protected void btnGetTransactions_Click(object sender, EventArgs e)
        {
            if(Convert.ToDateTime(txtBeginDate.Text) <= Convert.ToDateTime(txtEndDate.Text))
            {
            ddlTransactions.DataSource = ldsTransactions;
            ddlTransactions.DataValueField = "ReservationDetailBillingID";
            ddlTransactions.DataTextField = String.Format("{0:C}", "BillingAmount");
            ddlTransactions.DataBind();

            ddlTransactions.Visible = true;
            btnCreateReport.Visible = true;
            }
        }
    }
}