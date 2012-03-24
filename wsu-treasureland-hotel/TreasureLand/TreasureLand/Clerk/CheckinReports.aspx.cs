using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.Reporting.WebForms;

namespace TreasureLand.Clerk
{
    public partial class WebForm2 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnGetTransactions_Click(object sender, EventArgs e)
        {
            if (Convert.ToDateTime(txtBeginDate.Text) == Convert.ToDateTime(txtEndDate.Text))
            {
                ReportDataSource rds = new ReportDataSource("reportDatasource", sdsCheckIn);
                ReportViewer1.LocalReport.DataSources.Clear();
                ReportViewer1.LocalReport.DataSources.Add(rds);
                ReportViewer1.LocalReport.DisplayName = "reportDatasource";
                ReportViewer1.DataBind();
            }
            else if (Convert.ToDateTime(txtBeginDate.Text) <= Convert.ToDateTime(txtEndDate.Text))
            {
                ReportDataSource rds = new ReportDataSource("reportDatasource", sdsCheckInBetween);
                ReportViewer1.LocalReport.DataSources.Add(rds);
                ReportViewer1.LocalReport.DisplayName = "reportDatasource";
                ReportViewer1.DataBind();
            }
            else 
            { 
            }
        }
    }
}