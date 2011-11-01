using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TreasureLand.Admin
{
    public partial class ManageStatuses : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnAddStatuses_Click(object sender, EventArgs e)
        {
            TreasureLandDataClassesDataContext db = new TreasureLandDataClassesDataContext();
            RoomStatus status = new RoomStatus();
            status.Description = txtDescription.Text;
            db.RoomStatus.InsertOnSubmit(status);
            db.SubmitChanges();
        }
    }
}