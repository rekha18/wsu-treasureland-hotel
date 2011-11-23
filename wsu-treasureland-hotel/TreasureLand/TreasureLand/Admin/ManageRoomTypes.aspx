<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminMaster.master" AutoEventWireup="true" CodeBehind="ManageRoomTypes.aspx.cs" Inherits="TreasureLand.Admin.ManageRoomTypes" %>
<asp:Content ID="Content1" ContentPlaceHolderID="adminContentHolder" runat="server">
    <asp:MultiView ID="mvRoomTypes" runat="server" ActiveViewIndex="0">
        <asp:View ID="vRoomTypes" runat="server">
            <asp:Panel ID="pRoomTypes" runat="server" BackColor="Silver">
                <asp:GridView ID="gvRoomTpyes" runat="server" AutoGenerateColumns="False" 
                    DataKeyNames="HotelRoomTypeID" DataSourceID="ldsRoomTypes">
                    <Columns>
                        <asp:TemplateField HeaderText="RoomType" SortExpression="RoomType">
                            <EditItemTemplate>
                                <asp:DropDownList ID="DropDownList1" runat="server" DataSourceID="ldsRoomTypes" 
                                    DataTextField="RoomType" DataValueField="RoomType" 
                                    SelectedValue='<%# Bind("RoomType") %>'>
                                </asp:DropDownList>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="Label2" runat="server" Text='<%# Bind("RoomType") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Rack Rate" SortExpression="RoomTypeRackRate">
                            <EditItemTemplate>
                                <asp:TextBox ID="TextBox1" runat="server" MaxLength="10" 
                                    Text='<%# Bind("RoomTypeRackRate",  "{0:0.00}") %>' ValidationGroup="vgRoom"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rvRackRate" runat="server" 
                                    ControlToValidate="TextBox1" Display="Dynamic" 
                                    ErrorMessage="Rate is a required field" ForeColor="Red" 
                                    ValidationGroup="vgRoom">*</asp:RequiredFieldValidator>
                                <asp:CompareValidator ID="cvRackRate" runat="server" 
                                    ControlToValidate="TextBox1" Display="Dynamic" 
                                    ErrorMessage="Rate must be an amount" ForeColor="Red" Operator="DataTypeCheck" 
                                    Type="Currency" ValidationGroup="vgRoom">*</asp:CompareValidator>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="Label1" runat="server" 
                                    Text='<%# Bind("RoomTypeRackRate", "{0:c}") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="RoomTypeDescription" HeaderText="Description" 
                            SortExpression="RoomTypeDescription" />
                        <asp:CommandField ButtonType="Button" 
                            ShowEditButton="True" />
                    </Columns>
                </asp:GridView>
                <asp:ValidationSummary ID="ValidationSummary1" runat="server" ForeColor="Red" 
                    ValidationGroup="vgCost" />
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
                            <asp:RequiredFieldValidator ID="rvRoomType" runat="server" 
                                ControlToValidate="txtRoomType" ErrorMessage="Room Type is required" 
                                ForeColor="Red" ValidationGroup="vgAddRoom">*</asp:RequiredFieldValidator>
                        </td>
                        <td>
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td style="width: 112px">
                            Rack Rate:</td>
                        <td style="width: 245px">
                            <asp:TextBox ID="txtRackRate" runat="server" MaxLength="10"></asp:TextBox>
                            <asp:CompareValidator ID="cvRackRate" runat="server" 
                                ControlToValidate="txtRackRate" Display="Dynamic" 
                                ErrorMessage="Rack Rate must be a number" ForeColor="Red" 
                                Operator="DataTypeCheck" Type="Double" ValidationGroup="vgAddRoom">*</asp:CompareValidator>
                            <asp:RequiredFieldValidator ID="rvRackRate" runat="server" 
                                ControlToValidate="txtRackRate" Display="Dynamic" 
                                ErrorMessage="Rack Rate is required" ForeColor="Red" 
                                ValidationGroup="vgAddRoom">*</asp:RequiredFieldValidator>
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
                                CommandName="SwitchViewByIndex" onclick="btnAddRoomType_Click" 
                                Text="Add Room Type" ValidationGroup="vgAddRoom" />
                        </td>
                        <td>
                            &nbsp;</td>
                    </tr>
                </table>
                <br />
                <asp:ValidationSummary ID="ValidationSummary2" runat="server" ForeColor="Red" 
                    ValidationGroup="vgAddRoom" />
            </asp:Panel>
        </asp:View>
    </asp:MultiView>
</asp:Content>
