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
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            check();
        }

        protected void btnReadyforGuest_Click(object sender, EventArgs e)
        {
             lblHousekeeping.Text = "";
             btnReadyforGuest.Visible = true;
             btnNeedsMaintenance.Visible = true;
             TreasureLandDataClassesDataContext db = new TreasureLandDataClassesDataContext();
             var roo = db.Rooms.Single(r => r.RoomID == Convert.ToInt16(gvHouseKeeping.SelectedRow.Cells[0].Text));

             roo.RoomStatus = 'A';

             db.SubmitChanges();
             gvHouseKeeping.DataBind();
             check();
        }

        protected void btnNeedsMaintenance_Click(object sender, EventArgs e)
        {

            TreasureLandDataClassesDataContext db = new TreasureLandDataClassesDataContext();
            var roo = db.Rooms.Single(r => r.RoomID == Convert.ToInt16(gvHouseKeeping.SelectedRow.Cells[0].Text));

            roo.RoomStatus = 'M';

            db.SubmitChanges();
            gvHouseKeeping.DataBind();
            check();
        }


        protected void check()
        {
            if (gvHouseKeeping.Rows.Count == 0)
            {
                lblHousekeeping.Text = "There are no rooms that need housekeeping";
                btnReadyforGuest.Visible = false;
                btnNeedsMaintenance.Visible = false;
            }
            else
            {
                lblHousekeeping.Text = "";
                btnReadyforGuest.Visible = true;
                btnNeedsMaintenance.Visible = true;
            }
            gvHouseKeeping.SelectRow(0);
        }

        protected void gvHouseKeeping_SelectedIndexChanged(object sender, EventArgs e)
        {
              
        }
    }
}