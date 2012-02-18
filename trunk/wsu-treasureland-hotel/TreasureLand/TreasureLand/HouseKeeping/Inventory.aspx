<%@ Page Title="" Language="C#" MasterPageFile="~/HouseKeeping/HouseKeepingMasterPage.master" AutoEventWireup="true" CodeBehind="Inventory.aspx.cs" Inherits="TreasureLand.HouseKeeping.WebForm3" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table>
    <tr>
        <td style="width: 35px; height: 30px">
            <asp:Label ID="lblDate" runat="server" Text="Date:"></asp:Label>
        </td>
        <td style="width: 138px; height: 30px">
            <asp:TextBox ID="txtDate" runat="server" MaxLength="16"></asp:TextBox>
            <asp:RequiredFieldValidator ID="rfDate" runat="server" 
                ControlToValidate="txtDate" Display="Dynamic" 
                ErrorMessage="Date is a required field" ForeColor="Red" 
                ValidationGroup="vgAddExpense">*</asp:RequiredFieldValidator>
            <asp:CompareValidator ID="cvDate" runat="server" ControlToValidate="txtDate" 
                Display="Dynamic" ErrorMessage="Date must be a date" ForeColor="Red" 
                Operator="DataTypeCheck" Type="Date" ValidationGroup="vgAddExpense">*</asp:CompareValidator>
        </td>
    </tr>
    <tr>
        <td style="width: 35px; height: 30px">
            <asp:Label ID="lblInventoryName" runat="server" Text="Name"></asp:Label>
        </td>
        <td style="width: 138px; height: 30px">
            <asp:TextBox ID="txtInventoryName" runat="server" MaxLength="16"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td style="width: 35px; height: 30px">
            <asp:Label ID="lblInventoryType" runat="server" Text="Type:"></asp:Label>
        </td>
        <td style="width: 138px; height: 30px">
            <asp:DropDownList ID="ddlInventoryType" runat="server" DataSourceID="LinqDataSource1" 
                DataTextField="DepartmentID" DataValueField="Department">
            </asp:DropDownList>
        </td>
    </tr>
    </table>
&nbsp;&nbsp;&nbsp;&nbsp;<br />
    &nbsp;&nbsp;<asp:Button ID="btnAddExpense" runat="server" Text="Add Short Term Asset" 
    Width="150px" onclick="btnAddExpense_Click" style="margin-bottom: 0px" 
        ValidationGroup="vgAddExpense" Height="26px" />
&nbsp;<asp:ValidationSummary ID="ValidationSummary1" runat="server" ForeColor="Red" 
        ValidationGroup="vgAddExpense" />
    <asp:LinqDataSource ID="LinqDataSource1" runat="server" 
        ContextTypeName="TreasureLand.DBM.TreasureLandDataClassesDataContext" 
        EnableDelete="True" EnableInsert="True" EnableUpdate="True" EntityTypeName="" 
        TableName="ShortTermAssets">
    </asp:LinqDataSource>
    <asp:LinqDataSource ID="LinqDataSource2" runat="server" 
        ContextTypeName="TreasureLand.DBM.TreasureLandDataClassesDataContext" 
        EnableDelete="True" EnableInsert="True" EnableUpdate="True" EntityTypeName="" 
        TableName="ShortTermAssets">
    </asp:LinqDataSource>
    <asp:GridView ID="gvInventory" runat="server" AllowPaging="True" 
        AllowSorting="True" AutoGenerateColumns="False" DataKeyNames="ShortTermAssetID" 
        DataSourceID="LinqDataSource2">
        <Columns>
            <asp:CommandField ShowDeleteButton="True" ShowEditButton="True" />
            <asp:BoundField DataField="ShortTermItemName" HeaderText="Item Name" 
                SortExpression="ShortTermItemName" />
            <asp:DynamicField DataField="Department" HeaderText="Department" />
            <asp:BoundField DataField="ShortTermTotalQuantity" HeaderText="Total Quantity" 
                SortExpression="ShortTermTotalQuantity" />
        </Columns>
    </asp:GridView>
    <br />
</asp:Content>
