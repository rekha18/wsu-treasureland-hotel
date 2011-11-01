<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminMaster.master" AutoEventWireup="true" CodeBehind="ManageStatuses.aspx.cs" Inherits="TreasureLand.Admin.ManageStatuses" %>
<asp:Content ID="Content1" ContentPlaceHolderID="AdminContentHolder" runat="server">
    <p>
        <asp:MultiView ID="mvStatuses" runat="server" ActiveViewIndex="0">
            <asp:View ID="vStatuses" runat="server">
                <asp:Panel ID="Panel1" runat="server" BackColor="Silver">
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
                    <asp:Button ID="btnAddStatusesPage" runat="server" CommandArgument="1" 
                        CommandName="SwitchViewByIndex" Text="Add Status" />
                </asp:Panel>
            </asp:View>
            <asp:View ID="vAddStatuses" runat="server">
                <asp:Panel ID="Panel2" runat="server" BackColor="Silver">
                    <table style="width: 100%;">
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
                                &nbsp;&nbsp;
                                <asp:RequiredFieldValidator ID="rfvDescription" runat="server" 
                                    ControlToValidate="txtDescription" ErrorMessage="Description is required" 
                                    ForeColor="Red"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 81px">
                                <asp:Button ID="btnBack" runat="server" CommandArgument="0" 
                                    CommandName="SwitchViewByIndex" Text="Back" />
                            </td>
                            <td>
                                &nbsp;
                                <asp:Button ID="btnAddStatuses" runat="server" CommandArgument="0" 
                                    CommandName="SwitchViewByIndex" onclick="btnAddStatuses_Click" 
                                    Text="Add Status" />
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
            </asp:View>
        </asp:MultiView>
        <br />
    </p>
</asp:Content>
