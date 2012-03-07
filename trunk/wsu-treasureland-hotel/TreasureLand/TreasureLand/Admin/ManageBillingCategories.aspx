<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminMaster.master" AutoEventWireup="true" CodeBehind="ManageBillingCategories.aspx.cs" Inherits="TreasureLand.Admin.ManageBillingCategories" %>
<asp:Content ID="Content1" ContentPlaceHolderID="adminContentHolder" runat="server">
    <asp:MultiView ID="mvBillingCategories" runat="server" ActiveViewIndex="0">
        <asp:View ID="vUpdateBillingCategory" runat="server">
            <asp:Panel ID="Panel1" runat="server" BackColor="Silver">
                <asp:GridView ID="gvBilling" runat="server" AutoGenerateColumns="False" 
                    DataSourceID="ldsBilling" DataKeyNames="BillingCategoryID">
                    <Columns>
                        <asp:BoundField DataField="BillingCategoryID" HeaderText="ID" 
                            InsertVisible="False" ReadOnly="True" SortExpression="BillingCategoryID" />
                        <asp:TemplateField HeaderText="Description" 
                            SortExpression="BillingCategoryDescription">
                            <EditItemTemplate>
                                <asp:TextBox ID="TextBox1" runat="server" 
                                    Text='<%# Bind("BillingCategoryDescription") %>'></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                                    ControlToValidate="TextBox1" ErrorMessage="Description is a required field" 
                                    ForeColor="Red">*</asp:RequiredFieldValidator>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="Label1" runat="server" 
                                    Text='<%# Bind("BillingCategoryDescription") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Taxable" SortExpression="BillingCategoryTaxable">
                            <EditItemTemplate>
                                <asp:CheckBox ID="cbCheckBox" runat="server"
                                Checked='<%# Bind("BillingCategoryTaxable") %>' />
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:CheckBox ID="cbCheckBox" runat="server" Enabled="false"
                                Checked='<%# Bind("BillingCategoryTaxable") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:CommandField ButtonType="Button" 
                            ShowEditButton="True" />
                    </Columns>
                </asp:GridView>
                <asp:LinqDataSource ID="ldsBilling" runat="server" 
                    ContextTypeName="TreasureLand.DBM.TreasureLandDataClassesDataContext" 
                    EnableUpdate="True" EntityTypeName="" 
                    OrderBy="BillingCategoryID" TableName="BillingCategories" 
                    EnableDelete="True" EnableInsert="True">
                </asp:LinqDataSource>
                <asp:ValidationSummary ID="ValidationSummary1" runat="server" ForeColor="Red" />
                <br />
                <asp:Button ID="btnAddBillingCategory" runat="server" CommandArgument="1" 
                    CommandName="SwitchViewByIndex" Text="Add Service or Fee" />
            </asp:Panel>
        </asp:View>
        <asp:View ID="vAddBillingCategory" runat="server">
            <asp:Panel ID="Panel2" runat="server" BackColor="Silver">
                <table style="width:100%;">
                    <tr>
                        <td>
                            Description:</td>
                        <td>
                            <asp:TextBox ID="txtDesciption" runat="server" MaxLength="30"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvDescription" runat="server" 
                                ControlToValidate="txtDesciption" ErrorMessage="Description Is Required" 
                                ForeColor="Red" ValidationGroup="vgBilling"></asp:RequiredFieldValidator>
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
                <asp:Button ID="btnBack" runat="server" CommandArgument="0" 
                    CommandName="SwitchViewByIndex" Text="Back" />
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <asp:Button ID="btnAddBilling" runat="server" CommandArgument="0" 
                    CommandName="SwitchViewByIndex" onclick="btnAddBilling_Click" 
                    Text="Add Service or Fee" ValidationGroup="vgBilling" />
            </asp:Panel>
        </asp:View>
    </asp:MultiView>
</asp:Content>
