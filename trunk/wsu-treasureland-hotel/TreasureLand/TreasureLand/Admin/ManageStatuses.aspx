<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminMaster.master" AutoEventWireup="true" CodeBehind="ManageStatuses.aspx.cs" Inherits="TreasureLand.Admin.ManageStatuses" %>
<asp:Content ID="Content1" ContentPlaceHolderID="AdminContentHolder" runat="server">
    <p>
        <asp:MultiView ID="mvStatuses" runat="server" ActiveViewIndex="0">
            <asp:View ID="vStatuses" runat="server">
                <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" 
                    DataKeyNames="Status" DataSourceID="ldsStatuses">
                    <Columns>
                        <asp:BoundField DataField="Status" HeaderText="Status" ReadOnly="True" 
                            SortExpression="Status" />
                        <asp:BoundField DataField="Description" HeaderText="Description" 
                            SortExpression="Description" />
                    </Columns>
                </asp:GridView>
                <asp:LinqDataSource ID="ldsStatuses" runat="server" 
                    ContextTypeName="TreasureLand.TreasureLandDataClassesDataContext" 
                    EnableDelete="True" EnableInsert="True" EnableUpdate="True" EntityTypeName="" 
                    OrderBy="Status" TableName="RoomStatus">
                </asp:LinqDataSource>
                <br />
                <asp:Button ID="btnAddStatusesPage" runat="server" CommandArgument="1" 
                    CommandName="SwitchViewByIndex" Text="Add Status" />
            </asp:View>
            <asp:View ID="vAddStatuses" runat="server">
                <table style="width:100%;">
                    <tr>
                        <td style="width: 81px">
                            Add Status</td>
                        <td>
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td style="width: 81px">
                            Description:</td>
                        <td>
                            <asp:TextBox ID="txtDescription" runat="server" Width="187px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 81px">
                            <asp:Button ID="btnAddStatuses" runat="server" CommandArgument="0" 
                                CommandName="SwitchViewByIndex" onclick="btnAddStatuses_Click" 
                                Text="Add Status" />
                        </td>
                        <td>
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            <asp:Button ID="btnBack" runat="server" CommandArgument="0" 
                                CommandName="SwitchViewByIndex" Text="Back" />
                        </td>
                    </tr>
                </table>
                <br />
                <br />
                <br />
            </asp:View>
        </asp:MultiView>
        <br />
    </p>
</asp:Content>
