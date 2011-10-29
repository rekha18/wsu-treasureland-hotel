<%@ Page Title="" Language="C#" MasterPageFile="~/Maintenance/MaintenanceMasterPage.master" AutoEventWireup="true" CodeBehind="MaintenanceDefault.aspx.cs" Inherits="TreasureLand.Maintenance.MaintenanceDefault" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <p>
        <table style="width:100%;">
            <tr>
                <td align="center">
                    <asp:GridView ID="gvHouseKeeping" runat="server" AllowPaging="True" 
                        AllowSorting="True" AutoGenerateColumns="False" BackColor="White" 
                        BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px" CellPadding="3" 
                        DataSourceID="SqlDataSource1" ForeColor="Black" GridLines="Vertical">
                        <AlternatingRowStyle BackColor="#CCCCCC" />
                        <Columns>
                            <asp:CommandField ShowSelectButton="True" />
                            <asp:BoundField DataField="RoomID" HeaderText="RoomID" 
                                SortExpression="RoomID" />
                            <asp:BoundField DataField="RoomNumbers" HeaderText="RoomNumbers" 
                                SortExpression="RoomNumbers" />
                            <asp:BoundField DataField="RoomDescription" HeaderText="RoomDescription" 
                                SortExpression="RoomDescription" />
                            <asp:BoundField DataField="CheckinDate" HeaderText="CheckinDate" 
                                SortExpression="CheckinDate" />
                            <asp:BoundField DataField="Nights" HeaderText="Nights" 
                                SortExpression="Nights" />
                            <asp:CheckBoxField DataField="RoomStatus" HeaderText="RoomStatus" 
                                SortExpression="RoomStatus" />
                            <asp:BoundField DataField="Status" HeaderText="Status" 
                                SortExpression="Status" />
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
                <td style="width: 4px">
                    &nbsp;</td>
                <td>
                    <asp:DataList ID="DataList1" runat="server" CellPadding="4" 
                        DataKeyField="RoomID" DataSourceID="SqlDataSource2" ForeColor="#333333" 
                        Height="864px">
                        <AlternatingItemStyle BackColor="White" />
                        <AlternatingItemTemplate>
                            RoomID:
                            <asp:Label ID="RoomIDLabel" runat="server" Text='<%# Eval("RoomID") %>' />
                            <br />
                            HotelRoomTypeID:
                            <asp:Label ID="HotelRoomTypeIDLabel" runat="server" 
                                Text='<%# Eval("HotelRoomTypeID") %>' />
                            <br />
                            RoomNumbers:
                            <asp:Label ID="RoomNumbersLabel" runat="server" 
                                Text='<%# Eval("RoomNumbers") %>' />
                            <br />
                            RoomDescription:
                            <asp:Label ID="RoomDescriptionLabel" runat="server" 
                                Text='<%# Eval("RoomDescription") %>' />
                            <br />
                            RoomSmoking:
                            <asp:Label ID="RoomSmokingLabel" runat="server" 
                                Text='<%# Eval("RoomSmoking") %>' />
                            <br />
                            RoomBedConfiguration:
                            <asp:Label ID="RoomBedConfigurationLabel" runat="server" 
                                Text='<%# Eval("RoomBedConfiguration") %>' />
                            <br />
                            RoomHandicap:
                            <asp:Label ID="RoomHandicapLabel" runat="server" 
                                Text='<%# Eval("RoomHandicap") %>' />
                            <br />
                            RoomStatus:
                            <asp:Label ID="RoomStatusLabel" runat="server" 
                                Text='<%# Eval("RoomStatus") %>' />
                        </AlternatingItemTemplate>
                        <EditItemTemplate>
                            RoomID:
                            <asp:Label ID="RoomIDLabel0" runat="server" Text='<%# Eval("RoomID") %>' />
                            <br />
                            HotelRoomTypeID:
                            <asp:Label ID="HotelRoomTypeIDLabel0" runat="server" 
                                Text='<%# Eval("HotelRoomTypeID") %>' />
                            <br />
                            RoomNumbers:
                            <asp:Label ID="RoomNumbersLabel0" runat="server" 
                                Text='<%# Eval("RoomNumbers") %>' />
                            <br />
                            RoomDescription:
                            <asp:Label ID="RoomDescriptionLabel0" runat="server" 
                                Text='<%# Eval("RoomDescription") %>' />
                            <br />
                            RoomSmoking:
                            <asp:Label ID="RoomSmokingLabel0" runat="server" 
                                Text='<%# Eval("RoomSmoking") %>' />
                            <br />
                            RoomBedConfiguration:
                            <asp:Label ID="RoomBedConfigurationLabel0" runat="server" 
                                Text='<%# Eval("RoomBedConfiguration") %>' />
                            <br />
                            RoomHandicap:
                            <asp:Label ID="RoomHandicapLabel0" runat="server" 
                                Text='<%# Eval("RoomHandicap") %>' />
                            <br />
                            RoomStatus:
                            <asp:Label ID="RoomStatusLabel0" runat="server" 
                                Text='<%# Eval("RoomStatus") %>' />
                        </EditItemTemplate>
                        <FooterStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                        <HeaderStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                        <HeaderTemplate>
                            Modify Room Content
                        </HeaderTemplate>
                        <ItemStyle BackColor="#E3EAEB" />
                        <ItemTemplate>
                            RoomID:
                            <asp:Label ID="RoomIDLabel1" runat="server" Text='<%# Eval("RoomID") %>' />
                            <br />
                            HotelRoomTypeID:
                            <asp:Label ID="HotelRoomTypeIDLabel1" runat="server" 
                                Text='<%# Eval("HotelRoomTypeID") %>' />
                            <br />
                            RoomNumbers:
                            <asp:Label ID="RoomNumbersLabel1" runat="server" 
                                Text='<%# Eval("RoomNumbers") %>' />
                            <br />
                            RoomDescription:
                            <asp:Label ID="RoomDescriptionLabel1" runat="server" 
                                Text='<%# Eval("RoomDescription") %>' />
                            <br />
                            RoomSmoking:
                            <asp:Label ID="RoomSmokingLabel1" runat="server" 
                                Text='<%# Eval("RoomSmoking") %>' />
                            <br />
                            RoomBedConfiguration:
                            <asp:Label ID="RoomBedConfigurationLabel1" runat="server" 
                                Text='<%# Eval("RoomBedConfiguration") %>' />
                            <br />
                            RoomHandicap:
                            <asp:Label ID="RoomHandicapLabel1" runat="server" 
                                Text='<%# Eval("RoomHandicap") %>' />
                            <br />
                            RoomStatus:
                            <asp:Label ID="RoomStatusLabel1" runat="server" 
                                Text='<%# Eval("RoomStatus") %>' />
                            <br />
