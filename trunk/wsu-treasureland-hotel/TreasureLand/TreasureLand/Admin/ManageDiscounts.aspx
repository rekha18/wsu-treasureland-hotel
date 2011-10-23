<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminMaster.master" AutoEventWireup="true" CodeBehind="ManageDiscounts.aspx.cs" Inherits="TreasureLand.Admin.ManageDiscounts" %>
<asp:Content ID="Content1" ContentPlaceHolderID="adminContentHolder" runat="server">
    <asp:MultiView ID="mvDiscounts" runat="server" ActiveViewIndex="0">
        <asp:View ID="vDiscountsMain" runat="server">
            <asp:Button ID="btnAddDiscounts" runat="server" CausesValidation="False" 
                CommandArgument="1" CommandName="SwitchViewByIndex" Text="Add Discounts" />
            <br />
            <br />
            <asp:Button ID="btnEditOrDeleteDisconts" runat="server" 
                CausesValidation="False" CommandArgument="2" CommandName="SwitchViewByIndex" 
                Text="Update or Delete Discounts" />
            <asp:LinqDataSource ID="ldsDiscounts" runat="server" 
                ContextTypeName="TreasureLand.TreasureLandDataClassesDataContext" 
                EnableDelete="True" EnableInsert="True" EnableUpdate="True" EntityTypeName="" 
                OrderBy="DiscountID" TableName="Discounts">
            </asp:LinqDataSource>
        </asp:View>
        <asp:View ID="vDiscountAdd" runat="server">
            <table style="width: 100%;">
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
                            ErrorMessage="Must enter a valid date" ForeColor="Red" Operator="DataTypeCheck"></asp:CompareValidator>
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
                        <asp:CompareValidator ID="cvDiscountAmount" runat="server" Display="Dynamic"
                             ControlToValidate="txtDiscountAmount" 
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
            <asp:Button ID="btnInsertDiscount" runat="server" CommandArgument="2" 
                CommandName="SwitchViewByIndex" onclick="btnInsertDiscount_Click" 
                Text="Add Discount" />
            <br />
            <br />
            <asp:Button ID="btnBackFromAdd" runat="server" CausesValidation="False" 
                CommandArgument="0" CommandName="SwitchViewByIndex" Text="Back" />
        </asp:View>
        <asp:View ID="vDiscountsUpdate" runat="server">
            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" 
                DataKeyNames="DiscountID" DataSourceID="ldsDiscounts">
                <Columns>
                    <asp:BoundField DataField="DiscountID" HeaderText="DiscountID" 
                        InsertVisible="False" ReadOnly="True" SortExpression="DiscountID" />
                    <asp:BoundField DataField="DiscountDescription" 
                        HeaderText="DiscountDescription" SortExpression="DiscountDescription" />
                    <asp:BoundField DataField="DiscountExpiration" DataFormatString="{0:d}" 
                        HeaderText="DiscountExpiration" SortExpression="DiscountExpiration" />
                    <asp:BoundField DataField="DiscountRules" HeaderText="DiscountRules" 
                        SortExpression="DiscountRules" />
                    <asp:BoundField DataField="DiscountAmount" HeaderText="DiscountAmount" 
                        SortExpression="DiscountAmount" />
                    <asp:CheckBoxField DataField="IsPrecentage" HeaderText="IsPrecentage" 
                        SortExpression="IsPrecentage" />
                    <asp:CommandField ButtonType="Button" ShowEditButton="True" />
                    <asp:CommandField ButtonType="Button" ShowDeleteButton="True" />
                </Columns>
            </asp:GridView>
            <br />
            <br />
            <asp:Button ID="btnBackFromEdit" runat="server" CausesValidation="False" 
                CommandArgument="0" CommandName="SwitchViewByIndex" Text="Back" />
        </asp:View>
        <br />
        <br />
    </asp:MultiView>
</asp:Content>
