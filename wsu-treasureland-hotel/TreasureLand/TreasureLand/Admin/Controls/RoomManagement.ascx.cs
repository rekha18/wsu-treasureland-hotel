using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TreasureLand.DBM;
namespace TreasureLand.Admin.Controls
{
    public partial class RoomManagement : System.Web.UI.UserControl
    {
        TreasureLandDataClassesDataContext db = new TreasureLandDataClassesDataContext();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                
                ConfigureData();
            }
        }

        protected void GridView_Rooms_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                BindAll();
            }
        }

        protected void BindAll()
        {  
            GridView_Rooms.DataBind();
            DropDownList_RoomStatuses.DataBind();
            DropDownList_RoomTypes.DataBind();
        }
        protected void ConfigureData()
        {
            GridView_Rooms.DataKeyNames = new string[] { "RoomID" };
        }

        protected void Button_AddRoom_Click(object sender, EventArgs e)
        {
            MultiView_Rooms.SetActiveView(View_AddRoom);
        }

        protected void Room_Add_Click(object sender, EventArgs e)
        {
            try
            {
                short RoomTypeID = Convert.ToInt16(DropDownList_RoomTypes.SelectedValue);
                char Status = Convert.ToChar(DropDownList_RoomStatuses.SelectedValue);
                Room newRoom = new Room
                {
                    RoomNumbers = TextBox_RoomNumber.Text,
                    RoomDescription = TextBox_Description.Text,
                    RoomBedConfiguration = TextBox_BedConfig.Text,
                    RoomHandicap = CheckBox_Accessible.Checked,
                    RoomSmoking = CheckBox_Smoking.Checked,
                    HotelRoomTypeID = RoomTypeID,
                    RoomStatus = Status
                };
               
                db.Rooms.InsertOnSubmit(newRoom);
                db.SubmitChanges();
                BindAll();
                Label_StatusMsg.Text = "New room added.";
                MultiView_Rooms.SetActiveView(View_RoomsMain);
            }
            catch (Exception ex)
            {

                Label_StatusMsg.Text = "Could not add room: " + ex.Message;
            }

        }

        protected void View_AddRoom_Activate(object sender, EventArgs e)
        {
            TextBox_BedConfig.Text = string.Empty;
            TextBox_Description.Text = string.Empty;
            TextBox_RoomNumber.Text = string.Empty;
            
        }

    }
}