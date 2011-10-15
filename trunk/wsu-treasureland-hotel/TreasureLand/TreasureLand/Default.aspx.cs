using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.Linq;

namespace TreasureLand
{
    public partial class _Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
             ///This is just a test to make sure I can get some data
            TreasureLandDataClassesDataContext db = new TreasureLandDataClassesDataContext();
            var customersInDb = from c in db.Guests select c;
            int customerCount = customersInDb.Count();
            ///This is just a test to make sure I can get some data
        }
    }
}
