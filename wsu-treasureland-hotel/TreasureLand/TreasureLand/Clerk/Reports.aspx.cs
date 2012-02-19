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
    public class reportData
    {
        private decimal lineItemAmount;
        private int lineItemTransactionID;
        private string drinkName;
        private string menuItemName;


        public int LineItemTransactionID
        {
            get
            {
               return lineItemTransactionID;
            }
            set
            {
                lineItemTransactionID=value;
            }
        }

        public decimal LineItemAmount
        {
            get
            {
               return lineItemAmount;
            }
            set
            {
                lineItemAmount=value;
            }
        }
        public string DrinkName
        {
            get
            {
               return drinkName;
            }
            set
            {
                drinkName=value;
            }
        }
        public string MenuItemName
        {
        get
        {
        return MenuItemName;
        }   
        set
        {
        menuItemName=value;
        }
    }

    }

    public partial class WebForm8 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {                
                txtBeginDate.Text = DateTime.Now.ToShortDateString();
                txtEndDate_CalendarExtender.StartDate = DateTime.Now;
                txtEndDate.Text = txtEndDate_CalendarExtender.StartDate.Value.ToShortDateString();
            }
        }

        protected void btnCreateReport_Click(object sender, EventArgs e)
        {

            TreasureLandDataClassesDataContext db = new TreasureLandDataClassesDataContext();
            IEnumerable <reportData> ds = from l in db.LineItems
                     join rd in db.ReservationDetailBillings
                     on l.ReservationDetailBillingID equals rd.ReservationDetailBillingID
                     join m in db.MenuItems
                     on l.MenuItemID equals m.MenuItemID
                     join d in db.FoodDrinkCategories
                     on m.MenuItemID equals d.FoodDrinkCategoryID
                     where (rd.BillingItemDate >= Convert.ToDateTime(txtBeginDate.Text) &
                     rd.BillingItemDate <= Convert.ToDateTime(txtEndDate.Text))
                     select new reportData{ LineItemAmount = l.LineItemAmount, LineItemTransactionID = l.LineItemTransactionID, MenuItemName = m.MenuItemName};
           
           
            ReportDataSource rds = new ReportDataSource();
            rds.Value=ds;

            //Resets the ReportViewer, adds the new datasource, and changes the name of the report
            ReportViewer1.Reset();
            ReportViewer1.LocalReport.DataSources.Add(rds);
            ReportViewer1.LocalReport.DisplayName = "reportDatasource";
        }
    }
}