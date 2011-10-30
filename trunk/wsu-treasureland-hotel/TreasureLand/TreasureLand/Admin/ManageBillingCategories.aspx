<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminMaster.master" AutoEventWireup="true" CodeBehind="ManageBillingCategories.aspx.cs" Inherits="TreasureLand.Admin.ManageBillingCategories" %>
<asp:Content ID="Content1" ContentPlaceHolderID="adminContentHolder" runat="server">
    <asp:MultiView ID="mvBillingCategories" runat="server" ActiveViewIndex="0">
        <asp:View ID="vUpdateBillingCategory" runat="server">
            <asp:GridView ID="gvBilling" runat="server" AutoGenerateColumns="False" 
                DataKeyNames="BillingCategoryID" DataSourceID="ldsBilling">
                <Columns>
                    <asp:BoundField DataField="BillingCategoryID" HeaderText="ID" 
                        InsertVisible="False" ReadOnly="True" SortExpression="BillingCategoryID" />
                    <asp:BoundField DataField="BillingCategoryDescription" HeaderText="Description" 
                        SortExpression="BillingCategoryDescription" />
                    <asp:CheckBoxField DataField="BillingCategoryTaxable" HeaderText="Taxable" 
                        SortExpression="BillingCategoryTaxable" />
                    <asp:CommandField ButtonType="Button" ShowEditButton="True" />
                </Columns>
            </asp:GridView>
            <br />
            <asp:Button ID="btnAddBillingCategory" runat="server" CommandArgument="1" 
                CommandName="SwitchViewByIndex" Text="Add Billing Category" />
            <asp:LinqDataSource ID="ldsBilling" runat="server" 
                ContextTypeName="TreasureLand.TreasureLandDataClassesDataContext" 
                EnableInsert="True" EnableUpdate="True" EntityTypeName="" 
                OrderBy="BillingCategoryID" TableName="BillingCategories">
            </asp:LinqDataSource>
        </asp:View>
        <asp:View ID="vAddBillingCategory" runat="server">
            <table style="width:100%;">
                <tr>
                    <td>
                        Description:</td>
                    <td>
                        <asp:TextBox ID="txtDesciption" runat="server"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rfvDescription" runat="server" 
                            ControlToValidate="txtDesciption" ErrorMessage="Description Is Required" 
                            ForeColor="Red"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td>
                        Taxable:</td>
                    <td>
                        <asp:CheckBox ID="cbTaxable" runat="server" />
                    </td>
                </tr>
            </table>
            <br />
            <asp:Button ID="btnAddBilling" runat="server" CommandArgument="0" 
                CommandName="SwitchViewByIndex" Text="Add Billing Category" />
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:Button ID="btnBack" runat="server" CommandArgument="0" 
                CommandName="SwitchViewByIndex" Text="Back" />
            <br />
        </asp:View>
    </asp:MultiView>
</asp:Content>