<br />
                        </ItemTemplate>
                        <SelectedItemStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333" />
                        <SelectedItemTemplate>
                            RoomID:
                            <asp:Label ID="RoomIDLabel2" runat="server" Text='<%# Eval("RoomID") %>' />
                            <br />
                            HotelRoomTypeID:
                            <asp:Label ID="HotelRoomTypeIDLabel2" runat="server" 
                                Text='<%# Eval("HotelRoomTypeID") %>' />
                            <br />
                            RoomNumbers:
                            <asp:Label ID="RoomNumbersLabel2" runat="server" 
                                Text='<%# Eval("RoomNumbers") %>' />
                            <br />
                            RoomDescription:
                            <asp:Label ID="RoomDescriptionLabel2" runat="server" 
                                Text='<%# Eval("RoomDescription") %>' />
                            <br />
                            RoomSmoking:
                            <asp:Label ID="RoomSmokingLabel2" runat="server" 
                                Text='<%# Eval("RoomSmoking") %>' />
                            <br />
                            RoomBedConfiguration:
                            <asp:Label ID="RoomBedConfigurationLabel2" runat="server" 
                                Text='<%# Eval("RoomBedConfiguration") %>' />
                            <br />
                            RoomHandicap:
                            <asp:Label ID="RoomHandicapLabel2" runat="server" 
                                Text='<%# Eval("RoomHandicap") %>' />
                            <br />
                            RoomStatus:
                            <asp:Label ID="RoomStatusLabel2" runat="server" 
                                Text='<%# Eval("RoomStatus") %>' />
                        </SelectedItemTemplate>
                    </asp:DataList>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
                        ConnectionString="<%$ ConnectionStrings:TreasureLandDB %>" 
                        SelectCommand="SELECT ReservationDetail.RoomID, Room.RoomNumbers, Room.RoomDescription, ReservationDetail.CheckinDate, ReservationDetail.Nights, Room.RoomStatus, ReservationDetail.Status FROM ReservationDetail INNER JOIN Room ON ReservationDetail.RoomID = Room.RoomID">
                    </asp:SqlDataSource>
                </td>
                <td style="width: 4px">
                    &nbsp;</td>
                <td style="width: 184px">
                    <asp:SqlDataSource ID="SqlDataSource2" runat="server" 
                        ConnectionString="<%$ ConnectionStrings:TreasureLandDB %>" 
                        SelectCommand="SELECT [RoomID], [HotelRoomTypeID], [RoomNumbers], [RoomDescription], [RoomSmoking], [RoomBedConfiguration], [RoomHandicap], [RoomStatus] FROM [Room]">
                    </asp:SqlDataSource>
                </td>
            </tr>
        </table>
        Housekeeping main page</p>
    <br />
    Maintenance Home Page
</asp:Content>
