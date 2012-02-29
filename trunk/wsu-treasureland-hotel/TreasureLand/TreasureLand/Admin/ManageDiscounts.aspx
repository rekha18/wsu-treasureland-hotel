<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminMaster.master" AutoEventWireup="true" CodeBehind="ManageDiscounts.aspx.cs" Inherits="TreasureLand.Admin.ManageDiscounts" %>
<asp:Content ID="Content1" ContentPlaceHolderID="adminContentHolder" runat="server">
    <asp:MultiView ID="mvDiscounts" runat="server" ActiveViewIndex="0">
        <asp:View ID="vDiscountsUpdate" runat="server">
            <asp:Panel ID="Panel1" runat="server" BackColor="Silver">
                <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" 
                    DataKeyNames="DiscountID" DataSourceID="ldsDiscounts">
                    <Columns>
                        <asp:BoundField DataField="DiscountID" HeaderText="ID" InsertVisible="False" 
                            ReadOnly="True" SortExpression="DiscountID" />
                        <asp:BoundField DataField="DiscountDescription" HeaderText="Description" 
                            ReadOnly="True" SortExpression="DiscountDescription" />
                        <asp:TemplateField HeaderText="Expiration" SortExpression="DiscountExpiration">
                            <EditItemTemplate>
                                <asp:TextBox ID="TextBox1" runat="server" Height="20px" MaxLength="10" 
                                    Text='<%# Bind("DiscountExpiration", "{0:d}") %>'></asp:TextBox>
                                <asp:CompareValidator ID="cvDateValidator" runat="server" 
                                    ControlToValidate="TextBox1" 
                                    ErrorMessage="Expiration date must be in a valid date format" ForeColor="Red" 
                                    Operator="DataTypeCheck" Type="Date" ValidationGroup="dateSummary">*</asp:CompareValidator>
                                <asp:RequiredFieldValidator ID="rfvDateValidator" runat="server" 
                                    ControlToValidate="TextBox1" ErrorMessage="Expiration date is a required field" 
                                    ForeColor="Red" ValidationGroup="dateSummary">*</asp:RequiredFieldValidator>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="Label1" runat="server" 
                                    Text='<%# Bind("DiscountExpiration", "{0:d}") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Rules" SortExpression="DiscountRules">
                            <EditItemTemplate>
                                <asp:TextBox ID="TextBox2" runat="server" Text='<%# Bind("DiscountRules") %>'></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                                    ControlToValidate="TextBox2" ErrorMessage="Rules is a required field" 
                                    ForeColor="Red" ValidationGroup="dateSummary">*</asp:RequiredFieldValidator>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="Label2" runat="server" Text='<%# Bind("DiscountRules") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="DiscountAmount" 
                            HeaderText="Amount" SortExpression="DiscountAmount" ReadOnly="True" />
                        <asp:CheckBoxField DataField="IsPrecentage" HeaderText="Percentage" 
                            SortExpression="IsPrecentage" />
                        <asp:CommandField ButtonType="Button" ShowEditButton="True" 
                            ValidationGroup="dateSummary" />
                        <asp:CommandField ButtonType="Button" ShowDeleteButton="True" />
                    </Columns>
                </asp:GridView>
                <asp:ValidationSummary ID="vsDateSummary" runat="server" ForeColor="Red" 
                    ValidationGroup="dateSummary" />
                <br />
                <asp:LinqDataSource ID="ldsDiscounts" runat="server" 
                    ContextTypeName="TreasureLand.DBM.TreasureLandDataClassesDataContext" 
                    EnableDelete="True" EnableInsert="True" EnableUpdate="True" EntityTypeName="" 
                    OrderBy="DiscountID" TableName="Discounts">
                </asp:LinqDataSource>
                <asp:Button ID="btnAddDiscountItem" runat="server" CausesValidation="False" 
                    CommandArgument="1" CommandName="SwitchViewByIndex" Text="Add Discount" />
            </asp:Panel>
        </asp:View>
        <asp:View ID="vDiscountAdd" runat="server">
            <asp:Panel ID="Panel2" runat="server" BackColor="Silver">
                <table style="width: 100%;">
                    <tr>
                        <td style="width: 134px">
                            Add Discount</td>
                        <td>
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td style="width: 134px">
                            Discount Description:</td>
                        <td>
                            <asp:TextBox ID="txtDiscountDescription" runat="server" MaxLength="50"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvDiscountDescription" runat="server" 
                                ControlToValidate="txtDiscountDescription" Display="Dynamic" 
                                ErrorMessage="Description is required" ForeColor="Red" 
                                ValidationGroup="vgDiscounts"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 134px">
                            Discount Expiration:</td>
                        <td>
                            <asp:TextBox ID="txtDiscountExpiration" runat="server" MaxLength="16"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvDiscountExpiration" runat="server" 
                                ControlToValidate="txtDiscountExpiration" Display="Dynamic" 
                                ErrorMessage="Expiration Date is Required" ForeColor="Red" 
                                ValidationGroup="vgDiscounts"></asp:RequiredFieldValidator>
                            <asp:CompareValidator ID="cvDiscountExpiration" runat="server" 
                                ControlToValidate="txtDiscountExpiration" Display="Dynamic" 
                                ErrorMessage="Must enter a valid date" ForeColor="Red" 
                                Operator="DataTypeCheck" Type="Date" ValidationGroup="vgDiscounts"></asp:CompareValidator>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 134px">
                            Discount Rules:</td>
                        <td>
                            <asp:TextBox ID="txtDiscountRules" runat="server" MaxLength="100"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvDiscountRules" runat="server" 
                                ControlToValidate="txtDiscountRules" Display="Dynamic" 
                                ErrorMessage="Dicount rules are required" ForeColor="Red" 
                                ValidationGroup="vgDiscounts"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 134px">
                            Discount Amount:</td>
                        <td>
                            <asp:TextBox ID="txtDiscountAmount" runat="server" MaxLength="10"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvDiscountAmount" runat="server" 
                                ControlToValidate="txtDiscountAmount" Display="Dynamic" 
                                ErrorMessage="Discount amount is required" ForeColor="Red" 
                                ValidationGroup="vgDiscounts"></asp:RequiredFieldValidator>
                            <asp:CompareValidator ID="cvDiscountAmount" runat="server" 
                                ControlToValidate="txtDiscountAmount" Display="Dynamic" 
                                ErrorMessage="Please Enter a Decimal Number" ForeColor="Red" Type="Double" 
                                ValidationGroup="vgDiscounts"></asp:CompareValidator>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 134px">
                            Is Percent:</td>
                        <td>
                            <asp:CheckBox ID="cbIsPercentage" runat="server" />
                        </td>
                    </tr>
                </table>
                <br />
                <asp:Button ID="btnBackFromAdd" runat="server" CausesValidation="False" 
                    CommandArgument="0" CommandName="SwitchViewByIndex" Text="Back" />
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <asp:Button ID="btnInsertDiscount" runat="server" CommandArgument="0" 
                    CommandName="SwitchViewByIndex" onclick="btnInsertDiscount_Click" 
                    Text="Add Discount" ValidationGroup="vgDiscounts" />
            </asp:Panel>
        </asp:View>
    </asp:MultiView>
</asp:Content>
