<%@ Page Title="" Language="C#" MasterPageFile="~/Maintenance/MaintenanceMasterPage.master" AutoEventWireup="true" CodeBehind="MaintenanceDefault.aspx.cs" Inherits="TreasureLand.Maintenance.MaintenanceDefault" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div> 
        <table style="width:547px;">
            <tr>
                <td align="center" style="width: 494px">
                    <asp:GridView ID="gvHouseKeeping" runat="server" AllowPaging="True" 
                        AllowSorting="True" AutoGenerateColumns="False" BackColor="White" 
                        BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px" CellPadding="3" 
                        DataSourceID="LinqDataSource1" ForeColor="Black" GridLines="Vertical">
                        <AlternatingRowStyle BackColor="#CCCCCC" />
                        <Columns>
                            <asp:CommandField ShowSelectButton="True" />
                            <asp:BoundField DataField="RoomID" HeaderText="RoomID" ReadOnly="True" 
                                SortExpression="RoomID" Visible="False" />
                            <asp:BoundField DataField="RoomNumbers" HeaderText="RoomNumbers" 
                                ReadOnly="True" SortExpression="RoomNumbers" />
                            <asp:BoundField DataField="RoomDescription" HeaderText="RoomDescription" 
                                ReadOnly="True" SortExpression="RoomDescription" />
                            <asp:BoundField DataField="RoomStatus" HeaderText="RoomStatus" ReadOnly="True" 
                                SortExpression="RoomStatus" />
                            <asp:BoundField DataField="RoomBedConfiguration" 
                                HeaderText="RoomBedConfiguration" ReadOnly="True" 
                                SortExpression="RoomBedConfiguration" Visible="False" />
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
                           <asp:Parameter DefaultValue="M" Name="RoomStatus" Type="Char" />
                       </WhereParameters>
                    </asp:LinqDataSource>
                </td>
            </tr>
            <tr>
                <td style="width: 494px">
                    <asp:Label ID="lblMaintenance" runat="server" ForeColor="Red"></asp:Label>
                </td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td style="width: 494px">
                    &nbsp;</td>
                <td>
                    &nbsp;</td>
            </tr>
        </table>
    </div>
</asp:Content>
