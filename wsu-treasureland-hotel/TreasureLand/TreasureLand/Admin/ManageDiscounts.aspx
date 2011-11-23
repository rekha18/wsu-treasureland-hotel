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
                        <asp:BoundField DataField="DiscountExpiration" DataFormatString="{0:d}" 
                            HeaderText="Expiration" SortExpression="DiscountExpiration" />
                        <asp:BoundField DataField="DiscountRules" HeaderText="Rules" 
                            SortExpression="DiscountRules" />
                        <asp:BoundField DataField="DiscountAmount" HeaderText="Amount" ReadOnly="True" 
                            SortExpression="DiscountAmount" />
                        <asp:CheckBoxField DataField="IsPrecentage" HeaderText="Precentage" 
                            ReadOnly="True" SortExpression="IsPrecentage" />
                        <asp:CommandField ButtonType="Button" ShowEditButton="True" />
                        <asp:CommandField ButtonType="Button" ShowDeleteButton="True" />
                    </Columns>
                </asp:GridView>
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
                            <asp:TextBox ID="txtDiscountDescription" runat="server"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvDiscountDescription" runat="server" 
                                ControlToValidate="txtDiscountDescription" Display="Dynamic" 
                                ErrorMessage="Description is required" ForeColor="Red"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 134px">
                            Discount Expiration:</td>
                        <td>
                            <asp:TextBox ID="txtDiscountExpiration" runat="server"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvDiscountExpiration" runat="server" 
                                ControlToValidate="txtDiscountExpiration" Display="Dynamic" 
                                ErrorMessage="Expiration Date is Required" ForeColor="Red"></asp:RequiredFieldValidator>
                            <asp:CompareValidator ID="cvDiscountExpiration" runat="server" 
                                ControlToValidate="txtDiscountExpiration" Display="Dynamic" 
                                ErrorMessage="Must enter a valid date" ForeColor="Red" 
                                Operator="DataTypeCheck" Type="Date"></asp:CompareValidator>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 134px">
                            Discount Rules:</td>
                        <td>
                            <asp:TextBox ID="txtDiscountRules" runat="server"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvDiscountRules" runat="server" 
                                ControlToValidate="txtDiscountRules" Display="Dynamic" 
                                ErrorMessage="Dicount rules are required" ForeColor="Red"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 134px">
                            Discount Amount:</td>
                        <td>
                            <asp:TextBox ID="txtDiscountAmount" runat="server"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvDiscountAmount" runat="server" 
                                ControlToValidate="txtDiscountAmount" Display="Dynamic" 
                                ErrorMessage="Discount amount is required" ForeColor="Red"></asp:RequiredFieldValidator>
                            <asp:CompareValidator ID="cvDiscountAmount" runat="server" 
                                ControlToValidate="txtDiscountAmount" Display="Dynamic" 
                                ErrorMessage="Please Enter a Decimal Number" ForeColor="Red" Type="Double"></asp:CompareValidator>
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
                    Text="Add Discount" />
            </asp:Panel>
        </asp:View>
    </asp:MultiView>
</asp:Content>
