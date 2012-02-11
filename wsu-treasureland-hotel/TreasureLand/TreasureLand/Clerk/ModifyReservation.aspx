<%@ Page Title="" Language="C#" MasterPageFile="~/Clerk/ClerkMasterPage.master" AutoEventWireup="true" CodeBehind="ModifyReservation.aspx.cs" Inherits="TreasureLand.Clerk.ModifyReservation" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <p>Please select a room to change the currently chosen room to.<br /></p>
    <p>Select a room type: 
        <asp:DropDownList ID="ddlRoomTypes" runat="server" AutoPostBack="True" 
            DataSourceID="ldsRoomTypes" DataTextField="RoomType" 
            DataValueField="HotelRoomTypeID" 
            onselectedindexchanged="ddlRoomTypes_SelectedIndexChanged">
        </asp:DropDownList>
        <asp:LinqDataSource ID="ldsRoomTypes" runat="server" 
            ContextTypeName="TreasureLand.DBM.TreasureLandDataClassesDataContext" 
            EntityTypeName="" Select="new (RoomType, HotelRoomTypeID)" 
            TableName="HotelRoomTypes">
        </asp:LinqDataSource>
    </p>
    <asp:GridView ID="gvOpenRooms" runat="server" AutoGenerateColumns="False" 
        DataSourceID="sdsOpenRooms" AllowPaging="True" 
        onpageindexchanged="gvOpenRooms_PageIndexChanged" 
        onselectedindexchanged="gvOpenRooms_SelectedIndexChanged">
        <Columns>
            <asp:BoundField DataField="RoomID" HeaderText="Room ID" 
                SortExpression="RoomID" />
            <asp:BoundField DataField="RoomNumbers" HeaderText="Room Number" 
                SortExpression="RoomNumbers" />
            <asp:CommandField ButtonType="Button" ShowSelectButton="True" />
        </Columns>
        <SelectedRowStyle BackColor="Yellow" />
    </asp:GridView>
    <!-- A SQL data source is necessary here due to the complex operation of comparing
    all records to ensure that a room is open for the specified date range -->
    <asp:SqlDataSource ID="sdsOpenRooms" runat="server" 
        ConnectionString="<%$ ConnectionStrings:TreasurelandDB %>" SelectCommand="SELECT RoomID, RoomNumbers FROM Room
   WHERE RoomID != 
   (
      SELECT r.RoomID FROM Room r
         INNER JOIN HotelRoomType hrt ON hrt.HotelRoomTypeID = r.HotelRoomTypeID 
         INNER JOIN ReservationDetail rd ON rd.RoomID = r.RoomID 
         INNER JOIN Reservation res ON res.ReservationID = rd.ReservationID
         WHERE (@StartDate &lt;= (DATEADD(day, rd.Nights, res.ReservationDate)) AND
               (DATEADD(day, @Nights, @StartDate)) &gt;= res.ReservationDate )
   )
   AND HotelRoomTypeID = @HotelRoomType
   AND RoomStatus != 'M'
   ORDER BY RoomID">
        <SelectParameters>
            <asp:SessionParameter Name="StartDate" SessionField="StartDate" />
            <asp:SessionParameter Name="Nights" SessionField="Nights" />
            <asp:ControlParameter ControlID="ddlRoomTypes" Name="HotelRoomType" 
                PropertyName="SelectedValue" />
        </SelectParameters>
    </asp:SqlDataSource>
    <asp:CheckBox ID="cbRemoveDiscount" runat="server" Text="Check if guest requested the room change." />
    <p style="color:Red; margin: 0px; padding: 2px;">Checking will remove discounts and apply the standard room rate.</p>
    <br />
    <span style="margin: 0 0 0 300px">
        <asp:Button ID="btnSelect" runat="server" Text="Change Room" 
        Enabled="false" onclick="btnSelect_Click" 
        PostBackUrl="~/Clerk/UpdateReservation.aspx" /></span>
</asp:Content>
