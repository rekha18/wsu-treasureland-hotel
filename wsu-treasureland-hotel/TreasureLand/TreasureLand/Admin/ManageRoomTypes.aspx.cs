using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TreasureLand.DBM;

namespace TreasureLand.Admin
{
    public partial class ManageRoomTypes : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnAddRoomType_Click(object sender, EventArgs e)
        {
            //USes an linq to sql to insert a guest into the guest table
            TreasureLandDataClassesDataContext db = new TreasureLandDataClassesDataContext();
            HotelRoomType type = new HotelRoomType();
            type.HotelID = Convert.ToInt16(ddlHotel.SelectedValue);
            type.RoomType = txtRoomType.Text;
            type.RoomTypeDescription = txtDescription.Text;
            type.RoomTypeRackRate = Convert.ToDecimal(txtRackRate.Text);

            db.HotelRoomTypes.InsertOnSubmit(type);
            db.SubmitChanges();
            gvRoomTpyes.DataBind();
        }


    }
}