<%@ Page Title="Select a room" Language="C#" MasterPageFile="~/Clerk/ClerkMasterPage.master" AutoEventWireup="true" CodeBehind="SelectRoom.aspx.cs" Inherits="TreasureLand.Clerk.SelectRoom" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    
    <p>All open rooms for the specified date range are shown below. Please select<br />
    one or more rooms.</p>
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
    <table>
        <tr>
            <td valign="top">
                <asp:GridView ID="gvOpenRooms" runat="server" AutoGenerateColumns="False" 
                ondatabound="gvOpenRooms_DataBound" 
                onpageindexchanged="gvOpenRooms_PageIndexChanged" DataSourceID="sdsOpenRooms" 
                    AllowPaging="True" onprerender="gvOpenRooms_PreRender">
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
            </asp:GridView></td>
            <td valign="top"><!--<p><strong>Selected Rooms:</strong></p>
                <asp:PlaceHolder ID="phSelectedRoomsList" runat="server" 
                    onprerender="phSelectedRoomedsList_PreRender" 
                    oninit="phSelectedRoomedsList_PreRender" 
                    ></asp:PlaceHolder>
                <asp:Button ID="btnUpdateCheckedRoomsList" Visible="false" Text="Update Rooms"
                    OnClick="btnUpdateCheckedRoomsList_OnClick" runat="server"/>-->
                    <p style="text-align:center;">Selected Rooms</p>
                <asp:GridView ID="gvSelectedRooms" runat="server" AutoGenerateColumns="False">
                    <Columns>
                        <asp:BoundField DataField="RoomNumber" HeaderText="Room Number" 
                            SortExpression="RoomNumber" />
                        <asp:BoundField DataField="RoomType" HeaderText="Room Type" 
                            SortExpression="RoomType" />
                        <asp:TemplateField HeaderText="Deselect" SortExpression="Deselect">
                            <ItemTemplate>
                                <asp:CheckBox ID="cbDeselect" runat="server" AutoPostBack="True" 
                                    Checked="True" oncheckedchanged="cbDeselect_CheckedChanged" />
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
                            </EditItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </td>
        </tr>
    </table>
    <asp:Label ID="lblNoOpenRooms" runat="server" ForeColor="Red" />
    
    <!-- A SQL data source is necessary here due to the complex operation of comparing
    all records to ensure that a room is open for the specified date range -->
    <asp:SqlDataSource ID="sdsOpenRooms" runat="server" 
        ConnectionString="<%$ ConnectionStrings:TreasurelandDB %>" SelectCommand="SELECT RoomID, RoomNumbers FROM Room
   WHERE RoomID NOT IN 
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
            <td><asp:Button ID="btnSelect" runat="server" Text="Select Room(s)" Enabled="False" 
                    onclick="btnSelect_Click" /></td>
        </tr>
    </table>
    <p>Selecting a room for 
        <asp:Literal ID="lblStartDate" runat="server"></asp:Literal>
       &nbsp;for 
        <asp:Literal ID="lNights" runat="server"></asp:Literal>&nbsp;night(s).</p>
</asp:Content>
