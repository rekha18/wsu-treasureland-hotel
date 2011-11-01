<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminMaster.master" AutoEventWireup="true"
    CodeBehind="ManageRooms.aspx.cs" Inherits="TreasureLand.Admin.ManageRooms" %>

<asp:Content ID="Content1" ContentPlaceHolderID="AdminContentHolder" runat="server">
    <asp:ListView ID="ListView_Rooms" runat="server" DataSourceID="EntityDataSource_Rooms"
        DataKeyNames="RoomID">
        <AlternatingItemTemplate>
            <tr style="">
                <td>
                    <asp:Label ID="RoomNumbersLabel" runat="server" Text='<%# Eval("RoomNumbers") %>' />
                </td>
                <td>
                    <asp:Label ID="RoomDescriptionLabel" runat="server" Text='<%# Eval("RoomDescription") %>' />
                </td>
                <td>
                    <asp:CheckBox ID="RoomSmokingCheckBox" runat="server" Checked='<%# Eval("RoomSmoking") %>'
                        Enabled="false" />
                </td>
                <td>
                    <asp:Label ID="RoomBedConfigurationLabel" runat="server" Text='<%# Eval("RoomBedConfiguration") %>' />
                </td>
                <td>
                    <asp:CheckBox ID="RoomHandicapCheckBox" runat="server" Checked='<%# Eval("RoomHandicap") %>'
                        Enabled="false" />
                </td>
                <td>
                    <asp:Label ID="RoomStatusLabel" runat="server" Text='<%# Eval("RoomStatus") %>' />
                </td>
                <td>
                    <asp:Label ID="HotelRoomTypeLabel" runat="server" Text='<%# Eval("HotelRoomType.RoomType") %>' />
                </td>
            </tr>
        </AlternatingItemTemplate>
        <EditItemTemplate>
            <tr style="">
                <td>
                    <asp:Button ID="UpdateButton" runat="server" CommandName="Update" Text="Update" />
                    <asp:Button ID="CancelButton" runat="server" CommandName="Cancel" Text="Cancel" />
                </td>
                <td>
                    <asp:TextBox ID="RoomNumbersTextBox" runat="server" Text='<%# Bind("RoomNumbers") %>' />
                </td>
                <td>
                    <asp:TextBox ID="RoomDescriptionTextBox" runat="server" Text='<%# Bind("RoomDescription") %>' />
                </td>
                <td>
                    <asp:CheckBox ID="RoomSmokingCheckBox" runat="server" Checked='<%# Bind("RoomSmoking") %>' />
                </td>
                <td>
                    <asp:TextBox ID="RoomBedConfigurationTextBox" runat="server" Text='<%# Bind("RoomBedConfiguration") %>' />
                </td>
                <td>
                    <asp:CheckBox ID="RoomHandicapCheckBox" runat="server" Checked='<%# Bind("RoomHandicap") %>' />
                </td>
                <td>
                    <asp:TextBox ID="RoomStatusTextBox" runat="server" Text='<%# Bind("RoomStatus") %>' />
                </td>
                <td>
                    <asp:TextBox ID="HotelRoomTypeTextBox" runat="server" Text='<%# Bind("HotelRoomType.RoomType") %>' />
                </td>
            </tr>
        </EditItemTemplate>
        <EmptyDataTemplate>
            <table runat="server" style="">
                <tr>
                    <td>
                        No data was returned.
                    </td>
                </tr>
            </table>
        </EmptyDataTemplate>
        <InsertItemTemplate>
            <tr style="">
                <td>
                    <asp:Button ID="InsertButton" runat="server" CommandName="Insert" Text="Insert" />
                    <asp:Button ID="CancelButton" runat="server" CommandName="Cancel" Text="Clear" />
                </td>
                <td>
                    <asp:TextBox ID="RoomNumbersTextBox" runat="server" Text='<%# Bind("RoomNumbers") %>' />
                </td>
                <td>
                    <asp:TextBox ID="RoomDescriptionTextBox" runat="server" Text='<%# Bind("RoomDescription") %>' />
                </td>
                <td>
                    <asp:CheckBox ID="RoomSmokingCheckBox" runat="server" Checked='<%# Bind("RoomSmoking") %>' />
                </td>
                <td>
                    <asp:TextBox ID="RoomBedConfigurationTextBox" runat="server" Text='<%# Bind("RoomBedConfiguration") %>' />
                </td>
                <td>
                    <asp:CheckBox ID="RoomHandicapCheckBox" runat="server" Checked='<%# Bind("RoomHandicap") %>' />
                </td>
                <td>
                    <asp:TextBox ID="RoomStatusTextBox" runat="server" Text='<%# Bind("RoomStatus") %>' />
                </td>
                <td>
                    <asp:TextBox ID="HotelRoomTypeTextBox" runat="server" Text='<%# Bind("HotelRoomType.RoomType") %>' />
                </td>
            </tr>
        </InsertItemTemplate>
        <ItemTemplate>
            <tr style="">
                <td>
                    <asp:Label ID="RoomNumbersLabel" runat="server" Text='<%# Eval("RoomNumbers") %>' />
                </td>
                <td>
                    <asp:Label ID="RoomDescriptionLabel" runat="server" Text='<%# Eval("RoomDescription") %>' />
                </td>
                <td>
                    <asp:CheckBox ID="RoomSmokingCheckBox" runat="server" Checked='<%# Eval("RoomSmoking") %>'
                        Enabled="false" />
                </td>
                <td>
                    <asp:Label ID="RoomBedConfigurationLabel" runat="server" Text='<%# Eval("RoomBedConfiguration") %>' />
                </td>
                <td>
                    <asp:CheckBox ID="RoomHandicapCheckBox" runat="server" Checked='<%# Eval("RoomHandicap") %>'
                        Enabled="false" />
                </td>
                <td>
                    <asp:Label ID="RoomStatusLabel" runat="server" Text='<%# Eval("RoomStatus") %>' />
                </td>
                <td>
                    <asp:Label ID="HotelRoomTypeLabel" runat="server" Text='<%# Eval("HotelRoomType.RoomType") %>' />
                </td>
            </tr>
        </ItemTemplate>
        <LayoutTemplate>
            <table runat="server">
                <tr runat="server">
                    <td runat="server">
                        <table id="itemPlaceholderContainer" runat="server" border="0" style="">
                            <tr runat="server" style="">
                                <th runat="server">
                                    Room Number
                                </th>
                                <th runat="server">
                                    Description
                                </th>
                                <th runat="server">
                                    Smoking
                                </th>
                                <th runat="server">
                                    Bed Configuration
                                </th>
                                <th runat="server">
                                    Handicap
                                </th>
                                <th runat="server">
                                    Status
                                </th>
                                <th runat="server">
                                    Room Type
                                </th>
                            </tr>
                            <tr id="itemPlaceholder" runat="server">
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr runat="server">
                    <td runat="server" style="">
                        <asp:DataPager ID="DataPager1" runat="server">
                            <Fields>
                                <asp:NextPreviousPagerField ButtonType="Button" ShowFirstPageButton="True" ShowLastPageButton="True" />
                            </Fields>
                        </asp:DataPager>
                    </td>
                </tr>
            </table>
        </LayoutTemplate>
        <SelectedItemTemplate>
            <tr style="">
                
                <td>
                    <asp:Label ID="RoomNumbersLabel" runat="server" Text='<%# Eval("RoomNumbers") %>' />
                </td>
                <td>
                    <asp:Label ID="RoomDescriptionLabel" runat="server" Text='<%# Eval("RoomDescription") %>' />
                </td>
                <td>
                    <asp:CheckBox ID="RoomSmokingCheckBox" runat="server" Checked='<%# Eval("RoomSmoking") %>'
                        Enabled="false" />
                </td>
                <td>
                    <asp:Label ID="RoomBedConfigurationLabel" runat="server" Text='<%# Eval("RoomBedConfiguration") %>' />
                </td>
                <td>
                    <asp:CheckBox ID="RoomHandicapCheckBox" runat="server" Checked='<%# Eval("RoomHandicap") %>'
                        Enabled="false" />
                </td>
                <td>
                    <asp:Label ID="RoomStatusLabel" runat="server" Text='<%# Eval("RoomStatus") %>' />
                </td>
                <td>
                    <asp:Label ID="HotelRoomTypeLabel" runat="server" Text='<%# Eval("HotelRoomType.RoomType") %>' />
                </td>                
            </tr>
        </SelectedItemTemplate>
    </asp:ListView>
    <asp:EntityDataSource ID="EntityDataSource_Rooms" runat="server" ConnectionString="name=TreasureLandEntities"
        DefaultContainerName="TreasureLandEntities" EnableFlattening="False" EntitySetName="Rooms"
        EntityTypeFilter="Room">
    </asp:EntityDataSource>
</asp:Content>
