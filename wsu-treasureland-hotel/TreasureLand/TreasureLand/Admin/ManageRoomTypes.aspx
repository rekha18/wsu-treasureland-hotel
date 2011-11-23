<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminMaster.master" AutoEventWireup="true" CodeBehind="ManageRoomTypes.aspx.cs" Inherits="TreasureLand.Admin.ManageRoomTypes" %>
<asp:Content ID="Content1" ContentPlaceHolderID="adminContentHolder" runat="server">
    <asp:MultiView ID="mvRoomTypes" runat="server" ActiveViewIndex="0">
        <asp:View ID="vRoomTypes" runat="server">
            <asp:Panel ID="pRoomTypes" runat="server" BackColor="Silver">
                <asp:GridView ID="gvRoomTpyes" runat="server" AutoGenerateColumns="False" 
                    DataKeyNames="HotelRoomTypeID" DataSourceID="ldsRoomTypes">
                    <Columns>
                        <asp:BoundField DataField="RoomType" HeaderText="RoomType" 
                            SortExpression="RoomType" />
                        <asp:BoundField DataField="RoomTypeRackRate" HeaderText="Rack Rate" 
                            SortExpression="RoomTypeRackRate" DataFormatString="{0:c}" />
                        <asp:BoundField DataField="RoomTypeDescription" HeaderText="Description" 
                            SortExpression="RoomTypeDescription" />
                        <asp:CommandField ButtonType="Button" ShowDeleteButton="True" 
                            ShowEditButton="True" />
                    </Columns>
                </asp:GridView>
                <asp:LinqDataSource ID="ldsRoomTypes" runat="server" 
                    ContextTypeName="TreasureLand.DBM.TreasureLandDataClassesDataContext" 
                    EnableDelete="True" EnableInsert="True" EnableUpdate="True" EntityTypeName="" 
                    OrderBy="HotelRoomTypeID" TableName="HotelRoomTypes">
                </asp:LinqDataSource>
                <br />
                <asp:Button ID="btnRoomType" runat="server" CommandArgument="1" 
                    CommandName="SwitchViewByIndex" Text="Add Room Type" />
            </asp:Panel>
        </asp:View>
        <asp:View ID="vAddRoomTypes" runat="server">
            <asp:Panel ID="pAddRoomTypes" runat="server" BackColor="Silver">
                <table style="width:100%;">
                    <tr>
                        <td style="width: 112px">
                            Add Room Type</td>
                        <td style="width: 245px">
                            &nbsp;</td>
                        <td>
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td style="width: 112px">
                            Hotel:</td>
                        <td style="width: 245px">
                            <asp:DropDownList ID="ddlHotel" runat="server" DataSourceID="ldsHotels" 
                                DataTextField="HotelName" DataValueField="HotelID">
                            </asp:DropDownList>
                        </td>
                        <td>
                            <asp:LinqDataSource ID="ldsHotels" runat="server" 
                                ContextTypeName="TreasureLand.DBM.TreasureLandDataClassesDataContext" 
                                EntityTypeName="" OrderBy="HotelID" TableName="Hotels">
                            </asp:LinqDataSource>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 112px">
                            Room Type:</td>
                        <td style="width: 245px">
                            <asp:TextBox ID="txtRoomType" runat="server"></asp:TextBox>
                        </td>
                        <td>
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td style="width: 112px">
                            Rack Rate:</td>
                        <td style="width: 245px">
                            <asp:TextBox ID="txtRackRate" runat="server"></asp:TextBox>
                        </td>
                        <td>
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td style="width: 112px">
                            Description:</td>
                        <td style="width: 245px">
                            <asp:TextBox ID="txtDescription" runat="server" MaxLength="200"></asp:TextBox>
                        </td>
                        <td>
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td style="width: 112px">
                            <asp:Button ID="btnBack" runat="server" CommandArgument="0" 
                                CommandName="SwitchViewByIndex" Text="Back" />
                        </td>
                        <td style="width: 245px">
                            <asp:Button ID="btnAddRoomType" runat="server" CommandArgument="0" 
                                CommandName="SwitchViewByIndex" onclick="btnAddRoomType_Click" Text="Add Room Type" />
                        </td>
                        <td>
                            &nbsp;</td>
                    </tr>
                </table>
            </asp:Panel>
        </asp:View>
    </asp:MultiView>
</asp:Content>
