using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TreasureLand.DBM;

namespace TreasureLand.Maintenance
{
    public partial class MaintenanceDefault : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (gvMaintenance.Rows.Count == 0)
            {
                lblMaintenance.Text = "There are no rooms that need maintenance";
                btnReadyforGuest.Visible = false;
            }
            else
            {
                lblMaintenance.Text = "";
                btnReadyforGuest.Visible = true;
            }
        }

        protected void DataList1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (gvMaintenance.Rows.Count == 0)
            {
                lblMaintenance.Text = "There are no rooms that need maintenance";
                btnReadyforGuest.Visible = false;
            }
            else
            {
                lblMaintenance.Text = "";
                btnReadyforGuest.Visible = true;
            }
        }

        protected void btnReadyforGuest_Click(object sender, EventArgs e)
        {
            TreasureLandDataClassesDataContext db = new TreasureLandDataClassesDataContext();
            var roo = db.Rooms.Single(r => r.RoomID == Convert.ToInt16(gvMaintenance.SelectedRow.Cells[0].Text));

            roo.RoomStatus = 'A';

            db.SubmitChanges();
            gvMaintenance.DataBind();

        }
    }
}