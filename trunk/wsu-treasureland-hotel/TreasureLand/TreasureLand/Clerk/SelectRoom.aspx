<%@ Page Title="Select a room" Language="C#" MasterPageFile="~/Clerk/ClerkMasterPage.master" AutoEventWireup="true" CodeBehind="SelectRoom.aspx.cs" Inherits="TreasureLand.Clerk.SelectRoom" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    
    <p>All open rooms for the specified date range are shown below. Please select<br />
    one or more rooms.</p>
    <p>Select a room type: 
        <asp:DropDownList ID="ddlRoomTypes" runat="server" AutoPostBack="True" 
            DataSourceID="ldsRoomTypes" DataTextField="RoomType" 
            DataValueField="HotelRoomTypeID">
        </asp:DropDownList>
        <asp:LinqDataSource ID="ldsRoomTypes" runat="server" 
            ContextTypeName="TreasureLand.DBM.TreasureLandDataClassesDataContext" 
            EntityTypeName="" Select="new (RoomType, HotelRoomTypeID)" 
            TableName="HotelRoomTypes">
        </asp:LinqDataSource>
    </p>
    <asp:GridView ID="gvOpenRooms" runat="server" AutoGenerateColumns="False" 
        DataSourceID="sdsOpenRooms" AllowPaging="True" 
        ondatabound="gvOpenRooms_DataBound" 
        onpageindexchanged="gvOpenRooms_PageIndexChanged">
        <Columns>
            <asp:BoundField DataField="RoomID" HeaderText="Room ID" 
                SortExpression="RoomID" />
            <asp:BoundField DataField="RoomNumbers" HeaderText="Room Number" 
                SortExpression="RoomNumbers" />
            <asp:TemplateField HeaderText="Selected Rooms" ShowHeader="False">
                <ItemTemplate>
                    <asp:CheckBox ID="cbSelected" runat="server" AutoPostBack="True" 
                        oncheckedchanged="cbSelected_CheckedChanged" />
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
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
    <table>
        <tr>
            <td style="width: 300px">Rooms selected: 
                <asp:Label ID="lblTotalRooms" runat="server" Text="Label" ForeColor="Red">0</asp:Label></td>
            <td><asp:Button ID="btnSelect" runat="server" Text="Select" Enabled="False" 
                    onclick="btnSelect_Click" PostBackUrl="~/Clerk/CreateReservation.aspx" /></td>
        </tr>
    </table>
    <p>Selecting a room for 
        <asp:Literal ID="lblStartDate" runat="server"></asp:Literal>
       &nbsp;for 
        <asp:Literal ID="lNights" runat="server"></asp:Literal>&nbsp;night(s).</p>
    
</asp:Content>
