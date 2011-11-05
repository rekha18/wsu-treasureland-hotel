<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="~/Admin/Controls/RoomManagement.ascx.cs"
    Inherits="TreasureLand.Admin.Controls.RoomManagement" %>
<style type="text/css">
    .style1 {
        height: 24px;
    }
</style>
<h1>
    Room Manager
</h1>
<asp:MultiView ID="MultiView_Rooms" runat="server" ActiveViewIndex="0">
    <asp:View ID="View_RoomsMain" runat="server">
        <asp:GridView ID="GridView_Rooms" runat="server" AllowPaging="True" AllowSorting="True"
            PageSize="5" AutoGenerateColumns="False" DataSourceID="LinqDataSource_Rooms"
            BackColor="White" BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px"
            CellPadding="4" ForeColor="Black" GridLines="Vertical" DataKeyNames="RoomID"
            OnSelectedIndexChanged="GridView_Rooms_SelectedIndexChanged" ShowFooter="True">
            <AlternatingRowStyle BackColor="White" />
            <Columns>
                <asp:CommandField ButtonType="Button" ShowSelectButton="True" />
                <asp:BoundField DataField="RoomNumbers" HeaderText="Room Number" SortExpression="RoomNumbers" />
                <asp:BoundField DataField="RoomDescription" HeaderText="Description" SortExpression="RoomDescription" />
                <asp:CheckBoxField DataField="RoomSmoking" HeaderText="Smoking?" SortExpression="RoomSmoking" />
                <asp:BoundField DataField="RoomBedConfiguration" HeaderText="Bed Configuration" SortExpression="RoomBedConfiguration" />
                <asp:CheckBoxField DataField="RoomHandicap" HeaderText="Accessible?" SortExpression="RoomHandicap" />
                <asp:BoundField DataField="RoomStatus" HeaderText="Status" SortExpression="RoomStatus" />
                <asp:CommandField ShowEditButton="True" ButtonType="Button" />
            </Columns>
            <FooterStyle BackColor="#CCCC99" />
            <HeaderStyle BackColor="#6B696B" Font-Bold="True" ForeColor="White" />
            <PagerStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="Right" />
            <RowStyle BackColor="#F7F7DE" />
            <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
            <SortedAscendingCellStyle BackColor="#FBFBF2" />
            <SortedAscendingHeaderStyle BackColor="#848384" />
            <SortedDescendingCellStyle BackColor="#EAEAD3" />
            <SortedDescendingHeaderStyle BackColor="#575357" />
        </asp:GridView>
        <asp:Button runat="server" ID="Button_AddRoom" Text="New Room" OnClick="Button_AddRoom_Click" />
        <asp:Button runat="server" ID="Button_DeleteRoom" Text="Delete Room" Enabled="false" />
    </asp:View>
    <asp:View runat="server" ID="View_AddRoom" onactivate="View_AddRoom_Activate">
        <h3>
            Enter New Room Details</h3>
        <table>
            <tr>
                <td>
                    Room Number
                </td>
                <td align="right">
                    <asp:TextBox ID="TextBox_RoomNumber" runat="server" 
                        ValidationGroup="VG_NewRoomData"></asp:TextBox>
                </td>
                <td>
                    <asp:CheckBox ID="CheckBox_Smoking" runat="server" Text="Smoking" />
                    &nbsp;
                </td>
                <td>
                    <asp:CheckBox ID="CheckBox_Accessible" runat="server" Text="Accessible?" />
                </td>
                <td>
                    <asp:RequiredFieldValidator ID="RFV_RoomNumber" runat="server" forecolor="Red"
                        ErrorMessage="Please enter a room number." SetFocusOnError="True" 
                        ControlToValidate="TextBox_RoomNumber" ValidationGroup="VG_NewRoomData"></asp:RequiredFieldValidator>
                    </td>
            </tr>
            <tr>
                <td>
                    Type
                </td>
                <td>
                 <!--   <asp:DropDownList ID="DropDownList_RoomTypes" runat="server" DataSourceID="LinqDataSource_RoomTypes"
                        DataTextField="RoomType" DataValueField="HotelRoomTypeID" 
                        ValidationGroup="VG_NewRoomData">
                    </asp:DropDownList>-->
                </td>
                <td>
                    Status
                </td>
                <td><!--
                    <asp:DropDownList ID="DropDownList_RoomStatuses" runat="server" DataSourceID="LinqDataSource_Statuses"
                        DataTextField="Description" DataValueField="Status" 
                        ValidationGroup="VG_NewRoomData">
                    </asp:DropDownList>-->
                </td>
            </tr>
            <tr>
                <td colspan="5" class="style1">
                    Bed Configuration
                </td>
            </tr>
            <tr>
                <td colspan="5">
                    <asp:TextBox ID="TextBox_BedConfig" runat="server" Width="95%" 
                        ValidationGroup="VG_NewRoomData"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td colspan="5">
                    Description
                </td>
            </tr>
            <tr>
                <td colspan="5">
                    <asp:TextBox ID="TextBox_Description" runat="server" Width="95%" 
                        ValidationGroup="VG_NewRoomData"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td colspan="5" align="right">
                    <asp:Button ID="Room_Add" runat="server" Text="Add" OnClick="Room_Add_Click" 
                        CommandName="Insert" ValidationGroup="VG_NewRoomData" />
                </td>
            </tr>
        </table>
    </asp:View>
</asp:MultiView>
<asp:Panel ID="Panel_DataSourceBox" runat ="server" Visible="false">
<asp:Label ID="Label_StatusMsg" runat="server" ForeColor="Red" Font-Size="Large" />
<asp:LinqDataSource ID="LinqDataSource_Rooms" runat="server" ContextTypeName="TreasureLand.DBM.TreasureLandDataClassesDataContext"
    TableName="Rooms" EnableDelete="True" EnableInsert="True" EnableUpdate="True">
</asp:LinqDataSource>
<asp:LinqDataSource ID="LinqDataSource_RoomTypes" runat="server" ContextTypeName="TreasureLand.DBM.TreasureLandDataClassesDataContext"
    EntityTypeName="" TableName="HotelRoomTypes">
</asp:LinqDataSource>
<asp:LinqDataSource ID="LinqDataSource_Statuses" runat="server" ContextTypeName="TreasureLand.DBM.TreasureLandDataClassesDataContext"
    EntityTypeName="" Select="new (Status, Description)" TableName="RoomStatus">
</asp:LinqDataSource>
</asp:Panel>