<%@ Page Title="" Language="C#" MasterPageFile="~/HouseKeeping/HouseKeepingMasterPage.master" AutoEventWireup="true" CodeBehind="HousekeepingDefault.aspx.cs" Inherits="TreasureLand.HouseKeeping.WebForm1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div> 
        <table style="width:100%;">
            <tr>
                <td align="center" style="width: 494px">
                    <asp:GridView ID="gvHouseKeeping" runat="server" AllowPaging="True" 
                        AllowSorting="True" AutoGenerateColumns="False" BackColor="White" 
                        BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px" CellPadding="3" 
                        DataSourceID="LinqDataSource1" ForeColor="Black" GridLines="Vertical" 
                        onselectedindexchanged="gvHouseKeeping_SelectedIndexChanged" 
                        SelectedIndex="0">
                        <AlternatingRowStyle BackColor="#CCCCCC" />
                        <Columns>
                            <asp:BoundField DataField="RoomID" HeaderText="RoomID" ReadOnly="True" 
                                SortExpression="RoomID" />
                            <asp:BoundField DataField="RoomNumbers" HeaderText="RoomNumbers" 
                                ReadOnly="True" SortExpression="RoomNumbers" />
                            <asp:BoundField DataField="RoomDescription" HeaderText="RoomDescription" 
                                ReadOnly="True" SortExpression="RoomDescription" />
                            <asp:TemplateField HeaderText="RoomStatus" SortExpression="RoomStatus">
                                <EditItemTemplate>
                                    <asp:DropDownList ID="DropDownList1" runat="server" 
                                        DataSourceID="LinqDataSource1" DataTextField="Description" 
                                        DataValueField="Status" SelectedValue='<%# Bind("RoomStatus") %>'>
                                    </asp:DropDownList>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="Label1" runat="server" Text='<%# Bind("RoomStatus") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="RoomBedConfiguration" 
                                HeaderText="RoomBedConfiguration" ReadOnly="True" 
                                SortExpression="RoomBedConfiguration" Visible="False" />
                            <asp:CommandField ButtonType="Button" ShowSelectButton="True" />
                        </Columns>
                        <FooterStyle BackColor="#CCCCCC" />
                        <HeaderStyle BackColor="Black" BorderColor="#6600FF" BorderStyle="Groove" 
                            BorderWidth="3px" Font-Bold="True" ForeColor="White" />
                        <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
                        <RowStyle BorderColor="Blue" BorderStyle="Groove" BorderWidth="3px" />
                        <SelectedRowStyle BackColor="#000099" Font-Bold="True" ForeColor="White" />
                        <SortedAscendingCellStyle BackColor="#F1F1F1" />
                        <SortedAscendingHeaderStyle BackColor="#808080" />
                        <SortedDescendingCellStyle BackColor="#CAC9C9" />
                        <SortedDescendingHeaderStyle BackColor="#383838" />
                    </asp:GridView>
                </td>
                <td>
                    <asp:LinqDataSource ID="LinqDataSource1" runat="server" 
                        ContextTypeName="TreasureLand.DBM.TreasureLandDataClassesDataContext" 
                        EntityTypeName="" 
                        Select="new (RoomID, RoomNumbers, RoomDescription, RoomStatus, HotelRoomType, ReservationDetails, RoomBedConfiguration)" 
                        TableName="Rooms" Where="RoomStatus == @RoomStatus">
                        <WhereParameters>
                            <asp:Parameter DefaultValue="H" Name="RoomStatus" Type="Char" />
                        </WhereParameters>
                    </asp:LinqDataSource>
                    </td>
            </tr>
            <tr>
                <td style="width: 494px">
                    <asp:Label ID="lblHousekeeping" runat="server" ForeColor="Red"></asp:Label>
                </td>
                <td>
                    .</td>
            </tr>
            <tr>
                <td style="width: 494px">
                    <asp:Button ID="btnReadyforGuest" runat="server" 
                        onclick="btnReadyforGuest_Click" Text="Ready for Guest" 
                        PostBackUrl="~/HouseKeeping/HouseKeepingDefault.aspx" />
&nbsp;&nbsp;&nbsp;
                    <asp:Button ID="btnNeedsMaintenance" runat="server" 
                        onclick="btnNeedsMaintenance_Click" Text="Needs Maintenance" 
                        PostBackUrl="~/HouseKeeping/HouseKeepingDefault.aspx" />
                </td>
                <td>
                    &nbsp;</td>
            </tr>
        </table>
    </div>
</asp:Content>
