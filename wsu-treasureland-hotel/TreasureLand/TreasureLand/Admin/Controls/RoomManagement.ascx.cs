﻿using System;
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
                Label_StatusMsg.Text = String.Empty;
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
        }
        protected void ConfigureData()
        {
            GridView_Rooms.DataKeyNames = new string[] { "RoomID" };
        }

        protected void Button_AddRoom_Click(object sender, EventArgs e)
        {
            Label_StatusMsg.Text = "";
            MultiView_Rooms.SetActiveView(View_AddRoom);
        }

        protected void Room_Add_Click(object sender, EventArgs e)
        {
            Label_StatusMsg.Text = "";
            try
            {
                short RoomTypeID = Convert.ToInt16(ddlRoomTypes.SelectedValue);
                char Status = Convert.ToChar(ddlStatus.SelectedValue);
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
                Label_StatusMsg.Text = "Could not add room: ";
            }

        }

        protected void View_AddRoom_Activate(object sender, EventArgs e)
        {
            Label_StatusMsg.Text = "";
            TextBox_BedConfig.Text = string.Empty;
            TextBox_Description.Text = string.Empty;
            TextBox_RoomNumber.Text = string.Empty;

        }

        protected void GridView_Rooms_RowEditing(object sender, GridViewEditEventArgs e)
        {

        }

        protected void LinqDataSource_Rooms_Updated(object sender, LinqDataSourceStatusEventArgs e)
        {
            if (e.Exception != null)
            {
                Label_StatusMsg.Text = "Could not edit room: ";
                e.ExceptionHandled = true;
            }
        }

        protected void GridView_Rooms_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {

        }

        protected void GridView_Rooms_RowUpdated(object sender, GridViewUpdatedEventArgs e)
        {
            if (e.Exception != null)
            {
                Label_StatusMsg.Text = e.Exception.Message;
            }
        }

        protected void LinqDataSource_Rooms_Updating(object sender, LinqDataSourceUpdateEventArgs e)
        {
            if (e.Exception != null)
            {
                Label_StatusMsg.Text = "Could not edit room: " + e.Exception.Message;
                e.ExceptionHandled = true;
            }

        }

        protected void Button_DeleteRoom_Click(object sender, EventArgs e)
        {
            
        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            Label_StatusMsg.Text = "";
        }

        protected void GridView_Rooms_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            e.Keys.Values.AsQueryable().ToString();
            var res = from r in db.Reservations
                      join rd in db.ReservationDetails
                      on r.ReservationID equals rd.ReservationID
                      join ro in db.Rooms
                      on rd.RoomID equals ro.RoomID
                      where ro.RoomNumbers == GridView_Rooms.SelectedRow.Cells[0].Text
                      select r;

            if (res.ToList().Count != 0)
            {
                e.Cancel = true;
                Label_StatusMsg.Text = "Room has Reservations Associated with it";

            }


        }


    }
}