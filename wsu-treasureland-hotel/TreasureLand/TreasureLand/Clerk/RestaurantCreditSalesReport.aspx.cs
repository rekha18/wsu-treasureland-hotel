using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.Reporting.WebForms;
using System.Configuration;

namespace TreasureLand.Clerk
{
    public partial class WebForm5 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnGetTransactions_Click(object sender, EventArgs e)
        {

            if (Convert.ToDateTime(txtBeginDate.Text) == Convert.ToDateTime(txtEndDate.Text))
            {
                ReportViewer1.LocalReport.DataSources.Clear();
                sdsCreditSalesDateRange = new SqlDataSource(ConfigurationManager.ConnectionStrings["HotelDB"].ConnectionString, getDateRange()); 
                ReportDataSource rds = new ReportDataSource("reportDatasource", sdsCreditSalesDay);
                ReportViewer1.LocalReport.DataSources.Clear();
                ReportViewer1.LocalReport.DataSources.Add(rds);
                ReportViewer1.LocalReport.DisplayName = "reportDatasource";
                ReportViewer1.DataBind();
                ReportViewer1.Visible = true;
            }
            else if (Convert.ToDateTime(txtBeginDate.Text) <= Convert.ToDateTime(txtEndDate.Text))
            {
                ReportViewer1.LocalReport.DataSources.Clear(); 
                sdsCreditSalesDateRange = new SqlDataSource(ConfigurationManager.ConnectionStrings["HotelDB"].ConnectionString, getDateRange()); 
                
                ReportDataSource rds = new ReportDataSource("reportDatasource", sdsCreditSalesDateRange);
                ReportViewer1.LocalReport.DataSources.Add(rds);
                ReportViewer1.LocalReport.DisplayName = "reportDatasource";
                ReportViewer1.DataBind();
                ReportViewer1.Visible = true;
            }
            else
            {
                ReportViewer1.Visible = false;
            }
        }

        private string getSingleDay()
        {
            // DateTime date1= Convert.ToDateTime(txtBeginDate.Text);

            // DateTime date2 = Convert.ToDateTime(txtEndDate.Text);
            DateTime date1 = DateTime.Parse(txtBeginDate.Text, System.Globalization.CultureInfo.GetCultureInfo("en-gb"));
            DateTime date2 = DateTime.Parse(txtEndDate.Text, System.Globalization.CultureInfo.GetCultureInfo("en-gb"));
            string beginDate = (date1).ToString("yyyy-MM-dd");
            string endDate = (date2).ToString("yyyy-MM-dd");
            //string beginDate = date1.ToUniversalTime().ToShortDateString();
            //string endDate = date1.ToUniversalTime().ToShortDateString();
            string query = "SELECT rdb.BillingItemDate, rdb.ReservationDetailBillingID, rdb.BillingDescription, rdb.BillingAmount, " +
              "rdb.BillingItemQty, rdb.Comments, rdb.TransEmployee, u.UserName, " +
              "li.lineItemTransactionID, li.LineItemAmount, me.MenuItemName " +
              "FROM ReservationDetailBilling rdb " +
                "JOIN LineItem li " +
                "ON rdb.ReservationDetailBillingID = li.ReservationDetailBillingID " +
                "JOIN MenuItem me " +
                "ON me.MenuItemID = li.MenuItemID " +
                "JOIN ReservationDetailBilling rd " +
                "ON rd.ReservationDetailBillingID = li.ReservationDetailBillingID " +
                "LEFT JOIN dbo.aspnet_Membership m " +
                "ON m.Pin = rdb.TransEmployee " +
                "JOIN dbo.aspnet_Users u " +
                "ON u.UserID = m.UserID " +
                "WHERE rdb.ReservationDetailID is NULL " +
                "AND  (rdb.BillingItemDate between DATEDIFF(dd, 1, '" + beginDate + "') AND DATEADD(dd, 1, '" + endDate + "')) " +
                "ORDER BY me.MenuItemName";
            return query;
        }

        private string getDateRange()
        {
            DateTime date1 = DateTime.Parse(txtBeginDate.Text, System.Globalization.CultureInfo.GetCultureInfo("en-gb"));
            DateTime date2 = DateTime.Parse(txtEndDate.Text, System.Globalization.CultureInfo.GetCultureInfo("en-gb"));
            string beginDate = (date1).ToString("yyyy-MM-dd");
            string endDate = (date2).ToString("yyyy-MM-dd");
            string query = "SELECT rdb.BillingItemDate, rdb.ReservationDetailBillingID, rdb.BillingDescription, rdb.BillingAmount, " +
              "rdb.BillingItemQty, rdb.Comments, rdb.TransEmployee, u.UserName, " +
              "li.lineItemTransactionID, li.LineItemAmount, me.MenuItemName " +
              "FROM ReservationDetailBilling rdb " +
                "JOIN LineItem li " +
                "ON rdb.ReservationDetailBillingID = li.ReservationDetailBillingID " +
                "JOIN MenuItem me " +
                "ON me.MenuItemID = li.MenuItemID " +
                "JOIN ReservationDetailBilling rd " +
                "ON rd.ReservationDetailBillingID = li.ReservationDetailBillingID " +
                "LEFT JOIN dbo.aspnet_Membership m " +
                "ON m.Pin = rdb.TransEmployee " +
                "JOIN dbo.aspnet_Users u " +
                "ON u.UserID = m.UserID " +
                "WHERE rdb.ReservationDetailID is NULL " +
                "AND  (rdb.BillingItemDate between '" + beginDate + "' AND '" + endDate + "') " +
                "ORDER BY me.MenuItemName";
            return query;
        }
        



        protected void sdsCreditSalesDateRange_Selecting(object sender, SqlDataSourceSelectingEventArgs e)
        {
            //DateTime ukBeginDateTime = DateTime.Parse(txtBeginDate.Text, System.Globalization.CultureInfo.GetCultureInfo("en-gb"));
            //DateTime ukEndDateTime = DateTime.Parse(txtEndDate.Text, System.Globalization.CultureInfo.GetCultureInfo("en-gb"));

            //txtBeginDate.Text = (ukBeginDateTime).ToString("YYYY-MM-DD");
            
            //txtEndDate.Text = (ukEndDateTime).ToString("YYYY-MM-DD");
            
        }


        protected void sdsCreditSalesDateRange_Selected(object sender, SqlDataSourceStatusEventArgs e)
        {
        }
    }
}